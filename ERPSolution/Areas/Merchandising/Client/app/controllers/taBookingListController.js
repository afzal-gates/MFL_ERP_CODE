/// <reference path="taBookingListController.js" />
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TaBookingListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', '$window', TaBookingListController]);
    function TaBookingListController($q, config, MrcDataService, $stateParams, $state, $scope, $window) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        var OLD_MC_BYR_ACC_ID = 0;
        vm.form = {};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getbuyerAcList(), getOrderShipmentMonth(null)];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO #</h5><p> Style: #: data.STYLE_NO #</p></span>';

        function getbuyerAcList() {
            return vm.buyerAcList = {
                optionLabel: "--- Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.getDataByUrl('/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var ds = this.dataSource.data();
                    if (ds.length == 1) {
                        vm.onSearch({ MC_BYR_ACC_ID: ds[0].MC_BYR_ACC_ID });
                        onBuyerAccChange(ds[0].MC_BYR_ACC_ID);
                        getOrderShipmentMonth(ds[0].MC_BYR_ACC_ID);
                        vm.getPoHeaderList({ MC_BYR_ACC_ID: ds[0].MC_BYR_ACC_ID });
                    }
                },
                change: function (e) {
                    var item = this.dataItem(e.item);
                    onBuyerAccChange((item.MC_BYR_ACC_ID || null));
                    getOrderShipmentMonth((item.MC_BYR_ACC_ID || null));
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }


        vm.oderListDs = {
            transport: {
                read: function (e) {
                    var url = "/api/common/getOrderStyleDropDownData?pMC_BYR_ACC_ID";
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    if (params.filter) {
                        url += '&pORDER_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '&pORDER_NO';
                    }

                    MrcDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            serverFiltering: true
        };


        function onBuyerAccChange(MC_BYR_ACC_ID, FIRSTDATE, LASTDATE) {
            vm.oderListDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/common/getOrderStyleDropDownData?pMC_BYR_ACC_ID=" + (MC_BYR_ACC_ID || null);
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        if (params.filter) {
                            url += '&pORDER_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pORDER_NO';
                        }

                        url += '&pOption=3002'
                        if (FIRSTDATE && LASTDATE) {
                            url += '&pFIRSTDATE=' + FIRSTDATE;
                            url += '&pLASTDATE=' + LASTDATE;
                        }

                        MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            };


        };

        function getOrderShipmentMonth(pMC_BYR_ACC_ID) {
            return MrcDataService.getDataByFullUrl('/api/mrc/Order/OrderShipmentMonth?pMC_BYR_ACC_ID=' + (pMC_BYR_ACC_ID || null)).then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        }


        vm.onChangeShipMonth = function (data, e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.hasOwnProperty('MONTHOF') && item.MONTHOF) {
                data['FIRSTDATE'] = item.FIRSTDATE;
                data['LASTDATE'] = item.LASTDATE;
                onBuyerAccChange(data.MC_BYR_ACC_ID, item.FIRSTDATE, item.LASTDATE);
            } else {
                vm.form['FIRSTDATE'] = null;
                vm.form['LASTDATE'] = null;
                onBuyerAccChange(data.MC_BYR_ACC_ID, null, null);
            }
        };


        vm.openReport = function (pMC_ACCS_PO_H_ID) {
            $window.open('/Merchandising/Mrc/TaBookingRpt/' + pSCM_PURC_REQ_H_ID + '/#/RptView?pSCM_PURC_REQ_H_ID=' + pSCM_PURC_REQ_H_ID, "_blank", "location=yes,height=570,width=800,scrollbars=yes,status=yes");
        };

        vm.openReportPo = function (pSCM_PURC_REQ_H_ID) {
            $window.open('/Merchandising/Mrc/TaBookingPoRpt/' + pSCM_PURC_REQ_H_ID + '/#/RptPoView?pSCM_PURC_REQ_H_ID=' + pSCM_PURC_REQ_H_ID, "_blank", "location=yes,height=570,width=800,scrollbars=yes,status=yes");
        };



        
        vm.getPoHeaderList = function (data) {

            vm.taBookingPoHrdDs = new kendo.data.DataSource({
                    serverPaging: true,
                    serverFiltering: true,
                    schema: {
                        data: "data",
                        total: "total",
                        model: {
                            fields: {
                                ACCS_PO_DT: { type: "date" },
                                ACCS_DELV_DT: { type: "date" }
                            }
                        }
                    },
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            var url = '/TaBooking/AccPoHeaderList';
                            url += '?pMC_BYR_ACC_ID=' + (data.MC_BYR_ACC_ID || '');
                            url += '&pMC_ORDER_H_ID=' + (data.MC_ORDER_H_ID || '');
                            url += '&pFIRSTDATE=' + (data.FIRSTDATE || null);
                            url += '&pLASTDATE=' + (data.LASTDATE || null);
                            url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                            url += MrcDataService.kFilterStr2QueryParam(params.filter);
                            MrcDataService.getDataByUrl(url).then(function (res) {
                                e.success(res);
                            })
                        }
                    },
                    pageSize: 10
                });
        }


        if ($stateParams.pMC_BYR_ACC_ID && $stateParams.pMC_BYR_ACC_ID > 0) {
            vm.form['MC_BYR_ACC_ID'] = $stateParams.pMC_BYR_ACC_ID;
            vm.getPoHeaderList({ MC_BYR_ACC_ID : $stateParams.pMC_BYR_ACC_ID});
        }




        function getStyleExtList(pMC_BYR_ACC_ID, pMC_BUYER_ID) {

            if (pMC_BUYER_ID) {
               return MrcDataService.getDataByFullUrl('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + pMC_BYR_ACC_ID + '/' + pMC_BUYER_ID).then(function (res) {
                    vm.StyleExtDs = new kendo.data.DataSource({
                        data: res
                    });
                });
            }

        };

        vm.taBookingPoHrdOpt = {
            sortable: true,
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
            scrollable: true,
            pageable:true,
            columns: [
                //{ field: "BASE_STYLE", title: "Style", width: "60px" },
                { field: "WORK_STYLE", title: "Style", width: "50px", filterable:false},
                { field: "ORDER_NO_LIST", title: "Order#", width: "50px", filterable: false },
                {
                    title: "PO #",
                    field: "ACCS_PO_NO",
                    template: function () {
                        return "<a ui-sref='TaBooking.item({pMC_STYL_BGT_H_ID: dataItem.MC_STYL_BGT_H_ID, pMC_ACCS_PO_H_ID : dataItem.MC_ACCS_PO_H_ID,pBLK_BOM_LIST:dataItem.BLK_BOM_LIST,pBLK_BOM_ACT: dataItem.BLK_BOM_ACT, pINV_ITEM_ID :  dataItem.ITEM_ACT, pMC_STYLE_H_ID: dataItem.MC_STYLE_H_ID})' class='btn btn-xs btn-link' title='Edit PO #'> {{dataItem.ACCS_PO_NO}}</a>";
                    },
                    width: "60px",
                    filterable:false
                },
                { field: "ACCS_PO_SUB", title: "Subject of PO", width: "100px" },
                { field: "ACCS_PO_DT", title: "PO Date", width: "50px", format: "{0:dd-MMM-yyyy}" },
                { field: "ACCS_DELV_DT", title: "Del.Date", width: "50px", format: "{0:dd-MMM-yyyy}" },
                //{ field: "STORE_NAME_EN", title: "Del. Place", width: "80px" },
                //{ field: "LK_PURCH_TYPE_NAME", title: "Pur. Type", width: "40px" },
                //{
                //    field: "IS_SUPP_ONLINE", title: "Online?", width: "40px"
                //},

                { field: "TASK_STATUS_NAME", title: "Status", width: "80px"},
                
                //{
                //    title: "",
                //    template: function () {
                //        return "<a class='btn btn-xs btn-link' title='Print Booking Sheet' ng-click='vm.openReport(dataItem.MC_ACCS_PO_H_ID)'> <i class=\"fa fa-file-pdf-o\"></i> Booking</a>";
                //    },
                //    width: "40px"
                //},
                {
                    title: "",
                    template: function () {
                        return "<a class='btn btn-xs btn-link' title='Print Booking Sheet' ng-click='vm.openReportPo(dataItem.SCM_PURC_REQ_H_ID)'> <i class=\"fa fa-file-pdf-o\"></i> PO</a>";
                    },
                    width: "30px"
                },

            ]
        };



        vm.taBookingPoHrdDtl = function (Item) {
            return {
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByUrl('/TaBooking/getItemForPurchaseList?pSCM_PURC_REQ_H_ID=' + Item.SCM_PURC_REQ_H_ID).then(function (res) {
                                e.success(res.map(function (o) {
                                    o['MC_STYL_BGT_H_ID'] = Item.MC_STYL_BGT_H_ID;
                                    o['MC_STYLE_H_ID'] = Item.MC_STYLE_H_ID;

                                    o['SCM_PURC_REQ_H_ID'] = Item.SCM_PURC_REQ_H_ID;
                                    o['BLK_BOM_LIST'] = Item.BLK_BOM_LIST;
                                    return o;
                                }))
                            })
                        }
                    }
                },
                scrollable: true,
                columns: [
                { field: "SL", title: "SL #", type: "string", width: "10px" },
                {
                    title: "Item Description",
                    field: "ITEM_NAME_EN",
                    template: function () {
                        return "<a ui-sref='TaBooking.item({pMC_STYL_BGT_H_ID: dataItem.MC_STYL_BGT_H_ID, pMC_ACCS_PO_H_ID : dataItem.MC_ACCS_PO_H_ID,pBLK_BOM_LIST:dataItem.BLK_BOM_LIST,pBLK_BOM_ACT: dataItem.BLK_BOM_ID, pINV_ITEM_ID :  dataItem.INV_ITEM_ID,  pMC_STYLE_H_ID: dataItem.MC_STYLE_H_ID})' class='btn btn-xs btn-link' title='Edit PO #'>{{dataItem.ITEM_NAME_EN}}</a>";
                    },
                    width: "120px"
                }
                ]
            };
        }



        //vm.taBookingPoHrdDtl = function (Item) {
        //    return {
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    MrcDataService.getDataByUrl('/TaBooking/getItemForList?pMC_ACCS_PO_H_ID=' + Item.MC_ACCS_PO_H_ID).then(function (res) {
        //                        e.success(res.map(function (o) {
        //                            o['MC_STYL_BGT_H_ID'] = Item.MC_STYL_BGT_H_ID;
        //                            o['MC_STYLE_H_ID'] = Item.MC_STYLE_H_ID;

        //                            o['MC_ACCS_PO_H_ID'] = Item.MC_ACCS_PO_H_ID;
        //                            o['BLK_BOM_LIST'] =  Item.BLK_BOM_LIST;
        //                            return o;
        //                        }))
        //                    })
        //                }
        //            }
        //        },
        //        scrollable: true,
        //        columns: [
        //        { field: "SL", title: "SL #", type: "string", width: "10px" },
        //        {
        //            title: "Item Description",
        //            field: "ITEM_NAME_EN",
        //            template: function () {
        //                return "<a ui-sref='TaBooking.item({pMC_STYL_BGT_H_ID: dataItem.MC_STYL_BGT_H_ID, pMC_ACCS_PO_H_ID : dataItem.MC_ACCS_PO_H_ID,pBLK_BOM_LIST:dataItem.BLK_BOM_LIST,pBLK_BOM_ACT: dataItem.BLK_BOM_ID, pINV_ITEM_ID :  dataItem.INV_ITEM_ID,  pMC_STYLE_H_ID: dataItem.MC_STYLE_H_ID})' class='btn btn-xs btn-link' title='Edit PO #'>{{dataItem.ITEM_NAME_EN}}</a>";
        //            },
        //            width: "120px"
        //        }
        //        ]
        //    };
        //};

    }

})();