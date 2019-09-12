
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('ScPreTreatmentReceiveListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$rootScope', '$scope', 'Hub', 'cur_user_id', '$modal', ScPreTreatmentReceiveListController]);
    function ScPreTreatmentReceiveListController($q, config, DyeingDataService, $stateParams, $state, $rootScope, $scope, Hub, cur_user_id, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        vm.form = { REPORT_CODE: '', SC_PRG_NO: '', CHALAN_NO: '' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [ReceiveList(), GetSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.search = function () {
            vm.gridOptionsDS.read();
        }

        vm.clear = function () {
            vm.form = { SCM_SUPPLIER_ID: "" };
        }

        function GetSupplierList() {
            vm.supplierOption = {
                optionLabel: "-- Select S/C Party --",
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


        function ReceiveList() {

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
                        DyeingDataService.getDataByFullUrl('/api/Dye/ScPtReceive/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm + '&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || '') + '&pCHALAN_NO=' + (vm.form.CHALAN_NO || '') + '&pSC_PRG_NO=' + (vm.form.SC_PRG_NO || '') + '&pUSER_ID=' + cur_user_id).then(function (res) {
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
                { field: "MRR_NO", title: "MRR No", type: "string", width: "10%" },
                { field: "RCV_DT", title: "Issue Date", type: "date", template: "#= kendo.toString(kendo.parseDate(RCV_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "8%", filter: true },
                { field: "QC_NO", title: "QC No", type: "string", width: "8%" },
                { field: "CHALAN_NO", title: "Challan No", type: "string", width: "10%" },
                { field: "CHALAN_DT", title: "Challan Date", type: "date", template: "#= kendo.toString(kendo.parseDate(CHALAN_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%", filter: true },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "10%" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier", type: "string", width: "10%" },
                { field: "STORE_NAME_EN", title: "Store", type: "string", width: "10%" },
                { field: "SC_PRG_NO_LST", title: "Program List", type: "string", width: "12%" },

                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="ScPreTreatmentReceive({pDF_SC_PT_RCV_H_ID:dataItem.DF_SC_PT_RCV_H_ID})" ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs green"><i class="fa fa-edit"> Edit</i></a> ' +
                            ' <a ui-sref="ScPreTreatmentReceiveView({pDF_SC_PT_RCV_H_ID:dataItem.DF_SC_PT_RCV_H_ID})" class="btn btn-xs blue"><i class="fa fa-eye"> View</i></a>' +
                            ' <a ui-sref="ScPreTreatment({pDF_SC_PT_ISS_H_ID:0, pDF_SC_PT_RCV_H_ID:dataItem.DF_SC_PT_RCV_H_ID})" class="btn btn-xs yellow-gold"><i class="fa fa-eye"> Revise</i></a>';
                    },
                    width: "10%"
                }
            ]
        };


    }

})();