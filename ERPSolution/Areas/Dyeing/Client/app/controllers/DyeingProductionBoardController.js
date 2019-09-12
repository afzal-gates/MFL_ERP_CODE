(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeingProductionBoardController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', 'commonDataService', 'cur_user_id', 'Dialog', '$filter', '$modal', DyeingProductionBoardController]);
    function DyeingProductionBoardController($q, config, DyeingDataService, $stateParams, $state, $scope, commonDataService, cur_user_id, Dialog, $filter, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.DateTimeFormat = config.appDateTimeFormat;
        vm.TimeFormat = config.appTimeFormat;

        vm.today = new Date();
        //vm.form = {};

        $scope.datePickerOptions = {
            parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
        };

        vm.form = { REPROCESS_FLAG: '3000' };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getDiaTypeList(), GetMachineList(), getColorList(0, 1, 0), getFinProcessList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.getTotal = function (items) {

            var total = 0;
            if (items != null)
                for (var i = 0; i < items.length; i++) {
                    total = total + parseFloat(items[i].BATCH_QTY);
                }
            return total;
        }

        function getColorList(pMC_FAB_PROD_ORD_H_ID, pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID) {

            vm.colorListDS = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/ColourMaster/GetColorListAfterDyeByID?pMC_FAB_PROD_ORD_H_ID=" + (vm.form.MC_FAB_PROD_ORD_H_ID || 0);
                        url += "&pMC_BYR_ACC_GRP_ID=" + (vm.form.MC_BYR_ACC_GRP_ID || 0);
                        url += "&pMC_STYLE_H_EXT_ID=" + (vm.form.MC_STYLE_H_EXT_ID || 0);

                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            }
        };

        function getFinProcessList() {

            return vm.finProcessList = {
                optionLabel: "-- Select Finishing Process --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/Dye/DFProcessType/SelectAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "DF_PROC_TYPE_NAME",
                dataValueField: "DF_PROC_TYPE_ID"
            };
        };
        
        function getDiaTypeList() {

            return vm.diaTypeList = {
                optionLabel: "-- Select Dia --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.LookupListData(67).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        vm.productionTypeList = {
            optionLabel: "-- Select Type --",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                            e.success(_.findByValues(res, 'RF_FAB_PROD_CAT_ID', '1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17'));
                        });
                    }
                }
            },
            change: function (e) {
                var itmTyp = this.dataItem(e.item);

                if (itmTyp.RF_FAB_PROD_CAT_ID) {
                    getSelectMonth(null, itmTyp.RF_FAB_PROD_CAT_ID);
                    getStyleExtList(null, null, null, null)
                    getFabOederByOh(null, itmTyp.RF_FAB_PROD_CAT_ID, null, null, '')
                } else {
                    getSelectMonth(0, 0);
                    getFabOederByOh(null, null, null, null, '');
                    getStyleExtList(null, null, null, null);
                }
            },
            dataTextField: "FAB_PROD_CAT_SNAME",
            dataValueField: "RF_FAB_PROD_CAT_ID"
        };



        function getStyleExtList(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE) {

            vm.StyleExtDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=" + (vm.form.RF_FAB_PROD_CAT_ID || '');
                        url += "&pFIRSTDATE=" + (pFIRSTDATE || null);
                        url += "&pLASTDATE=" + (pLASTDATE || null);

                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '&pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '&pSTYLE_NO';
                        }
                        return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true
            }
        };


        function getFabOederByOh(pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID) {
            vm.FabOederByOhDs = {
                transport: {
                    read: function (e) {
                        var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_GRP_ID=" + pMC_BYR_ACC_GRP_ID;
                        url += "&pRF_FAB_PROD_CAT_LST=" + (vm.form.RF_FAB_PROD_CAT_ID || '');
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

        function getSelectMonth(MC_BYR_ACC_GRP_ID, RF_FAB_PROD_CAT_ID) {
            return DyeingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/SelectShipmentMonth/' + (MC_BYR_ACC_GRP_ID || 0) + '/0/' + (RF_FAB_PROD_CAT_ID || 0)).then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        };

        vm.onChangeShipMonth = function (e) {
            var itmShipMonth = e.sender.dataItem(e.sender.item);

            if (itmShipMonth.MONTHOF) {
                vm.form.FIRSTDATE = itmShipMonth.FIRSTDATE.split('T')[0];
                vm.form.LASTDATE = itmShipMonth.LASTDATE.split('T')[0];
                getStyleExtList((vm.form.MC_BYR_ACC_GRP_ID || null), (vm.form.RF_FAB_PROD_CAT_ID || null), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE)
                getFabOederByOh(vm.form.MC_BYR_ACC_GRP_ID, (vm.form.RF_FAB_PROD_CAT_ID || null), itmShipMonth.FIRSTDATE, itmShipMonth.LASTDATE, '')


            } else {
                getStyleExtList((vm.form.MC_BYR_ACC_GRP_ID || null), (vm.form.RF_FAB_PROD_CAT_ID || null), null, null);
                vm.form.FIRSTDATE = null;
                vm.form.LASTDATE = null;
                getFabOederByOh(vm.form.MC_BYR_ACC_GRP_ID, (vm.form.RF_FAB_PROD_CAT_ID || null), null, null, '')
            }
        };


        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.FAB_PROD_CAT_SNAME||"" #</p></span>';
        $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';

        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.FAB_PROD_CAT_SNAME||"" #</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.FAB_PROD_CAT_SNAME||"" #)</h5></span>';


        vm.buyerAcGrpList = {
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
                    getColorList(vm.form.MC_FAB_PROD_ORD_H_ID, item.MC_BYR_ACC_GRP_ID, 0);
                }
            },
            dataTextField: "BYR_ACC_GRP_NAME_EN",
            dataValueField: "MC_BYR_ACC_GRP_ID"
        };



        function GetMachineList() {

            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectRunningBatch?pOption=3000').then(function (res) {
                vm.DyeingMCRunList = res
            });

        };

        vm.loadProcessData = function (pOption) {
            console.log(pOption);
            vm.form.REPROCESS_FLAG = pOption;
            //return;
            vm.showSplash = true;
            DyeingDataService.getDataByFullUrl('/api/Dye/DFProduction/SelectRunningBatch?pMC_BYR_ACC_GRP_ID=' + (vm.form.MC_BYR_ACC_GRP_ID || null) + '&pMC_FAB_PROD_ORD_H_ID=' + (vm.form.MC_FAB_PROD_ORD_H_ID || null) + '&pLK_DIA_TYPE_ID=' + (vm.form.LK_DIA_TYPE_ID || null) + '&pMONTHOF=' + (vm.form.MONTHOF || '') + '&pOption=' + (pOption || 3001) + '&pMC_COLOR_ID=' + (vm.form.MC_COLOR_ID || null) + '&pDF_PROC_TYPE_ID=' + (vm.form.DF_PROC_TYPE_ID || null)).then(function (res) {
                vm.DyeingMCRunList = res;
                vm.showSplash = false;

            });
        }


        vm.PrintProcessData = function (pOption) {
            console.log(pOption);
            vm.form.REPROCESS_FLAG = pOption;
            vm.form.REPORT_CODE = 'RPT-6013';

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

        vm.resetItemData = function () {
            vm.formItem = { QTY_MOU_ID: '3', CENTRAL_STK: 0, SUB_STK: 0 };
        };

        vm.reset = function () {

            $state.go($state.current, { pDYE_STR_REQ_H_ID: 0 }, { inherit: false, reload: true });
        };


        vm.openMachineUI = function () {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Dyeing/Dye/_DyeMachineStatusModal',
                controller: 'dyeMachineStatusModalController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'app-modal-window'
            });
            modalInstance.result.then(function (dataItem) {

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }



    }


})();

