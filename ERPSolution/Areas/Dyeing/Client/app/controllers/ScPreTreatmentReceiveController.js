
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('ScPreTreatmentReceiveController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', 'Dialog', '$filter', '$modal', ScPreTreatmentReceiveController]);
    function ScPreTreatmentReceiveController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, Dialog, $filter, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};
        console.log(cur_user_id);
        $scope['IS_ACTIVE'] = cur_user_id == 252 ? true : false;
        console.log($scope.IS_ACTIVE);

        vm.form = formData.DF_SC_PT_RCV_H_ID ? formData : { IS_TRANSFER: 'N', SCM_STORE_ID: 24, STR_REQ_BY: cur_user_id, RCV_DT: vm.today, RF_ACTN_VIEW: 0, items: [] };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getUserData(), ReceiveItemList(), GetReqTypeList(), GetReqSourceList(),
                GetCompanyList(), GetStatusList(), GetMOUList(), GetSupplierList(), getDfProcessTypeData()
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.attachFile = [{ FILE_NAME: '', FILES_0: null }];

        vm.search = function () {
            vm.gridOptionsDS.read();
        }

        vm.loadIssueList = function () {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl('/api/Dye/ScPreTreatment/ScPtChallanBySupplierID?&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0) + '&pSC_PRG_NO=' + (vm.form.SC_PRG_NO || '')).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                click: function (x) {
                    alert("");
                }
            })
        }

        vm.itemList = [];

        function onChange(arg) {

            var grid = $("#scPreTreatmentGrid").data("kendoGrid");
            var row = grid.select();
            var item = grid.dataItem(row);
            DyeingDataService.getDataByFullUrl('/api/dye/ScPreTreatment/GetScPtIssueChallanByID?pDF_SC_PT_CHL_ISS_H_ID=' + item.DF_SC_PT_CHL_ISS_H_ID).then(function (res) {

                console.log(res[0]);
                var list = res.map(function (o) {
                    return {
                        BUYER_NAME_EN: o.BUYER_NAME_EN,
                        COLOR_NAME_EN: o.COLOR_NAME_EN,
                        DF_PROC_TYPE_LST: o.DF_PROC_TYPE_LST ? o.DF_PROC_TYPE_LST.split(',') : [],
                        DF_SC_PT_CHL_ISS_H_ID: o.DF_SC_PT_CHL_ISS_H_ID,
                        DF_SC_PT_CHL_ISS_D_ID: o.DF_SC_PT_CHL_ISS_D_ID,
                        DF_SC_PT_ISS_D1_ID: o.DF_SC_PT_ISS_D1_ID,
                        FAB_ITEM_DESC: o.FAB_ITEM_DESC,
                        SC_PRG_NO: o.SC_PRG_NO,
                        ISS_QTY: o.ISS_QTY,
                        ISS_ROLL_QTY: o.ISS_ROLL_QTY,
                        IS_SELECT: o.IS_SELECT,
                        IS_TRANSFR: o.IS_TRANSFR,
                        KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID,
                        LK_DIA_TYPE_NAME: o.LK_DIA_TYPE_NAME,
                        MC_ORDER_NO_LST: o.MC_ORDER_NO_LST,
                        PRE_RCV_QTY: o.PRE_RCV_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID,
                        RQD_GSM: o.RQD_GSM,
                        STYLE_NO: o.STYLE_NO,
                        RQD_QTY: o.RQD_QTY
                    }
                });
                vm.itemList = list;
            });

        }

        function getDfProcessTypeData() {
            return vm.processTypeList = {
                optionLabel: "-- Select Process --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByUrl('/DFProcessType/SelectAll').then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_PROC_SUB_GRP_ID == 576 || x.LK_PROC_SUB_GRP_ID == 578 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "DF_PROC_TYPE_NAME",
                dataValueField: "DF_PROC_TYPE_ID"
            };

        };


        vm.gridOptions = {

            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            change: onChange,
            selectable: true,
            sortable: true,
            scrollable: true,
            height: '300px',
            columns: [

                { field: "DF_SC_PT_ISS_H_ID", hidden: true },
                { field: "CHALAN_NO", title: "Challan No", type: "string", width: "60%" },
                { field: "CHALAN_DT", title: "Challan Date", type: "date", template: "#= kendo.toString(kendo.parseDate(CHALAN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "40%", filter: true },
                //{ field: "GATE_PASS_NO", title: "Gate Pass No", type: "string", width: "10%" },
            ]
        };


        function ReceiveItemList() {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/dye/ScPtReceive/GetScPtRcvDtlByID?pDF_SC_PT_RCV_H_ID=' + ($stateParams.pDF_SC_PT_RCV_H_ID || 0) + '&pIS_TRANSFER=' + (formData.IS_TRANSFER || 'N')).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                aggregate: [
                    { field: "ISS_QTY", aggregate: "sum" },
                    { field: "ISS_ROLL_QTY", aggregate: "sum" },
                    { field: "RCV_ROLL_QTY", aggregate: "sum" },
                    { field: "RCV_GREY_QTY", aggregate: "sum" },
                    { field: "RCV_FAB_QTY", aggregate: "sum" },
                    { field: "RTN_FAB_QTY", aggregate: "sum" },
                    { field: "LOSS_QTY", aggregate: "sum" },
                ],
                schema: {
                    model: {
                        id: "DF_SC_PT_CHL_ISS_D_ID",
                        fields: {
                            BUYER_NAME_EN: { type: "string" },
                            STYLE_NO: { type: "string" },
                            MC_ORDER_NO_LST: { type: "string" },
                            COLOR_NAME_EN: { type: "string" },
                            FAB_ITEM_DESC: { type: "string" },
                            ISS_QTY: { type: "number" },
                            ISS_ROLL_QTY: { type: "number" },
                            RCV_ROLL_QTY: { type: "number" },
                            RCV_GREY_QTY: { type: "number" },
                            RCV_FAB_QTY: { type: "number" },
                            RTN_FAB_QTY: { type: "number" },
                            LOSS_QTY: { type: "number" },
                        }

                    }
                },
                //group: {
                //    field: "ITEM_NAME_EN", title: "Yarn", aggregates: [{ field: "RQD_QTY", aggregate: "sum" }]
                //},
            });
        };

        vm.gridOptionsItem = {
            pageable: false,
            filterable: false,
            //height: '300px',
            scrollable: false,
            columns: [
                { field: "DF_SC_PT_RTN_D_ID", hidden: true },
                { field: "DF_SC_PT_RCV_D_ID", hidden: true },
                { field: "DF_SC_PT_RCV_H_ID", hidden: true },
                { field: "DF_SC_PT_ISS_H_ID", hidden: true },
                { field: "KNT_STYL_FAB_ITEM_ID", hidden: true },
                { field: "DF_SC_PT_CHL_ISS_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "PLOSS_QTY", hidden: true },
                { field: "DF_PROC_TYPE_LST", hidden: true },
                { field: "NXT_DF_PROC_TYPE_LST", hidden: true },
                { field: "RTN_ROLL_QTY", hidden: true },

                { field: "BUYER_NAME_EN", title: "Buyer Name", type: "string", width: "10%" },
                { field: "STYLE_NO", title: "Style", type: "string", width: "10%" },
                { field: "MC_ORDER_NO_LST", title: "Order", type: "string", width: "10%" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "5%" },
                { field: "FAB_ITEM_DESC", title: "Fabric Description", type: "string", width: "20%" },

                {
                    field: "ISS_QTY", title: "Issue Kg", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },
                {
                    field: "ISS_ROLL_QTY", title: "Issue Roll.", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },
                {
                    field: "RCV_ROLL_QTY", title: "Rcv Roll", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },
                {
                    field: "RCV_GREY_QTY", title: "Rcv Gray Qty", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },
                {
                    field: "RCV_FAB_QTY", title: "Rcv Qty", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },
                {
                    field: "RTN_FAB_QTY", title: "Rtn Qty", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },
                {
                    title: "Rtn Roll",
                    template: '<a title="Open" ng-disabled="!dataItem.RTN_FAB_QTY>0" ng-click="vm.openRollModal(dataItem)" class="btn btn-xs green"><i class="fa fa-recycle"> Roll</i></a>',
                    width: "5%"
                },
                {
                    field: "LOSS_QTY", title: "Loss Qty", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },

                { field: "NOTE", title: "Note", type: "string", width: "5%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem,0)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };

        vm.editItemData = function (dataItem) {
            vm.itemList = [];
            var item = angular.copy(dataItem);
            item["DF_PROC_TYPE_LST"] = item.DF_PROC_TYPE_LST ? item.DF_PROC_TYPE_LST.split(',') : [];
            item["NXT_DF_PROC_TYPE_LST"] = item.NXT_DF_PROC_TYPE_LST ? item.NXT_DF_PROC_TYPE_LST.split(',') : [];
            item["IS_SELECT"] = true;
            vm.itemList.push(item);
        }

        vm.addToGrid = function () {

            if (fnValidateSub() == true) {
                for (var i = 0; i < vm.itemList.length; i++) {

                    var idx = vm.ReceiveItemList.data().length + 1;

                    var obj = vm.itemList[i];
                    if (obj.IS_SELECT == true) {
                        if (vm.form.IS_TRANSFER != 'X') {
                            obj['DF_PROC_TYPE_LST'] = obj.DF_PROC_TYPE_LST ? obj.DF_PROC_TYPE_LST.join(',') : '0';
                            obj['NXT_DF_PROC_TYPE_LST'] = obj.NXT_DF_PROC_TYPE_LST ? obj.NXT_DF_PROC_TYPE_LST.join(',') : '0';
                        }
                        else {
                            obj['DF_PROC_TYPE_LST'] = '';
                            obj['NXT_DF_PROC_TYPE_LST'] = '';
                        }

                        if (obj.DF_SC_PT_RCV_D_ID > 0) {
                            var row = vm.ReceiveItemList.getByUid(obj.uid);
                            console.log(row);

                            row["BUYER_NAME_EN"] = obj.BUYER_NAME_EN;
                            row["BYR_ACC_GRP_NAME_EN"] = obj.BYR_ACC_GRP_NAME_EN;
                            row["BYR_ACC_NAME_EN"] = obj.BYR_ACC_NAME_EN;
                            row["COLOR_NAME_EN"] = obj.COLOR_NAME_EN;
                            row["DF_PROC_TYPE_LST"] = obj.DF_PROC_TYPE_LST;
                            row["DF_PROC_TYPE_NAME_LIST"] = obj.DF_PROC_TYPE_NAME_LIST;
                            row["DF_SC_PT_CHL_ISS_D_ID"] = obj.DF_SC_PT_CHL_ISS_D_ID;
                            row["DF_SC_PT_RCV_D_ID"] = obj.DF_SC_PT_RCV_D_ID;
                            row["DF_SC_PT_RCV_H_ID"] = obj.DF_SC_PT_RCV_H_ID;
                            row["DIA_TYPE_NAME"] = obj.DIA_TYPE_NAME;
                            row["FABRIC_DESC"] = obj.FABRIC_DESC;
                            row["FAB_ITEM_DESC"] = obj.FAB_ITEM_DESC;
                            row["FAB_PROD_CAT_NAME"] = obj.FAB_PROD_CAT_NAME;
                            row["FAB_TYPE_NAME"] = obj.FAB_TYPE_NAME;
                            row["FIB_COMP_NAME"] = obj.FIB_COMP_NAME;
                            row["FIN_DIA"] = obj.FIN_DIA;
                            row["FIN_GSM"] = obj.FIN_GSM;
                            row["ISS_QTY"] = obj.ISS_QTY;
                            row["ISS_ROLL_QTY"] = obj.ISS_ROLL_QTY;
                            row["KNT_STYL_FAB_ITEM_ID"] = obj.KNT_STYL_FAB_ITEM_ID;
                            row["KNT_YRN_LOT_LST"] = obj.KNT_YRN_LOT_LST;
                            row["LOSS_QTY"] = obj.LOSS_QTY;
                            row["MC_ORDER_NO_LST"] = obj.MC_ORDER_NO_LST;
                            row["NXT_DF_PROC_TYPE_LST"] = obj.NXT_DF_PROC_TYPE_LST;
                            row["PLOSS_QTY"] = obj.PLOSS_QTY;
                            row["QTY_MOU_ID"] = obj.QTY_MOU_ID;
                            row["RCV_FAB_QTY"] = obj.RCV_FAB_QTY;
                            row["RCV_GREY_QTY"] = obj.RCV_GREY_QTY;
                            row["RCV_ROLL_QTY"] = obj.RCV_ROLL_QTY;
                            row["RTN_FAB_QTY"] = obj.RTN_FAB_QTY;
                            row["SC_PRG_NO"] = obj.SC_PRG_NO;
                            row["STYLE_NO"] = obj.STYLE_NO;

                            vm.itemList = [];
                        }
                        else {
                            var lst = _.filter(vm.ReceiveItemList.data(), function (x) { return x.KNT_STYL_FAB_ITEM_ID == obj.KNT_STYL_FAB_ITEM_ID && x.DF_SC_PT_CHL_ISS_D_ID == obj.DF_SC_PT_CHL_ISS_D_ID })
                            if (lst.length == 0) {
                                vm.ReceiveItemList.insert(idx, obj);
                            }
                            else {
                                config.appToastMsg("MULTI-005 Duplicate Records!");
                                return;
                            }
                        }
                    }
                }
            }

        }


        vm.removeItemData = function (data) {

            if (!data.DF_SC_PT_RCV_D_ID || data.DF_SC_PT_RCV_D_ID <= 0) {
                vm.ReceiveItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ORDER_NO_LIST + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.ReceiveItemList.remove(data);
                 });
        }



        vm.addToDocGrid = function (data) {
            data['RPT_PATH_URL'] = new Date().getTime() + getExtention(data.ATT_FILE.name);
            vm.form.items.push(data);
            if (vm.slFileAreaShow && vm.form.items.length > 0) {
                vm.disableSubmit = false;
            } else {
                vm.disableSubmit = true;
            }
            vm.file = {};
        }


        function getExtention(fileName) {
            var i = fileName.lastIndexOf('.');
            if (i === -1) return false;
            return fileName.slice(i)
        }


        vm.removeAddedDoc = function (index) {
            if (vm.form.items[index].HR_SL_DOCS_ID > 0) {
                vm.form.items[index].REMOVE = 'Y';
            } else {
                vm.form.items.splice(index, 1);
            }

            if (vm.slFileAreaShow && vm.form.items.length > 0) {
                vm.disableSubmit = false;
            } else {
                vm.disableSubmit = true;
            }

        }

        $scope.RCV_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RCV_DT_LNopened = true;
        }

        $scope.CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.CHALAN_DT_LNopened = true;
        }


        vm.checkIssueStock = function (item) {
            if (parseFloat(item.ISS_QTY) < (parseFloat(item.RCV_GREY_QTY) + parseFloat((item.PRE_RCV_QTY || 0)))) {
                item.RCV_GREY_QTY = '';
                config.appToastMsg("MULTI-005 Quantity must less than Issue quantity.");
            }
            else {
                item.LOSS_QTY = (parseFloat(item.RCV_GREY_QTY) - parseFloat(item.RCV_FAB_QTY));
                item.PLOSS_QTY = parseFloat((100 / parseFloat(item.RCV_GREY_QTY)) * parseFloat(item.LOSS_QTY)).toFixed(2);
            }
            if (parseFloat(item.RCV_GREY_QTY) < parseFloat(item.RCV_FAB_QTY)) {
                item.RCV_FAB_QTY = '';
                config.appToastMsg("MULTI-005 Quantity must less than receive grey quantity.");
                return
            }
        }

        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 31;
            var PARENT_ID = 0;
            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
            var sList = null;
            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
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

        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetReqTypeByUser').then(function (res) {
                                if (res.length == 1) {
                                    vm.form.RF_REQ_TYPE_ID = res[0].RF_REQ_TYPE_ID;
                                }
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
                optionLabel: "-- Select Receiving Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5,7&pSC_USER_ID=' + cur_user_id).then(function (res) {
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

        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/knit/KnitPlan/getCompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
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


            return vm.supplierList = {
                optionLabel: "-- Select Party --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID"
            };

        };


        vm.openRollModal = function (item) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ScPreTreatmentReturn.html',
                controller: function ($scope, $modalInstance, DyeingDataService, formData) {

                    var vmDF = this;

                    $scope.ReturnRollList = (formData.RTN_ROLL_LST || []);

                    $scope.checkRollStatus = function () {

                        var ctrl = $scope.FAB_ROLL_NO;
                        if (ctrl.length == 13) {
                            var modelValue = angular.copy(ctrl);
                            DyeingDataService.getDataByUrl('/ScPtReceive/GetScPtRtnRollInfo?pKNT_STYL_FAB_ITEM_ID=' + formData.KNT_STYL_FAB_ITEM_ID + '&pFAB_ROLL_NO=' + modelValue).then(function (res) {
                                console.log(res);

                                if (res.length > 0) {

                                    var obj = {
                                        DF_SC_PT_RTN_ROLL_ID: 0,
                                        DF_SC_PT_RCV_D_ID: 0,
                                        KNT_FAB_ROLL_ID: res[0].KNT_FAB_ROLL_ID,
                                        FAB_ROLL_NO: res[0].FAB_ROLL_NO,
                                        RCV_ROLL_WT: res[0].RCV_ROLL_WT,
                                        WT_MOU_ID: 3,
                                    }

                                    var idx = $scope.ReturnRollList.length + 1;
                                    var lst = _.filter($scope.ReturnRollList, function (x) { return x.FAB_ROLL_NO == obj.FAB_ROLL_NO })
                                    if (lst.length == 0) {
                                        $scope.ReturnRollList.splice(idx, "0", angular.copy(obj));
                                    }
                                    else {
                                        config.appToastMsg("MULTI-005 Duplicate Records!");
                                        return;
                                    }
                                }
                                else {
                                    config.appToastMsg("MULTI-005 Invalid Roll!!!");
                                    return;

                                }
                            });
                            $scope.FAB_ROLL_NO = '';
                        }
                        else {
                            return false;
                        }
                    };

                    $scope.removeRoll = function (row) {
                        var index = $scope.ReturnRollList.indexOf(row);
                        $scope.ReturnRollList.splice(index, 1);
                    }

                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                    $scope.submitAll = function () {
                        var dataOri = angular.copy(formData);
                        dataOri['RTN_ROLL_LST'] = _.filter($scope.ReturnRollList, function (x) { return x.KNT_FAB_ROLL_ID > 0 });
                        $modalInstance.close(dataOri);

                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function (DyeingDataService) {
                        console.log(item);
                        return item;
                    }
                }
            });

            modalInstance.result.then(function (List) {
                console.log(List);

                if (List) {
                    item['RTN_ROLL_LST'] = List.RTN_ROLL_LST;
                }
            });

        };


        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6005';
            vm.form.DF_SC_PT_CHL_ISS_H_ID = dataItem.DF_SC_PT_CHL_ISS_H_ID;
            //vm.form.DYE_GSTR_REQ_H_ID = dataItem.DYE_GSTR_REQ_H_ID;

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

        vm.submitAll = function (data, IsFinal) {

            if (fnValidate() == true) {

                var dataOri = angular.copy(data);

                var _revdate = $filter('date')(dataOri.RCV_DT, 'yyyy-MM-ddTHH:mm:ss');
                dataOri["RCV_DT"] = _revdate;
                dataOri["IS_FINALIZED"] = IsFinal;

                var _clnate = $filter('date')(dataOri.CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                dataOri["CHALAN_DT"] = _clnate;

                if (dataOri.IS_TRANSFER == 'X') {

                    dataOri["XML_RTN_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                        return {
                            DF_SC_PT_RTN_D_ID: o.DF_SC_PT_RTN_D_ID == null ? 0 : o.DF_SC_PT_RTN_D_ID,
                            DF_SC_PT_RCV_H_ID: o.DF_SC_PT_RCV_H_ID == null ? 0 : o.DF_SC_PT_RCV_H_ID,
                            KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_ID,
                            DF_SC_PT_CHL_ISS_D_ID: o.DF_SC_PT_CHL_ISS_D_ID == 0 ? null : o.DF_SC_PT_CHL_ISS_D_ID,
                            RTN_ROLL_QTY: o.RTN_ROLL_QTY == null ? 0 : o.RTN_ROLL_QTY,
                            RTN_FAB_QTY: o.RTN_FAB_QTY == null ? 0 : o.RTN_FAB_QTY,
                            QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID

                        }
                    }));
                }
                else {
                    dataOri["XML_RCV_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                        return {
                            DF_SC_PT_RCV_D_ID: o.DF_SC_PT_RCV_D_ID == null ? 0 : o.DF_SC_PT_RCV_D_ID,
                            DF_SC_PT_RCV_H_ID: o.DF_SC_PT_RCV_H_ID == null ? 0 : o.DF_SC_PT_RCV_H_ID,
                            DF_SC_PT_CHL_ISS_D_ID: o.DF_SC_PT_CHL_ISS_D_ID,
                            KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_ID,
                            RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                            RCV_GREY_QTY: o.RCV_GREY_QTY == null ? 0 : o.RCV_GREY_QTY,
                            RCV_FAB_QTY: o.RCV_FAB_QTY == null ? 0 : o.RCV_FAB_QTY,
                            RTN_FAB_QTY: o.RTN_FAB_QTY == null ? 0 : o.RTN_FAB_QTY,
                            LOSS_QTY: o.LOSS_QTY == null ? 0 : o.LOSS_QTY,
                            PLOSS_QTY: o.PLOSS_QTY == null ? 0 : o.PLOSS_QTY,
                            QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID,
                            DF_PROC_TYPE_LST: o.DF_PROC_TYPE_LST,
                            NXT_DF_PROC_TYPE_LST: o.NXT_DF_PROC_TYPE_LST == null ? 3 : o.NXT_DF_PROC_TYPE_LST,
                            RTN_ROLL_LST: o.RTN_ROLL_LST == null ? "" : config.xmlStringShortNoTag(o.RTN_ROLL_LST.map(function (o) {
                                return {
                                    DF_SC_PT_RTN_ROLL_ID: o.DF_SC_PT_RTN_ROLL_ID,
                                    DF_SC_PT_RCV_D_ID: o.DF_SC_PT_RCV_D_ID,
                                    KNT_FAB_ROLL_ID: o.KNT_FAB_ROLL_ID,
                                    RCV_ROLL_WT: o.RCV_ROLL_WT,
                                    WT_MOU_ID: (o.WT_MOU_ID || 3)
                                }
                            })),
                            NOTE: o.NOTE,
                        }
                    }));
                }
                var id = vm.form.DF_SC_PT_RCV_H_ID

                return DyeingDataService.saveDataByUrl(dataOri, '/ScPtReceive/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pDF_SC_PT_RCV_H_ID: res.data.OPDF_SC_PT_RCV_H_ID }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDF_SC_PT_RCV_H_ID) > 0) {
                                vm.form.DF_SC_PT_RCV_H_ID = res.data.OPDF_SC_PT_RCV_H_ID;
                                $state.go($state.current, { pDF_SC_PT_RCV_H_ID: res.data.OPDF_SC_PT_RCV_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };


        vm.updateAll = function (data, IsFinal) {

            if (fnValidate() == true) {

                var dataOri = angular.copy(data);

                var _revdate = $filter('date')(dataOri.RCV_DT, 'yyyy-MM-ddTHH:mm:ss');
                dataOri["RCV_DT"] = _revdate;
                dataOri["IS_FINALIZED"] = IsFinal;

                var _clnate = $filter('date')(dataOri.CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                dataOri["CHALAN_DT"] = _clnate;

                if (dataOri.IS_TRANSFER == 'X') {

                    dataOri["XML_RTN_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                        return {
                            DF_SC_PT_RTN_D_ID: o.DF_SC_PT_RTN_D_ID == null ? 0 : o.DF_SC_PT_RTN_D_ID,
                            DF_SC_PT_RCV_H_ID: o.DF_SC_PT_RCV_H_ID == null ? 0 : o.DF_SC_PT_RCV_H_ID,
                            KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_ID,
                            DF_SC_PT_CHL_ISS_D_ID: o.DF_SC_PT_CHL_ISS_D_ID == 0 ? null : o.DF_SC_PT_CHL_ISS_D_ID,
                            RTN_ROLL_QTY: o.RTN_ROLL_QTY == null ? 0 : o.RTN_ROLL_QTY,
                            RTN_FAB_QTY: o.RTN_FAB_QTY == null ? 0 : o.RTN_FAB_QTY,
                            QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID

                        }
                    }));
                }
                else {
                    dataOri["XML_RCV_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                        return {
                            DF_SC_PT_RCV_D_ID: o.DF_SC_PT_RCV_D_ID == null ? 0 : o.DF_SC_PT_RCV_D_ID,
                            DF_SC_PT_RCV_H_ID: o.DF_SC_PT_RCV_H_ID == null ? 0 : o.DF_SC_PT_RCV_H_ID,
                            DF_SC_PT_ISS_H_ID: o.DF_SC_PT_ISS_H_ID,
                            KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_ID,
                            RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                            RCV_GREY_QTY: o.RCV_GREY_QTY == null ? 0 : o.RCV_GREY_QTY,
                            RCV_FAB_QTY: o.RCV_FAB_QTY == null ? 0 : o.RCV_FAB_QTY,
                            RTN_FAB_QTY: o.RTN_FAB_QTY == null ? 0 : o.RTN_FAB_QTY,
                            LOSS_QTY: o.LOSS_QTY == null ? 0 : o.LOSS_QTY,
                            PLOSS_QTY: o.PLOSS_QTY == null ? 0 : o.PLOSS_QTY,
                            QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID,
                            DF_PROC_TYPE_LST: o.DF_PROC_TYPE_LST,
                            NXT_DF_PROC_TYPE_LST: o.NXT_DF_PROC_TYPE_LST == null ? 3 : o.NXT_DF_PROC_TYPE_LST,

                            RTN_ROLL_LST: o.RTN_ROLL_LST == null ? "" : config.xmlStringShortNoTag(o.RTN_ROLL_LST.map(function (o) {
                                return {
                                    DF_SC_PT_RTN_ROLL_ID: o.DF_SC_PT_RTN_ROLL_ID,
                                    DF_SC_PT_RCV_D_ID: o.DF_SC_PT_RCV_D_ID,
                                    KNT_FAB_ROLL_ID: o.KNT_FAB_ROLL_ID,
                                    RCV_ROLL_WT: o.RCV_ROLL_WT,
                                    WT_MOU_ID: (o.WT_MOU_ID || 3)
                                }
                            })),
                            NOTE: o.NOTE,

                        }
                    }));
                }
                var id = vm.form.DF_SC_PT_RCV_H_ID

                return DyeingDataService.saveDataByUrl(data, '/ScPtReceive/Update').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            //config.appToastMsg("MULTI-001 Yarn receive has been updated successfully");
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pDF_SC_PT_RCV_H_ID: res.data.OPDF_SC_PT_RCV_H_ID }, { reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDF_SC_PT_RCV_H_ID) > 0) {
                                vm.form.DF_SC_PT_RCV_H_ID = res.data.OPDF_SC_PT_RCV_H_ID;
                                $state.go($state.current, { pDF_SC_PT_RCV_H_ID: res.data.OPDF_SC_PT_RCV_H_ID }, { reload: true });
                            }
                        }

                    }
                });
            }
        };

    }


})();


