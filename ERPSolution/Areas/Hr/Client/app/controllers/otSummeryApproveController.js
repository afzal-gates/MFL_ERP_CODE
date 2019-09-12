//============== Start for OtSummeryApproveController ===============
(function () {
    'use strict';
    angular.module('multitex.hr').controller('OtSummeryApproveController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', '$filter', '$modal', 'Dialog', '$window', OtSummeryApproveController]);

    function OtSummeryApproveController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams, $filter, $modal, Dialog, $window) {
                        
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'HR_OT_SUM_ID';
        
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.today = new Date();
        vm.periodType = 3;
        vm.form = { HR_OT_SUM_ID: 0 };

        
        
        activate();

        function activate() {
            var promise = [getCompanyList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        $scope.emoloyeeAuto = function (val) {
            //alert('a');
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectItem = function (item) {
            //console.log(item);            
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            //vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
            //vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;           
        }

        //======== Start Set Focus ===========
        $("#frmEMPLOYEE_CODE").keypress(function (e) {
            if (e.which == 13) {
                $("#frmOT_QTY").focus();
            };
        });

        $("#frmOT_QTY").keypress(function (e) {
            if (e.which == 13) {
                $("#saveBtn").focus();
            };
        });       
        //======== End Set Focus ===========
        

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

                    $stateParams.pHR_COMPANY_ID = dataItem.HR_COMPANY_ID;

                    return vm.compPayPeriodDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                return HrService.getDataByFullUrl("/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + dataItem.HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodType + "&pIS_CLOSED=N").then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    });
                },
                dataBound: function (e) {
                    if ($stateParams.pHR_COMPANY_ID != null && $stateParams.pHR_COMPANY_ID > 0) {
                        vm.form.HR_COMPANY_ID = $stateParams.pHR_COMPANY_ID;

                        return vm.compPayPeriodDataSource = new kendo.data.DataSource({
                            transport: {
                                read: function (e) {
                                    return HrService.getDataByFullUrl("/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + $stateParams.pHR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodType + "&pIS_CLOSED=N").then(function (res) {
                                        e.success(res);
                                    });
                                }
                            }
                        });
                    }
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
            
            vm.compPayPeriodOptions = {
                optionLabel: "-- Select Period --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "MONTH_YEAR_NAME",
                dataValueField: "ACC_PAY_PERIOD_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    
                    $stateParams.pACC_PAY_PERIOD_ID = (dataItem.ACC_PAY_PERIOD_ID || 0);
                    vm.otSummeryGridDataSource.read();                    
                },
                dataBound: function (e) {
                    if ($stateParams.pACC_PAY_PERIOD_ID != null && $stateParams.pACC_PAY_PERIOD_ID > 0) {
                        vm.form.ACC_PAY_PERIOD_ID = $stateParams.pACC_PAY_PERIOD_ID;                        
                    }
                },
            }

            //vm.compPayPeriodDataSource = new kendo.data.DataSource({                
            //    transport: {
            //        read: function (e) {                       
            //            var url = '/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=' + dataItem.HR_COMPANY_ID + '&pHR_PERIOD_TYPE_ID=' + vm.periodType + '&pIS_CLOSED=N';
                       
            //            return HrService.getDataByFullUrl(url).then(function (res) {
            //                console.log(res);
            //                e.success(res.data);
            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    }
            //});



        };
                
                      

        vm.otSummeryGridOption = {
            height: 250,
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
                { field: "EMPLOYEE_CODE", title: "Code", type: "string", width: "10%" },
                { field: "EMP_FULL_NAME_EN", title: "Name", type: "string", width: "15%", footerTemplate: "Total Record: #=count#" },
                { field: "DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "15%" },
                { field: "OT_QTY", title: "OT Qty", type: "string", width: "10%", footerTemplate: "#=sum#" },
                {
                    title: "Action",
                    template: function () {                        
                        return "<button class='btn btn-xs blue' ng-click='vm.editData(dataItem)' ><i class='fa fa-edit'> Edit</i></button> <button class='btn btn-xs red' ng-click='vm.removeData(dataItem)' ><i class='fa fa-remove'> Remove</i></button>";
                    },
                    width: "10%"
                }
            ]
        };

        vm.otSummeryGridDataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverSorting: true,
            //serverFiltering: true,
            //pageSize: 10,            
            transport: {
                read: function (e) {
                    //var webapi = new kendo.data.transports.webapi({});
                    //var params = webapi.parameterMap(e.data);
                    var url = '/api/hr/OtSummeryApprove/GetOtSummeryList?pACC_PAY_PERIOD_ID=' + ($stateParams.pACC_PAY_PERIOD_ID || 0);
                    //url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    //url += HrService.kFilterStr2QueryParam(params.filter);

                    console.log(url);

                    return HrService.GetDataByUrl(url).then(function (res) {
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });                    
                }
            },
            aggregate: [
                { field: "EMP_FULL_NAME_EN", aggregate: "count" },
                { field: "OT_QTY", aggregate: "sum" }
            ],
            schema: {                
                model: {
                    id: "HR_OT_SUM_ID",
                    fields: {                        
                        EMPLOYEE_CODE: { type: "string", editable: false },
                        EMP_FULL_NAME_EN: { type: "string", editable: false },
                        OT_QTY: { type: "number", editable: false },
                    }
                }
            }
        });

               

        //vm.onChangePeriodType = function () {

        //}

        vm.reset = function () {
            //vm.form = { HR_INCR_MEMO_ID: 0, IS_BY_ADMIN: 'Y', IS_FINALIZED: 'N', EFFECTIVE_DT: vm.today, MEMO_DT: vm.today };
            vm.form.HR_OT_SUM_ID = 0;
            vm.form.EMPLOYEE_CODE = null;
            vm.form.EMP_FULL_NAME_EN = null;
            vm.form.OT_QTY = null;            
        }

        vm.editData = function (data) {
            var data = angular.copy(data);

            vm.form.HR_OT_SUM_ID = data.HR_OT_SUM_ID;
            vm.form.HR_EMPLOYEE_ID = data.HR_EMPLOYEE_ID;
            vm.form.EMPLOYEE_CODE = data.EMPLOYEE_CODE;
            vm.form.EMP_FULL_NAME_EN = data.EMP_FULL_NAME_EN + ', ' + data.DESIGNATION_NAME_EN + ', ' + data.DEPARTMENT_NAME_EN;
            vm.form.OT_QTY = data.OT_QTY;

            //$state.go('CompPayPeriod', { pACC_PAY_PERIOD_ID: data.ACC_PAY_PERIOD_ID }, { notify: false });
        }

        vm.removeData = function (dataItem) {
            var submitData = angular.copy(dataItem);
            //submitData.PAY_DATE = $filter('date')(submitData.PAY_DATE, vm.dtFormat);

            var vMsg = 'Do you want to remove?';
            //if (pIS_CLOSED == 'Y') {
            //    submitData['IS_CLOSED'] = pIS_CLOSED;
            //    vMsg = "After closing, the period will lock & you can't any transaction into the period.</br></br> <b>Do you want to save and close the period?</b>";
            //}


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                return HrService.saveDataByUrl(submitData, '/api/hr/OtSummeryApprove/Remove').then(function (res) {
                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //vm.reset();

                            //if (pIS_CLOSED == 'Y') {
                            //    vm.form.IS_CLOSED = pIS_CLOSED;
                            //}
                            //vm.form.ACC_PAY_PERIOD_ID = res.data.PACC_PAY_PERIOD_ID_RTN;

                            vm.otSummeryGridDataSource.read();
                            vm.reset();
                            $('#frmEMPLOYEE_CODE').focus();

                            $state.go('OtSummeryApprove', { pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pACC_PAY_PERIOD_ID: vm.form.ACC_PAY_PERIOD_ID }, { notify: false });

                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });
            });


        };


        vm.cancel = function () {
            vm.reset();
        }

        vm.save = function () {
            var submitData = angular.copy(vm.form);
            //submitData.PAY_DATE = $filter('date')(submitData.PAY_DATE, vm.dtFormat);
            
            //var vMsg = 'Do you want to save?';
            //if (pIS_CLOSED == 'Y') {
            //    submitData['IS_CLOSED'] = pIS_CLOSED;
            //    vMsg = "After closing, the period will lock & you can't any transaction into the period.</br></br> <b>Do you want to save and close the period?</b>";
            //}


            //Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

            return HrService.saveDataByUrl(submitData, '/api/hr/OtSummeryApprove/Save').then(function (res) {
                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //vm.reset();

                            //if (pIS_CLOSED == 'Y') {
                            //    vm.form.IS_CLOSED = pIS_CLOSED;
                            //}
                            //vm.form.ACC_PAY_PERIOD_ID = res.data.PACC_PAY_PERIOD_ID_RTN;
                            
                            vm.otSummeryGridDataSource.read();
                            vm.reset();
                            $('#frmEMPLOYEE_CODE').focus();

                            $state.go('OtSummeryApprove', { pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pACC_PAY_PERIOD_ID: vm.form.ACC_PAY_PERIOD_ID }, { notify: false });

                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });
            //});


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
//============== End for OtSummeryApproveController ================

