(function () {
    'use strict';
    angular.module('multitex.knitting').controller('DailyMachineOilConsumptionController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', DailyMachineOilConsumptionController]);
    function DailyMachineOilConsumptionController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.KNT_MCN_OIL_ISS_H_ID ? formData : { HR_COMPANY_ID: 1, ISS_REF_DT: vm.today };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [ItemList(), RequisitionList(), GetItemCategoryList(), getOperatorList(), GetReqStoreList(), GetCompanyList(), GetMOUList(), getShiftList(), getMachineList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.GetOilStockByStore = function () {
            if (vm.form.SCM_STORE_ID)
                if (fnValidateSub() == true)
                    KnittingDataService.getDataByFullUrl('/api/knit/MacOilCons/GetOilStockByStore?pSCM_STORE_ID=' + vm.form.SCM_STORE_ID + '&pINV_ITEM_ID=' + vm.formItem.INV_ITEM_ID).then(function (res) {
                        console.log(res);
                        if (res.length > 0) {
                            vm.formItem.STOCK_QTY = res[0].STOCK_QTY;
                            vm.formItem.KG_COST_PRICE = res[0].COST_PRICE;

                        }
                    });
        }

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


        vm.CheckIssueQty = function () {
            if ((vm.formItem.STOCK_QTY || 0) < (vm.formItem.ISS_QTY_KG || 0))
                vm.formItem.ISS_QTY_KG = "";
        }

        function RequisitionList() {
            return vm.RequisitionItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/MacOilCons/GetMacOilConsInfoDtlByID?pKNT_MCN_OIL_ISS_H_ID=' + ($stateParams.pKNT_MCN_OIL_ISS_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

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


        function getShiftList() {

            return vm.shiftList = {
                optionLabel: "--Select Shift--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "SCHEDULE_NAME_EN",
                dataValueField: "HR_SCHEDULE_H_ID"
            };
        };

        //function RequisitionList() {
        //    return vm.RequisitionItemList = new kendo.data.DataSource({
        //        transport: {
        //            read: function (e) {
        //                KnittingDataService.getDataByFullUrl('/api/knit/YarnIssueReq/GetYarnRequisitionDtlByID?pKNT_YRN_STR_REQ_H_ID=' + ($stateParams.pKNT_YRN_STR_REQ_H_ID || 0)).then(function (res) {
        //                    e.success(res);
        //                });
        //            }
        //        }
        //    });
        //};

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

        $scope.ISS_REF_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ISS_REF_DT_LNopened = true;
        }

        vm.categoryBrandList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    KnittingDataService.getDataByFullUrl('/api/common/GetItemBrandList').then(function (res) {
                        var list = _.filter(res, { 'INV_ITEM_CAT_ID': 2 });
                        e.success(list);

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        function ItemList() {
            return vm.DyItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=206').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function GetItemCategoryList() {

            KnittingDataService.getDataByFullUrl('/api/inv/ItemCategory/SelectAll').then(function (res) {
                var filter = _.filter(res, { 'ITEM_CAT_LEVEL': 1 });
                vm.itemCategoryList = new kendo.data.DataSource({
                    data: filter
                });
            });
        }

        vm.getItemDataByCategory = function (e) {
            var itemss = e.sender.dataItem(e.item);
            KnittingDataService.getDataByFullUrl('/api/knit/YarnReceive/GetKnitSTKItemListByCatID?pINV_ITEM_CAT_ID=206').then(function (res) {
                vm.DyItemList = new kendo.data.DataSource({
                    data: res
                });
            });

        };

        vm.addToGrid = function (isValid) {

            if (fnValidateSub2() == true) {
                var item = _.filter(vm.DyItemList.data(), function (o) {
                    return o.INV_ITEM_ID === parseInt(vm.formItem.INV_ITEM_ID)
                })[0];

                vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;

                var mac = _.filter(vm.machineList.data(), function (o) {
                    return o.KNT_MACHINE_ID === parseInt(vm.formItem.KNT_MACHINE_ID)
                })[0];

                vm.formItem.KNT_MACHINE_NO = mac.KNT_MACHINE_NO;
                console.log(vm.operatorDataSource.data());
                var emp = _.filter(vm.operatorDataSource.data(), function (o) {
                    return o.OPERATOR_ID === parseInt(vm.formItem.OPERATOR_ID)
                })[0];
                console.log(emp);

                vm.formItem.EMP_FULL_NAME_EN = emp.EMP_FULL_NAME_EN;

                vm.formItem.SCM_STORE_ID = vm.form.SCM_STORE_ID;
                vm.formItem.HR_SCHEDULE_H_ID = vm.form.HR_SCHEDULE_H_ID;

                var STOCK_QTY = vm.formItem.STOCK_QTY;

                if (vm.formItem.uid) {
                    // Update

                    var row = vm.RequisitionItemList.getByUid(vm.formItem.uid);
                    var count = _.filter(vm.RequisitionItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.uid != vm.formItem.uid)
                    }).length;

                    if (count <= 1) {
                        row["INV_ITEM_ID"] = vm.formItem.INV_ITEM_ID;
                        row["ITEM_NAME_EN"] = vm.formItem.ITEM_NAME_EN;
                        row["KNT_MACHINE_ID"] = vm.formItem.KNT_MACHINE_ID;
                        row["KNT_MACHINE_NO"] = vm.formItem.KNT_MACHINE_NO;
                        row["OPERATOR_ID"] = vm.formItem.OPERATOR_ID;
                        row["EMP_FULL_NAME_EN"] = vm.formItem.EMP_FULL_NAME_EN;
                        row["SCM_STORE_ID"] = vm.formItem.SCM_STORE_ID;
                        row["HR_SCHEDULE_H_ID"] = vm.formItem.HR_SCHEDULE_H_ID;
                        row["STOCK_QTY"] = vm.formItem.STOCK_QTY;
                        row["ISS_QTY_KG"] = vm.formItem.ISS_QTY_KG;

                        vm.formItem = { 'INV_ITEM_ID': '2743', 'OPERATOR_ID': '', 'KNT_MACHINE_ID': '', 'STOCK_QTY': STOCK_QTY };
                    }
                }
                else {
                    var count = _.filter(vm.RequisitionItemList.data(), function (o) {
                        return (o.INV_ITEM_ID === vm.formItem.INV_ITEM_ID && o.OPERATOR_ID === vm.formItem.OPERATOR_ID && o.KNT_MACHINE_ID === vm.formItem.KNT_MACHINE_ID)
                    }).length;

                    if (count == 0) {

                        var idx = vm.RequisitionItemList.data().length + 1;
                        vm.RequisitionItemList.insert(idx, vm.formItem);
                        vm.formItem = { 'INV_ITEM_ID': '2743', 'OPERATOR_ID': '', 'KNT_MACHINE_ID': '', 'STOCK_QTY': STOCK_QTY };

                        var gview = vm.RequisitionItemList.data();
                    }
                }
            }
        };


        vm.removeItemData = function (data) {

            if (!data.KNT_YRN_STR_REQ_D_ID || data.KNT_YRN_STR_REQ_D_ID <= 0) {
                vm.RequisitionItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.RequisitionItemList.remove(data);
                 });

        }





        function GetReqStoreList() {

            return vm.reqStoreList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                console.log(res);
                                var lst = _.filter(res, function (x) { return x.SCM_STORE_ID == 14 || x.SCM_STORE_ID == 13 })
                                e.success(lst);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
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

        vm.formItem = { 'INV_ITEM_ID': 2743 };

        vm.resetItemData = function () {
            vm.formItem = { 'INV_ITEM_ID': 2743 };
        };

        vm.reset = function () {

            $state.go('YarnIssueReq', { pKNT_YRN_STR_REQ_H_ID: 0 });

        };

        vm.editItemData = function (dataItem) {
            vm.formItem = angular.copy(dataItem);
        }

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
                { field: "OPERATOR_ID", hidden: true },
                { field: "KNT_MACHINE_ID", hidden: true },
                { field: "SCM_STORE_ID", hidden: true },
                { field: "HR_SCHEDULE_H_ID", hidden: true },

                { field: "KNT_MACHINE_NO", title: "M/C No", type: "string", width: "7%" },
                { field: "EMP_FULL_NAME_EN", title: "Operator", type: "string", width: "7%" },
                { field: "ITEM_NAME_EN", title: "Yarn Item", type: "string", width: "7%" },
                { field: "STOCK_QTY", title: "Stock Qty", type: "string", width: "7%" },
                { field: "ISS_QTY_KG", title: "Issue Qty", type: "string", width: "7%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };

        vm.submitAll = function (dataOri, flag) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["XML_ISS_D"] = KnittingDataService.xmlStringShort(vm.RequisitionItemList.data().map(function (o) {
                    return {
                        KNT_MCN_OIL_ISS_D_ID: o.KNT_MCN_OIL_ISS_D_ID == null ? 0 : o.KNT_MCN_OIL_ISS_D_ID,
                        KNT_MCN_OIL_ISS_H_ID: o.KNT_MCN_OIL_ISS_H_ID,
                        INV_ITEM_ID: o.INV_ITEM_ID,
                        KNT_MACHINE_ID: o.KNT_MACHINE_ID == 0 ? null : o.KNT_MACHINE_ID,
                        OPERATOR_ID: o.OPERATOR_ID == null ? 3 : o.OPERATOR_ID,
                        ISS_QTY_KG: o.ISS_QTY_KG == null ? 0 : o.ISS_QTY_KG,
                        ISS_QTY_LTR: o.ISS_QTY_LTR == null ? 0 : o.ISS_QTY_LTR,
                        KG_COST_PRICE: o.KG_COST_PRICE == null ? 0 : o.KG_COST_PRICE
                    }
                }));

                var _invdate = $filter('date')(data.ISS_REF_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["ISS_REF_DT"] = _invdate;

                var id = vm.form.KNT_MCN_OIL_ISS_H_ID
                data.IS_FINALIZED = flag;

                return KnittingDataService.saveDataByUrl(data, '/MacOilCons/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pKNT_MCN_OIL_ISS_H_ID: id }, { reload: true });
                        }
                        else {

                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPKNT_MCN_OIL_ISS_H_ID) > 0) {
                                vm.form.KNT_YRN_STR_REQ_H_ID = res.data.OPKNT_YRN_STR_REQ_H_ID;
                                $state.go($state.current, { pKNT_MCN_OIL_ISS_H_ID: res.data.OPKNT_MCN_OIL_ISS_H_ID });
                            }
                        }

                    }
                });
            }
        };






    }

})();

