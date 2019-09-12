(function () {
    'use strict';
    angular.module('multitex.mrc').controller('AccessoriesBookingListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'Dialog', AccessoriesBookingListController]);
    function AccessoriesBookingListController($q, config, MrcDataService, $stateParams, $state, $scope, logger, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.form = {};
        activate()
        vm.showSplash = true;
        vm.form["ID"] = 0;
        function activate() {
            var promise = [getBuyerAccListData(), getStyleExtList(null), GetOrderTypeList(), getShipmentMonth(), getMOUList(), loadStyle()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        
        vm.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME #</p></span>';
        vm.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        function getStyleExtList(pMC_BYR_ACC_ID) {
            vm.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        //url += "&pRF_FAB_PROD_CAT_LST=1,3,5";

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

        function getMOUList() {
            vm.MOUList = {
                optionLabel: "Unt",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };

            vm.MOUListFake = {
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success([{ RF_MOU_ID: 5, MOU_CODE: 'Doz' }]);
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };

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

        //function styleListByBuyer(MC_BYR_ACC_ID) {

        //    MrcDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/0/0/0').then(function (res) {
        //        vm.shipMonthList = new kendo.data.DataSource({
        //            data: res
        //        })
        //    });

        //    if (!MC_BYR_ACC_ID) {
        //        vm.styleList = new kendo.data.DataSource({
        //            data: []
        //        })
        //        return;
        //    }

        //    MrcDataService.getDataByFullUrl('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + MC_BYR_ACC_ID + '/0').then(function (res) {
        //        vm.styleList = new kendo.data.DataSource({
        //            data: res
        //        })
        //    });
        //};

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

            vm.showSplash = true;
            var pFIRST_DATE = "";
            if (vm.form.MONTHOF) {
                var month = _.filter(vm.shipMonthList.data(), function (o) { return o.MONTHOF == vm.form.MONTHOF })

                if (month.length > 0) {
                    pFIRST_DATE = month[0].FIRSTDATE.split('T')[0];
                }
            }

            var path = '/api/mrc/AccBk/OrderListForAccBk/1/20/' + (vm.form.MC_BYR_ACC_ID || null) + '/' + (vm.form.STYLE_NO || null) + '/' + (vm.form.ORDER_NO || null) + '/' + (vm.form.ORDER_TYPE || null) + '/' + (vm.form.NATURE_OF_ORDER || null) + '/' + (pFIRST_DATE || null);

            MrcDataService.getDataByFullUrl(path).then(function (res) {
                vm.pendingFabProdOrderList = res.data;

                vm.showSplash = false;
                //console.log(vm.pendingFabProdOrderList);
            });


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



        vm.addAllNewRecoed = function (itemData) {
            vm.showSplash = true;
            console.log(itemData);
            itemData.IS_ACTIVE = true;
            MrcDataService.getDataByFullUrl('/api/mrc/AccBk/PurchaseReqList?pMC_STYLE_H_ID=' + itemData.MC_STYLE_H_ID + '&pMC_STYLE_H_EXT_ID=' + itemData.MC_STYLE_H_EXT_ID).then(function (res) {

                var item = angular.copy(res);
                itemData['AccesorriesList'] = item;
                vm.showSplash = false;

            });

        }

        function loadStyle() {

            vm.showSplash = true;
            var pFIRST_DATE = "";
            if (vm.form.MONTHOF) {
                var month = _.filter(vm.shipMonthList.data(), function (o) { return o.MONTHOF == vm.form.MONTHOF })

                if (month.length > 0) {
                    pFIRST_DATE = month[0].FIRSTDATE.split('T')[0];
                }
            }

            var path = '/api/mrc/AccBk/OrderListForAccBk/1/20/' + (vm.form.MC_BYR_ACC_ID || null) + '/' + (vm.form.STYLE_NO || null) + '/' + (vm.form.ORDER_NO || null) + '/' + (vm.form.ORDER_TYPE || null) + '/' + (vm.form.NATURE_OF_ORDER || null) + '/' + (pFIRST_DATE || null);

            MrcDataService.getDataByFullUrl(path).then(function (res) {
                vm.pendingFabProdOrderList = res.data;
                //console.log(vm.pendingFabProdOrderList);

                vm.showSplash = false;
            });
        }


        vm.generatePO = function (item) {

            var pINV_ITEM_ID = 0;

            pINV_ITEM_ID = item.INV_ITEM_LST.split(',')[0];

            var url = '/Merchandising/Mrc/TaBooking/#/TaBk/BgtHrd/0/PoHrd/' + item.SCM_PURC_REQ_H_ID + '/Item/' + pINV_ITEM_ID + '?pBLK_BOM_LIST=' + item.INV_ITEM_LST + '&pBLK_BOM_ACT=' + pINV_ITEM_ID + '&pMC_STYLE_H_ID=' + item.MC_STYLE_H_ID + '&pMC_FAB_PROD_ORD_H_ID=' + item.MC_FAB_PROD_ORD_H_ID + '&pIS_ACC_BK_BOM=1';
            console.log(url);
            var a = document.createElement('a');
            a.href = url;
            a.target = '_blank';
            document.body.appendChild(a);
            a.click();

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