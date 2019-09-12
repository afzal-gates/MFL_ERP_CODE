(function () {
    'use strict';
    angular.module('multitex.mrc').controller('InqStyleListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', InqStyleListController]);
    function InqStyleListController($q, config, MrcDataService, $stateParams, $state, $scope, logger) {

        var vm = this;
        vm.Title = $state.current.Title || '';
    
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
   }

})();