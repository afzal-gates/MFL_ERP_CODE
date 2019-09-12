(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('ScmReceiveReturnController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$timeout', '$filter', '$modal', ScmReceiveReturnController]);
    function ScmReceiveReturnController($q, config, InventoryDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $timeout, $filter, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.SCM_STR_RET_H_ID ? formData : formData.SCM_STR_RCV_H_ID ? formData : { HR_COMPANY_ID: '1', SCM_STORE_ID: 0, DC_MRR_DT: vm.today, IS_SC_OR_LC: 'L', ITEM_RCV_BY: cur_user_id };
        vm.formItem = { QTY_MOU_ID: '3', INV_ITEM_ID: "" };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), getCurrencyList(), ReceiveItemList(), GetReqSourceList(), GetStatusList(),
                GetCompanyList(), GetMOUList(), GetPackMOUList(), GetSupplierList(), GetSourceTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        if ($stateParams.pSCM_STR_RET_H_ID > 0) {

        } else {

        }

        vm.TotalReceiveQty = function () {
            if ((vm.formItem.RET_LOOSE_QTY || 0) < (vm.formItem.QTY_PER_PACK || 0))
                if ((vm.formItem.PACK_RET_QTY > 0 || (vm.formItem.RET_LOOSE_QTY || 0) > 0) && vm.formItem.QTY_PER_PACK > 0)
                    vm.formItem.RET_QTY = parseFloat(vm.formItem.PACK_RET_QTY * vm.formItem.QTY_PER_PACK) + parseInt((vm.formItem.RET_LOOSE_QTY || 0));
                else
                    vm.formItem.RET_QTY = 0;
            else
                vm.formItem.RET_LOOSE_QTY = 0;

            vm.TotalPriceAmount();
        };


        vm.TotalReceivePOQty = function (item) {
            //var item = angular.copy(data);
            console.log(item);
            if ((parseInt(item.RET_LOOSE_QTY) || 0) < (parseInt(item.QTY_PER_PACK) || 0))
                if ((parseInt(item.PACK_RET_QTY) > 0 || (parseInt(item.RET_LOOSE_QTY) || 0) > 0) && parseInt(item.QTY_PER_PACK) > 0) {
                    item.RET_QTY = parseFloat(item.PACK_RET_QTY * item.QTY_PER_PACK) + parseInt((item.RET_LOOSE_QTY || 0));
                }
                else {
                    item.RET_QTY = 0;
                }
            else
                item.RET_LOOSE_QTY = 0;

            if (parseFloat(item.RET_QTY) > 0 && parseFloat(item.UNIT_PRICE) > 0) {

                item.TOTAL_AMT = parseFloat(item.RET_QTY) * parseFloat(item.UNIT_PRICE);
                item.TOTAL_AMT_BDT = parseFloat(item.RET_QTY) * parseFloat(item.UNIT_PRICE);
            }
            else {
                item.TOTAL_AMT = 0;
                item.TOTAL_AMT_BDT = 0;
            }

        };


        vm.TotalPOPriceAmount = function (item) {
            if (item.RET_QTY > 0 && item.UNIT_PRICE > 0) {

                item.TOTAL_AMT = item.RET_QTY * item.UNIT_PRICE;
                item.TOTAL_AMT_BDT = item.RET_QTY * parseFloat(item.UNIT_PRICE);
            }
            else {
                item.TOTAL_AMT = 0;
                item.TOTAL_AMT_BDT = 0;
            }
        };

        vm.removePOItemData = function (item) {
            var index = vm.poList.indexOf(item);
            vm.poList.splice(index, 1);
        }

        vm.CurrencyTextChange = function (e) {
            var obj = e.sender.dataItem(e.item);
            console.log(obj);
            if (obj.RF_CURRENCY_ID == 1)
                vm.form.LOC_CONV_RATE = 1;
            else
                vm.form.LOC_CONV_RATE = "";
            vm.formItem.CURRENCY_NANE = obj.CURR_NAME_EN;

        };

        vm.TotalPriceAmount = function () {
            if (vm.formItem.RCV_QTY > 0 && vm.formItem.UNIT_PRICE > 0) {

                if (vm.form.RF_CURRENCY_ID == 1)
                    vm.formItem.PCT_ADDL_PRICE = 0;

                vm.formItem.TOTAL_AMT = vm.formItem.RCV_QTY * vm.formItem.UNIT_PRICE;

                vm.formItem.TOTAL_AMT_BDT = vm.formItem.RCV_QTY * parseFloat(vm.form.LOC_CONV_RATE) * (parseFloat(vm.formItem.UNIT_PRICE) + (parseFloat(vm.formItem.UNIT_PRICE) * (vm.formItem.PCT_ADDL_PRICE / 100)));
                //vm.formItem.TOTAL_AMT_BDT = vm.formItem.TOTAL_AMT * (vm.form.LOC_CONV_RATE || 0);
            }
            else {
                vm.formItem.TOTAL_AMT = 0;
                vm.formItem.TOTAL_AMT_BDT = 0;
            }
        };

        $scope.MRR_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.MRR_DT_LNopened = true;
        }

        $scope.INVOICE_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.INVOICE_DT_LNopened = true;
        }

        $scope.RET_CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RET_CHALAN_DT_LNopened = true;
        }

        $scope.RCV_DOC_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RCV_DOC_DT_LNopened = true;
        }

        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    InventoryDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        //var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4 || o.PARENT_ID === 3 || o.PARENT_ID === 4) });
                        e.success(res);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 25;
            var PARENT_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            InventoryDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "AFZAL" })
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
                            InventoryDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                var lst = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "AFZAL" })
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
                            InventoryDataService.getUserDatalist().then(function (res) {
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

        function ReceiveItemList() {

            InventoryDataService.getDataByFullUrl('/api/inv/StoreReceive/GetStoreReceiveDtlInfoByID?pSCM_STR_RCV_H_ID=' + ($stateParams.pSCM_STR_RCV_H_ID || 0)).then(function (res) {
                vm.poList = res;
            });

            if ($stateParams.pSCM_STR_RET_H_ID > 0) {

                vm.ReceiveItemList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/inv/StoreReceive/GetStoreReceiveDtlInfoByID?pSCM_STR_RCV_H_ID=' + ($stateParams.pSCM_STR_RCV_H_ID || 0)).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }
            else {
                vm.ReceiveItemList = new kendo.data.DataSource({
                    data:[]
                });
            }

        };

        function ItemList() {
            return vm.ItemList = new kendo.data.DataSource({
                data: []
            });
        };

        function GetItemCategoryList() {

            InventoryDataService.getDataByFullUrl('/api/inv/ItemCategory/ItemCategTreeList?pSC_USER_ID=' + cur_user_id).then(function (res) {
                console.log(res);
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: res[0].items
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            InventoryDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 0)).then(function (res) {
                vm.ItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        vm.getItemCategoryByStore = function (e) {
            var itemss = e.sender.dataItem(e.item);
            InventoryDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=' + (itemss.INV_ITEM_CAT_LST || 0)).then(function (res) {
                vm.ItemList = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        vm.addToGrid = function (dataItem) {

            if (fnValidateSub() == true) {

                if (dataItem.uid) {
                    // Update

                    var row = vm.ReceiveItemList.getByUid(dataItem.uid);
                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === dataItem.INV_ITEM_ID && o.uid != dataItem.uid)
                    }).length;

                    if (count == 1) {

                        row["INV_ITEM_ID"] = dataItem.INV_ITEM_ID;
                        row["ITEM_NAME_EN"] = dataItem.ITEM_NAME_EN;
                        row["SCM_STR_RCV_H_ID"] = dataItem.SCM_STR_RCV_H_ID;
                        row["PACK_MOU_ID"] = dataItem.PACK_MOU_ID;
                        row["QTY_MOU_ID"] = dataItem.QTY_MOU_ID;
                        row["MOU_CODE"] = dataItem.MOU_CODE;
                        row["PACK_MOU_CODE"] = dataItem.PACK_MOU_CODE;
                        row["PCT_ADDL_PRICE"] = dataItem.PCT_ADDL_PRICE;

                        row["PACK_RCV_QTY"] = dataItem.PACK_RCV_QTY;
                        row["QTY_PER_PACK"] = dataItem.QTY_PER_PACK;
                        row["RCV_QTY"] = dataItem.RCV_QTY;
                        row["UNIT_PRICE"] = dataItem.UNIT_PRICE;
                        row["TOTAL_AMT"] = dataItem.TOTAL_AMT;
                        row["TOTAL_AMT_BDT"] = dataItem.TOTAL_AMT_BDT;

                        row["SP_NOTE"] = dataItem.SP_NOTE;

                        //config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === dataItem.INV_ITEM_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.ReceiveItemList.data().length + 1;
                        vm.ReceiveItemList.insert(idx, dataItem);

                        //console.log(gview);
                        //config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
        };

        vm.removeItemData = function (data) {

            if (!data.SCM_STR_RET_D_ID || data.SCM_STR_RET_D_ID <= 0) {
                vm.ReceiveItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.ReceiveItemList.remove(data);
                 });

        }

        vm.reset = function () {

            $state.go($state.current, { pSCM_STR_RCV_H_ID: 0 });
        };


        function GetSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "-- Select Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.LookupListData(84).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function GetReqSourceList() {

            return vm.reqSourceList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                            //var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 12 || x.SCM_STORE_ID == 17 });
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getCurrencyList() {

            commonDataService.getCurrencyList().then(function (res) {
                vm.currencyList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

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
                        InventoryDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {

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
                        InventoryDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetPackMOUList() {
            return vm.PackMOUList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAll').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        vm.openPOList = function () {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'poInfo.html',
                controller: function ($scope, $modalInstance, poList) {

                    $scope.today = new Date();
                    $scope.dtFormat = config.appDateFormat;

                    $scope.FROM_DATE_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.FROM_DATE_LNopened = true;
                    }
                    $scope.TO_DATE_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.TO_DATE_LNopened = true;
                    }

                    console.log(poList[0]);
                    $scope.poList = poList;

                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }


                    $scope.selectPO = function (data) {
                        $modalInstance.close(data);
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    poList: function (InventoryDataService) {
                        return InventoryDataService.getDataByFullUrl('/api/inv/StoreReceive/PendingPOListForReceive?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0));
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
                vm.form.PO_NO_IMP = data.PO_NO_IMP;
                vm.form.CM_IMP_PO_H_ID = data.CM_IMP_PO_H_ID;
                InventoryDataService.getDataByFullUrl('/api/inv/StoreReceive/GetPoDetlByID?pCM_IMP_PO_H_ID=' + data.CM_IMP_PO_H_ID).then(function (res) {
                    vm.poList = res;
                });

            }, function () {
                vm.poList = [];
                console.log('Modal dismissed at: ' + new Date());
            });

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
                { field: "RF_BRAND_ID", hidden: true },
                { field: "SCM_STR_RCV_H_ID", hidden: true },
                { field: "SCM_STR_RCV_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },
                { field: "LOC_UNIT_PRICE", hidden: true },
                { field: "PCT_ADDL_PRICE", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "15%" },
                { field: "RCV_QTY", title: "Rcv Qty", type: "string", width: "7%" },
                //{ field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "7%" },
                { field: "PACK_RET_QTY", title: "Rtn Pack", type: "string", width: "7%" },
                { field: "PACK_MOU_CODE", title: "Pack Unit", type: "string", width: "7%" },
                { field: "QTY_PER_PACK", title: "Qty/Pack", type: "string", width: "7%" },
                { field: "RET_LOOSE_QTY", title: "Loose Qty", type: "string", width: "7%" },

                { field: "RET_QTY", title: "Total Rtn Qty.", type: "string", width: "7%" },
                { field: "MOU_CODE", title: "UoM", type: "string", width: "7%" },
                { field: "UNIT_PRICE", title: "Cost Price", type: "decimal", width: "7%" },
                { field: "TOTAL_AMT_BDT", title: "TTL BDT", type: "decimal", width: "7%" },
                { field: "SP_NOTE", title: "Note", width: "12%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };

        vm.printMRR = function (dataItem) {

            vm.form.REPORT_CODE = 'RPT-9000';

            vm.form.SCM_STR_RCV_H_ID = dataItem.SCM_STR_RCV_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Inv/InvReport/PreviewReportRDLC');
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

                if (!data.IS_FINALIZED)
                    data["IS_FINALIZED"] = "N";

                var _clndate = $filter('date')(data.CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["CHALAN_DT"] = _clndate;

                var _mrrdate = $filter('date')(data.MRR_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["MRR_DT"] = _mrrdate;

                var _invdate = $filter('date')(data.INVOICE_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["INVOICE_DT"] = _invdate;

                var _Lcdate = $filter('date')(data.RCV_DOC_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["RCV_DOC_DT"] = _Lcdate;

                if (data.IS_SC_OR_LC != "L") {
                    data["XML_RCV_D"] = InventoryDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                        return {
                            SCM_STR_RCV_D_ID: o.SCM_STR_RCV_D_ID == null ? 0 : o.SCM_STR_RCV_D_ID,
                            SCM_STR_RCV_H_ID: o.SCM_STR_RCV_H_ID == null ? 0 : o.SCM_STR_RCV_H_ID,
                            INV_ITEM_ID: o.INV_ITEM_ID,
                            PACK_RCV_QTY: o.PACK_RCV_QTY,
                            PACK_MOU_ID: o.PACK_MOU_ID,
                            PCT_ADDL_PRICE: o.PCT_ADDL_PRICE,
                            COST_PRICE: parseFloat(vm.form.LOC_CONV_RATE) * (parseFloat(o.UNIT_PRICE) + (parseFloat(o.UNIT_PRICE) * (o.PCT_ADDL_PRICE / 100))),
                            CHLN_QTY: o.PO_QTY,
                            QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                            LOOSE_QTY: o.LOOSE_QTY == null ? 0 : o.LOOSE_QTY,
                            RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                            UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                            QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID,
                            SP_NOTE: o.SP_NOTE
                        }
                    }));
                }
                else {
                    data["XML_RCV_D"] = InventoryDataService.xmlStringShort(vm.poList.map(function (o) {
                        return {
                            SCM_STR_RCV_D_ID: o.SCM_STR_RCV_D_ID == null ? 0 : o.SCM_STR_RCV_D_ID,
                            SCM_STR_RCV_H_ID: o.SCM_STR_RCV_H_ID == null ? 0 : o.SCM_STR_RCV_H_ID,
                            INV_ITEM_ID: o.INV_ITEM_ID,
                            PACK_RCV_QTY: o.PACK_RCV_QTY,
                            PACK_MOU_ID: o.PACK_MOU_ID,
                            PCT_ADDL_PRICE: o.PCT_ADDL_PRICE,
                            COST_PRICE: parseFloat(vm.form.LOC_CONV_RATE) * (parseFloat(o.UNIT_PRICE) + (parseFloat(o.UNIT_PRICE) * (o.PCT_ADDL_PRICE / 100))),
                            CHLN_QTY: o.PO_QTY,

                            QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                            LOOSE_QTY: o.LOOSE_QTY == null ? 0 : o.LOOSE_QTY,
                            RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                            UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                            QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID,
                            SP_NOTE: o.SP_NOTE
                        }
                    }));
                }
                var id = vm.form.SCM_STR_RCV_H_ID

                return InventoryDataService.saveDataByUrl(data, '/StoreReceive/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pSCM_STR_RCV_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPSCM_STR_RCV_H_ID) > 0) {
                                vm.form.SCM_STR_RCV_H_ID = res.data.OPSCM_STR_RCV_H_ID;
                                $state.go($state.current, { pSCM_STR_RCV_H_ID: res.data.OPSCM_STR_RCV_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };


        vm.submitFinal = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["XML_RCV_D"] = InventoryDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        SCM_STR_RCV_D_ID: o.SCM_STR_RCV_D_ID == null ? 0 : o.SCM_STR_RCV_D_ID,
                        SCM_STR_RCV_H_ID: o.SCM_STR_RCV_H_ID == null ? 0 : o.SCM_STR_RCV_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        PACK_RCV_QTY: o.PACK_RCV_QTY,
                        PACK_MOU_ID: o.PACK_MOU_ID,

                        PCT_ADDL_PRICE: o.PCT_ADDL_PRICE,
                        COST_PRICE: parseFloat(vm.form.LOC_CONV_RATE) * (parseFloat(o.UNIT_PRICE) + (parseFloat(o.UNIT_PRICE) * (o.PCT_ADDL_PRICE / 100))),
                        CHLN_QTY: o.PO_QTY,
                        QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                        LOOSE_QTY: o.LOOSE_QTY == null ? 0 : o.LOOSE_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID,
                        LOC_UNIT_PRICE: o.LOC_UNIT_PRICE == null ? 0 : o.LOC_UNIT_PRICE,
                        SP_NOTE: o.SP_NOTE

                    }
                }));

                data["IS_FINALIZED"] = "Y";

                var _clndate = $filter('date')(data.CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["CHALAN_DT"] = _clndate;

                var _mrrdate = $filter('date')(data.MRR_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["MRR_DT"] = _mrrdate;

                var _invdate = $filter('date')(data.INVOICE_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["INVOICE_DT"] = _invdate;

                var _Lcdate = $filter('date')(data.RCV_DOC_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["RCV_DOC_DT"] = _Lcdate;

                var id = vm.form.SCM_STR_RCV_H_ID

                return InventoryDataService.saveDataByUrl(data, '/StoreReceive/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pSCM_STR_RCV_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPSCM_STR_RCV_H_ID) > 0) {
                                vm.form.SCM_STR_RCV_H_ID = res.data.OPSCM_STR_RCV_H_ID;
                                $state.go($state.current, { pSCM_STR_RCV_H_ID: res.data.OPSCM_STR_RCV_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };
    }


})();

