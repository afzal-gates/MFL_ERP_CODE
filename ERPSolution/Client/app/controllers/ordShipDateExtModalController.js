(function () {
    'use strict';

    angular.module('multitex').controller('OrdShipDateExtModalController', ['$modalInstance', '$scope', 'config', 'DashBoardService', '$filter', 'Dialog', 'V_MC_ORDER_SHIP_ID', OrdShipDateExtModalController]);

    function OrdShipDateExtModalController($modalInstance, $scope, config, DashBoardService, $filter, Dialog, V_MC_ORDER_SHIP_ID) {

        $scope.dtFormat = config.appDateFormat;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        DashBoardService.getDataByUrl('/api/common/FindOrderShipData?pMC_ORDER_SHIP_ID=' + V_MC_ORDER_SHIP_ID).then(function (res) {
            $scope.data = res;
        });
        $scope.SHIP_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.SHIP_DTopened = true;
        };

        $scope.save = function (dataOri,valid) {
            if (!valid) return;
            var data = Object.assign({}, dataOri);
            var msg = "Current Ship Date : <b>" + $filter('date')(data.SHIP_DT, config.appDateFormat) + "</b><br>";
            msg += "Revised Ship Date :  <b>" + $filter('date')(dataOri.SHIP_DT_EXT_DT, config.appDateFormat) + "</b><br>";
            msg += 'Do you really want to update?<br> Click <b>Yes</b> to proceed';

            Dialog.confirm(msg, 'Is it Correct Information?', ['Yes', 'No'])
            .then(function () {
                data['SHIP_DT_EXT_DT'] = $filter('date')(data.SHIP_DT_EXT_DT, config.appDateFormat);
                return DashBoardService.saveDataByUrl(data, '/api/common/UpdateOrderShipData').then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $modalInstance.close();
                    }

                });
            });

        };



        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();