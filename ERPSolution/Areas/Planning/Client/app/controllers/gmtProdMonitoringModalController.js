(function () {
    'use strict';
    angular.module('multitex.planning').controller('GmtProdMonitoringModalController', ['$q', 'config', 'PlanningDataService', '$scope', '$modalInstance', 'data', GmtProdMonitoringModalController]);
    function GmtProdMonitoringModalController($q, config, PlanningDataService, $scope, $modalInstance, data) {

        $scope.data = data;
        $scope.cancel = function () {
            $modalInstance.dismiss(0);
        };

    }

})();