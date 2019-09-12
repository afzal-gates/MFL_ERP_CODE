(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeingBatchProgramRptController', ['$q', 'DyeingDataService', '$stateParams', '$state', '$scope', DyeingBatchProgramRptController]);
    function DyeingBatchProgramRptController($q, DyeingDataService, $stateParams, $state, $scope) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getProgramData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function getProgramData() {
           return DyeingDataService.getDataByUrl('/DyeBatchPlan/loadEventDataRpt?pDYE_BATCH_SCDL_ID=' + $stateParams.pDYE_BATCH_SCDL_ID).then(function (res) {
                vm.datas = res;
            });
        }
    }



})();