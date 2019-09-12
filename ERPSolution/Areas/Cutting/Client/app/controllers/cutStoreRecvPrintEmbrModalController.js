
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutStoreRecvPrintEmbrModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', CutStoreRecvPrintEmbrModalController]);
    function CutStoreRecvPrintEmbrModalController($q, config, CuttingDataService, $modalInstance, $scope) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

      
    }

})();
