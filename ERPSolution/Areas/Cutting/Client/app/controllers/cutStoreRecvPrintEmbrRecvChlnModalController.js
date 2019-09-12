
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutStoreRecvPrintEmbrRecvChlnModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', 'ChallanList', 'Dialog', CutStoreRecvPrintEmbrRecvChlnModalController]);
    function CutStoreRecvPrintEmbrRecvChlnModalController($q, config, CuttingDataService, $modalInstance, $scope, ChallanList, Dialog) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

        $scope.dtFormat = config.appDateFormat;


        $scope.GridDateOpened = [];
        $scope.GridDateOpen = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.GridDateOpened[index] = true;
        };
        $scope.challans = ChallanList;

        $scope.save = function (data, valid) {

            if (!valid) { return; }
                    Dialog.confirm('<h5 style="margin:0px;">Saving Print/Embr Receive Challan...</h5', 'Are You Sure?', ['Yes', ' No'])
                    .then(function () {
                        var data2save = {
                            XML: config.xmlStringShort(
                                            data.map(function (x) {
                                                return {
                                                    GMT_CUT_PRN_RCV_CHL_H_ID: -1,
                                                    SCM_SUPPLIER_ID: x.SCM_SUPPLIER_ID,
                                                    IS_AUTO_CHALLAN_NO: x.IS_AUTO_CHALLAN_NO,
                                                    CHALAN_NO: x.CHALAN_NO,
                                                    GATE_PASS_NO: x.GATE_PASS_NO,
                                                    VEHICLE_NO: x.VEHICLE_NO,
                                                    CHALAN_DT: moment(x.CHALAN_DT).format("DD-MMM-YYYY")
                                                }
                                            })
                                )
                        }
                        return CuttingDataService.saveDataByUrl(data2save, '/GmtCutBank/SavePrintEmbrRecvChallan').then(function (res) {
                            res['data'] = angular.fromJson(res.jsonStr);
                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                $modalInstance.close();
                            }

                        });
                    });
           
        };
   
    }

})();
