(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnTestIssueReturnController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', YarnTestIssueReturnController]);
    function YarnTestIssueReturnController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.KNT_YRN_ISS_RET_H_ID ? formData : { INV_ITEM_CAT_ID: '2', ITEM_RET_BY: cur_user_id };
        vm.formItem = {};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [RequisitionList(), getUserData(), GetItemCategoryList(), ItemList(), YarnLotList(), GetReqTypeList(),
                GetReqStoreList(), GetCompanyList(), GetStatusList(), GetYarnColorGroupList(), GetMOUList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function RequisitionList() {

            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReturn/GetYarnRtnDtlByID?pKNT_YRN_ISS_RET_H_ID=' + ($stateParams.pKNT_YRN_ISS_RET_H_ID || 0)).then(function (res) {
                vm.ReturnItemList = res;
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

        vm.checkReturnQty = function (item) {
            if (item.RCV_QTY > item.RET_QTY)
                item.RCV_QTY = '';
        };


        vm.form.INV_ITEM_CAT_ID = 2;

        $scope.RET_CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RET_CHALAN_DT_LNopened = true;
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

        vm.getRequisitionByType = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            return vm.requisitionList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReturn/GetYarnStoreReq?pREQ_TYPE_NAME=' + (obj.REQ_TYPE_NAME || null)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        vm.getOtherData = function () {
            vm.form.YRN_LOT_NO = '';
            if (vm.form.KNT_YRN_STR_REQ_H_ID > 0) {
                KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReturn/GetIssueInfoForReturn/' + (vm.form.KNT_YRN_STR_REQ_H_ID || null) + '/' + (vm.form.YARN_ITEM_ID || null) + '/' + (vm.form.RF_BRAND_ID || null) + '/' + (vm.form.KNT_YRN_LOT_ID || null) + '/' + (vm.form.YRN_LOT_NO || null)).then(function (res) {

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

            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReturn/GetIssueInfoForReturn/' + (vm.form.KNT_YRN_STR_REQ_H_ID || null) + '/' + (vm.form.YARN_ITEM_ID || null) + '/' + (vm.form.RF_BRAND_ID || null) + '/' + (vm.form.KNT_YRN_LOT_ID || null) + '/' + (vm.form.YRN_LOT_NO || null)).then(function (res) {

                vm.IssueItemList = res;

                //vm.DyItemList = new kendo.data.DataSource({
                //    data: res
                //});

                //vm.yarnLotList = new kendo.data.DataSource({
                //    data: res
                //});

                //vm.categoryBrandList = new kendo.data.DataSource({
                //    data: res
                //});

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


        vm.getItemLotStock = function (e) {

            var obj = e.sender.dataItem(e.item);
            var data = _.filter(vm.yarnLotList.data(), function (o) { return o.KNT_YRN_LOT_ID === obj.KNT_YRN_LOT_ID })
            console.log(data);
            vm.formItem.STOCK_QTY = data[0].STOCK_QTY;
            vm.formItem.PACK_MOU_ID = data[0].PACK_MOU_ID;
            vm.formItem.QTY_PER_PACK = data[0].QTY_PER_PACK;

        };

        vm.getItemStock = function (e) {

            var obj = e.sender.dataItem(e.item);
            KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/ItemLotStockByID/' + (obj.INV_ITEM_ID || 0)).then(function (res) {

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
            if (fnValidateSub() == true) {

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
                        row["KNT_YRN_ISS_RET_H_ID"] = vm.formItem.KNT_YRN_ISS_RET_H_ID;
                        row["KNT_YRN_STR_REQ_D_ID"] = vm.formItem.KNT_YRN_STR_REQ_D_ID;
                        row["KNT_YRN_LOT_ID"] = vm.formItem.KNT_YRN_LOT_ID;
                        row["PACK_MOU_ID"] = vm.formItem.PACK_MOU_ID;
                        row["MOU_CODE"] = vm.formItem.MOU_CODE;
                        row["LK_YRN_COLR_GRP_ID"] = vm.formItem.LK_YRN_COLR_GRP_ID;
                        row["YRN_COLR_GRP"] = vm.formItem.YRN_COLR_GRP;
                        row["YRN_LOT_NO"] = vm.formItem.YRN_LOT_NO;

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
            }
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
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.ReturnItemList.remove(data);
                 });

        }

        vm.resetItemData = function () {
            vm.formItem = {};
        };

        vm.reset = function () {

            $state.go('YarnTestIssueReq', { pKNT_YRN_ISS_RET_H_ID: 0 });

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
                                    vm.form.RF_ACTN_STATUS_ID = res[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = res[0].ACTN_STATUS_NAME;
                                    //alert(vm.form.RF_ACTN_STATUS_ID);
                                }
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };

            //return vm.statusList = {
            //    optionLabel: "-- Select Status --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                KnittingDataService.LookupListData(46).then(function (res) {
            //                    e.success(res);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "LK_DATA_NAME_EN",
            //    dataValueField: "LOOKUP_DATA_ID"
            //};
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
                                //KnittingDataService.LookupListData(88).then(function (res) {
                                e.success(res);
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
                { field: "KNT_YRN_ISS_RET_D_ID", hidden: true },
                { field: "KNT_YRN_ISS_RET_H_ID", hidden: true },
                { field: "KNT_YRN_LOT_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "KNT_JOB_CRD_H_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },

                { field: "ORDER_INFO", title: "Order Info", type: "string", width: "7%" },
                { field: "ITEM_NAME_EN", title: "Yarn Item", type: "string", width: "7%" },
                //{ field: "YRN_COLR_GRP", title: "Color Group", type: "string", width: "7%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "7%" },
                { field: "YRN_LOT_NO", title: "Lot#", type: "string", width: "7%" },
                { field: "ISSUE_QTY", title: "Issued Qty", type: "string", width: "7%" },
                { field: "RET_QTY", title: "Return Qty.", type: "string", width: "7%" },
                //{ field: "MOU_CODE", title: "Pack Unit", type: "string", width: "7%" },
                //{ field: "QTY_PER_PACK", title: "Qty/Pack", type: "string", width: "7%" },
                //{ field: "LOOSE_QTY", title: "Loose Qty", type: "string", width: "7%" },
                //{ field: "RQD_YRN_QTY", title: "Total Reqd Qty.", type: "string", width: "7%" },
                //{ field: "SP_NOTE", title: "Note", width: "7%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" ng-if="!dataItem.KNT_YRN_ISS_RET_D_ID>0" class="btn btn-xs red"><i class="fa fa-remove"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };

        vm.confirmAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["XML_RTN_D"] = KnittingDataService.xmlStringShort(vm.ReturnItemList.map(function (o) {
                    return {
                        KNT_YRN_ISS_RET_D_ID: o.KNT_YRN_ISS_RET_D_ID == null ? 0 : o.KNT_YRN_ISS_RET_D_ID,
                        KNT_JOB_CRD_H_ID: o.KNT_JOB_CRD_H_ID == 0 ? null : o.KNT_JOB_CRD_H_ID,
                        KNT_YRN_ISS_H_ID: o.KNT_YRN_ISS_H_ID == 0 ? null : o.KNT_YRN_ISS_H_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        QTY_MOU_ID: 3,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                        PACK_RET_QTY: 0,
                        QTY_PER_PACK: 0,
                        LOOSE_QTY: 0,
                        RET_QTY: o.RET_QTY == null ? 0 : o.RET_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY
                    }
                }));

                data["IS_FINALIZED"] = "Y";

                var id = vm.form.KNT_YRN_ISS_RET_H_ID

                return KnittingDataService.saveDataByUrl(data, '/YarnIssueReturn/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg("MULTI-001 Yarn receive has been updated successfully");
                            $state.go('YarnTestIssueRtn', { pKNT_YRN_ISS_RET_H_ID: 0 });
                        }
                        else {

                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPKNT_YRN_ISS_RET_H_ID) > 0) {
                                vm.form.KNT_YRN_ISS_RET_H_ID = res.data.OPKNT_YRN_ISS_RET_H_ID;
                                $state.go($state.current, { pKNT_YRN_ISS_RET_H_ID: 0 });
                            }
                        }

                    }
                });
            }
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["XML_RTN_D"] = KnittingDataService.xmlStringShort(vm.ReturnItemList.map(function (o) {
                    return {
                        KNT_YRN_ISS_RET_D_ID: o.KNT_YRN_ISS_RET_D_ID == null ? 0 : o.KNT_YRN_ISS_RET_D_ID,
                        KNT_JOB_CRD_H_ID: o.KNT_JOB_CRD_H_ID == 0 ? null : o.KNT_JOB_CRD_H_ID,
                        KNT_YRN_ISS_H_ID: o.KNT_YRN_ISS_H_ID == 0 ? null : o.KNT_YRN_ISS_H_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        QTY_MOU_ID: 3,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                        PACK_RET_QTY: 0,
                        QTY_PER_PACK: 0,
                        LOOSE_QTY: 0,
                        RET_QTY: o.RET_QTY == null ? 0 : o.RET_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY
                    }
                }));

                var id = vm.form.KNT_YRN_ISS_RET_H_ID

                return KnittingDataService.saveDataByUrl(data, '/YarnIssueReturn/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg("MULTI-001 Yarn receive has been updated successfully");
                            $state.go('YarnTestIssueRtn', { pKNT_YRN_ISS_RET_H_ID: 0 });
                        }
                        else {

                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPKNT_YRN_ISS_RET_H_ID) > 0) {
                                vm.form.KNT_YRN_ISS_RET_H_ID = res.data.OPKNT_YRN_ISS_RET_H_ID;
                                $state.go($state.current, { pKNT_YRN_ISS_RET_H_ID: 0 });
                            }
                        }

                    }
                });
            }
        };
    }


})();

