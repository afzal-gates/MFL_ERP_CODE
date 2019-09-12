(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeChemicalOpeningBalanceController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', '$filter', 'Dialog', 'commonDataService', 'cur_user_id', DyeChemicalOpeningBalanceController]);
    function DyeChemicalOpeningBalanceController($q, config, DyeingDataService, $stateParams, $state, $scope, $filter, Dialog, commonDataService, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetItemCategoryList(), ItemList(), getCurrencyList(), ReceiveItemList(), GetReqSourceList(), GetCompanyList(),
                GetMOUList(), GetSupplierList(), GetSourceTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.form = { IS_FINALIZED: 'N', QTY_MOU_ID: '3', LK_LOC_SRC_TYPE_ID: 452 };

        vm.getOpeningBlance = function () {

            var grid = $("#IDRequisitionDS").data("kendoGrid");
            var currentPage = grid.dataSource.page();
            ReceiveItemList(currentPage);

            //return vm.ReceiveItemList = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalReceive/GetDYEOpeningBlance/' + (vm.form.HR_COMPANY_ID || 0) + '/' + (vm.form.SCM_STORE_ID || 0) + '/' + (vm.form.INV_ITEM_CAT_ID || 0)).then(function (res) {
            //                e.success(res);
            //            });
            //        }
            //    },
            //    pageSize: 10,
            //    sortable: true
            //});
        }

        vm.TotalReceiveQty = function () {
            //alert(vm.form.QTY_PER_PACK);

            if (vm.form.QTY_PER_PACK <= vm.form.LOOSE_QTY) {
                vm.form.LOOSE_QTY = '';
                vm.form.RCV_QTY = ((vm.form.PACK_RCV_QTY || 0) * vm.form.QTY_PER_PACK) + (vm.form.LOOSE_QTY || 0);
                return;
            }
            if ((vm.form.PACK_RCV_QTY > 0 && vm.form.QTY_PER_PACK > 0) || vm.form.LOOSE_QTY > 0) {

                vm.form.RCV_QTY = ((vm.form.PACK_RCV_QTY || 0) * vm.form.QTY_PER_PACK) + (vm.form.LOOSE_QTY || 0);
            }
            else
                vm.form.RCV_QTY = 0;
        };

        vm.TotalPriceAmount = function () {
            if (vm.form.RCV_QTY > 0 && vm.form.COST_PRICE > 0)
                vm.form.TOTAL_AMT = vm.form.RCV_QTY * vm.form.COST_PRICE;
            else
                vm.form.TOTAL_AMT = 0;
        };

        $scope.OB_RCV_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.OB_RCV_DT_LNopened = true;
        }

        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    DyeingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4) });
                        e.success(list);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.getItemBrandName = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            var list = _.filter(vm.DyItemList.data(), { 'INV_ITEM_ID': obj.INV_ITEM_ID });
            vm.form.BRAND_NAME_EN = list[0].BRAND_NAME_EN;
            vm.form.PACK_MOU_ID = obj.PACK_MOU_ID;
            vm.form.QTY_PER_PACK = obj.PACK_RATIO;
            //vm.form.PCT_ADDL_PRICE = obj.PCT_ADDL_PRICE;

            //vm.form.PCT_ADDL_PRICE = obj.PCT_ADDL_PRICE;


        }

        function ReceiveItemList(currentPage) {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalReceive/GetDYEOpeningBlance/' + (vm.form.HR_COMPANY_ID || 0) + '/' + (vm.form.SCM_STORE_ID || 0) + '/' + (vm.form.INV_ITEM_CAT_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                page: (currentPage || 1),
                pageSize: 10,
                sortable: true
            });
        };

        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                data: []
            });
        };

        function GetItemCategoryList() {

            DyeingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {

                var list = _.filter(res, function (o) { return ((o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4)) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: list
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);

            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 3)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

            vm.getOpeningBlance();

        };


        vm.editItemData = function (data) {
            //vm.form.INV_ITEM_CAT_ID = angular.copy(data.INV_ITEM_CAT_ID);
            //DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + vm.form.INV_ITEM_CAT_ID).then(function (res) {

            //    vm.DyItemList = new kendo.data.DataSource({
            //        data: res
            //    });
            //    vm.form = angular.copy(data);
            //});


            vm.form['INV_ITEM_CAT_ID'] = angular.copy(data.INV_ITEM_CAT_ID);

            vm.DyItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + data.INV_ITEM_CAT_ID).then(function (res) {
                            e.success(res);
                            vm.form = angular.copy(data);
                        });
                    }
                }
            });


            vm.form = angular.copy(data);
        }

        vm.editItemDataFinalize = function (data) {

            Dialog.confirm('Finalizing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var ndata = angular.copy(data);
                     ndata.IS_FINALIZED = 'Y';
                     return DyeingDataService.saveDataByUrl(ndata, '/DyeChemicalReceive/Finalize').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {

                             var grid = $("#IDRequisitionDS").data("kendoGrid");
                             var currentPage = grid.dataSource.page();
                             //alert(currentPage);
                             ReceiveItemList(currentPage);
                         }
                     });
                 });
        }

        vm.resetItemData = function () {

            var HR_COMPANY_ID = vm.form.HR_COMPANY_ID;
            var SCM_STORE_ID = vm.form.SCM_STORE_ID;
            var RF_CURRENCY_ID = vm.form.RF_CURRENCY_ID;
            var OB_RCV_DT = vm.form.OB_RCV_DT;

            vm.form = { 'HR_COMPANY_ID': HR_COMPANY_ID, 'SCM_STORE_ID': SCM_STORE_ID, 'RF_CURRENCY_ID': RF_CURRENCY_ID, 'LK_LOC_SRC_TYPE_ID': 452, 'OB_RCV_DT': OB_RCV_DT, 'DC_ITEM_ID': '', 'PACK_MOU_ID': '', 'QTY_MOU_ID': '3' };

        };

        vm.reset = function () {

            var HR_COMPANY_ID = vm.form.HR_COMPANY_ID;
            var SCM_STORE_ID = vm.form.SCM_STORE_ID;
            var RF_CURRENCY_ID = vm.form.RF_CURRENCY_ID;
            var OB_RCV_DT = vm.form.OB_RCV_DT;

            vm.form = { 'HR_COMPANY_ID': HR_COMPANY_ID, 'SCM_STORE_ID': SCM_STORE_ID, 'RF_CURRENCY_ID': RF_CURRENCY_ID, 'LK_LOC_SRC_TYPE_ID': 452, 'OB_RCV_DT': OB_RCV_DT, 'DC_ITEM_ID': '', 'PACK_MOU_ID': '', 'QTY_MOU_ID': '3' };

        };




        function GetSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "-- Select Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(84).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetReqSourceList() {

            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=3,4&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 || x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        };

        function getCurrencyList() {
            return vm.currencyList = {
                optionLabel: "-- Select Currency --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getCurrencyList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "CURR_NAME_EN",
                dataValueField: "RF_CURRENCY_ID"
            };
        };

        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        function GetMOUList() {
            DyeingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                vm.mOUList = new kendo.data.DataSource({
                    data: res
                });

                vm.pmOUList = new kendo.data.DataSource({
                    data: res
                });
            });

            //return vm.mOUList = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            DyeingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
            //                e.success(res);

            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    }
            //});
        };

        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.gridOptionsItem = {
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },
            pageSize: 10,
            scrollable: true,
            sortable: true,
            columns: [
                { field: "DC_ITEM_ID", hidden: true },
                { field: "SCM_STORE_ID", hidden: true },
                { field: "DYE_DC_RCV_OB_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },
                { field: "LOC_UNIT_PRICE", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },
                { field: "IS_FINALIZED", hidden: true },
                { field: "HR_COMPANY_ID", hidden: true },
                { field: "PCT_ADDL_PRICE", hidden: true },

                { field: "STORE_NAME_EN", title: "Storing In", type: "string", width: "10%" },
                { field: "ITEM_CAT_NAME_EN", title: "Category", type: "string", width: "10%" },

                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "15%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "7%" },
                { field: "PACK_MOU_CODE", title: "Pack Unit", type: "string", width: "8%" },
                { field: "PACK_RCV_QTY", title: "Pack Qty.", type: "string", width: "8%" },
                { field: "QTY_PER_PACK", title: "Qty/Pack", type: "string", width: "8%" },
                { field: "LOOSE_QTY", title: "Loose Qty", type: "string", width: "8%" },

                { field: "RCV_QTY", title: "Total Qty.", type: "string", width: "8%" },
                { field: "MOU_CODE", title: "UoM", type: "string", width: "5%" },
                { field: "COST_PRICE", title: "Cost Price", type: "decimal", width: "8%" },
                //{ field: "TOTAL_AMT", title: "TTL Value", type: "decimal", width: "8%" },
                { field: "REMARKS", title: "Remarks", width: "10%" },

                {
                    title: "",
                    template: '<a  title="Edit" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit">Edit</i></a>  <a  title="Finalize" ng-click="vm.editItemDataFinalize(dataItem)" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs yellow-gold"><i class="fa fa-edit">Finalize</i></a>',
                    width: "10%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                if (!data.LOOSE_QTY) {
                    data.LOOSE_QTY = 0;
                }

                var _ldate = $filter('date')(data.OB_RCV_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["OB_RCV_DT"] = _ldate;

                return DyeingDataService.saveDataByUrl(data, '/DyeChemicalReceive/SaveOB').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        var grid = $("#IDRequisitionDS").data("kendoGrid");
                        var currentPage = grid.dataSource.page();
                        //alert(currentPage);
                        ReceiveItemList(currentPage);
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.form = { "HR_COMPANY_ID": data.HR_COMPANY_ID, "SCM_STORE_ID": data.SCM_STORE_ID, "INV_ITEM_CAT_ID": data.INV_ITEM_CAT_ID, "LK_LOC_SRC_TYPE_ID": 452, "OB_RCV_DT": data.OB_RCV_DT, "DC_ITEM_ID": data.DC_ITEM_ID, "PACK_MOU_ID": data.PACK_MOU_ID, "QTY_MOU_ID": data.QTY_MOU_ID, "BRAND_NAME_EN": data.BRAND_NAME_EN, "QTY_PER_PACK": data.QTY_PER_PACK, "COST_PRICE": data.COST_PRICE };
                        //vm.form['HR_COMPANY_ID'] = '';
                        //vm.form['SCM_STORE_ID'] = '';
                        //vm.form['INV_ITEM_CAT_ID'] = '';

                        //vm.form['DC_ITEM_ID'] = '';
                        //vm.form['PACK_MOU_ID'] = '';

                        //vm.form['BRAND_NAME_EN'] = '';
                        //vm.form['PACK_RCV_QTY'] = '';

                        //vm.form['QTY_PER_PACK'] = '';
                        //vm.form['LOOSE_QTY'] = '';

                        //vm.form['RCV_QTY'] = '';
                        //vm.form['COST_PRICE'] = '';
                        //vm.form['REMARKS'] = '';
                        //vm.form['uid'] = '';
                    }
                });
            }
        };
    }


})();

