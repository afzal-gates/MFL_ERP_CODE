
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('SewingDeliveryChallanModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', 'BundleDatas', 'Dialog', SewingDeliveryChallanModalController]);
    function SewingDeliveryChallanModalController($q, config, CuttingDataService, $modalInstance, $scope, BundleDatas, Dialog) {

        $scope.BundleDatas = BundleDatas;
        $scope.GMT_CUT_SEW_DLV_CHL_ID = null;
        getLineData();       
       
        $scope.challan = {
            GMT_CUT_SEW_DLV_CHL_ID: -1, HR_PROD_LINE_ID: '', NO_OF_BAG: 1, IS_TAG: 'N'
        };


        

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

        $scope.onClose = function () {
            $modalInstance.close();
        };


        function getSewingDeliveryChallan(pGMT_CUT_SEW_DLV_CHL_ID) {
            return CuttingDataService.getDataByUrl('/GmtCutBank/GetSewingDelChallanData?pOption=3001&pGMT_CUT_SEW_DLV_CHL_ID=' + pGMT_CUT_SEW_DLV_CHL_ID)
               .then(function (res) {
                   $scope.challan = res;
               });
        }
            

        function getLineData() {
            return CuttingDataService.getDataByFullUrl('/api/common/GetSewingLineData').then(function (res) {
                console.log(res);
                $scope.DsLine = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        $scope.submitData = function (data, valid) {
            if (!valid) {
                return;
            }

            
            return CuttingDataService.saveDataByUrl({
                GMT_CUT_SEW_DLV_CHL_ID: data.GMT_CUT_SEW_DLV_CHL_ID,
                HR_PROD_LINE_ID: data.HR_PROD_LINE_ID,
                IS_TAG: data.IS_TAG,
                NO_OF_BAG: data.NO_OF_BAG,
                IS_FINALIZED: 'N',
                GMT_CUT_PNL_BNK_LST: BundleDatas.GMT_CUT_PNL_BNK_LST,
                pOption: 1000

            }, '/GmtCutBank/SaveSewingDelChallan').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        
                        $scope.GMT_CUT_SEW_DLV_CHL_ID = res.data.OP_GMT_CUT_SEW_DLV_CHL_ID;

                        getSewingDeliveryChallan(res.data.OP_GMT_CUT_SEW_DLV_CHL_ID);
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            })

        }

        $scope.printDelivChallan = function () {

            var url = '/api/Cutting/CutReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');
            $scope.challan.REPORT_CODE = 'RPT-4514';

            for (var i in $scope.challan) {
                if ($scope.challan.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = $scope.challan[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);

        };

        $scope.FinalizeAndDeliver = function (data, valid) {
            if (!valid) {
                return;
            }

            if (parseInt(data.GMT_CUT_SEW_DLV_CHL_ID) > 1 && parseInt(data.HR_PROD_LINE_ID) > 0) {

                 Dialog.confirm('<h5 style="margin:0px;">Finalizing and Delivering Challan...</h5', 'Are you sure?', ['Yes', ' No'])
                         .then(function () {
                             return CuttingDataService.saveDataByUrl({
                                 GMT_CUT_SEW_DLV_CHL_ID: data.GMT_CUT_SEW_DLV_CHL_ID,
                                 HR_PROD_LINE_ID: data.HR_PROD_LINE_ID,
                                 IS_TAG: data.IS_TAG,
                                 NO_OF_BAG: data.NO_OF_BAG,
                                 IS_FINALIZED: 'Y',
                                 GMT_CUT_PNL_BNK_LST: BundleDatas.GMT_CUT_PNL_BNK_LST,
                                 pOption: 1001
                             }, '/GmtCutBank/SaveSewingDelChallan').then(function (res) {
                                 if (res.success === false) {
                                 }
                                 else {
                                     res['data'] = angular.fromJson(res.jsonStr);
                                     if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                         getSewingDeliveryChallan($scope.GMT_CUT_SEW_DLV_CHL_ID);
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
