////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScGreyFabRcvFromPartyHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'formData', 'formScPrgIssList', 'KnittingDataService', 'Dialog', KntScGreyFabRcvFromPartyHController]);
    function KntScGreyFabRcvFromPartyHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, formData, formScPrgIssList, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.fabRcvState = true;
        vm.IS_NEXT = 'Y';

        formData['prgIssDtl'] = formScPrgIssList;
        var key = 'KNT_SCO_CHL_RCV_H_ID';
        console.log(formData);
        console.log($stateParams);
        console.log(formData['KNT_SCO_CHL_RCV_H_ID']);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.today = new Date();
        vm.form = formData[key] ? formData : { KNT_SCO_CHL_RCV_H_ID: 0, LK_CLCF_CHL_TYP_ID: 560, IS_SUB_CONTR: 'Y', IS_FINALIZED: 'N', LK_CLCF_MVM_STS_ID: 570, RCV_DT: moment(vm.today).format("DD-MMM-YYYY"), prgIssDtl: [], fabRcvDtl: [], yrnRcvDtl: [], clcfRcvDtl: [] }; //formData[key] ? formData : { KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', IS_FINALIZE: 'N' };
        $scope.form = formData[key] ? formData : { KNT_SCO_CHL_RCV_H_ID: 0, LK_CLCF_CHL_TYP_ID: 560, IS_SUB_CONTR: 'Y', IS_FINALIZED: 'N', LK_CLCF_MVM_STS_ID: 570, RCV_DT: moment(vm.today).format("DD-MMM-YYYY"), prgIssDtl: [], fabRcvDtl: [], yrnRcvDtl: [], clcfRcvDtl: [] }; //formData[key] ? formData : { KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', IS_FINALIZE: 'N' };

        
        $scope.isFabAddToGrid = 'Y';
        $scope.selectedFabRow = {};
           
        vm.fabRcvState = ($state.current.name === 'ScGreyFabRcvFormPartyH.FabRcv') ? true : false;
        vm.yrnRcvState = ($state.current.name === 'ScGreyFabRcvFormPartyH.YrnRcv') ? true : false;
        vm.clcfRcvState = ($state.current.name === 'ScGreyFabRcvFormPartyH.ClcfRcv') ? true : false;

        
        
        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getSupplierList(), /*getScProgList(),*/ getStoreList()];
            return $q.all(promise).then(function () {               
                vm.showSplash = false;                
            });
        }


        vm.scRcvDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.scRcvDateOpened = true;
        };

        vm.chlnDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.chlnDateOpened = true;
        };

        if (formData[key]) {
            vm.fabRcvState = true;
            vm.IS_NEXT = 'Y';
            //return KnittingDataService.getDataByFullUrl('/api/Knit/KntScChlnRcv/GetScPrgIssList?&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0)).then(function (res) {
            //    vm.form.prgIssDtl = res;
            //    //$scope.form.prgIssDtl = res;

            //});
            //vm.gotoNext();
        }
        else {
            vm.IS_NEXT = 'N';
        }

        function getStoreList() {
            vm.storeOption = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                //dataSource: {
                //    transport: {
                //        read: function (e) {
                //            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                //                //KnittingDataService.LookupListData(92).then(function (res) {
                //                e.success(res);
                //            });
                //        }
                //    }
                //},
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);                    
                //},
                //dataBound: function (e) {
                //    if (formData[key]) {
                //        vm.form.SCM_STORE_ID = formData.SCM_STORE_ID;
                //    }
                //}
            };

            return vm.storeDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5';
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

                    vm.form.prgIssDtl = [];
                    vm.form.yrnRcvDtl = [];
                    vm.form.clcfRcvDtl = [];
                    vm.IS_NEXT = 'N';

                    vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    $scope.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;

                    $state.go($state.current.name, { pKNT_SCO_CHL_RCV_H_ID: ($stateParams.KNT_SCO_CHL_RCV_H_ID||0), pSCM_SUPPLIER_ID: item.SCM_SUPPLIER_ID }, { notify: false });
                },
                dataBound: function (e) {
                    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                        vm.gotoNext();
                    }
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

        function getScProgList() {
            vm.scProgOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "PRG_RCV_NO",
                dataValueField: "KNT_SC_PRG_RCV_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    //vm.form.SUP_TRD_NAME_EN = item.SUP_TRD_NAME_EN;
                    //vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    vm.form.SC_PRG_DT = item.SC_PRG_DT;
                }//,
                //dataBound: function (e) {
                //    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                //        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                //    }
                //}
            };

            return vm.scProgDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScRcv/GetScRcvProgramList?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID;
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

        

        $scope.$watch('vm.form', function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    //vm.form['SC_FAB_DELV_CHALAN_NO'] = vm.form.CHALAN_NO;
                    $scope.form = vm.form;
                    //$scope.form.CHALAN_DT = $filter('date')(vm.form.CHALAN_DT, vm.dtFormat);
                }
            }
        }, true);

        //$scope.$watchGroup(['vm.form.SCM_SUPPLIER_ID'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            //$state.go('ScGreyFabRcvFormPartyH', { pKNT_SCO_CHL_RCV_H_ID: 0, pSCM_SUPPLIER_ID: newVal }, { notify: false });
        //            vm.form.prgIssDtl = [];
        //            vm.form.yrnRcvDtl = [];
        //            vm.IS_NEXT = 'N';                    
        //        }
        //    }
        //}, true);

        vm.gotoNext = function () {
            //if (vm.fabRcvState == true) {
            //    $state.go('ScGreyFabRcvFormPartyH.FabRcv');
            //}
            //else {
            //    $state.go('ScGreyFabRcvFormPartyH.YrnRcv');
            //}

            vm.fabRcvState = true;
            vm.IS_NEXT = 'Y';
            $scope.orderColorSearchGridDataSource.read();
                        
            //$state.go('ScGreyFabRcvFormPartyH.FabRcv', { pKNT_SCO_CHL_RCV_H_ID: 0, pSCM_SUPPLIER_ID: vm.form.SCM_SUPPLIER_ID }, { notify: false });
            
             return KnittingDataService.getDataByFullUrl('/api/Knit/KntScChlnRcv/GetScPrgIssList?&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0)).then(function (res) {
                 vm.form.prgIssDtl = res;
                 //$scope.form.prgIssDtl = res;
                
             });                           
        }

        $scope.orderColorSearchGridDataSource = new kendo.data.DataSource({
            serverFiltering: true,
            transport: {
                read: function (e) {

                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = '/api/knit/KntCollarCuff/GetOrdCol4StrRcv?pLK_CLCF_CHL_TYP_ID=560&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID || 0);

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


        $scope.clcfChlnRcvGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    var url = '/api/knit/KntScoChlnRcv/GetChlnDtl4ClcfStrRcv?pKNT_SCO_CHL_RCV_H_ID=' + ($stateParams.pKNT_SCO_CHL_RCV_H_ID || vm.form.KNT_SCO_CHL_RCV_H_ID || 0);
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

        vm.batchSave = function (pIS_FINALIZED) {
            var submitRcvData = angular.copy($scope.form);
            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZED == 'Y') {
                submitRcvData['IS_FINALIZED'] = 'Y';
                vMsg = 'After finalize, the challan is lock.</br></br> <b>Do you want to save and finalize?</b>';
            }


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {
                
                submitRcvData['RCV_DT'] = $filter('date')(submitRcvData['RCV_DT'], vm.dtFormat);
                submitRcvData['CHALAN_DT'] = $filter('date')(submitRcvData['CHALAN_DT'], vm.dtFormat);

                //var fabRcvDtl = [];
                //angular.forEach(submitRcvData.prgIssDtl, function (val, key) {
                //    angular.forEach(val['fabRcvDtl'], function (val1, key1) {
                //        fabRcvDtl.push(val1);
                //    });
                //});
                //console.log(fabRcvDtl);


                submitRcvData.FAB_RCV_DTL_XML = KnittingDataService.xmlStringShort(submitRcvData['fabRcvDtl'].map(function (ob) {
                    return {
                        KNT_SCO_CHL_RCV_D_ID: ob.KNT_SCO_CHL_RCV_D_ID, KNT_SCO_CHL_RCV_H_ID: ob.KNT_SCO_CHL_RCV_H_ID, KNT_STYL_FAB_ITEM_ID: ob.KNT_STYL_FAB_ITEM_ID,
                        FAB_RCV_INDEX: ob.FAB_RCV_INDEX, RCV_NO_ROLL: ob.RCV_NO_ROLL, RCV_ROLL_WT: ob.RCV_ROLL_WT, WT_MOU_ID: ob.WT_MOU_ID,
                        NOTE: ob.NOTE, KNT_QC_STS_TYPE_ID: ob.KNT_QC_STS_TYPE_ID
                    };
                }));

          
                submitRcvData.YRN_RCV_DTL_XML = KnittingDataService.xmlStringShort(submitRcvData['yrnRcvDtl'].map(function (ob) {
                    return ob;
                }));


                var rcvData = $scope.clcfChlnRcvGridDataSource.data();
                //console.log(rcvData);
                
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

                return KnittingDataService.saveDataByFullUrl(submitRcvData, '/api/knit/KntScChlnRcv/BatchSave').then(function (res) {
                    console.log(res);

                    vm.errors = undefined;
                    if (res.success === false) {
                        vm.errors = [];
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //$scope.fabOrderGridDataSource.read();
                            //vm.getChallanList();
                            if (res.data.PKNT_SCO_CHL_RCV_H_ID_RTN != 0) {
                                $stateParams.pKNT_SCO_CHL_RCV_H_ID = res.data.PKNT_SCO_CHL_RCV_H_ID_RTN;
                                $scope.form.KNT_SCO_CHL_RCV_H_ID = res.data.PKNT_SCO_CHL_RCV_H_ID_RTN;

                                $scope.clcfChlnRcvGridDataSource.read();
                                
                                KnittingDataService.getDataByFullUrl('/api/Knit/KntScChlnRcv/GetRcvChallanHdr?&pKNT_SCO_CHL_RCV_H_ID=' + ($stateParams.pKNT_SCO_CHL_RCV_H_ID || 0)).then(function (result) {
                                    console.log(result);

                                    vm.form.IS_FINALIZED = result['IS_FINALIZED'];
                                    $scope.form.IS_FINALIZED = result['IS_FINALIZED'];

                                    $scope.form['fabRcvDtl'] = result['fabRcvDtl'];
                                    $scope.form['yrnRcvDtl'] = result['yrnRcvDtl'];

                                    if (vm.yrnRcvState == true) {
                                        $state.go('ScGreyFabRcvFormPartyH.YrnRcv', { pKNT_SCO_CHL_RCV_H_ID: res.data.PKNT_SCO_CHL_RCV_H_ID_RTN, pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID }, { reload: 'ScGreyFabRcvFormPartyH.YrnRcv' });
                                    }
                                    else if (vm.clcfRcvState == true) {
                                        $state.go('ScGreyFabRcvFormPartyH.ClcfRcv', { pKNT_SCO_CHL_RCV_H_ID: res.data.PKNT_SCO_CHL_RCV_H_ID_RTN, pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID }, { reload: 'ScGreyFabRcvFormPartyH.ClcfRcv' });
                                    }
                                    else {
                                        $state.go('ScGreyFabRcvFormPartyH.FabRcv', { pKNT_SCO_CHL_RCV_H_ID: res.data.PKNT_SCO_CHL_RCV_H_ID_RTN, pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID }, { reload: 'ScGreyFabRcvFormPartyH.FabRcv' });
                                    }
                                    
                                });                                                              
                            }

                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });



            });
        };

        vm.cancel = function () {
            vm.errors = undefined;
            //$state.go('KntScGreyFabDelvH.dtl', { pKNT_SC_GFAB_DLV_H_ID: 0 }, { inherit: false });
            //$scope.$parent.IS_NEXT = 'N';
            $state.go('ScGreyFabRcvFormPartyH.FabRcv', { pKNT_SCO_CHL_RCV_H_ID: 0, pSCM_SUPPLIER_ID: $scope.form.SCM_SUPPLIER_ID });

            $scope.form = { KNT_SCO_CHL_RCV_H_ID: 0, RCV_DT: moment(vm.today).format("DD-MMM-YYYY"), prgIssDtl: [], fabRcvDtl: [], yrnRcvDtl: [] };
        }

        vm.backToList = function () {
            return $state.go('ScGreyFabRcvFormPartyList', { pSCM_SUPPLIER_ID: $scope.form.SCM_SUPPLIER_ID });
        };

        vm.print = function (itm) {
            var rptCode;
            if (itm.BTN_ID == 1) {
                rptCode = 'RPT-3508';
            }
            else if (itm.BTN_ID == 2) {
                rptCode = 'RPT-3514';
            }

            var data = angular.copy($scope.form);

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
////////// End Header Controller





////////// Start Fabric Receive Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScGreyFabRcvFromPartyFabRcvController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntScGreyFabRcvFromPartyFabRcvController]);
    function KntScGreyFabRcvFromPartyFabRcvController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();
                
        //console.log(prgListData);

        //if (favRcvListData['KNT_SCO_CHL_RCV_D_ID']) {
        //    $scope.$parent.form['fabRcvDtl'] = angular.copy(favRcvListData);
        //}
        //else {
        //    $scope.$parent.form['fabRcvDtl'] = [];
        //}

        //vm.fabItem = { KNT_QC_STS_TYPE_ID: 1, QC_STS_TYP_NAME: 'Passed', NOTE: '' };

        
        vm.printButtonList = [{ BTN_ID: 1, BTN_NAME: 'Challan' }, { BTN_ID: 2, BTN_NAME: 'Party Ledger' }];
        

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getQcStatusTypeList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }
        
        
        function getQcStatusTypeList() {
            vm.qcStatusTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,                
                dataTextField: "QC_STS_TYP_NAME",
                dataValueField: "KNT_QC_STS_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.fabItem.QC_STS_TYP_NAME = item.QC_STS_TYP_NAME;
                },
                dataBound: function (e) {
                    vm.fabItem.KNT_QC_STS_TYPE_ID = 1;
                    vm.fabItem.QC_STS_TYP_NAME = 'Passed';
                }
            };

            return vm.qcStatusTypeDS = new kendo.data.DataSource({
                transport: {
                    read: function (e) {                        
                        var url = '/api/knit/KntScChlnRcv/GetQcStatusTypeList?pKNT_QC_STS_TYPE_ID_LST=1,4,7';
                        
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        vm.onChangePrgSearch = function (prgSearch) {
            var url = '/api/Knit/KntScChlnRcv/GetScPrgIssList?&pSCM_SUPPLIER_ID=' + ($scope.$parent.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID || 0);

            if (prgSearch.length == 0) {
                return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                    $scope.$parent.form.prgIssDtl = res;
                });
            }
            else if (prgSearch.length > 0) {
                url = url + '&pSEARCH_STR=' + prgSearch;
                return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                    $scope.$parent.form.prgIssDtl = res;
                });
            }
        }

        vm.makeActive = function (items, $index) {

            $scope.$parent.selectedFabRow = angular.copy(items[$index]);
            vm.fabItem = angular.copy(items[$index]);
            vm.fabItem.RCV_NO_ROLL = null;
            vm.fabItem.RCV_ROLL_WT = null;
            vm.fabItem.KNT_QC_STS_TYPE_ID = 1;
            vm.fabItem.QC_STS_TYP_NAME = 'Passed';

            items[$index]['IS_ACTIVE'] = true; //!items[$index]['IS_ACTIVE'];
            //vm.fabItem.FAB_RCV_INDEX = items[$index].fabRcvDtl.length + 1;

            angular.forEach(items, function (val, key) {
                if ($index != key) {
                    val['IS_ACTIVE'] = false;
                }
            });

            console.log(items[$index]);
            //getByrWiseSmplTyleList(items[$index]);
            
        };

        
        vm.addFabRcv = function (fabItem) {
            $scope.$parent.form.fabRcvDtl.push({
                KNT_SCO_CHL_RCV_D_ID: 0, KNT_SCO_CHL_RCV_H_ID: $stateParams.pKNT_SCO_CHL_RCV_H_ID, KNT_STYL_FAB_ITEM_ID: $scope.$parent.selectedFabRow.KNT_STYL_FAB_ITEM_ID,
                STYLE_NO: $scope.$parent.selectedFabRow.STYLE_NO, ORDER_NO_LST: $scope.$parent.selectedFabRow.ORDER_NO_LST, COLOR_NAME_EN: $scope.$parent.selectedFabRow.COLOR_NAME_EN,
                FAB_TYPE_SNAME: $scope.$parent.selectedFabRow.FAB_TYPE_SNAME, FIN_GSM: $scope.$parent.selectedFabRow.FIN_GSM, ACT_FIN_DIA: $scope.$parent.selectedFabRow.ACT_FIN_DIA,
                MC_DIA: $scope.$parent.selectedFabRow.MC_DIA, YARN_DETAILS: $scope.$parent.selectedFabRow.YARN_DETAILS,
                RCV_NO_ROLL: vm.fabItem.RCV_NO_ROLL, RCV_ROLL_WT: vm.fabItem.RCV_ROLL_WT, WT_MOU_ID: 3,
                KNT_QC_STS_TYPE_ID: vm.fabItem.KNT_QC_STS_TYPE_ID, QC_STS_TYP_NAME: vm.fabItem.QC_STS_TYP_NAME, NOTE: vm.fabItem.NOTE
            });

            var data = angular.copy(vm.fabItem);
            //vm.fabItem = { KNT_QC_STS_TYPE_ID: data.KNT_QC_STS_TYPE_ID, QC_STS_TYP_NAME: data.QC_STS_TYP_NAME, NOTE: '' };
            vm.fabItem.RCV_NO_ROLL = null;
            vm.fabItem.RCV_ROLL_WT = null;
            vm.fabItem.KNT_QC_STS_TYPE_ID = data.KNT_QC_STS_TYPE_ID;
            vm.fabItem.QC_STS_TYP_NAME = data.QC_STS_TYP_NAME;
            vm.fabItem.NOTE = '';
        }

        
        vm.editFabric = function (index, dataList) {
            vm.fabItem = angular.copy(dataList[index]);
            vm.fabItem['fabRcvIndex'] = index;
            console.log(vm.fabItem);

            $scope.$parent.isFabAddToGrid = 'N';
        };

        vm.updateFabRcv = function (data) {
            var fabRcvFormCopy = angular.copy(data);
            
            console.log(fabRcvFormCopy);
            console.log($scope.$parent.form.fabRcvDtl);
            $scope.$parent.form.fabRcvDtl[data.fabRcvIndex] = fabRcvFormCopy;
            vm.fabItem.RCV_NO_ROLL = null;
            vm.fabItem.RCV_ROLL_WT = null;
            vm.fabItem.KNT_QC_STS_TYPE_ID = fabRcvFormCopy.KNT_QC_STS_TYPE_ID;
            vm.fabItem.QC_STS_TYP_NAME = fabRcvFormCopy.QC_STS_TYP_NAME;
            vm.fabItem.NOTE = '';
            
            $scope.$parent.isFabAddToGrid = 'Y';
        };

        vm.removeFabric = function (index, removeFrom) {
            removeFrom.splice(index, 1);
        };

        //vm.next = function () {
        //    $state.go('KntGreyFabRcv', {
        //        pPROD_DT: vm.form.RCV_DT == null ? $stateParams.pRCV_DT : moment(vm.form.RCV_DT).format("YYYY-MMM-DD")
        //    }, { reload: 'KntGreyFabRcv' });
        //    vm.IS_NEXT = 'Y';
        //    //vm.showSplash = 'N';
        //};



        //$scope.$watchGroup(['vm.form.RCV_DT'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            vm.IS_NEXT = 'N';

        //            //vm.next();
        //            vm.rollDelvGridDataSource.read();
        //        }
        //    }
        //}, true);

        //vm.onChangeProdDate = function () {
        //    return MrcDataService.GetAllOthers('/api/mrc/SampleReq/getSmplProdBatchListByDate?&pPROD_DT=' + $filter('date')(vm.form.PROD_DT, vm.dtFormat)).then(function (res) {
        //        vm.batchList = res;
        //    }, function (err) {
        //        console.log(err);
        //    });
        //}


        


    }

})();
////////// End Fabric Receive Controller





////////// Start Yarn Receive Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScGreyFabRcvFromPartyYrnRcvController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntScGreyFabRcvFromPartyYrnRcvController]);
    function KntScGreyFabRcvFromPartyYrnRcvController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();

        //if (yrnRcvListData['KNT_SCO_CHL_YRN_RET_D_ID']) {
        //    $scope.$parent.form['yrnRcvDtl'] = angular.copy(yrnRcvListData);
        //}
        //else {
        //    $scope.$parent.form['yrnRcvDtl'] = [];
        //}

        //vm.printButtonList = [{ BTN_ID: 1, BTN_NAME: 'Challan' }, { BTN_ID: 2, BTN_NAME: 'Party Ledger' }];
        vm.yesNoList = [{ LST_ID: 'Y', LST_NAME: 'Yes' }, { LST_ID: 'N', LST_NAME: 'No' }];
        vm.isYrnAddToGrid = 'Y';

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        vm.errstyle = { 'border': '1px solid #f13d3d' };
        vm.yrnItem = { KNT_SCO_CHL_YRN_RET_D_ID: 0, MC_FAB_PROD_ORD_H_ID: 0, PACK_MOU_ID: 29, PACK_MOU_CODE: 'Bag', QTY_MOU_ID: 3, QTY_MOU_CODE: 'Kg', IS_TRANSFER: 'N', SP_NOTE: '' };



        activate();
        function activate() {
            var promise = [getYarnItemList(), getStyleExtList(null, null, null, null), getReturnTypeList(), getReturnCozTypeList(), getMOUList(), getYesNoList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }


        function getStyleExtList(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
            vm.StyleExtDsOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_FAB_PROD_ORD_H_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.yrnItem.STYLE_NO = item.STYLE_NO + '(' + item.ORDER_NO + ')';

                    return vm.yarnItemDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                KnittingDataService.getDataByFullUrl('/api/knit/KntScChlnRcv/GetYarnListByProdOrdHdr?&pMC_FAB_PROD_ORD_H_ID=' + item.MC_FAB_PROD_ORD_H_ID).then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    });
                }
            };

            vm.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/knit/KntScChlnRcv/GetScoOrdStyleListByHasKnitCard?pSCM_SUPPLIER_ID=" + ($scope.$parent.form.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID); //"/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                        //url += "&pRF_FAB_PROD_CAT_ID=" + pRF_FAB_PROD_CAT_ID;
                        //url += "&pNATURE_OF_ORDER=POLO";
                        //url += "&pFIRSTDATE=" + pFIRSTDATE;
                        //url += "&pLASTDATE=" + pLASTDATE;

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pSTYLE_NO';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + ($stateParams.MC_FAB_PROD_ORD_H_ID || null);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };
        
        function getYarnItemList() {
            vm.yarnItemOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "YARN_DETAIL",
                dataValueField: "YARN_ITEM_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.yrnItem.KNT_YRN_LOT_ID = item.KNT_YRN_LOT_ID;
                    vm.yrnItem.YARN_DETAIL = item.YARN_DETAIL;
                }
            };
        };

        function getReturnTypeList() {
            vm.returnTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    
                    vm.yrnItem.RETURN_TYPE_DESC = item.LK_DATA_NAME_EN;
                }
            };

            return vm.returnTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.LookupListData(108).then(function (res) {
                            e.success(res);
                        });                       
                    }
                }
            });
        };

        function getReturnCozTypeList() {
            vm.cozTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.yrnItem.RETURN_COZ_DESC = item.LK_DATA_NAME_EN;
                }
            };

            return vm.cozTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.LookupListData(109).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getMOUList() {
            vm.packMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.yrnItem.PACK_MOU_CODE = item.MOU_CODE;
                },
                dataBound: function (e) {
                    vm.yrnItem.PACK_MOU_ID = 29;
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
                    vm.yrnItem.QTY_MOU_CODE = item.MOU_CODE;
                }
            };

            return KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                //e.success(res);
                vm.packMouDataSource = new kendo.data.DataSource({
                    data: res
                });

                vm.qtyMouDataSource = new kendo.data.DataSource({
                    data: res
                });

            }, function (err) {
                console.log(err);
            });
            
        };

        function getYesNoList() {
            vm.yesNoOption = {                
                autoBind: true,
                dataTextField: "LST_NAME",
                dataValueField: "LST_ID",
                dataBound: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.yrnItem.IS_TRANSFER = 'N';
                }
            };

            return vm.yesNoDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        e.success(vm.yesNoList);
                    }
                }
            });
        };

        vm.resetYarnItem = function () {
            vm.yrnItem.KNT_SCO_CHL_YRN_RET_D_ID = 0;            
            vm.yrnItem.PACK_RET_QTY = null; 
            vm.yrnItem.QTY_PER_PACK = null;
            vm.yrnItem.LOOSE_QTY = null;
            vm.yrnItem.SP_NOTE = '';
        }

        vm.addYarnRcv = function (data) {
            $scope.$parent.form.yrnRcvDtl.push(angular.copy(data));
            vm.resetYarnItem();

            vm.isYrnAddToGrid = 'Y';
        }

        vm.editYarnRcv = function (index, dataList) {
            vm.yrnItem = angular.copy(dataList[index]);
            vm.yrnItem['yrnRcvIndex'] = index;
            console.log(vm.yrnItem);

            vm.isYrnAddToGrid = 'N';
            return vm.yarnItemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/KntScChlnRcv/GetYarnListByProdOrdHdr?&pMC_FAB_PROD_ORD_H_ID=' + vm.yrnItem.MC_FAB_PROD_ORD_H_ID).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
            
        };

        vm.updateYarnRcv = function (data) {
            var yrnRcvFormCopy = angular.copy(data);

            console.log(yrnRcvFormCopy);
            console.log($scope.$parent.form.yrnRcvDtl);
            $scope.$parent.form.yrnRcvDtl[data.yrnRcvIndex] = yrnRcvFormCopy;
            vm.resetYarnItem();

            vm.isYrnAddToGrid = 'Y';
        };

        vm.removeYarnRcv = function (index, removeFrom) {
            removeFrom.splice(index, 1);
        };

        vm.TotalReceiveQty = function (data) {
            if (data.PACK_RET_QTY > 0 && data.QTY_PER_PACK > 0)
                data.RET_QTY = (data.PACK_RET_QTY * data.QTY_PER_PACK)+(data.LOOSE_QTY||0);
            else
                data.RET_QTY = (data.LOOSE_QTY || 0);
        };

    }

})();
////////// End Yarn Receive Controller





////////// Start Collar Cuff Receive Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScGreyFabRcvFromPartyClcfRcvController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'formData', 'Dialog', 'access_token', KntScGreyFabRcvFromPartyClcfRcvController]);
    function KntScGreyFabRcvFromPartyClcfRcvController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, formData, Dialog, access_token) {

        var vm = this;
        vm.showSplash = true;
        $scope.errors = null;
        vm.Title = $state.current.Title || '';
        $scope.token = config.acc
        //vm.dtFormat = config.appDateFormat;
        //vm.today = new Date();
        //var key = 'KNT_CLCF_STR_RCV_H_ID';

        //vm.form = formData[key] ? formData : {
        //    KNT_CLCF_STR_RCV_H_ID: 0, RCV_DT: moment(vm.today).format("DD-MMM-YYYY"), CHALAN_DT: moment(vm.today).format("DD-MMM-YYYY"),
        //    USER_LEVEL: "", LK_CLCF_MVM_STS_ID: 570, SUP_TRD_NAME_EN: ""
        //};
        vm.formItem = { MC_BYR_ACC_GRP_ID: 0, MC_STYLE_H_EXT_ID: 0, MC_COLOR_ID: 0 };

        
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
                { field: "SCO_PRG_NO", title: "S/C Prog#", width: "10%" },
                { field: "FAB_PROD_CAT_SNAME", title: "Type", width: "8%", filterable: false },
                { field: "BYR_ACC_GRP_SNAME", title: "Buyer A/C", width: "10%", filterable: false },
                { field: "WORK_STYLE_NO", title: "Job#", width: "25%" },
                { field: "MC_ORDER_NO_LST", title: "Order#", width: "25%" },
                { field: "COLOR_NAME_EN", title: "Color", width: "15%" },
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

            //console.log(grid);

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

        //vm.getOrderByBuyerAC = function (pMC_BYR_ACC_GRP_ID) {

        //    return vm.orderDataSource = new kendo.data.DataSource({
        //        serverFiltering: true,
        //        transport: {
        //            read: function (e) {
        //                var webapi = new kendo.data.transports.webapi({});
        //                var params = webapi.parameterMap(e.data);
        //                var url = '/api/knit/KntCollarCuff/GetCollarCuffOrdReq?pMC_BYR_ACC_GRP_ID=' + pMC_BYR_ACC_GRP_ID;

        //                url += '&pageNumber=' + 1 + '&pageSize=' + 10;
        //                url += KnittingDataService.kFilterStr2QueryParam(params.filter);

        //                var paramsData = params.filter.replace(/'/g, '').split('~');
        //                console.log(paramsData[2]);

        //                return KnittingDataService.getDataByFullUrl(url).then(function (res) {
        //                    e.success(res.data);
        //                }, function (err) {
        //                    console.log(err);
        //                });
        //            }
        //        }
        //    });
        //};

        //vm.getOrderColor = function (pMC_ORDER_H_ID_LST) {
        //    return vm.orderColorDataSource = new kendo.data.DataSource({
        //        transport: {
        //            read: function (e) {
        //                var url = '/api/mrc/Order/MultiOrderWiseColorList/' + pMC_ORDER_H_ID_LST;

        //                return KnittingDataService.getDataByFullUrl(url).then(function (res) {
        //                    e.success(res);
        //                }, function (err) {
        //                    console.log(err);
        //                });
        //            }
        //        }
        //    });
        //};


        //$scope.$watchGroup(['vm.formItem.MC_BYR_ACC_ID', 'vm.formItem.MC_STYLE_H_EXT_ID', 'vm.formItem.MC_COLOR_ID'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            vm.IS_NEXT = 'N';
        //        }
        //    }
        //}, true);




        //vm.nextLoad = function () {
        //    vm.IS_NEXT = 'Y';
           
        //    vm.clcfRcvGridDataSource.read();
        //};

        vm.onChangeRcvQty = function (dataItem, ctrl) {

            var balQty = parseInt(dataItem.PRD_QTY) - parseInt(dataItem.DELV_QTY);
            console.log(dataItem);

            if (balQty < 0) {
                ctrl.$setValidity("max", false);
            }
            else {
                ctrl.$setValidity("max", true);
            }
        };

        vm.clcfRcvGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {

                    var url = '/api/knit/KntCollarCuff/GetClcf4StrRcvByScoProg?pLK_CLCF_CHL_TYP_ID=560&pRCV_DT=' + $scope.$parent.form.RCV_DT + '&pSCM_STORE_ID=' + $scope.$parent.form.SCM_STORE_ID + '&pMC_FAB_PROD_ORD_H_ID=' + vm.formItem.MC_FAB_PROD_ORD_H_ID + '&pMC_STYLE_H_EXT_ID=' + vm.formItem.MC_STYLE_H_EXT_ID + '&pMC_COLOR_ID=' + vm.formItem.MC_COLOR_ID + '&pKNT_SCO_CLCF_PRG_H_ID=' + vm.formItem.KNT_SCO_CLCF_PRG_H_ID;

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
                { field: "MC_ORDER_NO_LST", title: "Order#", width: "18%" },
                //{ field: "STYLE_NO", title: "Style#", width: "10%" },
                { field: "COLOR_NAME_EN", title: "Color", width: "12%" },
                { field: "GARM_PART_NAME", title: "Part", width: "8%" },
                //{ field: "SIZE_CODE", title: "Size", width: "7%" },
                { field: "KNT_YRN_LOT_LST", title: "Yarn", width: "30%", filterable: false },
                { field: "MESUREMENT", title: "Meas", width: "7%", filterable: false },
                //{ field: "PRD_QTY", title: "Prod.Qty", width: "6%", filterable: false },
                {
                    title: "Rcv.Qty",
                    field: "DELV_QTY",
                    //footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmRcvQty'><input type='number' class='form-control' name='DELV_QTY' ng-model='dataItem.DELV_QTY' min='0' ng-style='(frmRcvQty.DELV_QTY.$error.min)? vm.errstyle:\"\"'  /></ng-form>";
                    },
                    width: "10%",
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
                    width: "10%",
                    filterable: false
                }//,
                //{
                //    title: "Pass.Qty",
                //    field: "PASS_QTY",                   
                //    template: function () {
                //        return "<ng-form name='frmPassQty'><input type='number' class='form-control' name='PASS_QTY' ng-model='dataItem.PASS_QTY' min='0' ng-style='frmPassQty.PASS_QTY.$error.min||frmPassQty.PASS_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                //    },
                //    width: "8%",
                //    filterable: false
                //},
                //{
                //    title: "Rej.Qty",
                //    field: "REJ_QTY",                    
                //    template: function () {
                //        return "<ng-form name='frmRejQty'><input type='number' class='form-control' name='REJ_QTY' ng-model='dataItem.REJ_QTY' min='0' ng-style='frmRejQty.REJ_QTY.$error.min||frmRejQty.REJ_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                //    },
                //    width: "8%",
                //    filterable: false
                //},
                //{
                //    title: "Hold.Qty",
                //    field: "HOLD_QTY",                    
                //    template: function () {
                //        return "<ng-form name='frmHoldQty'><input type='number' class='form-control' name='HOLD_QTY' ng-model='dataItem.HOLD_QTY' min='0' ng-style='frmHoldQty.HOLD_QTY.$error.min||frmHoldQty.HOLD_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                //    },
                //    width: "6%",
                //    filterable: false
                //}//,
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
            var chlnDtlDataList = $scope.$parent.clcfChlnRcvGridDataSource.data();

            angular.forEach(dataList, function (ob) {

                var data = [];
                data = _.filter(chlnDtlDataList, function (o) {
                    if (o.KNT_CLCF_STYL_ITEM_ID == ob.KNT_CLCF_STYL_ITEM_ID) {
                        return o;
                    }
                });

                if (ob.DELV_QTY > 0 && data.length<1) {
                    $scope.$parent.clcfChlnRcvGridDataSource.insert(ob, 0);
                }
            });

            vm.formItem.MC_COLOR_ID = 0;
            //vm.orderColorDataSource.read();
            vm.clcfRcvGridDataSource.read();
        }

        
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
                { field: "MC_ORDER_NO_LST", title: "Order#", width: "18%", footerTemplate: "Total Record: #=count#" },
                //{ field: "STYLE_NO", title: "Style#", width: "10%" },
                { field: "COLOR_NAME_EN", title: "Color", width: "12%" },
                { field: "GARM_PART_NAME", title: "Part", width: "8%" },
                //{ field: "SIZE_CODE", title: "Size", width: "7%" },
                { field: "KNT_YRN_LOT_LST", title: "Yarn", width: "30%", filterable: false },
                { field: "MESUREMENT", title: "Meas", width: "7%", filterable: false },
                //{ field: "PRD_QTY", title: "Prod. Qty", width: "6%", filterable: false },
                {
                    title: "Rcv.Qty",
                    field: "DELV_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmChlnRcvQty'><input type='number' class='form-control' name='DELV_QTY' ng-model='dataItem.DELV_QTY' min='0' ng-change='vm.onChangeRcvQty(dataItem, frmChlnRcvQty.DELV_QTY)' ng-style='frmChlnRcvQty.DELV_QTY.$error.min||frmChlnRcvQty.DELV_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='dataItem.LK_CLCF_MVM_STS_ID>570' /></ng-form>";
                    },
                    width: "10%",
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
                    width: "10",
                    filterable: false
                }//,
                //{
                //    title: "Pass.Qty",
                //    field: "PASS_QTY",
                //    footerTemplate: "#=sum#",
                //    template: function () {
                //        return "<ng-form name='frmChlnPassQty'><input type='number' class='form-control' name='PASS_QTY' ng-model='dataItem.PASS_QTY' min='0' ng-change='vm.onChangeQcQty(dataItem, frmChlnPassQty.PASS_QTY, frmChlnRejQty.REJ_QTY, frmChlnHoldQty.HOLD_QTY)' ng-style='frmChlnPassQty.PASS_QTY.$error.min||frmChlnPassQty.PASS_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                //    },
                //    width: "8%",
                //    filterable: false
                //},
                //{
                //    title: "Rej.Qty",
                //    field: "REJ_QTY",
                //    footerTemplate: "#=sum#",
                //    template: function () {
                //        return "<ng-form name='frmChlnRejQty'><input type='number' class='form-control' name='REJ_QTY' ng-model='dataItem.REJ_QTY' min='0' ng-change='vm.onChangeQcQty(dataItem, frmChlnPassQty.PASS_QTY, frmChlnRejQty.REJ_QTY, frmChlnHoldQty.HOLD_QTY)' ng-style='frmChlnRejQty.REJ_QTY.$error.min||frmChlnRejQty.REJ_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                //    },
                //    width: "6%",
                //    filterable: false
                //},
                //{
                //    title: "Hold.Qty",
                //    field: "HOLD_QTY",
                //    footerTemplate: "#=sum#",
                //    template: function () {
                //        return "<ng-form name='frmChlnHoldQty'><input type='number' class='form-control' name='HOLD_QTY' ng-model='dataItem.HOLD_QTY' min='0' ng-change='vm.onChangeQcQty(dataItem, frmChlnPassQty.PASS_QTY, frmChlnRejQty.REJ_QTY, frmChlnHoldQty.HOLD_QTY)' ng-style='frmChlnHoldQty.HOLD_QTY.$error.min||frmChlnHoldQty.HOLD_QTY.$error.max? vm.errstyle:\"\"' ng-disabled='(dataItem.LK_CLCF_MVM_STS_ID>571 || vm.form.USER_LEVEL==\"CREATOR\" || vm.form.USER_LEVEL==\"\")' /></ng-form>";
                //    },
                //    width: "6%",
                //    filterable: false
                //}//,
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
                        KNT_CLCF_STR_RCV_D_ID: ob.KNT_CLCF_STR_RCV_D_ID, KNT_CLCF_STYL_ITEM_ID: ob.KNT_CLCF_STYL_ITEM_ID, MC_CLCF_ORD_REQ_ID: ob.MC_CLCF_ORD_REQ_ID,
                        DELV_QTY: ob.DELV_QTY, DELV_NO_ROLL: ob.DELV_NO_ROLL, DELV_ROLL_WT: ob.DELV_ROLL_WT, WT_MOU_ID: ob.WT_MOU_ID,
                        PASS_QTY: ob.PASS_QTY, REJ_QTY: ob.REJ_QTY, HOLD_QTY: ob.HOLD_QTY, ADDL_QTY: ob.ADDL_QTY, RCV_QTY: ob.RCV_QTY
                    };
                }));

                console.log(submitRcvData);
                //return;

                return $http({
                    headers: { 'Authorization': 'Bearer ' + access_token },
                    url: '/api/Knit/KntCollarCuff/BatchSaveCollarCuffRcv',
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
                            vm.form.KNT_CLCF_STR_RCV_H_ID = res['data'].PKNT_CLCF_STR_RCV_H_ID_RTN;
                            vm.form.USER_LEVEL = res['data'].PUSER_LEVEL_RTN;
                            vm.form.CHALAN_NO = res['data'].PCHALAN_NO_RTN;

                            $stateParams.pKNT_CLCF_STR_RCV_H_ID = res['data'].PKNT_CLCF_STR_RCV_H_ID_RTN;
                            $state.go('KntCollarCuffStrRcvH', { pKNT_CLCF_STR_RCV_H_ID: res['data'].PKNT_CLCF_STR_RCV_H_ID_RTN }, { notify: false });

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
            return $state.go('KntCollarCuffStrRcvH', { pKNT_CLCF_STR_RCV_H_ID: 0 }, { reload: 'KntCollarCuffStrRcvH' });
        }

        vm.backToList = function () {
            $state.go('KntCollarCuffStrRcvList');
        }

        vm.submitChallan = function () {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to submit?';

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                console.log(submitRcvData);
                //return;

                return $http({
                    headers: { 'Authorization': 'Bearer ' + access_token },
                    url: '/api/Knit/KntCollarCuff/SubmitClcfChlnRcv',
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
                            //vm.form.KNT_CLCF_STR_RCV_H_ID = res['data'].PKNT_CLCF_STR_RCV_H_ID_RTN;
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
                    url: '/api/Knit/KntCollarCuff/FinalizeClcfChlnRcv',
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
////////// End Collar Cuff Receive Controller





////////// Start List Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScGreyFabRcvFromPartyListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntScGreyFabRcvFromPartyListController]);
    function KntScGreyFabRcvFromPartyListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'KNT_SC_GFAB_DLV_H_ID';
        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.today = new Date();
        vm.form = { KNT_SCO_CHL_RCV_H_ID: 0, RCV_DT: moment(vm.today).format("DD-MMM-YYYY"), prgIssDtl: [], yrnRcvDtl: [] }; //formData[key] ? formData : { KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', IS_FINALIZE: 'N' };
        
        
        activate();
        function activate() {
            var promise = [getSupplierList(), /*getScProgList(), getStoreList()*/];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.scRcvDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.scRcvDateOpened = true;
        };

        vm.chlnDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.chlnDateOpened = true;
        };

        function getStoreList() {
            vm.storeOption = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                //dataSource: {
                //    transport: {
                //        read: function (e) {
                //            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                //                //KnittingDataService.LookupListData(92).then(function (res) {
                //                e.success(res);
                //            });
                //        }
                //    }
                //},
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);                    
                //},
                //dataBound: function (e) {
                //    if (formData[key]) {
                //        vm.form.SCM_STORE_ID = formData.SCM_STORE_ID;
                //    }
                //}
            };

            return vm.storeDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5';
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
                    vm.form.KNT_SC_PRG_RCV_ID = null;

                    //vm.scProgDataSource.read();
                },
                dataBound: function (e) {
                    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                        vm.getPartyChallanList();
                    }
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

        function getScProgList() {
            vm.scProgOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "PRG_RCV_NO",
                dataValueField: "KNT_SC_PRG_RCV_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    //vm.form.SUP_TRD_NAME_EN = item.SUP_TRD_NAME_EN;
                    //vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    vm.form.SC_PRG_DT = item.SC_PRG_DT;
                }//,
                //dataBound: function (e) {
                //    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                //        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                //    }
                //}
            };

            return vm.scProgDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScRcv/GetScRcvProgramList?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID;
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

        vm.getPartyChallanList = function () {

            vm.partyChallanDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total",
                    model: {
                        fields: {
                            RCV_DT: { type: "date" },
                            CHALAN_DT: { type: "date" }
                        }
                    }
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScChlnRcv/GetRcvChallanList?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID;
                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        console.log(params.filter);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            //angular.forEach(res.data, function (val, key) {
                            //    val['BLK_FAB_REQ_DT_STR'] = $filter('date')(val['BLK_FAB_REQ_DT'], vm.dtFormat);
                            //});
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
                //group: [{ field: 'STYLE_NO' }],
                sort: [{ field: 'RCV_DT', dir: 'desc' }]
            });
        };

        vm.partyChallanOptions = {
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
            //dataBound: function (e) {
            //    collapseAllGroups(this);
            //},
            columns: [
                { field: "RCV_DT", title: "Rcv. Date", type: "date", format: "{0:dd-MMM-yyyy}" },
                { field: "SUP_TRD_NAME_EN", title: "Party", type: "string" },
                { field: "CHALAN_NO", title: "Challan#", type: "string" },
                { field: "CHALAN_DT", title: "Challan Date", type: "date", format: "{0:dd-MMM-yyyy}" },
                { field: "BYR_ACC_NAME_EN", title: "B/A Name", type: "string", hidden: true },
                { field: "STYLE_NO", title: "Style#", type: "string", hidden: true },                                
                { field: "ORDER_NO", title: "Order#", type: "string" },                
                { field: "REMARKS", title: "Remarks", type: "string", hidden: true },
                {
                    title: "Action",
                    template: function () {                        
                        return "<a class='btn btn-xs blue' ui-sref='ScGreyFabRcvFormPartyH.FabRcv({pKNT_SCO_CHL_RCV_H_ID:dataItem.KNT_SCO_CHL_RCV_H_ID, pSCM_SUPPLIER_ID:dataItem.SCM_SUPPLIER_ID })' ><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "250px"
                }              
            ]
        };

        

        vm.newChallan = function () {
            $state.go('ScGreyFabRcvFormPartyH.FabRcv', { pKNT_SCO_CHL_RCV_H_ID: 0, pSCM_SUPPLIER_ID: vm.form.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID });
            
        }

    }

})();
////////// End List Controller