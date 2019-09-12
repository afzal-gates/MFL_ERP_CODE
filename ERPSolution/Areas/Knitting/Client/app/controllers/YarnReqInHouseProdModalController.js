(function () {
    'use strict';
    angular.module('multitex.knitting').controller('YarnReqInHouseProdController', ['$q', 'config', 'KnittingDataService', '$scope', '$modalInstance', 'JobCardList', 'Dialog', YarnReqInHouseProdController]);
    function YarnReqInHouseProdController($q, config, KnittingDataService, $scope, $modalInstance, JobCardList, Dialog) {

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };


        $scope.JobCardList = JobCardList;

        $scope.remove = function (KnitCard, idx, isDialog) {

            if (isDialog) {
                Dialog.confirm('Removing Knit Card ref# ' + KnitCard + ' ...<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                .then(function () {
                    $scope.JobCardList.splice(idx, 1);
                });
            } else {
                   $scope.JobCardList.splice(idx, 1);
            }

        }




        $scope.save = function (dataOri, valid) {
            if (!valid || dataOri.length==0) {
                return;
            }

            

            Dialog.confirm('Do you want to create Yarn Requisition?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
            .then(function () {
                var data = dataOri.map(function (o) {
                    return {
                        KNT_JOB_CRD_D_ID: o.KNT_JOB_CRD_D_ID,
                        KNT_JOB_CRD_H_ID: o.KNT_JOB_CRD_H_ID,
                        NEW_RQSTD_QTY: o.NEW_RQSTD_QTY,
                        RQD_CONE_QTY: o.RQD_CONE_QTY,
                        YARN_ITEM_ID: o.YARN_ITEM_ID,
                        KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID,
                        QTY_MOU_ID: 3,
                        MC_FAB_PROD_ORD_H_ID: o.MC_FAB_PROD_ORD_H_ID
                    }
                });

                return KnittingDataService.saveDataByUrl({}, '/KnitPlan/saveYarnReqInHouseProd?pXML=' + KnittingDataService.xmlStringShort(data)).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $modalInstance.close({ SUCCESS: true });
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                })
            });

        }
    }

})();