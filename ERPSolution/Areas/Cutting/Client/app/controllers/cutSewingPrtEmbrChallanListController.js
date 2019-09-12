////////// Start CutDeliveryChallan Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutSewingPrtEmbrChallanListController', ['config', '$q', '$scope', '$filter', '$state', '$stateParams', 'CuttingDataService', 'Dialog', '$modal', CutSewingPrtEmbrChallanListController]);
    function CutSewingPrtEmbrChallanListController(config, $q, $scope, $filter, $state, $stateParams, CuttingDataService, Dialog, $modal) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        $scope.DsService = new kendo.data.DataSource({
            data: [
                 { LOOKUP_DATA_ID: 692, LK_DATA_NAME_EN: 'Printing' },
                 { LOOKUP_DATA_ID: 693, LK_DATA_NAME_EN: 'Embroidery' },
                 { LOOKUP_DATA_ID: 694, LK_DATA_NAME_EN: 'HeatSeal' },
                 { LOOKUP_DATA_ID: 695, LK_DATA_NAME_EN: 'Printing+Embroidery' }
            ]
        });
        activate();
        function activate() {
            var promise = [getSupplierList(), getSupplierCatData()];
            return $q.all(promise).then(function () {
                
                vm.showSplash = false;                
            });
        }


        $scope.onFocused = function () {
            if ($stateParams.pGMT_CUT_PRN_DELV_CHL_H_ID) {
                $scope.challan = {};
                $scope.BundleDatas = {
                    datas: []
                };
                $state.go('SewPrtEmbrDelChallanList', {}, { inherit: false });
            } 
        };


        function getSupplierList() {
            return CuttingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=717').then(function (res) {

                $scope.DsSup = new kendo.data.DataSource({
                    data: res
                });
            });
        }




        if ($stateParams.pGMT_CUT_PRN_DELV_CHL_H_ID) {
            CuttingDataService.getDataByUrl('/GmtCutBank/getPrintEmbrDeliveryChallanAutoCompl?pGMT_CUT_PRN_DELV_CHL_H_ID=' + $stateParams.pGMT_CUT_PRN_DELV_CHL_H_ID).
            then(function (res) {
                if (res.length > 0) {
                    $scope.challan = res[0];
                    getPrintEmbrDelChallanSumData(res[0]);
                }

            });
        } 

        $scope.sewDelChallanAuto = function (q) {
            return CuttingDataService.getDataByUrl('/GmtCutBank/getPrintEmbrDeliveryChallanAutoCompl?pCHALAN_NO=' + q);
        }

        function getPrintEmbrDelChallanSumData(data) {
            return CuttingDataService.getDataByUrl('/GmtCutBank/getPrintEmbrDelChallanSumData?pGMT_CUT_PRN_DELV_CHL_H_ID=' + data.GMT_CUT_PRN_DELV_CHL_H_ID).then(function (res) {
                $scope.BundleDatas = {
                    datas: res,
                    total_bundles: _.sumBy(res, function (o) { return o.TTL_NO_OF_BNDL_SCANNED; }),
                    total_cut_qty: _.sumBy(res, function (o) { return o.TTL_CUTTING_QTY; }),
                    total_qty: _.sumBy(res, function (o) { return o.TTL_FINAL_QTY; }),
                    GMT_CUT_PNL_BNK_LST: res.map(function (o) { return o.GMT_CUT_PNL_BNK_LST; }).join(','),
                    RF_GARM_PART_LST: res.map(function (o) { return o.RF_GARM_PART_LST; }).join(','),
                };
                setModelVal(data);
                
            });
        }

        vm.onSubmit = function (data) {
            if (!data)
                return;
            getPrintEmbrDelChallanSumData(data);
         
        };

        function setModelVal(data) {
            getGmtPartList().then(function (res) {
                $scope.PartDs = new kendo.data.DataSource({
                    data: res
                });
            }).then(function () {
                $scope.challan = Object.assign({}, data);
            });
        }

        function getGmtPartList() {
            return CuttingDataService.getDataByFullUrl('/api/common/GmtPartList?pRF_GARM_PART_LST=' + $scope.BundleDatas.RF_GARM_PART_LST||'');
        }

        function getSupplierCatData() {
            return CuttingDataService.getDataByFullUrl('/api/common/LookupListData/89').then(function (res) {

                $scope.DsSupCat = new kendo.data.DataSource({
                    data: res
                });

            });
        }

        //function getServiceData() {
        //    return CuttingDataService.getDataByFullUrl('/api/common/LookupListData/142').then(function (res) {

        //        $scope.DsService = new kendo.data.DataSource({
        //            data: res.filter(function (o) { return [692, 693, 694, 695].indexOf(o.LOOKUP_DATA_ID) > -1; })

        //        });
        //    });
        //};
        


        $scope.submitData = function (obj, valid,isRechallan) {
            if (!valid) {
                return;
            }
            return CuttingDataService.saveDataByUrl({
                XML: config.xmlStringShort(
                   [{
                        GMT_CUT_PRN_DELV_CHL_H_ID: obj.GMT_CUT_PRN_DELV_CHL_H_ID, RF_GARM_PART_LST: obj.RF_GARM_PART_LST.join(','), LK_BNDL_MVM_RSN_ID: obj.LK_BNDL_MVM_RSN_ID,
                        LK_SUPPLIER_CAT: obj.LK_SUPPLIER_CAT, SCM_SUPPLIER_ID: obj.SCM_SUPPLIER_ID, IS_TAG: obj.IS_TAG, NO_OF_BAG: obj.NO_OF_BAG, GATE_PASS_NO: obj.GATE_PASS_NO,
                        VEHICLE_NO: obj.VEHICLE_NO, IS_RE_CHALLAN: (isRechallan || 'N')
                    }]
                ),
                GMT_CUT_PNL_BNK_LST: $scope.BundleDatas.GMT_CUT_PNL_BNK_LST,
                pOption: 1003

            }, '/GmtCutBank/SaveAndOrDelete').then(function (res) {
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
        $scope.FinalizeAndDeliver = function (obj, valid) {
            if (!valid) {
                return;
            }
            if ( parseInt(obj.GMT_CUT_PRN_DELV_CHL_H_ID) > 1 && parseInt(obj.SCM_SUPPLIER_ID) > 1 ) {
                Dialog.confirm('<h5 style="margin:0px;">Finalizing and Delivering Challan...</h5', 'Are you sure?', ['Yes', ' No'])
                        .then(function () {
                            return CuttingDataService.saveDataByUrl({
                                XML: config.xmlStringShort(
                                    [{
                                        GMT_CUT_PRN_DELV_CHL_H_ID: obj.GMT_CUT_PRN_DELV_CHL_H_ID, RF_GARM_PART_LST: obj.RF_GARM_PART_LST.join(','), LK_BNDL_MVM_RSN_ID: obj.LK_BNDL_MVM_RSN_ID,
                                        LK_SUPPLIER_CAT: obj.LK_SUPPLIER_CAT, SCM_SUPPLIER_ID: obj.SCM_SUPPLIER_ID, IS_TAG: 'N', NO_OF_BAG: obj.NO_OF_BAG, GATE_PASS_NO: obj.GATE_PASS_NO,
                                        VEHICLE_NO: obj.VEHICLE_NO
                                    }]
                                ),
                                GMT_CUT_PRN_DELV_CHL_H_LST: obj.GMT_CUT_PRN_DELV_CHL_H_ID,
                                IS_FINALIZED: 'Y',
                                GMT_CUT_PNL_BNK_LST: $scope.BundleDatas.GMT_CUT_PNL_BNK_LST,

                            }, '/GmtCutBank/SavePrintEmbrDelChallan').then(function (res) {
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

            $scope.challan.REPORT_CODE = 'RPT-4515';
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

        vm.printBndlChart4DelivChallan = function () {

            var url = '/api/Cutting/CutReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            $scope.challan.REPORT_CODE = 'RPT-4521';
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
                templateUrl: '/Cutting/Cutting/_DeliveryChallanPrintEmbrSearchModal',
                controller: 'DeliveryChallanPrintEmbrSearchModalController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'app-modal-window'
            });

            modalInstance.result.then(function (data) {
                console.log(data);

                $state.go('SewPrtEmbrDelChallanList', { pGMT_CUT_PRN_DELV_CHL_H_ID: data.GMT_CUT_PRN_DELV_CHL_H_ID }, { reload: true });
            });
        }

        vm.openEmblsmntDelivChalnSearchModal4Merge = function () {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Cutting/Cutting/_EmblsmntDelivChalnSearchModal4Merge',
                controller: 'EmblsmntDelivChalnSearchModal4MergeController',
                controllerAs: 'vm',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    ParentObJ: function (CuttingDataService) {
                        return { SCM_SUPPLIER_ID: $scope.challan.SCM_SUPPLIER_ID, MC_STYLE_H_ID: $scope.BundleDatas.datas[0].MC_STYLE_H_ID, GMT_CUT_PRN_DELV_CHL_H_ID: $scope.challan.GMT_CUT_PRN_DELV_CHL_H_ID, CHALAN_NO: $scope.challan.CHALAN_NO }                        
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);

                $state.go('SewPrtEmbrDelChallanList', { pGMT_CUT_PRN_DELV_CHL_H_ID: data.GMT_CUT_PRN_DELV_CHL_H_ID }, { reload: true });
            });
        }

        


        };
})();
////////// End CutPanelInspection Controller


