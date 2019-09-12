//=============== Start for KntShortProdOrdHController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntShortProdOrdHController', ['$http', '$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', 'formData', '$timeout', KntShortProdOrdHController]);
    function KntShortProdOrdHController($http, $q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService, $modal, formData, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        vm.srtFabDtlState = ($state.current.name === 'KntShortFabProdOrdH.SrtFabDtl') ? true : false;
        vm.reason4srtFabState = ($state.current.name === 'KntShortFabProdOrdH.Reason4SrtFab') ? true : false;
        vm.resposibility4srtFabState = ($state.current.name === 'KntShortFabProdOrdH.Resposibility4SrtFab') ? true : false;

        var key = 'KNT_SRT_FAB_REQ_H_ID';

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';


        $scope.fabOrderGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetSrtFabDtlByID?pKNT_SRT_FAB_REQ_H_ID=' + ($stateParams.pKNT_SRT_FAB_REQ_H_ID || 0)).then(function (res) {

                        if (res.length > 0) {
                            angular.forEach(res, function (val, key) {
                                val['collarCuffReqList'] = [];
                            });

                            e.success(res);
                        }
                        else {
                            e.success([]);
                        }

                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                model: {
                    id: "KNT_SRT_FAB_REQ_D1_ID",
                    fields: {
                        //SC_START_DT: { type: "date", editable: false },
                        //SC_END_DT: { type: "date", editable: false }
                    }
                }
            }
        });

        $scope.reasonGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetSrtFabReasonByID?pKNT_SRT_FAB_REQ_H_ID=' + ($stateParams.pKNT_SRT_FAB_REQ_H_ID || 0)).then(function (res) {

                        if (res.length > 0) {
                            e.success(res);
                        }
                        else {
                            e.success([]);
                        }

                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                model: {
                    id: "KNT_SRT_FAB_REQ_D2_ID",
                    fields: {
                        //SC_START_DT: { type: "date", editable: false },
                        //SC_END_DT: { type: "date", editable: false }
                    }
                }
            }
        });


        $scope.respGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetSrtFabResponsibilityByID?pKNT_SRT_FAB_REQ_H_ID=' + ($stateParams.pKNT_SRT_FAB_REQ_H_ID || 0)).then(function (res) {

                        if (res.length > 0) {
                            e.success(res);
                        }
                        else {
                            e.success([]);
                        }

                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                model: {
                    id: "KNT_SRT_FAB_REQ_D3_ID",
                    fields: {
                        //SC_START_DT: { type: "date", editable: false },
                        //SC_END_DT: { type: "date", editable: false }
                    }
                }
            }
        });

        vm.form = formData[key] ? formData : {
            KNT_SRT_FAB_REQ_H_ID: -1, RF_FAB_PROD_CAT_ID: 2, MC_FAB_PROD_ORD_H_ID: 0, MC_STYLE_H_ID: 0, MC_STYLE_H_EXT_ID: 0, SFAB_REQ_DT: $filter('date')(vm.today, vm.dtFormat),
            LK_FAB_QTY_SRC_ID: 433
        };
        $scope.form = formData[key] ? formData : {
            KNT_SRT_FAB_REQ_H_ID: -1, RF_FAB_PROD_CAT_ID: 2, MC_FAB_PROD_ORD_H_ID: 0, MC_STYLE_H_ID: 0, MC_STYLE_H_EXT_ID: 0, SFAB_REQ_DT: $filter('date')(vm.today, vm.dtFormat),
            LK_FAB_QTY_SRC_ID: 433
        };

        vm.srtReason = { KNT_SRT_FAB_REQ_D2_ID: -1, KNT_SRT_FAB_REQ_H_ID: 0 };

        $scope.fabOrder = {
            KNT_SRT_FAB_REQ_D1_ID: -1, KNT_SRT_FAB_REQ_H_ID: -1, FAB_COLOR_ID: '', IS_CONTRAST: 'N', GMT_COLOR_ID: '',
            DIA_MOU_CODE: 'Inch', FIN_DIA_TYPE: 'Open', IS_FIN_DIA_REQ: 'Y',
            QTY_MOU_ID: 3, QTY_MOU_CODE: 'Kg', collarCuffReqList: []
        };

       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getUserList(), getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getFabricByStyleId(), getFinDiaByProdOrdId(), getColorList(),
                getDetailData(), getSrtFabBkReasonTyp()
            ];
            return $q.all(promise).then(function () {
                //$timeout(function () {
                //    if (formData[key]) {
                //        vm.form.SFAB_REQ_BY_NAME = formData['SFAB_REQ_BY_NAME'];// ? formData : {};
                //    }
                //}, 1000);

                vm.showSplash = false;                
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.SFAB_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.SFAB_REQ_DT_LNopened = true;
        }
        vm.LAST_REV_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.LAST_REV_DT_LNopened = true;
        }
        
        function getUserList() {
            
            return vm.userDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/SelectAllUserData';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.isReqAttnChangeCount = 0;
        vm.onChangeReqAttnList = function () {
            var ids = angular.copy(vm.form.SFAB_REQ_ATTN);

            if (vm.isReqAttnChangeCount >= 2) {
                $scope.reqAttnList = ids;
            }
            else {
                if (vm.isReqAttnChangeCount < 2) {
                    vm.isReqAttnChangeCount = vm.isReqAttnChangeCount + 1;
                }
            }
        }

        $scope.$watch('vm.form', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                }
            }
        }, true);


        //$scope.$watchGroup(['vm.form.RF_FAB_PROD_CAT_ID', 'vm.form.MC_BYR_ACC_ID', 'vm.form.MC_STYLE_H_EXT_ID'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            vm.IS_NEXT = 'N';
        //        }
        //    }
        //}, true);
        
        function getProdTypeList() {
            vm.prodTypeList = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                                e.success(_.filter(res, function (ob) { return ob.FAB_PROD_CAT_CODE == 'FP06'; }));
                            });
                        }
                    }
                },                
                dataTextField: "FAB_PROD_CAT_SNAME",
                dataValueField: "RF_FAB_PROD_CAT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.LK_FAB_QTY_SRC_ID = 433; //((item.FAB_PROD_CAT_CODE == 'FP01') ? 434 : 435);
                }//,
                //dataBound: function (e) {
                //    var ds = e.sender.dataSource.data();
                //    if (ds.length == 1) {
                //        e.sender.value(ds[0].HR_DEPARTMENT_ID);
                //        vm.form.HR_DEPARTMENT_ID = ds[0].HR_DEPARTMENT_ID;
                //        vm.form.CORE_DEPT_ID = ds[0].CORE_DEPT_ID;
                //    }
                //    else if (ds.length > 0 && $stateParams.pHR_DEPARTMENT_ID > 0) {

                //        e.sender.value($stateParams.pHR_DEPARTMENT_ID);
                //        vm.form.HR_DEPARTMENT_ID = $stateParams.pHR_DEPARTMENT_ID;
                //        var selectedDept = _.filter(ds, function (ob) {
                //            if ($stateParams.pHR_DEPARTMENT_ID == ob.HR_DEPARTMENT_ID) {
                //                //alert('Ok');
                //                vm.form.CORE_DEPT_ID = ob.CORE_DEPT_ID;
                //                return ob;
                //            }
                //        });
                //    }
                //}
            };
        }

        vm.onCategoryDataBound = function (e) {
            var ds = e.sender.dataSource.data();
            console.log(ds);

            if (ds.length == 1) {
                e.sender.value(ds[0].RF_FAB_PROD_CAT_ID);
                vm.form.RF_FAB_PROD_CAT_ID = ds[0].RF_FAB_PROD_CAT_ID;                
            }
            //else if (ds.length > 0 && $stateParams.pRF_FAB_PROD_CAT_ID > 0) {

            //    e.sender.value($stateParams.pRF_FAB_PROD_CAT_ID);
            //    vm.form.RF_FAB_PROD_CAT_ID = $stateParams.pRF_FAB_PROD_CAT_ID;
            //    var selectedDept = _.filter(ds, function (ob) {
            //        if ($stateParams.pHR_DEPARTMENT_ID == ob.HR_DEPARTMENT_ID) {
            //            //alert('Ok');
            //            vm.form.CORE_DEPT_ID = ob.CORE_DEPT_ID;
            //            return ob;
            //        }
            //    });
            //}
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

                    $stateParams.pMC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_STYLE_H_ID = 0;

                    vm.styleExtDataSource.read();
                    //$scope.styleFabricDataSource.read();
                    //$scope.finDiaDataSource.read();
                },
                dataBound: function (e) {
                    if ($stateParams.pMC_BYR_ACC_GRP_ID != null && $stateParams.pMC_BYR_ACC_GRP_ID > 0) {
                        vm.form.MC_BYR_ACC_GRP_ID = $stateParams.pMC_BYR_ACC_GRP_ID;

                        vm.styleExtDataSource.read();
                        $scope.styleFabricDataSource.read();
                        $scope.finDiaDataSource.read();
                        //vm.getOrderByBuyerAC($stateParams.pMC_BYR_ACC_ID);
                    }
                },
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

                    //vm.form.MC_FAB_PROD_ORD_H_ID_BLK = item.MC_FAB_PROD_ORD_H_ID;
                    $stateParams.pMC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;

                    $scope.colorListDataSource.read();
                    $scope.styleFabricDataSource.read();
                    $scope.finDiaDataSource.read();
                },
                dataBound: function (e) {

                    if ($stateParams.pMC_STYLE_H_EXT_ID != null && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                        vm.form.MC_STYLE_H_EXT_ID = $stateParams.pMC_STYLE_H_EXT_ID;
                        
                        $scope.styleFabricDataSource.read();
                        $scope.finDiaDataSource.read();
                    }
                }
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pRF_FAB_PROD_CAT_ID=2&pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0) + '&pMC_STYLE_H_EXT_ID=' + (($stateParams.pMC_STYLE_H_EXT_ID > 0) ? $stateParams.pMC_STYLE_H_EXT_ID : null);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {                            
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getFabricByStyleId() {

            $scope.styleFabricOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FABRIC_DESC",
                dataValueField: "MC_STYLE_D_FAB_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $scope.fabOrder.FABRIC_DESC = item.FABRIC_DESC;
                    $scope.fabOrder.RF_FAB_TYPE_ID = item.RF_FAB_TYPE_ID;
                    $scope.fabOrder.RF_FIB_COMP_ID = item.RF_FIB_COMP_ID;
                    $scope.fabOrder.FIN_GSM = item.FB_WT_MAX;

                    if (item.RF_FAB_TYPE_ID == 15 || item.RF_FAB_TYPE_ID == 20 || item.RF_FAB_TYPE_ID == 21 || item.RF_FAB_TYPE_ID == 51) {
                        $scope.fabOrder.IS_FIN_DIA_REQ = 'N';
                    }
                    else {
                        $scope.fabOrder.IS_FIN_DIA_REQ = 'Y';
                    }
                }
            };

            return $scope.styleFabricDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + ((vm.form.MC_STYLE_H_ID > 0) ? vm.form.MC_STYLE_H_ID : 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
                        
        };

        function getFinDiaByProdOrdId() {

            $scope.finDiaList = {
                optionLabel: "--- Select Dia ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $scope.fabOrder.FIN_DIA = item.FIN_DIA;
                    $scope.fabOrder.DIA_MOU_ID = item.DIA_MOU_ID;
                    $scope.fabOrder.LK_DIA_TYPE_ID = item.LK_DIA_TYPE_ID;
                },
                dataTextField: "FIN_DIA_N_DIA_TYPE",
                dataValueField: "FIN_DIA_N_DIA_TYPE"
            };

            return $scope.finDiaDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetFinDiaByProdOrdId?&pRF_FAB_PROD_CAT_ID=2&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || $stateParams.pMC_STYLE_H_EXT_ID || 0)).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        }

        function getColorList() {
            $scope.colorListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $scope.fabOrder.COLOR_NAME_EN = item.COLOR_NAME_EN;
                    $scope.fabOrder.LK_COL_TYPE_ID = item.LK_COL_TYPE_ID;

                    $scope.fabOrder.IS_CONTRAST = 'N';
                    $scope.fabOrder.GMT_COLOR_ID = item.MC_COLOR_ID;
                    $scope.fabOrder.GMT_COLOR_NAME_EN = item.COLOR_NAME_EN;
                },
            };

            $scope.gmtColorListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $scope.fabOrder.GMT_COLOR_NAME_EN = item.COLOR_NAME_EN;
                    if ($scope.fabOrder.IS_CONTRAST == 'Y') {
                        $scope.fabOrder['SP_INSTRUCTION'] = 'Contrast for ' + item.COLOR_NAME_EN;
                    }
                },
            };

            return $scope.colorListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + (vm.form.MC_STYLE_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getFabTypeList() {
            vm.fabTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FAB_TYPE_SNAME",
                dataValueField: "RF_FAB_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.FAB_TYPE_SNAME = item.FAB_TYPE_SNAME;
                }
            };

            return vm.fabTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Common/FabricTypeList?pINV_ITEM_CAT_ID=34').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getFabricCompList() {
            vm.fibCompOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FIB_COMP_NAME",
                dataValueField: "RF_FIB_COMP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    console.log(this.dataItem(e.item).IS_ELA_MXD);
                    vm.fabOrder.FIB_COMP_NAME = item.FIB_COMP_NAME;
                    vm.fabOrder.IS_ELA_MXD = item.IS_ELA_MXD;
                    
                    if (this.dataItem(e.item).RF_FIB_COMP_ID == -1) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'FabricCompositionGeneration.html',
                            controller: function (FiberCompList, FiberTypeList, FiberCompGroup, $modalInstance, $scope, $timeout, config) {
                                var vm = this;
                                vm.form = {};
                                vm.errors = [];
                                vm.FiberCompositionTypeList = FiberTypeList.map(function (o) {
                                    return {
                                        LOOKUP_DATA_ID: o.LOOKUP_DATA_ID,
                                        LK_DATA_NAME_EN: o.LK_DATA_NAME_EN
                                    }
                                });

                                vm.form['FIB_COMP_NAME'] = '';
                                vm.formDisabled = false;


                                vm.FeederTypeList = {
                                    optionLabel: "--Fiber Group--",
                                    filter: "contains",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                e.success(FiberCompGroup);
                                            }
                                        }
                                    },
                                    select: function (e) {
                                        var FabGroupOb = [];
                                        var FIB_TYPE_ID_LST = this.dataItem(e.item).FIB_TYPE_ID_LST;
                                        if (FIB_TYPE_ID_LST) {
                                            FIB_TYPE_ID_LST.split(',').forEach(function (val) {

                                                FabGroupOb.push({
                                                    LOOKUP_DATA_ID: parseInt(val),
                                                    LK_DATA_NAME_EN: _.filter(FiberTypeList, function (o) {
                                                        return o.LOOKUP_DATA_ID == parseInt(val)
                                                    })[0].LK_DATA_NAME_EN
                                                });
                                            });
                                        }
                                        vm.form['LK_FIB_TYPE_LST'] = FabGroupOb;
                                    },
                                    dataTextField: "RF_FIB_COMP_GRP_NAME",
                                    dataValueField: "RF_FIB_COMP_GRP_ID"
                                };
                                


                                $scope.$watch('vm.form.LK_FIB_TYPE_LST', function (newValOri, oldVal) {
                                    if (newValOri && newValOri.length > 0) {
                                        var newVal = angular.copy(newValOri);
                                        vm.form['FIB_COMP_NAME'] = '';

                                        newVal.map(function (o) {
                                            o.PERCENT = o.PERCENT || 0;
                                            return o;
                                        });

                                        var pecentValue = _.sumBy(newVal, function (o) {
                                            return o.PERCENT;
                                        });

                                        var isFillUpData = _.every(newVal, function (o) {
                                            return o.PERCENT > 0;
                                        })


                                        //if (isFillUpData) {
                                        //    var dataToBeCheck = [];
                                        //    FiberCompList.forEach(function (o) {
                                        //        dataToBeCheck.push(config.parseXmlString(o.LK_FIB_TYPE_LST).map(function (ob) {
                                        //            return {
                                        //                LOOKUP_DATA_ID: parseInt(ob.LOOKUP_DATA_ID),
                                        //                PERCENT: parseInt(ob.PERCENT)
                                        //            }
                                        //        }));

                                        //    });

                                        //    var isAvailable = _.some(dataToBeCheck, function (o) {

                                        //        return angular.equals(o, newVal.map(function (ob) {
                                        //            return {
                                        //                LOOKUP_DATA_ID: parseInt(ob.LOOKUP_DATA_ID),
                                        //                PERCENT: parseInt(ob.PERCENT)
                                        //            }
                                        //        }));

                                        //    });




                                        //}

                                        //if (isAvailable && isFillUpData) {

                                        //    $timeout(function () {
                                        //        vm.errors = [['Duplicate Composition is not allowed. Please try another']];
                                        //        vm.formDisabled = true;
                                        //    })

                                        //};

                                        if (pecentValue < 100 && isFillUpData) {
                                            vm.errors = [['Wrong Percent Value !!!. Percent Value must be 100 in total.']];
                                            vm.formDisabled = true;
                                        };


                                        if (pecentValue == 100 && isFillUpData) {

                                            vm.formDisabled = false;
                                            vm.errors = [];

                                        };

                                        if (pecentValue > 100 && isFillUpData) {
                                            vm.formDisabled = true;
                                            vm.errors = [['Wrong Percent Value !!!. Percent Value must be 100 in total.']];
                                        };


                                        newVal.forEach(function (val, key) {
                                            vm.form['FIB_COMP_NAME'] += ' ' + val.PERCENT + '% ' + val.LK_DATA_NAME_EN;
                                        });
                                        vm.form['FIB_COMP_NAME'] = vm.form['FIB_COMP_NAME'].replace('null', '').trim();
                                    } else {
                                        vm.form['FIB_COMP_NAME'] = '';
                                    }

                                }, true);


                                vm.cancel = function (data) {
                                    $modalInstance.close(data);
                                };
                                vm.submitData = function (dataOri, token) {
                                    var data = angular.copy(dataOri);


                                    data['IS_ELA_MXD'] = (
                                        _.some(data.LK_FIB_TYPE_LST, function (o) {
                                            return o.LOOKUP_DATA_ID == 375;
                                        })
                                        ) ? 'Y' : 'N';


                                    data['LK_FIB_TYPE_LST'] = config.xmlStringLong(
                                             data.LK_FIB_TYPE_LST.map(function (o) {
                                                 return {
                                                     LOOKUP_DATA_ID: o.LOOKUP_DATA_ID,
                                                     PERCENT: o.PERCENT
                                                 }
                                             })
                                         );

                                    return KnittingDataService.saveDataByFullUrl(data, '/api/mrc/StyleDItemFab/SaveFiberComposition', token).then(function (res) {
                                        if (res.success === false) {
                                            vm.errors = res.errors;
                                        }
                                        else {
                                            res['data'] = angular.fromJson(res.jsonStr);
                                            config.appToastMsg(res.data.PMSG);

                                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                                vm.form['RF_FIB_COMP_ID'] = parseInt(res.data.V_RF_FIB_COMP_ID);
                                            }
                                        }
                                    }).catch(function (message) {
                                        exception.catcher('XHR loading Failded')(message);
                                    });

                                };


                            },
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal',
                            resolve: {
                                FiberCompList: function () {
                                    return vm.fibCompDataSource.data();
                                },
                                FiberTypeList: function () {
                                    return KnittingDataService.LookupListData(76);
                                },
                                FiberCompGroup: function () {
                                    return KnittingDataService.getDataByFullUrl('/api/mrc/YarnSpec/FiberCompGroup');
                                }
                            }
                        });

                        modalInstance.result.then(function (result) {

                            if (result.RF_FIB_COMP_ID && result.RF_FIB_COMP_ID > 0) {
                                vm.fibCompDataSource.read().then(function () {
                                    vm.fabOrder['RF_FIB_COMP_ID'] = result.RF_FIB_COMP_ID;
                                });
                            }

                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });


                    }

                }                            
            };

            return vm.fibCompDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Common/FibreCompList').then(function (res) {
                            e.success([{ 'FIB_COMP_NAME': '---New Composition---', RF_FIB_COMP_ID: -1 }].concat(res));
                        });
                    }

                }
            });

        };
        
        function getMOUList() {
            vm.diaMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {                    
                    var item = this.dataItem(e.item);
                    vm.fabOrder.DIA_MOU_CODE = item.MOU_CODE;
                },
                dataBound: function (e) {
                    vm.fabOrder['DIA_MOU_ID'] = 23;
                }
            };

            vm.qtyMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.QTY_MOU_CODE = item.MOU_CODE;
                },
                dataBound: function (e) {
                    vm.fabOrder['QTY_MOU_ID'] = 3;
                }
            };

            return KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                
                vm.diaMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

                
                vm.qtyMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });
            });
            
        };

        function getDiaTypeList() {
            vm.diaTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {                    
                    var item = this.dataItem(e.item);
                    vm.fabOrder.FIN_DIA_TYPE = item.LK_DATA_NAME_EN;
                },
                dataBound: function (e) {
                    vm.fabOrder['LK_DIA_TYPE_ID'] = 327;
                }
            };

            return vm.diaTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/LookupListData/67').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };                

        function getMcDiaList() {
            vm.mcDiaOption = {
                optionLabel: "-- Dia --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MC_DIA",
                dataValueField: "KNT_MC_DIA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.MC_DIA = item.MC_DIA;
                },
            };

            return vm.mcDiaDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Knit/KnitCommon/getMachineDiaList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getMcGgList() {
            vm.mcGgOption = {
                optionLabel: "-- GG --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.MC_GG = item.LK_DATA_NAME_EN;
                },
            };

            return vm.mcGgDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByUrlNoToken('/api/common/LookupListData/59').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };


        //++++++++++ Start Reason +++++++++++++++++
        vm.isAddReasonToGrid = 'Y';

        function getSrtFabBkReasonTyp() {
            vm.reasonOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SFAB_RSN_TYPE_NAME",
                dataValueField: "RF_SFAB_RSN_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.srtReason.SFAB_RSN_TYPE_NAME = item.SFAB_RSN_TYPE_NAME;

                    if (item.RF_SFAB_RSN_TYPE_ID < 0) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'NewSFabBkReasonEntryModal.html',
                            controller: 'SFabBkReasonModalController',
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal'//,
                            //resolve: {
                            //    ColourList: function (KnittingDataService) {
                            //        return KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll');
                            //    }
                            //}
                        });

                        modalInstance.result.then(function (result) {
                            console.log(result);

                            if (result.RF_SFAB_RSN_TYPE_ID && result.RF_SFAB_RSN_TYPE_ID > 0) {
                                vm.reasonDataSource.read().then(function () {
                                    vm.srtReason['RF_SFAB_RSN_TYPE_ID'] = result.RF_SFAB_RSN_TYPE_ID;
                                    vm.srtReason['SFAB_RSN_TYPE_NAME'] = result.SFAB_RSN_TYPE_NAME;
                                });
                            }
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });
                    }

                }//,
                //dataBound: function (e) {
                //    vm.fabOrder['LK_DIA_TYPE_ID'] = 327;
                //}
            };

            return vm.reasonDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/getSrtFabBkReasonTyp').then(function (res) {
                            e.success([{ 'SFAB_RSN_TYPE_NAME': '---New Reason---', RF_SFAB_RSN_TYPE_ID: -1 }].concat(res));

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.addReasonRow = function (data) {
            //var fabOrderCopy = angular.copy(data);

            $scope.reasonGridDataSource.insert(0, data);

            vm.cancelReasonToGrid();

            //console.log($scope.reasonGridDataSource.data().length);
        }
        //console.log($scope.reasonGridDataSource.data().length);

        vm.updateReasonIndex = 0;
        vm.editReasonRow = function (dataItem) {
            var index = $scope.reasonGridDataSource.indexOf(dataItem);
            //alert('index:' + index);

            console.log(dataItem);
            var data = angular.copy(dataItem);
            vm.srtReason = data;

            vm.isAddReasonToGrid = 'N';
        };

        vm.removeReasonRow = function (dataItem) {
            $scope.reasonGridDataSource.remove(dataItem);
        };

        vm.updateReasonRow = function (data) {
            var fabOrderCopy = angular.copy(data);

            var dataItem = $scope.reasonGridDataSource.getByUid(data.uid);

            console.log(dataItem);

            dataItem.set('RF_SFAB_RSN_TYPE_ID', vm.srtReason.RF_SFAB_RSN_TYPE_ID);
            dataItem.set('REASON_DESC', vm.srtReason.REASON_DESC);

            dataItem.set('editMode', false);

            vm.cancelReasonToGrid();
        }

        vm.cancelReasonToGrid = function () {
            var data = angular.copy(vm.srtReason);
            vm.srtReason = { KNT_SRT_FAB_REQ_D2_ID: -1, KNT_SRT_FAB_REQ_H_ID: vm.form.KNT_SRT_FAB_REQ_H_ID, RF_SFAB_RSN_TYPE_ID: data.RF_SFAB_RSN_TYPE_ID, REASON_DESC: '' };

            vm.isAddReasonToGrid = 'Y';
        }


        $scope.reasonGridOption = {
            height: 150,
            sortable: true,
            columns: [
                { field: "SFAB_RSN_TYPE_NAME", title: "Type", headerAttributes: { "class": "col-md-2" }, attributes: { "class": "col-md-2" } },
                { field: "REASON_DESC", title: "Description", headerAttributes: { "class": "col-md-9" }, attributes: { "class": "col-md-9" } },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editReasonRow(dataItem)'><i class='fa fa-edit'></i></button>";
                    },
                    headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" }
                }
            ]
        };
        
        //++++++++++ End REason +++++++++++++++++

        $scope.respGridOption = {
            height: 220,
            sortable: true,
            columns: [
                { field: "RESP_DEPT_NAME", title: "Responsible Dept", width: "30%" },                
                { field: "PCT_DIST_QTY", title: "Percentage Share(%)", width: "20%" },
                { field: "DIST_QTY", title: "Share Distribution Qty", width: "20%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)'><i class='fa fa-edit'></i></button>";
                    },
                    width: "10%"
                }
            ]
        };

        $scope.fabOrderGridOption = {
            height: 200,
            sortable: true,
            columns: [
                { field: "COLOR_NAME_EN", title: "Fab Color", headerAttributes: { "class": "col-md-2" }, attributes: { "class": "col-md-2" } },
                { field: "GMT_COLOR_NAME_EN", title: "GMT Color", headerAttributes: { "class": "col-md-2" }, attributes: { "class": "col-md-2" } },
                { field: "FABRIC_DESC", title: "Fabric", headerAttributes: { "class": "col-md-3" }, attributes: { "class": "col-md-3" } },
                { field: "FIN_DIA_N_DIA_TYPE", title: "Fin.Dia", headerAttributes: { "class": "col-md-2" }, attributes: { "class": "col-md-2" } },
                { field: "DYE_LOT_NO", title: "Dye Lot#", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" } },
                { field: "RQD_FAB_QTY_NAME", title: "Qty", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" } },
                //{ field: "SC_START_DT", title: "Start Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                //{ field: "SC_END_DT", title: "End Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.MC_FAB_PROD_ORD_D_ID>0?true:false' ><i class='fa fa-remove'></i></button>";
                    },
                    headerAttributes: { "class": "col-md-1" },
                    attributes: { "class": "col-md-1" }
                }
            ],
            dataBound: function () {
                var dataSource = this.dataSource;
                this.element.find('tr.k-master-row').each(function () {
                    var row = $(this);
                    var data = dataSource.getByUid(row.data('uid'));

                    console.log(data);
                    console.log(data.get('RF_FAB_TYPE_ID'));
                    // this example will work if ReportId is null or 0 (if the row has no details)
                    if (data.get('RF_FAB_TYPE_ID') != 15 && data.get('RF_FAB_TYPE_ID') != 20 && data.get('RF_FAB_TYPE_ID') != 21) {
                        row.find('.k-hierarchy-cell a').css({ opacity: 0.3, cursor: 'default' }).click(function (e) { e.stopImmediatePropagation(); return false; });
                    }
                });
            }
        };

        

        $scope.detailExpand = function (dtlDataItem) {

            console.log(dtlDataItem);

            if (dtlDataItem) {
                return KnittingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetSrtCollarCuffReq?pMC_STYLE_H_EXT_ID=' + ($stateParams.pMC_STYLE_H_EXT_ID || 0) + '&pFAB_COLOR_ID=' + (dtlDataItem.FAB_COLOR_ID || 0) + '&pKNT_SRT_FAB_REQ_D1_ID=' + (dtlDataItem.KNT_SRT_FAB_REQ_D1_ID || 0)).then(function (res) {

                    dtlDataItem['collarCuffReqList'] = res;

                }, function (err) {
                    console.log(err);
                });
            }

        };


        

        
        
        function getDetailData() {
            $scope.fabOrderGridDataSource.read();
            $scope.reasonGridDataSource.read();
            $scope.respGridDataSource.read();
        }


        vm.save = function () {

            var submitData = angular.copy(vm.form);
            submitData['SFAB_REQ_ATTN'] = submitData.SFAB_REQ_ATTN_LIST == null ? '' : submitData.SFAB_REQ_ATTN_LIST.join(',');

            var fabOrdGridData = $scope.fabOrderGridDataSource.data();
            var reasonGridData = $scope.reasonGridDataSource.data();
            var respGridData = $scope.respGridDataSource.data();

            var reqD1List = [];
            var reqD11List = [];

            var fabIdx = 0;
            var clcfIdx = 0;

            angular.forEach(fabOrdGridData, function (val, key) {
                fabIdx = fabIdx + 1;
                
                val['GMT_COLOR_LST'] = val.GMT_COLOR_ID_LST == null ? '' : val.GMT_COLOR_ID_LST.join(',');

                reqD1List.push({
                    FAB_REQ_D1_INDX: fabIdx, KNT_SRT_FAB_REQ_D1_ID: val.KNT_SRT_FAB_REQ_D1_ID, KNT_SRT_FAB_REQ_H_ID: val.KNT_SRT_FAB_REQ_H_ID, FAB_COLOR_ID: val.FAB_COLOR_ID,
                    MC_STYLE_D_FAB_ID: val.MC_STYLE_D_FAB_ID, RF_FAB_TYPE_ID: val.RF_FAB_TYPE_ID, RF_FIB_COMP_ID: val.RF_FIB_COMP_ID, FIN_DIA: val.FIN_DIA,
                    DIA_MOU_ID: val.DIA_MOU_ID, LK_DIA_TYPE_ID: val.LK_DIA_TYPE_ID, FIN_GSM: val.FIN_GSM, RQD_FAB_QTY: val.RQD_FAB_QTY,
                    QTY_MOU_ID: val.QTY_MOU_ID, SP_INSTRUCTION: val.SP_INSTRUCTION, DYE_LOT_NO: val.DYE_LOT_NO, IS_CONTRAST: val.IS_CONTRAST, GMT_COLOR_ID: val.GMT_COLOR_ID
                });

                angular.forEach(val['collarCuffReqList'], function (val1, key1) {
                    reqD11List.push({
                        FAB_REQ_D1_INDX: fabIdx, KNT_SRT_FAB_REQ_D1_ID: val['KNT_SRT_FAB_REQ_D1_ID'], KNT_SRT_FAB_REQ_D11_ID: val1['KNT_SRT_FAB_REQ_D11_ID'],
                        MC_CLCF_ORD_REQ_ID: val1['MC_CLCF_ORD_REQ_ID'], RQD_PC_QTY: val1['RQD_PC_QTY']
                    });
                });

            });



            submitData.FAB_REQ_D1_XML = KnittingDataService.xmlStringShort(reqD1List.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            submitData.FAB_REQ_D11_XML = KnittingDataService.xmlStringShort(reqD11List.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            submitData.FAB_REQ_D2_XML = KnittingDataService.xmlStringShort(reasonGridData.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            submitData.FAB_REQ_D3_XML = KnittingDataService.xmlStringShort(respGridData.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            console.log(submitData);
            //return;

            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    //console.log(submitData);

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/SaveFabProdOrdH').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                
                                if (res.data.PKNT_SRT_FAB_REQ_H_ID_RTN != 0) {
                                    $stateParams.pKNT_SRT_FAB_REQ_H_ID = res.data.PKNT_SRT_FAB_REQ_H_ID_RTN;

                                    vm.form.KNT_SRT_FAB_REQ_H_ID = res.data.PKNT_SRT_FAB_REQ_H_ID_RTN;
                                    vm.form.SFAB_REQ_NO = res.data.PSFAB_REQ_NO_RTN;
                                    vm.form.MC_FAB_PROD_ORD_H_ID = res.data.PMC_FAB_PROD_ORD_H_ID_RTN;
                                    vm.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                                    
                                    if (res.data.PLAST_REV_NO_RTN > 0) {
                                        //alert(res.data.PLAST_REV_NO_RTN);
                                        vm.form.LAST_REV_NO = res.data.PLAST_REV_NO_RTN;
                                        vm.form.LAST_REV_DT = res.data.PLAST_REV_DT_RTN;
                                    }

                                    if (vm.srtFabDtlState == true) {
                                        $state.go('KntShortFabProdOrdH.SrtFabDtl', { pKNT_SRT_FAB_REQ_H_ID: res.data.PKNT_SRT_FAB_REQ_H_ID_RTN, pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });
                                    }
                                    else if (vm.reason4srtFabState == true) {
                                        $state.go('KntShortFabProdOrdH.Reason4SrtFab', { pKNT_SRT_FAB_REQ_H_ID: res.data.PKNT_SRT_FAB_REQ_H_ID_RTN, pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });
                                    }
                                    else if (vm.resposibility4srtFabState == true) {
                                        $state.go('KntShortFabProdOrdH.Resposibility4SrtFab', { pKNT_SRT_FAB_REQ_H_ID: res.data.PKNT_SRT_FAB_REQ_H_ID_RTN, pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });
                                    }

                                    getDetailData();
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        

        vm.submit = function () {

            var submitData = angular.copy(vm.form);
            submitData['SFAB_REQ_ATTN'] = submitData.SFAB_REQ_ATTN_LIST == null ? '' : submitData.SFAB_REQ_ATTN_LIST.join(',');

            Dialog.confirm('Do you want to submit?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/SubmitSrtFabProdOrdH').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                if (res.data.PRF_ACTN_STATUS_ID_RTN != 0) {
                                    vm.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                                    
                                    //$state.go('KntShortFabProdOrdH.SrtFabDtl', { pKNT_SRT_FAB_REQ_H_ID: res.data.PKNT_SRT_FAB_REQ_H_ID_RTN, pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });

                                    //vm.fabOrderGridDataSource.read();
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };


        vm.approve1 = function () {
            //alert('x');

            $http({
                url: '/Knitting/Knit/FireMail',
                method: 'get',
                params: { pKNT_SRT_FAB_REQ_H_ID: (vm.form.KNT_SRT_FAB_REQ_H_ID || 0) }
            });
        }


        vm.approve = function () {

            var totRespQty = 0;
            var totBkQty = 0;

            var respQtyList = _.filter($scope.respGridDataSource.data(), function(ob){
                return ob.KNT_SRT_FAB_REQ_D3_ID>0;
            });

            console.log(respQtyList);

            totRespQty = _.sumBy(respQtyList, function (o) { return o.DIST_QTY; });
            totBkQty = _.sumBy($scope.fabOrderGridDataSource.data(), function (o) { return o.RQD_FAB_QTY; });

            console.log('Booking: ' + totBkQty.toFixed(3) + 'Resp:' + totRespQty.toFixed(3));

            if (totBkQty.toFixed(3) != totRespQty.toFixed(3)) {
                alert('Fabric booking quantity & responsable department`s share quantity are not equal or Maybe you do not update.');
                //vm.errors.push({ key: ['Fabric booking quantity & responsable department`s share quantity are not equal. Maybe you do not update.'] });
                return;
            }


            var submitData = angular.copy(vm.form);
            submitData['SFAB_REQ_ATTN'] = submitData.SFAB_REQ_ATTN_LIST == null ? '' : submitData.SFAB_REQ_ATTN_LIST.join(',');

            Dialog.confirm('Do you want to approve and finalize?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/ApproveSrtFabProdOrdH').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                console.log(res.data);

                                if (res.data.PRF_ACTN_STATUS_ID_RTN != 0) {
                                    vm.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                                    
                                    //$state.go('KntShortFabProdOrdH.SrtFabDtl', { pKNT_SRT_FAB_REQ_H_ID: res.data.PKNT_SRT_FAB_REQ_H_ID_RTN, pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });

                                    //vm.fabOrderGridDataSource.read();

                                    $http({
                                        url: '/Knitting/Knit/FireMail',
                                        method: 'get',
                                        params: { pKNT_SRT_FAB_REQ_H_ID: (vm.form.KNT_SRT_FAB_REQ_H_ID || 0) }
                                    });

                                    //KnittingDataService.getDataByFullUrl('/Knitting/Knit/SendSrtFabAprovMail?To=dev.maruf@multi-fabs.com&pKNT_SRT_FAB_REQ_H_ID=' + (vm.form.KNT_SRT_FAB_REQ_H_ID || 0));
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };
        
        vm.revise = function () {

            var submitData = angular.copy(vm.form);
            submitData['SFAB_REQ_ATTN'] = submitData.SFAB_REQ_ATTN_LIST == null ? '' : submitData.SFAB_REQ_ATTN_LIST.join(',');

            Dialog.confirm('Do you want to revision?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/RevisionSrtFabProdOrdH').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                if (res.data.PRF_ACTN_STATUS_ID_RTN != 0) {
                                    vm.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                                    vm.form['LAST_REV_NO'] = res.data.PLAST_REV_NO_RTN;
                                    vm.form['LAST_REV_DT'] = vm.today; //res.data.PLAST_REV_DT_RTN;

                                    //$state.go('KntShortFabProdOrdH.SrtFabDtl', { pKNT_SRT_FAB_REQ_H_ID: res.data.PKNT_SRT_FAB_REQ_H_ID_RTN, pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });

                                    //vm.fabOrderGridDataSource.read();
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

    }
})();
//=============== End for KntShortProdOrdHController =================








//=============== Start for KntShortProdOrdDController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntShortProdOrdDController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', KntShortProdOrdDController]);
    function KntShortProdOrdDController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        //vm.fabOrder = {
        //    MC_FAB_PROD_ORD_D_ID: -1, MC_FAB_PROD_ORD_H_ID: -1, FIN_DIA_N_DIA_TYPE: '', DIA_MOU_ID: 23, DIA_MOU_CODE: 'Inch', FIN_DIA_TYPE: 'Open',
        //    QTY_MOU_ID: 3, QTY_MOU_CODE: 'Kg'
        //};


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        
        function getDiaTypeList() {
            vm.diaTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabOrder.FIN_DIA_TYPE = item.LK_DATA_NAME_EN;
                },
                dataBound: function (e) {
                    vm.fabOrder['LK_DIA_TYPE_ID'] = 327;
                }
            };

            return vm.diaTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/LookupListData/67').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        

        vm.onChangeContrast = function () {
            $scope.$parent.fabOrder['GMT_COLOR_ID'] = $scope.$parent.fabOrder['FAB_COLOR_ID'];
            if ($scope.$parent.fabOrder.IS_CONTRAST == 'Y') {
                $scope.$parent.fabOrder['SP_INSTRUCTION'] = 'Contrast for ' + $scope.$parent.fabOrder.GMT_COLOR_NAME_EN;
            }
        }
        
        vm.onChangeNetFfQty = function () {
            $scope.$parent.fabOrder.RQD_FAB_QTY_NAME = $scope.$parent.fabOrder.RQD_FAB_QTY + ' ' + $scope.$parent.fabOrder.QTY_MOU_CODE;
        }



        vm.addRow = function (data) {

            //console.log($scope.$parent.colorListDataSource.data());
            //console.log(data['GMT_COLOR_ID_LST']);

            //var gmtColorList = _.filter($scope.$parent.colorListDataSource.data(), function (ob) {
            //    return _.some(data['GMT_COLOR_ID_LST'], function (val) {
            //        return ob.MC_COLOR_ID == val;
            //    });
            //});

            //data['GMT_COLOR_NAME_EN'] = _.map(gmtColorList, 'COLOR_NAME_EN').join('+');

            

            var fabOrderCopy = angular.copy(data);

            $scope.$parent.fabOrderGridDataSource.insert(0, data);

            vm.cancelToGrid();

            vm.isAddToGrid = 'Y';
        }

        

        vm.updateIndex = 0;
        vm.editRow = function (dataItem) {
            var index = $scope.$parent.fabOrderGridDataSource.indexOf(dataItem);
            //alert('index:' + index);

            //vm.updateIndex = findGridIndex(index, dataItem.KNT_SC_YRN_RCV_H_ID);
            //alert(vm.updateHrdGridIndex);

            console.log(dataItem);
            if (dataItem.RF_FAB_TYPE_ID == 15 || dataItem.RF_FAB_TYPE_ID == 20 || dataItem.RF_FAB_TYPE_ID == 21) {
                dataItem['IS_FIN_DIA_REQ'] = 'N';
            }
            else {
                dataItem['IS_FIN_DIA_REQ'] = 'Y';
            }

            var data = angular.copy(dataItem);
            
            $scope.$parent.fabOrder = data;

            vm.isAddToGrid = 'N';
        };

        vm.removeRow = function (dataItem) {
            $scope.$parent.fabOrderGridDataSource.remove(dataItem);
        };

        vm.updateFabOrder = function (data) {

            //var gmtColorList = _.filter($scope.$parent.colorListDataSource.data(), function (ob) {
            //    return _.some(data['GMT_COLOR_ID_LST'], function (val) {
            //        return ob.MC_COLOR_ID == val;
            //    });
            //});
            //data['GMT_COLOR_NAME_EN_LST'] = _.map(gmtColorList, 'COLOR_NAME_EN').join('+');

            var fabOrderCopy = angular.copy(data);

            var dataItem = $scope.$parent.fabOrderGridDataSource.getByUid(data.uid);

            console.log(dataItem);

            dataItem.set('MC_STYLE_D_FAB_ID', fabOrderCopy.MC_STYLE_D_FAB_ID);
            dataItem.set('RF_FAB_TYPE_ID', fabOrderCopy.RF_FAB_TYPE_ID);
            dataItem.set('FABRIC_DESC', fabOrderCopy.FABRIC_DESC);
            dataItem.set('RF_FIB_COMP_ID', fabOrderCopy.RF_FIB_COMP_ID);

            dataItem.set('FAB_COLOR_ID', fabOrderCopy.FAB_COLOR_ID);
            dataItem.set('COLOR_NAME_EN', fabOrderCopy.COLOR_NAME_EN);
            dataItem.set('IS_CONTRAST', fabOrderCopy.IS_CONTRAST);
            dataItem.set('GMT_COLOR_ID', fabOrderCopy.GMT_COLOR_ID);
            dataItem.set('GMT_COLOR_NAME_EN', fabOrderCopy.GMT_COLOR_NAME_EN);
            dataItem.set('FIN_DIA', fabOrderCopy.FIN_DIA);
            dataItem.set('DIA_MOU_ID', fabOrderCopy.DIA_MOU_ID);
            dataItem.set('LK_DIA_TYPE_ID', fabOrderCopy.LK_DIA_TYPE_ID);
            dataItem.set('FIN_GSM', fabOrderCopy.FIN_GSM);
            dataItem.set('RQD_FAB_QTY', fabOrderCopy.RQD_FAB_QTY);
            dataItem.set('DYE_LOT_NO', fabOrderCopy.DYE_LOT_NO);
            dataItem.set('QTY_MOU_ID', fabOrderCopy.QTY_MOU_ID);
            dataItem.set('RQD_FAB_QTY_NAME', fabOrderCopy.RQD_FAB_QTY + ' Kg');
            dataItem.set('KNT_MC_DIA_ID', fabOrderCopy.KNT_MC_DIA_ID);
            dataItem.set('LK_MC_GG_ID', fabOrderCopy.LK_MC_GG_ID);

            dataItem.set('FIN_DIA_N_DIA_TYPE', fabOrderCopy.FIN_DIA_N_DIA_TYPE);
            dataItem.set('MC_DIA_N_GG', fabOrderCopy.MC_DIA_N_GG);

            $scope.$parent.fabOrder.MC_FAB_PROD_ORD_D_ID = -1;
            $scope.$parent.fabOrder.RQD_FAB_QTY = '';

            dataItem.set('editMode', false);

            vm.isAddToGrid = 'Y';
        }

        vm.cancelToGrid = function () {
            $scope.$parent.fabOrder.DYE_LOT_NO = '';
            $scope.$parent.fabOrder.RQD_FAB_QTY = '';

            $scope.$parent.fabOrder.MC_BLK_FAB_REQ_H_ID = 0;
            $scope.$parent.fabOrder.MC_BLK_FAB_REQ_D_ID = 0;
            $scope.$parent.fabOrder.IS_FIN_DIA_REQ = 'Y';

            vm.isAddToGrid = 'Y';
        };

        
        


        //vm.mainCollarCuffOrdReqGridOption = {
        //    height: "400px",
        //    scrollable: true,            
        //    filterable: {
        //        extra: false,
        //        operators: {
        //            string: {
        //                contains: "Contains",
        //                startswith: "Starts With",
        //                eq: "Is Equal To"
        //            }
        //        }
        //    },
        //    dataBound: function () {
        //        this.expandRow(this.tbody.find("tr.k-master-row").first());
        //    },
        //    columns: [
        //        { field: "COLOR_NAME_EN", title: "Collar/Cuff Production Detail", width: "100%" }
        //    ]
        //};

        //vm.mainCollarCuffOrdReqGridDataSource = new kendo.data.DataSource({            
        //    schema: {                
        //        model: {                   
        //            fields: {
        //                COLOR_NAME_EN: { type: "string", editable: false }
        //            }
        //        }
        //    },
        //    transport: {
        //        read: function (e) {
        //            e.success([{ COLOR_NAME_EN: 'Collar/Cuff Production Detail' }]);
        //        }
        //    }
        //});

        



        vm.saveSrtFabDtl = function () {

            var submitData = { KNT_SRT_FAB_REQ_H_ID: $scope.$parent.form.KNT_SRT_FAB_REQ_H_ID, FAB_REQ_D1_XML: '', FAB_REQ_D11_XML: '' };
            
            var fabOrdGridData = vm.fabOrderGridDataSource.data();
            var reqD1List = [];
            var reqD11List = [];

            var fabIdx = 0;
            var clcfIdx = 0;

            angular.forEach(fabOrdGridData, function (val, key) {
                fabIdx = fabIdx + 1;
                
                reqD1List.push({
                    FAB_REQ_D1_INDX: fabIdx, KNT_SRT_FAB_REQ_D1_ID: val.KNT_SRT_FAB_REQ_D1_ID, KNT_SRT_FAB_REQ_H_ID: val.KNT_SRT_FAB_REQ_H_ID, FAB_COLOR_ID: val.FAB_COLOR_ID,
                    MC_STYLE_D_FAB_ID: val.MC_STYLE_D_FAB_ID, RF_FAB_TYPE_ID: val.RF_FAB_TYPE_ID, RF_FIB_COMP_ID: val.RF_FIB_COMP_ID, FIN_DIA: val.FIN_DIA,
                    DIA_MOU_ID: val.DIA_MOU_ID, LK_DIA_TYPE_ID: val.LK_DIA_TYPE_ID, FIN_GSM: val.FIN_GSM, RQD_FAB_QTY: val.RQD_FAB_QTY,
                    QTY_MOU_ID: val.QTY_MOU_ID, SP_INSTRUCTION: val.SP_INSTRUCTION
                });

                angular.forEach(val['collarCuffReqList'], function (val1, key1) {
                    reqD11List.push({
                        FAB_REQ_D1_INDX: fabIdx, KNT_SRT_FAB_REQ_D1_ID: val['KNT_SRT_FAB_REQ_D1_ID'], KNT_SRT_FAB_REQ_D11_ID: val1['KNT_SRT_FAB_REQ_D11_ID'],
                        KNT_CLCF_STYL_ITEM_ID: val1['KNT_CLCF_STYL_ITEM_ID'], RQD_PC_QTY: val1['RQD_PC_QTY']
                    });
                });

            });

            

            submitData.FAB_REQ_D1_XML = KnittingDataService.xmlStringShort(reqD1List.map(function (ob) {                                
                console.log(ob);
                return ob;
            }));

            submitData.FAB_REQ_D11_XML = KnittingDataService.xmlStringShort(reqD11List.map(function (ob) {
                console.log(ob);
                return ob;
            }));
           
            console.log(submitData);
            //return;

            //Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                //.then(function () {

                    

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/SaveFabProdOrdD1').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                vm.fabOrderGridDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                //});
        };


    }
})();
//=============== End for KntShortProdOrdDController =================









//=============== Start for KntShortProdOrdReasonController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntShortProdOrdReasonController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', KntShortProdOrdReasonController]);
    function KntShortProdOrdReasonController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        
        vm.srtFabDtlState = true;

        vm.srtReason = { KNT_SRT_FAB_REQ_D2_ID: -1, KNT_SRT_FAB_REQ_H_ID: $scope.$parent.form.KNT_SRT_FAB_REQ_H_ID };


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        
       


        vm.addRow = function (data) {
            var fabOrderCopy = angular.copy(data);

            $scope.$parent.reasonGridDataSource.insert(0, data);

            vm.cancelToGrid();           
        }

        vm.updateIndex = 0;
        vm.editRow = function (dataItem) {
            var index = $scope.$parent.reasonGridDataSource.indexOf(dataItem);
            //alert('index:' + index);
      
            console.log(dataItem);
            var data = angular.copy(dataItem);
            vm.srtReason = data;

            vm.isAddToGrid = 'N';
        };

        vm.removeRow = function (dataItem) {
            $scope.$parent.reasonGridDataSource.remove(dataItem);
        };

        vm.updateRow = function (data) {
            var fabOrderCopy = angular.copy(data);

            var dataItem = $scope.$parent.reasonGridDataSource.getByUid(data.uid);

            console.log(dataItem);

            dataItem.set('RF_SFAB_RSN_TYPE_ID', vm.srtReason.RF_SFAB_RSN_TYPE_ID);
            dataItem.set('REASON_DESC', vm.srtReason.REASON_DESC);            

            dataItem.set('editMode', false);

            vm.cancelToGrid();
        }                      

        vm.cancelToGrid = function () {
            var data = angular.copy(vm.srtReason);
            vm.srtReason = { KNT_SRT_FAB_REQ_D2_ID: -1, KNT_SRT_FAB_REQ_H_ID: $scope.$parent.form.KNT_SRT_FAB_REQ_H_ID, RF_SFAB_RSN_TYPE_ID: data.RF_SFAB_RSN_TYPE_ID, REASON_DESC: '' };

            vm.isAddToGrid = 'Y';
        }        

        vm.saveReason = function () {

            var submitData = angular.copy(vm.srtReason);

            
            Dialog.confirm('Do you want to save?', 'Are you sure?', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/SaveFabProdOrdD2').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                vm.cancel();
                                vm.reasonGridDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };


    }
})();
//=============== End for KntShortProdOrdReasonController =================










//=============== Start for KntShortProdOrdResposibilityController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntShortProdOrdResposibilityController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', KntShortProdOrdResposibilityController]);
    function KntShortProdOrdResposibilityController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        
        vm.srtFabDtlState = true;

        vm.srtResp = { KNT_SRT_FAB_REQ_D3_ID: -1, KNT_SRT_FAB_REQ_H_ID: $scope.$parent.form.KNT_SRT_FAB_REQ_H_ID };


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getRespDeptList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getRespDeptList() {
            vm.respDeptOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "RESP_DEPT_NAME",
                dataValueField: "RF_RESP_DEPT_ID",
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    vm.srtResp.RESP_DEPT_NAME = item.RESP_DEPT_NAME;
                //},
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.srtResp.RESP_DEPT_NAME = item.RESP_DEPT_NAME;

                    if (item.RF_RESP_DEPT_ID < 0) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'NewSFabBkRespDeptEntryModal.html',
                            controller: 'SFabBkRespDeptModalController',
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal'//,
                            //resolve: {
                            //    ColourList: function (KnittingDataService) {
                            //        return KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll');
                            //    }
                            //}
                        });

                        modalInstance.result.then(function (result) {
                            console.log(result);

                            if (result.RF_RESP_DEPT_ID && result.RF_RESP_DEPT_ID > 0) {
                                vm.respDeptDataSource.read().then(function () {
                                    vm.srtResp['RF_RESP_DEPT_ID'] = result.RF_RESP_DEPT_ID;
                                    vm.srtResp['RESP_DEPT_NAME'] = result.RESP_DEPT_NAME;
                                });
                            }
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });
                    }

                }
            };

            return vm.respDeptDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/getRespDeptList').then(function (res) {
                            e.success([{ 'RESP_DEPT_NAME': '---New Responsable Dept---', RF_RESP_DEPT_ID: -1 }].concat(res));

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.onChangePctDist = function () {
            //var totBkPctExists = 100 - _.sumBy($scope.respGridDataSource.data(), function (o) { return o.PCT_DIST_QTY; });
            var totBkQty = _.sumBy($scope.fabOrderGridDataSource.data(), function (o) { return o.RQD_FAB_QTY; }); //$scope.fabOrderGridDataSource.data();
            console.log(totBkQty);
            //_.sumBy($scope.fabOrderGridDataSource.data(), function (o) { return o.RQD_FAB_QTY; });
            vm.srtResp.DIST_QTY = (totBkQty * vm.srtResp.PCT_DIST_QTY)/100;
        }


        vm.addRow = function (data) {
            var fabOrderCopy = angular.copy(data);

            $scope.$parent.respGridDataSource.insert(0, data);

            vm.cancelToGrid();
        }
        
        vm.updateIndex = 0;
        vm.editRow = function (dataItem) {
            var index = $scope.$parent.respGridDataSource.indexOf(dataItem);
            //alert('index:' + index);

            console.log(dataItem);
            var data = angular.copy(dataItem);
            vm.srtResp = data;

            vm.isAddToGrid = 'N';
        };

        vm.removeRow = function (dataItem) {
            $scope.$parent.respGridDataSource.remove(dataItem);
        };

        vm.updateRow = function (data) {
            var fabOrderCopy = angular.copy(data);

            var dataItem = $scope.$parent.respGridDataSource.getByUid(data.uid);

            console.log(dataItem);

            dataItem.set('RF_RESP_DEPT_ID', vm.srtResp.RF_RESP_DEPT_ID);
            dataItem.set('DIST_QTY', vm.srtResp.DIST_QTY);
            dataItem.set('PCT_DIST_QTY', vm.srtResp.PCT_DIST_QTY);

            dataItem.set('editMode', false);

            vm.cancelToGrid();
        }


        vm.cancelToGrid = function () {
            var data = angular.copy(vm.srtResp);
            vm.srtResp = {
                KNT_SRT_FAB_REQ_D3_ID: -1, KNT_SRT_FAB_REQ_H_ID: $scope.$parent.form.KNT_SRT_FAB_REQ_H_ID, RF_RESP_DEPT_ID: data.RF_RESP_DEPT_ID, RESP_DEPT_NAME: data.RESP_DEPT_NAME,
                DIST_QTY: null, PCT_DIST_QTY: null
            };

            vm.isAddToGrid = 'Y';
        }

        

        //vm.editRow = function (dataItem) {
        //    vm.srtResp = angular.copy(dataItem);
        //}

        //vm.removeRow = function (dataItem) {
        //    $scope.$parent.reasonGridDataSource.remove(dataItem);
        //};

        //vm.cancel = function () {
        //    var data = angular.copy(vm.srtResp);
        //    vm.srtResp = { KNT_SRT_FAB_REQ_D3_ID: -1, KNT_SRT_FAB_REQ_H_ID: $scope.$parent.form.KNT_SRT_FAB_REQ_H_ID, RF_RESP_DEPT_ID: data.RF_RESP_DEPT_ID, DIST_QTY: null, PCT_DIST_QTY: null };
        //}

        
        vm.saveResp = function () {

            var submitData = angular.copy(vm.srtResp);
            
            Dialog.confirm('Do you want to save?', 'Are you sure?', ['Yes', 'No']).then(function () {

                    console.log(submitData);

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/SaveFabProdOrdD3').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                vm.cancel();
                                vm.respGridDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };


    }
})();
//=============== End for KntShortProdOrdResposibilityController =================












//=============== Start for KntShortProdOrdListController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntShortProdOrdListController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', 'userLavelData', KntShortProdOrdListController]);
    function KntShortProdOrdListController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService, userLavelData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        console.log(userLavelData);
        if (userLavelData['USER_DRAFT_NAME'] == 'DRAFTER') {
            vm.isVisableAddBtn = 'Y';
        }
        else {
            vm.isVisableAddBtn = 'N';
        }

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';
        vm.form = {};

        vm.srtFabBookingGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_SRT_FAB_REQ_H_ID",
                    fields: {
                        SFAB_REQ_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/FabProdKnitOrder/GetSrtFabBookingList?pMC_BYR_ACC_GRP_ID=' + (vm.form.MC_BYR_ACC_GRP_ID || $stateParams.pMC_BYR_ACC_GRP_ID) + '&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || $stateParams.pMC_STYLE_H_EXT_ID);
                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;

                    console.log(url);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

        

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerAcGrpList(), getByrAccWiseStyleExtList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

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

                    $stateParams.pMC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    $stateParams.pMC_STYLE_H_EXT_ID = null;

                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_STYLE_H_EXT_ID = null;
                    vm.form.MC_STYLE_H_ID = 0;

                    vm.styleExtDataSource.read();                    
                },
                dataBound: function (e) {
                    if ($stateParams.pMC_BYR_ACC_GRP_ID != null && $stateParams.pMC_BYR_ACC_GRP_ID > 0) {
                        vm.form.MC_BYR_ACC_GRP_ID = $stateParams.pMC_BYR_ACC_GRP_ID;

                        vm.styleExtDataSource.read();                        
                    }
                },
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

                    //vm.form.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;
                    
                },
                dataBound: function (e) {
                    if ($stateParams.pMC_STYLE_H_EXT_ID != null && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                        vm.form.MC_STYLE_H_EXT_ID = $stateParams.pMC_STYLE_H_EXT_ID;                        
                    }
                },
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pRF_FAB_PROD_CAT_ID=6&pMC_BYR_ACC_GRP_ID=' + vm.form.MC_BYR_ACC_GRP_ID;
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

                

        vm.srtFabBookingGridOption = {
            height: 420,
            sortable: true,
            //scrollable: {
            //    virtual: true               
            //},
            pageable: true,
            columns: [
                { field: "SFAB_REQ_DT", title: "Req Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "SFAB_REQ_NO", title: "Req#", width: "15%" },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer A/C", width: "15%" },
                { field: "STYLE_NO", title: "Style#", width: "15%" },
                { field: "MC_ORDER_NO_LST", title: "Order#", width: "20%" },
                {
                    title: "Status",
                    field: "EVENT_NAME",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID<=59'>{{dataItem.ACTN_STATUS_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==60'>{{dataItem.ACTN_STATUS_NAME}}</label>";
                    },
                    width: "15%"
                },
                { field: "USER_VERIFIER_NAME", title: "Verifier", hidden: true },
                { field: "USER_APPROVER_NAME", title: "Approver", hidden: true },
                {
                    title: "Action",
                    template: function () {
                        return "<a type='button' class='btn btn-xs blue' ui-sref='KntShortFabProdOrdH.SrtFabDtl({pKNT_SRT_FAB_REQ_H_ID : dataItem.KNT_SRT_FAB_REQ_H_ID, pMC_BYR_ACC_GRP_ID: dataItem.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: dataItem.MC_STYLE_H_EXT_ID})' ng-if='dataItem.RF_ACTN_STATUS_ID==58'><i class='fa fa-edit'></i></a>&nbsp;" +
                            "<a type='button' class='btn btn-xs blue' ui-sref='KntShortFabProdOrdH.SrtFabDtl({pKNT_SRT_FAB_REQ_H_ID : dataItem.KNT_SRT_FAB_REQ_H_ID, pMC_BYR_ACC_GRP_ID: dataItem.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: dataItem.MC_STYLE_H_EXT_ID})' ng-if='dataItem.RF_ACTN_STATUS_ID>58'>View</a>&nbsp;" +
                            "<a type='button' class='btn btn-xs blue' ng-click='vm.submitBooking(dataItem)' ng-if='dataItem.RF_ACTN_STATUS_ID==58'>Submit</a>&nbsp;"; //+
                            //"<a type='button' class='btn btn-xs blue' ng-click='vm.verifyRequsition(dataItem)' ng-if='(dataItem.RF_ACTN_STATUS_ID==45 && dataItem.USER_VERIFIER_NAME==\"VERIFIER\")'>Verify</a>&nbsp;" +
                            //"<a type='button' class='btn btn-xs blue' ng-click='vm.approveBooking(dataItem)' ng-if='(dataItem.RF_ACTN_STATUS_ID==59 && dataItem.USER_APPROVER_NAME==\"APPROVER\")'>Approve</a>&nbsp;";
                    },
                    width: "10%"
                }
            ]
        };

        
        vm.getSrtBookingList = function () {
            $state.go('KntShortFabProdOrdList', { pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });

            vm.srtFabBookingGridDataSource.read();
        };

        vm.submitBooking = function (dataItem) {

            var submitData = angular.copy(dataItem);
            submitData['SFAB_REQ_ATTN'] = submitData.SFAB_REQ_ATTN_LIST == null ? '' : submitData.SFAB_REQ_ATTN_LIST.join(',');

            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/SubmitSrtFabProdOrdH').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                if (res.data.PRF_ACTN_STATUS_ID_RTN != 0) {
                                    vm.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;

                                    //$state.go('KntShortFabProdOrdH.SrtFabDtl', { pKNT_SRT_FAB_REQ_H_ID: res.data.PKNT_SRT_FAB_REQ_H_ID_RTN, pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });

                                    vm.srtFabBookingGridDataSource.read();
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.approveBooking = function (dataItem) {

            var submitData = angular.copy(dataItem);
            submitData['SFAB_REQ_ATTN'] = submitData.SFAB_REQ_ATTN_LIST == null ? '' : submitData.SFAB_REQ_ATTN_LIST.join(',');

            Dialog.confirm('Do you want approve and finalize?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/FabProdKnitOrder/ApproveSrtFabProdOrdH').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                if (res.data.PRF_ACTN_STATUS_ID_RTN != 0) {
                                    vm.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;

                                    //$state.go('KntShortFabProdOrdH.SrtFabDtl', { pKNT_SRT_FAB_REQ_H_ID: res.data.PKNT_SRT_FAB_REQ_H_ID_RTN, pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });

                                    vm.srtFabBookingGridDataSource.read();
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };


    }
})();
//=============== End for KntShortProdOrdListController =================












//=============== Start for SFabBkReasonModalController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('SFabBkReasonModalController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modalInstance', SFabBkReasonModalController]);
    function SFabBkReasonModalController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService, $modalInstance) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        $scope.RF_SFAB_RSN_TYPE_ID = 0;
        $scope.SFAB_RSN_TYPE_NAME = '';
        $scope.IS_ACTIVE = 'Y';

        
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.cancel = function () {
            //$modalInstance.dismiss('cancel');
            var data = { RF_SFAB_RSN_TYPE_ID: $scope.RF_SFAB_RSN_TYPE_ID, SFAB_RSN_TYPE_NAME: $scope.SFAB_RSN_TYPE_NAME, IS_ACTIVE: $scope.IS_ACTIVE };
            $modalInstance.close(data);
        };

        vm.tranCancel = function () {
            $scope.RF_SFAB_RSN_TYPE_ID = 0;
            $scope.SFAB_RSN_TYPE_NAME = '';
            $scope.IS_ACTIVE = 'Y';
        }

        vm.editRow = function (dataItem) {
            var data = angular.copy(dataItem);

            $scope.RF_SFAB_RSN_TYPE_ID = dataItem.RF_SFAB_RSN_TYPE_ID;
            $scope.SFAB_RSN_TYPE_NAME = dataItem.SFAB_RSN_TYPE_NAME;
            $scope.IS_ACTIVE = dataItem.IS_ACTIVE;
        }

        vm.reasonTypeGridOption = {
            height: 300,
            sortable: true,
            columns: [                
                { field: "SFAB_RSN_TYPE_NAME", title: "Type", width: "15%" },
                { field: "IS_ACTIVE", title: "Active?", width: "15%" },
                {
                    title: "Action",
                    template: function () {
                        return "<a type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' ><i class='fa fa-edit'></i></a>"                           
                    },
                    width: "10%"
                }
            ]
        };

        vm.reasonTypeGridDataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverFiltering: true,
            schema: {
                //data: "data",
                //total: "total",
                model: {
                    id: "RF_SFAB_RSN_TYPE_ID"
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/common/getSrtFabBkReasonTyp';
                    //url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;

                    console.log(url);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });


        $scope.save = function (token, valid) {

            //if (!valid) return;

            var obj = { RF_SFAB_RSN_TYPE_ID: $scope.RF_SFAB_RSN_TYPE_ID, SFAB_RSN_TYPE_NAME: $scope.SFAB_RSN_TYPE_NAME, IS_ACTIVE: $scope.IS_ACTIVE };
            

            //data.fabReqDtl = vm.obGrid;
            //vm.copyOrder = data;
            console.log(obj);
            return KnittingDataService.saveDataByFullUrl(obj, '/api/common/SaveSrtFabBkReasonTyp').then(function (res) {
                console.log(res);

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $scope.RF_SFAB_RSN_TYPE_ID = parseInt(res.data.PRF_SFAB_RSN_TYPE_ID_RTN);
                        //$modalInstance.close(res.data);
                        //$scope.cancel();
                        vm.reasonTypeGridDataSource.read();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });


            

        };


    }
})();
//=============== End for SFabBkReasonModalController =================












//=============== Start for SFabBkRespModalController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('SFabBkRespDeptModalController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modalInstance', SFabBkRespDeptModalController]);
    function SFabBkRespDeptModalController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService, $modalInstance) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        $scope.RF_RESP_DEPT_ID = 0;
        $scope.RESP_DEPT_NAME = '';
        $scope.IS_ACTIVE = 'Y';


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.cancel = function () {
            //$modalInstance.dismiss('cancel');
            var data = { RF_RESP_DEPT_ID: $scope.RF_RESP_DEPT_ID, RESP_DEPT_NAME: $scope.RESP_DEPT_NAME, IS_ACTIVE: $scope.IS_ACTIVE };
            $modalInstance.close(data);
        };

        vm.tranCancel = function () {
            $scope.RF_RESP_DEPT_ID = 0;
            $scope.RESP_DEPT_NAME = '';
            $scope.IS_ACTIVE = 'Y';
        }

        vm.editRow = function (dataItem) {
            var data = angular.copy(dataItem);

            $scope.RF_RESP_DEPT_ID = dataItem.RF_RESP_DEPT_ID;
            $scope.RESP_DEPT_NAME = dataItem.RESP_DEPT_NAME;
            $scope.IS_ACTIVE = dataItem.IS_ACTIVE;
        }

        vm.respDeptGridOption = {
            height: 300,
            sortable: true,
            columns: [
                { field: "RESP_DEPT_NAME", title: "Department Name", width: "20%" },
                { field: "IS_ACTIVE", title: "Active?", width: "15%" },
                {
                    title: "Action",
                    template: function () {
                        return "<a type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' ><i class='fa fa-edit'></i></a>"
                    },
                    width: "10%"
                }
            ]
        };

        vm.respDeptGridDataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverFiltering: true,
            schema: {
                //data: "data",
                //total: "total",
                model: {
                    id: "RF_RESP_DEPT_ID"
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/common/getRespDeptList';
                    //url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;

                    console.log(url);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });


        $scope.save = function (token, valid) {

            //if (!valid) return;

            var obj = { RF_RESP_DEPT_ID: $scope.RF_RESP_DEPT_ID, RESP_DEPT_NAME: $scope.RESP_DEPT_NAME, IS_ACTIVE: $scope.IS_ACTIVE };


            //data.fabReqDtl = vm.obGrid;
            //vm.copyOrder = data;
            console.log(obj);
            return KnittingDataService.saveDataByFullUrl(obj, '/api/common/SaveSrtFabBkRespDept').then(function (res) {
                console.log(res);

                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $scope.RF_RESP_DEPT_ID = parseInt(res.data.PRF_RESP_DEPT_ID_RTN);
                        //$modalInstance.close(res.data);
                        //$scope.cancel();
                        vm.respDeptGridDataSource.read();
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });




        };


    }
})();
//=============== End for SFabBkRespModalController =================











//=============== Start for KntSrtProdOrdAprovListController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSrtProdOrdAprovListController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', KntSrtProdOrdAprovListController]);
    function KntSrtProdOrdAprovListController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, $filter, commonDataService) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

       

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';
        vm.form = {};

        vm.srtFabBookingGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_SRT_FAB_REQ_H_ID",
                    fields: {
                        SFAB_REQ_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/FabProdKnitOrder/GetSrtFabBookingAprovList?pMC_BYR_ACC_GRP_ID=' + (vm.form.MC_BYR_ACC_GRP_ID || $stateParams.pMC_BYR_ACC_GRP_ID) + '&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || $stateParams.pMC_STYLE_H_EXT_ID);
                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    console.log(url);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });



        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerAcGrpList(), getByrAccWiseStyleExtList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

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

                    $stateParams.pMC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    $stateParams.pMC_STYLE_H_EXT_ID = null;

                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_STYLE_H_EXT_ID = null;
                    vm.form.MC_STYLE_H_ID = 0;

                    vm.styleExtDataSource.read();
                },
                dataBound: function (e) {
                    if ($stateParams.pMC_BYR_ACC_GRP_ID != null && $stateParams.pMC_BYR_ACC_GRP_ID > 0) {
                        vm.form.MC_BYR_ACC_GRP_ID = $stateParams.pMC_BYR_ACC_GRP_ID;

                        vm.styleExtDataSource.read();
                    }
                },
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

                    //vm.form.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;

                },
                dataBound: function (e) {
                    if ($stateParams.pMC_STYLE_H_EXT_ID != null && $stateParams.pMC_STYLE_H_EXT_ID > 0) {
                        vm.form.MC_STYLE_H_EXT_ID = $stateParams.pMC_STYLE_H_EXT_ID;
                    }
                },
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pRF_FAB_PROD_CAT_ID=6&pMC_BYR_ACC_GRP_ID=' + vm.form.MC_BYR_ACC_GRP_ID;
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.getSrtBookingList = function () {
            vm.srtFabBookingGridDataSource.read();
        }

        vm.srtFabBookingGridOption = {
            height: 420,
            sortable: true,
            //scrollable: {
            //    virtual: true               
            //},
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
                { field: "SFAB_REQ_DT", title: "Req Date", format: "{0:dd-MMM-yyyy}", width: "10%", filterable: false },
                { field: "SFAB_REQ_NO", title: "Req#", width: "15%", filterable: true },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer A/C", width: "15%", filterable: false },
                { field: "STYLE_NO", title: "Style#", width: "15%", filterable: false },
                { field: "MC_ORDER_NO_LST", title: "Order#", width: "20%", filterable: false },
                {
                    title: "Status",
                    field: "EVENT_NAME",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID<=59'>{{dataItem.ACTN_STATUS_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==60'>{{dataItem.ACTN_STATUS_NAME}}</label>";
                    },
                    width: "15%",
                    filterable: false
                },
                { field: "USER_VERIFIER_NAME", title: "Verifier", hidden: true },
                { field: "USER_APPROVER_NAME", title: "Approver", hidden: true },
                { field: "USER_CORR_AFTER_APPROVE", title: "Corr after Approve", hidden: true },
                {
                    title: "Action",
                    template: function () {
                        return "<a type='button' class='btn btn-xs blue' href='/Knitting/Knit/ShortFabProdOrderRpt?a=345/#/shortFabProdOrdRpt?pKNT_SRT_FAB_REQ_H_ID={{dataItem.KNT_SRT_FAB_REQ_H_ID}}' target='_blank'><i class='fa fa-print'></i> Print</a>&nbsp; <a type='button' class='btn btn-xs blue' href='/Knitting/Knit/ShortFabProdOrder?a=213/#/shortFabProdOrd/{{dataItem.KNT_SRT_FAB_REQ_H_ID}}/srtFabDtl?pMC_BYR_ACC_GRP_ID={{dataItem.MC_BYR_ACC_GRP_ID}}&pMC_STYLE_H_EXT_ID={{dataItem.MC_STYLE_H_EXT_ID}}' ng-show='dataItem.USER_CORR_AFTER_APPROVE==\"CORR_AFTER_APPROVE\"' >View</a>";
                    },
                    width: "10%"
                }
            ]
        };

      

    }
})();
//=============== End for KntSrtProdOrdAprovListController =================
