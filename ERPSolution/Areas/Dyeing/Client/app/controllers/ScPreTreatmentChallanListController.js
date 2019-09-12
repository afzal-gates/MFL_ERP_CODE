
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('ScPreTreatmentChallanListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$modal', ScPreTreatmentChallanListController]);
    function ScPreTreatmentChallanListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        vm.form = { REPORT_CODE: '' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [CompleteBatchList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.printRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6005';
            vm.form.DF_SC_PT_CHL_ISS_H_ID = dataItem.DF_SC_PT_CHL_ISS_H_ID;
            //vm.form.DYE_GSTR_REQ_H_ID = dataItem.DYE_GSTR_REQ_H_ID;

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



        vm.printExcelRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6005';
            vm.form.IS_EXCEL_FORMAT = 'Y';
            vm.form.DF_SC_PT_CHL_ISS_H_ID = dataItem.DF_SC_PT_CHL_ISS_H_ID;
            //vm.form.DYE_GSTR_REQ_H_ID = dataItem.DYE_GSTR_REQ_H_ID;

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


        function CompleteBatchList() {

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
                        DyeingDataService.getDataByFullUrl('/api/Dye/ScPreTreatment/ScPtChallanGetAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
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
            scrollable: true,
            height: '400px',
            columns: [

                { field: "CHALAN_NO", title: "Challan No", type: "string", width: "10%" },
                { field: "CHALAN_DT", title: "Challan Date", type: "date", template: "#= kendo.toString(kendo.parseDate(CHALAN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier", type: "string", width: "10%" },
                { field: "STORE_NAME_EN", title: "Store", type: "string", width: "10%" },
                { field: "GATE_PASS_NO", title: "Gate Pass No", type: "string", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="ScPreTreatmentChallan({pDF_SC_PT_CHL_ISS_H_ID:dataItem.DF_SC_PT_CHL_ISS_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs green"><i class="fa fa-edit"> Edit</i></a> ' +
                            ' <a ng-click="vm.printRDLC(dataItem);" class="btn btn-xs blue"><i class="fa fa-print"> Print</i></a></a>'+
                        ' <a ng-click="vm.printExcelRDLC(dataItem);" class="btn btn-xs yellow-gold"><i class="fa fa-print"> Print Excel</i></a></a>';
                    },
                    width: "10%"
                }
            ]
        };


    }

})();