(function () {
    'use strict';
    angular.module('multitex.mrc').controller('OrderSCMController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'Dialog', OrderSCMController]);
    function OrderSCMController($q, config, MrcDataService, $stateParams, $state, $scope, logger, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.form = {};
        activate()
        vm.showSplash = true;
        vm.form["ID"] = 0;
        function activate() {
            var promise = [GetOrderTypeList(), getBuyerAccListData(), styleListByBuyer(), getShipmentMonth()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }



        function getBuyerAccListData() {
            return vm.buyerAccList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.styleListByBuyerAC = function (MC_BYR_ACC_ID) {

            getShipmentMonth();

            if (MC_BYR_ACC_ID.length == 0) {
                vm.styleList = new kendo.data.DataSource({
                    data: []
                })
                return;
            }

            MrcDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (vm.form.MC_BYR_ACC_ID || 0) + '/' + (vm.form.MC_STYLE_H_ID || 0) + '/' + (vm.form.RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });


            MrcDataService.getDataByFullUrl('/api/mrc/StyleH/BuyerAccWiseStyleListData/' + MC_BYR_ACC_ID).then(function (res) {
                console.log(res);
                vm.styleList = new kendo.data.DataSource({
                    data: res
                })
            })
        };

        vm.getDataByStyleId = function () {
            getShipmentMonth();
        }

        function getShipmentMonth() {
            var pBYR_ACC_NAME_EN = "";
            var SHIP_MONTH = "";

            var bList = _.filter(vm.buyerAccList.data(), function (o) { return o.MC_BYR_ACC_ID == vm.form.MC_BYR_ACC_ID })
            if (bList.length > 0)
                pBYR_ACC_NAME_EN = bList[0].BYR_ACC_NAME_EN;

            MrcDataService.getDataByFullUrl('/api/mrc/Order/SelectShipmentMonth/' + (vm.form.MC_BYR_ACC_ID || null) + '/' + (vm.form.STYLE_NO || null) + '/' + (vm.form.ORDER_NO || null) + '/' + (vm.form.ORDER_TYPE || null) + '/' + (vm.form.NATURE_OF_ORDER || null)).then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        }

        function styleListByBuyer(MC_BYR_ACC_ID) {

            MrcDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/0/0/0').then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });

            if (!MC_BYR_ACC_ID) {
                vm.styleList = new kendo.data.DataSource({
                    data: []
                })
                return;
            }

            MrcDataService.getDataByFullUrl('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + MC_BYR_ACC_ID + '/0').then(function (res) {
                vm.styleList = new kendo.data.DataSource({
                    data: res
                })
            });
        };

        function GetOrderTypeList() {
            return vm.OrderTypeList = {
                optionLabel: "-- Select Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(40).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LK_DATA_NAME_EN"
            };

        };



        vm.showOrderScmList = function () {
            vm.gridOptions.dataSource.read();
        }

        vm.clearSearch = function () {
            vm.form['MC_BYR_ACC_ID'] = '';
            vm.form['STYLE_NO'] = '';
            vm.form['ORDER_TYPE'] = '';
            vm.form['MONTHOF'] = '';
            vm.form['ORDER_NO'] = '';
            vm.form['NATURE_OF_ORDER'] = '';

        }


        function getTnaTaskStatusList() {
            MrcDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectByTnaID?pMC_TNA_TASK_ID=7').then(function (res) {
                vm.TnaTaskStatusList = res;
            }, function (err) {
                console.log(err);
            });

        }

        function parentTnaTaskStatusList() {
            vm.parentList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectByTnaID?pMC_TNA_TASK_ID=0').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.getTnaTaskStatusList = function (dataItem) {
            //vm.form.MC_TNA_TASK_ID = data.MC_TNA_TASK_ID;
            var itemss = dataItem;

            vm.form = { 'MC_TNA_TASK_ID': itemss.MC_TNA_TASK_ID };

            //vm.showSplash = true;

            MrcDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectByTnaID?pMC_TNA_TASK_ID=' + itemss.MC_TNA_TASK_ID).then(function (res) {
                vm.TnaTaskStatusList = res;
                vm.parentList = res
                vm.showSplash = false;
            }, function (err) {
                console.log(err);
                vm.showSplash = false;
            });
            //
        }

        vm.editTnaTaskStatus = function (data) {
            vm.form = angular.copy(data);
        }

        vm.reset = function (data) {
            vm.form = { 'MC_TNA_TASK_ID': data.MC_TNA_TASK_ID };
        }
        
        vm.gridOptions = {
            dataSource: new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {

                        vm.showSplash = true;
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = MrcDataService.kFilterStr2QueryParam(params.filter);
                        var pBYR_ACC_NAME_EN = "";

                        var pFIRST_DATE = "";
                        if (vm.form.MONTHOF) {
                            var month = _.filter(vm.shipMonthList.data(), function (o) { return o.MONTHOF == vm.form.MONTHOF })

                            if (month.length > 0) {
                                pFIRST_DATE = month[0].FIRSTDATE.split('T')[0];
                            }
                        }

                        var path = '/api/mrc/Order/OrderListForSCM/' + params.page + '/' + params.pageSize + '/' + (vm.form.MC_BYR_ACC_ID || null) + '/' + (vm.form.STYLE_NO || null) + '/' + (vm.form.ORDER_NO || null) + '/' + (vm.form.ORDER_TYPE || null) + '/' + (vm.form.NATURE_OF_ORDER || null) + '/' + (pFIRST_DATE || null);

                        MrcDataService.getDataByFullUrl(path).then(function (res) {
                            //vm.OrderScmList = new kendo.data.DataSource({
                            //    data: res
                            //});
                            e.success(res);
                            vm.showSplash = false;
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            }),

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
            resizable: true,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInit,
            columns: [
                { field: "MC_STYLE_H_ID", hidden: true },

                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "10%" },
                { field: "STYLE_DESC", title: "Style Name", width: "10%" },
                { field: "STYLE_NO", title: "Style #", width: "8%" },
                { field: "ORDER_NO", title: "Order #", width: "8%" },
                { field: "ORDER_TYPE_NAME_EN", title: "Order Type", width: "8%" },

                //{ field: "NATURE_OF_ORDER", title: "Nature of Style", width: "8%" /*, filterable: { ui: FilterOptionOrderNature }*/ },
                { field: "TOT_ORD_QTY", title: "Order Qty.", width: "10%", filterable: false },
                { field: "ORD_CONF_DT", title: "Order Conf. Date", type: "date", template: "#= kendo.toString(kendo.parseDate(ORD_CONF_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filterable: false },
                { field: "SHIP_DT", title: "Shipment Date", type: "date", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filterable: false },
                { field: "LEAD_TIME", title: "Lead Time", width: "8%", filterable: false },
                //{ field: "T_N_A", title: "T&A", width: "8%" },
                //{ field: "DETAILS", title: "[Details]", width: "8%" }
                //,
                {
                    title: "Details",
                    template: '<a  title="Edit" ng-click="vm.editTnaTaskStatus(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ]
        };



        vm.mainGridOptions = function (e) {
            console.log(e);
            return {
                dataSource: {
                    transport: {
                        read: function (e) {

                            MrcDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectByTnaID?pMC_TNA_TASK_ID=' + dataItem.MC_TNA_TASK_ID).then(function (res) {
                                e.success(res);

                            });
                        }
                    },
                    pageSize: 5,
                    serverPaging: true,
                    serverSorting: true
                },
                sortable: true,
                pageable: true,
                columns: [
                    { field: "MC_TNA_TASK_STATUS_ID", hidden: true },
                    { field: "MC_TNA_TASK_ID", hidden: true },
                    { field: "PARENT_ID", hidden: true },
                    { field: "EVENT_NAME", title: "EVENT NAME", width: "20%" },
                    { field: "TASK_STATUS_NAME", title: "TASK STATUS", width: "10%" },
                    { field: "STATUS_ORDER", title: "STATUS ORDER", width: "12%" }
                ]
            }
        };



        function FilterOptionBuyer(element) {

            //var result = [];
            //vm.OrderScmList.data().forEach(function (x) { if (!result.includes(x.BYR_ACC_NAME_EN)) result.push(x.BYR_ACC_NAME_EN) })

            //element.kendoDropDownList({
            //    dataSource: result,
            //    optionLabel: "--Select Buyer--",
            //    filter: "Contains"
            //})
        }

        function FilterOptionStyleDes(element) {

            //var result = [];
            //vm.OrderScmList.data().forEach(function (x) { if (!result.includes(x.STYLE_DESC)) result.push(x.STYLE_DESC) })

            //element.kendoDropDownList({
            //    dataSource: result,
            //    optionLabel: "--Select Style Des--",
            //    filter: "Contains"
            //})
        }
        function FilterOptionStyleNo(element) {

            //var result = [];
            //vm.OrderScmList.data().forEach(function (x) { if (!result.includes(x.STYLE_NO)) result.push(x.STYLE_NO) })

            //element.kendoDropDownList({
            //    dataSource: result,
            //    optionLabel: "--Select Style No--",
            //    filter: "Contains"
            //})
        }

        function FilterOptionOrder(element) {

            //var result = [];
            //vm.OrderScmList.data().forEach(function (x) { if (!result.includes(x.ORDER_NO)) result.push(x.ORDER_NO) })

            //element.kendoDropDownList({
            //    dataSource: result,
            //    optionLabel: "--Select Order No--",
            //    filter: "Contains"
            //})
        }

        function FilterOptionOrderType(element) {

            //var result = [];
            //vm.OrderScmList.data().forEach(function (x) { if (!result.includes(x.ORDER_TYPE_NAME_EN)) result.push(x.ORDER_TYPE_NAME_EN) })

            //element.kendoDropDownList({
            //    dataSource: result,
            //    optionLabel: "--Select Order Type--",
            //    filter: "Contains"
            //})
        }

        //function FilterOptionOrderNature(element) {

        //    var result = [];
        //    vm.OrderScmList.data().forEach(function (x) { if (!result.includes(x.NATURE_OF_ORDER)) result.push(x.NATURE_OF_ORDER) })

        //    element.kendoDropDownList({
        //        dataSource: result,
        //        optionLabel: "--Select Order Nature--",
        //        filter: "Contains"
        //    })
        //}

        function detailInit(ev) {
            var MC_STYLE_H_ID = ev.data.MC_STYLE_H_ID;
            var MC_BUYER_ID = ev.data.MC_BUYER_ID;

            var showDetails = function (eee) {



                var dataItem = this.dataItem($(eee.currentTarget).closest("tr"));

                if (dataItem.MC_TNA_TASK_STATUS_ID != 25) {
                    var url = '/Merchandising/Mrc/TaBooking?a=107/#/TaBk/BgtHrd/' + dataItem.MC_STYL_BGT_H_ID;
                    url += '/PoHrd/' + dataItem.MC_ACCS_PO_H_ID;
                    url += '/Item/' + dataItem.ITEM_ACT;
                    url += '?pBLK_BOM_LIST=' + dataItem.BLK_BOM_LIST;
                    url += '&pBLK_BOM_ACT=' + dataItem.BLK_BOM_ACT;
                    url += '&pMC_STYLE_H_ID=' + dataItem.MC_STYLE_H_ID;
                    var a = document.createElement('a');
                    a.href = url;
                    a.target = '_blank';
                    document.body.appendChild(a);
                    a.click();
                    return;

                } else {
                          Dialog.confirm('Start sourcing? <br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                             .then(function () {
                                 MrcDataService.getDataByUrl('/TaBooking/setTnAStatus?pMC_ACCS_PO_H_ID=' + dataItem.MC_ACCS_PO_H_ID + '&pMC_TNA_TASK_STATUS_ID=26').then(function (res) {
                                     var url = '/Merchandising/Mrc/TaBooking?a=107/#/TaBk/BgtHrd/' + dataItem.MC_STYL_BGT_H_ID;
                                     url += '/PoHrd/' + dataItem.MC_ACCS_PO_H_ID;
                                     url += '/Item/' + dataItem.ITEM_ACT;
                                     url += '?pBLK_BOM_LIST=' + dataItem.BLK_BOM_LIST;
                                     url += '&pBLK_BOM_ACT=' + dataItem.BLK_BOM_ACT;
                                     url += '&pMC_STYLE_H_ID=' + dataItem.MC_STYLE_H_ID;
                                     var a = document.createElement('a');
                                     a.href = url;
                                     a.target = '_blank';
                                     document.body.appendChild(a);
                                     a.click();
                                 });

                             }, function () {

                                 var url = '/Merchandising/Mrc/TaBooking?a=107/#/TaBk/BgtHrd/' + dataItem.MC_STYL_BGT_H_ID;
                                 url += '/PoHrd/' + dataItem.MC_ACCS_PO_H_ID;
                                 url += '/Item/' + dataItem.ITEM_ACT;
                                 url += '?pBLK_BOM_LIST=' + dataItem.BLK_BOM_LIST;
                                 url += '&pBLK_BOM_ACT=' + dataItem.BLK_BOM_ACT;
                                 url += '&pMC_STYLE_H_ID=' + dataItem.MC_STYLE_H_ID;
                                 var a = document.createElement('a');
                                 a.href = url;
                                 a.target = '_blank';
                                 document.body.appendChild(a);
                                 a.click();
                             });
                }
            };


            var deltailRow = ev.detailRow;
            var i = 1;

            deltailRow.find(".tabstrip").kendoTabStrip({
                animation: {
                    open: { effects: "fadeIn" }
                }
            });

            deltailRow.find(".orders").kendoGrid({
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/mrc/StyleDBOM/BOMListByID/' + MC_STYLE_H_ID).then(function (res) {
                                e.success(res);
                            });
                        }
                    },
                },
                sortable: false,
                columns: [
                    { field: "INV_ITEM_ID", hidden: true },
                    {
                        title: "SL No",
                        template: function () {
                            return i++;
                        },
                        width: "5%"
                    },
                    { field: "ITEM_NAME_EN", title: "Trim & Accessories", width: "30%" }
                ]
            });


            deltailRow.find(".PO_HEADER").kendoGrid({
                sortable: true,

                dataSource: {
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
                            var url = '/TaBooking/AccPoHeaderList?pMC_BYR_ACC_ID=' + ev.data.MC_BYR_ACC_ID;
                            url += '&pMC_BUYER_ID=' + (ev.data.pMC_BUYER_ID || null);
                            url += '&pMC_STYLE_H_EXT_ID=' + ev.data.MC_STYLE_H_EXT_ID;
                            //url += '&pMC_TNA_TASK_STATUS_ID=25';
                            // 26 Start Sourcing

                            url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                            url += MrcDataService.kFilterStr2QueryParam(params.filter);
                            MrcDataService.getDataByUrl(url).then(function (res) {
                                e.success(res);
                            })
                        }
                    },
                    pageSize: 10
                },

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
                pageable: true,
                columns: [
                    { field: "ACCS_PO_NO", title: "PO #", width: "80px", filterable:false },
                    { field: "ACCS_PO_SUB", title: "Subject of PO", width: "100px", filterable: false },
                    { field: "ACCS_PO_DT", title: "Create Date", width: "50px", format: "{0:dd-MMM-yyyy}" },
                    { field: "ACCS_DELV_DT", title: "Del. Date", width: "50px", format: "{0:dd-MMM-yyyy}" },
                    { field: "STORE_NAME_EN", title: "Del. Place", width: "80px" },
                    //{ field: "LK_PURCH_TYPE_NAME", title: "Pur. Type", width: "40px" },
                    { field: "TASK_STATUS_NAME", title: "Status", width: "80px" },
                    //{
                    //    field: "IS_SUPP_ONLINE", title: "Onl?", width: "30px"
                    //},
                    { command: { text: "Navigate", click: showDetails }, title: "Action", width: "50px" }
                ]
            });
        }





        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var id = vm.form.MC_TNA_TASK_STATUS_ID

                return MrcDataService.saveDataByFullUrl(data, '/api/mrc/TnaTaskStatus/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg("MULTI-001 TNA Task Status has been updated successfully");
                        }
                        else {
                            config.appToastMsg('MULTI-001' + res.data.PMSG);
                        }

                        vm.getTnaTaskStatusList(data);
                    }
                });
            }
        };

    }

})();