(function() {
    'use strict';

    angular.module('multitex', [
        /*
         * Order is not important. Angular makes a
         * pass to register all of the modules listed
         * and then when app.dashboard tries to use app.data,
         * it's components are available.
         */

        /*
         * Everybody has access to these.
         * We could place these under every feature area,
         * but this is easier to maintain.
         */ 
        'multitex.core',
        'ui.bootstrap',

         
         'multitex.security',         
         'multitex.admin',
         'multitex.hr',
         'multitex.mrc',         
         'multitex.inventory',
         'multitex.purchase',
         'multitex.dyeing',
         'multitex.knitting',
         'multitex.cmr',
         'multitex.garments',
         'multitex.planning',
         'multitex.ie',
         'multitex.cutting'
         
    ]);

})();