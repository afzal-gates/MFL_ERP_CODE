
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('FabricInspectionReportListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', FabricInspectionReportListController]);
    function FabricInspectionReportListController($q, config, DyeingDataService, $stateParams, $state, $scope, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate()
        vm.form = { MC_BYR_ACC_GRP_ID: '', MC_COLOR_ID: '', MC_FAB_PROD_ORD_H_ID: '', DYE_BATCH_NO: '' }

        $scope.inspectionList = [];

        vm.showSplash = true;
        function activate() {
            var promise = [GetInspectionList(), GetColorList(), getBuyerAcGrpList(),getStyleExtList(0)];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.expandAll = function (expanded) {
            // $scope is required here, hence the injection above, even though we're using "controller as" syntax
            $scope.$broadcast('onExpandAll', {
                expanded: expanded
            });
        }


        vm.clearData = function () {
            vm.form = { MC_BYR_ACC_GRP_ID: '', MC_COLOR_ID: '', MC_FAB_PROD_ORD_H_ID: '', DYE_BATCH_NO: '' }
        }


        function getStyleExtList(pMC_BYR_ACC_GRP_ID) {
            vm.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;

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

        function GetColorList() {

            return vm.colorList = {
                optionLabel: "-- Select Color --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID"
            };
        };

        function GetStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchStatusType').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BT_STS_TYP_NAME",
                dataValueField: "DYE_BT_STS_TYPE_ID"
            };
        };

        
        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
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
                        getStyleExtList(item.MC_BYR_ACC_GRP_ID);
                    }
                },
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }


        function GetInspectionList() {
            DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRpt').then(function (res) {

                var list = _.groupBy(res, function (x) { return x.DYE_BATCH_NO; });
                $scope.inspectionList = list;
            });
        };


        vm.SearchInspectionList = function () {

            DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRpt?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO || '') + '&pMC_BYR_ACC_GRP_ID=' + (vm.form.MC_BYR_ACC_GRP_ID || null) + '&pMC_FAB_PROD_ORD_H_ID=' + (vm.form.MC_FAB_PROD_ORD_H_ID || null) + '&pMC_COLOR_ID=' + (vm.form.MC_COLOR_ID || null)).then(function (res) {

               var list= _.groupBy(res, function (x) { return x.DYE_BATCH_NO; });
               $scope.inspectionList = list;
            });
        };

        //vm.gridOptions = {
        //    dataSource: new kendo.data.DataSource({
        //        serverPaging: true,
        //        serverSorting: true,
        //        serverFiltering: true,
        //        pageSize: 10,
        //        transport: {
        //            read: function (e) {
        //                var webapi = new kendo.data.transports.webapi({});
        //                var params = webapi.parameterMap(e.data);
        //                var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
        //                DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRpt').then(function (res) {
        //                    e.success(res);
        //                });
        //            }
        //        },
        //        schema: {
        //            data: "data",
        //            total: 'total'

        //        }
        //    }),
        //    filterable: {
        //        extra: false,
        //        operators: {
        //            string: {
        //                contains: "Contains"
        //            }
        //        }
        //    },
        //    pageable: {
        //        refresh: true,
        //        pageSizes: true,
        //        buttonCount: 5
        //    },
        //    sortable: true,
        //    columns: [
        //        { field: "DF_FAB_QC_RPT_ID", hidden: true },
        //        { field: "RPT_REF_NO", hidden: true },
        //        { field: "DYE_BT_CARD_H_ID", hidden: true },
        //        { field: "RPT_SL_NO", hidden: true },
        //        { field: "RQD_FIN_GSM", hidden: true },
        //        { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", type: "string", width: "10%" },
        //        { field: "STYLE_NO", title: "Style", type: "string", width: "10%" },
        //        //{ field: "QC_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(QC_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
        //        { field: "MC_ORDER_NO_LST", title: "Order", type: "string", width: "10%" },
        //        { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%" },
        //        { field: "DYE_BATCH_NO", title: "Batch", type: "string", width: "10%" },
        //        { field: "DYE_LOT_NO", title: "Lot", type: "string", width: "10%" },
        //        { field: "DIA_TYPE_NAME", title: "Dia Type", type: "string", width: "10%" },

        //        {
        //            title: "Action",
        //            template: function () {
        //                return '</a><a ui-sref="FabricInspectionReport({pDF_FAB_QC_RPT_ID:dataItem.DF_FAB_QC_RPT_ID})" class="btn btn-xs blue"><i class="fa fa-edit"> QC Report</i></a>';
        //            },
        //            width: "10%"
        //        }

        //    ]
        //};

    }

})();