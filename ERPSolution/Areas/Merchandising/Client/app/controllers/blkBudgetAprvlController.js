(function () {
    'use strict';
    angular.module('multitex.mrc').controller('BlkBudgetAprvlController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$timeout', '$state', '$stateParams', 'MrcDataService', BlkBudgetAprvlController]);
    function BlkBudgetAprvlController(logger, config, $q, $scope, $http, exception, $filter, $timeout, $state, $stateParams, MrcDataService) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.isShow = 'N';

        vm.today = new Date();
        var fromDt = moment().subtract(7, 'day').format("DD-MMM-YYYY");
        console.log(fromDt);

        vm.form = {
            MC_BYR_ACC_ID: '', MC_STYLE_H_ID: '', MC_STYLE_H_EXT_ID: '', BLK_FAB_REQ_NO: '', FROM_DATE: $filter('date')(fromDt, vm.dtFormat), TO_DATE: $filter('date')(vm.today, vm.dtFormat),
            SHIP_FROM_DATE: null, SHIP_TO_DATE: null
        };

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        activate();
        function activate() {
            var promise = [getOrderShipmentMonth(), getBuyerAcGrpList(), getByrAccWiseStyleExtList()];
            return $q.all(promise).then(function () {
                //if ($stateParams.pMC_BYR_ACC_ID > 0) {
                //    vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //}

                vm.showSplash = false;
            });
        };

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
        
        function getOrderShipmentMonth() {
            return MrcDataService.getDataByFullUrl('/api/mrc/Order/OrderShipmentMonth?pMC_BYR_ACC_ID').then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        }

        vm.onChangeShipMonth = function (data, e) {
            var item = e.sender.dataItem(e.sender.item);
            vm.weeks = [];

            data['SHIP_FROM_DATE'] = null;
            data['SHIP_TO_DATE'] = null;
            data['MONTHOF'] = null;

            if (item.hasOwnProperty('MONTHOF') && item.MONTHOF) {
                data['SHIP_FROM_DATE'] = item.FIRSTDATE;
                data['SHIP_TO_DATE'] = item.LASTDATE;
                data['MONTHOF'] = item.MONTHOF;
                
                var strt = moment(item.FIRSTDATE);
            }
            else {
                data['SHIP_FROM_DATE'] = null;
                data['SHIP_TO_DATE'] = null;
                data['MONTHOF'] = null;
            }
        };

        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
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
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;                   

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
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };
        
        function getBuyerAcList() {
            //vm.buyerAccWiseStyleList = new kendo.data.ObservableArray([]);

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
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
                    vm.form.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    vm.form.MC_STYLE_H_ID = null;

                    $stateParams.pMC_BYR_ACC_ID = item.MC_BYR_ACC_ID;

                    vm.styleDataSource.read();
                },
                dataBound: function (e) {
                    var item = this.dataItem(e.item);

                    if (item.MC_BYR_ACC_ID > 0) {
                        vm.form.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    }
                    else {
                        vm.form.MC_BYR_ACC_ID = 0;
                    }

                    vm.styleDataSource.read();

                    //if (MC_BYR_ACC_ID) {
                    //    return MrcDataService.GetAllOthers('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + MC_BYR_ACC_ID + '/' + null).then(function (res) {
                    //        vm.buyerAccWiseStyleList = res;

                    //        if ($stateParams.pMC_STYLE_H_ID) {
                    //            vm.form.MC_STYLE_H_ID = $stateParams.pMC_STYLE_H_ID;
                    //            vm.form.MC_STYLE_H_EXT_ID = $stateParams.MC_STYLE_H_EXT_ID;
                    //        }

                    //        vm.getBookingList();

                    //    }, function (err) {
                    //        console.log(err);
                    //    });
                    //}
                    //else {

                    //    vm.buyerAccWiseStyleList = [];
                    //}

                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }

        //vm.getBuyerAccWiseStyleList = function (e) {
        //    var MC_BYR_ACC_ID = e.sender.dataItem(e.item).MC_BYR_ACC_ID;

        //    vm.form.MC_STYLE_H_ID = null;
        //    vm.form.MC_STYLE_H_EXT_ID = null;

        //    if (parseInt(MC_BYR_ACC_ID) > 0 && MC_BYR_ACC_ID != '') {


        //        return MrcDataService.GetAllOthers('/api/mrc/StyleH/BuyerAcWiseStyleList?&pageNumber=1&pageSize=20' + '&pMC_BYR_ACC_ID=' + MC_BYR_ACC_ID).then(function (res) {
        //            vm.buyerAccWiseStyleList = res;

        //        }, function (err) {
        //            console.log(err);
        //        });
        //    }
        //    else {

        //        vm.getBuyerAccWiseStyleList = [];
        //    }
        //};


        //vm.onSelectStyleExt = function (e) {
        //    var item = e.sender.dataItem(e.item);

        //    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
        //    vm.form.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;
        //};


        function getByrAccWiseStyleList() {
            vm.styleOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    console.log(item);
                //    vm.form.KNT_SC_PRG_RCV_ID = item.KNT_SC_PRG_RCV_ID;
                //    vm.scOrderDataSource.read();
                //}                
            };

            return vm.styleDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/StyleH/BuyerAcWiseStyleList?&pageNumber=1&pageSize=20' + '&pMC_BYR_ACC_ID=' + (($stateParams.pMC_BYR_ACC_ID > 0) ? $stateParams.pMC_BYR_ACC_ID : vm.form.MC_BYR_ACC_ID);
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.getBookingList = function () {
            vm.isShow = 'Y';
            vm.fabReqDataSource.read();            
        };



        vm.printBookingRecord = function (dataItem, pREVISION_NO) {
            //console.log(dataItem);

            vm.form.REPORT_CODE = 'RPT-2001';
            vm.form.MC_BLK_FAB_REQ_H_ID = dataItem.MC_BLK_FAB_REQ_H_ID;
            vm.form.IS_MULTI_SHIP_DT = dataItem.IS_MULTI_SHIP_DT;
            vm.form.MC_BLK_REVISION_NO = pREVISION_NO;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
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

        vm.navigateAction = function (dataItem, navigateId, pREVISION_NO) {
            if (navigateId == 1) {
                $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID });
            }
            else if (navigateId == 2) {
                vm.printBookingRecord(dataItem, pREVISION_NO);
            }
            else if (navigateId == 6) {
                $state.go('BudgetSheet', { pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID, pMC_STYLE_H_ID: dataItem.MC_STYLE_H_ID, pMC_STYL_BGT_H_ID: dataItem.MC_STYL_BGT_H_ID });
            }
            else if (navigateId == 7) {
                vm.printBudgetSheetReport(dataItem);
            }
        };


        vm.fabReqDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                model: {
                    fields: {
                        BLK_FAB_REQ_DT: { type: "date" },
                        ACTION_DATE: { type: "date" },
                        SHIP_DT: { type: "date" }
                    }
                },
                data: "data",
                total: "total"
            },
            transport: {
                read: function (e) {

                    if (vm.isShow == 'Y') {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/BulkFabBk/ApprovedBlkFabBkList?&pMC_BYR_ACC_GRP_ID=' + (vm.form.MC_BYR_ACC_GRP_ID || null) + '&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || null) + '&pFROM_DATE=' + ($filter('date')(vm.form.FROM_DATE, vm.dtFormat) || null) + '&pTO_DATE=' + ($filter('date')(vm.form.TO_DATE, vm.dtFormat) || null) + '&pSHIP_FROM_DATE=' + ($filter('date')(vm.form.SHIP_FROM_DATE, vm.dtFormat) || null) + '&pSHIP_TO_DATE=' + ($filter('date')(vm.form.SHIP_TO_DATE, vm.dtFormat) || null);
                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);
                        console.log(params.filter);

                        return MrcDataService.getDataByUrl(url).then(function (res) {
                            angular.forEach(res.data, function (val, key) {
                                val['BLK_FAB_REQ_DT_STR'] = $filter('date')(val['BLK_FAB_REQ_DT'], vm.dtFormat);
                            });
                            e.success(res);
                            console.log(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                    else {
                        e.success([]);
                    }
                }
            },
            pageSize: 10,
            //scrollable: {
            //    virtual: true
            //    //scrollable:true
            //},
            //group: [{ field: 'STYLE_NO' }],
            //sort: [{ field: 'BLK_FAB_REQ_DT', dir: 'desc' }]
        });

        vm.gridOptionsFabReq = {
            height: '520',
            pageable: true,
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
            dataBound: function (e) {
                collapseAllGroups(this);
            },
            columns: [               
                //{ field: "BLK_FAB_REQ_DT_STR", title: "Booking Date", type: "date", format: "{0:dd-MMM-yyyy}", filterable: false },
                //{ field: "BLK_FAB_REQ_DT", title: "Booking Date A", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true },                                
                { field: "ACTION_DATE", title: "Aprov.Date", type: "date", format: "{0:dd-MMM-yyyy}", filterable: false, width: "8%" },
                { field: "SHIP_DT", title: "Ship.Date", type: "date", format: "{0:dd-MMM-yyyy}", filterable: false, width: "8%" },
                {
                    field: "BLK_FAB_REQ_DT",
                    title: "Prg/Rev Date",
                    template: function () {
                        return "<span>{{dataItem.BLK_FAB_REQ_DT|date: \'d MMM,yy h:mma\'}}<label class='label label-sm label-danger' ng-show='dataItem.REVISION_DT && dataItem.REVISION_NO'>Rev-{{dataItem.REVISION_NO}}</label></span>";
                    },
                    width: "14%",
                    filterable: false

                },
                //{ field: "REVISION_NO", title: "Revision#", type: "string" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", filterable: false, width: "15%" },
                { field: "STYLE_NO", title: "Style#", type: "string", filterable: false, width: "15%" },
                { field: "WORK_STYLE_NO_LST", title: "Work Style#", type: "string", filterable: false, width: "15%" },
                { field: "ORDER_NO_LST", title: "Order#", type: "string", filterable: false, width: "15%" },
                { field: "BLK_FAB_REQ_NO", title: "Booking Ref#", type: "string", hidden: true },
                { field: "REMARKS", title: "Remarks", type: "string", hidden: true },
                //{
                //    title: "Action",
                //    template: function () {                        
                //        return "<a class='btn btn-xs blue' ui-sref='BulkFabBkEntry.Dtl({pMC_BLK_FAB_REQ_H_ID:dataItem.MC_BLK_FAB_REQ_H_ID})' ><i class='fa fa-edit'> Edit</i></a>&nbsp;<a ng-click='vm.printBookingRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-print'> Print</i></a>&nbsp;<a class='btn btn-xs purple' href='/Merchandising/Mrc/BudgetSheet?a=93/#/BudgetSheet/{{dataItem.MC_BLK_FAB_REQ_H_ID}}/{{dataItem.MC_STYLE_H_ID}}' ><i class='fa fa-edit'> Budget Sheet</i></a>&nbsp;<a class='btn btn-xs purple' ng-click='vm.printBudgetSheetReport(dataItem.MC_BLK_FAB_REQ_H_ID)'><i class='fa fa-print'> Print</i></a>  ";
                //    },
                //    width: "250px"
                //},
                {
                    title: "",
                    template: function () {
                        return '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                                '<li><span style="color:black">Print</span>' +
                                    '<ul style="width:150px;">' +                                        
                                        '<li><i class="fa fa-print"> Booking Print</i>' +
                                            '<ul style="width:150px;"><li class="k-item k-state-default k-first" ng-repeat="itm in dataItem.REVISION_LIST"><a class="k-link" style="color:black" ng-click="vm.navigateAction(dataItem,2,itm.REVISION_NO)">{{itm.REV_REASON}}</a></li></ul>' +
                                        '</li>' +
                                        '<li disabled="disabled" style="padding:0px"><hr style="margin:0px;border-top: 1px solid grey;" /></li>' +                                        
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,7)" ><i class="fa fa-print"> Budget Print</i></a></li>' +
                                    '</ul>' +
                                '</li></ul>';
                    },
                    width: "6%"
                }
            ]
        };

        //#grid_active_cell > ul > li > div > ul > li.k-item.k-state-default.k-first
        //k-item k-state-default k-first
        //role="menuitem"

        //'<li><a ng-click="vm.navigateAction(dataItem,2)" ><i class="fa fa-print"> Booking Print</i></a></li>' +
        //vm.fabReqColumns = [
        //    { field: "STYLE_NO", title: "Style#", type: "string", hidden: true },            
        //    { field: "BLK_FAB_REQ_DT", title: "Booking Date", type: "date", format: "{0:dd-MMM-yyyy}" },
        //    { field: "WORK_STYLE_NO_LST", title: "Work Style#", type: "string" },
        //    { field: "ORDER_NO_LST", title: "Order#", type: "string" },
        //    { field: "BLK_FAB_REQ_NO", title: "Booking Ref#", type: "string" },                        
        //    { field: "REMARKS", title: "Remarks", type: "string" },
        //    {
        //        title: "Action",
        //        template: function () {
        //            //return "<a class='btn btn-xs blue' ui-sref='BulkFabBkEntry.Dtl({pMC_BLK_FAB_REQ_H_ID:dataItem.MC_BLK_FAB_REQ_H_ID})' ><i class='fa fa-edit'> Edit</i></a> <a class='btn btn-xs purple' ui-sref='BudgetSheet({ pMC_BLK_FAB_REQ_H_ID : dataItem.MC_BLK_FAB_REQ_H_ID, pMC_STYLE_H_ID: dataItem.MC_STYLE_H_ID})' ><i class='fa fa-edit'> Budget Sheet</i></a>            <a ng-click='vm.printBookingRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-print'> Print</i></a>";
        //            return "<a class='btn btn-xs blue' ui-sref='BulkFabBkEntry.Dtl({pMC_BLK_FAB_REQ_H_ID:dataItem.MC_BLK_FAB_REQ_H_ID})' ><i class='fa fa-edit'> Edit</i></a>&nbsp;<a ng-click='vm.printBookingRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-print'> Print</i></a>&nbsp;<a class='btn btn-xs purple' href='/Merchandising/Mrc/BudgetSheet?a=93/#/BudgetSheet/{{dataItem.MC_BLK_FAB_REQ_H_ID}}/{{dataItem.MC_STYLE_H_ID}}' ><i class='fa fa-edit'> Budget Sheet</i></a>&nbsp;<a class='btn btn-xs purple' ng-click='vm.printBudgetSheetReport(dataItem.MC_BLK_FAB_REQ_H_ID)'><i class='fa fa-print'> Print</i></a>  ";
        //        },
        //        width: "250px"
        //    }
        //];

        function collapseAllGroups(grid) {
            grid.table.find(".k-grouping-row").each(function () {
                grid.collapseGroup(this);
            });
        }

        vm.printBudgetSheetReport = function (dataOri) {
            var data = {
                MC_STYL_BGT_H_ID: dataOri.MC_STYL_BGT_H_ID,
                REVISION_NO: dataOri.BGT_REVISION_NO
            };
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-2003' }, data);

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




        vm.addNewBooking = function () {
            $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: 0, SID: vm.form.MC_STYLE_H_ID, BAID: vm.form.MC_BYR_ACC_ID, SEXID: vm.form.MC_STYLE_H_EXT_ID });
        };

    }
})();




