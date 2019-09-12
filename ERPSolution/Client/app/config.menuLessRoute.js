(function () {
    'use strict';

    angular
        .module('multitex.menulesspage')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [

            {
                state: 'KnitPlanJobCardRollInsp',
                config: {
                    url: '/RollInspection',
                    views: {
                        "KnitPlanJobCardRollInsp": {
                            controller: 'KnitPlanJobCardRollInspController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanJobCardRollInsp'
                        },

                    },
                    //resolve: {
                    //    KnitPlanHeader: function (KnittingDataService, $stateParams) {
                    //    }
                    //},
                    Title: 'Grey Fabric Roll Inspection'

                },
            },
            {
                state: 'KnitPlanJobCardRollProdTab',
                config: {
                    url: '/RollProdTab',
                    views: {
                        "KnitPlanJobCardRollProdTab": {
                            controller: 'KnitPlanJobCardRollProdTabController',
                            controllerAs: 'vm',
                            templateUrl: '/Knitting/Knit/_KnitPlanJobCardRollProdTab'
                        },

                    },
                    //resolve: {
                    //    KnitPlanHeader: function (KnittingDataService, $stateParams) {
                    //    }
                    //},
                    Title: 'Grey Fabric Roll Production'

                },
            },
            {
                state: 'AddFabBkingRpt',
                config: {
                    url: '/AddFabBkingRpt?pMC_BLK_ADFB_REQ_H_ID',
                    views: {
                        "AddFabBkingRpt": {
                            controller: 'AddFabBkingRptController',
                            controllerAs: 'vm',
                            templateUrl: '/Purchase/Purchase/_AddFabBkingRpt',
                        },
                    },
                    resolve: {
                        addFabBkRrptData: function (MenuLessPageDataService, $stateParams) {
                            return MenuLessPageDataService.getDataByFullUrl('/api/mrc/AddFabBk/GetAddFabBkRpt?pMC_BLK_ADFB_REQ_H_ID=' + $stateParams.pMC_BLK_ADFB_REQ_H_ID || 0);
                        }
                    },

                    Title: 'Additional Fabric Booking Report',
                    reloadOnSearch: false
                }
            },
            {
                state: 'serverMaintenance',
                config: {
                    url: '/serverMaintenance',
                    views: {
                        "serverMaintenance": {
                            controller: 'serverMaintenanceController',
                            controllerAs: 'vm',
                            templateUrl: '/Security/Security/_serverMaintenance',
                        },
                    },
                    resolve: {
                        fromData: function (MenuLessPageDataService, $stateParams) {

                            return MenuLessPageDataService.getDataByFullUrl('/api/security/GlobalNotify/OfflineMsg');
                        }
                    },

                    Title: 'Maintenance',
                    reloadOnSearch: false
                }
            },
            
        ];
    }
})();
