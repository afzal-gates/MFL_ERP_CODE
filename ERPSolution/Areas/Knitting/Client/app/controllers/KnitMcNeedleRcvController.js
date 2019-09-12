(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitMcNeedleRcvController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', KnitMcNeedleRcvController]);
    function KnitMcNeedleRcvController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), GetItemCategoryList(), ItemList(), getCurrencyList(), ReceiveItemList(), GetReqSourceList(),
                GetReqTypeList(), GetPriorityList(), getTnaStatusList(), GetCompanyList(), GetPaymentMethodList(), getFiscalYearData(),
                GetYarnColorGroupList(), GetMOUList(), GetSupplierList(), GetSourceTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.form = formData.KNT_NDL_STR_RCV_H_ID ? formData : { RF_REQ_TYPE_ID: 1, HR_COMPANY_ID: 1, SCM_STORE_ID: 1, PACK_MOU_ID: 18, IS_LC_PNDNG: 'N', INV_ITEM_CAT_ID: '211', ITEM_RCV_BY: cur_user_id, CREATED_BY: cur_user_id, MRR_DT: vm.today };
        vm.formItem = { QTY_MOU_ID: '1', PACK_MOU_ID: 18 };


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

        vm.getStoreByID = function () {

            if (vm.form.HR_COMPANY_ID > 0) {

                KnittingDataService.getDataByFullUrl('/api/common/StoreListByOfcComCatID?pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID || 0) + '&pINV_ITEM_CAT_LST=12').then(function (res) {
                    vm.reqSourceList = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
        }


        vm.setPaymentMethod = function () {
            if (vm.form.RF_REQ_TYPE_ID == 2)
                vm.form.RF_PAY_MTHD_ID = 4;
            else if (vm.form.LK_LOC_SRC_TYPE_ID == 453)
                vm.form.RF_PAY_MTHD_ID = 1;
            else
                if (vm.form.RF_PAY_MTHD_ID == 4)
                    vm.form.RF_PAY_MTHD_ID = "";

        }

        vm.BrandListBySupplierID = function () {

            return vm.categoryBrandList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetSupplierBrandList?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID).then(function (res) {

                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }


        vm.TotalReceiveQty = function () {
            if (vm.formItem.LOOSE_QTY < vm.formItem.QTY_PER_PACK)
                if (vm.formItem.PACK_RCV_QTY > 0 && vm.formItem.QTY_PER_PACK > 0)
                    vm.formItem.RCV_QTY = (vm.formItem.PACK_RCV_QTY * vm.formItem.QTY_PER_PACK) + vm.formItem.LOOSE_QTY;
                else
                    vm.formItem.RCV_QTY = 0;
            else
                vm.formItem.LOOSE_QTY = 0;

            vm.TotalPriceAmount();
        };

        vm.TotalPriceAmount = function () {
            if (vm.formItem.RCV_QTY > 0 && vm.formItem.UNIT_PRICE > 0)
                vm.formItem.TOTAL_AMT = vm.formItem.RCV_QTY * vm.formItem.UNIT_PRICE;
            else
                vm.formItem.TOTAL_AMT = 0;
        };

        $scope.MRR_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.MRR_DT_LNopened = true;
        }

        $scope.BL_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.BL_DT_LNopened = true;
        }

        $scope.RCV_DOC_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RCV_DOC_DT_LNopened = true;
        }

        $scope.CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.CHALAN_DT_LNopened = true;
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


        function ReceiveItemList() {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/KnitMcNeedleRcv/GetKnitMcNeedleRcvInfoDtlByID?pKNT_NDL_STR_RCV_H_ID=' + ($stateParams.pKNT_NDL_STR_RCV_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=211,128').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function GetItemCategoryList() {

            KnittingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 128 || o.INV_ITEM_CAT_ID === 211) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: list
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var obj = e.sender.dataItem(e.item);
            KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (obj.INV_ITEM_CAT_ID || 128)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        vm.getItemDtl = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            vm.formItem.PACK_MOU_ID = obj.PACK_MOU_ID;
            vm.formItem.QTY_PER_PACK = obj.PACK_RATIO;
            vm.formItem.QTY_MOU_ID = obj.CONS_MOU_ID;
            vm.formItem.PCT_ADDL_PRICE = obj.PCT_ADDL_PRICE;
            vm.formItem.UNIT_PRICE = obj.PURCH_PRICE;

        };

        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {

                var ItemID = vm.formItem.INV_ITEM_ID;
                var packID = vm.formItem.PACK_MOU_ID;

                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.INV_ITEM_ID === parseInt(vm.formItem.INV_ITEM_ID)
                })[0];

                vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var mouUnit = _.filter(vm.mOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.QTY_MOU_ID)
                })[0];

                var packUnit = _.filter(vm.packMoUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.PACK_MOU_ID)
                })[0];

                vm.formItem.MOU_CODE = mouUnit.MOU_CODE;
                vm.formItem.PACK_MOU_CODE = packUnit.MOU_CODE;

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
                        row["KNT_NDL_STR_RCV_H_ID"] = vm.formItem.KNT_NDL_STR_RCV_H_ID;
                        row["QTY_MOU_ID"] = vm.formItem.QTY_MOU_ID;
                        row["MOU_CODE"] = vm.formItem.MOU_CODE;
                        row["PACK_MOU_ID"] = vm.formItem.PACK_MOU_ID;
                        row["PACK_MOU_CODE"] = vm.formItem.PACK_MOU_CODE;

                        row["PACK_RCV_QTY"] = vm.formItem.PACK_RCV_QTY;
                        row["QTY_PER_PACK"] = vm.formItem.QTY_PER_PACK;
                        row["RCV_QTY"] = vm.formItem.RCV_QTY;
                        row["UNIT_PRICE"] = vm.formItem.UNIT_PRICE;
                        row["TOTAL_AMT"] = vm.formItem.TOTAL_AMT;

                        vm.formItem = { 'INV_ITEM_ID': ItemID, 'QTY_MOU_ID': '1', 'PACK_MOU_ID': 18 };

                        //config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.RF_BRAND_ID === vm.formItem.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === vm.formItem.LK_YRN_COLR_GRP_ID && o.YRN_LOT_NO === vm.formItem.YRN_LOT_NO)
                    }).length;

                    if (count == 0) {

                        var idx = vm.ReceiveItemList.data().length + 1;
                        vm.ReceiveItemList.insert(idx, vm.formItem);

                        var gview = vm.ReceiveItemList.data();
                        vm.formItem = { 'INV_ITEM_ID': ItemID, 'QTY_MOU_ID': '1', 'PACK_MOU_ID': 18 };
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
            //GetItemCategoryList();
            vm.form['INV_ITEM_CAT_ID'] = angular.copy(data.INV_ITEM_CAT_ID);
            vm.formItem = angular.copy(data);
            vm.TotalReceiveQty();
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
            vm.formItem = {};
            vm.formItem['HR_COUNTRY_ID'] = '';
            vm.formItem['LK_OFF_TYPE_ID'] = '';
        };

        vm.reset = function () {

            $state.go($state.current, { pKNT_NDL_STR_RCV_H_ID: 0 });

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
                            KnittingDataService.LookupListData(63).then(function (res) {
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
                            KnittingDataService.LookupListData(84).then(function (res) {
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
                            KnittingDataService.getDataByFullUrl('/api/common/GetPayMethod').then(function (res) {
                                //KnittingDataService.getDataByFullUrl('api/common/GetPayMethod').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_MTHD_NAME",
                dataValueField: "RF_PAY_MTHD_ID"
            };
        };

        function GetStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
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
                                var list = _.filter(res, function (o) { return o.IS_R_I == 'R' });
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
            return vm.reqSourceList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=12').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        //function GetReqSourceList() {

        //    return vm.reqSourceList = {
        //        optionLabel: "-- Select Store --",
        //        filter: "contains",
        //        autoBind: true,
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
        //                        //KnittingDataService.LookupListData(92).then(function (res) {
        //                        e.success(res);
        //                    });
        //                }
        //            }
        //        },
        //        dataTextField: "STORE_NAME_EN",
        //        dataValueField: "SCM_STORE_ID"
        //    };
        //};

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

        vm.departmentList = {
            optionLabel: "--Select Dept--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {

                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        function GetMOUList() {

            KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                vm.mOUList = new kendo.data.DataSource({
                    data: res
                });
                var list = _.filter(res, function (x) { return x.RF_MOU_ID == 29 || x.RF_MOU_ID == 15 })
                vm.packMoUList = new kendo.data.DataSource({
                    data: res
                });
            });

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

        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAll').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getTnaStatusList() {
            return vm.TnaStatusList = {
                optionLabel: "-- Select TNA Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectApprovRejectStatus?pMC_TNA_TASK_ID=46').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "EVENT_NAME",
                dataValueField: "MC_TNA_TASK_STATUS_ID"
            };
        };

        function getFiscalYearData() {
            return vm.FiscalYearData = {
                optionLabel: "-- Select Fiscal Year --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/hr/hrleavetrans/FiscalYearDataAll').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "FY_NAME_EN",
                dataValueField: "RF_FISCAL_YEAR_ID"
            };
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
                { field: "KNT_NDL_STR_RCV_H_ID", hidden: true },
                { field: "KNT_NDL_STR_RCV_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },
                { field: "PCT_ADDL_PRICE", hidden: true },

                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "17%" },
                { field: "PACK_RCV_QTY", title: "Receive Qty.", type: "string", width: "5%" },
                { field: "PACK_MOU_CODE", title: "Pack Unit", type: "string", width: "10%" },
                { field: "QTY_PER_PACK", title: "Qty/PACK", type: "string", width: "5%" },
                { field: "LOOSE_QTY", title: "Loose Qty", type: "string", width: "5%" },

                { field: "RCV_QTY", title: "Total Rcv Qty.", type: "string", width: "5%" },
                { field: "MOU_CODE", title: "UoM", type: "string", width: "5%" },
                { field: "UNIT_PRICE", title: "Rate", type: "decimal", width: "5%" },
                { field: "TOTAL_AMT", title: "TTL Value", type: "decimal", width: "5%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };


        vm.form.INV_ITEM_CAT_ID = 211;

        vm.confirmAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data['IS_FINALIZED'] = "Y";

                var _mrrdate = $filter('date')(data.MRR_DT, 'yyyy-MM-dd');
                data["MRR_DT"] = _mrrdate;

                var _clndate = $filter('date')(data.CHALAN_DT, 'yyyy-MM-dd');
                data["CHALAN_DT"] = _clndate;

                var _invdate = $filter('date')(data.RCV_DOC_DT, 'yyyy-MM-dd');
                data["RCV_DOC_DT"] = _invdate;

                data["XML_RCV_D"] = KnittingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        KNT_NDL_STR_RCV_D_ID: o.KNT_NDL_STR_RCV_D_ID == null ? 0 : o.KNT_NDL_STR_RCV_D_ID,
                        KNT_NDL_STR_RCV_H_ID: o.KNT_NDL_STR_RCV_H_ID == null ? 0 : o.KNT_NDL_STR_RCV_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        QTY_MOU_ID: o.QTY_MOU_ID,
                        PACK_MOU_ID: o.PACK_MOU_ID,
                        PCT_ADDL_PRICE: o.PCT_ADDL_PRICE,
                        COST_PRICE: (o.UNIT_PRICE + (o.PCT_ADDL_PRICE || 0)) * (vm.form.LOC_CONV_RATE || 1),
                        YRN_LOT_NO: o.YRN_LOT_NO == null ? 0 : o.YRN_LOT_NO,
                        PACK_RCV_QTY: o.PACK_RCV_QTY == null ? 0 : o.PACK_RCV_QTY,
                        QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                        LOOSE_QTY: o.LOOSE_QTY == null ? 0 : o.LOOSE_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        SP_NOTE: o.SP_NOTE
                    }
                }));

                var id = vm.form.KNT_NDL_STR_RCV_H_ID

                return KnittingDataService.saveDataByUrl(data, '/KnitMcNeedleRcv/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (parseInt(id) > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pKNT_NDL_STR_RCV_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPKNT_NDL_STR_RCV_H_ID) > 0) {
                                vm.form.KNT_NDL_STR_RCV_H_ID = res.data.OPKNT_NDL_STR_RCV_H_ID;
                                $state.go($state.current, { pKNT_NDL_STR_RCV_H_ID: res.data.OPKNT_NDL_STR_RCV_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data['IS_FINALIZED'] = "N";

                var _mrrdate = $filter('date')(data.MRR_DT, 'yyyy-MM-dd');
                data["MRR_DT"] = _mrrdate;

                var _clndate = $filter('date')(data.CHALAN_DT, 'yyyy-MM-dd');
                data["CHALAN_DT"] = _clndate;

                var _invdate = $filter('date')(data.RCV_DOC_DT, 'yyyy-MM-dd');
                data["RCV_DOC_DT"] = _invdate;

                data["XML_RCV_D"] = KnittingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        KNT_NDL_STR_RCV_D_ID: o.KNT_NDL_STR_RCV_D_ID == null ? 0 : o.KNT_NDL_STR_RCV_D_ID,
                        KNT_NDL_STR_RCV_H_ID: o.KNT_NDL_STR_RCV_H_ID == null ? 0 : o.KNT_NDL_STR_RCV_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        QTY_MOU_ID: o.QTY_MOU_ID,
                        PACK_MOU_ID: o.PACK_MOU_ID,
                        PCT_ADDL_PRICE: o.PCT_ADDL_PRICE,
                        COST_PRICE: (o.UNIT_PRICE + (o.PCT_ADDL_PRICE || 0)) * (vm.form.LOC_CONV_RATE || 1),
                        YRN_LOT_NO: o.YRN_LOT_NO == null ? 0 : o.YRN_LOT_NO,
                        PACK_RCV_QTY: o.PACK_RCV_QTY == null ? 0 : o.PACK_RCV_QTY,
                        QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                        LOOSE_QTY: o.LOOSE_QTY == null ? 0 : o.LOOSE_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        SP_NOTE: o.SP_NOTE
                    }
                }));

                var id = vm.form.KNT_NDL_STR_RCV_H_ID

                return KnittingDataService.saveDataByUrl(data, '/KnitMcNeedleRcv/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (parseInt(id) > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pKNT_NDL_STR_RCV_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPKNT_NDL_STR_RCV_H_ID) > 0) {
                                vm.form.KNT_NDL_STR_RCV_H_ID = res.data.OPKNT_NDL_STR_RCV_H_ID;
                                $state.go($state.current, { pKNT_NDL_STR_RCV_H_ID: res.data.OPKNT_NDL_STR_RCV_H_ID }, { reload: true });
                            }
                        }
                    }
                });
            }
        };
    }


})();

