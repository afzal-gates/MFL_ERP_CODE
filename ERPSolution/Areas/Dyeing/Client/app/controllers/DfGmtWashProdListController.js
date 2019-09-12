
(function () {
    'use strict';
    angular.module('multitex.ie').controller('DfGmtWashProdListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'cur_user_id', DfGmtWashProdListController]);
    function DfGmtWashProdListController($q, config, DyeingDataService, $stateParams, $state, $scope, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        var V_MC_BYR_ACC_ID;
        vm.form = {
            FIRSTDATE: null,
            LASTDATE: null
        };


        vm.template = '<span><b><h5 style="padding-top:4px;margin:0px;">#: kendo.toString(new Date(data.SHIP_DT), "dd-MM-yyyy") #</h5></b></span>';
        vm.valueTemplate = '<span><b><h5 style="padding-top:4px;margin:0px;">#: kendo.toString(new Date(data.SHIP_DT), "dd-MM-yyyy") #</h5></b></span>';
        vm.templateOrder = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO #</h5><p> (#: data.FAB_PROD_CAT_SNAME #)</p></span>';
        vm.valueTemplateOrder = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';
        vm.templateStyle = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.FAB_PROD_CAT_SNAME #)</p></span>';
        vm.valueTemplateStyle = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [requisitionList(), getStyleExtList(0), getOrderInfoList(), getShipmentInfoList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.WASH_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.WASH_REQ_DT_LNopened = true;
        }

        vm.buyerAcGrpList = {
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
                    //vm.OrderDs.read();
                }
            },
            dataTextField: "BYR_ACC_GRP_NAME_EN",
            dataValueField: "MC_BYR_ACC_GRP_ID"
        };

        function getStyleExtList(pMC_BYR_ACC_GRP_ID) {

            vm.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=" + (vm.form.RF_FAB_PROD_CAT_LST || "1,2,3,4,5,6,7,8,9,10,11,12");
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

        function getOrderInfoList() {
            return vm.OrderDs = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=" + (vm.form.MC_BYR_ACC_GRP_ID || 1);
                        url += "&pMC_STYLE_H_EXT_ID=" + (vm.form.MC_STYLE_H_EXT_ID || 1);
                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }

        vm.getOrderList = function (e) {
            var obj = e.sender.dataItem(e.item);
            vm.form.MC_STYLE_H_EXT_ID = obj.MC_STYLE_H_EXT_ID;
            //console.log(obj);
            vm.OrderDs.read();
        };


        function getShipmentInfoList() {
            return vm.shipmentDateDs = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/Order/GetOrdShipList?pMC_ORDER_H_ID=" + (vm.form.MC_ORDER_H_ID || 1);
                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            if (res.length == 1)
                                vm.form.MC_ORDER_SHIP_ID = res[0].MC_ORDER_SHIP_ID;
                            e.success(res);
                        });
                    }
                }
            });
        }

        vm.getShipmentList = function (e) {
            var obj = e.sender.dataItem(e.item);
            vm.form.MC_ORDER_H_ID = obj.MC_ORDER_H_ID;
            vm.shipmentDateDs.read();
        };


        vm.searchData = function () {

            var pm = "";
            if (vm.form.WASH_REQ_NO)
                pm = pm + "&pWASH_REQ_NO=" + vm.form.WASH_REQ_NO;
            if (vm.form.WASH_REQ_DT) {
                var _isudate = $filter('date')(vm.form.WASH_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pWASH_REQ_DT=" + _isudate;
            }
            if (vm.form.MC_ORDER_H_ID)
                pm = pm + "&pMC_ORDER_H_ID=" + vm.form.MC_ORDER_H_ID;
            if (vm.form.MC_STYLE_H_EXT_ID)
                pm = pm + "&pMC_STYLE_H_EXT_ID=" + vm.form.MC_STYLE_H_EXT_ID;
            if (vm.form.MC_BYR_ACC_GRP_ID)
                pm = pm + "&pMC_BYR_ACC_GRP_ID=" + vm.form.MC_BYR_ACC_GRP_ID;
            if (vm.form.MC_ORDER_SHIP_ID)
                pm = pm + "&pMC_ORDER_SHIP_ID=" + vm.form.MC_ORDER_SHIP_ID;

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        DyeingDataService.getDataByFullUrl('/api/ie/GmtWashReq/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            })
        }

        function requisitionList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl('/api/ie/GmtWashReq/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            })
        }


        vm.gridOptions = {
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            sortable: true,
            columns: [
                { field: "WASH_REQ_NO", title: "REQ No", type: "string", width: "10%" },
                { field: "WASH_REQ_DT", title: "Req Date", type: "date", template: "#= kendo.toString(kendo.parseDate(WASH_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer", type: "string", width: "10%" },
                { field: "STYLE_NO", title: "Style #", type: "string", width: "10%" },
                { field: "ORDER_NO", title: "Order #", type: "string", width: "10%" },
                { field: "SHIP_DT", title: "Ship Date", type: "date", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },

                { field: "WASH_RQD_QTY", title: "Order Qty", type: "string", width: "10%" },
                { field: "TGT_ST_DT", title: "Start Date", type: "date", template: "#= kendo.toString(kendo.parseDate(TGT_ST_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "TGT_END_DT", title: "Close Date", type: "date", template: "#= kendo.toString(kendo.parseDate(TGT_END_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },

                { field: "REMARKS", title: "Remarks", type: "string", width: "10%" },
                {
                    field: "EVENT_NAME", title: "Status", type: "string", width: "10%"
                    , template: '<span ng-class="{' + "'grid_dn'" + ':dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ',' + "'grid_ri'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SR'" + ',' + "'grid_si'" + ':dataItem.ACTN_ROLE_FLAG==' + "'SI'" + ',' + "'grid_ra'" + ':dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ',' + "'grid_wt'" + ':dataItem.ACTN_ROLE_FLAG.length==' + "'0'" + '}">#=EVENT_NAME #  </span>'
                },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="DfGmtWashProd({pGMT_DF_WASH_REQ_ID:dataItem.GMT_DF_WASH_REQ_ID, pDF_GMT_WASH_PROD_ID:0})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> Add Production</i></a>';
                            //' <a ng-click="vm.printRDLC(dataItem);" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a></a>';
                    },
                    width: "15%"
                }
            ]
        };


        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6011';
            vm.form.GMT_DF_WASH_REQ_ID = dataItem.GMT_DF_WASH_REQ_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/dye/DyeReport/PreviewReportRDLC');
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


    }

})();
