
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutBundleCardAmendModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', '$state', 'BundleCaradList', CutBundleCardAmendModalController]);
    function CutBundleCardAmendModalController($q, config, CuttingDataService, $modalInstance, $scope, $state, BundleCaradList) {

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
        $scope.SL = 1;
        $scope.isSelected = false;

        $scope.MAX_SL = _.maxBy(BundleCaradList, function (o) { return o.SL; }).SL;
        $scope.BundleCaradList = BundleCaradList;
        $scope.CurrentBundle = $scope.BundleCaradList[0];

        function setCurrentBundle() {
            var idx = $scope.BundleCaradList.findIndex(function (o) { return o.SL === $scope.SL });
            if (idx > -1) {                
                $scope.CurrentBundle = $scope.BundleCaradList[idx];
            }
        };

        $scope.setSelected = function (ctrl, isDisabled) {
            
            $scope.isSelected = true;

            $scope.CurrentBundle.IS_SHRT_FRM_CUTNG = false;
            $scope.CurrentBundle.IS_SHRT_FRM_PRN_EMB = false;
            $scope.CurrentBundle.IS_SHRT_DTO_LOST = false;
            $scope.CurrentBundle.IS_SRPL_DTO_CUT_BAL = false;
            $scope.CurrentBundle.IS_SRPL_DTO_CUT_PNL_RPL = false;
            $scope.CurrentBundle.IS_SRPL_DTO_PRN_EMB_BAL = false;

            if (isDisabled == 'Y') {
                $scope.isSelected = false;
            }
            else {
                
                if (ctrl == 'SHRT_FRM_CUTNG')
                    $scope.CurrentBundle.IS_SHRT_FRM_CUTNG = true;
                else if (ctrl == 'SHRT_FRM_PRN_EMB')
                    $scope.CurrentBundle.IS_SHRT_FRM_PRN_EMB = true;
                else if (ctrl == 'SHRT_DTO_LOST')
                    $scope.CurrentBundle.IS_SHRT_DTO_LOST = true;
                else if (ctrl == 'SRPL_DTO_CUT_BAL')
                    $scope.CurrentBundle.IS_SRPL_DTO_CUT_BAL = true;
                else if (ctrl == 'SRPL_DTO_CUT_PNL_RPL')
                    $scope.CurrentBundle.IS_SRPL_DTO_CUT_PNL_RPL = true;
                else if (ctrl == 'SRPL_DTO_PRN_EMB_BAL')
                    $scope.CurrentBundle.IS_SRPL_DTO_PRN_EMB_BAL = true;
            }
        };

        $scope.addOne = function (ctrl, val) {

            $scope.CurrentBundle.IS_DATA_ITEM_CHANGED = 'Y';

            if (ctrl == 'SHRT_FRM_CUTNG' && $scope.CurrentBundle.IS_SHRT_FRM_CUTNG) 
                $scope.CurrentBundle.SHRT_FRM_CUTNG = $scope.CurrentBundle.SHRT_FRM_CUTNG + val;                            
            else if (ctrl == 'SHRT_FRM_PRN_EMB' && $scope.CurrentBundle.IS_SHRT_FRM_PRN_EMB)
                $scope.CurrentBundle.SHRT_FRM_PRN_EMB = $scope.CurrentBundle.SHRT_FRM_PRN_EMB + val;
            else if (ctrl == 'SHRT_DTO_LOST' && $scope.CurrentBundle.IS_SHRT_DTO_LOST)
                $scope.CurrentBundle.SHRT_DTO_LOST = $scope.CurrentBundle.SHRT_DTO_LOST + val;
            else if (ctrl == 'SRPL_DTO_CUT_BAL' && $scope.CurrentBundle.IS_SRPL_DTO_CUT_BAL)
                $scope.CurrentBundle.SRPL_DTO_CUT_BAL = $scope.CurrentBundle.SRPL_DTO_CUT_BAL + val;
            else if (ctrl == 'SRPL_DTO_CUT_PNL_RPL' && $scope.CurrentBundle.IS_SRPL_DTO_CUT_PNL_RPL)
                $scope.CurrentBundle.SRPL_DTO_CUT_PNL_RPL = $scope.CurrentBundle.SRPL_DTO_CUT_PNL_RPL + val;
            else if (ctrl == 'SRPL_DTO_PRN_EMB_BAL' && $scope.CurrentBundle.IS_SRPL_DTO_PRN_EMB_BAL)
                $scope.CurrentBundle.SRPL_DTO_PRN_EMB_BAL = $scope.CurrentBundle.SRPL_DTO_PRN_EMB_BAL + val;
        }

        $scope.prev = function () {
            if ($scope.SL < 2) { return; }
            $scope.SL--;
            setCurrentBundle();
        }
        $scope.next = function () {
            if ($scope.SL > $scope.MAX_SL) { return; }
            $scope.SL++;
            setCurrentBundle();
        }

        $scope.shortCutInput = function (val) {

            //console.log(($scope.CurrentBundle.SHRT_FRM_PRN_EMB + $scope.CurrentBundle.SHRT_DTO_LOST) + (($scope.CurrentBundle.SHRT_FRM_CUTNG + val) * 1));
            
            if ($scope.CurrentBundle.IS_SHRT_FRM_CUTNG)                
                if (($scope.CurrentBundle.SHRT_FRM_PRN_EMB + $scope.CurrentBundle.SHRT_DTO_LOST) + (($scope.CurrentBundle.SHRT_FRM_CUTNG + val) * 1) > $scope.CurrentBundle.QTY_IN_BNDL)
                    $scope.CurrentBundle.SHRT_FRM_CUTNG = $scope.CurrentBundle.SHRT_FRM_CUTNG;
                else {
                    $scope.CurrentBundle.SHRT_FRM_CUTNG = ($scope.CurrentBundle.SHRT_FRM_CUTNG + val) * 1;
                    $scope.CurrentBundle.IS_DATA_ITEM_CHANGED = 'Y';
                }
            else if ($scope.CurrentBundle.IS_SHRT_FRM_PRN_EMB)
                if (($scope.CurrentBundle.SHRT_FRM_CUTNG + $scope.CurrentBundle.SHRT_DTO_LOST) + (($scope.CurrentBundle.SHRT_FRM_PRN_EMB + val) * 1) > $scope.CurrentBundle.QTY_IN_BNDL)
                    $scope.CurrentBundle.SHRT_FRM_PRN_EMB = $scope.CurrentBundle.SHRT_FRM_PRN_EMB;
                else {
                    $scope.CurrentBundle.SHRT_FRM_PRN_EMB = ($scope.CurrentBundle.SHRT_FRM_PRN_EMB + val) * 1;
                    $scope.CurrentBundle.IS_DATA_ITEM_CHANGED = 'Y';
                }
            else if ($scope.CurrentBundle.IS_SHRT_DTO_LOST)
                if (($scope.CurrentBundle.SHRT_FRM_CUTNG + $scope.CurrentBundle.SHRT_FRM_PRN_EMB) + (($scope.CurrentBundle.SHRT_DTO_LOST + val) * 1) > $scope.CurrentBundle.QTY_IN_BNDL)
                    $scope.CurrentBundle.SHRT_DTO_LOST = $scope.CurrentBundle.SHRT_DTO_LOST;
                else {
                    $scope.CurrentBundle.SHRT_DTO_LOST = ($scope.CurrentBundle.SHRT_DTO_LOST + val) * 1;
                    $scope.CurrentBundle.IS_DATA_ITEM_CHANGED = 'Y';
                }
            else if ($scope.CurrentBundle.IS_SRPL_DTO_CUT_BAL)
                if (($scope.CurrentBundle.SRPL_DTO_CUT_BAL + val) * 1 > 999)
                    $scope.CurrentBundle.SRPL_DTO_CUT_BAL = $scope.CurrentBundle.SRPL_DTO_CUT_BAL;
                else {
                    $scope.CurrentBundle.SRPL_DTO_CUT_BAL = ($scope.CurrentBundle.SRPL_DTO_CUT_BAL + val) * 1;
                    $scope.CurrentBundle.IS_DATA_ITEM_CHANGED = 'Y';
                }
            else if ($scope.CurrentBundle.IS_SRPL_DTO_CUT_PNL_RPL)
                if (($scope.CurrentBundle.SRPL_DTO_CUT_PNL_RPL + val) * 1 > 999)
                    $scope.CurrentBundle.SRPL_DTO_CUT_PNL_RPL = $scope.CurrentBundle.SRPL_DTO_CUT_PNL_RPL;
                else {
                    $scope.CurrentBundle.SRPL_DTO_CUT_PNL_RPL = ($scope.CurrentBundle.SRPL_DTO_CUT_PNL_RPL + val) * 1;
                    $scope.CurrentBundle.IS_DATA_ITEM_CHANGED = 'Y';
                }
            else if ($scope.CurrentBundle.IS_SRPL_DTO_PRN_EMB_BAL)
                if (($scope.CurrentBundle.SRPL_DTO_PRN_EMB_BAL + val) * 1 > 999)
                    $scope.CurrentBundle.SRPL_DTO_PRN_EMB_BAL = $scope.CurrentBundle.SRPL_DTO_PRN_EMB_BAL;
                else {
                    $scope.CurrentBundle.SRPL_DTO_PRN_EMB_BAL = ($scope.CurrentBundle.SRPL_DTO_PRN_EMB_BAL + val) * 1;
                    $scope.CurrentBundle.IS_DATA_ITEM_CHANGED = 'Y';
                }
        }
            
        $scope.onTrimDigit = function () {
           
            $scope.CurrentBundle.IS_DATA_ITEM_CHANGED = 'Y';

            if ($scope.CurrentBundle.IS_SHRT_FRM_CUTNG)
                $scope.CurrentBundle.SHRT_FRM_CUTNG = parseInt($scope.CurrentBundle.SHRT_FRM_CUTNG.toString().substring(0, ($scope.CurrentBundle.SHRT_FRM_CUTNG.toString().length - 1)) || 0);
            else if ($scope.CurrentBundle.IS_SHRT_FRM_PRN_EMB)
                $scope.CurrentBundle.SHRT_FRM_PRN_EMB = parseInt($scope.CurrentBundle.SHRT_FRM_PRN_EMB.toString().substring(0, ($scope.CurrentBundle.SHRT_FRM_PRN_EMB.toString().length - 1)) || 0);
            else if ($scope.CurrentBundle.IS_SHRT_DTO_LOST)
                $scope.CurrentBundle.SHRT_DTO_LOST = parseInt($scope.CurrentBundle.SHRT_DTO_LOST.toString().substring(0, ($scope.CurrentBundle.SHRT_DTO_LOST.toString().length - 1)) || 0);

            else if ($scope.CurrentBundle.IS_SRPL_DTO_CUT_BAL)
                $scope.CurrentBundle.SRPL_DTO_CUT_BAL = parseInt($scope.CurrentBundle.SRPL_DTO_CUT_BAL.toString().substring(0, ($scope.CurrentBundle.SRPL_DTO_CUT_BAL.toString().length - 1)) || 0);
            else if ($scope.CurrentBundle.IS_SRPL_DTO_CUT_PNL_RPL)
                $scope.CurrentBundle.SRPL_DTO_CUT_PNL_RPL = parseInt($scope.CurrentBundle.SRPL_DTO_CUT_PNL_RPL.toString().substring(0, ($scope.CurrentBundle.SRPL_DTO_CUT_PNL_RPL.toString().length - 1)) || 0);
            else if ($scope.CurrentBundle.IS_SRPL_DTO_PRN_EMB_BAL)
                $scope.CurrentBundle.SRPL_DTO_PRN_EMB_BAL = parseInt($scope.CurrentBundle.SRPL_DTO_PRN_EMB_BAL.toString().substring(0, ($scope.CurrentBundle.SRPL_DTO_PRN_EMB_BAL.toString().length - 1)) || 0);
        };


        $scope.submitData = function () {
            var submitData = {};

            submitData['BNDL_AMND_XML'] = CuttingDataService.xmlStringShort($scope.BundleCaradList.map(function (ob) {
                if (ob.IS_DATA_ITEM_CHANGED == 'Y') {
                    return ob;
                }
            }));

            console.log($scope.BundleCaradList);
            console.log(submitData);
            //return;

            return CuttingDataService.saveDataByUrl(submitData, '/LayChart/FinalizeBundleCardAmnd').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;                    
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {                        
                        $modalInstance.close();
                        $state.go('CutBundleCardAmend', {}, { inherit: false, reload: true });
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });
        }

    }

})();
