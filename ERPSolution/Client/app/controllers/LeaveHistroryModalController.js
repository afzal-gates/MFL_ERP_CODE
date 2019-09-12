(function () {
    'use strict';

    angular.module('multitex').controller('LeaveHistroryModalController', ['$modalInstance', '$scope', 'item', 'notifications', '$http', '$state', 'config', '$filter', LeaveHistroryModalController]);

    function LeaveHistroryModalController($modalInstance, $scope, item, notifications, $http, $state, config, $filter) {
        item['APPLY_DATE'] = $filter('date')(moment(item.APPLY_DATE)._d, 'medium');
        item['CREATION_DATE'] = $filter('date')(moment(item.CREATION_DATE)._d, 'medium');
        $scope.item = item;
        console.log($scope.item);
        $scope.notifications = notifications;
        $scope.validSave = true;

        $scope.save = function (data, valid) {
            if (!valid) return;
            $modalInstance.close(data);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();