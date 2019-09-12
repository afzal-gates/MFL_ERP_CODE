(function () {
    'use strict';

    angular.module('multitex.hr').controller('LvAttendanceStatusController', ['$modalInstance', '$scope', 'items', 'config','$filter', LvAttendanceStatusController]);

    function LvAttendanceStatusController($modalInstance, $scope, items, config, $filter) {

        var data = [];
        angular.forEach(items, function (val, key) {
            val['ATTEN_DATE'] = $filter('date')(moment(val.ATTEN_DATE)._d, config.appDateFormat);
            val['CLK_IN_WT'] = $filter('date')(moment(val.CLK_IN_WT)._d, 'mediumTime');
            val['CLK_OUT_WT'] = $filter('date')(moment(val.CLK_OUT_WT)._d, 'mediumTime');
            data.push(val);
        });
        $scope.attendanceStatus = data;

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