//=============== Start for CapctiClndrController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('CapctyClndrController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', CapctyClndrController]);
    function CapctyClndrController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modal, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        //var key = 'GMT_SW_MCN_SPEC_ID';
        vm.form = { RF_FISCAL_YEAR_ID: 0 };
        vm.procForm = {};
        //$scope.form = formData[key] ? formData : {
        //    GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: ''
        //};
        
              
       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getFiscalYrList(), getClndrYearList(), getClndrMonthList()];
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
        
         
        function getFiscalYrList() {

            vm.fiscalYrOption = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.procForm.YR_START_DATE = item.FY_START_DATE;
                    vm.procForm.YR_END_DATE = item.FY_END_DATE;

                    $state.go('CapctyClndr', { pRF_FISCAL_YEAR_ID: item.RF_FISCAL_YEAR_ID}, { notify: false });
                },
                dataBound: function (e) {
                    
                    if ($stateParams.pRF_FISCAL_YEAR_ID > 0) {
                        
                        var list = vm.fiscalYrDataSource.data();
                        console.log(list);

                        var item = _.filter(list, function (ob) {
                            return ob.RF_FISCAL_YEAR_ID == $stateParams.pRF_FISCAL_YEAR_ID;
                        });
                        console.log(item);

                        vm.procForm.RF_FISCAL_YEAR_ID = item[0].RF_FISCAL_YEAR_ID;
                        vm.procForm.YR_START_DATE = item[0].FY_START_DATE;
                        vm.procForm.YR_END_DATE = item[0].FY_END_DATE;
                    }
                },
                dataTextField: "FY_NAME_EN",
                dataValueField: "RF_FISCAL_YEAR_ID"
            };

            return PlanningDataService.getDataByFullUrl('/api/common/GetPayFiscalYear').then(function (res) {
                vm.fiscalYrDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
        }
     
        function getClndrYearList() {
            return vm.clndrYearDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/CapctiClndr/GetClndrYearList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 12
            });
        };

        vm.onChangeClndrYear = function (dataItem) {
            console.log(dataItem);
            var item = angular.copy(dataItem);
            vm.form.RF_FISCAL_YEAR_ID = item.RF_FISCAL_YEAR_ID;
            vm.form.PARENT_ID = null;
            vm.form.RF_CAL_MONTH_ID = null;

            vm.clndrMonthDataSource.read();
            vm.monthlyWeekGridDataSource.read();
        };

        function getClndrMonthList() {
            return vm.clndrMonthDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/CapctiClndr/GetClndrMonthList?pRF_FISCAL_YEAR_ID=' + (vm.form.RF_FISCAL_YEAR_ID||0)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 12
            });
        };

        vm.onChangeClndrMonth = function (dataItem) {
            console.log(dataItem);
            var item = angular.copy(dataItem);

            vm.form.PARENT_ID = item.GMT_PROD_PLN_CLNDR_ID;
            vm.form.RF_CAL_MONTH_ID = item.RF_CAL_MONTH_ID;

            vm.monthlyWeekGridDataSource.read();
        };


       
        vm.monthlyWeekGridOption = {
            height: 450,
            sortable: true,
            columns: [
                { field: "YR_WK_NO", title: "Year WK#", width: "25%" },
                { field: "MN_WK_NO", title: "Month WK#", width: "25%" },
                { field: "WK_START_DATE", title: "Start", format: "{0:dd-MMM-yyyy}", width: "25%" },
                { field: "WK_END_DATE", title: "End", format: "{0:dd-MMM-yyyy}", width: "25%" }//,
                //{
                //    title: "Action",
                //    template: function () {
                //        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editMcnProfileRow(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;" +
                //            "<button type='button' class='btn btn-xs red' ng-click='vm.removeMcnProfileRow(dataItem)'><i class='fa fa-remove'></i></button>";
                //    },
                //    width: "10%"
                //}
            ]
        };

        vm.monthlyWeekGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return PlanningDataService.getDataByFullUrl('/api/pln/CapctiClndr/GetClndrWkList?pRF_FISCAL_YEAR_ID=' + (vm.form.RF_FISCAL_YEAR_ID || 0) + '&pRF_CAL_MONTH_ID=' + (vm.form.RF_CAL_MONTH_ID || 0) + '&pPARENT_ID=' + (vm.form.PARENT_ID || 0)).then(function (res) {

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
                    id: "GMT_PROD_PLN_CLNDR_ID",
                    fields: {                        
                        WK_START_DATE: { type: "date", editable: false },
                        WK_END_DATE: { type: "date", editable: false }
                    }
                }
            }
        });
        // ============= End Machine Profile Detail ===========


        vm.clndrProcess = function () {

            var submitData = angular.copy(vm.procForm);
            
            console.log(submitData);
            //return;

            Dialog.confirm('Do you want to process?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    //console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/CapctiClndr/ProdClndrProcess').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {                                                                
                                vm.clndrYearDataSource.read();
                                //vm.clndrMonthDataSource.read();
                                //vm.monthlyWeekGridDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

       


    }
})();
//=============== End for CapctiClndrController =================

