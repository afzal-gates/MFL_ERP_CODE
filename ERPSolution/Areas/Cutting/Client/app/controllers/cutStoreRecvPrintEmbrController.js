////////// Start CutPanelInspection Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutStoreRecvPrintEmbrController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', '$modal', CutStoreRecvPrintEmbrController]);
    function CutStoreRecvPrintEmbrController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog, $modal) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        activate();
        function activate() {
            var promise = [getPrintEmbrSummaryData(), getCountData()];
            return $q.all(promise).then(function () {
                
                vm.showSplash = false;                
            });
        }


        function getPrintEmbrSummaryData() {
            return CuttingDataService.getDataByUrl('/GmtCutBank/GetPrintEmbrRecvData').then(function (res) {
                vm.prtEmbrRecvData = res;
                
            });
        };

        function getCountData() {
            return CuttingDataService.getDataByUrl('/GmtCutBank/GetCountDataAtStoreRecvFromPrint').then(function (res) {
                vm.countData = res;
            });
        };


        

        vm.submitData = function (BARCODE, valid) {
            vm.errors = null;
            if ((BARCODE || '').length < 18 && (BARCODE || '').length > 18 || ! /^\d+$/.test(BARCODE || '')) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.StoreRecvAfterPrintEmbrForm['BARCODE'].$setValidity('invalid_barcode', false);
                $scope.BARCODE = '';
                return;
            }
            var IS_OK = parseInt(BARCODE.substr(BARCODE.length - 4, 1));
            if (!(IS_OK == 0 || IS_OK == 1)) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.StoreRecvAfterPrintEmbrForm['BARCODE'].$setValidity('invalid_barcode', false);
                $scope.BARCODE = '';

                return;
            }

            var PART_ID = parseInt(BARCODE.substr(BARCODE.length - 3));
            var BARCODE = BARCODE.substr(0, 14).trim();

            return CuttingDataService.saveDataByUrl({
                GMT_CUT_PRN_RCV_CHL_D_ID: -1,
                BARCODE: BARCODE,
                HAS_REJECT: IS_OK,
                pOption: 1000,
                RF_GARM_PART_ID: PART_ID

            }, '/GmtCutBank/CutPrnRcvChlDsave').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                    new Audio('../../Content/sounds/errorSound.mp3').play();
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        new Audio('../../Content/sounds/OkSound.mp3').play();
                        $scope.StoreRecvAfterPrintEmbrForm['BARCODE'].$setValidity('invalid_barcode', true);
                        $scope.BARCODE = '';
                        getPrintEmbrSummaryData();
                        getCountData();
                    } else {
                        vm.errors = res.data.PMSG.substr(9);
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        $scope.StoreRecvAfterPrintEmbrForm['BARCODE'].$setValidity('invalid_barcode', false);
                        $scope.BARCODE = '';
                    }

                   
                }
            })

        }

        

        vm.openRecvChallan = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_StoreRecvPrintEmbrRecvChlnModal',
                controller: 'CutStoreRecvPrintEmbrRecvChlnModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    ChallanList: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrl('/GmtCutBank/getPrintDeliveryRecvChallan');
                    }
                },

            });

            modalInstance.result.then(function (dta) {
                getPrintEmbrSummaryData();
                getCountData();
            });
        }


        vm.openStoreRecvOpt = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_StoreRecvOptPrintEmbrModal',
                controller: function ($scope, $modalInstance, Dialog, $modal, ReadyToRecv) {
                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                    $scope.ReadyToRecv = ReadyToRecv;
                    $scope.openDeliveryChallanModal = function () {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: '/Cutting/Cutting/_DeliveryChallanModal',
                            controller: 'DeliveryChallanModalController',
                            size: 'lg',
                            windowClass: 'app-modal-window'
                        });

                        modalInstance.result.then(function (dta) {
                            console.log(dta);
                        });
                    }

                    $scope.storeRecvOnly = function (list) {
                        Dialog.confirm('<h3 style="margin:0px;"> Saving data... </h3', 'Store Receive Only', ['Yes', ' No'])
                               .then(function () {
                                   var data2save = {
                                       GMT_CUT_PNL_BNK_LST: list.map(function (o) { return o.GMT_CUT_PNL_BNK_LST; }).join(','),
                                       pOption: 1001
                                   };
                                   return CuttingDataService.saveDataByUrl(data2save, '/GmtCutBank/SavePrintEmbrRecvChallan').then(function (res) {
                                       res['data'] = angular.fromJson(res.jsonStr);
                                       if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                           $modalInstance.close();
                                       }
                                   });
                        });
                    };

                    $scope.deliveryChallan = function (list) {

                        Dialog.confirm('<h3 style="margin:0px;"> Preparing Sewing Delivery Challan... </h3', 'Sewing Delivery Challan', ['Yes', ' No'])
                       .then(function () {
                           var modalInstance = $modal.open({
                               animation: true,
                               templateUrl: '/Cutting/Cutting/_SewingDeliveryChallanModal',
                               controller: 'SewingDeliveryChallanModalController',
                               size: 'lg',
                               windowClass: 'app-modal-window',
                               resolve: {
                                   BundleDatas: function () {
                                       return {
                                           GMT_CUT_PNL_BNK_LST: list.map(function (o) { return o.GMT_CUT_PNL_BNK_LST; }).join(',')
                                       };
                                   }
                               }
                           });

                           modalInstance.result.then(function (dta) {
                               $modalInstance.close();
                           });

                       });
                    };


                    $scope.deliveryChallanSC = function (list) {

                        Dialog.confirm('<h3 style="margin:0px;"> Preparing Sub-Contract Sewing Delivery Challan... </h3', 'Sewing Sub-Contract Delivery Challan', ['Yes', ' No'])
                       .then(function () {
                           var modalInstance = $modal.open({
                               animation: true,
                               templateUrl: '/Cutting/Cutting/_SewingScDeliveryChallanModal',
                               controller: 'SewingScDeliveryChallanModalController',
                               size: 'lg',
                               windowClass: 'app-modal-window',
                               resolve: {
                                   BundleDatas: function () {
                                       return {
                                           GMT_CUT_PNL_BNK_LST: list.map(function (o) { return o.GMT_CUT_PNL_BNK_LST; }).join(',')
                                       };
                                   }
                               }
                           });

                           modalInstance.result.then(function (dta) {
                               $modalInstance.close();
                           });

                       });
                    };








                },
                size: 'lg',
                resolve: {
                    ReadyToRecv: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrl('/GmtCutBank/GetPrintEmbrRecvData?pOption=3006');
                    }
                },
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (dta) {
                getPrintEmbrSummaryData();
                getCountData();
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

          vm.openRejectShortPrintEmbrModal = function () {
              var modalInstance = $modal.open({
                  animation: true,
                  templateUrl: '/Cutting/Cutting/_StoreRecvPrintEmbrModal',
                  controller: 'CutRejectedPrintEmbrModalController',
                  size: 'lg',
                  windowClass: 'app-modal-window',
                  resolve: {
                      BundleCaradList: function (CuttingDataService) {
                          return CuttingDataService.getDataByUrl('/GmtCutBank/getRejectedPrintEmbrData');
                      }
                  }
              });

              modalInstance.result.then(function (dta) {
                  getPrintEmbrSummaryData();
                  getCountData();
              });
          }

        };
})();
////////// End CutPanelInspection Controller






