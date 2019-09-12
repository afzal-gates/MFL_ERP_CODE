(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DCIssueController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', 'Dialog', DCIssueController]);
    function DCIssueController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.DYE_STR_REQ_H_ID ? formData : { DYE_DC_ISS_H_ID: 0, ITEM_ISS_BY: cur_user_id, ISS_REF_DT: vm.today, };
        vm.formItem = { 'QTY_MOU_ID': '3', 'CENTRAL_STK': 0, 'SUB_STK': 0 };

        //vm.form.ISS_STORE_ID = 4;
        if (!vm.form.ISS_REF_DT)
            vm.form.ISS_REF_DT = vm.today;

        if (!vm.form.ITEM_ISS_BY)
            vm.form.ITEM_ISS_BY = cur_user_id;

        vm.form.SCM_STORE_ID = formData.REQ_STORE_ID;
        if (!vm.form.DYE_DC_ISS_H_ID)
            vm.form.DYE_DC_ISS_H_ID = 0;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), GetItemCategoryList(), ItemList(), getCurrencyList(), ReceiveItemList(), GetReqSourceList(), GetReqTypeList(),
                GetCompanyList(), GetStatusList(), GetMOUList(), GetSupplierList(), GetSourceTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.ISS_REF_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ISS_REF_DT_LNopened = true;
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

        vm.getStockQty = function (preItem) {
            var count = 0;
            var REQ_STORE_ID = (vm.form.ISS_STORE_ID || 0);

            DyeingDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/GetStockInfoForBP/' + preItem.DC_ITEM_ID + '/' + REQ_STORE_ID).then(function (res) {

                preItem.CENTRAL_STK = (res.CENTRAL_STK || 0);
                preItem.SUB_STK = (res.SUB_STK || 0);
            });

        }

        vm.checkStock = function (item) {
            if ((item.SUB_STK) < item.ISS_QTY) {
                item.ISS_QTY = '';
                config.appToastMsg("MULTI-005 Issue quantity must less than stock quantity.");
            }
        }

        vm.getItemBrandName = function (e) {
            var obj = e.sender.dataItem(e.item);
            if (obj.INV_ITEM_ID > 0) {
                if (fnValidateSub2() == true) {
                    var list = _.filter(vm.DyItemList.data(), { 'INV_ITEM_ID': obj.INV_ITEM_ID });
                    vm.formItem.BRAND_NAME_EN = list[0].BRAND_NAME_EN;

                    DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalStoreTransfer/GetStockInfoForIssue/' + obj.INV_ITEM_ID + '/' + vm.form.ISS_STORE_ID).then(function (res) {

                        vm.formItem.CENTRAL_STK = res.CENTRAL_STK;
                        vm.formItem.SUB_STK = res.SUB_STK;
                    });
                }
                else
                    vm.formItem["DC_ITEM_ID"] = '';
            }
        }

        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 10;
            var PARENT_ID = 0;
            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {

                                if (res.length == 1) {
                                    vm.form.RF_ACTN_STATUS_ID = res[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = res[0].ACTN_STATUS_NAME;
                                }
                                e.success(res);
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



        vm.getRequisitionByType = function (e) {

            var obj = e.sender.dataItem(e.item);
            if (obj.RF_REQ_TYPE_ID > 0) {
                DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/SelectAll/1/500').then(function (res) {

                    var list = _.filter(res.data, function (o) { return (o.RF_REQ_TYPE_ID === obj.RF_REQ_TYPE_ID && o.RF_ACTN_STATUS_ID >= 4) });

                    vm.requisitionList = new kendo.data.DataSource({
                        data: list
                    });
                });
            }
            else
                vm.requisitionList = new kendo.data.DataSource({
                    data: []
                });
        }

        //vm.getRequisitionDtlInfo = function (e) {

        //    var obj = e.sender.dataItem(e.item);
        //    if (obj.DYE_STR_REQ_H_ID > 0) {
        //        console.log(obj);
        //        vm.form.STR_REQ_DT = obj.STR_REQ_DT;
        //        vm.form.STR_REQ_BY_NAME = obj.STR_REQ_BY_NAME;
        //        vm.form.HR_COMPANY_ID = obj.HR_COMPANY_ID;
        //        vm.form.SCM_STORE_ID = obj.REQ_STORE_ID;
        //        vm.form.SCM_SUPPLIER_ID = obj.SCM_SUPPLIER_ID;

        //        if (vm.form.RF_REQ_TYPE_ID != 10)
        //            DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/GetDCIssueRequisitionInfoDtlByID?pDYE_STR_REQ_H_ID=' + (obj.DYE_STR_REQ_H_ID || 0)).then(function (res) {
        //                vm.ReceiveItemList = res;
        //            });
        //        else if (vm.form.RF_REQ_TYPE_ID == 10)
        //            DyeingDataService.getDataByFullUrl('/api/dye/DCLoanReturn/GetDCLoanReturnInfoDtlByID?pDYE_STR_REQ_H_ID=' + (obj.DYE_STR_REQ_H_ID || 0)).then(function (res) {
        //                vm.ReceiveItemList = res;
        //            });
        //    }
        //    else {
        //        vm.ReceiveItemList = {};
        //        vm.form.STR_REQ_DT = obj.STR_REQ_DT;
        //        vm.form.STR_REQ_BY_NAME = obj.STR_REQ_BY_NAME;
        //        vm.form.HR_COMPANY_ID = obj.HR_COMPANY_ID;
        //        vm.form.SCM_STORE_ID = obj.REQ_STORE_ID;
        //        vm.form.SCM_SUPPLIER_ID = obj.SCM_SUPPLIER_ID;
        //    }
        //}

        function ReceiveItemList() {

            if ($stateParams.pDYE_STR_REQ_H_ID > 0) {
                if (vm.form.RF_REQ_TYPE_ID == 10) {
                    DyeingDataService.getDataByFullUrl('/api/dye/DCLoanReturn/GetDCLoanReturnInfoDtlByID?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (resL) {
                        vm.ReceiveItemList = resL;
                        console.log(vm.ReceiveItemList);
                    });
                }
                else {
                    DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/GetDCIssueRequisitionInfoDtlByID?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
                        vm.ReceiveItemList = res;
                        console.log(vm.ReceiveItemList);
                    });
                }
            }
            else {
                DyeingDataService.getDataByFullUrl('/api/dye/DCIssue/GetDCIssueInfoDtlByID?pDYE_DC_ISS_H_ID=' + ($stateParams.pDYE_DC_ISS_H_ID || 0)).then(function (res) {
                    vm.ReceiveItemList = res;
                });
            }


            vm.requisitionList = new kendo.data.DataSource({
                data: []
            });

        };

        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                data: []
            });
        };

        function GetItemCategoryList() {

            DyeingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {

                var list = _.filter(res, function (o) { return ((o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4 || o.PARENT_ID === 3 || o.PARENT_ID === 4)) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: list
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 3)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };


        vm.copyAllRequiredQty = function () {
            for (var i = 0; i < vm.ReceiveItemList.length; i++)
                if (parseFloat(vm.ReceiveItemList[i].SUB_STK) >= parseFloat(vm.ReceiveItemList[i].RQD_QTY - ((vm.ReceiveItemList[i].ISSUED_QTY / 1000) || 0)))
                    vm.ReceiveItemList[i].ISS_QTY = (vm.ReceiveItemList[i].RQD_QTY - ((vm.ReceiveItemList[i].ISSUED_QTY / 1000) || 0));
                else
                    vm.ReceiveItemList[i].ISS_QTY = '';
        }

        vm.copyRequiredQty = function (item) {
            if (parseFloat(item.SUB_STK) >= parseFloat(item.RQD_QTY))
                item.ISS_QTY = item.RQD_QTY;
            else
                item.ISS_QTY = '';
        }

        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {
                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.INV_ITEM_ID === parseInt(vm.formItem.DC_ITEM_ID)
                })[0];

                vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var mouUnit = _.filter(vm.mOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.QTY_MOU_ID)
                })[0];

                vm.formItem.MOU_CODE = mouUnit.MOU_CODE;

                vm.formItem.INV_ITEM_CAT_ID = vm.form.INV_ITEM_CAT_ID;
                if (vm.formItem.uid) {
                    // Update

                    var row = vm.ReceiveItemList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.DC_ITEM_ID === vm.formItem.DC_ITEM_ID && o.uid != vm.formItem.uid)
                    }).length;

                    if (count <= 1) {
                        row["DC_ITEM_ID"] = vm.formItem.DC_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                        row["BRAND_NAME_EN"] = vm.formItem.BRAND_NAME_EN;
                        row["CENTRAL_STK"] = vm.formItem.CENTRAL_STK;
                        row["SUB_STK"] = vm.formItem.SUB_STK;
                        row["MOU_CODE"] = vm.formItem.MOU_CODE;
                        row["INV_ITEM_CAT_ID"] = vm.formItem.INV_ITEM_CAT_ID;

                        row["PACK_MOU_ID"] = vm.formItem.PACK_MOU_ID;
                        row["QTY_MOU_ID"] = vm.formItem.QTY_MOU_ID;
                        row["RQD_QTY"] = vm.formItem.RQD_QTY;

                        config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.ReceiveItemList.data(), function (o) {
                        return (o.DC_ITEM_ID === vm.formItem.DC_ITEM_ID && o.RF_BRAND_ID === vm.formItem.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === vm.formItem.LK_YRN_COLR_GRP_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.ReceiveItemList.data().length + 1;
                        vm.ReceiveItemList.insert(idx, vm.formItem);

                        var gview = vm.ReceiveItemList.data();
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
            vm.form.INV_ITEM_CAT_ID = angular.copy(data.INV_ITEM_CAT_ID);
            if (vm.form.INV_ITEM_CAT_ID > 0)
                DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + vm.form.INV_ITEM_CAT_ID).then(function (res) {
                    vm.DyItemList = new kendo.data.DataSource({
                        data: res
                    });
                    vm.formItem = angular.copy(data);
                    vm.form['QTY_MOU_ID'] = angular.copy(data.QTY_MOU_ID);
                });

        }

        vm.removeItemData = function (data) {

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var index = vm.ReceiveItemList.indexOf(data);
                     vm.ReceiveItemList.splice(index, 1);

                     if (data.DYE_DC_ISS_D_ID > 0) {
                         console.log(data);

                         return DyeingDataService.saveDataByUrl(data, '/DCIssue/DeleteIndividualItem').then(function (res) {

                             if (res.success === false) {
                                 vm.errors = res.errors;
                             }
                             else {

                                 res['data'] = angular.fromJson(res.jsonStr);

                                 config.appToastMsg(res.data.PMSG);
                                 $state.go($state.current, { pDYE_DC_ISS_H_ID: data.DYE_DC_ISS_H_ID }, { inherit: false, reload: true });
                                 
                             }
                         });
                     }
                 });



        }

        vm.resetItemData = function () {
            vm.formItem = { QTY_MOU_ID: '3', CENTRAL_STK: 0, SUB_STK: 0 };
        };

        vm.reset = function () {
            $state.go($state.current, { pDYE_DC_ISS_H_ID: 0 }, { reload: true });
            //$state.go('YarnReceive', { pDYE_DC_ISS_H_ID: 0 });

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
                                //var list = _.filter(res, function (o) { return (o.RF_REQ_TYPE_ID === 5 || o.RF_REQ_TYPE_ID === 6 || o.RF_REQ_TYPE_ID === 7 || o.RF_REQ_TYPE_ID === 8) });
                                var list = _.filter(res, function (o) { return (o.RF_REQ_TYPE_ID === 8 || o.RF_REQ_TYPE_ID === 9) });
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
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=3,4,5&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 || x.LK_WH_TYPE_ID == 512 || x.LK_WH_TYPE_ID == 513 });
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
                        DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
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
                { field: "DYE_DC_ISS_H_ID", hidden: true },
                { field: "DYE_STR_REQ_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "25%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "15%" },
                { field: "CENTRAL_STK", title: "Central Stock", type: "string", width: "10%" },
                { field: "SUB_STK", title: "Sub-Stock", type: "string", width: "10%" },
                { field: "RQD_QTY", title: "Reqd Qty.", type: "string", width: "10%" },
                { field: "MOU_CODE", title: "Unit of Measure", type: "string", width: "10%" },

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
                console.log(data);
                //var result = [];
                //vm.ReceiveItemList.data().forEach(function (x) { if (!result.includes(x.INV_ITEM_CAT_ID)) result.push(x.INV_ITEM_CAT_ID) })
                //var sCat = result.toString();
                //data['INV_ITEM_CAT_LST'] = sCat;

                var iList = _.filter(vm.ReceiveItemList, function (x) { return x.ISS_QTY > 0 })

                var _isudate = $filter('date')(data.ISS_REF_DT, vm.dtFormat);
                data["ISS_REF_DT"] = _isudate;

                data["XML_ISS_D"] = DyeingDataService.xmlStringShort(iList.map(function (o) {
                    return {
                        DYE_DC_ISS_D_ID: o.DYE_DC_ISS_D_ID == null ? 0 : o.DYE_DC_ISS_D_ID,
                        DYE_DC_ISS_H_ID: o.DYE_DC_ISS_H_ID == null ? 0 : o.DYE_DC_ISS_H_ID,
                        DYE_STR_REQ_D_ID: o.DYE_STR_REQ_D_ID == null ? 0 : o.DYE_STR_REQ_D_ID,
                        DC_ITEM_ID: o.DC_ITEM_ID,
                        SL_NO: o.SL_NO == null ? 0 : o.SL_NO,
                        PACK_ISS_QTY: 0,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                        QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                        ISS_QTY_K: o.ISS_QTY_K == null ? 0 : o.ISS_QTY_K,
                        ISS_QTY_G: o.ISS_QTY_G == null ? 0 : o.ISS_QTY_G,
                        ISS_QTY_M: o.ISS_QTY_M == null ? 0 : o.ISS_QTY_M,
                        ISS_QTY: o.ISS_QTY == null ? 0 : o.ISS_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID
                        //COST_PRICE, SP_NOTE
                    }
                }));

                var id = vm.form.DYE_DC_ISS_H_ID

                if (id > 0)
                    data["SCM_STORE_ID"] = data.ISS_STORE_ID

                return DyeingDataService.saveDataByUrl(data, '/DCIssue/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pDYE_DC_ISS_H_ID: res.data.OPDYE_DC_ISS_H_ID }, { inherit: false, reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDYE_DC_ISS_H_ID) > 0) {
                                vm.form.DYE_DC_ISS_H_ID = res.data.OPDYE_DC_ISS_H_ID;
                                $state.go($state.current, { pDYE_DC_ISS_H_ID: res.data.OPDYE_DC_ISS_H_ID }, { inherit: false, reload: true });
                            }
                        }

                    }
                });
            }
        };
    }


})();

