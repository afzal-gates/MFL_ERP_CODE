//============== Start for CompPayPeriodController
(function () {
    'use strict';
    angular.module('multitex.hr').controller('CompPayPeriodController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', '$filter', '$modal', 'formData', 'Dialog', CompPayPeriodController]);

    function CompPayPeriodController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams, $filter, $modal, formData, Dialog) {
                        
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'ACC_PAY_PERIOD_ID';
        
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.today = new Date();
        vm.periodType = 3;
        vm.form = formData[key] ? formData : { ACC_PAY_PERIOD_ID: 0, IS_SHOW4_RPT: 'Y', IS_CLOSED: 'N' };

        
        
        activate();

        function activate() {
            var promise = [getCompanyList(), getPeriodType(), getFiscalYear(), getMonthList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        vm.START_DATE_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.START_DATE_Opened = true;
        }
        
        vm.END_DATE_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.END_DATE_Opened = true;
        }

        vm.PAY_DATE_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.PAY_DATE_Opened = true;
        }


        vm.newFY = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'NewFYEntryModal.html',
                controller: 'FYModalController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'large-Modal'//,
                //resolve: {
                //    ColourList: function (KnittingDataService) {
                //        return KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll');
                //    }
                //}
            });

            modalInstance.result.then(function (result) {
                console.log(result);

                if (result.RF_FISCAL_YEAR_ID && result.RF_FISCAL_YEAR_ID > 0) {
                    vm.fiscalYearDataSource.read().then(function () {
                        vm.form['RF_FISCAL_YEAR_ID'] = result.RF_FISCAL_YEAR_ID;
                        vm.form['FY_NAME_EN'] = result.FY_NAME_EN;
                    });
                }
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

        function getCompanyList() {
           
            vm.compOptions = {
                optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",                
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    //return vm.compPayPeriodDataSource = new kendo.data.DataSource({
                    //    transport: {
                    //        read: function (e) {
                    //            return HrService.getDataByFullUrl("/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + dataItem.HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodType + "&pIS_CLOSED=N").then(function (res) {
                    //                e.success(res);
                    //            });
                    //        }
                    //    }
                    //});
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.GetAllOthers('/api/common/CompanyList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                }
            }                                  
        };
                
        function getPeriodType() {
            vm.periodTypeOptions = {
                optionLabel: "-- Select Type --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "PERIOD_TYPE_NAME_EN_UNIT",
                dataValueField: "HR_PERIOD_TYPE_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);                                       
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getDataByFullUrl('/api/common/GetPayPeriodType').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                }
            }
         
        };

        function getFiscalYear() {
            vm.fiscalYearOptions = {
                optionLabel: "-- Select FY --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "FY_NAME_EN",
                dataValueField: "RF_FISCAL_YEAR_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    vm.form.FY_START_DATE = dataItem.FY_START_DATE;

                    console.log(moment(vm.form.FY_START_DATE).get('year'));

                    if (dataItem.RF_FISCAL_YEAR_ID < 0) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'NewFYEntryModal.html',
                            controller: 'FYModalController',
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal'//,
                            //resolve: {
                            //    ColourList: function (KnittingDataService) {
                            //        return KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll');
                            //    }
                            //}
                        });

                        modalInstance.result.then(function (result) {
                            console.log(result);

                            if (result.RF_FISCAL_YEAR_ID && result.RF_FISCAL_YEAR_ID > 0) {
                                vm.fiscalYearDataSource.read().then(function () {
                                    vm.form['RF_FISCAL_YEAR_ID'] = result.RF_FISCAL_YEAR_ID;
                                    vm.form['FY_NAME_EN'] = result.FY_NAME_EN;
                                });
                            }
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });
                    }
                }                
            }

            return vm.fiscalYearDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/common/GetPayFiscalYear?pIS_CLOSED=N').then(function (res) {
                            e.success([{ 'FY_NAME_EN': '---New FY---', RF_FISCAL_YEAR_ID: -1 }].concat(res));
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        function getMonthList() {
            vm.monthListOptions = {
                optionLabel: "-- Select Month --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "MONTH_NAME_EN",
                dataValueField: "RF_CAL_MONTH_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);

                    if (vm.form.HR_PERIOD_TYPE_ID == 3) {
                        //var lastDay = moment().dayOfMonth(); //year(year).month(month).date(day)
                        console.log(moment([moment(vm.form.FY_START_DATE).get('year'), 0, 31]).month(dataItem.MONTH_VALUE - 1).format("YYYY-MM-DD"));

                        vm.form.START_DATE = moment([moment(vm.form.FY_START_DATE).get('year'), 0, 1]).month(dataItem.MONTH_VALUE - 1).format("YYYY-MM-DD");
                        vm.form.END_DATE = moment([moment(vm.form.FY_START_DATE).get('year'), 0, 31]).month(dataItem.MONTH_VALUE - 1).format("YYYY-MM-DD");
                        vm.form.PAY_DATE = moment([moment(vm.form.FY_START_DATE).get('year'), 0, 31]).month(dataItem.MONTH_VALUE - 1).format("YYYY-MM-DD");
                    }
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getDataByFullUrl('/api/common/GetMonthList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                }
            }

        };
        

        vm.compPayPeriodGridOption = {
            height: 300,
            sortable: true,
            scrollable: {
                virtual: true,
                scrollable:true
            },
            pageable: false,
            editable: false,
            selectable: "row",
            navigatable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            columns: [
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "15%" },
                { field: "FY_NAME_EN", title: "Fiscal Year", type: "string", width: "10%" },
                { field: "MONTH_NAME_EN", title: "Month", type: "string", width: "10%" },
                { field: "PERIOD_TYPE_NAME_EN", title: "Type", type: "string", width: "10%" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "15%", filterable: false },
                { field: "PAY_DATE", title: "Pay Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "10%", filterable: false },
                { field: "IS_SHOW4_RPT", title: "Show for Rpt?", type: "string", width: "10%", filterable: false },
                { field: "IS_CLOSED", title: "Close?", type: "string", width: "10%", filterable: false },
                
                {
                    title: "Action",
                    template: function () {                        
                        return "<button class='btn btn-xs blue' ng-click='vm.editData(dataItem)' ><i class='fa fa-edit'> Edit</i></button>";
                    },
                    width: "10%"
                }
            ]
        };

        vm.compPayPeriodGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 10,            
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/hr/CompPayPeriod/GetCompPayPeriodList';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += HrService.kFilterStr2QueryParam(params.filter);

                    return HrService.GetDataByUrl(url).then(function (res) {
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });                    
                }
            },
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "ACC_PAY_PERIOD_ID",
                    fields: {
                        COMP_NAME_EN: { type: "string", editable: false },
                        PAY_DATE: { type: "date", editable: false }
                    }
                }
            }
        });

               

        //vm.onChangePeriodType = function () {

        //}

        vm.reset = function () {
            //vm.form = { HR_INCR_MEMO_ID: 0, IS_BY_ADMIN: 'Y', IS_FINALIZED: 'N', EFFECTIVE_DT: vm.today, MEMO_DT: vm.today };
            vm.form.HR_INCR_MEMO_ID = 0;
            vm.form.AUTH_NO = null;
            vm.form.MEMO_NO = null;
            vm.form.MEMO_DT = null;
            vm.form.REMARKS = null;
        }

        vm.editData = function (data) {
            vm.form = angular.copy(data);
            $state.go('CompPayPeriod', { pACC_PAY_PERIOD_ID: data.ACC_PAY_PERIOD_ID }, { notify: false });
        }

        vm.save = function (pIS_CLOSED) {
            var submitData = angular.copy(vm.form);

            submitData.START_DATE = $filter('date')(submitData.START_DATE, vm.dtFormat);
            submitData.END_DATE = $filter('date')(submitData.END_DATE, vm.dtFormat);
            submitData.PAY_DATE = $filter('date')(submitData.PAY_DATE, vm.dtFormat);
            
            var vMsg = 'Do you want to save?';
            if (pIS_CLOSED == 'Y') {
                submitData['IS_CLOSED'] = pIS_CLOSED;
                vMsg = "After closing, the period will lock & you can't any transaction into the period.</br></br> <b>Do you want to save and close the period?</b>";
            }


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                return HrService.saveDataByUrl(submitData, '/api/common/SaveCompPayPeriod').then(function (res) {
                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //vm.reset();

                            if (pIS_CLOSED == 'Y') {
                                vm.form.IS_CLOSED = pIS_CLOSED;
                            }
                            vm.form.ACC_PAY_PERIOD_ID = res.data.PACC_PAY_PERIOD_ID_RTN;
                            vm.compPayPeriodGridDataSource.read();

                            $state.go('CompPayPeriod', { pACC_PAY_PERIOD_ID: res.data.PACC_PAY_PERIOD_ID_RTN }, { notify: false });

                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });
            });


        };


        vm.memoFinalize = function () {
            return HrService.saveDataByUrl(vm.form, '/api/Hr/HrIncriment/MemoFinalize').then(function (res) {
                if (res.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    console.log(res);
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        //vm.reset();
                        vm.incrMemoGridDataSource.read();                        
                    };

                    config.appToastMsg(res.data.PMSG);

                }
            });
        }
            
        
    }
    
})();
//============== End for HrIncrMemoController
















//=============== Start for FYModalController =================
(function () {
    'use strict';
    angular.module('multitex.hr').controller('FYModalController', ['$q', 'config', 'Dialog', 'HrService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modalInstance', FYModalController]);
    function FYModalController($q, config, Dialog, HrService, $stateParams, $state, $scope, $filter, commonDataService, $modalInstance) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        //$scope.RF_FISCAL_YEAR_ID = 0;
        //$scope.FY_NAME_EN = '';
        //$scope.FY_START_DATE = '';
        //$scope.FY_END_DATE = '';
        //$scope.IS_CLOSED = 'N';
        vm.form = {
            RF_FISCAL_YEAR_ID: 0, FY_NAME_EN: '',
            FY_START_DATE: moment([moment(vm.today).get('year'), 0, 1]).month(0).format("YYYY-MM-DD"),
            FY_END_DATE: moment([moment(vm.today).get('year'), 0, 31]).month(11).format("YYYY-MM-DD"),
            CY_START_DATE: moment([moment(vm.today).get('year'), 0, 1]).month(0).format("YYYY-MM-DD"),
            CY_END_DATE: moment([moment(vm.today).get('year'), 0, 31]).month(11).format("YYYY-MM-DD")
        };

    //vm.form.END_DATE = moment([moment(vm.form.FY_START_DATE).get('year'), 0, 31]).month(dataItem.MONTH_VALUE - 1).format("YYYY-MM-DD");

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.FY_START_DATE_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.FY_START_DATE_Opened = true;
        }

        vm.FY_END_DATE_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.FY_END_DATE_Opened = true;
        }

        vm.CY_START_DATE_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.CY_START_DATE_Opened = true;
        }

        vm.CY_END_DATE_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.CY_END_DATE_Opened = true;
        }

        $scope.cancel = function () {
            //$modalInstance.dismiss('cancel');
            
            $modalInstance.close(vm.form);
        };

        vm.tranCancel = function () {
            vm.form = {
                RF_FISCAL_YEAR_ID: 0, FY_NAME_EN: '',
                FY_START_DATE: moment([moment(vm.today).get('year'), 0, 1]).month(0).format("YYYY-MM-DD"),
                FY_END_DATE: moment([moment(vm.today).get('year'), 0, 31]).month(11).format("YYYY-MM-DD"),
                CY_START_DATE: moment([moment(vm.today).get('year'), 0, 1]).month(0).format("YYYY-MM-DD"),
                CY_END_DATE: moment([moment(vm.today).get('year'), 0, 31]).month(11).format("YYYY-MM-DD")
            };
        }

        vm.editRow = function (dataItem) {
            var data = angular.copy(dataItem);

            vm.form = data;
        }

        vm.FYGridOption = {
            height: 200,
            sortable: true,
            scrollable: {
                virtual: true,
                scrollable: true
            },
            pageable: false,
            editable: false,
            selectable: "row",
            navigatable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            columns: [
                { field: "FY_NAME_EN", title: "Fiscal Year", type: "string", width: "20%" },
                { field: "FY_START_DATE", title: "FY Start", type: "date", format: "{0:dd-MMM-yyyy}", width: "15%" },
                { field: "FY_END_DATE", title: "FY End", type: "date", format: "{0:dd-MMM-yyyy}", width: "15%" },
                { field: "CY_START_DATE", title: "CY Start", type: "date", format: "{0:dd-MMM-yyyy}", width: "15%", filterable: false },
                { field: "CY_END_DATE", title: "CY End", type: "date", format: "{0:dd-MMM-yyyy}", width: "15%", filterable: false },
                { field: "IS_CLOSED", title: "Close?", type: "string", width: "10%", filterable: false },
                {
                    title: "Action",
                    template: function () {
                        return "<button class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' ><i class='fa fa-edit'> Edit</i></button>";
                    },
                    width: "10%"
                }
            ]
        };

        vm.FYGridDataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverSorting: true,
            //serverFiltering: true,
            //pageSize: 10,            
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/common/GetPayFiscalYear';
                    //url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    //url += HrService.kFilterStr2QueryParam(params.filter);

                    return HrService.GetDataByUrl(url).then(function (res) {
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                //data: "data",
                //total: "total",
                model: {
                    id: "ACC_PAY_PERIOD_ID",
                    fields: {
                        COMP_NAME_EN: { type: "string", editable: false },
                        FY_START_DATE: { type: "date", editable: false },
                        FY_END_DATE: { type: "date", editable: false },
                        CY_START_DATE: { type: "date", editable: false },
                        CY_END_DATE: { type: "date", editable: false }
                    }
                }
            }
        });


        vm.save = function (token, valid, pIS_CLOSED) {

            //if (!valid) return;

            var submitData = angular.copy(vm.form);

            submitData['FY_VALUE_1'] = moment(submitData.FY_START_DATE).get('year');
            submitData['FY_VALUE_2'] = moment(submitData.FY_END_DATE).get('year');
            submitData['IS_CLOSED'] = pIS_CLOSED;

            submitData.FY_START_DATE = $filter('date')(submitData.FY_START_DATE, vm.dtFormat);
            submitData.FY_END_DATE = $filter('date')(submitData.FY_END_DATE, vm.dtFormat);
            submitData.CY_START_DATE = $filter('date')(submitData.CY_START_DATE, vm.dtFormat);
            submitData.CY_END_DATE = $filter('date')(submitData.CY_END_DATE, vm.dtFormat);
            
            var vMsg = 'Do you want to save?';
            if (pIS_CLOSED == 'Y') {
                submitData['IS_CLOSED'] = pIS_CLOSED;
                vMsg = "After closing, the fiscal year will lock & you can't any transaction into the fiscal year.</br></br> <b>Do you want to save and close the fiscal year?</b>";
            }

            
            

            //data.fabReqDtl = vm.obGrid;
            //vm.copyOrder = data;
            console.log(submitData);

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {
                return HrService.saveDataByUrl(submitData, '/api/common/SaveFiscalYear').then(function (res) {
                    console.log(res);

                    vm.errors = undefined;
                    if (res.success === false) {
                        vm.errors = [];
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.form.RF_FISCAL_YEAR_ID = parseInt(res.data.PRF_FISCAL_YEAR_ID_RTN);
                            //$modalInstance.close(res.data);
                            //$scope.cancel();
                            vm.FYGridDataSource.read();
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
//=============== End for SFabBkReasonModalController =================



