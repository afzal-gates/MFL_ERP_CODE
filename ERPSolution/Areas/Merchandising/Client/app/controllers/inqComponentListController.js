(function () {
    'use strict';
    angular.module('multitex.mrc').controller('InqComponentListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', InqComponentListController]);
    function InqComponentListController($q, config, MrcDataService, $stateParams, $state, $scope, logger) {

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