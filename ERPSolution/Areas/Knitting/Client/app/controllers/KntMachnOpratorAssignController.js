(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntMachnOpratorAssignController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', '$http', 'commonDataService', KntMachnOpratorAssignController]);
    function KntMachnOpratorAssignController($q, config, KnittingDataService, $stateParams, $state, $scope, $filter, $http, commonDataService) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.dtTimeFormat = config.appDateTimeFormat;

        vm.today = kendo.toString(new Date(), "dd/MMM/yyyy hh:mm tt"); //$filter('date')(new Date(), "dd/MMM/yyyy hh:mm t");
        vm.KNT_MACHINE_ID = 0;

        var key = 'KNT_MACHN_OPR_ID';
        vm.form = { EFFECT_FROM: vm.today, KNT_MACHINE_ID: null };

       

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getMachineList(), getOperatorList(), getScheduleList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;

                //vm.form = formData[key] ? formData : { SC_PRG_DT: vm.today };
                //$scope.form = vm.form;
            });
        }
        
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.EFFECT_FROM_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.EFFECT_FROM_LNopened = true;
        }

        vm.EXPIRED_ON_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.EXPIRED_ON_LNopened = true;
        }

        vm.isDate = function (x) {
            return x instanceof Date;
        };

        function getMachineList() {
            return vm.machineList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/KnitCommon/GetMachineList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 15
            });
        };

        function getOperatorList() {
            return vm.operatorList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/hr/GetEmployeeList?pLK_JOB_STATUS_ID=30&pHR_DESIGNATION_GRP_ID=21&pCORE_DEPT_ID=46').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 7
            });
        };

        function getScheduleList() {
            vm.scheduleOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SCHEDULE_NAME_EN",
                dataValueField: "HR_SCHEDULE_H_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    vm.fabOrder.COLOR_NAME_EN = item.COLOR_NAME_EN;
                //}
            };

            return vm.scheduleDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        
                        KnittingDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        //vm.filterTaskList = function () {
        //    if (vm.form.TASK_NAME.length >= 3)
        //        vm.TnaTaskList.query({ filter: { field: 'TA_TASK_NAME_EN', operator: 'contains', value: vm.form.TASK_NAME }, pageSize: 15, page: 1 });
        //    else if (vm.form.TASK_NAME.length == 0)
        //        vm.TnaTaskList.query({ filter: {}, pageSize: 15, page: 1 });
        //}

        vm.onChangeMachine = function (dataItem) {
            vm.reset();
            var item = dataItem;

            vm.KNT_MACHINE_ID = dataItem.KNT_MACHINE_ID;

            vm.form.KNT_MACHINE_ID = dataItem.KNT_MACHINE_ID;
            vm.form.KNT_MACHINE_NO = dataItem.KNT_MACHINE_NO;
            console.log(dataItem);

            //vm.reset();
            vm.assignedGridDataSource.read();
        };

        vm.onChangeOperator = function (dataItem) {

            var item = dataItem;

            vm.form.HR_EMPLOYEE_ID = dataItem.HR_EMPLOYEE_ID;
            vm.form.EMPLOYEE_CODE = dataItem.EMPLOYEE_CODE;
            vm.form.EMP_FULL_NAME_EN = dataItem.EMP_FULL_NAME_EN;
        };

        vm.Save = function () {
            console.log(vm.form);
            //return;

            var submitData = angular.copy(vm.form);
            submitData['OPERATOR_ID'] = submitData.HR_EMPLOYEE_ID;
            //submitData['EFFECT_FROM'] = $filter('date')(submitData.EFFECT_FROM, config.appDateFormat);

            return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/KntMachinOprAssign/Save').then(function (res) {
                console.log(res);

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.reset();
                        vm.assignedGridDataSource.read();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

            
        };
        
        //$scope.$watch('vm.form', function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            $scope.form = vm.form;
        //            $scope.form.SC_PRG_DT = $filter('date')(vm.form.SC_PRG_DT, vm.dtFormat);
        //        }
        //    }
        //}, true);

        vm.reset = function () {
            vm.form.KNT_MACHN_OPR_ID = 0;
            vm.form.EFFECT_FROM = vm.today;
            vm.form.KNT_MACHINE_ID = null;
            vm.form.KNT_MACHINE_NO = null;
            vm.form.HR_EMPLOYEE_ID = null;
            vm.form.EMPLOYEE_CODE = null;
            vm.form.EMP_FULL_NAME_EN = null;
        }

        vm.assignedGridOption = {
            //height: 350,
            sortable: true,
            selectable: "row",
            //pageable: true,
            filterable: false,           
            columns: [
                { field: "SCHEDULE_NAME_EN", title: "Schedule", width: "33%" },
                { field: "EMPLOYEE_CODE", title: "Emp.Code", type: "string", width: "20%" },
                { field: "EMP_FULL_NAME_EN", title: "Name", width: "30%" },
                { field: "EFFECT_FROM", title: "Eff.Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "17%" },
            ],
            change: function (e) {
                var grid = $("#assignedGrid").data("kendoGrid");
                //grid.select("tr:eq(1)");
                var row = grid.select();
                
                var dataItem = angular.copy(grid.dataItem(row));
                var fromDateTime = kendo.toString(dataItem.EFFECT_FROM, "dd/MMM/yyyy hh:mm tt");
                console.log(dataItem);

                dataItem.HR_EMPLOYEE_ID = dataItem.OPERATOR_ID;
                dataItem.EFFECT_FROM = fromDateTime;
                vm.form = dataItem;
                $scope.$apply();
                console.log(vm.form);
            }
        };                

        vm.assignedGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return KnittingDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetAssiPersonListByMachinId?pKNT_MACHINE_ID=' + vm.KNT_MACHINE_ID).then(function (res) {

                        console.log(res);

                        if (res.length > 0) {
                            e.success(res);
                        }
                        else {                            
                            e.success([]);
                        }

                    }, function (err) {
                        console.log(err);
                    });
                }
            },            
            schema: {
                model: {
                    id: "KNT_MACHINE_ID",
                    fields: {
                        EFFECT_FROM: { type: "date", editable: false }
                    }
                }
            }
        });

       


        
    }


})();
