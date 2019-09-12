(function () {
    'use strict';

    angular.module('multitex.knitting').controller('LblPrinterCfgModalController', ['$modalInstance', '$scope', 'config', 'KnittingDataService', '$localStorage', LblPrinterCfgModalController]);

    function LblPrinterCfgModalController($modalInstance, $scope, config, KnittingDataService, $localStorage) {


        $scope.PRNTR_ADDRESS = ($localStorage.PRNTR_ADDRESS || null);

        KnittingDataService.getDataByFullUrl('/api/common/getLabelPrinter').then(function (res) {
            $scope.PrinterList = res;
        });

        $scope.save = function (pPRNTR_ADDRESS) {
            $localStorage.PRNTR_ADDRESS = pPRNTR_ADDRESS;
            $modalInstance.close(true);
        };
        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();