(function () {
    'use strict';

    angular.module('multitex.admin').controller('ActionTypeController', ['AdminService', 'logger', 'config', '$q', '$stateParams', '$http', '$state', ActionTypeController]);

    function ActionTypeController(AdminService, logger, config, $q, $stateParams, $http, $state) {

        var vm = this;
        activate();

        function activate() {
            var promise = [getActionTypeData()];
            return $q.all(promise).then(function () {
            });
        }

        vm.showAddNewLvl = false;
        vm.showAddNewConfig = false;


        function getActionTypeData() {
           return  $http({
                method: 'get',
                url: "/Hr/Admin/ActionConfig/ActionTypeListData"  //+ "&pType=" + showType
            }).
            success(function (data, status, headers, config) {
                vm.actionType = data;
            }).
            error(function (data, status, headers, config) {
                alert('something went wrong')
                console.log(status);
            });
        }
        vm.atData = { LK_STATUS_TBL_ID: $stateParams.LK_STATUS_TBL_ID, RF_ACTION_CFG_H_ID: $stateParams.RF_ACTION_CFG_H_ID,lvl: $stateParams.lvl };
        if (vm.atData.RF_ACTION_CFG_H_ID != null && !vm.atData.lvl) {
            vm.showAddNewConfig = true;
            vm.gridOptions = {
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            AdminService.getActionConfigData(vm.atData.RF_ACTION_CFG_H_ID).then(function (res) {
                                e.success(res)
                            });
                        }
                    },
                    pageSize: 10
                },
                filterable: true,
                sortable: true,
                pageSize: 10,
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },
                columns: [
                    { field: "COMP_NAME_EN", title: "Company", type: "string", width: "100px" },
                    { field: "OFFICE_NAME_EN", title: "Office", type: "string", width: "100px" },
                    { field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "100px" },
                   { field: "SECTION_NAME", title: "Section", type: "string", width: "100px" },
                    { field: "FLOOR_NAME", title: "Floor", type: "string", width: "80px" },
                    { field: "DESIG_ORDER_LOW", title: "Low", type: "number", width: "50px" },
                    { field: "DESIG_ORDER_HIGH", title: "High", type: "number", width: "50px" },
                    { field: "EMPLOYEE_CODE", title: "Code", type: "string", width: "50px" },

                    {
                        title: "Action",
                        template: function () {
                            return "</a><a ng-click='vm.addApprover(dataItem)' class='btn btn-xs blue'><i class='fa fa-plus'>Approver</i></a></a>&nbsp;&nbsp;<a ng-click='vm.editRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-edit'>Edit</i></a>";
                        },
                        width: "100px"
                    }
                ]
            };
        }

        if (vm.atData.RF_ACTION_CFG_H_ID != null && vm.atData.lvl) {
            vm.showAddNewLvl = true;
            AdminService.getLvlData(vm.atData.RF_ACTION_CFG_H_ID).then(function (res) {
                vm.lvlDatas = res;
            });
        }


        vm.editRecord = function (data) {
            $state.go('ActionType.add', { atData: vm.atData, configData: data });
        }

        vm.addApprover = function (data) {
            $state.go('ActionType.approveradd', { atData: vm.atData, configData: data });
        }



    }



})();