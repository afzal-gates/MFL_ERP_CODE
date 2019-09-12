(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('ShadeCheckRollInspectionListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$modal', ShadeCheckRollInspectionListController]);
    function ShadeCheckRollInspectionListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $modal) {

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
                        DyeingDataService.getDataByFullUrl('/api/Dye/OtherCheckRoll/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pUSER_ID=' + cur_user_id).then(function (res) {
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
            sortable: false,
            scrollable: false,
            //height: '400px',
            columns: [
                { field: "DYE_BATCH_NO", title: "Batch No", type: "string", width: "7%" },
                { field: "DYE_LOT_NO", title: "Lot", type: "string", width: "4%" },
                
                { field: "RP_SL_NO", title: "SL#", type: "string", width: "4%" },
                { field: "ACT_BATCH_QTY", title: "B Qty", type: "string", width: "5%" },
                { field: "BATCH_RP_QTY", title: "RP Qty", type: "string", width: "5%" },
                { field: "RCV_DT_FRM_QC", title: "Rcv Date", type: "date", width: "8%", template: "#= kendo.toString(kendo.parseDate(RCV_DT_FRM_QC,'yyyy-MM-dd'),'dd-MMM-yyyy') #", filter: true },
                { field: "SHD_APRV_DT", title: "Aprv Date", type: "date", width: "8%", template: "#= kendo.toString(kendo.parseDate(SHD_APRV_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", filter: true },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer Name", type: "string", width: "10%" },
                //{ field: "STYLE_NO", title: "Style #", type: "string", width: "15%" },
                //{ field: "MC_ORDER_NO_LST", title: "Order No", type: "string", width: "10%" },
                { field: "STYLE_NO", title: "Style/Order", width: "10%", editable: false, template: "<span style=\"display: block;\">#=STYLE_NO #</span> <span style=\"display: block;font-style:italic;\"><small>#:MC_ORDER_NO_LST#</small><span>" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "7%" },
                { field: "GREY_QTY", title: "G. Qty", type: "string", width: "5%" },
                { field: "GREY_ROLL_QTY", title: "G. Roll", type: "string", width: "5%" },
                { field: "FIN_QTY", title: "Fin Qty", type: "string", width: "5%" },
                { field: "FIN_ROLL_QTY", title: "Fin Roll", type: "string", width: "5%" },
                { field: "QC_STATUS", title: "Status", type: "string", width: "7%" },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="ShadeCheckRollInspection({pDF_RIB_SHADE_RPT_H_ID:dataItem.DF_RIB_SHADE_RPT_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs green"><i class="fa fa-edit"> Edit</i></a> '+
                         ' <a ui-sref="ShadeCheckRollInspectionView({pDF_RIB_SHADE_RPT_H_ID:dataItem.DF_RIB_SHADE_RPT_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'Y'" + '" class="btn btn-xs blue"><i class="fa fa-eye"> View</i></a> ';
                            
                    },
                    width: "10%"
                }
            ]
        };


    }

})();

