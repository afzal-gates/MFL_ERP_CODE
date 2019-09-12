(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnOpeningBalanceController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'Dialog', '$filter', YarnOpeningBalanceController]);
    function YarnOpeningBalanceController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, Dialog, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [ItemList(), getCurrencyList(), ReceiveItemList(), GetReqSourceList(), GetReqTypeList(), GetCompanyList(), GetPaymentMethodList(), GetYarnColorGroupList(), GetMOUList(), GetSupplierList(), GetSourceTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.form = { QTY_MOU_ID: '3', SCM_STORE_ID: 1 };

        vm.TotalReceiveQty = function () {
            if (vm.form.PACK_RCV_QTY > 0 && vm.form.QTY_PER_PACK > 0)
                vm.form.RCV_QTY = vm.form.PACK_RCV_QTY * vm.form.QTY_PER_PACK;
            else
                vm.form.RCV_QTY = 0;
        };

        vm.TotalPriceAmount = function () {
            if (vm.form.RCV_QTY > 0 && vm.form.UNIT_PRICE > 0)
                vm.form.TOTAL_AMT = vm.form.RCV_QTY * vm.form.UNIT_PRICE;
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
                    KnittingDataService.getDataByFullUrl('/api/purchase/SupplierProfile/BrandListByItemCategory?pINV_ITEM_CAT_ID=2').then(function (res) {
                        //var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                        e.success(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

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

        function ReceiveItemList() {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetYarnOpeningBlance').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 20

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

        //vm.packageType = function (LK_LOC_SRC_TYPE_ID) {

        //    if (LK_LOC_SRC_TYPE_ID == 452)
        //        vm.form.PACK_BOSTA = "BOSTA";
        //    else if (LK_LOC_SRC_TYPE_ID == 453)
        //        vm.form.PACK_BOSTA = "CARTOON";
        //};

        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {
                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.INV_ITEM_ID === parseInt(vm.form.INV_ITEM_ID)
                })[0];

                vm.form.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var itemB = _.filter(vm.categoryBrandList.data(), function (o) {
                    return o.RF_BRAND_ID === parseInt(vm.form.RF_BRAND_ID)
                })[0];

                vm.form.BRAND_NAME_EN = itemB.BRAND_NAME_EN;

                vm.form.PACK_BOSTA = "Cartoon";

                var yColorG = _.filter(vm.yarnColorGroupList.data(), function (o) {
                    return o.LOOKUP_DATA_ID === parseInt(vm.form.LK_YRN_COLR_GRP_ID)
                })[0];

                vm.form.LK_YRN_COLR_GRP_NAME = yColorG.LK_DATA_NAME_EN;

                var mouUnit = _.filter(vm.mOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.form.QTY_MOU_ID)
                })[0];

                console.log(mouUnit);

                vm.form.MOU_CODE = mouUnit.MOU_CODE;



                var packUnit = _.filter(vm.PackMOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.form.PACK_MOU_ID)
                })[0];

                vm.form.PACK_MOU_CODE = packUnit.MOU_CODE;


                vm.form.INV_ITEM_CAT_ID = vm.form.INV_ITEM_CAT_ID;
                if (vm.form.uid) {
                    // Update

                    var row = vm.ReceiveItemList.getByUid(vm.form.uid);
                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.form.INV_ITEM_ID && o.uid != vm.form.uid)
                    }).length;

                    if (count <= 1) {
                        row["INV_ITEM_ID"] = vm.form.INV_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.form.ITEM_NAME_EN;
                        row["RF_BRAND_ID"] = vm.form.RF_BRAND_ID;
                        row["BRAND_NAME_EN"] = vm.form.BRAND_NAME_EN;
                        row["KNT_YRN_RCV_H_ID"] = vm.form.KNT_YRN_RCV_H_ID;
                        row["SCM_PURC_REQ_D_ID"] = vm.form.SCM_PURC_REQ_D_ID;
                        row["QTY_MOU_ID"] = vm.form.QTY_MOU_ID;
                        row["MOU_CODE"] = vm.form.MOU_CODE;
                        row["LK_YRN_COLR_GRP_ID"] = vm.form.LK_YRN_COLR_GRP_ID;
                        row["LK_YRN_COLR_GRP_NAME"] = vm.form.LK_YRN_COLR_GRP_NAME;
                        row["PACK_MOU_CODE"] = vm.form.PACK_MOU_CODE;
                        row["YRN_LOT_NO"] = vm.form.YRN_LOT_NO;
                        row["CONE_QTY"] = vm.form.CONE_QTY;

                        row["PACK_RCV_QTY"] = vm.form.PACK_RCV_QTY;
                        row["QTY_PER_PACK"] = vm.form.QTY_PER_PACK;
                        row["RCV_QTY"] = vm.form.RCV_QTY;
                        row["UNIT_PRICE"] = vm.form.UNIT_PRICE;
                        row["TOTAL_AMT"] = vm.form.TOTAL_AMT;

                        row["SP_NOTE"] = vm.form.SP_NOTE;

                        config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.form.INV_ITEM_ID && o.RF_BRAND_ID === vm.form.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === vm.form.LK_YRN_COLR_GRP_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.ReceiveItemList.data().length + 1;
                        vm.ReceiveItemList.insert(idx, vm.form);

                        var gview = vm.ReceiveItemList.data();
                        //console.log(gview);
                        config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
        };

        vm.editItemData = function (data) {
            vm.form = angular.copy(data);
            vm.form['QTY_MOU_ID'] = angular.copy(data.QTY_MOU_ID);
        }

        vm.removeItemData = function (dataOri) {

            Dialog.confirm('Removing "' + dataOri.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var data = angular.copy(dataOri);

                     return KnittingDataService.saveDataByUrl(data, '/YarnReceive/DeleteOB').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             ReceiveItemList();
                             config.appToastMsg(res.data.PMSG);
                         }
                     });

                 });
        }

        vm.filanlizedItemData = function (dataOri) {

            Dialog.confirm('Finalizing "' + dataOri.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var data = angular.copy(dataOri);
                     data.IS_FINALIZED = 'Y';
                     return KnittingDataService.saveDataByUrl(data, '/YarnReceive/SaveOB').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             ReceiveItemList();
                             config.appToastMsg(res.data.PMSG);
                         }
                     });

                 });
        }

        vm.resetItemData = function () {
            var HR_COMPANY_ID = vm.form.HR_COMPANY_ID;
            var SCM_STORE_ID = vm.form.SCM_STORE_ID;
            var RF_CURRENCY_ID = vm.form.RF_CURRENCY_ID;
            var OB_RCV_DT = vm.form.OB_RCV_DT;

            vm.form = { 'HR_COMPANY_ID': HR_COMPANY_ID, 'SCM_STORE_ID': SCM_STORE_ID, 'RF_CURRENCY_ID': RF_CURRENCY_ID, 'OB_RCV_DT': OB_RCV_DT, 'YARN_ITEM_ID': '', 'LK_YRN_COLR_GRP_ID': '', 'RF_BRAND_ID': '', 'PACK_MOU_ID': '', 'QTY_MOU_ID': '3' };
            //vm.form['uid'] = '';

        };

        vm.reset = function () {
            var HR_COMPANY_ID = vm.form.HR_COMPANY_ID;
            var SCM_STORE_ID = vm.form.SCM_STORE_ID;
            var RF_CURRENCY_ID = vm.form.RF_CURRENCY_ID;
            var OB_RCV_DT = vm.form.OB_RCV_DT;

            vm.form = { 'HR_COMPANY_ID': HR_COMPANY_ID, 'SCM_STORE_ID': SCM_STORE_ID, 'RF_CURRENCY_ID': RF_CURRENCY_ID, 'OB_RCV_DT': OB_RCV_DT, 'YARN_ITEM_ID': '', 'LK_YRN_COLR_GRP_ID': '', 'RF_BRAND_ID': '', 'PACK_MOU_ID': '', 'QTY_MOU_ID': '3' };
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

        function GetReqSourceList() {

            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 1 })
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


        function GetMOUList() {
            KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {

                var list = _.filter(res, function (x) { return x.RF_MOU_ID == 29 || x.RF_MOU_ID == 15 });
                vm.mOUList = new kendo.data.DataSource({
                    data: res
                });

                vm.PackMOUList = new kendo.data.DataSource({
                    data: list
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

        vm.selectAll = function (id) {
            //alert(id);
            for (var i = 0; i < vm.ReceiveItemList.data().length; i++) {

                if (id && vm.ReceiveItemList.data()[i].IS_FINALIZED == 'N')
                    vm.ReceiveItemList.data()[i].IS_FINALIZED_F = 'Y';
                else if (id && vm.ReceiveItemList.data()[i].IS_FINALIZED == 'N')
                    vm.ReceiveItemList.data()[i].IS_FINALIZED_F = 'N';
            }
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
            height: '600px',
            scrollable: true,
            enableRowSelection: true,
            enableSelectAll: true,
            columns: [
                { field: "KNT_YRN_RCV_OB_ID", hidden: true },
                { field: "HR_COMPANY_ID", hidden: true },
                { field: "MC_DELV_WH_ID", hidden: true },
                { field: "RF_CURRENCY_ID", hidden: true },
                { field: "YARN_ITEM_ID", hidden: true },
                { field: "KNT_YRN_LOT_ID", hidden: true },

                { field: "QTY_MOU_ID", hidden: true },
                { field: "LK_YRN_COLR_GRP_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "TOTAL_AMT", hidden: true },

                //OB_REF_NO, , LOC_CONV_RATE, LOC_UNIT_PRICE,

                { field: "ITEM_NAME_EN", title: "Yarn Item", type: "string", width: "15%" },
                { field: "YRN_COLR_GRP_NAME", title: "Color Group", type: "string", width: "10%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "8%" },
                { field: "YRN_LOT_NO", title: "Lot#", type: "string", width: "7%" },
                { field: "PACK_RCV_QTY", title: "Pack Qty.", type: "string", template: "#= kendo.toString(PACK_RCV_QTY,'n') #", width: "5%" },
                { field: "QTY_PER_PACK", title: "Qty/PACK", type: "string", template: "#= kendo.toString(QTY_PER_PACK,'n') #", width: "5%" },
                { field: "PACK_MOU_CODE", title: "Pack Unit", width: "5%" },
                { field: "RCV_QTY", title: "Total Rcv Qty.", type: "string", template: "#= kendo.toString(RCV_QTY,'n') #", width: "5%" },
                { field: "MOU_CODE", title: "UoM", type: "string", width: "7%" },
                { field: "UNIT_PRICE", title: "Cost Price", type: "decimal", template: "#= kendo.toString(UNIT_PRICE,'n') #", width: "5%" },
                //{ field: "TOTAL_AMT", title: "TTL Value", type: "decimal", template: "#= kendo.toString(TOTAL_AMT,'n') #", width: "5%" },
                { field: "CONE_QTY", title: "No of Cone", width: "5%" },

                { field: "OB_RCV_DT", title: "Rcv. Date", type: "date", template: "#= kendo.toString(kendo.parseDate(OB_RCV_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "8%" },
                {
                    title: "",
                    //<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> 
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '"  class="btn btn-xs red"><i class="fa fa-remove"></i></a>  <a  title="Edit" ng-click="vm.editItemData(dataItem)" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs green"><i class="fa fa-edit">Edit</i></a> ',
                    width: "8%"
                },
                {
                    title: "Finalize",
                    type: "boolean",
                    // <a  title="Edit" ng-click="vm.filanlizedItemData(dataItem)" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs yellow-gold"><i class="fa fa-edit">Finalize</i></a>
                    template: '<input type="checkBox" ng-model="dataItem.IS_FINALIZED_F" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" data-ng-init=' + "dataItem.IS_FINALIZED_F=dataItem.IS_FINALIZED" + ' ng-true-value="' + "'Y'" + '"" ng-false-value="' + "'N'" + '"/> <a  title="Finalize" ng-click="vm.finalizeAll(dataItem)" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '"  class="btn btn-xs yellow-gold"><i class="fa fa-save">Finalize</i></a>',
                    headerTemplate: '<input type="checkBox" ng-model="vm.allCheckBox" ng-click="vm.selectAll(vm.allCheckBox)"/>',
                    width: "7%"
                }

            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                return KnittingDataService.saveDataByUrl(data, '/YarnReceive/SaveOB').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        ReceiveItemList();
                        config.appToastMsg(res.data.PMSG);
                    }
                });
            }
        };

        vm.finalizeAll = function (dataOri) {

            var data = angular.copy(dataOri);
            data["IS_FINALIZED"] = data.IS_FINALIZED_F;

            var _date = $filter('date')(data.OB_RCV_DT, 'yyyy-MM-ddTHH:mm:ss');
            data["OB_RCV_DT"] = _date;

            return KnittingDataService.saveDataByUrl(data, '/YarnReceive/SaveOB').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    ReceiveItemList();
                    config.appToastMsg(res.data.PMSG);
                }
            });
        };
    }




})();

