(function () {
    'use strict';

    angular
        .module('multitex.cmr')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [

            {
                state: 'BuyerNotifyParty',
                config: {
                    url: '/BuyerNotifyParty',
                    Title: 'Buyer Wise Notify Party',
                    controller: 'BuyerNotifyPartyController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_BuyerNotifyParty'
                }
            },
            {
                state: 'ReferenceType',
                config: {
                    url: '/ReferenceType',
                    Title: 'Reference Type Data Entry',
                    controller: 'ReferenceTypeController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_ReferenceType'
                }
            },

            {
                state: 'ReferenceType.IncoTerm',
                config: {
                    url: '/IncoTerm',
                    Title: 'Inco Term',
                    views: {
                        "sub-view": {
                            controller: 'IncoTermController',
                            controllerAs: 'vm',
                            templateUrl: '/Commercial/Cmr/_IncoTerm'
                        }
                    }
                }
            },

            {
                state: 'ReferenceType.PaymentTerm',
                config: {
                    url: '/PaymentTerm',
                    Title: 'Payment Term',
                    views: {
                        "sub-view": {
                            controller: 'PaymentTermController',
                            controllerAs: 'vm',
                            templateUrl: '/Commercial/Cmr/_PaymentTerm'
                        }
                    }
                }
            },
            {
                state: 'ReferenceType.DeliveryPlace',
                config: {
                    url: '/DeliveryPlace',
                    Title: 'Delivery Place',
                    views: {
                        "sub-view": {
                            controller: 'DeliveryPlaceController',
                            controllerAs: 'vm',
                            templateUrl: '/Commercial/Cmr/_DeliveryPlace'
                        }
                    }
                }
            },
            {
                state: 'ReferenceType.TransitPort',
                config: {
                    url: '/TransitPort',
                    Title: 'Transit Port',
                    views: {
                        "sub-view": {
                            controller: 'TransitPortController',
                            controllerAs: 'vm',
                            templateUrl: '/Commercial/Cmr/_TransitPort'
                        }
                    }
                }
            },
            {
                state: 'ReferenceType.LCType',
                config: {
                    url: '/LCType',
                    Title: 'LC Type',
                    views: {
                        "sub-view": {
                            controller: 'LCTypeController',
                            controllerAs: 'vm',
                            templateUrl: '/Commercial/Cmr/_LCType'
                        }
                    }
                }
            },
            {
                state: 'ReferenceType.ShipMode',
                config: {
                    url: '/ShipMode',
                    Title: 'Shipment Mode',
                    views: {
                        "sub-view": {
                            controller: 'ShipModeController',
                            controllerAs: 'vm',
                            templateUrl: '/Commercial/Cmr/_ShipMode'
                        }
                    }
                }
            },
            {
                state: 'ReferenceType.GmtAsn',
                config: {
                    url: '/GmtAsn',
                    Title: 'Garments Business Association',
                    views: {
                        "sub-view": {
                            controller: 'GmtAsnController',
                            controllerAs: 'vm',
                            templateUrl: '/Commercial/Cmr/_GmtAsn'
                        }
                    }
                }
            },
            {
                state: 'ExpLcContractList',
                config: {
                    url: '/ExpLcContractList',
                    Title: 'Export LC/Sales Contact',
                    controller: 'ExpLcContractListController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_ExpLcContractList'                    
                }
            },
            {
                state: 'ExpLcContract',
                config: {
                    url: '/ExpLcContract?pCM_EXP_LC_H_ID&pREVISE',
                    controller: 'ExpLcContractController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_ExpLcContract',
                    Title: 'Export LC/Sales Contact',
                    resolve: {
                        formData: function (CmrDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pCM_EXP_LC_H_ID) && $stateParams.pCM_EXP_LC_H_ID > 0) {

                                return CmrDataService.getDataByFullUrl('/api/cmr/ExportLCSalesContact/GetExportLcContactInfo?pCM_EXP_LC_H_ID=' + ($stateParams.pCM_EXP_LC_H_ID || 0)).then(function (res) {
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
                state: 'ExpLcPIList',
                config: {
                    url: '/ExpLcPIList',
                    Title: 'Export LC PI List',
                    controller: 'ExpLcPIListController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_ExpLcPIList'
                }
            },
            {
                state: 'ExpLcPI',
                config: {
                    url: '/ExpLcPI?pCM_EXP_PI_H_ID&pREVISE',
                    controller: 'ExpLcPIController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_ExpLcPI',
                    Title: 'Export LC PI',
                    resolve: {
                        formData: function (CmrDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pCM_EXP_PI_H_ID) && $stateParams.pCM_EXP_PI_H_ID > 0) {

                                return CmrDataService.getDataByFullUrl('/api/cmr/ExportLCPI/GetExportLcPIInfo?pCM_EXP_PI_H_ID=' + ($stateParams.pCM_EXP_PI_H_ID || 0)).then(function (res) {
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
                state: 'ImpLcPIList',
                config: {
                    url: '/ImpLcPIList',
                    Title: 'Import LC PI List',
                    controller: 'ImpLcPIListController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_ImpLcPIList'
                }
            },
            {
                state: 'ImpLcPI',
                config: {
                    url: '/ImpLcPI?pCM_IMP_PI_H_ID&pREVISE',
                    controller: 'ImpLcPIController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_ImpLcPI',
                    Title: 'Import LC PI',
                    resolve: {
                        formData: function (CmrDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pCM_IMP_PI_H_ID) && $stateParams.pCM_IMP_PI_H_ID > 0) {

                                return CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/GetImportLcPIInfo?pCM_IMP_PI_H_ID=' + ($stateParams.pCM_IMP_PI_H_ID || 0)).then(function (res) {
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
                state: 'ImpLcPOPIRevise',
                config: {
                    url: '/ImpLcPOPIRevise?pCM_IMP_PI_H_ID',
                    controller: 'ImpLcPOPIReviseController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_ImpLcPOPIRevise',
                    Title: 'Import LC PO & PI Revise',
                    resolve: {
                        formData: function (CmrDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pCM_IMP_PI_H_ID) && $stateParams.pCM_IMP_PI_H_ID > 0) {

                                return CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/GetImportLcPIInfo?pCM_IMP_PI_H_ID=' + ($stateParams.pCM_IMP_PI_H_ID || 0)).then(function (res) {
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
                state: 'ImpLcList',
                config: {
                    url: '/ImpLcList',
                    Title: 'Import LC',
                    controller: 'ImpLcListController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_ImpLcList'
                }
            },
            {
                state: 'ImpLc',
                config: {
                    url: '/ImpLc?pCM_IMP_LC_H_ID&pREVISE',
                    controller: 'ImpLcController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_ImpLc',
                    Title: 'Import LC',
                    resolve: {
                        formData: function (CmrDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pCM_IMP_LC_H_ID) && $stateParams.pCM_IMP_LC_H_ID > 0) {

                                return CmrDataService.getDataByFullUrl('/api/cmr/ImportLC/GetImportLc?pCM_IMP_LC_H_ID=' + ($stateParams.pCM_IMP_LC_H_ID || 0)).then(function (res) {
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
                state: 'LcUDList',
                config: {
                    url: '/LcUDList',
                    Title: 'UD List',
                    controller: 'LcUDListController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_LcUDList'
                }
            },
            {
                state: 'LcUD',
                config: {
                    url: '/LcUD?pCM_UD_H_ID&pREVISE',
                    controller: 'LcUDController',
                    controllerAs: 'vm',
                    templateUrl: '/Commercial/Cmr/_LcUD',
                    Title: 'UD Master Information',
                    resolve: {
                        formData: function (CmrDataService, $stateParams) {
                            if (angular.isDefined($stateParams.pCM_UD_H_ID) && $stateParams.pCM_UD_H_ID > 0) {

                                return CmrDataService.getDataByFullUrl('/api/cmr/LcUD/GetUDInfo?pCM_UD_H_ID=' + ($stateParams.pCM_UD_H_ID || 0)).then(function (res) {
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
