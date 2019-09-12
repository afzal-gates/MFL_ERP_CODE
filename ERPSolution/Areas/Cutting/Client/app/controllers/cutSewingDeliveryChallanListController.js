////////// Start CutDeliveryChallan Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutSewingDeliveryChallanController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', '$modal', CutSewingDeliveryChallanController]);
    function CutSewingDeliveryChallanController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog, $modal) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.autoFocus= false;

        activate();
        function activate() {
            var promise = [getLineData()];
            return $q.all(promise).then(function () {
                
                vm.showSplash = false;                
            });
        }


        $scope.onFocused = function () {
            if ($stateParams.pGMT_CUT_SEW_DLV_CHL_ID) {
                $scope.challan = {};
                $scope.BundleDatas = {
                    datas: []
                };
                $state.go('DeliveryChallan', {}, { inherit: false });
            } 
        };


        if ($stateParams.pGMT_CUT_SEW_DLV_CHL_ID) {
             CuttingDataService.getDataByUrl('/GmtCutBank/getSewingDeliveryAutoCompl?pCHALAN_NO&pGMT_CUT_SEW_DLV_CHL_ID=' + $stateParams.pGMT_CUT_SEW_DLV_CHL_ID).
            then(function (res) {
                if (res.length > 0) {
                    $scope.challan = res[0];
                    getSewingDelChallanSumData(res[0].GMT_CUT_SEW_DLV_CHL_ID);
                }

            });
        } 

        $scope.sewDelChallanAuto = function (q) {
            return CuttingDataService.getDataByUrl('/GmtCutBank/getSewingDeliveryAutoCompl?pCHALAN_NO='+q);
        }

        function getSewingDelChallanSumData(pGMT_CUT_SEW_DLV_CHL_ID) {
            return CuttingDataService.getDataByUrl('/GmtCutBank/getSewingDeliveryChallan?pGMT_CUT_SEW_DLV_CHL_ID=' + pGMT_CUT_SEW_DLV_CHL_ID).then(function (res) {
                $scope.BundleDatas = {
                    datas: res,
                    total_bundles: _.sumBy(res, function (o) { return o.TTL_NO_OF_BNDL_SCANNED; }),
                    total_cut_qty: _.sumBy(res, function (o) { return o.TTL_CUTTING_QTY; }),
                    total_qty: _.sumBy(res, function (o) { return o.TTL_FINAL_QTY; }),
                    GMT_CUT_PNL_BNK_LST: res.map(function (o) { return o.GMT_CUT_PNL_BNK_LST; }).join(',')
                };
            });
        }

        vm.onSubmit = function (data) {
            if (!data)
                return;
            $scope.challan = Object.assign({}, data);
            getSewingDelChallanSumData(data.GMT_CUT_SEW_DLV_CHL_ID);
        };

        function getLineData() {
            return CuttingDataService.getDataByFullUrl('/api/common/GetSewingLineData').then(function (res) {
                $scope.DsLine = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        $scope.submitData = function (data, valid) {
            if (!valid) {
                return;
            }


            return CuttingDataService.saveDataByUrl({
                GMT_CUT_SEW_DLV_CHL_ID: data.GMT_CUT_SEW_DLV_CHL_ID,
                HR_PROD_LINE_ID: data.HR_PROD_LINE_ID,
                IS_TAG: data.IS_TAG,
                NO_OF_BAG: data.NO_OF_BAG,
                IS_FINALIZED: 'N',
                GMT_CUT_PNL_BNK_LST: $scope.BundleDatas.GMT_CUT_PNL_BNK_LST,
                pOption: 1000

            }, '/GmtCutBank/SaveSewingDelChallan').then(function (res) {
                if (res.success === false) {
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            })

        }

        $scope.FinalizeAndDeliver = function (data, valid) {
            if (!valid) {
                return;
            }

            if (parseInt(data.GMT_CUT_SEW_DLV_CHL_ID) > 1 && parseInt(data.HR_PROD_LINE_ID) > 0) {

                Dialog.confirm('<h5 style="margin:0px;">Finalizing and Delivering Challan...</h5', 'Are you sure?', ['Yes', ' No'])
                        .then(function () {
                            return CuttingDataService.saveDataByUrl({
                                GMT_CUT_SEW_DLV_CHL_ID: data.GMT_CUT_SEW_DLV_CHL_ID,
                                HR_PROD_LINE_ID: data.HR_PROD_LINE_ID,
                                IS_TAG: data.IS_TAG,
                                NO_OF_BAG: data.NO_OF_BAG,
                                IS_FINALIZED: 'Y',
                                GMT_CUT_PNL_BNK_LST: $scope.BundleDatas.GMT_CUT_PNL_BNK_LST,
                                pOption: 1001
                            }, '/GmtCutBank/SaveSewingDelChallan').then(function (res) {
                                if (res.success === false) {
                                }
                                else {
                                    res['data'] = angular.fromJson(res.jsonStr);
                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                        $scope.challan['IS_FINALIZED'] = 'Y';
                                    }
                                    config.appToastMsg(res.data.PMSG);
                                }
                            })
                        });


            } else {
                config.appToastMsg('MULTI-002Please Complete Challan Data Properly');
            }


        }

        vm.printDelivChallan = function () {

            var url = '/api/Cutting/CutReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            $scope.challan.REPORT_CODE = 'RPT-4514';
            //vm.form.GMT_CUT_INFO_ID = 3;

            //vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            //vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);

            var params = angular.copy($scope.challan);

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

        vm.printBundleChart = function () {

            var url = '/api/Cutting/CutReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            $scope.challan.REPORT_CODE = 'RPT-4522';
            //vm.form.GMT_CUT_INFO_ID = 3;

            //vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            //vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);

            var params = angular.copy($scope.challan);

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

        vm.openBundleStatusModal = function (cuttingData) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_BundleStatusModal',
                controller: 'BundleStatusModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    bndlData: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrl('/GmtCutBank/GetData4BndlStatus?pGMT_CUT_INFO_ID=' + cuttingData.GMT_CUT_INFO_ID).then(function (res) {
                            return res;
                        });
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                console.log(dta);
            });
        }

        vm.openDeliveryChallanSearch = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_DeliveryChallanSewSearchModal',
                controller: 'DeliveryChallanSearchModalController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'app-modal-window'
            });

            modalInstance.result.then(function (data) {
                console.log(data);

                $state.go('DeliveryChallan', { pGMT_CUT_SEW_DLV_CHL_ID: data.GMT_CUT_SEW_DLV_CHL_ID }, {reload: true});
            });
        }


        };
})();
////////// End CutPanelInspection Controller










// Start CutSewDelvChallanController Modal Controller
(function () {
    'use strict';

    angular.module('multitex.cutting').controller('DeliveryChallanSearchModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'CuttingDataService', 'Dialog', DeliveryChallanSearchModalController]);
    function DeliveryChallanSearchModalController($modalInstance, $q, $scope, $http, $state, $filter, config, CuttingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.autoFocus = false;

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.ORDER_NO #</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';

        vm.bagTagList = new kendo.data.DataSource({
            data: [{ IS_TAG: 'Y', TAG_NAME: 'Yes' }, { IS_TAG: 'N', TAG_NAME: 'No' }]
        });
        vm.statusList = new kendo.data.DataSource({
            data: [{ IS_FINALIZED: 'Y', STATUS_NAME: 'Delivered' }, { IS_FINALIZED: 'N', STATUS_NAME: 'Ready for Input' }]
        });

        
        vm.form = {};

        activate();
        
       
        function activate() {
            var promise = [getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getLineData()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }
            
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        vm.chlnDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.chlnDateOpened = true;
        };

        function getBuyerAcGrpList() {

            return vm.buyerAcGrpList = {
                optionLabel: "--- Select Buyer Group ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CuttingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/GetBuyerAccGrpList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.styleExtDataSource.read();

                },
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID"
            };
        }

        function getByrAccWiseStyleExtList() {
            vm.styleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ORDER_NO",
                dataValueField: "MC_ORDER_H_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;
                    vm.orderColorListDataSource.read();

                }
            };

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/common/getOrderStyleDropDownDataGmt?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : '');
                        url += CuttingDataService.kFilterStr2QueryParam(params.filter);
                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.orderColorListDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    return CuttingDataService.getDataByFullUrl('/api/common/getOrderColorDataForGmt?pMC_ORDER_H_ID=' + (vm.form.MC_ORDER_H_ID || 0)).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

        function getLineData() {

            return vm.lineList = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CuttingDataService.getDataByFullUrl('/api/common/GetSewingLineData').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },                
                dataTextField: "LINE_CODE",
                dataValueField: "HR_PROD_LINE_ID"
            };
        }

        $scope.challanAuto = function (val) {

            return CuttingDataService.getDataByUrl('/GmtCutBank/getSewingDeliveryAutoCompl?pCHALAN_NO=' + val).then(function (res) {
                console.log(res);

                return res;
            });

        };

        $scope.onSelectItem = function (item) {
            //console.log(item);
            vm.form.GMT_CUT_SEW_DLV_CHL_ID = item.GMT_CUT_SEW_DLV_CHL_ID;
        }

        $scope.$watch('vm.form.CHALAN_NO', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.GMT_CUT_SEW_DLV_CHL_ID = null;
            }

        });

        vm.challanList = [];
        vm.TTL_FINAL_QTY = 0;
        vm.TTL_NO_OF_BNDL_SCANNED = 0;
        vm.searchChallan = function () {
            return CuttingDataService.getDataByFullUrl('/api/Cutting/GmtCutBank/GetSewDelvChallanList?pGMT_CUT_SEW_DLV_CHL_ID=' + vm.form.GMT_CUT_SEW_DLV_CHL_ID + '&pCHALAN_DT=' + $filter('date')(vm.form.CHALAN_DT, vm.dtFormat) +
                '&pMC_BYR_ACC_GRP_ID=' + vm.form.MC_BYR_ACC_GRP_ID + '&pMC_ORDER_H_ID=' + vm.form.MC_ORDER_H_ID + '&pMC_COLOR_ID=' + vm.form.MC_COLOR_ID + 
                '&pHR_PROD_LINE_ID=' + vm.form.HR_PROD_LINE_ID + '&pIS_TAG=' + (vm.form.IS_TAG||'') + '&pIS_FINALIZED=' + (vm.form.IS_FINALIZED||'')).then(function (res) {

                    vm.challanList = res;
                    vm.TTL_FINAL_QTY = _.sumBy(res, function (o) { return o.FINAL_QTY; });
                    vm.TTL_NO_OF_BNDL_SCANNED = _.sumBy(res, function (o) { return o.NO_OF_BNDL_SCANNED; });
            });
        }

        vm.selectChallan = function (data) {
            $modalInstance.close(data);
        }

    }

})();
// End CutSewDelvChallanController Modal Controller












////////// Start CutSewDelvChaln4SplitController Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutSewDelvChaln4SplitController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', '$modal', CutSewDelvChaln4SplitController]);
    function CutSewDelvChaln4SplitController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, CuttingDataService, Dialog, $modal) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.autoFocus = false;

        activate();
        function activate() {
            var promise = [getLineData()];
            return $q.all(promise).then(function () {

                vm.showSplash = false;
            });
        }

        
        if ($stateParams.pGMT_CUT_SEW_DLV_CHL_ID) {
            CuttingDataService.getDataByUrl('/GmtCutBank/getSewingDeliveryAutoCompl?pCHALAN_NO&pGMT_CUT_SEW_DLV_CHL_ID=' + $stateParams.pGMT_CUT_SEW_DLV_CHL_ID).
           then(function (res) {
               if (res.length > 0) {
                   $scope.challan = res[0];

                   $scope.challan.FRM_CHALAN_NO = res[0].CHALAN_NO;
                   $scope.challan.CHALAN_NO = null;
                   $scope.challan.IS_FINALIZED = 'N';
                   $scope.challan.NEW_GMT_CUT_SEW_DLV_CHL_ID = 0;

                   getSewingDelChallanSumData(res[0].GMT_CUT_SEW_DLV_CHL_ID);
               }

           });
        }

       
        function getSewingDelChallanSumData(pGMT_CUT_SEW_DLV_CHL_ID) {
            return CuttingDataService.getDataByUrl('/GmtCutBank/getSewingDeliveryChallan?pGMT_CUT_SEW_DLV_CHL_ID=' + pGMT_CUT_SEW_DLV_CHL_ID).then(function (res) {

                angular.forEach(res, function (val, key) {
                    angular.forEach(val['details'], function (val1, key1) {
                        val1['IS_SPLIT'] = 'N';
                    });
                });

                $scope.BundleDatas = {
                    datas: res,
                    total_bundles: _.sumBy(res, function (o) { return o.TTL_NO_OF_BNDL_SCANNED; }),
                    total_cut_qty: _.sumBy(res, function (o) { return o.TTL_CUTTING_QTY; }),
                    total_qty: _.sumBy(res, function (o) { return o.TTL_FINAL_QTY; }),
                    GMT_CUT_PNL_BNK_LST: res.map(function (o) { return o.GMT_CUT_PNL_BNK_LST; }).join(',')
                };
            });
        }

        vm.onSubmit = function (data) {
            if (!data)
                return;
            $scope.challan = Object.assign({}, data);
            getSewingDelChallanSumData(data.GMT_CUT_SEW_DLV_CHL_ID);
        };

        function getLineData() {
            return CuttingDataService.getDataByFullUrl('/api/common/GetSewingLineData').then(function (res) {
                $scope.DsLine = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        $scope.submitData = function (data, valid) {
            if (!valid) {
                return;
            }

            
            var vCutPnlBnkLst="";
            angular.forEach($scope.BundleDatas.datas, function (val, key) {

                angular.forEach(val['details'], function (val1, key1) {                    
                    if (val1['IS_SPLIT'] == 'Y') {
                        console.log(val1['GMT_CUT_PNL_BNK_LST']);
                        vCutPnlBnkLst = vCutPnlBnkLst + val1['GMT_CUT_PNL_BNK_LST'] + ",";
                    }
                });
            });

            var submitData = {
                GMT_CUT_SEW_DLV_CHL_ID: 0,
                HR_PROD_LINE_ID: data.HR_PROD_LINE_ID,
                IS_TAG: data.IS_TAG,
                NO_OF_BAG: data.NO_OF_BAG,
                IS_FINALIZED: 'N',
                GMT_CUT_PNL_BNK_LST: vCutPnlBnkLst,
                pOption: 1000
            };
            
            console.log(submitData);
            console.log(vCutPnlBnkLst);
            //return;

            Dialog.confirm('Do you want to create new challan by spliting?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {
                     return CuttingDataService.saveDataByUrl(submitData, '/GmtCutBank/SaveSewingDelChallan').then(function (res) {
                         if (res.success === false) {
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);

                             console.log(res['data']);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                 $state.go('DeliveryChallan', { pGMT_CUT_SEW_DLV_CHL_ID: res['data'].OP_GMT_CUT_SEW_DLV_CHL_ID }, { reload: true });
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     });
                 });

        }

        $scope.FinalizeAndDeliver = function (data, valid) {
            if (!valid) {
                return;
            }

            if (parseInt(data.GMT_CUT_SEW_DLV_CHL_ID) > 1 && parseInt(data.HR_PROD_LINE_ID) > 0) {

                Dialog.confirm('<h5 style="margin:0px;">Finalizing and Delivering Challan...</h5', 'Are you sure?', ['Yes', ' No'])
                        .then(function () {
                            return CuttingDataService.saveDataByUrl({
                                GMT_CUT_SEW_DLV_CHL_ID: data.GMT_CUT_SEW_DLV_CHL_ID,
                                HR_PROD_LINE_ID: data.HR_PROD_LINE_ID,
                                IS_TAG: data.IS_TAG,
                                NO_OF_BAG: data.NO_OF_BAG,
                                IS_FINALIZED: 'Y',
                                GMT_CUT_PNL_BNK_LST: $scope.BundleDatas.GMT_CUT_PNL_BNK_LST,
                                pOption: 1001
                            }, '/GmtCutBank/SaveSewingDelChallan').then(function (res) {
                                if (res.success === false) {
                                }
                                else {
                                    res['data'] = angular.fromJson(res.jsonStr);
                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                        $scope.challan['IS_FINALIZED'] = 'Y';
                                    }
                                    config.appToastMsg(res.data.PMSG);
                                }
                            })
                        });


            } else {
                config.appToastMsg('MULTI-002Please Complete Challan Data Properly');
            }


        }

        vm.printDelivChallan = function () {

            var url = '/api/Cutting/CutReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            $scope.challan.REPORT_CODE = 'RPT-4514';
            //vm.form.GMT_CUT_INFO_ID = 3;

            //vm.form.FROM_DATE = $filter('date')(vm.form.FROM_DATE, vm.dtFormat);
            //vm.form.TO_DATE = $filter('date')(vm.form.TO_DATE, vm.dtFormat);

            var params = angular.copy($scope.challan);

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

        vm.openBundleStatusModal = function (cuttingData) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_BundleStatusModal',
                controller: 'BundleStatusModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    bndlData: function (CuttingDataService) {
                        return CuttingDataService.getDataByUrl('/GmtCutBank/GetData4BndlStatus?pGMT_CUT_INFO_ID=' + cuttingData.GMT_CUT_INFO_ID).then(function (res) {
                            return res;
                        });
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                console.log(dta);
            });
        }

        vm.openDeliveryChallanSearch = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_DeliveryChallanSewSearchModal',
                controller: 'DeliveryChallanSearchModalController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'app-modal-window'
            });

            modalInstance.result.then(function (data) {
                console.log(data);

                $state.go('DeliveryChallan', { pGMT_CUT_SEW_DLV_CHL_ID: data.GMT_CUT_SEW_DLV_CHL_ID }, { reload: true });
            });
        }




    };
})();
////////// End CutSewDelvChaln4SplitController Controller