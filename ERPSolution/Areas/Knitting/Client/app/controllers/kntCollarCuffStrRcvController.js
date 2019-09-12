////////// Start Collar Cuff Header Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntCollarCuffStrRcvHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'formData', 'Dialog', 'access_token', KntCollarCuffStrRcvHController]);
    function KntCollarCuffStrRcvHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, formData, Dialog, access_token) {

        var vm = this;
        vm.showSplash = true;
        $scope.errors = null;
        vm.Title = $state.current.Title || '';
        $scope.token=config.acc
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        var key = 'KNT_SCO_CHL_RCV_H_ID';

        console.log($state.current.data);

        vm.form = formData[key] ? formData : {
            KNT_SCO_CHL_RCV_H_ID: 0, LK_CLCF_CHL_TYP_ID: 559, CLCF_SRC_PROD_CAT_ID: $state.current.data.CLCF_SRC_PROD_CAT_ID, IS_SUB_CONTR: 'N', IS_FINALIZED: 'N', RCV_DT: moment(vm.today).format("DD-MMM-YYYY"), CHALAN_DT: moment(vm.today).format("DD-MMM-YYYY"),
            USER_LEVEL: "", LK_CLCF_MVM_STS_ID: 570, SUP_TRD_NAME_EN: ""
        };// { PROD_DT: moment(vm.today).format("DD-MMM-YYYY") };
        vm.formItem = { MC_BYR_ACC_GRP_ID: 0, MC_STYLE_H_EXT_ID: 0, MC_COLOR_ID: 0 };

        vm.orderColorSearchGridDataSource = new kendo.data.DataSource({
            serverFiltering: true,
            transport: {
                read: function (e) {

                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = '/api/knit/KntCollarCuff/GetOrdCol4StrRcv?pLK_CLCF_CHL_TYP_ID=559&pRF_FAB_PROD_CAT_ID_LST=' + $state.current.data.RF_FAB_PROD_CAT_ID_LST;
                    
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                    console.log(url);

                    var paramsData = params.filter.replace(/'/g, '').split('~');
                    console.log(paramsData);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                        console.log(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });


        vm.orderColorSearchGridOption = {
            height: "150px",
            scrollable: true,
            //scrollable: {
            //    virtual: true               
            //},
            sortable: true,
            selectable: "row",
            //pageable: true,
            //groupable: true,
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
                //{ field: "SCO_PRG_NO", title: "S/C Prog#", width: "10%" },
                { field: "FAB_PROD_CAT_SNAME", title: "Type", width: "8%", filterable: false },
                { field: "BYR_ACC_GRP_SNAME", title: "Buyer A/C", width: "15%", filterable: false },
                { field: "WORK_STYLE_NO", title: "Job#", width: "25%" },
                { field: "MC_ORDER_NO_LST", title: "Order#", width: "25%" },                
                { field: "COLOR_NAME_EN", title: "Color", width: "20%" },
                {
                    title: "Action",
                    template: function () {
                        return "<a type='button' class='btn btn-xs blue' ng-click='vm.selectOrderColor(dataItem)' >Select</a>"
                    },
                    width: "7%"
                }
            ]
        }


        vm.selectOrderColor = function (dataItem) {
            vm.formItem.MC_FAB_PROD_ORD_H_ID = dataItem.MC_FAB_PROD_ORD_H_ID;
            vm.formItem.MC_STYLE_H_EXT_ID = dataItem.MC_STYLE_H_EXT_ID;
            vm.formItem.MC_COLOR_ID = dataItem.MC_COLOR_ID;
            vm.formItem.KNT_SCO_CLCF_PRG_H_ID = dataItem.KNT_SCO_CLCF_PRG_H_ID;

            var grid = $("#orderColorSearchGrid").data("kendoGrid");
            
            console.log(grid);

            grid.items().each(function () {
                var data = grid.dataItem(this);
                
                //some condition
                if (data.uid == dataItem.uid) {
                    grid.select(this);
                    $('[data-uid=' + data.uid + ']').addClass('k-state-selected');
                }
                else {
                    $('[data-uid=' + data.uid + ']').removeClass('k-state-selected');
                }
            });

            
            vm.clcfRcvGridDataSource.read();
            
        }

        /*
        vm.orderDataSource = new kendo.data.DataSource({
            serverFiltering: true,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KntCollarCuff/GetCollarCuffOrdReq?pMC_BYR_ACC_GRP_ID=' + (vm.formItem.MC_BYR_ACC_GRP_ID || $stateParams.pMC_BYR_ACC_GRP_ID || null);
                    url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    var paramsData = params.filter.replace(/'/g, '').split('~');
                    console.log(paramsData[2]);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res.data);
                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.orderColorOption = {
            optionLabel: "-- Select --",
            filter: "contains",
            autoBind: true,
            dataTextField: "COLOR_NAME_EN",
            dataValueField: "MC_COLOR_ID",
            select: function (e) {
                var item = this.dataItem(e.item);
                vm.form.COLOR_NAME_EN = item.COLOR_NAME_EN;
                console.log(item);
            },
            dataBound: function (e) {
                var item = this.dataItem(e.item);
                if ($stateParams.pMC_COLOR_ID != null && $stateParams.pMC_COLOR_ID > 0) {
                    vm.form.MC_COLOR_ID = $stateParams.pMC_COLOR_ID;
                    vm.form.COLOR_NAME_EN = item.COLOR_NAME_EN;
                }
            }
        };
        */




        vm.scoClcfProgTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.SCO_PRG_NO #</h5><p> (#: data.SUP_TRD_NAME_EN||""#)</p></span>';
        vm.scoClcfProgValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.SCO_PRG_NO # (#: data.SUP_TRD_NAME_EN||"" #)</h5></span>';

        vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME # (#: data.STYLE_NO #)</p></span>';
        vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
        
        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getChallanType(), /*getScoClcfProg(),*/ getSupplierList(), getStoreList(), getBuyerAcGrpList()/*, getOrder(), getOrderColor()*/];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.rcvDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.rcvDateOpened = true;
        };
        vm.chlnDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.chlnDateOpened = true;
        };


        function getChallanType() {
            vm.chlnTypeOptions = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1 && !formData[key]) {
                        e.sender.value(ds[0].LOOKUP_DATA_ID);
                        vm.form.LK_CLCF_CHL_TYP_ID = ds[0].LOOKUP_DATA_ID;                        
                    }                   
                }
            };

            return vm.chlnTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        if (vm.form.LK_CLCF_MVM_STS_ID == 570) {
                            return KnittingDataService.getDataByFullUrl('/api/knit/KntCollarCuff/GetClcfChlnType').then(function (res) {
                                e.success(res);
                            });
                        }
                        else {
                            return KnittingDataService.getDataByUrlNoToken('/api/common/LookupListData/110').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                }
            });
        };

        function getScoClcfProg() {
            vm.scoClcfProgOptions = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SCO_PRG_NO",
                dataValueField: "KNT_SCO_CLCF_PRG_H_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.SUP_TRD_NAME_EN = item.SUP_TRD_NAME_EN;
                }
            };

            return vm.scoClcfProgDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        if (formData['KNT_SCO_CLCF_PRG_H_ID'] > 0) {
                            var url = '/api/knit/ScoCollarCuff/GetScoCollarCuffProgList?pKNT_SCO_CLCF_PRG_H_ID=' + formData['KNT_SCO_CLCF_PRG_H_ID'] + '&pageNumber=1&pageSize=10';
                        }
                        else {
                            var url = '/api/knit/ScoCollarCuff/GetScoCollarCuffProgList?&pageNumber=1&pageSize=10';
                        }
                        
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        console.log(url);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {                            
                            e.success(res.data);
                            console.log(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    //vm.form.KNT_SC_PRG_RCV_ID = null;

                    vm.orderColorSearchGridDataSource.read();
                }
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getOrderColor() {
            return vm.orderColorOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                dataBound: function (e) {
                    var item = this.dataItem(e.item);
                    if ($stateParams.pMC_COLOR_ID != null && $stateParams.pMC_COLOR_ID > 0) {
                        vm.formItem.MC_COLOR_ID = $stateParams.pMC_COLOR_ID;
                    }
                }
            };
        }

        function getOrder() {
            return vm.orderOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ORDER_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    if (item.MC_ORDER_H_ID_LST) {
                        vm.formItem.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;
                        vm.formItem.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID_LST;
                        $stateParams.pMC_ORDER_H_ID_LST = item.MC_ORDER_H_ID_LST;
                        $stateParams.pMC_COLOR_ID = 0;
                        vm.getOrderColor(item.MC_ORDER_H_ID_LST);
                    }
                    else {
                        vm.formItem.MC_ORDER_H_ID_LST = 0;
                        vm.getOrderColor('0');
                    }
                },
                dataBound: function (e) {
                    if ($stateParams.pMC_STYLE_H_EXT_ID != null && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                        vm.formItem.MC_STYLE_H_EXT_ID = $stateParams.pMC_STYLE_H_EXT_ID;

                        if ($stateParams.pMC_ORDER_H_ID_LST != null) {
                            vm.getOrderColor($stateParams.pMC_ORDER_H_ID_LST);
                        }
                    }
                }
            };
        }

        function getStoreList() {
            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5').then(function (res) {
                                //KnittingDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    //$scope.dataItem.STORE_NAME_EN = item.STORE_NAME_EN;
                },
                dataBound: function (e) {
                    if ($stateParams.pSCM_STORE_ID != null && $stateParams.pSCM_STORE_ID > 0) {
                        vm.form.SCM_STORE_ID = $stateParams.pSCM_STORE_ID;                        
                    }
                }
            };
            
        };

        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
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

                    vm.getOrderByBuyerAC(item.MC_BYR_ACC_GRP_ID);
                },
                dataBound: function (e) {
                    if ($stateParams.pMC_BYR_ACC_GRP_ID != null && $stateParams.pMC_BYR_ACC_GRP_ID > 0) {
                        vm.formItem.MC_BYR_ACC_GRP_ID = $stateParams.pMC_BYR_ACC_GRP_ID;
                        
                        vm.getOrderByBuyerAC($stateParams.pMC_BYR_ACC_GRP_ID);
                    }
                },
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }

        /*
        function getBuyerAccListData() {
            return vm.buyerAccList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);

                    vm.getOrderByBuyerAC(item.MC_BYR_ACC_ID);
                },
                dataBound: function (e) {
                    if ($stateParams.pMC_BYR_ACC_ID != null && $stateParams.pMC_BYR_ACC_ID > 0) {
                        vm.formItem.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;

                        vm.getOrderByBuyerAC($stateParams.pMC_BYR_ACC_ID);
                    }
                }
            };
        };
        */

        vm.getOrderByBuyerAC = function (pMC_BYR_ACC_GRP_ID) {
           
            return vm.orderDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntCollarCuff/GetCollarCuffOrdReq?pMC_BYR_ACC_GRP_ID=' + pMC_BYR_ACC_GRP_ID;
                        
                        url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });            
        };
     
        vm.getOrderColor = function (pMC_ORDER_H_ID_LST) {
            return vm.orderColorDataSource = new kendo.data.DataSource({                
                transport: {
                    read: function (e) {                        
                        var url = '/api/mrc/Order/MultiOrderWiseColorList/' + pMC_ORDER_H_ID_LST;
                        
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        
        $scope.$watchGroup(['vm.formItem.MC_BYR_ACC_ID', 'vm.formItem.MC_STYLE_H_EXT_ID', 'vm.formItem.MC_COLOR_ID'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.IS_NEXT = 'N';                    
                }
            }
        }, true);
               

        

        vm.nextLoad = function () {
            vm.IS_NEXT = 'Y';
            //var spDate = moment(vm.form.PROD_DT).format("DD-MMM-YYYY");    
            //$state.go('KntCollarCuffStrRcvH', {
            //    pRCV_DT: $filter('date')(vm.form.RCV_DT, vm.dtFormat), pSCM_STORE_ID: vm.form.SCM_STORE_ID, pMC_FAB_PROD_ORD_H_ID: vm.form.MC_FAB_PROD_ORD_H_ID,
            //    pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID, pMC_COLOR_ID: vm.form.MC_COLOR_ID
            //}, { notify: false });

            vm.clcfRcvGridDataSource.read();
        };

        vm.onChangeRcvQty = function (dataItem, ctrl) {
            
            var balQty = parseInt(dataItem.PRD_QTY) - parseInt(dataItem.DELV_QTY);
            console.log(dataItem);

            if(balQty<0){
                ctrl.$setValidity("max", false);
            }
            else{
                ctrl.$setValidity("max", true);
            }
        };

        vm.clcfRcvGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    
                    var url = '/api/knit/KntCollarCuff/GetCollarCuff4StrRcv?pLK_CLCF_CHL_TYP_ID=559&pRCV_DT=' + $filter('date')(vm.form.RCV_DT, vm.dtFormat) + '&pSCM_STORE_ID=' + vm.form.SCM_STORE_ID + '&pMC_FAB_PROD_ORD_H_ID=' + vm.formItem.MC_FAB_PROD_ORD_H_ID + '&pMC_STYLE_H_EXT_ID=' + vm.formItem.MC_STYLE_H_EXT_ID + '&pMC_COLOR_ID=' + vm.formItem.MC_COLOR_ID;

                    console.log(url);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                        console.log(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.clcfRcvGridOption = {
            height: "300px",
            scrollable: true,
            //scrollable: {
            //    virtual: true               
            //},
            sortable: true,
            //pageable: true,
            //groupable: true,
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
                //{ field: "BYR_ACC_NAME_EN", title: "Buyer A/c", width: "12%" },                
                { field: "MC_ORDER_NO_LST", title: "Order#", width: "12%" },
                //{ field: "STYLE_NO", title: "Style#", width: "10%" },
                { field: "COLOR_NAME_EN", title: "Color", width: "12%" },
                { field: "GARM_PART_NAME", title: "Part", width: "7%" },
                //{ field: "SIZE_CODE", title: "Size", width: "7%" },
                { field: "KNT_YRN_LOT_LST", title: "Yarn", width: "13%", filterable: false },
                { field: "MESUREMENT", title: "Meas", width: "7%", filterable: false },
                { field: "PRD_QTY", title: "Prod.Qty", width: "6%", filterable: false },                
                {
                    title: "Delv.Qty",
                    field: "DELV_QTY",
                    //footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmRcvQty'><input type='number' class='form-control' name='DELV_QTY' ng-model='dataItem.DELV_QTY' min='0' ng-style='(frmRcvQty.DELV_QTY.$error.min)? vm.errstyle:\"\"'  /></ng-form>";
                    },
                    width: "8%",
                    filterable: false
                },                
                {
                    title: "No of Roll",
                    field: "DELV_NO_ROLL",
                    template: function () {
                        return "<ng-form name='frmNoRoll'><input type='number' class='form-control' name='DELV_NO_ROLL' ng-model='dataItem.DELV_NO_ROLL' ng-min='dataItem.DELV_QTY>0?1:0' ng-required='dataItem.DELV_QTY>0' ng-style='(frmNoRoll.DELV_NO_ROLL.$error.min||frmNoRoll.DELV_NO_ROLL.$error.required)? vm.errstyle:\"\"'  /></ng-form>";
                    },
                    width: "7%",
                    filterable: false
                },
                {
                    title: "Weight in Kg",
                    field: "DELV_ROLL_WT",
                    template: function () {
                        return "<ng-form name='frmRollWt'><input type='number' class='form-control' name='DELV_ROLL_WT' ng-model='dataItem.DELV_ROLL_WT' ng-min='0' ng-style='(frmRollWt.DELV_ROLL_WT.$error.min)? vm.errstyle:\"\"'  /></ng-form>";
                    },
                    width: "8%",
                    filterable: false
                },
                {
                    title: "Pass.Qty",
                    field: "PASS_QTY",
                    //footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmPassQty'><input type='number' class='form-control' name='PASS_QTY' ng-model='dataItem.PASS_QTY' min='0' ng-style='frmPassQty.PASS_QTY.$error.min||frmPassQty.PASS_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                    },
                    width: "8%",
                    filterable: false
                },
                {
                    title: "Rej.Qty",
                    field: "REJ_QTY",
                    //footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmRejQty'><input type='number' class='form-control' name='REJ_QTY' ng-model='dataItem.REJ_QTY' min='0' ng-style='frmRejQty.REJ_QTY.$error.min||frmRejQty.REJ_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                    },
                    width: "8%",
                    filterable: false
                },
                {
                    title: "Hold.Qty",
                    field: "HOLD_QTY",
                    //footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmHoldQty'><input type='number' class='form-control' name='HOLD_QTY' ng-model='dataItem.HOLD_QTY' min='0' ng-style='frmHoldQty.HOLD_QTY.$error.min||frmHoldQty.HOLD_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                    },
                    width: "6%",
                    filterable: false
                }//,
                //{
                //    title: "Rcv.Qty",
                //    field: "RCV_QTY",
                //    //footerTemplate: "#=sum#",
                //    template: function () {
                //        return "<ng-form name='frmRcvQty'><input type='number' class='form-control' name='RCV_QTY' ng-model='dataItem.RCV_QTY' min='0' ng-style='frmRcvQty.RCV_QTY.$error.min||frmRcvQty.RCV_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='true' /></ng-form>";
                //    },
                //    width: "6%",
                //    filterable: false
                //}
                //{
                //    title: "Remarks",
                //    field: "REMARKS",
                //    template: function () {
                //        return "<ng-form name='frmRemarks'><input type='text' class='form-control' name='REMARKS' ng-model='dataItem.REMARKS' ng-disabled='dataItem.IS_FINALIZE==\"Y\"' /></ng-form>";
                //    },
                //    width: "12%",
                //    filterable: false
                //}
            ]
        };


        vm.addGrid4Rcv = function () {
            var dataList = vm.clcfRcvGridDataSource.data();

            angular.forEach(dataList, function (ob) {
                if (ob.DELV_QTY > 0) {
                    vm.clcfChlnRcvGridDataSource.insert(ob, 0);
                }
            });

            vm.formItem.MC_COLOR_ID = 0;
            //vm.orderColorDataSource.read();
            vm.clcfRcvGridDataSource.read();           
        }

        vm.clcfChlnRcvGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    var url = '/api/knit/KntCollarCuff/GetChlnDtl4ClcfStrRcv?pKNT_SCO_CHL_RCV_H_ID=' + ($stateParams.pKNT_SCO_CHL_RCV_H_ID || vm.form.KNT_SCO_CHL_RCV_H_ID || 0);
                    console.log(url);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                        console.log(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            aggregate: [
                { field: "MC_ORDER_NO_LST", aggregate: "count" },
                { field: "DELV_QTY", aggregate: "sum" },                              
                { field: "DELV_NO_ROLL", aggregate: "sum" },
                { field: "DELV_ROLL_WT", aggregate: "sum" },
                { field: "PASS_QTY", aggregate: "sum" },
                { field: "REJ_QTY", aggregate: "sum" },
                { field: "HOLD_QTY", aggregate: "sum" }
            ],
        });

        vm.clcfChlnRcvGridOption = {
            height: "300px",
            scrollable: true,
            //scrollable: {
            //    virtual: true               
            //},
            sortable: true,
            //pageable: true,
            //groupable: true,
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
                //{ field: "BYR_ACC_NAME_EN", title: "Buyer A/c", width: "12%" },
                { field: "MC_ORDER_NO_LST", title: "Order#", width: "12%", footerTemplate: "Total Record: #=count#" },
                //{ field: "STYLE_NO", title: "Style#", width: "10%" },
                { field: "COLOR_NAME_EN", title: "Color", width: "12%" },
                { field: "GARM_PART_NAME", title: "Part", width: "7%" },
                //{ field: "SIZE_CODE", title: "Size", width: "7%" },
                { field: "KNT_YRN_LOT_LST", title: "Yarn", width: "13%", filterable: false },
                { field: "MESUREMENT", title: "Meas", width: "7%", filterable: false },
                { field: "PRD_QTY", title: "Prod. Qty", width: "6%", filterable: false },
                {
                    title: "Delv.Qty",
                    field: "DELV_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmChlnRcvQty'><input type='number' class='form-control' name='DELV_QTY' ng-model='dataItem.DELV_QTY' min='0' ng-change='vm.onChangeRcvQty(dataItem, frmChlnRcvQty.DELV_QTY)' ng-style='frmChlnRcvQty.DELV_QTY.$error.min||frmChlnRcvQty.DELV_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='dataItem.LK_CLCF_MVM_STS_ID>570' /></ng-form>";
                    },
                    width: "8%",
                    filterable: false
                },                
                {
                    title: "No of Roll",
                    field: "DELV_NO_ROLL",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmChlnNoRoll'><input type='number' class='form-control' name='DELV_NO_ROLL' ng-model='dataItem.DELV_NO_ROLL' ng-min='dataItem.DELV_QTY>0?1:0' ng-required='dataItem.DELV_QTY>0' ng-style='frmChlnNoRoll.DELV_NO_ROLL.$error.min||frmChlnNoRoll.DELV_NO_ROLL.$error.required? vm.errstyle:\"\"' ng-disabled='dataItem.LK_CLCF_MVM_STS_ID>570' /></ng-form>";
                    },
                    width: "7%",
                    filterable: false
                },
                {
                    title: "Weight in Kg",
                    field: "DELV_ROLL_WT",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmChlnRollWt'><input type='number' class='form-control' name='DELV_ROLL_WT' ng-model='dataItem.DELV_ROLL_WT' ng-min='0' ng-style='frmChlnRollWt.DELV_ROLL_WT.$error.min? vm.errstyle:\"\"' ng-disabled='dataItem.LK_CLCF_MVM_STS_ID>570' /></ng-form>";
                    },
                    width: "8",
                    filterable: false
                },
                {
                    title: "Pass.Qty",
                    field: "PASS_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmChlnPassQty'><input type='number' class='form-control' name='PASS_QTY' ng-model='dataItem.PASS_QTY' min='0' ng-change='vm.onChangeQcQty(dataItem, frmChlnPassQty.PASS_QTY, frmChlnRejQty.REJ_QTY, frmChlnHoldQty.HOLD_QTY)' ng-style='frmChlnPassQty.PASS_QTY.$error.min||frmChlnPassQty.PASS_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                    },
                    width: "8%",
                    filterable: false
                },
                {
                    title: "Rej.Qty",
                    field: "REJ_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmChlnRejQty'><input type='number' class='form-control' name='REJ_QTY' ng-model='dataItem.REJ_QTY' min='0' ng-change='vm.onChangeQcQty(dataItem, frmChlnPassQty.PASS_QTY, frmChlnRejQty.REJ_QTY, frmChlnHoldQty.HOLD_QTY)' ng-style='frmChlnRejQty.REJ_QTY.$error.min||frmChlnRejQty.REJ_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                    },
                    width: "6%",
                    filterable: false
                },
                {
                    title: "Hold.Qty",
                    field: "HOLD_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmChlnHoldQty'><input type='number' class='form-control' name='HOLD_QTY' ng-model='dataItem.HOLD_QTY' min='0' ng-change='vm.onChangeQcQty(dataItem, frmChlnPassQty.PASS_QTY, frmChlnRejQty.REJ_QTY, frmChlnHoldQty.HOLD_QTY)' ng-style='frmChlnHoldQty.HOLD_QTY.$error.min||frmChlnHoldQty.HOLD_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                    },
                    width: "6%",
                    filterable: false
                }//,
                //{
                //    title: "Rcv.Qty",
                //    field: "RCV_QTY",
                //    footerTemplate: "#=sum#",
                //    template: function () {
                //        return "<ng-form name='frmRcvQty'><input type='number' class='form-control' name='RCV_QTY' ng-model='dataItem.RCV_QTY' min='0' ng-style='frmRcvQty.RCV_QTY.$error.min||frmRcvQty.RCV_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                //    },
                //    width: "6%",
                //    filterable: false
                //}                
            ]
        };

        vm.onChangeQcQty = function (dataItem, ctrl, ctrl1, ctrl2) {

            console.log(ctrl);
            console.log(ctrl1);
            console.log(ctrl2);

            var balQty = parseInt(dataItem.DELV_QTY) - (parseInt(dataItem.PASS_QTY) + parseInt(dataItem.REJ_QTY) + parseInt(dataItem.HOLD_QTY));//-(parseInt(dataItem.RCV_QTY) + parseInt(dataItem.HOLD_QTY)); //parseInt(dataItem.SEW_QTY) - parseInt(dataItem.REJECT_QTY);
            console.log(dataItem);
            //alert(balQty);

            if (balQty != 0) {
                ctrl.$setValidity("max", false);
                ctrl1.$setValidity("max", false);
                ctrl2.$setValidity("max", false);
            }
            else {
                ctrl.$setValidity("max", true);
                ctrl1.$setValidity("max", true);
                ctrl2.$setValidity("max", true);
            }

        };

        
        vm.Save = function () {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to save?';
            

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                //submitRcvData['IS_FINALIZE'] = pIS_FINALIZE;
                submitRcvData['RCV_DT'] = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

                var rcvData = vm.clcfChlnRcvGridDataSource.data();
                console.log(rcvData);
                
                submitRcvData.CLCF_STR_RCV_D_XML = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {
                    //ob['RCV_DT'] = $filter('date')($scope.form.RCV_DT, vm.dtFormat);

                    return {
                        KNT_SCO_CHL_CLCF_RCV_D_ID: ob.KNT_SCO_CHL_CLCF_RCV_D_ID, KNT_CLCF_STYL_ITEM_ID: ob.KNT_CLCF_STYL_ITEM_ID, MC_CLCF_ORD_REQ_ID: ob.MC_CLCF_ORD_REQ_ID,
                        DELV_QTY: ob.DELV_QTY, DELV_NO_ROLL: ob.DELV_NO_ROLL, DELV_ROLL_WT: ob.DELV_ROLL_WT, WT_MOU_ID: ob.WT_MOU_ID,
                        PASS_QTY: ob.PASS_QTY, REJ_QTY: ob.REJ_QTY, HOLD_QTY: ob.HOLD_QTY, ADDL_QTY: ob.ADDL_QTY, RCV_QTY: ob.RCV_QTY
                    };                  
                }));

                console.log(submitRcvData);
                //return;
                                
                return $http({
                    headers: { 'Authorization': 'Bearer ' + access_token },
                    url: '/api/Knit/KntCollarCuff/BatchSaveClcfInternalRcv',
                    method: 'post',
                    data: submitRcvData
                }).then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);
                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.form.KNT_SCO_CHL_RCV_H_ID = res['data'].PKNT_SCO_CHL_RCV_H_ID_RTN;
                            vm.form.USER_LEVEL = res['data'].PUSER_LEVEL_RTN;
                            vm.form.CHALAN_NO = res['data'].PCHALAN_NO_RTN;

                            $stateParams.pKNT_SCO_CHL_RCV_H_ID = res['data'].PKNT_SCO_CHL_RCV_H_ID_RTN;
                            $state.go('KntCollarCuffStrRcvH', { pKNT_SCO_CHL_RCV_H_ID: res['data'].PKNT_SCO_CHL_RCV_H_ID_RTN }, { notify: false });

                            vm.isSaved = true;
                            //vm.clcfRcvGridDataSource.read();
                            vm.clcfChlnRcvGridDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });

        };

        vm.cancel = function () {
            if ($state.current.data.CLCF_SRC_PROD_CAT_ID == 1) {
                return $state.go('KntCollarCuffStrRcvH4Smpl', { pKNT_SCO_CHL_RCV_H_ID: 0 }, { reload: 'KntCollarCuffStrRcvH4Smpl' });
            }
            else {
                return $state.go('KntCollarCuffStrRcvH', { pKNT_SCO_CHL_RCV_H_ID: 0 }, { reload: 'KntCollarCuffStrRcvH' });
            }            
        }

        vm.backToList = function () {
            if ($state.current.data.CLCF_SRC_PROD_CAT_ID == 1) {
                $state.go('KntCollarCuffStrRcvList4Smpl');
            }
            else {
                $state.go('KntCollarCuffStrRcvList');
            }            
        }

        vm.submitChallan = function () {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to submit?';
            
            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {
                                
                console.log(submitRcvData);
                //return;

                return $http({
                    headers: { 'Authorization': 'Bearer ' + access_token },
                    url: '/api/Knit/KntCollarCuff/SubmitClcfInternalChlnRcv',
                    method: 'post',
                    data: submitRcvData
                }).then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);
                        console.log(res['data']);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //vm.form.KNT_SCO_CHL_RCV_H_ID = res['data'].PKNT_SCO_CHL_RCV_H_ID_RTN;
                            vm.form.LK_CLCF_MVM_STS_ID = res['data'].PLK_CLCF_MVM_STS_ID_RTN;
                            //vm.form.LK_CLCF_MVM_STS_ID = 571;
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        }

        vm.finalizeChallan = function () {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to save and finalize?';
            
            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                submitRcvData['IS_FINALIZE'] = 'Y';
                submitRcvData['RCV_DT'] = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

                var rcvData = vm.clcfChlnRcvGridDataSource.data();
                console.log(rcvData);


                submitRcvData.CLCF_STR_RCV_D_XML = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {
                    //ob['RCV_DT'] = $filter('date')($scope.form.RCV_DT, vm.dtFormat);

                    return {
                        KNT_CLCF_STR_RCV_D_ID: ob.KNT_CLCF_STR_RCV_D_ID, KNT_CLCF_STYL_ITEM_ID: ob.KNT_CLCF_STYL_ITEM_ID, MC_CLCF_ORD_REQ_ID: ob.MC_CLCF_ORD_REQ_ID,
                        DELV_QTY: ob.DELV_QTY, DELV_NO_ROLL: ob.DELV_NO_ROLL, DELV_ROLL_WT: ob.DELV_ROLL_WT, WT_MOU_ID: ob.WT_MOU_ID,
                        PASS_QTY: ob.PASS_QTY, REJ_QTY: ob.REJ_QTY, HOLD_QTY: ob.HOLD_QTY, ADDL_QTY: ob.ADDL_QTY, RCV_QTY: ob.RCV_QTY
                    };
                }));

                console.log(submitRcvData);
                //return;

                return $http({
                    headers: { 'Authorization': 'Bearer ' + access_token },
                    url: '/api/Knit/KntCollarCuff/FinalizeClcfInternalChlnRcv',
                    method: 'post',
                    data: submitRcvData
                }).then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {
                        //alert('test');
                        res['data'] = angular.fromJson(res.data.jsonStr);
                        console.log(res['data']);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.form.LK_CLCF_MVM_STS_ID = res['data'].PLK_CLCF_MVM_STS_ID_RTN;
                            
                            vm.isSaved = true;
                            vm.clcfChlnRcvGridDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });

        };


    }

})();
////////// End Collar Cuff Header Controller











////////// Start List Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntCollarCuffStrRcvListController', ['logger', 'config', '$q', '$scope', '$http', 'Hub', '$rootScope', 'exception', '$filter', '$state', '$stateParams', '$location', 'KnittingDataService', 'Dialog', 'cur_user_id', 'access_token', KntCollarCuffStrRcvListController]);
    function KntCollarCuffStrRcvListController(logger, config, $q, $scope, $http, Hub, $rootScope, exception, $filter, $state, $stateParams, $location, KnittingDataService, Dialog, cur_user_id, access_token) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        
        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();

        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        //vm.LIST_FROM = $stateParams.pLIST_FROM;

        //vm.IS_PROG_PARAM = 'Y';
        //vm.IS_NEXT = 'Y';
        //var key = 'SCM_STR_OIL_REQ_H_ID';
        //vm.today = new Date();
        //vm.form = {
        //    STR_REQ_DT: vm.today, SCM_STR_OIL_REQ_H_ID: 0, HR_COMPANY_ID: 1, RF_REQ_TYPE_ID: 19, ISS_STORE_ID: 12,
        //    RCV_STORE_ID: 13, STR_REQ_BY_NAME: cur_user_id, OIL_REQ_D_XML: ''
        //};
        //vm.formItem = { SCM_STR_TR_REQ_D_ID: 0 };

        //$('#FAB_ROLL_NO').focus();

        //$("input[type=text]").focus(function () { $(this).select(); }).mouseup(
        //   function (e) {
        //       e.preventDefault();
        //   });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getClcfStrRcvList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }

        var hub = new Hub('dashboard', {
            listeners: {
                'newConnection': function (id) {
                    vm.connected.push(id);
                    $rootScope.$apply();
                },
                'removeConnection': function (id) {
                    $rootScope.$apply();
                },
                'broadcastClcfStrRcvList': function () {
                    getClcfStrRcvList();
                    $rootScope.$apply()
                },

            },
            methods: [],
            errorHandler: function (error) {
                console.error(error);
            }
        });

        vm.connected = [];


        vm.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.STR_REQ_DT_LNopened = true;
        };


        vm.clcfStrRcvOptions = {
            height: 400,
            sortable: true,
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
            columns: [
                { field: "RCV_DT", title: "Rcv Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "CLCF_CHL_TYPE_NAME", title: "Challan Type", type: "string", width: "15%" },
                { field: "CHALAN_NO", title: "Challan#", type: "string", width: "14%" },
                { field: "CHALAN_DT", title: "Chln Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                //{ field: "ISS_STORE_NAME_EN", title: "From", width: "15%" },
                //{ field: "RCV_STORE_NAME_EN", title: "To", width: "15%" },
                //{ field: "RQD_QTY", title: "Rqd.Qty", width: "8%" },
                //{ field: "ISS_QTY", title: "Iss.Qty", width: "8%" },
                //{ field: "GATE_PASS_NO", title: "Gate Pass#", type: "string", width: "14%" },
                //{ field: "USER_LEVEL", title: "User Lavel", type: "string", width: "14%" },
                {
                    title: "Status",
                    field: "CLCF_MVM_STS_NAME",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.LK_CLCF_MVM_STS_ID<=570'>{{dataItem.CLCF_MVM_STS_NAME}}</label> <label class='label label-sm label-danger' ng-show='dataItem.LK_CLCF_MVM_STS_ID==571'>{{dataItem.CLCF_MVM_STS_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.LK_CLCF_MVM_STS_ID==572'>{{dataItem.CLCF_MVM_STS_NAME}}</label>";
                    },
                    width: "15%"
                },
                { field: "USER_VERIFIER_NAME", title: "Verifier", hidden: true },
                { field: "USER_ISSUER_NAME", title: "Issuer", hidden: true },
                {
                    title: "Action",
                    template: function () {
                        return "<a type='button' class='btn btn-xs blue' ng-click='vm.editChallan(dataItem)' ng-if='(dataItem.LK_CLCF_MVM_STS_ID==570 && dataItem.USER_LEVEL==\"CREATOR\")'><i class='fa fa-edit'></i></a>" +
                            "<a type='button' class='btn btn-xs blue' ng-click='vm.editChallan(dataItem)' ng-if='dataItem.LK_CLCF_MVM_STS_ID>570'>View</a>" +
                            "&nbsp;<a type='button' class='btn btn-xs blue' ng-click='vm.submitChallan(dataItem)' ng-if='(dataItem.LK_CLCF_MVM_STS_ID==570 && dataItem.USER_LEVEL==\"CREATOR\")'>Submit</a>" +
                            //"<a type='button' class='btn btn-xs blue' ng-click='vm.verifyRequsition(dataItem)' ng-if='(dataItem.RF_ACTN_STATUS_ID==45 && dataItem.USER_VERIFIER_NAME==\"VERIFIER\")'>Verify</a>&nbsp;" +
                            "&nbsp;<a type='button' class='btn btn-xs blue' ng-click='vm.receiveChallan(dataItem)' ng-if='(dataItem.LK_CLCF_MVM_STS_ID==571 && dataItem.USER_LEVEL==\"RECEIVER\")'>Receive</a>";
                    },
                    width: "15%"
                }
            ]
        };


        function getClcfStrRcvList() {

            vm.clcfStrRcvDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total",
                    model: {
                        id: "KNT_SCO_CHL_RCV_H_ID",
                        fields: {
                            RCV_DT: { type: "date", editable: false },
                            CHALAN_DT: { type: "date", editable: false }
                        }
                    }
                },
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntCollarCuff/GetClcfInternalChlnList';
                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pCLCF_SRC_PROD_CAT_ID=' + ($state.current.data.CLCF_SRC_PROD_CAT_ID||0);

                        console.log(url);

                        KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }



        vm.addNew = function () {
            if ($state.current.data.CLCF_SRC_PROD_CAT_ID == 1) {
                return $state.go('KntCollarCuffStrRcvH4Smpl', { pKNT_SCO_CHL_RCV_H_ID: 0 });
            }
            else {
                return $state.go('KntCollarCuffStrRcvH', { pKNT_SCO_CHL_RCV_H_ID: 0 });
            }
        };

        vm.editChallan = function (dataItem) {
            if ($state.current.data.CLCF_SRC_PROD_CAT_ID == 1) {
                return $state.go('KntCollarCuffStrRcvH4Smpl', { pKNT_SCO_CHL_RCV_H_ID: dataItem.KNT_SCO_CHL_RCV_H_ID });
            }
            else {
                return $state.go('KntCollarCuffStrRcvH', { pKNT_SCO_CHL_RCV_H_ID: dataItem.KNT_SCO_CHL_RCV_H_ID });
            }            
        };

        vm.receiveChallan = function (dataItem) {
            if ($state.current.data.CLCF_SRC_PROD_CAT_ID == 1) {
                return $state.go('KntCollarCuffStrRcvH4Smpl', { pKNT_SCO_CHL_RCV_H_ID: dataItem.KNT_SCO_CHL_RCV_H_ID });
            }
            else {
                return $state.go('KntCollarCuffStrRcvH', { pKNT_SCO_CHL_RCV_H_ID: dataItem.KNT_SCO_CHL_RCV_H_ID });
            }            
        };


        vm.submitChallan = function (dataItem) {
            var submitRcvData = angular.copy(dataItem);
            var vMsg = 'Do you want to submit?';

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                console.log(submitRcvData);
                //return;

                return $http({
                    headers: { 'Authorization': 'Bearer ' + access_token },
                    url: '/api/Knit/KntCollarCuff/SubmitClcfInternalChlnRcv',
                    method: 'post',
                    data: submitRcvData
                }).then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.clcfStrRcvDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        }

        vm.verifyRequsition = function (dataItem) {
            var submitRcvData = angular.copy(dataItem);
            var vMsg = 'Do you want to verify?';

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + $scope.token },
                    url: '/api/Knit/Req4SubStr/VerifyRequisition',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.mcOilReq4SubStrListDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.printChallan = function () {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3515';

            if (vm.form.SCM_STORE_ID == null || vm.form.SCM_STORE_ID == '') {
                vm.form.SCM_STORE_ID = -1;
            }

            var url;
            if (vm.isRDLC == true) {
                url = '/api/Knit/KntReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Knit/KntReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

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
////////// End List Controller
