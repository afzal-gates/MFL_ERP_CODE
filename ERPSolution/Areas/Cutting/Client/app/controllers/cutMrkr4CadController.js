////////// Start CutMrkr4CadController Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutMrkr4CadController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', 'mrkrHdrData', CutMrkr4CadController]);
    function CutMrkr4CadController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog, mrkrHdrData) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
             
        
        var key = 'GMT_MRKR_ID';
        vm.today = new Date();

        vm.mainSupportList = [{ IS_MAIN_SUPPORT: 'M', MAIN_SUPPORT_NAME: 'Main' }, { IS_MAIN_SUPPORT: 'S', MAIN_SUPPORT_NAME: 'Support' }];

        vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p>(#: data.ORDER_NO #)</p></span>';
        vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';

        vm.form = mrkrHdrData[key] ? mrkrHdrData : {
            GMT_MRKR_ID: 0, MC_BYR_ACC_GRP_ID: 0, MC_STYLE_H_EXT_ID: null, MC_STYLE_H_ID: 0, MC_ORDER_H_ID: null, MC_COLOR_ID: null,
            LK_WAY_TYPE_ID: 668, LK_MRKR_TYPE_ID: 669, itemsOrdSizeRatio: []
        };

        if (mrkrHdrData[key] > 0) {
            vm.IS_SHOW_MARKER = 'Y';
        }
        else {
            vm.IS_SHOW_MARKER = 'N';
        }

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        vm.sameColorsDataSource = new kendo.data.DataSource({
            data: []
        });


        activate();
        function activate() {
            var promise = [getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getOrderColor(), getFabricByStyleId(), getMrkrWayTypeList(), getMrkrTypeList(),
                getBookingDiaByProdOrdId(), getGmtPartList()
            ];
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

                    //vm.form.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;
                    vm.form.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;

                    //vm.getOrderColor(item.MC_FAB_PROD_ORD_H_ID);
                    
                    vm.styleFabricDataSource.read();
                    vm.ordColorDataSource.read();
                    vm.bookingDiaDataSource.read();

                    CuttingDataService.getDataByFullUrl("/api/mrc/Order/OrderWiseSizeList/" + vm.form.MC_ORDER_H_ID).then(function (res) {

                        vm.form['itemsOrdSizeRatio'] = res;
                                
                    });
                }
            };            

            vm.OrdStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData);

                        if (paramsData[2]) {
                            var url = '/api/mrc/Order/GetStyleExOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0);
                        }
                        else {
                            var url = '/api/mrc/Order/GetStyleExOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0) + '&pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID;
                        }

                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += CuttingDataService.kFilterStr2QueryParam(params.filter);




                        //var webapi = new kendo.data.transports.webapi({});
                        //var params = webapi.parameterMap(e.data);
                        //var url = '/api/mrc/Order/GetStyleExOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0);
                        
                        //url += CuttingDataService.kFilterStr2QueryParam(params.filter);

                        //var paramsData = params.filter.replace(/'/g, '').split('~');
                        //console.log(paramsData);

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
            
        };

        //api/mrc/StyleDItem/ChildItemListByStyle/1
        function getOrderItem() {
            vm.OrdItemOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "MC_STYLE_D_ITEM_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    $scope.viewOrdItem.FROM_COLOR_NAME_EN = item.COLOR_NAME_EN;
                //    console.log(item);
                //}
            };

            return vm.OrdItemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/StyleDItem/ChildItemListByStyle/' + vm.form.MC_STYLE_H_ID;

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {

                            angular.forEach(res, function (val, key) {
                                if (val['MODEL_NO'] != '') {
                                    val['ITEM_NAME_EN'] = val['ITEM_NAME_EN'] + ' ' + val['MODEL_NO'];
                                }
                                else if (val['COMBO_NO'] != '') {
                                    val['ITEM_NAME_EN'] = val['ITEM_NAME_EN'] + ' ' + val['COMBO_NO'];
                                }
                            });

                            e.success(res);
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

                    var colorList = vm.ordColorDataSource.data();

                    console.log(colorList);

                    vm.sameColorsDataSource = new kendo.data.DataSource({
                        data: _.filter(colorList, function (ob) {
                            if (ob.MC_COLOR_ID != item.MC_COLOR_ID) {
                                return ob;
                            }
                        })
                        //data: colorList //[{MC_COLOR_ID:1, COLOR_NAME_EN:'Test'}]
                    });
                }//,
                //dataBound: function (e) {                    
                //    var colorList = vm.ordColorDataSource.data();

                //    console.log(colorList);

                //    vm.sameColorsDataSource = new kendo.data.DataSource({
                //        data: _.filter(colorList, function (ob) {
                //            if (ob.MC_COLOR_ID == vm.form.GMT_COLOR_ID) {
                //                return ob;
                //            }
                //        })                        
                //    });
                //}
            };

            return vm.ordColorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/Order/OrderOrByerAccWiseColorList/' + vm.form.MC_ORDER_H_ID + '/null';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            
                            var colorList = _.map(_.groupBy(res, function (doc) {
                                return doc.MC_COLOR_ID;
                            }), function (grouped) {
                                return grouped[0];
                            });

                            e.success(colorList);

                            if (colorList.length == 1) {
                                vm.form.GMT_COLOR_ID = colorList[0].MC_COLOR_ID;

                                vm.sameColorsDataSource = new kendo.data.DataSource({
                                    data: _.filter(colorList, function (ob) {
                                        if (ob.MC_COLOR_ID != colorList[0].MC_COLOR_ID) {
                                            return ob;
                                        }
                                    })
                                });
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getFabricByStyleId() {

            vm.styleFabricOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FABRIC_DESC",
                dataValueField: "MC_STYLE_D_FAB_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.bookingDiaDataSource.read();

                    //vm.fabOrder.FABRIC_DESC = item.FABRIC_DESC;
                    //vm.fabOrder.RF_FAB_TYPE_ID = item.RF_FAB_TYPE_ID;
                    //vm.fabOrder.RF_FIB_COMP_ID = item.RF_FIB_COMP_ID;
                    //vm.fabOrder.FIN_GSM = item.FB_WT_MAX;
                }
            };

            return vm.styleFabricDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CuttingDataService.getDataByFullUrl('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + ((vm.form.MC_STYLE_H_ID > 0) ? vm.form.MC_STYLE_H_ID : 0)).then(function (res) {
                            e.success(res);

                            if (res.length == 1) {
                                vm.form.MC_STYLE_D_FAB_ID = res[0].MC_STYLE_D_FAB_ID;
                            }
                        });
                    }
                }
            });

        };
        
        function getMrkrWayTypeList() {

            return vm.mrkrWayTypeList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CuttingDataService.LookupListData(135).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getMrkrTypeList() {

            return vm.mrkrTypeList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CuttingDataService.LookupListData(136).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getBookingDiaByProdOrdId() {

            vm.bookingDiaOption = {
                optionLabel: "--- Select Dia ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.BK_FIN_DIA = item.FIN_DIA;
                    vm.form.DIA_MOU_ID = item.DIA_MOU_ID;                    
                },
                dataTextField: "FIN_DIA_N_DIA_TYPE",
                dataValueField: "FIN_DIA_N_DIA_TYPE"
            };

            return vm.bookingDiaDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CuttingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetFinDiaByProdOrdId?&pRF_FAB_PROD_CAT_ID=2&pMC_STYLE_H_EXT_ID=' + (vm.form.MC_STYLE_H_EXT_ID || $stateParams.pMC_STYLE_H_EXT_ID || 0)).then(function (res) {
                            e.success(res);

                            if (res.length == 1) {
                                vm.form.FIN_DIA_N_DIA_TYPE = res[0].FIN_DIA_N_DIA_TYPE;
                            }
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        }

        function getGmtPartList() {

            vm.gmtPartDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        var url = '/api/common/GmtPartList';

                        console.log(url);

                        CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res)
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


        vm.mrkrListGridOption = {
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
                //{ field: "CHALAN_DT", title: "Date", format: "{0:dd-MM-yyyy}", width: "100px", editable: false, filterable: false },
                { field: "MRKR_REF_NO", title: "Ref#", width: "12%", filterable: true },
                { field: "MRKR_SH_DESC", title: "Short Description", width: "23%", filterable: false },
                { field: "WAY_TYPE_NAME", title: "Type of Way", width: "15%", filterable: false },
                { field: "MARKER_TYPE_NAME", title: "Type of Marker", width: "15%", filterable: false },
                { field: "BK_FIN_DIA", title: "Bk/Dia", width: "5%", filterable: false },
                { field: "CUT_FIN_DIA", title: "Cut/Dia", width: "5%", filterable: false },
                { field: "MRKR_LEN", title: "Length", width: "5%", filterable: false },
                { field: "MRKR_WDT", title: "Width", width: "5%", filterable: false },
                { field: "IS_PATERN", title: "Pattern", width: "10%", filterable: false },
                {
                    field: "MRKR_STATUS_NAME",
                    title: "Status",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.IS_APROVED==\"N\"' >{{dataItem.MRKR_STATUS_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.IS_APROVED==\"Y\"' >{{dataItem.MRKR_STATUS_NAME}}</label>";
                    },
                    width: "10%"
                },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' ><i class='fa fa-edit'></i></button>";
                    },
                    width: "5%"
                }
            ]
        };


        vm.mrkrListGridDataSource = new kendo.data.DataSource({                       
            transport: {
                read: function (e) {                    
                    var url = '/api/Cutting/MrkrReq/GetMarkerList?pMC_STYLE_H_ID=' + (vm.form.MC_STYLE_H_ID || 0) + '&pMC_ORDER_H_ID=' + (vm.form.MC_ORDER_H_ID || null) + '&pGMT_COLOR_ID=' + (vm.form.GMT_COLOR_ID || null);
                    console.log(url);

                    CuttingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },           
            schema: {               
                model: {
                    id: "GMT_MRKR_ID",
                    fields: {                        
                        MRKR_REF_NO: { type: "string", filterable: false }
                    }
                }
            }
        });
                
        vm.addNewMarker = function () {                        
            
            var formCopy = angular.copy(vm.form);

            vm.form.MRKR_REF_NO = "";
            vm.form.MRKR_SH_DESC = "";

            vm.form.GMT_MRKR_ID = 0;
            vm.form.LK_WAY_TYPE_ID = 668;
            vm.form.LK_MRKR_TYPE_ID = 669;

            vm.form.CUT_FIN_DIA = null;
            vm.form.MRKR_LEN = null;
            vm.form.MAX_PLY_QTY = null;
            vm.form.MRKR_WDT = null;
            vm.form.IS_PATERN = "N";
            vm.form.CONS_PER_DZ = null;
            vm.form.PCT_MRKR_EFFC = null;
            vm.form.IS_APROVED = "N";

            vm.form.itemsOrdSizeRatio = [];

            getOrdSizeRatio(vm.form);
            $state.go('MrkrH', { pGMT_MRKR_ID: vm.form.GMT_MRKR_ID }, { notify: false });

        }
        
        //vm.cancel = function () {
        //    vm.IS_SHOW_MARKER = 'N';
        //    $state.go('MrkrH', { pGMT_MRKR_ID: 0 }, { reload: true });           
        //}

        vm.editRow = function (dataItem) {
            vm.IS_SHOW_MARKER = 'Y';
            vm.form = angular.copy(dataItem);

            $state.go('MrkrH', { pGMT_MRKR_ID: dataItem.GMT_MRKR_ID }, { notify: false });

            getOrdSizeRatio(dataItem);
        }


        function getOrdSizeRatio(dataItem) {

            console.log(dataItem);

            CuttingDataService.getDataByFullUrl("/api/Cutting/MrkrReq/GetOrdSizeCutRatio?pMC_ORDER_H_ID=" + (dataItem['MC_ORDER_H_ID']||0) + '&pGMT_COLOR_ID=' + (dataItem['GMT_COLOR_ID']||0)
                + '&pGMT_MRKR_ID=' + (dataItem['GMT_MRKR_ID']||0)).then(function (res) {

                vm.form['itemsOrdItm'] = res;
            });
        };

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
              
        $scope.$watch('vm.form.GMT_COLOR_ID', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.IS_SHOW_MARKER = 'Y';
                    vm.mrkrListGridDataSource.read();

                    getOrdSizeRatio(vm.form);
                }
            }
        }, true);


        vm.save = function (pIS_APROVED) {
            var vMsg = 'Do you want to save?';
            var submitData = angular.copy(vm.form);
            var ratioData = [];

            submitData['IS_APROVED'] = pIS_APROVED;

            if (pIS_APROVED == 'Y') {
                vMsg = 'Do you want to save and finalize?';
            }
            

            angular.forEach(submitData['itemsOrdItm'], function (val, key) {
                angular.forEach(val['itemsOrdSizeRatio'], function (val1, key1) {
                    ratioData.push({ GMT_MRKR_D_ID: val1.GMT_MRKR_D_ID || 0, GMT_MRKR_ID: submitData['GMT_MRKR_ID'], MC_STYLE_D_ITEM_ID: val.MC_STYLE_D_ITEM_ID, MC_SIZE_ID: val1.MC_SIZE_ID, RATIO_QTY: val1.RATIO_QTY, SIZE_QTY: val1.SIZE_QTY, GMT_MRKR_RATIO_GRP_ID: val1.GMT_MRKR_RATIO_GRP_ID });
                });
            });

            submitData.GMT_MRKR_D_XML = CuttingDataService.xmlStringShort(ratioData.map(function (o) {
                return o;
            }));


            console.log(submitData);
            //return;


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + $scope.token },
                         url: '/api/Cutting/MrkrReq/MrkrBatchSave',
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

                             if (res.data.PGMT_MRKR_ID_RTN > 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                 vm.form['GMT_MRKR_ID'] = res.data.PGMT_MRKR_ID_RTN;
                                 vm.form['MRKR_REF_NO_RTN'] = res.data.PMRKR_REF_NO_RTN;
                                 
                                 if (pIS_APROVED == 'Y') {
                                     vm.form['IS_APROVED'] = pIS_APROVED;
                                 }

                                 vm.mrkrListGridDataSource.read();
                                 getOrdSizeRatio(vm.form);

                                 $state.go('MrkrH', { pGMT_MRKR_ID: res.data.PGMT_MRKR_ID_RTN }, { notify: false });
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
        

    }

})();
////////// End CutMrkr4CadController Controller






