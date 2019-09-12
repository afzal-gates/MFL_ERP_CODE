(function () {
    'use strict';

    angular.module('multitex.core').controller('ConfirmModalController', ['$modalInstance','data',ConfirmModalController]);
    function ConfirmModalController($modalInstance, data) {
        var vm = this;

        vm.cancel = cancel;
        vm.ok = ok;
        vm.properties = data;

        function cancel() {
            $modalInstance.dismiss();
        }

        function ok() {
            $modalInstance.close();
        }


    };
})();