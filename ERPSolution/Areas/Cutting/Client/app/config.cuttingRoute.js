(function () {
    'use strict';

    angular
        .module('multitex.cutting')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [


        {
            state: 'CutReportParams',
            config: {
                url: '/cutReportParams',
                views: {
                    "CutReportParams": {
                        controller: 'CutReportController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_CutReportParams'
                    }
                },
                Title: 'Cutting Reports',
                resolve: {
                    CutRptData: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrlNoToken('/api/common/getReportDataListByUser/' + 10); //// Here 1 is report group ID
                    }
                }
            }
        },
        {
            state: 'MrkrReqH',
            config: {
                url: '/mrkrReqH/:pGMT_MRKR_REQ_ID?pMC_BYR_ACC_GRP_ID&pMC_STYLE_H_EXT_ID&pMC_ORDER_H_ID&pGMT_COLOR_ID',
                views: {
                    "MrkrReqH": {
                        controller: 'CutMrkrReqHController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_MrkrReqH'
                    }
                },
                resolve: {
                    mrkrReqHdrData: function (CuttingDataService, $stateParams) {
                        if ($stateParams.pGMT_MRKR_REQ_ID && $stateParams.pGMT_MRKR_REQ_ID > 0) {
                            return CuttingDataService.getDataByFullUrl('/api/Cutting/MrkrReq/GetMrkrReqHdr?pGMT_MRKR_REQ_ID=' + ($stateParams.pGMT_MRKR_REQ_ID || 0));
                        }
                        else {
                            return {};
                        }
                    }
                },
                Title: 'Marker Requisition',
                reloadOnSearch: false
            }
        },
        {
            state: 'MrkrReqList',
            config: {
                url: '/mrkrReqList?pMC_BYR_ACC_GRP_ID&pMC_STYLE_H_EXT_ID',
                views: {
                    "MrkrReqList": {
                        controller: 'CutMrkrReqListController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_MrkrReqList'
                    }
                },
                Title: 'Marker Requisition',                
                reloadOnSearch: false
            }
        },
        {
            state: 'MrkrH',
            config: {
                url: '/mrkrH/:pGMT_MRKR_ID',
                views: {
                    "MrkrH": {
                        controller: 'CutMrkr4CadController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_MrkrH'
                    }
                },
                resolve: {
                    mrkrHdrData: function (CuttingDataService, $stateParams) {
                        if ($stateParams.pGMT_MRKR_ID && $stateParams.pGMT_MRKR_ID > 0) {
                            return CuttingDataService.getDataByFullUrl('/api/Cutting/MrkrReq/GetMrkrHdr?pGMT_MRKR_ID=' + ($stateParams.pGMT_MRKR_ID || 0));
                        }
                        else {
                            return {};
                        }
                    }
                },
                Title: 'Marker Creation',
                reloadOnSearch: false
            }
        },
        {
            state: 'LayChartH',
            config: {
                url: '/laychtH/:pGMT_CUT_INFO_ID?pMC_BYR_ACC_GRP_ID&pMC_STYLE_H_ID',
                views: {
                    "LayChartH": {
                        controller: 'CutLayChartHController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_LayChartH'
                    }
                },
                resolve: {
                    layChartHdrData: function (CuttingDataService, $stateParams) {
                        if ($stateParams.pGMT_CUT_INFO_ID && $stateParams.pGMT_CUT_INFO_ID > 0) {
                            return CuttingDataService.getDataByFullUrl('/api/Cutting/LayChart/GetCutInfoHdr?pGMT_CUT_INFO_ID=' + ($stateParams.pGMT_CUT_INFO_ID || 0));//.then(function (res) {

                            //    res['LAY_ST_TIME'] = kendo.toString(res['LAY_ST_TIME'], "dd/MMM/yyyy hh:mm tt");
                            //    res['LAY_END_TIME'] = kendo.toString(res['LAY_END_TIME'], "dd/MMM/yyyy hh:mm tt");

                            //    console.log(res);
                            //    return res;
                            //});
                        }
                        else {
                            return {};
                        }
                    },
                    userLevelData: function (CuttingDataService, $stateParams) {
                        return CuttingDataService.getDataByFullUrl('/api/Cutting/LayChart/GetCutInfoUserLavel');
                    }
                },
                Title: 'Lay Chart',
                reloadOnSearch: false
            }
        },
        {
            state: 'CuttingList',
            config: {
                url: '/cutList?pGMT_CUT_INFO_ID&pMC_BYR_ACC_GRP_ID&pMC_STYLE_H_EXT_ID&pMC_ORDER_H_ID&pMC_ORDER_SHIP_ID&pGMT_COLOR_ID',
                views: {
                    "CuttingList": {
                        controller: 'CuttingListController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_CuttingList'
                    }
                },
                Title: 'Cutting List',
                reloadOnSearch: false
            }
        },
        {
            state: 'LayChartH.BundleInfo',
            config: {
                url: '/bundleInfo',
                views: {
                    "LayChartH.BundleInfo": {
                        controller: 'CutBundleChartController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_BundleChart'
                    }
                },
                Title: 'Lay Chart',
                reloadOnSearch: false
            }
        },
        {
            state: 'StrRcv',
            config: {
                url: '/strRcv',
                views: {
                    "StrRcv": {
                        controller: 'CutStrRcvController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_StrRcv'
                    }
                },
                Title: 'Store Receive',
                reloadOnSearch: false
            }
        },
        {
            state: 'CutPanelInspection',
            config: {
                url: '/main?pGMT_CUT_PNL_INSPTN_H_ID',
                views: {
                    "CutPanelInspection": {
                        controller: 'CutPanelInspectionController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_CutPanelInspection'
                    }
                },
                resolve: {
                    GMT_PROD_PLN_CLNDR_ID: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrl('/CutCutPnlInspect/GetCalendarId').then(function (res) {
                            return res.GMT_PROD_PLN_CLNDR_ID;
                        });
                    }
                },

                Title: 'Cut Panel Inspection',
                reloadOnSearch: false
            }
        },
        {
            state: 'CutPanelRecut',
            config: {
                url: '/recut',
                views: {
                    "CutPanelRecut": {
                        controller: 'CutPanelRecutController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_CutPanelRecut'
                    }
                },
                resolve: {
                    V_GMT_PROD_PLN_CLNDR_ID: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrl('/CutCutPnlInspect/GetCalendarId').then(function (res) {
                            return res.GMT_PROD_PLN_CLNDR_ID;
                        });
                    }
                },
                Title: 'Cut Panel Recut',
                reloadOnSearch: false
            }
        },
        {
            state: 'BndlCardPrintParam',
            config: {
                url: '/bndlCardPrintParam?pMC_BYR_ACC_GRP_ID&pMC_STYLE_H_EXT_ID&pMC_ORDER_H_ID&pMC_ORDER_SHIP_ID&pGMT_COLOR_ID',
                views: {
                    "BndlCardPrintParam": {
                        controller: 'BndlCardPrintParamController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_BndlCardPrintParam'
                    }
                },
                Title: 'Bundle Card Print',
                reloadOnSearch: false
            }
        },
        {
            state: 'StoreRecv',
            config: {
                url: '/scan?pGMT_CUT_PNL_BNK_ID',
                views: {
                    "StoreRecv": {
                        controller: 'StoreRecvController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_StoreRecv'
                    }
                },
                resolve: {
                    V_GMT_PROD_PLN_CLNDR_ID: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrl('/CutCutPnlInspect/GetCalendarId').then(function (res) {
                            return res.GMT_PROD_PLN_CLNDR_ID;
                        });
                    }
                },
                Title: 'Bundles: Store Receive',
                reloadOnSearch: false
            }
        },
        {
            state: 'ReScan4SewInput',
            config: {
                url: '/rescan?pGMT_CUT_PNL_BNK_ID',
                views: {
                    "ReScan4SewInput": {
                        controller: 'ReScan4SewInputController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_ReScan4SewInput'
                    }
                },
                resolve: {
                    V_GMT_PROD_PLN_CLNDR_ID: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrl('/CutCutPnlInspect/GetCalendarId').then(function (res) {
                            return res.GMT_PROD_PLN_CLNDR_ID;
                        });
                    }
                },
                Title: 'Bundles: Re-scan for Sewing Input',
                reloadOnSearch: false
            }
        },
        {
            state: 'SewDelvChaln4Split',
            config: {
                url: '/sewDelvChaln4Split?pGMT_CUT_SEW_DLV_CHL_ID',
                views: {
                    "SewDelvChaln4Split": {
                        controller: 'CutSewDelvChaln4SplitController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_SewDelvChaln4Split'
                    }
                },
                Title: 'Split Sewing Delivery Challan',
                reloadOnSearch: false
            }
        },
        {
            state: 'DeliveryChallan',
            config: {
                url: '/SewDelChallanList?pGMT_CUT_SEW_DLV_CHL_ID',
                views: {
                    "DeliveryChallan": {
                        controller: 'CutSewingDeliveryChallanController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_SewingDeliveryChallanList'
                    }
                },
                Title: 'Sewing Delivery Challan List',
                reloadOnSearch: false
            }
     
        },
        {
            state: 'StoreRecvPrintEmbr',
            config: {
                url: '/PrintEmbr',
                views: {
                    "StoreRecvPrintEmbr": {
                        controller: 'CutStoreRecvPrintEmbrController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_StoreRecvPrintEmbr'
                    }
                },
                Title: 'Bundles: Store Receive after Print/Embr',
                reloadOnSearch: false
            }
        },
        {
            state: 'CutBundleCardAmend',
            config: {
                url: '/Amendment?pGMT_CUT_BNDL_AMND_ID',
                views: {
                    "CutBundleCardAmend": {
                        controller: 'CutBundleCardAmendController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_CutBundleCardAmend'
                    }
                },
                resolve: {
                    PROD_PLN_CLNDR_ID: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrl('/CutCutPnlInspect/GetCalendarId').then(function (res) {
                            return res.GMT_PROD_PLN_CLNDR_ID;
                        });
                    }
                },
                Title: 'Bundle Card Amendment',
                reloadOnSearch: false
            }
        },
        {
            state: 'SewScDelChallanList',
            config: {
                url: '/SewScDelChallanList?pGMT_CUT_SC_DLV_CHL_ID',
                views: {
                    "DeliveryChallan": {
                        controller: 'CutSewingScDeliveryChallanListController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_SewingScDeliveryChallanList'
                    }
                },
                Title: 'Sewing S/C Delivery Challan List',
                reloadOnSearch: false
            }
        },

        {
            state: 'SewPrtEmbrDelChallanList',
            config: {
                url: '/SewPrtEmbrDelChallanList?pGMT_CUT_PRN_DELV_CHL_H_ID',
                views: {
                    "DeliveryChallan": {
                        controller: 'CutSewingPrtEmbrChallanListController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_SewingPrintEmbrDeliveryChallanList'
                    }
                },
                Title: 'Print/Embroidery Delivery Challan List',
                reloadOnSearch: false
            }
        },
        {
            state: 'CutTableTgt',
            config: {
                url: '/cutTblTgt?pCALENDAR_DT&pGMT_PROD_PLN_CLNDR_ID',
                views: {
                    "CutTableTgt": {
                        controller: 'CutTableTgtController',
                        controllerAs: 'vm',
                        templateUrl: '/Cutting/Cutting/_CutTableTgt'
                    }
                },
                //resolve: {
                //    GmtProdPlanClndr: function (CuttingDataService) {
                //        return CuttingDataService.getDataByUrl('/CutCutPnlInspect/GetCalendarId').then(function (res) {
                //            return res.GMT_PROD_PLN_CLNDR_ID;
                //        });
                //    }
                //},
                Title: 'Cutting Table Target',
                reloadOnSearch: false
            }
        },
        {
            state: 'CutFabReqList',
            config: {
                url: '/CutFabReqList',
                controller: 'CutFabReqListController',
                controllerAs: 'vm',
                templateUrl: '/Cutting/Cutting/_CutFabReqList',
                Title: 'Cutting Requisition for Finished Fabric List'
            }
        },
        {
            state: 'CutFabReq',
            config: {
                url: '/CutFabReq?pDF_FSTR_REQ_H_ID',
                controller: 'CutFabReqController',
                controllerAs: 'vm',
                templateUrl: '/Cutting/Cutting/_CutFabReq',
                Title: 'Cutting Requisition for Finished Fabric',
                resolve: {
                    formData: function (DyeingDataService, $stateParams) {

                        if (angular.isDefined($stateParams.pDF_FSTR_REQ_H_ID) && $stateParams.pDF_FSTR_REQ_H_ID > 0) {
                            return DyeingDataService.getDataByFullUrl('/api/Cutting/CutFabReq/GetCutFabReqInfo?pDF_FSTR_REQ_H_ID=' + ($stateParams.pDF_FSTR_REQ_H_ID || 0)).then(function (res) {
                                return res;
                            });
                        }
                        else {
                            return {};
                        }
                    }
                }
            }
        },

     ];
    }
})();
