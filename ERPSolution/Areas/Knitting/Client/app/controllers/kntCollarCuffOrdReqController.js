////////// Start List Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntCollarCuffOrdReqController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntCollarCuffOrdReqController]);
    function KntCollarCuffOrdReqController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isSearch = 'N';

        var key = 'KNT_SC_GFAB_DLV_H_ID';
        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        $scope.search = {
            MC_BYR_ACC_GRP_ID: 0,
            RF_FAB_PROD_CAT_ID: 2,
            MC_STYLE_H_EXT_ID: null,
            MC_FAB_PROD_ORD_H_ID: null,
            FIRSTDATE: ''
        };

        vm.form = $stateParams.pSCM_SUPPLIER_ID > 0 ? { SCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID } : { SCM_SUPPLIER_ID: 0 };

        activate();
        function activate() {
            var promise = [getBuyerAcGrpList(), getSelectMonth(0, 0), getStyleExtList(null, null, null, null), getFabOederByOh(null, null, null, null, null)];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


                

        function getStyleExtList(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
            $scope.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                        url += "&pRF_FAB_PROD_CAT_ID=" + pRF_FAB_PROD_CAT_ID;
                        //url += "&pNATURE_OF_ORDER=POLO";
                        url += "&pHAS_FK_CLRCUF=Y";
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

        function getFabOederByOh(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID) {
            $scope.FabOederByOhDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                        url += "&pRF_FAB_PROD_CAT_ID=" + pRF_FAB_PROD_CAT_ID;
                        url += "&pNATURE_OF_ORDER=POLO";
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
                    getSelectMonth(null, itmTyp.RF_FAB_PROD_CAT_ID);
                    getStyleExtList(null, null, null, null)
                    getFabOederByOh(null, itmTyp.RF_FAB_PROD_CAT_ID, null, null, null)
                } else {
                    getSelectMonth(0, 0);
                    getFabOederByOh(null, null, null, null, null);
                    getStyleExtList(null, null, null, null);
                }
            },
            dataTextField: "FAB_PROD_CAT_SNAME",
            dataValueField: "RF_FAB_PROD_CAT_ID"
        };





        function getSelectMonth(MC_BYR_ACC_GRP_ID, RF_FAB_PROD_CAT_ID) {
            return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetShipmentMonth?pMC_BYR_ACC_GRP_ID=' + (MC_BYR_ACC_GRP_ID || null) + 'pRF_FAB_PROD_CAT_ID=' + (RF_FAB_PROD_CAT_ID || null)).then(function (res) {
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
                getStyleExtList(($scope.search.MC_BYR_ACC_GRP_ID || null), ($scope.search.RF_FAB_PROD_CAT_ID || null), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE)
                getFabOederByOh($scope.search.MC_BYR_ACC_GRP_ID, ($scope.search.RF_FAB_PROD_CAT_ID || null), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE, null)


            } else {
                getStyleExtList(($scope.search.MC_BYR_ACC_GRP_ID || null), ($scope.search.RF_FAB_PROD_CAT_ID || null), null, null);
                $scope.search['FIRSTDATE'] = null;
                $scope.search['LASTDATE'] = null;
                getFabOederByOh($scope.search.MC_BYR_ACC_GRP_ID, ($scope.search.RF_FAB_PROD_CAT_ID || null), null, null, null)
            }
        };


        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME||"" #</p></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.FAB_PROD_CAT_SNAME # (#: data.STYLE_NO #)</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        $scope.onFabOrderChange = function (e) {
            var itm = e.sender.dataItem(e.sender.item);
            if (itm.MC_FAB_PROD_ORD_H_ID) {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);

            } else {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);
            }
        };


        //$scope.buyerAcList = {
        //    optionLabel: "--- Buyer A/C ---",
        //    filter: "contains",
        //    autoBind: true,
        //    dataSource: {
        //        transport: {
        //            read: function (e) {
        //                return KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
        //                    e.success(res);
        //                });
        //            }
        //        }
        //    },
        //    select: function (e) {
        //        var item = this.dataItem(e.item);
        //        if (item.MC_BYR_ACC_GRP_ID.length != 0) {
        //            getStyleExtList(item.MC_BYR_ACC_GRP_ID, null);
        //            getSelectMonth(item.MC_BYR_ACC_GRP_ID);
        //            getFabOederByOh(item.MC_BYR_ACC_GRP_ID, null, null, null, null);
        //        }
        //    },
        //    dataTextField: "BYR_ACC_NAME_EN",
        //    dataValueField: "MC_BYR_ACC_GRP_ID"
        //};

        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                   
                    $scope.search.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                                        
                    getStyleExtList(item.MC_BYR_ACC_GRP_ID, null);
                    getSelectMonth(item.MC_BYR_ACC_GRP_ID, ($scope.search.RF_FAB_PROD_CAT_ID||null));
                    getFabOederByOh(item.MC_BYR_ACC_GRP_ID, null, null, null, null);
                    
                },
                //dataBound: function (e) {
                //    if ($stateParams.pMC_BYR_ACC_GRP_ID != null && $stateParams.pMC_BYR_ACC_GRP_ID > 0) {
                //        vm.form.MC_BYR_ACC_GRP_ID = $stateParams.pMC_BYR_ACC_GRP_ID;

                //        vm.styleExtDataSource.read();
                //    }
                //},
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }
     

        vm.mainCollarCuffOrdReqGridOption = {
            height: "400px",
            scrollable:true,
            //scrollable: {
            //    virtual: true               
            //},
            sortable: true,
            pageable: true,
            //groupable: true,
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
                { field: "FAB_PROD_CAT_SNAME", title: "Prod.Catg", width: "10%" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/c", width: "15%" },
                { field: "STYLE_NO", title: "Style", width: "18%" },
                //{ field: "RQD_PC_QTY", title: "Tot.Qty (pcs)", width: "10%" },
                { field: "INHOUSE_ASSIGN_QTY", title: "Req In-house", width: "10%" },
                { field: "INHOUSE_PROD_QTY", title: "Prod.In-house", width: "10%" },
                { field: "SCO_ASSIGN_QTY", title: "Req S/C Qty", width: "10%" },
                { field: "SCO_PROD_QTY", title: "Prod. S/c", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.navCollarCuffProd(dataItem)' title='Goto Production'><i class='fa fa-external-link'></i> Prod..</button> <a class='btn btn-xs blue' href='/Knitting/Knit/ScoCollarCuffProg?a=225/#/scoClrCufProg/{{dataItem.KNT_SCO_CLCF_PRG_H_ID || 0}}/clrCuffDtl?pMC_FAB_PROD_ORD_H_ID={{dataItem.MC_FAB_PROD_ORD_H_ID}}&pMC_STYLE_H_ID={{dataItem.MC_STYLE_H_ID}}&pORDER_NO={{dataItem.ORDER_NO}}' title='Goto S/C Program (Out-house)'><i class='fa fa-external-link'></i> S/C Prog</a>";
                    },
                    width: "15%"
                }
            ]
        };

        vm.navCollarCuffProd = function (dataItem) {
            console.log(dataItem);
            //var spDate = moment(vm.today).format("DD-MMM-YYYY");
            $state.go('KntCollarCuffOrdProdH.dtl', {
                pRF_FAB_PROD_CAT_ID: dataItem.RF_FAB_PROD_CAT_ID, pMC_BYR_ACC_GRP_ID: dataItem.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: dataItem.MC_STYLE_H_EXT_ID, pMC_COLOR_ID: 0, pMC_ORDER_H_ID_LST: dataItem.MC_ORDER_H_ID_LST,
                pPROD_DT: moment(vm.today).format("YYYY-MMM-DD")
            });
        }

        vm.navScoProg = function (dataItem) {
            console.log('==============');
            console.log(dataItem);
            //var spDate = moment(vm.today).format("DD-MMM-YYYY");
            $state.go('ScoCollarCuffProgH.CollarCuffDtl', {
                pKNT_SCO_CLCF_PRG_H_ID: dataItem.KNT_SCO_CLCF_PRG_H_ID || 0, pMC_FAB_PROD_ORD_H_ID: dataItem.MC_FAB_PROD_ORD_H_ID, pMC_STYLE_H_ID: dataItem.MC_STYLE_H_ID
            });
        }

        vm.mainCollarCuffOrdReqGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            //serverFiltering: true,            
            pageSize: 10,
            schema: {
                data: "data",
                total: "total",
                model: {
                    //id: "KNT_SC_GFAB_DLV_H_ID",
                    fields: {
                        SHIP_DT: { type: "date", editable: false }
                    }
                }
            },            
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KntCollarCuff/GetCollarCuffOrdReq?pMC_BYR_ACC_GRP_ID=' + $scope.search.MC_BYR_ACC_GRP_ID
                        + '&pRF_FAB_PROD_CAT_ID=' + $scope.search.RF_FAB_PROD_CAT_ID + '&pFIRSTDATE=' + $scope.search.FIRSTDATE
                        + '&pMC_STYLE_H_EXT_ID=' + $scope.search.MC_STYLE_H_EXT_ID + '&pMC_FAB_PROD_ORD_H_ID=' + $scope.search.MC_FAB_PROD_ORD_H_ID;

                    //showGrid(($scope.search.MC_BYR_ACC_GRP_ID || 0), ($scope.search.MC_FAB_PROD_ORD_H_ID || 0), ($scope.search.RF_FAB_PROD_CAT_ID || 0), ($scope.search.FIRSTDATE || null))

                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    if (vm.isSearch == 'Y') {
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
                            e.success(res);
                        });
                    }
                    else {
                        e.success([]);
                    }
                }
            }//,            
            //sort: [{ field: 'CHALAN_DT', dir: 'desc' }]
        });


        vm.detailExpand = function (dtlDataItem) {
            
            if (dtlDataItem) {
                return KnittingDataService.getDataByFullUrl("/api/knit/KntCollarCuff/GetCollarCuffOrdReqDtl?pMC_FAB_PROD_ORD_H_ID=" + dtlDataItem.MC_FAB_PROD_ORD_H_ID + '&pMC_STYLE_H_ID=' + dtlDataItem.MC_STYLE_H_ID).then(function (res) {

                    console.log(dtlDataItem);

                    dtlDataItem['itemsOrderColor'] = [];

                    var orderColors = _.map(_.groupBy(_.map(res, function (o) {
                        o['COL_SIZE'] = o.RF_GARM_PART_ID + o.FAB_COLOR_ID;
                        return o;
                    }), function (doc) {
                        return doc.COL_SIZE;
                    }), function (grouped) {
                        return grouped[0];
                    });

                    //console.log('=======');
                    //console.log(res);
                    //console.log(orderColors);

                    var gmtParts = _.map(_.groupBy(res, function (doc) {
                        return doc.RF_GARM_PART_ID;
                    }), function (grouped) {
                        return grouped[0];
                    });

                    var orderSizes = _.sortBy(_.map(_.groupBy(res, function (doc) {
                        return doc.MESUREMENT;
                    }), function (grouped) {
                        
                        grouped[0]['SIZE_CODE_LST'] = _.maxBy(_.map(grouped, function (o) {
                            o['SIZE_CODE_LST_LEN'] = o['SIZE_CODE_LST'].length;
                            return o;
                        }), 'SIZE_CODE_LST_LEN').SIZE_CODE_LST;

                        return grouped[0];
                    }), function (o) { return o.MESUREMENT; });


                    console.log(orderSizes);
                    var itmGmtPart = [];
                    angular.forEach(orderColors, function (val, key) {
                        dtlDataItem['itemsOrderColor'].push({
                            FAB_COLOR_ID: val['FAB_COLOR_ID'], COLOR_NAME_EN: val['COLOR_NAME_EN'], RF_GARM_PART_ID: val['RF_GARM_PART_ID'],
                            GARM_PART_NAME: val['GARM_PART_NAME'], itemsOrderSize: []
                        });

                        angular.forEach(orderSizes, function (val1, key1) {

                            var sizeQty = _.filter(res, function (ob) {
                                return ob.FAB_COLOR_ID == val['FAB_COLOR_ID'] && ob.RF_GARM_PART_ID == val['RF_GARM_PART_ID'] && ob.MESUREMENT == val1['MESUREMENT'];
                            });
                            console.log(sizeQty);

                            if (sizeQty.length < 1) {
                                sizeQty.push({RQD_PCS_QTY: 0, MESUREMENT: 'NA'});
                            }
                            dtlDataItem['itemsOrderColor'][key]['itemsOrderSize'].push({
                                SIZE_CODE: val1['SIZE_CODE_LST'], RQD_PCS_QTY: sizeQty[0]['RQD_PCS_QTY'], MESUREMENT: val1['MESUREMENT']
                            });
                            
                        });
                    });


                    //angular.forEach(orderColors, function (val, key) {
                    //    dtlDataItem['itemsOrderColor'].push({ MC_COLOR_ID: val['MC_COLOR_ID'], COLOR_NAME_EN: val['COLOR_NAME_EN'], itemsGMTPart: [] });

                    //    angular.forEach(gmtParts, function (val1, key1) {                            
                    //        dtlDataItem['itemsOrderColor'][key]['itemsGMTPart'].push({ RF_GARM_PART_ID: val1['RF_GARM_PART_ID'], GARM_PART_NAME: val1['GARM_PART_NAME'], itemsOrderSize: [] });

                    //        angular.forEach(orderSizes, function (val2, key2) {

                    //            var sizeQty = _.filter(res, function (ob) {
                    //                return ob.MC_COLOR_ID == val['MC_COLOR_ID'] && ob.RF_GARM_PART_ID == val1['RF_GARM_PART_ID'] && ob.MC_SIZE_ID == val2['MC_SIZE_ID'];
                    //            });

                    //            dtlDataItem['itemsOrderColor'][key]['itemsGMTPart'][key1]['itemsOrderSize'].push({
                    //                MC_SIZE_ID: val2['MC_SIZE_ID'], SIZE_CODE: val2['SIZE_CODE'], RQD_PC_QTY: sizeQty[0]['RQD_PC_QTY'], MESUREMENT: sizeQty[0]['MESUREMENT']
                    //            });
                    //        });
                    //    });
                    //});

                    


                    console.log(dtlDataItem);

                }, function (err) {
                    console.log(err);
                });
            }

            //return {
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                return KnittingDataService.getDataByFullUrl("/api/knit/KntCollarCuff/GetCollarCuffOrdReqDtl?pMC_FAB_PROD_ORD_H_ID=" + dtlDataItem.MC_FAB_PROD_ORD_H_ID).then(function (res) {
            //                    e.success(res);                                

            //                }, function (err) {
            //                    console.log(err);
            //                });
            //            }
            //        },
            //        serverPaging: true,
            //        serverSorting: true,
            //        serverFiltering: true,
            //        //pageSize: 5,
            //        //group: [{ field: "COLOR_NAME_EN", dir: "asc" }, { field: "GARM_PART_NAME", dir: "asc" }],
            //        filter: { field: "MC_FAB_PROD_ORD_H_ID", operator: "eq", value: dtlDataItem.MC_FAB_PROD_ORD_H_ID }
            //    },
            //    height: 250,
            //    scrollable: true,
            //    sortable: true,                
            //    columns: [
            //        { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%", hidden: true },
            //        { field: "GARM_PART_NAME", title: "GMT Part", width: "10%", hidden: true },
            //        { field: "SIZE_CODE", title: "Size", width: "10%" },
            //        { field: "MESUREMENT", title: "Measurement", width: "10%" },
            //        { field: "RQD_PC_QTY", title: "Qty", width: "10%" }                    
            //    ]
            //};
        };

        vm.getCollarCuffOrdReqList = function () {
            vm.isSearch = 'Y';
            vm.mainCollarCuffOrdReqGridDataSource.read();
        };

        //vm.addScFabDelvChallan = function () {
        //    $state.go('KntScGreyFabDelvH.dtl', { pKNT_SC_GFAB_DLV_H_ID: 0 });
        //};

        //vm.editScFabDelvChallan = function (dataItem) {
        //    $state.go('KntScGreyFabDelvH.dtl', { pKNT_SC_GFAB_DLV_H_ID: dataItem.KNT_SC_GFAB_DLV_H_ID });
        //};

        
        

    }

})();
////////// End List Controller