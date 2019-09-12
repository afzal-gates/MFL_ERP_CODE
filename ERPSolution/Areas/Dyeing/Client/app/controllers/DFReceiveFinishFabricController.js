(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DFReceiveFinishFabricController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', 'Dialog', DFReceiveFinishFabricController]);
    function DFReceiveFinishFabricController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        vm.form = formData.DF_SCO_FSTR_RCV_H_ID ? formData : { DF_SCO_FSTR_RCV_H_ID: 0, RCV_DT: vm.today, CREATED_BY: cur_user_id };

        if (!vm.form.RCV_DT)
            vm.form.RCV_DT = vm.today;

        vm.form.SCM_STORE_ID = formData.SCM_STORE_ID;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetStoreList(), GetMOUList(), GetSupplierList(), GetProcessList(), ScProgramDetails(), getUserData(),getReceiveDetailList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
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
            if (item.RCV_FIN_FAB_QTY > item.ISS_QTY) {
                item.RCV_FIN_FAB_QTY = '';
            }
            else {
                item.LOSS_QTY = item.ISS_QTY - item.RCV_FIN_FAB_QTY;
                //item.LOSS_QTY_P = (item.LOSS_QTY/item.ISS_QTY)* 100;
            }
        }


        vm.checkOtherRcvQty = function (item) {
            if (item.RCV_QTY > item.ISS_QTY) {
                item.RCV_QTY = '';
            }
            else {
                item.LOSS_QTY = item.ISS_QTY - item.RCV_QTY;
                //item.LOSS_QTY_P = (item.LOSS_QTY/item.ISS_QTY)* 100;
            }
        }


        function ScProgramDetails() {
            DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/GetScProgramReceiveDtlInfo?pDF_SCO_FSTR_RCV_H_ID=' + (formData.DF_SCO_FSTR_RCV_H_ID || "0")).then(function (res) {
                vm.scProgramList = res;
            });
        }

        function getReceiveDetailList(){
            if ($stateParams.pDF_SCO_FSTR_RCV_H_ID > 0) {
                DyeingDataService.getDataByFullUrl('/api/Dye/DfRcvFinFabric/SelectReceiveInfoDtl?pDF_SCO_FSTR_RCV_H_ID=' + ($stateParams.pDF_SCO_FSTR_RCV_H_ID || "0")).then(function (res) {
                    vm.scProgramFabList = res.fab;
                    vm.scProgramCCList = res.CC;
                    vm.scProgramTrimList = res.Trim;
                });
            }
        }


        vm.getScProgramByChallanNo = function () {
            DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/GetScProgramDetailInfo?pCHALAN_NO=' + (vm.form.CHALLAN_NO || "")).then(function (res) {
                vm.scProgramList = res;
            });
        }

        vm.searchScProgramInfo = function (e) {
            if (vm.form.SCM_SUPPLIER_ID > 0) {
                DyeingDataService.getDataByFullUrl('/api/Dye/DfRcvFinFabric/SelectForReceive?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || "0")).then(function (res) {
                    vm.scProgramFabList = res.fab;
                    vm.scProgramCCList = res.CC;
                    vm.scProgramTrimList = res.Trim;
                });
            }
        }

        vm.searchBatchInfo = function (pPageNo) {
            if (vm.form.DYE_BATCH_NO && vm.form.DYE_BATCH_NO.length > 0) {
                vm.showSplash = true;
                DyeingDataService.getDataByFullUrl('/api/Dye/DfScProgram/SelectForScProgram?pDYE_BATCH_NO=' + (vm.form.DYE_BATCH_NO.trim() || "")).then(function (res) {
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
                        DyeingDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=579').then(function (res) {
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

        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var _clndate = $filter('date')(data.CHALAN_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["CHALAN_DT"] = _clndate;

                var _proDate = $filter('date')(data.RCV_DT, 'yyyy-MM-ddTHH:mm:ss');
                data["RCV_DT"] = _proDate;

                var list = _.filter(vm.scProgramFabList[0].processTypeList, function (x) { return x.DYE_BT_CARD_H_ID === 0 });

                console.log(list);

                for (var i = 0; i < vm.scProgramFabList.length; i++)
                    for (var j = 0; j < vm.scProgramFabList[i].processTypeList.length; j++)
                        if (vm.scProgramFabList[i].processTypeList[j].PROC_STATUS == true) {
                            vm.scProgramFabList[i].processTypeList[j].DYE_BT_CARD_H_ID = vm.scProgramFabList[i].DYE_BT_CARD_H_ID
                            vm.scProgramFabList[i].processTypeList[j]["DYE_SC_PRG_ISS_ID"] = vm.scProgramFabList[i].DYE_SC_PRG_ISS_ID
                            list.push(vm.scProgramFabList[i].processTypeList[j]);
                        }

                for (var i = 0; i < vm.scProgramCCList.length; i++)
                    for (var j = 0; j < vm.scProgramCCList[i].processTypeList.length; j++)
                        if (vm.scProgramCCList[i].processTypeList[j].PROC_STATUS == true) {
                            vm.scProgramCCList[i].processTypeList[j].DYE_BT_CARD_H_ID = vm.scProgramCCList[i].DYE_BT_CARD_H_ID
                            vm.scProgramCCList[i].processTypeList[j]["DYE_SC_PRG_ISS_ID"] = vm.scProgramCCList[i].DYE_SC_PRG_ISS_ID
                            list.push(vm.scProgramCCList[i].processTypeList[j]);
                        }

                for (var i = 0; i < vm.scProgramTrimList.length; i++)
                    for (var j = 0; j < vm.scProgramTrimList[i].processTypeList.length; j++)
                        if (vm.scProgramTrimList[i].processTypeList[j].PROC_STATUS == true) {
                            vm.scProgramTrimList[i].processTypeList[j].DYE_BT_CARD_H_ID = vm.scProgramTrimList[i].DYE_BT_CARD_H_ID
                            vm.scProgramTrimList[i].processTypeList[j]["DYE_SC_PRG_ISS_ID"] = vm.scProgramTrimList[i].DYE_SC_PRG_ISS_ID
                            list.push(vm.scProgramTrimList[i].processTypeList[j]);
                        }

                data["XML_RCV_PROC"] = DyeingDataService.xmlStringShort(list.map(function (o) {
                    return {
                        DF_SCO_FSTR_RCV_PROC_ID: o.DF_SCO_FSTR_RCV_PROC_ID == null ? 0 : o.DF_SCO_FSTR_RCV_PROC_ID,
                        DF_SCO_FSTR_RCV_H_ID: o.DF_SCO_FSTR_RCV_H_ID,
                        DYE_SC_PRG_ISS_ID: o.DYE_SC_PRG_ISS_ID,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID == 0 ? null : o.DYE_BT_CARD_H_ID,
                        DF_PROC_TYPE_ID: o.DF_PROC_TYPE_ID == null ? 0 : o.DF_PROC_TYPE_ID
                    }
                }));

                data["XML_RCV_D1"] = DyeingDataService.xmlStringShort(vm.scProgramFabList.map(function (o) {
                    return {
                        DF_SCO_FSTR_RCV_D1_ID: o.DF_SCO_FSTR_RCV_D1_ID == null ? 0 : o.DF_SCO_FSTR_RCV_D1_ID,
                        DYE_SC_PRG_ISS_ID: o.DYE_SC_PRG_ISS_ID == null ? 0 : o.DYE_SC_PRG_ISS_ID,
                        SC_BATCH_NO: o.SC_BATCH_NO == null ? 0 : o.SC_BATCH_NO,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                        KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID == null ? 0 : o.KNT_STYL_FAB_ITEM_ID,
                        RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                        RCV_FIN_FAB_QTY: o.RCV_FIN_FAB_QTY == null ? 0 : o.RCV_FIN_FAB_QTY,
                        LOSS_QTY: o.LOSS_QTY == null ? 0 : o.LOSS_QTY,
                        QTY_MOU_ID: 3,
                        NOTE: "",
                    }
                }));

                data["XML_RCV_D2"] = DyeingDataService.xmlStringShort(vm.scProgramCCList.map(function (o) {
                    return {
                        DF_SCO_FSTR_RCV_D2_ID: o.DF_SCO_FSTR_RCV_D2_ID == null ? 0 : o.DF_SCO_FSTR_RCV_D2_ID,
                        DYE_SC_PRG_ISS_ID: o.DYE_SC_PRG_ISS_ID == null ? 0 : o.DYE_SC_PRG_ISS_ID,
                        SC_BATCH_NO: o.SC_BATCH_NO == null ? 0 : o.SC_BATCH_NO,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                        KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID == null ? 0 : o.KNT_CLCF_STYL_ITEM_ID,
                        RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                        LOSS_QTY: o.LOSS_QTY == null ? 0 : o.LOSS_QTY,
                        QTY_MOU_ID: 3,
                        NOTE: "",
                    }
                }));

                data["XML_RCV_D3"] = DyeingDataService.xmlStringShort(vm.scProgramTrimList.map(function (o) {
                    return {
                        DF_SCO_FSTR_RCV_D3_ID: o.DF_SCO_FSTR_RCV_D3_ID == null ? 0 : o.DF_SCO_FSTR_RCV_D3_ID,
                        DYE_SC_PRG_ISS_ID: o.DYE_SC_PRG_ISS_ID == null ? 0 : o.DYE_SC_PRG_ISS_ID,
                        SC_BATCH_NO: o.SC_BATCH_NO == null ? 0 : o.SC_BATCH_NO,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                        MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID == null ? 0 : o.MC_ORD_TRMS_ITEM_ID,
                        RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                        LOSS_QTY: o.LOSS_QTY == null ? 0 : o.LOSS_QTY,
                        QTY_MOU_ID: 3,
                        NOTE: "",
                    }
                }));

                data["IS_FINALIZED"] = "N";

                var id = vm.form.DF_SCO_FSTR_RCV_H_ID

                return DyeingDataService.saveDataByUrl(data, '/DfRcvFinFabric/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pDF_SCO_FSTR_RCV_H_ID: res.data.OPDF_SCO_FSTR_RCV_H_ID }, { inherit: false, reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDF_SCO_FSTR_RCV_H_ID) > 0) {
                                vm.form.DF_SCO_FSTR_RCV_H_ID = res.data.OPDF_SCO_FSTR_RCV_H_ID;
                                $state.go($state.current, { pDF_SCO_FSTR_RCV_H_ID: res.data.OPDF_SCO_FSTR_RCV_H_ID }, { inherit: false, reload: true });
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

                var list = _.filter(vm.scProgramList[0].processTypeList, function (x) { return x.DYE_BT_CARD_H_ID === 0 });

                for (var i = 0; i < vm.scProgramFabList.length; i++)
                    for (var j = 0; j < vm.scProgramFabList[i].processTypeList.length; j++)
                        if (vm.scProgramFabList[i].processTypeList[j].PROC_STATUS == true) {
                            vm.scProgramFabList[i].processTypeList[j].DYE_BT_CARD_H_ID = vm.scProgramFabList[i].DYE_BT_CARD_H_ID
                            vm.scProgramFabList[i].processTypeList[j]["DYE_SC_PRG_ISS_ID"] = vm.scProgramFabList[i].DYE_SC_PRG_ISS_ID
                            list.push(vm.scProgramFabList[i].processTypeList[j]);
                        }

                for (var i = 0; i < vm.scProgramCCList.length; i++)
                    for (var j = 0; j < vm.scProgramCCList[i].processTypeList.length; j++)
                        if (vm.scProgramCCList[i].processTypeList[j].PROC_STATUS == true) {
                            vm.scProgramCCList[i].processTypeList[j].DYE_BT_CARD_H_ID = vm.scProgramCCList[i].DYE_BT_CARD_H_ID
                            vm.scProgramCCList[i].processTypeList[j]["DYE_SC_PRG_ISS_ID"] = vm.scProgramCCList[i].DYE_SC_PRG_ISS_ID
                            list.push(vm.scProgramCCList[i].processTypeList[j]);
                        }

                for (var i = 0; i < vm.scProgramTrimList.length; i++)
                    for (var j = 0; j < vm.scProgramTrimList[i].processTypeList.length; j++)
                        if (vm.scProgramTrimList[i].processTypeList[j].PROC_STATUS == true) {
                            vm.scProgramTrimList[i].processTypeList[j].DYE_BT_CARD_H_ID = vm.scProgramTrimList[i].DYE_BT_CARD_H_ID
                            vm.scProgramTrimList[i].processTypeList[j]["DYE_SC_PRG_ISS_ID"] = vm.scProgramTrimList[i].DYE_SC_PRG_ISS_ID
                            list.push(vm.scProgramTrimList[i].processTypeList[j]);
                        }

                data["XML_RCV_PROC"] = DyeingDataService.xmlStringShort(list.map(function (o) {
                    return {
                        DF_SCO_FSTR_RCV_PROC_ID: o.DF_SCO_FSTR_RCV_PROC_ID == null ? 0 : o.DF_SCO_FSTR_RCV_PROC_ID,
                        DF_SCO_FSTR_RCV_H_ID: o.DF_SCO_FSTR_RCV_H_ID,
                        DYE_SC_PRG_ISS_ID: o.DYE_SC_PRG_ISS_ID,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID == 0 ? null : o.DYE_BT_CARD_H_ID,
                        DF_PROC_TYPE_ID: o.DF_PROC_TYPE_ID == null ? 0 : o.DF_PROC_TYPE_ID
                    }
                }));

                data["XML_RCV_D1"] = DyeingDataService.xmlStringShort(vm.scProgramFabList.map(function (o) {
                    return {
                        DF_SCO_FSTR_RCV_D1_ID: o.DF_SCO_FSTR_RCV_D1_ID == null ? 0 : o.DF_SCO_FSTR_RCV_D1_ID,
                        DYE_SC_PRG_ISS_ID: o.DYE_SC_PRG_ISS_ID == null ? 0 : o.DYE_SC_PRG_ISS_ID,
                        SC_BATCH_NO: o.SC_BATCH_NO == null ? 0 : o.SC_BATCH_NO,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                        KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID == null ? 0 : o.KNT_STYL_FAB_ITEM_ID,
                        RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                        RCV_FIN_FAB_QTY: o.RCV_FIN_FAB_QTY == null ? 0 : o.RCV_FIN_FAB_QTY,
                        LOSS_QTY: o.LOSS_QTY == null ? 0 : o.LOSS_QTY,
                        QTY_MOU_ID: 3,
                        NOTE: "",
                    }
                }));

                data["XML_RCV_D2"] = DyeingDataService.xmlStringShort(vm.scProgramCCList.map(function (o) {
                    return {
                        DF_SCO_FSTR_RCV_D2_ID: o.DF_SCO_FSTR_RCV_D2_ID == null ? 0 : o.DF_SCO_FSTR_RCV_D2_ID,
                        DYE_SC_PRG_ISS_ID: o.DYE_SC_PRG_ISS_ID == null ? 0 : o.DYE_SC_PRG_ISS_ID,
                        SC_BATCH_NO: o.SC_BATCH_NO == null ? 0 : o.SC_BATCH_NO,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                        KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID == null ? 0 : o.KNT_CLCF_STYL_ITEM_ID,
                        RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                        LOSS_QTY: o.LOSS_QTY == null ? 0 : o.LOSS_QTY,
                        QTY_MOU_ID: 3,
                        NOTE: "",
                    }
                }));

                data["XML_RCV_D3"] = DyeingDataService.xmlStringShort(vm.scProgramTrimList.map(function (o) {
                    return {
                        DF_SCO_FSTR_RCV_D3_ID: o.DF_SCO_FSTR_RCV_D3_ID == null ? 0 : o.DF_SCO_FSTR_RCV_D3_ID,
                        DYE_SC_PRG_ISS_ID: o.DYE_SC_PRG_ISS_ID == null ? 0 : o.DYE_SC_PRG_ISS_ID,
                        SC_BATCH_NO: o.SC_BATCH_NO == null ? 0 : o.SC_BATCH_NO,
                        DYE_BT_CARD_H_ID: o.DYE_BT_CARD_H_ID,
                        MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID == null ? 0 : o.MC_ORD_TRMS_ITEM_ID,
                        RCV_ROLL_QTY: o.RCV_ROLL_QTY == null ? 0 : o.RCV_ROLL_QTY,
                        RCV_QTY: o.RCV_QTY == null ? 0 : o.RCV_QTY,
                        LOSS_QTY: o.LOSS_QTY == null ? 0 : o.LOSS_QTY,
                        QTY_MOU_ID: 3,
                        NOTE: "",
                    }
                }));

                data["IS_FINALIZED"] = "Y";

                var id = vm.form.DF_SCO_FSTR_RCV_H_ID

                return DyeingDataService.saveDataByUrl(data, '/DfRcvFinFabric/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { pDF_SCO_FSTR_RCV_H_ID: res.data.OPDF_SCO_FSTR_RCV_H_ID }, { inherit: false, reload: true });
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                            if (parseInt(res.data.OPDF_SCO_FSTR_RCV_H_ID) > 0) {
                                vm.form.DF_SCO_FSTR_RCV_H_ID = res.data.OPDF_SCO_FSTR_RCV_H_ID;
                                $state.go($state.current, { pDF_SCO_FSTR_RCV_H_ID: res.data.OPDF_SCO_FSTR_RCV_H_ID }, { inherit: false, reload: true });
                            }
                        }

                    }
                });
            }
        };
    }


})();


