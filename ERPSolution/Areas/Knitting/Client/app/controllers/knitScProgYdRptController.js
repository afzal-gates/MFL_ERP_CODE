(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitScProgYdRptController', ['$q', '$scope', '$http', '$stateParams', 'KnittingDataService', KnitScProgYdRptController]);
    function KnitScProgYdRptController($q, $scope, $http, $stateParams, KnittingDataService) {

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

        function calTotal(data) {
            angular.forEach(data, function (i) {
                i['ttl_fdr'] = 0;
               angular.forEach(i.feeder, function (o) {
                    i.ttl_fdr += o.NO_OF_FDR * (o.NO_OF_REPEAT || 1);
                });
            });
        }


        function getDataFormServer() {
            if (!$stateParams.pKNT_SC_PRG_ISS_ID)
                return;

            KnittingDataService.getDataByUrl('/KnitPlan/getScKnitProgramYd?pKNT_SC_PRG_ISS_ID=' + $stateParams.pKNT_SC_PRG_ISS_ID)
                .then(function (res) {
                    console.log(res);
                    vm.data = res;
                    calTotal(res);
                });
        }

    
    };
})();