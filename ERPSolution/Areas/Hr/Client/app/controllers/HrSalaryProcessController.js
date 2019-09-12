(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrSalaryProcessController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', 'HrService', HrSalaryProcessController]);

    function HrSalaryProcessController(logger, config, $q, $scope, $http, exception, $filter, $state, HrService) {

        var vm = this;
        
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        activate();

        vm.insert = true;
        vm.today = new Date();        

        //var currMonth=moment()
        var lastDayOfMonth = moment().daysInMonth();

        var firstDate = moment().date(1);
        var lastDate = moment().date(lastDayOfMonth);


        vm.form = {DAY_TYPE:null};
        vm.SELECTION_TYPE = 1;
        $scope.SELECTION_TYPE = vm.SELECTION_TYPE;
        var dt = firstDate._d;
        vm.form.FROM_DATE = $filter('date')(vm.today, vm.dtFormat);
        dt = lastDate._d;
        vm.form.TO_DATE = $filter('date')(vm.today, vm.dtFormat);
                       
                        
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

        vm.OT_START_DTDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.OT_START_DTDateOpened = true;
        };

        vm.OT_END_DTDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.OT_END_DTDateOpened = true;
        };


        $scope.emoloyeeAuto = function (val) {
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectItem = function (item) {
            vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;
            }

        });


        $scope.$watch('SELECTION_TYPE', function (newVal, oldVal) {
            //alert(newVal + ' ' + oldVal);
            if (newVal == null) {
                $scope.SELECTION_TYPE = vm.SELECTION_TYPE;
                newVal = vm.SELECTION_TYPE;
            };

            vm.form.CORE_DEPT_ID = "";
            vm.form.HR_DEPARTMENT_ID=""; 
            vm.form.HR_SHIFT_TEAM_ID = "";
            vm.form.LK_FLOOR_ID = "";
            vm.form.LINE_NO = "";
            vm.form.HR_MANAGEMENT_TYPE_ID = "";
            vm.form.HR_EMPLOYEE_ID = "";
            vm.form.EMPLOYEE_CODE = "";

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

                                //$('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                                //    //optionLabel: "-- Select Period --",
                                //    filter: "startswith",
                                //    autoBind: true,
                                //    dataSource: {
                                //        transport: {
                                //            read: function (e) {
                                //                $http({
                                //                    method: 'post',
                                //                    url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                //                    params: {
                                //                        pHR_COMPANY_ID: res[0].HR_COMPANY_ID,
                                //                        pHR_PERIOD_TYPE_ID: 3
                                //                    }
                                //                }).
                                //                success(function (data, status, headers, config) {
                                //                    e.success(data);
                                //                    vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                //                }).
                                //                error(function (data, status, headers, config) {
                                //                    alert('something went wrong')
                                //                    console.log(status);
                                //                });
                                //            }
                                //        }
                                //    },
                                //    dataTextField: "REMARKS",
                                //    dataValueField: "ACC_PAY_PERIOD_ID"
                                //});

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
                        optionLabel: "-- Select Period --",
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
                                            pHR_PERIOD_TYPE_ID: vm.PERIOD_TYPE_ID
                                        }
                                    }).
                                    success(function (data, status, headers, config) {
                                        e.success(data);
                                        //if (data.length > 0) {
                                        //    //vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
                                            
                                        //    dt = moment(data[0].START_DATE)._d;
                                        //    vm.form.FROM_DATE = $filter('date')(dt, vm.dtFormat);

                                        //    dt = moment(data[0].END_DATE)._d;
                                        //    vm.form.TO_DATE = $filter('date')(dt, vm.dtFormat);
                                        //    alert(vm.form.FROM_DATE);
                                        //    vm.form.NO_OF_DAYS = (vm.form.TO_DATE - vm.form.FROM_DATE);
                                        //};
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
                        select: function (e) {
                            var dataItem = this.dataItem(e.item);

                            dt = moment(dataItem.START_DATE)._d;
                            vm.form.FROM_DATE = $filter('date')(dt, vm.dtFormat);

                            var dt1 = moment(dataItem.END_DATE)._d;
                            vm.form.TO_DATE = $filter('date')(dt, vm.dtFormat);                                                        

                            vm.form.NO_OF_DAYS = moment(dt1).diff(dt, 'days')+1;
                            //console.log(dataItem);
                            
                        }
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

        vm.mailSending = false; 

        vm.sendMailNotification = function (ACC_PAY_PERIOD_ID) {

               vm.mailSending = true;
                $http({
                    method: 'get',
                    url: "/Hr/HrSalaryProcess/FireJobCardNotificationMail?pACC_PAY_PERIOD_ID=" + ACC_PAY_PERIOD_ID
                }).
                success(function (data, status, headers, config) {
                    vm.mailSending = false;
                }).
                error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
        }

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
        
        function religionList() {
            return vm.religionListData = {
                optionLabel: "-- Select Religion --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getLookupListData(2).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };            
        };


        vm.desigGroupGridDataSource = new kendo.data.DataSource({
            pageSize: 2000,
            transport: {
                //autosync: true,
                read: function (e) {
                    $http({
                        method: 'post',
                        url: "/Hr/HrSalaryTran/DesignationGrpListData",  //+ "&pType=" + showType
                        params: { pPARAM_TYPE: 'PARTIAL SALARY PROCESS' }
                    }).success(function (data, status, headers, config) {
                        e.success(data)
                    }).
                    error(function (data, status, headers, config) {
                        alert('something went wrong')
                        console.log(status);
                    });
                }
            }         
        });


        function getDesigGrpList() {            
            $scope.gridOptions = {
                autoBind: true,                
                filterable: true,
                selectable: "row",                
                //sortable: {
                //    mode: "single",
                //    allowUnsort: false
                //},
                height:500,
                pageable: {
                    refresh: true,
                    pageSizes: false//,
                    //buttonCount: 5
                },
                columns: [                    
                    {
                        filterable: false,                        
                        field: "IS_PARAM_ACTIVE",
                        width: "30px",
                        headerTemplate: "<input type='checkbox' ng-model='vm.isParamActive' ng-click='vm.selectAllDesigGroup(vm.isParamActive,0)'> <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Select All?'\"></i>",
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
        

        vm.selectAllDesigGroup = function (v, index) {
            var data = vm.desigGroupGridDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            console.log(data);

            angular.forEach(data, function (val, key) {
                val['IS_PARAM_ACTIVE'] = v;
            });
        };

        //$('#checkAll').click(function () {
        //    if ($(this).attr('checked')) {
        //        $('.check-box').attr('checked', 'checked');
        //    } else {
        //        $('.check-box').removeAttr('checked');
        //    }
        //});

        //vm.allChecked = function (bool) {
            
        //    if (bool) {
        //        alert('t');
        //        $('.check-box').addAttr('checked', 'checked');
        //    } else {
        //        $('.check-box').removeAttr('checked');
        //    }
            
        //};

        //vm.EmployeeDataList = {
        //    optionLabel: "-- Select Employee --",
        //    template: '<span class="text-primary">#= data.EMPLOYEE_CODE #</span>  #if(data.EMP_FULL_NAME_EN!=undefined){#<span>: #= data.EMP_FULL_NAME_EN #</span>#}#',
        //    filter: "startswith",
        //    autoBind: true,
        //    dataSource: {
        //        transport: {
        //            read: function (e) {
        //                HrService.getEmployeeData().then(function (res) {
        //                    e.success(res);
        //                });
        //            }
        //        }
        //    },
        //    dataTextField: "EMPLOYEE_CODE",
        //    dataValueField: "HR_EMPLOYEE_ID"
        //};
        
        vm.salaryProcess = function (form, insert) {
            vm.showSplash = true;

            if (!insert) return;

            var x = confirm("Do you want to process salary?");
            if (x == false) {
                vm.showSplash = false;
                return;
            }

            if (insert) {
                vm.insert = false;                
                
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrSalaryProcess/SalaryProcess',
                    method: 'post',
                    data: {
                        pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pACC_PAY_PERIOD_ID: vm.form.ACC_PAY_PERIOD_ID, pHR_OFFICE_ID: vm.form.HR_OFFICE_ID,
                        pHR_DEPARTMENT_ID: vm.form.HR_DEPARTMENT_ID,
                        pHR_DESIGNATION_ID: null, pHR_SHIFT_TEAM_ID: vm.form.HR_SHIFT_TEAM_ID, pHR_EMPLOYEE_ID: vm.form.HR_EMPLOYEE_ID,
                        pHR_MANAGEMENT_TYPE_ID: vm.form.HR_MANAGEMENT_TYPE_ID, pLK_FLOOR_ID: vm.form.LK_FLOOR_ID, pLINE_NO: vm.form.LINE_NO,
                        pCORE_DEPT_ID: vm.form.CORE_DEPT_ID
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


        vm.downLoad4Bkash = function (formData) {
            var frmData = angular.copy(formData);

            var url = '/Hr/HrReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            frmData['REPORT_CODE'] = 'RPT-1071';
            frmData['IS_EXCEL_FORMAT'] = 'Y';
            frmData['HR_PAY_ELEMENT_ID'] = 1;
            
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

        vm.form.ROUND_TYPE_ID = 2;
        vm.form.ROUND_AMOUNT = 100;
        vm.roundTypeList = [{ ROUND_TYPE_ID: 1, ROUND_TYPE_NAME: 'Up' }, { ROUND_TYPE_ID: 2, ROUND_TYPE_NAME: 'Down' }];

        vm.partSalaryProcess = function (form, insert) {
            vm.showSplash = true;

            if (!insert) return;

            vm.desigGrpData = [];
            vm.desigGrp = $("#desigGrpGrid").data("kendoGrid").dataSource.data();
                        
            angular.forEach(vm.desigGrp, function (val, key) {                
                if (val.IS_PARAM_ACTIVE) {
                    vm.desigGrpData.push(val.HR_DESIGNATION_GRP_ID);
                }
            });

            var x = confirm("Do you want to process partial salary?");
            if (x == false) {
                vm.showSplash = false;
                return;
            }
            
            vm.form['OT_START_DT'] = $filter('date')(vm.form.OT_START_DT, vm.dtFormat);
            vm.form['OT_END_DT'] = $filter('date')(vm.form.OT_END_DT, vm.dtFormat);
            vm.form['HR_DESIGNATION_GRP_IDS'] = vm.desigGrpData.join(',');

            if (insert) {
                vm.insert = false;

                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrSalaryProcess/PartialSalaryProcess',
                    method: 'post',
                    data: {
                        pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pACC_PAY_PERIOD_ID: vm.form.ACC_PAY_PERIOD_ID, pHR_OFFICE_ID: vm.form.HR_OFFICE_ID, pHR_DEPARTMENT_ID: vm.form.HR_DEPARTMENT_ID,
                        pHR_DESIGNATION_ID: null, pHR_SHIFT_TEAM_ID: vm.form.HR_SHIFT_TEAM_ID, pHR_EMPLOYEE_ID: vm.form.HR_EMPLOYEE_ID, pLK_RELIGION_ID: vm.form.LK_RELIGION_ID,
                        pHR_MANAGEMENT_TYPE_ID: vm.form.HR_MANAGEMENT_TYPE_ID, pLK_FLOOR_ID: vm.form.LK_FLOOR_ID, pLINE_NO: vm.form.LINE_NO,
                        pCORE_DEPT_ID: vm.form.CORE_DEPT_ID, pIS_INCLUDE_ADVANCE: vm.form.IS_INCLUDE_ADVANCE, pIS_INCLUDE_OT: vm.form.IS_INCLUDE_OT,
                        pROUND_AMOUNT: vm.form.ROUND_AMOUNT, pROUND_TYPE_ID: vm.form.ROUND_TYPE_ID, pADDL_PRE_DAYS: vm.form.ADDL_PRE_DAYS, pHR_DESIGNATION_GRP_IDS: vm.form.HR_DESIGNATION_GRP_IDS,
                        pOT_START_DT: vm.form.OT_START_DT, pOT_END_DT: vm.form.OT_END_DT
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


        function activate(){
            var promise = [religionList(), getCompanyList(), getOfficeList(), parentDepartmentList(), getSubdepartmentListData(), shiftTeamList(), getManagementTypeList(), floorList(), lineList(), getDesigGrpList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();