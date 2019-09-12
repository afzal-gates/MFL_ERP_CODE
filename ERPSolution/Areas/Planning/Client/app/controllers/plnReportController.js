(function () {
    'use strict';
    angular.module('multitex.planning').controller('PlnReportController', ['logger', 'config', 'PlanningDataService', 'plnRptData', '$q', '$state', '$scope', '$http', '$sessionStorage', '$filter', 'access_token', 'cur_user_id', PlnReportController]);
    function PlnReportController(logger, config, PlanningDataService, plnRptData, $q, $state, $scope, $http, $sessionStorage, $filter, access_token, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.showSplash = true;

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME + " (" + data.ORDER_NO + ")"#</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        vm.styleChildItemTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.ITEM_SNAME + " " + data.COMBO_NO #</h5></span>';

        vm.insert = true;
        vm.today = new Date();
        vm.rptList = angular.copy(plnRptData);

        vm.form = {
            REPORT_CODE: 'RPT-1500', IS_EXCEL_FORMAT: 'N', FROM_DATE: $filter('date')(vm.today, vm.dtFormat), TO_DATE: $filter('date')(vm.today, vm.dtFormat),
            SCM_SUPPLIER_ID: 0, FROM_MONTH: $filter('date')(vm.today, 'MMM/yyyy'), TO_MONTH: $filter('date')(vm.today, 'MMM/yyyy')
        };

        //vm.form.REPORT_CODE = 'RPT-3501';
        //vm.form.IS_EXCEL_FORMAT = 'N';

        //$scope.$watch('vm.form.REPORT_CODE', function (newVal, oldVal) {
        //    vm.form.pREPORT_CODE = vm.form.REPORT_CODE;
        //});

        //vm.form.FROM_DATE = $filter('date')(vm.today, vm.dtFormat);
        //vm.form.TO_DATE = $filter('date')(vm.today, vm.dtFormat);



        function activate() {
            var promise = [
                getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getStyleChildItem(), getOrderColorList(), getOrderShipment(), getOrderShipmentMonth(), getClndrYearList(), getClndrMonthList(), getClndrWeekList(),
                getProdLine()
            ];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);

                vm.showSplash = false;
            });
        }


        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };


        vm.fromDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.fromDateOpened = true;
        };

        vm.toDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.toDateOpened = true;
        };

        vm.FROM_MONTH_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.FROM_MONTH_OPENED = true;
        };

        vm.TO_MONTH_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.TO_MONTH_OPENED = true;
        };

        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PlanningDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_STYLE_H_ID = 0;

                    vm.styleExtDataSource.read();

                },
                //dataBound: function (e) {
                //    if ($stateParams.pMC_BYR_ACC_ID != null && $stateParams.pMC_BYR_ACC_ID > 0) {
                //        vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;

                //        vm.styleExtDataSource.read();
                //        //vm.styleFabricDataSource.read();
                //        //vm.getOrderByBuyerAC($stateParams.pMC_BYR_ACC_ID);
                //    }
                //},
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }

        function getByrAccWiseStyleExtList() {
            vm.styleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;

                    vm.ordShipDataSource.read();
                    vm.styleChildItemDataSource.read();
                    vm.orderColorListDataSource.read();

                }//,
                //dataBound: function (e) {
                //    if ($stateParams.pMC_STYLE_H_EXT_ID != null && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                //        vm.form.MC_STYLE_H_EXT_ID = $stateParams.pMC_STYLE_H_EXT_ID;

                //        vm.styleFabricDataSource.read();
                //    }
                //}
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : ''); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += PlanningDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return PlanningDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getOrderColorList() {
            vm.orderColorListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID"
            };

            return vm.orderColorListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + (vm.form.MC_STYLE_H_ID || 0)).then(function (res) {
                            e.success(res);
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

                        return PlanningDataService.getDataByFullUrl(url).then(function (res) {
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

        function getStyleChildItem() {

            vm.styleChildItemOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_SNAME",
                dataValueField: "MC_STYLE_D_ITEM_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_STYLE_D_ITEM_ID = item.MC_STYLE_D_ITEM_ID;

                    //vm.OrdStyleExtDataSource.read();
                }
            };

            vm.styleChildItemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/StyleDItem/ChildItemListByStyle/' + (vm.form.MC_STYLE_H_ID || 0);

                        return PlanningDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);                            

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        }

        function getProdLine() {
            vm.prodLineListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LINE_CODE",
                dataValueField: "HR_PROD_LINE_ID"
            };

            return vm.prodLineDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/common/GetSewingLineData').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getOrderShipmentMonth() {
            return vm.shipMonthList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/mrc/Order/OrderShipmentMonth?pMC_BYR_ACC_ID').then(function (res) {

                        });
                    }
                }
            });
        }

        function getClndrYearList() {
            return vm.clndrYearDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/CapctiClndr/GetClndrYearList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.onChangeClndrYear = function (e) {
            var item = e.sender.dataItem(e.item);
            console.log(item);
            vm.form.RF_FISCAL_YEAR_ID = item.RF_FISCAL_YEAR_ID;
            vm.form.PARENT_ID = null;
            vm.form.RF_CAL_MONTH_ID = null;
            vm.form.FY_NAME_EN = item.FY_NAME_EN;

            vm.clndrMonthDataSource.read();
            vm.clndrWeekDataSource.read();
        };

        function getClndrMonthList() {
            return vm.clndrMonthDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/CapctiClndr/GetClndrMonthList?pRF_FISCAL_YEAR_ID=' + (vm.form.RF_FISCAL_YEAR_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.onChangeClndrMonth = function (e) {
            var item = e.sender.dataItem(e.item);

            vm.form.PARENT_ID = item.GMT_PROD_PLN_CLNDR_ID;
            vm.form.RF_CAL_MONTH_ID = item.RF_CAL_MONTH_ID;

            vm.clndrWeekDataSource.read();
        };

        function getClndrWeekList() {
            return vm.clndrWeekDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/CapctiClndr/GetClndrWkList?pRF_FISCAL_YEAR_ID=' + (vm.form.RF_FISCAL_YEAR_ID || 0) + '&pRF_CAL_MONTH_ID=' + (vm.form.RF_CAL_MONTH_ID || 0) + '&pPARENT_ID=' + (vm.form.PARENT_ID || 0)).then(function (res) {

                            console.log(res);

                            angular.forEach(res, function (val, key) {
                                console.log(val);
                                val['WEEK_NAME'] = val['MN_WK_NO'] + ' [' + $filter('date')(val['WK_START_DATE'], vm.dtFormat) + ' To ' + $filter('date')(val['WK_END_DATE'], vm.dtFormat) + ']';
                            });

                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.onChangeClndrWeek = function (e) {
            var item = e.sender.dataItem(e.item);

            vm.form.FROM_DATE = item.WK_START_DATE;
            vm.form.TO_DATE = item.WK_END_DATE;
        };




        vm.rptOnChange = function (itm, idx) {
            //alert(idx);           

            var rptList = [];
            rptList = angular.copy(plnRptData);
            vm.form.REPORT_CODE = rptList[idx].RPT_CODE;
        }

        $scope.$watch('vm.form.REPORT_CODE', function (newVal, oldVal) {

            vm.isRDLC = false;
            vm.isByrAccGrp = false;
            vm.isOrder = false;
            vm.isOrderShipment = false;
            vm.isStyleChildItem = false;
            vm.isOrderColor = false;
            vm.prodLineLst = false;
            vm.isShipMonth = false;
            vm.isClndrYear = false;
            vm.isClndrMonth = false;
            vm.isClndrWeek = false;
            vm.isFormDate = false;
            vm.isToDate = false;
            vm.isFormMonth = false;
            vm.isToMonth = false;


            if (vm.form.REPORT_CODE == 'RPT-5000') { //RPT-5000" Next 3 Days Sewing Input Status
                vm.isRDLC = true;
                vm.isFormDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-5001') { //RPT-5001" Production Plan (Max 31 Days)
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.prodLineLst = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-5002') { //RPT-5002" Daily Line Plan
                vm.isRDLC = true;
                vm.isFormDate = true;
            } else if (vm.form.REPORT_CODE == 'RPT-5003') { //RPT-5002" Capacity Booking Status
                vm.isRDLC = true;
                vm.isClndrYear = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-5004') { //RPT-5004" Production Plan (Max 16 Days)
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.prodLineLst = true;
                vm.isFormDate = true;
                vm.isToDate = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-5005') { //RPT-5005" Order Wise Plan Execution
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;
                vm.isOrderShipment = true;
                vm.isStyleChildItem = true;
            }
            else if (vm.form.REPORT_CODE == 'RPT-5006') { //RPT-5006" Production Plan Summery                
                vm.isRDLC = true;                
                vm.isFormMonth = true;
                vm.isToMonth = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-5007') { //RPT-5007" Order Wise Fabric Monitoring
                vm.isRDLC = true;
                vm.isByrAccGrp = true;
                vm.isOrder = true;                
            }
            else if (vm.form.REPORT_CODE == 'RPT-5008') { //RPT-5008" Buyer wise Capacity Allocation
                vm.isRDLC = true;
                vm.isFormMonth = true;                
            }

        });


        vm.preview = function () {

            var url;
            if (vm.isRDLC == true) {
                url = '/api/Pln/PlnReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Pln/PlnReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.HR_PROD_LINE_LST = null;
            if (vm.form.HR_PROD_LINE_LIST) {
                vm.form.HR_PROD_LINE_LST = vm.form.HR_PROD_LINE_LIST.join(',');
            }

            vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);

            var params = angular.copy(vm.form);

            console.log(params);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    //input.value = params[i];
                    input.value = (params[i] instanceof Date) ? params[i].toISOString() : params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);

        };


    }



})();