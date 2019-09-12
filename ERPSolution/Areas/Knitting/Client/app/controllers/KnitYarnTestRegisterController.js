(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitYarnTestRegisterController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$filter', 'Dialog', KnitYarnTestRegisterController]);
    function KnitYarnTestRegisterController($q, config, KnittingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();


        vm.form = { REPORT_CODE: '' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [batchRequisitionList(), yarnBrandListModal(), yarnCountList(), GetTestStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }



        function yarnBrandListModal() {
            return vm.categoryBrandList = {
                optionLabel: "-- Select Brand --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                                var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                                e.success(list);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BRAND_NAME_EN",
                dataValueField: "RF_BRAND_ID"
            };
        };

        //vm.categoryBrandList = new kendo.data.DataSource({
        //    transport: {
        //        read: function (e) {
        //            KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
        //                var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
        //                e.success(list);

        //            }, function (err) {
        //                console.log(err);
        //            });
        //        }
        //    }
        //});

        function yarnCountList() {
            return vm.YarnCountList = {
                optionLabel: "-- Select Yarn Count --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return KnittingDataService.getDataByFullUrl('/api/Common/YarnCountList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "YR_COUNT_DESC",
                dataValueField: "RF_YRN_CNT_ID"
            };
        };

        function GetTestStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(95).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LOOKUP_DATA_ID != 489 && x.LOOKUP_DATA_ID != 490 })
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
        }

        vm.searchData = function () {

            var pm = "";
            if (vm.form.RF_YRN_CNT_ID)
                pm = pm + "&pRF_YRN_CNT_ID=" + vm.form.RF_YRN_CNT_ID;
            if (vm.form.RF_BRAND_ID)
                pm = pm + "&pRF_BRAND_ID=" + vm.form.RF_BRAND_ID;
            if (vm.form.YRN_LOT_NO)
                pm = pm + "&pYRN_LOT_NO=" + vm.form.YRN_LOT_NO;
            if (vm.form.LK_YRN_TST_TYPE_ID)
                pm = pm + "&pLK_YRN_TST_TYPE_ID=" + vm.form.LK_YRN_TST_TYPE_ID;

            console.log(pm);
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/KnitYarnTestRegister?' + pm).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            })
        }


        function batchRequisitionList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = KnittingDataService.kFilterStr2QueryParam(params.filter);
                        KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/KnitYarnTestRegister?pYRN_LOT_NO=a' + pm).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            })
        }

        vm.gridOptions = {

            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            sortable: true,
            scrollable: true,
            height: 600,
            columns: [
                { field: "SUP_TRD_NAME_EN", title: "Supplier Name", type: "string", width: "10%" },
                { field: "ITEM_NAME_EN", title: "Name of Yarn", type: "string", width: "10%" },
                { field: "YRN_LOT_NO", title: "Yarn Lot #", type: "string", width: "10%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "10%" },
                { field: "IMP_LC_NO", title: "L/C No", type: "string", width: "10%" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "5%" },
                { field: "TEST_DT", title: "Test Date", type: "date", template: "#= kendo.toString(kendo.parseDate(TEST_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "TEST_RESULT", title: "Result", type: "string", width: "10%" },
                { field: "TEST_NOTE", title: "Remarks", type: "string", width: "10%" },
            ]
        };

        vm.printTestRegister = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3533';

            vm.form.YRN_LOT_NO = (dataItem.YRN_LOT_NO || "");
            vm.form.RF_YRN_CNT_ID = dataItem.RF_YRN_CNT_ID;
            vm.form.RF_BRAND_ID = dataItem.RF_BRAND_ID;
            vm.form.LK_YRN_TST_TYPE_ID = dataItem.LK_YRN_TST_TYPE_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        };



    }

})();