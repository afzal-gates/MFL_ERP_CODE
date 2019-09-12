////////// Start KntScoBillProcHController Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoBillProcHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', 'scoBillHdrData', KntScoBillProcHController]);
    function KntScoBillProcHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog, scoBillHdrData) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
        
        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'KNT_SCO_BILL_H_ID';
        vm.today = new Date();

        vm.form = scoBillHdrData[key] ? scoBillHdrData : {
            BILL_DT: vm.today, KNT_SCO_BILL_H_ID: 0, HR_COMPANY_ID: null, SCM_SUPPLIER_ID: null, BILL_NO: '', REMARKS: '', MISC_DESC: '', MISC_BILL_AMT: 0, TOT_BILL_AMT: 0,
            SCO_BILL_D_XML: '', REPORT_CODE: 'RPT-3563'
        };
        $scope.form = scoBillHdrData[key] ? scoBillHdrData : {
            BILL_DT: vm.today, KNT_SCO_BILL_H_ID: 0, HR_COMPANY_ID: null, SCM_SUPPLIER_ID: null, BILL_NO: '', REMARKS: '', MISC_DESC: '', MISC_BILL_AMT: 0, TOT_BILL_AMT: 0,
            SCO_BILL_D_XML: '', REPORT_CODE: 'RPT-3563'
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
            height: 440,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
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
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.selectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Select?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Select?' ng-model='dataItem.IS_SELECT' ng-true-value='\"Y\"' ng-false-value='\"N\"' >";
                    },
                    width: "50px"
                },
                { field: "CHALAN_DT", title: "Date", format: "{0:dd-MM-yyyy}", width: "100px", editable: false, filterable: false },
                { field: "CHALAN_NO", title: "Challan#", width: "200px", editable: false, filterable: true },
                { field: "RCV_ROLL_WT", title: "Qty", width: "100px", editable: false }//,                             
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
                    var url = '/api/knit/KntScoBill/GetChallanList4Bill';
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
                    id: "KNT_SCO_CHL_RCV_H_ID",
                    fields: {
                        CHALAN_DT: { type: "date", filterable: false },
                        RCV_ROLL_WT: { type: "number", filterable: false }
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
////////// End KntScoBillProcHController Controller









////////// Start KntScoBillProcDController Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoBillProcDController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', '$modal', 'KnittingDataService', 'Dialog', KntScoBillProcDController]);
    function KntScoBillProcDController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, $modal, KnittingDataService, Dialog) {

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
                        return ob.KNT_SCO_CHL_RCV_H_ID == val['KNT_SCO_CHL_RCV_H_ID'];
                    });

                    if (chlnData.length < 1) {
                        angular.forEach(val['fabRcvDtl'], function (val1, key1) {
                            val1['KNT_SCO_CHL_RCV_H_ID'] = val['KNT_SCO_CHL_RCV_H_ID'];
                            val1['CHALAN_NO'] = val['CHALAN_NO'];
                            
                            val1['CHLN_QTY'] = val1['RCV_ROLL_WT'];
                            val1['DEDUCT_QTY'] = 0; //val1['DELV_GFAB_WT'];
                            val1['NET_QTY'] = val1['RCV_ROLL_WT'] - val1['DEDUCT_QTY'];

                            val1['BILL_AMT'] = val1['PROD_RATE'] * val1['NET_QTY'];

                            vm.billDtlDataSource.insert(0, val1);
                        });
                    }
                }
            });

            vm.totalBillAmt();
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

            vm.totalBillAmt();
        }

        vm.onBlurDuducQty = function (dataItem) {
            dataItem.NET_QTY = (dataItem.CHLN_QTY - dataItem.DEDUCT_QTY).toFixed(2);
            dataItem.BILL_AMT = ((dataItem.CHLN_QTY - dataItem.DEDUCT_QTY) * dataItem.PROD_RATE).toFixed(2);

            vm.totalBillAmt();
        }

        vm.totalBillAmt = function () {
            var billDtlData = vm.billDtlDataSource.data();

            var totBillamt = _.sumBy(billDtlData, function (o) { return o.BILL_AMT; });
            $scope.$parent.form.CURR_BILL_AMT = (totBillamt + $scope.$parent.form.MISC_BILL_AMT).toFixed(2);
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
                //{ field: "BYR_ACC_NAME_EN", title: "Buyer", type: "string", width: "10%", editable: false },
                { field: "STYLE_NO", title: "Style#", type: "string", width: "0%", editable: false, hidden: true },
                { field: "ORDER_NO_LST", title: "Order#", type: "string", width: "15%", editable: false },
                //{ field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%", editable: false, filterable: false },
                { field: "FAB_ITEM_DESC", title: "Fabric", type: "string", width: "35%", editable: false, filterable: false },                
                { field: "CHLN_QTY", title: "Delv.Qty", type: "number", width: "7%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                {
                    title: "Dedu.Qty",
                    field: "DEDUCT_QTY",
                    template: function () {
                        return "<ng-form name='frmDeduQty'><input type='number' class='form-control' name='DEDUCT_QTY' ng-model='dataItem.DEDUCT_QTY' ng-blur='vm.onBlurDuducQty(dataItem)' min='0' ng-disabled='($parent.form.RF_ACTN_STATUS_ID==75)' ng-style='(frmDeduQty.DEDUCT_QTY.$error.min)? vm.errstyle:\"\"' /></ng-form>";
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
                        return "<ng-form name='frmProdRate'><input type='number' class='form-control' name='PROD_RATE' ng-model='dataItem.PROD_RATE' ng-blur='vm.onBlurProdRateQty(dataItem)' min='1' required ng-style='(frmProdRate.PROD_RATE.$error.min||frmProdRate.PROD_RATE.$error.required)? vm.errstyle:\"\"' ng-disabled='$parent.form.RF_ACTN_STATUS_ID==75' /></ng-form>";
                    },
                    width: "5%",
                    filterable: false
                },
                { field: "BILL_AMT", title: "Bill Amt", type: "number", width: "10%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
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
                    
                    var url = '/api/Knit/KntScoBill/GetBillDtl?pKNT_SCO_BILL_H_ID=' + ($stateParams.pKNT_SCO_BILL_H_ID || $scope.$parent.form.KNT_SCO_BILL_H_ID || 0);
                    
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
                    id: "KNT_SCO_BILL_D_ID",
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
                submitData['PREV_BILL_AMT'] = parseInt(submitData['PREV_BILL_AMT'], 2);
                submitData['CURR_BILL_AMT'] = parseInt(submitData['CURR_BILL_AMT'], 2);
                submitData['REV_AMT'] = parseInt(submitData['REV_AMT'], 2);

                var billDtlData = vm.billDtlDataSource.data();
                console.log(billDtlData);


                submitData.SCO_BILL_D_XML = KnittingDataService.xmlStringShort(billDtlData.map(function (ob) {                    
                    return {
                        KNT_SCO_BILL_D_ID: ob.KNT_SCO_BILL_D_ID, KNT_SCO_BILL_H_ID: ob.KNT_SCO_BILL_H_ID, KNT_SCO_CHL_RCV_H_ID: ob.KNT_SCO_CHL_RCV_H_ID,
                        ORDER_NO_LST: ob.ORDER_NO_LST, FAB_ITEM_DESC: ob.FAB_ITEM_DESC.replace('"','@'), CHALAN_NO: ob.CHALAN_NO, CHLN_QTY: ob.CHLN_QTY, DEDUCT_QTY: ob.DEDUCT_QTY,
                        NET_QTY: ob.NET_QTY, QTY_MOU_ID: 3, PROD_RATE: ob.PROD_RATE
                    };
                }));

                //alert('x');
                console.log(submitData);
                //return;

                return $http({
                    headers: { 'Authorization': 'Bearer ' + token },
                    url: '/api/Knit/KntScoBill/BatchSave',
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
                                $state.go('ScoBillProcList', { pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID });
                            }
                            else if ($scope.$parent.form.RF_ACTN_STATUS_ID == 12 && pNEXT_RF_ACTN_STATUS_ID == 15) { //Resend by Receiver
                                $state.go('ScoBillProcList', { pSCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID });
                            }
                            else {
                                $stateParams.pKNT_SCO_BILL_H_ID = res.data.PKNT_SCO_BILL_H_ID_RTN;

                                $scope.$parent.form.KNT_SCO_BILL_H_ID = res.data.PKNT_SCO_BILL_H_ID_RTN;
                                $scope.$parent.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                                $scope.$parent.form.BILL_NO = res.data.PBILL_NO_RTN;

                                //if ($scope.$parent.form.PARENT_ID) {
                                $scope.$parent.form.REV_BILL_AMT = res.data.PPREV_BILL_AMT_RTN;
                                $scope.$parent.form.CURR_BILL_AMT = res.data.PCURR_BILL_AMT_RTN;
                                $scope.$parent.form.REV_AMT = res.data.PREV_AMT_RTN;
                                $scope.$parent.form.IS_DM_CM = res.data.PIS_DM_CM_RTN;
                                //}

                                $state.go('ScoBillProcH.Dtl', { pKNT_SCO_BILL_H_ID: res['data'].PKNT_SCO_BILL_H_ID_RTN }, { notify: false });
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
            return $state.go('ScoBillProcList');
        };
        

        vm.reviseBillModal = function (scoBillHdrID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ScoBillRevisionModal.html',
                controller: 'KntScoBillReviseController',
                size: 'md',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    scoBillHdrID: function () {
                        return scoBillHdrID;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                //console.log('received');
                console.log(data);

                var vRevisionData = angular.copy(data);

                $state.go('ScoBillProcH.Dtl', { pKNT_SCO_BILL_H_ID: vRevisionData.KNT_SCO_BILL_H_ID });
                               
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
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




    }

})();
////////// End KntSciBillProcDController Controller






// Start KntScoBillReviseController Modal Controller
(function () {
    'use strict';

    angular.module('multitex.knitting').controller('KntScoBillReviseController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'scoBillHdrID', 'KnittingDataService', 'Dialog', KntScoBillReviseController]);
    function KntScoBillReviseController($modalInstance, $q, $scope, $http, $state, $filter, config, scoBillHdrID, KnittingDataService, Dialog) {

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

        $scope.BILL_DT = $scope.today;


        function activate() {
            var promise = [getBillCorrTypeList()];
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

        function getBillCorrTypeList() {
            $scope.billCorrTypeOptions = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $scope.CORR_TYPE_NAME = item.LK_DATA_NAME_EN;
                }
            };

            return $scope.billCorrTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/LookupListData/133').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
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

            //$scope.$parent.form

            var submitData = {
                KNT_SCO_BILL_H_ID: $scope.$parent.form.KNT_SCO_BILL_H_ID, BILL_NO: $scope.BILL_NO, BILL_DT: $filter('date')($scope.BILL_DT, $scope.dtFormat),
                LK_BL_CR_RSN_TYP_ID: $scope.LK_BL_CR_RSN_TYP_ID, CORR_TYPE_NAME: $scope.CORR_TYPE_NAME, OTH_RSN_DESC: $scope.OTH_RSN_DESC,
                HR_COMPANY_ID: $scope.$parent.form.HR_COMPANY_ID, SCM_SUPPLIER_ID: $scope.$parent.form.SCM_SUPPLIER_ID, REMARKS: $scope.$parent.form.REMARKS
            };

            Dialog.confirm('Do you want to revision?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + token },
                         url: '/api/knit/KntScoBill/SaveRevision',
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
                                 submitData['KNT_SCO_BILL_H_ID'] = res.data.PKNT_SCO_BILL_H_ID_RTN;
                                 submitData['BILL_NO'] = res.data.PBILL_NO_RTN;
                                 submitData['REVISION_NO'] = res.data.PREVISION_NO_RTN;
                                 submitData['RF_ACTN_STATUS_ID'] = 4;
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
// End KntScoBillReviseController Modal Controller









//=============== Start for KntScoBillProcListController List =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntScoBillProcListController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'userLavelData', KntScoBillProcListController]);
    function KntScoBillProcListController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, commonDataService, userLavelData) {

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
                { field: "BILL_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "8%" },
                { field: "BILL_NO", title: "Bill#", type: "string", width: "14%" },
                { field: "REF_BILL_NO", title: "Ref.Bill#", type: "string", width: "14%" },
                { field: "REVISION_NO", title: "Rev#", type: "number", width: "6%", filterable: false },
                { field: "OTH_RSN_DESC", title: "Rev.Descr", width: "25%", filterable: false },
                { field: "SUP_TRD_NAME_EN", title: "Supplier", width: "15%" },
                //{ field: "COMP_NAME_EN", title: "Company", width: "15%" },
                //{ field: "EVENT_NAME", title: "Status", width: "10%" },
                {
                    field: "EVENT_NAME",
                    title: "Status",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID<75'>{{dataItem.EVENT_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==75'>{{dataItem.EVENT_NAME}}</label>";
                    },
                    width: "12%"
                },
                {
                    title: "Action",
                    template: function () {                        
                        return "<button type='button' class='btn btn-xs blue' title='Edit/View' ng-click='vm.editBill(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;";
                    },
                    width: "6%"
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
                    id: "KNT_SCO_BILL_H_ID",
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
                    var url = '/api/knit/KntScoBill/GetBillList?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID || 0);

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

            $state.go('ScoBillProcList', { pSCM_SUPPLIER_ID: (vm.form.SCM_SUPPLIER_ID || 0) }, { notify: false });
        };
        
        vm.editBill = function (dataItem) {
            $state.go('ScoBillProcH.Dtl', { pKNT_SCO_BILL_H_ID: dataItem.KNT_SCO_BILL_H_ID });
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
//=============== End for KntScoBillProcListController List =================