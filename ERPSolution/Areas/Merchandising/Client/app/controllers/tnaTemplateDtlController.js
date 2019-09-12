(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TnaTemplateDtlController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'TnATaskList', TnaTemplateDtlController]);
    function TnaTemplateDtlController($q, config, MrcDataService, $stateParams, $state, $scope, logger, TnATaskList) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'TnaTemplateDtl';
        var key = 'MC_TNA_TMPLT_D_ID';
        var templateDataOri = [];
        vm.MC_TNA_TMPLT_H_ID = $stateParams.MC_TNA_TMPLT_H_ID || 0;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getTaskGroupList(), getparentTasksList(), getTemplateData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;


            });
        }


        var IS_PRODUCTION = function (MC_TNA_TASK_ID) {

            if (!MC_TNA_TASK_ID) {
                return false;
            }

            if (_.find(TnATaskList, function (o) { return o.MC_TNA_TASK_ID == MC_TNA_TASK_ID; }).IS_PRODUCTION === 'Y') {
                return true;
            } else {
                return false;
            }

        };

        vm.add = function () {

            var newItem = {};
            var task = {};
            var item = angular.copy(vm.templateDatas.slice(-1)[0]);

            if (angular.isUndefined(item)) {
                newItem = {
                    MC_TNA_TMPLT_D_ID: -1,
                    MC_TNA_TASK_ID: '',
                    PARENT_TASK_ID: '',
                    STD_DAYS: 0,
                    IS_PRODUCTION_N: 'N',
                    IS_PRODUCTION_B: 'N',
                    IS_ST_END: 'N',
                    IS_ST_END_BASE: 'N'
                }

            } else {

                if (IS_PRODUCTION(item.MC_TNA_TASK_ID)) 
                {

                    task = _.find(TnATaskList, function (o) { return o.MC_TNA_TASK_ID == item.MC_TNA_TASK_ID; })
                  
                  newItem = {
                        MC_TNA_TMPLT_D_ID: -1,
                        MC_TNA_TASK_ID: item.MC_TNA_TASK_ID,
                        PARENT_TASK_ID: item.PARENT_TASK_ID,
                        STD_DAYS: item.STD_DAYS,
                        IS_PRODUCTION_N: item.IS_PRODUCTION_N,
                        IS_PRODUCTION_B: item.IS_PRODUCTION_B,
                        DayMulTi: item.DayMulTi,
                        IS_START_TASK: task.IS_START_TASK,
                        IS_END_TASK: task.IS_END_TASK,
                        IS_ST_END: item.IS_ST_END === 'S' ? 'E' : item.IS_ST_END==='E'? 'S' :'N',
                        IS_ST_END_BASE: item.IS_ST_END_BASE === 'S' ? 'E' : item.IS_ST_END_BASE === 'E' ? 'S' : 'N'
                    }

                } else {
                    task = _.find(TnATaskList, function (o) { return o.MC_TNA_TASK_ID == item.MC_TNA_TASK_ID; });

                    if (!task) {
                        return;
                    }

                    newItem = {
                        MC_TNA_TMPLT_D_ID: -1,
                        MC_TNA_TASK_ID: item.MC_TNA_TASK_ID,
                        PARENT_TASK_ID: item.PARENT_TASK_ID,
                        STD_DAYS: item.STD_DAYS,
                        IS_PRODUCTION_N: item.IS_PRODUCTION_N,
                        IS_PRODUCTION_B: item.IS_PRODUCTION_B,
                        DayMulTi: item.DayMulTi,
                        IS_START_TASK: task.IS_START_TASK,
                        IS_END_TASK: task.IS_END_TASK,

                        IS_ST_END: 'N',
                        IS_ST_END_BASE:'N'
                    }

                }
            }

            vm.templateDatas.push(newItem);
  

        }
        vm.remove = function (idx) {
            if (idx && idx >= 0) {
                vm.templateDatas.splice(idx, 1);
            }
        }

        function getparentTasksList() {
            return vm.parentTasksList = {
                optionLabel: "-- Select Base Task --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(TnATaskList)
                        }
                    },
                    group: { field: "TA_GRP_NAME_EN" },
                    sort: [{ field: 'MC_TNA_TASK_GRP_ID', dir: 'asc' }, { field: 'MC_TNA_TASK_ID', dir: 'asc' }]
                },
                dataTextField: "TA_TASK_NAME_EN",
                dataValueField: "MC_TNA_TASK_ID"
            };
        }





        function getTaskGroupList() {
            return vm.TaskGroupList = {
                optionLabel: "-- Select Task Name --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                           e.success(TnATaskList)
                        }
                    },

                    group: { field: "TA_GRP_NAME_EN" },
                    sort: [{ field: 'MC_TNA_TASK_GRP_ID', dir: 'asc' }, { field: 'MC_TNA_TASK_ID', dir: 'asc' }],

                },
                dataTextField: "TA_TASK_NAME_EN",
                dataValueField: "MC_TNA_TASK_ID"
            };
        }

        function getTemplateData() {
            if ($stateParams.CPY_TNA_TMPLT_ID && $stateParams.MC_TNA_TMPLT_H_ID == 0) {
                return MrcDataService.treelistData($stateParams.CPY_TNA_TMPLT_ID).then(function (res) {
                    vm.templateDatas = res;
                    templateDataOri = angular.copy(res);
                });


            } else if (!$stateParams.CPY_TNA_TMPLT_ID && $stateParams.MC_TNA_TMPLT_H_ID > 0) {
                return MrcDataService.treelistData($stateParams.MC_TNA_TMPLT_H_ID).then(function (res) {
                    vm.templateDatas = res;
                    templateDataOri = angular.copy(res);
                });

            } else {
                vm.templateDatas = [];
                templateDataOri = [];
            };


        };

        // Move list item Up
        $scope.Up = function (ix) {
            if (ix != 0) {
                var tmp = vm.templateDatas[ix - 1];
                var taskOrderNext = vm.templateDatas[ix].TASK_ORD_NO;
                var taskOrderPrev = vm.templateDatas[ix - 1].TASK_ORD_NO;

                vm.templateDatas[ix - 1] = vm.templateDatas[ix];
                vm.templateDatas[ix - 1].TASK_ORD_NO = taskOrderPrev;
                vm.templateDatas[ix] = tmp;
                vm.templateDatas[ix].TASK_ORD_NO = taskOrderNext;
            }

        };

        // Move list item Down
        $scope.Down = function (ix) {
            if (ix > -1 && ix < vm.templateDatas.length - 1) {
                var taskOrderPrev = vm.templateDatas[ix].TASK_ORD_NO;
                var taskOrderNext = vm.templateDatas[ix + 1].TASK_ORD_NO;

                var tmp = vm.templateDatas[ix + 1];
                vm.templateDatas[ix + 1] = vm.templateDatas[ix];
                vm.templateDatas[ix + 1].TASK_ORD_NO = taskOrderNext;
                vm.templateDatas[ix] = tmp;
                vm.templateDatas[ix].TASK_ORD_NO = taskOrderPrev;
            }
        };

        vm.cancel = function () {
            if (templateDataOri.length > 0) {
                vm.templateDatas = templateDataOri;
            } else {
                return getTemplateData();
            }
        };

        vm.onTaskChange = function (item, IS_BASED) {


            var task = {};

            if (IS_BASED) {
                 
                if (!item.PARENT_TASK_ID) {
                    return;
                }
                task = _.find(TnATaskList, function (o) { return o.MC_TNA_TASK_ID == item.PARENT_TASK_ID; })

                item.IS_PRODUCTION_B = task.IS_PRODUCTION;
            } else {
                if (!item.MC_TNA_TASK_ID) {
                    return;
                }
                task = _.find(TnATaskList, function (o) { return o.MC_TNA_TASK_ID == item.MC_TNA_TASK_ID; })
                item.IS_PRODUCTION_N = task.IS_PRODUCTION;
                item.IS_START_TASK = task.IS_START_TASK;
                item.IS_END_TASK = task.IS_END_TASK;
            }

        };


        vm.submitData = function (formData, token) {

            var TemplateHeader = {};
            var saveData = [];

            if (formData.length == 0) {
                xml = '<trans><trans/>'
            } else {
                saveData = formData.map(function (obj) {
                    return {
                        MC_TNA_TMPLT_D_ID: ($stateParams.CPY_TNA_TMPLT_ID && $stateParams.MC_TNA_TMPLT_H_ID == 0)? -1: obj.MC_TNA_TMPLT_D_ID,
                        TASK_ORD_NO: obj.TASK_ORD_NO,
                        MC_TNA_TASK_ID: obj.MC_TNA_TASK_ID,
                        PARENT_TASK_ID: obj.PARENT_TASK_ID,
                        STD_DAYS: (obj.STD_DAYS || 0) * obj.DayMulTi,
                        IS_ST_END: obj.IS_PRODUCTION_N==='N'? 'N' : (obj.IS_ST_END ||'N'),
                        IS_ST_END_BASE: obj.IS_PRODUCTION_B==='N'? 'N' : (obj.IS_ST_END_BASE ||'N')
                    }
                });

                var xml = MrcDataService.xmlStringShort(saveData);

            }

            TemplateHeader = angular.copy($scope.$parent.TemplateHeader);

            if ($stateParams.CPY_TNA_TMPLT_ID && $stateParams.MC_TNA_TMPLT_H_ID == 0) {
                TemplateHeader['MC_TNA_TMPLT_H_ID'] = -1;
            } else if (!$stateParams.CPY_TNA_TMPLT_ID && $stateParams.MC_TNA_TMPLT_H_ID > 0) {
                TemplateHeader['MC_TNA_TMPLT_H_ID'] = $stateParams.MC_TNA_TMPLT_H_ID;
            };

            TemplateHeader['XML'] = xml;



            return MrcDataService.saveData(TemplateHeader, 'TnaTemplate', token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $state.go($state.current, { MC_TNA_TMPLT_H_ID: res.data.V_MC_TNA_TMPLT_H_ID }, { inherit: false });
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

        }

    }


})();