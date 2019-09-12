(function () {
    'use strict';

    angular.module('multitex').controller('TnaUpdateModalController', ['$modalInstance', '$scope', 'Order', 'Task', '$http', '$state', 'config', 'DashBoardService', 'cur_date_server', '$filter', 'Dialog', TnaUpdateModalController]);

    function TnaUpdateModalController($modalInstance, $scope, Order, Task, $http, $state, config, DashBoardService, cur_date_server, $filter, Dialog) {

        $scope.Order = angular.copy(Order);
        $scope.Task = angular.copy(Task);

        var CurrDate = new Date(cur_date_server);

        $scope.Task['IS_ONLY_COMMNENT'] = 'N';
        $scope.Task['ACT_START_DT'] = Task.ACT_START_DT ? Task.ACT_START_DT: new Date(cur_date_server);
        $scope.format = config.appDateFormat;

        if ($scope.Task.LAG_DAYS > -1) {
            
            $scope.Task['MIN_DATE'] = CurrDate.setDate(CurrDate.getDate() - 3);
            $scope.Task['MAX_DATE'] = $scope.Task.PLAN_START_DT;

        } else {
            $scope.Task['MIN_DATE'] = CurrDate.setDate(CurrDate.getDate() - 3);
            $scope.Task['MAX_DATE'] = new Date(cur_date_server);
        }


        $scope.save = function (dataOri, token,valid) {
            if (!valid) return;

            if (dataOri.IS_ONLY_COMMNENT == 'Y') {
                var data = angular.copy(dataOri);
                data['EXCEPTED_DATE'] = $filter('date')(data.EXCEPTED_DATE, config.appDateFormat);
                return DashBoardService.saveDataByUrl(data, '/api/common/SaveTnAData', token).then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $modalInstance.close(1);
                    }
                });

            } else if (dataOri.IS_ONLY_COMMNENT == 'A') {
                var msg = "T&A Task Name :  <b>" + dataOri.TA_TASK_NAME_EN + "</b><br><br>";
                msg += "<b>N/A</b> has selected<br>";
                msg += 'Do you really want to update?<br> Click <b>Yes</b> to proceed';

                Dialog.confirm(msg, 'Is it Correct Information?', ['Yes', 'No'])
                .then(function () {
                    var data = angular.copy(dataOri);
                    data['ACT_START_DT'] = $filter('date')(data.ACT_START_DT, config.appDateFormat);
                    return DashBoardService.saveDataByUrl(data, '/api/common/SaveTnAData', token).then(function (res) {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $modalInstance.close(1);
                        }

                    });
                });
            } else {
                var msg = "T&A Task Name :  <b>" + dataOri.TA_TASK_NAME_EN + "</b><br>";
                msg += "Actual Date :  <b>" + $filter('date')(dataOri.ACT_START_DT, config.appDateFormat) + "</b><br>";
                msg += 'Do you really want to update?<br> Click <b>Yes</b> to proceed';

                Dialog.confirm(msg, 'Is it Correct Information?', ['Yes', 'No'])
                .then(function () {
                    var data = angular.copy(dataOri);
                    data['ACT_START_DT'] = $filter('date')(data.ACT_START_DT, config.appDateFormat);
                    return DashBoardService.saveDataByUrl(data, '/api/common/SaveTnAData', token).then(function (res) {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $modalInstance.close(data.ACT_START_DT);
                        }

                    });
                });
            }



        };

        $scope.ACT_START_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ACT_START_DTopened = true;
            //setTimeout(function () {
            //    $scope.ACT_START_DTopened = false;
            //}, 10);
        };

        $scope.EXCEPTED_DATEopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.EXCEPTED_DATEopened = true;
        };


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();