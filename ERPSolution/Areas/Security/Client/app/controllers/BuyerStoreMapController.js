(function () {
    'use strict';

    angular.module('multitex.security').controller('BuyerStoreMapController', ['$state', 'logger', 'config', '$q', '$scope', '$http', '$stateParams', '$filter', 'SecurityService', BuyerStoreMapController]);

    function BuyerStoreMapController($state, logger, config, $q, $scope, $http, $stateParams, $filter, SecurityService) {

        var vm = this;

        $scope.format = config.appDateFormat;
        vm.currentDate = $filter('date')(new Date(), config.appDateFormat);

        vm.form = {};
        vm.errors = {};

        activate();
        vm.Title = $state.current.Title || '';
        vm.showSplash = true;

        function activate() {
            var promise = [getBuyerData(), loadStoreList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }


        function getBuyerData() {
            return vm.storeList = {
                optionLabel: "--- Select Store ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return SecurityService.getDataByFullUrl('/api/inv/StoreProfile/SelectAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };

        }


        vm.getStoreBuyerList = function () {
            if (vm.form.SCM_STORE_ID) {
                //vm.showSplash = true;
                vm.gridOptionsDS.read();
                //vm.showSplash = false;
            }
        }


        $scope.makeBatchSelected = function (IS_TRUE) {
            var data = $('#kendoGrid').data("kendoGrid").dataSource.data();
            angular.forEach($('#kendoGrid').data("kendoGrid").dataSource.data(), function (dataItem) {
                dataItem.set('IS_ACTIVE', IS_TRUE ? 'Y' : 'N');
            });

        }


        function loadStoreList() {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        vm.showSplash = true;
                        console.log(vm.form.SCM_STORE_ID);
                        SecurityService.getDataByFullUrl('/api/security/UserMap/GetBuyerStoreByID?pSCM_STORE_ID=' + (vm.form.SCM_STORE_ID || null)).then(function (res) {
                            e.success(res);
                            vm.showSplash = false;
                        });
                    }
                },
            })
        }

        vm.gridOptions = {
            //autoBind: true,            
            sortable: false,
            pageable: false,
            columns: [
                { field: "SCM_STORE_ID", hidden: true },
                { field: "MC_BYR_ACC_GRP_ID", hidden: true },
                { field: "SC_MAP_BYR_STR_ID", hidden: true },
                {
                    headerTemplate: "<input type='checkbox' ng-model='IS_ALL_SELECT' ng-change='makeBatchSelected(IS_ALL_SELECT)'> Select All",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                    },
                    width: "10%"
                },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer A/C Group", width: "40%" },
                { field: "BYR_ACC_GRP_SNAME", title: "Short Name", width: "20%" },
            ],
            //editable: true
        };

        vm.submitData = function (dataOri) {
            var data = angular.copy(dataOri);

            console.log(data.XML_MAP_D);

            var list = _.filter(vm.gridOptionsDS.data(), function (x) { return x.IS_ACTIVE == "Y" });
            //data["XML_MAP_D"] = "Afzal";
            data["XML_MAP_D"] = SecurityService.xmlStringShort(list.map(function (o) {
                return {
                    SC_MAP_BYR_STR_ID: o.SC_MAP_BYR_STR_ID == null ? 0 : o.SC_MAP_BYR_STR_ID,
                    MC_BYR_ACC_GRP_ID: o.MC_BYR_ACC_GRP_ID == null ? 0 : o.MC_BYR_ACC_GRP_ID,
                    SCM_STORE_ID: vm.form.SCM_STORE_ID,
                }
            }));
            console.log(data.XML_MAP_D);

            if (fnValidate() == true) {

                return SecurityService.saveDataByUrl(data, '/UserMap/SaveBSM').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.gridOptionsDS.read();
                    }
                });
            }
        }
    }


})();