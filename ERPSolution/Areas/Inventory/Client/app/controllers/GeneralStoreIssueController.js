(function () {
    'use strict';
    angular.module('multitex.inventory').controller('GeneralStoreIssueController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', 'Dialog', GeneralStoreIssueController]);
    function GeneralStoreIssueController($q, config, InventoryDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.SCM_STR_GEN_REQ_H_ID ? formData : {
            SCM_STR_GEN_REQ_H_ID: 0, SCM_STR_GEN_ISS_H_ID: 0, ITEM_ISS_BY: cur_user_id,
            STR_REQ_DT: vm.today, ISS_DT: vm.today, RF_REQ_TYPE_ID: 44, SCM_STORE_ID: 0
        };
        vm.formItem = { 'QTY_MOU_ID': '1', 'CENTRAL_STK': 0, 'SUB_STK': 0 };

        if (!vm.form.ISS_DT)
            vm.form.ISS_DT = vm.today;

        if (!vm.form.ITEM_ISS_BY)
            vm.form.ITEM_ISS_BY = cur_user_id;

        if (!vm.form.SCM_STR_GEN_ISS_H_ID)
            vm.form.SCM_STR_GEN_ISS_H_ID = 0;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), ReceiveItemList(), GetReqSourceList(), GetReqTypeList(), GetCompanyList(), GetStatusList(), GetMOUList(),
                getCategoryList(), getCompanyList(), getDepartmentList(), getStoreList(), getItemList()
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
        }

        $scope.ISS_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ISS_DT_LNopened = true;
        }

        vm.getBalance=function(item){
            return (item.STOCK_QTY || 0) - (item.ISS_QTY || 0);
        }

        vm.getStockQty = function (preItem) {
            var count = 0;

            InventoryDataService.getDataByFullUrl('/api/dye/DCBatchProgramRequisition/GetStockInfoForBP/' + preItem.INV_ITEM_ID + '/' + cm.form.SCM_STORE_ID).then(function (res) {

                preItem.STOCK_QTY = (res.STOCK_QTY || 0);
            });

        }

        vm.checkStock = function (item) {
            if ((item.STOCK_QTY) < item.ISS_QTY) {
                item.ISS_QTY = '';
                config.appToastMsg("MULTI-005 Issue quantity must less than stock quantity.");
            }
        }

        vm.addToGrid = function (data) {
            if (fnValidateSub() == true) {
                var idx = vm.ReceiveItemList.length + 1;
                var obj = angular.copy(data);
                var catId = angular.copy(obj.INV_ITEM_CAT_ID);

                var count = _.filter(vm.ReceiveItemList, function (x) { return x.INV_ITEM_ID == obj.INV_ITEM_ID }).length;
                if (count == 0) {
                    obj['RQD_QTY'] = obj.ISS_QTY;

                    var data = _.filter(vm.mOUList.data(), function (ob) {
                        return ob.RF_MOU_ID == obj.QTY_MOU_ID;
                    });
                    if (data.length > 0)
                        obj['MOU_CODE'] = data[0].MOU_CODE;

                    var cat = _.filter(vm.categoryDataSource.data(), function (ob) {
                        return ob.INV_ITEM_CAT_ID == obj.INV_ITEM_CAT_ID;
                    });
                    if (cat.length > 0)
                        obj['ITEM_CAT_NAME_EN'] = cat[0].ITEM_CAT_NAME_EN;
                    console.log(obj);
                    vm.ReceiveItemList.splice(idx, 0, obj);

                    vm.formItem = { 'QTY_MOU_ID': '1', 'INV_ITEM_CAT_ID': catId, 'INV_ITEM_ID': 0 };
                }
            }
        }


        function getCategoryList() {
            vm.categoryOptions = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID",
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        e.sender.value(ds[0].INV_ITEM_CAT_ID);
                        vm.form.INV_ITEM_CAT_ID = ds[0].INV_ITEM_CAT_ID;
                        vm.itemDataSource.read();
                    }

                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;
                    vm.itemDataSource.read();
                    console.log(item);

                }
            };

            return vm.categoryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/inv/ItemCategory/LoginUserWiseItemCatList';

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            //var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 206) });                            
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;
                    vm.itemDataSource.read();
                    console.log(item);
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
                }
            };

            return vm.companyDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        //var url = '/api/common/CompanyList';
                        var url = "/api/security/UserMap/GetOfficeUserByID?pSC_USER_ID=" + cur_user_id;

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            var list = _.filter(res, function (x) { return x.IS_ACTIVE == 'Y' });
                            console.log(list);
                            e.success(list);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getDepartmentList() {
            vm.departmentOptions = {
                optionLabel: "-- Select Section --",
                filter: "contains",
                autoBind: true,
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.departmentDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/Hr/HrEmployee/DeptListData';

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getStoreList() {
            vm.storeOptions = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.storeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetStoreInfo?pSC_USER_ID=' + cur_user_id;

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            //var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 513 });
                            e.success(res);
                            if (res.length == 1)
                                vm.form.SCM_STORE_ID = res[0].SCM_STORE_ID;
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

            //vm.storeOptions = {
            //    optionLabel: "-- Select Store --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataTextField: "STORE_NAME_EN",
            //    dataValueField: "SCM_STORE_ID",
            //    select: function (e) {
            //        var item = this.dataItem(e.item);
            //        console.log(item);
            //    }
            //};

            //return vm.storeDataSource = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            var url = '/api/common/GetStoreInfo';

            //            return InventoryDataService.getDataByFullUrl(url).then(function (res) {
            //                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 513 });
            //                e.success(list);
            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    }
            //});
        };


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
                            InventoryDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {

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

        }


        function ReceiveItemList() {

            if ($stateParams.pSCM_STR_GEN_REQ_H_ID > 0) {

                InventoryDataService.getDataByFullUrl('/api/Inv/GenStrReq/GetGenStrReqDtlByID?pSCM_STR_GEN_REQ_H_ID=' + ($stateParams.pSCM_STR_GEN_REQ_H_ID || 0)).then(function (res) {
                    vm.ReceiveItemList = res;
                });
            }
            else {
                InventoryDataService.getDataByFullUrl('/api/Inv/GenStrReq/GetGenStrIssueDtlByID?pSCM_STR_GEN_ISS_H_ID=' + ($stateParams.pSCM_STR_GEN_ISS_H_ID || 0)).then(function (res) {
                    vm.ReceiveItemList = res;
                });
            }

        };

        function GetItemCategoryList() {

            InventoryDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {

                var list = _.filter(res, function (o) { return ((o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4 || o.PARENT_ID === 3 || o.PARENT_ID === 4)) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: list
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            InventoryDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 3)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };


        function getItemList() {
            vm.itemOptions = {
                optionLabel: "-- Select Item --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;
                    vm.formItem.PACK_MOU_ID = item.PACK_MOU_ID;
                    vm.formItem.QTY_PER_PACK = item.PACK_RATIO;

                    console.log(item);

                    //var data = _.filter(vm.packMouDataSource.data(), function (ob) {
                    //    return ob.RF_MOU_ID == item.PACK_MOU_ID;
                    //});
                    //if (data.length > 0)
                    //    vm.formItem.PACK_MOU_CODE = data[0].MOU_CODE;

                    var url = '/api/knit/McNeedleBroken/GetItemStockByID?pSCM_STORE_ID=' + (vm.form.SCM_STORE_ID || 0) + '&pINV_ITEM_ID=' + (item.INV_ITEM_ID || 0);
                    return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                        vm.formItem.STOCK_QTY = res['STOCK_QTY'];
                    }, function (err) {
                        console.log(err);
                    });
                }
            };

            return vm.itemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (vm.form.INV_ITEM_CAT_ID || 206);

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.copyAllRequiredQty = function () {
            for (var i = 0; i < vm.ReceiveItemList.length; i++)
                if (parseFloat(vm.ReceiveItemList[i].STOCK_QTY) >= parseFloat(vm.ReceiveItemList[i].RQD_QTY - (vm.ReceiveItemList[i].ISSUED_QTY || 0)))
                    vm.ReceiveItemList[i].ISS_QTY = (vm.ReceiveItemList[i].RQD_QTY - (vm.ReceiveItemList[i].ISSUED_QTY || 0));
                else
                    vm.ReceiveItemList[i].ISS_QTY = '';
        }

        vm.copyRequiredQty = function (item) {
            if (parseFloat(item.STOCK_QTY) >= parseFloat(item.RQD_QTY))
                item.ISS_QTY = item.RQD_QTY;
            else
                item.ISS_QTY = '';
        }


        vm.removeItemData = function (data) {

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var index = vm.ReceiveItemList.indexOf(data);
                     vm.ReceiveItemList.splice(index, 1);
                     //vm.ReceiveItemList.remove(data);
                 });

        }

        vm.reset = function () {
            $state.go($state.current, { pSCM_STR_GEN_ISS_H_ID: 0 }, { reload: true });
            //$state.go('YarnReceive', { pSCM_STR_GEN_ISS_H_ID: 0 });

        };


        //  DropdownList


        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/common/GetReqTypeByUser').then(function (res) {
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

            //return vm.reqSourceList = {
            //    optionLabel: "-- Select Store --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                InventoryDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
            //                    var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 512 });
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
                            var url = "/api/security/UserMap/GetOfficeUserByID?pSC_USER_ID=" + cur_user_id;
                            InventoryDataService.getDataByFullUrl(url).then(function (res) {
                                var list = _.filter(res, function (x) { return x.IS_ACTIVE == 'Y' });
                                console.log(list);
                                e.success(list);
                                if (list.length == 1)
                                    vm.form.HR_COMPANY_ID = list[0].HR_COMPANY_ID;
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

            //return vm.mOUList = {
            //    optionLabel: "--MOU--",
            //    filter: "contains",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                InventoryDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
            //                    e.success(res);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "MOU_CODE",
            //    dataValueField: "RF_MOU_ID"
            //};
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

        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
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
                { field: "INV_ITEM_ID", hidden: true },
                { field: "SCM_STR_GEN_ISS_H_ID", hidden: true },
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


        vm.printSlip = function (item) {
            //console.log(dataItem);
            //vm.form.SCM_STR_GEN_REQ_H_ID = item.SCM_STR_GEN_REQ_H_ID;

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3003';

            var url = '/api/Inv/InvReport/PreviewReportRDLC';

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


        vm.submitAll = function (dataOri, id) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var iList = _.filter(vm.ReceiveItemList, function (x) { return x.ISS_QTY > 0 })

                var _isudate = $filter('date')(data.ISS_DT, vm.dtFormat);
                data["ISS_DT"] = _isudate;

                data["IS_FINALIZED"] = id;

                data["XML_ISS_D"] = InventoryDataService.xmlStringShort(iList.map(function (o) {
                    return {
                        SCM_STR_GEN_ISS_D_ID: o.SCM_STR_GEN_ISS_D_ID == null ? 0 : o.SCM_STR_GEN_ISS_D_ID,
                        SCM_STR_GEN_ISS_H_ID: o.SCM_STR_GEN_ISS_H_ID == null ? 0 : o.SCM_STR_GEN_ISS_H_ID,
                        SCM_STR_GEN_REQ_D_ID: o.SCM_STR_GEN_REQ_D_ID == null ? 0 : o.SCM_STR_GEN_REQ_D_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        INV_ITEM_CAT_ID: o.INV_ITEM_CAT_ID,
                        ISS_QTY: o.ISS_QTY == null ? 0 : o.ISS_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID
                    }
                }));

                var id = vm.form.SCM_STR_GEN_ISS_H_ID

                if (id > 0)
                    data["SCM_STORE_ID"] = data.SCM_STORE_ID

                return InventoryDataService.saveDataByUrl(data, '/GenStrReq/SaveIssue').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pSCM_STR_GEN_ISS_H_ID: res.data.OPSCM_STR_GEN_ISS_H_ID }, { inherit: false, reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPSCM_STR_GEN_ISS_H_ID) > 0) {
                                vm.form.SCM_STR_GEN_ISS_H_ID = res.data.OPSCM_STR_GEN_ISS_H_ID;
                                $state.go($state.current, { pSCM_STR_GEN_ISS_H_ID: res.data.OPSCM_STR_GEN_ISS_H_ID }, { inherit: false, reload: true });
                            }
                        }

                    }
                });
            }
        };
    }


})();

