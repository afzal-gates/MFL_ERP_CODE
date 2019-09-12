
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DfBtCrdPrintController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', '$filter', DfBtCrdPrintController]);
    function DfBtCrdPrintController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;

        vm.dyeBatchLot = [];

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetDiaTypeList(), GetFabGroupList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function GetDiaTypeList() {

            return vm.DiaTypeList = {
                optionLabel: "-- Select DIA Type--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(67).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetFabGroupList() {
            return vm.FabGroupList = {
                optionLabel: "-- Select Fabric Group --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(45).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };



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

                                vm.form.LK_FBR_GRP_LST = null;

                                if (res.prd.length > 0) {
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
                                    vm.form.DYE_BT_PROD_ID = 0;
                                    vm.form.DYE_BT_STS_TYPE_ID = 1;
                                    vm.form.IS_DISABLE = 0;
                                    vm.form.LK_CHQ_RL_STS_ID = null;
                                }
                            }
                            vm.showSplash = false;
                        });
                    });
                    return;
                }
                else {
                    vm.showSplash = true;
                    DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchForProduction?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || "")).then(function (res) {

                        if (res.batch.length > 0) {
                            console.log(res);
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
                            vm.form.LK_FBR_GRP_LST = null;

                            if (res.prd.length > 0) {
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
                                vm.form.DYE_BT_PROD_ID = 0;
                                vm.form.DYE_BT_STS_TYPE_ID = 1;
                                vm.form.IS_DISABLE = 0;
                                vm.form.LK_CHQ_RL_STS_ID = null;
                            }
                        }
                        vm.showSplash = false;
                    });
                }
            }
        }

        vm.reset = function () {

            $state.go($state.current, { inherit: false, reload: true });
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



        vm.printDupBatchCard = function (dataItem, IsCombine) {
            console.log(dataItem);
            vm.form["REPORT_CODE"] = 'RPT-4043';

            vm.form["DYE_BATCH_NO"] = dataItem.DYE_BATCH_NO;
            vm.form["DYE_BT_CARD_H_ID"] = dataItem.DYE_BT_CARD_H_ID;
            vm.form["LK_DIA_TYPE_ID"] = dataItem.LK_DIA_TYPE_ID;
            vm.form["IS_COMBINE"] = IsCombine;
            vm.form["IS_DUPLICATE"] = dataItem.IS_DUPLICATE == true ? "Duplicate" : "";

            vm.form["LK_FBR_GRP_LST"] = !dataItem.LK_FBR_GRP_LST ? '' : dataItem.LK_FBR_GRP_LST.join(',');

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
        }

    }


})();

