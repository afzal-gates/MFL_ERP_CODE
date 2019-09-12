(function () {
    'use strict';

    angular.module('multitex.security').controller('ModalRoleAssignController', ['$modalInstance', '$scope', 'items', 'roles', '$http', '$state', 'SecurityService','config', ModalRoleAssignController]);

    function ModalRoleAssignController($modalInstance, $scope, items, roles, $http, $state, SecurityService, config) {
        $scope.items = items;
        $scope.roles = roles;
        $scope.RoleListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Security/ScRole/RoleListData"  //+ "&pType=" + showType
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                }
            },
            dataTextField: "ROLE_NAME_EN",
            dataValueField: "SC_ROLE_ID"
        };

        $scope.validSave = true;
        $scope.save = function (data,valid) {
            if (!valid) return;
            SecurityService.saveRoleData(data).then(function (res) {
                $scope.validSave = false;
                config.appToastMsg(res.data.vMsg);
                $state.go("UserList", {}, { reload: true });
            });
        };

        $scope.update = function (data, valid) {
            if (!valid) return;
            SecurityService.updateRoleData(data).then(function (res) {
                $scope.validSave = false;
                config.appToastMsg(res.data.vMsg);
                $state.go("UserList", {}, { reload: true });
            });
        };





        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();