(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleDItemFabListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', '$modal', StyleDItemFabListController]);
    function StyleDItemFabListController($q, config, MrcDataService, $stateParams, $state, $scope, logger, $modal) {

        var vm = this;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getFabList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.params = $stateParams;

        function getFabList() {
            MrcDataService.getDataByUrl('/StyleDItemFab/FabDataByItemId/' + $stateParams.MC_STYLE_D_ITEM_ID).then(function (res) {
                vm.FabList = res;
            }, function (err) {
                console.log(err);
            })
        };


    }

})();