
(function () {
    'use strict';
    angular.module('multitex.garments').controller('GmtWashDelChallanSearchModalController', ['$q', 'config', 'GarmentsDataService', '$modalInstance', '$scope', GmtWashDelChallanSearchModalController]);
    function GmtWashDelChallanSearchModalController($q, config, GarmentsDataService, $modalInstance, $scope) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

      
    }

})();
