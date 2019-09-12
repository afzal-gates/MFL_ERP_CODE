// Start Bundle Card 

(function () {
    'use strict';
    angular.module('multitex.cutting').directive('checkBundlCard', function (CuttingDataService, $q, config) {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                ngModel.$asyncValidators.AlreadyScanned = function (modelValue, viewValue) {

                    if (/^\d+$/.test(viewValue) && viewValue.length == 15) {
                        
                        var vBarCode = viewValue.substr(0, 14).trim();
                        
                        return CuttingDataService.getDataByUrl('/LayChart/GetBndlCardInfo?pBUNDLE_BARCODE=' + vBarCode).then(function (res) {
                            if (res.GMT_BNDL_CRD_PDATA_ID > 0) {
                                return $q.resolve();
                            }
                            else {
                                return $q.reject();
                            }
                        });
                        
                    }
                    else {
                        return $q.resolve();
                    }
                };
            }
        };
    });
})();




////////// Start CutBundleCardAmendController Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutBundleCardAmendController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', '$modal', 'PROD_PLN_CLNDR_ID', CutBundleCardAmendController]);
    function CutBundleCardAmendController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog, $modal, PROD_PLN_CLNDR_ID) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        $scope.BUNDLE_BARCODE = '';

        activate();
        function activate() {
            var promise = [getLastBundleScannedData($stateParams.pGMT_CUT_BNDL_AMND_ID), getBndlAmndList()];
            return $q.all(promise).then(function () {

                vm.showSplash = false;
            });
        }

        vm.openDefectModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_CutBundleCardAmendModal',
                controller: 'CutBundleCardAmendModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    BundleCaradList: function () {
                        return CuttingDataService.getDataByUrl('/LayChart/GetBndlAmndList4Finalize?pGMT_PROD_PLN_CLNDR_ID=' + PROD_PLN_CLNDR_ID);
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                console.log(dta);
            });
        }

        function getLastBundleScannedData(pGMT_CUT_BNDL_AMND_ID) {
            if (!pGMT_CUT_BNDL_AMND_ID)
                return;
            return CuttingDataService.getDataByUrl('/LayChart/GetLastBndlAmndInfo?pGMT_CUT_BNDL_AMND_ID=' + pGMT_CUT_BNDL_AMND_ID).then(function (res) {
                vm.LastBundle = res;
            });
        }

        function getBndlAmndList() {
            return CuttingDataService.getDataByUrl('/LayChart/GetBndlAmndList?pGMT_PROD_PLN_CLNDR_ID=' + PROD_PLN_CLNDR_ID).then(function (res) {
                vm.dataList = res;
                //vm.totalBndlPending = _.sumBy(res, function (o) { return o.TTL_BNDL_PENDING; });;
            });
        };

        vm.removeBndlAmnd = function (pGMT_CUT_BNDL_AMND_ID) {
            Dialog.confirm('Do you want to remove the record?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {
                     return CuttingDataService.saveDataByUrl({
                         GMT_CUT_BNDL_AMND_ID: pGMT_CUT_BNDL_AMND_ID                         
                     }, '/LayChart/RemoveBundleCardAmnd').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                             new Audio('../../Content/sounds/errorSound.mp3').play();
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $state.go('CutBundleCardAmend', {}, { inherit: false, reload: true });
                             }
                         }
                     })
                 });
        }



        vm.submitData = function (BUNDLE_BARCODE, valid) {            

            if (!valid) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.frmBndlCardAmend['BUNDLE_BARCODE'].$setValidity('invalid_barcode', true);
                return;
            }

            if (BUNDLE_BARCODE.length < 15 || BUNDLE_BARCODE.length > 15 /*|| ! /^\d+$/.test(BUNDLE_BARCODE)*/) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.frmBndlCardAmend['BUNDLE_BARCODE'].$setValidity('invalid_barcode', false);
                return;
            }
            //var IS_OK = parseInt(BUNDLE_BARCODE.substr(BUNDLE_BARCODE.length - 4, 1));
            //if (!(IS_OK == 0 || IS_OK == 1)) {
            //    new Audio('../../Content/sounds/errorSound.mp3').play();
            //    $scope.frmBndlCardAmend['BUNDLE_BARCODE'].$setValidity('invalid_barcode', false);

            //    return;
            //}

            var vBarCode = BUNDLE_BARCODE.substr(0, 14).trim();

            
            return CuttingDataService.saveDataByUrl({                
                BUNDLE_BARCODE: vBarCode,                
                GMT_PROD_PLN_CLNDR_ID: PROD_PLN_CLNDR_ID
            }, '/LayChart/SaveBundleCardAmnd').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                    new Audio('../../Content/sounds/errorSound.mp3').play();
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        new Audio('../../Content/sounds/OkSound.mp3').play();
                        $scope.frmBndlCardAmend['BUNDLE_BARCODE'].$setValidity('invalid_barcode', true);
                        $scope.BUNDLE_BARCODE = '';
                        getLastBundleScannedData(res.data.PGMT_CUT_BNDL_AMND_ID_RTN);
                        getBndlAmndList();
                        $state.go('CutBundleCardAmend', { pGMT_CUT_BNDL_AMND_ID: res.data.PGMT_CUT_BNDL_AMND_ID_RTN }, { notify: false });
                    }
                    else {
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        $scope.frmBndlCardAmend['BUNDLE_BARCODE'].$setValidity('invalid_barcode', false);
                    }
                }
            });
            
        }

    };
})();
////////// End CutBundleCardAmendController Controller






