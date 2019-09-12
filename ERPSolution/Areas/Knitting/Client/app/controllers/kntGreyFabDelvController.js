////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntGreyFabDelvHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'formData', 'woRefList', 'KnittingDataService', 'Dialog', 'cur_user_id', KntGreyFabDelvHController]);
    function KntGreyFabDelvHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, formData, woRefList, KnittingDataService, Dialog, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'KNT_SC_GFAB_DLV_H_ID';
        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.today = new Date();
        vm.printButtonList = [{ BTN_ID: 1, BTN_NAME: 'Challan' }, { BTN_ID: 2, BTN_NAME: 'Party Ledger' }];

        vm.form = formData[key] ? formData : {
            KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', YRN_RTN_DTL_XML: '',
            yrnRtnDtl: []
        };
        $scope.form = formData[key] ? formData : {
            KNT_SC_GFAB_DLV_H_ID: 0, SCM_SUPPLIER_ID: 0, CHALAN_NO: null, CHALAN_DT: vm.today, GT_PASS_NO: '', GFAB_DLV_DTL_XML: '', YRN_RTN_DTL_XML: '',
            yrnRtnDtl: []
        };
        $scope.yrnItem = { KNT_SCI_YRN_RET_D_ID: 0, KNT_SC_GFAB_DLV_H_ID: 0, PACK_MOU_ID: 29, PACK_MOU_CODE: 'Bag', QTY_MOU_ID: 3, QTY_MOU_CODE: 'Kg', SP_NOTE: '' };

        
        vm.fabDelvState = ($state.current.name === 'KntScGreyFabDelvH.FabDelv') ? true : false;
        vm.yrnRtnState = ($state.current.name === 'KntScGreyFabDelvH.YrnRtn') ? true : false;

        vm.errstyle = { 'border': '1px solid #f13d3d' };




        activate();
        function activate() {
            var promise = [getSupplierList(), getScByrOrdList(), getStoreList()];
            return $q.all(promise).then(function () {               
                vm.showSplash = false;                
            });
        }

        vm.chlnDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.chlnDateOpened = true;
        };

        function getStoreList() {
            vm.storeOption = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                //dataSource: {
                //    transport: {
                //        read: function (e) {
                //            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                //                //KnittingDataService.LookupListData(92).then(function (res) {
                //                e.success(res);
                //            });
                //        }
                //    }
                //},
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);                    
                //},
                //dataBound: function (e) {
                //    if (formData[key]) {
                //        vm.form.SCM_STORE_ID = formData.SCM_STORE_ID;
                //    }
                //}
            };
            ///api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5&pSC_USER_ID=' + cur_user_id
            return vm.storeDataSource = new kendo.data.DataSource({               
                transport: {
                    read: function (e) {
                        
                        var url = '/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5&pSC_USER_ID=' + cur_user_id;
                        
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };

        function getSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    //vm.form.KNT_SC_PRG_RCV_ID = null;

                    $scope.scByrOrdListDataSource.read();
                }
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getScByrOrdList() {
            $scope.scByrOrdListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SC_WO_REF_NO",
                dataValueField: "SCM_SC_WO_REF_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    $scope.yrnItem.SC_WO_REF_NO = item.SC_WO_REF_NO;

                    return $scope.yarnItemDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                KnittingDataService.getDataByFullUrl('/api/knit/KntScRcv/GetYrnByScOrdRefID?&pSCM_SC_WO_REF_ID=' + item.SCM_SC_WO_REF_ID).then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    });
                }
            };

            return $scope.scByrOrdListDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScRcv/GetScOrdRefByPartyID?&pageNumber=1&pageSize=10&pSCM_SUPPLIER_ID=' + (($scope.form.SCM_SUPPLIER_ID == null) ? 0 : $scope.form.SCM_SUPPLIER_ID);

                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);
                        console.log(url);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            console.log(res.data);
                            //e.success([]);
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        

        $scope.$watch('vm.form', function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.form['SC_FAB_DELV_CHALAN_NO'] = vm.form.CHALAN_NO;
                    $scope.form = vm.form;
                    $scope.form.CHALAN_DT = $filter('date')(vm.form.CHALAN_DT, vm.dtFormat);
                }
            }
        }, true);

        //$scope.$watchGroup(['vm.form'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            vm.IS_NEXT = 'N';

        //            //vm.next();
        //            vm.rollDelvGridDataSource.read();
        //        }
        //    }
        //}, true);

        $scope.rollDelvGridOption = {
            height: 300,
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
                { field: "FAB_PROD_CAT_SNAME", title: "Prod.Type", type: "string", width: "7%", editable: false },
                { field: "BUYER_SNAME", title: "Buyer/Party", type: "string", width: "10%", editable: false },
                { field: "STYLE_NO", title: "Style#", type: "string", width: "10%", editable: false },
                { field: "ORDER_NO_LST", title: "Order#", type: "string", width: "20%", editable: false },
                //{ field: "FAB_DETAIL", title: "Fabric Detail", type: "string", width: "30%", editable: false },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "7%", editable: false },
                { field: "FAB_ROLL_NO", title: "Roll#", type: "string", width: "10%", editable: false },
                { field: "FAB_TYPE_SNAME", title: "Fab.Type", type: "string", width: "8%", editable: false, filterable: false },
                { field: "ACT_FIN_DIA", title: "F.Dia", type: "string", width: "5%", editable: false, filterable: false },
                { field: "MC_DIA", title: "Mc.Dia", type: "string", width: "5%", editable: false, hidden: true },
                { field: "ACT_FIN_GSM", title: "F.Gsm", type: "string", width: "5%", editable: false, hidden: true },
                { field: "MC_GG", title: "Mc GG", type: "string", width: "5%", editable: false, filterable: false, footerTemplate: "Total:" },
                //{ field: "KNT_FAB_ROLL_ID", title: "Roll ID", type: "string", width: "5%", editable: false },
                { field: "DELV_NO_ROLL", title: "R.Qty", type: "number", width: "5%", editable: false, filterable: false, footerTemplate: "#=sum#" },
                { field: "DELV_ROLL_WT", title: "Roll Wt", type: "number", width: "5%", editable: false, filterable: false, footerTemplate: "#=sum#" },
                { field: "DELV_STATUS", title: "Status", type: "string", width: "7%", editable: false, filterable: false },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.IS_FINALIZE==\"Y\"' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "3%"
                }
            ],
            change: function (e) {
                var grid = $("#delvGrid").data("kendoGrid");
                //grid.select("tr:eq(1)");
                var row = grid.select();
                $scope.selectedItem = grid.dataItem(row);
                $scope.$apply();
            },
            dataBound: function () {
                var grid = this;
                $.each(grid.tbody.find('tr'), function (idx) {

                    var model = grid.dataItem(this);

                    if (idx == 0) {
                        $scope.selectedItem = model;
                        $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                    }
                });
            }
        };

        

        $scope.rollDelvGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/Knit/KntGreyFabDelv/GetScFabDelvDtlByDelvHid';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pKNT_SC_GFAB_DLV_H_ID=' + $stateParams.pKNT_SC_GFAB_DLV_H_ID || 0;

                    console.log(url);

                    //url += config.kFilterStr2QueryParam(params.filter);
                    //if (params.filter.length == 0) {
                    //    remQueryParam();
                    //}
                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            aggregate: [
                { field: "DELV_NO_ROLL", aggregate: "sum" },
                { field: "DELV_ROLL_WT", aggregate: "sum" }
            ],
            pageSize: 2000,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_SC_GFAB_DLV_D_ID",
                    fields: {
                        RCV_DT: { type: "date", editable: false },
                        ORDER_NO_LST: { type: "string", filterable: false },
                        DELV_NO_ROLL: { type: "number", filterable: false },
                        DELV_ROLL_WT: { type: "number", filterable: false }
                    }
                }
            }
        });


        $scope.yrnRtnGridOption = {
            height: 300,
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
                { field: "RETURN_TYPE_DESC", title: "Return Type", type: "string", width: "10%", editable: false },                                
                { field: "SC_WO_REF_NO", title: "Work Order Ref#", type: "string", width: "20%", editable: false },
                { field: "YR_SPEC_DESC", title: "Yarn", type: "string", width: "20%", editable: false },
                { field: "PACK_RET_QTY", title: "Pack Qty", type: "string", width: "10%", editable: false },
                { field: "PACK_MOU_CODE", title: "Pack Unit", type: "string", width: "10%", editable: false },
                { field: "QTY_PER_PACK", title: "Qty/Pack", type: "string", width: "10%", editable: false, filterable: false },
                { field: "RET_QTY", title: "Rtn.Qty", type: "number", width: "10%", editable: false, filterable: false },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeYarnRcv(dataItem)' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "10%"
                }
            ]
        };


        $scope.yrnRtnGridDataSource = new kendo.data.DataSource({            
            transport: {
                read: function (e) {
                    
                    var url = '/api/Knit/KntGreyFabDelv/GetSciYarnRtnList?pKNT_SC_GFAB_DLV_H_ID=' + $stateParams.pKNT_SC_GFAB_DLV_H_ID || $scope.form.KNT_SC_GFAB_DLV_H_ID || 0;
                    console.log(url);
                                   
                    KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                        //e.success([]);
                    });
                }
            },            
            pageSize: 100
        });



        vm.submitData = function (token, pIS_FINALIZE) {
            var submitRcvData = angular.copy($scope.form);
            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZE == 'Y') {
                submitRcvData['IS_FINALIZE'] = 'Y';
                vMsg = 'Do you want to finalize?';
            }


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {

                //submitRcvData['IS_FINALIZE'] = pIS_FINALIZE;
                //submitRcvData['RCV_DT'] = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

                var rcvData = $scope.rollDelvGridDataSource.data();
                var yrnRtnData = $scope.yrnRtnGridDataSource.data();
                //console.log(rcvData);


                submitRcvData.GFAB_DLV_DTL_XML = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {
                    ob.STORE_NAME_EN = '';
                    ob.FIB_COMP_CODE = '';
                    ob.KNT_YRN_LOT_DESC = '';
                    

                    return ob;
                }));
                submitRcvData.YRN_RTN_DTL_XML = KnittingDataService.xmlStringShort(yrnRtnData.map(function (ob) {
                    return {
                        KNT_SCI_YRN_RET_D_ID: ob.KNT_SCI_YRN_RET_D_ID,
                        KNT_SC_GFAB_DLV_H_ID: ob.KNT_SC_GFAB_DLV_H_ID,
                        SCM_SC_WO_REF_ID: ob.SCM_SC_WO_REF_ID,
                        LK_YRN_RET_TYPE_ID: ob.LK_YRN_RET_TYPE_ID,
                        LK_YRN_RET_COZ_ID: ob.LK_YRN_RET_COZ_ID,
                        YARN_ITEM_ID: ob.YARN_ITEM_ID,
                        KNT_YRN_LOT_ID: ob.KNT_YRN_LOT_ID,
                        PACK_RET_QTY: ob.PACK_RET_QTY,
                        PACK_MOU_ID: ob.PACK_MOU_ID,

                        QTY_PER_PACK: ob.QTY_PER_PACK,
                        LOOSE_QTY: ob.LOOSE_QTY,
                        RET_QTY: ob.RET_QTY,

                        QTY_MOU_ID: ob.QTY_MOU_ID,
                        SP_NOTE: ob.SP_NOTE,
                        IS_FINALIZE: ob.IS_FINALIZE
                    };
                }));

                console.log(submitRcvData);
                //return;

                return $http({
                    headers: { 'Authorization': 'Bearer ' + token },
                    url: '/api/Knit/KntGreyFabDelv/BatchSave',
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

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.isSaved = true;

                            $scope.form.KNT_SC_GFAB_DLV_H_ID = res.data.PKNT_SC_GFAB_DLV_H_ID_RTN;
                            $scope.form.CHALAN_NO = res.data.PCHALAN_NO_RTN;
                            $stateParams.pKNT_SC_GFAB_DLV_H_ID = res.data.PKNT_SC_GFAB_DLV_H_ID_RTN;

                            if (pIS_FINALIZE == 'Y') {
                                $scope.form.IS_FINALIZE = 'Y';
                            }

                            if (vm.fabDelvState == true) {
                                $state.go('KntScGreyFabDelvH.FabDelv', { pKNT_SC_GFAB_DLV_H_ID: res.data.PKNT_SC_GFAB_DLV_H_ID_RTN }, { notify: false });
                            }
                            else {
                                $state.go('KntScGreyFabDelvH.YrnRtn', { pKNT_SC_GFAB_DLV_H_ID: res.data.PKNT_SC_GFAB_DLV_H_ID_RTN }, { notify: false });
                            }
                            
                            $scope.rollDelvGridDataSource.read();
                            $scope.yrnRtnGridDataSource.read();
                            
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.cancel = function () {
            vm.errors = undefined;
            $state.go('KntScGreyFabDelvH.FabDelv', { pKNT_SC_GFAB_DLV_H_ID: 0 }, { inherit: false });
        }

        vm.backToList = function () {
            return $state.go('KntScGreyFabDelvList', { pSCM_SUPPLIER_ID: $scope.form.SCM_SUPPLIER_ID });
        };

        vm.print = function (itm) {
            var rptCode;
            if (itm.BTN_ID == 1) {
                rptCode = 'RPT-3508';
            }
            else if (itm.BTN_ID == 2) {
                rptCode = 'RPT-3514';
            }

            var data = angular.copy($scope.form);

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            //var params = angular.copy($scope.form);
            var params = angular.extend({ REPORT_CODE: rptCode }, data);
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
////////// End Header Controller





////////// Start Fabric Delivery Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntGreyFabDelvDController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntGreyFabDelvDController]);
    function KntGreyFabDelvDController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();
                        

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }
        
        $("#FAB_ROLL_NO").keypress(function (e) {
            vm.errors = undefined;
            
            if (e.which == 13) {
                var isExistsRoll = 'N';
                KnittingDataService.getDataByFullUrl('/api/Knit/KntGreyFabDelv/GetRollData4Delv?&pFAB_ROLL_NO=' + vm.form.FAB_ROLL_NO + '&pSCM_SUPPLIER_ID=' + $scope.$parent.form.SCM_SUPPLIER_ID + '&pSCM_STORE_ID=' + $scope.$parent.form.SCM_STORE_ID).then(function (res) {

                    if (res.IS_G_F != 'G' && res.KNT_QC_STS_TYPE_ID != 4) { //4--> Rejected
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = [{ error: 'Sorry! At first receive the roll# at grey store then delivery to party.' }];
                        return;
                    }
                    else if (res.IS_DELV == 'Y') {
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = [{ error: 'Sorry! Already delivered the roll#' }];
                        return;
                    }
                    

                    var rcvGridList = $scope.$parent.rollDelvGridDataSource.data();
                    var rollIdList = [];

                    var rcvList = _.filter(rcvGridList, function (o) {
                        if (o.KNT_FAB_ROLL_ID == res.KNT_FAB_ROLL_ID) {
                            isExistsRoll = 'Y';
                        }

                        return o.KNT_FAB_ROLL_ID == res.KNT_FAB_ROLL_ID;
                    });

                    if (isExistsRoll == 'Y') {
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = [{ error: 'Sorry! Already include the roll# into the challan.' }];
                        return;
                    }


                    if (res['FAB_ROLL_NO']) {
                        var data = {
                            KNT_SC_GFAB_DLV_D_ID: 0, KNT_SC_GFAB_DLV_H_ID: res.KNT_STYL_FAB_ITEM_ID, KNT_QC_STS_TYPE_ID: res.KNT_QC_STS_TYPE_ID,
                            FAB_PROD_CAT_SNAME: res.FAB_PROD_CAT_SNAME, STYLE_NO: res.STYLE_NO,
                            BUYER_SNAME: res.BUYER_SNAME, ORDER_NO_LST: res.ORDER_NO_LST, COLOR_NAME_EN: res.COLOR_NAME_EN, FAB_TYPE_SNAME: res.FAB_TYPE_SNAME,
                            ACT_FIN_GSM: res.ACT_FIN_GSM, ACT_FIN_DIA: res.ACT_FIN_DIA, MC_DIA: res.MC_DIA, MC_GG: res.MC_GG, FAB_ROLL_NO: res.FAB_ROLL_NO,
                            //FAB_DETAIL: res.COLOR_NAME_EN + '/' + res.FAB_TYPE_SNAME + '/' + res.ACT_FIN_DIA + '/' + res.MC_DIA + '/' + res.ACT_FIN_GSM + '/' + res.MC_GG,
                            KNT_FAB_ROLL_ID: res.KNT_FAB_ROLL_ID, DELV_NO_ROLL: 1, DELV_ROLL_WT: res.RCV_ROLL_WT, WT_MOU_ID: res.WT_MOU_ID
                        };

                        $scope.$parent.rollDelvGridDataSource.insert(data, 0);
                        new Audio('../../Content/sounds/OkSound.mp3').play();
                    }
                    else {
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = [{ error: 'Please input valid roll# or this roll# isn`t valid of selected supplier.' }];
                        return;
                    }
                }, function (err) {
                    console.log(err);
                });

                vm.form.FAB_ROLL_NO = '';
                $("#FAB_ROLL_NO").focus();
            };
        });


        

        vm.removeRow = function (dataItem) {
            //alert('Roll Remove')

            Dialog.confirm('Do you want to remove the record?', 'Confirmation', ['Yes', 'No']).then(function () {

                if (dataItem.KNT_SC_GFAB_DLV_D_ID > 0) {
                    return $http({
                        headers: { 'Authorization': 'Bearer ' + $scope.token },
                        url: '/api/Knit/KntGreyFabDelv/RemoveFabDelv',
                        method: 'post',
                        data: dataItem
                    })
                        .then(function (res) {
                            $scope.errors = undefined;
                            if (res.data.success === false) {
                                $scope.errors = [];
                                $scope.errors = res.data.errors;
                            }
                            else {

                                res['data'] = angular.fromJson(res.data.jsonStr);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    $scope.$parent.rollDelvGridDataSource.remove(dataItem);

                                };

                                config.appToastMsg(res.data.PMSG);
                            }
                        }).catch(function (message) {
                            exception.catcher('XHR loading Failded')(message);
                        });
                }
                else {
                    $scope.$parent.rollDelvGridDataSource.remove(dataItem);
                }
            });

            
            //var dataList = $parent.rollRcvGridDataSource.data();

        };

        //vm.next = function () {
        //    $state.go('KntGreyFabRcv', {
        //        pPROD_DT: vm.form.RCV_DT == null ? $stateParams.pRCV_DT : moment(vm.form.RCV_DT).format("YYYY-MMM-DD")
        //    }, { reload: 'KntGreyFabRcv' });
        //    vm.IS_NEXT = 'Y';
        //    //vm.showSplash = 'N';
        //};



        //$scope.$watchGroup(['vm.form.RCV_DT'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            vm.IS_NEXT = 'N';

        //            //vm.next();
        //            $scope.$parent.rollDelvGridDataSource.read();
        //        }
        //    }
        //}, true);

        //vm.onChangeProdDate = function () {
        //    return MrcDataService.GetAllOthers('/api/mrc/SampleReq/getSmplProdBatchListByDate?&pPROD_DT=' + $filter('date')(vm.form.PROD_DT, vm.dtFormat)).then(function (res) {
        //        vm.batchList = res;
        //    }, function (err) {
        //        console.log(err);
        //    });
        //}


        


    }

})();
////////// End Fabric Delivery Controller





////////// Start Yarn Return Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntSciYrnRtnDController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntSciYrnRtnDController]);
    function KntSciYrnRtnDController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();

        //if (yrnRcvListData['KNT_SCO_CHL_YRN_RET_D_ID']) {
        //    $scope.$parent.form['yrnRcvDtl'] = angular.copy(yrnRcvListData);
        //}
        //else {
        //    $scope.$parent.form['yrnRcvDtl'] = [];
        //}

        //vm.printButtonList = [{ BTN_ID: 1, BTN_NAME: 'Challan' }, { BTN_ID: 2, BTN_NAME: 'Party Ledger' }];
        vm.yesNoList = [{ LST_ID: 'Y', LST_NAME: 'Yes' }, { LST_ID: 'N', LST_NAME: 'No' }];
        vm.isYrnAddToGrid = 'Y';

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        vm.errstyle = { 'border': '1px solid #f13d3d' };
        

        activate();
        function activate() {
            var promise = [getReturnTypeList(), getMOUList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }


        
        function getYarnItemList() {
            vm.yarnItemOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "YARN_DETAIL",
                dataValueField: "YARN_ITEM_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    $scope.$parent.yrnItem.KNT_YRN_LOT_ID = item.KNT_YRN_LOT_ID;
                    $scope.$parent.yrnItem.YARN_DETAIL = item.YARN_DETAIL;
                }
            };
        };

        function getReturnTypeList() {
            vm.returnTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    $scope.$parent.yrnItem.RETURN_TYPE_DESC = item.LK_DATA_NAME_EN;
                }
            };

            return vm.returnTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.LookupListData(108).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

       
        function getMOUList() {
            vm.packMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);

                    $scope.$parent.yrnItem.PACK_MOU_ID = item.RF_MOU_ID;
                    $scope.$parent.yrnItem.PACK_MOU_CODE = item.MOU_CODE;
                },
                dataBound: function (e) {
                    $scope.$parent.yrnItem.PACK_MOU_ID = 29;
                }
            };

            vm.qtyMouOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);

                    $scope.$parent.yrnItem.QTY_MOU_ID = item.RF_MOU_ID;
                    $scope.$parent.yrnItem.QTY_MOU_CODE = item.MOU_CODE;
                },
                dataBound: function (e) {
                    $scope.$parent.yrnItem.QTY_MOU_ID = 3;
                }
            };

            return KnittingDataService.getDataByFullUrl('/api/common/MOUList').then(function (res) {
                //e.success(res);
                vm.packMouDataSource = new kendo.data.DataSource({
                    data: res
                });

                vm.qtyMouDataSource = new kendo.data.DataSource({
                    data: res
                });

            }, function (err) {
                console.log(err);
            });

        };

       
        vm.resetYarnItem = function () {
            $scope.$parent.yrnItem.KNT_SCO_CHL_YRN_RET_D_ID = 0;
            $scope.$parent.yrnItem.PACK_RET_QTY = null;
            $scope.$parent.yrnItem.QTY_PER_PACK = null;
            $scope.$parent.yrnItem.LOOSE_QTY = null;
            $scope.$parent.yrnItem.SP_NOTE = '';
        }

        vm.addYarnRcv = function (data) {

            console.log($scope.$parent.frmSciGreyFabDelv);

            if (!data.LK_YRN_RET_TYPE_ID) {
                $scope.$parent.frmSciGreyFabDelv.LK_YRN_RET_TYPE_ID.$setValidity('required', false);
                return;
            }
            if (!data.SCM_SC_WO_REF_ID) {
                $scope.$parent.frmSciGreyFabDelv.SCM_SC_WO_REF_ID.$setValidity('required', false);
                return;
            }
            if (!data.YARN_ITEM_ID) {
                $scope.$parent.frmSciGreyFabDelv.YARN_ITEM_ID.$setValidity('required', false);
                return;
            }

            if (!data.PACK_RET_QTY && data.LK_YRN_RET_TYPE_ID != 515) {
                $scope.$parent.frmSciGreyFabDelv.PACK_RET_QTY.$setValidity('required', false);
                return;
            }
            else if (data.PACK_RET_QTY < 1 && data.LK_YRN_RET_TYPE_ID != 515) {
                $scope.$parent.frmSciGreyFabDelv.PACK_RET_QTY.$setValidity('min', false);
                return;
            }

            if (!data.PACK_MOU_ID && data.LK_YRN_RET_TYPE_ID != 515) {
                $scope.$parent.frmSciGreyFabDelv.PACK_MOU_ID.$setValidity('required', false);
                return;
            }

            if (!data.RET_QTY) {
                $scope.$parent.frmSciGreyFabDelv.RET_QTY.$setValidity('required', false);
                return;
            }
            else if (data.RET_QTY<1) {
                $scope.$parent.frmSciGreyFabDelv.RET_QTY.$setValidity('min', false);
                return;
            }

            

            $scope.$parent.yrnRtnGridDataSource.insert(data, 0);
            vm.resetYarnItem();

            vm.isYrnAddToGrid = 'Y';
        }

        vm.editYarnRcv = function (index, dataList) {
            vm.yrnItem = angular.copy(dataList[index]);
            vm.yrnItem['yrnRcvIndex'] = index;
            console.log(vm.yrnItem);

            vm.isYrnAddToGrid = 'N';
            return vm.yarnItemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/knit/KntScChlnRcv/GetYarnListByProdOrdHdr?&pMC_FAB_PROD_ORD_H_ID=' + vm.yrnItem.MC_FAB_PROD_ORD_H_ID).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        vm.updateYarnRcv = function (data) {
            var yrnRcvFormCopy = angular.copy(data);

            console.log(yrnRcvFormCopy);
            console.log($scope.$parent.form.yrnRcvDtl);
            $scope.$parent.form.yrnRcvDtl[data.yrnRcvIndex] = yrnRcvFormCopy;
            vm.resetYarnItem();

            vm.isYrnAddToGrid = 'Y';
        };

        vm.removeYarnRcv = function (dataItem) {
            //alert('Remove Yarn');

            Dialog.confirm('Do you want to remove the record?', 'Confirmation', ['Yes', 'No']).then(function () {

                if (dataItem.KNT_SCI_YRN_RET_D_ID > 0) {
                    return $http({
                        headers: { 'Authorization': 'Bearer ' + $scope.token },
                        url: '/api/Knit/KntGreyFabDelv/RemoveYarnReturn',
                        method: 'post',
                        data: dataItem
                    })
                        .then(function (res) {
                            $scope.errors = undefined;
                            if (res.data.success === false) {
                                $scope.errors = [];
                                $scope.errors = res.data.errors;
                            }
                            else {

                                res['data'] = angular.fromJson(res.data.jsonStr);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    $scope.$parent.yrnRtnGridDataSource.remove(dataItem);

                                };

                                config.appToastMsg(res.data.PMSG);
                            }
                        }).catch(function (message) {
                            exception.catcher('XHR loading Failded')(message);
                        });
                }
                else {
                    $scope.$parent.yrnRtnGridDataSource.remove(dataItem);
                }
            });

            
        };

        vm.TotalReceiveQty = function (data) {
            if (data.PACK_RET_QTY > 0 && data.QTY_PER_PACK > 0)
                data.RET_QTY = (data.PACK_RET_QTY * data.QTY_PER_PACK) + (data.LOOSE_QTY || 0);
            else
                data.RET_QTY = (data.LOOSE_QTY || 0);
        };
        
        vm.onChangeYarnItemChange = function (e) {
            var item = e.sender.dataItem(e.item); //e.sender.dataItem(e.sender.select());
            console.log(item);

            $scope.$parent.yrnItem.KNT_YRN_LOT_ID = item.KNT_YRN_LOT_ID;
            $scope.$parent.yrnItem.YR_SPEC_DESC = item.YR_SPEC_DESC;
        }

    }

})();
////////// End Yarn Receive Controller




////////// Start List Controller
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntGreyFabDelvListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', KntGreyFabDelvListController]);
    function KntGreyFabDelvListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'KNT_SC_GFAB_DLV_H_ID';
        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.form = $stateParams.pSCM_SUPPLIER_ID > 0 ? { SCM_SUPPLIER_ID: $stateParams.pSCM_SUPPLIER_ID } : { SCM_SUPPLIER_ID: 0 };

        activate();
        function activate() {
            var promise = [GetSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.chlnDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.chlnDateOpened = true;
        };

        function GetSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    GetScProgList(item.SCM_SUPPLIER_ID);
                    $stateParams.pSCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                }//,
                //dataBound: function (e) {
                //    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                //        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                //    }
                //}
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function GetScProgList(supplierId) {
            vm.scProgOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "PRG_RCV_NO",
                dataValueField: "KNT_SC_PRG_RCV_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.SUP_TRD_NAME_EN = item.SUP_TRD_NAME_EN;
                    vm.form.SCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                    vm.form.SC_PRG_DT = item.SC_PRG_DT;
                }//,
                //dataBound: function (e) {
                //    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                //        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                //    }
                //}
            };

            return vm.scProgDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/knit/KntScRcv/GetScRcvProgramList?pSCM_SUPPLIER_ID=' + supplierId;
                        url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };
     
        vm.printChallan = function (dataItem) {

            $scope.form = angular.copy(dataItem);

            $scope.form.SC_FAB_DELV_CHALAN_NO = $scope.form.CHALAN_NO;
            $scope.form.REPORT_CODE = 'RPT-3508';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.copy($scope.form);
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


        vm.scFabDelvChallanOption = {
            height: 400,
            sortable: true,
            pageable: true,
            //groupable: true,
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
                { field: "SUP_TRD_NAME_EN", title: "Supplier", width: "10%", hidden: true },
                { field: "STORE_NAME_EN", title: "Store", width: "10%" },
                { field: "CHALAN_DT", title: "Challan Date", format: "{0:dd-MMM-yyyy}", width: "10%" },
                { field: "CHALAN_NO", title: "Challan#", type: "string", width: "10%" },                
                { field: "GT_PASS_NO", title: "Gate Pass#", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editScFabDelvChallan(dataItem)' title='Edit'><i class='fa fa-edit'></i></button>&nbsp;&nbsp;<button type='button' class='btn btn-xs blue' ng-click='vm.printChallan(dataItem)' title='Print'><i class='fa fa-print'></i></button>";
                    },
                    width: "4%"
                }
            ]
        };

        vm.scFabDelvChallanDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_SC_GFAB_DLV_H_ID",
                    fields: {
                        CHALAN_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/knit/KntGreyFabDelv/GetScFabDelvChlnListByPartyID?pSCM_SUPPLIER_ID=' + ($stateParams.pSCM_SUPPLIER_ID || 0); //+ '&pKNT_SC_PRG_RCV_ID=' + vm.form.KNT_SC_PRG_RCV_ID;

                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += KnittingDataService.kFilterStr2QueryParam(params.filter);

                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    });
                }
            },
            group: {
                field: "SUP_TRD_NAME_EN",
                dir: "asc"
            },
            sort: [{ field: 'CHALAN_DT', dir: 'desc' }]
        });

        vm.getScFabDelvChallanList = function () {
            vm.scFabDelvChallanDataSource.read();
        };

        vm.addScFabDelvChallan = function () {
            $state.go('KntScGreyFabDelvH.FabDelv', { pKNT_SC_GFAB_DLV_H_ID: 0 });
        };

        vm.editScFabDelvChallan = function (dataItem) {
            $state.go('KntScGreyFabDelvH.FabDelv', { pKNT_SC_GFAB_DLV_H_ID: dataItem.KNT_SC_GFAB_DLV_H_ID });
        };

        
        

    }

})();
////////// End List Controller