(function () {
    'use strict';

    angular.module('multitex.admin').controller('LeaveTypeEntryController', ['AdminService', 'logger', 'config', '$q', '$stateParams', '$http','$state', LeaveTypeEntryController]);

    function LeaveTypeEntryController(AdminService, logger, config, $q, $stateParams, $http,$state) {

        var vm = this;
  
        vm.submitData = function (url,update) {
            $http({
                method: 'post',
                url: url,
                data: vm.form,
                headers: { "RequestVerificationToken": vm.antiForgeryToken }
            }).success(function (data, status, headers, config1) {                
                vm.errors = [];
                if (data.success === false) {
                    vm.errors = data.errors;
                }
                else {                   
                    if (update) {
                        $state.go('LeaveTypeList');
                    } else {
                        $state.go('LeaveTypeEntry', { data: null }, {reload:true});
                    }
                    config.appToastMsg(data.vMsg);
                }
            }).
            error(function (data, status, headers, config) {
            console.log(status);
            });
        }

        AdminService.getPeriodData().then(function (res) {
            vm.periodData = res;
        });

        if ($stateParams.data == null) {
            vm.form = {};
        } else {
            vm.form = $stateParams.data;
        }





    }



})();