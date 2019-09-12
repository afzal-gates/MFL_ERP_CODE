
(function () {
    'use strict';
    angular.module('multitex.cmr').controller('LcUDListController', ['$q', 'config', 'CmrDataService', 'commonDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', '$modal', LcUDListController]);
    function LcUDListController($q, config, CmrDataService, commonDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = {};
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [loadAllSupplier(), GetCompanyList(), getBuyerListData(), ImportLcList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.ISSUE_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ISSUE_DT_LNopened = true;
        }

        function loadAllSupplier() {

            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/purchase/SupplierProfile/SelectAllByCat/2').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        }


        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };


        function getBuyerListData() {

            vm.buyerList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CmrDataService.getDataByFullUrl('/api/mrc/buyer/SelectAll').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };


        vm.searchData = function () {

            var pm = "";
            //if (vm.form.HR_COMPANY_ID)
            //    pm = pm + "&pHR_COMPANY_ID=" + vm.form.HR_COMPANY_ID;
            if (vm.form.SCM_SUPPLIER_ID)
                pm = pm + "&pSCM_SUPPLIER_ID=" + vm.form.SCM_SUPPLIER_ID;
            if (vm.form.MC_BUYER_ID)
                pm = pm + "&pMC_BUYER_ID=" + vm.form.MC_BUYER_ID;

            if (vm.form.UD_NO)
                pm = pm + "&pUD_NO=" + vm.form.UD_NO;
            if (vm.form.LC_NO)
                pm = pm + "&pLC_NO=" + vm.form.LC_NO;

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        CmrDataService.getDataByFullUrl('/api/cmr/LcUD/GetAllUDInfo/' + params.page + '/' + params.pageSize + '?' + pm).then(function (res) {
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

        function ImportLcList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        CmrDataService.getDataByFullUrl('/api/cmr/LcUD/GetAllUDInfo/' + params.page + '/' + params.pageSize).then(function (res) {
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

                { field: "CM_UD_ID", hidden: true },
                { field: "CM_EXP_LC_H_ID", hidden: true },
                { field: "CM_IMP_LC_H_ID", hidden: true },

                { field: "UD_NO", title: "UD No", width: "10%" },
                { field: "UD_DT", title: "UD Date", type: "date", template: "#= kendo.toString(kendo.parseDate(UD_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "COMP_NAME_EN", title: "Company Name", width: "10%" },
                { field: "EXP_LCSC_NO", title: "Exp. L/C No", width: "10%" },
                { field: "IMP_LC_NO", title: "Imp. L/C No", width: "10%" },
                { field: "GMT_ASN_NAME_EN", title: "Authority", width: "10%" },
                { field: "AMND_NO", title: "Revision No", width: "10%" },
                {
                    title: "",
                    template: '<a ui-sref="LcUD({pCM_UD_H_ID:dataItem.CM_UD_H_ID,pREVISE:0})" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a>' +
                        '<a ui-sref="LcUD({pCM_UD_H_ID:dataItem.CM_UD_H_ID,pREVISE:1})" class="btn btn-xs yellow-gold"><i class="fa fa-edit"> Amendment</i></a></a>',
                    width: "10%"
                }
            ],
        };
    }
})();