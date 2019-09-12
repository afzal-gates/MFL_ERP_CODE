(function () {
    'use strict';
    angular.module('multitex.knitting').controller('McnNeedleIssueController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'formData', 'cur_user_id', '$filter', McnNeedleIssueController]);
    function McnNeedleIssueController($q, config, KnittingDataService, $stateParams, $state, $scope, commonDataService, formData, cur_user_id, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        console.log($stateParams);
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        //vm.form = {};

        //vm.form = formData.KNT_YRN_ISS_H_ID ? formData : { KNT_YRN_STR_REQ_H_ID: '0', INV_ITEM_CAT_ID: '2', HR_COMPANY_ID: 1, SCM_STORE_ID: 1, ITEM_ISS_BY: cur_user_id, IS_FINALIZED: 'N', ISS_CHALAN_DT: vm.today };

        vm.form = formData.SCM_STR_NDL_REQ_H_ID || formData.SCM_STR_NDL_ISS_H_ID ? formData : {
            STR_ISS_DT: vm.today, SCM_STR_NDL_REQ_H_ID: 0, RF_ACTN_STATUS_ID: 0, HR_COMPANY_ID: 1, RF_REQ_TYPE_ID: 22, HR_DEPARTMENT_ID: 46, ITEM_ISS_BY: cur_user_id,
            ISS_STR_ID: 0, RCV_STR_ID: 0, STR_REQ_BY_NAME: cur_user_id
        };

        //vm.form["ISS_STR_ID"] = 0;
        //vm.form["RCV_STR_ID"] = vm.form.FRM_REQ_STR_ID;
        vm.formItem = {};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getCategoryList(), getReqTypeList(), getCompanyList(), getDepartmentList(), getStoreList(), getUserList(), getItemList(), getMouList(),
                getStatusList(), getOperatorList(), getMachineList(), GetReasonList(), RequisitionDtlList(), getUserData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.copyItem = function (item) {

            if (item.RQD_QTY_PCS < item.CENTRAL_STR_STOCK)
                item.ISS_QTY_PCS = angular.copy(item.RQD_QTY_PCS);
        }

        vm.copyAll = function () {

            for (var i = 0; i < vm.RequisitionDtlList.length; i++) {
                var item = vm.RequisitionDtlList[i];
                if (item.RQD_QTY_PCS < item.CENTRAL_STR_STOCK)
                    item.ISS_QTY_PCS = angular.copy(item.RQD_QTY_PCS);
            }
        }

        $scope.STR_ISS_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_ISS_DT_LNopened = true;
        }

        $scope.STR_REQ_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.STR_REQ_DT_LNopened = true;
        }

        vm.checkStock = function (item) {
            if (item.RQD_QTY_PCS < item.ISS_QTY_PCS || parseInt(item.CENTRAL_STR_STOCK) < item.ISS_QTY_PCS) {
                item.ISS_QTY_PCS = '';
                config.appToastMsg("MULTI-005 Issue Qty Invalid!");
                return;
            }
        }

        function GetReasonList() {

            return vm.reasonList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/McnNeedleReq/GetNeedlReqReason').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

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

        function getCategoryList() {
            vm.categoryOptions = {
                optionLabel: "-- Select Category --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID",
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        e.sender.value(ds[0].INV_ITEM_CAT_ID);
                        vm.form.INV_ITEM_CAT_ID = ds[0].INV_ITEM_CAT_ID;
                        vm.itemDataSource.read();
                    }
                    //else if (ds.length > 0 && $stateParams.pLK_FLOOR_ID > 0) {
                    //    e.sender.value($stateParams.pLK_FLOOR_ID);
                    //    vm.form.LK_FLOOR_ID = $stateParams.pLK_FLOOR_ID;
                    //}

                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                }
            };

            return vm.categoryDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/inv/ItemCategory/SelectAll';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            var list = _.filter(res, function (o) { return (o.INV_ITEM_CAT_ID === 128 || o.INV_ITEM_CAT_ID === 211) });
                            e.success(list);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getReqTypeList() {
            vm.reqTypeOptions = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.reqTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetReqType';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            var list = _.filter(res, function (o) { return (o.RF_REQ_TYPE_ID === 22) });
                            e.success(list);
                            //e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getCompanyList() {
            vm.companyOptions = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.companyDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/CompanyList';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getDepartmentList() {
            vm.departmentOptions = {
                optionLabel: "-- Select Department --",
                filter: "contains",
                autoBind: true,
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.departmentDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/Hr/Admin/HrDesignation/DepartmentListData';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getStoreList() {
            vm.storeOptions = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.storeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=12&pSC_USER_ID=' + cur_user_id;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            //var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 15 || x.SCM_STORE_ID == 16 || x.SCM_STORE_ID == 17 || x.SCM_STORE_ID == 21 });
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        //vm.userTemplate = '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>';
        //vm.userValueTemplate = '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span> <span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>';
        function getUserList() {
            vm.userOptions = {
                optionLabel: "-- Select User --",
                filter: "contains",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template: '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                          '<span class="k-state-default"><h4>#: data.USER_NAME_EN #</h4><p>#: data.USER_EMAIL #</p></span>',
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.userDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/SelectAllUserData';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
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
                            KnittingDataService.getUserDatalist().then(function (res) {
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


        function getItemList() {
            vm.itemOptions = {
                optionLabel: "-- Select Item --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;
                    vm.formItem.PACK_MOU_ID = item.PACK_MOU_ID;
                    console.log(item);

                    var data = _.filter(vm.packMouDataSource.data(), function (ob) {
                        return ob.RF_MOU_ID == item.PACK_MOU_ID;
                    });
                    if (data.length > 0)
                        vm.formItem.PACK_MOU_CODE = data[0].MOU_CODE;
                    console.log(data);
                }
            };

            return vm.itemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST=128,211';

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getMouList() {
            vm.packMouOptions = {
                optionLabel: "--Pack UoM--",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"//,              
            };

            vm.qtyMouOptions = {
                optionLabel: "-- UoM --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.formItem.QTY_MOU_CODE = item.MOU_CODE;
                }
            };


            return KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {

                vm.packMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

                vm.qtyMouDataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(res);
                        }
                    }
                });

            }, function (err) {
                console.log(err);
            });


        };

        var RF_ACTN_TYPE_ID = 23;
        var PARENT_ID = 0;
        if (vm.form.RF_ACTN_STATUS_ID > 0) {
            PARENT_ID = vm.form.RF_ACTN_STATUS_ID;
        }
        function getStatusList() {
            vm.actionOptions = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ACTN_STATUS_NAME",
                dataValueField: "RF_ACTN_STATUS_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                },
                dataBound: function (e) {
                    var ds = e.sender.dataSource.data();
                    if (ds.length == 1) {
                        e.sender.value(ds[0].RF_ACTN_STATUS_ID);
                        vm.form.RF_ACTN_STATUS_ID_NEXT = ds[0].RF_ACTN_STATUS_ID;
                    }
                }
            };

            return vm.actionDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/RfActionStatusByID/' + RF_ACTN_TYPE_ID + '/' + PARENT_ID + '/' + cur_user_id;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };


        vm.TotalReqQty = function () {
            if ((vm.formItem.PACK_RQD_QTY || 0) > 0) {
                var total = parseFloat(vm.formItem.PACK_RQD_QTY) * parseFloat(vm.formItem.QTY_PER_PACK);
                var stock = parseFloat(vm.formItem.CENTRAL_STK);
                if (total > stock) {

                    vm.formItem.PACK_RQD_QTY = '';
                    vm.formItem.RQD_QTY = 0;
                }
                else {
                    vm.formItem.RQD_QTY = total;
                }
            }
            else
                vm.formItem.PACK_RQD_QTY = 0;
        };


        function RequisitionDtlList() {
            var url = '';
            if ($stateParams.pSCM_STR_NDL_REQ_H_ID && $stateParams.pSCM_STR_NDL_REQ_H_ID > 0) {
                url = '/api/knit/McnNeedleReq/GetNeedlReqDtl?pSCM_STR_NDL_REQ_H_ID=' + ($stateParams.pSCM_STR_NDL_REQ_H_ID || vm.form.SCM_STR_NDL_REQ_H_ID || 0) + '&pOption=3002';
            }
            else if ($stateParams.pSCM_STR_NDL_ISS_H_ID) {
                url = '/api/knit/McnNeedleReq/GetNeedleIssueDtl?pSCM_STR_NDL_ISS_H_ID=' + ($stateParams.pSCM_STR_NDL_ISS_H_ID || 0);
            }
            KnittingDataService.getDataByFullUrl(url).then(function (res) {
                vm.RequisitionDtlList = res;
            });
        }

        vm.backAll = function (dataOri) {

            //if (fnValidate() == true) {

            var data = angular.copy(dataOri);

            return KnittingDataService.saveDataByUrl(data, '/McnNeedleReq/Back2Req').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);
                    console.log(res);

                    config.appToastMsg(res.data.PMSG);
                    if (parseInt(res.data.OPSCM_STR_NDL_REQ_H_ID) > 0) {
                        vm.form.SCM_STR_NDL_ISS_H_ID = res.data.OPSCM_STR_NDL_ISS_H_ID;
                        $state.go($state.current, { pSCM_STR_NDL_REQ_H_ID: res.data.OPSCM_STR_NDL_REQ_H_ID, pSCM_STR_NDL_ISS_H_ID: 0 }, { reload: true });
                    }

                }
            });
            //}
        };


        vm.submitAll = function (dataOri, pIS_FINALIZED) {

            //if (fnValidate() == true) {

            var data = angular.copy(dataOri);

            var list = _.filter(vm.RequisitionDtlList, function (x) { return x.ISS_QTY_PCS > 0 })
            //var i = 0;
            data["XML_ISS_D"] = KnittingDataService.xmlStringShort(list.map(function (o) {
                return {
                    SCM_STR_NDL_ISS_D_ID: o.SCM_STR_NDL_ISS_D_ID == null ? 0 : o.SCM_STR_NDL_ISS_D_ID,
                    KNT_MACHINE_ID: o.KNT_MACHINE_ID,
                    INV_ITEM_ID: o.INV_ITEM_ID,
                    //SL_NO: ++i,
                    OPERATOR_ID: o.OPERATOR_ID == 0 ? null : o.OPERATOR_ID,
                    ISS_QTY_PCS: o.ISS_QTY_PCS == null ? 0 : o.ISS_QTY_PCS,
                    UNIT_PRICE: o.UNIT_PRICE == null ? 0 : o.UNIT_PRICE,
                    COST_PRICE: o.COST_PRICE == null ? 0 : o.COST_PRICE,
                }
            }));

            var _invdate = $filter('date')(data.STR_ISS_DT, 'yyyy-MM-ddTHH:mm:ss');
            data["STR_ISS_DT"] = _invdate;
            data["IS_FINALIZED"] = pIS_FINALIZED;
            data["RF_ACTN_STATUS_ID"] = vm.form.RF_ACTN_STATUS_ID_NEXT;

            var id = vm.form.SCM_STR_NDL_ISS_H_ID

            return KnittingDataService.saveDataByUrl(data, '/McnNeedleReq/Issue').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);
                    console.log(res);
                    if (id > 0) {
                        config.appToastMsg(res.data.PMSG);
                        $state.go($state.current, { pSCM_STR_NDL_REQ_H_ID: 0, pSCM_STR_NDL_ISS_H_ID: id }, { reload: true });
                    }
                    else {

                        config.appToastMsg(res.data.PMSG);
                        if (parseInt(res.data.OPSCM_STR_NDL_ISS_H_ID) > 0) {
                            vm.form.SCM_STR_NDL_ISS_H_ID = res.data.OPSCM_STR_NDL_ISS_H_ID;
                            $state.go($state.current, { pSCM_STR_NDL_REQ_H_ID: 0, pSCM_STR_NDL_ISS_H_ID: res.data.OPSCM_STR_NDL_ISS_H_ID }, { reload: true });
                        }
                    }

                }
            });
            //}
        };

    }


})();

