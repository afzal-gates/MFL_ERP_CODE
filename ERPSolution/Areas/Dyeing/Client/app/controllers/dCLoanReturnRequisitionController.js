(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCLoanReturnRequisitionController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$modal', 'Dialog', DCLoanReturnRequisitionController]);
    function DCLoanReturnRequisitionController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $modal, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.DYE_STR_REQ_H_ID ? formData : { REQ_STORE_ID: '6', STR_REQ_BY: cur_user_id, STR_REQ_TO: cur_user_id, STR_REQ_DT: vm.today, RF_ACTN_VIEW: 0, RF_REQ_TYPE_ID: 10, HR_COMPANY_ID: 1, HR_DEPARTMENT_ID: 30 };
        vm.formItem = { 'QTY_MOU_ID': '3', 'CENTRAL_STK': 0, 'SUB_STK': 0 };
        vm.form.INV_ITEM_CAT_ID = 2;
        vm.LoanItemList = {};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), GetItemCategoryList(), ItemList(), getCurrencyList(), ReceiveItemList(), GetReqSourceList(), GetReqTypeList(), GetCompanyList(), GetStatusList(), GetMOUList(), GetSupplierList(), GetSourceTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.openSupplierLoanDtl = function (SCM_SUPPLIER_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'supplierLoanDtl.html',
                controller: function ($scope, $modalInstance, LoanItemList) {

                    $scope.LoanItemList = LoanItemList;
                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    LoanItemList: function (DyeingDataService) {
                        return DyeingDataService.getDataByFullUrl('/api/dye/DCLoanReturn/GetSupplierLoanDtlByID/' + (SCM_SUPPLIER_ID || 0) + '/' + vm.form.REQ_STORE_ID);
                    }
                }
            });

        };


        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
        }

        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    DyeingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4 || o.PARENT_ID === 3 || o.PARENT_ID === 4) });
                        e.success(list);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.checkStock = function (item) {
            if (item.STOCK_QTY < item.RQD_QTY) {
                item.RQD_QTY = 0;
                config.appToastMsg("MULTI-005 Requisition quantity must less than stock quantity.");
            }
        }

        vm.getItemBrandName = function (item) {
            //var obj = e.sender.dataItem(e.item);
            if (item.ADJ_BY_ITEM_ID > 0) {

                DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalStoreTransfer/GetStockInfo/' + item.ADJ_BY_ITEM_ID + '/' + vm.form.REQ_STORE_ID + '/' + vm.form.REQ_STORE_ID).then(function (res) {

                    item.STOCK_QTY = res.SUB_STK;
                });

            }
        }

        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 1;
            var PARENT_ID = 0;
            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
            var sList = null;
            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                if (sList.length == 1) {
                    vm.form.RF_ACTN_VIEW = 0;
                    vm.form.RF_ACTN_STATUS_ID = res[0].RF_ACTN_STATUS_ID;
                    vm.form.ACTN_STATUS_NAME = res[0].ACTN_STATUS_NAME;
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
                                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                                e.success(sList);
                            });
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };

            //return vm.statusList = {
            //    optionLabel: "-- Select Status --",
            //    filter: "startswith",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
            //                    console.log(res);
            //                    e.success(res);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "ACTN_STATUS_NAME",
            //    dataValueField: "RF_ACTN_STATUS_ID"
            //};
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
                            DyeingDataService.getUserDatalist().then(function (res) {
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
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/DCLoanReturn/GetDCLoanReturnInfoDtlByID?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                data: []
            });
        };

        function GetItemCategoryList() {

            DyeingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {

                var list = _.filter(res, function (o) { return ((o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4)) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: list
                });
            });
        }

        vm.getItemByBrand = function (item) {

            var catList = "3,4";
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=' + catList).then(function (res) {
                var list = _.filter(res, function (o) { return o.BRAND_NAME_EN == item.BRAND_NAME_EN })
                item.DyItemList = new kendo.data.DataSource({
                    data: list
                });
            });
        }

        vm.getSupplierLoanDtl = function (e) {
            if (fnValidateSub3() == true) {
                var obj = e.sender.dataItem(e.item);
                DyeingDataService.getDataByFullUrl('/api/dye/DCLoanReturn/GetSupplierLoanDtlByID/' + (obj.SCM_SUPPLIER_ID || 0) + '/' + vm.form.REQ_STORE_ID).then(function (res) {
                    vm.ReceivedLoanItemList = res;
                });
            } else {
                vm.form['SCM_SUPPLIER_ID'] = '';
            }

        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 3)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

            DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAllByCat/' + (itemss.INV_ITEM_CAT_ID || '3,4')).then(function (resS) {
                vm.supplierList = new kendo.data.DataSource({
                    data: resS
                });
            });

        };

        vm.addToGrid = function (item) {

            //var item = angular.copy(dataOri);

            if (item.STOCK_QTY >= item.RQD_QTY && fnValidateSub() == true) {
                if (item.IS_ITEM_ADJ == "Y" && item.DC_ITEM_ID != item.ADJ_BY_ITEM_ID) {

                    var altItem = _.filter(item.DyItemList.data(), function (o) {
                        return o.INV_ITEM_ID === parseInt(item.ADJ_BY_ITEM_ID)
                    })[0];

                    item.ADJ_ITEM_NAME_EN = altItem.ITEM_NAME_EN;
                    item.INV_ITEM_CAT_ID = altItem.INV_ITEM_CAT_ID;
                }
                else {

                    item.ADJ_BY_ITEM_ID = '';
                    item.IS_ITEM_ADJ = 'N';
                }

                var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                    return (o.DC_ITEM_ID === item.DC_ITEM_ID && o.DYE_SUP_LN_STK_ID === item.DYE_SUP_LN_STK_ID)
                }).length;

                if (count == 0) {

                    var idx = vm.ReceiveItemList.data().length + 1;
                    vm.ReceiveItemList.insert(idx, item);

                    config.appToastMsg("Item information has been added successfully!");
                }
                else {
                    config.appToastMsg("MULTI-005 Duplicate item name exists!");
                }

            }
            else {

                item.RQD_QTY = 0;
                if (fnValidateSub() == true)
                    item.RQD_QTY = '';
            }
        };

        vm.removeItemData = function (data) {

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.ReceiveItemList.remove(data);
                 });
        }


        vm.resetItemData = function () {
            vm.formItem = { DC_ITEM_ID: '', QTY_MOU_ID: '3' };
        };

        vm.reset = function () {

            $state.go('DCLoanReturnRequisition', { pDYE_STR_REQ_H_ID: 0 });

        };


        //  DropdownList


        function GetPriorityList() {

            return vm.PriorityList = {
                optionLabel: "-- Select Priority --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(63).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };



        function GetSourceTypeList() {

            return vm.sourceTypeList = {
                optionLabel: "-- Select Source --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(84).then(function (res) {
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
                            DyeingDataService.getDataByFullUrl('/api/common/GetPayMethod').then(function (res) {
                                //DyeingDataService.getDataByFullUrl('api/common/GetPayMethod').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PAY_MTHD_NAME",
                dataValueField: "RF_PAY_MTHD_ID"
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
                            DyeingDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                var list = _.filter(res, function (o) { return (o.RF_REQ_TYPE_ID === 10) });
                                //DyeingDataService.LookupListData(88).then(function (res) {
                                e.success(list);
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
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=3,4&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 || x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
            //return vm.reqSourceList = {
            //    optionLabel: "-- Select Store --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
            //                    var list = _.filter(res, function (x) { return (x.SCM_STORE_ID == 4 || x.SCM_STORE_ID == 6) })
            //                    //DyeingDataService.LookupListData(92).then(function (res) {
            //                    e.success(list);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "STORE_NAME_EN",
            //    dataValueField: "SCM_STORE_ID"
            //};
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
                            DyeingDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

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
                        DyeingDataService.getDataByFullUrl('/Hr/Admin/HrDesignation/DepartmentListData').then(function (res) {
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
                        DyeingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
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
                        DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAllByCat/3,4').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
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
                { field: "DC_ITEM_ID", hidden: true },
                { field: "DYE_SUP_LN_STK_ID", hidden: true },
                { field: "DYE_STR_REQ_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },
                { field: "ADJ_BY_ITEM_ID", hidden: true },
                { field: "IS_ITEM_ADJ", hidden: true },

                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "25%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "15%" },
                { field: "STOCK_QTY", title: "Stock Qty", type: "string", width: "10%" },
                { field: "LN_PAY_BAL_QTY", title: "Pending Loan Qty", type: "string", width: "10%" },
                { field: "RQD_QTY", title: "Return Qty.", type: "string", width: "10%" },
                { field: "MOU_CODE", title: "Unit of Measure", type: "string", width: "10%" },
                { field: "ADJ_ITEM_NAME_EN", title: "Alternet Item", type: "string", width: "20%" },

                {
                    title: "Remove",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a>',
                    width: "10%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var result = [];
                vm.ReceiveItemList.data().forEach(function (x) { if (!result.includes(x.INV_ITEM_CAT_ID)) result.push(x.INV_ITEM_CAT_ID) })
                var sCat = result.toString();
                data['INV_ITEM_CAT_LST'] = sCat;

                data["XML_REQ_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        DYE_STR_REQ_D_ID: o.DYE_STR_REQ_D_ID == null ? 0 : o.DYE_STR_REQ_D_ID,
                        DYE_STR_REQ_H_ID: o.DYE_STR_REQ_H_ID == null ? 0 : o.DYE_STR_REQ_H_ID,
                        DC_ITEM_ID: o.DC_ITEM_ID,
                        IS_ITEM_ADJ: o.IS_ITEM_ADJ,
                        ADJ_BY_ITEM_ID: o.ADJ_BY_ITEM_ID == 0 ? null : o.ADJ_BY_ITEM_ID,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 13 : o.PACK_MOU_ID,
                        RQD_QTY_K: o.RQD_QTY_K == null ? 0 : o.RQD_QTY_K,
                        RQD_QTY_G: o.RQD_QTY_G == null ? 0 : o.RQD_QTY_G,
                        RQD_QTY_M: o.RQD_QTY_M == null ? 0 : o.RQD_QTY_M,
                        RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID
                    }
                }));

                var id = vm.form.DYE_STR_REQ_H_ID

                return DyeingDataService.saveDataByUrl(data, '/DCLoanReturn/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            //config.appToastMsg("MULTI-001 Yarn receive has been updated successfully");
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pDYE_STR_REQ_H_ID: res.data.OPDYE_STR_REQ_H_ID }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDYE_STR_REQ_H_ID) > 0) {
                                vm.form.DYE_STR_REQ_H_ID = res.data.OPDYE_STR_REQ_H_ID;
                                $state.go($state.current, { pDYE_STR_REQ_H_ID: res.data.OPDYE_STR_REQ_H_ID });
                            }
                        }

                    }
                });
            }
        };
    }


})();

