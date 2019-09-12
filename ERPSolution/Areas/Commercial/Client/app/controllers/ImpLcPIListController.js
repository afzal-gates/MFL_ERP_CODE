
(function () {
    'use strict';
    angular.module('multitex.cmr').controller('ImpLcPIListController', ['$q', 'config', 'CmrDataService', 'commonDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', '$modal', ImpLcPIListController]);
    function ImpLcPIListController($q, config, CmrDataService, commonDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = { DOC_TITLE: 'Document No' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [piList(), getSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.PI_DT_IMP_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PI_DT_IMP_LNopened = true;
        }

        function getSupplierList() {
            return vm.supplierList = {
                optionLabel: "-- Select Supplier --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/purchase/supplierprofile/selectall').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID"
            };
        }

        vm.searchData = function () {

            var pm = "";
            if (vm.form.SCM_SUPPLIER_ID)
                pm = pm + "&pSCM_SUPPLIER_ID=" + vm.form.SCM_SUPPLIER_ID;
            if (vm.form.PI_DT_IMP) {
                var _isudate = $filter('date')(vm.form.PI_DT_IMP, 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pPI_DT_IMP=" + _isudate;
            }
            if (vm.form.PI_NO_IMP)
                pm = pm + "&pPI_NO_IMP=" + vm.form.PI_NO_IMP;
            if (vm.form.PO_NO_IMP)
                pm = pm + "&pPO_NO_IMP=" + vm.form.PO_NO_IMP;

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            })
        }

        function piList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        CmrDataService.getDataByFullUrl('/api/cmr/ImportLCPI/SelectAll/' + params.page + '/' + params.pageSize).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                schema: {
                    data: "data",
                    total: 'total'
                }
            })
        }

        vm.gridOptions = {
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
            height: '400px',
            scrollable: true,
            columns: [

                { field: "CM_IMP_PI_H_ID", hidden: true },
                //{ field: "RPT_PATH_URL", hidden: true },

                { field: "PI_NO_IMP", title: "PI No", width: "10%" },
                { field: "PI_DT_IMP", title: "PI Date", type: "date", template: "#= kendo.toString(kendo.parseDate(PI_DT_IMP,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "COMP_NAME_EN", title: "Company Name", width: "15%" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier Name", width: "15%" },
                { field: "TERMS_DESC", title: "Terms Description", width: "20%" },
                //{ field: "STYLE_NO", title: "Style No", width: "10%" },
                //{ field: "ORDER_NO", title: "Order No", width: "10%" },
                //{ field: "ACTN_STATUS_NAME", title: "Status", width: "10%" },

                {
                    title: "Action",
                    template: '<a ui-sref="ImpLcPI({pCM_IMP_PI_H_ID:dataItem.CM_IMP_PI_H_ID,pREVISE:0})" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a>' +
                        ' <a ui-sref="ImpLcPI({pCM_IMP_PI_H_ID:dataItem.CM_IMP_PI_H_ID,pREVISE:1})" class="btn btn-xs yellow-gold"><i class="fa fa-edit"> Revise PI</i></a>' +
                    ' <a ui-sref="ImpLcPOPIRevise({pCM_IMP_PI_H_ID:dataItem.CM_IMP_PI_H_ID})" class="btn btn-xs green"><i class="fa fa-edit"> Revise PO & PI</i></a></a>',
                    width: "15%"
                },
                {
                    title: "Print",
                    template: '<a ng-click="vm.printExportPI(dataItem)" class="btn btn-xs green"><i class="fa fa-print"> Print</i></a>',
                    width: "10%"
                }
            ],
        };



        vm.printExportPI = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-7000';

            vm.form.CM_EXP_PI_H_ID = dataItem.CM_EXP_PI_H_ID;
            vm.form.RPT_PATH_URL = dataItem.RPT_PATH_URL;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Cmr/CmrReport/PreviewReportRDLC');
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



    }

})();