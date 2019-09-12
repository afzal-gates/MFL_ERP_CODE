
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('ScPreTreatmentBillController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'scoBillHdrData', 'cur_user_id', '$filter', 'Dialog', ScPreTreatmentBillController]);
    function ScPreTreatmentBillController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, scoBillHdrData, cur_user_id, $filter, Dialog) {


        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        $scope.Showed = true;

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'DF_SCO_PT_BILL_H_ID';
        vm.today = new Date();

        vm.form = scoBillHdrData[key] ? scoBillHdrData : {
            BILL_DT: vm.today, DF_SCO_PT_BILL_H_ID: 0, HR_COMPANY_ID: null, SCM_SUPPLIER_ID: null, BILL_NO: '', REMARKS: '', SCO_BILL_D_XML: '', REPORT_CODE: 'RPT-3563'
        };
        $scope.form = scoBillHdrData[key] ? scoBillHdrData : {
            BILL_DT: vm.today, DF_SCO_PT_BILL_H_ID: 0, HR_COMPANY_ID: null, SCM_SUPPLIER_ID: null, BILL_NO: '', REMARKS: '', SCO_BILL_D_XML: '', REPORT_CODE: 'RPT-3563'
        };

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });


        activate();
        function activate() {
            var promise = [getSupplierList(), getCompanyList(), GetStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 35;
            var PARENT_ID = 0;
            if ($stateParams.pADDITION_FLAG > 0)
                vm.form.RF_ACTN_STATUS_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                console.log(sList);
                if (sList.length == 1) {
                    vm.form.RF_ACTN_VIEW = 0;
                    vm.form.RF_ACTN_STATUS_ID = sList[0].RF_ACTN_STATUS_ID;
                    vm.form.ACTN_STATUS_NAME = sList[0].ACTN_STATUS_NAME;
                    //alert(vm.form.ACTN_STATUS_NAME);
                }
                else {
                    vm.form.RF_ACTN_VIEW = 1;
                }
            });

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                var lst = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                                if (lst.length == 1) {
                                    vm.form.RF_ACTN_VIEW = 0;
                                    vm.form.RF_ACTN_STATUS_ID = lst[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = lst[0].ACTN_STATUS_NAME;
                                    //alert(vm.form.ACTN_STATUS_NAME);
                                }
                                else {
                                    vm.form.RF_ACTN_VIEW = 1;
                                }
                                e.success(lst);
                            });
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };
        };



        vm.billDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.billDateOpened = true;
        };

        $scope.BILL_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.BILL_DT_LNopened = true;
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

                    vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;

                    vm.delvChlnDataSource.read();
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
                        DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
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

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.delvChlnOptions = {
            height: '380px',
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
                { field: "CHALAN_NO", title: "Challan#", width: "200px", editable: false, filterable: true }

            ]
        };


        vm.delvChlnDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            pageSize: 100,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/Dye/DfScPtBill/GetChallanList4Bill';
                    url += '?pageNo=' + params.page + '&pageSize=' + params.pageSize + '&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0);

                    url += DyeingDataService.kFilterStr2QueryParam(params.filter);

                    console.log(url);

                    DyeingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "DF_SC_PT_RCV_H_ID",
                    fields: {
                        CHALAN_DT: { type: "date", filterable: false },
                        CHALAN_NO: { type: "string", filterable: false }
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


        vm.selectAll = function (v, index) {
            var data = vm.delvChlnDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            console.log(data);

            angular.forEach(data, function (val, key) {
                val['IS_SELECT'] = v;
            });
        };

        vm.includeBill = function () {
            var data = vm.delvChlnDataSource.data();
            var billData = vm.billDtlDataSource.data();

            angular.forEach(data, function (val, key) {

                if (val['IS_SELECT'] == 'Y') {
                    var chlnData = _.filter(billData, function (ob) {
                        return ob.DF_SC_PT_RCV_H_ID == val['DF_SC_PT_RCV_H_ID'];
                    });

                    if (chlnData.length < 1) {

                        angular.forEach(val['fabList'], function (val1, key1) {
                            console.log(val1);
                            val1['DF_SC_PT_RCV_H_ID'] = val1['DF_SC_PT_RCV_H_ID'];
                            val1['CHALAN_NO'] = val1['CHALAN_NO'];
                            val1['DF_PROC_TYPE_LST'] = val1['DF_PROC_TYPE_LST'];
                            val1['DF_PROC_TYPE_NAME_LIST'] = val1['DF_PROC_TYPE_NAME_LIST'];
                            val1['LK_DIA_TYPE_ID'] = val1['LK_DIA_TYPE_ID'];
                            val1['RCV_GREY_QTY'] = val1['RCV_GREY_QTY'];

                            val1['CHLN_QTY'] = val1['RCV_GREY_QTY'];
                            val1['DELV_QTY'] = val1['RCV_FAB_QTY'];
                            val1['DEDUCT_QTY'] = 0; //val1['DELV_GFAB_WT'];
                            val1['NET_QTY'] = val1['RCV_GREY_QTY'] - val1['DEDUCT_QTY'];
                            val1['PROD_RATE'] = 0;
                            val1['BILL_AMT'] = val1['PROD_RATE'] * val1['NET_QTY'];

                            vm.billDtlDataSource.insert(0, val1);
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
            console.log("SDFF");
        }

        vm.onBlurDuducQty = function (dataItem) {
            dataItem.NET_QTY = (dataItem.CHLN_QTY - dataItem.DEDUCT_QTY).toFixed(2);
            dataItem.BILL_AMT = ((dataItem.CHLN_QTY - dataItem.DEDUCT_QTY) * dataItem.PROD_RATE).toFixed(2);
            console.log("rtert");
        }


        vm.billDtlOptions = {
            height: '350px',
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
                { field: "DF_PROC_TYPE_LST", hidden: true },
                { field: "DF_SC_PT_RCV_H_ID", hidden: true },
                { field: "LK_DIA_TYPE_ID", hidden: true },

                { field: "CHALAN_NO", title: "Challan#", type: "string", width: "10%", editable: false, filterable: false },
                { field: "DIA_TYPE_NAME", title: "Dia Type", type: "string", width: "7%", editable: false, filterable: false },
                { field: "DF_PROC_TYPE_NAME_LIST", title: "DF Process List", type: "string", width: "10%", editable: false, filterable: false },
                { field: "DELV_QTY", title: "Delv. Qty", type: "number", width: "8%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },

                { field: "CHLN_QTY", title: "Grey Rcv. Qty", type: "number", width: "8%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                {
                    title: "Dedu.Qty",
                    field: "DEDUCT_QTY",
                    template: function () {
                        return "<ng-form name='frmDeduQty'><input type='number' class='form-control' name='DEDUCT_QTY' ng-model='dataItem.DEDUCT_QTY' ng-blur='vm.onBlurDuducQty(dataItem)' min='0' ng-disabled='($parent.form.RF_ACTN_STATUS_ID==75)' ng-style='(frmDeduQty.DEDUCT_QTY.$error.min)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "5%",
                    filterable: false,
                    footerTemplate: "#=kendo.toString(sum, '0.00')#"
                },
                { field: "NET_QTY", title: "Net.Qty", type: "number", width: "7%", editable: false, filterable: false, footerTemplate: "#=kendo.toString(sum, '0.00')#" },
                {
                    title: "Rate",
                    field: "PROD_RATE",
                    template: function () {
                        return "<ng-form name='frmProdRate'><input type='number' class='form-control' name='PROD_RATE' ng-model='dataItem.PROD_RATE' ng-blur='vm.onBlurDuducQty(dataItem)' min='1' required ng-style='(frmProdRate.PROD_RATE.$error.min||frmProdRate.PROD_RATE.$error.required)? vm.errstyle:\"\"' ng-disabled='$parent.form.RF_ACTN_STATUS_ID==75' /></ng-form>";
                    },
                    width: "6%",
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

                    var url = '/api/dye/DfScPtBill/GetBillDtl?pDF_SCO_PT_BILL_H_ID=' + ($stateParams.pDF_SCO_PT_BILL_H_ID || $scope.$parent.form.DF_SCO_PT_BILL_H_ID || 0);

                    console.log(url);

                    //e.success([]);

                    DyeingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            aggregate: [
                { field: "DELV_QTY", aggregate: "sum" },
                { field: "CHLN_QTY", aggregate: "sum" },
                { field: "DEDUCT_QTY", aggregate: "sum" },
                { field: "NET_QTY", aggregate: "sum" },
                { field: "BILL_AMT", aggregate: "sum" }
            ],
            schema: {
                model: {
                    id: "DF_SCO_PT_BILL_D_ID",
                    fields: {
                        DELV_QTY: { type: "number", filterable: false },
                        CHLN_QTY: { type: "number", filterable: false },
                        DEDUCT_QTY: { type: "number", filterable: false },
                        NET_QTY: { type: "number", filterable: false },
                        BILL_AMT: { type: "number", filterable: false }
                    }
                }
            }
        });


        vm.submitData = function (token, pIS_FINALIZE) {
            var submitData = angular.copy(vm.form);
            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZE == 'Y') {
                vMsg = 'Do you want to save and finalize?';
            }


            Dialog.confirm(vMsg, 'Are you sure?', ['Yes', 'No']).then(function () {

                submitData['IS_FINALIZED'] = pIS_FINALIZE;

                submitData['BILL_DT'] = $filter('date')(vm.form.BILL_DT, vm.dtFormat);
                submitData['PREV_BILL_AMT'] = parseInt(submitData['PREV_BILL_AMT'], 2);
                submitData['CURR_BILL_AMT'] = parseInt(submitData['CURR_BILL_AMT'], 2);
                submitData['REV_AMT'] = (parseInt(submitData['REV_AMT'], 2) || 0);

                var billDtlData = vm.billDtlDataSource.data();
                console.log(billDtlData);


                submitData.XML_BILL_D = DyeingDataService.xmlStringShort(billDtlData.map(function (ob) {
                    return {
                        DF_SCO_PT_BILL_D_ID: ob.DF_SCO_PT_BILL_D_ID,
                        DF_SCO_PT_BILL_H_ID: ob.DF_SCO_PT_BILL_H_ID,
                        DF_SC_PT_RCV_H_ID: ob.DF_SC_PT_RCV_H_ID,
                        DF_PROC_TYPE_LST: ob.DF_PROC_TYPE_LST == null ? ob.DF_PROC_TYPE_LST : ob.DF_PROC_TYPE_LST,
                        LK_DIA_TYPE_ID: ob.LK_DIA_TYPE_ID == null ? 1 : ob.LK_DIA_TYPE_ID,
                        CHLN_QTY: ob.CHLN_QTY,
                        DEDUCT_QTY: ob.DEDUCT_QTY,
                        NET_QTY: ob.NET_QTY,
                        QTY_MOU_ID: 3,
                        PROD_RATE: ob.PROD_RATE,
                        ACC_COA_ID: ob.ACC_COA_ID
                    };
                }));

                console.log(submitData);

                var id = vm.form.DF_SCO_PT_BILL_H_ID;

                return DyeingDataService.saveDataByUrl(submitData, '/DfScPtBill/BatchSave').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pDF_SCO_PT_BILL_H_ID: res.data.OPDF_SCO_PT_BILL_H_ID }, { inherit: false, reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDF_SCO_PT_BILL_H_ID) > 0) {
                                vm.form.DF_SCO_PT_BILL_H_ID = res.data.OPDF_SCO_PT_BILL_H_ID;
                                $state.go($state.current, { pDF_SCO_PT_BILL_H_ID: res.data.OPDF_SCO_PT_BILL_H_ID }, { inherit: false, reload: true });
                            }
                        }

                    }
                });

            });
        };

        vm.backToList = function () {
            return $state.go('ScPreTreatmentBillList');
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
                console.log(data);

                var vRevisionData = angular.copy(data);

                $state.go('ScoBillProcH.Dtl', { pDF_SCO_PT_BILL_H_ID: vRevisionData.DF_SCO_PT_BILL_H_ID });

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };


        vm.printBill = function () {
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