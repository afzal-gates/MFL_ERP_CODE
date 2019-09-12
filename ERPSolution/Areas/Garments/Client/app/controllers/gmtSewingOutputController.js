////////// Start GmtSewingOutPut Controller
(function () {
    'use strict';
    angular.module('multitex.garments').controller('GmtSewingOutputController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'GarmentsDataService', 'Dialog', '$modal', GmtSewingOutputController]);
    function GmtSewingOutputController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, GarmentsDataService, Dialog, $modal) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        activate();
        function activate() {
            var promise = [getCalendarDateList(), getLastBundleScannedData($stateParams.pGMT_SEW_PROD_SCAN_ID)];
            return $q.all(promise).then(function () {
                
                vm.showSplash = false;                
            });
        }


        function getCalendarDateList() {
            return GarmentsDataService.getDataByFullUrl('/api/pln/CapctiClndr/getCurrentProdCalendarList').then(function (res) {
                vm.clndrDateDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function getSewProdScanSummeryData(GMT_PROD_PLN_CLNDR_ID) {
            return GarmentsDataService.getDataByUrl('/GmtSewProdScan/GetSewProdScanSummery?pGMT_PROD_PLN_CLNDR_ID=' + GMT_PROD_PLN_CLNDR_ID).then(function (res) {
                vm.dataList = res;
                vm.totalBndlPending = _.sumBy(res, function (o) { return o.TTL_BNDL_PENDING; });
                vm.totalBndlOk = _.sumBy(res, function (o) { return o.TTL_BNDL_OK; });
            });
        };

        function FinSewProdScanHrlyData(GMT_PROD_PLN_CLNDR_ID) {
            return GarmentsDataService.getDataByUrl('/GmtSewProdScan/FinSewProdScanHrlyData?pGMT_PROD_PLN_CLNDR_ID=' + GMT_PROD_PLN_CLNDR_ID).then(function (res) {
                vm.dataListHrly = res;
            });
        };


        vm.ClndrListOnBound = function (e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].GMT_PROD_PLN_CLNDR_ID);
                vm.GMT_PROD_PLN_CLNDR_ID = ds[0].GMT_PROD_PLN_CLNDR_ID;
                getSewProdScanSummeryData(ds[0].GMT_PROD_PLN_CLNDR_ID);
                FinSewProdScanHrlyData(ds[0].GMT_PROD_PLN_CLNDR_ID);
            }
        }

        vm.ClndrListOnChange = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item && item.GMT_PROD_PLN_CLNDR_ID) {
                getSewProdScanSummeryData(item.GMT_PROD_PLN_CLNDR_ID);
                FinSewProdScanHrlyData(item.GMT_PROD_PLN_CLNDR_ID);
            }
        }

        function getLastBundleScannedData(pGMT_SEW_PROD_SCAN_ID) {
            if (!pGMT_SEW_PROD_SCAN_ID)
                return;
            return GarmentsDataService.getDataByFullUrl('/api/Cutting/CutCutPnlInspect/getLastBundleCardScannedInfo?pGMT_CUT_PNL_INSPTN_H_ID=' + pGMT_SEW_PROD_SCAN_ID + '&pOption=3014').then(function (res) {
                vm.LastBundle = res;
            });
        }

        vm.cutSewProdScanActions = function (pGMT_SEW_PROD_SCAN_ID, act, act_msg, pOption) {
            Dialog.confirm('<h5 style="margin:0px;"> ' + act_msg + ' </h5', act, ['Yes', ' No'])
                 .then(function () {
                     return GarmentsDataService.saveDataByFullUrl({
                         GMT_CUT_PNL_INSPTN_H_ID: pGMT_SEW_PROD_SCAN_ID,
                         pOption: pOption
                     }, '/api/Cutting/CutCutPnlInspect/CutPanelInsActions').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                             new Audio('../../Content/sounds/errorSound.mp3').play();
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $state.go('GmtSewingOutPut', {}, { inherit: false, reload: true });
                             }
                             config.appToastMsg(res.data.PMSG)
                         }

                     })
                 });
        }

        vm.clearAll = function () {
            Dialog.confirm('<h5 style="margin:0px;">Clearing All Ok from List.... </h5', 'Confirmation', ['Yes', ' No'])
                 .then(function () {
                     return GarmentsDataService.saveDataByUrl({
                         GMT_PROD_PLN_CLNDR_ID: vm.GMT_PROD_PLN_CLNDR_ID,
                         pOption : 1002
                     }, '/GmtSewProdScan/SaveGmtSewProdScan').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                             new Audio('../../Content/sounds/errorSound.mp3').play();
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $state.go('GmtSewingOutPut', {}, { inherit: false, reload: true });
                             }
                            
                             config.appToastMsg(res.data.PMSG)
                         }
                     })
                 });
        }


        vm.submitData = function (BARCODE, valid) {
            vm.errors = null;
            if ((BARCODE || '').length < 18 && (BARCODE || '').length > 18 || ! /^\d+$/.test(BARCODE || '')) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.SewOutputScanForm['BARCODE'].$setValidity('invalid_barcode', false);
                $scope.BARCODE = '';
                return;
            }
            var IS_OK = parseInt(BARCODE.substr(BARCODE.length - 4, 1));
            if (!(IS_OK == 0 || IS_OK == 1)) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.SewOutputScanForm['BARCODE'].$setValidity('invalid_barcode', false);
                $scope.BARCODE = '';
                return;
            }

            var PART_ID = parseInt(BARCODE.substr(BARCODE.length - 3));
            var BARCODE = BARCODE.substr(0, 14).trim();

            return GarmentsDataService.saveDataByUrl({
                HAS_REJECT: IS_OK,
                BARCODE: BARCODE,
                GMT_PROD_PLN_CLNDR_ID: vm.GMT_PROD_PLN_CLNDR_ID
            }, '/GmtSewProdScan/SaveGmtSewProdScan').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                    new Audio('../../Content/sounds/errorSound.mp3').play();
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        new Audio('../../Content/sounds/OkSound.mp3').play();
                        $scope.SewOutputScanForm['BARCODE'].$setValidity('invalid_barcode', true);

                        $scope.BARCODE = '';
                         getLastBundleScannedData(res.data.OP_GMT_SEW_PROD_SCAN_ID);
                         getSewProdScanSummeryData(vm.GMT_PROD_PLN_CLNDR_ID);
                         FinSewProdScanHrlyData(vm.GMT_PROD_PLN_CLNDR_ID);
                        $state.go('GmtSewingOutPut', { pOP_GMT_SEW_PROD_SCAN_ID: res.data.OP_GMT_SEW_PROD_SCAN_ID }, { notify: false });

                    } else {
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = res.data.PMSG.substr(9);
                       // $scope.SewOutputScanForm['BARCODE'].$setValidity('invalid_barcode', false);
                        $scope.BARCODE = '';
                    }

                }
            })




        }

        vm.openDefectModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Garments/Gmt/_SewingOutPutDefectRejectModal',
                controller: 'GmtSewingOutputDefectRejectModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    BundleCaradList: function () {
                        return GarmentsDataService.getDataByUrl('/GmtSewProdScan/GetSewProdDefectData?pGMT_PROD_PLN_CLNDR_ID=' + vm.GMT_PROD_PLN_CLNDR_ID);
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                $state.go('GmtSewingOutPut', {}, { inherit: false, reload: true });
            });
        }



        };
})();
////////// End GmtSewingOutPut Controller






