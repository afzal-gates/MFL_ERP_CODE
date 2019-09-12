//============== Start for SmsBroadcast2EmpController
(function () {
    'use strict';
    angular.module('multitex.hr').controller('SmsBroadcast2EmpController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', '$filter', '$timeout', 'Dialog', SmsBroadcast2EmpController]);

    function SmsBroadcast2EmpController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams, $filter, $timeout, Dialog) {
                        
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        //var key = 'HR_EMP_TRNFR_ID';
        
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.periodType = 3;
        vm.today = new Date();
        
        vm.smsTypeList = [{ SMS_TYPE_ID: 1, SMS_TYPE_NAME: 'Bulk' }, { SMS_TYPE_ID: 2, SMS_TYPE_NAME: 'Salary Pay Slip' }, { SMS_TYPE_ID: 3, SMS_TYPE_NAME: 'Bonus Pay Slip' }];
        vm.paramSpecTypeList = [{ PARAM_SPEC_TYPE_ID: 1, PARAM_SPEC_TYPE_NAME: 'All Employees' }, { PARAM_SPEC_TYPE_ID: 2, PARAM_SPEC_TYPE_NAME: 'Specific Department' },
            { PARAM_SPEC_TYPE_ID: 3, PARAM_SPEC_TYPE_NAME: 'Specific Team' }, { PARAM_SPEC_TYPE_ID: 4, PARAM_SPEC_TYPE_NAME: 'Specific Floor' },
            { PARAM_SPEC_TYPE_ID: 5, PARAM_SPEC_TYPE_NAME: 'Specific Employee Type' }, { PARAM_SPEC_TYPE_ID: 6, PARAM_SPEC_TYPE_NAME: 'Specific Employees' }
        ];


        vm.form = {
            SMS_TYPE_ID: 1, PARAM_SPEC_TYPE_ID: 1, HR_COMPANY_ID_LIST: [], HR_OFFICE_ID_LIST: [], HR_DEPARTMENT_ID_LIST: [], HR_MANAGEMENT_TYPE_ID_LIST: [],
            HR_SHIFT_TEAM_ID_LIST: [], LK_FLOOR_ID_LIST: [], LINE_NO_LIST: []
        };
        
        
        activate();

        function activate() {
            var promise = [getCompanyList(), getCompPeriodList(), getOfficeList(), getSectionList(), shiftTeamList(), getManagementTypeList(), getLkFloorList(), getLkLineList() /*getProdFloorList(), getProdLineList()*/];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        
        $scope.$watch('vm.form.PARAM_SPEC_TYPE_ID', function (newVal, oldVal) {
            //alert(newVal + ' ' + oldVal);
            //if (newVal == null) {
            //    vm.form.PARAM_SPEC_TYPE_ID = vm.SELECTION_TYPE;
            //    newVal = vm.SELECTION_TYPE;
            //};

            vm.isSubDepartment = false;
            vm.isTeam = false;
            vm.isFloor = false;
            vm.isEmployee = false;
            vm.isManagementType = false;
            
            vm.form.HR_COMPANY_ID_LIST = [];
            vm.form.HR_OFFICE_ID_LIST = [];
            vm.form.HR_DEPARTMENT_ID_LIST = [];
            vm.form.HR_MANAGEMENT_TYPE_ID_LIST = [];
            vm.form.HR_SHIFT_TEAM_ID_LIST = [];

            vm.form.LK_FLOOR_ID_LIST = [];
            vm.form.LINE_NO_LIST = [];
            
            vm.form.HR_EMPLOYEE_ID = null;
            vm.form.EMPLOYEE_CODE = null;

            vm.empListData = [];
            $("#empGrid").empty();

            //if (newVal != oldVal) {
            if (vm.form.PARAM_SPEC_TYPE_ID == 2) {
                vm.isSubDepartment = true;                
            }
            else if (vm.form.PARAM_SPEC_TYPE_ID == 3) {                
                vm.isTeam = true;                
            }
            else if (vm.form.PARAM_SPEC_TYPE_ID == 4) {                
                vm.isFloor = true;                
            }
            else if (vm.form.PARAM_SPEC_TYPE_ID == 5) {                
                vm.isManagementType = true;                
            }
            else if (vm.form.PARAM_SPEC_TYPE_ID == 6) {            
                vm.isEmployee = true;
            }
        });


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

            vm.obEmp = item;
            vm.empGrid();
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;
            }

        });

        vm.empListData = [];
        vm.empGrid = function () {
            var dataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        if (vm.obEmp != null) {
                            vm.empListData.push({
                                HR_EMPLOYEE_ID: vm.obEmp.HR_EMPLOYEE_ID, EMPLOYEE_CODE: vm.obEmp.EMPLOYEE_CODE, EMP_FULL_NAME_EN: vm.obEmp.EMP_FULL_NAME_EN
                            });
                            e.success(vm.empListData);
                            vm.obEmp = null
                            vm.form.EMPLOYEE_CODE = null;
                        }
                    },
                    destroy: function (e) {
                        var index = HrService.getIndexByKeyValue(vm.empListData, 'HR_EMPLOYEE_ID', e.HR_EMPLOYEE_ID);
                        vm.empListData.splice(index, 1);

                        e.success(vm.empListData);
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }
                },
                pageSize: 1000,
                schema: {
                    model: {
                        id: "HR_EMPLOYEE_ID",
                        fields: {
                            HR_EMPLOYEE_ID: { type: "number", editable: false },
                            EMPLOYEE_CODE: { type: "string", editable: false },
                            EMP_FULL_NAME_EN: { type: "string", editable: false },
                        }
                    }
                }
                //aggregate: [
                //    { field: "PAY_AMT", aggregate: "sum" }
                //]
            });


            $("#empGrid").kendoGrid({
                dataSource: dataSource,
                navigatable: true,
                sortable: true,
                selectable: "row",
                pageable: {
                    refresh: false,
                    pageSizes: false
                },
                height: 200,
                //toolbar: ["save", "cancel"],
                columns: [
                    { field: "EMPLOYEE_CODE", title: "Emp.Code", width: "100px" },
                    { field: "EMP_FULL_NAME_EN", title: "Name", width: "auto" },
                    { command: [{ name: "destroy", text: "" }], width: "50px" }
                ],
                editable: {
                    confirmation: false, //"Do you want to remove this record?",
                    mode: "inline"
                }
            });
        };

        function getCompanyList() {
            vm.compOptions = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);

                    vm.form.HR_COMPANY_ID = dataItem.HR_COMPANY_ID;

                    vm.compPeriodDataSource.read()
                }
            }

            return vm.compDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        return HrService.GetAllOthers('/api/common/CompanyList').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });                            
        };
        
        function getCompPeriodList() {
            vm.compPeriodOption = {
                optionLabel: "-- Select Period --",
                filter: "contains",
                autoBind: true,
                dataTextField: "REMARKS",
                dataValueField: "ACC_PAY_PERIOD_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);                    
                }
            }

            return vm.compPeriodDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        return HrService.getDataByFullUrl("/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + vm.form.HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodType + "&pIS_CLOSED=N").then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        function getOfficeList() {
            
            return vm.officeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/common/OfficeList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
           
        };
        
        vm.coreDeptId = 0;
        function getSectionList() {
            return vm.sectionDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getSubDepartmentData().then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
            
        };

        function shiftTeamList() {
            return vm.shiftTeamDataSource = new kendo.data.DataSource({
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
            });            
        };

        function getManagementTypeList() {
            return vm.employeeTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getEmployeeTypeData().then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });            
        };

        function getDesignationList() {
            vm.desigOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "DESIGNATION_NAME_EN",
                dataValueField: "HR_DESIGNATION_ID"
            };

            return vm.desigDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDesigData((vm.coreDeptId || -1)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        function getLkFloorList() {
            return vm.lkFloorDataSource = new kendo.data.DataSource({
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
            });            
        };

        function getLkLineList() {
            return vm.lkLineDataSource = new kendo.data.DataSource({
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
            });
        };

        function getProdFloorList() {
            vm.prodFloorOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FLOOR_DESC_EN",
                dataValueField: "HR_PROD_FLR_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    vm.form.N_HR_PROD_FLR_ID = dataItem.HR_PROD_FLR_ID;

                    vm.prodLineDataSource.read();
                },
            };

            return vm.prodFloorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/GetProdFloorList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                group: { field: "BLDNG_DESC_EN" },
                sort: [{ field: 'HR_PROD_BLDNG_ID', dir: 'asc' }]
            });
        }

        function getProdLineList() {
            vm.prodLineOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LINE_NO",
                dataValueField: "HR_PROD_LINE_ID"
            };

            return vm.prodLineDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.getDataByFullUrl('/api/hr/GetPrdLineList?pHR_PROD_FLR_ID=' + (vm.form.N_HR_PROD_FLR_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }
                   

        vm.send = function () {
            var submitData = angular.copy(vm.form);

            //submitData.TRNF_REF_DT = $filter('date')(submitData.TRNF_REF_DT, vm.dtFormat);
            //submitData.EFFECT_DT = $filter('date')(submitData.EFFECT_DT, vm.dtFormat);

            vm.empList = [];
            submitData.HR_EMPLOYEE_ID_LST = null;
            angular.forEach(vm.empListData, function (val, key) {
                vm.empList.push(val.HR_EMPLOYEE_ID);
            });
            submitData['HR_EMPLOYEE_ID_LST'] = vm.empList.join(',');
            
            submitData['HR_COMPANY_ID_LST'] = vm.form.HR_COMPANY_ID_LIST.join(',');
            submitData['HR_OFFICE_ID_LST'] = vm.form.HR_OFFICE_ID_LIST.join(',');
            submitData['HR_DEPARTMENT_ID_LST'] = vm.form.HR_DEPARTMENT_ID_LIST.join(',');
            submitData['HR_SHIFT_TEAM_ID_LST'] = vm.form.HR_SHIFT_TEAM_ID_LIST.join(',');
            submitData['LK_FLOOR_ID_LST'] = vm.form.LK_FLOOR_ID_LIST.join(',');
            submitData['LINE_NO_LST'] = vm.form.LINE_NO_LIST.join(',');
            submitData['HR_MANAGEMENT_TYPE_ID_LST'] = vm.form.HR_MANAGEMENT_TYPE_ID_LIST.join(',');
                        
            var vMsg = 'Do you want to send?';
            
            console.log(submitData);


            //HrService.getDataByFullUrl('/api/hr/SmsBroadcast2Emp/SendSms?pSMS_TYPE_ID=' + submitData['SMS_TYPE_ID'] + '&pHR_DEPARTMENT_ID_LST=' + submitData['HR_DEPARTMENT_ID_LST']).then(function (res) {
            //    console.log(res);

            //    var anchor = angular.element('<a/>');
            //    anchor.attr({
            //        href: '/UPLOAD_DOCS/sms.xls', 
            //        target: '_blank',
            //        download: 'filename.xls'
            //    })[0].click();
            //});

           


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {
                vm.showSplash = true;

                return HrService.saveDataByUrl(submitData, '/api/hr/SmsBroadcast2Emp/SendSms').then(function (res) {
                    console.log(res);

                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        
                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);

                        

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                           
                            var anchor = angular.element('<a/>');
                            anchor.attr({
                                href: '/UPLOAD_DOCS/sms.txt',
                                target: '_blank',
                                download: 'sms_' + $filter('date')(vm.today, vm.dtFormat) + '.txt'
                            })[0].click();

                        };

                        vm.showSplash = false;
                        config.appToastMsg(res.data.PMSG);

                    }
                });
            });


        };


    }
    
})();
//============== End for MbnBillProcessController


