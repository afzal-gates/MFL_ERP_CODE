(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCIssueBatchProgramController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', 'Dialog', '$modal', '$filter', DCIssueBatchProgramController]);
    function DCIssueBatchProgramController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, Dialog, $modal, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.DYE_STR_REQ_H_ID ? formData : { REQ_STORE_ID: '4', ISS_STORE_ID: '6', STR_REQ_BY: cur_user_id, ISS_REF_DT: vm.today, RF_ACTN_VIEW: 0 };
        vm.formItem = { 'QTY_MOU_ID': '4', 'CENTRAL_STK': 0, 'SUB_STK': 0 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getItemDataByCategory(), GetBatchList(), getUserData(), GetItemCategoryList(), getCurrencyList(), ReceiveItemList(), GetReqSourceList(), GetReqTypeList(), GetCompanyList(), GetStatusList(), GetMOUList(), GetSupplierList(), GetSourceTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        vm.form['ISS_REF_DT'] = vm.today;




        vm.AlternativeBatchItem = function (items, idx) {

            if (items.RQD_QTY > items.SUB_STK) {

                var item = angular.copy(items);

                items.ISS_QTY = items.SUB_STK;

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: 'alternativeItemDtl.html',
                    controller: function ($scope, $modalInstance, DyeingDataService, formData) {
                        var vmS = this;
                        $scope.form = formData;
                        getItemData($scope.form.DC_AGENT_ID);

                        console.log($scope.form);

                        $scope.cancel = function (data) {
                            $modalInstance.close(data);
                        }

                        $scope.checkStockQty = function () {
                            if (vmS.form.ITEM_CAT_NAME_EN.indexOf("Dye")) {

                                var qty = parseFloat(vmS.form.RQD_QTY);
                                //var qty = parseFloat(vm.form.TOTAL_WEIGHT) * parseFloat(pItem.DOSE_QTY);
                                if (qty < vmS.form.ISS_QTY || vmS.form.ISS_QTY > vmS.form.SUB_STK)
                                    vmS.form.ISS_QTY = '';
                            }
                            else {

                                var qty = parseFloat(vmS.form.RQD_QTY);
                                //var qty = parseFloat(vm.form.TOTAL_WATER) * parseFloat(pItem.DOSE_QTY);
                                if (qty < vmS.form.ISS_QTY || vmS.form.ISS_QTY > vmS.form.SUB_STK)
                                    vmS.form.ISS_QTY = '';
                            }
                        }

                        $scope.getStockQty = function (pItem) {
                            var count = 0;
                            var REQ_STORE_ID = (scope.form.ISS_STORE_ID || 0);

                            DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/GetStockInfoForBP/' + (pItem.IS_ALT_ITEM == 'Y' ? pItem.ALT_ITEM_ID : pItem.INV_ITEM_ID) + '/' + REQ_STORE_ID).then(function (res) {
                                //console.log(res);
                                //pItem.CENTRAL_STK = res.CENTRAL_STK;
                                pItem.CENTRAL_STK = (res.CENTRAL_STK || 0);
                                pItem.SUB_STK = (res.SUB_STK || 0);
                                pItem.ITEM_CAT_NAME_EN = res.ITEM_CAT_NAME_EN;
                                //$scope.PreItemCalc(pItem, 1);
                            });

                        }

                        function getItemData(DC_AGENT_ID) {

                            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=' + DC_AGENT_ID).then(function (res) {

                                return $scope["ItemList"] = {
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


                        $scope.updateAll = function (dataOri) {

                            var data = angular.copy(dataOri);

                            return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DCBatchProgramRequisition/UpdateBTREQD2').then(function (res) {

                                if (res.success === false) {
                                    vm.errors = res.errors;
                                }
                                else {
                                    res['data'] = angular.fromJson(res.jsonStr);
                                    config.appToastMsg(res.data.PMSG);
                                    $scope.cancel(data);
                                }
                            });
                        }

                    },
                    size: 'lg',
                    windowClass: 'large-Modal',
                    resolve: {
                        formData: function (DyeingDataService) {
                            var qty = item.RQD_QTY - item.SUB_STK;
                            item["RQD_QTY"] = qty.toFixed(3);
                            item["RQD_QTY_K"] = qty / 1000 >= 1 ? parseInt(qty / 1000) : 0;
                            var gm = (qty / 1000).toFixed(3).toString().split('.');

                            item["RQD_QTY_G"] = parseInt(gm[1]);
                            var mgAll = (qty / 1000).toFixed(6).toString().split('.');
                            var mg = ((mgAll[1] / 1000)).toFixed(3).toString().split('.');
                            item["RQD_QTY_M"] = parseInt(mg[1]);
                            item["ALT_BT_DC_REQ_D2_ID"] = item.DYE_BT_DC_REQ_D2_ID;

                            if (item.MOU_CODE == "%") {

                                var D_Qty = qty; //(((parseInt((pItem.RQD_QTY_K || 0) * 1000) + parseInt(pItem.RQD_QTY_G || 0)) + (parseInt(pItem.RQD_QTY_M || 0) * 0.001)) / 10);
                                //console.log(D_Qty);
                                item["DOSE_QTY"] = (D_Qty / parseFloat(vm.form.TOTAL_WEIGHT)).toFixed(4);
                                //pItem.RQD_QTY = parseFloat((D_Qty * 10)).toFixed(3);
                            }
                            else {

                                var D_Qty = qty; //(parseInt((pItem.RQD_QTY_K || 0) * 1000) + parseInt(pItem.RQD_QTY_G || 0)) + (parseInt(pItem.RQD_QTY_M || 0) * 0.001);
                                //console.log(D_Qty);
                                item["DOSE_QTY"] = (D_Qty / parseFloat(vm.form.TOTAL_WATER)).toFixed(4);
                                //pItem.RQD_QTY = parseFloat(D_Qty).toFixed(3);
                            }

                            return item;
                        }
                    }
                });

                modalInstance.result.then(function (data) {

                    var indx = idx + 1;
                    console.log(data);
                    if (data)
                        if (data.INV_ITEM_ID != items.INV_ITEM_ID)
                            vm.recipeItemList.splice(indx, "0", data);

                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });
            }
        };


        vm.AllQtyCopy = function () {
            for (var i = 0; i < vm.recipeItemList.length; i++) {
                if (vm.recipeItemList[i].RQD_QTY <= vm.recipeItemList[i].SUB_STK)
                    vm.recipeItemList[i].ISS_QTY = vm.recipeItemList[i].RQD_QTY;
            }
        }

        vm.copyReqQty = function (preItem) {
            if (preItem.RQD_QTY <= preItem.SUB_STK)
                preItem.ISS_QTY = preItem.RQD_QTY;
        }

        vm.checkAllGrid = function () {
            vm.form.TOTAL_WATER = vm.form.TOTAL_WEIGHT * vm.form.LQR_RATIO;

            //for (var i = 0; i < vm.PreList.length; i++) {
            //    vm.PreItemCalc(vm.PreList[i], 1);
            //}

            //for (var i = 0; i < vm.PostList.length; i++) {
            //    vm.PreItemCalc(vm.PostList[i], 1);
            //}

            //for (var i = 0; i < vm.DyeList.length; i++) {
            //    vm.PreItemCalc(vm.DyeList[i], 1);
            //}

        }


        $scope.ISS_REF_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ISS_REF_DT_LNopened = true;
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

        vm.batchForIssueList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/SelectPendingBatch/1/1000?&pRF_REQ_TYPE_ID=7&pUSER_ID=' + cur_user_id + '&pOption=3004').then(function (res) {
                        var list = _.filter(res, function (o) { return (o.ACTN_ROLE_FLAG === "SI") });
                        e.success(list);
                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.selectNewRequisition = function (e) {
            vm.showSplash = true;
            var obj = e.sender.dataItem(e.item);
            $state.go($state.current, { pDYE_STR_REQ_H_ID: obj.DYE_STR_REQ_H_ID }, { reload: true });
        }

        vm.checkStock = function () {
            if ((vm.formItem.CENTRAL_STK + vm.formItem.SUB_STK) < vm.formItem.RQD_QTY) {
                vm.formItem.RQD_QTY = 0;
                config.appToastMsg("MULTI-005 Requisition quantity must less than stock quantity.");
            }
        }

        vm.getItemBrandName = function (e) {
            var obj = e.sender.dataItem(e.item);
            if (obj.DYE_BT_CARD_H_ID > 0) {
                if (fnValidateSub2() == true) {
                    var list = _.filter(vm.batchList.data(), { 'DYE_BT_CARD_H_ID': obj.DYE_BT_CARD_H_ID });
                    vm.formItem.ACT_BATCH_QTY = list[0].ACT_BATCH_QTY;

                    vm.form.DYE_MACHINE_NO = list[0].DYE_MACHINE_NO;
                    vm.form.BUYER_NAME_EN = list[0].BUYER_NAME_EN;
                    vm.form.STYLE_NO = list[0].STYLE_NO;
                    vm.form.ORDER_NO = list[0].ORDER_NO;
                    vm.form.COLOR_NAME_EN = list[0].COLOR_NAME_EN;
                    vm.form.LD_RECIPE_NO = list[0].LD_RECIPE_NO;
                    vm.form.DYE_MACHINE_NO = list[0].DYE_MACHINE_NO;
                    vm.form.LQR_RATIO = list[0].LQR_RATIO;
                    vm.form.COLOR_GRP_NAME_EN = list[0].COLOR_GRP_NAME_EN;


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

                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfoDtlByID?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0) + '&pQUERY_TYPE_ID=1').then(function (output) {
                    var list = _.uniqBy(output, function (d) { return d.PRD_PHASE_NAME });
                    //console.log(output);
                    //console.log(list);
                    vm.recipeGroupList = list;
                    vm.recipeItemList = output;
                    vm.recipeItemList["itemList"] = {};

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
            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                sList = res;
                console.log(sList);
                if (res.length == 1) {
                    vm.form.RF_ACTN_VIEW = 0;
                    vm.form.RF_ACTN_STATUS_ID = res[0].RF_ACTN_STATUS_ID;
                    vm.form.ACTN_STATUS_NAME = res[0].ACTN_STATUS_NAME;
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

                                if (res.length == 1) {
                                    vm.form.RF_ACTN_STATUS_ID = res[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = res[0].ACTN_STATUS_NAME;
                                }
                                e.success(res);
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


        function GetItemCategoryList() {

            DyeingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {

                var list = _.filter(res, function (o) { return ((o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4)) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: list
                });
            });
        }

        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {
                vm.showSplash = true;
                var MC_LD_RECIPE_H_ID = 0;
                var MC_COLOR_GRP_ID = 0;
                var item = _.filter(vm.batchList.data(), function (o) {
                    return (o.DYE_BT_CARD_H_ID == parseInt(vm.formItem.DYE_BT_CARD_H_ID) || o.MERGE_BT_CRD_ID == parseInt(vm.formItem.DYE_BT_CARD_H_ID))
                });

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
                    //console.log(res)
                    //for (var i = 0; i < res.length; i++) {
                    //    res[i].ItemList = dataList;
                    //}

                    vm.DyeList = res;

                });

                DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/GetBatchExtraTreatmentItemList/' + (MC_COLOR_GRP_ID || 0)).then(function (result) {

                    vm.PreList = _.filter(result, function (o) { return o.DYE_PRD_PHASE_TYPE_ID == 1 })
                    ////vm.PreList["ItemList"] = vm.ItemList;
                    //for (var i = 0; i < vm.PreList.length; i++) {
                    //    vm.PreList[i].ItemList = dataList;
                    //}
                    vm.PostList = _.filter(result, function (o) { return o.DYE_PRD_PHASE_TYPE_ID == 3 })
                    //for (var i = 0; i < vm.PostList.length; i++) {
                    //    vm.PostList[i].ItemList = dataList;
                    //}
                    //vm.PostList["ItemList"] = vm.ItemList;
                    vm.showSplash = false;
                });
            }

        };

        vm.removeBatch = function (data) {

            Dialog.confirm('Removing "' + data.DYE_BATCH_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.BatchProgramList.remove(data);
                     var i = 0;
                     var row = 0;

                     var list = _.filter(vm.BatchProgramList.data(), function (o) {
                         return o.SL_NO > 0;
                     }).length;
                     for (i = 0; i < list; i++) {
                         var list2 = _.filter(vm.BatchProgramList.data(), function (o) {
                             return o.SL_NO > 0;
                         })[i];
                         list2.SL_NO = ++row;
                     }
                 });
        }

        vm.resetItemData = function () {
            vm.formItem = { QTY_MOU_ID: '4', CENTRAL_STK: 0, SUB_STK: 0 };
        };

        vm.reset = function () {
            $state.go('YarnReceive', { pDYE_STR_REQ_H_ID: 0 });
        };


        function GetPriorityList() {

            return vm.PriorityList = {
                optionLabel: "-- Select Priority --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(63).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "-- Select Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(84).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetPaymentMethodList() {

            return vm.paymentMethodList = {
                optionLabel: "-- Select Payment Method --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetPayMethod').then(function (res) {
                                //DyeingDataService.getDataByFullUrl('api/common/GetPayMethod').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_MTHD_NAME",
                dataValueField: "RF_PAY_MTHD_ID"
            };
        };

        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                var list = _.filter(res, function (o) { return (o.RF_REQ_TYPE_ID === 5 || o.RF_REQ_TYPE_ID === 6) });
                                //DyeingDataService.LookupListData(88).then(function (res) {
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetReqSourceList() {
            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=3,4&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 || x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
            //return vm.reqSourceList = {
            //    optionLabel: "-- Select Store --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
            //                    var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 4 || x.SCM_STORE_ID == 6 });
            //                    e.success(list);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "STORE_NAME_EN",
            //    dataValueField: "SCM_STORE_ID"
            //};
        };

        function getCurrencyList() {
            return vm.currencyList = {
                optionLabel: "-- Select Currency --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getCurrencyList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "CURR_NAME_EN",
                dataValueField: "RF_CURRENCY_ID"
            };
        };

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
            height: '150px',
            scrollable: true,
            columns: [
                { field: "DYE_BT_CARD_H_ID", hidden: true },
                { field: "DYE_BT_DC_REQ_D1_ID", hidden: true },

                { field: "rowNumber", title: "SL No", type: "string", width: "20%", template: "<span class='row-number'></span>" },

                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "40%", },
                {
                    field: "ACT_BATCH_QTY", title: "Qty",
                    //aggregates: ["sum"], footerTemplate: "Total Weight: #=sum#",
                    //footerAttributes: { style: "text-align:right" },
                    headerAttributes: { style: "text-align:right" },
                    attributes: { class: "text-right" },
                    format: "{0:n}", type: "number", width: "30%"
                },
                { field: "Unit", title: "Unit", type: "string", width: "10%", template: "<span class='row-unit'></span>" },

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

        vm.checkStockQty = function (pItem) {
            if (pItem.ITEM_CAT_NAME_EN.indexOf("Dye")) {

                var qty = parseFloat(pItem.RQD_QTY);
                //var qty = parseFloat(vm.form.TOTAL_WEIGHT) * parseFloat(pItem.DOSE_QTY);
                if (qty < pItem.ISS_QTY || pItem.ISS_QTY > pItem.SUB_STK)
                    pItem.ISS_QTY = '';
            }
            else {

                var qty = parseFloat(pItem.RQD_QTY);
                //var qty = parseFloat(vm.form.TOTAL_WATER) * parseFloat(pItem.DOSE_QTY);
                if (qty < pItem.ISS_QTY || pItem.ISS_QTY > pItem.SUB_STK)
                    pItem.ISS_QTY = '';
            }
        }

        //vm.PreItemCalc = function (pItem, col) {
        //    console.log(pItem);

        //    if (pItem.RQD_QTY_K > 9999) {
        //        pItem.RQD_QTY_K = "";
        //        //return;
        //    }
        //    if (pItem.RQD_QTY_G > 999) {
        //        pItem.RQD_QTY_G = "";
        //        //return;
        //    }
        //    if (pItem.RQD_QTY_M > 999) {

        //        pItem.RQD_QTY_M = "";
        //        //return;
        //    }

        //    if (col == 1) {
        //        //alert(pItem.ITEM_CAT_NAME_EN);
        //        if (pItem.ITEM_CAT_NAME_EN.indexOf("Dye")) {
        //            pItem.QTY_MOU_ST = "g/L";
        //            var qty = parseFloat(vm.form.TOTAL_WEIGHT) * parseFloat(pItem.DOSE_QTY);

        //            pItem.RQD_QTY_K = qty / 1000 >= 1 ? parseInt(qty / 1000) : 0;
        //            var gm = (qty / 1000).toFixed(3).toString().split('.');

        //            pItem.RQD_QTY_G = parseInt(gm[1]);
        //            var mgAll = (qty / 1000).toFixed(6).toString().split('.');
        //            var mg = ((mgAll[1] / 1000)).toFixed(3).toString().split('.');
        //            pItem.RQD_QTY_M = parseInt(mg[1]);
        //        }
        //        else {
        //            pItem.QTY_MOU_ST = "%";

        //            var qty = parseFloat(vm.form.TOTAL_WATER) * parseFloat(pItem.DOSE_QTY);

        //            pItem.RQD_QTY_K = qty / 1000 >= 1 ? parseInt(qty / 1000) : 0;
        //            var gm = (qty / 1000).toFixed(3).toString().split('.');

        //            pItem.RQD_QTY_G = parseInt(gm[1]);
        //            var mgAll = (qty / 1000).toFixed(6).toString().split('.');
        //            var mg = ((mgAll[1] / 1000)).toFixed(3).toString().split('.');
        //            pItem.RQD_QTY_M = parseInt(mg[1]);
        //        }
        //    }
        //    else {

        //        if (pItem.ITEM_CAT_NAME_EN.indexOf("Dye")) {

        //            var D_Qty = (parseInt((pItem.RQD_QTY_K || 0) * 1000) + parseInt(pItem.RQD_QTY_G || 0)) + (parseInt(pItem.RQD_QTY_M || 0) * 0.001)
        //            //console.log(D_Qty);
        //            pItem.DOSE_QTY = (D_Qty / parseFloat(vm.form.TOTAL_WEIGHT)).toFixed(4);

        //            pItem.QTY_MOU_ST = "g/L";
        //        }
        //        else {

        //            var D_Qty = (parseInt((pItem.RQD_QTY_K || 0) * 1000) + parseInt(pItem.RQD_QTY_G || 0)) + (parseInt(pItem.RQD_QTY_M || 0) * 0.001)
        //            //console.log(D_Qty);
        //            pItem.DOSE_QTY = (D_Qty / parseFloat(vm.form.TOTAL_WATER)).toFixed(4);

        //            pItem.QTY_MOU_ST = "%";
        //        }
        //    }
        //}


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

        vm.removeBatchItem = function (data) {

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var index = vm.recipeItemList.indexOf(data);
                     vm.recipeItemList.splice(index, 1);
                 });
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
            var count = 0;
            var REQ_STORE_ID = vm.form.ISS_STORE_ID;

            DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/GetStockInfoForBP/' + (preItem.IS_ALT_ITEM == 'Y' ? preItem.ALT_ITEM_ID : preItem.INV_ITEM_ID) + '/' + REQ_STORE_ID).then(function (res) {
                //console.log(res);
                //preItem.CENTRAL_STK = res.CENTRAL_STK;
                preItem.CENTRAL_STK = (res.CENTRAL_STK || 0);
                preItem.SUB_STK = (res.SUB_STK || 0);
                preItem.ITEM_CAT_NAME_EN = res.ITEM_CAT_NAME_EN;
                //preItem.INV_ITEM_ID = obj.INV_ITEM_ID;
                //vm.PreItemCalc(preItem, 1);
            });

        }

        function getItemDataByCategory() {

            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=3,4').then(function (res) {

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

        vm.deleteItem = function (dataOri) {
            var data = angular.copy(dataOri);
            data['DC_ITEM_ID'] = data.IS_ALT_ITEM == 'Y' ? data.ALT_ITEM_ID : data.INV_ITEM_ID;
            Dialog.confirm('Do you want to delete item: "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                .then(function () {

                    var id = vm.form.DYE_STR_REQ_H_ID

                    return DyeingDataService.saveDataByUrl(data, '/DCIssueBatchProgram/DeleteItem').then(function (res) {

                        if (res.success === false) {
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (id > 0) {
                                config.appToastMsg(res.data.PMSG);
                                $state.go($state.current, { pDYE_BT_DC_ISS_H_ID: id }, { reload: true });
                            }

                        }
                    });

                });
        }

        vm.updateAll = function (dataOri) {

            var data = angular.copy(dataOri);

            Dialog.confirm('Back to Production of Requisition= "' + data.STR_REQ_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     var xlist = _.filter(vm.recipeItemList, function (x) { return x.IS_SEND == 'Y' })
                     var msg = "";
                     if (xlist.length <= 0) {
                         config.appToastMsg("MULTI-003 There is no item to send back dyeing. Please select your item/s");
                         return;
                     }
                     data["XML_REQ_D"] = DyeingDataService.xmlStringShort(xlist.map(function (o) {
                         return {
                             DYE_BT_DC_REQ_D2_ID: o.DYE_BT_DC_REQ_D2_ID == null ? 0 : o.DYE_BT_DC_REQ_D2_ID
                         }
                     }));
                     var id = vm.form.DYE_STR_REQ_H_ID

                     return DyeingDataService.saveDataByUrl(data, '/DCIssueBatchProgram/UpdateRequisition').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.jsonStr);

                             if (id > 0) {
                                 config.appToastMsg(res.data.PMSG);
                                 $state.go($state.current, { pDYE_BT_DC_ISS_H_ID: res.data.OPDYE_BT_DC_ISS_H_ID }, { reload: true });
                             }
                             else {
                                 config.appToastMsg(res.data.PMSG);
                                 if (parseInt(res.data.OPDYE_BT_DC_ISS_H_ID) > 0) {
                                     $state.go($state.current, { pDYE_BT_DC_ISS_H_ID: res.data.OPDYE_BT_DC_ISS_H_ID }, { reload: true });
                                 }
                             }

                         }
                     });
                 });
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var list = _.filter(vm.recipeItemList, function (x) { return x.ISS_QTY > 0 || x.IS_CLOSE == 'Y' })

                var _isudate = $filter('date')(data.ISS_REF_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["ISS_REF_DT"] = _isudate;



                var count = _.filter(vm.recipeItemList, function (x) { return x.RQD_QTY > x.ISS_QTY && x.IS_FINALIZED === 'N' });

                if (count.length > 0) {
                    Dialog.confirm('There is at-least one item is less than required qty. Are you want to continue?', 'Attention', ['Yes', 'No'])
                         .then(function () {

                             data["XML_ISS_D"] = DyeingDataService.xmlStringShort(list.map(function (o) {
                                 return {
                                     DYE_PRD_PHASE_TYPE_ID: o.DYE_PRD_PHASE_TYPE_ID == null ? 2 : o.DYE_PRD_PHASE_TYPE_ID,
                                     DYE_BT_DC_ISS_D_ID: o.DYE_BT_DC_ISS_D_ID == null ? 0 : o.DYE_BT_DC_ISS_D_ID,
                                     DYE_BT_DC_REQ_D2_ID: o.DYE_BT_DC_REQ_D2_ID == null ? 0 : o.DYE_BT_DC_REQ_D2_ID,
                                     DC_ITEM_ID: o.INV_ITEM_ID == null ? 0 : o.INV_ITEM_ID,
                                     ALT_ITEM_ID: o.ALT_ITEM_ID == 0 ? null : o.ALT_ITEM_ID,
                                     IS_ALT_ITEM: o.IS_ALT_ITEM == null ? "N" : o.IS_ALT_ITEM,
                                     DOSE_MOU_ID: o.QTY_MOU_ST == "%" ? 27 : 28,
                                     STEP_NO: o.STEP_NO,
                                     ISS_QTY_K: o.RQD_QTY_K == null ? 0 : o.RQD_QTY_K,
                                     ISS_QTY_G: o.RQD_QTY_G == null ? 0 : o.RQD_QTY_G,
                                     ISS_QTY_M: o.RQD_QTY_M == null ? 0 : o.RQD_QTY_M,
                                     ISS_QTY: o.ISS_QTY == null ? 0 : o.ISS_QTY,
                                     QTY_MOU_ID: 4, //o.QTY_MOU_ID == null ? 4 : o.QTY_MOU_ID,
                                     SP_NOTE: o.SP_NOTE == null ? "" : o.SP_NOTE
                                 }
                             }));

                             var id = vm.form.DYE_STR_REQ_H_ID

                             return DyeingDataService.saveDataByUrl(data, '/DCIssueBatchProgram/Save').then(function (res) {

                                 if (res.success === false) {
                                     vm.errors = res.errors;
                                 }
                                 else {

                                     res['data'] = angular.fromJson(res.jsonStr);

                                     if (id > 0) {
                                         //config.appToastMsg("MULTI-001 Yarn receive has been updated successfully");
                                         config.appToastMsg(res.data.PMSG);
                                         $state.go($state.current, { pDYE_BT_DC_ISS_H_ID: res.data.OPDYE_BT_DC_ISS_H_ID }, { reload: true });
                                     }
                                     else {
                                         config.appToastMsg(res.data.PMSG);
                                         if (parseInt(res.data.OPDYE_BT_DC_ISS_H_ID) > 0) {
                                             //vm.form.DYE_BT_DC_ISS_H_ID = res.data.OPDYE_BT_DC_ISS_H_ID;
                                             $state.go($state.current, { pDYE_BT_DC_ISS_H_ID: res.data.OPDYE_BT_DC_ISS_H_ID }, { reload: true });
                                         }
                                     }

                                 }
                             });
                         });
                    return;
                }

                data["XML_ISS_D"] = DyeingDataService.xmlStringShort(list.map(function (o) {
                    return {
                        DYE_PRD_PHASE_TYPE_ID: o.DYE_PRD_PHASE_TYPE_ID == null ? 2 : o.DYE_PRD_PHASE_TYPE_ID,
                        DYE_BT_DC_ISS_D_ID: o.DYE_BT_DC_ISS_D_ID == null ? 0 : o.DYE_BT_DC_ISS_D_ID,
                        DYE_BT_DC_REQ_D2_ID: o.DYE_BT_DC_REQ_D2_ID == null ? 0 : o.DYE_BT_DC_REQ_D2_ID,
                        DC_ITEM_ID: o.INV_ITEM_ID == null ? 0 : o.INV_ITEM_ID,
                        ALT_ITEM_ID: o.ALT_ITEM_ID == 0 ? null : o.ALT_ITEM_ID,
                        IS_ALT_ITEM: o.IS_ALT_ITEM == null ? "N" : o.IS_ALT_ITEM,
                        DOSE_MOU_ID: o.QTY_MOU_ST == "%" ? 27 : 28,
                        STEP_NO: o.STEP_NO,
                        ISS_QTY_K: o.RQD_QTY_K == null ? 0 : o.RQD_QTY_K,
                        ISS_QTY_G: o.RQD_QTY_G == null ? 0 : o.RQD_QTY_G,
                        ISS_QTY_M: o.RQD_QTY_M == null ? 0 : o.RQD_QTY_M,
                        ISS_QTY: o.ISS_QTY == null ? 0 : o.ISS_QTY,
                        QTY_MOU_ID: 4, //o.QTY_MOU_ID == null ? 4 : o.QTY_MOU_ID,
                        SP_NOTE: o.SP_NOTE == null ? "" : o.SP_NOTE
                    }
                }));

                var id = vm.form.DYE_STR_REQ_H_ID

                return DyeingDataService.saveDataByUrl(data, '/DCIssueBatchProgram/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            //config.appToastMsg("MULTI-001 Yarn receive has been updated successfully");
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pDYE_BT_DC_ISS_H_ID: res.data.OPDYE_BT_DC_ISS_H_ID }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDYE_BT_DC_ISS_H_ID) > 0) {
                                //vm.form.DYE_BT_DC_ISS_H_ID = res.data.OPDYE_BT_DC_ISS_H_ID;
                                $state.go($state.current, { pDYE_BT_DC_ISS_H_ID: res.data.OPDYE_BT_DC_ISS_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };
    }


})();
