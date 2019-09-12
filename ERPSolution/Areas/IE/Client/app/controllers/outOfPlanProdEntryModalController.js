
(function () {
    'use strict';
    angular.module('multitex.ie').controller('OutOfPlanProdEntryModalController', ['$q', 'config', 'IeDataService', '$modalInstance', '$scope', 'plan_datas', 'LineData', OutOfPlanProdEntryModalController]);
    function OutOfPlanProdEntryModalController($q, config, IeDataService, $modalInstance, $scope, plan_datas, LineData) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
        $scope.plan_datas = plan_datas;
        $scope.LINE_CODE = LineData.LINE_CODE;

        $scope.ProdList = [];
        $scope.getSummeryData = function (data) {
            $scope.data = data;
            return IeDataService.getDataByFullUrl('/api/ie/GmtIeTarget/SewPlanProd4OutOfPlan?pGMT_PLN_LINE_LOAD_ID=' + data.GMT_PLN_LINE_LOAD_ID + '&pGMT_PROD_PLN_CLNDR_ID=' + LineData.GMT_PROD_PLN_CLNDR_ID + '&pHR_PROD_LINE_ID=' + LineData.HR_PROD_LINE_ID).then(function (res) {
                 $scope.ProdList = res;
            });
        };

        $scope.save = function (data) {

            return IeDataService.saveDataByUrl({
                GMT_PROD_PLN_CLNDR_ID: LineData.GMT_PROD_PLN_CLNDR_ID,
                GMT_PLN_LINE_LOAD_ID: data.GMT_PLN_LINE_LOAD_ID,
                HR_PROD_LINE_ID: LineData.HR_PROD_LINE_ID,
                HR_PROD_FLR_ID: data.HR_PROD_FLR_ID,
                PROD_QTY: (data.PROD_QTY||0),
                GMT_PLN_CLNDR_MN_ID: LineData.GMT_PLN_CLNDR_MN_ID
            }, '/GmtIeTarget/SaveOutOfPlanProd').then(function (res) {
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
