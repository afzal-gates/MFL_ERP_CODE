
(function () {
    'use strict';
    angular.module('multitex.ie').controller('OrderAllocationCnfModalController', ['$q', 'config', 'IeDataService', '$modalInstance', '$scope', 'plan_datas', 'V_GMT_PROD_PLN_CLNDR_ID', 'V_LINE_CODE', OrderAllocationCnfModalController]);
    function OrderAllocationCnfModalController($q, config, IeDataService, $modalInstance, $scope, plan_datas, V_GMT_PROD_PLN_CLNDR_ID, V_LINE_CODE) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

        $scope.data = [];
        $scope.plan_datas = plan_datas;

        $scope.LINE_CODE = V_LINE_CODE;

        $scope.getSummeryData = function (pGMT_PLN_LINE_LOAD_ID) {
            return IeDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getEventDataForTuning?pGMT_PLN_LINE_LOAD_ID=' + pGMT_PLN_LINE_LOAD_ID).then(function (res) {
                $scope.data = res;
            });
        };

        $scope.save = function (data) {
            var data2bSave = [];
            angular.forEach(data, function (val) {
                data2bSave.push(
                        {
                            GMT_PLN_LINE_LOAD_ID: val.GMT_PLN_LINE_LOAD_ID,
                            HR_PROD_LINE_ID: val.HR_PROD_LINE_ID,
                            IS_USED_IN_TRGT: val.IS_USED_IN_TRGT,
                        }
                    );
            });
            return IeDataService.saveDataByUrl({
                XML: config.xmlStringShort(data2bSave), pOption: 1000, GMT_PROD_PLN_CLNDR_ID: V_GMT_PROD_PLN_CLNDR_ID
            }, '/GmtIeTarget/Save').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $modalInstance.close();
                    }
                    config.appToastMsg(res.data.PMSG);
                }

            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
            
        }
    }

})();
