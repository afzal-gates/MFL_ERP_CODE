(function () {
    'use strict';
    angular.module('multitex.cmr').controller('ImpLcPOPIReviseController', ['$q', 'config', 'CmrDataService', 'commonDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', '$modal', 'formData', '$http', ImpLcPOPIReviseController]);
    function ImpLcPOPIReviseController($q, config, CmrDataService, commonDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog, $modal, formData, $http) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.EDIT = 0;
        vm.form = formData.CM_IMP_PI_H_ID ? formData : { CM_IMP_PI_H_ID: 0, DOC_TITLE: 'Import PI No', IS_REVISE: false, items: [] };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getPOMasterList(), loadAllSupplier(), GetCompanyList(), getBuyerListData(), GetCountryList(), GetPaymentTermList(),
                            GetDeliveryPlaceList(), GetIncoTermList(), GetPortList(), getLcStatusList(), GetStatusList(),
                            GetLcTypeList(), GetShipModeList(),getPIDetailsList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getPIDetailsList(){
           
            CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/GetPIDocDtlInfo?pCM_IMP_PI_H_ID=' + (vm.form.CM_IMP_PI_H_ID || 0)).then(function (res) {
                vm.form.items = res;
            });
        }
        

        if ($stateParams.pREVISE > 0) {
            vm.form["IS_REVISE"] = true;
            vm.form.REVISION_NO = formData.REVISION_NO + 1;
            vm.form.REVISION_DT = '';
        }
        else {
            vm.form["IS_REVISE"] = false;
        }

        vm.attachFile = [{ FILE_NAME: '', FILES_0: null }];

        vm.add = function (data, key) {

            var getModelAsFormData = function (data) {
                var dataAsFormData = new FormData();
                angular.forEach(data, function (value, key) {
                    dataAsFormData.append(key, value);
                });
                return dataAsFormData;
            };
            angular.forEach(data.items, function (val, k) {
                console.log(val);
                $http({
                    method: 'post',
                    url: '/Commercial/Cmr/uploadPIDocs',
                    data: getModelAsFormData(val),
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined, "RequestVerificationToken": key }
                }).success(function (data, status, headers, config1) {
                    console.log(status);
                }).
                  error(function (data, status, headers, config) {
                      console.log(status);
                  });
            });
            
        }



        vm.addToDocGrid = function (data) {
            data['RPT_PATH_URL'] = new Date().getTime() + getExtention(data.ATT_FILE.name);
            vm.form.items.push(data);
            if (vm.slFileAreaShow && vm.form.items.length > 0) {
                vm.disableSubmit = false;
            } else {
                vm.disableSubmit = true;
            }
            vm.file = {};
        }


        function getExtention(fileName) {
            var i = fileName.lastIndexOf('.');
            if (i === -1) return false;
            return fileName.slice(i)
        }


        vm.removeAddedDoc = function (index) {
            if (vm.form.items[index].HR_SL_DOCS_ID > 0) {
                vm.form.items[index].REMOVE = 'Y';
            } else {
                vm.form.items.splice(index, 1);
            }
            
            if (vm.slFileAreaShow && vm.form.items.length > 0) {
                vm.disableSubmit = false;
            } else {
                vm.disableSubmit = true;
            }

        }

        vm.SelectAll = function (IS_SELECT) {
            //alert(IS_SELECT);
            if (IS_SELECT) {
                for (var i = 0; i < vm.PIItemList.length; i++)
                    vm.PIItemList[i].IS_SELECT = true;
            }
            else {
                for (var i = 0; i < vm.PIItemList.length; i++)
                    vm.PIItemList[i].IS_SELECT = false;
            }
        }

        vm.onChangePO = function (obj) {
            var item = angular.copy(obj);

            CmrDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetPoDetlByID?pCM_IMP_PO_H_ID=' + item.CM_IMP_PO_H_ID).then(function (res) {
                vm.PIItemList = res;
            });

            vm.form = item;
            vm.form.CM_IMP_PI_H_ID = $stateParams.pCM_IMP_PI_H_ID
            vm.form["items"] = [];
            if ($stateParams.pREVISE > 0) {
                vm.form["IS_REVISE"] = true;
                vm.form.REVISION_NO = vm.form.REVISION_NO + 1;
                vm.form.REVISION_DT = '';
            }
            else {
                vm.form["IS_REVISE"] = false;
            }
            //vm.loadAllsupplier();
        }


        function loadAllSupplier() {

            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/purchase/SupplierProfile/SelectAllByCat/2').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }

        vm.loadPendingRequest = function (e) {
            var item = e.sender.dataItem(e.item);
            if (item.SCM_SUPPLIER_ID > 0) {



                if (vm.importOrderList.data().length > 0) {
                    Dialog.confirm('Are you sure that you want to work with new supplier! there is some previous supplier data exists into list.', 'Attention', ['Yes', 'No'])
                        .then(function () {
                            vm.importOrderList = new kendo.data.DataSource({
                                data: []
                            });

                            vm.PIItemList = [];
                            vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;

                            CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/GetPOBySupplier?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0)).then(function (res) {
                                vm.PoMasterList = new kendo.data.DataSource({
                                    data: res
                                });
                            });
                            return;
                        });
                    vm.form.SCM_SUPPLIER_ID = vm.PoMasterList.data()[0].SCM_SUPPLIER_ID;
                }
                else {
                    CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/GetPOBySupplier?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0)).then(function (res) {
                        vm.PoMasterList = new kendo.data.DataSource({
                            data: res
                        });
                    });
                }
            }
        }

        function getPOMasterList() {
            return vm.PoMasterList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/GetPOBySupplier?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 10,
            });
        };


        if (formData) {
            formData["REVISION_DT"] = new Date();

            vm.importOrderList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/GetPIDtlInfo?pCM_IMP_PI_H_ID=' + (formData.CM_IMP_PI_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
            
            vm.PoFirstist = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/GetPIDtlInfo?pCM_IMP_PI_H_ID=' + (formData.CM_IMP_PI_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

            if (formData.IS_SC_OR_LC == "L")
                vm.form["DOC_TITLE"] = "Export LC No";
            else
                vm.form["DOC_TITLE"] = "Sales Contact No";

        }

        vm.CalculateDiscount = function () {
            var PI = 0.00;
            if (vm.form.PO_AMT > vm.form.DISC_AMT) {
                if (vm.form.IS_DISC_P_A == "P") {
                    PI = ((vm.form.PO_AMT / 100) * vm.form.DISC_AMT).toFixed(2);
                    vm.form.PI_NET_AMT = (parseFloat(vm.form.PO_AMT) - parseFloat(PI));
                    vm.form.PI_NET_AMT = vm.form.PI_NET_AMT.toFixed(2)
                } else {
                    vm.form.PI_NET_AMT = vm.form.PO_AMT - vm.form.DISC_AMT;
                }
            }
        }

        $scope.PI_DT_IMP_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PI_DT_IMP_LNopened = true;
        }

        $scope.REVISION_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REVISION_DT_LNopened = true;
        }


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 22;
            var PARENT_ID = 0;
            if ($stateParams.pREVISE > 0)
                vm.form.RF_ACTN_STATUS_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            CmrDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "SR" })
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
                            CmrDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                var lst = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "SR" })
                                if (lst.length == 1) {
                                    vm.form.RF_ACTN_VIEW = 0;
                                    vm.form.RF_ACTN_STATUS_ID = lst[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = lst[0].ACTN_STATUS_NAME;
                                    //alert(vm.form.ACTN_STATUS_NAME);
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


        function getCurrencyList() {

            return vm.currencyList = {
                optionLabel: "--Select Currency--",
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
                            CmrDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };


        function getBuyerListData() {


        };

        vm.getBuyerNotifyPartyList = function (e) {
            var obj = e.sender.dataItem(e.item);
            if (vm.form.MC_BUYER_ID > 0) {
                vm.form.ADDRESS_PI = obj.ADDRESS_PI;
                vm.notifyPartyList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/cmr/BuyerNotifyParty/GetBuyerNotifyPartyInfo?pMC_BUYER_ID=' + (vm.form.MC_BUYER_ID || 1)).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });

                vm.styleList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/mrc/StyleH/BuyerWiseStyleListData/' + (vm.form.MC_BUYER_ID || 1)).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }
        }

        vm.clearGrid = function () {
            vm.EDIT = 0;
            vm.PIItemList = []
        }

        vm.AddToGrid = function () {

            for (var i = 0; i < vm.PIItemList.length; i++) {

                var idx = vm.importOrderList.data().length + 1;

                var obj = vm.PIItemList[i];
                if (obj.IS_SELECT == true) {

                    var lst = _.filter(vm.importOrderList.data(), function (x) { return x.CM_IMP_PO_D_YRN_ID == obj.CM_IMP_PO_D_YRN_ID })
                    if (lst.length == 0) {
                        obj["TTL_VALUE"] = obj.CONF_QTY * obj.CONF_RATE;
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
                ttl_po = ttl_po + (obj.CONF_QTY * obj.CONF_RATE);
            }
            vm.form.PI_NET_AMT = ttl_po;

            //vm.CalculateDiscount();
        }


        vm.updateToGrid = function () {
            
            for (var i = 0; i < vm.PIItemList.length; i++) {

                var obj = vm.PIItemList[i];
                var row = vm.importOrderList.getByUid(obj.uid);
                //var row = _.filter(vm.importOrderList.data(), function (x) { return x.uid == obj.uid });
                row["CM_IMP_PI_D_YRN_ID"] = obj.CM_IMP_PI_D_YRN_ID;
                row["CM_IMP_PI_H_ID"] = obj.CM_IMP_PI_H_ID;
                row["CM_IMP_PO_D_YRN_ID"] = obj.CM_IMP_PO_D_YRN_ID;
                row["INV_ITEM_ID"] = obj.INV_ITEM_ID;
                row["LK_YRN_COLR_GRP_ID"] = obj.LK_YRN_COLR_GRP_ID;
                row["RF_BRAND_ID"] = obj.RF_BRAND_ID;
                row["QTY_MOU_ID"] = obj.QTY_MOU_ID;
                
                row["ITEM_NAME_EN"] = obj.ITEM_NAME_EN;
                row["BRAND_NAME_EN"] = obj.BRAND_NAME_EN;
                row["LOT_REF_NO"] = obj.LOT_REF_NO;
                row["HS_CODE"] = obj.HS_CODE;
                row["CONF_QTY"] = obj.CONF_QTY;
                row["CONF_RATE"] = obj.CONF_RATE;
                row["PURC_REQ_NO"] = obj.ORDER_DTL;
                row["QTY_MOU_ID"] = obj.QTY_MOU_ID;
                row["LK_YRN_COLR_GRP_NAME"] = obj.LK_YRN_COLR_GRP_NAME;
                var total = obj.CONF_QTY * obj.CONF_RATE;
                row["TTL_VALUE"] = total.toFixed(2);
            }

            var ttl_po = 0;
            for (var i = 0; i < vm.importOrderList.data().length; i++) {
                var obj = vm.importOrderList.data()[i];
                ttl_po = ttl_po + (obj.CONF_QTY * obj.CONF_RATE);
            }
            vm.form.PI_NET_AMT = ttl_po;

            vm.EDIT = 0;
            vm.PIItemList = [];
            //vm.CalculateDiscount();
        }
        

        
        vm.AddToGridPO = function () {

            for (var i = 0; i < vm.POItemList.length; i++) {

                var idx = vm.importOrderList.data().length + 1;

                var obj = vm.POItemList[i];
                if (obj.IS_SELECT == true) {

                    var lst = _.filter(vm.importOrderList.data(), function (x) { return x.CM_IMP_PO_D_YRN_ID == obj.CM_IMP_PO_D_YRN_ID })
                    if (lst.length == 0) {
                        obj["TTL_VALUE"] = obj.CONF_QTY * obj.CONF_RATE;
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
            for (var i = 0; i < vm.PoFirstist.data().length; i++) {
                var obj = vm.PoFirstist.data()[i];
                ttl_po = ttl_po + (obj.CONF_QTY * obj.CONF_RATE);
            }
            vm.form.PI_NET_AMT = ttl_po;

            //vm.CalculateDiscount();
        }


        vm.updateToGridPO = function () {
            
            for (var i = 0; i < vm.POItemList.length; i++) {

                var obj = vm.POItemList[i];
                var row = vm.PoFirstist.getByUid(obj.uid);
                //var row = _.filter(vm.importOrderList.data(), function (x) { return x.uid == obj.uid });
                row["CM_IMP_PI_D_YRN_ID"] = obj.CM_IMP_PI_D_YRN_ID;
                row["CM_IMP_PI_H_ID"] = obj.CM_IMP_PI_H_ID;
                row["CM_IMP_PO_D_YRN_ID"] = obj.CM_IMP_PO_D_YRN_ID;
                row["INV_ITEM_ID"] = obj.INV_ITEM_ID;
                row["LK_YRN_COLR_GRP_ID"] = obj.LK_YRN_COLR_GRP_ID;
                row["RF_BRAND_ID"] = obj.RF_BRAND_ID;
                row["QTY_MOU_ID"] = obj.QTY_MOU_ID;
                
                row["ITEM_NAME_EN"] = obj.ITEM_NAME_EN;
                row["BRAND_NAME_EN"] = obj.BRAND_NAME_EN;
                row["LOT_REF_NO"] = obj.LOT_REF_NO;
                row["HS_CODE"] = obj.HS_CODE;
                row["CONF_QTY"] = obj.CONF_QTY;
                row["CONF_RATE"] = obj.CONF_RATE;
                row["PURC_REQ_NO"] = obj.ORDER_DTL;
                row["QTY_MOU_ID"] = obj.QTY_MOU_ID;
                row["LK_YRN_COLR_GRP_NAME"] = obj.LK_YRN_COLR_GRP_NAME;
                var total = obj.CONF_QTY * obj.CONF_RATE;
                row["TTL_VALUE"] = total.toFixed(2);
            }

            var ttl_po = 0;
            for (var i = 0; i < vm.PoFirstist.data().length; i++) {
                var obj = vm.PoFirstist.data()[i];
                ttl_po = ttl_po + (obj.CONF_QTY * obj.CONF_RATE);
            }
            vm.form.PI_NET_AMT = ttl_po;

            vm.EDIT = 0;
            vm.POItemList = [];
            //vm.CalculateDiscount();
        }

        function GetShipModeList() {
            return vm.shipModeList = {
                optionLabel: "-- Select Ship Mode --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetShipMode').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "SHIP_MODE_NAME_EN",
                dataValueField: "RF_SHIP_MODE_ID"
            };
        };

        function GetLcTypeList() {
            return vm.lcTypeList = {
                optionLabel: "-- Select LC Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetLCType').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LC_TYPE_NAME_EN",
                dataValueField: "RF_LC_TYPE_ID"
            };
        };

        function GetPortList() {
            return vm.portList = {
                optionLabel: "-- Select Port --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetTransitPort').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "TRAN_PORT_NAME_EN",
                dataValueField: "RF_TRAN_PORT_ID"
            };
        };


        function GetCountryList() {
            return vm.CountryList = {
                optionLabel: "-- Select Country --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/mrc/buyer/GetCountryList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
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
                            CmrDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetPayTerm').then(function (res) {
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
                            CmrDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetIncoTerm').then(function (res) {
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
                optionLabel: "-- Select Delivery Place --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetDeliveryPlace').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DELV_PLC_NAME_EN",
                dataValueField: "RF_DELV_PLC_ID"
            };
        }


        function getLcStatusList() {
            return vm.lcStatusList = {
                optionLabel: "-- LC Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CmrDataService.LookupListData(117).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        vm.DocTitle = function () {
            if (vm.form.IS_SC_OR_LC == "S")
                vm.form.DOC_TITLE = "Sales Contact No";
            else
                vm.form.DOC_TITLE = "Export LC No";

        }

        vm.clearAll = function (data) {

            vm.EDIT = 0;
            vm.PIItemList = []
        }


        vm.editData = function (dataItem) {
            var item = angular.copy(dataItem);
            item["IS_SELECT"] = true;
            vm.EDIT = 1;
            vm.PIItemList = [item];
        }

        vm.removeData = function (data) {

            Dialog.confirm('Finalizing "' + data.ORDER_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var List = _.filter(vm.importOrderList.data(), function (x) { return x.MC_ORDER_H_ID == data.MC_ORDER_H_ID });

                     for (var i = 0; i < List.length; i++) {
                         var item = List[i];
                         vm.importOrderList.remove(item);
                     }

                 });
        }

        
        vm.editPOData = function (dataItem) {
            var item = angular.copy(dataItem);
            item["IS_SELECT"] = true;
            vm.EDIT = 1;
            vm.POItemList = [item];
        }

        vm.removePOData = function (data) {

            Dialog.confirm('Finalizing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var List = _.filter(vm.PoFirstist.data(), function (x) { return x.CM_IMP_PI_D_YRN_ID == data.CM_IMP_PI_D_YRN_ID });

                     for (var i = 0; i < List.length; i++) {
                         var item = List[i];
                         vm.PoFirstist.remove(item);
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

                { field: "CM_IMP_PI_D_YRN_ID", hidden: true },
                { field: "CM_IMP_PI_H_ID", hidden: true },
                { field: "CM_IMP_PO_D_YRN_ID", hidden: true },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "LK_YRN_COLR_GRP_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                { field: "PURC_REQ_NO", title: "Requisition No", width: "12%" },
                { field: "ORDER_DTL", title: "Order Info", width: "15%" },
                { field: "ITEM_NAME_EN", title: "Description of Goods", width: "15%" },
                { field: "LK_YRN_COLR_GRP_NAME", title: "Color Group", width: "8%" },
                { field: "BRAND_NAME_EN", title: "Brand", width: "8%" },
                { field: "LOT_REF_NO", title: "lot Ref. #", width: "5%" },
                { field: "HS_CODE", title: "HS Code", width: "7%" },
                { field: "CONF_QTY", title: "Quantity", width: "6%" },
                { field: "CONF_RATE", title: "Unit Price(US$)", width: "7%" },
                { field: "TTL_VALUE", title: "Total Price(US$)", width: "8%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a title="Edit" ng-click="vm.editData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
        };

        
        vm.gridOptionsPO = {
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

                { field: "CM_IMP_PI_D_YRN_ID", hidden: true },
                { field: "CM_IMP_PI_H_ID", hidden: true },
                { field: "CM_IMP_PO_D_YRN_ID", hidden: true },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "LK_YRN_COLR_GRP_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                { field: "PURC_REQ_NO", title: "Requisition No", width: "12%" },
                { field: "ORDER_DTL", title: "Order Info", width: "15%" },
                { field: "ITEM_NAME_EN", title: "Description of Goods", width: "15%" },
                { field: "LK_YRN_COLR_GRP_NAME", title: "Color Group", width: "8%" },
                { field: "BRAND_NAME_EN", title: "Brand", width: "8%" },
                { field: "LOT_REF_NO", title: "lot Ref. #", width: "5%" },
                { field: "HS_CODE", title: "HS Code", width: "7%" },
                { field: "CONF_QTY", title: "Quantity", width: "6%" },
                { field: "CONF_RATE", title: "Unit Price(US$)", width: "7%" },
                { field: "TTL_VALUE", title: "Total Price(US$)", width: "8%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removePOData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a title="Edit" ng-click="vm.editPOData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
        };

        vm.revisePO = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                var LC_VAL = 0;

                data["XML_PI"] = CmrDataService.xmlStringShort(vm.PoFirstist.data().map(function (o) {
                    return {
                        CM_IMP_PI_D_YRN_ID: o.CM_IMP_PI_D_YRN_ID == null ? 0 : o.CM_IMP_PI_D_YRN_ID,
                        CM_IMP_PI_H_ID: o.CM_IMP_PI_H_ID == null ? 0 : o.CM_IMP_PI_H_ID,
                        CM_IMP_PO_D_YRN_ID: o.CM_IMP_PO_D_YRN_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID == null ? 0 : o.INV_ITEM_ID,
                        LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID == null ? 0 : o.LK_YRN_COLR_GRP_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID == null ? '' : o.RF_BRAND_ID,
                        HS_CODE: o.HS_CODE == null ? 0 : o.HS_CODE,
                        PI_QTY: o.CONF_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID == 0 ? 3 : o.QTY_MOU_ID
                    }
                }));
                var id = vm.form.CM_IMP_PI_H_ID;

                return CmrDataService.saveDataByUrl(data, '/ImportLCPI/RevisePO').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                        }
                        vm.form.CM_IMP_PI_H_ID = id;
                        $state.go($state.current, { pCM_IMP_PI_H_ID: id }, { inherit: false, reload: true })
                    }
                });
            }
        };

        vm.submitAll = function (data, key) {

            if (fnValidate() == true) {

                var getModelAsFormData = function (data) {
                    var dataAsFormData = new FormData();
                    angular.forEach(data, function (value, key) {
                        dataAsFormData.append(key, value);
                    });
                    return dataAsFormData;
                };
                angular.forEach(data.items, function (val, k) {
                    //console.log(val);
                    $http({
                        method: 'post',
                        url: '/Commercial/Cmr/uploadPIDocs',
                        data: getModelAsFormData(val),
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined, "RequestVerificationToken": key }
                    }).success(function (data, status, headers, config1) {
                        console.log(status);
                    }).
                      error(function (data, status, headers, config) {
                          console.log(status);
                      });
                });


                var dataOri = angular.copy(data);
                console.log(dataOri.items);
                dataOri["IS_UPDATE"] = 'N';
                dataOri["XML_PI"] = CmrDataService.xmlStringShort(vm.importOrderList.data().map(function (o) {
                    return {
                        CM_IMP_PI_D_YRN_ID: o.CM_IMP_PI_D_YRN_ID == null ? 0 : o.CM_IMP_PI_D_YRN_ID,
                        CM_IMP_PI_H_ID: o.CM_IMP_PI_H_ID == null ? 0 : o.CM_IMP_PI_H_ID,
                        CM_IMP_PO_D_YRN_ID: o.CM_IMP_PO_D_YRN_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID == null ? 0 : o.INV_ITEM_ID,
                        LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID == null ? 0 : o.LK_YRN_COLR_GRP_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID == null ? '' : o.RF_BRAND_ID,
                        HS_CODE: o.HS_CODE == null ? 0 : o.HS_CODE,
                        PI_QTY: o.CONF_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID == 0 ? 3 : o.QTY_MOU_ID
                    }
                }));
                
                dataOri["XML_PI_D"] = CmrDataService.xmlStringShort(data.items.map(function (o) {
                    return {
                        CM_IMP_PI_DOC_ID: o.CM_IMP_PI_DOC_ID == null ? 0 : o.CM_IMP_PI_DOC_ID,
                        CM_IMP_PI_H_ID: o.CM_IMP_PI_H_ID == null ? 0 : o.CM_IMP_PI_H_ID,
                        PI_NO_IMP: o.PI_NO_IMP,
                        REVISION_NO: o.REVISION_NO == null ? 0 : o.REVISION_NO,
                        RPT_PATH_URL: o.RPT_PATH_URL,
                    }
                }));

                var id = vm.form.CM_IMP_PI_H_ID;

                return CmrDataService.saveDataByUrl(dataOri, '/ImportLCPI/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {                        
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                        }
                        vm.form.CM_IMP_PI_H_ID = res.data.OPCM_IMP_PI_H_ID;
                        $state.go($state.current, { pCM_EXP_PI_H_ID: res.data.OPCM_IMP_PI_H_ID, pREVISE: 0 }, { inherit: false, reload: true })
                    }
                });
            }
        };



        vm.reviseAll = function (data, key) {

            if (fnValidate() == true) {

                var getModelAsFormData = function (data) {
                    var dataAsFormData = new FormData();
                    angular.forEach(data, function (value, key) {
                        dataAsFormData.append(key, value);
                    });
                    return dataAsFormData;
                };
                angular.forEach(data.items, function (val, k) {
                    //console.log(val);
                    $http({
                        method: 'post',
                        url: '/Commercial/Cmr/uploadPIDocs',
                        data: getModelAsFormData(val),
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined, "RequestVerificationToken": key }
                    }).success(function (data, status, headers, config1) {
                        console.log(status);
                    }).
                      error(function (data, status, headers, config) {
                          console.log(status);
                      });
                });


                var dataOri = angular.copy(data);
                console.log(dataOri.items);
                dataOri["IS_UPDATE"] = 'N';
                dataOri["XML_PI"] = CmrDataService.xmlStringShort(vm.importOrderList.data().map(function (o) {
                    return {
                        CM_IMP_PI_D_YRN_ID: o.CM_IMP_PI_D_YRN_ID == null ? 0 : o.CM_IMP_PI_D_YRN_ID,
                        CM_IMP_PI_H_ID: o.CM_IMP_PI_H_ID == null ? 0 : o.CM_IMP_PI_H_ID,
                        CM_IMP_PO_D_YRN_ID: o.CM_IMP_PO_D_YRN_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID == null ? 0 : o.INV_ITEM_ID,
                        LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID == null ? 0 : o.LK_YRN_COLR_GRP_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID == null ? '' : o.RF_BRAND_ID,
                        HS_CODE: o.HS_CODE == null ? 0 : o.HS_CODE,
                        PI_QTY: o.CONF_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID == 0 ? 3 : o.QTY_MOU_ID
                    }
                }));
                
                dataOri["XML_PI_D"] = CmrDataService.xmlStringShort(data.items.map(function (o) {
                    return {
                        CM_IMP_PI_DOC_ID: o.CM_IMP_PI_DOC_ID == null ? 0 : o.CM_IMP_PI_DOC_ID,
                        CM_IMP_PI_H_ID: o.CM_IMP_PI_H_ID == null ? 0 : o.CM_IMP_PI_H_ID,
                        PI_NO_IMP: o.PI_NO_IMP,
                        REVISION_NO: o.REVISION_NO == null ? 0 : o.REVISION_NO,
                        RPT_PATH_URL: o.RPT_PATH_URL,
                    }
                }));

                var id = vm.form.CM_IMP_PI_H_ID;

                return CmrDataService.saveDataByUrl(dataOri, '/ImportLCPI/Update').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {                        
                        res['data'] = angular.fromJson(res.jsonStr);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                        }
                        vm.form.CM_IMP_PI_H_ID = res.data.OPCM_IMP_PI_H_ID;
                        $state.go($state.current, { pCM_EXP_PI_H_ID: res.data.OPCM_IMP_PI_H_ID, pREVISE: 0 }, { reload: true })
                    }
                });
            }
        };


    }

})();