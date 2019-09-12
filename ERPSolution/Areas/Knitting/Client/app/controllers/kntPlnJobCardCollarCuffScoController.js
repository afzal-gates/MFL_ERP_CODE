﻿(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntPlnJobCardCollarCuffScoController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', KntPlnJobCardCollarCuffScoController]);
    function KntPlnJobCardCollarCuffScoController($q, config, KnittingDataService, $stateParams, $state, $scope, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;

        vm.params = $stateParams;

        function activate() {
            var promise = [getJobCardList(), getKpData($stateParams.pMC_FAB_PROD_ORD_H_ID, null)];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        vm.IS_EDIT_MODE = false;

        function getKpData(pMC_FAB_PROD_ORD_H_ID, pMC_FAB_PROD_ORD_D_ID) {
            return KnittingDataService.getDataByUrl('/FabProdKnitOrder/SelectByID?pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID + '&pOption=3003&pLK_COL_TYPE_ID_LST=358,361,407,432&pMC_FAB_PROD_ORD_D_ID='+(pMC_FAB_PROD_ORD_D_ID||'')).then(function (res) {
                vm.IS_EDIT_MODE = false;

                $scope.datas = _.filter(res, function (ob) {
                    return ob['REQ_PENDING'] > 0;
                });
            })
        };

        vm.editData = function (item) {
            item['EDIT_MODE'] = true;
            $scope.datas = [item];
        };

        vm.onRemoveReqRow = function (index, dataItems) {
            //alert(index);
            dataItems.splice(index, 1);
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
                                     RF_FAB_PROD_CAT_ID: o.RF_FAB_PROD_CAT_ID,
                                     IS_SOLID: (o.IS_SOLID || 'S')
                                     
                                 }
                             }))
                         }

                     }));

                     var submitData = { XML: xml, KNT_YRN_STR_REQ_H_ID: ($stateParams.pKNT_YRN_STR_REQ_H_ID || -1), KNT_SCO_CLCF_PRG_H_ID: $stateParams.pKNT_SCO_CLCF_PRG_H_ID, IS_EDIT_MODE: 'N', IS_SUBMIT: 'Y' };
                     console.log(submitData);

                     return KnittingDataService.saveDataByUrl({ XML: xml, KNT_YRN_STR_REQ_H_ID: ($stateParams.pKNT_YRN_STR_REQ_H_ID || -1), KNT_SCO_CLCF_PRG_H_ID: $stateParams.pKNT_SCO_CLCF_PRG_H_ID, IS_EDIT_MODE: 'N', IS_SUBMIT: 'Y' }, '/KnitPlan/CreateStoreRequisitionCollarCuff').then(function (res) {
                         res['data'] = angular.fromJson(res.jsonStr);
                         console.log(res);

                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             getJobCardList(parseInt(res.data.OP_KNT_YRN_STR_REQ_H_ID));
                             getKpData($stateParams.pMC_FAB_PROD_ORD_H_ID, null);

                             $state.go('KnitPlanJobCardCollarCuffSco', { pMC_FAB_PROD_ORD_H_ID: $stateParams.pMC_FAB_PROD_ORD_H_ID, pKNT_SCO_CLCF_PRG_H_ID: $stateParams.pKNT_SCO_CLCF_PRG_H_ID, pKNT_YRN_STR_REQ_H_ID: parseInt(res.data.OP_KNT_YRN_STR_REQ_H_ID) }, { notify: false });
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
                                IS_SOLID: (o.IS_SOLID || 'S'),
                                RF_FAB_PROD_CAT_ID: o.RF_FAB_PROD_CAT_ID

                            }
                        }))
                    }

                }));

                return KnittingDataService.saveDataByUrl({ XML: xml, KNT_YRN_STR_REQ_H_ID: ($stateParams.pKNT_YRN_STR_REQ_H_ID || -1), KNT_SCO_CLCF_PRG_H_ID: $stateParams.pKNT_SCO_CLCF_PRG_H_ID, IS_EDIT_MODE: (IS_EDIT_MODE || 'N'), IS_SUBMIT: 'N' }, '/KnitPlan/CreateStoreRequisitionCollarCuff').then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $state.go('KnitPlanJobCardCollarCuffSco', { pMC_FAB_PROD_ORD_H_ID: $stateParams.pMC_FAB_PROD_ORD_H_ID, pKNT_SCO_CLCF_PRG_H_ID: $stateParams.pKNT_SCO_CLCF_PRG_H_ID, pKNT_YRN_STR_REQ_H_ID: parseInt(res.data.OP_KNT_YRN_STR_REQ_H_ID) }, { notify: false });
                        $stateParams.pKNT_YRN_STR_REQ_H_ID = parseInt(res.data.OP_KNT_YRN_STR_REQ_H_ID);
                        getJobCardList(parseInt(res.data.OP_KNT_YRN_STR_REQ_H_ID));
                       
                    }
                    config.appToastMsg(res.data.PMSG);
                });
            }



        };

        function getJobCardList(pKNT_YRN_STR_REQ_H_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/GetJobCardListCollarCuffSco?pKNT_YRN_STR_REQ_H_ID=' + (pKNT_YRN_STR_REQ_H_ID || $stateParams.pKNT_YRN_STR_REQ_H_ID || 0) + '&pOption=3008').then(function (res) {
                vm.jobCardList = res;
                if (res.length > 0) {
                    vm.YARN_REQ = {
                        STR_REQ_NO: res[0].STR_REQ_NO,
                        LK_REQ_STATUS_ID: res[0].LK_REQ_STATUS_ID
                    }
                }
            });
        }

        vm.resetAdding = function () {
            $scope.datas = [];
        }

        vm.submitData = function (dataOri, valid1) {

            console.log(dataOri);

            //if (!valid1) {
            //    return;
            //}

            angular.forEach(dataOri, function (val, key) {

                if (val.IDX > -1) {
                    //alert('Y');

                    angular.forEach(val.items, function (val1, key1) {

                        var data = {
                            KNT_PLAN_H_ID: val.KNT_PLAN_H_ID,
                            REMARKS: val.REMARKS,
                            items: val.items,
                            BYR_ACC_NAME_EN: val.BYR_ACC_NAME_EN,
                            WORK_STYLE_NO: val.WORK_STYLE_NO,
                            ORDER_NO_LIST: val.ORDER_NO_LIST,
                            FAB_TYPE_NAME: val.FAB_TYPE_NAME,
                            COLOR_NAME_EN: val.COLOR_NAME_EN,
                            YRN_DETAILS: val1.YRN_DETAILS || val1.ITEM_NAME_EN,
                            IDX: key,
                            RQSTD_NEW: val1.RQSTD_NEW,
                            RF_FAB_PROD_CAT_ID: val.RF_FAB_PROD_CAT_ID
                        }

                        vm.jobCardList.splice(val.IDX, 1, data);
                    });
                    

                } else {
                    //alert('N');

                    angular.forEach(val.items, function (val1, key1) {
                        var data = {
                            KNT_PLAN_H_ID: val.KNT_PLAN_H_ID,
                            REMARKS: val.REMARKS,
                            items: val.items,
                            BYR_ACC_NAME_EN: val.BYR_ACC_NAME_EN,
                            WORK_STYLE_NO: val.WORK_STYLE_NO,
                            ORDER_NO_LIST: val.ORDER_NO_LIST,
                            FAB_TYPE_NAME: val.FAB_TYPE_NAME,
                            COLOR_NAME_EN: val.COLOR_NAME_EN,
                            YRN_DETAILS: val1.YRN_DETAILS || val1.ITEM_NAME_EN,
                            IDX: key,
                            RQSTD_QTY: val1.RQSTD_NEW,
                            RF_FAB_PROD_CAT_ID: val.RF_FAB_PROD_CAT_ID
                        }

                        if (!_.some(vm.jobCardList, function (o) {
                            return o.KNT_PLAN_H_ID == val.KNT_PLAN_H_ID;
                        })) {
                            vm.jobCardList.push(data);
                        }
                    });
                }
                $scope.datas = [];

            });

            
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

            vm.createStoreRequisition(data,false,'Y');
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
                            pRF_FAB_PROD_CAT_ID: null,
                            pHAS_COL_CUFF: 'Y',
                            IS_REQUISITION: true,
                            RF_FAB_PROD_CAT_ID_LST: '1,2,3,4,5,6',
                            LK_COL_TYPE_ID_LST: ($state.current.data.LK_COL_TYPE_ID_LST || '358,361,407,432'),
                            DEFAULT_CAT_ID: 2,
                            ORDER_WISE_SELECT: true

                        }
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data.MC_FAB_PROD_ORD_H_ID) {
                    if (data.hasOwnProperty('MC_FAB_PROD_ORD_D_ID')) {
                        getKpData(data.MC_FAB_PROD_ORD_H_ID, data.MC_FAB_PROD_ORD_D_ID);
                    } else {
                        getKpData(data.MC_FAB_PROD_ORD_H_ID, null);
                    }

                    
                } 

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        }

        vm.backtButtonList = [{ BTN_ID: 1, BTN_NAME: 'Program List' }, { BTN_ID: 2, BTN_NAME: 'Program' }];
        vm.backToProg = function (itm) {
            if (itm.BTN_ID == 1) {
                $state.go('ScoCollarCuffProgList', { pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID || 0 });
            }
            else if (itm.BTN_ID == 2) {
                $state.go('ScoCollarCuffProgH.CollarCuffDtl', { pKNT_SCO_CLCF_PRG_H_ID: $stateParams.pKNT_SCO_CLCF_PRG_H_ID || 0, pMC_FAB_PROD_ORD_H_ID: $stateParams.pMC_FAB_PROD_ORD_H_ID || 0 });
            }
            
        }

    }

})();