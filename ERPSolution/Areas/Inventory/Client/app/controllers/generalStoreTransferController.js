(function () {
    'use strict';
    angular.module('multitex.inventory').controller('GeneralStoreTransferController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', GeneralStoreTransferController]);
    function GeneralStoreTransferController($q, config, InventoryDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.INV_STR_TR_REQ_H_ID ? formData : { RF_REQ_TYPE_ID: '12', HR_COMPANY_ID: 0, INV_ITEM_CAT_ID: 0, STR_REQ_BY: cur_user_id, FRM_STORE_ID: 0, TO_STORE_ID: 0, RF_ACTN_VIEW: 0, HR_DEPARTMENT_ID: 0 };
        vm.formItem = { 'QTY_MOU_ID': '3', 'CENTRAL_STK': 0, 'SUB_STK': 0 };
        //vm.form.INV_ITEM_CAT_ID = 2;

        if ($stateParams.pLK_STR_TRN_TYP_ID)
            vm.form.LK_STR_TRN_TYP_ID = ($stateParams.pLK_STR_TRN_TYP_ID || 643);

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), GetItemCategoryList(), ItemList(), getCurrencyList(), ReceiveItemList(), GetFromStoreList(), GetToStoreList(), GetReqTypeList(),
                GetCompanyList(), GetStatusList(), GetMOUList(), GetPackMOUList(), GetSupplierList(), GetSourceTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.STR_TR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_TR_REQ_DT_LNopened = true;
        }

        vm.loadStroeInfo = function () {

            vm.toStoreList.read();
            vm.fromStoreList.read();
        }

        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    InventoryDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        e.success(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });


        vm.checkStock = function () {
            if (vm.formItem.CENTRAL_STK < vm.formItem.RQD_QTY) {
                vm.formItem.RQD_QTY = 0;
                config.appToastMsg("MULTI-005 Requisition quantity must less than central stock quantity.");
            }
        }

        vm.getItemBrandName = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            if (fnValidateSub2() == true) {
                //var list = _.filter(vm.DyItemList.data(), { 'INV_ITEM_ID': obj.INV_ITEM_ID });
                vm.formItem.BRAND_NAME_EN = obj.BRAND_NAME_EN;
                vm.formItem.PACK_MOU_ID = obj.PACK_MOU_ID;
                vm.formItem.QTY_PER_PACK = obj.PACK_RATIO;

                InventoryDataService.getDataByFullUrl('/api/inv/StoreTransfer/GetStockInfo/' + obj.INV_ITEM_ID + '/' + vm.form.FRM_STORE_ID + '/' + vm.form.TO_STORE_ID).then(function (res) {

                    vm.formItem.CENTRAL_STK = res.CENTRAL_STK;
                    vm.formItem.SUB_STK = res.SUB_STK;
                });
            }

        }

        vm.TotalReceiveQty = function () {
            if ((vm.formItem.PACK_QTY || 0) > 0) {
                var total = parseFloat(vm.formItem.PACK_QTY) * parseFloat(vm.formItem.QTY_PER_PACK);
                var stock = parseFloat(vm.formItem.CENTRAL_STK);
                if (total > stock) {

                    vm.formItem.PACK_QTY = '';
                    vm.formItem.RQD_QTY = 0;
                }
                else {
                    vm.formItem.RQD_QTY = total;
                }
            }
            else
                vm.formItem.PACK_QTY = 0;
        };

        function GetPackMOUList() {
            return vm.PackMOUList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 1;
            var PARENT_ID = 0;
            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
            var sList = null;
            InventoryDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
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
                            InventoryDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };
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
                            InventoryDataService.getUserDatalist().then(function (res) {
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

        function ReceiveItemList() {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/inv/StoreTransfer/GetStoreTransferInfoDtlByID?pINV_STR_TR_REQ_H_ID=' + ($stateParams.pINV_STR_TR_REQ_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                data: []
            });
        };

        function GetItemCategoryList() {

            InventoryDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {

                var list = _.filter(res, function (o) { return (o.IS_LEAF === 'N' && o.PARENT_ID > 0) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: list
                });
            });
        }

        vm.getItemDataByCategory = function (e) {

            vm.toStoreList.read();
            vm.fromStoreList.read();

            var itemss = e.sender.dataItem(e.item);
            InventoryDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 0)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });



        };

        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {
                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.INV_ITEM_ID === parseInt(vm.formItem.INV_ITEM_ID)
                })[0];

                vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var mouUnit = _.filter(vm.mOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.QTY_MOU_ID)
                })[0];

                var pmouUnit = _.filter(vm.PackMOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.PACK_MOU_ID)
                })[0];

                vm.formItem.PACK_MOU_CODE = pmouUnit.MOU_CODE;
                vm.formItem.MOU_CODE = mouUnit.MOU_CODE;

                vm.formItem.INV_ITEM_CAT_ID = vm.form.INV_ITEM_CAT_ID;
                if (vm.formItem.uid) {
                    // Update

                    var row = vm.ReceiveItemList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.uid != vm.formItem.uid)
                    }).length;

                    if (count <= 1) {
                        row["INV_ITEM_ID"] = vm.formItem.INV_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                        row["BRAND_NAME_EN"] = vm.formItem.BRAND_NAME_EN;
                        row["CENTRAL_STK"] = vm.formItem.CENTRAL_STK;
                        row["SUB_STK"] = vm.formItem.SUB_STK;
                        row["MOU_CODE"] = vm.formItem.MOU_CODE;
                        row["PACK_MOU_CODE"] = vm.formItem.PACK_MOU_CODE;
                        row["INV_ITEM_CAT_ID"] = vm.formItem.INV_ITEM_CAT_ID;

                        row["QTY_MOU_ID"] = vm.formItem.QTY_MOU_ID;
                        row["PACK_MOU_ID"] = vm.formItem.PACK_MOU_ID;
                        row["PACK_QTY"] = vm.formItem.PACK_QTY;
                        row["QTY_PER_PACK"] = vm.formItem.QTY_PER_PACK;
                        row["RQD_QTY"] = vm.formItem.RQD_QTY;
                        vm.formItem = { INV_ITEM_ID: '', QTY_MOU_ID: '3' };
                        //config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.RF_BRAND_ID === vm.formItem.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === vm.formItem.LK_YRN_COLR_GRP_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.ReceiveItemList.data().length + 1;
                        vm.ReceiveItemList.insert(idx, vm.formItem);

                        var gview = vm.ReceiveItemList.data();
                        //console.log(gview);
                        vm.formItem = { INV_ITEM_ID: '', QTY_MOU_ID: '3' };
                        //config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
        };

        vm.editItemData = function (data) {
            vm.form.INV_ITEM_CAT_ID = angular.copy(data.INV_ITEM_CAT_ID);
            vm.DyItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + vm.form.INV_ITEM_CAT_ID).then(function (res) {
                            //InventoryDataService.LookupListData(92).then(function (res) {
                            e.success(res);
                            vm.formItem = angular.copy(data);
                            vm.form['QTY_MOU_ID'] = angular.copy(data.QTY_MOU_ID);
                        });
                    }
                }
            });

        }

        vm.removeItemData = function (data) {

            if (!data.KNT_YRN_RCV_D_ID || data.KNT_YRN_RCV_D_ID <= 0) {
                vm.ReceiveItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.ReceiveItemList.remove(data);
                 });

        }

        vm.resetItemData = function () {
            vm.formItem = { INV_ITEM_ID: '', QTY_MOU_ID: '3' };
        };

        vm.reset = function () {

            $state.go('YarnReceive', { pINV_STR_TR_REQ_H_ID: 0 });

        };


        //  DropdownList


        function GetPriorityList() {

            return vm.PriorityList = {
                optionLabel: "-- Select Priority --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.LookupListData(63).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };



        function GetSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "-- Select Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.LookupListData(84).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetPaymentMethodList() {

            return vm.paymentMethodList = {
                optionLabel: "-- Select Payment Method --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/common/GetPayMethod').then(function (res) {
                                //InventoryDataService.getDataByFullUrl('api/common/GetPayMethod').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_MTHD_NAME",
                dataValueField: "RF_PAY_MTHD_ID"
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
                            InventoryDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                //InventoryDataService.LookupListData(88).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetFromStoreList() {

            return vm.fromStoreList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=' + (vm.form.INV_ITEM_CAT_ID || '') + '&pSC_USER_ID=' + cur_user_id).then(function (res) {
                            if (vm.form.LK_STR_TRN_TYP_ID == 643) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 });
                                e.success(list);
                            }
                            else if (vm.form.LK_STR_TRN_TYP_ID == 644) {

                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 });
                                e.success(list);
                            }
                            else if (vm.form.LK_STR_TRN_TYP_ID == 645) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            }
                            else {
                                e.success([]);
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        }

        function GetToStoreList() {

            return vm.toStoreList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=' + (vm.form.INV_ITEM_CAT_ID || '') + '&pSC_USER_ID=' + cur_user_id).then(function (res) {
                            if (vm.form.LK_STR_TRN_TYP_ID == 643) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 });
                                e.success(list);
                            }
                            else if (vm.form.LK_STR_TRN_TYP_ID == 644) {

                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            }
                            else if (vm.form.LK_STR_TRN_TYP_ID == 645) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            }
                            else {
                                e.success([]);
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
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
                            InventoryDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        vm.departmentList = {
            optionLabel: "--Select Dept--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {

                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        function GetMOUList() {
            return vm.mOUList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
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
                { field: "INV_ITEM_ID", hidden: true },
                { field: "INV_STR_TR_REQ_H_ID", hidden: true },
                { field: "INV_STR_TR_REQ_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "25%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "15%" },
                { field: "CENTRAL_STK", title: "Central Stock", type: "string", width: "10%" },
                { field: "SUB_STK", title: "Sub-Stock", type: "string", width: "10%" },
                { field: "PACK_QTY", title: "Pack Qty", type: "string", width: "10%" },
                { field: "PACK_MOU_CODE", title: "Pack Unit", type: "string", width: "10%" },
                { field: "QTY_PER_PACK", title: "Qty/Pack", type: "string", width: "10%" },
                { field: "RQD_QTY", title: "Reqd Qty.", type: "string", width: "10%" },
                { field: "MOU_CODE", title: "MoU", type: "string", width: "10%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
            //editable: true
        };



        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var result = [];
                vm.ReceiveItemList.data().forEach(function (x) { if (!result.includes(x.INV_ITEM_CAT_ID)) result.push(x.INV_ITEM_CAT_ID) })
                var sCat = result.toString();
                data['INV_ITEM_CAT_LST'] = sCat;

                data["XML_REQ_D"] = InventoryDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        INV_STR_TR_REQ_D_ID: o.INV_STR_TR_REQ_D_ID == null ? 0 : o.INV_STR_TR_REQ_D_ID,
                        INV_STR_TR_REQ_H_ID: o.INV_STR_TR_REQ_H_ID == null ? 0 : o.INV_STR_TR_REQ_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        PACK_RQD_QTY: o.PACK_QTY,
                        PACK_MOU_ID: o.PACK_MOU_ID,
                        QTY_PER_PACK: o.QTY_PER_PACK,
                        RQD_QTY: o.RQD_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID
                    }
                }));

                var id = vm.form.INV_STR_TR_REQ_H_ID


                var _invdate = $filter('date')(data.STR_TR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["STR_TR_REQ_DT"] = _invdate;

                return InventoryDataService.saveDataByUrl(data, '/StoreTransfer/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            //config.appToastMsg("MULTI-001 Yarn receive has been updated successfully");
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pINV_STR_TR_REQ_H_ID: res.data.OPINV_STR_TR_REQ_H_ID }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPINV_STR_TR_REQ_H_ID) > 0) {
                                vm.form.INV_STR_TR_REQ_H_ID = res.data.OPINV_STR_TR_REQ_H_ID;
                                $state.go($state.current, { pINV_STR_TR_REQ_H_ID: res.data.OPINV_STR_TR_REQ_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };


        vm.updateAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var result = [];
                vm.ReceiveItemList.data().forEach(function (x) { if (!result.includes(x.INV_ITEM_CAT_ID)) result.push(x.INV_ITEM_CAT_ID) })
                var sCat = result.toString();
                data['INV_ITEM_CAT_LST'] = sCat;

                data["XML_REQ_D"] = InventoryDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        INV_STR_TR_REQ_D_ID: o.INV_STR_TR_REQ_D_ID == null ? 0 : o.INV_STR_TR_REQ_D_ID,
                        INV_STR_TR_REQ_H_ID: o.INV_STR_TR_REQ_H_ID == null ? 0 : o.INV_STR_TR_REQ_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        PACK_RQD_QTY: o.PACK_QTY,
                        PACK_MOU_ID: o.PACK_MOU_ID,
                        QTY_PER_PACK: o.QTY_PER_PACK,
                        RQD_QTY: o.RQD_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID
                    }
                }));

                var id = vm.form.INV_STR_TR_REQ_H_ID


                var _invdate = $filter('date')(data.STR_TR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["STR_TR_REQ_DT"] = _invdate;

                return InventoryDataService.saveDataByUrl(data, '/StoreTransfer/Update').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            //config.appToastMsg("MULTI-001 Yarn receive has been updated successfully");
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pINV_STR_TR_REQ_H_ID: res.data.OPINV_STR_TR_REQ_H_ID }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPINV_STR_TR_REQ_H_ID) > 0) {
                                vm.form.INV_STR_TR_REQ_H_ID = res.data.OPINV_STR_TR_REQ_H_ID;
                                $state.go($state.current, { pINV_STR_TR_REQ_H_ID: res.data.OPINV_STR_TR_REQ_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };

    }


})();

