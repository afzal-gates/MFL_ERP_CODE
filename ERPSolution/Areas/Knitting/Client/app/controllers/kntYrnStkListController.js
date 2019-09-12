(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntYrnStkListController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', KntYrnStkListController]);
    function KntYrnStkListController($q, config, KnittingDataService, $stateParams, $state, $scope, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;

        vm.form = { RF_YRN_CNT_ID: '', RF_FIB_COMP_ID: '', LK_SPN_PRCS_ID: '', LK_COTN_TYPE_ID: '', IS_SLUB: '', IS_MELLANGE: '', YRN_LOT_NO: '', RF_BRAND_ID: '', SCM_STORE_ID: '' };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getYarnCountList(), getFabricCompList(), getCotnTypeList(), getStoreList(), getCatgBrandList(), getBrandLotList(), getSpinFinList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getStoreList() {

            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                //KnittingDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);

                }
            };
        };

        function getCatgBrandList() {

            vm.catBrandOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BRAND_NAME_EN",
                dataValueField: "RF_BRAND_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    var brandId = item.RF_BRAND_ID > 0 ? item.RF_BRAND_ID : -1;
                    //vm.yrnDtl.BRAND_NAME_EN = item.BRAND_NAME_EN;
                    getBrandWiseYarnLot(brandId, '');
                }
            };

            return vm.categoryBrandDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                            var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                            e.success(list);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getBrandLotList() {

            vm.brandLotOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "YRN_LOT_NO",
                dataValueField: "KNT_YRN_LOT_ID",
            };

            getBrandWiseYarnLot(-1, '');
        };

        function getBrandWiseYarnLot(pRF_BRAND_ID, pYRN_LOT_NO) {
            return vm.brandWiseYarnLOtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntYrnLotStock/GetBrandWiseYarnLotList?pRF_BRAND_ID=' + pRF_BRAND_ID;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        url += '&pYRN_LOT_NO';

                        console.log(url);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        function getSpinFinList() {
            return vm.SpinFinList = {
                optionLabel: "--Spin Finish--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(61).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {

                    if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                        vm.spinType = this.dataItem(e.item).LK_DATA_NAME_EN;
                    } else {
                        vm.spinType = '';
                    }


                },

                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getFabricCompList() {
            return vm.FabricCompList = {
                optionLabel: "--Fib. Composition--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/Common/FibreCompList').then(function (res) {
                                vm.FiberCompList = res;
                                //var data = [{
                                //    FIB_COMP_NAME: '--New Composition--',
                                //    RF_FIB_COMP_ID: -1
                                //}];

                                //res.forEach(function (val, key) {
                                //    data.push(val);
                                //})

                                e.success(res);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    },
                    //requestEnd: function (e) {
                    //    if (vm.selectedFibCom != '') {
                    //        vm.form['RF_FIB_COMP_ID'] = vm.selectedFibCom;
                    //    }

                    //}
                },
                select: function (e) {

                    //if (this.dataItem(e.item).RF_FIB_COMP_ID) {
                    //    vm.composition = this.dataItem(e.item).FIB_COMP_NAME;
                    //} else {
                    //    vm.composition = '';
                    //}                    
                },
                dataTextField: "FIB_COMP_NAME",
                dataValueField: "RF_FIB_COMP_ID"
            };
        }

        function getCotnTypeList() {
            return vm.CotnTypeList = {
                optionLabel: "--Cotton Type--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(62).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    if (dataItem.LOOKUP_DATA_ID && dataItem.LOOKUP_DATA_ID != 300) {
                        vm.cotnType = dataItem.LK_DATA_NAME_EN;
                    } else {
                        vm.cotnType = '';
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getYarnCountList() {

            return vm.YarnCountList = {
                optionLabel: "--Count--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/Common/YarnCountList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });

                        }
                    }
                },
                select: function (e) {
                    if (this.dataItem(e.item).RF_YRN_CNT_ID) {
                        vm.count = this.dataItem(e.item).YR_COUNT_DESC;
                    } else {
                        vm.count = '';
                    }


                },
                dataTextField: "YR_COUNT_DESC",
                dataValueField: "RF_YRN_CNT_ID"
            };



        }


        vm.yarnCurrStockOpt = {
            //height: 350,
            sortable: true,
            pageable: true,
            //filterable: {
            //    extra: false,
            //    operators: {
            //        string: {
            //            contains: "Contains",
            //            startswith: "Starts With",
            //            eq: "Is Equal To"
            //        }
            //    }
            //},
            columns: [
                { field: "YR_COUNT_NO", title: "Y. Count", width: "5%" },
                { field: "FIB_COMP_NAME", title: "Fib.Composition", type: "string", width: "13%" },
                { field: "SPN_PROCESS_NAME", title: "Spin Process", width: "9%" },
                { field: "COTTON_TYPE_NAME", title: "Cotton Type", width: "8%" },
                { field: "IS_SLUB", title: "Slab", width: "5%", filterable: false },
                { field: "IS_MELLANGE", title: "Mellange", width: "5%", filterable: false },
                { field: "BRAND_NAME_EN", title: "Brand", width: "12%" },
                { field: "YRN_LOT_NO", title: "Lot", width: "8%" },
                { field: "STORE_NAME_EN", title: "Store", width: "11%" },
                { field: "CUR_STOCK_QTY", title: "Phy. Qty", width: "8%" },
                {
                    field: "ALOC_QTY", title: "Alc. Qty", width: "8%",
                    template: "<a ng-click='vm.openAllocDtl(dataItem)'>#=ALOC_QTY#</a>"
                    //template: "<a ng-click='vm.reqDelete(dataItem)'  class='btn btn-xs red'> #=ALOC_QTY</a>"
                },
                { field: "STOCK_QTY", title: "Stock", width: "8%" }

            ]
        };

        vm.openAllocDtl = function (dataItem) {

            console.log(dataItem);
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'AlocYarnRequisitionDetails.html',
                controller: function ($scope, config, KnittingDataService, $modalInstance, AllocList) {
                    $scope.form = {};
                    $scope.data = AllocList;

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                resolve: {
                    AllocList: function () {
                        return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/GetYarnAllcoDtlByID?pKNT_YRN_LOT_ID=' + dataItem.KNT_YRN_LOT_ID + '&pYARN_ITEM_ID=' + dataItem.YARN_ITEM_ID);
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal',
            });

            modalInstance.result.then(function () {
                console.log("");
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }


        //if ($stateParams.pPRG_ISS_NO) {
        //    vm.ScProgramHeaderDs.filter({ field: "PRG_ISS_NO", operator: "contains", value: $stateParams.pPRG_ISS_NO });
        //} else {
        //    vm.ScProgramHeaderDs.filter();
        //}

        vm.getYarnCurrStock = function () {
            vm.yarnCurrStockDs = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total"//,
                    //model: {
                    //    fields: {
                    //        SC_PRG_DT: { type: "date" },
                    //    }
                    //}
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntYrnLotStock/GetYarnLotStockList';
                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pRF_YRN_CNT_ID=' + vm.form.RF_YRN_CNT_ID
                                + '&pRF_FIB_COMP_ID=' + vm.form.RF_FIB_COMP_ID + '&pLK_SPN_PRCS_ID=' + vm.form.LK_SPN_PRCS_ID
                                + '&pLK_COTN_TYPE_ID=' + vm.form.LK_COTN_TYPE_ID + '&pIS_SLUB=' + vm.form.IS_SLUB
                                + '&pIS_MELLANGE=' + vm.form.IS_MELLANGE + '&pKNT_YRN_LOT_ID=' + vm.form.KNT_YRN_LOT_ID
                                + '&pRF_BRAND_ID=' + vm.form.RF_BRAND_ID + '&pSCM_STORE_ID=' + vm.form.SCM_STORE_ID;

                        console.log(url);

                        //url += config.kFilterStr2QueryParam(params.filter);
                        //if (params.filter.length == 0) {
                        //    remQueryParam();
                        //}
                        KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });

                        console.log(url);
                    }
                },
                pageSize: 10
            });
        };


        vm.printYarnCurrStock = function () {
            //console.log(dataItem);

            vm.form.REPORT_CODE = 'RPT-3504';
            //vm.form.KNT_SC_PRG_ISS_ID = dataItem.KNT_SC_PRG_ISS_ID;

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


        vm.excelYarnCurrStock = function () {
            //console.log(dataItem);

            vm.form.REPORT_CODE = 'RPT-3504';
            vm.form.IS_EXCEL_FORMAT = 'Y';

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