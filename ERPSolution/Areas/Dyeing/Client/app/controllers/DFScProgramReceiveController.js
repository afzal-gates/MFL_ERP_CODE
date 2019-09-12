(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DFScProgramReceiveController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', 'Dialog', DFScProgramReceiveController]);
    function DFScProgramReceiveController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.DF_SCO_CHL_RCV_H_ID ? formData : { IS_TRANSFER: 'N', IS_AOP: 'N', DF_SCO_CHL_RCV_H_ID: 0, RCV_DT: vm.today, CREATED_BY: cur_user_id };

        if (!vm.form.RCV_DT)
            vm.form.RCV_DT = vm.today;

        vm.form.SCM_STORE_ID = formData.SCM_STORE_ID;

        if ($stateParams.pIS_AOP == 'Y') {
            vm.form.IS_FINALIZED = 'Y';
            vm.form.IS_AOP = 'Y';
        }

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetStoreList(), ReceiveItemList(), getDfProcessTypeData(), GetSupplierList(), GetProcessList(), getUserData(), GetCompanyList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }



        function getDfProcessTypeData() {
            return vm.processTypeList = {
                optionLabel: "-- Select Type --",
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
                        DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/SelectByPartyID?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || "0") + '&pIS_AOP=' + (vm.form.IS_AOP || 'N')).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                click: function (x) {
                    alert("");
                }
            })
        }


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
            height: '290px',
            columns: [

                { field: "DF_SC_PT_ISS_H_ID", hidden: true },
                { field: "CHALAN_NO", title: "Challan No", type: "string", width: "10%" },
                { field: "CHALAN_DT", title: "Challan Date", type: "date", template: "#= kendo.toString(kendo.parseDate(CHALAN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                { field: "GATE_PASS_NO", title: "Gate Pass No", type: "string", width: "10%" },
            ]
        };


        function onChange(arg) {

            var grid = $("#scPreTreatmentGrid").data("kendoGrid");
            var row = grid.select();
            var item = grid.dataItem(row);
            console.log(item);
            DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/DfScProgramChallanDtlByID?pCHALAN_NO=' + (item.CHALAN_NO || "")).then(function (res) {
                console.log(res[0]);
                var list = res.map(function (o) {
                    return {
                        BUYER_NAME_EN: o.BUYER_NAME_EN,
                        COLOR_NAME_EN: o.COLOR_NAME_EN,
                        DF_PROC_TYPE_LST: o.DF_PROC_TYPE_LST ? o.DF_PROC_TYPE_LST.split(',') : [],
                        DF_PROC_TYPE_NAME: o.DF_PROC_TYPE_NAME,
                        DF_SC_BT_CHL_ISS_D_ID: o.DF_SC_BT_CHL_ISS_D_ID,
                        DF_SC_BT_CHL_ISS_H_ID: o.DF_SC_BT_CHL_ISS_H_ID,
                        DF_SC_PRG_ISS_BT_D1_ID: o.DF_SC_PRG_ISS_BT_D1_ID,
                        DF_SC_PRG_ISS_H_ID: o.DF_SC_PRG_ISS_H_ID,
                        DYE_BATCH_NO: o.DYE_BATCH_NO,
                        DYE_LOT_NO: o.DYE_LOT_NO,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                        FAB_ITEM_DESC: o.FAB_ITEM_DESC,
                        FAB_TYPE_NAME: o.FAB_TYPE_NAME,
                        FIB_COMP_NAME: o.FIB_COMP_NAME,
                        FIN_DIA: o.FIN_DIA,
                        FIN_GSM: o.FIN_GSM,
                        ISS_QTY: o.ISS_QTY,
                        ISS_ROLL_QTY: o.ISS_ROLL_QTY,
                        IS_SELECT: o.IS_SELECT,
                        IS_TRANSFR: o.IS_TRANSFR,
                        KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID,
                        LK_DIA_TYPE_NAME: o.LK_DIA_TYPE_NAME,
                        ORDER_NO: o.ORDER_NO,
                        PRE_RCV_QTY: o.PRE_RCV_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID,
                        RQD_GSM: o.RQD_GSM,
                        STYLE_NO: o.STYLE_NO,
                        SUP_TRD_NAME_EN: o.SUP_TRD_NAME_EN,
                        processTypeList: o.processTypeList,
                    }
                });
                vm.scProgramList = list;
            });

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

        vm.checkRcvQty = function (item) {

            if ((parseFloat(item.ISS_QTY) + parseFloat(item.PRE_RCV_QTY)) < parseFloat(item.RCV_GREY_FAB_QTY))
                item.RCV_GREY_FAB_QTY = '';

            if (item.RCV_FIN_FAB_QTY > item.RCV_GREY_FAB_QTY) {
                item.RCV_FIN_FAB_QTY = '';
            }
            else {
                item.LOSS_QTY = item.RCV_GREY_FAB_QTY - item.RCV_FIN_FAB_QTY;
                //item.LOSS_QTY_P = (item.LOSS_QTY/item.ISS_QTY)* 100;
            }
        }


        vm.getScProgramByChallanNo = function () {
            DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/DfScProgramChallanDtlByID?pCHALAN_NO=' + (vm.form.CHALLAN_NO || "")).then(function (res) {
                vm.scProgramList = res;
            });
        }

        vm.searchScProgramInfo = function (e) {
            if (vm.form.SCM_SUPPLIER_ID > 0) {
                return vm.challanList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/SelectByPartyID?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || "0") + '&pIS_AOP=' + (vm.form.IS_AOP || 'N')).then(function (res) {
                                e.success(res);

                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                });
            }
        }

        function GetProcessList() {
            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/GetDFProcessType').then(function (res) {
                vm.processList = res;
            });
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
        }

        vm.removeItemData = function (data) {

            if (!data.DYE_STR_REQ_D_ID || data.DYE_STR_REQ_D_ID <= 0) {
                vm.ReceiveItemList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.ReceiveItemList.remove(data);
                 });

        }


        vm.removeRecoed = function (data) {

            Dialog.confirm('Removing "' + data.DYE_BATCH_NO + '.', 'Attention', ['Yes', 'No']).then(function () {

                var index = vm.scProgramList.indexOf(data);
                vm.scProgramList.splice(index, 1);
            });
        }


        vm.resetItemData = function () {
            vm.formItem = { QTY_MOU_ID: '3', CENTRAL_STK: 0, SUB_STK: 0 };
        };

        vm.reset = function () {
            $state.go('YarnReceive', { pDYE_DC_ISS_H_ID: 0 });
        };


        //  DropdownList

        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 16;
            var PARENT_ID = 0;

            if (vm.form.RF_ACTN_STATUS_ID > 0)
                PARENT_ID = vm.form.RF_ACTN_STATUS_ID;

            var sList = null;
            DyeingDataService.getDataByFullUrl('/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id).then(function (res) {
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

        function GetStoreList() {

            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            //'/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5&pSC_USER_ID=' + cur_user_id
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                var list = _.filter(res, function (x) { return (x.SCM_STORE_ID == 10 || x.SCM_STORE_ID == 11) })
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

        function ReceiveItemList() {
            vm.ReceiveItemList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/GetScProgramReceiveDtlInfo?pDF_SCO_CHL_RCV_H_ID=' + (formData.DF_SCO_CHL_RCV_H_ID || "0") + '&pIS_TRANSFER=' + (formData.IS_TRANSFER || 'N')).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                aggregate: [
                    { field: "ISS_QTY", aggregate: "sum" },
                    { field: "ISS_ROLL_QTY", aggregate: "sum" },
                    { field: "RCV_ROLL_QTY", aggregate: "sum" },
                    { field: "RCV_GREY_FAB_QTY", aggregate: "sum" },
                    { field: "RCV_FIN_FAB_QTY", aggregate: "sum" },
                    { field: "RTN_GREY_FAB_QTY", aggregate: "sum" },
                    { field: "LOSS_QTY", aggregate: "sum" },
                ],
                schema: {
                    model: {
                        id: "DF_SC_BT_CHL_ISS_D_ID",
                        fields: {
                            DYE_BATCH_NO: { type: "string" },
                            BUYER_NAME_EN: { type: "string" },
                            STYLE_NO: { type: "string" },
                            MC_ORDER_NO_LST: { type: "string" },
                            COLOR_NAME_EN: { type: "string" },
                            FAB_ITEM_DESC: { type: "string" },
                            ISS_QTY: { type: "number" },
                            ISS_ROLL_QTY: { type: "number" },
                            RCV_ROLL_QTY: { type: "number" },
                            RCV_GREY_FAB_QTY: { type: "number" },
                            RCV_FIN_FAB_QTY: { type: "number" },
                            RTN_GREY_FAB_QTY: { type: "number" },
                            LOSS_QTY: { type: "number" },
                        }

                    }
                },
            });
        };


        vm.gridOptionsItem = {
            pageable: false,
            filterable: false,
            //height: '300px',
            scrollable: false,
            columns: [
                { field: "DF_SCO_CHL_RCV_D_ID", hidden: true },
                { field: "DF_SCO_CHL_RCV_H_ID", hidden: true },
                { field: "DF_SC_PT_ISS_H_ID", hidden: true },
                { field: "KNT_STYL_FAB_ITEM_H_ID", hidden: true },
                { field: "DF_SC_BT_CHL_ISS_D_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "IS_STORED", hidden: true },
                { field: "DF_PROC_TYPE_LST", hidden: true },
                { field: "NXT_DF_PROC_TYPE_LST", hidden: true },
                { field: "NOTE", hidden: true },

                { field: "DYE_BATCH_NO", title: "Batch #", type: "string", width: "10%" },
                { field: "BUYER_NAME_EN", title: "Buyer Name", type: "string", width: "10%" },
                { field: "STYLE_NO", title: "Style", type: "string", width: "10%" },
                { field: "ORDER_NO", title: "Order", type: "string", width: "10%" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "5%" },
                { field: "FAB_ITEM_DESC", title: "Fabric Details", type: "string", width: "25%" },
                //{ field: "FIB_COMP_NAME", title: "Fiber Comp", type: "string", width: "20%" },
                //{ field: "FIN_DIA", title: "Fin Dia", type: "string", width: "20%" },
                //{ field: "LK_DIA_TYPE_NAME", title: "Dia Type", type: "string", width: "20%" },

                //RQD_GSM,

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
                    field: "RCV_GREY_FAB_QTY", title: "Rcv Gray Qty", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },
                {
                    field: "RCV_FIN_FAB_QTY", title: "Rcv Fin Qty", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },
                {
                    field: "RTN_GREY_FAB_QTY", title: "Rtn Qty", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },
                //{
                //    title: "Rtn Roll",
                //    template: '<a title="Open" ng-disabled="!dataItem.RTN_FAB_QTY>0" ng-click="vm.openRollModal(dataItem)" class="btn btn-xs green"><i class="fa fa-recycle"> Roll</i></a>',
                //    width: "5%"
                //},
                {
                    field: "LOSS_QTY", title: "Loss Qty", type: "decimal", width: "5%",
                    aggregates: ["sum"], footerTemplate: "#=sum#",
                },

                //{ field: "NOTE", title: "Note", type: "string", width: "5%" },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-remove"></i></a> <a  title="Edit" ng-click="vm.editItemData(dataItem,0)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "5%"
                }
            ],
            //editable: true
        };


        vm.addToGrid = function () {
            //if (fnValidateSub() == true) {
            console.log("D");
            for (var i = 0; i < vm.scProgramList.length; i++) {

                var idx = vm.ReceiveItemList.data().length + 1;

                console.log(i);

                var obj = vm.scProgramList[i];
                if (obj.IS_SELECT == true) {
                    console.log(obj);
                    if (vm.form.IS_TRANSFER != 'R') {
                        obj['DF_PROC_TYPE_LST'] = obj.DF_PROC_TYPE_LST ? obj.DF_PROC_TYPE_LST.join(',') : '0';

                        obj['NXT_DF_PROC_TYPE_LST'] = obj.NXT_DF_PROC_TYPE_LST ? obj.NXT_DF_PROC_TYPE_LST.join(',') : '0';
                    }
                    else {
                        obj['DF_PROC_TYPE_LST'] = '';
                        obj['NXT_DF_PROC_TYPE_LST'] = '';
                    }

                    var lst = _.filter(vm.ReceiveItemList.data(), function (x) { return x.KNT_STYL_FAB_ITEM_H_ID == obj.KNT_STYL_FAB_ITEM_H_ID && x.DF_SCO_CHL_RCV_D_ID == obj.DF_SCO_CHL_RCV_D_ID })
                    if (lst.length == 0) {
                        vm.ReceiveItemList.insert(idx, obj);
                    }
                    else {
                        config.appToastMsg("MULTI-005 Duplicate Records!");
                        return;
                    }
                }
            }
            //}

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


        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6009';
            vm.form.DF_SC_BT_CHL_ISS_H_ID = dataItem.DF_SC_BT_CHL_ISS_H_ID;
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


        vm.submitAll = function (data) {

            if (fnValidate() == true) {

                var dataOri = angular.copy(data);

                var _revdate = $filter('date')(dataOri.RCV_DT, 'yyyy-MM-ddTHH:mm:ss');
                dataOri["RCV_DT"] = _revdate;
                //dataOri["IS_FINALIZED"] = IsFinal;

                var _clnate = $filter('date')(dataOri.CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                dataOri["CHALAN_DT"] = _clnate;

                if (dataOri.IS_TRANSFER == 'R') {

                    dataOri["XML_RTN_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                        return {
                            DF_SCO_CHL_RTN_D_ID: o.DF_SCO_CHL_RTN_D_ID == null ? 0 : o.DF_SCO_CHL_RTN_D_ID,
                            DF_SCO_CHL_RCV_H_ID: o.DF_SCO_CHL_RCV_H_ID == null ? 0 : o.DF_SCO_CHL_RCV_H_ID,
                            KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_H_ID,
                            DF_SC_BT_CHL_ISS_D_ID: o.DF_SC_BT_CHL_ISS_D_ID == 0 ? null : o.DF_SC_BT_CHL_ISS_D_ID,
                            RTN_GREY_FAB_QTY: o.RTN_GREY_FAB_QTY == null ? 0 : o.RTN_GREY_FAB_QTY,
                            RTN_ROLL_QTY: o.RTN_ROLL_QTY == null ? 0 : o.RTN_ROLL_QTY,
                            RTN_FAB_QTY: o.RTN_FAB_QTY == null ? 0 : o.RTN_FAB_QTY,
                            QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID

                        }
                    }));
                }
                else {

                    dataOri["XML_RCV_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                        return {
                            DF_SCO_CHL_RCV_D_ID: o.DF_SCO_CHL_RCV_D_ID == null ? 0 : o.DF_SCO_CHL_RCV_D_ID,
                            DF_SCO_CHL_RCV_H_ID: o.DF_SCO_CHL_RCV_H_ID == null ? 0 : o.DF_SCO_CHL_RCV_H_ID,
                            DF_SC_PRG_ISS_H_ID: o.DF_SC_PRG_ISS_H_ID == null ? 0 : o.DF_SC_PRG_ISS_H_ID,
                            DF_SC_BT_CHL_ISS_D_ID: o.DF_SC_BT_CHL_ISS_D_ID == null ? 0 : o.DF_SC_BT_CHL_ISS_D_ID,
                            DF_SC_PRG_ISS_BT_D1_ID: o.DF_SC_PRG_ISS_BT_D1_ID,
                            DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                            KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID == null ? 0 : o.KNT_STYL_FAB_ITEM_H_ID,
                            RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                            RTN_GREY_FAB_QTY: o.RTN_GREY_FAB_QTY == null ? 0 : o.RTN_GREY_FAB_QTY,
                            RCV_GREY_FAB_QTY: o.RCV_GREY_FAB_QTY == null ? 0 : o.RCV_GREY_FAB_QTY,
                            RCV_FIN_FAB_QTY: o.RCV_FIN_FAB_QTY == null ? 0 : o.RCV_FIN_FAB_QTY,
                            LOSS_QTY: o.LOSS_QTY == null ? 0 : o.LOSS_QTY,
                            DF_PROC_TYPE_LST: o.DF_PROC_TYPE_LST,
                            NXT_DF_PROC_TYPE_LST: o.NXT_DF_PROC_TYPE_LST == null ? 3 : o.NXT_DF_PROC_TYPE_LST,
                            QTY_MOU_ID: 3,
                            NOTE: "",
                            IS_STORED: "Y"
                        }
                    }));

                }
                
                dataOri["IS_FINALIZED"] = "N";

                var id = vm.form.DF_SCO_CHL_RCV_H_ID

                return DyeingDataService.saveDataByUrl(dataOri, '/DfScProgram/Receive').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if ($stateParams.pIS_AOP == 'Y') {
                            $state.go("AopBatch", { pDYE_BT_CARD_H_ID: 0 }, { inherit: false, reload: true });
                            return;
                        }

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pDF_SCO_CHL_RCV_H_ID: res.data.OPDF_SCO_CHL_RCV_H_ID }, { inherit: false, reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDF_SCO_CHL_RCV_H_ID) > 0) {
                                vm.form.DF_SCO_CHL_RCV_H_ID = res.data.OPDF_SCO_CHL_RCV_H_ID;
                                $state.go($state.current, { pDF_SCO_CHL_RCV_H_ID: res.data.OPDF_SCO_CHL_RCV_H_ID }, { inherit: false, reload: true });
                            }
                        }

                    }
                });
            }
        };


        vm.completeAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);
                console.log(data);

                var _clndate = $filter('date')(data.CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["CHALAN_DT"] = _clndate;

                var _proDate = $filter('date')(data.RCV_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["RCV_DT"] = _proDate;


                if (data.IS_TRANSFER == 'R') {

                    data["XML_RTN_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                        return {
                            DF_SCO_CHL_RTN_D_ID: o.DF_SCO_CHL_RTN_D_ID == null ? 0 : o.DF_SCO_CHL_RTN_D_ID,
                            DF_SCO_CHL_RCV_H_ID: o.DF_SCO_CHL_RCV_H_ID == null ? 0 : o.DF_SCO_CHL_RCV_H_ID,
                            KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_H_ID,
                            DF_SC_BT_CHL_ISS_D_ID: o.DF_SC_BT_CHL_ISS_D_ID == 0 ? null : o.DF_SC_BT_CHL_ISS_D_ID,
                            RTN_GREY_FAB_QTY: o.RTN_GREY_FAB_QTY == null ? 0 : o.RTN_GREY_FAB_QTY,
                            RTN_ROLL_QTY: o.RTN_ROLL_QTY == null ? 0 : o.RTN_ROLL_QTY,
                            RTN_FAB_QTY: o.RTN_FAB_QTY == null ? 0 : o.RTN_FAB_QTY,
                            QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID

                        }
                    }));
                }
                else {

                    data["XML_RCV_D"] = DyeingDataService.xmlStringShort(vm.ReceiveItemList.data().map(function (o) {
                        return {
                            DF_SCO_CHL_RCV_D_ID: o.DF_SCO_CHL_RCV_D_ID == null ? 0 : o.DF_SCO_CHL_RCV_D_ID,
                            DF_SCO_CHL_RCV_H_ID: o.DF_SCO_CHL_RCV_H_ID == null ? 0 : o.DF_SCO_CHL_RCV_H_ID,
                            DF_SC_PRG_ISS_H_ID: o.DF_SC_PRG_ISS_H_ID == null ? 0 : o.DF_SC_PRG_ISS_H_ID,
                            DF_SC_BT_CHL_ISS_D_ID: o.DF_SC_BT_CHL_ISS_D_ID == null ? 0 : o.DF_SC_BT_CHL_ISS_D_ID,
                            DF_SC_PRG_ISS_BT_D1_ID: o.DF_SC_PRG_ISS_BT_D1_ID,
                            DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                            KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID == null ? 0 : o.KNT_STYL_FAB_ITEM_H_ID,
                            RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                            RTN_GREY_FAB_QTY: o.RTN_GREY_FAB_QTY == null ? 0 : o.RTN_GREY_FAB_QTY,
                            RCV_GREY_FAB_QTY: o.RCV_GREY_FAB_QTY == null ? 0 : o.RCV_GREY_FAB_QTY,
                            RCV_FIN_FAB_QTY: o.RCV_FIN_FAB_QTY == null ? 0 : o.RCV_FIN_FAB_QTY,
                            LOSS_QTY: o.LOSS_QTY == null ? 0 : o.LOSS_QTY,
                            DF_PROC_TYPE_LST: o.DF_PROC_TYPE_LST,
                            NXT_DF_PROC_TYPE_LST: o.NXT_DF_PROC_TYPE_LST == null ? 3 : o.NXT_DF_PROC_TYPE_LST,
                            QTY_MOU_ID: 3,
                            NOTE: "",
                            IS_STORED: "Y"
                        }
                    }));

                }
                data["IS_FINALIZED"] = "Y";

                var id = vm.form.DF_SCO_CHL_RCV_H_ID

                return DyeingDataService.saveDataByUrl(data, '/DfScProgram/Receive').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        
                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pDF_SCO_CHL_RCV_H_ID: res.data.OPDF_SCO_CHL_RCV_H_ID }, { inherit: false, reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDF_SCO_CHL_RCV_H_ID) > 0) {
                                vm.form.DF_SCO_CHL_RCV_H_ID = res.data.OPDF_SCO_CHL_RCV_H_ID;
                                $state.go($state.current, { pDF_SCO_CHL_RCV_H_ID: res.data.OPDF_SCO_CHL_RCV_H_ID }, { inherit: false, reload: true });
                            }
                        }


                    }
                });
            }
        };
    }


})();

