(function () {
    'use strict';

    angular
        .module('multitex.inventory')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [

            /////////// Start for Assign User Category
            {
                state: 'AssignUserCategory',
                config: {
                    url: '/CategoryPermission',
                    controller: 'AssignUserCategoryController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_AssignUserToCategory',
                    Title: 'Assign User Category'//,
                    //resolve: {
                    //    TreeList: function (InventoryDataService) {
                    //        return InventoryDataService.getItemCategTreeList();
                    //    }
                    //}
                }
            },
            ///////////// End for Assign User Category

            /////////// Start for Item Category
            {
                state: 'ItemCategoryEntry',
                config: {
                    url: '/entry',
                    controller: 'ItemCategoryController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ItemCategoryEntry',
                    Title: 'Item Category'//,
                    //resolve: {
                    //    TreeList: function (InventoryDataService) {
                    //        return InventoryDataService.getItemCategTreeList();
                    //    }
                    //}
                }
            },
            {
                state: 'ItemCategoryPermission',
                config: {
                    url: '/itmcatpermission',
                    controller: 'ItemCategoryPermissionController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ItemCategoryPermission',
                    Title: 'Item Category Permission'//,
                    //resolve: {
                    //    TreeList: function (InventoryDataService) {
                    //        return InventoryDataService.getItemCategTreeList();
                    //    }
                    //}
                }
            },
            ///////////// End for Item Category


            /////////// Start for Item Information            
            {
                state: 'ItemList',
                config: {
                    url: '/itemList',
                    controller: 'ItemListController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ItemList',
                    Title: 'Item List',
                    params: { pItemObj: null }
                }
            },
            /////////// End for Item Information

            /////////// Start for Item Sales POS
            {
                state: 'ItemSales',
                config: {
                    url: '/itemSales',
                    controller: 'ItemSalesController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ItemSales',
                    Title: 'Item Sales'
                }
            },
            /////////// End for Item Sales POS

            {
               state: 'StoreProfile',
               config: {
                   url: '/StoreProfile',
                   controller: 'StoreProfileController',
                   controllerAs: 'vm',
                   templateUrl: '/Inventory/Inv/_StoreProfile',
                   Title: 'Store Profile'
               }
            },
            {
                state: 'ScmStoreReceiveList',
                config: {
                    url: '/ScmStoreReceiveList',
                    controller: 'ScmStoreReceiveListController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ScmStoreReceiveList',
                    Title: 'Material Receive List'
                }
            },
            {
                state: 'ScmStoreReceive',
                config: {
                    url: '/ScmStoreReceive?pSCM_STR_RCV_H_ID',
                    controller: 'ScmStoreReceiveController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ScmStoreReceive',
                    Title: 'Material Receive at Store',
                    resolve: {
                        formData: function (InventoryDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_STR_RCV_H_ID) && $stateParams.pSCM_STR_RCV_H_ID > 0) {
                                
                                return InventoryDataService.getDataByFullUrl('/api/Inv/StoreReceive/GetStoreReceiveInfoByID?pSCM_STR_RCV_H_ID=' + ($stateParams.pSCM_STR_RCV_H_ID || 0)).then(function (res) {
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
                state: 'MoUConversion',
                config: {
                    url: '/MoUConversion',
                    controller: 'MoUConversionController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_MoUConversion',
                    Title: 'MoU Conversion Entry'
                }
            },
            {
                state: 'ScmReceiveReturnList',
                config: {
                    url: '/ScmReceiveReturnList',
                    controller: 'ScmReceiveReturnListController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ScmReceiveReturnList',
                    Title: 'Material Receive Return List'
                }
            },
            {
                state: 'ScmReceiveReturn',
                config: {
                    url: '/ScmReceiveReturn?pSCM_STR_RCV_H_ID&pSCM_STR_RET_H_ID',
                    controller: 'ScmReceiveReturnController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ScmReceiveReturn',
                    Title: 'Material Receive Return ',
                    resolve: {
                        formData: function (InventoryDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_STR_RET_H_ID) && $stateParams.pSCM_STR_RET_H_ID > 0) {

                                return InventoryDataService.getDataByFullUrl('/api/Inv/StoreReceive/GetReceiveReturnInfoByID?pSCM_STR_RET_H_ID=' + ($stateParams.pSCM_STR_RET_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                            else if (angular.isDefined($stateParams.pSCM_STR_RCV_H_ID) && $stateParams.pSCM_STR_RCV_H_ID > 0) {

                                return InventoryDataService.getDataByFullUrl('/api/Inv/StoreReceive/GetStoreReceiveInfoByID?pSCM_STR_RCV_H_ID=' + ($stateParams.pSCM_STR_RCV_H_ID || 0)).then(function (res) {
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
                state: 'GeneralStoreTransferList',
                config: {
                    url: '/GeneralStoreTransferList',
                    controller: 'GeneralStoreTransferListController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_GeneralStoreTransferList',
                    Title: 'General Store Transfer List'
                }
            },
            {
                state: 'GeneralStoreTransfer',
                config: {
                    url: '/GeneralStoreTransfer?pINV_STR_TR_REQ_H_ID&pLK_STR_TRN_TYP_ID',
                    controller: 'GeneralStoreTransferController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_GeneralStoreTransfer',
                    Title: 'General Store Transfer',
                    resolve: {
                        formData: function (InventoryDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pINV_STR_TR_REQ_H_ID) && $stateParams.pINV_STR_TR_REQ_H_ID > 0) {

                                return InventoryDataService.getDataByFullUrl('/api/inv/StoreTransfer/GetStoreTransferInfo?pINV_STR_TR_REQ_H_ID=' + ($stateParams.pINV_STR_TR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'GeneralStoreTransferIssue',
                config: {
                    url: '/GeneralStoreTransferIssue?pINV_STR_TR_REQ_H_ID&pINV_STR_TR_ISS_H_ID',
                    controller: 'GeneralStoreTransferIssueController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_GeneralStoreTransferIssue',
                    Title: 'Store Transfer Issue',
                    resolve: {
                        formData: function (InventoryDataService, $stateParams) {

                            if (angular.isDefined($stateParams.pINV_STR_TR_ISS_H_ID) && $stateParams.pINV_STR_TR_ISS_H_ID > 0) {

                                return InventoryDataService.getDataByFullUrl('/api/inv/StoreTransfer/GetIssueByID?pINV_STR_TR_ISS_H_ID=' + ($stateParams.pINV_STR_TR_ISS_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                            else if (angular.isDefined($stateParams.pINV_STR_TR_REQ_H_ID) && $stateParams.pINV_STR_TR_REQ_H_ID > 0) {

                                return InventoryDataService.getDataByFullUrl('/api/inv/StoreTransfer/GetStoreTransferInfo?pINV_STR_TR_REQ_H_ID=' + ($stateParams.pINV_STR_TR_REQ_H_ID || 0)).then(function (res) {
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
                state: 'ScmGenStrReqList',
                config: {
                    url: '/ScmGenStrReqList',
                    controller: 'GeneralStoreRequisitionListController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ScmGenStrReqList',
                    Title: 'General Store Requisition List'
                }
            },
            {
                state: 'ScmGenStrReq',
                config: {
                    url: '/ScmGenStrReq?pSCM_STR_GEN_REQ_H_ID',
                    controller: 'GeneralStoreRequisitionController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ScmGenStrReq',
                    Title: 'General Store Requisition',
                    resolve: {
                        formData: function (InventoryDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_STR_GEN_REQ_H_ID) && $stateParams.pSCM_STR_GEN_REQ_H_ID > 0) {

                                return InventoryDataService.getDataByFullUrl('/api/inv/GenStrReq/GetGenStrReqInfo?pSCM_STR_GEN_REQ_H_ID=' + ($stateParams.pSCM_STR_GEN_REQ_H_ID || 0)).then(function (res) {
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
                state: 'ScmGenStrIssueList',
                config: {
                    url: '/ScmGenStrIssueList',
                    controller: 'GeneralStoreIssueListController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ScmGenStrIssueList',
                    Title: 'General Store Issue List'
                }
            },
            {
                state: 'ScmGenStrIssue',
                config: {
                    url: '/ScmGenStrIssue?pSCM_STR_GEN_REQ_H_ID&pSCM_STR_GEN_ISS_H_ID',
                    controller: 'GeneralStoreIssueController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_ScmGenStrIssue',
                    Title: 'General Store Issue',
                    resolve: {
                        formData: function (InventoryDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_STR_GEN_REQ_H_ID) && $stateParams.pSCM_STR_GEN_REQ_H_ID > 0) {

                                return InventoryDataService.getDataByFullUrl('/api/inv/GenStrReq/GetGenStrReqInfo?pSCM_STR_GEN_REQ_H_ID=' + ($stateParams.pSCM_STR_GEN_REQ_H_ID || 0)).then(function (res) {
                                    return res;
                                });
                            }
                            else if (angular.isDefined($stateParams.pSCM_STR_GEN_ISS_H_ID) && $stateParams.pSCM_STR_GEN_ISS_H_ID > 0) {

                                return InventoryDataService.getDataByFullUrl('/api/inv/GenStrReq/GetGenStrIssueInfoByID?pSCM_STR_GEN_ISS_H_ID=' + ($stateParams.pSCM_STR_GEN_ISS_H_ID || 0)).then(function (res) {
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
                state: 'InvReportParams',
                config: {
                    url: '/InvReportParams',
                    views: {
                        "InvReportParams": {
                            controller: 'InvReportController',
                            controllerAs: 'vm',
                            templateUrl: '/Inventory/Inv/_InvReportParams'
                        }
                    },
                    Title: 'Inventory Reports',
                    resolve: {
                        InvRptData: function (InventoryDataService) {
                            return InventoryDataService.getDataByUrlNoToken('/api/common/getReportDataListByUser/' + 2); //// Here 1 is report group ID
                        }
                    }
                }
            },
            {
                state: 'OpeningBal',
                config: {
                    url: '/OpeningBal',
                    controller: 'OpeningBalController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_OpeningBal',
                    Title: 'General Store Opening Balance'
                }
            },
            {
                state: 'StrInActiveItem',
                config: {
                    url: '/StrInActiveItem',
                    controller: 'StrInActiveItemController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_StrInActiveItem',
                    Title: 'Store Inactive Item'
                }
            },
            {
                state: 'StrIssRtnList',
                config: {
                    url: '/StrIssRtnList',
                    controller: 'StrIssRtnListController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_StrIssRtnList',
                    Title: 'General Store Issue Return List'
                }
            },
            {
                state: 'StrIssRtn',
                config: {
                    url: '/StrIssRtn?pSCM_STR_GEN_ISS_RTN_H_ID',
                    controller: 'StrIssRtnController',
                    controllerAs: 'vm',
                    templateUrl: '/Inventory/Inv/_StrIssRtn',
                    Title: 'General Store Issue Return',
                    resolve: {
                        formData: function (InventoryDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_STR_GEN_ISS_RTN_H_ID) && $stateParams.pSCM_STR_GEN_ISS_RTN_H_ID > 0) {

                                return InventoryDataService.getDataByFullUrl('/api/Inv/IssueReturn/GetIssueReturnInfo?pSCM_STR_GEN_ISS_RTN_H_ID=' + ($stateParams.pSCM_STR_GEN_ISS_RTN_H_ID || 0)).then(function (res) {
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
