
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeMcMaintenanceController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', 'Dialog', '$ngConfirm', DyeMcMaintenanceController]);
    function DyeMcMaintenanceController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter, Dialog, $ngConfirm) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.DYE_MCN_STOP_TRAN_ID ? formData : { DYE_MCN_STOP_TRAN_ID: 0, REPORT_BY: cur_user_id, REPORT_TIME: vm.today, STOP_TIME: vm.today };
        vm.formItem = { 'QTY_MOU_ID': '3', 'CENTRAL_STK': 0, 'SUB_STK': 0 };

        vm.form["XML_MCN"] = formData.XML_MCN ? formData.XML_MCN.split(',') : [];
        vm.form["XML_DFCT"] = formData.XML_DFCT ? formData.XML_DFCT.split(',') : [];
        vm.form["RF_RESP_DEPT_ID"] = ($stateParams.pOPTION || 0);

        vm['OPTION'] = ($stateParams.pOPTION || 0);

        //if (vm.form.REPORT_TIME)
        //    vm.form.REPORT_TIME = $filter('REPORT_TIME')(date[vm.form.REPORT_TIME, "dd-MMM-yyyy hh:mm tt"]);;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetResDeptList(), getUserData(), GetItemCategoryList(), GetMachineList($stateParams.pOPTION), ReceiveItemList(), GetReqSourceList(), GetReqTypeList(),
                            GetCompanyList(), GetStatusList(), GetMOUList(), GetSupplierList(), GetMaintenanceTypeList(), GetDefectTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.loadStart = function (id) {
            //vm.today = new Date();
            if (id == 3)
                vm.form.PRB_END_TIME = vm.today;
            else if (id == 2)
                vm.form.PRB_ST_TIME = vm.today;
            else if (id == 1)
                vm.form.REPORT_TIME = vm.today;
        }

        $scope.datePickerOptions = {
            start: "day",
            depth: "year",
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
            format: "dd-MMM-yyyy hh:mm tt"
        };

        $scope.TRAN_DATE_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TRAN_DATE_LNopened = true;
        }


        function GetResDeptList() {
            return vm.resDeptList = {
                optionLabel: "-- Select Department --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/getRespDeptList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    console.log(item);
                //},
                dataTextField: "RESP_DEPT_NAME",
                dataValueField: "RF_RESP_DEPT_ID"
            };
        };

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


        vm.checkMachineCapacity = function (e) {

            if (vm.form.DYE_MACHINE_ID > 0) {
                var item = e.sender.dataItem(e.item);
                console.log(item);
                if (item.D_PRD_CAPACITY < vm.form.TOTAL_WEIGHT) {
                    vm.form.DYE_MACHINE_ID = '';
                }
                else {
                    vm.form.DYE_MACHINE_NO = item.DYE_MACHINE_NO;
                }
            }
        }


        function GetMachineList(id) {
            if (id == 5) {

                return vm.machineList = {
                    optionLabel: "-- Select Machine --",
                    filter: "contains",
                    autoBind: true,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                DyeingDataService.getDataByFullUrl('/api/dye/DFMachine/SelectAll').then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    },
                    dataTextField: "DF_MC_NAME",
                    dataValueField: "DF_MACHINE_ID"
                };
            }
            else if (id == 3) {
                return vm.machineList = {
                    optionLabel: "-- Select Machine --",
                    filter: "contains",
                    autoBind: true,
                    dataSource: {
                        transport: {
                            read: function (e) {
                                DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0').then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    },
                    dataTextField: "DYE_MACHINE_NO",
                    dataValueField: "DYE_MACHINE_ID"
                };
            }
            //DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0').then(function (res) {
            //    vm.machineList = new kendo.data.DataSource({
            //        data: res
            //    });
            //});
        };



        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 34;
            var PARENT_ID = 0;
            if ($stateParams.pADDITION_FLAG > 0)
                vm.form.RF_ACTN_STATUS_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                console.log(res);
                vm.form["ACTN_ROLE_FLAG"] = res.length > 0 ? (res[0].ACTN_ROLE_FLAG || 'DN') : 'DN';
                var sList = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "NA" })
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
                            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                                var lst = _.filter(res, function (x) { return x.ACTN_ROLE_FLAG != "DN" })
                                if (lst.length == 1) {
                                    vm.form.RF_ACTN_VIEW = 0;
                                    vm.form.RF_ACTN_STATUS_ID = lst[0].RF_ACTN_STATUS_ID;
                                    vm.form.ACTN_STATUS_NAME = lst[0].ACTN_STATUS_NAME;
                                    //alert(vm.form.ACTN_STATUS_NAME);
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
                     //vm.ReceiveItemList.remove(data);
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


        function GetDefectTypeList() {

            return vm.defectTypeList = {
                optionLabel: "-- Select Defect Type --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DyeMcMaintenance/DyeMcDefectTypeList?pRF_RESP_DEPT_LST=' + (vm.form.RF_RESP_DEPT_ID || '3,5')).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DFCT_TYPE_NAME_EN",
                dataValueField: "RF_MCN_DFCT_TYPE_ID"
            };
        };



        function GetMaintenanceTypeList() {

            return vm.maintenanceTypeList = {
                optionLabel: "-- Select Maintenance Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(141).then(function (res) {
                                if ($stateParams.pDYE_MCN_STOP_TRAN_ID == 0) {
                                    var list = _.filter(res, function (x) { return x.LOOKUP_DATA_ID == 685 || x.LOOKUP_DATA_ID == 688 })
                                    if ($stateParams.pOPTION == 3)
                                        e.success(list);
                                    else
                                        e.success(res);
                                }
                                else {
                                    e.success(res);
                                }
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

        $scope.prompt = function () {
            $ngConfirm({
                title: 'Prompt',
                contentUrl: 'form.html',
                buttons: {
                    getPassCode: {
                        text: 'Pass Code...',
                        disabled: true,
                        btnClass: 'btn-green',
                        action: function (scope) {
                            $ngConfirm('Hello <strong>' + scope.username + '</strong>, i hope you have a great day!');
                        }
                    },
                    later: function () {
                    }
                },
                onScopeReady: function (scope) {
                    var self = this;
                    scope.textChange = function () {
                        if (scope.username)
                            self.buttons.getPassCode.setDisabled(false);
                        else
                            self.buttons.getPassCode.setDisabled(true);
                    }
                }
            })
        };


        vm.submitAll = function (dataOri, status) {


            if (fnValidate() == true) {
                var data = angular.copy(dataOri);
                console.log(data);

                var _trndate = $filter('date')(data.TRAN_DATE, vm.dtFormat);
                data["TRAN_DATE"] = _trndate;

                var _rptdate = $filter('date')(data.REPORT_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["REPORT_TIME"] = _rptdate;

                var _stopdate = $filter('date')(data.PRB_ST_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["PRB_ST_TIME"] = _stopdate;

                var _startdate = $filter('date')(data.PRB_END_TIME, 'yyyy-MM-ddTHH:mm:ss');
                data["PRB_END_TIME"] = _startdate;

                data["IS_UPDATED"] = status;

                data["XML_MCN"] = !data.XML_MCN ? '' : data.XML_MCN.join(',');
                data["XML_DFCT"] = !data.XML_DFCT ? '' : data.XML_DFCT.join(',');

                if (vm.form.ACTN_ROLE_FLAG == "RA") {
                    $ngConfirm({
                        title: 'Prompt',
                        contentUrl: 'form.html',
                        buttons: {
                            getPassCode: {
                                text: 'OK',
                                disabled: true,
                                btnClass: 'btn-green',
                                action: function (scope) {
                                    //$ngConfirm('Hello <strong>' + scope.passCode + '</strong>, i hope you have a great day!');

                                    if (scope.passCode == "7869") {
                                        return DyeingDataService.saveDataByUrl(data, '/DyeMcMaintenance/Save').then(function (res) {

                                            if (res.success === false) {
                                                vm.errors = res.errors;
                                            }
                                            else {

                                                res['data'] = angular.fromJson(res.jsonStr);

                                                config.appToastMsg(res.data.PMSG);
                                                if (parseInt(res.data.OPDYE_MCN_STOP_TRAN_ID) > 0) {
                                                    vm.form.DYE_MCN_STOP_TRAN_ID = res.data.OPDYE_MCN_STOP_TRAN_ID;
                                                    $state.go($state.current, { pDYE_MCN_STOP_TRAN_ID: res.data.OPDYE_MCN_STOP_TRAN_ID }, { reload: true });
                                                }

                                            }
                                        });
                                    }
                                    else {

                                        config.appToastMsg("MULTI-005 Invalid Pass Code!!!");
                                        return;
                                    }
                                }
                            },
                            later: function () {
                                return;
                            }
                        },
                        onScopeReady: function (scope) {
                            var self = this;
                            scope.textChange = function () {
                                if (scope.passCode)
                                    self.buttons.getPassCode.setDisabled(false);
                                else
                                    self.buttons.getPassCode.setDisabled(true);
                            }
                        }
                    });
                }
                else {

                    return DyeingDataService.saveDataByUrl(data, '/DyeMcMaintenance/Save').then(function (res) {

                        if (res.success === false) {
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDYE_MCN_STOP_TRAN_ID) > 0) {
                                vm.form.DYE_MCN_STOP_TRAN_ID = res.data.OPDYE_MCN_STOP_TRAN_ID;
                                $state.go($state.current, { pDYE_MCN_STOP_TRAN_ID: res.data.OPDYE_MCN_STOP_TRAN_ID }, { reload: true });
                            }

                        }
                    });
                }
            }
        };
    }


})();

