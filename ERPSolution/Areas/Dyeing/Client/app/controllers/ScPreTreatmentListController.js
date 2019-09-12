
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('ScPreTreatmentListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$modal', ScPreTreatmentListController]);
    function ScPreTreatmentListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $modal) {

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
            vm.form.DF_SC_PT_ISS_H_ID = dataItem.DF_SC_PT_ISS_H_ID;
            vm.form.DYE_GSTR_REQ_H_ID = dataItem.DYE_GSTR_REQ_H_ID;

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


        vm.printProgRDLC = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-6007';
            vm.form.DF_SC_PT_ISS_H_ID = dataItem.DF_SC_PT_ISS_H_ID;
            vm.form.SC_PRG_NO = dataItem.SC_PRG_NO;

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
                        DyeingDataService.getDataByFullUrl('/api/Dye/ScPreTreatment/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
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
                //SC_PRG_NO, PRG_ISS_BY, PRG_ISS_TO, PRG_ISS_DT, DF_PROC_TYPE_LST, EXP_DELV_DT, CHALAN_NO, CHALAN_DT, GATE_PASS_NO, VEHICLE_NO
                { field: "SC_PRG_NO", title: "Program No", type: "string", width: "10%" },
                { field: "PRG_ISS_DT", title: "Issue Date", type: "date", template: "#= kendo.toString(kendo.parseDate(PRG_ISS_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                { field: "EXP_DELV_DT", title: "Exp. Delivery Date", type: "date", template: "#= kendo.toString(kendo.parseDate(EXP_DELV_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                { field: "CHALAN_NO", title: "Challan No", type: "string", width: "10%" },
                { field: "CHALAN_DT", title: "Challan Date", type: "date", template: "#= kendo.toString(kendo.parseDate(CHALAN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                //{ field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "10%" },
                { field: "SUP_TRD_NAME_BN", title: "Supplier", type: "string", width: "10%" },
                { field: "STORE_NAME_EN", title: "Store", type: "string", width: "10%" },
                { field: "GATE_PASS_NO", title: "Gate Pass No", type: "string", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="ScPreTreatment({pDF_SC_PT_ISS_H_ID:dataItem.DF_SC_PT_ISS_H_ID})" ng-if="(dataItem.ACTN_ROLE_FLAG==' + "'RI'" + ' || dataItem.ACTN_ROLE_FLAG==' + "'RA'" + ') && dataItem.PERMISSION==1" class="btn btn-xs green"><i class="fa fa-edit"> Edit</i></a> ' +
                            ' <a ng-click="vm.printProgRDLC(dataItem);" class="btn btn-xs blue"><i class="fa fa-print"> Print Prog</i></a></a>' +
                            ' <a ng-click="vm.printRDLC(dataItem);" class="btn btn-xs blue"><i class="fa fa-print"> Print Chln</i></a></a>';
                        
                    },
                    width: "10%"
                }
            ]
        };


    }

})();

