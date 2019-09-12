(function () {
    'use strict';
    angular.module('multitex.inventory').controller('StoreProfileController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', 'Dialog', '$http', StoreProfileController]);
    function StoreProfileController($q, config, InventoryDataService, $stateParams, $state, $scope, Dialog, $http) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.form = { IS_ACTIVE: 'Y' };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetOfficeList(), GetStoreTypeList(), GetItemCategoryList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.getAddressInfo = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            vm.form.ADDRESS_EN = obj.ADDRESS_EN;
        }

        function GetOfficeList() {

            InventoryDataService.getDataByFullUrl('/Hr/HrOffice/OfficeListData').then(function (res) {
                vm.OfficeList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        function GetItemCategoryList() {

            vm.itemCategoryList = {
                optionLabel: "-- Select Category--",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return InventoryDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                                var cList = _.filter(res, { 'ITEM_CAT_LEVEL': 1 });
                                e.success(cList);
                            }, function (err) {
                                console.log(err);
                            })
                        }
                    }
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        }

        function GetStoreTypeList() {
            return vm.storeTypeList = {
                optionLabel: "-- Select Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.LookupListData(100).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        vm.resetAll = function () {
            vm.form = { 'HR_OFFICE_ID': '', 'LK_WH_TYPE_ID': '', 'IS_ACTIVE': 'Y', 'INV_ITEM_CAT_LST': '' };
        };


        vm.editAddressData = function (data) {
            var item = angular.copy(data);
            var cList = item.INV_ITEM_CAT_LST ? item.INV_ITEM_CAT_LST.split(',') : [];
            vm.form = item;
            vm.form['INV_ITEM_CAT_LST'] = cList;
        }


        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/inv/StoreProfile/SelectAll').then(function (res) {
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
                { field: "SCM_STORE_ID", hidden: true },
                { field: "LK_WH_TYPE_ID", hidden: true },
                { field: "HR_OFFICE_ID", hidden: true },
                { field: "STORE_CODE", title: "Code", width: "10%" },
                { field: "STORE_NAME_EN", title: "Name (EN)", width: "15%" },
                { field: "STORE_NAME_BN", title: "Name (BN)", width: "15%" },
                { field: "STORE_TYPE_NAME", title: "Store Type", width: "8%" },
                { field: "OFFICE_NAME_EN", title: "Office Name", width: "15%" },
                { field: "ADDRESS_EN", title: "Address", width: "15%" },
                { field: "INV_ITEM_CAT_LST", title: "Category", width: "8%" },
                { field: "IS_ACTIVE", title: "Status", width: "8%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.deleteStoreInfo(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle">Delete</i></a> <a  title="Edit" ng-click="vm.editAddressData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit">Edit</i></a>',
                    width: "8%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data['INV_ITEM_CAT_LST'] = !data.INV_ITEM_CAT_LST ? '0' : data.INV_ITEM_CAT_LST.join(',');

                return InventoryDataService.saveDataByUrl(data, '/StoreProfile/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        vm.form.SCM_STORE_ID = res.data.OPSCM_STORE_ID;
                        config.appToastMsg(res.data.PMSG);
                        if (vm.OptionCallBack != undefined)
                            vm.gridOptions.dataSource.transport.read(vm.OptionCallBack);
                    }
                });
            }
        };


        vm.deleteStoreInfo = function (dataOri) {

            var data = angular.copy(dataOri);

            return InventoryDataService.saveDataByUrl(data, '/StoreProfile/Delete').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);

                    if (vm.OptionCallBack != undefined)
                        vm.gridOptions.dataSource.transport.read(vm.OptionCallBack);
                }
            });
        };


    }


})();