(function () {
    'use strict';
    angular.module('multitex.cmr').controller('DUModalController', ['$q', 'config', 'CmrDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', 'formData', '$modalInstance', DUModalController]);
    function DUModalController($q, config, CmrDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog, formData, $modalInstance) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = formData.MC_BUYER_ID ? formData : { IS_ACTIVE: 'Y' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetCountryList(), getBuyerListData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.cancel = function () {
            $modalInstance.close(vm.DuList);
        }

        function getBuyerListData() {
            return vm.buyerList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (res) {
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

        if (formData) {
            if (formData.MC_BUYER_ID > 0) {
                CmrDataService.getDataByFullUrl('/api/cmr/BuyerNotifyParty/GetBuyerNotifyPartyInfo?pMC_BUYER_ID=' + formData.MC_BUYER_ID).then(function (res) {

                    vm.DuList = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
            else {
                vm.DuList = new kendo.data.DataSource({
                    data: []
                });

                vm.form = { HR_COUNTRY_ID: '', IS_ACTIVE: 'Y' };
            }
        }


        vm.getBuyerNotifyPartyList = function (data) {
            if (data.MC_BUYER_ID > 0) {
                vm.form.MC_BUYER_ID = data.MC_BUYER_ID;
                CmrDataService.getDataByFullUrl('/api/cmr/BuyerNotifyParty/GetBuyerNotifyPartyInfo?pMC_BUYER_ID=' + data.MC_BUYER_ID).then(function (res) {

                    vm.DuList = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
            else {
                vm.DuList = new kendo.data.DataSource({
                    data: []
                });

                vm.form = { HR_COUNTRY_ID: '', IS_ACTIVE: 'Y' };
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
                vm.DuList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.FAB_PROC_NAME + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.DuList.remove(data);
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

                { field: "CM_UD_ID", hidden: true },
                //{ field: "MC_BUYER_ID", hidden: true },
                { field: "RF_GMT_ASN_ID", hidden: true },

                { field: "UD_NO", title: "UD No", width: "30%" },
                { field: "UD_DT", title: "UD Date", width: "20%" },
                { field: "GMT_ASN_NAME_EN", title: "Garments Business Association", width: "40%" },

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

                var id = vm.form.CM_UD_ID

                return CmrDataService.saveDataByUrl(data, '/BuyerNotifyParty/Save').then(function (res) {

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