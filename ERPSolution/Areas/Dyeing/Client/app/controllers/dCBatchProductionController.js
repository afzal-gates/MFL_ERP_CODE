(function () {
    'use strict';
    angular.module('multitex.dyeing').directive('autoFocus', function ($timeout) {
        return {
            restrict: 'AC',
            link: function (_scope, _element) {
                $timeout(function () {
                    _element[0].focus();
                }, 0);
            }
        };
    });

    angular.module('multitex.dyeing').controller('DCBatchProductionController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', '$filter', '$ngConfirm', DCBatchProductionController]);
    function DCBatchProductionController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog, $filter, $ngConfirm) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.DateTimeFormat = config.appDateTimeFormat;
        vm.TimeFormat = config.appTimeFormat;
        vm.ACTION_DIS = 0;
        vm.REPROCESS_FLAG = 0;
        vm.today = new Date();
        //vm.form = {};

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };

        vm.form = { REQ_STORE_ID: '4', ISS_STORE_ID: '6', STR_REQ_BY: cur_user_id, PROD_DT: vm.today, RF_ACTN_VIEW: 0, DYE_RE_PROC_TYPE_ID: 1 };
        vm.formItem = { 'QTY_MOU_ID': '3', 'CENTRAL_STK': 0, 'SUB_STK': 0 };

        vm.dyeBatchLot = [];

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetMachineList(), ReceiveItemList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.PROD_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PROD_DT_LNopened = true;
        }

        vm.showLoadBtn = function (str) {
            var row = 0;
            var pwd = str.substring(str.length - 4, str.length);

            $ngConfirm({
                title: 'Prompt for OTP',
                contentUrl: 'form.html',
                buttons: {
                    getPassCode: {
                        text: 'OK',
                        disabled: true,
                        btnClass: 'btn-green',
                        action: function (scope) {

                            if (scope.passCode == pwd) {
                                vm.form.IS_BP_LOCKED = "N";
                                config.appToastMsg("MULTI-001 OTP Success!!!");
                            }
                            else {

                                config.appToastMsg("MULTI-005 OTP does not match!!!");
                                return;
                            }
                        }
                    },
                    later: function () {
                        return;
                    }
                },
                onScopeReady: function (scope) {
                    var self = this;
                    scope.showPassword = false;
                    scope.passCode = "";
                    scope.toggleShowPassword = function () {
                        scope.showPassword = !scope.showPassword;
                    }

                    scope.textChange = function () {
                        if (scope.passCode.length == 4)
                            self.buttons.getPassCode.setDisabled(false);
                        else
                            self.buttons.getPassCode.setDisabled(true);
                    }
                }
            });

        }

        vm.searchBatchInfo = function (pPageNo) {

            if (pPageNo.length > 9) {

                if (vm.form.DYE_BATCH_NO.includes('S-') == true)
                    vm.form.IS_ROLL_OK = 'Y';

                if (vm.form.DYE_BATCH_NO.match('L')) {

                    DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchSubLotByID?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "")).then(function (res_b) {
                        if (res_b.length == 0) {

                            config.appToastMsg("MULTI-005 No Data Found!!!");
                            return;
                        }

                        vm.dyeBatchLot = res_b;
                        vm.form.DYE_STR_REQ_H_ID_LOT = res_b[0].DYE_STR_REQ_H_ID;
                        vm.showSplash = true;
                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "")).then(function (res) {

                            if (res.batch.length > 0) {

                                vm.BatchProgramList = res.prd;
                                vm.BatchFabricList = res.fab;

                                vm.form.DYE_STR_REQ_H_ID = res.batch[0].DYE_STR_REQ_H_ID;
                                vm.form.STR_REQ_NO = res.batch[0].STR_REQ_NO;
                                vm.form.STR_REQ_DT = res.batch[0].STR_REQ_DT;
                                vm.form.DYE_MACHINE_ID = res.batch[0].DYE_MACHINE_ID;
                                vm.form.BUYER_NAME_EN = res.batch[0].BUYER_NAME_EN;
                                vm.form.STYLE_NO = res.batch[0].STYLE_NO;
                                vm.form.ORDER_NO = res.batch[0].ORDER_NO;
                                vm.form.COLOR_NAME_EN = res.batch[0].COLOR_NAME_EN;
                                vm.form.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                                vm.form.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                                vm.form.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                                vm.form.DYE_RE_PROC_TYPE_ID = res.batch[0].DYE_RE_PROC_TYPE_ID;
                                vm.form.REQ_RE_PROC_TYPE_ID = res.batch[0].REQ_RE_PROC_TYPE_ID;
                                vm.form.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                                vm.form.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                                vm.form.ACTN_ROLE_FLAG = res.batch[0].ACTN_ROLE_FLAG;
                                vm.form.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO;
                                vm.form.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                                vm.form.ACT_BATCH_QTY = res.batch[0].ACT_BATCH_QTY;

                                if (res.prd.length > 0) {
                                    vm.form.IS_BP_LOCKED = (res.prd[0].IS_BP_LOCKED || 'N');
                                    vm.form.IS_EDT_LOCKED = (res.prd[0].IS_EDT_LOCKED || 'N');
                                    vm.form.DYE_BT_PROD_ID = (res.prd[0].DYE_BT_PROD_ID || 0);
                                    vm.form.DYE_MACHINE_ID = (res.prd[0].DYE_MACHINE_ID || res.batch[0].DYE_MACHINE_ID);
                                    vm.form.DYE_BT_STS_TYPE_ID = (res.prd[0].DYE_BT_STS_TYPE_ID || 1);
                                    vm.form.IS_DISABLE = (res.prd[0].IS_DISABLE || 0);
                                    vm.form.LK_CHQ_RL_STS_ID = (res.prd[0].LK_CHQ_RL_STS_ID || null);
                                    vm.form.LOAD_TIME = (res.prd[0].LOAD_TIME || new Date());
                                    vm.form.UN_LOAD_TIME = (res.prd[0].UN_LOAD_TIME || new Date());
                                    vm.form.IS_RPROC_DONE = (res.prd[0].IS_RPROC_DONE || 'Y');

                                }
                                else {
                                    vm.form.IS_RPROC_DONE = 'Y';
                                    vm.form.IS_EDT_LOCKED = 'N';
                                    vm.form.IS_BP_LOCKED = 'N';
                                    vm.form.DYE_BT_PROD_ID = 0;
                                    vm.form.DYE_BT_STS_TYPE_ID = 1;
                                    vm.form.IS_DISABLE = 0;
                                    vm.form.LK_CHQ_RL_STS_ID = null;
                                }
                                vm.loadRecipe(true);
                            }
                            else {
                                config.appToastMsg("MULTI-005 Please issue required material at this batch first!");
                                vm.form = { PROD_DT: vm.today, DYE_BATCH_NO: vm.form.DYE_BATCH_NO, DYE_STR_REQ_H_ID_LOT: '', DYE_MACHINE_ID: '' };
                                vm.BatchProgramList = [];
                                vm.BatchFabricList = [];
                                vm.loadRecipe(false);
                            }
                            vm.showSplash = false;
                        });
                    });
                    return;
                }
                else {

                    //DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchSubLotByID?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "")).then(function (res_b) {
                    //    vm.dyeBatchLot = res_b;
                    //    if (res_b.length == 1) {
                    //        vm.form.DYE_STR_REQ_H_ID_LOT = res_b[0].DYE_STR_REQ_H_ID;
                    vm.showSplash = true;
                    DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "")).then(function (res) {

                        if (res.batch.length > 0) {

                            vm.BatchProgramList = res.prd;
                            vm.BatchFabricList = res.fab;

                            vm.form.DYE_STR_REQ_H_ID = res.batch[0].DYE_STR_REQ_H_ID;
                            vm.form.STR_REQ_NO = res.batch[0].STR_REQ_NO;
                            vm.form.STR_REQ_DT = res.batch[0].STR_REQ_DT;
                            vm.form.DYE_MACHINE_ID = res.batch[0].DYE_MACHINE_ID;
                            vm.form.BUYER_NAME_EN = res.batch[0].BUYER_NAME_EN;
                            vm.form.STYLE_NO = res.batch[0].STYLE_NO;
                            vm.form.ORDER_NO = res.batch[0].ORDER_NO;
                            vm.form.COLOR_NAME_EN = res.batch[0].COLOR_NAME_EN;
                            vm.form.LD_RECIPE_NO = res.batch[0].LD_RECIPE_NO;
                            vm.form.COLOR_GRP_NAME_EN = res.batch[0].COLOR_GRP_NAME_EN;
                            vm.form.MC_LD_RECIPE_H_ID = res.batch[0].MC_LD_RECIPE_H_ID;
                            vm.form.DYE_RE_PROC_TYPE_ID = res.batch[0].DYE_RE_PROC_TYPE_ID;
                            vm.form.REQ_RE_PROC_TYPE_ID = res.batch[0].REQ_RE_PROC_TYPE_ID;
                            vm.form.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                            vm.form.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                            vm.form.ACTN_ROLE_FLAG = res.batch[0].ACTN_ROLE_FLAG;
                            vm.form.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO;
                            vm.form.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                            vm.form.ACT_BATCH_QTY = res.batch[0].ACT_BATCH_QTY;

                            if (res.prd.length > 0) {
                                vm.form.IS_EDT_LOCKED = (res.prd[0].IS_EDT_LOCKED || 'N');
                                vm.form.IS_BP_LOCKED = (res.prd[0].IS_BP_LOCKED || 'N');
                                vm.form.DYE_BT_PROD_ID = (res.prd[0].DYE_BT_PROD_ID || 0);
                                vm.form.DYE_MACHINE_ID = (res.prd[0].DYE_MACHINE_ID || res.batch[0].DYE_MACHINE_ID);
                                vm.form.DYE_BT_STS_TYPE_ID = (res.prd[0].DYE_BT_STS_TYPE_ID || 1);
                                vm.form.IS_DISABLE = (res.prd[0].IS_DISABLE || 0);
                                vm.form.LK_CHQ_RL_STS_ID = (res.prd[0].LK_CHQ_RL_STS_ID || null);
                                vm.form.LOAD_TIME = (res.prd[0].LOAD_TIME || new Date());
                                vm.form.UN_LOAD_TIME = (res.prd[0].UN_LOAD_TIME || new Date());
                                vm.form.IS_RPROC_DONE = (res.prd[0].IS_RPROC_DONE || 'Y');

                            }
                            else {
                                vm.form.IS_RPROC_DONE = 'Y';
                                vm.form.IS_BP_LOCKED = 'N';
                                vm.form.IS_EDT_LOCKED = 'N';
                                vm.form.DYE_BT_PROD_ID = 0;
                                vm.form.DYE_BT_STS_TYPE_ID = 1;
                                vm.form.IS_DISABLE = 0;
                                vm.form.LK_CHQ_RL_STS_ID = null;
                            }
                            vm.loadRecipe(true);
                        }
                        else {
                            config.appToastMsg("MULTI-005 Please issue required material at this batch first!");
                            vm.form = { PROD_DT: vm.today, DYE_BATCH_NO: vm.form.DYE_BATCH_NO, DYE_STR_REQ_H_ID_LOT: '', DYE_MACHINE_ID: '' };
                            vm.BatchProgramList = [];
                            vm.BatchFabricList = [];
                            vm.loadRecipe(false);
                        }
                        vm.showSplash = false;
                    });
                    //    }
                    //    else {
                    //        vm.form = { PROD_DT: vm.today, DYE_BATCH_NO: vm.form.DYE_BATCH_NO, DYE_STR_REQ_H_ID_LOT: '', DYE_MACHINE_ID: '' };
                    //        vm.BatchProgramList = [];
                    //        vm.BatchFabricList = [];
                    //        vm.loadRecipe(false);
                    //        config.appToastMsg("MULTI-005 Please Select Working Batch Sub-Lot!!!");
                    //    }
                    //});

                }

            }
        }


        vm.searchBatchInfoBySubLot = function (e) {
            return;
            var item = e.sender.dataItem(e.item);
            if (item.DYE_STR_REQ_H_ID > 0) {

                vm.showSplash = true;
                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "") + '&pDYE_STR_REQ_H_ID=' + (item.DYE_STR_REQ_H_ID || null)).then(function (res) {

                    if (res.batch.length > 0) {

                        vm.BatchProgramList = res.prd;
                        vm.BatchFabricList = res.fab;
                        if (res.prd[0].DYE_STR_REQ_H_ID > 0) {
                            vm.form.DYE_STR_REQ_H_ID = res.prd[0].DYE_STR_REQ_H_ID;
                            vm.form.STR_REQ_NO = res.prd[0].STR_REQ_NO;
                            vm.form.STR_REQ_DT = res.prd[0].STR_REQ_DT;
                            vm.form.DYE_MACHINE_ID = res.prd[0].DYE_MACHINE_ID;
                            vm.form.BUYER_NAME_EN = res.prd[0].BUYER_NAME_EN;
                            vm.form.STYLE_NO = res.prd[0].STYLE_NO;
                            vm.form.ORDER_NO = res.prd[0].ORDER_NO;
                            vm.form.COLOR_NAME_EN = res.prd[0].COLOR_NAME_EN;
                            vm.form.LD_RECIPE_NO = res.prd[0].LD_RECIPE_NO;
                            vm.form.COLOR_GRP_NAME_EN = res.prd[0].COLOR_GRP_NAME_EN;
                            vm.form.MC_LD_RECIPE_H_ID = res.prd[0].MC_LD_RECIPE_H_ID;
                            vm.form.DYE_RE_PROC_TYPE_ID = res.prd[0].DYE_RE_PROC_TYPE_ID;
                            vm.form.REQ_RE_PROC_TYPE_ID = res.prd[0].REQ_RE_PROC_TYPE_ID;
                            vm.form.LQR_RATIO = "1:" + res.prd[0].LQR_RATIO;
                            vm.form.DYE_MTHD_NAME = res.prd[0].DYE_MTHD_NAME;
                            vm.form.ACTN_ROLE_FLAG = res.prd[0].ACTN_ROLE_FLAG;
                            vm.form.DYE_BATCH_NO_LST = res.prd[0].DYE_BATCH_NO;
                            vm.form.ACT_BATCH_QTY_LST = res.prd[0].ACT_BATCH_QTY + " Kg";
                            vm.form.ACT_BATCH_QTY = res.prd[0].ACT_BATCH_QTY;
                        }
                        else {
                            var id = res.batch.length - 1;
                            vm.form.DYE_STR_REQ_H_ID = res.batch[id].DYE_STR_REQ_H_ID;
                            vm.form.STR_REQ_NO = res.batch[id].STR_REQ_NO;
                            vm.form.STR_REQ_DT = res.batch[id].STR_REQ_DT;
                            vm.form.DYE_MACHINE_ID = res.batch[id].DYE_MACHINE_ID;
                            vm.form.BUYER_NAME_EN = res.batch[id].BUYER_NAME_EN;
                            vm.form.STYLE_NO = res.batch[id].STYLE_NO;
                            vm.form.ORDER_NO = res.batch[id].ORDER_NO;
                            vm.form.COLOR_NAME_EN = res.batch[id].COLOR_NAME_EN;
                            vm.form.LD_RECIPE_NO = res.batch[id].LD_RECIPE_NO;
                            vm.form.COLOR_GRP_NAME_EN = res.batch[id].COLOR_GRP_NAME_EN;
                            vm.form.MC_LD_RECIPE_H_ID = res.batch[id].MC_LD_RECIPE_H_ID;
                            vm.form.DYE_RE_PROC_TYPE_ID = res.batch[id].DYE_RE_PROC_TYPE_ID;
                            vm.form.REQ_RE_PROC_TYPE_ID = res.batch[id].REQ_RE_PROC_TYPE_ID;
                            vm.form.LQR_RATIO = "1:" + res.batch[id].LQR_RATIO;
                            vm.form.DYE_MTHD_NAME = res.batch[id].DYE_MTHD_NAME;
                            vm.form.ACTN_ROLE_FLAG = res.batch[id].ACTN_ROLE_FLAG;
                            vm.form.DYE_BATCH_NO_LST = res.batch[id].DYE_BATCH_NO;
                            vm.form.ACT_BATCH_QTY_LST = res.batch[id].ACT_BATCH_QTY + " Kg";
                            vm.form.ACT_BATCH_QTY = res.batch[id].ACT_BATCH_QTY;
                        }
                        if (res.prd.length > 0) {
                            vm.form.IS_EDT_LOCKED = (res.prd[0].IS_EDT_LOCKED || 'N');
                            vm.form.IS_BP_LOCKED = (res.prd[0].IS_BP_LOCKED || 'N');
                            vm.form.DYE_BT_PROD_ID = (res.prd[0].DYE_BT_PROD_ID || 0);
                            vm.form.DYE_MACHINE_ID = (res.prd[0].DYE_MACHINE_ID || res.batch[0].DYE_MACHINE_ID);
                            vm.form.DYE_BT_STS_TYPE_ID = (res.prd[0].DYE_BT_STS_TYPE_ID || 1);
                            vm.form.IS_DISABLE = (res.prd[0].IS_DISABLE || 0);
                            vm.form.LK_CHQ_RL_STS_ID = (res.prd[0].LK_CHQ_RL_STS_ID || null);
                            vm.form.LOAD_TIME = (res.prd[0].LOAD_TIME || new Date());
                            vm.form.UN_LOAD_TIME = (res.prd[0].UN_LOAD_TIME || new Date());
                            vm.form.IS_RPROC_DONE = (res.prd[0].IS_RPROC_DONE || 'Y');

                        }
                        else {
                            vm.form.IS_RPROC_DONE = 'Y';
                            vm.form.IS_EDT_LOCKED = 'N';
                            vm.form.IS_BP_LOCKED = 'N';
                            vm.form.DYE_BT_PROD_ID = 0;
                            vm.form.DYE_BT_STS_TYPE_ID = 1;
                            vm.form.IS_DISABLE = 0;
                            vm.form.LK_CHQ_RL_STS_ID = null;
                        }
                        vm.loadRecipe(true);
                    }
                    else {
                        config.appToastMsg("MULTI-005 Please issue required material at this batch first!");
                        vm.form = { PROD_DT: vm.today, DYE_BATCH_NO: vm.form.DYE_BATCH_NO, DYE_STR_REQ_H_ID: '', DYE_MACHINE_ID: '' };
                        vm.BatchProgramList = [];
                        vm.BatchFabricList = [];
                        vm.loadRecipe(false);
                    }
                    vm.showSplash = false;
                });

            }
        }



        function GetMachineList() {

            DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0').then(function (res) {
                vm.machineList = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
        }

        $scope.LOAD_TIME_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.LOAD_TIME_LNopened = true;
        }

        $scope.UNLOAD_TIME_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.UNLOAD_TIME_LNopened = true;
        }

        vm.loadRecipe = function (val) {

            if (val) {
                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfoDtlByID?pDYE_STR_REQ_H_ID=' + (vm.form.DYE_STR_REQ_H_ID || 0)).then(function (output) {
                    var list = _.uniqBy(output, function (d) { return d.PRD_PHASE_NAME });
                    console.log(output);
                    vm.recipeGroupList = list;
                    vm.recipeItemList = output;
                    vm.showSplash = false;
                });
            }
            else {
                vm.recipeGroupList = [];
                vm.recipeItemList = [];
                vm.showSplash = false;
            }
        }

        function GetBatchList() {

            if ($stateParams.pDYE_STR_REQ_H_ID > 0) {

                vm.showSplash = true;

                vm.form = formData[0];
                var TW = 0;
                for (var b = 0; b < formData.length; b++)
                    TW = TW + formData[b].ACT_BATCH_QTY;

                if ($stateParams.pADDITION_FLAG > 0) {
                    for (var i = 0; i < formData.length; i++) {
                        formData[i].DYE_BT_DC_REQ_D1_ID = '';
                    }

                    vm.form["TOTAL_WEIGHT_OLD"] = TW;
                }

                vm.form.TOTAL_WEIGHT = TW;
                vm.form.TOTAL_WATER = TW * vm.form.LQR_RATIO;

                vm.batchList = new kendo.data.DataSource({
                    data: formData
                });

                var bData = formData.map(function (o) {
                    return {
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID == null ? 0 : o.DYE_BT_CARD_H_ID,
                        DYE_BT_DC_REQ_D1_ID: o.DYE_BT_DC_REQ_D1_ID == null ? 0 : o.DYE_BT_DC_REQ_D1_ID,
                        DYE_BATCH_NO: o.DYE_BATCH_NO,
                        ACT_BATCH_QTY: o.ACT_BATCH_QTY,
                        UNIT: "Kg"
                    }
                });

                vm.BatchProgramList = new kendo.data.DataSource({
                    data: bData
                });

                var OptUrl = "GetDCBatchProgramRequisitionInfoDtlByID";

                if ($stateParams.pADDITION_FLAG > 0) {
                    OptUrl = "GetDCBatchProgramRequisitionInfoDtlForARByID";
                }
                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/' + OptUrl + '?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (output) {
                    var list = _.uniqBy(output, function (d) { return d.PRD_PHASE_NAME });
                    //console.log(output);
                    //console.log(list);
                    vm.recipeGroupList = list;
                    if ($stateParams.pADDITION_FLAG > 0) {
                        for (var i = 0; i < output.length; i++) {
                            output[i].DYE_BT_DC_REQ_D2_ID = '';
                            output[i].RQD_QTY = '';
                            output[i].RQD_QTY_K = '';
                            output[i].RQD_QTY_G = '';
                            output[i].RQD_QTY_M = '';
                        }
                    }

                    vm.recipeItemList = output;
                    //vm.recipeItemList["ItemList"] = {};

                    vm.recipeItemList["MOU_CODE"] = "";
                    vm.recipeItemList["DOSE_QTY"] = 0;
                    vm.recipeItemList["QTY_MOU_ST"] = 0;
                    vm.recipeItemList["RQD_QTY_K"] = 0;
                    vm.recipeItemList["RQD_QTY_G"] = 0;
                    vm.recipeItemList["RQD_QTY_M"] = 0;
                    vm.recipeItemList["RQD_QTY"] = 0;
                    vm.recipeItemList["SUB_STK"] = 0;
                    vm.recipeItemList["CENTRAL_STK"] = 0;
                    vm.showSplash = false;
                });

            }
            else {
                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetPendingBatchProgram').then(function (res) {

                    vm.batchList = new kendo.data.DataSource({
                        data: res
                    });

                    var list = _.filter(res, function (o) { return o.DYE_BT_CARD_H_ID === 0 });

                    var bData = list.map(function (o) {
                        return {
                            DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID == null ? 0 : o.DYE_BT_CARD_H_ID,
                            DYE_BT_DC_REQ_D1_ID: o.DYE_BT_DC_REQ_D1_ID == null ? 0 : o.DYE_BT_DC_REQ_D1_ID,
                            DYE_BATCH_NO: o.DYE_BATCH_NO,
                            ACT_BATCH_QTY: o.ACT_BATCH_QTY,
                            UNIT: "Kg"
                        }
                    });

                    vm.BatchProgramList = new kendo.data.DataSource({
                        data: bData
                    });


                    vm.BatchFabricList = new kendo.data.DataSource({
                        data: []
                    });


                });
            }
        }


        function ReceiveItemList() {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/GetDCIssueRequisitionInfoDtlByID?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };


        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {
                vm.showSplash = true;

                if (vm.formItem.uid) {
                    vm.form.TOTAL_WEIGHT = "";
                    var row = vm.BatchProgramList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.BatchProgramList.data(), function (o) {
                        return (o.DYE_BT_CARD_H_ID === vm.formItem.DYE_BT_CARD_H_ID && o.uid == vm.formItem.uid)
                    }).length;

                    if (count <= 1) {

                        row["DYE_BT_CARD_H_ID"] = vm.formItem.DYE_BT_CARD_H_ID;
                        row["ACT_BATCH_QTY"] = parseFloat(vm.formItem.ACT_BATCH_QTY);
                        row["DYE_BATCH_NO"] = vm.formItem.DYE_BATCH_NO;
                        //row["SL_NO"] = vm.formItem.SL_NO;
                        row["DYE_BT_DC_REQ_D1_ID"] = vm.formItem.DYE_BT_DC_REQ_D1_ID;

                        vm.formItem = { DYE_BT_CARD_H_ID: '' };

                        var sum = 0;
                        for (var x = 0; x < vm.BatchProgramList.data().length; x++)
                            sum = sum + vm.BatchProgramList.data()[x].ACT_BATCH_QTY;
                        vm.form.TOTAL_WEIGHT = sum;
                        vm.form.TOTAL_WATER = sum * vm.form.LQR_RATIO;
                        console.log(vm.BatchProgramList.data());
                        vm.BatchProgramList.data().sort()
                        vm.checkAllGrid();
                        config.appToastMsg("Item information has been updated successfully!");
                    }
                    vm.showSplash = false;
                    return;
                }
                else {

                    var MC_LD_RECIPE_H_ID = 0;
                    var MC_COLOR_GRP_ID = 0;
                    var item = _.filter(vm.batchList.data(), function (o) {
                        return (o.DYE_BT_CARD_H_ID == parseInt(vm.formItem.DYE_BT_CARD_H_ID) || o.MERGE_BT_CRD_ID == parseInt(vm.formItem.DYE_BT_CARD_H_ID))
                    });

                    //DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetPendingBatchProgram?pDYE_BT_CARD_H_ID=' + vm.formItem.DYE_BT_CARD_H_ID).then(function (res) {
                    //    item.push(res);
                    //});

                    for (var i = 0; i < item.length; i++) {

                        MC_LD_RECIPE_H_ID = item[i].MC_LD_RECIPE_H_ID;
                        MC_COLOR_GRP_ID = item[i].MC_COLOR_GRP_ID;

                        var count = _.filter(vm.BatchProgramList.data(), function (o) {
                            return (o.DYE_BT_CARD_H_ID === item[i].DYE_BT_CARD_H_ID)
                        }).length;

                        if (count == 0) {

                            var idx = vm.BatchProgramList.data().length + 1;
                            item[i].SL_NO = idx;
                            vm.BatchProgramList.insert(idx, item[i]);

                            var sum = 0;
                            for (var x = 0; x < vm.BatchProgramList.data().length; x++)
                                sum = sum + vm.BatchProgramList.data()[x].ACT_BATCH_QTY;
                            vm.form.TOTAL_WEIGHT = sum;
                            vm.form.TOTAL_WATER = sum * vm.form.LQR_RATIO;

                        }
                        else {
                            config.appToastMsg("MULTI-005 Duplicate Batch exists!");
                        }
                    }


                    var dataList = angular.copy(vm.ItemList);

                    DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetRecipeItemList?pMC_LD_RECIPE_H_ID=' + (MC_LD_RECIPE_H_ID || 0)).then(function (res) {

                        vm.DyeList = res;

                    });

                    var pOption = 3001;

                    if (vm.form.WITHOUT_HIS == true)
                        pOption = 3002;

                    DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDDosingTempleteDtl?pDYE_DOSE_TMPLT_H_ID=' + (vm.form.DYE_DOSE_TMPLT_H_ID || 0) + '&pMC_LD_RECIPE_H_ID=' + (vm.form.MC_LD_RECIPE_H_ID || 0) + "&pOption=" + pOption).then(function (output) {
                        var list = _.uniqBy(output, function (d) { return d.PRD_PHASE_NAME });

                        vm.recipeGroupList = list;
                        vm.recipeItemList = output;

                        vm.recipeItemList["ItemList"] = {};

                        vm.recipeItemList["MOU_CODE"] = "";
                        vm.recipeItemList["DOSE_QTY"] = 0;
                        vm.recipeItemList["QTY_MOU_ST"] = 0;
                        vm.recipeItemList["RQD_QTY_K"] = 0;
                        vm.recipeItemList["RQD_QTY_G"] = 0;
                        vm.recipeItemList["RQD_QTY_M"] = 0;
                        vm.recipeItemList["RQD_QTY"] = 0;
                        vm.recipeItemList["SUB_STK"] = 0;
                        vm.recipeItemList["CENTRAL_STK"] = 0;
                        vm.checkAllGrid();
                        vm.showSplash = false;
                    });
                }
            }

        };

        vm.resetItemData = function () {
            vm.formItem = { QTY_MOU_ID: '3', CENTRAL_STK: 0, SUB_STK: 0 };
        };

        vm.reset = function () {

            $state.go($state.current, { pDYE_STR_REQ_H_ID: 0 }, { inherit: false, reload: true });
        };



        vm.gridOptionsFabric = {
            pageable: false,
            height: '100px',
            scrollable: true,
            columns: [
                { field: "SL_NO", title: "SL No", type: "string", width: "10%", template: "<span class='row-number'></span>" },
                { field: "FABRIC_DESC", title: "Fabric Description", type: "string", width: "100%" },
            ],
            dataBound: function () {
                var rows = this.items();
                $(rows).each(function () {
                    var index = $(this).index() + 1;
                    var rowLabel = $(this).find(".row-number");
                    $(rowLabel).html(index);

                    var rowLabel = $(this).find(".row-unit");
                    $(rowLabel).html("Kg");
                });
            }
        };


        $scope.toolTipOptions = {
            filter: "td",
            position: "top",
            content: function (e) {
                var grid = e.target.closest(".k-grid").getKendoGrid();
                var dataItem = grid.dataItem(e.target.closest("tr"));
                return dataItem.STR_REQ_NO;

            },
            show: function (e) {
                this.popup.element[0].style.width = "100%";
            }
        }

        vm.gridOptionsBatch = {
            pageable: false,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            height: '200px',
            scrollable: true,
            columns: [
                { field: "SL_NO", title: "SL No", type: "string", width: "7%", template: "<span class='row-number'></span>" },

                //{ field: "STR_REQ_NO", title: "Req No", type: "string", width: "10%", },
                { field: "DYE_MACHINE_NO", title: "M/C No", type: "string", width: "10%", },
                { field: "LOAD_TIME", title: "Load Time", type: "date", template: "#= kendo.toString(kendo.parseDate(LOAD_TIME),'dd-MMM-yyyy hh:mm tt') #", width: "25%", },
                { field: "UN_LOAD_TIME", title: "Unload Time", type: "date", template: "#= kendo.toString(kendo.parseDate(UN_LOAD_TIME),'dd-MMM-yyyy hh:mm tt') #", width: "25%", },
                { field: "TIME_DIFF", title: "Duration", type: "string", width: "10%", },
                { field: "RE_PROC_TYPE_NAME", title: "Type", type: "string", width: "10%", },
                { field: "BT_STS_TYP_NAME", title: "Status", type: "string", width: "15%", },

            ],
            dataBound: function () {
                var rows = this.items();
                $(rows).each(function () {
                    var index = $(this).index() + 1;
                    var rowLabel = $(this).find(".row-number");
                    $(rowLabel).html(index);

                    var rowLabel = $(this).find(".row-unit");
                    $(rowLabel).html("Kg");
                });
            }
            //editable: true
        };

        vm.rowIndex = 0;


        vm.updateThis = function (dataOri) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);
                //data["UN_LOAD_TIME"] = Date.(data.UN_LOAD_TIME);
                console.log(data);
                Dialog.confirm('Are want to process this data!!! "' + data.DYE_BATCH_NO + '.', 'Attention', ['Yes', 'No'])
               .then(function () {
                   var ld = data["LOAD_TIME"];
                   var uld = data["UN_LOAD_TIME"];
                   data["LQR_RATIO"] = "8";

                   if (!ld || !uld) {

                       config.appToastMsg("MULTI-005 Please select load & unload time");
                       return;
                   }
                   vm.showSplash = true;

                   if (parseInt(data["REQ_RE_PROC_TYPE_ID"]) == 0)
                       data["REQ_RE_PROC_TYPE_ID"] = "";

                   var _ldate = $filter('date')(data.LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                   data["LOAD_TIME"] = _ldate;

                   var _date = $filter('date')(data.UN_LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                   data["UN_LOAD_TIME"] = _date;

                   var _fdate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                   data["PROD_DT"] = _fdate;

                   return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBarcodeBatchProd').then(function (res) {

                       if (res.success === false) {
                           vm.errors = res.errors;
                       }
                       else {

                           res['data'] = angular.fromJson(res.jsonStr);
                           config.appToastMsg(res.data.PMSG);
                           vm.form = { PROD_DT: vm.today };
                           vm.BatchProgramList = [];
                           vm.BatchFabricList = [];
                           //vm.searchBatchInfo();
                           vm.showSplash = false;
                           angular.element('#DYE_BATCH_NO').focus();
                       }
                   });
               });
            }
        }


        vm.pauseThis = function (dataOri, id) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);
                //data["UN_LOAD_TIME"] = Date.(data.UN_LOAD_TIME);
                console.log(data);
                Dialog.confirm('Are want to process this data!!! "' + data.DYE_BATCH_NO + '.', 'Attention', ['Yes', 'No'])
               .then(function () {
                   vm.showSplash = true;

                   data["DFCT_TYPE_ID"] = id;
                   data["DYE_BT_STS_TYPE_ID"] = 5;
                   data["LQR_RATIO"] = "8";
                   data["LK_CHQ_RL_STS_ID"] = null;

                   if (parseInt(data["REQ_RE_PROC_TYPE_ID"]) == 0)
                       data["REQ_RE_PROC_TYPE_ID"] = "";

                   var _ldate = $filter('date')(data.LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                   data["LOAD_TIME"] = _ldate;

                   var _date = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                   data["UN_LOAD_TIME"] = _date;

                   var _fdate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                   data["PROD_DT"] = _fdate;

                   return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBarcodeBatchProd').then(function (res) {

                       if (res.success === false) {
                           vm.errors = res.errors;
                       }
                       else {

                           res['data'] = angular.fromJson(res.jsonStr);
                           config.appToastMsg(res.data.PMSG);
                           vm.form = { PROD_DT: vm.today };
                           vm.BatchProgramList = [];
                           vm.BatchFabricList = [];
                           //vm.searchBatchInfo();
                           vm.showSplash = false;
                           angular.element('#DYE_BATCH_NO').focus();
                       }
                   });
               });
            }
        }

        vm.resumeThis = function (dataOri) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(data);

                vm.showSplash = true;

                data["LQR_RATIO"] = "8";
                data["DYE_BT_STS_TYPE_ID"] = 4;
                data["IS_EDT_LOCKED"] = 'Y';
                data["DYE_BT_PROD_ID"] = 0;

                var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                data["LOAD_TIME"] = _ldate;

                data["UN_LOAD_TIME"] = null;

                if (parseInt(data["REQ_RE_PROC_TYPE_ID"]) == 0)
                    data["REQ_RE_PROC_TYPE_ID"] = "";

                var _fdate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                data["PROD_DT"] = _fdate;

                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBarcodeBatchProd').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.form = { PROD_DT: vm.today };
                        vm.BatchProgramList = [];
                        vm.BatchFabricList = [];
                        //vm.searchBatchInfo();
                        vm.showSplash = false;
                        angular.element('#DYE_BATCH_NO').focus();
                    }
                });
            }
        }

        vm.callMaintenance = function (dataOri) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(data);
                Dialog.confirm('Are want to process this data!!! "' + data.DYE_BATCH_NO + '.', 'Attention', ['Yes', 'No'])
               .then(function () {
                   vm.showSplash = true;

                   return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/PreventionalMaintenance').then(function (res) {

                       if (res.success === false) {
                           vm.errors = res.errors;
                       }
                       else {

                           res['data'] = angular.fromJson(res.jsonStr);
                           config.appToastMsg(res.data.PMSG);
                           vm.form = { PROD_DT: vm.today };
                           vm.BatchProgramList = [];
                           vm.BatchFabricList = [];
                           //vm.searchBatchInfo();
                           vm.showSplash = false;
                           angular.element('#DYE_BATCH_NO').focus();
                       }
                   });
               });
            }
        }


        vm.submitAll = function (dataOri, id) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(data);
                Dialog.confirm('Are want to process this data!!! "' + data.DYE_BATCH_NO + '.', 'Attention', ['Yes', 'No'])
               .then(function () {
                   vm.showSplash = true;

                   if (data.IS_ROLL_OK == 'Y') {
                       id = id == 6 ? 7 : id;
                   }

                   data["LQR_RATIO"] = "8";


                   data["DYE_BT_STS_TYPE_ID"] = id;

                   if (parseInt(data["REQ_RE_PROC_TYPE_ID"]) == 0)
                       data["REQ_RE_PROC_TYPE_ID"] = "";

                   if (id == 4) {
                       var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                       data["LOAD_TIME"] = _ldate;
                       data["UN_LOAD_TIME"] = null;
                       data["DYE_BT_PROD_ID"] = 0;

                   }
                   else {

                       var _ldate = $filter('date')(data.LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss');
                       data["LOAD_TIME"] = _ldate;

                       var _date = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                       data["UN_LOAD_TIME"] = _date;
                   }

                   var _fdate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                   data["PROD_DT"] = _fdate;

                   return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBarcodeBatchProd').then(function (res) {

                       if (res.success === false) {
                           vm.errors = res.errors;
                       }
                       else {

                           res['data'] = angular.fromJson(res.jsonStr);
                           config.appToastMsg(res.data.PMSG);
                           vm.form = { PROD_DT: vm.today };
                           vm.BatchProgramList = [];
                           vm.BatchFabricList = [];
                           //vm.searchBatchInfo();
                           vm.showSplash = false;
                           angular.element('#DYE_BATCH_NO').focus();
                       }
                   });
               });
            }
        };


    }


})();

