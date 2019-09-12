//=============== Start for ThroughPutTimeController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('ItemPartMapController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', ItemPartMapController]);
    function ItemPartMapController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modal, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        var prodTypeList = [];
        
        vm.form = {GMT_PART_GRP_CAT_ID: null, GMT_CAT_ID: null};
        
        //$scope.form = formData[key] ? formData : {
        //    GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: ''
        //};

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };
        
              
       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getGmtCategoryGroupList(), getGmtCategoryList(), getGmtPartList()];
            return $q.all(promise).then(function () {
               
                vm.showSplash = false;                
            });
        }

                    

        //$scope.$watch('vm.form', function (newVal, oldVal) {
        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            $scope.form = vm.form;
        //        }
        //    }
        //}, true);


        //$scope.$watchGroup(['vm.form.RF_FAB_PROD_CAT_ID', 'vm.form.MC_BYR_ACC_ID', 'vm.form.MC_STYLE_H_EXT_ID'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            vm.IS_NEXT = 'N';
        //        }
        //    }
        //}, true);
        
     
        function getGmtCategoryGroupList() {
            return vm.gmtCategoryGroupDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/mrc/StyleItem/InvItemByParent/8').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });            
        };

        vm.onChangeGmtCategoryGroup = function (dataItem) {
            console.log(dataItem);
            var item = angular.copy(dataItem);

            vm.form.GMT_CAT_GRP_ID = item.INV_ITEM_CAT_ID;
            
            vm.gmtCategoryDataSource.read();
        };

        vm.onDataBoundGmtCategoryGroup = function (e) {
            console.log(e);

            var listView = e.sender;
            var firstItem = listView.element.children().first();
            listView.select(firstItem);
        };

        function getGmtCategoryList() {
            return vm.gmtCategoryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/inv/ItemCategory/ItemCategList4LastLevel?pPARENT_ID=' + (vm.form.GMT_CAT_GRP_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };
        
        vm.onChangeGmtCategory = function (dataItem) {
            console.log(dataItem);
            var item = angular.copy(dataItem);

            vm.form.GMT_CAT_ID = item.INV_ITEM_CAT_ID;

            vm.gmtPartDataSource.read();
        };
       
        vm.onDataBoundGmtCategory = function (e) {
            console.log(e);
            
            var listView = e.sender;
            var firstItem = listView.element.children().first();
            listView.select(firstItem);            
        };

        function getGmtPartList() {

            return vm.gmtPartDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/PlnItemPartMap/GetCategGmtPartMapList?pINV_ITEM_CAT_ID=' + (vm.form.GMT_CAT_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.gmtPartOption = {
            height: '400px',
            sortable: false,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            columns: [
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.selectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Select?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Select?' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"' >";
                    },
                    width: "3%"
                },
                { field: "GARM_PART_NAME", title: "Part Name", width: "20%" }
            ]
        };
        
        vm.selectAll = function (v, index) {
            var data = vm.gmtPartDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            console.log(data);

            angular.forEach(data, function (val, key) {
                val['IS_ACTIVE'] = v;
            });
        };
        
               

        vm.save = function () {

            var submitData = angular.copy(vm.form);            
            
            submitData['INV_ITEM_CAT_ID'] = submitData.GMT_CAT_ID;

            var gmtPartDataList = vm.gmtPartDataSource.data();
            var dataList = _.filter(gmtPartDataList, function (ob) {
                return ob.IS_ACTIVE == 'Y';
            });


            submitData['GMT_MAP_GARM_PART_XML'] = PlanningDataService.xmlStringShort(dataList.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            console.log(submitData);


            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/PlnItemPartMap/BatchSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                
                                vm.gmtPartDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.newGmtPart = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'NewGmtPartModal.html',
                controller: 'GmtPartModalController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'large-Modal'//,
                //resolve: {
                //    FiberCompList: function () {
                //        return vm.fibCompDataSource.data();
                //    },
                //    FiberTypeList: function () {
                //        return KnittingDataService.LookupListData(76);
                //    },
                //    FiberCompGroup: function () {
                //        return KnittingDataService.getDataByFullUrl('/api/mrc/YarnSpec/FiberCompGroup');
                //    }
                //}
            });

            modalInstance.result.then(function (result) {

                //if (result.RF_FIB_COMP_ID && result.RF_FIB_COMP_ID > 0) {
                //    vm.fibCompDataSource.read().then(function () {
                //        vm.fabOrder['RF_FIB_COMP_ID'] = result.RF_FIB_COMP_ID;
                //    });
                //}
                vm.gmtPartDataSource.read();

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }


    }
})();
//=============== End for ThroughPutTimeController =================










//=============== Start for GmtPartModalController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('GmtPartModalController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modalInstance', GmtPartModalController]);
    function GmtPartModalController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, commonDataService, $modalInstance) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

       
        vm.form = { RF_GARM_PART_ID: 0, GARM_PART_CODE: null, GARM_PART_NAME: null, IS_FLAT_CIR: 'C', IS_ACTIVE: 'Y' };


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.cancel = function () {
            //$modalInstance.dismiss('cancel');
            var data = { RF_GARM_PART_ID: vm.form.RF_GARM_PART_ID, GARM_PART_CODE: vm.form.GARM_PART_CODE, GARM_PART_NAME: vm.form.GARM_PART_NAME, IS_FLAT_CIR: vm.form.IS_FLAT_CIR, IS_ACTIVE: vm.form.IS_ACTIVE };
            $modalInstance.close(data);
        };

        vm.tranCancel = function () {
            vm.form = { RF_GARM_PART_ID: 0, GARM_PART_CODE: null, GARM_PART_NAME: null, IS_FLAT_CIR: 'C', IS_ACTIVE: 'Y' };
        }

        vm.editRow = function (dataItem) {
            var data = angular.copy(dataItem);

            vm.form = { RF_GARM_PART_ID: dataItem.RF_GARM_PART_ID, GARM_PART_CODE: dataItem.GARM_PART_CODE, GARM_PART_NAME: dataItem.GARM_PART_NAME, IS_FLAT_CIR: dataItem.IS_FLAT_CIR, IS_ACTIVE: dataItem.IS_ACTIVE };
        }

        vm.gmtPartGridOption = {
            height: 300,
            sortable: true,
            columns: [
                { field: "GARM_PART_CODE", title: "Code", width: "15%" },
                { field: "GARM_PART_NAME", title: "Name", width: "15%" },
                { field: "IS_FLAT_CIR", title: "Flat Knit?", width: "10%" },                
                { field: "IS_ACTIVE", title: "Active?", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "<a type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' ><i class='fa fa-edit'></i></a>"
                    },
                    width: "10%"
                }
            ]
        };

        vm.gmtPartGridDataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverFiltering: true,
            schema: {
                //data: "data",
                //total: "total",
                model: {
                    id: "RF_GARM_PART_ID"
                }
            },
            //pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/common/GmtPartList';
                    //url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;

                    console.log(url);

                    return PlanningDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });


        vm.save = function (token, valid) {

            //if (!valid) return;

            var obj = angular.copy(vm.form);


            //data.fabReqDtl = vm.obGrid;
            //vm.copyOrder = data;
            console.log(obj);
            return PlanningDataService.saveDataByFullUrl(obj, '/api/common/SaveGmtPart').then(function (res) {
                console.log(res);

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.form.RF_GARM_PART_ID = parseInt(res.data.PRF_GARM_PART_ID_RTN);
                        //$modalInstance.close(res.data);
                        //$scope.cancel();
                        vm.gmtPartGridDataSource.read();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });




        };


    }
})();
//=============== End for SFabBkReasonModalController =================
