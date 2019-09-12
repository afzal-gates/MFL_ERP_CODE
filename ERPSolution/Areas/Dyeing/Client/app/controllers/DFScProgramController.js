(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DFScProgramController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', 'Dialog', DFScProgramController]);
    function DFScProgramController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.DF_SC_PRG_ISS_H_ID ? formData : { DF_SC_PRG_ISS_H_ID: 0, IS_AOP: 'N', SC_PRG_DT: vm.today, PRG_ISS_BY: cur_user_id };

        if (!vm.form.SC_PRG_DT)
            vm.form.SC_PRG_DT = vm.today;

        //vm.form.SCM_STORE_ID = formData.SCM_STORE_ID;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getDfProcessTypeData(), getUserData(), ReceiveItemList(), GetStoreList(), GetCompanyList(), GetMOUList(), GetSupplierList(),
                GetProcessList(), GetStatusList(), GetProdCatList(), GetReqSourceList(), GetReqTypeList(), GetDfParamList(), GetScPartyList(), loadProcessDetailData()
            ];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        if (formData) {
            vm.form["DF_PROC_TYPE_LST"] = formData.DF_PROC_TYPE_LST ? formData.DF_PROC_TYPE_LST.split(',') : [];

        }

        vm.removeParam = function (item, obj) {
            var idx = item.scParamList.indexOf(obj);
            if (item.scParamList.length > 1)
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

        $scope.SC_PRG_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.SC_PRG_DT_LNopened = true;
        }

        $scope.EXP_DELV_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.EXP_DELV_DT_LNopened = true;
        }

        $scope.CHALAN_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.CHALAN_DT_LNopened = true;
        }



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

        function getDfProcessTypeData() {
            return vm.processTypeList = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByUrl('/DFProcessType/SelectAll').then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_PROC_SUB_GRP_ID == 576 || x.LK_PROC_SUB_GRP_ID == 575 || x.LK_PROC_SUB_GRP_ID == 577 || x.LK_PROC_SUB_GRP_ID == 578 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "DF_PROC_TYPE_NAME",
                dataValueField: "DF_PROC_TYPE_ID"
            };
        }

        function ReceiveItemList() {
            DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/GetScProgramDetailInfo?pDF_SC_PRG_ISS_H_ID=' + (formData.DF_SC_PRG_ISS_H_ID || "0")).then(function (res) {

                //console.log(res[0]);
                var list = res.map(function (o) {
                    return {
                        ACT_BATCH_QTY: o.ACT_BATCH_QTY,
                        BUYER_NAME_EN: o.BUYER_NAME_EN,
                        COLOR_NAME_EN: o.COLOR_NAME_EN,
                        DF_PROC_TYPE_LST: o.DF_PROC_TYPE_LST ? o.DF_PROC_TYPE_LST.split(',') : [],
                        DF_SC_PRG_ISS_BT_D1_ID: o.DF_SC_PRG_ISS_BT_D1_ID,
                        DF_SC_PRG_ISS_H_ID: o.DF_SC_PRG_ISS_H_ID,
                        DIA_TYPE_NAME: o.DIA_TYPE_NAME,
                        DYE_BATCH_NO: o.DYE_BATCH_NO,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                        DYE_LOT_NO: o.DYE_LOT_NO,
                        FAB_ITEM_DESC: o.FAB_ITEM_DESC,
                        FAB_TYPE_NAME: o.FAB_TYPE_NAME,
                        FIB_COMP_NAME: o.FIB_COMP_NAME,
                        FIN_DIA: o.FIN_DIA,
                        FIN_GSM: o.FIN_GSM,
                        ISS_QTY: o.ISS_QTY,
                        ISS_ROLL_QTY: o.ISS_ROLL_QTY,
                        IS_ACTIVE: o.IS_ACTIVE,
                        KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID,
                        KNT_YRN_LOT_LST: o.KNT_YRN_LOT_LST,
                        LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                        ORDER_NO: o.ORDER_NO,
                        QTY_MOU_ID: o.QTY_MOU_ID,
                        RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                        RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                        RQD_GSM: o.RQD_GSM,
                        STYLE_NO: o.STYLE_NO,
                        processTypeList: o.processTypeList
                    }
                })
                vm.scProgramList = list;
            });
        }

        vm.checkQty = function (item) {
            if ((parseFloat(item.ACT_BATCH_QTY) - (parseFloat(item.PRE_ISS_QTY) + parseFloat(item.ISS_QTY))) < 0) {
                item.ISS_QTY = '';

                config.appToastMsg("MULTI-005 Issue Quantity Limit is Over!!!");
            }

        }

        vm.searchBatchInfo = function (pPageNo) {
            if (vm.form.DYE_BATCH_NO && vm.form.DYE_BATCH_NO.length > 0) {
                var list = _.filter(vm.scProgramList, function (x) { return x.DYE_BATCH_NO == vm.form.DYE_BATCH_NO });
                if (list.length == 0) {
                    vm.showSplash = true;
                    DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/SelectForScProgram?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "") + '&pIS_AOP=' + (vm.form.IS_AOP || 'N')).then(function (res) {
                        if (!vm.scProgramList)
                            vm.scProgramList = _.filter(res, function (x) { return x.DYE_BT_CARD_H_ID == 0 });

                        if (res.length > 0) {
                            angular.forEach(res, function (val, key) {
                                val["processTypeList"] = [];

                                //var id = _.filter(vm.scProgramList, function (x) { return x.DYE_BATCH_NO === vm.form.DYE_BATCH_NO }).length;
                                //if (id <= 0)
                                vm.scProgramList.push(val);
                                //else
                                //    config.appToastMsg("MULTI-005 Batch No:" + vm.form.DYE_BATCH_NO + " already exists!");
                            });
                        }
                    });
                }
                else {
                    config.appToastMsg("MULTI-005 This Batch Card Already Exists in Your List!!!");
                }
            }
        }

        vm.addAllNewRecoed = function (itemData, indx) {
            var idx = indx + 1;
            itemData["IS_ACTIVE"] = true;

            var item = { "DF_PROC_TYPE_ID": "0", "PARAM_DESC": "", "DYE_BT_CARD_H_ID": itemData.DYE_BT_CARD_H_ID };
            itemData['processTypeList'].push(item);
            console.log(itemData);
        }

        vm.removeExtraRecord = function (itemData, data) {

            Dialog.confirm('Removing "' + data.PARAM_DESC + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var index = itemData['processTypeList'].indexOf(data);
                     itemData['processTypeList'].splice(index, 1);
                 });
        }

        function GetProcessList() {
            return vm.processList = {
                optionLabel: "-- Select Process --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/GetDFProcessType').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DF_PROC_TYPE_NAME",
                dataValueField: "DF_PROC_TYPE_ID"
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

        function GetStoreList() {

            return vm.storeList = {
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



        function loadProcessDetailData() {

            DyeingDataService.getDataByUrl('/DfScProgram/GetScProgramProcessDetail?pDF_SC_PRG_ISS_H_ID=' + (formData.DF_SC_PRG_ISS_H_ID || 0)).then(function (res) {
                vm.PreProcessList = res;
            });

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



        vm.submitAll = function (dataOri, mode) {

            if (fnValidate() == true) {
                var data = angular.copy(dataOri);

                console.log(data);

                vm.showSplash = true;

                data["IS_UPDATE"] = mode;
                data["DF_PROC_TYPE_LST"] = data.DF_PROC_TYPE_LST ? data.DF_PROC_TYPE_LST.join(',') : '0';


                var _proDate = $filter('date')(data.SC_PRG_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["SC_PRG_DT"] = _proDate;

                var _idate = $filter('date')(data.PRG_ISS_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["PRG_ISS_DT"] = _idate;

                var _edate = $filter('date')(data.EXP_DELV_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["EXP_DELV_DT"] = _edate;

                data["SCM_STORE_ID"] = 8;
                data["IS_BATCH_WISE"] = 'Y';


                data["XML_BT_D"] = DyeingDataService.xmlStringShort(vm.scProgramList.map(function (o) {
                    return {
                        DF_SC_PRG_ISS_BT_D1_ID: o.DF_SC_PRG_ISS_BT_D1_ID == null ? 0 : o.DF_SC_PRG_ISS_BT_D1_ID,
                        DF_SC_PRG_ISS_H_ID: o.DF_SC_PRG_ISS_H_ID == null ? 0 : o.DF_SC_PRG_ISS_H_ID,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID == null ? data.DYE_BT_CARD_H_ID : o.DYE_BT_CARD_H_ID,
                        KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID == null ? 0 : o.KNT_STYL_FAB_ITEM_H_ID,
                        ISS_QTY: o.ISS_QTY == null ? 0 : o.ISS_QTY,
                        ISS_ROLL_QTY: o.ISS_ROLL_QTY == null ? 0 : o.ISS_ROLL_QTY,
                        DF_PROC_TYPE_LST: o.DF_PROC_TYPE_LST,
                        LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID == 0 ? null : o.LK_DIA_TYPE_ID,
                        RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID == 0 ? null : o.RF_FAB_TYPE_ID,
                        RF_FIB_COMP_ID: o.RF_FIB_COMP_ID == 0 ? null : o.RF_FIB_COMP_ID,
                        RQD_GSM: o.RQD_GSM == null ? 0 : o.RQD_GSM,
                        FIN_DIA: o.FIN_DIA == null ? 0 : o.FIN_DIA,

                        QTY_MOU_ID: 3
                    }
                }));

                if (data.DF_SC_PRG_ISS_H_ID > 0 && vm.PreProcessList.length > 0) {
                    data["XML_PROC_D"] = DyeingDataService.xmlStringShort(vm.PreProcessList.map(function (o, index) {
                        return {
                            DF_SC_PRG_ISS_PROC_D2_ID: o.DF_SC_PRG_ISS_PROC_D2_ID == 0 ? null : o.DF_SC_PRG_ISS_PROC_D2_ID,
                            DF_SC_PRG_ISS_H_ID: o.DF_SC_PRG_ISS_H_ID == 0 ? null : o.DF_SC_PRG_ISS_H_ID,
                            DYE_BT_CARD_H_ID: (o.DYE_BT_CARD_H_ID == null || o.DYE_BT_CARD_H_ID == 0) ? null : o.DYE_BT_CARD_H_ID,
                            DF_PROC_TYPE_ID: o.DF_PROC_TYPE_ID == 0 ? null : o.DF_PROC_TYPE_ID,
                            EXP_DELV_DT: o.EXP_DELV_DT == 0 ? null : o.EXP_DELV_DT,
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
                            if (obj2.RF_DF_PARAM_TYPE_ID > 0)
                                arrParamList.splice(idx, "0", angular.copy(obj2));
                        }
                    }

                    console.log(arrParamList);

                    data["XML_PROC_PRM"] = DyeingDataService.xmlStringShort(arrParamList.map(function (o) {
                        return {
                            DF_SC_PRG_ISS_PROC_D2_ID: o.DF_SC_PRG_ISS_PROC_D2_ID == 0 ? null : o.DF_SC_PRG_ISS_PROC_D2_ID,
                            DF_SC_PRG_ISS_PROC_D3_ID: o.DF_SC_PRG_ISS_PROC_D3_ID == 0 ? null : o.DF_SC_PRG_ISS_PROC_D3_ID,
                            RF_DF_PARAM_TYPE_ID: o.RF_DF_PARAM_TYPE_ID == 0 ? null : o.RF_DF_PARAM_TYPE_ID,
                            PARAM_DESC: o.PARAM_DESC,
                            REC_ID: o.REC_ID,
                        }
                    }));
                }


                data["CHALAN_DT"] = _proDate;
                data["IS_FINALIZED"] = 'Y';

                data["XML_ISS_D"] = DyeingDataService.xmlStringShort(vm.scProgramList.map(function (o) {
                    return {
                        DF_SC_PRG_ISS_BT_D1_ID: o.DF_SC_PRG_ISS_BT_D1_ID == 0 ? null : o.DF_SC_PRG_ISS_BT_D1_ID,
                        DF_SC_BT_CHL_ISS_D_ID: o.DF_SC_BT_CHL_ISS_D_ID == 0 ? null : o.DF_SC_BT_CHL_ISS_D_ID,
                        DF_SC_BT_CHL_ISS_H_ID: o.DF_SC_BT_CHL_ISS_H_ID == 0 ? null : o.DF_SC_BT_CHL_ISS_H_ID,
                        KNT_STYL_FAB_ITEM_H_ID: o.KNT_STYL_FAB_ITEM_H_ID == 0 ? null : o.KNT_STYL_FAB_ITEM_H_ID,
                        DF_PROC_TYPE_LST: o.DF_PROC_TYPE_LST,
                        ISS_ROLL_QTY: o.ISS_ROLL_QTY == null ? 0 : o.ISS_ROLL_QTY,
                        ISS_QTY: o.ISS_QTY == null ? 0 : o.ISS_QTY,
                        QTY_MOU_ID: o.QTY_MOU_ID == null ? 3 : o.QTY_MOU_ID
                    }
                }));


                return DyeingDataService.saveDataByFullUrl(data, '/api/Dye/DfScProgram/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        
                        if (parseInt(res.data.OPDF_SC_PRG_ISS_H_ID) > 0) {
                            $state.go($state.current, { pDF_SC_PRG_ISS_H_ID: (res.data.OPDF_SC_PRG_ISS_H_ID || 0) }, { reload: true });
                        }

                    }
                });
            }
        };




    }


})();

