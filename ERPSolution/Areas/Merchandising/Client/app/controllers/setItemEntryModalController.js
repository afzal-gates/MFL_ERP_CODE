(function () {
    'use strict';

    angular.module('multitex.mrc').controller('SetItemEntryModalController', ['$modalInstance', '$scope','$state', 'config', 'MrcDataService','Style', SetItemEntryModalController]);

    function SetItemEntryModalController($modalInstance, $scope, $state, config, MrcDataService,Style) {

        $scope.SetItems = [];
        $scope.Style = Style;

        if (Style.items.length > 0) {
            $scope.SetItems = Style.items;
        } else {

            angular.forEach(_.range(Style.NO_OF_SET), function (val, key) {
                $scope.SetItems.push({ MODEL_NO: null, ITEM_NAME_EN: Style.STYLE_DESC, SEGMENT_FLAG: 'I' });
            })
        }


        $scope.addNew = function () {
            $scope.SetItems.push({ MC_STYLE_D_ITEM_ID: -1, ITEM_NAME_EN: Style.STYLE_DESC, SEGMENT_FLAG: 'I' });
        }

        $scope.save = function (data) {
            $scope.addSetItem(data);
            $modalInstance.dismiss('cancel');
        };

        $scope.remove = function (index) {
            $scope.SetItems.splice(index, 1);
        }


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();