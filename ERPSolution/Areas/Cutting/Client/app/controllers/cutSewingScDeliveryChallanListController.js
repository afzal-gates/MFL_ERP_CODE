////////// Start CutDeliveryChallan Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutSewingScDeliveryChallanListController', ['config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', '$modal', CutSewingScDeliveryChallanListController]);
    function CutSewingScDeliveryChallanListController(config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog, $modal) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.autoFocus= false;

        activate();
        function activate() {
            var promise = [getServiceData(), getSupplierList()];
            return $q.all(promise).then(function () {
                
                vm.showSplash = false;                
            });
        }


        $scope.onFocused = function () {
            if ($stateParams.pGMT_CUT_SC_DLV_CHL_ID) {
                $scope.challan = {};
                $scope.BundleDatas = {
                    datas: []
                };
                $state.go('SewPrtEmbrDelChallanList', {}, { inherit: false });
            } 
        };


        if ($stateParams.pGMT_CUT_SC_DLV_CHL_ID) {
            CuttingDataService.getDataByUrl('/GmtCutBank/getSewingScDeliveryAutoCompl?pGMT_CUT_SC_DLV_CHL_ID=' + $stateParams.pGMT_CUT_SC_DLV_CHL_ID).
            then(function (res) {
                if (res.length > 0) {
                    $scope.challan = res[0];
                    getSewingScDelChallanSumData(res[0].GMT_CUT_SC_DLV_CHL_ID);
                }

            });
        } 

        $scope.sewDelChallanAuto = function (q) {
            return CuttingDataService.getDataByUrl('/GmtCutBank/getSewingScDeliveryAutoCompl?pCHALAN_NO=' + q);
        }

        function getSewingScDelChallanSumData(pGMT_CUT_SC_DLV_CHL_ID) {
            return CuttingDataService.getDataByUrl('/GmtCutBank/getSewingScDeliveryChallan?pGMT_CUT_SC_DLV_CHL_ID=' + pGMT_CUT_SC_DLV_CHL_ID).then(function (res) {
                $scope.BundleDatas = {
                    datas: res,
                    total_bundles: _.sumBy(res, function (o) { return o.TTL_NO_OF_BNDL_SCANNED; }),
                    total_cut_qty: _.sumBy(res, function (o) { return o.TTL_CUTTING_QTY; }),
                    total_qty: _.sumBy(res, function (o) { return o.TTL_FINAL_QTY; }),
                    GMT_CUT_PNL_BNK_LST: res.map(function (o) { return o.GMT_CUT_PNL_BNK_LST; }).join(',')
                };
            });
        }

        vm.onSubmit = function (data) {
            if (!data)
                return;
            $scope.challan = Object.assign({}, data);
            getSewingScDelChallanSumData(data.GMT_CUT_SC_DLV_CHL_ID);
        };



        function getServiceData() {
            return CuttingDataService.getDataByFullUrl('/api/common/LookupListData/142').then(function (res) {

                $scope.DsService = new kendo.data.DataSource({
                    data: res.filter(function (o) { return [702, 703].indexOf(o.LOOKUP_DATA_ID) > -1; })
                });
            });
        }


        function getSupplierList() {
            return CuttingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {

                $scope.DsSup = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        $scope.submitData = function (data, valid) {
            if (!valid) {
                return;
            }


            return CuttingDataService.saveDataByUrl({
                GMT_CUT_SC_DLV_CHL_ID: data.GMT_CUT_SC_DLV_CHL_ID,
                SCM_SUPPLIER_ID: data.SCM_SUPPLIER_ID,
                CHALAN_NO: data.CHALAN_NO,
                IS_TAG: data.IS_TAG,
                LK_BNDL_MVM_RSN_ID: data.LK_BNDL_MVM_RSN_ID,
                NO_OF_BAG: data.NO_OF_BAG,
                GATE_PASS_NO: data.GATE_PASS_NO,
                VEHICLE_NO: data.VEHICLE_NO,
                IS_FINALIZED: 'N',

                GMT_CUT_PNL_BNK_LST: $scope.BundleDatas.GMT_CUT_PNL_BNK_LST,
                pOption: 1000

            }, '/GmtCutBank/SaveScSewingDelChallan').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                 
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            })

        }

        $scope.FinalizeAndDeliver = function (data, valid) {
            if (!valid) {
                return;
            }

            if (parseInt(data.GMT_CUT_SC_DLV_CHL_ID) > 1 && parseInt(data.SCM_SUPPLIER_ID) > 1) {

                Dialog.confirm('<h5 style="margin:0px;">Finalizing and Delivering Challan...</h5', 'Are you sure?', ['Yes', ' No'])
                        .then(function () {
                            return CuttingDataService.saveDataByUrl({
                                GMT_CUT_SC_DLV_CHL_ID: data.GMT_CUT_SC_DLV_CHL_ID,
                                SCM_SUPPLIER_ID: data.SCM_SUPPLIER_ID,
                                CHALAN_NO: data.CHALAN_NO,
                                IS_TAG: 'N',
                                LK_BNDL_MVM_RSN_ID: data.LK_BNDL_MVM_RSN_ID,
                                NO_OF_BAG: data.NO_OF_BAG,
                                GATE_PASS_NO: data.GATE_PASS_NO,
                                VEHICLE_NO: data.VEHICLE_NO,
                                IS_FINALIZED: 'Y',
                                pOption: 1001,
                            }, '/GmtCutBank/SaveScSewingDelChallan').then(function (res) {
                                if (res.success === false) {
                                }
                                else {
                                    res['data'] = angular.fromJson(res.jsonStr);
                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                        $scope.challan['IS_FINALIZED'] = 'Y';
                                    }
                                    config.appToastMsg(res.data.PMSG);
                                }
                            })
                        });


            } else {
                config.appToastMsg('MULTI-002Please Complete Challan Data Properly');
            }


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

        vm.openDeliveryChallanSearch = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_DeliveryChallanSearchModal',
                controller: 'DeliveryChallanSearchModalController',
                size: 'lg',
                windowClass: 'app-modal-window'
            });

            modalInstance.result.then(function (dta) {
                console.log(dta);
            });
        }




        };
})();
////////// End CutPanelInspection Controller






