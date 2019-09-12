(function () {
    'use strict';
    angular.module('multitex.knitting').controller('ComparativeStatementController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'formData', 'Dialog', '$http', '$modal', 'commonDataService', 'cur_user_id', '$filter', '$window', ComparativeStatementController]);
    function ComparativeStatementController($q, config, KnittingDataService, $stateParams, $state, $scope, formData, Dialog, $http, $modal, commonDataService, cur_user_id, $filter, $window) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.SCM_PURC_REQ_H_ID ? formData : { PURC_REQ_BY: cur_user_id, PURC_REQ_TO: cur_user_id };
        vm.formItem = {};
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), getCurrencyList(), RequisitionItemList(), GetReqSourceList(),
                GetReqTypeList(), GetCompanyList(), getPlanList(),
                GetYarnColorGroupList(), GetMOUList(), GetSupplierList(), importSupplier(), localSupplier()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.goDashboard = function () {
            $window.location = "/";
        }

        vm.selectOrderList = function (e) {

            vm.OrderList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/Order/OrderHdrDataList?pMC_STYLE_H_EXT_ID=' + (vm.formItem.MC_STYLE_H_EXT_ID || 0)).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }


        function GetBuyerList() {
            vm.buyerList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        vm.selectStyleList = function (e) {
            var item = e.sender.dataItem(e.item);
            vm.form.MC_BUYER_ID = item.MC_BUYER_ID;
            vm.styleList.read();
        }

        function getPlanList() {

            return vm.planList = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KnitYarnReq/GetPurchasePlan';

                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function getUserData() {
            return vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getUserDatalist().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    vm.IsChange = false;
                },

                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

            //$("#customers").kendoDropDownList(vm.userList);
        }

        $scope.PURC_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PURC_REQ_DT_LNopened = true;
        }

        $scope.TARGET_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TARGET_DT_LNopened = true;
        }


        $scope.DELIVERY_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.DELIVERY_DT_LNopened = true;
        }


        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        e.success(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.addDuplicateItem = function (indx, item) {
            Dialog.confirm('Are you sure to add duplicate this yarn?', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     var obj = angular.copy(item);
                     var idx = indx + 1;
                     console.log(obj);
                     console.log(idx);
                     vm.RequisitionItemList.splice(idx, "0", obj);
                 });
        }

        vm.checkSupplier = function (idx, obj, id) {
            if (obj.SCM_SUPPLIER_ID > 0) {
                if (id == 1) {
                    for (var i = 0; i < vm.importSupplierList.length; i++) {
                        if (idx != i) {
                            var item = vm.importSupplierList[i];
                            if (obj.SCM_SUPPLIER_ID == item.SCM_SUPPLIER_ID) {
                                obj.SCM_SUPPLIER_ID = '';
                                config.appToastMsg("MTEX-005 Duplicate Supplier Selection!");
                            }
                        }
                    }

                }
                else {
                    for (var i = 0; i < vm.localSupplierList.length; i++) {
                        if (idx != i) {
                            var item = vm.localSupplierList[i];
                            if (obj.SCM_SUPPLIER_ID == item.SCM_SUPPLIER_ID) {
                                obj.SCM_SUPPLIER_ID = '';
                                config.appToastMsg("MTEX-005 Duplicate Supplier Selection!");
                            }
                        }

                    }
                }
            }
        }

        vm.confirmSupplier = function (obj, item, type) {

            if (obj.QUOTE_RATE) {

                if (type == 0) {
                    for (var i = 0; i < item.IMPORT_SUP_LST.length; i++)
                        item.IMPORT_SUP_LST[i].IS_FINALIZED = "N";

                    for (var i = 0; i < item.LOCAL_SUP_LST.length; i++)
                        if (item.LOCAL_SUP_LST[i].SCM_SUPPLIER_ID != obj.SCM_SUPPLIER_ID)
                            item.LOCAL_SUP_LST[i].IS_FINALIZED = "N";
                }
                else {
                    for (var i = 0; i < item.LOCAL_SUP_LST.length; i++)
                        item.LOCAL_SUP_LST[i].IS_FINALIZED = "N";

                    for (var i = 0; i < item.IMPORT_SUP_LST.length; i++)
                        if (item.IMPORT_SUP_LST[i].SCM_SUPPLIER_ID != obj.SCM_SUPPLIER_ID)
                            item.IMPORT_SUP_LST[i].IS_FINALIZED = "N";
                }

                if (obj.IS_FINALIZED == "Y") {
                    item.CONF_RATE = obj.QUOTE_RATE;
                    item.SCM_SUPPLIER_ID = obj.SCM_SUPPLIER_ID;
                }
                else {
                    item.CONF_RATE = '';
                    item.SCM_SUPPLIER_ID = '';
                }
            }
            else
                obj.IS_FINALIZED = "N";
        }

        vm.setImportSupplier = function (item, idx, id) {
            if (id == 1) {
                for (var i = 0; i < vm.importSupplierList.length; i++) {
                    if (i == idx) {
                        if (vm.importSupplierList[i].SCM_SUPPLIER_ID > 0)
                            item.SCM_SUPPLIER_ID = vm.importSupplierList[i].SCM_SUPPLIER_ID;
                        else {
                            item.QUOTE_RATE = '';
                            config.appToastMsg("MTEX-005 Please select supplier first!");
                        }
                    }
                }
            }
            else {
                for (var i = 0; i < vm.localSupplierList.length; i++) {
                    if (i == idx) {
                        if (vm.localSupplierList[i].SCM_SUPPLIER_ID > 0)
                            item.SCM_SUPPLIER_ID = vm.localSupplierList[i].SCM_SUPPLIER_ID;
                        else {
                            item.QUOTE_RATE = '';
                            config.appToastMsg("MTEX-005 Please select supplier first!");
                        }
                    }
                }
            }
        }

        vm.changeLocalSupplier = function (idx, item) {
            if (item.SCM_SUPPLIER_ID > 0)
                for (var i = 0; i < vm.RequisitionItemList.length; i++) {
                    vm.RequisitionItemList[i].LOCAL_SUP_LST[idx].SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                }
        }

        vm.addLocalSupplier = function () {
            var idx = vm.localSupplierList.length;
            var item = { SCM_SUPPLIER_ID: 0, QUOTE_RATE: '' }
            if (vm.form.LOCAL_SUP == 1) {
                for (var i = 0; i < vm.RequisitionItemList.length; i++) {
                    vm.RequisitionItemList[i].LOCAL_SUP_LST = [];
                }
            }

            vm.localSupplierList.splice(idx, "0", { SCM_SUPPLIER_ID: 0, QUOTE_RATE: '' });

            for (var i = 0; i < vm.RequisitionItemList.length; i++) {
                if (vm.form.LOCAL_SUP == 1) {
                    var obj = angular.copy(item);
                    vm.RequisitionItemList[i].LOCAL_SUP_LST.splice(idx - 1, "0", obj);
                }
                var obj2 = angular.copy(item);
                vm.RequisitionItemList[i].LOCAL_SUP_LST.splice(idx, "0", obj2);
            }

            vm.form.LOCAL_SUP = idx + 1;

        }

        vm.addImportSupplier = function () {
            var idx = vm.importSupplierList.length;
            var item = { SCM_SUPPLIER_ID: 0, QUOTE_RATE: '' }

            //var first = angular.copy(vm.RequisitionItemList[0]);

            if (vm.form.IMPORT_SUP == 1) {
                for (var i = 0; i < vm.RequisitionItemList.length; i++) {
                    vm.RequisitionItemList[i].IMPORT_SUP_LST = [];
                }
            }

            vm.importSupplierList.splice(idx, "0", { SCM_SUPPLIER_ID: 0, QUOTE_RATE: '' });

            for (var i = 0; i < vm.RequisitionItemList.length; i++) {
                if (vm.form.IMPORT_SUP == 1) {
                    var obj = angular.copy(item);
                    vm.RequisitionItemList[i].IMPORT_SUP_LST.splice(idx - 1, "0", obj);
                }
                var obj2 = angular.copy(item);
                vm.RequisitionItemList[i].IMPORT_SUP_LST.splice(idx, "0", obj2);
            }

            vm.form.IMPORT_SUP = idx + 1;

        }

        function RequisitionItemList() {

            var url = '/api/knit/KnitYarnReq/ReqDtlByID?pSCM_PURC_REQ_H_ID=' + ($stateParams.pSCM_PURC_REQ_H_ID || 0);

            if (formData.IS_ORDER_WISE) {
                if (formData.IS_ORDER_WISE == "Y")
                    url = '/api/knit/KnitYarnReq/YarnReqDtlForPlan?pSCM_PURC_REQ_H_ID=' + ($stateParams.pSCM_PURC_REQ_H_ID || 0);
            }

            KnittingDataService.getDataByFullUrl(url).then(function (res) {
                vm.RequisitionItemList = res.list;

                var cnt = _.filter(res.Col, function (x) { return x.SCM_SUPPLIER_ID == 0 });

                if (cnt.length > 0) {

                    vm.form.LOCAL_SUP = cnt.length;
                    vm.form.IMPORT_SUP = cnt.length;

                    vm.localSupplierList = angular.copy(cnt);
                    vm.importSupplierList = angular.copy(cnt);
                }
                else {
                    var sup_l = _.filter(res.Col, function (x) { return x.IS_LOCAL == "L" });
                    var sup_f = _.filter(res.Col, function (x) { return x.IS_LOCAL == "F" });
                    if (sup_l.length > 0) {
                        vm.localSupplierList = sup_l;
                        vm.form.LOCAL_SUP = sup_l.length;
                    }
                    else {
                        vm.form.LOCAL_SUP = 1;
                        var item = angular.copy({ SCM_PURC_REQ_D_ID: 0, SCM_SUPPLIER_ID: 0, QUOTE_RATE: '' });
                        vm.localSupplierList = [item];

                    }
                    if (sup_f.length > 0) {

                        vm.form.IMPORT_SUP = sup_f.length;
                        vm.importSupplierList = sup_f;
                    }
                    else {
                        vm.form.IMPORT_SUP = 1;
                        var item = angular.copy({ SCM_PURC_REQ_D_ID: 0, SCM_SUPPLIER_ID: 0, QUOTE_RATE: '' });
                        vm.importSupplierList = [item];

                    }

                    console.log("Afzal");
                    console.log(vm.localSupplierList);
                    console.log(vm.importSupplierList);
                    console.log("Afzal");

                }

                //vm.localSupplierList = [{ SCM_SUPPLIER_ID: 0, QUOTE_RATE: '' }];
                //vm.importSupplierList = [{ SCM_SUPPLIER_ID: 0, QUOTE_RATE: '' }];

                //var item = { SCM_SUPPLIER_ID: 0, QUOTE_RATE: '' }
                //for (var i = 0; i < res.list.length; i++) {
                //    var obj = angular.copy(item);
                //    var obj2 = angular.copy(item);
                //    vm.RequisitionItemList[i].LOCAL_SUP_LST = [obj];
                //    vm.RequisitionItemList[i].IMPORT_SUP_LST = [obj2];
                //}
            });

        };

        function GetItemCategoryList() {

            KnittingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var filter = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 2) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: filter
                });
            });

            KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (vm.form.INV_ITEM_CAT_ID || 2)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 2)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        function GetPriorityList() {

            return vm.PriorityList = {
                optionLabel: "-- Select Priority --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(63).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetPaymentMethodList() {

            return vm.paymentMethodList = {
                optionLabel: "-- Select Payment Method --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetPayMethod').then(function (res) {
                                //KnittingDataService.getDataByFullUrl('api/common/GetPayMethod').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_MTHD_NAME",
                dataValueField: "RF_PAY_MTHD_ID"
            };
        };

        function GetSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "-- Select Source Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetReqSRC').then(function (res) {
                                //KnittingDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_SRC_NAME",
                dataValueField: "RF_REQ_SRC_ID"
            };
        };

        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                //KnittingDataService.LookupListData(88).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetReqSourceList() {
            return vm.reqSourceList = {
                optionLabel: "-- Select Req Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetReqSRC').then(function (res) {
                                //KnittingDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_SRC_NAME",
                dataValueField: "RF_REQ_SRC_ID"
            };
        };

        function getCurrencyList() {
            return vm.currencyList = {
                optionLabel: "-- Select Currency --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getCurrencyList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "CURR_NAME_EN",
                dataValueField: "RF_CURRENCY_ID"
            };
        };

        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };

        vm.departmentList = {
            optionLabel: "--Select Dept--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {

                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        function GetMOUList() {
            return vm.mOUList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetYarnColorGroupList() {
            return vm.yarnColorGroupList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.LookupListData(93).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetSupplierList() {
            return vm.supplierList = {
                optionLabel: "Supplier",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "SUP_SNAME",
                dataValueField: "SCM_SUPPLIER_ID"
            };

            //vm.showSplash = true;
            //KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
            //    allSupplier(res);
            //    var list_L = _.filter(res, function (x) { return x.IS_LOCAL == "L" })
            //    var list_F = _.filter(res, function (x) { return x.IS_LOCAL == "F" })
            //    localSupplier(list_L);
            //    importSupplier(list_F);

            //});

        };

        function allSupplier(data) {
            return vm.supplierList = {
                optionLabel: "Supplier",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "SUP_SNAME",
                dataValueField: "SCM_SUPPLIER_ID"
            };
        }


        function localSupplier() {
            return vm.SupplierLocalList = {
                optionLabel: "Supplier",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                                var list_L = _.filter(res, function (x) { return x.IS_LOCAL == "L" })
                                e.success(list_L);
                            });
                        }
                    }
                },
                dataTextField: "SUP_SNAME",
                dataValueField: "SCM_SUPPLIER_ID"
            };
        }

        function importSupplier() {
            vm.showSplash = false;
            return vm.SupplierImportList = {
                optionLabel: "Supplier",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                                var list_F = _.filter(res, function (x) { return x.IS_LOCAL == "F" })
                                e.success(list_F);
                            });
                        }
                    }
                },
                dataTextField: "SUP_SNAME",
                dataValueField: "SCM_SUPPLIER_ID"
            };
        }

        function getTnaStatusList() {
            return vm.TnaStatusList = {
                optionLabel: "-- Select TNA Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/TnaTaskStatus/SelectApprovRejectStatus?pMC_TNA_TASK_ID=46').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "EVENT_NAME",
                dataValueField: "MC_TNA_TASK_STATUS_ID"
            };
        };

        function getFiscalYearData() {
            return vm.FiscalYearData = {
                optionLabel: "-- Select Fiscal Year --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/hr/hrleavetrans/FiscalYearDataAll').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "FY_NAME_EN",
                dataValueField: "RF_FISCAL_YEAR_ID"
            };
        };

        function convertDate(old) {
            var _isudate = $filter('date')(old, 'yyyy-MM-ddTHH:mm:ss');
            return _isudate;
        }


        vm.printRequisition = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3581';

            vm.form.SCM_PURC_REQ_H_ID = dataItem.SCM_PURC_REQ_H_ID;
            vm.form.IS_EXCEL_FORMAT = "Y";

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
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

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var result = [];
                console.log("Supplier");
                console.log(vm.localSupplierList);

                var list = _.filter(vm.localSupplierList, function (x) { return x.SCM_SUPPLIER_ID == -1 })

                for (var j = 0; j < vm.localSupplierList.length; j++)
                    for (var i = 0; i < vm.RequisitionItemList.length; i++) {
                        if (vm.localSupplierList[j].SCM_SUPPLIER_ID > 0) {
                            var idx = list.length;
                            vm.RequisitionItemList[i].LOCAL_SUP_LST[j].SCM_SUPPLIER_ID = vm.localSupplierList[j].SCM_SUPPLIER_ID;
                            vm.RequisitionItemList[i].LOCAL_SUP_LST[j].QUOTE_QTY = vm.RequisitionItemList[i].RQD_QTY;
                            vm.RequisitionItemList[i].LOCAL_SUP_LST[j].SCM_PURC_REQ_D_ID = vm.RequisitionItemList[i].SCM_PURC_REQ_D_ID;
                            vm.RequisitionItemList[i].LOCAL_SUP_LST[j].LK_LOC_SRC_TYPE_ID = 452;

                            list.splice(idx, "0", angular.copy(vm.RequisitionItemList[i].LOCAL_SUP_LST[j]));

                        }
                    }
                for (var j = 0; j < vm.importSupplierList.length; j++)
                    for (var i = 0; i < vm.RequisitionItemList.length; i++) {
                        if (vm.importSupplierList[j].SCM_SUPPLIER_ID > 0) {
                            var idx = list.length;
                            vm.RequisitionItemList[i].IMPORT_SUP_LST[j].SCM_SUPPLIER_ID = vm.importSupplierList[j].SCM_SUPPLIER_ID;
                            vm.RequisitionItemList[i].IMPORT_SUP_LST[j].QUOTE_QTY = vm.RequisitionItemList[i].RQD_QTY;
                            vm.RequisitionItemList[i].IMPORT_SUP_LST[j].SCM_PURC_REQ_D_ID = vm.RequisitionItemList[i].SCM_PURC_REQ_D_ID;
                            vm.RequisitionItemList[i].IMPORT_SUP_LST[j].LK_LOC_SRC_TYPE_ID = 453;


                            list.splice(idx, "0", angular.copy(vm.RequisitionItemList[i].IMPORT_SUP_LST[j]));
                        }
                    }

                console.log("List");
                console.log(list);
                //return;
                data["XML_PRICE_D"] = KnittingDataService.xmlStringShort(list.map(function (o) {
                    return {
                        SCM_PURC_YRN_PRICE_ID: o.SCM_PURC_YRN_PRICE_ID == null ? 0 : o.SCM_PURC_YRN_PRICE_ID,
                        SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID == null ? 0 : o.SCM_PURC_REQ_D_ID,
                        SCM_SUPPLIER_ID: o.SCM_SUPPLIER_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID == 0 ? null : 1,
                        QUOTE_QTY: o.QUOTE_QTY == null ? 1 : o.QUOTE_QTY,
                        QUOTE_RATE: o.QUOTE_RATE,
                        LK_LOC_SRC_TYPE_ID: o.LK_LOC_SRC_TYPE_ID,
                        IS_FINALIZED: o.IS_FINALIZED == null ? "N" : o.IS_FINALIZED,
                    }
                }));

                //var pricelist = _.filter(vm.RequisitionItemList, function (x) { return x.SCM_SUPPLIER_ID > 0 });
                //console.log(pricelist);

                data["XML_PRICE_CONF"] = KnittingDataService.xmlStringShort(vm.RequisitionItemList.map(function (o) {
                    return {
                        SCM_PURC_REQ_D_YRN_ID: o.SCM_PURC_REQ_D_YRN_ID == null ? 0 : o.SCM_PURC_REQ_D_YRN_ID,
                        SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID == null ? 0 : o.SCM_PURC_REQ_D_ID,
                        CONF_SUPPLIER_ID: (o.SCM_SUPPLIER_ID || 0),
                        CONF_BRAND_ID: o.RF_BRAND_ID == 0 ? 1 : o.RF_BRAND_ID == "" ? 1 : o.RF_BRAND_ID,
                        CONF_RATE: o.CONF_RATE,
                        CONF_QTY: o.CONF_QTY == null ? o.RQD_QTY : o.CONF_QTY,
                        IS_DELETED: o.IS_DELETED == null ? "N" : o.IS_DELETED,
                        REMARKS: o.REMARKS
                    }
                }));

                return KnittingDataService.saveDataByUrl(data, '/KnitYarnReq/SaveCS').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        config.appToastMsg(res.data.PMSG);
                        if (res.data.OPSCM_PURC_REQ_H_ID)
                            $state.go($state.current, { pSCM_PURC_REQ_H_ID: res.data.OPSCM_PURC_REQ_H_ID }, { reload: true });

                    }
                });
            }
        };
    }


})();