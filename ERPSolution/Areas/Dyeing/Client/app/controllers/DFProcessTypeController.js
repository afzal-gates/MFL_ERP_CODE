(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DFProcessTypeController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', '$filter', '$http', 'commonDataService', 'Dialog', DFProcessTypeController]);
    function DFProcessTypeController($q, config, DyeingDataService, $stateParams, $state, $scope, $filter, $http, commonDataService, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = $filter('date')(new Date(), config.appDateFormat);
        vm.MC_FAB_PROC_GRP_ID = 0;

        var key = 'MC_FAB_PROC_GRP_ID';
        vm.form = {};

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getProcessGroupList(), getProcessList(), GetProcessSubGroupList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function getProcessGroupList() {
            return vm.FabProcGrpList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DFProcessType/GetFabProcGrpList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 15
            });
        };

        function getProcessList() {

            DyeingDataService.getDataByFullUrl('/api/Dye/DFProcessType/SelectAll?pMC_FAB_PROC_GRP_ID=0').then(function (res) {

                return vm.processList = new kendo.data.DataSource({
                    data: res
                });
            });
        };


        vm.onChangeFabProcGrp = function (dataItem) {

            var item = angular.copy(dataItem);

            console.log(item);

            vm.form['MC_FAB_PROC_GRP_ID'] = item.MC_FAB_PROC_GRP_ID;

            vm.processList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DFProcessType/SelectAll?pMC_FAB_PROC_GRP_ID=' + item.MC_FAB_PROC_GRP_ID).then(function (res) {

                            e.success(res);
                        });
                    }
                }
            });
        };


        function GetProcessSubGroupList() {

            return vm.processSubGroupList = {
                optionLabel: "-- Select Sub-Group --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(115).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        vm.gridOptions = {
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
            height: '200px',
            scrollable: true,
            columns: [

                { field: "DF_PROC_TYPE_ID", hidden: true },
                { field: "MC_FAB_PROC_GRP_ID", hidden: true },
                { field: "LK_PROC_SUB_GRP_ID", hidden: true },

                { field: "DF_PROC_TYPE_CODE", title: "Process Code", width: "10%" },
                { field: "DF_PROC_TYPE_NAME", title: "Process Name", width: "15%" },
                { field: "DF_PROC_TYPE_SNAME", title: "Short Name", width: "10%" },
                { field: "PROC_SUB_GRP_NAME", title: "Sub Group", width: "15%" },
                { field: "DISPLAY_ORDER", title: "Display Order", width: "10%" },
                { field: "PROC_RATE", title: "Rate", width: "10%" },
                { field: "IS_ACTIVE", title: "Status", width: "10%" },

                {
                    title: "Action",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle">Delete</i></a>  <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit">Edit</i></a>',
                    width: "10%"
                }
            ],
        };

        vm.editItemData = function (data) {
            vm.form = angular.copy(data);
        }

        vm.removeItemData = function (dataOri) {

            Dialog.confirm('Deleting "' + dataOri.DF_PROC_TYPE_NAME + '.', 'Attention', ['Yes', 'No'])
                .then(function () {

                    var data = angular.copy(dataOri);

                    return DyeingDataService.saveDataByUrl(data, '/DFProcessType/Delete').then(function (res) {

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            }
                            vm.onChangeFabProcGrp(data);
                            vm.form = { LK_PROC_SUB_GRP_ID: '', MC_FAB_PROC_GRP_ID: data.MC_FAB_PROC_GRP_ID, IS_ACTIVE: 'N' };
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });

                });
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                return DyeingDataService.saveDataByUrl(data, '/DFProcessType/Save').then(function (res) {

                    vm.errors = undefined;
                    if (res.success === false) {
                        vm.errors = [];
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        }
                        vm.onChangeFabProcGrp(data);
                        vm.form = { LK_PROC_SUB_GRP_ID: '', MC_FAB_PROC_GRP_ID: data.MC_FAB_PROC_GRP_ID, IS_ACTIVE: 'N' };
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        };


        vm.clearAll = function (dataOri) {
            var data = angular.copy(dataOri);            
            vm.form = { LK_PROC_SUB_GRP_ID: '', MC_FAB_PROC_GRP_ID: data.MC_FAB_PROC_GRP_ID, IS_ACTIVE: 'N' };
        }


    }


})();
