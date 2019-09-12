(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrBonusProcessController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', 'HrService', 'access_token', 'Dialog', HrBonusProcessController]);

    function HrBonusProcessController(logger, config, $q, $scope, $http, exception, $filter, $state, HrService, access_token, Dialog) {

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
        vm.form.FB_LIMIT_DT = $filter('date')(vm.today, vm.dtFormat);
                       
                        
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
            }
            else if ($scope.SELECTION_TYPE == 2) {
                vm.isSubDepartment = true;                
                vm.isTeam = false;
                vm.isFloor = false;
                vm.isEmployee = false;
                vm.isManagementType = false;
            }
            else if ($scope.SELECTION_TYPE == 3) {
                vm.isSubDepartment = false;
                vm.isTeam = true;
                vm.isFloor = false;
                vm.isEmployee = false;
                vm.isManagementType = false;
            }
            else if ($scope.SELECTION_TYPE == 4) {
                vm.isSubDepartment = false;
                vm.isTeam = false;
                vm.isFloor = true;
                vm.isEmployee = false;
                vm.isManagementType = false;
            }
            else if ($scope.SELECTION_TYPE == 5) {
                vm.isSubDepartment = false;
                vm.isTeam = false;
                vm.isFloor = false;
                vm.isEmployee = false;
                vm.isManagementType = true;
            }
            else if ($scope.SELECTION_TYPE == 6) {
                vm.isSubDepartment = false;
                vm.isTeam = false;
                vm.isFloor = false;
                vm.isEmployee = true;
                vm.isManagementType = false;
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
                                        headers: { 'Authorization': 'Bearer ' + access_token },
                                        method: 'get',
                                        url: "/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + vHR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=3&pIS_CLOSED=N"                                        
                                    }).
                                    success(function (data, status, headers, config) {
                                        e.success(data);
                                    }).
                                    error(function (data, status, headers, config) {
                                        alert('something went wrong')
                                        console.log(status);
                                    });
                                }
                            }
                        },
                        dataTextField: "MONTH_YEAR_NAME",
                        dataValueField: "ACC_PAY_PERIOD_ID"
                    });
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
                dataValueField: "LOOKUP_DATA_ID"
            };
        };
        

        

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
        
        vm.bonusProcess = function (form, insert) {
            vm.showSplash = true;

            if (!insert) return;

            //Dialog.alert('You are going to delete', 'Are you sure?', ['Ok'])
            //.then(function () {
            //    ///
            //});            

            var x = confirm("Do you want to process bonus?");
            if (x == false) {
                vm.showSplash = false;
                return;
            }

            if (insert) {
                vm.insert = false;                
                
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrBonusProcess/BonusProcess',
                    method: 'post',
                    data: {
                        pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pACC_PAY_PERIOD_ID: vm.form.ACC_PAY_PERIOD_ID, pHR_DEPARTMENT_ID: vm.form.HR_DEPARTMENT_ID,
                        pHR_DESIGNATION_ID: null, pHR_SHIFT_TEAM_ID: vm.form.HR_SHIFT_TEAM_ID, pHR_EMPLOYEE_ID: vm.form.HR_EMPLOYEE_ID,
                        pHR_MANAGEMENT_TYPE_ID: vm.form.HR_MANAGEMENT_TYPE_ID, pLK_FLOOR_ID: vm.form.LK_FLOOR_ID, pLINE_NO: vm.form.LINE_NO,
                        pCORE_DEPT_ID: vm.form.CORE_DEPT_ID, pLK_FB_TYPE_ID: vm.form.LK_FB_TYPE_ID, pFB_LIMIT_DT: vm.form.FB_LIMIT_DT
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
            var promise = [getCompanyList(), getBonusTypeListData(), parentDepartmentList(), getSubdepartmentListData(), shiftTeamList(), getManagementTypeList(), floorList(), lineList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();