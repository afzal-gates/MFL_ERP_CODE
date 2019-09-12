(function () {
    'use strict';
    angular.module('multitex.hr').controller('CountryController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state', '$scope', '$modal', 'logger', CountryController]);
    function CountryController($q, config, HrService, $filter, $http, $stateParams, $state, $scope, $modal, logger) {

        var vm = this;
        vm.form = { IS_ACTIVE: 'Y' };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getRegeion(), CountryList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function CountryList() {
            vm.countryList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        HrService.GetDataByUrl('/api/common/GetCountryList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 10
            });
        };
        vm.editItemData = function (data) {
            vm.form = angular.copy(data);
        }

        vm.reset = function () {
            vm.form = { LK_REGION_ID: '', IS_ACTIVE: 'Y' };
        }

        function getRegeion() {
            return vm.regionList = {
                optionLabel: "-- Select Region --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getLookupListData(12).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }


        vm.gridOptions = {
            pageable: true,
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
            sortable:true,
            columns: [
                { field: "HR_COUNTRY_ID", hidden: true },
                { field: "LK_REGION_ID", hidden: true },
                { field: "COUNTRY_CODE", title: "COUNTRY CODE", type: "string", width: "7%" },
                { field: "COUNTRY_NAME_EN", title: "COUNTRY NAME(EN)", type: "string", width: "7%" },
                { field: "COUNTRY_NAME_BN", title: "COUNTRY NAME(BN)", type: "string", width: "7%" },
                { field: "REGION_NAME_EN", title: "REGION", type: "string", width: "7%" },
                { field: "IS_ACTIVE", title: "ACTIVE", type: "string", width: "7%" },
                {
                    title: "",
                    template: '<a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (data) {

            return HrService.saveDataByUrl(data, '/api/hr/Country/Save').then(function (res) {
                
                res['data'] = angular.fromJson(res.jsonStr);
                config.appToastMsg(res.data.PMSG);
                if (data.HR_COUNTRY_ID > 0) {
                    vm.form.HR_COUNTRY_ID = res.data.OPHR_COUNTRY_ID;
                }
                else {
                    vm.form = { LK_REGION_ID: '', IS_ACTIVE: 'Y' }
                }
                //if (res.data.OPHR_COUNTRY_ID > 0)
                //    vm.form.HR_COUNTRY_ID = res.data.OPHR_COUNTRY_ID;
                CountryList();
            });
        }

    }

})();