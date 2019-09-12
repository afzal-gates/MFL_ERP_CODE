(function () {
    'use strict';

    angular.module('multitex.mrc').controller('TaskGroupModalController', ['$modalInstance', '$scope', '$state', 'config', 'MrcDataService', 'TaskGroupList', TaskGroupModalController]);

    function TaskGroupModalController($modalInstance, $scope, $state, config, MrcDataService, TaskGroupList) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'TnaTaskGroup';
        var key = 'MC_TNA_TASK_GRP_ID';

        vm.form = { IS_ACTIVE: 'Y' };

        vm.reset = function () {
            $scope.modal.TA_GRP_NAME_EN.$touched = false;
            vm.form = { IS_ACTIVE: 'Y' };
        }
        


        vm.submitData = function (data, token) {
            return MrcDataService.saveData(data, ctrl, token).then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };


        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(TaskGroupList);
                    }
                },
                pageSize: 10
            },
            filterable: true,
            selectable: false,
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "TA_GRP_NAME_EN", title: "Task Group Name", type: "string", width: "100px" },
                { field: "IS_ACTIVE", title: "Active?", type: "string", width: "80px" },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='TaskList({MC_TNA_TASK_ID:dataItem.MC_TNA_TASK_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "50px"
                }
            ]
        };

    }

})();