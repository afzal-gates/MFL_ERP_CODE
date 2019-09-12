(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('MachineWashRequisitionController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', 'Dialog', '$filter', MachineWashRequisitionController]);
    function MachineWashRequisitionController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, Dialog, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.DYE_STR_REQ_H_ID ? formData : { REQ_STORE_ID: '0', ISS_STORE_ID: '0', HR_DEPARTMENT_ID: '47', STR_REQ_BY: cur_user_id, STR_REQ_DT: vm.today, RF_ACTN_VIEW: 0 };
        vm.formItem = { 'QTY_MOU_ID': '3', 'CENTRAL_STK': 0, 'SUB_STK': 0 };
        vm.form.INV_ITEM_CAT_ID = 3;
        vm.form.RF_REQ_TYPE_ID = 8;


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), GetItemCategoryList(), ItemList(), getCurrencyList(), getReceipeTemplateList(), ReceiveItemList(), GetReqStoreList(),
                GetReqSourceList(), GetReqTypeList(), GetCompanyList(), GetStatusList(), GetMOUList(), GetSupplierList(), GetSourceTypeList(), GetMachineList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.datePickerOptions = {
            start: "day",
            depth: "year",
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
            format: "dd-MMM-yyyy hh:mm tt"
        };

        function getReceipeTemplateList() {
            return vm.TemplateList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDDosingTemplete').then(function (res) {
                            var list = _.filter(res, function (x) { return x.LK_DC_TMPLT_TYPE_ID === 499 })
                            e.success(list);
                        });
                    }
                },
                //pageSize: 15
            });
        };

        vm.getMachinList = function () {
            if (vm.form.HR_COMPANY_ID) {
                DyeingDataService.getDataByFullUrl('/api/Dye/DyeMachine/GetMachineListByCompany?pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID||1)).then(function (res) {
                    vm.machineList = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
        }

        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-4010';
            vm.form.DYE_STR_REQ_H_ID = dataItem.DYE_STR_REQ_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Dye/DyeReport/PreviewReportRDLC');
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

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
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

        vm.checkStock = function () {
            if ((vm.formItem.CENTRAL_STK + vm.formItem.SUB_STK) < vm.formItem.RQD_QTY) {
                vm.formItem.RQD_QTY = 0;
                config.appToastMsg("MULTI-005 Requisition quantity must less than stock quantity.");
            }
        }

        vm.PreItemCalcRecord = function (preItem) {

            preItem.RQD_QTY = (((preItem.RQD_QTY_G || 0) * 1) + ((preItem.RQD_QTY_K || 0) * 1000) + ((preItem.RQD_QTY_M || 0) * 0.001))
        }

        vm.getStockQty = function (preItem) {
            if (preItem.INV_ITEM_ID > 0) {
                //console.log(preItem);
                if (!preItem.DC_AGENT_ID) {
                    var ic = _.filter(preItem.ItemList, function (x) { return x.INV_ITEM_ID == preItem.INV_ITEM_ID });
                    preItem.DC_AGENT_ID = ic[0].INV_ITEM_CAT_ID;
                    preItem.MOU_CODE = ic[0].MOU_CODE;
                }
                else {
                    var ic = _.filter(preItem.ItemList, function (x) { return x.INV_ITEM_ID == preItem.INV_ITEM_ID });
                    preItem.MOU_CODE = ic[0].MOU_CODE;
                }

                DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalStoreTransfer/GetStockInfoForIssue/' + preItem.INV_ITEM_ID + '/' + vm.form.REQ_STORE_ID).then(function (res) {

                    preItem.CENTRAL_STK = res.CENTRAL_STK;
                    preItem.SUB_STK = res.SUB_STK;
                });
            }
            //var obj = e.sender.dataItem(e.item);
            //if (obj.INV_ITEM_ID > 0) {
            //    if (fnValidateSub2() == true) {
            //        var list = _.filter(vm.DyItemList.data(), { 'INV_ITEM_ID': obj.INV_ITEM_ID });
            //        vm.formItem.BRAND_NAME_EN = list[0].BRAND_NAME_EN;

            //        DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalStoreTransfer/GetStockInfoForIssue/' + obj.INV_ITEM_ID + '/' + vm.form.REQ_STORE_ID).then(function (res) {

            //            vm.formItem.CENTRAL_STK = res.CENTRAL_STK;
            //            vm.formItem.SUB_STK = res.SUB_STK;
            //        });
            //    }
            //    else
            //        vm.formItem["DC_ITEM_ID"] = '';
            //}
        }

        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 10;
            var PARENT_ID = 0;
            if ($stateParams.pADDITION_FLAG > 0)
                vm.form.RF_ACTN_STATUS_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
                console.log("A");
                console.log(res);
                console.log("B");
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

        function ReceiveItemList() {
            vm.showSplash = true;

            if ($stateParams.pDYE_STR_REQ_H_ID > 0) {

                DyeingDataService.getDataByFullUrl('/api/dye/DCIssueRequisition/GetDCIssueRequisitionInfoDtlByID?pDYE_STR_REQ_H_ID=' + ($stateParams.pDYE_STR_REQ_H_ID || 0)).then(function (res) {
                    vm.recipeItemList = res;
                });
                vm.showSplash = false;
            }
            else {
                DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDDosingTempleteDtl?pDYE_DOSE_TMPLT_H_ID=' + vm.form.DYE_DOSE_TMPLT_H_ID).then(function (output) {

                    //DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetDCOtherTemplate?pLK_DC_CONS_TYPE_ID=499').then(function (output) {

                    vm.recipeItemList = output;
                    vm.recipeItemList["ItemList"] = {};

                    vm.recipeItemList["MOU_CODE"] = "";
                    vm.recipeItemList["DOSE_QTY"] = 0;
                    vm.recipeItemList["QTY_MOU_ST"] = 0;
                    vm.recipeItemList["RQD_QTY_K"] = 0;
                    vm.recipeItemList["RQD_QTY_G"] = 0;
                    vm.recipeItemList["RQD_QTY_M"] = 0;
                    vm.recipeItemList["RQD_QTY"] = 0;
                    vm.recipeItemList["SUB_STK"] = 0;
                    vm.recipeItemList["CENTRAL_STK"] = 0;
                    vm.showSplash = false;
                });
            }

        };

        vm.machineWashDetail = function () {
            if (fnValidateSub() == true) {
                if (vm.form.DYE_DOSE_TMPLT_H_ID > 0) {
                    vm.showSplash = true;
                    DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetLDDosingTempleteDtl?pDYE_DOSE_TMPLT_H_ID=' + vm.form.DYE_DOSE_TMPLT_H_ID).then(function (output) {

                        vm.recipeItemList = output;
                        vm.recipeItemList["itemList"] = {};

                        vm.recipeItemList["MOU_CODE"] = "";
                        vm.recipeItemList["DOSE_QTY"] = 0;
                        vm.recipeItemList["QTY_MOU_ST"] = 0;
                        vm.recipeItemList["RQD_QTY_K"] = 0;
                        vm.recipeItemList["RQD_QTY_G"] = 0;
                        vm.recipeItemList["RQD_QTY_M"] = 0;
                        vm.recipeItemList["RQD_QTY"] = 0;
                        vm.recipeItemList["SUB_STK"] = 0;
                        vm.recipeItemList["CENTRAL_STK"] = 0;
                        vm.showSplash = false;
                    });
                }
            }
            else {
                vm.form.DYE_DOSE_TMPLT_H_ID = '';
            }
        }

        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                data: []
            });
        };

        function GetItemCategoryList() {
            return vm.itemCategoryList = {
                optionLabel: "-- Select Agent --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                                var list = _.filter(res, function (o) { return ((o.INV_ITEM_CAT_ID === 3 || o.INV_ITEM_CAT_ID === 4 || o.PARENT_ID === 3 || o.PARENT_ID === 4)) });
                                e.success(list);

                            }, function (err) {
                                console.log(err);
                            });

                        }
                    }
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        };

        vm.getItemListByCategory = function (preItem) {

            return preItem.ItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (preItem.DC_AGENT_ID || 3)).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.moveArray = function (item, idx) {
            if (idx >= 0) {
                var index = vm.recipeItemList.indexOf(item);
                vm.recipeItemList.splice(index, 1);
                vm.recipeItemList.splice(idx, "0", item);
                //vm.recipeItemList.sort();
            }
        }

        vm.addAllNewRecoed = function (PhaseID, indx) {
            //var idx = indx + 1;
            //var item = {
            //    SL_NO: idx,
            //    INV_ITEM_ID: 0,
            //    ITEM_NAME_EN: "--Select Item--",
            //    DYE_PRD_PHASE_TYPE_ID: PhaseID,
            //    MOU_CODE: "",
            //    DOSE_QTY: "0"
            //};
            //vm.recipeItemList.splice(idx, "0", item);
            //vm.recipeItemList.sort();

            var idx = indx + 1;

            var item = {
                SL_NO: idx,
                INV_ITEM_ID: 0,
                ITEM_NAME_EN: "--Select Item--",
                DYE_PRD_PHASE_TYPE_ID: PhaseID,
                MOU_CODE: "",
                DOSE_QTY: "0"
            };

            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=3,4').then(function (res) {
                item["ItemList"] = res;
            });

            console.log(item);
            vm.recipeItemList.splice(idx, "0", item);

        }

        vm.removeExtraRecord = function (data) {

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var index = vm.recipeItemList.indexOf(data);
                     console.log(data);
                     vm.recipeItemList.splice(index, 1);
                     if (data.DYE_STR_REQ_D_ID > 0) {

                     }
                 });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (itemss.DC_AGENT_ID || 3)).then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };


        vm.resetItemData = function () {
            vm.formItem = { DC_ITEM_ID: '', QTY_MOU_ID: '3' };
        };

        vm.reset = function () {

            $state.go('YarnReceive', { pDYE_STR_REQ_H_ID: 0 });

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

        function GetMachineList() {

            DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0').then(function (res) {
                vm.machineList = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetReqTypeByUser').then(function (res) {
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
                optionLabel: "-- Select Issue Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=3,4&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        };

        function GetReqStoreList() {
            return vm.issueStoreList = {
                optionLabel: "-- Select Requisition Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=3,4&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 513 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
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

        vm.getItemListByCatID = function (preItem) {
            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (preItem.DC_AGENT_ID || 3)).then(function (res) {
                preItem["ItemList"] = res;
            });
            //return preItem.ItemList = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {
            //            DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + (preItem.DC_AGENT_ID || 3)).then(function (res) {
            //                e.success(res);

            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    }
            //});

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

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var result = [];
                vm.recipeItemList.forEach(function (x) { if (!result.includes(x.DC_AGENT_ID)) result.push(x.DC_AGENT_ID) })
                var sCat = result.toString();
                data['INV_ITEM_CAT_LST'] = sCat;

                var _reqdate = $filter('date')(data.STR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["STR_REQ_DT"] = _reqdate;

                data["XML_REQ_D"] = DyeingDataService.xmlStringShort(vm.recipeItemList.map(function (o) {
                    return {
                        DYE_STR_REQ_D_ID: o.DYE_STR_REQ_D_ID == null ? 0 : o.DYE_STR_REQ_D_ID,
                        DYE_STR_REQ_H_ID: o.DYE_STR_REQ_H_ID == null ? 0 : o.DYE_STR_REQ_H_ID,
                        DC_ITEM_ID: o.INV_ITEM_ID,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                        RQD_QTY_K: o.RQD_QTY_K == null ? 0 : o.RQD_QTY_K,
                        RQD_QTY_G: o.RQD_QTY_G == null ? 0 : o.RQD_QTY_G,
                        RQD_QTY_M: o.RQD_QTY_M == null ? 0 : o.RQD_QTY_M,
                        RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 4 : o.QTY_MOU_ID

                    }
                }));

                var id = vm.form.DYE_STR_REQ_H_ID

                return DyeingDataService.saveDataByUrl(data, '/DCIssueRequisition/Save').then(function (res) {

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
                                $state.go($state.current, { pDYE_STR_REQ_H_ID: res.data.OPDYE_STR_REQ_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };

        vm.updateAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var result = [];
                vm.recipeItemList.forEach(function (x) { if (!result.includes(x.DC_AGENT_ID)) result.push(x.DC_AGENT_ID) })
                var sCat = result.toString();
                data['INV_ITEM_CAT_LST'] = sCat;

                var _reqdate = $filter('date')(data.STR_REQ_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["STR_REQ_DT"] = _reqdate;

                data["XML_REQ_D"] = DyeingDataService.xmlStringShort(vm.recipeItemList.map(function (o) {
                    return {
                        DYE_STR_REQ_D_ID: o.DYE_STR_REQ_D_ID == null ? 0 : o.DYE_STR_REQ_D_ID,
                        DYE_STR_REQ_H_ID: o.DYE_STR_REQ_H_ID == null ? 0 : o.DYE_STR_REQ_H_ID,
                        DC_ITEM_ID: o.INV_ITEM_ID,
                        PACK_MOU_ID: o.PACK_MOU_ID == null ? 3 : o.PACK_MOU_ID,
                        RQD_QTY_K: o.RQD_QTY_K == null ? 0 : o.RQD_QTY_K,
                        RQD_QTY_G: o.RQD_QTY_G == null ? 0 : o.RQD_QTY_G,
                        RQD_QTY_M: o.RQD_QTY_M == null ? 0 : o.RQD_QTY_M,
                        RQD_QTY: o.RQD_QTY == null ? 0 : o.RQD_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 4 : o.QTY_MOU_ID

                    }
                }));

                var id = vm.form.DYE_STR_REQ_H_ID

                return DyeingDataService.saveDataByUrl(data, '/DCIssueRequisition/Update').then(function (res) {

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
                                $state.go($state.current, { pDYE_STR_REQ_H_ID: res.data.OPDYE_STR_REQ_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };

    }


})();

