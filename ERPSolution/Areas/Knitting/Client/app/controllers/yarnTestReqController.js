(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnTestReqController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', YarnTestReqController]);
    function YarnTestReqController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};
        console.log(formData);
        vm.form = formData.KNT_YRN_STR_REQ_H_ID ? formData : { INV_ITEM_CAT_ID: '2', HR_COMPANY_ID: '1', SCM_STORE_ID: '1', RF_REQ_TYPE_ID: '4', STR_REQ_BY: cur_user_id, STR_REQ_DT: vm.today, RF_ACTN_VIEW: 0 };
        vm.formItem = {};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [RequisitionList(), getUserData(), GetItemCategoryList(), ItemList(), YarnLotList(), GetReqTypeList(), GetReqStoreList(), GetCompanyList(), GetStatusList(), GetYarnColorGroupList(), GetMOUList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function RequisitionList() {
            return vm.RequisitionItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/GetYarnRequisitionDtlByID?pKNT_YRN_STR_REQ_H_ID=' + ($stateParams.pKNT_YRN_STR_REQ_H_ID || 0)).then(function (res) {
                            e.success(res);
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

        vm.TotalRequiredQty = function () {
            if (vm.formItem.LOOSE_QTY < vm.formItem.QTY_PER_PACK)
                if ((vm.formItem.PACK_RQD_QTY > 0 || vm.formItem.LOOSE_QTY > 0) && vm.formItem.QTY_PER_PACK > 0) {

                    vm.formItem.RQD_YRN_QTY = parseFloat((vm.formItem.PACK_RQD_QTY || 0)) * parseFloat(vm.formItem.QTY_PER_PACK) + parseFloat((vm.formItem.LOOSE_QTY || 0));
                }
                else
                    vm.formItem.RQD_YRN_QTY = 0;
            else
                vm.formItem.LOOSE_QTY = 0;
        };


        vm.form.INV_ITEM_CAT_ID = 2;

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
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
        
        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        //KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=2').then(function (res) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetYarnForTest/null/null').then(function (res) {
                            console.log(res);
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
            vm.formItem.PACK_MOU_ID = data[0].PACK_MOU_ID;

            vm.formItem.LK_YRN_COLR_GRP_ID = data[0].LK_YRN_COLR_GRP_ID;



        };

        vm.getItemStock = function (e) {

            if (fnValidateSub2() == true) {
                var obj = e.sender.dataItem(e.item);
                KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/ItemLotStockByID/' + (obj.YARN_ITEM_ID || 0) + '/' + (vm.form.SCM_STORE_ID || 0) + '/' + (vm.form.RF_REQ_TYPE_ID || 0)).then(function (res) {

                    vm.yarnLotList = new kendo.data.DataSource({
                        data: res.data
                    });

                    vm.categoryBrandList = new kendo.data.DataSource({
                        data: res.bList
                    });
                    
                    vm.yarnColorGroupList = new kendo.data.DataSource({
                        data: res.cList
                    });

                });
            }
            else
                vm.formItem.YARN_ITEM_ID = '';
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

                    var row = vm.RequisitionItemList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.RequisitionItemList.data(), function (o) {
                        return (o.YARN_ITEM_ID === vm.formItem.YARN_ITEM_ID && o.uid != vm.formItem.uid)
                    }).length;

                    if (count <= 1) {
                        row["YARN_ITEM_ID"] = vm.formItem.YARN_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                        row["RF_BRAND_ID"] = vm.formItem.RF_BRAND_ID;
                        row["BRAND_NAME_EN"] = vm.formItem.BRAND_NAME_EN;
                        row["KNT_YRN_STR_REQ_H_ID"] = vm.formItem.KNT_YRN_STR_REQ_H_ID;
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

                        vm.formItem = { 'YARN_ITEM_ID': '', 'RF_BRAND_ID': '', 'KNT_YRN_LOT_ID': '', 'LK_YRN_COLR_GRP_ID': '', 'PACK_MOU_ID': '' };

                        //config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.RequisitionItemList.data(), function (o) {
                        return (o.YARN_ITEM_ID === vm.formItem.YARN_ITEM_ID && o.RF_BRAND_ID === vm.formItem.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === vm.formItem.LK_YRN_COLR_GRP_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.RequisitionItemList.data().length + 1;
                        vm.RequisitionItemList.insert(idx, vm.formItem);
                        vm.formItem = { 'YARN_ITEM_ID': '', 'RF_BRAND_ID': '', 'KNT_YRN_LOT_ID': '', 'LK_YRN_COLR_GRP_ID': '', 'PACK_MOU_ID': '' };

                        var gview = vm.RequisitionItemList.data();
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
        };

        vm.editItemData = function (data) {
            var edata = angular.copy(data);

            vm.categoryBrandList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/ItemLotStockByID/' + (edata.YARN_ITEM_ID || 0) + '/' + (vm.form.SCM_STORE_ID || 0) + '/' + (vm.form.RF_REQ_TYPE_ID || 0)).then(function (res) {

                            //vm.categoryBrandList = new kendo.data.DataSource({
                            //    data: res
                            //});

                            //vm.yarnLotList = new kendo.data.DataSource({
                            //    data: res
                            //});

                            e.success(res.bList);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

            vm.yarnLotList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/ItemLotStockByID/' + (edata.YARN_ITEM_ID || 0) + '/' + (vm.form.SCM_STORE_ID || 0) + '/' + (vm.form.RF_REQ_TYPE_ID || 0)).then(function (res) {

                            e.success(res.data);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

            vm.yarnColorGroupList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/ItemLotStockByID/' + (edata.YARN_ITEM_ID || 0) + '/' + (vm.form.SCM_STORE_ID || 0) + '/' + (vm.form.RF_REQ_TYPE_ID || 0)).then(function (res) {

                            e.success(res.cList);
                            vm.formItem = edata;

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });


        }

        vm.removeItemData = function (data) {

            if (!data.KNT_YRN_STR_REQ_D_ID || data.KNT_YRN_STR_REQ_D_ID <= 0) {
                vm.RequisitionItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.RequisitionItemList.remove(data);
                 });

        }

        vm.resetItemData = function () {
            vm.formItem = {};
        };

        vm.reset = function () {

            $state.go('YarnTestReq', { pKNT_YRN_STR_REQ_H_ID: 0 });

        };


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 4;
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
            //    filter: "startswith",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                KnittingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
            //                    console.log(res);
            //                    e.success(res);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "ACTN_STATUS_NAME",
            //    dataValueField: "RF_ACTN_STATUS_ID"
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
                                var list = _.filter(res, function (o) { return o.RF_REQ_TYPE_ID == 4 })
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
                { field: "KNT_YRN_STR_REQ_D_ID", hidden: true },
                { field: "KNT_YRN_STR_REQ_H_ID", hidden: true },
                { field: "KNT_YRN_LOT_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "LK_YRN_COLR_GRP_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Yarn Item", type: "string", width: "15%" },
                { field: "YRN_COLR_GRP", title: "Color Group", type: "string", width: "10%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "10%" },
                { field: "YRN_LOT_NO", title: "Lot#", type: "string", width: "10%" },
                { field: "STOCK_QTY", title: "Stock Qty", type: "string", width: "10%" },
                //{ field: "PACK_RQD_QTY", title: "Reqd Qty.", type: "string", width: "7%" },
                { field: "MOU_CODE", title: "Pack Unit", type: "string", width: "10%" },
                { field: "QTY_PER_PACK", title: "Qty/Pack", type: "string", width: "5%" },
                //{ field: "LOOSE_QTY", title: "Loose Qty", type: "string", width: "7%" },
                { field: "RQD_CONE_QTY", title: "Reqd Cone", type: "string", width: "5%" },
                { field: "RQD_YRN_QTY", title: "Reqd Qty.", type: "string", width: "10%" },

                { field: "SP_NOTE", title: "Note", width: "10%" },
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

                data["XML_REQ_D"] = KnittingDataService.xmlStringShort(vm.RequisitionItemList.data().map(function (o) {
                    return {
                        KNT_YRN_STR_REQ_D_ID: o.KNT_YRN_STR_REQ_D_ID == null ? 0 : o.KNT_YRN_STR_REQ_D_ID,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        QTY_MOU_ID: 3,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID == null ? 0 : o.KNT_YRN_LOT_ID,
                        PACK_RQD_QTY: o.PACK_RQD_QTY == null ? 0 : o.PACK_RQD_QTY,
                        QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                        LOOSE_QTY: o.LOOSE_QTY == null ? 0 : o.LOOSE_QTY,
                        RQD_YRN_QTY: o.RQD_YRN_QTY == null ? 0 : o.RQD_YRN_QTY,
                        RQD_CONE_QTY: o.RQD_CONE_QTY == null ? 0 : o.RQD_CONE_QTY,
                        SP_NOTE: o.SP_NOTE
                    }
                }));

                var id = vm.form.KNT_YRN_STR_REQ_H_ID

                return KnittingDataService.saveDataByUrl(data, '/YarnIssueReq/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go('YarnTestReq', { pKNT_YRN_STR_REQ_H_ID: id }, { reload: true });
                        }
                        else {

                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPKNT_YRN_STR_REQ_H_ID) > 0) {
                                vm.form.KNT_YRN_STR_REQ_H_ID = res.data.OPKNT_YRN_STR_REQ_H_ID;
                                $state.go($state.current, { pKNT_YRN_STR_REQ_H_ID: res.data.OPKNT_YRN_STR_REQ_H_ID });
                            }
                        }

                    }
                });
            }
        };

        vm.printRequisition = function () {

            vm.form.REPORT_CODE = 'RPT-3511';

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

