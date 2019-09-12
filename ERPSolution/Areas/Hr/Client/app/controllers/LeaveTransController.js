(function () {
    'use strict';
    angular.module('multitex.hr').controller('LeaveTransController', ['$q', '$scope', 'config', 'HrService', '$filter', '$http', '$state', '$stateParams', 'logger', 'sc_role_id', LeaveTransController]);
    function LeaveTransController($q, $scope, config, HrService, $filter, $http, $state, $stateParams, logger, sc_role_id) {

        var vm = this;
        vm.LeaveBalance = 0;
        vm.LeaveTypeData = [];
        $scope.format = config.appDateFormat;
        vm.toDay = $filter('date')(new Date(), config.appDateFormat);
        vm.maxDate = $filter('date')(moment().endOf("year")._d, config.appDateFormat);


        if (!angular.equals({}, $stateParams.LeaveTrans)) {
            //alert('xx');
            $stateParams.LeaveTrans['LK_LV_STATUS_ID_ORG'] = $stateParams.LeaveTrans['LK_LV_STATUS_ID'];
            $stateParams.LeaveTrans['EDD_DT'] = $stateParams.LeaveTrans.EDD_DT ? $stateParams.LeaveTrans.EDD_DT : vm.toDay;
            $stateParams.LeaveTrans['APPROVE_DATE'] = $stateParams.LeaveTrans.APPROVE_DATE ? $stateParams.LeaveTrans.APPROVE_DATE : vm.toDay;
        }

        vm.form = angular.equals({}, $stateParams.LeaveTrans) ? { EDD_DT: vm.toDay, FROM_DATE: vm.toDay, TO_DATE: vm.toDay, APPLY_DATE: vm.toDay, APPROVE_DATE: vm.toDay, IS_DAY_OR_HR: 'Y', LK_LV_STATUS_ID: 70 } : $stateParams.LeaveTrans;
        vm.errors = {};
        vm.disableSubmit = true;
        vm.dateRangeError = false;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getLeaveTypeData(),getLookupListData(), getFiscalYearData(), getCompanyData()];
            return $q.all(promise).then(function () {
                //vm.form = angular.equals({}, $stateParams.LeaveTrans) ? { FROM_DATE: vm.toDay,TO_DATE: vm.toDay, IS_DAY_OR_HR: 'Y', HR_COMPANY_ID: null, RF_FISCAL_YEAR_ID: null, HR_LEAVE_TYPE_ID: null } : $stateParams.LeaveTrans;
                vm.showSplash = false;
            });
        }

     

        $scope.NEXT_JOIN_DATEopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.NEXT_JOIN_DATEopened = true;
        };

        $scope.FROM_DATEopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.FROM_DATEopened = true;
        };

        $scope.TO_DATEopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TO_DATEopened = true;
        };

        $scope.EDD_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.EDD_DTopened = true;
            setTimeout(function () {
                $scope.EDD_DTopened = false;
            }, 300);
        };


        $scope.APPLY_DATEopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.APPLY_DATEopened = true;
        };

        $scope.APPROVE_DATEopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.APPROVE_DATEopened = true;
        };

        $scope.$watchGroup(['vm.form.HR_LEAVE_TYPE_ID','vm.form.EDD_DT'], function (newVal, oldVal) {
            if (newVal[0] && angular.isObject(newVal[1])) {
                vm.form['FROM_DATE'] = $filter('date')(moment(newVal[1]).subtract(55, 'days')._d, config.appDateFormat);
                vm.form['TO_DATE'] = $filter('date')(moment(newVal[1]).add(56, 'days')._d, config.appDateFormat);
            }
        });

        $scope.dateTimeNow = function () {
            $scope.date = new Date();
        };
        $scope.dateTimeNow();

        $scope.toggleMinDate = function () {
            $scope.minDate = $scope.minDate ? null : new Date();
        };


        $scope.dateOptions = {
            startingDay: 6,
            showWeeks: false
        };

        $scope.hourStep = 1;
        $scope.minuteStep = 10;

        $scope.timeOptions = {
            hourStep: [1, 2, 3],
            minuteStep: [1, 5, 10, 15, 25, 30]
        };

        $scope.showMeridian = true;
        $scope.timeToggleMode = function () {
            $scope.showMeridian = !$scope.showMeridian;
        };

        $scope.resetHours = function () {
            $scope.date.setHours(1);
        };


        vm.submitData = function (url, update) {

            if (vm.form.HR_LEAVE_TYPE_ID != 4) {
                vm.form['EDD_DT'] = null;
            }

            $http({
                method: 'post',
                url:url,
                data: vm.form,
                headers: { "RequestVerificationToken": vm.antiForgeryToken }
            }).success(function (res, status, headers, config1) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.form.LK_LV_STATUS_ID_ORG = 153 // Reject after Approved
                        vm.form.HR_LEAVE_TRANS_ID = res.data['OP_HR_LEAVE_TRANS_ID'];
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).
            error(function (data, status, headers, config) {
                console.log(status);
            });

        };
 



        $scope.$watchGroup(['vm.form.HR_COMPANY_ID', 'vm.form.RF_FISCAL_YEAR_ID', 'vm.form.HR_EMPLOYEE_ID', 'vm.form.HR_LEAVE_TYPE_ID'], function (newVal, oldval) {
            if (newVal[0] && newVal[1] && newVal[2] && newVal[3] && vm.LeaveTypeData) {


                if (newVal[0] && newVal[1] && newVal[2] && newVal[3] == 5) {
                    var data = [];
                    var obj = { HR_COMPANY_ID: newVal[0], RF_FISCAL_YEAR_ID: newVal[1], HR_EMPLOYEE_ID: newVal[2], HR_LEAVE_TYPE_ID: newVal[3] };
                    HrService.getOffDayLeaveData(obj).then(function (res) {
                        angular.forEach(res, function (val, key) {
                            val['ATTEN_DATE'] = $filter('date')(moment(val.ATTEN_DATE)._d, config.appDateFormat);
                            val['CLK_IN_WT'] = $filter('date')(moment(val.CLK_IN_WT)._d, 'mediumTime');
                            val['CLK_OUT_WT'] = $filter('date')(moment(val.CLK_OUT_WT)._d, 'mediumTime');
                            data.push(val);
                        });
                        vm.offDayLeaveData = data;
                    });
                }

                if (angular.isDefined(vm.employee)){
                        HrService.getLeaveBalanceByEmplType(newVal[0], newVal[1], newVal[2], newVal[3]).then(function (res) {
                            vm.LeaveBalance = res;
                            if (vm.LeaveTypeData.length > 0) {
                                var a = vm.LeaveTypeData[findWithAttr(vm.LeaveTypeData, 'HR_LEAVE_TYPE_ID', newVal[3])].IS_BAL_CHQ;
                            }

                            console.log('=====');
                            console.log(res);
                            console.log(vm.employee);

                            if (newVal[3] == 4 && vm.employee['LK_GENDER_ID'] == 39) {
                                logger.warning('You are not applicable to apply');
                                vm.disableSubmit = true;
                            }
                            if (res < 1 && a == 'Y') {
                                vm.disableSubmit = true;
                                logger.warning('You don’t have enough balance')

                            } else if (res != 0 && a == 'Y') {
                                vm.disableSubmit = false;
                            }
                            
                        });
                }

            }

        });



        $scope.$watchGroup(['vm.form.HR_COMPANY_ID', 'vm.form.RF_FISCAL_YEAR_ID', 'vm.form.TO_DATE', 'vm.form.HR_EMPLOYEE_ID'], function (newVal, oldval) {
            if (newVal[0] != null && newVal[1] != null && newVal[2] != null && newVal[3] != null) {
                return HrService.findNextWorkingDay(newVal[0], newVal[1], newVal[2], newVal[3]).then(function (res) {
                    vm.form['NEXT_JOIN_DATE'] = $filter('date')(moment(res)._d, config.appDateFormat);
                });
            }

        });


        $scope.$watchGroup(['vm.form.HR_COMPANY_ID', 'vm.form.RF_FISCAL_YEAR_ID', 'vm.form.HR_LEAVE_TYPE_ID', 'vm.form.FROM_DATE', 'vm.form.TO_DATE', 'vm.form.IS_DAY_OR_HR','vm.form.HR_EMPLOYEE_ID'], function (newVal, oldval) {
            
            if (moment(newVal[3]).isBefore(moment(newVal[4])) || moment(newVal[3]).isSame(moment(newVal[4]))) {
                  vm.dateRangeError = false;
                  if (newVal[0] != null && newVal[1] != null && newVal[2] != null && newVal[5] == 'Y' && newVal[6] != null) {
                      return HrService.DateDiff(newVal[0], newVal[1], newVal[2], newVal[3], newVal[4], newVal[6]).then(function (res) {

                          vm.form['NO_DAYS_APPL'] = res.DateDiff;
                        vm.dateRangeError = false;
                        vm.disableSubmit = false;
                    });
                }
            } else {
                vm.dateRangeError = true;
                vm.disableSubmit = true;
            }
        });






        function getLeaveTypeData() {
            var LeaveTypeList = (sc_role_id == 10) ? [4] : [1,2,3,4,5,6,7];
            return HrService.getLeaveTypeData().then(function (res) {
                return vm.LeaveTypeData = res.filter(function (o) { return  LeaveTypeList.indexOf(o.HR_LEAVE_TYPE_ID) > -1 });
            });
        }


        vm.onChangeLeaveType = function (item) {
            var a = vm.LeaveTypeData[findWithAttr(vm.LeaveTypeData, 'HR_LEAVE_TYPE_ID', item)].IS_BAL_CHQ;
            console.log(a);

            vm.form.EMPLOYEE_CODE = null;
            vm.form.HR_EMPLOYEE_ID = null;
            vm.showEmployeeInfo = false;

            vm.showBalance = (a == 'Y') ? true : false;
            vm.disableSubmit = (a == 'Y') ? true : false;
            if (item == 4) {
                vm.form['LK_LV_STATUS_ID'] = 69;
            } 
        }

        $scope.$watch('vm.form.IS_DAY_OR_HR', function (newVal, OldVal) {
            if (newVal == 'N') {
                vm.form['TO_DATE'] = vm.toDay;
            }
        });




        function getCompanyData() {
            return HrService.getCompanyData().then(function (res) {
                vm.form['HR_COMPANY_ID'] = res[0]['HR_COMPANY_ID'];
                vm.companyData = res;
            });
        }



        function getFiscalYearData() {
            return HrService.getDataByFullUrl('/api/common/GetPayFiscalYear?pIS_CLOSED=N').then(function (res) {
                vm.FiscalYearData = res;
                return vm.form['RF_FISCAL_YEAR_ID'] = res[0]['RF_FISCAL_YEAR_ID'];
            });
        }


        function getLookupListData() {
            return   HrService.getLookupListData(17).then(function (res) {
                vm.leaveStatusList = res;
            });
        }



        $scope.emoloyeeAuto = function (val, HR_LEAVE_TYPE_ID) {
            var a = 'N';
            if (HR_LEAVE_TYPE_ID && vm.LeaveTypeData) {
                var a = vm.LeaveTypeData[findWithAttr(vm.LeaveTypeData, 'HR_LEAVE_TYPE_ID', HR_LEAVE_TYPE_ID)].IS_FEMALE;                
            } 

            var LK_GENDER_ID = (a == 'Y') ? 40 : null;
            return $http.get('/Hr/hrleavetrans/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val,
                    LK_GENDER_ID: LK_GENDER_ID
                }
            }).then(function (response) {
                return response.data;
            });

        };

        $scope.onSelectItem = function (item) {
            vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;
            vm.showEmployeeInfo = true;
            vm.employee = item;
        }


        $scope.onSelectItem2 = function (item) {
            vm.onDutyEmployee = item;
            vm.form['ON_DUTY_EMP_ID'] = item.HR_EMPLOYEE_ID;
        }





        function getEmployeeDataById(){
            return HrService.getEmployeeDataById().then(function (res) {
                return vm.currentEmployeeId = res.HR_EMPLOYEE_ID;
            });
        }

        function getEmployeeData(){
            return HrService.getEmployeeData().then(function (res) {
                return vm.EmployeeDataList1=res;
            });
        }


        function findWithAttr(array, attr, value) {
            for (var i = 0; i < array.length; i += 1) {
                if (array[i][attr] === value) {
                    return i;
                }
            }
        }


    };

})();