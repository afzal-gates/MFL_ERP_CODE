
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeingFinishingMachineController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', '$http', '$modal', DyeingFinishingMachineController]);
    function DyeingFinishingMachineController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog, $http, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);

        vm.form = { DF_MC_TYPE_ID: '', IS_SMP_BLK: 'B', RF_BRAND_ID: '', C_ORIGIN_ID: '', CAP_MOU_ID: '', LK_MAC_STATUS_ID: '', RF_LOCATION_ID: '' };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [loadMachineList(), getBrandList(), GetMachineTypeList(), getCountryList(), getLocationList(),
                getMachineStatusList(), GetMOUList(), getFloorLocationList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }




        function getFloorLocationList() {
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
                        DyeingDataService.getDataByFullUrl('/api/hr/GetProdFloorList?pLK_PFLR_TYP_ID=530').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                group: { field: "BLDNG_DESC_EN" },
                sort: [{ field: 'HR_PROD_BLDNG_ID', dir: 'asc' }]
            });
        }
        
        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    DyeingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {

                        e.success([{
                            BRAND_NAME_EN: '--New Brand--',
                            RF_BRAND_ID: -1
                        }].concat(res));

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.removeKeyImage = function () {
            vm.form['MC_IMAGE'] = null;
            angular.element(document.getElementById('MC_IMAGE_FILE')).val(null);
        }

        $scope.$watchGroup(['vm.form.MC_IMAGE_FILE'], function (newVal, OldVal) {
            if (newVal[0]) {
                vm.form["MC_IMAGE"] = newVal[0].base64;
            }
        }, true);


        vm.newBrandEntry = function (e) {
            var itemss = e.sender.dataItem(e.item);
            var aryB = {};
            if (itemss.RF_BRAND_ID == -1) {
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '../../GlobalUI/BrandEntry',
                    controller: function ($modalInstance, $scope, $timeout, DyeingDataService) {
                        var vm = this;
                        vm.form = {};
                        vm.errors = [];
                        BrandGetAll()
                        function BrandGetAll() {

                            DyeingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                                //DyeingDataService.getDataByFullUrl('/api/common/GetCategoryWiseBrandList/' + ((vm.brand.INV_ITEM_CAT_ID == '-- Select Item Category--' ? 0 : vm.brand.INV_ITEM_CAT_ID) || 0)).then(function (res) {
                                vm.brandAllList = new kendo.data.DataSource({
                                    data: res
                                });
                            });
                        };

                        vm.gridBrand = {
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
                            height: '400px',
                            scrollable: true,
                            groupable: false,
                            columns: [

                                { field: "RF_BRAND_ID", hidden: true },
                                { field: "BRAND_NAME_EN", title: "Brand Name (EN)", width: "30%" },
                                { field: "BRAND_NAME_BN", title: "Brand Name (BN)", width: "30%" },
                                { field: "IS_ACTIVE", title: "Is Active", width: "20%" },
                                {
                                    title: "",
                                    template: '<a title="Edit" ng-click="vm.editBrand(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                                    width: "20%"
                                }
                            ],
                            //editable: true

                        };

                        vm.form['BRAND_NAME_EN'] = '';
                        vm.formDisabled = false;


                        vm.cancel = function (data) {
                            aryB = data;
                            $modalInstance.close(vm.brandAllList);
                        };

                        vm.editBrand = function (data) {
                            vm.form = angular.copy(data);
                        }

                        vm.removeBrand = function (data) {

                            if (!data.RF_BRAND_ID || data.RF_BRAND_ID <= 0) {
                                vm.brandAllList.remove(data);
                                return;
                            };

                            Dialog.confirm('Removing "' + data.BRAND_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                                 .then(function () {
                                     vm.brandAllList.remove(data);
                                 });

                        }

                        vm.resetBrandInfo = function () {
                            vm.form = {};
                        };

                        vm.submitDataNewBrand = function (dataOri, token) {
                            var data = angular.copy(dataOri);

                            return DyeingDataService.saveDataByFullUrl(data, '/api/common/BrandSave', token).then(function (res) {
                                if (res.success === false) {
                                    vm.errors = res.errors;
                                }
                                else {
                                    res['data'] = angular.fromJson(res.jsonStr);
                                    config.appToastMsg(res.data.PMSG);

                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                        vm.form['RF_BRAND_ID'] = parseInt(res.data.opRF_BRAND_ID);
                                    }
                                    BrandGetAll();
                                    //vm.form = {};
                                }
                            }).catch(function (message) {
                                exception.catcher('XHR loading Failded')(message);
                            });

                        };


                    },
                    controllerAs: 'vm',
                    size: 'lg',
                    windowClass: 'large-Modal',

                });

                modalInstance.result.then(function (data) {
                    vm.categoryBrandList = data;
                    vm.form.RF_BRAND_ID = aryB.RF_BRAND_ID;

                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });
            }
        }


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
                                e.success([{
                                    BRAND_NAME_EN: '--New Brand--',
                                    RF_BRAND_ID: -1
                                }].concat(res));
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
                            DyeingDataService.getDataByFullUrl('/api/dye/DFMachine/GetMachineTypeList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DF_MC_TYPE_NAME_EN",
                dataValueField: "DF_MC_TYPE_ID"
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

        vm.resetAll = function () {
            vm.form = { DF_MC_TYPE_ID: '', IS_SMP_BLK: 'B', RF_BRAND_ID: '', C_ORIGIN_ID: '', CAP_MOU_ID: '', LK_MAC_STATUS_ID: '', RF_LOCATION_ID: '' };
        }

        vm.editData = function (dataItem) {

            vm.form = angular.copy(dataItem);

        }

        function loadMachineList() {

            return vm.machineList = new kendo.data.DataSource({
                pageSize: 10,
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/DFMachine/SelectAll').then(function (res) {
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
                { field: "DF_MC_TYPE_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "C_ORIGIN_ID", hidden: true },
                { field: "CAP_MOU_ID", hidden: true },
                { field: "LK_MAC_STATUS_ID", hidden: true },
                { field: "RF_LOCATION_ID", hidden: true },

                { field: "DF_MACHINE_NO", title: "Machine No", type: "string", width: "8%" },
                { field: "DF_MC_NAME", title: "Machine Name", type: "string", width: "8%" },
                { field: "MODEL_NO", title: "Model", type: "string", width: "8%" },
                { field: "D_PRD_CAPACITY", title: "Capacity", type: "string", width: "5%" },
                { field: "PCT_EFFCNCY", title: "Efficency", type: "string", width: "8%" },
                { field: "MC_SERIAL_NO", title: "Serial", type: "string", width: "8%" },
                { field: "MFG_YEAR", title: "Year", type: "string", width: "5%" },
                { field: "MC_SPEED", title: "RPM", type: "string", width: "5%" },
                { field: "DF_MC_TYPE_NAME_EN", title: "Type", type: "string", width: "8%" },
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

                return DyeingDataService.saveDataByUrl(data, '/DFMachine/Save').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        vm.form = { DF_MC_TYPE_ID: '', IS_SMP_BLK: 'B', RF_BRAND_ID: '', C_ORIGIN_ID: '', CAP_MOU_ID: '', LK_MAC_STATUS_ID: '', RF_LOCATION_ID: '' };
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        loadMachineList();
                    }
                });

                //$http({
                //    method: 'post',
                //    url: '/Dyeing/Dye/DFMachineSave',
                //    data: data
                //}).success(function (data, status, headers, config1) {
                //    data['data'] = angular.fromJson(data.jsonStr);
                //    config.appToastMsg(data.data.PMSG);

                //    loadMachineList();

                //}).error(function (data, status, headers, config) {
                //    console.log(status);
                //});


            }
        };

        vm.deleteData = function (dataOri) {

            var data = angular.copy(dataOri);

            return DyeingDataService.saveDataByUrl(data, '/DFMachine/Delete').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    vm.removeKeyImage();
                    vm.form = { DF_MC_TYPE_ID: '', IS_SMP_BLK: 'B', RF_BRAND_ID: '', C_ORIGIN_ID: '', CAP_MOU_ID: '', LK_MAC_STATUS_ID: '', RF_LOCATION_ID: '' };
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    loadMachineList();
                }
            });

        };

    }


})();

