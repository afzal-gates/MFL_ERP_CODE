////////// Start CutPanelInspection Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutPanelInspectionController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', '$modal', 'GMT_PROD_PLN_CLNDR_ID', CutPanelInspectionController]);
    function CutPanelInspectionController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog, $modal, GMT_PROD_PLN_CLNDR_ID) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.LastBundle = null;
        $scope.BARCODE = '';
        vm.totalBndlPending = 0;


        activate();
        function activate() {
            var promise = [getLastBundleScannedData($stateParams.pGMT_CUT_PNL_INSPTN_H_ID), getCalendarDateList()];
            return $q.all(promise).then(function () { 
                vm.showSplash = false;                
            });
        }

       

        vm.openDefectModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_CutPanelInspectionModal',
                controller: 'CutPanelInspectionModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    BundleCaradList: function () {
                        return CuttingDataService.getDataByUrl('/CutCutPnlInspect/GetBundleListOfRejectedPanel?pGMT_PROD_PLN_CLNDR_ID=' + vm.GMT_PROD_PLN_CLNDR_ID);
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                $state.go('CutPanelInspection', {}, { inherit: false, reload: true });
            });
        }


        function getCalendarDateList() {
            return CuttingDataService.getDataByFullUrl('/api/pln/CapctiClndr/getCurrentProdCalendarList').then(function (res) {
                vm.clndrDateDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function getLastBundleScannedData(pGMT_CUT_PNL_INSPTN_H_ID) {
            if (!pGMT_CUT_PNL_INSPTN_H_ID)
                return;
            return CuttingDataService.getDataByUrl('/CutCutPnlInspect/getLastBundleCardScannedInfo?pGMT_CUT_PNL_INSPTN_H_ID=' + pGMT_CUT_PNL_INSPTN_H_ID).then(function (res) {
                vm.LastBundle = res;
            });
        }

        function getCutPanelInsSummeryData(GMT_PROD_PLN_CLNDR_ID) {
            return CuttingDataService.getDataByUrl('/CutCutPnlInspect/getCutPanelInsSummeryData?pGMT_PROD_PLN_CLNDR_ID=' + GMT_PROD_PLN_CLNDR_ID).then(function (res) {
                vm.dataList = res;
                vm.totalBndlPending = _.sumBy(res, function (o) { return o.TTL_BNDL_PENDING; });
            });
        };
        vm.ClndrListOnBound = function (e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].GMT_PROD_PLN_CLNDR_ID);
                vm.GMT_PROD_PLN_CLNDR_ID = ds[0].GMT_PROD_PLN_CLNDR_ID;
                getCutPanelInsSummeryData(ds[0].GMT_PROD_PLN_CLNDR_ID);
            }
        }

        vm.ClndrListOnChange = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item && item.GMT_PROD_PLN_CLNDR_ID) {
                getCutPanelInsSummeryData(item.GMT_PROD_PLN_CLNDR_ID);
            }
        }

        vm.markPendingAsProduction = function(data) {
            Dialog.confirm('<h5 style="margin:0px;"> Part #: <b> ' + data.GARM_PART_NAME + ' </b> &nbsp;  Color # : <b> ' + data.COLOR_NAME_EN + '</b>&nbsp;  Cutting # : <b> ' + data.CUTNG_NO + '</b>&nbsp;  Pending Bundle # : <b> ' + (data.TTL_BNDL_CUTTING - data.TTL_BNDL_INSPECTED - data.TTL_BNDL_OK) + '</b> </h5', 'Mark as QC Pass?', ['Yes', ' No'])
                 .then(function () {
                     return CuttingDataService.saveDataByUrl({
                         GMT_CUT_INFO_ID: data.GMT_CUT_INFO_ID,
                         RF_GARM_PART_ID: data.RF_GARM_PART_ID,
                         GMT_PROD_PLN_CLNDR_ID: vm.GMT_PROD_PLN_CLNDR_ID

                     }, '/CutCutPnlInspect/MarkAsQcPass').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                             new Audio('../../Content/sounds/errorSound.mp3').play();
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 getCutPanelInsSummeryData(vm.GMT_PROD_PLN_CLNDR_ID);
                             }
                         }
                     })
                 });
        }

        vm.cutPanelInsActions = function (pGMT_CUT_PNL_INSPTN_H_ID, act, act_msg, pOption) {
            Dialog.confirm('<h5 style="margin:0px;"> ' + act_msg + ' </h5', act, ['Yes', ' No'])
                 .then(function () {
                     return CuttingDataService.saveDataByUrl({
                         GMT_CUT_PNL_INSPTN_H_ID: pGMT_CUT_PNL_INSPTN_H_ID,
                         pOption: pOption
                     }, '/CutCutPnlInspect/CutPanelInsActions').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                             new Audio('../../Content/sounds/errorSound.mp3').play();
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $state.go('CutPanelInspection', {}, { inherit: false,reload: true });
                             }
                         }
                     })
                 });
        }


            

        vm.submitData = function (BARCODE, valid) {
            vm.errors = null;
            if ((BARCODE||'').length < 18 && (BARCODE||'').length > 18 || ! /^\d+$/.test(BARCODE||'')) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.CutPanelInspectionForm['BARCODE'].$setValidity('invalid_barcode', false);
                $scope.BARCODE = '';
                return;
            }
            var IS_OK = parseInt(BARCODE.substr(BARCODE.length - 4, 1));
            if (!(IS_OK == 0 || IS_OK == 1)) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.CutPanelInspectionForm['BARCODE'].$setValidity('invalid_barcode', false);
                $scope.BARCODE = '';
                return;
            }
           
            var PART_ID = parseInt(BARCODE.substr(BARCODE.length - 3));
            var BARCODE = BARCODE.substr(0, 14).trim();

            return CuttingDataService.saveDataByUrl({
                HAS_DEFECT: IS_OK,
                BARCODE: BARCODE,
                RF_GARM_PART_ID: PART_ID,
                GMT_PROD_PLN_CLNDR_ID: vm.GMT_PROD_PLN_CLNDR_ID

            }, '/CutCutPnlInspect/SaveBundleCard').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                    new Audio('../../Content/sounds/errorSound.mp3').play();
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        new Audio('../../Content/sounds/OkSound.mp3').play();
                        $scope.CutPanelInspectionForm['BARCODE'].$setValidity('invalid_barcode', true);
                        $scope.BARCODE = '';
                        getLastBundleScannedData(res.data.OP_GMT_CUT_PNL_INSPTN_H_ID);
                        getCutPanelInsSummeryData(vm.GMT_PROD_PLN_CLNDR_ID);
                        $state.go('CutPanelInspection', { pGMT_CUT_PNL_INSPTN_H_ID: res.data.OP_GMT_CUT_PNL_INSPTN_H_ID }, { notify: false });
                    } else {
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = res.data.PMSG.substr(10);
                        $scope.CutPanelInspectionForm['BARCODE'].$setValidity('invalid_barcode', false);
                    }
                    $scope.BARCODE = '';
                }
            })



        }




        };
})();
////////// End CutPanelInspection Controller






