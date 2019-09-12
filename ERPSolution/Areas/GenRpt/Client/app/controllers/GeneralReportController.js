(function () {
    'use strict';
    angular.module('multitex.cmr').controller('GeneralReportController', ['$q', 'config', 'GenRptDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', GeneralReportController]);
    function GeneralReportController($q, config, GenRptDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = { REPORT_CODE: '', IS_ACTIVE: 'Y' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetCountryList(), getBuyerListData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getBuyerListData() {
            return vm.buyerList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        GenRptDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 15
            });

        };

        function GetCountryList() {
            return vm.CountryList = {
                optionLabel: "-- Select Country --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            GenRptDataService.getDataByFullUrl('/api/mrc/buyer/GetCountryList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };
        };


        vm.getBuyerNotifyPartyList = function (data) {
            if (data.MC_BUYER_ID > 0) {
                vm.form.BUYER_NAME_EN = data.BUYER_NAME_EN;
                vm.form.MC_BUYER_ID = data.MC_BUYER_ID;
                GenRptDataService.getDataByFullUrl('/api/cmr/BuyerNotifyParty/GetBuyerNotifyPartyInfo?pMC_BUYER_ID=' + data.MC_BUYER_ID).then(function (res) {

                    vm.NotifyList = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
            else {
                vm.NotifyList = new kendo.data.DataSource({
                    data: []
                });

                vm.form = { HR_COUNTRY_ID: '', IS_ACTIVE: 'N' };
            }
        }

        vm.clearAll = function (data) {

            vm.form = { HR_COUNTRY_ID: '', MC_BUYER_ID: data.MC_BUYER_ID, BUYER_NAME_EN: data.BUYER_NAME_EN, IS_ACTIVE: 'Y' };
        }

        vm.editItemData = function (data) {
            vm.form = angular.copy(data);
        }

        vm.removeData = function (data) {

            if (!data.SCM_SC_QUOT_RATE_D_ID || data.SCM_SC_QUOT_RATE_D_ID <= 0) {
                vm.NotifyList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.FAB_PROC_NAME + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.NotifyList.remove(data);
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
            height: '200px',
            scrollable: true,
            columns: [

                { field: "CM_NOTIFY_PARTY_ID", hidden: true },
                { field: "MC_BUYER_ID", hidden: true },
                { field: "HR_COUNTRY_ID", hidden: true },
                { field: "NTF_PARTY_NAME_BN", hidden: true },
                { field: "ADDRESS_EN", hidden: true },
                { field: "ADDRESS_BN", hidden: true },
                { field: "POST_CODE", hidden: true },
                { field: "PO_BOX_NO", hidden: true },

                { field: "BUYER_NAME_EN", title: "Buyer", width: "10%" },
                { field: "COUNTRY_NAME_EN", title: "Country", width: "10%" },
                { field: "NTF_PARTY_CODE", title: "Code", width: "10%" },
                { field: "NTF_PARTY_NAME_EN", title: "Name", width: "10%" },
                { field: "WORK_PHONE", title: "Phone", width: "10%" },
                { field: "MOB_PHONE", title: "Mobile", width: "10%" },
                { field: "FAX_NO", title: "Fax", width: "10%" },
                { field: "EMAIL_ID", title: "E-mail", width: "10%" },
                { field: "WEB_URL", title: "Web-Url", width: "10%" },
                { field: "IS_ACTIVE", title: "Status", width: "10%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>  <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
        };



        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var id = vm.form.CM_NOTIFY_PARTY_ID

                return GenRptDataService.saveDataByUrl(data, '/BuyerNotifyParty/Save').then(function (res) {

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
                        vm.getBuyerNotifyPartyList(data);
                        vm.clearAll(data);

                    }
                });
            }
        };

    }

})();