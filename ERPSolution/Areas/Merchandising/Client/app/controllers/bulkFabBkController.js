(function () {
    'use strict';
    angular.module('multitex.mrc').controller('bulkFabBkController', ['logger', 'config', '$q', '$scope', '$http', 'exception', 'formData', 'revisionList', '$filter', '$timeout', '$state', '$stateParams', '$modal', 'MrcDataService', 'Dialog', bulkFabBkController]);
    function bulkFabBkController(logger, config, $q, $scope, $http, exception, formData, revisionList, $filter, $timeout, $state, $stateParams, $modal, MrcDataService, Dialog) {
            
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        //MrcDataService.GetAllOthers(config.appExRateUsdBdtApi).then(function (res) {
        //    vm.BDT_RATE = res;
        //    vm.form.CURR_CONV_LOC = parseFloat(vm.BDT_RATE);
        //});

        vm.pageSizeList = [{ PAGE_SIZE_NAME: 'A4' }, { PAGE_SIZE_NAME: 'Legal' }, { PAGE_SIZE_NAME: 'Legal-1' }];
        vm.PAGE_SIZE_NAME = 'A4';

        vm.IS_COPY = $stateParams.pIS_COPY;

        var key = 'MC_BLK_FAB_REQ_H_ID';
        vm.today = new Date();
        vm.form = {
            MC_BLK_FAB_REQ_H_ID: 0, MC_STYL_BGT_H_ID: 0, BLK_FAB_REQ_DT: vm.today, PCUT_WSTG: 5, PYD_WSTG: 4, PAOP_WSTG: 6, HAS_PLOSS: 'N', MC_ORDER_H_ID_LST: []
        };
        
        //vm.form = formData[key] ? formData : { BLK_FAB_REQ_DT: vm.today, PCUT_WSTG: 5 };

        //console.log(formData);  
        console.log(revisionList);
        vm.revisionList = angular.copy(revisionList);
        
        vm.MC_COLOR_ID_LST = [];
        $scope.styleWiseFabListData = [];
        $scope.styleWiseTapeItemListData = [];
        vm.itemListData = [];
        vm.ParentItemTemplate = '<b>{{dataItem.ITEM_NAME_EN+" "+dataItem.MODEL_NO}}</b>';
        vm.ItemTemplate = '<h4 style="padding-bottom:0px;margin-bottom:0px;"><b>{{dataItem.ITEM_NAME_EN+" "+dataItem.MODEL_NO}}</b></h4> <div ><p ng-repeat="itm in dataItem.CHILD_ITEMS" style="padding-left:20px;padding-top:0px;padding-bottom:0px;margin-bottom:0px;">{{ itm.ITEM_NAME_EN }}</p></div>';
        vm.ItemValueTemplate = '{{dataItem.ITEM_NAME_EN+" "+dataItem.MODEL_NO}}';

        vm.pcsMouID = 1;
        vm.setMouID = 2;
        vm.defaultCurrID = 2;
        
        //$timeout( function(){ $scope.callAtTimeout(); }, 3000);

        activate();
        function activate() {
            var promise = [getBuyerAcList(), getCurrencyList()];
            return $q.all(promise).then(function () {

                vm.form.RF_CURRENCY_ID = vm.defaultCurrID;                                
                vm.showSplash = false;
                
            });
        }

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.processLossModal = function (mcBlkFabReqHID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ProcessLossModal.html',
                controller: 'ProcessLossModalController',
                size: 'small',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    mcBlkFabReqHID: function () {
                        return mcBlkFabReqHID;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
                vm.form.HAS_PLOSS = data.PHAS_PLOSS_RTN;

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        //$scope.collarCuffModal = function (mcBlkFabReqHID, pMC_STYLE_H_ID) {
        //    //alert(pMC_STYLE_H_ID);
        //    var modalInstance = $modal.open({
        //        animation: true,
        //        //templateUrl: '/Merchandising/Mrc/_CollarCuffManagement',
        //        templateUrl: 'CollarCuffMeasModal.html',
        //        controller: 'CollarCuffController',                
        //        controllerAs: 'vm',
        //        size: 'lg',
        //        windowClass: 'app-modal-window',
        //        resolve: {
        //            styleID: function () {
        //                return pMC_STYLE_H_ID;
        //            }
        //        }
        //    });

        //    modalInstance.result.then(function (data) {
        //        console.log(data);
        //    }, function () {
        //        console.log('Modal dismissed at: ' + new Date());
        //    });
        //};

        $scope.collarCuffModal = function (mcBlkFabReqHID, pMC_STYLE_H_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'CollarCuffMeasModal.html',
                controller: 'CollarCuffMeasModalController',
                size: 'lg',
                windowClass: 'app-modal-window',               
                resolve: {
                    styleID: function () {
                        return pMC_STYLE_H_ID;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        $scope.rowCategoryModal = function (mcBlkFabReqHID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ReportRowCategoryModal.html',
                controller: 'RowCategoryModalController',
                size: 'md',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    mcBlkFabReqHID: function () {
                        return mcBlkFabReqHID;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };
        
        $scope.bookingRevisionModal = function (mcBlkFabReqHID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'BookingRevisionModal.html',
                controller: 'BookingRevisionModalController',
                size: 'md',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    mcBlkFabReqHID: function () {
                        return mcBlkFabReqHID;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                //console.log('received');
                console.log(data);

                var vRevisionData = angular.copy(data);
                vm.form.REVISION_NO = vRevisionData.REVISION_NO;
                vm.form.REVISION_DT = vRevisionData.REVISION_DT;
                vm.form.REV_REASON = vRevisionData.REV_REASON;
                vm.form.IS_BFB_FINALIZED = vRevisionData.IS_BFB_FINALIZED;
                $scope.IS_BFB_FINALIZED = vRevisionData.IS_BFB_FINALIZED;

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };



        vm.bookingDateOpen = function ($event) {            
            $event.preventDefault();
            $event.stopPropagation();

            vm.bookingDateOpened = true;
        };

        vm.revisionDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.revisionDateOpened = true;
        };
         
        vm.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };
        /// Start New Code
        function getBuyerAcList() {            
            vm.styleWiseWorkStyleList = new kendo.data.ObservableArray([]);
            vm.styleWiseOrderList = new kendo.data.ObservableArray([]);
            vm.orderWiseItemList = new kendo.data.ObservableArray([]);
            
            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "startswith",
                autoBind: true,
                dataBound: function (e) {
                    if (formData[key]) {
                        var MC_BYR_ACC_ID = formData['MC_BYR_ACC_ID']; //this.dataItem(e.item).MC_BYR_ACC_ID;
                        $scope.IS_BFB_FINALIZED = formData['IS_BFB_FINALIZED'];
                    }
                    else {
                        if ($stateParams.BAID && $stateParams.BAID > 0) {
                            var MC_BYR_ACC_ID = $stateParams.BAID;
                            vm.form['MC_BYR_ACC_ID'] = $stateParams.BAID;
                        }                        
                    }

                    if (MC_BYR_ACC_ID) {
                        var selectedStyle = {};
                        vm.buyerAccWiseStyleDataSource = new kendo.data.DataSource({
                            serverFiltering: true,                            
                            transport: {
                                read: function (e) {
                                    var webapi = new kendo.data.transports.webapi({});
                                    var params = webapi.parameterMap(e.data);
                                    var url = '/StyleHExt/BuyerWiseStyleHExtList/' + MC_BYR_ACC_ID + '/' + null + '?';                                   
                                    url += MrcDataService.kFilterStr2QueryParam(params.filter);

                                    //console.log('------');
                                    //console.log($stateParams);
                                    

                                    var paramsData = params.filter.replace(/'/g, '').split('~');
                                    //console.log(paramsData[2]);

                                    if (formData[key] && paramsData[2] == undefined) {
                                        url += '&pMC_STYLE_H_EXT_ID=' + formData.MC_STYLE_H_EXT_ID;                                        
                                    }
                                    else if ($stateParams.SEXID != undefined && paramsData[2] == undefined) {
                                        url += '&pMC_STYLE_H_EXT_ID=' + $stateParams.SEXID;
                                    }
                                    else {
                                        url += '&pMC_STYLE_H_EXT_ID';
                                    };

                                    return MrcDataService.getDataByUrl(url).then(function (res) {
                                        e.success(res);
                                        //vm.StyleDataSource = _.uniq(_.map(res.data, 'STYLE_NO'));
                                        if (formData[key]) {
                                            console.log(formData);
                                            console.log(res);
                                            selectedStyle = _.filter(res, { 'MC_STYLE_H_EXT_ID': parseInt(formData['MC_STYLE_H_EXT_ID']) });
                                        }
                                        else {
                                            if ($stateParams.SEXID && $stateParams.SEXID > 0) {
                                                selectedStyle = _.filter(res, { 'MC_STYLE_H_EXT_ID': parseInt($stateParams.SEXID) });
                                            }
                                        }

                                        if (selectedStyle.length > 0) {
                                            formData['HAS_SET'] = selectedStyle[0].HAS_SET;
                                            formData['STYLE_DESC'] = selectedStyle[0].STYLE_DESC;
                                            $scope.MC_STYLE_H_ID = selectedStyle[0].MC_STYLE_H_ID;
                                            $scope.MC_STYLE_H_EXT_ID = selectedStyle[0].MC_STYLE_H_EXT_ID;
                                            $scope.HAS_SET = selectedStyle[0].HAS_SET;

                                            if (parseInt(selectedStyle[0].MC_STYLE_H_ID) > 0 && selectedStyle[0].MC_STYLE_H_ID != '') {
                                                vm.getFabDataByStyleId(selectedStyle[0].MC_STYLE_H_ID);
                                                vm.getTapeItemDataByStyleId(selectedStyle[0].MC_STYLE_H_ID)

                                                var orderListURL;
                                                if (selectedStyle[0].HAS_EXT == 'Y') {
                                                    orderListURL = '/api/mrc/Order/WorkStyleListByUserBuyerAcc/null/' + selectedStyle[0].MC_STYLE_H_ID + '?&pIS_TNA_FINALIZED=Y';
                                                }
                                                else {
                                                    orderListURL = '/api/mrc/Order/WorkStyleListByUserBuyerAcc/' + selectedStyle[0].MC_STYLE_H_EXT_ID + '/null?&pIS_TNA_FINALIZED=Y';
                                                }

                                                return MrcDataService.GetAllOthers(orderListURL).then(function (res) {
                                                    $scope.styleWiseOrderList = res;
                                                    vm.styleWiseOrderList = _.sortBy(res, function (ob) {
                                                        return ob.ORDER_NO;
                                                    });

                                                    //console.log('========');
                                                    //console.log(vm.styleWiseOrderList);

                                                    vm.styleWiseWorkStyleList = _.map(_.groupBy(res, function (doc) {
                                                        return doc.WORK_STYLE_NO;
                                                    }), function (grouped) {
                                                        return grouped[0];
                                                    });

                                                    if ($stateParams.ORDID && $stateParams.ORDID > 0) {
                                                        vm.MC_ORDER_LIST = _.map(_.filter(vm.styleWiseOrderList, function (o) {
                                                            return o.MC_ORDER_H_ID == $stateParams.ORDID;
                                                        }), 'MC_ORDER_H_ID');
                                                    }

                                                    //vm.form.MC_ORDER_H_ID_LST = _.sortBy(res, 'MC_SIZE_GRP_ID', 'DISPLAY_ORDER desc', function (ob) {
                                                    //    return ob;
                                                    //});

                                                    vm.form.MC_ORDER_H_ID_LST = _.map(vm.styleWiseOrderList, 'MC_ORDER_H_ID');
                                                    vm.getOrderWiseItemList();

                                                }, function (err) {
                                                    console.log(err);
                                                });
                                            }
                                            else {
                                                $scope.styleWiseOrderList = [];
                                                vm.styleWiseOrderList = [];
                                                vm.styleWiseWorkStyleList = [];
                                            }
                                        }

                                    }, function (err) {
                                        console.log(err);
                                    });
                                }
                            }
                        });                                             
                    }
                    else {                       
                        vm.buyerAccWiseStyleDataSource = new kendo.data.DataSource({
                            data: []
                        });
                    }                    
                },                
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
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }
        /// End New Code
          
        
        vm.getBuyerAccWiseStyleList = function (e) {
                        
            var MC_BYR_ACC_ID = e.sender.dataItem(e.item).MC_BYR_ACC_ID;                      
            $stateParams.BAID = MC_BYR_ACC_ID;

            if (parseInt(MC_BYR_ACC_ID) > 0 && MC_BYR_ACC_ID != '') {
                vm.buyerAccWiseStyleDataSource = new kendo.data.DataSource({                    
                    serverFiltering: true,                    
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            var url = '/StyleHExt/BuyerWiseStyleHExtList/' + MC_BYR_ACC_ID + '/' + null + '?';                            
                            url += MrcDataService.kFilterStr2QueryParam(params.filter);
                            url += '&pMC_STYLE_H_EXT_ID';

                            return MrcDataService.getDataByUrl(url).then(function (res) {
                                e.success(res);                                
                            }, function (err) {
                                console.log(err);
                            })
                        }
                    }
                });
            }
            else {                                
                vm.buyerAccWiseStyleDataSource = new kendo.data.DataSource({
                    data: []
                });
            }
        };

        
        vm.WorkStyleListByUserBuyerAcc = function (e) {
            
            vm.showSplash = true;

            var item = e.sender.dataItem(e.item);
            
            //console.log(item);

            var MC_STYLE_H_ID = item.MC_STYLE_H_ID;
            var MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;

            vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
            vm.form.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;

            //if (MC_STYLE_H_ID != '' && MC_STYLE_H_ID != null) {
            //    alert(formData['MC_STYLE_H_ID']);
            //    MC_STYLE_H_ID = formData['MC_STYLE_H_ID'];
            //}

            if (e != undefined) {
                //var item = e.sender.dataItem(e.sender.select());
                //var item = e.sender.dataItem(e.item);

                vm.form.HAS_SET = item.HAS_SET;
                vm.form.STYLE_DESC = item.STYLE_DESC;
                
                $scope.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                $scope.HAS_SET = item.HAS_SET;
            }

            if (parseInt(MC_STYLE_H_EXT_ID) > 0 && MC_STYLE_H_EXT_ID != '') {
                vm.getFabDataByStyleId(MC_STYLE_H_ID);
                vm.getTapeItemDataByStyleId(MC_STYLE_H_ID)

                var orderListURL;
                if (item.HAS_EXT == 'Y') {
                    orderListURL = '/api/mrc/Order/WorkStyleListByUserBuyerAcc/null/' + MC_STYLE_H_ID + '?&pIS_TNA_FINALIZED=Y';
                }
                else {
                    orderListURL = '/api/mrc/Order/WorkStyleListByUserBuyerAcc/' + MC_STYLE_H_EXT_ID + '/null?&pIS_TNA_FINALIZED=Y';
                }
                                
                MrcDataService.GetAllOthers(orderListURL).then(function (res) {
                    $scope.styleWiseOrderList = res;                    
                    vm.styleWiseOrderList = _.sortBy(res, function (ob) {
                        return ob.ORDER_NO;
                    });

                    //console.log('//========//');
                    //console.log(vm.styleWiseOrderList);


                    vm.styleWiseWorkStyleList = _.map(_.groupBy(res, function (doc) {                        
                        return doc.ORDER_NO;
                    }), function (grouped) {
                        return grouped[0];
                    });
                    
                    vm.MC_ORDER_LIST = _.map(vm.styleWiseOrderList, 'MC_ORDER_H_ID');
                    vm.form.MC_ORDER_H_ID_LST = _.map(vm.styleWiseOrderList, 'MC_ORDER_H_ID');
                    console.log(vm.form.MC_ORDER_H_ID_LST);

                    vm.getOrderWiseItemList();
                    
                    vm.showSplash = false;

                }, function (err) {
                    console.log(err);
                });               
            }
            else {
                $scope.styleWiseOrderList = [];
                vm.styleWiseOrderList = [];
                vm.styleWiseWorkStyleList = [];
            }

            
        };

        function getCurrencyList() {

            return vm.currencyList = {
                //optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/common/CurrencyList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "CURR_CODE",
                dataValueField: "RF_CURRENCY_ID"
            };
        };
        
        vm.getOrderWiseItemList = function () {
            
            if (!formData[key]) {
                if ($stateParams.BAID && $stateParams.BAID > 0) {                    
                    vm.form['MC_BYR_ACC_ID'] = $stateParams.BAID;
                }
                if ($stateParams.SEXID && $stateParams.SEXID > 0) {
                    vm.form['MC_STYLE_H_ID'] = $stateParams.SID;
                    vm.form['MC_STYLE_H_EXT_ID'] = $stateParams.SEXID;
                }
            }

            $scope.orderWiseColorList = [];
            $scope.orderWiseSizeList = [];

            vm.orderIds = "";
                        
            if ((vm.form.MC_ORDER_H_ID_LST != [] || vm.form.MC_ORDER_H_ID_LST != "") || formData['MC_ORDER_H_ID_LST'].length > 0) {
                if (vm.form.MC_ORDER_H_ID_LST) {
                    vm.orderIds = vm.form.MC_ORDER_H_ID_LST.join(",");
                    //alert(vm.form.MC_ORDER_H_ID_LST.length);

                    vm.getOrderWiseColorList();
                    vm.getOrderWiseSizeList();
                }
                else if (formData['MC_ORDER_H_ID_LST']) {
                    //alert(formData['MC_ORDER_H_ID_LST']);
                    vm.orderIds = formData['MC_ORDER_H_ID_LST'].join(",");

                    vm.getOrderWiseColorList();
                    vm.getOrderWiseSizeList();
                }
                //alert(vm.orderIds);
                


                return MrcDataService.GetAllOthers('/api/mrc/Order/OrderWiseItemList/null/' + (vm.orderIds||"0")).then(function (res) {

                    $scope.orderWiseAllItemList = res;
                    

                    var MC_STYLE_H_ID = formData[key] ? formData['MC_STYLE_H_ID'] : vm.form.MC_STYLE_H_ID;
                    var uniqueItems = vm.uniqueOrderItem(res);

                    //alert(MC_STYLE_H_ID);
                    //console.log(res);
                    //console.log(uniqueItems);
                    //if (vm.form.HAS_SET == 'Y') {
                    vm.orderWiseItemList = _.filter(uniqueItems, function (ob) {
                        if (ob.COMBO_NO != '') {
                            ob.ITEM_NAME_EN = ob.ITEM_NAME_EN + ' ' + ob.MODEL_NO + ' ' + ob.COMBO_NO;
                        }
                        else {
                            ob.ITEM_NAME_EN = ob.ITEM_NAME_EN + ' ' + ob.MODEL_NO;
                        }
                        return ob.MC_STYLE_H_ID == parseInt(MC_STYLE_H_ID) && ob.PARENT_ID == null;
                    });

                    //console.log(vm.orderWiseItemList);

                    vm.ITEM_MODEL_LST = _.map(vm.orderWiseItemList, 'MC_STYLE_D_ITEM_ID');
                    vm.form.STYLE_D_ITEM_LST = vm.ITEM_MODEL_LST;

                    //}
                    //else {
                    //    vm.orderWiseItemList = res;
                    //}

                    
                    vm.styleDitemList();
                    
                    if (formData[key]) {
                        $timeout(function () {                            
                            //console.log(formData);
                            vm.form = formData[key] ? formData : { BLK_FAB_REQ_DT: vm.today, PCUT_WSTG: 5 };
                        }, 1000);
                    }
                    

                }, function (err) {
                    console.log(err);
                });
            }
            
        };

        vm.uniqueOrderItem = function (res) {
            return _.map(_.groupBy(res, function (doc) {
                return doc.MODEL_NO;
            }), function (grouped) {
                return grouped[0];
            });
        }

        vm.styleDitemList = function () {
            console.log(formData['STYLE_D_ITEM_LST']);
            var itmCatList = formData[key] ? formData['STYLE_D_ITEM_LST'] : vm.form.STYLE_D_ITEM_LST;
            //console.log('test');
            //console.log(itmCatList);


            $scope.STYLE_D_ITEM_LST = itmCatList;
            $scope.modelWiseAllItemList = _.filter($scope.orderWiseAllItemList, function (ob) {                
                return _.some(itmCatList, function (val) {                    
                    return ob.PARENT_ID == null || ob.PARENT_ID == val;
                });
            });

            var YD_ITEM_LIST = _.filter($scope.orderWiseAllItemList, function (ob) {
                return ob.IS_YD == 'Y';
            });
            if (YD_ITEM_LIST.length > 0) {
                vm.form.IS_YD = 'Y';
            }
            else {
                vm.form.IS_YD = 'N';
            }
        };

        vm.getOrderWiseSizeList = function () {
            if (vm.orderIds != null) {
                return MrcDataService.GetAllOthers('/api/mrc/Order/MultiOrderWiseSizeList/' + vm.orderIds).then(function (res) {
                    $scope.orderWiseSizeList = _.sortBy(res, 'MC_SIZE_GRP_ID', 'DISPLAY_ORDER desc', function (ob) {
                        return ob;
                    });
                }, function (err) {
                    console.log(err);
                });
            }
            else {
                $scope.orderWiseSizeList = [];
            }
        } 

        vm.getOrderWiseColorList = function () {
            if (vm.orderIds != null) {
                return MrcDataService.GetAllOthers('/api/mrc/Order/MultiOrderWiseColorList/' + (vm.orderIds||"0")).then(function (res) {
                    $scope.orderWiseColorList = res;
                }, function (err) {
                    console.log(err);
                });
            }
            else {
                $scope.orderWiseColorList = [];
            }
        };
               
        vm.getFabDataByStyleId = function (MC_STYLE_H_ID) {
            return MrcDataService.GetAllOthers('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + MC_STYLE_H_ID + '?pIS_ACTIVE=Y').then(function (res) {
                $scope.styleWiseFabListData = res;              
            }, function (err) {
                console.log(err);
            });
        };

        vm.getTapeItemDataByStyleId = function (MC_STYLE_H_ID) {
            return MrcDataService.GetAllOthers('/api/mrc/StyleDBOM/TapeItemList/' + MC_STYLE_H_ID).then(function (res) {
                $scope.styleWiseTapeItemListData = res;
            }, function (err) {
                console.log(err);
            });
        };
        
        //vm.nextState = function () {
        //    $state.go('BulkFabBkEntry.Dtl');
        //};

        $scope.workStyleAuto = function (val) {            
            var workStyles = _.filter(vm.workStyleList, function (ob) {
                return _.startsWith(ob.WORK_STYLE_NO.toUpperCase(), val.toUpperCase());
            });
            return workStyles;
        };

        $scope.onSelectWorkStyle = function (item) {
            //console.log(item);    
            //console.log(vm.orderListData);
            vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
            vm.form.STYLE_DESC = item.STYLE_DESC;
            vm.form.BUYER_NAME_EN = item.BUYER_NAME_EN;
            vm.form.HAS_SET = item.HAS_SET;
            $scope.HAS_SET = item.HAS_SET;
            vm.form.JOB_TRAC_NO = item.JOB_TRAC_NO;
            vm.form.RF_CURRENCY_ID = item.RF_CURRENCY_ID;
            
            vm.getFabDataByBuyerAccId();
            vm.workStyleWiseOrderList = _.filter(vm.orderListData, { 'WORK_STYLE_NO': item.WORK_STYLE_NO });
        }
                   

        vm.lsitView = function () {
            $state.go('BulkFabBkList', { pMC_BYR_ACC_ID: vm.form.MC_BYR_ACC_ID, pMC_STYLE_H_ID: vm.form.MC_STYLE_H_EXT_ID });
        };

        vm.cancel = function () {
            vm.errors = undefined;
            $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: 0 }, { inherit: false });
        };

        vm.bookingProcess = function (data, token) {
            vm.showSplash = true;
            
            var x = confirm("Are you want to process?");
            if (x == false) {
                vm.showSplash = false;
                return;
            }
            else {
                return MrcDataService.processBulkFabBooking(data, token).then(function (res) {
                    vm.errors = undefined;
                    if (res.success === false) {
                        vm.errors = [];
                        vm.errors = res.errors;
                        vm.showSplash = false;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        //console.log(res['data']);
                        //if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            config.appToastMsg(res.data.PMSG);
                            alert(res.data.PMSG.substr(9));
                        //};

                        vm.showSplash = false;
                    }

                });
            }
        };

        vm.printBookingRecord = function (pREVISION_NO) {
            //console.log(dataItem);

            vm.form.REPORT_CODE = 'RPT-2001';
            vm.form.PAGE_SIZE_NAME = vm.PAGE_SIZE_NAME;
            vm.form.MC_BLK_REVISION_NO = pREVISION_NO;// vm.form.REVISION_NO;

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

        //$scope.childData = function (data) {
        //    console.log(data);
        //};

        //$scope.fabReqDtl = new kendo.data.ObservableArray([]);
        vm.submitData = function (data, token) {
            vm.errors = [];

            if (vm.errors != undefined && vm.errors.length > 0) {
                return;
            }
            else {
                vm.errors = undefined;
            }

            if ($stateParams.pIS_COPY == 'Y') {
                data['MC_BLK_FAB_REQ_H_ID'] = 0;
                data['REVISION_NO'] = null;
                data['REVISION_DT'] = null;
                data['REV_REASON'] = null;

                angular.forEach($scope.fabReqDtl._data, function (val, key) {
                    val['MC_BLK_FAB_REQ_D_ID'] = 0;
                });

                $scope.processConsData._data = [];
            }

            var obj = angular.copy(data);

            //console.log(data);
            obj.BLK_FAB_REQ_DT = $filter('date')(obj.BLK_FAB_REQ_DT, vm.dtFormat);
            obj.REVISION_DT = $filter('date')(obj.REVISION_DT, vm.dtFormat);
            //obj.PCUT_WSTG = obj.PCUT_WSTG * 100;

            if (obj.MC_ORDER_H_ID_LST == null) {
                obj.MC_ORDER_H_ID_LST = vm.MC_ORDER_LIST;
            }

            if (obj.STYLE_D_ITEM_LST == null) {
                obj.STYLE_D_ITEM_LST = vm.ITEM_MODEL_LST;
            }

            if (obj.MC_ORDER_H_ID_LST != null) {
                obj.MC_ORDER_H_ID_LST = obj.MC_ORDER_H_ID_LST.join(',');
            }
            if (obj.STYLE_D_ITEM_LST != null) {
                obj.STYLE_D_ITEM_LST = obj.STYLE_D_ITEM_LST.join(',');
            }
            
            
            console.log($scope.processConsData);

            obj.fabReqDtl = $scope.fabReqDtl._data;
            obj.MC_BLK_FAB_REQ_D_XML = MrcDataService.xmlStringShort(obj.fabReqDtl.map(function (ob) {
                return ob;
            }));
            obj.MC_BLK_FAB_REQ_CAL_XML = MrcDataService.xmlStringShort($scope.processConsData._data.map(function (ob) {
                return { MC_BLK_FAB_REQ_CAL_ID: ob.MC_BLK_FAB_REQ_CAL_ID, FIN_FAB_QTY: ob.FIN_FAB_QTY, GREY_FAB_QTY: ob.GREY_FAB_QTY };
            }));

            //data.fabReqDtl = vm.obGrid;
            //vm.copyOrder = data;
            console.log(obj);
            //return;

            return MrcDataService.batchSaveBulkFabBooking(obj, token).then(function (res) {
                //vm.showSplash = true;
                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                    //vm.showSplash = false;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    //console.log(res['data']);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {                        
                        vm.isSaved = true;
                    };

                    if (res.data.PMC_BLK_FAB_REQ_H_ID_RTN != 0) {

                        vm.form.MC_BLK_FAB_REQ_H_ID = res.data.PMC_BLK_FAB_REQ_H_ID_RTN;
                        vm.form.BLK_FAB_REQ_NO = res.data.PBLK_FAB_REQ_NO_RTN;

                        vm.bulkFabBookingDtlList(res.data.PMC_BLK_FAB_REQ_H_ID_RTN);
                        //MrcDataService.bulkFabBookingDtlList(res.data.PMC_BLK_FAB_REQ_H_ID_RTN, null).then(function (result) {

                        //});
                        //$state.go($state.current, $stateParams.current, { reload: true });
                        if (vm.IS_COPY == 'Y') {
                            $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: res.data.PMC_BLK_FAB_REQ_H_ID_RTN }, { inherit: false });
                        }
                        else {
                            $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: res.data.PMC_BLK_FAB_REQ_H_ID_RTN }, { reload: 'BulkFabBkEntry.Dtl' });
                        }
                        //BulkFabBkEntry.Dtl({ pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID })
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };


        vm.bulkFabBookingDtlList = function (pMC_BLK_FAB_REQ_H_ID) {
            return MrcDataService.GetAllOthers('/api/mrc/BulkFabBk/BulkFabBookingDtlList/' + pMC_BLK_FAB_REQ_H_ID).then(function (res) {                
                $scope.fabReqDtl = _.filter(res, function (ob) {
                    return true;
                });

                //$state.go('')

            }, function (err) {
                console.log(err);
            });
        };
       
        vm.bookingFinalize = function (data, token) {
            Dialog.confirm('After finalize, the booking related order# & fabrics booking record is lock.</br></br> <b>Do you want to finalize?</b>', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/mrc/BulkFabBk/SaveBulkFabFinalize',
                         method: 'post',
                         params: { pMC_BLK_FAB_REQ_H_ID: data.MC_BLK_FAB_REQ_H_ID }
                     })
                     .then(function (res) {
                         //$scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             //$scope.$parent.errors = [];
                             //$scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 vm.form.IS_BFB_FINALIZED = 'Y';                                 
                             };
                             
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });
        };

        vm.send4BulkBudgetAprvl = function (pMC_BLK_FAB_REQ_H_ID, token) {
            Dialog.confirm('<b>Are you sure, send for verification?</b>', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/mrc/BulkFabBk/Send4BulkBudgetAprvl',
                         method: 'post',
                         params: { pMC_BLK_FAB_REQ_H_ID: pMC_BLK_FAB_REQ_H_ID }
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
                             //    vm.form.IS_BFB_FINALIZED = 'Y';
                             //};

                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
        };


    }

})();





// Start Process Loss Controller
(function () {
    'use strict';

    angular.module('multitex.mrc').controller('ProcessLossModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'mcBlkFabReqHID', 'MrcDataService', ProcessLossModalController]);
    function ProcessLossModalController($modalInstance, $q, $scope, $http, $state, $filter, config, mcBlkFabReqHID, MrcDataService) {

        $scope.showSplash = true;
        $scope.MC_BLK_FAB_REQ_H_ID = angular.copy(mcBlkFabReqHID);
        //$scope.plDataList = new kendo.data.ObservableArray([]);

        activate();
       
        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;


        function activate() {
            var promise = [getProcessLoss()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                $scope.showSplash = false;
            });
        }

        
        $scope.plColumns = [
            { field: "COLOR_NAME_EN", title: "Color", type: "string", width: '170px' },
            { field: "PLOSS_PCT", title: "CK P.Loss(%)", type: "string", editable: true, width: '80px' },
            { field: "PLOSS_PCT_FKNIT", title: "FK P.Loss(%)", type: "string", editable: true, width: '80px' },
            { field: "YD_PLOSS_PCT", title: "YD Loss(%)", type: "string", editable: true, width: '100px', hidden: true },
            { field: "AOP_PLOSS_PCT", title: "AOP Loss(%)", type: "string", editable: true, width: '100px', hidden: true }
            //{
            //    title: "Action",
            //    template: function () {
            //        return "<a class='btn btn-xs blue' ui-sref='BulkFabBkEntry.Dtl({pMC_BLK_FAB_REQ_H_ID:dataItem.MC_BLK_FAB_REQ_H_ID})' ><i class='fa fa-edit'> Edit</i></a>            <a ng-click='vm.printBookingRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-print'> Print</i></a>";
            //    },
            //    width: "200px"
            //}
        ];

        ////////////////////////////////////////////////////
        function getProcessLoss() {

            return MrcDataService.GetAllOthers('/api/mrc/BulkFabBk/ProcessLossColorList/' + $scope.MC_BLK_FAB_REQ_H_ID).then(function (res) {
                $scope.plDataList = new kendo.data.DataSource({
                    data: res
                });                
                
            }, function (err) {
                console.log(err);
            });
            
        };



        $scope.save = function (token, valid) {
            
            if (!valid) return;

            var obj = {};// = { MC_BLK_FAB_REQ_H_ID: $scope.MC_BLK_FAB_REQ_H_ID };// = angular.copy(data);
            obj.MC_BLK_FAB_PLOSS_XML = MrcDataService.xmlStringShort($scope.plDataList._data.map(function (ob) {
                return ob;
            }));                                   

            //data.fabReqDtl = vm.obGrid;
            //vm.copyOrder = data;
            console.log(obj);

            return MrcDataService.batchSaveBulkFabPL(obj, token).then(function (res) {
                //vm.showSplash = true;
                $scope.errors = undefined;
                if (res.success === false) {
                    $scope.errors = [];
                    $scope.errors = res.errors;
                    //vm.showSplash = false;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    //console.log(res['data']);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getProcessLoss();
                        $scope.isSaved = true;

                        $modalInstance.close(res.data);
                        $scope.cancel();
                    };

                    //if (res.data.PMC_BLK_FAB_REQ_H_ID_RTN != 0) {

                    //}
                    config.appToastMsg(res.data.PMSG);                    
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
                        
        };

        
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();
// End Process Loss Controller





// Start Report Row Category Controller
(function () {
    'use strict';

    angular.module('multitex.mrc').controller('RowCategoryModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'mcBlkFabReqHID', 'MrcDataService', RowCategoryModalController]);
    function RowCategoryModalController($modalInstance, $q, $scope, $http, $state, $filter, config, mcBlkFabReqHID, MrcDataService) {

        $scope.showSplash = true;
        $scope.MC_BLK_FAB_REQ_H_ID = angular.copy(mcBlkFabReqHID);
        //$scope.plDataList = new kendo.data.ObservableArray([]);

        activate();

        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;


        function activate() {
            var promise = [getRowCategory()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                $scope.showSplash = false;
            });
        }


        $scope.rowCategoryColumns = [
            { field: "REQ_FAB_TYPE_NAME", title: "Category", type: "string", width: '100px', hidden: true },
            { field: "MC_ROW_CAT_NAME", title: "Color", type: "string" },            
            {
                title: "Display Order",
                template: function () {
                    return "<input type='text' ng-model='dataItem.MC_ROW_CAT_ID' name='gray-{{dataItem.uid}}' class='form-control' />";
                },
                width: "100px"
            }
        ];

        ////////////////////////////////////////////////////
        function getRowCategory() {

            return MrcDataService.GetAllOthers('/api/mrc/BulkFabBk/ReportRowCategoryList/' + $scope.MC_BLK_FAB_REQ_H_ID).then(function (res) {
                $scope.ReportRowCategoryDataList = new kendo.data.DataSource({
                    data: res
                });

            }, function (err) {
                console.log(err);
            });

        };



        $scope.save = function (token, valid) {

            if (!valid) return;

            var obj = {};// = { MC_BLK_FAB_REQ_H_ID: $scope.MC_BLK_FAB_REQ_H_ID };// = angular.copy(data);
            obj.MC_BLK_FAB_DISPLAY_ORDER_XML = MrcDataService.xmlStringShort($scope.ReportRowCategoryDataList._data.map(function (ob) {
                return ob;
            }));

            //data.fabReqDtl = vm.obGrid;
            //vm.copyOrder = data;
            //console.log(obj);

            return MrcDataService.batchSaveBulkFabRptDisplayOrder(obj, token).then(function (res) {
                //vm.showSplash = true;
                $scope.errors = undefined;
                if (res.success === false) {
                    $scope.errors = [];
                    $scope.errors = res.errors;
                    //vm.showSplash = false;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    //console.log(res['data']);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {                        
                        $scope.isSaved = true;
                    };

                    //if (res.data.PMC_BLK_FAB_REQ_H_ID_RTN != 0) {

                    //}
                    config.appToastMsg(res.data.PMSG);

                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

            $modalInstance.close(data);
        };


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();
// End Report Row Category Controller




// Start Process Collar/Cuff Measurement Controller
(function () {
    'use strict';

    angular.module('multitex.mrc').controller('CollarCuffMeasModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'styleID', 'MrcDataService', CollarCuffMeasModalController]);
    function CollarCuffMeasModalController($modalInstance, $q, $scope, $http, $state, $filter, config, styleID, MrcDataService) {

        $scope.showSplash = true;
        //$scope.MC_BLK_FAB_REQ_H_ID = angular.copy(mcBlkFabReqHID);
        //$scope.plDataList = new kendo.data.ObservableArray([]);

        activate();

        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;


        function activate() {
            var promise = [getOrderScmList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                $scope.showSplash = false;
            });
        }


        $scope.collarCuffMeasColumns = [                       
            { field: "SIZE_CODE", title: "Size", type: "string", width: '150px' },
            {
                title: "Measurement",
                template: function () {
                    return "<input type='text' ng-model='dataItem.MEASUREMENT' name='MEASUREMENT-{{dataItem.uid}}' class='form-control' />";
                },
                width: "150px"
            }
        ];

        

        ////////////////////////////////////////////////////
        function getCollarCuffMeas() {
            $scope.GMTPart = [{ RF_GARM_PART_ID: 10, GARM_PART_NAME: 'Collar' }, { RF_GARM_PART_ID: 12, GARM_PART_NAME: 'Cuff' }];

            return MrcDataService.GetAllOthers('/api/mrc/BulkFabBk/CollarCuffMeasList/' + $scope.MC_BLK_FAB_REQ_H_ID).then(function (res) {
                //$scope.orderSize = res;

                $scope.uniqueOrderSize = _.map(_.groupBy(res, function (doc) {
                    return doc.SIZE_CODE;
                }), function (grouped) {
                    return grouped[0];
                });

                angular.forEach($scope.GMTPart, function (val, key) {
                    $scope.GMTPart[key].orderSize = _.filter(res, function (ob) {                        
                        return ob.RF_GARM_PART_ID == val['RF_GARM_PART_ID'];
                    });
                });
                

                $scope.collarCuffMeasDataList = new kendo.data.DataSource({
                    data: res
                });

            }, function (err) {
                console.log(err);
            });

        };


        //==========================
        function getOrderScmList() {
            return $scope.OrderScmList = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        //var pm = MrcDataService.kFilterStr2QueryParam(params.filter);

                        MrcDataService.getDataByFullUrl('/api/mrc/Order/OrderListForCollarCuff/' + params.page + '/' + params.pageSize + '?pMC_STYLE_H_ID=' + styleID).then(function (res) {

                            var list = _.map(res['data'], function (o) {
                                o['GMTPart'] = [];
                                o['itmGmtPart'] = [];
                                o['uniqueOrderSize'] = [];
                                o['IS_FINALIZED_CC'] = null;
                                o['IS_FIRST'] = 0;
                                o['FINALIZED'] = 0;
                                return o;
                            });
                            res['data'] = list;
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: "total"
                }
            });

        }

        

        $scope.gridOptions = {
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
            height: '500px',
            scrollable: true,
            sortable: true,
            columns: [
                { field: "MC_STYLE_H_ID", hidden: true },
                { field: "SZ_RANGE", hidden: true },                
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "20%" },
                { field: "STYLE_DESC", title: "Style Name", width: "20%" },
                { field: "STYLE_NO", title: "Style #", width: "15%" },
                { field: "IS_FINALIZED", title: "Finalize?", width: "10%" },
                //{ field: "ORDER_NO", title: "Order #", width: "15%" },
                //{ field: "SHIP_DT", title: "Shipment Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "10s%" },
                //{
                //    title: "Add Measurement",
                //    template: '<a  title="Edit" ng-click="vm.addCollarCuffMeasurement(dataItem.MC_STYLE_H_ID, dataItem.SZ_RANGE)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>  <a  title="Info" ng-if="dataItem.IS_FINALIZED==' + "'Y'" + '" class="btn btn-xs yellow-gold"><i class="fa fa-info">  Revise</i></a>',
                //    width: "15%"
                //}
            ]
        };

        $scope.detailExpand = function (evData) {

            if (evData) {

                var p1 = evData.MC_STYLE_H_ID;
                var p2 = (evData.SZ_RANGE || "0");
                evData.IS_FINALIZED_CC = null;

                ////vm.vmUI = ev;
                ////var GMTPart = [{ RF_GARM_PART_ID: 10, GARM_PART_NAME: 'Collar' }, { RF_GARM_PART_ID: 12, GARM_PART_NAME: 'Cuff' }];

                MrcDataService.getDataByFullUrl('/api/common/GmtPartListByStyle?pMC_STYLE_H_ID=' + p1).then(function (resgp) {

                    var GMTPart = resgp;
                    MrcDataService.getDataByFullUrl('/api/mrc/SizeMaster/GetStyleSizeList/' + p1 + '/' + p2).then(function (res) {
                        console.log(res);
                        var count = _.filter(res, function (o) { return o.MEAS_LENGTH > 0 }).length;
                        var final = _.filter(res, function (o) { return o.IS_FINALIZED == 'Y' }).length;

                        //var abcArry = res;
                        var uniqueOrderSize = _.map(_.groupBy(res, function (doc) {
                            return doc.SIZE_CODE;
                        }), function (grouped) {
                            return grouped[0];
                        });

                        var itmGmtPart = [];
                        angular.forEach(GMTPart, function (val, key) {
                            itmGmtPart[key] = _.filter(res, function (ob) {
                                return ob.RF_GARM_PART_ID == val['RF_GARM_PART_ID'];
                            });

                        });

                        evData['IS_FIRST'] = count;
                        evData['FINALIZED'] = final;
                        evData['IS_FINALIZED_CC'] = 'N';
                        evData['GMTPart'] = GMTPart;
                        evData['itmGmtPart'] = itmGmtPart;
                        evData['uniqueOrderSize'] = uniqueOrderSize;
                        console.log(evData);

                    }, function (err) {
                        console.log(err);
                    });
                }, function (err) {
                    console.log(err);
                });


            }
        }
        //==========================



        $scope.save = function (token, valid) {

            if (!valid) return;

            var obj = {};
            //obj.MC_BLK_COL_REQ_XML = MrcDataService.xmlStringShort($scope.collarCuffMeasDataList._data.map(function (ob) {
            //    return ob;
            //}));

            var collarCuffData = [];
            angular.forEach($scope.GMTPart, function (val, key) {
                collarCuffData = collarCuffData.concat(val['orderSize']);
            });


            obj.MC_BLK_COL_REQ_XML = MrcDataService.xmlStringShort(collarCuffData);


            //data.fabReqDtl = vm.obGrid;
            //vm.copyOrder = data;
            //console.log(obj);

            return MrcDataService.batchSaveBulkFabCollarCuff(obj, token).then(function (res) {
                //vm.showSplash = true;
                $scope.errors = undefined;
                if (res.success === false) {
                    $scope.errors = [];
                    $scope.errors = res.errors;
                    //vm.showSplash = false;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    //console.log(res['data']);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        getCollarCuffMeas();
                        $scope.isSaved = true;
                    };
                    
                    config.appToastMsg(res.data.PMSG);

                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

            $modalInstance.close(data);
        };


        $scope.cancel = function () {            
            $modalInstance.dismiss('cancel');
        };
    }

})();
// End Process Collar/Cuff Measurement Controller



// Start Booking Revision Modal Controller
(function () {
    'use strict';

    angular.module('multitex.mrc').controller('BookingRevisionModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'mcBlkFabReqHID', 'MrcDataService', 'Dialog', BookingRevisionModalController]);
    function BookingRevisionModalController($modalInstance, $q, $scope, $http, $state, $filter, config, mcBlkFabReqHID, MrcDataService, Dialog) {

        $scope.showSplash = true;

        activate();

        $scope.today = new Date();
        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.REVISION_DT = $scope.today;

        var blkBookID = angular.copy(mcBlkFabReqHID);

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

            //$scope.$parent.errors = [];

            //if ($scope.$parent.errors != undefined && $scope.$parent.errors.length > 0) {
            //    return;
            //}
            //else {
            //    $scope.$parent.errors = undefined;
            //}

            var submitData = { MC_BLK_FAB_REQ_H_ID: blkBookID, REVISION_DT: $filter('date')($scope.REVISION_DT, $scope.dtFormat), REV_REASON: $scope.REV_REASON };

            Dialog.confirm('Do you want to revision?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/mrc/BulkFabBk/SaveBlkBookingRevision',
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

                             if (res.data.PREVISION_NO_RTN > 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 //$scope.REVISION_NO = res.data.PREVISION_NO_RTN;
                                 submitData['REVISION_NO'] = res.data.PREVISION_NO_RTN;
                                 submitData['IS_BFB_FINALIZED'] = "N";
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
// End Booking Revision Modal Controller




// Detail Controller
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('bulkFabBkDtlController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$timeout', '$state', '$stateParams', 'MrcDataService', bulkFabBkDtlController]);
    function bulkFabBkDtlController(logger, config, $q, $scope, $http, exception, $filter, $timeout, $state, $stateParams, MrcDataService) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        //var key = 'MC_BLK_FAB_REQ_H_ID';
        vm.today = new Date();
        //vm.form = { BLK_FAB_REQ_DT: vm.today, PCUT_WSTG: 0.0005 };
        //vm.formDtl = formData[key] ? formData : {
        //    MC_BLK_FAB_REQ_D_ID: 0, MC_BLK_FAB_REQ_H_ID: 0, STYLE_D_ITEM_LST: '', STYLE_D_ITM_CAT_LST: '',
        //    MC_COLOR_LST: '', COMBO_NO_LST: '', MC_SIZE_LST: '', FAB_COLOR_LST: '', COLOR_SPEC: '', PCUT_WSTG: 0.0005
        //};

        //vm.INV_ITEM_CAT_LIST = [{INV_ITEM_CAT_ID:7, }];

        vm.formDtl = {
            MC_BLK_FAB_REQ_D_ID: 0, MC_BLK_FAB_REQ_H_ID: 0, STYLE_D_ITEM_LST: '', STYLE_D_ITM_CAT_LST: '', IS_CONTRAST: 'N',
            MC_COLOR_LST: '', COMBO_NO_LST: '', MC_SIZE_LST: '', FAB_COLOR_LST: '', COLOR_SPEC: '', PCUT_WSTG: 5, IS_ALL_ORDER: 'Y'
        };


        vm.bulkFabBkDtlState = ($state.current.name === 'BulkFabBkEntry.Dtl') ? true : false;
        vm.processConsDtlState = ($state.current.name === 'BulkFabBkEntry.ProcessConsData') ? true : false;

        vm.tabIsActive = false;
        
        vm.defaultDiaMouID = 23;
        vm.defaultQtyMouID = 5;
        vm.defaultConsMouID = 3;
        vm.defaultGmtPart = ["1"];
        vm.defaultGmtItemPart = ["234"];
        vm.defaultFabGrp = 193;
        vm.defaultDiaType = 327;
        
        vm.kgMouId = 3;
        vm.ydMouId = 8;

        
        vm.isAddToGrid = 'Y';
        vm.updateGridIndex = null;

        vm.tabs = [
            { title: 'Consumption Entry', content: "<h3>Dynamic content 1</h3>", disabled: false, active: true },
            { title: 'Process Data', content: 'Dynamic content 2', disabled: false, active: false }
        ];

        activate();
        function activate() {
            var promise = [getFabCategory(), getGmtPartList(), getFabGrpList(), getMouList(), getDiaTypeList(), getGmtItemPartList(), getDtlList(),
                getFeederType(), getYDType()];
            return $q.all(promise).then(function () {
                
                //vm.formDtl.RF_GARM_PART_LST = vm.defaultGmtPart;
                //vm.formDtl.LK_FBR_GRP_ID = vm.defaultFabGrp;                
                vm.formDtl.DIA_MOU_ID = vm.defaultDiaMouID;
                vm.formDtl.LK_DIA_TYPE_ID = vm.defaultDiaType;
                vm.formDtl.QTY_MOU_ID = vm.defaultQtyMouID;
                vm.formDtl.CONS_MOU_ID = vm.defaultConsMouID;                
                //vm.formDtl.STYLE_D_ITM_CAT_LST = vm.defaultGmtItemPart;

                var fabGroupItem = _.filter(vm.fabGrpList, function (ob) { return ob.LOOKUP_DATA_ID == vm.defaultFabGrp; });
                vm.formDtl.FABRIC_GROUP_NAME = fabGroupItem[0].LK_DATA_NAME_EN;

                var consItem = _.filter(vm.mouList, function (ob) { return ob.RF_MOU_ID == vm.defaultConsMouID; });
                vm.formDtl.CONS_DOZ_UNIT_NAME = consItem[0].MOU_CODE;

                var diaMouItem = _.filter(vm.mouList, function (ob) { return ob.RF_MOU_ID == vm.defaultDiaMouID; });
                vm.formDtl.FIN_DIA_UNIT_NAME = diaMouItem[0].MOU_CODE;

                var diaTypeItem = _.filter(vm.diaTypeList, function (ob) { return ob.LOOKUP_DATA_ID == vm.defaultDiaType; });
                vm.formDtl.FIN_DIA_TYPE_NAME = diaTypeItem[0].LK_DATA_NAME_EN;
                
                vm.showSplash = false;
            });
        }
            
        
        

        function getDtlList() {

            $scope.$parent.fabReqDtl = new kendo.data.DataSource({
                data: []                
            });
            $scope.$parent.processConsData = new kendo.data.DataSource({
                data: []
            });

            //console.log($stateParams);
            if ($stateParams.pMC_BLK_FAB_REQ_H_ID != null && parseInt($stateParams.pMC_BLK_FAB_REQ_H_ID) > 0) {
                loadDtlState();
                MrcDataService.GetAllOthers('/api/mrc/BulkFabBk/BulkFabBookingDtlList/' + $stateParams.pMC_BLK_FAB_REQ_H_ID).then(function (res) {
                    $scope.$parent.fabReqDtl = new kendo.data.DataSource({
                        data: res
                    });
                }, function (err) {
                    console.log(err);
                });

                MrcDataService.GetAllOthers('/api/mrc/BulkFabBk/BulkFabProcessConsList/' + $stateParams.pMC_BLK_FAB_REQ_H_ID).then(function (res) {
                    $scope.$parent.processConsData = new kendo.data.DataSource({
                        data: res
                    });
                }, function (err) {
                    console.log(err);
                });
            }
        };

        function loadDtlState() {
            vm.styleWiseFabListData = $scope.$parent.styleWiseFabListData;
            vm.orderWiseColorList = $scope.$parent.orderWiseColorList;
            vm.orderWiseSizeList = $scope.$parent.orderWiseSizeList;

            ///========== Start for Buyer Color List ==============
            if ($scope.$parent.MC_STYLE_H_ID > 0) {
                MrcDataService.GetAllOthers('/api/mrc/ColourMaster/ColourListByBuyerId/' + $scope.$parent.MC_STYLE_H_ID).then(function (res) {
                    vm.buyerColorList = res;
                }, function (err) {
                    console.log(err);
                });
            }
            ///========== End for Buyer Color List ==============

            ///========== Start for Item Part =============
            if ($scope.$parent.HAS_SET == 'Y') {
                vm.orderWiseItemPartList = _.filter($scope.$parent.modelWiseAllItemList, function (ob) {
                    return _.some($scope.$parent.STYLE_D_ITEM_LST, function (val) {
                        return ob.PARENT_ID == val;
                    });
                });

                //console.log(vm.orderWiseItemPartList);
            }
            else {
                vm.orderWiseItemPartList = $scope.$parent.modelWiseAllItemList;
            }

            var comboNoList = _.filter(_.map(_.groupBy($scope.$parent.modelWiseAllItemList, function (doc) {
                return doc.COMBO_NO != "";
            }), function (grouped) {
                return grouped[0];
            }), function (obj) {
                return obj.COMBO_NO != "";
            });

            vm.orderWiseComboNoList = [];
            angular.forEach(comboNoList, function (val, key) {
                var cList = val.COMBO_NO.split(',');
                angular.forEach(cList, function (val1, key1) {
                    vm.orderWiseComboNoList.push({ COMBO_NO: val1 });
                });
            });
            ///========== End for Item Part =============
        };
        
        function getFeederType() {
            return MrcDataService.GetAllOthers('/api/common/LookupListData/60').then(function (res) {
                vm.feederTypeList = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getYDType() {
            return MrcDataService.GetAllOthers('/api/common/LookupListData/71').then(function (res) {
                vm.ydTypeList = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getGmtPartList() {
            return MrcDataService.GetAllOthers('/api/common/GmtPartList').then(function (res) {
                vm.gmtPartList = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getGmtItemPartList() {
            return MrcDataService.GetAllOthers('/api/common/LookupListData/47').then(function (res) {
                vm.gmtItemPartList = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getFabGrpList() {
            return MrcDataService.GetAllOthers('/api/common/LookupListData/45').then(function (res) {
                vm.fabGrpList = res;
            }, function (err) {
                console.log(err);
            });            
        };

        function getMouList() {
            return MrcDataService.GetAllOthers('/api/common/MOUList/Y').then(function (res) {
                vm.mouList = res;                
            }, function (err) {
                console.log(err);
            });
        };

        function getDiaTypeList() {
            return MrcDataService.GetAllOthers('/api/common/LookupListData/67').then(function (res) {
                vm.diaTypeList = res;
            }, function (err) {
                console.log(err);
            });            
        };

        function getFabCategory() {
            return MrcDataService.GetAllOthers('/api/inv/ItemCategory/SelectAll').then(function (res) {
                vm.fabCategoryList = _.filter(res, function (ob) {
                    return ob.ITEM_CAT_CODE == 'AT' || ob.ITEM_CAT_CODE == 'FB01' || ob.ITEM_CAT_CODE == 'FB02';
                }).map(function (o) {
                    o.ITEM_CAT_NAME_EN = o.ITEM_CAT_NAME_EN.replace(' Fabric', '');   //replace(o.ITEM_CAT_NAME_EN, 'Fabric', '');
                    return o;
                });
                
                vm.formDtl.INV_ITEM_CAT_ID = vm.fabCategoryList[0].INV_ITEM_CAT_ID;
            }, function (err) {
                console.log(err);
            });
        };
        
        vm.buyerColorList = new kendo.data.ObservableArray([]);
        $scope.$watch('$parent.MC_STYLE_H_ID', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {         
                    return MrcDataService.GetAllOthers('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + parseInt(newVal)).then(function (res) {
                        vm.buyerColorList = res;
                    }, function (err) {
                        console.log(err);
                    });                    
                }
            }
        }, true);
        

        $scope.$watch('$parent.styleWiseTapeItemListData', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                vm.tapeItemList = newVal;                
            }
        }, true);


        $scope.$watch('$parent.styleWiseFabListData', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                vm.fabListData = newVal;
                //vm.styleWiseFabListData = newVal;
                //console.log(newVal);
                vm.styleWiseFabListData = _.filter(vm.fabListData, function (ob) {
                    return ob.INV_ITEM_CAT_ID == vm.formDtl.INV_ITEM_CAT_ID;
                });
            }
        }, true);
     

        //$scope.STYLE_D_ITEM_LST
        $scope.$watch('$parent.modelWiseAllItemList', function (newVal, oldVal) {
            //alert('dd');
            if (!_.isEqual(newVal, oldVal)) {
                                                
                if ($scope.$parent.HAS_SET == 'Y') {                    
                    vm.orderWiseItemPartList = _.filter(newVal, function (ob) {
                        return _.some($scope.$parent.STYLE_D_ITEM_LST, function (val) {
                            //if (ob.IS_YD == 'Y') {
                            //    vm.formDtl.IS_YD = 'Y';
                            //}
                            return ob.PARENT_ID == val;
                        });
                    });                                        
                }
                else {
                    vm.orderWiseItemPartList = newVal;                    
                }

                var comboNoList = _.filter(_.map(_.groupBy(newVal, function (doc) {
                    return doc.COMBO_NO != "";
                }), function (grouped) {
                    return grouped[0];
                }), function (obj) {
                    return obj.COMBO_NO != "";
                });

                vm.orderWiseComboNoList = [];
                angular.forEach(comboNoList, function (val, key) {
                    var cList = val.COMBO_NO.split(',');
                    angular.forEach(cList, function (val1, key1) {
                        vm.orderWiseComboNoList.push({ COMBO_NO: val1 });
                    });
                });
                

                //var isYD = _.some(vm.orderWiseItemPartList, {'IS_YD':'Y'} )
                //vm.formDtl.IS_YD = isYD ? 'Y' : 'N';

                //console.log(vm.orderWiseItemPartList);
                //console.log(vm.orderWiseComboNoList);
            }
        }, true);
        
        $scope.$watch('$parent.orderWiseColorList', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                vm.orderWiseColorList = newVal;
                //console.log(newVal);
            }
        }, true);

        $scope.$watch('$parent.orderWiseSizeList', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                vm.orderWiseSizeList = newVal;
                //console.log(newVal);
            }
        }, true); 

        $scope.$watch('$parent.PCUT_WSTG', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                vm.formDtl.PCUT_WSTG = newVal;
                //console.log(newVal);
            }
        }, true);

        vm.isAllOrder = function () {
            if (vm.formDtl.IS_ALL_ORDER == 'Y') {
                vm.formDtl.MAP_ORDER_H_ID_LST = []; //_.map(vm.orderWiseColorList, 'MC_COLOR_ID');
                vm.isEnableMapOrderList = false;
            }
            else {
                vm.isEnableMapOrderList = true;
            }
        };

        vm.itmOnChange = function () {            
            console.log(vm.orderWiseItemPartList);
            console.log(vm.formDtl.STYLE_D_ITEM_LST);
            
            var ydItemPartList = [];
            ydItemPartList = _.filter(vm.orderWiseItemPartList, function (ob) {
                return _.some(vm.formDtl.STYLE_D_ITEM_LST, function (val) {
                    if (ob.MC_STYLE_D_ITEM_ID == val && ob.IS_YD == 'Y') {
                        vm.formDtl.LK_YD_TYPE_ID = ob.LK_YD_TYPE_ID;
                    }
                    return ob.MC_STYLE_D_ITEM_ID == val && ob.IS_YD == 'Y';
                });
            });
            
            console.log(ydItemPartList);
           
            if (ydItemPartList.length > 0) {
                vm.formDtl.IS_YD = 'Y';

            }
            else {
                vm.formDtl.IS_YD = 'N';
                vm.formDtl.LK_YD_TYPE_ID = '';
            }
        };

        vm.isAllGmtColor = function () {
            if (vm.formDtl.IS_ALL_COL == 'Y') {
                vm.formDtl.MC_COLOR_LST = _.map(vm.orderWiseColorList, 'MC_COLOR_ID');
                vm.isEnableGmtColorList = true;
            }
            else {
                vm.formDtl.MC_COLOR_LST = [];
                vm.isEnableGmtColorList = false;
            }            
        };

        vm.isAllSize = function () {
            if (vm.formDtl.IS_ALL_SIZE == 'Y') {
                vm.formDtl.MC_SIZE_LST = _.map(vm.orderWiseSizeList, 'MC_SIZE_ID');
                vm.isEnableSizeList = true;
            }
            else {
                vm.formDtl.MC_SIZE_LST = [];
                vm.isEnableSizeList = false;
            }
        };

        vm.isSameAsGmtColor = function () {
            if (vm.formDtl.IS_SAME_GMT_COL == 'Y') {
                vm.formDtl.FAB_COLOR_LST = vm.formDtl.MC_COLOR_LST; //_.map(vm.orderWiseColorList, 'MC_COLOR_ID');
                vm.isEnableFabGmtColorList = true;
            }
            else {
                vm.formDtl.FAB_COLOR_LST = [];
                vm.isEnableFabGmtColorList = false;
            }
        };
        
        vm.gmtColorClose = function () {
            if (vm.formDtl.MC_COLOR_LST != null) {
                if (vm.formDtl.MC_COLOR_LST.length != vm.orderWiseColorList) {
                    vm.formDtl.IS_ALL_COL = 'N';
                }
            }
        };

        vm.gmtSizeClose = function () {
            if (vm.formDtl.MC_SIZE_LST != null) {
                if (vm.formDtl.MC_SIZE_LST.length != vm.orderWiseSizeList) {
                    vm.formDtl.IS_ALL_SIZE = 'N';
                }
            }
        };

        vm.fabColorClose = function () {
            if (vm.formDtl.FAB_COLOR_LST != null) {
                if (vm.formDtl.FAB_COLOR_LST.length != vm.formDtl.MC_COLOR_LST.length) {
                    vm.formDtl.IS_SAME_GMT_COL = 'N';
                }                
            }
        };

        vm.isContrastOnChange = function () {
            
            if (vm.formDtl.IS_CONTRAST == 'Y') {
                /*
                var gmtColList = _.filter(vm.orderWiseColorList, function (ob) {
                    return _.some(vm.formDtl.MC_COLOR_LST, function (val) {
                        return ob.MC_COLOR_ID == val;
                    });
                });
                
                if (gmtColList.length > 0) {
                    vm.formDtl.COLOR_SPEC = vm.formDtl.COLOR_SPEC + "Contrast for " + _.map(gmtColList, 'COLOR_NAME_EN').join(',');
                }
                else {
                    vm.formDtl.COLOR_SPEC = vm.formDtl.COLOR_SPEC + "Contrast for ";
                }
                */

                vm.formDtl.COLOR_SPEC = 'Contrast';
            }
            else {
                //vm.formDtl.COLOR_SPEC.replace('Contrast', '');
                vm.formDtl.COLOR_SPEC = '';
            }
        };


        //vm.gridDtlData = new kendo.data.ObservableArray([]);
        //vm.gridDtlData = $scope.$parent.fabReqDtl;
        vm.gridDtlColumns = [
            { field: "uid", title: "UID", type: "string", hidden: true },
            { field: "MC_BLK_FAB_REQ_D_ID", title: "ID", type: "string", hidden: true },
            { field: "DISPLAY_ORDER", title: "Disp. Order", type: "string", width: '50px' },
            { field: "IS_ALL_ORDER", title: "IS_ALL_ORDER", type: "string", hidden: true },
            { field: "MAP_ORDER_H_ID_LST", title: "Order", type: "string", hidden: true },
            { field: "GARM_PART_NAME_LST", title: "GMT Part", type: "string" },
            { field: "FABRIC_GROUP_NAME", title: "Fab. Group", type: "string", hidden: true },
            { field: "FIN_DIA_DESC", title: "Dia", type: "string" },
            { field: "CONS_DOZ_DESC", title: "Cons/Doz", type: "string" },
            { field: "ITEM_PART_NAME_LST", title: "Item Part", type: "string" },
            { field: "GARM_COLOR_NAME_LST", title: "GMT Color", type: "string" },
            { field: "FAB_COLOR_NAME_LST", title: "Fab. Color", type: "string" },
            { field: "SIZE_NAME_LST", title: "Size", type: "string" },
            {
                title: "Action",
                template: function () {
                    return "<a class='btn btn-xs blue' ng-click='vm.editItem(dataItem)'><i class='fa fa-edit'> Edit</i></a></a>&nbsp;<a ng-click='vm.removeItem(dataItem)' class='btn btn-xs red' ng-disabled='$parent.IS_BFB_FINALIZED==\"Y\"'><i class='fa fa-remove'> Remove</i></a>";
                },
                width: "130px"
            }         
        ];

        vm.gridDtlFilterable = {
            extra: false,
            operators: {
                string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                }
            }
        };

        vm.gridProcessColumns = [
            { field: "uid", title: "UID", type: "string", hidden: true },
            { field: "MC_BLK_FAB_REQ_CAL_ID", title: "ID", type: "string", hidden: true },
            { field: "ITEM_CAT_NAME_EN", title: "Fab. Category", type: "string", editable: false, width: '100px' },
            { field: "COLOR_GRP_NAME_EN", title: "Color Grp", type: "string", editable: false, width: '100px' },
            { field: "FAB_COLOR_NAME", title: "Fab. Color", type: "string", editable: false, width: '250px' },
            { field: "GMT_PART_NAME", title: "GMT Part", type: "string", editable: false, width: '200px' },
            { field: "SIZE_RANGE", title: "Size Range", type: "string", editable: false, width: '100px' },
            { field: "FIN_DIA_DESC", title: "Fin. Dia", type: "string", editable: false },            
            {
                title: "Fin. Qty",
                template: function () {
                    return "<input type='text' ng-model='dataItem.FIN_FAB_QTY' name='gray-{{dataItem.uid}}' class='form-control' ng-disabled='true' />";
                },
                width: "100px"
            },
            {
                title: "Gray Qty",
                template: function () {
                    return "<input type='text' ng-model='dataItem.GREY_FAB_QTY' name='gray-{{dataItem.uid}}' class='form-control' ng-disabled='true' />";
                },
                width: "100px"
            }
        ];
                
        vm.cancelToGrid = function () {            
            //vm.formDtl.DESCRIPTION = '';
            //vm.formDtl.RF_GARM_PART_LST = [];

            vm.formDtl.MC_BLK_FAB_REQ_D_ID = 0;
            vm.formDtl.MC_BLK_FAB_REQ_H_ID = 0;
            //vm.formDtl.FIN_DIA = null;
            //vm.formDtl.CUT_DIA = null;
            //vm.formDtl.CONS_QTY = null;

            vm.isAddToGrid = 'Y';
        };

        vm.updateGridIndex;


        function findGridIndex(index, MC_BLK_FAB_REQ_D_ID) {
            if (index > -1) {
                return index;
            } else {

                return _.findIndex($scope.$parent.fabReqDtl, function (obj) {
                    return parseInt(obj.MC_BLK_FAB_REQ_D_ID) == parseInt(MC_BLK_FAB_REQ_D_ID);
                });
            }

        };

        vm.editItem = function (dataItem) {
            ///console.log($scope.$parent.fabReqDtl);
                        
            vm.isAddToGrid = 'N';
            vm.updateGridIndex = findGridIndex($scope.$parent.fabReqDtl.indexOf(dataItem), dataItem.MC_BLK_FAB_REQ_D_ID);
            
            //alert(vm.updateGridIndex);
            
            var item = angular.copy(dataItem);
            //console.log(item);                       

            if (item.IS_ALL_ORDER == 'Y') {                
                vm.isEnableMapOrderList = false;
            }
            else {
                vm.isEnableMapOrderList = true;
            }

            
            if (item.MAP_ORDER_H_ID_LST != null) {
                item.MAP_ORDER_H_ID_LST = item.MAP_ORDER_H_ID_LST.split(',');                                
            }            
            if (item.RF_GARM_PART_LST != null) {
                item.RF_GARM_PART_LST = item.RF_GARM_PART_LST.split(',');
            }
            if (item.STYLE_D_ITEM_LST != null) {
                item.STYLE_D_ITEM_LST = item.STYLE_D_ITEM_LST.split(',');
            }
            if (item.MC_COLOR_LST != null) {
                item.MC_COLOR_LST = item.MC_COLOR_LST.split(',');
            }
            if (item.MC_SIZE_LST != null) {
                item.MC_SIZE_LST = item.MC_SIZE_LST.split(',');
            }
            if (item.FAB_COLOR_LST != null) {
                item.FAB_COLOR_LST = item.FAB_COLOR_LST.split(',');
            }
            if (item.COMBO_NO_LST != null) {
                item.COMBO_NO_LST = item.COMBO_NO_LST.split(',');
            }
            
            
            console.log(dataItem);
            return newFn(item.INV_ITEM_CAT_ID).then(function () {
                $timeout(function () {
                    vm.formDtl = item;
                }, 400);
                                
            }, function (err) {
                console.error(err)
            })
                      
            
        };

        function newFn(pINV_ITEM_CAT_ID, pMC_STYLE_D_FAB_ID) {
            var deferred = $q.defer();
            var fn = fabCatChange(pINV_ITEM_CAT_ID);
            if (fn.length == 0) {
                deferred.reject('Error');
            } else {
                deferred.resolve(true);
            }

            return deferred.promise;
        }
        
        function fabCatChange(pINV_ITEM_CAT_ID) {
            if (pINV_ITEM_CAT_ID == 7) {                
                return $scope.$parent.styleWiseTapeItemListData;
            }
            else {
                return vm.styleWiseFabListData = _.filter($scope.$parent.styleWiseFabListData, function (ob) {
                    return ob.INV_ITEM_CAT_ID == pINV_ITEM_CAT_ID;
                });
            }
        }

        vm.removeItem = function (dataItem) {            
            //vm.gridDtlData.remove(dataItem);
            //vm.updateGridIndex = findGridIndex($scope.$parent.fabReqDtl.indexOf(dataItem), dataItem.MC_BLK_FAB_REQ_D_ID);
            $scope.$parent.fabReqDtl.remove(dataItem);
        };

        vm.addToGrid = function () {
            //console.log(vm.formDtl);
            
            var mapOrderList = _.filter($scope.$parent.styleWiseOrderList, function (ob) {
                return _.some(vm.formDtl.MAP_ORDER_H_ID_LST, function (val) {
                    return ob.MC_ORDER_H_ID == val;
                });
            });

            var gmtPartList = _.filter(vm.gmtPartList, function (ob) {
                return _.some(vm.formDtl.RF_GARM_PART_LST, function (val) {
                    return ob.RF_GARM_PART_ID == val;
                });
            });

            var itmPartList = _.filter(vm.orderWiseItemPartList, function (ob) {
                return _.some(vm.formDtl.STYLE_D_ITEM_LST, function (val) {
                    return ob.MC_STYLE_D_ITEM_ID == val;
                });
            });

            var gmtColorList = _.filter(vm.orderWiseColorList, function (ob) {
                return _.some(vm.formDtl.MC_COLOR_LST, function (val) {
                    return ob.MC_COLOR_ID == val;
                });
            });

            var fabColorList = _.filter(vm.buyerColorList, function (ob) {
                return _.some(vm.formDtl.FAB_COLOR_LST, function (val) {
                    return ob.MC_COLOR_ID == val;
                });
            });

            var sizeList = _.filter(vm.orderWiseSizeList, function (ob) {
                return _.some(vm.formDtl.MC_SIZE_LST, function (val) {
                    return ob.MC_SIZE_ID == val;
                });
            });

            //if (vm.formDtl.SIZE_NAME_LST != null) {
                vm.formDtl.SIZE_NAME_LST = _.map(sizeList, 'SIZE_CODE').join('+');
            //}
            //if (vm.formDtl.GARM_COLOR_NAME_LST != null) {
                vm.formDtl.GARM_COLOR_NAME_LST = _.map(gmtColorList, 'COLOR_NAME_EN').join('+');
                vm.formDtl.FAB_COLOR_NAME_LST = _.map(fabColorList, 'COLOR_NAME_EN').join('+');
            //}
            //if (vm.formDtl.ITEM_PART_NAME_LST != null) {
                vm.formDtl.ITEM_PART_NAME_LST = _.map(itmPartList, 'ITEM_CAT_NAME_EN').join('+');
            //}
            //if (vm.formDtl.GARM_PART_NAME_LST != null) {
                vm.formDtl.GARM_PART_NAME_LST = _.map(gmtPartList, 'GARM_PART_NAME').join('+');
            //}
            vm.formDtl.FIN_DIA_DESC = vm.formDtl.FIN_DIA + ' ' + vm.formDtl.FIN_DIA_UNIT_NAME + ' ' + vm.formDtl.FIN_DIA_TYPE_NAME;
            vm.formDtl.CONS_DOZ_DESC = vm.formDtl.CONS_QTY + ' ' + vm.formDtl.CONS_DOZ_UNIT_NAME;
            

            if (vm.isAddToGrid == 'Y') {
                //vm.gridDtlData.push(vm.formDtl);
                var vDtl = angular.copy(vm.formDtl);

                if ((vDtl.FIN_DIA != '' && vDtl.FIN_DIA != '0') && (vDtl.DIA_MOU_ID == '' || vDtl.DIA_MOU_ID == null)) {
                    vDtl.DIA_MOU_ID = vm.defaultDiaMouID;
                }
                if ((vDtl.FIN_DIA != '' && vDtl.FIN_DIA != '0') && (vDtl.LK_DIA_TYPE_ID == '' || vDtl.LK_DIA_TYPE_ID == null)) {
                    vDtl.LK_DIA_TYPE_ID = vm.defaultDiaType;
                }
                if (vDtl.QTY_MOU_ID == '' || vDtl.QTY_MOU_ID == null) {
                    vDtl.QTY_MOU_ID = vm.defaultQtyMouID;
                }
                if (vDtl.CONS_MOU_ID == '' || vDtl.CONS_MOU_ID == null) {
                    vDtl.CONS_MOU_ID = vm.defaultConsMouID;
                }

                if (vDtl.MAP_ORDER_H_ID_LST != null) {
                    vDtl.MAP_ORDER_H_ID_LST = vDtl.MAP_ORDER_H_ID_LST.join(',');
                }

                vDtl.RF_GARM_PART_LST = vDtl.RF_GARM_PART_LST.join(',');
                vDtl.STYLE_D_ITEM_LST = vDtl.STYLE_D_ITEM_LST.join(',');
                vDtl.MC_COLOR_LST = vDtl.MC_COLOR_LST.join(',');
                vDtl.MC_SIZE_LST = vDtl.MC_SIZE_LST.join(','); 
                vDtl.FAB_COLOR_LST = vDtl.FAB_COLOR_LST.join(',');
                //vDtl.STYLE_D_ITM_CAT_LST = vDtl.STYLE_D_ITM_CAT_LST.join(',');
                if (vDtl.COMBO_NO_LST != null) {
                    if (vDtl.COMBO_NO_LST.length > 0) {
                        vDtl.COMBO_NO_LST = vDtl.COMBO_NO_LST.join(',');
                    }
                }
                vDtl.PCUT_WSTG = vDtl.PCUT_WSTG;

                

                $scope.$parent.fabReqDtl.insert(vDtl);
            }
            else {
                //alert(vm.updateGridIndex);
                //vm.gridDtlData[vm.updateGridIndex] = vm.formDtl;
                var vDtl = angular.copy(vm.formDtl);

                if ((vDtl.FIN_DIA != '' && vDtl.FIN_DIA != '0') && (vDtl.DIA_MOU_ID == '' || vDtl.DIA_MOU_ID == null)) {
                    vDtl.DIA_MOU_ID = vm.defaultDiaMouID;                    
                }
                if ((vDtl.FIN_DIA != '' && vDtl.FIN_DIA != '0') && (vDtl.LK_DIA_TYPE_ID == '' || vDtl.LK_DIA_TYPE_ID == null)) {
                    vDtl.LK_DIA_TYPE_ID = vm.defaultDiaType;
                }
                if (vDtl.QTY_MOU_ID == '' || vDtl.QTY_MOU_ID == null) {
                    vDtl.QTY_MOU_ID = vm.defaultQtyMouID;
                }
                if (vDtl.CONS_MOU_ID == '' || vDtl.CONS_MOU_ID == null) {
                    vDtl.CONS_MOU_ID = vm.defaultConsMouID;
                }

                if (vDtl.MAP_ORDER_H_ID_LST != null) {
                    vDtl.MAP_ORDER_H_ID_LST = vDtl.MAP_ORDER_H_ID_LST.join(',');
                }
                
                vDtl.RF_GARM_PART_LST = vDtl.RF_GARM_PART_LST.join(',');
                vDtl.STYLE_D_ITEM_LST = vDtl.STYLE_D_ITEM_LST.join(',');
                vDtl.MC_COLOR_LST = vDtl.MC_COLOR_LST.join(',');
                vDtl.MC_SIZE_LST = vDtl.MC_SIZE_LST.join(',');
                vDtl.FAB_COLOR_LST = vDtl.FAB_COLOR_LST.join(',');
                //vDtl.STYLE_D_ITM_CAT_LST = vDtl.STYLE_D_ITM_CAT_LST.join(',');
                if (vDtl.COMBO_NO_LST != null) {
                    vDtl.COMBO_NO_LST = vDtl.COMBO_NO_LST.join(',');
                }
                vDtl.PCUT_WSTG = vDtl.PCUT_WSTG;
                
                vm.updateGrid(vDtl, vm.updateGridIndex);
                //$scope.$parent.fabReqDtl.remove(vDtl);
                //$scope.$parent.fabReqDtl.push(vDtl);
                                
                //vm.updateGridIndex = null;
            }

            vm.cancelToGrid();
            vm.isAddToGrid = 'Y';
            console.log($scope.$parent.fabReqDtl.data);
            //console.log($scope.$parent.fabReqDtl[vm.updateGridIndex]);            
        };

        vm.updateGrid = function (data, index) {
            console.log(data.uid);
            console.log($scope.$parent.fabReqDtl);
            //console.log(dataItem);

            var dataItem = $scope.$parent.fabReqDtl.getByUid(data.uid);

            //console.log($scope.$parent.fabReqDtl._data);
            //console.log(data.uid);
            console.log(dataItem);
            
            dataItem.set('DISPLAY_ORDER', data.DISPLAY_ORDER);
            dataItem.set('COLOR_SPEC', data.COLOR_SPEC);
            dataItem.set('COMBO_NO_LST', data.COMBO_NO_LST);
            dataItem.set('CONS_DOZ_DESC', data.CONS_DOZ_DESC);
            dataItem.set('CONS_DOZ_UNIT_NAME', data.CONS_DOZ_UNIT_NAME);
            dataItem.set('CONS_MOU_ID', data.CONS_MOU_ID);
            dataItem.set('CONS_QTY', data.CONS_QTY);
            dataItem.set('QTY_MOU_ID', data.QTY_MOU_ID);
            dataItem.set('CUT_DIA', data.CUT_DIA);
            dataItem.set('DIA_MOU_ID', data.DIA_MOU_ID);
            dataItem.set('FABRIC_DESC', data.FABRIC_DESC);
            dataItem.set('FABRIC_GROUP_NAME', data.FABRIC_GROUP_NAME);
            dataItem.set('FAB_COLOR_LST', data.FAB_COLOR_LST);
            dataItem.set('IS_CONTRAST', data.IS_CONTRAST);
            dataItem.set('FIN_DIA', data.FIN_DIA);
            dataItem.set('FIN_DIA_DESC', data.FIN_DIA_DESC);
            dataItem.set('FIN_DIA_TYPE_NAME', data.FIN_DIA_TYPE_NAME);
            dataItem.set('FIN_DIA_UNIT_NAME', data.FIN_DIA_UNIT_NAME);

            dataItem.set('GARM_COLOR_NAME_LST', data.GARM_COLOR_NAME_LST); 
            dataItem.set('FAB_COLOR_NAME_LST', data.FAB_COLOR_NAME_LST);
            dataItem.set('GARM_PART_NAME_LST', data.GARM_PART_NAME_LST);
            dataItem.set('IS_ALL_COL', data.IS_ALL_COL);
            dataItem.set('IS_ALL_SIZE', data.IS_ALL_SIZE);
            dataItem.set('IS_SAME_GMT_COL', data.IS_SAME_GMT_COL);
            dataItem.set('ITEM_PART_NAME_LST', data.ITEM_PART_NAME_LST);
            dataItem.set('LK_DIA_TYPE_ID', data.LK_DIA_TYPE_ID);
            dataItem.set('LK_FBR_GRP_ID', data.LK_FBR_GRP_ID);
            dataItem.set('MC_BLK_FAB_REQ_D_ID', data.MC_BLK_FAB_REQ_D_ID);

            dataItem.set('IS_YD', data.IS_YD);
            dataItem.set('LK_YD_TYPE_ID', data.LK_YD_TYPE_ID);
            dataItem.set('LK_FEDER_TYPE_ID', data.LK_FEDER_TYPE_ID);

            dataItem.set('IS_ALL_ORDER', data.IS_ALL_ORDER);
            dataItem.set('MAP_ORDER_H_ID_LST', data.MAP_ORDER_H_ID_LST);
            dataItem.set('MC_BLK_FAB_REQ_H_ID', data.MC_BLK_FAB_REQ_H_ID);
            dataItem.set('MC_COLOR_LST', data.MC_COLOR_LST);
            dataItem.set('MC_SIZE_LST', data.MC_SIZE_LST);
            dataItem.set('INV_ITEM_CAT_ID', data.INV_ITEM_CAT_ID);
            dataItem.set('MC_STYLE_D_FAB_ID', data.MC_STYLE_D_FAB_ID);
            dataItem.set('PCUT_WSTG', data.PCUT_WSTG);
            dataItem.set('RF_GARM_PART_LST', data.RF_GARM_PART_LST);
            dataItem.set('SIZE_NAME_LST', data.SIZE_NAME_LST);
            dataItem.set('STYLE_D_ITEM_LST', data.STYLE_D_ITEM_LST);
            dataItem.set('STYLE_D_ITM_CAT_LST', data.STYLE_D_ITM_CAT_LST);
            
        };
        
        vm.fabCatOnChange = function () {
            vm.styleWiseFabListData = _.filter($scope.$parent.styleWiseFabListData, function (ob) {
                return ob.INV_ITEM_CAT_ID == vm.formDtl.INV_ITEM_CAT_ID;
            });

            if (vm.formDtl.INV_ITEM_CAT_ID == 7) {
                vm.formDtl.MC_STYLE_D_FAB_ID = null;
                vm.formDtl.IS_FEDER_TYPE = 'N';
                vm.formDtl.LK_FEDER_TYPE_ID = '';
            }
            else {
                vm.formDtl.INV_ITEM_ID = null;
            }
        };

        vm.fabOnChange = function (e) {
            var item = e.sender.dataItem(e.item); //e.sender.dataItem(e.sender.select());
            console.log(item);

            //vm.formDtl.FABRIC_DESC = item.FABRIC_DESC;
            if (item.IS_ELA_MXD == 'Y') {
                vm.formDtl.IS_FEDER_TYPE = 'Y';
                vm.formDtl.LK_FEDER_TYPE_ID = item.LK_FEDER_TYPE_ID;
            }
            else {
                vm.formDtl.IS_FEDER_TYPE = 'N';
                vm.formDtl.LK_FEDER_TYPE_ID = '';
            }
            
            
            if (item.INV_ITEM_CAT_ID == 34) {
                vm.formDtl.INV_ITEM_ID = '';
            }
            else if (item.INV_ITEM_CAT_ID == 35) {
                vm.formDtl.INV_ITEM_ID = item.INV_ITEM_ID;
            }
            //else {
            //    vm.formDtl.CONS_MOU_ID = vm.kgMouId;
            //}
        };
        
        vm.fabGrpOnChange = function (e) {
            var item = e.sender.dataItem(e.sender.select());
            vm.formDtl.FABRIC_GROUP_NAME = item.LK_DATA_NAME_EN;
        };

        vm.finDiaUnitOnChange = function (e) {
            var item = e.sender.dataItem(e.sender.select());
            vm.formDtl.FIN_DIA_UNIT_NAME = item.MOU_CODE;
        };

        vm.finDiaTypeOnChange = function (e) {
            var item = e.sender.dataItem(e.sender.select());
            vm.formDtl.FIN_DIA_TYPE_NAME = item.LK_DATA_NAME_EN;
        };

        vm.consDozOnChange = function (e) {            
            var item = e.sender.dataItem(e.sender.select());
            vm.formDtl.CONS_DOZ_UNIT_NAME = item.MOU_CODE;
        };
        
    }

})();







// Booking List Controller
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('bulkFabBkListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$timeout', '$state', '$stateParams', 'MrcDataService', bulkFabBkListController]);
    function bulkFabBkListController(logger, config, $q, $scope, $http, exception, $filter, $timeout, $state, $stateParams, MrcDataService) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();
        vm.form = { MC_BYR_ACC_ID: 0, MC_STYLE_H_ID: '', MC_STYLE_H_EXT_ID: '', BLK_FAB_REQ_NO: '' };

        
        activate();
        function activate() {
            var promise = [getBuyerAcList(), getByrAccWiseStyleList()];
            return $q.all(promise).then(function () {
                if ($stateParams.pMC_BYR_ACC_ID > 0) {
                    vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                }

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

        vm.form.FROM_DT = vm.today;
        vm.toDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.toDateOpened = true;
        };
        vm.form.TO_DT = vm.today;

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
                dataValueField: "MC_STYLE_H_ID",
                dataBound: function (e) {
                    if ($stateParams.pMC_STYLE_H_ID > 0) {
                        vm.form.MC_STYLE_H_ID = $stateParams.pMC_STYLE_H_ID;
                    }
                }
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
                        var url = '/api/mrc/StyleH/BuyerAcWiseStyleList?&pageNumber=1&pageSize=20' + '&pMC_BYR_ACC_ID=' + (($stateParams.pMC_BYR_ACC_ID>0)?$stateParams.pMC_BYR_ACC_ID:vm.form.MC_BYR_ACC_ID);
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        if (!paramsData[2] && $stateParams.pMC_STYLE_H_ID>0) {
                            url += '&pMC_STYLE_H_ID=' + $stateParams.pMC_STYLE_H_ID;
                        }
                           
                        console.log(url);

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
           
            vm.fabReqDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,                
                schema: {
                    data: "data",
                    total: "total"
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/BulkFabBk/BulkFabBookingList/' + vm.form.MC_BYR_ACC_ID + '/' + ((vm.form.MC_STYLE_H_ID>0)?vm.form.MC_STYLE_H_ID:null) + '/null?';
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
                },
                pageSize: 10,
                //scrollable: {
                //    virtual: true
                //    //scrollable:true
                //},
                group: [{ field: 'STYLE_NO' }],
                sort: [{ field: 'STYLE_NO', dir: 'asc' }, { field: 'BLK_FAB_REQ_DT', dir: 'desc' }]
            });
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
            if (navigateId == 1) { //Edit Booking
                $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID });
            }
            else if (navigateId == 2) { //Print Booking
                vm.printBookingRecord(dataItem, pREVISION_NO);
            }
            else if (navigateId == 3) { //Copy Booking As
                $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID, pIS_COPY: 'Y' });
            }
            else if (navigateId == 6) { //Edit Budget
                $state.go('BudgetSheet', { pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID, pMC_STYLE_H_ID: dataItem.MC_STYLE_H_ID, pMC_STYL_BGT_H_ID: dataItem.MC_STYL_BGT_H_ID });
            }
            else if (navigateId == 7) { //Print Budget
                vm.printBudgetSheetReport(dataItem);
            }
        };
        
        //vm.fabReqDataSource = new kendo.data.ObservableArray([]);
        //vm.fabReqDataSource = new kendo.data.DataSource({
        //    data: [],
        //    group: [{ field: 'STYLE_NO' }],
        //    sort: [{ field: 'STYLE_NO', dir: 'asc' }, { field: 'BLK_FAB_REQ_DT', dir: 'asc' }]
        //});
        
        vm.gridOptionsFabReq = {                        
            height: '500',                        
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
                { field: "STYLE_NO", title: "Style#", type: "string", hidden: true },
                { field: "BLK_FAB_REQ_DT_STR", title: "Booking Date", type: "date", format: "{0:dd-MMM-yyyy}", filterable: false },
                { field: "BLK_FAB_REQ_DT", title: "Booking Date A", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true },
                { field: "WORK_STYLE_NO_LST", title: "Work Style#", type: "string" },
                { field: "ORDER_NO_LST", title: "Order#", type: "string" },
                { field: "BLK_FAB_REQ_NO", title: "Booking Ref#", type: "string" },
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
                                '<li><span style="color:black">Navigate</span>' +
                                    '<ul style="width:150px;">' +
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,1)"><i class="fa fa-edit"> Booking Edit</i></a></li>' +
                                        '<li><i class="fa fa-print"> Booking Print</i>' +
                                            '<ul style="width:150px;"><li class="k-item k-state-default k-first" ng-repeat="itm in dataItem.REVISION_LIST"><a class="k-link" style="color:black" ng-click="vm.navigateAction(dataItem,2,itm.REVISION_NO)">{{itm.REV_REASON}}</a></li></ul>' +
                                        '</li>' +
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,3)"><i class="fa fa-copy"> Save As</i></a></li>' +
                                        '<li disabled="disabled" style="padding:0px"><hr style="margin:0px;border-top: 1px solid grey;" /></li>' +
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,6)" ><i class="fa fa-edit"> Budget Edit</i></a></li>' +
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,7)" ><i class="fa fa-print"> Budget Print</i></a></li>' +
                                    '</ul>' +
                                '</li></ul>';
                    },
                    width: "120px"
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
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReportRDLC');
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
//