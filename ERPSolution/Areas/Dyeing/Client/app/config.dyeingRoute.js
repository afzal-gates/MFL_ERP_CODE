(function () {
    'use strict';

    angular
        .module('multitex.dyeing')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [

            {
                state: 'LabdipRecipeList',
                config: {
                    url: '/LabdipRecipeList',
                    Title: 'Labdip Recipe List',
                    controller: 'LabdipRecipeListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_LabdipRecipeList'
                }
            },
            {
                state: 'LabdipRecipe',
                config: {
                    url: '/LabdipRecipe/{pMC_LD_RECIPE_H_ID:int}/{pRECHECK_ID:int}?pMC_LD_REQ_D_ID&pView',
                    Title: 'Labdip Recipe',
                    controller: 'LabdipRecipeController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_LabdipRecipe',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pMC_LD_RECIPE_H_ID) && $stateParams.pMC_LD_RECIPE_H_ID >= 0 && (angular.isDefined($stateParams.pRECHECK_ID) && $stateParams.pRECHECK_ID == 0)) {

                                if (angular.isDefined($stateParams.pMC_LD_REQ_D_ID) && $stateParams.pMC_LD_REQ_D_ID >= 0) {
                                    return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/SelectLD?pMC_LD_REQ_D_ID=' + ($stateParams.pMC_LD_REQ_D_ID || 0)).then(function (res) {
                                        return res;
                                    });
                                }
                                else {
                                    return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/SelectLDRecipe/' + ($stateParams.pMC_LD_RECIPE_H_ID || 0) + '/0').then(function (res) {
                                        return res;
                                    });
                                }
                                //return {};
                            }
                            else if (angular.isDefined($stateParams.pMC_LD_RECIPE_H_ID) && $stateParams.pMC_LD_RECIPE_H_ID >= 0 && (angular.isDefined($stateParams.pRECHECK_ID) && $stateParams.pRECHECK_ID == 1)) {


                                return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/SelectLDRecipe/' + ($stateParams.pMC_LD_RECIPE_H_ID || 0) + '/1').then(function (res) {
                                    return res;
                                });
                                //return {};
                            }
                            else if (angular.isDefined($stateParams.pMC_LD_RECIPE_H_ID) && $stateParams.pMC_LD_RECIPE_H_ID >= 0 && (angular.isDefined($stateParams.pRECHECK_ID) && $stateParams.pRECHECK_ID == 2)) {


                                return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/SelectLDRecipe/' + ($stateParams.pMC_LD_RECIPE_H_ID || 0) + '/2').then(function (res) {
                                    return res;
                                });
                                //return {};
                            }
                            else {
                                return {};
                            }
                        }
                    }
                }
            },
            {
                state: 'LabdipRecipeViewList',
                config: {
                    url: '/LabdipRecipeViewList',
                    Title: 'Labdip Recipe View List',
                    controller: 'LabdipRecipeViewListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_LabdipRecipeViewList'
                }
            },
            {
                state: 'LabdipRecipeView',
                config: {
                    url: '/LabdipRecipeView/{pMC_LD_RECIPE_H_ID:int}/{pRECHECK_ID:int}?pMC_LD_REQ_D_ID',
                    Title: 'Labdip Recipe View',
                    controller: 'LabdipRecipeController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_LabdipRecipeView',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pMC_LD_RECIPE_H_ID) && $stateParams.pMC_LD_RECIPE_H_ID >= 0 && (angular.isDefined($stateParams.pRECHECK_ID) && $stateParams.pRECHECK_ID == 0)) {

                                if (angular.isDefined($stateParams.pMC_LD_REQ_D_ID) && $stateParams.pMC_LD_REQ_D_ID >= 0) {
                                    return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/SelectLD?pMC_LD_REQ_D_ID=' + ($stateParams.pMC_LD_REQ_D_ID || 0)).then(function (res) {
                                        return res;
                                    });
                                }
                                else {
                                    return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/SelectLDRecipe/' + ($stateParams.pMC_LD_RECIPE_H_ID || 0) + '/0').then(function (res) {
                                        return res;
                                    });
                                }
                                //return {};
                            }
                            else {
                                return {};
                            }
                        }
                    }
                }
            },
            {
                state: 'Requisition',
                config: {
                    url: '/Requisition?pSCM_PURC_REQ_H_ID',
                    controller: 'RequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_Requisition',
                    Title: 'Requisition',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_PURC_REQ_H_ID) && $stateParams.pSCM_PURC_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/dye/PurcReq/GetPurcReqInfo?pSCM_PURC_REQ_H_ID=' + ($stateParams.pSCM_PURC_REQ_H_ID || 0)).then(function (res) {
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
                state: 'RequisitionList',
                config: {
                    url: '/RequisitionList',
                    controller: 'RequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_RequisitionList',
                    Title: 'Requisition List'
                }
            },
            {
                state: 'DyeChemicalReceiveList',
                config: {
                    url: '/DyeChemicalReceiveList',
                    controller: 'DyeChemicalReceiveListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeChemicalReceiveList',
                    Title: 'Daily Dyes & Chemical Receive List'
                }
            },
            {
                state: 'DyeChemicalReceive',
                config: {
                    url: '/DyeChemicalReceive?pDYE_DC_RCV_H_ID',
                    controller: 'DyeChemicalReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeChemicalReceive',
                    Title: 'Daily Dyes & Chemical Receive',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_DC_RCV_H_ID) && $stateParams.pDYE_DC_RCV_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DyeChemicalReceive/GetDyeChemicalReceiveInfo?pDYE_DC_RCV_H_ID=' + ($stateParams.pDYE_DC_RCV_H_ID || 0)).then(function (res) {
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
                state: 'DyeChemicalReceiveView',
                config: {
                    url: '/DyeChemicalReceiveView?pDYE_DC_RCV_H_ID',
                    controller: 'DyeChemicalReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeChemicalReceiveView',
                    Title: 'Daily Dyes & Chemical Receive',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_DC_RCV_H_ID) && $stateParams.pDYE_DC_RCV_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DyeChemicalReceive/GetDyeChemicalReceiveInfo?pDYE_DC_RCV_H_ID=' + ($stateParams.pDYE_DC_RCV_H_ID || 0)).then(function (res) {
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
                state: 'DyeChemicalStoreTransferList',
                config: {
                    url: '/DyeChemicalStoreTransferList',
                    controller: 'DyeChemicalStoreTransferListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeChemicalStoreTransferList',
                    Title: 'Store Transfer Requisition & Issue List'
                }
            },
            {
                state: 'DyeChemicalStoreTransfer',
                config: {
                    url: '/DyeChemicalStoreTransfer?pDYE_STR_TR_REQ_H_ID&pTYPE',
                    controller: 'DyeChemicalStoreTransferController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeChemicalStoreTransfer',
                    Title: 'Store Transfer Requisition',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_TR_REQ_H_ID) && $stateParams.pDYE_STR_TR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DyeChemicalStoreTransfer/GetDyeChemicalStoreTransferInfo?pDYE_STR_TR_REQ_H_ID=' + ($stateParams.pDYE_STR_TR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'DyeChemicalStoreIssue',
                config: {
                    url: '/DyeChemicalStoreIssue?pDYE_STR_TR_REQ_H_ID&pDYE_STR_TR_ISS_H_ID',
                    controller: 'DyeChemicalStoreIssueController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeChemicalStoreIssue',
                    Title: 'Store Transfer Issue',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDYE_STR_TR_ISS_H_ID) && $stateParams.pDYE_STR_TR_ISS_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DyeChemicalStoreTransfer/GetIssueByID?pDYE_STR_TR_ISS_H_ID=' + ($stateParams.pDYE_STR_TR_ISS_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                            else if (angular.isDefined($stateParams.pDYE_STR_TR_REQ_H_ID) && $stateParams.pDYE_STR_TR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DyeChemicalStoreTransfer/GetDyeChemicalStoreTransferInfo?pDYE_STR_TR_REQ_H_ID=' + ($stateParams.pDYE_STR_TR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'DyeChemicalOpeningBalance',
                config: {
                    url: '/DyeChemicalOpeningBalance',
                    controller: 'DyeChemicalOpeningBalanceController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeChemicalOpeningBalance',
                    Title: 'Stock Opening Balance'
                }
            },
            {
                state: 'DCIssueRequisitionList',
                config: {
                    url: '/DCIssueRequisitionList',
                    controller: 'DCIssueRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssueRequisitionList',
                    Title: 'Dyes Chemical Finishing Requisition List'
                }
            },
            {
                state: 'DCIssueRequisition',
                config: {
                    url: '/DCIssueRequisition?pDYE_STR_REQ_H_ID',
                    controller: 'DCIssueRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssueRequisition',
                    Title: 'Dyes Chemical Finishing Requisition',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'MachineWashRequisitionList',
                config: {
                    url: '/MachineWashRequisitionList',
                    controller: 'MachineWashRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_MachineWashRequisitionList',
                    Title: 'Machine Wash Requisition List'
                }
            },
            {
                state: 'MachineWashRequisition',
                config: {
                    url: '/MachineWashRequisition?pDYE_STR_REQ_H_ID',
                    controller: 'MachineWashRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_MachineWashRequisition',
                    Title: 'Machine Wash Requisition',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'DCIssueList',
                config: {
                    url: '/DCIssueList',
                    controller: 'DCIssueListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssueList',
                    Title: 'Dyes Chemical Issue List'
                }
            },
            {
                state: 'DCIssue',
                config: {
                    url: '/DCIssue?pDYE_STR_REQ_H_ID&pDYE_DC_ISS_H_ID',
                    controller: 'DCIssueController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssue',
                    Title: 'Dyes Chemical Issue',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                            else if (angular.isDefined($stateParams.pDYE_DC_ISS_H_ID) && $stateParams.pDYE_DC_ISS_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssue/GetDCIssueInfo?pDYE_DC_ISS_H_ID=' + ($stateParams.pDYE_DC_ISS_H_ID || 0)).then(function (res) {
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
                state: 'DCLoanReturnRequisitionList',
                config: {
                    url: '/DCLoanReturnRequisitionList',
                    controller: 'DCLoanReturnRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCLoanReturnRequisitionList',
                    Title: 'Requisition for Loan Rtn to Party List'
                }
            },
            {
                state: 'DCLoanReturnRequisition',
                config: {
                    url: '/DCLoanReturnRequisition?pDYE_STR_REQ_H_ID',
                    controller: 'DCLoanReturnRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCLoanReturnRequisition',
                    Title: 'Requisition for Loan Return to Party',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'DCLoanReturn',
                config: {
                    url: '/DCLoanReturn?pDYE_DC_ISS_H_ID',
                    controller: 'DCLoanReturnController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCLoanReturn',
                    Title: 'Dyes Chemical Loan Return',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_DC_ISS_H_ID) && $stateParams.pDYE_DC_ISS_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssue/GetDCIssueInfo?pDYE_DC_ISS_H_ID=' + ($stateParams.pDYE_DC_ISS_H_ID || 0)).then(function (res) {
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
                state: 'DCBatchProgramRequisitionList',
                config: {
                    url: '/DCBatchProgramRequisitionList',
                    controller: 'DCBatchProgramRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCBatchProgramRequisitionList',
                    Title: 'Dyes Chemical Batch Program Requisition List'
                }
            },
            {
                state: 'DCBatchProgramRequisition',
                config: {
                    url: '/DCBatchProgramRequisition?pDYE_STR_REQ_H_ID&pADDITION_FLAG&pLOT_ID&pON_LINE_QC_ID&pRIB_ID',
                    controller: 'DCBatchProgramRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCBatchProgramRequisition',
                    Title: 'Dyes Chemical Batch Program Requisition',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0) + '&pLOT_ID=' + ($stateParams.pLOT_ID || '') + '&pON_LINE_QC_ID=' + ($stateParams.pON_LINE_QC_ID || '') + '&pRIB_ID=' + ($stateParams.pRIB_ID || '')).then(function (res) {
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
                state: 'DCIssueBatchProgram',
                config: {
                    url: '/DCIssueBatchProgram?pDYE_STR_REQ_H_ID',
                    controller: 'DCIssueBatchProgramController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssueBatchProgram',
                    Title: 'Issue Against Batch Program',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'DCBatchProgramAddition',
                config: {
                    url: '/DCBatchProgramAddition?pDYE_STR_REQ_H_ID',
                    controller: 'DCBatchProgramAdditionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCBatchProgramAddition',
                    Title: 'Addition Batch Program Requisition',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
             /////////// End for Supplier Profile

             /////////// Start of Dyeing Batch Plan
            {
                state: 'DyeBatchPlan',
                config: {
                    url: '/DyeBatchPlan?pDYE_BATCH_SCDL_ID',
                    views: {
                        "DyeBatchPlan": {
                            controller: 'DyingBatchPlanController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_DyeBatchPlan'
                        }
                    },
                    Title: 'Dyeing Batch Plan',
                    resolve: {
                        ScheduleData: function (DyeingDataService, $stateParams) {
                            if ($stateParams.pDYE_BATCH_SCDL_ID) {
                                return DyeingDataService.getDataByUrl('/DyeBatchPlan/getSchedulePlanData?pDYE_BATCH_SCDL_ID=' + $stateParams.pDYE_BATCH_SCDL_ID);
                            } else {
                                return {};
                            }
                        },
                        McList: function (DyeingDataService) {
                            return DyeingDataService.getDataByFullUrl('/api/knit/KnitPlan/getCompanyList');
                        }
                    }
                }
            },
            ////////// End of Dyeing Batch Plan

            ////////// Start of Dyeing Report
            {
                state: 'DcReportParams',
                config: {
                    url: '/dcReportParams',
                    views: {
                        "DcReportParams": {
                            controller: 'DyeReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_DcReportParams'
                        }
                    },
                    Title: 'Dyeing Reports',
                    resolve: {
                        dyeRptData: function (DyeingDataService) {
                            return DyeingDataService.getDataByUrlNoToken('/api/common/getReportDataListByUser/' + 5); //// Here 1 is report group ID
                        }
                    }
                }
            },
            ////////// End of Dyeing Report
            {
                state: 'DosingTemplate',
                config: {
                    url: '/DosingTemplate',
                    controller: 'DCDosingTempleteController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DosingTemplate',
                    Title: 'Dosing Template'
                }
            },


            {
                state: 'DCRequestLoanRequisitionList',
                config: {
                    url: '/DCRequestLoanRequisitionList',
                    controller: 'DCRequestLoanRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCRequestLoanRequisitionList',
                    Title: 'Requisition for Loan Issue List'
                }
            },
            {
                state: 'DCRequestLoanRequisition',
                config: {
                    url: '/DCRequestLoanRequisition?pDYE_STR_REQ_H_ID',
                    controller: 'DCRequestLoanRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCRequestLoanRequisition',
                    Title: 'Requisition for Loan Issue to Party',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'DCIssueLoanRequisitionList',
                config: {
                    url: '/DCIssueLoanRequisitionList',
                    controller: 'DCIssueLoanRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssueLoanRequisitionList',
                    Title: 'Requisition for Loan Issue List'
                }
            },
            {
                state: 'DCIssueLoanRequisition',
                config: {
                    url: '/DCIssueLoanRequisition?pDYE_STR_REQ_H_ID',
                    controller: 'DCIssueLoanRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssueLoanRequisition',
                    //Title: 'Requisition for Loan Issue',
                    Title: 'Store Issue for Loan',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'DCIssueAgainstLoanRequisitionList',
                config: {
                    url: '/DCIssueAgainstLoanRequisitionList',
                    controller: 'DCIssueAgainstLoanRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssueAgainstLoanRequisitionList',
                    Title: 'Issue Against Loan Requisition List'
                }
            },
            {
                state: 'DCIssueAgainstLoanRequisition',
                config: {
                    url: '/DCIssueAgainstLoanRequisition?pDYE_STR_REQ_H_ID&pDYE_DC_ISS_H_ID',
                    controller: 'DCIssueAgainstLoanRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssueAgainstLoanRequisition',
                    //Title: 'Issue Against Loan Requisition',
                    Title: 'Store Issue for Loan',

                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                            else if (angular.isDefined($stateParams.pDYE_DC_ISS_H_ID) && $stateParams.pDYE_DC_ISS_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssue/GetDCIssueInfo?pDYE_DC_ISS_H_ID=' + ($stateParams.pDYE_DC_ISS_H_ID || 0)).then(function (res) {
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
                state: 'DCIssuedLoanAdjustList',
                config: {
                    url: '/DCIssuedLoanAdjustList',
                    controller: 'DCIssuedLoanAdjustListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssuedLoanAdjustList',
                    Title: 'Loan Return From Party List'
                }
            },

            {
                state: 'DCIssuedLoanAdjust',
                config: {
                    url: '/DCIssuedLoanAdjust?pDYE_DC_LRT_H_ID=',
                    controller: 'DCIssuedLoanAdjustController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssuedLoanAdjust',
                    Title: 'Loan Return From Party',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_DC_LRT_H_ID) && $stateParams.pDYE_DC_LRT_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCReceiveableLoan/GetDCReceiveableLoanInfo?pDYE_DC_LRT_H_ID=' + ($stateParams.pDYE_DC_LRT_H_ID || 0)).then(function (res) {
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
                state: 'DyeMachine',
                config: {
                    url: '/DyeMachine',
                    controller: 'DyeMachineController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeMachine',
                    Title: 'Dye Machine'
                }
            },

          /////////// Start of Dyeing Batch Fab Requisition
            {
                state: 'DyeBatchFabReq',
                config: {
                    url: '/DyeBatchFabReq?pDYE_BATCH_SCDL_ID',
                    views: {
                        "DyeBatchFabReq": {
                            controller: 'DyeBatchFabReqController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_DyeBatchFabReq'
                        }
                    },
                    Title: 'Fabric Requisition For Batch (Bulk)',
                    resolve: {
                        McList: function (DyeingDataService) {
                            return DyeingDataService.getDataByFullUrl('/api/knit/KnitPlan/getCompanyList');
                        }
                    }
                }
            },
            ////////// End of Dyeing Fab Requisition 


          /////////// Start of Daily Batch Production
            {
                state: 'DyeDailyBatchProdReprocess',
                config: {
                    url: '/DyeDailyBatchProdReprocess',
                    controller: 'DyeDailyBatchProdReprocessController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeDailyBatchProdReprocess',
                    Title: 'Daily Bulk Batch Production',
                    data: {
                        state: 'DyeDailyBatchProdReprocess',
                        RF_FAB_PROD_CAT_ID: 2
                    }
                }
            },
            ////////// End of Daily Batch Production 

            /////////// Start of Supplier Loan Opening Stock
            {
                state: 'DCOpeningSupLNSTK',
                config: {
                    url: '/DCOpeningSupLNSTK',
                    controller: 'DCOpeningSupLNSTKController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCOpeningSupLNSTK',
                    Title: 'Supplier Loan Opening Stock'
                }
            },
            ////////// End of Supplier Loan Opening Stock
            {
                state: 'DyeingBatchProgramRpt',
                config: {
                    url: '/DyeingBatchProgramRpt?pDYE_BATCH_SCDL_ID',
                    controller: 'DyeingBatchProgramRptController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeingProgramRpt',
                    Title: 'Dyeing Batch Program'
                }
            },

            /////////// Start of Issue Against Batch Program
            {
                state: 'DCIssueBatchProgramList',
                config: {
                    url: '/DCIssueBatchProgramList',
                    controller: 'DCIssueBatchProgramListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCIssueBatchProgramList',
                    Title: 'Issue Against Batch Program List'
                }
            },
            ////////// End of Issue Against Batch Program

           /////////// Start of Dyeing Batch Fab Requisition
            {
                state: 'DyeBatchFabReqSample',
                config: {
                    url: '/DyeBatchFabReqSample?pDYE_BATCH_SCDL_ID',
                    views: {
                        "DyeBatchFabReqSample": {
                            controller: 'DyeBatchFabReqSampleController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_DyeBatchFabReqSample'
                        }
                    },
                    Title: 'Fab Requisition For Batch (Sample)',
                    resolve: {
                        McList: function (DyeingDataService) {
                            return DyeingDataService.getDataByFullUrl('/api/knit/KnitPlan/getCompanyList');
                        }
                    },
                    reloadOnSearch: false
                }
            },
            ////////// End of Dyeing Fab Requisition 


            /////////// Start of Daily Other Batch Production 
            {
                state: 'DyeDailyOtherBatchProdReprocess',
                config: {
                    url: '/DyeDailyOtherBatchProdReprocess',
                    controller: 'DyeDailyBatchProdReprocessController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeDailyBatchProdReprocess',
                    Title: 'Daily Other Batch Production',
                    data: {
                        state: 'DyeDailyOtherBatchProdReprocess',
                        RF_FAB_PROD_CAT_ID: 0
                    }
                }
            },

            ////////// End of Daily Other Batch Production
            {
                state: 'DCBatchProgramCompleteList',
                config: {
                    url: '/DCBatchProgramCompleteList',
                    controller: 'DCBatchProgramCompleteListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCBatchProgramCompleteList',
                    Title: 'Complete Batch Program List'
                }
            },
            {
                state: 'DyeChemicalLoanReceiveList',
                config: {
                    url: '/DyeChemicalLoanReceiveList',
                    controller: 'DyeChemicalLoanReceiveListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeChemicalLoanReceiveList',
                    Title: 'Daily Loan Receive List'
                }
            },
            {
                state: 'DyeChemicalLoanReceive',
                config: {
                    url: '/DyeChemicalLoanReceive?pDYE_DC_RCV_H_ID',
                    controller: 'DyeChemicalLoanReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeChemicalLoanReceive',
                    Title: 'Daily Loan Receive',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_DC_RCV_H_ID) && $stateParams.pDYE_DC_RCV_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DyeChemicalReceive/GetDyeChemicalReceiveInfo?pDYE_DC_RCV_H_ID=' + ($stateParams.pDYE_DC_RCV_H_ID || 0)).then(function (res) {
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
                state: 'DyeChemicalLoanReceiveView',
                config: {
                    url: '/DyeChemicalLoanReceiveView?pDYE_DC_RCV_H_ID',
                    controller: 'DyeChemicalLoanReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeChemicalLoanReceiveView',
                    Title: 'Daily Dyes & Chemical Receive',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_DC_RCV_H_ID) && $stateParams.pDYE_DC_RCV_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DyeChemicalReceive/GetDyeChemicalReceiveInfo?pDYE_DC_RCV_H_ID=' + ($stateParams.pDYE_DC_RCV_H_ID || 0)).then(function (res) {
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
                state: 'CompleteBatchList',
                config: {
                    url: '/CompleteBatchList',
                    controller: 'CompleteBatchListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_CompleteBatchList',
                    Title: 'Dyeing Check-Roll Return for Re-Process List'
                }
            },
            {
                state: 'CompleteBatchReprocess',
                config: {
                    url: '/CompleteBatchReprocess?pDYE_STR_REQ_H_ID&pADDITION_FLAG',
                    controller: 'CompleteBatchReprocessController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_CompleteBatchReprocess',
                    Title: 'Complete Batch Reprocess',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'DCBatchProgramAddRequisition',
                config: {
                    url: '/DCBatchProgramAddRequisition?pDYE_STR_REQ_H_ID&pADDITION_FLAG',
                    controller: 'DCBatchProgramAddRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DCBatchProgramAddRequisition',
                    Title: 'Dyes Chemical Batch Program Requisition',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetDCBatchProgramAdditionRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'BatchProductionList',
                config: {
                    url: '/BatchProductionList',
                    controller: 'DCBatchProductionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_BatchProductionList',
                    Title: 'QC Check Roll List'
                }
            },
            {
                state: 'BatchProduction',
                config: {
                    url: '/BatchProduction',
                    controller: 'DCBatchProductionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_BatchProduction',
                    Title: 'Batch Production'
                }
            },
            {
                state: 'CheckRollInspection',
                config: {
                    url: '/CheckRollInspection?pDYE_BATCH_NO',
                    controller: 'DCCheckRollInspectionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_CheckRollInspection',
                    Title: 'Check Roll Inspection'
                }
            },
            {
                state: 'DyeingFinishingMachine',
                config: {
                    url: '/DyeingFinishingMachine',
                    controller: 'DyeingFinishingMachineController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeingFinishingMachine',
                    Title: 'Dyeing Finishing Machine'
                }
            },
            {
                state: 'DFMachineProcessMapping',
                config: {
                    url: '/DFMachineProcessMapping',
                    controller: 'DFMachineProcessMappingController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DFMachineProcessMapping',
                    Title: 'Machine & Process Mapping'
                }
            },
            {
                state: 'DyeingFinishingProduction',
                config: {
                    url: '/DyeingFinishingProduction',
                    controller: 'DyeingFinishingProductionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeingFinishingProduction',
                    Title: 'Dyeing Finishing Production'
                }
            },
            {
                state: 'MachineWashProduction',
                config: {
                    url: '/MachineWashProduction',
                    controller: 'MachineWashProductionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_MachineWashProduction',
                    Title: 'Machine Wash Production'
                }
            },
            {
                state: 'DyeingProductionBoard',
                config: {
                    url: '/DyeingProductionBoard',
                    controller: 'DyeingProductionBoardController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeingProductionBoard',
                    Title: 'Dyeing Finishing Floor Status'
                }
            },

            {
                state: 'GreyFabReqListBulk',
                config: {
                    url: '/list/bulk?pDYE_GSTR_REQ_H_ID&page',
                    views: {
                        "GreyFabReqList": {
                            controller: 'GreyFabReqListInHController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_GreyFabReqListInH'
                        }
                    },
                    Title: 'Grey Fab Requisition (Bulk)',
                    data: {
                        RF_FAB_PROD_CAT_LST: '2,5,6',
                        RF_ACTN_TYPE_ID: 13
                    },
                    reloadOnSearch: false
                }
            },

            {
                state: 'GreyFabReqListScP',
                config: {
                    url: '/list/scp?pDYE_GSTR_REQ_H_ID&page',
                    views: {
                        "GreyFabReqList": {
                            controller: 'GreyFabReqListController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_GreyFabReqList'
                        }
                    },
                    Title: 'Grey Fab Requisition (S/C Program)',
                    data: {
                        RF_FAB_PROD_CAT_LST: '1,2,3,5,6',
                        RF_ACTN_TYPE_ID: 18
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'GreyFabReqIssue',
                config: {
                    url: '/issue/{pDYE_BT_CARD_H_ID:int}?pDYE_GSTR_ISS_H_ID&pDYE_GSTR_REQ_H_ID&state&page',
                    views: {
                        "GreyFabReqIssue": {
                            controller: 'GreyFabReqIssueController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_GreyFabReqIssue'
                        }
                    },
                    Title: 'Grey Fab Issue',
                    resolve: {
                        data: function (DyeingDataService, $stateParams) {

                            if ($stateParams.pDYE_BT_CARD_H_ID && $stateParams.pDYE_BT_CARD_H_ID > 0) {
                                return DyeingDataService.getDataByUrl('/GreyFabReq/GetBatchCardInfo?pDYE_BT_CARD_H_ID=' + $stateParams.pDYE_BT_CARD_H_ID + '&pDYE_GSTR_ISS_H_ID=' + $stateParams.pDYE_GSTR_ISS_H_ID)
                            }
                            else if ($stateParams.pDYE_GSTR_REQ_H_ID && $stateParams.pDYE_GSTR_REQ_H_ID > 0) {
                                return DyeingDataService.getDataByUrl('/GreyFabReq/GetPreTreatmentInfo?pDYE_GSTR_REQ_H_ID=' + $stateParams.pDYE_GSTR_REQ_H_ID)
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
                state: 'GreyFabReqIssue.group',
                config: {
                    url: '/group?pGRP_ID&pLK_FBR_GRP_ID',
                    views: {
                        "TabInside": {
                            controller: 'GreyFabReqIssueDController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_GreyFabReqIssueD'
                        }
                    },
                    Title: 'Grey Fab Issue'
                }
            },
            {
                state: 'GreyFabReqIssue.grouptrim',
                config: {
                    url: '/group/trim',
                    views: {
                        "TabInside": {
                            controller: 'GreyFabReqIssueTrimController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_GreyFabReqIssueTrim'
                        }
                    },
                    Title: 'Grey Fab Issue'
                }
            },
            {
                state: 'ScProgramList',
                config: {
                    url: '/ScProgramList',
                    controller: 'DFScProgramListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScProgramList',
                    Title: 'S/C Program List'
                }
            },
            {
                state: 'ScProgram',
                config: {
                    url: '/ScProgram?pDF_SC_PRG_ISS_H_ID',
                    controller: 'DFScProgramController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScProgram',
                    Title: 'S/C Program',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SC_PRG_ISS_H_ID) && $stateParams.pDF_SC_PRG_ISS_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/GetScProgramInfo?pDF_SC_PRG_ISS_H_ID=' + ($stateParams.pDF_SC_PRG_ISS_H_ID || 0)).then(function (res) {
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
                state: 'DfReportParams',
                config: {
                    url: '/dfReportParams',
                    views: {
                        "DfReportParams": {
                            controller: 'DyeingFinishingReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_DfReportParams'
                        }
                    },
                    Title: 'Dyeing Finishing Reports',
                    resolve: {
                        dyeRptData: function (DyeingDataService) {
                            return DyeingDataService.getDataByUrlNoToken('/api/common/getReportDataListByUser/' + 6); //// Here 1 is report group ID
                        }
                    }
                }
            },
            {
                state: 'ScProgramReceiveList',
                config: {
                    url: '/ScProgramReceiveList',
                    controller: 'DFScProgramReceiveListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScProgramReceiveList',
                    Title: 'Receive S/C Dyeing Finishing Program List'
                }
            },
            {
                state: 'ScProgramReceive',
                config: {
                    url: '/ScProgramReceive?pDF_SCO_CHL_RCV_H_ID&pIS_AOP',
                    controller: 'DFScProgramReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScProgramReceive',
                    Title: 'Receive S/C Dyeing Finishing Program',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SCO_CHL_RCV_H_ID) && $stateParams.pDF_SCO_CHL_RCV_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/GetScProgramReceiveInfo?pDF_SCO_CHL_RCV_H_ID=' + ($stateParams.pDF_SCO_CHL_RCV_H_ID || 0)).then(function (res) {
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
                state: 'ScProgramReceiveView',
                config: {
                    url: '/ScProgramReceiveView?pDF_SCO_CHL_RCV_H_ID&pIS_AOP',
                    controller: 'DFScProgramReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScProgramReceiveView',
                    Title: 'Receive S/C Dyeing Finishing Program View',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SCO_CHL_RCV_H_ID) && $stateParams.pDF_SCO_CHL_RCV_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/GetScProgramReceiveInfo?pDF_SCO_CHL_RCV_H_ID=' + ($stateParams.pDF_SCO_CHL_RCV_H_ID || 0)).then(function (res) {
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
                state: 'DFProcessType',
                config: {
                    url: '/DFProcessType',
                    controller: 'DFProcessTypeController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DFProcessType',
                    Title: 'Dyeing Finishing Process Type'
                }
            },

            {
                state: 'DyeScProgram',
                config: {
                    url: '/DyeScProgram?pDYE_SC_PRG_ISS_ID',
                    views: {
                        "DyeScProgramList": {
                            controller: 'DyeScProgramController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_DyeScProgram'
                        }
                    },
                    Title: 'Dyeing S/C Program List',
                    reloadOnSearch: false
                }
            },
            {
                state: 'ReceiveFinishFabricList',
                config: {
                    url: '/ReceiveFinishFabricList',
                    controller: 'DFReceiveFinishFabricListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ReceiveFinishFabricList',
                    Title: 'Receive Finish Fabric at Store List'
                }
            },
            {
                state: 'ReceiveFinishFabric',
                config: {
                    url: '/ReceiveFinishFabric?pDF_SCO_FSTR_RCV_H_ID',
                    controller: 'DFReceiveFinishFabricController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ReceiveFinishFabric',
                    Title: 'Receive Finish Fabric at Store',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SCO_FSTR_RCV_H_ID) && $stateParams.pDF_SCO_FSTR_RCV_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DfRcvFinFabric/SelectReceiveInfo?pDF_SCO_FSTR_RCV_H_ID=' + ($stateParams.pDF_SCO_FSTR_RCV_H_ID || 0)).then(function (res) {
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
                state: 'GreyFabReqListSample',
                config: {
                    url: '/list/sample?pDYE_GSTR_REQ_H_ID&page',
                    views: {
                        "GreyFabReqList": {
                            controller: 'GreyFabReqListInHController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_GreyFabReqListInH'
                        }
                    },
                    Title: 'Grey Fab Requisition (Sample)',
                    data: {
                        RF_FAB_PROD_CAT_LST: '1,3',
                        RF_ACTN_TYPE_ID: 15
                    },
                    reloadOnSearch: false
                }
            },
            {
                state: 'RunningBatchList',
                config: {
                    url: '/RunningBatchList',
                    controller: 'DCRunningBatchListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_RunningBatchList',
                    Title: 'Running Batch List'
                }
            },
            {
                state: 'RunningBatchEdit',
                config: {
                    url: '/RunningBatchEdit?pDYE_STR_REQ_H_ID',
                    controller: 'DCRunningBatchEditController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_RunningBatchEdit',
                    Title: 'Running Batch Update',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'ScPreTreatmentList',
                config: {
                    url: '/ScPreTreatmentList',
                    controller: 'ScPreTreatmentListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScPreTreatmentList',
                    Title: 'S/C Pre-Treatment List'
                }
            },
            {
                state: 'ScPreTreatment',
                config: {
                    url: '/ScPreTreatment?pDF_SC_PT_ISS_H_ID&pDF_SC_PT_RCV_H_ID',
                    controller: 'ScPreTreatmentController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScPreTreatment',
                    Title: 'S/C Pre-Treatment',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SC_PT_ISS_H_ID) && $stateParams.pDF_SC_PT_ISS_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/Dye/ScPreTreatment/GetScPreTreatmentInfo?pDF_SC_PT_ISS_H_ID=' + ($stateParams.pDF_SC_PT_ISS_H_ID || 0)).then(function (res) {
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
                state: 'GreyFabReqPTIssue',
                config: {
                    url: '/issue?pDYE_GSTR_REQ_H_ID&state&page',
                    views: {
                        "GreyFabReqPTIssue": {
                            controller: 'GreyFabReqPTIssueController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_GreyFabReqPTIssue'
                        }
                    },
                    Title: 'Grey Fab S/C Issue',
                    resolve: {
                        data: function (DyeingDataService, $stateParams) {

                            if ($stateParams.pDYE_GSTR_REQ_H_ID && $stateParams.pDYE_GSTR_REQ_H_ID > 0) {
                                return DyeingDataService.getDataByUrl('/GreyFabReq/GetPreTreatmentInfo?pDYE_GSTR_REQ_H_ID=' + $stateParams.pDYE_GSTR_REQ_H_ID)
                            }
                            else {
                                return {};
                            }
                        }
                    },
                    reloadOnSearch: true
                }
            },
            {
                state: 'GreyFabReqPTIssue.groups',
                config: {
                    url: '/groups?pKNT_STYL_FAB_ITEM_ID',
                    views: {
                        "TabInsidePT": {
                            controller: 'GreyFabReqPTIssueDController',
                            controllerAs: 'vm',
                            templateUrl: '/Dyeing/Dye/_GreyFabReqPTIssueD'
                        }
                    },
                    Title: 'Grey Fab S/C Issue'
                },
            },
            {
                state: 'ScPreTreatmentReceiveList',
                config: {
                    url: '/ScPreTreatmentReceiveList',
                    controller: 'ScPreTreatmentReceiveListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScPreTreatmentReceiveList',
                    Title: 'S/C Pre-Treatment Receive List'
                }
            },
            {
                state: 'ScPreTreatmentReceive',
                config: {
                    url: '/ScPreTreatmentReceive?pDF_SC_PT_RCV_H_ID',
                    controller: 'ScPreTreatmentReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScPreTreatmentReceive',
                    Title: 'S/C Pre-Treatment Receive',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SC_PT_RCV_H_ID) && $stateParams.pDF_SC_PT_RCV_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/dye/ScPtReceive/GetScPtReceiveInfo?pDF_SC_PT_RCV_H_ID=' + ($stateParams.pDF_SC_PT_RCV_H_ID || 0)).then(function (res) {
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
                state: 'ScPreTreatmentReceiveView',
                config: {
                    url: '/ScPreTreatmentReceiveView?pDF_SC_PT_RCV_H_ID',
                    controller: 'ScPreTreatmentReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScPreTreatmentReceiveView',
                    Title: 'S/C Pre-Treatment Receive',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SC_PT_RCV_H_ID) && $stateParams.pDF_SC_PT_RCV_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/dye/ScPtReceive/GetScPtReceiveInfo?pDF_SC_PT_RCV_H_ID=' + ($stateParams.pDF_SC_PT_RCV_H_ID || 0)).then(function (res) {
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
                state: 'FinishFabricInspection',
                config: {
                    url: '/FinishFabricInspection',
                    controller: 'FinishFabricInspectionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_FinishFabricInspection',
                    Title: 'Finish Fabric Inspection'
                }
            },
            {
                state: 'FabricInspectionReportList',
                config: {
                    url: '/FabricInspectionReportList',
                    controller: 'FabricInspectionReportListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_FabricInspectionReportList',
                    Title: 'Fabric Inspection Report List'
                }
            },
            {
                state: 'FabricInspectionReport',
                config: {
                    url: '/FabricInspectionReport?pDF_FAB_QC_RPT_ID',
                    controller: 'FabricInspectionReportController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_FabricInspectionReport',
                    Title: 'Fabric Inspection Report',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_FAB_QC_RPT_ID) && $stateParams.pDF_FAB_QC_RPT_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/dye/DfFabInsp/GetDfQCRptByID?pDF_FAB_QC_RPT_ID=' + ($stateParams.pDF_FAB_QC_RPT_ID || 0)).then(function (res) {
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
                state: 'ScPreTreatmentChallanList',
                config: {
                    url: '/ScPreTreatmentChallanList',
                    controller: 'ScPreTreatmentChallanListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScPreTreatmentChallanList',
                    Title: 'S/C Pre-Treatment Challan List'
                }
            },
            {
                state: 'ScPreTreatmentChallan',
                config: {
                    url: '/ScPreTreatmentChallan?pDF_SC_PT_CHL_ISS_H_ID',
                    controller: 'ScPreTreatmentChallanController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScPreTreatmentChallan',
                    Title: 'S/C Pre-Treatment Challan',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SC_PT_CHL_ISS_H_ID) && $stateParams.pDF_SC_PT_CHL_ISS_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/dye/ScPreTreatment/ScPtChallanInfoByID?pDF_SC_PT_CHL_ISS_H_ID=' + ($stateParams.pDF_SC_PT_CHL_ISS_H_ID || 0)).then(function (res) {
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
                state: 'ScPreTreatmentBillList',
                config: {
                    url: '/ScPreTreatmentBillList',
                    controller: 'ScPreTreatmentBillListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScPreTreatmentBillList',
                    Title: 'S/C Pre-Treatment Bill List'
                }
            },
            {
                state: 'ScPreTreatmentBill',
                config: {
                    url: '/ScPreTreatmentBill?pDF_SCO_PT_BILL_H_ID',
                    controller: 'ScPreTreatmentBillController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScPreTreatmentBill',
                    Title: 'S/C Pre-Treatment Bill',
                    resolve: {
                        scoBillHdrData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SCO_PT_BILL_H_ID) && $stateParams.pDF_SCO_PT_BILL_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/dye/DfScPtBill/GetBillHdr?pDF_SCO_PT_BILL_H_ID=' + ($stateParams.pDF_SCO_PT_BILL_H_ID || 0)).then(function (res) {
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
                state: 'DyeMcMaintenanceList',
                config: {
                    url: '/DyeMcMaintenanceList',
                    controller: 'DyeMcMaintenanceListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeMcMaintenanceList',
                    Title: 'Dyeing M/C Maintenance List',
                    pOPTION: 3
                }
            },            
            {
                state: 'DfMcMaintenanceList',
                config: {
                    url: '/DfMcMaintenanceList',
                    controller: 'DyeMcMaintenanceListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeMcMaintenanceList',
                    Title: 'Dyeing Finishing M/C Maintenance List',
                    pOPTION: 5
                },
            },            
            {
                state: 'DyeMcMaintenance',
                config: {
                    url: '/DyeMcMaintenance?pDYE_MCN_STOP_TRAN_ID&pOPTION',
                    controller: 'DyeMcMaintenanceController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeMcMaintenance',
                    Title: 'M/C Maintenance',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDYE_MCN_STOP_TRAN_ID) && $stateParams.pDYE_MCN_STOP_TRAN_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/dye/dyeMcMaintenance/DyeMcStopTranGetByID?pDYE_MCN_STOP_TRAN_ID=' + ($stateParams.pDYE_MCN_STOP_TRAN_ID || 0)).then(function (res) {
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
                state: 'ScProgramChallanList',
                config: {
                    url: '/ScProgramChallanList',
                    controller: 'ScProgramChallanListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScProgramChallanList',
                    Title: 'Dyeing Finishing S/C Program Challan List'
                }
            },
            {
                state: 'ScProgramChallan',
                config: {
                    url: '/ScProgramChallan?pDF_SC_BT_CHL_ISS_H_ID',
                    controller: 'ScProgramChallanController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ScProgramChallan',
                    Title: 'Dyeing Finishing S/C Program Challan',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SC_BT_CHL_ISS_H_ID) && $stateParams.pDF_SC_BT_CHL_ISS_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/dye/DfScProgram/DfScProgramChallanGetByID?pDF_SC_BT_CHL_ISS_H_ID=' + ($stateParams.pDF_SC_BT_CHL_ISS_H_ID || 0)).then(function (res) {
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
                state: 'BatchAtDfStore',
                config: {
                    url: '/BatchAtDfStore',
                    controller: 'BatchAtDfStoreController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_BatchAtDfStore',
                    Title: 'List of Batches at Dyeing Finishing Store'
                }
            },
            {
                state: 'BatchAtFabricStore',
                config: {
                    url: '/BatchAtFabricStore',
                    controller: 'BatchAtFabricStoreController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_BatchAtFabricStore',
                    Title: 'List of Batches at Fabric Store'
                }
            },
            {
                state: 'FabricHoldList',
                config: {
                    url: '/FabricHoldList',
                    controller: 'FabricHoldListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_FabricHoldList',
                    Title: 'Fabric Hold List'
                }
            },
            {
                state: 'DfOnlineQcList',
                config: {
                    url: '/DfOnlineQcList',
                    controller: 'DfOnlineQcListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DfOnlineQcList',
                    Title: 'Dyeing Finishing Online QC List'
                }
            },
            {
                state: 'DfOnlineQc',
                config: {
                    url: '/DfOnlineQc?pDF_PROC_QC_RPT_H_ID',
                    controller: 'DfOnlineQcController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DfOnlineQc',
                    Title: 'Dyeing Finishing Online QC',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_PROC_QC_RPT_H_ID) && $stateParams.pDF_PROC_QC_RPT_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectOnlineQCByID?pDF_PROC_QC_RPT_H_ID=' + ($stateParams.pDF_PROC_QC_RPT_H_ID || 0)).then(function (res) {
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
                state: 'DfGmtWashProdList',
                config: {
                    url: '/DfGmtWashProdList',
                    controller: 'DfGmtWashProdListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DfGmtWashProdList',
                    Title: 'GMT Wash Production List'
                }
            },
            {
                state: 'DfGmtWashProd',
                config: {
                    url: '/DfGmtWashProd?pGMT_DF_WASH_REQ_ID&pDF_GMT_WASH_PROD_ID',
                    controller: 'DfGmtWashProdController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DfGmtWashProd',
                    Title: 'GMT Wash Production',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pGMT_DF_WASH_REQ_ID) && $stateParams.pGMT_DF_WASH_REQ_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/ie/GmtWashReq/GetGmtWashReqInfoByID?pGMT_DF_WASH_REQ_ID=' + ($stateParams.pGMT_DF_WASH_REQ_ID || 0)).then(function (res) {
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
                state: 'DfScProgramBillList',
                config: {
                    url: '/DfScProgramBillList',
                    controller: 'DfScProgramBillListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DfScProgramBillList',
                    Title: 'S/C Program Bill List'
                }
            },
            {
                state: 'DfScProgramBill',
                config: {
                    url: '/DfScProgramBill?pDF_SCO_FP_BILL_H_ID',
                    controller: 'DfScProgramBillController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DfScProgramBill',
                    Title: 'S/C Program Bill',
                    resolve: {
                        scoBillHdrData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SCO_FP_BILL_H_ID) && $stateParams.pDF_SCO_FP_BILL_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/dye/DfScProgramBill/GetBillHdr?pDF_SCO_FP_BILL_H_ID=' + ($stateParams.pDF_SCO_FP_BILL_H_ID || 0)).then(function (res) {
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
                state: 'AopBatchList',
                config: {
                    url: '/AopBatchList',
                    controller: 'AopBatchListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_AopBatchList',
                    Title: 'AOP Batch List'
                }
            },
            {
                state: 'AopBatch',
                config: {
                    url: '/AopBatch?pDYE_BT_CARD_H_ID',
                    controller: 'AopBatchController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_AopBatch',
                    Title: 'AOP Batch',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDYE_BT_CARD_H_ID) && $stateParams.pDYE_BT_CARD_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/dye/AopBatch/GetAopBatchInfo?pDYE_BT_CARD_H_ID=' + ($stateParams.pDYE_BT_CARD_H_ID || 0)).then(function (res) {
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
                state: 'FinFabDelv2Store',
                config: {
                    url: '/FinFabDelv2Store',
                    controller: 'FinFabDelv2StoreController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_FinFabDelv2Store',
                    Title: 'Waiting for Finished Fabric Delivery to Store'
                }
            },
            {
                state: 'DfBtCrdPrint',
                config: {
                    url: '/DfBtCrdPrint',
                    controller: 'DfBtCrdPrintController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DfBtCrdPrint',
                    Title: 'Print Batch Card for DF'
                }
            },
            {
                state: 'DfBtFinalQC',
                config: {
                    url: '/DfBtFinalQC',
                    controller: 'DfBtFinalQCController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DfBtFinalQC',
                    Title: 'Batch wise DF Final Q.C'
                }
            },
            {
                state: 'ShadeCheckRollInspectionList',
                config: {
                    url: '/ShadeCheckRollInspectionList',
                    controller: 'ShadeCheckRollInspectionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ShadeCheckRollInspectionList',
                    Title: 'Rib/Collar Shade Approval'
                }
            },
            {
                state: 'ShadeCheckRollInspection',
                config: {
                    url: '/ShadeCheckRollInspection?pDF_RIB_SHADE_RPT_H_ID',
                    controller: 'ShadeCheckRollInspectionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ShadeCheckRollInspection',
                    Title: 'Rib/Collar Shade Approval',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_RIB_SHADE_RPT_H_ID) && $stateParams.pDF_RIB_SHADE_RPT_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/Dye/OtherCheckRoll/GetRibShadRptByID?pDF_RIB_SHADE_RPT_H_ID=' + ($stateParams.pDF_RIB_SHADE_RPT_H_ID || 0)).then(function (res) {
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
                state: 'ShadeCheckRollInspectionView',
                config: {
                    url: '/ShadeCheckRollInspectionView?pDF_RIB_SHADE_RPT_H_ID',
                    controller: 'ShadeCheckRollInspectionController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_ShadeCheckRollInspectionView',
                    Title: 'Rib/Collar Shade Approval',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_RIB_SHADE_RPT_H_ID) && $stateParams.pDF_RIB_SHADE_RPT_H_ID > 0) {
                                return DyeingDataService.getDataByFullUrl('/api/Dye/OtherCheckRoll/GetRibShadRptByID?pDF_RIB_SHADE_RPT_H_ID=' + ($stateParams.pDF_RIB_SHADE_RPT_H_ID || 0)).then(function (res) {
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
                state: 'DfMaterialReqList',
                config: {
                    url: '/DfMaterialReqList',
                    controller: 'DfMaterialReqListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DfMaterialReqList',
                    Title: 'Finishing Material Requisition List'
                }
            },
            {
                state: 'DfMaterialReq',
                config: {
                    url: '/DfMaterialReq?pDYE_STR_REQ_H_ID',
                    controller: 'DfMaterialReqController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DfMaterialReq',
                    Title: 'Finishing Material Requisition',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pDYE_STR_REQ_H_ID) && $stateParams.pDYE_STR_REQ_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfo?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'DyeScProgramList',
                config: {
                    url: '/DyeScProgramList',
                    controller: 'DyeScProgramListController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeScProgramList',
                    Title: 'Dyeing S/C Program List'
                }
            },
            {
                state: 'DyeingScProgram',
                config: {
                    url: '/DyeingScProgram?pDF_SC_PRG_ISS_H_ID',
                    controller: 'DyeingScProgramController',
                    controllerAs: 'vm',
                    templateUrl: '/Dyeing/Dye/_DyeingScProgram',
                    Title: 'Dyeing S/C Program',
                    resolve: {
                        formData: function (DyeingDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pDF_SC_PRG_ISS_H_ID) && $stateParams.pDF_SC_PRG_ISS_H_ID > 0) {

                                return DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/GetScProgramInfo?pDF_SC_PRG_ISS_H_ID=' + ($stateParams.pDF_SC_PRG_ISS_H_ID || 0)).then(function (res) {
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
