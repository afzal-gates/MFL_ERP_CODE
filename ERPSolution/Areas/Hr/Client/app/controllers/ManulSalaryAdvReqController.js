(function () {
    'use strict';
    angular.module('multitex.hr').controller('ManulSalaryAdvReqController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state', '$scope', 'entityService', 'logger', '$modal', ManulSalaryAdvReqController]);
    function ManulSalaryAdvReqController($q, config, HrService, $filter, $http, $stateParams, $state, $scope, entityService, logger, $modal) {

        var vm = this;
        vm.editMode = false;
        vm.disableSave = false;
        vm.RequestDetails = false;
        vm.level = $stateParams.l || 0;
        vm.form = {};

        $scope.format = config.appDateFormat;
        vm.toDay = $filter('date')(new Date(), config.appDateFormat);

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getAdvanceType(), getBoardOfDirectors(), getCompanyData(), getAdvStatus()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        if ($stateParams.i) {
            HrService.getSalaryAdvDataById($stateParams.i, $stateParams.a || 0).then(function (res) {
                        res['JOINING_DT'] = $filter('date')(moment(res.JOINING_DT)._d, config.appDateFormat);
                        res['ADV_RQST_DATE'] = $filter('date')(moment(res.ADV_RQST_DATE)._d, config.appDateFormat);
                        res['ADV_APRV_DATE'] = res.ADV_APRV_DATE ? $filter('date')(moment(res.ADV_APRV_DATE)._d, config.appDateFormat) : null;
                        res['DEDU_ST_DT'] = res.DEDU_ST_DT ? $filter('date')(moment(res.DEDU_ST_DT)._d, config.appDateFormat) : null;
                        res['ADV_APRV_AMT'] = res.ADV_APRV_AMT > 0 ? res.ADV_APRV_AMT : res.ADV_RQST_AMT;
                        res['NO_OF_INSTL'] = res.NO_OF_INSTL ? res.NO_OF_INSTL : null;
                        res['APRV_BY_ID'] = res.APRV_BY_ID;
                        res['NO_OF_SLAB'] = res.items.length == 0 ? 1 : res.items.length;
                        res['IS_MANUAL'] =true;
                        res['IS_SLAB'] = angular.copy(res.items.length);
                        console.log(res);
                        vm.form = res;
                    });


        } else {
            vm.form['LK_ADV_STATUS_ID'] = 123;
            vm.form['LK_ADV_TYPE_ID'] = 146;
            vm.form['ADV_APRV_DATE'] = vm.toDay;
        }

        function getAdvanceType() {
            return HrService.getLookupListData(30).then(function (res) {
                vm.advanceType = res;
                return vm.advanceType;
            });
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

        function getAdvStatus() {
            return HrService.getLookupListData(24).then(function (res) {
                vm.AdvStatus = res;
            });
        }

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
            console.log(item);
            vm.form['JOINING_DT'] = $filter('date')(moment(item.JOINING_DT)._d, config.appDateFormat);
            vm.form['HR_COMPANY_ID'] = item.HR_COMPANY_ID;
            vm.form['DEPARTMENT_NAME_EN'] = item.DEPARTMENT_NAME_EN;
            vm.form['DESIGNATION_NAME_EN'] = item.DESIGNATION_NAME_EN;
            vm.form['EMPLOYEE_CODE'] = item.EMPLOYEE_CODE,            
            vm.form['EMP_FULL_NAME_EN'] = item.EMP_FULL_NAME_EN;
            vm.form['GROSS_SALARY'] = item.GROSS_SALARY;
            vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;
        }

        $scope.ADV_RQST_DATEopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ADV_RQST_DATEopened = true;
        };

        $scope.ADV_APRV_DATEopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ADV_APRV_DATEopened = true;
        };

        $scope.DEDU_ST_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.DEDU_ST_DTopened = true;
        };


        $scope.$watchGroup(['vm.form.ADV_APRV_AMT', 'vm.form.NO_OF_INSTL'], function (NewVal) {
            if (NewVal[0] && NewVal[1]) {
                vm.form['INSTL_AMT'] = (NewVal[0] / NewVal[1]);
            } else {
                vm.form['INSTL_AMT'] = 0;
            }
        });



        vm.saveData = function (data, token, valid) {
            data['items'] = [{ HR_SAL_LN_PAY_SLB_ID: -1, SLAB_NO: 1, NO_OF_INSTL: data.NO_OF_INSTL, INSTL_AMT: data.INSTL_AMT }];
            if (!valid) return;
            data['DEDU_ST_DT'] = moment(data.DEDU_ST_DT).endOf("month")._d;
                $http({
                    method: 'post',
                    url: '/Hr/HrSalAdvance/SaveManualAdvData',
                    data: { SAL_ADV: data },
                    headers: { "RequestVerificationToken": token }
                }).success(function (data, status, headers, config1) {
                    config.appToastMsg(data.MSG);
                    if (data.HR_SAL_ADVANCE_ID > 0) {
                        $state.go('ManualSalaryAdvReq', { i: data.HR_SAL_ADVANCE_ID });
                    } else {
                        $state.go($state.current);
                    }


                }).error(function (data, status, headers, config) {
                        console.log(status);
                });
        }

        $scope.$watchGroup(['vm.form.ADV_RQST_AMT', 'vm.form.HR_SAL_ADVANCE_ID'], function (newVal, oldVal) {
            if (newVal[0] != oldVal[0] && (newVal[1] || 0) == 0) {
                vm.form['ADV_APRV_AMT'] = angular.copy(newVal[0]);
            }
        });


        vm.updateData = function (data, token, valid) {
            if (!valid) return;
            data['DEDU_ST_DT'] = moment(data.DEDU_ST_DT).endOf("month")._d;
            HrService.updateData(data, token).then(function (res) {
                console.log(res);
                if (res.success) {
                    config.appToastMsg(res.vMsg);
                    $state.go('ManualSalaryAdvReq', { i: data.HR_SAL_ADVANCE_ID });
                } else {
                    logger.error('Something Went Wrong');
                }
            });
        }

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