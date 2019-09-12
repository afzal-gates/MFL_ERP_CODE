(function () {
    'use strict';
    angular.module('multitex.purchase').controller('SupplierProfileController', ['$q', 'config', 'PurchaseDataService', '$stateParams', '$state', '$scope', 'formData', 'Dialog', '$http', '$modal', 'commonDataService', SupplierProfileController]);
    function SupplierProfileController($q, config, PurchaseDataService, $stateParams, $state, $scope, formData, Dialog, $http, $modal, commonDataService) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.form = formData.SCM_SUPPLIER_ID ? formData : { IS_ACTIVE: 'Y' };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetCountryList(), GetContactTypeList(), GetSupplierTypeList(), GetSupplierOfficeTypeList(), GetItemCategoryList(), GetCertificatesTypeList(), brandList(), addressList(), contactList(), getBKAccTypeList(), getCurrencyList(), supBankInfoList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        vm.file = {};

        vm.officeType = [{ "LOCAL_ID": "L", "TYPE": "LOCAL" }, { "LOCAL_ID": "F", "TYPE": "FOREGIN" }];

        function GetCertificatesTypeList() {
            PurchaseDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetCertificateList?pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0)).then(function (res) {
                vm.certificatesType = res;
            });
        }

        vm.CountryAssign = function () {

            if (vm.form.IS_LOCAL == "L") {
                vm.form.HR_COUNTRY_ID = 1;
                vm.address.HR_COUNTRY_ID = 1;
            }
            else {

                vm.form.HR_COUNTRY_ID = '';
                vm.address.HR_COUNTRY_ID = '';
            }
        }

        vm.brand = {};
        vm.form = {};

        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    PurchaseDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {

                        e.success([{
                            BRAND_NAME_EN: '--New Brand--',
                            RF_BRAND_ID: -1
                        }].concat(res));

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.bankListData = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    commonDataService.getBankListData().then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

        vm.bankBranchListData = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    commonDataService.getBranchListData(0).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });


        function supBankInfoList() {
            vm.bankInfoList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetSupplierBankList?pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function brandList() {
            vm.brandList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetSupplierBrandList?pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    model: {
                        id: "SCM_SUPPLIER_ID",
                        fields: {
                            BRAND_NAME_EN: { editable: false },
                            ITEM_CAT_NAME_EN: { editable: false },
                        },
                    }
                }
            });
        };

        function addressList() {
            vm.addressList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetSupplierAddressList?pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0)).then(function (res) {
                            e.success(res);
                            var xx = _.filter(res, { 'IS_DEFAULT': 'Y' })[0];
                            vm.address = xx ? xx : { 'IS_ACTIVE': 'Y' };
                            if (!vm.address)
                                vm.HRCOUNTRYID();
                        });
                    }
                }
            });
        };

        function contactList() {
            vm.contactList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetSupplierContactList?pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0)).then(function (res) {
                            e.success(res);
                            var xx = _.filter(res, { 'IS_DEFAULT': 'Y' })[0];
                            vm.contact = xx ? xx : { 'IS_ACTIVE': 'Y' };
                        });
                    }
                }
            });
        };


        vm.SupplierOfficeName = function () {
            vm.address.SUP_OFFICE_NAME = vm.form.SUP_TRD_NAME_EN;
            vm.form.SUP_OWNER_NAME = vm.form.SUP_TRD_NAME_EN;
            if (vm.bankInfo)
                vm.bankInfo.BK_ACC_TITLE = vm.form.SUP_TRD_NAME_EN;
        };

        vm.OfficeNameContact = function () {
            vm.contact.SCM_SUP_OFFICE = vm.address.SUP_OFFICE_NAME;
        };

        vm.HRCOUNTRYID = function () {
            vm.address.HR_COUNTRY_ID = vm.form.HR_COUNTRY_ID;
        };

        vm.WORKPHONE = function () {
            vm.contact.CP_WORK_PHONE = vm.address.WORK_PHONE;
        };

        vm.MOBPHONE = function () {
            vm.contact.CP_MOB_PHONE = vm.address.MOB_PHONE;
        };

        vm.newBankAdd = function () {
            var ary = {};
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/GlobalUI/BankEntry',
                controller: function ($modalInstance, $scope, $timeout, PurchaseDataService) {
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
                                        PurchaseDataService.getDataByFullUrl('/api/mrc/buyer/GetCountryList').then(function (res) {
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
                                    PurchaseDataService.getDataByFullUrl('/api/common/BankDataList').then(function (res) {
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

                        return PurchaseDataService.saveDataByFullUrl(data, '/api/common/BankSave', token).then(function (res) {
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
                console.log(ary);
                vm.bankListData = data;
                vm.bankInfo.RF_BANK_ID = ary.RF_BANK_ID;

                vm.branchListData = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            commonDataService.getBranchListData(ary.RF_BANK_ID).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });


            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

        vm.branchListData = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    commonDataService.getBranchListData(0).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

        vm.selectBranch = function (e) {
            var itemss = e.sender.dataItem(e.item);

            if (itemss.RF_BANK_ID > 0) {
                vm.branchListData = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            commonDataService.getBranchListData(itemss.RF_BANK_ID).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }
        }

        vm.selectBranchAddress = function (e) {
            var itemss = e.sender.dataItem(e.item);

            if (itemss.RF_BANK_BRANCH_ID > 0) {
                var list = _.filter(vm.branchListData.data(), { 'RF_BANK_BRANCH_ID': parseInt(itemss.RF_BANK_BRANCH_ID) })[0];

                vm.bankInfo.BRANCH_ADDRESS_EN = list.BRANCH_ADDRESS_EN;
                vm.bankInfo.ROUTING_NO = list.ROUTING_NO;
                vm.bankInfo.SWIFT_CODE_EXT = list.SWIFT_CODE_EXT;

                var listBank = _.filter(vm.bankListData.data(), { 'RF_BANK_ID': parseInt(vm.bankInfo.RF_BANK_ID) })[0];
                vm.bankInfo.SWIFT_CODE = listBank.SWIFT_CODE;
            }
            else {
                vm.bankInfo.BRANCH_ADDRESS_EN = "";
                vm.bankInfo.ROUTING_NO = "";
                vm.bankInfo.SWIFT_CODE_EXT = "";
                vm.bankInfo.SWIFT_CODE = "";
            }

        }

        vm.newBankBranchAdd = function (BankID) {
            var aryBr = {};
            //alert(BankID);
            var SWIFT_CODE = "";
            if (vm.bankListData) {
                var listBank = _.filter(vm.bankListData.data(), { 'RF_BANK_ID': parseInt((BankID || 0)) })[0];
                if (listBank)
                    SWIFT_CODE = listBank.SWIFT_CODE;
            }

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/GlobalUI/BankBranchEntry',
                controller: function ($modalInstance, $scope, $timeout, PurchaseDataService, commonDataService) {
                    var vm = this;

                    vm.errors = [];
                    //GetBankList()
                    BankBranchDataList()
                    GetCountryList();
                    vm.bankList = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                commonDataService.getBankListData().then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    });



                    //function GetBankList() {
                    //    return vm.bankList = {
                    //        optionLabel: "-- Select Bank --",
                    //        filter: "contains",
                    //        autoBind: true,
                    //        dataSource: {
                    //            transport: {
                    //                read: function (e) {
                    //                    PurchaseDataService.getDataByFullUrl('/api/common/BankDataList').then(function (res) {
                    //                        e.success(res);
                    //                    });
                    //                }
                    //            }
                    //        },
                    //        dataTextField: "BANK_NAME_EN",
                    //        dataValueField: "RF_BANK_ID"
                    //    };
                    //};

                    vm.getBankSwiftCode = function (e) {

                        var itemss = e.sender.dataItem(e.item);

                        var listBank = _.filter(vm.bankList.data(), { 'RF_BANK_ID': parseInt(itemss.RF_BANK_ID) })[0];
                        vm.form.SWIFT_CODE = listBank.SWIFT_CODE;
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
                                        PurchaseDataService.getDataByFullUrl('/api/mrc/buyer/GetCountryList').then(function (res) {
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
                                    PurchaseDataService.getDataByFullUrl('/api/common/GetBankBranchList').then(function (res) {
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

                        return PurchaseDataService.saveDataByFullUrl(data, '/api/common/BankBranchSave', token).then(function (res) {
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


                },
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'large-Modal',

            });

            modalInstance.result.then(function (data) {
                var list = _.filter(data.data(), { 'RF_BANK_ID': parseInt(aryBr.RF_BANK_ID) })
                vm.branchListData.data(list);
                //vm.branchListData = new kendo.data.dataSource({
                //    data: list
                //});

                vm.bankInfo.RF_BANK_BRANCH_ID = aryBr.RF_BANK_BRANCH_ID;
                if (vm.bankInfo.RF_BANK_ID > 0)
                    console.log("Pls select a bank")
                else {
                    if (aryBr.RF_BANK_BRANCH_ID > 0) {
                        vm.bankInfo.RF_BANK_ID = aryBr.RF_BANK_ID;
                        var listAd = _.filter(data, { 'RF_BANK_BRANCH_ID': parseInt(aryBr.RF_BANK_BRANCH_ID) })[0];
                        vm.bankInfo.BRANCH_ADDRESS_EN = listAd.BRANCH_ADDRESS_EN;
                        vm.bankInfo.ROUTING_NO = listAd.ROUTING_NO;
                        vm.bankInfo.SWIFT_CODE_EXT = listAd.SWIFT_CODE_EXT;

                        var listBank = _.filter(vm.bankListData.data(), { 'RF_BANK_ID': parseInt(aryBr.RF_BANK_ID) })[0];
                        vm.bankInfo.SWIFT_CODE = listBank.SWIFT_CODE;
                    }

                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

        vm.newBrandEntry = function (e) {
            var itemss = e.sender.dataItem(e.item);
            var aryB = {};
            if (itemss.RF_BRAND_ID == -1) {
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '../../GlobalUI/BrandEntry',
                    controller: function ($modalInstance, $scope, $timeout, PurchaseDataService) {
                        var vm = this;
                        vm.form = {};
                        vm.errors = [];
                        BrandGetAll()
                        function BrandGetAll() {

                            PurchaseDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                                //PurchaseDataService.getDataByFullUrl('/api/common/GetCategoryWiseBrandList/' + ((vm.brand.INV_ITEM_CAT_ID == '-- Select Item Category--' ? 0 : vm.brand.INV_ITEM_CAT_ID) || 0)).then(function (res) {
                                vm.brandAllList = new kendo.data.DataSource({
                                    data: res
                                });
                            });
                        };

                        vm.gridBrand = {
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

                                { field: "RF_BRAND_ID", hidden: true },
                                { field: "BRAND_NAME_EN", title: "Brand Name (EN)", width: "30%" },
                                { field: "BRAND_NAME_BN", title: "Brand Name (BN)", width: "30%" },
                                { field: "IS_ACTIVE", title: "Is Active", width: "20%" },
                                {
                                    title: "",
                                    template: '<a title="Edit" ng-click="vm.editBrand(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                                    width: "20%"
                                }
                            ],
                            //editable: true

                        };

                        vm.form['BRAND_NAME_EN'] = '';
                        vm.formDisabled = false;


                        vm.cancel = function (data) {
                            aryB = data;
                            $modalInstance.close(vm.brandAllList);
                        };

                        vm.editBrand = function (data) {
                            vm.form = angular.copy(data);
                        }

                        vm.removeBrand = function (data) {

                            if (!data.RF_BRAND_ID || data.RF_BRAND_ID <= 0) {
                                vm.brandAllList.remove(data);
                                return;
                            };

                            Dialog.confirm('Removing "' + data.BRAND_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                                 .then(function () {
                                     vm.brandAllList.remove(data);
                                 });

                        }

                        vm.resetBrandInfo = function () {
                            vm.form = {};
                        };

                        vm.submitDataNewBrand = function (dataOri, token) {
                            var data = angular.copy(dataOri);

                            return PurchaseDataService.saveDataByFullUrl(data, '/api/common/BrandSave', token).then(function (res) {
                                if (res.success === false) {
                                    vm.errors = res.errors;
                                }
                                else {
                                    res['data'] = angular.fromJson(res.jsonStr);
                                    config.appToastMsg(res.data.PMSG);

                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                        vm.form['RF_BRAND_ID'] = parseInt(res.data.opRF_BRAND_ID);
                                    }
                                    BrandGetAll();
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
                    vm.categoryBrandList = data;
                    vm.brand.RF_BRAND_ID = aryB.RF_BRAND_ID;

                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });
            }
        }

        function GetItemCategoryList() {

            PurchaseDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var filter = _.filter(res, { 'ITEM_CAT_LEVEL': 1 });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: filter
                });
            });
        }

        vm.submitAddressData = function (isValid) {

            if (fnValidateSub() == true) {

                if (vm.address.uid) {
                    // Update
                    var def = _.filter(vm.addressList.data(), function (o) {
                        return (o.IS_DEFAULT === "Y" && o.uid != vm.address.uid)
                    }).length;

                    if (vm.address.IS_DEFAULT == "Y" && def > 0) {
                        config.appToastMsg("MULTI-005 Multiple default contact exists!");
                        return;
                    }

                    var row = vm.addressList.getByUid(vm.address.uid);
                    var count = _.filter(vm.addressList.data(), function (o) {
                        return o.SUP_OFFICE_NAME === vm.address.SUP_OFFICE_NAME
                    }).length;

                    if (count <= 1) {

                        if (vm.address.IS_DEFAULT) {
                            vm.form.ADDRESS_DEFA = vm.address.ADDRESS_EN;
                        }

                        row["SCM_SUP_ADDRESS_ID"] = vm.address.SCM_SUP_ADDRESS_ID;
                        row["SCM_SUPPLIER_ID"] = vm.address.SCM_SUPPLIER_ID;
                        row["LK_OFF_TYPE_ID"] = vm.address.LK_OFF_TYPE_ID;
                        row["SUP_OFFICE_NAME"] = vm.address.SUP_OFFICE_NAME;
                        row["ADDRESS_EN"] = vm.address.ADDRESS_EN;
                        row["ADDRESS_BN"] = vm.address.ADDRESS_BN;
                        row["CITY_NAME"] = vm.address.CITY_NAME;
                        row["PO_BOX_NO"] = vm.address.PO_BOX_NO;
                        row["HR_COUNTRY_ID"] = vm.address.HR_COUNTRY_ID;
                        row["WORK_PHONE"] = vm.address.WORK_PHONE;
                        row["MOB_PHONE"] = vm.address.MOB_PHONE;
                        row["FAX_NO"] = vm.address.FAX_NO;
                        row["EMAIL_ID"] = vm.address.EMAIL_ID;
                        row["WEB_URL"] = vm.address.WEB_URL;
                        row["IS_DEFAULT"] = vm.address.IS_DEFAULT;
                        row["IS_ACTIVE"] = vm.address.IS_ACTIVE;
                        row["POST_CODE"] = vm.address.POST_CODE;
                        config.appToastMsg("Supplier address information has been updated successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate office name exists!");
                    }

                } else {
                    //insert

                    var def = _.filter(vm.addressList.data(), function (o) {
                        return o.IS_DEFAULT === "Y"
                    }).length;
                    if (vm.address.IS_DEFAULT == "Y" && def > 0) {
                        config.appToastMsg("MULTI-005 Multiple default address exists!");
                        return;
                    }

                    var count = _.filter(vm.addressList.data(), function (o) {
                        return o.SUP_OFFICE_NAME === vm.address.SUP_OFFICE_NAME
                    }).length;

                    if (count == 0) {
                        vm.contact.SUP_OFFICE_NAME = vm.address.SUP_OFFICE_NAME;

                        var idx = vm.addressList.data().length + 1;
                        vm.addressList.insert(idx, vm.address);

                        if (vm.address.IS_DEFAULT) {
                            vm.form.ADDRESS_DEFA = vm.address.ADDRESS_EN;
                        }

                        var gview = vm.addressList.data();
                        var selectedItem = gview[0];
                        if (selectedItem.ADDRESS_EN == null)
                            vm.addressList.remove(selectedItem);
                        config.appToastMsg("Supplier address information has been added successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate office name exists!");
                    }
                }

                //vm.address = {};
            }
        };

        vm.removeAddressData = function (data) {

            if (!data.SCM_SUP_ADDRESS_ID || data.SCM_SUP_ADDRESS_ID <= 0) {
                vm.addressList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ADDRESS_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.addressList.remove(data);
                 });

        }

        vm.submitBrandData = function (isValid) {
            if (fnValidateSub4() == true) {
                var itemCat = _.filter(vm.itemCategoryList.data(), function (o) {
                    return o.INV_ITEM_CAT_ID === parseInt(vm.brand.INV_ITEM_CAT_ID)
                })[0];

                var itemBrand = _.filter(vm.categoryBrandList.data(), function (o) {
                    return o.RF_BRAND_ID === parseInt(vm.brand.RF_BRAND_ID)
                })[0];

                vm.brand.ITEM_CAT_NAME_EN = itemCat.ITEM_CAT_NAME_EN;
                vm.brand.BRAND_NAME_EN = itemBrand.BRAND_NAME_EN;

                if (vm.brand.uid) {
                    // Update
                    var row = vm.brandList.getByUid(vm.brand.uid);
                    var count = _.filter(vm.brandList.data(), function (o) {
                        return o.RF_BRAND_ID === vm.brand.RF_BRAND_ID && o.INV_ITEM_CAT_ID === vm.brand.INV_ITEM_CAT_ID
                    }).length;

                    if (count <= 1) {
                        row["SCM_SUP_BRAND_ID"] = vm.brand.SCM_SUP_BRAND_ID;
                        row["SCM_SUPPLIER_ID"] = vm.brand.SCM_SUPPLIER_ID;
                        row["RF_BRAND_ID"] = vm.brand.RF_BRAND_ID;
                        row["INV_ITEM_CAT_ID"] = vm.brand.INV_ITEM_CAT_ID;
                        row["ITEM_CAT_NAME_EN"] = vm.brand.ITEM_CAT_NAME_EN;
                        row["BRAND_NAME_EN"] = vm.brand.BRAND_NAME_EN;
                        config.appToastMsg("Supplier brand has been updated successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate brand name exists!");
                    }

                } else {
                    //insert
                    var count = _.filter(vm.brandList.data(), function (o) {
                        return o.RF_BRAND_ID === vm.brand.RF_BRAND_ID && o.INV_ITEM_CAT_ID === vm.brand.INV_ITEM_CAT_ID
                    }).length;

                    if (count == 0) {
                        var idx = vm.brandList.data().length + 1;
                        vm.brandList.insert(idx, vm.brand);

                        //var gview = vm.brandList.data();
                        //var selectedItem = gview[0];
                        //if (selectedItem.ITEM_CAT_NAME_EN == null)
                        //    vm.brandList.remove(selectedItem);
                        config.appToastMsg("Supplier brand has been added successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate brand name exists!");
                    }
                }
                //vm.brand = {};
            }
        };

        vm.removeBrandData = function (data) {

            if (!data.SCM_SUPPLIER_ID || data.SCM_SUPPLIER_ID <= 0) {
                vm.brandList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.BRAND_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.brandList.remove(data);
                 });

        }


        vm.submitContactData = function (isValid) {
            if (fnValidateSub2() == true) {

                if (vm.contact.uid) {
                    // Update
                    var def = _.filter(vm.contactList.data(), function (o) {
                        return (o.IS_DEFAULT === "Y" && o.uid != vm.contact.uid)
                    }).length;

                    if (vm.contact.IS_DEFAULT == "Y" && def > 0) {
                        config.appToastMsg("MULTI-005 Multiple default contact exists!");
                        return;
                    }
                    var row = vm.contactList.getByUid(vm.contact.uid);
                    var count = _.filter(vm.contactList.data(), function (o) {
                        return o.CP_NAME_EN === vm.contact.CP_NAME_EN
                    }).length;

                    if (count <= 1) {
                        row['SCM_SUP_CP_ID'] = vm.contact.SCM_SUP_CP_ID;
                        row['SCM_SUP_ADDRESS_ID'] = vm.contact.SCM_SUP_ADDRESS_ID;
                        row['LK_CP_TYPE_ID'] = vm.contact.LK_CP_TYPE_ID;
                        row['CP_WORK_PHONE'] = vm.contact.CP_WORK_PHONE;
                        row['CP_MOB_PHONE'] = vm.contact.CP_MOB_PHONE;
                        row['CP_EMAIL_ID'] = vm.contact.CP_EMAIL_ID;
                        row['IS_DEFAULT'] = vm.contact.IS_DEFAULT;
                        row['CP_NAME_EN'] = vm.contact.CP_NAME_EN;
                        row['IS_ACTIVE'] = vm.contact.IS_ACTIVE;
                        row['SUP_OFFICE_NAME'] = vm.contact.SUP_OFFICE_NAME;
                        row['CP_DESIG'] = vm.contact.CP_DESIG;
                        config.appToastMsg("Supplier contact information has been updated successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate contact name exists!");
                    }

                } else {
                    //insert
                    var def = _.filter(vm.contactList.data(), function (o) {
                        return o.IS_DEFAULT === "Y"
                    }).length;
                    if (vm.contact.IS_DEFAULT == "Y" && def > 0) {
                        config.appToastMsg("MULTI-005 Multiple default contact exists!");
                        return;
                    }

                    var count = _.filter(vm.contactList.data(), function (o) {
                        return o.CP_NAME_EN === vm.contact.CP_NAME_EN
                    }).length;

                    if (count == 0) {
                        if (vm.address.SUP_OFFICE_NAME != null || vm.contact.SUP_OFFICE_NAME != null) {
                            vm.contact.SUP_OFFICE_NAME = vm.address.SUP_OFFICE_NAME;
                            var idx = vm.contactList.data().length + 1;
                            vm.contactList.insert(idx, vm.contact);

                            var gview = vm.contactList.data();
                            var selectedItem = gview[0];
                            if (selectedItem.CP_NAME_EN == null)
                                vm.contactList.remove(selectedItem);
                            config.appToastMsg("Supplier contact information has been added successfully!");
                        }
                        else {
                            config.appToastMsg("MULTI-005 Please select a office name");
                        }
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate contact name exists!");
                    }
                }

            }
        };

        vm.removeContactData = function (data) {

            if (!data.SCM_SUP_CP_ID || data.SCM_SUP_CP_ID <= 0) {
                vm.contactList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.CP_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.contactList.remove(data);
                 });
        }

        vm.submitBankData = function (data) {
            if (fnValidateSub3() == true) {
                var count = _.filter(vm.bankInfoList.data(), function (o) {
                    return (o.IS_DEFAULT === 'Y' && o.uid != data.uid)
                }).length;

                if (count > 0 && data.IS_DEFAULT == 'Y') {
                    config.appToastMsg("MULTI-005 Multiple default bank a/c exists!");
                    return;
                }

                data.SCM_SUPPLIER_ID = vm.form.SCM_SUPPLIER_ID;

                return PurchaseDataService.saveDataByUrl(data, '/SupplierProfile/SaveSupplierBankData').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        supBankInfoList();
                        vm.bankInfo = { 'RF_BANK_ID': '', 'RF_BANK_BRANCH_ID': '', 'RF_CURRENCY_ID': '', 'LK_ACC_TYPE_ID': '', 'IS_AC_PAYABLE': 'Y', 'IS_ACTIVE': 'Y' };
                    }
                });

                if (vm.bankInfo.uid) {

                }
                else {

                }
            }
        }

        vm.resetBankData = function () {
            vm.bankInfo = { 'RF_BANK_ID': '', 'RF_BANK_BRANCH_ID': '', 'RF_CURRENCY_ID': '', 'LK_ACC_TYPE_ID': '', 'IS_AC_PAYABLE': 'Y', 'IS_ACTIVE': 'Y' };
        }

        vm.resetAddress = function () {

            vm.address = {};
            vm.address['HR_COUNTRY_ID'] = vm.form.HR_COUNTRY_ID;
            vm.address['LK_OFF_TYPE_ID'] = '';
            vm.address['IS_ACTIVE'] = 'Y';

        };

        vm.resetContact = function () {
            vm.contact = {};
            vm.contact['LK_CP_TYPE_ID'] = '';
            vm.contact['IS_ACTIVE'] = 'Y';
        };

        vm.resetBrand = function () {
            //GetItemCategoryList();
            //GetBrandList();
            vm.brand['uid'] = '';
            vm.brand['INV_ITEM_CAT_ID'] = '-- Select Item Category--';
            vm.brand['RF_BRAND_ID'] = '-- Select Brand --';

        };

        function removeEmpty() {
            if (vm.addressList != null && vm.addressList.data() > 0) {
                var gview = vm.addressList.data();
                var selectedItem = gview[0];
                if (selectedItem.ADDRESS_EN == null)
                    vm.addressList.remove(selectedItem);
            }
            if (vm.contactList != null && vm.contactList.data() > 0) {
                var gviewC = vm.contactList.data();
                var selectedItemC = gviewC[0];
                if (selectedItemC.CP_NAME_EN == null)
                    vm.contactList.remove(selectedItemC);
            }
        }


        vm.editAddressData = function (data) {
            vm.address = angular.copy(data);
        }

        vm.editContactData = function (data) {
            vm.contact = angular.copy(data);;
        }

        vm.editBrandData = function (data) {
            vm.brand = angular.copy(data);
            GetBrandList();
        }

        vm.editBankInfoData = function (data) {
            vm.branchListData = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        commonDataService.getBranchListData(data.RF_BANK_ID).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
            vm.bankInfo = angular.copy(data);

        }

        vm.reset = function () {

            $state.go('SupplierMaster', { pSCM_SUPPLIER_ID: 0 });

        };

        if (formData['SCM_SUPPLIER_ID']) {
            //formData['INV_ITEM_CAT_ID_LST'] = formData.INV_ITEM_CAT_ID_LST ? formData.INV_ITEM_CAT_ID_LST.split(',') : [];
            vm.form = formData;
            if (!vm.bankInfo) {
                vm.bankInfo = {};
                vm.bankInfo.BK_ACC_TITLE = vm.form.SUP_TRD_NAME_EN;
                vm.bankInfo.IS_AC_PAYABLE = "Y";
                vm.bankInfo.IS_ACTIVE = "Y";
            }
            else {
                vm.bankInfo.BK_ACC_TITLE = vm.form.SUP_TRD_NAME_EN;
                vm.bankInfo.IS_ACTIVE = "Y";
            }
        } else {
            vm.form = { INV_ITEM_CAT_ID_LST: [], IS_ACTIVE: 'Y' }
            vm.address = { IS_ACTIVE: 'Y' }
            vm.contact = { IS_ACTIVE: 'Y' }
        }

        //  DropdownList


        function GetSupplierTypeList() {
            return vm.supplierTypeList = {
                optionLabel: "-- Select Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.LookupListData(82).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetSupplierOfficeTypeList() {
            return vm.supplierOfficeTypeList = {
                optionLabel: "-- Select Office Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.LookupListData(85).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetContactTypeList() {
            return vm.contactTypeList = {
                optionLabel: "-- Select Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.LookupListData(83).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };
        function GetCountryList() {
            return vm.CountryList = {
                optionLabel: "-- Select Country --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/mrc/buyer/GetCountryList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };
        };

        function getBKAccTypeList() {
            return vm.bankAcTypeList = {
                optionLabel: "-- Select A/C Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getLookupList(22).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getCurrencyList() {
            return vm.currencyList = {
                optionLabel: "-- Select Currency --",
                filter: "startswith",
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
            height: '150px',
            scrollable: true,
            columns: [
                { field: "SCM_SUP_ADDRESS_ID", hidden: true },
                { field: "SCM_SUPPLIER_ID", hidden: true },
                { field: "LK_OFF_TYPE_ID", hidden: true },
                { field: "SUP_OFFICE_NAME", title: "Office Name", width: "25%" },
                { field: "ADDRESS_EN", title: "Address", width: "25%" },
                { field: "ADDRESS_BN", title: "ADDRESS(EN)", hidden: true },
                { field: "CITY_NAME", hidden: true },
                { field: "PO_BOX_NO", hidden: true },
                { field: "HR_COUNTRY_ID", hidden: true },
                { field: "WORK_PHONE", hidden: true },
                { field: "MOB_PHONE", hidden: true },
                { field: "FAX_NO", hidden: true },
                { field: "EMAIL_ID", hidden: true },
                { field: "WEB_URL", hidden: true },
                { field: "IS_DEFAULT", hidden: true },
                { field: "IS_ACTIVE", hidden: true },
                { field: "POST_CODE", title: "Post Code", width: "20%" },
                { field: "IS_DEFAULT", title: "Default", width: "15%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeAddressData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a> <a  title="Edit" ng-click="vm.editAddressData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "15%"
                }
            ],
            //editable: true
        };

        vm.gridOptionsContact = {
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
            height: '150px',
            scrollable: true,
            columns: [

                { field: "SCM_SUP_CP_ID", hidden: true },
                { field: "SCM_SUP_ADDRESS_ID", hidden: true },
                { field: "LK_CP_TYPE_ID", hidden: true },
                { field: "CP_NAME_BN", hidden: true },
                { field: "CP_WORK_PHONE", hidden: true },
                { field: "CP_MOB_PHONE", hidden: true },
                { field: "CP_EMAIL_ID", hidden: true },
                { field: "IS_DEFAULT", hidden: true },
                { field: "IS_ACTIVE", hidden: true },
                { field: "SUP_OFFICE_NAME", title: "Office Name", width: "20%" },
                { field: "CP_NAME_EN", title: "Contact Name", width: "30%" },
                { field: "CP_DESIG", title: "Designation", width: "20%" },
                { field: "IS_DEFAULT", title: "Default", width: "15%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeContactData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a> <a  title="Edit" ng-click="vm.editContactData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "15%"
                }
            ],
            //editable: true
        };

        vm.gridOptionsBrand = {
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
            height: '130px',
            scrollable: true,
            groupable: false,
            columns: [

                { field: "SCM_SUP_BRAND_ID", hidden: true },
                { field: "SCM_SUPPLIER_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },
                { field: "ITEM_CAT_NAME_EN", title: "Item Category Name", width: "40%" },
                { field: "BRAND_NAME_EN", title: "Brand Name", width: "40%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeBrandData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a> <a  title="Edit" ng-click="vm.editBrandData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "20%"
                }
            ],
            //editable: true
        };

        vm.gridOptionsBank = {
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
            height: '150px',
            scrollable: true,
            columns: [
                { field: "RF_BANK_ID", hidden: true },
                { field: "RF_BANK_BRANCH_ID", hidden: true },
                { field: "ACC_BK_ACCOUNT_ID", hidden: true },
                { field: "SCM_SUP_ADDRESS_ID", hidden: true },
                { field: "SCM_SUPPLIER_ID", hidden: true },
                { field: "LK_OFF_TYPE_ID", hidden: true },
                { field: "BANK_ACC_TITLE", hidden: true },
                { field: "BANK_CURRENCY", hidden: true },
                { field: "BANK_ACC_TYPE", hidden: true },
                { field: "IS_ACTIVE", hidden: true },
                //{ field: "IS_DEFAULT", hidden: true },
                { field: "BRANCH_ADDRESS_EN", hidden: true },

                { field: "BANK_NAME_EN", title: "Bank Name", width: "20%", headerTemplate: "<b>Bank Name</b>" },
                { field: "BANK_BRANCH_NAME_EN", title: "Branch Name", width: "20%", headerTemplate: "<b>Branch Name</b>" },
                //{ field: "BRANCH_ADDRESS", title: "Address", width: "15%", headerTemplate: "<b>Address</b>" },
                { field: "SWIFT_CODE", title: "Swift Code", width: "12%", headerTemplate: "<b>Swift Code</b>" },
                { field: "SWIFT_CODE_EXT", title: "Swift Code Ext.", width: "10%", headerTemplate: "<b>Ext.</b>" },
                { field: "BK_ACC_NO", title: "Bank A/C No", width: "15%", headerTemplate: "<b>Bank A/C No</b>" },
                //{ field: "BANK_ACC_TITLE", title: "A/C Title", width: "10%", headerTemplate: "<b>A/C Title</b>" },
                //{ field: "BANK_CURRENCY", title: "Currency", width: "5%", headerTemplate: "<b>Currency</b>" },
                //{ field: "BANK_ACC_TYPE", title: "A/C Type", width: "5%", headerTemplate: "<b>A/C Type</b>" },
                { field: "IS_ACTIVE", title: "Active", width: "8%", headerTemplate: "<b>Active</b>" },
                { field: "IS_DEFAULT", title: "Default", width: "8%", headerTemplate: "<b>Default</b>" },
                //{ field: "IS_ACTIVE", title: "Active", width: "5%", headerTemplate: "<b>Active</b>" },
                {
                    title: "",
                    template: '<a  title="Edit" ng-click="vm.editBankInfoData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "7%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri, isValid) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                //var dataCT = angular.copy(vm.certificatesType);

                if (!isValid) {
                    $scope.formData.$submitted = true;
                    return;
                }

                removeEmpty();

                //dataCT = _.filter(dataCT, function (o) {
                //    return o.IS_EXISTS === "Y"
                //});

                data["XML_STR_CERT"] = "<trans></trans>";
                data["INV_ITEM_CAT_ID_LST"] = "";

                var lst = _.filter(vm.addressList.data(), function (x) { return x.IS_DEFAULT === 'Y' });

                if (lst.length == 0) {
                    config.appToastMsg("MULTI-005 Please select default supplier address!!!");
                    return;
                }

                //data["XML_STR_CERT"] = PurchaseDataService.xmlStringShort(dataCT.map(function (o) {
                //    var xx = new Date().getTime();
                //    return {
                //        RF_AUD_CERT_TYPE_ID: o.RF_AUD_CERT_TYPE_ID == null ? 0 : o.RF_AUD_CERT_TYPE_ID,
                //        SCM_SUPPLIER_ID: o.SCM_SUPPLIER_ID == null ? 0 : o.SCM_SUPPLIER_ID,
                //        DOC_PATH_FILE: o.DOC_PATH_FILE,
                //        DOC_PATH: (xx + '(' + o.CERT_TYPE_TITLE + ')')
                //    }
                //})
                //    );

                //data['INV_ITEM_CAT_ID_LST'] = !data.INV_ITEM_CAT_ID_LST ? '0' : data.INV_ITEM_CAT_ID_LST.join(',');

                data["XML_STR_Address"] = PurchaseDataService.xmlStringShort(vm.addressList.data().map(function (o) {

                    return {
                        SCM_SUP_ADDRESS_ID: o.SCM_SUP_ADDRESS_ID == null ? 0 : o.SCM_SUP_ADDRESS_ID,
                        SCM_SUPPLIER_ID: o.SCM_SUPPLIER_ID == null ? 0 : o.SCM_SUPPLIER_ID,
                        LK_OFF_TYPE_ID: o.LK_OFF_TYPE_ID,
                        SUP_OFFICE_NAME: o.SUP_OFFICE_NAME,
                        ADDRESS_EN: o.ADDRESS_EN,
                        ADDRESS_BN: o.ADDRESS_BN,
                        CITY_NAME: o.CITY_NAME,
                        POST_CODE: o.POST_CODE == null ? '0' : o.POST_CODE,
                        PO_BOX_NO: o.PO_BOX_NO,
                        HR_COUNTRY_ID: o.HR_COUNTRY_ID,
                        WORK_PHONE: o.WORK_PHONE,
                        MOB_PHONE: o.MOB_PHONE,
                        FAX_NO: o.FAX_NO,
                        EMAIL_ID: o.EMAIL_ID,
                        WEB_URL: o.WEB_URL,
                        IS_DEFAULT: o.IS_DEFAULT == null ? "Y" : o.IS_DEFAULT,
                        IS_ACTIVE: o.IS_ACTIVE == null ? "Y" : o.IS_ACTIVE
                    }
                })
                    );
                data["XML_STR_Contact"] = PurchaseDataService.xmlStringShort(vm.contactList.data().map(function (o) {

                    return {
                        SCM_SUP_ADDRESS_ID: o.SCM_SUP_ADDRESS_ID,
                        SUP_OFFICE_NAME: o.SUP_OFFICE_NAME,
                        CP_DESIG: o.CP_DESIG,
                        CP_EMAIL_ID: o.CP_EMAIL_ID,
                        CP_MOB_PHONE: o.CP_MOB_PHONE,
                        CP_NAME_BN: o.CP_NAME_BN,
                        CP_NAME_EN: o.CP_NAME_EN,
                        CP_WORK_PHONE: o.CP_WORK_PHONE,
                        CREATION_DATE: o.CREATION_DATE,
                        IS_ACTIVE: o.IS_ACTIVE == null ? "Y" : o.IS_ACTIVE,
                        IS_DEFAULT: o.IS_DEFAULT == null ? "Y" : o.IS_DEFAULT,
                        LK_CP_TYPE_ID: o.LK_CP_TYPE_ID,
                        SCM_SUP_CP_ID: o.SCM_SUP_CP_ID == null ? 0 : o.SCM_SUP_CP_ID

                    }

                })
                    );

                data["XML_STR_Brand"] = PurchaseDataService.xmlStringShort(vm.brandList.data().map(function (o) {

                    return {
                        SCM_SUPPLIER_ID: o.SCM_SUPPLIER_ID == null ? 0 : o.SCM_SUPPLIER_ID,
                        SCM_SUP_BRAND_ID: o.SCM_SUP_BRAND_ID == null ? 0 : o.SCM_SUP_BRAND_ID,
                        INV_ITEM_CAT_ID: o.INV_ITEM_CAT_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID
                    }

                })
                    );
                var id = vm.form.SCM_SUPPLIER_ID

                return PurchaseDataService.saveDataByUrl(data, '/SupplierProfile/SaveSupplierData').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go('SupplierMaster', { pSCM_SUPPLIER_ID: id });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (res.data.OPSCM_SUPPLIER_ID)
                                $state.go($state.current, { pSCM_SUPPLIER_ID: res.data.OPSCM_SUPPLIER_ID });
                        }

                    }
                });
            }
        };
    }


})();