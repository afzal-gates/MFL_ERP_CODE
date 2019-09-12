(function () {
    'use strict';
    angular.module('multitex.ie').controller('GmtWashReqController', ['$q', 'config', 'IeDataService', '$stateParams', '$state', '$scope', 'formData', '$filter', 'cur_user_id', GmtWashReqController]);
    function GmtWashReqController($q, config, IeDataService, $stateParams, $state, $scope, formData, $filter, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        var V_MC_BYR_ACC_ID;

        vm.template = '<span><b><h5 style="padding-top:4px;margin:0px;">#: kendo.toString(new Date(data.SHIP_DT), "dd-MM-yyyy") #</h5></b></span>';
        vm.valueTemplate = '<span><b><h5 style="padding-top:4px;margin:0px;">#: kendo.toString(new Date(data.SHIP_DT), "dd-MM-yyyy") #</h5></b></span>';
        vm.templateOrder = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO #</h5><p> (#: data.FAB_PROD_CAT_SNAME #)</p></span>';
        vm.valueTemplateOrder = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';
        vm.templateStyle = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.FAB_PROD_CAT_SNAME #)</p></span>';
        vm.valueTemplateStyle = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.ORDER_NO #</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';


        vm.form = formData.GMT_DF_WASH_REQ_ID ? formData : { WASH_REQ_DT: vm.today, TGT_ST_DT: '', TGT_END_DT: '', WASH_REQ_BY: cur_user_id, MC_ORDER_SHIP_ID: '', RF_REQ_TYPE_ID: 43 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getStyleExtList(formData.MC_BYR_ACC_GRP_ID), getOrderInfoList(), getShipmentInfoList(), getUserData(), getStatusList(), getDfProcessTypeData(), GetFloorList(),
                GetLineList(), GetReqTypeList(), GetProdCatList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.form = formData.GMT_DF_WASH_REQ_ID ? formData : { WASH_REQ_DT: vm.today, TGT_ST_DT: '', TGT_END_DT: '', WASH_REQ_BY: cur_user_id, MC_ORDER_SHIP_ID: '', RF_REQ_TYPE_ID: 43 };


        function getStatusList() {

            var RF_ACTN_TYPE_ID = 39;
            var PARENT_ID = 0;
            if (vm.form.RF_ACTN_STATUS_ID > 0) {
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
            }

            vm.actionOptions = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        vm.form.RF_ACTN_STATUS_ID = ds[0].RF_ACTN_STATUS_ID;
                    }
                    else {
                        vm.form.RF_ACTN_STATUS_ID = '';
                    }
                }
            };

            return vm.actionDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id;

                        return IeDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };


        function GetProdCatList() {

            return vm.prodCatList = {
                optionLabel: "-- GMT Production Category --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            IeDataService.LookupListData(148).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            IeDataService.getDataByFullUrl('/api/common/GetReqTypeByUser').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };


        function GetFloorList() {

            return vm.floorList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        IeDataService.getDataByFullUrl('/api/hr/GetFloorList?pHR_COMPANY_ID=1').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        function GetLineList() {

            return vm.lineList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        IeDataService.getDataByFullUrl('/api/hr/GetPrdLineList?pHR_PROD_FLR_ID=' + (vm.form.HR_PROD_FLR_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        vm.getLine = function () {
            vm.lineList.read();
        }


        function getDfProcessTypeData() {
            return vm.processTypeList = {
                optionLabel: "-- Select DF Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            IeDataService.getDataByFullUrl('/api/dye/DFProcessType/SelectAll').then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_PROC_SUB_GRP_ID == 575 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "DF_PROC_TYPE_NAME",
                dataValueField: "DF_PROC_TYPE_ID"
            };

        };

        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            IeDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.IsChange = false;
                },

                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

            //$("#customers").kendoDropDownList(vm.userList);
        }


        $scope.WASH_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.WASH_REQ_DT_LNopened = true;
        }

        $scope.TGT_ST_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TGT_ST_DT_LNopened = true;
        }

        $scope.TGT_END_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TGT_END_DT_LNopened = true;
        }


        vm.buyerAcGrpList = {
            optionLabel: "--- Buyer A/C Group ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return IeDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            select: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_BYR_ACC_GRP_ID.length != 0) {

                    vm.OrderDs.read();
                    //getStyleExtList(item.MC_BYR_ACC_GRP_ID, null);
                    //vm.getOrderList();
                    //getOrderList(item.MC_BYR_ACC_GRP_ID, null);
                }
            },
            dataTextField: "BYR_ACC_GRP_NAME_EN",
            dataValueField: "MC_BYR_ACC_GRP_ID"
        };

        function getStyleExtList(pMC_BYR_ACC_GRP_ID) {

            return vm.StyleExtDs = new kendo.data.DataSource({
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
                        return IeDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            });
        };

        function getOrderInfoList() {
            return vm.OrderDs = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/common/getOrderStyleDropDownDataGmt?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : '') + (vm.form.ORDER_NO != null ? '&pORDER_NO=' + vm.form.ORDER_NO : '') + '&pGMT_DF_WASH_REQ_ID=' + (vm.form.GMT_DF_WASH_REQ_ID || 0);
                        url += IeDataService.kFilterStr2QueryParam(params.filter);
                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(vm.form.MC_BYR_ACC_GRP_ID);
                        console.log(vm.form.ORDER_NO);

                        return IeDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });

                        //var url = "/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=" + (vm.form.MC_BYR_ACC_GRP_ID || 0);
                        //url += "&pMC_STYLE_H_EXT_ID=" + (vm.form.MC_STYLE_H_EXT_ID || 0);
                        //return IeDataService.getDataByFullUrl(url).then(function (res) {
                        //    e.success(res);
                        //});
                    }
                },
                serverFiltering: true
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
                        var url = "/api/mrc/Order/GetOrdShipList?pMC_ORDER_H_ID=" + (vm.form.MC_ORDER_H_ID || 0);
                        return IeDataService.getDataByFullUrl(url).then(function (res) {
                            //console.log(res);
                            var list = res.map(function (o) {
                                return {
                                    SHIP_DT: $filter('date')(o.SHIP_DT, 'dd-MM-yyyy'),
                                    MC_ORDER_SHIP_ID: o.MC_ORDER_SHIP_ID,
                                }
                            })
                            if (list.length == 1)
                                vm.form.MC_ORDER_SHIP_ID = list[0].MC_ORDER_SHIP_ID;
                            e.success(list);
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


        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var _reqDate = $filter('date')(data.WASH_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["WASH_REQ_DT"] = _reqDate;

                var _frmDate = $filter('date')(data.TGT_ST_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["TGT_ST_DT"] = _frmDate;

                var _toDate = $filter('date')(data.TGT_END_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["TGT_END_DT"] = _toDate;

                //data["LK_GMT_PROD_CAT_ID"] = 1;
                //data["RF_REQ_TYPE_ID"] = 1;

                var id = vm.form.GMT_DF_WASH_REQ_ID

                return IeDataService.saveDataByUrl(data, '/GmtWashReq/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        config.appToastMsg(res.data.PMSG);
                        $state.go($state.current, { pGMT_DF_WASH_REQ_ID: res.data.OPGMT_DF_WASH_REQ_ID }, { inherit: false, reload: true });

                    }
                });
            }
        };

    }

})();
