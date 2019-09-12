(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntFabricStoreStkController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'Dialog', KntFabricStoreStkController]);
    function KntFabricStoreStkController($q, config, KnittingDataService, $stateParams, $state, $scope, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
       

        $scope.search = {
            FROM_DT: null,// vm.today,
            TO_DT: vm.today,
            MC_BYR_ACC_ID: 0,
            RF_FAB_PROD_CAT_ID: 0,
            MC_STYLE_H_EXT_ID: null,
            MC_FAB_PROD_ORD_H_ID: null,
            FIRSTDATE: '',
            MC_COLOR_ID: null,
            ORDER_NO_LST: '',
            IS_G_F: ''
        };

        vm.form = { RF_YRN_CNT_ID: '', RF_FIB_COMP_ID: '', LK_SPN_PRCS_ID: '', LK_COTN_TYPE_ID: '', IS_SLUB: '', IS_MELLANGE: '', YRN_LOT_NO: '', RF_BRAND_ID: '', SCM_STORE_ID: '' };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getStyleExtList(null, null, null, null)
                //getSelectMonth(0, 0), getFabOederByOh(null, null, null, null, null)
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        

        vm.fromDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.fromDateOpened = true;
        };

        vm.toDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.toDateOpened = true;
        };

        $scope.gfFabricList = {
            optionLabel: "-- Select Type --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success([{ IS_G_F: 'G', GF_FAB_NAME: 'Grey' }, { IS_G_F: 'F', GF_FAB_NAME: 'Finish' }]);
                    }
                }
            },            
            dataTextField: "GF_FAB_NAME",
            dataValueField: "IS_G_F"
        };



        $scope.productionTypeList = {
            optionLabel: "-- Select Type --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            change: function (e) {
                var itmTyp = this.dataItem(e.item);

                if (itmTyp.RF_FAB_PROD_CAT_ID) {
                    //getSelectMonth(null, itmTyp.RF_FAB_PROD_CAT_ID);
                    getStyleExtList(null, null, null, null)
                    //getFabOederByOh(null, itmTyp.RF_FAB_PROD_CAT_ID, null, null, null)
                } else {
                    //getSelectMonth(0, 0);
                    //getFabOederByOh(null, null, null, null, null);
                    getStyleExtList(null, null, null, null);
                }
            },
            dataTextField: "FAB_PROD_CAT_SNAME",
            dataValueField: "RF_FAB_PROD_CAT_ID"
        };

        
        function getStyleExtList(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
            $scope.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_ID=" + pRF_FAB_PROD_CAT_ID;
                        //url += "&pNATURE_OF_ORDER=POLO";
                        url += "&pFIRSTDATE=" + pFIRSTDATE;
                        url += "&pLASTDATE=" + pLASTDATE;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pSTYLE_NO';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + ($stateParams.MC_FAB_PROD_ORD_H_ID || null);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };

        vm.getOrderColor = function (pMC_FAB_PROD_ORD_H_ID) {
            //var dataItem = e.sender.dataItem(e.item);
            //console.log(dataItem);

            $scope.orderColorDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/knit/FabProdKnitOrder/GetColorListByProdOrdID?pMC_FAB_PROD_ORD_H_ID=" + pMC_FAB_PROD_ORD_H_ID;
                        
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }//,
                //serverFiltering: true
            }
        };

        function getFabOederByOh(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID) {
            $scope.FabOederByOhDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_ID=" + pRF_FAB_PROD_CAT_ID;
                        //url += "&pNATURE_OF_ORDER=POLO";
                        url += "&pFIRSTDATE=" + pFIRSTDATE;
                        url += "&pLASTDATE=" + pLASTDATE;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pORDER_NO_LST';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };


        function getSelectMonth(MC_BYR_ACC_ID, RF_FAB_PROD_CAT_ID) {
            return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (MC_BYR_ACC_ID || 0) + '/0/' + (RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
                $scope.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        };

        $scope.onChangeShipMonth = function (e) {
            var itmShipMonth = e.sender.dataItem(e.sender.item);

            if (itmShipMonth.MONTHOF) {
                $scope.search['FIRSTDATE'] = itmShipMonth.FIRSTDATE.split('T')[0];
                $scope.search['LASTDATE'] = itmShipMonth.LASTDATE.split('T')[0];
                getStyleExtList(($scope.search.MC_BYR_ACC_ID || null), ($scope.search.RF_FAB_PROD_CAT_ID || null), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE)
                getFabOederByOh($scope.search.MC_BYR_ACC_ID, ($scope.search.RF_FAB_PROD_CAT_ID || null), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE, null)


            } else {
                getStyleExtList(($scope.search.MC_BYR_ACC_ID || null), ($scope.search.RF_FAB_PROD_CAT_ID || null), null, null);
                $scope.search['FIRSTDATE'] = null;
                $scope.search['LASTDATE'] = null;
                getFabOederByOh($scope.search.MC_BYR_ACC_ID, ($scope.search.RF_FAB_PROD_CAT_ID || null), null, null, null)
            }
        };


        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME # (#: data.ORDER_NO||""#)</p></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.FAB_PROD_CAT_SNAME||"" #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        $scope.onFabOrderChange = function (e) {
            var itm = e.sender.dataItem(e.sender.item);
            if (itm.MC_FAB_PROD_ORD_H_ID) {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);                
                vm.getOrderColor(itm.MC_FAB_PROD_ORD_H_ID);
            } else {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);
                vm.getOrderColor(0);
            }

            
        };

        $scope.buyerAcList = {
            optionLabel: "--- Buyer A/C ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            select: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_BYR_ACC_ID.length != 0) {
                    getStyleExtList(item.MC_BYR_ACC_ID, null);
                    //getSelectMonth(item.MC_BYR_ACC_ID);
                    //getFabOederByOh(item.MC_BYR_ACC_ID, null, null, null, null);
                    vm.getOrderColor(0);
                }
            },
            dataTextField: "BYR_ACC_NAME_EN",
            dataValueField: "MC_BYR_ACC_ID"
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


        vm.dailyProdOpt = {
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
                //{ field: "PROD_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "100px" },
                { field: "GROUP_FIELD", title: "Group Desc", hidden: true },
                { field: "FAB_PROD_CAT_SNAME", title: "Type", width: "100px" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "150px" },
                { field: "ORDER_NO_LST", title: "Order#", width: "150px" },
                { field: "STYLE_NO", title: "Style#", width: "150px" },
                //{ field: "WORK_STYLE_NO", title: "Work Style#", width: "150px" },
                { field: "COLOR_NAME_EN", title: "Color", width: "100px", filterable: false },
                { field: "IS_G_F", title: "G/F Fab", width: "80px" },
                { field: "FABRIC_DESC", title: "Fabric", width: "300px" },
                { field: "FIN_DIA", title: "Fin DIA", width: "80px" },
                //{ field: "FIN_GSM", title: "Fin GSM", width: "80px", filterable: false },
                //{ field: "KNT_MACHINE_NO", title: "M/C#", width: "80px" },
                //{ field: "MC_RPM", title: "RPM", width: "70px" },
                {
                    field: "MC_DIA", title: "MC DIAxGG", headerAttributes: {
                        style: "text-align: center; font-weight: bold; white-space: normal"
                    },
                    width: "70px"
                },
                //{ field: "MC_GG", title: "MC GG", width: "70px" },
                {
                    field: "KNT_YRN_LOT_LST",
                    title: "Yarn Detail (Count - Brand - Lot - SL)",
                    headerAttributes: {
                        style: "text-align: center; font-weight: bold;white-space: normal"
                    },
                    width: "200px"
                },
                {                    
                    title: "Today Rcv. Qty (Kg)",
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: center; font-weight: bold"
                    },
                    columns: [                        
                        {
                            field: "TODAY_RCV_NO_ROLL", title: "# of Roll", width: "80px"//,
                            //footerTemplate: "#=sum#"
                        },
                        {
                            field: "TODAY_RCV_ROLL_WT", title: "Rcv.Qty", width: "80px"//,
                            //footerTemplate: "#=sum#"
                        }
                    ]
                },
                {
                    field: "TARGET_QTY", title: "Target Qty",
                    headerAttributes: {
                        style: "text-align: center; font-weight: bold; white-space: normal"
                    },
                    width: "80px"//,
                    //footerTemplate: "#=sum#"
                },
                {
                    field: "CUMULA_QTY", title: "Total Rcv. Qty",
                    headerAttributes: {
                        style: "text-align: center; font-weight: bold; white-space: normal"
                    },
                    width: "80px"//,
                    //footerTemplate: "#=sum#"
                },                        
                {
                    field: "BAL_QTY", title: "Due for Prod. Qty",
                    headerAttributes: {
                        style: "text-align: center; font-weight: bold; white-space: normal"
                    },
                    width: "80px"//,
                    //footerTemplate: "#=sum#"
                },
                {
                    field: "DELV_QTY", title: "Delevery Qty",
                    headerAttributes: {
                        style: "text-align: center; font-weight: bold; white-space: normal"
                    },
                    width: "80px"//,
                    //footerTemplate: "#=sum#"
                },
                {
                    field: "STOCK_QTY", title: "Stock Qty",
                    headerAttributes: {
                        style: "text-align: center; font-weight: bold; white-space: normal"
                    },
                    width: "80px"//,
                    //footerTemplate: "#=sum#"
                }
            ]
        };

        


        //if ($stateParams.pPRG_ISS_NO) {
        //    vm.ScProgramHeaderDs.filter({ field: "PRG_ISS_NO", operator: "contains", value: $stateParams.pPRG_ISS_NO });
        //} else {
        //    vm.ScProgramHeaderDs.filter();
        //}

        vm.getDailyProd = function () {
            vm.DailyProdDs = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KnitCommon/GetFabStrStkList';
                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pRF_FAB_PROD_CAT_ID=' + $scope.search.RF_FAB_PROD_CAT_ID
                                + '&pMC_BYR_ACC_ID=' + $scope.search.MC_BYR_ACC_ID + '&pMC_COLOR_ID=' + $scope.search.MC_COLOR_ID
                                + '&pFROM_DT=' + moment($scope.search.FROM_DT).format("DD-MMM-YYYY") + '&pTO_DT=' + moment($scope.search.TO_DT).format("DD-MMM-YYYY")
                                + '&pMC_FAB_PROD_ORD_H_ID=' + $scope.search.MC_FAB_PROD_ORD_H_ID + '&pORDER_NO_LST=' + $scope.search.ORDER_NO_LST
                                + '&pIS_G_F=' + $scope.search.IS_G_F;

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
                group: [
                    { field: "GROUP_FIELD", dir: "asc" }//,
                    //{ field: "COLOR_NAME_EN", dir: "asc" },
                ],
                aggregate: [
                    { field: "KNT_YRN_LOT_LST", aggregate: "count" },
                    { field: "A_SHIFT_QTY", aggregate: "sum" },
                    { field: "B_SHIFT_QTY", aggregate: "sum" },
                    { field: "C_SHIFT_QTY", aggregate: "sum" },
                    { field: "TOT_QTY", aggregate: "sum" }
                ],
                schema: {
                    data: "data",
                    total: "total",
                    model: {
                        fields: {
                            PROD_DT: { type: "date" },
                            KNT_YRN_LOT_LST: { type: "string", editable: false },
                            A_SHIFT_QTY: { type: "number", editable: false },
                            B_SHIFT_QTY: { type: "number", editable: false },
                            C_SHIFT_QTY: { type: "number", editable: false },
                            TOT_QTY: { type: "number", editable: false },
                        }
                    }
                }                
            });
        };


        vm.printYarnCurrStock = function () {
            //console.log(dataItem);

            vm.form.REPORT_CODE = 'RPT-3504';
            //vm.form.KNT_SC_PRG_ISS_ID = dataItem.KNT_SC_PRG_ISS_ID;
            
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
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