////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoProgTransHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'formData', 'KnittingDataService', 'Dialog', KntScoProgTransHController]);
    function KntScoProgTransHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, formData, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        
        var key = 'KNT_SCO_YRN_TR_H_ID';
        console.log(formData);
        console.log($stateParams);
        console.log(formData['KNT_SCO_YRN_TR_H_ID']);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.today = new Date();
        vm.form = formData[key] ? formData : { KNT_SCO_YRN_TR_H_ID: 0, CHALAN_DT: moment(vm.today).format("DD-MMM-YYYY"), IS_FINALIZED: 'N', chlnYrnTransDtl: [], yrnTranStkDtl: [] }; //formData[key] ? formData : { KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', IS_FINALIZE: 'N' };
        $scope.form = formData[key] ? formData : { KNT_SCO_YRN_TR_H_ID: 0, CHALAN_DT: moment(vm.today).format("DD-MMM-YYYY"), IS_FINALIZED: 'N', chlnYrnTransDtl: [], yrnTranStkDtl: [] }; //formData[key] ? formData : { KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', IS_FINALIZE: 'N' };

        //vm.IS_NEXT = 'N';
        $scope.isFabAddToGrid = 'Y';
        $scope.selectedFabRow = {};
           
        vm.yrnTransState = ($state.current.name === 'ScoProgTransH.YrnTrans') ? true : false;
                        
        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getSupplierList(), getScoIssProgList(), getScoFabYrnRcvChlnList()];
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
            vm.yrnTransState = true;
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

        
        function getSupplierList() {
            vm.fromSupplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.FRM_SUPPLIER_ID = item.SCM_SUPPLIER_ID
                    //vm.form.yrnTranStkDtl = [];
                    vm.IS_NEXT = 'N';

                    vm.scoFabYrnRcvChlnDataSource.read();
                },
                dataBound: function (e) {
                    
                    if ($stateParams.pFRM_SUPPLIER_ID != null && $stateParams.pFRM_SUPPLIER_ID > 0) {
                        vm.form.FRM_SUPPLIER_ID = $stateParams.pFRM_SUPPLIER_ID;
                    }
                    
                }
            };

            vm.toSupplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.TO_SUPPLIER_ID = item.SCM_SUPPLIER_ID
                    //vm.form.yrnTranStkDtl = [];                    
                    vm.IS_NEXT = 'N';

                    vm.scoIssProgDataSource.read();
                },
                dataBound: function (e) {
                    if ($stateParams.pTO_SUPPLIER_ID != null && $stateParams.pTO_SUPPLIER_ID > 0) {
                        vm.form.TO_SUPPLIER_ID = $stateParams.pTO_SUPPLIER_ID;
                    }
                }
            };

            return KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                //e.success(res);
                vm.frmSupplierDataSource = new kendo.data.DataSource({
                    data: res
                });

                vm.toSupplierDataSource = new kendo.data.DataSource({
                    data: res
                });
            });

            //return vm.supplierDataSource = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
            //                e.success(res);
            //            });
            //        }
            //    }
            //});
        };

        function getScoIssProgList() {
            vm.scoIssProgOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "PRG_ISS_NO",
                dataValueField: "KNT_SC_PRG_ISS_ID"
            };

            return vm.scoIssProgDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        
                        if (formData[key]) {
                            var url = '/api/knit/KnitPlan/ScProgramPagingData?&pageNumber=1&pageSize=10&pSCM_SUPPLIER_ID=' + vm.form.TO_SUPPLIER_ID;
                            url += '&pKNT_SC_PRG_ISS_ID=' + formData['KNT_SC_PRG_ISS_ID'];
                        }
                        else {
                            var url = '/api/knit/KnitPlan/ScProgramPagingData?&pageNumber=1&pageSize=10&pIS_TI_TE=TI&pIS_YD=N&pSCM_SUPPLIER_ID=' + vm.form.TO_SUPPLIER_ID;
                        }
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
      
        function getScoFabYrnRcvChlnList() {
            vm.scoFabYrnRcvChlnOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "CHALAN_NO",
                dataValueField: "KNT_SCO_CHL_RCV_H_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.KNT_SCO_CHL_RCV_H_ID = item.KNT_SCO_CHL_RCV_H_ID
                    vm.form.RTN_CHALAN_DT = item.CHALAN_DT;
                    //vm.form.yrnTranStkDtl = [];
                    vm.IS_NEXT = 'N';                    
                },
                dataBound: function (e) {

                    if (formData[key]) {
                        vm.form.RTN_CHALAN_DT = formData['CHALAN_DT'];
                    }

                }
            };

            return vm.scoFabYrnRcvChlnDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScChlnRcv/GetRcvChallanList?&pageNumber=1&pageSize=10&pIS_TRANSFER=Y&pIS_FINALIZED=Y&pSCM_SUPPLIER_ID=' + vm.form.FRM_SUPPLIER_ID;
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

        $scope.chlnYrnDtlDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    
                    return KnittingDataService.getDataByFullUrl('/api/knit/KntScoChlnTrans/GetTransChlnYrnDtl?&pKNT_SCO_YRN_TR_H_ID=' + ($stateParams.pKNT_SCO_YRN_TR_H_ID || 0)).then(function (res) {

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
            aggregate: [
                { field: "YARN_DETAIL", aggregate: "count" },
                { field: "INTACT_QTY", aggregate: "sum" },
                { field: "LOOSE_QTY", aggregate: "sum" },
                { field: "TR_QTY", aggregate: "sum" }
            ],
            schema: {
                model: {
                    id: "KNT_SCO_YRN_TR_D_ID",
                    fields: {
                        YARN_DETAIL: { type: "string", editable: false },
                        INTACT_QTY: { type: "number", editable: false },
                        LOOSE_QTY: { type: "number", editable: false },
                        TR_QTY: { type: "number", editable: false }
                    }
                }
            }
        });

        $scope.chlnYrnDtlOption = {
            height: 240,
            sortable: true,
            columns: [                
                { field: "YARN_DETAIL", title: "Yarn", width: "30%", footerTemplate: "Total Record: #=count#" },
                //{ field: "PACK_TR_QTY_DESC", title: "Pack Qty", width: "15%" },
                //{ field: "PACK_TR_QTY", title: "Pack Qty", width: "10%" },
                {
                    title: "Pack Qty",
                    field: "PACK_TR_QTY",                    
                    template: function () {
                        return "<ng-form name='frmPackQty'><input type='number' class='form-control' name='PACK_TR_QTY' ng-model='dataItem.PACK_TR_QTY' ng-disabled='$parent.form.IS_FINALIZED==\"Y\"' ng-blur='vm.onBlurTrQty(dataItem);vm.transQtyValidation(dataItem, frmPackQty.PACK_TR_QTY)' min='0' ng-style='(frmPackQty.PACK_TR_QTY.$error.min)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "10%",
                    filterable: false
                },
                { field: "PACK_MOU_CODE", title: "Pack MoU", width: "10%" },
                { field: "QTY_PER_PACK", title: "Qty/Pack(Kg)", width: "10%" },
                //{ field: "INTACT_QTY", title: "Intact(Kg)", width: "15%", footerTemplate: "#=sum#" },
                {
                    title: "Intact(Kg)",
                    field: "INTACT_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmIntactQty'><input type='number' class='form-control' name='INTACT_QTY' ng-model='dataItem.INTACT_QTY' min='0' ng-disabled='true' ng-style='(frmIntactQty.INTACT_QTY.$error.min)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "10%",
                    filterable: false
                },
                //{ field: "LOOSE_QTY", title: "Loose(Kg)", width: "15%", footerTemplate: "#=sum#" },
                {
                    title: "Loose(Kg)",
                    field: "LOOSE_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmLooseQty'><input type='number' class='form-control' name='LOOSE_QTY' ng-model='dataItem.LOOSE_QTY' ng-disabled='$parent.form.IS_FINALIZED==\"Y\"' ng-blur='vm.onBlurTrQty(dataItem);vm.transQtyValidation(dataItem, frmLooseQty.LOOSE_QTY)' min='0' ng-style='(frmLooseQty.LOOSE_QTY.$error.min)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "10%",
                    filterable: false
                },                
                //{ field: "TR_QTY", title: "Tr. Qty(Kg)", width: "15%", footerTemplate: "#=sum#" },
                {
                    title: "Tr. Qty(Kg)",
                    field: "TR_QTY",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmTrQty'><input type='number' class='form-control' name='TR_QTY' ng-model='dataItem.TR_QTY' ng-disabled='true' min='1' required ng-style='(frmTrQty.TR_QTY.$error.min||frmTrQty.TR_QTY.$error.required||(dataItem.TR_QTY>dataItem.DUE_TR_QTY && $parent.form.IS_FINALIZED==\"N\"))? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "10%",
                    filterable: false
                },
                {
                    title: "Due Tr. Qty(Kg)",
                    field: "DUE_TR_QTY",
                    //footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmDueTrQty'><input type='number' class='form-control' name='DUE_TR_QTY' ng-model='dataItem.DUE_TR_QTY' ng-disabled='true' /></ng-form>";
                    },
                    width: "10%",
                    filterable: false
                }
                //{
                //    title: "Action",
                //    template: function () {
                //        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.MC_FAB_PROD_ORD_D_ID>0?true:false' ><i class='fa fa-remove'></i></button>";
                //    },
                //    width: "5%"
                //}
            ]
        };
        
        

        vm.gotoNext = function () {
           
            vm.yrnTransState = true;
            vm.IS_NEXT = 'Y';
            $state.go('ScoProgTransH.YrnTrans', { pKNT_SCO_YRN_TR_H_ID: 0, pFRM_SUPPLIER_ID: vm.form.FRM_SUPPLIER_ID, pTO_SUPPLIER_ID: vm.form.TO_SUPPLIER_ID }, { notify: false });
            
            return KnittingDataService.getDataByFullUrl('/api/knit/KntScoChlnTrans/GetChlnWiseTransferedYrn?pKNT_SCO_CHL_RCV_H_ID=' + (vm.form.KNT_SCO_CHL_RCV_H_ID || 0)).then(function (res) {
                var dataList = [];
                //angular.forEach(res, function (val, key) {                    
                //    //val['PACK_TR_QTY_DESC'] = val['PACK_RET_QTY'] + ' ' + val['PACK_MOU_CODE'];
                //    //val['PACK_TR_QTY'] = val['PACK_RET_QTY'];
                //    val['TR_QTY'] = val['RET_QTY'];
                //});

                console.log(res);

                $scope.chlnYrnDtlDataSource = new kendo.data.DataSource({
                    data: res,
                    aggregate: [
                        { field: "YARN_DETAIL", aggregate: "count" },
                        { field: "INTACT_QTY", aggregate: "sum" },
                        { field: "LOOSE_QTY", aggregate: "sum" },
                        { field: "TR_QTY", aggregate: "sum" }
                    ],
                    schema: {
                        model: {
                            id: "KNT_SCO_YRN_TR_D_ID",
                            fields: {
                                YARN_DETAIL: { type: "string", editable: false },
                                INTACT_QTY: { type: "number", editable: false },
                                LOOSE_QTY: { type: "number", editable: false },
                                TR_QTY: { type: "number", editable: false }
                            }
                        }
                    }
                });               
            });
        }

        vm.batchSave = function (token, pIS_FINALIZED) {
            var submitRcvData = angular.copy($scope.form);
            submitRcvData['chlnYrnTransDtl']=$scope.chlnYrnDtlDataSource.data();

            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZED == 'Y') {
                submitRcvData['IS_FINALIZED'] = 'Y';
                vMsg = 'Do you want to finalize?';
            }


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {
                
                submitRcvData['CHALAN_DT'] = $filter('date')(submitRcvData['CHALAN_DT'], vm.dtFormat);
                
                submitRcvData.YRN_TRANS_DTL_XML = KnittingDataService.xmlStringShort(submitRcvData['chlnYrnTransDtl'].map(function (ob) {
                    return ob;
                }));
          

                console.log(submitRcvData);
                //return;

                return KnittingDataService.saveDataByFullUrl(submitRcvData, '/api/knit/KntScoChlnTrans/BatchSave4ScoYrnTransfer').then(function (res) {
                    console.log(res);

                    vm.errors = undefined;
                    if (res.success === false) {
                        vm.errors = [];
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            
                            if (res.data.PKNT_SCO_YRN_TR_H_ID_RTN != 0) {
                                $stateParams.KNT_SCO_YRN_TR_H_ID = res.data.PKNT_SCO_YRN_TR_H_ID_RTN;
                                $scope.form.KNT_SCO_YRN_TR_H_ID = res.data.PKNT_SCO_YRN_TR_H_ID_RTN;
                                if (pIS_FINALIZED == 'Y') {
                                    $scope.form.IS_FINALIZED = 'Y';
                                }
                                
                                $state.go('ScoProgTransH.YrnTrans', { pKNT_SCO_YRN_TR_H_ID: res.data.PKNT_SCO_YRN_TR_H_ID_RTN, pFRM_SUPPLIER_ID: vm.form.FRM_SUPPLIER_ID, pTO_SUPPLIER_ID: vm.form.TO_SUPPLIER_ID }, { reload: true });
                                //$scope.chlnYrnDtlDataSource.read();                                                                                           
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
            $state.go('ScoProgTransH.YrnTrans', { pKNT_SCO_YRN_TR_H_ID: 0, pFRM_SUPPLIER_ID: vm.form.FRM_SUPPLIER_ID, pTO_SUPPLIER_ID: vm.form.TO_SUPPLIER_ID });
            
            $scope.form = { KNT_SCO_YRN_TR_H_ID: 0, CHALAN_DT: moment(vm.today).format("DD-MMM-YYYY"), chlnYrnTransDtl: [], yrnTranStkDtl: [] };
        }

        vm.backToList = function () {
            return $state.go('ScoProgTransList', { pSCM_SUPPLIER_ID: $stateParams.pTO_SUPPLIER_ID||0 });
        };

        vm.printChallan = function () {
            var rptCode = 'RPT-3524';
            
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





////////// Start Yarn Transfer Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoProgTransYrnTransController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntScoProgTransYrnTransController]);
    function KntScoProgTransYrnTransController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

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
            var promise = [];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }
        
        
        
        vm.onBlurTrQty = function (dataItem) {
            dataItem.INTACT_QTY = (dataItem.PACK_TR_QTY * dataItem.QTY_PER_PACK);
            dataItem.TR_QTY = (dataItem.PACK_TR_QTY * dataItem.QTY_PER_PACK) + dataItem.LOOSE_QTY;
        };
        
        vm.transQtyValidation = function (dataItem, ctrl) {
            //alert('v');
            if (dataItem.TR_QTY > dataItem.DUE_TR_QTY) {
                ctrl.$setValidity('TR_QTY', false);
                //frmScProgTrans.capabilities.$error.required = true;
            } else {
                ctrl.$setValidity('TR_QTY', true);
                //frmScProgTrans.capabilities.$error.required = false;
            }
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
////////// End Yarn Transfer Controller






////////// Start List Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoProgTransListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntScoProgTransListController]);
    function KntScoProgTransListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'KNT_SCO_YRN_TR_H_ID';
        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.today = new Date();
        vm.form = { KNT_SCO_YRN_TR_H_ID: 0, CHALAN_DT: moment(vm.today).format("DD-MMM-YYYY"), prgIssDtl: [], yrnRcvDtl: [] }; //formData[key] ? formData : { KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', IS_FINALIZE: 'N' };


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
                            CHALAN_DT: { type: "date" }
                        }
                    }
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScoChlnTrans/GetTransChlnList?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID;
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
                { field: "FRM_SUP_TRD_NAME_EN", title: "From Party", type: "string" },
                { field: "TO_SUP_TRD_NAME_EN", title: "To Party", type: "string" },
                { field: "CHALAN_NO", title: "Challan#", type: "string" },
                { field: "CHALAN_DT", title: "Challan Date", type: "date", format: "{0:dd-MMM-yyyy}" },                
                {
                    title: "Action",
                    template: function () {
                        return "<a class='btn btn-xs blue' ui-sref='ScoProgTransH.YrnTrans({pKNT_SCO_YRN_TR_H_ID: dataItem.KNT_SCO_YRN_TR_H_ID, pFRM_SUPPLIER_ID: dataItem.FRM_SUPPLIER_ID, pTO_SUPPLIER_ID: dataItem.TO_SUPPLIER_ID })' ><i class='fa fa-edit'> Edit</i></a>&nbsp;<a ng-click='vm.printChallan(dataItem)' class='btn btn-xs blue' ng-disabled='dataItem.IS_FINALIZED!=\"Y\"'><i class='fa fa-print'> Print</i></a>";
                    },
                    width: "250px"
                }
            ]
        };


        vm.newChallan = function () {
            $state.go('ScoProgTransH.YrnTrans', { pKNT_SCO_YRN_TR_H_ID: 0, pFRM_SUPPLIER_ID: 0, pSCM_SUPPLIER_ID: vm.form.SCM_SUPPLIER_ID });
            //$state.go('ScoProgTransH.YrnTrans', { pKNT_SCO_YRN_TR_H_ID: res.data.PKNT_SCO_YRN_TR_H_ID_RTN, pFRM_SUPPLIER_ID: vm.form.FRM_SUPPLIER_ID, pTO_SUPPLIER_ID: vm.form.TO_SUPPLIER_ID });
        }

        vm.printChallan = function (itm) {
            var rptCode = 'RPT-3524';

            var data = angular.copy(itm);

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