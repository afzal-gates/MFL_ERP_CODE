(function () {
    'use strict';
    angular.module('multitex.purchase').controller('FundRequisitionController', ['$q', 'config', 'PurchaseDataService', '$stateParams', '$state', '$scope', 'formData', 'Dialog', '$http', '$modal', '$filter', 'commonDataService', 'cur_user_id', FundRequisitionController]);
    function FundRequisitionController($q, config, PurchaseDataService, $stateParams, $state, $scope, formData, Dialog, $http, $modal, $filter, commonDataService, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.EDIT = 0;

        vm.form = formData.SCM_FUND_REQ_H_ID ? formData : { 'REQ_BY': cur_user_id, 'REQ_TO': cur_user_id, 'RF_REQ_TYPE_ID': 1, 'HR_DEPARTMENT_ID': 46 };

        vm.formItem = { 'QTY_MOU_ID': 3 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), getCurrencyList(), RequisitionList(), GetReqSourceList(),
                GetReqTypeList(), GetCompanyList(), GetSourceTypeList(), GetStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        if (formData['SCM_FUND_REQ_H_ID']) {
            formData['INV_ITEM_CAT_LST'] = formData.INV_ITEM_CAT_LST ? formData.INV_ITEM_CAT_LST.split(',') : [];
            vm.form = formData;
        } else {
            vm.form = { INV_ITEM_CAT_ID_LST: [], 'PURC_REQ_BY': cur_user_id, 'PREPARED_BY': cur_user_id, 'PURC_REQ_TO': cur_user_id, 'RF_REQ_TYPE_ID': 1, 'HR_DEPARTMENT_ID': 46 }

        }


        if ($stateParams.pSCM_FUND_REQ_H_ID > 0) {
            vm.importOrderList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/Purchase/fund/GetFundDtlItemByID?pSCM_FUND_REQ_H_ID=' + $stateParams.pSCM_FUND_REQ_H_ID).then(function (res) {
                            var ttl_po = 0;
                            for (var i = 0; i < res.length; i++) {
                                var obj = res[i];
                                ttl_po = ttl_po + (obj.RQD_QTY * obj.MKT_RATE);
                                //res["TTL_VALUE"] = (obj.RQD_QTY * obj.MKT_RATE)
                            }
                            //vm.form.PI_NET_AMT = ttl_po;

                            e.success(res);
                        });
                    }
                }
            });


        }
        else {
            vm.importOrderList = new kendo.data.DataSource({
                data: []
            });
        }

        vm.checkBalance = function (item) {
            if (parseFloat(item.BAL_QTY) < parseFloat(item.RQD_QTY)) {
                item.RQD_QTY = 0;
                config.appToastMsg("MULTI-005 Required Qty Exceed!");
            }
        }

        vm.clearGrid = function () {
            vm.EDIT = 0;
            vm.RequisitionItemList = []
        }

        vm.AddToGrid = function () {

            if (fnValidateSub() == true) {

                for (var i = 0; i < vm.RequisitionItemList.length; i++) {

                    var idx = vm.importOrderList.data().length + 1;

                    var obj = vm.RequisitionItemList[i];
                    if (obj.IS_SELECT == true) {

                        var lst = _.filter(vm.importOrderList.data(), function (x) { return x.SCM_PURC_REQ_D_ID == obj.SCM_PURC_REQ_D_ID })
                        if (lst.length == 0) {
                            obj["TTL_VALUE"] = obj.RQD_QTY * obj.MKT_RATE;
                            obj["RQD_QTY"] = obj.RQD_QTY;
                            obj["MKT_RATE"] = obj.MKT_RATE;

                            vm.importOrderList.insert(idx, obj);
                        }
                        else {

                            config.appToastMsg("MULTI-005 Duplicate Records!");
                            return;
                        }
                    }
                }

                vm.EDIT = 0;
                var ttl_po = 0;
                for (var i = 0; i < vm.importOrderList.data().length; i++) {
                    var obj = vm.importOrderList.data()[i];
                    ttl_po = ttl_po + (obj.RQD_QTY * obj.UNIT_PRICE);
                }
                vm.form.PI_NET_AMT = ttl_po;
            }
            //vm.CalculateDiscount();
        }


        vm.updateToGrid = function () {

            for (var i = 0; i < vm.RequisitionItemList.length; i++) {

                var obj = vm.RequisitionItemList[i];
                var row = vm.importOrderList.getByUid(obj.uid);
                //var row = _.filter(vm.importOrderList.data(), function (x) { return x.uid == obj.uid });
                row["SCM_PURC_REQ_D_ID"] = obj.SCM_PURC_REQ_D_ID;
                row["SCM_PURC_REQ_H_ID"] = obj.SCM_PURC_REQ_H_ID;
                row["SCM_FUND_REQ_D_ITM_ID"] = obj.SCM_FUND_REQ_D_ITM_ID;
                row["INV_ITEM_ID"] = obj.INV_ITEM_ID;
                row["RF_BRAND_ID"] = obj.RF_BRAND_ID;
                row["QTY_MOU_ID"] = obj.QTY_MOU_ID;

                row["ITEM_NAME_EN"] = obj.ITEM_NAME_EN;
                row["BRAND_NAME_EN"] = obj.BRAND_NAME_EN;
                row["RQD_QTY"] = obj.RQD_QTY;
                row["MKT_RATE"] = obj.MKT_RATE;
                var total = obj.RQD_QTY * obj.MKT_RATE;
                row["TTL_VALUE"] = total.toFixed(2);
            }

            var ttl_po = 0;
            for (var i = 0; i < vm.importOrderList.data().length; i++) {
                var obj = vm.importOrderList.data()[i];
                ttl_po = ttl_po + (obj.RQD_QTY * obj.MKT_RATE);
            }
            vm.form.PI_NET_AMT = ttl_po;

            vm.EDIT = 0;
            vm.RequisitionItemList = [];
            //vm.CalculateDiscount();
        }


        vm.editData = function (dataItem) {
            var item = angular.copy(dataItem);
            item["IS_SELECT"] = true;
            item["RQD_QTY"] = item.RQD_QTY;
            item["MKT_RATE"] = item.MKT_RATE;
            item["IS_SELECT"] = true;
            vm.EDIT = 1;
            vm.RequisitionItemList = [item];
        }

        vm.removeData = function (data) {

            Dialog.confirm('Finalizing "' + data.ORDER_NO + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var List = _.filter(vm.importOrderList.data(), function (x) { return x.SCM_FUND_REQ_D_ITM_ID == data.SCM_FUND_REQ_D_ITM_ID });

                     for (var i = 0; i < List.length; i++) {
                         var item = List[i];
                         vm.importOrderList.remove(item);
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

                { field: "SCM_FUND_REQ_D_ITM_ID", hidden: true },
                { field: "INV_ITEM_ID", hidden: true },
                { field: "SCM_PURC_REQ_D_ID", hidden: true },
                { field: "SCM_PURC_REQ_H_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Description of Goods", width: "15%" },
                { field: "BRAND_NAME_EN", title: "Brand", width: "8%" },
                { field: "RQD_QTY", title: "Quantity", width: "6%" },
                { field: "MKT_RATE", title: "Unit Price", width: "7%" },
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
            var RF_ACTN_TYPE_ID = 28;
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

        $scope.REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REQ_DT_LNopened = true;
        }

        function RequisitionList() {

            var url = '/api/Purchase/fund/PendingReqForFund';

            if (formData.SCM_FUND_REQ_H_ID)
                url = '/api/Purchase/fund/GetFundDtlByID?pSCM_FUND_REQ_H_ID=' + formData.SCM_FUND_REQ_H_ID;

            PurchaseDataService.getDataByFullUrl(url).then(function (res) {
                vm.RequisitionList = res;
            });
        };


        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {
                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.INV_ITEM_ID === parseInt(vm.formItem.INV_ITEM_ID)
                })[0];

                vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var itemB = _.filter(vm.categoryBrandList.data(), function (o) {
                    return o.RF_BRAND_ID === parseInt(vm.formItem.RF_BRAND_ID)
                })[0];

                vm.formItem.BRAND_NAME_EN = itemB.BRAND_NAME_EN;

                var mouUnit = _.filter(vm.mOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.QTY_MOU_ID)
                })[0];

                vm.formItem.MOU_CODE = mouUnit.MOU_CODE;
                vm.formItem.INV_ITEM_CAT_ID = vm.form.INV_ITEM_CAT_ID;

                if (vm.formItem.uid) {
                    // Update

                    var row = vm.RequisitionList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.RequisitionList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.uid != vm.formItem.uid)
                    }).length;

                    if (count <= 1) {
                        row["INV_ITEM_CAT_ID"] = vm.formItem.INV_ITEM_CAT_ID;
                        row["INV_ITEM_ID"] = vm.formItem.INV_ITEM_ID;
                        row["RF_BRAND_ID"] = vm.formItem.RF_BRAND_ID;
                        row["SCM_FUND_REQ_H_ID"] = vm.formItem.SCM_FUND_REQ_H_ID;
                        row["SCM_PURC_REQ_D_ID"] = vm.formItem.SCM_PURC_REQ_D_ID;
                        row["QTY_MOU_ID"] = vm.formItem.QTY_MOU_ID;

                        row["LK_YRN_COLR_GRP_ID"] = vm.formItem.LK_YRN_COLR_GRP_ID;
                        row["LK_YRN_COLR_GRP_NAME"] = vm.formItem.LK_YRN_COLR_GRP_NAME;
                        row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                        row["LK_LOC_SRC_TYPE_ID"] = vm.formItem.LK_LOC_SRC_TYPE_ID;
                        row["LK_PRIORITY_ID"] = vm.formItem.LK_PRIORITY_ID;
                        row["BUFR_ALOC_QTY"] = vm.formItem.BUFR_ALOC_QTY;
                        row["LC_PIPLINE_QTY"] = vm.formItem.LC_PIPLINE_QTY;
                        row["MOU_CODE"] = vm.formItem.MOU_CODE;
                        row["RQD_QTY"] = vm.formItem.RQD_QTY;
                        row["LAST_PUR_RATE"] = vm.formItem.LAST_PUR_RATE;
                        row["SP_NOTE"] = vm.formItem.SP_NOTE;

                        config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.RequisitionList.data(), function (o) {
                        return o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID
                    }).length;

                    if (count == 0) {

                        var idx = vm.RequisitionList.data().length + 1;
                        vm.RequisitionList.insert(idx, vm.formItem);

                        var gview = vm.RequisitionList.data();
                        //console.log(gview);
                        config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
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
            $state.go($state.current, { pSCM_FUND_REQ_H_ID: 0 });

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

        function GetReqSourceList() {
            return vm.reqSourceList = {
                optionLabel: "-- Select Req Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            PurchaseDataService.getDataByFullUrl('/api/common/GetReqSRC').then(function (res) {
                                //PurchaseDataService.LookupListData(92).then(function (res) {
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

            vm.form.REPORT_CODE = 'RPT-8002';
            vm.form.SCM_FUND_REQ_H_ID = dataItem.SCM_FUND_REQ_H_ID;

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


        vm.gridOptionsItem = {
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
            height: '300px',
            scrollable: true,
            columns: [
                { field: "INV_ITEM_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },
                { field: "RF_BRAND_ID", hidden: true },
                { field: "LK_PRIORITY_ID", hidden: true },
                { field: "LK_LOC_SRC_TYPE_ID", hidden: true },
                { field: "SCM_FUND_REQ_H_ID", hidden: true },
                { field: "SCM_PURC_REQ_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Item Name", width: "20%", headerTemplate: "<b>Item Name</b>" },
                { field: "BRAND_NAME_EN", title: "Brand", width: "10%", headerTemplate: "<b>Brand</b>" },
                { field: "BUFR_ALOC_QTY", title: "Stock Qty.", width: "10%", headerTemplate: "<b>Stock Buffer Qty.</b>" },
                { field: "LC_PIPLINE_QTY", title: "LC Qty.", width: "5%", headerTemplate: "<b>LC Qty.</b>" },
                { field: "LOAN_QTY", title: "Loan Qty.", width: "5%", headerTemplate: "<b>Loan Qty.</b>" },
                { field: "RQD_QTY", title: "Reqd. Qty.", width: "5%", headerTemplate: "<b>Reqd. Qty.</b>" },
                { field: "MOU_CODE", title: "Unit", width: "5%", headerTemplate: "<b>Unit</b>" },
                { field: "LAST_PUR_RATE", title: "Rate/Kg", width: "5%", headerTemplate: "<b>Rate/Kg</b>" },
                { field: "TOTAL_VALUE", title: "Total Price", width: "5%", headerTemplate: "<b>Total Amt</b>" },
                { field: "SP_NOTE", title: "Note", width: "15%", headerTemplate: "<b>Note</b>" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data['RF_CURRENCY_ID'] = 1;

                var _isudate = $filter('date')(data.REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["REQ_DT"] = _isudate;

                var list = _.filter(vm.RequisitionList, function (x) { return x.APRV_AMT > 0 });
                if (data.SCM_FUND_REQ_H_ID > 0 && data.ACTN_ROLE_FLAG == 'RA')
                    data["XML_FUND_D"] = PurchaseDataService.xmlStringShort(list.map(function (o) {
                        return {
                            SCM_FUND_REQ_D_ID: o.SCM_FUND_REQ_D_ID == null ? 0 : o.SCM_FUND_REQ_D_ID,
                            SCM_FUND_REQ_H_ID: o.SCM_FUND_REQ_H_ID == null ? 0 : o.SCM_FUND_REQ_H_ID,
                            HR_DEPARTMENT_ID: o.HR_DEPARTMENT_ID,
                            SCM_PURC_REQ_H_ID: o.SCM_PURC_REQ_H_ID,
                            RQD_AMT: o.RQD_AMT == null ? 0 : o.RQD_AMT,
                            APRV_AMT: o.APRV_AMT == null ? 0 : o.APRV_AMT,
                            SP_NOTE: o.SP_NOTE,
                        }
                    }));
                else
                    data["XML_FUND_ITEM"] = PurchaseDataService.xmlStringShort(vm.importOrderList.data().map(function (o) {
                        return {
                            SCM_FUND_REQ_D_ITM_ID: o.SCM_FUND_REQ_D_ITM_ID == null ? 0 : o.SCM_FUND_REQ_D_ITM_ID,
                            SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID == null ? 0 : o.SCM_PURC_REQ_D_ID,
                            SCM_PURC_REQ_H_ID: o.SCM_PURC_REQ_H_ID == null ? 0 : o.SCM_PURC_REQ_H_ID,
                            INV_ITEM_ID: o.INV_ITEM_ID,
                            RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                            QTY_MOU_ID: o.QTY_MOU_ID,
                            MKT_RATE: o.MKT_RATE
                        }
                    }));

                var id = vm.form.SCM_FUND_REQ_H_ID

                return PurchaseDataService.saveDataByUrl(data, '/fund/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg("MULTI-001 Requisition information has been updated successfully");
                            $state.go($state.current, { pSCM_FUND_REQ_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg('MULTI-001' + res.data.PMSG);
                            if (res.data.OPSCM_FUND_REQ_H_ID)
                                $state.go($state.current, { pSCM_FUND_REQ_H_ID: res.data.OPSCM_FUND_REQ_H_ID }, { reload: true });
                        }
                    }
                });
            }
        };
    }


})();