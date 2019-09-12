(function () {
    'use strict';
    angular.module('multitex.mrc').controller('smplFabBookListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'MrcDataService', smplFabBookListController]);
    function smplFabBookListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, MrcDataService) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'MC_SMP_REQ_H_ID';
        vm.today = new Date();
        vm.form = {MC_BYR_ACC_ID: 0, LK_ORDER_TYPE_LST: '0', MC_STYLE_H_EXT_ID: 0};// formData[key] ? formData : {};
        //$scope.sampFabQty = [];
        
        vm.styleExtTemplate = '<span><span style="padding:0px;margin:0px;color:black">#: data.STYLE_NO #</span><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><span style="padding:0px;margin:0px;color:black">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</span></span>';

        activate();
        function activate() {
            var promise = [getBuyerAcList(), getByrAccWiseStyleExtList()];
            return $q.all(promise).then(function () {
                vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }

        

        //function getBuyerAcList() {
        //    MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {                
        //        vm.MC_BYR_ACC_LIST = res;

        //        if ($stateParams.pMC_BYR_ACC_ID && vm.MC_BYR_ACC_LIST.length>0) {
        //            vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
        //        }
        //    });
           
        //};

        function getBuyerAcList() {
            vm.buyerAccWiseStyleList = new kendo.data.ObservableArray([]);

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "startswith",
                autoBind: true,
                dataBound: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    vm.form.LK_ORDER_TYPE_LST = item.LK_ORDER_TYPE_LST;
                    vm.form.MC_STYLE_H_EXT_ID = null;

                    if (item.MC_BYR_ACC_ID) {
                        return MrcDataService.GetAllOthers('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + item.MC_BYR_ACC_ID + '/' + null).then(function (res) {
                            vm.buyerAccWiseStyleList = res;
                            //alert($stateParams.pMC_STYLE_H_ID);
                            if ($stateParams.pMC_STYLE_H_ID) {
                                vm.form.MC_STYLE_H_ID = $stateParams.pMC_STYLE_H_ID;
                                vm.form.MC_STYLE_H_EXT_ID = $stateParams.MC_STYLE_H_EXT_ID;
                            }

                            vm.showBooking();

                        }, function (err) {
                            console.log(err);
                        });
                    }
                    else {
                        //alert('else');
                        vm.buyerAccWiseStyleList = [];
                    }

                },
                select: function (e) {
                    var item = this.dataItem(e.item);

                    console.log(item);

                    vm.form.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    vm.form.LK_ORDER_TYPE_LST = item.LK_ORDER_TYPE_LST;
                    vm.form.MC_STYLE_H_EXT_ID = null;

                    vm.styleExtDataSource.read();
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
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
                        url += '&pLK_ORDER_TYPE_LST=' + vm.form.LK_ORDER_TYPE_LST;
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


        function getBuyerListData() {
            return vm.buyerList = {
                optionLabel: "-- Select Buyer --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/buyer/BuyerByUserList').then(function (res) {
                                var uniques = _.map(_.groupBy(res, function (doc) {
                                    return doc.MC_BUYER_ID;
                                }), function (grouped) {
                                    return grouped[0];
                                });
                                e.success(uniques);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BUYER_NAME_EN",
                dataValueField: "MC_BUYER_ID"
            };
        };

        vm.showBooking = function () {
            if (vm.form.MC_BYR_ACC_ID == "") {
                var vBuyerAcId = null;
            }
            else {
                //alert('x');
                var vBuyerAcId = vm.form.MC_BYR_ACC_ID;                                
            }
            
            vm.smplGridDataSource.read();

            //var dataSource = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            var webapi = new kendo.data.transports.webapi({});
            //            var params = webapi.parameterMap(e.data);
            //            var url = '/api/mrc/SampleReq/BuyerAccWiseBookingList/' + vBuyerAcId + '/null';
            //            url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
            //            url += MrcDataService.kFilterStr2QueryParam(params.filter);
            //            console.log(params.filter);

            //            return MrcDataService.getDataByUrl(url).then(function (res) {
            //                angular.forEach(res.data, function (val, key) {
            //                    val['SMP_REQ_DT'] = $filter('date')(val['SMP_REQ_DT'], vm.dtFormat);
            //                });
            //                e.success(res);
            //                console.log(res);
            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    },
            //    pageSize: 10
            //});

            //$('#bookingGrid').data("kendoGrid").setDataSource(dataSource);

            //return MrcDataService.GetAllOthers('/api/mrc/SampleReq/BuyerAccWiseBookingList/' + vBuyerAcId + '/null').then(function (res) {
                
            //});            
        };

        vm.addNew = function () {            
            return $state.go('SmplFabBookEntry.Dtl', { pMC_SMP_REQ_H_ID: 0 });
        };

        vm.editBookingRecord = function (dataItem) {            
            vm.form = dataItem;
            return $state.go('SmplFabBookEntry', { pBookingObj: dataItem });
        };
                
        vm.printBookingRecord = function (dataItem, pREVISION_NO) {
            //console.log(dataItem);

            vm.form.REPORT_CODE = 'RPT-2000';
            vm.form.MC_SMP_REQ_H_ID = dataItem.MC_SMP_REQ_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReportRDLC');
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
                $state.go('SmplFabBookEntry.Dtl', { pMC_SMP_REQ_H_ID: dataItem.MC_SMP_REQ_H_ID, pLK_STYL_DEV_TYP_ID: dataItem.LK_STYL_DEV_TYP_ID||0 });
                
            }
            else if (navigateId == 2) {
                vm.printBookingRecord(dataItem, pREVISION_NO);
            }
            else if (navigateId == 3) {
                    $state.go('SmplProdEntry.Dtl', { pMC_SMP_REQ_H_ID: dataItem.MC_SMP_REQ_H_ID, pMC_BYR_ACC_ID: 0, pMC_STYLE_H_EXT_ID: 0, pRF_SMPL_TYPE_ID: 0, pPROD_DT: moment(vm.today).format("YYYY-MMM-DD"), pPROD_BATCH_NO: 1 });
            }            
        };

        vm.smplGridDataSource = new kendo.data.DataSource({
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
                    var url = '/SampleReq/BuyerAccWiseBookingList/' + (vm.form.MC_BYR_ACC_ID||0) + '/null?';
                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pLK_ORDER_TYPE_LST=' + (vm.form.LK_ORDER_TYPE_LST || '') + '&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || null);
                            
                    url += MrcDataService.kFilterStr2QueryParam(params.filter);
                    console.log(params.filter);

                    return MrcDataService.getDataByUrl(url).then(function (res) {
                        angular.forEach(res.data, function (val, key) {
                            val['SMP_REQ_DT'] = $filter('date')(val['SMP_REQ_DT'], vm.dtFormat);
                        });
                        e.success(res);
                        console.log(res);
                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            pageSize: 10,
            group: [{ field: 'STYLE_NO_LST' }],
            sort: [{ field: 'STYLE_NO_LST', dir: 'asc' }, { field: 'SMP_REQ_DT', dir: 'desc' }]
        });
            
        vm.smplGridOptions = {
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
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "100px", hidden: true },
                { field: "SMP_REQ_DT", title: "Prog. Date", type: "date", width: "80px", format: "{0:" + vm.dtFormat + "}" },
                { field: "SMP_REQ_NO", title: "Program#", type: "string", width: "80px" },
                { field: "STYLE_NO_LST", title: "Style#", type: "string", width: "180px" },
                { field: "ORDER_NO_LST", title: "Order#", type: "string", width: "180px" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "200px" },
                {
                    title: "",
                    template: function () {                        
                        return '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                                '<li><span style="color:black">Navigate</span>' +
                                    '<ul style="width:150px;">'+
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,1)"><i class="fa fa-edit"> Edit Program</i></a></li>' +
                                        '<li><a class="k-link" style="color:black" ng-click="vm.navigateAction(dataItem,2,0)"> <i class="fa fa-print"> Print</i></a></li>' +                                            
                                        '<li disabled="disabled" style="padding:0px"><hr style="margin:0px;border-top: 1px solid grey;" /></li>' +
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,3)" ><i class="fa fa-edit"> Sample Production</i></a></li>' +                                        
                                    '</ul>'+
                                '</li></ul>';
                    },
                    width: "80px"
                }
            ]
        };


        function collapseAllGroups(grid) {
            grid.table.find(".k-grouping-row").each(function () {
                grid.collapseGroup(this);
            });
        }
        


    }

})();



//////============= Sample Fabric Booking Controller ====================
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('smplFabBookController', ['logger', 'config', '$q', '$scope', '$rootScope', '$http', 'exception', 'formData', 'byrAcData', 'userData', '$filter', '$state', '$stateParams', 'MrcDataService', '$timeout', smplFabBookController]);
    function smplFabBookController(logger, config, $q, $scope, $rootScope, $http, exception, formData, byrAcData, userData, $filter, $state, $stateParams, MrcDataService, $timeout) {

        var vm = this;
        $scope.errors = null;
        $scope.reqToList = [];
        vm.assignToUserList = '169,51,79,113,193,204,205,244,234,383'.split(',');
        console.log(vm.assignToUserList);

        vm.MC_BYR_ACC_LIST = new kendo.data.ObservableArray([]);
        vm.userDataList = new kendo.data.ObservableArray([]);
        vm.MC_BYR_ACC_LIST = byrAcData;
        vm.userDataList = userData;
        
        if ($stateParams.pLK_STYL_DEV_TYP_ID == 542) {
            
            $rootScope.activeTabItemColor = false;
            $rootScope.activeTabFabBooking = true;
        }
        else {
            $rootScope.activeTabItemColor = true;
            $rootScope.activeTabFabBooking = false;
        }

        $scope.sampFabQty = [];
        //$scope.activeTabItemColor = true;

        //$scope.activeTabFabBookingFn = function (bool) {
        //    console.log(bool);
        //    $scope.activeTabFabBooking = bool;
        //};
        //$scope.activeTabItemColorFn = function (bool) {
        //    console.log(bool);
        //    $scope.activeTabItemColor = bool;
        //};

        vm.showSplash = true;
        $scope.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };
        vm.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };

        vm.today = new Date(); //kendo.toString(new Date(), "dd/MMM/yyyy hh:mm tt");// new Date();
               
        //console.log(formData['SMP_REQ_DT']);
        //console.log($filter('date')(formData['SMP_REQ_DT'], "dd/MMM/yyyy hh:mm a"));
        //formData['SMP_REQ_DT'] = $filter('date')(formData['SMP_REQ_DT'], "dd/MMM/yyyy hh:mm a");
        //formData['REVISION_DT'] = $filter('date')(formData['REVISION_DT'], "dd/MMM/yyyy hh:mm a");

        var key = 'MC_SMP_REQ_H_ID';
        vm.isChangeByrAcActive = 'N';
        
        //if (formData[key]) {
        //    vm.userDataList = formData['userDataList'];
        //    vm.attenUserList = formData['attenUserList'];
        //}

        console.log(formData);
        vm.form = { SMP_REQ_DT: vm.today, HR_COMPANY_LIST: [], /*MC_BYR_ACC_LIST: [],*/ MC_BUYER_LIST: [], MC_BYR_ACC_ID_LIST: [], SMP_REQ_TO_LIST: [] };


        activate();
        function activate() {
            //vm.form = {};
            var promise = [getCompanyList(), /*getBuyerAcList(),*/ getGmtPartList(), getDiaTypeList(), getRfMouList(), getYDType()];
            return $q.all(promise).then(function () {
                ///************* Start for Edit ****************************
                $timeout(function () {
                    if (formData[key]) {
                        vm.form = formData[key] ? formData : {};
                        $scope.MC_BYR_ACC_ID_LIST = formData[key] ? formData['MC_BYR_ACC_ID_LIST'] : [];
                        $scope.reqToList = formData[key] ? formData['SMP_REQ_TO_LIST'] : [];
                    }
                }, 1000);                                
                ///************* End for Edit ****************************               
                vm.showSplash = false;
                vm.isChangeByrAcActive = 'Y';
            });
        };

        vm.sfbReqDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.sfbReqDateOpened = true;
        };        

        vm.isDate = function (x) {
            return x instanceof Date;
        };

        

        vm.styleChange = function (item) {
            console.log(item);
            //vm.styleWiseOrderList = res;
            //            //console.log(res); 
            //            vm.form.itemsReqDtl[0].orderList = res;
            
            //vm.styleWiseOrderList = _.filter(vm.orderList, { 'MC_STYLE_H_ID': item.MC_STYLE_H_ID });

            var vStyleHId = parseInt(item.MC_STYLE_H_ID);
            var style = _.filter(vm.buyerWiseStyleList, { 'MC_STYLE_H_ID': vStyleHId });
            
            item.orderList = _.filter(vm.orderList, { 'MC_STYLE_H_ID': vStyleHId });
            
            item.itemList = _.filter(vm.itemListData, { 'MC_STYLE_H_ID': vStyleHId });
            item.STYLE_DESC = style.length > 0 ? style[0].STYLE_DESC : '';

            //var fab = _.filter(vm.fabList, { 'MC_STYLE_H_ID': item.MC_STYLE_H_ID });            
            //item.itemsReqDtl[vm.selectedReqDtl.REQ_D_INDEX].itemsReqDtl2[vm.selectedReqDtl1.REQ_D1_INDEX].fabList = _.filter(vm.fabList, { 'MC_STYLE_H_ID': item.MC_STYLE_H_ID });;
        };

        vm.orderChange = function (item, pMC_ORDER_H_ID, pMC_STYLE_D_ITEM_ID) {
            if (pMC_ORDER_H_ID > 0) {
                vm.buyerOrderAccWiseColorList = _.filter(vm.colorList, { 'MC_ORDER_H_ID': pMC_ORDER_H_ID });

                var order = _.filter(item.orderList, { 'MC_ORDER_H_ID': pMC_ORDER_H_ID });
                //console.log(order);
                item.SHIP_DT = $filter('date')(order[0].SHIP_DT, vm.dtFormat);
                item.IS_TNA_FINALIZED = order[0].IS_TNA_FINALIZED;
                
                if (pMC_STYLE_D_ITEM_ID > 0) {
                    var styleItem = _.filter(order[0].itemsStyle, { 'MC_STYLE_D_ITEM_ID': pMC_STYLE_D_ITEM_ID });
                    //console.log(styleItem);
                    item.ORDER_QTY = styleItem.length>0?styleItem[0].ORDER_QTY:0;
                }
                else {
                    item.ORDER_QTY = 0;
                }
            }
            //item.TOT_ORD_QTY = order[0].TOT_ORD_QTY || 0;
        };
        
        $scope.$watch('vm.form', function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                    $scope.form.SMP_REQ_DT = $filter('date')(vm.form.SMP_REQ_DT, vm.dtFormat);
                    //console.log($scope.form);
                }
            }
        }, true);

        function getColorList(pMC_ORDER_H_ID, pMC_BYR_ACC_ID) {
            return MrcDataService.GetAllOthers('/api/mrc/Order/OrderOrByerAccWiseColorList/pMC_ORDER_H_ID/pMC_BYR_ACC_ID').then(function (res) {
                vm.colorList = res;
                var uniques = _.map(_.groupBy(res, function (doc) {
                    return doc.MC_COLOR_ID;
                }), function (grouped) {
                    return grouped[0];
                });
                vm.buyerOrderAccWiseColorList = new kendo.data.ObservableArray(uniques);

            }, function (err) {
                console.log(err);
            });

            //return MrcDataService.selectAllData('ColourMaster').then(function (res) {
            //    //vm.colorList = res;
            //    vm.colorList = {
            //        optionLabel: "-- Select --",
            //        filter: "startswith",
            //        autoBind: true,
            //        dataSource: res,
            //        dataTextField: "COLOR_NAME_EN",
            //        dataValueField: "MC_COLOR_ID"
            //    };
                
            //}, function (err) {
            //    console.log(err);
            //});
        };
        
        function getOrderList(pMC_BYR_ACC_ID) {
            return MrcDataService.GetAllOthers('/api/mrc/Order/OrderDataList/null/null/null/null/' + pMC_BYR_ACC_ID).then(function (res) {
                vm.orderList = res;
                vm.buyerAcWiseOrderList = new kendo.data.ObservableArray(res);
                                  
            }, function (err) {
                console.log(err);
            });
        };
                
        function getUserDataList(pMC_BYR_ACC_IDS) {
            if (angular.isDefined(pMC_BYR_ACC_IDS)) {
                return MrcDataService.GetAllOthers('/api/mrc/UserBuyerAcc/GetUsersByByrAc/' + pMC_BYR_ACC_IDS).then(function (res) {
                    vm.userDataList = _.filter(res, function (ob) {
                        //alert(ob.HR_EMPLOYEE_ID);
                        return ob.HR_EMPLOYEE_ID != 0;
                    });

                    var userIdList = _.filter(vm.assignToUserList, function (val) {
                        return _.some(vm.userDataList, function (ob) {
                            return ob.SC_USER_ID == val;
                        });
                    });

                    $scope.reqToList = userIdList;
                    vm.setReqToList(userIdList);
                                        
                    vm.attenUserList = _.filter(res, function (ob) {
                        return ob.HR_EMPLOYEE_ID != 0;
                    });
                    //console.log(res); 
                    //vm.form.itemsReqDtl[0].orderList = res;               
                }, function (err) {
                    console.log(err);
                });
            }
        };

        vm.setReqToList = function (pUserIdList) {
            vm.form.SMP_REQ_TO_LIST = pUserIdList;
        }

        vm.isReqToChangeCount = 0;
        vm.onChangeReqToList = function () {
            var ids = angular.copy(vm.form.SMP_REQ_TO_LIST);

            if (vm.isReqToChangeCount >= 2) {                
                $scope.reqToList = ids;
            }
            else {
                if (vm.isReqToChangeCount < 2) {
                    vm.isReqToChangeCount = vm.isReqToChangeCount + 1;
                }
            }
        }

        function getFabDataByBuyerAccId(pMC_BYR_ACC_ID) {
            return MrcDataService.GetAllOthers('/api/mrc/StyleDItemFab/FabDataByBuyerAccId/' + pMC_BYR_ACC_ID).then(function (res) {
                vm.fabList = res;
                vm.buyerAccWiseFabList = res;
                //console.log(res); 
                //vm.form.itemsReqDtl[0].orderList = res;               
            }, function (err) {
                console.log(err);
            });
        };

        vm.getStyleWiseOrderList = function (pMC_STYLE_H_ID) {
            var MC_STYLE_H_ID = parseInt(pMC_STYLE_H_ID);
            //alert(MC_STYLE_H_ID);
            //console.log(vm.orderList);
            vm.buyerAcWiseOrderList = _.filter(vm.orderList, { 'MC_STYLE_H_ID': MC_STYLE_H_ID });
            //console.log(vm.buyerAcWiseOrderList);
        };

        
        function getItemList(pMC_STYLE_H_ID) {
            //alert('x');
            return MrcDataService.GetAllOthers('/api/mrc/StyleDItem/StyleDtlItemList/' + pMC_STYLE_H_ID + '/' + 'A').then(function (res) {
                vm.itemList = res;
                vm.styleWiseItemList = new kendo.data.ObservableArray(res);
                //console.log(res);                
            }, function (err) {
                console.log(err);
            });
        };

        vm.getStyleWiseItemList = function (pMC_STYLE_H_ID) {
            var MC_STYLE_H_ID = parseInt(pMC_STYLE_H_ID);
            vm.styleWiseItemList = _.filter(vm.itemList, { 'MC_STYLE_H_ID': MC_STYLE_H_ID });
        };
                

        function getCompanyList() {
            vm.companyList = [];
            if (parseInt($stateParams.pMC_SMP_REQ_H_ID) < 1) {
                //var compDataSource = new kendo.data.DataSource({
                //    transport: {
                //        read: function (e) {
                //            MrcDataService.GetAllOthers('/api/common/CompanyList').then(function (res) {
                //                vm.form.HR_COMPANY_LIST = res;
                //                e.success(res);
                //            }, function (err) {
                //                console.log(err);
                //            });
                //        },
                //        pageSize: 5
                //    }
                //});

                //return vm.companyList = {
                //    dataSource: compDataSource,
                //    index: 0,
                //    height: 200,
                //    dataTextField: "COMP_NAME_EN",
                //    dataValueField: "HR_COMPANY_ID",
                //};

                MrcDataService.GetAllOthers('/api/common/CompanyList').then(function (res) {
                    vm.companyList = res;
                    vm.form.HR_COMPANY_LIST = res;
                    vm.form.HR_COMPANY_ID = 1;
                });                
            }
           
        };

        //vm.form.MC_BYR_ACC_LIST = new kendo.data.ObservableArray([]);
        //vm.userDataList = new kendo.data.ObservableArray([]);

        function getBuyerAcList() {
            if (formData['MC_BYR_ACC_LST']) {
                
                vm.form.MC_BYR_ACC_LIST = formData['MC_BYR_ACC_LIST'];
                $scope.MC_BYR_ACC_ID_LIST = formData['MC_BYR_ACC_LST'].split(',');
                getUserDataList(formData['MC_BYR_ACC_LST']);
            }
            else {
                MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {                    
                    vm.form.MC_BYR_ACC_LIST = res;
                });
            }
        };
       
        
        vm.onChangeByrAc = function () {
            console.log(vm.form.MC_BYR_ACC_ID_LIST);

            if (vm.isChangeByrAcActive == 'Y') {                
                if (vm.form.MC_BYR_ACC_ID_LIST != null) {
                    //alert('a');
                    $scope.MC_BYR_ACC_ID_LIST = vm.form.MC_BYR_ACC_ID_LIST;
                    getUserDataList(vm.form.MC_BYR_ACC_ID_LIST.join(','));
                }
                else {
                    //alert('b');
                    $scope.MC_BYR_ACC_ID_LIST = ['0'];
                    getUserDataList('0');
                }
            }
        };

        vm.reqToOnSelect = function (e) {
            var item = e.sender.dataItem(e.item);
           
            if (item.SC_USER_ID > 0) {                
                vm.attenUserList = _.filter(vm.userDataList, function(ob) {                 
                    return ob.SC_USER_ID != parseInt(item.SC_USER_ID);
                });
                vm.form.SMP_REQ_ATTN_LIST = [];
            }
            else {
                vm.attenUserList = vm.userDataList;
            }
        };

        function getItemListData() {
            return MrcDataService.GetAllOthers('/api/mrc/StyleDItem/StyleDtlItemList/' + null + '/' + 'A').then(function (res) {
                vm.itemListData = res;
                if (vm.form.itemsReqDtl > 0) {
                    vm.form.itemsReqDtl[0].itemList = res;
                }
                //console.log(res);                
            }, function (err) {
                console.log(err);
            });
        };

        function getColorTypeList() {
            return vm.colorTypeListData = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(31).then(function (res) {
                                e.success(res);
                            });
                        },
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };
                
        function getGmtPartList() {
            return MrcDataService.GetAllOthers('/api/common/GmtPartList').then(function (res) {
                //vm.gmtPartList = res;
                $scope.gmtPartList = res;
                //vm.gmtPartCircList = _.filter(res, { 'IS_FLAT_CIR': 'C' });
                //vm.gmtPartFlatList = _.filter(res, { 'IS_FLAT_CIR': 'F' });
            }, function (err) {
                console.log(err);
            });
        }

        function getDiaTypeList() {
            return MrcDataService.GetAllOthers('/api/common/LookupListData/67').then(function (res) {
                $scope.diaTypeList = res;
            }, function (err) {
                console.log(err);
            });
        };

        function getRfMouList() {
            return MrcDataService.GetAllOthers('/api/common/MOUList/Y').then(function (res) {
                $scope.rfMouList = res;
                //vm.rfMouList = res;
            }, function (err) {
                console.log(err);
            });
        };
        
        function getYDType() {
            return MrcDataService.GetAllOthers('/api/common/LookupListData/71').then(function (res) {
                $scope.ydTypeList = res;
            }, function (err) {
                console.log(err);
            });
        };
        
        vm.addRow = function (data, copyTo, obName) {
            var copiedData = angular.copy(data);
            
            //console.log(copiedData);
            if (obName == 'itemsReqDtl') {
                copiedData.MC_SMP_REQ_D_ID = 0;
                copiedData.MC_STYLE_H_ID = '';
                copiedData.MC_ORDER_H_ID = '';
                copiedData.MC_STYLE_D_ITEM_ID = '';
                copiedData.STYLE_DESC = '';
                copiedData.SHIP_DT = '';
                copiedData.ORDER_QTY = '';
                
                angular.forEach(copiedData.itemsReqDtl1, function (val, key) {
                    val['MC_SMP_REQ_D1_ID'] = 0;
                    val['RF_SMPL_TYPE_ID'] = '';                                        
                    val['MC_COLOR_ID'] = '';
                    val['RQD_QTY'] = '';
                    val['LK_PRIORITY_ID'] = '';
                    val['REMARKS'] = '';
                });
                angular.forEach(copiedData.itemsReqDtl2, function (val, key) {
                    val['MC_SMP_REQ_D2_ID'] = 0;                    
                    val['GARM_PART_LIST'] = '';
                    val['MC_STYLE_D_FAB_ID'] = '';
                    val['FIN_DIA'] = '';
                    val['RQD_QTY'] = '';
                });
                angular.forEach(copiedData.itemsReqDtl3, function (val, key) {                    
                    angular.forEach(val['itemsSize'], function (val1, key1) {
                        val1['MC_SMP_REQ_D3_ID'] = 0;
                        val1['RQD_PCS'] = 0;
                        val1['MEASUREMENT'] = '';
                    });
                });
            }
            else if (obName == 'itemsReqDtl1') {                
                copiedData['MC_SMP_REQ_D1_ID'] = 0;
                copiedData['RF_SMPL_TYPE_ID'] = '';
                copiedData['MC_COLOR_ID'] = '';
                copiedData['RQD_QTY'] = '';
                copiedData['LK_PRIORITY_ID'] = '';
                copiedData['REMARKS'] = '';
            }
            else if (obName == 'itemsReqDtl2') {                
                copiedData['MC_SMP_REQ_D2_ID'] = 0;
                copiedData['GARM_PART_LIST'] = '';
                copiedData['MC_STYLE_D_FAB_ID'] = '';
                copiedData['FIN_DIA'] = '';
                copiedData['RQD_QTY'] = '';
            }
            else if (obName == 'itemsReqDtl3') {
                angular.forEach(copiedData.itemsSize, function (val, key) {
                    val['MC_SMP_REQ_D3_ID'] = 0;
                    val['RQD_PCS'] = 0;
                    val['MEASUREMENT'] = '';
                });
            }
            //console.log(copiedData);

            copyTo.push(copiedData);
        };

        vm.removeRow = function (index, removeFrom, master, obParent, parentIndex) {
            removeFrom.splice(index, 1);

            if (master == true) {
                if (removeFrom.length < 1) {
                    obParent.splice(parentIndex, 1);
                }
            }
        };

        vm.selectRow = function (index, selectFrom, obName) {
            if (obName == 'itemsReqDtl') {
                vm.selectedReqDtl = selectFrom[index];
                vm.selectedReqDtl1 = selectFrom[index].itemsReqDtl1[0];
                vm.selectedReqDtl2 = selectFrom[index].itemsReqDtl2[0];
                vm.selectedReqDtl3 = selectFrom[index].itemsReqDtl3[0];
            }
            else if (obName == 'itemsReqDtl1') {
                vm.selectedReqDtl1 = selectFrom[index];
            }
            else if (obName == 'itemsReqDtl2') {
                vm.selectedReqDtl2 = selectFrom[index];
            }
            else if (obName == 'itemsReqDtl3') {
                vm.selectedReqDtl3 = selectFrom[index];
            }
        };

        vm.genCollerCuffSize = function (formData) {
            var obLength = 0;
            vm.itemsColorHdr = [];
            obLength = vm.MC_SIZE_LST.length < 1 ? 0 : vm.MC_SIZE_LST.length;
            
            //console.log(vm.itemsColorHdr);
            console.log(formData);
            if (formData.length < 1) {
                for (var i = 0; i < vm.GARM_PART_LST.length; i++) {
                    formData.push({
                        RF_GARM_PART_ID: vm.GARM_PART_LST[i].RF_GARM_PART_ID, itemsSize: []
                    });

                    //formData[i].itemsSize = [];

                    for (var x = 0; x < obLength; x++) {
                        formData[i].itemsSize.push({ MC_SMP_REQ_D3_ID: 0, MC_SMP_REQ_D_ID: 0, RF_GARM_PART_ID: vm.GARM_PART_LST[i].RF_GARM_PART_ID, SIZE_CODE: vm.MC_SIZE_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_LST[x].MC_SIZE_ID, RQD_PCS: 0, MEASUREMENT: '' });
                    };
                };                
            }
            else {
                angular.forEach(formData, function (val, key) {
                    for (var x = 0; x < obLength; x++) {                        
                        var vIndex = _.findIndex(val['itemsSize'], { 'SIZE_CODE': vm.MC_SIZE_LST[x].SIZE_CODE });
                        //alert(vIndex);
                        if (vIndex < 0) {
                            val['itemsSize'].push({ MC_SMP_REQ_D3_ID: 0, MC_SMP_REQ_D_ID: 0, RF_GARM_PART_ID: val['RF_GARM_PART_ID'], SIZE_CODE: vm.MC_SIZE_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_LST[x].MC_SIZE_ID, RQD_PCS: 0, MEASUREMENT: '' });
                        }
                    };
                });
            }

            if (formData.length > 0) {
                vm.selectedReqDtl3 = formData[0];
            };
        };
       
        vm.styleWiseTotal = function (itmReqDtl) {
            var totRqdQty = 0;
            angular.forEach(itmReqDtl.itemsReqDtl2, function (val, key) {
                totRqdQty = totRqdQty + parseInt(val['RQD_QTY']);
            });

            return totRqdQty;
        };               
        
        vm.next = function () {
            $state.go('SmplFabBookEntry.Dtl', { pMC_SMP_REQ_D_ID:0, bAcId: vm.form.MC_BYR_ACC_ID });
        };


    }
})();





/////============== Sample & Fabric Booking Detail Controller =========================
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('smplFabBookDtlController', ['logger', 'config', '$q', '$scope', '$rootScope', '$http', '$modal', 'exception', 'formDtlList', 'bookingData', '$timeout', '$filter', '$state', '$stateParams', 'MrcDataService', 'Dialog', smplFabBookDtlController]);
    function smplFabBookDtlController(logger, config, $q, $scope, $rootScope, $http, $modal, exception, formDtlList, bookingData, $timeout, $filter, $state, $stateParams, MrcDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        //vm.errors = null;
        //vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.anyColorId;
        vm.availColorId;

        vm.pcsMouID = 1;
        vm.setMouID = 2;
        vm.formDtl = [];
        vm.fabBookingData = [];

        vm.MC_COLOR_ID_LST = [];
        vm.MC_SIZE_ID_LST = [];
        vm.styleExtAsortList = [];

        console.log(formDtlList);
        console.log(bookingData);

        if (formDtlList.length > 0) {
            vm.IS_STYLE_GEN = 'N';
        }
        else {
            vm.IS_STYLE_GEN = 'Y';
        }
        
        vm.ItemTemplate = '<h4 style="padding-bottom:0px;margin-bottom:0px;"><b>{{dataItem.ITEM_NAME_EN+" "+dataItem.MODEL_NO}}</b></h4> <div ><p ng-repeat="itm in dataItem.CHILD_ITEMS" style="padding-left:20px;padding-top:0px;padding-bottom:0px;margin-bottom:0px;">{{ itm.ITEM_NAME_EN }}</p></div>';
        vm.ItemValueTemplate = '{{dataItem.ITEM_NAME_EN+" "+dataItem.MODEL_NO}}';
        vm.today = new Date();

        var parent = $rootScope;
        var child = parent.$new();
        vm.params = $stateParams;

        

        vm.targetDateOpened = [];
        vm.targetDateOpen = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.targetDateOpened[index] = true;
        };

        
        vm.activeTabFabBookingFn = function (bool) {            
            $rootScope.activeTabFabBooking = bool;
        };
        vm.activeTabItemColorFn = function (bool) {
            $rootScope.activeTabItemColor=bool;
        };



        //var data = { MC_SMP_REQ_D_ID: -1, MC_SMP_REQ_H_ID: 0, MC_ORDER_H_ID: 0, RF_SMPL_TYPE_ID: 0, LK_SMPL_SRC_TYPE_ID: 0, TARGET_DT: '', REMARKS: '' };
        vm.dataSource = new kendo.data.DataSource({
            data: []
        });
        
        vm.sampleSourceList = [];
        vm.searchTypeList = [{ SEARCH_TYPE_ID: 1, SEARCH_NAME_EN: 'Style#' }, { SEARCH_TYPE_ID: 2, SEARCH_NAME_EN: 'Order#' }];


        activate();
        function activate() {
            var promise = [getFabQtyStyleList(), getAnyAvailColorList()];
            return $q.all(promise).then(function () {
                
                vm.fabBookingData = angular.copy(bookingData);
                vm.getFabReqQty(bookingData);

                if (formDtlList.length > 0) {
                    var obList = angular.copy(formDtlList);

                    vm.sampleSourceList = obList[0].sampleSourceList;
                    
                    //vm.fabBookingData = angular.copy(bookingData);
                    //vm.getFabReqQty(bookingData);
                    
                    angular.forEach(obList, function (val, key) {                                         
                        vm.formDtl.push(val);
                    });

                    //vm.selectDtlRow(0, vm.formDtl, true, 'editMode');
                                                           
                    $timeout(function () {
                        //if (vm.formDtl[0].styleItemsList.length > 0) {                            
                        //    vm.makeActive(vm.formDtl, 0);
                        //}
                    }, 1000);
                    //if (vm.formDtl[0].itemsStyle.length > 0) {
                    //    vm.selectRow(0, vm.formDtl[0].itemsStyle, true);
                    //}
                    
                    console.log(vm.formDtl);
                }
                else {
                    vm.formDtl = [];
                    //vm.formDtl = [{ MC_SMP_REQ_D_ID: -1, MC_SMP_REQ_H_ID: 0, MC_ORDER_H_ID: 0, RF_SMPL_TYPE_ID: 0, LK_SMPL_SRC_TYPE_ID: 0, TARGET_DT: '', REMARKS: '', itemsStyle: [] }];
                    //vm.selectDtlRow(0, vm.formDtl, true, 'newMode');
                }

                vm.showSplash = false;

                //console.log(parent);
                //$scope.dataSource.insert(0, data);                
            });
        };
        
        vm.SEARCH_TYPE_ID = 1;

        //function getSampTypeList(pMC_BUYER_ID) {
        //    return MrcDataService.GetAllOthers('/api/mrc/buyer/SampleListByBuyerID/' + pMC_BUYER_ID).then(function (res) {
        //        obName[index].buyerWiseSampleTypeList = res;
        //    }, function (err) {
        //        console.log(err);
        //    });
        //}

        function getAnyAvailColorList() {
            MrcDataService.GetAllOthers('/api/mrc/ColourMaster/GetAnyAvailColorList').then(function (res) {
                
                angular.forEach(res, function (val, key) {
                    if (val['IS_ANY_COLOR'] == 'Y') {
                        vm.anyColorId = val['MC_COLOR_ID'];
                    }
                    else if (val['IS_AVAILABLE_COLOR'] == 'Y') {
                        vm.availColorId = val['MC_COLOR_ID'];
                    }
                    
                });

            }, function (err) {
                console.log(err);
            });
        };

        function getItemListData() {
            MrcDataService.GetAllOthers('/api/mrc/StyleDItem/StyleDtlItemList/' + null + '/' + 'A').then(function (res) {
                vm.itemListData = res;
                vm.styleItems = new kendo.data.ObservableArray(res);
            }, function (err) {
                console.log(err);
            });
        };

                

        //function getAssortList() {
        //    return MrcDataService.GetAllOthers('/api/mrc/SampleReq/GetAssortList/' + $stateParams.pMC_SMP_REQ_H_ID).then(function (res) {
        //        vm.styleExtAsortList = res;                                              
        //    }, function (err) {
        //        console.log(err);
        //    });
        //};

        //function getRfMouList() {
        //    return MrcDataService.GetAllOthers('/api/common/MOUList/Y').then(function (res) {
        //        $scope.rfMouList = res;
        //        vm.rfMouList = res;                
        //    }, function (err) {
        //        console.log(err);
        //    });
        //};        

        //function getSampleSourceList() {
        //    return MrcDataService.GetAllOthers('/api/common/LookupListData/67').then(function (res) {
        //        $scope.rfMouList = res;
        //        vm.rfMouList = res;
        //    }, function (err) {
        //        console.log(err);
        //    });
        //};

        $scope.$watch('$parent.MC_BYR_ACC_ID_LIST', function (newVal, oldVal) {
            //console.log(oldVal);
            console.log(newVal);
            
            //if (!_.isEqual(newVal, oldVal)) {
                
            if (newVal != '' && angular.isDefined(newVal)) {
                    //if ($stateParams.pMC_SMP_REQ_H_ID > 0) {
                    //    angular.forEach(vm.formDtl, function (val, key) {
                    //        val['buyerAccWiseStyleList']=
                    //    });
                //}
                var ids = newVal.join(',');

                    //if ($stateParams.pMC_SMP_REQ_H_ID < 1) {
                return MrcDataService.GetAllOthers('/api/mrc/StyleHExt/MultiByrAccWiseStyleListData/' + ids + '?&pIS_TNA_FINALIZED=Y&pHAS_SMP_YRN_INHOUSE_TNA=Y').then(function (res) {
                            vm.buyerAccWiseStyleList = res;
                            //vm.formDtl[0].buyerAccWiseStyleList = res;
                        }, function (err) {
                            console.log(err);
                        });
                    //}

                }
            //}
        }, true);
        
        vm.cancelTran = function () {
            vm.errors = undefined;
            $state.go('SmplFabBookEntry.Dtl', { pMC_SMP_REQ_H_ID: 0 }, { inherit: false });
        };

        vm.lsitView = function () {
            return $state.go('SmplFabBookList', { pMC_BYR_ACC_ID: $scope.$parent.MC_BYR_ACC_ID });
        };

        vm.addSmpTypeRow = function (data, copyTo, obName) {            
            return MrcDataService.GetAllOthers('/api/mrc/buyer/SampleListByBuyerID/' + data.MC_BUYER_ID).then(function (res) {
                console.log(copiedData);

                var copiedData = angular.copy(data);
                copiedData.MC_SMP_REQ_D_ID = -1;
                copiedData.RF_SMPL_TYPE_ID = '';
                copiedData.LK_SMPL_SRC_TYPE_ID = '';
                copiedData.testInstList = [];

                MrcDataService.GetAllOthers('/api/mrc/SampleReq/GenTestInsList/' + data.MC_BUYER_ID + '/' + data.MC_STYLE_H_ID + '/'
                    + data.IS_NEED_TEST + '/' + data.IS_ASSORT + '?pMC_ORDER_H_ID=' + copiedData.MC_ORDER_H_ID).then(function (res1) {

                        angular.forEach(res1, function (val, key) {
                            copiedData.testInstList.push(val);
                        });
                }, function (err) {
                    console.log(err);
                });

                copiedData.buyerWiseSampleTypeList = res;
                copyTo.push(copiedData);

            }, function (err) {
                console.log(err);
            });            
        };

        vm.removeSmpTypeRow = function (index, removeFrom, master, data, token) {
            Dialog.confirm('Do you want to remove the symple type?', 'Are you sure?', ['Yes', 'No'])
                 .then(function () {
                     
                    return $http({
                        headers: { 'Authorization': 'Bearer ' + token },
                        url: '/api/mrc/SampleReq/DeleteSampType',
                        method: 'post',
                        data: data
                    })
                    .then(function (res) {
                        $scope.$parent.errors = undefined;
                        if (res.data.success === false) {
                            $scope.$parent.errors = [];
                            $scope.$parent.errors = res.data.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.data.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                removeFrom.splice(index, 1);

                                if (master == true) {
                                    if (removeFrom.length < 1) {
                                        obParent.splice(parentIndex, 1);
                                    }
                                }
                            };

                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });



                 });
        };


        vm.childItemListData = [];
        vm.addItemRow = function (data, copyTo, pMC_STYLE_H_ID) {
            var copiedData = angular.copy(data);
            copiedData.MC_SMP_REQ_D_CFG_ID = -1;
            copiedData.MC_STYLE_D_ITEM_ID = null;
            copiedData.PARENT_ID = null;

            //console.log(data);
            if (copiedData.childItemList == null) {
                copiedData.childItemList = [];
            }

            if (copiedData.childItemList.length < 1) {
                MrcDataService.GetAllOthers('/api/mrc/StyleDItem/ChildItemListByStyle/' + pMC_STYLE_H_ID).then(function (res) {                    
                    copiedData.childItemList = res;
                    //angular.forEach(res, function (val, key) {
                    //    copiedData.childItemList.push(val);
                    //});
                }, function (err) {
                    console.log(err);
                });
            }

            console.log(copiedData);
            copyTo.push(copiedData);
        };

        vm.removeRow = function (index, removeFrom, master, obParent, parentIndex) {
            removeFrom.splice(index, 1);

            if (master == true) {
                if (removeFrom.length < 1) {
                    obParent.splice(parentIndex, 1);
                }
            }
        };

        vm.removeItemTempRow = function (index, removeFrom, master) {
            var id = removeFrom.styleItemsList[index].PARENT_ID;
            var items = angular.copy(removeFrom.styleItemsList);
            //console.log(items);

            id = 0;
            if (id > 0) {
                removeFrom.styleItemsList = [];
                angular.forEach(items, function (val, key) {                    
                    if (id != val['PARENT_ID']) {
                        removeFrom.styleItemsList.push(val);
                    }
                });                
            }
            else {
                removeFrom.styleItemsList.splice(index, 1);
            }

            //if (master == true) {
            //    if (removeFrom.length < 1) {
            //        obParent.splice(parentIndex, 1);
            //    }
            //}
        };

        vm.removeItemRow = function (index, removeFrom, master, data, token) {
            var submitData = angular.copy(data);
            submitData.RF_TEST_INST_ID = removeFrom['RF_TEST_INST_ID'];

            Dialog.confirm('Do you want to remove the item?', 'Are you sure?', ['Yes', 'No'])
                 .then(function () {

                     return $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/mrc/SampleReq/DeleteSampCfg',
                         method: 'post',
                         data: submitData
                     })
                     .then(function (res) {
                         $scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             $scope.$parent.errors = [];
                             $scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 vm.removeItemTempRow(index, removeFrom, master);
                             };

                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });



                 });
        };

        vm.sampleTypeOnSelect = function (e, data) {
            var item = e.sender.dataItem(e.item);
            console.log(item);

            if (item.IS_NEED_TEST == 'Y') {
                data.IS_NEED_TEST = 'Y';
            }
            else {
                data.IS_NEED_TEST = 'N';
            }

            if (item.IS_ASSORT == 'Y') {
                data.IS_ASSORT = 'Y';
            }
            else {
                data.IS_ASSORT = 'N';
            }

            data.testInstList = [];
            return MrcDataService.GetAllOthers('/api/mrc/SampleReq/GenTestInsList/' + data.MC_BUYER_ID + '/' + data.MC_STYLE_H_ID + '/' + item.IS_NEED_TEST + '/' + item.IS_ASSORT + '?pMC_ORDER_H_ID=' + data.MC_ORDER_H_ID).then(function (res1) {
                angular.forEach(res1, function (val, key) {                    
                    data.testInstList.push(val);
                });
            }, function (err) {
                console.log(err);
            });
           
        };

        vm.genStyles = function () {
            
            console.log(vm.GEN_STYLE_H_EXT_LIST);

            if (vm.GEN_STYLE_H_EXT_LIST.length > 0) {
                return MrcDataService.GetAllOthers('/api/mrc/SampleReq/StyleListWiseSampList/' + vm.GEN_STYLE_H_EXT_LIST.join(',')).then(function (res) {
                    var isGenComplete = 'N';
                    angular.forEach(res, function (val, key) {
                        var smplTypeExistsList = _.filter(vm.formDtl, function (ob) {
                            return ob.MC_STYLE_H_EXT_ID == val.MC_STYLE_H_EXT_ID && ob.RF_SMPL_TYPE_ID == val.RF_SMPL_TYPE_ID;
                        });

                        if (smplTypeExistsList.length < 1) {
                            vm.formDtl.push(val);
                        };

                        if (res.length == key) {
                            isGenComplete = 'Y';
                        }
                    });
                    //vm.formDtl = res;
                    if (res.length > 0) {
                        vm.sampleSourceList = res[0].sampleSourceList;
                    }

                    if (isGenComplete == 'Y') {
                        config.appToastMsg('Generate completed');
                    }

                    console.log(vm.formDtl);
                    //vm.formDtl[0].buyerAccWiseStyleList = res;
                }, function (err) {
                    console.log(err);
                });
            }
        };

        vm.selectDtlRow = function (index, selectFrom, master, tranMode) {            
            vm.selectedDtl = selectFrom[index];
            
            //if (tranMode != 'editMode') {
            //    if (vm.selectedDtl.HAS_SET == 'Y') {
            //        vm.formItem.QTY_MOU_ID = vm.setMouID;
            //    }
            //    else {
            //        vm.formItem.QTY_MOU_ID = vm.pcsMouID;
            //    }
            //}

            if (parseInt(vm.selectedDtl.MC_STYLE_H_ID) > 0) {
                vm.styleModelItems = _.filter(vm.itemListData, { 'MC_STYLE_H_ID': parseInt(vm.selectedDtl.MC_STYLE_H_ID), 'PARENT_ID': null });
                getColorList(parseInt(vm.selectedDtl.MC_STYLE_H_ID));
            }

            //if (tranMode != 'editMode') {
            //    if (vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle.length > 0) {
            //        vm.selectedItem = vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle[0];
            //    }
            //}
            
        };

        vm.genItemColorSize = function (formData, obName) {
            
            //console.log(vm.MC_STYLE_D_ITEM_ID_LST);
            var vItems = [];
            angular.forEach(vm.MC_STYLE_D_ITEM_ID_LST, function (valModel, keyModel) {
                var vCount = 0;
                if (valModel['CHILD_ITEMS'].length > 0) {
                    angular.forEach(valModel['CHILD_ITEMS'], function (valCh, keyCh) {
                        
                        if(vCount == 0){
                            valCh['IS_SHOW_HDR'] = 'Y';
                            vCount = vCount + 1;
                        }
                        else {
                            valCh['IS_SHOW_HDR'] = 'N';
                        }

                        valCh['PARENT_ITEM_ID'] = valModel['MC_STYLE_D_ITEM_ID'];
                        vItems.push(valCh);
                    });
                    //vItems = valModel['CHILD_ITEMS'];
                }
                else {
                    if (vCount == 0) {
                        valModel['IS_SHOW_HDR'] = 'Y';
                        vCount = vCount + 1;
                    }
                    valModel['PARENT_ITEM_ID'] = '';
                    vItems.push(valModel);
                }
                //vm.styleModelItems = _.filter(vm.itemListData, { 'MC_STYLE_H_ID': parseInt(vm.selectedDtl.MC_STYLE_H_ID), 'PARENT_ID': null });
                //console.log(vItems);

                var obLength = 0;
                vm.itemsColorHdr = [];
                obLength = vm.MC_SIZE_ID_LST.length < 1 ? 0 : vm.MC_SIZE_ID_LST.length;

                var isNewColor = 'N';
                var obItmColorList = [];
                var obItmColorLength = 0;
                if (vm.MC_COLOR_ID_LST.length > 0) {
                    isNewColor = 'Y';
                    obItmColorLength = vm.MC_COLOR_ID_LST.length;
                    obItmColorList = vm.MC_COLOR_ID_LST;
                }

                angular.forEach(vItems, function (valItem, keyItem) {

                    //formData.push({
                    //    MC_STYLE_D_ITEM_ID: valItem['MC_STYLE_D_ITEM_ID'], PARENT_ID: valItem['PARENT_ID'], ITEM_SNAME: valItem['ITEM_SNAME'], itemsColor: []
                    //});

                    console.log(valItem);
                    valItem['itemsColor'] = [];

                    for (var i = 0; i < obItmColorLength; i++) {

                        //var vItmColorIndex = _.findIndex(formData, { 'MC_COLOR_ID': obItmColorList[i].MC_COLOR_ID });

                        //if (vItmColorIndex < 0) {
                        valItem['itemsColor'].push({
                                ITEM_INDEX: 0, COLOR_INDEX: 0, MC_COLOR_ID: obItmColorList[i].MC_COLOR_ID, COLOR_NAME_EN: obItmColorList[i].COLOR_NAME_EN, COMBO_NO: null,
                                MC_ORDER_SIZE_ID: 0, TOTAL_QTY: 0, itemsSize: [] //vm.itemsColorHdr
                                /*RQD_QTY: null, QTY_MOU_ID: null, MOU_NAME_EN: null, UNIT_PRICE: null,
                                RF_CURRENCY_ID: null, TOTAL_SIZE_PRICE: null*/
                        });
                        //}

                            

                        for (var x = 0; x < obLength; x++) {
                            var vIndex = _.findIndex(valItem['itemsColor'][i].itemsSize, { 'SIZE_CODE': vm.MC_SIZE_ID_LST[x].SIZE_CODE });

                            console.log(valItem['itemsColor'][i].itemsSize);

                            if (vIndex < 0) {
                                valItem['itemsColor'][i].itemsSize.push({ MC_SMP_REQ_D1_ID: 0, MC_SMP_REQ_D_ID: 0, MC_STYLE_D_ITEM_ID: valItem.MC_STYLE_D_ITEM_ID, PARENT_ITEM_ID: valItem.PARENT_ITEM_ID, QTY_MOU_ID: valItem.QTY_MOU_ID, SIZE_CODE: vm.MC_SIZE_ID_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_ID_LST[x].MC_SIZE_ID, RQD_QTY: 0 });
                            }
                            //formData[i].itemsSize.push({ MC_ORDER_SIZE_ID: 0, MC_ORDER_STYL_ID: 0, SIZE_CODE: vm.MC_SIZE_ID_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_ID_LST[x].MC_SIZE_ID, RQD_QTY: 0, UNIT_PRICE: itmUnitPrice });
                        };
                    };

                    //angular.forEach(vm.MC_COLOR_ID_LST, function (valColor, key1) {
                    //    angular.forEach(vm.MC_SIZE_ID_LST, function (valSize, key3) {
                    //        formData.push({
                    //            MC_STYLE_D_ITEM_ID: valItem['MC_STYLE_D_ITEM_ID'], PARENT_ID: valItem['PARENT_ID'], ITEM_SNAME: valItem['ITEM_SNAME'],
                    //            MC_COLOR_ID: valColor['MC_COLOR_ID'], MC_SIZE_ID: valSize['MC_SIZE_ID'],
                    //            MC_COLOR_ID_LIST: vm.buyerColorList
                    //        });
                    //    });
                    //});
                });
            });


            angular.forEach(vItems, function (val, key) {
                //var vItmIndex = _.findIndex(valItem['itemsColor'][i].itemsSize, { 'SIZE_CODE': vm.MC_SIZE_ID_LST[x].SIZE_CODE });

                formData.push(val);
            });
            
            console.log(formData);
        };

        vm.addColorRow = function (data, copyTo) {
            var copiedData = angular.copy(data);
            copiedData.MC_SMP_REQ_D2_ID = -1;
            
            angular.forEach(copiedData.GMT_PARTS, function (val, key) {
                angular.forEach(val['GMT_PARTS_QTY'], function (val1, key1) {
                    val1['MC_SMP_REQ_D2_ID'] = -1;
                });
            });
            
            console.log(copiedData);
            copyTo.push(copiedData);
        };

        vm.removeColorRow = function (index, removeFrom, master, data, data1, token) {
            var submitData = angular.copy(data);
            submitData.MC_SMP_REQ_STYL_ID = data['MC_SMP_REQ_STYL_ID'];
            submitData.MC_COLOR_ID = data1['MC_COLOR_ID'];

            console.log(data.MC_COLOR_ID);
            console.log(removeFrom.MC_SMP_REQ_STYL_ID);

            Dialog.confirm('Do you want to remove the item?', 'Are you sure?', ['Yes', 'No'])
                 .then(function () {

                     return $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/mrc/SampleReq/DeleteSampFabColor',
                         method: 'post',
                         data: submitData
                     })
                     .then(function (res) {
                         $scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             $scope.$parent.errors = [];
                             $scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 removeFrom.splice(index, 1);
                             };

                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });



                 });
        };
        
        vm.selectRow = function (index, selectFrom, master, obName) {
            vm.sizeGrandTotal = 0;
            vm.allChildItemsColor = [];

            if (obName == 'itmStyleChild') {
                vm.selectedChildItem = selectFrom[index];

                //console.log(vm.form.itemsStyle[vm.selectedItem.ITEM_INDEX]);
                //console.log(vm.form.itemsStyle[vm.selectedItem.ITEM_INDEX].childItems);
            }
            else {
                vm.selectedItem = selectFrom[index];
                vm.selectedChildItem = null;
            }

            if (vm.selectedDtl.HAS_SET == 'Y' && obName != 'itmStyleChild') {
                //console.log(vm.selectedItem.childItems);
                angular.forEach(vm.selectedItem.childItems, function (val, key) {
                    angular.forEach(val['itemsColor'], function (val1, key1) {
                        //console.log(val1);
                        vm.allChildItemsColor.push(val1);
                    });
                });
            }

            getItemWiseTotal(obName);
        };

        vm.genColorSize = function (formData, obName) {

            var obLength = 0;
            vm.itemsColorHdr = [];
            obLength = vm.MC_SIZE_ID_LST.length < 1 ? 0 : vm.MC_SIZE_ID_LST.length;


            var obItmColorList = [];
            var obItmColorLength = 0;
            if (vm.MC_COLOR_ID_LST.length > 0) {
                isNewColor = 'Y';
                obItmColorLength = vm.MC_COLOR_ID_LST.length;
                obItmColorList = vm.MC_COLOR_ID_LST;
            }
            else if (obName == 'itmStyleChild' && vm.MC_COLOR_ID_LST.length < 1) {
                var isNewColor = 'Y';
                angular.forEach(vm.selectedItem.childItems, function (val, key) {
                    obItmColorLength = obItmColorLength + val['itemsColor'].length;
                    angular.forEach(val['itemsColor'], function (val1, key1) {
                        isNewColor = 'N';
                        var childItmColorIndex = -1;
                        if (obItmColorList.length > 0) {
                            childItmColorIndex = _.findIndex(obItmColorList, { 'MC_COLOR_ID': val1['MC_COLOR_ID'] });
                        }
                        if (childItmColorIndex < 0) {
                            obItmColorList.push(val1);
                        }
                    });
                });
                //obItmColorLength = vm.selectedItem.childItems[0].itemsColor.length;
                //obItmColorList = vm.selectedItem.childItems[0].itemsColor;
            }
            else {
                obItmColorLength = formData.length;
                obItmColorList = formData;
            }
            //console.log('ddd');
            //console.log(obItmColorList);

            //var itmUnitPrice = vm.selectedItem.UNIT_PRICE;
            //if (obName == 'itmStyleChild') {
            //    itmUnitPrice = vm.selectedChildItem.UNIT_PRICE;
            //}

            if (vm.formDtl[vm.selectedDtl.DETAIL_INDEX].HAS_SET == 'Y' && vm.selectedItem.childItems.length > 0) {

                angular.forEach(vm.selectedItem.childItems, function (val, key) {
                    //itmUnitPrice = val['UNIT_PRICE'];
                    if (isNewColor == 'Y') {
                        for (var i = 0; i < obItmColorLength; i++) {

                            //if (isNewColor == 'Y') {
                            val['itemsColor'].push({
                                DETAIL_INDEX: 0, ITEM_INDEX: 0, COLOR_INDEX: 0, MC_COLOR_ID: obItmColorList[i].MC_COLOR_ID, COLOR_NAME_EN: obItmColorList[i].COLOR_NAME_EN, COMBO_NO: null,
                                MC_ORDER_SIZE_ID: 0, TOTAL_QTY: 0, itemsSize: [] //vm.itemsColorHdr
                                /*RQD_QTY: null, QTY_MOU_ID: null, MOU_NAME_EN: null, UNIT_PRICE: null,
                                RF_CURRENCY_ID: null, TOTAL_SIZE_PRICE: null*/
                            });

                            for (var x = 0; x < obLength; x++) {
                                var vIndex = _.findIndex(val['itemsColor'][i].itemsSize, { 'SIZE_CODE': vm.MC_SIZE_ID_LST[x].SIZE_CODE });
                                if (vIndex < 0) {
                                    val['itemsColor'][i].itemsSize.push({ MC_SMP_REQ_D1_ID: 0, MC_SMP_REQ_D_ID: 0, MC_STYLE_D_ITEM_ID: val['MC_STYLE_D_ITEM_ID'], QTY_MOU_ID: val['QTY_MOU_ID'], SIZE_CODE: vm.MC_SIZE_ID_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_ID_LST[x].MC_SIZE_ID, RQD_QTY: 0 });
                                }
                            };
                        };
                    }
                    else {
                        for (var i = 0; i < val['itemsColor'].length; i++) {

                            for (var x = 0; x < obLength; x++) {
                                var vIndex = _.findIndex(val['itemsColor'][i].itemsSize, { 'SIZE_CODE': vm.MC_SIZE_ID_LST[x].SIZE_CODE });
                                if (vIndex < 0) {
                                    val['itemsColor'][i].itemsSize.push({ MC_SMP_REQ_D1_ID: 0, MC_SMP_REQ_D_ID: 0, MC_STYLE_D_ITEM_ID: val['MC_STYLE_D_ITEM_ID'], QTY_MOU_ID: val['QTY_MOU_ID'], SIZE_CODE: vm.MC_SIZE_ID_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_ID_LST[x].MC_SIZE_ID, RQD_QTY: 0 });
                                }
                            };
                        };
                    }
                });

                vm.selectRow(vm.selectedItem.ITEM_INDEX, vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle, true, 'itmStyleParent');
            }
            else {
                for (var i = 0; i < obItmColorLength; i++) {

                    var vItmColorIndex = _.findIndex(formData, { 'MC_COLOR_ID': obItmColorList[i].MC_COLOR_ID });

                    if (vItmColorIndex < 0) {
                        formData.push({
                            ITEM_INDEX: 0, COLOR_INDEX: 0, MC_COLOR_ID: obItmColorList[i].MC_COLOR_ID, COLOR_NAME_EN: obItmColorList[i].COLOR_NAME_EN, COMBO_NO: null,
                            MC_ORDER_SIZE_ID: 0, TOTAL_QTY: 0, itemsSize: [] //vm.itemsColorHdr
                            /*RQD_QTY: null, QTY_MOU_ID: null, MOU_NAME_EN: null, UNIT_PRICE: null,
                            RF_CURRENCY_ID: null, TOTAL_SIZE_PRICE: null*/
                        });
                    }

                    for (var x = 0; x < obLength; x++) {
                        var vIndex = _.findIndex(formData[i].itemsSize, { 'SIZE_CODE': vm.MC_SIZE_ID_LST[x].SIZE_CODE });

                        console.log(formData[i].itemsSize);

                        if (vIndex < 0) {
                            formData[i].itemsSize.push({ MC_SMP_REQ_D1_ID: 0, MC_SMP_REQ_D_ID: 0, MC_STYLE_D_ITEM_ID: vm.selectedItem.MC_STYLE_D_ITEM_ID, QTY_MOU_ID: vm.selectedItem.QTY_MOU_ID, SIZE_CODE: vm.MC_SIZE_ID_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_ID_LST[x].MC_SIZE_ID, RQD_QTY: 0 });
                        }
                        //formData[i].itemsSize.push({ MC_ORDER_SIZE_ID: 0, MC_ORDER_STYL_ID: 0, SIZE_CODE: vm.MC_SIZE_ID_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_ID_LST[x].MC_SIZE_ID, RQD_QTY: 0, UNIT_PRICE: itmUnitPrice });
                    };
                };
            }

            vm.MC_COLOR_ID_LST = [];
        };        

        vm.colorWiseTotal = function (itemColor, obName) {
            var totColorQty = 0;
            angular.forEach(itemColor.itemsSize, function (val, key) {
                totColorQty = totColorQty + parseInt(val['RQD_QTY']);
            });

            //console.log(vm.form.itemsStyle[vm.selectedItem.ITEM_INDEX].itemsColor);
            //vm.sizeGrandTotal = 0;
            //angular.forEach(vm.form.itemsStyle[vm.selectedItem.ITEM_INDEX].itemsColor, function (val, key) {                
            //    angular.forEach(val['itemsSize'], function (val1, key1) {
            //        vm.sizeGrandTotal = parseInt(vm.sizeGrandTotal) + parseInt(val1['RQD_QTY']);
            //    });
            //});

            //getItemWiseTotal(obName);

            return totColorQty;
        };

        function getItemWiseTotal(obName) {
            //alert(vm.selectedItem.ITEM_INDEX);
            if (vm.selectedItem.ITEM_INDEX == null) {
                vm.selectedItem.ITEM_INDEX = 0;
            }
            vm.sizeGrandTotal = 0;

            //if (obName == 'itmStyleChild') {
            //    angular.forEach(vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle[vm.selectedItem.ITEM_INDEX].childItems[vm.selectedChildItem.CHILD_ITEM_INDEX].itemsColor, function (val, key) {
            //        angular.forEach(val['itemsSize'], function (val1, key1) {
            //            vm.sizeGrandTotal = parseInt(vm.sizeGrandTotal) + parseInt(val1['RQD_QTY']);
            //        });
            //    });
            //}
            //else if (obName == 'itmStyleParent') {
            //    angular.forEach(vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle[vm.selectedItem.ITEM_INDEX].childItems, function (val, key) {
            //        angular.forEach(val['itemsColor'], function (val1, key1) {
            //            angular.forEach(val1['itemsSize'], function (val2, key3) {
            //                vm.sizeGrandTotal = parseInt(vm.sizeGrandTotal) + parseInt(val2['RQD_QTY']);
            //            });
            //        });

            //    });
            //}
            //else {
            //    angular.forEach(vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle[vm.selectedItem.ITEM_INDEX].itemsColor, function (val, key) {
            //        angular.forEach(val['itemsSize'], function (val1, key1) {
            //            vm.sizeGrandTotal = parseInt(vm.sizeGrandTotal) + parseInt(val1['RQD_QTY']);
            //        });
            //    });
            //}

            return;
        };

        vm.sizeWiseSumQty = function (itmSizeQty, obName) {
            var colorSumQty = 0;

            if (obName == 'itmStyleParent') {
                angular.forEach(vm.allChildItemsColor, function (val, key) {
                    angular.forEach(val['itemsSize'], function (val1, key1) {
                        if (val1['MC_SIZE_ID'] == itmSizeQty.MC_SIZE_ID) {
                            colorSumQty = colorSumQty + parseInt(val1['RQD_QTY']);
                        }
                    });

                });
            }
            else if (obName == 'itmStyleChild') {
                angular.forEach(vm.selectedChildItem.itemsColor, function (val, key) {
                    angular.forEach(val['itemsSize'], function (val1, key1) {
                        if (val1['MC_SIZE_ID'] == itmSizeQty.MC_SIZE_ID) {
                            colorSumQty = colorSumQty + parseInt(val1['RQD_QTY']);
                        }
                    });

                });
            }
            else if (obName == 'itmStyle') {
                angular.forEach(vm.selectedItem.itemsColor, function (val, key) {
                    angular.forEach(val['itemsSize'], function (val1, key1) {
                        if (val1['MC_SIZE_ID'] == itmSizeQty.MC_SIZE_ID) {
                            colorSumQty = colorSumQty + parseInt(val1['RQD_QTY']);
                        }
                    });

                });
            }

            return colorSumQty;
        };

        function getColorList(pMC_STYLE_H_ID) {
            //alert(pMC_STYLE_H_ID);
            return MrcDataService.GetAllOthers('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + parseInt(pMC_STYLE_H_ID)).then(function (res) {
                vm.buyerColorList = res;                
            }, function (err) {
                console.log(err);
            });
        };

        vm.isAllStyleColor = function (item) {
            //console.log(item);
            var itm = angular.copy(item);

            if (item.IS_ALL_COL == 'Y') {
                item.MC_COLOR_LIST = _.map(_.filter(itm.styleWiseColorList, function (ob) {
                    return (ob.MC_COLOR_ID != vm.anyColorId && ob.MC_COLOR_ID != vm.availColorId);
                }), 'MC_COLOR_ID');
                //item.MC_COLOR_LIST = _.map(itm.styleWiseColorList, 'MC_COLOR_ID');
            }
            else {
                item.MC_COLOR_LIST = [];
            }
        };

        vm.isAnyStyleColor = function (item) {
            if (item.IS_ANY_COL == 'Y') {
                item.MC_COLOR_LIST = _.filter(item.MC_COLOR_LIST, function (val) {
                    return (val != vm.anyColorId && val != vm.availColorId);
                });
            }
        }

        vm.onChangeGmtColorStylItm = function (item) {
            if (item.MC_COLOR_LIST != null) {
                if (item.MC_COLOR_LIST.length != null) {
                    if (item.MC_COLOR_LIST.length != item.styleWiseColorList.length) {
                        item.IS_ALL_COL = 'N';

                        var isAnyColor = _.some(item.MC_COLOR_LIST, function (ob) {                            
                            return ob == vm.anyColorId;
                        });
                        var isAvailableColor = _.some(item.MC_COLOR_LIST, function (ob) {
                            return ob == vm.availColorId;
                        });

                        if (isAnyColor == true) {
                            item.IS_ANY_COL = 'Y';
                            item.IS_AVAILABLE_COL = 'N';
                            item.MC_COLOR_LIST = _.filter(item.MC_COLOR_LIST, function (ob) {
                                return (ob != vm.anyColorId && ob != vm.availColorId);
                            });// [vm.anyColorId];
                        }
                        else if (isAvailableColor == true) {
                            item.IS_ANY_COL = 'N';
                            item.IS_AVAILABLE_COL = 'Y';
                            item.MC_COLOR_LIST = [vm.availColorId];
                        }
                    }
                }
            }
        };

        function getSizeList() {
            return MrcDataService.selectAllData('SizeMaster').then(function (res) {
                vm.sizeList = res;
                console.log(vm.sizeList);
            }, function (err) {
                console.log(err);
            });
        };

        vm.onChangeTBC = function (item) {
            if (item.IS_SZ_TBC == 'Y') {
                item.MC_SIZE_LIST = []; //_.map(item.styleWiseColorList, 'MC_COLOR_ID');
            }
        };

        vm.onChangeSize = function (item) {
            if (item.MC_SIZE_LIST != null) {
                if (item.MC_SIZE_LIST.length != null) {
                    item.IS_SZ_TBC = 'N';
                }
            }
        };

        function getByrWiseSmplTyleList(data) {
            console.log(data);

            if (data.buyerWiseSampleTypeList == undefined) {
                data.buyerWiseSampleTypeList = [];
                return MrcDataService.GetAllOthers('/api/mrc/buyer/SampleListByBuyerID/' + data.MC_BUYER_ID).then(function (res) {                                        
                    data.buyerWiseSampleTypeList = res;                    
                }, function (err) {
                    console.log(err);
                });
            }
        }

        vm.makeActive = function (items, $index) {
            angular.forEach(items, function (val, key) {
                if ($index != key) {
                    val['IS_ACTIVE'] = false;                    
                }
            });

            //console.log(items[$index]);
            getByrWiseSmplTyleList(items[$index]);

            if (items[$index]['IS_ASSORT'] == 'N') {
                items[$index]['IS_ACTIVE'] = !items[$index]['IS_ACTIVE'];

                if (items[$index]['testInstList'].length < 1) {
                    return MrcDataService.GetAllOthers('/api/mrc/SampleReq/SampWiseItmCfgList/' + items[$index]['MC_SMP_REQ_D_ID'] + '/' + items[$index]['IS_NEED_TEST'] + '/'
                                                                                                + items[$index]['MC_STYLE_H_ID'] + '?pMC_ORDER_H_ID=' + items[$index]['MC_ORDER_H_ID']).then(function (res) {
                        angular.forEach(res, function (val1, key1) {
                            items[$index]['testInstList'].push(val1);
                        });
                    }, function (err) {
                        console.log(err);
                    });
                }
                console.log(items[$index]);
            }
            else {
                console.log('c');
            }
        };

        vm.getCopyFromSmplType = function (e, itmDtl) {
            var item = e.sender.dataItem(e.item);

            var copyForm = _.filter(vm.formDtl, function (ob) {
                return ob.MC_STYLE_H_EXT_ID == itmDtl.MC_STYLE_H_EXT_ID && ob.RF_SMPL_TYPE_ID == item.RF_SMPL_TYPE_ID;
            });

            //console.log(item);
            //console.log(copyForm);
            //vm.formDtl

            if (copyForm[0]['testInstList'].length < 1) {
                var index = (copyForm[0]['DETAIL_INDEX'])-1;
                return MrcDataService.GetAllOthers('/api/mrc/SampleReq/SampWiseItmCfgList/' + vm.formDtl[index]['MC_SMP_REQ_D_ID'] + '/' + vm.formDtl[index]['IS_NEED_TEST'] + '/'
                    + vm.formDtl[index]['MC_STYLE_H_ID'] + '?pMC_ORDER_H_ID=' + vm.formDtl[index]['MC_ORDER_H_ID']).then(function (res) {
                        angular.forEach(res, function (val1, key1) {
                            var testInst = angular.copy(val1);
                            vm.formDtl[index]['testInstList'].push(testInst);

                            //console.log(val1);
                            //console.log(testInst);                            
                        });
                    }, function (err) {
                        console.log(err);
                    });
            }
            
        }

        vm.copyItemFromSmpType = function (itmDtl, smplTypeId) {
            
            //var copyForm = getCopyFromSmplType(itmDtl, smplTypeId);
            var copyForm = _.filter(vm.formDtl, function (ob) {
                return ob.MC_STYLE_H_EXT_ID == itmDtl.MC_STYLE_H_EXT_ID && ob.RF_SMPL_TYPE_ID == smplTypeId;
            });
            console.log(copyForm);

            var itemList = copyForm[0].testInstList[0].styleItemsList;
                                   
            //console.log(itmDtl);

           
            console.log(itemList);

            if (copyForm[0]['MC_STYLE_H_EXT_ID']) {
                angular.forEach(itmDtl.testInstList, function (val, key) {
                    val['styleItemsList'] = [];
                    angular.forEach(itemList, function (val1, key1) {
                        console.log(val1);
                        var itms = angular.copy(val1);
                        itms['MC_SMP_REQ_D_CFG_ID'] = 0;

                        val['styleItemsList'].push(itms);
                    });
                    
                });
            }

            //console.log('==============');
            //console.log(itmDtl);
        }

        vm.styleItemAdd = function () {
            if (vm.formItem.MC_STYLE_D_ITEM_ID == "" || vm.formItem.MC_STYLE_D_ITEM_ID == null) {
                alert('Please select item');
                return;
            }
            else if (vm.formItem.ORDER_QTY == "" || vm.formItem.ORDER_QTY == null) {
                alert('Please input item quantity');
                return;
            }            
            else if (vm.formItem.QTY_MOU_ID == "" || vm.formItem.QTY_MOU_ID == null) {
                alert('Please select item unit');
                return;
            }


            vm.setItems = [];
            if (vm.formDtl[vm.selectedDtl.DETAIL_INDEX].HAS_SET == 'Y') {

                vm.parentItem = _.filter(vm.itemListData, { 'MC_STYLE_H_ID': parseInt(vm.selectedDtl.MC_STYLE_H_ID), 'MC_STYLE_D_ITEM_ID': parseInt(vm.formItem.MC_STYLE_D_ITEM_ID) });
                vm.childItems = _.filter(vm.itemListData, { 'MC_STYLE_H_ID': parseInt(vm.selectedDtl.MC_STYLE_H_ID), 'PARENT_ID': parseInt(vm.formItem.MC_STYLE_D_ITEM_ID) });
                angular.forEach(vm.childItems, function (val, key) {
                    vm.parentItem.push(angular.copy(val));
                });

                //console.log(vm.parentItem);

                vm.setItems = _.sortBy(vm.parentItem, 'MC_STYLE_D_ITEM_ID'); //ITEM_CODE

                //console.log(vm.setItems);

                angular.forEach(vm.setItems, function (val, key) {
                    var vItemQty = 0;
                    var vItemUnitPrice = 0;
                    var vItemTotPrice = 0;
                    var vItemMOUId = 1;

                    if (val['PARENT_ID'] == null) {
                        vItemQty = vm.formItem.ORDER_QTY;                        
                        vItemMOUId = vm.setMouID;

                        //var cboList = new kendo.data.ObservableArray(val['COMBO_NO'].split(','));
                        //vm.comboList = [];
                        //angular.forEach(cboList, function (val1, key1) {
                        //    vm.comboList.push({ COMBO_NO: val1 });
                        //});

                        vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle.push({
                            ITEM_INDEX: 0, DETAIL_INDEX: vm.selectedDtl.DETAIL_INDEX, MC_STYLE_D_ITEM_ID: val['MC_STYLE_D_ITEM_ID'], ITEM_NAME_EN: val['ITEM_NAME_EN'] + ' ' + val['MODEL_NO'], PARENT_ITEM_ID: val['PARENT_ID'],
                            ORDER_QTY: vItemQty, QTY_MOU_ID: vItemMOUId, IS_ACTIVE: false,
                            childItems: [], itemsColor: []
                        });
                    }
                    else {
                        vItemQty = vm.formItem.ORDER_QTY; //(vm.formItem.ORDER_QTY / (vm.setItems.length - 1));
                        vItemUnitPrice = (vm.formItem.UNIT_PRICE / (vm.setItems.length - 1));
                        vItemTotPrice = (vm.formItem.UNIT_PRICE / (vm.setItems.length - 1)) * vItemQty;
                        vItemMOUId = vm.pcsMouID;

                        var vItemStyleIndex = _.findIndex(vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle, { 'MC_STYLE_D_ITEM_ID': val['PARENT_ID'] });
                        //alert(vItemStyleIndex);
                        //console.log(vm.form.itemsStyle);

                        vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle[vItemStyleIndex].childItems.push({
                            ITEM_INDEX: 0, DETAIL_INDEX: vm.selectedDtl.DETAIL_INDEX, MC_STYLE_D_ITEM_ID: val['MC_STYLE_D_ITEM_ID'], ITEM_NAME_EN: val['ITEM_NAME_EN'] + ' ' + val['MODEL_NO'], PARENT_ITEM_ID: val['PARENT_ID'],
                            ORDER_QTY: vItemQty, QTY_MOU_ID: vItemMOUId, IS_ACTIVE: false,
                            itemsColor: []
                        });
                        //console.log(vm.form.itemsStyle);
                    }


                });
            }
            else {
                
                var vItemQty = vm.formItem.ORDER_QTY;
                var vItemUnitPrice = vm.formItem.UNIT_PRICE;
                var vItemTotPrice = vm.formItem.TOTAL_PRICE;
                var vItemMOUId = vm.formItem.QTY_MOU_ID;

                vm.parentItem = _.filter(vm.itemListData, { 'MC_STYLE_D_ITEM_ID': parseInt(vm.formItem.MC_STYLE_D_ITEM_ID) });

                angular.forEach(vm.parentItem, function (val, key) {
                    vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle.push({
                        ITEM_INDEX: 0, DETAIL_INDEX: vm.selectedDtl.DETAIL_INDEX, MC_STYLE_D_ITEM_ID: val['MC_STYLE_D_ITEM_ID'], ITEM_NAME_EN: val['ITEM_NAME_EN'] + ' ' + val['MODEL_NO'], PARENT_ITEM_ID: val['PARENT_ID'],
                        ORDER_QTY: vItemQty, QTY_MOU_ID: vItemMOUId, UNIT_PRICE: vItemUnitPrice, TOTAL_PRICE: vItemTotPrice, IS_ACTIVE: false,
                        childItems: [], itemsColor: [] 
                    });
                });
            }


            vm.selectedItem = vm.formDtl[vm.selectedDtl.DETAIL_INDEX].itemsStyle[0];

            vm.formItem.MC_STYLE_D_ITEM_ID = '';
            vm.formItem.ORDER_QTY = 0;
            //vm.formItem.UNIT_PRICE = 0;
            //vm.formItem.TOTAL_PRICE = 0;
            //vm.formItem.QTY_MOU_ID = vm.pcsMouID;

            //console.log(vm.form.itemsStyle);

            vm.MC_COLOR_ID_LST = vm.buyerColorList;
        };

        //vm.addNew = function () {
        //    var data = { MC_SMP_REQ_D_ID: -1, MC_SMP_REQ_H_ID: 0, MC_ORDER_H_ID: 0, RF_SMPL_TYPE_ID: 0, LK_SMPL_SRC_TYPE_ID: 0, TARGET_DT: null, REMARKS: '' };
        //    vm.dataSource.insert(0, data);
        //};
        
        vm.styleOnSelect = function (e, index) {
            var item = e.sender.dataItem(e.item);
            //console.log(item);
            getStyleWiseOrderAndSampType(item, index, vm.formDtl);            
        };

        vm.styleWiseOrderList = [];
        function getStyleWiseOrderAndSampType(item, index, obName) {
            if (parseInt(item.MC_STYLE_H_EXT_ID) > 0 && item.MC_STYLE_H_EXT_ID != '') {
                obName[index].MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                obName[index].HAS_SET = item.HAS_SET;

                MrcDataService.GetAllOthers('/api/mrc/buyer/SampleListByBuyerID/' + item.MC_BUYER_ID).then(function (res) {
                    obName[index].buyerWiseSampleTypeList = res;
                }, function (err) {
                    console.log(err);
                });

                var orderListURL;
                if (item.HAS_EXT == 'Y') {
                    orderListURL = '/api/mrc/Order/WorkStyleListByUserBuyerAcc/null/' + item.MC_STYLE_H_ID + '?&pIS_TNA_FINALIZED=Y';
                }
                else {
                    orderListURL = '/api/mrc/Order/WorkStyleListByUserBuyerAcc/' + item.MC_STYLE_H_EXT_ID + '/null?&pIS_TNA_FINALIZED=Y';
                }

                MrcDataService.GetAllOthers(orderListURL).then(function (res) {
                    obName[index].styleWiseOrderList = res;
                }, function (err) {
                    console.log(err);
                });

            }
            else {
                //alert('else');
                vm.styleWiseOrderList = [];
            }
        };               

        function getItemListData() {
            MrcDataService.GetAllOthers('/api/mrc/StyleDItem/StyleDtlItemList/' + null + '/' + 'A').then(function (res) {
                vm.itemListData = res;// new kendo.data.ObservableArray(res);
                vm.styleItems = new kendo.data.ObservableArray(res);
            }, function (err) {
                console.log(err);
            });
        };

        
        vm.onChangeFabColor = function (e, itm) {
            var data = e.sender.dataItem(e.sender.select());
            console.log(data);

            itm.LK_COL_TYPE_ID = data.LK_COL_TYPE_ID;
            itm.COLOR_NAME_EN = data.COLOR_NAME_EN;
        };
   
        vm.getSmplStyleCheck = function (submitData, token) {
            var deferred = $q.defer();
            $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: '/api/mrc/SampleReq/GetSmplStyleCheck',
                method: 'post',
                data: submitData
            })
            .then(function (res) {
                $scope.$parent.errors = undefined;
                if (res.data.success === false) {
                    $scope.$parent.errors = [];
                    $scope.$parent.errors = res.data.errors;
                }
                else {
                     
                    var dt = JSON.parse(res.data.jsonStr);

                    if (dt.PMSG.substr(0, 9) == 'MULTI-003') {
                        deferred.resolve({ success: true, msg : dt.PMSG  });
                    }
                    else if (dt.PMSG.substr(0, 9) == '') {
                        deferred.resolve({ success: true, msg: 'Do you want to save?' });
                    }
                    else {
                        deferred.reject({ success: false, msg: 'Failed' });
                    }
                  
             
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
                deferred.reject({ success: false, msg: 'Http request Failed' });

            });
            return deferred.promise;     
        };

        vm.submitData = function (data, token) {
            //console.log(data);
            var vMC_BYR_ACC_LST = angular.copy($scope.$parent.form['MC_BYR_ACC_ID_LIST']);
            var vSMP_REQ_TO = angular.copy($scope.$parent.reqToList); //angular.copy($scope.$parent.form['SMP_REQ_TO_LIST']);
            var vSMP_REQ_ATTN = angular.copy($scope.$parent.form['SMP_REQ_ATTN_LIST']);

            console.log(vSMP_REQ_TO);
            if (vSMP_REQ_TO == null || vSMP_REQ_TO == undefined) {
                $scope.$parent.errors = [];
                $scope.$parent.errors.push({ key: ['Please reselect issue to list'] });
                return;
            }
            //return;

            $scope.$parent.form.MC_BYR_ACC_LST = vMC_BYR_ACC_LST == null ? '' : vMC_BYR_ACC_LST.join(",");
            $scope.$parent.form.SMP_REQ_TO = vSMP_REQ_TO == null ? '' : vSMP_REQ_TO.join(",");
            $scope.$parent.form.SMP_REQ_ATTN = ''; //'79,193,204,205'; //vSMP_REQ_ATTN == null ? '' : vSMP_REQ_ATTN.join(",");
            
            var submitData = angular.copy($scope.$parent.form);
            submitData.LK_STYL_DEV_TYP_ID = $stateParams.pLK_STYL_DEV_TYP_ID||0;
            submitData.DTL_XML = [];

            var reqD = 1; //Detail
            var reqDCFG = 1; //Item
              
            
            console.log(submitData);
            console.log(vm.formDtl);
            console.log(vm.fabBookingData);
            //return;

            var vXML = [];
            var vDTL_XML = [];
            var vCFG_XML = [];
            var ob = {};
            
            if ($stateParams.pLK_STYL_DEV_TYP_ID == 542) { // Devlopment Style (Only fabric booking)
                angular.forEach(vm.fabBookingData, function (val, key) {
                    vDTL_XML.push({
                        MC_SMP_REQ_STYL_ID: val['MC_SMP_REQ_STYL_ID'] == null ? 0 : val['MC_SMP_REQ_STYL_ID'],
                        MC_ORDER_H_ID: val['MC_ORDER_H_ID']
                    });
                });
            }
            else {
                angular.forEach(vm.formDtl, function (val, key) {
                    vDTL_XML.push({
                        DETAIL_INDEX: reqD,
                        MC_SMP_REQ_D_ID: val['MC_SMP_REQ_D_ID'] == null ? 0 : val['MC_SMP_REQ_D_ID'],
                        MC_SMP_REQ_STYL_ID: val['MC_SMP_REQ_STYL_ID'] == null ? 0 : val['MC_SMP_REQ_STYL_ID'],
                        MC_ORDER_H_ID: val['MC_ORDER_H_ID'],
                        RF_SMPL_TYPE_ID: val['RF_SMPL_TYPE_ID'],
                        LK_SMPL_SRC_TYPE_ID: val['LK_SMPL_SRC_TYPE_ID'],
                        TARGET_DT: val['TARGET_DT'] == null ? '' : $filter('date')(val['TARGET_DT'], vm.dtFormat),
                        REMARKS: val['REMARKS']
                    });

                    angular.forEach(val['testInstList'], function (val1, key1) {
                        reqDCFG = 1;

                        angular.forEach(val1['styleItemsList'], function (val2, key2) {
                            ob = {};

                            ob.DETAIL_INDEX = reqD;
                            ob.DETAIL_INDEX_CFG = reqDCFG;
                            ob.RF_TEST_INST_ID = val1['RF_TEST_INST_ID'];

                            ob.MC_SMP_REQ_D_CFG_ID = val2['MC_SMP_REQ_D_CFG_ID'];
                            ob.MC_STYLE_D_ITEM_ID = val2['MC_STYLE_D_ITEM_ID'];
                            ob.IS_ALL_COL = val2['IS_ALL_COL'];
                            ob.IS_ANY_COL = val2['IS_ANY_COL'];
                            ob.IS_AVAILABLE_COL = val2['IS_AVAILABLE_COL'];

                            if (val['IS_ASSORT'] != 'Y') {
                                if (val2['MC_COLOR_LIST'].length > 0) {
                                    ob.MC_COLOR_LST = val2['MC_COLOR_LIST'].join(',');
                                }
                                if (val2['IS_SZ_TBC'] == 'N') {
                                    if (val2['MC_SIZE_LIST'].length > 0) {
                                        ob.MC_SIZE_LST = val2['MC_SIZE_LIST'].join(',');
                                    }
                                }
                            }
                            ob.RQD_QTY = val2['RQD_QTY'];
                            ob.IS_SZ_TBC = val2['IS_SZ_TBC'];
                            ob.CONFIRM_DT = val2['CONFIRM_DT'] == null ? "" : $filter('date')(val2['CONFIRM_DT'], vm.dtFormat);
                            ob.QTY_MOU_ID = val2['QTY_MOU_ID'];
                            ob.REMARKS_CFG = val2['REMARKS'];

                            vXML.push(ob);
                            vCFG_XML.push(ob);
                            reqDCFG = reqDCFG + 1;
                        });
                    });
                    reqD = reqD + 1;
                });

            }
            console.log(vDTL_XML);
            console.log(vCFG_XML);

            

            submitData.DTL_XML = MrcDataService.xmlStringShort(vDTL_XML.map(function (ob) {
                return ob;
            }));

            submitData.CFG_XML = MrcDataService.xmlStringShort(vCFG_XML.map(function (ob) {
                return ob;
            }));

            
            submitData.ASSORT_XML = MrcDataService.xmlStringShort(vm.styleExtAsortList.map(function (ob) {
                var x = _.filter(vDTL_XML, { 'MC_ORDER_H_ID': ob.MC_ORDER_H_ID, 'RF_SMPL_TYPE_ID': 15 });
                //console.log('===============');
                //console.log(x);

                return { 
                    DETAIL_INDEX: x[0].DETAIL_INDEX, MC_SMP_REQ_D_ASORT_ID: ob.MC_SMP_REQ_D_ASORT_ID, MC_SMP_REQ_D_ID: ob.MC_SMP_REQ_D_ID, MC_ORDER_H_ID: ob.MC_ORDER_H_ID,
                    STYLE_NO: ob.STYLE_NO, ASSORT_QTY: ob.ASSORT_QTY, QTY_MOU_ID: ob.QTY_MOU_ID
                };
            }));
            


            var vXMLFabQty = [];
            angular.forEach(vm.fabBookingData, function (val, key) {
                angular.forEach(val['STYLE_COLORS'], function (val1, key1) {
                    angular.forEach(val1['GMT_PARTS'], function (val2, key2) {
                        angular.forEach(val2['GMT_PARTS_QTY'], function (val3, key3) {
                            val3['MC_SMP_REQ_STYL_ID'] = val['MC_SMP_REQ_STYL_ID'];
                            val3['MC_COLOR_ID'] = val1['MC_COLOR_ID'];
                            val3['LK_YD_TYPE_ID'] = val1['LK_YD_TYPE_ID'];
                            val3['COLOR_SPEC'] = val1['COLOR_SPEC'];
                            val3['IS_CONTRAST'] = val1['IS_CONTRAST'];
                            val3['GMT_COLOR_ID'] = val1.GMT_COLOR_ID;

                            //if (val1.GMT_COLOR_ID_LST != null) {
                            //    if (val1.GMT_COLOR_ID_LST.length > 0) {
                            //        val3['GMT_COLOR_LST'] = val1.GMT_COLOR_ID_LST.join(','); 
                            //    }
                            //}

                            //alert(val.STYLE_COLORS[0].GMT_PARTS[0].FIN_DIA);

                            val3['MC_STYLE_D_FAB_ID'] = val2['MC_STYLE_D_FAB_ID'];

                            //console.log('---------');
                            //console.log(val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST);
                            if (val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST != null) {
                                if (val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST.length > 0) {
                                    val3['RF_GARM_PART_LST'] = val.STYLE_COLORS[0].GMT_PARTS[key2].RF_GARM_PART_ID_LST.join(','); //val2['RF_GARM_PART_LST'];
                                }
                            }
                            val3['FIN_DIA'] = val.STYLE_COLORS[0].GMT_PARTS[key2].FIN_DIA; //val2['FIN_DIA'];
                            val3['DIA_MOU_ID'] = val.STYLE_COLORS[0].GMT_PARTS[key2].DIA_MOU_ID; //val2['DIA_MOU_ID'];
                            val3['LK_DIA_TYPE_ID'] = val.STYLE_COLORS[0].GMT_PARTS[key2].LK_DIA_TYPE_ID; //val2['LK_DIA_TYPE_ID'];
                            //val2['OTHER_SPEC']
                        
                            vXMLFabQty.push(val3);
                        });
                    });
                });
            });
            
            console.log(vXMLFabQty);
            //console.log(vm.formFabQty);

            submitData.FAB_QTY_XML = MrcDataService.xmlStringShort(vXMLFabQty.map(function (ob) {
                return {
                    MC_SMP_REQ_D2_ID: ob.MC_SMP_REQ_D2_ID, MC_SMP_REQ_STYL_ID: ob.MC_SMP_REQ_STYL_ID,
                    MC_STYLE_D_FAB_ID: ob.MC_STYLE_D_FAB_ID, RF_GARM_PART_LST: ob.RF_GARM_PART_LST,
                    FIN_DIA: ob.FIN_DIA, DIA_MOU_ID: ob.DIA_MOU_ID, LK_DIA_TYPE_ID: ob.LK_DIA_TYPE_ID,
                    RQD_QTY: ob.RQD_QTY, QTY_MOU_ID: ob.QTY_MOU_ID, MC_COLOR_ID: ob.MC_COLOR_ID, GMT_COLOR_ID: ob.GMT_COLOR_ID,
                    LK_YD_TYPE_ID: ob.LK_YD_TYPE_ID, COLOR_SPEC: ob.COLOR_SPEC, IS_CONTRAST: ob.IS_CONTRAST
                };
            }));
            
            console.log('submitData');
            console.log(submitData);
            //return;
            
            vm.getSmplStyleCheck(submitData, token).then(function (res) {

                console.log(res);
                var vConfirmMsg = 'Do you want to save?';
                if (res.msg.substr(0, 9) == 'MULTI-003') {
                    //vConfirmMsg = res.msg.substr(9) + '</br></br> <b>Do you want to create another program for this style#?</b>';
                    alert(res.msg.substr(9));
                    return;
                }
                else {
                    Dialog.confirm(vConfirmMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                        return $http({
                            headers: { 'Authorization': 'Bearer ' + token },
                            url: '/api/mrc/SampleReq/BatchSave',
                            method: 'post',
                            data: submitData
                        })
                        .then(function (res) {
                            $scope.$parent.errors = undefined;
                            if (res.data.success === false) {
                                $scope.$parent.errors = [];
                                $scope.$parent.errors = res.data.errors;
                            }
                            else {

                                res['data'] = angular.fromJson(res.data.jsonStr);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    vm.isSaved = true;
                                }
                                else {
                                    $scope.$parent.errors = [];
                                    $scope.$parent.errors.push({ key: [res.data.PMSG] });
                                }

                                if (res.data.PMC_SMP_REQ_H_ID_RTN != 0) {
                                    $scope.$parent.form.MC_SMP_REQ_H_ID = res.data.PMC_SMP_REQ_H_ID_RTN;
                                    $scope.$parent.form.SMP_REQ_NO = res.data.PSMP_REQ_NO_RTN;

                                    $state.go('SmplFabBookEntry.Dtl', { pMC_SMP_REQ_H_ID: res.data.PMC_SMP_REQ_H_ID_RTN }, { reload: 'SmplFabBookEntry.Dtl' });

                                }
                                config.appToastMsg(res.data.PMSG);
                            }
                        }).catch(function (message) {
                            exception.catcher('XHR loading Failded')(message);
                        });
                    });
                }

            });

            


        };

        vm.printBookingRecord = function (dataItem) {
            //console.log(dataItem);

            $scope.$parent.form.REPORT_CODE = 'RPT-2000';
            $scope.$parent.form.MC_SMP_REQ_H_ID = dataItem.MC_SMP_REQ_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = $scope.$parent.form;

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

        vm.onChangeReqQty = function () {
            vm.getFabReqQty(vm.fabBookingData);
        }

        vm.getFabReqQty = function (fabBkList) {
            vm.TOTAL_FAB_QTY = 0;
            angular.forEach(fabBkList, function (val, key) {
                
                angular.forEach(val['STYLE_COLORS'], function (val1, key1) {                
                    angular.forEach(val1['GMT_PARTS'], function (val2, key2) {
                
                        angular.forEach(val2['GMT_PARTS_QTY'], function (val3, key3) {                
                            vm.TOTAL_FAB_QTY = vm.TOTAL_FAB_QTY + val3['RQD_QTY'];
                        });
                    });
                });
            });
            
            //console.log(vm.fabBookingData);
        }

        vm.sendToSample = function (data, token) {
            var submitData = angular.copy($scope.$parent.form);

            //==== Start For update pending mail ==========
            MrcDataService.getDataByFullUrl('/api/mrc/SampleReq/GetMailSendPendingListSmplProg').then(function (res) {

                angular.forEach(res, function (val, key) {

                    var submitPendingData = val; //{ MC_BLK_FAB_REQ_H_ID: val['MC_BLK_FAB_REQ_H_ID'], MC_BLK_REVISION_NO: val['REVISION_NO'] };

                    submitPendingData['REPORT_CODE'] = 'RPT-2000';
                    submitPendingData['IS_EXPORT_TO_DISK'] = 'Y';
                    
                    return $http({
                        method: 'post',
                        url: '/api/mrc/MrcReport/PreviewReportRDLC',
                        data: submitPendingData
                    }).then(function (res) {
                        return res.data;
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
            });
            //==== End For update pending mail ==========


            Dialog.confirm('Do you want to submit?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     return $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/mrc/SampleReq/SendToSample',
                         method: 'post',
                         data: submitData
                     })
                     .then(function (res) {
                         $scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             $scope.$parent.errors = [];
                             $scope.$parent.errors = res.data.errors;
                             console.log(res.data.errors);
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);
                             config.appToastMsg(res.data.PMSG);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 $scope.$parent.form['MC_TNA_TASK_STATUS_ID'] = 14;
                                 
                                 submitData['REPORT_CODE'] = 'RPT-2000';
                                 submitData['IS_EXPORT_TO_DISK'] = 'Y';

                                 return $http({
                                     method: 'post',
                                     url: '/api/mrc/MrcReport/PreviewReportRDLC',
                                     data: submitData
                                 }).then(function (res) {
                                     return res.data;
                                 }).catch(function (message) {
                                     exception.catcher('XHR loading Failded')(message);
                                 });
                             };
                             
                            
                             
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
        };

        //=========== Start for Fabric Qty ===================
        function getFabQtyStyleList() {
            return MrcDataService.GetAllOthers('/api/mrc/SampleReq/SamFabQtyStyleListByHID/' + $stateParams.pMC_SMP_REQ_H_ID).then(function (res) {
                vm.formFabQty = res;
            }, function (err) {
                console.log(err);
            });
        };

        //$scope.$watch('vm.formFabQty', function (newVal, oldVal) {
        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {

        //            $scope.$parent.sampFabQty = vm.formFabQty;
        //        }
        //    }
        //}, true);


        vm.fabQtyMakeActive = function (items, $index) {
            angular.forEach(items, function (val, key) {
                if ($index != key) {
                    val['IS_ACTIVE'] = false;
                }
            })
            items[$index]['IS_ACTIVE'] = !items[$index]['IS_ACTIVE'];
        };

        vm.fabQtySelectRow = function (index, selectFrom, master, obName) {
            if (obName == 'formFabQty') {
                vm.selectedFabQty = selectFrom[index];
            }
            //else {
            //    vm.selectedItem = selectFrom[index];
            //    vm.selectedChildItem = null;
            //}                      
        };

        vm.fabQtyAddRow = function (data, copyTo, obName) {
            var copiedData = angular.copy(data);

            if (obName == 'formFabQty') {
                copiedData.MC_SMP_REQ_D2_ID = 0;
                copiedData.FIN_DIA = "";

                angular.forEach(copiedData.itemsColorQty, function (val, key) {
                    val['MC_SMP_REQ_D3_ID'] = 0;
                    val['RQD_QTY'] = 0;
                });
            }

            copyTo.push(copiedData);
        };

        vm.fabQtyRemoveRow = function (index, removeFrom, master) {
            removeFrom.splice(index, 1);
        };

        vm.onChangeContrast = function (itm) {

            console.log(itm);

            itm['GMT_COLOR_ID'] = itm['MC_COLOR_ID'];

            if (itm['IS_CONTRAST'] == 'Y') {
                itm['COLOR_SPEC'] = 'Contrast for ' + itm['COLOR_NAME_EN'];
            }
            else {
                itm['COLOR_SPEC'] = '';
            }

        }

        vm.onChangeGmtColor4Fab = function (item) {
            if (item.GMT_COLOR_ID != null) {
                

                    var selectedGmtColorList = _.filter(item.STYLE_COLORS_LIST, function (ob) {
                                                
                        return ob['MC_COLOR_ID'] == item.GMT_COLOR_ID;
                    });

                    console.log(selectedGmtColorList);

                    if (item['IS_CONTRAST'] == 'Y') {

                        var colorName = "";
                        angular.forEach(selectedGmtColorList, function (val, key) {
                            colorName = colorName + val['COLOR_NAME_EN'] + ", ";
                        });

                        item['COLOR_SPEC'] = 'Contrast for ' + colorName.substring(0, colorName.length-2);
                    }
                
            }
        };
        //=========== End for Fabric Qty ===================
        
        vm.styleQtyChange = function (index) {
            vm.showGMTPartDia = index;
            vm.styleModelIndex = index;
        };

        vm.styleModelIndex = -1;
        $scope.gmtPartModal = function (pMC_STYLE_H_ID, index) {
            vm.styleModelIndex = index;

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'GMTPartModal.html',
                controller: 'GMTPartModalController',
                size: 'small',
                //scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    styleFabData: function (MrcDataService) {
                        if (angular.isDefined(pMC_STYLE_H_ID) && pMC_STYLE_H_ID > 0) {
                            return MrcDataService.getDataByUrl('/StyleDItemFab/SelectFabByStyleID/' + pMC_STYLE_H_ID).then(function (res) {
                                return res;
                            }, function (err) {
                                console.log(err);
                            });
                        }
                        else {
                            return {};
                        }
                    },
                    mouListData: function (MrcDataService) {
                        if (angular.isDefined(pMC_STYLE_H_ID) && pMC_STYLE_H_ID > 0) {
                            return MrcDataService.GetAllOthers('/api/common/MOUList/Y').then(function (res) {
                                return res;
                            }, function (err) {
                                console.log(err);
                            });
                        }
                        else {
                            return {};
                        }
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
                var vGmtParts = angular.copy(data);
                angular.forEach(vm.fabBookingData[vm.styleModelIndex].STYLE_COLORS, function (val, key) {
                    angular.forEach(vGmtParts, function (val1, key1) {
                        console.log(val);
                        console.log(val1);
                        val['GMT_PARTS_QTY'].push({
                            MC_SMP_REQ_D2_ID: 0, FIN_DIA: val1.FIN_DIA, DIA_MOU_ID: val1.DIA_MOU_ID, MC_STYLE_D_FAB_ID: val1.MC_STYLE_D_FAB_ID,
                            RF_GARM_PART_LST: val1.RF_GARM_PART_LST, LK_DIA_TYPE_ID: val1.LK_DIA_TYPE_ID,
                            FIN_DIA_DESC: val1.FIN_DIA_DESC, RF_GARM_PART_NAME_LST: val1.RF_GARM_PART_NAME_LST, FAB_TYPE_SNAME: val1.FAB_TYPE_SNAME
                        });
                    });
                });
                
                console.log(vm.fabBookingData);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        
        $scope.smpReqAsortModal = function (items, index) {
            var smpReqDtlID = items[index].MC_SMP_REQ_D_ID;
            var assortList = [];

            if (vm.styleExtAsortList.length < 1) {
                //assortList = _.map(_.groupBy(items, function (doc) {
                //    if (doc.IS_ASSORT == 'Y') {
                //        return doc.STYLE_NO;
                //    }
                //}), function (grouped) {
                //    return grouped[0];
                //});

                assortList = _.filter(items, function (doc) {
                    return doc.IS_ASSORT == 'Y';
                });
            }

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'SmpReqAsortModal.html',
                controller: 'SmpReqAsortModalController',
                size: 'small',
                //scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    styleExtAsortData: function (MrcDataService) {
                        console.log(vm.styleExtAsortList);
                        if (vm.styleExtAsortList.length < 1 || vm.styleExtAsortList==null) {
                            
                            return MrcDataService.GetAllOthers('/api/mrc/SampleReq/GetAssortList/' + $stateParams.pMC_SMP_REQ_H_ID).then(function (res) {
                                if (res.length > 0) {
                                    angular.forEach(res, function (val, key) {
                                        val['ASSORT_QTY'] = val['RQD_QTY'];
                                    });
                                    
                                    vm.styleExtAsortList = res;
                                    return res;
                                }
                                else {
                                    vm.styleExtAsortList = assortList;
                                    return assortList;
                                }
                            }, function (err) {
                                console.log(err);
                            });
                        }
                        else {                            
                            return vm.styleExtAsortList;
                        }
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log('received');
                console.log(data);

                vm.styleExtAsortList = angular.copy(data);
                               
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };


        $scope.smpBkRevisionModal = function () {
           
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'SmplBkRevisionModal.html',
                controller: 'SmplBkRevisionModalController',
                size: 'md',
                //scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    smpHdrData: function () {
                        return $scope.$parent.form;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                //console.log('received');
                console.log(data);

                var vRevisionData = angular.copy(data);

                $scope.$parent.form.MC_SMP_REQ_H_ID = vRevisionData.MC_SMP_REQ_H_ID;
                $scope.$parent.form.REVISION_NO = vRevisionData.REVISION_NO;
                $scope.$parent.form.REVISION_DT = vRevisionData.REVISION_DT;
                $scope.$parent.form.REV_REASON = vRevisionData.REV_REASON;
                $scope.$parent.form.MC_TNA_TASK_STATUS_ID = vRevisionData.MC_TNA_TASK_STATUS_ID;
                $scope.$parent.form.IS_ITEM_REV = vRevisionData.IS_ITEM_REV;
                $scope.$parent.form.IS_FAB_REV = vRevisionData.IS_FAB_REV;
                                
                $state.go('SmplFabBookEntry.Dtl', { pMC_SMP_REQ_H_ID: vRevisionData.MC_SMP_REQ_H_ID }, { reload: 'SmplFabBookEntry.Dtl' });


            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.addFabricColumn = function (itmStylColors, dataItem) {
            console.log(dataItem);
            console.log(itmStylColors);

            angular.forEach(itmStylColors, function (val, key) {

                val['GMT_PARTS'].push({
                    DIA_MOU_ID: dataItem['DIA_MOU_ID'], FAB_TYPE_DESC: dataItem['FAB_TYPE_DESC'], FIN_DIA: '', FIN_DIA_DESC: '',
                    LK_DIA_TYPE_ID: dataItem['LK_DIA_TYPE_ID'], MC_STYLE_D_FAB_ID: dataItem['MC_STYLE_D_FAB_ID'], RF_GARM_PART_ID_LST: dataItem['RF_GARM_PART_ID_LST'],
                    GMT_PARTS_QTY: [{ MC_SMP_REQ_D2_ID: 0, QTY_MOU_ID: 3, RQD_QTY: null, QTY_MOU_LIST: val['GMT_PARTS'][0]['GMT_PARTS_QTY'][0]['QTY_MOU_LIST'] }]
                });

            });
        }



    }

})();




// Start Asort Controller
(function () {
    'use strict';

    angular.module('multitex').controller('SmpReqAsortModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'styleExtAsortData', 'MrcDataService', SmpReqAsortModalController]);
    function SmpReqAsortModalController($modalInstance, $q, $scope, $http, $state, $filter, config, styleExtAsortData, MrcDataService) {

        $scope.showSplash = true;
        //$scope.MC_BLK_FAB_REQ_H_ID = angular.copy(mcBlkFabReqHID);
        //$scope.plDataList = new kendo.data.ObservableArray([]);

        console.log(styleExtAsortData);

        activate();

        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;
        $scope.QTY_MOU_ID = 26;

        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                $scope.showSplash = false;
            });
        }


        $scope.asortGridOption = {
            //pageable: true,
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
                { field: "STYLE_NO", title: "Style", type: "string", width: '170px' },
                {
                    title: "Assortment",
                    template: function () {
                        return "<input type='number' ng-model='dataItem.ASSORT_QTY' name='gray-{{dataItem.uid}}' class='form-control' />";
                    },
                    width: "80px"
                }
            ]            
        };

        $scope.asortGridDataSource = new kendo.data.DataSource({
            data: styleExtAsortData
        });

        //function getAssortmentDataList() {
        //    $scope.asortDataList = new kendo.data.DataSource({
        //        data: styleExtAsortData
        //    });
        //};



        $scope.save = function (token, valid) {
            //if (!valid) return;
            console.log($scope.asortDataList);
            var asortList = $scope.asortGridDataSource.data();
            var obj = {};
            obj = asortList.map(function (ob) {
                return {
                    DETAIL_INDEX: ob.DETAIL_INDEX, MC_SMP_REQ_D_ASORT_ID: ob.MC_SMP_REQ_D_ASORT_ID > 0 ? ob.MC_SMP_REQ_D_ASORT_ID : 0,
                    MC_SMP_REQ_D_ID: ob.MC_SMP_REQ_D_ID, MC_ORDER_H_ID: ob.MC_ORDER_H_ID, STYLE_NO: ob.STYLE_NO, ASSORT_QTY: ob.ASSORT_QTY,
                    QTY_MOU_ID: ob.MC_SMP_REQ_D_ASORT_ID > 0 ? ob.QTY_MOU_ID : $scope.QTY_MOU_ID
                };
            });

            console.log(obj);
            return $modalInstance.close(obj);
        };


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();
// End Asort Controller



// Start GMT Part Model Controller
(function () {
    'use strict';

    angular.module('multitex').controller('GMTPartModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'styleFabData', 'mouListData', 'MrcDataService', GMTPartModalController]);
    function GMTPartModalController($modalInstance, $q, $scope, $http, $state, $filter, config, styleFabData, mouListData, MrcDataService) {
       
        //var vm = this;

        var defaultDiaMouID = 23;
        //vm.defaultQtyMouID = 5;
        //vm.defaultConsMouID = 3;
        //vm.defaultGmtPart = ["1"];        
        //vm.defaultFabGrp = 193;
        var defaultDiaType = 327;

        $scope.showSplash = true;
        $scope.styleFabList = angular.copy(styleFabData);
        $scope.mouList = angular.copy(mouListData);
        //$scope.MC_BLK_FAB_REQ_H_ID = angular.copy(mcBlkFabReqHID);
        //$scope.plDataList = new kendo.data.ObservableArray([]);
        console.log(styleFabData);

        $scope.gmtPartDiaDataList = new kendo.data.ObservableArray([]);
        //$scope.gmtPartDiaDataList = new kendo.data.DataSource({
        //    data: res
        //});

        activate();

        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;


        function activate() {
            var promise = [getGmtPartList(), getDiaTypeList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);                
                $scope.DIA_MOU_ID = defaultDiaMouID;
                $scope.LK_DIA_TYPE_ID = defaultDiaType;

                $scope.showSplash = false;
            });
        };

        
        function getGmtPartList() {
            return MrcDataService.GetAllOthers('/api/common/GmtPartList').then(function (res) {
                $scope.gmtPartList = res;
            }, function (err) {
                console.log(err);
            });
        };

        //function getMouList() {
        //    return MrcDataService.GetAllOthers('/api/common/MOUList/Y').then(function (res) {
        //        $scope.mouList = res;                
        //    }, function (err) {
        //        console.log(err);
        //    });
        //};

        function getDiaTypeList() {
            return MrcDataService.GetAllOthers('/api/common/LookupListData/67').then(function (res) {
                $scope.diaTypeList = res;                
            }, function (err) {
                console.log(err);
            });
        };

        $scope.FIN_DIA_UNIT_NAME = "Inch";
        $scope.finDiaUnitOnChange = function (e) {
            var item = e.sender.dataItem(e.sender.select());
            $scope.FIN_DIA_UNIT_NAME = item.MOU_CODE;
            //$scope.FIN_DIA_DESC=
        };

        $scope.FIN_DIA_TYPE_NAME = "Open";
        $scope.finDiaTypeOnChange = function (e) {
            var item = e.sender.dataItem(e.sender.select());
            $scope.FIN_DIA_TYPE_NAME = item.LK_DATA_NAME_EN;
        };

        $scope.fabOnChange = function (e) {
            var item = e.sender.dataItem(e.item); //e.sender.dataItem(e.sender.select());
            $scope.FAB_TYPE_SNAME = item.FAB_TYPE_SNAME;
        };

        $scope.addToGrid = function () {
            var gmtPartList = _.filter($scope.gmtPartList, function (ob) {
                return _.some($scope.RF_GARM_PART_ID_LST, function (val) {
                    return ob.RF_GARM_PART_ID == val;
                });
            });
            $scope.RF_GARM_PART_NAME_LST = _.map(gmtPartList, 'GARM_PART_NAME').join('+');

            var vDiaDesc = $scope.FIN_DIA + ' ' + $scope.FIN_DIA_UNIT_NAME + ' ' + $scope.FIN_DIA_TYPE_NAME;
            var vPartList = $scope.RF_GARM_PART_ID_LST.join(',');
            

            $scope.gmtPartDiaDataList.push({
                RF_GARM_PART_LST: vPartList, RF_GARM_PART_ID_LST: $scope.RF_GARM_PART_ID_LST, RF_GARM_PART_NAME_LST: $scope.RF_GARM_PART_NAME_LST, MC_STYLE_D_FAB_ID: $scope.MC_STYLE_D_FAB_ID,
                FIN_DIA: $scope.FIN_DIA, DIA_MOU_ID: $scope.DIA_MOU_ID, LK_DIA_TYPE_ID: $scope.LK_DIA_TYPE_ID, FIN_DIA_UNIT_NAME: $scope.FIN_DIA_UNIT_NAME, FIN_DIA_TYPE_NAME: $scope.FIN_DIA_TYPE_NAME,
                FIN_DIA_DESC: vDiaDesc, FAB_TYPE_SNAME: $scope.FAB_TYPE_SNAME, RQD_QTY: 0
            });
        };

        $scope.plColumns = [
            { field: "RF_GARM_PART_NAME_LST", title: "GMT Part", type: "string", width: '170px' },
            { field: "FIN_DIA", title: "Dia", type: "string", editable: true, width: '80px' },
            { field: "MC_STYLE_D_FAB_ID", title: "Fabric", type: "string", editable: true, width: '80px' }
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
            //console.log(obj);

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
                        $scope.cancel();
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
        
        $scope.generate = function () {
            $modalInstance.close($scope.gmtPartDiaDataList);
        };
    }

})();
// End GMT Part Model Controller



// Start Sample Booking Revision Controller
(function () {
    'use strict';

    angular.module('multitex').controller('SmplBkRevisionModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'MrcDataService', 'smpHdrData', 'Dialog', SmplBkRevisionModalController]);
    function SmplBkRevisionModalController($modalInstance, $q, $scope, $http, $state, $filter, config, MrcDataService, smpHdrData, Dialog) {

        //$scope = angular.copy(smpHdrData);

        $scope.showSplash = true;

        activate();

        $scope.today = new Date();
        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.IS_ITEM_REV = 'Y';
        $scope.IS_FAB_REV = 'Y';
        $scope.REVISION_DT = $scope.today;

        //var blkBookID = angular.copy(mcBlkFabReqHID);

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
                MC_SMP_REQ_H_ID: smpHdrData.MC_SMP_REQ_H_ID, HR_COMPANY_ID: smpHdrData.HR_COMPANY_ID, MC_BYR_ACC_LST: smpHdrData.MC_BYR_ACC_LST,
                SMP_REQ_ATTN: smpHdrData.SMP_REQ_ATTN, SMP_REQ_DT: smpHdrData.SMP_REQ_DT,
                REVISION_DT: $filter('date')($scope.REVISION_DT, $scope.dtFormat), REV_REASON: $scope.REV_REASON,
                IS_ITEM_REV: $scope.IS_ITEM_REV, IS_FAB_REV: $scope.IS_FAB_REV
            };

            Dialog.confirm('Do you want to revision?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {
                    
                     $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/mrc/SampleReq/BatchSaveRevise',
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

                                 submitData['MC_SMP_REQ_H_ID'] = res.data.PMC_SMP_REQ_H_ID_RTN;
                                 submitData['REVISION_NO'] = res.data.PREVISION_NO_RTN;
                                 submitData['IS_BFB_FINALIZED'] = "N";
                                 submitData['MC_TNA_TASK_STATUS_ID'] = 13;
                                 submitData['IS_ITEM_REV'] = $scope.IS_ITEM_REV;
                                 submitData['IS_FAB_REV'] = $scope.IS_FAB_REV;

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
// End Sample Booking Revision Controller
