(function () {
    'use strict';
    angular.module('multitex.hr').controller('AdvSalaryController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state', '$scope', '$modal', 'logger', AdvSalaryController]);
    function AdvSalaryController($q, config, HrService, $filter, $http, $stateParams, $state, $scope, $modal, logger) {

        var vm = this;
        vm.disableNoOfInstallment = false;
        vm.disableAdvReqAmount = false;
        vm.saveChanges = false;
        vm.RequestDetails = false;
        vm.advType = true;
        vm.level = $stateParams.l || 0;

        vm.form = {};

        $scope.format = config.appDateFormat;

        vm.toDay = $filter('date')(new Date(), config.appDateFormat);



        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getAdvanceType(), getCompanyData(), getEmployeeDataByUserId(), blockSection(), getBoardOfDirectors()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function blockSection() {
            if ($stateParams.l && $stateParams.l == 1) {
                vm.RequestDetails = true;
                vm.disableNoOfInstallment = true;
                vm.disableAdvReqAmount = true;
            } else if ($stateParams.l && $stateParams.l == 2) {
                vm.RequestDetails = true;
                vm.disableNoOfInstallment = true;
                vm.disableAdvReqAmount = true;

            } else if ($stateParams.l && $stateParams.l == 3) {
                vm.RequestDetails = true;
                vm.disableNoOfInstallment = false;
                vm.disableAdvReqAmount = false;

            } else if ($stateParams.l && $stateParams.l == 4) {
                vm.RequestDetails = true;
                vm.disableNoOfInstallment = true;
                vm.disableAdvReqAmount = true;
            }

        }


        function getAdvanceType() {

            vm.advanceType = [{
                LK_DATA_NAME_EN: 'Advance From Salary',
                LOOKUP_DATA_ID : 147
            }];
            //return HrService.getLookupListData(30).then(function (res) {
            //    vm.advanceType = [];
            //});
        }

        function getCompanyData() {
            return HrService.getCompanyData().then(function (res) {
                vm.companyData = res;
            });
        }

        function getBoardOfDirectors() {
            return HrService.getBoardOfDirectors().then(function (res) {
                vm.getBoardOfDirectors = res;
            });
        }


        function getEmployeeDataByUserId() {

            if ($stateParams.i) {
                vm.editMode = true;
                return HrService.getSalaryAdvDataById($stateParams.i, $stateParams.a || 0).then(function (res) {
                    res['JOINING_DT'] = $filter('date')(moment(res.JOINING_DT)._d, config.appDateFormat);
                    res['ADV_RQST_DATE'] = $filter('date')(moment(res.ADV_RQST_DATE)._d, config.appDateFormat);
                    res['ADV_APRV_DATE'] = res.ADV_APRV_DATE ? $filter('date')(moment(res.ADV_APRV_DATE)._d, config.appDateFormat) : null;
                    res['DEDU_ST_DT'] = res.DEDU_ST_DT ? $filter('date')(moment(res.DEDU_ST_DT)._d, config.appDateFormat) : null;

                    res['ADV_APRV_AMT'] = res.ADV_APRV_AMT > 0 ? res.ADV_APRV_AMT : res.ADV_RQST_AMT;

                    res['NO_OF_INSTL'] = res.NO_OF_INSTL ? res.NO_OF_INSTL : null;
                    res['MAX_NO_INSTL'] = res.MAX_NO_INSTL ? res.MAX_NO_INSTL : null;

                    res['NO_OF_SLAB'] = res.items.length == 0 ? 1 : res.items.length;
                    res['IS_SLAB'] = angular.copy(res.items.length);

                    res['MAX_SAL'] = (res.SALARY_FOR_APPLY * res.GROSS_SALARY);

                    if (res.APRV_BY_ID == 0) {
                        res['APRV_BY_ID'] = res.APPROVER_ID;
                    }
                    res['IS_MANUAL'] = false;
                    res['SC_MENU_ID'] = $scope.SC_MENU_ID;
                    res['HR_SAL_ADV_APRVL_ID'] = $stateParams.a || 0;
                    res['LK_ADV_STATUS_ID'] = $stateParams.m || 0;
                    vm.form = res;
                });

            } else {
                return HrService.getEmployeeDataByUserId().then(function (res) {

                    var dt = moment(res.JOINING_DT)._d;
                    res['NO_OF_INSTL'] = res.NO_OF_INSTALLMENT;
                    //res['LK_ADV_TYPE_ID'] = 146;
                    res['MAX_SAL'] = (res.SALARY_FOR_APPLY * res.GROSS_SALARY);
                    res['JOINING_DT'] = $filter('date')(dt, config.appDateFormat);
                    res['ADV_RQST_DATE'] = $filter('date')(moment(res.ADV_RQST_DATE)._d, config.appDateFormat);
                    res['SC_MENU_ID'] = $scope.SC_MENU_ID;
                    vm.form = res;
                });
            }

        }

        $scope.DEDU_ST_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.DEDU_ST_DTopened = true;
        };

        $scope.$watchGroup(['vm.form.ADV_APRV_AMT', 'vm.form.NO_OF_INSTL'], function (newVal, oldVal) {


            if (newVal[0] != oldVal[0] && newVal[1]) {
                vm.form['INSTL_AMT'] = (newVal[0] / newVal[1]);
            } else {
                vm.form['INSTL_AMT'] = 0;
            }
        });


        $scope.$watchGroup(['vm.form.ADV_RQST_AMT', 'vm.form.NO_OF_INSTL', 'vm.form.ADV_APRV_AMT'], function (newVal, oldVal) {

            if (vm.level == 0) {
                if (newVal[0] != oldVal[0] && newVal[1]) {
                    vm.form['INSTL_AMT'] = (newVal[0] / newVal[1]);
                }
            } else {
                if (newVal[0] != oldVal[0] && newVal[1] && newVal[0] == newVal[2]) {
                    vm.form['INSTL_AMT'] = (newVal[0] / newVal[1]);
                }
            }


        });






        $scope.$watch('vm.form.LK_ADV_TYPE_ID', function (newVal, oldVal) {
            if (newVal != oldVal) {
                if (newVal == 147) {
                    vm.form['NO_OF_INSTL'] = 1;
                    vm.form['DEDU_ST_DT'] = moment(new Date()).endOf("month")._d;
                    vm.disableNoOfInstallment = true;
                    vm.RequestDetails = false;
                } else {
                    if (vm.level == 0) {

                        if (vm.form.DSG_GRP_ORDER && !(vm.form.DSG_GRP_ORDER <= vm.form.MIN_LEVEL)) {
                            vm.RequestDetails = true;
                            vm.advType = false;
                            vm.disableNoOfInstallment = true;
                            logger.info('Minimum Sr. Executive position is required to apply');
                        }
                        if (vm.form.MIN_LEVEL && moment().diff(vm.form.JOINING_DT, 'years', true) <= vm.form.REQ_YEARS) {
                            vm.RequestDetails = true;
                            vm.advType = false;
                            vm.disableNoOfInstallment = true;
                            logger.info('Minimum 2 years service required to apply');
                        }

                        vm.disableNoOfInstallment = false;
                    } else if (vm.level == 3) {
                        vm.disableNoOfInstallment = false;
                    } else {
                        vm.disableNoOfInstallment = true;
                    }

                }
            }
        });


        $scope.$watch('vm.form.NO_OF_INSTL', function (newVal, oldVal) {
            if (newVal != oldVal) {
                if (vm.level == 0) {
                    if (newVal > vm.form.MAX_NO_INSTL) {
                        $scope.AdvSalForm['NO_OF_INSTL'].$setValidity("max", false);
                    } else {
                        $scope.AdvSalForm['NO_OF_INSTL'].$setValidity("max", true);
                    }

                }
            }

        });

        $scope.$watchGroup(['vm.form.INSTL_AMT', 'vm.form.DEDU_ST_DT', 'vm.form.ADV_RQST_AMT', 'vm.form.ADV_APRV_AMT'], function (newVal, oldVal) {
            if (parseInt(newVal[0]) == (parseInt(oldVal[0]) || parseInt(newVal[0])) && newVal[1] == (oldVal[1] || newVal[1]) && newVal[2] == (oldVal[2] || newVal[2]) && newVal[3] == (oldVal[3] || newVal[3])) {
                vm.saveChanges = false;
            } else {
                vm.saveChanges = true;

            }
        });

      

        vm.saveData = function (data, token, valid) {
            if (!valid) return;
            data['DEDU_ST_DT'] = moment(data.DEDU_ST_DT).endOf("month")._d;
            data['ADV_APRV_AMT'] = angular.isDefined(data.ADV_APRV_AMT) ? data.ADV_APRV_AMT : data.ADV_RQST_AMT
            HrService.saveData(data, token).then(function (res) {
                config.appToastMsg(res.MSG);
                if (res.HR_SAL_ADVANCE_ID > 0) {
                    $state.go('SalaryAdvReq', { i: res.HR_SAL_ADVANCE_ID }, { reload: true });
                } else {
                    $state.go($state.current);
                }
            });
        }


        vm.updateData = function (data, token, valid) {
            if (!valid) return;
            data['LK_ADV_STATUS_ID'] = null;
            data['DEDU_ST_DT'] = moment(data.DEDU_ST_DT).endOf("month")._d;
            data['ADV_APRV_AMT'] = data.ADV_RQST_AMT;

            HrService.updateData(data, token).then(function (res) {
                if (res.success) {
                    config.appToastMsg(res.vMsg);
                    $state.go('SalaryAdvReq', { i: data.HR_SAL_ADVANCE_ID }, { reload: true });
                } else {
                    logger.error('Something Went Wrong');
                }
            });
        }

        vm.submitData = function (data, token, valid) {
            if (!valid) return;

            data['DEDU_ST_DT'] = moment(data.DEDU_ST_DT).endOf("month")._d;
            data['APROVER_LVL_NO'] = 0;
            HrService.submitData(data, token).then(function (res) {
                if (res.success) {
                    config.appToastMsg(res.MSG);
                    $state.go('UserDashBoard');
                } else {
                    logger.error('Something Went Wrong');
                }
            });
        }



        vm.saveDataForSectionHead = function (data, token, valid) {
            if (!valid) return;

            data['DEDU_ST_DT'] = moment(data.DEDU_ST_DT).endOf("month")._d;

            HrService.saveDataForSectionHead(data, token).then(function (res) {
                if (res.success) {
                    config.appToastMsg(res.vMsg);
                    $state.go($state.current, $stateParams, { reload: true });
                } else {
                    logger.error('Something Went Wrong');
                }
            });
        }



        vm.advApprove = function (data, key, LK_ADV_STATUS_ID) {
            data['APROVER_LVL_NO'] = vm.level;
            data['LK_ADV_STATUS_ID'] = LK_ADV_STATUS_ID;
            $http({
                method: 'post',
                url: '/hr/HrSalAdvance/submitData',
                data: data,
                headers: { "RequestVerificationToken": key }
            }).success(function (data, status, headers, config1) {
                config.appToastMsg(data.MSG);
                $state.go('UserDashBoard');
            }).
            error(function (data, status, headers, config) {
                console.log(status);
            });
        }

        vm.submitDataForSectionHead = function (data, token, valid) {
            if (!valid) return;
            console.log(data);
            HrService.submitDataForSectionHead(data, token).then(function (res) {
                if (res.success) {
                    config.appToastMsg(res.vMsg);
                    $state.go('UserDashBoard');
                } else {
                    logger.error('Something Went Wrong');
                }
            });
        }

        $scope.$watch('vm.form.ADV_RQST_AMT', function (newVal, oldVal) {
            if (newVal != oldVal) {
                if (newVal > vm.form.MAX_SAL) {

                    if (vm.level == 0) {
                        $scope.AdvSalForm['IS_SUBMIT'].$setValidity("IS_SUBMIT", false);
                        logger.info('You can not apply for amount more than your two months salary');
                    } else if (vm.level == 3) {
                        $scope.AdvSalForm['IS_SUBMIT'].$setValidity("IS_SUBMIT", true);
                    }
                } else {
                    $scope.AdvSalForm['IS_SUBMIT'].$setValidity("IS_SUBMIT", true);
                }
            }
        });


        $scope.$watch('vm.form.NO_OF_SLAB', function (newVal, oldVal) {
            console.log(newVal);
            if (newVal != oldVal && vm.form.IS_SLAB == 0) {
                vm.form.items = [];
            }
        });


        $scope.openModalForMakingSchedule = function (data) {
            if (data.items.length == 0 && data.NO_OF_SLAB == 1) {
                data.items.push({ HR_SAL_LN_PAY_SLB_ID: -1, SLAB_NO: 1, NO_OF_INSTL: data.NO_OF_INSTL, INSTL_AMT: data.INSTL_AMT });
            } else if (data.items.length == 0 && data.NO_OF_SLAB > 1) {
                var i = angular.copy(data.NO_OF_SLAB);
                var j = 1;
                while (0 < i) {
                    data.items.push({ HR_SAL_LN_PAY_SLB_ID: -1 * j, SLAB_NO: j, NO_OF_INSTL: null, INSTL_AMT: null });
                    i = i - 1;
                    j++;
                }
            }

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'AdvSalaryModal.html',
                controller: 'AdvSalaryModalController',
                windowClass: 'large-Modal',
                resolve: {
                    item: function () {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };



        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };


    }

})();