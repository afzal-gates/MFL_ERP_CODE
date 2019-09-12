(function () {
    'use strict';
    angular.module('multitex.hr').controller('LeaveTransEditDataController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state', LeaveTransEditDataController]);
    function LeaveTransEditDataController($q, config, HrService, $filter, $http, $stateParams, $state) {

        var vm = this;
        vm.dtFormat = config.appDateFormat;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.editRecord = function (data) {
            delete data['CREATION_DATE'];
            $state.go("LeaveTransEntry", { LeaveTrans: data });
        }





        vm.gridOptions = {
            autoBind: true,
            dataSource: $stateParams.dataSource,
            //filterable: {
            //    mode: "row"
            //},
            selectable: "row",
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "LEAVE_REF_NO", title: "Leave Ref.", type: "number", width: "50px" },
                { field: "LV_TYPE_NAME_EN", title: "Leave Type", type: "string", width: "60px" },
                { field: "EMPLOYEE_CODE", title: "Employee Code", type: "string", width: "60px" },
                { field: "EMP_FULL_NAME_EN", title: "Name", type: "string", width: "60px" },
                { field: "DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "60px" },
                { field: "LK_DATA_NAME_EN", title: "Leave Status", type: "string", width: "60px" },
                {
                    field: "FROM_DATE",
                    filterable: {
                        cell: {
                            enabled: false
                        }
                    },
                    title: "From Date", type: "date", format: "{0:" + vm.dtFormat + "}", width: "50px"
                },
                {
                    field: "TO_DATE",
                    filterable: {
                        cell: {
                            enabled: false
                        }
                    },
                    title: "To Date", type: "date", format: "{0:" + vm.dtFormat + "}", width: "50px"
                },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ng-click='vm.editRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "30px"
                }
            ]
        };
    }

})();