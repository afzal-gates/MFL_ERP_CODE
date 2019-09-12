
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('FabricHoldListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', '$modal', 'Dialog', 'cur_user_id', FabricHoldListController]);
    function FabricHoldListController($q, config, DyeingDataService, $stateParams, $state, $scope, $modal, Dialog, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.form = {};

        activate()

        function activate() {
            var promise = [GetResponsibleDepartmentList(), FabricHoldList(), getColorList(0, 1, 0)];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function getColorList(pMC_FAB_PROD_ORD_H_ID, pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID) {

            vm.colorListDS = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/ColourMaster/GetColorListAfterDyeByID?pMC_FAB_PROD_ORD_H_ID=" + (vm.form.MC_FAB_PROD_ORD_H_ID || 0);
                        url += "&pMC_BYR_ACC_GRP_ID=" + (vm.form.MC_BYR_ACC_GRP_ID || 0);
                        url += "&pMC_STYLE_H_EXT_ID=" + (vm.form.MC_STYLE_H_EXT_ID || 0);

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            }
        };

        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME #</p></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        $scope.buyerAcGrpList = {
            optionLabel: "--- Buyer A/C Group ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            select: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_BYR_ACC_GRP_ID.length != 0) {
                    getStyleExtList(item.MC_BYR_ACC_GRP_ID, null);
                    getColorList(null, item.MC_BYR_ACC_GRP_ID, null);
                }
            },
            dataTextField: "BYR_ACC_GRP_NAME_EN",
            dataValueField: "MC_BYR_ACC_GRP_ID"
        };



        function getStyleExtList(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
            $scope.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16";
                        url += "&pFIRSTDATE=" + (pFIRSTDATE || null);
                        url += "&pLASTDATE=" + (pLASTDATE || null);

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pSTYLE_NO';
                        }
                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };


        function GetResponsibleDepartmentList() {
            return vm.resDeptList = {
                optionLabel: "-- Select Department --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/getRespDeptList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                //change: function (e) {
                //    var item = this.dataItem(e.item);
                //    if (item.SCM_STORE_ID) {
                //        var multiselect = $("#ORDER_LIST").data("kendoDropDownList");
                //        multiselect.dataSource.read();
                //    }
                //},
                dataTextField: "RESP_DEPT_NAME",
                dataValueField: "RF_RESP_DEPT_ID"
            };
        };




        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.STYLE_NO# || #: data.FAB_PROD_CAT_SNAME #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.STYLE_NO||"" #)</h5></span>';


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
            change: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_BYR_ACC_ID) {
                    var multiselect = $("#ORDER_LIST").data("kendoDropDownList");
                    multiselect.dataSource.read();
                }
            },
            dataTextField: "BYR_ACC_NAME_EN",
            dataValueField: "MC_BYR_ACC_ID"
        };


        vm.printHoldList = function (dataItem,isExcel) {

            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6010';

            vm.form.MC_BYR_ACC_GRP_ID = dataItem.MC_BYR_ACC_GRP_ID;
            vm.form.MC_STYLE_H_EXT_ID = dataItem.MC_STYLE_H_EXT_ID;
            vm.form.MC_COLOR_ID = dataItem.MC_COLOR_ID;
            vm.form.IS_EXCEL_FORMAT = isExcel;

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

        vm.searchData = function () {
            vm.gridOptionsDS.read();
        }


        function FabricHoldList() {
            vm.gridOptionsDS = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/FabHold/FabricHoldList?pMC_BYR_ACC_GRP_ID=' + (vm.form.MC_BYR_ACC_GRP_ID || null) + '&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || null) + '&pMC_COLOR_ID=' + (vm.form.MC_COLOR_ID || null)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    model: {
                        fields: {
                            //BYR_ACC_GRP_NAME_EN: { type: "string" },
                            //STYLE_NO: { type: "string" },
                            //FAB_TYPE_SNAME: { type: "string" },
                            //COLOR_NAME_EN: { type: "string" },
                            //RQD_GSM: { type: "string" },
                            //DYE_LOT_NO: { type: "string" },
                            //BATCH_QTY: { type: "decimal" },
                            //ACT_PROD_DT: { type: "date" },
                            //COMP_SNAME: { type: "string" },
                            //FB_DFCT_TYPE_NAME: { type: "string" },
                            //RESP_DEPT_NAME: { type: "string" },

                            RESP_DEPT_NAME: { type: "string" },
                            ACT_BATCH_QTY: { type: "decimal" },
                            BATCH_QTY: { type: "decimal" },
                        }
                    }
                },
                group: {
                    field: "RESP_DEPT_NAME", title: "Department", aggregates: [{ field: "BATCH_QTY", aggregate: "sum" }, { field: "ACT_BATCH_QTY", aggregate: "sum" }]
                },
                aggregate: [{ field: "BATCH_QTY", aggregate: "sum" }, { field: "ACT_BATCH_QTY", aggregate: "sum" }]
            });
        }


        vm.gridOptions = {

            pageable: false,
            filterable: false,
            height: '100%',
            scrollable: false,
            groupable: true,
            columns: [
                //{ field: "COMP_SNAME", title: "Company", type: "string", width: "10%", },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", type: "string", width: "10%", },
                {
                    field: "STYLE_NO", title: "Style/Order", type: "string", width: "10%", template: "<span style=\"display: block;\">#=STYLE_NO #</span> <span style=\"display: block;font-style:italic; color:red;\"><small>#:ORDER_NO#</small><span>"
                },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "5%" },
                { field: "FAB_TYPE_SNAME", title: "Fabric Type", type: "string", width: "5%" },
                { field: "RQD_GSM", title: "Req. GSM", type: "string", width: "5%" },
                { field: "DYE_LOT_NO", title: "Lot No", type: "string", width: "10%" },
                {
                    field: "ACT_BATCH_QTY", title: "Lot Qty.", type: "string", width: "10%",
                    template: "<span style='text-align:center'>#=ACT_BATCH_QTY # Kg</span>",
                    aggregates: ["sum"],
                    groupHeaderTemplate: "Age: #= value # total: #= count #",
                    groupFooterTemplate: "Total: #=sum# Kg",
                    footerTemplate: "G. Total: #=sum# Kg"
                },
                {
                    field: "BATCH_QTY", title: "Re-process Qty.", type: "string", width: "10%",
                    template: "<span style='text-align:center'>#=BATCH_QTY # Kg</span>",
                    aggregates: ["sum"], 
                    groupHeaderTemplate: "Age: #= value # total: #= count #",
                    groupFooterTemplate: "Total: #=sum# Kg", 
                    footerTemplate: "G. Total: #=sum# Kg"
                },
                { field: "ACT_PROD_DT", title: "Hold Date", type: "date", template: "#= kendo.toString(kendo.parseDate(ACT_PROD_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "8%" },
                { field: "COMP_SNAME", title: "Factory Name", type: "string", width: "5%" },
                { field: "FB_DFCT_TYPE_NAME", title: "Reason", type: "string", width: "15%" },
                { field: "RESP_DEPT_NAME", title: "Department", type: "string", width: "15%" },

            ]
        };

    }

})();