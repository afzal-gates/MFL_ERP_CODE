(function () {
    'use strict';
    angular.module('multitex.mrc').controller('ProjectionOrderListController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', '$filter', ProjectionOrderListController]);
    function ProjectionOrderListController($q, config, MrcDataService, $stateParams, $state, $scope, $filter) {

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
                        MrcDataService.getDataByFullUrl('/api/purchase/supplierprofile/SelectAllByCat/3,4').then(function (res) {
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
                            MrcDataService.getDataByFullUrl('/api/common/GetReqType').then(function (res) {
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
                            MrcDataService.getDataByFullUrl('/api/common/GetStoreInfo').then(function (res) {
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
                            MrcDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {

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
                serverSorting: false,
                serverFiltering: true,
                pageSize: 20,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var pm = MrcDataService.kFilterStr2QueryParam(params.filter);
                        if (pm.length > 0) {

                            MrcDataService.getDataByFullUrl('/api/mrc/ProjectionFabBk/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm).then(function (res) {
                                e.success(res);
                            });
                        }
                        else {
                            //if (vm.form.MRR_DT) {
                            //    var _mrrDate = $filter('date')(vm.form.MRR_DT, 'yyyy-MM-ddTHH:mm:ss');
                            //    vm.form.MRR_DT = _mrrDate;
                            //}

                            MrcDataService.getDataByFullUrl('/api/mrc/ProjectionFabBk/SelectAll/' + params.page + '/' + params.pageSize + '?' + pm).then(function (res) {
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
                            MC_PROV_FAB_BK_H_ID: { type: "string" },
                            PROV_FAB_BK_NO: { type: "string" },
                            PROV_FAB_BK_DT: { type: "date" },
                            REVISION_NO: { type: "string" },
                            COMP_NAME_EN: { type: "string" },
                            STYLE_NO: { type: "string" },
                            ORDER_NO: { type: "string" },
                            ORDER_DT: { type: "date" },
                            SHIP_DT: { type: "date" },
                            BUYER_NAME_EN: { type: "string" },
                            BYR_ACC_NAME_EN: { type: "string" },

                        }
                    }
                },

                group: [{ field: 'STYLE_NO' }],
                sort: [{ field: 'STYLE_NO', dir: 'asc' }, { field: 'BLK_FAB_REQ_DT', dir: 'desc' }]
            })
        }


        vm.printBooking = function (dataItem) {

            vm.form.REPORT_CODE = 'RPT-2009';

            vm.form.MC_ORDER_H_ID = dataItem.MC_ORDER_H_ID;
            vm.form.MC_PROV_FAB_BK_H_ID = dataItem.MC_PROV_FAB_BK_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Mrc/MrcReport/PreviewReportRDLC');
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
            scrollable: false,
            dataBound: function (e) {
                collapseAllGroups(this);
            },
            columns: [
                { field: "PROV_FAB_BK_NO", title: "Booking No", type: "string", width: "10%" },
                { field: "PROV_FAB_BK_DT", title: "Booking Date", type: "date", template: "#= kendo.toString(kendo.parseDate(PROV_FAB_BK_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "REVISION_NO", title: "Rev#", type: "string", width: "6%" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "7%" },
                { field: "STYLE_NO", title: "Style #", type: "string", width: "10%" },
                { field: "ORDER_NO", title: "Order #", type: "string", width: "10%" },
                { field: "ORDER_DT", title: "Order Date", type: "date", template: "#= kendo.toString(kendo.parseDate(ORDER_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "8%" },
                { field: "SHIP_DT", title: "Ship Date", type: "date", template: "#= kendo.toString(kendo.parseDate(SHIP_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "8%" },
                //{ field: "BUYER_NAME_EN", title: "Buyer", type: "string", width: "8%" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "10%" },
                {
                    title: "",
                    template: function () {
                        return '<ul id="myMenu" style="z-index:9999;" kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                                '<li><span style="color:black; z-index:9999">Navigate</span>' +
                                    '<ul style="width:150px; z-index:9999;">' +
                                        '<li><i class="fa fa-print"> Booking Edit</i>' +
                                            '<ul style="width:150px;"><li ng-repeat="itm in dataItem.REVISION_LIST"><a style="color:black">{{itm.REVISION_NO>0?"Revise-":"Orginal"}}{{itm.REVISION_NO>0?itm.REVISION_NO:""}}</a>' +

                                                '<ul style="width:150px;"><li ng-if="dataItem.PERMISSION>0">' +
                                                                            '<a ui-sref="ProjectionOrder({pMC_PROV_FAB_BK_H_ID:itm.MC_PROV_FAB_BK_H_ID, pIS_VIEW:' + "'N'" + '})" ><i> Edit</i></a>' +
                                                                            '</li><li ng-if="itm.ACTN_ROLE_FLAG==' + "'DN'" + ' && itm.IS_ACTIVE==' + "'Y'" + '"><a ui-sref="ProjectionOrder({pMC_PROV_FAB_BK_H_ID:itm.MC_PROV_FAB_BK_H_ID, pIS_VIEW:' + "'Y'" + '})"><i> View</i></a>' +
                                                                            '</li><li ng-if="itm.ACTN_ROLE_FLAG==' + "'DN'" + ' && itm.IS_ACTIVE==' + "'Y'" + '"><a ui-sref="ProjectionOrder({pMC_PROV_FAB_BK_H_ID:itm.MC_PROV_FAB_BK_H_ID, pIS_REVISE:' + "'Y'" + ', pIS_VIEW:' + "'N'" + '})"><i> Revise</i></a>' +
                                                                            '</li><li ng-if="itm.IS_FINALIZED==' + "'N'" + '"><a ui-sref="ProjectionOrder({pMC_PROV_FAB_BK_H_ID:itm.MC_PROV_FAB_BK_H_ID})"><i> Edit</i></a>' +
                                            '</li></ul>' +
                                        '</li></ul>' +
                                        '</li>' +
                                        '<li><i class="fa fa-print"> Booking Print</i>' +
                                            '<ul style="width:150px;"><li class="k-item k-state-default k-first" ng-repeat="itm in dataItem.REVISION_LIST"><a class="k-link" style="color:black" ng-click="vm.printBooking(itm)">{{itm.REVISION_NO>0?"Revise-"+itm.REVISION_NO:"Orginal"}}</a></li></ul>' +
                                        '</li>' +
                                    '</ul>' +
                                '</li></ul>';
                    },
                    width: "120px"
                },

                //{
                //    title: "Action",
                //    template: function () {
                //        return '</a><a ui-sref="ProjectionOrder({pMC_PROV_FAB_BK_H_ID:dataItem.MC_PROV_FAB_BK_H_ID, pIS_VIEW:' + "'N'" + '})" ng-if="dataItem.PERMISSION>0"  class="btn btn-xs yellow-gold"><i class="fa fa-edit"> Edit</i></a>' +
                //            ' <a ui-sref="ProjectionOrder({pMC_PROV_FAB_BK_H_ID:dataItem.MC_PROV_FAB_BK_H_ID, pIS_VIEW:' + "'Y'" + '})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" +  ' && dataItem.IS_ACTIVE==' + "'Y'" + '"  class="btn btn-xs yellow-gold"><i class="fa fa-edit"> View</i></a>' +
                //            ' <a ui-sref="ProjectionOrder({pMC_PROV_FAB_BK_H_ID:dataItem.MC_PROV_FAB_BK_H_ID, pIS_REVISE:' + "'Y'" + ', pIS_VIEW:' + "'N'" + '})" ng-if="dataItem.ACTN_ROLE_FLAG==' + "'DN'" + ' && dataItem.IS_ACTIVE==' + "'Y'" + '"  class="btn btn-xs blue"><i class="fa fa-recycle"> Revise</i></a>' +
                //            ' <a ui-sref="ProjectionOrder({pMC_PROV_FAB_BK_H_ID:dataItem.MC_PROV_FAB_BK_H_ID})"  ng-if="dataItem.IS_FINALIZED==' + "'N'" + '" class="btn btn-xs blue"><i class="fa fa-edit"> Edit</i></a>' +
                //            ' <a ng-click="vm.printBooking(dataItem);" class="btn btn-xs green"><i class="fa fa-print"> Print</i></a></a>';
                //    },
                //    width: "12%"
                //}
            ]
        };



        function collapseAllGroups(grid) {
            grid.table.find(".k-grouping-row").each(function () {
                grid.collapseGroup(this);
            });
        }


        vm.getBookingList = function () {

            vm.fabReqDataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                schema: {
                    data: "data",
                    total: "total"
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/BulkFabBk/BulkFabBookingList/' + vm.form.MC_BYR_ACC_ID + '/' + ((vm.form.MC_STYLE_H_ID > 0) ? vm.form.MC_STYLE_H_ID : null) + '/null?';
                        url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += MrcDataService.kFilterStr2QueryParam(params.filter);
                        console.log(params.filter);

                        return MrcDataService.getDataByUrl(url).then(function (res) {
                            angular.forEach(res.data, function (val, key) {
                                val['BLK_FAB_REQ_DT_STR'] = $filter('date')(val['BLK_FAB_REQ_DT'], vm.dtFormat);
                            });
                            e.success(res);
                            console.log(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                },
                pageSize: 10,
                group: [{ field: 'STYLE_NO' }],
                sort: [{ field: 'STYLE_NO', dir: 'asc' }, { field: 'BLK_FAB_REQ_DT', dir: 'desc' }]
            });
        };


        vm.navigateAction = function (dataItem, navigateId, pREVISION_NO) {
            if (navigateId == 1) {
                $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID });
            }
            else if (navigateId == 2) {
                vm.printBookingRecord(dataItem, pREVISION_NO);
            }
            else if (navigateId == 6) {
                $state.go('BudgetSheet', { pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID, pMC_STYLE_H_ID: dataItem.MC_STYLE_H_ID, pMC_STYL_BGT_H_ID: dataItem.MC_STYL_BGT_H_ID });
            }
            else if (navigateId == 7) {
                vm.printBudgetSheetReport(dataItem);
            }
        };


        vm.gridOptionsFabReq = {
            height: '500',
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
            dataBound: function (e) {
                collapseAllGroups(this);
            },
            columns: [
                { field: "STYLE_NO", title: "Style#", type: "string", hidden: true },
                { field: "BLK_FAB_REQ_DT_STR", title: "Booking Date", type: "date", format: "{0:dd-MMM-yyyy}", filterable: false },
                { field: "BLK_FAB_REQ_DT", title: "Booking Date A", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true },
                { field: "WORK_STYLE_NO_LST", title: "Work Style#", type: "string" },
                { field: "ORDER_NO_LST", title: "Order#", type: "string" },
                { field: "BLK_FAB_REQ_NO", title: "Booking Ref#", type: "string" },
                { field: "REMARKS", title: "Remarks", type: "string", hidden: true },
                {
                    title: "",
                    template: function () {
                        return '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                                '<li><span style="color:black">Navigate</span>' +
                                    '<ul style="width:150px;">' +
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,1)"><i class="fa fa-edit"> Booking Edit</i></a></li>' +
                                        '<li><i class="fa fa-print"> Booking Print</i>' +
                                            '<ul style="width:150px;"><li class="k-item k-state-default k-first" ng-repeat="itm in dataItem.REVISION_LIST"><a class="k-link" style="color:black" ng-click="vm.navigateAction(dataItem,2,itm.REVISION_NO)">{{itm.REV_REASON}}</a></li></ul>' +
                                        '</li>' +
                                        '<li disabled="disabled" style="padding:0px"><hr style="margin:0px;border-top: 1px solid grey;" /></li>' +
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,6)" ><i class="fa fa-edit"> Budget Edit</i></a></li>' +
                                        '<li><a style="color:black" ng-click="vm.navigateAction(dataItem,7)" ><i class="fa fa-print"> Budget Print</i></a></li>' +
                                    '</ul>' +
                                '</li></ul>';
                    },
                    width: "120px"
                }
            ]
        };

    }




})();