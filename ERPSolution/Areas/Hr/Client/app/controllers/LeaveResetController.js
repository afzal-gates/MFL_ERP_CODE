(function () {
    'use strict';
    angular.module('multitex.hr').controller('LeaveResetController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state','logger','$scope', LeaveResetController]);
    function LeaveResetController($q, config, HrService, $filter, $http, $stateParams, $state,logger,$scope) {

        var vm = this;
        vm.processBtn = false;
        vm.form = {};
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.processLeaveReset = function (data) {
            console.log(data);
            return HrService.processLeaveReset(data).then(function (res) {
                config.appToastMsg(res.vMsg);
            });
        }


        $scope.$watchGroup(['vm.form.HR_COMPANY_ID', 'vm.form.RF_FISCAL_YEAR_ID', 'vm.form.HR_LEAVE_TYPE_ID'], function (newVal, oldVal) {
            
            var data = { HR_COMPANY_ID: newVal[0], RF_FISCAL_YEAR_ID: newVal[1], HR_LEAVE_TYPE_ID: newVal[2] }

            if(angular.isDefined(newVal[2])){
                return HrService.leaveBalanceByType(data).then(function (res) {
                    if (angular.equals([], res)) {
                        vm.processBtn = true;
                    }
                });
            }
        });


       //$scope.$watch('vm.form.HR_LEAVE_TYPE_ID', function (newVal,oldVal) {
       //    HrService.leaveBalanceByType(vm.form).then(function (res) {
       //        if (angular.equals([], res)) {
       //            vm.processBtn = false;
       //        }
       //    });
       // });


       


        vm.leaveBalanceByType = function (data) {
            return HrService.leaveBalanceByType(data).then(function (res) {
                var dataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize:10
                });
                $state.go('LeaveReset.data', { dataSource: dataSource });
            });
        }







        HrService.getCompanyData().then(function (res) {

            vm.form['HR_COMPANY_ID'] = res[0]['HR_COMPANY_ID'];
            vm.companyData = res;
        });

        HrService.getFiscalYearData().then(function (res) {
            vm.form['RF_FISCAL_YEAR_ID'] = res[0]['RF_FISCAL_YEAR_ID'];
            vm.FiscalYearData = res;
        });


        HrService.ProcessableLeaveType().then(function (res) {
            vm.LeaveTypeData = res;
        });



        //if (angular.equals({}, $stateParams.datas)) {
        //    vm.leaveBalance = [];
        //} else {
        //    HrService.getLeaveBalance($stateParams.datas).then(function (res) {
        //        console.log(res);
        //        return vm.leaveBalance = res;
        //    });

        //}
    }

})();