////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntMcNeedleBrokenHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'formData', 'usrMcTypeList', 'KnittingDataService', 'Dialog', 'cur_user_id', KntMcNeedleBrokenHController]);
    function KntMcNeedleBrokenHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, formData, usrMcTypeList, KnittingDataService, Dialog, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log(cur_user_id);
        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        vm.isAddToGrid = 'Y';
        vm.updateGridIndex = null;

        if (usrMcTypeList.length > 0) {
            var mcType = usrMcTypeList[0];
        }
        else {
            var mcType = { KNT_MC_TYPE_ID: 0, MC_TYPE_NAME_EN: null };
        }

        var key = 'KNT_MCN_NDL_BRK_H_ID';
        vm.today = new Date();

        if (formData[key]) {
            formData['KNT_MC_TYPE_ID'] = mcType.KNT_MC_TYPE_ID;
            formData['MC_TYPE_NAME_EN'] = mcType.MC_TYPE_NAME_EN;
        }

        vm.form = formData[key] ? formData : {
            TRAN_REF_DT: vm.today, KNT_MCN_NDL_BRK_H_ID: 0, MCN_NDL_BRK_DTL_XML: '', IS_FINALIZED: 'N', KNT_MC_TYPE_ID: mcType.KNT_MC_TYPE_ID, MC_TYPE_NAME_EN: mcType.MC_TYPE_NAME_EN
        };
        $scope.form = formData[key] ? formData : {
            TRAN_REF_DT: vm.today, KNT_MCN_NDL_BRK_H_ID: 0, MCN_NDL_BRK_DTL_XML: '', IS_FINALIZED: 'N', KNT_MC_TYPE_ID: mcType.KNT_MC_TYPE_ID, MC_TYPE_NAME_EN: mcType.MC_TYPE_NAME_EN
        };

        if ($stateParams.pIS_FINALIZED && $stateParams.pIS_FINALIZED == 'Y') {
            vm.form["IS_REVISE"] = 'Y';
        }
        else {
            vm.form["IS_REVISE"] = 'N';
        }
        //$('#FAB_ROLL_NO').focus();

        //$("input[type=text]").focus(function () { $(this).select(); }).mouseup(
        //   function (e) {
        //       e.preventDefault();
        //   });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getCompanyList(), getOfficeList(), getScheduleList(), getMcTypeByUserList(), getMachineList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }

        vm.TRAN_REF_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.TRAN_REF_DT_LNopened = true;
        };

        $scope.$watch('vm.form', function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                    $scope.form.TRAN_REF_DT = $filter('date')(vm.form.TRAN_REF_DT, vm.dtFormat);
                }
            }
        }, true);

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

                    vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;

                    vm.officeDataSource.read();
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

        function getOfficeList() {
            vm.officeOptions = {
                optionLabel: "-- Select Office --",
                filter: "contains",
                autoBind: true,
                dataTextField: "OFFICE_NAME_EN",
                dataValueField: "HR_OFFICE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };

            return vm.officeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/GetOfficeList?pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID || 0); //'/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=128&pSC_USER_ID=' + cur_user_id;

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getScheduleList() {
            vm.scheduleOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SCHEDULE_NAME_EN",
                dataValueField: "HR_SCHEDULE_H_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    vm.fabOrder.COLOR_NAME_EN = item.COLOR_NAME_EN;
                //}
            };

            return vm.scheduleDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        KnittingDataService.getDataByFullUrl('/api/knit/KntMachinOprAssign/GetKnitScheduleList').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getMcTypeByUserList() {
            vm.mcTypeOption = {
                optionLabel: "-- Select Type --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MC_TYPE_NAME_EN",
                dataValueField: "KNT_MC_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);

                    vm.form.KNT_MC_TYPE_ID = item.KNT_MC_TYPE_ID;
                    $scope.form.KNT_MC_TYPE_ID = item.KNT_MC_TYPE_ID;

                    $scope.machineDataSource.read();
                }
            };

            return vm.mcTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        KnittingDataService.getDataByFullUrl('/api/knit/KnitCommon/KntMcTypeListByUser').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getMachineList() {

            return $scope.machineDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        KnittingDataService.getDataByFullUrl('/api/knit/KnitCommon/GetMachineList?pKNT_MC_TYPE_ID=' + ($scope.form.KNT_MC_TYPE_ID || 0)).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        $scope.mcNdlBrknDtlOptions = {
            height: 350,
            sortable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            navigatable: true,
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
            columns: [
                { field: "KNT_MACHINE_NO", title: "M/C No", type: "string", width: "10%", editable: false },
                { field: "MC_DIA", title: "M/C Dia", type: "string", width: "10%", editable: false },
                { field: "EMP_FULL_NAME_EN", title: "Operator", type: "string", width: "25%", editable: false },
                { field: "ITEM_NAME_EN", title: "Item", type: "string", width: "20%", editable: false },
                //{ field: "STOCK_QTY", title: "Stock Qty", type: "string", width: "10%", editable: false },                
                { field: "BRK_QTY_PCS", title: "Qty (Pcs)", type: "string", width: "10%", editable: false, filterable: false },
                { field: "COST_PRICE", title: "Cost", type: "string", width: "11%", editable: false, filterable: false },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.IS_FINALIZE==\"Y\"' ><i class='fa fa-remove'></i></button>&nbsp;" +
                            "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem)' ng-disabled='dataItem.IS_FINALIZE==\"Y\"' ><i class='fa fa-edit'></i></button>&nbsp;";

                    },
                    width: "14%"
                }
            ]
        };


        $scope.mcNdlBrknDtlDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    var url = '/api/knit/McNeedleBroken/GetNeedleBrokenDtlByID?pKNT_MCN_NDL_BRK_H_ID=' + ($stateParams.pKNT_MCN_NDL_BRK_H_ID || vm.form.pKNT_MCN_NDL_BRK_H_ID || 0);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });



        vm.submitData = function (token, pIS_FINALIZED) {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to save?';

            if (pIS_FINALIZED == 'Y') {
                vMsg = 'Do you want to save and finalize?';
            }

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                submitRcvData['IS_FINALIZED'] = pIS_FINALIZED;
                submitRcvData['TRAN_REF_DT'] = $filter('date')(vm.form.TRAN_REF_DT, vm.dtFormat);

                var rcvData = $scope.mcNdlBrknDtlDataSource.data();
                console.log(rcvData);


                submitRcvData.MCN_NDL_BRK_DTL_XML = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {
                    return ob;
                }));

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + token },
                    url: '/api/Knit/McNeedleBroken/BatchSave',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.form.KNT_MCN_NDL_BRK_H_ID = res['data'].PKNT_MCN_NDL_BRK_H_ID_RTN;
                            vm.form.TRAN_REF_NO = res['data'].PTRAN_REF_NO_RTN;

                            if (pIS_FINALIZED == 'Y') {
                                vm.form.IS_FINALIZED = pIS_FINALIZED;
                            }

                            $stateParams.pKNT_MCN_NDL_BRK_H_ID = res['data'].PKNT_MCN_NDL_BRK_H_ID_RTN;
                            $state.go('KntMcNeedleBroken.Dtl', { pKNT_MCN_NDL_BRK_H_ID: res.data.PKNT_MCN_NDL_BRK_H_ID_RTN }, { notify: false });

                            $scope.mcNdlBrknDtlDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };


        vm.reviseData = function (token, pIS_FINALIZED) {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to save?';

            if (pIS_FINALIZED == 'Y') {
                vMsg = 'Do you want to save and finalize?';
            }

            if ($stateParams.pIS_FINALIZED && $stateParams.pIS_FINALIZED == 'Y') {
                alert("");
                return;
            }

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                submitRcvData['IS_FINALIZED'] = pIS_FINALIZED;
                submitRcvData['TRAN_REF_DT'] = $filter('date')(vm.form.TRAN_REF_DT, vm.dtFormat);

                var rcvData = $scope.mcNdlBrknDtlDataSource.data();
                console.log(rcvData);


                submitRcvData.MCN_NDL_BRK_DTL_XML = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {
                    return ob;
                }));

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + token },
                    url: '/api/Knit/McNeedleBroken/BatchSave',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.form.KNT_MCN_NDL_BRK_H_ID = res['data'].PKNT_MCN_NDL_BRK_H_ID_RTN;
                            vm.form.TRAN_REF_NO = res['data'].PTRAN_REF_NO_RTN;

                            if (pIS_FINALIZED == 'Y') {
                                vm.form.IS_FINALIZED = pIS_FINALIZED;
                            }

                            $stateParams.pKNT_MCN_NDL_BRK_H_ID = res['data'].PKNT_MCN_NDL_BRK_H_ID_RTN;
                            $state.go('KntMcNeedleBroken.Dtl', { pKNT_MCN_NDL_BRK_H_ID: res.data.PKNT_MCN_NDL_BRK_H_ID_RTN }, { notify: false });

                            $scope.mcNdlBrknDtlDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.backToList = function () {
            return $state.go('KntMcNeedleBrokenList');
        };

        vm.cancel = function () {
            return $state.go('KntMcNeedleBroken.Dtl', { pKNT_MCN_NDL_BRK_H_ID: 0 });
        };

        vm.printChallan = function () {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3515';

            if (vm.form.SCM_STORE_ID == null || vm.form.SCM_STORE_ID == '') {
                vm.form.SCM_STORE_ID = -1;
            }

            var url;
            if (vm.isRDLC == true) {
                url = '/api/Knit/KntReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Knit/KntReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

            var params = angular.copy(vm.form);

            console.log(params);

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

        vm.submitRequsition = function () {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZE == 'Y') {
                vMsg = 'Do you want to save and finalize?';
            }

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                submitRcvData['IS_FINALIZE'] = pIS_FINALIZE;
                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + $scope.token },
                    url: '/api/Knit/Req4SubStr/SubmitRequisition',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            PARENT_ID = res['data'].PRF_ACTN_STATUS_ID_RTN;

                            vm.actionDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.verifyRequsition = function () {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to verify?';

            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + $scope.token },
                    url: '/api/Knit/Req4SubStr/VerifyRequisition',
                    method: 'post',
                    data: submitRcvData
                })
                .then(function (res) {
                    $scope.errors = undefined;
                    if (res.data.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.data.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.data.jsonStr);

                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.mcOilReq4SubStrListDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.issueRequsition = function () {
            return $state.go('KntMcOilReqIss4SubStr', { pSCM_STR_OIL_REQ_H_ID: vm.form.SCM_STR_OIL_REQ_H_ID });
        };

    }

})();
////////// End Header Controller








////////// Start Detail Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntMcNeedleBrokenDController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', KntMcNeedleBrokenDController]);
    function KntMcNeedleBrokenDController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log(KnittingDataService);
        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        vm.isAddToGrid = 'Y';
        vm.updateGridIndex = null;


        vm.today = new Date();

        vm.formItem = { KNT_MCN_NDL_BRK_D_ID: 0, KNT_MACHINE_ID: null, BRK_QTY_PCS: null, COST_PRICE: null };

        //$('#FAB_ROLL_NO').focus();

        //$("input[type=text]").focus(function () { $(this).select(); }).mouseup(
        //   function (e) {
        //       e.preventDefault();
        //   });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getMachineList(), getOperatorList(), getItemList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }




        function getMachineList() {
            vm.machineOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "KNT_MACHINE_NO",
                dataValueField: "KNT_MACHINE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.formItem.KNT_MACHINE_ID = item.KNT_MACHINE_ID;
                    vm.formItem.KNT_MACHINE_NO = item.KNT_MACHINE_NO;
                    vm.formItem.MC_DIA = item.MC_DIA;

                    vm.itemDataSource.read();
                }
            };

            //return vm.machineDataSource = new kendo.data.DataSource({
            //    transport: {
            //        read: function (e) {

            //            KnittingDataService.getDataByFullUrl('/api/knit/KnitCommon/GetMachineList?pKNT_MC_TYPE_ID=' + ($scope.form.KNT_MC_TYPE_ID || 0)).then(function (res) {
            //                e.success(res);

            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    }
            //});

        };


        function getOperatorList() {
            vm.mcOperatorOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "EMP_FULL_NAME_EN",
                dataValueField: "HR_EMPLOYEE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.formItem.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
                }
            };

            return vm.mcOperatorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        KnittingDataService.getDataByFullUrl('/api/hr/GetEmployeeList?pLK_JOB_STATUS_ID=30&pHR_DESIGNATION_GRP_ID=21&pCORE_DEPT_ID=46').then(function (res) {
                            var listData = _.map(res, function (ob) {
                                ob['EMP_FULL_NAME_EN'] = ob.EMPLOYEE_CODE + ': ' + ob.EMP_FULL_NAME_EN;
                                return ob;
                            });

                            e.success(listData);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function getItemList() {
            vm.itemOptions = {
                optionLabel: "-- Select Item --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "INV_ITEM_ID",
                //dataBound: function (e) {
                //    var ds = e.sender.dataSource.data();
                //    if (ds.length == 1) {
                //        e.sender.value(ds[0].INV_ITEM_ID);
                //        vm.formItem.INV_ITEM_ID = ds[0].INV_ITEM_ID;
                //        vm.formItem.ITEM_NAME_EN = ds[0].ITEM_NAME_EN;
                //        vm.formItem.PACK_MOU_ID = ds[0].PACK_MOU_ID;                        
                //    }                    
                //},
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.formItem.ITEM_NAME_EN = item.ITEM_NAME_EN;
                    vm.formItem.COST_PRICE = item.COST_PRICE;
                    //vm.formItem.PACK_MOU_ID = item.PACK_MOU_ID;
                    //vm.formItem.QTY_PER_PACK = item.PACK_RATIO;

                    console.log(item);

                    var url = '/api/knit/McNeedleBroken/GetItemStockByID?pHR_OFFICE_ID=' + ($scope.$parent.form.HR_OFFICE_ID || 0) + '&pINV_ITEM_ID=' + (item.INV_ITEM_ID || 0);
                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        vm.formItem.STOCK_QTY = res['STOCK_QTY'];
                    }, function (err) {
                        console.log(err);
                    });
                }
            };

            return vm.itemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/dye/LabdipRecipe/GetItemListByCatID?pINV_ITEM_CAT_ID=128';
                        //var url = '/api/knit/McNeedleBroken/GetAssignNdleItemListByMc?pKNT_MACHINE_ID=' + (vm.formItem.KNT_MACHINE_ID || 0);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        vm.addToGrid = function () {
            $scope.$parent.mcNdlBrknDtlDataSource.insert(0, vm.formItem);
            vm.resetRow();
            //vm.formItem.SCM_STR_TR_REQ_D_ID = 0;
            //vm.formItem.CENTRAL_STR_STOCK = 0;
            //vm.formItem.SUB_STR_STOCK = 0;
            //vm.formItem.PACK_RQD_QTY = 0;
            //vm.formItem.QTY_PER_PACK = 0;
            //vm.formItem.RQD_QTY = 0;

            //vm.itemDataSource.read();
        }

        function findGridIndex(index, pKNT_MCN_NDL_BRK_D_ID) {
            if (index > -1) {
                return index;
            } else {

                return _.findIndex($scope.$parent.mcNdlBrknDtlDataSource, function (obj) {
                    return parseInt(obj.KNT_MCN_NDL_BRK_D_ID) == parseInt(pKNT_MCN_NDL_BRK_D_ID);
                });
            }

        };
        vm.editRow = function (dataItem) {
            vm.isAddToGrid = 'N';
            vm.updateGridIndex = findGridIndex($scope.$parent.mcNdlBrknDtlDataSource.indexOf(dataItem), dataItem.KNT_MCN_NDL_BRK_D_ID);

            vm.formItem = angular.copy(dataItem);
        };

        vm.updateRow = function (data) {
            console.log(data.uid);
            //console.log($scope.$parent.fabReqDtl);
            //console.log(dataItem);

            var dataItem = $scope.$parent.mcNdlBrknDtlDataSource.getByUid(data.uid);

            //console.log(data.uid);
            console.log(dataItem);

            dataItem.set('INV_ITEM_ID', data.INV_ITEM_ID);
            dataItem.set('ITEM_NAME_EN', data.ITEM_NAME_EN);
            dataItem.set('KNT_MACHINE_ID', data.KNT_MACHINE_ID);
            dataItem.set('KNT_MACHINE_NO', data.KNT_MACHINE_NO);
            dataItem.set('MC_DIA', data.MC_DIA);
            dataItem.set('OPERATOR_ID', data.OPERATOR_ID);
            dataItem.set('EMP_FULL_NAME_EN', data.EMP_FULL_NAME_EN);

            dataItem.set('STOCK_QTY', data.STOCK_QTY);
            dataItem.set('BRK_QTY_PCS', data.BRK_QTY_PCS);
            dataItem.set('COST_PRICE', data.COST_PRICE);

            vm.resetRow();
        };

        vm.resetRow = function () {
            var data = angular.copy(vm.formItem);
            vm.formItem = {
                KNT_MCN_NDL_BRK_D_ID: 0, BRK_QTY_PCS: null, KNT_MACHINE_ID: data.KNT_MACHINE_ID, KNT_MACHINE_NO: data.KNT_MACHINE_NO, MC_DIA: data.MC_DIA,
                OPERATOR_ID: data.OPERATOR_ID, EMP_FULL_NAME_EN: data.EMP_FULL_NAME_EN, INV_ITEM_ID: data.INV_ITEM_ID, ITEM_NAME_EN: data.ITEM_NAME_EN, COST_PRICE: data.COST_PRICE
            };

        }

        vm.removeRow = function (dataItem) {
            $scope.$parent.mcNdlBrknDtlDataSource.remove(dataItem);
            //var dataList = vm.rollRcvGridDataSource.data();

        };
    }

})();
////////// End Detail Controller








////////// Start List Controller
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('KntMcNeedleBrokenListController', ['logger', 'config', '$q', '$scope', '$http', 'Hub', '$rootScope', 'exception', '$filter', '$state', '$stateParams', '$location', 'KnittingDataService', 'cur_user_id', KntMcNeedleBrokenListController]);
    function KntMcNeedleBrokenListController(logger, config, $q, $scope, $http, Hub, $rootScope, exception, $filter, $state, $stateParams, $location, KnittingDataService, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;


        //console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        //$('#FAB_ROLL_NO').focus();

        //$("input[type=text]").focus(function () { $(this).select(); }).mouseup(
        //   function (e) {
        //       e.preventDefault();
        //   });

        vm.mcNdlBrknListOptions = {
            height: 400,
            sortable: true,
            pageable: true,
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
            columns: [
                { field: "TRAN_REF_NO", title: "Reference#", type: "string", width: "15%" },
                { field: "TRAN_REF_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "MC_TYPE_NAME_EN", title: "M/C Type", width: "10%" },
                { field: "COMP_NAME_EN", title: "Company", width: "10%" },
                { field: "OFFICE_NAME_EN", title: "Office", width: "10%" },
                { field: "SCHEDULE_NAME_EN", title: "Shift", width: "15%" },
                { field: "BRK_QTY_PCS", title: "Qty (pcs)", width: "10%" },
                { field: "COST_PRICE", title: "Cost Price", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return '<a class="btn btn-xs blue" ng-click="vm.editRequsition(dataItem)" ><i class="fa fa-edit"></i></a>';
                            //' <a class="btn btn-xs yellow-gold" ui-sref="KntMcNeedleBroken.Dtl({pKNT_MCN_NDL_BRK_H_ID:dataItem.KNT_MCN_NDL_BRK_H_ID,pIS_FINALIZED:dataItem.IS_FINALIZED})" ng-if="dataItem.IS_FINALIZED==' + "'Y'" + '" ><i class="fa fa-edit"></i>Revise</a>';
                    },
                    width: "10%"
                }
            ]
        };

        vm.mcNdlBrknListDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_MCN_NDL_BRK_H_ID",
                    fields: {
                        TRAN_REF_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {

                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = '/api/knit/McNeedleBroken/GetNeedleBrokenList?';
                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    console.log(url);

                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });


        activate();
        function activate() {
            var promise = [getMcNdlBrknList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }



        function getMcNdlBrknList() {
            vm.mcNdlBrknListDataSource.read();
        }



        vm.addNew = function () {
            return $state.go('KntMcNeedleBroken.Dtl', { pKNT_MCN_NDL_BRK_H_ID: 0 });
        };

        vm.editRequsition = function (dataItem) {
            return $state.go('KntMcNeedleBroken.Dtl', { pKNT_MCN_NDL_BRK_H_ID: dataItem.KNT_MCN_NDL_BRK_H_ID });
        };



        vm.printChallan = function () {
            //console.log(dataItem);

            vm.isRDLC = true;
            vm.form.REPORT_CODE = 'RPT-3515';

            if (vm.form.SCM_STORE_ID == null || vm.form.SCM_STORE_ID == '') {
                vm.form.SCM_STORE_ID = -1;
            }

            var url;
            if (vm.isRDLC == true) {
                url = '/api/Knit/KntReport/PreviewReportRDLC';
            }
            else {
                url = '/api/Knit/KntReport/PreviewReport';
            }

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            vm.form.FROM_DATE = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

            var params = angular.copy(vm.form);

            console.log(params);

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
////////// End List Controller
