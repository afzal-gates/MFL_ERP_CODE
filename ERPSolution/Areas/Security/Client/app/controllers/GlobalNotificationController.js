
(function () {
    'use strict';
    angular.module('multitex.security').controller('GlobalNotificationController', ['config', 'SecurityService', '$state', '$scope', '$sessionStorage', GlobalNotificationController]);
    function GlobalNotificationController(config, SecurityService, $state, $scope, $sessionStorage) {
        //mydt();
        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;

        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        loadType();

        function loadType() {
            return vm.TypeList = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            var res = [{ TYPE_ID: 'info', TYPE_NAME: 'Information' },
                                { TYPE_ID: 'success', TYPE_NAME: 'Success' },
                                { TYPE_ID: 'warning', TYPE_NAME: 'warning' },
                                { TYPE_ID: 'error', TYPE_NAME: 'Error' }];
                            e.success(res);
                        }
                    }
                },
                dataTextField: "TYPE_NAME",
                dataValueField: "TYPE_ID"
            }
        }

        vm.showMessage = function () {
            if (fnValidate() == true) {

                var url = '/api/Security/GlobalNotify/showMsg?alertType=' + vm.form.TYPE_ID + '&heading=' + vm.form.title + '&msg=' + vm.form.message + '&msgTime=' + vm.form.msgTime + '&offline=' + vm.form.offline;
                //Software Will go Off-line After 5 Munites
                SecurityService.getDataByFullUrl(url).then(function (res) {
                    vm.errors = res;
                });
            }
        }

        vm.clearMessage = function () {
                        
            SecurityService.getDataByFullUrl('/api/security/GlobalNotify/ClearMsg').then(function (data) {

                vm.form = {};
                $("#alert-placeholder").empty();
            });
        }

    }
}
)();