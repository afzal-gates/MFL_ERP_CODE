//=============== Start for KntFabBk4LabdipHController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntFabBk4LabdipHController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$http', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', 'FabReqHdrData', KntFabBk4LabdipHController]);
    function KntFabBk4LabdipHController($q, config, Dialog, KnittingDataService, $http, $stateParams, $state, $scope, $filter, commonDataService, $modal, FabReqHdrData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        var key = 'KNT_GEN_FAB_REQ_H_ID';
        
        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        vm.form = FabReqHdrData[key] ? FabReqHdrData : { KNT_GEN_FAB_REQ_H_ID: -1, MC_FAB_PROD_ORD_H_ID: 0, GFAB_REQ_DT: $filter('date')(vm.today, vm.dtFormat) };
        $scope.form = FabReqHdrData[key] ? FabReqHdrData : { KNT_GEN_FAB_REQ_H_ID: -1, MC_FAB_PROD_ORD_H_ID: 0, GFAB_REQ_DT: $filter('date')(vm.today, vm.dtFormat) };
        
       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getReqTypeList(), getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getFabricByStyleId()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;                
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.GFAB_REQ_DT_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.GFAB_REQ_DT_Opened = true;
        }

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

            vm.form['GFAB_REQ_TO'] = item.HR_EMPLOYEE_ID;
            vm.form.REQ_TO_EMP_CODE = item.EMPLOYEE_CODE;
            vm.form.REQ_TO_EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.REQ_TO_EMP_DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            vm.form.REQ_TO_EMP_PROFILE = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN;
        }

        //$scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

        //    if (!newVal || newVal == '') {
        //        vm.form.GFAB_REQ_TO = null;
        //    }

        //});
                
        $scope.$watch('vm.form', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                }
            }
        }, true);
             
        function getReqTypeList() {
            
            return vm.reqTypeList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetReqTypeByUser').then(function (res) {   //GetReqType
                                var list = _.filter(res, function (o) { return (o.RF_REQ_TYPE_ID != 4 && o.IS_R_I == "R") })
                                //KnittingDataService.LookupListData(88).then(function (res) {
                                e.success(list);

                                if (list.length == 1) {
                                    vm.form.RF_REQ_TYPE_ID = list[0].RF_REQ_TYPE_ID;                                    
                                }
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
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
                            ///api/mrc/BuyerAcc/GetBuyerAccGrpList
                            ///api/mrc/BuyerAcc/getBuyerAccListByUserId
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

                    //$stateParams.pMC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_STYLE_H_ID = 0;

                    vm.styleExtDataSource.read();
                    $scope.styleFabricDataSource.read();

                },
                //dataBound: function (e) {
                //    if ($stateParams.pMC_BYR_ACC_ID != null && $stateParams.pMC_BYR_ACC_ID > 0) {
                //        vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;

                //        vm.styleExtDataSource.read();
                //        //vm.styleFabricDataSource.read();
                //        //vm.getOrderByBuyerAC($stateParams.pMC_BYR_ACC_ID);
                //    }
                //},
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
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
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;

                    vm.form.STYLE_ORDER_NAME = item.STYLE_NO + ' (' + item.ORDER_NO + ')';
                    //data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)

                    $scope.styleFabricDataSource.read();
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
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
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
                dataValueField: "MC_STYLE_D_FAB_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    console.log(item);

                //    vm.fabOrder.FABRIC_DESC = item.FABRIC_DESC;
                //    vm.fabOrder.RF_FAB_TYPE_ID = item.RF_FAB_TYPE_ID;
                //    vm.fabOrder.RF_FIB_COMP_ID = item.RF_FIB_COMP_ID;
                //    vm.fabOrder.FIN_GSM = item.FB_WT_MAX;
                //}
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

    }
})();
//=============== End for KntFabBk4LabdipHController =================











//=============== Start for KntFabBk4LabdipDController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntFabBk4LabdipDController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$http', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', '$timeout', KntFabBk4LabdipDController]);
    function KntFabBk4LabdipDController($q, config, Dialog, KnittingDataService, $http, $stateParams, $state, $scope, $filter, commonDataService, $modal, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        var brandListData = [];
        var fabPartListData = [];
        var colGrpListData = [];

        
        vm.fabOrder = { KNT_GEN_FAB_REQ_D_ID: 0, FIB_COMP_NAME: '', FAB_TYPE_SNAME: '', FIN_GSM: '', FIN_DIA: '', DIA_MOU_CODE: 'Inch', FIN_DIA_TYPE: 'Open', QTY_MOU_CODE: 'Kg' };
   
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getFabTypeList(), getFabricCompList(), getMOUList(), getDiaTypeList(), getColorList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
                        

        function getFabTypeList() {
            vm.fabTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FAB_TYPE_SNAME",
                dataValueField: "RF_FAB_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    if (vm.fabOrder.IS_ELA_MXD == 'Y') {
                        vm.fabOrder.FAB_TYPE_SNAME = 'Lycra ' + item.FAB_TYPE_SNAME;
                    }
                    else {
                        vm.fabOrder.FAB_TYPE_SNAME = item.FAB_TYPE_SNAME;
                    }

                    genSpecialInstruction();
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

                    vm.fabOrder.FIB_COMP_NAME = item.FIB_COMP_NAME;
                    vm.fabOrder.IS_ELA_MXD = item.IS_ELA_MXD;

                    if (vm.fabOrder.IS_ELA_MXD == 'Y') {
                        vm.fabOrder.FAB_TYPE_SNAME = 'Lycra ' + vm.fabOrder.FAB_TYPE_SNAME;
                    }

                    genSpecialInstruction();

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
                                    vm.fabOrder['FIB_COMP_NAME'] = result.FIB_COMP_NAME;

                                    if (result.IS_ELA_MXD == 'Y') {
                                        vm.fabOrder.FAB_TYPE_SNAME = 'Lycra ' + vm.fabOrder.FAB_TYPE_SNAME;
                                    }

                                    genSpecialInstruction();
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

                    if (item.RF_MOU_ID > 0) {
                        vm.fabOrder.DIA_MOU_CODE = item.MOU_CODE;
                    }
                    else {
                        vm.fabOrder.DIA_MOU_CODE = '';
                    }
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

                    if (item.LOOKUP_DATA_ID > 0) {
                        vm.fabOrder.FIN_DIA_TYPE = item.LK_DATA_NAME_EN;
                    }
                    else {
                        vm.fabOrder.FIN_DIA_TYPE = '';
                    }
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

        function getColorList() {
            vm.colorListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.fabOrder.COLOR_NAME_EN = item.COLOR_NAME_EN;
                    vm.fabOrder.LK_COL_TYPE_ID = item.LK_COL_TYPE_ID;


                    if (item.MC_COLOR_ID < 0) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'NewColourEntryModal.html',
                            controller: 'ColourMasterModalController',
                            controllerAs: 'vm',
                            size: 'lg',
                            windowClass: 'large-Modal',
                            resolve: {
                                ColourList: function (KnittingDataService) {
                                    return KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll');
                                }
                            }
                        });

                        modalInstance.result.then(function (result) {
                            if (result.MC_COLOR_ID && result.MC_COLOR_ID > 0) {
                                vm.colorListDataSource.read().then(function () {
                                    vm.fabOrder['FAB_COLOR_ID'] = result.MC_COLOR_ID;
                                    vm.fabOrder['COLOR_NAME_EN'] = result.COLOR_NAME_EN;
                                });
                            }
                        }, function () {
                            console.log('Modal dismissed at: ' + new Date());
                        });
                    }
                }
            };

            return vm.colorListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/ColourMaster/SelectAll').then(function (res) {
                            e.success([{ 'COLOR_NAME_EN': '---New Color---', MC_COLOR_ID: -1 }].concat(res));
                        });
                    }
                }
            });
        };

        function genSpecialInstruction() {
            vm.fabOrder.SP_INSTRUCTION = (vm.fabOrder['FIB_COMP_NAME'] ? vm.fabOrder['FIB_COMP_NAME'] : '') + ' ' +
                (vm.fabOrder['FAB_TYPE_SNAME'] ? vm.fabOrder['FAB_TYPE_SNAME'] : '') + ' ' +
                (vm.fabOrder['IS_SLUB'] == 'Y' ? 'Slub ' : '') +
                (vm.fabOrder['IS_MELLANGE'] == 'Y' ? 'Mellange ' : '') +
                (vm.fabOrder['FIN_GSM'] ? vm.fabOrder['FIN_GSM'] + ' GSM' : '');
        }

        vm.onChangeFinGsm = function () {
            genSpecialInstruction();
        }

        vm.onChangeIsSlub = function () {
            genSpecialInstruction();
        }

        vm.onChangeIsMellange = function () {
            genSpecialInstruction();
        }
        
        vm.fabOnChange = function (e) {
            var item = e.sender.dataItem(e.sender.select());
            console.log(item);

            vm.fabOrder.FABRIC_DESC = item.FABRIC_DESC;
            vm.fabOrder.RF_FAB_TYPE_ID = item.RF_FAB_TYPE_ID;
            vm.fabOrder.FAB_TYPE_SNAME = item.FAB_TYPE_SNAME;
            vm.fabOrder.RF_FIB_COMP_ID = item.RF_FIB_COMP_ID;
            vm.fabOrder.FIN_GSM = item.FB_WT_MAX;            
        };

        vm.onChangeFinDia = function () {
            vm.fabOrder.FIN_DIA_N_DIA_TYPE = vm.fabOrder.FIN_DIA + ' ' + vm.fabOrder.DIA_MOU_CODE + ' ' + vm.fabOrder.FIN_DIA_TYPE;
        }

        vm.onChangeMcDiaGG = function () {
            vm.fabOrder.MC_DIA_N_GG = vm.fabOrder.MC_DIA + 'X' + vm.fabOrder.MC_GG;
        }

        vm.onChangeNetFfQty = function () {
            vm.fabOrder.RQD_FAB_QTY_NAME = vm.fabOrder.RQD_FAB_QTY + ' Kg'; //+ vm.fabOrder.QTY_MOU_CODE;
        }

        vm.addRow = function (data) {
            var ddRF_FIB_COMP_ID = $("#RF_FIB_COMP_ID").data("kendoDropDownList");            
            var dataItem = ddRF_FIB_COMP_ID.dataItem();            
            //console.log(dataItem);

            data['FIB_COMP_NAME'] = dataItem.FIB_COMP_NAME;

            var fabOrderCopy = angular.copy(data);

            vm.fabOrderGridDataSource.insert(0, data);

            vm.cancelToGrid();

            vm.isAddToGrid = 'Y';
        }

        function findGridIndex(index, KNT_SC_YRN_RCV_H_ID) {
            var dataList = $scope.$parent.mainYrnRcvGridDataSource.data();

            if (index > -1) {
                return index;
            } else {

                return _.findIndex(dataList, function (obj) {
                    return parseInt(obj.KNT_SC_YRN_RCV_H_ID) == parseInt(KNT_SC_YRN_RCV_H_ID);
                });
            }

        };

        vm.updateIndex = 0;
        vm.editRow = function (dataItem) {
            var index = vm.fabOrderGridDataSource.indexOf(dataItem);
            //alert('index:' + index);

            //vm.updateIndex = findGridIndex(index, dataItem.KNT_SC_YRN_RCV_H_ID);
            //alert(vm.updateHrdGridIndex);

            console.log(dataItem);
            var data = angular.copy(dataItem);
            vm.fabOrder = data;                                  

            vm.isAddToGrid = 'N';
        };

        
        vm.removeRow = function (dataItem) {
            vm.fabOrderGridDataSource.remove(dataItem);
        };

        vm.updateFabOrder = function (data) {
            var fabOrderCopy = angular.copy(data);

            var dataItem = vm.fabOrderGridDataSource.getByUid(data.uid);

            console.log(dataItem);

            dataItem.set('MC_STYLE_D_FAB_ID', fabOrderCopy.MC_STYLE_D_FAB_ID);
            dataItem.set('RF_FAB_TYPE_ID', fabOrderCopy.RF_FAB_TYPE_ID);
            dataItem.set('FAB_TYPE_SNAME', fabOrderCopy.FAB_TYPE_SNAME);

            dataItem.set('STYLE_ORDER_NAME', fabOrderCopy.STYLE_ORDER_NAME);
            dataItem.set('FABRIC_DESC', fabOrderCopy.FABRIC_DESC);

            dataItem.set('SP_INSTRUCTION', fabOrderCopy.SP_INSTRUCTION);

            dataItem.set('RF_FIB_COMP_ID', fabOrderCopy.RF_FIB_COMP_ID);
            dataItem.set('FIB_COMP_NAME', fabOrderCopy.FIB_COMP_NAME);

            dataItem.set('FAB_COLOR_ID', fabOrderCopy.FAB_COLOR_ID);
            dataItem.set('COLOR_NAME_EN', fabOrderCopy.COLOR_NAME_EN);
            dataItem.set('FIN_DIA', fabOrderCopy.FIN_DIA);
            dataItem.set('DIA_MOU_ID', fabOrderCopy.DIA_MOU_ID);
            dataItem.set('LK_DIA_TYPE_ID', fabOrderCopy.LK_DIA_TYPE_ID);
            dataItem.set('FIN_GSM', fabOrderCopy.FIN_GSM);
            dataItem.set('RQD_FAB_QTY', fabOrderCopy.RQD_FAB_QTY);
            
            dataItem.set('RQD_FAB_QTY_NAME', fabOrderCopy.RQD_FAB_QTY_NAME);
            dataItem.set('KNT_MC_DIA_ID', fabOrderCopy.KNT_MC_DIA_ID);            
            dataItem.set('FIN_DIA_N_DIA_TYPE', fabOrderCopy.FIN_DIA_N_DIA_TYPE);
            
            vm.fabOrder.MC_FAB_PROD_ORD_D_ID = -1;
            vm.fabOrder.NET_FFAB_QTY = '';

            dataItem.set('editMode', false);

            vm.isAddToGrid = 'Y';
        }
        
        vm.cancelToGrid = function () {
            vm.fabOrder.RQD_FAB_QTY = '';

            //vm.fabOrder.MC_BLK_FAB_REQ_H_ID = 0;
            //vm.fabOrder.MC_BLK_FAB_REQ_D_ID = 0;

            vm.isAddToGrid = 'Y';
        };

        vm.fabOrderGridOption = {
            height: 220,
            sortable: true,
            columns: [                
                { field: "COLOR_NAME_EN", title: "Color", width: "15%" },
                { field: "FABRIC_DESC", title: "Fabric", width: "30%" },
                //{ field: "FAB_TYPE_SNAME", title: "Fab Type", width: "15%" },
                //{ field: "FIB_COMP_NAME", title: "Fab Comp", width: "15%" },                
                { field: "SP_INSTRUCTION", title: "SP Instruc", width: "30%" },
                { field: "FIN_GSM", title: "Gsm", width: "5%" },
                { field: "FIN_DIA_N_DIA_TYPE", title: "Fin.Dia", width: "10%" },
                { field: "RQD_FAB_QTY_NAME", title: "Qty", width: "5%" },                
                //{ field: "SC_START_DT", title: "Start Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                //{ field: "SC_END_DT", title: "End Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem);'><i class='fa fa-edit'></i></button>&nbsp;<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.MC_FAB_PROD_ORD_D_ID>0?true:false' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "5%"
                }
            ]
        };

        vm.fabOrderGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    //e.success([]);
                    return KnittingDataService.getDataByFullUrl('/api/knit/GenFabReq/GetFabReqDtl?pKNT_GEN_FAB_REQ_H_ID=' + ($scope.$parent.form.KNT_GEN_FAB_REQ_H_ID || 0)).then(function (res) {

                        if (res.length > 0) {
                            //angular.forEach(res, function (val, key) {
                            //    val['SC_START_DT'] = $filter('date')(val['SC_START_DT'], vm.dtFormat);
                            //    val['SC_END_DT'] = $filter('date')(val['SC_END_DT'], vm.dtFormat);
                            //});

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
                    id: "KNT_GEN_FAB_REQ_D_ID",
                    fields: {
                        //SC_START_DT: { type: "date", editable: false },
                        //SC_END_DT: { type: "date", editable: false }
                    }
                }
            }
        });

        vm.backToList = function () {
            $state.go('FabBk4LabdipList');
        }

        vm.newFabOrder = function () {
            vm.IS_NEXT = 'N';
            vm.cancelToGrid();

            $state.go('KntSmplFabProdOrdMnul', { pMC_FAB_PROD_ORD_H_ID: 0, pRF_FAB_PROD_CAT_ID: vm.form.RF_FAB_PROD_CAT_ID, pMC_BYR_ACC_ID: $stateParams.pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID }, { notify: false });
        }

        vm.batchSave = function (pIS_FINALIZE) {
            
            var submitData = angular.copy($scope.$parent.form);
            submitData['GEN_FAB_REQ_DTL_XML'] = "";

            var vMsg;
            if (pIS_FINALIZE == 'Y') {
                vMsg = "Do you want to save and finalize?";
                submitData['IS_FINALIZE'] = 'Y';
            }
            else {
                vMsg = "Do you want to save?";
            }


            var fabOrdGridData = vm.fabOrderGridDataSource.data();


            //var yrnRcvHdrIndex = 0;
            //var yrnRcvDtlIndex = 0;
            //var yrnRcvHdrData = [];
            //var yrnRcvDtlData = [];

            //var fabDtlIndex = 0;
            ////var fabDtlYrnIndex = 0;
            var fabDtlData = [];
            //var fabDtlYrnData = [];


            angular.forEach(fabOrdGridData, function (val, key) {

                var fabData = {
                    KNT_GEN_FAB_REQ_D_ID: val['KNT_GEN_FAB_REQ_D_ID'], MC_STYLE_D_FAB_ID: val['MC_STYLE_D_FAB_ID'],
                    DIA_MOU_ID: val['DIA_MOU_ID'], FIN_DIA: val['FIN_DIA'], LK_DIA_TYPE_ID: val['LK_DIA_TYPE_ID'],
                    FAB_COLOR_ID: val['FAB_COLOR_ID'], LK_COL_TYPE_ID: val['LK_COL_TYPE_ID'],
                    RF_FAB_TYPE_ID: val['RF_FAB_TYPE_ID'], RF_FIB_COMP_ID: val['RF_FIB_COMP_ID'], FIN_GSM: val['FIN_GSM'],
                    IS_SLUB: val['IS_SLUB'], IS_MELLANGE: val['IS_MELLANGE'], SP_INSTRUCTION: val['SP_INSTRUCTION'],
                    RQD_FAB_QTY: val['RQD_FAB_QTY']
                };

                fabDtlData.push(fabData);

            });


            submitData.GEN_FAB_REQ_DTL_XML = KnittingDataService.xmlStringShort(fabDtlData.map(function (ob) {
                return ob;
            }));



            console.log(submitData);
            console.log(fabOrdGridData);
            //return;

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    return KnittingDataService.saveDataByFullUrl(submitData, '/api/knit/GenFabReq/BatchSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                if (res.data.PKNT_GEN_FAB_REQ_H_ID_RTN != 0) {
                                    $stateParams.pKNT_GEN_FAB_REQ_H_ID = res.data.PKNT_GEN_FAB_REQ_H_ID_RTN;
                                    $scope.$parent.form.KNT_GEN_FAB_REQ_H_ID = res.data.PKNT_GEN_FAB_REQ_H_ID_RTN;
                                    $scope.$parent.form.GFAB_REQ_NO = res.data.PGFAB_REQ_NO_RTN;

                                    if (pIS_FINALIZE == 'Y') {
                                        $scope.$parent.form.RF_ACTN_STATUS_ID = 73;
                                    }

                                    vm.fabOrderGridDataSource.read();
                                    $state.go('FabBk4LabdipH.Dtl', { pKNT_GEN_FAB_REQ_H_ID: res['data'].PKNT_GEN_FAB_REQ_H_ID_RTN }, { notify: false });

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
//=============== End for KntFabBk4LabdipDController =================












//=============== Start for KntFabBk4LabdipListController =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntFabBk4LabdipListController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$http', '$stateParams', '$state', '$scope', '$filter', 'commonDataService', '$modal', KntFabBk4LabdipListController]);
    function KntFabBk4LabdipListController($q, config, Dialog, KnittingDataService, $http, $stateParams, $state, $scope, $filter, commonDataService, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        var key = 'KNT_GEN_FAB_REQ_H_ID';

        vm.form = { FROM_DT: $filter('date')(vm.today, vm.dtFormat), TO_DT: $filter('date')(vm.today, vm.dtFormat) };
        

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.FROM_DT_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.FROM_DT_Opened = true;
        }

        vm.TO_DT_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.TO_DT_Opened = true;
        }

        vm.reqGridOption = {
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
                { field: "GFAB_REQ_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "GFAB_REQ_NO", title: "Req#", type: "string", width: "15%" },
                { field: "REQ_TYPE_NAME", title: "Req Type", type: "string", width: "15%" },                
                { field: "REMARKS", title: "Remarks", width: "20%", filterable: false },                
                {
                    field: "ACTN_STATUS_NAME",
                    title: "Status",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID==71'>{{dataItem.ACTN_STATUS_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==73'>{{dataItem.ACTN_STATUS_NAME}}</label>";
                    },
                    width: "10%"
                },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' title='Edit/View' ng-click='vm.editReq(dataItem)'><i class='fa fa-edit'></i></button>";
                    },
                    width: "10%"
                }
            ]
        };

        vm.reqGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_GEN_FAB_REQ_H_ID",
                    fields: {
                        GFAB_REQ_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/GenFabReq/GetFabReqList?pFROM_DT=' + $filter('date')(vm.form.FROM_DT, vm.dtFormat) + '&pTO_DT=' + $filter('date')(vm.form.TO_DT, vm.dtFormat);

                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    });
                }
            },
            sort: [{ field: 'GFAB_REQ_DT', dir: 'desc' }]
        });

      
        vm.getReqList = function () {
            vm.reqGridDataSource.read();
        };

        vm.editReq = function (dataItem) {
            $state.go('FabBk4LabdipH.Dtl', { pKNT_GEN_FAB_REQ_H_ID: dataItem.KNT_GEN_FAB_REQ_H_ID });
        };


    }
})();
//=============== End for KntFabBk4LabdipHController =================