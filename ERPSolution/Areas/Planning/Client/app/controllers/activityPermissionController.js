//=============== Start for ActivityPermissionHController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('ActivityPermissionHController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', ActivityPermissionHController]);
    function ActivityPermissionHController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modal, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        vm.eventAccessState = ($state.current.name === 'ActivityPermissionH.EventAccess') ? true : false;
        vm.resourceAccessState = ($state.current.name === 'ActivityPermissionH.ResourceAccess') ? true : false;
        vm.orderAccessState = ($state.current.name === 'ActivityPermissionH.OrderAccess') ? true : false;
        
        vm.form = {};
        
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
            var promise = [getUserList()];
            return $q.all(promise).then(function () {
               
                vm.showSplash = false;                
            });
        }
             
        function getUserList() {

            return vm.userDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/SelectAllUserData';

                        PlanningDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                },
                schema: {
                    model: {
                        id: "SC_USER_ID",
                        fields: {
                            SC_USER_ID: { type: "number" },
                            LOGIN_ID: { type: "string" }
                        }
                    }
                }
            });
        };

        vm.onChangeUser = function (dataItem) {
            console.log(dataItem);
            var item = angular.copy(dataItem);

            vm.form.SC_USER_ID = item.SC_USER_ID;
            
            if (vm.eventAccessState == true) {
                $state.go('ActivityPermissionH.EventAccess', { pSC_USER_ID: item.SC_USER_ID }, { reload: 'ActivityPermissionH.EventAccess' });
            }
            else if (vm.resourceAccessState == true) {
                $state.go('ActivityPermissionH.ResourceAccess', { pSC_USER_ID: item.SC_USER_ID }, { reload: 'ActivityPermissionH.ResourceAccess' });
            }
            else if (vm.orderAccessState == true) {
                $state.go('ActivityPermissionH.OrderAccess', { pSC_USER_ID: item.SC_USER_ID }, { reload: 'ActivityPermissionH.OrderAccess' });
            }

            //vm.gmtCategoryDataSource.read();
        };

        vm.onDataBoundUser = function (e) {
            console.log(e);

            var listView = e.sender;

            if ($stateParams.pSC_USER_ID > 0) {
                var listViewItem = listView.element.children("[data-uid='" + listView.dataSource.get($stateParams.pSC_USER_ID).uid + "']");
            }
            else {
                var listViewItem = listView.element.children().first();
            }
            
            listView.select(listViewItem);
        };

    }
})();
//=============== End for ActivityPermissionHController =================







//=============== Start for ActivityPermissionEventAccessController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('ActivityPermissionEventAccessController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', 'eventAccessData', '$timeout', ActivityPermissionEventAccessController]);
    function ActivityPermissionEventAccessController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, eventAccessData, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';
        var key = 'GMT_PLN_EVNT_ACCESS_ID';

        vm.formEvent = eventAccessData[key] ? eventAccessData : {
            GMT_PLN_EVNT_ACCESS_ID: 0, IS_CLK_DISABLED: 'N', IS_MOVE_V_DISABLED: 'N', IS_MOVE_H_DISABLED: 'N', IS_DELETE_DISABLED: 'N', IS_DBL_CLK_DISABLED: 'N',
            IS_MOVE_DISABLED: 'N', IS_RESIZE_DISABLED: 'N', IS_RIGHT_CLK_DISABLED: 'N'
        };

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };



        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {

                vm.showSplash = false;
            });
        }


        //vm.eventAccessOption = {
        //    height: '400px',
        //    sortable: false,
        //    scrollable: true,
        //    pageable: false,
        //    editable: false,
        //    selectable: "row",
        //    columns: [
        //        {
        //            headerTemplate: "<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.selectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Select?'\"></i>",
        //            template: function () {
        //                return "<input type='checkbox' title='Select?' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"' >";
        //            },
        //            width: "3%"
        //        },
        //        { field: "GARM_PART_NAME", title: "Part Name", width: "20%" }
        //    ]
        //};

       

        vm.save = function () {

            var submitData = angular.copy(vm.formEvent);

            submitData['SC_USER_ID'] = $stateParams.pSC_USER_ID;

            console.log(submitData);


            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/PlnActvtyPermission/EventSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                $state.go('ActivityPermissionH.EventAccess', { pSC_USER_ID: $stateParams.pSC_USER_ID }, { reload: 'ActivityPermissionH.EventAccess' });
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.cancel = function () {
            $state.go('ActivityPermissionH.EventAccess', { pSC_USER_ID: $stateParams.pSC_USER_ID }, { reload: 'ActivityPermissionH.EventAccess' });
            //vm.formEvent = {
            //    GMT_PLN_EVNT_ACCESS_ID: 0, IS_CLK_DISABLED: 'N', IS_MOVE_V_DISABLED: 'N', IS_MOVE_H_DISABLED: 'N', IS_DELETE_DISABLED: 'N', IS_DBL_CLK_DISABLED: 'N',
            //    IS_MOVE_DISABLED: 'N', IS_RESIZE_DISABLED: 'N', IS_RIGHT_CLK_DISABLED: 'N'
            //}
        }


    }
})();
//=============== End for ActivityPermissionEventAccessController =================








//=============== Start for ActivityPermissionResourceAccessController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('ActivityPermissionResourceAccessController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', 'resourceAccessData', '$timeout', ActivityPermissionResourceAccessController]);
    function ActivityPermissionResourceAccessController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, resourceAccessData, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        //vm.formResource = { GMT_PLN_RSRC_ACCESS_ID: 0 };

        console.log(resourceAccessData);

        if (resourceAccessData) {
            vm.floorList = resourceAccessData; //[{ HR_PROD_FLR_ID: 1, FLOOR_CODE: 'A', lineItems: [{ HR_PROD_LINE_ID: 1, LINE_NO: 1 }, { HR_PROD_LINE_ID: 2, LINE_NO: 2 }] }, { HR_PROD_FLR_ID: 2, FLOOR_CODE: 'B', lineItems: [{ HR_PROD_LINE_ID: 11, LINE_NO: 1 }] }];
        }
        else {
            vm.floorList = [];
        }
            //vm.floorList = [, { HR_PROD_LINE_ID: 11, LINE_NO: 1, FLOOR_CODE: 'B' }];

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };



        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {

                vm.showSplash = false;
            });
        }

        
        vm.save = function () {

            var submitData = { SC_USER_ID: $stateParams.pSC_USER_ID };
            
            var lnDataList = [];
            angular.forEach(vm.floorList, function (val, key) {
                angular.forEach(val['items'], function (val1, key1) {
                    lnDataList.push(val1);
                });
            });


            submitData['RSRC_ACCESS_XML'] = PlanningDataService.xmlStringShort(lnDataList.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            console.log(submitData);


            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/PlnActvtyPermission/ResourceSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                $state.go('ActivityPermissionH.ResourceAccess', { pSC_USER_ID: $stateParams.pSC_USER_ID }, { reload: 'ActivityPermissionH.ResourceAccess' });
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.cancel = function () {
            $state.go('ActivityPermissionH.ResourceAccess', { pSC_USER_ID: $stateParams.pSC_USER_ID }, { reload: 'ActivityPermissionH.ResourceAccess' });
        }


    }
})();
//=============== End for ActivityPermissionResourceAccessController =================









//=============== Start for ActivityPermissionOrderAccessController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('ActivityPermissionOrderAccessController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', ActivityPermissionOrderAccessController]);
    function ActivityPermissionOrderAccessController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modal, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        vm.formOrder = { GMT_PLN_ORD_ACCESS_ID: 0 };

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };



        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerAcList()];
            return $q.all(promise).then(function () {

                vm.showSplash = false;
            });
        }

        function getBuyerAcList() {
            
            vm.buyerAcOption = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,                
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.formOrder.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    vm.formOrder.BYR_ACC_NAME_EN = item.BYR_ACC_NAME_EN;
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };

            return vm.buyerAcDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/mrc/BuyerAcc/SelectAll').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }

        vm.ordAccessOption = {
            height: '300px',
            sortable: false,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            columns: [                
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "70%" },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' ><i class='fa fa-edit'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "30%"
                }
            ]
        };

        vm.ordAccessGridDataSource = new kendo.data.DataSource({            
            transport: {
                read: function (e) {                    
                    var url = '/api/pln/PlnActvtyPermission/GetPlnOrdAccess?pSC_USER_ID=' + $stateParams.pSC_USER_ID;
                   
                    console.log(url);

                    return PlanningDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

       
        vm.addRow = function (data) {            
            vm.ordAccessGridDataSource.insert(0, data);

            vm.cancelRow();
        }

        vm.updateIndex = 0;
        vm.editRow = function (dataItem) {
            var index = vm.ordAccessGridDataSource.indexOf(dataItem);
            //alert('index:' + index);

            console.log(dataItem);
            var data = angular.copy(dataItem);
            vm.formOrder = data;

            vm.isAddToGrid = 'N';
        };

        vm.removeRow = function (dataItem) {
            vm.ordAccessGridDataSource.remove(dataItem);
        };

        vm.updateRow = function (data) {
            var fabOrderCopy = angular.copy(data);

            var dataItem = vm.ordAccessGridDataSource.getByUid(data.uid);

            console.log(data);

            dataItem.set('MC_BYR_ACC_ID', data.MC_BYR_ACC_ID);
            dataItem.set('BYR_ACC_NAME_EN', data.BYR_ACC_NAME_EN);

            dataItem.set('editMode', false);

            vm.cancelRow();
        }

        vm.cancelRow = function () {
            var data = angular.copy(vm.formOrder);
            vm.formOrder = { GMT_PLN_ORD_ACCESS_ID: 0, MC_BYR_ACC_ID: data.MC_BYR_ACC_ID, BYR_ACC_NAME_EN: data.BYR_ACC_NAME_EN };

            vm.isAddToGrid = 'Y';
        }

        vm.save = function () {

            var submitData = { SC_USER_ID: $stateParams.pSC_USER_ID };
            
            var ordAccessDataList = vm.ordAccessGridDataSource.data();
            
            submitData['ORD_ACCESS_XML'] = PlanningDataService.xmlStringShort(ordAccessDataList.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            console.log(submitData);


            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/PlnActvtyPermission/OrdAccessSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                vm.ordAccessGridDataSource.read();
                                //$state.go('ActivityPermissionH.OrderAccess', { pSC_USER_ID: $stateParams.pSC_USER_ID }, { reload: 'ActivityPermissionH.OrderAccess' });
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.cancel = function () {
            vm.cancelRow();
            vm.ordAccessGridDataSource.read();
        }


    }
})();
//=============== End for ActivityPermissionOrderAccessController =================