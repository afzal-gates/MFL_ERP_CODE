
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('EmblsmntDelivChalnSearchModal4MergeController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', '$state', '$filter', '$http', 'ParentObJ', 'Dialog', EmblsmntDelivChalnSearchModal4MergeController]);
    function EmblsmntDelivChalnSearchModal4MergeController($q, config, CuttingDataService, $modalInstance, $scope, $state, $filter, $http, ParentObJ, Dialog) {


        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.autoFocus = false;

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> #: data.ORDER_NO #</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';

        vm.form = {};

        activate();
        function activate() {
            var promise = [getBuyerAcGrpList(), getByrAccWiseStyleExtList(), getSupplierList(), getServiceData()];
            return $q.all(promise).then(function () {
            
            });
        }

        $scope.qs = '';

        $scope.form = {
            IS_TAG : 'Y'
        };

        $scope.dtFormat = config.appDateFormat;

        vm.chlnDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.chlnDateOpened = true;
        };



        $scope.GridDateOpened = [];
        $scope.GridDateOpen = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.GridDateOpened[index] = true;
        };


        $scope.BuyerGrpDs = new kendo.data.DataSource({
            data: []
        });
        $scope.StylesDs = new kendo.data.DataSource({
            data: []
        });

        $scope.OrderDs = new kendo.data.DataSource({
            data: []
        });
        $scope.ColorDs = new kendo.data.DataSource({
            data: []
        });

        function getSupplierList() {
            return CuttingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=717').then(function (res) {

                $scope.DsSup = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function getServiceData() {
            return CuttingDataService.getDataByFullUrl('/api/common/LookupListData/142').then(function (res) {

                $scope.DsService = new kendo.data.DataSource({
                    data: res.filter(function (o) { return [692, 693, 694, 695].indexOf(o.LOOKUP_DATA_ID) > -1; })

                });
            });
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
                        var url = '/api/common/getOrderStyleDropDownDataGmt?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : '') + '&pMC_STYLE_H_ID=';
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

        
        vm.selectChallan = function (data) {
            $modalInstance.close(data);
        }
        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

        vm.challanList = [];
        vm.searchChallan = function () {
            return CuttingDataService.getDataByFullUrl('/api/Cutting/GmtCutBank/GetPrintEmbrDelvChalnList?pGMT_CUT_PRN_DELV_CHL_H_ID=' + vm.form.GMT_CUT_PRN_DELV_CHL_H_ID + '&pCHALAN_DT=' + $filter('date')(vm.form.CHALAN_DT, vm.dtFormat) +
                '&pMC_BYR_ACC_GRP_ID=' + vm.form.MC_BYR_ACC_GRP_ID + '&pMC_STYLE_H_ID=&pMC_ORDER_H_ID=' + vm.form.MC_ORDER_H_ID + '&pMC_COLOR_ID=' + vm.form.MC_COLOR_ID +
                '&pLK_BNDL_MVM_RSN_ID=' + vm.form.LK_BNDL_MVM_RSN_ID +
                '&pSCM_SUPPLIER_ID=' + ParentObJ.SCM_SUPPLIER_ID + '&pIS_TAG=' + (vm.form.IS_TAG || '') + '&pIS_FINALIZED=').then(function (res) {

                    vm.challanList = res;
                });
        }

        
        vm.mergChalnLst = "";
        vm.onChangeSelect = function () {
            vm.mergChalnLst = "";
            angular.forEach(vm.challanList, function (val, key) {
                if (val['IS_SELECT'] == "Y") {
                    vm.mergChalnLst = vm.mergChalnLst + val['GMT_CUT_PRN_DELV_CHL_H_ID'] + ",";
                }
            });
        }

        vm.challanMerge = function () {

            Dialog.confirm('Do you want to merge these selected challan with challan# ' + ParentObJ['CHALAN_NO'] + ' ?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + $scope.token },
                         url: '/api/Cutting/GmtCutBank/MergeChallan',
                         method: 'post',
                         data: { GMT_CUT_PRN_DELV_CHL_H_ID: ParentObJ['GMT_CUT_PRN_DELV_CHL_H_ID'], GMT_CUT_PRN_DELV_CHL_H_LST: vm.mergChalnLst }
                     }).then(function (res) {
                         //$scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             //$scope.$parent.errors = [];
                             //$scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                 $modalInstance.close({ GMT_CUT_PRN_DELV_CHL_H_ID: ParentObJ['GMT_CUT_PRN_DELV_CHL_H_ID'] });
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });
        }


    }
})();

