
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('BundleStatusModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', 'bndlData', BundleStatusModalController]);
    function BundleStatusModalController($q, config, CuttingDataService, $modalInstance, $scope, bndlData) {

        $scope.bndlData = bndlData;

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

      
    }

})();
