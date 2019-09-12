(function () {
    'use strict';

    angular.module('multitex.security').controller('SecurityController', ['logger', 'SecurityService','config','$q', SecurityController]);

    function SecurityController(logger, SecurityService, config, $q) {
        var vm = this;
        vm.title = config.appTitle;
        vm.showSplash = true;

        activate(); 

        alert("");

        function activate() {
            var promises = [getName()];
            return $q.all(promises).then(function () {
                vm.showSplash = false;
                //logger.success(config.appTitle + ' : Security Module loaded!', null);
            });
        }




        function getName() {
           return SecurityService.getName().then(function (data) {
               vm.contact = data;
               return vm.contact;
            });
        }



    }

})();