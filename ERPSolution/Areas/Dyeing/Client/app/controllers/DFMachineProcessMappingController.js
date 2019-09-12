(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DFMachineProcessMappingController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', '$http', DFMachineProcessMappingController]);
    function DFMachineProcessMappingController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog, $http) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);

        vm.form = { DF_MACHINE_ID: '', DF_PROC_TYPE_ID: '', LK_DIA_TYPE_ID: '', NXT_BT_STS_TYPE_LST: [] };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [loadMachineProcessList(), GetMachineList(), GetProcessList(), GetDiaTypeList(), GetBatchStatusTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function GetBatchStatusTypeList() {

            return vm.statusTypeList = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchStatusType').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BT_STS_TYP_NAME",
                dataValueField: "DYE_BT_STS_TYPE_ID"
            };
        };



        function GetMachineList() {

            return vm.machineList = {
                optionLabel: "-- Select Machine --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/dye/DFMachine/SelectAll').then(function (res) {
                                for (var x = 0; x < res.length; x++) {
                                    var SO = res[x].DF_MC_NAME + " (" + res[x].DF_MACHINE_NO + ")";
                                    res[x].DF_MC_NAME = SO;
                                }
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DF_MC_NAME",
                dataValueField: "DF_MACHINE_ID"
            };
        };

        function GetProcessList() {
            return vm.processList = {
                optionLabel: "-- Select Process --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/GetDFProcessType').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DF_PROC_TYPE_NAME",
                dataValueField: "DF_PROC_TYPE_ID"
            };
        };


        function GetDiaTypeList() {
            return vm.diaTypeList = {
                optionLabel: "-- Select Dia Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(67).then(function (res) {
                                e.success(res);
                            });
                        }
                    } //327
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        vm.resetAll = function () {
            vm.form = { DF_MACHINE_ID: '', DF_PROC_TYPE_ID: '', LK_DIA_TYPE_ID: '', DYE_BT_STS_TYPE_ID: '', NXT_BT_STS_TYPE_LST: '' };
        }

        vm.editData = function (dataItem) {

            vm.form = angular.copy(dataItem);
            vm.form["NXT_BT_STS_TYPE_LST"] = dataItem.NXT_BT_STS_TYPE_LST ? dataItem.NXT_BT_STS_TYPE_LST.split(',') : [];

        }

        vm.searchMappingList = function () {
            vm.machineProcessList.read();
        }

        function loadMachineProcessList() {

            return vm.machineProcessList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/GetDFMachineProcessMappingList?pDF_MACHINE_ID=' + (vm.form.DF_MACHINE_ID || null) + '&pDF_PROC_TYPE_ID=' + (vm.form.DF_PROC_TYPE_ID || null) + '&pLK_DIA_TYPE_ID=' + vm.form.LK_DIA_TYPE_ID).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }

        vm.gridOptions = {

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
                { field: "DF_MACHINE_ID", hidden: true },
                { field: "DF_PROC_TYPE_ID", hidden: true },
                { field: "DF_MAP_MCN_PROC_ID", hidden: true },
                { field: "LK_DIA_TYPE_ID", hidden: true },
                { field: "DYE_BT_STS_TYPE_ID", hidden: true },
                { field: "NXT_BT_STS_TYPE_LST", hidden: true },
                { field: "IS_DEFA_FINIS", hidden: true },

                { field: "DF_PROC_TYPE_NAME", title: "Process", type: "string", width: "20%" },
                { field: "DF_MACHINE_NO", title: "Machine No", type: "string", width: "20%" },
                { field: "DF_MC_NAME", title: "Machine Name", type: "string", width: "20%" },
                { field: "DIA_TYPE_NAME", title: "Dia Type", type: "string", width: "15%" },
                { field: "PROC_SEQ", title: "Process Sqeuence", type: "string", width: "15%" },

                {
                    title: "Action",
                    template: function () {
                        return "</a><a ng-click='vm.editData(dataItem)' class='btn btn-xs green'><i class='fa fa-edit'> Edit</i></a> <a ng-click='vm.deleteData(dataItem)' class='btn btn-xs red'><i class='fa fa-remove'> Delete</i></a></a>";
                    },
                    width: "10%"
                }
            ]
        };


        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["NXT_BT_STS_TYPE_LST"] = data.NXT_BT_STS_TYPE_LST ? data.NXT_BT_STS_TYPE_LST.join(',') : '0';

                return DyeingDataService.saveDataByUrl(data, '/DFProduction/SaveMapping').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        vm.form = { DF_MACHINE_ID: '', DF_PROC_TYPE_ID: '', LK_DIA_TYPE_ID: '', DYE_BT_STS_TYPE_ID: '', NXT_BT_STS_TYPE_LST: '' };
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        loadMachineProcessList();
                    }
                });

            }
        };

        vm.deleteData = function (dataOri) {

            var data = angular.copy(dataOri);

            return DyeingDataService.saveDataByUrl(data, '/DFProduction/DeleteMapping').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    vm.form = { DF_MACHINE_ID: '', DF_PROC_TYPE_ID: '', LK_DIA_TYPE_ID: '', DYE_BT_STS_TYPE_ID: '', NXT_BT_STS_TYPE_LST: '' };
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    loadMachineProcessList();
                }
            });

        };

    }


})();

