(function () {
    'use strict';

    angular.module('multitex.hr').controller('LeaveBalanceModalController', ['$modalInstance', '$scope', 'items', LeaveBalanceModalController]);

    function LeaveBalanceModalController($modalInstance, $scope,items) {
        $scope.leaveBalance = items;

        $scope.ok = function () {  
        };

        $scope.save = function (data, valid) {
            if (!valid) return;
            $modalInstance.close(data);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();