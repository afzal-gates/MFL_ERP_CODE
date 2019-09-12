(function () {
    'use strict';
    angular.module('multitex.mrc').controller('AccessoriesBookingController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'Dialog', AccessoriesBookingController]);
    function AccessoriesBookingController($q, config, MrcDataService, $stateParams, $state, $scope, logger, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.form = {};
        activate()
        vm.showSplash = true;
        vm.form["ID"] = 0;
        function activate() {
            var promise = [getBuyerAccListData(), getStyleExtList(999), GetOrderTypeList(), getShipmentMonth(), getMOUList(), loadStyle()];
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
                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
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
            getStyleExtList((MC_BYR_ACC_ID || null));

            //MrcDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (vm.form.MC_BYR_ACC_ID || 0) + '/' + (vm.form.MC_STYLE_H_ID || 0) + '/' + (vm.form.RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
            //    vm.shipMonthList = new kendo.data.DataSource({
            //        data: res
            //    })
            //});

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
            //var pBYR_ACC_NAME_EN = "";
            //var SHIP_MONTH = "";

            //var bList = _.filter(vm.buyerAccList.data(), function (o) { return o.MC_BYR_ACC_ID == vm.form.MC_BYR_ACC_ID })
            //if (bList.length > 0)
            //    pBYR_ACC_NAME_EN = bList[0].BYR_ACC_NAME_EN;

            MrcDataService.getDataByFullUrl('/api/mrc/Order/SelectShipmentMonth/' + (vm.form.MC_BYR_ACC_ID || 0) + '/' + (vm.form.STYLE_NO || null) + '/' + (vm.form.ORDER_NO || null) + '/' + (vm.form.ORDER_TYPE || null) + '/' + (vm.form.NATURE_OF_ORDER || null)).then(function (res) {
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

            var path = '/api/mrc/AccBk/OrderListForAccBk/1/20/' + (vm.form.MC_BYR_ACC_ID || null) + '/' + (vm.form.STYLE_NO || null) + '/' + (vm.form.ORDER_NO || null) + '/' + (vm.form.ORDER_TYPE || null) + '/' + (vm.form.NATURE_OF_ORDER || null) + '/' + (pFIRST_DATE || null) + '/' + (vm.form.MC_STYLE_H_EXT_ID || null);

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
            MrcDataService.getDataByFullUrl('/api/mrc/AccBk/BOMListForAccBkByID?pMC_STYLE_H_ID=' + itemData.MC_STYLE_H_ID + '&pMC_STYLE_H_EXT_ID=' + itemData.MC_STYLE_H_EXT_ID).then(function (res) {

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

            var path = '/api/mrc/AccBk/OrderListForAccBk/1/20/' + (vm.form.MC_BYR_ACC_ID || null) + '/' + (vm.form.STYLE_NO || null) + '/' + (vm.form.ORDER_NO || null) + '/' + (vm.form.ORDER_TYPE || null) + '/' + (vm.form.NATURE_OF_ORDER || null) + '/' + (pFIRST_DATE || null) + '/' + (vm.form.MC_STYLE_H_EXT_ID || null);

            MrcDataService.getDataByFullUrl(path).then(function (res) {
                vm.pendingFabProdOrderList = res.data;
                //console.log(vm.pendingFabProdOrderList);

                vm.showSplash = false;
            });
        }


        vm.generatePO = function (item) {

            //var data = angular.copy(dataOri);
            //var list = _.filter(vm.pendingYarnList, function (x) { return x.KNT_YRN_LOT_ID = 0 });
            //for (var i = 0; i < vm.pendingYarnList.length; i++)
            //    for (var j = 0; j < vm.pendingYarnList[i].YarnList.length; j++)
            //        list.push(vm.pendingYarnList[i].YarnList[j]);

            var allData = _.filter(item.AccesorriesList, function (o) { return o.IS_SELECT == true });

            //var allData = (_.filter(item.AccesorriesList, function (o) { return o.IS_SELECT == true })).map(function (o) {
            //    return {
            //        INV_ITEM_ID: o.INV_ITEM_ID
            //    }
            //});
            var INV_ITEM_LST = '';
            for (var i = 0; i < allData.length; i++)
                if (i == 0)
                    INV_ITEM_LST = allData[i].INV_ITEM_ID;
                else
                    INV_ITEM_LST += "," + allData[i].INV_ITEM_ID;

            console.log(INV_ITEM_LST);
            var itemList = angular.copy(allData);
            var url = '/Merchandising/Mrc/TaBooking/#/TaBk/BgtHrd/0/PoHrd/' + item.MC_STYL_BGT_H_ID + '/Item/' + itemList[0].INV_ITEM_ID + '?pBLK_BOM_LIST=' + INV_ITEM_LST + '&pBLK_BOM_ACT=' + itemList[0].INV_ITEM_ID + '&pMC_STYLE_H_ID=' + item.MC_STYLE_H_ID + '&pMC_FAB_PROD_ORD_H_ID=' + item.MC_FAB_PROD_ORD_H_ID + '&pIS_ACC_BK_BOM=1&pSCM_PURC_REQ_H_ID=0';
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