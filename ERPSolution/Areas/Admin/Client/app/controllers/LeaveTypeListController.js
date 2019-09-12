(function () {
    'use strict';

    angular.module('multitex.admin').controller('LeaveTypeListController', ['AdminService', 'logger', 'config', '$q', '$state', '$http', LeaveTypeListController]);

    function LeaveTypeListController(AdminService, logger, config, $q, $state, $http) {

        var vm = this;
        vm.editRecord = function (data) {
            //console.log(data);
            $state.go('LeaveTypeEntry', { data: data});
        }

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'get',
                            url: "/Hr/Admin/HrLeaveType/ListData"  //+ "&pType=" + showType
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                },
                pageSize: 10
            },
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
                { field: "LEAVE_TYPE_CODE", title: "Code", type: "string", width: "100px" },
                { field: "LV_TYPE_NAME_EN", title: "Leave Type Name", type: "string", width: "200px" },

                {
                    title: "Action",
                    template: function () {
                        return "</a><a ng-click='vm.editRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "50px"
                }
            ]
        };



    }



})();