﻿(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('AopBatchListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$filter', AopBatchListController]);
    function AopBatchListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [programList(), GetReqSourceList(), GetSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        
        $scope.RCV_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.RCV_DT_LNopened = true;
        }


        //  DropdownList

        function GetReqSourceList() {
            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            DyeingDataService.getDataByFullUrl('/api/common/GetStoreInfo?pINV_ITEM_CAT_LST=3,4&pSC_USER_ID=' + cur_user_id).then(function (res) {
                                var list = _.filter(res, function (x) { return x.LK_WH_TYPE_ID == 511 || x.LK_WH_TYPE_ID == 512 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        };

        
        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        vm.searchData = function () {

            var pm = "";
            if (vm.form.RCV_REF_NO)
                pm = pm + "&pRCV_REF_NO=" + vm.form.RCV_REF_NO;
            if (vm.form.RCV_DT) {
                var _isudate = $filter('date')(vm.form.RCV_DT, 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pRCV_DT=" + _isudate;
            }
            if (vm.form.CHALAN_NO)
                pm = pm + "&pCHALAN_NO=" + vm.form.CHALAN_NO;
            if (vm.form.SCM_STORE_ID)
                pm = pm + "&pSCM_STORE_ID=" + vm.form.SCM_STORE_ID;
            if (vm.form.SCM_SUPPLIER_ID)
                pm = pm + "&pSCM_SUPPLIER_ID=" + vm.form.SCM_SUPPLIER_ID;
            if (vm.form.DYE_BATCH_NO)
                pm = pm + "&pDYE_BATCH_NO=" + vm.form.DYE_BATCH_NO;

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        //var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl('/api/Dye/AopBatch/SelectAopBatchList/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total',
                    //model: {
                    //    fields: {
                    //        RCV_REF_NO: { type: "string" },
                    //        RCV_DT: { type: "date" },
                    //        CHALAN_NO: { type: "string" },
                    //        CHALAN_DT: { type: "date" },
                    //        SUP_TRD_NAME_EN: { type: "string" },
                    //        STORE_NAME_EN: { type: "string" },
                    //        DYE_BATCH_NO: { type: "string" },
                    //        GATE_PASS_NO: { type: "decimal" },
                    //    }
                    //}
                }
            })
        }

        function programList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
                        DyeingDataService.getDataByFullUrl('/api/Dye/AopBatch/SelectAopBatchList/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total',
                    //model: {
                    //    fields: {
                    //        RCV_REF_NO: { type: "string" },
                    //        RCV_DT: { type: "date" },
                    //        CHALAN_NO: { type: "string" },
                    //        CHALAN_DT: { type: "date" },
                    //        COMP_NAME_EN: { type: "string" },
                    //        SUP_TRD_NAME_EN: { type: "string" },
                    //        STORE_NAME_EN: { type: "string" },
                    //        DYE_BATCH_NO: { type: "string" },
                    //        GATE_PASS_NO: { type: "decimal" },
                    //    }
                    //}
                }
            })
        }


        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-4032';

            vm.form["DYE_BATCH_NO"] = dataItem.DYE_BATCH_NO;
            vm.form["DYE_BT_CARD_H_ID"] = dataItem.DYE_BT_CARD_H_ID;

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
        };

        vm.gridOptions = {
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains"
                    }
                }
            },
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            sortable: true,
            columns: [
                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "10%" },
                { field: "DYE_LOT_NO", title: "Lot No", type: "string", width: "10%" },
                { field: "BATCH_REQ_DT", title: "Batch Date", type: "date", template: "#= kendo.toString(kendo.parseDate(BATCH_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                { field: "ACT_BATCH_QTY", title: "Batch Qty", type: "string", width: "10%" },
                //{ field: "CHALAN_DT", title: "Challan Date", type: "date", template: "#= kendo.toString(kendo.parseDate(CHALAN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer Name", type: "string", width: "20%" },
                { field: "STYLE_NO", title: "Style No", type: "string", width: "15%" },
                { field: "ORDER_NO", title: "Order No", type: "string", width: "15%" },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="AopBatch({pDYE_BT_CARD_H_ID:dataItem.DYE_BT_CARD_H_ID})" class="btn btn-xs green"><i class="fa fa-edit"> Edit</i></a> '+
                            ' <a ng-click="vm.printRDLC(dataItem);" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a></a>';
                        //return '</a><a ui-sref="AopBatch({pDYE_BT_CARD_H_ID:dataItem.DYE_BT_CARD_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs green"><i class="fa fa-edit"> Edit</i></a> ';
                            //' <a ng-click="vm.printRDLC(dataItem);" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a></a>';
                    },
                    width: "10%"
                }

            ]
        };

    }

})();