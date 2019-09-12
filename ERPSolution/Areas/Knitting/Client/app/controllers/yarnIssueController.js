(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnIssueController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', YarnIssueController]);
    function YarnIssueController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.KNT_YRN_ISS_H_ID ? formData : { KNT_YRN_STR_REQ_H_ID: '0', INV_ITEM_CAT_ID: '2', HR_COMPANY_ID: 1, SCM_STORE_ID: 1, ITEM_ISS_BY: cur_user_id, IS_FINALIZED: 'N', ISS_CHALAN_DT: vm.today };

        vm.form.RF_REQ_TYPE_ID = ($stateParams.pRF_REQ_TYPE_ID || formData.RF_REQ_TYPE_ID || 0);
        vm.form.KNT_YRN_STR_REQ_H_ID = ($stateParams.pKNT_YRN_STR_REQ_H_ID || formData.KNT_YRN_STR_REQ_H_ID || 0);

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [RequisitionList(), getUserData(), GetItemCategoryList(), ItemList(), YarnLotList(), IssueItemList(), GetReqSourceList(), GetReqTypeList(), GetStatusList(), GetCompanyList(), GetYarnColorGroupList(), GetMOUList(), GetSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAllByCat/0').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(46).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function RequisitionList() {
            return vm.requisitionList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnStoreReq?pRF_REQ_TYPE_ID=' + (vm.form.RF_REQ_TYPE_ID || null)).then(function (res) {
                            e.success(res);
                            var obj = _.filter(res, function (o) { return o.KNT_YRN_STR_REQ_H_ID == vm.form.KNT_YRN_STR_REQ_H_ID })[0];
                            console.log(obj);
                            if (obj) {
                                vm.form.STR_REQ_DT = obj.STR_REQ_DT;
                                //vm.form.RF_REQ_TYPE_ID = obj.RF_REQ_TYPE_ID;
                                vm.form.USER_NAME_EN = obj.USER_NAME_EN;
                                vm.form.HR_COMPANY_ID = obj.HR_COMPANY_ID;
                                vm.form.SCM_STORE_ID = obj.SCM_STORE_ID;
                                vm.form.SCM_SUPPLIER_ID = obj.SCM_SUPPLIER_ID;
                                vm.form.PRG_ISS_NO = obj.PRG_ISS_NO;
                            }
                        });
                    }
                }
            });
        };

        function GetMOUList() {
            return vm.mOUList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
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

            //$("#customers").kendoDropDownList(vm.userList);
        }

        vm.TotalIssueQtyMain = function (index) {
            if (parseFloat(vm.IssueItemList[index].ISSUE_QTY) > parseFloat(vm.IssueItemList[index].STOCK_QTY)) {
                vm.IssueItemList[index].ISSUE_QTY = 0;
            }
            if (parseFloat(vm.IssueItemList[index].ISSUE_QTY) > parseFloat(vm.IssueItemList[index].ADV_ISS_QTY)) {
                vm.IssueItemList[index].IS_FRM_ADV = 'N';
            }
        }

        vm.TotalIssueQty = function (index) {
            var pQty = parseInt(vm.IssueItemList[index].PACK_ISS_QTY || 0);
            var lQty = parseFloat(vm.IssueItemList[index].LOOSE_QTY || 0);
            var qtyPack = parseFloat(vm.IssueItemList[index].QTY_PER_PACK || 0);
            if (qtyPack > 0)
                if ((lQty + pQty) > 0 && qtyPack > 0) {
                    console.log(vm.IssueItemList[index].QTY_PER_PACK);
                    var IssQty = parseFloat((vm.IssueItemList[index].PACK_ISS_QTY || 0) * (vm.IssueItemList[index].QTY_PER_PACK || 0)) + parseFloat(vm.IssueItemList[index].LOOSE_QTY || 0);
                    if (qtyPack <= lQty) {

                        vm.IssueItemList[index].ISSUE_QTY = parseFloat((vm.IssueItemList[index].PACK_ISS_QTY || 0) * (vm.IssueItemList[index].QTY_PER_PACK || 0));
                        vm.IssueItemList[index].LOOSE_QTY = 0;
                    }
                    else if (parseFloat(IssQty) > parseFloat(vm.IssueItemList[index].STOCK_QTY)) {
                        vm.IssueItemList[index].ISSUE_QTY = 0;
                        vm.IssueItemList[index].PACK_ISS_QTY = 0;
                        vm.IssueItemList[index].LOOSE_QTY = 0;
                    }
                    else {
                        vm.IssueItemList[index].ISSUE_QTY = IssQty;
                    }
                }
                else {
                    vm.IssueItemList[index].ISSUE_QTY = 0;
                    //vm.IssueItemList[index].PACK_ISS_QTY = 0;
                    vm.IssueItemList[index].LOOSE_QTY = 0;
                }


            if (parseFloat(vm.IssueItemList[index].ISSUE_QTY) > parseFloat(vm.IssueItemList[index].ADV_ISS_QTY)) {
                vm.IssueItemList[index].IS_FRM_ADV = 'N';
            }
        };


        vm.form.INV_ITEM_CAT_ID = 2;

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
        }

        $scope.ISS_CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ISS_CHALAN_DT_LNopened = true;
        }

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


        function IssueItemList() {

            if (vm.form.KNT_YRN_ISS_H_ID > 0) {
                KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnIssueInfoDtlByID?pKNT_YRN_ISS_H_ID=' + (vm.form.KNT_YRN_ISS_H_ID || 0)).then(function (res) {
                    vm.IssueItemList = res;
                });

                KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnJCDtl/' + (vm.form.KNT_YRN_STR_REQ_H_ID || 0) + '?pOption=3002').then(function (res) {

                    vm.DyItemList = new kendo.data.DataSource({
                        data: res
                    });

                    vm.yarnLotList = new kendo.data.DataSource({
                        data: res
                    });


                    vm.categoryBrandList = new kendo.data.DataSource({
                        data: res
                    });

                    vm.yarnColorGroupList = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
            else {

                KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnJCDtl/' + (vm.form.KNT_YRN_STR_REQ_H_ID || 0) + '?pOption=3002').then(function (res) {
                    vm.IssueItemList = res;
                });
            }

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

        vm.getItemStock = function (e) {

            var obj = e.sender.dataItem(e.item);
            //alert(obj.YARN_ITEM_ID);
            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnJCDtl/' + (vm.form.KNT_YRN_STR_REQ_H_ID || 0) + '?pOption=3002').then(function (res) {

                var data = _.filter(res, function (o) { return o.YARN_ITEM_ID === obj.YARN_ITEM_ID })
                console.log(data);
                vm.formItem.STOCK_QTY = data[0].STOCK_QTY;
                vm.formItem.REQ_QTY = data[0].RQD_YRN_QTY;
            });

        };

        vm.getRequisitionByType = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            return vm.requisitionList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnStoreReq?pRF_REQ_TYPE_ID=' + (obj.RF_REQ_TYPE_ID || null)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        vm.getRequisitionInfo = function (e) {

            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            vm.form.STR_REQ_DT = obj.STR_REQ_DT;
            //vm.form.RF_REQ_TYPE_ID = obj.RF_REQ_TYPE_ID;
            vm.form.USER_NAME_EN = obj.USER_NAME_EN;
            vm.form.HR_COMPANY_ID = obj.HR_COMPANY_ID;
            vm.form.SCM_STORE_ID = obj.SCM_STORE_ID;
            vm.form.SCM_SUPPLIER_ID = obj.SCM_SUPPLIER_ID;

            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnJCDtl/' + (vm.form.KNT_YRN_STR_REQ_H_ID || 0) + '?pOption=3002').then(function (res) {

                vm.IssueItemList = res;

                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnLotList = new kendo.data.DataSource({
                    data: res
                });


                vm.categoryBrandList = new kendo.data.DataSource({
                    data: res
                });

                vm.yarnColorGroupList = new kendo.data.DataSource({
                    data: res
                });
            });

            //return vm.DyItemList = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnJCDtl/' + (vm.form.KNT_YRN_STR_REQ_H_ID || 0)).then(function (res) {
            //                e.success(res);
            //            });
            //        }
            //    }
            //});
        }

        if (vm.form.KNT_YRN_STR_REQ_H_ID > 0) {

            //KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnStoreReq?pRF_REQ_TYPE_ID=' + (vm.form.RF_REQ_TYPE_ID || null)).then(function (res) {
            //    var obj = _.filter(res, function (o) { return o.KNT_YRN_STR_REQ_H_ID == vm.form.KNT_YRN_STR_REQ_H_ID })[0];
            //    console.log(obj);
            //    if (obj) {
            //        vm.form.STR_REQ_DT = obj.STR_REQ_DT;
            //        //vm.form.RF_REQ_TYPE_ID = obj.RF_REQ_TYPE_ID;
            //        vm.form.USER_NAME_EN = obj.USER_NAME_EN;
            //        vm.form.HR_COMPANY_ID = obj.HR_COMPANY_ID;
            //        vm.form.SCM_STORE_ID = obj.SCM_STORE_ID;
            //        vm.form.SCM_SUPPLIER_ID = obj.SCM_SUPPLIER_ID;
            //        vm.form.PRG_ISS_NO = obj.PRG_ISS_NO;
            //    }
            //});
        }

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

        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {
                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.YARN_ITEM_ID === parseInt(vm.formItem.YARN_ITEM_ID)
                })[0];

                vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var itemB = _.filter(vm.categoryBrandList.data(), function (o) {
                    return o.RF_BRAND_ID === parseInt(vm.formItem.RF_BRAND_ID)
                })[0];

                vm.formItem.BRAND_NAME_EN = itemB.BRAND_NAME_EN;

                var mouUnit = _.filter(vm.mOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.PACK_MOU_ID)
                })[0];

                vm.formItem.MOU_CODE = mouUnit.MOU_CODE;

                var yColorG = _.filter(vm.yarnColorGroupList.data(), function (o) {
                    return o.LK_YRN_COLR_GRP_ID === parseInt(vm.formItem.LK_YRN_COLR_GRP_ID)
                })[0];
                console.log(yColorG);

                vm.formItem.YRN_COLR_GRP = yColorG.YRN_COLR_GRP;

                var yarnlot = _.filter(vm.yarnLotList.data(), function (o) {
                    return o.KNT_YRN_LOT_ID === parseInt(vm.formItem.KNT_YRN_LOT_ID)
                })[0];

                vm.formItem.YRN_LOT_NO = yarnlot.YRN_LOT_NO;

                vm.formItem.INV_ITEM_CAT_ID = vm.form.INV_ITEM_CAT_ID;
                if (vm.formItem.uid) {
                    // Update

                    var row = vm.IssueItemList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.IssueItemList.data(), function (o) {
                        return (o.YARN_ITEM_ID === vm.formItem.YARN_ITEM_ID && o.uid != vm.formItem.uid)
                    }).length;

                    if (count <= 1) {
                        row["YARN_ITEM_ID"] = vm.formItem.YARN_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                        row["RF_BRAND_ID"] = vm.formItem.RF_BRAND_ID;
                        row["BRAND_NAME_EN"] = vm.formItem.BRAND_NAME_EN;
                        row["KNT_JOB_CRD_H_ID"] = vm.formItem.KNT_JOB_CRD_H_ID;
                        row["KNT_YRN_ISS_H_ID"] = vm.formItem.KNT_YRN_ISS_H_ID;
                        row["SCM_PURC_REQ_D_ID"] = vm.formItem.SCM_PURC_REQ_D_ID;
                        row["PACK_MOU_ID"] = vm.formItem.PACK_MOU_ID;
                        row["MOU_CODE"] = vm.formItem.MOU_CODE;
                        row["LK_YRN_COLR_GRP_ID"] = vm.formItem.LK_YRN_COLR_GRP_ID;
                        row["YRN_COLR_GRP"] = vm.formItem.YRN_COLR_GRP;
                        row["YRN_LOT_NO"] = vm.formItem.YRN_LOT_NO;

                        row["CTN_RCV_QTY"] = vm.formItem.CTN_RCV_QTY;
                        row["QTY_PER_CTN"] = vm.formItem.QTY_PER_CTN;
                        row["RCV_QTY"] = vm.formItem.RCV_QTY;
                        row["UNIT_PRICE"] = vm.formItem.UNIT_PRICE;
                        row["TOTAL_AMT"] = vm.formItem.TOTAL_AMT;

                        row["SP_NOTE"] = vm.formItem.SP_NOTE;

                        //config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.IssueItemList.data(), function (o) {
                        return (o.YARN_ITEM_ID === vm.formItem.YARN_ITEM_ID && o.RF_BRAND_ID === vm.formItem.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === vm.formItem.LK_YRN_COLR_GRP_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.IssueItemList.data().length + 1;
                        vm.IssueItemList.insert(idx, vm.formItem);

                        var gview = vm.IssueItemList.data();
                        //console.log(gview);
                        //config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
        };

        vm.editItemData = function (data) {
            vm.formItem = angular.copy(data);
        }

        vm.removeItemData = function (data) {

            if (!data.KNT_YRN_ISS_D_ID || data.KNT_YRN_ISS_D_ID <= 0) {
                vm.IssueItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.IssueItemList.remove(data);
                 });

        }

        vm.resetItemData = function () {
            vm.formItem = {};
        };

        vm.reset = function () {

            $state.go('YarnIssue', { pKNT_YRN_ISS_H_ID: 0 });

        };


        function GetStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(46).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
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
                                //KnittingDataService.LookupListData(88).then(function (res) {
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetReqSourceList() {
            //alert("");
            return vm.reqSourceList = {
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

        vm.gridOptionsItem = {
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
                { field: "KNT_JOB_CRD_H_ID", hidden: true },
                { field: "KNT_YRN_ISS_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "LK_YRN_COLR_GRP_ID", hidden: true },
                { field: "MC_FAB_PROD_ORD_H_ID", hidden: true },
                { field: "KNT_YDP_D_COL_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },
                { field: "RQD_CONE_QTY", hidden: true },

                { field: "ITEM_NAME_EN", title: "Yarn Item", type: "string", width: "7%" },
                { field: "YRN_COLR_GRP", title: "Color Group", type: "string", width: "7%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "7%" },
                { field: "YRN_LOT_NO", title: "Lot#", type: "string", width: "7%" },
                { field: "STOCK_QTY", title: "Stock Qty", type: "string", width: "7%" },
                { field: "REQ_QTY", title: "Reqd Qty.", type: "string", width: "7%" },
                { field: "PACK_ISS_QTY", title: "Issued Qty", type: "string", width: "7%" },
                { field: "MOU_CODE", title: "Pack Unit", type: "string", width: "7%" },
                { field: "QTY_PER_PACK", title: "Qty/Pack", type: "string", width: "7%" },
                { field: "LOOSE_QTY", title: "Loose Qty", type: "string", width: "7%" },
                { field: "ISSUE_QTY", title: "Total Issue Qty.", type: "string", width: "7%" },
                { field: "SP_NOTE", title: "Note", width: "7%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" ng-if="!dataItem.KNT_YRN_ISS_D_ID>0" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var ListOfItem = _.filter(vm.IssueItemList, function (o) { return o.SELECT_BIT == 'Y' && o.ISSUE_QTY > 0 })

                if (ListOfItem.length == 0) {

                    config.appToastMsg("MTEX-005 No data found for save. Please check item list");
                    return;
                }

                var _isudate = $filter('date')(data.ISS_CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data['ISS_CHALAN_DT'] = _isudate;

                data["XML_ISS_D"] = KnittingDataService.xmlStringShort(ListOfItem.map(function (o) {
                    return {
                        KNT_YRN_ISS_D_ID: o.KNT_YRN_ISS_D_ID == null ? 0 : o.KNT_YRN_ISS_D_ID,
                        KNT_YRN_STR_REQ_D_ID: o.KNT_YRN_STR_REQ_D_ID == null ? 0 : o.KNT_YRN_STR_REQ_D_ID,
                        MC_FAB_PROD_ORD_H_ID: o.MC_FAB_PROD_ORD_H_ID == 0 ? null : o.MC_FAB_PROD_ORD_H_ID,
                        KNT_JOB_CRD_H_ID: o.KNT_JOB_CRD_H_ID == 0 ? null : o.KNT_JOB_CRD_H_ID,
                        KNT_YDP_D_COL_ID: o.KNT_YDP_D_COL_ID == 0 ? null : o.KNT_YDP_D_COL_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID,
                        RQD_CONE_QTY: o.RQD_CONE_QTY,
                        QTY_MOU_ID: 3,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        REQ_QTY: o.REQ_QTY,
                        PACK_ISS_QTY: o.PACK_ISS_QTY == null ? 0 : o.PACK_ISS_QTY,
                        QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                        LOOSE_QTY: o.LOOSE_QTY == null ? 0 : o.LOOSE_QTY,
                        ISSUE_QTY: o.ISSUE_QTY == null ? 0 : o.ISSUE_QTY,
                        ADV_ISSUE_QTY: (parseFloat(o.ISSUE_QTY) - parseFloat(o.REQ_QTY)) > 0 ? (parseFloat(o.ISSUE_QTY) - parseFloat(o.REQ_QTY)) : 0,
                        COST_PRICE: o.COST_PRICE == null ? 0 : o.COST_PRICE,
                        IS_FRM_ADV: o.IS_FRM_ADV == null ? 'N' : o.IS_FRM_ADV == "Y" ? o.IS_FRM_ADV : "N",
                        SP_NOTE: o.SP_NOTE
                    }
                }));

                var id = vm.form.KNT_YRN_ISS_H_ID

                return KnittingDataService.saveDataByUrl(data, '/YarnIssue/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go("YarnIssue", { pKNT_YRN_ISS_H_ID: (id || 0) }, { reload: true });
                        }
                        else {

                            config.appToastMsg(res.data.PMSG);

                            if (parseInt(res.data.OPKNT_YRN_ISS_H_ID) > 0) {
                                vm.form.KNT_YRN_ISS_H_ID = res.data.OPKNT_YRN_ISS_H_ID;
                                $state.go("YarnIssue", { pKNT_YRN_ISS_H_ID: res.data.OPKNT_YRN_ISS_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };
    }


})();

