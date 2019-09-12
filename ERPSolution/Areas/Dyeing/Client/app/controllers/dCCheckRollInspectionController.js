(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCCheckRollInspectionController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', '$filter', '$modal', DCCheckRollInspectionController]);
    function DCCheckRollInspectionController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog, $filter, $modal) {

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
        if ($stateParams.pDYE_BATCH_NO)
            vm.form = { REQ_STORE_ID: 0, ISS_STORE_ID: 0, STR_REQ_BY: cur_user_id, STR_REQ_DT: vm.today, RF_ACTN_VIEW: 0, DYE_RE_PROC_TYPE_ID: 1, DYE_BATCH_NO: ($stateParams.pDYE_BATCH_NO.substring(0, 10) || '') };
        else
            vm.form = { REQ_STORE_ID: 0, ISS_STORE_ID: 0, STR_REQ_BY: cur_user_id, STR_REQ_DT: vm.today, RF_ACTN_VIEW: 0, DYE_RE_PROC_TYPE_ID: 1, DYE_BATCH_NO: '' };
        vm.formItem = { 'QTY_MOU_ID': '3', 'CENTRAL_STK': 0, 'SUB_STK': 0 };
        //vm.form.DYE_BATCH_NO = $stateParams.pDYE_BATCH_NO;
        angular.element('#DYE_BATCH_NO').focus();

        vm.form.IS_FINALIZED = "Y";

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetDefectList(), ReceiveItemList(), getRollStatusList(), getDfProcessTypeData(), GetMultipleDeptList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.clearData = function () {
            $state.go('CheckRollInspection', { pDYE_BATCH_NO: '' }, { reload: true });
        }

        $scope.PROD_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PROD_DT_LNopened = true;
        }


        function GetMultipleDeptList() {
            return vm.getMultipleDeptData = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        var MULTI_DEPT_LST = "";
                        var lst = _.filter(vm.defectList, function (x) { return x.LAST_UPDATED_BY > 0 && x.IS_MULTI_DEPT_ALLOW == 'Y' })

                        for (var i = 0; i < lst.length; i++) {
                            if (i > 0)
                                MULTI_DEPT_LST = RF_FB_DFCT_TYPE_LST + "," + lst[i].MULTI_DEPT_LST;
                            else
                                MULTI_DEPT_LST = lst[i].MULTI_DEPT_LST;
                        }

                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetRespDeptListByID?pMULTI_DEPT_LST=' + (MULTI_DEPT_LST || '')).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.getMultipleDept = function () {
            vm.getMultipleDeptData.read();
        }

        vm.openDyeRollInspDtl = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'DyeRollInspDtl.html',
                controller: function ($scope, $modalInstance, DyeingDataService) {

                    $scope.dtFormat = config.appDateFormat;
                    $scope.TimeFormat = config.appTimeFormat;
                    $scope.today = new Date();

                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
                    };

                    $scope.CHK_ROLL_DT_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.CHK_ROLL_DT_LNopened = true;
                    }

                    $scope.formItemList = {};
                    buyerAccGroupList();

                    function loadAllHold() {

                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetHoldQCList?pMC_BYR_ACC_GRP_ID=' + $scope.MC_BYR_ACC_GRP_ID + '&pORDER_NO=' + ($scope.STYLE_ORDER_NO || "") + '&pDYE_BATCH_NO=' + ($scope.DYE_BATCH_NO || "") + "&pCHK_ROLL_DT=" + (qcDate || "")).then(function (res) {

                            $scope.formItemList = res;
                        });
                    }

                    function colorList() {
                        DyeingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (resC) {
                            $scope.colorList = resC
                        });
                    }

                    function buyerAccGroupList() {
                        return $scope.buyerAccGrpList = {
                            optionLabel: "--- Select Buyer Group ---",
                            filter: "contains",
                            autoBind: true,
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                                            e.success(res);
                                        }, function (err) {
                                            console.log(err);
                                        });
                                    }
                                }
                            },
                            dataTextField: "BYR_ACC_GRP_NAME_EN",
                            dataValueField: "MC_BYR_ACC_GRP_ID"
                        };
                    }

                    $scope.clearAll = function () {
                        $scope.MC_BYR_ACC_GRP_ID = '';
                        $scope.COLOR_NAME_EN = '';
                        $scope.STYLE_ORDER_NO = '';
                        $scope.QC_DT = '';
                    }

                    $scope.getRollDtl = function () {

                        //var qcDate = $filter('date')($scope.CHK_ROLL_DT, 'yyyy-MM-ddTHH:mm:ss');

                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetHoldQCList?pMC_BYR_ACC_GRP_ID=' + $scope.MC_BYR_ACC_GRP_ID + '&pORDER_NO=' + ($scope.STYLE_ORDER_NO || "") + '&pDYE_BATCH_NO=' + ($scope.DYE_BATCH_NO || "") + "&pCHK_ROLL_DT=" + ($scope.CHK_ROLL_DT || "")).then(function (res) {
                            $scope.formItemList = res;
                        });
                    }

                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.selectRollNo = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.submitData = function (dataOri, id) {
                        console.log(dataOri);
                        var data = angular.copy(dataOri);

                        if (id == 1 && data.QC_NOTE.length <= 0) {
                            alert("Please Select QC Note!!!");

                            return;
                        }
                        data.KNT_QC_STS_TYPE_ID = id;

                        return DyeingDataService.saveDataByFullUrl(data, '/api/knit/KnitPlanRollInsp/Save').then(function (res) {
                            if (res.success === false) {
                                vm.errors = res.errors;
                            }
                            else {

                                res['data'] = angular.fromJson(res.jsonStr);

                                $scope.alerts = [];
                                $scope.closeAlert = function (index) {
                                    $scope.alerts.splice(index, 1);
                                };

                                var typ = "";
                                var msgtyp = res.data.PMSG.substring(0, 9);
                                var mesg = res.data.PMSG.substring(9, res.data.PMSG.length);

                                if (msgtyp == "MULTI-001")
                                    typ = "success";
                                else
                                    typ = "danger";

                                $scope.alerts.push({
                                    type: typ, msg: mesg
                                });
                                loadAllHold()

                            }
                        }).catch(function (message) {
                            exception.catcher('XHR loading Failded')(message);
                        });
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
            });

            modalInstance.result.then(function (item) {

                if (item.DYE_BATCH_NO.length > 0) {
                    var lst = item.DYE_BATCH_NO.split('/');
                    if (lst.length > 0)
                        vm.form.DYE_BATCH_NO = lst[0];
                    else
                        vm.form.DYE_BATCH_NO = item.DYE_BATCH_NO;

                    vm.searchBatchInfo(1);
                }
            });

        };

        vm.searchBatchInfo = function (ScanValue) {
            if (ScanValue == "1")
                ScanValue = (vm.form.DYE_BATCH_NO || vm.form.FAB_ROLL_NO);
            if (ScanValue)
                if (ScanValue.length > 9) {

                    ScanValue = ScanValue.toUpperCase();
                    console.log(ScanValue);
                    vm.showSplash = true;
                    GetDefectList();
                    DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (ScanValue.trim() || "")).then(function (res) {

                        var prdList = _.filter(res.prd, function (x) { return x.DYE_BT_STS_TYPE_ID > 5 })

                        if (prdList.length > 0) {

                            if (prdList[0].RF_DY_DFCT_TYPE_LST.length > 2) {
                                var dt = prdList[0].RF_DY_DFCT_TYPE_LST.split(',');

                                for (var i = 0; i < vm.defectList.length; i++) {
                                    if (dt.indexOf(vm.defectList[i].RF_FB_DFCT_TYPE_ID.toString()) >= 0) {
                                        vm.defectList[i].LAST_UPDATED_BY = "1";
                                    }
                                }
                            }
                            else {
                                for (var i = 0; i < vm.defectList.length; i++) {
                                    if (parseInt(prdList[0].RF_DY_DFCT_TYPE_LST) == parseInt(vm.defectList[i].RF_FB_DFCT_TYPE_ID)) {
                                        vm.defectList[i].LAST_UPDATED_BY = "1";
                                    }
                                }
                            }

                            if (prdList[0].DYE_BT_STS_TYPE_ID > 6 && prdList[0].IS_FINALIZED == "Y") {

                                config.appToastMsg("MULTI-005 This is a complete batch!");
                                vm.form = {}
                                angular.element('#DYE_BATCH_NO').focus();
                                //vm.showSplash = false;
                                //return;
                            }
                            vm.form.DYE_BT_PROD_ID = prdList[0].DYE_BT_PROD_ID;
                            vm.form.REMARKS = prdList[0].REMARKS;

                            vm.BatchProgramList = prdList;
                            vm.BatchFabricList = res.fab;
                            if (res.batch.length > 0) {
                                console.log(res.batch);
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
                                vm.form.LQR_RATIO = "1:" + res.batch[0].LQR_RATIO;
                                vm.form.DYE_MTHD_NAME = res.batch[0].DYE_MTHD_NAME;
                                vm.form.UN_HOLD_DT = res.batch[0].UN_HOLD_DT;
                                vm.form.CHK_ROLL_DT = res.batch[0].CHK_ROLL_DT;

                                vm.form.DF_PROC_TYP_LST = res.batch[0].DF_PROC_TYP_LST ? res.batch[0].DF_PROC_TYP_LST.split(',') : [];
                                vm.form.CK_ROLL_RCV_DT = (res.batch[0].CK_ROLL_RCV_DT || null);
                                vm.form.CK_ROLL_FIN_DT = (res.batch[0].CK_ROLL_FIN_DT || null);
                                vm.form.LK_CHQ_RL_STS_ID = res.batch[0].LK_CHQ_RL_STS_ID;
                                //vm.form.DYE_BATCH_NO_LST = res.batch[0].DYE_LOT_NO == null ? res.batch[0].DYE_BATCH_NO : res.batch[0].DYE_BATCH_NO + "-" + res.batch[0].DYE_LOT_NO;
                                vm.form.DYE_BATCH_NO_LST = res.batch[0].DYE_BATCH_NO;
                                vm.form.ACT_BATCH_QTY_LST = res.batch[0].ACT_BATCH_QTY + " Kg";
                            }
                            else {

                                config.appToastMsg("MULTI-005 Batch not found!");
                                vm.form = {}
                                angular.element('#DYE_BATCH_NO').focus();
                            }
                        }
                        else {

                            config.appToastMsg("MULTI-005 Not Ready for Check-Roll Process!");
                            vm.form = {}
                            angular.element('#DYE_BATCH_NO').focus();
                        }
                        vm.showSplash = false;
                    });
                }
        }

        function getRollStatusList() {

            DyeingDataService.LookupListData(111).then(function (res) {
                vm.rollStatusList = res
            });
        };

        function GetDefectList() {

            DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=708&pOption=3002').then(function (res) {
                //DyeingDataService.getDataByFullUrl('/api/common/getDyeDfctTypeList').then(function (res) {
                vm.defectList = res
            });
        };


        function getDfProcessTypeData() {
            return vm.processTypeList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        //DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=709&pOption=3002').then(function (res) {
                        DyeingDataService.getDataByUrl('/DFProcessType/SelectAll').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
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

                { field: "DYE_MACHINE_NO", title: "M/C No", type: "string", width: "10%", },
                //{ field: "CK_ROLL_RCV_DT", title: "M/C No", type: "string", width: "10%", template: "#if(CK_ROLL_FIN_DT === null) {#<div class='customClass'>#:null#</div>#} else{##:kendo.toString(new Date(CK_ROLL_FIN_DT), 'dd-MM-yy hh:mmtt')##}#" },
                { field: "LOAD_TIME", title: "Load Time", type: "date", template: "#= kendo.toString(new Date(LOAD_TIME), 'dd-MM-yy hh:mmtt') #", width: "10%" },
                { field: "UN_LOAD_TIME", title: "Unload Time", type: "date", template: "#= kendo.toString(new Date(UN_LOAD_TIME), 'dd-MM-yy hh:mmtt') #", width: "10%" },
                { field: "CK_ROLL_RCV_DT", title: "BT. Rcv", type: "date", template: "#if(CK_ROLL_RCV_DT === null) {#<div class='customClass'>#:null#</div>#} else{##:kendo.toString(new Date(CK_ROLL_RCV_DT), 'dd-MM-yy hh:mmtt')##}#", width: "10%" },
                { field: "CK_ROLL_FIN_DT", title: "DF. Rcv", type: "date", template: "#if(CK_ROLL_FIN_DT === null) {#<div class='customClass'>#:null#</div>#} else{##:kendo.toString(new Date(CK_ROLL_FIN_DT), 'dd-MM-yy hh:mmtt')##}#", width: "10%" },

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

        vm.updateAll = function (dataOri, id) {
            Dialog.confirm('Are you want to process this action!!!".', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     if (fnValidateSub2() == true) {
                         var data = angular.copy(dataOri);

                         data.DYE_BT_STS_TYPE_ID = 6;

                         if (id == 0) {
                             var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                             data["CK_ROLL_RCV_DT"] = _ldate;
                         }
                         else if (id == 1) {
                             var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                             data["CK_ROLL_FIN_DT"] = _ldate;
                         }
                         else if (id == 2) {
                             var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                             data["CK_ROLL_FIN_DT"] = _ldate;


                             var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                             data["CHK_ROLL_DT"] = _ldate;

                             data["LQR_RATIO"] = "8";
                             data["LK_CHQ_RL_STS_ID"] = 561;
                             data["IS_ROLL_OK"] = "Y";
                             data["REMARKS"] = "By Pass QC";
                             data.DYE_BT_STS_TYPE_ID = 7;
                         }

                         data["DF_PROC_TYP_LST"] = data.DF_PROC_TYP_LST ? data.DF_PROC_TYP_LST.join(',') : '0';

                         return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBatchCheck').then(function (res) {

                             if (res.success === false) {
                                 vm.errors = res.errors;
                             }
                             else {

                                 res['data'] = angular.fromJson(res.jsonStr);
                                 config.appToastMsg(res.data.PMSG);
                                 $state.go('CheckRollInspection', {}, { reload: true });
                             }
                         });
                     }

                 });
        }


        vm.submitAll = function (dataOri, id) {

            Dialog.confirm('Are you want to process this action!!!".', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     if (vm.form.DYE_BT_PROD_ID > 0) {
                         var data = angular.copy(dataOri);

                         var RF_DY_DFCT_TYPE_LST = "";
                         var lst = _.filter(vm.defectList, function (x) { return x.LAST_UPDATED_BY > 0 })

                         console.log(lst);

                         for (var i = 0; i < lst.length; i++) {

                             if (i > 0)
                                 RF_DY_DFCT_TYPE_LST = RF_DY_DFCT_TYPE_LST + "," + lst[i].RF_FB_DFCT_TYPE_ID;
                             else
                                 RF_DY_DFCT_TYPE_LST = lst[i].RF_FB_DFCT_TYPE_ID;
                         }

                         data['RF_DY_DFCT_TYPE_LST'] = !RF_DY_DFCT_TYPE_LST ? '0' : RF_DY_DFCT_TYPE_LST;

                         if (data.LK_CHQ_RL_STS_ID == 563) {
                             var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                             data["UN_HOLD_DT"] = _ldate;
                         }
                         else {

                             var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                             data["CHK_ROLL_DT"] = _ldate;
                         }
                         data["LQR_RATIO"] = "8";
                         data["LK_CHQ_RL_STS_ID"] = id;
                         //data["IS_FINALIZED"] = "N";

                         if (id == 561) {
                             //Dialog.confirm('Do You Want to Finalized!!!".', 'Attention', ['Yes', 'No'])
                             //     .then(function () {
                             //         data["IS_FINALIZED"] = "Y";
                             //     });
                             data["IS_ROLL_OK"] = "Y";
                             data.DYE_BT_STS_TYPE_ID = 7;
                         }
                         else if (id == 562) {
                             data["IS_ROLL_OK"] = "N";

                             //Dialog.confirm('Do You Want to Finalized!!!".', 'Attention', ['Yes', 'No'])
                             //     .then(function () {
                             //         data["IS_FINALIZED"] = "Y";
                             //     });

                             data.DYE_BT_STS_TYPE_ID = 8;
                             if (RF_DY_DFCT_TYPE_LST.length < 1) {
                                 config.appToastMsg("MULTI-005 Please select at least 1 defect!!!");
                                 return;
                             }

                         }
                         else if (id == 563) {
                             data["IS_ROLL_OK"] = "H";
                             data.DYE_BT_STS_TYPE_ID = 6;
                             if (RF_DY_DFCT_TYPE_LST.length < 1) {
                                 config.appToastMsg("MULTI-005 Please select at least 1 defect!!!");
                                 return;
                             }

                         }
                         else {
                             //Dialog.confirm('Do You Want to Finalized!!!".', 'Attention', ['Yes', 'No'])
                             //     .then(function () {
                             //         data["IS_FINALIZED"] = "Y";
                             //     });
                             data["IS_ROLL_OK"] = "Y";
                             data.DYE_BT_STS_TYPE_ID = 7;
                             if (RF_DY_DFCT_TYPE_LST.length < 1) {
                                 config.appToastMsg("MULTI-005 Please select at least 1 defect!!!");
                                 return;
                             }
                         }

                         data["DF_PROC_TYP_LST"] = data.DF_PROC_TYP_LST ? data.DF_PROC_TYP_LST.join(',') : '0';

                         return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBarcodeBatchQC').then(function (res) {

                             if (res.success === false) {
                                 vm.errors = res.errors;
                             }
                             else {
                                 res['data'] = angular.fromJson(res.jsonStr);
                                 config.appToastMsg(res.data.PMSG);
                                 vm.form = {};
                                 vm.BatchProgramList = new kendo.data.DataSource({
                                     data: []
                                 });
                                 vm.BatchFabricList = new kendo.data.DataSource({
                                     data: []
                                 });
                                 $state.go('CheckRollInspection', { pDYE_BATCH_NO: '' }, { reload: true });
                             }
                         });
                     }
                 });
        }

    }


})();

