////////// Start KntOrdWiseGreyTrnReqHController Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntOrdWiseGreyTrnReqHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', 'OrdWiseGreyTrnReqHdrData', 'userLavelData', KntOrdWiseGreyTrnReqHController]);
    function KntOrdWiseGreyTrnReqHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog, OrdWiseGreyTrnReqHdrData, userLavelData) {

        var vm = this;
        vm.showSplash = true;
        $scope.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        activate();

        console.log(userLavelData);
        $scope.userLavelData = userLavelData;

        if (userLavelData['USER_SENDER_NAME'] == 'SENDER') {
            vm.actionUserType = userLavelData['USER_SENDER_NAME'];
        }
        else {
            vm.actionUserType = userLavelData['USER_RECEIVER_NAME'];
        }

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pTRN_REQ_DT).format("DD-MMM-YYYY");
        
        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'KNT_ORD_TRN_REQ_H_ID';
        vm.today = new Date();

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME # (#: data.ORDER_NO #)</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME + " " + data.ORDER_NO #)</h5></span>';
        
        $scope.templateToColorFab = '<span><h5 style="padding:0px;margin:0px;">#: data.BKING_FABRIC_DESC #</h5><p> <b>Required Balance: #: data.GFAB_RQD_BAL_QTY # Kg</b></p></span>';
        $scope.valueTemplateToColorFab = '<span><h5 style="padding:0px;margin:0px;">#: data.BKING_FABRIC_DESC # <b>[Required Balance: #: data.GFAB_RQD_BAL_QTY # Kg]</b></h5></span>';

        $scope.stateParams = $stateParams;
        $scope.transSource = { LK_TRN_SRC_ID: $stateParams.pLK_TRN_SRC_ID };


        vm.form = OrdWiseGreyTrnReqHdrData[key] ? OrdWiseGreyTrnReqHdrData : {
            LK_TRN_SRC_ID: $stateParams.pLK_TRN_SRC_ID, TRN_TYPE_ID: 22, FRM_PROD_CAT_ID: 2, TO_PROD_CAT_ID: 2, TRN_REQ_DT: vm.today, KNT_ORD_TRN_REQ_H_ID: 0, TRN_REQ_BY: null, TRN_REQ_TO: null, TRN_REQ_NO: '',
            ACTION_USER_TYPE: vm.actionUserType, IS_PROJECTION_BK: 'Y', ORD_TRN_REQ_DTL_XML: ''
        };
        $scope.form = OrdWiseGreyTrnReqHdrData[key] ? OrdWiseGreyTrnReqHdrData : {
            LK_TRN_SRC_ID: $stateParams.pLK_TRN_SRC_ID, TRN_TYPE_ID: 22, FRM_PROD_CAT_ID: 2, TO_PROD_CAT_ID: 2, TRN_REQ_DT: vm.today, KNT_ORD_TRN_REQ_H_ID: 0, TRN_REQ_BY: null, TRN_REQ_TO: null, TRN_REQ_NO: '',
            ACTION_USER_TYPE: vm.actionUserType, IS_PROJECTION_BK: 'Y', ORD_TRN_REQ_DTL_XML: ''
        };

        $scope.viewOrdItem = {};
        $scope.viewToOrdItem = {};
        
        
        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        
        function activate() {
            var promise = [getTransactionTypeList(), getBuyerAcGrpList(), getByrAccWiseStyleExtList()];
            return $q.all(promise).then(function () {
                //vm.form.TRN_TYPE_ID = OrdWiseGreyTrnReqHdrData.TRN_TYPE_ID;
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }

        vm.trnDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.trnDateOpened = true;
        };

        vm.emoloyeeAuto = function (val) {
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        vm.onSelectItem = function (item) {
            console.log(item);

            vm.form['TRN_REQ_TO'] = item.HR_EMPLOYEE_ID;
            vm.form.TRN_REQ_TO_NAME = item.EMP_FULL_NAME_EN;
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.TRN_REQ_TO = null;
            }

        });

        function getTransactionTypeList() {
            vm.tranTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "TRN_TYPE_NAME",
                dataValueField: "TRN_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                     
                    vm.form.FRM_PROD_CAT_ID = item.FRM_PROD_CAT_ID;
                    vm.form.TO_PROD_CAT_ID = item.TO_PROD_CAT_ID;

                    $scope.form.FRM_PROD_CAT_ID = item.FRM_PROD_CAT_ID;
                    $scope.form.TO_PROD_CAT_ID = item.TO_PROD_CAT_ID;

                    $scope.viewOrdStyleExtDataSource.read();
                    $scope.viewToOrdStyleExtDataSource.read();
                },
                dataBound: function (e) {

                    //if ($stateParams.pKNT_ORD_TRN_REQ_H_ID<1) {

                        var ds = e.sender.dataSource.data();

                        if (vm.form.TRN_TYPE_ID) {
                            
                            var item = _.find(ds, function (o) {
                                return o.TRN_TYPE_ID == vm.form.TRN_TYPE_ID;
                            });

                            console.log(item);

                            if (item) {
                                
                                e.sender.value(item.TRN_TYPE_ID);

                                vm.form.TRN_TYPE_ID = item.TRN_TYPE_ID;
                                vm.form.FRM_PROD_CAT_ID = item.FRM_PROD_CAT_ID;
                                vm.form.TO_PROD_CAT_ID = item.TO_PROD_CAT_ID;

                                $scope.form.FRM_PROD_CAT_ID = item.FRM_PROD_CAT_ID;
                                $scope.form.TO_PROD_CAT_ID = item.TO_PROD_CAT_ID;

                                $scope.viewOrdStyleExtDataSource.read();
                                $scope.viewToOrdStyleExtDataSource.read();
                            }
                        }
                    //}
                }
            };

            vm.tranTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        var url = '/api/knit/KntOrdWiseGreyTrnReq/GetTransactionTypeList';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            if ($stateParams.pLK_TRN_SRC_ID == 726) {
                                e.success(res);
                            }
                            else {
                                e.success(_.filter(res, function (ob) { return ob.FRM_PROD_CAT_ID == 8 || (ob.FRM_PROD_CAT_ID == 2 && ob.TO_PROD_CAT_ID == 2) }));
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });            
        }

        function getBuyerAcGrpList() {
            $scope.viewBuyerAcGrpOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    //$stateParams.pMC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    $scope.viewOrdItem.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    //vm.form.MC_STYLE_H_ID = 0;

                    $scope.viewOrdStyleExtDataSource.read();
                }
            };

            $scope.viewBuyerAcGrpDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        var url = '/api/mrc/BuyerAcc/GetBuyerAccGrpList';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });



            $scope.viewToBuyerAcGrpOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    //$stateParams.pMC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    $scope.viewToOrdItem.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    //vm.form.MC_STYLE_H_ID = 0;

                    $scope.viewToOrdStyleExtDataSource.read();
                },
                dataBound: function (e) {                    
                    console.log($stateParams);

                    if ($stateParams.pLK_TRN_SRC_ID == 726) {
                        
                        $scope.viewToOrdItem.MC_BYR_ACC_GRP_ID = $stateParams.pTO_MC_BYR_ACC_GRP_ID;
                        $scope.viewToOrdItem.MC_STYLE_H_EXT_ID = $stateParams.pMC_STYLE_H_EXT_ID;

                        $scope.viewToOrdStyleExtDataSource.read();                        
                    }
                }
            };

            $scope.viewToBuyerAcGrpDataSource = new kendo.data.DataSource({                
                transport: {
                    read: function (e) {
                        
                        var url = '/api/mrc/BuyerAcc/GetBuyerAccGrpList';
                      
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        }

        function getByrAccWiseStyleExtList() {
            $scope.viewOrdStyleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $scope.viewOrdItem.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;
                    $scope.viewOrdItem.FROM_STYLE_NO = item.STYLE_NO;
                    $scope.viewOrdItem.FROM_ORDER_NO = item.ORDER_NO;

                    vm.getOrderColor(item.MC_FAB_PROD_ORD_H_ID);
                }
            };

            $scope.viewOrdStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pRF_FAB_PROD_CAT_ID=' + ($scope.form.FRM_PROD_CAT_ID || 0) + '&pMC_BYR_ACC_GRP_ID=' + (($scope.viewOrdItem.MC_BYR_ACC_GRP_ID > 0) ? $scope.viewOrdItem.MC_BYR_ACC_GRP_ID : 0);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });


            //===================== TO ============================================
            $scope.viewToOrdStyleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $scope.viewToOrdItem.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;
                    $scope.viewToOrdItem.TO_STYLE_NO = item.STYLE_NO;
                    $scope.viewToOrdItem.TO_ORDER_NO = item.ORDER_NO;

                    //vm.getToOrderColor(item.MC_FAB_PROD_ORD_H_ID);
                    vm.getToOrderFabBking(item.MC_FAB_PROD_ORD_H_ID, null);
                },
                dataBound: function (e) {

                    if ($stateParams.pLK_TRN_SRC_ID == 726) {                        
                        
                        var ds = e.sender.dataSource.data();

                        if ($stateParams.pTO_MC_FAB_PROD_ORD_H_ID) {
                            var item = _.find(ds, function (o) {
                                return o.MC_FAB_PROD_ORD_H_ID == $stateParams.pTO_MC_FAB_PROD_ORD_H_ID;
                            });

                            if (item) {
                                e.sender.value(item.MC_STYLE_H_EXT_ID);

                                $scope.viewToOrdItem.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;
                                $scope.viewToOrdItem.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;
                                $scope.viewToOrdItem.TO_STYLE_NO = item.STYLE_NO;
                                $scope.viewToOrdItem.TO_ORDER_NO = item.ORDER_NO;

                                //vm.getToOrderColor(item.MC_FAB_PROD_ORD_H_ID);
                                vm.getToOrderFabBking(item.MC_FAB_PROD_ORD_H_ID, $stateParams.pTO_MC_COLOR_ID);
                            }
                        }                        
                    }                    
                }
            };

            $scope.viewToOrdStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        if ($stateParams.pLK_TRN_SRC_ID == 726) {
                            console.log($stateParams);
                            var url = '/api/mrc/Order/GetOrderList?pRF_FAB_PROD_CAT_ID=' + ($scope.form.TO_PROD_CAT_ID || 2) + '&pMC_STYLE_H_EXT_ID=' + ($scope.viewToOrdItem.MC_STYLE_H_EXT_ID || $stateParams.pMC_STYLE_H_EXT_ID || '') + '&pMC_BYR_ACC_GRP_ID=' + (($scope.viewToOrdItem.MC_BYR_ACC_GRP_ID > 0) ? $scope.viewToOrdItem.MC_BYR_ACC_GRP_ID : 0);
                        }
                        else {
                            var url = '/api/mrc/Order/GetOrderList?pRF_FAB_PROD_CAT_ID=' + ($scope.form.TO_PROD_CAT_ID || 0) + '&pMC_BYR_ACC_GRP_ID=' + (($scope.viewToOrdItem.MC_BYR_ACC_GRP_ID > 0) ? $scope.viewToOrdItem.MC_BYR_ACC_GRP_ID : 0);
                        }

                        
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData);
                        console.log('=============================');
                        console.log(url);


                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        $scope.viewOrdColorOption = {
            optionLabel: "-- Select --",
            filter: "contains",
            autoBind: true,
            dataTextField: "COLOR_NAME_EN",
            dataValueField: "MC_COLOR_ID",
            select: function (e) {
                var item = this.dataItem(e.item);
                $scope.viewOrdItem.FROM_COLOR_NAME_EN = item.COLOR_NAME_EN;
                console.log(item);
            }
        };

        vm.getOrderColor = function (pMC_FAB_PROD_ORD_H_ID) {
            return $scope.viewOrdColorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/knit/FabProdKnitOrder/GetColorListByProdOrdID?pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        //=========== Start To Order Color ==================
        //$scope.viewToOrdColorOption = {
        //    optionLabel: "-- Select --",
        //    filter: "contains",
        //    autoBind: true,
        //    dataTextField: "COLOR_NAME_EN",
        //    dataValueField: "MC_COLOR_ID",
        //    select: function (e) {
        //        var item = this.dataItem(e.item);
        //        $scope.viewToOrdItem.TO_BKING_FABRIC_DESC = item.COLOR_NAME_EN;
        //        console.log(item);
        //    },
        //    dataBound: function (e) {

        //        if ($stateParams.pLK_TRN_SRC_ID == 726) {

        //            var ds = e.sender.dataSource.data();

        //            if ($stateParams.pTO_MC_COLOR_ID) {
        //                var item = _.find(ds, function (o) {
        //                    return o.MC_COLOR_ID == $stateParams.pTO_MC_COLOR_ID;
        //                });

        //                if (item) {
        //                    e.sender.value(item.MC_COLOR_ID);

        //                    $scope.viewToOrdItem.MC_COLOR_ID = item.MC_COLOR_ID;
        //                    $scope.viewToOrdItem.TO_BKING_FABRIC_DESC = item.COLOR_NAME_EN;                            
        //                }
        //            }
        //        }
        //    }
        //};

        //vm.getToOrderColor = function (pMC_FAB_PROD_ORD_H_ID) {
        //    return $scope.viewToOrdColorDataSource = new kendo.data.DataSource({
        //        transport: {
        //            read: function (e) {
        //                var url = '/api/knit/FabProdKnitOrder/GetColorListByProdOrdID?pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID;

        //                return KnittingDataService.getDataByFullUrl(url).then(function (res) {
        //                    e.success(res);
        //                }, function (err) {
        //                    console.log(err);
        //                });
        //            }
        //        }
        //    });
        //};
        //=========== End To Order Color ==================

        //=========== Start To Order Fabric Booking Data =======================
        $scope.viewToOrdFabBkingOption = {
            optionLabel: "-- Select --",
            filter: "contains",
            autoBind: true,
            dataTextField: "BKING_FABRIC_DESC",
            dataValueField: "MC_FAB_PROD_ORD_D_ID",
            select: function (e) {
                var item = this.dataItem(e.item);

                $scope.viewToOrdItem.MC_COLOR_ID = item.FAB_COLOR_ID;
                $scope.viewToOrdItem.TO_COLOR_NAME_EN = item.FAB_COLOR_NAME_EN;

                $scope.viewToOrdItem.TO_FAB_ITEM_DESC = item.FAB_ITEM_DESC;
                $scope.viewToOrdItem.GFAB_RQD_BAL_QTY = item.GFAB_RQD_BAL_QTY;

                $scope.viewToOrdItem.RF_FAB_TYPE_ID = item.RF_FAB_TYPE_ID;
                $scope.viewToOrdItem.RF_FIB_COMP_ID = item.RF_FIB_COMP_ID;
                $scope.viewToOrdItem.FIN_GSM = item.FIN_GSM;
                $scope.viewToOrdItem.LK_DIA_TYPE_ID = item.LK_DIA_TYPE_ID;

                console.log(item);
            },
            dataBound: function (e) {

                if ($stateParams.pLK_TRN_SRC_ID == 726) {

                    var ds = e.sender.dataSource.data();

                    //if ($stateParams.pTO_MC_COLOR_ID) {
                    //    var item = _.find(ds, function (o) {
                    //        return o.MC_COLOR_ID == $stateParams.pTO_MC_COLOR_ID;
                    //    });

                    //    if (item) {
                    //        e.sender.value(item.MC_COLOR_ID);

                    //        $scope.viewToOrdItem.MC_COLOR_ID = item.MC_COLOR_ID;
                    //        $scope.viewToOrdItem.TO_BKING_FABRIC_DESC = item.COLOR_NAME_EN;                            
                    //    }
                    //}
                }
            }
        };

        vm.getToOrderFabBking = function (pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID) {
            return $scope.viewToOrdFabBkingDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/knit/KntOrdWiseGreyTrnReq/GetOrdFabBkingList?pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + pFAB_COLOR_ID;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };
        //=========== End To Order Fabric Booking Data =======================



        $scope.viewFromOrderDataSource = new kendo.data.DataSource({            
            pageSize: 100,
            //aggregate: [
            //    { field: "TRN_FAB_QTY", aggregate: "sum" }
            //],
            schema: {                
                model: {
                    fields: {
                        TRN_FAB_QTY: { type: "number", filterable: false }
                    }
                }
            },
            transport: {
                read: function (e) {             
                    var url = '/api/knit/KntOrdWiseGreyTrnReq/GetProdBalQty4TransList?pMC_FAB_PROD_ORD_H_ID=' + ($scope.viewOrdItem.MC_FAB_PROD_ORD_H_ID || 0) + '&pFAB_COLOR_ID=' + ($scope.viewOrdItem.MC_COLOR_ID || 0);
                                       
                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }            
        });




        $scope.viewFromOrderOptions = {
            height: 250,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
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
            change: function (e) {
                //console.log(e.action);

                var total = calc();
                $("#myId").html(total.toString());

                //var grid = $("#ViewFromOrderGrid").data("kendoGrid");
                //grid.footer.find(".k-footer-template").replaceWith(grid.footerTemplate());                
            },
            columns: [
                //{
                //    //headerTemplate: "<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.selectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Select?'\"></i>",
                //    template: function () {
                //        return "<input type='checkbox' title='Select?' ng-model='dataItem.IS_SELECT' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-click='vm.viewOrdSelect(dataItem)' >";
                //    },
                //    width: "50px"
                //},
                { field: "KNT_STYL_FAB_ITEM_ID", title: "Item", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" }, filterable: false },
                { field: "FAB_ITEM_DESC", title: "Fabric", headerAttributes: { "class": "col-md-7" }, attributes: { "class": "col-md-7" }, filterable: false },
                //{ field: "NO_OF_ROLL", title: "No of Roll", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" }, filterable: false },
                { field: "FAB_ROLL_WT", title: "Store Rcv Qty", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" }, filterable: false },
                { field: "ALREADY_TRN_FAB_QTY", title: "Issue Qty", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" }, filterable: false },
                { field: "BAL_QTY", title: "Bal Qty", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" }, filterable: false, footerTemplate: "Total:" },
                {
                    title: "Trns Qty",
                    field: "TRN_FAB_QTY",
                    template: function () {
                        return "<ng-form name='frmRcvFabQty'><input type='number' class='form-control' name='TRN_FAB_QTY' ng-model='dataItem.TRN_FAB_QTY' min='0' max='{{dataItem.BAL_QTY}}' ng-style='(frmRcvFabQty.TRN_FAB_QTY.$error.min||frmRcvFabQty.TRN_FAB_QTY.$error.max)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    headerAttributes: { "class": "col-md-2" }, attributes: { "class": "col-md-2" },
                    filterable: false,
                    footerTemplate: "<span id='myId'>0</span>"
                    //footerTemplate: "#=kendo.toString(sum, '0.00')#"
                }
                //{ field: "RCV_FAB_QTY", title: "Trns Qty", headerAttributes: { "class": "col-md-2" }, attributes: { "class": "col-md-2" }, filterable: false }
            ]
        };

        
        

        function calc() {
            // assume this to be dynamically determined  
            var field = "TRN_FAB_QTY";

            // assume this to be dynamically determined
            var dataSource = $scope.viewFromOrderDataSource;

            // some custom calc logic
            var newValue = 0;

            angular.forEach($scope.viewFromOrderDataSource.data(), function (val, key) {
                newValue += val['TRN_FAB_QTY'];
            });

            console.log(newValue);
         
            return newValue;
        }

        
        $scope.$watch('vm.form', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                }
            }
        }, true);

        //$scope.$watchGroup(['vm.form.SCM_STORE_ID', 'vm.form.RCV_DT'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            vm.selectedItem = undefined;
        //            vm.errors = null;

        //            vm.IS_NEXT = 'N';
                    
        //            //vm.next();
        //            vm.rollRcvGridDataSource.read();
        //        }
        //    }
        //}, true);
              

    }

})();
////////// End KntOrdWiseGreyTrnReqHController Controller









////////// Start KntOrdWiseGreyTrnReqDController Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntOrdWiseGreyTrnReqDController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', '$modal', 'KnittingDataService', 'Dialog', KntOrdWiseGreyTrnReqDController]);
    function KntOrdWiseGreyTrnReqDController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, $modal, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        //vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;               
        
        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }

      
        vm.viewOrdSelect = function (dataItem) {
            var data = $scope.$parent.viewFromOrderDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            //console.log(data);

            angular.forEach(data, function (val, key) {
                
                if (dataItem['uid'] == val['uid']) {
                    val['IS_SELECT'] = "Y";
                }
                else {
                    val['IS_SELECT'] = "N";
                }
            });
        };
        
        vm.loadFabric4Trans = function () {
            $scope.$parent.viewFromOrderDataSource.read();
        }
        
        vm.addTrnsDtlRow = function () {
            var fromDataList = _.filter($scope.$parent.viewFromOrderDataSource.data(), function (ob) {
                return ob['TRN_FAB_QTY'] > 0;
            });

            angular.forEach(fromDataList, function (val, key) {

                console.log(val);

                var data = {
                    KNT_ORD_TRN_REQ_D_ID: -1, 
                    FAB_PROD_ORD_H_ID1: $scope.$parent.viewOrdItem.MC_FAB_PROD_ORD_H_ID, FROM_STYLE_NO: $scope.$parent.viewOrdItem.FROM_STYLE_NO, FROM_ORDER_NO: $scope.$parent.viewOrdItem.FROM_ORDER_NO,
                    FAB_COLOR_ID1: $scope.$parent.viewOrdItem.MC_COLOR_ID, FROM_COLOR_NAME_EN: $scope.$parent.viewOrdItem.FROM_COLOR_NAME_EN,
                    KNT_STYL_FAB_ITEM_ID1: val['KNT_STYL_FAB_ITEM_ID'], FROM_FAB_ITEM_DESC: val['FAB_ITEM_DESC'], TRN_FAB_QTY: val['TRN_FAB_QTY'],                    
                    FAB_PROD_ORD_H_ID2: $scope.$parent.viewToOrdItem.MC_FAB_PROD_ORD_H_ID, MC_FAB_PROD_ORD_D_ID2: $scope.$parent.viewToOrdItem.MC_FAB_PROD_ORD_D_ID, TO_STYLE_NO: $scope.$parent.viewToOrdItem.TO_STYLE_NO, TO_ORDER_NO: $scope.$parent.viewToOrdItem.TO_ORDER_NO,
                    FAB_COLOR_ID2: $scope.$parent.viewToOrdItem.MC_COLOR_ID, TO_COLOR_NAME_EN: $scope.$parent.viewToOrdItem.TO_COLOR_NAME_EN,
                    TO_FAB_ITEM_DESC: $scope.$parent.viewToOrdItem.TO_FAB_ITEM_DESC, GFAB_RQD_BAL_QTY: $scope.viewToOrdItem.GFAB_RQD_BAL_QTY
                };

                if ($scope.viewOrdItem.MC_BYR_ACC_GRP_ID != 13 || $scope.viewToOrdItem.MC_BYR_ACC_GRP_ID != 13) {
                    alert("Sorry! You can only bulk order to bulk order transfer of Lindex buyer.");
                }
                else if (data.TRN_FAB_QTY > 0 && val.RF_FAB_TYPE_ID == $scope.$parent.viewToOrdItem.RF_FAB_TYPE_ID && val.RF_FIB_COMP_ID == $scope.$parent.viewToOrdItem.RF_FIB_COMP_ID
                    /*&& val.FIN_GSM == $scope.$parent.viewToOrdItem.FIN_GSM*/ && val.LK_DIA_TYPE_ID == $scope.$parent.viewToOrdItem.LK_DIA_TYPE_ID
                    /*&& data.TRN_FAB_QTY <= $scope.viewToOrdItem.GFAB_RQD_BAL_QTY*/) {

                    vm.transDtlDataSource.insert(0, data);
                    $("#ViewFromOrderGrid").data("kendoGrid").dataSource.data([]);
                }
                    //else if (data.TRN_FAB_QTY > $scope.viewToOrdItem.GFAB_RQD_BAL_QTY) {
                    //    alert("Sorry! Transfer fabric qty must be less than or equal to grey fabric required balance.");
                    //}
                else if (val.RF_FAB_TYPE_ID != $scope.$parent.viewToOrdItem.RF_FAB_TYPE_ID) {
                    alert("Sorry! Fabric Type is not match with both fabric items.");
                }
                else if (val.RF_FIB_COMP_ID != $scope.$parent.viewToOrdItem.RF_FIB_COMP_ID) {
                    alert("Sorry! Fabric composition is not match with both fabric items.");
                }
                else if (val.LK_DIA_TYPE_ID != $scope.$parent.viewToOrdItem.LK_DIA_TYPE_ID) {
                    alert("Sorry! Finish dia type is not match with both fabric items.");
                }
                else {
                    alert("Sorry! Fabrication or composition or finish dia type are not match with both fabric items.");
                }
                
                //$scope.$parent.viewFromOrderDataSource.read();
            });

        }
        

        vm.removeTrnsDtlRow = function (dataItem) {
            vm.transDtlDataSource.remove(dataItem);
        };

               

        vm.transDtlOptions = {
            height: 350,
            sortable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            navigatable: true,
            filterable: false,
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
            columns: [
                {
                    title: "From Order",                    
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: center; font-weight: bold"//,
                        //width: "65%"
                    },                    
                    columns: [
                        { field: "KNT_STYL_FAB_ITEM_ID1", title: "Item1", width: "10%" },
                        { field: "FROM_STYLE_NO", title: "Style#", width: "10%" },
                        { field: "FROM_ORDER_NO", title: "Order#", width: "10%" },
                        //{ field: "TODAY_RCV_NO_ROLL", title: "Order#", headerAttributes: { "class": "col-md-2" }, attributes: { "class": "col-md-2" } },
                        { field: "FROM_COLOR_NAME_EN", title: "Color", width: "10%" },
                        { field: "FROM_FAB_ITEM_DESC", title: "Fabric", width: "15%" },
                        { field: "TRN_FAB_QTY", title: "TrQty", width: "5%", footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                    ]
                },
                {
                    title: "To Order",
                    headerAttributes: {
                        "class": "table-header-cell col-md-4",
                        style: "text-align: center; font-weight: bold"//,
                        //width: "30%"
                    },                    
                    columns: [
                        { field: "KNT_STYL_FAB_ITEM_ID2", title: "Item2", width: "10%" },
                        { field: "TO_STYLE_NO", title: "Style#", width: "10%" },
                        { field: "TO_ORDER_NO", title: "Order#", width: "10%" },
                        { field: "TO_COLOR_NAME_EN", title: "Color", width: "10%" },
                        { field: "TO_FAB_ITEM_DESC", title: "Fabric", width: "15%" }
                    ]
                },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeTrnsDtlRow(dataItem)'  ><i class='fa fa-remove'></i></button>";
                    },
                    width: "5%"
                }
                
            ]            
        };


        vm.transDtlDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    
                    var url = '/api/knit/KntOrdWiseGreyTrnReq/GetFabTransDtl?pKNT_ORD_TRN_REQ_H_ID=' + ($stateParams.pKNT_ORD_TRN_REQ_H_ID || $scope.$parent.form.KNT_ORD_TRN_REQ_H_ID || 0);
                    
                    console.log(url);

                    //e.success([]);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            aggregate: [
                { field: "TRN_FAB_QTY", aggregate: "sum" }
            ],            
            schema: {                
                model: {
                    id: "KNT_ORD_TRN_REQ_D_ID",
                    fields: {                        
                        TRN_FAB_QTY: { type: "number", filterable: false }
                    }
                }
            }
        });


        vm.submitData = function (token, pIS_FINALIZE, pNEXT_RF_ACTN_STATUS_ID) {
            var submitData = angular.copy($scope.$parent.form);
            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZE == 'Y') {
                vMsg = 'Do you want to save and finalize?';
            }


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                submitData['IS_FINALIZED'] = pIS_FINALIZE;
                submitData['NEXT_RF_ACTN_STATUS_ID'] = pNEXT_RF_ACTN_STATUS_ID;
                submitData['TRN_REQ_DT'] = $filter('date')($scope.$parent.form.TRN_REQ_DT, vm.dtFormat);
                
                var trnDtlData = vm.transDtlDataSource.data();
                console.log(trnDtlData);


                submitData.ORD_TRN_REQ_DTL_XML = KnittingDataService.xmlStringShort(trnDtlData.map(function (ob) {
                    return {
                        KNT_ORD_TRN_REQ_D_ID: ob.KNT_ORD_TRN_REQ_D_ID, KNT_ORD_TRN_REQ_H_ID: ob.KNT_ORD_TRN_REQ_H_ID, 
                        FAB_PROD_ORD_H_ID1: ob.FAB_PROD_ORD_H_ID1, FAB_COLOR_ID1: ob.FAB_COLOR_ID1, KNT_STYL_FAB_ITEM_ID1: ob.KNT_STYL_FAB_ITEM_ID1,
                        FAB_PROD_ORD_H_ID2: ob.FAB_PROD_ORD_H_ID2, MC_FAB_PROD_ORD_D_ID2: ob.MC_FAB_PROD_ORD_D_ID2, FAB_COLOR_ID2: ob.FAB_COLOR_ID2, KNT_STYL_FAB_ITEM_ID2: ob.KNT_STYL_FAB_ITEM_ID2,
                        //TO_FAB_ITEM_DESC: ob.TO_FAB_ITEM_DESC,
                        TRN_FAB_QTY: ob.TRN_FAB_QTY, RCV_FAB_QTY: ob.RCV_FAB_QTY, QTY_MOU_ID: 3
                    };
                }));

                
                console.log(submitData);
                //return;

                return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/KntOrdWiseGreyTrnReq/BatchSave').then(function (res) {
                    console.log(res);

                    $scope.$parent.errors = undefined;
                    if (res.success === false) {
                        $scope.$parent.errors = [];
                        $scope.$parent.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $stateParams.pKNT_ORD_TRN_REQ_H_ID = res.data.PKNT_ORD_TRN_REQ_H_ID_RTN;

                            $scope.$parent.form.KNT_ORD_TRN_REQ_H_ID = res.data.PKNT_ORD_TRN_REQ_H_ID_RTN;
                            $scope.$parent.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                            $scope.$parent.form.TRN_REQ_NO = res.data.PTRN_REQ_NO_RTN;

                            vm.transDtlDataSource.read();

                            if (res.data.PRF_ACTN_STATUS_ID_RTN == 96) {
                                $http({
                                    url: '/Knitting/Knit/FireMailSendOTP4GreyFabTran',
                                    method: 'get',
                                    params: { pLK_TRN_SRC_ID: $scope.$parent.transSource.LK_TRN_SRC_ID, pKNT_ORD_TRN_REQ_H_ID: (res.data.PKNT_ORD_TRN_REQ_H_ID_RTN || $scope.$parent.form.KNT_ORD_TRN_REQ_H_ID || $stateParams.pKNT_ORD_TRN_REQ_H_ID || 0) }
                                }).then(function (res) {
                                    console.log(res);                                    
                                });

                                $state.go('OrdWiseGreyTrnReqList');
                            }
                            else {
                                $state.go('OrdWiseGreyTrnReqH.Dtl', { pKNT_ORD_TRN_REQ_H_ID: res['data'].PKNT_ORD_TRN_REQ_H_ID_RTN }, { notify: false });
                            }
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.sendMail = function () {
            $http({
                url: '/Knitting/Knit/FireMailSendOTP4GreyFabTran',
                method: 'get',
                params: { pLK_TRN_SRC_ID: $scope.$parent.transSource.LK_TRN_SRC_ID, pKNT_ORD_TRN_REQ_H_ID: ($scope.$parent.form.KNT_ORD_TRN_REQ_H_ID || $stateParams.pKNT_ORD_TRN_REQ_H_ID || 0) }
            }).then(function (res) {
                console.log(res);
            });
        }

        vm.backToList = function () {
            return $state.go('OrdWiseGreyTrnReqList');
        };
        
        vm.otpModal = function () {
            
            var trnDtlData = vm.transDtlDataSource.data();
            console.log(trnDtlData);
            
            $scope.$parent.form.ORD_TRN_REQ_DTL_XML = KnittingDataService.xmlStringShort(trnDtlData.map(function (ob) {
                return {
                    KNT_ORD_TRN_REQ_D_ID: ob.KNT_ORD_TRN_REQ_D_ID, KNT_ORD_TRN_REQ_H_ID: ob.KNT_ORD_TRN_REQ_H_ID,
                    FAB_PROD_ORD_H_ID1: ob.FAB_PROD_ORD_H_ID1, FAB_COLOR_ID1: ob.FAB_COLOR_ID1, KNT_STYL_FAB_ITEM_ID1: ob.KNT_STYL_FAB_ITEM_ID1,
                    FAB_PROD_ORD_H_ID2: ob.FAB_PROD_ORD_H_ID2, MC_FAB_PROD_ORD_D_ID2: ob.MC_FAB_PROD_ORD_D_ID2, FAB_COLOR_ID2: ob.FAB_COLOR_ID2, KNT_STYL_FAB_ITEM_ID2: ob.KNT_STYL_FAB_ITEM_ID2,
                    //TO_FAB_ITEM_DESC: ob.TO_FAB_ITEM_DESC,
                    TRN_FAB_QTY: ob.TRN_FAB_QTY, RCV_FAB_QTY: ob.RCV_FAB_QTY, QTY_MOU_ID: 3
                };
            }));

            console.log($scope.$parent.form);

            $http({
                url: '/Knitting/Knit/FireMailSendOTP4GreyFabTran',
                method: 'get',
                params: { pLK_TRN_SRC_ID: $scope.$parent.transSource.LK_TRN_SRC_ID, pKNT_ORD_TRN_REQ_H_ID: ($scope.$parent.form.KNT_ORD_TRN_REQ_H_ID || $stateParams.pKNT_ORD_TRN_REQ_H_ID || 0) }
            }).then(function (res) {
                console.log(res);
                $scope.$parent.form.OTP_CODE = res.data.OTP_CODE;
            });

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'OrdWiseGreyTrnReqOtpModal.html',
                controller: 'KntOrdWiseGreyTrnReqOtpController',
                size: 'md',
                scope: $scope,
                windowClass: 'large-Modal'//,
                //resolve: {
                //    scBillHdrID: function () {
                //        return scBillHdrID;
                //    }
                //}
            });

            modalInstance.result.then(function (data) {
                //console.log('received');
                console.log(data);

                

            }, function () {
                $scope.$parent.form.OTP_CODE = null;
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.printBill = function () {
            //console.log(dataItem);
            $scope.$parent.form.REPORT_CODE = 'RPT-3563';

            var url;
            url = '/api/Knit/KntReport/PreviewReport';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');
            
            var params = angular.copy($scope.$parent.form);

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
////////// End KntOrdWiseGreyTrnReqDController Controller





// Start OTP Modal Controller
(function () {
    'use strict';

    angular.module('multitex.knitting').controller('KntOrdWiseGreyTrnReqOtpController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'KnittingDataService', 'Dialog', KntOrdWiseGreyTrnReqOtpController]);
    function KntOrdWiseGreyTrnReqOtpController($modalInstance, $q, $scope, $http, $state, $filter, config, KnittingDataService, Dialog) {

        $scope.showSplash = true;
        $scope.countOfSubmit = 1;

        activate();
        console.log($scope.$parent.form);
        console.log($scope.$parent.form.OTP_CODE);

        $scope.today = new Date();
        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

       

        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                $scope.showSplash = false;
            });
        }


        $scope.save = function (token, valid) {
            if (!valid) return;
           
            //console.log($scope.$parent.form);

            var submitData = angular.copy($scope.$parent.form);
            
            submitData['TRN_REQ_DT'] = $filter('date')($scope.$parent.form.TRN_REQ_DT, $scope.dtFormat);
            submitData['IS_FINALIZED'] = "Y";

            $scope.countOfSubmit = $scope.countOfSubmit + 1;

            if ($scope.countOfSubmit > 5) {
                $scope.cancel();
            }


            if ($scope.$parent.form.OTP_CODE === $scope.OTP_CODE) {

                console.log(submitData);

                return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/KntOrdWiseGreyTrnReq/BatchSave').then(function (res) {
                    console.log(res);

                    $scope.errors = undefined;
                    if (res.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //$stateParams.pKNT_ORD_TRN_REQ_H_ID = res.data.PKNT_ORD_TRN_REQ_H_ID_RTN;

                            //$scope.$parent.form.KNT_ORD_TRN_REQ_H_ID = res.data.PKNT_ORD_TRN_REQ_H_ID_RTN;
                            $scope.$parent.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                            //$scope.$parent.form.TRN_REQ_NO = res.data.PTRN_REQ_NO_RTN;

                            //vm.transDtlDataSource.read();
                            //$state.go('OrdWiseGreyTrnReqH.Dtl', { pKNT_ORD_TRN_REQ_H_ID: res['data'].PKNT_ORD_TRN_REQ_H_ID_RTN }, { notify: false });
                            $modalInstance.close(submitData);
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
            else {
                $scope.errors = [];
                $scope.errors = [{ error: 'Sorry! OTP mismatch. Try maximum ' + (6-$scope.countOfSubmit).toString() + ' times' }];
            }

        };


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();
// End OTP Modal Controller





//=============== Start for KntOrdWiseGreyTrnReqListController List =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntOrdWiseGreyTrnReqListController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'userLavelData', KntOrdWiseGreyTrnReqListController]);
    function KntOrdWiseGreyTrnReqListController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, commonDataService, userLavelData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.form = {};

        console.log(userLavelData);
        vm.userLavelData = userLavelData;

        if (userLavelData['USER_SENDER_NAME'] == 'SENDER') {
            vm.isVisableAddBtn = 'Y';
        }
        else {
            vm.isVisableAddBtn = 'N';
        }

        vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME # (#: data.ORDER_NO_LST #)</p></span>';
        vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME||" "||data.ORDER_NO_LST #)</h5></span>';

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerAcGrpList(), getByrAccWiseStyleExtList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.transAuto = function (val) {
            return KnittingDataService.getDataByUrl('/KntOrdWiseGreyTrnReq/GetTransData4AutoSearch?&pTRN_REQ_NO=' + val);
        };

        $scope.onSelectItem = function (item) {
            //console.log(item);
            vm.form.KNT_ORD_TRN_REQ_H_ID = item.KNT_ORD_TRN_REQ_H_ID;
        }

        $scope.$watch('vm.form.TRN_REQ_NO', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.KNT_ORD_TRN_REQ_H_ID = null;
            }

        });

        function getBuyerAcGrpList() {

            vm.buyerAcGrpOption = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,                
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    //$stateParams.pMC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    //vm.form.MC_STYLE_H_ID = 0;

                    vm.viewOrdStyleExtDataSource.read();
                },
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };

            vm.buyerAcGrpDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/BuyerAcc/GetBuyerAccGrpList';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getByrAccWiseStyleExtList() {
            vm.viewOrdStyleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;                    
                }
            };

            vm.viewOrdStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pRF_FAB_PROD_CAT_ID=8&pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : null);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
            
        };

        vm.ordTrnListGridOption = {
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
                { field: "TRN_REQ_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "TRN_REQ_NO", title: "Transfer#", type: "string", width: "15%" },
                { field: "TRN_TYPE_NAME", title: "Trans Type", type: "string", width: "15%" },
                { field: "REMARKS", title: "Remarks", width: "30%", filterable: false },                
                {
                    field: "ACTN_STATUS_NAME",
                    title: "Status",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID<=92'>{{dataItem.ACTN_STATUS_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==96'>{{dataItem.ACTN_STATUS_NAME}}</label> <label class='label label-sm label-danger' ng-show='dataItem.RF_ACTN_STATUS_ID==123'>{{dataItem.ACTN_STATUS_NAME}}</label>";
                    },
                    width: "15%"
                },
                {
                    title: "Action",
                    template: function () {                        
                        return "<button type='button' class='btn btn-xs blue' title='Edit/View' ng-click='vm.editBill(dataItem)'><i class='fa fa-edit'></i></button>";
                    },
                    width: "5%"
                }
            ]
        };

        vm.ordTrnListGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_ORD_TRN_REQ_H_ID",
                    fields: {
                        TRN_REQ_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KntOrdWiseGreyTrnReq/GetOrdWiseGreyTrnReqList?&pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : null) + '&pMC_FAB_PROD_ORD_H_ID=' + ((vm.form.MC_FAB_PROD_ORD_H_ID > 0) ? vm.form.MC_FAB_PROD_ORD_H_ID : null) + '&pKNT_ORD_TRN_REQ_H_ID=' + ((vm.form.KNT_ORD_TRN_REQ_H_ID > 0) ? vm.form.KNT_ORD_TRN_REQ_H_ID : null);

                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    console.log(url);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    });
                }
            }//,
            //sort: [{ field: 'TRN_REQ_DT', dir: 'desc' }]
        });


        vm.getBillList = function () {
            vm.ordTrnListGridDataSource.read();

            //$state.go('OrdWiseGreyTrnReqList', { pTRN_REQ_TO: (vm.form.TRN_REQ_TO || 0) }, { notify: false });
        };
        
        vm.editBill = function (dataItem) {
            $state.go('OrdWiseGreyTrnReqH.Dtl', { pLK_TRN_SRC_ID: dataItem.LK_TRN_SRC_ID, pKNT_ORD_TRN_REQ_H_ID: dataItem.KNT_ORD_TRN_REQ_H_ID });
        };

        vm.printBill = function (dataItem) {
            //console.log(dataItem);
            dataItem.REPORT_CODE = 'RPT-3563';

            var url;
            url = '/api/Knit/KntReport/PreviewReport';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            var params = angular.copy(dataItem);

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
//=============== End for KntOrdWiseGreyTrnReqListController List =================