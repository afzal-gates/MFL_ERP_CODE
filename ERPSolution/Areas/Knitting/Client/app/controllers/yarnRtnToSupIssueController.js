(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnRtnToSupIssueController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'Dialog', 'cur_user_id', '$filter', YarnRtnToSupIssueController]);
    function YarnRtnToSupIssueController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, Dialog, cur_user_id, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.KNT_YRN_RPL_ISS_H_ID ? formData : { INV_ITEM_CAT_ID: '2', HR_COMPANY_ID: 1, SCM_STORE_ID: 1, SUP_CTRL_DISABLE: 0, ITEM_RET_BY: cur_user_id, ISS_CHALAN_DT: vm.today, RF_ACTN_VIEW: 0 };
        vm.formItem = {};

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [ReturnList(), ReceiveItemList(), getCurrencyList(), getUserData(), GetItemCategoryList(), ItemList(), YarnLotList(), GetReqTypeList(), GetReqStoreList(), GetCompanyList(), GetStatusList(), GetYarnColorGroupList(), GetMOUList(), GetSupplierList(), GetReturnTypeList(), getLCList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getCurrencyList() {
            return vm.currencyList = {
                optionLabel: "-- $ --",
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

        function GetReturnTypeList() {

            return vm.ReturnTypeList = {
                optionLabel: "-- Select Return Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(103).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAllByCat/2').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function getLCList() {
            return vm.lcList = {
                optionLabel: "-- L/C --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/Knit/YarnReturnToSupplier/GetReplacementIssueDtlByID?pKNT_YRN_RPL_ISS_H_ID=' + ($stateParams.pKNT_YRN_RPL_ISS_H_ID || 0)).then(function (res) {
                                console.log(res);
                                var lst = _.uniqBy(res, 'CM_IMP_LC_H_ID');

                                console.log("Afzal");
                                console.log(lst);
                                console.log("Hossain");
                                e.success(lst);
                            });
                        }
                    }
                },
                dataTextField: "IMP_LC_NO",
                dataValueField: "CM_IMP_LC_H_ID"
            };
        }


        function ReturnList() {
            return vm.ReturnItemList = new kendo.data.DataSource({
                //data: []
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Knit/YarnReturnToSupplier/GetReplacementIssueDtlByID?pKNT_YRN_RPL_ISS_H_ID=' + (formData.PARENT_ID || $stateParams.pKNT_YRN_RPL_ISS_H_ID || 0)).then(function (res) {

                            e.success(res);
                        });
                    }
                }
            });
        };

        function GetMOUList() {

            KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                vm.mOUList = new kendo.data.DataSource({
                    data: res
                });
                var list = _.filter(res, function (x) { return x.RF_MOU_ID == 29 || x.RF_MOU_ID == 15 })
                vm.packMoUList = new kendo.data.DataSource({
                    data: list
                });
            });

        };

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.IsChange = false;
                },

                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

        }

        vm.TotalReceiveQty = function () {
            if (vm.formRcvItem.LOOSE_QTY < vm.formRcvItem.QTY_PER_PACK)
                if (vm.formRcvItem.PACK_RCV_QTY > 0 && vm.formRcvItem.QTY_PER_PACK > 0)
                    vm.formRcvItem.RCV_QTY = (vm.formRcvItem.PACK_RCV_QTY * vm.formRcvItem.QTY_PER_PACK) + vm.formRcvItem.LOOSE_QTY;
                else
                    vm.formRcvItem.RCV_QTY = 0;
            else
                vm.formRcvItem.LOOSE_QTY = 0;
        };

        vm.TotalPriceAmount = function () {
            if (vm.formRcvItem.RCV_QTY > 0 && vm.formRcvItem.UNIT_PRICE > 0) {
                vm.formRcvItem.TOTAL_AMT = vm.formRcvItem.RCV_QTY * vm.formRcvItem.UNIT_PRICE;
                vm.formRcvItem.COST_PRICE = vm.formRcvItem.RCV_QTY * vm.formRcvItem.UNIT_PRICE * (vm.form.LOC_CONV_RATE || 1);
            }
            else {
                vm.formRcvItem.TOTAL_AMT = 0;
                vm.formRcvItem.COST_PRICE = 0;
            }
        };


        vm.checkReturnQty = function (item) {
            if (item.STOCK_QTY < item.ISSUE_QTY)
                item.ISSUE_QTY = '';
        };

        vm.loadPendingItemBySupID = function (item) {
            KnittingDataService.getDataByFullUrl('/api/Knit/YarnReturnToSupplier/GetYarnSupRtnReqDtlBySupID?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID).then(function (res) {
                vm.IssueItemList = res;
            });
        }

        vm.form.INV_ITEM_CAT_ID = 2;

        $scope.ISS_CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ISS_CHALAN_DT_LNopened = true;
        }


        $scope.RCV_CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RCV_CHALAN_DT_LNopened = true;
        }
        


        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    KnittingDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetSupplierBrandList?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID).then(function (res) {
                        //KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        //    var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                        e.success(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.SupplierList = new kendo.data.DataSource({
            data: []
        });

        vm.OrderList = new kendo.data.DataSource({
            data: []
        });


        vm.addToRcvGrid = function (isValid) {

            if (fnValidateSub2() == true) {

                var ItemID = vm.formRcvItem.YARN_ITEM_ID;
                var brandID = vm.formRcvItem.RF_BRAND_ID;
                var ColorGrpID = vm.formRcvItem.LK_YRN_COLR_GRP_ID;
                var packID = vm.formRcvItem.PACK_MOU_ID;

                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.INV_ITEM_ID === parseInt(vm.formRcvItem.YARN_ITEM_ID)
                })[0];

                vm.formRcvItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var itemB = _.filter(vm.categoryBrandList.data(), function (o) {
                    return o.RF_BRAND_ID === parseInt(vm.formRcvItem.RF_BRAND_ID)
                })[0];

                vm.formRcvItem.BRAND_NAME_EN = itemB.BRAND_NAME_EN;

                var yColorG = _.filter(vm.yarnColorGroupList.data(), function (o) {
                    return o.LOOKUP_DATA_ID === parseInt(vm.formRcvItem.LK_YRN_COLR_GRP_ID)
                })[0];

                vm.formRcvItem.LK_YRN_COLR_GRP_NAME = yColorG.LK_DATA_NAME_EN;

                var mouUnit = _.filter(vm.mOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formRcvItem.QTY_MOU_ID)
                })[0];


                var packUnit = _.filter(vm.packMoUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formRcvItem.PACK_MOU_ID)
                })[0];

                vm.formRcvItem.MOU_CODE = mouUnit.MOU_CODE;
                vm.formRcvItem.PACK_MOU_CODE = packUnit.MOU_CODE;

                vm.formRcvItem.KNT_YRN_STR_RPL_REQ_D_ID = vm.form.KNT_YRN_STR_RPL_REQ_D_ID;
                vm.formRcvItem.INV_ITEM_CAT_ID = vm.form.INV_ITEM_CAT_ID;
                if (vm.formRcvItem.uid) {
                    // Update

                    var row = vm.ReceiveItemList.getByUid(vm.formRcvItem.uid);
                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.YARN_ITEM_ID === vm.formRcvItem.YARN_ITEM_ID && o.uid != vm.formRcvItem.uid)
                    }).length;

                    if (count <= 1) {
                        row["YARN_ITEM_ID"] = vm.formRcvItem.YARN_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.formRcvItem.ITEM_NAME_EN;
                        row["RF_BRAND_ID"] = vm.formRcvItem.RF_BRAND_ID;
                        row["BRAND_NAME_EN"] = vm.formRcvItem.BRAND_NAME_EN;
                        row["KNT_YRN_RCV_H_ID"] = vm.formRcvItem.KNT_YRN_RCV_H_ID;
                        row["SCM_PURC_REQ_D_ID"] = vm.formRcvItem.SCM_PURC_REQ_D_ID;
                        row["QTY_MOU_ID"] = vm.formRcvItem.QTY_MOU_ID;
                        row["MOU_CODE"] = vm.formRcvItem.MOU_CODE;
                        row["LK_YRN_COLR_GRP_ID"] = vm.formRcvItem.LK_YRN_COLR_GRP_ID;
                        row["LK_YRN_COLR_GRP_NAME"] = vm.formRcvItem.LK_YRN_COLR_GRP_NAME;
                        row["PACK_MOU_ID"] = vm.formRcvItem.PACK_MOU_ID;
                        row["PACK_MOU_CODE"] = vm.formRcvItem.PACK_MOU_CODE;
                        row["YRN_LOT_NO"] = vm.formRcvItem.YRN_LOT_NO;
                        row["KNT_YRN_STR_RPL_REQ_D_ID"] = vm.formRcvItem.KNT_YRN_STR_RPL_REQ_D_ID;


                        row["PACK_RCV_QTY"] = vm.formRcvItem.PACK_RCV_QTY;
                        row["QTY_PER_PACK"] = vm.formRcvItem.QTY_PER_PACK;
                        row["RCV_QTY"] = vm.formRcvItem.RCV_QTY;
                        row["UNIT_PRICE"] = vm.formRcvItem.UNIT_PRICE;
                        row["TOTAL_AMT"] = vm.formRcvItem.TOTAL_AMT;

                        row["CONE_QTY"] = vm.formRcvItem.CONE_QTY;

                        vm.formRcvItem = { 'YARN_ITEM_ID': ItemID, 'QTY_MOU_ID': '3', 'LK_YRN_COLR_GRP_ID': ColorGrpID, 'RF_BRAND_ID': brandID, 'PACK_MOU_ID': packID };
                    }
                } else {
                    //insert

                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.YARN_ITEM_ID === vm.formRcvItem.YARN_ITEM_ID && o.RF_BRAND_ID === vm.formRcvItem.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === vm.formRcvItem.LK_YRN_COLR_GRP_ID && o.YRN_LOT_NO === vm.formRcvItem.YRN_LOT_NO)
                    }).length;

                    if (count == 0) {

                        var idx = vm.ReceiveItemList.data().length + 1;
                        vm.ReceiveItemList.insert(idx, vm.formRcvItem);

                        var gview = vm.ReceiveItemList.data();
                        vm.formRcvItem = { 'YARN_ITEM_ID': ItemID, 'QTY_MOU_ID': '3', 'LK_YRN_COLR_GRP_ID': ColorGrpID, 'RF_BRAND_ID': brandID, 'PACK_MOU_ID': packID };
                        //console.log(gview);
                        //config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
        };

        vm.editRcvItemData = function (data) {
            vm.form['INV_ITEM_CAT_ID'] = angular.copy(data.INV_ITEM_CAT_ID);
            vm.formRcvItem = angular.copy(data);
        }

        vm.removeRcvItemData = function (data) {

            if (!data.KNT_YRN_RCV_D_ID || data.KNT_YRN_RCV_D_ID <= 0) {
                vm.ReceiveItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.ReceiveItemList.remove(data);
                 });

        }

        function ReceiveItemList() {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Knit/YarnReturnToSupplier/GetReplacementReceiveDtlByID?pKNT_YRN_RPL_ISS_H_ID=' + ($stateParams.pKNT_YRN_RPL_ISS_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.gridOptionsRcvItem = {
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
            height: '300px',
            scrollable: true,
            columns: [
                { field: "YARN_ITEM_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "KNT_YRN_RCV_H_ID", hidden: true },
                { field: "KNT_YRN_RCV_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },
                { field: "LK_YRN_COLR_GRP_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },
                { field: "PCT_ADDL_PRICE", hidden: true },

                { field: "ITEM_NAME_EN", title: "Yarn Item", type: "string", width: "17%" },
                { field: "LK_YRN_COLR_GRP_NAME", title: "Color Group", type: "string", width: "10%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "10%" },
                { field: "YRN_LOT_NO", title: "Lot#", type: "string", width: "7%" },
                { field: "PACK_RCV_QTY", title: "Receive Qty.", type: "string", width: "5%" },
                { field: "PACK_MOU_CODE", title: "Pack Unit", type: "string", width: "10%" },
                { field: "QTY_PER_PACK", title: "Qty/PACK", type: "string", width: "5%" },
                { field: "LOOSE_QTY", title: "Loose Qty", type: "string", width: "5%" },

                { field: "RCV_QTY", title: "Total Rcv Qty.", type: "string", width: "5%" },
                { field: "MOU_CODE", title: "UoM", type: "string", width: "5%" },
                { field: "CONE_QTY", title: "Cone Qty", width: "5%" },
                { field: "UNIT_PRICE", title: "Rate", type: "decimal", width: "5%" },
                { field: "TOTAL_AMT", title: "TTL Value", type: "decimal", width: "5%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeRcvItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editRcvItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };


        vm.getOtherData = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            vm.form.SUP_TRD_NAME_EN = obj.SUP_TRD_NAME_EN;
            vm.form.YRN_LOT_NO = '';
            if (vm.form.KNT_YRN_STR_REQ_H_ID > 0) {
                KnittingDataService.getDataByFullUrl('/api/Knit/YarnReturnToSupplier/GetStockForSupplierReturn?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || null) + '&pYARN_ITEM_ID=' + (vm.form.YARN_ITEM_ID || null) + '&pRF_BRAND_ID=' + (vm.form.RF_BRAND_ID || null) + '&pIMP_LC_NO=' + (vm.form.IMP_LC_NO || null) + '&pYRN_LOT_NO=' + (vm.form.YRN_LOT_NO || null)).then(function (res) {

                    vm.IssueItemList = res;

                    KnittingDataService.getDataByFullUrl('/api/Knit/YarnReturnToSupplier/GetStockForSupplierReturn?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || null) + '&pYARN_ITEM_ID=' + (vm.form.YARN_ITEM_ID || null) + '&pRF_BRAND_ID=' + (vm.form.RF_BRAND_ID || null) + '&pIMP_LC_NO=' + (vm.form.IMP_LC_NO || null) + '&pYRN_LOT_NO=' + (vm.form.YRN_LOT_NO || null)).then(function (res) {

                        vm.DyItemList = new kendo.data.DataSource({
                            data: resData.iList
                        });

                        vm.yarnLotList = new kendo.data.DataSource({
                            data: resData.lList
                        });

                        vm.categoryBrandList = new kendo.data.DataSource({
                            data: resData.bList
                        });

                    });
                });
            }
            else {
                vm.form.KNT_YRN_STR_REQ_H_ID = 0;
                vm.IssueItemList = [];
                ItemList();
                YarnLotList();
                vm.categoryBrandList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                                var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                                e.success(list);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                });
            }
        };

        vm.getRequisitionInfo = function () {

            KnittingDataService.getDataByFullUrl('/api/Knit/YarnReturnToSupplier/GetStockForSupplierReturn?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || null) + '&pYARN_ITEM_ID=' + (vm.form.YARN_ITEM_ID || null) + '&pRF_BRAND_ID=' + (vm.form.RF_BRAND_ID || null) + '&pIMP_LC_NO=' + (vm.form.IMP_LC_NO || "") + '&pYRN_LOT_NO=' + (vm.form.YRN_LOT_NO || "")).then(function (res) {
                vm.IssueItemList = res;
            });
        };


        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=2').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function YarnLotList() {
            return vm.yarnLotList = new kendo.data.DataSource({
                data: []
            });
        };

        function GetItemCategoryList() {

            KnittingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var filter = _.filter(res, { 'ITEM_CAT_LEVEL': 1 });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: filter
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 2)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        vm.addToGrid = function (item) {
            console.log(item);
            if (item.ISSUE_QTY > 0) {

                if (vm.formItem.uid) {
                    // Update

                    var row = vm.ReturnItemList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.ReturnItemList.data(), function (o) {
                        return (o.YARN_ITEM_ID === item.YARN_ITEM_ID && o.uid != item.uid)
                    }).length;

                    if (count <= 1) {
                        row["YARN_ITEM_ID"] = vm.formItem.YARN_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                        row["RF_BRAND_ID"] = vm.formItem.RF_BRAND_ID;
                        row["BRAND_NAME_EN"] = vm.formItem.BRAND_NAME_EN;
                        row["KNT_YRN_STR_REQ_D_ID"] = vm.formItem.KNT_YRN_STR_REQ_D_ID;
                        row["KNT_YRN_LOT_ID"] = vm.formItem.KNT_YRN_LOT_ID;
                        row["PACK_MOU_ID"] = vm.formItem.PACK_MOU_ID;
                        row["MOU_CODE"] = vm.formItem.MOU_CODE;
                        row["LK_YRN_COLR_GRP_ID"] = vm.formItem.LK_YRN_COLR_GRP_ID;
                        row["YRN_COLR_GRP"] = vm.formItem.YRN_COLR_GRP;
                        row["YRN_LOT_NO"] = vm.formItem.YRN_LOT_NO;
                        
                        row["REQ_RTN_QTY"] = vm.formItem.REQ_RTN_QTY;
                        row["PACK_RQD_QTY"] = vm.formItem.PACK_RQD_QTY;
                        row["QTY_PER_PACK"] = vm.formItem.QTY_PER_PACK;
                        row["LOOSE_QTY"] = vm.formItem.LOOSE_QTY;
                        row["RQD_YRN_QTY"] = vm.formItem.RQD_YRN_QTY;

                        row["SP_NOTE"] = vm.formItem.SP_NOTE;

                        //config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.ReturnItemList.data(), function (o) {
                        return (o.YARN_ITEM_ID === item.YARN_ITEM_ID && o.RF_BRAND_ID === item.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === item.LK_YRN_COLR_GRP_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.ReturnItemList.data().length + 1;
                        vm.ReturnItemList.insert(idx, item);
                    }

                }
                if (vm.ReturnItemList.data().length > 0)
                    vm.form.SUP_CTRL_DISABLE = 1;
            }
            else
                config.appToastMsg("MULTI-005 Please input return quantity.");
        };

        vm.editItemData = function (data) {
            var edata = angular.copy(data);

            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/ItemLotStockByID/' + (edata.YARN_ITEM_ID || 0)).then(function (res) {

                vm.yarnLotList = new kendo.data.DataSource({
                    data: res
                });

                vm.categoryBrandList = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnColorGroupList = new kendo.data.DataSource({
                    data: res
                });

                vm.formItem = edata;
            });

        }

        vm.removeItemData = function (data) {

            if (!data.KNT_YRN_STR_REQ_D_ID || data.KNT_YRN_STR_REQ_D_ID <= 0) {
                vm.ReturnItemList.remove(data);
                if (vm.ReturnItemList.data().length == 0)
                    vm.form.SUP_CTRL_DISABLE = 0;
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.ReturnItemList.remove(data);

                     if (vm.ReturnItemList.data().length == 0)
                         vm.form.SUP_CTRL_DISABLE = 0;
                 });

        }

        vm.resetItemData = function () {
            vm.formItem = {};
        };

        vm.reset = function () {

            $state.go('YarnIssueReq', { pKNT_YRN_RPL_ISS_H_ID: 0 });

        };


        function GetStatusList() {

            var RF_ACTN_TYPE_ID = 3;
            var PARENT_ID = 0;
            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
            var sList = null;
            KnittingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                sList = res;
                console.log(sList);
                if (res.length == 1) {
                    vm.form.RF_ACTN_VIEW = 0;
                    vm.form.RF_ACTN_STATUS_ID = res[0].RF_ACTN_STATUS_ID;
                    vm.form.ACTN_STATUS_NAME = res[0].ACTN_STATUS_NAME;
                    //alert(vm.form.ACTN_STATUS_NAME);
                }
                else {
                    vm.form.RF_ACTN_VIEW = 1;
                }
            });

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                if (res.length == 1) {
                                    vm.form.RF_ACTN_VIEW = 0;
                                    vm.form.RF_ACTN_STATUS_ID = res[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = res[0].ACTN_STATUS_NAME;
                                    //alert(vm.form.ACTN_STATUS_NAME);
                                }
                                else {
                                    vm.form.RF_ACTN_VIEW = 1;
                                }
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };
        };

        function GetReqTypeList() {

            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                var list = _.filter(res, function (o) { return o.RF_REQ_TYPE_ID != 4 && o.IS_R_I == 'I' })
                                var nlist = _.filter(list, function (o) { return o.RF_REQ_TYPE_ID == 3 || o.RF_REQ_TYPE_ID == 6 || o.RF_REQ_TYPE_ID == 5 || o.RF_REQ_TYPE_ID == 15 || o.RF_REQ_TYPE_ID == 14 })
                                //KnittingDataService.LookupListData(88).then(function (res) {
                                e.success(nlist);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetReqStoreList() {

            return vm.reqStoreList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                //KnittingDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
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
                            KnittingDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        function GetYarnColorGroupList() {
            return vm.yarnColorGroupList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.LookupListData(93).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.gridOptionsRtnItem = {
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
            height: '300px',
            scrollable: true,
            columns: [
                { field: "YARN_ITEM_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "KNT_YRN_RPL_ISS_D_ID", hidden: true },
                { field: "KNT_YRN_STR_RPL_REQ_D_ID", hidden: true },
                { field: "CM_IMP_LC_H_ID", hidden: true },
                { field: "KNT_YRN_LOT_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "PACK_ISS_QTY", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },
                { field: "UNIT_PRICE", hidden: true },
                { field: "COST_PRICE", hidden: true },
                { field: "QTY_PER_PACK", hidden: true },

                { field: "ITEM_NAME_EN", title: "Yarn Item", type: "string", width: "7%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "7%" },
                { field: "IMP_LC_NO", title: "L/C #", type: "string", width: "7%" },
                { field: "YRN_LOT_NO", title: "Lot #", type: "string", width: "7%" },
                { field: "STOCK_QTY", title: "Stock Qty", type: "string", width: "7%" },
                { field: "REQ_RTN_QTY", title: "Required Qty", type: "string", width: "7%" },
                { field: "ISSUE_QTY", title: "Return Qty.", type: "string", width: "7%" },
                { field: "SP_NOTE", title: "Note", type: "string", width: "7%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" ng-if="!dataItem.KNT_YRN_ISS_RET_D_ID>0" class="btn btn-xs red"><i class="fa fa-remove"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };


        vm.submitConfirm = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["XML_RPL_D"] = KnittingDataService.xmlStringShort(vm.ReturnItemList.data().map(function (o) {
                    return {
                        KNT_YRN_RPL_ISS_D_ID: o.KNT_YRN_RPL_ISS_D_ID == null ? 0 : o.KNT_YRN_RPL_ISS_D_ID,
                        KNT_YRN_STR_RPL_REQ_D_ID: o.KNT_YRN_STR_RPL_REQ_D_ID == 0 ? null : o.KNT_YRN_STR_RPL_REQ_D_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        PACK_ISS_QTY: o.PACK_ISS_QTY,
                        QTY_MOU_ID: 3,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                        QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                        LOOSE_QTY: 0,
                        ISSUE_QTY: o.ISSUE_QTY == null ? 0 : o.ISSUE_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        COST_PRICE: o.COST_PRICE == null ? 0 : o.COST_PRICE,
                        SP_NOTE: o.SP_NOTE == null ? 0 : o.SP_NOTE,
                    }
                }));

                var _clndate = $filter('date')(data.ISS_CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["ISS_CHALAN_DT"] = _clndate;

                var id = vm.form.KNT_YRN_RPL_ISS_H_ID

                return KnittingDataService.saveDataByUrl(data, '/YarnReturnToSupplier/SaveIssue').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (parseInt(res.data.OPKNT_YRN_RPL_ISS_H_ID) > 0) {
                            vm.form.KNT_YRN_RPL_ISS_H_ID = res.data.OPKNT_YRN_RPL_ISS_H_ID;
                            $state.go($state.current, { pKNT_YRN_RPL_ISS_H_ID: res.data.OPKNT_YRN_RPL_ISS_H_ID }, { reload: true });
                        }

                    }
                });
            }
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["XML_RPL_D"] = KnittingDataService.xmlStringShort(vm.ReturnItemList.data().map(function (o) {
                    return {
                        KNT_YRN_RPL_ISS_D_ID: o.KNT_YRN_RPL_ISS_D_ID == null ? 0 : o.KNT_YRN_RPL_ISS_D_ID,
                        KNT_YRN_STR_RPL_REQ_D_ID: o.KNT_YRN_STR_RPL_REQ_D_ID == 0 ? null : o.KNT_YRN_STR_RPL_REQ_D_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        PACK_ISS_QTY: o.PACK_ISS_QTY,
                        QTY_MOU_ID: 3,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                        QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                        LOOSE_QTY: 0,
                        ISSUE_QTY: o.ISSUE_QTY == null ? 0 : o.ISSUE_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        COST_PRICE: o.COST_PRICE == null ? 0 : o.COST_PRICE,
                        SP_NOTE: o.SP_NOTE == null ? 0 : o.SP_NOTE,
                    }
                }));

                var _clndate = $filter('date')(data.ISS_CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["ISS_CHALAN_DT"] = _clndate;

                return KnittingDataService.saveDataByUrl(data, '/YarnReturnToSupplier/SaveIssue').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        config.appToastMsg(res.data.PMSG);
                        if (parseInt(res.data.OPKNT_YRN_RPL_ISS_H_ID) > 0) {
                            vm.form.KNT_YRN_RPL_ISS_H_ID = res.data.OPKNT_YRN_RPL_ISS_H_ID;
                            $state.go($state.current, { pKNT_YRN_RPL_ISS_H_ID: res.data.OPKNT_YRN_RPL_ISS_H_ID }, { reload: true });
                        }
                    }
                });
            }
        };



        vm.receiveConfirm = function (dataOri) {

            //if (fnValidateSub2() == true) {

            var data = angular.copy(dataOri);

            //if (!data.IS_FINALIZED)
            data["IS_FINALIZED"] = "Y";

            data["XML_RPL_D"] = KnittingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                return {
                    KNT_YRN_RPL_ISS_D_ID: o.KNT_YRN_RPL_ISS_D_ID == null ? 0 : o.KNT_YRN_RPL_ISS_D_ID,
                    KNT_YRN_STR_RPL_REQ_D_ID: o.KNT_YRN_STR_RPL_REQ_D_ID == 0 ? null : o.KNT_YRN_STR_RPL_REQ_D_ID,
                    YARN_ITEM_ID: o.YARN_ITEM_ID,
                    KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                    PACK_ISS_QTY: o.PACK_ISS_QTY,
                    QTY_MOU_ID: 3,
                    PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                    QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                    LOOSE_QTY: 0,
                    ISSUE_QTY: o.ISSUE_QTY == null ? 0 : o.ISSUE_QTY,
                    UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                    COST_PRICE: o.COST_PRICE == null ? 0 : o.COST_PRICE,
                    SP_NOTE: o.SP_NOTE == null ? 0 : o.SP_NOTE,

                    LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID,
                    RF_BRAND_ID: o.RF_BRAND_ID,
                    LOC_UNIT_PRICE: (o.UNIT_PRICE + (o.PCT_ADDL_PRICE || 0)) * (vm.form.LOC_CONV_RATE || 1),
                    YRN_LOT_NO: o.YRN_LOT_NO == null ? 0 : o.YRN_LOT_NO,
                    PACK_RCV_QTY: o.PACK_RCV_QTY == null ? 0 : o.PACK_RCV_QTY,
                    RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                    CONE_QTY: o.CONE_QTY,
                    CONE_WT: (o.QTY_PER_PACK || 0) / (o.CONE_QTY || 1)
                }
            }));

            var _clndate = $filter('date')(data.RCV_CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
            data["ISS_CHALAN_DT"] = _clndate;

            var id = vm.form.KNT_YRN_RPL_ISS_H_ID

            return KnittingDataService.saveDataByUrl(data, '/YarnReturnToSupplier/ReceiveConfirm').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);
                    if (parseInt(res.data.OPKNT_YRN_RPL_ISS_H_ID) > 0) {
                        vm.form.KNT_YRN_RPL_ISS_H_ID = res.data.OPKNT_YRN_RPL_ISS_H_ID;
                        $state.go($state.current, { pKNT_YRN_RPL_ISS_H_ID: res.data.OPKNT_YRN_RPL_ISS_H_ID }, { reload: true });
                    }

                }
            });
            //}
        };

        vm.receiveAll = function (dataOri) {

            if (fnValidateSub2() == true) {

                var data = angular.copy(dataOri);

                data["PARENT_ID"] = data.KNT_YRN_RPL_ISS_H_ID;

                data["XML_RPL_D"] = KnittingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        KNT_YRN_RPL_ISS_D_ID: o.KNT_YRN_RPL_ISS_D_ID == null ? 0 : o.KNT_YRN_RPL_ISS_D_ID,
                        KNT_YRN_STR_RPL_REQ_D_ID: o.KNT_YRN_STR_RPL_REQ_D_ID == 0 ? null : o.KNT_YRN_STR_RPL_REQ_D_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        PACK_ISS_QTY: o.PACK_ISS_QTY,
                        QTY_MOU_ID: 3,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                        QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                        LOOSE_QTY: 0,
                        ISSUE_QTY: o.ISSUE_QTY == null ? 0 : o.ISSUE_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        COST_PRICE: o.COST_PRICE == null ? 0 : o.COST_PRICE,
                        SP_NOTE: o.SP_NOTE == null ? 0 : o.SP_NOTE,

                        LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID,
                        LOC_UNIT_PRICE: (o.UNIT_PRICE + (o.PCT_ADDL_PRICE || 0)) * (vm.form.LOC_CONV_RATE || 1),
                        YRN_LOT_NO: o.YRN_LOT_NO == null ? 0 : o.YRN_LOT_NO,
                        PACK_RCV_QTY: o.PACK_RCV_QTY == null ? 0 : o.PACK_RCV_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                        CONE_QTY: o.CONE_QTY,
                        CONE_WT: (o.QTY_PER_PACK || 0) / (o.CONE_QTY || 1)
                    }
                }));

                data["KNT_YRN_RPL_ISS_H_ID"] = 0;

                var _clndate = $filter('date')(data.RCV_CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["ISS_CHALAN_DT"] = _clndate;

                return KnittingDataService.saveDataByUrl(data, '/YarnReturnToSupplier/ReceiveSave').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        config.appToastMsg(res.data.PMSG);
                        if (parseInt(res.data.OPKNT_YRN_RPL_ISS_H_ID) > 0) {
                            vm.form.KNT_YRN_RPL_ISS_H_ID = res.data.OPKNT_YRN_RPL_ISS_H_ID;
                            $state.go($state.current, { pKNT_YRN_RPL_ISS_H_ID: res.data.OPKNT_YRN_RPL_ISS_H_ID }, { reload: true });
                        }
                    }
                });
            }
        };




        vm.printChallan = function () {

            vm.form.REPORT_CODE = 'RPT-3510';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
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
        }



    }


})();

