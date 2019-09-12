(function () {
    'use strict';
    angular.module('multitex.knitting').controller('JobCardDashboardYdRecvTrnsModalController', ['$q', 'config', 'KnittingDataService', 'Params', '$scope', '$modalInstance', '$modal', JobCardDashboardYdRecvTrnsModalController]);
    function JobCardDashboardYdRecvTrnsModalController($q, config, KnittingDataService, Params, $scope, $modalInstance, $modal) {

        activate()
        $scope.showSplash = true;
        function activate() {
            var promise = [getSelectMonth(0, 0),
                           getStyleExtList(null, Params.DEFAULT_CAT_ID, null, null),
                           getFabOederByOh(null, Params.DEFAULT_CAT_ID, null, null, ''),
                           showGrid(0, (Params.pMC_FAB_PROD_ORD_H_ID || 0), (Params.pMC_FAB_PROD_ORD_H_ID ? 0 : Params.DEFAULT_CAT_ID), null, (Params.LK_COL_TYPE_ID_LST || ''))];
            return $q.all(promise).then(function () {
                $scope.showSplash = false;
            });
        }

        $scope.search = {
            MC_BYR_ACC_ID: 0,
            RF_FAB_PROD_CAT_ID: Params.DEFAULT_CAT_ID,
            MC_FAB_PROD_ORD_H_ID: 0,
            FIRSTDATE: null
        };

        function newKnitPlan(dataItem) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Knitting/Knit/_KnitPlanYdTrnsModal',
                controller: 'KnitPlanYdTrnsController as vm',
                resolve: {
                    KnitPlanHeader: function () {
                        return KnittingDataService.getDataByUrl('/KnitPlan/getKnitPlanHeaderData?pMC_FAB_PROD_ORD_D_ID=' + dataItem.MC_FAB_PROD_ORD_D_ID + '&pRF_FAB_PROD_CAT_ID=' + dataItem.RF_FAB_PROD_CAT_ID)
                            .then(function (res) {
                                return angular.extend(res, { SCM_SUPPLIER_ID: Params.pSCM_SUPPLIER_ID, KNT_SC_PRG_ISS_ID: Params.pKNT_SC_PRG_ISS_ID });
                            });
                    }
                },
                size: 'lg',
                windowClass: 'app-modal-window'
            });

            modalInstance.result.then(function (data) {
                $modalInstance.close(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }


        function getStyleExtList(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
            $scope.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_ID=" + pRF_FAB_PROD_CAT_ID;
                        url += "&pFIRSTDATE=" + pFIRSTDATE;
                        url += "&pLASTDATE=" + pLASTDATE;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pSTYLE_NO';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + (Params.pMC_FAB_PROD_ORD_H_ID || null);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
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
                        url += "&pRF_FAB_PROD_CAT_LST=" + (pRF_FAB_PROD_CAT_ID||'');
                        url += "&pFIRSTDATE=" + pFIRSTDATE;
                        url += "&pLASTDATE=" + pLASTDATE;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pORDER_NO_LST';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_LST=' + (pMC_FAB_PROD_ORD_H_ID||'');

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };


        $scope.productionTypeList = {
            optionLabel: "-- Select Type --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                            e.success(_.findByValues(res, 'RF_FAB_PROD_CAT_ID', Params.RF_FAB_PROD_CAT_ID_LST));
                        });
                    }
                }
            },
            change: function (e) {
                var itmTyp = this.dataItem(e.item);

                if (itmTyp.RF_FAB_PROD_CAT_ID) {
                    getSelectMonth(null, itmTyp.RF_FAB_PROD_CAT_ID);
                    getStyleExtList(null, null, null, null)
                    getFabOederByOh(null, itmTyp.RF_FAB_PROD_CAT_ID, null, null, '')
                } else {
                    getSelectMonth(0, 0);
                    getFabOederByOh(null, null, null, null, '');
                    getStyleExtList(null, null, null, null);
                }
            },
            dataTextField: "FAB_PROD_CAT_SNAME",
            dataValueField: "RF_FAB_PROD_CAT_ID"
        };





        function getSelectMonth(MC_BYR_ACC_ID, RF_FAB_PROD_CAT_ID) {
            return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (MC_BYR_ACC_ID || 0) + '/0/' + (RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
                $scope.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        };

        $scope.onChangeShipMonth = function (e) {
            var itmShipMonth = e.sender.dataItem(e.sender.item);

            if (itmShipMonth.MONTHOF) {
                $scope.search['FIRSTDATE'] = itmShipMonth.FIRSTDATE.split('T')[0];
                $scope.search['LASTDATE'] = itmShipMonth.LASTDATE.split('T')[0];
                getStyleExtList(($scope.search.MC_BYR_ACC_ID || null), ($scope.search.RF_FAB_PROD_CAT_ID || null), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE)
                getFabOederByOh($scope.search.MC_BYR_ACC_ID, ($scope.search.RF_FAB_PROD_CAT_ID || null), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE, '')


            } else {
                getStyleExtList(($scope.search.MC_BYR_ACC_ID || null), ($scope.search.RF_FAB_PROD_CAT_ID || null), null, null);
                $scope.search['FIRSTDATE'] = null;
                $scope.search['LASTDATE'] = null;
                getFabOederByOh($scope.search.MC_BYR_ACC_ID, ($scope.search.RF_FAB_PROD_CAT_ID || null), null, null, '')
            }
        };


        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME||"" #</p></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.FAB_PROD_CAT_SNAME||"" #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        $scope.onFabOrderChange = function (e) {
            var itm = e.sender.dataItem(e.sender.item);
            if (itm.MC_FAB_PROD_ORD_H_ID) {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);

            } else {
                getFabOederByOh(null, null, null, null, '');
            }
        };


        $scope.buyerAcList = {
            optionLabel: "--- Buyer A/C ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
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
                    getFabOederByOh(item.MC_BYR_ACC_ID, null, null, null, '');
                }
            },
            dataTextField: "BYR_ACC_NAME_EN",
            dataValueField: "MC_BYR_ACC_ID"
        };




        $scope.showFabricationDtl = function (MC_FAB_PROD_ORD_H_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'KnitProdOrderFabDtl.html',
                controller: function ($scope, $modalInstance, ItemList) {

                    $scope.ItemList = ItemList;
                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    ItemList: function (KnittingDataService) {
                        return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectForKnitProdOrder/' + (MC_FAB_PROD_ORD_H_ID || 0));
                    }
                }
            });
        }
        $scope.buyerAccList = {
            optionLabel: "-- Select Buyer Account--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            dataTextField: "BYR_ACC_NAME_EN",
            dataValueField: "MC_BYR_ACC_ID"
        };

        $scope.showFabProdOrderList = function () {
            Params['pMC_FAB_PROD_ORD_H_ID'] = -1;
            showGrid(($scope.search.MC_BYR_ACC_ID || 0), ($scope.search.MC_FAB_PROD_ORD_H_ID || 0), ($scope.search.RF_FAB_PROD_CAT_ID || 0), ($scope.search.FIRSTDATE || null), (Params.LK_COL_TYPE_ID_LST || ''))
        }


        var ColNonOrdSelect = [
                { field: "MC_FAB_PROD_ORD_H_ID", hidden: true },

                { field: "PRODUCTION_CAT", title: "Prod. Category", type: "string", width: "7%", template: "#=PRODUCTION_CAT #  # if( LAST_REV_NO && LAST_REV_NO>0) {#<span style=\"padding-bottom: 0px;padding-top: 0px;display:block\" class=\"badge badge-info\">r-#=LAST_REV_NO #<span># }#" },
                //{ field: "BUYER_NAME_EN", title: "Buyer/Party", type: "string", width: "7%" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "7%" },
                { field: "STYLE_DESC", title: "Style Name", type: "string", width: "7%" },
                { field: "STYLE_NO", title: "Work Style No", type: "string", width: "7%" },
                { field: "ORDER_NO", title: "Order No", type: "string", width: "7%" },

                { field: "ORDER_TYPE", title: "Order Type", type: "string", width: "7%" },
                { field: "TOT_ORD_QTY", title: "Order Qty", type: "string", width: "7%" },
                { field: "SHIP_DT_STR", title: "Shipment Date", type: "date", format: "{0:yyyy-MM-dd}", width: "7%" },
                { field: "SHIP_DT", title: "Ship. Month", width: "7%", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'MMM-yyyy') #" },
        ];

        var ColOrdSelect = [
                { field: "MC_FAB_PROD_ORD_H_ID", hidden: true },

                { field: "PRODUCTION_CAT", title: "Prod. Category", type: "string", width: "7%", template: "#=PRODUCTION_CAT #  # if( LAST_REV_NO && LAST_REV_NO>0) {#<span style=\"padding-bottom: 0px;padding-top: 0px;display:block\" class=\"badge badge-info\">r-#=LAST_REV_NO #<span># }#" },
                //{ field: "BUYER_NAME_EN", title: "Buyer/Party", type: "string", width: "7%" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "7%" },
                { field: "STYLE_DESC", title: "Style Name", type: "string", width: "7%" },
                { field: "STYLE_NO", title: "Work Style No", type: "string", width: "7%" },
                { field: "ORDER_NO", title: "Order No", type: "string", width: "7%" },

                { field: "ORDER_TYPE", title: "Order Type", type: "string", width: "7%" },
                { field: "TOT_ORD_QTY", title: "Order Qty", type: "string", width: "7%" },
                { field: "SHIP_DT_STR", title: "Shipment Date", type: "date", format: "{0:yyyy-MM-dd}", width: "7%" },
                { field: "SHIP_DT", title: "Ship. Month", width: "7%", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'MMM-yyyy') #" },
                {
                    title: "Action",
                    template: function () {
                        return "<a ng-click='goKnittingJobCard(dataItem)' class='btn btn-xs'><i class='fa  fa-check-square'></i> Select All</a>";
                    },
                    width: "7%"
                }
        ];

        $scope.gridOptions = {
            dataBound: function (e) {
                var grid = this;
                $.each(grid.tbody.find('tr'), function () {
                    if (!Params.pMC_FAB_PROD_ORD_H_ID) return;
                    var model = grid.dataItem(this);
                    if (model.MC_FAB_PROD_ORD_H_ID == Params.pMC_FAB_PROD_ORD_H_ID) {
                        grid.expandRow("[data-uid='" + model.uid + "']");
                    }
                });
                //grid.element.find('tr.k-master-row').each(function () {
                //    var model = grid.dataItem(this);
                //    var row = $(this);
                //    if (model.NO_OF_DTL==0) {
                //        row.find('.k-hierarchy-cell a').css({ opacity: 0.3, cursor: 'default' }).click(function (e) { e.stopImmediatePropagation(); return false; });
                //    }
                //});

            },
            scrollable: true,
            pageable: true,
            sortable: true,
            height: 500,
            columns: (Params.hasOwnProperty('ORDER_WISE_SELECT') && Params.ORDER_WISE_SELECT) ? ColOrdSelect : ColNonOrdSelect
        };


        function showGrid(MC_BYR_ACC_ID, MC_FAB_PROD_ORD_H_ID, RF_FAB_PROD_CAT_ID, FIRSTDATE, LK_COL_TYPE_ID_LST) {
            return $scope.gridOptionsDs = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectAll/' + params.page + '/' + params.pageSize + '/' + MC_BYR_ACC_ID + '/' + MC_FAB_PROD_ORD_H_ID + '/' + RF_FAB_PROD_CAT_ID + '/null/' + FIRSTDATE + '?pHAS_COL_CUFF=' + (Params.pHAS_COL_CUFF || 'N') + '&pLK_COL_TYPE_ID_LST=' + LK_COL_TYPE_ID_LST).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            });
        }

        var ColNonReq = [
                { field: "COLOR_TYPE", title: "Type", type: "string", width: "7%", template: "#=COLOR_TYPE # # if( IS_ACTIVE=='N') {#<span style=\"padding-bottom: 0px;padding-top: 0px;padding-bottom: 0px;display:block\" class=\"badge badge-default\">Inactive<span># }#" },
                { field: "COLOR_NAME_EN", title: "Fabric Color", width: "7%" },
                { field: "FABRIC_SNAME", title: "Fabrication", width: "30%" },
                { field: "FIN_GSM", title: "GSM", width: "7%" },
                { field: "FIN_DIA_N_DIA_TYPE", title: "Fin. Dia & Type", width: "7%" },
                { field: "FEDER_TYPE_NAME", title: "Feeder Type", width: "7%" },
                { field: "YD_TYPE_NAME", title: "Stripe Type", width: "7%" },
                { field: "KNT_PLAN_NO", title: "KP Ref#", width: "7%" },

                { field: "RQD_GFAB_QTY", title: "Rqd", width: "4%" },
                { field: "BAL_GFAB_QTY", title: "Left2KC", width: "4%" },

                {
                    title: "Action",
                    template: function () {
                        return "<a ng-click='goKnittingJobCard(dataItem)' class='btn btn-xs' ng-disabled='dataItem.IS_ACTIVE==\"N\"'><i class='fa  fa-check-square'></i> Select</a>";
                    },
                    width: "7%"
                }
        ];

        var ColReq = [
               { field: "COLOR_TYPE", title: "Type", type: "string", width: "7%", template: "#=COLOR_TYPE # # if( IS_ACTIVE=='N') {#<span style=\"padding-bottom: 0px;padding-top: 0px;padding-bottom: 0px;display:block\" class=\"badge badge-default\">Inactive<span># }#" },
               { field: "COLOR_NAME_EN", title: "Fabric Color", width: "7%" },
               { field: "FABRIC_SNAME", title: "Fabrication", width: "30%" },
               { field: "FIN_GSM", title: "GSM", width: "7%" },
               { field: "FIN_DIA_N_DIA_TYPE", title: "Fin. Dia & Type", width: "7%" },
               { field: "FEDER_TYPE_NAME", title: "Feeder Type", width: "7%" },
               { field: "YD_TYPE_NAME", title: "Stripe Type", width: "7%" },
               { field: "KNT_PLAN_NO", title: "KP Ref#", width: "7%" },

               { field: "RQD_GFAB_QTY", title: "Rqd", width: "4%" },
               { field: "REQ_PENDING", title: "Left2R", width: "4%" },

               {
                   title: "Action",
                   template: function () {
                       return "<a ng-click='goKnittingJobCard(dataItem)' class='btn btn-xs' ng-disabled='dataItem.IS_ACTIVE==\"N\"'><i class='fa  fa-check-square'></i> Select</a>";
                   },
                   width: "7%"
               }
        ];



        $scope.detailGridOptionsForSupp = function (dataItem) {
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            if (dataItem.HAS_COL_CUFF == 'Y') {
                                var url = '/api/knit/FabProdKnitOrder/SelectByID?pMC_FAB_PROD_ORD_H_ID=' + dataItem.MC_FAB_PROD_ORD_H_ID + '&pOption=3003&pLK_COL_TYPE_ID_LST=' + dataItem.LK_COL_TYPE_ID_LST;
                            } else {
                                var url = '/api/knit/FabProdKnitOrder/SelectByID?pMC_FAB_PROD_ORD_H_ID=' + dataItem.MC_FAB_PROD_ORD_H_ID + '&pLK_COL_TYPE_ID_LST=' + dataItem.LK_COL_TYPE_ID_LST;
                            }

                            return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                                e.success(res.map(function (o) {
                                    o['RF_FAB_PROD_CAT_ID'] = dataItem.RF_FAB_PROD_CAT_ID;
                                    return o;
                                }))
                            });
                        }
                    },
                    group: {
                        field: "COLOR_NAME_EN",
                        dir: "asc"
                    },
                    filter: { field: "MC_FAB_PROD_ORD_H_ID", operator: "eq", value: dataItem.MC_FAB_PROD_ORD_H_ID }
                },
                scrollable: false,
                sortable: true,
                pageable: false,
                groupable: true,
                columns: (Params.IS_REQUISITION && Params.hasOwnProperty('ORDER_WISE_SELECT')) ? ColReq : ColNonReq
            };

        };

        $scope.cancel = function () {
            $modalInstance.dismiss();
        };

        $scope.goKnittingJobCard = function (dataItem) {
            newKnitPlan(dataItem);
        };

    }

})();