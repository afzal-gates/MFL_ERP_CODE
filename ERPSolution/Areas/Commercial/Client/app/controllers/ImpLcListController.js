
(function () {
    'use strict';
    angular.module('multitex.cmr').controller('ImpLcListController', ['$q', 'config', 'CmrDataService', 'commonDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', '$modal', ImpLcListController]);
    function ImpLcListController($q, config, CmrDataService, commonDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = { DOC_TITLE: 'Document No' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [loadAllSupplier(), GetCompanyList(), getBuyerListData(), getLcStatusList(), ImportLcList()];
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

            vm.notifyPartyList = new kendo.data.DataSource({
                data: []
            });


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

        vm.getBuyerNotifyPartyList = function (e) {
            var obj = e.sender.dataItem(e.item);
            if (vm.form.MC_BUYER_ID > 0) {
                vm.form.ADDRESS_PI = obj.ADDRESS_PI;
                vm.notifyPartyList = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            CmrDataService.getDataByFullUrl('/api/cmr/BuyerNotifyParty/GetBuyerNotifyPartyInfo?pMC_BUYER_ID=' + (vm.form.MC_BUYER_ID || 1)).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }
        }

        function getLcStatusList() {
            return vm.lcStatusList = {
                optionLabel: "-- LC Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CmrDataService.LookupListData(117).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        vm.searchData = function () {

            var pm = "";
            if (vm.form.HR_COMPANY_ID)
                pm = pm + "&pHR_COMPANY_ID=" + vm.form.HR_COMPANY_ID;
            if (vm.form.SCM_SUPPLIER_ID)
                pm = pm + "&pSCM_SUPPLIER_ID=" + vm.form.SCM_SUPPLIER_ID;
            if (vm.form.LK_LC_STS_ID)
                pm = pm + "&pLK_LC_STS_ID=" + vm.form.LK_LC_STS_ID;
            if (vm.form.ISSUE_DT) {
                var _isudate = $filter('date')(vm.form.ISSUE_DT, 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pISSUE_DT=" + _isudate;
            }
            if (vm.form.IMP_LC_NO)
                pm = pm + "&pIMP_LC_NO=" + vm.form.IMP_LC_NO;
            if (vm.form.LC_PI_NO)
                pm = pm + "&pLC_PI_NO=" + vm.form.LC_PI_NO;

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        CmrDataService.getDataByFullUrl('/api/cmr/ImportLC/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm).then(function (res) {
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
                        CmrDataService.getDataByFullUrl('/api/cmr/ImportLC/SelectAll/' + params.page + '/' + params.pageSize).then(function (res) {
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

                { field: "CM_IMP_LC_H_ID", hidden: true },

                { field: "IMP_LC_NO", title: "L/C No", width: "10%" },
                { field: "COMP_NAME_EN", title: "Company Name", width: "10%" },
                { field: "EXP_LCSC_NO", title: "Exp. L/C No", width: "10%" },
                { field: "PI_NO_IMP", title: "PI No", width: "10%" },
                { field: "SUP_TRD_NAME_EN", title: "Supplier", width: "10%" },
                { field: "ISSUE_DT", title: "Issue Date", type: "date", template: "#= kendo.toString(kendo.parseDate(ISSUE_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },

                { field: "LC_TYPE_NAME_EN", title: "L/C Type", width: "10%" },
                { field: "REVISION_NO", title: "Revision No", width: "10%" },
                { field: "ACTN_STATUS_NAME", title: "Status", width: "10%" },

                {
                    title: "",
                    template: '<a ui-sref="ImpLc({pCM_IMP_LC_H_ID:dataItem.CM_IMP_LC_H_ID,pREVISE:0})" ng-if="dataItem.ACTN_ROLE_FLAG!=' + "'DN'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                        '<a ui-sref="ImpLc({pCM_IMP_LC_H_ID:dataItem.CM_IMP_LC_H_ID,pREVISE:1})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" + '" class="btn btn-xs yellow-gold"><i class="fa fa-edit"> Amendment</i></a></a>',
                    //'<a ui-sref="IMPLcContract({pCM_IMP_LC_H_ID:dataItem.CM_IMP_LC_H_ID})" ng-if="dataItem.LK_LC_STS_ID!=589" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a></a>',
                    width: "10%"
                }
            ],
        };


        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                data["CM_NOTIFY_PARTY_LST"] = !data.CM_NOTIFY_PARTY_LST ? '0' : data.CM_NOTIFY_PARTY_LST.join(',');
                data["DSG_PORT_LST"] = !data.DSG_PORT_LST ? '0' : data.DSG_PORT_LST.join(',');

                var id = vm.form.RF_TRAN_PORT_ID;

                return CmrDataService.saveDataByUrl(data, '/IMPortLCSalesContact/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                        }
                        loadData();

                    }
                });
            }
        };

    }

})();