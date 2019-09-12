(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrEmployeeListController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', HrEmployeeListController]);

    function HrEmployeeListController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams) {




        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        
        vm.insert = true;
        vm.form = {};

                        
        vm.editRecord = function (dataItem) {
            vm.insert = false;
            
            //$routeParams = dataItem;
            //$location.path('/OfficeEntry');

            //console.log($routeParams);
            vm.form = dataItem;
            //console.log($stateParams.pState);

            if ($stateParams.pState == "EmployeeEntry") {
                return $state.go('EmployeeEntry', { pEmployeeObj: dataItem });
            }
            else {
                return $state.go('EmployeeEntryPay', { pEmployeeObj: dataItem });
            }
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

        vm.gridOptions = {
            autoBind: true,
            dataSource: $stateParams.dataSource,
            filterable: {
                mode: "row"
            },
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [

                {
                    field: "EMPLOYEE_CODE", title: "Code", type: "string", width: "100px", filterable: {
                        cell: {
                            showOperators: false
                        }
                    }
                },
                {
                    field: "EMP_FULL_NAME_EN", title: "Name", type: "string", width: "200px", filterable: {
                        cell: {
                            showOperators: true
                        }
                    }
                },
                { field: "DEPARTMENT_NAME_EN", title: "Section", type: "string", width: "200px" },
                { field: "DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "200px" },
                { field: "FLOOR_NAME_EN", title: "Floor", type: "string", width: "100px" },
                { field: "LINE_NO", title: "Line#", type: "string", width: "100px" },

                //{ field: "COMP_BUS_TYPE_NAME", title: "Business Type", type: "string", width: "150px" },
                //{ field: "VAT_REG_NO", title: "Vat Registation#", type: "string", width: "150px" },
                //{ field: "YEAR_ESTD", title: "Establish Year", type: "string", width: "100px" },
                //{ field: "COMP_WEB", title: "Web Site", type: "string", width: "200px" },
                //{ field: "COMP_DESC", title: "Description", type: "string", width: "300px" },

                //{ field: "IS_ACTIVE", title: "Active?", type: "string", width: "100px" }
                {
                    title: "Action",
                    template: function () {

                        return "<a class='btn btn-xs blue' ng-click='vm.editRecord(dataItem)'  title='Edit'><i class='fa fa-edit'></i>Edit</a>";
                        //return "<a ng-click='vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
                    },
                    width: "50px"
                }

            ]
        };

                
        
        

                           

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