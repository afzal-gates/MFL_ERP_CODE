(function () {
    'use strict';

    angular.module('multitex').controller('OfficeMappingModalController', ['$modalInstance', '$scope', 'buyer', 'AllOffice', 'SelectedOffice', '$state', 'config', 'MrcDataService', OfficeMappingModalController]);

    function OfficeMappingModalController($modalInstance, $scope, buyer, AllOffice, SelectedOffice, $state, config, MrcDataService) {

        $scope.save = function (data, token) {
            var dataForSave = [];
            if (data.length > 0) {
                angular.forEach(data, function (val, key) {
                    dataForSave.push({ MC_BUYER_ID: $scope.buyer.MC_BUYER_ID, MC_BUYER_OFF_ID: val.MC_BUYER_OFF_ID, IS_LOC_AGNT: (val.LK_BOFF_TYPE_ID == 187 || val.LK_BOFF_TYPE_ID == 188) ? 'Y' : 'N', IS_ACTIVE: 'Y', CREATED_BY: $scope.CREATED_BY || 1, CAN_BE_DEL: (val.EXIST || val.EXIST=='Y')?'Y':'N' });
                });

                return MrcDataService.officeMapData(dataForSave, token).then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                },function (err) {
                    console.log(err);
                })
            }
        };

        $scope.buyer = buyer;
        $scope.AllOffice = angular.copy(AllOffice);

        $scope.SelectedOffice = angular.copy(SelectedOffice||[]);

        $scope.demoOptions = {
            title: '',
            filterPlaceHolder: 'Start typing to filter the lists below.',
            labelAll: 'Office not yet selected',
            labelSelected: 'Office selected',
            helpMessage: 'Click items to transfer them between fields.',
            /* angular will use this to filter your lists */
            orderProperty: '',
            /* this contains the initial list of all items (i.e. the left side) */
            items: $scope.AllOffice,
            /* this list should be initialized as empty or with any pre-selected items */
            selectedItems: $scope.SelectedOffice
        };

        function findWithAttr(array, attr, value) {
            for (var i = 0; i < array.length; i += 1) {
                if (array[i][attr] === value) {
                    return i;
                }
            }
        }


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();