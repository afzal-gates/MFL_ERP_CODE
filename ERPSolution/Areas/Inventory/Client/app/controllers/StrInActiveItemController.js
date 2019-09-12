
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('StrInActiveItemController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'Dialog', '$filter', '$timeout', StrInActiveItemController]);
    function StrInActiveItemController($q, config, InventoryDataService, $stateParams, $state, $scope, commonDataService, Dialog, $filter, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.IS_NEXT = 'Y';
        vm.form = { QTY_MOU_ID: 1 };
        
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getCategoryList(), ItemList(), StoreItemList(), GetStoreList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function GetStoreList() {
            return vm.storeList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };
        
        function StoreItemList() {

            vm.ItemGridDataSource = new kendo.data.DataSource({
                serverPaging: false,
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/inv/Item/StrInactiveItemByID?pSCM_STORE_ID=' + (vm.form.SCM_STORE_ID || 0);
                        
                        InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 20,
            });
        };
        
        vm.gridOptionsItem = {
            pageable: true,
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
            scrollable: true,
            enableRowSelection: true,
            enableSelectAll: true,
            columns: [
                { field: "DYE_CFG_SUB_STR_ITM_IA_ID", hidden: true },
                { field: "SCM_STORE_ID", hidden: true },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },

                { field: "ITEM_CAT_NAME_EN", title: "Category", type: "string", width: "20%" },
                { field: "ITEM_CODE", title: "Code", type: "string", width: "10%" },
                { field: "ITEM_NAME_EN", title: "Item Name(EN)", type: "string", width: "25%" },
                { field: "ITEM_NAME_BN", title: "Item Name(BN)", type: "string", width: "25%" },
                {
                    title: "Action",
                    template: '<a title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"> Delete</i></a>' +
                        ' <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit">Edit</i></a> ',
                    width: "8%"
                }
            ],
            //editable: true
        };


        function getCategoryList() {
            vm.categoryOptions = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID",
                dataBound: function (e) {
                    //var ds = e.sender.dataSource.data();
                    //if (ds.length == 1) {
                    //    e.sender.value(ds[0].INV_ITEM_CAT_ID);
                    //    vm.form.INV_ITEM_CAT_ID = ds[0].INV_ITEM_CAT_ID;
                    //    vm.itemDataSource.read();
                    //}         
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;

                    vm.itemDataSource.read();
                }
            };

            return vm.categoryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/inv/ItemCategory/SelectAll';

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function ItemList() {
            vm.itemOptions = {
                optionLabel: "-- Select Item --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID"                
            };

            return vm.itemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=3,4,5';

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        vm.editItemData = function (item) {
            var data = angular.copy(item);
            //vm.form.INV_ITEM_CAT_ID = data.INV_ITEM_CAT_ID;
            //vm.itemDataSource.read();
            vm.form.DYE_CFG_SUB_STR_ITM_IA_ID = data.DYE_CFG_SUB_STR_ITM_IA_ID;
            vm.form.INV_ITEM_ID = data.INV_ITEM_ID;
        };


        vm.loadItemList = function () {
            vm.ItemGridDataSource.read();
        }

        vm.removeItemData = function (dataOri) {

            Dialog.confirm('Removing "' + dataOri.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var data = angular.copy(dataOri);

                     return InventoryDataService.saveDataByUrl(data, '/Item/StrInactiveItemDelete').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             vm.ItemGridDataSource.read();
                             config.appToastMsg(res.data.PMSG);
                         }
                     });
                 });
        }

        vm.reset = function () {
            var data = angular.copy(vm.form);
            vm.form = {
                SCM_STORE_ID: data.SCM_STORE_ID,
                //INV_ITEM_CAT_ID: data.INV_ITEM_CAT_ID,
                INV_ITEM_ID: ''
            };
        };


        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                data['OB_RCV_DT'] = $filter('date')(vm.form.OB_RCV_DT, vm.dtFormat);

                return InventoryDataService.saveDataByUrl(data, '/Item/StrInactiveItemSave').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        vm.ItemGridDataSource.read();
                        vm.reset();
                        config.appToastMsg(res.data.PMSG);
                    }
                });
            }
        };
        
        vm.printOB = function () {
            vm.form.REPORT_CODE = 'RPT-3556';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        };

    }
})();

