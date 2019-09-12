
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DfScProgramBillListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$filter', DfScProgramBillListController]);
    function DfScProgramBillListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $filter) {


        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.form = {};

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [GetSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


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
                    $stateParams.pSCM_SUPPLIER_ID = item.SCM_SUPPLIER_ID;
                },
                dataBound: function (e) {
                    if ($stateParams.pSCM_SUPPLIER_ID != null && $stateParams.pSCM_SUPPLIER_ID > 0) {
                        vm.form.SCM_SUPPLIER_ID = $stateParams.pSCM_SUPPLIER_ID;
                    }
                }
            };

            return vm.supplierDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        DyeingDataService.getDataByFullUrl('/api/purchase/supplierprofile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=465').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        vm.sciBillGridOption = {
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
                { field: "BILL_DT", title: "Date", format: "{0:dd-MMM-yyyy}", width: "8%" },
                { field: "BILL_NO", title: "Bill#", type: "string", width: "14%" },
                { field: "REF_BILL_NO", title: "Ref.Bill#", type: "string", width: "14%" },
                { field: "REVISION_NO", title: "Rev#", type: "number", width: "6%", filterable: false },
                { field: "OTH_RSN_DESC", title: "Rev.Descr", width: "25%", filterable: false },
                { field: "SUP_TRD_NAME_EN", title: "Supplier", width: "15%" },
                {
                    field: "EVENT_NAME",
                    title: "Status",
                    template: function () {
                        return "<label class='label label-sm label-warning'>{{dataItem.EVENT_NAME}}</label>";
                    },
                    width: "12%"
                },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' title='Edit/View' ng-click='vm.editBill(dataItem)'><i class='fa fa-edit'></i></button>&nbsp;";
                    },
                    width: "6%"
                }
            ]
        };

        vm.sciBillGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "DF_SCO_FP_BILL_H_ID",
                    fields: {
                        BILL_DT: { type: "date", editable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/dye/DfScProgramBill/GetBillList?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || $stateParams.pSCM_SUPPLIER_ID || 0);

                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += DyeingDataService.kFilterStr2QueryParam(params.filter);

                    return DyeingDataService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    });
                }
            },
            sort: [{ field: 'BILL_DT', dir: 'desc' }]
        });


        vm.getBillList = function () {
            vm.sciBillGridDataSource.read();
        };

        vm.editBill = function (dataItem) {
            $state.go('DfScProgramBill', { pDF_SCO_FP_BILL_H_ID: dataItem.DF_SCO_FP_BILL_H_ID });
        };

        vm.printBill = function (dataItem) {
            //console.log(dataItem);
            dataItem.REPORT_CODE = 'RPT-3563';

            var url;
            url = '/api/Knit/KntReport/PreviewReport';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');

            var params = angular.copy(dataItem);

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
