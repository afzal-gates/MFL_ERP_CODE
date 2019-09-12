(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitCardUnAssignQtyModalController', ['$scope', 'KnittingDataService', '$modalInstance', '$q', 'JobCard', 'config', KnitCardUnAssignQtyModalController]);
    function KnitCardUnAssignQtyModalController($scope, KnittingDataService, $modalInstance, $q, JobCard, config) {

        $scope.form = JobCard;
        $scope.form['UN_ASIGN_QTY_ORI'] = (JobCard.ASIGN_QTY - JobCard.PROD_QTY - JobCard.UN_ASIGN_QTY);
        $scope.form['UN_ASIGN_QTY_NEW'] = (JobCard.ASIGN_QTY - JobCard.PROD_QTY - JobCard.UN_ASIGN_QTY);

        getYarnDetailsData();
        getChallanListByJobCard();

        setTimeout(function () {
            calculateUnAssignedQty($scope.form.details, $scope.form.UN_ASIGN_QTY_NEW);
        })


        $scope.$watch('form.UN_ASIGN_QTY_NEW', function (newVal, oldVal) {
            if (newVal && newVal !== oldVal) {
                calculateUnAssignedQty($scope.form.details, parseFloat(newVal));
            }
        });

        function calculateUnAssignedQty(data, mainQty) {
            angular.forEach(data, function (val) {
                console.log(val);
                val['UN_ASIGN_QTY'] = val.RQD_YRN_QTY_JC > 0 ? parseFloat(((val.RATIO_QTY / 100) * mainQty).toFixed(2)) : 0;
            });
        };

        function getYarnDetailsData() {
            KnittingDataService.getDataByUrl('/KnitPlan/getYarnDetailsDataByKp?pKNT_PLAN_H_ID=' + JobCard.KNT_PLAN_H_ID + '&pKNT_JOB_CRD_H_ID=' + (JobCard.KNT_JOB_CRD_H_ID || null))
            .then(function (res) {
                $scope.YarnDetails = res;
            });
        };


        function getChallanListByJobCard() {

            if (JobCard.RF_FAB_PROD_CAT_ID == 1 || JobCard.RF_FAB_PROD_CAT_ID == 3) {
                return KnittingDataService.getDataByUrl('/YarnIssue/getRequisitionDataByJobCardH?pKNT_PLAN_H_ID=' + (JobCard.KNT_PLAN_H_ID || null))
                .then(function (res) {
                    $scope.challans = res;
                });
            } else {
             return KnittingDataService.getDataByUrl('/YarnIssue/getRequisitionDataByJobCardH?pKNT_JOB_CRD_H_ID=' + (JobCard.KNT_JOB_CRD_H_ID || null))
                .then(function (res) {
                    $scope.challans = res;
                });
            }

        };



        $scope.deleteReq = function (data) {
            KnittingDataService.saveDataByUrl({ KNT_YRN_STR_REQ_H_ID: data.KNT_YRN_STR_REQ_H_ID }, '/KnitPlan/deleteStoreReq').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getChallanListByJobCard();
                        getYarnDetailsData();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });
        };

        $scope.save = function (data, isValid) {
            if (!isValid) {
                return;
            }

            var data2save = Object.assign({}, data);
            data2save['XML'] = config.xmlStringShort(data2save.details.map(function (o) {
                return {
                    KNT_PLAN_D_ID: o.KNT_PLAN_D_ID,
                    KNT_JOB_CRD_D_ID: o.KNT_JOB_CRD_D_ID,
                    UN_ASIGN_QTY: o.UN_ASIGN_QTY
                };
            }));
            data2save['XML_ISSUE_RETURN'] = config.xmlStringShort(
                    $scope.challans.map(function (o) {
                        return {
                            KNT_YRN_ISS_H_ID: o.KNT_YRN_ISS_H_ID,
                            UNASSIGN_TYPE: o.UNASSIGN_TYPE,
                            DETAILS: config.xmlStringShortNoTag(o.yarn_list.map(function (x) {
                                return {
                                    YARN_ITEM_ID: x.YARN_ITEM_ID,
                                    KNT_YRN_LOT_ID: x.KNT_YRN_LOT_ID,
                                    RET_QTY: x.RET_QTY,
                                    RET_ADV: ((x.ISSUE_QTY_JC - x.REQ_DN_YRN_QTY_JC) < 0 || x.ISSUE_RET_QTY > 0) ? 0 : (x.ISSUE_QTY_JC - x.REQ_DN_YRN_QTY_JC)
                                }
                            }))
                        }

                    })
            );


            KnittingDataService.saveDataByUrl(data2save, '/KnitPlan/SaveUnassignJobCardData').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $modalInstance.close({
                            success: true
                        });
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });

            //if (data['UN_ASIGN_QTY'] > (JobCard.ASIGN_QTY - JobCard.REQ_YRN_QTY_DONE)) {

            //    alert("You can un assign maximum " + (JobCard.ASIGN_QTY - JobCard.REQ_YRN_QTY_DONE) + "kg because yarn requisition already done againest it.");
            //}
            //else {
            //    KnittingDataService.saveDataByUrl(data, '/KnitPlan/SaveUnassignJobCardData').then(function (res) {
            //        if (res.success === false) {
            //        }
            //        else {
            //            res['data'] = angular.fromJson(res.jsonStr);

            //            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
            //                $modalInstance.close({
            //                    success: true
            //                });
            //            }
            //            config.appToastMsg(res.data.PMSG);
            //        }
            //    });
            //}
        };

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

    };


})();