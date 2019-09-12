(function () {
    'use strict';

    angular
        .module('multitex.displayboard')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [
            {
                state: 'HourlyProductionBoard',
                config: {
                    url: '/HourlyProdBoard',
                    views: {
                        "HourlyProdBoard": {
                            controller: 'HourlyProductionBoardController',
                            controllerAs: 'vm',
                            templateUrl: '/Home/_HourlyProdBoard'
                        },

                    },
                    //resolve: {
                    //    KnitPlanHeader: function (KnittingDataService, $stateParams) {
                    //    }
                    //},
                    Title: 'Hourly Production Board'

                },
            },
            {
                state: 'GmtProductionBoard',
                config: {
                    url: '/GmtProductionBoard',
                    views: {
                        "GmtProductionBoard": {
                            controller: 'GmtProductionBoardController',
                            controllerAs: 'vm',
                            templateUrl: '/Home/_GmtProductionBoard'
                        },
                    },
                    Title: 'Hourly Production Board'
                },
            }
        ];
    }
})();
