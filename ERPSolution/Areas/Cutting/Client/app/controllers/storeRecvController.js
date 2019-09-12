
////////// Start CutPanelInspection Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('StoreRecvController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', '$modal', 'V_GMT_PROD_PLN_CLNDR_ID', StoreRecvController]);
    function StoreRecvController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog, $modal, V_GMT_PROD_PLN_CLNDR_ID) {

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

        vm.RemoveFromScannedList = function (V_GMT_CUT_PNL_BNK_LST) {
            Dialog.confirm('<h5 style="margin:0px;">Deleting...</h5', 'Are you sure?', ['Yes', ' No'])
                 .then(function () {
                     return CuttingDataService.saveDataByUrl({
                         GMT_CUT_PNL_BNK_LST: V_GMT_CUT_PNL_BNK_LST,
                         pOption: 1001,
                     }, '/GmtCutBank/SaveAndOrDelete').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                             new Audio('../../Content/sounds/errorSound.mp3').play();
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $state.go('StoreRecv', {}, { reload: true, inherit: false });
                             }
                         }
                     })
                 });
        }


        vm.onMarkAsStore = function (pGMT_BNDL_CRD_PDATA_LST) {
            Dialog.confirm('<h5 style="margin:0px;">Mark as Store Receive...</h5', 'Are you sure?', ['Yes', ' No'])
                 .then(function () {
                     return CuttingDataService.saveDataByUrl({
                         GMT_CUT_PNL_BNK_LST: pGMT_BNDL_CRD_PDATA_LST,
                         GMT_PROD_PLN_CLNDR_ID: V_GMT_PROD_PLN_CLNDR_ID,
                         pOption: 1005,
                     }, '/GmtCutBank/SaveAndOrDelete').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                             new Audio('../../Content/sounds/errorSound.mp3').play();
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $state.go('StoreRecv', {}, { reload: true, inherit: false });
                             }
                         }
                     })
                 });
        };


        function getStoreRecvSummaryData () {
            return CuttingDataService.getDataByUrl('/GmtCutBank/GetStoreRecvSummaryData?pGMT_PROD_PLN_CLNDR_ID='+V_GMT_PROD_PLN_CLNDR_ID)
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
                pOption: 1000

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
                        $state.go('StoreRecv', { pGMT_CUT_PNL_BNK_ID: res.data.OP_GMT_CUT_PNL_BNK_ID }, { notify: false });
                    } else {
                        vm.errors = res.data.PMSG.substr(9);
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        $scope.CutPanelStoreRecvForm['BARCODE'].$setValidity('invalid_barcode', false);
                    }
                    $scope.BARCODE = '';
                }
            })

        }



        vm.openStoreRecvOpt = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_StoreRecvOptModal',
                controller: function ($scope, $modalInstance, Dialog, $modal, BundleDatas) {

                    console.log(BundleDatas);

                    $scope.opts = [
                        //{ OPT_NO: 1, TEXT_1: 'Store Receive Only', TEXT_2: 'Ready For Sewing Input', cls: 'bg-green-meadow' },
                        //{ OPT_NO: 2, TEXT_1: 'Store Receive Only', TEXT_2: 'Waiting For External Services [Print/Embr/HeatSeal]', cls: 'bg-green-turquoise' },
                        //{ OPT_NO: 6, TEXT_1: 'Store Receive Only', TEXT_2: 'Waiting For External Services [Sewing S/C]', cls: 'blue-hoki' },

                        { OPT_NO: 3, TEXT_1: 'Store Receive With Delivery Challan', TEXT_2: 'Print/Embr/HeatSeal', cls: 'bg-green-seagreen' },
                        { OPT_NO: 4, TEXT_1: 'Store Receive With Sewing Delivery Challan', TEXT_2: 'In-House', cls: 'bg-blue-steel' },
                        { OPT_NO: 5, TEXT_1: 'Store Receive With Sewing Delivery Challan', TEXT_2: 'Sub-Contract', cls: 'bg-blue-madison' }

                    ];
                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }

                    $scope.takeAction = function (opt) {
                        var dataOpt = { GMT_CUT_PNL_BNK_LST: vm.GMT_CUT_PNL_BNK_LST };

                        Dialog.confirm('<h5 style="margin:0px;"><b>' + opt.TEXT_1 + '</b><br/><b>(' + opt.TEXT_2 + ')</b></h5', 'Are You Sure?', ['Yes', ' No'])
                           .then(function () {
                               if (opt.OPT_NO == 1) {
                                   dataOpt['pOption'] = 1002;
                                   dataOpt['LK_CPNL_STATUS_ID'] = 701;// Ready For Sewing Input(W/O tagging)
                                   dataOpt['IS_CLOSED'] = 'Y';
                                   

                               } else if (opt.OPT_NO == 2) {
                                   dataOpt['pOption'] = 1002;
                                   dataOpt['LK_CPNL_STATUS_ID'] = 699; //Waiting For External Service
                                   dataOpt['IS_CLOSED'] = 'Y';
                                   

                               } else if (opt.OPT_NO == 6) {
                                   dataOpt['pOption'] = 1002;
                                   dataOpt['LK_CPNL_STATUS_ID'] = 704; //Waiting For Sewing S/C 
                                   dataOpt['IS_CLOSED'] = 'Y';


                               }

                               else if (opt.OPT_NO == 3) {
                                   var modalInstance = $modal.open({
                                       animation: true,
                                       templateUrl: '/Cutting/Cutting/_DeliveryChallanModal',
                                       controller: 'DeliveryChallanModalController',
                                       size: 'lg',
                                       windowClass: 'app-modal-window',
                                       resolve: {
                                           BundleDatas: function () {
                                               return BundleDatas;
                                           }
                                       }
                                   });

                                   modalInstance.result.then(function (dta) {
                                       $modalInstance.close();
                                   });
                               } else if (opt.OPT_NO == 4) {
                                   var modalInstance = $modal.open({
                                       animation: true,
                                       templateUrl: '/Cutting/Cutting/_SewingDeliveryChallanModal',
                                       controller: 'SewingDeliveryChallanModalController',
                                       size: 'lg',
                                       windowClass: 'app-modal-window',
                                       resolve: {
                                           BundleDatas: function () {
                                               return BundleDatas;
                                           }
                                       }
                                   });

                                   modalInstance.result.then(function (dta) {
                                       $modalInstance.close();
                                   });
                               } else if (opt.OPT_NO == 5) {
                                   var modalInstance = $modal.open({
                                       animation: true,
                                       templateUrl: '/Cutting/Cutting/_SewingScDeliveryChallanModal',
                                       controller: 'SewingScDeliveryChallanModalController',
                                       size: 'lg',
                                       windowClass: 'app-modal-window',
                                       resolve: {
                                           BundleDatas: function () {
                                               return BundleDatas;
                                           }
                                       }
                                   });

                                   modalInstance.result.then(function (dta) {
                                       $modalInstance.close();
                                   });
                               };

                               if (opt.OPT_NO == 1 || opt.OPT_NO == 2 || opt.OPT_NO == 6) {
                                   return CuttingDataService.saveDataByUrl(dataOpt, '/GmtCutBank/SaveAndOrDelete').then(function (res) {
                                       res['data'] = angular.fromJson(res.jsonStr);
                                       if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                           $modalInstance.close();
                                       } 
                                   });
                               };


                           });

      
                    };
                },

                resolve: {
                    BundleDatas: function () {
                        return {
                            datas : vm.summaryData,
                            total_bundles: _.sumBy(vm.summaryData, function (o) { return o.TTL_NO_OF_BNDL_SCANNED; }),
                            total_cut_qty: _.sumBy(vm.summaryData, function (o) { return o.TTL_CUTTING_QTY_CUR; }),
                            total_qty : _.sumBy(vm.summaryData, function (o) { return o.TTL_FINAL_QTY; }),
                            short_qty: _.sumBy(vm.summaryData, function (o) { return o.TTL_SHORT_QTY; }),
                            GMT_CUT_PNL_BNK_LST: vm.summaryData.map(function (o) { return o.GMT_CUT_PNL_BNK_LST; }).join(','),
                            RF_GARM_PART_LST: vm.summaryData.map(function (o) { return o.RF_GARM_PART_LST; }).join(','),

                        }
                    }
                }, 
                size: 'md',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (dta) {
                $state.go('StoreRecv', {}, { reload: true, inherit: false });
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






