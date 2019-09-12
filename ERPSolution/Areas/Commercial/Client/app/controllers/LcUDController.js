
(function () {
    'use strict';
    angular.module('multitex.cmr').controller('LcUDController', ['$q', 'config', 'CmrDataService', 'commonDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', '$modal', 'formData', LcUDController]);
    function LcUDController($q, config, CmrDataService, commonDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog, $modal, formData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = formData.CM_UD_H_ID ? formData : { RF_CURRENCY_ID: 2 };
        vm.formEL = { ISSUE_DT: vm.today };
        vm.formIM = {};
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [loadAllSupplier(), GetCompanyList(), getBuyerListData(), getExportLcList(), getAuthorityList(), GetLcTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        if ($stateParams.pREVISE > 0) {
            vm.form["IS_REVISE"] = true;
        }
        else {
            vm.form["IS_REVISE"] = false;
        }


        if (formData) {

            vm.importList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/cmr/LcUD/GetImpUDInfo?pCM_UD_H_ID=' + (formData.CM_UD_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

            vm.exportList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/cmr/LcUD/GetExpUDInfo?pCM_UD_H_ID=' + (formData.CM_UD_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        }

        vm.getPIDtl = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            vm.formItem["PI_QTY"] = obj.PI_QTY;
            vm.formItem["UNIT_PRICE"] = obj.UNIT_PRICE;
        }

        vm.getItemDataByPI = function (e) {
            //var obj = e.sender.dataItem(e.item);

            if (vm.formItem.CM_IMP_PI_H_ID > 0) {
                CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/GetPIDtlInfo?pCM_IMP_PI_H_ID=' + (vm.formItem.CM_IMP_PI_H_ID || 0)).then(function (res) {
                    vm.itemList = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
            else {
                vm.itemList = new kendo.data.DataSource({
                    data: []
                });
            }

        };

        function getExportLcList() {
            return vm.exportLcList = {
                optionLabel: "-- Select Export LC --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/cmr/ExportLCSalesContact/GetExportLcByBuyerID?pMC_BUYER_ID=' + (vm.form.MC_BUYER_ID || null)).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "EXP_LCSC_NO",
                dataValueField: "CM_EXP_LC_H_ID"
            };
        }


        function getAuthorityList() {
            vm.authorityList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetGmtAsn').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }

        vm.getAuthorityAddress = function (e) {

            var obj = e.sender.dataItem(e.item);
            vm.form["GMT_ASN_ADDRESS"] = obj.ADDRESS;
        }

        vm.getCompanyAddress = function (e) {

            var obj = e.sender.dataItem(e.item);
            vm.form["COMPANY_ADDRESS"] = obj.REG_ADD_EN;
        }

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


        vm.getExportLcByBuyer = function (e) {

            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            vm.form["BUYER_ADDRESS"] = obj.ADDRESS_PI;
            vm.form["BUYER_COUNTRY"] = obj.COUNTRY_NAME_EN;

            if (vm.form.MC_BUYER_ID > 0) {
                CmrDataService.getDataByFullUrl('/api/cmr/ExportLCSalesContact/GetExportLcByBuyerID?pMC_BUYER_ID=' + (vm.form.MC_BUYER_ID || 0)).then(function (res) {
                    vm.expLcList = new kendo.data.DataSource({
                        data: res
                    });
                });
                
                CmrDataService.getDataByFullUrl('/api/cmr/ImportLC/GetImportLcByID?pMC_BUYER_ID=' + (vm.form.MC_BUYER_ID || null) + '&pCM_EXP_LC_H_ID=' + (obj.CM_EXP_LC_H_ID || null)).then(function (res) {
                    vm.importLcList = new kendo.data.DataSource({
                        data: res
                    });
                });
            }

        }

        vm.getExportLcInfo = function (e) {

            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            //vm.formEL = obj;
            vm.formEL.ISSUE_DT = obj.ISSUE_DT;
            vm.formEL.LC_VALUE = obj.LC_VALUE;
            vm.formEL.CURR_NAME_EN = obj.CURR_NAME_EN;
            vm.formEL.PCT_TOLR_AMT = obj.PCT_TOLR_AMT;

            CmrDataService.getDataByFullUrl('/api/cmr/ImportLC/GetImportLcByID?pMC_BUYER_ID=' + (vm.form.MC_BUYER_ID || null) + '&pCM_EXP_LC_H_ID=' + (obj.CM_EXP_LC_H_ID || null)).then(function (res) {
                vm.importLcList = new kendo.data.DataSource({
                    data: res
                });
            });

        }

        vm.getImportLcInfo = function (e) {

            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            //vm.formIM = obj;
            vm.formIM.ISSUE_DT = obj.ISSUE_DT;
            vm.formIM.LC_VALUE = obj.LC_VALUE;
            vm.formIM.CURR_NAME_EN = obj.CURR_NAME_EN;
            vm.formIM.PCT_TOLR_AMT = obj.PCT_TOLR_AMT;
            vm.formIM.CM_EXP_LC_H_ID = vm.formEL.CM_EXP_LC_H_ID;
            vm.formIM.CM_IMP_LC_H_ID = obj.CM_IMP_LC_H_ID;
            
        }

        function getImportLcList() {
            CmrDataService.getDataByFullUrl('/api/cmr/ImportLC/GetImportLcByID?pMC_BUYER_ID=' + (vm.form.MC_BUYER_ID || null) + '&pCM_EXP_LC_H_ID=' + (vm.form.CM_EXP_LC_H_ID || null)).then(function (res) {
                vm.importLcList = new kendo.data.DataSource({
                    data: res
                });
            });
        }



        $scope.UD_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.UD_DT_LNopened = true;
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

            return vm.companyList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };


        function getBuyerListData() {

            if (vm.form.MC_BUYER_ID > 0)
                CmrDataService.getDataByFullUrl('/api/cmr/ExportLCSalesContact/GetExportLcByBuyerID?pMC_BUYER_ID=' + (vm.form.MC_BUYER_ID || 0)).then(function (res) {                   
                    vm.expLcList = new kendo.data.DataSource({
                        data: res
                    });
                });
            else
                vm.expLcList = new kendo.data.DataSource({
                    data: []
                });

            vm.importList = new kendo.data.DataSource({
                data: []
            });

            vm.exportList = new kendo.data.DataSource({
                data: []
            });

            vm.buyerList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        vm.getBuyerAddress = function (e) {
            var obj = e.sender.dataItem(e.item);
            if (vm.form.MC_BUYER_ID > 0)
                vm.form.ADDRESS_PI = obj.ADDRESS_PI;
            else
                vm.form.ADDRESS_PI = "";
        }


        vm.addToImportGrid = function (isValid) {

            if (fnValidateSub() == true) {
               
                var impLc = _.filter(vm.importLcList.data(), function (o) {
                    return o.CM_IMP_LC_H_ID === parseInt(vm.formIM.CM_IMP_LC_H_ID)
                })[0];

                vm.formIM.IMP_LC_NO = impLc.IMP_LC_NO;
                console.log(vm.formIM);
                if (vm.formIM.uid) {
                    // Update

                    var row = vm.importList.getByUid(vm.formIM.uid);
                    var count = _.filter(vm.importList.data(), function (o) {
                        return (o.CM_IMP_LC_H_ID === vm.formIM.CM_IMP_LC_H_ID && o.uid != vm.formIM.uid)
                    }).length;

                    if (count <= 1) {
                        row["CM_IMP_LC_H_ID"] = vm.formIM.CM_IMP_LC_H_ID;
                        row["EXP_LCSC_NO"] = vm.formIM.IMP_LC_NO;

                        row["MOU_CODE"] = "Kg";
                        row["INV_ITEM_CAT_ID"] = 2;

                        config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.importList.data(), function (o) {
                        return (o.CM_IMP_LC_H_ID === vm.formIM.CM_IMP_LC_H_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.importList.data().length + 1;
                        vm.importList.insert(idx, vm.formIM);

                        var gview = vm.importList.data();
                        //console.log(gview);
                        config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
        };


        vm.addToExportGrid = function (isValid) {

            if (fnValidateSub() == true) {

                var impLc = _.filter(vm.expLcList.data(), function (o) {
                    return o.CM_EXP_LC_H_ID === parseInt(vm.formEL.CM_EXP_LC_H_ID)
                })[0];

                vm.formEL.EXP_LCSC_NO = impLc.EXP_LCSC_NO;

                if (vm.formEL.uid) {
                    // Update

                    var row = vm.exportList.getByUid(vm.formEL.uid);
                    var count = _.filter(vm.exportList.data(), function (o) {
                        return (o.CM_EXP_LC_H_ID === vm.formEL.CM_EXP_LC_H_ID && o.uid != vm.formEL.uid)
                    }).length;

                    if (count <= 1) {
                        row["CM_EXP_LC_H_ID"] = vm.formEL.CM_EXP_LC_H_ID;
                        row["EXP_LCSC_NO"] = vm.formEL.EXP_LCSC_NO;

                        row["PI_QTY"] = vm.formEL.PI_QTY;
                        row["UNIT_PRICE"] = vm.formEL.UNIT_PRICE;
                        row["TTL_VALUE"] = vm.formEL.TTL_VALUE;

                        config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.exportList.data(), function (o) {
                        return (o.CM_EXP_LC_H_ID === vm.formEL.CM_EXP_LC_H_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.exportList.data().length + 1;
                        vm.exportList.insert(idx, vm.formEL);

                        var gview = vm.exportList.data();
                        //console.log(gview);
                        config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
        };


        vm.clearAll = function (data) {

            vm.form = { IS_ACTIVE: 'Y' };
        }

        vm.editItemData = function (data) {
            vm.form = angular.copy(data);
        }

        vm.removeData = function (data) {

            Dialog.confirm('Finalizing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.IMPortOrderList.remove(data);
                 });
        }

        vm.gridOptionsIMP = {
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
            scrollable: true,
            columns: [

                { field: "CM_UD_D_ID", hidden: true },
                { field: "CM_IMP_LC_H_ID", hidden: true },
                { field: "CM_EXP_LC_H_ID", hidden: true },

                { field: "IMP_LC_NO", title: "Import L/C No", width: "10%" },
                { field: "ISSUE_DT", title: "LC Date", width: "10%" },
                { field: "LC_VALUE", title: "Value", width: "20%" },
                { field: "LC_VALUE", title: "Used Value", width: "10%" },
                { field: "CURR_NAME_EN", title: "Currency", width: "10%" },
                { field: "PCT_TOLR_AMT", title: "Tolerance %", width: "10%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>',
                    width: "10%"
                }
            ],
        };


        vm.gridOptionsEXP = {
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
            scrollable: true,
            columns: [

                { field: "CM_EXP_LC_H_ID", hidden: true },

                { field: "EXP_LCSC_NO", title: "Export L/C No", width: "10%" },
                { field: "ISSUE_DT", title: "Date", width: "10%" },
                { field: "LC_VALUE", title: "Value", width: "20%" },
                { field: "LC_VALUE", title: "Used Value", width: "10%" },
                { field: "CURR_NAME_EN", title: "Currency", width: "10%" },
                { field: "PCT_TOLR_AMT", title: "Tolerance %", width: "10%" },
                { field: "SHIP_DT", title: "Shipment Date", width: "10%" },
                { field: "SHIP_DT", title: "Expiry Date", width: "10%" },
                
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>',
                    width: "10%"
                }
            ],
        };

        vm.updateAll = function (dataOri, IsFinal) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                
                data["IS_FINALIZED"] = IsFinal;
                data["XML_UD_D"] = CmrDataService.xmlStringShort(vm.importList.data().map(function (o) {
                    return {
                        CM_UD_D_ID: o.CM_UD_D_ID == null ? 0 : o.CM_UD_D_ID,
                        CM_UD_H_ID: o.CM_UD_H_ID == 0 ? null : o.CM_UD_H_ID,
                        CM_EXP_LC_H_ID: o.CM_EXP_LC_H_ID,
                        CM_IMP_LC_H_ID: o.CM_IMP_LC_H_ID == 0 ? null : o.CM_IMP_LC_H_ID
                    }
                }));

                var id = vm.form.CM_UD_H_ID;

                return CmrDataService.saveDataByUrl(data, '/LcUD/Save').then(function (res) {

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
                        vm.form.CM_UD_H_ID = res.data.OPCM_UD_H_ID;
                        $state.go($state.current, { pCM_UD_H_ID: res.data.OPCM_UD_H_ID, pREVISE: 0 }, { inherit: false, reload: true })
                    }
                });
            }
        };

        vm.submitAll = function (dataOri, IsFinal) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                data["IS_FINALIZED"] = IsFinal;
                data["XML_UD_D"] = CmrDataService.xmlStringShort(vm.importList.data().map(function (o) {
                    return {
                        CM_UD_D_ID: o.CM_UD_D_ID == null ? 0 : o.CM_UD_D_ID,
                        CM_UD_H_ID: o.CM_UD_H_ID == 0 ? null : o.CM_UD_H_ID,
                        CM_EXP_LC_H_ID: o.CM_EXP_LC_H_ID,
                        CM_IMP_LC_H_ID: o.CM_IMP_LC_H_ID == 0 ? null : o.CM_IMP_LC_H_ID
                    }
                }));

                var id = vm.form.CM_UD_H_ID;

                return CmrDataService.saveDataByUrl(data, '/LcUD/Save').then(function (res) {

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
                        vm.form.CM_UD_H_ID = res.data.OPCM_UD_H_ID;
                        $state.go($state.current, { pCM_UD_H_ID: res.data.OPCM_UD_H_ID, pREVISE: 0 }, { inherit: false, reload: true })
                    }
                });
            }
        };


        vm.reviseAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                data["IS_FINALIZED"] = 'N';
                data["XML_UD_D"] = CmrDataService.xmlStringShort(vm.importList.data().map(function (o) {
                    return {
                        CM_UD_D_ID: o.CM_UD_D_ID == null ? 0 : o.CM_UD_D_ID,
                        CM_UD_H_ID: o.CM_UD_H_ID == 0 ? null : o.CM_UD_H_ID,
                        CM_EXP_LC_H_ID: o.CM_EXP_LC_H_ID,
                        CM_IMP_LC_H_ID: o.CM_IMP_LC_H_ID == 0 ? null : o.CM_IMP_LC_H_ID
                    }
                }));

                var id = vm.form.CM_UD_H_ID;

                return CmrDataService.saveDataByUrl(data, '/LcUD/Update').then(function (res) {

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
                        vm.form.CM_UD_H_ID = res.data.OPCM_UD_H_ID;
                        $state.go($state.current, { pCM_UD_H_ID: res.data.OPCM_UD_H_ID, pREVISE: 0 }, { inherit: false, reload: true })
                    }
                });
            }
        };


    }

})();