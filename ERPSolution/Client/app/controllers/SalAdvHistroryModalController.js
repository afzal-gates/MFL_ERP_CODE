(function () {
    'use strict';

    angular.module('multitex').controller('SalAdvHistroryModalController', ['$modalInstance', '$scope', 'item', 'notifications', '$http', '$state', 'config', '$filter', SalAdvHistroryModalController]);

    function SalAdvHistroryModalController($modalInstance, $scope, item, notifications, $http, $state, config, $filter) {
        item['APPLY_DATE'] = $filter('date')(moment(item.APPLY_DATE)._d, 'medium');
        item['CREATION_DATE'] = $filter('date')(moment(item.CREATION_DATE)._d, 'medium');
        $scope.item = item;
        $scope.notifications = notifications;
        console.log($scope.notifications);
        $scope.save = function (data) {
            $modalInstance.close(data);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();