
(function () {
    'use strict';
    angular.module('multitex.cmr').controller('ExpLcContractController', ['$q', 'config', 'CmrDataService', 'commonDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', '$modal', 'formData', ExpLcContractController]);
    function ExpLcContractController($q, config, CmrDataService, commonDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog, $modal, formData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = formData.CM_EXP_LC_H_ID ? formData : { DOC_TITLE: 'Document No', RF_CURRENCY_ID: 2, IS_ADV_LC: 'N', IS_LC_ADJ: 'N', IS_PART_SHPM: 'N', IS_TRAN_SHPM: 'N', LC_TOT_VAL: 0 };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getCurrencyList(), GetCompanyList(), getBuyerListData(), GetCountryList(), GetPaymentTermList(),
                            GetDeliveryPlaceList(), GetIncoTermList(), GetPortList(), getLcStatusList(), GetStatusList()];
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

            if (vm.form.MC_BUYER_ID > 0) {
                vm.notifyPartyList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/cmr/BuyerNotifyParty/GetBuyerNotifyPartyInfo?pMC_BUYER_ID=' + (formData.MC_BUYER_ID || 1)).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }

            vm.exportOrderList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/cmr/ExportLCSalesContact/GetPOListByID?pCM_EXP_LC_H_ID=' + (formData.CM_EXP_LC_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    model: {
                        fields: {
                            ITEM_NAME_EN: { type: "string" },
                            TTL_VALUE: { type: "number" }
                        }
                    }
                },
                aggregate: [{ field: "TTL_VALUE", aggregate: "sum" }]
            });

            vm.form["CM_NOTIFY_PARTY_LST"] = formData.CM_NOTIFY_PARTY_LST ? formData.CM_NOTIFY_PARTY_LST.split(',') : [];
            vm.form["DSG_PORT_LST"] = formData.DSG_PORT_LST ? formData.DSG_PORT_LST.split(',') : [];
            if (formData.IS_SC_OR_LC == "L")
                vm.form["DOC_TITLE"] = "Export LC No";
            else
                vm.form["DOC_TITLE"] = "Sales Contact No";

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

            vm.notifyPartyList = new kendo.data.DataSource({
                data: []
            });

            vm.styleList = new kendo.data.DataSource({
                data: []
            });

            vm.orderList = new kendo.data.DataSource({
                data: []
            });
            vm.orderDtlList = new kendo.data.DataSource({
                data: []
            });

            vm.exportOrderList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        e.success([]);
                    }
                },
                schema: {
                    model: {
                        fields: {
                            ITEM_NAME_EN: { type: "string" },
                            TTL_VALUE: { type: "number" }
                        }

                    }
                },
                group: {
                    field: "ITEM_NAME_EN", title: "Yarn", aggregates: [{ field: "TTL_VALUE", aggregate: "sum" }]
                },
                aggregate: [{ field: "TTL_VALUE", aggregate: "sum" }]
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
            vm.orderDtlList = new kendo.data.DataSource({
                data: []
            });
            vm.form.MC_ORDER_H_ID = "";
        }

        vm.AddToGrid = function () {

            for (var i = 0; i < vm.orderDtlList.data().length; i++) {

                var idx = vm.exportOrderList.data().length + 1;
                var obj = vm.orderDtlList.data()[i];
                vm.exportOrderList.insert(idx, obj);
            }

            //console.log(vm.exportOrderList.data());

            var ttl = 0;
            for (var i = 0; i < vm.exportOrderList.data().length; i++) {
                var item = vm.exportOrderList.data()[i];
                ttl = ttl + item.TTL_VALUE;
            }
            vm.form.PO_TOT_VAL = ttl;
        }

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
                            CmrDataService.getDataByFullUrl('/api/cmr/ExportLCSalesContact/GetOrderDtlInfo?pMC_ORDER_H_ID=' + (vm.form.MC_ORDER_H_ID || null)).then(function (res) {
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
                vm.form.DOC_TITLE = "Sales Contact No";
            else
                vm.form.DOC_TITLE = "Export LC No";

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

            Dialog.confirm('Finalizing "' + data.ORDER_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var List = _.filter(vm.exportOrderList.data(), function (x) { return x.MC_ORDER_H_ID == data.MC_ORDER_H_ID });

                     for (var i = 0; i < List.length; i++) {
                         var item = List[i];
                         vm.exportOrderList.remove(item);
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
            height: '300px',
            scrollable: true,
            //groupable: true,
            columns: [

                { field: "MC_ORDER_H_ID", hidden: true },
                { field: "MC_STYLE_D_ITEM_ID", hidden: true },
                { field: "MC_COLOR_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                { field: "ORDER_NO", title: "Order No", width: "10%" },
                { field: "STYLE_NO", title: "Style No", width: "10%" },
                { field: "ITEM_NAME_EN", title: "Description of Goods", width: "20%" },
                { field: "COLOR_NAME_EN", title: "Color", width: "10%" },
                { field: "MC_SIZE_LST", title: "Size", width: "10%" },
                { field: "ORDER_QTY", title: "Quantity", width: "10%" },
                { field: "UNIT_PRICE", title: "Unit Price(US$)", width: "10%" },
                //{ field: "TTL_VALUE", title: "Total Price(US$)", width: "10%" },

                { field: "TTL_VALUE", title: "Total Price(US$)", width: "10%", aggregates: ["sum"], groupFooterTemplate: "Total: #=sum#", footerTemplate: "Total Price: #=sum#", },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>',
                    width: "10%"
                }
            ],
        };

        vm.gridOptionsODL = {
            pageable: false,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },
            height: '150px',
            scrollable: true,
            columns: [

                { field: "MC_ORDER_H_ID", hidden: true },
                { field: "MC_STYLE_D_ITEM_ID", hidden: true },
                { field: "MC_COLOR_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                { field: "ORDER_NO", title: "Order No", width: "10%" },
                { field: "STYLE_NO", title: "Style No", width: "10%" },
                { field: "ITEM_NAME_EN", title: "Description of Goods", width: "20%" },
                { field: "COLOR_NAME_EN", title: "Color", width: "10%" },
                { field: "MC_SIZE_LST", title: "Size", width: "10%" },
                { field: "ORDER_QTY", title: "Quantity", width: "10%" },
                { field: "UNIT_PRICE", title: "Unit Price(US$)", width: "10%" },
                { field: "TTL_VALUE", title: "Total Price(US$)", width: "10%" },

            ],
        };


        vm.openNotifyModal = function (item) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Commercial/Cmr/_BuyerNotifyPartyModal',
                controller: 'BuyerNotifyPartyModalController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'large-Modal',
                //windowClass: 'app-modal-window',
                resolve: {
                    formData: function (CmrDataService) {
                        item["IS_ACTIVE"] = 'Y';
                        return item;
                    }
                }
            });
            modalInstance.result.then(function (dataItem) {
                console.log(dataItem.data());
                vm.notifyPartyList = new kendo.data.DataSource({
                    data: dataItem.data()
                });

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }


        vm.updateAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                var LC_VAL = 0;

                //for (var i = 0; i < vm.exportOrderList.data().length; i++) {
                //    var obj = vm.exportOrderList.data()[i];
                //    LC_VAL = LC_VAL + obj.TTL_VALUE;
                //}

                if (parseInt(vm.form.LC_TOT_VAL) != parseInt(vm.form.PO_TOT_VAL) && vm.form.IS_ADV_LC != 'Y' && vm.form.IS_IGNOR_DIFF != 'Y') {
                    config.appToastMsg("MULTI-005 LC value mismatch with PO value!!! " + vm.form.LC_TOT_VAL);
                    return;
                }

                data["CM_NOTIFY_PARTY_LST"] = !data.CM_NOTIFY_PARTY_LST ? '0' : data.CM_NOTIFY_PARTY_LST.join(',');
                data["DSG_PORT_LST"] = !data.DSG_PORT_LST ? '0' : data.DSG_PORT_LST.join(',');
                data["IS_LC_ADJ"] = 'N';
                data["IS_UPDATE"] = 'Y';
                data["XML_PO"] = CmrDataService.xmlStringShort(vm.exportOrderList.data().map(function (o) {
                    return {
                        CM_EXP_LC_D_PO_ID: o.CM_EXP_LC_D_PO_ID == null ? 0 : o.CM_EXP_LC_D_PO_ID,
                        CM_EXP_LC_H_ID: o.CM_EXP_LC_H_ID == null ? 0 : o.CM_EXP_LC_H_ID,
                        MC_ORDER_H_ID: o.MC_ORDER_H_ID,
                        MC_STYLE_D_ITEM_ID: o.MC_STYLE_D_ITEM_ID == null ? 0 : o.MC_STYLE_D_ITEM_ID,
                        MC_COLOR_ID: o.MC_COLOR_ID == null ? 0 : o.MC_COLOR_ID,
                        MC_SIZE_LST: o.MC_SIZE_LST == null ? '' : o.MC_SIZE_LST,
                        ORDER_QTY: o.ORDER_QTY == null ? 0 : o.ORDER_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 1 : o.QTY_MOU_ID == 0 ? 1 : o.QTY_MOU_ID
                    }
                }));

                var id = vm.form.CM_EXP_LC_H_ID;

                return CmrDataService.saveDataByUrl(data, '/ExportLCSalesContact/Save').then(function (res) {

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
                        vm.form.CM_EXP_LC_H_ID = res.data.OPCM_EXP_LC_H_ID;
                        $state.go($state.current, { pCM_EXP_LC_H_ID: res.data.OPCM_EXP_LC_H_ID, pREVISE: 0 }, { inherit: false, reload: true })
                    }
                });
            }
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                var LC_VAL = 0;

                //for (var i = 0; i < vm.exportOrderList.data().length; i++) {
                //    var obj = vm.exportOrderList.data()[i];
                //    LC_VAL = LC_VAL + obj.TTL_VALUE;
                //}
                //if (parseInt(LC_VAL) != parseInt(vm.form.LC_TOT_VAL) && vm.form.IS_ADV_LC != 'Y') {
                //    config.appToastMsg("MULTI-005 LC value mismatch with PO value!!! " + LC_VAL);
                //    return;
                //}

                if (parseInt(vm.form.LC_TOT_VAL) != parseInt(vm.form.PO_TOT_VAL) && vm.form.IS_ADV_LC != 'Y' && vm.form.IS_IGNOR_DIFF != 'Y') {
                    config.appToastMsg("MULTI-005 LC value mismatch with PO value!!! " + vm.form.LC_TOT_VAL);
                    return;
                }

                data["CM_NOTIFY_PARTY_LST"] = !data.CM_NOTIFY_PARTY_LST ? '0' : data.CM_NOTIFY_PARTY_LST.join(',');
                data["DSG_PORT_LST"] = !data.DSG_PORT_LST ? '0' : data.DSG_PORT_LST.join(',');
                data["IS_LC_ADJ"] = 'N';
                data["IS_UPDATE"] = 'N';
                data["XML_PO"] = CmrDataService.xmlStringShort(vm.exportOrderList.data().map(function (o) {
                    return {
                        CM_EXP_LC_D_PO_ID: o.CM_EXP_LC_D_PO_ID == null ? 0 : o.CM_EXP_LC_D_PO_ID,
                        CM_EXP_LC_H_ID: o.CM_EXP_LC_H_ID == null ? 0 : o.CM_EXP_LC_H_ID,
                        MC_ORDER_H_ID: o.MC_ORDER_H_ID,
                        MC_STYLE_D_ITEM_ID: o.MC_STYLE_D_ITEM_ID == null ? 0 : o.MC_STYLE_D_ITEM_ID,
                        MC_COLOR_ID: o.MC_COLOR_ID == null ? 0 : o.MC_COLOR_ID,
                        MC_SIZE_LST: o.MC_SIZE_LST == null ? '' : o.MC_SIZE_LST,
                        ORDER_QTY: o.ORDER_QTY == null ? 0 : o.ORDER_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 1 : o.QTY_MOU_ID == 0 ? 1 : o.QTY_MOU_ID
                    }
                }));

                var id = vm.form.CM_EXP_LC_H_ID;

                return CmrDataService.saveDataByUrl(data, '/ExportLCSalesContact/Save').then(function (res) {

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
                        vm.form.CM_EXP_LC_H_ID = res.data.OPCM_EXP_LC_H_ID;
                        $state.go($state.current, { pCM_EXP_LC_H_ID: res.data.OPCM_EXP_LC_H_ID, pREVISE: 0 }, { inherit: false, reload: true })
                    }
                });
            }
        };



        vm.reviseAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                var LC_VAL = 0;

                //for (var i = 0; i < vm.exportOrderList.data().length; i++) {
                //    var obj = vm.exportOrderList.data()[i];
                //    LC_VAL = LC_VAL + obj.TTL_VALUE;
                //}
                //if (parseInt(LC_VAL) != parseInt(vm.form.LC_TOT_VAL) && vm.form.IS_ADV_LC != 'Y') {
                //    config.appToastMsg("MULTI-005 LC value mismatch with PO value!!! " + LC_VAL);
                //    return;
                //}

                if (parseInt(vm.form.LC_TOT_VAL) != parseInt(vm.form.PO_TOT_VAL) && vm.form.IS_ADV_LC != 'Y' && vm.form.IS_IGNOR_DIFF != 'Y') {
                    config.appToastMsg("MULTI-005 LC value mismatch with PO value!!! " + vm.form.LC_TOT_VAL);
                    return;
                }

                data["CM_NOTIFY_PARTY_LST"] = !data.CM_NOTIFY_PARTY_LST ? '0' : data.CM_NOTIFY_PARTY_LST.join(',');
                data["DSG_PORT_LST"] = !data.DSG_PORT_LST ? '0' : data.DSG_PORT_LST.join(',');
                data["IS_LC_ADJ"] = 'N';
                data["XML_PO"] = CmrDataService.xmlStringShort(vm.exportOrderList.data().map(function (o) {
                    return {
                        CM_EXP_LC_D_PO_ID: o.CM_EXP_LC_D_PO_ID == null ? 0 : o.CM_EXP_LC_D_PO_ID,
                        CM_EXP_LC_H_ID: o.CM_EXP_LC_H_ID == null ? 0 : o.CM_EXP_LC_H_ID,
                        MC_ORDER_H_ID: o.MC_ORDER_H_ID,
                        MC_STYLE_D_ITEM_ID: o.MC_STYLE_D_ITEM_ID == null ? 0 : o.MC_STYLE_D_ITEM_ID,
                        MC_COLOR_ID: o.MC_COLOR_ID == null ? 0 : o.MC_COLOR_ID,
                        MC_SIZE_LST: o.MC_SIZE_LST == null ? '' : o.MC_SIZE_LST,
                        ORDER_QTY: o.ORDER_QTY == null ? 0 : o.ORDER_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 1 : o.QTY_MOU_ID == 0 ? 1 : o.QTY_MOU_ID
                    }
                }));

                var id = vm.form.CM_EXP_LC_H_ID;

                return CmrDataService.saveDataByUrl(data, '/ExportLCSalesContact/Update').then(function (res) {

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
                        vm.form.CM_EXP_LC_H_ID = res.data.OPCM_EXP_LC_H_ID;
                        $state.go($state.current, { pCM_EXP_LC_H_ID: res.data.OPCM_EXP_LC_H_ID, pREVISE: 0 }, { inherit: false, reload: true })
                    }
                });
            }
        };


    }

})();