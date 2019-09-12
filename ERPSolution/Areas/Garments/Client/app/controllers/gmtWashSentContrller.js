
(function () {
    'use strict';
    angular.module('multitex.garments').controller('GmtWashSentController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'GarmentsDataService', 'Dialog', '$modal', GmtWashSentController]);
    function GmtWashSentController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, GarmentsDataService, Dialog, $modal) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        activate();
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                
                vm.showSplash = false;                
            });
        }
      };
})();







