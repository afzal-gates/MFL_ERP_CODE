
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('ScPreTreatmentController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'formData', 'commonDataService', 'cur_user_id', 'Dialog', '$filter', ScPreTreatmentController]);
    function ScPreTreatmentController($q, config, DyeingDataService, $stateParams, $state, $scope, formData, commonDataService, cur_user_id, Dialog, $filter) {

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

        //vm.myList = [];
        //getAllDfProcessType();

        vm.form = formData.DF_SC_PT_ISS_H_ID ? formData : { LK_FAB_PRG_TYPE: 732, IS_SC: 'Y', IS_BATCH: 'N', RF_FAB_PROD_CAT_LST: '', ISS_STORE_ID: '8', DF_SC_PT_RCV_H_ID: $stateParams.pDF_SC_PT_RCV_H_ID, RF_REQ_TYPE_ID: 32, PRG_ISS_BY: cur_user_id, PRG_ISS_DT: vm.today, CHALAN_DT: vm.today, RF_ACTN_VIEW: 0, ACTN_ROLE_FLAG: 'RI', DF_PROC_TYPE_LST: [] };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetProdCatList(), getSelectMonth(0, 0), getDfProcessTypeData(), getUserData(), GetReqSourceList(), GetReqTypeList(), GetCompanyList(),
                           GetDfParamList(), GetStatusList(), GetMOUList(), GetSupplierList(), GetSourceTypeList(), loadProcessDetailData(), loadOrderDetailData(),
                           GetScPartyList(), GetSupplier(), GetProgramTypeList()
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        if (formData) {
            vm.form["DF_PROC_TYPE_LST"] = formData.DF_PROC_TYPE_LST ? formData.DF_PROC_TYPE_LST.split(',') : [];
        }

        if ($stateParams.pDF_SC_PT_RCV_H_ID > 0) {
            vm.form.REVISION_NO = (vm.form.REVISION_NO || 0) + 1;
            vm.form.REVISION_DT = vm.today;

            DyeingDataService.getDataByFullUrl('/api/dye/ScPtReceive/GetScPtRcvDtlByID?pDF_SC_PT_RCV_H_ID=' + ($stateParams.pDF_SC_PT_RCV_H_ID || 0)).then(function (res) {
                console.log(res[0]);
                vm.OrderList = res.map(function (o) {
                    return {
                        DF_SC_PT_ISS_D1_ID: 0,
                        DF_SC_PT_ISS_H_ID: 0,
                        KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_ID,
                        RQD_QTY: o.RQD_QTY,
                        BUYER_NAME_EN: o.BUYER_NAME_EN,
                        BYR_ACC_GRP_NAME_EN: o.BYR_ACC_GRP_NAME_EN,
                        BYR_ACC_NAME_EN: o.BYR_ACC_NAME_EN,
                        COLOR_NAME_EN: o.COLOR_NAME_EN,
                        DIA_TYPE_NAME: o.DIA_TYPE_NAME,
                        FABRIC_DESC: o.FABRIC_DESC,
                        FAB_ITEM_DESC: o.FAB_ITEM_DESC,
                        FAB_PROD_CAT_NAME: o.FAB_PROD_CAT_NAME,
                        FAB_TYPE_NAME: o.FAB_TYPE_NAME,
                        FIB_COMP_NAME: o.FIB_COMP_NAME,
                        FIN_DIA: o.FIN_DIA,
                        FIN_GSM: o.FIN_GSM,
                        SC_PRG_NO: o.SC_PRG_NO,
                        KNT_YRN_LOT_LST: o.KNT_YRN_LOT_LST,
                        MC_ORDER_NO_LST: o.MC_ORDER_NO_LST,
                        RCV_FAB_QTY: o.RCV_FAB_QTY,
                        RCV_GREY_QTY: o.RCV_GREY_QTY,
                        RCV_ROLL_QTY: o.RCV_ROLL_QTY,
                        RTN_FAB_QTY: o.RTN_FAB_QTY,
                        STYLE_NO: o.STYLE_NO,
                        BAL_GFAB_QTY: o.RCV_FAB_QTY + o.RTN_FAB_QTY,

                        FAB_COLOR_ID: o.FAB_COLOR_ID,
                        LK_DYE_MTHD_ID: o.LK_DYE_MTHD_ID,
                        MC_BYR_ACC_GRP_ID: o.MC_BYR_ACC_GRP_ID,
                        MC_FAB_PROD_ORD_H_ID: o.MC_FAB_PROD_ORD_H_ID,
                        KNT_FAB_QTY: o.RCV_FAB_QTY + o.RTN_FAB_QTY,
                        ORDER_NO_LIST: o.MC_ORDER_NO_LST,
                        RF_FAB_PROD_CAT_ID: 2,
                        RF_FAB_TYPE_ID: 1,

                        DF_PROC_TYPE_LST: [],
                        ISS_ROLL_QTY: o.RCV_ROLL_QTY,
                        ISS_QTY: '',
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID
                    }
                });

            });

        }

        vm.removeParam = function (item, obj) {
            var idx = item.scParamList.indexOf(obj);
            if (item.scParamList.length > 0)
                item.scParamList.splice(idx, 1);
        }


        vm.addParam = function (item, obj) {
            console.log(item);
            var idx = obj + 1;
            var itm = {
                RF_DF_PARAM_TYPE_ID: '',
                PARAM_DESC: '',
                DF_SC_PT_ISS_D3_ID: '',
                DF_SC_PT_ISS_D2_ID: ''
            };

            item.scParamList.splice(idx, "0", angular.copy(itm));

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
                        DyeingDataService.getDataByUrl('/ScPreTreatment/GetScPTIssueD1ByID?pDF_SC_PT_ISS_H_ID=' + (formData.DF_SC_PT_ISS_H_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    model: {
                        id: "DF_SC_PT_ISS_D1_ID",
                        fields: {
                            BYR_ACC_NAME_EN: { editable: false },
                            STYLE_NO: { editable: false },
                            COLOR_NAME_EN: { editable: false },
                            FABRIC_DESC: { editable: false },
                            RQD_QTY: { editable: true },
                            ISS_ROLL_QTY: { editable: false },
                            ISS_QTY: { editable: false },
                        }
                    }
                }

            })
        }

        function loadProcessDetailData() {

            DyeingDataService.getDataByUrl('/ScPreTreatment/GetScPTIssueD2ByID?pDF_SC_PT_ISS_H_ID=' + (formData.DF_SC_PT_ISS_H_ID || 0)).then(function (res) {
                vm.PreProcessList = res;
            });

        }

        vm.gridOptions = {
            pageable: false,
            filterable: false,
            height: '100%',
            scrollable: false,
            editable: true,
            columns: [
                { field: "KNT_STYL_FAB_ITEM_ID", hidden: true },
                { field: "DF_SC_PT_ISS_D1_ID", hidden: true },
                { field: "QTY_MOU_ID", hidden: true },
                { field: "BYR_ACC_NAME_EN", title: "ByrAcc", width: "10%", editable: false, template: "<span style=\"display: block;\">#=BYR_ACC_NAME_EN #</span> <span style=\"display: block;font-style:italic;color:FUCHSIA;\"><small>(#:FAB_PROD_CAT_NAME#)</small><span>" },
                { field: "STYLE_NO", title: "Order/Style", width: "15%", editable: false, template: "<span style=\"display: block;\">#=ORDER_NO_LIST #</span> <span style=\"display: block;font-style:italic;\"><small>#:STYLE_NO#</small><span>" },

                //{ field: "COL_TYPE_NAME", title: "Type", width: "5%" },
                { field: "COLOR_NAME_EN", title: "Color Name", width: "10%", editable: false, template: "<span style=\"display: block;\">#=COLOR_NAME_EN #</span>" },

                { field: "FABRIC_DESC", title: "Fabric Description", width: "30%", editable: false },
                { field: "DF_PROC_TYPE_NAME_LST", title: "Pre-Process", width: "10%", editable: false },
                { field: "RQD_QTY", title: "Req. Qty", width: "10%", editable: true },
                { field: "ISS_ROLL_QTY", title: "Issue Roll Qty", width: "10%", editable: false },
                { field: "ISS_QTY", title: "Issue Qty", width: "10%", editable: false },

                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeRecipeItemData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>  <a  title="Edit" ng-click="vm.editRecipeItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
        };

        vm.addToGrid = function (data) {
            if (fnValidateSub() == true) {
                var items = angular.copy(_.filter(data, function (o) { return o.IS_SELECT == true }));

                DyeingDataService.getDataByUrl('/DFProcessType/SelectAll').then(function (res) {
                    var list = _.filter(res, function (x) { return x.LK_PROC_SUB_GRP_ID == 576 || x.LK_PROC_SUB_GRP_ID == 578 });

                    for (var i = 0; i < items.length; i++) {
                        var obj = items[i];

                        for (var j = 0; j < obj.DF_PROC_TYPE_LST.length; j++) {
                            var pItem = _.filter(list, function (x) { return x.DF_PROC_TYPE_ID == obj.DF_PROC_TYPE_LST[j] })[0];

                            console.log(pItem);
                            if (j == 0)
                                obj.DF_PROC_TYPE_NAME_LST = pItem.DF_PROC_TYPE_NAME;
                            else
                                obj.DF_PROC_TYPE_NAME_LST = obj.DF_PROC_TYPE_NAME_LST + ", " + pItem.DF_PROC_TYPE_NAME;
                        }

                        obj.DF_PROC_TYPE_LST = obj.DF_PROC_TYPE_LST ? obj.DF_PROC_TYPE_LST.join(',') : '0';
                        var idx = vm.ScItemListDS.indexOf(obj);;
                        if (idx < 0) {
                            obj["QTY_MOU_ID"] = 3;
                            var id = vm.ScItemListDS.data().length + 1;
                            vm.ScItemListDS.insert(id, obj);
                        }
                        else {
                            config.appToastMsg("MULTI-005 Duplicate item name exists!");
                        }

                        console.log(obj);
                    }

                });
            }
        }


        vm.removeRecipeItemData = function (data) {

            if (!data.MC_LD_RECIPE_D_ID || data.MC_LD_RECIPE_D_ID <= 0) {
                vm.ScItemListDS.remove(data);

                var i = 0;
                var row = 0;

                var list = _.filter(vm.ScItemListDS.data(), function (o) {
                    return o.SL_NO > 0;
                }).length;
                for (i = 0; i < list; i++) {
                    var list2 = _.filter(vm.ScItemListDS.data(), function (o) {
                        return o.SL_NO > 0;
                    })[i];
                    list2.SL_NO = ++row;
                }

                var list = _.filter(vm.ScItemListDS.data(), function (o) {
                    return o.QTY_MOU_ST === "%";
                })
                var sum = 0;
                for (var i = 0; i < list.length; i++)
                    sum = sum + list[i].DOSE_QTY;

                $("#TOTAL_WT").val(sum);

                return;
            };

            Dialog.confirm('Removing "' + data.ITEM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.ScItemListDS.remove(data);
                     var i = 0;
                     var row = 0;

                     var list = _.filter(vm.ScItemListDS.data(), function (o) {
                         return o.SL_NO > 0;
                     }).length;

                     for (i = 0; i < list; i++) {
                         var list2 = _.filter(vm.ScItemListDS.data(), function (o) {
                             return o.SL_NO > 0;
                         })[i];
                         if (list2)
                             list2.SL_NO = ++row;
                     }
                 });

            //vm.ScItemListDS.remove(data);


            var list = _.filter(vm.ScItemListDS.data(), function (o) {
                return o.QTY_MOU_ST === "%";
            })
            var sum = 0;
            for (var i = 0; i < list.length; i++)
                sum = sum + list[i].DOSE_QTY;

            $("#TOTAL_WT").val(sum);
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

        function GetProgramTypeList() {

            return vm.programTypeList = {
                optionLabel: "-- Select Program Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(149).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };



        function GetProdCatList() {

            return vm.prodCatList = {
                optionLabel: "-- Select Production Category --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/security/UserMap/GetProdCatUserByID?pSC_USER_ID=' + (cur_user_id || null)).then(function (res) {
                                var list = _.filter(res, function (x) { return x.IS_ACTIVE == 'Y' })
                                for (var i = 0; i < list.length; i++) {
                                    if (i == 0)
                                        vm.form['RF_FAB_PROD_CAT_LST'] = list[i].RF_FAB_PROD_CAT_ID;
                                    else
                                        vm.form.RF_FAB_PROD_CAT_LST = vm.form.RF_FAB_PROD_CAT_LST + "," + list[i].RF_FAB_PROD_CAT_ID;
                                }
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "FAB_PROD_CAT_NAME",
                dataValueField: "RF_FAB_PROD_CAT_ID"
            };
        };


        function GetDfParamList() {

            return vm.dfParamList = {
                optionLabel: "-- Select Param --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/dye/ScPreTreatment/ScDfParamList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "PARAM_TYPE_EN_NAME",
                dataValueField: "RF_DF_PARAM_TYPE_ID"
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
                                var list = _.filter(res, function (o) { return (o.RF_REQ_TYPE_ID === 8 || o.RF_REQ_TYPE_ID === 9) });
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

        vm.reloadScParty = function () {
            vm.supplierList.read();


            //DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
            //    if (vm.form.IS_SC == 'Y') {
            //        var list = _.filter(res, function (x) { return x.LK_SUP_TYPE_ID != 717 })
            //        vm.scPartyList = list;
            //        alert(vm.form.IS_SC);
            //    }
            //    else {
            //        var list = _.filter(res, function (x) { return x.LK_SUP_TYPE_ID == 717 });
            //        vm.scPartyList = list;
            //    }
            //});

        }

        function GetSupplier() {
            vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                            if (vm.form.IS_SC == 'Y') {
                                var list = _.filter(res, function (x) { return x.LK_SUP_TYPE_ID != 717 })
                                e.success(list);
                            }
                            else {
                                var list = _.filter(res, function (x) { return x.LK_SUP_TYPE_ID == 717 });
                                e.success(list);
                            }
                        });
                    }
                }
            });
        }

        function GetScPartyList() {

            return vm.scPartyList = {
                optionLabel: "-- Select S/C Party --",
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

        $scope.EXP_DELV_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.EXP_DELV_DT_LNopened = true;
        }

        $scope.PRG_ISS_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PRG_ISS_DT_LNopened = true;
        }

        $scope.CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.CHALAN_DT_LNopened = true;
        }

        function getStyleExtList(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
            $scope.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=" + (vm.form.RF_FAB_PROD_CAT_LST || "2,5,6");
                        url += "&pFIRSTDATE=" + (pFIRSTDATE || null);
                        url += "&pLASTDATE=" + (pLASTDATE || null);

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pSTYLE_NO';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + (formData.MC_FAB_PROD_ORD_H_ID || null);
                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };


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

        function getFabOederByOh(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID) {
            $scope.FabOederByOhDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=" + (vm.form.RF_FAB_PROD_CAT_LST || "2,5,6");
                        url += "&pFIRSTDATE=" + (pFIRSTDATE || null);
                        url += "&pLASTDATE=" + (pLASTDATE || null);

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pORDER_NO_LST';
                        }
                        url += '&pMC_FAB_PROD_ORD_H_ID=' + pMC_FAB_PROD_ORD_H_ID;

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };


        getFabOederByOh(null, formData.RF_FAB_PROD_CAT_ID, null, null, null);
        getStyleExtList(null, formData.RF_FAB_PROD_CAT_ID, null, null);


        function refreshLastEvtDate(pDYE_MACHINE_ID, host) {
            return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID=null&pIS_SMP_BLK&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID + '&pDYE_MACHINE_ID=' + pDYE_MACHINE_ID).then(function (res) {
                if (res.length == 1) {
                    host['DYE_BATCH_PLAN_ID'] = -1;
                    host['LOAD_TIME'] = res[0].LAST_EVT_END;
                    host['RQD_PRD_QTY'] = 0;
                    host['UN_LOAD_TIME'] = moment(res[0].LAST_EVT_END).add($scope.form.DURATION, 'h')._d;
                    getDfProcessTypeData();
                }
            });
        }

        $scope.onChangeTrims = function (data, pINV_ITEM_CAT_ID) {
            if (data.IS_WITH_TRMS == 'Y') {

                return DyeingDataService.getDataByUrl('/DyeBatch/GetDataFabReqCalProgramWithTrims?pMC_FAB_PROD_ORD_H_LST=' + data.MC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + data.FAB_COLOR_ID).then(function (res) {
                    $scope.form['requirements'] = res;
                });

            }
        };


        $scope.McDataDs = new kendo.data.DataSource({
            data: []
        });



        function getWovenFabricAvailability(pMC_FAB_PROD_ORD_H_LST, pMC_COLOR_ID, pINV_ITEM_CAT_ID, host) {
            if (!pMC_FAB_PROD_ORD_H_LST)
                return;

            var url = '/DyeBatchPlan/getProgAvailabilityWovenFab';
            url += '?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_LST;
            url += '&pMC_COLOR_ID=' + pMC_COLOR_ID;
            url += '&pINV_ITEM_CAT_ID=' + pINV_ITEM_CAT_ID;
            DyeingDataService.getDataByUrl(url).then(function (res) {
                host['requirements'] = res;
            });

        };
        function getColorDs(pMC_FAB_PROD_ORD_H_LST, pINV_ITEM_CAT_ID, host) {
            if (!pMC_FAB_PROD_ORD_H_LST)
                return;

            var url = '/DyeBatchPlan/getColorList';
            url += '?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_LST;
            url += '&pINV_ITEM_CAT_ID=' + pINV_ITEM_CAT_ID;
            DyeingDataService.getDataByUrl(url).then(function (res) {
                host['color_list_ds'] = new kendo.data.DataSource({
                    data: res
                });
            });

        };
        $scope.onChangeColor = function (e, cat_id, host) {
            var col = e.sender.dataItem(e.sender.item);
            if (!col.MC_COLOR_ID)
                return;
            getWovenFabricAvailability(host.MC_FAB_PROD_ORD_H_ID, host.FAB_COLOR_ID, cat_id, host);
        };


        $scope.onBoundDyeMc = function (e) {
            // e.sender.value(formData.DYE_MACHINE_ID);
        };


        $scope.onChangeDyeMc = function (e, host) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.id) {
                host['NO_OF_BATCH'] = 1;
                host['INTERVAL'] = 10;
                host['MC_FAB_PROD_ORD_H_ID_LST'] = [-1];
                host['DYE_BATCH_PLAN_ID'] = -1;
                host['DYE_BATCH_SCDL_ID'] = formData.DYE_BATCH_SCDL_ID;
                host['DYE_MACHINE_ID'] = item.id;
                host['END_DT'] = item.END_DT;

                DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID=null&pIS_SMP_BLK=' + formData.IS_SMP_BLK + '&pDYE_BATCH_SCDL_ID=' + formData.DYE_BATCH_SCDL_ID + '&pDYE_MACHINE_ID=' + item.id).then(function (res) {
                    if (res.length == 1) {
                        host['LOAD_TIME'] = res[0].LAST_EVT_END;
                        host['MAX_LOAD'] = res[0].MAX_LOAD;
                        host['PCT_EFFCNCY'] = res[0].PCT_EFFCNCY;
                    }
                });

                host['START_DT'] = item.START_DT;
                host['UN_LOAD_TIME'] = '';
                host['IS_ADD_OTH_ORD'] = 'N';
                host['DURATION'] = '';


            }
        };
        $scope.alerts = [];
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };
        $scope.ColourTypelist = {
            optionLabel: "-- Col Type--",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.LookupListData(74).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };

        vm.findGridData = function (dataOri) {

            if (fnValidateSub() == true) {
                var Defdata = {
                    MC_BYR_ACC_GRP_ID: null,
                    FIRSTDATE: null,
                    LASTDATE: null,
                    MC_FAB_PROD_ORD_H_IDD: null,
                    MC_STYLE_H_EXT_ID: null,
                    MC_COLOR_ID: null,
                    RF_FAB_PROD_CAT_ID: ''
                }

                var data = dataOri ? dataOri : Defdata;
                var url = "/api/Knit/FabProdKnitOrder/getFabOrderDataForScProgram?pMC_BYR_ACC_GRP_ID=" + (data.MC_BYR_ACC_GRP_ID || null);
                //url += "&pRF_FAB_PROD_CAT_ID=" + formData.RF_FAB_PROD_CAT_ID;
                url += "&pFIRSTDATE=" + (data.FIRSTDATE || null);
                url += "&pRF_FAB_PROD_CAT_ID_LST=" + (vm.form.RF_FAB_PROD_CAT_ID || vm.form.RF_FAB_PROD_CAT_LST || '1,2,3,4,5,6,7,8,9,10');
                url += "&pLASTDATE=" + (data.LASTDATE || null);
                url += "&pMC_FAB_PROD_ORD_H_ID=" + ((data.MC_FAB_PROD_ORD_H_IDDD || data.MC_FAB_PROD_ORD_H_IDD) || null);
                url += "&pMC_STYLE_H_EXT_ID=" + (data.MC_STYLE_H_EXT_ID || null);
                url += "&pMC_COLOR_ID=" + (data.MC_COLOR_ID || null);
                url += '&pageNumber=1&pageSize=50';

                console.log(url);

                DyeingDataService.getDataByFullUrl(url).then(function (res) {
                    console.log(res);
                    vm.OrderList = res.data;
                });
            }
        };


        vm.findBatchFabData = function () {

            //var url = "/api/dye/ScPreTreatment/SelectBatchFabForPT?pDYE_BATCH_NO=" + (vm.form.DYE_BATCH_NO || null);
            DyeingDataService.getDataByFullUrl("/api/dye/ScPreTreatment/SelectBatchFabForPT?pDYE_BATCH_NO=" + (vm.form.DYE_BATCH_NO || null)).then(function (res) {
                vm.OrderList = res;
                vm.form.DYE_BT_CARD_H_ID = res[0].DYE_BT_CARD_H_ID;
            });
        };


        function refreshRequirement(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID) {
            return DyeingDataService.getDataByUrl('/DyeBatch/GetDataFabReqCalProgram?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + pFAB_COLOR_ID).then(function (res) {
                $scope.form['requirements'] = res;
            });
        }





        $scope.gridSearchResultOpt = {
            height: '600px',
            scrollable: true,
            columns: [
                { field: "BYR_ACC_NAME_EN", title: "ByrAcc", width: "50px", template: "<span style=\"display: block;\">#=BYR_ACC_NAME_EN #</span> <span style=\"display: block;font-style:italic;color:FUCHSIA;\"><small>(#:FAB_PROD_CAT_NAME#)</small><span>" },
                { field: "STYLE_NO", title: "Order/Style", width: "50px", template: "<span style=\"display: block;\">#=ORDER_NO_LIST #</span> <span style=\"display: block;font-style:italic;\"><small>#:STYLE_NO#</small><span>" },

                { field: "ORDER_NO_LIST_CON", hidden: true, title: "Order #", width: "80px", groupHeaderTemplate: " <b>  #= value # </b>" },
                { field: "COL_TYPE_NAME", title: "Type", width: "30px" },
                { field: "COLOR_NAME_EN", title: "Color Name", width: "40px", template: "<span style=\"display: block;\">#=COLOR_NAME_EN #</span> # if( !LD_RECIPE_NO) {#<span style=\"display: block;color:red;\">No Labdip<span># }#" },

                { field: "FABRIC_DESC", title: "Fabric Info", width: "80px" },
                { field: "DF_PROC_TYPE_NAME_LST", title: "Pre-Process", width: "80px" },

                { field: "NET_GFAB_QTY", title: "Req. Qty", width: "40px" },
                { field: "KNT_FAB_QTY", title: "Grey Qty", width: "40px" },
                { field: "BATCH_GFAB_QTY", title: "BT", width: "40px" },
                { field: "BAL_GFAB_QTY", title: "Balance", width: "40px" },
            ],
            change: function (e) {
                var item = e.sender.dataItem(this.select());


                $scope.form['BYR_ACC_NAME_EN'] = item.BYR_ACC_NAME_EN;
                $scope.form['COLOR_NAME_EN'] = item.COLOR_NAME_EN;
                $scope.form['DYE_MTHD_NAME'] = item.DYE_MTHD_NAME;
                $scope.form['FAB_TYPE_NAME'] = item.FAB_TYPE_NAME;
                $scope.form['ORDER_NO_LIST'] = item.ORDER_NO_LIST;
                $scope.form['STYLE_NO'] = item.STYLE_NO;

                $scope.form['FAB_COLOR_ID'] = item.FAB_COLOR_ID;
                $scope.form['LK_DYE_MTHD_ID'] = item.LK_DYE_MTHD_ID;
                $scope.form['RF_FAB_TYPE_ID'] = item.RF_FAB_TYPE_ID;
                $scope.form['MC_FAB_PROD_ORD_H_ID'] = item.MC_FAB_PROD_ORD_H_ID;
                $scope.form['FAB_PROD_CAT_NAME'] = item.FAB_PROD_CAT_NAME;

                $scope.form['RF_FAB_PROD_CAT_ID'] = item.RF_FAB_PROD_CAT_ID;
                $scope.form['MC_STYLE_H_EXT_ID'] = item.MC_STYLE_H_EXT_ID;
                $scope.form['MC_BYR_ACC_GRP_ID'] = item.MC_BYR_ACC_GRP_ID;
                $scope.form['IS_WITH_TRMS'] = 'N';

                DyeingDataService.getDataByUrl('/DyeBatch/GetDataFabReqCalProgram?pMC_FAB_PROD_ORD_H_LST=' + item.MC_FAB_PROD_ORD_H_ID + '&pFAB_COLOR_ID=' + item.FAB_COLOR_ID).then(function (res) {
                    $scope.form['requirements'] = res;
                });

                if (cur_tab != 'KNIT') {
                    $scope.onSelect(cur_tab);
                }



            },
            selectable: true,
        };




        function getSelectMonth(MC_BYR_ACC_GRP_ID, RF_FAB_PROD_CAT_ID) {
            return DyeingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/0/0/' + (RF_FAB_PROD_CAT_ID || 0) + '?pMC_BYR_ACC_GRP_ID=' + (MC_BYR_ACC_GRP_ID || null)).then(function (res) {
                $scope.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        };

        $scope.onChangeShipMonth = function (e) {
            var itmShipMonth = e.sender.dataItem(e.sender.item);

            if (itmShipMonth.FIRSTDATE && itmShipMonth.LASTDATE) {
                $scope.search['FIRSTDATE'] = itmShipMonth.FIRSTDATE;
                $scope.search['LASTDATE'] = itmShipMonth.LASTDATE;

                getStyleExtList(($scope.search.MC_BYR_ACC_ID || null), formData.RF_FAB_PROD_CAT_ID, itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE)

                getFabOederByOh($scope.search.MC_BYR_ACC_ID, formData.RF_FAB_PROD_CAT_ID, itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE, null)


            } else {
                getStyleExtList(($scope.search.MC_BYR_ACC_ID || null), formData.RF_FAB_PROD_CAT_ID, null, null);
                $scope.search['FIRSTDATE'] = null;
                $scope.search['LASTDATE'] = null;

                getFabOederByOh($scope.search.MC_BYR_ACC_ID, formData.RF_FAB_PROD_CAT_ID, null, null, null)
            }
        };


        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME #</p></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.FAB_PROD_CAT_SNAME #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

        $scope.onFabOrderChange = function (e) {
            var itm = e.sender.dataItem(e.sender.item);
            if (itm.MC_FAB_PROD_ORD_H_ID) {
                getFabOederByOh(null, null, null, null, itm.MC_FAB_PROD_ORD_H_ID);

            } else {
                getFabOederByOh(null, null, null, null, null);
            }


        };

        DyeingDataService.getDataByFullUrl('/api/common/GetDyeingMethodList').then(function (res) {
            $scope.DyeingMthdDs = new kendo.data.DataSource({
                data: res
            });
        });

        $scope.buyerAcGrpList = {
            optionLabel: "--- Buyer A/C Group ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        return DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            },
            select: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_BYR_ACC_GRP_ID.length != 0) {
                    getStyleExtList(item.MC_BYR_ACC_GRP_ID, null);
                    getSelectMonth(item.MC_BYR_ACC_GRP_ID);
                    getFabOederByOh(item.MC_BYR_ACC_GRP_ID, null, null, null, null);
                }
            },
            dataTextField: "BYR_ACC_GRP_NAME_EN",
            dataValueField: "MC_BYR_ACC_GRP_ID"
        };




        $scope.addOtherOrder = function (data) {
            data.push(null);
        };

        $scope.remOtherOrder = function (data, idx) {
            if (data.length > 1) {
                data.splice(idx, 1);
            }

        };

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };



        $scope.save = function (data, isValid) {

            if (!isValid) {
                return;
            }
            var RANGE_START;
            var RANGE_END;

            var DfProcessData = [];

            var ORD_LIST = [];


            if (data.IS_ADD_OTH_ORD == 'Y') {
                data['LNK_ORD_H_ID_LST'] = ORD_LIST.join(',');
            } else {
                data['LNK_ORD_H_ID_LST'] = null;
            }
            var startDate;
            var data2Save = [];

            angular.forEach($scope.df_data, function (val, key) {
                angular.forEach(val.items, function (v, k) {
                    if (v.IS_SELECTED == "Y") {
                        DfProcessData.push({
                            DYE_BT_RQD_PROC_ID: v.DYE_BT_RQD_PROC_ID,
                            DF_PROC_TYPE_ID: v.DF_PROC_TYPE_ID
                        });
                    }
                })
            });


            var DfProcessXml = config.xmlStringShortNoTag(DfProcessData);


            for (var i = 0; i < data.NO_OF_BATCH; i++) {

                if (i == 0) {
                    data2Save = [];
                    startDate = new DayPilot.Date(data.LOAD_TIME).addMinutes(0);
                    RANGE_START = startDate;
                } else if (i > 0 && data2Save.length > 0) {
                    startDate = new DayPilot.Date(data2Save[i - 1]['UN_LOAD_TIME']).addMinutes(data.INTERVAL);
                }


                data2Save.push({
                    DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID,
                    DYE_BATCH_SCDL_ID: data.DYE_BATCH_SCDL_ID,
                    DYE_MACHINE_ID: data.DYE_MACHINE_ID,
                    MC_FAB_PROD_ORD_H_ID: data.MC_FAB_PROD_ORD_H_ID,
                    FAB_COLOR_ID: data.FAB_COLOR_ID,
                    RF_FAB_TYPE_ID: data.RF_FAB_TYPE_ID,
                    LK_DYE_MTHD_ID: data.LK_DYE_MTHD_ID,
                    INV_ITEM_CAT_ID: 34,
                    INV_ITEM_CAT_LST: data.IS_WITH_TRMS == "Y" ? "34,7" : "34",
                    PRIORITY_NO: i,
                    RQD_PRD_QTY: data.RQD_PRD_QTY,
                    QTY_MOU_ID: 3,
                    IS_WITH_TRMS: data.IS_WITH_TRMS,
                    LNK_ORD_H_ID_LST: data.LNK_ORD_H_ID_LST,
                    LOAD_TIME: startDate.toString(),
                    UN_LOAD_TIME: startDate.addHours(data.DURATION).toString(),
                    DF_XML: DfProcessXml
                });

                RANGE_END = startDate.addHours(data.DURATION);

            };


            console.log(data2Save);

            // ***************************************** END ***************************************

            return DyeingDataService.saveDataByUrl({
                DYE_BATCH_SCDL_ID: formData.DYE_BATCH_SCDL_ID,
                XML: config.xmlStringShort(data2Save),
                pOption: 1001,
                IS_SMP_BLK: 'B'
            }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        refreshLastEvtDate(data.DYE_MACHINE_ID, data);
                        //$modalInstance.close({
                        //    KNT_SC_PRG_ISS_ID: parseInt(res.data.OP_KNT_SC_PRG_ISS_ID || 0)
                        //});
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });
        };


        vm.submitAll = function (dataOri, mode) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(data);

                vm.showSplash = true;

                data["IS_UPDATE"] = mode;
                data["DF_PROC_TYPE_LST"] = data.DF_PROC_TYPE_LST ? data.DF_PROC_TYPE_LST.join(',') : '0';

                var _idate = $filter('date')(data.PRG_ISS_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["PRG_ISS_DT"] = _idate;

                var _edate = $filter('date')(data.EXP_DELV_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["EXP_DELV_DT"] = _edate;

                data["ISS_STORE_ID"] = 8;

                data["XML_REQ_D"] = DyeingDataService.xmlStringShort(vm.ScItemListDS.data().map(function (o) {
                    return {
                        DF_SC_PT_ISS_D1_ID: o.DF_SC_PT_ISS_D1_ID == 0 ? null : o.DF_SC_PT_ISS_D1_ID,
                        DF_SC_PT_ISS_H_ID: o.DF_SC_PT_ISS_H_ID == 0 ? null : o.DF_SC_PT_ISS_H_ID,
                        KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_ID,
                        RQD_QTY: o.RQD_QTY,
                        DF_PROC_TYPE_LST: o.DF_PROC_TYPE_LST,
                        ISS_ROLL_QTY: o.ISS_ROLL_QTY == null ? 0 : o.ISS_ROLL_QTY,
                        ISS_QTY: o.ISS_QTY == null ? 0 : o.ISS_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID
                    }
                }));

                if (data.DF_SC_PT_ISS_H_ID > 0 && vm.PreProcessList.length > 0) {
                    data["XML_PROC_D"] = DyeingDataService.xmlStringShort(vm.PreProcessList.map(function (o, index) {
                        return {
                            DF_SC_PT_ISS_D2_ID: o.DF_SC_PT_ISS_D2_ID == 0 ? null : o.DF_SC_PT_ISS_D2_ID,
                            DF_SC_PT_ISS_H_ID: o.DF_SC_PT_ISS_H_ID == 0 ? null : o.DF_SC_PT_ISS_H_ID,
                            SCM_SUPPLIER_ID: o.SCM_SUPPLIER_ID == 0 ? null : o.SCM_SUPPLIER_ID,
                            DF_PROC_TYPE_ID: o.DF_PROC_TYPE_ID == 0 ? null : o.DF_PROC_TYPE_ID,
                            REC_ID: index,
                        }
                    }));


                    var arrParamList = _.filter(vm.PreProcessList[0].scParamList, function (x) { return x.PARAM_DESC == "Afzal" });

                    for (var i = 0; i < vm.PreProcessList.length; i++) {
                        var list = vm.PreProcessList[i].scParamList;
                        for (var j = 0; j < list.length; j++) {
                            var idx = arrParamList.length;
                            var obj2 = angular.copy(list[j]);
                            obj2["REC_ID"] = i;
                            arrParamList.splice(idx, "0", angular.copy(obj2));
                        }
                    }

                    console.log(arrParamList);

                    data["XML_PROC_PRM"] = DyeingDataService.xmlStringShort(arrParamList.map(function (o) {
                        return {
                            DF_SC_PT_ISS_D2_ID: o.DF_SC_PT_ISS_D2_ID == 0 ? null : o.DF_SC_PT_ISS_D2_ID,
                            DF_SC_PT_ISS_D3_ID: o.DF_SC_PT_ISS_D3_ID == 0 ? null : o.DF_SC_PT_ISS_D3_ID,
                            RF_DF_PARAM_TYPE_ID: o.RF_DF_PARAM_TYPE_ID == 0 ? null : o.RF_DF_PARAM_TYPE_ID,
                            PARAM_DESC: o.PARAM_DESC,
                            REC_ID: o.REC_ID,
                        }
                    }));
                }

                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/ScPreTreatment/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        $state.go($state.current, { pDF_SC_PT_ISS_H_ID: (res.data.OPDF_SC_PT_ISS_H_ID || 0) }, { reload: true, inherit: false });
                    }
                });
            }
        };


        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6007';
            vm.form.DF_SC_PT_ISS_H_ID = dataItem.DF_SC_PT_ISS_H_ID;

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

