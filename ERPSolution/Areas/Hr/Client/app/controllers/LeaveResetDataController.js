(function () {
    'use strict';
    angular.module('multitex.hr').controller('LeaveResetDataController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state', LeaveResetDataController]);
    function LeaveResetDataController($q, config, HrService, $filter, $http, $stateParams, $state) {

        var vm = this;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.gridOptions = {
            autoBind: true,
            dataSource: $stateParams.dataSource,
            filterable: {
                mode: "row"
            },
            selectable: "row",
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "EMPLOYEE_CODE", title: "Employee Code", type: "number", width: "100px" },
                { field: "EMP_FULL_NAME_EN", title: "Employee Name", type: "string", width: "100px" },
                { field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "100px" },
                { field: "DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "100px" },

                {
                    field: "MAX_DAY_LV_CY",
                    filterable:{
                        cell:{
                            enabled:false
                        }
                    },
                    title: "Leave Quota", type: "number", width: "50px"
                },
                {
                    field: "TAKE_DAY_CY",
                    filterable: {
                        cell: {
                            enabled: false
                        }
                    },
                    title: "Leave Taken", type: "number", width: "50px"
                },
                {
                    field: "BAL_DAY_CY",
                    filterable: {
                        cell: {
                            enabled: false
                        }
                    },
                    title: "Balance", type: "number", width: "50px"
                }
                ]
        };


         
    }

})();