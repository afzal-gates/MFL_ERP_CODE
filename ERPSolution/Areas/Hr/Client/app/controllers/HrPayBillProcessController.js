(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrPayBillController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', 'HrService', 'billType', HrPayBillController]);

    function HrPayBillController(logger, config, $q, $scope, $http, exception, $filter, $state, HrService, billType) {

        var vm = this;
        
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        activate();

        function activate() {
            var promise = [getCompanyList(), getOfficeList(), getOtTypeList(), getBillTypeList(), getDesigGrpList(), floorList(), lineList(), shiftTeamList(), getManagementTypeList(),
            parentDepartmentList(), getSubdepartmentListData()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        console.log(billType);

        vm.insert = true;
        vm.today = new Date();        

        //var currMonth=moment()
        var lastDayOfMonth = moment().daysInMonth();

        var firstDate = moment().date(1);
        var lastDate = moment().date(lastDayOfMonth);

        vm.form = {PAY_PCT:50};
        vm.SELECTION_TYPE = 1;
        $scope.SELECTION_TYPE = vm.SELECTION_TYPE;
        var dt = firstDate._d;
        vm.form.JOINING_DT = $filter('date')(vm.today, vm.dtFormat);
        dt = lastDate._d;
        //vm.form.TO_DATE = $filter('date')(vm.today, vm.dtFormat);
                                               
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
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
                        vm.empListData.splice(index,1);

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

        vm.isOtBill = false;
        vm.isNightBill = false;

        function getBillTypeList() {
            return vm.billTypeListData = {
                optionLabel: "-- Select Bill Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getBillTypeListData().then(function (res) {
                                e.success(res);                                
                            });
                        },
                    }
                },                
                dataTextField: "PAY_ELEMENT_NAME_EN",
                dataValueField: "PAY_ELEMENT_CODE",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.form.HR_PAY_ELEMENT_ID = dataItem.HR_PAY_ELEMENT_ID;

                    if (dataItem.PAY_ELEMENT_CODE == 'A010') { //OT Bill
                        vm.isOtBill = true;
                        vm.isNightBill = false;
                    }
                    else if (dataItem.PAY_ELEMENT_CODE == 'A013') { //Night Bill
                        vm.isOtBill = false;
                        vm.isNightBill = true;
                        //vm.EMPLOYEE_SELECTION = 0;
                        vm.empListData = [];
                        $("#empGrid").empty();
                    }
                    else {
                        vm.isOtBill = false;
                        vm.isNightBill = false;
                    }
                }
            };
        };

        function getOtTypeList() {
            return vm.otTypeListData = {
                placeholder: "Select OT Types...",
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
                dataValueField: "OT_TYPE_CODE"
            };
        };

        function getCompanyList() {
            return vm.companyList = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                //index:1,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getCompanyData().then(function (res) {
                                e.success(res);

                                $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                                    optionLabel: "-- Select --",
                                    filter: "startswith",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'post',
                                                    url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                                    params: {
                                                        pHR_COMPANY_ID: res[0].HR_COMPANY_ID,
                                                        pHR_PERIOD_TYPE_ID: null
                                                    }
                                                }).
                                                success(function (data, status, headers, config) {
                                                    if (billType['BILL_TYPE'] == 'EL_BILL') {
                                                        var dataList = _.filter(data, function (ob) {
                                                            return ob.HR_PERIOD_TYPE_ID == 4;
                                                        });
                                                    }
                                                    else if (billType['BILL_TYPE'] == 'OTHER_BILL') {
                                                        var dataList = _.filter(data, function (ob) {
                                                            return ob.HR_PERIOD_TYPE_ID != 4;
                                                        });
                                                    }
                                                    else {
                                                        var dataList = data;
                                                    }

                                                    e.success(dataList);
                                                    
                                                    if (dataList.length > 0) {
                                                        //vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;

                                                        dt = moment(dataList[0].START_DATE)._d;
                                                        //vm.form.FROM_DATE = $filter('date')(dt, vm.dtFormat);

                                                        dt = moment(dataList[0].END_DATE)._d;
                                                        //vm.form.TO_DATE = $filter('date')(dt, vm.dtFormat);
                                                    }
                                                    else {
                                                        //vm.form.ACC_PAY_PERIOD_ID = null;
                                                        vm.form.FROM_DATE = null;
                                                        vm.form.TO_DATE = null;
                                                    }
                                                }).
                                                error(function (data, status, headers, config) {
                                                    alert('something went wrong')
                                                    console.log(status);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "REMARKS",
                                    dataValueField: "ACC_PAY_PERIOD_ID"
                                });

                                $('#ACC_PAY_MONTH_ID').kendoDropDownList({
                                    optionLabel: "-- Select --",
                                    filter: "startswith",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'post',
                                                    url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                                    params: {
                                                        pHR_COMPANY_ID: res[0].HR_COMPANY_ID,
                                                        pHR_PERIOD_TYPE_ID: null
                                                    }
                                                }).
                                                success(function (data, status, headers, config) {
                                                    if (billType['BILL_TYPE'] == 'EL_BILL') {
                                                        var dataList = _.filter(data, function (ob) {
                                                            return ob.HR_PERIOD_TYPE_ID == 3;
                                                        });
                                                    }
                                                    else {
                                                        var dataList = data;
                                                    }

                                                    e.success(dataList);
                                                    //console.log(data.length);

                                                }).
                                                error(function (data, status, headers, config) {
                                                    alert('something went wrong')
                                                    console.log(status);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "REMARKS",
                                    dataValueField: "ACC_PAY_PERIOD_ID"
                                });





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

                    $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                        optionLabel: "-- Select --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    $http({
                                        method: 'post',
                                        url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                        params: {
                                            pHR_COMPANY_ID: vHR_COMPANY_ID,
                                            pHR_PERIOD_TYPE_ID: null
                                        }
                                    }).
                                    success(function (data, status, headers, config) {
                                        if (billType['BILL_TYPE'] == 'EL_BILL') {
                                            var dataList = _.filter(data, function (ob) {
                                                return ob.HR_PERIOD_TYPE_ID == 4;
                                            });
                                        }
                                        else if (billType['BILL_TYPE'] == 'OTHER_BILL') {
                                            var dataList = _.filter(data, function (ob) {
                                                return ob.HR_PERIOD_TYPE_ID != 4;
                                            });
                                        }
                                        else {
                                            var dataList = data;
                                        }

                                        e.success(dataList);
                                        //console.log(data.length);
                                        if (dataList.length > 0) {
                                            
                                            //vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;

                                            dt = moment(dataList[0].START_DATE)._d;
                                            //vm.form.FROM_DATE = $filter('date')(dt, vm.dtFormat);

                                            dt = moment(dataList[0].END_DATE)._d;
                                            //vm.form.TO_DATE = $filter('date')(dt, vm.dtFormat);
                                        }
                                        else {
                                            //vm.form.ACC_PAY_PERIOD_ID = null;
                                            vm.form.FROM_DATE = null;
                                            vm.form.TO_DATE = null;
                                        }
                                    }).
                                    error(function (data, status, headers, config) {
                                        alert('something went wrong')
                                        console.log(status);
                                    });
                                }
                            }
                        },
                        select: function (e1) {
                            var dataItem1 = this.dataItem(e1.item);

                            //alert(dataItem1.ACC_PAY_PERIOD_ID);
                            //vm.form.ACC_PAY_PERIOD_ID = dataItem1.ACC_PAY_PERIOD_ID;

                            dt = moment(dataItem1.START_DATE)._d;
                            vm.form.FROM_DATE = $filter('date')(dt, vm.dtFormat);

                            dt = moment(dataItem1.END_DATE)._d;
                            vm.form.TO_DATE = $filter('date')(dt, vm.dtFormat);
                        },
                        dataTextField: "REMARKS",
                        dataValueField: "ACC_PAY_PERIOD_ID"
                    });


                    $('#ACC_PAY_MONTH_ID').kendoDropDownList({
                        optionLabel: "-- Select --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    $http({
                                        method: 'post',
                                        url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                        params: {
                                            pHR_COMPANY_ID: vHR_COMPANY_ID,
                                            pHR_PERIOD_TYPE_ID: null
                                        }
                                    }).
                                    success(function (data, status, headers, config) {
                                        if (billType['BILL_TYPE'] == 'EL_BILL') {
                                            var dataList = _.filter(data, function (ob) {
                                                return ob.HR_PERIOD_TYPE_ID == 3;
                                            });
                                        }                                        
                                        else {
                                            var dataList = data;
                                        }

                                        e.success(dataList);
                                        //console.log(data.length);
                                        
                                    }).
                                    error(function (data, status, headers, config) {
                                        alert('something went wrong')
                                        console.log(status);
                                    });
                                }
                            }
                        },                        
                        dataTextField: "REMARKS",
                        dataValueField: "ACC_PAY_PERIOD_ID"
                    });



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


        //========== Start for EL Process
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

        function getDesigGrpList() {
            $scope.gridOptions = {
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrSalaryTran/DesignationGrpListData",  //+ "&pType=" + showType
                                params:{pPARAM_TYPE:'EL ENCASHMENT PROCESS'}
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    },
                    pageSize: 100
                },
                filterable: true,
                selectable: "row",
                sortable: {
                    mode: "single",
                    allowUnsort: false
                },
                height: 420,
                pageable: {
                    refresh: true,
                    pageSizes: false//,
                    //buttonCount: 5
                },
                columns: [
                    {
                        filterable: false,
                        title: " ", //"<input type='checkbox' id='checkAll' class='check-box' ng-model='vm.allCheck' >",
                        field: "IS_PARAM_ACTIVE",
                        width: "30px",
                        template: function (e) {
                            return "<input type='checkbox' ng-model='dataItem.IS_PARAM_ACTIVE' class='check-box'>";
                        }
                    },
                    { field: "DESIG_GRP_NAME_EN", title: "Designation Group", width: "220px" },
                    //{                      
                    //    template: function (e) {
                    //        return "<input type='hidden' ng-value='vm.desigGrp[dataItem.HR_DESIGNATION_GRP_ID].HR_DESIGNATION_GRP_ID=dataItem.HR_DESIGNATION_GRP_ID' ng-model='vm.desigGrp[dataItem.HR_DESIGNATION_GRP_ID].HR_DESIGNATION_GRP_ID'>";
                    //    }
                    //},
                ]
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
                                e.success(data);
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
                        //            var dataItem = this.dataItem(e.item.index());
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

        $scope.$watch('SELECTION_TYPE', function (newVal, oldVal) {
            //alert(newVal + ' ' + oldVal);
            if (newVal == null) {
                $scope.SELECTION_TYPE = vm.SELECTION_TYPE;
                newVal = vm.SELECTION_TYPE;
            };

            vm.form.CORE_DEPT_ID = "";
            vm.form.HR_DEPARTMENT_ID = "";
            vm.form.HR_SHIFT_TEAM_ID = "";
            vm.form.LK_FLOOR_ID = "";
            vm.form.LINE_NO = "";
            vm.form.HR_MANAGEMENT_TYPE_ID = "";
            vm.form.HR_EMPLOYEE_ID = "";
            vm.form.EMPLOYEE_CODE = "";

            vm.empListData = [];
            $("#empGrid").empty();

            //if (newVal != oldVal) {
            if ($scope.SELECTION_TYPE == 1) {
                vm.isSubDepartment = false;
                vm.isTeam = false;
                vm.isFloor = false;
                vm.isEmployee = false;
                vm.isManagementType = false;
                vm.isReligion = false;
            }
            else if ($scope.SELECTION_TYPE == 2) {
                vm.isSubDepartment = true;
                vm.isTeam = false;
                vm.isFloor = false;
                vm.isEmployee = false;
                vm.isManagementType = false;
                vm.isReligion = false;
            }
            else if ($scope.SELECTION_TYPE == 3) {
                vm.isSubDepartment = false;
                vm.isTeam = true;
                vm.isFloor = false;
                vm.isEmployee = false;
                vm.isManagementType = false;
                vm.isReligion = false;
            }
            else if ($scope.SELECTION_TYPE == 4) {
                vm.isSubDepartment = false;
                vm.isTeam = false;
                vm.isFloor = true;
                vm.isEmployee = false;
                vm.isManagementType = false;
                vm.isReligion = false;
            }
            else if ($scope.SELECTION_TYPE == 5) {
                vm.isSubDepartment = false;
                vm.isTeam = false;
                vm.isFloor = false;
                vm.isEmployee = false;
                vm.isManagementType = true;
                vm.isReligion = false;
            }
            else if ($scope.SELECTION_TYPE == 6) {
                vm.isSubDepartment = false;
                vm.isTeam = false;
                vm.isFloor = false;
                vm.isEmployee = true;
                vm.isManagementType = false;
                vm.isReligion = false;
            }
            else if ($scope.SELECTION_TYPE == 7) {
                vm.isSubDepartment = false;
                vm.isTeam = false;
                vm.isFloor = false;
                vm.isEmployee = false;
                vm.isManagementType = false;
                vm.isReligion = true;
            }
            //};
        });

        vm.ElEncashmentProcess = function (form, insert) {
            if (vm.form.HR_COMPANY_ID == null) {
                alert('Please select company');
                return;
            }
            else if (vm.form.ACC_PAY_PERIOD_ID == null) {
                alert('Please select pay period');
                return;
            }
            else if (vm.form.ACC_PAY_MONTH_ID == null) {
                alert('Please select pay month');
                return;
            }
            else if (vm.form.JOINING_DT == null) {
                alert('Please select joining date');
                return;
            } 

            vm.showSplash = true;

            if (!insert) return;

            vm.desigGrpData = [];
            vm.desigGrp = $("#desigGrpGrid").data("kendoGrid").dataSource.data();

            angular.forEach(vm.desigGrp, function (val, key) {
                if (val.IS_PARAM_ACTIVE) {
                    vm.desigGrpData.push(val.HR_DESIGNATION_GRP_ID);
                }
            });

            var x = confirm("Do you want to process EL encashment?");
            if (x == false) {
                vm.showSplash = false;
                return;
            }


            vm.form['HR_DESIGNATION_GRP_IDS'] = vm.desigGrpData.join(',');

            if (insert) {
                vm.insert = false;

                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrPayBill/ElEncashmentProcess',
                    method: 'post',
                    data: {
                        pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pACC_PAY_PERIOD_ID: vm.form.ACC_PAY_PERIOD_ID, pACC_PAY_MONTH_ID: vm.form.ACC_PAY_MONTH_ID,
                        pHR_DEPARTMENT_ID: vm.form.HR_DEPARTMENT_ID,
                        pHR_DESIGNATION_ID: null, pHR_SHIFT_TEAM_ID: vm.form.HR_SHIFT_TEAM_ID, pHR_EMPLOYEE_ID: vm.form.HR_EMPLOYEE_ID,
                        pEMPLOYEE_TYPE_ID: vm.form.EMPLOYEE_TYPE_ID, pLK_FLOOR_ID: vm.form.LK_FLOOR_ID, pLINE_NO: vm.form.LINE_NO,
                        pCORE_DEPT_ID: vm.form.CORE_DEPT_ID, pHR_DESIGNATION_GRP_IDS: vm.form.HR_DESIGNATION_GRP_IDS, pJOINING_DT: vm.form.JOINING_DT,
                        pPAY_PCT: vm.form.PAY_PCT/100
                    }
                }).then(function (data, status, headers, config1) {

                    vm.errors = undefined;
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        config.appToastMsg(data.data.vMsg);
                        alert(data.data.vMsg.substr(9));
                        vm.insert = true;
                    }

                    vm.showSplash = false;

                }).catch(function (message) {
                    //console.log(message);
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        };
        //========== End for EL Process
        
        vm.billProcess = function (form, insert) {
            vm.showSplash = true;

            if (!insert) return;

            var x = confirm("Do you want to process?");
            if (x == false) {
                vm.showSplash = false;
                return;
            }

            //alert(vm.form.ACC_PAY_PERIOD_ID);

            if (insert) {
                vm.insert = false;

                vm.empList = [];
                vm.form.HR_EMPLOYEE_IDS = null;
                angular.forEach(vm.empListData, function (val, key) {
                    vm.empList.push(val.HR_EMPLOYEE_ID);
                });
                vm.form['HR_EMPLOYEE_IDS'] = vm.empList.join(',');

                if (form.PAY_ELEMENT_CODE == 'A010') { // OT Bill
                    if (form.OT_TYPES != null && form.OT_TYPES != undefined) {
                        form['OT_TYPES'] = form.OT_TYPES.join(',');
                        //alert(form.OT_TYPES);
                    }

                    $http({
                        headers: { "RequestVerificationToken": vm.antiForgeryToken },
                        url: '/Hr/HrPayBill/OTBillProcessData',
                        method: 'post',
                        data: {
                            pFromDate: vm.form.FROM_DATE, pToDate: vm.form.TO_DATE, pPAY_ELEMENT_CODE: vm.form.PAY_ELEMENT_CODE,
                            pOT_TYPES: vm.form.OT_TYPES,
                            pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pACC_PAY_PERIOD_ID: vm.form.ACC_PAY_PERIOD_ID, pHR_EMPLOYEE_IDS: vm.form.HR_EMPLOYEE_IDS,
                            pCORE_DEPT_ID: vm.form.CORE_DEPT_ID, pHR_DEPARTMENT_ID: vm.form.HR_DEPARTMENT_ID, pHR_SHIFT_TEAM_ID: vm.form.HR_SHIFT_TEAM_ID,
                            pLK_FLOOR_ID: vm.form.LK_FLOOR_ID, pLINE_NO: vm.form.LINE_NO, pEMPLOYEE_TYPE_ID: vm.form.HR_MANAGEMENT_TYPE_ID
                        }
                    }).then(function (data, status, headers, config1) {

                        vm.errors = undefined;
                        if (data.success === false) {
                            vm.errors = data.errors;
                        }
                        else {
                            config.appToastMsg(data.data.vMsg);
                            alert(data.data.vMsg.substr(9));
                            vm.insert = true;
                        }

                        vm.showSplash = false;

                    }).catch(function (message) {
                        //console.log(message);
                        exception.catcher('XHR loading Failded')(message);
                    });
                }
                else if (form.PAY_ELEMENT_CODE == 'A012' || form.PAY_ELEMENT_CODE == 'A013') { // Tiffin Bill, Night Bill
                    //console.log(vm.form);
                    //console.log(vm.empListData);                    

                    //vm.empList = [];
                    //vm.form.HR_EMPLOYEE_IDS = null;
                    //if (form.PAY_ELEMENT_CODE == 'A013') {
                    //    if (vm.EMPLOYEE_SELECTION == 0) {
                    //        vm.empListData = [];
                    //    };

                    //    angular.forEach(vm.empListData, function (val, key) {
                    //        vm.empList.push(val.HR_EMPLOYEE_ID);
                    //    });

                    //    vm.form['HR_EMPLOYEE_IDS'] = vm.empList.join(',');
                    //}

                    //console.log(vm.form.HR_EMPLOYEE_IDS);
                    

                    $http({
                        headers: { "RequestVerificationToken": vm.antiForgeryToken },
                        url: '/Hr/HrPayBill/OtherBillProcessData',
                        method: 'post',
                        data: {
                            pFromDate: vm.form.FROM_DATE, pToDate: vm.form.TO_DATE, pPAY_ELEMENT_CODE: vm.form.PAY_ELEMENT_CODE,
                            pOT_TYPES: vm.form.OT_TYPES,
                            pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pACC_PAY_PERIOD_ID: vm.form.ACC_PAY_PERIOD_ID,
                            pHR_PAY_ELEMENT_ID: vm.form.HR_PAY_ELEMENT_ID, pHR_EMPLOYEE_IDS: vm.form.HR_EMPLOYEE_IDS, pHR_OFFICE_ID: vm.form.HR_OFFICE_ID
                        }
                    }).then(function (data, status, headers, config1) {

                        vm.errors = undefined;
                        if (data.success === false) {
                            vm.errors = data.errors;
                        }
                        else {
                            config.appToastMsg(data.data.vMsg);
                            alert(data.data.vMsg.substr(9));
                            vm.insert = true;
                        }

                        vm.showSplash = false;

                    }).catch(function (message) {
                        //console.log(message);
                        exception.catcher('XHR loading Failded')(message);
                    });
                }
                //else if (form.PAY_ELEMENT_CODE == 'A013') { // Tiffin Bill
                //    $http({
                //        headers: { "RequestVerificationToken": vm.antiForgeryToken },
                //        url: '/Hr/HrPayBill/NightBillProcessData',
                //        method: 'post',
                //        data: {
                //            pFromDate: vm.form.FROM_DATE, pToDate: vm.form.TO_DATE, pPAY_ELEMENT_CODE: vm.form.PAY_ELEMENT_CODE,
                //            pOT_TYPES: vm.form.OT_TYPES,
                //            pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pACC_PAY_PERIOD_ID: vm.form.ACC_PAY_PERIOD_ID
                //        }
                //    }).then(function (data, status, headers, config1) {

                //        vm.errors = undefined;
                //        if (data.success === false) {
                //            vm.errors = data.errors;
                //        }
                //        else {
                //            config.appToastMsg(data.data.vMsg);
                //            vm.insert = true;
                //        }

                //        vm.showSplash = false;

                //    }).catch(function (message) {
                //        //console.log(message);
                //        exception.catcher('XHR loading Failded')(message);
                //    });
                //}

            }
        };
        
        vm.downLoad4Bkash = function(formData){
            var frmData = angular.copy(formData);
            
            var url = '/Hr/HrReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            frmData['REPORT_CODE'] = 'RPT-1071';
            frmData['IS_EXCEL_FORMAT'] = 'Y';
            frmData['FROM_DATE'] = $filter('date')(frmData.FROM_DATE, vm.dtFormat);
            frmData['TO_DATE'] = $filter('date')(frmData.TO_DATE, vm.dtFormat);
            
            var params = angular.copy(frmData);
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

            //alert('Download Successfully');
        }                   
        
    }

    

})();