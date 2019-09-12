(function () {
    'use strict';
    angular.module('multitex.knitting').controller('GeneralBufferYarnRequisitionController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'formData', 'Dialog', '$http', '$modal', 'commonDataService', 'cur_user_id', GeneralBufferYarnRequisitionController]);
    function GeneralBufferYarnRequisitionController($q, config, KnittingDataService, $stateParams, $state, $scope, formData, Dialog, $http, $modal, commonDataService, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.SCM_PURC_REQ_H_ID ? formData : { 'PURC_REQ_BY': cur_user_id, 'PURC_REQ_TO': cur_user_id, 'RF_REQ_TYPE_ID': 1, 'HR_DEPARTMENT_ID': 46, LK_PURC_PROD_GRP_ID: 790 };

        vm.formItem = { 'QTY_MOU_ID': 3 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), GetItemCategoryList(), getCurrencyList(), RequisitionItemList(), GetReqSourceList(),
                GetReqTypeList(), GetPriorityList(), getTnaStatusList(), GetCompanyList(), GetPaymentMethodList(), getFiscalYearData(),
                GetYarnColorGroupList(), GetMOUList(), GetSupplierList(), GetSourceTypeList(), GetBuyerList(), GetStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        if (formData['SCM_PURC_REQ_H_ID']) {
            formData['INV_ITEM_CAT_LST'] = formData.INV_ITEM_CAT_LST ? formData.INV_ITEM_CAT_LST.split(',') : [];
            vm.form = formData;
        } else {
            vm.form = { INV_ITEM_CAT_ID_LST: [], 'PURC_REQ_BY': cur_user_id, 'PURC_REQ_TO': cur_user_id, 'RF_REQ_TYPE_ID': 1, 'HR_DEPARTMENT_ID': 46 }

        }


        vm.clearBuyerList = function () {
            vm.form.MC_BUYER_LST = '';
        }


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 10;
            var PARENT_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            KnittingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
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
                            KnittingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
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


        function GetBuyerList() {

            return vm.buyerList = {
                optionLabel: "-- Select Buyer --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "BUYER_NAME_EN",
                dataValueField: "MC_BUYER_ID"
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


        function RequisitionItemList() {
            var url = '/api/knit/KnitYarnReq/ReqDtlByID?pSCM_PURC_REQ_H_ID=' + ($stateParams.pSCM_PURC_REQ_H_ID || 0);

            if (formData.IS_ORDER_WISE) {
                if (formData.IS_ORDER_WISE == "Y")
                    url = '/api/knit/KnitYarnReq/YarnReqDtlByID?pSCM_PURC_REQ_H_ID=' + ($stateParams.pSCM_PURC_REQ_H_ID || 0);
            }


            vm.RequisitionItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function GetItemCategoryList() {

            KnittingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var filter = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 2) });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: filter
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.INV_ITEM_CAT_ID || 0)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        vm.addToGrid = function (isValid) {

            if (fnValidateSub() == true) {
                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.INV_ITEM_ID === parseInt(vm.formItem.INV_ITEM_ID)
                })[0];

                vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                //var itemB = _.filter(vm.categoryBrandList.data(), function (o) {
                //    return o.RF_BRAND_ID === parseInt(vm.form.RF_BRAND_ID)
                //})[0];

                //vm.formItem.BRAND_NAME_EN = itemB.BRAND_NAME_EN;

                var yColorG = _.filter(vm.yarnColorGroupList.data(), function (o) {
                    return o.LOOKUP_DATA_ID === parseInt(vm.formItem.LK_YRN_COLR_GRP_ID)
                })[0];

                vm.formItem.LK_YRN_COLR_GRP_NAME = yColorG.LK_DATA_NAME_EN;

                var mouUnit = _.filter(vm.mOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.QTY_MOU_ID)
                })[0];

                vm.formItem.MOU_CODE = mouUnit.MOU_CODE;
                vm.formItem.INV_ITEM_CAT_ID = vm.form.INV_ITEM_CAT_ID;

                if (vm.formItem.uid) {
                    // Update

                    var row = vm.RequisitionItemList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.RequisitionItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.uid != vm.formItem.uid && o.LK_YRN_COLR_GRP_ID === vm.formItem.LK_YRN_COLR_GRP_ID)
                    }).length;

                    if (count <= 1) {
                        row["INV_ITEM_CAT_ID"] = vm.formItem.INV_ITEM_CAT_ID;
                        row["INV_ITEM_ID"] = vm.formItem.INV_ITEM_ID;
                        row["RF_BRAND_ID"] = vm.formItem.RF_BRAND_ID;
                        row["SCM_PURC_REQ_H_ID"] = vm.formItem.SCM_PURC_REQ_H_ID;
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

                    var count = _.filter(vm.RequisitionItemList.data(), function (o) {
                        return o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.LK_YRN_COLR_GRP_ID === vm.formItem.LK_YRN_COLR_GRP_ID
                    }).length;

                    if (count == 0) {

                        var idx = vm.RequisitionItemList.data().length + 1;
                        vm.RequisitionItemList.insert(idx, vm.formItem);

                        var gview = vm.RequisitionItemList.data();
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

            if (!data.SCM_PURC_REQ_D_ID || data.SCM_PURC_REQ_D_ID <= 0) {
                vm.RequisitionItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.RequisitionItemList.remove(data);
                 });

        }

        vm.resetItemData = function () {
            vm.formItem = { 'QTY_MOU_ID': 3 };
            vm.formItem['INV_ITEM_ID'] = '';
            vm.formItem['LK_YRN_COLR_GRP_ID'] = '';
            vm.formItem['RF_BRAND_ID'] = '';
        };

        vm.reset = function () {

            $state.go('GenBuffYarnRequisition', { pSCM_PURC_REQ_H_ID: 0 });

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
                optionLabel: "--Source--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(84).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };

            //return vm.sourceTypeList = {
            //    optionLabel: "-- Select Source Type --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                KnittingDataService.getDataByFullUrl('/api/common/GetReqSRC').then(function (res) {
            //                    //KnittingDataService.LookupListData(92).then(function (res) {
            //                    e.success(res);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "REQ_SRC_NAME",
            //    dataValueField: "RF_REQ_SRC_ID"
            //};
        };

        //function GetStatusList() {

        //    return vm.statusList = {
        //        optionLabel: "-- Select Status --",
        //        filter: "startswith",
        //        autoBind: true,
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    KnittingDataService.LookupListData(46).then(function (res) {
        //                        e.success(res);
        //                    });
        //                }
        //            }
        //        },
        //        dataTextField: "LK_DATA_NAME_EN",
        //        dataValueField: "LOOKUP_DATA_ID"
        //    };
        //};

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
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

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

                { field: "SCM_PURC_REQ_H_ID", hidden: true },
                { field: "SCM_PURC_REQ_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "LK_YRN_COLR_GRP_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Yarn Item Name", width: "20%", headerTemplate: "<b>Yarn Item Name</b>" },
                { field: "LK_YRN_COLR_GRP_NAME", title: "Color Group", width: "10%", headerTemplate: "<b>Color Group</b>" },
                //{ field: "BRAND_NAME_EN", title: "Brand", width: "10%", headerTemplate: "<b>Brand</b>" },
                { field: "BUFR_ALOC_QTY", title: "Available Buffer Qty.", width: "10%", headerTemplate: "<b>Available Buffer Qty.</b>" },
                //{ field: "LC_PIPLINE_QTY", title: "LC Qty.", width: "5%", headerTemplate: "<b>LC Qty.</b>" },
                { field: "RQD_QTY", title: "Reqd. Qty.", width: "5%", headerTemplate: "<b>Reqd. Qty.</b>" },
                { field: "MOU_CODE", title: "Unit", width: "5%", headerTemplate: "<b>Unit</b>" },
                { field: "LAST_PUR_RATE", title: "Rate/Kg", width: "5%", headerTemplate: "<b>Rate/Kg</b>" },
                //{ field: "TOTAL_AMT", title: "Total Amt", width: "5%", headerTemplate: "<b>Total Amt</b>" },
                { field: "SP_NOTE", title: "Note", width: "15%", headerTemplate: "<b>Note</b>" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
            //editable: true
        };


        vm.printRequisition = function (item) {
            vm.form = item;
            vm.form.REPORT_CODE = 'RPT-3571';

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
        }

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var result = [];
                vm.RequisitionItemList.data().forEach(function (x) { if (!result.includes(x.INV_ITEM_CAT_ID)) result.push(x.INV_ITEM_CAT_ID) })
                var sCat = result.toString();
                data['INV_ITEM_CAT_ID'] = sCat;
                data['IS_ORDER_WISE'] = 'N';
                data['RF_CURRENCY_ID'] = '2';
                data['LK_PURC_PROD_GRP_ID'] = '790';

                data["MC_BUYER_LST"] = !data.MC_BUYER_LST ? '' : data.MC_BUYER_LST.join(',');

                data["XML_REQ_D"] = KnittingDataService.xmlStringShort(vm.RequisitionItemList.data().map(function (o) {
                    return {
                        SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID == null ? 0 : o.SCM_PURC_REQ_D_ID,
                        SCM_PURC_REQ_H_ID: o.SCM_PURC_REQ_H_ID == null ? 0 : o.SCM_PURC_REQ_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID,
                        RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID,
                        LK_LOC_SRC_TYPE_ID: o.LK_LOC_SRC_TYPE_ID == null ? 1 : o.LK_LOC_SRC_TYPE_ID,
                        LK_PRIORITY_ID: o.LK_PRIORITY_ID == null ? 1 : o.LK_PRIORITY_ID,
                        RF_REQ_SRC_ID: o.RF_REQ_SRC_ID == null ? 1 : o.RF_REQ_SRC_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID,
                        SP_NOTE: o.SP_NOTE
                    }
                }));

                data["RequisitionItemList"] = vm.RequisitionItemList.data().map(function (o) {
                    return {
                        SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID == null ? 0 : o.SCM_PURC_REQ_D_ID,
                        SCM_PURC_REQ_H_ID: o.SCM_PURC_REQ_H_ID == null ? 0 : o.SCM_PURC_REQ_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID,
                        RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID,
                        LK_LOC_SRC_TYPE_ID: o.LK_LOC_SRC_TYPE_ID == null ? 1 : o.LK_LOC_SRC_TYPE_ID,
                        LK_PRIORITY_ID: o.LK_PRIORITY_ID == null ? 1 : o.LK_PRIORITY_ID,
                        RF_REQ_SRC_ID: o.RF_REQ_SRC_ID == null ? 1 : o.RF_REQ_SRC_ID,
                        RF_BRAND_ID: o.RF_BRAND_ID,
                        SP_NOTE: o.SP_NOTE
                    }
                });

                //data["XML_REQ_D"] = KnittingDataService.xmlStringShort(vm.RequisitionItemList.data().map(function (o) {
                //    return {
                //        SCM_PURC_REQ_D_ID: o.SCM_PURC_REQ_D_ID == null ? 0 : o.SCM_PURC_REQ_D_ID,
                //        SCM_PURC_REQ_H_ID: o.SCM_PURC_REQ_H_ID == null ? 0 : o.SCM_PURC_REQ_H_ID,
                //        INV_ITEM_ID: o.INV_ITEM_ID,
                //        LK_YRN_COLR_GRP_ID: o.LK_YRN_COLR_GRP_ID,
                //        BUFR_ALOC_QTY: o.BUFR_ALOC_QTY == null ? 0 : o.BUFR_ALOC_QTY,
                //        CAL_RQD_QTY: o.PLAN_RQD_QTY == null ? 0 : o.PLAN_RQD_QTY,
                //        PLAN_RQD_QTY: o.PLAN_RQD_QTY == null ? 0 : o.PLAN_RQD_QTY,
                //        ORD_ADV_ALOC_QTY: o.ORD_ADV_ALOC_QTY == null ? 0 : o.ORD_ADV_ALOC_QTY,
                //        NXT_BUFR_QTY: o.NXT_BUFR_QTY == null ? 0 : o.NXT_BUFR_QTY,
                //        QTY_MOU_ID: o.QTY_MOU_ID,
                //        LK_LOC_SRC_TYPE_ID: o.LK_LOC_SRC_TYPE_ID == null ? 1 : o.LK_LOC_SRC_TYPE_ID,
                //        RF_BRAND_ID: o.RF_BRAND_ID,
                //        LOT_REF_NO: o.LOT_REF_NO == null ? "A" : o.LOT_REF_NO,
                //        SP_NOTE: o.SP_NOTE
                //    }
                //}));

                var id = vm.form.SCM_PURC_REQ_H_ID

                return KnittingDataService.saveDataByUrl(data, '/KnitYarnReq/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg("MULTI-001 Requisition information has been updated successfully");
                            $state.go($state.current, { pSCM_PURC_REQ_H_ID: id });
                        }
                        else {
                            config.appToastMsg('MULTI-001' + res.data.PMSG);
                            if (res.data.OPSCM_PURC_REQ_H_ID)
                                $state.go($state.current, { pSCM_PURC_REQ_H_ID: res.data.OPSCM_PURC_REQ_H_ID });
                        }

                    }
                });
            }
        };
    }


})();