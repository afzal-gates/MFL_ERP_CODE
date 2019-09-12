////////// Start KntSciYarnWipTranHController Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSciYarnWipTranHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', 'sciYrnTranHdrData', KntSciYarnWipTranHController]);
    function KntSciYarnWipTranHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog, sciYrnTranHdrData) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
        
        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'KNT_SCI_YRN_TR_H_ID';
        vm.today = new Date();

        vm.form = sciYrnTranHdrData[key] ? sciYrnTranHdrData : {
            TRX_DT: vm.today, KNT_SCI_YRN_TR_H_ID: 0, SCM_SUPPLIER_ID: null, FRM_COMPANY_ID: null, FRM_OFFICE_ID: null, TO_COMPANY_ID: null, TO_OFFICE_ID: null, SCI_BILL_D_XML: '', REPORT_CODE: 'RPT-3563'
        };
        $scope.form = sciYrnTranHdrData[key] ? sciYrnTranHdrData : {
            TRX_DT: vm.today, KNT_SCI_YRN_TR_H_ID: 0, SCM_SUPPLIER_ID: null, FRM_COMPANY_ID: null, FRM_OFFICE_ID: null, TO_COMPANY_ID: null, TO_OFFICE_ID: null, SCI_BILL_D_XML: '', REPORT_CODE: 'RPT-3563'
        };

                 

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getSupplierList(), getCompanyList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }

        vm.billDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.billDateOpened = true;
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

                    $scope.delvChlnDataSource.read();
                },
                dataBound: function (e) {
                    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
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
        
        function getCompanyList() {
            vm.companyOptions = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;                   
                }
            };

            return vm.companyDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/CompanyList';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        $scope.delvChlnOptions = {
            height: 380,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
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
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.selectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Select?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Select?' ng-model='dataItem.IS_SELECT' ng-true-value='\"Y\"' ng-false-value='\"N\"' >";
                    },
                    width: "50px"
                },
                { field: "CHALAN_DT", title: "Date", format: "{0:dd-MM-yyyy}", width: "100px", editable: false, filterable: false },
                { field: "CHALAN_NO", title: "Challan#", width: "200px", editable: false, filterable: true },
                { field: "DELV_ROLL_WT", title: "Qty", width: "100px", editable: false }//,                             
                //{
                //    title: "",
                //    template: function () {
                //        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.IS_FINALIZE==\"Y\"' ><i class='fa fa-remove'></i></button>";
                //    },
                //    width: "50px"
                //}
            ]
        };


        $scope.delvChlnDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            pageSize: 100,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KntSciBill/GetChallanList4Bill';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID||0);

                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    console.log(url);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },           
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_SC_GFAB_DLV_H_ID",
                    fields: {
                        CHALAN_DT: { type: "date", filterable: false },
                        DELV_ROLL_WT: { type: "number", filterable: false }
                    }
                }
            }
        });
        
        $scope.$watch('vm.form', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                }
            }
        }, true);

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
              

    }

})();
////////// End KntSciBillProcHController Controller









////////// Start KntSciBillProcDController Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSciYarnWipTranDController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', '$modal', 'KnittingDataService', 'Dialog', KntSciYarnWipTranDController]);
    function KntSciYarnWipTranDController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, $modal, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        
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

                    vm.IS_NEXT = 'N';

                    //vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    //vm.form.KNT_SC_PRG_RCV_ID = null;

                    //vm.scProgDataSource.read();
                },
                dataBound: function (e) {
                    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
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


        vm.selectAll = function (v, index) {
            var data = $scope.$parent.delvChlnDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            console.log(data);

            angular.forEach(data, function (val, key) {
                val['IS_SELECT'] = v;
            });
        };
        
        vm.includeBill = function () {
            var data = $scope.$parent.delvChlnDataSource.data();
            var billData = vm.billDtlDataSource.data();

            angular.forEach(data, function (val, key) {

                if (val['IS_SELECT'] == 'Y') {
                    var chlnData = _.filter(billData, function (ob) {
                        return ob.KNT_SC_GFAB_DLV_H_ID == val['KNT_SC_GFAB_DLV_H_ID'];
                    });

                    if (chlnData.length < 1) {
                        angular.forEach(val['itmDtl'], function (val1, key1) {
                            if (val1['IS_PAY_BY_BILL'] == 'Y') {

                                val1['KNT_SC_GFAB_DLV_H_ID'] = val['KNT_SC_GFAB_DLV_H_ID'];
                                val1['CHALAN_NO'] = val['CHALAN_NO'];

                                val1['CHLN_QTY'] = val1['DELV_GFAB_WT'];
                                val1['DEDUCT_QTY'] = 0; //val1['DELV_GFAB_WT'];
                                val1['NET_QTY'] = val1['DELV_GFAB_WT'] - val1['DEDUCT_QTY'];

                                if (val1['IS_PAY_BY_BILL'] == 'N') {
                                    val1['BILL_AMT'] = 0;
                                    val1['PROD_RATE'] = 0;
                                }
                                else {
                                    val1['BILL_AMT'] = val1['PROD_RATE'] * val1['NET_QTY'];
                                }

                                vm.billDtlDataSource.insert(0, val1);
                            }
                        });
                    }
                }
            });
        }
        
        vm.removeRow = function (dataItem) {
            var data = angular.copy(dataItem);

            var dataList = vm.billDtlDataSource.data();
            var length = dataList.length;

            var vMsg = 'Do you want to remove items of the challan# ' + dataItem.CHALAN_NO;
            
            console.log(dataList);

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                var item, i;
                for (i = length - 1; i >= 0; i--) {

                    item = dataList[i];
                    if (data.CHALAN_NO == item['CHALAN_NO']) {
                        vm.billDtlDataSource.remove(item);
                    }

                }             
            });            
        };

        vm.onBlurProdRateQty = function (dataItem) {            
            dataItem.NET_QTY = (dataItem.CHLN_QTY - dataItem.DEDUCT_QTY).toFixed(2);
            dataItem.BILL_AMT = ((dataItem.CHLN_QTY - dataItem.DEDUCT_QTY) * dataItem.PROD_RATE).toFixed(2);
        }

        vm.onBlurDuducQty = function (dataItem) {
            dataItem.NET_QTY = (dataItem.CHLN_QTY - dataItem.DEDUCT_QTY).toFixed(2);
            dataItem.BILL_AMT = ((dataItem.CHLN_QTY - dataItem.DEDUCT_QTY) * dataItem.PROD_RATE).toFixed(2);
        }

        

        vm.billDtlOptions = {
            height: 350,
            sortable: true,
            pageable: false,
            editable: false,
            selectable: "row",
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
                { field: "CHALAN_NO", title: "Challan#", type: "string", width: "10%", editable: false, filterable: false },
                { field: "BUYER_SNAME", title: "Buyer", type: "string", width: "0%", editable: false, hidden: true },
                { field: "STYLE_NO", title: "Style#", type: "string", width: "0%", editable: false, hidden: true },
                { field: "ORDER_NO_LST", title: "Order#", type: "string", width: "14%", editable: false },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%", editable: false, filterable: false },
                { field: "FABRIC_DESC", title: "Fabric", type: "string", width: "21%", editable: false, filterable: false },                
                { field: "CHLN_QTY", title: "Delv.Qty", type: "number", width: "7%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },                
                {
                    title: "Dedu.Qty",
                    field: "DEDUCT_QTY",
                    template: function () {
                        return "<ng-form name='frmDeduQty'><input type='number' class='form-control' name='DEDUCT_QTY' ng-model='dataItem.DEDUCT_QTY' ng-blur='vm.onBlurDuducQty(dataItem)' min='0' ng-required='dataItem.IS_PAY_BY_BILL==\"Y\"' ng-disabled='(dataItem.IS_PAY_BY_BILL==\"N\"||$parent.form.RF_ACTN_STATUS_ID==75)' ng-style='(frmDeduQty.DEDUCT_QTY.$error.min && dataItem.IS_PAY_BY_BILL==\"Y\") || (frmDeduQty.DEDUCT_QTY.$error.required && dataItem.IS_PAY_BY_BILL==\"Y\")? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "8%",
                    filterable: false,
                    footerTemplate: "#=kendo.toString(sum, '0.00')#"
                },
                { field: "NET_QTY", title: "Net.Qty", type: "number", width: "7%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                {
                    title: "Rate",
                    field: "PROD_RATE",                    
                    template: function () {
                        return "<ng-form name='frmProdRate'><input type='number' class='form-control' name='PROD_RATE' ng-model='dataItem.PROD_RATE' ng-blur='vm.onBlurProdRateQty(dataItem)' min='{{dataItem.MIN_PROD_RATE}}' ng-required='dataItem.IS_PAY_BY_BILL==\"Y\"' ng-disabled='(dataItem.IS_PAY_BY_BILL==\"N\"||$parent.form.RF_ACTN_STATUS_ID==75)' ng-style='(frmProdRate.PROD_RATE.$error.min && dataItem.IS_PAY_BY_BILL==\"Y\") || (frmProdRate.PROD_RATE.$error.required && dataItem.IS_PAY_BY_BILL==\"Y\")? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "5%",
                    filterable: false
                },
                { field: "BILL_AMT", title: "Bill Amt", type: "number", width: "10%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                { field: "IS_PAY_BY_BILL", title: "Bill?", type: "string", width: "5%", filterable: false, editable: false },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='$parent.form.RF_ACTN_STATUS_ID==75' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "3%"
                }
            ]            
        };


        vm.billDtlDataSource = new kendo.data.DataSource({            
            transport: {
                read: function (e) {
                    
                    var url = '/api/Knit/KntSciBill/GetBillDtl?pKNT_SCI_BILL_H_ID=' + ($stateParams.pKNT_SCI_BILL_H_ID || $scope.$parent.form.KNT_SCI_BILL_H_ID || 0);
                    
                    console.log(url);

                    //e.success([]);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            aggregate: [
                { field: "CHLN_QTY", aggregate: "sum" },
                { field: "DEDUCT_QTY", aggregate: "sum" },
                { field: "NET_QTY", aggregate: "sum" },
                { field: "BILL_AMT", aggregate: "sum" }
            ],            
            schema: {                
                model: {
                    id: "KNT_SCI_BILL_D_ID",
                    fields: {                        
                        CHLN_QTY: { type: "number", filterable: false },
                        DEDUCT_QTY: { type: "number", filterable: false },
                        NET_QTY: { type: "number", filterable: false },
                        BILL_AMT: { type: "number", filterable: false }
                    }
                }
            }
        });


        vm.submitData = function (token, pIS_FINALIZE, pNEXT_RF_ACTN_STATUS_ID) {
            var submitData = angular.copy($scope.$parent.form);
            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZE == 'Y') {
                vMsg = 'Do you want to save and finalize?';
            }


            Dialog.confirm(vMsg, 'Are you sure?', ['Yes', 'No']).then(function () {

                submitData['IS_FINALIZED'] = pIS_FINALIZE;
                submitData['NEXT_RF_ACTN_STATUS_ID'] = pNEXT_RF_ACTN_STATUS_ID;
                
                submitData['BILL_DT'] = $filter('date')($scope.$parent.form.BILL_DT, vm.dtFormat);
                submitData['PREV_BILL_AMT']= parseInt(submitData['PREV_BILL_AMT'],2);
                submitData['CURR_BILL_AMT'] = parseInt(submitData['CURR_BILL_AMT'], 2);

                var billDtlData = vm.billDtlDataSource.data();
                console.log(billDtlData);


                submitData.SCI_BILL_D_XML = KnittingDataService.xmlStringShort(billDtlData.map(function (ob) {                    
                    return {
                        KNT_SCI_BILL_D_ID: ob.KNT_SCI_BILL_D_ID, KNT_SCI_BILL_H_ID: ob.KNT_SCI_BILL_H_ID, KNT_SC_GFAB_DLV_H_ID: ob.KNT_SC_GFAB_DLV_H_ID,
                        KNT_STYL_FAB_ITEM_ID: ob.KNT_STYL_FAB_ITEM_ID, CHALAN_NO: ob.CHALAN_NO, CHLN_QTY: ob.CHLN_QTY, DEDUCT_QTY: ob.DEDUCT_QTY, NET_QTY: ob.NET_QTY,
                        QTY_MOU_ID: 3, PROD_RATE: ob.PROD_RATE, IS_PAY_BY_BILL: ob.IS_PAY_BY_BILL
                    };
                }));

                console.log(submitData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + token },
                    url: '/api/Knit/KntSciBill/BatchSave',
                    method: 'post',
                    data: submitData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.isSaved = true;

                            if ($scope.$parent.form.RF_ACTN_STATUS_ID == 12 && pIS_FINALIZE == 'Y') { //Verify and Finalize by Receiver
                                $state.go('SciBillProcList', { pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID });                                
                            }
                            else if ($scope.$parent.form.RF_ACTN_STATUS_ID == 12 && pNEXT_RF_ACTN_STATUS_ID == 15) { //Resend by Receiver
                                $state.go('SciBillProcList', { pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID });
                            }
                            else {
                                $scope.$parent.form.KNT_SCI_BILL_H_ID = res.data.PKNT_SCI_BILL_H_ID_RTN;
                                $scope.$parent.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                                $scope.$parent.form.BILL_NO = res.data.PBILL_NO_RTN;

                                //if ($scope.$parent.form.PARENT_ID) {
                                $scope.$parent.form.PREV_BILL_AMT = res.data.PPREV_BILL_AMT_RTN;
                                $scope.$parent.form.CURR_BILL_AMT = res.data.PCURR_BILL_AMT_RTN;
                                $scope.$parent.form.REV_AMT = res.data.PREV_AMT_RTN;
                                $scope.$parent.form.IS_DM_CM = res.data.PIS_DM_CM_RTN;
                                //}

                                $state.go('SciBillProcH.Dtl', { pKNT_SCI_BILL_H_ID: res['data'].PKNT_SCI_BILL_H_ID_RTN }, { notify: false });

                                vm.billDtlDataSource.read();
                            }
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.backToList = function () {
            return $state.go('SciBillProcList');
        };
        
        vm.printBill = function () {
            //console.log(dataItem);
            $scope.$parent.form.REPORT_CODE = 'RPT-3563';

            var url;
            url = '/api/Knit/KntReport/PreviewReport';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');
            
            var params = angular.copy($scope.$parent.form);

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

        vm.reviseBillModal = function (scBillHdrID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'SciBillRevisionModal.html',
                controller: 'KntSciBillReviseController',
                size: 'md',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    scBillHdrID: function () {
                        return scBillHdrID;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                //console.log('received');
                console.log(data);

                var vRevisionData = angular.copy(data);

                $state.go('SciBillProcH.Dtl', { pKNT_SCI_BILL_H_ID: vRevisionData.KNT_SCI_BILL_H_ID });

                //$scope.$parent.form.KNT_SCI_BILL_H_ID = vRevisionData.KNT_SCI_BILL_H_ID;
                //$scope.$parent.form.REVISION_NO = vRevisionData.REVISION_NO;
                //$scope.$parent.form.BILL_DT = vRevisionData.BILL_DT;
                //$scope.$parent.form.BILL_NO = vRevisionData.BILL_NO;
                //$scope.$parent.form.RF_ACTN_STATUS_ID = vRevisionData.RF_ACTN_STATUS_ID;

                //$scope.$parent.form.CORR_TYPE_NAME = vRevisionData.CORR_TYPE_NAME;
                //$scope.$parent.form.REV_AMT = vRevisionData.REV_AMT;
                //$scope.$parent.form.OTH_RSN_DESC = vRevisionData.OTH_RSN_DESC;

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };





    }

})();
////////// End KntSciBillProcDController Controller









//=============== Start for KntSciYarnWipTranListController List =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSciYarnWipTranListController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'userLavelData', KntSciYarnWipTranListController]);
    function KntSciYarnWipTranListController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, commonDataService, userLavelData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.form = {};

        console.log(userLavelData);
        if (userLavelData['USER_DRAFT_NAME'] == 'DRAFTER') {
            vm.isVisableAddBtn = 'Y';
        }
        else {
            vm.isVisableAddBtn = 'N';
        }

        //vm.fabOrder = { FIN_DIA_N_DIA_TYPE: '', DIA_MOU_ID: 23, LK_DIA_TYPE_ID: 327, DIA_MOU_CODE: 'Open', QTY_MOU_ID: 3, QTY_MOU_CODE: 'Kg' };
        //vm.fabOrder = { MC_FAB_PROD_ORD_D_ID: -1, MC_FAB_PROD_ORD_H_ID: -1, FIN_DIA_N_DIA_TYPE: '', DIA_MOU_CODE: 'Open', QTY_MOU_CODE: 'Kg' };


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [GetSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function GetSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    $stateParams.pSCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                },
                dataBound: function (e) {
                    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
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

        vm.sciBillGridOption = {
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
                { field: "BILL_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "BILL_NO", title: "Bill#", type: "string", width: "10%" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier", width: "15%" },
                { field: "COMP_NAME_EN", title: "Company", width: "15%" },
                //{ field: "EVENT_NAME", title: "Status", width: "10%" },
                {
                    field: "EVENT_NAME",
                    title: "Status",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID<75 && dataItem.RF_ACTN_STATUS_ID!=15'>{{dataItem.EVENT_NAME}}</label> <label class='label label-sm label-danger' ng-show='dataItem.RF_ACTN_STATUS_ID==15'>{{dataItem.EVENT_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==75'>{{dataItem.EVENT_NAME}}</label>";
                    },
                    width: "10%"
                },
                {
                    title: "Action",
                    template: function () {                        
                        return "<button type='button' class='btn btn-xs blue' title='Edit/View' ng-click='vm.editBill(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;<button type='button' class='btn btn-xs blue' title='Print' ng-click='vm.printBill(dataItem)'><i class='fa fa-print'></i></button>";
                    },
                    width: "4%"
                }
            ]
        };

        vm.sciBillGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_SCI_BILL_H_ID",
                    fields: {
                        BILL_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KntSciBill/GetBillList?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID || 0);

                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    });
                }
            },
            sort: [{ field: 'BILL_DT', dir: 'desc' }]
        });


        vm.getBillList = function () {
            vm.sciBillGridDataSource.read();

            $state.go('SciBillProcList', { pSCM_SUPPLIER_ID: (vm.form.SCM_SUPPLIER_ID || 0) }, { notify: false });
        };
        
        vm.editBill = function (dataItem) {
            $state.go('SciBillProcH.Dtl', { pKNT_SCI_BILL_H_ID: dataItem.KNT_SCI_BILL_H_ID });
        };

        vm.printBill = function (dataItem) {
            //console.log(dataItem);
            dataItem.REPORT_CODE = 'RPT-3563';

            var url;
            url = '/api/Knit/KntReport/PreviewReport';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            var params = angular.copy(dataItem);

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
//=============== End for KntSciBillProcListController List =================