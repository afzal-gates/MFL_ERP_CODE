
////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.inventory').controller('GeneralStoreRequisitionController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'formData', 'InventoryDataService', 'Dialog', 'cur_user_id', GeneralStoreRequisitionController]);
    function GeneralStoreRequisitionController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, formData, InventoryDataService, Dialog, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        vm.isAddToGrid = 'Y';
        vm.updateGridIndex = null;

        var key = 'SCM_STR_OIL_REQ_H_ID';
        vm.today = new Date();

        vm.form = formData.SCM_STR_GEN_REQ_H_ID ? formData : { STR_REQ_DT: vm.today, RF_REQ_TYPE_ID: 44, SCM_STORE_ID: 27, STR_REQ_BY: cur_user_id, INV_ITEM_CAT_LST: '' };
        vm.formItem = { SCM_STR_GEN_REQ_D_ID: 0, QTY_MOU_ID: 0, QTY_MOU_CODE: '' };

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getCategoryList(), getReqTypeList(), getCompanyList(), getDepartmentList(),
                RequisitionItemList(), getStoreList(), getUserList(), getItemList(), getMouList(),
                GetStatusList()];
            return $q.all(promise).then(function () {

                vm.showSplash = false;
            });
        }

        vm.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.STR_REQ_DT_LNopened = true;
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
                        var url = '/api/inv/ItemCategory/SelectAll';

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
                        var url = '/api/common/GetReqTypeByUser';

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
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

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
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
                        var url = '/api/common/GetStoreInfo';

                        return InventoryDataService.getDataByFullUrl(url).then(function (res) {
                            //var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 513 });
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function getUserList() {
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

        //vm.userTemplate = '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>';
        //vm.userValueTemplate = '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span> <span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>';

        //function getUserList() {
        //    vm.userOptions = {
        //        optionLabel: "-- Select User --",
        //        filter: "contains",
        //        autoBind: true,
        //        valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
        //        template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
        //                  '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
        //        dataTextField: "LOGIN_ID",
        //        dataValueField: "SC_USER_ID",
        //        select: function (e) {
        //            var item = this.dataItem(e.item);
        //            console.log(item);
        //        }
        //    };

        //    return vm.userDataSource = new kendo.data.DataSource({
        //        transport: {
        //            read: function (e) {
        //                var url = '/api/common/SelectAllUserData';

        //                return InventoryDataService.getDataByFullUrl(url).then(function (res) {
        //                    e.success(res);
        //                }, function (err) {
        //                    console.log(err);
        //                });
        //            }
        //        }
        //    });
        //};

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

                    var data = _.filter(vm.packMouDataSource.data(), function (ob) {
                        return ob.RF_MOU_ID == item.PACK_MOU_ID;
                    });
                    if (data.length > 0)
                        vm.formItem.PACK_MOU_CODE = data[0].MOU_CODE;

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

        function getMouList() {
            vm.packMouOptions = {
                optionLabel: "--Pack UoM--",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);                    
                //    console.log(item);
                //    vm.formItem.PACK_MOU_CODE = item.MOU_CODE;
                //}                
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


            return InventoryDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {

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


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 10;
            var PARENT_ID = 0;
            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
            var sList = null;
            InventoryDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                sList = res;
                console.log(sList);
                if (res.length == 1) {
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
                            e.success(sList);
                            //});
                        }
                    }
                },
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID"
            };

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
                { field: "DYE_DC_RCV_H_ID", hidden: true },
                { field: "DYE_DC_RCV_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },
                { field: "LOC_UNIT_PRICE", hidden: true },
                { field: "PCT_ADDL_PRICE", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "15%" },
                { field: "PACK_RQD_QTY", title: "Reqd. Pack", type: "string", width: "10%", editable: false },
                { field: "PACK_MOU_CODE", title: "Pack Unit", type: "string", width: "10%", editable: false },
                { field: "QTY_PER_PACK", title: "Qty/Pack", type: "string", width: "11%", editable: false },
                { field: "RQD_QTY", title: "Reqd. Qty", type: "string", width: "10%", editable: false, filterable: false },
                { field: "QTY_MOU_CODE", title: "UoM", type: "string", width: "5%", editable: false, filterable: false },
                //{ field: "SP_NOTE", title: "Note", width: "12%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem,0)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };



        function RequisitionItemList() {
            vm.RequisitionItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/Inv/GenStrReq/GetGenStrReqDtlByID?pSCM_STR_GEN_REQ_H_ID=' + ($stateParams.pSCM_STR_GEN_REQ_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };


        vm.TotalReqQty = function () {

            if (parseFloat(vm.formItem.QTY_PER_PACK) <= parseFloat(vm.formItem.LOOSE_QTY)) {
                vm.formItem.LOOSE_QTY = 0;
            }

            if ((vm.formItem.PACK_RQD_QTY || 0) > 0) {
                var total = (parseFloat(vm.formItem.PACK_RQD_QTY) * parseFloat(vm.formItem.QTY_PER_PACK)) + (parseFloat(vm.formItem.LOOSE_QTY) || 0);
                var stock = parseFloat(vm.formItem.STOCK_QTY);
                if (total > stock) {

                    vm.formItem.PACK_RQD_QTY = '';
                    vm.formItem.RQD_QTY = '';
                }
                else {
                    vm.formItem.RQD_QTY = total;
                }
                //vm.formItem.RQD_QTY = total;
            }
            else
                vm.formItem.PACK_RQD_QTY = '';
        };


        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {

                var item = _.filter(vm.itemDataSource.data(), function (o) {
                    return o.INV_ITEM_ID === parseInt(vm.formItem.INV_ITEM_ID)
                })[0];

                vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var mouUnit = _.filter(vm.qtyMouDataSource.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.QTY_MOU_ID)
                })[0];

                var packmouUnit = _.filter(vm.packMouDataSource.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.PACK_MOU_ID)
                })[0];

                vm.formItem.PACK_MOU_CODE = packmouUnit.MOU_CODE;

                console.log(mouUnit);

                vm.formItem.QTY_MOU_CODE = mouUnit.MOU_CODE;

                vm.formItem.INV_ITEM_CAT_ID = vm.form.INV_ITEM_CAT_ID;
                if (vm.formItem.uid) {
                    // Update

                    var row = vm.RequisitionItemList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.RequisitionItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.uid != vm.formItem.uid)
                    }).length;
                    console.log(count);

                    if (count <= 1) {

                        row["INV_ITEM_ID"] = vm.formItem.INV_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                        row["DYE_DC_RCV_H_ID"] = vm.formItem.DYE_DC_RCV_H_ID;
                        row["PACK_MOU_ID"] = vm.formItem.PACK_MOU_ID;
                        row["QTY_MOU_ID"] = vm.formItem.QTY_MOU_ID;
                        row["QTY_MOU_CODE"] = vm.formItem.QTY_MOU_CODE;
                        row["PACK_MOU_CODE"] = vm.formItem.PACK_MOU_CODE;
                        row["PACK_RQD_QTY"] = vm.formItem.PACK_RQD_QTY;

                        row["LOOSE_QTY"] = vm.formItem.LOOSE_QTY;
                        row["QTY_PER_PACK"] = vm.formItem.QTY_PER_PACK;
                        row["RQD_QTY"] = vm.formItem.RQD_QTY;

                        vm.formItem = { INV_ITEM_ID: '', QTY_MOU_ID: '1', PACK_MOU_ID: '' };

                        //config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                } else {
                    //insert

                    var count = _.filter(vm.RequisitionItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.RF_BRAND_ID === vm.formItem.RF_BRAND_ID && o.LK_YRN_COLR_GRP_ID === vm.formItem.LK_YRN_COLR_GRP_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.RequisitionItemList.data().length + 1;
                        vm.RequisitionItemList.insert(idx, vm.formItem);

                        var gview = vm.RequisitionItemList.data();
                        vm.formItem = { INV_ITEM_ID: '', QTY_MOU_ID: '1', PACK_MOU_ID: '' };
                        //console.log(gview);
                        //config.appToastMsg("Item information has been added successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }
                }
            }
        };


        vm.editItemData = function (data, step) {

            vm.form['INV_ITEM_CAT_ID'] = angular.copy(data.INV_ITEM_CAT_ID);
            vm.itemDataSource.read();
            var mydata = angular.copy(data);
            vm.formItem = mydata;
            //vm.DyItemList = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + data.INV_ITEM_CAT_ID).then(function (res) {
            //                e.success(res);
            //                var mydata = angular.copy(data);
            //                vm.formItem = mydata;
            //                //vm.formItem.TOTAL_AMT_BDT = vm.formItem.RCV_QTY * vm.formItem.COST_PRICE; //((vm.form.LOC_CONV_RATE || 0) + (vm.formItem.PCT_ADDL_PRICE * ((vm.form.LOC_CONV_RATE || 0) / 100)));
            //                if (vm.form.RF_CURRENCY_ID == 1)
            //                    vm.formItem.PCT_ADDL_PRICE = 0;
            //                vm.formItem.TOTAL_AMT_BDT = vm.formItem.RCV_QTY * parseFloat(vm.form.LOC_CONV_RATE) * (parseFloat(vm.formItem.UNIT_PRICE) + (parseFloat(vm.formItem.UNIT_PRICE) * (vm.formItem.PCT_ADDL_PRICE / 100)))
            //            });
            //        }
            //    }
            //});

        }

        vm.removeItemData = function (data) {

            if (!data.KNT_YRN_RCV_D_ID || data.KNT_YRN_RCV_D_ID <= 0) {
                vm.RequisitionItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.RequisitionItemList.remove(data);
                 });

        }

        vm.resetItemData = function () {

            vm.formItem = {};
            vm.formItem['INV_ITEM_ID'] = '';
            vm.formItem['HR_COUNTRY_ID'] = '';
            vm.formItem['PACK_MOU_ID'] = '';
            vm.formItem['QTY_MOU_ID'] = '3';

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

                data["IS_FINALIZED"] = id;

                data["XML_REQ_D"] = InventoryDataService.xmlStringShort(vm.RequisitionItemList.data().map(function (o) {
                    return {
                        SCM_STR_GEN_REQ_D_ID: o.SCM_STR_GEN_REQ_D_ID == null ? 0 : o.SCM_STR_GEN_REQ_D_ID,
                        SCM_STR_GEN_REQ_H_ID: o.SCM_STR_GEN_REQ_H_ID == null ? 0 : o.SCM_STR_GEN_REQ_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        INV_ITEM_CAT_ID: o.INV_ITEM_CAT_ID,
                        PACK_RQD_QTY: o.PACK_RQD_QTY,
                        QTY_PER_PACK: o.QTY_PER_PACK,
                        LOOSE_QTY: o.LOOSE_QTY,
                        RQD_QTY: o.RQD_QTY,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 0 : o.PACK_MOU_ID,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID

                    }
                }));

                var id = vm.form.SCM_STR_GEN_REQ_H_ID

                var _invdate = $filter('date')(data.STR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["STR_REQ_DT"] = _invdate;
                data["INV_ITEM_CAT_LST"] = data.INV_ITEM_CAT_ID;

                return InventoryDataService.saveDataByUrl(data, '/GenStrReq/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pSCM_STR_GEN_REQ_H_ID: res.data.OPSCM_STR_GEN_REQ_H_ID }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPSCM_STR_GEN_REQ_H_ID) > 0) {
                                vm.form.SCM_STR_GEN_REQ_H_ID = res.data.OPSCM_STR_GEN_REQ_H_ID;

                                $state.go($state.current, { pSCM_STR_GEN_REQ_H_ID: res.data.OPSCM_STR_GEN_REQ_H_ID }, { reload: true });
                            }
                        }
                    }
                });
            }
        };

    }

})();
////////// End Header Controller

