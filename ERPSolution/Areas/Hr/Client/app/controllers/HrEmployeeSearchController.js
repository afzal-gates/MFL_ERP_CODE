(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrEmployeeSearchController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', HrEmployeeSearchController]);

    function HrEmployeeSearchController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams) {
        
        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        
        vm.insert = true;
        vm.form = {};

        vm.addNew = function () {
            //alert($stateParams.pState);

            if ($stateParams.pState == "EmployeeEntry") {
                return $state.go('EmployeeEntry');
            }
            else {
                return $state.go('EmployeeEntryPay');
            }
            //return $state.go('EmployeeEntry');
        };

                
        vm.editRecord = function (dataItem) {
            vm.insert = false;
            
            //$routeParams = dataItem;
            //$location.path('/OfficeEntry');

            //console.log($routeParams);
            vm.form = dataItem;
            //console.log(dataItem);

            return $state.go('EmployeeEntry', { pEmployeeObj: dataItem });
        };

       
        vm.sectionListData = {
            optionLabel: "-- Select Section --",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrEmployee/DeptListData",  //+ "&pType=" + showType
                            params: { pPARENT_ID: null }
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
            index:1,
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        function jobStatusList() {
            return $http({
                url: '/Hr/HrEmployee/LookupListData',
                method: 'get',
                params: {
                    pLookupTableId: 11,
                },
            })
                .then(function (result) {
                    vm.jobStatusListData = result.data;
                    vm.form.LK_JOB_STATUS_ID = vm.jobStatusListData[0].LOOKUP_DATA_ID;
                    return;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };
            
        //vm.empDataSource = [];
        //var empDataSource = [];

        $scope.$watchGroup(['vm.form.HR_DEPARTMENT_ID', 'vm.form.LK_JOB_STATUS_ID'], function (newVal, oldval){

            if (newVal[0] != null && newVal[1] != null) {
                
                vm.form.EMPLOYEE_CODE = null;
                vm.form.OLD_EMP_CODE = null;
                
                HrService.getEmployeeList(newVal[0], newVal[1], vm.form.EMPLOYEE_CODE).then(function (res) {
                    vm.empDataSource = res;                    
                    //console.log(res); 

                    var empDataSource = new kendo.data.DataSource({
                        data: res,
                        pageSize: 10
                        //schema: {
                        //    model: {
                        //        fields: {
                        //            EMPLOYEE_CODE: { type: "date" },
                        //            EFFECTIVE_FROM: { type: "date" }
                        //        }
                        //    }
                        //}
                    });

                    
                    if ($stateParams.pState == "EmployeeEntry") {
                        $state.go('EmployeeList.data', { dataSource: empDataSource });
                    }
                    else {
                        $state.go('EmployeeEntryPayList.data', { dataSource: empDataSource });
                    }

                }); 
                                                      

            }
        });



        $scope.emoloyeeAuto = function (val) {
            //alert('a');
            return HrService.EmployeeAutoDataList(val, 30, null).then(function (res) {
                return res;
            });
        };


        //$scope.onSelectItem = function (item) {                       
        //    vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
        //    vm.form.EMPLOYEE_CODE = item.EMPLOYEE_CODE;
        //}


        vm.employeeCodeSearch = function () {
            vm.form.OLD_EMP_CODE = null;

            HrService.getEmployeeList(null, null, vm.form.EMPLOYEE_CODE, null).then(function (res) {
                vm.empDataSource = res;
                //console.log(res); 

                var empDataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize: 10
                    //schema: {
                    //    model: {
                    //        fields: {
                    //            EMPLOYEE_CODE: { type: "date" },
                    //            EFFECTIVE_FROM: { type: "date" }
                    //        }
                    //    }
                    //}
                });

                //$state.go('EmployeeList.data', { dataSource: empDataSource });
                if ($stateParams.pState == "EmployeeEntry") {
                    $state.go('EmployeeList.data', { dataSource: empDataSource });
                }
                else {
                    $state.go('EmployeeEntryPayList.data', { dataSource: empDataSource });
                }

            });
        }
                
        vm.employeeOldCodeSearch = function () {
            vm.form.EMPLOYEE_CODE = null;

            HrService.getEmployeeList(null, null, null, vm.form.OLD_EMP_CODE).then(function (res) {
                vm.empDataSource = res;
                //console.log(res); 

                var empDataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize: 10
                    //schema: {
                    //    model: {
                    //        fields: {
                    //            EMPLOYEE_CODE: { type: "date" },
                    //            EFFECTIVE_FROM: { type: "date" }
                    //        }
                    //    }
                    //}
                });

                //$state.go('EmployeeList.data', { dataSource: empDataSource });
                if ($stateParams.pState == "EmployeeEntry") {
                    $state.go('EmployeeList.data', { dataSource: empDataSource });
                }
                else {
                    $state.go('EmployeeEntryPayList.data', { dataSource: empDataSource });
                }

            });
        }
        

                           

        function activate(){
            var promise = [jobStatusList()];
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