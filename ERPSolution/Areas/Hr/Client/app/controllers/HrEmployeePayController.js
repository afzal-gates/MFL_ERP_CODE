(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrEmployeePayController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', HrEmployeePayController]);

    function HrEmployeePayController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams) {
        
        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        vm.today = new Date();
        vm.insert = true;
        vm.form = {};               
        
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $('#EMPLOYEE_CODE').focus();

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        //======== Start Set Focus ===========
        $("#EMPLOYEE_CODE").keypress(function (e) {
            if (e.which == 13) {
                $("#GROSS_SALARY").focus();
            };
        });

        $("#GROSS_SALARY").keypress(function (e) {
            if (e.which == 13) {
                $("#BTN_UPDATE_GROSS").focus();
            };
        });

        $scope.emoloyeeAuto = function (val) {
            return HrService.EmployeeAutoDataList(val, 30, null).then(function (res) {
                return res;
            });
        };

        $scope.onSelectItem = function (item) {
            //console.log(item);            

            if (item.GROSS_SALARY < 1 || item.GROSS_SALARY == null) {
                vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
                vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
                vm.form.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
                vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
                vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
                //vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
                vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;
                vm.form.GROSS_SALARY = item.GROSS_SALARY;

                $("#GROSS_SALARY").focus();
            }
            else {
                vm.form.HR_EMPLOYEE_ID = null;
                vm.form.EMP_FULL_NAME_EN = 'Please search only those employee whose gross salary are empty.';
            }
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;
                vm.form.EMP_FULL_NAME_EN = null;
                vm.form.DEPARTMENT_NAME_EN = null;
                vm.form.DESIGNATION_NAME_EN = null;
                vm.form.COMP_NAME_EN = null;
                //vm.form.HR_COMPANY_ID = null;
                vm.form.GROSS_SALARY = null;
            }

        });
        
        //vm.empPayDataSource = new kendo.data.DataSource({
        //    transport: {
        //        read: function (e) {
        //            e.success(data);
        //        }
        //    }
        //});

        //$('#kendoGrid').data("kendoGrid").setDataSource(empPayDataSource);

        vm.reset = function () {
            vm.form = {};
            $('#EMPLOYEE_CODE').focus();
        }

        vm.submit = function (form) {
            $http({
                headers: { "RequestVerificationToken": vm.antiForgeryToken },
                url: '/Hr/HrEmployee/EmployeeGrossUpdate',
                method: 'post',
                data: form
            }).success(function (data, status, headers, config1) {
                vm.errors = undefined;
                if (data.success === false) {
                    vm.errors = data.errors;
                }
                else {                    
                    data['data'] = angular.fromJson(data.jsonStr);
                    
                    console.log(data);
                    config.appToastMsg(data.data.PMSG);
                    vm.reset();
                }
            }).error(function (data, status, headers, config) {
                alert('something went wrong')
                console.log(status);
            });
        };



        function activate(){
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        //function getData(){
        //     return AdminService.getData().then(function (data) {
        //         //vm.schedulerOptions.dataSource.data = data;
        //         //return vm.schedulerOptions.dataSource.data;
           
        //    });
        //}

 
        

            
        
    }

    

})();