(function () {
    'use strict';
    angular.module('multitex.security').controller('FabDfctTypeMapController', ['$state', 'logger', 'config', '$q', '$scope', '$http', '$stateParams', '$filter', 'SecurityService', FabDfctTypeMapController]);
    function FabDfctTypeMapController($state, logger, config, $q, $scope, $http, $stateParams, $filter, SecurityService) {

        var vm = this;

        $scope.format = config.appDateFormat;
        vm.currentDate = $filter('date')(new Date(), config.appDateFormat);

        vm.form = {};
        vm.errors = {};

        activate();
        vm.Title = $state.current.Title || '';
        vm.showSplash = true;

        function activate() {
            var promise = [GetFabDfctTypeList(), loadFabDfctTypeMapList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function GetFabDfctTypeList() {

            return vm.FabDfctTypeList = {
                optionLabel: "-- Select Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            SecurityService.LookupListData(144).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        vm.getFabDfctTypeData = function () {
            if (vm.form.LK_FAB_INSP_TYPE_ID) {
                vm.gridOptionsDS.read();
            }
        }


        $scope.makeBatchSelected = function (IS_TRUE) {
            var data = $('#kendoGrid').data("kendoGrid").dataSource.data();
            angular.forEach($('#kendoGrid').data("kendoGrid").dataSource.data(), function (dataItem) {
                dataItem.set('IS_ACTIVE', IS_TRUE ? 'Y' : 'N');
            });

        }


        function loadFabDfctTypeMapList() {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        vm.showSplash = true;
                        console.log(vm.form.SC_USER_ID);
                        SecurityService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=' + (vm.form.LK_FAB_INSP_TYPE_ID || null)).then(function (res) {
                            e.success(res);
                            vm.showSplash = false;
                        });
                    }
                },
            })
        }


        vm.gridOptions = {
            //autoBind: true, 
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },           
            sortable: true,
            pageable: false,
            columns: [
                { field: "SC_MAP_FAB_DFCT_TYP_ID", hidden: true },
                { field: "LK_FAB_INSP_TYPE_ID", hidden: true },
                { field: "RF_FB_DFCT_TYPE_ID", hidden: true },
                {
                    headerTemplate: "<input type='checkbox' ng-model='IS_ALL_SELECT' ng-change='makeBatchSelected(IS_ALL_SELECT)'> Select All",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.IS_ACTIVE' ng-true-value='\"Y\"' ng-false-value='\"N\"'>";
                    },
                    width: "10%"
                },
                { field: "FB_DFCT_TYPE_CODE", title: "Code", width: "10%" },
                { field: "FB_DFCT_TYPE_NAME", title: "Defect Name", width: "30%" },
                { field: "FB_DFCT_GRP_NAME", title: "Defect Group Name", width: "20%" },
                { field: "RESP_DEPT_NAME", title: "Department Name", width: "20%" },
                {
                    title: "Display Order", width: "8%", template: function () {
                        return "<input type='number' style='width:\90%;\' ng-model='dataItem.DISPLAY_ORDER'/>";
                    },
                },
            ],
            //editable: true
        };

        vm.submitData = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var list = _.filter(vm.gridOptionsDS.data(), function (x) { return x.IS_ACTIVE == "Y" });
                data["XML_MAP_D"] = SecurityService.xmlStringShort(list.map(function (o) {
                    return {
                        SC_MAP_FAB_DFCT_TYP_ID: o.SC_MAP_FAB_DFCT_TYP_ID == null ? 0 : o.SC_MAP_FAB_DFCT_TYP_ID,
                        LK_FAB_INSP_TYPE_ID: o.LK_FAB_INSP_TYPE_ID == null ? 0 : o.LK_FAB_INSP_TYPE_ID,
                        RF_FB_DFCT_TYPE_ID: o.RF_FB_DFCT_TYPE_ID == null ? 0 : o.RF_FB_DFCT_TYPE_ID,
                        DISPLAY_ORDER: o.DISPLAY_ORDER == null ? 0 : o.DISPLAY_ORDER,
                    }
                }));

                return SecurityService.saveDataByUrl(data, '/UserMap/SaveFDT').then(function (res) {

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