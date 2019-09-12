(function () {
    'use strict';
    angular.module('multitex.hr').controller('OffDayRoasterController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state', '$scope', OffDayRoasterController]);
    function OffDayRoasterController($q, config, HrService, $filter, $http, $stateParams, $state, $scope) {

        var vm = this;
        vm.form = {};
        activate()
        vm.showSplash = true;
        vm.enableDept = true;

        vm.dtFormat = config.appDateFormat;
        vm.toDay = $filter('date')(new Date(), config.appDateFormat);

        function activate() {
            var promise = [getCompanyData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getCompanyData() {
            return HrService.getCompanyData().then(function (res) {
                vm.form['HR_COMPANY_ID'] = res[0]['HR_COMPANY_ID'];
                vm.companyData = res;
            });
        }



        function departmentList() {
            return $http({
                url: '/Hr/OffDayRoaster/DeptListData',
                method: 'get',
                params: { pPARENT_ID: null }
            })
            .then(function (result) {
                vm.departmentListData = result.data;
                return;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };


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
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {
            if (newVal == '') {
                vm.form['HR_EMPLOYEE_ID'] = null;
            }
        })


        $scope.EFFECTIVE_FROMopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.EFFECTIVE_FROMopened = true;
        };

        vm.parentDepartmentListData = {
            optionLabel: "Select",
            filter: "startswith",
            autoBind: true,
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID",
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/OffDayRoaster/ParentDeptListData"  //+ "&pType=" + showType
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                }//,
                //group: { field: "PARENT_NAME" }
            },
            change: function () {

                var vParentID = this.value();
                if (vParentID != null) {

                    return $http({
                        url: '/Hr/OffDayRoaster/DeptListData',
                        method: 'get',
                        params: { pPARENT_ID: vParentID }
                    })
                    .then(function (result) {
                        vm.departmentListData = result.data;
                        vm.form.HR_DEPARTMENT_ID = vm.departmentListData[0].HR_DEPARTMENT_ID;
                        return;
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                }

            }
        };

        vm.newSchedule = function () {
            vm.enableDept = false;
            vm.form['EMPLOYEE_CODE'] = null;
            vm.form['HR_EMPLOYEE_ID'] = null;
        }

        vm.newScheduleByEmployee = function () {
            vm.enableDept = true;
            vm.form['HR_DEPARTMENT_ID'] = null;
            vm.form['PARENT_ID'] = null;
            
            vm.form['HR_EMPLOYEE_ID'] = null;
            vm.form['EMPLOYEE_CODE'] = null;
        }

        vm.employeeData = function (data) {
            return HrService.getEmployeeDataByDept(data).then(function (res) {
                var form = {};
                angular.forEach(res, function (val, key) {
                   
                    form[val.HR_EMPLOYEE_ID] = { RF_CALENDAR_DAY_ID: val.RF_CALENDAR_DAY_ID == 0 ? val.DEFAULT_OFFDAY : val.RF_CALENDAR_DAY_ID };
                })

                var dataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize: 1000,
                    schema:  {
                        model: {
                            fields: {
                                LAST_UPDATE_DATE: { type: "date" },
                                EFFECTIVE_FROM: { type: "date" }
                            }
                        }
                    }  
                });
                $state.go('OffDayRoaster.data', { dataSource: dataSource, HR_COMPANY_ID: data.HR_COMPANY_ID, formData: form });
            });
        }    
    }

})();