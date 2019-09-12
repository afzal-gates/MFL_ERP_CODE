//////============= OrderH Controller ====================
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('OrderHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$timeout', '$state', '$stateParams', '$modal', 'formData', 'ordStatusList', 'MrcDataService', OrderHController]);
    function OrderHController(logger, config, $q, $scope, $http, exception, $filter, $timeout, $state, $stateParams, $modal, formData, ordStatusList, MrcDataService) {
      
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;        
        vm.dtFormat = config.appDateFormat;
        vm.Title = $state.current.Title;
        
        vm.orderStatusList = angular.copy(ordStatusList);
        vm.resolveOrdData = angular.copy(formData);

        formData['HR_COUNTRY_ID_LST'] = formData['HR_COUNTRY_ID_LST'] ? formData['HR_COUNTRY_ID_LST'].split(',') : [];
        console.log(formData);



        $scope.cntryList = [];
        //$scope.cntryDataSource = new kendo.data.DataSource({
        //    data: []
        //});
        //if (formData[key]) {
        //    $scope.cntryDataSource = new kendo.data.DataSource({
        //        data: formData['HR_COUNTRY_ID_LST'].split(',')
        //    });
        //}
        //else {
        //    $scope.cntryDataSource = new kendo.data.DataSource({
        //        data: []
        //    });
        //}
        

        var key = 'MC_ORDER_H_ID';
        vm.today = new Date();
        
        vm.form = formData[key] ? formData : {
            MC_ORDER_H_ID: 0, MC_BLK_FAB_REQ_H_ID: 0, IS_MCR: 'N', IS_MULTI_SHIP_DT: 'N',
            LK_ORD_STATUS_ID: 363, RF_CURRENCY_ID: vm.defaultCurrID, itmsOrdShipDT: [
                {
                    MC_ORDER_SHIP_ID: 0, MC_ORDER_H_ID: 0, SHIP_DT: $filter('date')(vm.today, vm.dtFormat), SHIP_DESC: 'Shipment-1', IS_ACTIVE: true, itemsStyle: [],
                    formItem: { QTY_MOU_ID: '', ORDER_QTY: null, UNIT_PRICE: null, TOTAL_PRICE: null }
                } /*{ MC_ORDER_SHIP_ID: 0, MC_ORDER_H_ID: 0, SHIP_DT: $filter('date')(vm.today, vm.dtFormat), SHIP_DESC: '2nd', itemsStyle: [] }*/
            ]
        };

        $scope.templateOrderStatus = '<span class="#: LOOKUP_DATA_ID==365 ? \"k-state-disabled\": \"\"#">#=data.LK_DATA_NAME_EN#</span>';
        $scope.onSelectIOrderStatus = function (e) {
            $(".k-state-disabled").parent().click(false);
        };

        //if (formData[key]) {
        //    $scope.form
        //}
        
        vm.shipDateList = [];
        
        

        //if (formData.IS_ORD_FINALIZED == 'Y') {
        //    $(".controlReadonly").attr('disabled', true);            
        //}
        //else {
        //    $(".controlReadonly").attr('disabled', false);
        //}
        
        vm.defaultCurrID = 2;
        
        //$timeout( function(){ $scope.callAtTimeout(); }, 3000);
        console.log(formData[key]);

        activate();
        function activate() {
            var promise = [/*OrderStatusList(),*/ getOrderTypeList(), getBuyerAcList(), getCountryList(),
                /*getSizeList(),*/ getRF_MouList(), getRF_CurrencyList(), getTnaTempList()];
            return $q.all(promise).then(function () {
                  
                //vm.form.RF_CURRENCY_ID = vm.defaultCurrID;

                $timeout(function () {
                    //if (formData[key]) {
                    //    vm.form = formData[key] ? formData : {
                    //        MC_ORDER_H_ID: 0, MC_BLK_FAB_REQ_H_ID: 0, IS_MCR: 'N', IS_MULTI_SHIP_DT: 'N',
                    //        LK_ORD_STATUS_ID: 363, RF_CURRENCY_ID: vm.defaultCurrID, itemsStyle: []
                    //    };
                    //}
                }, 500);

                vm.showSplash = false;                               
            });
        }

        $scope.$watch('vm.form', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                    //console.log($scope.form.REVISION_NO);
                }
            }
        }, true);

        //added By Aminul for Tna Template Selection
        function getTnaTempList() {
            return vm.TnaTempList = {
                optionLabel: "-- TNA Template --",
                filter: "startswith",
                autoBind: true,
                template: '<span><h4 style="padding:0px;margin-top: 0px;margin-bottom: 0px;">#: data.TNA_TMPLT_CODE #</h4><p style="padding:0px;margin-top: 0px;margin-bottom: 0px;"> <small>#: data.REMARKS #</small></p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByUrl('/StyleH/TnAList/OrderType/null/Buyer/null/LeadTime/null').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "TNA_TMPLT_CODE",
                dataValueField: "MC_TNA_TMPLT_H_ID"
            };
        }
        //added By Aminul for Tna Template Selection
        $scope.$watchGroup(['vm.form.ORD_CONF_DT', 'vm.form.SHIP_DT', 'vm.form.MC_BUYER_ID', 'vm.form.LK_ORD_TYPE_ID', 'vm.form.MC_ORDER_H_ID'], function (newVal, oldVal) {

            if (newVal[1] && newVal[0]) {
                var a = moment(newVal[1]);
                var b = moment(newVal[0]);
                vm.form.LEAD_TIME = a.diff(b, 'days');
            }

            if (!newVal[4]) {
                console.log(newVal);

                if (newVal[0] && newVal[1] && newVal[2] && newVal[3]) {
                    return MrcDataService.getDataByUrl('/StyleH/TnAList/OrderType/' + newVal[3] + '/Buyer/' + newVal[2] + '/LeadTime/' + vm.form.LEAD_TIME).then(function (res) {
                        var dataSource = new kendo.data.DataSource({
                            data: res
                        });

                        if (res.length == 1) {
                            vm.form['MC_TNA_TMPLT_H_ID'] = res[0].MC_TNA_TMPLT_H_ID;
                            $("#MC_TNA_TMPLT_H_ID").data("kendoDropDownList").setDataSource(dataSource);
                            $("#MC_TNA_TMPLT_H_ID").data("kendoDropDownList").select(1);

                        } else {

                            $("#MC_TNA_TMPLT_H_ID").data("kendoDropDownList").setDataSource(dataSource);
                        }

                    }, function (err) {
                        console.log(err);
                    })
                }
            }

        });


        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        

        Date.prototype.getWeek = function () {
            var onejan = new Date(this.getFullYear(), 0, 1);            
            var weekNum = Math.ceil((((this - onejan) / 86400000) + onejan.getDay()+1) / 7);
            
            var startDay = 6; //0=sunday, 1=monday etc.
            var d = vm.form.SHIP_DT.getDay(); //get the current day
            var weekStart = new Date(vm.form.SHIP_DT.valueOf() - (d <= 0 ? 7 - startDay : d - startDay) * 86400000); //rewind to start day
            var weekEnd = new Date(weekStart.valueOf() + 6 * 86400000); //add 6 days to get last day
                       

            var date = new Date(vm.form.SHIP_DT.getTime());
            date.setHours(0, 0, 0, 0);
            // Thursday in current week decides the year.
            var monthStartDateNum;
            var weekday = new Array(7);
            weekday[0] = "Saturday";
            weekday[1] = "Sunday";
            weekday[2] = "Monday";
            weekday[3] = "Tuesday";
            weekday[4] = "Wednesday";
            weekday[5] = "Thursday";
            weekday[6] = "Friday";
            
            for (var i = 0; i < 7; i++) {
                var dt = new Date(vm.form.SHIP_DT.getFullYear(), vm.form.SHIP_DT.getMonth(), i + 1);

                if (weekday[0] == weekday[dt.getDay()]) {
                    monthStartDateNum = i + 1;
                    break;
                }
            }

            //alert(monthStartDateNum);

            date.setDate(date.getDate() + 3 - (date.getDay() + 1) % 7);
            // January 4 is always in week 1.
            var week1 = new Date(date.getFullYear(), date.getMonth(), monthStartDateNum);
            // Adjust to Thursday in week 1 and count number of weeks from date to week1.
            var monthWeek = 1 + Math.round(((date.getTime() - week1.getTime()) / 86400000 - 3 + (week1.getDay() + 1) % 7) / 7);

            vm.form.WEEK_NUM_DATE_RANGE = $filter('date')(weekStart, vm.dtFormat) + ' To ' + $filter('date')(weekEnd, vm.dtFormat) + ', ' + monthWeek;

            //return weekNum;
        };
        
        $scope.ORDER_DT_LNopen = function ($event) {
            if (vm.form.IS_ORD_FINALIZED != 'Y') {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.ORDER_DT_LNopened = true;
            }
        };
        $scope.DocsReceiveDT_LNopen = function ($event) {
            if (vm.form.IS_ORD_FINALIZED != 'Y') {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.DocsReceiveDT_LNopened = true;
            }
        };
        $scope.WorkReceiveDT_LNopen = function ($event) {
            if (vm.form.IS_ORD_FINALIZED != 'Y') {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.WorkReceiveDT_LNopened = true;
            }
        };
        $scope.ConfirmDate_LNopen = function ($event) {
            if (vm.form.IS_ORD_FINALIZED != 'Y') {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.ConfirmDate_LNopened = true;
            }
        }       
        vm.shipmentDateOpen = function ($event) {
            if (vm.form.IS_ORD_FINALIZED != 'Y') {
                $event.preventDefault();
                $event.stopPropagation();

                vm.shipmentDateOpened = true;
            }
        };
        $scope.RevisionDT_LNopen = function ($event) {
            if (vm.form.IS_ORD_FINALIZED != 'Y') {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.RevisionDT_LNopened = true;
            }
        }
        //vm.form.IS_MULTI_SHIP_DT = 'N';
        vm.shipDateOnBlur = function () {
            //if (vm.form.IS_MULTI_SHIP_DT=='N') {
            //    vm.shipDateList = [{SHIP_DT: angular.copy(vm.form.SHIP_DT), itemsColor: []}];
            //}
            if (vm.form.itmsOrdShipDT.length == 1) {
                vm.form.itmsOrdShipDT[0].SHIP_DT = angular.copy(vm.form.SHIP_DT);
            }
        }

               
        vm.openDropDown = function (dropDownId) {
            var dropdownlist = $(dropDownId).data("kendoDropDownList");
            dropdownlist.open();
        };

        function getBuyerAcList() {

            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataBound: function (e) {
                    if (formData[key]) {
                        var MC_BYR_ACC_ID = formData['MC_BYR_ACC_ID']; //this.dataItem(e.item).MC_BYR_ACC_ID;
                        vm.form['MC_BYR_ACC_ID'] = formData['MC_BYR_ACC_ID'];
                    }
                    else {
                        if ($stateParams.BAID && $stateParams.BAID > 0) {
                            var MC_BYR_ACC_ID = $stateParams.BAID;
                            vm.form['MC_BYR_ACC_ID'] = $stateParams.BAID;
                        }
                    }

                    if (MC_BYR_ACC_ID) {
                        if (vm.buyerListData.length <1) {                        
                            getBuyerListData(MC_BYR_ACC_ID);
                        }                        
                    }

                    //if (MC_BYR_ACC_ID) {
                        
                    //    if (vm.buyerListData.length > 0) {
                    //        vm.accWiseBuyerList = _.filter(vm.buyerListData, { 'MC_BYR_ACC_ID': parseInt(MC_BYR_ACC_ID) });
                    //    }
                    //    else {
                    //        getBuyerListData(MC_BYR_ACC_ID);
                    //    }

                    //    getStyleExtList(MC_BYR_ACC_ID, null);
                    //}
                    //else {
                    //    vm.accWiseBuyerList = [];
                    //    vm.buyerWiseStyleList = [];
                    //}
                    
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    return MrcDataService.GetAllOthers('/api/mrc/buyer/getBuyerDropdownList?MC_BYR_ACC_ID=' + item.MC_BYR_ACC_ID).then(function (res) {
                        vm.form.accWiseBuyerList = res;
                    });
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

        vm.buyerListData = [];
        function getBuyerListData(pMC_BYR_ACC_ID) {
            MrcDataService.GetAllOthers('/api/mrc/buyer/BuyerByUserList').then(function (res) {
                //vm.buyerListData = res;
                var uniques = _.map(_.groupBy(res, function (doc) {
                    return doc.MC_BUYER_ID;
                }), function (grouped) {
                    return grouped[0];
                });

                vm.buyerListData = res;
                if (!formData[key]) {
                    vm.form.accWiseBuyerList = _.filter(res, { 'MC_BYR_ACC_ID': parseInt(pMC_BYR_ACC_ID) });
                }

                if (formData[key]) {                   
                    vm.form['MC_BUYER_ID'] = formData['MC_BUYER_ID'];
                    getStyleExtList(parseInt(pMC_BYR_ACC_ID), formData['MC_BUYER_ID']);
                }

                //if ($stateParams.pOrderObj != null) {
                //    vm.accWiseBuyerList = _.filter(vm.buyerListData, function (ob) {
                //        return ob.MC_BYR_ACC_ID == $stateParams.pOrderObj.MC_BYR_ACC_ID; //parseInt(MC_BYR_ACC_ID)
                //    });

                //    getStyleExtList(null, $stateParams.pOrderObj.MC_BUYER_ID);
                //}

            }, function (err) {
                console.log(err);
            });            
        };
       
        //vm.buyerWiseStyleList = new kendo.data.ObservableArray([]);
        function getStyleExtList(pMC_BYR_ACC_ID, pMC_BUYER_ID) {
            if (pMC_BYR_ACC_ID) {
                var selectedStyle = {};

                vm.buyerWiseStyleDataSource = new kendo.data.DataSource({
                    serverFiltering: true,
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            var url = '/StyleHExt/BuyerWiseStyleHExtList/' + pMC_BYR_ACC_ID + '/' + pMC_BUYER_ID + '?';
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

                            console.log(formData);
                            return MrcDataService.getDataByUrl(url).then(function (res) {
                                e.success(_.filter(res, function (o) {
                                    return o.HAS_EXT == 'N';
                                }));
                                //vm.StyleDataSource = _.uniq(_.map(res.data, 'STYLE_NO'));
                                if (formData[key]) {                                    
                                    //console.log(res);
                                    //selectedStyle = _.filter(res, { 'MC_STYLE_H_EXT_ID': parseInt(formData['MC_STYLE_H_EXT_ID']) });
                                    vm.form['MC_STYLE_H_EXT_ID'] = vm.resolveOrdData.MC_STYLE_H_EXT_ID;// formData['MC_STYLE_H_EXT_ID'];                                    
                                }
                                else {
                                    if ($stateParams.SEXID && $stateParams.SEXID > 0) {
                                        //selectedStyle = _.filter(res, { 'MC_STYLE_H_EXT_ID': parseInt($stateParams.SEXID) });
                                        vm.form['MC_STYLE_H_EXT_ID'] = $stateParams.SEXID;
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
                vm.buyerWiseStyleDataSource = new kendo.data.DataSource({
                    data: []
                });
            }


            //MrcDataService.GetAllOthers('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + pMC_BYR_ACC_ID + '/' + pMC_BUYER_ID).then(function (res) {
            //    vm.styleList = res;
            //    vm.form.buyerWiseStyleList = res;                

            //}, function (err) {                
            //    console.log(err);
            //});
        };
        
        vm.buyerAcOnSelect = function (e) {            
            var item = e.sender.dataItem(e.item); //e.sender.dataItem(e.sender.select());

            MrcDataService.GetAllOthers('/api/mrc/buyer/getBuyerDropdownList?MC_BYR_ACC_ID=' + vm.form.MC_BYR_ACC_ID).then(function (res) {
                vm.form.accWiseBuyerList = res;
            });

            //if (item.MC_BYR_ACC_ID > 0) {
            //    if (vm.buyerListData.length > 0) {
            //        vm.form.accWiseBuyerList = _.filter(vm.buyerListData, { 'MC_BYR_ACC_ID': parseInt(item.MC_BYR_ACC_ID) });
            //    }
            //    else {
            //        getBuyerListData();                    
            //    }

            //    getStyleExtList(item.MC_BYR_ACC_ID, null);
            //}
            //else {
            //    vm.form.accWiseBuyerList = [];
            //    vm.form.buyerWiseStyleList = [];
            //}
        };

        vm.buyerOnSelect = function (e) {
            var item = e.sender.dataItem(e.item); //e.sender.dataItem(e.sender.select());
            
            if (item.MC_BUYER_ID > 0) {
                getStyleExtList(vm.form.MC_BYR_ACC_ID, vm.form.MC_BUYER_ID);
                //vm.form.buyerWiseStyleList = _.filter(vm.styleList, { 'MC_BUYER_ID': parseInt(item.MC_BUYER_ID) });
            }
            
        };

        vm.styleOnSelect = function (e) {
            var dataItem = e.sender.dataItem(e.item); //e.sender.dataItem(e.sender.select());
            console.log(dataItem);

            if (dataItem) {
                var vID = parseInt(dataItem.MC_STYLE_H_ID);

                vm.form.MC_STYLE_H_ID = dataItem.MC_STYLE_H_ID;
                vm.form.STYLE_EXT_NO = dataItem.STYL_EXT_NO;
                vm.form.WORK_STYLE_NO = dataItem.STYLE_NO;
                vm.form.STYLE_NO = dataItem.STYLE_NO;
                vm.form.HR_COMPANY_ID = dataItem.MANUF_ID;
                vm.form.HAS_COMBO = dataItem.HAS_COMBO;
                vm.form.HAS_SET = dataItem.HAS_SET;
                vm.form.HAS_MULTI_COL_PACK = dataItem.HAS_MULTI_COL_PACK;
                vm.form.PCS_PER_PACK = dataItem.PCS_PER_PACK;

                
                if (dataItem.IS_N_R == 'N') {
                    vm.form.LK_ORD_TYPE_ID = 175;
                }
                else if (dataItem.IS_N_R == 'R') {                    
                    vm.form.LK_ORD_TYPE_ID = 174;
                }
                else {
                    vm.form.LK_ORD_TYPE_ID = 311;
                }
                
            }
            //vm.buyerWiseStyleList = _.filter(vm.styleList, { 'MC_BUYER_ID': parseInt(item.MC_BUYER_ID) });

        };
                       
        function getItemListData() {
            MrcDataService.GetAllOthers('/api/mrc/StyleDItem/StyleDtlItemList/' + null + '/' + 'A').then(function (res) {
                vm.itemListData = _.filter(res, function (o) {
                    return o.LK_ITEM_STATUS_ID == 258;
                });// new kendo.data.ObservableArray(res);
                
                //vm.styleItems = new kendo.data.ObservableArray(res);
                if ($stateParams.pOrderObj!=null) {
                    var items = _.filter(vm.itemListData, { 'MC_STYLE_H_ID': parseInt($stateParams.pOrderObj.MC_STYLE_H_ID), 'PARENT_ID': null });
                    vm.styleItems = new kendo.data.ObservableArray(items);
                }
                else {
                    vm.styleItems = new kendo.data.ObservableArray(_.filter(res, function (o) {
                        return o.LK_ITEM_STATUS_ID == 258;
                    }));
                }
                
            }, function (err) {
                console.log(err);
            });                  
        };
        
        function getCountryList() {
            return vm.countryList = {
                optionLabel: "-- Select Country --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/common/GetCountryList').then(function (res) {
                                $scope.cntryList = res;
                                var dataList = [];

                                console.log(formData['HR_COUNTRY_ID_LST']);
                                //console.log(formData['HR_COUNTRY_ID_LST'].split(','));
                                //console.log('=========');

                                angular.forEach($scope.cntryList, function (val, key) {
                                    angular.forEach(formData['HR_COUNTRY_ID_LST'], function (val1, key1) {
                                        if (val['HR_COUNTRY_ID'] == val1) {
                                            dataList.push(val);
                                        }
                                    });
                                });

                                $scope.cntryDataSource = new kendo.data.DataSource({
                                    data: dataList
                                });

                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });                                                     
                        }
                    }
                },
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };
        };

        function getDestPointList() {
            return vm.destPointList = {
                optionLabel: "-- Select Destination --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/DestnationPoint/SelectAll').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },                
                dataTextField: "DEST_POINT_NAME_EN",
                dataValueField: "MC_DEST_POINT_ID"
            };
        }

        function getOrderTypeList() {
            return MrcDataService.LookupListData(40).then(function (res) {
                vm.OrderTypeList = res;
                //console.log(res);
                //vm.form.LK_ORD_TYPE_ID = res[0].LOOKUP_DATA_ID;
            }, function (err) {
                console.log(err);
            });
        }      
        
        vm.onCloseCountry = function (e) {
            var item = e.sender.dataItem(e.item);            
            console.log(item);

            var dataList = [];
            
            angular.forEach($scope.cntryList, function (val, key) {
                angular.forEach($scope.form.HR_COUNTRY_ID_LST, function (val1, key1) {
                    if (val['HR_COUNTRY_ID'] == val1) {
                        dataList.push(val);
                    }
                });
            });

            $scope.cntryDataSource = new kendo.data.DataSource({
                data: dataList
            });
        }

        function OrderStatusList() {
            return MrcDataService.LookupListData(56).then(function (res) {
                vm.orderStatusList = res;                
            });

            //return vm.orderStatusList = {                
            //    filter: "startswith",
            //    autoBind: true,
            //    dataBound: function (e) {
            //        if (formData[key]) {                       
            //            vm.form['LK_ORD_STATUS_ID'] = formData['LK_ORD_STATUS_ID'];
            //        }
            //    },
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                MrcDataService.LookupListData(56).then(function (res) {
            //                    e.success(res);                                
            //                }, function (err) {
            //                    console.log(err);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "LK_DATA_NAME_EN",
            //    dataValueField: "LOOKUP_DATA_ID"
            //};
        }
                
        function getSizeList() {

            //return vm.sizeList = {
            //    //optionLabel: "-- Select --",
            //    filter: "startswith",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                MrcDataService.selectAllData('SizeMaster').then(function (res) {
            //                    e.success(res);
            //                }, function (err) {
            //                    console.log(err);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "SIZE_CODE",
            //    dataValueField: "MC_SIZE_ID"
            //};
            return MrcDataService.selectAllData('SizeMaster').then(function (res) {
                vm.sizeList = res;
                vm.StyleWiseSizeList = _.sortBy(res, 'MC_SIZE_GRP_ID', 'DISPLAY_ORDER', function (ob) {
                    return ob;
                });

            }, function (err) {
                console.log(err);
            });
        }
                
        function getRF_MouList() {
            return MrcDataService.GetAllOthers('/api/common/MOUList/Y').then(function (res) {
                vm.RF_MouList = res;
                if (formData[key]) {
                    vm.form.QTY_MOU_ID = formData['QTY_MOU_ID'];
                }
                $scope.RF_MouList = res;
                //vm.RF_MouList = {
                //    //optionLabel: "-- Select --",
                //    filter: "startswith",
                //    autoBind: true,
                //    dataSource: res,
                //    dataTextField: "MOU_CODE",
                //    dataValueField: "RF_MOU_ID"
                //};
            }, function (err) {
                console.log(err);
            });            
        };

        function getRF_CurrencyList() {
            return MrcDataService.GetAllOthers('/api/common/CurrencyList').then(function (res) {
                vm.currencyList = res;
                if (formData[key]) {
                    vm.form.RF_CURRENCY_ID = formData['RF_CURRENCY_ID'];
                }
            }, function (err) {
                console.log(err);
            });

            //return vm.currencyList = {
                
            //    filter: "startswith",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                MrcDataService.GetAllOthers('/api/common/CurrencyList').then(function (res) {
            //                    e.success(res);
            //                }, function (err) {
            //                    console.log(err);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "CURR_CODE",
            //    dataValueField: "RF_CURRENCY_ID",
            //    select: function (e) {
            //        var dataItem = this.dataItem(e.item);
                    
            //    }
            //};
        };

        function getCompanyList() {
            return vm.companyList = {
                //optionLabel: "-- Select Supplier --",
                filter: "startswith",
                autoBind: true,
                //index:1,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/common/CompanyList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"//,
                //select: function (dataItem) {
                //    var dataItem = this.dataItem(e.item);
                //    var vCompID = dataItem.HR_COMPANY_ID;                                       
                //}
            };
        };

        function officeList() {
            return vm.officeList = {
                //optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'get',
                                url: "/Hr/HrEmployee/OfficeListData"  //+ "&pType=" + showType
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });

                            

                            //MrcDataService.selectAllData('/Hr/HrEmployee/OfficeListData').then(function (res) {
                            //    e.success(res);
                            //}, function (err) {
                            //    console.log(err);
                            //});
                        }
                    }
                },
                dataTextField: "OFFICE_NAME_EN",
                dataValueField: "HR_OFFICE_ID"
            };
        };
        //////// Start New Code 
        

        vm.sizeWiseSumUPr = function (itmSizeQty) {
            var colorSumUpr = 0;

            angular.forEach(vm.allChildItemsColor, function (val, key) {
                angular.forEach(val['itemsSize'], function (val1, key1) {
                    if (val1['MC_SIZE_ID'] == itmSizeQty.MC_SIZE_ID) {
                        colorSumUpr = colorSumUpr + parseFloat(val1['UNIT_PRICE']);
                    }
                });
            });

            return colorSumUpr;
        };

        

        vm.leadTimeAuto = function () {
            var a = moment(vm.form.SHIP_DT);
            var b = moment(vm.form.ORD_CONF_DT);
            //console.log(b);

            vm.form.LEAD_TIME = a.diff(b, 'days');

            //MrcDataService.selectAllData('ColourMaster').then(function (res) {
            //    vm.form.MC_TNA_TMPLT_H_ID = res.MC_TNA_TMPLT_H_ID;
            //    vm.form.TNA_TMPLT_CODE = res.TNA_TMPLT_CODE;

            //}, function (err) {
            //    console.log(err);
            //});

        };

        
        //////// End New Code


        

        vm.CopyOrder = function () {
            //var copiedData = angular.copy(vm.obGrid);
            //vm.form = vm.copyOrder;

            vm.form.MC_ORDER_H_ID = 0;
            
            var i = 0;
            angular.forEach(vm.obGrid, function (val, key) {
                vm.obGrid[i].MC_ORDER_STYL_ID = 0;

                var x = 0;
                angular.forEach(vm.obGrid[i].items, function (val, key) {
                    vm.obGrid[i].items[x].MC_ORDER_SIZE_ID = 0;
                    x = x + 1;
                });

                i = i + 1;
            });

            
        };
        
        vm.addStyleRow = function (data, copyTo) {
            var copiedData = angular.copy(data);
            copiedData.MC_ORDER_STYL_ID = 0;
            angular.forEach(copiedData.items, function (val, key) {
                val['MC_ORDER_SIZE_ID'] = 0; 
                val['MC_ORDER_STYL_ID'] = 0;
            });

            copyTo.push(copiedData);
        };
        
        vm.addSizeRow = function (data, copyTo) {
            var copiedData = angular.copy(data);
            copiedData.MC_ORDER_SIZE_ID = 0;
            copyTo.push(copiedData);
        };                

        vm.removeSizeRow = function (index, removeFrom, master) {
            removeFrom.splice(index, 1);
        };

        vm.removeStyleRow = function (index, removeFrom, master) {
            removeFrom.splice(index, 1);
        };

        vm.subTotalSizeWise = function (data) {            
            var v_subtotal_qty = 0;
            var v_subtotal_price = 0;
            angular.forEach(data.items, function (val, key) {
                v_subtotal_qty = v_subtotal_qty + val['SIZE_QTY'];
                v_subtotal_price = v_subtotal_price + val['TOTAL_SIZE_PRICE'];
            });
            
            vm.obGrid[data.STYLE_INDEX-1]['SUB_TOTAL_QTY'] = v_subtotal_qty;
            vm.obGrid[data.STYLE_INDEX-1]['SUB_TOTAL_PRICE'] = v_subtotal_price;

            vm.grandTotal();
        };

        vm.grandTotal = function () {
            var v_subtotal_qty = 0;
            var v_subtotal_price = 0;

            angular.forEach(vm.obGrid, function (val, key) {
                v_subtotal_qty = v_subtotal_qty + val['SUB_TOTAL_QTY'];
                v_subtotal_price = v_subtotal_price + val['SUB_TOTAL_PRICE'];
            });

            vm.form.TOT_ORD_QTY = v_subtotal_qty;
            vm.form.TOT_ORD_VALUE = v_subtotal_price;
        };

        


        //added by Aminul for Tna Generation
        vm.generateTna = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'GenerateTnaTemplate.html',
                controller: 'GenerateTnaModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    Order: function () {
                        return data;
                    },
                    TnATaskList: function (MrcDataService) {
                        return MrcDataService.getDataByUrl('/StyleH/TnAList/TnATemp/' + data.MC_TNA_TMPLT_H_ID + '/Order/' + data.MC_ORDER_H_ID + '/Buyer/' + data.MC_BUYER_ID);
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };



    }

})();





//////============= OrderDtl Controller ====================
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('OrderDtlController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$timeout', '$state', '$stateParams', '$modal', 'formItemsStyleData', 'MrcDataService', 'Dialog', OrderDtlController]);
    function OrderDtlController(logger, config, $q, $scope, $http, exception, $filter, $timeout, $state, $stateParams, $modal, formItemsStyleData, MrcDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.MC_COLOR_ID_LST = [];
        vm.StyleWiseSizeList = [];
        vm.itemListData = [];
        vm.ItemTemplate = '<h4 style="padding-bottom:0px;margin-bottom:0px;"><b>{{dataItem.ITEM_NAME_EN+" "+dataItem.MODEL_NO}}</b></h4> <div ><p ng-repeat="itm in dataItem.CHILD_ITEMS" style="padding-left:20px;padding-top:0px;padding-bottom:0px;margin-bottom:0px;">{{ itm.ITEM_NAME_EN }}</p></div>';
        vm.ItemValueTemplate = '{{dataItem.ITEM_NAME_EN+" "+dataItem.MODEL_NO}}';

        vm.file = {};

        vm.pcsMouID = 1;
        vm.setMouID = 2;
        
        //itemsStyle
        console.log(formItemsStyleData);
        
        vm.formItem = { QTY_MOU_ID: '', ORDER_QTY: null, UNIT_PRICE: null, TOTAL_PRICE: null };
        vm.selectedItem = null;
                

        activate();
        function activate() {
            var promise = [/*getSizeList()*/];
            return $q.all(promise).then(function () {
                
                //if (formItemsStyleData.length > 0) {
                //    angular.forEach(formItemsStyleData, function (val, key) {
                //        $scope.$parent.form.itemsStyle.push(val);
                //    });
                //}

                
                vm.showSplash = false;
            });
        }
        
        vm.sizeShipmentDateOpened = [];
        vm.sizeShipmentDateOpen = function ($event, index) {
            if ($scope.$parent.form.IS_ORD_FINALIZED != 'Y') {
                $event.preventDefault();
                $event.stopPropagation();

                vm.sizeShipmentDateOpened[index] = true;
            }
        };

        function getSizeList() {

            //return vm.sizeList = {
            //    //optionLabel: "-- Select --",
            //    filter: "startswith",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                MrcDataService.selectAllData('SizeMaster').then(function (res) {
            //                    e.success(res);
            //                }, function (err) {
            //                    console.log(err);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "SIZE_CODE",
            //    dataValueField: "MC_SIZE_ID"
            //};
            return MrcDataService.selectAllData('SizeMaster').then(function (res) {
                vm.sizeList = res;
                vm.StyleWiseSizeList = _.sortBy(res, 'MC_SIZE_GRP_ID', 'DISPLAY_ORDER', function (ob) {
                    return ob;
                });

            }, function (err) {
                console.log(err);
            });
        }

        $scope.$watch('$parent.form.MC_STYLE_H_EXT_ID', function (newVal, oldVal) {
            
            console.log($scope.$parent.form);
            console.log(newVal);           
            
            if (!_.isEqual(newVal, oldVal)) {

                if (newVal != '' && angular.isDefined(newVal)) {
                    if ($scope.$parent.form.HAS_SET == 'Y') {
                        //$scope.$parent.form.QTY_MOU_ID = vm.setMouID;
                        vm.formItem.QTY_MOU_ID = vm.setMouID;
                    }
                    else if ($scope.$parent.form.PCS_PER_PACK > 0) {                        
                        //$scope.$parent.form.QTY_MOU_ID = 12;//vm.pcsMouID;
                        vm.formItem.QTY_MOU_ID = vm.pcsMouID;
                    }
                    else if ($scope.$parent.form.HAS_SET == 'N') {
                        //$scope.$parent.form.QTY_MOU_ID = vm.pcsMouID;
                        vm.formItem.QTY_MOU_ID = vm.pcsMouID;
                    }


                    MrcDataService.GetAllOthers('/api/mrc/StyleDItem/StyleDtlItemList/' + $scope.$parent.form.MC_STYLE_H_ID + '/' + 'A').then(function (res) {
                        vm.itemListData = _.filter(res, function (o) {
                            o['CHILD_ITEMS'] = _.filter(o.CHILD_ITEMS, function (o1) {
                                return (o1.LK_ITEM_STATUS_ID == 258);
                            });

                            return (o.LK_ITEM_STATUS_ID == 258);
                        });// new kendo.data.ObservableArray(res);

                        vm.styleItems = new kendo.data.ObservableArray(_.filter(res, function (o) {
                            return o.LK_ITEM_STATUS_ID == 258;
                        }));
                        //if ($stateParams.pOrderObj != null) {
                        //    var items = _.filter(vm.itemListData, { 'MC_STYLE_H_ID': parseInt($stateParams.pOrderObj.MC_STYLE_H_ID), 'PARENT_ID': null });
                        //    vm.styleItems = new kendo.data.ObservableArray(items);
                        //}
                        //else {
                        //    vm.styleItems = new kendo.data.ObservableArray(res);
                        //}

                    }, function (err) {
                        console.log(err);
                    });

                    //vm.styleItems = _.filter(vm.itemListData, { 'MC_STYLE_H_ID': parseInt(dataItem.MC_STYLE_H_ID), 'PARENT_ID': null });
                    if ($scope.$parent.form.MC_STYLE_H_ID > 0) {
                        getColorList($scope.$parent.form.MC_STYLE_H_ID);
                        getStyleWiseSizeList($scope.$parent.form.MC_STYLE_H_ID);
                    }
                }
            }            

        }, true);

        function getColorList(pMC_STYLE_H_ID) {
            //alert(pMC_STYLE_H_ID);
            return MrcDataService.GetAllOthers('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + parseInt(pMC_STYLE_H_ID) + '?pLK_COL_TYPE_LIST').then(function (res) {
                //vm.buyerColorList = res;

                vm.buyerColorList = _.map(_.groupBy(res, function (doc) {
                    return doc.MC_COLOR_ID;
                }), function (grouped) {
                    return grouped[0];
                });

            }, function (err) {
                console.log(err);
            });
        };

        vm.IS_STYLE_ALL_SIZE = 'N';
        function getStyleWiseSizeList(pMC_STYLE_H_ID) {

            vm.StyleWiseSizeList = [];

            if (pMC_STYLE_H_ID != null) {
                return MrcDataService.GetAllOthers('/api/mrc/Order/StyleWiseSizeList/' + pMC_STYLE_H_ID).then(function (res) {
                    if (res.length > 0) {
                        vm.IS_STYLE_ALL_SIZE = 'Y';
                        vm.StyleWiseSizeList = _.sortBy(res, 'DISPLAY_ORDER', function (ob) {
                            return ob;
                        });
                    }
                    //else {
                    //    vm.IS_STYLE_ALL_SIZE = 'N';
                    //    vm.StyleWiseSizeList = _.sortBy(vm.sizeList, 'MC_SIZE_GRP_ID', 'DISPLAY_ORDER', function (ob) {
                    //        return ob;
                    //    });
                    //}
                }, function (err) {
                    console.log(err);
                });
            }
        }

        vm.onSelectStyleItem = function (e, itmOrdShipDT) {
            var item = e.sender.dataItem(e.item);
            console.log('************');
            console.log($scope.$parent.form);
            console.log(itmOrdShipDT);

            if ($scope.$parent.form.HAS_SET == 'Y') {
                itmOrdShipDT.formItem.ORDER_QTY = $scope.$parent.form.TOT_ORD_QTY;
                itmOrdShipDT.formItem.UNIT_PRICE = $scope.$parent.form.UNIT_PRICE;
                itmOrdShipDT.formItem.QTY_MOU_ID = $scope.$parent.form.QTY_MOU_ID;
            }
            else if ($scope.$parent.form.PCS_PER_PACK > 0) {
                //$scope.$parent.form.QTY_MOU_ID = 12;//vm.pcsMouID;
                itmOrdShipDT.formItem.ORDER_QTY = $scope.$parent.form.TOT_ORD_QTY * $scope.$parent.form.PCS_PER_PACK;
                itmOrdShipDT.formItem.UNIT_PRICE = $scope.$parent.form.UNIT_PRICE / $scope.$parent.form.PCS_PER_PACK;
                itmOrdShipDT.formItem.QTY_MOU_ID = 1;//$scope.$parent.form.QTY_MOU_ID;
            }
            else if ($scope.$parent.form.HAS_SET == 'N') {
                itmOrdShipDT.formItem.ORDER_QTY = $scope.$parent.form.TOT_ORD_QTY;
                itmOrdShipDT.formItem.UNIT_PRICE = $scope.$parent.form.UNIT_PRICE;
                itmOrdShipDT.formItem.QTY_MOU_ID = $scope.$parent.form.QTY_MOU_ID;
            }
        };

        vm.styleItemAdd = function (itmOrdShipDT) {
            if ($scope.$parent.form.SHIP_DT == "" || $scope.$parent.form.SHIP_DT == null) {
                alert('Please select shipment date');
                return;
            }
            else if (itmOrdShipDT.formItem.MC_STYLE_D_ITEM_ID == "" || itmOrdShipDT.formItem.MC_STYLE_D_ITEM_ID == null) {
                alert('Please select item');
                return;
            }
            else if (itmOrdShipDT.formItem.ORDER_QTY == "" || itmOrdShipDT.formItem.ORDER_QTY == null) {
                alert('Please input item quantity');
                return;
            }
            else if (itmOrdShipDT.formItem.UNIT_PRICE == "" || itmOrdShipDT.formItem.UNIT_PRICE == null) {
                alert('Please input item unit price');
                return;
            }
            else if (itmOrdShipDT.formItem.QTY_MOU_ID == "" || itmOrdShipDT.formItem.QTY_MOU_ID == null) {
                alert('Please select item unit');
                return;
            }

            
            vm.setItems = [];
            if ($scope.$parent.form.HAS_SET == 'Y') {
                console.log(vm.itemListData);

                //vm.parentItem = vm.itemListData;
                vm.parentItem = _.filter(vm.itemListData, { 'MC_STYLE_H_ID': parseInt($scope.$parent.form.MC_STYLE_H_ID), 'MC_STYLE_D_ITEM_ID': parseInt(itmOrdShipDT.formItem.MC_STYLE_D_ITEM_ID) });
                //vm.childItems = _.filter(vm.itemListData, { 'MC_STYLE_H_ID': parseInt($scope.$parent.form.MC_STYLE_H_ID), 'PARENT_ID': parseInt(vm.formItem.MC_STYLE_D_ITEM_ID) });
                vm.childItems = [];

                angular.forEach(vm.parentItem, function (val, key) {
                    angular.forEach(val['CHILD_ITEMS'], function (val1, key1) {
                        //val1['MODEL_NO'] = val['MODEL_NO'];
                        vm.childItems.push(angular.copy(val1));                        
                        vm.parentItem.push(angular.copy(val1));
                    });
                });

                //angular.forEach(vm.childItems, function (val, key) {
                //    vm.parentItem.push(angular.copy(val));
                //});
               
                vm.setItems = _.sortBy(vm.parentItem, 'MC_STYLE_D_ITEM_ID'); //ITEM_CODE
                //vm.setItems = _.sortBy(vm.itemListData, 'MC_STYLE_D_ITEM_ID'); //ITEM_CODE
                               
                angular.forEach(vm.setItems, function (val, key) {
                    var vItemQty = 0;
                    var vItemUnitPrice = 0;
                    var vItemTotPrice = 0;
                    var vItemMOUId = 1;

                    if (val['PARENT_ID'] == null) {
                        
                        vItemQty = itmOrdShipDT.formItem.ORDER_QTY;
                        vItemUnitPrice = itmOrdShipDT.formItem.UNIT_PRICE;
                        vItemTotPrice = itmOrdShipDT.formItem.TOTAL_PRICE;
                        vItemMOUId = vm.setMouID;

                        var cboList = new kendo.data.ObservableArray(val['COMBO_NO'].split(','));
                        vm.comboList = [];
                        angular.forEach(cboList, function (val1, key1) {
                            if (val1 != "") {
                                vm.comboList.push({ COMBO_NO: val1 });
                            }
                        });

                        
                        itmOrdShipDT.itemsStyle.push({
                            ITEM_INDEX: 0, MC_STYLE_D_ITEM_ID: val['MC_STYLE_D_ITEM_ID'], ITEM_NAME_EN: val['ITEM_NAME_EN'] + ' ' + val['MODEL_NO'], PARENT_ITEM_ID: val['PARENT_ID'],
                            ORDER_QTY: vItemQty, QTY_MOU_ID: vItemMOUId, UNIT_PRICE: vItemUnitPrice, TOTAL_PRICE: vItemTotPrice, IS_ACTIVE: false,
                            COMBO_NO_LIST: vm.comboList, childItems: [], itemsColor: []
                        });
                    }
                    else {
                        vItemQty = itmOrdShipDT.formItem.ORDER_QTY;
                        vItemUnitPrice = (itmOrdShipDT.formItem.UNIT_PRICE / (vm.setItems.length - 1));
                        vItemTotPrice = (itmOrdShipDT.formItem.UNIT_PRICE / (vm.setItems.length - 1)) * vItemQty;
                        vItemMOUId = vm.pcsMouID;

                        var vItemStyleIndex = _.findIndex(itmOrdShipDT.itemsStyle, { 'MC_STYLE_D_ITEM_ID': val['PARENT_ID'] });
                        
                        itmOrdShipDT.itemsStyle[vItemStyleIndex].childItems.push({
                            ITEM_INDEX: 0, MC_STYLE_D_ITEM_ID: val['MC_STYLE_D_ITEM_ID'], ITEM_NAME_EN: val['ITEM_NAME_EN'] + ' ' + val['MODEL_NO'], PARENT_ITEM_ID: val['PARENT_ID'],
                            ORDER_QTY: vItemQty, QTY_MOU_ID: vItemMOUId, UNIT_PRICE: vItemUnitPrice, TOTAL_PRICE: vItemTotPrice, IS_ACTIVE: false,
                            COMBO_NO_LIST: vm.comboList, itemsColor: []
                        });
                        
                    }
                });
            }
            else {
                var vItemQty = itmOrdShipDT.formItem.ORDER_QTY;
                var vItemUnitPrice = itmOrdShipDT.formItem.UNIT_PRICE;
                var vItemTotPrice = itmOrdShipDT.formItem.TOTAL_PRICE;
                var vItemMOUId = itmOrdShipDT.formItem.QTY_MOU_ID;

                vm.parentItem = _.filter(vm.itemListData, { 'MC_STYLE_D_ITEM_ID': parseInt(itmOrdShipDT.formItem.MC_STYLE_D_ITEM_ID) });
                

                var cboList = new kendo.data.ObservableArray(vm.parentItem[0]['COMBO_NO'].split(','));
                vm.comboList = [];
                angular.forEach(cboList, function (val1, key1) {
                    if (val1 != "") {
                        vm.comboList.push({ COMBO_NO: val1 });
                    }
                });

                angular.forEach(vm.parentItem, function (val, key) {
                    itmOrdShipDT.itemsStyle.push({
                        ITEM_INDEX: 0, MC_STYLE_D_ITEM_ID: val['MC_STYLE_D_ITEM_ID'], ITEM_NAME_EN: val['ITEM_NAME_EN'] + ' ' + val['MODEL_NO'], PARENT_ITEM_ID: val['PARENT_ID'],
                        ORDER_QTY: vItemQty, QTY_MOU_ID: vItemMOUId, UNIT_PRICE: vItemUnitPrice, TOTAL_PRICE: vItemTotPrice, IS_ACTIVE: false,
                        COMBO_NO_LIST: vm.comboList, childItems: [], itemsColor: []
                    });
                });
            }


            vm.selectedItem = itmOrdShipDT.itemsStyle[0];

            itmOrdShipDT.formItem.MC_STYLE_D_ITEM_ID = '';
            itmOrdShipDT.formItem.ORDER_QTY = "";
            itmOrdShipDT.formItem.UNIT_PRICE = "";
            itmOrdShipDT.formItem.TOTAL_PRICE = 0;
            

            vm.IS_STYLE_ALL_COLOR = 'Y';
            vm.MC_COLOR_ID_LST = _.filter(vm.buyerColorList, function (ob) {
                return ob;
            });

            if (vm.IS_STYLE_ALL_SIZE == 'Y') {
                vm.MC_SIZE_ID_LST = _.filter(vm.StyleWiseSizeList, function (ob) {
                    return ob;
                });
            }

            vm.getTotalColSpan();
        };

        vm.getTotalColSpan = function () {
            if ($scope.$parent.form.HR_COUNTRY_ID_LST.length > 1 && $scope.$parent.form.HAS_MULTI_COL_PACK == 'Y' && $scope.$parent.form.HAS_COMBO == 'Y') {
                vm.TOTAL_COL_SPAN = 4;
            }
            else if ($scope.$parent.form.HR_COUNTRY_ID_LST.length == 1 && $scope.$parent.form.HAS_MULTI_COL_PACK == 'Y' && $scope.$parent.form.HAS_COMBO == 'Y') {
                vm.TOTAL_COL_SPAN = 3;
            }
            else if ($scope.$parent.form.HR_COUNTRY_ID_LST.length > 1 && $scope.$parent.form.HAS_MULTI_COL_PACK == 'Y') {
                vm.TOTAL_COL_SPAN = 3;
            }
            else if ($scope.$parent.form.HR_COUNTRY_ID_LST.length == 1 && $scope.$parent.form.HAS_MULTI_COL_PACK == 'Y') {
                vm.TOTAL_COL_SPAN = 2;
            }
            else if ($scope.$parent.form.HAS_MULTI_COL_PACK == 'Y') {
                vm.TOTAL_COL_SPAN = 2;
            }
            else if ($scope.$parent.form.HR_COUNTRY_ID_LST.length > 1 && $scope.$parent.form.HAS_COMBO == 'Y') {
                vm.TOTAL_COL_SPAN = 3;
            }
            else if ($scope.$parent.form.HR_COUNTRY_ID_LST.length == 1 && $scope.$parent.form.HAS_COMBO == 'Y') {
                vm.TOTAL_COL_SPAN = 2;
            }
            else if ($scope.$parent.form.HAS_COMBO == 'Y') {
                vm.TOTAL_COL_SPAN = 2;
            }
            else if ($scope.$parent.form.HR_COUNTRY_ID_LST.length>1) {
                vm.TOTAL_COL_SPAN = 2;
            }
            else {
                vm.TOTAL_COL_SPAN = 1;
            }
        };

        vm.addRow = function (data, copyTo, obName, index) {
            var copiedData = angular.copy(data);

            if (obName == 'itmsOrdShipDT') {
                copiedData.MC_ORDER_SHIP_ID = 0;

                angular.forEach(copiedData.itemsStyle, function (val, key) {
                    val['MC_ORDER_STYL_ID'] = 0;

                    angular.forEach(copiedData.itemsColor, function (val1, key1) {
                        val1['MC_ORDER_COL_ID'] = 0;

                        angular.forEach(val1['itemsSize'], function (val2, key2) {
                            val2['MC_ORDER_SIZE_ID'] = 0;
                        });
                    });
                });
            }
            else if (obName == 'itemsStyle') {
                copiedData.MC_ORDER_STYL_ID = 0;
                copiedData.MC_STYLE_D_ITEM_ID = "";
                copiedData.ORDER_QTY = 0;
                copiedData.UNIT_PRICE = 0;
                copiedData.TOTAL_PRICE = 0;

                angular.forEach(val['itemsColor'], function (val1, key1) {
                    val1['MC_ORDER_COL_ID'] = 0;

                    angular.forEach(val1['itemsSize'], function (val2, key2) {
                        val2['MC_ORDER_SIZE_ID'] = 0;
                    });
                });
            }            
            else if (obName == 'itemsColor') {
                copiedData['MC_ORDER_COL_ID'] = 0;

                angular.forEach(copiedData.itemsSize, function (val, key) {
                    val['MC_ORDER_SIZE_ID'] = 0;
                });
            }

            if (obName == 'itemsColor') {
                copyTo.splice(index + 1, 0, copiedData);
            }
            else {
                copyTo.push(copiedData);
            }
        };

        vm.removeRow = function (index, removeFrom, master) {
            removeFrom.splice(index, 1);
        };

        vm.selectRow = function (index, selectFrom, master, obName) {
            //console.log(selectFrom);
            //console.log(selectFrom[index]);
            //return;

            vm.sizeGrandTotal = 0;
            vm.allChildItemsColor = [];
            vm.parentItemShipDate = {};

            if (obName == 'itmStyleChild') {
                vm.selectedChildItem = selectFrom[index];
            }
            else {
                vm.selectedItem = selectFrom[index];
                vm.selectedChildItem = null;
            }

            if ($scope.$parent.form.HAS_SET == 'Y' && obName != 'itmStyleChild') {

                vm.parentItemShipDate = { itemsColor: [] };
                

                //angular.forEach(vm.selectedItem.childItems, function (val, key) {
                //    angular.forEach(val['itemsShipDate'], function (val1, key1) {
                //        var spDateIndex = _.findIndex(vm.parentItemShipDate, { 'SHIP_DT': val1['SHIP_DT'] });

                //        if (spDateIndex < 0) {
                //            vm.parentItemShipDate.push({ SHIP_DT: val1['SHIP_DT'], itemsColor: [] });
                //        }
                //    });
                //});

                angular.forEach(vm.selectedItem.childItems, function (val, key) {
                    
                    angular.forEach(val['itemsColor'], function (val1, key1) {
                        //console.log(spDateIndex);
                        vm.parentItemShipDate.itemsColor.push(val1);
                    });

                });


                //console.log(vm.parentItemShipDate);                
            }

            getItemWiseTotal(obName);

            vm.getTotalColSpan();

        };

        vm.updateDtlPrice = function (obj) {
            console.log(obj);
            angular.forEach(obj.itemsShipDate, function (val, key) {
                angular.forEach(val['itemsColor'], function (val1, key1) {
                    angular.forEach(val1['itemsSize'], function (val2, key2) {
                        val2['UNIT_PRICE'] = obj['UNIT_PRICE'];
                    });
                });
            });
        };

        function getItemWiseTotal(obName) {
            //alert(vm.selectedItem.ITEM_INDEX);
            if (vm.selectedItem.ITEM_INDEX == null) {
                vm.selectedItem.ITEM_INDEX = 0;
            }
            vm.sizeGrandTotal = 0;

            if (obName == 'itmStyleChild') {
                angular.forEach(vm.selectedChildItem['itemsColor'], function (val1, key1) {
                    angular.forEach(val1['itemsSize'], function (val2, key2) {
                        if (val2['SIZE_QTY'] != null && parseInt(val2['SIZE_QTY']) > 0) {
                            vm.sizeGrandTotal = parseInt(vm.sizeGrandTotal) + parseInt(val2['SIZE_QTY']);
                        }

                    });
                });
            }
            else if (obName == 'itmStyleParent') {
                angular.forEach(vm.parentItemShipDate['itemsColor'], function (val1, key1) {
                    angular.forEach(val1['itemsSize'], function (val2, key2) {
                        if (val2['SIZE_QTY'] != null && parseInt(val2['SIZE_QTY']) > 0) {
                            vm.sizeGrandTotal = parseInt(vm.sizeGrandTotal) + parseInt(val2['SIZE_QTY']);
                        }
                    });
                });

            }
            else {
                angular.forEach(vm.selectedItem['itemsColor'], function (val1, key1) {
                    angular.forEach(val1['itemsSize'], function (val2, key2) {
                        if (val2['SIZE_QTY'] != null && parseInt(val2['SIZE_QTY']) > 0) {
                            vm.sizeGrandTotal = parseInt(vm.sizeGrandTotal) + parseInt(val2['SIZE_QTY']);
                        }
                    });
                });                
            }

            return;
        };

        vm.getSeyleAllColor = function () {
            if (vm.IS_STYLE_ALL_COLOR == 'Y') {
                vm.MC_COLOR_ID_LST = _.filter(vm.buyerColorList, function (ob) {
                    return ob;
                });
            }
            else {
                vm.MC_COLOR_ID_LST = [];
            }
        };

        vm.changeStyleColorLst = function () {
            if (vm.MC_COLOR_ID_LST.length == vm.buyerColorList.length) {
                vm.IS_STYLE_ALL_COLOR = 'Y';
            }
            else {
                vm.IS_STYLE_ALL_COLOR = 'N';
            }
        };

        vm.genColorSize = function (formData, obName) {

            var obLength = 0;
            vm.itemsColorHdr = [];
            obLength = vm.MC_SIZE_ID_LST.length < 1 ? 0 : vm.MC_SIZE_ID_LST.length;
            vm.sizeLength = obLength;

            //console.log(vm.selectedItem.childItems);

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
                
                obItmColorLength = vm.selectedItem.childItems[0].itemsColor.length;
                obItmColorList = vm.selectedItem.childItems[0].itemsColor;
            }
            else {
               
                obItmColorLength = formData[0].itemsColor.length;
                obItmColorList = formData[0].itemsColor;

                vm.sizeLength = obLength + formData[0].itemsColor[0].itemsSize.length;
            }
            //console.log('ddd');
            //console.log(obItmColorList);

            var itmUnitPrice = vm.selectedItem.UNIT_PRICE;
            //if (obName == 'itmStyleChild') {
            //    itmUnitPrice = vm.selectedChildItem.UNIT_PRICE;
            //}

            if ($scope.$parent.form.HAS_SET == 'Y' && vm.selectedItem.childItems.length > 0) {
                if ($scope.$parent.form.PCS_PER_PACK > 1 && $scope.$parent.form.HAS_MULTI_COL_PACK == 'Y') {
                    obItmColorLength = obItmColorLength * $scope.$parent.form.PCS_PER_PACK;
                    //alert(obItmColorLength);
                }

                angular.forEach(vm.selectedItem.childItems, function (val, key) {

                    itmUnitPrice = val['UNIT_PRICE'];
                    if (isNewColor == 'Y') {

                        //for (var s = 0; s < val['itemsShipDate'].length; s++) {

                            var idx = 0;
                            for (var i = 0; i < obItmColorLength; i++) {

                                if ($scope.$parent.form.PCS_PER_PACK > 1 && $scope.$parent.form.HAS_MULTI_COL_PACK == 'Y') {
                                    var vItmColorIndex = _.findIndex(val['itemsColor'], { 'MC_COLOR_ID': obItmColorList[idx].MC_COLOR_ID });
                                }
                                else {
                                    var vItmColorIndex = _.findIndex(val['itemsColor'], { 'MC_COLOR_ID': obItmColorList[i].MC_COLOR_ID });
                                }

                                if (((i + 1) % $scope.$parent.form.PCS_PER_PACK) == 0) {
                                    idx = idx + 1;
                                }

                                if (vItmColorIndex < 0) {
                                    if ($scope.$parent.form.PCS_PER_PACK > 1 && $scope.$parent.form.HAS_MULTI_COL_PACK == 'Y') {
                                        for (var cp = 0; cp < $scope.$parent.form.PCS_PER_PACK; cp++) {
                                            val['itemsColor'].push({
                                                ITEM_INDEX: 0, COLOR_INDEX: 0, MC_COLOR_ID: obItmColorList[idx].MC_COLOR_ID, COLOR_NAME_EN: obItmColorList[idx].COLOR_NAME_EN, COMBO_NO: null,
                                                COMBO_NO_LIST: val['COMBO_NO_LIST'], MC_ORDER_SIZE_ID: 0, TOTAL_QTY: 0, itemsSize: []
                                            });
                                        }
                                    }
                                    else {
                                        console.log('test');
                                        console.log(val);

                                        val['itemsColor'].push({
                                            ITEM_INDEX: 0, COLOR_INDEX: 0, MC_COLOR_ID: obItmColorList[i].MC_COLOR_ID, COLOR_NAME_EN: obItmColorList[i].COLOR_NAME_EN, COMBO_NO: null,
                                            COMBO_NO_LIST: val['COMBO_NO_LIST'], MC_ORDER_SIZE_ID: 0, TOTAL_QTY: 0, itemsSize: []
                                        });
                                    }

                                }

                                for (var x = 0; x < obLength; x++) {
                                    var vIndex = _.findIndex(val['itemsColor'][i].itemsSize, { 'SIZE_CODE': vm.MC_SIZE_ID_LST[x].SIZE_CODE });

                                    if (vIndex < 0) {
                                        val['itemsColor'][i].itemsSize.push({ MC_ORDER_SIZE_ID: 0, MC_ORDER_STYL_ID: 0, SIZE_CODE: vm.MC_SIZE_ID_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_ID_LST[x].MC_SIZE_ID, SIZE_QTY: null, UNIT_PRICE: itmUnitPrice });
                                    }
                                };
                            };
                        //};
                    }
                    else {
                        //for (var s = 0; s < val['itemsShipDate'].length; s++) {

                        for (var i = 0; i < val['itemsColor'].length; i++) {

                                for (var x = 0; x < obLength; x++) {
                                    var vIndex = _.findIndex(val['itemsColor'][i].itemsSize, { 'SIZE_CODE': vm.MC_SIZE_ID_LST[x].SIZE_CODE });

                                    if (vIndex < 0) {
                                        val['itemsColor'][i].itemsSize.push({ MC_ORDER_SIZE_ID: 0, MC_ORDER_STYL_ID: 0, SIZE_CODE: vm.MC_SIZE_ID_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_ID_LST[x].MC_SIZE_ID, SIZE_QTY: null, UNIT_PRICE: itmUnitPrice });
                                        vm.sizeLength = val['itemsColor'][i].itemsSize.length;
                                    }
                                };
                            };
                        //};

                        //for (var i = 0; i < val['itemsColor'].length; i++) {

                        //    for (var x = 0; x < obLength; x++) {
                        //        var vIndex = _.findIndex(val['itemsColor'][i].itemsSize, { 'SIZE_CODE': vm.MC_SIZE_ID_LST[x].SIZE_CODE });
                        //        if (vIndex < 0) {
                        //            val['itemsColor'][i].itemsSize.push({ MC_ORDER_SIZE_ID: 0, MC_ORDER_STYL_ID: 0, SIZE_CODE: vm.MC_SIZE_ID_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_ID_LST[x].MC_SIZE_ID, SIZE_QTY: 0, UNIT_PRICE: itmUnitPrice });
                        //        }
                        //    };
                        //};
                    }
                });

                //console.log(vm.selectedItem.childItems);
                console.log(formData);

                vm.selectRow(vm.selectedItem.ITEM_INDEX, formData, true, 'itmStyleParent');
            }
            else {
                for (var s = 0; s < formData.length; s++) {
                    console.log(formData.length);
                    if ($scope.$parent.form.PCS_PER_PACK > 1 && $scope.$parent.form.HAS_MULTI_COL_PACK == 'Y') {
                        obItmColorLength = obItmColorLength * $scope.$parent.form.PCS_PER_PACK;
                        //alert(obItmColorLength);
                    }

                    var idx = 0;
                    for (var i = 0; i < obItmColorLength; i++) {

                        console.log(vm.selectedItem.COMBO_NO_LIST);

                        if ($scope.$parent.form.PCS_PER_PACK > 1 && $scope.$parent.form.HAS_MULTI_COL_PACK == 'Y') {
                            var vItmColorIndex = _.findIndex(formData[s].itemsColor, { 'MC_COLOR_ID': obItmColorList[idx].MC_COLOR_ID });
                        }
                        else {
                            var vItmColorIndex = _.findIndex(formData[s].itemsColor, { 'MC_COLOR_ID': obItmColorList[i].MC_COLOR_ID });
                        }

                        if (((i + 1) % $scope.$parent.form.PCS_PER_PACK) == 0) {
                            idx = idx + 1;
                        }


                        if (vItmColorIndex < 0) {
                            //console.log(vm.form.itemsStyle[vm.selectedItem.ITEM_INDEX].itemsShipDate[0]);
                            if ($scope.$parent.form.PCS_PER_PACK > 1 && $scope.$parent.form.HAS_MULTI_COL_PACK == 'Y') {
                                for (var cp = 0; cp < $scope.$parent.form.PCS_PER_PACK; cp++) {
                                    formData[s]['itemsColor'].push({
                                        ITEM_INDEX: 0, COLOR_INDEX: 0, MC_COLOR_ID: obItmColorList[idx].MC_COLOR_ID, COLOR_NAME_EN: obItmColorList[idx].COLOR_NAME_EN, COMBO_NO: null,
                                        COMBO_NO_LIST: vm.selectedItem.COMBO_NO_LIST, MC_ORDER_SIZE_ID: 0, TOTAL_QTY: 0, itemsSize: []
                                    });
                                }
                            }
                            else {
                                formData[s]['itemsColor'].push({
                                    ITEM_INDEX: 0, COLOR_INDEX: 0, MC_COLOR_ID: obItmColorList[i].MC_COLOR_ID, COLOR_NAME_EN: obItmColorList[i].COLOR_NAME_EN, COMBO_NO: null,
                                    COMBO_NO_LIST: vm.selectedItem.COMBO_NO_LIST, MC_ORDER_SIZE_ID: 0, TOTAL_QTY: 0, itemsSize: []
                                });
                            }
                        }

                        for (var x = 0; x < obLength; x++) {
                            var vIndex = _.findIndex(formData[s].itemsColor[i].itemsSize, { 'SIZE_CODE': vm.MC_SIZE_ID_LST[x].SIZE_CODE });

                            if (vIndex < 0) {
                                formData[s].itemsColor[i].itemsSize.push({ MC_ORDER_SIZE_ID: 0, MC_ORDER_STYL_ID: 0, SIZE_CODE: vm.MC_SIZE_ID_LST[x].SIZE_CODE, MC_SIZE_ID: vm.MC_SIZE_ID_LST[x].MC_SIZE_ID, SIZE_QTY: null, UNIT_PRICE: itmUnitPrice });
                            }
                        };
                    };
                };
            }

            vm.MC_COLOR_ID_LST = [];
        };

        vm.makeActive = function (items, $index) {
            angular.forEach(items, function (val, key) {
                if ($index != key) {
                    val['IS_ACTIVE'] = false;
                }
            })
            items[$index]['IS_ACTIVE'] = !items[$index]['IS_ACTIVE'];
        };

        vm.colorWiseTotal = function (itemColor, obName) {
            var totColorQty = 0;
            angular.forEach(itemColor.itemsSize, function (val, key) {
                if (val['SIZE_QTY'] != null && parseInt(val['SIZE_QTY']) > 0) {
                    totColorQty = totColorQty + parseInt(val['SIZE_QTY']);
                }
            });

            getItemWiseTotal(obName);

            return totColorQty;
        };
        
        vm.sizeWiseSumQty = function (itmSizeQty, obName) {
            var colorSumQty = 0;

            if (obName == 'itmStyleParent') {

                angular.forEach(vm.parentItemShipDate['itemsColor'], function (val1, key1) {
                    angular.forEach(val1['itemsSize'], function (val2, key2) {
                        if (val2['MC_SIZE_ID'] == itmSizeQty.MC_SIZE_ID) {
                            if (val2['SIZE_QTY'] != null && parseInt(val2['SIZE_QTY']) > 0) {
                                colorSumQty = colorSumQty + parseInt(val2['SIZE_QTY']);
                            }
                        }
                    });
                });
            }
            else if (obName == 'itmStyleChild') {

                angular.forEach(vm.selectedChildItem['itemsColor'], function (val1, key1) {
                    angular.forEach(val1['itemsSize'], function (val2, key2) {
                        if (val2['MC_SIZE_ID'] == itmSizeQty.MC_SIZE_ID) {
                            if (val2['SIZE_QTY'] != null && parseInt(val2['SIZE_QTY']) > 0) {
                                colorSumQty = colorSumQty + parseInt(val2['SIZE_QTY']);
                            }
                        }
                    });
                });

            }
            else if (obName == 'itmStyle') {
                //console.log(vm.selectedItem);
                angular.forEach(vm.selectedItem['itemsColor'], function (val1, key1) {
                    angular.forEach(val1['itemsSize'], function (val2, key2) {
                        if (val2['MC_SIZE_ID'] == itmSizeQty.MC_SIZE_ID) {
                            if (val2['SIZE_QTY'] != null && parseInt(val2['SIZE_QTY']) > 0) {
                                colorSumQty = colorSumQty + parseInt(val2['SIZE_QTY']);
                            }
                        }
                    });
                });             
            }

            return colorSumQty;
        };
        
        vm.submitData = function (data, token) {
            vm.errors = [];
            var vTotalPcsQty = 0;
            var vStyleItemQty = 0;
            var vSizeWiseTotQty = 0;

            var submitData = angular.copy(data);
            submitData['HR_COUNTRY_ID_LST'] = submitData['HR_COUNTRY_ID_LST'].join(',');

            //console.log(data.itemsStyle);

            angular.forEach(submitData.itmsOrdShipDT, function (val, key) {
                val['SHIP_DT'] = $filter('date')(val['SHIP_DT'], vm.dtFormat);

                angular.forEach(val['itemsStyle'], function (val1, key1) {
                    vStyleItemQty = parseInt(val1['ORDER_QTY']);
                    vSizeWiseTotQty = 0;

                    if (val1['childItems'].length > 0) {
                        //alert('s');

                        angular.forEach(val1['childItems'], function (val2, key2) {                                                     
                            
                            angular.forEach(val2['itemsColor'], function (val3, key3) {
                                angular.forEach(val3['itemsSize'], function (val4, key4) {
                                    
                                    if (val4['SIZE_QTY'] != null && parseInt(val4['SIZE_QTY']) > 0) {
                                        vSizeWiseTotQty = parseInt(vSizeWiseTotQty) + parseInt(val4['SIZE_QTY']);
                                        vTotalPcsQty = parseInt(vTotalPcsQty) + parseInt(val4['SIZE_QTY']);
                                    }
                                });
                            });
                            
                        });

                        vSizeWiseTotQty = (vSizeWiseTotQty / val1['childItems'].length);
                        //alert(vSizeWiseTotQty);
                    }
                    else {
                        angular.forEach(val1['itemsColor'], function (val2, key2) {
                            angular.forEach(val2['itemsSize'], function (val3, key3) {
                                
                                if (val3['SIZE_QTY'] != null && parseInt(val3['SIZE_QTY']) > 0) {
                                    vSizeWiseTotQty = parseInt(vSizeWiseTotQty) + parseInt(val3['SIZE_QTY']);
                                    vTotalPcsQty = parseInt(vTotalPcsQty) + parseInt(val3['SIZE_QTY']);
                                }
                            });
                        });
                    }

                    if (vStyleItemQty != vSizeWiseTotQty) {
                        vm.errors.push({ key: ['Item order quantity & size wise quantity are not equal of ' + val1['ITEM_NAME_EN'] + ' and shipment date ' + val['SHIP_DT']] });
                    }

                });
            });

            console.log(vTotalPcsQty + '-' + submitData['TOTAL_PCS_QTY']);

            if (vTotalPcsQty != submitData['TOTAL_PCS_QTY']) {
                vm.errors.push({ key: ['Quantity in pcs & size wise quantity are not equal'] });
            }

            if (vm.errors != undefined && vm.errors.length > 0) {
                //console.log(vStyleItemQty + '-' + vSizeWiseTotQty);
                //console.log(data);
                return;
            }
            else {
                vm.errors = undefined;
            }
            //vm.chkStyleAndSizeWiseQty();

            //console.log(data);
            submitData.ORDER_DT = $filter('date')(submitData.ORDER_DT, vm.dtFormat);
            submitData.ORD_CONF_DT = $filter('date')(submitData.ORD_CONF_DT, vm.dtFormat);
            submitData.ORD_DOC_RCV_DT = $filter('date')(submitData.ORD_DOC_RCV_DT, vm.dtFormat);
            submitData.ART_WRK_RCV_DT = $filter('date')(submitData.ART_WRK_RCV_DT, vm.dtFormat);
            submitData.SHIP_DT = $filter('date')(submitData.SHIP_DT, vm.dtFormat);

            console.log(submitData);

            submitData.accWiseBuyerList = [];
            submitData.buyerWiseStyleList = [];
            submitData.items = [];
            submitData.items = vm.obGrid;
            vm.copyOrder = submitData;

            return MrcDataService.batchSaveOrder(submitData, token).then(function (res) {
                //vm.showSplash = true;
                vm.errors = undefined;
                if (res.success === false) {
                    vm.errors = [];
                    vm.errors = res.errors;
                    //vm.showSplash = false;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    console.log(res['data']);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.isSaved = true;
                    };

                    if (res.data.PMC_ORDER_H_ID_RTN != 0) {
                        MrcDataService.orderDataList(null, res.data.PMC_ORDER_H_ID_RTN, null).then(function (result) {
                            $scope.$parent.form.MC_ORDER_H_ID = res.data.PMC_ORDER_H_ID_RTN;
                            $scope.$parent.form.JOB_TRAC_NO = res.data.PJOB_TRAC_NO_RTN;
                            $scope.$parent.form.UNIT_PRICE = parseFloat(res.data.PUNIT_PRICE_RTN);
                            $scope.$parent.form.TOT_ORD_VALUE = $scope.$parent.form.TOT_ORD_QTY * parseFloat(res.data.PUNIT_PRICE_RTN)
                            console.log(result[0]);

                            vm.selectedItem = null;

                            if (result[0].itmsOrdShipDT.length > 0) {
                                $scope.$parent.form.itmsOrdShipDT = result[0].itmsOrdShipDT;
                            }
                            else {
                                $scope.$parent.form.itmsOrdShipDT = [
                                    //{
                                        //ITEM_INDEX: 0, MC_STYLE_D_ITEM_ID: 0, ORDER_QTY: 0, QTY_MOU_ID: null, UNIT_PRICE: 0, TOTAL_PRICE: 0,
                                        //itemsColor: [
                                        //    //{ ITEM_INDEX: 0, COLOR_INDEX: 0, MC_COLOR_ID: 0, COLOR_NAME_EN: null }
                                        //]
                                    //}
                                ];
                            }
                            //console.log(res);
                            //console.log(vm.form);
                            //console.log(vm.obGrid);
                        });
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        vm.lsitView = function () {
            //$state.go('OrderList', { pOrderObj: $scope.$parent.form });
            $state.go('OrderList', { pMC_BYR_ACC_ID: $scope.$parent.form.MC_BYR_ACC_ID, pMC_STYLE_H_ID: $scope.$parent.form.MC_STYLE_H_EXT_ID });
        };

        vm.cancel = function () {
            vm.errors = undefined;
            $state.go('OrderH.Dtl', { pMC_ORDER_H_ID: 0 }, { inherit: false });            
        };

        vm.exportOrdData = function () {
            $scope.$parent.form.REPORT_CODE = 'RPT-2004';
            $scope.$parent.form.IS_EXCEL_FORMAT = 'Y';
            //$scope.$parent.form.MC_BLK_FAB_REQ_H_ID = dataItem.MC_BLK_FAB_REQ_H_ID;
            //$scope.$parent.form.IS_MULTI_SHIP_DT = dataItem.IS_MULTI_SHIP_DT;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
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

            //return MrcDataService.GetAllOthers('/api/mrc/Order/ExportOrderDataToExcel/' + parseInt(vm.form.MC_ORDER_H_ID) + '/' + vm.form.STYLE_NO).then(function (res) {
            //    res['data'] = angular.fromJson(res.jsonStr);
            //    console.log(res['data']);
            //    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
            //        vm.isSaved = true;
            //    };

            //    config.appToastMsg(res.data.PMSG);

            //}, function (err) {
            //    console.log(err);
            //});
        }

        vm.gotoBulkBooking = function () {
            if ($scope.$parent.form.IS_TNA_FINALIZED=='Y') {
                if ($scope.$parent.form.MC_BLK_FAB_REQ_H_ID < 1) {
                    $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: $scope.$parent.form.MC_BLK_FAB_REQ_H_ID, SID: $scope.$parent.form.MC_STYLE_H_ID, BAID: $scope.$parent.form.MC_BYR_ACC_ID, SEXID: $scope.$parent.form.MC_STYLE_H_EXT_ID, ORDID: $scope.$parent.form.MC_ORDER_H_ID });
                }
                else {
                    $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: $scope.$parent.form.MC_BLK_FAB_REQ_H_ID });
                }
            }
            else {
                vm.errors = [];
                vm.errors.push({ key: ['Please finalize the order TNA then goto fabric booking'] });
                //alert('Please finalize the order TNA then goto fabric booking');
            }
        };

        vm.orderFinalize = function (data, token) {
            Dialog.confirm('Do you want to finalize the order?', 'Are you sure?', ['Yes', 'No'])
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

        $scope.orderRevisionModal = function (obj) {
            
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'OrderRevisionModal.html',
                controller: 'OrderRevisionModalController',
                size: 'small',
                //scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    orderData: function () {
                        return obj;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log('received');
                console.log(data);

                var vRevisionData = angular.copy(data);
                $scope.$parent.form.REVISION_NO = vRevisionData.REVISION_NO;
                $scope.$parent.form.REVISION_DT = vRevisionData.REVISION_DT;
                $scope.$parent.form.REV_REASON = vRevisionData.REV_REASON;
                $scope.$parent.form.IS_ORD_FINALIZED = vRevisionData.IS_ORD_FINALIZED;

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.uploadDoc = function () {
            
            var modalInstance = $modal.open({
                animation: true,                
                templateUrl: '/GlobalUI/UploadOtherDocs',
                controller: 'UploadOtherDocsController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'app-modal-window', //'large-Modal', //'app-modal-window',
                resolve: {
                    orderData: function () {
                        return $scope.form;
                    },
                    pageAccess: function () {
                        return { IS_ONLY_VIEW: 'N' };
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
             
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

        //$('#Button').removeAttr('disabled');
        //$(".btnSelect").button({ disabled: false });
        //$(".btnSelect").removeAttr('disabled');
        


    }

})();





// Start Revision Modal Controller
(function () {
    'use strict';

    angular.module('multitex').controller('OrderRevisionModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'orderData', 'MrcDataService', 'Dialog', OrderRevisionModalController]);
    function OrderRevisionModalController($modalInstance, $q, $scope, $http, $state, $filter, config, orderData, MrcDataService, Dialog) {

        $scope.showSplash = true;
        
        activate();

        $scope.today = new Date();
        $scope.dtFormat = config.appDateFormat;
        $scope.formInvalid = false;
        
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        

        var orderObj = angular.copy(orderData);
        $scope.REVISION_DT = $scope.today;
        if (parseInt(orderObj.REVISION_NO) > 0) {
            $scope.REVISION_NO = parseInt(orderObj.REVISION_NO) + 1;
        }
        else {
            $scope.REVISION_NO = 1;
        }
        
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

        

        $scope.save = function (token, valid) {
            if (!valid) return;

            //$scope.$parent.errors = [];

            //if ($scope.$parent.errors != undefined && $scope.$parent.errors.length > 0) {
            //    return;
            //}
            //else {
            //    $scope.$parent.errors = undefined;
            //}
            
            var submitData = { MC_ORDER_H_ID: orderObj.MC_ORDER_H_ID, REVISION_DT: $filter('date')($scope.REVISION_DT, $scope.dtFormat), REV_REASON: $scope.REV_REASON };

            Dialog.confirm('Do you want to revision?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/mrc/Order/SaveOrderRevision',
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
                                 submitData['IS_ORD_FINALIZED'] = "N";
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
// End Revision Modal Controller




//////============= Order List Controller ======================
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('orderListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$window', '$state', '$stateParams', '$modal', 'MrcDataService', orderListController]);
    function orderListController(logger, config, $q, $scope, $http, exception, $filter, $window, $state, $stateParams, $modal, MrcDataService) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();
        vm.form = { IS_MCR: 'N' };
        vm.obGrid = [];
        vm.selectedRow = {};
        vm.width = $('#PORTLET_BODY').width(); //$window.innerWidth;

        var orderDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    e.success([]);
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            group: [{ field: 'STYLE_NO' }],
            sort: [{ field: 'STYLE_NO', dir: 'asc' }, { field: 'ORD_CONF_DT', dir: 'desc' }],
            pageSize: 10
        });


        //alert(angular.element($window).width);

        activate();
        function activate() {
            var promise = [getBuyerAccListByUserId()];
            return $q.all(promise).then(function () {

                vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                vm.showSplash = false;
            });
        }



        //added by Aminul by tna generation
        vm.generateTna = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'GenerateTnaTemplate.html',
                controller: 'GenerateTnaModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    Order: function () {
                        return data;
                    },
                    TnATaskList: function (MrcDataService) {
                        return MrcDataService.getDataByUrl('/StyleH/TnAList/TnATemp/' + data.MC_TNA_TMPLT_H_ID + '/Order/' + data.MC_ORDER_H_ID + '/Buyer/' + data.MC_BUYER_ID);
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }


        function getBuyerAccListByUserId() {
            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "startswith",
                autoBind: true,
                dataBound: function (e) {
                    var MC_BYR_ACC_ID = this.dataItem(e.item).MC_BYR_ACC_ID;
                    if (MC_BYR_ACC_ID) {
                        vm.showOrder();
                    }
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId/').then(function (res) {
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
                    vm.form.MC_BUYER_ID = null;
                    vm.form.MC_BUYER_OFF_ID = null;
                }
            };
        }

        function getLocalAgentList() {
            return vm.localAgentList = {
                optionLabel: "--- Local Agent---",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerOffice/OfficeDatasByUserID').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BOFF_NAME_EN",
                dataValueField: "MC_BUYER_OFF_ID"
            };
        }

        function getBuyerListData() {
            return vm.buyerList = {
                optionLabel: "-- Select Buyer --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/buyer/BuyerByUserList').then(function (res) {
                                e.success(res);
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

        vm.showOrder = function () {
            //var vBuyerId = null;
            //var vBuyerOffId = null;

            //if (vm.form.MC_BUYER_ID == ""){
            //    vBuyerId = null;
            //}
            //else {
            //    vBuyerId = vm.form.MC_BUYER_ID;
            //}

            //if (vm.form.MC_BUYER_OFF_ID == ""){
            //    vBuyerOffId = null;
            //}
            //else {
            //    vBuyerOffId = vm.form.MC_BUYER_OFF_ID;orderDataList
            //}

            return MrcDataService.getDataByUrl('/Order/OrderHdrDataList?pMC_BYR_ACC_ID=' + vm.form.MC_BYR_ACC_ID + '&pEX_ORD_STATUS_ID=786').then(function (res) {
                orderDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            angular.forEach(res, function (val, key) {
                                val['ORDER_DT'] = $filter('date')(val['ORDER_DT'], vm.dtFormat);
                                val['ORD_CONF_DT_STR'] = $filter('date')(val['ORD_CONF_DT'], vm.dtFormat);
                                val['SHIP_DT'] = $filter('date')(val['SHIP_DT'], vm.dtFormat);
                            });

                            e.success(res);
                        },
                        parameterMap: function (options, operation) {
                            if (operation !== "read" && options.models) {
                                return { models: kendo.stringify(options.models) };
                            }
                        }
                    },
                    group: [{ field: 'STYLE_NO' }],
                    sort: [{ field: 'STYLE_NO', dir: 'asc' }, { field: 'ORD_CONF_DT', dir: 'desc' }],
                    pageSize: 50,
                });

                $('#orderGrid').data("kendoGrid").setDataSource(orderDataSource);
            });
        };

        vm.addNewOrder = function () {
            return $state.go('OrderH.Dtl', { pMC_ORDER_H_ID: 0, BAID: vm.form.MC_BYR_ACC_ID });
        };

        vm.editOrderRecord = function (dataItem) {
            vm.form = dataItem;
            return $state.go('OrderH.Dtl', { pMC_ORDER_H_ID: dataItem.MC_ORDER_H_ID });
        };

        

        vm.gridOptionsOrder = {
            autoBind: true,
            height: '350px',
            scrollable: true,
            navigatable: true,
            dataSource: orderDataSource,
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
            selectable: false,
            dataBound: function (e) {
                collapseAllGroups(this);
            },
            sortable: true,
            //pageSize: 10,
            //pageable: true,
            refresh: true,
            columns: [
                { field: "STYLE_NO", title: "Style #", type: "string", width: "100px", hidden: true },
                { field: "JOB_TRAC_NO", title: "Job #", type: "string", width: "120px", hidden: true },
                { field: "ORD_CONF_DT_STR", title: "Order Date", type: "date", width: "100px", format: "{0:" + vm.dtFormat + "}" },
                { field: "ORDER_NO", title: "Order #", type: "string", width: "120px" },
                { field: "WORK_STYLE_NO", title: "Work Style #", type: "string", width: "100px" },
                { field: "SHIP_DT", title: "Ship. Date", type: "date", width: "100px", format: "{0:" + vm.dtFormat + "}" },
                { field: "TOT_ORD_QTY", title: "Total Qty", type: "string", width: "100px" },
                { field: "MOU_CODE", title: "Unit", type: "string", width: "100px" },
                { field: "TOT_ORD_VALUE", title: "Total Price", type: "string", width: "100px" },
                { field: "CURR_NAME_EN", title: "Currency", type: "string", width: "80px" },
                {
                    title: "Action",
                    template: function () {
                        //return "<a class='btn btn-xs blue' ng-click='vm.test1()' onclick='vm.removeRecord(dataItem)'  title='Remove'><i class='fa fa-remove'></i> Remove</a>";
                        return "<a ng-click='vm.editOrderRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-edit'> Details Order</i></a>     &nbsp;<a ng-disabled='!dataItem.MC_TNA_TMPLT_H_ID' ng-click='vm.generateTna(dataItem)' class='btn btn-xs purple'><i class='fa fa-edit'>T&A</i> </a>";
                    },
                    width: "180px"
                },
            ]
        };

        function collapseAllGroups(grid) {
            grid.table.find(".k-grouping-row").each(function () {
                grid.collapseGroup(this);
            });
        }





    }

})();
