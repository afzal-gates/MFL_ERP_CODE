(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeChemicalStoreIssueController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', '$filter', 'cur_user_id', DyeChemicalStoreIssueController]);
    function DyeChemicalStoreIssueController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, $filter, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), GetItemCategoryList(), ItemList(), getCurrencyList(), ReceiveItemList(), GetReqSourceList(), GetReqTypeList(), GetCompanyList(), GetStatusList(), GetMOUList(), GetSupplierList(), GetSourceTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.form = formData.DYE_STR_TR_REQ_H_ID ? formData : { RF_ACTN_STATUS_ID: 6, B_DISABLE: 1 };
        vm.formItem = { 'QTY_MOU_ID': '3', 'CENTRAL_STK': 0, 'SUB_STK': 0 };
        vm.form.CHALAN_DT = '';

        vm.form.RF_ACTN_STATUS_ID = 6;
        //vm.form.B_DISABLE = 1;

        //if ($stateParams.pDYE_STR_TR_REQ_H_ID > 0)
        //    vm.form.B_DISABLE = 0;


        $scope.STR_TR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_TR_REQ_DT_LNopened = true;
        }

        $scope.BL_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.BL_DT_LNopened = true;
        }

        $scope.INVOICE_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.INVOICE_DT_LNopened = true;
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
                        var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4) });
                        e.success(list);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        vm.checkStock = function (item) {
            //alert("");
            if (item) {
                var total = (((parseFloat(item.QTY_PER_PACK) * parseFloat(item.PACK_ISS_QTY)) + (parseFloat(item.PRE_ISS_QTY) || 0)) || 0);
                //var newtotal = (parseFloat(item.QTY_PER_PACK) * parseFloat(item.PACK_ISS_QTY))+ parseFloat(item.PRE_ISS_QTY || 0);
                console.log(total);
                if (parseFloat(item.CENTRAL_STK) < total || total > parseFloat(item.RQD_QTY)) {
                    item.ISSUE_QTY = 0;
                    item.PACK_ISS_QTY = '';
                    config.appToastMsg("MULTI-005 Requisition quantity must less than central stock quantity.");
                }
                else
                    item.ISSUE_QTY = (total - (parseFloat(item.PRE_ISS_QTY) || 0));
                //item.ISSUE_QTY = $stateParams.pDYE_STR_TR_REQ_H_ID > 0 ? (total - (item.PRE_ISS_QTY || 0)) : total;
            }
        }

        vm.getItemBrandName = function (e) {
            var obj = e.sender.dataItem(e.item);

            var list = _.filter(vm.DyItemList.data(), { 'INV_ITEM_ID': obj.INV_ITEM_ID });
            vm.formItem.BRAND_NAME_EN = list[0].BRAND_NAME_EN;

            DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalStoreTransfer/GetStockInfo/' + obj.INV_ITEM_ID + '/' + vm.form.FRM_STORE_ID + '/' + vm.form.TO_STORE_ID).then(function (res) {

                vm.formItem.CENTRAL_STK = res.CENTRAL_STK;
                vm.formItem.SUB_STK = res.SUB_STK;
            });

        }

        vm.copyAllRequiredQty = function () {
            for (var i = 0; i < vm.ReceiveItemList.length; i++) {
                vm.ReceiveItemList[i].PACK_ISS_QTY = vm.ReceiveItemList[i].PACK_RQD_QTY;
                vm.checkStock(vm.ReceiveItemList[i]);
            }
        }

        vm.copyRequiredQty = function (item) {
            item.PACK_ISS_QTY = item.PACK_RQD_QTY;
            vm.checkStock(item);
        }

        function GetStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(46).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
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

        function ReceiveItemList() {

            if ($stateParams.pDYE_STR_TR_ISS_H_ID) {
                DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalStoreTransfer/GetIssueDtlByID?pDYE_STR_TR_ISS_H_ID=' + ($stateParams.pDYE_STR_TR_ISS_H_ID || 0)).then(function (res) {
                    console.log(res);
                    vm.ReceiveItemList = res;

                });
            }
            else {
                DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalStoreTransfer/GetDyeChemicalStoreTransferInfoDtlByID?pDYE_STR_TR_REQ_H_ID=' + ($stateParams.pDYE_STR_TR_REQ_H_ID || 0)).then(function (res) {
                    vm.ReceiveItemList = res;
                });
            }

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

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 3)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

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
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + vm.form.INV_ITEM_CAT_ID).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
                vm.formItem = angular.copy(data);
                vm.form['QTY_MOU_ID'] = angular.copy(data.QTY_MOU_ID);
            });

        }

        vm.removeItemData = function (data) {

            if (!data.KNT_YRN_RCV_D_ID || data.KNT_YRN_RCV_D_ID <= 0) {
                vm.ReceiveItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.ReceiveItemList.remove(data);
                 });

        }

        vm.resetItemData = function () {
            vm.formItem = { QTY_MOU_ID: '3', CENTRAL_STK: 0, SUB_STK: 0 };
        };

        vm.reset = function () {

            $state.go('YarnReceive', { pDYE_STR_TR_REQ_H_ID: 0 });

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

        function GetStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(46).then(function (res) {
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
                            DyeingDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                //DyeingDataService.LookupListData(88).then(function (res) {
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
            //                    var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 4 || x.SCM_STORE_ID == 6 });
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
                { field: "DYE_STR_TR_REQ_H_ID", hidden: true },
                { field: "DYE_STR_TR_REQ_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },

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


        vm.printChallan = function (dataItem) {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-4008';
            vm.form.DYE_STR_TR_ISS_H_ID = dataItem.DYE_STR_TR_ISS_H_ID;
            vm.form.DYE_STR_TR_REQ_H_ID = dataItem.DYE_STR_TR_REQ_H_ID;

            var url = '/api/dye/dyeReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            var params = angular.copy(vm.form);

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


        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                data["ITEM_ISS_BY"] = data.STR_REQ_TO;

                if ($stateParams.pDYE_STR_TR_ISS_H_ID > 0) {

                    data["XML_ISS_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.map(function (o) {
                        return {
                            DYE_STR_TR_ISS_D_ID: o.DYE_STR_TR_ISS_D_ID == null ? 0 : o.DYE_STR_TR_ISS_D_ID,
                            DYE_STR_TR_ISS_H_ID: o.DYE_STR_TR_ISS_H_ID == null ? 0 : o.DYE_STR_TR_ISS_H_ID,
                            DC_ITEM_ID: o.DC_ITEM_ID,
                            PACK_ISS_QTY: o.PACK_ISS_QTY,
                            QTY_PER_PACK: o.QTY_PER_PACK,
                            ISS_QTY: o.ISSUE_QTY,
                            PACK_MOU_ID: o.PACK_MOU_ID == null ? 0 : o.PACK_MOU_ID,
                            QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID
                        }
                    }));
                }
                else {

                    var list = _.filter(vm.ReceiveItemList, function (o) { return o.IS_SELECT === true && o.ISSUE_QTY > 0 });

                    if (list.length == 0) {
                        config.appToastMsg("MULTI-005 Please select at least 1 item!!!");
                        return;
                    }

                    data["XML_ISS_D"] = DyeingDataService.xmlStringShort(list.map(function (o) {
                        return {
                            DYE_STR_TR_ISS_D_ID: o.DYE_STR_TR_ISS_D_ID == null ? 0 : o.DYE_STR_TR_ISS_D_ID,
                            DYE_STR_TR_ISS_H_ID: o.DYE_STR_TR_ISS_H_ID == null ? 0 : o.DYE_STR_TR_ISS_H_ID,
                            DC_ITEM_ID: o.DC_ITEM_ID,
                            PACK_ISS_QTY: o.PACK_ISS_QTY,
                            QTY_PER_PACK: o.QTY_PER_PACK,
                            ISS_QTY: o.ISSUE_QTY,
                            PACK_MOU_ID: o.PACK_MOU_ID == null ? 0 : o.PACK_MOU_ID,
                            QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID
                        }
                    }));
                }

                var id = vm.form.DYE_STR_TR_ISS_H_ID

                var _invdate = $filter('date')(data.ISS_REF_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["ISS_REF_DT"] = _invdate;

                return DyeingDataService.saveDataByUrl(data, '/DyeChemicalStoreTransfer/SaveIssue').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            vm.form["B_DISABLE"] = 0;
                            //config.appToastMsg("MULTI-001 Updated successfully");
                            //$state.go('DyeChemicalStoreIssue', { pDYE_STR_TR_REQ_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDYE_STR_TR_ISS_H_ID) > 0) {
                                vm.form.DYE_STR_TR_ISS_H_ID = res.data.OPDYE_STR_TR_ISS_H_ID;
                                vm.form["B_DISABLE"] = 0;
                                $state.go($state.current, { pDYE_STR_TR_ISS_H_ID: res.data.OPDYE_STR_TR_ISS_H_ID });
                            }
                        }
                    }
                });
            }
        };
    }


})();

