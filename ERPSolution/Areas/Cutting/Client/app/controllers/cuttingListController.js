////////// Start CuttingListController Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CuttingListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', CuttingListController]);
    function CuttingListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog) {

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
            MC_STYLE_H_EXT_ID: $stateParams.pMC_STYLE_H_EXT_ID || null, MC_ORDER_SHIP_ID: $stateParams.pMC_ORDER_SHIP_ID || null, GMT_COLOR_ID: $stateParams.pGMT_COLOR_ID || null,
            GMT_CUT_INFO_ID: 0, RF_GARM_PART_LST: null, RF_GARM_PART_ID_LIST: []
        };
           
        
        vm.addnlServiceList = [{ SERVICE_ID: 1, ADDNL_SERVICE_NAME: 'Print' }, { SERVICE_ID: 2, ADDNL_SERVICE_NAME: 'Solid' }, { SERVICE_ID: 3, ADDNL_SERVICE_NAME: 'Embroidery' }];

        
        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getOrderShipment(), getOrderColor(), getAddnlService(), getGmtPartList()];
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
                    
                    vm.form.MC_STYLE_H_EXT_ID = null;
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
                    vm.form.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;
                    vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;

                    //vm.getOrderColor(item.MC_FAB_PROD_ORD_H_ID);
                                   
                    vm.ordShipDataSource.read();
                    vm.ordColorDataSource.read();                   
                }
            };            

            vm.OrdStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetStyleExOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0) + '&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || null);
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
                }
            };

            return vm.ordColorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/Order/OrderOrByerAccWiseColorList/' + (vm.form.MC_ORDER_H_ID || -1) + '/null';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            
                            var colorList = _.map(_.groupBy(res, function (doc) {
                                return doc.MC_COLOR_ID;
                            }), function (grouped) {
                                return grouped[0];
                            });

                            e.success(colorList);

                            if (colorList.length == 1) {
                                vm.form.GMT_COLOR_ID = colorList[0].MC_COLOR_ID;                                
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
                            

                            var mrkrPartList = [];

                            if (vm.form.RF_GARM_PART_LST != null) {
                                var partList = vm.form.RF_GARM_PART_LST.split(',');

                                mrkrPartList = _.filter(res, function (ob) {
                                    return _.some(partList, function (val) {
                                        return ob.RF_GARM_PART_ID == val;
                                    });
                                });
                            }

                            console.log(mrkrPartList);

                            if(mrkrPartList.length>0){
                                e.success(mrkrPartList)
                            }
                            else {
                                e.success(res)
                            }

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

        //function getOrdSizeRatio(dataItem) {
        //    CuttingDataService.getDataByFullUrl("/api/Cutting/MrkrReq/GetOrdSizeCutRatio?pMC_ORDER_H_ID=" + dataItem['MC_ORDER_H_ID'] + '&pGMT_COLOR_ID=' + dataItem['GMT_COLOR_ID']
        //        + '&pGMT_MRKR_ID=' + dataItem['GMT_MRKR_ID']).then(function (res) {

        //            vm.form['itemsOrdItm'] = res;

        //        });
        //};


        //vm.addRow = function (data) {
        //    var dataItem = angular.copy(data);
        //    vm.form.itemsCutInfoPly.push(dataItem);
        //}
                    

        vm.cuttingListGridOption = {
            height: 150,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            navigatable: true,
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
            columns: [
                { field: "TABLE_NO", title: "Table", width: "15%", filterable: true },
                { field: "MRKR_SH_DESC", title: "Marker Description", width: "30%", filterable: false },
                { field: "CUTNG_NO", title: "Cut#", width: "10%", filterable: false },
                { field: "TTL_CUT_QTY", title: "Total Cutting", width: "15%", filterable: false },
                { field: "LAY_ST_TIME", title: "Date of Cutting", type: "date", format: "{0:dd-MMM-yyyy hh:mm tt}", width: "15%", filterable: false },
                {
                    title: "",
                    template: function () {
                        return "<a class='btn btn-xs blue' title='Edit' href='/Cutting/Cutting/LayChart?a=368/#/laychtH/{{dataItem.GMT_CUT_INFO_ID}}?pMC_BYR_ACC_GRP_ID={{dataItem.MC_BYR_ACC_GRP_ID}}&pMC_STYLE_H_ID={{dataItem.MC_STYLE_H_ID}}'><i class='fa fa-edit'></i></a>&nbsp;<button type='button' class='btn btn-xs blue' ng-click='vm.saveMarkAsCuttingProd(dataItem)' ng-disabled='dataItem.IS_CUTTING_PROD==\"Y\"' >Mark as Cutting Prod</button>";
                    },
                    width: "15%"
                }
            ],
            change: function (e) {
                var grid = $("#cuttingListGrid").data("kendoGrid");
                //grid.select("tr:eq(1)");
                var row = grid.select();
                var item = grid.dataItem(row);

                console.log(item);
                vm.form.GMT_CUT_INFO_ID = item.GMT_CUT_INFO_ID;
                vm.form.RF_GARM_PART_LST = null; //item.RF_GARM_PART_LST;
                vm.form.RF_GARM_PART_ID_LIST = [];
                
                
                $state.go('CuttingList', { pGMT_CUT_INFO_ID: item.GMT_CUT_INFO_ID, pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID, 
                    pMC_ORDER_H_ID: vm.form.MC_ORDER_H_ID, pMC_ORDER_SHIP_ID: vm.form.MC_ORDER_SHIP_ID, pGMT_COLOR_ID: vm.form.GMT_COLOR_ID
                }, { notify: false });                              

                $scope.$apply();
                console.log(vm.form);

                vm.bundleCardGridDataSource.read();
                vm.gmtPartDataSource.read();
            },
            dataBound: function () {
                var grid = this;
                $.each(grid.tbody.find('tr'), function (idx) {

                    var model = grid.dataItem(this);
                    console.log(idx);

                    if ($stateParams.pGMT_CUT_INFO_ID == model.GMT_CUT_INFO_ID) {

                        vm.form.GMT_CUT_INFO_ID = model.GMT_CUT_INFO_ID;
                        vm.form.RF_GARM_PART_LST = null; //model.RF_GARM_PART_LST;
                        vm.form.RF_GARM_PART_ID_LIST = [];
                        
                        $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                        vm.bundleCardGridDataSource.read();
                        vm.gmtPartDataSource.read();
                    }
                    
                });
            }
        };


        vm.cuttingListGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                                     
                    var url = '/api/Cutting/LayChart/GetCuttingList?&pMC_ORDER_H_ID=' + vm.form.MC_ORDER_H_ID + '&pMC_ORDER_SHIP_ID=' + (vm.form.MC_ORDER_SHIP_ID || '') + '&pGMT_COLOR_ID=' + (vm.form.GMT_COLOR_ID || '');

                    console.log(url);

                    CuttingDataService.getDataByFullUrl(url).then(function (res) {
                        //res['LAY_ST_TIME'] = moment(res.LAY_ST_TIME).format("DD-MMM-YYYY hh:mm a")
                        e.success(res);

                        //var grid = $("#cuttingListGrid").data("kendoGrid"); //this;
                        //$.each(grid.tbody.find('tr'), function (idx) {

                        //    var model = grid.dataItem(this);
                        //    if (idx == 0) {

                        //        vm.form.GMT_CUT_INFO_ID = model.GMT_CUT_INFO_ID;

                        //        $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                        //        vm.bundleCardGridDataSource.read();
                        //    }

                        //});
                    });
                }
            },           
            schema: {               
                model: {
                    id: "GMT_CUT_INFO_ID",
                    fields: {                        
                        LAY_ST_TIME: { type: "date", filterable: false }
                    }
                }
            }
        });


        vm.getCuttingList = function () {
            $state.go('CuttingList', { pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID, pMC_ORDER_H_ID: vm.form.MC_ORDER_H_ID, pMC_ORDER_SHIP_ID: vm.form.MC_ORDER_SHIP_ID, pGMT_COLOR_ID: vm.form.GMT_COLOR_ID }, { notify: false });

            vm.cuttingListGridDataSource.read();
        }



        vm.bundleCardGridOption = {
            height: 150,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "cell",
            navigatable: true,
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
            columns: [
                { field: "BUNDLE_CARD_LIST_SL", title: "S/L", width: "20%", filterable: true },
                { field: "SIZE_CODE", title: "Size", width: "20%", filterable: false },
                { field: "NO_OF_BUNDLE", title: "# of Bundle", width: "20%", filterable: false },
                { field: "QTY_IN_BNDL", title: "Qty", width: "20%", filterable: false }//,                                
            ]
        };


        vm.bundleCardGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    
                    var url = '/api/Cutting/LayChart/GetBundleCardList?pGMT_CUT_INFO_ID=' + vm.form.GMT_CUT_INFO_ID;

                    console.log(url);

                    CuttingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });


        vm.saveMarkAsCuttingProd = function (dataItem) {
            var submitData = angular.copy(dataItem);

            console.log(submitData);
            //return;
            
            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + $scope.token },
                         url: '/api/Cutting/LayChart/SaveMarkAsCuttingProd',
                         method: 'post',
                         data: submitData
                     }).then(function (res) {
                         //$scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             //$scope.$parent.errors = [];
                             //$scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                 dataItem.IS_CUTTING_PROD = 'Y';
                                 
                                 //$state.go('LayChartH', { pGMT_CUT_INFO_ID: res.data.PGMT_CUT_INFO_ID_RTN }, { notify: false });
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });

        };
              
        vm.bundleCardPrintType = [
            { BNDL_PRINT_TYP_ID: 1, BNDL_PRINT_TYP_NAME: 'Bundle Chart', IS_GARM_PART_RQD: false, IS_SERVICE_RQD: false },
            { BNDL_PRINT_TYP_ID: 2, BNDL_PRINT_TYP_NAME: 'Bundle Normal', IS_GARM_PART_RQD: true, IS_SERVICE_RQD: true },
            { BNDL_PRINT_TYP_ID: 3, BNDL_PRINT_TYP_NAME: 'Bundle for Piping', IS_GARM_PART_RQD: false, IS_SERVICE_RQD: false },
            { BNDL_PRINT_TYP_ID: 4, BNDL_PRINT_TYP_NAME: 'Bundle for Recut', IS_GARM_PART_RQD: false, IS_SERVICE_RQD: false },
            { BNDL_PRINT_TYP_ID: 5, BNDL_PRINT_TYP_NAME: 'Bundle for Full Cake Size Wise', IS_GARM_PART_RQD: true, IS_SERVICE_RQD: false },
            { BNDL_PRINT_TYP_ID: 6, BNDL_PRINT_TYP_NAME: 'Bundle for Full Cake Ratio Wise', IS_GARM_PART_RQD: true, IS_SERVICE_RQD: false }
        ];

        vm.printBundleCard = function () {

            var url = '/api/Cutting/CutReport/PreviewReport';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.REPORT_CODE = 'RPT-4500';
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

        vm.printBundleChart = function () {

            var url = '/api/Cutting/CutReport/PreviewReport';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.REPORT_CODE = 'RPT-4501';
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

        vm.print4PipingBundleCard = function (gmtPartName) {

            var url = '/api/Cutting/CutReport/PreviewReport';
            //var url = '/api/Cutting/CutReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.REPORT_CODE = 'RPT-4503';
            vm.form.RF_GARM_PART_NAME_LST = gmtPartName;
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

        vm.printBndlCard4FullCake = function (bndlPrintTypeId) {

            var url = '/api/Cutting/CutReport/PreviewReport';
            //var url = '/api/Cutting/CutReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.REPORT_CODE = 'RPT-4504';
            vm.form.BNDL_PRINT_TYP_ID = bndlPrintTypeId;
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

        vm.printBndlChart4FullCake = function (bndlPrintTypeId) {

            var url = '/api/Cutting/CutReport/PreviewReport';
            //var url = '/api/Cutting/CutReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.REPORT_CODE = 'RPT-4505';
            vm.form.BNDL_PRINT_TYP_ID = bndlPrintTypeId;
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
        
        vm.printBundleChart4CutPanelInsp = function () {

            var url = '/api/Cutting/CutReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.REPORT_CODE = 'RPT-4506';
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






