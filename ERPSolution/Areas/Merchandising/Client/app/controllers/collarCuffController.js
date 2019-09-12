(function () {
    'use strict';
    angular.module('multitex.mrc').controller('CollarCuffController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', '$modal', '$document', 'styleID', CollarCuffController]);
    function CollarCuffController($q, config, MrcDataService, $stateParams, $state, $scope, logger, $modal, $document, styleID) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.form = {};
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerAccListData()];
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

        

        vm.checkDataTypeL = function (dataItem) {

            var aa = angular.copy(dataItem.MEAS_LENGTH);
            var val = aa * 1;
            console.log(val.toString());
            if (val.toString() != "NaN") {
                console.log("A");
                return;
            }
            else {
                console.log("B");
                dataItem.MEAS_LENGTH = "";
                return;
            }
        }

        vm.checkDataTypeW = function (dataItem) {

            var aa = angular.copy(dataItem.MEAS_WIDTH);
            var val = aa * 1;
            console.log(val.toString());
            if (val.toString() != "NaN") {
                return;
            }
            else {
                dataItem.MEAS_WIDTH = "";
                return;
            }
        }

        vm.showStyle = function () {

            var bac = _.filter(vm.buyerAccList.data(), function (x) { return x.MC_BYR_ACC_ID == vm.form.MC_BYR_ACC_ID })
            if(bac.length>0)
                getOrderScmList(bac[0].BYR_ACC_NAME_EN, vm.form.STYLE_NO, vm.form.MC_BYR_ACC_ID);
            else
                getOrderScmList(null, vm.form.STYLE_NO, vm.form.MC_BYR_ACC_ID);
        }

        function getOrderScmList(pBYR_ACC_NAME_EN, pSTYLE_NO, pMC_BYR_ACC_ID) {
            vm.showSplash = true;
            return vm.OrderScmList = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = MrcDataService.kFilterStr2QueryParam(params.filter);

                        MrcDataService.getDataByFullUrl('/api/mrc/Order/OrderListForCollarCuff/' + params.page + '/' + params.pageSize + '?&pBYR_ACC_NAME_EN=' + (pBYR_ACC_NAME_EN || null) + '&pSTYLE_NO=' + (pSTYLE_NO || null) + '&pMC_BYR_ACC_ID=' + (pMC_BYR_ACC_ID||null)).then(function (res) {

                            var list = _.map(res['data'], function (o) {
                                o['GMTPart'] = [];
                                o['itmGmtPart'] = [];
                                o['uniqueOrderSize'] = [];
                                o['IS_FINALIZED_CC'] = null;
                                o['IS_FIRST'] = 0;
                                o['FINALIZED'] = 0;
                                return o;
                            });
                            res['data'] = list;
                            vm.showSplash = false;
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: "total"
                }
            });

        }

        vm.reset = function (data) {
            vm.form = { 'MC_STYLE_H_ID': data.MC_STYLE_H_ID };
        }

        vm.gridOptions = {
            pageable: true,
            //filterable: {
            //    extra: false,
            //    operators: {
            //        string: {
            //            contains: "Contains",
            //            startswith: "Starts With",
            //            eq: "Is Equal To"
            //        }
            //    }
            //},
            height: '500px',
            scrollable: true,
            sortable: true,
            columns: [
                { field: "MC_STYLE_H_ID", hidden: true },
                { field: "SZ_RANGE", hidden: true },
                { field: "IS_FINALIZED", hidden: true },

                 { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "20%" },
                { field: "STYLE_DESC", title: "Style Name", width: "20%" },
                { field: "STYLE_NO", title: "Style #", width: "15%" },
                //{ field: "ORDER_NO", title: "Order #", width: "15%" },
                //{ field: "SHIP_DT", title: "Shipment Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "10s%" },
                {
                    title: "Add Measurement",
                    template: '<a  title="Edit" ng-click="vm.addCollarCuffMeasurement(dataItem.MC_STYLE_H_ID, dataItem.SZ_RANGE)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>  <a  title="Info" ng-if="dataItem.IS_FINALIZED==' + "'Y'" + '" class="btn btn-xs yellow-gold"><i class="fa fa-info">  Revise</i></a>',
                    width: "15%"
                }
            ]
        };

        vm.detailExpand = function (evData) {

            if (evData) {

                var p1 = evData.MC_STYLE_H_ID;
                var p2 = (evData.SZ_RANGE || "0");
                evData.IS_FINALIZED_CC = null;

                ////vm.vmUI = ev;
                ////var GMTPart = [{ RF_GARM_PART_ID: 10, GARM_PART_NAME: 'Collar' }, { RF_GARM_PART_ID: 12, GARM_PART_NAME: 'Cuff' }];

                MrcDataService.getDataByFullUrl('/api/common/GmtPartListByStyle?pMC_STYLE_H_ID=' + p1).then(function (resgp) {

                    var GMTPart = resgp;
                    MrcDataService.getDataByFullUrl('/api/mrc/SizeMaster/GetStyleSizeList/' + p1 + '/' + p2).then(function (res) {
                        console.log(res);
                        var count = _.filter(res, function (o) { return o.MEAS_LENGTH > 0 }).length;
                        var final = _.filter(res, function (o) { return o.IS_FINALIZED == 'Y' }).length;

                        //var abcArry = res;
                        var uniqueOrderSize = _.map(_.groupBy(res, function (doc) {
                            return doc.SIZE_CODE;
                        }), function (grouped) {
                            return grouped[0];
                        });

                        var itmGmtPart = [];
                        angular.forEach(GMTPart, function (val, key) {
                            itmGmtPart[key] = _.filter(res, function (ob) {
                                return ob.RF_GARM_PART_ID == val['RF_GARM_PART_ID'];
                            });

                        });

                        evData['IS_FIRST'] = count;
                        evData['FINALIZED'] = final;
                        evData['IS_FINALIZED_CC'] = 'N';
                        evData['GMTPart'] = GMTPart;
                        evData['itmGmtPart'] = itmGmtPart;
                        evData['uniqueOrderSize'] = uniqueOrderSize;
                        console.log(evData);

                    }, function (err) {
                        console.log(err);
                    });
                }, function (err) {
                    console.log(err);
                });


            }
        }


        vm.submitSizeAll = function (data) {

            //var data = angular.copy(dataOri);
            var mylst = []
            for (var i = 0; i < data.itmGmtPart.length; i++) {
                //mylst.push(data.itmGmtPart[0][i]);
                for (var j = 0; j < data.itmGmtPart[i].length; j++) {
                    mylst.push(data.itmGmtPart[i][j]);
                }
            }

            data['XML_CollerCuff'] = MrcDataService.xmlStringShort(mylst.map(function (o) {
                return {
                    MC_STYL_CLCF_MEAS_ID: o.MC_STYL_CLCF_MEAS_ID == null ? 0 : o.MC_STYL_CLCF_MEAS_ID,
                    MC_SIZE_ID: o.MC_SIZE_ID == null ? 0 : o.MC_SIZE_ID,
                    RF_GARM_PART_ID: o.RF_GARM_PART_ID == null ? 0 : o.RF_GARM_PART_ID,
                    MEAS_LENGTH: o.MEAS_LENGTH,
                    MEAS_WIDTH: o.MEAS_WIDTH
                }
            }));

            data['IS_FINALIZED'] = data.IS_FINALIZED_CC;
            data['IS_REVIZED'] = data.FINALIZED;

            //return;
            return MrcDataService.saveDataByFullUrl(data, '/api/mrc/CollarCuff/Save').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);

                    if (data['IS_FINALIZED'] == 'Y')
                        data['FINALIZED'] = data.FINALIZED + 1;

                    if (data['IS_REVIZED'] > 0) {
                        data['IS_FIRST'] = 1;
                        data['FINALIZED'] = 0;
                        data['IS_FINALIZED_CC'] = 'N';

                    }

                    if (data['IS_FINALIZED'] != 'Y' && data['IS_REVIZED'] == 0) {
                        data['IS_FIRST'] = 1;
                        data['FINALIZED'] = 0;
                        data['IS_FINALIZED_CC'] = 'N';
                    }
                }
            });
        };
    }

})();