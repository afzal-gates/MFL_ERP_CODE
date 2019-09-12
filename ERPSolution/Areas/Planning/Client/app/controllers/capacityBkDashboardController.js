(function () {
    'use strict';
    angular.module('multitex.planning').controller('CapacityBkDashboardController', ['$q', 'config', 'PlanningDataService', '$stateParams', '$state', '$scope', 'Dialog', CapacityBkDashboardController]);
    function CapacityBkDashboardController($q, config, PlanningDataService, $stateParams, $state, $scope, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getFiscalYear()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getTabDatas(pRF_FISCAL_YEAR_ID) {
            return PlanningDataService.getDataByFullUrl('/api/pln/CapctiClndr/GetClndrMonthList?pRF_FISCAL_YEAR_ID=' + (pRF_FISCAL_YEAR_ID || 0))
                .then(function (data) {
                    
                    vm.tabList = data;
                });
        };


        function getFiscalYear() {
            return vm.fiscalYearOpt = {
                optionLabel: "--- Year ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return PlanningDataService.getDataByFullUrl('/api/common/GetPayFiscalYear').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var ds = this.dataSource.data();
                    var act_data = ds.find(function (o) {
                        return o.IS_ACTIVE === 'Y';
                    });

                    if (act_data) {
                        e.sender.value(act_data.RF_FISCAL_YEAR_ID);
                        vm.form['RF_FISCAL_YEAR_ID'] = act_data.RF_FISCAL_YEAR_ID;
                        getTabDatas(act_data.RF_FISCAL_YEAR_ID);
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }

        vm.onFiscalYearChange = function (e) {
            var item = e.sender.dataItem(e.sender.item);
           
            getTabDatas(item.RF_FISCAL_YEAR_ID);            
        }

        vm.onSelect = function (tab) {
            $state.go('CapacityBkDashboard.details', { pGMT_PROD_PLN_CLNDR_ID: tab.GMT_PROD_PLN_CLNDR_ID });
        };

    }

})();

(function () {
    'use strict';
    angular.module('multitex.planning').controller('CapacityBkDashboardDtlController', ['$q', 'config', 'PlanningDataService', '$stateParams', '$state', '$scope', 'Dialog', CapacityBkDashboardDtlController]);
    function CapacityBkDashboardDtlController($q, config, PlanningDataService, $stateParams, $state, $scope, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getWeekData(), getLineChartData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getWeekData () {
            return PlanningDataService.getDataByFullUrl('/api/pln/CapctiClndr/GetClndrWkList?pRF_FISCAL_YEAR_ID&pRF_CAL_MONTH_ID&pPARENT_ID=' + ($stateParams.pGMT_PROD_PLN_CLNDR_ID || 0)).then(function (res) {
                vm.tabList = [{ CAL_WK_CODE: 'All', GMT_PROD_PLN_CLNDR_ID: '', IS_TAB_ACT: true }].concat(res);
                console.log(vm.tabList);
            });
        };


        vm.onSelect = function (tab) {

            $state.go('CapacityBkDashboard.details.board', { pGMT_PROD_PLN_CLNDR_ID_WK: tab.GMT_PROD_PLN_CLNDR_ID });
        };

        function getLineChartData() {
            PlanningDataService.getDataByFullUrl(' /api/pln/PlanCommon/getGmtPlanCapcityDshBrdDataLineChrt?pGMT_PROD_PLN_CLNDR_ID_MN=' + ($stateParams.pGMT_PROD_PLN_CLNDR_ID || 0)).then(function (res) {
                vm.lineChart = res.map(function (o) {
                    o['categoryAxis'] = {
                        categories: o.WK_CODE_LIST,
                        majorGridLines: {
                            visible: false
                        },
                        labels: {
                            rotation: "auto",
                            padding: { top: 5 }
                        },
                    };
                    return o;

                });
            });
        }
        $scope.valueAxis = {
            labels: {
                format: "{0}"
            },
            line: {
                visible: false
            },
            axisCrossingValue: -10
        };


        $scope.tooltip = {
            visible: true,
            format: "{0}%",
            template: "#= series.name #: #= value #"
        };



       // $scope.series = [{
       //     name: "Target Qty",
       //     color: "#3870A8",
       //     data: [10000000, 7000000, 6000000]
       // }, {
       //     name: "Booked Qty",
       //     color: "#7E622A",
       //     data: [1000, 70000, 60000]
       // },
       // {
       //     name: "Available SAH",
       //     color: "#f89764",
       //     data: [1000999, 7009900, 6990000]
       // },

       // {
       //     name: "Booked SAH",
       //     color: "#f89764",
       //     data: [109900, 700000, 6000000]
       // },

       // {
       //     name: "Target FOB",
       //     color: "#f89764",
       //     data: [100000, 7000000, 6000000]
       // }
       // ,
       // {
       //     name: "Booked FOB",
       //     color: "#f89764",
       //     data: [100000, 7000000, 600000]
       // },

       // {
       // name: "Target CM",
       // color: "#f89764",
       // data: [100000, 7000000, 6000000]
       //}
       //,
       // {
       //     name: "Booked CM",
       //     color: "#f89764",
       //     data: [100000, 7000000, 600000]
       // }



       // ];

       // $scope.categoryExis = {
       //     categories: ['Basic', 'Polo', 'Fancy'],
       //     line: {
       //         visible: false
       //     },
       //     labels: {
       //         padding: { top: 10 }
       //     }
       // };

       // $scope.valueExis = {
       //     labels: {
       //         format: "{0}"
       //     },
       //     line: {
       //         visible: false
       //     },
       //     axisCrossingValue: 0
       // };



       // //vm.companyOpt = {
       // //    optionLabel: "-- All --",
       // //    filter: "contains",
       // //    autoBind: true,
       // //    dataTextField: "COMP_NAME_EN",
       // //    dataValueField: "HR_COMPANY_ID",
       // //    dataSource: {
       // //        transport: {
       // //            read: function (e) {
       // //                PlanningDataService.getDataByUrl('/GmtLineLoad/getCompanyData').then(function (res) {
       // //                    e.success(res);
       // //                }, function (err) {
       // //                    console.log(err);
       // //                });
       // //            }
       // //        }
       // //    },
       // //    change: function (e) {
       // //        var dataItem = this.dataItem(e.item);
       // //        return vm.officeDs = new kendo.data.DataSource({
       // //            transport: {
       // //                read: function (e) {

       // //                    PlanningDataService.getDataByUrl('/GmtLineLoad/getOfficeList?pHR_COMPANY_ID=' + (dataItem.HR_COMPANY_ID || '')).then(function (res) {
       // //                        e.success(res);
       // //                    });
       // //                }
       // //            }
       // //        });
       // //    }
       // //};

       // //vm.officeOpt = {
       // //    optionLabel: "-- All --",
       // //    filter: "contains",
       // //    autoBind: true,
       // //    dataTextField: "OFFICE_NAME_EN",
       // //    dataValueField: "HR_OFFICE_ID",
       // //    change: function (e) {
       // //        var ditm = this.dataItem(e.item);
       // //        return vm.floorDs = new kendo.data.DataSource({
       // //            transport: {
       // //                read: function (e) {
       // //                    PlanningDataService.getDataByUrl('/GmtLineLoad/getFloorData?pHR_OFFICE_ID=' + (ditm.HR_OFFICE_ID || '')).then(function (res) {
       // //                        e.success(res);
       // //                    });
       // //                }
       // //            }
       // //        });
       // //    }
       // //};




       // console.log($stateParams);

    }

})();

(function () {
    'use strict';
    angular.module('multitex.planning').controller('CapacityBkDashboardDtlChartController', ['$q', 'config', 'PlanningDataService', '$stateParams', '$state', '$scope', 'Dialog', '$modal', CapacityBkDashboardDtlChartController]);
    function CapacityBkDashboardDtlChartController($q, config, PlanningDataService, $stateParams, $state, $scope, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getGmtPlanCapcityDshBrd()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getGmtPlanCapcityDshBrd() {
            return PlanningDataService.getDataByFullUrl('/api/pln/PlanCommon/getGmtPlanCapcityDshBrdData?pGMT_PROD_PLN_CLNDR_ID_MN=' + $stateParams.pGMT_PROD_PLN_CLNDR_ID + '&pGMT_PROD_PLN_CLNDR_ID_WK=' + ($stateParams.pGMT_PROD_PLN_CLNDR_ID_WK||'')).then(function (res) {
                vm.chartDatas = res.map(function (obj) {
                    obj['order_lists_ds'] = new kendo.data.DataSource({
                        data: obj.order_lists
                    });
                    return obj;
                });
            });
        }

        vm.onReset = function () {
            getGmtPlanCapcityDshBrd();
        }
        vm.save = function (data) {

            var data2save = [];

            data.forEach(function (obj) {
                if (obj.dirty) {
                    data2save.push({ MC_ORDER_SHIP_ID: obj.MC_ORDER_SHIP_ID, MC_ORDER_H_ID: obj.MC_ORDER_H_ID, SHIP_DT: moment(obj.SHIP_DT).format("DD-MMM-YYYY") });
                }
            });

            if (data2save.length) {

                Dialog.confirm('Do you really want to update?', 'Confirmation', ['Yes', 'No'])
                    .then(function () {
                        return PlanningDataService.saveDataByFullUrl({}, '/api/pln/PlanCommon/GmtPlnOrderShipDtChange?pXML=' + config.xmlStringShort(data2save)).then(function (res) {
                            vm.errors = undefined;
                            if (res.success === false) {
                                vm.errors = [];
                                vm.errors = res.errors;
                            }
                            else {

                                res['data'] = angular.fromJson(res.jsonStr);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    $state.go('CapacityBkDashboard.details.board', { pGMT_PROD_PLN_CLNDR_ID: $stateParams.pGMT_PROD_PLN_CLNDR_ID, pGMT_PROD_PLN_CLNDR_ID_WK: ($stateParams.pGMT_PROD_PLN_CLNDR_ID_WK || '') }, { reload: 'CapacityBkDashboard.details' });
                                }
                                config.appToastMsg(res.data.PMSG);
                            }
                        }).catch(function (message) {
                            exception.catcher('XHR loading Failded')(message);
                        });
                    });
            }
        }

        $scope.electricity = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    e.success([]);
                }
            },
            sort: {
                field: "year",
                dir: "asc"
            }
        });

        $scope.categoryExis = {
            categories: ['Basic', 'Fency', 'Polo'],
            line: {
                visible: false
            },
            labels: {
                padding: { top: 10 }
            }
        };

        $scope.valueExis = {
            labels: {
                format: "{0}"
            },
            line: {
                visible: false
            },
            axisCrossingValue: 0
        };

        $scope.tooltip = {
            visible: true,
            format: "{0}",
            template: "#= series.name #: #= value #"
        };
        kendo.culture().calendar.firstDay = 6;


        $scope.openOrdShipExtentionModal = function (pMC_ORDER_SHIP_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Home/_UpdateOrdeShipDataModal',
                controller: 'OrdShipDateExtModalController',
                size: 'lg',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    V_MC_ORDER_SHIP_ID: function () {
                        return pMC_ORDER_SHIP_ID;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log('closed');
                getGmtPlanCapcityDshBrd();
            });
        };


        function getOrderList() {
            return PlanningDataService.getDataByUrl('/StyleHExt/MainStyleWrtStyleHExtList?pMC_STYLE_H_ID=' + $scope.$parent.StyleData.MC_STYLE_H_ID).then(function (res) {
                vm.OrderListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };



        vm.OrderListOpt = {
            schema: {
                model: {
                    fields: {
                        SHIP_DT: { type: "date" },
                        ORDER_NO: { type: "string" },
                        DATA_STATUS: { type: "string" },
                        PLAN_STATUS: { type: "string" },
                        LK_DATA_NAME_EN: { type: "string" },
                        LINE_CODE_LST: { type: "string" }
                    }
                }
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

            pageable: false,
            sortable: true,
            selectable: true,
            height: '500px',
            scrollable: true,
            columns: [
                
                {
                    field: "Cat", title: "Cat", width: "10%",
                    template: '<b>#=LK_DATA_NAME_EN#</b> <h6><b> # if (IS_PROV == \"Y\" ) { # <span class=\"label label-warning\">Projected</span> # } #  # if (LK_ORD_STATUS_ID == 365 ) { # <span class=\"label label-danger\">Shipout</span> # } #   # if (IS_LOADABLE == 0 ) { # <span class=\"label label-info\">Data Missing</span> # } # </b></h6>'
                },
                {
                    field: "Style", title: "Style", width: "15%",
                    template: "<b>#= ORDER_NO # </b> <br><small><i>[#=STYLE_NO#]</i></small> <br><small>#=BYR_ACC_NAME_EN#</small>",
                },


                {
                    field: "SHIP_DT", title: "Ship", width: "15%", type: "date",
                    template: function() {
                        return "{{dataItem.SHIP_DT|date:'dd-MMM-yyyy'}} <br><small>({{ dataItem.TOT_ORD_QTY }} Pcs)</small> <br><small><a  ng-click='openOrdShipExtentionModal(dataItem.MC_ORDER_SHIP_ID)' class='btn btn-sm btn-link'>ShipDtExt</a></small> "
                    }
                },
                {
                    field: "SHIP_OFFSET", title: "Ship Offset", type: "string", width: "10%",
                    template: function () {
                        return "<h4><span class='badge' ng-class=\"{'badge-danger': dataItem.SHIP_OFFSET > 0 }\"> {{ dataItem.SHIP_OFFSET > 0 ? 'D': 'E' }} [{{dataItem.SHIP_OFFSET* (dataItem.SHIP_OFFSET < 0 ?  -1: 1)}}]</span> </h4>";
                    },

                },

                {
                    field: "SEW_START_DT", title: "Plan Start", width: "15%",
                    template: "#if (SEW_START_DT) {# #= kendo.toString( new Date(SEW_START_DT),'dd-MMM-yyyy h tt')# #} else {# <span class='badge badge-warning'><small>No Plan</small></span> #}#",
                },
                {
                    field: "SEW_END_DT", title: "Plan End", width: "15%",
                    template: "#if (SEW_END_DT) {# #= kendo.toString(new Date(SEW_END_DT),'dd-MMM-yyyy h tt')# #} else {# <span class='badge badge-warning'><small>No Plan</small></span> #}#",
                },
                {
                    field: "Lines", title: "Lines", width: "15%",
                    template: '<b>#=LINE_CODE_LST#</b>'
                },
                {
                    field: "ALLOCATED_QTY", title: "Allocation", width: "10%", type: "date",
                    template: "#= ALLOCATED_QTY #  <br><small> #if ((ALLOCATED_QTY/TOT_ORD_QTY) < 1) {# <span class='badge badge-warning'><b>#= kendo.toString((ALLOCATED_QTY/TOT_ORD_QTY)*100, 'n')#%</b></span>  #}#</small>",
                }

                
            ]
        };

        function nonEditor(container, options) {
            container.text(options.model[options.field]);
        }

        function dateEditor(container, options) {
 
            $('<input type="text" data-value-field="SHIP_DT" data-bind="value:' + options.field + '"  />')
                    .appendTo(container)
                    .kendoDatePicker({
                        format: "dd-MMM-yyyy",
                        value: new Date(options.model.SHIP_DT),
                        change: function () {
                            options.model.dirty = true;
                        }
                    });
        }


    }

})();


