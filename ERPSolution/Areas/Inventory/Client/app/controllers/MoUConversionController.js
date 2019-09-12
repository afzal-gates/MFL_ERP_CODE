(function () {
    'use strict';
    angular.module('multitex.inventory').controller('MoUConversionController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', 'Dialog', '$http', MoUConversionController]);
    function MoUConversionController($q, config, InventoryDataService, $stateParams, $state, $scope, Dialog, $http) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.form = { IS_ACTIVE: 'Y' };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetRoundMethodList(), GetItemList(), GetMOUList(), GetItemCategoryList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function GetItemList() {

            InventoryDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (vm.form.INV_ITEM_CAT_ID || 0)).then(function (res) {
                vm.itemList = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function GetRoundMethodList() {
            return vm.roundMethodList = {
                optionLabel: "-- Select Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.LookupListData(127).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        function GetMOUList() {
            return vm.mOUList = {
                optionLabel: "-- Select MoU --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        };

        vm.changeCategory = function (e) {
            var item = e.sender.dataItem(e.item);

            InventoryDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (item.INV_ITEM_CAT_ID || 0)).then(function (res) {
                vm.itemList = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function GetItemCategoryList() {

            InventoryDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        vm.resetAll = function () {
            vm.form = { 'INV_ITEM_CAT_ID': '', 'INV_ITEM_ID': '', 'FRM_MOU_ID': '', 'TO_MOU_ID': '', 'LK_ROUND_MTHD_ID': '' };
        };

        vm.editData = function (dataItem) {
            vm.form = angular.copy(dataItem);
            GetItemList();
            vm.form = angular.copy(dataItem);
        }

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/Inv/GetItemMoUConvRate').then(function (res) {
                            e.success(res);
                            vm.OptionCallBack = e;
                        });
                    }
                },
                pageSize: 10
            },
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
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "RF_ITM_MOU_CONV_ID", hidden: true },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "FRM_MOU_ID", hidden: true },
                { field: "TO_MOU_ID", hidden: true },
                { field: "LK_ROUND_MTHD_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Item Description", width: "30%" },
                { field: "FRM_MOU_CODE", title: "From MoU", width: "15%" },
                { field: "TO_MOU_CODE", title: "To MoU", width: "15%" },
                { field: "LK_ROUND_MTHD_NAME", title: "Round Method", width: "15%" },
                { field: "CONV_FACTOR", title: "Factor", width: "10%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.deleteRecord(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle">Delete</i></a> <a  title="Edit" ng-click="vm.editData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit">Edit</i></a>',
                    width: "15%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                return InventoryDataService.saveDataByUrl(data, '/SaveMoUConvRate').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        vm.form = { 'INV_ITEM_CAT_ID': '', 'INV_ITEM_ID': '', 'FRM_MOU_ID': '', 'TO_MOU_ID': '', 'LK_ROUND_MTHD_ID': '' };
                        vm.form.RF_ITM_MOU_CONV_ID = res.data.OPRF_ITM_MOU_CONV_ID;
                        config.appToastMsg(res.data.PMSG);
                        if (vm.OptionCallBack != undefined)
                            vm.gridOptions.dataSource.transport.read(vm.OptionCallBack);
                    }
                });
            }
        };


        vm.deleteRecord = function (dataOri) {

            var data = angular.copy(dataOri);

            return InventoryDataService.saveDataByUrl(data, '/DeleteMoUConvRate').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    vm.form = { 'INV_ITEM_CAT_ID': '', 'INV_ITEM_ID': '', 'FRM_MOU_ID': '', 'TO_MOU_ID': '', 'LK_ROUND_MTHD_ID': '' };
                    if (vm.OptionCallBack != undefined)
                        vm.gridOptions.dataSource.transport.read(vm.OptionCallBack);
                }
            });
        };


    }


})();