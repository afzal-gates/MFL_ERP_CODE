
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('SewingScDeliveryChallanModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', 'BundleDatas', 'Dialog', SewingScDeliveryChallanModalController]);
    function SewingScDeliveryChallanModalController($q, config, CuttingDataService, $modalInstance, $scope, BundleDatas, Dialog) {



        $scope.BundleDatas = BundleDatas;
        $scope.GMT_CUT_SC_DLV_CHL_ID = null;

        getServiceData();
        getSupplierList();

        $scope.challan = {
            GMT_CUT_SC_DLV_CHL_ID: -1, LK_BNDL_MVM_RSN_ID: '', SCM_SUPPLIER_ID: '', IS_TAG: 'N'
        };


        

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

        $scope.onClose = function () {
            $modalInstance.close();
        };


        function getSewingScDelChallan(pGMT_CUT_SC_DLV_CHL_ID) {
            return CuttingDataService.getDataByUrl('/GmtCutBank/GetScSewingDelChallanData?pOption=3001&pGMT_CUT_SEW_DLV_CHL_ID=' + pGMT_CUT_SC_DLV_CHL_ID)
               .then(function (res) {
                   $scope.challan = res;
               });
        }
            
        function getServiceData() {
            return CuttingDataService.getDataByFullUrl('/api/common/LookupListData/142').then(function (res) {
                
                $scope.DsService = new kendo.data.DataSource({
                    data: res.filter(function(o){ return  [702,703].indexOf(o.LOOKUP_DATA_ID) > -1;})
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

                GMT_CUT_PNL_BNK_LST: BundleDatas.GMT_CUT_PNL_BNK_LST,
                pOption: 1000

            }, '/GmtCutBank/SaveScSewingDelChallan').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {   
                        $scope.GMT_CUT_SC_DLV_CHL_ID = res.data.OP_GMT_CUT_SC_DLV_CHL_ID;
                        getSewingScDelChallan(res.data.OP_GMT_CUT_SC_DLV_CHL_ID);
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
                                 GMT_CUT_SC_DLV_CHL_ID: $scope.GMT_CUT_SC_DLV_CHL_ID,
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
                                         getSewingScDelChallan($scope.GMT_CUT_SC_DLV_CHL_ID);
                                     }
                                     config.appToastMsg(res.data.PMSG);
                                 }
                             })
                 });


            } else {
                config.appToastMsg('MULTI-002Please Complete Challan Data Properly');
            }


        }

    }
})();
