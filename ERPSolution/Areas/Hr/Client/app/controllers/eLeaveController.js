(function () {
    'use strict';
    angular.module('multitex.hr').controller('eLeaveController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state', '$scope', 'logger', '$modal', 'entityService', eLeaveController]);
    function eLeaveController($q, config, HrService, $filter, $http, $stateParams, $state, $scope, logger, $modal, entityService) {

        var vm = this;
        vm.LeaveBalance = 0;
        vm.IsAddMoreDoc = false;
        vm.slFileAreaShow = false;
        vm.level = $stateParams.l || 0;
        vm.message = $stateParams.m || 0;
        vm.LeaveTypeData = [];
        $scope.format = config.appDateFormat;
        vm.toDay = $filter('date')(new Date(), config.appDateFormat);
        vm.maxDate = $filter('date')(moment().endOf("year")._d, config.appDateFormat);

        vm.disableSubmit = true;

        $scope.alerts = [];

        $scope.addAlert = function (msg) {
            $scope.alerts.push({ msg:msg});
        };

        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        vm.form = angular.equals({}, $stateParams.LeaveTrans) ? { FROM_DATE: vm.toDay, TO_DATE: vm.toDay, APPLY_DATE: vm.toDay, APPROVE_DATE: vm.toDay, IS_DAY_OR_HR: 'Y',IS_CONFIRM_JOIN:'Y',items:[] } : $stateParams.LeaveTrans;
        vm.errors = {};
        //vm.disableSubmit = false;
        vm.dateRangeError = false;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getEmployeeDataById(),getLeaveTypeData(), getLookupListData(), getFiscalYearData(), getCompanyData(), blockSection()];
            return $q.all(promise).then(function () {
                //vm.form = angular.equals({}, $stateParams.LeaveTrans) ? { FROM_DATE: vm.toDay,TO_DATE: vm.toDay, IS_DAY_OR_HR: 'Y', HR_COMPANY_ID: null, RF_FISCAL_YEAR_ID: null, HR_LEAVE_TYPE_ID: null } : $stateParams.LeaveTrans;
                vm.showSplash = false;
            });
        }


        function blockSection() {
            if ($stateParams.l && $stateParams.l == 1) {
                vm.RequestDetails = true;
                vm.dtRange = true;
            } else if ($stateParams.l && $stateParams.l == 2) {
                vm.RequestDetails = true;
                vm.dtRange = false;
            } else if ($stateParams.l && $stateParams.l == 3) {
                vm.RequestDetails = true;
                vm.dtRange = true;
            } else if ($stateParams.l && $stateParams.l == 4) {
                vm.RequestDetails = true;
                vm.dtRange = false;
            }
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
            }, 10);
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

        $scope.$watchGroup(['vm.form.HR_LEAVE_TYPE_ID', 'vm.form.EDD_DT'], function (newVal, oldVal) {
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


        vm.saveDataForEleave = function (data, key) {

            var getModelAsFormData = function (data) {
                var dataAsFormData = new FormData();
                angular.forEach(data, function (value, key) {
                    dataAsFormData.append(key, value);
                });
                return dataAsFormData;
            };
            angular.forEach(data.items, function (val, k) {
                console.log(val);
                $http({
                    method: 'post',
                    url: '/Hr/HrLeaveTrans/uploadImage',
                    data:getModelAsFormData(val),
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined, "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    console.log(status);
                }).
                  error(function (data, status, headers, config) {
                      console.log(status);
                  });
            });




            $http({
                method: 'post',
                url: '/Hr/HrLeaveTrans/saveDataForEleave',
                data: {ob:data,pOption:2001},
                headers: { "RequestVerificationToken": key }
            }).success(function (data, status, headers, config1) {
                config.appToastMsg(data.MSG);
                if (data.HR_LEAVE_TRANS_ID == 0) {
                    return;
                } else {
                    $state.go('eleave', { i: data.HR_LEAVE_TRANS_ID }, { reload: true });
                }
                  
            }).
            error(function (data, status, headers, config) {
                console.log(status);
            });
        };

        vm.updateDataForEleave = function (data, antiForkey) {

            var getModelAsFormData = function (data) {
                var dataAsFormData = new FormData();
                angular.forEach(data, function (value, key) {
                    dataAsFormData.append(key, value);
                });
                return dataAsFormData;
            };

            angular.forEach(data.items, function (val, k) {
                $http({
                    method: 'post',
                    url: '/Hr/HrLeaveTrans/uploadImage',
                    data: getModelAsFormData(val),
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined, "RequestVerificationToken": antiForkey }
                }).success(function (data, status, headers, config1) {
                    console.log(status);
                }).
                  error(function (data, status, headers, config) {
                      console.log(status);
                  });
            });


            $http({
                method: 'post',
                url: '/Hr/HrLeaveTrans/saveDataForEleave',
                data: { ob: data, pOption: 2002 },
                headers: { "RequestVerificationToken": antiForkey }
            }).success(function (data, status, headers, config1) {
                config.appToastMsg(data.MSG);
                $state.go('eleave', { i: data.HR_LEAVE_TRANS_ID }, { reload: true });
            }).
            error(function (data, status, headers, config) {
                console.log(status);
            });
        };


        vm.applyForEleave = function (data, key) {
            $http({
                method: 'post',
                url: '/Hr/HrLeaveTrans/saveDataForEleave',
                data: { ob: data, pOption: 2003 },
                headers: { "RequestVerificationToken": key }
            }).success(function (data, status, headers, config1) {
                config.appToastMsg(data.MSG);
                //$state.go('eleave', { i: data.HR_LEAVE_TRANS_ID }, { reload: true });
                $state.go('UserDashBoard');

            }).
            error(function (data, status, headers, config) {
                console.log(status);
            });
        };


        vm.sendApplication = function (data, key) {
            $http({
                method: 'post',
                url: '/Hr/HrLeaveTrans/saveDataForEleave',
                data: { ob: data, pOption: 2011 },
                headers: { "RequestVerificationToken": key }
            }).success(function (data, status, headers, config1) {
                config.appToastMsg(data.MSG);
                $state.go('UserDashBoard');

            }).
            error(function (data, status, headers, config) {
                console.log(status);
            });
        }

        vm.changeNoOfDaysLeave = function (data, key, action) {
            data['LK_LV_STATUS_ID'] = action;
            $http({
                method: 'post',
                url: '/Hr/HrLeaveTrans/saveDataForEleave',
                data: { ob: data, pOption: 2012 },
                headers: { "RequestVerificationToken": key }
            }).success(function (data, status, headers, config1) {
                config.appToastMsg(data.MSG);
                $state.go('UserDashBoard');

            }).
            error(function (data, status, headers, config) {
                console.log(status);
            });
        }



        vm.recomend2 = function (data, key, status) {
                data['LK_LV_STATUS_ID'] = status;
                $http({
                    method: 'post',
                    url: '/Hr/HrLeaveTrans/saveDataForEleave',
                    data: { ob: data, pOption: 2005 },
                    headers: { "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    config.appToastMsg(data.MSG);
                    $state.go('UserDashBoard');

                }).
                error(function (data, status, headers, config) {
                    console.log(status);
                });
        }

        vm.approve4 = function (data, key, status) {
                data['LK_LV_STATUS_ID'] = status;
                $http({
                    method: 'post',
                    url: '/Hr/HrLeaveTrans/saveDataForEleave',
                    data: { ob: data, pOption: 2006 },
                    headers: { "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    config.appToastMsg(data.MSG);
                    $state.go('UserDashBoard');
                }).
                error(function (data, status, headers, config) {
                    console.log(status);
                });
        }





        vm.saveDataBySecHeadForEleave = function (data, key) {
                $http({
                    method: 'post',
                    url: '/Hr/HrLeaveTrans/saveDataForEleave',
                    data: { ob: data, pOption: 2004 },
                    headers: { "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    config.appToastMsg(data.MSG);
                    $state.go('eleave', { i: data.HR_LEAVE_TRANS_ID,l:data.APROVER_LVL_NO,a:data.HR_LEAVE_APRVL_ID,m:data.LK_LV_STATUS_ID }, { reload: true });

                }).
                error(function (data, status, headers, config) {
                    console.log(status);
                });
        }

        vm.addToGrid = function (data) {
            data['DOC_PATH_REF'] = new Date().getTime() + getExtention(data.ATT_FILE.name);
            vm.form.items.push(data);
            if (vm.slFileAreaShow && vm.form.items.length>0) {
                vm.disableSubmit = false;
            } else {
                vm.disableSubmit = true;
            }
            vm.file = {};
        }

        vm.removeAddedDoc = function (index) {
            if (vm.form.items[index].HR_SL_DOCS_ID > 0) {
                vm.form.items[index].REMOVE = 'Y';
            } else {
                vm.form.items.splice(index, 1);
            }


            if (vm.slFileAreaShow && vm.form.items.length > 0) {
                vm.disableSubmit = false;
            } else {
                vm.disableSubmit = true;
            }

        }

       function getExtention(fileName) {
            var i = fileName.lastIndexOf('.');
            if (i === -1) return false;
            return fileName.slice(i)
        }


        //vm.approvedByTopManagementForEleave = function (data, key, message) {
        //    data['LK_LV_STATUS_ID'] = message;
        //    console.log(data);
        //    $http({
        //        method: 'post',
        //        url: '/Hr/HrLeaveTrans/saveDataForEleave',
        //        data: { ob: data, pOption: 2007 },
        //        headers: { "RequestVerificationToken": key }
        //    }).success(function (data, status, headers, config1) {
        //        config.appToastMsg(data.MSG);
        //        $state.go('UserDashBoard');

        //    }).
        //    error(function (data, status, headers, config) {
        //        console.log(status);
        //    });
        //}

       $scope.$watchGroup(['vm.form.HR_COMPANY_ID', 'vm.form.RF_FISCAL_YEAR_ID', 'vm.form.HR_EMPLOYEE_ID', 'vm.form.HR_LEAVE_TYPE_ID'], function (newVal, oldval) {
            if (newVal[3]) {
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
                
                vm.showBalanceErr = false;

                HrService.getLeaveBalanceByEmplType(newVal[0], newVal[1], newVal[2], newVal[3]).then(function (res) {
                    vm.LeaveBalance = res;

                    //console.log(res);
                    //console.log(vm.LeaveTypeData);

                    if (vm.LeaveTypeData.length > 0) {
                        var a = vm.LeaveTypeData[findWithAttr(vm.LeaveTypeData, 'HR_LEAVE_TYPE_ID', newVal[3])].IS_BAL_CHQ;
                        //console.log(a);
                    }

                    if (newVal[3]==4 && vm.employee.LK_GENDER_ID==39 ){
                        logger.warning('You are not applicable to apply');
                        vm.disableSubmit = true;
                        vm.showBalanceErr = true;
                    }
                    else if (res <= 0 && a == 'Y') {
                        vm.disableSubmit = true;
                        vm.showBalanceErr = true;
                        logger.warning('You don’t have enough balance')

                    }
                    else if (res > 0 && a == 'Y') {
                        vm.disableSubmit = false;
                        vm.showBalanceErr = false;
                    }
                });
            }

        });


        //$scope.$watch('vm.form.FROM_DATE', function (newVal, Oldval) {
        //if (newVal != Oldval) {
        //    vm.form['TO_DATE'] = null;
        //}
        //});

        $scope.$watchGroup(['vm.form.HR_COMPANY_ID', 'vm.form.RF_FISCAL_YEAR_ID', 'vm.form.TO_DATE','vm.form.HR_EMPLOYEE_ID'], function (newVal, oldval) {
            if (newVal[0] != null && newVal[1] != null && newVal[2] != null && newVal[3] != null) {
                return HrService.findNextWorkingDay(newVal[0], newVal[1], newVal[2], newVal[3]).then(function (res) {
                    vm.form['NEXT_JOIN_DATE'] = $filter('date')(moment(res)._d, config.appDateFormat);
                });
            }

        });


        $scope.$watch('vm.slFileAreaShow', function (newVal, oldVal) {
            if (newVal) {
                    if (vm.form.items.length>0) {
                        vm.disableSubmit = false;
                    } else {
                        vm.disableSubmit = true;
                    }
            }
        });

        function onChangeLeaveType(item, oldItem) {
            //if (!oldItem) {
            //    return false;
            //}

            console.log(item);
            console.log(oldItem);
            console.log(vm.LeaveTypeData);

            var aaa = vm.LeaveTypeData[findWithAttr(vm.LeaveTypeData, 'HR_LEAVE_TYPE_ID', item)].IS_BAL_CHQ;
            //console.log(aaa);
            vm.showBalanceErr = (aaa == 'Y') ? true : false;
        }


        

        $scope.$watchGroup(['vm.form.HR_COMPANY_ID', 'vm.form.RF_FISCAL_YEAR_ID', 'vm.form.HR_LEAVE_TYPE_ID', 'vm.form.FROM_DATE', 'vm.form.TO_DATE', 'vm.form.IS_DAY_OR_HR', 'vm.form.HR_EMPLOYEE_ID'], function (newVal, oldval) {
            if (moment(newVal[3]).isBefore(moment(newVal[4])) || moment(newVal[3]).isSame(moment(newVal[4]))) {
                vm.dateRangeError = false;
                if (newVal[0] != null && newVal[1] != null && newVal[2] != null && newVal[5] == 'Y' && newVal[6] != null) {
                    return HrService.DateDiff(newVal[0], newVal[1], newVal[2], newVal[3], newVal[4], newVal[6]).then(function (res) {
                        vm.form['NO_DAYS_APPL'] = res.DateDiff;
                        vm.dateRangeError = false;
                        //vm.disableSubmit = false;
                        //vm.showBalanceErr = false;

                        if (res.DateDiff > vm.LeaveBalance) {
                            vm.disableSubmit = true;
                            vm.showBalanceErr = true;
                        }
                        else {
                            vm.disableSubmit = false;
                            vm.showBalanceErr = false;
                        }

                        //if (newVal[2] != oldval[2] && oldval[2]!=undefined && oldval[2]!=null) {
                        //    if (res.DateDiff > vm.LeaveBalance) {
                        //        //alert(newVal[2] + '-' + oldval[2]);
                        //        //onChangeLeaveType(newVal[2], oldval[2]);
                        //        vm.disableSubmit = true;
                        //    } else {
                        //        vm.disableSubmit = false;
                        //        vm.showBalanceErr = false;
                        //    }
                        //}

                        if (res.IS_APLY_TYM_EXP == 'Y') {
                            vm.applyDtExpireErr = true;
                        } else {
                            vm.applyDtExpireErr = false;
                        }
                     
                        if (newVal[2] == 2 && res.DateDiff >= 3) {
                            vm.slFileAreaShow =true;
                        } else {
                            vm.slFileAreaShow = false;
                        }
                    });
                }
            } else {
                vm.dateRangeError = true;
                vm.disableSubmit = true;
            }



        });

        function getLeaveTypeData() {
            return HrService.getLeaveTypeData().then(function (res) {
                return vm.LeaveTypeData = res;
            });
        }

        $scope.$watch('vm.form.IS_DAY_OR_HR', function (newVal, OldVal) {
            if (newVal == 'N') {
                vm.form['TO_DATE'] = vm.toDay;
            }
        });




        function getCompanyData() {
            return HrService.getCompanyData().then(function (res) {
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
            return HrService.getLookupListData(17).then(function (res) {
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

        $scope.$watch('vm.form.EMPLOYEE_CODE2', function (newVal, oldVal) {
            if (newVal == '') {
                vm.onDutyEmployee = false;
                vm.form['ON_DUTY_EMP_ID'] = null;
            }
        });

        $scope.onSelectItem = function (item) {
            vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;
            vm.showEmployeeInfo = true;
            vm.employee = item;
        }


        $scope.onSelectItem2 = function (item) {
            vm.onDutyEmployee = item;
            vm.form['ON_DUTY_EMP_ID'] = item.HR_EMPLOYEE_ID;
        }





        function getEmployeeDataById() {

            if ($stateParams.i) {
                return HrService.getLeaveDataById($stateParams.i, $stateParams.a || null).then(function (res) {

                    console.log(res);

                    res['NEXT_JOIN_DATE'] = $filter('date')(moment(res.NEXT_JOIN_DATE)._d, config.appDateFormat);
                    res['FROM_DATE'] = $filter('date')(moment(res.FROM_DATE)._d, config.appDateFormat);
                    res['TO_DATE'] = $filter('date')(moment(res.TO_DATE)._d, config.appDateFormat);
                    res['APPLY_DATE'] = $filter('date')(moment(res.APPLY_DATE)._d, config.appDateFormat);
                    res['APPROVE_DATE'] = $filter('date')(moment(res.APPROVE_DATE)._d, config.appDateFormat);
                    res['SC_MENU_ID'] = $scope.SC_MENU_ID;
                    res['LAST_LV_FROM_DATE'] = res.LAST_LV_FROM_DATE ? $filter('date')(moment(res.LAST_LV_FROM_DATE)._d, config.appDateFormat) : null;
                    res['LAST_LV_TO_DATE'] = res.LAST_LV_TO_DATE ? $filter('date')(moment(res.LAST_LV_TO_DATE)._d, config.appDateFormat) : null;
                    res['items'] = res.items;
                    res['HR_COMPANY_ID'] = (res.HR_COMPANY_ID||null)

                    vm.form = res;
                    vm.showEmployeeInfo = true;
                    vm.employee = res;
                
                });
               
            } else {
                return HrService.getEmployeeDataByUserId().then(function (res) {
                    console.log(res);
                    vm.form['HR_OFFICE_ID'] = res.HR_OFFICE_ID;
                    vm.form['HR_DEPARTMENT_ID'] = res.HR_DEPARTMENT_ID;
                    vm.form['DESIG_ORDER'] = res.DESIG_ORDER;
                    vm.form['LK_FLOOR_ID'] = res.LK_FLOOR_ID;
                    vm.form['LINE_NO'] = res.LINE_NO
                    vm.form['SC_MENU_ID'] = $scope.SC_MENU_ID;
                    vm.form['EMPLOYEE_CODE'] = res.EMPLOYEE_CODE;
                    vm.form['HR_EMPLOYEE_ID'] = res.HR_EMPLOYEE_ID;
                    vm.form['HR_COMPANY_ID'] = (res.HR_COMPANY_ID||null);

                    res['LAST_LV_FROM_DATE'] =res.LAST_LV_FROM_DATE? $filter('date')(moment(res.LAST_LV_FROM_DATE)._d, config.appDateFormat):null;
                    res['LAST_LV_TO_DATE'] =res.LAST_LV_TO_DATE? $filter('date')(moment(res.LAST_LV_TO_DATE)._d, config.appDateFormat):null;
                    vm.showEmployeeInfo = true;
                    vm.employee = res;
                });
            }



        }

        function getEmployeeData() {
            return HrService.getEmployeeData().then(function (res) {
                return vm.EmployeeDataList1 = res;
            });
        }


        //vm.AddMoreDoc = function (data,isInsert) {
        //    vm.IsAddMoreDoc = true;
        //    vm.form['HR_SL_DOCS_ID'] = data.HR_SL_DOCS_ID;
        //    vm.form['DOC_PATH_REF'] = data.DOC_PATH_REF;
        //    vm.form['DOC_NAME_EN'] = data.DOC_NAME_EN;
        //    vm.isInsert = isInsert;
        //}


        vm.uploadDocsMore = function (data,key) {
            return entityService.saveTutorial(data, '/Hr/HrLeaveTrans/uploadDocsMore', key)
                .then(function (data, status, headers, config1) {
                    vm.errors = undefined;
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        config.appToastMsg(data.MSG);
                        $state.go('eleave', { i: data.HR_LEAVE_TRANS_ID }, { reload: true });
                    }

                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

        }

        vm.updateUploadDocs = function (data, key) {
            return entityService.saveTutorial(data, '/Hr/HrLeaveTrans/updateUploadDocs', key)
                .then(function (data, status, headers, config1) {
                    vm.errors = undefined;
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        config.appToastMsg(data.MSG);
                        $state.go('eleave', { i: data.HR_LEAVE_TRANS_ID }, { reload: true });
                    }

                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        vm.deleteUploadDocs = function (data, HR_SL_DOCS_ID,DOC_PATH_REF,key) {
            data['HR_SL_DOCS_ID'] = HR_SL_DOCS_ID;
            data['DOC_PATH_REF'] = DOC_PATH_REF;
            return entityService.saveTutorial(data, '/Hr/HrLeaveTrans/deleteUploadDocs', key)
                .then(function (data, status, headers, config1) {
                    vm.errors = undefined;
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        config.appToastMsg(data.MSG);
                        $state.go('eleave', { i: data.HR_LEAVE_TRANS_ID }, { reload: true });
                    }

                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }



        function findWithAttr(array, attr, value) {
            for (var i = 0; i < array.length; i += 1) {
                if (array[i][attr] === value) {
                    return i;
                }
            }
        }


        vm.showLeaveBalance = function (HR_COMPANY_ID, RF_FISCAL_YEAR_ID,HR_EMPLOYEE_ID) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'myModalContent.html',
                controller: 'LeaveBalanceModalController',
                windowClass: 'large-Modal',
                resolve: {
                    items: function (HrService) {
                        return HrService.getLeaveBalance({
                            HR_COMPANY_ID: HR_COMPANY_ID,
                            RF_FISCAL_YEAR_ID: RF_FISCAL_YEAR_ID,
                            HR_EMPLOYEE_ID: HR_EMPLOYEE_ID
                        });

                       
                    },
                }
            });

            modalInstance.result.then(function (selectedItem) {
                $scope.selected = selectedItem;
            }, function () {
                console.log('Dismissed');
            });
        };


        vm.attendanceStatus = function (HR_EMPLOYEE_ID, FROM_DATE, TO_DATE) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'attendanceStatus.html',
                controller: 'LvAttendanceStatusController',
                windowClass: 'large-Modal',
                resolve: {
                    items: function (HrService) {
                        return HrService.getAttendanceStatus({
                            HR_EMPLOYEE_ID: HR_EMPLOYEE_ID,
                            FROM_DATE: FROM_DATE,
                            TO_DATE: TO_DATE
                        });


                    },
                }
            });

            modalInstance.result.then(function (selectedItem) {
                $scope.selected = selectedItem;
            }, function () {
                console.log('Dismissed');
            });
        };

    }

})();