
(function () {
    'use strict';

    angular.module('multitex.dyeing').directive('autoFocus', function ($timeout) {
        return {
            restrict: 'AC',
            link: function (_scope, _element) {
                $timeout(function () {
                    _element[0].focus();
                }, 0);
            }
        };
    });

    angular.module('multitex.dyeing').controller('DfOnlineQcController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'formData', 'cur_user_id', DfOnlineQcController]);
    function DfOnlineQcController($q, config, DyeingDataService, $stateParams, $state, $scope, formData, cur_user_id) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.form = formData.DF_PROC_QC_RPT_H_ID ? formData : { DF_PROC_QC_RPT_H_ID: 0, IS_SELF_QC: 'N', IS_QC_OK: 'N', QC_DT: vm.today, QC_BY: -1, SHFT_IN_CHRGE: -1, DYE_BATCH_NO: '' };

        vm.formItem = {}

        vm.dyeBatchLot = [];
        //vm.majorDfctList = [];

        vm.form['CREATED_BY'] = cur_user_id;
        vm.form['ACTION_DT'] = vm.today;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetDefectList(), getQcStatusList(), GetCompanyList(), GetDfReProcessTypeList(),
                //GetStatusList(),
                getQcReturnList(), getUserData(), getShiftData(), getQcUserData(), loadShift(), getData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getData() {
            DyeingDataService.getDataByFullUrl('/api/knit/KntScChlnRcv/GetQcStatusTypeList').then(function (res) {
                if (vm.form.IS_QC_OK == "Y") {

                    var list = _.filter(res, function (x) { return x.KNT_QC_STS_TYPE_ID == 1 })
                    vm.qcStatusList = list;
                    vm.form.KNT_QC_STS_TYPE_ID = list[0].KNT_QC_STS_TYPE_ID;
                }
                else {
                    var list = _.filter(res, function (x) { return x.KNT_QC_STS_TYPE_ID == 3 || x.KNT_QC_STS_TYPE_ID == 6 })
                    vm.qcStatusList = list;
                    vm.form.KNT_QC_STS_TYPE_ID = list[0].KNT_QC_STS_TYPE_ID;
                }
            });
        }

        $scope.expandAll = function (expanded) {
            // $scope is required here, hence the injection above, even though we're using "controller as" syntax
            $scope.$broadcast('onExpandAll', {
                expanded: expanded
            });
        }


        if (formData.DYE_BATCH_NO) {
            vm.BatchFabricList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectOnlineQcDtlByID?pDF_PROC_QC_RPT_H_ID=' + (formData.DF_PROC_QC_RPT_H_ID || null)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
            //DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchIssueFabricByBatchNo?pDYE_BATCH_NO=' + (formData.DYE_BATCH_NO.trim() || "")).then(function (res) {
            //    vm.BatchFabricList = res;
            //});
        }

        var CurShiftData = {};

        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 35;
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

        function getShifData(res) {

            CurShiftData = _.find(res, function (o) {
                return o.IS_SCHE_ACTIVE == 'Y';
            });
            if (CurShiftData) {
                vm.form.HR_SCHEDULE_H_ID = CurShiftData.HR_SCHEDULE_H_ID;
                //if (pKNT_MACHINE_ID) {
                //    setOperatorData(pKNT_MACHINE_ID, CurShiftData.HR_SCHEDULE_H_ID);
                //}

            };
        };


        function loadShift() {
            DyeingDataService.getDataByFullUrl('/api/Knit/KnitPlan/getShiftDataByPlanId?pHR_SHIFT_PLAN_ID=2').then(function (res) {

                vm.shifDataDs = new kendo.data.DataSource({
                    data: res
                });
                getShifData(res);
            });
        }

        $scope.QC_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.QC_DT_LNopened = true;
        }


        function getQcUserData() {

            return vm.QcUserList = {
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



        vm.getMultipleDept = function (item, idx) {

            if (item.LAST_UPDATED_BY > 0) {
                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetRespDeptListByID?pMULTI_DEPT_LST=' + (item.MULTI_DEPT_LST || (item.RF_RESP_DEPT_ID_OLD || ''))).then(function (res) {
                    item["resDeptList"] = res;
                    if (res.length == 1) {
                        item['RF_RESP_DEPT_ID'] = res[0].RF_RESP_DEPT_ID;
                        console.log(item.RF_RESP_DEPT_ID);
                    }
                    else {
                        item['RF_RESP_DEPT_ID'] = 0;
                    }
                });
            }
            else {
                item.LAST_UPDATED_BY = 0;
                item["resDeptList"] = [];
            }

            var grpList = _.filter(vm.defectList, function (x) { return x.RESP_DEPT_NAME != "Afzal" });
            console.log(grpList);

            var row = parseInt(grpList.length);

            var list = _.filter(grpList[0], function (x) { return x.LAST_UPDATED_BY > 0 });
            for (var i = 1; i < row; i++) {
                var itemData = _.filter(grpList[i], function (x) { return x.LAST_UPDATED_BY > 0 });
                for (var j = 0; j < itemData.length; j++)
                    list.push(itemData[j]);
            }

            //var list = _.filter(vm.defectList, function (x) { return x.LAST_UPDATED_BY > 0 });
            console.log(list);
            vm.majorDfctList = list;
            if (list.length == 1)
                vm.form.MAJ_DFCT_TYPE_ID = list[0].RF_FB_DFCT_TYPE_ID;
            else
                vm.form.MAJ_DFCT_TYPE_ID = 0;
        }

        function getUserData() {

            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectDfQcUser?pLK_DF_EMP_TYPE_ID=' + (vm.form.LK_DF_EMP_TYPE_ID || 721)).then(function (res) {
                console.log(res);
                var lst = [{
                    EMP_FULL_NAME_EN: '-- Select In-Charge --',
                    HR_EMPLOYEE_ID: -1
                }].concat(res);
                vm.userList = lst;
            });

            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectDfQcUser?pLK_DF_EMP_TYPE_ID=' + (vm.form.LK_DF_EMP_TYPE_ID || 720)).then(function (res) {
                console.log(res);
                var lst = [{
                    EMP_FULL_NAME_EN: '-- Select Inspector --',
                    HR_EMPLOYEE_ID: -1
                }].concat(res);
                vm.userInspList = lst;
            });
        }

        function getShiftData() {

            DyeingDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
                var lst = [{
                    SCHEDULE_NAME_EN: '-- Select Shift --',
                    HR_SCHEDULE_H_ID: -1
                }].concat(res);
                vm.shiftList = lst;
            });
        }


        function GetDefectList() {
            //DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=709&pOption=3002').then(function (res) {
            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectOnlineQcDectListByID?pDF_PROC_QC_RPT_H_ID=' + (formData.DF_PROC_QC_RPT_H_ID || null) + '&pLK_FAB_INSP_TYPE_ID=709').then(function (res) {

                var list = _.groupBy(res, function (x) { return x.RESP_DEPT_NAME; });
                console.log(list);
                vm.defectList = list;

                //vm.defectList = res;
                if (vm.form.DF_PROC_QC_RPT_H_ID)
                    vm.majorDfctList = res;
            });
        };

        vm.loadReProcessType = function () {

            vm.dfReprocessTypeList.read();
        }

        function GetDfReProcessTypeList() {
            return vm.dfReprocessTypeList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchReprocessTypeList').then(function (res) {
                            var list = _.filter(res, function (x) { return x.LK_RP_RTN_TYPE_ID == vm.form.LK_RP_RTN_TYPE_ID && x.DYE_RE_PROC_TYPE_ID > 2 });
                            console.log(list);
                            e.success(list);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getQcStatusList() {

            DyeingDataService.getDataByFullUrl('/api/knit/KntScChlnRcv/GetQcStatusTypeList').then(function (res) {
                var list = _.filter(res, function (x) { return x.KNT_QC_STS_TYPE_ID == 3 || x.KNT_QC_STS_TYPE_ID == 6 })
                vm.qcStatusList = list;
                vm.form.KNT_QC_STS_TYPE_ID = list[0].KNT_QC_STS_TYPE_ID;
            });

        };

        vm.changeQC = function (id) {

            DyeingDataService.getDataByFullUrl('/api/knit/KntScChlnRcv/GetQcStatusTypeList').then(function (res) {
                if (id == "Y") {

                    var list = _.filter(res, function (x) { return x.KNT_QC_STS_TYPE_ID == 1 })
                    vm.qcStatusList = list;
                    vm.form.KNT_QC_STS_TYPE_ID = list[0].KNT_QC_STS_TYPE_ID;
                }
                else {
                    var list = _.filter(res, function (x) { return x.KNT_QC_STS_TYPE_ID == 3 || x.KNT_QC_STS_TYPE_ID == 6 })
                    vm.qcStatusList = list;
                    vm.form.KNT_QC_STS_TYPE_ID = list[0].KNT_QC_STS_TYPE_ID;
                }
            });
        }


        function getQcReturnList() {

            return vm.qcReturnList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(139).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LOOKUP_DATA_ID != 682 });
                                e.success(list);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };



        vm.searchBatchInfo = function (pPageNo) {

            if (vm.form.DYE_BATCH_NO && vm.form.DYE_BATCH_NO.length > 0) {
                vm.showSplash = true;

                DyeingDataService.getDataByUrl('/DFProduction/GetBatchForDFQC?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {
                    console.log(res[0]);
                    vm.form.BUYER_NAME_EN = res[0].BUYER_NAME_EN;
                    vm.form.STYLE_NO = res[0].STYLE_NO;
                    vm.form.ORDER_NO = res[0].ORDER_NO;
                    vm.form.COLOR_NAME_EN = res[0].COLOR_NAME_EN;
                    vm.form.COLOR_GRP_NAME_EN = res[0].COLOR_GRP_NAME_EN;
                    vm.form.BT_STS_TYP_NAME = res[0].BT_STS_TYP_NAME;
                    vm.form.LK_CHQ_RL_STS_ID = res[0].LK_CHQ_RL_STS_ID;
                    vm.form.LAST_DF_PROC_TYPE_ID = res[0].LAST_DF_PROC_TYPE_ID;
                    vm.form.DYE_BATCH_NO_LST = res[0].DYE_BATCH_NO;
                    vm.form.DYE_LOT_NO = res[0].DYE_LOT_NO;
                    vm.form.DYE_BT_CARD_H_ID = res[0].DYE_BT_CARD_H_ID;
                    vm.form.HR_COMPANY_ID = res[0].HR_COMPANY_ID;
                    vm.form.ACT_BATCH_QTY_LST = res[0].ACT_BATCH_QTY + " Kg";

                    vm.form['BATCH_RP_QTY'] = 0;
                    vm.form.DIA_TYPE_NAME = res[0].DIA_TYPE_NAME;

                    vm.BatchFabricList = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchIssueFabricByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {

                                    vm.form.BATCH_RP_QTY = 0;
                                    for (var i = 0; i < res.length; i++)
                                        vm.form.BATCH_RP_QTY = parseInt(vm.form.BATCH_RP_QTY) + parseInt(res[i].RQD_QTY);
                                    e.success(res);

                                });
                            }
                        }
                    });

                    //DyeingDataService.getDataByFullUrl('/api/Dye/DCBatchProgramRequisition/GetBatchIssueFabricByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {
                    //    vm.BatchFabricList = res;
                    //});

                    //DyeingDataService.getDataByUrl('/GreyFabReq/getFinishFabricInsRollByBatchID?pDYE_BT_CARD_H_ID=' + res[0].DYE_BT_CARD_H_ID).then(function (res) {
                    //    vm.RollProductionList = res;
                    //});
                    vm.showSplash = false;

                });

            }
        }


        vm.inputIntoQcList = function (dataItem) {

            if (dataItem.IS_SELECT) {
                console.log(dataItem);
                vm.form.BATCH_RP_QTY = parseInt(vm.form.BATCH_RP_QTY) + parseInt(dataItem.RQD_QTY);
            }
            else {
                vm.form.BATCH_RP_QTY = parseInt(vm.form.BATCH_RP_QTY) - parseInt(dataItem.RQD_QTY);
                console.log("A");
            }
        }

        var record = 0;

        vm.gridOptionsFabric = {
            pageable: false,
            scrollable: true,
            columns: [
                { field: "LK_DIA_TYPE_ID", hidden: true },
                { field: "LK_FBR_GRP_ID", hidden: true },
                { field: "RF_FAB_TYPE_ID", hidden: true },
                { field: "RF_FIB_COMP_ID", hidden: true },
                { field: "KNT_STYL_FAB_ITEM_H_ID", hidden: true },
                { field: "DF_PROC_QC_RPT_D_ID", hidden: true },
                { field: "IS_SELECT", title: "+/-", type: "string", width: "4%", template: "<input type='checkbox' ng-click='vm.inputIntoQcList(dataItem);' ng-model='dataItem.IS_SELECT' ng-init='dataItem.IS_SELECT=true' />" },
                { field: "FAB_ITEM_DESC", title: "Fabric Details", type: "string", width: "50%", },
                //{ field: "FAB_TYPE_SNAME", title: "Fabric Type", type: "string", width: "10%" },
                //{ field: "FIB_COMP_NAME", title: "Fiber Comp.", type: "string", width: "15%" },
                //{ field: "RQD_GSM", title: "Req. GSM", type: "string", width: "8%" },
                //{ field: "FIN_DIA", title: "Fin. DIA", type: "string", width: "5%" },
                //{ field: "LK_DIA_TYPE_NAME", title: "Dia Type", type: "string", width: "8%" },
                { field: "RQD_QTY", title: "Lot Qty.", type: "decimal", width: "10%" },

            ],
            dataBound: function () {
                var rows = this.items();
                $(rows).each(function () {
                    var index = $(this).index() + 1;
                    var rowLabel = $(this).find(".row-number");
                    $(rowLabel).html(index);

                    var rowLabel = $(this).find(".row-unit");
                    $(rowLabel).html("Kg");
                });
            }
        };

        vm.gridOptionsFinalFabric = {

            dataSource: {
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/Dye/DfFabInsp/GetDfQCRptByBatchNo?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {
                            e.success(res);
                        })
                    }
                },
                group: [
                    { field: "LK_DIA_TYPE_NAME", dir: "asc" },
                ]
            },
            pageable: false,
            //height: '400px',
            scrollable: true,
            groupable: true,
            columns: [
                { field: "LK_DIA_TYPE_ID", hidden: true },
                { field: "LK_FBR_GRP_ID", hidden: true },
                { field: "RF_FAB_TYPE_ID", hidden: true },
                { field: "RF_FIB_COMP_ID", hidden: true },
                { field: "FABRIC_GROUP_NAME", title: "Fab Grp", type: "string", width: "5%", },
                { field: "FAB_TYPE_SNAME", title: "Fabric Type", type: "string", width: "5%" },
                { field: "FIB_COMP_NAME", title: "Fiber Comp.", type: "string", width: "15%" },
                { field: "RQD_GSM", title: "Req. GSM", type: "string", width: "7%" },
                { field: "FIN_DIA", title: "Fin. DIA", type: "string", width: "5%" },
                { field: "LK_DIA_TYPE_NAME", title: "Dia Type", type: "string", width: "5%" },
                { field: "RQD_QTY", title: "Grey Qty.", type: "decimal", width: "7%" },
                { field: "SUB_LOT_NO", title: "Sub-Lot", type: "string", width: "7%" },
                { field: "LOT_QTY", title: "Lot Qty.", type: "decimal", width: "7%" },
                { field: "FINIS_QTY", title: "Finish Qty", type: "decimal", width: "7%" },
                { field: "FAB_STR_POS", title: "Fabric Positionk", type: "string", width: "10%" },
                { field: "QC_STS_TYP_NAME", title: "QC Status", type: "string", width: "10%" },
                { field: "SUB_LOT_GRP_NAME", title: "Sub Lot Group", type: "string", width: "10%" },

            ]
        };


        vm.printFabricInspection = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6006';

            vm.form.DF_FAB_QC_RPT_ID = dataItem.DF_FAB_QC_RPT_ID;
            vm.form.SUB_LOT_NO = dataItem.SUB_LOT_NO;

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
        }


        vm.printSubBatchCard = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-4041';

            vm.form.DF_FAB_QC_RPT_ID = dataItem.DF_FAB_QC_RPT_ID;
            vm.form.DF_BT_SUB_LOT_ID = dataItem.DF_BT_SUB_LOT_ID;
            vm.form.SUB_BATCH_NO = dataItem.SUB_BATCH_NO;

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
        }

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(vm.BatchFabricList.data());

                var qcDtlList = _.filter(vm.BatchFabricList.data(), function (x) { return x.IS_SELECT === true; });
                console.log(qcDtlList);

                data["XML_QC_D"] = DyeingDataService.xmlStringShort(qcDtlList.map(function (o) {
                    return {
                        DF_PROC_QC_RPT_D_ID: o.DF_PROC_QC_RPT_D_ID == null ? 0 : o.DF_PROC_QC_RPT_D_ID,
                        DF_PROC_QC_RPT_H_ID: o.DF_PROC_QC_RPT_H_ID == null ? 0 : o.DF_PROC_QC_RPT_H_ID,
                        LK_FBR_GRP_ID: o.LK_FBR_GRP_ID == 0 ? null : o.LK_FBR_GRP_ID,
                        LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID == 0 ? null : o.LK_DIA_TYPE_ID,
                        RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID == 0 ? null : o.RF_FAB_TYPE_ID,
                        RF_FIB_COMP_ID: o.RF_FIB_COMP_ID == 0 ? null : o.RF_FIB_COMP_ID,
                        KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_H_ID,
                        BATCH_QTY: o.RQD_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == 0 ? null : o.QTY_MOU_ID
                    }
                }));


                var grpList = _.filter(vm.defectList, function (x) { return x.RESP_DEPT_NAME != "Afzal" });
                var row = parseInt(grpList.length);

                var dfctList = _.filter(grpList[0], function (x) { return x.LAST_UPDATED_BY > 0 });
                for (var i = 1; i < row; i++) {
                    var itemData = _.filter(grpList[i], function (x) { return x.LAST_UPDATED_BY > 0 });
                    for (var j = 0; j < itemData.length; j++)
                        dfctList.push(itemData[j]);
                }

                //var dfctList = _.filter(vm.defectList, function (x) { return parseInt(x.LAST_UPDATED_BY) > 0 });
                data["XML_QC_DFCT"] = DyeingDataService.xmlStringShort(dfctList.map(function (o) {
                    return {
                        DF_PROC_QC_RPT_DFCT_ID: o.DF_PROC_QC_RPT_DFCT_ID == null ? 0 : o.DF_PROC_QC_RPT_DFCT_ID,
                        DF_PROC_QC_RPT_H_ID: o.DF_PROC_QC_RPT_H_ID == null ? 0 : o.DF_PROC_QC_RPT_H_ID,
                        RF_FB_DFCT_TYPE_ID: o.RF_FB_DFCT_TYPE_ID == 0 ? null : o.RF_FB_DFCT_TYPE_ID,
                        RF_RESP_DEPT_ID: o.RF_RESP_DEPT_ID == 0 ? null : o.RF_RESP_DEPT_ID
                    }
                }));


                if (data.KNT_QC_STS_TYPE_ID == 3)
                    data['DF_BT_STS_TYPE_ID'] = 4;
                else
                    data['DF_BT_STS_TYPE_ID'] = 1;

                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DFProduction/OnlineQc').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        GetDefectList();
                        $state.go($state.current, { pDF_PROC_QC_RPT_H_ID: res.data.OPDF_PROC_QC_RPT_H_ID }, { reload: true });
                    }
                });
            }
        }
    }

})();