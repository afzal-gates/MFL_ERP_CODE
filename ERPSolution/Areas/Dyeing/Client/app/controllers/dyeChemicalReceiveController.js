(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeChemicalReceiveController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$timeout','$filter', DyeChemicalReceiveController]);
    function DyeChemicalReceiveController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $timeout, $filter) {

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
            var promise = [getUserData(), GetItemCategoryList(), ItemList(), getCurrencyList(), ReceiveItemList(),
                           GetReqSourceList(), GetReqTypeList(), GetCompanyList(), GetPaymentMethodList(), GetMOUList(),
                           GetPackMOUList(), GetSupplierList(), GetSourceTypeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.form = formData.DYE_DC_RCV_H_ID ? formData : { HR_COMPANY_ID: '1', SCM_STORE_ID: 6, DC_MRR_DT: vm.today, IS_LC_PNDNG: 'N', ITEM_RCV_BY: cur_user_id };
        vm.formItem = { QTY_MOU_ID: '3', DC_ITEM_ID: "" };

        vm.setPaymentMethod = function () {
            if (vm.form.RF_REQ_TYPE_ID == 2)
                vm.form.RF_PAY_MTHD_ID = 4;
            else if (vm.form.LK_LOC_SRC_TYPE_ID == 453)
                vm.form.RF_PAY_MTHD_ID = 1;
            else
                if (vm.form.RF_PAY_MTHD_ID == 4)
                    vm.form.RF_PAY_MTHD_ID = "";

        }

        vm.TotalReceiveQty = function () {
            if ((vm.formItem.LOOSE_QTY || 0) < (vm.formItem.QTY_PER_PACK || 0))
                if ((vm.formItem.PACK_RCV_QTY > 0 || (vm.formItem.LOOSE_QTY || 0) > 0) && vm.formItem.QTY_PER_PACK > 0)
                    vm.formItem.RCV_QTY = parseFloat(vm.formItem.PACK_RCV_QTY * vm.formItem.QTY_PER_PACK) + parseInt((vm.formItem.LOOSE_QTY || 0));
                else
                    vm.formItem.RCV_QTY = 0;
            else
                vm.formItem.LOOSE_QTY = 0;

            vm.TotalPriceAmount();
        };

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

        $scope.DC_MRR_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.DC_MRR_DT_LNopened = true;
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

        $scope.CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.CHALAN_DT_LNopened = true;
        }

        $scope.IMP_LC_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.IMP_LC_DT_LNopened = true;
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

        vm.getItemBrandName = function (e) {
            
            var obj = e.sender.dataItem(e.item);
            if (obj.INV_ITEM_ID > 0) {
                var list = _.filter(vm.DyItemList.data(), { 'INV_ITEM_ID': obj.INV_ITEM_ID });                
                vm.formItem.BRAND_NAME_EN = list[0].BRAND_NAME_EN;
                vm.formItem.PACK_MOU_ID = obj.PACK_MOU_ID;
                //vm.formItem.QTY_MOU_ID = obj.QTY_MOU_ID;
                vm.formItem.QTY_PER_PACK = obj.PACK_RATIO;
                vm.formItem.PCT_ADDL_PRICE = obj.PCT_ADDL_PRICE;
                vm.formItem.UNIT_PRICE = obj.PURCH_PRICE;
            }
        }

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
                        DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalReceive/GetDyeChemicalReceiveInfoDtlByID?pDYE_DC_RCV_H_ID=' + ($stateParams.pDYE_DC_RCV_H_ID || 0)).then(function (res) {
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

                var packmouUnit = _.filter(vm.PackMOUList.data(), function (o) {
                    return o.RF_MOU_ID === parseInt(vm.formItem.PACK_MOU_ID)
                })[0];

                vm.formItem.PACK_MOU_CODE = packmouUnit.MOU_CODE;

                console.log(mouUnit);

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
                        //row["RF_BRAND_ID"] = vm.formItem.RF_BRAND_ID;
                        row["BRAND_NAME_EN"] = vm.formItem.BRAND_NAME_EN;
                        row["DYE_DC_RCV_H_ID"] = vm.formItem.DYE_DC_RCV_H_ID;
                        row["PACK_MOU_ID"] = vm.formItem.PACK_MOU_ID;
                        row["QTY_MOU_ID"] = vm.formItem.QTY_MOU_ID;
                        row["MOU_CODE"] = vm.formItem.MOU_CODE;
                        row["PACK_MOU_CODE"] = vm.formItem.PACK_MOU_CODE;
                        row["PCT_ADDL_PRICE"] = vm.formItem.PCT_ADDL_PRICE;

                        row["PACK_RCV_QTY"] = vm.formItem.PACK_RCV_QTY;
                        row["QTY_PER_PACK"] = vm.formItem.QTY_PER_PACK;
                        row["RCV_QTY"] = vm.formItem.RCV_QTY;
                        row["UNIT_PRICE"] = vm.formItem.UNIT_PRICE;
                        row["TOTAL_AMT"] = vm.formItem.TOTAL_AMT;
                        row["TOTAL_AMT_BDT"] = vm.formItem.TOTAL_AMT_BDT;

                        row["SP_NOTE"] = vm.formItem.SP_NOTE;

                        vm.formItem = { DC_ITEM_ID: '', QTY_MOU_ID: '3' };

                        //config.appToastMsg("Item information has been updated successfully!");
                    }
                    else {
                        //config.appToastMsg("MULTI-005 Duplicate item name exists!");
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
                        vm.formItem = { DC_ITEM_ID: '', QTY_MOU_ID: '3' };
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

            vm.DyItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=' + data.INV_ITEM_CAT_ID).then(function (res) {
                            e.success(res);
                            var mydata = angular.copy(data);
                            vm.formItem = mydata;
                            //vm.formItem.TOTAL_AMT_BDT = vm.formItem.RCV_QTY * vm.formItem.COST_PRICE; //((vm.form.LOC_CONV_RATE || 0) + (vm.formItem.PCT_ADDL_PRICE * ((vm.form.LOC_CONV_RATE || 0) / 100)));
                            if (vm.form.RF_CURRENCY_ID == 1)
                                vm.formItem.PCT_ADDL_PRICE = 0;
                            vm.formItem.TOTAL_AMT_BDT = vm.formItem.RCV_QTY * parseFloat(vm.form.LOC_CONV_RATE) * (parseFloat(vm.formItem.UNIT_PRICE) + (parseFloat(vm.formItem.UNIT_PRICE) * (vm.formItem.PCT_ADDL_PRICE / 100)))
                        });
                    }
                }
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

            vm.formItem = {};
            vm.formItem['DC_ITEM_ID'] = '';
            vm.formItem['HR_COUNTRY_ID'] = '';
            vm.formItem['PACK_MOU_ID'] = '';
            vm.formItem['QTY_MOU_ID'] = '3';

        };

        vm.reset = function () {

            $state.go('YarnReceive', { pDYE_DC_RCV_H_ID: 0 });

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
                                var list = _.filter(res, function (o) { return o.IS_R_I == "R" && o.RF_REQ_TYPE_ID == 1  });
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

            commonDataService.getCurrencyList().then(function (res) {
                vm.currencyList = new kendo.data.DataSource({
                    data: res
                });
            });

            //return vm.currencyList = {
            //    optionLabel: "--Select Currency--",
            //    filter: "contains",
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                commonDataService.getCurrencyList().then(function (res) {
            //                    e.success(res);
            //                });
            //            }
            //        }
            //    },
            //    dataTextField: "CURR_NAME_EN",
            //    dataValueField: "RF_CURRENCY_ID"
            //};
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

        function GetPackMOUList() {
            return vm.PackMOUList = new kendo.data.DataSource({
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
                { field: "RF_BRAND_ID", hidden: true },
                { field: "DYE_DC_RCV_H_ID", hidden: true },
                { field: "DYE_DC_RCV_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "PACK_MOU_ID", hidden: true },
                { field: "LOC_UNIT_PRICE", hidden: true },
                { field: "PCT_ADDL_PRICE", hidden: true },
                { field: "INV_ITEM_CAT_ID", hidden: true },

                { field: "ITEM_NAME_EN", title: "Item Name", type: "string", width: "15%" },
                { field: "BRAND_NAME_EN", title: "Brand", type: "string", width: "7%" },
                { field: "PACK_RCV_QTY", title: "Rcv Pack", type: "string", width: "7%" },
                { field: "PACK_MOU_CODE", title: "Pack Unit", type: "string", width: "7%" },
                { field: "QTY_PER_PACK", title: "Kg/Pack", type: "string", width: "7%" },
                { field: "LOOSE_QTY", title: "Loose Kg", type: "string", width: "7%" },

                { field: "RCV_QTY", title: "Total Rcv Qty.", type: "string", width: "7%" },
                { field: "MOU_CODE", title: "UoM", type: "string", width: "7%" },
                { field: "UNIT_PRICE", title: "Rate/Kg", type: "decimal", width: "7%" },
                { field: "TOTAL_AMT", title: "TTL Value", type: "decimal", width: "7%" },
                { field: "TOTAL_AMT_BDT", title: "TTL BDT", type: "decimal", width: "7%" },
                //{ field: "SP_NOTE", title: "Note", width: "12%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem,0)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                if (!data.IS_FINALIZED)
                    data["IS_FINALIZED"] = "N";

                var _clndate = $filter('date')(data.CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["CHALAN_DT"] = _clndate;

                var _mrrdate = $filter('date')(data.DC_MRR_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["DC_MRR_DT"] = _mrrdate;

                var _bldate = $filter('date')(data.BL_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["BL_DT"] = _bldate;

                var _invdate = $filter('date')(data.INVOICE_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["INVOICE_DT"] = _invdate;

                var _Lcdate = $filter('date')(data.IMP_LC_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["IMP_LC_DT"] = _Lcdate;

                data["XML_RCV_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        DYE_DC_RCV_D_ID: o.DYE_DC_RCV_D_ID == null ? 0 : o.DYE_DC_RCV_D_ID,
                        DYE_DC_RCV_H_ID: o.DYE_DC_RCV_H_ID == null ? 0 : o.DYE_DC_RCV_H_ID,
                        DC_ITEM_ID: o.DC_ITEM_ID,
                        PACK_RCV_QTY: o.PACK_RCV_QTY,
                        PACK_MOU_ID: o.PACK_MOU_ID,

                        PCT_ADDL_PRICE: o.PCT_ADDL_PRICE,
                        COST_PRICE: parseFloat(vm.form.LOC_CONV_RATE) * (parseFloat(o.UNIT_PRICE) + (parseFloat(o.UNIT_PRICE) * (o.PCT_ADDL_PRICE / 100))),

                        QTY_PER_PACK: o.QTY_PER_PACK == null ? 0 : o.QTY_PER_PACK,
                        LOOSE_QTY: o.LOOSE_QTY == null ? 0 : o.LOOSE_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                        UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 0 : o.QTY_MOU_ID,
                        LOC_UNIT_PRICE: o.LOC_UNIT_PRICE == null ? 0 : o.LOC_UNIT_PRICE,
                        SP_NOTE: o.SP_NOTE

                    }
                }));

                var id = vm.form.DYE_DC_RCV_H_ID

                return DyeingDataService.saveDataByUrl(data, '/DyeChemicalReceive/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go('DyeChemicalReceive', { pDYE_DC_RCV_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDYE_DC_RCV_H_ID) > 0) {
                                vm.form.DYE_DC_RCV_H_ID = res.data.OPDYE_DC_RCV_H_ID;
                                $state.go($state.current, { pDYE_DC_RCV_H_ID: res.data.OPDYE_DC_RCV_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };


        vm.submitFinal = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["XML_RCV_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                    return {
                        DYE_DC_RCV_D_ID: o.DYE_DC_RCV_D_ID == null ? 0 : o.DYE_DC_RCV_D_ID,
                        DYE_DC_RCV_H_ID: o.DYE_DC_RCV_H_ID == null ? 0 : o.DYE_DC_RCV_H_ID,
                        DC_ITEM_ID: o.DC_ITEM_ID,
                        PACK_RCV_QTY: o.PACK_RCV_QTY,
                        PACK_MOU_ID: o.PACK_MOU_ID,

                        PCT_ADDL_PRICE: o.PCT_ADDL_PRICE,
                        COST_PRICE: parseFloat(vm.form.LOC_CONV_RATE) * (parseFloat(o.UNIT_PRICE) + (parseFloat(o.UNIT_PRICE) * (o.PCT_ADDL_PRICE / 100))),

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

                var _mrrdate = $filter('date')(data.DC_MRR_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["DC_MRR_DT"] = _mrrdate;

                var _bldate = $filter('date')(data.BL_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["BL_DT"] = _bldate;

                var _invdate = $filter('date')(data.INVOICE_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["INVOICE_DT"] = _invdate;
                
                var _Lcdate = $filter('date')(data.IMP_LC_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["IMP_LC_DT"] = _Lcdate;

                var id = vm.form.DYE_DC_RCV_H_ID

                return DyeingDataService.saveDataByUrl(data, '/DyeChemicalReceive/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go('DyeChemicalReceive', { pDYE_DC_RCV_H_ID: id }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDYE_DC_RCV_H_ID) > 0) {
                                vm.form.DYE_DC_RCV_H_ID = res.data.OPDYE_DC_RCV_H_ID;
                                $state.go($state.current, { pDYE_DC_RCV_H_ID: res.data.OPDYE_DC_RCV_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };
    }


})();

