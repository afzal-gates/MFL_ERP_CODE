//=============== Start for LearningCurvController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('LearningCurvController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', LearningCurvController]);
    function LearningCurvController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modal, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        //var key = 'GMT_LRN_CURVE_ID';
        vm.form = { GMT_LRN_CURVE_ID: 0 };
        vm.errstyle = { 'border': '1px solid #f13d3d' };
        //$scope.form = formData[key] ? formData : {
        //    GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: ''
        //};
        
              
       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getFiscalYrList()];
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

            return PlanningDataService.getDataByFullUrl('/api/common/GetPayFiscalYear?pIS_CLOSED=N').then(function (res) {
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


       
        vm.fabClassMainGridOption = {
            height: 450,
            sortable: true,
            columns: [
                { field: "FAB_CLASS_NAME_EN", title: "Fabric Class", width: "100%" }                
            ],
            dataBound: function () {
                this.expandRow(this.tbody.find("tr.k-master-row"));
            }
        };
        
        vm.fabClassMainGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return PlanningDataService.getDataByFullUrl('/api/common/GmtFabClassList').then(function (res) {

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
            }
        });

        vm.fabClassDetailGridOption = function (dtlDataItem) {

            console.log(dtlDataItem);

            //if (dtlDataItem) {
            //    return PlanningDataService.getDataByFullUrl('/api/pln/LearningCurv/GetLearningCurvList?pRF_FAB_CLASS_ID=' + (dtlDataItem.RF_FAB_CLASS_ID || 0)).then(function (res) {
            //        if (res.length > 0) {
            //            e.success(res);
            //        }
            //        else {
            //            e.success([]);
            //        }
            //    }, function (err) {
            //        console.log(err);
            //    });
            //}

            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            return PlanningDataService.getDataByFullUrl('/api/pln/LearningCurv/GetLearningCurvList?pRF_FAB_CLASS_ID=' + (dtlDataItem.RF_FAB_CLASS_ID || 0)).then(function (res) {
                                
                                e.success(res);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    },
                    //serverPaging: true,
                    //serverSorting: true,
                    //serverFiltering: true,
                    //pageSize: 5,
                    //filter: { field: "RF_FAB_CLASS_ID", operator: "eq", value: dtlDataItem.RF_FAB_CLASS_ID }
                },
                scrollable: true,
                sortable: true,
                //pageable: true,
                columns: [                    
                    { field: "PRODUCT_TYP_NAME_EN", title: "Prod.Type", width: "10%" },
                    {
                        title: "Day-1",
                        field: "DAY1_PE",                        
                        template: function () {
                            return "<ng-form name='frmDAY1_PE'> <input type='number' class='form-control' name='DAY1_PE' ng-model='dataItem.DAY1_PE' min='0' max='{{dataItem.LEAST_POSBLE_EFF}}' ng-style='frmDAY1_PE.DAY1_PE.$error.max? vm.errstyle:\"\"||frmDAY1_PE.DAY1_PE.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLED_DAY1==\"Y\"' /> </ng-form>";
                        },
                        width: "9%",
                        filterable: false
                    },
                    {
                        title: "Day-2",
                        field: "DAY2_PE",                        
                        template: function () {
                            return "<ng-form name='frmDAY2_PE'> <input type='number' class='form-control' name='DAY2_PE' ng-model='dataItem.DAY2_PE' min='0' max='{{dataItem.LEAST_POSBLE_EFF}}' ng-style='frmDAY2_PE.DAY2_PE.$error.max? vm.errstyle:\"\"||frmDAY2_PE.DAY2_PE.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLED_DAY2==\"Y\"' />";
                        },
                        width: "9%",
                        filterable: false
                    },

                    {
                        title: "Day-3",
                        field: "DAY3_PE",
                        template: function () {
                            return "<ng-form name='frmDAY3_PE'> <input type='number' class='form-control' name='DAY3_PE' ng-model='dataItem.DAY3_PE' min='0' max='{{dataItem.LEAST_POSBLE_EFF}}' ng-style='frmDAY3_PE.DAY3_PE.$error.max? vm.errstyle:\"\"||frmDAY3_PE.DAY3_PE.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLED_DAY3==\"Y\"' />";
                        },
                        width: "9%",
                        filterable: false
                    },
                    {
                        title: "Day-4",
                        field: "DAY4_PE",
                        template: function () {
                            return "<ng-form name='frmDAY4_PE'> <input type='number' class='form-control' name='DAY4_PE' ng-model='dataItem.DAY4_PE' min='0' max='{{dataItem.LEAST_POSBLE_EFF}}' ng-style='frmDAY4_PE.DAY4_PE.$error.max? vm.errstyle:\"\"||frmDAY4_PE.DAY4_PE.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLED_DAY4==\"Y\"' />";
                        },
                        width: "9%",
                        filterable: false
                    },
                    {
                        title: "Day-5",
                        field: "DAY5_PE",
                        template: function () {
                            return "<ng-form name='frmDAY5_PE'> <input type='number' class='form-control' name='DAY5_PE' ng-model='dataItem.DAY5_PE' min='0' max='{{dataItem.LEAST_POSBLE_EFF}}' ng-style='frmDAY5_PE.DAY5_PE.$error.max? vm.errstyle:\"\"||frmDAY5_PE.DAY5_PE.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLED_DAY5==\"Y\"' />";
                        },
                        width: "9%",
                        filterable: false
                    },
                    {
                        title: "Day-6",
                        field: "DAY6_PE",
                        template: function () {
                            return "<ng-form name='frmDAY6_PE'> <input type='number' class='form-control' name='DAY6_PE' ng-model='dataItem.DAY6_PE' min='0' max='{{dataItem.LEAST_POSBLE_EFF}}' ng-style='frmDAY6_PE.DAY6_PE.$error.max? vm.errstyle:\"\"||frmDAY6_PE.DAY6_PE.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLED_DAY6==\"Y\"' />";
                        },
                        width: "9%",
                        filterable: false
                    },
                    {
                        title: "Day-7",
                        field: "DAY7_PE",
                        template: function () {
                            return "<ng-form name='frmDAY7_PE'> <input type='number' class='form-control' name='DAY7_PE' ng-model='dataItem.DAY7_PE' min='0' max='{{dataItem.LEAST_POSBLE_EFF}}' ng-style='frmDAY7_PE.DAY7_PE.$error.max? vm.errstyle:\"\"||frmDAY7_PE.DAY7_PE.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLED_DAY7==\"Y\"' />";
                        },
                        width: "9%",
                        filterable: false
                    },
                    {
                        title: "Day-8",
                        field: "DAY8_PE",
                        template: function () {
                            return "<ng-form name='frmDAY8_PE'> <input type='number' class='form-control' name='DAY8_PE' ng-model='dataItem.DAY8_PE' min='0' max='{{dataItem.LEAST_POSBLE_EFF}}' ng-style='frmDAY8_PE.DAY8_PE.$error.max? vm.errstyle:\"\"||frmDAY8_PE.DAY8_PE.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLED_DAY8==\"Y\"' />";
                        },
                        width: "9%",
                        filterable: false
                    },
                    {
                        title: "Day-9",
                        field: "DAY9_PE",
                        template: function () {
                            return "<ng-form name='frmDAY9_PE'> <input type='number' class='form-control' name='DAY9_PE' ng-model='dataItem.DAY9_PE' min='0' max='{{dataItem.LEAST_POSBLE_EFF}}' ng-style='frmDAY9_PE.DAY9_PE.$error.max? vm.errstyle:\"\"||frmDAY9_PE.DAY9_PE.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLED_DAY9==\"Y\"' />";
                        },
                        width: "9%",
                        filterable: false
                    },
                    {
                        title: "Day-10",
                        field: "DAY10_PE",
                        template: function () {
                            return "<ng-form name='frmDAY10_PE'> <input type='number' class='form-control' name='DAY10_PE' ng-model='dataItem.DAY10_PE' min='0' max='{{dataItem.LEAST_POSBLE_EFF}}' ng-style='frmDAY10_PE.DAY10_PE.$error.max? vm.errstyle:\"\"||frmDAY10_PE.DAY10_PE.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.IS_DISABLED_DAY10==\"Y\"' />";
                        },
                        width: "9%",
                        filterable: false
                    }
                    //{
                    //    title: "Status",
                    //    field: "TASK_STATUS_NAME",
                    //    template: function () {
                    //        return '<span>{{dataItem.TASK_STATUS_NAME}}</span>&nbsp;<a style="color:blue" ng-show="dataItem.IS_BYR_FEEDBK==\'Y\'" ng-click="byrFeedBackReport(dataItem)">[Rcvd Feedback]</a>';
                    //    },
                    //    width: "180px"
                    //}                    
                ]
            };

        };
        


        vm.save = function (grid) {

            var submitData = angular.copy(vm.form);
            
            var data = grid.dataSource.data();
            var lrnCurvDataList = [];

            angular.forEach(data, function (val, key) {
                if (val['ChildGrid'] != undefined) {
                    //console.log(val['ChildGrid'].dataSource.data());
                    angular.forEach(val['ChildGrid'].dataSource.data(), function (val1, key1) {
                        //console.log(val1);
                        val1['RF_FAB_CLASS_ID'] = val['RF_FAB_CLASS_ID'];
                        lrnCurvDataList.push(val1);
                    });
                }
                
            });


            submitData['GMT_LRN_CURVE_XML'] = PlanningDataService.xmlStringShort(lrnCurvDataList.map(function (ob) {
                //console.log(ob);
                return ob;
            }));
            
            console.log(submitData);
            console.log(lrnCurvDataList);
            //return;

            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    //console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/LearningCurv/BatchSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {                                                                
                                vm.fabClassMainGridDataSource.read();
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
//=============== End for LearningCurvController =================

