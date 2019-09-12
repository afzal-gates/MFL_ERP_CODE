/// <reference path="dCBatchProgramAddRequisitionController.js" />
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCBatchProgramAddRequisitionController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', 'Dialog', DCBatchProgramAddRequisitionController]);
    function DCBatchProgramAddRequisitionController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, Dialog) {

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

        vm.form = formData.DYE_STR_REQ_H_ID ? formData : { STR_REQ_BY: cur_user_id, STR_REQ_DT: vm.today, RF_ACTN_VIEW: 0, DYE_RE_PROC_TYPE_ID: 1 };
        vm.formItem = { 'QTY_MOU_ID': '3', 'CENTRAL_STK': 0, 'SUB_STK': 0 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetItemCategoryList(), GetBatchList(), getdosingTemplateList(), getItemDataByCategory(),
                           GetReqSourceList(), GetMachineList(), ReceiveItemList(), GetStatusList(), GetCompanyList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        if ($stateParams.pADDITION_FLAG > 0) {
            //vm.form.DYE_RE_PROC_TYPE_ID = $stateParams.pADDITION_FLAG;
            vm.REPROCESS_FLAG = 1;
        }
        else {
            //vm.form["DYE_RE_PROC_TYPE_ID"] = 2;
        }

        //if (!vm.form.DYE_RE_PROC_TYPE_ID) {
        //    vm.form["DYE_RE_PROC_TYPE_ID"] = 2;
        //}


        vm.moveArray = function (item, idx) {
            if (idx >= 0) {
                var index = vm.recipeItemList.indexOf(item);
                vm.recipeItemList.splice(index, 1);
                vm.recipeItemList.splice(idx, "0", item);
                //vm.recipeItemList.sort();
                if (!$stateParams.pADDITION_FLAG)
                    vm.ACTION_DIS = 1;
            }
        }


        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        function GetReqSourceList() {
            vm.storeList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=3,4&pSC_USER_ID=' + cur_user_id).then(function (res) {
                            var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 512 });
                            e.success(list);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        function GetItemCategoryList() {

            DyeingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var list = _.filter(res, function (o) { return ((o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4 || o.PARENT_ID === 3 || o.PARENT_ID === 4)) });

                return vm.itemCategoryList = {
                    optionLabel: "-- Select Agent --",
                    filter: "contains",
                    autoBind: true,
                    data: list,
                    dataTextField: "ITEM_CAT_NAME_EN",
                    dataValueField: "INV_ITEM_CAT_ID"
                };
            });
        }

        vm.maxWeightCheck = function () {
            if (parseInt(vm.form.TOTAL_WEIGHT_OLD) < parseInt(vm.form.TOTAL_WEIGHT))
                vm.form.TOTAL_WEIGHT = vm.form.TOTAL_WEIGHT_OLD;
            vm.checkAllGrid();
            //vm.form.TOTAL_WATER = parseInt(vm.form.TOTAL_WEIGHT) * parseInt(vm.form.LQR_RATIO);


        }


        vm.checkMachineCapacity = function (e) {

            if (vm.form.DYE_MACHINE_ID > 0) {
                var item = e.sender.dataItem(e.item);
                console.log(item);
                if (item.D_PRD_CAPACITY < vm.form.TOTAL_WEIGHT) {
                    vm.form.DYE_MACHINE_ID = '';
                }
            }
        }

        vm.onChangeTemplate = function (e) {

            var item = e.sender.dataItem(e.item);

            vm.recipeItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDDosingTempleteDtl?pDYE_DOSE_TMPLT_H_ID=' + item.DYE_DOSE_TMPLT_H_ID).then(function (res) {

                            e.success(res);
                        });
                    }
                }
            });
        };

        function GetMachineList() {

            DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0').then(function (res) {
                vm.machineList = new kendo.data.DataSource({
                    data: res
                });
            });
        };


        function getdosingTemplateList() {
            return vm.dosingTemplateList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDDosingTemplete').then(function (res) {
                            var list = _.filter(res, function (x) { return x.LK_DC_TMPLT_TYPE_ID === 536 })
                            e.success(list);
                        });
                    }
                },
                //pageSize: 15
            });
        };

        vm.swapMou = function (preItem) {
            if (preItem.MOU_CODE == "%") {
                preItem.MOU_CODE = "g/L";
                if (preItem.INV_ITEM_ID)
                    vm.PreItemCalcRecord(preItem, 1);
            }
            else {
                preItem.MOU_CODE = "%";
                if (preItem.INV_ITEM_ID)
                    vm.PreItemCalcRecord(preItem, 1);
            }
        }

        vm.checkAllGrid = function () {
            vm.form.TOTAL_WATER = vm.form.TOTAL_WEIGHT * vm.form.LQR_RATIO;

            for (var i = 0; i < vm.recipeItemList.length; i++) {
                vm.PreItemCalcRecord(vm.recipeItemList[i], 1);
            }

            //for (var i = 0; i < vm.PostList.length; i++) {
            //    vm.PreItemCalcRecord(vm.PostList[i], 1);
            //}

            //for (var i = 0; i < vm.DyeList.length; i++) {
            //    vm.PreItemCalcRecord(vm.DyeList[i], 1);
            //}

        }

        if ($stateParams.pADDITION_FLAG) {
            vm.form.REQ_TYPE_NAME = "Addition/Topping";
        }
        else
            vm.form.REQ_TYPE_NAME = "Addition/Topping";

        vm.printBatchCard = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-4001';

            vm.form.REQ_TYPE_NAME = "Addition/Topping";

            dataItem.DYE_STR_REQ_H_ID = _.filter(vm.recipeItemList, function (x) { return x.RQD_QTY > 0 && x.IS_LAST == true })[0].DYE_STR_REQ_H_ID;
            vm.form.DYE_STR_REQ_H_ID = dataItem.DYE_STR_REQ_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);

            form.submit();

            document.body.removeChild(form);
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


        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    DyeingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4) });
                        e.success(list);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.checkStock = function () {
            if ((vm.formItem.SUB_STK) < vm.formItem.RQD_QTY) {
                vm.formItem.RQD_QTY = 0;
                config.appToastMsg("MULTI-005 Requisition quantity must less than stock quantity.");
            }
        }

        vm.getItemListByCatID = function (preItem) {
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (preItem.DC_AGENT_ID || 3)).then(function (res) {
                preItem["ItemList"] = res;
            });

            //return preItem.ItemList = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (preItem.DC_AGENT_ID || 3)).then(function (res) {
            //                e.success(res);

            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    }
            //});

        };

        vm.getItemListByCategory = function (preItem) {

            return preItem.ItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=' + (preItem.DC_AGENT_ID || "3,4")).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.addAllNewRecoed = function (PhaseID, indx, items) {
            var idx = indx + 1;
            console.log(vm.recipeItemList);

            var StepNo = 0;

            if ($stateParams.pADDITION_FLAG == 2)
                StepNo = items.STEP_NO;

            var AGENT_ID = "4";

            var item = {
                //index: idx,
                //SL_NO: idx,
                INV_ITEM_ID: 0,
                ITEM_NAME_EN: "--Select Item--",
                DYE_PRD_PHASE_TYPE_ID: PhaseID,
                MOU_CODE: "",
                IS_FINALIZED: 'N',
                IS_WAIT: 'X',
                DOSE_QTY: "0",
                STEP_NO: StepNo,
                IS_LAST: true,
                DYE_STR_REQ_H_ID: 9999999999
            };

            if (PhaseID == 3)
                AGENT_ID = "3,4";

            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=' + AGENT_ID).then(function (res) {

                if (vm.form.DYE_RE_PROC_TYPE_ID < 5) {
                    var list = _.filter(res, function (x) { return (x.IS_RD_CHEM || "N") != "Y" })
                    //console.log(list);
                    item["ItemList"] = list;
                }
                else {

                    item["ItemList"] = res;
                }
            });

            console.log(item);
            vm.recipeItemList.splice(idx, "0", item);

            console.log(vm.recipeItemList);
            //if (!$stateParams.pADDITION_FLAG)
            //    vm.ACTION_DIS = 1;
        }

        vm.removeExtraRecord = function (data) {
            var index = vm.recipeItemList.indexOf(data);
            vm.recipeItemList.splice(index, 1);
            config.appToastMsg("MULTI-001 " + data.ITEM_NAME_EN + " has been removed");
            //if (!$stateParams.pADDITION_FLAG)
            //    vm.ACTION_DIS = 1;
        }

        vm.PreItemCalcRecord = function (pItem, col) {
            if (pItem.INV_ITEM_ID > 0) {
                console.log(pItem);

                if (pItem.RQD_QTY_K > 9999) {
                    pItem.RQD_QTY_K = "";
                    //return;
                }
                if (pItem.RQD_QTY_G > 999) {
                    pItem.RQD_QTY_G = "";
                    //return;
                }
                if (pItem.RQD_QTY_M > 999) {

                    pItem.RQD_QTY_M = "";
                    //return;
                }

                if (col == 1) {
                    //alert(pItem.ITEM_CODE);
                    if (pItem.MOU_CODE == "%") {

                        //pItem.MOU_CODE = "g/L";
                        var qty = (parseFloat(vm.form.TOTAL_WEIGHT) * (parseFloat(pItem.DOSE_QTY) * 10));

                        pItem.RQD_QTY_K = qty / 1000 >= 1 ? parseInt(qty / 1000) : 0;
                        var gm = (qty / 1000).toFixed(5).toString().split('.');

                        pItem.RQD_QTY_G = (parseInt(gm[1]) / 100).toString().split('.')[0];
                        var mgAll = (qty / 1000).toFixed(6).toString().split('.');
                        var mg = ((mgAll[1] / 1000)).toFixed(3).toString().split('.');
                        pItem.RQD_QTY_M = parseInt(mg[1]);
                        pItem.RQD_QTY = parseFloat(qty).toFixed(3);

                        if (parseFloat(pItem.RQD_QTY) > (parseFloat(pItem.SUB_STK))) {
                            pItem.RQD_QTY = '';
                            pItem.RQD_QTY_M = '';
                            pItem.RQD_QTY_G = '';
                            pItem.RQD_QTY_K = '';
                        }
                    }
                    else {
                        //pItem.MOU_CODE = "g/L";

                        var qty = parseFloat(vm.form.TOTAL_WATER) * parseFloat(pItem.DOSE_QTY);

                        pItem.RQD_QTY_K = qty / 1000 >= 1 ? parseInt(qty / 1000) : 0;
                        var gm = (qty / 1000).toFixed(5).toString().split('.');

                        pItem.RQD_QTY_G = (parseInt(gm[1]) / 100).toString().split('.')[0];
                        var mgAll = (qty / 1000).toFixed(6).toString().split('.');
                        var mg = ((mgAll[1] / 1000)).toFixed(3).toString().split('.');
                        pItem.RQD_QTY_M = parseInt(mg[1]);
                        pItem.RQD_QTY = parseFloat(qty).toFixed(3);

                        if (parseFloat(pItem.RQD_QTY) > (parseFloat(pItem.SUB_STK))) {
                            pItem.RQD_QTY = '';
                            pItem.RQD_QTY_M = '';
                            pItem.RQD_QTY_G = '';
                            pItem.RQD_QTY_K = '';
                        }
                    }
                }
                else {

                    if (pItem.MOU_CODE == "%") {

                        var D_Qty = (((parseInt((pItem.RQD_QTY_K || 0) * 1000) + parseInt(pItem.RQD_QTY_G || 0)) + (parseInt(pItem.RQD_QTY_M || 0) * 0.001)) / 10);
                        //console.log(D_Qty);
                        pItem.DOSE_QTY = (D_Qty / parseFloat(vm.form.TOTAL_WEIGHT)).toFixed(5);
                        pItem.RQD_QTY = parseFloat((D_Qty * 10)).toFixed(3);

                        if (parseFloat(pItem.RQD_QTY) > (parseFloat(pItem.SUB_STK))) {
                            pItem.RQD_QTY = '';
                            pItem.RQD_QTY_M = '';
                            pItem.RQD_QTY_G = '';
                            pItem.RQD_QTY_K = '';
                        }
                    }
                    else {

                        var D_Qty = (parseInt((pItem.RQD_QTY_K || 0) * 1000) + parseInt(pItem.RQD_QTY_G || 0)) + (parseInt(pItem.RQD_QTY_M || 0) * 0.001);
                        //console.log(D_Qty);
                        pItem.DOSE_QTY = (D_Qty / parseFloat(vm.form.TOTAL_WATER)).toFixed(5);
                        pItem.RQD_QTY = parseFloat(D_Qty).toFixed(3);

                        if (parseFloat(pItem.RQD_QTY) > (parseFloat(pItem.SUB_STK))) {
                            pItem.RQD_QTY = '';
                            pItem.RQD_QTY_M = '';
                            pItem.RQD_QTY_G = '';
                            pItem.RQD_QTY_K = '';
                        }
                    }
                }

                var REQ_STORE_ID = (vm.form.SCM_STORE_ID || 4);
                DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/GetStockInfoForBP/' + pItem.INV_ITEM_ID + '/' + REQ_STORE_ID).then(function (res) {
                    pItem.CENTRAL_STK = res.CENTRAL_STK;
                    pItem.SUB_STK = res.SUB_STK;
                });
            }
        }


        vm.getBatchDetails = function (e) {
            var obj = e.sender.dataItem(e.item);
            if (obj.DYE_BT_CARD_H_ID > 0) {
                if (fnValidateSub2() == true) {
                    var list = _.filter(vm.batchList.data(), { 'DYE_BT_CARD_H_ID': obj.DYE_BT_CARD_H_ID });
                    vm.formItem.ACT_BATCH_QTY = list[0].ACT_BATCH_QTY;


                    console.log(list[0]);

                    vm.form.DYE_MACHINE_NO = list[0].DYE_MACHINE_NO;
                    vm.form.DYE_MACHINE_ID = list[0].DYE_MACHINE_ID;
                    vm.form.BUYER_NAME_EN = list[0].BUYER_NAME_EN;
                    vm.form.STYLE_NO = list[0].STYLE_NO;
                    vm.form.ORDER_NO = list[0].ORDER_NO;
                    vm.form.COLOR_NAME_EN = list[0].COLOR_NAME_EN;
                    vm.form.LD_RECIPE_NO = list[0].LD_RECIPE_NO;
                    //vm.form.DYE_MACHINE_NO = list[0].DYE_MACHINE_NO;
                    //vm.form.LQR_RATIO = list[0].LQR_RATIO;
                    vm.form.COLOR_GRP_NAME_EN = list[0].COLOR_GRP_NAME_EN;
                    vm.form.MC_LD_RECIPE_H_ID = list[0].MC_LD_RECIPE_H_ID;
                    vm.form.LQR_RATIO = list[0].LQR_RATIO;
                    vm.form.DYE_MTHD_NAME = list[0].DYE_MTHD_NAME;

                    var item = _.filter(vm.batchList.data(), function (o) {
                        return (o.DYE_BT_CARD_H_ID == parseInt(vm.formItem.DYE_BT_CARD_H_ID) || o.MERGE_BT_CRD_ID == parseInt(vm.formItem.DYE_BT_CARD_H_ID))
                    });

                    for (var i = 0; i < item.length; i++) {
                        var objItem = item[i];

                        if (item[i].MC_LD_RECIPE_H_ID <= 0) {

                            DyeingDataService.getDataByFullUrlWait('/api/dye/DCBatchProgramRequisition/UpdateBatchCardLabRecipeNo?pDYE_BT_CARD_H_ID=' + item[i].DYE_BT_CARD_H_ID + '&pMC_COLOR_ID=' + item[i].MC_COLOR_ID + '&pMC_STYLE_H_EXT_ID=' + item[i].MC_STYLE_H_EXT_ID).then(function (res) {
                                if (res.length == 0) {
                                    config.appToastMsg("MULTI-005 Lab-dip Recipe Not Found! Batch (" + objItem.DYE_BATCH_NO + ")");
                                    return;
                                }
                                else {

                                    vm.form.LD_RECIPE_NO = res[0].LD_RECIPE_NO;
                                    vm.form.MC_LD_RECIPE_H_ID = res[0].MC_LD_RECIPE_H_ID;
                                    vm.form.DYE_MTHD_NAME = res[0].DYE_MTHD_NAME;
                                    vm.form.LK_DYE_MTHD_ID = res[0].LK_DYE_MTHD_ID;
                                    vm.form.MC_COLOR_GRP_ID = res[0].MC_COLOR_GRP_ID;
                                    vm.form.COLOR_GRP_NAME_EN = res[0].COLOR_GRP_NAME_EN;

                                    console.log(objItem);

                                    objItem.LD_RECIPE_NO = res[0].LD_RECIPE_NO;
                                    objItem.MC_LD_RECIPE_H_ID = res[0].MC_LD_RECIPE_H_ID;
                                    objItem.DYE_MTHD_NAME = res[0].DYE_MTHD_NAME;
                                    objItem.LK_DYE_MTHD_ID = res[0].LK_DYE_MTHD_ID;
                                    objItem.MC_COLOR_GRP_ID = res[0].MC_COLOR_GRP_ID;
                                    objItem.COLOR_GRP_NAME_EN = res[0].COLOR_GRP_NAME_EN;

                                    vm.dosingTemplateList = new kendo.data.DataSource({
                                        transport: {
                                            read: function (e) {
                                                DyeingDataService.getDataByFullUrlWait('/api/dye/LabdipRecipe/GetLDDosingTempleteByColorGrpID?pMC_COLOR_GRP_ID=' + objItem.MC_COLOR_GRP_ID + '&LK_DYE_MTHD_ID=' + objItem.LK_DYE_MTHD_ID).then(function (res) {
                                                    if (res.length == 1) {
                                                        vm.form.DYE_DOSE_TMPLT_H_ID = res[0].DYE_DOSE_TMPLT_H_ID;
                                                        vm.form["TEMPLATE_ID"] = 1;
                                                    }
                                                    else
                                                        vm.form["TEMPLATE_ID"] = 0;
                                                    e.success(res);
                                                });
                                            }
                                        }
                                    });
                                }
                            });
                        }
                        else {

                            vm.dosingTemplateList = new kendo.data.DataSource({
                                transport: {
                                    read: function (e) {
                                        DyeingDataService.getDataByFullUrlWait('/api/dye/LabdipRecipe/GetLDDosingTempleteByColorGrpID?pMC_COLOR_GRP_ID=' + objItem.MC_COLOR_GRP_ID + '&LK_DYE_MTHD_ID=' + objItem.LK_DYE_MTHD_ID).then(function (res) {
                                            if (res.length == 1) {
                                                vm.form.DYE_DOSE_TMPLT_H_ID = res[0].DYE_DOSE_TMPLT_H_ID;
                                                vm.form["TEMPLATE_ID"] = 1;
                                            }
                                            else
                                                vm.form["TEMPLATE_ID"] = 0;
                                            e.success(res);
                                        });
                                    }
                                }
                            });
                        }


                    }



                }
                else {
                    vm.formItem["ACT_BATCH_QTY"] = '';
                    vm.form.DYE_MACHINE_NO = '';
                    vm.form.BUYER_NAME_EN = '';
                    vm.form.STYLE_NO = '';
                    vm.form.ORDER_NO = '';
                    vm.form.COLOR_NAME_EN = '';
                    vm.form.LD_RECIPE_NO = '';
                    vm.form.LD_RECIPE_NO = '';
                    vm.form.LQR_RATIO = '';
                    vm.form.COLOR_GRP_NAME_EN = '';
                }
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

                vm.BatchProgramList = new kendo.data.DataSource({
                    data: formData,
                    schema: {
                        model: {
                            id: "DYE_BT_CARD_H_ID",
                            fields: {
                                DYE_BT_CARD_H_ID: { editable: false, nullable: true },
                                DYE_BATCH_NO: { validation: { required: true } },
                                ACT_BATCH_QTY: { type: "number", validation: { required: true } },
                            }
                        },
                        //aggregate: [{ field: "ACT_BATCH_QTY", aggregate: "sum" }]
                    },
                    //aggregate: [{ field: "ACT_BATCH_QTY", aggregate: "sum" }]

                });

                var OptUrl = "GetDCBatchProgramReqInfoDtlAllAddition";

                if ($stateParams.pADDITION_FLAG > 0) {
                    OptUrl = "GetDCBatchProgramReqInfoDtlForAddition";
                }
                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/' + OptUrl + '?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
                    var output = res.list;
                    var add = res.addition;
                    var list = _.uniqBy(output, function (d) { return d.PRD_PHASE_NAME });
                    //console.log(output);
                    console.log("A");
                    console.log(list);
                    console.log("AA");
                    vm.recipeGroupList = list.sort();

                    var R_ID = 9999999999;

                    for (var i = 0; i < add.length; i++) {
                        if ((i + 1) == add.length) {
                            add[i].IS_LAST = true;
                            R_ID = add[i].DYE_STR_REQ_H_ID;
                        }
                        else
                            add[i].IS_LAST = false;
                    }

                    vm.additionStepList = add;

                    for (var i = 0; i < output.length; i++) {
                        if (parseInt(output[i].DYE_STR_REQ_H_ID) == R_ID) {
                            if ($stateParams.pADDITION_FLAG > 0) {
                                output[i].DYE_BT_DC_REQ_D2_ID = '';
                                output[i].RQD_QTY = '';
                                output[i].RQD_QTY_K = '';
                                output[i].RQD_QTY_G = '';
                                output[i].RQD_QTY_M = '';
                            }
                            output[i].IS_LAST = true;
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

                    vm.BatchProgramList = new kendo.data.DataSource({
                        data: list,
                        schema: {
                            model: {
                                id: "DYE_BT_CARD_H_ID",
                                fields: {
                                    DYE_BT_CARD_H_ID: { editable: false, nullable: true },
                                    DYE_BATCH_NO: { validation: { required: true } },
                                    ACT_BATCH_QTY: { type: "number", validation: { required: true } },
                                }
                            },
                            //aggregate: [{ field: "ACT_BATCH_QTY", aggregate: "sum" }]
                        },
                        //aggregate: [{ field: "ACT_BATCH_QTY", aggregate: "sum" }]

                    });

                });
            }
        }


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 10;
            var PARENT_ID = 0;
            if ($stateParams.pADDITION_FLAG > 0)
                vm.form.RF_ACTN_STATUS_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                console.log(sList);
                if (sList.length == 1) {
                    vm.form.RF_ACTN_VIEW = 0;
                    vm.form.RF_ACTN_STATUS_ID = sList[0].RF_ACTN_STATUS_ID;
                    vm.form.ACTN_STATUS_NAME = sList[0].ACTN_STATUS_NAME;
                    //alert(vm.form.ACTN_STATUS_NAME);
                }
                else {
                    vm.form.RF_ACTN_VIEW = 1;
                }
            });

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                var lst = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                                if (lst.length == 1) {
                                    vm.form.RF_ACTN_VIEW = 0;
                                    vm.form.RF_ACTN_STATUS_ID = lst[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = lst[0].ACTN_STATUS_NAME;
                                    //alert(vm.form.ACTN_STATUS_NAME);
                                }
                                else {
                                    vm.form.RF_ACTN_VIEW = 1;
                                }
                                e.success(lst);
                            });
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };
        };

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.IsChange = false;
                },

                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

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


        vm.reset = function () {

            $state.go($state.current, { pDYE_STR_REQ_H_ID: 0 }, { inherit: false, reload: true });
            //$state.go('YarnReceive', { pDYE_STR_REQ_H_ID: 0 });
        };


        vm.departmentList = {
            optionLabel: "--Select Dept--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        function GetMOUList() {
            return vm.mOUList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
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
                { field: "DYE_BT_CARD_H_ID", hidden: true },
                { field: "DYE_BT_DC_REQ_D1_ID", hidden: true },

                { field: "SL_NO", title: "SL No", type: "string", width: "15%", template: "<span class='row-number'></span>" },

                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "40%", },
                {
                    field: "ACT_BATCH_QTY", title: "Qty",
                    //aggregates: ["sum"], footerTemplate: "Total Weight: #=sum#",
                    //footerAttributes: { style: "text-align:right" },
                    headerAttributes: { style: "text-align:right" },
                    attributes: { class: "text-right" },
                    format: "{0:n}", type: "number", width: "20%"
                },
                { field: "Unit", title: "Unit", type: "string", width: "15%", template: "<span class='row-unit'></span>" },

                //{
                //    title: "",
                //    //footerTemplate: "Kg",
                //    template: '<a  title="Delete" ng-click="vm.removeBatch(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a>',
                //    width: "10%"
                //}
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

        vm.addDyeItem = function () {
            var idx = vm.LabdipRecipeItemList.data().length + 1;
            vm.LabdipRecipeItemList.insert(idx, {
                SL_NO: idx,
                INV_ITEM_ID: 0,
                ITEM_NAME_EN: "--Select Item--",
                DOSE_QTY: "0"
            });
        }

        vm.removeDyeItem = function (data) {

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.LabdipRecipeItemList.remove(data);
                 });
        }

        vm.PreItemCalc = function (pItem, col) {

            console.log(pItem);

            if (pItem.RQD_QTY_K > 9999) {
                pItem.RQD_QTY_K = "";
                //return;
            }
            if (pItem.RQD_QTY_G > 999) {
                pItem.RQD_QTY_G = "";
                //return;
            }
            if (pItem.RQD_QTY_M > 999) {

                pItem.RQD_QTY_M = "";
                //return;
            }

            if (col == 1) {
                //alert(pItem.ITEM_CODE);
                if (pItem.ITEM_CODE.indexOf("DY")) {
                    pItem.QTY_MOU_ST = "g/L";
                    var qty = parseFloat(vm.form.TOTAL_WEIGHT) * parseFloat(pItem.DOSE_QTY);

                    pItem.RQD_QTY_K = qty / 1000 >= 1 ? parseInt(qty / 1000) : 0;
                    var gm = (qty / 1000).toFixed(3).toString().split('.');

                    pItem.RQD_QTY_G = parseInt(gm[1]);
                    var mgAll = (qty / 1000).toFixed(6).toString().split('.');
                    var mg = ((mgAll[1] / 1000)).toFixed(3).toString().split('.');
                    pItem.RQD_QTY_M = parseInt(mg[1]);
                }
                else {
                    pItem.QTY_MOU_ST = "%";

                    var qty = parseFloat(vm.form.TOTAL_WATER) * parseFloat(pItem.DOSE_QTY);

                    pItem.RQD_QTY_K = qty / 1000 >= 1 ? parseInt(qty / 1000) : 0;
                    var gm = (qty / 1000).toFixed(3).toString().split('.');

                    pItem.RQD_QTY_G = parseInt(gm[1]);
                    var mgAll = (qty / 1000).toFixed(6).toString().split('.');
                    var mg = ((mgAll[1] / 1000)).toFixed(3).toString().split('.');
                    pItem.RQD_QTY_M = parseInt(mg[1]);
                }
            }
            else {

                if (pItem.ITEM_CODE.indexOf("DY")) {

                    var D_Qty = (parseInt((pItem.RQD_QTY_K || 0) * 1000) + parseInt(pItem.RQD_QTY_G || 0)) + (parseInt(pItem.RQD_QTY_M || 0) * 0.001)
                    //console.log(D_Qty);
                    pItem.DOSE_QTY = (D_Qty / parseFloat(vm.form.TOTAL_WEIGHT)).toFixed(6);

                    pItem.QTY_MOU_ST = "g/L";
                }
                else {

                    var D_Qty = (parseInt((pItem.RQD_QTY_K || 0) * 1000) + parseInt(pItem.RQD_QTY_G || 0)) + (parseInt(pItem.RQD_QTY_M || 0) * 0.001)
                    //console.log(D_Qty);
                    pItem.DOSE_QTY = (D_Qty / parseFloat(vm.form.TOTAL_WATER)).toFixed(6);

                    pItem.QTY_MOU_ST = "%";
                }
            }

            var REQ_STORE_ID = (vm.form.SCM_STORE_ID || 4);
            DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/GetStockInfoForBP/' + pItem.INV_ITEM_ID + '/' + REQ_STORE_ID).then(function (res) {
                pItem.CENTRAL_STK = res.CENTRAL_STK;
                pItem.SUB_STK = res.SUB_STK;
            });
        }


        vm.addPreItem = function () {
            var idx = vm.PreList.length + 1;

            vm.PreList.push({
                SL_NO: idx,
                INV_ITEM_ID: 0,
                ITEM_NAME_EN: "--Select Item--",
                DYE_PRD_PHASE_TYPE_ID: 1,
                DOSE_QTY: "0"
            })
        }

        vm.removePreItem = function (data) {

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var index = vm.PreList.indexOf(data);
                     vm.PreList.splice(index, 1);
                 });
        }

        vm.addPostItem = function () {
            var idx = vm.PostList.length + 1;

            vm.PostList.push({
                SL_NO: idx,
                INV_ITEM_ID: "",
                ITEM_NAME_EN: "--Select Item--",
                DYE_PRD_PHASE_TYPE_ID: 3,
                DOSE_QTY: "0"
            });
        }

        vm.removePostItem = function (data) {

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var index = vm.PostList.indexOf(data);
                     vm.PostList.splice(index, 1);
                 });
        }

        vm.addDyeItem = function () {
            var idx = vm.DyeList.length + 1;

            vm.DyeList.push({
                SL_NO: idx,
                INV_ITEM_ID: 0,
                ITEM_NAME_EN: "--Select Item--",
                DYE_PRD_PHASE_TYPE_ID: 2,
                DOSE_QTY: "0"
            })
        }

        vm.removeDyeItem = function (data) {

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var index = vm.DyeList.indexOf(data);
                     vm.DyeList.splice(index, 1);
                 });
        }
        vm.rowIndex = 0;

        vm.getStockQty = function (preItem) {
            //console.log(preItem);
            if (!preItem.DC_AGENT_ID) {
                var ic = _.filter(preItem.ItemList, function (x) { return x.INV_ITEM_ID == preItem.INV_ITEM_ID });
                preItem.DC_AGENT_ID = ic[0].INV_ITEM_CAT_ID;
                preItem.MOU_CODE = ic[0].MOU_CODE;
            }
            else {
                var ic = _.filter(preItem.ItemList, function (x) { return x.INV_ITEM_ID == preItem.INV_ITEM_ID });
                preItem.MOU_CODE = ic[0].MOU_CODE;
            }

            if (preItem.INV_ITEM_ID > 0) {
                var count = 0;
                var REQ_STORE_ID = (vm.form.SCM_STORE_ID || 4);

                DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/GetStockInfoForBP/' + preItem.INV_ITEM_ID + '/' + REQ_STORE_ID).then(function (res) {
                    //console.log(res);
                    preItem.CENTRAL_STK = res.CENTRAL_STK;
                    preItem.SUB_STK = res.SUB_STK;
                    preItem.ITEM_CODE = res.ITEM_CODE;
                    //preItem.INV_ITEM_ID = obj.INV_ITEM_ID;
                    vm.PreItemCalcRecord(preItem, 1);
                });
            }
            else {
                preItem.CENTRAL_STK = '';
                preItem.SUB_STK = '';
            }
        }

        function getItemDataByCategory() {

            return DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=3,4').then(function (res) {

                return vm.ItemList = {
                    optionLabel: "-- Select Dyes Chemical --",
                    filter: "contains",
                    autoBind: true,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                e.success(res);
                            }
                        }
                    },
                    dataTextField: "ITEM_NAME_EN",
                    dataValueField: "INV_ITEM_ID"
                };

            });

        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {
                vm.showSplash = true;
                var data = angular.copy(dataOri);

                var count = _.filter(vm.recipeItemList, function (x) { return x.RQD_QTY > 0 });
                if (count.length == 0) {
                    config.appToastMsg("MULTI-005 There is no item for save");
                    vm.showSplash = false;
                    return;
                }

                var id = 0;

                if (!data.REQ_STORE_ID)
                    data["REQ_STORE_ID"] = data.SCM_STORE_ID;
                if (!data.ISS_STORE_ID)
                    data["ISS_STORE_ID"] = data.SCM_STORE_ID;

                data["XML_REQ_D1"] = DyeingDataService.xmlStringShort(vm.BatchProgramList.data().map(function (o) {
                    return {
                        DYE_STR_REQ_H_ID: o.DYE_STR_REQ_H_ID == null ? 0 : o.DYE_STR_REQ_H_ID,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                        DYE_MACHINE_ID: vm.form.DYE_MACHINE_ID,
                        DYE_MACHINE_NO: vm.form.DYE_MACHINE_NO,
                        DYE_BT_DC_REQ_D1_ID: o.DYE_BT_DC_REQ_D1_ID == null ? 0 : o.DYE_BT_DC_REQ_D1_ID,
                        DYE_DOSE_TMPLT_H_ID: vm.form.DYE_DOSE_TMPLT_H_ID == null ? 0 : vm.form.DYE_DOSE_TMPLT_H_ID,

                        BATCH_QTY: o.ACT_BATCH_QTY == null ? 0 : o.ACT_BATCH_QTY,
                        //BATCH_QTY: vm.form.TOTAL_WEIGHT == null ? 0 : vm.form.TOTAL_WEIGHT,

                        ADDL_QTY: vm.form.ADDL_QTY == null ? 0 : vm.form.ADDL_QTY,
                        LQR_RATIO: vm.form.LQR_RATIO == null ? 0 : vm.form.LQR_RATIO,
                        REMARKS: vm.form.REMARKS == null ? "" : vm.form.REMARKS,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID
                    }
                }));

                //var req_D2_List = _.filter(vm.recipeItemList, function (x) { return (x.RQD_QTY||0) > 0 });
                var ix = 50;
                data["XML_REQ_D2"] = DyeingDataService.xmlStringShort((_.filter(vm.recipeItemList, function (x) { return x.RQD_QTY > 0 && x.IS_LAST == true })).map(function (o) {
                    return {
                        DYE_PRD_PHASE_TYPE_ID: o.DYE_PRD_PHASE_TYPE_ID == null ? 2 : o.DYE_PRD_PHASE_TYPE_ID,
                        DYE_STR_REQ_H_ID: o.DYE_STR_REQ_H_ID == null ? 0 : o.DYE_STR_REQ_H_ID,
                        DYE_BT_DC_REQ_D2_ID: o.DYE_BT_DC_REQ_D2_ID == null ? 0 : o.DYE_BT_DC_REQ_D2_ID,
                        DC_AGENT_ID: o.DC_AGENT_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID == null ? 0 : o.INV_ITEM_ID,
                        DOSE_QTY: o.DOSE_QTY == null ? 0 : o.DOSE_QTY,
                        DOSE_MOU_ID: o.MOU_CODE == "%" ? 27 : 28,
                        STEP_NO: o.STEP_NO == 0 ? ++ix : o.STEP_NO,
                        //STEP_NO: o.STEP_NO == null ? 0 : o.STEP_NO,
                        RQD_QTY_K: o.RQD_QTY_K == null ? 0 : o.RQD_QTY_K,
                        RQD_QTY_G: o.RQD_QTY_G == null ? 0 : o.RQD_QTY_G,
                        RQD_QTY_M: o.RQD_QTY_M == null ? 0 : o.RQD_QTY_M,
                        RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID
                    }
                }));

                var url = "/DCBatchProgramRequisition/SaveAddition";

                if ($stateParams.pADDITION_FLAG > 0) {

                    data.PRE_DYE_STR_REQ_H_ID = vm.form.DYE_STR_REQ_H_ID;
                    data.DYE_STR_REQ_H_ID = 0;
                    id = vm.form.DYE_STR_REQ_H_ID;
                    data.DYE_RE_PROC_TYPE_ID = 2;
                }
                else {
                    data.DYE_STR_REQ_H_ID = _.filter(vm.recipeItemList, function (x) { return x.RQD_QTY > 0 && x.IS_LAST == true })[0].DYE_STR_REQ_H_ID;
                    url = "/DCBatchProgramRequisition/SaveABP";
                    id = vm.form.DYE_STR_REQ_H_ID;
                    data.DYE_RE_PROC_TYPE_ID = 2;
                }

                return DyeingDataService.saveDataByUrl(data, url).then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        config.appToastMsg(res.data.PMSG);
                        if (parseInt(res.data.OPDYE_STR_REQ_H_ID) > 0) {
                            vm.form.DYE_STR_REQ_H_ID = id;
                            $state.go($state.current, { pDYE_STR_REQ_H_ID: id }, { inherit: false, reload: true });
                        }

                    }
                    vm.showSplash = false;
                });
            }
        };


    }


})();

