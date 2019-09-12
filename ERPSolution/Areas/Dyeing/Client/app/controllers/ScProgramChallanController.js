
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('ScProgramChallanController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'formData', 'commonDataService', 'cur_user_id', 'Dialog', '$filter', ScProgramChallanController]);
    function ScProgramChallanController($q, config, DyeingDataService, $stateParams, $state, $scope, formData, commonDataService, cur_user_id, Dialog, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.DateTimeFormat = config.appDateTimeFormat;
        vm.TimeFormat = config.appTimeFormat;
        vm.ACTION_DIS = 0;
        vm.REPROCESS_FLAG = 0;
        vm.today = new Date();
        //vm.form = {};

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };

        vm.form = formData.DF_SC_BT_CHL_ISS_H_ID ? formData : { CHALAN_DT: vm.today, RF_ACTN_VIEW: 0 };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetReqSourceList(), GetCompanyList(), GetMOUList(), GetSupplierList(), loadOrderDetailData(), GetStatusList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.ScProgramList = [];

        vm.loadPendingProgram = function () {

            if (vm.form.IS_ALT_PARTY != 'Y')
                DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/DfScProgramBySupplierID?&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || 0)).then(function (res) {
                    vm.ScProgramList = res;
                });
        }


        function loadOrderDetailData() {
            return vm.ScItemListDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        DyeingDataService.getDataByUrl('/DfScProgram/DfScProgramChallanDtlByID?pDF_SC_BT_CHL_ISS_H_ID=' + (formData.DF_SC_BT_CHL_ISS_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    model: {
                        id: "DF_SC_PRG_ISS_BT_D1_ID",
                        fields: {
                            BYR_NAME_EN: { editable: false },
                            STYLE_NO: { editable: false },
                            ORDER_NO: { editable: false },
                            FAB_TYPE_SNAME: { editable: false },
                            FIB_COMP_NAME: { editable: false },
                            LK_DIA_TYPE_NAME: { editable: false },
                            FIN_DIA: { editable: false },
                            RQD_GSM: { editable: false },
                            //FAB_ROLL_LST: { editable: false },
                            RQD_QTY: { editable: false },
                            ISS_QTY: { editable: false },
                        }
                    }
                }

            })
        }


        vm.gridOptions = {
            pageable: false,
            filterable: false,
            height: '100%',
            scrollable: false,
            editable: true,
            columns: [
                { field: "DF_SC_PRG_CHL_ISS_D_ID", hidden: true },
                { field: "DF_SC_PRG_CHL_ISS_H_ID", hidden: true },
                { field: "KNT_STYL_FAB_ITEM_ID", hidden: true },
                { field: "DF_SC_PRG_ISS_BT_D1_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },

                { field: "BUYER_NAME_EN", title: "Buyer Name", width: "15%", editable: false },
                { field: "STYLE_NO", title: "Style No", width: "8%", editable: false },
                { field: "ORDER_NO", title: "Order No", width: "8%", editable: false },
                { field: "FAB_TYPE_SNAME", title: "Fabric Type", width: "5%", editable: false },
                { field: "FIB_COMP_NAME", title: "Fiber Composition", width: "5%", editable: false },
                { field: "LK_DIA_TYPE_NAME", title: "Dia Type", width: "5%", editable: false },
                { field: "FIN_DIA", title: "Fin. Dia", width: "5%", editable: false },
                { field: "RQD_GSM", title: "Req. GSM", width: "5%", editable: false },
                //{ field: "FAB_ROLL_LST", title: "Issued Roll No", width: "30%", editable: false },
                { field: "RQD_QTY", title: "Req. Qty", width: "5%", editable: true },
                //{ field: "ISS_ROLL_QTY", title: "Issue Roll Qty", width: "5%", editable: true },
                { field: "ISS_QTY", title: "Issue Qty", width: "5%", editable: true },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeRecipeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a> ',
                    width: "8%"
                }
            ],
        };

        vm.addToGrid = function (data) {
            if (fnValidateSub() == true) {
                var items = angular.copy(_.filter(data, function (o) { return o.IS_SELECT == true }));
                for (var i = 0; i < items.length; i++) {
                    var obj = items[i];
                    console.log(obj);
                    var idx = vm.ScItemListDS.indexOf(obj);;
                    if (idx < 0) {
                        obj["QTY_MOU_ID"] = 3;
                        var id = vm.ScItemListDS.data().length + 1;
                        vm.ScItemListDS.insert(id, obj);
                    }
                    else {

                        config.appToastMsg("MULTI-005 Duplicate item name exists!");
                    }

                }

            }
        }

        vm.removeRecipeItemData = function (obj) {
            //var idx = vm.ScItemListDS.indexOf(obj);
            vm.ScItemListDS.remove(obj);
        }


        function GetStatusList() {
            var RF_ACTN_TYPE_ID = 31;
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


        function GetReqSourceList() {
            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
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

        $scope.CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.CHALAN_DT_LNopened = true;
        }


        vm.submitAll = function (dataOri, mode, status) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(data);

                vm.showSplash = true;

                data["IS_UPDATE"] = mode;

                var _idate = $filter('date')(data.CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["CHALAN_DT"] = _idate;

                data["IS_FINALIZED"] = status;

                data["XML_ISS_D"] = DyeingDataService.xmlStringShort(vm.ScItemListDS.data().map(function (o) {
                    return {
                        DF_SC_PRG_ISS_BT_D1_ID: o.DF_SC_PRG_ISS_BT_D1_ID == 0 ? null : o.DF_SC_PRG_ISS_BT_D1_ID,
                        DF_SC_BT_CHL_ISS_D_ID: o.DF_SC_BT_CHL_ISS_D_ID == 0 ? null : o.DF_SC_BT_CHL_ISS_D_ID,
                        DF_SC_BT_CHL_ISS_H_ID: o.DF_SC_BT_CHL_ISS_H_ID == 0 ? null : o.DF_SC_BT_CHL_ISS_H_ID,
                        KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_ID,
                        ISS_ROLL_QTY: o.ISS_ROLL_QTY == null ? 0 : o.ISS_ROLL_QTY,
                        ISS_QTY: o.ISS_QTY == null ? 0 : o.ISS_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID
                    }
                }));


                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DfScProgram/SaveChallan').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        $state.go($state.current, { pDF_SC_BT_CHL_ISS_H_ID: res.data.OPDF_SC_BT_CHL_ISS_H_ID }, { reload: true });
                    }
                });
            }
        };


        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6009';
            vm.form.DF_SC_BT_CHL_ISS_H_ID = dataItem.DF_SC_BT_CHL_ISS_H_ID;

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



    }


})();

