(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitYdStatementRptController', ['$q', '$scope', '$http', '$stateParams', 'KnittingDataService', KnitYdStatementRptController]);
    function KnitYdStatementRptController($q, $scope, $http, $stateParams, KnittingDataService) {
        var vm = this;
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getStyleOrderData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.toDay = new Date();
        vm.params = $stateParams;

        function getStyleOrderData() {
            var url = '/KnitYdProgram/getKntStatementData';
            url += '?pageNumber=' + $stateParams.pageNo + '&pageSize=' + $stateParams.pageSize;



            url += '&pRF_FAB_PROD_CAT_ID=' + ($stateParams.pRF_FAB_PROD_CAT_ID || null);

            url += '&pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || null);
            url += '&pMC_FAB_PROD_ORD_H_ID=' + ($stateParams.pMC_FAB_PROD_ORD_H_ID || null);
            url += '&pMC_BYR_ACC_ID=' + ($stateParams.pMC_BYR_ACC_ID || null);
            url += '&pPCT_DONE_CODE=' + ($stateParams.pPCT_DONE_CODE || null);

            if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                url += '&pLASTDATE=' + $stateParams.pLASTDATE;
            }

            return KnittingDataService.getDataByUrl(url).then(function (res) {
                vm.datas = res.data;
            });
        };
    };
})();