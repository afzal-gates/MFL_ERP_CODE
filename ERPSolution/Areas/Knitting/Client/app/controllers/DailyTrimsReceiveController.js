(function () {
    'use strict';
    angular.module('multitex.knitting').controller('DailyTrimsReceiveController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$modal', DailyTrimsReceiveController]);
    function DailyTrimsReceiveController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.INV_TRMS_RCV_H_ID ? formData : { IS_PO: 'Y', IS_BYR_WISE: 'N', HR_COMPANY_ID: 1, SCM_STORE_ID: 5, INV_ITEM_CAT_ID: 7, RCV_BY: cur_user_id, HR_EMPLOYEE_ID: cur_user_id, MRR_DT: vm.today };
        vm.formItem = { QTY_MOU_ID: '8' };
        vm.form["INV_ITEM_CAT_ID"] = 7;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getbuyerAcGrpList(), getUserData(), GetItemCategoryList(), ItemList(), getCurrencyList(), ReceiveItemList(), getStyleColorList(),
                GetReqSourceList(), GetReqTypeList(), GetCompanyList(), GetPaymentMethodList(), GetMOUList(), GetSupplierList(), GetSourceTypeList(),
                getBuyerAcList(), getPOList(), GetStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function GetBuyerList() {

            return vm.buyerList = {
                optionLabel: "-- Select Buyer --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BUYER_NAME_EN",
                dataValueField: "MC_BUYER_ID"
            };
        };


        vm.CreateNewTrimsItem = function (items) {

            if (fnValidateSub3() == true) {

                var item = angular.copy(items);

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/Knitting/Knit/orderTrimsItemDtl',
                    controller: 'DailyTrimsAccesoriesController',
                    controllerAs: 'vmS',
                    size: 'lg',
                    windowClass: 'app-modal-window',
                    resolve: {
                        formData: function (KnittingDataService) {
                            item["INV_ITEM_CAT_ID"] = items.INV_ITEM_CAT_ID;
                            item["MC_STYLE_H_EXT_ID"] = items.MC_STYLE_H_EXT_ID;
                            item["MC_FAB_PROD_ORD_H_ID"] = items.MC_FAB_PROD_ORD_H_ID;
                            item["MC_BYR_ACC_ID"] = items.MC_BYR_ACC_ID;
                            return item;
                        }
                    }
                });
                modalInstance.result.then(function (dataItem) {
                    console.log(dataItem);
                    vm.TrimsItemList.read();

                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });
            }
        };


        function getbuyerAcGrpList() {

            vm.buyerAcGrpList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        vm.getBuyerGrpWiseStyleList = function (e) {

            var MC_BYR_ACC_GRP_ID = e.sender.dataItem(e.item).MC_BYR_ACC_GRP_ID;

            vm.byrGrpWiseBookingStyleDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/StyleHExt/BookingStyleHExtList?pMC_BYR_ACC_GRP_ID=' + (MC_BYR_ACC_GRP_ID || null);
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        var myUrl = url.replace("pORDER_NO_LST", 'pSTYLE_NO')
                        console.log(myUrl);

                        url = myUrl;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);

                            console.log(res);
                        }, function (err) {
                            console.log(err);
                        })
                    }
                }
            });

        };


        function getBuyerAcList() {

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/SelectAll').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        };

        vm.getBuyerAccWiseStyleList = function (e) {

            var MC_BUYER_ID = e.sender.dataItem(e.item).MC_BUYER_ID;

            vm.byrAccWiseBookingStyleDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/StyleHExt/ByrWiseBookingStyleHExtList/' + null + '/' + (MC_BUYER_ID || null) + '?';
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        url += '&pMC_STYLE_H_EXT_ID';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);

                            console.log(res);
                        }, function (err) {
                            console.log(err);
                        })
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

        function getStyleColorList() {
            vm.styleColorListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID"
            };

            return vm.styleColorListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + (vm.form.MC_STYLE_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getPOList() {
            KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetPOList?pIS_LOCAL_CASH_P=Y&pLK_PURC_PROD_GRP_ID=791').then(function (res) {
                vm.PoMasterList = new kendo.data.DataSource({
                    data: res,
                    pageSize: 12
                });
            }, function (err) {
                console.log(err);
            });

        }

        vm.checkPOQty = function (item) {
            if (item.PO_QTY < item.RCV_QTY) {
                item.RCV_QTY = '';
                config.appToastMsg("MULTI-005 Can not Accept More Than Order Qty!!!");
            }
        }


        vm.onChangePO = function (obj) {
            var item = angular.copy(obj);
            console.log(item);
            vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
            KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetPoDetlByID?pCM_IMP_PO_H_ID=' + item.CM_IMP_PO_H_ID).then(function (res) {
                vm.POItemList = res;
            });
        }


        if (formData.INV_TRMS_RCV_H_ID) {
            var MC_BYR_ACC_GRP_ID = formData.MC_BYR_ACC_GRP_ID;

            vm.byrGrpWiseBookingStyleDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/StyleHExt/BookingStyleHExtList?pMC_BYR_ACC_GRP_ID=' + (MC_BYR_ACC_GRP_ID || null);
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        var myUrl = url.replace("pORDER_NO_LST", 'pSTYLE_NO')
                        console.log(myUrl);

                        url = myUrl + '&pMC_STYLE_H_EXT_ID=' + formData.MC_STYLE_H_EXT_ID;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);

                            console.log(res);
                        }, function (err) {
                            console.log(err);
                        })
                    }
                }
            });
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

        $scope.REQ_DOC_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REQ_DOC_DT_LNopened = true;
        }

        $scope.INVOICE_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.INVOICE_DT_LNopened = true;
        }

        $scope.CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.CHALAN_DT_LNopened = true;
        }


        function ReceiveItemList() {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/DailyTrimsReceive/GetDailyTrimsReceiveInfoDtlByID?pINV_TRMS_RCV_H_ID=' + ($stateParams.pINV_TRMS_RCV_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function ItemList() {
            return vm.TrimsItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/DailyTrimsReceive/GetTrimsItemListByID?pINV_ITEM_CAT_ID=7&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || 0) + '&pMC_BYR_ACC_ID=' + (vm.form.MC_BYR_ACC_ID || 0)).then(function (res) {
                            e.success([{
                                ITEM_NAME_EN: '--New Trims Item--',
                                MC_ORD_TRMS_ITEM_ID: -1
                            }].concat(res));
                        });
                    }
                }
            });
        };

        function GetItemCategoryList() {

            KnittingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var filter = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 7) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: filter
                });
                vm.form.INV_ITEM_CAT_ID = 7;
            });
        }

        vm.getItemDataByCategory = function (e) {
            //var item = e.sender.dataItem(e.item);

            //vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
            //vm.form.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;

            //vm.TrimsItemList.read();
            //vm.styleColorListDataSource.read();

            //console.log(item);

        };

        vm.getItemDataByBuyer = function () {
            console.log(vm.form);

            vm.TrimsItemList.read();
            vm.styleColorListDataSource.read();
        }

        vm.getItemDtl = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            if (obj.MC_ORD_TRMS_ITEM_ID < 0) {
                vm.CreateNewTrimsItem(vm.form);
            }
            else {

                vm.formItem.ITEM_SPEC_AUTO = obj.ITEM_SPEC_AUTO;
                vm.formItem.PARTICULARS_LST = obj.PARTICULARS_VALUE;
            }

        };

        //vm.packageType = function (LK_LOC_SRC_TYPE_ID) {

        //    if (LK_LOC_SRC_TYPE_ID == 452)
        //        vm.formItem.PACK_BOSTA = "BOSTA";
        //    else if (LK_LOC_SRC_TYPE_ID == 453)
        //        vm.formItem.PACK_MOU_CODE = "CARTOON";
        //};

        vm.addToGrid = function (data) {

            if (vm.form.IS_PO == 'Y') {
                var item = angular.copy(data);
                var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                    return (o.MC_ORD_TRMS_ITEM_ID === item.MC_ORD_TRMS_ITEM_ID)
                }).length;

                if (count == 0) {

                    var idx = vm.ReceiveItemList.data().length + 1;
                    vm.ReceiveItemList.insert(idx, item);
                }
                else {
                    config.appToastMsg("MULTI-005 Duplicate item name exists!");
                }
            }
            else {
                if (fnValidateSub() == true) {

                    var ItemID = vm.formItem.MC_ORD_TRMS_ITEM_ID;

                    var item = _.filter(vm.TrimsItemList.data(), function (o) {
                        return parseInt(o.MC_ORD_TRMS_ITEM_ID) === parseInt(vm.formItem.MC_ORD_TRMS_ITEM_ID)
                    })[0];
                    console.log(item);
                    vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                    var mouUnit = _.filter(vm.mOUList.data(), function (o) {
                        return o.RF_MOU_ID === parseInt(vm.formItem.QTY_MOU_ID)
                    })[0];


                    var packUnit = _.filter(vm.packMoUList.data(), function (o) {
                        return o.RF_MOU_ID === parseInt(vm.formItem.PACK_MOU_ID)
                    })[0];

                    vm.formItem.MOU_CODE = mouUnit.MOU_CODE;

                    if (vm.formItem.uid) {
                        // Update

                        var row = vm.ReceiveItemList.getByUid(vm.formItem.uid);
                        var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                            return (o.MC_ORD_TRMS_ITEM_ID === vm.formItem.MC_ORD_TRMS_ITEM_ID && o.uid != vm.formItem.uid)
                        }).length;

                        if (count <= 1) {
                            row["MC_ORD_TRMS_ITEM_ID"] = vm.formItem.MC_ORD_TRMS_ITEM_ID;
                            row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                            row["ITEM_SPEC_AUTO"] = vm.formItem.ITEM_SPEC_AUTO;
                            row["QTY_MOU_ID"] = vm.formItem.QTY_MOU_ID;
                            row["MOU_CODE"] = vm.formItem.MOU_CODE;
                            row["RCV_QTY"] = vm.formItem.RCV_QTY;

                            vm.formItem = { 'MC_ORD_TRMS_ITEM_ID': '', 'QTY_MOU_ID': '8' };

                            //config.appToastMsg("Item information has been updated successfully!");
                        }
                        else {
                            //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                        }

                    } else {
                        //insert
                        console.log(data);
                        console.log(vm.ReceiveItemList.data());

                        var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                            return (parseInt(o.MC_ORD_TRMS_ITEM_ID) === parseInt(vm.formItem.MC_ORD_TRMS_ITEM_ID))
                        }).length;

                        if (count == 0) {

                            var idx = vm.ReceiveItemList.data().length + 1;
                            vm.ReceiveItemList.insert(idx, vm.formItem);

                            var gview = vm.ReceiveItemList.data();
                            vm.formItem = { 'MC_ORD_TRMS_ITEM_ID': '', 'QTY_MOU_ID': '8' };
                            //console.log(gview);
                            //config.appToastMsg("Item information has been added successfully!");
                        }
                        else {
                            config.appToastMsg("MULTI-005 Duplicate item name exists!");
                        }
                    }
                }
            }
        };

        vm.editItemData = function (data) {
            vm.form['INV_ITEM_CAT_ID'] = angular.copy(data.INV_ITEM_CAT_ID);
            var obj = angular.copy(data);
            console.log(obj);
            vm.formItem = {
                INV_TRMS_RCV_D_ID: obj.INV_TRMS_RCV_D_ID,
                INV_TRMS_RCV_H_ID: obj.INV_TRMS_RCV_H_ID,
                MC_ORD_TRMS_ITEM_ID: obj.MC_ORD_TRMS_ITEM_ID,
                RCV_QTY: obj.RCV_QTY,
                REJ_QTY: obj.REJ_QTY,
                QTY_MOU_ID: obj.QTY_MOU_ID,
                CM_IMP_PO_D_ID: obj.CM_IMP_PO_D_ID,
                uid: obj.uid,
                ITEM_SPEC_AUTO: obj.ITEM_SPEC_AUTO,
                ITEM_NAME_EN: obj.ITEM_NAME_EN,
                PARTICULARS_LST: obj.PARTICULARS,
                PARTICULARS: obj.PARTICULARS
            }
        }

        vm.removeItemData = function (data) {

            if (!data.INV_TRMS_RCV_D_ID || data.INV_TRMS_RCV_D_ID <= 0) {
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
            $state.go('DailyTrimsReceive', { pINV_TRMS_RCV_H_ID: 0 });

        };


        //  DropdownList


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
                                var list = _.filter(res, function (o) { return o.IS_R_I == 'R' && (o.RF_REQ_TYPE_ID == 1 || o.RF_REQ_TYPE_ID == 2) });
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

            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pSC_USER_ID=' + cur_user_id).then(function (res) {
                                //var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 7 || x.SCM_STORE_ID == 8 || x.SCM_STORE_ID == 9 })
                                e.success(res);
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



        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 25;
            var PARENT_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            KnittingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "AFZAL" })
                console.log(sList);
                if (sList.length == 1) {
                    vm.form.RF_ACTN_VIEW = 0;
                    vm.form.RF_ACTN_STATUS_ID = sList[0].RF_ACTN_STATUS_ID;
                    vm.form.ACTN_STATUS_NAME = sList[0].ACTN_STATUS_NAME;
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
                                var lst = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "AFZAL" })
                                if (lst.length == 1) {
                                    vm.form.RF_ACTN_VIEW = 0;
                                    vm.form.RF_ACTN_STATUS_ID = lst[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = lst[0].ACTN_STATUS_NAME;
                                }
                                else {
                                    vm.form.RF_ACTN_VIEW = 1;
                                }
                                e.success(lst);
                            });
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };
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
                { field: "MC_ORD_TRMS_ITEM_ID", hidden: true },
                { field: "INV_TRMS_RCV_H_ID", hidden: true },
                { field: "INV_TRMS_RCV_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "CM_IMP_PO_D_ID", hidden: true },

                { field: "ORDER_DTL", title: "Order Details", type: "string", width: "25%" },
                { field: "ITEM_NAME_EN", title: "Trims Item", type: "string", width: "15%" },
                { field: "ITEM_SPEC_AUTO", title: "Item Specification", type: "string", width: "15%" },
                { field: "PARTICULARS", title: "Particulars", type: "string", width: "20%" },
                { field: "PO_QTY", title: "PO Qty.", type: "string", width: "7%" },
                { field: "RCV_QTY", title: "Total Rcv Qty.", type: "string", width: "7%" },
                { field: "MOU_CODE", title: "UoM", type: "string", width: "7%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
            //editable: true
        };


        vm.form.INV_ITEM_CAT_ID = 0;

        vm.confirmAll = function (dataOri, id) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data['IS_FINALIZED'] = "N";
                data['IS_UPDATE'] = id;

                data["XML_RCV_D"] = KnittingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        INV_TRMS_RCV_D_ID: o.INV_TRMS_RCV_D_ID == null ? 0 : o.INV_TRMS_RCV_D_ID,
                        INV_TRMS_RCV_H_ID: o.INV_TRMS_RCV_H_ID == null ? 0 : o.INV_TRMS_RCV_H_ID,
                        MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID,
                        CM_IMP_PO_D_ID: o.CM_IMP_PO_D_ID == 0 ? null : o.CM_IMP_PO_D_ID,
                        ITEM_SPEC_AUTO: o.ITEM_SPEC_AUTO,
                        QTY_MOU_ID: o.QTY_MOU_ID,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY
                    }
                }));

                var id = vm.form.INV_TRMS_RCV_H_ID

                return KnittingDataService.saveDataByUrl(data, '/DailyTrimsReceive/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (parseInt(id) > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go('DailyTrimsReceive', { pINV_TRMS_RCV_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPINV_TRMS_RCV_H_ID) > 0) {
                                vm.form.INV_TRMS_RCV_H_ID = res.data.OPINV_TRMS_RCV_H_ID;
                                $state.go($state.current, { pINV_TRMS_RCV_H_ID: res.data.OPINV_TRMS_RCV_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };

        vm.submitAll = function (dataOri, id) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                data['IS_UPDATE'] = id;

                data["XML_RCV_D"] = KnittingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        INV_TRMS_RCV_D_ID: o.INV_TRMS_RCV_D_ID == null ? 0 : o.INV_TRMS_RCV_D_ID,
                        INV_TRMS_RCV_H_ID: o.INV_TRMS_RCV_H_ID == null ? 0 : o.INV_TRMS_RCV_H_ID,
                        MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID,
                        CM_IMP_PO_D_ID: o.CM_IMP_PO_D_ID == 0 ? null : o.CM_IMP_PO_D_ID,
                        ITEM_SPEC_AUTO: o.ITEM_SPEC_AUTO,
                        QTY_MOU_ID: o.QTY_MOU_ID,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY
                    }
                }));

                var id = vm.form.INV_TRMS_RCV_H_ID

                return KnittingDataService.saveDataByUrl(data, '/DailyTrimsReceive/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (parseInt(id) > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go('DailyTrimsReceive', { pINV_TRMS_RCV_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPINV_TRMS_RCV_H_ID) > 0) {
                                vm.form.INV_TRMS_RCV_H_ID = res.data.OPINV_TRMS_RCV_H_ID;
                                $state.go($state.current, { pINV_TRMS_RCV_H_ID: res.data.OPINV_TRMS_RCV_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };
    }


})();

