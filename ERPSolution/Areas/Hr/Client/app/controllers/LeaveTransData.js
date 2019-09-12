(function () {
    'use strict';
    angular.module('multitex.hr').controller('LeaveTransDataController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state', LeaveTransDataController]);
    function LeaveTransDataController($q, config, HrService, $filter, $http, $stateParams, $state) {

        var vm = this;

        
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        if (angular.equals({}, $stateParams.datas)) {
            vm.leaveBalance = [];
        } else {
            HrService.getLeaveBalance($stateParams.datas).then(function (res) {
                vm.Params = $stateParams.datas;
               return vm.leaveBalance = res;
            });
            
        }


    }

})();