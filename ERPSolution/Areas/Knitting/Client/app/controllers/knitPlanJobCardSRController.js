(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitPlanJobCardSRController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'JobCardHeader', 'Dialog', 'YarnLotList', '$modal', '$window', KnitPlanJobCardSRController]);
    function KnitPlanJobCardSRController($q, config, KnittingDataService, $stateParams, $state, $scope, JobCardHeader, Dialog, YarnLotList, $modal, $window) {


        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getJobCardList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        vm.IS_EDIT_MODE = false;

        if ($state.current.data.LK_COL_TYPE_ID_LST == '360') {
            vm.knitNaviYd = true;
        } else {
            vm.knitNaviYd = false;
        }

        function getKpData(pMC_FAB_PROD_ORD_D_ID, pRF_FAB_PROD_CAT_ID) {
            return KnittingDataService.getDataByUrl(
                '/KnitPlan/getKnitPlanHeaderDataCollarCuff?pMC_FAB_PROD_ORD_D_ID=' + pMC_FAB_PROD_ORD_D_ID + '&pRF_FAB_PROD_CAT_ID=' + pRF_FAB_PROD_CAT_ID).then(function (res) {
                vm.IS_EDIT_MODE = false;
                $scope.data = res;
            })
        };

        vm.editData = function (item) {
            $scope.data = item;
            vm.IS_EDIT_MODE = _.every(item.items, function (o) {
                return o.KNT_YRN_STR_REQ_D_ID > 0
            });
        };

        vm.createStoreRequisition = function (data, isSUBMIT, IS_EDIT_MODE) {

            if (data.length == 0) {
                return;
            }

            if (isSUBMIT) {

                Dialog.confirm('Do you really want to submit Yarn Requisition?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var xml = config.xmlStringShort(data.map(function (dataOri) {
                         return {
                             KNT_PLAN_H_ID: dataOri.KNT_PLAN_H_ID,

                             XML: config.xmlStringShortNoTag(dataOri.items.map(function (o) {
                                 return {
                                     YARN_ITEM_ID: o.YARN_ITEM_ID,
                                     LK_YFAB_PART_ID: o.LK_YFAB_PART_ID || '',
                                     RF_BRAND_ID: o.RF_BRAND_ID,
                                     KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID,
                                     RQD_YRN_QTY: o.RQD_YRN_QTY,
                                     KNT_PLAN_D_ID: o.KNT_PLAN_D_ID,
                                     RQSTD_NEW: o.RQSTD_NEW,
                                     KNT_YRN_STR_REQ_D_ID: o.KNT_YRN_STR_REQ_D_ID > 0 ? o.KNT_YRN_STR_REQ_D_ID : -1,
                                     RQD_CONE_QTY: o.RQD_CONE_QTY,
                                     IS_SOLID: o.IS_SOLID
                                 }
                             }))
                         }

                     }));

                     return KnittingDataService.saveDataByUrl({ XML: xml, KNT_YRN_STR_REQ_H_ID: ($stateParams.pKNT_YRN_STR_REQ_H_ID || -1), IS_EDIT_MODE: 'N', IS_SUBMIT: 'Y' }, '/KnitPlan/CreateStoreRequisitionSR').then(function (res) {
                         res['data'] = angular.fromJson(res.jsonStr);
                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             getJobCardList(parseInt(res.data.OP_KNT_YRN_STR_REQ_H_ID));
                             $state.go('KnitPlanJobCardSR', { pKNT_YRN_STR_REQ_H_ID: parseInt(res.data.OP_KNT_YRN_STR_REQ_H_ID) }, { notify: false });
                         }
                         config.appToastMsg(res.data.PMSG);
                     });
                 });

            } else {
                var xml = config.xmlStringShort(data.map(function (dataOri) {
                    return {
                        KNT_PLAN_H_ID: dataOri.KNT_PLAN_H_ID,

                        XML: config.xmlStringShortNoTag(dataOri.items.map(function (o) {
                            return {
                                YARN_ITEM_ID: o.YARN_ITEM_ID,
                                LK_YFAB_PART_ID: o.LK_YFAB_PART_ID || '',
                                RF_BRAND_ID: o.RF_BRAND_ID,
                                KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID,
                                RQD_YRN_QTY: o.RQD_YRN_QTY,
                                KNT_PLAN_D_ID: o.KNT_PLAN_D_ID,
                                RQSTD_NEW: o.RQSTD_NEW,
                                KNT_YRN_STR_REQ_D_ID: o.KNT_YRN_STR_REQ_D_ID > 0 ? o.KNT_YRN_STR_REQ_D_ID : -1,
                                RQD_CONE_QTY: o.RQD_CONE_QTY,
                                IS_SOLID: o.IS_SOLID
                            }
                        }))
                    }

                }));

                return KnittingDataService.saveDataByUrl({ XML: xml, KNT_YRN_STR_REQ_H_ID: ($stateParams.pKNT_YRN_STR_REQ_H_ID || -1), IS_EDIT_MODE: (IS_EDIT_MODE || 'N'), IS_SUBMIT: 'N' }, '/KnitPlan/CreateStoreRequisitionSR').then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $state.go('KnitPlanJobCardSR', { pKNT_YRN_STR_REQ_H_ID: parseInt(res.data.OP_KNT_YRN_STR_REQ_H_ID) }, { notify: false });
                        $stateParams.pKNT_YRN_STR_REQ_H_ID = parseInt(res.data.OP_KNT_YRN_STR_REQ_H_ID);
                        getJobCardList(parseInt(res.data.OP_KNT_YRN_STR_REQ_H_ID));

                    }
                    config.appToastMsg(res.data.PMSG);
                });
            }



        };
        function getJobCardList(pKNT_YRN_STR_REQ_H_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getJobCardListCollarCuff?pKNT_SC_PRG_ISS_ID=' + (pKNT_YRN_STR_REQ_H_ID || $stateParams.pKNT_YRN_STR_REQ_H_ID) + '&pOption=3008').then(function (res) {
                vm.jobCardList = res;
                if (res.length > 0) {
                    vm.YARN_REQ = {
                        STR_REQ_NO: res[0].STR_REQ_NO,
                        LK_REQ_STATUS_ID: res[0].LK_REQ_STATUS_ID
                    }
                }
            });
        }


        vm.submitData = function (dataOri, valid1) {

            if (!valid1) {
                return;
            }

            var data = {
                KNT_PLAN_H_ID: dataOri.KNT_PLAN_H_ID,

                REMARKS: dataOri.REMARKS,

                items: dataOri.items,

                BYR_ACC_NAME_EN: dataOri.BYR_ACC_NAME_EN,
                WORK_STYLE_NO: dataOri.WORK_STYLE_NO,
                ORDER_NO_LIST: dataOri.ORDER_NO_LIST,
                FAB_TYPE_NAME: dataOri.FAB_TYPE_NAME,
                COLOR_NAME_EN: dataOri.COLOR_NAME_EN,
                YRN_DETAILS: dataOri.YRN_DETAILS
            }

            if (!_.some(vm.jobCardList, function (o) {
                return o.KNT_PLAN_H_ID == data.KNT_PLAN_H_ID;
            })) {
                vm.jobCardList.push(data);
            }

        }


        vm.updateData = function (dataOri, valid1) {
            if (!valid1) {
                return;
            }
            var data = [{
                KNT_PLAN_H_ID: dataOri.KNT_PLAN_H_ID,
                REMARKS: dataOri.REMARKS,
                items: dataOri.items,
            }];

            vm.createStoreRequisition(data, false, 'Y');
        }
        vm.unAssignQty = function (itm) {
            vm.jobCardList.splice(vm.jobCardList.indexOf(itm), 1);
        }
        vm.openExColourListModal = function (pMC_FAB_PROD_ORD_H_ID) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ExColourListModal.html',
                controller: 'JobCardDashboardModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    Params: function () {
                        return {
                            pMC_FAB_PROD_ORD_H_ID: pMC_FAB_PROD_ORD_H_ID,
                            pRF_FAB_PROD_CAT_ID: 1,
                            IS_REQUISITION: true,
                            DEFAULT_CAT_ID: 1,
                            RF_FAB_PROD_CAT_ID_LST: '1,3',
                            LK_COL_TYPE_ID_LST: ($state.current.data.LK_COL_TYPE_ID_LST || '358,361,407,432')
                        }
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data.KNT_PLAN_H_ID) {
                    getKpData(data.MC_FAB_PROD_ORD_D_ID, data.RF_FAB_PROD_CAT_ID);
                }
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        }



    }

})();