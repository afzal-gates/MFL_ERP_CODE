////////// Start CutPanelInspection Controller


    (function () {
        'use strict';
        angular.module('multitex.cutting').directive('checkValidBundleCardForRecut', function (CuttingDataService, $q, config) {
            return {
                require: 'ngModel',
                link: function (scope, element, attrs, ngModel) {
                    ngModel.$asyncValidators.ValidBundleCardForRecut = function (modelValue, viewValue) {

                        if (/^\d+$/.test(viewValue) && viewValue.length == 18) {
                            var IS_OK = parseInt(viewValue.substr(viewValue.length - 4, 1));
                            var PART_ID = parseInt(viewValue.substr(viewValue.length - 3));
                            var BARCODE = viewValue.substr(0, 14).trim();

                            if (IS_OK < 2) {
                                return CuttingDataService.getDataByUrl('/CutCutPnlInspect/CheckScannedBarcode?pBARCODE=' + BARCODE + '&pRF_GARM_PART_ID=' + PART_ID + '&pOption=3000')
                                        .then(
                                         function (res) {
                                             if (res == 0) {
                                                 return $q.reject();
                                             } else if (res > 0) {
                                                 return $q.resolve();
                                             }
                                         }
                                    );
                            } else {
                                return $q.resolve();
                            }
                        } else {
                            return $q.resolve();
                        }

                    };
                }
            };
        });
    })();


(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutPanelRecutController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', '$modal', 'V_GMT_PROD_PLN_CLNDR_ID', CutPanelRecutController]);
    function CutPanelRecutController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog, $modal, V_GMT_PROD_PLN_CLNDR_ID) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        activate();
        function activate() {
            var promise = [getCalendarDateList()];
            return $q.all(promise).then(function () {

                vm.showSplash = false;
            });
        };

        function getCalendarDateList() {
            return CuttingDataService.getDataByFullUrl('/api/pln/CapctiClndr/getCurrentProdCalendarList').then(function (res) {
                vm.clndrDateDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

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



        vm.openDefectModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_CutPanelRecutModal',
                controller: 'CutPanelRecutModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    dataList: function (CuttingDataService) {
                       return CuttingDataService.getDataByUrl('/CutCutPnlInspect/getRecutSummeryDataOfRecut?pGMT_PROD_PLN_CLNDR_ID=' + vm.GMT_PROD_PLN_CLNDR_ID);
                    },
                    V_GMT_PROD_PLN_CLNDR_ID: function () { return vm.GMT_PROD_PLN_CLNDR_ID }
                }
            });

            modalInstance.result.then(function (dta) {
                $state.go('CutPanelRecut', {}, { inherit: false, reload: true });
            });
        }


        vm.markPendingAsProduction = function (data) {
            Dialog.confirm('<h5 style="margin:0px;"> Part #: <b> ' + data.GARM_PART_NAME + ' </b> &nbsp;  Color # : <b> ' + data.COLOR_NAME_EN + '</b>&nbsp;  Cutting # : <b> ' + data.CUTNG_NO + '</b>&nbsp;  Pending Bundle # : <b> ' + (data.TTL_BNDL_HAVING_REJ_PNL - data.TTL_BNDL_HV_RECUT_PROB - data.TTL_BNDL_REPLACED) + '</b> </h5', 'Mark all 100% Recut?', ['Yes', ' No'])
                 .then(function () {
                     return CuttingDataService.saveDataByUrl({
                         GMT_CUT_INFO_ID: data.GMT_CUT_INFO_ID,
                         RF_GARM_PART_ID: data.RF_GARM_PART_ID,
                         GMT_PROD_PLN_CLNDR_ID: vm.GMT_PROD_PLN_CLNDR_ID,
                         pOption: 1008

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


        function getCutPanelInsSummeryData(pGMT_PROD_PLN_CLNDR_ID) {
            return CuttingDataService.getDataByUrl('/CutCutPnlInspect/getRecutSummeryData?pGMT_PROD_PLN_CLNDR_ID=' + pGMT_PROD_PLN_CLNDR_ID).then(function (res) {
                vm.dataList = res;
                vm.totalPendingNotOk = _.sumBy(res, function (o) { return o.TTL_BNDL_HV_RECUT_PROB; });
                vm.totalPendingOK = _.sumBy(res, function (o) { return o.TTL_BNDL_HV_NO_RECUT_PROB; });
            });

        };

        vm.takeAction = function (idx, pGMT_CUT_PNL_RECUT_ID) {
            //// 0=>Clear From List, 1=>Mark As OK, 2=> Mark As Not OK
            var V_GMT_CUT_PNL_RECUT_LST;
            var actions = [
                        {
                            ACT_TYPE: 0, ALERT_MSG: 'Clearing OK Bundle From List...'
                        },
                        {
                            ACT_TYPE: 1, ALERT_MSG: 'Marking As OK...'
                        },
                        {
                            ACT_TYPE: 2, ALERT_MSG: 'Marking As Not OK...'
                        }

               ];

            if ((pGMT_CUT_PNL_RECUT_ID || '').toString().length == 0) {
                V_GMT_CUT_PNL_RECUT_LST = vm.dataList.map(function (o) { return o.OK_CUT_PNL_RECUT_LST; }).join(',');
            } else {
                V_GMT_CUT_PNL_RECUT_LST = (pGMT_CUT_PNL_RECUT_ID || '').toString();
            }

            Dialog.confirm('<h5 style="margin:0px;"> ' + actions[idx].ALERT_MSG + ' </h5', 'Are You Sure?', ['Yes', ' No'])
             .then(function () {
                 return CuttingDataService.saveDataByUrl({
                     GMT_CUT_PNL_RECUT_LST: V_GMT_CUT_PNL_RECUT_LST,
                     ACT_TYPE: actions[idx].ACT_TYPE
                 }, '/CutCutPnlInspect/ExecuteRecutAction').then(function (res) {
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


        vm.submitData = function (BARCODE, valid) {
            if (!vm.GMT_PROD_PLN_CLNDR_ID) {
                return;
            }

            vm.errors = null;
            if ((BARCODE || '').length < 18 && (BARCODE || '').length > 18 || ! /^\d+$/.test(BARCODE || '')) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.CutPanelRecutForm['BARCODE'].$setValidity('invalid_barcode', false);
                $scope.BARCODE = '';
                return;
            }
            var IS_OK = parseInt(BARCODE.substr(BARCODE.length - 4, 1));
            if (!(IS_OK == 0 || IS_OK == 1)) {
                new Audio('../../Content/sounds/errorSound.mp3').play();
                $scope.CutPanelRecutForm['BARCODE'].$setValidity('invalid_barcode', false);
                $scope.BARCODE = '';

                return;
            }

            var PART_ID = parseInt(BARCODE.substr(BARCODE.length - 3));
            var BARCODE = BARCODE.substr(0, 14).trim();

            return CuttingDataService.saveDataByUrl({
                HAS_DEFECT: IS_OK,
                BARCODE: BARCODE,
                RF_GARM_PART_ID: PART_ID,
                GMT_PROD_PLN_CLNDR_ID: vm.GMT_PROD_PLN_CLNDR_ID,
                pOption: 1005

            }, '/CutCutPnlInspect/SaveBundleCard').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                    new Audio('../../Content/sounds/errorSound.mp3').play();
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.errors = null;
                        new Audio('../../Content/sounds/OkSound.mp3').play();
                        $scope.CutPanelRecutForm['BARCODE'].$setValidity('invalid_barcode', true);
                        getCutPanelInsSummeryData(vm.GMT_PROD_PLN_CLNDR_ID);
                    } else {
                        vm.errors = res.data.PMSG.substr(9);
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        $scope.CutPanelRecutForm['BARCODE'].$setValidity('invalid_barcode', false);
                    }
                    $scope.BARCODE = '';
                }
            })

        }




        };
})();
////////// End CutPanelInspection Controller






