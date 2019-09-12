////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSciGreyFabRtnFromPartyHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'formData', 'formSciRejFabList', 'KnittingDataService', 'Dialog', KntSciGreyFabRtnFromPartyHController]);
    function KntSciGreyFabRtnFromPartyHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, formData, formSciRejFabList, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        formData['rejFabDtl'] = formSciRejFabList;
        var key = 'KNT_SCI_GFAB_RET_H_ID';
        
        console.log(formData);
        console.log($stateParams);
        console.log(formData['KNT_SCI_GFAB_RET_H_ID']);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.today = new Date();
        vm.form = formData[key] ? formData : { KNT_SCI_GFAB_RET_H_ID: 0, RET_CHALAN_DT: moment(vm.today).format("DD-MMM-YYYY"), rejFabDtl: [], fabRtnDtl: [], yrnRcvDtl: [] }; //formData[key] ? formData : { KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', IS_FINALIZE: 'N' };
        $scope.form = formData[key] ? formData : { KNT_SCI_GFAB_RET_H_ID: 0, RET_CHALAN_DT: moment(vm.today).format("DD-MMM-YYYY"), rejFabDtl: [], fabRtnDtl: [], yrnRcvDtl: [] }; //formData[key] ? formData : { KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', IS_FINALIZE: 'N' };

        //vm.IS_NEXT = 'N';
        $scope.isFabAddToGrid = 'Y';
        $scope.selectedFabRow = {};
           
        vm.fabRcvState = ($state.current.name === 'SciGreyFabRtnFromPartyH.FabricReturn') ? true : false;
        //vm.yrnRcvState = ($state.current.name === 'ScGreyFabRcvFormPartyH.YrnRcv') ? true : false;

        
        
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
            //    vm.form.rejFabDtl = res;
            //    //$scope.form.rejFabDtl = res;

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

                    vm.form.rejFabDtl = [];
                    vm.form.yrnRcvDtl = [];
                    vm.IS_NEXT = 'N';

                    //vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    //vm.form.KNT_SC_PRG_RCV_ID = null;

                    //vm.scProgDataSource.read();
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
        //            vm.form.rejFabDtl = [];
        //            vm.form.yrnRcvDtl = [];
        //            vm.IS_NEXT = 'N';                    
        //        }
        //    }
        //}, true);

        vm.gotoNext = function () {
            
            vm.fabRcvState = true;
            vm.IS_NEXT = 'Y';
            $state.go('SciGreyFabRtnFromPartyH.FabricReturn', { pKNT_SCI_GFAB_RET_H_ID: 0, pSCM_SUPPLIER_ID: vm.form.SCM_SUPPLIER_ID }, { notify: false });
            
            return KnittingDataService.getDataByFullUrl('/api/Knit/KntSciChlnRtn/GetSciQcRejFabList4Rtn?&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0)).then(function (res) {
                 vm.form.rejFabDtl = res;                                 
             });                           
        }

        vm.batchSave = function (pIS_FINALIZED) {
            var submitRcvData = angular.copy($scope.form);
            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZED == 'Y') {
                submitRcvData['IS_FINALIZED'] = pIS_FINALIZED;
                vMsg = 'After finalize, the challan is lock.</br></br> <b>Do you want to save and finalize?</b>';
            }


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                //submitRcvData['IS_FINALIZE'] = pIS_FINALIZE;
                submitRcvData['RET_CHALAN_DT'] = $filter('date')(submitRcvData['RET_CHALAN_DT'], vm.dtFormat);
                submitRcvData['CHALAN_DT'] = $filter('date')(submitRcvData['CHALAN_DT'], vm.dtFormat);

                //var fabRtnDtl = [];
                //angular.forEach(submitRcvData.rejFabDtl, function (val, key) {
                //    angular.forEach(val['fabRtnDtl'], function (val1, key1) {
                //        fabRtnDtl.push(val1);
                //    });
                //});
                //console.log(fabRtnDtl);


                submitRcvData.FAB_RTN_DTL_XML = KnittingDataService.xmlStringShort(submitRcvData['fabRtnDtl'].map(function (ob) {                    
                    return {
                        KNT_SCI_GFAB_RET_D_ID: ob.KNT_SCI_GFAB_RET_D_ID, KNT_SCI_GFAB_RET_H_ID: ob.KNT_SCI_GFAB_RET_H_ID, KNT_STYL_FAB_ITEM_ID: ob.KNT_STYL_FAB_ITEM_ID,
                        FAB_RTN_INDEX: ob.FAB_RTN_INDEX, RET_ROLL_QTY: ob.RET_ROLL_QTY, RET_GFAB_QTY: ob.RET_GFAB_QTY, QTY_MOU_ID: ob.QTY_MOU_ID, NOTE: ob.NOTE
                    };
                }));

                
                //submitRcvData.YRN_RCV_DTL_XML = KnittingDataService.xmlStringShort(submitRcvData['yrnRcvDtl'].map(function (ob) {
                //    return ob;
                //}));

                console.log(submitRcvData);
                //return;

                return KnittingDataService.saveDataByFullUrl(submitRcvData, '/api/knit/KntScChlnRtn/BatchSave4SciFabRtn').then(function (res) {
                    console.log(res);

                    vm.errors = undefined;
                    if (res.success === false) {
                        vm.errors = [];
                        vm.errors = res.errors;
                    }
                    else {

                        console.log(res);

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //$scope.fabOrderGridDataSource.read();
                            //vm.getChallanList();
                            if (res.data.PKNT_SCI_GFAB_RET_H_ID_RTN != 0) {
                                $stateParams.pKNT_SCI_GFAB_RET_H_ID = res.data.PKNT_SCI_GFAB_RET_H_ID_RTN;
                                $scope.form.KNT_SCI_GFAB_RET_H_ID = res.data.PKNT_SCI_GFAB_RET_H_ID_RTN;

                                if (pIS_FINALIZED == 'Y') {
                                    vm.form.IS_FINALIZED = pIS_FINALIZED;
                                    $scope.form.IS_FINALIZED = pIS_FINALIZED;
                                }
                                
                                $state.go('SciGreyFabRtnFromPartyH.FabricReturn', { pKNT_SCI_GFAB_RET_H_ID: res.data.PKNT_SCI_GFAB_RET_H_ID_RTN, pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID }, { reload: 'SciGreyFabRtnFromPartyH.FabricReturn' });

                                //if (vm.yarnReceivState == true) {
                                //    $state.go('KntSubContProgH.YarnReceive', { pKNT_SCO_CHL_RCV_H_ID: res.data.PKNT_SCO_CHL_RCV_H_ID_RTN }, { reload: 'ScGreyFabRcvFormPartyH.FabRcv' });
                                //}
                                //else {
                                //    $state.go('KntSubContProgH.FabColorKP', { pKNT_SC_PRG_RCV_ID: res.data.PKNT_SC_PRG_RCV_ID_RTN }, { notify: false });

                                //    $scope.fabOrderGridDataSource.read();
                                //}

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
            $state.go('SciGreyFabRtnFromPartyH.FabricReturn', { pKNT_SCI_GFAB_RET_H_ID: 0, pSCM_SUPPLIER_ID: $scope.form.SCM_SUPPLIER_ID });

            $scope.form = { KNT_SCI_CHL_RCV_H_ID: 0, RCV_DT: moment(vm.today).format("DD-MMM-YYYY"), rejFabDtl: [], fabRtnDtl: [], yrnRcvDtl: [] };
        }

        vm.backToList = function () {
            return $state.go('SciGreyFabRtnFromPartyList', { pSCM_SUPPLIER_ID: $scope.form.SCM_SUPPLIER_ID });
        };

        vm.printChallan = function () {
            var rptCode = 'RPT-3520';
           
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





////////// Start Fabric Return Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSciGreyFabRtnFromPartyFabRtnController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntSciGreyFabRtnFromPartyFabRtnController]);
    function KntSciGreyFabRtnFromPartyFabRtnController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();
                
        //console.log(prgListData);
        //console.log(favRcvListData);

        //if (favRcvListData['KNT_SCO_CHL_RCV_D_ID']) {
        //    alert('x');
        //    $scope.$parent.form['fabRtnDtl'] = angular.copy(favRcvListData);
        //}
        //else {
        //    $scope.$parent.form['fabRtnDtl'] = [];
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
            var promise = [];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }
        
        vm.onChangePrgSearch = function (prgSearch) {
            var url = '/api/Knit/KntSciChlnRtn/GetSciQcRejFabList4Rtn?&pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || $scope.$parent.SCM_SUPPLIER_ID || 0) //'/api/Knit/KntScChlnRcv/GetScPrgIssList?&pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || $scope.$parent.SCM_SUPPLIER_ID || 0);

            if (prgSearch.length == 0) {
                return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                    $scope.$parent.form['rejFabDtl'] = res;
                });
            }
            else if (prgSearch.length > 0) {
                url = url + '&pSEARCH_STR=' + prgSearch;
                console.log(url);

                return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                    $scope.$parent.form['rejFabDtl'] = res;

                    console.log(res);
                });
            }
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
                        var url = '/api/knit/KntScChlnRcv/GetQcStatusTypeList?pKNT_QC_STS_TYPE_ID_LST=1,4';
                        
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        vm.makeActive = function (items, $index) {

            $scope.$parent.selectedFabRow = angular.copy(items[$index]);
            vm.fabItem = angular.copy(items[$index]);
            vm.fabItem.RCV_NO_ROLL = null;
            vm.fabItem.RET_GFAB_QTY = null;            
            
            items[$index]['IS_ACTIVE'] = true; //!items[$index]['IS_ACTIVE'];
            //vm.fabItem.FAB_RCV_INDEX = items[$index].fabRtnDtl.length + 1;

            angular.forEach(items, function (val, key) {
                if ($index != key) {
                    val['IS_ACTIVE'] = false;
                }
            });

            console.log(items[$index]);
            //getByrWiseSmplTyleList(items[$index]);
            
        };

        
        vm.addFabRcv = function (fabItem) {
            $scope.$parent.form.fabRtnDtl.push({
                KNT_SCI_GFAB_RET_D_ID: 0, KNT_SCI_GFAB_RET_H_ID: $stateParams.pKNT_SCI_GFAB_RET_H_ID, KNT_STYL_FAB_ITEM_ID: $scope.$parent.selectedFabRow.KNT_STYL_FAB_ITEM_ID,
                STYLE_NO: $scope.$parent.selectedFabRow.STYLE_NO, ORDER_NO_LST: $scope.$parent.selectedFabRow.ORDER_NO_LST, COLOR_NAME_EN: $scope.$parent.selectedFabRow.COLOR_NAME_EN,
                FAB_TYPE_SNAME: $scope.$parent.selectedFabRow.FAB_TYPE_SNAME, FIN_GSM: $scope.$parent.selectedFabRow.FIN_GSM, ACT_FIN_DIA: $scope.$parent.selectedFabRow.ACT_FIN_DIA,
                MC_DIA: $scope.$parent.selectedFabRow.MC_DIA, YARN_DETAILS: $scope.$parent.selectedFabRow.YARN_DETAILS,
                RET_ROLL_QTY: vm.fabItem.RET_ROLL_QTY, RET_GFAB_QTY: vm.fabItem.RET_GFAB_QTY, QTY_MOU_ID: 3, NOTE: vm.fabItem.NOTE
            });

            var data = angular.copy(vm.fabItem);
            //vm.fabItem = { KNT_QC_STS_TYPE_ID: data.KNT_QC_STS_TYPE_ID, QC_STS_TYP_NAME: data.QC_STS_TYP_NAME, NOTE: '' };
            
            vm.fabItem.RET_ROLL_QTY = null;
            vm.fabItem.RET_GFAB_QTY = null;     
            vm.fabItem.NOTE = '';
        }

        
        vm.editFabric = function (index, dataList) {
            vm.fabItem = angular.copy(dataList[index]);
            vm.fabItem['FAB_RTN_INDEX'] = index;
            console.log(vm.fabItem);

            $scope.$parent.isFabAddToGrid = 'N';
        };

        vm.updateFabRcv = function (data) {
            var fabRcvFormCopy = angular.copy(data);
            
            console.log(fabRcvFormCopy);
            console.log($scope.$parent.form.fabRtnDtl);
            $scope.$parent.form.fabRtnDtl[data.FAB_RTN_INDEX] = fabRcvFormCopy;
            
            vm.fabItem.RET_ROLL_QTY = null;
            vm.fabItem.RET_GFAB_QTY = null;
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
////////// End Fabric Return Controller




////////// Start List Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSciGreyFabRtnFromPartyListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntSciGreyFabRtnFromPartyListController]);
    function KntSciGreyFabRtnFromPartyListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'KNT_SCI_GFAB_RET_H_ID';
        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.today = new Date();
        vm.form = { KNT_SCI_GFAB_RET_H_ID: 0, RET_CHALAN_DT: moment(vm.today).format("DD-MMM-YYYY"), rejFabDtl: [], yrnRcvDtl: [] }; //formData[key] ? formData : { KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', IS_FINALIZE: 'N' };
        
        
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
                            RET_CHALAN_DT: { type: "date" }
                        }
                    }
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScChlnRtn/GetSciRtnChallanList?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID;
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
                sort: [{ field: 'RET_CHALAN_DT', dir: 'desc' }]
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
                { field: "RET_CHALAN_NO", title: "Challan#", type: "string" },
                { field: "RET_CHALAN_DT", title: "Challan Date", type: "date", format: "{0:dd-MMM-yyyy}" },
                { field: "SUP_TRD_NAME_EN", title: "Party", type: "string" },                                
                { field: "STYLE_NO", title: "Style#", type: "string", hidden: true },                                
                { field: "ORDER_NO", title: "Order#", type: "string" },                
                { field: "REMARKS", title: "Remarks", type: "string", hidden: true },
                {
                    title: "Action",
                    template: function () {                        
                        return "<a class='btn btn-xs blue' ui-sref='SciGreyFabRtnFromPartyH.FabricReturn({pKNT_SCI_GFAB_RET_H_ID:dataItem.KNT_SCI_GFAB_RET_H_ID, pSCM_SUPPLIER_ID:dataItem.SCM_SUPPLIER_ID })' ><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "250px"
                }          
            ]
        };

        

        vm.newChallan = function () {            
            $state.go('SciGreyFabRtnFromPartyH.FabricReturn', { pKNT_SCI_GFAB_RET_H_ID: 0, pSCM_SUPPLIER_ID: vm.form.SCM_SUPPLIER_ID });
        }

        vm.printChallan = function (dataItem) {
            var rptCode = 'RPT-3520';

            var data = angular.copy(dataItem);

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
////////// End List Controller