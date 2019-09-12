(function () {
    'use strict';

    angular
        .module('multitex.purchase')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [



            /////////// Start for Supplier Profile
            {
                state: 'SupplierMaster',
                config: {
                    url: '/SupplierProfile?pSCM_SUPPLIER_ID',
                    controller: 'SupplierProfileController',
                    controllerAs: 'vm',
                    templateUrl: '/Purchase/Purchase/_SupplierMaster',
                    Title: 'Supplier/Party Profile',
                    resolve: {
                        formData: function (PurchaseDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pSCM_SUPPLIER_ID) && $stateParams.pSCM_SUPPLIER_ID > 0) {

                                return PurchaseDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetSupplierInfo?pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0)).then(function (res) {
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
               state: 'SupplierList',
               config: {
                   url: '/SupplierList',
                   controller: 'SupplierListController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_SupplierList',
                   Title: 'Supplier/Party List'
               }
           },
           {
               state: 'GeneratePOList',
               config: {
                   url: '/GeneratePOList',
                   controller: 'GeneratePOListController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_GeneratePOList',
                   Title: 'List of PO to Supplier'
               }
           },
           {
                state: 'GeneratePO',
                config: {
                    url: '/GeneratePO?pCM_IMP_PO_H_ID&pREVISE',
                    controller: 'GeneratePOController',
                    controllerAs: 'vm',
                    templateUrl: '/Purchase/Purchase/_GeneratePO',
                    Title: 'PO to Supplier',
                    resolve: {
                        formData: function (PurchaseDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pCM_IMP_PO_H_ID) && $stateParams.pCM_IMP_PO_H_ID > 0) {

                                return PurchaseDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetPOByID?pCM_IMP_PO_H_ID=' + ($stateParams.pCM_IMP_PO_H_ID || 0)).then(function (res) {
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
               state: 'PurchaseRequisitionList',
               config: {
                   url: '/PurchaseRequisitionList',
                   controller: 'PurchaseRequisitionListController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_PurchaseRequisitionList',
                   Title: 'Purchase Requisition List'
               }
           },
           {
               state: 'PurchaseRequisition',
               config: {
                   url: '/PurchaseRequisition?pSCM_PURC_REQ_H_ID',
                   controller: 'PurchaseRequisitionController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_PurchaseRequisition',
                   Title: 'Purchase Requisition',
                   resolve: {
                       formData: function (PurchaseDataService, $stateParams) {
                           if (angular.isDefined($stateParams.pSCM_PURC_REQ_H_ID) && $stateParams.pSCM_PURC_REQ_H_ID > 0) {

                               return PurchaseDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetYarnBuffReqInfo?pSCM_PURC_REQ_H_ID=' + ($stateParams.pSCM_PURC_REQ_H_ID || 0)).then(function (res) {
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
               state: 'FundRequisitionList',
               config: {
                   url: '/FundRequisitionList',
                   controller: 'FundRequisitionListController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_FundRequisitionList',
                   Title: 'Fund Requisition List'
               }
           },
           {
               state: 'FundRequisition',
               config: {
                   url: '/FundRequisition?pSCM_FUND_REQ_H_ID',
                   controller: 'FundRequisitionController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_FundRequisition',
                   Title: 'Fund Requisition',
                   resolve: {
                       formData: function (PurchaseDataService, $stateParams) {
                           if (angular.isDefined($stateParams.pSCM_FUND_REQ_H_ID) && $stateParams.pSCM_FUND_REQ_H_ID > 0) {

                               return PurchaseDataService.getDataByFullUrl('/api/Purchase/fund/GetFundInfoByID?pSCM_FUND_REQ_H_ID=' + ($stateParams.pSCM_FUND_REQ_H_ID || 0)).then(function (res) {
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
               state: 'GeneralPOList',
               config: {
                   url: '/GeneralPOList',
                   controller: 'GeneralPOListController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_GeneralPOList',
                   Title: 'General PO List'
               }
           },
           {
               state: 'GeneralPO',
               config: {
                   url: '/GeneralPO?pCM_IMP_PO_H_ID&pSCM_PURC_REQ_H_ID',
                   controller: 'GeneralPOController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_GeneralPO',
                   Title: 'General PO',
                   resolve: {
                       formData: function (PurchaseDataService, $stateParams) {
                           if (angular.isDefined($stateParams.pCM_IMP_PO_H_ID) && $stateParams.pCM_IMP_PO_H_ID > 0) {

                               return PurchaseDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetPOByID?pCM_IMP_PO_H_ID=' + ($stateParams.pCM_IMP_PO_H_ID || 0)).then(function (res) {
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
               state: 'ReceiveList',
               config: {
                   url: '/ReceiveList',
                   controller: 'ReceiveListController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_ReceiveList',
                   Title: 'Receive List'
               }
           },
           {
               state: 'Receive',
               config: {
                   url: '/Receive?pSCM_STR_RCV_H_ID',
                   controller: 'ReceiveController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_Receive',
                   Title: 'Receive',
                   resolve: {
                       formData: function (PurchaseDataService, $stateParams) {
                           if (angular.isDefined($stateParams.pSCM_STR_RCV_H_ID) && $stateParams.pSCM_STR_RCV_H_ID > 0) {

                               return PurchaseDataService.getDataByFullUrl('/api/Inv/StoreReceive/GetStoreReceiveInfoByID?pSCM_STR_RCV_H_ID=' + ($stateParams.pSCM_STR_RCV_H_ID || 0)).then(function (res) {
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
               state: 'CertificateType',
               config: {
                   url: '/CertificateType',
                   controller: 'CertificateTypeController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_CertificateType',
                   Title: 'Certificate Type'
               }
           },
           {
               state: 'CertificateList',
               config: {
                   url: '/CertificateList',
                   controller: 'CertificateListController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_CertificateList',
                   Title: 'Certificates List'
               }
           },
           {
               state: 'Certificate',
               config: {
                   url: '/Certificate?pHR_AUD_CERT_REG_ID&pIS_NEW_OR_R',
                   controller: 'CertificateController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_Certificate',
                   Title: 'Certificate',
                   resolve: {
                       formData: function (PurchaseDataService, $stateParams) {
                           if (angular.isDefined($stateParams.pHR_AUD_CERT_REG_ID) && $stateParams.pHR_AUD_CERT_REG_ID > 0) {

                               return PurchaseDataService.getDataByFullUrl('/api/Purchase/AuditCertificate/GetCertificateByID?pHR_AUD_CERT_REG_ID=' + ($stateParams.pHR_AUD_CERT_REG_ID || 0)).then(function (res) {
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
               state: 'InvAdjList',
               config: {
                   url: '/InvAdjList',
                   controller: 'InvAdjListController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_InvAdjList',
                   Title: 'Inventory Adjustment List'
               }
           },
           {
               state: 'InvAdj',
               config: {
                   url: '/InvAdj?pSCM_STR_ITM_ADJ_H_ID',
                   controller: 'InvAdjController',
                   controllerAs: 'vm',
                   templateUrl: '/Purchase/Purchase/_InvAdj',
                   Title: 'Inventory Adjustment',
                   resolve: {
                       formData: function (PurchaseDataService, $stateParams) {
                           if (angular.isDefined($stateParams.pSCM_STR_ITM_ADJ_H_ID) && $stateParams.pSCM_STR_ITM_ADJ_H_ID > 0) {

                               return PurchaseDataService.getDataByFullUrl('/api/Purchase/InvAdj/GetInvAdjInfoByID?pSCM_STR_ITM_ADJ_H_ID=' + ($stateParams.pSCM_STR_ITM_ADJ_H_ID || 0)).then(function (res) {
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

        ];
    }
})();
