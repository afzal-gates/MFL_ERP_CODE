(function () {
    'use strict';
    angular.module('multitex.garments').controller('GmtWashRecvController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'GarmentsDataService', 'Dialog', '$modal', GmtWashRecvController]);
    function GmtWashRecvController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, GarmentsDataService, Dialog, $modal) {

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

        vm.openWashDelChallanSearchModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Garments/Gmt/_GmtWashDelChallanModal',
                controller: 'GmtWashDelChallanSearchModalController',
                size: 'lg',
                windowClass: 'app-modal-window'
            });

            modalInstance.result.then(function (dta) {
                console.log(dta);
            });
        }

    };
})();

