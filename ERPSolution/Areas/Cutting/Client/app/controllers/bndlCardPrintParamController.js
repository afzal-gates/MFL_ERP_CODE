////////// Start BndlCardPrintParamController Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('BndlCardPrintParamController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', BndlCardPrintParamController]);
    function BndlCardPrintParamController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.dtTimeFormat = config.appDateTimeFormat;

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
                
        vm.IS_SHOW_MARKER = 'N';
        var key = 'GMT_MRKR_REQ_ID';
        vm.today = kendo.toString(new Date(), "dd/MMM/yyyy hh:mm tt");

        vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p>(#: data.ORDER_NO #)</p></span>';
        vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';

        vm.form = {
            MC_BYR_ACC_GRP_ID: $stateParams.pMC_BYR_ACC_GRP_ID || 0, MC_ORDER_H_ID: $stateParams.pMC_ORDER_H_ID || 0,
            MC_STYLE_H_EXT_ID: $stateParams.pMC_STYLE_H_EXT_ID || 0, MC_ORDER_SHIP_ID: $stateParams.pMC_ORDER_SHIP_ID || null, GMT_COLOR_ID: $stateParams.pGMT_COLOR_ID || null,
            GMT_CUT_INFO_ID: 0, MC_ORDER_SIZE_ID: 0, RF_GARM_PART_ID_LIST: []
        };
           
        
        vm.addnlServiceList = [{ SERVICE_ID: 1, ADDNL_SERVICE_NAME: 'Print' }, { SERVICE_ID: 2, ADDNL_SERVICE_NAME: 'Solid' }, { SERVICE_ID: 3, ADDNL_SERVICE_NAME: 'Embroidery' }];

        
        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getOrderShipment(), getOrderColor(), getCuttingNo(), getOrderSize(), getAddnlService(), getGmtPartList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }

        vm.reqDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.reqDateOpened = true;
        };
                
        

        function getBuyerAcGrpList() {

            vm.buyerAcGrpOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    
                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;                    

                    vm.OrdStyleExtDataSource.read();
                }
            };

            vm.buyerAcGrpDataSource = new kendo.data.DataSource({                
                transport: {
                    read: function (e) {                        
                        var url = '/api/mrc/BuyerAcc/GetBuyerAccGrpList';
                       
                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
           
        }

        function getByrAccWiseStyleExtList() {
            vm.OrdStyleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;

                    //vm.getOrderColor(item.MC_FAB_PROD_ORD_H_ID);
                                   
                    vm.ordShipDataSource.read();
                    vm.ordColorDataSource.read();
                    vm.ordSizeDataSource.read();
                }
            };            

            vm.OrdStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetStyleExOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += CuttingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData);

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
            
        };

        function getOrderShipment() {

            vm.ordShipOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SHIP_DESC",
                dataValueField: "MC_ORDER_SHIP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_ORDER_SHIP_ID = item.MC_ORDER_SHIP_ID;

                    //vm.OrdStyleExtDataSource.read();
                }
            };

            vm.ordShipDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/Order/GetOrdShipList?pMC_ORDER_H_ID=' + (vm.form.MC_ORDER_H_ID || 0);

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);

                            if (res.length == 1) {
                                vm.form.MC_ORDER_SHIP_ID = res[0].MC_ORDER_SHIP_ID;
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        }
       
        function getOrderColor() {
            vm.ordColorOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.GMT_COLOR_ID = item.MC_COLOR_ID;
                    vm.cuttingNoDataSource.read();
                }
            };

            return vm.ordColorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/Order/OrderOrByerAccWiseColorList/' + vm.form.MC_ORDER_H_ID + '/null';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            
                            var colorList = _.map(_.groupBy(res, function (doc) {
                                return doc.MC_COLOR_ID;
                            }), function (grouped) {
                                return grouped[0];
                            });

                            e.success(colorList);

                            if (colorList.length == 1) {
                                vm.form.GMT_COLOR_ID = colorList[0].MC_COLOR_ID;
                                vm.cuttingNoDataSource.read();
                            }
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }
      
        function getCuttingNo() {
            vm.cuttingNoOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "CUTNG_NO",
                dataValueField: "GMT_CUT_INFO_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    //vm.form.MC_SIZE_ID = item.MC_SIZE_ID;
                }
            };

            return vm.cuttingNoDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/Cutting/LayChart/GetCuttingList?&pMC_ORDER_H_ID=' + vm.form.MC_ORDER_H_ID||0 + '&pMC_ORDER_SHIP_ID=' + (vm.form.MC_ORDER_SHIP_ID || '') + '&pGMT_COLOR_ID=' + (vm.form.GMT_COLOR_ID || '');

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {

                            e.success(res);

                            if (res.length == 1) {
                                vm.form.GMT_CUT_INFO_ID = res[0].GMT_CUT_INFO_ID;
                            }
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getOrderSize() {
            vm.ordSizeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SIZE_CODE",
                dataValueField: "MC_ORDER_SIZE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_SIZE_ID = item.MC_SIZE_ID;
                }
            };

            return vm.ordSizeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/Order/OrderWiseSizeList/' + vm.form.MC_ORDER_H_ID;

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {

                            e.success(res);

                            if (res.length == 1) {
                                vm.form.MC_ORDER_SIZE_ID = res[0].MC_ORDER_SIZE_ID;
                            }
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getAddnlService() {
            return vm.addnlServiceOptions = {
                optionLabel: "-- Select --",
                //template: '<span class="text-primary">#= EMPLOYEE_CODE #</span> :  #= EMP_FULL_NAME_EN #',
                //filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            e.success(vm.addnlServiceList);
                        }
                    }
                },
                dataTextField: "ADDNL_SERVICE_NAME",
                dataValueField: "SERVICE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    if (item.SERVICE_ID > 0) {
                        vm.form.ADDNL_SERVICE_NAME = item.ADDNL_SERVICE_NAME;
                    }
                    else {
                        vm.form.ADDNL_SERVICE_NAME = '';
                    }
                }
            };
        };

        function getGmtPartList() {

            vm.gmtPartDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        var url = '/api/common/GmtPartList';

                        console.log(url);

                        CuttingDataService.getDataByFullUrl(url).then(function (res) {                            
                            e.success(res)
                        });
                    }
                }
            });           
        };

        vm.onCloseGmtPart = function () {           

            vm.gmtPartIds = "";
            vm.form.RF_GARM_PART_LST = "";

            console.log(vm.form.RF_GARM_PART_ID_LIST);

            if (vm.form.RF_GARM_PART_ID_LIST != null) {

                if (vm.form.RF_GARM_PART_ID_LIST != []) {
                    vm.gmtPartIds = vm.form.RF_GARM_PART_ID_LIST.join(",");

                    vm.form.RF_GARM_PART_LST = vm.gmtPartIds;
                }
            }

        };

      
        vm.printBundleCard = function () {

            var url = '/api/Cutting/CutReport/PreviewReport';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.REPORT_CODE = 'RPT-4502';
            //vm.form.GMT_CUT_INFO_ID = 3;

            //vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            //vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);
            
            var params = angular.copy(vm.form);

            console.log(params);

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
////////// End CutMrkr4CadController Controller






