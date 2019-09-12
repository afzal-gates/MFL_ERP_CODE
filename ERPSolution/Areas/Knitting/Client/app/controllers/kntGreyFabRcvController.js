////////// Start Header Controller
/// <reference path="kntGreyFabRcvController.js" />
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntGreyFabRcvController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'KnittingDataService', 'Dialog', 'cur_user_id', KntGreyFabRcvController]);
    function KntGreyFabRcvController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, KnittingDataService, Dialog, cur_user_id) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
        
        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'MC_SMP_REQ_H_ID';
        vm.today = new Date();
        vm.TOT_RCV_ROLL_WT = 0;

        vm.form = {
            RCV_DT: vm.today, KNT_FAB_STR_RCV_ID: 0, IS_G_F: 'G', KNT_STYL_FAB_ITEM_ID: 0, RCV_NO_ROLL: 0, RCV_ROLL_WT: 0, WT_MOU_ID: 0, REMARKS: '', XML_DTL: '',
            REPORT_CODE: 'RPT-3546'
        };
         
        $('#FAB_ROLL_NO').focus();

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getStoreList(), getProdCatSourceList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }

        vm.rcvDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.rcvDateOpened = true;
        };

        function getStoreList() {
            return vm.storeList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=5&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                //KnittingDataService.LookupListData(92).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    
                    vm.form.LK_WH_TYPE_ID = item.LK_WH_TYPE_ID;

                    vm.form.STORE_NAME_EN = item.STORE_NAME_EN;
                }
            };
        };

        function getProdCatSourceList() {
            
            return vm.prodCatDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {                       
                        var url = '/api/common/GetFabProdCat';
                        
                        return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.onChangeProdCat = function () {
            
            var prodCatList =[];
            prodCatList = _.map(_.filter(vm.prodCatDataSource.data(), function (ob) {
                return _.some(vm.form.RF_FAB_PROD_CAT_ID_LST, function (val) {                    
                    return ob.RF_FAB_PROD_CAT_ID == val;
                });
            }), 'FAB_PROD_CAT_SNAME');

            console.log(prodCatList);
            vm.form.RF_FAB_PROD_CAT_NAME_LST = prodCatList.length>0?prodCatList.join(','):'All';
        }


        $("#FAB_ROLL_NO").keypress(function (e) {
            vm.errors = undefined;
            
            
            if (e.which == 13) {
                var rcvData = [];
                var submitRcvData = {};

                var isExistsRoll = 'N';
                KnittingDataService.getDataByFullUrl('/api/Knit/KntGreyFabRcv/GetRollData?&pFAB_ROLL_NO=' + vm.form.FAB_ROLL_NO + '&pSCM_STORE_ID=' + vm.form.SCM_STORE_ID).then(function (res) {

                    //alert(res.RF_FAB_PROD_CAT_ID + '-' + vm.form.LK_WH_TYPE_ID);
                    //return;

                    console.log(res);
                    console.log(vm.form);


                    if (res.KNT_FAB_ROLL_ID < 1) {
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = [{ error: 'Sorry! Roll# invalid or waiting for QC.' }];
                        return;
                    }
                    else if (res.IS_G_F == 'G') {
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = [{ error: 'Sorry! Already received the roll# at ' + $filter('date')(res.RCV_DT, vm.dtFormat) + '.' }];
                        return;
                    }
                    else if (res.RF_FAB_PROD_CAT_ID != 4 && vm.form.LK_WH_TYPE_ID == 514) { //4-->S/C In-house, 514-->Party WH
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = [{ error: 'Sorry! This roll# receive only that store which type is in-house grey store.' }];
                        return;
                    }
                    else if (res.RF_FAB_PROD_CAT_ID == 4 && vm.form.LK_WH_TYPE_ID != 514) { //4-->S/C In-house, 514-->Party WH
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = [{ error: 'Sorry! This roll# receive only that store which type is S/C party store.' }];
                        return;
                    }
                    else if (res.KNT_SC_PRG_RCV_ID < 1 && res.SC_MAP_BYR_STR_ID < 1 && res.RF_FAB_PROD_CAT_ID != 10) {
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = [{ error: 'Sorry! This buyer`s fabric roll is not valid to receive at selected store.' }];
                        return;
                    }
                    else if (res.SC_MAP_STR_PRD_CAT_ID < 1) {
                        new Audio('../../Content/sounds/errorSound.mp3').play();
                        vm.errors = [{ error: 'Sorry! Store selection is wrong for this production category.' }];
                        return;
                    }

                    
                    //return;

                    //var rcvGridList = vm.rollRcvGridDataSource.data();
                    //var rollIdList = [];

                    //var rcvList = _.filter(rcvGridList, function (o) {                        
                    //    if (o.KNT_FAB_ROLL_ID==res.KNT_FAB_ROLL_ID) {
                    //        isExistsRoll = 'Y';
                    //    }

                    //    return o.KNT_FAB_ROLL_ID == res.KNT_FAB_ROLL_ID;
                    //});

                    //if (isExistsRoll == 'Y') {
                    //    vm.errors = [{error: 'Sorry! Already received the roll#.'}];                        
                    //    return;
                    //}


                    if (res['RCV_NO_ROLL']) {
                        var data = {
                            KNT_FAB_STR_RCV_ID: 0, SCM_STORE_ID: vm.form.SCM_STORE_ID, RCV_DT: $filter('date')(vm.form.RCV_DT, vm.dtFormat), IS_G_F: 'G',
                            KNT_STYL_FAB_ITEM_ID: res.KNT_STYL_FAB_ITEM_ID, FAB_PROD_CAT_SNAME: res.FAB_PROD_CAT_SNAME, STYLE_NO: res.STYLE_NO,
                            BUYER_SNAME: res.BUYER_SNAME, ORDER_NO_LST: res.ORDER_NO_LST, COLOR_NAME_EN: res.COLOR_NAME_EN, FAB_TYPE_SNAME: res.FAB_TYPE_SNAME,
                            ACT_FIN_GSM: res.ACT_FIN_GSM, ACT_FIN_DIA: res.ACT_FIN_DIA, MC_DIA: res.MC_DIA, MC_GG: res.MC_GG, FAB_ROLL_NO: res.FAB_ROLL_NO,
                            //FAB_DETAIL: res.COLOR_NAME_EN + '/' + res.FAB_TYPE_SNAME + '/' + res.ACT_FIN_DIA + '/' + res.MC_DIA + '/' + res.ACT_FIN_GSM + '/' + res.MC_GG,
                            KNT_FAB_ROLL_ID: res.KNT_FAB_ROLL_ID, RCV_NO_ROLL: res.RCV_NO_ROLL, RCV_ROLL_WT: res.RCV_ROLL_WT, WT_MOU_ID: res.WT_MOU_ID,
                            ITM_ROLL_REF: res.ITM_ROLL_REF
                        };

                        //vm.rollRcvGridDataSource.insert(data, 0);
                        
                            submitRcvData['IS_FINALIZE'] = 'Y';
                            submitRcvData['SCM_STORE_ID'] = vm.form.SCM_STORE_ID;
                            submitRcvData['RCV_DT'] = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

                            rcvData.push(data);
                            console.log(rcvData);


                            submitRcvData.XML_DTL = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {
                                //ob['RCV_DT'] = $filter('date')($scope.form.RCV_DT, vm.dtFormat);

                                return {
                                    KNT_FAB_STR_RCV_ID: ob.KNT_FAB_STR_RCV_ID, RCV_DT: ob.RCV_DT, IS_G_F: ob.IS_G_F,
                                    KNT_STYL_FAB_ITEM_ID: ob.KNT_STYL_FAB_ITEM_ID, RCV_NO_ROLL: ob.RCV_NO_ROLL, RCV_ROLL_WT: ob.RCV_ROLL_WT,
                                    WT_MOU_ID: ob.WT_MOU_ID, REMARKS: ob.REMARKS, IS_FINALIZE: ob.IS_FINALIZE, KNT_FAB_ROLL_ID: ob.KNT_FAB_ROLL_ID,
                                    SCM_STORE_ID: ob.SCM_STORE_ID
                                };
                            }));

                            console.log(submitRcvData);

                            return $http({
                                headers: { 'Authorization': 'Bearer ' + $scope.token },
                                url: '/api/Knit/KntGreyFabRcv/BatchSave',
                                method: 'post',
                                data: submitRcvData
                            }).then(function (res) {
                                $scope.errors = undefined;
                                if (res.data.success === false) {
                                    new Audio('../../Content/sounds/errorSound.mp3').play();
                                    $scope.errors = [];
                                    $scope.errors = res.data.errors;
                                }
                                else {

                                    res['data'] = angular.fromJson(res.data.jsonStr);

                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                        vm.isSaved = true;
                                        new Audio('../../Content/sounds/OkSound.mp3').play();

                                        data['RCV_STATUS'] = "Stored";
                                        data['IS_FINALIZE'] = "Y";

                                        vm.rollRcvGridDataSource.insert(data, 0);
                                        vm.TOT_RCV_ROLL_WT = vm.TOT_RCV_ROLL_WT + data['RCV_ROLL_WT'];

                                        //vm.rollRcvGridDataSource.read();
                                    };

                                    config.appToastMsg(res.data.PMSG);
                                }
                            }).catch(function (message) {
                                exception.catcher('XHR loading Failded')(message);
                            });
                       
                    }
                    else {
                        vm.errors = [{ error: 'Sorry! This roll# is invalid or this roll# isn`t valid from QC' }];
                        return;                       
                    }
                }, function (err) {
                    console.log(err);
                });
                
                vm.form.FAB_ROLL_NO = '';
                $("#FAB_ROLL_NO").focus();
            };
        });


        vm.rollRcvGridOption = {
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
                { field: "FAB_PROD_CAT_SNAME", title: "Prod.Type", type: "string", width: "7%", editable: false },
                { field: "BUYER_SNAME", title: "Buyer/Party", type: "string", width: "10%", editable: false },
                { field: "STYLE_NO", title: "Style#", type: "string", width: "10%", editable: false },
                { field: "ORDER_NO_LST", title: "Order#", type: "string", width: "15%", editable: false },                
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%", editable: false },
                { field: "ITM_ROLL_REF", title: "Code", type: "string", width: "6%", editable: false, filterable: false },
                { field: "FAB_ROLL_NO", title: "Roll#", type: "string", width: "9%", editable: false },
                { field: "FAB_TYPE_SNAME", title: "Fab.Type", type: "string", width: "7%", editable: false, filterable: false },
                { field: "ACT_FIN_DIA", title: "F.Dia", type: "string", width: "5%", editable: false, filterable: false },
                { field: "ACT_FIN_GSM", title: "F.Gsm", type: "string", width: "5%", editable: false, filterable: false/*, footerTemplate: "Total:"*/ },
                { field: "MC_DIA", title: "Mc.Dia", type: "string", width: "5%", editable: false, filterable: false, hidden: true },
                { field: "MC_GG", title: "Mc GG", type: "string", width: "5%", editable: false, filterable: false, hidden: true },
                //{ field: "KNT_FAB_ROLL_ID", title: "Roll ID", type: "string", width: "5%", editable: false },                
                { field: "RCV_ROLL_WT", title: "Roll Wt(Kg)", type: "number", width: "7%", editable: false, filterable: false/*, footerTemplate: "#=sum#"*/ },
                { field: "RCV_STATUS", title: "Status", type: "string", width: "5%", editable: false, filterable: false },
                {
                    title: "",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.IS_FINALIZE==\"Y\"' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "3%"
                }
            ],
            change: function (e) {
                var grid = $("#rcvGrid").data("kendoGrid");
                //grid.select("tr:eq(1)");
                var row = grid.select();
                vm.selectedItem = grid.dataItem(row);
                $scope.$apply();
            },
            dataBound: function () {
                var grid = this;
                $.each(grid.tbody.find('tr'), function (idx) {

                    var model = grid.dataItem(this);

                    if (idx == 0) {
                        vm.selectedItem = model;
                        $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                    }
                });
            }
        };

       
        vm.rollRcvGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/Knit/KntGreyFabRcv/GetFabRcvDataByDate';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pSCM_STORE_ID=' + vm.form.SCM_STORE_ID + '&pRCV_DT=' + $filter('date')(vm.form.RCV_DT, vm.dtFormat);

                    console.log(url);

                    //url += config.kFilterStr2QueryParam(params.filter);
                    //if (params.filter.length == 0) {
                    //    remQueryParam();
                    //}
                    KnittingDataService.getDataByFullUrl(url).then(function (res) {

                        console.log(res);

                        if (res.data.length > 0) {
                            vm.TOT_RCV_ROLL_WT = res.data[0].TOT_RCV_ROLL_WT;
                        }

                        e.success(res);
                    });                    
                }               
            },
            //aggregate: [
            //    { field: "RCV_NO_ROLL", aggregate: "sum" },
            //    { field: "RCV_ROLL_WT", aggregate: "sum" }
            //],
            pageSize: 20,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "KNT_FAB_STR_RCV_ID",
                    fields: {
                        RCV_DT: { type: "date", editable: false },
                        ORDER_NO_LST: { type: "string", filterable: false },
                        RCV_NO_ROLL: { type: "number", filterable: false },
                        RCV_ROLL_WT: { type: "number", filterable: false }                        
                    }
                }
            }            
        });
        
//        { field: "FAB_TYPE_SNAME", title: "Fab.Type", type: "string", width: "7%", editable: false, footerTemplate: "Total:" },
//    { field: "ACT_FIN_DIA", title: "F.Dia", type: "string", width: "5%", editable: false },                
//{ field: "ACT_FIN_GSM", title: "F.Gsm", type: "string", width: "5%", editable: false },
//                { field: "MC_DIA", title: "Mc.Dia", type: "string", width: "5%", editable: false },
//                { field: "MC_GG", title: "Mc GG", type: "string", width: "5%", editable: false, footerTemplate: "Total:" },
//                //{ field: "KNT_FAB_ROLL_ID", title: "Roll ID", type: "string", width: "5%", editable: false },
//                { field: "RCV_NO_ROLL", title: "R.Qty", type: "number", width: "5%", editable: false, footerTemplate: "#=sum#" },
//                { field: "RCV_ROLL_WT", title: "Roll Wt(Kg)", type: "number", width: "7%", editable: false, footerTemplate: "#=sum#" },
//                { field: "RCV_STATUS", title: "Status", type: "string", width: "5%", editable: false },

        vm.removeRow = function (dataItem) {
            vm.rollRcvGridDataSource.remove(dataItem);
            //var dataList = vm.rollRcvGridDataSource.data();

        };

        vm.next = function () {
            $state.go('KntGreyFabRcv', {
                pPROD_DT: vm.form.RCV_DT == null ? $stateParams.pRCV_DT : moment(vm.form.RCV_DT).format("YYYY-MMM-DD")
            }, { reload: 'KntGreyFabRcv' });
            vm.IS_NEXT = 'Y';
            //vm.showSplash = 'N';
        };
        
        

        $scope.$watchGroup(['vm.form.SCM_STORE_ID', 'vm.form.RCV_DT'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.selectedItem = undefined;
                    vm.errors = null;

                    vm.IS_NEXT = 'N';
                    
                    //vm.next();
                    vm.rollRcvGridDataSource.read();
                }
            }
        }, true);
        
        //vm.onChangeProdDate = function () {
        //    return MrcDataService.GetAllOthers('/api/mrc/SampleReq/getSmplProdBatchListByDate?&pPROD_DT=' + $filter('date')(vm.form.PROD_DT, vm.dtFormat)).then(function (res) {
        //        vm.batchList = res;
        //    }, function (err) {
        //        console.log(err);
        //    });
        //}

        
        vm.submitData = function (token, pIS_FINALIZE) {
            var submitRcvData = angular.copy(vm.form);
            var vMsg = 'Do you want to save?';
            if (pIS_FINALIZE == 'Y') {
                vMsg = 'Do you want to save and finalize?';
            }

           
            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {                             

                submitRcvData['IS_FINALIZE'] = pIS_FINALIZE;
                submitRcvData['SCM_STORE_ID'] = vm.form.SCM_STORE_ID;
                submitRcvData['RCV_DT'] = $filter('date')(vm.form.RCV_DT, vm.dtFormat);

                var rcvData = vm.rollRcvGridDataSource.data();
                console.log(rcvData);
            

                submitRcvData.XML_DTL = KnittingDataService.xmlStringShort(rcvData.map(function (ob) {                
                    //ob['RCV_DT'] = $filter('date')($scope.form.RCV_DT, vm.dtFormat);

                    return {
                        KNT_FAB_STR_RCV_ID: ob.KNT_FAB_STR_RCV_ID, RCV_DT: ob.RCV_DT, IS_G_F: ob.IS_G_F,
                        KNT_STYL_FAB_ITEM_ID: ob.KNT_STYL_FAB_ITEM_ID, RCV_NO_ROLL: ob.RCV_NO_ROLL, RCV_ROLL_WT: ob.RCV_ROLL_WT,
                        WT_MOU_ID: ob.WT_MOU_ID, REMARKS: ob.REMARKS, IS_FINALIZE: ob.IS_FINALIZE, KNT_FAB_ROLL_ID: ob.KNT_FAB_ROLL_ID,
                        SCM_STORE_ID: ob.SCM_STORE_ID
                    };
                }));

                console.log(submitRcvData);

                return $http({
                    headers: { 'Authorization': 'Bearer ' + token },
                    url: '/api/Knit/KntGreyFabRcv/BatchSave',
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
                            vm.rollRcvGridDataSource.read();
                        };

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });

            });
        };

        vm.backToList = function () {
            return $state.go('SmplFabBookList', { pMC_BYR_ACC_ID: 0 });
        };

       
        vm.rptList = [{ REPORT_CODE: 'RPT-3546', REPORT_NAME: 'Detail' }, { REPORT_CODE: 'RPT-3515', REPORT_NAME: 'Summery' }];

        vm.printChallan = function () {
            //console.log(dataItem);

            vm.isRDLC = true;
            //vm.form.REPORT_CODE = 'RPT-3515';
            
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
            vm.form.TO_DATE = $filter('date')(vm.form.RCV_DT, vm.dtFormat);
            
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
////////// End Header Controller

