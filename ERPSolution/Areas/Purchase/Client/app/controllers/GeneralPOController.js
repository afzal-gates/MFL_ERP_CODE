(function () {
    'use strict';
    angular.module('multitex.purchase').controller('GeneralPOController', ['$q', 'config', 'PurchaseDataService', '$stateParams', '$state', '$scope', 'Dialog', '$http', '$modal', 'commonDataService', 'cur_user_id', '$filter', 'formData', GeneralPOController]);
    function GeneralPOController($q, config, PurchaseDataService, $stateParams, $state, $scope, Dialog, $http, $modal, commonDataService, cur_user_id, $filter, formData) {
        //mydt();
        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.EDIT = 0;

        var supID = formData.SCM_SUPPLIER_ID ? angular.copy(formData.SCM_SUPPLIER_ID) : 0;

        vm.form = formData.CM_IMP_PO_H_ID ? formData : {
            CM_IMP_PO_H_ID: 0, DELV_STORE_ID: 0, PURC_REQ_BY: cur_user_id, PURC_REQ_TO: cur_user_id,
            IS_SELECT: false, CP_NAME_EN: 'N/A', PO_DT_IMP: vm.today, LK_PURC_PROD_GRP_ID: 792
        };
        vm.form.CP_NAME_EN = "N/A";

        vm.formItem = {};
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetSupplierList(), getCurrencyList(), RequisitionItemList(), GetReqSourceList(), GetPaymentMethodList(),
                GetCompanyList(), GetMOUList(), GetSourceTypeList(), GetStoreList(), GetShipModeList(),
                GetPaymentTermList(), GetDeliveryPlaceList(), GetIncoTermList(), getReqMasterList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        if ($stateParams.pSCM_PURC_REQ_H_ID > 0 && vm.form.CM_IMP_PO_H_ID == 0) {
            PurchaseDataService.getDataByFullUrl('/api/knit/KnitYarnReq/ReqDtlByID?pSCM_PURC_REQ_H_ID=' + $stateParams.pSCM_PURC_REQ_H_ID).then(function (res) {
                vm.RequisitionItemList = res;
            });
        }

        if (formData) {
            vm.importOrderList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetPoDetlByID?pCM_IMP_PO_H_ID=' + vm.form.CM_IMP_PO_H_ID).then(function (res) {
                            var ttl_po = 0;
                            for (var i = 0; i < res.length; i++) {
                                var obj = res[i];
                                ttl_po = ttl_po + (obj.PO_QTY * obj.UNIT_PRICE);
                                res["TTL_VALUE"] = angular.copy((obj.PO_QTY * obj.UNIT_PRICE))
                            }
                            vm.form.PI_NET_AMT = ttl_po.toFixed(2);
                            console.log(res);
                            e.success(res);
                        });
                    }
                }
            });


        }
        else {
            vm.importOrderList = new kendo.data.DataSource({
                data: []
            });
        }

        if ($stateParams.pREVISE > 0) {
            vm.form["IS_REVISE"] = true;
            vm.form.REVISION_NO = formData.REVISION_NO + 1;
            vm.form.REVISION_DT = '';
            vm.form.CP_NAME_EN = 'N/A';
            vm.loadAllsupplier;
        }
        else {
            vm.form["IS_REVISE"] = false;
            vm.form.CP_NAME_EN = 'N/A';
        }


        vm.copyCompany = function (obj) {
            if (obj)
                vm.form.BILL_TO_COMP_ID = vm.form.HR_COMPANY_ID;
            else
                vm.form.BILL_TO_COMP_ID = "";
        }

        vm.onChangeReq = function (obj) {
            var item = angular.copy(obj);
            console.log(item);
            //vm.form = item;
            //vm.loadAllsupplier();

            PurchaseDataService.getDataByFullUrl('/api/knit/KnitYarnReq/ReqDtlByID?pSCM_PURC_REQ_H_ID=' + item.SCM_PURC_REQ_H_ID).then(function (res) {
                vm.RequisitionItemList = res;
            });
        }

        vm.checkBalance = function (item) {
            if (parseFloat(item.BAL_QTY) < parseFloat(item.RQD_QTY)) {
                item.RQD_QTY = 0;
                config.appToastMsg("MULTI-005 Required Qty Exceed!");
            }
        }

        vm.clearGrid = function () {
            vm.EDIT = 0;
            vm.RequisitionItemList = []
        }


        vm.copyAll = function () {
            var obj = angular.copy(vm.RequisitionItemList[0]);
            for (var i = 1; i < vm.RequisitionItemList.length; i++) {
                vm.RequisitionItemList[i].DELV_TGT_DT = angular.copy(obj.DELV_TGT_DT);
            }
        }

        vm.AddToGrid = function () {

            if (fnValidateSub() == true) {

                for (var i = 0; i < vm.RequisitionItemList.length; i++) {

                    var idx = vm.importOrderList.data().length + 1;

                    var obj = vm.RequisitionItemList[i];
                    console.log(obj);

                    if (obj.IS_SELECT == true) {

                        var lst = _.filter(vm.importOrderList.data(), function (x) { return x.SCM_PURC_REQ_D_ID == obj.SCM_PURC_REQ_D_ID })
                        if (lst.length == 0) {
                            obj["TTL_VALUE"] = obj.RQD_QTY * obj.MKT_RATE;
                            obj["PO_QTY"] = obj.RQD_QTY;
                            obj["UNIT_PRICE"] = obj.MKT_RATE;

                            vm.importOrderList.insert(idx, obj);
                        }
                        else {

                            config.appToastMsg("MULTI-005 Duplicate Records!");
                            return;
                        }
                    }
                }

                vm.EDIT = 0;
                var ttl_po = 0;
                for (var i = 0; i < vm.importOrderList.data().length; i++) {
                    var obj = vm.importOrderList.data()[i];
                    ttl_po = ttl_po + (obj.PO_QTY * obj.UNIT_PRICE);
                }
                vm.form.PI_NET_AMT = ttl_po;
            }
            //vm.CalculateDiscount();
        }


        vm.updateToGrid = function () {

            for (var i = 0; i < vm.RequisitionItemList.length; i++) {

                var obj = vm.RequisitionItemList[i];
                var row = vm.importOrderList.getByUid(obj.uid);
                //var row = _.filter(vm.importOrderList.data(), function (x) { return x.uid == obj.uid });
                row["SCM_PURC_REQ_D_ID"] = obj.SCM_PURC_REQ_D_ID;
                row["CM_IMP_PO_D_ID"] = obj.CM_IMP_PO_D_ID;
                row["INV_ITEM_ID"] = obj.INV_ITEM_ID;
                row["LK_YRN_COLR_GRP_ID"] = obj.LK_YRN_COLR_GRP_ID;
                row["MC_ORD_TRMS_ITEM_ID"] = obj.MC_ORD_TRMS_ITEM_ID;
                row["RF_BRAND_ID"] = obj.RF_BRAND_ID;
                row["QTY_MOU_ID"] = obj.QTY_MOU_ID;

                row["ITEM_NAME_EN"] = obj.ITEM_NAME_EN;
                row["BRAND_NAME_EN"] = obj.BRAND_NAME_EN;
                row["LOT_REF_NO"] = obj.LOT_REF_NO;
                row["HS_CODE"] = obj.HS_CODE;
                row["PO_QTY"] = obj.RQD_QTY;
                row["UNIT_PRICE"] = obj.MKT_RATE;
                row["PURC_REQ_NO"] = obj.ORDER_DTL;
                row["QTY_MOU_ID"] = obj.QTY_MOU_ID;
                row["LK_YRN_COLR_GRP_NAME"] = obj.LK_YRN_COLR_GRP_NAME;
                var total = obj.RQD_QTY * obj.MKT_RATE;
                row["TTL_VALUE"] = total.toFixed(2);
            }

            var ttl_po = 0;
            for (var i = 0; i < vm.importOrderList.data().length; i++) {
                var obj = vm.importOrderList.data()[i];
                ttl_po = ttl_po + (obj.PO_QTY * obj.UNIT_PRICE);
            }
            vm.form.PI_NET_AMT = ttl_po;

            vm.EDIT = 0;
            vm.RequisitionItemList = [];
            //vm.CalculateDiscount();
        }


        vm.editData = function (dataItem) {
            var item = angular.copy(dataItem);
            console.log(item);
            item["IS_SELECT"] = true;
            item["RQD_QTY"] = parseFloat(item.PO_QTY);
            item["BAL_QTY"] = parseFloat(item.PO_QTY);
            item["MKT_RATE"] = item.UNIT_PRICE;
            item["IS_SELECT"] = true;
            vm.EDIT = 1;
            vm.RequisitionItemList = [item];
            console.log(vm.RequisitionItemList);
        }

        vm.removeData = function (data) {

            Dialog.confirm('Finalizing "' + data.MC_ORD_TRMS_ITEM_ID + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var List = _.filter(vm.importOrderList.data(), function (x) { return x.MC_ORD_TRMS_ITEM_ID == data.MC_ORD_TRMS_ITEM_ID });

                     for (var i = 0; i < List.length; i++) {
                         var item = List[i];
                         vm.importOrderList.remove(item);
                     }

                 });
        }

        vm.gridOptions = {
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
            height: '400px',
            scrollable: true,
            columns: [

                { field: "CM_IMP_PO_D_ID", hidden: true },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "SCM_PURC_REQ_D_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Description of Goods", width: "15%" },
                { field: "BRAND_NAME_EN", title: "Brand", width: "8%" },
                { field: "PO_QTY", title: "Quantity", width: "6%" },
                { field: "UNIT_PRICE", title: "Unit Price(US$)", width: "7%" },
                { field: "TTL_VALUE", title: "Total Price(US$)", width: "8%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a title="Edit" ng-click="vm.editData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
        };



        function getReqMasterList() {
            return vm.ReqMasterList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/Purchase/PurchaseReq/GetPendingReqListForGPO?pSCM_PURC_REQ_H_ID=' + $stateParams.pSCM_PURC_REQ_H_ID + '&pLK_PURC_PROD_GRP_ID=' + vm.form.LK_PURC_PROD_GRP_ID).then(function (res) {
                            vm.form.SCM_SUPPLIER_ID = res.length > 0 ? res[0].SCM_SUPPLIER_ID : "";
                            console.log(res);
                            e.success(res);
                        });
                    }
                },
                pageSize: 15
            });
        };

        function GetShipModeList() {
            return vm.shipModeList = {
                optionLabel: "-- Select Ship Mode --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetShipMode').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "SHIP_MODE_NAME_EN",
                dataValueField: "RF_SHIP_MODE_ID"
            };
        };


        function GetStoreList() {
            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        };

        function GetSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "--Source--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.LookupListData(84).then(function (res) {
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
                            PurchaseDataService.getDataByFullUrl('/api/common/GetPayMethod').then(function (res) {
                                //PurchaseDataService.getDataByFullUrl('api/common/GetPayMethod').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_MTHD_NAME",
                dataValueField: "RF_PAY_MTHD_ID"
            };
        };


        function GetPaymentTermList() {
            return vm.paymentTermList = {
                optionLabel: "-- Select Payment Term --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetPayTerm').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAYM_TERM_NAME_EN",
                dataValueField: "RF_PAYM_TERM_ID"
            };
        }


        function GetIncoTermList() {
            return vm.incoTermList = {
                optionLabel: "- Inco Term -",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetIncoTerm').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "INCO_TERM_NAME_EN",
                dataValueField: "RF_INCO_TERM_ID"
            };
        }


        function GetDeliveryPlaceList() {
            return vm.deliveryPlaceList = {
                optionLabel: "Place of Delivery",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetDeliveryPlace').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DELV_PLC_NAME_EN",
                dataValueField: "RF_DELV_PLC_ID"
            };
        }



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
                            PurchaseDataService.getUserDatalist().then(function (res) {
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

        $scope.PO_DT_IMP_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PO_DT_IMP_LNopened = true;
        }

        $scope.REVISION_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REVISION_DT_LNopened = true;
        }

        $scope.DELV_TGT_DT_LNopened = [false];

        $scope.DELV_TGT_DT_LNopen = function ($event, idx) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.DELV_TGT_DT_LNopened[idx] = true;
        }


        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    PurchaseDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        e.success(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.loadPendingRequest = function (e) {
            var item = e.sender.dataItem(e.item);
            if (item.IS_LOCAL == 'L')
                vm.form.LK_LOC_SRC_TYPE_ID = 452;
            else if (item.IS_LOCAL == 'F')
                vm.form.LK_LOC_SRC_TYPE_ID = 453;
            else
                vm.form.LK_LOC_SRC_TYPE_ID = '';

            vm.form.CP_NAME_EN = item.CP_NAME_EN;

            PurchaseDataService.getDataByFullUrl('/api/knit/KnitYarnReq/PendingRequisitionBySupplierID?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID).then(function (res) {
                vm.RequisitionItemList = res;
            });

        }

        vm.SelectAll = function (IS_SELECT) {
            //alert(IS_SELECT);
            if (IS_SELECT) {
                for (var i = 0; i < vm.RequisitionItemList.length; i++)
                    vm.RequisitionItemList[i].IS_SELECT = true;
            }
            else {
                for (var i = 0; i < vm.RequisitionItemList.length; i++)
                    vm.RequisitionItemList[i].IS_SELECT = false;
            }
        }

        function RequisitionItemList() {



        };

        function GetItemCategoryList() {

            PurchaseDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var filter = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 2) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: filter
                });
            });

            PurchaseDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (vm.form.INV_ITEM_CAT_ID || 2)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            PurchaseDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 2)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        function GetPriorityList() {

            return vm.PriorityList = {
                optionLabel: "-- Select Priority --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.LookupListData(63).then(function (res) {
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
                            PurchaseDataService.getDataByFullUrl('/api/common/GetPayMethod').then(function (res) {
                                //PurchaseDataService.getDataByFullUrl('api/common/GetPayMethod').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_MTHD_NAME",
                dataValueField: "RF_PAY_MTHD_ID"
            };
        };

        //function GetSourceTypeList() {

        //    return vm.sourceTypeList = {
        //        optionLabel: "-- Select Source Type --",
        //        filter: "contains",
        //        autoBind: true,
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    PurchaseDataService.getDataByFullUrl('/api/common/GetReqSRC').then(function (res) {
        //                        //PurchaseDataService.LookupListData(92).then(function (res) {
        //                        e.success(res);
        //                    });
        //                }
        //            }
        //        },
        //        dataTextField: "REQ_SRC_NAME",
        //        dataValueField: "RF_REQ_SRC_ID"
        //    };
        //};

        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                //PurchaseDataService.LookupListData(88).then(function (res) {
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
                optionLabel: "-- Select Req Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/common/GetReqSRC').then(function (res) {
                                //PurchaseDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_SRC_NAME",
                dataValueField: "RF_REQ_SRC_ID"
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
                            PurchaseDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

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
                        PurchaseDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {

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
                        PurchaseDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetYarnColorGroupList() {
            return vm.yarnColorGroupList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.LookupListData(93).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetSupplierList() {
            if (vm.form.CM_IMP_PO_H_ID > 0) {

                return vm.poSupplierList = {
                    optionLabel: "Supplier",
                    filter: "contains",
                    autoBind: true,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                PurchaseDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                                    e.success(res);
                                    if (supID > 0)
                                        vm.form.SCM_SUPPLIER_ID = supID;
                                });
                            }
                        }
                    },
                    dataTextField: "SUP_TRD_NAME_EN",
                    dataValueField: "SCM_SUPPLIER_ID"
                };

            } else {
                return vm.supplierList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                                e.success(res);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                });
            }

        };

        vm.loadAllsupplier = function () {

            return vm.supplierList = {
                optionLabel: "Supplier",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID"
            };
        }

        function getTnaStatusList() {
            return vm.TnaStatusList = {
                optionLabel: "-- Select TNA Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectApprovRejectStatus?pMC_TNA_TASK_ID=46').then(function (res) {
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
                            PurchaseDataService.getDataByFullUrl('/hr/hrleavetrans/FiscalYearDataAll').then(function (res) {
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

        function convertDate(old) {
            var _isudate = $filter('date')(old, 'yyyy-MM-ddTHH:mm:ss');
            return _isudate;
        }

        function convertDate(old) {
            var _isudate = $filter('date')(old, 'yyyy-MM-ddTHH:mm:ss');
            return _isudate;
        }

        vm.printPO = function (dataItem) {

            if ($stateParams.pSCM_PURC_REQ_H_ID > 0) {

                vm.form["SCM_PURC_REQ_H_ID"] = $stateParams.pSCM_PURC_REQ_H_ID;
                vm.form["CM_IMP_PO_H_ID"] = $stateParams.pCM_IMP_PO_H_ID;

                vm.form.REPORT_CODE = 'RPT-2012';

                var form = document.createElement("form");
                form.setAttribute("method", "post");
                form.setAttribute("action", '/api/mrc/MrcReport/PreviewReportRDLC');
                form.setAttribute("target", '_blank');

            }
            else {
                vm.form.REPORT_CODE = 'RPT-8000';

                vm.form.CM_IMP_PO_H_ID = dataItem.CM_IMP_PO_H_ID;

                var form = document.createElement("form");
                form.setAttribute("method", "post");
                form.setAttribute("action", '/api/Pur/PurReport/PreviewReportRDLC');
                form.setAttribute("target", '_blank');
            }
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
        };

        vm.submitAll = function (dataOri, id) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var _isudate = $filter('date')(data.PO_DT_IMP, 'yyyy-MM-ddTHH:mm:ss');
                data["PO_DT_IMP"] = _isudate;

                data["HR_COMPANY_ID"] = data.HR_COMPANY_ID;
                data["IS_LOCAL_CASH_P"] = "Y";
                data["IS_FINALIZED"] = id;
                data["CASH_SUPL_NAME"] = data.CASH_SUPL_NAME;
                data["LK_LOC_SRC_TYPE_ID"] = data.LK_LOC_SRC_TYPE_ID;
                data["RF_PAY_MTHD_ID"] = data.RF_PAY_MTHD_ID;
                data["DELV_STORE_ID"] = data.DELV_STORE_ID;
                data["BILL_TO_COMP_ID"] = data.BILL_TO_COMP_ID;
                data["RF_INCO_TERM_ID"] = data.RF_INCO_TERM_ID;
                data["RF_DELV_PLC_ID"] = data.RF_DELV_PLC_ID;
                data["RF_PAYM_TERM_ID"] = data.RF_PAYM_TERM_ID;
                data["RF_SHIP_MODE_ID"] = (data.RF_SHIP_MODE_ID || 1);
                data["RF_CURRENCY_ID"] = data.RF_CURRENCY_ID;
                data["TERMS_DESC"] = data.TERMS_DESC;
                data["REMARKS"] = data.REMARKS;
                data["LK_PO_STATUS_ID"] = (data.LK_PO_STATUS_ID || 1);
                data["LK_PURC_PROD_GRP_ID"] = (data.LK_PURC_PROD_GRP_ID || 791);

                //var list = _.filter(vm.RequisitionItemList, function (x) { return x.IS_SELECT == true })

                data["PO_D_LIST"] = vm.importOrderList.data().map(function (o) {
                    return {
                        CM_IMP_PO_D_ID: o.CM_IMP_PO_D_ID == null ? 0 : o.CM_IMP_PO_D_ID,
                        SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID == null ? 0 : o.SCM_PURC_REQ_D_ID,
                        MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID == null ? 0 : o.MC_ORD_TRMS_ITEM_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        RF_BRAND_ID: 1,
                        LK_YRN_COLR_GRP_ID: 1,
                        PO_QTY: o.PO_QTY == null ? o.RQD_QTY : o.PO_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID,
                        UNIT_PRICE: o.UNIT_PRICE,
                        DELV_TGT_DT: convertDate(o.DELV_TGT_DT)
                    }
                });

                return PurchaseDataService.saveDataByFullUrl(data, '/api/knit/KnitYarnReq/SavePO').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        config.appToastMsg(res.data.PMSG);
                        if (res.data.OPCM_IMP_PO_H_ID)
                            $state.go($state.current, { pCM_IMP_PO_H_ID: res.data.OPCM_IMP_PO_H_ID }, { reload: true });

                    }
                });
            }
        };


        vm.reviseAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var _isudate = $filter('date')(data.PO_DT_IMP, 'yyyy-MM-ddTHH:mm:ss');
                data["PO_DT_IMP"] = _isudate;

                var _rdate = $filter('date')(data.REVISION_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["REVISION_DT"] = _rdate;

                data["HR_COMPANY_ID"] = data.HR_COMPANY_ID;
                data["IS_LOCAL_CASH_P"] = "N";
                data["IS_FINALIZED"] = "N";
                data["CASH_SUPL_NAME"] = data.CASH_SUPL_NAME;
                data["LK_LOC_SRC_TYPE_ID"] = data.LK_LOC_SRC_TYPE_ID;
                data["RF_PAY_MTHD_ID"] = data.RF_PAY_MTHD_ID;
                data["DELV_STORE_ID"] = data.DELV_STORE_ID;
                data["BILL_TO_COMP_ID"] = data.BILL_TO_COMP_ID;
                data["RF_INCO_TERM_ID"] = data.RF_INCO_TERM_ID;
                data["RF_DELV_PLC_ID"] = data.RF_DELV_PLC_ID;
                data["RF_PAYM_TERM_ID"] = data.RF_PAYM_TERM_ID;
                data["RF_SHIP_MODE_ID"] = (data.RF_SHIP_MODE_ID || 1);
                data["RF_CURRENCY_ID"] = data.RF_CURRENCY_ID;
                data["TERMS_DESC"] = data.TERMS_DESC;
                data["REMARKS"] = data.REMARKS;
                data["LK_PO_STATUS_ID"] = (data.LK_PO_STATUS_ID || 1);

                var list = _.filter(vm.RequisitionItemList, function (x) { return x.IS_SELECT == true })
                data["PO_D_LIST"] = vm.importOrderList.data().map(function (o) {
                    return {
                        CM_IMP_PO_D_ID: o.CM_IMP_PO_D_ID == null ? 0 : o.CM_IMP_PO_D_ID,
                        SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID == null ? 0 : o.SCM_PURC_REQ_D_ID,
                        MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID == null ? 0 : o.MC_ORD_TRMS_ITEM_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        RF_BRAND_ID: 1,
                        LK_YRN_COLR_GRP_ID: 1,
                        PO_QTY: o.PO_QTY == null ? o.RQD_QTY : o.PO_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID,
                        UNIT_PRICE: o.UNIT_PRICE,
                        DELV_TGT_DT: convertDate(o.DELV_TGT_DT)
                    }
                });


                return PurchaseDataService.saveDataByFullUrl(data, '/api/knit/KnitYarnReq/RevisePO').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        config.appToastMsg(res.data.PMSG);
                        if (res.data.OPCM_IMP_PO_H_ID)
                            $state.go($state.current, { pCM_IMP_PO_H_ID: res.data.OPCM_IMP_PO_H_ID }, { reload: true });

                    }
                });
            }
        };


    }

}
)();