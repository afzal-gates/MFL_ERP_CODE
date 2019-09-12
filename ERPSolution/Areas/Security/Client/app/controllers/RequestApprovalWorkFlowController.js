(function () {
    'use strict';

    angular.module('multitex.security').controller('RequestApprovalWorkFlowController', ['$state', 'logger', 'config', '$q', '$scope', '$http', '$stateParams', '$filter', 'SecurityService', '$modal', RequestApprovalWorkFlowController]);

    function RequestApprovalWorkFlowController($state, logger, config, $q, $scope, $http, $stateParams, $filter, SecurityService, $modal) {

        var vm = this;

        $scope.format = config.appDateFormat;
        vm.currentDate = $filter('date')(new Date(), config.appDateFormat);


        vm.form = $stateParams.data == null ? {} : angular.extend($stateParams.data, {
            MEMORABLE_TEXT_REPEATED: $stateParams.data.MEMORABLE_TEXT,
            USER_EXPIRE_ON: $filter('date')(moment($stateParams.data.USER_EXPIRE_ON)._d, config.appDateFormat)

        });

        vm.errors = {};

        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;

        function activate() {
            var promise = [getActionTypeStatusList(), parentActionTypeStatusList(), getUserData(), getUserListData(), getSectionList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        function getSectionList() {
            SecurityService.getDataByFullUrl('/api/mrc/TnaTaskStatus/GetDepartmentList').then(function (res) {
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
                            SecurityService.getUserDatalistByApi().then(function (res) {
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

        function getUserListData() {
            vm.multiUserList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        SecurityService.getUserDatalistByApi().then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        }

        vm.openActionType = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ActionType.html',
                controller: function ($scope, $modalInstance) {
                    $scope.form = {};
                    //$scope.LoanItemList = LoanItemList;
                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.gridOptions = {
                        dataSource: new kendo.data.DataSource({
                            serverPaging: false,
                            serverSorting: false,
                            serverFiltering: false,
                            pageSize: 10,
                            transport: {
                                read: function (e) {
                                    SecurityService.getDataByFullUrl('/api/security/RequestApprovalWorkFlow/ActionTypeAll').then(function (res) {
                                        e.success(res);
                                    });
                                }
                            }
                        }),
                        filterable: {
                            extra: false,
                            operators: {
                                string: {
                                    contains: "Contains"
                                }
                            }
                        },
                        pageable: {
                            refresh: true,
                            pageSizes: true,
                            buttonCount: 5
                        },
                        sortable: true,
                        columns: [

                            { field: "RF_ACTN_TYPE_ID", hidden: true },
                            { field: "ACTN_TYPE_CODE", title: "Code", type: "string", width: "10%" },
                            { field: "ACTN_TYPE_NAME", title: "Action Type Name", type: "string", width: "25%" },
                            { field: "ACTN_TYPE_SNAME", title: "Short Name", type: "string", width: "20%" },
                            { field: "PAGE_URL", title: "Page URL", type: "string", width: "25%" },
                            { field: "IS_ACTIVE", title: "Status", type: "string", width: "10%" },

                            {
                                title: "",
                                template: '<a  title="Delete" ng-click="removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>  <a  title="Edit" ng-click="editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                                width: "10%"
                            }
                        ]
                    };

                    $scope.editItemData = function (itemData) {
                        $scope.form = angular.copy(itemData);
                    }

                    $scope.reset = function (aa) {
                        $scope.form = {};
                    }

                    $scope.removeItemData = function (data) {

                        return SecurityService.saveDataByFullUrl(data, '/api/security/RequestApprovalWorkFlow/DeleteAT').then(function (res) {

                            if (res.success === false) {
                                vm.errors = res.errors;
                            }
                            else {

                                res['data'] = angular.fromJson(res.jsonStr);
                                config.appToastMsg(res.data.PMSG);
                                $scope.webapiGrid.dataSource.read();
                            }
                        });
                    }


                    $scope.submitAll = function (data) {

                        return SecurityService.saveDataByFullUrl(data, '/api/security/RequestApprovalWorkFlow/SaveAT').then(function (res) {

                            if (res.success === false) {
                                vm.errors = res.errors;
                            }
                            else {

                                res['data'] = angular.fromJson(res.jsonStr);
                                config.appToastMsg(res.data.PMSG);
                                $scope.webapiGrid.dataSource.read();
                            }
                        });

                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                //resolve: {
                //    LoanItemList: function (SecurityService) {
                //        return SecurityService.getDataByFullUrl('/api/security/RequestApprovalWorkFlow/ActionTypeAll');
                //    }
                //}
            });

            modalInstance.result.then(function (data) {

                vm.ActionTypeList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            SecurityService.getDataByFullUrl('/api/security/RequestApprovalWorkFlow/ActionTypeAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    },
                    pageSize: 10
                });


            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        };

        vm.ActionTypeList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    SecurityService.getDataByFullUrl('/api/security/RequestApprovalWorkFlow/ActionTypeAll').then(function (res) {
                        e.success(res);
                    });
                }
            },
            pageSize: 10
        });

        vm.getActionTypeList = function (dataItem) {
            var item = dataItem;
            vm.form = { 'RF_ACTN_TYPE_ID': item.RF_ACTN_TYPE_ID };

            vm.showSplash = true;

            SecurityService.getDataByFullUrl('/api/security/RequestApprovalWorkFlow/GetActionStatusByID?pRF_ACTN_TYPE_ID=' + item.RF_ACTN_TYPE_ID).then(function (res) {
                vm.ActionTypeStatusList = new kendo.data.DataSource({
                    data: res
                });
                vm.parentList = res
                vm.showSplash = false;
            }, function (err) {
                console.log(err);
                vm.showSplash = false;
            });
        }

        function getActionTypeStatusList() {
            SecurityService.getDataByFullUrl('/api/security/RequestApprovalWorkFlow/GetActionStatusByID?pRF_ACTN_TYPE_ID=' + (vm.form.RF_ACTN_TYPE_ID || 0)).then(function (res) {
                vm.ActionTypeStatusList = new kendo.data.DataSource({
                    data: res
                });
            }, function (err) {
                console.log(err);
            });

        }

        function parentActionTypeStatusList() {
            vm.parentList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        SecurityService.getDataByFullUrl('/api/security/RequestApprovalWorkFlow/GetActionStatusByID?pRF_ACTN_TYPE_ID=' + (vm.form.RF_ACTN_TYPE_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };


        vm.resetAll = function () {
            var x = vm.form.RF_ACTN_TYPE_ID;
            vm.form = { RF_ACTN_TYPE_ID: x, HR_DEPARTMENT_ID: '', PARENT_ID: '', ASSIGNED_TO_LST: [], EXEC_BY_ID: '' };
        }

        vm.editActionStatus = function (dataItem) {

            vm.form = angular.copy(dataItem);
            var List = vm.form.ASSIGNED_TO_LST ? vm.form.ASSIGNED_TO_LST.split(',') : [];
            vm.form.ASSIGNED_TO_LST = List;
        }

        vm.gridOptionsActionStatus = {
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
                { field: "RF_ACTN_STATUS_ID", hidden: true },
                { field: "RF_ACTN_TYPE_ID", hidden: true },
                { field: "PARENT_ID", hidden: true },
                { field: "HR_DEPARTMENT_ID", hidden: true },
                { field: "ALT_ASSIGNED_TO_ID", hidden: true },
                { field: "EXEC_BY_ID", hidden: true },
                { field: "ACTN_ROLE_FLAG", hidden: true },
                { field: "APROVER_LVL_NO", hidden: true },
                { field: "IS_NOTIFY_EMAIL", hidden: true },
                { field: "ACTN_STATUS_NAME", title: "Action Name", width: "20%" },
                { field: "ASSIGNED_TO_LST", title: "Assign To", width: "10%" },
                { field: "ACTION_SORT", title: "Order", width: "8%" },
                { field: "EVENT_NAME", title: "Message", width: "15%" },
                { field: "NXT_ACTION_NAME", title: "Next Action", width: "15%" },
                { field: "DEPARTMENT_NAME_EN", title: "Department", width: "10%" },
                { field: "IS_REPEAT", title: "Repeat", width: "8%" },
                { field: "IS_ACTIVE", title: "Active", width: "8%" },
                { field: "ALT_USER_NAME", title: "Alternet User", width: "15%" },

                {
                    title: "",
                    template: '<a  title="Edit" ng-click="vm.editActionStatus(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "7%"
                }
            ],
            //editable: true
        };

        vm.submitData = function (data) {
            var x = vm.form.RF_ACTN_TYPE_ID;
            if (fnValidate() == true) {


                data['ASSIGNED_TO_LST'] = !data.ASSIGNED_TO_LST ? '0' : data.ASSIGNED_TO_LST.join(',');


                return SecurityService.saveDataByFullUrl(data, '/api/security/RequestApprovalWorkFlow/SaveAS').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        vm.form = { RF_ACTN_TYPE_ID: x, HR_DEPARTMENT_ID: '', PARENT_ID: '', ASSIGNED_TO_LST: [], EXEC_BY_ID: '' };
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        getActionTypeStatusList();
                        parentActionTypeStatusList();
                    }
                });
            }
        }
    }


})();