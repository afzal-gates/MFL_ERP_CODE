(function () {
    'use strict';
    angular.module('multitex.hr').controller('LeaveTransListController', ['$q', 'config', 'HrService', '$filter', '$http','$stateParams','$state','$scope', LeaveTransListController]);
    function LeaveTransListController($q, config, HrService, $filter, $http, $stateParams, $state, $scope) {

        var vm = this;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getCompanyData(), getFiscalYearData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.form = {};
        //if (angular.equals({}, $stateParams.datas)) {
        //    vm.form = {};
        //} else {
        //    vm.form = $stateParams.datas;
        //}



        vm.onChange = function (data) {
            return $state.transitionTo('LeaveTransList', { datas: data }, { reload: false, inherit: false, notify: true });
        }

        vm.loadLeaveBalance = function (data) {
            console.log(data);
            //alert('1');
            if ($state.includes('LeaveTransList.data')) {
                //alert('2');

                $state.go('LeaveTransList');
            }

            HrService.getCompanyData().then(function (res) {
                //alert('3');
                return $state.go('LeaveTransList.data', { datas: data });
            });

  
        }

        function getCompanyData() {
            return HrService.getCompanyData().then(function (res) {
                //vm.form['HR_COMPANY_ID'] = res[0]['HR_COMPANY_ID'];
                vm.companyData = res;
            });
        }



        function getFiscalYearData() {
            return HrService.getFiscalYearData().then(function (res) {
                vm.FiscalYearData = res;
                //return vm.form['RF_FISCAL_YEAR_ID'] = res[0]['RF_FISCAL_YEAR_ID'];
            });
        }


        $scope.emoloyeeAuto = function (val) {
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectItem = function (item) {
            vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;
            vm.showEmployeeInfo = true;
            vm.employee = item;
        }





        //$http({
        //    method: 'post',
        //    url: '/hr/HrReport/Report',
        //    params: { HR_COMPANY_ID: 1, RF_FISCAL_YEAR_ID: 1, HR_EMPLOYEE_ID: 1,HR_LEAVE_TYPE_ID:1}
        //}).then(function (res) {
        //    return res.data;
        //}).catch(function (message) {
        //    exception.catcher('XHR loading Failded')(message);
        //});

        //function getEmployeeDataById() {
        //    return HrService.getEmployeeDataById().then(function (res) {
        //        return vm.form['HR_EMPLOYEE_ID'] = res.HR_EMPLOYEE_ID;
        //    });
        //}

        function getLeaveDataByEmployeeId() {
            return HrService.getLeaveDataByEmployeeId($stateParams.datas).then(function (res) {
                console.log(res);
                var data = [];
                angular.forEach(res, function (val, key) {                   
                    val['FROM_DATE'] = $filter('date')(moment(val.FROM_DATE)._d, config.appDateFormat);
                    val['TO_DATE'] = $filter('date')(moment(val.TO_DATE)._d, config.appDateFormat);
                    val['NEXT_JOIN_DATE'] = $filter('date')(moment(val.NEXT_JOIN_DATE)._d, config.appDateFormat);
                    val['CREATION_DATE'] = $filter('date')(moment(val.CREATION_DATE)._d, config.appDateFormat);
                    data.push(val);
                });
                vm.leaveDatas = data;
            });
        }




        
        //function getEmployeeDataList() {
        //    return vm.EmployeeDataList = {                
        //        template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
        //        filter: "startswith",
        //        autoBind: true,
        //        optionLabel: "Select Employee",
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    HrService.getEmployeeList(null, 30, null, null).then(function (res) {
        //                        console.log(res);
        //                        e.success(res);
        //                    });
        //                }
        //            }
        //        },
        //        dataTextField: "EMPLOYEE_CODE",
        //        dataValueField: "HR_EMPLOYEE_ID"
        //    };

        //};

    };

})();