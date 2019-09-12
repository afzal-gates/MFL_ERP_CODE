
////////// Start CutPanelInspection Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('ReScan4SewInputController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', '$modal', 'V_GMT_PROD_PLN_CLNDR_ID', ReScan4SewInputController]);
    function ReScan4SewInputController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog, $modal, V_GMT_PROD_PLN_CLNDR_ID) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.LastBundle = { GMT_CUT_PNL_BNK_ID: -1 };
        vm.GMT_CUT_PNL_BNK_LST = '';

        activate();
        function activate() {
            var promise = [getStoreRecvSummaryData(), getLastBundleScannedData($stateParams.pGMT_CUT_PNL_BNK_ID)];
            return $q.all(promise).then(function () {
                
                vm.showSplash = false;                
            });
        }



        function getLastBundleScannedData(pGMT_CUT_PNL_BNK_ID) {
            if (!pGMT_CUT_PNL_BNK_ID)
                return;
            return CuttingDataService.getDataByUrl('/GmtCutBank/getLastScannedBundle?pGMT_CUT_PNL_BNK_ID=' + pGMT_CUT_PNL_BNK_ID).then(function (res) {
                vm.LastBundle = res;
            });
        }

        //vm.RemoveFromScannedList = function (V_GMT_CUT_PNL_BNK_LST) {
        //    Dialog.confirm('<h5 style="margin:0px;">Deleting...</h5', 'Are you sure?', ['Yes', ' No'])
        //         .then(function () {
        //             return CuttingDataService.saveDataByUrl({
        //                 GMT_CUT_PNL_BNK_LST: V_GMT_CUT_PNL_BNK_LST,
        //                 pOption: 1001,
        //             }, '/GmtCutBank/SaveAndOrDelete').then(function (res) {
        //                 if (res.success === false) {
        //                     vm.errors = res.errors;
        //                     new Audio('../../Content/sounds/errorSound.mp3').play();
        //                 }
        //                 else {
        //                     res['data'] = angular.fromJson(res.jsonStr);
        //                     if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
        //                         $state.go('StoreRecv', {}, { reload: true, inherit: false });
        //                     }
        //                 }
        //             })
        //         });
        //}



        function getStoreRecvSummaryData () {
            return CuttingDataService.getDataByUrl('/GmtCutBank/GetRescanData4SewInput')
             .then(function(res){
                 vm.summaryData = res;

                 vm.ttlBundleScanned = _.sumBy(res, function (o) { return o.TTL_NO_OF_BNDL_SCANNED; });
                 vm.ttlQtyScanned = _.sumBy(res, function (o) { return o.TTL_FINAL_QTY; });
                 vm.GMT_CUT_PNL_BNK_LST = res.map(function (o) { return o.GMT_CUT_PNL_BNK_LST; }).join(',');

             });
        }

        vm.submitData = function (BARCODE, valid) {
            vm.errors = null;
            if (!BARCODE ||BARCODE.length < 18 || BARCODE.length > 18 || ! /^\d+$/.test(BARCODE)) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.CutPanelStoreRecvForm['BARCODE'].$setValidity('invalid_barcode', false);
                $scope.BARCODE = '';
                return;
            }
            var IS_OK = parseInt(BARCODE.substr(BARCODE.length - 4, 1));
            if (!(IS_OK == 0 || IS_OK == 1)) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.CutPanelStoreRecvForm['BARCODE'].$setValidity('invalid_barcode', false);
                $scope.BARCODE = '';
                return;
            }

            var PART_ID = parseInt(BARCODE.substr(BARCODE.length - 3));
            var BARCODE = BARCODE.substr(0, 14).trim();

            return CuttingDataService.saveDataByUrl({
                GMT_CUT_PNL_BNK_ID: -1,
                BARCODE: BARCODE,
                GMT_PROD_PLN_CLNDR_ID: V_GMT_PROD_PLN_CLNDR_ID,
                pOption: 1004

            }, '/GmtCutBank/SaveAndOrDelete').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                    new Audio('../../Content/sounds/errorSound.mp3').play();
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.errors = null;
                        new Audio('../../Content/sounds/OkSound.mp3').play();
                        $scope.CutPanelStoreRecvForm['BARCODE'].$setValidity('invalid_barcode', true);
                        $scope.BARCODE = '';
                        getStoreRecvSummaryData();
                        getLastBundleScannedData(res.data.OP_GMT_CUT_PNL_BNK_ID);
                        $state.go('ReScan4SewInput', { pGMT_CUT_PNL_BNK_ID: res.data.OP_GMT_CUT_PNL_BNK_ID }, { notify: false });
                    } else {
                        vm.errors = res.data.PMSG.substr(9);
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        $scope.CutPanelStoreRecvForm['BARCODE'].$setValidity('invalid_barcode', false);
                    }
                    $scope.BARCODE = '';
                }
            })

        }
        
        vm.openSewDelvChalnModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_SewingDeliveryChallanModal',
                controller: 'SewingDeliveryChallanModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    BundleDatas: function () {
                        return {
                            datas: vm.summaryData,
                            total_bundles: _.sumBy(vm.summaryData, function (o) { return o.TTL_NO_OF_BNDL_SCANNED; }),
                            total_cut_qty: _.sumBy(vm.summaryData, function (o) { return o.TTL_CUTTING_QTY; }),
                            total_qty: _.sumBy(vm.summaryData, function (o) { return o.TTL_FINAL_QTY; }),
                            short_qty: _.sumBy(vm.summaryData, function (o) { return o.TTL_SHORT_QTY; }),
                            GMT_CUT_PNL_BNK_LST: vm.summaryData.map(function (o) { return o.GMT_CUT_PNL_BNK_LST; }).join(','),
                            RF_GARM_PART_LST: vm.summaryData.map(function (o) { return o.RF_GARM_PART_LST; }).join(','),

                        }
                    }                   
                }
            });

            modalInstance.result.then(function (dta) {
                $state.go('ReScan4SewInput', {}, { reload: true, inherit: false });
            });
        }
        
        vm.openBundleStatusModal = function (cuttingData) {
            
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_BundleStatusModal',
                controller: 'BundleStatusModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    bndlData: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrl('/GmtCutBank/GetData4BndlStatus?pGMT_CUT_INFO_ID=' + cuttingData.GMT_CUT_INFO_ID).then(function (res) {
                            return res;
                        });
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                console.log(dta);
            });
        }


    };
})();
////////// End CutPanelInspection Controller






