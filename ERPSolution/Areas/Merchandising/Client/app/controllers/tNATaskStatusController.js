(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TNATaskStatusController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', TNATaskStatusController]);
    function TNATaskStatusController($q, config, MrcDataService, $stateParams, $state, $scope, logger) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.form = {};
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getTnaTaskList(), getTnaTaskStatusList(), parentTnaTaskStatusList(), getSectionList(), getUserData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.IsChange = false;
                },
                
                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

            //$("#customers").kendoDropDownList(vm.userList);
        }

        function convertValues(value) {
            var data = {};

            value = $.isArray(value) ? value : [value];

            for (var idx = 0; idx < value.length; idx++) {
                data["values[" + idx + "]"] = value[idx];
            }

            return data;
        }

        function getTnaTaskList() {
            MrcDataService.getDataByFullUrl('/api/mrc/TnaTask/SelectAll').then(function (res) {
                vm.TnaTaskList = new kendo.data.DataSource({
                    data: res,
                    pageSize: 15
                });
                //vm.TnaTaskList = res;
            }, function (err) {
                console.log(err);
            });

        }

        function getSectionList() {
            MrcDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/GetDepartmentList').then(function (res) {
                var list = res.map(function (o) {
                    return {
                        DEPARTMENT_NAME_EN: [o.PARENT_NAME, o.DEPARTMENT_NAME_EN].join(' - '),
                        HR_DEPARTMENT_ID: o.HR_DEPARTMENT_ID,
                        IS_LEAF: o.IS_LEAF
                    }
                });
                vm.sectionList = new kendo.data.DataSource({
                    //  data: list
                    data: _.filter(list, { 'IS_LEAF': 'Y' })
                });
            }, function (err) {
                console.log(err);
            });
        }

        function getTnaTaskStatusList() {
            MrcDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectByTnaID?pMC_TNA_TASK_ID=0').then(function (res) {
                vm.TnaTaskStatusList = res;
            }, function (err) {
                console.log(err);
            });

        }

        function parentTnaTaskStatusList() {
            vm.parentList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectByTnaID?pMC_TNA_TASK_ID=0').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.getTnaTaskStatusList = function (dataItem) {
            //vm.form.MC_TNA_TASK_ID = data.MC_TNA_TASK_ID;
            var itemss = dataItem;

            vm.form = { 'MC_TNA_TASK_ID': itemss.MC_TNA_TASK_ID };

            vm.showSplash = true;

            MrcDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectByTnaID?pMC_TNA_TASK_ID=' + itemss.MC_TNA_TASK_ID).then(function (res) {
                vm.TnaTaskStatusList = res;
                vm.parentList = res
                vm.showSplash = false;
            }, function (err) {
                console.log(err);
                vm.showSplash = false;
            });
            //
        }

        vm.filterTaskList = function () {
            if (vm.form.TASK_NAME.length >= 3)
                vm.TnaTaskList.query({ filter: { field: 'TA_TASK_NAME_EN', operator: 'contains', value: vm.form.TASK_NAME }, pageSize: 15, page: 1 });
            else if (vm.form.TASK_NAME.length == 0)
                vm.TnaTaskList.query({ filter: {}, pageSize: 15, page: 1 });
        }

        vm.editTnaTaskStatus = function (data) {
            vm.form = angular.copy(data);
        }

        vm.reset = function (data) {
            vm.form = { 'MC_TNA_TASK_ID': data.MC_TNA_TASK_ID, 'HR_DEPARTMENT_ID': '', 'PARENT_ID': '' };
        }

        vm.gridOptionsTnaTaskStatus = {
            pageable: false,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            height: '300px',
            scrollable: true,
            sortable: true,
            columns: [
                { field: "MC_TNA_TASK_STATUS_ID", hidden: true },
                { field: "MC_TNA_TASK_ID", hidden: true },
                { field: "PARENT_ID", hidden: true },
                { field: "HR_DEPARTMENT_ID", hidden: true },
                { field: "EVENT_NAME", title: "Event Name", width: "20%", headerTemplate: "<b>Event Name</b>" },
                { field: "TASK_STATUS_NAME", title: "Message", width: "15%", headerTemplate: "<b>Message</b>" },
                { field: "STATUS_ORDER", title: "Order", width: "12%", headerTemplate: "<b>Order</b>" },
                { field: "IS_REPEAT", title: "Repeat", width: "10%", headerTemplate: "<b>Repeat</b>" },
                { field: "ST_END_FLAG", title: "S/E Flag", width: "10%", headerTemplate: "<b>S/E Flag</b>" },
                { field: "IS_ACTIVE", title: "Active", width: "8%", headerTemplate: "<b>Active</b>" },
                { field: "IS_FB_FRM_BUYER", title: "Buyer FB", width: "15%", headerTemplate: "<b>Buyer FB</b>" },
                {
                    title: "",
                    template: '<a  title="Edit" ng-click="vm.editTnaTaskStatus(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "7%"
                }
            ],
            //editable: true
        };


        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var id = vm.form.MC_TNA_TASK_STATUS_ID

                return MrcDataService.saveDataByFullUrl(data, '/api/mrc/TnaTaskStatus/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg("MULTI-001 TNA Task Status has been updated successfully");
                        }
                        else {
                            config.appToastMsg('MULTI-001' + res.data.PMSG);
                        }

                        vm.getTnaTaskStatusList(data);
                    }
                });
            }
        };

    }

})();