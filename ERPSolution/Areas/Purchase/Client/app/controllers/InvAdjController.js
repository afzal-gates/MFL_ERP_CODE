
(function () {
    'use strict';
    angular.module('multitex.purchase').controller('InvAdjController', ['$q', 'config', 'PurchaseDataService', '$stateParams', '$state', '$scope', 'formData', 'Dialog', '$http', '$modal', '$filter', 'commonDataService', 'cur_user_id', InvAdjController]);
    function InvAdjController($q, config, PurchaseDataService, $stateParams, $state, $scope, formData, Dialog, $http, $modal, $filter, commonDataService, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.EDIT = 0;

        vm.form = formData.SCM_STR_ITM_ADJ_H_ID ? formData : { 'REQ_BY': cur_user_id, 'REQ_TO': cur_user_id, 'RF_REQ_TYPE_ID': 42, 'HR_DEPARTMENT_ID': 46 };

        vm.formItem = { 'QTY_MOU_ID': 3 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), RequisitionList(), GetReqTypeList(), GetCompanyList(), GetStatusList(), GetStoreList(), GetItemCategoryList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        if ($stateParams.pSCM_STR_ITM_ADJ_H_ID > 0) {
            vm.adjReqList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/Purchase/InvAdj/GetInvAdjDtlByID?pSCM_STR_ITM_ADJ_H_ID=' + $stateParams.pSCM_STR_ITM_ADJ_H_ID).then(function (res) {
                            var ttl_po = 0;
                            for (var i = 0; i < res.length; i++) {
                                var obj = res[i];
                                ttl_po = ttl_po + (obj.ADJ_QTY * obj.COST_PRICE);
                            }
                            e.success(res);
                        });
                    }
                }
            });
        }
        else {
            vm.adjReqList = new kendo.data.DataSource({
                data: []
            });
        }


        if (formData['SCM_STR_ITM_ADJ_H_ID']) {
            formData['INV_ITEM_CAT_LST'] = formData.INV_ITEM_CAT_LST ? formData.INV_ITEM_CAT_LST.split(',') : [];
            vm.form = formData;
        } else {
            vm.form = { INV_ITEM_CAT_ID_LST: [], 'PURC_REQ_BY': cur_user_id, 'PREPARED_BY': cur_user_id, 'PURC_REQ_TO': cur_user_id, 'RF_REQ_TYPE_ID': 42, 'HR_DEPARTMENT_ID': 46 }

        }

        vm.loadItemStock = function () {

            if (fnValidateSub() == true) {
                var pINV_ITEM_CAT_LST = !vm.form.INV_ITEM_CAT_LST ? '' : vm.form.INV_ITEM_CAT_LST.join(',');
                vm.showSplash = true;

                var url = '/api/Purchase/InvAdj/GetDcItemStockByID?pINV_ITEM_ID=' + (vm.form.INV_ITEM_ID || null) + '&pSCM_STORE_ID=' + (vm.form.SCM_STORE_ID || null) + '&pINV_ITEM_CAT_LST=' + (pINV_ITEM_CAT_LST || null);

                PurchaseDataService.getDataByFullUrl(url).then(function (res) {
                    vm.RequisitionItemList = res;
                    vm.showSplash = false;
                });
            }
        }

        vm.getItemDataByCategory = function () {


            var pINV_ITEM_CAT_LST = !vm.form.INV_ITEM_CAT_LST ? '' : vm.form.INV_ITEM_CAT_LST.join(',');
            //var itemss = e.sender.dataItem(e.item);
            PurchaseDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=' + (pINV_ITEM_CAT_LST || 0)).then(function (res) {
                vm.ItemList = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        vm.calculateBalance = function (item) {
            item.ADJ_QTY = item.STOCK_QTY - item.ADJ_STOCK_QTY;
        }

        vm.totalCost = function (item) {
            item.TTL_VALUE = Math.abs(item.ADJ_QTY * item.COST_PRICE)
        }

        vm.clearGrid = function () {
            vm.EDIT = 0;
            vm.RequisitionItemList = []
        }



        vm.AddToGrid = function () {

            if (fnValidateSub() == true) {

                for (var i = 0; i < vm.RequisitionItemList.length; i++) {

                    var idx = vm.adjReqList.data().length + 1;

                    var obj = vm.RequisitionItemList[i];
                    if (obj.IS_SELECT == true) {

                        var lst = _.filter(vm.adjReqList.data(), function (x) { return x.SCM_PURC_REQ_D_ID == obj.SCM_PURC_REQ_D_ID })
                        if (lst.length == 0) {
                            obj["TTL_VALUE"] = obj.ADJ_QTY * obj.COST_PRICE;
                            obj["ADJ_QTY"] = obj.ADJ_QTY;
                            obj["COST_PRICE"] = obj.COST_PRICE;

                            vm.adjReqList.insert(idx, obj);
                        }
                        else {

                            config.appToastMsg("MULTI-005 Duplicate Records!");
                            return;
                        }
                    }
                }

                vm.EDIT = 0;
                var ttl_po = 0;
                for (var i = 0; i < vm.adjReqList.data().length; i++) {
                    var obj = vm.adjReqList.data()[i];
                    ttl_po = ttl_po + (obj.ADJ_QTY * obj.UNIT_PRICE);
                }
                vm.form.PI_NET_AMT = ttl_po;
            }
            //vm.CalculateDiscount();
        }


        vm.updateToGrid = function () {

            for (var i = 0; i < vm.RequisitionItemList.length; i++) {

                var obj = vm.RequisitionItemList[i];
                var row = vm.adjReqList.getByUid(obj.uid);
                //var row = _.filter(vm.adjReqList.data(), function (x) { return x.uid == obj.uid });
                row["SCM_PURC_REQ_D_ID"] = obj.SCM_PURC_REQ_D_ID;
                row["SCM_PURC_REQ_H_ID"] = obj.SCM_PURC_REQ_H_ID;
                row["SCM_FUND_REQ_D_ITM_ID"] = obj.SCM_FUND_REQ_D_ITM_ID;
                row["INV_ITEM_ID"] = obj.INV_ITEM_ID;
                row["RF_BRAND_ID"] = obj.RF_BRAND_ID;
                row["QTY_MOU_ID"] = obj.QTY_MOU_ID;

                row["ITEM_NAME_EN"] = obj.ITEM_NAME_EN;
                row["BRAND_NAME_EN"] = obj.BRAND_NAME_EN;
                row["STOCK_QTY"] = obj.STOCK_QTY;
                row["ADJ_STOCK_QTY"] = obj.ADJ_STOCK_QTY;
                row["ADJ_QTY"] = obj.ADJ_QTY;
                row["COST_PRICE"] = obj.COST_PRICE;
                var total = obj.ADJ_QTY * obj.COST_PRICE;
                row["TTL_VALUE"] = total.toFixed(2);
            }

            var ttl_po = 0;
            for (var i = 0; i < vm.adjReqList.data().length; i++) {
                var obj = vm.adjReqList.data()[i];
                ttl_po = ttl_po + (obj.ADJ_QTY * obj.COST_PRICE);
            }
            vm.form.PI_NET_AMT = ttl_po;

            vm.EDIT = 0;
            vm.RequisitionItemList = [];
            //vm.CalculateDiscount();
        }


        vm.editData = function (dataItem) {
            var item = angular.copy(dataItem);
            item["IS_SELECT"] = true;
            item["ADJ_QTY"] = item.ADJ_QTY;
            item["COST_PRICE"] = item.COST_PRICE;
            item["IS_SELECT"] = true;
            vm.EDIT = 1;
            vm.RequisitionItemList = [item];
        }

        vm.removeData = function (data) {

            Dialog.confirm('Finalizing "' + data.ORDER_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var List = _.filter(vm.adjReqList.data(), function (x) { return x.SCM_FUND_REQ_D_ITM_ID == data.SCM_FUND_REQ_D_ITM_ID });

                     for (var i = 0; i < List.length; i++) {
                         var item = List[i];
                         vm.adjReqList.remove(item);
                     }

                 });
        }

        vm.gridOptions = {
            pageable: false,
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
            height: '400px',
            scrollable: true,
            columns: [

                { field: "SCM_STR_ITM_ADJ_D_ID", hidden: true },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "SCM_STR_ITM_ADJ_H_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Description of Goods", width: "15%" },
                //{ field: "BRAND_NAME_EN", title: "Brand", width: "8%" },
                { field: "STOCK_QTY", title: "Stock Quantity", width: "6%" },
                { field: "ADJ_STOCK_QTY", title: "Current Stock Qty", width: "6%" },
                { field: "ADJ_QTY", title: "Adjust Quantity", width: "6%" },
                { field: "COST_PRICE", title: "Unit Price", width: "7%" },
                { field: "TTL_VALUE", title: "Total Price", width: "8%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a title="Edit" ng-click="vm.editData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
        };

        vm.showRequisition = function (item) {
            vm.showSplash = true;
            console.log(item);
            var url = '/api/knit/KnitYarnReq/ReqDtlByID?pSCM_PURC_REQ_H_ID=' + (item.SCM_PURC_REQ_H_ID || 0);

            PurchaseDataService.getDataByFullUrl(url).then(function (res) {
                vm.RequisitionItemList = res;
                vm.showSplash = false;
            });
        }


        vm.SelectAll = function (IS_SELECT) {
            //alert(IS_SELECT);
            if (IS_SELECT) {
                for (var i = 0; i < vm.RequisitionItemList.length; i++)
                    vm.RequisitionItemList[i].IS_SELECT = true;
            }
            else {
                for (var i = 0; i < vm.RequisitionItemList.length; i++)
                    vm.RequisitionItemList[i].IS_SELECT = false;
            }
        }

        vm.selectItemInfo = function (e) {

            var item = e.sender.dataItem(e.item);
            console.log(item);
            vm.formItem.PURCH_PRICE = item.PURCH_PRICE;

        }

        vm.clearBuyerList = function () {
            vm.form.MC_BUYER_LST = '';
        }


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 38;
            var PARENT_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            PurchaseDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                //var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                console.log(res);
                if (res.length == 1) {
                    vm.form.RF_ACTN_VIEW = 0;
                    vm.form.RF_ACTN_STATUS_ID = res[0].RF_ACTN_STATUS_ID;
                    vm.form.ACTN_STATUS_NAME = res[0].ACTN_STATUS_NAME;
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
                            PurchaseDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                var lst = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                                if (lst.length == 1) {
                                    vm.form.RF_ACTN_VIEW = 0;
                                    vm.form.RF_ACTN_STATUS_ID = lst[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = lst[0].ACTN_STATUS_NAME;
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
                            PurchaseDataService.getUserDatalist().then(function (res) {
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


        function GetStoreList() {
            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=3,4&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 || x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        }

        $scope.ADJ_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ADJ_REQ_DT_LNopened = true;
        }

        function RequisitionList() {

            var url = '/api/Purchase/fund/PendingReqForFund';

            if (formData.SCM_STR_ITM_ADJ_H_ID)
                url = '/api/Purchase/fund/GetFundDtlByID?pSCM_STR_ITM_ADJ_H_ID=' + formData.SCM_STR_ITM_ADJ_H_ID;

            PurchaseDataService.getDataByFullUrl(url).then(function (res) {
                vm.RequisitionList = res;
            });
        };


        vm.editItemData = function (data) {
            vm.formItem = angular.copy(data);
            //vm.form['QTY_MOU_ID'] = angular.copy(data.QTY_MOU_ID);
        }

        vm.removeItemData = function (data) {

            Dialog.confirm('Removing "' + data.PURC_REQ_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var index = vm.RequisitionList.indexOf(data);
                     vm.RequisitionList.splice(index, 1);
                 });

        }

        vm.resetItemData = function () {
            vm.formItem = { 'QTY_MOU_ID': 3 };
            vm.formItem['INV_ITEM_ID'] = '';
            vm.formItem['LK_YRN_COLR_GRP_ID'] = '';
            vm.formItem['RF_BRAND_ID'] = '';
        };

        vm.reset = function () {
            $state.go($state.current, { pSCM_STR_ITM_ADJ_H_ID: 0 });

        };

        function GetSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "--Source--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.LookupListData(84).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
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
                            PurchaseDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                //PurchaseDataService.LookupListData(88).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetItemCategoryList() {
            return vm.categoryList = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };

        }

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
                            PurchaseDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

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
                        PurchaseDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {

                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "DEPARTMENT_NAME_EN",
            dataValueField: "HR_DEPARTMENT_ID"
        };

        vm.printRequisition = function (dataItem) {

            vm.form.REPORT_CODE = 'RPT-8004';
            vm.form.SCM_STR_ITM_ADJ_H_ID = dataItem.SCM_STR_ITM_ADJ_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Pur/PurReport/PreviewReportRDLC');
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

                var _isudate = $filter('date')(data.ADJ_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["ADJ_REQ_DT"] = _isudate;

                data["INV_ITEM_CAT_LST"] = !vm.form.INV_ITEM_CAT_LST ? '' : vm.form.INV_ITEM_CAT_LST.join(',');

                data["XML_REQ_D"] = PurchaseDataService.xmlStringShort(vm.adjReqList.data().map(function (o) {
                    return {
                        SCM_STR_ITM_ADJ_D_ID: o.SCM_STR_ITM_ADJ_D_ID == null ? 0 : o.SCM_STR_ITM_ADJ_D_ID,
                        SCM_STR_ITM_ADJ_H_ID: o.SCM_STR_ITM_ADJ_H_ID == null ? 0 : o.SCM_STR_ITM_ADJ_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        ADJ_QTY: o.ADJ_QTY,
                        STOCK_QTY: o.STOCK_QTY == null ? 0 : o.STOCK_QTY,
                        ADJ_STOCK_QTY: o.ADJ_STOCK_QTY == null ? 0 : o.ADJ_STOCK_QTY,
                        QTY_MOU_ID: (o.QTY_MOU_ID || 3),
                        COST_PRICE: o.COST_PRICE,
                    }
                }));

                var id = vm.form.SCM_STR_ITM_ADJ_H_ID

                return PurchaseDataService.saveDataByUrl(data, '/InvAdj/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pSCM_STR_ITM_ADJ_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (res.data.OPSCM_STR_ITM_ADJ_H_ID)
                                $state.go($state.current, { pSCM_STR_ITM_ADJ_H_ID: res.data.OPSCM_STR_ITM_ADJ_H_ID }, { reload: true });
                        }
                    }
                });
            }
        };
    }


})();