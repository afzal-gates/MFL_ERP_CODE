(function () {
    'use strict';

    angular
        .module('multitex.planning')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [
            {
                state: 'PlnReportParams',
                config: {
                    url: '/plnReportParams',
                    views: {
                        "PlnReportParams": {
                            controller: 'PlnReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/Pln/_PlnReportParams'
                        }
                    },
                    Title: 'Planning Reports',
                    resolve: {
                        plnRptData: function (PlanningDataService) {
                            return PlanningDataService.getDataByUrlNoToken('/api/common/getReportDataListByUser/' + 11); //// Here 1 is report group ID
                        }
                    }
                }
            },
            {
                state: 'CapctyClndr',
                config: {
                    url: '/capctyClndr?pRF_FISCAL_YEAR_ID',
                    views: {
                        "CapctyClndr": {
                            controller: 'CapctyClndrController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_ProdCapctyClndr'
                        }
                    },
                    //resolve: {
                    //    formData: function (GarmentsDataService, $stateParams) {
                    //        if ($stateParams.pGMT_SW_MCN_SPEC_ID && $stateParams.pGMT_SW_MCN_SPEC_ID > 0) {
                    //            return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtMcn/GetSwMcnSpecById?pGMT_SW_MCN_SPEC_ID=' + ($stateParams.pGMT_SW_MCN_SPEC_ID || 0));
                    //        }
                    //        else {
                    //            return {};
                    //        }
                    //    }
                    //},
                    Title: 'Production Calender',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ProdCapctyMN',
                config: {
                    url: '/capctyMN?pHR_COMPANY_ID&pPROD_UNIT_ID&pRF_FISCAL_YEAR_ID&pGMT_PRODUCT_TYP_ID&pGMT_CAPACITY_MN_ID&pGMT_PROD_PLN_CLNDR_ID&pCORE_DEPT_ID&pLK_CALC_MTHD_ID',
                    views: {
                        "ProdCapctyMN": {
                            controller: 'ProdCapctyMNController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_ProdCapctyMN'
                        }
                    },
                    resolve: {
                        formData: function (PlanningDataService, $stateParams) {
                            if ($stateParams.pGMT_CAPACITY_MN_ID && $stateParams.pGMT_CAPACITY_MN_ID > 0) {
                                return PlanningDataService.getDataByFullUrl('/api/pln/ProdCapcti/GetCapctiMonById?pGMT_CAPACITY_MN_ID=' + ($stateParams.pGMT_CAPACITY_MN_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Monthly Capacity Plan',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ProdCapctyMnList',
                config: {
                    url: '/capctyMnList?pHR_COMPANY_ID&pPROD_UNIT_ID=&pRF_FISCAL_YEAR_ID=&pGMT_PRODUCT_TYP_ID=',
                    views: {
                        "ProdCapctyMnList": {
                            controller: 'ProdCapctyMnListController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_ProdCapctyMnList'
                        }
                    },                    
                    Title: 'Monthly Capacity Plan',
                    reloadOnSearch: false
                }
            },
            {
                state: 'LearningCurv',
                config: {
                    url: '/learnCurv',
                    views: {
                        "LearningCurv": {
                            controller: 'LearningCurvController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_LearningCurv'
                        }
                    },
                    Title: 'Planning Learning Curve',
                    reloadOnSearch: false
                }
            },
            {
                state: 'OrdVlumWiseCpmEff',
                config: {
                    url: '/ordVlumCpm',
                    views: {
                        "OrdVlumWiseCpmEff": {
                            controller: 'OrdVlumWiseCpmEffController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_OrdVlumWiseCpmEff'
                        }
                    },
                    Title: 'Order Volume wise CPM Efficiency',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ThroughPutTime',
                config: {
                    url: '/thrPutTime',
                    views: {
                        "ThroughPutTime": {
                            controller: 'ThroughPutTimeController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_ThroughPutTime'
                        }
                    },
                    Title: 'Throughput Time',
                    reloadOnSearch: false
                }
            },
            {
                state: 'GmtPlanStatus',
                config: {
                    url: '/GmtPlanStatus',
                    views: {
                        "GmtPlanStatus": {
                            controller: 'GmtPlanStatusController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_GmtPlanStatus'
                        }
                    },
                    Title: 'Line Loading Plan: Status Rules',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ItemPartMap',
                config: {
                    url: '/itmPrtMap',
                    views: {
                        "ItemPartMap": {
                            controller: 'ItemPartMapController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_ItemPartMap'
                        }
                    },
                    Title: 'Item and Part Mapping',
                    reloadOnSearch: false
                }
            },
            {
                state: 'PartProcessSpec',
                config: {
                    url: '/prtProcSpec',
                    views: {
                        "PartProcessSpec": {
                            controller: 'PartProcessSpecController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_PartProcessSpec'
                        }
                    },
                    resolve: {
                        GmtPartID: function () {
                            return null;
                        },
                        $modalInstance: function () {
                            return null;
                        }
                    },
                    Title: 'Process Specification',
                    reloadOnSearch: false
                }
            },
            {
                state: 'PlnCritProcessTemp',
                config: {
                    url: '/CritProcessTemp?pMC_BUYER_ID&pMC_TNA_TMPLT_H_LST',
                    views: {
                        "PlnCritProcessTemp": {
                            controller: 'PlnCritProcessTempController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_PlnCritProcessTemp'
                        }
                    },
                    resolve: {
                        TnaTaskDs: function (PlanningDataService) {
                            return PlanningDataService.getDataByUrl('/CritProcess/getTnaTasksDs');
                        },
                        BuyerList: function (PlanningDataService) {
                            return PlanningDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll');
                        },
                    },
                    Title: 'Critical Activity Routing',
                    reloadOnSearch: false
                }
            },

            {
                state: 'ProdTypeLineMap',
                config: {
                    url: '/ProdTypeLineMap?pGMT_PRODUCT_TYP_ID',
                    views: {
                        "ProdTypeLineMap": {
                            controller: 'ProdTypeLineMapController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_ProdTypeLineMap'
                        }
                    },
                    Title: 'Product Criticality-Line & Preferred Gmt Category',
                    reloadOnSearch: false
                }
            },

            {
                state: 'PlnHolyDayModel',
                config: {
                    url: '/PlnHolyDayModel',
                    views: {
                        "PlnHolyDayModel": {
                            controller: 'PlnHolyDayModelController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_PlnHolyDayModel'
                        }
                    },
                    Title: 'Holyday Model',
                    reloadOnSearch: false
                }
            },

            {
                state: 'GmtLineLoadPlan',
                config: {
                    url: '/board',
                    views: {
                        "GmtLineLoadPlan": {
                            controller: 'GmtLineLoadingPlanController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_GmtLineLoadPlan'
                        }
                    },
                    Title: 'Line Loading Plan',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ActivityPermissionH',
                config: {
                    url: '/actvtyPermison/:pSC_USER_ID',
                    views: {
                        "ActivityPermissionH": {
                            controller: 'ActivityPermissionHController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_ActivityPermissionH'
                        }
                    },                    
                    Title: 'Planning Activity Permission',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ActivityPermissionH.EventAccess',
                config: {
                    url: '/eventAccess',
                    views: {
                        "ActivityPermissionH.EventAccess": {
                            controller: 'ActivityPermissionEventAccessController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_ActivityPermissionEventAccess'
                        }
                    },                    
                    resolve: {
                        eventAccessData: function (PlanningDataService, $stateParams) {
                            return PlanningDataService.getDataByFullUrl('/api/pln/PlnActvtyPermission/GetEventAccess?pSC_USER_ID=' + $stateParams.pSC_USER_ID);
                        }
                    },
                    Title: 'Planning Activity Permission',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ActivityPermissionH.ResourceAccess',
                config: {
                    url: '/resourceAccess',
                    views: {
                        "ActivityPermissionH.ResourceAccess": {
                            controller: 'ActivityPermissionResourceAccessController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_ActivityPermissionResourceAccess'
                        }
                    },
                    resolve: {
                        resourceAccessData: function (PlanningDataService, $stateParams) {
                            return PlanningDataService.getDataByFullUrl('/api/pln/PlnActvtyPermission/GetPlnRsurcAccessFlrLnByUser?pLK_PFLR_TYP_ID=0&pSC_USER_ID=' + $stateParams.pSC_USER_ID);
                        }
                    },
                    Title: 'Planning Activity Permission',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ActivityPermissionH.OrderAccess',
                config: {
                    url: '/orderAccess',
                    views: {
                        "ActivityPermissionH.OrderAccess": {
                            controller: 'ActivityPermissionOrderAccessController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_ActivityPermissionOrderAccess'
                        }
                    },
                    Title: 'Planning Activity Permission',
                    reloadOnSearch: false
                }
            },
            {
                state: 'OrderItemListLnPln',
                config: {
                    url: '/OrderItemList/:pMC_BYR_ACC_ID',
                    views: {
                        "GmtLineLoadPlan": {
                            controller: 'OrderItemListLnPlnController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_OrderItemListLnPln'
                        }
                    },
                    Title: 'Order List',
                    pOption: 3009,
                    resolve: {
                        BuyerAccData: function (PlanningDataService, $stateParams) {
                            return PlanningDataService.getDataByFullUrl('/api/pln/PlanCommon/getBuyerAccListByUser');
                        }
                    },
                }
            },
            {
                state: 'OrderItemListLnPln.list',
                config: {
                    url: '/list?pFIRSTDATE&pLASTDATE&page&pageSize&pMONTHOF',
                    views: {
                        "TabInside": {
                            controller: 'OrderItemListLnPlnDController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_OrderItemListLnPlnD'
                        }
                    },
                    Title: 'Order List',
                    pOption: 3009
                }
            },

            {
                state: 'CapacityBkDashboard',
                config: {
                    url: '/CapacityBkBoard',
                    views: {
                        "CapacityBkBoard": {
                            controller: 'CapacityBkDashboardController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_CapacityBkBoard'
                        }
                    },
                    
                    reloadOnSearch: false
                }
            },
            {
                state: 'CapacityBkDashboard.details',
                config: {
                    url: '/:pGMT_PROD_PLN_CLNDR_ID/details',
                    views: {
                        "TabInside": {
                            controller: 'CapacityBkDashboardDtlController',
                            controllerAs: 'vm',
                            templateUrl: '/Planning/pln/_CapacityBkBoardDtl'
                        }
                    },
                    reloadOnSearch: false,
                    Title: 'Capacity Booking Dashboard',
                }
            },
        {
            state: 'CapacityBkDashboard.details.board',
            config: {
                url: '/board?pGMT_PROD_PLN_CLNDR_ID_WK',
                views: {
                    "TabInsideD": {
                        controller: 'CapacityBkDashboardDtlChartController',
                        controllerAs: 'vm',
                        templateUrl: '/Planning/pln/_CapacityBkBoardDtlChart'
                    }
                },
                reloadOnSearch: true,
                Title: 'Capacity Booking Dashboard',
            }
        },
        {
            state: 'OrderItemListLnPlnItm',
            config: {
                url: '/OrderItemListItm/:pMC_BYR_ACC_ID',
                views: {
                    "GmtLineLoadPlan": {
                        controller: 'OrderItemListLnPlnController',
                        controllerAs: 'vm',
                        templateUrl: '/Planning/pln/_OrderItemListLnPln'
                    }
                },
                Title: 'Order List',
                pOption: 3012,
                resolve: {
                    BuyerAccData: function (PlanningDataService, $stateParams) {
                        return PlanningDataService.getDataByFullUrl('/api/pln/PlanCommon/getBuyerAccListByUser');
                    }
                },
            }
        },

        {
            state: 'OrderItemListLnPlnItm.Itemlist',
            config: {
                url: '/listItm?pFIRSTDATE&pLASTDATE&page&pageSize&pMONTHOF',
                views: {
                    "TabInside": {
                        controller: 'OrderItemListLnPlnDController',
                        controllerAs: 'vm',
                        templateUrl: '/Planning/pln/_OrderItemListLnPlnD'
                    }
                },
                Title: 'Order List',
                pOption: 3012
            }
        },
        {
            state: 'ByrWiseCapctyAlloc',
            config: {
                url: '/byrCapctyAlloc?pRF_FISCAL_YEAR_ID&pGMT_PROD_PLN_CLNDR_ID',
                views: {
                    "ByrWiseCapctyAlloc": {
                        controller: 'ByrWiseCapctyAllocController',
                        controllerAs: 'vm',
                        templateUrl: '/Planning/pln/_ByrWiseCapctyAlloc'
                    }
                },
                Title: 'Buyer Wise Capacity Allocation'
            }
        },



        ];
    }
})();
