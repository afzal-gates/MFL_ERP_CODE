
(function () {
    'use strict';
    angular.module('multitex.garments').controller('GmtAsSampleSearchModalController', ['$q', 'config', 'GarmentsDataService', '$modalInstance', '$scope', GmtAsSampleSearchModalController]);
    function GmtAsSampleSearchModalController($q, config, GarmentsDataService, $modalInstance, $scope) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
    }

})();
