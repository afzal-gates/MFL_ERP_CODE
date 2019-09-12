(function () {
    'use strict';

    angular
        .module('multitex.genRpt')
        .run(appRun);

    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates(), '/');
    }

    function getStates() {
        return [

            {
                state: 'GeneralReport',
                config: {
                    url: '/GeneralReport',
                    Title: 'General Reports',
                    controller: 'GeneralReportController',
                    controllerAs: 'vm',
                    templateUrl: '/GenRpt/GenRpt/_GeneralReport'
                }
            },

        ];
    }
})();
