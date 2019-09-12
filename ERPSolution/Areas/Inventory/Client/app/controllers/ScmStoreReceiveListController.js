(function () {
    'use strict';
    angular.module('multitex.inventory').controller('ScmStoreReceiveListController', ['$q', 'config', 'InventoryDataService', '$stateParams', '$state', '$scope', '$filter', ScmStoreReceiveListController]);
    function ScmStoreReceiveListController($q, config, InventoryDataService, $stateParams, $state, $scope, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = {}
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [GetSupplierList(), GetStoreList(), GetReqTypeList(), GetCompanyList(), dailyReceiveList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.MRR_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.MRR_DT_LNopened = true;
        }

        vm.searchData = function () {
            dailyReceiveList();
        }


        function GetSupplierList() {
            return vm.supplierList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        InventoryDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAllByCat/3,4').then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };


        function GetReqTypeList() {
            return vm.reqTypeList = {
                optionLabel: "-- Select Req Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
                                var list = _.filter(res, function (o) { return o.IS_R_I == "R" && (o.RF_REQ_TYPE_ID == 1 || o.RF_REQ_TYPE_ID == 2) });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "REQ_TYPE_NAME",
                dataValueField: "RF_REQ_TYPE_ID"
            };
        };

        function GetStoreList() {

            return vm.reqSourceList = {
                optionLabel: "-- Select Store --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
                                var list = _.filter(res, function (x) { return x.SCM_STORE_ID == 12 || x.SCM_STORE_ID == 17 });
                                e.success(list);
                            });
                        }
                    }
                },
                dataTextField: "STORE_NAME_EN",
                dataValueField: "SCM_STORE_ID"
            };
        };


        function GetCompanyList() {

            return vm.companyList = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            InventoryDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        };


        function dailyReceiveList() {
            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = InventoryDataService.kFilterStr2QueryParam(params.filter);
                        if (pm.length > 0) {
                            InventoryDataService.getDataByFullUrl('/api/inv/StoreReceive/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm).then(function (res) {
                                e.success(res);
                            });
                        }
                        else {
                            if (vm.form.MRR_DT) {
                                var _mrrDate = $filter('date')(vm.form.MRR_DT, 'dd-MMM-yyyy');
                                vm.form.MRR_DT = _mrrDate;
                            }

                            InventoryDataService.getDataByFullUrl('/api/inv/StoreReceive/SelectAll/' + params.page + '/' + params.pageSize + '?pRF_REQ_TYPE_ID=' + vm.form.RF_REQ_TYPE_ID + '&pMRR_NO=' + (vm.form.MRR_NO || "") + '&pMRR_DT=' + (vm.form.MRR_DT || "") + '&pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID || "") + '&pSCM_STORE_ID=' + (vm.form.SCM_STORE_ID || "") + '&pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || "") + '&pRF_REQ_TYPE_ID=' + (vm.form.RF_REQ_TYPE_ID || "") + '&pIMP_LC_NO=' + (vm.form.IMP_LC_NO || "")).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },                
                schema: {
                    data: "data",
                    total: 'total',
                    model: {
                        fields: {
                            DYE_DC_RCV_H_ID: { type: "string" },
                            DC_MRR_NO: { type: "string" },
                            DC_MRR_DT: { type: "date" },
                            COMP_NAME_EN: { type: "string" },
                            PAY_MTHD_NAME: { type: "string" },
                            //FROM_ST_NAME: { type: "string" },
                            SOURCE_TYPE: { type: "string" },
                            CHALAN_NO: { type: "string" },
                            IMP_LC_NO: { type: "string" },
                            REMARKS: { type: "string" }
                        }
                    }
                }
            })
        }


        vm.printMRR = function (dataItem) {

            vm.form.REPORT_CODE = 'RPT-3000';

            vm.form.SCM_STR_RCV_H_ID = dataItem.SCM_STR_RCV_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Inv/InvReport/PreviewReportRDLC');
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
            columns: [
                { field: "MRR_NO", title: "MRR No", type: "string", width: "10%" },
                { field: "MRR_DT", title: "MRR Date", type: "date", template: "#= kendo.toString(kendo.parseDate(MRR_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "12%" },
                //{ field: "REQ_TYPE_NAME", title: "Req. Type", type: "string", width: "13%" },
                { field: "PAY_MTHD_NAME", title: "Pay Method", type: "string", width: "10%" },
                { field: "SOURCE_TYPE", title: "Source Type", type: "string", width: "10%" },
                { field: "CHALAN_NO", title: "Challan No", type: "string", width: "8%" },
                { field: "IMP_LC_NO", title: "L/C #", type: "string", width: "10%" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="ScmStoreReceive({pSCM_STR_RCV_H_ID:dataItem.SCM_STR_RCV_H_ID})" class="btn btn-xs green"><i class="fa fa-eye"> View</i></a> <a ui-sref="ScmStoreReceive({pSCM_STR_RCV_H_ID:dataItem.SCM_STR_RCV_H_ID})"  ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a> <a ui-sref="ScmReceiveReturn({pSCM_STR_RCV_H_ID:dataItem.SCM_STR_RCV_H_ID})"  ng-if="dataItem.IS_FINALIZED!=' + "'N'" + '" class="btn btn-xs blue"><i class="fa fa-edit"> Return</i></a> <a ng-click="vm.printMRR(dataItem);" class="btn btn-xs green"><i class="fa fa-print"> Print</i></a></a>';
                    },
                    width: "10%"
                }
            ]
        };
    }

})();