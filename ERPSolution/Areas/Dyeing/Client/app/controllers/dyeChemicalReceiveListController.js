(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyeChemicalReceiveListController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', '$filter', '$sessionStorage', DyeChemicalReceiveListController]);
    function DyeChemicalReceiveListController($q, config, DyeingDataService, $stateParams, $state, $scope, $filter, $sessionStorage) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();
        vm.loading = 0;

        vm.form = $sessionStorage.SessionMessage ? $sessionStorage.SessionMessage : { PAGE_NO: "1" };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [dailyReceiveList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.DC_MRR_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.DC_MRR_DT_LNopened = true;
        }

        vm.searchData = function () {
            vm.loading = 0;
            var data = {
                DC_MRR_NO: vm.form.DC_MRR_NO,
                DC_MRR_DT: vm.form.DC_MRR_DT,
                COMP_NAME_EN: vm.form.COMP_NAME_EN,
                PAY_MTHD_NAME: vm.form.PAY_MTHD_NAME,
                LOC_SRC_TYPE_NAME: vm.form.LOC_SRC_TYPE_NAME,
                CHALAN_NO: vm.form.CHALAN_NO,
                IMP_LC_NO: vm.form.IMP_LC_NO,
                PAGE_NO: 1
            };
            vm.form.PAGE_NO = 1;
            $sessionStorage.SessionMessage = angular.copy(data);

            dailyReceiveList();
        }

        function dailyReceiveList() {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                page: (vm.form.PAGE_NO || page),
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
                        console.log(e);
                        var data = angular.copy($sessionStorage.SessionMessage);

                        if (pm.length > 0) {
                            DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalReceive/SelectAll/' + params.page + '/' + params.pageSize + '?pRF_REQ_TYPE_ID=1&' + pm).then(function (res) {
                                e.success(res);
                            });
                        }
                        else {
                            if (vm.form.DC_MRR_DT) {
                                var _mrrDate = $filter('date')(vm.form.DC_MRR_DT, 'yyyy-MM-ddTHH:mm:ss');
                                vm.form.DC_MRR_DT = _mrrDate;
                            }

                            DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalReceive/SelectAll/' + (vm.loading == 0 ? (data.PAGE_NO || 1) : params.page) + '/' + params.pageSize + '?pRF_REQ_TYPE_ID=1&pDC_MRR_NO=' + (vm.form.DC_MRR_NO || "") + '&pDC_MRR_DT=' + (vm.form.DC_MRR_DT || "") + '&pCOMP_NAME_EN=' + (vm.form.COMP_NAME_EN || "") + '&pPAY_MTHD_NAME=' + (vm.form.PAY_MTHD_NAME || "") + '&pLOC_SRC_TYPE_NAME=' + (vm.form.LOC_SRC_TYPE_NAME || "") + '&pCHALAN_NO=' + (vm.form.CHALAN_NO || "") + '&pIMP_LC_NO=' + (vm.form.IMP_LC_NO || "")).then(function (res) {
                                e.success(res);
                            });
                        }

                        $sessionStorage.SessionMessage.PAGE_NO = params.page;
                        vm.form.PAGE_NO = params.page;
                        vm.loading = 1;
                    }
                },
                //DC_MRR_NO=&DC_MRR_DT=&COMP_NAME_EN=&PAY_MTHD_NAME=&LOC_SRC_TYPE_NAME=&CHALAN_NO=&IMP_LC_NO=&REMARKS=
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
                            LOC_SRC_TYPE_NAME: { type: "string" },
                            CHALAN_NO: { type: "string" },
                            IMP_LC_NO: { type: "string" },
                            REMARKS: { type: "string" }
                        }
                    }
                }
            })
        }

        vm.gridOptions = {
            //dataSource: new kendo.data.DataSource({
            //    serverPaging: true,
            //    serverSorting: true,
            //    serverFiltering: true,
            //    pageSize: 20,
            //    transport: {
            //        read: function (e) {
            //            var webapi = new kendo.data.transports.webapi({});
            //            var params = webapi.parameterMap(e.data);
            //            var pm = DyeingDataService.kFilterStr2QueryParam(params.filter);
            //            DyeingDataService.getDataByFullUrl('/api/dye/DyeChemicalReceive/SelectAll/' + params.page + '/' + params.pageSize + '?pRF_REQ_TYPE_ID=1&' + pm).then(function (res) {
            //                e.success(res);
            //            });
            //        }
            //    },
            //    schema: {
            //        data: "data",
            //        total: 'total'
            //    }
            //}),
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
                { field: "DC_MRR_NO", title: "MRR No", type: "string", width: "10%" },
                { field: "DC_MRR_DT", title: "MRR Date", type: "date", template: "#= kendo.toString(kendo.parseDate(DC_MRR_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                //{ field: "REQ_TYPE_NAME", title: "REQ_TYPE_NAME", type: "string", width: "50px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "12%" },
                //{ field: "REQ_TYPE_NAME", title: "Req. Type", type: "string", width: "13%" },
                { field: "PAY_MTHD_NAME", title: "Pay Method", type: "string", width: "10%" },
                { field: "LOC_SRC_TYPE_NAME", title: "Source Type", type: "string", width: "10%" },
                { field: "CHALAN_NO", title: "Challan No", type: "string", width: "8%" },
                { field: "IMP_LC_NO", title: "L/C #", type: "string", width: "10%" },
                { field: "REMARKS", title: "Remarks", type: "string", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return '</a><a ui-sref="DyeChemicalReceiveView({pDYE_DC_RCV_H_ID:dataItem.DYE_DC_RCV_H_ID})" class="btn btn-xs green"><i class="fa fa-eye"> View</i></a> <a ui-sref="DyeChemicalReceive({pDYE_DC_RCV_H_ID:dataItem.DYE_DC_RCV_H_ID})"  ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a></a>';
                    },
                    width: "10%"
                }
            ]
        };
    }

})();