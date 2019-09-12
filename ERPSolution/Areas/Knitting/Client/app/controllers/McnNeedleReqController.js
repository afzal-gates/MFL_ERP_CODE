(function () {
    'use strict';
    angular.module('multitex.knitting').controller('McnNeedleReqController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', 'Dialog', McnNeedleReqController]);
    function McnNeedleReqController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        vm.isAddToGrid = 'Y';
        vm.updateGridIndex = null;

        var key = 'SCM_STR_NDL_REQ_H_ID';
        vm.today = new Date();
        vm.formItem = { SCM_STR_NDL_REQ_H_ID: 0, QTY_MOU_ID: 1, QTY_MOU_CODE: 'Pcs' };


        vm.form = formData.SCM_STR_NDL_REQ_H_ID ? formData : {
            STR_REQ_DT: vm.today, SCM_STR_NDL_REQ_H_ID: 0, RF_ACTN_STATUS_ID: 0, RF_REQ_TYPE_ID: 22, HR_DEPARTMENT_ID: 46,
            FRM_REQ_STR_ID: 0, STR_REQ_BY_NAME: cur_user_id, BRK_TRAN_DT1: vm.today, BRK_TRAN_DT2: vm.today
        };


        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getCategoryList(), getReqTypeList(), getCompanyList(), getDepartmentList(), getStoreList(), getSubStoreList(), getUserList(), getItemList(), getMouList(),
                getStatusList(), getOperatorList(), getMachineList(), GetReasonList(), getRequisitionList(), getMcTypeByUserList(), getBrokenItemList()
            ];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }

        vm.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.STR_REQ_DT_LNopened = true;
        };

        vm.BRK_TRAN_DT1_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.BRK_TRAN_DT1_LNopened = true;
        };

        vm.BRK_TRAN_DT2_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.BRK_TRAN_DT2_LNopened = true;
        };

        vm.changeItem = function (item) {
            if (item.INV_ITEM_ID > 0)
                Dialog.confirm("Do you want to change broken entry item!!!!", 'Confirmation', ['Yes', 'No']).then(function () {
                    item.IS_CHANGE = "Y";
                });
        },

        vm.getStock = function () {
            var url = '/api/knit/McnNeedleReq/GetStockInfo?pINV_ITEM_ID=' + (vm.formItem.INV_ITEM_ID || 0) + '&pFRM_REQ_STR_ID=' + (vm.form.FRM_REQ_STR_ID || 0) + '&pTO_REQ_STR_ID=' + (vm.form.TO_REQ_STR_ID || 0) + '&pRF_NDL_REQ_TYPE_ID=' + (vm.form.RF_NDL_REQ_TYPE_ID || 0) + '&pKNT_MACHINE_ID=' + (vm.formItem.KNT_MACHINE_ID || 0);

            KnittingDataService.getDataByFullUrl(url).then(function (res) {
                vm.formItem.CENTRAL_STR_STOCK = res.CENTRAL_STR_STOCK;
                vm.formItem.SUB_STR_STOCK = res.SUB_STR_STOCK;
                vm.formItem.BRK_QTY_PCS = res.BRK_QTY_PCS;
            });
        }

        vm.checkStock = function () {
            if (parseFloat(vm.formItem.RQD_QTY_PCS) > parseFloat(vm.formItem.CENTRAL_STR_STOCK)) {
                vm.formItem.RQD_QTY_PCS = '';
                config.appToastMsg("MULTI-005 Value can not gater than stock Qty.!!!!");
            }
        }


        vm.checkBrokenStock = function (item) {
            if (parseFloat(item.RQD_QTY_PCS) > parseFloat(item.CENTRAL_STR_STOCK)) {
                item.RQD_QTY_PCS = '';
                config.appToastMsg("MULTI-005 Value can not gater than stock Qty.!!!!");
            }
        }

        vm.getPendingList = function () {

            if (fnValidateSub() == true)
                if (vm.form.RF_NDL_REQ_TYPE_ID == 1 && $stateParams.pSCM_STR_NDL_REQ_H_ID == 0) {

                    var fromDate = $filter('date')(vm.form.BRK_TRAN_DT1, 'yyyy-MM-ddTHH:mm:ss');
                    var toDate = $filter('date')(vm.form.BRK_TRAN_DT2, 'yyyy-MM-ddTHH:mm:ss');

                    var url = '/api/knit/McnNeedleReq/GetPendingBrokenItem?pFRM_REQ_STR_ID=' + (vm.form.FRM_REQ_STR_ID || 0) + '&pTO_REQ_STR_ID=' + (vm.form.TO_REQ_STR_ID || 0) + '&pRF_NDL_REQ_TYPE_ID=' + (vm.form.RF_NDL_REQ_TYPE_ID || 0) + '&pKNT_MC_TYPE_ID=' + (vm.form.KNT_MC_TYPE_ID || 0) + '&pFROM_DATE=' + (fromDate || null) + '&pTO_DATE=' + (toDate || null);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        vm.pendingItemList = res;
                    });
                }
            //else
            //    vm.pendingItemList = [];
        }


        function getRequisitionList() {
            if (vm.form.RF_NDL_REQ_TYPE_ID == 1) {
                var url = '/api/knit/McnNeedleReq/GetNeedlReqDtlList?pSCM_STR_NDL_REQ_H_ID=' + ($stateParams.pSCM_STR_NDL_REQ_H_ID || vm.form.SCM_STR_NDL_REQ_H_ID || 0);

                KnittingDataService.getDataByFullUrl(url).then(function (res) {
                    vm.pendingItemList = res;
                });
            }
            else
                vm.pendingItemList = [];
        }

        function getMcTypeByUserList() {
            vm.mcTypeOption = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MC_TYPE_NAME_EN",
                dataValueField: "KNT_MC_TYPE_ID",
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    vm.form.KNT_MC_TYPE_ID = item.KNT_MC_TYPE_ID;
                //    $scope.form.KNT_MC_TYPE_ID = item.KNT_MC_TYPE_ID;
                //    $scope.machineDataSource.read();
                //}
            };

            return vm.mcTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        KnittingDataService.getDataByFullUrl('/api/knit/KnitCommon/KntMcTypeListByUser').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function GetReasonList() {

            return vm.reasonList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/McnNeedleReq/GetNeedlReqReason').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getMachineList() {
            return vm.machineList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Knit/KnitCommon/GetMachineList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }

        vm.getOperatorList = function () {
            vm.operatorDataSource.read();
        };

        function getOperatorList() {

            vm.operatorOption = {
                optionLabel: "-- Select Operator--",
                filter: "contains",
                autoBind: true,
                dataTextField: "EMP_FULL_NAME_EN",
                dataValueField: "OPERATOR_ID"
            };

            return vm.operatorDataSource = new kendo.data.DataSource({
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntMachinOprAssign/GetAssiPersonListByMachinId?pKNT_MACHINE_ID=' + (vm.formItem.KNT_MACHINE_ID || 0);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            for (var x = 0; x < res.length; x++) {
                                var SO = res[x].EMP_FULL_NAME_EN + " (" + res[x].EMPLOYEE_CODE + ")";
                                res[x].EMP_FULL_NAME_EN = SO;
                            }
                            console.log(res);
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

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
                        vm.formItem.INV_ITEM_CAT_ID = ds[0].INV_ITEM_CAT_ID;
                        vm.itemDataSource.read();
                    }
                    //else if (ds.length > 0 && $stateParams.pLK_FLOOR_ID > 0) {
                    //    e.sender.value($stateParams.pLK_FLOOR_ID);
                    //    vm.form.LK_FLOOR_ID = $stateParams.pLK_FLOOR_ID;
                    //}

                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.formItem.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;
                    vm.itemDataSource.read();

                }
            };

            return vm.categoryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/inv/ItemCategory/SelectAll';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 128 || o.INV_ITEM_CAT_ID === 211) });
                            e.success(list);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getReqTypeList() {
            vm.reqTypeOptions = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.reqTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetReqType';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            var list = _.filter(res, function (o) { return (o.RF_REQ_TYPE_ID === 22) });
                            e.success(list);
                            //e.success(res);
                        }, function (err) {
                            console.log(err);
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

        function getDepartmentList() {
            vm.departmentOptions = {
                optionLabel: "-- Select Department --",
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
                        var url = '/Hr/Admin/HrDesignation/DepartmentListData';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
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
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        e.sender.value(ds[0].SCM_STORE_ID);
                        vm.form.TO_REQ_STR_ID = ds[0].SCM_STORE_ID;
                    }
                }
            };

            return vm.storeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=12&pSC_USER_ID=' + cur_user_id;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID === 511 });
                            e.success(list);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function getSubStoreList() {
            vm.subStoreOptions = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        e.sender.value(ds[0].SCM_STORE_ID);
                        vm.form.FRM_REQ_STR_ID = ds[0].SCM_STORE_ID;
                        vm.form.HR_COMPANY_ID = ds[0].HR_COMPANY_ID;
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
                }
            };

            return vm.subStoreDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=12&pSC_USER_ID=' + cur_user_id;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID === 512 });
                            e.success(list);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getUserList() {
            vm.userOptions = {
                optionLabel: "-- Select User --",
                filter: "contains",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.userDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/SelectAllUserData';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function getBrokenItemList() {

            return vm.itemList = {
                optionLabel: "-- Select Item --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            var url = '/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (vm.formItem.INV_ITEM_CAT_ID || 128);
                            KnittingDataService.getDataByFullUrl(url).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID"
            };
        }


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
                    console.log(item);

                    var data = _.filter(vm.packMouDataSource.data(), function (ob) {
                        return ob.RF_MOU_ID == item.PACK_MOU_ID;
                    });
                    if (data.length > 0)
                        vm.formItem.PACK_MOU_CODE = data[0].MOU_CODE;
                    console.log(data);
                }
            };

            return vm.itemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (vm.formItem.INV_ITEM_CAT_ID || 128);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getMouList() {
            vm.packMouOptions = {
                optionLabel: "--Pack UoM--",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"//,              
            };

            vm.qtyMouOptions = {
                optionLabel: "-- UoM --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.formItem.QTY_MOU_CODE = item.MOU_CODE;
                }
            };


            return KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {

                vm.packMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

                vm.qtyMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

            }, function (err) {
                console.log(err);
            });


        };

        var RF_ACTN_TYPE_ID = 23;
        var PARENT_ID = 0;
        if (vm.form.RF_ACTN_STATUS_ID > 0) {
            PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
        }
        function getStatusList() {
            vm.actionOptions = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        e.sender.value(ds[0].RF_ACTN_STATUS_ID);
                        vm.form.RF_ACTN_STATUS_ID_NEXT = ds[0].RF_ACTN_STATUS_ID;
                    }
                }
            };

            return vm.actionDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };


        vm.TotalReqQty = function () {
            if ((vm.formItem.PACK_RQD_QTY || 0) > 0) {
                var total = parseFloat(vm.formItem.PACK_RQD_QTY) * parseFloat(vm.formItem.QTY_PER_PACK);
                var stock = parseFloat(vm.formItem.CENTRAL_STK);
                if (total > stock) {

                    vm.formItem.PACK_RQD_QTY = '';
                    vm.formItem.RQD_QTY = 0;
                }
                else {
                    vm.formItem.RQD_QTY = total;
                }
            }
            else
                vm.formItem.PACK_RQD_QTY = 0;
        };

        vm.resetItemData = function () {
            vm.formItem = { INV_ITEM_CAT_ID: '', KNT_MACHINE_ID: '', OPERATOR_ID: '', INV_ITEM_ID: '' }
        }

        vm.reqDtlOptions = {
            height: 350,
            sortable: true,
            pageable: false,
            editable: false,
            //selectable: "row",
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

                { field: "INV_ITEM_ID", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },
                { field: "KNT_MACHINE_ID", hidden: true },
                { field: "OPERATOR_ID", hidden: true },
                //{ field: "QTY_MOU_ID", hidden: true },
                { field: "KNT_MACHINE_NO", title: "Machine No", type: "string", width: "10%", editable: false },
                { field: "EMP_FULL_NAME_EN", title: "Machine Operator", type: "string", width: "10%", editable: false },
                { field: "ITEM_NAME_EN", title: "Item", type: "string", width: "20%", editable: false, footerTemplate: "Total Record: #=count#" },
                { field: "CENTRAL_STR_STOCK", title: "Maint. Stock (Ltr)", type: "string", width: "10%", editable: false },
                { field: "SUB_STR_STOCK", title: "Sub Stock (Pcs)", type: "string", width: "10%", editable: false },
                { field: "BRK_QTY_PCS", title: "Brocken Qty", type: "string", width: "10%", editable: false },
                { field: "RQD_QTY_PCS", title: "Reqd. Qty", type: "string", width: "10%", editable: false, filterable: false, footerTemplate: "#=sum#" },
                //{ field: "QTY_MOU_CODE", title: "UoM", type: "string", width: "5%", editable: false, filterable: false },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ]
        };


        vm.reqDtlDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    var url = '/api/knit/McnNeedleReq/GetNeedlReqDtl?pSCM_STR_NDL_REQ_H_ID=' + ($stateParams.pSCM_STR_NDL_REQ_H_ID || vm.form.SCM_STR_NDL_REQ_H_ID || 0);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            aggregate: [
                { field: "ITEM_NAME_EN", aggregate: "count" },
                { field: "RQD_QTY_PCS", aggregate: "sum" }
            ]
        });

        vm.onChangeIssPackQty = function (dataItem) {
            dataItem.ISS_QTY = parseFloat(dataItem.PACK_ISS_QTY) * parseFloat(dataItem.QTY_PER_PACK);
        }

        //vm.addToGrid = function () {
        //    vm.reqDtlDataSource.insert(0, vm.formItem);
        //    vm.resetRow();
        //}

        vm.addToGrid = function () {
            console.log(vm.formItem);
            if (fnValidateSub() == true) {

                if (parseInt(vm.formItem.CENTRAL_STR_STOCK) < parseInt(vm.formItem.CENTRAL_STR_STOCK)) {
                    config.appToastMsg("MULTI-005 There is insufficient balance!!!");
                    return false;
                }
                var mac = _.filter(vm.machineList.data(), function (o) {
                    return o.KNT_MACHINE_ID === parseInt(vm.formItem.KNT_MACHINE_ID)
                })[0];

                vm.formItem.KNT_MACHINE_NO = mac.KNT_MACHINE_NO;

                var emp = _.filter(vm.operatorDataSource.data(), function (o) {
                    return o.OPERATOR_ID === parseInt(vm.formItem.OPERATOR_ID)
                })[0];
                console.log(emp);

                vm.formItem.EMP_FULL_NAME_EN = emp.EMP_FULL_NAME_EN;

                //var qty = _.filter(vm.qtyMouDataSource.data(), function (o) {
                //    return o.RF_MOU_ID === parseInt(vm.formItem.QTY_MOU_ID)
                //})[0];
                //console.log(qty);

                //vm.formItem["QTY_MOU_CODE"] = qty.MOU_CODE;
                //vm.formItem["MOU_CODE"] = qty.MOU_CODE;

                var item_cat = angular.copy(vm.formItem.INV_ITEM_CAT_ID);

                var item = angular.copy(vm.formItem);

                if (vm.formItem.uid) {
                    // Update

                    var row = vm.reqDtlDataSource.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.reqDtlDataSource.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.uid != vm.formItem.uid)
                    }).length;

                    if (count <= 1) {
                        row["INV_ITEM_ID"] = vm.formItem.INV_ITEM_ID;
                        row["INV_ITEM_CAT_ID"] = vm.formItem.INV_ITEM_CAT_ID;
                        row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                        row["KNT_MACHINE_ID"] = vm.formItem.KNT_MACHINE_ID;
                        row["KNT_MACHINE_NO"] = vm.formItem.KNT_MACHINE_NO;
                        //row["QTY_MOU_ID"] = vm.formItem.QTY_MOU_ID;
                        row["OPERATOR_ID"] = vm.formItem.OPERATOR_ID;
                        row["EMP_FULL_NAME_EN"] = vm.formItem.EMP_FULL_NAME_EN;
                        row["CENTRAL_STR_STOCK"] = vm.formItem.CENTRAL_STR_STOCK;
                        row["SUB_STR_STOCK"] = vm.formItem.SUB_STR_STOCK;
                        row["BRK_QTY_PCS"] = vm.formItem.BRK_QTY_PCS;
                        row["RQD_QTY_PCS"] = vm.formItem.RQD_QTY_PCS;

                        vm.formItem = { 'INV_ITEM_ID': '', 'OPERATOR_ID': '', 'KNT_MACHINE_ID': '', 'INV_ITEM_CAT_ID': item_cat };
                    }
                }
                else {
                    var count = _.filter(vm.reqDtlDataSource.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.OPERATOR_ID === vm.formItem.OPERATOR_ID && o.KNT_MACHINE_ID === vm.formItem.KNT_MACHINE_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.reqDtlDataSource.data().length + 1;
                        vm.reqDtlDataSource.insert(idx, item);
                        vm.formItem = { 'INV_ITEM_ID': '', 'OPERATOR_ID': '', 'KNT_MACHINE_ID': '', 'INV_ITEM_CAT_ID': item_cat };
                        console.log(vm.reqDtlDataSource.data());
                    }
                }
            }
        };


        vm.removeItemData = function (data) {

            if (!data.SCM_STR_NDL_REQ_D_ID || data.SCM_STR_NDL_REQ_D_ID <= 0) {
                vm.reqDtlDataSource.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.reqDtlDataSource.remove(data);
                 });

        }

        vm.editItemData = function (dataItem) {
            vm.formItem = angular.copy(dataItem);
            vm.getOperatorList()
            vm.formItem = angular.copy(dataItem);
        }

        function findGridIndex(index, SCM_STR_TR_REQ_D_ID) {
            if (index > -1) {
                return index;
            } else {

                return _.findIndex(vm.reqDtlDataSource, function (obj) {
                    return parseInt(obj.SCM_STR_TR_REQ_D_ID) == parseInt(SCM_STR_TR_REQ_D_ID);
                });
            }

        };
        vm.editRow = function (dataItem) {
            vm.isAddToGrid = 'N';
            vm.updateGridIndex = findGridIndex(vm.reqDtlDataSource.indexOf(dataItem), dataItem.SCM_STR_TR_REQ_D_ID);

            vm.formItem = angular.copy(dataItem);
        };


        vm.resetRow = function () {
            var data = angular.copy(vm.formItem);
            vm.formItem = {
                SCM_STR_TR_REQ_D_ID: 0, QTY_MOU_ID: 1, QTY_MOU_CODE: 'Pcs', INV_ITEM_ID: data.INV_ITEM_ID, ITEM_NAME_EN: data.ITEM_NAME_EN
            };
        }

        vm.removeRow = function (dataItem) {
            vm.reqDtlDataSource.remove(dataItem);
            //var dataList = vm.rollRcvGridDataSource.data();

        };


        vm.removeItemData = function (item) {
            var index = vm.pendingItemList.indexOf(item);
            vm.pendingItemList.splice(index, 1);
        }


        vm.printChallan = function () {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3515';

            if (vm.form.SCM_STORE_ID == null || vm.form.SCM_STORE_ID == '') {
                vm.form.SCM_STORE_ID = -1;
            }

            var url;
            if (vm.isRDLC == true) {
                url = '/api/Knit/KntReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Knit/KntReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

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

        vm.submitAll = function (dataOri, pIS_UPDATE) {

            if (fnValidate() == true || pIS_UPDATE == "Y") {

                var data = angular.copy(dataOri);

                var result = [];

                if (data.RF_NDL_REQ_TYPE_ID > 1) {
                    console.log(vm.reqDtlDataSource.data());
                    vm.reqDtlDataSource.data().forEach(function (x) { if (!result.includes(x.INV_ITEM_CAT_ID)) result.push(x.INV_ITEM_CAT_ID) })
                    var sCat = result.toString();
                    data['INV_ITEM_CAT_LST'] = sCat;

                    data["XML_REQ_D"] = KnittingDataService.xmlStringShort(vm.reqDtlDataSource.data().map(function (o) {
                        return {
                            SCM_STR_NDL_REQ_D_ID: o.SCM_STR_NDL_REQ_D_ID == null ? 0 : o.SCM_STR_NDL_REQ_D_ID,
                            KNT_MACHINE_ID: o.KNT_MACHINE_ID,
                            INV_ITEM_ID: o.INV_ITEM_ID,
                            INV_ITEM_CAT_ID: o.INV_ITEM_CAT_ID == 0 ? null : o.INV_ITEM_CAT_ID,
                            OPERATOR_ID: o.OPERATOR_ID == 0 ? null : o.OPERATOR_ID,
                            BRK_QTY_PCS: o.BRK_QTY_PCS == null ? 0 : o.BRK_QTY_PCS,
                            RQD_QTY_PCS: o.RQD_QTY_PCS == null ? 0 : o.RQD_QTY_PCS,
                            IS_CHANGE: o.IS_CHANGE == null ? 'N' : o.IS_CHANGE,
                        }
                    }));
                }
                else {
                    vm.pendingItemList.forEach(function (x) { if (!result.includes(x.INV_ITEM_CAT_ID)) result.push(x.INV_ITEM_CAT_ID) })
                    var sCat = result.toString();
                    data['INV_ITEM_CAT_LST'] = sCat;

                    data["XML_REQ_D"] = KnittingDataService.xmlStringShort(vm.pendingItemList.map(function (o) {
                        return {
                            SCM_STR_NDL_REQ_D_LST_ID: o.SCM_STR_NDL_REQ_D_LST_ID == null ? 0 : o.SCM_STR_NDL_REQ_D_LST_ID,
                            MCN_ITEM_LST: o.MCN_ITEM_LST,
                            INV_ITEM_ID: o.INV_ITEM_ID,
                            INV_ITEM_CAT_ID: o.INV_ITEM_CAT_ID == 0 ? null : o.INV_ITEM_CAT_ID,
                            BRK_QTY_PCS: o.BRK_QTY_PCS == null ? 0 : o.BRK_QTY_PCS,
                            RQD_QTY_PCS: o.RQD_QTY_PCS == null ? 0 : o.RQD_QTY_PCS,
                            IS_CHANGE: o.IS_CHANGE == null ? 'N' : o.IS_CHANGE,
                        }
                    }));
                }
                var _invdate = $filter('date')(data.STR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["STR_REQ_DT"] = _invdate;
                data["IS_UPDATE"] = pIS_UPDATE;
                data["RF_ACTN_STATUS_ID"] = vm.form.RF_ACTN_STATUS_ID_NEXT;

                var id = vm.form.SCM_STR_NDL_REQ_H_ID

                return KnittingDataService.saveDataByUrl(data, '/McnNeedleReq/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pSCM_STR_NDL_REQ_H_ID: id }, { reload: true });
                        }
                        else {

                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPSCM_STR_NDL_REQ_H_ID) > 0) {
                                vm.form.SCM_STR_NDL_REQ_H_ID = res.data.OPSCM_STR_NDL_REQ_H_ID;
                                $state.go($state.current, { pSCM_STR_NDL_REQ_H_ID: res.data.OPSCM_STR_NDL_REQ_H_ID });
                            }
                        }

                    }
                });
            }
        };



    }

})();

