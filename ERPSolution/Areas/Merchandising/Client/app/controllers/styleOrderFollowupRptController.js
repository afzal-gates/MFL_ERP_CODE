(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleOrderFollowupRptController', ['$q', '$scope', '$http', '$stateParams', 'MrcRptDataService', StyleOrderFollowupRptController]);
    function StyleOrderFollowupRptController($q, $scope, $http, $stateParams, MrcRptDataService) {
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
        vm.pMONTHOF = ($stateParams.pMONTHOF || '');
        vm.page = ($stateParams.page || 1);
       
        

        function getStyleOrderData() {
            var url = '/api/mrc/Pms/BuyerStyleOrderFollowupList';

            url += '?pageNumber=' + $stateParams.page + '&pageSize=' + $stateParams.pageSize;
            url += '&pMC_BYR_ACC_ID=' + ($stateParams.pMC_BYR_ACC_ID < 1 ? null : $stateParams.pMC_BYR_ACC_ID);
            url += '&pRF_FAB_PROD_CAT_ID=' + ($stateParams.pRF_FAB_PROD_CAT_ID?$stateParams.pRF_FAB_PROD_CAT_ID:2);
            url += '&pMC_ORDER_H_ID_LST=' + ($stateParams.pMC_ORDER_H_ID_LST ? $stateParams.pMC_ORDER_H_ID_LST : '');
            url += '&WITH_SUM=Y'
            if ($stateParams.pFIRSTDATE && $stateParams.pLASTDATE) {
                url += '&pFIRSTDATE=' + $stateParams.pFIRSTDATE;
                url += '&pLASTDATE=' + $stateParams.pLASTDATE;
            }

            return MrcRptDataService.getDataByFullUrl(url).then(function (res) {
                vm.datas = res;



                vm.buyer = res.data.length>0?res.data[0].BYR_ACC_GRP_NAME_EN : '';
            });
        };
    };
})();