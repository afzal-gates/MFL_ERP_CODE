(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrReportController', ['logger', 'config', 'HrService', 'hrRptData', '$q', '$scope', '$http', '$sessionStorage', '$filter', 'access_token', HrReportController]);

    function HrReportController(logger, config, HrService, hrRptData, $q, $scope, $http, $sessionStorage, $filter, access_token) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.showSplash = true;                        

        vm.insert = true;        
        vm.today = new Date();
        vm.rptList = angular.copy(hrRptData);

        vm.monthsList = [{ RF_CAL_MONTH_ID: 1, MONTH_NAME: 'January' }, { RF_CAL_MONTH_ID: 2, MONTH_NAME: 'February' }, { RF_CAL_MONTH_ID: 3, MONTH_NAME: 'March' },
            { RF_CAL_MONTH_ID: 4, MONTH_NAME: 'April' }, { RF_CAL_MONTH_ID: 5, MONTH_NAME: 'May' }, { RF_CAL_MONTH_ID: 6, MONTH_NAME: 'June' },
            { RF_CAL_MONTH_ID: 7, MONTH_NAME: 'July' }, { RF_CAL_MONTH_ID: 8, MONTH_NAME: 'August' }, { RF_CAL_MONTH_ID: 9, MONTH_NAME: 'September' },
            { RF_CAL_MONTH_ID: 10, MONTH_NAME: 'October' }, { RF_CAL_MONTH_ID: 11, MONTH_NAME: 'November' }, { RF_CAL_MONTH_ID: 12, MONTH_NAME: 'December' }
        ];

        vm.salPartList = [{ SAL_PART_ID: 0, SAL_PART_NAME: '100%' }, { SAL_PART_ID: 1, SAL_PART_NAME: '70%' }, { SAL_PART_ID: 2, SAL_PART_NAME: '30%' }];

        
        vm.form = {};
        vm.form.REPORT_CODE = 'RPT-1000';
        vm.form.IS_EXCEL_FORMAT = 'N';
        vm.form.SALARY_PART_PCT = 100;

        //$scope.$watch('vm.form.REPORT_CODE', function (newVal, oldVal) {
        //    vm.form.pREPORT_CODE = vm.form.REPORT_CODE;
        //});
        
        vm.form.FROM_DATE = $filter('date')(vm.today, vm.dtFormat);
        vm.form.TO_DATE = $filter('date')(vm.today, vm.dtFormat);

        vm.periodTypeId = null;


        function activate() {
            var promise = [getGenderList(), getFiscalYearData(), getOtTypeList(), getCompanyList(), getOfficeList(), getManagementTypeList(), parentDepartmentList(), getSubdepartmentListData(), //getEmployeeDataList(),
                           floorList(), lineList(), shiftTeamList(), getBonusTypeListData(), taFlagList(), getReportTypeList(), getPayTypeList(), jobStatusList(),
                           getJobCardTypeList(), getBankList(), hasUserAccountList(), getDiscpActTypeList(), getDesigGrpList(), getIncrMemoList(), getMonths(), getSalPart(),
                           getEmpDutyNatureList(), getAssignOperatorList(), getPayElementListData(), getDesigGradeList()
            ];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                
                vm.showSplash = false;
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        //function getReportDataList() {
        //    return HrService.GetAllOthers('/api/common/getReportDataListByUser/' + vm.pRF_REPORT_GRP_ID).then(function (res) {
        //        vm.reportDataList = res;
        //    }, function (err) {
        //        console.log(err);
        //    });
        //};

        vm.readTemplate = function () {
            $http({
                method: 'get',
                url: "/Hr/HrReport/ReadTemplate"
            });
        };

        vm.fromDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.fromDateOpened = true;
        };

        vm.toDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.toDateOpened = true;
        };

        function getCompanyList() {
            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                //index:1,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getCompanyData().then(function (res) {
                                e.success(res);

                                //////////////////////============//////////////////////////////
                                $('#ACC_PAY_PERIOD_ID_MONTH').kendoDropDownList({
                                    optionLabel: "-- Select Period --",
                                    filter: "startswith",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'get',
                                                    url: "/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + res[0].HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=3&pIS_SHOW4_RPT=Y",
                                                    headers: { 'Authorization': 'Bearer ' + access_token }
                                                }).
                                                success(function (data, status, headers, config) {
                                                    e.success(data);
                                                    if (data.length > 0) {
                                                        vm.form.ACC_PAY_PERIOD_ID_MONTH = data[0].ACC_PAY_PERIOD_ID;
                                                        vm.form.FROM_DATE = data[0].START_DATE;
                                                        vm.form.TO_DATE = data[0].END_DATE;
                                                    };
                                                }).
                                                error(function (data, status, headers, config) {
                                                    alert('something went wrong')
                                                    console.log(status);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "MONTH_YEAR_NAME",
                                    dataValueField: "ACC_PAY_PERIOD_ID",
                                    //template: '<span>#: MONTH_YEAR_NAME #</span>',
                                    select: function (e) {
                                        var dataItem = this.dataItem(e.item);
                                        vm.form.ACC_PAY_PERIOD_ID = dataItem.ACC_PAY_PERIOD_ID;
                                        vm.form.ACC_PAY_PERIOD_ID_MONTH = dataItem.ACC_PAY_PERIOD_ID;
                                        var dt = moment(dataItem.START_DATE)._d;
                                        vm.form['FROM_DATE'] = dataItem.START_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                                        dt = moment(dataItem.END_DATE)._d;
                                        vm.form['TO_DATE'] = dataItem.END_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                                    }
                                });

                                ////////////////////////////////////////////////////
                                $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                                    optionLabel: "-- Select Period --",
                                    filter: "startswith",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'get',
                                                    url: "/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + res[0].HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodTypeId + "&pIS_SHOW4_RPT=Y",
                                                    headers: { 'Authorization': 'Bearer ' + access_token }
                                                }).
                                                success(function (data, status, headers, config) {
                                                    e.success(data);
                                                    if (data.length > 0) {
                                                        vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                                        vm.form.FROM_DATE = data[0].START_DATE;
                                                        vm.form.TO_DATE = data[0].END_DATE;
                                                    };
                                                }).
                                                error(function (data, status, headers, config) {
                                                    alert('something went wrong')
                                                    console.log(status);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "REMARKS",
                                    dataValueField: "ACC_PAY_PERIOD_ID",
                                    //template: '<span>#: MONTH_YEAR_NAME #</span>',
                                    select: function (e) {
                                        var dataItem = this.dataItem(e.item);
                                        vm.form.ACC_PAY_PERIOD_ID = dataItem.ACC_PAY_PERIOD_ID;
                                        var dt = moment(dataItem.START_DATE)._d;
                                        vm.form['FROM_DATE'] = dataItem.START_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                                        dt = moment(dataItem.END_DATE)._d;
                                        vm.form['TO_DATE'] = dataItem.END_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                                    }
                                });
                                ///////////////////////////////////////////////////
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    //alert('a');
                    var dataItem = this.dataItem(e.item);
                    var vHR_COMPANY_ID = dataItem.HR_COMPANY_ID; //this.value();
                    vm.form.ACC_PAY_PERIOD_ID = null;

                    ////////////////////////////////////////////////////////
                    $('#ACC_PAY_PERIOD_ID_MONTH').kendoDropDownList({
                        optionLabel: "-- Select Period --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    $http({
                                        method: 'get',
                                        url: "/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + vHR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=3&pIS_SHOW4_RPT=Y",
                                        headers: { 'Authorization': 'Bearer ' + access_token }
                                    }).
                                    success(function (data, status, headers, config) {
                                        e.success(data);
                                        if (data.length > 0) {
                                            vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                            vm.form.ACC_PAY_PERIOD_ID_MONTH = data[0].ACC_PAY_PERIOD_ID;
                                            vm.form.FROM_DATE = data[0].START_DATE;
                                            vm.form.TO_DATE = data[0].END_DATE;
                                        };

                                    }).
                                    error(function (data, status, headers, config) {
                                        alert('something went wrong')
                                        console.log(status);
                                    });
                                }
                            }
                        },
                        dataTextField: "MONTH_YEAR_NAME",
                        dataValueField: "ACC_PAY_PERIOD_ID",
                        //valueTemplate: '<span>#: MONTH_YEAR_NAME #</span>',
                        select: function (e) {
                            var dataItem = this.dataItem(e.item);

                            vm.form.ACC_PAY_PERIOD_ID = dataItem.ACC_PAY_PERIOD_ID;
                            vm.form.ACC_PAY_PERIOD_ID_MONTH = dataItem.ACC_PAY_PERIOD_ID;

                            //console.log(dataItem);

                            var dt = moment(dataItem.START_DATE)._d;
                            vm.form['FROM_DATE'] = dataItem.START_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                            dt = moment(dataItem.END_DATE)._d;
                            vm.form['TO_DATE'] = dataItem.END_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);

                            //vm.form.RF_FISCAL_YEAR_ID = dataItem.RF_FISCAL_YEAR_ID;
                            //vm.form.RF_CAL_MONTH_ID = dataItem.RF_CAL_MONTH_ID;
                        }
                    });

                    ////////////////////////////////////////////////////////
                    $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                        optionLabel: "-- Select Period --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    $http({
                                        method: 'get',
                                        url: "/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + vHR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodTypeId + "&pIS_SHOW4_RPT=Y",
                                        headers: { 'Authorization': 'Bearer ' + access_token }
                                    }).
                                    success(function (data, status, headers, config) {
                                        e.success(data);
                                        if (data.length > 0) {
                                            vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                            vm.form.FROM_DATE = data[0].START_DATE;
                                            vm.form.TO_DATE = data[0].END_DATE;
                                        };

                                    }).
                                    error(function (data, status, headers, config) {
                                        alert('something went wrong')
                                        console.log(status);
                                    });
                                }
                            }
                        },
                        dataTextField: "REMARKS",
                        dataValueField: "ACC_PAY_PERIOD_ID",
                        //valueTemplate: '<span>#: MONTH_YEAR_NAME #</span>',
                        select: function (e) {
                            var dataItem = this.dataItem(e.item);
                            vm.form.ACC_PAY_PERIOD_ID = dataItem.ACC_PAY_PERIOD_ID;
                            
                            var dt = moment(dataItem.START_DATE)._d;
                            vm.form['FROM_DATE'] = dataItem.START_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                            dt = moment(dataItem.END_DATE)._d;
                            vm.form['TO_DATE'] = dataItem.END_DATE == null ? '' : $filter('date')(dt, vm.dtFormat);
                        }
                    });
                    /////////////////////////////////////////////////////////////////////////
                }

            };

        };

        function getOfficeList() {
            return vm.officeListData = {
                optionLabel: "-- Select Office --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrOffice/OfficeListData"
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                dataTextField: "OFFICE_NAME_EN",
                dataValueField: "HR_OFFICE_ID"
            };
        };

        function getManagementTypeList() {
            return vm.managementTypeList = {
                optionLabel: "-- Select Employee Type --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getEmployeeTypeData().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "MNG_TYPE_NAME_EN",
                dataValueField: "HR_MANAGEMENT_TYPE_ID"
            };
        };

        function getPayTypeList() {
            var data=[{PAY_TYPE_ID:1, PAY_TYPE_NAME:'Cash'}, {PAY_TYPE_ID:2, PAY_TYPE_NAME:'Bank'}]
            return vm.payTypeListData = {
                optionLabel: "-- Select Pay Type --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                filter: "startswith",
                autoBind: true,
                dataSource: data,
                dataTextField: "PAY_TYPE_NAME",
                dataValueField: "PAY_TYPE_ID"
            }
        }
       
        function getGenderList() {
            return vm.genderListData = {
                optionLabel: "-- Select --",
                //filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/LookupListData",  //+ "&pType=" + showType
                                params: {
                                    pLookupTableId: 16
                                }
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                //select: function (e) {
                //    var dataItem = this.dataItem(e.item);
                //    vm.form.FLOOR_NAME = dataItem.LK_DATA_NAME_EN;
                //},
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getBankList() {
            var data = [{ RF_BANK_ID: 1, BANK_NAME_EN: 'Dutch Bangla Banl Ltd.' },
                { RF_BANK_ID: 2, BANK_NAME_EN: 'BRAC Bank Ltd.' },
                { RF_BANK_ID: 3, BANK_NAME_EN: 'Janata Bank Ltd.' },
                { RF_BANK_ID: 4, BANK_NAME_EN: 'Dhaka Bank Ltd.' },
            ];

            return vm.bankListData = {                
                optionLabel: "-- Select Bank --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                filter: "startswith",
                autoBind: true,
                dataSource: data,
                dataTextField: "BANK_NAME_EN",
                dataValueField: "RF_BANK_ID"
            };
        };

        function getOtTypeList() {
            return vm.otTypeListData = {
                optionLabel: '-- Select OT Type --',
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getOtTypeData().then(function (res) {
                                e.success(res);
                                //console.log(res);
                            });
                        },
                    }
                },
                dataTextField: "OT_TYPE_NAME_EN",
                dataValueField: "HR_OT_TYPE_ID"
            };
        };

        function getDesigGrpList() {
            return vm.desigGrpList = {
                optionLabel: "-- Select Desig Group --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.GetAllOthers('/Hr/HrSalaryTran/DesignationGrpListData').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DESIG_GRP_NAME_EN",
                dataValueField: "HR_DESIGNATION_GRP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.DSG_GRP_ORDER = item.DSG_GRP_ORDER;
                    vm.form.ASSIGN_OPERATOR_ID = 1;
                }
            };           
        };

        function parentDepartmentList() {
            vm.parentDepartmentListData = {
                optionLabel: "-- Select Department --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/ParentDeptListData"  //+ "&pType=" + showType
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }//,
                    //group: { field: "PARENT_NAME" }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.HR_DEPARTMENT_ID; //this.value();                    

                    if (vParentID != null) {

                        //$http({
                        //    url: '/Hr/HrEmployee/DeptListData',
                        //    method: 'get',
                        //    params: { pPARENT_ID: vParentID }
                        //})
                        // .then(function (result) {
                        //     vm.departmentListData = result.data;
                        //     //vm.form.HR_DEPARTMENT_ID = vm.departmentListData[0].HR_DEPARTMENT_ID;
                        //     return;
                        // }).catch(function (message) {
                        //     exception.catcher('XHR loading Failded')(message);
                        // });
                        //alert('select ' + vParentID);

                        HrService.getSubDepartmentData(vParentID).then(function (res) {
                            $("#HR_DEPARTMENT_ID").kendoDropDownList({
                                optionLabel: "-- Select Section --",
                                dataTextField: "DEPARTMENT_NAME_EN",
                                dataValueField: "HR_DEPARTMENT_ID",
                                dataSource: res,
                                filter: "startswith"
                            });

                        });

                        //HrService.getDesigData(vParentID).then(function (res) {
                        //    $("#dropdownlist").kendoDropDownList({
                        //        optionLabel: "Select",
                        //        dataTextField: "DESIGNATION_NAME_EN",
                        //        dataValueField: "HR_DESIGNATION_ID",
                        //        dataSource: res,
                        //        filter: "startswith",
                        //        select: function (e) {
                        //            var dataItem = this.dataItem(e.item);
                        //            vm.form.HR_GRADE_ID = dataItem.HR_GRADE_ID;
                        //        }
                        //    });

                        //});

                    }

                }
            };
        };

        function getSubdepartmentListData() {            
            return vm.subdepartmentListData = {
                optionLabel: "-- Select Section --",                
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getSubDepartmentData().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID"
            };
        };
        
        function getMonths() {
            return vm.monthsListData = {
                optionLabel: "-- Select Month --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(vm.monthsList);
                        }
                    }
                },
                dataTextField: "MONTH_NAME",
                dataValueField: "RF_CAL_MONTH_ID"
            };
        };

        function getSalPart() {
            return vm.salPartOprion = {
                optionLabel: "-- Select Part --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(vm.salPartList);
                        }
                    }
                },
                dataTextField: "SAL_PART_NAME",
                dataValueField: "SAL_PART_ID"
            };
        };

        function getAssignOperatorList() {
            vm.assignOperatorList = [{ ASSIGN_OPERATOR_ID: 1, ASSIGN_OPERATOR: '=' }, { ASSIGN_OPERATOR_ID: 2, ASSIGN_OPERATOR: '>=' }, { ASSIGN_OPERATOR_ID: 3, ASSIGN_OPERATOR: '<=' }];

            return vm.assignOperatorListData = {
                optionLabel: "-- Select --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                //filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(vm.assignOperatorList);
                        }
                    }
                },
                dataTextField: "ASSIGN_OPERATOR",
                dataValueField: "ASSIGN_OPERATOR_ID"
            };
        };

        function getFiscalYearData() {
            return vm.FiscalYearData = {
                optionLabel: "-- Select Fiscal Year --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'get',
                                url: "/hr/hrleavetrans/FiscalYearDataAll"
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.form['FY_NAME_EN'] = this.dataItem(e.item).FY_NAME_EN;
                },
                dataTextField: "FY_NAME_EN",
                dataValueField: "RF_FISCAL_YEAR_ID"
            };
        };

        function floorList() {
            return vm.floorListData = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/LookupListData",  //+ "&pType=" + showType
                                params: {
                                    pLookupTableId: 18
                                }
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.form.FLOOR_NAME = dataItem.LK_DATA_NAME_EN;
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function lineList() {
            return vm.lineListData = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/LineListData"  //+ "&pType=" + showType                                
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                dataTextField: "Text",
                dataValueField: "Value"
            };
        };
        
        //function lineList() {
        //    vm.lineListData = [{ LINE_NO: 0 }];

        //    for (var i = 0; i < 50; i++) {
        //        vm.lineListData[i] = { LINE_NO: i + 1 };
        //    }
        //};

        function getDesigGradeList() {
            return vm.gradeListData = {
                optionLabel: "Select",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/GradeListData"  //+ "&pType=" + showType
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                dataTextField: "GRADE_NO_EN",
                dataValueField: "HR_GRADE_ID"
            };
        };
                
        function getPayElementListData() {
            vm.payElementData = {
                optionLabel: "-- Select Pay Element --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return $http({
                                method: 'get',
                                url: '/Hr/HrEmployee/PayElementListData?pLK_PAY_ELM_TYPE_ID=160'
                            }).then(function (result) {                                
                                e.success(result.data)
                            }).catch(function (message) {
                                exception.catcher('XHR loading Failded')(message);
                            });
                        }
                    }
                },
                dataTextField: "PAY_ELEMENT_NAME_EN",
                dataValueField: "HR_PAY_ELEMENT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                }              
            };
        };

        function shiftTeamList() {
            return vm.shiftTeamListData = {
                optionLabel: "-- Select Team --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/ShiftTeamListData",  //+ "&pType=" + showType                                
                                params: { pHR_SHIFT_PLAN_ID: null }
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                dataTextField: "SHIFT_TEAM_NAME_EN",
                dataValueField: "HR_SHIFT_TEAM_ID"
            };
        };

        function getBonusTypeListData() {
            return vm.bonusTypeListData = {
                optionLabel: "-- Select Section --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getLookupListData(38).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.form.BONUS_TYPE_NAME = dataItem.LK_DATA_NAME_EN;                    
                }
            };
        };

        function taFlagList() {
            return vm.taFlagListData = {
                optionLabel: "-- Select Attendance Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: [{ TA_FLAG: 'P', TA_FLAG_NAME: 'Present' }, { TA_FLAG: 'L', TA_FLAG_NAME: 'Late' }, { TA_FLAG: 'A', TA_FLAG_NAME: 'Absent' }, { TA_FLAG: 'M', TA_FLAG_NAME: 'Miss Punch' }, { TA_FLAG: 'W', TA_FLAG_NAME: 'Holiday Present' }],
                dataTextField: "TA_FLAG_NAME",
                dataValueField: "TA_FLAG"
            };
        };

        function getJobCardTypeList() {
            return vm.jobCardTypeListData = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: [{ HR_DAY_TYPE_ID: 1, DAY_TYPE_NAME_EN: 'Working Day' }, { HR_DAY_TYPE_ID: -1, DAY_TYPE_NAME_EN: 'All Holiday' }],
                dataTextField: "DAY_TYPE_NAME_EN",
                dataValueField: "HR_DAY_TYPE_ID"
            };
        };

        function getReportTypeList() {
            return vm.reportTypeListData = {
                optionLabel: "-- Select Report Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: [{ REPORT_TYPE_ID: 1, REPORT_TYPE_NAME: 'Orginal' }, { REPORT_TYPE_ID: 2, REPORT_TYPE_NAME: 'Compliance' }/*{ REPORT_TYPE_ID: 3, REPORT_TYPE_NAME: 'Compliance-1' }, { REPORT_TYPE_ID: 4, REPORT_TYPE_NAME: 'Compliance-2' }*/],
                dataTextField: "REPORT_TYPE_NAME",
                dataValueField: "REPORT_TYPE_ID"
            };
        };

        function jobStatusList() {
            vm.jobStatusDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/Hr/HrEmployee/LookupListData?pLookupTableId=11';

                        return HrService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

            return vm.jobStatusListData = {
                optionLabel: "-- Select Job Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: vm.jobStatusDataSource,
                /*
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/LookupListData",  //+ "&pType=" + showType                                
                                params: { pLookupTableId: 11 }
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                */
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };


            //return $http({
            //    url: '/Hr/HrEmployee/LookupListData',
            //    method: 'get',
            //    params: {
            //        pLookupTableId: 11,
            //    },
            //})
            //    .then(function (result) {
            //        vm.jobStatusListData = result.data;
            //        vm.form.LK_JOB_STATUS_ID = vm.jobStatusListData[0].LOOKUP_DATA_ID;
            //        return;
            //    }).catch(function (message) {
            //        exception.catcher('XHR loading Failded')(message);
            //    });
        };

        vm.onChangeMultiJobStatus = function () {
            
            vm.form.LK_JOB_STATUS_LST = "";

            console.log(vm.form.LK_JOB_STATUS_ID_LIST);

            if (vm.form.LK_JOB_STATUS_ID_LIST != null) {

                if (vm.form.LK_JOB_STATUS_ID_LIST != []) {
                    vm.form.LK_JOB_STATUS_LST = vm.form.LK_JOB_STATUS_ID_LIST.join(",");
                }
            }

        };

        function hasUserAccountList() {
            return vm.hasUserAccountListData = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: [{ HAS_USER_ACCOUNT: 'Y', HAS_USER_ACCOUNT_NAME: 'Without ERP Account' }],
                dataTextField: "HAS_USER_ACCOUNT_NAME",
                dataValueField: "HAS_USER_ACCOUNT"
            };
        };

        function getDiscpActTypeList() {
            return HrService.GetAllOthers('/Hr/HrDisciplinAction/ActionTypeListData').then(function (res) {
                vm.discpActTypeList = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getIncrMemoList() {
            vm.incrMemoListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "AUTH_NO",
                dataValueField: "HR_INCR_MEMO_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);

                    vm.form.HR_COMPANY_ID = dataItem.HR_COMPANY_ID;
                    vm.form.HR_MANAGEMENT_TYPE_ID = dataItem.HR_MANAGEMENT_TYPE_ID;
                    vm.form.FROM_DATE = dataItem.START_DATE;
                    vm.form.TO_DATE = dataItem.END_DATE;
                }
            };

            return vm.incrMemoListDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/hr/HrIncriment/GetIncrMemoList?pageNumber=' + 1 + '&pageSize=' + 10;// + '&pIS_FINALIZED=Y';
                        url += HrService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return HrService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

            return HrService.GetAllOthers('/Hr/HrDisciplinAction/ActionTypeListData').then(function (res) {
                vm.discpActTypeList = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getEmpDutyNatureList() {
            return vm.empDutyNatureListData = {
                optionLabel: "-- Select --",
                //filter: "startswith",
                autoBind: true,
                dataSource: [{ IS_SHIFT: 'N', IS_SHIFT_NAME: 'No' }, { IS_SHIFT: 'Y', IS_SHIFT_NAME: 'Yes' }],
                dataTextField: "IS_SHIFT_NAME",
                dataValueField: "IS_SHIFT"
            };
        };

        $scope.emoloyeeAuto = function (val) {
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val,
                    pLK_JOB_STATUS_ID:null
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectItem = function (item) {
            //console.log(item);
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;
            }

        });
               

        ///////// Item Auto Search
        $scope.itemAuto = function (val) {           
            return $http.get('/api/inv/Item/ItemAutoDataList/' + val, {                
            }).then(function (response) {
                return response.data;
            });
        };
        
        $scope.onSelectInvItem = function (item) {
            vm.form.ITEM_CODE = item.ITEM_CODE + '-' + item.ITEM_NAME_EN;
            vm.form.INV_ITEM_ID = item.INV_ITEM_ID;
            vm.form.ITEM_NAME_EN = item.ITEM_NAME_EN;
        };
        //////// End Item Auto Search

        vm.rptOnChange = function (itm,idx) {
            //alert(idx);           
            console.log(hrRptData[idx]);
            var rptList = [];
            rptList = angular.copy(hrRptData);
            vm.form.REPORT_CODE = rptList[idx].RPT_CODE;
        }

        $scope.$watch('vm.form.REPORT_CODE', function (newVal, oldVal) {
            vm.periodTypeId = null;
            //alert(newVal + ' ' + oldVal);
            //if (newVal != oldVal) {
            vm.isBank = false;
            vm.form.SALARY_PART_PCT = 100;

            vm.isRDLC = false;
            vm.isCompany = false;
            vm.isOffice = false;
            vm.isPayPeriod = false;
            vm.isPayPeriodMonth = false;
            vm.isPayElement = false;
            vm.isPayType = false;
            vm.isJobStatus = false;
            vm.isMultiJobStatus = false;
            vm.isBonusType = false;
            vm.isManagementType = false;
            vm.isDesigGrp = false;
            vm.isDesigGrade = false;
            vm.isIncrMemo = false;
            vm.isSection = false;
            vm.isOtType = false;
            vm.isTeam = false;
            vm.isFloor = false;
            vm.isGender = false;
            vm.isEmployee = false;
            
            vm.isItem = false;
            vm.isFormDate = false;
            vm.isToDate = false;
            vm.isOutTimeWt = false;
            vm.isExtraOtTime = false;
            vm.isTaFlag = false;
            vm.isEmpDutyNature = false;
            vm.isReportType = false;
            vm.isDayType = false;
            vm.isBank = false;
            vm.isGrossSalary = false;
            vm.isNumOfDay = false;
            vm.hasUserAccount = false;
            vm.isFiscalYear = false;
            vm.isMonth = false;
            vm.isDiscpActType = false;
            vm.isSalPart = false;

            vm.isItemCategory = false;
            vm.isCustomerType = false;
            vm.isSalesPosStore = false;
            vm.isSalesPoint = false;

                if (vm.form.REPORT_CODE == 'RPT-1000') {
                    vm.isCompany = true;
                    vm.isOffice = true;                    
                    vm.isJobStatus = true;                    
                    vm.isManagementType = true;                    
                    vm.isSection = true;                    
                    vm.isTeam = true;
                    vm.isFloor = true;                    
                    vm.isEmployee = true;                    
                    vm.isFormDate = true;
                    vm.isToDate = true;                    
                    vm.isReportType = true;
                    vm.isDayType = true;                    
                    vm.hasUserAccount = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1001') {
                    vm.isCompany = true;
                    vm.isOffice = true;                    
                    vm.isManagementType = true;                    
                    vm.isFloor = true;                    
                    vm.isFormDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1002') {
                    vm.isCompany = true;
                    vm.isOffice = true;                    
                    vm.isManagementType = true;                    
                    vm.isSection = true;                    
                    vm.isFloor = true;
                    vm.isGender = true;                    
                    vm.isFormDate = true;                    
                    vm.isOutTimeWt = true;
                    vm.isTaFlag = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1003') { //Daily Attendance Report (By Section)
                    vm.isCompany = true;
                    vm.isOffice = true;                    
                    vm.isManagementType = true;
                    vm.isDesigGrp = true;                    
                    vm.isSection = true;                    
                    vm.isFloor = true;                    
                    vm.isFormDate = true;
                    vm.isOutTimeWt = true;
                    vm.isTaFlag = true;
                    vm.isEmpDutyNature = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1004') {
                    vm.isCompany = true;
                    vm.isOffice = true;                    
                    vm.isSection = true;                    
                    vm.isTeam = true;                    
                    vm.isFormDate = true;                    
                    vm.isTaFlag = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1012') {
                    vm.isCompany = true;
                    vm.isOffice = true;                    
                    vm.isFloor = true;                    
                    vm.isFormDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1013') { // Monthly Attendance summery Report
                    vm.isCompany = true;
                    vm.isPayPeriodMonth = true;
                    vm.isOffice = true;                    
                    vm.isManagementType = true;                    
                    vm.isSection = true;                    
                    vm.isFloor = true;                    
                    //vm.isFormDate = true;
                    //vm.isToDate = true;                    
                    vm.isReportType = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1014') { // OT Summery
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;                    
                    vm.isManagementType = true;                    
                    vm.isSection = true;                    
                    vm.isFormDate = true;
                    vm.isToDate = true;                    
                    vm.isEmpDutyNature = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1015') { // Leave Balance Statement
                    vm.isCompany = true;                    
                    vm.isSection = true;                    
                    vm.isFloor = true;                    
                    vm.isFiscalYear = true;
                    vm.isJobStatus = true;
                    vm.isManagementType = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1017') { //Tiffin Bill
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;                    
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isTeam = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1018') { // Night Bill
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1019') { // OT Bill Report
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isOtType = true;
                    vm.isFloor = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1020') { // Salary Sheet
                    vm.isRDLC = true;
                    vm.form.SALARY_PART_PCT = 100;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isDesigGrp = true;
                    vm.isEmployee = true;
                    vm.isReportType = true;
                    vm.isMultiJobStatus = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1021') { // Salary Payslip
                    vm.form.SALARY_PART_PCT = 100;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isPayType = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isTeam = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isReportType = true;
                    vm.hasUserAccount = true;
                    vm.isMultiJobStatus = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1022') { // Daily Attendance & Allocation Summery
                    vm.isCompany = true;
                    vm.isOffice = true;                    
                    vm.isManagementType = true;                    
                    vm.isFloor = true;
                    vm.isFormDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1023') { // Partial Salary Sheet
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isDesigGrp = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1024') { // Bonus Sheet
                    vm.periodTypeId = 3;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriodMonth = true;
                    vm.isBonusType = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1025') { // Salary Top Sheet By Section
                    vm.form.SALARY_PART_PCT = 100;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isMultiJobStatus = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1027') { // Textile Daily Attendance Summery
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFormDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1028') { // Daily Estimate Manpower
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isFormDate = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1029') { // Night Bill Top Sheet
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1030') { // Bonus Pay Slip
                    vm.periodTypeId = 3;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriodMonth = true;
                    vm.isPayType = true;
                    vm.isBonusType = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1031') { // Bonus Bank Statment
                    vm.periodTypeId = 3;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriodMonth = true;
                    vm.isBonusType = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1032') { // Pay Slip Partial Salary Sheet
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isPayType = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1033') { // Salary Bank Statment
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isPayType = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isBank = true;
                    vm.isGrossSalary = true;
                    vm.isMultiJobStatus = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1034') { // Salary Cash Statment
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isPayType = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isGrossSalary = true;
                    vm.isMultiJobStatus = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1035') { // Daily Movement
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1037') { // Absent Summery
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;
                    vm.isNumOfDay = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1038') { // Employee List
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isDesigGrp = true;
                    vm.isDesigGrade = true;
                    vm.isJobStatus = true;
                    vm.isTeam = true;
                    vm.isFloor = true;
                    vm.isGender = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1039') { // Monthly Lunch Summery
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1040') { // Allocation Summery By Line Type
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isManagementType = true;
                    vm.isFloor = true;
                    vm.isFormDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1042') { // Job Card - Dual Employee
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isJobStatus = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isTeam = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;
                    vm.isReportType = true;
                    vm.isDayType = true;
                    vm.hasUserAccount = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1043') { // Incriment Proposal List
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1044') { // Other Bill Summery from Atten.
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1045') { // Employee Profile
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isJobStatus = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1046') { // EL Encashment Bill
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1047') { // EL Encashment Pay Slip
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isPayType = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1048') { // EL Encashment Bank Statement
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                    vm.isBank = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1049') { // Daily Operator/Helper Status
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isFormDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1050') { // Employee List By Disciplinary Action
                    vm.isCompany = true;                    
                    vm.isManagementType = true;                    
                    vm.isFormDate = true;
                    vm.isToDate = true;
                    vm.isDiscpActType = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1051') { // 70% Salary Sheet for Staff
                    vm.form.SALARY_PART_PCT = 70;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isDesigGrp = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1052') { // 70% Salary Pay Slip for Staff
                    vm.form.SALARY_PART_PCT = 70;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isTeam = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.hasUserAccount = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1053') { // 30% Salary Cash Statment for Staff
                    vm.form.SALARY_PART_PCT = 30;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isPayType = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isDesigGrp = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1054') { // 70% Salary Bank Statement for Staff
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isPayType = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isBank = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1055') { // Salary Loan/Advance Deduction Statemtnt
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isTeam = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.hasUserAccount = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1056') { // Increment Proposal List
                    vm.isOffice = true;
                    vm.isIncrMemo = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1057') { // Increment Letter
                    vm.isIncrMemo = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1058') { // Salary Top Sheet
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isMultiJobStatus = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1059') { // Salary Analysis
                    vm.isCompany = true;
                    vm.isFiscalYear = true;
                    vm.isMonth = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1060') { // Fairshop Daily Sales Report
                    vm.isCompany = true;
                    //vm.isOffice = true;
                    vm.isItemCategory = true;
                    vm.isCustomerType = true;
                    vm.isSalesPosStore = true;
                    vm.isSalesPoint = true;

                    vm.isEmployee = true;
                    vm.isItem = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1061') { // Salary Top Sheet (For Cash)
                    vm.isRDLC = true;
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isMultiJobStatus = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1062') { // Increment Top Sheet
                    vm.isIncrMemo = true;
                    vm.isSection = true;                                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1063') { // Salary Deduction Sheet
                    vm.form.SALARY_PART_PCT = 100;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1064') { // Increment Check Sheet
                    vm.isIncrMemo = true;
                    vm.isSection = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1066') { // Bonus Top Sheet
                    vm.periodTypeId = 3;

                    vm.isCompany = true;                    
                    vm.isPayPeriodMonth = true;
                    vm.isBonusType = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1067') { // Employee Tax Pay Statment                    
                    vm.isFormDate = true;
                    vm.isToDate = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1068') { // A/C Head wise Salary Deduction
                    vm.form.SALARY_PART_PCT = 100;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isPayElement = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isDesigGrp = true;
                    vm.isEmployee = true;
                    vm.isMultiJobStatus = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1069') { // Salary Top Sheet By Section (For Cash)                    
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isSalPart = true;
                    vm.isMultiJobStatus = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1070') { // Fairshop Sales Summery
                    vm.isSalesPosStore = true;
                    vm.isSalesPoint = true;
                    vm.isItemCategory = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1072') { // Terminal Wise Movement
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;
                }                
                else if (vm.form.REPORT_CODE == 'RPT-1073') { // Daily Out Punch wise Manpower
                    vm.isRDLC = true;

                    vm.isCompany = true;
                    vm.isFormDate = true;
                    vm.isFloor = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1074') { // Fairshop Advance Deduction Detail
                    vm.isRDLC = true;

                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    //vm.isSalesPosStore = true;
                    //vm.isSalesPoint = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1075') { // Extra OT Bill Report
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isExtraOtTime = true;
                    vm.isFloor = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1076') { // Item wise Sales Summery
                    vm.isRDLC = true;
                    vm.isCompany = true;
                    //vm.isOffice = true;
                    vm.isItemCategory = true;
                    vm.isCustomerType = true;
                    vm.isSalesPosStore = true;
                    vm.isSalesPoint = true;

                    vm.isEmployee = true;
                    vm.isItem = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1077') { // Increment Letter for New Gejet
                    vm.isIncrMemo = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1078') { // Floor Wise Movement
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1079') { //Employee Job Card by Virtual OT
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isJobStatus = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isTeam = true;
                    vm.isFloor = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;
                    vm.isExtraOtTime = true;
                    //vm.isReportType = true;
                    vm.isDayType = true;
                    vm.hasUserAccount = true;
                }
                else if (vm.form.REPORT_CODE == 'RPT-1080') { //Employee List by Joining Date for BKMEA
                    vm.isRDLC = true;
                    vm.isCompany = true;
                    vm.isOffice = true;                    
                    vm.isFormDate = true;
                    vm.isToDate = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1081') { // Employee List from Monthly Salary
                    vm.isRDLC = true;
                    
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isPayPeriod = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isFloor = true;
                    vm.isDesigGrp = true;
                    vm.isEmployee = true;                    
                }
                else if (vm.form.REPORT_CODE == 'RPT-1082') { // Employee List - General
                    vm.isCompany = true;
                    vm.isOffice = true;
                    vm.isManagementType = true;
                    vm.isSection = true;
                    vm.isDesigGrp = true;
                    vm.isDesigGrade = true;
                    vm.isJobStatus = true;
                    vm.isTeam = true;
                    vm.isFloor = true;
                    vm.isGender = true;
                    vm.isEmployee = true;
                    vm.isFormDate = true;
                    vm.isToDate = true;
                }
        });

        

        vm.preview = function () {
            if (vm.form.REPORT_CODE == 'RPT-1051' || vm.form.REPORT_CODE == 'RPT-1052' || vm.form.REPORT_CODE == 'RPT-1053' || vm.form.REPORT_CODE == 'RPT-1054') {
                if (vm.form.HR_MANAGEMENT_TYPE_ID == '' || vm.form.HR_MANAGEMENT_TYPE_ID == null) {
                    vm.form.HR_MANAGEMENT_TYPE_ID = 2;
                }
            };

            var url;
            if (vm.isRDLC == true) {
                url = '/Hr/HrReport/PreviewReportRDLC';
            }
            else {
                url = '/Hr/HrReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);
            var formOutTimeWt = angular.copy(vm.form.FROM_OUT_TIME_WT); 
            var toOutTimeWt = angular.copy(vm.form.TO_OUT_TIME_WT); 

            var params = angular.copy(vm.form);
            if (formOutTimeWt == '' || formOutTimeWt == null || formOutTimeWt == undefined) {
                params.FROM_OUT_TIME_WT = null;
                params.TO_OUT_TIME_WT = null;
            }
            else {
                params.FROM_OUT_TIME_WT = moment(formOutTimeWt).format('HH:mm');
                params.TO_OUT_TIME_WT = moment(toOutTimeWt).format('HH:mm');
            }
            console.log(params);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);

            form.submit();

            document.body.removeChild(form);
                

            //$http({
            //    //headers: { "RequestVerificationToken": vm.antiForgeryToken },
            //    url: '/Hr/HrReport/Index',
            //    method: 'post'             
            //    //params: { ob: vm.form }
            //}).then(function () {                
            //    window.open("/CrystalReportViewer", '_blank')
            
            //});
        };


        vm.saleStoreDs = {
            optionLabel: "-- Sales Store --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'get',
                            url: "/api/inv/ItemCategory/getSalesStore"
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                }
            },
            dataTextField: "STORE_NAME_EN",
            dataValueField: "PS_STORE_ID"
        };

         vm.salePointDs = {
            optionLabel: "-- Sales Point --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'get',
                            url: "/api/inv/ItemCategory/getSalesPoint"
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                }
            },
            dataTextField: "COUNTER_DESC",
            dataValueField: "PS_COUNTR_ID"
         };

         vm.itemCategoryDs = {
             optionLabel: "-- Item Category --",
             filter: "contains",
             autoBind: true,
             dataSource: {
                 transport: {
                     read: function (e) {
                         $http({
                             method: 'get',
                             url: "/api/inv/ItemCategory/ItemCategList4LastLevel?pPARENT_ID=13"
                         }).
                         success(function (data, status, headers, config) {
                             e.success(data)
                         }).
                         error(function (data, status, headers, config) {
                             alert('something went wrong')
                             console.log(status);
                         });
                     }
                 }
             },
             dataTextField: "ITEM_CAT_NAME_EN",
             dataValueField: "INV_ITEM_CAT_ID",
             select: function (e) {
                 var item = this.dataItem(e.item);
                 
                 vm.form.ITEM_CAT_NAME_EN = item.ITEM_CAT_NAME_EN;
             }
         };

         vm.customerTypeDs = {
             optionLabel: "-- Customer Type --",
             filter: "contains",
             autoBind: true,
             dataSource: {
                 transport: {
                     read: function (e) {
                         $http({
                             method: 'post',
                             url: "/Hr/HrEmployee/LookupListData",
                             params: { pLookupTableId: 118 }
                         }).
                         success(function (data, status, headers, config) {
                             e.success(data)
                         }).
                         error(function (data, status, headers, config) {
                             alert('something went wrong')
                             console.log(status);
                         });
                     }
                 }
             },
             dataTextField: "LK_DATA_NAME_EN",
             dataValueField: "LOOKUP_DATA_ID"
         };



                                 
        
    }

    

})();