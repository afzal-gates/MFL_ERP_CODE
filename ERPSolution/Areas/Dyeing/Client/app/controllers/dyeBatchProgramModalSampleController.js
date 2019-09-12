
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeBatchProgramModalSampleController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$modalInstance', '$scope', 'formData', 'McDataDs', 'Dialog', DyeBatchProgramModalSampleController]);
    function DyeBatchProgramModalSampleController($q, config, DyeingDataService, $stateParams, $modalInstance, $scope, formData, McDataDs, Dialog) {

        var MC_OPEN = [];
        var ObjFromXml = [];
        getSelectMonth(0, 0);
        var cur_tab = 'KNIT';
        getDfProcessTypeData();

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p style="font-size:x-small; color:red;"> #: data.FAB_PROD_CAT_SNAME # (#: data.STYLE_NO #)</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #/#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        console.log(formData);

        $scope.form = {
            NO_OF_BATCH: 1,
            INTERVAL: 10,
            MC_FAB_PROD_ORD_H_ID_LST: [-1],
            DYE_BATCH_PLAN_ID: formData.DYE_BATCH_PLAN_ID,
            RF_FAB_TYPE_ID: formData.RF_FAB_TYPE_ID,
            FAB_COLOR_ID: formData.FAB_COLOR_ID,
            DYE_BATCH_SCDL_ID: formData.DYE_BATCH_SCDL_ID,
            MC_FAB_PROD_ORD_H_ID: formData.MC_FAB_PROD_ORD_H_ID,
            RF_FAB_PROD_CAT_ID: (formData.RF_FAB_PROD_CAT_ID || 1),
            DYE_MACHINE_ID: formData.DYE_MACHINE_ID,
            END_DT: formData.END_DT,
            LOAD_TIME: formData.LOAD_TIME,
            START_DT: formData.START_DT,
            UN_LOAD_TIME: formData.UN_LOAD_TIME,
            IS_ADD_OTH_ORD: 'N',
            BAL_GFAB_QTY:0,
            DURATION: formData.DURATION
        };



        $scope.onIsBatchStoreClick = function (itm) {
            var url;

            if (!itm.hasOwnProperty('requirements')) {
                itm['requirements'] = [];
            }
            console.log(itm);
            url = '/DyeBatch/GetDataFabReqCal?pMC_FAB_PROD_ORD_H_LST=' + itm.MC_FAB_PROD_ORD_H_ID + (itm.LNK_ORD_H_ID_LST ? (',' + itm.LNK_ORD_H_ID_LST) : '');
            url += '&pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID;
            url += '&pDYE_BATCH_SCDL_ID=' + itm.DYE_BATCH_SCDL_ID;
            url += '&pFAB_COLOR_ID=' + itm.FAB_COLOR_ID;
            url += '&pRF_FAB_TYPE_ID=' + itm.RF_FAB_TYPE_ID;
            url += '&pRQD_PRD_QTY=' + itm.RQD_PRD_QTY;
            url += '&pINV_ITEM_CAT_ID=' + itm.INV_ITEM_CAT_ID;
            url += '&pIS_BATCH_STORE=' + (itm.IS_BATCH_STORE || "N");
            return DyeingDataService.getDataByUrl(url).then(function (res) {
                itm['requirements'] = res;
            });
        };

        function getStyleExtList(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
            $scope.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=1,3,5";
                        url += "&pFIRSTDATE=" + (pFIRSTDATE || null);
                        url += "&pLASTDATE=" + (pLASTDATE || null);

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pSTYLE_NO';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + (formData.MC_FAB_PROD_ORD_H_ID || null);
                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };

        function getFabOederByOh(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID) {
            $scope.FabOederByOhDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=1,3";
                        url += "&pFIRSTDATE=" + pFIRSTDATE;
                        url += "&pLASTDATE=" + pLASTDATE;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pORDER_NO_LST';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID;

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };

        function getDfProcessTypeData() {
            return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDFProcessData?pDYE_BATCH_PLAN_ID').then(function (res) {
                $scope.df_data = res;
            });
        };

        function getFabOederByOhMerge(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, pRF_FAB_TYPE_ID, pRF_FAB_PROD_CAT_ID) {
            $scope.FabOederByOhDsMerge = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOhMerge?pFAB_COLOR_ID=" + pFAB_COLOR_ID;
                        url += "&pRF_FAB_TYPE_ID=" + pRF_FAB_TYPE_ID;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pORDER_NO_LST';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID;
                        url += '&pRF_FAB_PROD_CAT_ID=' + pRF_FAB_PROD_CAT_ID;

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };

        $scope.colorDs = {
            transport: {
                read: function (e) {
                    var url = "/api/mrc/ColourMaster/SelectAll?pOption=3000";
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    if (params.filter) {
                        url += '&pCOLOR_NAME_EN=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '&pCOLOR_NAME_EN';
                    }

                    return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            serverFiltering: true
        }


        getFabOederByOh(null, (formData.RF_FAB_PROD_CAT_ID || 1), null, null, null);
        getStyleExtList(null, (formData.RF_FAB_PROD_CAT_ID || 1), null, null);

        findGridDataAll();

        function refreshLastEvtDate(pDYE_MACHINE_ID) {
            return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID=null&pIS_SMP_BLK&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID + '&pDYE_MACHINE_ID=' + pDYE_MACHINE_ID).then(function (res) {
                if (res.length == 1) {
                    $scope.form['DYE_BATCH_PLAN_ID'] = -1;
                    $scope.form['LOAD_TIME'] = res[0].LAST_EVT_END;
                    $scope.form['UN_LOAD_TIME'] = moment(res[0].LAST_EVT_END).add($scope.form.DURATION, 'h')._d;

                    getDfProcessTypeData();

                    refreshRequirement($scope.form.MC_FAB_PROD_ORD_H_ID, $scope.form.FAB_COLOR_ID, $scope.form.RF_FAB_TYPE_ID, 0);
                }
            });
        }

        $scope.onChangeOtherOrder = function (data) {
            if (data.IS_ADD_OTH_ORD == 'Y') {
                getFabOederByOhMerge(data.MC_FAB_PROD_ORD_H_ID, data.FAB_COLOR_ID, data.RF_FAB_TYPE_ID, (formData.RF_FAB_PROD_CAT_ID || 1));

            } else {
                $scope.form.MC_FAB_PROD_ORD_H_ID_LST = [undefined];
            }
        };

        $scope.$watch('form.MC_FAB_PROD_ORD_H_ID_LST', function (newVal, oldVal) {

            if ($scope.form.MC_FAB_PROD_ORD_H_ID) {
                refreshRequirement($scope.form.MC_FAB_PROD_ORD_H_ID + ',' + newVal.filter(function (res) { return res !== undefined }).join(','), $scope.form.FAB_COLOR_ID, $scope.form.RF_FAB_TYPE_ID, $scope.form.RQD_PRD_QTY);
            }

        }, true);

        $scope.$watch('form.RQD_PRD_QTY', function (newVal, oldVal) {
            if (newVal) {
                refreshRequirement($scope.form.MC_FAB_PROD_ORD_H_ID + ',' + $scope.form.MC_FAB_PROD_ORD_H_ID_LST.filter(function (res) { return res !== undefined }).join(','), $scope.form.FAB_COLOR_ID, $scope.form.RF_FAB_TYPE_ID, newVal);
            }

        });


        $scope.McDataDs = new kendo.data.DataSource({
            data: McDataDs
        });

        $scope.onChangeDyeMc = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.id) {
                $scope.form['NO_OF_BATCH'] = 1;
                $scope.form['INTERVAL'] = 10;
                $scope.form['MC_FAB_PROD_ORD_H_ID_LST'] = [undefined];
                $scope.form['DYE_BATCH_PLAN_ID'] = -1;
                $scope.form['DYE_BATCH_SCDL_ID'] = formData.DYE_BATCH_SCDL_ID;
                $scope.form['DYE_MACHINE_ID'] = item.id;
                $scope.form['END_DT'] = item.END_DT;

                DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID=null&pIS_SMP_BLK=' + formData.IS_SMP_BLK + '&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID + '&pDYE_MACHINE_ID=' + item.id).then(function (res) {
                    if (res.length == 1) {
                        $scope.form['LOAD_TIME'] = res[0].LAST_EVT_END;
                        $scope.form['LOAD_TIME'] = res[0].LAST_EVT_END;
                        $scope.form['MAX_LOAD'] = res[0].MAX_LOAD;
                        $scope.form['PCT_EFFCNCY'] = res[0].PCT_EFFCNCY;
                    }
                });

                $scope.form['START_DT'] = item.START_DT;
                $scope.form['UN_LOAD_TIME'] = '';
                $scope.form['IS_ADD_OTH_ORD'] = 'N';
                $scope.form['DURATION'] = '';
                $scope.form['LOAD_TO_DO'] = item.LOAD_TO_DO;
            }
        };


        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };


        $scope.ColourTypelist = {
            optionLabel: "-- Col Type--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.LookupListData(74).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };


        function getWovenFabricAvailability(pMC_FAB_PROD_ORD_H_LST, pMC_COLOR_ID, pINV_ITEM_CAT_ID, host) {
            if (!pMC_FAB_PROD_ORD_H_LST)
                return;

            var url = '/DyeBatchPlan/getProgAvailabilityWovenFab';
            url += '?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_LST;
            url += '&pMC_COLOR_ID=' + pMC_COLOR_ID;
            url += '&pINV_ITEM_CAT_ID=' + pINV_ITEM_CAT_ID;
            DyeingDataService.getDataByUrl(url).then(function (res) {
                host['requirements'] = res;
            });

        };
        function getColorDs(pMC_FAB_PROD_ORD_H_LST, pINV_ITEM_CAT_ID, host) {
            if (!pMC_FAB_PROD_ORD_H_LST)
                return;

            var url = '/DyeBatchPlan/getColorList';
            url += '?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_LST;
            url += '&pINV_ITEM_CAT_ID=' + pINV_ITEM_CAT_ID;
            DyeingDataService.getDataByUrl(url).then(function (res) {
                host['color_list_ds'] = new kendo.data.DataSource({
                    data: res
                });
            });

        };
        $scope.onChangeColor = function (e, cat_id, host) {
            var col = e.sender.dataItem(e.sender.item);
            if (!col.MC_COLOR_ID)
                return;
            getWovenFabricAvailability(host.MC_FAB_PROD_ORD_H_ID, host.FAB_COLOR_ID, cat_id, host);
        };
        $scope.onSelect = function (TabType) {

            switch (TabType) {
                case 'WOVEN':
                    cur_tab = 'WOVEN';
                    $scope.formWoven = angular.copy($scope.form);
                    DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID&pIS_SMP_BLK&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID).then(function (res) {
                        $scope.McDataDs = new kendo.data.DataSource({
                            data: res
                        });

                    });

                    getColorDs($scope.form.MC_FAB_PROD_ORD_H_ID, 35, $scope.formWoven);
                    $scope.formWoven['requirements'] = [];

                    break;
                case 'TRIMS':
                    cur_tab = 'TRIMS';
                    $scope.formTrims = angular.copy($scope.form);
                    DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID&pIS_SMP_BLK&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID).then(function (res) {
                        $scope.McDataDs = new kendo.data.DataSource({
                            data: res
                        });
                    });
                    //getWovenFabricAvailability($scope.form.MC_FAB_PROD_ORD_H_ID, $scope.form.FAB_COLOR_ID, 7, $scope.formTrims);
                    getColorDs($scope.form.MC_FAB_PROD_ORD_H_ID, 7, $scope.formTrims);
                    $scope.formTrims['requirements'] = [];

                    break;
                default:
                    $scope.McDataDs = new kendo.data.DataSource({
                        data: McDataDs
                    });
            }
        };


        $scope.findGridData = function (dataOri) {

            var Defdata = {
                MC_BYR_ACC_ID: null,
                FIRSTDATE: null,
                LASTDATE: null,
                MC_FAB_PROD_ORD_H_IDD: null,
                LK_COL_TYPE_ID: null,
                MC_COLOR_ID: null
            }

            var data = dataOri ? dataOri : Defdata;

            $scope.gridSearchResultDs = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                pageSize: 100,
                schema: {
                    data: "data",
                    total: "total",
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = "/api/Knit/FabProdKnitOrder/getFabOrderDataForBatchProgram?pMC_BYR_ACC_ID=" + data.MC_BYR_ACC_ID;
                        //url += "&pRF_FAB_PROD_CAT_ID=" + formData.RF_FAB_PROD_CAT_ID;
                        url += "&pRF_FAB_PROD_CAT_ID_LST=1,3,5";
                        url += "&pFIRSTDATE=" + data.FIRSTDATE;
                        url += "&pLASTDATE=" + data.LASTDATE;
                        url += "&pMC_FAB_PROD_ORD_H_ID=" + data.MC_FAB_PROD_ORD_H_IDD;
                        url += "&pLK_COL_TYPE_ID=" + data.LK_COL_TYPE_ID;

                        url += "&pMC_COLOR_ID=" + data.MC_COLOR_ID;

                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += config.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                //group: { field: "BYR_ACC_NAME_EN" }
            });
        };


        function refreshRequirement(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, pRF_FAB_TYPE_ID, pRQD_PRD_QTY) {
            var url = '/DyeBatch/GetDataFabReqCal?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + pFAB_COLOR_ID + '&pRF_FAB_TYPE_ID=' + pRF_FAB_TYPE_ID + '&pRQD_PRD_QTY=' + (pRQD_PRD_QTY || 0) + '&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID;
            return DyeingDataService.getDataByUrl(url).then(function (res) {
                $scope.form['requirements'] = res;
            });
        }



        function findGridDataAll() {
            var data = {
                MC_BYR_ACC_ID: null,
                FIRSTDATE: null,
                LASTDATE: null,
                MC_FAB_PROD_ORD_H_IDD: (formData.MC_FAB_PROD_ORD_H_ID || null),
                LK_COL_TYPE_ID: null,
                MC_COLOR_ID: (formData.FAB_COLOR_ID || null)
            }

            $scope.gridSearchResultDs = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                pageSize: 100,
                schema: {
                    data: "data",
                    total: "total",
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = "/api/Knit/FabProdKnitOrder/getFabOrderDataForBatchProgram?pMC_BYR_ACC_ID=" + data.MC_BYR_ACC_ID;
                        //url += "&pRF_FAB_PROD_CAT_ID=" + formData.RF_FAB_PROD_CAT_ID;
                        url += "&pRF_FAB_PROD_CAT_ID_LST=1,3,5";
                        url += "&pFIRSTDATE=" + data.FIRSTDATE;
                        url += "&pLASTDATE=" + data.LASTDATE;
                        url += "&pMC_FAB_PROD_ORD_H_ID=" + data.MC_FAB_PROD_ORD_H_IDD;
                        url += "&pLK_COL_TYPE_ID=" + data.LK_COL_TYPE_ID;

                        url += "&pMC_COLOR_ID=" + data.MC_COLOR_ID;

                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += config.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                //group: { field: "BYR_ACC_NAME_EN" }
            });


            $scope.gridBatchDs = new kendo.data.DataSource({
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = "/api/dye/DyeBatch/GetBatchListByID?pMC_BYR_ACC_ID=" + data.MC_BYR_ACC_ID;
                        //url += "&pMC_BYR_ACC_GRP_ID=" + formData.MC_BYR_ACC_GRP_ID;
                        url += "&pMC_FAB_PROD_ORD_H_ID=" + data.MC_FAB_PROD_ORD_H_IDD;
                        url += "&pMC_COLOR_ID=" + data.MC_COLOR_ID;
                        //=&pMC_STYLE_H_ID=&=&pMC_STYLE_H_EXT_ID
                        DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };


        $scope.gridBatchOpt = {
            height: '300px',
            scrollable: true,
            columns: [
                { field: "DYE_BATCH_NO", title: "Batch #", width: "15%" },
                { field: "DYE_LOT_NO", title: "Lot #", width: "10%" },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", width: "20%" },
                { field: "STYLE_NO", title: "Order/Style", width: "15%" },
                { field: "ORDER_NO", hidden: true, title: "Order #", width: "15%" },
                { field: "COLOR_GRP_NAME_EN", title: "Color Group", width: "10%" },
                { field: "COLOR_NAME_EN", title: "Color Name", width: "15%" },
                { field: "ACT_BATCH_QTY", title: "Batch Qty", width: "10%" },
            ],
            change: function (e) {
                var item = e.sender.dataItem(this.select());
                DyeingDataService.getDataByFullUrl('/api/dye/DyeBatch/GetBatchDataByID?pDYE_BATCH_NO=' + item.DYE_BATCH_NO).then(function (res) {
                    $scope.dyePrograms = res;
                });
            },
            selectable: true,
        };



        $scope.OpenFabricBookingData = function (data) {
            console.log(data);
            var params = {
                REPORT_CODE: 'RPT-2000',
                MC_SMP_REQ_H_ID: data.MC_SMP_REQ_H_ID
            };
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');
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

        $scope.gridSearchResultOpt = {
            height: '200px',
            scrollable: true,
            columns: [
                { field: "BYR_ACC_NAME_EN", title: "ByrAcc", width: "40px" },
                { field: "STYLE_NO", title: "Order/Style", width: "80px", template: "<span style=\"display: block;\">#=ORDER_NO_LIST #</span> <span style=\"display: block;font-style:italic;\"><small>#:STYLE_NO#</small><span>" },

                { field: "ORDER_NO_LIST_CON", hidden: true, title: "Order #", width: "100px", groupHeaderTemplate: " <b>  #= value # </b>" },
                { field: "COL_TYPE_NAME", title: "Type", width: "80px", template: '<span style=\"display: block;\">#=COL_TYPE_NAME #</span> <span title="Open fabric booking" ng-click="OpenFabricBookingData(dataItem)" style="display: block;font-style:italic;cursor: pointer;"><i class="fa fa fa-file-pdf-o"></i>Booking Sheet</span>' },
                { field: "COLOR_NAME_EN", title: "Color Name", width: "70px", template: "<span style=\"display: block;\">#=COLOR_NAME_EN #</span> # if( !LD_RECIPE_NO) {#<span style=\"display: block;color:red;\">No Labdip<span># }#" },
                
                { field: "NET_GFAB_QTY", title: "Req. Qty", width: "30px" },
                { field: "GFAB_RCV_QTY", title: "Rcv. Qty", width: "30px" },
                { field: "BATCH_GFAB_QTY", title: "BT", width: "30px" },
                { field: "BAL_GFAB_QTY", title: "Balance", width: "30px" },
            ],
            change: function (e) {
                var item = e.sender.dataItem(this.select());


                $scope.form['BYR_ACC_NAME_EN'] = item.BYR_ACC_NAME_EN;
                $scope.form['COLOR_NAME_EN'] = item.COLOR_NAME_EN;
                $scope.form['DYE_MTHD_NAME'] = item.DYE_MTHD_NAME;
                $scope.form['FAB_TYPE_NAME'] = item.FAB_TYPE_NAME;
                $scope.form['ORDER_NO_LIST'] = item.ORDER_NO_LIST;
                $scope.form['STYLE_NO'] = item.STYLE_NO;

                $scope.form['FAB_COLOR_ID'] = item.FAB_COLOR_ID;
                $scope.form['LK_DYE_MTHD_ID'] = item.LK_DYE_MTHD_ID;
                $scope.form['RF_FAB_TYPE_ID'] = item.RF_FAB_TYPE_ID;
                $scope.form['MC_FAB_PROD_ORD_H_ID'] = item.MC_FAB_PROD_ORD_H_ID;
                $scope.form['BAL_GFAB_QTY'] = item.BAL_GFAB_QTY;
                
                console.log(item);

                DyeingDataService.getDataByUrl('/DyeBatch/GetDataFabReqCalProgram?pMC_FAB_PROD_ORD_H_LST=' + item.MC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + item.FAB_COLOR_ID).then(function (res) {
                    $scope.form['requirements'] = res;
                });

                loadOldBatch(null, item.MC_FAB_PROD_ORD_H_ID, item.FAB_COLOR_ID);

            },
            selectable: true,
        };

        function loadOldBatch(pMC_BYR_ACC_ID, pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID) {
            $scope.gridBatchDs = new kendo.data.DataSource({
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = "/api/dye/DyeBatch/GetBatchListByID?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pMC_FAB_PROD_ORD_H_ID=" + pMC_FAB_PROD_ORD_H_ID;
                        url += "&pMC_COLOR_ID=" + pFAB_COLOR_ID;
                        DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }


        function getSelectMonth(MC_BYR_ACC_ID, RF_FAB_PROD_CAT_ID) {
            return DyeingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (MC_BYR_ACC_ID || 0) + '/0/' + (RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
                $scope.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        };

        $scope.checkBatchQty = function () {
            //if (parseFloat($scope.form.RQD_PRD_QTY) > parseFloat($scope.form.BAL_GFAB_QTY)) {
            //    $scope.form.RQD_PRD_QTY = '';
            //    config.appToastMsg("MULTI-003 You can not give more than stock fabric qty.");
            //    return false;
            //}
        }

        $scope.onChangeShipMonth = function (e) {
            var itmShipMonth = e.sender.dataItem(e.sender.item);

            if (itmShipMonth.FIRSTDATE && itmShipMonth.LASTDATE) {
                $scope.search['FIRSTDATE'] = itmShipMonth.FIRSTDATE;
                $scope.search['LASTDATE'] = itmShipMonth.LASTDATE;

                getStyleExtList(($scope.search.MC_BYR_ACC_ID || null), (formData.RF_FAB_PROD_CAT_ID || 1), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE)

                getFabOederByOh($scope.search.MC_BYR_ACC_ID, (formData.RF_FAB_PROD_CAT_ID || 1), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE, null)


            } else {
                getStyleExtList(($scope.search.MC_BYR_ACC_ID || null), (formData.RF_FAB_PROD_CAT_ID || 1), null, null);
                $scope.search['FIRSTDATE'] = null;
                $scope.search['LASTDATE'] = null;

                getFabOederByOh($scope.search.MC_BYR_ACC_ID, (formData.RF_FAB_PROD_CAT_ID || 1), null, null, null)
            }
        };
        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME #</p></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        $scope.onFabOrderChange = function (e) {
            var itm = e.sender.dataItem(e.sender.item);
            if (itm.MC_FAB_PROD_ORD_H_ID) {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);

            } else {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);
            }


        };

        DyeingDataService.getDataByFullUrl('/api/common/GetDyeingMethodList').then(function (res) {
            $scope.DyeingMthdDs = new kendo.data.DataSource({
                data: res
            });
        });

        $scope.buyerAcList = {
            optionLabel: "--- Buyer A/C ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            select: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_BYR_ACC_ID.length != 0) {
                    getStyleExtList(item.MC_BYR_ACC_ID, null);
                    getSelectMonth(item.MC_BYR_ACC_ID);
                    getFabOederByOh(item.MC_BYR_ACC_ID, null, null, null, null);
                }
            },
            dataTextField: "BYR_ACC_NAME_EN",
            dataValueField: "MC_BYR_ACC_ID"
        };


        $scope.onChangeDuration = function (DURATION, LOAD_TIME) {
            if (LOAD_TIME && DURATION) {
                $scope.form['UN_LOAD_TIME'] = moment(LOAD_TIME).add(DURATION, 'h')._d;
            }

        };

        $scope.onChangeDate = function (LOAD_TIME, UN_LOAD_TIME) {
            $scope.form['DURATION'] = moment(new Date(UN_LOAD_TIME)).diff(moment(new Date(LOAD_TIME)), 'hours');
        };

        $scope.$watch('form.UN_LOAD_TIME', function (newVal, oldVal) {
            console.log($scope.form.LOAD_TIME);
            if (newVal !== oldVal && $scope.form.LOAD_TIME) {
                $scope.form['DURATION'] = moment(newVal).diff(moment($scope.form.LOAD_TIME), 'hours');
            }

        });


        $scope.addOtherOrder = function (data) {
            data.push(null);
        };

        $scope.remOtherOrder = function (data, idx) {
            if (data.length > 1) {
                data.splice(idx, 1);
            }

        };

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };


        $scope.onRequirementClick = function (itm) {
            var url;

            if (!itm.hasOwnProperty('requirements')) {
                itm['requirements'] = [];
            }

            if (itm.IS_OPEN_REQ && itm.requirements.length == 0) {
                url = '/DyeBatch/GetDataFabReqCal?pMC_FAB_PROD_ORD_H_LST=' + itm.MC_FAB_PROD_ORD_H_ID + (itm.LNK_ORD_H_ID_LST ? (',' + itm.LNK_ORD_H_ID_LST) : '');
                url += '&pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID;
                url += '&pDYE_BATCH_SCDL_ID=' + itm.DYE_BATCH_SCDL_ID;
                url += '&pFAB_COLOR_ID=' + itm.FAB_COLOR_ID;
                url += '&pRF_FAB_TYPE_ID=' + itm.RF_FAB_TYPE_ID;
                url += '&pRQD_PRD_QTY=' + itm.RQD_PRD_QTY;

                return DyeingDataService.getDataByUrl(url).then(function (res) {

                    itm['requirements'] = res;
                    $scope.disabledEdit = false;
                });
            }
        };

        $scope.getDfProcessTypeData = function (itm) {
            if (!itm.hasOwnProperty('df_data')) {
                itm['df_data'] = [];
            }

            if (itm.IS_OPEN_DF_PRC && itm.df_data.length == 0) {
                return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDFProcessData?pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID).then(function (res) {
                    itm['df_data'] = res;
                });
            };
        };


        $scope.onChangeQty = function (main) {
            var sum = 0;
            angular.forEach(_.groupBy(
              main.requirements
            , function (o) {
                return o.MC_FAB_PROD_ORD_H_ID;
            }), function (val, key) {

                sum = _.sumBy(val, function (oo) { return (oo.ACT_BATCH_QTY || oo.RQD_QTY_KG || 0); });

                angular.forEach(main.requirements, function (vvv, kkk) {
                    if (vvv.MC_FAB_PROD_ORD_H_ID == parseInt(key) && vvv.MC_FAB_PROD_ORD_H_SL == 1) {
                        vvv['BT_TOTAL'] = sum;
                    };
                });

            });
        };


        $scope.onChangeQtyD = function (list, d) {
            d.ACT_BATCH_QTY = _.sumBy(d.non_col_cu_avail, function (o) { return (o.ASSIGN_QTY || 0); });
            $scope.onChangeQty(list);
        };




        $scope.save = function (data, isValid) {


            if (!isValid) {
                return;
            }

            if (!data.LK_DYE_MTHD_ID) {
                $scope.DyeingBatchPlan.LK_DYE_MTHD_ID.$setValidity('required', false);
                return;
            }

            var RANGE_START;
            var RANGE_END;

            var DfProcessData = [];


            var ORD_LIST = [];

            angular.forEach(data.MC_FAB_PROD_ORD_H_ID_LST, function (val) {
                if (val && val > 0) {
                    if (ORD_LIST.indexOf(val) < 0) {
                        ORD_LIST.push(val);
                    }
                }
            });

            if (data.IS_ADD_OTH_ORD == 'Y') {
                data['LNK_ORD_H_ID_LST'] = ORD_LIST.join(',');
            } else {
                data['LNK_ORD_H_ID_LST'] = null;
            }
            var startDate;
            var data2Save = [];


            angular.forEach($scope.df_data, function (val, key) {
                angular.forEach(val.items, function (v, k) {
                    if (v.IS_SELECTED == "Y") {
                        DfProcessData.push({
                            DYE_BT_RQD_PROC_ID: v.DYE_BT_RQD_PROC_ID,
                            DF_PROC_TYPE_ID: v.DF_PROC_TYPE_ID
                        });
                    }
                })
            });


            var DfProcessXml = config.xmlStringShortNoTag(DfProcessData);
            var batchNo = "";
            batchNo = data.requirements[0].DYE_BATCH_NO;

            for (var i = 0; i < data.NO_OF_BATCH; i++) {

                if (i == 0) {
                    data2Save = [];
                    startDate = new DayPilot.Date(data.LOAD_TIME).addMinutes(0);
                    RANGE_START = startDate;
                } else if (i > 0 && data2Save.length > 0) {
                    startDate = new DayPilot.Date(data2Save[i - 1]['UN_LOAD_TIME']).addMinutes(data.INTERVAL);
                }


                var child2Save = [];


                var parent = _.filter(data.requirements, function (o) {
                    return o.MC_FAB_PROD_ORD_H_ID == data.MC_FAB_PROD_ORD_H_ID;
                });

                //var child = _.filter(data.requirements, function (o) {
                //    return o.MC_FAB_PROD_ORD_H_ID != data.MC_FAB_PROD_ORD_H_ID;
                //}) || [];

                var child = [];

                angular.forEach(data.requirements, function (oo) {

                    // if (oo.non_col_cu_avail.length > 0 || oo.col_cu_avail.length > 0) {

                    child2Save.push({
                        ACT_BATCH_QTY: oo.ACT_BATCH_QTY,
                        DYE_BATCH_NO: data.requirements[0].DYE_BATCH_NO,
                        DYE_LOT_NO: data.requirements[0].DYE_LOT_NO,
                        MC_FAB_PROD_ORD_H_ID: oo.MC_FAB_PROD_ORD_H_ID,

                        RF_FAB_TYPE_ID: oo.RF_FAB_TYPE_ID,
                        RF_FIB_COMP_ID: oo.RF_FIB_COMP_ID,
                        LK_DIA_TYPE_ID: oo.LK_DIA_TYPE_ID,
                        MC_STYLE_H_ID: oo.MC_STYLE_H_ID,
                        INV_ITEM_CAT_ID: 34,

                        DYE_BT_CARD_H_ID: oo.DYE_BT_CARD_H_ID,
                        LK_FBR_GRP_ID: oo.LK_FBR_GRP_ID,
                        DYE_BT_CARD_GRP_ID: oo.DYE_BT_CARD_GRP_ID,

                        NON_COL_CUF: config.xmlStringShortNoTagChild(oo.non_col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                            return {
                                DYE_BT_CARD_D_FAB_ID: o.DYE_BT_CARD_D_FAB_ID,
                                KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID,
                                RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                                LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                                RQD_QTY: o.RCV_ROLL_WT,
                                QTY_MOU_ID: 3,
                                ASSIGN_QTY: o.ASSIGN_QTY
                            }
                        })),
                        COL_CUF: config.xmlStringShortNoTagChild(oo.col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                            return {
                                KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID,
                                DYE_BT_CARD_D_CLCF_ID: o.DYE_BT_CARD_D_CLCF_ID,
                                PRD_QTY: o.PRD_QTY,
                                QTY_MOU_ID: 1,
                                ASSIGN_QTY: o.ASSIGN_QTY
                            }
                        })),

                        CHILD: config.xmlStringShortNoTagChild(child.map(function (ob) {

                            return {
                                ACT_BATCH_QTY: ob.ACT_BATCH_QTY,
                                DYE_BATCH_NO: child[0].DYE_BATCH_NO,

                                DYE_LOT_NO: child[0].DYE_LOT_NO,
                                MC_FAB_PROD_ORD_H_ID: ob.MC_FAB_PROD_ORD_H_ID,
                                RF_FAB_TYPE_ID: ob.RF_FAB_TYPE_ID,
                                RF_FIB_COMP_ID: ob.RF_FIB_COMP_ID,
                                LK_DIA_TYPE_ID: ob.LK_DIA_TYPE_ID,

                                MC_STYLE_H_ID: ob.MC_STYLE_H_ID,
                                INV_ITEM_CAT_ID: 34,
                                DYE_BT_CARD_H_ID: ob.DYE_BT_CARD_H_ID,
                                LK_FBR_GRP_ID: ob.LK_FBR_GRP_ID,
                                DYE_BT_CARD_GRP_ID: ob.DYE_BT_CARD_GRP_ID,

                                NON_COL_CUF: config.xmlStringShortNoTag(ob.non_col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                    return {
                                        DYE_BT_CARD_D_FAB_ID: o.DYE_BT_CARD_D_FAB_ID,
                                        KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID,
                                        RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                        RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                                        LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                                        RQD_QTY: o.RCV_ROLL_WT,
                                        QTY_MOU_ID: 3,
                                        ASSIGN_QTY: o.ASSIGN_QTY
                                    }
                                })),
                                COL_CUF: config.xmlStringShortNoTag(ob.col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                    return {
                                        KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID,
                                        DYE_BT_CARD_D_CLCF_ID: o.DYE_BT_CARD_D_CLCF_ID,
                                        PRD_QTY: o.PRD_QTY,
                                        QTY_MOU_ID: 1,
                                        ASSIGN_QTY: o.ASSIGN_QTY
                                    }
                                }))


                            }

                        }))

                    });
                    // }

                });



                data2Save.push({
                    DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID,
                    DYE_BATCH_SCDL_ID: data.DYE_BATCH_SCDL_ID,
                    DYE_MACHINE_ID: data.DYE_MACHINE_ID,
                    MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,

                    FAB_COLOR_ID: data.FAB_COLOR_ID,
                    RF_FAB_TYPE_ID: data.RF_FAB_TYPE_ID,

                    LK_DYE_MTHD_ID: data.LK_DYE_MTHD_ID,
                    PRIORITY_NO: i,
                    RQD_PRD_QTY: data.RQD_PRD_QTY,
                    QTY_MOU_ID: 3,
                    LNK_ORD_H_ID_LST: data.LNK_ORD_H_ID_LST,
                    LOAD_TIME: startDate.toString(),
                    UN_LOAD_TIME: startDate.addHours(data.DURATION).toString(),
                    REQUIREMENTS: config.xmlStringShortNoTag(child2Save),
                    MC_STYLE_D_FAB_ID: data.MC_STYLE_D_FAB_ID,
                    DF_XML: DfProcessXml
                });

                RANGE_END = startDate.addHours(data.DURATION);

            };

            return DyeingDataService.saveDataByUrl({
                DYE_BATCH_SCDL_ID: formData.DYE_BATCH_SCDL_ID,
                XML: config.xmlStringShort(data2Save),
                pOption: 1001,
                IS_SMP_BLK: 'S'
            }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        refreshLastEvtDate(data.DYE_MACHINE_ID);

                    }

                    DyeingDataService.getDataByFullUrl('/api/dye/DyeBatch/GetBatchDataByID?pDYE_BATCH_NO=' + batchNo).then(function (res) {
                        $scope.dyePrograms = res;
                    });
                    config.appToastMsg(res.data.PMSG);
                }
            });
        };

        $scope.saveWovenTrims = function (data, isValid, pINV_ITEM_CAT_ID) {
            if (!isValid) {
                return;
            }
            if (!data.LK_DYE_MTHD_ID) {
                $scope.DyeingBatchPlan.LK_DYE_MTHD_ID.$setValidity('required', false);
                return;
            }

            if (!data.FAB_COLOR_ID) {
                if (data.INV_ITEM_CAT_ID == 7) {
                    $scope.DyeingBatchPlanTrims.FAB_COLOR_ID.$setValidity('required', false);
                } else if (data.INV_ITEM_CAT_ID == 35) {
                    $scope.DyeingBatchPlanWoven.FAB_COLOR_ID.$setValidity('required', false);
                }
                return;
            }

            var RANGE_START;
            var RANGE_END;
            var batchNo = "";

            var ORD_LIST = [];

            angular.forEach(data.MC_FAB_PROD_ORD_H_ID_LST, function (val) {
                if (val && val > 0) {
                    if (ORD_LIST.indexOf(val) < 0) {
                        ORD_LIST.push(val);
                    }
                }
            });

            if (data.IS_ADD_OTH_ORD == 'Y') {
                data['LNK_ORD_H_ID_LST'] = ORD_LIST.join(',');
            } else {
                data['LNK_ORD_H_ID_LST'] = null;
            }
            var startDate;
            var data2Save = [];
            for (var i = 0; i < data.NO_OF_BATCH; i++) {

                if (i == 0) {
                    data2Save = [];
                    startDate = new DayPilot.Date(data.LOAD_TIME).addMinutes(0);
                    RANGE_START = startDate;
                } else if (i > 0 && data2Save.length > 0) {
                    startDate = new DayPilot.Date(data2Save[i - 1]['UN_LOAD_TIME']).addMinutes(data.INTERVAL);
                }


                var child2Save = [];


                var parent = _.filter(data.requirements, function (o) {
                    return o.MC_FAB_PROD_ORD_H_ID == data.MC_FAB_PROD_ORD_H_ID;
                });

                var child = _.filter(data.requirements, function (o) {
                    return o.MC_FAB_PROD_ORD_H_ID != data.MC_FAB_PROD_ORD_H_ID;
                }) || [];

                batchNo = data.requirements[0].DYE_BATCH_NO;

                angular.forEach(parent, function (oo) {

                    // if (oo.non_col_cu_avail.length > 0 || oo.col_cu_avail.length > 0) {

                    child2Save.push({

                        DYE_BATCH_NO: parent[0].DYE_BATCH_NO,
                        DYE_LOT_NO: parent[0].DYE_LOT_NO,
                        MC_FAB_PROD_ORD_H_ID: oo.MC_FAB_PROD_ORD_H_ID,
                        DYE_BT_CARD_D_TRMS_ID: oo.DYE_BT_CARD_D_TRMS_ID,
                        MC_ORD_TRMS_ITEM_ID: oo.MC_ORD_TRMS_ITEM_ID,
                        MC_STYLE_H_ID: oo.MC_STYLE_H_ID,

                        RQD_QTY_YDS: oo.RQD_QTY_YDS,
                        RQD_QTY_KG: oo.RQD_QTY_KG,

                        CHILD: config.xmlStringShortNoTagChild(child.map(function (ob) {
                            return {

                                DYE_BATCH_NO: child[0].DYE_BATCH_NO,
                                DYE_LOT_NO: child[0].DYE_LOT_NO,
                                MC_FAB_PROD_ORD_H_ID: ob.MC_FAB_PROD_ORD_H_ID,
                                DYE_BT_CARD_D_TRMS_ID: ob.DYE_BT_CARD_D_TRMS_ID,
                                MC_ORD_TRMS_ITEM_ID: ob.MC_ORD_TRMS_ITEM_ID,
                                MC_STYLE_H_ID: ob.MC_STYLE_H_ID,


                                RQD_QTY_YDS: ob.RQD_QTY_YDS,
                                RQD_QTY_KG: ob.RQD_QTY_KG
                            }

                        }))

                    });
                    // }

                });


                data2Save.push({
                    DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID,
                    DYE_BATCH_SCDL_ID: data.DYE_BATCH_SCDL_ID,
                    DYE_MACHINE_ID: data.DYE_MACHINE_ID,
                    MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,
                    FAB_COLOR_ID: data.FAB_COLOR_ID,
                    LK_DYE_MTHD_ID: data.LK_DYE_MTHD_ID,
                    PRIORITY_NO: i,
                    RQD_PRD_QTY: data.RQD_PRD_QTY,
                    INV_ITEM_CAT_ID: pINV_ITEM_CAT_ID,
                    LNK_ORD_H_ID_LST: data.LNK_ORD_H_ID_LST,
                    LOAD_TIME: startDate.toString(),
                    UN_LOAD_TIME: startDate.addHours(data.DURATION).toString(),
                    REQUIREMENTS: config.xmlStringShortNoTag(child2Save)

                });

                RANGE_END = startDate.addHours(data.DURATION);

            };


            return DyeingDataService.saveDataByUrl({
                DYE_BATCH_SCDL_ID: formData.DYE_BATCH_SCDL_ID,
                XML: config.xmlStringShort(data2Save),
                pOption: 1007,
                IS_SMP_BLK: 'B'
            }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        refreshLastEvtDate(data.DYE_MACHINE_ID, data);
                    }
                    config.appToastMsg(res.data.PMSG);
                    DyeingDataService.getDataByFullUrl('/api/dye/DyeBatch/GetBatchDataByID?pDYE_BATCH_NO=' + batchNo).then(function (res) {
                        $scope.dyePrograms = res;
                    });

                }
            });
        };

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }



        $scope.submitData = function (dataOri) {

            var data2save = [];
            MC_OPEN = [];
            var DfProcessData = [];

            //angular.forEach(dataOri, function (val0, key0) {
            angular.forEach(dataOri, function (val1, key1) {

                if (val1.IS_SELECTED_BT) {

                    if (MC_OPEN.indexOf($scope.form.DYE_MACHINE_ID) < 0) {
                        MC_OPEN.push($scope.form.DYE_MACHINE_ID);
                    }

                    if (val1.df_data && val1.df_data.length > 0) {
                        var DfDataD = [];

                        angular.forEach(val1.df_data, function (val, key) {
                            angular.forEach(val.items, function (v, k) {
                                if (v.IS_SELECTED == "Y") {
                                    DfDataD.push({
                                        DYE_BT_RQD_PROC_ID: v.DYE_BT_RQD_PROC_ID,
                                        DF_PROC_TYPE_ID: v.DF_PROC_TYPE_ID
                                    });
                                }
                            })
                        });

                        DfProcessData.push({
                            DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                            DF_XML_D: config.xmlStringShortNoTag(DfDataD)
                        });
                    }

                    angular.forEach(val1.df_data, function (val, key) {
                        angular.forEach(val.items, function (v, k) {
                            if (v.IS_SELECTED == "Y") {
                                DfDataD.push({
                                    DYE_BT_RQD_PROC_ID: v.DYE_BT_RQD_PROC_ID,
                                    DF_PROC_TYPE_ID: v.DF_PROC_TYPE_ID
                                });
                            }
                        })
                    });

                    var child = [];

                    angular.forEach(val1.requirements, function (oo) {

                        data2save.push({
                            DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                            IS_LOT_MIX: (val1.IS_LOT_MIX || 'N'),
                            ACT_LOAD_TIME: val1.LOAD_TIME,
                            ACT_UN_LOAD_TIME: val1.UN_LOAD_TIME,
                            FAB_COLOR_ID: val1.FAB_COLOR_ID,
                            LK_DYE_MTHD_ID: val1.LK_DYE_MTHD_ID,
                            MC_STYLE_D_FAB_ID: val1.MC_STYLE_D_FAB_ID,
                            IS_BATCH_STORE: val1.IS_BATCH_STORE,

                            INV_ITEM_CAT_ID: oo.INV_ITEM_CAT_ID,

                            DYE_BATCH_NO: _.filter(val1.requirements, function (x) { return x.DYE_BATCH_NO.length > 0 && x.MC_FAB_PROD_ORD_H_ID == oo.MC_FAB_PROD_ORD_H_ID })[0].DYE_BATCH_NO,
                            DYE_LOT_NO: _.filter(val1.requirements, function (x) { return x.DYE_LOT_NO.length > 0 && x.MC_FAB_PROD_ORD_H_ID == oo.MC_FAB_PROD_ORD_H_ID })[0].DYE_LOT_NO,

                            BT_TOTAL: _.filter(val1.requirements, function (x) { return x.BT_TOTAL > 0 && x.MC_FAB_PROD_ORD_H_ID == oo.MC_FAB_PROD_ORD_H_ID })[0].BT_TOTAL,

                            ACT_BATCH_QTY: oo.INV_ITEM_CAT_ID == 34 ? oo.ACT_BATCH_QTY : (oo.RQD_QTY_KG || 0),

                            LK_DIA_TYPE_ID: (oo.LK_DIA_TYPE_ID || ''),
                            RF_FAB_TYPE_ID: (oo.RF_FAB_TYPE_ID || ''),
                            RF_FIB_COMP_ID: (oo.RF_FIB_COMP_ID || ''),
                            RQD_GSM: (oo.RQD_GSM || ''),

                            MC_FAB_PROD_ORD_H_ID: oo.MC_FAB_PROD_ORD_H_ID,
                            DYE_BT_CARD_H_ID: oo.DYE_BT_CARD_H_ID,
                            LK_FBR_GRP_ID: oo.LK_FBR_GRP_ID,
                            DYE_BT_CARD_GRP_ID: oo.DYE_BT_CARD_GRP_ID,
                            INV_ORD_GFAB_STK_ID: oo.INV_ORD_GFAB_STK_ID,

                            DYE_BT_CARD_D_TRMS_ID: (oo.DYE_BT_CARD_D_TRMS_ID || ''),
                            RQD_QTY_YDS: (oo.RQD_QTY_YDS || 0),
                            RQD_QTY_KG: (oo.RQD_QTY_KG || 0),

                            NON_COL_CUF: config.xmlStringShortNoTag(oo.non_col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                return {
                                    DYE_BT_CARD_D_FAB_ID: o.DYE_BT_CARD_D_FAB_ID,
                                    KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID,
                                    RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                    RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                                    LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                                    IS_BATCH_STORE: o.IS_BATCH_STORE,
                                    RQD_QTY: o.RCV_ROLL_WT,
                                    QTY_MOU_ID: 3,
                                    ASSIGN_QTY: o.ASSIGN_QTY
                                }
                            })),
                            COL_CUF: config.xmlStringShortNoTag(oo.col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                return {
                                    KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID,
                                    DYE_BT_CARD_D_CLCF_ID: o.DYE_BT_CARD_D_CLCF_ID,
                                    RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                    RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                                    LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                                    PRD_QTY: o.PRD_QTY,
                                    QTY_MOU_ID: 1,
                                    ASSIGN_QTY: o.ASSIGN_QTY
                                }
                            })),

                            TRIMS_ACC: config.xmlStringShortNoTag(oo.woven_trims_avai.filter(function (f) { return (f.RQD_QTY_YDS || 0) > 0 }).map(function (o) {
                                return {
                                    MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID,
                                    DYE_BT_CARD_D_TRMS_ID: o.DYE_BT_CARD_D_TRMS_ID,
                                    DYE_BT_CARD_H_ID: (oo.DYE_BT_CARD_H_ID || ''),
                                    RQD_QTY_YDS: (o.RQD_QTY_YDS || 0),
                                    RQD_QTY_KG: (oo.RQD_QTY_KG || 0),
                                }
                            })),

                            CHILD: config.xmlStringShortNoTag(child.map(function (ob) {

                                return {
                                    DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                                    ACT_LOAD_TIME: val1.LOAD_TIME,
                                    ACT_UN_LOAD_TIME: val1.UN_LOAD_TIME,
                                    FAB_COLOR_ID: val1.FAB_COLOR_ID,
                                    LK_DYE_MTHD_ID: val1.LK_DYE_MTHD_ID,
                                    MC_STYLE_D_FAB_ID: val1.MC_STYLE_D_FAB_ID,
                                    INV_ITEM_CAT_ID: ob.INV_ITEM_CAT_ID,

                                    LK_DIA_TYPE_ID: (ob.LK_DIA_TYPE_ID || ''),
                                    RQD_GSM: (ob.RQD_GSM || ''),

                                    ACT_BATCH_QTY: ob.INV_ITEM_CAT_ID == 34 ? ob.ACT_BATCH_QTY : (ob.RQD_QTY_KG || 0),

                                    DYE_BATCH_NO: _.filter(child, function (x) { return x.DYE_BATCH_NO.length > 0 })[0].DYE_BATCH_NO,
                                    DYE_LOT_NO: _.filter(child, function (x) { return x.DYE_LOT_NO.length > 0 })[0].DYE_LOT_NO,

                                    MC_FAB_PROD_ORD_H_ID: ob.MC_FAB_PROD_ORD_H_ID,
                                    DYE_BT_CARD_H_ID: ob.DYE_BT_CARD_H_ID,
                                    LK_FBR_GRP_ID: (ob.LK_FBR_GRP_ID || ''),
                                    RF_FAB_TYPE_ID: ob.RF_FAB_TYPE_ID,
                                    RF_FIB_COMP_ID: ob.RF_FIB_COMP_ID,
                                    IS_BATCH_STORE: ob.IS_BATCH_STORE,
                                    DYE_BT_CARD_GRP_ID: (ob.DYE_BT_CARD_GRP_ID || ''),

                                    DYE_BT_CARD_D_TRMS_ID: (ob.DYE_BT_CARD_D_TRMS_ID || ''),
                                    RQD_QTY_YDS: (ob.RQD_QTY_YDS || 0),
                                    RQD_QTY_KG: (ob.RQD_QTY_KG || 0),
                                    INV_ORD_GFAB_STK_ID: ob.INV_ORD_GFAB_STK_ID,

                                    NON_COL_CUF: config.xmlStringShortNoTagChild(ob.non_col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                        return {
                                            DYE_BT_CARD_D_FAB_ID: o.DYE_BT_CARD_D_FAB_ID,
                                            KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID,
                                            RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                            RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                                            LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                                            RQD_QTY: o.RCV_ROLL_WT,
                                            QTY_MOU_ID: 3,
                                            ASSIGN_QTY: o.ASSIGN_QTY
                                        }
                                    })),
                                    COL_CUF: config.xmlStringShortNoTagChild(ob.col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                        return {
                                            KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID,
                                            DYE_BT_CARD_D_CLCF_ID: o.DYE_BT_CARD_D_CLCF_ID,
                                            RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                            RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                                            LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                                            PRD_QTY: o.PRD_QTY,
                                            QTY_MOU_ID: 1,
                                            ASSIGN_QTY: o.ASSIGN_QTY
                                        }
                                    }))
                                }
                            }))
                        });
                    });
                }
            });
            //});

            if (data2save.length == 0) {
                return;
            } else {
                Dialog.confirm('You are updating Batch data...<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return DyeingDataService.saveDataByUrl({
                         XML: config.xmlStringShort(data2save),
                         XML_DF: config.xmlStringShort(DfProcessData)
                     }, '/DyeBatch/Save').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);

                             //if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             //    getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID);
                             //}
                             return config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
            }
        }


        $scope.printBatchCard = function (dataItem) {

            console.log(dataItem);
            $scope.form['REPORT_CODE'] = 'RPT-4032';

            var idNo = dataItem.DYE_BATCH_NO_LST.split('(');
            $scope.form['DYE_BATCH_NO'] = idNo[0];
            $scope.form['DYE_BT_CARD_H_ID'] = dataItem.DYE_BT_CARD_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = $scope.form;

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

        $scope.createRequisition = function (dataOri) {

            console.log(dataOri);
            //return false;
            var data2save = [];
            //angular.forEach(dataOri, function (val0, key0) {
            angular.forEach(dataOri, function (val1, key1) {
                if (val1.IS_SELECTED_BT) {
                    if (data2save.indexOf({
                        DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID
                    }) < 0) {
                        data2save.push({
                            DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                            IS_BATCH_STORE: (val1.IS_BATCH_STORE || 'N'),
                            INV_ITEM_CAT_ID: (val1.INV_ITEM_CAT_ID || 34)
                        });
                    }
                }
            });
            //});

            if (data2save.length == 0) {
                return;
            } else {
                Dialog.confirm('Creating Grey Fabric Req for Batch?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return DyeingDataService.saveDataByUrl({
                         XML: config.xmlStringShort(data2save),
                         RF_REQ_TYPE_ID: 17,// 16 bulk;17 Sample
                         PREFIX: 'RS-',
                         RF_ACTN_TYPE_ID: 6, // 6 Sample,5 Bulk
                         HR_COMPANY_ID: (formData.HR_COMPANY_ID || 1)
                     }, '/DyeBatch/CreateRequisition').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);

                             //if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             //    getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID);
                             //}
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
            }
        }


        $scope.remove = function (data) {
            Dialog.confirm('Removing Program ID# <b>' + data.DYE_BATCH_PLAN_ID + '</b><br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     MC_OPEN = [];
                     MC_OPEN.push(data.DYE_MACHINE_ID);
                     return DyeingDataService.saveDataByUrl({
                         DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID
                     }, '/DyeBatch/Delete').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 DyeingDataService.getDataByFullUrl('/api/dye/DyeBatch/GetBatchDataByID?pDYE_BATCH_NO=' + data.DYE_BATCH_NO).then(function (res) {
                                     $scope.dyePrograms = res;
                                 });
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
        }

    }

})();
