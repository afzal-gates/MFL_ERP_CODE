////////// Start CutMrkr4CadController Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutLayChartHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'layChartHdrData', 'userLevelData', 'Dialog', CutLayChartHController]);
    function CutLayChartHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, layChartHdrData, userLevelData, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.dtTimeFormat = config.appDateTimeFormat;

        vm.IS_SHOW_MARKER = 'N';
        var key = 'GMT_CUT_INFO_ID';
        vm.today = kendo.toString(new Date(), "dd/MMM/yyyy hh:mm tt");

        //console.log($stateParams);
        console.log(layChartHdrData);
        console.log(userLevelData);

        vm.userLevelData = userLevelData;

        if (layChartHdrData[key]) {
            layChartHdrData['LAY_ST_TIME'] = moment(layChartHdrData.LAY_ST_TIME).format("DD-MMM-YYYY hh:mm a");
            if (layChartHdrData['LAY_END_TIME'] != null) {
                layChartHdrData['LAY_END_TIME'] = moment(layChartHdrData.LAY_END_TIME).format("DD-MMM-YYYY hh:mm a");
            }

            layChartHdrData.TOT_PLY_QTY = _.sumBy(layChartHdrData.itemsCutInfoPly, function (o) { return o.PLY_QTY; });
        }



        //vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO + (data.IS_MULTI_SHIP_DT=="Y"?"-" + data.SHIP_DESC:"") #</h5></span>';
        //vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # #: data.FAB_PROD_CAT_SNAME # (#: data.ORDER_NO #)</h5></span>';
        vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p>(#: data.ORDER_NO #)</p></span>';
        vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';

        vm.form = layChartHdrData[key] ? layChartHdrData : {
            GMT_CUT_INFO_ID: 0, MC_BYR_ACC_GRP_ID: $stateParams.pMC_BYR_ACC_GRP_ID || 0, MC_STYLE_H_ID: $stateParams.pMC_STYLE_H_ID || 0,
            LK_CUT_TYPE_ID: 675, LAY_ST_TIME: vm.today, LAY_END_TIME: null, LK_BNDL_DIV_TYP_ID: 683, MAX_RATIO_QTY: 3,
            MAX_QTY_IN_BNDL: 30, LK_STCKR_OPT_ID: 679, LK_DIA_TYPE_ID: 327, HAS_GRADING: "N", TOT_PLY_QTY: 0,
            itemsCutInfoSzRtoItm: [],
            itemsCutInfoPly: [{
                GMT_CUT_INFO_PLY_ID: 0, ROLL_SL: 0, MC_ORDER_SHIP_ID: null, GMT_COLOR_ID: null, MC_ORDER_CNTRY_ID: null, PLY_QTY: null, FAB_ROLL_WT: null, DYE_LOT_NO: null,
                itemsOrdShipList: [], itemsColorList: [], itemsCountryList: []
            }],
            itemsCutInfoGrd: [{ GMT_CUT_INFO_GRD_ID: 0, MC_ORDER_SHIP_ID: null, GMT_COLOR_ID: null, MC_ORDER_CNTRY_ID: null, MC_STYLE_D_ITEM_ID: null, MC_SIZE_ID: null, BUNDLE_NO: 501, DYE_LOT_NO: null, GRD_SL1: null, GRD_SL2: null, QTY_IN_BNDL: null }]
        };


        console.log(vm.form);

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getBuyerAcGrpList(), getStyleList(), getByrAccWiseStyleExtList(), /*getOrderShipment(), getOrderColor(),*/ getCuttingTableList(), getDiaTypeList(),
                getBndlDivideTyp(), getMrkrWayTypeList(), getMrkrTypeList(), getCuttingTypeList(), getStickerOptionList(), getMarkerList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }



        //if (layChartHdrData[key]) {

        //    if (layChartHdrData.itemsCutInfoGrd.length < 1) {

        //        vm.form['MC_ORDER_H_ID'] = layChartHdrData.MC_ORDER_H_ID;

        //        layChartHdrData.itemsCutInfoGrd.push({ GMT_CUT_INFO_GRD_ID: 0, MC_ORDER_CNTRY_ID: null, MC_STYLE_D_ITEM_ID: 0, MC_SIZE_ID: 0, BUNDLE_NO: null, DYE_LOT_NO: null, GRD_SL1: null, GRD_SL2: null, QTY_IN_BNDL: null });

        //        getOrdCountryList();
        //        getOrdItemList();
        //        getOrdSizeList();
        //    }
        //}
        //else {
        //    if (vm.form['MC_ORDER_H_ID'] > 0) {
        //        getOrdCountryList();
        //        getOrdItemList();
        //        getOrdSizeList();
        //    }
        //}

        vm.reqDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.reqDateOpened = true;
        };

        function getCuttingTableList() {
            vm.cuttingTableOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "TABLE_NO",
                dataValueField: "GMT_CUT_TABLE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.GMT_CUT_TABLE_ID = item.GMT_CUT_TABLE_ID;
                }
            };

            return vm.cuttingTableDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/Cutting/MrkrReq/GetCuttingTableList';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getDiaTypeList() {
            vm.diaTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.diaTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/LookupListData/67';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        function getCuttingTypeList() {
            vm.cuttingTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.cuttingTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/LookupListData/137';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        function getStickerOptionList() {
            vm.stickerOptOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.stickerOptDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/LookupListData/138';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        function getBuyerAcGrpList() {

            vm.buyerAcGrpOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;

                    vm.styleDataSource.read();
                    vm.OrdStyleExtDataSource.read();
                }
            };

            vm.buyerAcGrpDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/BuyerAcc/GetBuyerAccGrpList';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        }

        function getStyleList() {

            vm.styleOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.mrkrListDataSource.read();

                    getOrderShipment();
                },
                dataBound: function (e) {

                    if (vm.form.GMT_CUT_INFO_ID < 1 && vm.form.MC_STYLE_H_ID > 0) {
                        vm.mrkrListDataSource.read();

                        getOrderShipment();
                    }
                }
            };

            vm.styleDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var stylID = vm.form.MC_STYLE_H_ID;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        if (paramsData[2]) {
                            var url = '/api/mrc/StyleH/GetStyleList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0);
                        }
                        else {
                            var url = '/api/mrc/StyleH/GetStyleList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0) + '&pMC_STYLE_H_ID=' + ((vm.form.MC_STYLE_H_ID > 0) ? vm.form.MC_STYLE_H_ID : '');
                        }

                        url += CuttingDataService.kFilterStr2QueryParam(params.filter);

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                            if (stylID > 0) {
                                vm.form.MC_STYLE_H_ID = stylID;
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        }

        function getByrAccWiseStyleExtList() {
            vm.OrdStyleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                   
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;

                    vm.styleDataSource.read();
                    vm.mrkrListDataSource.read();
                    getOrderShipment();                    
                }
            };

            vm.OrdStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData);

                        if (paramsData[2]) {
                            var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0);
                        }
                        else {
                            var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0) + '&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID||null);
                        }

                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += CuttingDataService.kFilterStr2QueryParam(params.filter);

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        function getOrderShipment() {

            return CuttingDataService.getDataByFullUrl("/api/mrc/Order/GetOrderShipmentList?pMC_STYLE_H_ID=" + (vm.form['MC_STYLE_H_ID'] || 0) + '&pNUMBER_OF_REC=50').then(function (res) {

                vm.form['itemsCutInfoPly'][0].itemsOrdShipList = res;
                //vm.form['itemsCutInfoPly'][0].MC_ORDER_SHIP_ID = res[0].MC_ORDER_SHIP_ID;

                vm.form['itemsCutInfoGrd'][0].itemsOrdShipList = res;
                //vm.form['itemsCutInfoGrd'][0].MC_ORDER_SHIP_ID = res[0].MC_ORDER_SHIP_ID;

                //vm.onChangeOrdShip(res[0], 0);                
            });

            //vm.ordShipOption = {
            //    optionLabel: "-- Select --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataTextField: "ORDER_NO",
            //    dataValueField: "MC_ORDER_SHIP_ID",
            //    select: function (e) {
            //        var item = this.dataItem(e.item);
            //        console.log(item);

            //        //vm.form.MC_ORDER_SHIP_ID = item.MC_ORDER_SHIP_ID;

            //        //vm.OrdStyleExtDataSource.read();
            //    }
            //};

            //vm.ordShipDataSource[idx] = new kendo.data.DataSource({
            //    serverFiltering: true,
            //    transport: {
            //        read: function (e) {

            //            var webapi = new kendo.data.transports.webapi({});
            //            var params = webapi.parameterMap(e.data);

            //            var paramsData = params.filter.replace(/'/g, '').split('~');
            //            console.log(paramsData);

            //            if (paramsData[2]) {
            //                var url = '/api/mrc/Order/GetOrderShipmentList?pMC_STYLE_H_ID=' + (vm.form['MC_STYLE_H_ID'] || 0);
            //            }
            //            else {
            //                var url = '/api/mrc/Order/GetOrderShipmentList?pMC_STYLE_H_ID=' + ((vm.form['MC_STYLE_H_ID'] > 0) ? vm.form['MC_STYLE_H_ID'] : 0);
            //            }

                        
            //            url += CuttingDataService.kFilterStr2QueryParam(params.filter);
            //            console.log(url);
                        

            //            return CuttingDataService.getDataByFullUrl(url).then(function (res) {
            //                e.success(res);

            //                if (vm.form['GMT_CUT_INFO_ID'] < 1 && res.length == 1) {

            //                    console.log(res[0]);

            //                    vm.form['itemsCutInfoPly'][0].MC_ORDER_SHIP_ID = res[0].MC_ORDER_SHIP_ID;
            //                    vm.form['itemsCutInfoGrd'][0].MC_ORDER_SHIP_ID = res[0].MC_ORDER_SHIP_ID;
            //                }

            //                //vm.form['itemsCutInfoPly'][0].itemsOrdShipList = res;
            //                //vm.form['itemsCutInfoPly'][0].MC_ORDER_SHIP_ID = res[0].MC_ORDER_SHIP_ID;

            //                //vm.form['itemsCutInfoGrd'][0].itemsOrdShipList = res;
            //                //vm.form['itemsCutInfoGrd'][0].MC_ORDER_SHIP_ID = res[0].MC_ORDER_SHIP_ID;

            //                //vm.onChangeOrdShip(res[0], 0);

            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    }
            //});

        }

        vm.onChangeOrdShip = function (itm, idx, obName, e) {

            //var item = e.sender.dataItem(e.item);
            //itm['MC_ORDER_H_ID'] = item.MC_ORDER_H_ID;

            

            //alert(pMC_ORDER_H_ID);

            //return;

            var data = _.filter(itm.itemsOrdShipList, function (ob) {
                if (ob.MC_ORDER_SHIP_ID == itm.MC_ORDER_SHIP_ID) {
                    return ob;
                }
            });


            console.log(itm);
            //console.log(data);

            if (data.length > 0) {
                //alert('x');
                itm['MC_ORDER_H_ID'] = data[0].MC_ORDER_H_ID;
            }
            //console.log(itm);


            if (itm.MC_ORDER_H_ID) {
                CuttingDataService.getDataByFullUrl("/api/mrc/Order/OrderOrByerAccWiseColorList/" + itm.MC_ORDER_H_ID + '/null?pGMT_MRKR_ID=' + vm.form.GMT_MRKR_ID).then(function (res) {

                    if (obName == 'PLY') {
                        if (res.length > 0) {
                            vm.form['itemsCutInfoPly'][idx].itemsColorList = res;

                            if (res.length == 1) {
                                vm.form['itemsCutInfoPly'][idx].GMT_COLOR_ID = res[0].MC_COLOR_ID;
                            }
                        }
                        else {
                            vm.form['itemsCutInfoPly'][idx].itemsColorList = [];
                            vm.form['itemsCutInfoPly'][idx].GMT_COLOR_ID = null;
                        }
                    }

                    if (obName == 'GRD') {
                       
                        if (res.length > 0) {
                            vm.form['itemsCutInfoGrd'][idx].itemsColorList = res;

                            if (res.length == 1) {                                
                                vm.form['itemsCutInfoGrd'][idx].GMT_COLOR_ID = res[0].MC_COLOR_ID;
                                vm.onChangeOrdColor(itm, res[0].MC_COLOR_ID, idx);
                            }
                            else {
                                vm.form['itemsCutInfoGrd'][idx].GMT_COLOR_ID = null;
                                vm.onChangeOrdColor(itm, null, idx);
                            }
                        }
                        else {
                            vm.form['itemsCutInfoGrd'][idx].itemsColorList = [];
                            vm.form['itemsCutInfoGrd'][idx].GMT_COLOR_ID = null;
                        }
                    }
                });


                CuttingDataService.getDataByFullUrl("/api/mrc/Order/GetOrdCountryList?pMC_ORDER_H_ID=" + itm.MC_ORDER_H_ID).then(function (res) {

                    if (obName == 'PLY') {
                        if (res.length > 0) {
                            vm.form['itemsCutInfoPly'][idx].itemsCountryList = res;

                            if (res.length == 1) {
                                vm.form['itemsCutInfoPly'][idx].MC_ORDER_CNTRY_ID = res[0].MC_ORDER_CNTRY_ID;
                            }
                        }
                        else {
                            vm.form['itemsCutInfoPly'][idx].itemsCountryList = [];
                            vm.form['itemsCutInfoPly'][idx].MC_ORDER_CNTRY_ID = null;
                        }
                    }

                    if (obName == 'GRD') {
                        if (res.length > 0) {
                            vm.form['itemsCutInfoGrd'][idx].itemsCountryList = res;

                            if (res.length == 1) {
                                vm.form['itemsCutInfoGrd'][idx].MC_ORDER_CNTRY_ID = res[0].MC_ORDER_CNTRY_ID;
                            }
                        }
                        else {
                            vm.form['itemsCutInfoGrd'][idx].itemsCountryList = [];
                            vm.form['itemsCutInfoGrd'][idx].MC_ORDER_CNTRY_ID = null;
                        }
                    }
                });


                if (obName == 'GRD') {
                    CuttingDataService.getDataByFullUrl("/api/mrc/Order/OrderWiseSizeList/" + itm.MC_ORDER_H_ID).then(function (res) {
                        if (res.length > 0) {
                            vm.form['itemsCutInfoGrd'][idx].itemsSizeList = res;

                            if (res.length == 1) {
                                vm.form['itemsCutInfoGrd'][idx].MC_SIZE_ID = res[0].MC_SIZE_ID;
                            }
                        }
                        else {
                            vm.form['itemsCutInfoGrd'][idx].itemsSizeList = [];
                            vm.form['itemsCutInfoGrd'][idx].MC_SIZE_ID = null;
                        }
                    });
                }
            }

            //vm.ordColorOption = {
            //    optionLabel: "-- Select --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataTextField: "COLOR_NAME_EN",
            //    dataValueField: "MC_COLOR_ID",
            //    select: function (e) {
            //        var item = this.dataItem(e.item);
            //        console.log(item);

            //        vm.form.GMT_COLOR_ID = item.MC_COLOR_ID;

            //        vm.mrkrListDataSource.read();
            //        getOrdItemList();
            //    }
            //};

            //return vm.ordColorDataSource = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            var url = '/api/mrc/Order/OrderOrByerAccWiseColorList/' + vm.form.MC_ORDER_H_ID + '/null?pGMT_MRKR_ID=' & vm.form.GMT_MRKR_ID;

            //            //var url = '/api/mrc/Order/GetOrdColList?pMC_ORDER_H_ID=' + vm.form.MC_ORDER_H_ID;

            //            return CuttingDataService.getDataByFullUrl(url).then(function (res) {
            //                var colorList = _.map(_.groupBy(res, function (doc) {
            //                    return doc.MC_COLOR_ID;
            //                }), function (grouped) {
            //                    return grouped[0];
            //                });

            //                e.success(colorList);

            //                if (vm.form['GMT_CUT_INFO_ID'] < 1 && colorList.length == 1) {
            //                    vm.form.GMT_COLOR_ID = colorList[0].MC_COLOR_ID;
            //                    vm.form.COLOR_NAME_EN = colorList[0].COLOR_NAME_EN;

            //                    vm.mrkrListDataSource.read();
            //                    getOrdItemList();
            //                }

            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    }
            //});
        }

        

        //api/mrc/StyleDItem/ChildItemListByStyle/1
        function getOrderItem() {
            vm.OrdItemOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "MC_STYLE_D_ITEM_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    $scope.viewOrdItem.FROM_COLOR_NAME_EN = item.COLOR_NAME_EN;
                //    console.log(item);
                //}
            };

            return vm.OrdItemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/StyleDItem/ChildItemListByStyle/' + vm.form.MC_STYLE_H_ID;

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {

                            angular.forEach(res, function (val, key) {
                                if (val['MODEL_NO'] != '') {
                                    val['ITEM_NAME_EN'] = val['ITEM_NAME_EN'] + ' ' + val['MODEL_NO'];
                                }
                                else if (val['COMBO_NO'] != '') {
                                    val['ITEM_NAME_EN'] = val['ITEM_NAME_EN'] + ' ' + val['COMBO_NO'];
                                }
                            });

                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getFabricByStyleId() {

            vm.styleFabricOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FABRIC_DESC",
                dataValueField: "MC_STYLE_D_FAB_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    console.log(item);

                //    vm.fabOrder.FABRIC_DESC = item.FABRIC_DESC;
                //    vm.fabOrder.RF_FAB_TYPE_ID = item.RF_FAB_TYPE_ID;
                //    vm.fabOrder.RF_FIB_COMP_ID = item.RF_FIB_COMP_ID;
                //    vm.fabOrder.FIN_GSM = item.FB_WT_MAX;
                //}
            };

            return vm.styleFabricDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CuttingDataService.getDataByFullUrl('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + ((vm.form.MC_STYLE_H_ID > 0) ? vm.form.MC_STYLE_H_ID : 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        function getMarkerList() {
            vm.mrkrListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MRKR_SH_DESC",
                dataValueField: "GMT_MRKR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.LK_WAY_TYPE_ID = item.LK_WAY_TYPE_ID;
                    vm.form.LK_MRKR_TYPE_ID = item.LK_MRKR_TYPE_ID;
                    vm.form.MRKR_LEN = item.MRKR_LEN;
                    vm.form.MRKR_WDT = item.MRKR_WDT;

                    getOrdSizeRatio(item);
                },
                dataBound: function (e) {
                    if (layChartHdrData[key]) {
                        getOrdSizeRatio(vm.form);

                        var item = this.dataItem(e.item);
                        console.log(item);

                        vm.form.LK_WAY_TYPE_ID = item.LK_WAY_TYPE_ID;
                        vm.form.LK_MRKR_TYPE_ID = item.LK_MRKR_TYPE_ID;
                        vm.form.MRKR_LEN = item.MRKR_LEN;
                        vm.form.MRKR_WDT = item.MRKR_WDT;
                    }
                }
            };

            return vm.mrkrListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/Cutting/MrkrReq/GetMarkerList?pMC_STYLE_H_ID=' + (vm.form.MC_STYLE_H_ID||0);

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {

                            var dataList = _.filter(res, function (ob) {
                                if (ob.IS_APROVED == 'Y') {
                                    return ob;
                                }
                                else {
                                    return ob;
                                }
                            });

                            e.success(dataList);

                            if (vm.form['GMT_CUT_INFO_ID'] < 1 && dataList.length == 1) {
                                vm.form.GMT_MRKR_ID = res[0].GMT_MRKR_ID;

                                vm.form.LK_WAY_TYPE_ID = res[0].LK_WAY_TYPE_ID;
                                vm.form.LK_MRKR_TYPE_ID = res[0].LK_MRKR_TYPE_ID;
                                vm.form.MRKR_LEN = res[0].MRKR_LEN;
                                vm.form.MRKR_WDT = res[0].MRKR_WDT;

                                getOrdSizeRatio(vm.form);
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getBndlDivideTyp() {

            vm.bndlDivideTypOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.bndlDivideTypDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/LookupListData/140';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getMrkrWayTypeList() {

            return vm.mrkrWayTypeList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CuttingDataService.LookupListData(135).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getMrkrTypeList() {

            return vm.mrkrTypeList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CuttingDataService.LookupListData(136).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getOrdSizeRatio(dataItem) {
            CuttingDataService.getDataByFullUrl("/api/Cutting/LayChart/GetCutInfoSzRto?pGMT_MRKR_ID=" + dataItem['GMT_MRKR_ID'] + '&pGMT_CUT_INFO_ID=' + dataItem['GMT_CUT_INFO_ID']).then(function (res) {

                angular.forEach(res, function (val, key) {

                    var sizeRatio = [];
                    angular.forEach(val['itemsOrdSizeRatio'], function (val1, key1) {
                        if (val1['RATIO_QTY'] > 0) {
                            sizeRatio.push(val1);
                        }
                    });

                    val['itemsOrdSizeRatio'] = sizeRatio;
                    val['TOT_RATIO_QTY'] = _.sumBy(sizeRatio, function (o) { return o.RATIO_QTY; });
                });

                vm.form['itemsCutInfoSzRtoItm'] = res;

            });
        };

        //function getOrdCountryList() {
        //    CuttingDataService.getDataByFullUrl("/api/mrc/Order/GetOrdCountryList?pMC_ORDER_H_ID=" + vm.form['MC_ORDER_H_ID']).then(function (res) {

        //        vm.form['itemsCutInfoPly'][0].itemsCountryList = res;
        //        vm.form['itemsCutInfoPly'][0].MC_ORDER_CNTRY_ID = res[0].MC_ORDER_CNTRY_ID;

        //        vm.form['itemsCutInfoGrd'][0].itemsCountryList = res;
        //        vm.form['itemsCutInfoGrd'][0].MC_ORDER_CNTRY_ID = res[0].MC_ORDER_CNTRY_ID;
        //    });
        //};

        vm.onChangeOrdColor = function (itm, pMC_COLOR_ID, idx) {

            //var data = _.filter(itm.itemsOrdShipList, function (ob) {
            //    if (ob.MC_ORDER_SHIP_ID == itm.MC_ORDER_SHIP_ID) {
            //        return ob;
            //    }
            //});


            //if (data.length>0) {

            return CuttingDataService.getDataByFullUrl("/api/mrc/Order/GetOrdItmList?pMC_ORDER_H_ID=" + itm.MC_ORDER_H_ID + '&pMC_COLOR_ID=' + pMC_COLOR_ID).then(function (res) {

                if (res.length > 0) {
                    vm.form['itemsCutInfoGrd'][idx].itemsStyleItemList = res;

                    if (res.length == 1) {
                        vm.form['itemsCutInfoGrd'][idx].MC_STYLE_D_ITEM_ID = res[0].MC_STYLE_D_ITEM_ID;
                    }
                }
                else {
                    vm.form['itemsCutInfoGrd'][idx].itemsStyleItemList = [];
                    vm.form['itemsCutInfoGrd'][idx].MC_STYLE_D_ITEM_ID = null;
                }
            });
            //}
        };

        //vm.onChangeOrdItm = function (pMC_ORDER_H_ID, idx) {
        //    CuttingDataService.getDataByFullUrl("/api/mrc/Order/OrderWiseSizeList/" + pMC_ORDER_H_ID).then(function (res) {
        //        if (res.length > 0) {
        //            vm.form['itemsCutInfoGrd'][idx].itemsSizeList = res;

        //            if (res.length == 1) {
        //                vm.form['itemsCutInfoGrd'][idx].MC_SIZE_ID = res[0].MC_SIZE_ID;
        //            }
        //        }
        //        else {
        //            vm.form['itemsCutInfoGrd'][idx].itemsSizeList = [];
        //            vm.form['itemsCutInfoGrd'][idx].MC_SIZE_ID = null;
        //        }
        //    });
        //};

        vm.addRow = function (data) {
            var dataItem = angular.copy(data);

            dataItem['GMT_CUT_INFO_PLY_ID'] = 0;
            dataItem['ROLL_SL'] = 0;

            vm.form.itemsCutInfoPly.push(dataItem);
        }

        vm.addRowGrd = function (data) {
            var dataItem = angular.copy(data);

            dataItem['GMT_CUT_INFO_GRD_ID'] = 0;
            dataItem['BUNDLE_NO'] = null; //(vm.form.itemsCutInfoGrd.length + 1)+500;
            vm.form.itemsCutInfoGrd.push(dataItem);
        }

        vm.removeRow = function (index, removeFrom) {
            removeFrom.splice(index, 1);
        };

        vm.onChangeHasGrading = function () {
            getOrdSizeRatio(vm.form);
        }


        vm.cuttingGridOption = {
            height: 150,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "cell",
            navigatable: true,
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
            columns: [
                { field: "ITEM_NAME_EN", title: "Table", width: "15%", filterable: true },
                { field: "SIZE_CODE", title: "Marker Description", width: "30%", filterable: false },
                { field: "GROUP_NAME", title: "Cut#", width: "10%", filterable: false },
                { field: "RATIO_NAME", title: "Total Cutting", width: "15%", filterable: false },
                { field: "RATIO_NAME", title: "Date of Cutting", width: "15%", filterable: false },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' ng-disabled='true' >Mark as Cutting Prod</button>";
                    },
                    width: "15%"
                }
            ]
        };


        vm.cuttingGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    e.success([]);

                    //var url = '/api/Cutting/MrkrReq/GetMarkerList?pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID;

                    //console.log(url);

                    //CuttingDataService.getDataByFullUrl(url).then(function (res) {
                    //    if (vm.form.GMT_COLOR_ID > 1) {
                    //        e.success([{
                    //            GMT_MRKR_ID: 1, MRKR_REF_NO: 'MRKR2018050001', MRKR_SH_DESC: 'Cut dia: 60", Len: , Wth: 160"', WAY_TYPE_NAME: 'All GMT one way',
                    //            MARKER_TYPE_NAME: 'Normal', BK_FIN_DIA: '60"', CUT_FIN_DIA: '60"', MRKR_LEN: '', MRKR_WDT: '60"', MRKR_STATUS_NAME: 'Not Approve'
                    //        }]);
                    //    }
                    //    else {
                    //        e.success(res);
                    //    }
                    //});
                }
            }//,           
            //schema: {               
            //    model: {
            //        id: "GMT_MRKR_ID",
            //        fields: {                        
            //            MRKR_REF_NO: { type: "string", filterable: false }
            //        }
            //    }
            //}
        });



        vm.bundleGridOption = {
            height: 150,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "cell",
            navigatable: true,
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
            columns: [
                { field: "ITEM_NAME_EN", title: "S/L", width: "20%", filterable: true },
                { field: "SIZE_CODE", title: "Size", width: "20%", filterable: false },
                { field: "GROUP_NAME", title: "# of Bundle", width: "20%", filterable: false },
                { field: "RATIO_NAME", title: "Qty", width: "20%", filterable: false }//,                
                //{
                //    title: "",
                //    template: function () {
                //        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' ng-disabled='true' >Mark as Cutting Prod</button>";
                //    },
                //    width: "20%"
                //}
            ]
        };


        vm.bundleGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    e.success([]);

                    //var url = '/api/Cutting/MrkrReq/GetMarkerList?pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID;

                    //console.log(url);

                    //CuttingDataService.getDataByFullUrl(url).then(function (res) {
                    //    if (vm.form.GMT_COLOR_ID > 1) {
                    //        e.success([{
                    //            GMT_MRKR_ID: 1, MRKR_REF_NO: 'MRKR2018050001', MRKR_SH_DESC: 'Cut dia: 60", Len: , Wth: 160"', WAY_TYPE_NAME: 'All GMT one way',
                    //            MARKER_TYPE_NAME: 'Normal', BK_FIN_DIA: '60"', CUT_FIN_DIA: '60"', MRKR_LEN: '', MRKR_WDT: '60"', MRKR_STATUS_NAME: 'Not Approve'
                    //        }]);
                    //    }
                    //    else {
                    //        e.success(res);
                    //    }
                    //});
                }
            }//,           
            //schema: {               
            //    model: {
            //        id: "GMT_MRKR_ID",
            //        fields: {                        
            //            MRKR_REF_NO: { type: "string", filterable: false }
            //        }
            //    }
            //}
        });



        vm.onChangeRatioQty = function (itm) {
            itm.TOT_RATIO_QTY = _.sumBy(itm.itemsOrdSizeRatio, function (o) { return o.RATIO_QTY; });
        }

        vm.onChangePlyQty = function () {
            vm.form.TOT_PLY_QTY = _.sumBy(vm.form.itemsCutInfoPly, function (o) { return o.PLY_QTY; });
        }

        vm.batchSave = function () {
            var submitData = angular.copy(vm.form);

            var isValid = 'Y';

            if (!submitData.MC_BYR_ACC_GRP_ID) {
                $scope.frmLayChart.MC_BYR_ACC_GRP_ID.$setValidity('required', false);
                isValid = 'N';
                //return;
            }
            else {
                $scope.frmLayChart.MC_BYR_ACC_GRP_ID.$setValidity('required', true);
            }

            if (!submitData.MC_STYLE_H_ID) {
                $scope.frmLayChart.MC_STYLE_H_ID.$setValidity('required', false);
                isValid = 'N';
                //return;
            }
            else {
                $scope.frmLayChart.MC_STYLE_H_ID.$setValidity('required', true);
            }


            //if (!submitData.MC_ORDER_SHIP_ID) {
            //    $scope.frmLayChart.MC_ORDER_SHIP_ID.$setValidity('required', false);
            //    isValid = 'N';
            //    //return;
            //}
            //else {
            //    $scope.frmLayChart.MC_ORDER_SHIP_ID.$setValidity('required', true);
            //}

            //if (!submitData.GMT_COLOR_ID) {
            //    $scope.frmLayChart.GMT_COLOR_ID.$setValidity('required', false);
            //    isValid = 'N';
            //    //return;
            //}
            //else {
            //    $scope.frmLayChart.GMT_COLOR_ID.$setValidity('required', true);
            //}

            if (!submitData.CUTNG_NO) {
                $scope.frmLayChart.CUTNG_NO.$setValidity('required', false);
                isValid = 'N';
                //return;
            }
            else {
                $scope.frmLayChart.CUTNG_NO.$setValidity('required', true);
            }

            if (!submitData.LK_CUT_TYPE_ID) {
                $scope.frmLayChart.LK_CUT_TYPE_ID.$setValidity('required', false);
                isValid = 'N';
                //return;
            }
            else {
                $scope.frmLayChart.LK_CUT_TYPE_ID.$setValidity('required', true);
            }

            if (!submitData.LAY_ST_TIME) {
                $scope.frmLayChart.LAY_ST_TIME.$setValidity('required', false);
                isValid = 'N';
                //return;
            }

            if (!submitData.GMT_CUT_TABLE_ID) {
                $scope.frmLayChart.GMT_CUT_TABLE_ID.$setValidity('required', false);
                isValid = 'N';
                //return;
            }
            else {
                $scope.frmLayChart.GMT_CUT_TABLE_ID.$setValidity('required', true);
            }

            if (!submitData.LK_DIA_TYPE_ID) {
                $scope.frmLayChart.LK_DIA_TYPE_ID.$setValidity('required', false);
                isValid = 'N';
                //return;
            }
            else {
                $scope.frmLayChart.LK_DIA_TYPE_ID.$setValidity('required', true);
            }

            if (!submitData.LK_BNDL_DIV_TYP_ID) {
                $scope.frmLayChart.LK_BNDL_DIV_TYP_ID.$setValidity('required', false);
                isValid = 'N';
                //return;
            }
            else {
                $scope.frmLayChart.LK_BNDL_DIV_TYP_ID.$setValidity('required', true);
            }

            if (!submitData.LK_STCKR_OPT_ID) {
                $scope.frmLayChart.LK_STCKR_OPT_ID.$setValidity('required', false);
                isValid = 'N';
                //return;
            }
            else {
                $scope.frmLayChart.LK_STCKR_OPT_ID.$setValidity('required', true);
            }

            if (!submitData.MAX_RATIO_QTY && vm.form.LK_BNDL_DIV_TYP_ID == 683) {
                $scope.frmLayChart.MAX_RATIO_QTY.$setValidity('required', false);
                isValid = 'N';
                //return;
            }
            else {
                $scope.frmLayChart.MAX_RATIO_QTY.$setValidity('required', true);
            }

            if (!submitData.GMT_MRKR_ID) {
                $scope.frmLayChart.GMT_MRKR_ID.$setValidity('required', false);
                isValid = 'N';
                //return;
            }
            else {
                $scope.frmLayChart.GMT_MRKR_ID.$setValidity('required', true);
            }


            //angular.forEach($scope.frmLayChart.$error, function (value, key) {
            //    console.log(value);

            //    //if (typeof value === 'object' && value.hasOwnProperty('$modelValue'))
            //    //    console.log(value);
            //});



            if ($scope.frmLayChart.$error) {
                if ($scope.frmLayChart.$error.required) {
                    isValid = 'N';
                }
            }

            if (isValid == 'N') {
                return;
            }





            submitData['LAY_ST_TIME'] = $filter('date')(submitData.LAY_ST_TIME, vm.dtTimeFormat);

            if (submitData.LAY_END_TIME != null) {
                submitData['LAY_END_TIME'] = $filter('date')(submitData.LAY_END_TIME, vm.dtTimeFormat);
            }


            var cutInfoSzRto = [];
            angular.forEach(vm.form.itemsCutInfoSzRtoItm, function (val, key) {
                angular.forEach(val['itemsOrdSizeRatio'], function (val1, key1) {
                    cutInfoSzRto.push(val1);
                });
            });

            submitData['GMT_CUT_INFO_SZ_RTO_XML'] = CuttingDataService.xmlStringShort(cutInfoSzRto.map(function (ob) {
                return ob;
            }));

            submitData['GMT_CUT_INFO_PLY_XML'] = CuttingDataService.xmlStringShort(vm.form.itemsCutInfoPly.map(function (ob) {
                return {
                    GMT_CUT_INFO_PLY_ID: ob.GMT_CUT_INFO_PLY_ID, GMT_CUT_INFO_ID: ob.GMT_CUT_INFO_ID, MC_ORDER_SHIP_ID: ob.MC_ORDER_SHIP_ID, GMT_COLOR_ID: ob.GMT_COLOR_ID, MC_ORDER_CNTRY_ID: ob.MC_ORDER_CNTRY_ID,
                    ROLL_SL: ob.ROLL_SL, KNT_FAB_ROLL_ID: ob.KNT_FAB_ROLL_ID, FAB_ROLL_WT: ob.FAB_ROLL_WT, PLY_QTY: ob.PLY_QTY, DYE_LOT_NO: ob.DYE_LOT_NO,
                    COLOR_COMBO_TXT: ob.COLOR_COMBO_TXT
                };
            }));

            submitData['GMT_CUT_INFO_GRD_XML'] = CuttingDataService.xmlStringShort(vm.form.itemsCutInfoGrd.map(function (ob) {
                return ob;
            }));


            console.log(submitData);
            //return;


            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + $scope.token },
                         url: '/api/Cutting/LayChart/LayChartBatchSave',
                         method: 'post',
                         data: submitData
                     }).then(function (res) {
                         //$scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             //$scope.$parent.errors = [];
                             //$scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             if (res.data.PGMT_CUT_INFO_ID_RTN > 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                 vm.form['GMT_MRKR_REQ_ID'] = res.data.PGMT_CUT_INFO_ID_RTN;
                                 vm.form['CUTNG_NO'] = res.data.PCUTNG_NO_RTN;

                                 $state.go('LayChartH', { pGMT_CUT_INFO_ID: res.data.PGMT_CUT_INFO_ID_RTN }, { reload: true });
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });

        };

        vm.procBndlCard = function () {

            var submitData = angular.copy(vm.form);

            Dialog.confirm('Do you want to finalize and process?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + $scope.token },
                         url: '/api/Cutting/LayChart/ProcBndlCard',
                         method: 'post',
                         data: submitData
                     }).then(function (res) {
                         //$scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             //$scope.$parent.errors = [];
                             //$scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             vm.form.IS_FINALIZED = "Y";

                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });
        }



    }

})();
////////// End CutMrkr4CadController Controller






