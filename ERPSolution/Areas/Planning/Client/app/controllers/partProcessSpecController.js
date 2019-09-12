//=============== Start for PartProcessSpecController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('PartProcessSpecController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modalInstance', '$timeout', 'GmtPartID', PartProcessSpecController]);
    function PartProcessSpecController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modalInstance, $timeout, GmtPartID) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        var prodTypeList = [];
        
        vm.form = { GMT_PART_OPR_SPEC_ID: 0, RF_GARM_PART_ID: null };
        
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
            var promise = [getGmtPartList(), getprocSpecList()];
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
        
     
       
        
        vm.onChangeGmtPart = function (dataItem) {
            console.log(dataItem);
            var item = angular.copy(dataItem);

            vm.form.RF_GARM_PART_ID = item.RF_GARM_PART_ID;

            vm.procSpecDataSource.read();
        };
       
        
        vm.onDataBoundGmtPart = function (e) {
            console.log(e);
            
            var listView = e.sender;

            if (GmtPartID != null) {
                var listViewItem = listView.element.children("[data-uid='" + listView.dataSource.get(GmtPartID).uid + "']");
                //console.log(listViewItem);

                //listView.select(listViewItem);
            }
            else {
                var listViewItem = listView.element.children().first();
            }
            listView.select(listViewItem);
        };

        function getGmtPartList() {

            return vm.gmtPartDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/common/GmtPartList?pIS_PROC_GRP=Y').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    model: {
                        id: "RF_GARM_PART_ID",
                        fields: {
                            RF_GARM_PART_ID: { type: "number" },
                            GARM_PART_NAME: { type: "string" }
                        }
                    }
                }
            });
        };


        function getprocSpecList() {

            return vm.procSpecDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/PlnPartProcSpec/GetGmtPartProcSpecList?pRF_GARM_PART_ID=' + (vm.form.RF_GARM_PART_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.procSpecOption = {
            height: '400px',
            sortable: false,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            columns: [                
                { field: "PROC_SPEC_SL", title: "SL", width: "10%" },
                {
                    title: "Process Spec",
                    field: "PART_OPR_SPEC",
                    template: function () {
                        return "<ng-form name='frmPART_OPR_SPEC'> <input type='text' class='form-control' name='PART_OPR_SPEC' ng-model='dataItem.PART_OPR_SPEC' required ng-style='frmPART_OPR_SPEC.PART_OPR_SPEC.$error.required? vm.errstyle:\"\"'  /> </ng-form>";
                    },
                    width: "75%",
                    filterable: false
                },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.addRow(dataItem)' ><i class='fa fa-plus'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='(vm.thrghPutTimeGridDataSource.data().length==1)' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "15%"
                }
            ]
        };
        
        vm.addRow = function (dataItem) {
            console.log(dataItem);
            //var dataCopy = angular.copy(data);

            var data = {
                GMT_PART_OPR_SPEC_ID: 0, PART_OPR_SPEC: null, PROC_SPEC_SL: null
            };

            vm.procSpecDataSource.insert(data);
        }

        vm.removeRow = function (dataItem) {
            vm.procSpecDataSource.remove(dataItem);
        };
               
        vm.save = function () {

            var submitData = angular.copy(vm.form);                                   

            var dataList = vm.procSpecDataSource.data();
            


            submitData['GMT_PART_OPR_SPEC_XML'] = PlanningDataService.xmlStringShort(dataList.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            console.log(submitData);


            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/PlnPartProcSpec/BatchSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                
                                vm.procSpecDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        $scope.cancel = function () {
            //$modalInstance.dismiss('cancel');
            var data = { RF_GARM_PART_ID: vm.form.RF_GARM_PART_ID, GARM_PART_CODE: vm.form.GARM_PART_CODE, GARM_PART_NAME: vm.form.GARM_PART_NAME, IS_FLAT_CIR: vm.form.IS_FLAT_CIR, IS_ACTIVE: vm.form.IS_ACTIVE };
            $modalInstance.close(data);
        };


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
