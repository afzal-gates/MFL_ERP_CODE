(function () {
    'use strict';
    angular.module('multitex.mrc').controller('InqItemListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', InqItemListController]);
    function InqItemListController($q, config, MrcDataService, $stateParams, $state, $scope, logger) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        vm.Style = $scope.$parent.StyleData;
    
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        if (vm.Style.HAS_SET == 'Y' && vm.Style.HAS_COMBO == 'N') {
               MrcDataService.getDataByUrl('/StyleItem/ItemListForSetWithNoCombo/' + $stateParams.MC_STYLE_ID).then(function (res) {
                    vm.allItemList = res;
                }, function (err) {
                    console.log(err);
                });

        } else if (vm.Style.HAS_SET == 'N' && vm.Style.HAS_COMBO == 'N') {

                MrcDataService.getDataByUrl('/StyleItem/ItemListForOtherWithNoCombo/' + $stateParams.MC_STYLE_ID).then(function (res) {
                    vm.allItemList = res;
                }, function (err) {
                    console.log(err);
                });

            }

        vm.workMode = function () {
            $state.go('Inquiry.Style.Item', $stateParams.current);
        }

   }

})();