(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TaskListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', '$modal', TaskListController]);
    function TaskListController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData, $modal) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'TnaTask';
        var key = 'MC_TNA_TASK_ID';
        vm.Title = $state.current.Title || '';



        vm.form = formData[key] ? formData : { IS_ACTIVE: 'Y'};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getTaskGroupList(), getSampleTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }




        function getTaskGroupList() {
            return vm.TaskGroupList = {
                optionLabel: "-- Select Task Group --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.selectAllData('TnaTaskGroup').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "TA_GRP_NAME_EN",
                dataValueField: "MC_TNA_TASK_GRP_ID"
            };
        }


        function getSampleTypeList() {

            return MrcDataService.GetAllOthers('/api/common/SelectAllSampleTypeData').then(function (res) {
                return vm.sampleTypeList = {
                    autoBind: false,
                    optionLabel: "-- Sample Type --",
                    dataSource: res,
                    dataTextField: "SMPL_TYPE_NAME",
                    dataValueField: "RF_SMPL_TYPE_ID",
                    filter: "startswith"
                };
            });
        };


        vm.onChangeTaskGroup = function (val) {
            var grid = $("#kendoGrid").data("kendoGrid");

            if (angular.isUndefined(val)) {
                grid.dataSource.filter([]);
                return;
            }

            var TaskGroupDropDown = $("#MC_TNA_TASK_GRP_ID").data("kendoDropDownList").dataSource.data();
            var index = findWithAttr(TaskGroupDropDown, 'MC_TNA_TASK_GRP_ID', parseInt(val));
            grid.dataSource.filter({ field: "TA_GRP_NAME_EN", operator: "startswith", value: TaskGroupDropDown[index].TA_GRP_NAME_EN });

        }


   


        vm.submitData = function (data, token) {
            if (angular.isDefined(data[key]) && data[key] > 0) {

                return MrcDataService.updateData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        $("#MC_TNA_TASK_GRP_ID").data("kendoDropDownList").dataSource.read();
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {

                return MrcDataService.saveData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.V_MC_COLOR_ID != 0) {
                            $state.go($state.current, { MC_TNA_TASK_ID: res.data.V_MC_TNA_TASK_ID });
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.openTaskGroupModal = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'TaskGroupModal.html',
                controller: 'TaskGroupModalController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    TaskGroupList: function (MrcDataService) {
                       return MrcDataService.selectAllData('TnaTaskGroup');
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        vm.showSplash = true;
                        MrcDataService.selectAllData('TnaTask').then(function (res) {
                            e.success(res);
                            vm.showSplash = false;
                        }, function (err) {
                            console.log(err);
                        });
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
                { field: "TA_GRP_NAME_EN", title: "TaskGroup", type: "string", width: "100px" },
                { field: "TA_TASK_SL", title: "TaskOrder", type: "string", width: "60px" },
                { field: "TA_TASK_NAME_EN", title: "Task Name(En)", type: "string", width: "150px" },
                { field: "TA_TASK_NAME_BN", title: "Task Name(Bn)", type: "string", width: "120px" },
                { field: "IS_ACTIVE", title: "Active?", type: "string", width: "60px" },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ng-click='vm.form=dataItem' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "50px"
                }
            ]
        };

        function findWithAttr(array, attr, value) {
            for (var i = 0; i < array.length; i += 1) {
                if (array[i][attr] === value) {
                    return i;
                }
            }
        }

    }

})();