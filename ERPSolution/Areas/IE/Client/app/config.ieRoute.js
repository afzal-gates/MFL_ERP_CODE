(function () {
    'use strict';

    angular
        .module('multitex.ie')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [
            {
                state: 'IeReportParams',
                config: {
                    url: '/ieReportParams',
                    views: {
                        "GmtReportParams": {
                            controller: 'GmtReportController',
                            controllerAs: 'vm',
                            templateUrl: '/IE/ie/_IeReportParams'
                        }
                    },
                    Title: 'IE Reports',
                    resolve: {
                        gmtRptData: function (IeDataService) {
                            return IeDataService.getDataByUrlNoToken('/api/common/getReportDataListByUser/' + 7); //// Here 1 is report group ID
                        }
                    }
                }
            },
            {
                state: 'GmtStylItmList',
                config: {
                    url: '/gmtStylItmList/:pMC_BYR_ACC_ID',
                    views: {
                        "GmtStylItmList": {
                            controller: 'GmtStylItmController',
                            controllerAs: 'vm',
                            templateUrl: '/IE/ie/_GmtStylItmList'
                        }
                    },                    
                    Title: 'GMT Item Specification',
                    resolve: {
                        BuyerAccData: function (IeDataService, $stateParams) {
                            return IeDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                return [{ BYR_ACC_NAME_EN: 'All', IS_TAB_ACT: true, MC_BYR_ACC_ID: -1 }].concat(res.map(function (o) {
                                    o['IS_TAB_ACT'] = false;
                                    return o;
                                }));
                            });
                        }
                    }
                }
            },
            {
                state: 'GmtStylItmList.grid',
                config: {
                    url: '/grid?pFIRSTDATE&pLASTDATE&page&pageSize&pMONTHOF',
                    views: {
                        "TabInside": {
                            controller: 'GmtStylItmDController',
                            controllerAs: 'vm',
                            templateUrl: '/IE/IE/_GmtStylItmListD'
                        }
                    },
                    Title: 'Buyer wise Style and Order Followup'
                }
            },
            {
                state: 'GmtItemOprSpec',
                config: {
                    url: '/itmOprSpec?pMC_BYR_ACC_ID&pMC_STYLE_H_ID',
                    views: {
                        "GmtItemOprSpec": {
                            controller: 'ItemOprSpecController',
                            controllerAs: 'vm',
                            templateUrl: '/IE/IE/_GmtItemOprSpec'
                        }
                    },
                    Title: 'Garments Item Specification',
                    reloadOnSearch: false,
                    resolve: {
                        StyleItemListData: function (IeDataService, $stateParams) {
                            return IeDataService.getDataByFullUrl('/api/mrc/StyleDItem/ChildItemListByStyle/' + $stateParams.pMC_STYLE_H_ID).then(function (res) {
                                //console.log(res);
                                return res;
                            });
                        }
                    }
                }
            },
            {
                state: 'GmtItemOprSpec.itmDtl',
                config: {
                    url: '/itmDtl?pMC_STYLE_D_ITEM_ID&pINV_ITEM_CAT_ID',
                    views: {
                        "TabInside": {
                            controller: 'ItemOprSpecDController',
                            controllerAs: 'vm',
                            templateUrl: '/IE/IE/_GmtItemOprSpecD'
                        }
                    },
                    Title: 'Garments Item Specification'
                }
            },
            {
                state: 'GmtWashReqList',
                config: {
                    url: '/GmtWashReqList',
                    controller: 'GmtWashReqListController',
                    controllerAs: 'vm',
                    templateUrl: '/ie/ie/_GmtWashReqList',
                    Title: 'Garments Wash Requisition List'
                }
            },
            {
                state: 'GmtWashReq',
                config: {
                    url: '/GmtWashReq?pGMT_DF_WASH_REQ_ID',
                    controller: 'GmtWashReqController',
                    controllerAs: 'vm',
                    templateUrl: '/ie/ie/_GmtWashReq',
                    Title: 'Garments Wash Requisition',
                    resolve: {
                        formData: function (IeDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pGMT_DF_WASH_REQ_ID) && $stateParams.pGMT_DF_WASH_REQ_ID > 0) {
                                return IeDataService.getDataByFullUrl('/api/ie/GmtWashReq/GetGmtWashReqInfoByID?pGMT_DF_WASH_REQ_ID=' + ($stateParams.pGMT_DF_WASH_REQ_ID || 0)).then(function (res) {
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
            {
                state: 'SewingTargetSetting',
                config: {
                    url: '/SewingTargetSetting?pLINE_LIST&pFLOOR_LIST',
                    views: {
                        "SewingTargetSetting": {
                            controller: 'SewingTargetSettingController',
                            controllerAs: 'vm',
                            templateUrl: '/ie/ie/_SewingTargetSetting'
                        },
                    },
                    resolve: {
                        FloorData: function (IeDataService) {
                            return IeDataService.getDataByFullUrl('/api/common/GetSewingFloorData');
                        }
                    },
                    Title: 'Sewing Target Setting'
                },
            },
            {
                state: 'ManMinAdjustment',
                config: {
                    url: '/ManMinAdjustment?pLINE_LIST&pFLOOR_LIST',
                    views: {
                        "SewingTargetSetting": {
                            controller: 'IeManMinsAdjustmentController',
                            controllerAs: 'vm',
                            templateUrl: '/ie/ie/_IeManMinsAdjustment'
                        },
                    },
                    resolve: {
                        FloorData: function (DashBoardService) {
                            return DashBoardService.getDataByFullUrl('/api/common/GetSewingFloorData');
                        }
                    },

                    Title: 'Man Minutes Adjustment'
                },
            },
            {
                state: 'GmtManualProductionEntry',
                config: {
                    url: '/GmtManualProductionEntry?pLINE_LIST&pFLOOR_LIST',
                    views: {
                        "SewingTargetSetting": {
                            controller: 'SewingTargetSettingController',
                            controllerAs: 'vm',
                            templateUrl: '/ie/ie/_SewingTargetSetting'
                        },
                    },
                    resolve: {
                        FloorData: function (IeDataService) {
                            return IeDataService.getDataByFullUrl('/api/common/GetSewingFloorData');
                        }
                    },
                    Title: 'Gmt Manual Production Entry'
                },
            },
            {
                state: 'IeGmtItemVariation',
                config: {
                    url: '/edit',
                    views: {
                        "GmtItemVariation": {
                            controller: 'IeGmtItemVariationController',
                            controllerAs: 'vm',
                            templateUrl: '/ie/ie/_IeGmtItemVariation'
                        },
                    },
                    Title: 'Gmt Item: Tech Variation'
                },
            },

        ];
    }
})();
