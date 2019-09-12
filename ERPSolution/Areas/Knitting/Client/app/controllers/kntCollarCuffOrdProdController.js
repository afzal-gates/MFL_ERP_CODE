////////// Start Collar Cuff Header Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('kntCollarCuffOrdProdHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', 'clcfDesignData', kntCollarCuffOrdProdHController]);
    function kntCollarCuffOrdProdHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog, clcfDesignData) {

        var vm = this;
        vm.showSplash = true;
        $scope.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        var prodDate = $stateParams.pPROD_DT == '1900-Jan-01' ? moment(vm.today).format("DD-MMM-YYYY") : moment($stateParams.pPROD_DT).format("DD-MMM-YYYY");

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME # (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"-"||data.FAB_PROD_CAT_SNAME #)</h5></span>';

        //$scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.FAB_PROD_CAT_SNAME # (#: data.STYLE_NO #)</p></span>';
        //$scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        console.log('CLCF Design');
        console.log(clcfDesignData);

        vm.form = {
            MC_ORDER_H_ID_LST: '', COLOR_NAME_EN: '', PROD_DT: prodDate, COLLAR_DESIG_TYPE: clcfDesignData['COLLAR_DESIG_TYPE'] ? clcfDesignData['COLLAR_DESIG_TYPE'] : '',
            YARN_DETAIL: clcfDesignData['YARN_DETAIL'] ? clcfDesignData['YARN_DETAIL'] : ''
        };
        $scope.form = {
            MC_ORDER_H_ID_LST: '', COLOR_NAME_EN: '', PROD_DT: prodDate, COLLAR_DESIG_TYPE: clcfDesignData['COLLAR_DESIG_TYPE'] ? clcfDesignData['COLLAR_DESIG_TYPE'] : '',
            YARN_DETAIL: clcfDesignData['YARN_DETAIL'] ? clcfDesignData['YARN_DETAIL'] : ''
        };

        //var key = 'KNT_SC_GFAB_DLV_H_ID';
        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
        
        if ($stateParams.pIS_NEXT == 'Y') {
            $scope.IS_NEXT = 'Y';
        } else {
            $scope.IS_NEXT = 'N';
        }

        vm.orderOption = {
            optionLabel: "-- Select --",
            filter: "contains",
            autoBind: true,
            dataTextField: "ORDER_NO",
            dataValueField: "MC_STYLE_H_EXT_ID",
            select: function (e) {
                var item = this.dataItem(e.item);
                console.log(item);
                if (item.MC_ORDER_H_ID_LST) {
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID_LST;
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;

                    $stateParams.pMC_ORDER_H_ID_LST = item.MC_ORDER_H_ID_LST;
                    $stateParams.pMC_COLOR_ID = 0;
                    vm.getOrderColor(item.MC_STYLE_H_ID);
                }
                else {
                    vm.form.MC_ORDER_H_ID_LST = 0;
                    vm.form.MC_STYLE_H_ID = 0;
                    vm.getOrderColor('0');
                }
            }//,
            //dataBound: function (e) {                
            //    if ($stateParams.pMC_STYLE_H_EXT_ID != null && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
            //        vm.form.MC_STYLE_H_EXT_ID = $stateParams.pMC_STYLE_H_EXT_ID;

            //        if ($stateParams.pMC_ORDER_H_ID_LST != null) {
            //            vm.getOrderColor($stateParams.pMC_ORDER_H_ID_LST);
            //        }
            //    }
            //}
        };

        vm.orderDataSource = new kendo.data.DataSource({
            serverFiltering: true,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KntCollarCuff/GetCollarCuffOrdReq?pMC_BYR_ACC_GRP_ID=' + (vm.form.MC_BYR_ACC_GRP_ID || $stateParams.pMC_BYR_ACC_GRP_ID || null);
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


        //vm.form = $stateParams.pSCM_SUPPLIER_ID > 0 ? { SCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID } : { SCM_SUPPLIER_ID: 0 };

        activate();
        function activate() {
            var promise = [getProdTypeListData(), getBuyerAcGrpList(), /*getBuyerAccListData(),*/ getMachineList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;                
            });
        }

        vm.prodDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.prodDateOpened = true;
        };

        
        function getProdTypeListData() {
            return vm.prodTypeList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "FAB_PROD_CAT_SNAME",
                dataValueField: "RF_FAB_PROD_CAT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);                    
                },
                dataBound: function (e) {
                    if ($stateParams.pRF_FAB_PROD_CAT_ID != null && $stateParams.pRF_FAB_PROD_CAT_ID > 0) {
                        vm.form.RF_FAB_PROD_CAT_ID = $stateParams.pRF_FAB_PROD_CAT_ID;
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

                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.orderDataSource.read();
                },
                dataBound: function (e) {
                    if ($stateParams.pMC_BYR_ACC_GRP_ID != null && $stateParams.pMC_BYR_ACC_GRP_ID > 0) {
                        vm.form.MC_BYR_ACC_GRP_ID = $stateParams.pMC_BYR_ACC_GRP_ID;

                        vm.orderDataSource.read();
                    }
                },
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }

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
                        vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;

                        vm.getOrderByBuyerAC($stateParams.pMC_BYR_ACC_ID);
                    }
                }
            };
        };

        vm.getOrderByBuyerAC = function (pMC_BYR_ACC_ID) {
           
            return vm.orderDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntCollarCuff/GetCollarCuffOrdReq?pMC_BYR_ACC_ID=' + pMC_BYR_ACC_ID;
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
     
        vm.getOrderColor = function (pMC_STYLE_H_ID) {
            return vm.orderColorDataSource = new kendo.data.DataSource({                
                transport: {
                    read: function (e) {                        
                        //var url = '/api/mrc/Order/MultiOrderWiseColorList/' + pMC_ORDER_H_ID_LST;
                        var url = '/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + parseInt(pMC_STYLE_H_ID) + '?pIS_DUMMY_COLOR=Y';
                        //var url = '/api/mrc/Order/OrderItemWiseColorList?pIS_DUMMY_COLOR=Y&pMC_STYLE_H_ID=' + parseInt(pMC_STYLE_H_ID);
                                                
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getMachineList() {
            $scope.machineOption = {
                optionLabel: "-M/C-",
                filter: "contains",
                autoBind: true,                
                dataTextField: "KNT_MACHINE_NO",
                dataValueField: "KNT_MACHINE_ID"
            }

            return $scope.machineDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {                        
                        return KnittingDataService.getDataByFullUrl('/api/knit/KnitPlan/getKnitMachine?pKNT_MC_TYPE_ID=2').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        $scope.$watch('vm.form', function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    
                    $scope.form = vm.form;
                    $scope.form.PROD_DT = $filter('date')(vm.form.PROD_DT, vm.dtFormat);
                }
            }
        }, true);

        $scope.$watchGroup(['vm.form.RF_FAB_PROD_CAT_ID', 'vm.form.MC_BYR_ACC_ID', 'vm.form.MC_STYLE_H_EXT_ID', 'vm.form.MC_COLOR_ID', 'vm.form.PROD_DT'], function (newVal, oldVal) {

            console.log(newVal);
            console.log(oldVal);

            if (!_.isEqual(newVal, oldVal)) {
                if (oldVal[2] != undefined || oldVal[3] != newVal[3]) {
                    $scope.IS_NEXT = 'N';
                    $stateParams.pIS_NEXT = 'N';
                }
            }
        }, true);
                
        vm.nextLoad = function () {
            $scope.IS_NEXT = 'Y';

            return KnittingDataService.getDataByFullUrl('/api/knit/KntCollarCuff/GetCollarCuffDesigType?pRF_FAB_PROD_CAT_ID=' + (vm.form.RF_FAB_PROD_CAT_ID || $stateParams.pRF_FAB_PROD_CAT_ID) + '&pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID + '&pMC_COLOR_ID=' + vm.form.MC_COLOR_ID).then(function (res) {
                
                vm.form.COLLAR_DESIG_TYPE = res.COLLAR_DESIG_TYPE;
                vm.form.YARN_DETAIL = res.YARN_DETAIL;

                vm.form.CPI = res.CPI;
                vm.form.NO_PLY = res.NO_PLY;
                
                //var spDate = moment(vm.form.PROD_DT).format("DD-MMM-YYYY");
                $state.go('KntCollarCuffOrdProdH.dtl', {
                    pRF_FAB_PROD_CAT_ID: (vm.form.RF_FAB_PROD_CAT_ID || $stateParams.pRF_FAB_PROD_CAT_ID), pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID, pMC_COLOR_ID: vm.form.MC_COLOR_ID,
                    pMC_ORDER_H_ID_LST: (vm.form.MC_ORDER_H_ID_LST == '' ? $stateParams.pMC_ORDER_H_ID_LST : vm.form.MC_ORDER_H_ID_LST),
                    pPROD_DT: vm.form.PROD_DT == null ? $stateParams.pPROD_DT : moment(vm.form.PROD_DT).format("YYYY-MMM-DD"),
                    pIS_NEXT: $scope.IS_NEXT

                }, { reload: 'KntCollarCuffOrdProdH.dtl' });
                
            }, function (err) {
                console.log(err);
            });            
        }

    }

})();
////////// End Collar Cuff Header Controller




////////// Start Collar Cuff Detail Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('kntCollarCuffOrdProdDController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'collarCuffProdData', 'KnittingDataService', 'Dialog', kntCollarCuffOrdProdDController]);
    function kntCollarCuffOrdProdDController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, collarCuffProdData, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.collarCuffProdList = collarCuffProdData;

        vm.printButtonList = [{ BTN_ID: 1, BTN_NAME: 'Daily Production' }, { BTN_ID: 2, BTN_NAME: 'Order wise Production' }];

        vm.grandTotalCollarSumQty = function () {
            
            if (vm.collarCuffProdList.length > 0) {
                //alert('x');

                var reqRowTotCollarQty = _.sumBy(_.filter(vm.collarCuffProdList, function (ob) {
                    return ob; //ob.RF_GARM_PART_ID == 10
                })[0].ordSizeList, function (o) { return o.RQD_PCS_QTY; });
            }
            else if (collarCuffProdData.length > 0) {
                //alert('y');

                var reqRowTotCollarQty = _.sumBy(_.filter(collarCuffProdData, function (ob) {
                    return ob; //ob.RF_GARM_PART_ID == 10
                })[0].ordSizeList, function (o) { return o.RQD_PCS_QTY; });
            }
            else {
                var reqRowTotCollarQty = 0;
            }

            var totQty = 0;
            var totBalCollarQty = 0;

            if (vm.collarCuffProdList.length > 0) {
                var totProdCollarQty = _.sumBy(_.filter(vm.collarCuffProdList, function (ob) {
                    return ob; //ob.RF_GARM_PART_ID == 10
                })[0].ordSizeList, function (o) { return o.ALREADY_PRD_QTY_COLLAR; });

                var totDelvQty = _.sumBy(_.filter(vm.collarCuffProdList, function (ob) {
                    return ob; //ob.RF_GARM_PART_ID == 10
                })[0].ordSizeList, function (o) { return o.ALREADY_DELV_QTY; });
            }
            else {
                var totProdCollarQty = 0;
                var totDelvQty = 0;
            }

            angular.forEach(vm.collarCuffProdList, function (val, key) {
                angular.forEach(val['ordSizeList'], function (val1, key1) {
                    //if (val1['RF_GARM_PART_ID'] == 10) {                        
                    totQty = totQty + parseInt(val1['PRD_QTY']);
                    //}
                });
            });

            vm.reqRowTotCollarQty = reqRowTotCollarQty;
            vm.grandTotalCollarQty = totQty;
            vm.grandTotalProdCollarQty = totProdCollarQty;
            vm.grandTotalBalCollarQty = vm.reqRowTotCollarQty - vm.grandTotalProdCollarQty;

            vm.grandTotalDelvQty = totDelvQty;
        }

        //var key = 'KNT_SC_GFAB_DLV_H_ID';
        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        //vm.form = $stateParams.pSCM_SUPPLIER_ID > 0 ? { SCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID } : { SCM_SUPPLIER_ID: 0 };

        activate();
        function activate() {
            var promise = [getCollarCuffProd(), getYarnListData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.prodDateOpened = [];
        vm.prodDateOpen = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.prodDateOpened[index] = true;
        };

        function getYarnListData() {
            vm.yarnListOption = {
                optionLabel: "- Yarn -",
                filter: "contains",
                autoBind: true,
                dataTextField: "KNT_YRN_LOT_LST",
                dataValueField: "KNT_PLAN_D_ID"                
            }

            return vm.yarnListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        return KnittingDataService.getDataByFullUrl('/api/knit/KntCollarCuff/GetCollarCuffYarnList?pRF_FAB_PROD_CAT_ID=' + $stateParams.pRF_FAB_PROD_CAT_ID + '&pMC_STYLE_H_EXT_ID=' + $stateParams.pMC_STYLE_H_EXT_ID + '&pMC_COLOR_ID=' + $stateParams.pMC_COLOR_ID).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.onSelectClcfYarn = function (e) {

            console.log(e.sender.dataItem(e.item));
            //angular.forEach(vm.collarCuffProdList, function (val, key) {
            //    val['KNT_YRN_LOT_LST'] = e.sender.dataItem(e.item).KNT_YRN_LOT_LST;
            //});
        };


        vm.mainCollarCuffOrdReqGridOption = {
            height: "400px",
            scrollable: true,
            //expanded: true,
            //scrollable: {
            //    virtual: true               
            //},
            //sortable: true,
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
            dataBound: function () {
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            columns: [
                { field: "COLOR_NAME_EN", title: "Collar/Cuff Production Detail", width: "100%" }
            ]
        };

        vm.mainCollarCuffOrdReqGridDataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverSorting: true,
            //serverFiltering: true,            
            //pageSize: 10,
            //expanded: true,
            schema: {
                //data: "data",
                //total: "total",
                model: {
                    //id: "KNT_SC_GFAB_DLV_H_ID",
                    fields: {
                        COLOR_NAME_EN: { type: "string", editable: false }
                    }
                }
            },
            transport: {
                read: function (e) {
                    e.success([{ COLOR_NAME_EN: 'Collar/Cuff Production Detail' }]);
                }
            }//,            
            //sort: [{ field: 'CHALAN_DT', dir: 'desc' }]
        });

        vm.detailExpand = function (dtlDataItem) {

            if (dtlDataItem) {
                dtlDataItem['collarCuffProdList'] = vm.collarCuffProdList;

                return dtlDataItem;

                //return KnittingDataService.getDataByFullUrl("/api/knit/KntCollarCuff/GetCollarCuffOrdReqDtl?pMC_FAB_PROD_ORD_H_ID=" + dtlDataItem.MC_FAB_PROD_ORD_H_ID + '&pMC_STYLE_H_ID=' + dtlDataItem.MC_STYLE_H_ID).then(function (res) {

                //    console.log(dtlDataItem);

                //    dtlDataItem['itemsOrderColor'] = [];

                //    console.log(dtlDataItem);

                //}, function (err) {
                //    console.log(err);
                //});
            }            
        };



        

        //vm.collarCuffProdList = [];
        function getCollarCuffProd() {
            
            //return KnittingDataService.getDataByFullUrl('/api/knit/KntCollarCuff/GetCollarCuffProd?pRF_FAB_PROD_CAT_ID=2&pMC_STYLE_H_EXT_ID=' + $stateParams.pMC_STYLE_H_EXT_ID + '&pMC_COLOR_ID=' + $stateParams.pMC_COLOR_ID + '&pPROD_DT=' + $stateParams.pPROD_DT).then(function (res) {
                               
            //    console.log(res);
            //    vm.collarCuffProdList = res;
               
            //    console.log(vm.collarCuffProdList);

            //    vm.grandTotalCollarSumQty();               
            //});
            vm.grandTotalCollarSumQty();
        }
        
        vm.sizeWiseCollarSumQty = function (itmSize) {
            var totQty = 0;

            angular.forEach(vm.collarCuffProdList, function (val, key) {
                angular.forEach(val['ordSizeList'], function (val1, key1) {
                    if (val1['MESUREMENT'] == itmSize.MESUREMENT) {
                        totQty = totQty + parseInt(val1['PRD_QTY']);
                    }
                });                
            });
            
            return totQty;
        }
                
        vm.sizeWiseCollarAlreadyProdSumQty = function (itmSize) {
            var totQty = 0;

            //console.log(itmSize);

            angular.forEach(vm.collarCuffProdList, function (val, key) {
                angular.forEach(val['ordSizeList'], function (val1, key1) {
                    if (val1['MESUREMENT'] == itmSize.MESUREMENT) {
                        totQty = parseInt(val1['ALREADY_PRD_QTY_COLLAR']);
                    }
                });
            });

            return totQty;
        }
        
        vm.sizeWiseCollarBalSumQty = function (itmSize) {
            var totQty = 0;

            angular.forEach(vm.collarCuffProdList, function (val, key) {
                angular.forEach(val['ordSizeList'], function (val1, key1) {
                    if (val1['MESUREMENT'] == itmSize.MESUREMENT) {
                        totQty = parseInt(val1['ALREADY_PRD_QTY_COLLAR']);
                    }
                });
            });

            return itmSize.RQD_PCS_QTY - totQty;
        }

        vm.sizeWiseAlreadyDelvSumQty = function (itmSize) {
            var totQty = 0;

            //console.log(itmSize);

            angular.forEach(vm.collarCuffProdList, function (val, key) {
                angular.forEach(val['ordSizeList'], function (val1, key1) {
                    if (val1['MESUREMENT'] == itmSize.MESUREMENT) {
                        totQty = parseInt(val1['ALREADY_DELV_QTY']);
                    }
                });
            });

            return totQty;
        }
       
        
        vm.rowTotalQty = function (obj) {
            var totQty = 0;

            angular.forEach(obj['ordSizeList'], function (val1, key1) {
                totQty = totQty + parseInt(val1['PRD_QTY']);
            });
                        
            return totQty;
        }

        vm.newDate = function (index, obj) {
            var rowCount = vm.collarCuffProdList[0].MC_ROW_SPAN;
            
            for (var i = 0; i < rowCount; i++) {
                var dataSizeList = [];
                var dataItem = angular.copy(vm.collarCuffProdList[i]);

                angular.forEach(dataItem.ordSizeList, function(val, key){
                    val['PRD_QTY'] = 0;
                    val['KNT_CLCF_ORD_PRD_ID'] = 0;

                    dataSizeList.push(val);
                });

                vm.collarCuffProdList.push({
                    PROD_DT: obj.PROD_DT, PROD_DT_DISP: obj.PROD_DT_DISP, KNT_MACHINE_ID: 0, KNT_MACHINE_NO: "", GARM_PART_NAME: dataItem.GARM_PART_NAME, RF_GARM_PART_ID: dataItem.RF_GARM_PART_ID,
                    IS_DT_SAVE: 'N', IS_MC_SAVE: 'N', IS_DT_GRP: dataItem.IS_DT_GRP, DATE_ROW_SPAN: dataItem.MC_ROW_SPAN, IS_MC_GRP: dataItem.IS_MC_GRP, MC_ROW_SPAN: dataItem.MC_ROW_SPAN,
                    ordSizeList: dataSizeList
                });
            }
             
            //console.log(dataList);
        }

        vm.newMachine = function (index, obj) {            
            var rowCount = vm.collarCuffProdList[index].MC_ROW_SPAN;

            for (var i = 0; i < rowCount; i++) {
                var dataSizeList = [];
                var dataItem = angular.copy(vm.collarCuffProdList[i]);

                angular.forEach(dataItem.ordSizeList, function (val, key) {
                    val['PRD_QTY'] = 0;
                    val['KNT_CLCF_ORD_PRD_ID'] = 0;

                    dataSizeList.push(val);
                });

                vm.collarCuffProdList.push({
                    PROD_DT: dataItem.PROD_DT, PROD_DT_DISP: null, KNT_MACHINE_ID: 0, KNT_MACHINE_NO: "", GARM_PART_NAME: dataItem.GARM_PART_NAME, RF_GARM_PART_ID: dataItem.RF_GARM_PART_ID,
                    IS_DT_SAVE: 'Y', IS_MC_SAVE: 'N', IS_DT_GRP: "N", DATE_ROW_SPAN: dataItem.MC_ROW_SPAN, IS_MC_GRP: dataItem.IS_MC_GRP, MC_ROW_SPAN: dataItem.MC_ROW_SPAN,
                    ordSizeList: dataSizeList
                });
            }

            obj.DATE_ROW_SPAN = obj.DATE_ROW_SPAN + obj.MC_ROW_SPAN;
        }


        $scope.$parent.errors = undefined;
        vm.Save = function () {
            console.log(vm.collarCuffProdList);
            var submitData = { IS_FINALIZED: 'N' };
            var itemData = [];
            var prodDt;
            var mcID;
            var clcfKpID;

            var sizeWiseTotCollarReqQty = 0;
            var sizeWiseTotCuffReqQty = 0;
            var sizeWiseTotCollarProdQty=0;
            var sizeWiseTotCuffProdQty=0;

            $scope.$parent.errors = [];
            angular.forEach(vm.collarCuffProdList, function (val, key) {
                //if (val['IS_DT_GRP'] == "Y") {
                //    prodDt = $filter('date')(val['PROD_DT_DISP'], vm.dtFormat);
                //}
                if (val['IS_MC_GRP'] == "Y") {
                    mcID = val['KNT_MACHINE_ID'];
                    clcfKpID = val['KNT_PLAN_D_ID'];
                }
                
                angular.forEach(val['ordSizeList'], function (val1, key1) {
                                                                              
                    itemData.push({
                        KNT_CLCF_ORD_PRD_ID: val1['KNT_CLCF_ORD_PRD_ID'], PROD_DT: $filter('date')($scope.form.PROD_DT, vm.dtFormat), KNT_CLCF_STYL_ITEM_ID: val1['KNT_CLCF_STYL_ITEM_ID'],
                        MC_CLCF_ORD_REQ_ID: val1['MC_CLCF_ORD_REQ_ID'], KNT_MACHINE_ID: mcID, KNT_PLAN_D_ID: clcfKpID, KNT_YRN_LOT_LST: val['KNT_YRN_LOT_LST'], //LK_FK_DGN_TYP_ID: val1['LK_FK_DGN_TYP_ID'],
                        MC_FAB_PROD_ORD_H_ID: val1['MC_FAB_PROD_ORD_H_ID'], MC_FAB_PROD_ORD_D_ID: val1['MC_FAB_PROD_ORD_D_ID'], RF_FAB_TYPE_ID: val1['RF_FAB_TYPE_ID'], RF_FIB_COMP_ID: val1['RF_FIB_COMP_ID'],
                        PRD_QTY: val1['PRD_QTY'], RF_GARM_PART_ID: val1['RF_GARM_PART_ID'], MC_COLOR_ID: val1['MC_COLOR_ID'], MC_SIZE_ID_LST: val1['MC_SIZE_ID_LST'], SIZE_CODE_LST: val1['SIZE_CODE'], MESUREMENT: val1['MESUREMENT'],
                        CPI: $scope.$parent.form.CPI, NO_PLY: $scope.$parent.form.NO_PLY
                    });                    
                });

            });

            //if ($scope.$parent.errors != undefined && $scope.$parent.errors.length > 0) {
            //    $scope.$parent.errors = _.map(_.groupBy($scope.$parent.errors, function (doc) {
            //        return doc.GMT_PART_SZ_ID;
            //    }), function (grouped) {
            //        return grouped[0];
            //    });
                
            //    return;
            //}
            //else {
            //    $scope.$parent.errors = undefined;
            //}

            submitData.XML_DTL = KnittingDataService.xmlStringShort(itemData.map(function (ob) {                
                return ob;
            }));

            console.log(submitData);
            //return;

            return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/KntCollarCuff/BatchSaveCollarCuffProd').then(function (res) {
                console.log(res);

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $state.go('KntCollarCuffOrdProdH.dtl', {
                            pMC_BYR_ACC_ID: $stateParams.pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID: $stateParams.pMC_STYLE_H_EXT_ID, pMC_COLOR_ID: $stateParams.pMC_COLOR_ID, pMC_ORDER_H_ID_LST: $stateParams.pMC_ORDER_H_ID_LST
                        }, { reload: 'KntCollarCuffOrdProdH.dtl' });
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });


        };

        vm.back = function () {
            $state.go('KntCollarCuffOrdReqList', {});
        }

        vm.cancel = function () {
            $state.go('KntCollarCuffOrdProdH.dtl', {
                pMC_BYR_ACC_ID: $stateParams.pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID: $stateParams.pMC_STYLE_H_EXT_ID, pMC_COLOR_ID: $stateParams.pMC_COLOR_ID,
                pMC_ORDER_H_ID_LST: $stateParams.pMC_ORDER_H_ID_LST
            }, { reload: 'KntCollarCuffOrdProdH.dtl' });
        }
        
        vm.dataFinalize = function () {
            console.log(vm.collarCuffProdList);
            var submitData = { IS_FINALIZED: 'Y' };
            var itemData = [];
            var prodDt;
            var mcID;
            var clcfItemID;

            var sizeWiseTotCollarReqQty = 0;
            var sizeWiseTotCuffReqQty = 0;
            var sizeWiseTotCollarProdQty = 0;
            var sizeWiseTotCuffProdQty = 0;

            $scope.$parent.errors = [];
            angular.forEach(vm.collarCuffProdList, function (val, key) {
                //if (val['IS_DT_GRP'] == "Y") {
                //    prodDt = $filter('date')(val['PROD_DT_DISP'], vm.dtFormat);
                //}
                if (val['IS_MC_GRP'] == "Y") {
                    mcID = val['KNT_MACHINE_ID'];
                    clcfItemID = val['KNT_CLCF_STYL_ITEM_ID'];
                }

                angular.forEach(val['ordSizeList'], function (val1, key1) {

                    itemData.push({
                        KNT_CLCF_ORD_PRD_ID: val1['KNT_CLCF_ORD_PRD_ID'], PROD_DT: $filter('date')($scope.form.PROD_DT, vm.dtFormat), KNT_CLCF_STYL_ITEM_ID: clcfItemID, //val1['KNT_CLCF_STYL_ITEM_ID'],
                        MC_CLCF_ORD_REQ_ID: val1['MC_CLCF_ORD_REQ_ID'], KNT_MACHINE_ID: mcID,
                        MC_FAB_PROD_ORD_H_ID: val1['MC_FAB_PROD_ORD_H_ID'], MC_FAB_PROD_ORD_D_ID: val1['MC_FAB_PROD_ORD_D_ID'], RF_FAB_TYPE_ID: val1['RF_FAB_TYPE_ID'], RF_FIB_COMP_ID: val1['RF_FIB_COMP_ID'],
                        PRD_QTY: val1['PRD_QTY'], RF_GARM_PART_ID: val1['RF_GARM_PART_ID'], MC_COLOR_ID: val1['MC_COLOR_ID'], MC_SIZE_ID: val1['MC_SIZE_ID'], MESUREMENT: val1['MESUREMENT'],
                        CPI: $scope.$parent.form.CPI, NO_PLY: $scope.$parent.form.NO_PLY
                    });
                });

            });
          
            submitData.XML_DTL = KnittingDataService.xmlStringShort(itemData.map(function (ob) {
                return ob;
            }));

            console.log(submitData);
            //return;

            Dialog.confirm('After finalize, these data will lock.</br></br> <b>Do you want to save and finalize?</b>', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/KntCollarCuff/BatchSaveCollarCuffProd').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                $state.go('KntCollarCuffOrdProdH.dtl', {
                                    pMC_BYR_ACC_ID: $stateParams.pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID: $stateParams.pMC_STYLE_H_EXT_ID, pMC_COLOR_ID: $stateParams.pMC_COLOR_ID, pMC_ORDER_H_ID_LST: $stateParams.pMC_ORDER_H_ID_LST
                                }, { reload: 'KntCollarCuffOrdProdH.dtl' });
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });

        };

        vm.printProduction = function (itm) {
            var rptCode;
            if (itm.BTN_ID == 1) {
                rptCode = 'RPT-3548';
            }
            else if (itm.BTN_ID == 2) {
                rptCode = 'RPT-3547';
            }

            
            var data = angular.copy($scope.$parent.form);
            data['FROM_DATE'] = data.PROD_DT;
            data['MC_COLOR_ID'] = null;
            data['MC_STYLE_H_EXT_ID'] = null;
            
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            //var params = angular.copy($scope.form);
            var params = angular.extend({ REPORT_CODE: rptCode }, data);
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
////////// End Collar Cuff Detail Controller