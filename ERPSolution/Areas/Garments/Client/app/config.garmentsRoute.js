(function () {
    'use strict';

    angular
        .module('multitex.garments')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [
            {
                state: 'GmtReportParams',
                config: {
                    url: '/gmtReportParams',
                    views: {
                        "GmtReportParams": {
                            controller: 'GmtReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Garments/Gmt/_GmtReportParams'
                        }
                    },
                    Title: 'Garments Reports',
                    resolve: {
                        gmtRptData: function (GarmentsDataService) {
                            return GarmentsDataService.getDataByUrlNoToken('/api/common/getReportDataListByUser/' + 7); //// Here 1 is report group ID
                        }
                    }
                }
            },
            {
                state: 'SwMcnSpec',
                config: {
                    url: '/swMcnScec/:pGMT_SW_MCN_SPEC_ID',
                    views: {
                        "SwMcnSpec": {
                            controller: 'GmtSwMcnSpecController',
                            controllerAs: 'vm',
                            templateUrl: '/Garments/Gmt/_SwMcnSpec'
                        }
                    },
                    resolve: {
                        formData: function (GarmentsDataService, $stateParams) {
                            if ($stateParams.pGMT_SW_MCN_SPEC_ID && $stateParams.pGMT_SW_MCN_SPEC_ID > 0) {
                                return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtMcn/GetSwMcnSpecById?pGMT_SW_MCN_SPEC_ID=' + ($stateParams.pGMT_SW_MCN_SPEC_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Sewing Machine Specification',
                    reloadOnSearch: false
                }
            },
            {
                state: 'GmtSewingOutPut',
                config: {
                    url: '/SewingOutput?pGMT_SEW_PROD_SCAN_ID',
                    views: {
                        "SewingOutput": {
                            controller: 'GmtSewingOutputController',
                            controllerAs: 'vm',
                            templateUrl: '/Garments/Gmt/_SewingOutPut'
                        }
                    },
                    Title: 'Sewing Output'
                }
            },
            {
                state: 'GmtWashSent',
                config: {
                    url: '/WashSent',
                    views: {
                        "GmtWashSent": {
                            controller: 'GmtWashSentController',
                            controllerAs: 'vm',
                            templateUrl: '/Garments/Gmt/_GmtWashSent'
                        }
                    },
                    Title: 'Wash Sent'
                }
            },

            {
                state: 'GmtWashRecv',
                config: {
                    url: '/WashRecv',
                    views: {
                        "GmtWashSent": {
                            controller: 'GmtWashRecvController',
                            controllerAs: 'vm',
                            templateUrl: '/Garments/Gmt/_GmtWashRecv'
                        }
                    },
                    Title: 'Wash Receive'
                }
            },

            {
                state: 'GmtFinIron',
                config: {
                    url: '/gmtFinIron?pCALENDAR_DT=&pHR_PROD_FLR_LST=',
                    views: {
                        "GmtFinIron": {
                            controller: 'GmtFinIronController',
                            controllerAs: 'vm',
                            templateUrl: '/Garments/Gmt/_GmtFinIron'
                        }
                    },
                    resolve: {
                        FloorData: function (GarmentsDataService) {
                            return GarmentsDataService.getDataByFullUrl('/api/common/GetSewingFloorData');
                        }
                    },
                    Title: 'Ironing'
                }
            },

            {
                state: 'GmtFinPoly',
                config: {
                    url: '/gmtFinPoly?pCALENDAR_DT=&pHR_PROD_FLR_LST=',
                    views: {
                        "GmtFinPoly": {
                            controller: 'GmtFinPolyController',
                            controllerAs: 'vm',
                            templateUrl: '/Garments/Gmt/_GmtFinPoly'
                        }
                    },
                    resolve: {
                        FloorData: function (GarmentsDataService) {
                            return GarmentsDataService.getDataByFullUrl('/api/common/GetSewingFloorData');
                        }
                    },
                    Title: 'Poly'
                }
            },

            {
                state: 'GmtFinFold',
                config: {
                    url: '/gmtFinFold?pCALENDAR_DT=&pHR_PROD_FLR_LST=',
                    views: {
                        "GmtFinFold": {
                            controller: 'GmtFinFoldController',
                            controllerAs: 'vm',
                            templateUrl: '/Garments/Gmt/_GmtFinFold'
                        }
                    },
                    resolve: {
                        FloorData: function (GarmentsDataService) {
                            return GarmentsDataService.getDataByFullUrl('/api/common/GetSewingFloorData');
                        }
                    },
                    Title: 'Folding'
                }
            },


            {
                state: 'GmtScInShipLeftOver',
                config: {
                    url: '/ScInShipLeftOver',
                    views: {
                        "ScInShipLeftOver": {
                            controller: 'GmtScInShipLeftOverContrller',
                            controllerAs: 'vm',
                            templateUrl: '/Garments/Gmt/_ScInShipLeftOver'
                        }
                    },
                    Title: 'SC In/ ShipOut/LeftOver/Finishing Rejection'
                }
              },
                {
                    state: 'GmtAsSampleSent',
                    config: {
                        url: '/AsSampleSent',
                        views: {
                            "AsSample": {
                                controller: 'GmtAsSampleSentContrller',
                                controllerAs: 'vm',
                                templateUrl: '/Garments/Gmt/_AsSampleSent'
                            }
                        },
                        Title: 'As Sample Sent'
                    }
                },
                {
                    state: 'GmtAsSampleRecv',
                    config: {
                        url: '/AsSampleRecv',
                        views: {
                            "AsSample": {
                                controller: 'GmtAsSampleRecvContrller',
                                controllerAs: 'vm',
                                templateUrl: '/Garments/Gmt/_AsSampleRecv'
                            }
                        },
                        Title: 'As Sample Recv'
                    }
                },

        ];
    }
})();
