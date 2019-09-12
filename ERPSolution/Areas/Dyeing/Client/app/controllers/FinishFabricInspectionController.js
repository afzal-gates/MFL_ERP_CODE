
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
    angular.module('multitex.dyeing').controller('FinishFabricInspectionController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', '$filter', '$modal', FinishFabricInspectionController]);
    function FinishFabricInspectionController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog, $filter, $modal) {

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

        vm.form = { DYE_BATCH_NO: 'B-18091297', DF_BT_STS_TYPE_ID: 1, RF_ACTN_STATUS_ID: 1, QC_BY: cur_user_id, QC_DT: vm.today, RF_ACTN_VIEW: 0, my_loop: 0 };

        vm.RollProductionList = [];

        vm.checkRollResultList = [];


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetDefectList(), ReceiveItemList(), getRollStatusList(), getUserData(), getShiftData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.QC_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.QC_DT_LNopened = true;
        }


        function getUserData() {

            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectDfQcUser?pLK_DF_EMP_TYPE_ID=' + (vm.form.LK_DF_EMP_TYPE_ID || 720)).then(function (res) {
                console.log(res);
                var lst = [{
                    EMP_FULL_NAME_EN: '-- Select In-Charge --',
                    HR_EMPLOYEE_ID: -1
                }].concat(res);
                vm.userList = lst;
            });
        }

        function getShiftData() {

            DyeingDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
                var lst = [{
                    SCHEDULE_NAME_EN: '-- Select Shift --',
                    HR_SCHEDULE_H_ID: -1
                }].concat(res);
                vm.shiftList = lst;
            });
        }

        vm.setRoll = function (item) {
            vm.form.FAB_ROLL_NO = item.FAB_ROLL_NO;
        }

        vm.openDFRollInsp = function (objData) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'DFRollInsp.html',
                controller: function ($scope, $modalInstance, DyeingDataService, formData) {
                    //console.log(formData);
                    $scope.dtFormat = config.appDateFormat;
                    $scope.TimeFormat = config.appTimeFormat;
                    $scope.today = new Date();
                    $scope.form = formData ? formData : {};

                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
                    };

                    $scope.CHK_ROLL_DT_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.CHK_ROLL_DT_LNopened = true;
                    }

                    $scope.formItemList = {};
                    loadInspecPage();
                    defectList();
                    getFabricFaultyList();

                    function getFabricFaultyList() {

                        return $scope.faultList = {
                            optionLabel: "-- Select Faults --",
                            filter: "contains",
                            autoBind: true,
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetFabricFaultyList').then(function (res) {
                                            e.success(res);
                                        }, function (err) {
                                            console.log(err);
                                        });
                                    }
                                }
                            },
                            dataTextField: "FLT_APRN_TYPE_NAME",
                            dataValueField: "RF_FB_FLT_APRN_TYPE_ID"
                        };
                    };

                    function defectList() {

                        if (formData.items.length == 0)

                            DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=708&pOption=3002').then(function (res) {

                                $scope.formItemList = res;
                                $scope.defectList = _.filter(res, function (x) { return x.RF_FB_DFCT_TYPE_ID == 0 });
                            });

                            //DyeingDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/DefectTypeList').then(function (res) {
                            //    $scope.formItemList = res;
                            //    $scope.defectList = _.filter(res, function (x) { return x.RF_FB_DFCT_TYPE_ID == 0 });
                            //});
                        else {
                            $scope.formItemList = formData.items;
                            DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=708&pOption=3002').then(function (res) {
                                $scope.defectList = res;
                            });

                            //DyeingDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/DefectTypeList').then(function (res) {
                            //    $scope.defectList = _.filter(res, function (x) { return x.RF_FB_DFCT_TYPE_ID == 0 });
                            //});
                        }
                    }

                    function loadInspecPage() {
                        var FAB_ROLL_NO = formData.FAB_ROLL_NO;

                        if (formData.items.length == 0)
                            DyeingDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/RollDelails?pFAB_ROLL_NO=' + (FAB_ROLL_NO) + '&pOption=3010').then(function (res) {
                                $scope.form = res;
                                console.log(res);
                                if (!res.FAB_ROLL_NO) {
                                    $scope.alerts = [];
                                    $scope.closeAlert = function (index) {
                                        $scope.alerts.splice(index, 1);
                                    };

                                    $scope.alerts.push({
                                        type: "danger", msg: "QC Status already updated!!!"
                                    });
                                }
                            });

                    }


                    $scope.addOne = function (item) {
                        if (item.DFCT_QTY)
                            item.DFCT_QTY += 1;
                        else
                            item.DFCT_QTY = 1;

                        console.log(item);
                        var chk = _.filter($scope.defectList, function (x) { return x.RF_FB_DFCT_TYPE_ID === item.RF_FB_DFCT_TYPE_ID }).length;
                        item.EARN_PT = item.DFCT_QTY * item.STD_PT;
                        item.KNT_FAB_ROLL_ID = $scope.form.KNT_FAB_ROLL_ID;
                        if (chk > 0)
                            return;
                        else {
                            $scope.defectList.splice(1, "0", item);
                        }
                    }

                    $scope.subOne = function (item) {
                        if (item.DFCT_QTY > 0)
                            item.DFCT_QTY -= 1;

                        if (item.DFCT_QTY == 0) {

                            var index = $scope.defectList.indexOf(item);
                            $scope.defectList.splice(index, 1);
                        }
                    }

                    $scope.updateDDL = function (item) {
                        item.EARN_PT = item.DFCT_QTY * item.STD_PT;
                        console.log(item);

                        item.KNT_FAB_ROLL_ID = $scope.form.KNT_FAB_ROLL_ID;
                        if (item.DFCT_QTY > 0) {
                            $scope.defectList.splice(1, "0", item);
                        }
                        else {
                            var index = $scope.defectList.indexOf(item);
                            $scope.defectList.splice(index, 1);
                        }
                        console.log("WW");
                    }



                    $scope.ClaculateDefectResult = function () {

                        var total = 0;

                        for (var i = 0; i < $scope.formItemList.length; i++) {
                            var item = $scope.formItemList[i];
                            if (item.IS_CALC_APLY == 'Y' && item.EARN_PT > 0) {
                                console.log(item)
                                total = total + ((item.DFCT_QTY || 1) * item.STD_PT);
                            }
                        }

                        $scope.form.TOTAL_PT = total.toFixed(2);
                        $scope.form.GRADE_PT = ((total * 100 * $scope.form.FIN_GSM) / ($scope.form.FAB_ROLL_WT * 1000 * 1.2)).toFixed(2);

                        DyeingDataService.getDataByFullUrl('/api/knit/KnitPlanRollInsp/GetRollQCGRD?pGRADE_PT=' + $scope.form.GRADE_PT).then(function (res) {
                            $scope.form.GRADE_NO = res.QC_GRD_NAME;
                            $scope.form.LK_QC_GRD_ID = res.LK_QC_GRD_ID;
                        });
                        //vm.form.GRADE_NO = total > 70 ? 'F' : total > 50 ? 'C' : total > 30 ? 'B' : total > 0 ? 'A' : 'F';

                    }

                    $scope.checkWeight = function () {
                        if (parseFloat($scope.form.ACT_ROLL_WT) > parseFloat($scope.form.FAB_ROLL_WT))
                            $scope.form.ACT_ROLL_WT = 0;
                        else
                            $scope.form.ACT_ROLL_LOSS = (((parseFloat($scope.form.FAB_ROLL_WT) - parseFloat($scope.form.ACT_ROLL_WT)) * 100) / parseFloat($scope.form.FAB_ROLL_WT)).toFixed(2);
                    }

                    $scope.clearAll = function () {
                        $scope.MC_BYR_ACC_GRP_ID = '';
                        $scope.COLOR_NAME_EN = '';
                        $scope.STYLE_ORDER_NO = '';
                        $scope.QC_DT = '';
                    }

                    $scope.cancel = function (data) {
                        data["items"] = angular.copy($scope.formItemList);
                        $modalInstance.close(data);
                    }

                    $scope.fillCheckRollData = function (data) {
                        //for (var i = 0; i < $scope.formItemList.length; i++)
                        //{
                        //    $scope.formItemList[i]["FAB_ROLL_NO"] = $scope.form.FAB_ROLL_NO;
                        //    $scope.formItemList[i]["FAB_ROLL_WT"] = $scope.form.FAB_ROLL_WT;
                        //}
                        data["items"] = angular.copy($scope.formItemList);
                        $modalInstance.close(data);
                    }
                },
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    formData: function () { return objData; }
                }
            });

            modalInstance.result.then(function (item) {

                if (item.FAB_ROLL_NO != "") {
                    var obj = _.filter(vm.checkRollResultList, function (x) { return x.FAB_ROLL_NO == item.FAB_ROLL_NO });
                    if (obj.length == 0) {
                        var idx = vm.checkRollResultList.length;
                        item['DF_ROLL_SL'] = idx + 1;
                        vm.checkRollResultList.splice(idx, "0", item);
                    }
                    else {
                        var index = $scope.defectList.indexOf(item);
                        vm.checkRollResultList.splice(index, 1);
                        vm.checkRollResultList.splice(index, "0", item);
                    }
                }
            });

        };


        vm.openDyeRollInspDtl = function () {
            alert("");
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

        vm.searchRoll = function () {
            vm.form.my_loop = vm.form.my_loop + 1;
            var obj = _.filter(vm.RollProductionList, function (x) { return x.FAB_ROLL_NO === vm.form.FAB_ROLL_NO })[0];
            var idx = vm.RollProductionList.indexOf(obj);
            vm.RollProductionList[idx].KNT_JOB_CRD_H_ID = vm.form.my_loop;

            var item = { FAB_ROLL_NO: vm.form.FAB_ROLL_NO, items: [] };
            vm.openDFRollInsp(item);

        }

        vm.loadModalByData = function (item) {
            console.log(item);
            vm.openDFRollInsp(item);
        }

        vm.searchBatchInfo = function (pPageNo) {
            if ((vm.form.DYE_BATCH_NO && vm.form.DYE_BATCH_NO.length > 0) || (vm.form.FAB_ROLL_NO && vm.form.FAB_ROLL_NO.length > 0)) {
                vm.showSplash = true;
                GetDefectList();

                DyeingDataService.getDataByUrl('/DCBatchProgramRequisition/GetBatchForDF?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {
                    console.log(res);


                    vm.form.DYE_STR_REQ_H_ID = res[0].DYE_STR_REQ_H_ID;
                    vm.form.STR_REQ_NO = res[0].STR_REQ_NO;
                    vm.form.STR_REQ_DT = res[0].STR_REQ_DT;
                    vm.form.DYE_MACHINE_ID = res[0].DYE_MACHINE_ID;
                    vm.form.BUYER_NAME_EN = res[0].BUYER_NAME_EN;
                    vm.form.STYLE_NO = res[0].STYLE_NO;
                    vm.form.ORDER_NO = res[0].ORDER_NO;
                    vm.form.COLOR_NAME_EN = res[0].COLOR_NAME_EN;
                    vm.form.LD_RECIPE_NO = res[0].LD_RECIPE_NO;
                    vm.form.COLOR_GRP_NAME_EN = res[0].COLOR_GRP_NAME_EN;
                    vm.form.MC_LD_RECIPE_H_ID = res[0].MC_LD_RECIPE_H_ID;
                    vm.form.LQR_RATIO = "1:" + res[0].LQR_RATIO;
                    vm.form.DYE_MTHD_NAME = res[0].DYE_MTHD_NAME;
                    vm.form.UN_HOLD_DT = res[0].UN_HOLD_DT;
                    vm.form.CHK_ROLL_DT = res[0].CHK_ROLL_DT;
                    vm.form.CK_ROLL_RCV_DT = res[0].CK_ROLL_RCV_DT;
                    vm.form.CK_ROLL_FIN_DT = res[0].CK_ROLL_FIN_DT;
                    vm.form.LK_CHQ_RL_STS_ID = res[0].LK_CHQ_RL_STS_ID;
                    vm.form.DYE_BATCH_NO_LST = res[0].DYE_BATCH_NO;
                    vm.form.DYE_LOT_NO = res[0].DYE_LOT_NO;
                    vm.form.FAB_COLOR_ID = res[0].FAB_COLOR_ID;
                    vm.form.DYE_BT_CARD_H_ID = res[0].DYE_BT_CARD_H_ID;
                    vm.form.ACT_BATCH_QTY_LST = res[0].ACT_BATCH_QTY + " Kg";

                    DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchFabricByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {
                        vm.BatchFabricList = res;
                    });

                    DyeingDataService.getDataByUrl('/GreyFabReq/getFinishFabricInsRollByBatchID?pDYE_BT_CARD_H_ID=' + res[0].DYE_BT_CARD_H_ID).then(function (res) {
                        vm.RollProductionList = res;
                    });
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

            DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=709&pOption=3002').then(function (res) {
                vm.defectList = res
            });

            //DyeingDataService.getDataByFullUrl('/api/common/getDyeDfctTypeList').then(function (res) {
            //    vm.defectList = res
            //});
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
                { field: "FABRIC_GROUP_NAME", title: "Fab Grp", type: "string", width: "10%", },
                { field: "FAB_TYPE_SNAME", title: "Fabric Type", type: "string", width: "10%" },
                { field: "FIB_COMP_NAME", title: "Fiber Comp.", type: "string", width: "15%" },
                { field: "KNT_YRN_LOT", title: "Yarn Lot", type: "string", width: "35%" },
                { field: "RQD_QTY", title: "Reqd. Qty.", type: "decimal", width: "10%" },
                { field: "FIN_QTY", title: "Finish Qty", type: "decimal", width: "10%" },
                { field: "RQD_QTY", title: "Blance Type", type: "decimal", width: "10%" },

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
                { field: "LOAD_TIME", title: "Load Time", type: "date", template: "#= kendo.toString(new Date(LOAD_TIME), 'dd-MM-yy hh:mmtt') #", width: "10%" },
                { field: "UN_LOAD_TIME", title: "Unload Time", type: "date", template: "#= kendo.toString(new Date(UN_LOAD_TIME), 'dd-MM-yy hh:mmtt') #", width: "10%" },
                { field: "CK_ROLL_RCV_DT", title: "BT. Rcv", type: "date", template: "#= kendo.toString(new Date(CK_ROLL_RCV_DT), 'dd-MM-yy hh:mmtt') #", width: "10%" },
                { field: "CK_ROLL_FIN_DT", title: "DF. Rcv", type: "date", template: "#= kendo.toString(new Date(CK_ROLL_FIN_DT), 'dd-MM-yy hh:mmtt') #", width: "10%" },

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
                         if (id == 0) {
                             var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                             data["CK_ROLL_RCV_DT"] = _ldate;
                         }
                         else if (id == 1) {
                             var _ldate = $filter('date')(new Date(), 'yyyy-MM-ddTHH:mm:ss');
                             data["CK_ROLL_FIN_DT"] = _ldate;
                         }

                         data.DYE_BT_STS_TYPE_ID = 6;

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
                     if (vm.checkRollResultList.length > 0) {
                         var data = angular.copy(dataOri);

                         var lst = _.filter(vm.defectList, function (x) { return x.LAST_UPDATED_BY > 0 })

                         var list = vm.checkRollResultList[0].items;
                         list["KNT_FAB_ROLL_ID"] = vm.checkRollResultList[0].KNT_FAB_ROLL_ID;
                         //var list = _.filter(vm.checkRollResultList[0].items, function (x) { return x.KNT_FAB_ROLL_ID > 0 });

                         for (var i = 1; i < vm.checkRollResultList.length; i++) {
                             if (vm.checkRollResultList[i].KNT_FAB_ROLL_ID > 0) {
                                 var obj = vm.checkRollResultList[i].items;
                                 obj["KNT_FAB_ROLL_ID"] = vm.checkRollResultList[i].KNT_FAB_ROLL_ID;
                                 for (var j = 0; j < obj.length; j++) {
                                     list.push(obj[j]);
                                 }
                             }
                         }

                         console.log(list);

                         data["XML_QC_D"] = DyeingDataService.xmlStringShort(list.map(function (o) {
                             return {
                                 DF_ROLL_QC_PT_ID: o.DF_ROLL_QC_PT_ID == null ? 0 : o.DF_ROLL_QC_PT_ID,
                                 DF_FAB_QC_ROLL_ID: o.DF_FAB_QC_ROLL_ID == null ? 0 : o.DF_FAB_QC_ROLL_ID,
                                 RF_FB_DFCT_TYPE_ID: o.RF_FB_DFCT_TYPE_ID,
                                 KNT_FAB_ROLL_ID: o.KNT_FAB_ROLL_ID,
                                 DFCT_QTY: o.DFCT_QTY,
                                 EARN_PT: o.EARN_PT == null ? 0 : o.EARN_PT,
                             }
                         }));

                         var fList = _.filter(list, function (x) { return x.IS_CALC_APLY == 'N' && parseInt(x.DFCT_QTY) > 0 });

                         console.log(fList);

                         data["XML_QC_FLT"] = DyeingDataService.xmlStringShort(fList.map(function (o) {
                             return {
                                 DF_FAB_QC_ROLL_FA_ID: o.DF_FAB_QC_ROLL_FA_ID == null ? 0 : o.DF_FAB_QC_ROLL_FA_ID,
                                 DF_FAB_QC_ROLL_ID: o.DF_FAB_QC_ROLL_ID == null ? 0 : o.DF_FAB_QC_ROLL_ID,
                                 RF_FB_DFCT_TYPE_ID: o.RF_FB_DFCT_TYPE_ID,
                                 KNT_FAB_ROLL_ID: o.KNT_FAB_ROLL_ID,
                                 IS_MN_MJ: o.IS_MN_MJ
                             }
                         }));

                         data["XML_ROLL_D"] = DyeingDataService.xmlStringShort(vm.checkRollResultList.map(function (o) {
                             return {
                                 DF_FAB_QC_ROLL_ID: o.DF_FAB_QC_ROLL_ID == null ? 0 : o.DF_FAB_QC_ROLL_ID,
                                 DF_FAB_QC_RPT_ID: o.DF_FAB_QC_RPT_ID == null ? 0 : o.DF_FAB_QC_RPT_ID,
                                 KNT_FAB_ROLL_ID: o.KNT_FAB_ROLL_ID == null ? 0 : o.KNT_FAB_ROLL_ID,
                                 RQD_FIN_DIA: o.RQD_FIN_DIA,
                                 ACT_FIN_DIA: o.ACT_FIN_DIA,
                                 ACT_ROLL_WT: o.ACT_ROLL_WT,
                                 TOTAL_PT: o.TOTAL_PT == null ? 0 : o.TOTAL_PT,
                                 GRADE_PT: o.GRADE_PT == null ? 0 : o.GRADE_PT,
                                 LK_QC_GRD_ID: o.LK_QC_GRD_ID == null ? 0 : o.LK_QC_GRD_ID,
                                 ACT_FIN_GSM: o.ACT_FIN_GSM == null ? 0 : o.ACT_FIN_GSM,
                                 SHADE_GRP: o.SHADE_GRP,
                                 DF_ROLL_SL: o.DF_ROLL_SL == null ? 1 : o.DF_ROLL_SL,
                                 ROLL_NOTE: o.ROLL_NOTE,
                                 DF_FAB_GRP_ID: o.DF_FAB_GRP_ID,
                                 KNT_QC_STS_TYPE_ID: o.KNT_QC_STS_TYPE_ID == null ? 0 : o.KNT_QC_STS_TYPE_ID,
                                 RF_FB_FLT_APRN_TYPE_LST: o.RF_FB_FLT_APRN_TYPE_LST,
                             }
                         }));

                         return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DfFabInsp/Save').then(function (res) {

                             if (res.success === false) {
                                 vm.errors = res.errors;
                             }
                             else {
                                 res['data'] = angular.fromJson(res.jsonStr);
                                 config.appToastMsg(res.data.PMSG);
                                 vm.form = {};
                                 $state.go($state.current, {}, { reload: true });
                             }
                         });
                     }
                 });
        }

    }


})();


