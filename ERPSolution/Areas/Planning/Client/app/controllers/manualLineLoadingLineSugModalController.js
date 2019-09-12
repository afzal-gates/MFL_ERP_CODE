(function () {
    'use strict';
    angular.module('multitex.planning').controller('ManualLineLoadingLineSugModalController', ['$modalInstance', '$scope', '$q', 'config', '$filter', 'PlanningDataService', 'Dialog', 'data', 'lines', ManualLineLoadingLineSugModalController]);

    function ManualLineLoadingLineSugModalController($modalInstance, $scope, $q, config, $filter, PlanningDataService, Dialog, data, lines) {


        activate();
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                $scope.showSplash = false;
            });
        }

        $scope.data = data;
        $scope.lines = lines;

        $scope.selected = function (itm) {
            $modalInstance.close(itm);
        };


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();
