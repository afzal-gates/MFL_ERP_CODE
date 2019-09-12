
(function () {
    'use strict';
    angular.module('multitex.cmr').controller('ImpLcController', ['$q', 'config', 'CmrDataService', 'commonDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', '$modal', 'formData', ImpLcController]);
    function ImpLcController($q, config, CmrDataService, commonDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog, $modal, formData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = formData.CM_IMP_LC_H_ID ? formData : { RF_CURRENCY_ID: 2 };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [loadAllSupplier(), getCurrencyList(), GetCompanyList(), getBuyerListData(), GetCountryList(), GetPaymentTermList(),
                            GetDeliveryPlaceList(), GetIncoTermList(), GetPortList(), getLcStatusList(), GetStatusList(), getExportLcList(),
                            getImportPIList(), getCompanyInsuranceList(), GetLcTypeList()];
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
                    vm.itemList = res;
                    //vm.itemList = new kendo.data.DataSource({
                    //    data: res
                    //});
                });
            }
            else {
                vm.itemList = [];
                //vm.itemList = new kendo.data.DataSource({
                //    data: []
                //});
            }

        };

        vm.removeRow = function (item, indx) {

            var index = vm.itemList.indexOf(item);
            vm.itemList.splice(index, 1);

            //var obj = angular.copy(item);
            //var idx = indx + 1;
            //vm.itemList.splice(idx, "0", obj);
        }

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


        function getCompanyInsuranceList() {
            vm.insuranceCompanyList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/common/getCompanyInsuranceList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }

        vm.getInsuranceInfo = function (e) {

            var obj = e.sender.dataItem(e.item);
            vm.form["INSUR_CVN_NO"] = obj.INSUR_CVN_NO;
            vm.form["INSUR_DT"] = obj.INSUR_DT;
            vm.form["INSUR_ADDRESS_EN"] = obj.ADDRESS_EN;
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
            if (vm.form.MC_BUYER_ID > 0)
                CmrDataService.getDataByFullUrl('/api/cmr/ExportLCSalesContact/GetExportLcByBuyerID?pMC_BUYER_ID=' + (vm.form.MC_BUYER_ID || 0)).then(function (res) {
                    vm.exportLcList = res;
                    vm.expLcList = new kendo.data.DataSource({
                        data: res
                    });
                });

        }

        function getImportPIList() {
            CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/GetImportLCPIInfoBySupplierID?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || null)).then(function (res) {
                vm.importPIList = new kendo.data.DataSource({
                    data: res
                });
            });
        }


        if (formData) {

            vm.IMPortOrderList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/cmr/ImportLC/GetImpLcDtlInfo?pCM_IMP_LC_H_ID=' + (formData.CM_IMP_LC_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        }

        $scope.ISSUE_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ISSUE_DT_LNopened = true;
        }

        $scope.EXPIRY_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.EXPIRY_DT_LNopened = true;
        }

        $scope.LTST_SHPMNT_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.LTST_SHPMNT_DT_LNopened = true;
        }

        $scope.REVISION_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REVISION_DT_LNopened = true;
        }

        vm.copyReceiverBank = function () {

            if (vm.form.SMAE == true)
                vm.form.LIN_BKBR_ID = vm.form.RCV_BKBR_ID;
            else
                vm.form.LIN_BKBR_ID = '';

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

            if (vm.form.MC_BUYER_ID > 0)
                CmrDataService.getDataByFullUrl('/api/cmr/ExportLCSalesContact/GetExportLcByBuyerID?pMC_BUYER_ID=' + (vm.form.MC_BUYER_ID || 0)).then(function (res) {
                    vm.exportLcList = res;
                    vm.expLcList = new kendo.data.DataSource({
                        data: res
                    });
                });
            else
                vm.expLcList = new kendo.data.DataSource({
                    data: []
                });

            vm.IMPortOrderList = new kendo.data.DataSource({
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

        vm.getSupplierInfo = function (e) {

            var obj = e.sender.dataItem(e.item);

            if (obj.SCM_SUPPLIER_ID > 0)
                vm.form.ADDRESS_DEFA = obj.ADDRESS_DEFA;
            else
                vm.form.ADDRESS_DEFA = "";
        }


        vm.getBuyerAddress = function (e) {
            var obj = e.sender.dataItem(e.item);
            if (vm.form.MC_BUYER_ID > 0)
                vm.form.ADDRESS_PI = obj.ADDRESS_PI;
            else
                vm.form.ADDRESS_PI = "";
        }

        vm.clearGrid = function () {
            vm.orderDtlList = new kendo.data.DataSource({
                data: []
            });
            vm.form.MC_ORDER_H_ID = "";
        }


        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {

                for (var i = 0; i < vm.itemList.length; i++) {

                    var item = angular.copy(vm.itemList[i]);

                    vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;
                    vm.formItem.INV_ITEM_ID = item.INV_ITEM_ID;

                    var pi = _.filter(vm.importPIList.data(), function (o) {
                        return o.CM_IMP_PI_H_ID === parseInt(vm.formItem.CM_IMP_PI_H_ID)
                    })[0];

                    vm.formItem.PI_NO_IMP = pi.PI_NO_IMP;

                    if (vm.formItem.CM_EXP_LC_H_ID > 0) {
                        var lc = _.filter(vm.expLcList.data(), function (o) {
                            return o.CM_EXP_LC_H_ID === parseInt(vm.formItem.CM_EXP_LC_H_ID)
                        })[0];

                        vm.formItem.EXP_LCSC_NO = lc.EXP_LCSC_NO;
                    }

                    vm.formItem.UNIT_PRICE = item.UNIT_PRICE;
                    vm.formItem.LC_QTY = item.LC_QTY;
                    vm.formItem.PI_QTY = item.PI_QTY;

                    vm.formItem["TTL_VALUE"] = item.LC_QTY * item.UNIT_PRICE;


                    var count = _.filter(vm.IMPortOrderList.data(), function (o) {
                        return (o.INV_ITEM_ID === item.INV_ITEM_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.IMPortOrderList.data().length + 1;
                        vm.IMPortOrderList.insert(idx, vm.formItem);

                        var gview = vm.IMPortOrderList.data();
                        //console.log(gview);
                        config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                }
            }
        };


        vm.getStyleOrderList = function (e) {
            var obj = e.sender.dataItem(e.item);
            if (vm.form.MC_STYLE_H_ID > 0) {
                vm.orderList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/mrc/Order/OrderDataList/' + (vm.form.MC_BUYER_ID || null) + '/null/' + (vm.form.MC_STYLE_H_ID || null) + '/null/null').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }
        }


        vm.showOrderDtl = function (e) {
            var obj = e.sender.dataItem(e.item);
            if (vm.form.MC_ORDER_H_ID > 0) {
                vm.orderDtlList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/cmr/ImportLC/GetOrderDtlInfo?pMC_ORDER_H_ID=' + (vm.form.MC_ORDER_H_ID || null)).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }
        }

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
                vm.form.DOC_TITLE = "Back to Back LC No";
            else
                vm.form.DOC_TITLE = "Direct LC No";

        }

        vm.SenderbankBranchList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    CmrDataService.getDataByFullUrl('/api/common/GetBankBranchList').then(function (res) {

                        e.success([{
                            BANK_BRANCH_NAME_EN: '--New Branch--',
                            RF_BANK_BRANCH_ID: -1
                        }].concat(res));

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });



        vm.RcvbankBranchList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    CmrDataService.getDataByFullUrl('/api/common/GetBankBranchList').then(function (res) {

                        e.success([{
                            BANK_BRANCH_NAME_EN: '--New Branch--',
                            RF_BANK_BRANCH_ID: -1
                        }].concat(res));

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });



        vm.LeanbankBranchList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    CmrDataService.getDataByFullUrl('/api/common/GetBankBranchList').then(function (res) {

                        e.success([{
                            BANK_BRANCH_NAME_EN: '--New Branch--',
                            RF_BANK_BRANCH_ID: -1
                        }].concat(res));

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.newBankBranchAdd = function (BankID, id, e) {
            var aryBr = {};

            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            if (id == 1) {
                vm.form.SND_BANK_AS = obj.BANK_NAME_EN + ", " + obj.BRANCH_ADDRESS_EN + ", " + obj.SWIFT_CODE + ", " + obj.SWIFT_CODE_EXT;
            }
            else if (id == 2) {
                vm.form.RCV_BANK_AS = obj.BANK_NAME_EN + ", " + obj.BRANCH_ADDRESS_EN + ", " + obj.SWIFT_CODE + ", " + obj.SWIFT_CODE_EXT;
            }
            else if (id == 3) {
                vm.form.LEAN_BANK_AS = obj.BANK_NAME_EN + ", " + obj.BRANCH_ADDRESS_EN + ", " + obj.SWIFT_CODE + ", " + obj.SWIFT_CODE_EXT;
            }
            if (BankID == -1) {
                var SWIFT_CODE = "";
                if (vm.bankList) {
                    var listBank = _.filter(vm.bankListData.data(), { 'RF_BANK_ID': parseInt((BankID || 0)) })[0];
                    if (listBank)
                        SWIFT_CODE = listBank.SWIFT_CODE;
                }
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/GlobalUI/BankBranchEntry',
                    controller: function ($modalInstance, $scope, $timeout, CmrDataService, commonDataService) {
                        var vm = this;

                        vm.errors = [];
                        //GetBankList()
                        BankBranchDataList()
                        GetCountryList();
                        vm.bankList = new kendo.data.DataSource({
                            transport: {
                                read: function (e) {
                                    commonDataService.getBankListData().then(function (res) {
                                        e.success([{
                                            BANK_NAME_EN: '--New Bank--',
                                            RF_BANK_ID: -1
                                        }].concat(res));
                                    });
                                }
                            }
                        });


                        vm.getBankSwiftCode = function (e) {

                            var itemss = e.sender.dataItem(e.item);
                            if (itemss.RF_BANK_ID == -1)
                                vm.newBankAdd();
                            else {
                                var listBank = _.filter(vm.bankList.data(), { 'RF_BANK_ID': parseInt(itemss.RF_BANK_ID) })[0];
                                vm.form.SWIFT_CODE = listBank.SWIFT_CODE;
                            }
                            return false;
                        }

                        function GetCountryList() {
                            return vm.CountryList = {
                                optionLabel: "-- Select Country --",
                                filter: "startswith",
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


                        function BankBranchDataList() {
                            vm.bankBranchAllList = new kendo.data.DataSource({
                                transport: {
                                    read: function (e) {
                                        CmrDataService.getDataByFullUrl('/api/common/GetBankBranchList').then(function (res) {
                                            e.success(res);
                                        });
                                    }
                                }
                            });
                        };

                        vm.form = { 'RF_BANK_ID': BankID, 'SWIFT_CODE': SWIFT_CODE };



                        vm.editBankBranch = function (data) {
                            vm.form = angular.copy(data);
                        }
                        vm.gridBankBranch = {
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
                            groupable: false,
                            columns: [
                                { field: "RF_BANK_BRANCH_ID", hidden: true },
                                { field: "RF_BANK_ID", hidden: true },
                                { field: "BANK_BRANCH_NAME_BN", hidden: true },
                                { field: "SWIFT_CODE_EXT", hidden: true },
                                { field: "SWIFT_CODE", hidden: true },
                                { field: "ROUTING_NO", hidden: true },
                                { field: "HR_COUNTRY_ID", hidden: true },
                                { field: "BRANCH_ADDRESS_BN", hidden: true },
                                { field: "PHONE_NO", hidden: true },
                                { field: "PHONE_EXT", hidden: true },
                                { field: "FAX_NO", hidden: true },
                                { field: "EMAIL_ID", hidden: true },
                                { field: "IS_CORPORATE_L", hidden: true },
                                { field: "IS_CORPORATE_F", hidden: true },
                                { field: "BANK_BRANCH_CODE", title: "Branch Code", width: "15%" },
                                { field: "BANK_NAME_EN", title: "Bank Name", width: "20%" },
                                { field: "BANK_BRANCH_NAME_EN", title: "Branch Name", width: "25%" },
                                { field: "BRANCH_ADDRESS_EN", title: "Address", width: "20%" },
                                { field: "IS_ACTIVE", title: "Active", width: "10%" },

                                {
                                    title: "",
                                    template: '<a title="Edit" ng-click="vm.editBankBranch(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                                    width: "10%"
                                }
                            ],
                            //editable: true

                        };

                        vm.form['BANK_BRANCH_NAME_EN'] = '';
                        vm.formDisabled = false;


                        vm.cancel = function (data) {
                            aryBr = data;
                            $modalInstance.close(vm.bankBranchAllList);
                        };

                        vm.removeBankBranch = function (data) {

                            if (!data.RF_BANK_BRANCH_ID || data.RF_BANK_BRANCH_ID <= 0) {
                                vm.bankAllList.remove(data);
                                return;
                            };

                            Dialog.confirm('Removing "' + data.BANK_BRANCH_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                                 .then(function () {
                                     vm.bankAllList.remove(data);
                                 });

                        }

                        vm.resetBankBranchInfo = function () {
                            vm.form = {};
                        };

                        vm.submitDataNewBankBranch = function (dataOri, token) {
                            var data = angular.copy(dataOri);

                            return CmrDataService.saveDataByFullUrl(data, '/api/common/BankBranchSave', token).then(function (res) {
                                if (res.success === false) {
                                    vm.errors = res.errors;
                                }
                                else {
                                    res['data'] = angular.fromJson(res.jsonStr);
                                    config.appToastMsg(res.data.PMSG);

                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                        //alert(parseInt(res.data.OPRF_BANK_BRANCH_ID));
                                        vm.form['RF_BANK_BRANCH_ID'] = parseInt(res.data.OPRF_BANK_BRANCH_ID);
                                    }
                                    BankBranchDataList()
                                    //vm.form = {};
                                }
                            }).catch(function (message) {
                                exception.catcher('XHR loading Failded')(message);
                            });

                        };



                        vm.newBankAdd = function () {
                            var ary = {};
                            var modalInstance = $modal.open({
                                animation: true,
                                templateUrl: '/GlobalUI/BankEntry',
                                controller: function ($modalInstance, $scope, $timeout, CmrDataService) {
                                    var vm = this;
                                    vm.form = {};
                                    vm.errors = [];
                                    GetCountryList()
                                    BankDataList()
                                    function GetCountryList() {
                                        return vm.CountryList = {
                                            optionLabel: "-- Select Country --",
                                            filter: "startswith",
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

                                    function BankDataList() {
                                        vm.bankAllList = new kendo.data.DataSource({
                                            transport: {
                                                read: function (e) {
                                                    CmrDataService.getDataByFullUrl('/api/common/BankDataList').then(function (res) {
                                                        e.success(res);
                                                    });
                                                }
                                            }
                                        });
                                    };

                                    vm.editBank = function (data) {
                                        vm.form = angular.copy(data);
                                    }
                                    vm.gridBank = {
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
                                        groupable: false,
                                        columns: [

                                            { field: "RF_BANK_ID", hidden: true },
                                            { field: "BANK_NAME_BN", hidden: true },
                                            { field: "BANK_PREFIX", hidden: true },
                                            { field: "IS_LOCAL", hidden: true },
                                            { field: "HR_COUNTRY_ID", hidden: true },
                                            { field: "IS_TREASURY", hidden: true },
                                            { field: "IBN_NO", hidden: true },
                                            { field: "CALL_CENTER_NO", hidden: true },
                                            { field: "WEB_URL", hidden: true },
                                            { field: "BANK_CODE", title: "BANK CODE", width: "15%" },
                                            { field: "BANK_NAME_EN", title: "Bank Name (EN)", width: "30%" },
                                            { field: "SWIFT_CODE", title: "Swift Code", width: "30%" },
                                            { field: "IS_ACTIVE", title: "Active", width: "15%" },

                                            {
                                                title: "",
                                                template: '<a title="Edit" ng-click="vm.editBank(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                                                width: "10%"
                                            }
                                        ],
                                        //editable: true

                                    };

                                    vm.form['BANK_NAME_EN'] = '';
                                    vm.formDisabled = false;


                                    vm.cancel = function (data) {
                                        ary = data;
                                        $modalInstance.close(vm.bankAllList);
                                    };

                                    vm.editBrand = function (data) {
                                        vm.form = angular.copy(data);
                                    }

                                    vm.removeBank = function (data) {

                                        if (!data.RF_BANK_ID || data.RF_BANK_ID <= 0) {
                                            vm.bankAllList.remove(data);
                                            return;
                                        };

                                        Dialog.confirm('Removing "' + data.BANK_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                                             .then(function () {
                                                 vm.bankAllList.remove(data);
                                             });

                                    }

                                    vm.resetBankInfo = function () {
                                        vm.form = {};
                                    };

                                    vm.submitDataNewBank = function (dataOri, token) {
                                        var data = angular.copy(dataOri);

                                        return CmrDataService.saveDataByFullUrl(data, '/api/common/BankSave', token).then(function (res) {
                                            if (res.success === false) {
                                                vm.errors = res.errors;
                                            }
                                            else {
                                                res['data'] = angular.fromJson(res.jsonStr);
                                                config.appToastMsg(res.data.PMSG);

                                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                                    vm.form['RF_BANK_ID'] = parseInt(res.data.OPRF_BANK_ID);
                                                }
                                                BankDataList()
                                                //vm.form = {};
                                            }
                                        }).catch(function (message) {
                                            exception.catcher('XHR loading Failded')(message);
                                        });

                                    };


                                },
                                controllerAs: 'vm',
                                size: 'lg',
                                windowClass: 'large-Modal',

                            });

                            modalInstance.result.then(function (data) {
                                vm.bankList.read();

                            }, function () {
                                console.log('Modal dismissed at: ' + new Date());
                            });
                        }



                    },
                    controllerAs: 'vm',
                    size: 'lg',
                    windowClass: 'large-Modal',

                });

                modalInstance.result.then(function (data) {
                    vm.SenderbankBranchList.read();
                    vm.RcvbankBranchList.read();
                    vm.LeanbankBranchList.read();

                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });
            }
        }


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

                     //var List = _.filter(vm.IMPortOrderList.data(), function (x) { return x.INV_ITEM_ID == data.INV_ITEM_ID });
                     //for (var i = 0; i < List.length; i++) {
                     //    var item = List[i];
                     //    vm.IMPortOrderList.remove(item);
                     //}
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
            height: '300px',
            scrollable: true,
            columns: [


                { field: "CM_IMP_LC_D_ID", hidden: true },
                { field: "CM_IMP_LC_H_ID", hidden: true },
                { field: "CM_EXP_LC_H_ID", hidden: true },
                { field: "CM_IMP_PI_H_ID", hidden: true },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                { field: "EXP_LCSC_NO", title: "Export L/C No", width: "10%" },
                { field: "PI_NO_IMP", title: "PI No", width: "10%" },
                { field: "ITEM_NAME_EN", title: "Description of Goods", width: "20%" },
                { field: "PI_QTY", title: "PI Quantity", width: "10%" },
                { field: "LC_QTY", title: "LC Quantity", width: "10%" },
                { field: "UNIT_PRICE", title: "Unit Price(US$)", width: "10%" },
                { field: "TTL_VALUE", title: "Total Price(US$)", width: "10%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>',
                    width: "10%"
                }
            ],
        };

        vm.updateAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                //var LC_VAL = 0;
                //for (var i = 0; i < vm.IMPortOrderList.data().length; i++) {
                //    var obj = vm.IMPortOrderList.data()[i];
                //    LC_VAL = LC_VAL + obj.TTL_VALUE;
                //}
                //if (parseInt(LC_VAL) != parseInt(vm.form.LC_TOT_VAL) && vm.form.IS_ADV_LC != 'Y') {
                //    config.appToastMsg("MULTI-005 LC value mismatch with PO value!!! " + LC_VAL);
                //    return;
                //}

                data["EXP_LC_NO_LST"] = "1";
                data["DSG_PORT_LST"] = !data.DSG_PORT_LST ? '0' : data.DSG_PORT_LST.join(',');
                //data["IS_LC_ADJ"] = 'N';
                data["IS_UPDATE"] = 'Y';
                data["XML_LC_D"] = CmrDataService.xmlStringShort(vm.IMPortOrderList.data().map(function (o) {
                    return {
                        CM_IMP_LC_D_ID: o.CM_IMP_LC_D_ID == null ? 0 : o.CM_IMP_LC_D_ID,
                        CM_IMP_LC_H_ID: o.CM_IMP_LC_H_ID == 0 ? null : o.CM_IMP_LC_H_ID,
                        CM_EXP_LC_H_ID: o.CM_EXP_LC_H_ID,
                        CM_IMP_PI_H_ID: o.CM_IMP_PI_H_ID == 0 ? null : o.CM_IMP_PI_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        LC_QTY: o.LC_QTY == null ? 0 : o.LC_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 1 : o.QTY_MOU_ID == 0 ? 3 : o.QTY_MOU_ID
                    }
                }));

                var id = vm.form.CM_IMP_LC_H_ID;

                return CmrDataService.saveDataByUrl(data, '/ImportLC/Save').then(function (res) {

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
                        vm.form.CM_IMP_LC_H_ID = res.data.OPCM_IMP_LC_H_ID;
                        $state.go($state.current, { pCM_IMP_LC_H_ID: res.data.OPCM_IMP_LC_H_ID, pREVISE: 0 }, { inherit: false, reload: true })
                    }
                });
            }
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                //var LC_VAL = 0;
                //for (var i = 0; i < vm.IMPortOrderList.data().length; i++) {
                //    var obj = vm.IMPortOrderList.data()[i];
                //    LC_VAL = LC_VAL + obj.TTL_VALUE;
                //}
                //if (parseInt(LC_VAL) != parseInt(vm.form.LC_TOT_VAL) && vm.form.IS_ADV_LC != 'Y') {
                //    config.appToastMsg("MULTI-005 LC value mismatch with PO value!!! " + LC_VAL);
                //    return;
                //}

                data["EXP_LC_NO_LST"] = "1";
                data["DSG_PORT_LST"] = !data.DSG_PORT_LST ? '0' : data.DSG_PORT_LST.join(',');
                //data["IS_LC_ADJ"] = 'N';
                data["IS_UPDATE"] = 'N';
                data["XML_LC_D"] = CmrDataService.xmlStringShort(vm.IMPortOrderList.data().map(function (o) {
                    return {
                        CM_IMP_LC_D_ID: o.CM_IMP_LC_D_ID == null ? 0 : o.CM_IMP_LC_D_ID,
                        CM_IMP_LC_H_ID: o.CM_IMP_LC_H_ID == 0 ? null : o.CM_IMP_LC_H_ID,
                        CM_EXP_LC_H_ID: o.CM_EXP_LC_H_ID,
                        CM_IMP_PI_H_ID: o.CM_IMP_PI_H_ID == 0 ? null : o.CM_IMP_PI_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        LC_QTY: o.LC_QTY == null ? 0 : o.LC_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 1 : o.QTY_MOU_ID == 0 ? 3 : o.QTY_MOU_ID
                    }
                }));

                var id = vm.form.CM_IMP_LC_H_ID;

                return CmrDataService.saveDataByUrl(data, '/ImportLC/Save').then(function (res) {

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
                        vm.form.CM_IMP_LC_H_ID = res.data.OPCM_IMP_LC_H_ID;
                        $state.go($state.current, { pCM_IMP_LC_H_ID: res.data.OPCM_IMP_LC_H_ID, pREVISE: 0 }, { inherit: false, reload: true })
                    }
                });
            }
        };


        vm.reviseAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                //var LC_VAL = 0;
                //for (var i = 0; i < vm.IMPortOrderList.data().length; i++) {
                //    var obj = vm.IMPortOrderList.data()[i];
                //    LC_VAL = LC_VAL + obj.TTL_VALUE;
                //}
                //if (parseInt(LC_VAL) != parseInt(vm.form.LC_TOT_VAL) && vm.form.IS_ADV_LC != 'Y') {
                //    config.appToastMsg("MULTI-005 LC value mismatch with PO value!!! " + LC_VAL);
                //    return;
                //}

                data["EXP_LC_NO_LST"] = "1";
                data["DSG_PORT_LST"] = !data.DSG_PORT_LST ? '0' : data.DSG_PORT_LST.join(',');
                //data["IS_LC_ADJ"] = 'N';
                data["XML_LC_D"] = CmrDataService.xmlStringShort(vm.IMPortOrderList.data().map(function (o) {
                    return {
                        CM_IMP_LC_D_PO_ID: o.CM_IMP_LC_D_PO_ID == null ? 0 : o.CM_IMP_LC_D_PO_ID,
                        CM_IMP_LC_H_ID: o.CM_IMP_LC_H_ID == null ? 0 : o.CM_IMP_LC_H_ID,
                        MC_ORDER_H_ID: o.MC_ORDER_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        MC_STYLE_D_ITEM_ID: o.MC_STYLE_D_ITEM_ID == null ? 0 : o.MC_STYLE_D_ITEM_ID,
                        MC_COLOR_ID: o.MC_COLOR_ID == null ? 0 : o.MC_COLOR_ID,
                        MC_SIZE_LST: o.MC_SIZE_LST == null ? '' : o.MC_SIZE_LST,
                        ORDER_QTY: o.ORDER_QTY == null ? 0 : o.ORDER_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 1 : o.QTY_MOU_ID == 0 ? 1 : o.QTY_MOU_ID
                    }
                }));

                var id = vm.form.CM_IMP_LC_H_ID;

                return CmrDataService.saveDataByUrl(data, '/ImportLC/Update').then(function (res) {

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
                        vm.form.CM_IMP_LC_H_ID = res.data.OPCM_IMP_LC_H_ID;
                        $state.go($state.current, { pCM_IMP_LC_H_ID: res.data.OPCM_IMP_LC_H_ID, pREVISE: 0 }, { inherit: false, reload: true })
                    }
                });
            }
        };


    }

})();