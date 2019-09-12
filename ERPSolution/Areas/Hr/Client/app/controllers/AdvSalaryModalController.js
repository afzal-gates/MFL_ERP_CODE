(function () {
    'use strict';

    angular.module('multitex.hr').controller('AdvSalaryModalController', ['$modalInstance', '$scope', 'item', '$http', '$state', 'config', '$filter', 'logger', 'HrService', AdvSalaryModalController]);

    function AdvSalaryModalController($modalInstance, $scope, item, $http, $state, config, $filter, logger, HrService) {

        $scope.item = item;
        $scope.disableSave = true;
        $scope.sumInstl = 0;
        $scope.sumAmt = 0;
        $scope.save = function (data) {
            data['NO_OF_INSTL'] = $scope.sumInstl;
            var dataSet = [];
            angular.forEach(data.items, function (val, key) {
                if (val.NO_OF_INSTL == val.TOT_INSTL_PAID) {
                    val['IS_CLOSED'] = 'Y';
                } else {
                    val['IS_CLOSED'] = 'N';
                }
                dataSet.push(val);
            });

            data['items'] = dataSet;

            HrService.fileClose(data).then(function (res) {
                if (res.success) {
                    config.appToastMsg(res.vMsg);

                    if (data.IS_MANUAL) {
                        $state.go('ManualSalaryAdvReq', { i: data.HR_SAL_ADVANCE_ID }, { reload: true });
                    } else {
                        $state.go('SalaryAdvReq', { i: data.HR_SAL_ADVANCE_ID, l: 4, a: data.HR_SAL_ADV_APRVL_ID, m: 154 });
                    }

                } else {
                    logger.error('Something Went Wrong');
                }
            });
           // $modalInstance.close(data);
        };


        if ($scope.item.NO_OF_SLAB == 1) {
            $scope.disableSave = false;
        }

        $scope.$watch('item.items', function (newVal, oldVal) {
            var sum_inst = 0;
            var sum_amount = 0;
            angular.forEach(newVal, function (val, key) {
                if (val.CAN_BE_DEL != 1) {
                    sum_inst += val.NO_OF_INSTL;
                    sum_amount += val.NO_OF_INSTL * val.INSTL_AMT;
                }
            });

            $scope.sumInstl = sum_inst;
            $scope.sumAmt = sum_amount;
            if ($scope.item.ADV_APRV_AMT == Math.floor(sum_amount)) {
                $scope.disableSave = false;
            } else {
                $scope.disableSave = true;
            }
        }, true);


        $scope.remove = function (index) {
            if ($scope.item.items[index].HR_SAL_LN_PAY_SLB_ID > 0) {
                $scope.item.items[index].CAN_BE_DEL = 1;
            } else {
                $scope.item.items.splice(index, 1);
            }

        }

        $scope.add = function () {
            var index = 0;
            angular.forEach($scope.item.items, function (val, key) {
                if (val.TOT_INSTL_PAID > 0) {
                    $scope.item.items[index].NO_OF_INSTL = val.TOT_INSTL_PAID;
                    $scope.item.items[index].IS_CLOSED = 'Y';
                }
                index = index + 1;
            })
            $scope.item.items.push({ HR_SAL_LN_PAY_SLB_ID: -1, SLAB_NO: $scope.item.items.length + 1, NO_OF_INSTL: 1, INSTL_AMT: null });

        }


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();