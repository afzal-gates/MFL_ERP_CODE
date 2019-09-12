////////// Start KntYdProcLossAdjHController Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntYdProcLossAdjHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', 'kntYdProcLossAdjHdrData', KntYdProcLossAdjHController]);
    function KntYdProcLossAdjHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog, kntYdProcLossAdjHdrData) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
        
        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'KNT_YD_PL_ADJ_H_ID';
        vm.today = new Date();

        vm.form = kntYdProcLossAdjHdrData[key] ? kntYdProcLossAdjHdrData : {
            ADJ_DT: vm.today, KNT_YD_PL_ADJ_H_ID: 0, HR_COMPANY_ID: null, SCM_SUPPLIER_ID: null, PL_ADJ_MEMO_NO: '', PL_ADJ_DTL_XML: ''
        };
        $scope.form = kntYdProcLossAdjHdrData[key] ? kntYdProcLossAdjHdrData : {
            ADJ_DT: vm.today, KNT_YD_PL_ADJ_H_ID: 0, HR_COMPANY_ID: null, SCM_SUPPLIER_ID: null, PL_ADJ_MEMO_NO: '', PL_ADJ_DTL_XML: ''
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

        vm.adjDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.adjDateOpened = true;
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

                    $scope.ydProgramDataSource.read();
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
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=556').then(function (res) {
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

        $scope.ydProgramDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            pageSize: 100,
            schema: {
                data: "data",
                total: "total",
                model: {
                    fields: {
                        PRG_ISS_DT: { type: "date" },
                        DELV_TGT_DT: { type: "date" }
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KnitYdProgram/getProgamList';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pIS_PL_ADJ=N&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0);

                    url += config.kFilterStr2QueryParam(params.filter);
                   
                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }            
        });



        $scope.ydProgramOptions = {
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
                    //headerTemplate: "<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.selectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Select?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Select?' ng-model='dataItem.IS_SELECT' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-click='vm.progSelect(dataItem)' >";
                    },
                    width: "50px"
                },
                { field: "PRG_REF_NO", title: "Y/D Program #", width: "100px", filterable: false },
                //{ field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "10%", filterable: false },
                { field: "FAB_PROD_CAT_SNAME", title: "Type", width: "70px", filterable: false },
                { field: "ORDER_NO", title: "Order (Style)", width: "100px", filterable: false },
                //{ field: "REVISION_NO", title: "Rev#", width: "5%", filterable: false },
                //{ field: "SUP_TRD_NAME_EN", title: "Supplier Name", width: "20%", filterable: false },
                { field: "PRG_ISS_DT", title: "Issue", width: "80%", format: "{0:dd-MMM-yyyy}" }
            ]
        };

        
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
////////// End KntYdProcLossAdjHController Controller









////////// Start KntYdProcLossAdjDController Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntYdProcLossAdjDController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', '$modal', 'KnittingDataService', 'Dialog', KntYdProcLossAdjDController]);
    function KntYdProcLossAdjDController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, $modal, KnittingDataService, Dialog) {

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

      
        vm.progSelect = function (dataItem) {
            var data = $scope.$parent.ydProgramDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            //console.log(data);

            angular.forEach(data, function (val, key) {
                
                if (dataItem['uid'] == val['uid']) {
                    val['IS_SELECT'] = "Y";
                }
                else {
                    val['IS_SELECT'] = "N";
                }
            });
        };
        
        vm.includeBill = function () {
            var data = $scope.$parent.ydProgramDataSource.data();
            var plAdjData = vm.plAdjDtlDataSource.data();

            angular.forEach(data, function (val, key) {

                if (val['IS_SELECT'] == 'Y') {
                    var progData = _.filter(plAdjData, function (ob) {
                        return ob.KNT_YD_PRG_H_ID == val['KNT_YD_PRG_H_ID'];
                    });

                    if (progData.length < 1) {
                        $scope.$parent.form['KNT_YD_PRG_H_ID'] = val['KNT_YD_PRG_H_ID'];

                        angular.forEach(val['colYrnDtl'], function (val1, key1) {
                            vm.plAdjDtlDataSource.insert(0, val1);
                        });
                    }
                }
            });
        }
        
        vm.removeRow = function (dataItem) {
            var data = angular.copy(dataItem);

            var dataList = vm.plAdjDtlDataSource.data();
            var length = dataList.length;

            var vMsg = 'Do you want to remove items of the program ref# ' + dataItem.PRG_REF_NO;
            
            console.log(dataList);

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                var item, i;
                for (i = length - 1; i >= 0; i--) {

                    item = dataList[i];
                    if (data.PRG_REF_NO == item['PRG_REF_NO']) {
                        vm.plAdjDtlDataSource.remove(item);
                    }

                }             
            });            
        };

               

        vm.plAdjDtlOptions = {
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
                { field: "PRG_REF_NO", title: "YD Program#", type: "string", width: "10%", editable: false, filterable: false },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%", editable: false, filterable: false },
                { field: "YARN_DETAIL", title: "Yarn", type: "string", width: "21%", editable: false, filterable: false },                
                {
                    title: "PL Qty",
                    field: "PL_ADJ_QTY",
                    template: function () {
                        return "<ng-form name='frmDeduQty'><input type='number' class='form-control' name='PL_ADJ_QTY' ng-model='dataItem.PL_ADJ_QTY' min='0' ng-disabled='(dataItem.ACTION_USER_TYPE==\"SENDER\" && $parent.form.RF_ACTN_STATUS_ID==19)||$parent.form.RF_ACTN_STATUS_ID==87' ng-style='frmDeduQty.PL_ADJ_QTY.$error.min || frmDeduQty.PL_ADJ_QTY.$error.required? vm.errstyle:\"\"' required /></ng-form>";
                    },
                    width: "8%",
                    filterable: false,
                    footerTemplate: "#=kendo.toString(sum, '0.00')#"
                },                
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='(dataItem.ACTION_USER_TYPE==\"SENDER\" && $parent.form.RF_ACTN_STATUS_ID==19)||$parent.form.RF_ACTN_STATUS_ID==87' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "3%"
                }
            ]            
        };


        vm.plAdjDtlDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    
                    var url = '/api/Knit/KnitYdProgram/GetPlAdjDtl?pKNT_YD_PL_ADJ_H_ID=' + ($stateParams.pKNT_YD_PL_ADJ_H_ID || $scope.$parent.form.KNT_YD_PL_ADJ_H_ID || 0);
                    
                    console.log(url);

                    //e.success([]);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            aggregate: [
                { field: "PL_ADJ_QTY", aggregate: "sum" }
            ],            
            schema: {                
                model: {
                    id: "KNT_YD_PL_ADJ_D_ID",
                    fields: {                        
                        PL_ADJ_QTY: { type: "number", filterable: false }
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
                submitData['RF_ACTN_STATUS_ID'] = pNEXT_RF_ACTN_STATUS_ID;                                
                submitData['ADJ_DT'] = $filter('date')($scope.$parent.form.ADJ_DT, vm.dtFormat);
                
                var plAdjDtlData = vm.plAdjDtlDataSource.data();
                console.log(plAdjDtlData);


                submitData.PL_ADJ_DTL_XML = KnittingDataService.xmlStringShort(plAdjDtlData.map(function (ob) {
                    return {
                        KNT_YD_PL_ADJ_D_ID: ob.KNT_YD_PL_ADJ_D_ID, KNT_YD_PL_ADJ_H_ID: ob.KNT_YD_PL_ADJ_H_ID, YRN_COLOR_ID: ob.YRN_COLOR_ID,
                        YARN_ITEM_ID: ob.YARN_ITEM_ID, RF_BRAND_ID: ob.RF_BRAND_ID, KNT_YRN_LOT_ID: ob.KNT_YRN_LOT_ID, PL_ADJ_QTY: ob.PL_ADJ_QTY
                    };
                }));

                console.log(submitData);
                //return submitData;

                return $http({
                    headers: { 'Authorization': 'Bearer ' + token },
                    url: '/api/Knit/KnitYdProgram/PlAdjBatchSave',
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

                            $stateParams.pKNT_YD_PL_ADJ_H_ID = res.data.PKNT_YD_PL_ADJ_H_ID_RTN;

                            $scope.$parent.form.KNT_YD_PL_ADJ_H_ID = res.data.PKNT_YD_PL_ADJ_H_ID_RTN;
                            $scope.$parent.form.RF_ACTN_STATUS_ID = res.data.PRF_ACTN_STATUS_ID_RTN;
                            $scope.$parent.form.PL_ADJ_MEMO_NO = res.data.PPL_ADJ_MEMO_NO_RTN;
                           
                            vm.plAdjDtlDataSource.read();
                            $state.go('KnitYdProcLossAdjH.Dtl', { pKNT_YD_PL_ADJ_H_ID: res['data'].PKNT_YD_PL_ADJ_H_ID_RTN }, { notify: false });
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.backToList = function () {
            return $state.go('KnitYdProcLossAdjList');
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
////////// End KntYdProcLossAdjDController Controller







//=============== Start for KntYdProcLossAdjListController List =================
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntYdProcLossAdjListController', ['$q', 'config', 'Dialog', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'userLavelData', KntYdProcLossAdjListController]);
    function KntYdProcLossAdjListController($q, config, Dialog, KnittingDataService, $stateParams, $state, $scope, commonDataService, userLavelData) {

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
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=556').then(function (res) {
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
                { field: "ADJ_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "PL_ADJ_MEMO_NO", title: "Adj Memo#", type: "string", width: "15%" },                
                { field: "PRG_REF_NO", title: "Program#", type: "string", width: "15%", filterable: false },
                { field: "REMARKS", title: "Remarks", width: "30%", filterable: false },                
                {
                    field: "EVENT_NAME",
                    title: "Status",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID<=19'>{{dataItem.EVENT_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==87'>{{dataItem.EVENT_NAME}}</label>";
                    },
                    width: "15%"
                },
                {
                    title: "Action",
                    template: function () {                        
                        return "<button type='button' class='btn btn-xs blue' title='Edit/View' ng-click='vm.editBill(dataItem)'><i class='fa fa-edit'></i></button>";
                    },
                    width: "5%"
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
                    id: "KNT_YD_PL_ADJ_H_ID",
                    fields: {
                        ADJ_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KnitYdProgram/GetPlAdjList?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID || 0);

                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    });
                }
            },
            sort: [{ field: 'ADJ_DT', dir: 'desc' }]
        });


        vm.getBillList = function () {
            vm.sciBillGridDataSource.read();

            $state.go('KnitYdProcLossAdjList', { pSCM_SUPPLIER_ID: (vm.form.SCM_SUPPLIER_ID || 0) }, { notify: false });
        };
        
        vm.editBill = function (dataItem) {
            $state.go('KnitYdProcLossAdjH.Dtl', { pKNT_YD_PL_ADJ_H_ID: dataItem.KNT_YD_PL_ADJ_H_ID });
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
//=============== End for KntYdProcLossAdjListController List =================