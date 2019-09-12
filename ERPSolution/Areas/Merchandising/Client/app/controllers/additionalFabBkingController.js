//=============== Start for AdditionalFabBkingHController =================
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('AdditionalFabBkingHController', ['$http', '$q', 'config', 'Dialog', 'MrcDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', 'addFabBkingHdrData', 'userLavelData', '$timeout', AdditionalFabBkingHController]);
    function AdditionalFabBkingHController($http, $q, config, Dialog, MrcDataService, $stateParams, $state, $scope, $filter, commonDataService, $modal, addFabBkingHdrData, userLavelData, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';
      
        console.log(userLavelData);
        $scope.userLavelData = angular.copy(userLavelData);

        var key = 'MC_BLK_ADFB_REQ_H_ID';

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        vm.form = addFabBkingHdrData[key] ? addFabBkingHdrData : {
            MC_BLK_ADFB_REQ_H_ID: -1, RF_FAB_PROD_CAT_ID: 9, MC_FAB_PROD_ORD_H_ID: 0, MC_BLK_FAB_REQ_H_ID: 0, MC_STYLE_H_ID: 0, MC_STYLE_H_EXT_ID: 0, AFAB_REQ_DT: $filter('date')(vm.today, vm.dtFormat)
        };
        $scope.form = addFabBkingHdrData[key] ? addFabBkingHdrData : {
            MC_BLK_ADFB_REQ_H_ID: -1, RF_FAB_PROD_CAT_ID: 9, MC_FAB_PROD_ORD_H_ID: 0, MC_BLK_FAB_REQ_H_ID: 0, MC_STYLE_H_ID: 0, MC_STYLE_H_EXT_ID: 0, AFAB_REQ_DT: $filter('date')(vm.today, vm.dtFormat)
        };
        

        $scope.fabOrder = {
            MC_BLK_ADFB_REQ_D_ID: -1, MC_BLK_ADFB_REQ_H_ID: -1, FAB_COLOR_ID: '', IS_CONTRAST: 'N', GMT_COLOR_ID: '',
            DIA_MOU_CODE: 'Inch', FIN_DIA_TYPE: 'Open', IS_FIN_DIA_REQ: 'Y',
            QTY_MOU_ID: 3, QTY_MOU_CODE: 'Kg', collarCuffReqList: []
        };

       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerAcList(), getByrAccWiseStyleExtList(), getFabricByStyleId(), getFinDiaByProdOrdId(), getColorList()];
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

        vm.AFAB_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.AFAB_REQ_DT_LNopened = true;
        }
        vm.LAST_REV_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.LAST_REV_DT_LNopened = true;
        }
        
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
                    vm.form.MC_STYLE_H_EXT_ID = null;

                    $stateParams.pMC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    $stateParams.pMC_STYLE_H_EXT_ID = '';

                    vm.styleExtDataSource.read();
                },
                dataBound: function (e) {
                    if ($stateParams.pMC_BYR_ACC_ID != null && $stateParams.pMC_BYR_ACC_ID > 0) {
                        vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                        vm.form.MC_STYLE_H_EXT_ID = addFabBkingHdrData['MC_STYLE_H_EXT_ID'];
                        
                        vm.styleExtDataSource.read();
                        $scope.styleFabricDataSource.read();
                        $scope.finDiaDataSource.read();                        
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }
        

        $scope.fabOrderGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return MrcDataService.getDataByFullUrl('/api/mrc/AddFabBk/GetAddFabBkingDtl?pMC_BLK_ADFB_REQ_H_ID=' + ($stateParams.pMC_BLK_ADFB_REQ_H_ID || $scope.form.MC_BLK_ADFB_REQ_H_ID || 0)).then(function (res) {

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
                    id: "MC_BLK_ADFB_REQ_D_ID",
                    fields: {
                        //SC_START_DT: { type: "date", editable: false },
                        //SC_END_DT: { type: "date", editable: false }
                    }
                }
            }
        });



        function getUserList() {
            
            return vm.userDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/SelectAllUserData';

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
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
                            MrcDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
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
                        

        function getByrAccWiseStyleExtList() {
            vm.styleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_BLK_FAB_REQ_H_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    
                    $stateParams.pMC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;
                    
                    vm.form.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;
                    
                    $scope.colorListDataSource.read();
                    $scope.styleFabricDataSource.read();
                    $scope.finDiaDataSource.read();
                },
                dataBound: function (e) {

                    if ($stateParams.pMC_BLK_FAB_REQ_H_ID != null && $stateParams.pMC_BLK_FAB_REQ_H_ID > 0) {
                        vm.form.MC_BLK_FAB_REQ_H_ID = $stateParams.pMC_BLK_FAB_REQ_H_ID;
                        
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
                        var url = '/api/mrc/Order/GetOrderList?pRF_FAB_PROD_CAT_ID=2&pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0) + '&pMC_STYLE_H_EXT_ID=' + ((vm.form.MC_STYLE_H_EXT_ID > 0) ? vm.form.MC_STYLE_H_EXT_ID : null);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);
                        console.log(url);

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {                            
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
                        MrcDataService.getDataByFullUrl('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + ((vm.form.MC_STYLE_H_ID > 0) ? vm.form.MC_STYLE_H_ID : 0)).then(function (res) {
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
                        MrcDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetFinDiaByProdOrdId?&pRF_FAB_PROD_CAT_ID=2&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || $stateParams.pMC_STYLE_H_EXT_ID || 0)).then(function (res) {
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
                        MrcDataService.getDataByFullUrl('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + (vm.form.MC_STYLE_H_ID || 0)).then(function (res) {
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
                        MrcDataService.getDataByFullUrl('/api/Common/FabricTypeList?pINV_ITEM_CAT_ID=34').then(function (res) {
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

                                    return MrcDataService.saveDataByFullUrl(data, '/api/mrc/StyleDItemFab/SaveFiberComposition', token).then(function (res) {
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
                                    return MrcDataService.LookupListData(76);
                                },
                                FiberCompGroup: function () {
                                    return MrcDataService.getDataByFullUrl('/api/mrc/YarnSpec/FiberCompGroup');
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
                        MrcDataService.getDataByFullUrl('/api/Common/FibreCompList').then(function (res) {
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

            return MrcDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                
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
                        MrcDataService.getDataByFullUrl('/api/common/LookupListData/67').then(function (res) {
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
                        MrcDataService.getDataByFullUrl('/api/Knit/KnitCommon/getMachineDiaList').then(function (res) {
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
                        MrcDataService.getDataByUrlNoToken('/api/common/LookupListData/59').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };
                               
        $scope.fabOrderGridOption = {
            height: 300,
            sortable: true,
            columns: [
                { field: "COLOR_NAME_EN", title: "Fab Color", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" } },
                { field: "GMT_COLOR_NAME_EN", title: "GMT Color", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" } },
                { field: "SP_INSTRUCTION", title: "Spec", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" } },
                { field: "FABRIC_DESC", title: "Fabric", headerAttributes: { "class": "col-md-3" }, attributes: { "class": "col-md-3" } },
                { field: "FIN_DIA_N_DIA_TYPE", title: "Fin.Dia", headerAttributes: { "class": "col-md-2" }, attributes: { "class": "col-md-2" } },                
                { field: "RQD_FFAB_QTY", title: "F.Fab(Kg)", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" } },
                { field: "PCT_LOSS_QTY", title: "PL(%)", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" } },
                { field: "RQD_GFAB_QTY", title: "G.Fab(Kg)", headerAttributes: { "class": "col-md-1" }, attributes: { "class": "col-md-1" } },
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
                return MrcDataService.getDataByFullUrl('/api/mrc/AddFabBk/GetAddFabBkingCollarCuffReq?pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || 0) + '&pFAB_COLOR_ID=' + (dtlDataItem.FAB_COLOR_ID || 0) + '&pMC_BLK_ADFB_REQ_D_ID=' + (dtlDataItem.MC_BLK_ADFB_REQ_D_ID || 0)).then(function (res) {

                    dtlDataItem['collarCuffReqList'] = res;

                }, function (err) {
                    console.log(err);
                });
            }

        };

        
        function getDetailData() {
            $scope.fabOrderGridDataSource.read();           
        }
                
    }
})();
//=============== End for AdditionalFabBkingHController =================








//=============== Start for AdditionalFabBkingDController =================
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('AdditionalFabBkingDController', ['$q', 'config', 'Dialog', 'MrcDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', AdditionalFabBkingDController]);
    function AdditionalFabBkingDController($q, config, Dialog, MrcDataService, $stateParams, $state, $scope, $filter, commonDataService, $modal) {

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
                        MrcDataService.getDataByFullUrl('/api/common/LookupListData/67').then(function (res) {
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
            $scope.$parent.fabOrder.RQD_FFAB_QTY_NAME = $scope.$parent.fabOrder.RQD_FFAB_QTY + ' ' + $scope.$parent.fabOrder.QTY_MOU_CODE;

            $scope.$parent.fabOrder.RQD_GFAB_QTY = Math.round($scope.$parent.fabOrder.RQD_FFAB_QTY + (($scope.$parent.fabOrder.RQD_FFAB_QTY * $scope.$parent.fabOrder.PCT_LOSS_QTY) / 100));
        }

        vm.addRow = function (data) {

            //var gmtColorList = _.filter($scope.$parent.colorListDataSource.data(), function (ob) {
            //    return _.some(data['GMT_COLOR_ID_LST'], function (val) {
            //        return ob.MC_COLOR_ID == val;
            //    });
            //});

            //data['GMT_COLOR_NAME_EN_LST'] = _.map(gmtColorList, 'COLOR_NAME_EN').join('+');

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
            if (dataItem.RF_FAB_TYPE_ID == 15 || dataItem.RF_FAB_TYPE_ID == 20 || dataItem.RF_FAB_TYPE_ID == 21 || dataItem.RF_FAB_TYPE_ID == 51) {
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
            dataItem.set('SP_INSTRUCTION', fabOrderCopy.SP_INSTRUCTION);
            dataItem.set('FIN_DIA', fabOrderCopy.FIN_DIA);
            dataItem.set('DIA_MOU_ID', fabOrderCopy.DIA_MOU_ID);
            dataItem.set('LK_DIA_TYPE_ID', fabOrderCopy.LK_DIA_TYPE_ID);
            dataItem.set('FIN_GSM', fabOrderCopy.FIN_GSM);
            dataItem.set('RQD_FFAB_QTY', fabOrderCopy.RQD_FFAB_QTY);
            dataItem.set('PCT_LOSS_QTY', fabOrderCopy.PCT_LOSS_QTY);
            dataItem.set('RQD_GFAB_QTY', fabOrderCopy.RQD_GFAB_QTY);
            
            dataItem.set('QTY_MOU_ID', fabOrderCopy.QTY_MOU_ID);
            dataItem.set('RQD_FFAB_QTY_NAME', fabOrderCopy.RQD_FFAB_QTY_NAME);
            dataItem.set('KNT_MC_DIA_ID', fabOrderCopy.KNT_MC_DIA_ID);
            dataItem.set('LK_MC_GG_ID', fabOrderCopy.LK_MC_GG_ID);

            dataItem.set('FIN_DIA_N_DIA_TYPE', fabOrderCopy.FIN_DIA_N_DIA_TYPE);
            dataItem.set('MC_DIA_N_GG', fabOrderCopy.MC_DIA_N_GG);

            $scope.$parent.fabOrder.MC_FAB_PROD_ORD_D_ID = -1;
            $scope.$parent.fabOrder.SP_INSTRUCTION = '';
            $scope.$parent.fabOrder.RQD_FFAB_QTY = '';
            $scope.$parent.fabOrder.PCT_LOSS_QTY = '';
            $scope.$parent.fabOrder.RQD_GFAB_QTY = '';

            dataItem.set('editMode', false);

            vm.isAddToGrid = 'Y';
        }

        vm.cancelToGrid = function () {
            //$scope.$parent.fabOrder.IS_CONTRAST = 'N';
            //$scope.$parent.fabOrder.GMT_COLOR_ID_LST = $scope.$parent.fabOrder.FAB_COLOR_ID.split(',');
            $scope.$parent.fabOrder.PCT_LOSS_QTY = '';
            $scope.$parent.fabOrder.RQD_FFAB_QTY = '';

            $scope.$parent.fabOrder.MC_BLK_ADFB_REQ_H_ID = 0;
            $scope.$parent.fabOrder.MC_BLK_ADFB_REQ_D_ID = 0;
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

        



        vm.save = function () {

            var submitData = angular.copy($scope.form);
            
            var fabOrdGridData = $scope.$parent.fabOrderGridDataSource.data();
            
            var reqDList = [];
            var reqD1List = [];

            var fabIdx = 0;
            var clcfIdx = 0;

            angular.forEach(fabOrdGridData, function (val, key) {
                fabIdx = fabIdx + 1;

                //val['GMT_COLOR_LST'] = val.GMT_COLOR_ID_LST == null ? '' : val.GMT_COLOR_ID_LST.join(',');

                reqDList.push({
                    ADFB_REQ_D_INDX: fabIdx, MC_BLK_ADFB_REQ_D_ID: val.MC_BLK_ADFB_REQ_D_ID, MC_BLK_ADFB_REQ_H_ID: val.MC_BLK_ADFB_REQ_H_ID, FAB_COLOR_ID: val.FAB_COLOR_ID,
                    MC_STYLE_D_FAB_ID: val.MC_STYLE_D_FAB_ID, RF_FAB_TYPE_ID: val.RF_FAB_TYPE_ID, RF_FIB_COMP_ID: val.RF_FIB_COMP_ID, FIN_DIA: val.FIN_DIA,
                    DIA_MOU_ID: val.DIA_MOU_ID, LK_DIA_TYPE_ID: val.LK_DIA_TYPE_ID, FIN_GSM: val.FIN_GSM, RQD_FFAB_QTY: val.RQD_FFAB_QTY, PCT_LOSS_QTY: val.PCT_LOSS_QTY,
                    QTY_MOU_ID: val.QTY_MOU_ID, SP_INSTRUCTION: val.SP_INSTRUCTION, IS_CONTRAST: val.IS_CONTRAST, GMT_COLOR_ID: val.GMT_COLOR_ID
                });

                angular.forEach(val['collarCuffReqList'], function (val1, key1) {
                    reqD1List.push({
                        ADFB_REQ_D_INDX: fabIdx, MC_BLK_ADFB_REQ_D1_ID: val1['MC_BLK_ADFB_REQ_D1_ID'], MC_BLK_ADFB_REQ_D_ID: val['MC_BLK_ADFB_REQ_D_ID'],
                        MC_CLCF_ORD_REQ_ID: val1['MC_CLCF_ORD_REQ_ID'], RQD_PC_QTY: val1['RQD_PC_QTY']
                    });
                });

            });



            submitData.BLK_ADFB_DTL_XML = MrcDataService.xmlStringShort(reqDList.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            submitData.BLK_ADFB_CLCF_DTL_XML = MrcDataService.xmlStringShort(reqD1List.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            
            console.log(submitData);
            //return;

            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    //console.log(submitData);

                    return MrcDataService.saveDataByFullUrl(submitData, '/api/mrc/AddFabBk/BatchSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                if (res.data.PMC_BLK_ADFB_REQ_H_ID_RTN != 0) {
                                    $stateParams.pMC_BLK_ADFB_REQ_H_ID = res.data.PMC_BLK_ADFB_REQ_H_ID_RTN;

                                    $scope.$parent.form.MC_BLK_ADFB_REQ_H_ID = res.data.PMC_BLK_ADFB_REQ_H_ID_RTN;
                                    $scope.$parent.form.AFAB_REQ_NO = res.data.PAFAB_REQ_NO_RTN;
                                    $scope.$parent.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;

                                    if (res.data.PLAST_REV_NO_RTN > 0) {
                                        //alert(res.data.PLAST_REV_NO_RTN);
                                        $scope.$parent.form.LAST_REV_NO = res.data.PLAST_REV_NO_RTN;
                                        $scope.$parent.form.LAST_REV_DT = res.data.PLAST_REV_DT_RTN;
                                    }
                                    
                                    $state.go('AddFabBkingH.Dtl', { pMC_BLK_ADFB_REQ_H_ID: res.data.PMC_BLK_ADFB_REQ_H_ID_RTN }, { notify: false });
                                    getFabricDtlData(res.data.PMC_BLK_ADFB_REQ_H_ID_RTN);
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };


        function getFabricDtlData(pMC_BLK_ADFB_REQ_H_ID) {

            
            $scope.$parent.fabOrderGridDataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        //e.success([]);
                        return MrcDataService.getDataByFullUrl('/api/mrc/AddFabBk/GetAddFabBkingDtl?pMC_BLK_ADFB_REQ_H_ID=' + (pMC_BLK_ADFB_REQ_H_ID || 0)).then(function (res) {

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
                        id: "MC_BLK_ADFB_REQ_D_ID",
                        fields: {
                            //SC_START_DT: { type: "date", editable: false },
                            //SC_END_DT: { type: "date", editable: false }
                        }
                    }
                }
            });
        }

        vm.submit = function () {

            var submitData = angular.copy($scope.$parent.form);
            
            Dialog.confirm('Do you want to submit?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return MrcDataService.saveDataByFullUrl(submitData, '/api/mrc/AddFabBk/SubmitAddFabBk').then(function (res) {
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
                                    $scope.$parent.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                                    $scope.$parent.form.EVENT_NAME = res.data.PEVENT_NAME_RTN;
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };
        
        vm.approve = function () {

            var submitData = angular.copy($scope.$parent.form);

            Dialog.confirm('Do you want to approve?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return MrcDataService.saveDataByFullUrl(submitData, '/api/mrc/AddFabBk/ApproveAddFabBk').then(function (res) {
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
                                    $scope.$parent.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                                    $scope.$parent.form.EVENT_NAME = res.data.PEVENT_NAME_RTN;
                                }

                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.reviseModal = function (adfbHdrID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'AdfbRevisionModal.html',
                controller: 'KntAdfbReviseController',
                size: 'md',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    adfbHdrID: function () {
                        return adfbHdrID;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                //console.log('received');
                console.log(data);

                var vRevisionData = angular.copy(data);
                
                $state.go('AddFabBkingH.Dtl', { pMC_BLK_ADFB_REQ_H_ID: vRevisionData.MC_BLK_ADFB_REQ_H_ID });
                
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };
    }
})();
//=============== End for AdditionalFabBkingDController =================







// Start KntSciBillReviseController Modal Controller
(function () {
    'use strict';

    angular.module('multitex.knitting').controller('KntAdfbReviseController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'adfbHdrID', 'KnittingDataService', 'Dialog', KntAdfbReviseController]);
    function KntAdfbReviseController($modalInstance, $q, $scope, $http, $state, $filter, config, adfbHdrID, KnittingDataService, Dialog) {

        $scope.showSplash = true;

        activate();
        console.log($scope.$parent.form);

        $scope.today = new Date();
        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.LAST_REV_DT = $scope.today;


        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                $scope.showSplash = false;
            });
        }

        $scope.RevisionDate_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RevisionDate_LNopened = true;
        }

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };

       
        

        $scope.save = function (token, valid) {
            if (!valid) return;
           

            var submitData = {
                MC_BLK_ADFB_REQ_H_ID: $scope.$parent.form.MC_BLK_ADFB_REQ_H_ID, LAST_REV_DT: $filter('date')($scope.LAST_REV_DT, $scope.dtFormat),
                REV_REASON: $scope.REV_REASON
            };

            Dialog.confirm('Do you want to revision?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/mrc/AddFabBk/ReviseAddFabBk',
                         method: 'post',
                         data: submitData
                     })
                     .then(function (res) {
                         //$scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             //$scope.$parent.errors = [];
                             //$scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             //if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             //    vm.removeItemTempRow(index, removeFrom, master);
                             //};

                             if (res.data.PLAST_REV_NO_RTN > 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 submitData['MC_BLK_ADFB_REQ_H_ID'] = res.data.PMC_BLK_ADFB_REQ_H_ID_RTN;
                                 //submitData['AFAB_REQ_NO'] = res.data.PAFAB_REQ_NO_RTN;
                                 //submitData['LAST_REV_NO'] = res.data.PLAST_REV_NO_RTN;
                                 //submitData['RF_ACTN_STATUS_ID'] = 16;
                                 //submitData['REVISION_DT'] = $filter('date')($scope.REVISION_DT, "dd/MMM/yyyy hh:mm a");
                                 $modalInstance.close(submitData);
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });

            //var obj = {};
            //obj = $scope.asortDataList._data.map(function (ob) {
            //    return {
            //        DETAIL_INDEX: ob.DETAIL_INDEX, MC_SMP_REQ_D_ASORT_ID: ob.MC_SMP_REQ_D_ASORT_ID > 0 ? ob.MC_SMP_REQ_D_ASORT_ID : 0,
            //        MC_SMP_REQ_D_ID: ob.MC_SMP_REQ_D_ID, MC_ORDER_H_ID: ob.MC_ORDER_H_ID, STYLE_NO: ob.STYLE_NO, ASSORT_QTY: ob.RQD_QTY,
            //        QTY_MOU_ID: ob.MC_SMP_REQ_D_ASORT_ID > 0 ? ob.QTY_MOU_ID : $scope.QTY_MOU_ID
            //    };
            //});


        };


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();
// End KntSciBillReviseController Modal Controller








//=============== Start for AdditionalFabBkingListController =================
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('AdditionalFabBkingListController', ['$q', 'config', 'Dialog', 'MrcDataService', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', 'userLavelData', AdditionalFabBkingListController]);
    function AdditionalFabBkingListController($q, config, Dialog, MrcDataService, $stateParams, $state, $scope, $filter, commonDataService, userLavelData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        console.log(userLavelData);
        if (userLavelData['USER_DRAFTER_NAME'] == 'DRAFTER') {
            vm.isVisableAddBtn = 'Y';
        }
        else {
            vm.isVisableAddBtn = 'N';
        }

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';
        vm.form = {};

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerAcList(), getByrAccWiseStyleExtList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

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
                    
                    $stateParams.pMC_BYR_ACC_ID = item.MC_BYR_ACC_ID;

                    vm.styleExtDataSource.read();
                },                
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }

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
                        var url = '/api/mrc/Order/GetOrderList?pRF_FAB_PROD_CAT_ID=2&pMC_BYR_ACC_ID=' + vm.form.MC_BYR_ACC_ID;
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

               
        vm.additionalBkingGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "MC_BLK_ADFB_REQ_H_ID",
                    fields: {
                        AFAB_REQ_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/mrc/AddFabBk/GetAddFabBkingList?pMC_BYR_ACC_ID=' + (vm.form.MC_BYR_ACC_ID || $stateParams.pMC_BYR_ACC_ID) + '&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || $stateParams.pMC_STYLE_H_EXT_ID || null);
                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;

                    console.log(url);

                    return MrcDataService.getDataByFullUrl(url).then(function (res) {

                        angular.forEach(res.data, function (val, key) {
                            val['USER_DRAFTER_NAME'] = userLavelData['USER_DRAFTER_NAME'];
                            val['USER_APPROVER_NAME'] = userLavelData['USER_APPROVER_NAME'];
                        });

                        e.success(res);
                    });
                }
            }
        });

        vm.additionalBkingGridOption = {
            height: 420,
            sortable: true,
            //scrollable: {
            //    virtual: true               
            //},
            pageable: true,
            columns: [
                { field: "AFAB_REQ_DT", title: "Req Date", format: "{0:dd-MMM-yyyy}", width: "8%" },
                { field: "AFAB_REQ_NO", title: "Req#", width: "11%" },
                { field: "LAST_REV_NO", title: "Rev#", width: "5%" },
                { field: "AFAB_REF_REQ_NO", title: "Ref#", width: "11%" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "10%" },
                { field: "STYLE_NO", title: "Style#", width: "15%" },
                { field: "MC_ORDER_NO_LST", title: "Order#", width: "18%" },
                {
                    title: "Status",
                    field: "EVENT_NAME",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID<=88'>{{dataItem.EVENT_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==90'>{{dataItem.EVENT_NAME}}</label>";
                    },
                    width: "15%"
                },
                { field: "IS_ACTIVE", title: "Actv?", width: "5%" },
                { field: "USER_DRAFTER_NAME", title: "Verifier", hidden: true },
                { field: "USER_APPROVER_NAME", title: "Approver", hidden: true },
                {
                    title: "Action",
                    template: function () {
                        return "<a type='button' class='btn btn-xs blue' title='Edit/View' ui-sref='AddFabBkingH.Dtl({pMC_BLK_ADFB_REQ_H_ID : dataItem.MC_BLK_ADFB_REQ_H_ID, pMC_BYR_ACC_ID: dataItem.MC_BYR_ACC_ID, pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID, pMC_STYLE_H_EXT_ID: dataItem.MC_STYLE_H_EXT_ID})' ng-if='dataItem.USER_DRAFTER_NAME==\"DRAFTER\"' ><i class='fa fa-edit'></i></a>&nbsp;" +
                            //"<a type='button' class='btn btn-xs blue' ui-sref='AddFabBkingH.Dtl({pMC_BLK_ADFB_REQ_H_ID : dataItem.MC_BLK_ADFB_REQ_H_ID, pMC_BYR_ACC_ID: dataItem.MC_BYR_ACC_ID, pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID, pMC_STYLE_H_EXT_ID: dataItem.MC_STYLE_H_EXT_ID})' ng-if='dataItem.RF_ACTN_STATUS_ID>16'>View</a>&nbsp;" +
                            "<a type='button' class='btn btn-xs blue' ng-click='vm.submit(dataItem)' ng-if='dataItem.RF_ACTN_STATUS_ID==16 && userLavelData.USER_DRAFTER_NAME==\"DRAFTER\"'>Submit</a>&nbsp;" +
                            "<a type='button' class='btn btn-xs blue' href='/Merchandising/Mrc/AddFabBkingRpt?a=360/#/addFabBkingRpt?pMC_BLK_ADFB_REQ_H_ID={{dataItem.MC_BLK_ADFB_REQ_H_ID}}' target='_blank' >Print</a>&nbsp;" +                            
                            "<a type='button' class='btn btn-xs blue' ng-click='vm.approve(dataItem)' ng-if='(dataItem.RF_ACTN_STATUS_ID==88 && dataItem.USER_APPROVER_NAME==\"APPROVER\")'>Approve</a>&nbsp;";
                    },
                    width: "8%"
                }
            ]
        };

        //userLavelData.USER_DRAFTER_NAME==\"DRAFTER\"

        vm.getAdditionalBkingList = function () {
            $state.go('AddFabBkingList', { pMC_BYR_ACC_ID: vm.form.MC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });

            vm.additionalBkingGridDataSource.read();
        };

        vm.submit = function (dataItem) {

            var submitData = angular.copy(dataItem);

            Dialog.confirm('Do you want to submit?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return MrcDataService.saveDataByFullUrl(submitData, '/api/mrc/AddFabBk/SubmitAddFabBk').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                vm.additionalBkingGridDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.approve = function (dataItem) {

            var submitData = angular.copy(dataItem);

            Dialog.confirm('Do you want to approve?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);

                    return MrcDataService.saveDataByFullUrl(submitData, '/api/mrc/AddFabBk/ApproveAddFabBk').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                vm.additionalBkingGridDataSource.read();

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
//=============== End for AdditionalFabBkingListController =================







