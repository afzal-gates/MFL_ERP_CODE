(function () {
    'use strict';

    angular
        .module('multitex.knitting')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [

            {
                state: 'FabProdKnitOrder',
                config: {
                    url: '/FabProdKnitOrder?pMC_FAB_PROD_ORD_H_ID',
                    Title: 'Fabric Production Knitting Order (Bulk)',
                    controller: 'FabricProductionKnittingOrderController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_FabProdKnitOrder',
                    data: {
                        RF_FAB_PROD_CAT_ID_LST: '2,4,5,6,8,9,10',
                        DEFAULT_CAT_ID: '2',
                        state: 'FabProdKnitOrder',
                        LK_COL_TYPE_ID_LST: '358,361,407,432'
                        //358 Solid;360 YD;361 AOP, 407 Mellange, 432 Mixed
                    }
                }
            },
            {
                state: 'FabProdKnitOrderSample',
                config: {
                    url: '/FabProdKnitOrderSample?pMC_FAB_PROD_ORD_H_ID&pRF_FAB_PROD_CAT_ID',
                    Title: 'Fabric Production Knitting Order (Sample)',
                    controller: 'FabricProductionKnittingOrderController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_FabProdKnitOrder',
                    data: {
                        RF_FAB_PROD_CAT_ID_LST: '1,3,10',
                        DEFAULT_CAT_ID: '1',
                        state: 'FabProdKnitOrderSample',
                        LK_COL_TYPE_ID_LST: '358,361,407,432'
                    }
                }
            },
            {
                state: 'FabProdKnitOrderYD',
                config: {
                    url: '/FabProdKnitOrderYD?pMC_FAB_PROD_ORD_H_ID&pRF_FAB_PROD_CAT_ID',
                    Title: 'Fabric Production Knitting Order (YD)',
                    controller: 'FabricProductionKnittingOrderController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_FabProdKnitOrder',
                    data: {
                        RF_FAB_PROD_CAT_ID_LST: '1,2,3,4,5,6,8,9',
                        DEFAULT_CAT_ID: '2',
                        state: 'FabProdKnitOrderYD',
                        LK_COL_TYPE_ID_LST: '360',
                    }
                }
            },
            {
                state: 'YarnPurReq',
                config: {
                    url: '/YarnPurReq?pMC_FAB_PROD_ORD_H_ID&pSCM_PURC_REQ_H_ID',
                    Title: 'Yarn Purchase Requisition',
                    controller: 'YarnPurchaseRequisitionController',
                    templateUrl: '/Knitting/Knit/YarnPurchaseRequisition',
                    controllerAs: 'vm',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pMC_FAB_PROD_ORD_H_ID) && $stateParams.pMC_FAB_PROD_ORD_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/Select?pMC_FAB_PROD_ORD_H_ID=' + ($stateParams.pMC_FAB_PROD_ORD_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                            else if (angular.isDefined($stateParams.pSCM_PURC_REQ_H_ID) && $stateParams.pSCM_PURC_REQ_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetYarnBuffReqInfo?pSCM_PURC_REQ_H_ID=' + ($stateParams.pSCM_PURC_REQ_H_ID || 0)).then(function (res) {
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
                state: 'GenBuffYarnRequisition',
                config: {
                    url: '/GenBuffYarnRequisition?pSCM_PURC_REQ_H_ID',
                    controller: 'GeneralBufferYarnRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_GenBuffYarnRequisition',
                    Title: 'General Buffer Yarn Requisition',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_PURC_REQ_H_ID) && $stateParams.pSCM_PURC_REQ_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetYarnBuffReqInfo?pSCM_PURC_REQ_H_ID=' + ($stateParams.pSCM_PURC_REQ_H_ID || 0)).then(function (res) {
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
                state: 'GenBuffYarnRequisitionList',
                config: {
                    url: '/GenBuffYarnRequisitionList',
                    controller: 'GeneralBufferYarnRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_GenBuffYarnRequisitionList',
                    Title: 'General Buffer Yarn Requisition List'
                }
            },
            {
                state: 'YarnReceiveList',
                config: {
                    url: '/YarnReceiveList',
                    controller: 'YarnReceiveListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnReceiveList',
                    Title: 'Yarn Receive List'
                }
            },
            {
                state: 'YarnReceive',
                config: {
                    url: '/YarnReceive?pKNT_YRN_RCV_H_ID',
                    controller: 'YarnReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnReceive',
                    Title: 'Yarn Receive',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_RCV_H_ID) && $stateParams.pKNT_YRN_RCV_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetYarnReceiveInfo?pKNT_YRN_RCV_H_ID=' + ($stateParams.pKNT_YRN_RCV_H_ID || 0)).then(function (res) {
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
                state: 'YarnReceiveView',
                config: {
                    url: '/YarnReceiveView?pKNT_YRN_RCV_H_ID',
                    controller: 'YarnReceiveViewController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnReceiveView',
                    Title: 'Yarn Receive View',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_RCV_H_ID) && $stateParams.pKNT_YRN_RCV_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetYarnReceiveInfo?pKNT_YRN_RCV_H_ID=' + ($stateParams.pKNT_YRN_RCV_H_ID || 0)).then(function (res) {
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
                state: 'YarnOpeningBalance',
                config: {
                    url: '/YarnOpeningBalance',
                    controller: 'YarnOpeningBalanceController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnOpeningBalance',
                    Title: 'Yarn Opening Balance'
                }
            },


             /////////// End for Supplier Profile

            {
                state: 'KnitPlan',
                config: {
                    url: '/KnitPlan?pMC_FAB_PROD_ORD_D_ID&pRF_FAB_PROD_CAT_ID&state&pCT_ID_LST',
                    views: {
                        "KnitPlanContainer": {
                            controller: 'KnitPlanController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlan'
                        },

                    },
                    resolve: {
                        KnitPlanHeader: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pMC_FAB_PROD_ORD_D_ID && $stateParams.pMC_FAB_PROD_ORD_D_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getKnitPlanHeaderData?pMC_FAB_PROD_ORD_D_ID=' + $stateParams.pMC_FAB_PROD_ORD_D_ID + '&pRF_FAB_PROD_CAT_ID=' + $stateParams.pRF_FAB_PROD_CAT_ID);
                            }
                            else {
                                return {};
                            }
                        },
                        $modalInstance: function () {
                            return null;
                        }
                    },
                    Title: 'Knit Plan',
                    reloadOnSearch: false

                },
            },
            {
                state: 'JobCardDashboard',
                config: {
                    url: '/JobCardDashboard?pMC_FAB_PROD_ORD_H_ID',
                    controller: 'JobCardDashboardController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_JobCardDashboard',
                    Title: 'Knit Card Dashboard',
                    data: {
                        RF_FAB_PROD_CAT_ID_LST: '2,3,4,5',
                        DEFAULT_CAT_ID: '2',
                        state: 'JobCardDashboard',
                        LK_COL_TYPE_ID_LST: '358,361,407,432'
                        //358 Solid;360 YD;361 AOP, 407 Mellange, 432 Mixed
                    }

                }

            },
            {
                state: 'JobCardDashboardSample',
                config: {
                    url: '/JobCardDashboardSample?pMC_FAB_PROD_ORD_H_ID',
                    controller: 'JobCardDashboardController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_JobCardDashboard',
                    Title: 'Knit Card Dashboard- Sample',
                    data: {
                        RF_FAB_PROD_CAT_ID_LST: '1,3',
                        DEFAULT_CAT_ID: '1',
                        state: 'JobCardDashboardSample',
                        LK_COL_TYPE_ID_LST: '358,361,407,432'
                    }
                }

            },

            {
                state: 'JobCardDashboardYD',
                config: {
                    url: '/JobCardDashboardYD?pMC_FAB_PROD_ORD_H_ID',
                    controller: 'JobCardDashboardController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_JobCardDashboard',
                    Title: 'Knit Card Dashboard- Sample',
                    data: {
                        RF_FAB_PROD_CAT_ID_LST: '1,2,3,4,5',
                        DEFAULT_CAT_ID: '1',
                        state: 'JobCardDashboardSample',
                        LK_COL_TYPE_ID_LST: '360'
                    }
                }

            },

            {
                state: 'KntSubContProgList',
                config: {
                    url: '/knittingSubcontProgList?pSCM_SUPPLIER_ID=0',
                    views: {
                        "KntSubContProgList": {
                            controller: 'KntSubContProgListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_knittingSubcontProgList'
                        }
                    },
                    Title: 'Knitting Sub-contract Program List'
                }
            },
            {
                state: 'KntSubContProgH',
                config: {
                    url: '/knittingSubcontProgH/:pKNT_SC_PRG_RCV_ID?pSCM_SUPPLIER_ID=0',
                    views: {
                        "KntSubContProgH": {
                            controller: 'KntSubContProgHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitSubcontProgH'
                        }
                    },
                    Title: 'Knitting Sub-contract Program',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SC_PRG_RCV_ID) && $stateParams.pKNT_SC_PRG_RCV_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetScRcvProgramByID?pKNT_SC_PRG_RCV_ID=' + ($stateParams.pKNT_SC_PRG_RCV_ID || 0)).then(function (res) {
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
                state: 'KntSubContProgH.YarnReceive',
                config: {
                    url: '/yarnReceive',
                    views: {
                        "KntSubContProgH.YarnReceive": {
                            controller: 'KntSubContProgYrnRcvController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitSubcontProgYrnRcv'
                        }
                    },
                    Title: 'Knitting Sub-contract Program',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SC_PRG_RCV_ID) && $stateParams.pKNT_SC_PRG_RCV_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetScYrnRcvHdrListByPrgID?pKNT_SC_PRG_RCV_ID=' + ($stateParams.pKNT_SC_PRG_RCV_ID || 0)).then(function (res) {
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
                state: 'KntSubContProgH.FabColorKP',
                config: {
                    url: '/fabColorKP',
                    views: {
                        "KntSubContProgH.FabColorKP": {
                            controller: 'KntSubContProgFabColorKPController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitSubcontProgFabColorKP'
                        }
                    },
                    Title: 'Knitting Sub-contract Program'
                }
            },
            {
                state: 'KnitPlanJobCard',
                config: {
                    url: '/JobCard?pKNT_PLAN_H_ID&pKNT_JOB_CRD_H_ID&pKNT_SC_PRG_ISS_ID&pIS_SUB_CONTR&state&pMC_FAB_PROD_ORD_H_ID&pRF_FAB_PROD_CAT_ID&pCT_ID_LST&pSC_PROG_STATE',
                    views: {
                        "KnitPlanContainer": {
                            controller: 'KnitPlanJobCardController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanJobCard'
                        }
                    },
                    resolve: {
                        JobCardHeader: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_PLAN_H_ID && $stateParams.pKNT_PLAN_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardHeaderData?pKNT_PLAN_H_ID=' + $stateParams.pKNT_PLAN_H_ID + '&pKNT_JOB_CRD_H_ID=' + ($stateParams.pKNT_JOB_CRD_H_ID || null));
                            }
                            else {
                                return {};
                            }
                        },
                        YarnLotList: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_PLAN_H_ID && $stateParams.pKNT_PLAN_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLotForJobCard?pKNT_PLAN_H_ID=' + $stateParams.pKNT_PLAN_H_ID);
                            } else {
                                return [];
                            }

                        }
                    },
                    Title: 'Knit Card'
                }
            },
            {
                state: 'ScProgramListSolid',
                config: {
                    url: '/ScProgramListSolid?pPRG_ISS_NO',
                    views: {
                        "ScProgramListContainer": {
                            controller: 'ScProgramListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScProgramList'
                        }
                    },
                    data: {
                        IS_YD: 'N',
                        RF_FAB_PROD_CAT_ID_LST: '2,6,8,9',
                        SC_PROG_STATE: 'ScProgramListSolid'
                    },
                    Title: 'Sub-Contract Program List for Bulk'
                }
            },
            {
                state: 'ScProgramListSolid4Smpl',
                config: {
                    url: '/ScProgramListSolid4Smpl?pPRG_ISS_NO',
                    views: {
                        "ScProgramListContainer": {
                            controller: 'ScProgramListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScProgramList'
                        }
                    },
                    data: {
                        IS_YD: 'N',
                        RF_FAB_PROD_CAT_ID_LST: '1,3',
                        SC_PROG_STATE: 'ScProgramListSolid4Smpl'
                    },
                    Title: 'Sub-Contract Program List for Sample'
                }
            },
            {
                state: 'ScProgramListYd',
                config: {
                    url: '/ScProgramListYd?pPRG_ISS_NO',
                    views: {
                        "ScProgramListContainer": {
                            controller: 'ScProgramListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScProgramList'
                        }
                    },
                    data: {
                        IS_YD: 'Y',
                        RF_FAB_PROD_CAT_ID_LST: '1,2,3,5,6,8,9,10'
                    },
                    Title: 'Sub-Contract Program List'
                }
            },

            {
                state: 'YarnIssueList',
                config: {
                    url: '/YarnIssueList',
                    controller: 'YarnIssueListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnIssueList',
                    Title: 'Yarn Issue List'
                }
            },
            {
                state: 'YarnIssue',
                config: {
                    url: '/YarnIssue?pKNT_YRN_ISS_H_ID&pKNT_YRN_STR_REQ_H_ID&pRF_REQ_TYPE_ID',
                    controller: 'YarnIssueController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnIssue',
                    Title: 'Yarn Issue',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_ISS_H_ID) && $stateParams.pKNT_YRN_ISS_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnIssueInfo?pKNT_YRN_ISS_H_ID=' + ($stateParams.pKNT_YRN_ISS_H_ID || 0)).then(function (res) {
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
                state: 'KnitPlanJobCardRollProd',
                config: {
                    url: '/KnitPlanJobCardRollProd?pJOB_CRD_NO&pKNT_JOB_CRD_H_ID',
                    views: {
                        "KnitPlanJobCardRollContainer": {
                            controller: 'KnitPlanJobCardRollProdController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanJobCardRollProd'
                        }
                    },
                    resolve: {
                        JobCardHeader: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_JOB_CRD_H_ID && $stateParams.pKNT_JOB_CRD_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardHeaderData?pKNT_JOB_CRD_H_ID=' + $stateParams.pKNT_JOB_CRD_H_ID);
                            }
                            else {
                                return {
                                    KNT_JOB_CRD_H_ID: -1
                                };
                            }
                        }
                    },
                    Title: 'Roll Production'
                }
            },

            {
                state: 'KntMCLoadingPlan',
                config: {
                    url: '/KntMCLoadingPlan?pHR_COMPANY_ID&pHR_PROD_BLDNG_ID&pHR_PROD_FLR_ID&pKNT_MACHINE_ID',
                    views: {
                        "KntMCLoadingPlanContainer": {
                            controller: 'KntMCLoadingPlanController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntMCLoadingPlan'
                        }
                    },
                    Title: 'Machine Loading Plan'
                }
            },
            {
                state: 'KntMachnOpratorAssign',
                config: {
                    url: '/kntMachnOprator',
                    views: {
                        "KntMachnOpratorAssign": {
                            controller: 'KntMachnOpratorAssignController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntMachnOpratorAssign'
                        }
                    },
                    Title: 'Machine Wise Operator Assign'
                }
            },
            {
                state: 'KntReportParams',
                config: {
                    url: '/kntReportParams',
                    views: {
                        "KntReportParams": {
                            controller: 'KntReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntReportParams'
                        }
                    },
                    Title: 'Knitting Reports',
                    resolve: {
                        kntRptData: function (KnittingDataService) {
                            return KnittingDataService.getDataByUrlNoToken('/api/common/getReportDataListByUser/' + 4); //// Here 1 is report group ID
                        }
                    }
                }
            },
            {
                state: 'KntYrnStkList',
                config: {
                    url: '/kntYrnStk',
                    views: {
                        "KntYrnStkList": {
                            controller: 'KntYrnStkListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntYrnStkList'
                        }
                    },
                    Title: 'Yarn Current Stock'
                }
            },
            {
                state: 'YarnIssueReqList',
                config: {
                    url: '/YarnIssueReqList',
                    controller: 'YarnIssueRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnIssueReqList',
                    Title: 'Yarn Issue Requisition List'
                }
            },
            {
                state: 'YarnIssueReq',
                config: {
                    url: '/YarnIssueReq?pKNT_YRN_STR_REQ_H_ID',
                    controller: 'YarnIssueRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnIssueReq',
                    Title: 'Yarn Issue Requisition',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pKNT_YRN_STR_REQ_H_ID) && $stateParams.pKNT_YRN_STR_REQ_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/GetYarnIssueReqInfo?pKNT_YRN_STR_REQ_H_ID=' + ($stateParams.pKNT_YRN_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'YarnIssueRtnReqList',
                config: {
                    url: '/YarnIssueRtnReqList',
                    controller: 'YarnIssueReturnRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnIssueRtnReqList',
                    Title: 'Yarn Return to Store List'
                }
            },
            {
                state: 'YarnIssueRtnReq',
                config: {
                    url: '/YarnIssueRtnReq?pKNT_YRN_ISS_RET_H_ID',
                    controller: 'YarnIssueReturnRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnIssueRtnReq',
                    Title: 'Yarn Return to Store',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_ISS_RET_H_ID) && $stateParams.pKNT_YRN_ISS_RET_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReturn/GetYarnIssueRtnReqInfo?pKNT_YRN_ISS_RET_H_ID=' + ($stateParams.pKNT_YRN_ISS_RET_H_ID || 0)).then(function (res) {
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
                state: 'YarnIssueRtnList',
                config: {
                    url: '/YarnIssueRtnList',
                    controller: 'YarnIssueReturnListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnIssueRtnList',
                    Title: 'Yarn Return to Store List'
                }
            },
            {
                state: 'YarnIssueRtn',
                config: {
                    url: '/YarnIssueRtn?pKNT_YRN_ISS_RET_H_ID',
                    controller: 'YarnIssueReturnController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnIssueRtn',
                    Title: 'Yarn Return to Store',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_ISS_RET_H_ID) && $stateParams.pKNT_YRN_ISS_RET_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReturn/GetYarnIssueRtnReqInfo?pKNT_YRN_ISS_RET_H_ID=' + ($stateParams.pKNT_YRN_ISS_RET_H_ID || 0)).then(function (res) {
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
                state: 'YarnTest',
                config: {
                    url: '/YarnTest?pKNT_YRN_STR_REQ_H_ID',
                    controller: 'YarnTestController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnTest',
                    Title: 'Yarn Test',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_STR_REQ_H_ID) && $stateParams.pKNT_YRN_STR_REQ_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/GetYarnIssueReqInfo?pKNT_YRN_STR_REQ_H_ID=' + ($stateParams.pKNT_YRN_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'KntGreyFabRcv',
                config: {
                    url: '/greyFabRcvH',
                    views: {
                        "KntGreyFabRcvH": {
                            controller: 'KntGreyFabRcvController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntGreyFabRcv'
                        }
                    },
                    Title: 'Grey Fabric Receive'
                }
            },
            {
                state: 'KntScGreyFabDelvH',
                config: {
                    url: '/scGreyFabDelvH/:pKNT_SC_GFAB_DLV_H_ID',
                    views: {
                        "KntScGreyFabDelvH": {
                            controller: 'KntGreyFabDelvHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntScGreyFabDelvH'
                        }
                    },
                    Title: 'Grey Fabric Delivery to Party',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SC_GFAB_DLV_H_ID) && $stateParams.pKNT_SC_GFAB_DLV_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KntGreyFabDelv/GetScGreyFabDelvHdr?pKNT_SC_GFAB_DLV_H_ID=' + ($stateParams.pKNT_SC_GFAB_DLV_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                            else {
                                return {};
                            }
                        },
                        woRefList: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SC_GFAB_DLV_H_ID) && $stateParams.pKNT_SC_GFAB_DLV_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KntGreyFabDelv/GetScGreyFabDelvHdr?pKNT_SC_GFAB_DLV_H_ID=' + ($stateParams.pKNT_SC_GFAB_DLV_H_ID || 0)).then(function (res) {
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
                state: 'KntScGreyFabDelvH.FabDelv',
                config: {
                    url: '/delvDtl',
                    views: {
                        "KntScGreyFabDelvH.FabDelv": {
                            controller: 'KntGreyFabDelvDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntScGreyFabDelvD'
                        }
                    },
                    Title: 'Grey Fabric Delivery to Party',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntScGreyFabDelvH.YrnRtn',
                config: {
                    url: '/yrnRtnDtl',
                    views: {
                        "KntScGreyFabDelvH.YrnRtn": {
                            controller: 'KntSciYrnRtnDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntSciYrnRtnD'
                        }
                    },
                    Title: 'Grey Fabric Delivery to Party',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntScGreyFabDelvList',
                config: {
                    url: '/delvList?pSCM_SUPPLIER_ID',
                    views: {
                        "KntScGreyFabDelvList": {
                            controller: 'KntGreyFabDelvListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntScGreyFabDelvList'
                        }
                    },
                    Title: 'Grey Fabric Delivery to Party List'
                }
            },
            {
                state: 'YarnTestReqList',
                config: {
                    url: '/YarnTestReqList',
                    controller: 'YarnTestReqListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnTestReqList',
                    Title: 'Yarn Test Requisition List'
                }
            },
            {
                state: 'YarnTestReq',
                config: {
                    url: '/YarnTestReq?pKNT_YRN_STR_REQ_H_ID',
                    controller: 'YarnTestReqController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnTestReq',
                    Title: 'Yarn Test Requisition',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_STR_REQ_H_ID) && $stateParams.pKNT_YRN_STR_REQ_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/GetYarnIssueReqInfo?pKNT_YRN_STR_REQ_H_ID=' + ($stateParams.pKNT_YRN_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'YarnTestIssueList',
                config: {
                    url: '/YarnTestIssueList',
                    controller: 'YarnTestIssueListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnTestIssueList',
                    Title: 'Yarn Test Issue List'
                }
            },
            {
                state: 'YarnTestIssue',
                config: {
                    url: '/YarnTestIssue?pKNT_YRN_ISS_H_ID&pKNT_YRN_STR_REQ_H_ID',
                    controller: 'YarnTestIssueController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnTestIssue',
                    Title: 'Yarn Test Issue',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_ISS_H_ID) && $stateParams.pKNT_YRN_ISS_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssue/GetYarnIssueInfo?pKNT_YRN_ISS_H_ID=' + ($stateParams.pKNT_YRN_ISS_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                                //else if ($stateParams.pKNT_YRN_STR_REQ_H_ID)
                                //    KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/GetYarnIssueReqInfo?pKNT_YRN_STR_REQ_H_ID=' + ($stateParams.pKNT_YRN_STR_REQ_H_ID || 0)).then(function (res) {
                                //        return res;
                                //    });
                            else {
                                return {};
                            }
                        }
                    }
                }
            },
            {
                state: 'KntMachineProfile',
                config: {
                    url: '/kntMachPro',
                    views: {
                        "KntMachineProfile": {
                            controller: 'KntMachineProfileController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntMachineProfile'
                        }
                    },
                    Title: 'Knit Machine Profile'
                }
            },
            {
                state: 'YarnApprovalWithoutTest',
                config: {
                    url: '/YarnApprovalWithoutTest',
                    controller: 'YarnApprovalWithoutTestController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnApprovalWithoutTest',
                    Title: 'Yarn Approval Without Test'
                }
            },
            {
                state: 'YarnTestIssueRtnReqList',
                config: {
                    url: '/YarnTestIssueRtnReqList',
                    controller: 'YarnTestIssueReturnRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnTestIssueRtnReqList',
                    Title: 'Yarn Test Issue Return List'
                }
            },
            {
                state: 'YarnTestIssueRtnReq',
                config: {
                    url: '/YarnTestIssueRtnReq?pKNT_YRN_ISS_RET_H_ID',
                    controller: 'YarnTestIssueReturnRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnTestIssueRtnReq',
                    Title: 'Yarn Test Issue Return Requisition',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_ISS_RET_H_ID) && $stateParams.pKNT_YRN_ISS_RET_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReturn/GetYarnIssueRtnReqInfo?pKNT_YRN_ISS_RET_H_ID=' + ($stateParams.pKNT_YRN_ISS_RET_H_ID || 0)).then(function (res) {
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
                state: 'YarnTestIssueRtn',
                config: {
                    url: '/YarnTestIssueRtn?pKNT_YRN_ISS_RET_H_ID',
                    controller: 'YarnTestIssueReturnController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnTestIssueRtn',
                    Title: 'Yarn Test Issue Return',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_ISS_RET_H_ID) && $stateParams.pKNT_YRN_ISS_RET_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReturn/GetYarnIssueRtnReqInfo?pKNT_YRN_ISS_RET_H_ID=' + ($stateParams.pKNT_YRN_ISS_RET_H_ID || 0)).then(function (res) {
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
                state: 'KnitCardRpt',
                config: {
                    url: '/KnitCardRpt?pKNT_JOB_CRD_H_ID',
                    controller: 'KnitCardRptController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitCardRpt',
                    Title: 'Knit Card'
                }
            },

            {
                state: 'KnitPlanJobCardSR',
                config: {
                    url: '/JobCardSR?pKNT_YRN_STR_REQ_H_ID',
                    views: {
                        "KnitPlanContainer": {
                            controller: 'KnitPlanJobCardSRController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanJobCardSR'
                        }
                    },
                    resolve: {
                        JobCardHeader: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_PLAN_H_ID && $stateParams.pKNT_PLAN_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardHeaderData?pKNT_PLAN_H_ID=' + $stateParams.pKNT_PLAN_H_ID + '&pKNT_JOB_CRD_H_ID=' + ($stateParams.pKNT_JOB_CRD_H_ID || null));
                            }
                            else {
                                return {};
                            }
                        },
                        YarnLotList: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_PLAN_H_ID && $stateParams.pKNT_PLAN_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLotForJobCard?pKNT_PLAN_H_ID=' + $stateParams.pKNT_PLAN_H_ID);
                            } else {
                                return [];
                            }
                        }
                    },
                    Title: 'Sample Yarn Requisition',
                    data: {
                        LK_COL_TYPE_ID_LST: '358,361,407,432'
                    }
                }
            },

            {
                state: 'KntCollarCuffOrdReqList',
                config: {
                    url: '/clcfOrdReq?pMC_ORDER_H_ID',
                    views: {
                        "KntCollarCuffOrdReqList": {
                            controller: 'KntCollarCuffOrdReqController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_CollarCuffOrderReqList'
                        }
                    },
                    Title: 'Order wise Collar Cuff Requirement'
                }
            },
            {
                state: 'KntCollarCuffOrdProdH',
                config: {
                    url: '/collarCuffOrdProdH/:pRF_FAB_PROD_CAT_ID/:pMC_BYR_ACC_GRP_ID/:pMC_STYLE_H_EXT_ID/:pMC_COLOR_ID?pMC_ORDER_H_ID_LST&pPROD_DT&pIS_NEXT',
                    views: {
                        "KntCollarCuffOrdProdH": {
                            controller: 'kntCollarCuffOrdProdHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_CollarCuffOrderProdH'
                        }
                    },
                    resolve: {
                        clcfDesignData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pMC_STYLE_H_EXT_ID && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KntCollarCuff/GetCollarCuffDesigType?pRF_FAB_PROD_CAT_ID=' + $stateParams.pRF_FAB_PROD_CAT_ID + '&pMC_STYLE_H_EXT_ID=' + $stateParams.pMC_STYLE_H_EXT_ID + '&pMC_COLOR_ID=' + $stateParams.pMC_COLOR_ID);
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Order Wise Collar Cuff Production',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntCollarCuffOrdProdH.dtl',
                config: {
                    url: '/collarCuffProdDtl',
                    views: {
                        "KntCollarCuffOrdProdH.dtl": {
                            controller: 'kntCollarCuffOrdProdDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_CollarCuffOrderProdD'
                        }
                    },
                    resolve: {
                        collarCuffProdData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pMC_STYLE_H_EXT_ID && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KntCollarCuff/GetCollarCuffProd?pRF_FAB_PROD_CAT_ID=' + $stateParams.pRF_FAB_PROD_CAT_ID + '&pMC_STYLE_H_EXT_ID=' + $stateParams.pMC_STYLE_H_EXT_ID + '&pMC_COLOR_ID=' + $stateParams.pMC_COLOR_ID + '&pPROD_DT=' + $stateParams.pPROD_DT);
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Order Wise Collar Cuff Production',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitPlanJobCardCollarCuff',
                config: {
                    url: '/JobCardCollarCuff?pKNT_YRN_STR_REQ_H_ID&pKNT_PLAN_H_ID',
                    views: {
                        "KnitPlanContainer": {
                            controller: 'KnitPlanJobCardCollarCuffController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanJobCardCollarCuff'
                        }
                    },
                    Title: 'Yarn Requisition For Collar And/Or Cuff',
                    data: {
                        LK_COL_TYPE_ID_LST: '358,361,407,432'
                    }
                }
            },
            {
                state: 'KnitPlanJobCardCollarCuffSco',
                config: {
                    url: '/JobCardCollarCuffSco?pKNT_YRN_STR_REQ_H_ID&pKNT_PLAN_H_ID&pKNT_SCO_CLCF_PRG_H_ID&pMC_FAB_PROD_ORD_H_ID',
                    views: {
                        "KnitPlanClrCufSco": {
                            controller: 'KntPlnJobCardCollarCuffScoController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanJobCardCollarCuffSco'
                        }
                    },
                    Title: 'Yarn Requisition For Collar And/Or Cuff',
                    data: {
                        LK_COL_TYPE_ID_LST: '358,361,407,432'
                    }
                }
            },
            {
                state: 'KnitDailyProduction',
                config: {
                    url: '/dailyProdList',
                    views: {
                        "KnitDailyProductionList": {
                            controller: 'KntDailyProdListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitDailyProductionList'
                        }
                    },
                    Title: 'Daily Production List'
                }
            },

            {
                state: 'KntCollarCuffStrRcvH4Smpl',
                config: {
                    url: '/collarCuffStrRcv4Smpl?pKNT_SCO_CHL_RCV_H_ID=0',
                    views: {
                        "KntCollarCuffStrRcvH": {
                            controller: 'KntCollarCuffStrRcvHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_CollarCuffStrRcvH'
                        }
                    },
                    data: {
                        RF_FAB_PROD_CAT_ID_LST: '1,3',
                        CLCF_SRC_PROD_CAT_ID: 1
                    },
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_SCO_CHL_RCV_H_ID && $stateParams.pKNT_SCO_CHL_RCV_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/KntCollarCuff/GetClcfInternalChlnHdr?pKNT_SCO_CHL_RCV_H_ID=' + ($stateParams.pKNT_SCO_CHL_RCV_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Collar Cuff Transfer/Recieve at fabric Store (Sample)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntCollarCuffStrRcvList4Smpl',
                config: {
                    url: '/collarCuffStrRcvList4Smpl',
                    views: {
                        "KntCollarCuffStrRcvList": {
                            controller: 'KntCollarCuffStrRcvListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_CollarCuffStrRcvList'
                        }
                    },
                    data: {
                        CLCF_SRC_PROD_CAT_ID: 1
                    },
                    Title: 'Collar Cuff Transfer/Recieve at fabric Store (Sample)',
                    reloadOnSearch: false
                }
            },

            {
                state: 'KntCollarCuffStrRcvH',
                config: {
                    url: '/collarCuffStrRcv?pKNT_SCO_CHL_RCV_H_ID=0',
                    views: {
                        "KntCollarCuffStrRcvH": {
                            controller: 'KntCollarCuffStrRcvHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_CollarCuffStrRcvH'
                        }                        
                    },
                    data: {
                        RF_FAB_PROD_CAT_ID_LST: '2,4,5,6,9,10',
                        CLCF_SRC_PROD_CAT_ID: 2
                    },
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_SCO_CHL_RCV_H_ID && $stateParams.pKNT_SCO_CHL_RCV_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/KntCollarCuff/GetClcfInternalChlnHdr?pKNT_SCO_CHL_RCV_H_ID=' + ($stateParams.pKNT_SCO_CHL_RCV_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Collar Cuff Transfer/Recieve at fabric Store (Bulk)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntCollarCuffStrRcvList',
                config: {
                    url: '/collarCuffStrRcvList',
                    views: {
                        "KntCollarCuffStrRcvList": {
                            controller: 'KntCollarCuffStrRcvListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_CollarCuffStrRcvList'
                        }
                    },
                    data: {
                        CLCF_SRC_PROD_CAT_ID: 2
                    },
                    Title: 'Collar Cuff Transfer/Recieve at fabric Store (Bulk)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitFabricStoreStk',
                config: {
                    url: '/fabStrStkList',
                    views: {
                        "KnitFabricStoreStk": {
                            controller: 'KntFabricStoreStkController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitFabricStoreStkList'
                        }
                    },
                    Title: 'Fabric Store Stock List'
                }
            },
            {
                state: 'YarnRtnToSupReqList',
                config: {
                    url: '/YarnRtnToSupReqList',
                    controller: 'YarnRtnToSupReqListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnRtnToSupReqList',
                    Title: 'Requisition for Yarn Replace by Supplier List'
                }
            },
            {
                state: 'YarnRtnToSupReq',
                config: {
                    url: '/YarnRtnToSupReq?pKNT_YRN_STR_RPL_REQ_H_ID',
                    controller: 'YarnRtnToSupReqController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnRtnToSupReq',
                    Title: 'Requisition for Yarn Replace by Supplier',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_STR_RPL_REQ_H_ID) && $stateParams.pKNT_YRN_STR_RPL_REQ_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/Knit/YarnReturnToSupplier/GetReplacementReqByID?pKNT_YRN_STR_RPL_REQ_H_ID=' + ($stateParams.pKNT_YRN_STR_RPL_REQ_H_ID || 0)).then(function (res) {
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
                state: 'KntSmplFabProdOrdMnul',
                config: {
                    url: '/fabProdOrdMnul/:pMC_FAB_PROD_ORD_H_ID?&pRF_FAB_PROD_CAT_ID&pMC_BYR_ACC_GRP_ID&pMC_STYLE_H_EXT_ID',
                    views: {
                        "KntSmplFabProdOrdMnul": {
                            controller: 'KntSmplProdOrdMnulController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_SmplFabProdOrderMnul'
                        }
                    },
                    Title: 'Direct Sample Fabric Booking',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntShortFabProdOrdH',
                config: {
                    url: '/shortFabProdOrd/:pKNT_SRT_FAB_REQ_H_ID?&pMC_BYR_ACC_GRP_ID&pMC_STYLE_H_EXT_ID',
                    views: {
                        "KntShortFabProdOrdH": {
                            controller: 'KntShortProdOrdHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ShortFabProdOrderH'
                        }
                    },
                    Title: 'Short Fabric Booking',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SRT_FAB_REQ_H_ID) && $stateParams.pKNT_SRT_FAB_REQ_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/FabProdKnitOrder/GetSrtFabProdOrdHdr?&pKNT_SRT_FAB_REQ_H_ID=' + ($stateParams.pKNT_SRT_FAB_REQ_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntShortFabProdOrdH.SrtFabDtl',
                config: {
                    url: '/srtFabDtl',
                    views: {
                        "KntShortFabProdOrdH.SrtFabDtl": {
                            controller: 'KntShortProdOrdDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ShortFabProdOrderD'
                        }
                    },
                    //resolve: {
                    //    collarCuffProdData: function (KnittingDataService, $stateParams) {
                    //        if ($stateParams.pMC_STYLE_H_EXT_ID && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                    //            return KnittingDataService.getDataByFullUrl('/api/knit/KntCollarCuff/GetCollarCuffProd?pRF_FAB_PROD_CAT_ID=2&pMC_STYLE_H_EXT_ID=' + $stateParams.pMC_STYLE_H_EXT_ID + '&pMC_COLOR_ID=' + $stateParams.pMC_COLOR_ID + '&pPROD_DT=' + $stateParams.pPROD_DT);
                    //        }
                    //        else {
                    //            return {};
                    //        }
                    //    }
                    //},
                    Title: 'Short Fabric Booking',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntShortFabProdOrdH.Reason4SrtFab',
                config: {
                    url: '/srtFabReason',
                    views: {
                        "KntShortFabProdOrdH.Reason4SrtFab": {
                            controller: 'KntShortProdOrdReasonController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ShortFabProdOrderReason'
                        }
                    },
                    Title: 'Short Fabric Booking',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntShortFabProdOrdH.Resposibility4SrtFab',
                config: {
                    url: '/srtFabResposibility',
                    views: {
                        "KntShortFabProdOrdH.Resposibility4SrtFab": {
                            controller: 'KntShortProdOrdResposibilityController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ShortFabProdOrderResposibility'
                        }
                    },
                    Title: 'Short Fabric Booking',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntShortFabProdOrdList',
                config: {
                    url: '/shortFabProdOrdList?&pMC_BYR_ACC_GRP_ID&pMC_STYLE_H_EXT_ID',
                    views: {
                        "KntShortFabProdOrdList": {
                            controller: 'KntShortProdOrdListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ShortFabProdOrderList'
                        }
                    },
                    Title: 'Short Fabric Booking',
                    resolve: {
                        userLavelData: function (KnittingDataService, $stateParams) {
                            return KnittingDataService.getDataByFullUrl('/api/Knit/FabProdKnitOrder/GetSrtFabReqUserLavel');
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntShortFabProdOrdRpt',
                config: {
                    url: '/shortFabProdOrdRpt?pKNT_SRT_FAB_REQ_H_ID',
                    views: {
                        "KntShortFabProdOrdRpt": {
                            controller: 'ExcessFabReqRptController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ShortFabProdOrderRpt'
                        }
                    },
                    Title: 'Short Fabric Booking',
                    resolve: {
                        rptData: function (KnittingDataService, $stateParams) {
                            return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetSrtFabReqRpt?pKNT_SRT_FAB_REQ_H_ID=' + $stateParams.pKNT_SRT_FAB_REQ_H_ID);
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntSrtFabProdOrdAprovList',
                config: {
                    url: '/srtFabProdOrdAprovList?&pMC_BYR_ACC_GRP_ID&pMC_STYLE_H_EXT_ID',
                    views: {
                        "KntSrtFabProdOrdAprovList": {
                            controller: 'KntSrtProdOrdAprovListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_SrtFabProdOrdAprovList'
                        }
                    },
                    Title: 'Short Fabric Booking List (Approved)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScGreyFabRcvFormPartyH',
                config: {
                    url: '/fabRcvFrmParty/:pKNT_SCO_CHL_RCV_H_ID?pSCM_SUPPLIER_ID',
                    views: {
                        "ScGreyFabRcvFormPartyH": {
                            controller: 'KntScGreyFabRcvFromPartyHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScGreyFabRcvFromPartyH'
                        }
                    },
                    Title: 'S/C Grey Fabric Receive from Party',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SCO_CHL_RCV_H_ID) && $stateParams.pKNT_SCO_CHL_RCV_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/KntScChlnRcv/GetRcvChallanHdr?&pKNT_SCO_CHL_RCV_H_ID=' + ($stateParams.pKNT_SCO_CHL_RCV_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        },
                        //formScPrgIssList: function () {
                        //    return {};
                        //}
                        formScPrgIssList: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_SUPPLIER_ID) && $stateParams.pSCM_SUPPLIER_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/KntScChlnRcv/GetScPrgIssList?&pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScGreyFabRcvFormPartyH.FabRcv',
                config: {
                    url: '/fabRcv',
                    views: {
                        "ScGreyFabRcvFormPartyH.FabRcv": {
                            controller: 'KntScGreyFabRcvFromPartyFabRcvController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScGreyFabRcvFromPartyFabRcv'
                        }
                    },
                    Title: 'S/C Grey Fabric Receive from Party',
                    //resolve: {
                    //    favRcvListData: function (KnittingDataService, $stateParams) {
                    //        if (angular.isDefined($stateParams.pKNT_SCO_CHL_RCV_H_ID) && $stateParams.pKNT_SCO_CHL_RCV_H_ID > 0) {

                    //            return KnittingDataService.getDataByFullUrl('/api/Knit/KntScChlnRcv/GetFabRcvList?&pKNT_SCO_CHL_RCV_H_ID=' + ($stateParams.pKNT_SCO_CHL_RCV_H_ID || 0)).then(function (res) {
                    //                return res;
                    //            });
                    //        }
                    //        else {
                    //            return {};
                    //        }
                    //    }
                    //},
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScGreyFabRcvFormPartyH.YrnRcv',
                config: {
                    url: '/yrnRcv',
                    views: {
                        "ScGreyFabRcvFormPartyH.YrnRcv": {
                            controller: 'KntScGreyFabRcvFromPartyYrnRcvController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScGreyFabRcvFromPartyYrnRcv'
                        }
                    },
                    Title: 'S/C Grey Fabric Receive from Party',
                    //resolve: {
                    //    yrnRcvListData: function (KnittingDataService, $stateParams) {
                    //        if (angular.isDefined($stateParams.pKNT_SCO_CHL_RCV_H_ID) && $stateParams.pKNT_SCO_CHL_RCV_H_ID > 0) {

                    //            return KnittingDataService.getDataByFullUrl('/api/Knit/KntScChlnRcv/GetYrnRcvList?&pKNT_SCO_CHL_RCV_H_ID=' + ($stateParams.pKNT_SCO_CHL_RCV_H_ID || 0)).then(function (res) {
                    //                return res;
                    //            });
                    //        }
                    //        else {
                    //            return {};
                    //        }
                    //    }
                    //},
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScGreyFabRcvFormPartyH.ClcfRcv',
                config: {
                    url: '/clcfRcv',
                    views: {
                        "ScGreyFabRcvFormPartyH.ClcfRcv": {
                            controller: 'KntScGreyFabRcvFromPartyClcfRcvController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScGreyFabRcvFromPartyClcfRcv'
                        }
                    },
                    Title: 'S/C Grey Fabric Receive from Party',                    
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScGreyFabRcvFormPartyList',
                config: {
                    url: '/fabRcvFromPartyList?pSCM_SUPPLIER_ID',
                    views: {
                        "ScGreyFabRcvFromPartyList": {
                            controller: 'KntScGreyFabRcvFromPartyListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScGreyFabRcvFromPartyList'
                        }
                    },
                    Title: 'S/C Grey Fabric Receive from Party',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntPendingLoadingPlan',
                config: {
                    url: '/KntMCLoadingPlanPending?pHR_COMPANY_ID&pHR_PROD_BLDNG_ID&pHR_PROD_FLR_ID&pKNT_MACHINE_ID',
                    views: {
                        "KntMCLoadingPlanContainer": {
                            controller: 'KntMCLoadingPlanPendingController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KntMCLoadingPlanPending'
                        }
                    },
                    Title: 'Pending Knit Card: Loading Plan'
                }
            },
            {
                state: 'DailyTrimsReceiveList',
                config: {
                    url: '/DailyTrimsReceiveList',
                    controller: 'DailyTrimsReceiveListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_DailyTrimsReceiveList',
                    Title: 'Daily Trims Receive List'
                }
            },
            {
                state: 'DailyTrimsReceive',
                config: {
                    url: '/DailyTrimsReceive?pINV_TRMS_RCV_H_ID',
                    controller: 'DailyTrimsReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_DailyTrimsReceive',
                    Title: 'Daily Trims Receive',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pINV_TRMS_RCV_H_ID) && $stateParams.pINV_TRMS_RCV_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/DailyTrimsReceive/GetDailyTrimsReceiveInfo?pINV_TRMS_RCV_H_ID=' + ($stateParams.pINV_TRMS_RCV_H_ID || 0)).then(function (res) {
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
                state: 'DailyTrimsReceiveView',
                config: {
                    url: '/DailyTrimsReceiveView?pINV_TRMS_RCV_H_ID',
                    controller: 'DailyTrimsReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_DailyTrimsReceiveView',
                    Title: 'Daily Trims Receive',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pINV_TRMS_RCV_H_ID) && $stateParams.pINV_TRMS_RCV_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/DailyTrimsReceive/GetDailyTrimsReceiveInfo?pINV_TRMS_RCV_H_ID=' + ($stateParams.pINV_TRMS_RCV_H_ID || 0)).then(function (res) {
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
                state: 'ScYarnRcvChallanH',
                config: {
                    url: '/scYrnChln/:pKNT_SC_PRG_RCV_ID?pSCM_SUPPLIER_ID=0',
                    views: {
                        "ScYarnRcvChallanH": {
                            controller: 'ScYarnRcvChallanHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScYrnRcvChallanH'
                        }
                    },
                    Title: 'Sub-contract Yarn Receive Challan (In-house)',
                    reloadOnSearch: false,
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SC_PRG_RCV_ID) && $stateParams.pKNT_SC_PRG_RCV_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetScRcvProgramByID?pKNT_SC_PRG_RCV_ID=' + ($stateParams.pKNT_SC_PRG_RCV_ID || 0)).then(function (res) {
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
                state: 'ScYarnRcvChallanH.YarnReceive',
                config: {
                    url: '/chlnDtl',
                    views: {
                        "ScYarnRcvChallanH.YarnReceive": {
                            controller: 'ScYarnRcvChallanDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScYrnRcvChallanD'
                        }
                    },
                    Title: 'Sub-contract Yarn Receive Challan (In-house)',
                    reloadOnSearch: false,
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SC_PRG_RCV_ID) && $stateParams.pKNT_SC_PRG_RCV_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetScYrnRcvHdrListByPrgID?pKNT_SC_PRG_RCV_ID=' + ($stateParams.pKNT_SC_PRG_RCV_ID || 0)).then(function (res) {
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
                state: 'ScoGreyFabRtnToPartyH',
                config: {
                    url: '/scoFabRtntoParty/:pKNT_SCO_GFAB_RET_H_ID?pSCM_SUPPLIER_ID',
                    views: {
                        "ScoGreyFabRtnToPartyH": {
                            controller: 'KntScoGreyFabRtnToPartyHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoGreyFabRtnChlnToPartyH'
                        }
                    },
                    Title: 'Grey Fabric Return Challan (S/C Out-house)',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SCO_GFAB_RET_H_ID) && $stateParams.pKNT_SCO_GFAB_RET_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/KntScChlnRtn/GetScoRtnChalnHdr?&pKNT_SCO_GFAB_RET_H_ID=' + ($stateParams.pKNT_SCO_GFAB_RET_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        },
                        formScoRejFabList: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_SUPPLIER_ID) && $stateParams.pSCM_SUPPLIER_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/KntScChlnRtn/GetScoQcRejFabList4Rtn?&pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    }//,
                    //reloadOnSearch: false
                }
            },
            {
                state: 'ScoGreyFabRtnToPartyH.FabricReturn',
                config: {
                    url: '/fabRtn',
                    views: {
                        "ScoGreyFabRtnToPartyH.FabricReturn": {
                            controller: 'KntScoGreyFabRtnToPartyFabRtnController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoGreyFabRtnChlnToPartyFabRtn'
                        }
                    },
                    Title: 'Grey Fabric Return Challan (S/C Out-house)'//,
                    //reloadOnSearch: false
                }
            },
            {
                state: 'ScoGreyFabRtnToPartyList',
                config: {
                    url: '/fabRtnToPartyList?pSCM_SUPPLIER_ID',
                    views: {
                        "ScoGreyFabRtnToPartyList": {
                            controller: 'KntScoGreyFabRtnToPartyListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoGreyFabRtnChlnToPartyList'
                        }
                    },
                    Title: 'Grey Fabric Return Challan (S/C Out-house)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitYdProgram',
                config: {
                    url: '/KnitYdProgram?pKNT_YD_PRG_H_ID&pPARENT_ID&pREV',
                    views: {
                        "KnitYdProgram": {
                            controller: 'KnitYdProgramController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitYdProgram'
                        }
                    },
                    Title: 'Knitting Y/D Program',
                    resolve: {
                        YarnList: function (KnittingDataService) {
                            var url = '/KnitPlan/getYarnItemByFabId?pMC_STYLE_D_FAB_ID&pRF_FAB_PROD_CAT_ID=10&pKNT_SC_PRG_RCV_ID';
                            return KnittingDataService.getDataByUrl(url);
                        },
                        FormData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_YD_PRG_H_ID && $stateParams.pKNT_YD_PRG_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitYdProgram/Select?pKNT_YD_PRG_H_ID=' + $stateParams.pKNT_YD_PRG_H_ID);
                            }
                            else if ($stateParams.pPARENT_ID && $stateParams.pPARENT_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitYdProgram/Select?pPARENT_ID=' + $stateParams.pPARENT_ID);
                            } else {
                                return KnittingDataService.getDataByUrl('/KnitYdProgram/getProgramNoAuto');
                            }

                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScoProgTransH',
                config: {
                    url: '/scoPrgTr/:pKNT_SCO_YRN_TR_H_ID?pFRM_SUPPLIER_ID&pTO_SUPPLIER_ID',
                    views: {
                        "ScoProgTransH": {
                            controller: 'KntScoProgTransHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoProgTransH'
                        }
                    },
                    Title: 'Program Transfer (S/C Out-house)',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SCO_YRN_TR_H_ID) && $stateParams.pKNT_SCO_YRN_TR_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KntScoChlnTrans/GetTransChlnHdr?&pKNT_SCO_YRN_TR_H_ID=' + ($stateParams.pKNT_SCO_YRN_TR_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScoProgTransH.YrnTrans',
                config: {
                    url: '/yrnTrans',
                    views: {
                        "ScoProgTransH.YrnTrans": {
                            controller: 'KntScoProgTransYrnTransController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoProgTransYrnTrans'
                        }
                    },
                    Title: 'Program Transfer (S/C Out-house)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScoProgTransList',
                config: {
                    url: '/scoPrgTrList?pSCM_SUPPLIER_ID',
                    views: {
                        "ScoProgTransList": {
                            controller: 'KntScoProgTransListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoProgTransList'
                        }
                    },
                    Title: 'Program Transfer (S/C Out-house)',
                    reloadOnSearch: false
                }
            },
             {
                 state: 'KnitYdProgramList',
                 config: {
                     url: '/KnitYdProgramList',
                     views: {
                         "KnitYdProgramList": {
                             controller: 'KnitYdProgramListController',
                             controllerAs: 'vm',
                             templateUrl: '/Knitting/Knit/_KnitYdProgramList'
                         }
                     },
                     Title: 'Knitting Y/D Program List',
                     reloadOnSearch: false
                 }
             },
             {
                 state: 'SciGreyFabRtnFromPartyH',
                 config: {
                     url: '/sciFabRtnFromParty/:pKNT_SCI_GFAB_RET_H_ID?pSCM_SUPPLIER_ID',
                     views: {
                         "SciGreyFabRtnFromPartyH": {
                             controller: 'KntSciGreyFabRtnFromPartyHController',
                             controllerAs: 'vm',
                             templateUrl: '/Knitting/Knit/_SciGreyFabRtnChlnFromPartyH'
                         }
                     },
                     Title: 'Grey Fabric Return Challan (S/C In-house)',
                     resolve: {
                         formData: function (KnittingDataService, $stateParams) {
                             if (angular.isDefined($stateParams.pKNT_SCI_GFAB_RET_H_ID) && $stateParams.pKNT_SCI_GFAB_RET_H_ID > 0) {
                                 return KnittingDataService.getDataByFullUrl('/api/Knit/KntSciChlnRtn/GetSciRtnChalnHdr?&pKNT_SCI_GFAB_RET_H_ID=' + ($stateParams.pKNT_SCI_GFAB_RET_H_ID || 0));
                             }
                             else {
                                 return {};
                             }
                         },
                         formSciRejFabList: function (KnittingDataService, $stateParams) {
                             if (angular.isDefined($stateParams.pSCM_SUPPLIER_ID) && $stateParams.pSCM_SUPPLIER_ID > 0) {
                                 return KnittingDataService.getDataByFullUrl('/api/Knit/KntSciChlnRtn/GetSciQcRejFabList4Rtn?&pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0));
                             }
                             else {
                                 return {};
                             }
                         }
                     },
                     reloadOnSearch: false
                 }
             },
            {
                state: 'SciGreyFabRtnFromPartyH.FabricReturn',
                config: {
                    url: '/fabRtn',
                    views: {
                        "SciGreyFabRtnFromPartyH.FabricReturn": {
                            controller: 'KntSciGreyFabRtnFromPartyFabRtnController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_SciGreyFabRtnChlnFromPartyFabRtn'
                        }
                    },
                    Title: 'Grey Fabric Return Challan (S/C In-house)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'SciGreyFabRtnFromPartyList',
                config: {
                    url: '/fabRtnFromPartyList?pSCM_SUPPLIER_ID',
                    views: {
                        "SciGreyFabRtnFromPartyList": {
                            controller: 'KntSciGreyFabRtnFromPartyListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_SciGreyFabRtnChlnFromPartyList'
                        }
                    },
                    Title: 'Grey Fabric Return Challan (S/C In-house)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScoCollarCuffProgH',
                config: {
                    url: '/scoClrCufProg/:pKNT_SCO_CLCF_PRG_H_ID?pMC_FAB_PROD_ORD_H_ID&pMC_STYLE_H_ID&pSCM_SUPPLIER_ID&pORDER_NO',
                    views: {
                        "ScoCollarCuffProgH": {
                            controller: 'KntScoCollarCuffProgHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoCollarCuffProgH'
                        }
                    },
                    Title: 'Collar Cuff Program (S/C Out-house)',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_SCO_CLCF_PRG_H_ID) && $stateParams.pKNT_SCO_CLCF_PRG_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/ScoCollarCuff/GetScoCollarCuffHdr?&pKNT_SCO_CLCF_PRG_H_ID=' + ($stateParams.pKNT_SCO_CLCF_PRG_H_ID || 0) + '&pMC_FAB_PROD_ORD_H_ID=' + ($stateParams.pMC_FAB_PROD_ORD_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScoCollarCuffProgH.CollarCuffDtl',
                config: {
                    url: '/clrCuffDtl',
                    views: {
                        "ScoCollarCuffProgH.CollarCuffDtl": {
                            controller: 'KntScoCollarCuffProgDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoCollarCuffProgD'
                        }
                    },
                    Title: 'Collar Cuff Program (S/C Out-house)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScoCollarCuffProgList',
                config: {
                    url: '/scoClrCufProgList?pSCM_SUPPLIER_ID',
                    views: {
                        "ScoCollarCuffProgList": {
                            controller: 'KntScoCollarCuffProgListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoCollarCuffProgList'
                        }
                    },
                    Title: 'Collar Cuff Program (S/C Out-house)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitYdYrnRecv',
                config: {
                    url: '/KnitYdYrnRecv?pKNT_YD_RCV_H_ID&ConMode&stateBack',
                    views: {
                        "KnitYdYrnRecv": {
                            controller: 'KnitYdYrnRecvController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitYdYrnRecv'
                        }
                    },
                    Title: 'Dyed Yarn Receive',
                    resolve: {

                        FormData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_YD_RCV_H_ID && $stateParams.pKNT_YD_RCV_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitYdProgram/getYdYarnRecvData?pKNT_YD_RCV_H_ID=' + $stateParams.pKNT_YD_RCV_H_ID);
                            } else {
                                return {
                                };
                            }
                        }
                    },
                    reloadOnSearch: false
                }
            },

            {
                state: 'KnitYdYrnRecvList',
                config: {
                    url: '/KnitYdYrnRecvList',
                    views: {
                        "KnitYdYrnRecvList": {
                            controller: 'KnitYdYrnRecvListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitYdYrnRecvList'
                        }
                    },
                    data: {
                        stateBack: 'KnitYdYrnRecvList',
                        stateTo: 'KnitYdYrnRecv',
                        IS_TRANSFER: 'N',
                        ConMode: 'N',
                        NewMode: true,
                        RF_ACTN_STATUS_ID: ''
                    },
                    Title: 'Dyed Yarn Receive List (Inhouse)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitPlanYd',
                config: {
                    url: '/KnitPlanYd?pMC_FAB_PROD_ORD_D_ID&pRF_FAB_PROD_CAT_ID&state&pCT_ID_LST',
                    views: {
                        "KnitPlanYdContainer": {
                            controller: 'KnitPlanYdController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanYd'
                        },

                    },
                    resolve: {
                        KnitPlanHeader: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pMC_FAB_PROD_ORD_D_ID && $stateParams.pMC_FAB_PROD_ORD_D_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getKnitPlanHeaderData?pMC_FAB_PROD_ORD_D_ID=' + $stateParams.pMC_FAB_PROD_ORD_D_ID + '&pRF_FAB_PROD_CAT_ID=' + $stateParams.pRF_FAB_PROD_CAT_ID);
                            }
                            else {
                                return {};
                            }
                        },
                        $modalInstance: function () {
                            return null;
                        }
                    },
                    Title: 'Knit Plan (Yarn Dye)',
                    reloadOnSearch: false
                },
            },

            {
                state: 'KnitPlanJobCardYd',
                config: {
                    url: '/JobCardYd?pKNT_PLAN_H_ID&pKNT_JOB_CRD_H_ID&pKNT_SC_PRG_ISS_ID&pIS_SUB_CONTR&state&pMC_FAB_PROD_ORD_H_ID&pRF_FAB_PROD_CAT_ID',
                    views: {
                        "KnitPlanYdContainer": {
                            controller: 'KnitPlanJobCardYdController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanJobCardYd'
                        }
                    },
                    resolve: {
                        $modalInstance: function () {
                            return null;
                        },
                        JobCardHeader: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_PLAN_H_ID && $stateParams.pKNT_PLAN_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardHeaderData?pKNT_PLAN_H_ID=' + $stateParams.pKNT_PLAN_H_ID + '&pKNT_JOB_CRD_H_ID=' + ($stateParams.pKNT_JOB_CRD_H_ID || null));
                            }
                            else {
                                return {};
                            }
                        },
                        YarnLotList: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_PLAN_H_ID && $stateParams.pKNT_PLAN_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLotForJobCard?&pIS_YD=Y&pKNT_PLAN_H_ID=' + $stateParams.pKNT_PLAN_H_ID);

                            } else {
                                return [];
                            }

                        }
                    },
                    Title: 'Knit Card (Y/D)'
                }
            },
            {
                state: 'KntMcOilReq4SubStr',
                config: {
                    url: '/mcOilReq4SubStr/:pSCM_STR_OIL_REQ_H_ID',
                    views: {
                        "KntMcOilReq4SubStr": {
                            controller: 'KntMcOilReq4SubStrHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_McOilReq4SubStrH'
                        }
                    },
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pSCM_STR_OIL_REQ_H_ID && $stateParams.pSCM_STR_OIL_REQ_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/Req4SubStr/GetReqByHdrID?pSCM_STR_OIL_REQ_H_ID=' + ($stateParams.pSCM_STR_OIL_REQ_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Knit M/C Oil Requisition to Store',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntMcOilReq4SubStrList',
                config: {
                    url: '/mcOilReq4SubStrList?pLIST_FROM',
                    views: {
                        "KntMcOilReq4SubStrList": {
                            controller: 'KntMcOilReq4SubStrListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_McOilReq4SubStrList'
                        }
                    },
                    Title: 'Knit M/C Oil Requisition to Store',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntMcOilReqIss4SubStr',
                config: {
                    url: '/mcOilReqIss4SubStr/:pSCM_STR_OIL_REQ_H_ID',
                    views: {
                        "KntMcOilReqIss4SubStr": {
                            controller: 'KntMcOilReqIss4SubStrHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_McOilReqIss4SubStrH'
                        }
                    },
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pSCM_STR_OIL_REQ_H_ID && $stateParams.pSCM_STR_OIL_REQ_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/Req4SubStr/GetReqByHdrID?pSCM_STR_OIL_REQ_H_ID=' + ($stateParams.pSCM_STR_OIL_REQ_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Issue Material to User by Requisition',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitPlanJobCardSRYd',
                config: {
                    url: '/JobCardSRYd?pKNT_YRN_STR_REQ_H_ID',
                    views: {
                        "KnitPlanContainer": {
                            controller: 'KnitPlanJobCardSRController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanJobCardSR'
                        }
                    },
                    resolve: {
                        JobCardHeader: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_PLAN_H_ID && $stateParams.pKNT_PLAN_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardHeaderData?pKNT_PLAN_H_ID=' + $stateParams.pKNT_PLAN_H_ID + '&pKNT_JOB_CRD_H_ID=' + ($stateParams.pKNT_JOB_CRD_H_ID || null));
                            }
                            else {
                                return {};
                            }
                        },
                        YarnLotList: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_PLAN_H_ID && $stateParams.pKNT_PLAN_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLotForJobCard?pKNT_PLAN_H_ID=' + $stateParams.pKNT_PLAN_H_ID);
                            } else {
                                return [];
                            }
                        }
                    },
                    Title: 'Sample Yarn Requisition',
                    data: {
                        LK_COL_TYPE_ID_LST: '360'
                    }
                }
            },
            {
                state: 'KnitPlanJobCardCollarCuffYd',
                config: {
                    url: '/JobCardCollarCuffYd?pKNT_YRN_STR_REQ_H_ID&pKNT_PLAN_H_ID',
                    views: {
                        "KnitPlanContainer": {
                            controller: 'KnitPlanJobCardCollarCuffController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanJobCardCollarCuff'
                        }
                    },
                    Title: 'Yarn Requisition For Collar And/Or Cuff (Y/D)',
                    data: {
                        LK_COL_TYPE_ID_LST: '360'
                    }
                }
            },
            {
                state: 'DailyMachineOilConsumptionList',
                config: {
                    url: '/DailyMachineOilConsumptionList',
                    controller: 'DailyMachineOilConsumptionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_DailyMachineOilConsumptionList',
                    Title: 'Daily Machine Oil ConsumptionList'
                }
            },
            {
                state: 'DailyMachineOilConsumption',
                config: {
                    url: '/DailyMachineOilConsumption?pKNT_MCN_OIL_ISS_H_ID',
                    controller: 'DailyMachineOilConsumptionController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_DailyMachineOilConsumption',
                    Title: 'Daily Machine Oil Consumption',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_MCN_OIL_ISS_H_ID) && $stateParams.pKNT_MCN_OIL_ISS_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/MacOilCons/GetMacOilConsInfo?pKNT_MCN_OIL_ISS_H_ID=' + ($stateParams.pKNT_MCN_OIL_ISS_H_ID || 0)).then(function (res) {
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
                state: 'KntMcNeedleBroken',
                config: {
                    url: '/mcNdlBrkn/:pKNT_MCN_NDL_BRK_H_ID?pIS_FINALIZED',
                    views: {
                        "KntMcNeedleBroken": {
                            controller: 'KntMcNeedleBrokenHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_McNeedleBrokenH'
                        }
                    },
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_MCN_NDL_BRK_H_ID && $stateParams.pKNT_MCN_NDL_BRK_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/Knit/McNeedleBroken/GetNeedleBrokenByHdrID?pKNT_MCN_NDL_BRK_H_ID=' + ($stateParams.pKNT_MCN_NDL_BRK_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        },
                        usrMcTypeList: function (KnittingDataService, $stateParams) {
                            return KnittingDataService.getDataByFullUrl('/api/Knit/KnitCommon/KntMcTypeListByUser');
                        }
                    },
                    Title: 'Daily Shift and M/C Wise Needle Broken',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntMcNeedleBroken.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "KntMcNeedleBroken.Dtl": {
                            controller: 'KntMcNeedleBrokenDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_McNeedleBrokenD'
                        }
                    },
                    Title: 'Daily Shift and M/C Wise Needle Broken',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntMcNeedleBrokenList',
                config: {
                    url: '/mcNdlBrknList',
                    views: {
                        "KntMcNeedleBrokenList": {
                            controller: 'KntMcNeedleBrokenListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_McNeedleBrokenList'
                        }
                    },
                    Title: 'Daily Shift and M/C Wise Needle Broken',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntMcnNeedleOpnBal',
                config: {
                    url: '/mcnNdlOpnBal',
                    views: {
                        "KntMcnNeedleOpnBal": {
                            controller: 'KntMcnNeedleOpnBalController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_McnNeedleOpnBal'
                        }
                    },
                    Title: 'Opening Balance for Knit M/C Needle & Sinker',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KntMcnNeedleAssign',
                config: {
                    url: '/mcnNdlAssign',
                    views: {
                        "KntMcnNeedleAssign": {
                            controller: 'KntMcnNeedleAssignController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_McnNeedleAssign'
                        }
                    },
                    Title: 'Assign Knit M/C Needle & Sinker',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitPlanYarnTestReqList',
                config: {
                    url: '/KnitPlanYarnTestReqList',
                    controller: 'KnitPlanYarnTestReqListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitPlanYarnTestReqList',
                    Title: 'Knit Plan & Requisition for Yarn Test List'
                }
            },
            {
                state: 'KnitPlanYarnTestReq',
                config: {
                    url: '/KnitPlanYarnTestReq?pKNT_YRN_LOT_TEST_H_ID',
                    controller: 'KnitPlanYarnTestReqController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitPlanYarnTestReq',
                    Title: 'Knit Plan & Requisition for Yarn Test',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_LOT_TEST_H_ID) && $stateParams.pKNT_YRN_LOT_TEST_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/GetYarnTest?pKNT_YRN_LOT_TEST_H_ID=' + ($stateParams.pKNT_YRN_LOT_TEST_H_ID || 0)).then(function (res) {
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
                state: 'YarnTestResultList',
                config: {
                    url: '/YarnTestResultList?pKNT_YRN_LOT_TEST_H_ID',
                    controller: 'YarnTestResultListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnTestResultList',
                    Title: 'Yarn Test Result',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_LOT_TEST_H_ID) && $stateParams.pKNT_YRN_LOT_TEST_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/GetYarnTest?pKNT_YRN_LOT_TEST_H_ID=' + ($stateParams.pKNT_YRN_LOT_TEST_H_ID || 0)).then(function (res) {
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
                state: 'KntScProgYd',
                config: {
                    url: '/KntScProgYd?pKNT_SC_PRG_ISS_ID',
                    controller: 'KnitScProgYdRptController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitScProgYdRpt',
                    Title: 'Knit Sub-Contract Program (Y/D)'
                }
            },
            {
                state: 'KnitYdYrnRecvListTrns',
                config: {
                    url: '/KnitYdYrnRecvListTrns',
                    views: {
                        "KnitYdYrnRecvList": {
                            controller: 'KnitYdYrnRecvListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitYdYrnRecvList'
                        }
                    },
                    Title: 'Dyed Yarn Receive List (Transfer)',
                    data: {
                        stateBack: 'KnitYdYrnRecvListTrns',
                        stateTo: 'KnitYdYrnRecvTrns',
                        IS_TRANSFER: 'Y',
                        ConMode: 'N',
                        NewMode: true,
                        RF_ACTN_STATUS_ID: ''
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitYdYrnRecvTrns',
                config: {
                    url: '/KnitYdYrnRecvTrns?pKNT_YD_RCV_H_ID&ConMode&stateBack',
                    views: {
                        "KnitYdYrnRecv": {
                            controller: 'KnitYdYrnRecvTrnsController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitYdYrnRecvTrns'
                        }
                    },
                    Title: 'Dyed Yarn Receive & Transfer',
                    resolve: {

                        FormData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_YD_RCV_H_ID && $stateParams.pKNT_YD_RCV_H_ID > 0) {
                                return KnittingDataService.getDataByUrl('/KnitYdProgram/getYdYarnRecvData?pKNT_YD_RCV_H_ID=' + $stateParams.pKNT_YD_RCV_H_ID);
                            } else {
                                return {

                                };
                            }
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'YarnTestResultPrint',
                config: {
                    url: '/YarnTestResultPrint?pKNT_YRN_LOT_TEST_H_ID&pKNT_YRN_LOT_TEST_D1_ID',
                    controller: 'YarnTestResultPrintController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnTestResultPrint',
                    Title: 'Yarn Test Result',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_LOT_TEST_H_ID) && $stateParams.pKNT_YRN_LOT_TEST_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KntPlanYarnTest/GetYarnTestDtl?pKNT_YRN_LOT_TEST_H_ID=' + ($stateParams.pKNT_YRN_LOT_TEST_H_ID || 0)).then(function (res) {

                                    var lst = _.filter(res, function (x) { return x.KNT_YRN_LOT_TEST_D1_ID == $stateParams.pKNT_YRN_LOT_TEST_D1_ID });
                                    return lst[0];
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
                state: 'KnitScPartyRate',
                config: {
                    url: '/KnitScPartyRate',
                    controller: 'KnitScPartyRateController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitScPartyRate',
                    Title: 'S/C Party Wise Rate Entry'
                }
            },
            {
                state: 'KnitYdStatement',
                config: {
                    url: '/KnitYdStatement',
                    views: {
                        "KnitYdProgramList": {
                            controller: 'KnitYdStatementController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_YdStatement'
                        }
                    },
                    Title: 'Knitting Y/D Statement',
                    reloadOnSearch: false
                }
            },

            {
                state: 'KnitYdStatementRpt',
                config: {
                    url: '/YdStatementRpt?pageNo&pageSize&pRF_FAB_PROD_CAT_ID&pSCM_SUPPLIER_ID&pMC_FAB_PROD_ORD_H_ID&pMC_BYR_ACC_ID&pFIRSTDATE&pLASTDATE&pMONTHOF&pPCT_DONE_CODE',
                    controller: 'KnitYdStatementRptController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitYdStatementRpt',
                    Title: 'Knitting Y/D Statement',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitYarnTestRegister',
                config: {
                    url: '/KnitYarnTestRegister',
                    controller: 'KnitYarnTestRegisterController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitYarnTestRegister',
                    Title: 'Knit Yarn Test Register'
                }
            },
            {
                state: 'KnitYdYrnRecvListCon',
                config: {
                    url: '/KnitYdYrnRecvListCon',
                    views: {
                        "KnitYdYrnRecvList": {
                            controller: 'KnitYdYrnRecvListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitYdYrnRecvList'
                        }
                    },
                    Title: 'Dyed Yarn Receive List (Waiting For Confirmation)',
                    data: {
                        stateBack: 'KnitYdYrnRecvListCon',
                        stateTo: 'KnitYdYrnRecvTrns',
                        IS_TRANSFER: '',
                        ConMode: 'Y',
                        NewMode: false,
                        RF_ACTN_STATUS_ID: ''
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'McnNeedleReqList',
                config: {
                    url: '/McnNeedleReqList',
                    controller: 'McnNeedleReqListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_McnNeedleReqList',
                    Title: 'Machine wise Needle Requisition List'
                }
            },
            {
                state: 'McnNeedleReq',
                config: {
                    url: '/McnNeedleReq?pSCM_STR_NDL_REQ_H_ID',
                    controller: 'McnNeedleReqController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_McnNeedleReq',
                    Title: 'Machine wise Needle Requisition',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_STR_NDL_REQ_H_ID) && $stateParams.pSCM_STR_NDL_REQ_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/McnNeedleReq/GetNeedleReqInfo?pSCM_STR_NDL_REQ_H_ID=' + ($stateParams.pSCM_STR_NDL_REQ_H_ID || 0)).then(function (res) {
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
                state: 'McnNeedleIssueList',
                config: {
                    url: '/McnNeedleIssueList',
                    controller: 'McnNeedleIssueListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_McnNeedleIssueList',
                    Title: 'Machine wise Needle Issue List'
                }
            },
            {
                state: 'McnNeedleIssue',
                config: {
                    url: '/McnNeedleIssue?pSCM_STR_NDL_REQ_H_ID&pSCM_STR_NDL_ISS_H_ID',
                    controller: 'McnNeedleIssueController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_McnNeedleIssue',
                    Title: 'Machine wise Needle Issue',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_STR_NDL_REQ_H_ID) && $stateParams.pSCM_STR_NDL_REQ_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/McnNeedleReq/GetNeedleReqInfo?pSCM_STR_NDL_REQ_H_ID=' + ($stateParams.pSCM_STR_NDL_REQ_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                            else if (angular.isDefined($stateParams.pSCM_STR_NDL_ISS_H_ID) && $stateParams.pSCM_STR_NDL_ISS_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/McnNeedleReq/GetNeedleIssueInfo?pSCM_STR_NDL_ISS_H_ID=' + ($stateParams.pSCM_STR_NDL_ISS_H_ID || 0)).then(function (res) {
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
                state: 'AdvPurReqList',
                config: {
                    url: '/AdvPurReqList',
                    controller: 'AdvPurReqListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_AdvPurReqList',
                    Title: 'Advance Yarn Booking List'
                }
            },
            {
                state: 'AdvPurReq',
                config: {
                    url: '/AdvPurReq?pSCM_PURC_REQ_H_ID',
                    controller: 'AdvPurReqController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_AdvPurReq',
                    Title: 'Advance Yarn Booking',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_PURC_REQ_H_ID) && $stateParams.pSCM_PURC_REQ_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetYarnBuffReqInfo?pSCM_PURC_REQ_H_ID=' + ($stateParams.pSCM_PURC_REQ_H_ID || 0)).then(function (res) {
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
                state: 'PurchasePlanList',
                config: {
                    url: '/PurchasePlanList',
                    controller: 'PurchasePlanListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_PurchasePlanList',
                    Title: 'Purchase Plan List'
                }
            },
            {
                state: 'ComparativeStatement',
                config: {
                    url: '/ComparativeStatement?pSCM_PURC_REQ_H_ID',
                    controller: 'ComparativeStatementController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_ComparativeStatement',
                    Title: 'Comparison of Price Quatation for Purchase Order',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_PURC_REQ_H_ID) && $stateParams.pSCM_PURC_REQ_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetYarnBuffReqInfo?pSCM_PURC_REQ_H_ID=' + ($stateParams.pSCM_PURC_REQ_H_ID || 0)).then(function (res) {
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
                state: 'KnitCardTestRpt',
                config: {
                    url: '/KnitCardTestRpt?pKNT_JOB_CRD_H_ID',
                    controller: 'KnitCardRptController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitCardTestRpt',
                    Title: 'Yarn Test'
                }
            },
            {
                state: 'KnitMCOilOB',
                config: {
                    url: '/KnitMCOilOB',
                    controller: 'KnitMCOilOBController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitMCOilOB',
                    Title: 'M/C Oil Opening Blance'
                }
            },
            {
                state: 'KnitMcNeedleRcvList',
                config: {
                    url: '/KnitMcNeedleRcvList',
                    controller: 'KnitMcNeedleRcvListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitMcNeedleRcvList',
                    Title: 'M/C Needle Receive List'
                }
            },
            {
                state: 'KnitMcNeedleRcv',
                config: {
                    url: '/KnitMcNeedleRcv?pKNT_NDL_STR_RCV_H_ID',
                    controller: 'KnitMcNeedleRcvController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitMcNeedleRcv',
                    Title: 'M/C Needle Receive',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_NDL_STR_RCV_H_ID) && $stateParams.pKNT_NDL_STR_RCV_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KnitMcNeedleRcv/GetKnitMcNeedleRcvInfo?pKNT_NDL_STR_RCV_H_ID=' + ($stateParams.pKNT_NDL_STR_RCV_H_ID || 0)).then(function (res) {
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
                state: 'KnitMcNeedleRcvView',
                config: {
                    url: '/KnitMcNeedleRcvView?pKNT_NDL_STR_RCV_H_ID',
                    controller: 'KnitMcNeedleRcvController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitMcNeedleRcvView',
                    Title: 'M/C Needle Receive View',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_NDL_STR_RCV_H_ID) && $stateParams.pKNT_NDL_STR_RCV_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KnitMcNeedleRcv/GetKnitMcNeedleRcvInfo?pKNT_NDL_STR_RCV_H_ID=' + ($stateParams.pKNT_NDL_STR_RCV_H_ID || 0)).then(function (res) {
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
                state: 'KnitMcOilRcvList',
                config: {
                    url: '/KnitMcOilRcvList',
                    controller: 'KnitMcOilRcvListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitMcOilRcvList',
                    Title: 'M/C Oil Receive List'
                }
            },
            {
                state: 'KnitMcOilRcv',
                config: {
                    url: '/KnitMcOilRcv?pKNT_OIL_STR_RCV_H_ID',
                    controller: 'KnitMcOilRcvController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitMcOilRcv',
                    Title: 'M/C Oil Receive',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_OIL_STR_RCV_H_ID) && $stateParams.pKNT_OIL_STR_RCV_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KnitMcOilRcv/GetKnitMcOilRcvInfo?pKNT_OIL_STR_RCV_H_ID=' + ($stateParams.pKNT_OIL_STR_RCV_H_ID || 0)).then(function (res) {
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
                state: 'KnitMcOilRcvView',
                config: {
                    url: '/KnitMcOilRcvView?pKNT_OIL_STR_RCV_H_ID',
                    controller: 'KnitMcOilRcvController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitMcOilRcvView',
                    Title: 'M/C Oil Receive View',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_OIL_STR_RCV_H_ID) && $stateParams.pKNT_OIL_STR_RCV_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/knit/KnitMcOilRcv/GetKnitMcOilRcvInfo?pKNT_OIL_STR_RCV_H_ID=' + ($stateParams.pKNT_OIL_STR_RCV_H_ID || 0)).then(function (res) {
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
                state: 'KnitUserRequisitionMap',
                config: {
                    url: '/KnitUserRequisitionMap',
                    controller: 'KnitUserRequisitionMapController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_KnitUserRequisitionMap',
                    Title: 'User & Requisition Mapping'
                }
            },
            {
                state: 'ScoBillProcH',
                config: {
                    url: '/scoBillProcH/:pKNT_SCO_BILL_H_ID',
                    views: {
                        "ScoBillProcH": {
                            controller: 'KntScoBillProcHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoBillProcH'
                        }
                    },
                    resolve: {
                        scoBillHdrData: function (KnittingDataService, $stateParams) {
                            //return {};
                            if ($stateParams.pKNT_SCO_BILL_H_ID && $stateParams.pKNT_SCO_BILL_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KntScoBill/GetBillHdr?pKNT_SCO_BILL_H_ID=' + ($stateParams.pKNT_SCO_BILL_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'S/C Out-house Bill Process',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScoBillProcH.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "ScoBillProcH.Dtl": {
                            controller: 'KntScoBillProcDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoBillProcD'
                        }
                    },
                    Title: 'S/C Out-house Bill Process',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScoBillProcList',
                config: {
                    url: '/billList?pSCM_SUPPLIER_ID',
                    views: {
                        "ScoBillProcList": {
                            controller: 'KntScoBillProcListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoBillProcList'
                        }
                    },
                    Title: 'S/C Out-house Bill Process',
                    resolve: {
                        userLavelData: function (KnittingDataService, $stateParams) {
                            return KnittingDataService.getDataByFullUrl('/api/Knit/KntScoBill/GetScoBillProcUserLavel');
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'SciBillProcH',
                config: {
                    url: '/sciBillProcH/:pKNT_SCI_BILL_H_ID',
                    views: {
                        "SciBillProcH": {
                            controller: 'KntSciBillProcHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_SciBillProcH'
                        }
                    },
                    resolve: {
                        sciBillHdrData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_SCI_BILL_H_ID && $stateParams.pKNT_SCI_BILL_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KntSciBill/GetBillHdr?pKNT_SCI_BILL_H_ID=' + ($stateParams.pKNT_SCI_BILL_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'S/C In-house Bill Process',
                    reloadOnSearch: false
                }
            },
            {
                state: 'SciBillProcH.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "SciBillProcH.Dtl": {
                            controller: 'KntSciBillProcDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_SciBillProcD'
                        }
                    },
                    Title: 'S/C In-house Bill Process',
                    resolve: {
                        userLavelData: function (KnittingDataService, $stateParams) {
                            return KnittingDataService.getDataByFullUrl('/api/Knit/KntScoBill/GetScoBillProcUserLavel');
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'SciBillProcList',
                config: {
                    url: '/sciBillList?pSCM_SUPPLIER_ID',
                    views: {
                        "SciBillProcList": {
                            controller: 'KntSciBillProcListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_SciBillProcList'
                        }
                    },
                    Title: 'S/C In-house Bill Process',
                    resolve: {
                        userLavelData: function (KnittingDataService, $stateParams) {
                            return KnittingDataService.getDataByFullUrl('/api/Knit/KntSciBill/GetSciBillProcUserLavel');
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'SciYarnWipTranH',
                config: {
                    url: '/sciYarnWipTranH/:pKNT_SCI_YRN_TR_H_ID',
                    views: {
                        "SciYarnWipTranH": {
                            controller: 'KntSciYarnWipTranHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_SciYarnWipTranH'
                        }
                    },
                    resolve: {
                        sciYrnTranHdrData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_SCI_YRN_TR_H_ID && $stateParams.pKNT_SCI_YRN_TR_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KntSciYrnTran/GetYrnTranHdr?pKNT_SCI_YRN_TR_H_ID=' + ($stateParams.pKNT_SCI_YRN_TR_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Yarn WIP Transfer (S/C In-house)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'SciYarnWipTranH.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "SciYarnWipTranH.Dtl": {
                            controller: 'KntSciYarnWipTranDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_SciYarnWipTranD'
                        }
                    },
                    Title: 'Yarn WIP Transfer (S/C In-house)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitYdProcLossAdjH',
                config: {
                    url: '/kntYdProcLossAdjH/:pKNT_YD_PL_ADJ_H_ID',
                    views: {
                        "KnitYdProcLossAdjH": {
                            controller: 'KntYdProcLossAdjHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitYdProcLossAdjH'
                        }
                    },
                    resolve: {
                        kntYdProcLossAdjHdrData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_YD_PL_ADJ_H_ID && $stateParams.pKNT_YD_PL_ADJ_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KnitYdProgram/GetPlAdjHdr?pKNT_YD_PL_ADJ_H_ID=' + ($stateParams.pKNT_YD_PL_ADJ_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Process Loss Adjustment (Y/D)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitYdProcLossAdjH.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "KnitYdProcLossAdjH.Dtl": {
                            controller: 'KntYdProcLossAdjDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitYdProcLossAdjD'
                        }
                    },
                    Title: 'Process Loss Adjustment (Y/D)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'KnitYdProcLossAdjList',
                config: {
                    url: '/kntYdProcLossAdjList?pSCM_SUPPLIER_ID',
                    views: {
                        "KnitYdProcLossAdjList": {
                            controller: 'KntYdProcLossAdjListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitYdProcLossAdjList'
                        }
                    },
                    Title: 'Process Loss Adjustment (Y/D)',
                    resolve: {
                        userLavelData: function (KnittingDataService, $stateParams) {
                            return KnittingDataService.getDataByFullUrl('/api/Knit/KnitYdProgram/GetPlAdjUserLavel');
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'OrdWiseGreyTrnReqH',
                config: {
                    url: '/ordWiseGreyTrnReqH/:pKNT_ORD_TRN_REQ_H_ID?pLK_TRN_SRC_ID&pTO_MC_BYR_ACC_GRP_ID&pMC_STYLE_H_EXT_ID&pTO_MC_FAB_PROD_ORD_H_ID&pTO_MC_COLOR_ID',
                    views: {
                        "OrdWiseGreyTrnReqH": {
                            controller: 'KntOrdWiseGreyTrnReqHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_OrdWiseGreyTrnReqH'
                        }
                    },
                    resolve: {
                        OrdWiseGreyTrnReqHdrData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_ORD_TRN_REQ_H_ID && $stateParams.pKNT_ORD_TRN_REQ_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KntOrdWiseGreyTrnReq/GetOrdWiseGreyTrnReqHdr?pKNT_ORD_TRN_REQ_H_ID=' + ($stateParams.pKNT_ORD_TRN_REQ_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        },
                        userLavelData: function (KnittingDataService, $stateParams) {
                            return KnittingDataService.getDataByFullUrl('/api/Knit/KntOrdWiseGreyTrnReq/GetFabTransUserLavel');
                        }
                    },
                    Title: 'Order wise Grey Fabric Transfer',
                    reloadOnSearch: false
                }
            },
            {
                state: 'OrdWiseGreyTrnReqH.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "OrdWiseGreyTrnReqH.Dtl": {
                            controller: 'KntOrdWiseGreyTrnReqDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_OrdWiseGreyTrnReqD'
                        }
                    },
                    Title: 'Order wise Grey Fabric Transfer',
                    reloadOnSearch: false
                }
            },
            {
                state: 'OrdWiseGreyTrnReqList',
                config: {
                    url: '/ordWiseGreyTrnReqList',
                    views: {
                        "OrdWiseGreyTrnReqList": {
                            controller: 'KntOrdWiseGreyTrnReqListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_OrdWiseGreyTrnReqList'
                        }
                    },
                    Title: 'Order wise Grey Fabric Transfer',
                    resolve: {
                        userLavelData: function (KnittingDataService, $stateParams) {
                            return KnittingDataService.getDataByFullUrl('/api/Knit/KntOrdWiseGreyTrnReq/GetFabTransUserLavel');
                        }
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'FabBk4LabdipH',
                config: {
                    url: '/fabBk4Labdip/:pKNT_GEN_FAB_REQ_H_ID',
                    views: {
                        "FabBk4LabdipH": {
                            controller: 'KntFabBk4LabdipHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_FabBk4LabdipH'
                        }
                    },
                    resolve: {
                        FabReqHdrData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_GEN_FAB_REQ_H_ID && $stateParams.pKNT_GEN_FAB_REQ_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/GenFabReq/GetFabReqHdr?pKNT_GEN_FAB_REQ_H_ID=' + ($stateParams.pKNT_GEN_FAB_REQ_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'General Fabric Booking',
                    reloadOnSearch: false
                }
            },
            {
                state: 'FabBk4LabdipH.Dtl',
                config: {
                    url: '/dtl',
                    views: {
                        "FabBk4LabdipH.Dtl": {
                            controller: 'KntFabBk4LabdipDController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_FabBk4LabdipD'
                        }
                    },
                    Title: 'General Fabric Booking',
                    reloadOnSearch: false
                }
            },
            {
                state: 'FabBk4LabdipList',
                config: {
                    url: '/list',
                    views: {
                        "FabBk4LabdipList": {
                            controller: 'KntFabBk4LabdipListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_FabBk4LabdipList'
                        }
                    },
                    Title: 'General Fabric Booking',
                    reloadOnSearch: false
                }
            },
            {
                state: 'PartyWiseYarnIssueChallanList',
                config: {
                    url: '/PartyWiseYarnIssueChallanList',
                    controller: 'PartyWiseYarnIssueChallanListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_PartyWiseYarnIssueChallanList',
                    Title: 'Party Wise Yarn Issue Challan List'
                }
            },
            {
                state: 'PartyWiseYarnIssueChallan',
                config: {
                    url: '/PartyWiseYarnIssueChallan?pKNT_YRN_CHL_ISS_H_ID',
                    controller: 'PartyWiseYarnIssueChallanController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_PartyWiseYarnIssueChallan',
                    Title: 'Party Wise Yarn Issue Challan',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pKNT_YRN_CHL_ISS_H_ID) && $stateParams.pKNT_YRN_CHL_ISS_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueChallan/GetYarnIssueChallanInfo?pKNT_YRN_CHL_ISS_H_ID=' + ($stateParams.pKNT_YRN_CHL_ISS_H_ID || 0)).then(function (res) {
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
                state: 'YarnRtnToSupIssueList',
                config: {
                    url: '/YarnRtnToSupIssueList',
                    controller: 'YarnRtnToSupIssueListController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnRtnToSupIssueList',
                    Title: 'Issue Yarn Replace by Supplier List'
                }
            },
            {
                state: 'YarnRtnToSupIssue',
                config: {
                    url: '/YarnRtnToSupIssue?pKNT_YRN_RPL_ISS_H_ID',
                    controller: 'YarnRtnToSupIssueController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_YarnRtnToSupIssue',
                    Title: 'Issue Yarn Replace by Supplier',
                    resolve: {
                        formData: function (KnittingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pKNT_YRN_RPL_ISS_H_ID) && $stateParams.pKNT_YRN_RPL_ISS_H_ID > 0) {

                                return KnittingDataService.getDataByFullUrl('/api/Knit/YarnReturnToSupplier/GetReplacementIssueByID?pKNT_YRN_RPL_ISS_H_ID=' + ($stateParams.pKNT_YRN_RPL_ISS_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                            //else if (angular.isDefined($stateParams.pKNT_YRN_STR_RPL_REQ_H_ID) && $stateParams.pKNT_YRN_STR_RPL_REQ_H_ID > 0) {
                            //    return KnittingDataService.getDataByFullUrl('/api/Knit/YarnReturnToSupplier/GetReplacementReqByID?pKNT_YRN_STR_RPL_REQ_H_ID=' + ($stateParams.pKNT_YRN_STR_RPL_REQ_H_ID || 0)).then(function (res) {
                            //        return res;
                            //    });
                            //}
                            else {
                                return {};
                            }
                        }
                    }
                }
            },           
            {
                state: 'YdRecvChalnH',
                config: {
                    url: '/ydRcvChlnH/:pKNT_YD_RCV_CHL_H_ID',
                    views: {
                        "YdRecvChalnH": {
                            controller: 'KntYdRecvChalnHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_YdRecvChalnH'
                        }
                    },
                    resolve: {
                        ydRecvChalnHdrData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_YD_RCV_CHL_H_ID && $stateParams.pKNT_YD_RCV_CHL_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KntYdRecvChaln/GetYdRecvChalnHdr?pKNT_YD_RCV_CHL_H_ID=' + ($stateParams.pKNT_YD_RCV_CHL_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Dyed Yarn Receive Challan',
                    reloadOnSearch: false
                }
            },            
            {
                state: 'YdRecvChalnList',
                config: {
                    url: '/ydRcvChlnList?pSCM_SUPPLIER_ID',
                    views: {
                        "YdRecvChalnList": {
                            controller: 'KntYdRecvChalnListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_YdRecvChalnList'
                        }
                    },
                    Title: 'Dyed Yarn Receive Challan',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScoYdTrChalnH',
                config: {
                    url: '/scoYdTrChalnH/:pKNT_SCO_YD_TR_CHL_H_ID',
                    views: {
                        "ScoYdTrChalnH": {
                            controller: 'KntScoYdTrChalnHController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoYdTrChalnH'
                        }
                    },
                    resolve: {
                        ScoYdTrChalnHdrData: function (KnittingDataService, $stateParams) {
                            if ($stateParams.pKNT_SCO_YD_TR_CHL_H_ID && $stateParams.pKNT_SCO_YD_TR_CHL_H_ID > 0) {
                                return KnittingDataService.getDataByFullUrl('/api/knit/KntScoYdTrChaln/GetScoYdTrChalnHdr?pKNT_SCO_YD_TR_CHL_H_ID=' + ($stateParams.pKNT_SCO_YD_TR_CHL_H_ID || 0));
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    Title: 'Dyed Yarn Transfer Challan (S/C Out-house)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ScoYdTrChalnList',
                config: {
                    url: '/scoYdTrChalnList?pSCM_SUPPLIER_ID',
                    views: {
                        "ScoYdTrChalnList": {
                            controller: 'KntScoYdTrChalnListController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_ScoYdTrChalnList'
                        }
                    },
                    Title: 'Dyed Yarn Transfer Challan (S/C Out-house)',
                    reloadOnSearch: false
                }
            },
            {
                state: 'FabProdKnitOrderFBR',
                config: {
                    url: '/FabProdKnitOrderFBR?pMC_FAB_PROD_ORD_H_ID&pRF_FAB_PROD_CAT_ID',
                    Title: 'Fabric Production Knitting Order (Flat Bed RIB)',
                    controller: 'FabricProductionKnittingOrderController',
                    controllerAs: 'vm',
                    templateUrl: '/Knitting/Knit/_FabProdKnitOrder',
                    data: {
                        RF_FAB_PROD_CAT_ID_LST: '1,2,3,4,5,6,8,9',
                        DEFAULT_CAT_ID: '2',
                        state: 'FabProdKnitOrderFBR',
                        LK_COL_TYPE_ID_LST: '360,358,361,407,432',
                        RF_FAB_TYPE_ID: 5
                    }
                }
            },


        ];
    }
})();
