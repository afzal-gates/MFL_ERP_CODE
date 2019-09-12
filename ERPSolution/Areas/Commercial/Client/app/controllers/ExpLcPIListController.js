
(function () {
    'use strict';
    angular.module('multitex.cmr').controller('ExpLcPIListController', ['$q', 'config', 'CmrDataService', 'commonDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', '$modal', ExpLcPIListController]);
    function ExpLcPIListController($q, config, CmrDataService, commonDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = { DOC_TITLE: 'Document No' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetCompanyList(), getBuyerListData(), getLcStatusList(), contactList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.PI_DT_EXP_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.PI_DT_EXP_LNopened = true;
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

        vm.SenderbankBranchList = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    CmrDataService.getDataByFullUrl('/api/common/GetBankBranchList').then(function (res) {

                        e.success([{
                            BANK_BRANCH_NAME_EN: '--New Branch--',
                            RF_BANK_BRANCH_ID: -1
                        }].concat(res));

                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });


        vm.searchData = function () {

            var pm = "";
            if (vm.form.HR_COMPANY_ID)
                pm = pm + "&pHR_COMPANY_ID=" + vm.form.HR_COMPANY_ID;
            if (vm.form.MC_BUYER_ID)
                pm = pm + "&pMC_BUYER_ID=" + vm.form.MC_BUYER_ID;
            if (vm.form.LK_LC_STS_ID)
                pm = pm + "&pLK_LC_STS_ID=" + vm.form.LK_LC_STS_ID;
            if (vm.form.PI_DT_EXP) {
                var _isudate = $filter('date')(vm.form.PI_DT_EXP, 'yyyy-MM-ddTHH:mm:ss');
                pm = pm + "&pPI_DT_EXP=" + _isudate;
            }
            if (vm.form.PI_NO_EXP)
                pm = pm + "&pPI_NO_EXP=" + vm.form.PI_NO_EXP;
            if (vm.form.STYLE_ORDER_NO)
                pm = pm + "&pSTYLE_ORDER_NO=" + vm.form.STYLE_ORDER_NO;

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        CmrDataService.getDataByFullUrl('/api/cmr/ExportLCPI/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm).then(function (res) {
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

        function contactList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        CmrDataService.getDataByFullUrl('/api/cmr/ExportLCPI/SelectAll/' + params.page + '/' + params.pageSize).then(function (res) {
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

                { field: "CM_EXP_PI_H_ID", hidden: true },
                { field: "RPT_PATH_URL", hidden: true },
                
                { field: "PI_NO_EXP", title: "PI No", width: "10%" },
                { field: "COMP_NAME_EN", title: "Company Name", width: "15%" },
                { field: "BUYER_NAME_EN", title: "Buyer Name", width: "15%" },
                { field: "COUNTRY_NAME_EN", title: "Export Country", width: "15%" },
                { field: "PI_DT_EXP", title: "PI Date", type: "date", template: "#= kendo.toString(kendo.parseDate(PI_DT_EXP,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },

                { field: "STYLE_NO", title: "Style No", width: "10%" },
                { field: "ORDER_NO", title: "Order No", width: "10%" },
                { field: "ACTN_STATUS_NAME", title: "Status", width: "10%" },

                {
                    title: "",
                    template: '<a ui-sref="ExpLcPI({pCM_EXP_PI_H_ID:dataItem.CM_EXP_PI_H_ID,pREVISE:0})" ng-if="dataItem.ACTN_ROLE_FLAG!=' + "'DN'" + ' && dataItem.PERMISSION==1" class="btn btn-xs blue"><i class="fa fa-edit"> {{dataItem.NXT_ACTION_NAME}}</i></a>' +
                        ' <a ui-sref="ExpLcPI({pCM_EXP_PI_H_ID:dataItem.CM_EXP_PI_H_ID,pREVISE:1})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" + '" class="btn btn-xs yellow-gold"><i class="fa fa-edit"> Amendment</i></a></a>',
                    //'<a ui-sref="ExpLcContract({pCM_EXP_PI_H_ID:dataItem.CM_EXP_PI_H_ID})" ng-if="dataItem.LK_LC_STS_ID!=589" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a></a>',
                    width: "10%"
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