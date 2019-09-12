(function () {
    'use strict';

    angular.module('multitex').controller('ShowImageModalController', ['$modalInstance', '$scope', 'Buyer', '$state', 'config', 'MrcDataService', ShowImageModalController]);

    function ShowImageModalController($modalInstance, $scope, Buyer, $state, config, MrcDataService) {

        $scope.save = function (token, CREATED_BY) {

            var saveForData = [];

            var data = $('#kendoGrid').data("kendoGrid").dataSource.data();

            angular.forEach(data, function (val, key) {
                if (val.BYR_ACC_NAME_EN && val.BYR_ACC_SNAME) {
                    val['CREATED_BY'] = CREATED_BY;
                    saveForData.push(val);
                }
            });

            if (saveForData.length == 0) {
                return;
            } else {
                return MrcDataService.submitBuyerAcc(saveForData, token).then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    $('#kendoGrid').data("kendoGrid").dataSource.read();
                }, function (err) {
                    console.log(err);
                })
            }


        };

        $scope.STYL_KEY_IMG = localStorage.getItem("VData");

        $scope.addNew = function (CREATED_BY) {

            var data = { IS_ACTIVE: 'Y', CREATED_BY: CREATED_BY, MC_BYR_ACC_ID: -1, BYR_ACC_DESC: null, BYR_ACC_NAME_EN: null, BYR_ACC_SNAME: null, DISPLAY_ORDER: null, MC_BUYER_ID: Buyer.MC_BUYER_ID }
            $('#kendoGrid').data("kendoGrid").dataSource.insert(0, data);
        }

        $scope.remove = function (data) {
            $('#kendoGrid').data("kendoGrid").dataSource.remove(data);
        }

        $scope.Buyer = Buyer;

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };


    }

})();