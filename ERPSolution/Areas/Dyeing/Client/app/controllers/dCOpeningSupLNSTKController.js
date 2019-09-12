(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCOpeningSupLNSTKController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'Dialog', '$filter', 'cur_user_id', DCOpeningSupLNSTKController]);
    function DCOpeningSupLNSTKController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, Dialog, $filter, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetItemCategoryList(), ItemList(), getCurrencyList(), ReceiveItemList(), GetReqSourceList(), GetCompanyList(), GetMOUList(), GetSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.form = { IS_FINALIZED: 'N', QTY_MOU_ID: '3' };


        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAllByCat/3,4').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.getOpeningBlance = function (e) {

            var obj = e.sender.dataItem(e.item);
            if (obj.SCM_SUPPLIER_ID > 0) {
                var grid = $("#IDRequisitionDS").data("kendoGrid");
                var currentPage = grid.dataSource.page();
                //alert(currentPage);
                ReceiveItemList(currentPage);
                //return vm.ReceiveItemList = new kendo.data.DataSource({
                //    transport: {
                //        read: function (e) {
                //            DyeingDataService.getDataByFullUrl('/api/Dye/DCSupLNSTK/SelectOpeningLN?SCM_SUPPLIER_ID=' + (obj.SCM_SUPPLIER_ID || 0)).then(function (res) {
                //                e.success(res);
                //            });
                //        }
                //    },
                //    pageSize: 20,
                //    sortable: true
                //});
            }
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

        $scope.OP_BAL_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.OP_BAL_DT_LNopened = true;
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
        }

        function ReceiveItemList(currentPage) {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DCSupLNSTK/SelectOpeningLN?SCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                page: (currentPage || 1),
                pageSize: 20,
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

        };


        vm.editItemData = function (data) {

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
        }

        vm.resetItemData = function () {

            var SCM_SUPPLIER_ID = vm.form.SCM_SUPPLIER_ID;
            var dt = vm.form.OP_BAL_DT;
            vm.form = { 'SCM_SUPPLIER_ID': '', 'OP_BAL_DT': dt, 'INV_ITEM_CAT_ID': '', 'DC_ITEM_ID': '', 'PACK_MOU_ID': '', 'QTY_MOU_ID': '3', 'IS_FINALIZED': 'N' };

        };

        vm.reset = function () {

            //var HR_COMPANY_ID = vm.form.HR_COMPANY_ID;
            //var SCM_STORE_ID = vm.form.SCM_STORE_ID;
            //var RF_CURRENCY_ID = vm.form.RF_CURRENCY_ID;
            //var OB_RCV_DT = vm.form.OB_RCV_DT;
            var SCM_SUPPLIER_ID = vm.form.SCM_SUPPLIER_ID;
            var dt = vm.form.OP_BAL_DT;

            vm.form = { 'SCM_SUPPLIER_ID': '', 'OP_BAL_DT': dt, 'INV_ITEM_CAT_ID': '', 'DC_ITEM_ID': '', 'PACK_MOU_ID': '3', 'QTY_MOU_ID': '3', 'IS_FINALIZED': 'N' };
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
            //return vm.reqSourceList = {
            //    optionLabel: "-- Select Store --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
            //                    var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 4 || x.SCM_STORE_ID == 6 });
            //                    e.success(list);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "STORE_NAME_EN",
            //    dataValueField: "SCM_STORE_ID"
            //};
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
                { field: "DYE_SUP_LN_STK_ID", hidden: true },
                { field: "SCM_SUPPLIER_ID", hidden: true },
                { field: "DC_ITEM_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },
                { field: "OP_BAL_DT", hidden: true },

                { field: "SUP_TRD_NAME_EN", title: "Supplier Name", type: "string", width: "10%" },
                //{ field: "ITEM_CAT_NAME_EN", title: "Category", type: "string", width: "10%" },

                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "15%" },
                { field: "LN_RCV_QTY", title: "LN Rcvd", type: "string", width: "7%" },
                //{ field: "LN_PAY_QTY", title: "LN Rcvd Rtn", type: "string", width: "8%" },
                //{ field: "LN_PAY_ADJ_QTY", title: "Rcvd Rtn Adj.", type: "string", width: "8%" },
                { field: "LN_ISS_QTY", title: "LN ISSUE", type: "string", width: "8%" },
                //{ field: "LN_ISS_RTN_QTY", title: "LN Iss Rtn", type: "string", width: "8%" },

                //{ field: "LN_RTN_ADJ_QTY", title: "LN Rtn Adj.", type: "string", width: "8%" },
                { field: "MOU_CODE", title: "UoM", type: "string", width: "5%" },
                { field: "IS_FINALIZED", title: "Finalized", type: "string", width: "5%" },

                {
                    title: "",
                    template: '<a  title="Edit" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit">Edit</i></a> ' +
                        '<a  title="Edit" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" ng-click="vm.submitFinal(dataItem)" class="btn btn-xs yellow-gold"><i class="fa fa-edit">finalized</i></a>',
                    width: "15%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                if (!data.IS_OP_BAL)
                    data.IS_OP_BAL = 'Y';
                if (data.LN_RCV_QTY > 0 && data.LN_ISS_QTY > 0) {

                    config.appToastMsg("MULTI-005 You Cannot put receive & issue loan qty at the same time");
                    return;

                }
                var SCM_SUPPLIER_ID = data.SCM_SUPPLIER_ID;
                var dt = data.OP_BAL_DT;
                var _isudate = $filter('date')(data.OP_BAL_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["OP_BAL_DT"] = _isudate;

                return DyeingDataService.saveDataByUrl(data, '/DCSupLNSTK/SaveLNOB').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        var grid = $("#IDRequisitionDS").data("kendoGrid");
                        var currentPage = grid.dataSource.page();
                        //alert(currentPage);
                        ReceiveItemList(currentPage);
                        //grid.dataSource.page(currentPage);
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        //vm.form = { 'SCM_SUPPLIER_ID': '', 'OP_BAL_DT': dt, 'INV_ITEM_CAT_ID': '', 'DC_ITEM_ID': '', 'PACK_MOU_ID': '', 'QTY_MOU_ID': '3', 'IS_FINALIZED': 'N' };
                        vm.form = { "IS_FINALIZED": 'N', "QTY_MOU_ID": '3', "OP_BAL_DT": dt, "SCM_SUPPLIER_ID": data.SCM_SUPPLIER_ID, "INV_ITEM_CAT_ID": data.INV_ITEM_CAT_ID, 'DC_ITEM_ID': '', 'PACK_MOU_ID': '3' };

                        vm.form['uid'] = '';
                    }
                });
            }
        };

        vm.submitFinal = function (dataOri) {

            Dialog.confirm('Warning! Are you sure that this is finally saved.', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     var data = angular.copy(dataOri);
                     if (!data.IS_OP_BAL)
                         data.IS_OP_BAL = 'Y';

                     data.IS_FINALIZED = 'Y';
                     var _isudate = $filter('date')(data.OP_BAL_DT, 'yyyy-MM-ddTHH:mm:ss');
                     data["OP_BAL_DT"] = _isudate;

                     return DyeingDataService.saveDataByUrl(data, '/DCSupLNSTK/SaveLNOB').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {

                             vm.form = { "IS_FINALIZED": 'N', "QTY_MOU_ID": '3', "OP_BAL_DT": data.OP_BAL_DT, "SCM_SUPPLIER_ID": data.SCM_SUPPLIER_ID, "INV_ITEM_CAT_ID": data.INV_ITEM_CAT_ID, 'PACK_MOU_ID': '3' };
                             var grid = $("#IDRequisitionDS").data("kendoGrid");
                             var currentPage = grid.dataSource.page();
                             //alert(currentPage);
                             ReceiveItemList(currentPage);
                             res['data'] = angular.fromJson(res.jsonStr);

                             config.appToastMsg(res.data.PMSG);
                         }
                     });
                 });
        };

    }


})();

