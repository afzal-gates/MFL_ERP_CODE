(function () {
    'use strict';
    angular.module('multitex.knitting').controller('FabricProductionKnittingOrderController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', '$modal', '$window', 'Dialog', FabricProductionKnittingOrderController]);
    function FabricProductionKnittingOrderController($q, config, KnittingDataService, $stateParams, $state, $scope, $modal, $window, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.state = $state.current.data.state;


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getSelectMonth(0, 0), getStyleExtList(null, ($stateParams.pRF_FAB_PROD_CAT_ID || $state.current.data.DEFAULT_CAT_ID), null, null), getFabOederByOh(null, ($stateParams.pRF_FAB_PROD_CAT_ID || $state.current.data.DEFAULT_CAT_ID), null, null, '')/*, showGrid(0, ($stateParams.pMC_FAB_PROD_ORD_H_ID||0), ($stateParams.pRF_FAB_PROD_CAT_ID || $state.current.data.DEFAULT_CAT_ID), null, ($state.current.data.LK_COL_TYPE_ID_LST || ''))*/];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }



        $scope.search = {
            MC_BYR_ACC_ID: 0,
            RF_FAB_PROD_CAT_ID: ($stateParams.pRF_FAB_PROD_CAT_ID || $state.current.data.DEFAULT_CAT_ID),
            MC_FAB_PROD_ORD_H_ID: ($stateParams.pMC_FAB_PROD_ORD_H_ID || 0),
            FIRSTDATE: null
        };

        if ($stateParams.pMC_FAB_PROD_ORD_H_ID) {
            //vm.showFabProdOrderList();
            showGrid(($scope.search.MC_BYR_ACC_ID || 0), ($stateParams.pMC_FAB_PROD_ORD_H_ID || 0), ($stateParams.pRF_FAB_PROD_CAT_ID || 0), $scope.search.FIRSTDATE, ($state.current.data.LK_COL_TYPE_ID_LST || ''));
        }


        function getStyleExtList(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
            $scope.StyleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                delay: 1000,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_FAB_PROD_ORD_H_ID",
                select: function (e) {
                    var itm = this.dataItem(e.item);
                    console.log(itm);

                    if (itm.MC_FAB_PROD_ORD_H_ID) {
                        getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);

                    } else {
                        getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);
                    }
                }
            };

            $scope.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=" + (pRF_FAB_PROD_CAT_ID || '');
                        url += "&pFIRSTDATE=" + pFIRSTDATE;
                        url += "&pLASTDATE=" + pLASTDATE;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pSTYLE_NO';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + ($stateParams.pMC_FAB_PROD_ORD_H_ID || null);

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
            $scope.FabOederByOhOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                delay: 1000,
                dataTextField: "ORDER_NO_LST",
                dataValueField: "MC_FAB_PROD_ORD_H_ID"//,
                //select: function (e) {
                //    var itm = this.dataItem(e.item);
                //    console.log(itm);

                //    if (itm.MC_FAB_PROD_ORD_H_ID) {
                //        getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);

                //    } else {
                //        getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);
                //    }
                //}
            };

            $scope.FabOederByOhDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=" + (pRF_FAB_PROD_CAT_ID || '');
                        url += "&pFIRSTDATE=" + pFIRSTDATE;
                        url += "&pLASTDATE=" + pLASTDATE;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pORDER_NO_LST';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_LST=' + (pMC_FAB_PROD_ORD_H_ID || '');

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
                            e.success(_.findByValues(res, 'RF_FAB_PROD_CAT_ID', $state.current.data.RF_FAB_PROD_CAT_ID_LST));
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




        vm.showFabricationDtl = function (MC_FAB_PROD_ORD_H_ID) {
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

        vm.showStyleItemImage = function (dataItem) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'StyleItemImage.html',
                controller: function ($scope, $modalInstance, images) {

                    $scope.images = images;
                    $scope.cancel = function (data) {
                        $modalInstance.dismiss();
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    images: function () {
                        return dataItem.images;
                    }
                }
            });
        }



        vm.buyerAccList = {
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
        vm.showFabProdOrderList = function () {
            $state.go($state.current.data.state, {}, { notify: false, inherit: false });
            showGrid(($scope.search.MC_BYR_ACC_ID || 0), ($scope.search.MC_FAB_PROD_ORD_H_ID || 0), ($scope.search.RF_FAB_PROD_CAT_ID || 0), ($scope.search.FIRSTDATE || null), ($state.current.data.LK_COL_TYPE_ID_LST || ''), ($state.current.data.RF_FAB_TYPE_ID || ''));
        };

        vm.OpenReportBulk = function (data) {
            data.IS_RPT_OPENING = true;

            var vRptCode = 'RPT-2001';
            if (data.RF_FAB_PROD_CAT_ID == 2) {
                vRptCode = 'RPT-2001';
            }
            else if (data.RF_FAB_PROD_CAT_ID == 1 || data.RF_FAB_PROD_CAT_ID == 3) {
                vRptCode = 'RPT-2000';
            }

            return KnittingDataService.post4pdfRes('/api/mrc/MrcReport/PreviewReport',
                  {
                      PAGE_SIZE_NAME: 'A4',
                      REPORT_CODE: 'RPT-2001',
                      MC_BLK_REVISION_NO: data.LAST_REV_NO,
                      MC_BLK_FAB_REQ_H_ID: data.MC_BLK_FAB_REQ_H_ID
                  }
                  ).then(function (res) {
                      var file = new Blob([res], { type: 'application/pdf' });
                      var fileURL = URL.createObjectURL(file);
                      $window.open(fileURL, "_new", "location=yes,width=1000,height=600,scrollbars=yes,status=yes");
                      data.IS_RPT_OPENING = false;
                  })
        };

        var columnsWithBkRpt = [
                { field: "MC_FAB_PROD_ORD_H_ID", hidden: true },
                { field: "PRODUCTION_CAT", title: "Prod. Category", type: "string", width: "7%", template: "#=PRODUCTION_CAT # # if( KP_TO_DO>0) {#<h6 style=\"padding-bottom: 0px;padding-top: 0px;\" class=\"badge badge-danger\">#=KP_TO_DO #<h6># }# # if( LAST_REV_NO && LAST_REV_NO>0) {#<span style=\"padding-bottom: 0px;padding-top: 0px;display:block\" class=\"badge badge-info\">r-#=LAST_REV_NO #<span># }#" },

                //{ field: "BUYER_NAME_EN", title: "Buyer/Party", type: "string", width: "7%" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "7%" },
                { field: "STYLE_DESC", title: "Style Name", type: "string", width: "7%" },
                //{ field: "SUP_SNAME", title: "Nature of Style", type: "string", width: "7%" },
                { field: "STYLE_NO", title: "Work Style No", type: "string", width: "7%" },
                //{ field: "WORK_STYLE_NO", title: "Base Style No", type: "string", width: "7%" },
                {
                    title: "Fab.",
                    template: function () {
                        return "</a><a  class='btn btn-xs btn-link' ng-click='vm.showFabricationDtl(dataItem.MC_FAB_PROD_ORD_H_ID)' >Show</a></a>";
                    },
                    width: "5%"
                },
                {
                    title: "Item Image",
                    template: function () {
                        return "</a><a  class='btn btn-xs btn-link' ng-click='vm.showStyleItemImage(dataItem)' >Show</a></a>";
                    },
                    width: "7%"
                },                
                {
                    title: "Bk Rpt",
                    template: function () {
                        return "</a><a  class='btn btn-xs btn-link' ng-click='vm.OpenReportBulk(dataItem)' >{{dataItem.IS_RPT_OPENING?'Wait...':'Open'}}</a></a>";
                    },
                    width: "7%"
                },
                //{ field: "SUP_SNAME", title: "Tex Item", type: "string", width: "7%" },
                { field: "ORDER_NO", title: "Order No", type: "string", width: "7%" },

                { field: "ORDER_TYPE", title: "Order Type", type: "string", width: "7%" },
                //{ field: "SUPPLIER_GROUP", title: "Prod. Phase", type: "string", width: "7%" },
                { field: "TOT_ORD_QTY", title: "Order Qty", type: "string", width: "7%" },
                { field: "SHIP_DT_STR", title: "Shipment Date", type: "date", format: "{0:yyyy-MM-dd}", width: "7%" },
                { field: "SHIP_DT", title: "Ship. Month", width: "7%", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'MMM-yyyy') #" },
                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='YarnPurReq({pMC_FAB_PROD_ORD_H_ID:dataItem.MC_FAB_PROD_ORD_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Yarn Req</i></a></a>";
                    },
                    width: "5%"
                }
        ];

        var columnsWithOutBkRpt = [
        { field: "MC_FAB_PROD_ORD_H_ID", hidden: true },
        { field: "PRODUCTION_CAT", title: "Prod. Category", type: "string", width: "7%", template: "#=PRODUCTION_CAT # # if( KP_TO_DO>0) {#<h6 style=\"padding-bottom: 0px;padding-top: 0px;\" class=\"badge badge-danger\">#=KP_TO_DO #<h6># }# # if( LAST_REV_NO && LAST_REV_NO>0) {#<span style=\"padding-bottom: 0px;padding-top: 0px;display:block\" class=\"badge badge-info\">r-#=LAST_REV_NO #<span># }#" },

        //{ field: "BUYER_NAME_EN", title: "Buyer/Party", type: "string", width: "7%" },
        { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "7%" },
        { field: "STYLE_DESC", title: "Style Name", type: "string", width: "7%" },
        //{ field: "SUP_SNAME", title: "Nature of Style", type: "string", width: "7%" },
        { field: "STYLE_NO", title: "Work Style No", type: "string", width: "7%" },
        //{ field: "WORK_STYLE_NO", title: "Base Style No", type: "string", width: "7%" },
        {
            title: "Fab.",
            template: function () {
                return "</a><a  class='btn btn-xs btn-link' ng-click='vm.showFabricationDtl(dataItem.MC_FAB_PROD_ORD_H_ID)' >Show</a></a>";
            },
            width: "5%"
        },
        {
            title: "Item Image",
            template: function () {
                return "</a><a  class='btn btn-xs btn-link' ng-click='vm.showStyleItemImage(dataItem)' >Show</a></a>";
            },
            width: "7%"
        },
        //{ field: "SUP_SNAME", title: "Tex Item", type: "string", width: "7%" },
        { field: "ORDER_NO", title: "Order No", type: "string", width: "7%" },

        { field: "ORDER_TYPE", title: "Order Type", type: "string", width: "7%" },
        //{ field: "SUPPLIER_GROUP", title: "Prod. Phase", type: "string", width: "7%" },
        { field: "TOT_ORD_QTY", title: "Order Qty", type: "string", width: "7%" },
        { field: "SHIP_DT_STR", title: "Shipment Date", type: "date", format: "{0:yyyy-MM-dd}", width: "7%" },
        { field: "SHIP_DT", title: "Ship. Month", width: "7%", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'MMM-yyyy') #" },
        //{
        //    title: "Action",
        //    template: function () {
        //        return "</a><a ui-sref='YarnPurReq({pMC_FAB_PROD_ORD_H_ID:dataItem.MC_FAB_PROD_ORD_H_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Yarn Req</i></a></a>";
        //    },
        //    width: "10%"
        //}
        ];


        vm.gridOptions = {
            dataBound: function (e) {
                var grid = this;
                $.each(grid.tbody.find('tr'), function () {
                    if (!$stateParams.pMC_FAB_PROD_ORD_H_ID) return;
                    var model = grid.dataItem(this);
                    if (model.MC_FAB_PROD_ORD_H_ID == $stateParams.pMC_FAB_PROD_ORD_H_ID) {
                        grid.expandRow("[data-uid='" + model.uid + "']");
                    }
                });
            },
            scrollable: true,
            pageable: true,
            sortable: true,
            height: 500,
            columns: $state.current.data.RF_FAB_PROD_CAT_ID_LST.indexOf('2') > -1 ? columnsWithBkRpt : columnsWithOutBkRpt
        };


        function showGrid(MC_BYR_ACC_ID, MC_FAB_PROD_ORD_H_ID, RF_FAB_PROD_CAT_ID, FIRSTDATE, LK_COL_TYPE_ID_LST, pRF_FAB_TYPE_ID) {
            return vm.gridOptionsDs = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectAll/' + params.page + '/' + params.pageSize + '/' + MC_BYR_ACC_ID + '/' + MC_FAB_PROD_ORD_H_ID + '/' + RF_FAB_PROD_CAT_ID + '/null/' + FIRSTDATE + '?pLK_COL_TYPE_ID_LST=' + LK_COL_TYPE_ID_LST + '&pRF_FAB_TYPE_ID=' + pRF_FAB_TYPE_ID).then(function (res) {
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

        vm.navigateToKp = function (dataItem) {

            //if (dataItem.IS_FLAT_CIR == 'F') {
            //    return;
            //}
            if ($state.current.data.LK_COL_TYPE_ID_LST == '360' || dataItem.LK_COL_TYPE_ID == 360) {
                var url = '/Knitting/Knit/KnitPlanYd/#/KnitPlanYd?pMC_FAB_PROD_ORD_D_ID=' + dataItem.MC_FAB_PROD_ORD_D_ID + '&pRF_FAB_PROD_CAT_ID=' + dataItem.RF_FAB_PROD_CAT_ID;
            } else {
                if (dataItem.RF_FAB_PROD_CAT_ID == 8)
                    var url = '/Knitting/Knit/KnitPlan/#/KnitPlan?pMC_FAB_PROD_ORD_D_ID=' + dataItem.MC_FAB_PROD_ORD_D_ID + '&pRF_FAB_PROD_CAT_ID=' + dataItem.RF_FAB_PROD_CAT_ID;
                else
                    var url = '/Knitting/Knit/KnitPlan/#/KnitPlan?pMC_FAB_PROD_ORD_D_ID=' + dataItem.MC_FAB_PROD_ORD_D_ID + '&pRF_FAB_PROD_CAT_ID=' + dataItem.RF_FAB_PROD_CAT_ID;
            }

            url += '&state=' + $state.current.data.state;
            url += '&pCT_ID_LST=' + $state.current.data.LK_COL_TYPE_ID_LST;
            var a = document.createElement('a');
            a.href = url;
            a.target = '_self';
            document.body.appendChild(a);
            a.click();
        }

        vm.moveToYDKP = function (dataItem) {

            console.log(dataItem);

            var submitData = angular.copy(dataItem);

            Dialog.confirm('Do you want to move?', 'Confirmation', ['Yes', 'No']).then(function () {

                console.log(submitData);

                return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/MoveToYDKP').then(function (res) {
                    console.log(res);

                    vm.errors = undefined;
                    if (res.success === false) {
                        vm.errors = [];
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                            vm.gridOptionsDs.read();

                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            });


            //return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/MoveToYDKP?pMC_FAB_PROD_ORD_H_ID=' + dataItem.MC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + dataItem.FAB_COLOR_ID).then(function (res) {
            //    e.success(res.map(function (o) {
            //        o['RF_FAB_PROD_CAT_ID'] = dataItem.RF_FAB_PROD_CAT_ID;
            //        return o;
            //    }))
            //});
        }

        vm.detailGridOptionsForSupp = function (dataItem) {
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectByID?pMC_FAB_PROD_ORD_H_ID=' + dataItem.MC_FAB_PROD_ORD_H_ID + '&pLK_COL_TYPE_ID_LST=' + dataItem.LK_COL_TYPE_ID_LST + '&pHAS_YD=' + dataItem.HAS_YD + '&pRF_FAB_TYPE_ID=' + ($state.current.data.RF_FAB_TYPE_ID || '')).then(function (res) {
                                e.success(res.map(function (o) {
                                    o['RF_FAB_PROD_CAT_ID'] = dataItem.RF_FAB_PROD_CAT_ID;
                                    o['CURR_STATE'] = vm.state;
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
                columns: [
                { field: "COLOR_TYPE", title: "Type", type: "string", width: "8%", template: "#=COLOR_TYPE # # if( IS_BKING_APPROVE=='N') {#<span style=\"padding-bottom: 0px;padding-top: 0px;padding-bottom: 0px;display:block\" class=\"badge badge-warning\">Not Approve</span># } else if( IS_ACTIVE=='N') {#<span style=\"padding-bottom: 0px;padding-top: 0px;padding-bottom: 0px;display:block\" class=\"badge badge-default\">Inactive</span># }# " },
                //{ field: "COLOR_TYPE", title: "Type", width: "5%" },
                //{ field: "COLOR_GRP_NAME_EN", title: "Yarn Color Group", width: "7%" }, 
                //{ field: "COLOR_NAME_EN", title: "Fabric Color", width: "7%" },
                //{ field: "FAB_TYPE_NAME", title: "Fabric Type", width: "7%" },
                //{ field: "FIB_COMP_NAME", title: "Fiber Comp", width: "30%" },
                { field: "FABRIC_SNAME", title: "Fabrication", width: "23%", template: " <i><b> #=LK_FBR_GRP_TXT# || #=FAB_TYPE_NAME# || #=FEDER_TYPE_NAME# </b></i>  <br><i> #=FIB_COMP_NAME# </i> <br><small>#=FABRIC_SNAME # </small>" },
                { field: "FIN_GSM", title: "GSM", width: "7%" },
                { field: "FIN_DIA_N_DIA_TYPE", title: "Fin. Dia & Type", width: "7%" },
                { field: "FEDER_TYPE_NAME", title: "Feeder Type", width: "7%" },
                { field: "YD_TYPE_NAME", title: "Stripe Type", width: "7%" },
                { field: "KNT_PLAN_NO", title: "Kp Ref#", width: "7%" },
                { field: "NET_GFAB_QTY", title: "Req Qty", width: "7%" },
                { field: "GFAB_ADJ_QTY", title: "Adjusted", width: "7%" },
                { field: "BAL_GFAB_QTY", title: "Left2KP", width: "4%" },

                {
                    title: "Action",
                    template: function () {

                        return "<a  class='btn btn-xs blue' ng-disabled='dataItem.IS_BKING_APPROVE==\"N\"' ng-click='vm.navigateToKp(dataItem)'><i class='fa fa-edit'></i> Knit Plan</a>";
                    },
                    width: "7%"
                }

                ]
            };
        };
    }

})();