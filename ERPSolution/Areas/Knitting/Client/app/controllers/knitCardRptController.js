(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitCardRptController', ['$q', '$scope', '$http', '$stateParams', 'KnittingDataService', KnitCardRptController]);
    function KnitCardRptController($q, $scope, $http, $stateParams, KnittingDataService) {

        var vm = this;
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getDataFormServer()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.ttl_fdr = 0;

       function calTotal(data) {
              data.forEach(function (o) {
                vm.ttl_fdr += o.NO_OF_FDR * (o.NO_OF_REPEAT || 1);
            });
        }

        function getDataFormServer() {
            if (!$stateParams.pKNT_JOB_CRD_H_ID)
                return;

            KnittingDataService.getDataByUrl('/KnitPlan/getJobCardHeaderDataRollProd?pKNT_JOB_CRD_H_ID=' + $stateParams.pKNT_JOB_CRD_H_ID)
                .then(function (res) {
                    vm.data = res;
                    calTotal(res.feeder);
                })
                
                .catch(function (message) {
                    console.error(message);
                });
        }

    
    };
})();