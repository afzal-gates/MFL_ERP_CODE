(function () {
    'use strict';
    angular.module('multitex.purchase').controller('GeneratePOListController', ['$q', 'config', 'PurchaseDataService', '$stateParams', '$state', '$scope', GeneratePOListController]);
    function GeneratePOListController($q, config, PurchaseDataService, $stateParams, $state, $scope) {

        var vm = this;
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getSupplierList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getSupplierList() {
            PurchaseDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetPOList?pIS_LOCAL_CASH_P=N').then(function (res) {
                vm.poList = new kendo.data.DataSource({
                    data: res,
                    pageSize: 10
                });
            }, function (err) {
                console.log(err);
            });

        }

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        PurchaseDataService.getDataByFullUrl('/api/knit/KnitYarnReq/GetPOList?pIS_LOCAL_CASH_P=N').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 10
            },
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
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "PO_NO_IMP", title: "PO No", type: "string", width: "10%" },
                { field: "PO_DT_IMP", title: "PO Date", type: "date", template: "#= kendo.toString(kendo.parseDate(PO_DT_IMP,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier Name", type: "string", width: "15%" },
                //{ field: "SUP_OWNER_NAME", title: "Owner Name", type: "string", width: "50px" },
                { field: "REVISION_NO", title: "REVISION_NO", type: "string", width: "5%" },
                { field: "PAY_MTHD_NAME", title: "PAY_MTHD_NAME", type: "string", width: "5%" },
                { field: "STORE_NAME_EN", title: "STORE_NAME_EN", type: "string", width: "10%" },

                { field: "INCO_TERM_NAME_EN", title: "Inco Term", type: "string", width: "10%" },
                { field: "PAYM_TERM_NAME_EN", title: "Payment Term", type: "string", width: "10%" },
                { field: "SHIP_MODE_NAME_EN", title: "Shipment Mode", type: "string", width: "10%" },

                //, HR_COMPANY_ID, , , IS_LOCAL_CASH_P, SCM_SUPPLIER_ID, CASH_SUPL_NAME, , SUP_SNAME
                //RF_PAYM_TERM_ID, RF_SHIP_MODE_ID, RF_CURRENCY_ID, TERMS_DESC, REMARKS, LK_PO_STATUS_ID, , 
                //REVISION_DT, REV_REASON, SOURCE_NAME, , , COMP_NAME_EN, , 
                //DELV_PLC_NAME_EN, , , PO_STATUS

                {
                    
                    title: "Action",
                    template: '<a ui-sref="GeneratePO({pCM_IMP_PO_H_ID:dataItem.CM_IMP_PO_H_ID,pREVISE:0})" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                        ' <a ui-sref="GeneratePO({pCM_IMP_PO_H_ID:dataItem.CM_IMP_PO_H_ID,pREVISE:1})" class="btn btn-xs yellow-gold"><i class="fa fa-edit"> Amendment</i></a></a>',
                    width: "10%"

                    //title: "Action",
                    //template: function () {
                    //    return "</a><a ui-sref='GeneratePO({pCM_IMP_PO_H_ID:dataItem.CM_IMP_PO_H_ID,pREVISE:0})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a> <a ui-sref='GeneratePO({pCM_IMP_PO_H_ID:dataItem.CM_IMP_PO_H_ID,pREVISE:1})' class='btn btn-xs blue'><i class='fa fa-edit'> Revise</i></a></a>";
                    //},
                    //width: "10%"
                }
            ]
        };


    }

})();