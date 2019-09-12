
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeMachineController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', DyeMachineController]);
    function DyeMachineController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);

        vm.form = { DYE_MC_TYPE_ID: '', IS_SMP_BLK: 'B', RF_BRAND_ID: '', C_ORIGIN_ID: '', CAP_MOU_ID: '', LK_MAC_STATUS_ID: '', RF_LOCATION_ID: '' };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [loadMachineList(), getBrandList(), GetMachineTypeList(), getCountryList(), getLocationList(), getMachineStatusList(), GetMOUList(), getMcPrdStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function getMcPrdStatusList() {
            return vm.prdStatus = {
                optionLabel: "-- Select M/C State --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DyeMachine/GetMachineProdStatusList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "MAC_PROD_STS_EN_NAME",
                dataValueField: "RF_MAC_PROD_STS_ID"
            };
        };

        function getCountryList() {
            return vm.countryList = {
                optionLabel: "-- Select Country --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/mrc/buyer/GetCountryList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };
        };

        function getBrandList() {
            return vm.brandList = {
                optionLabel: "-- Select Brand --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BRAND_NAME_EN",
                dataValueField: "RF_BRAND_ID"
            };
        }


        function getMachineStatusList() {

            return vm.machineStatusList = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(98).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getLocationList() {
            return vm.locationList = {
                optionLabel: "-- Select Location --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/LocationList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LOCATION_NAME",
                dataValueField: "RF_LOCATION_ID"
            };
        };

        function GetMachineTypeList() {

            return vm.machineTypeList = {
                optionLabel: "-- Select Machine Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/dye/DyeMachine/GetMachineTypeList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DYE_TYPE_NAME_EN",
                dataValueField: "DYE_MC_TYPE_ID"
            };
        };

        function GetMOUList() {
            return vm.mOUList = {
                optionLabel: "-- Select MoU --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        };

            vm.mcLocationOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FLOOR_DESC_EN",
                dataValueField: "HR_PROD_FLR_ID"
            };

            vm.mcLocationDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/hr/GetProdFloorList?pLK_PFLR_TYP_ID=529').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                group: { field: "BLDNG_DESC_EN" },
                sort: [{ field: 'HR_PROD_BLDNG_ID', dir: 'asc' }]
            });


        vm.resetAll = function () {
            vm.form = { DYE_MC_TYPE_ID: '', IS_SMP_BLK: 'B', RF_BRAND_ID: '', C_ORIGIN_ID: '', CAP_MOU_ID: '', LK_MAC_STATUS_ID: '', RF_LOCATION_ID: '' };
        }

        vm.editData = function (dataItem) {

            vm.form = angular.copy(dataItem);

        }

        function loadMachineList() {

            return vm.machineList = new kendo.data.DataSource({
                pageSize: 10,
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/DyeMachine/SelectAll').then(function (res) {
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
                { field: "DYE_MACHINE_ID", hidden: true },
                { field: "DYE_MC_TYPE_ID", hidden: true },
                { field: "C_ORIGIN_ID", hidden: true },
                { field: "CAP_MOU_ID", hidden: true },
                { field: "LK_MAC_STATUS_ID", hidden: true },
                { field: "RF_LOCATION_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "MAX_TEMP_C", hidden: true },
                { field: "IS_SMP_BLK", hidden: true },

                { field: "DYE_MACHINE_NO", title: "Machine No", type: "string", width: "8%" },
                { field: "MODEL_NO", title: "Model", type: "string", width: "8%" },
                { field: "D_PRD_CAPACITY", title: "Capacity", type: "string", width: "5%" },
                { field: "PCT_EFFCNCY", title: "Efficency", type: "string", width: "8%" },
                { field: "MC_SERIAL_NO", title: "Serial", type: "string", width: "8%" },
                { field: "MFG_YEAR", title: "Year", type: "string", width: "5%" },
                { field: "MC_RPM", title: "RPM", type: "string", width: "5%" },
                { field: "NO_NOZLE", title: "Nozle", type: "string", width: "5%" },
                { field: "DYE_TYPE_NAME_EN", title: "Type", type: "string", width: "8%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "8%" },
                { field: "COUNTRY_NAME_EN", title: "Country", type: "string", width: "8%" },
                { field: "MOU_CODE", title: "Unit", type: "string", width: "5%" },
                { field: "LOCATION_NAME", title: "Location", type: "string", width: "8%" },
                { field: "MAC_STATUS_NAME", title: "Status", type: "string", width: "5%" },

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

                return DyeingDataService.saveDataByUrl(data, '/DyeMachine/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        vm.form = { DYE_MC_TYPE_ID: '', IS_SMP_BLK:'B', RF_BRAND_ID: '', C_ORIGIN_ID: '', CAP_MOU_ID: '', LK_MAC_STATUS_ID: '', RF_LOCATION_ID: '' };
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        loadMachineList();
                    }
                });
            }
        };

        vm.deleteData = function (dataOri) {

            var data = angular.copy(dataOri);

            return DyeingDataService.saveDataByUrl(data, '/DyeMachine/Delete').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {

                    vm.form = { DYE_MC_TYPE_ID: '', IS_SMP_BLK: 'B', RF_BRAND_ID: '', C_ORIGIN_ID: '', CAP_MOU_ID: '', LK_MAC_STATUS_ID: '', RF_LOCATION_ID: '' };
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    loadMachineList();
                }
            });

        };

    }


})();

