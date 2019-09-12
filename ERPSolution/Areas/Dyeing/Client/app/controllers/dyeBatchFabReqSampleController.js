(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeBatchFabReqSampleController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', '$filter', 'McList', DyeBatchFabReqSampleController]);
    function DyeBatchFabReqSampleController($q, config, DyeingDataService, $stateParams, $state, $scope, Dialog, $modal, $filter, McList) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.disabledEdit = true;
        var LcScheduleData = {};
        var MC_OPEN = [];

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getLastScPlanDt()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        //$scope.FIRSTDATE_LNopen = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();
        //    $scope.FIRSTDATE_LNopened = true;
        //}
        //$scope.LASTDATE_LNopen = function ($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();
        //    $scope.LASTDATE_LNopened = true;
        //}


        vm.DYE_BATCH_SCDL_ID = $stateParams.pDYE_BATCH_SCDL_ID || null;

        function getLastScPlanDt() {
            return DyeingDataService.getDataByUrl('/DyeBatchPlan/getLastScPlanDt?pIS_SMP_BLK=S').then(function (res) {
                vm.PRE_BC_END_DT = angular.fromJson(res).OP_PRE_BC_END_DT;
            });
        }

        vm.companyDs = new kendo.data.DataSource({
            data: McList
        });

        vm.onChangeQty = function (main) {
            var sum = 0;
            angular.forEach(_.groupBy(
              main.requirements
            , function (o) {
                return o.MC_FAB_PROD_ORD_H_ID;
            }), function (val, key) {

                sum = _.sumBy(val, function (oo) { return (oo.ACT_BATCH_QTY || oo.RQD_QTY_KG || 0); });

                angular.forEach(main.requirements, function (vvv, kkk) {
                    if (vvv.MC_FAB_PROD_ORD_H_ID == parseInt(key) && vvv.MC_FAB_PROD_ORD_H_SL == 1) {
                        vvv['BT_TOTAL'] = sum;
                    };
                });

            });
        };


        vm.onChangeQtyD = function (list, d) {
            d.ACT_BATCH_QTY = _.sumBy(d.non_col_cu_avail, function (o) { return (o.ASSIGN_QTY || 0); });
            vm.onChangeQty(list);
        };


        vm.onCompanyDataBound = function (e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].HR_COMPANY_ID);
                vm.HR_COMPANY_ID = ds[0].HR_COMPANY_ID;
            } else if (ds.length > 1) {
                e.sender.value(ds[0].HR_COMPANY_ID);
                vm.HR_COMPANY_ID = ds[0].HR_COMPANY_ID;
            } else {
                vm.HR_COMPANY_ID = -1;
            }
        };



        vm.onChangeCompany = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.HR_COMPANY_ID && $stateParams.pDYE_BATCH_SCDL_ID) {
                getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID, '', item.HR_COMPANY_ID);
            } else {
                getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID, '', null);
            }
        }

        
        vm.openLotDiaWiseKnitProd = function (data) {
            var params = {
                REPORT_CODE: 'RPT-3591',
                MC_BYR_ACC_GRP_ID: data.MC_BYR_ACC_GRP_ID,
                MC_STYLE_H_EXT_ID: data.MC_STYLE_H_EXT_ID,
                MC_COLOR_ID: data.FAB_COLOR_ID,
                RF_FAB_PROD_CAT_ID: data.RF_FAB_PROD_CAT_ID
            };

            console.log(params);
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');
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



        vm.form = { REPORT_CODE: '', DYE_BATCH_NO: '', DYE_BT_CARD_H_ID: '' }

        vm.printBatchCard = function (dataItem) {

            console.log(dataItem);
            vm.form['REPORT_CODE'] = 'RPT-4032';

            var idNo = dataItem.DYE_BATCH_NO_LST.split('(');
            vm.form['DYE_BATCH_NO'] = idNo[0];

            //vm.form['DYE_BATCH_NO'] = dataItem.DYE_BATCH_NO;
            vm.form['DYE_BT_CARD_H_ID'] = dataItem.DYE_BT_CARD_H_ID;

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

        vm.SchedulePlanData = {
            optionLabel: "--Select Plan--",
            filter: "contains",
            autoBind: true,
            template: '<span><h4 style="padding:0px;margin:0px;"><b>#: data.SCDL_REF_NO #</b></h4><p> #= kendo.toString(kendo.parseDate(data.START_DT),"dd/MM/yyyy HH:mm") # to #= kendo.toString(kendo.parseDate(data.END_DT),"dd/MM/yyyy HH:mm") #</p></span>',
            valueTemplate: '<span><h4 style="padding:0px;margin:0px;"><b>#: data.SCDL_REF_NO #</b><small>(#= kendo.toString(kendo.parseDate(data.START_DT),"dd/MM/yyyy HH:mm") # to #= kendo.toString(kendo.parseDate(data.END_DT),"dd/MM/yyyy HH:mm") #)</small></h4></span>',
            dataSource: {
                transport: {
                    read: function (e) {


                        var url = "/DyeBatchPlan/getSchedulePlanDatas";
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '?pSCDL_REF_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '?pSCDL_REF_NO';
                        }
                        url += '&pDYE_BATCH_SCDL_ID=' + ($stateParams.pDYE_BATCH_SCDL_ID || null);
                        url += '&pIS_SMP_BLK=S';

                        DyeingDataService.getDataByUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true,
            },
            select: function (e) {

                var item = this.dataItem(e.item);
                //if (item.IS_FINALIZED == 'N' && item.DYE_BATCH_SCDL_ID) {
                //    vm.EditPlanData = item;
                //}
                //else {
                //    vm.EditPlanData['DYE_BATCH_SCDL_ID'] = null;
                //}

                if (item && item.DYE_BATCH_SCDL_ID) {

                    getBatchProgramData(item.DYE_BATCH_SCDL_ID, '', vm.HR_COMPANY_ID);
                    $state.go('DyeBatchFabReqSample', { pDYE_BATCH_SCDL_ID: item.DYE_BATCH_SCDL_ID }, { notify: false });
                    vm.EditPlanData = item;
                    //getBatchProgramData(item.DYE_BATCH_SCDL_ID);
                    // $state.go('DyeBatchFabReqSample', { pDYE_BATCH_SCDL_ID: item.DYE_BATCH_SCDL_ID }, { reload: true });
                };
            },
            dataBound: function (e) {
                var ds = e.sender.dataSource.data();

                if ($stateParams.pDYE_BATCH_SCDL_ID) {
                    var item = _.find(ds, function (o) {
                        return o.DYE_BATCH_SCDL_ID == $stateParams.pDYE_BATCH_SCDL_ID;
                    });

                    LcScheduleData = item;
                } else {
                    var item = _.find(ds, function (o) {
                        return o.IS_ACTIVE == 'Y';
                    });

                    LcScheduleData = item;
                }


                if (item && item.DYE_BATCH_SCDL_ID) {
                    vm.EditPlanData = item;
                    $state.go('DyeBatchFabReqSample', { pDYE_BATCH_SCDL_ID: item.DYE_BATCH_SCDL_ID }, { notify: false });
                    e.sender.value(item.DYE_BATCH_SCDL_ID);
                    getBatchProgramData(item.DYE_BATCH_SCDL_ID, '', vm.HR_COMPANY_ID);

                };
            },
            dataTextField: "SCDL_REF_NO",
            dataValueField: "DYE_BATCH_SCDL_ID"
        };

        //vm.onChangeQtyD = function (d) {
        //    d.ACT_BATCH_QTY = _.sumBy(d.non_col_cu_avail, function (o) { return (o.ASSIGN_QTY || 0); });
        //};


        vm.openBatchPlanModal = function (item) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Dyeing/Dye/_DyeProgramScdlModal',
                controller: function ($scope, config, $modalInstance, $filter, DyeingDataService, formData) {
                    $scope.alerts = [];
                    $scope.closeAlert = function (index) {
                        $scope.alerts.splice(index, 1);
                    };

                    $scope.onStartChange = function (e) {
                        console.log(e.sender);
                    };

                    $scope.form = formData;


                    $scope.datePickerOptionsEnd = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
                        format: "dd-MMM-yyyy hh:mm tt",
                        change: function (e) {
                            $scope.form['DATE_DIFF'] = moment(this.value()).diff(moment($scope.form.START_DT), 'days');
                        }
                    };

                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
                        format: "dd-MMM-yyyy hh:mm tt"
                    };

                    $scope.save = function (data) {

                        data['pOption'] = 1000;
                        data['START_DT'] = $filter('date')(data.START_DT, 'medium');
                        data['END_DT'] = $filter('date')(data.END_DT, 'medium');


                        return DyeingDataService.saveDataByUrl(data, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                            if (res.success === false) {
                            }
                            else {
                                res['data'] = angular.fromJson(res.jsonStr);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    $modalInstance.close({
                                        DYE_BATCH_SCDL_ID: parseInt(res.data.OP_DYE_BATCH_SCDL_ID || 0)
                                    });
                                }
                                config.appToastMsg(res.data.PMSG);
                            }
                        });
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function () {
                        return {
                            DYE_BATCH_SCDL_ID: item.DYE_BATCH_SCDL_ID,
                            END_DT: item.END_DT,
                            IS_FINALIZED: item.IS_FINALIZED,
                            PRE_BC_END_DT: item.PRE_BC_END_DT,
                            SCDL_NOTE: item.SCDL_NOTE,
                            SCDL_REF_NO: item.SCDL_REF_NO,
                            START_DT: item.START_DT ? item.START_DT : item.PRE_BC_END_DT,
                            IS_SMP_BLK: item.IS_SMP_BLK ? item.IS_SMP_BLK : 'S'
                        }
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                $state.go('DyeBatchFabReqSample', { pDYE_BATCH_SCDL_ID: dta.DYE_BATCH_SCDL_ID }, { reload: true });
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };


        vm.openSampleFabricModal = function (item) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'SampleFabricModal.html',
                controller: function ($scope, config, $modalInstance, $filter, DyeingDataService, formData) {
                    $scope.alerts = [];
                    $scope.closeAlert = function (index) {
                        $scope.alerts.splice(index, 1);
                    };


                    getFabOederByOh(null, 1, null, null, null);
                    getStyleExtList(null, 1, null, null);

                    findGridDataAll();
                    getSelectMonth(null, null);
                    //getFabOederByOh(null, null, null, null, null);


                    $scope.form = formData;

                    $scope.today = new Date();

                    $scope.dtFormat = config.appDateFormat;

                    $scope.datePickerOptionsEnd = {
                        parseFormats: ["yyyy-MM-dd"],
                        format: "dd-MMM-yyyy",
                        change: function (e) {
                            $scope.form['DATE_DIFF'] = moment(this.value()).diff(moment($scope.form.START_DT), 'days');
                        }
                    };

                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-dd"],
                        format: "dd-MMM-yyyy"
                    };


                    //$scope.dateOptions = {
                    //    formatYear: 'yy',
                    //    startingDay: 6
                    //};

                    //$scope.FROM_DATE_LNopen = function ($event) {
                    //    $event.preventDefault();
                    //    $event.stopPropagation();
                    //    $scope.FROM_DATE_LNopened = true;
                    //}
                    //$scope.TO_DATE_LNopen = function ($event) {
                    //    //$scope.FIRSTDATE_LNopened = false;
                    //    $event.preventDefault();
                    //    $event.stopPropagation();
                    //    $scope.TO_DATE_LNopened = true;
                    //}

                    $scope.ClearData = function (obj) {
                        alert("");
                        $scope.form = { MC_BYR_ACC_GRP_ID: '', MC_FAB_PROD_ORD_H_IDD: '', MC_COLOR_ID: '' }
                    }

                    $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p style="font-size:x-small; color:red;"> #: data.FAB_PROD_CAT_SNAME # (#: data.STYLE_NO #)</p></span>';
                    $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #/#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';

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

                    function getStyleExtList(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
                        $scope.StyleExtDs = {
                            transport: {
                                read: function (e) {
                                    var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                                    url += "&pRF_FAB_PROD_CAT_LST=1,3,5";
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



                    function getFabOederByOh(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID) {
                        $scope.FabOederByOhDs = {
                            transport: {
                                read: function (e) {
                                    var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                                    url += "&pRF_FAB_PROD_CAT_LST=1,3,5";
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

                    //$scope.buyerAcList = {
                    //    optionLabel: "--- Buyer A/C ---",
                    //    filter: "contains",
                    //    autoBind: true,
                    //    dataSource: {
                    //        transport: {
                    //            read: function (e) {
                    //                return DyeingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                    //                    e.success(res);
                    //                });
                    //            }
                    //        }
                    //    },
                    //    select: function (e) {
                    //        var item = this.dataItem(e.item);
                    //        if (item.MC_BYR_ACC_ID.length != 0) {
                    //            getStyleExtList(item.MC_BYR_ACC_ID, null);
                    //            getSelectMonth(item.MC_BYR_ACC_ID);
                    //            getFabOederByOh(item.MC_BYR_ACC_ID, null, null, null, null);
                    //        }
                    //    },
                    //    dataTextField: "BYR_ACC_NAME_EN",
                    //    dataValueField: "MC_BYR_ACC_ID"
                    //};


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

                    function getSelectMonth(MC_BYR_ACC_ID, RF_FAB_PROD_CAT_ID) {
                        return DyeingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (MC_BYR_ACC_ID || 0) + '/0/' + (RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
                            $scope.shipMonthList = new kendo.data.DataSource({
                                data: res
                            })
                        });
                    };

                    $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME #</p></span>';
                    $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME #)</h5></span>';



                    //function getStyleExtList(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {
                    //    $scope.StyleExtDs = {
                    //        transport: {
                    //            read: function (e) {
                    //                var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                    //                url += "&pRF_FAB_PROD_CAT_LST=1,3,5";
                    //                url += "&pFIRSTDATE=" + (pFIRSTDATE || null);
                    //                url += "&pLASTDATE=" + (pLASTDATE || null);

                    //                var webapi = new kendo.data.transports.webapi({});
                    //                var params = webapi.parameterMap(e.data);
                    //                if (params.filter) {
                    //                    url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                    //                } else {
                    //                    url += '&pSTYLE_NO';
                    //                }
                    //                url += '&pMC_FAB_PROD_ORD_H_ID=' + (formData.MC_FAB_PROD_ORD_H_ID || null);
                    //                return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                    //                    e.success(res);
                    //                });
                    //            }
                    //        },
                    //        serverFiltering: true
                    //    }
                    //};

                    $scope.colorDs = {
                        transport: {
                            read: function (e) {
                                var url = "/api/mrc/ColourMaster/SelectAll?pOption=3000";
                                var webapi = new kendo.data.transports.webapi({});
                                var params = webapi.parameterMap(e.data);
                                if (params.filter) {
                                    url += '&pCOLOR_NAME_EN=' + params.filter.replace(/'/g, '').split('~')[2];
                                } else {
                                    url += '&pCOLOR_NAME_EN';
                                }

                                return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                                    e.success(res);
                                });
                            }
                        },
                        serverFiltering: true
                    }

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


                    function getFabOederByOh(pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID) {
                        $scope.FabOederByOhDs = {
                            transport: {
                                read: function (e) {
                                    var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_ID=" + pMC_BYR_ACC_ID;
                                    url += "&pRF_FAB_PROD_CAT_LST=1,3";
                                    url += "&pFIRSTDATE=" + pFIRSTDATE;
                                    url += "&pLASTDATE=" + pLASTDATE;

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

                    function findGridDataAll() {
                        var data = {
                            MC_BYR_ACC_ID: null,
                            MC_BYR_ACC_GRP_ID: null,
                            FIRSTDATE: null,
                            LASTDATE: null,
                            MC_FAB_PROD_ORD_H_IDD: null,
                            LK_COL_TYPE_ID: null,
                            MC_COLOR_ID: null
                        }

                        $scope.gridSearchResultDs = new kendo.data.DataSource({
                            serverPaging: true,
                            serverFiltering: true,
                            pageSize: 20,
                            schema: {
                                data: "data",
                                total: "total",
                            },
                            transport: {
                                read: function (e) {
                                    var webapi = new kendo.data.transports.webapi({});
                                    var params = webapi.parameterMap(e.data);
                                    var url = "/api/Knit/FabProdKnitOrder/getFabOrderDataForSampleBatchProgram?pMC_BYR_ACC_ID=" + data.MC_BYR_ACC_ID;
                                    url += "&pMC_BYR_ACC_GRP_ID=" + data.MC_BYR_ACC_GRP_ID;
                                    url += "&pRF_FAB_PROD_CAT_ID_LST=1,3,5";
                                    url += "&pFIRSTDATE=" + data.FIRSTDATE;
                                    url += "&pLASTDATE=" + data.LASTDATE;
                                    url += "&pMC_FAB_PROD_ORD_H_ID=" + data.MC_FAB_PROD_ORD_H_IDD;
                                    url += "&pLK_COL_TYPE_ID=" + data.LK_COL_TYPE_ID;

                                    url += "&pMC_COLOR_ID=" + data.MC_COLOR_ID;

                                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                                    url += config.kFilterStr2QueryParam(params.filter);
                                    DyeingDataService.getDataByFullUrl(url).then(function (res) {
                                        e.success(res);
                                    });
                                }
                            },
                            //group: { field: "BYR_ACC_NAME_EN" }
                        });
                    };

                    $scope.SearchData = function (dataOri) {

                        var Defdata = {
                            MC_BYR_ACC_ID: null,
                            MC_BYR_ACC_GRP_ID: null,
                            FIRSTDATE: null,
                            LASTDATE: null,
                            MC_FAB_PROD_ORD_H_IDD: null,
                            LK_COL_TYPE_ID: null,
                            MC_COLOR_ID: null
                        }

                        var data = dataOri ? dataOri : Defdata;

                        $scope.gridSearchResultDs = new kendo.data.DataSource({
                            serverPaging: true,
                            serverFiltering: true,
                            pageSize: 20,
                            schema: {
                                data: "data",
                                total: "total",
                            },
                            transport: {
                                read: function (e) {
                                    var webapi = new kendo.data.transports.webapi({});
                                    var params = webapi.parameterMap(e.data);
                                    var url = "/api/Knit/FabProdKnitOrder/getFabOrderDataForSampleBatchProgram?pMC_BYR_ACC_ID=" + data.MC_BYR_ACC_ID;
                                    url += "&pMC_BYR_ACC_GRP_ID=" + data.MC_BYR_ACC_GRP_ID;
                                    url += "&pRF_FAB_PROD_CAT_ID_LST=1,3,5";
                                    url += "&pFIRSTDATE=" + data.FIRSTDATE;
                                    url += "&pLASTDATE=" + data.LASTDATE;
                                    url += "&pMC_FAB_PROD_ORD_H_ID=" + data.MC_FAB_PROD_ORD_H_IDD;
                                    url += "&pLK_COL_TYPE_ID=" + data.LK_COL_TYPE_ID;

                                    url += "&pMC_COLOR_ID=" + data.MC_COLOR_ID;

                                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                                    url += config.kFilterStr2QueryParam(params.filter);
                                    DyeingDataService.getDataByFullUrl(url).then(function (res) {
                                        e.success(res);
                                    });
                                }
                            },
                            //group: { field: "BYR_ACC_NAME_EN" }
                        });

                    }



                    $scope.gridSearchResultOpt = {
                        height: '600px',
                        scrollable: true,
                        pageable: {
                            refresh: true,
                            pageSizes: true,
                            buttonCount: 5
                        },
                        columns: [
                            { field: "BYR_ACC_NAME_EN", title: "ByrAcc", width: "40px" },
                            { field: "STYLE_NO", title: "Order/Style", width: "60px", template: "<span style=\"display: block;\">#=ORDER_NO_LIST #</span> <span style=\"display: block;font-style:italic;\"><small>#:STYLE_NO#</small><span>" },
                            { title: 'Image', template: '<img ng-click="openShowImageModal(dataItem.STYL_KEY_IMG)" data-ng-src="data:image/png;base64,#:data.STYL_KEY_IMG#" title="Click for Image Enlarge" alt="No Photo" style="border:1px solid black; width:45px" />', width: "60px" },

                            { field: "ORDER_NO_LIST_CON", hidden: true, title: "Order #", width: "100px", groupHeaderTemplate: " <b>  #= value # </b>" },
                            { field: "COLOR_NAME_EN", title: "Color Name", width: "40px" },
                            { field: "SHIP_DT", title: "Ship Date", type: "date", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'dd-MM-yyyy') #", width: "40px" },
                            { field: "FAB_TYPE_NAME", title: "Fab Type", width: "40px" },
                            { field: "FIB_COMP_NAME", title: "Fib Comp", width: "40px" },
                            { field: "FIN_GSM", title: "GSM", width: "20px" },
                            { field: "FIN_DIA", title: "DIA", width: "40px" },
                            { field: "MC_DIA_N_GG", title: "M/C Dia x GG", width: "30px" },
                            { field: "JOB_CRD_NO", title: "Code No", width: "40px" },
                            { field: "KNT_YRN_LOT_LST", title: "Yarn Info", width: "150px" },
                            //FAB_TYPE_SNAME,
                            { field: "STOCK_QTY", title: "Stock Qty", width: "30px" },
                            {
                                title: "Action", width: "35px",
                                template: function () {
                                    return '<a ng-click="OpenBatchModal(dataItem);" class="btn btn-xs blue"><i class="fa fa-edit"> Batch</i></a> ';
                                }
                            }
                        ],
                        change: function (e) {
                        },
                        selectable: true,
                    };


                    $scope.openShowImageModal = function (image) {
                        var modalInstance = $modal.open({
                            animation: true,
                            templateUrl: 'StyleItemImageStyleG.html',
                            controller: function ($scope, $modalInstance, image) {
                                $scope.image = image;
                                $scope.cancel = function (data) {
                                    $modalInstance.dismiss();
                                }

                            },
                            size: 'lg',
                            windowClass: 'large-Modal',
                            resolve: {
                                image: function () {
                                    return image;
                                }
                            }
                        });
                    }

                    function dateConvert(REF_DT) {

                        var _isudate = $filter('date')(REF_DT, $scope.dtFormat);
                        return _isudate;
                    }

                    $scope.SearchBatchData = function (dataOri) {

                        var Defdata = {
                            MC_BYR_ACC_ID: null,
                            MC_BYR_ACC_GRP_ID: null,
                            FROM_DATE: null,
                            TO_DATE: null,
                            MC_FAB_PROD_ORD_H_IDD: null,
                            LK_COL_TYPE_ID: null,
                            MC_COLOR_ID: null,
                            JOB_CRD_NO: null
                        }

                        var data = dataOri ? dataOri : Defdata;

                        $scope.gridBatchtDs = new kendo.data.DataSource({
                            serverPaging: true,
                            serverFiltering: true,
                            pageSize: 20,
                            schema: {
                                data: "data",
                                total: "total",
                            },
                            transport: {
                                read: function (e) {
                                    var webapi = new kendo.data.transports.webapi({});
                                    var params = webapi.parameterMap(e.data);
                                    var url = "/api/Knit/FabProdKnitOrder/getFabOrderDataForSampleBatchProgram?pMC_BYR_ACC_ID=" + data.MC_BYR_ACC_ID;
                                    url += "&pMC_BYR_ACC_GRP_ID=" + data.MC_BYR_ACC_GRP_ID;
                                    url += "&pRF_FAB_PROD_CAT_ID_LST=1,3,5";
                                    url += "&pFIRSTDATE=" + dateConvert(data.FROM_DATE);
                                    url += "&pLASTDATE=" + dateConvert(data.TO_DATE);
                                    url += "&pMC_FAB_PROD_ORD_H_ID=" + data.MC_FAB_PROD_ORD_H_IDD;
                                    //url += "&pLK_COL_TYPE_ID=" + data.LK_COL_TYPE_ID;
                                    url += "&pMC_COLOR_ID=" + data.MC_COLOR_ID;
                                    url += "&pJOB_CRD_NO=" + (data.JOB_CRD_NO || '');
                                    url += "&pOption=3015";
                                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                                    url += config.kFilterStr2QueryParam(params.filter);
                                    DyeingDataService.getDataByFullUrl(url).then(function (res) {
                                        e.success(res);
                                    });
                                }
                            },
                            group: { field: "DYE_BATCH_NO" }
                        });

                    }



                    $scope.gridBatchOpt = {
                        height: '600px',
                        scrollable: true,
                        pageable: {
                            refresh: true,
                            pageSizes: true,
                            buttonCount: 5
                        },
                        groupable: true,
                        columns: [
                            { field: "DYE_BATCH_NO", hidden: true, title: "Batch No", width: "10px" },
                            { field: "BYR_ACC_NAME_EN", title: "ByrAcc", width: "40px" },
                            { field: "STYLE_NO", title: "Order/Style", width: "60px", template: "<span style=\"display: block;\">#=ORDER_NO_LIST #</span> <span style=\"display: block;font-style:italic;\"><small>#:STYLE_NO#</small><span>" },
                            { field: "COLOR_NAME_EN", title: "Color Name", width: "40px" },
                            { field: "SHIP_DT", title: "Batch Date", type: "date", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'dd-MM-yyyy') #", width: "40px" },
                            { field: "STOCK_QTY", title: "B. Qty", width: "25px" },
                            { field: "ISSUED_QTY", title: "I. Qty", width: "25px" },

                            { field: "FAB_TYPE_NAME", title: "Fab Type", width: "40px" },
                            { field: "FIB_COMP_NAME", title: "Fib Comp", width: "40px" },
                            { field: "FIN_GSM", title: "GSM", width: "20px" },
                            { field: "FIN_DIA", title: "DIA", width: "40px" },
                            { field: "MC_DIA_N_GG", title: "M/C Dia x GG", width: "30px" },
                            { field: "JOB_CRD_NO", title: "Code No", width: "40px" },
                            { field: "KNT_YRN_LOT_LST", title: "Yarn Info", width: "130px" },
                            {
                                title: "Action", width: "35px",
                                template: function () {
                                    return '<a ng-click="OpenBatchModal(dataItem);" class="btn btn-xs blue"><i class="fa fa-eye"> View</i></a> ';
                                }
                            }
                        ],
                        change: function (e) {
                        },
                        selectable: true,
                    };


                    $scope.OpenBatchModal = function (item) {

                        item['DYE_MACHINE_ID'] = $scope.form.DYE_MACHINE_ID;
                        item['DYE_BATCH_SCDL_ID'] = $scope.form.DYE_BATCH_SCDL_ID;
                        item['END_DT'] = $scope.form.END_DT;
                        item['IS_FINALIZED'] = $scope.form.IS_FINALIZED;
                        item['PRE_BC_END_DT'] = $scope.form.PRE_BC_END_DT;
                        item['SCDL_NOTE'] = $scope.form.SCDL_NOTE;
                        item['SCDL_REF_NO'] = $scope.form.SCDL_REF_NO;
                        item['START_DT'] = $scope.form.START_DT;
                        item['IS_SMP_BLK'] = $scope.form.IS_SMP_BLK;
                        item['DYE_BATCH_PLAN_ID'] = -1;

                        DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID=null&pIS_SMP_BLK=S&pDYE_BATCH_SCDL_ID=' + (item.DYE_BATCH_SCDL_ID) + '&pDYE_MACHINE_ID=' + item.DYE_MACHINE_ID).then(function (res) {
                            if (res.length == 1) {
                                item['LOAD_TIME'] = res[0].LAST_EVT_END;
                            }

                            var modalInstance = $modal.open({
                                animation: true,
                                templateUrl: '/Dyeing/Dye/_DyeBatchProgramModal?ViewName=_DyeBatchProgramModalSample',
                                controller: 'DyeBatchProgramModalSampleController',
                                size: 'lg',
                                windowClass: 'app-modal-window',
                                resolve: {
                                    formData: function () {

                                        return item;
                                    },
                                    McDataDs: function () {
                                        return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID&pIS_SMP_BLK=S&pDYE_BATCH_SCDL_ID=' + ($stateParams.pDYE_BATCH_SCDL_ID) + '&pHR_COMPANY_ID=' + (vm.HR_COMPANY_ID || ''));
                                    }
                                }
                            });

                            modalInstance.result.then(function (dta) {
                                //getBatchProgramData(vm.DYE_BATCH_SCDL_ID, '', vm.HR_COMPANY_ID);
                            }, function () {
                                //getBatchProgramData(vm.DYE_BATCH_SCDL_ID, '', vm.HR_COMPANY_ID);
                            });
                        });

                    }

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    formData: function () {
                        return {

                            UN_LOAD_TIME: '',
                            DYE_BATCH_SCDL_ID: (vm.EditPlanData.DYE_BATCH_SCDL_ID || $stateParams.pDYE_BATCH_SCDL_ID),
                            START_DT: vm.EditPlanData.START_DT,
                            END_DT: vm.EditPlanData.END_DT,
                            DYE_BATCH_PLAN_ID: -1,
                            DURATION: 0,
                            RF_FAB_PROD_CAT_ID: 1,
                            IS_SMP_BLK: 'S',
                            FROM_DATE: '',
                            TO_DATE: '',
                            DYE_MACHINE_ID: item.DYE_MACHINE_ID,
                            IS_FINALIZED: item.IS_FINALIZED,
                        }
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                getBatchProgramData(vm.DYE_BATCH_SCDL_ID, '', vm.HR_COMPANY_ID);
                $state.go('DyeBatchFabReqSample', { pDYE_BATCH_SCDL_ID: dta.DYE_BATCH_SCDL_ID }, { reload: true });
            }, function () {
                getBatchProgramData(vm.DYE_BATCH_SCDL_ID, '', vm.HR_COMPANY_ID);
                console.log('Modal dismissed at: ' + new Date());
            });
        };



        function getBatchProgramData(pDYE_BATCH_SCDL_ID, pDYE_BATCH_NO, pHR_COMPANY_ID) {

            if (!pDYE_BATCH_SCDL_ID) return;

            return DyeingDataService.getDataByUrl('/DyeBatch/GetDataFabReqDyeBatch?pDYE_BATCH_SCDL_ID=' + pDYE_BATCH_SCDL_ID + '&pOption=3002&pIS_SMP_BLK=S&pDYE_BATCH_NO=' + (pDYE_BATCH_NO || '') + '&pHR_COMPANY_ID=' + (pHR_COMPANY_ID || null)).then(function (res) {
                vm.data = res.map(function (o) {
                    o['IS_OPEN'] = MC_OPEN.length > 0 ? _.some(MC_OPEN, function (oo) { return oo == o.DYE_MACHINE_ID }) : false;
                    return o;
                });
            });
        };

        vm.onRequirementClick = function (itm) {
            var url;

            if (!itm.hasOwnProperty('requirements')) {
                itm['requirements'] = [];
            }

            if (itm.IS_OPEN_REQ && itm.requirements.length == 0) {
                url = '/DyeBatch/GetDataFabReqCal?pMC_FAB_PROD_ORD_H_LST=' + itm.MC_FAB_PROD_ORD_H_ID + (itm.LNK_ORD_H_ID_LST ? (',' + itm.LNK_ORD_H_ID_LST) : '');
                url += '&pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID;
                url += '&pDYE_BATCH_SCDL_ID=' + itm.DYE_BATCH_SCDL_ID;
                url += '&pFAB_COLOR_ID=' + itm.FAB_COLOR_ID;
                url += '&pRF_FAB_TYPE_ID=' + itm.RF_FAB_TYPE_ID;
                url += '&pRQD_PRD_QTY=' + itm.RQD_PRD_QTY;

                return DyeingDataService.getDataByUrl(url).then(function (res) {

                    itm['requirements'] = res;
                    vm.disabledEdit = false;
                });
            }
        };

        vm.getDfProcessTypeData = function (itm) {
            if (!itm.hasOwnProperty('df_data')) {
                itm['df_data'] = [];
            }

            if (itm.IS_OPEN_DF_PRC && itm.df_data.length == 0) {
                return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDFProcessData?pDYE_BATCH_PLAN_ID=' + itm.DYE_BATCH_PLAN_ID).then(function (res) {
                    itm['df_data'] = res;
                });
            };
        };

        vm.cancel = function () {
            return getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID, '', vm.HR_COMPANY_ID);
        }

        $scope.$watch('DYE_BATCH_NO', function (newVal, oldVal) {
            if (newVal !== undefined && $stateParams.pDYE_BATCH_SCDL_ID !== undefined) {
                return getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID, newVal, vm.HR_COMPANY_ID);
            } else if (newVal !== undefined && $stateParams.pDYE_BATCH_SCDL_ID === undefined) {
                return getBatchProgramData(null, newVal, vm.HR_COMPANY_ID);
            }
        });


        vm.submitData = function (dataOri) {

            var data2save = [];
            MC_OPEN = [];
            var DfProcessData = [];
            angular.forEach(dataOri, function (val0, key0) {
                angular.forEach(val0.dyePrograms, function (val1, key1) {

                    if (val1.IS_SELECTED_BT) {

                        if (MC_OPEN.indexOf(val0.DYE_MACHINE_ID) < 0) {
                            MC_OPEN.push(val0.DYE_MACHINE_ID);
                        }

                        if (val1.df_data && val1.df_data.length > 0) {
                            var DfDataD = [];

                            angular.forEach(val1.df_data, function (val, key) {
                                angular.forEach(val.items, function (v, k) {
                                    if (v.IS_SELECTED == "Y") {
                                        DfDataD.push({
                                            DYE_BT_RQD_PROC_ID: v.DYE_BT_RQD_PROC_ID,
                                            DF_PROC_TYPE_ID: v.DF_PROC_TYPE_ID
                                        });
                                    }
                                })
                            });

                            DfProcessData.push({
                                DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                                DF_XML_D: config.xmlStringShortNoTag(DfDataD)
                            });
                        }


                        angular.forEach(val1.df_data, function (val, key) {
                            angular.forEach(val.items, function (v, k) {
                                if (v.IS_SELECTED == "Y") {
                                    DfDataD.push({
                                        DYE_BT_RQD_PROC_ID: v.DYE_BT_RQD_PROC_ID,
                                        DF_PROC_TYPE_ID: v.DF_PROC_TYPE_ID
                                    });
                                }
                            })
                        });

                        var child = [];

                        angular.forEach(val1.requirements, function (oo) {

                            data2save.push({
                                DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                                IS_LOT_MIX: (val1.IS_LOT_MIX || 'N'),
                                ACT_LOAD_TIME: val1.LOAD_TIME,
                                ACT_UN_LOAD_TIME: val1.UN_LOAD_TIME,
                                FAB_COLOR_ID: val1.FAB_COLOR_ID,
                                LK_DYE_MTHD_ID: val1.LK_DYE_MTHD_ID,
                                MC_STYLE_D_FAB_ID: val1.MC_STYLE_D_FAB_ID,
                                IS_BATCH_STORE: val1.IS_BATCH_STORE,

                                INV_ITEM_CAT_ID: oo.INV_ITEM_CAT_ID,

                                DYE_BATCH_NO: _.filter(val1.requirements, function (x) { return x.DYE_BATCH_NO.length > 0 && x.MC_FAB_PROD_ORD_H_ID == oo.MC_FAB_PROD_ORD_H_ID })[0].DYE_BATCH_NO,
                                DYE_LOT_NO: _.filter(val1.requirements, function (x) { return x.DYE_LOT_NO.length > 0 && x.MC_FAB_PROD_ORD_H_ID == oo.MC_FAB_PROD_ORD_H_ID })[0].DYE_LOT_NO,

                                BT_TOTAL: _.filter(val1.requirements, function (x) { return x.BT_TOTAL > 0 && x.MC_FAB_PROD_ORD_H_ID == oo.MC_FAB_PROD_ORD_H_ID })[0].BT_TOTAL,


                                ACT_BATCH_QTY: oo.INV_ITEM_CAT_ID == 34 ? oo.ACT_BATCH_QTY : (oo.RQD_QTY_KG || 0),

                                LK_DIA_TYPE_ID: (oo.LK_DIA_TYPE_ID || ''),
                                RF_FAB_TYPE_ID: (oo.RF_FAB_TYPE_ID || ''),
                                RF_FIB_COMP_ID: (oo.RF_FIB_COMP_ID || ''),
                                RQD_GSM: (oo.RQD_GSM || ''),

                                MC_FAB_PROD_ORD_H_ID: oo.MC_FAB_PROD_ORD_H_ID,
                                DYE_BT_CARD_H_ID: oo.DYE_BT_CARD_H_ID,
                                LK_FBR_GRP_ID: oo.LK_FBR_GRP_ID,
                                DYE_BT_CARD_GRP_ID: oo.DYE_BT_CARD_GRP_ID,
                                INV_ORD_GFAB_STK_ID: oo.INV_ORD_GFAB_STK_ID,

                                DYE_BT_CARD_D_TRMS_ID: (oo.DYE_BT_CARD_D_TRMS_ID || ''),
                                RQD_QTY_YDS: (oo.RQD_QTY_YDS || 0),
                                RQD_QTY_KG: (oo.RQD_QTY_KG || 0),

                                NON_COL_CUF: config.xmlStringShortNoTag(oo.non_col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                    return {
                                        DYE_BT_CARD_D_FAB_ID: o.DYE_BT_CARD_D_FAB_ID,
                                        KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID,
                                        RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                        RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                                        LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                                        IS_BATCH_STORE: o.IS_BATCH_STORE,
                                        RQD_QTY: o.RCV_ROLL_WT,
                                        QTY_MOU_ID: 3,
                                        ASSIGN_QTY: o.ASSIGN_QTY
                                    }
                                })),
                                COL_CUF: config.xmlStringShortNoTag(oo.col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                    return {
                                        KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID,
                                        DYE_BT_CARD_D_CLCF_ID: o.DYE_BT_CARD_D_CLCF_ID,
                                        RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                        RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                                        LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                                        PRD_QTY: o.PRD_QTY,
                                        QTY_MOU_ID: 1,
                                        ASSIGN_QTY: o.ASSIGN_QTY
                                    }
                                })),

                                TRIMS_ACC: config.xmlStringShortNoTag(oo.woven_trims_avai.filter(function (f) { return (f.RQD_QTY_YDS || 0) > 0 }).map(function (o) {
                                    return {
                                        MC_ORD_TRMS_ITEM_ID: o.MC_ORD_TRMS_ITEM_ID,
                                        DYE_BT_CARD_D_TRMS_ID: o.DYE_BT_CARD_D_TRMS_ID,
                                        DYE_BT_CARD_H_ID: (oo.DYE_BT_CARD_H_ID || ''),
                                        RQD_QTY_YDS: (o.RQD_QTY_YDS || 0),
                                        RQD_QTY_KG: (oo.RQD_QTY_KG || 0),
                                    }
                                })),

                                CHILD: config.xmlStringShortNoTag(child.map(function (ob) {


                                    return {
                                        DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                                        ACT_LOAD_TIME: val1.LOAD_TIME,
                                        ACT_UN_LOAD_TIME: val1.UN_LOAD_TIME,
                                        FAB_COLOR_ID: val1.FAB_COLOR_ID,
                                        LK_DYE_MTHD_ID: val1.LK_DYE_MTHD_ID,
                                        MC_STYLE_D_FAB_ID: val1.MC_STYLE_D_FAB_ID,
                                        INV_ITEM_CAT_ID: ob.INV_ITEM_CAT_ID,

                                        LK_DIA_TYPE_ID: (ob.LK_DIA_TYPE_ID || ''),
                                        RQD_GSM: (ob.RQD_GSM || ''),

                                        ACT_BATCH_QTY: ob.INV_ITEM_CAT_ID == 34 ? ob.ACT_BATCH_QTY : (ob.RQD_QTY_KG || 0),

                                        DYE_BATCH_NO: _.filter(child, function (x) { return x.DYE_BATCH_NO.length > 0 })[0].DYE_BATCH_NO,
                                        DYE_LOT_NO: _.filter(child, function (x) { return x.DYE_LOT_NO.length > 0 })[0].DYE_LOT_NO,

                                        MC_FAB_PROD_ORD_H_ID: ob.MC_FAB_PROD_ORD_H_ID,
                                        DYE_BT_CARD_H_ID: ob.DYE_BT_CARD_H_ID,
                                        LK_FBR_GRP_ID: (ob.LK_FBR_GRP_ID || ''),
                                        RF_FAB_TYPE_ID: ob.RF_FAB_TYPE_ID,
                                        RF_FIB_COMP_ID: ob.RF_FIB_COMP_ID,
                                        IS_BATCH_STORE: ob.IS_BATCH_STORE,
                                        DYE_BT_CARD_GRP_ID: (ob.DYE_BT_CARD_GRP_ID || ''),

                                        DYE_BT_CARD_D_TRMS_ID: (ob.DYE_BT_CARD_D_TRMS_ID || ''),
                                        RQD_QTY_YDS: (ob.RQD_QTY_YDS || 0),
                                        RQD_QTY_KG: (ob.RQD_QTY_KG || 0),
                                        INV_ORD_GFAB_STK_ID: ob.INV_ORD_GFAB_STK_ID,

                                        NON_COL_CUF: config.xmlStringShortNoTagChild(ob.non_col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                            return {
                                                DYE_BT_CARD_D_FAB_ID: o.DYE_BT_CARD_D_FAB_ID,
                                                KNT_STYL_FAB_ITEM_ID: o.KNT_STYL_FAB_ITEM_ID,
                                                RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                                RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                                                LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                                                RQD_QTY: o.RCV_ROLL_WT,
                                                QTY_MOU_ID: 3,
                                                ASSIGN_QTY: o.ASSIGN_QTY
                                            }
                                        })),
                                        COL_CUF: config.xmlStringShortNoTagChild(ob.col_cu_avail.filter(function (f) { return (f.ASSIGN_QTY || 0) > 0 }).map(function (o) {
                                            return {
                                                KNT_CLCF_STYL_ITEM_ID: o.KNT_CLCF_STYL_ITEM_ID,
                                                DYE_BT_CARD_D_CLCF_ID: o.DYE_BT_CARD_D_CLCF_ID,
                                                RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                                RF_FIB_COMP_ID: o.RF_FIB_COMP_ID,
                                                LK_DIA_TYPE_ID: o.LK_DIA_TYPE_ID,
                                                PRD_QTY: o.PRD_QTY,
                                                QTY_MOU_ID: 1,
                                                ASSIGN_QTY: o.ASSIGN_QTY
                                            }
                                        }))


                                    }

                                }))

                            });
                            // }

                        });


                    }


                });


            });

            if (data2save.length == 0) {
                return;
            } else {
                Dialog.confirm('You are updating Batch data...<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return DyeingDataService.saveDataByUrl({
                         XML: config.xmlStringShort(data2save),
                         XML_DF: config.xmlStringShort(DfProcessData)
                     }, '/DyeBatch/Save').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID);
                             }
                             return config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
            }




        }

        vm.createRequisition = function (dataOri) {

            console.log(dataOri);

            var data2save = [];
            angular.forEach(dataOri, function (val0, key0) {
                angular.forEach(val0.dyePrograms, function (val1, key1) {
                    if (val1.IS_SELECTED_BT) {
                        if (data2save.indexOf({
                            DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID
                        }) < 0) {
                            data2save.push({
                                DYE_BATCH_PLAN_ID: val1.DYE_BATCH_PLAN_ID,
                                INV_ITEM_CAT_ID: (val0.pINV_ITEM_CAT_ID || 34)
                            });
                        }
                    }
                });


            });

            if (data2save.length == 0) {
                return;
            } else {
                Dialog.confirm('Creating Grey Fabric Req for Batch?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return DyeingDataService.saveDataByUrl({
                         XML: config.xmlStringShort(data2save),
                         RF_REQ_TYPE_ID: 17,// 16 bulk;17 Sample
                         PREFIX: 'RS-',
                         RF_ACTN_TYPE_ID: 6, // 6 Sample,5 Bulk
                         HR_COMPANY_ID: vm.HR_COMPANY_ID
                     }, '/DyeBatch/CreateRequisition').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID);
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
            }
        }

        vm.printPendingLabdipRecepi = function (data) {
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/dye/dyereport/previewreportrdlc');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-4022' }, data);

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

        function printBatchProgam(data) {
            console.log(data);
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/dye/dyereport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-4024' }, data);
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


        function printSampleBatch(data) {
            console.log(data);
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/dye/dyereport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-4046' }, data);
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

        vm.remove = function (data) {
            Dialog.confirm('Removing Program ID# <b>' + data.DYE_BATCH_PLAN_ID + '</b><br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {

                     MC_OPEN = [];
                     MC_OPEN.push(data.DYE_MACHINE_ID);
                     return DyeingDataService.saveDataByUrl({
                         DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID
                     }, '/DyeBatch/Delete').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 getBatchProgramData($stateParams.pDYE_BATCH_SCDL_ID, '', vm.HR_COMPANY_ID);
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });
                 });
        }

        vm.printBatchProgam = function (IS_EXCEL_FORMAT) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'StartEndDateModal.html',
                controller: function ($scope, $modalInstance) {
                    $scope.showSplash = true;
                    activate();

                    $scope.today = new Date();

                    var y = $scope.today.getFullYear(), m = $scope.today.getMonth();
                    var firstDay = new Date(y, m, 1);
                    $scope.dtFormat = config.appDateFormat;
                    $scope.formInvalid = false;

                    $scope.dateOptions = {
                        formatYear: 'yy',
                        startingDay: 6
                    };

                    $scope.form = {
                        FROM_DATE: firstDay,
                        TO_DATE: $scope.today,
                    };

                    $scope.RevisionDate_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.RevisionDate_LNopened = true;
                    }
                    $scope.RevisionDate_LNopen2 = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.RevisionDate_LNopened2 = true;
                    }


                    $scope.save = function (token, valid) {
                        if (!valid) return;
                        $modalInstance.close($scope.form);
                    };


                    $scope.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };
                },
                size: 'small',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {
                printBatchProgam({
                    FROM_DATE: $filter('date')(data.FROM_DATE),
                    TO_DATE: $filter('date')(data.TO_DATE),
                    IS_EXCEL_FORMAT: IS_EXCEL_FORMAT,
                    HR_COMPANY_ID: vm.HR_COMPANY_ID
                });
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };


        vm.printSampleBatch = function (IS_EXCEL_FORMAT) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'StartEndDateModal.html',
                controller: function ($scope, $modalInstance) {
                    $scope.showSplash = true;
                    activate();

                    $scope.today = new Date();

                    var y = $scope.today.getFullYear(), m = $scope.today.getMonth();
                    var firstDay = new Date(y, m, 1);
                    $scope.dtFormat = config.appDateFormat;
                    $scope.formInvalid = false;

                    $scope.dateOptions = {
                        formatYear: 'yy',
                        startingDay: 6
                    };

                    $scope.form = {
                        FROM_DATE: firstDay,
                        TO_DATE: $scope.today,
                    };

                    $scope.RevisionDate_LNopen = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.RevisionDate_LNopened = true;
                    }
                    $scope.RevisionDate_LNopen2 = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        $scope.RevisionDate_LNopened2 = true;
                    }


                    $scope.save = function (token, valid) {
                        if (!valid) return;
                        $modalInstance.close($scope.form);
                    };


                    $scope.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };
                },
                size: 'small',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {
                printSampleBatch({
                    FROM_DATE: $filter('date')(data.FROM_DATE),
                    TO_DATE: $filter('date')(data.TO_DATE),
                    IS_EXCEL_FORMAT: IS_EXCEL_FORMAT,
                    HR_COMPANY_ID: vm.HR_COMPANY_ID
                });
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

    }



})();