
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('DeliveryChallanPrintEmbrSearchModalController', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', '$state', '$filter', DeliveryChallanPrintEmbrSearchModalController]);
    function DeliveryChallanPrintEmbrSearchModalController($q, config, CuttingDataService, $modalInstance, $scope, $state, $filter) {


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

        
        vm.selectChallan = function (data) {
            $modalInstance.close(data);
        }
        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

        vm.challanList = [];
        vm.TTL_NO_OF_BAG = 0;
        vm.TTL_NO_OF_BNDL_SCANNED = 0;
        vm.TTL_FINAL_QTY = 0;
        vm.searchChallan = function () {
            return CuttingDataService.getDataByFullUrl('/api/Cutting/GmtCutBank/GetPrintEmbrDelvChalnList?pGMT_CUT_PRN_DELV_CHL_H_ID=' + vm.form.GMT_CUT_PRN_DELV_CHL_H_ID + '&pCHALAN_DT=' + $filter('date')(vm.form.CHALAN_DT, vm.dtFormat) +
                '&pMC_BYR_ACC_GRP_ID=' + vm.form.MC_BYR_ACC_GRP_ID + '&pMC_ORDER_H_ID=' + vm.form.MC_ORDER_H_ID + '&pMC_COLOR_ID=' + vm.form.MC_COLOR_ID +
                '&pLK_BNDL_MVM_RSN_ID=' + vm.form.LK_BNDL_MVM_RSN_ID +
                '&pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID + '&pIS_TAG=' + (vm.form.IS_TAG || '') + '&pIS_FINALIZED=' + (vm.form.IS_FINALIZED || '')).then(function (res) {

                    vm.challanList = res;
                    vm.TTL_NO_OF_BAG = _.sumBy(res, function (o) { return o.NO_OF_BAG; });
                    vm.TTL_NO_OF_BNDL_SCANNED = _.sumBy(res, function (o) { return o.NO_OF_BNDL_SCANNED; });
                    vm.TTL_FINAL_QTY = _.sumBy(res, function (o) { return o.FINAL_QTY; });
                });
        }

        //$scope.onSearch = function (data) {

        //    getQueryString(data);
        //    $scope.gridDs.read();
        //}
        

        //$scope.gridDs = new kendo.data.DataSource({
        //    serverPaging: true,
        //    schema: {
        //        data: "data",
        //        total: "total",
        //        model: {
        //            fields: {
        //                STR_REQ_DT: { type: "date" }
        //            }
        //        }
        //    },
        //    transport: {
        //        read: function (e) {
        //            var webapi = new kendo.data.transports.webapi({});
        //            var params = webapi.parameterMap(e.data);
        //            var url = '/GreyFabReq/List';
        //            url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;

        //            url += $scope.qs.length>1 ? '&'+$scope.qs : '';
        //            //$state.go($state.current, { page: params.page });

        //            DyeingDataService.getDataByUrl(url).then(function (res) {
        //                e.success(res);
        //            });
        //        }
        //    },
        //    pageSize: 10
        //});

        //$scope.OnSelect = function (dataItem) {
        //    console.log(dataItem);
        //};



        //$scope.gridOptions = {
        //    filterable: {
        //        extra: false,
        //        operators: {
        //            string: {
        //                contains: "Contains"
        //            }
        //        }
        //    },
        //    pageable: {
        //        refresh: true,
        //        pageSizes: true,
        //        buttonCount: 5
        //    },
        //    sortable: true,
        //    resizable: true,
        //    columns: [
        //        {
        //            field: "STR_REQ_NO", title: "Requisition No", width: "12%",
        //            template: "<span style=\"display: block;\">#=STR_REQ_NO #</span> <span style=\"display: block;font-style:italic; color:red;\"><small>#:PENDING#</small><span>"
        //        },

        //        { field: "STR_REQ_DT", title: "Req Date", width: "10%", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #" },
        //        { field: "ACT_BATCH_QTY", title: "Req(Kg)", width: "8%" },
        //        { field: "DYE_BATCH_NO", title: "Batch No", width: "10%" },

        //        { field: "EVENT_NAME_S", title: "Status", type: "string", width: "10%", template: "# if( ACTN_ROLE_FLAG!=='DN') {#<h4 style=\"padding-bottom: 0px;padding-top: 5px;margin-top: 0px;margin-bottom: 0px;\" class=\"badge badge-danger\">#=EVENT_NAME_S #<h4># }# # if( ACTN_ROLE_FLAG==='DN') {#<h4 style=\"padding-bottom: 0px;padding-top: 5px;margin-top: 0px;margin-bottom: 0px;\" class=\"badge badge-success\">#=EVENT_NAME_S #<h4># }#" },
        //        { field: "REQ_TYPE_NAME", title: "Requisition Type", type: "string", width: "15%" },
        //        { field: "COMP_NAME_EN", title: "Company", type: "string", width: "15%" },

        //        {
        //            title: "Delv.Challan",
        //            template: "<a ng-click='vm.printDeliveryChallan(dataItem)'  class='btn btn-xs purple'> Delivery Challan</a>",
        //            width: "10%"
        //        },
        //        {
        //            title: "Action",
        //            template: "# if( ACTN_ROLE_FLAG!=='DN') {# <a ng-click='vm.markAsCompleted(dataItem)'  class='btn btn-xs'> Mark as Issued</a> #}#",
        //            width: "10%"
        //        },
        //        {
        //            title: "Action",
        //            template: "<a class='btn btn-xs bg-grey-cascade' ng-click='OnSelect(dataItem)'><i class='fa fa-send-o'></i> Select</a>",
        //            width: "10%"
        //        }
        //    ]
        //};




    }
})();




(function () {
    'use strict';
    angular.module('multitex.cutting').controller('DeliveryChallanSearchModalControllerSsc', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', DeliveryChallanSearchModalControllerSsc]);
    function DeliveryChallanSearchModalControllerSsc($q, config, CuttingDataService, $modalInstance, $scope) {
        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
    }
})();


(function () {
    'use strict';
    angular.module('multitex.cutting').controller('DeliveryChallanSearchModalControllerSew', ['$q', 'config', 'CuttingDataService', '$modalInstance', '$scope', DeliveryChallanSearchModalControllerSew]);
    function DeliveryChallanSearchModalControllerSew($q, config, CuttingDataService, $modalInstance, $scope) {
        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
    }
})();
