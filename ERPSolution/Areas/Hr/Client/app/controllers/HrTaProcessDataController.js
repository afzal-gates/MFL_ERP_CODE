(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrTaProcessDataController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', 'Hub', '$rootScope', 'HrService', HrTaProcessDataController]);

    function HrTaProcessDataController(logger, config, $q, $scope, $http, exception, $filter, $state, Hub, $rootScope, HrService) {

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

            vm.form.HR_DEPARTMENT_ID=''; 
            vm.form.HR_SHIFT_TEAM_ID = '';
            vm.form.LK_FLOOR_ID = '';
            vm.form.LINE_NO = '';
            vm.form.HR_MANAGEMENT_TYPE_ID = '';
            vm.form.HR_EMPLOYEE_ID = '';

            //if (newVal != oldVal) {
            if ($scope.SELECTION_TYPE == 1) {
                vm.isSubDepartment = false;
                vm.isTeam = false;
                vm.isFloor = false;
                vm.isEmployee = false;
                vm.isManagementType = false;
            }


            if ($scope.SELECTION_TYPE == 2) {
                vm.isSubDepartment = true;
            }
            else {
                vm.isSubDepartment = false;
            }
            if ($scope.SELECTION_TYPE == 3) {
                vm.isTeam = true;
            }
            else {
                vm.isTeam = false;
            }
            if ($scope.SELECTION_TYPE == 4) {
                vm.isFloor = true;
            }
            else {
                vm.isFloor = false;
            }
            if ($scope.SELECTION_TYPE == 5) {
                vm.isManagementType = true;
            }
            else {
                vm.isManagementType = false;
            }
            if ($scope.SELECTION_TYPE == 6) {
                vm.isEmployee = true;
            }
            else {
                vm.isEmployee = false;
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

        function floorList() {
            return $http({
                url: '/Hr/HrEmployee/LookupListData',
                method: 'get',
                params: {
                    pLookupTableId: 18,
                },
            })
                .then(function (result) {
                    vm.floorListData = result.data;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        function lineList() {
            vm.lineListData = [{ LINE_NO: 0 }];
            
            for (var i = 0; i < 50; i++) {
                vm.lineListData[i] = { LINE_NO: i + 1 };
            }            
        };

        function shiftTeamList() {
            return $http({
                url: '/Hr/HrEmployee/ShiftTeamListData',
                method: 'get',
                params: { pHR_SHIFT_PLAN_ID: null }
            })
                .then(function (result) {
                    vm.shiftTeamListData = result.data;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        function departmentList() {
            return $http({
                url: '/Hr/HrEmployee/DeptListData',
                method: 'get',
                params: { pPARENT_ID: null }
            })
            .then(function (result) {
                vm.departmentListData = result.data;
                return;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };
        
        vm.shiftPlanListData = {
            optionLabel: "-- Select Shift Plan --",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrEmployee/ShiftPlanListData"  //+ "&pType=" + showType
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
            change: function shiftTeamList() {
                var vShiftID = this.value();
                //alert(vShiftID);
                return $http({
                    url: '/Hr/HrEmployee/ShiftTeamListData',
                    method: 'get',
                    params: { pHR_SHIFT_PLAN_ID: vShiftID }
                })
                    .then(function (result) {
                        vm.shiftTeamListData = result.data;
                        vm.form.HR_SHIFT_TEAM_ID = vm.shiftTeamListData[0].HR_SHIFT_TEAM_ID;
                        return;
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
            },
            dataTextField: "SHIFT_PLAN_NAME_EN",
            dataValueField: "HR_SHIFT_PLAN_ID"
        };


        vm.parentDepartmentListData = {
            optionLabel: "-- Select Section --",
            filter: "startswith",
            autoBind: true,            
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
            change: function () {

                var vParentID = this.value();

                if (vParentID != null) {

                    return $http({
                        url: '/Hr/HrEmployee/DeptListData',
                        method: 'get',
                        params: { pPARENT_ID: vParentID }
                    })
                    .then(function (result) {
                        vm.departmentListData = result.data;
                        vm.form.HR_DEPARTMENT_ID = vm.departmentListData[0].HR_DEPARTMENT_ID;
                        return;
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                }

            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        vm.managementTypeList = {
            optionLabel: "-- Select Management Type --",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/Admin/HrDesignation/ManagementTypeListData",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "MNG_TYPE_NAME_EN",
            dataValueField: "HR_MANAGEMENT_TYPE_ID"
        };

        

        vm.EmployeeDataList = {
            optionLabel: "-- Select Employee --",
            template: '<span class="text-primary">#= data.EMPLOYEE_CODE #</span>  #if(data.EMP_FULL_NAME_EN!=undefined){#<span>: #= data.EMP_FULL_NAME_EN #</span>#}#',
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        HrService.getEmployeeData().then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "EMPLOYEE_CODE",
            dataValueField: "HR_EMPLOYEE_ID"
        };
        
        //vm.attenProgressBar = 0;
        //vm.isProcessing = 'N';        
        //var myVar = setInterval(function () { myTimer() }, 1000);
        //function myTimer() {            
        //    vm.isProcessing = 'N';
        //    //$("#progressbar").die();
        //    //$("#progressbar").live();
            

        //    return HrService.getAttenProcessProgressBar().then(function (res) {
        //        vm.isProcessing = 'Y';
        //        //alert(res);                
        //        vm.attenProgressBar = res;               
        //        return vm.attenProgressBar;
        //    });

        //    $scope.$apply();
        //    $rootScope.$apply();
        //};
        
        
        vm.attendanceProcess = function (form, insert) {
            vm.showSplash = true;

            if (!insert) return;

            var x = confirm("Are you want to process attendance data?");
            if (x == false) {
                vm.showSplash = false;
                return;
            }            

            //vm.isProcessing = 'Y';

            if (insert) {
                vm.insert = false;                
                
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrTaProcessData/AttendanceProcess',
                    method: 'post',
                    data: { pFromDate:vm.form.FROM_DATE, pToDate:vm.form.TO_DATE, pHR_DEPARTMENT_ID:vm.form.HR_DEPARTMENT_ID,
                        pHR_DESIGNATION_ID: null, pHR_SHIFT_TEAM_ID: vm.form.HR_SHIFT_TEAM_ID, pHR_EMPLOYEE_ID: vm.form.HR_EMPLOYEE_ID,
                        pHR_MANAGEMENT_TYPE_ID: vm.form.HR_MANAGEMENT_TYPE_ID, pLK_FLOOR_ID: vm.form.LK_FLOOR_ID, pLINE_NO: vm.form.LINE_NO,
                        pHR_DAY_TYPE_ID: vm.form.DAY_TYPE, pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pHR_OFFICE_ID: vm.form.HR_OFFICE_ID
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

                //vm.isProcessing = 'N';

            }
        };

        //============== Start Progress Bar =======================
        var hub = new Hub('dashboard', {
            listeners: {
                'newConnection': function (id) {
                    vm.connected.push(id);
                    $rootScope.$apply();
                },
                'removeConnection': function (id) {
                    $rootScope.$apply();
                },
                'AttenProcessProgressBar': function () {
                    AttenProcessProgressBar();
                    $rootScope.$apply()
                }                
            },
            methods: [],
            errorHandler: function (error) {
                console.error(error);
            }
        });

        vm.connected = [];

        
        function AttenProcessProgressBar() {
            return HrService.getAttenProcessProgressBar().then(function (res) {
                //var data = [];
                //angular.forEach(res, function (val, key) {
                //    val['LAST_UPDATE_DATE'] = moment(val.LAST_UPDATE_DATE).fromNow();
                //    data.push(val)
                //});
                //console.log(res);
                //console.log(res.data);

                vm.attenProgressBar = res;
                return vm.attenProgressBar;
            })
        }
        //============== Start Progress Bar =======================


        

        function activate(){
            var promise = [getCompanyList(), getOfficeList(), floorList(), shiftTeamList(), departmentList(), lineList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();