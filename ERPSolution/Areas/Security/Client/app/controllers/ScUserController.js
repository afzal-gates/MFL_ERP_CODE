(function () {
    'use strict';

    angular.module('multitex.security').controller('ScUserController', ['$state', 'logger', 'config', '$q', '$scope', '$http', '$stateParams', '$filter', '$window', 'SecurityService', ScUserController]);

    function ScUserController($state, logger, config, $q, $scope, $http, $stateParams, $filter, $window, SecurityService) {

        var vm = this;

        $scope.format = config.appDateFormat;
        vm.currentDate = $filter('date')(new Date(), config.appDateFormat);

        showMessageLoading();

        vm.form = $stateParams.data == null ? {} : angular.extend($stateParams.data, {
            MEMORABLE_TEXT_REPEATED: $stateParams.data.MEMORABLE_TEXT,
            USER_EXPIRE_ON: $filter('date')(moment($stateParams.data.USER_EXPIRE_ON)._d, config.appDateFormat)

        });
        $scope.pw = '';
        vm.errors = {};

        vm.showEmployeeInfo = false;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;


        function showMessageLoading() {
            SecurityService.getDataByUrl('/GlobalNotify/GetMsg').then(function (data) {
                if (data.page > 0) {
                    if (Date.parse(data.msgTimeOut) > Date.parse(vm.today)) {
                        $("#alert-placeholder").empty();
                        var alertHtml = '<div class="alert alert-' + htmlEncode(data.alertType) + ' " style="color:red; opacity: 0.95;" alert-dismissible" role="alert"> <i class="fa fa-' + htmlEncode(data.alertType) + '" ></i> <button type="button" class="close" data-dismiss="alert"><span>×</span></button><strong>' + htmlEncode(data.title) + '</strong> <br />' + htmlEncode(data.vMsg) + '</div>';

                        $(alertHtml)
                          .hide()                           //hide the newly created element (this is required for fadeIn to work)
                          .appendTo('#alert-placeholder')   //add it to the palceholder in the page
                          .fadeIn(1500);                      //little flair to grab user attention

                        setTimeout(function () {
                            $("#alert-placeholder").empty();
                        }, 600000);
                    }

                }
                else {
                    $window.location.href = '/Security/Security/serverMaintenance/#/serverMaintenance';
                }
            });
        }


        vm.submit = function (isValid, form) {

            //console.log(insert);
            //alert('hello1');

            //if (!isValid) return;

            //alert('ok');

        }

        vm.changeCaptcha = function () {
            $http({ method: 'get', url: '/Security/ScUser/CaptchaImage' });

            $state.go('SignInCaptcha');
            $state.reload();
        };

        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }


        $scope.USER_EXPIRE_ONopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.USER_EXPIRE_ONopened = true;
        };


        //function getEmployeeDataList() {
        //    return vm.EmployeeDataList = {
        //        optionLabel: "Select Employee",
        //        template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
        //        filter: "startswith",
        //        select: onSelect,
        //        autoBind: true,
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    SecurityService.getEmployeeList(null, 30, null, null).then(function (res) {
        //                        console.log(res);
        //                        e.success(res);
        //                    });
        //                }
        //            }
        //        },
        //        dataTextField: "EMPLOYEE_CODE",
        //        dataValueField: "HR_EMPLOYEE_ID"
        //    };

        //};


        //function onSelect(e) {
        //    var dataItem = this.dataItem(e.item);
        //    vm.employee = dataItem;
        //    vm.form['USER_NAME_EN'] = dataItem.EMP_FULL_NAME_EN;
        //    vm.showEmployeeInfo = true;
        //}






        //function getData(){
        //     return AdminService.getData().then(function (data) {
        //         //vm.schedulerOptions.dataSource.data = data;
        //         //return vm.schedulerOptions.dataSource.data;

        //    });
        //}

        $scope.$watch('vm.form.MEMORABLE_TEXT_REPEATED', function (newVal, OldVal) {
            if (vm.form.MEMORABLE_TEXT_REPEATED != vm.form.MEMORABLE_TEXT) {
                vm.errors.MEMORABLE_TEXT_REPEATED = ["Memorable Word does not match"];
            } else {
                vm.errors.MEMORABLE_TEXT_REPEATED = false
            }

        });

        //SecurityService.getUserDataList().then(function (res) {
        //    console.log(res);
        //});




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
            vm.employee = item;
            vm.form['USER_NAME_EN'] = item.EMP_FULL_NAME_EN;
            vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;

            vm.showEmployeeInfo = true;
        }


        $scope.getEmployeeData = function (val) {
            return $http.get('/Hr/HrEmployee/EmployeeListData', {
                params: {
                    pHR_DEPARTMENT_ID: null,
                    pLK_JOB_STATUS_ID: 30,
                    pEMPLOYEE_CODE: null,
                    pOLD_EMP_CODE: null
                }
            }).then(function (response) {
                return response.data.map(function (item) {
                    return item.EMPLOYEE_CODE;
                });
            });
        };


        vm.SaveChangePassword = function (isValid, form) {
            if (!isValid) return;

            $http({
                headers: { "RequestVerificationToken": vm.antiForgeryToken },
                url: '/Security/ScUser/SaveChangePassword',
                method: 'post',
                data: { ob: form }
            }).success(function (data, status, headers, config1) {
                vm.errors = [];
                if (data.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    if (data.vMsg.substr(0, 9) == 'MULTI-001') {
                        vm.form.PASSWORD_HASH_OLD = '';
                        vm.form.PASSWORD_HASH = '';
                        vm.form.CONFIRMED_PASSWORD = '';
                    };

                    config.appToastMsg(data.vMsg);
                    //console.log(vm.clockData);
                }
            }).error(function (data, status, headers, config) {
                alert('something went wrong')
                console.log(status);
            });

        }


        vm.submitData = function (isValid) {
            if (!isValid) return;
            return SecurityService.saveUser(vm.form, $scope.pw, vm.antiForgeryToken).then(function (res) {
                if (!res.data.success) {
                    vm.errors = res.data.errors;
                    return;
                } else {
                    config.appToastMsg(res.data.vMsg);
                    $state.go("UserEntry", { data: null });
                }

            }, function (res) {
                console.log(res.errors);
            });
        }


        vm.updateData = function (isValid) {
            //console.log(vm.form);
            //if (!isValid) return;
            //alert('t');
            return SecurityService.updateUser(vm.form, $scope.pw, vm.antiForgeryToken).then(function (res) {
                if (!res.data.success) {
                    vm.errors = res.data.errors;
                    return;
                } else {
                    config.appToastMsg(res.data.vMsg);
                    $state.go("UserList");
                }

            }, function (res) {
                console.log(res.errors);
            });
        }

    }



})();