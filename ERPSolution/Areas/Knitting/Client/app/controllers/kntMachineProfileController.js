(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntMachineProfileController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'logger', KntMachineProfileController]);
    function KntMachineProfileController($q, config, KnittingDataService, $stateParams, $state, $scope, logger) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';

        vm.form = { KNT_MACHINE_ID: 0, IS_ACTIVE: 'Y' };
        vm.formCopy = {};

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getCountryList(), getItemBrandList(), getMcDiaList(), getMcTypeList(), getLocationList(), getMcGgList(), getMachineList() /*getTreeData()*/];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        };
        
        vm.reset = function () {
            //console.log(vm.formCopy);   
            vm.form.KNT_MACHINE_ID = 0;
            vm.form.KNT_MACHINE_NO = '';
            vm.form.MODEL_NO = '';
            vm.form.MC_SERIAL_NO = '';
            vm.form.D_PRD_CAPACITY = '';
            vm.form.MC_RPM = '';
            vm.form.NO_FEEDER = '';
            vm.form.PCT_EFFCNCY = '';
            vm.form.MFG_YEAR = '';
            vm.form.IS_ACTIVE = 'Y';
            vm.form.KNT_MC_GG_ID_LIST = [];

            vm.onChangeMcGgList();
        };

        vm.TranCancel = function () {
            vm.reset();
        };

        function getGridData() {
            return InventoryDataService.getItemList(vm.form.INV_ITEM_CAT_ID).then(function (res) {
                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    },
                    pageSize: 50,
                });

                $('#itemGrid').data("kendoGrid").setDataSource(dataSource);
            });
        };

        
        function getTreeData() {
            return KnittingDataService.getDataByFullUrl('/api/knit/KnitCommon/KntMcTypeTreeList').then(function (res) {
               

                return vm.thingsOptions = {
                    dataSource: res,
                    autoScroll: true,
                    select: function (e) {
                        reset();
                        var dataItem = this.dataItem(e.node);
                        vm.selectedItem = dataItem;
                        
                        //getGridData();                        
                    }
                };
            });
        };
        
        function getCountryList() {
            vm.countryOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };

            return vm.countryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByUrlNoToken('/api/common/GetCountryList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });            
        };

        function getItemBrandList() {
            vm.brandOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BRAND_NAME_EN",
                dataValueField: "RF_BRAND_ID"                
            };

            return vm.brandDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByUrlNoToken('/api/common/GetItemBrandList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });                       
        };

        function getMcDiaList() {
            vm.mcDiaOption = {
                optionLabel: "-- Select Dia --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MC_DIA",
                dataValueField: "KNT_MC_DIA_ID"
            };

            return vm.mcDiaDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Knit/KnitCommon/getMachineDiaList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });            
        }

        function getMcTypeList() {
            vm.mcTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MC_TYPE_NAME_EN",
                dataValueField: "KNT_MC_TYPE_ID"
            };

            return vm.mcTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Knit/KnitCommon/KntMcTypeList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }

        function getLocationList() {
            vm.mcLocationOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FLOOR_DESC_EN",
                dataValueField: "HR_PROD_FLR_ID"
            };

            return vm.mcLocationDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/hr/GetProdFloorList?pLK_PFLR_TYP_ID=527').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                group: { field: "BLDNG_DESC_EN" },
                sort: [{ field: 'HR_PROD_BLDNG_ID', dir: 'asc' }]
            });
        }

        function getMcGgList() {
            vm.mcDfGgOption = {
                optionLabel: "-- Select GG --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };

            return vm.mcGgDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByUrlNoToken('/api/common/LookupListData/59').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }

        function getMachineList() {           
            return vm.machineListDataSource = new kendo.data.DataSource({                
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/KnitCommon/GetMachineList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 10
            });
        };

        vm.onChangeMachineList = function (dataItem) {

            var item = angular.copy(dataItem);
            vm.form = item;

            vm.onChangeMcGgList();
            console.log(dataItem);
        };
        
        vm.onChangeMcGgList = function () {
            var mcGgList = vm.mcGgDataSource.data();
            var dfMcGg = _.filter(mcGgList, function (ob) {
                return _.some(vm.form.KNT_MC_GG_ID_LIST, function (val) {
                    return ob.LOOKUP_DATA_ID == val;
                });
            });
            
            return vm.mcDfGgDataSource = new kendo.data.DataSource({
                data: dfMcGg
            });
        }
         
        vm.onChangeMcNo = function () {
            var mcNo = angular.copy(vm.form.KNT_MACHINE_NO);
            console.log(mcNo);
            if (mcNo != undefined) {
                var mcIsExists = _.some(vm.machineListDataSource.data(), function (ob) {
                    return mcNo.toUpperCase() == ob.KNT_MACHINE_NO.toUpperCase() && mcNo != ob.KNT_MACHINE_ID;
                });

                if (mcIsExists == true) {
                    vm.errors = [{ error: 'Machine# already exists' }];
                }
                else {
                    vm.errors = undefined;
                }
            }
        };

        vm.Save = function () {
            console.log(vm.form);
            var submitData = angular.copy(vm.form);
            
            return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/KnitCommon/Save').then(function (res) {
                console.log(res);

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.form.KNT_MACHINE_ID = res.data.PKNT_MACHINE_ID_RTN;
                        //vm.reset();
                        vm.machineListDataSource.read();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });


        };

    }

})();




