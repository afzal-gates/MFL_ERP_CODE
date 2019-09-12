//=============== Start for ByrWiseCapctyAllocController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('ByrWiseCapctyAllocController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', '$http', ByrWiseCapctyAllocController]);
    function ByrWiseCapctyAllocController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modal, $timeout, $http) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        
        vm.errstyle = { 'border': '1px solid #f13d3d' };

        //var key = 'GMT_CAPACITY_MN_ID';

        vm.form = {};

        //vm.form = formData[key] ? formData : {
        //    GMT_CAPACITY_MN_ID: 0, LK_CALC_MTHD_ID: $stateParams.pLK_CALC_MTHD_ID, WORK_DAYS_IN_MN: 26, PCT_ABSENCE: 5, PCT_BACKLOG: 0, CORE_DEPT_ID: $stateParams.pCORE_DEPT_ID,
        //    HR_COMPANY_ID: ($stateParams.pHR_COMPANY_ID || ''),
        //    PROD_UNIT_ID: ($stateParams.pPROD_UNIT_ID || ''), GMT_PRODUCT_TYP_ID: ($stateParams.pGMT_PRODUCT_TYP_ID || ''),
        //    RF_FISCAL_YEAR_ID: ($stateParams.pRF_FISCAL_YEAR_ID || ''), GMT_PROD_PLN_CLNDR_ID: ($stateParams.pGMT_PROD_PLN_CLNDR_ID || ''),
        //    items : []
        //};

        //$scope.form = formData[key] ? formData : {
        //    GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: ''
        //};

        //console.log(formData);


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getClndrYearList(), getClndrMonthList()];
            return $q.all(promise).then(function () {

                vm.showSplash = false;
            });
        }
     

        function getClndrYearList() {
            vm.clndrYearOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FY_NAME_EN",
                dataValueField: "RF_FISCAL_YEAR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);

                    vm.form.RF_FISCAL_YEAR_ID = item.RF_FISCAL_YEAR_ID;
                    //vm.form.GMT_PROD_PLN_CLNDR_ID = 0;
                    $stateParams.pGMT_PROD_PLN_CLNDR_ID = 0;
                    
                    vm.clndrMonthDataSource.read();                    
                },
                dataBound: function (e) {

                    vm.clndrMonthDataSource.read();

                    if ($stateParams.pRF_FISCAL_YEAR_ID > 0) {
                        vm.form.RF_FISCAL_YEAR_ID = $stateParams.pRF_FISCAL_YEAR_ID;

                        $state.go('ByrWiseCapctyAlloc', { pRF_FISCAL_YEAR_ID: $stateParams.pRF_FISCAL_YEAR_ID, pGMT_PROD_PLN_CLNDR_ID: $stateParams.pGMT_PROD_PLN_CLNDR_ID }, { notify: false });
                    }                   
                }
            };

            return vm.clndrYearDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/CapctiClndr/GetClndrYearList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 12
            });
        };

        function getClndrMonthList() {
            
            vm.clndrMonthOption = {
                height: 400,
                sortable: false,
                scrollable: true,
                pageable: false,
                editable: false,
                selectable: "row",
                columns: [
                    { field: "MONTH_NAME_EN", title: "Month", width: "25%" }
                    //{
                    //    title: "Action",
                    //    template: function () {
                    //        return "<button type='button' class='btn btn-xs blue' ng-click='vm.editRow(dataItem, 56)' ng-show='dataItem.CUTTING_ID==0' >Not Ok</button>";
                    //    },
                    //    width: "15%"
                    //},
                ],
                change: function (e) {
                    var grid = $("#capctyMnGrid").data("kendoGrid");
                    //grid.select("tr:eq(1)");
                    var row = grid.select();
                    var item = grid.dataItem(row);

                    vm.form.GMT_PROD_PLN_CLNDR_ID = item.GMT_PROD_PLN_CLNDR_ID;
                    vm.mainMnByrWiseAllocDataSource.read();
                    vm.mnCapctyFree4ByrAllocDataSource.read();
                                        
                    $scope.$apply();
                    console.log(vm.form);

                    $state.go('ByrWiseCapctyAlloc', { pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID, pGMT_PROD_PLN_CLNDR_ID: vm.form.GMT_PROD_PLN_CLNDR_ID }, { notify: false });
                },
                dataBound: function (e) {

                    var grid = this;
                    $.each(grid.tbody.find('tr'), function (idx) {

                        var model = grid.dataItem(this);
                        if (idx == 0 && $stateParams.pGMT_PROD_PLN_CLNDR_ID == 0) {
                            vm.form.GMT_PROD_PLN_CLNDR_ID = model.GMT_PROD_PLN_CLNDR_ID;
                            vm.mainMnByrWiseAllocDataSource.read();
                            vm.mnCapctyFree4ByrAllocDataSource.read();

                            $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                        }
                        else if (model.GMT_PROD_PLN_CLNDR_ID == $stateParams.pGMT_PROD_PLN_CLNDR_ID && $stateParams.pGMT_PROD_PLN_CLNDR_ID > 0) {
                            vm.form.GMT_PROD_PLN_CLNDR_ID = model.GMT_PROD_PLN_CLNDR_ID;
                            vm.mainMnByrWiseAllocDataSource.read();
                            vm.mnCapctyFree4ByrAllocDataSource.read();

                            $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                        }

                    });

                    $state.go('ByrWiseCapctyAlloc', { pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID, pGMT_PROD_PLN_CLNDR_ID: vm.form.GMT_PROD_PLN_CLNDR_ID }, { notify: false });
                }
            };

            return vm.clndrMonthDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/CapctiClndr/GetClndrMonthList?pRF_FISCAL_YEAR_ID=' + (vm.form.RF_FISCAL_YEAR_ID || 0)).then(function (res) {
                            e.success(res);
                        });
                    }                                       
                },
                pageSize: 12
            });
        };

        //vm.onChangeCapctyMn = function (dataItem) {
        //    console.log(dataItem);
        //    vm.form.GMT_PROD_PLN_CLNDR_ID = dataItem.GMT_PROD_PLN_CLNDR_ID;
        //    $state.go('ByrWiseCapctyAlloc', { pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID, pGMT_PROD_PLN_CLNDR_ID: vm.form.GMT_PROD_PLN_CLNDR_ID }, { notify: false });

        //    vm.mainMnByrWiseAllocDataSource.read();
        //}

        vm.mainMnByrWiseAllocOption = {
            height: 635,
            scrollable: true,            
            sortable: true,
            //pageable: true,
            //groupable: true,
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
            columns: [                
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer A/c", width: "15%", footerTemplate: "Total: #=count#" },
                {
                    title: "Basic (Pcs)",
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: center; font-weight: bold"
                    },
                    width: "30%",
                    columns: [                        
                        {
                            title: "Allocation",
                            field: "BASIC_ALLOC_PCS",
                            footerTemplate: "#=sum#",
                            template: function () {
                                return "<ng-form name='frmMnBasicPcs'><input type='number' class='form-control' name='BASIC_ALLOC_PCS' ng-model='dataItem.BASIC_ALLOC_PCS' min='0' ng-style='(frmMnBasicPcs.BASIC_ALLOC_PCS.$error.min)? vm.errstyle:\"\"' ng-disabled='dataItem.IS_BASIC_EXISTS>0' /></ng-form>";
                            },                            
                            filterable: false
                        },
                        {
                            title: "Booked",
                            field: "BASIC_BOOKED_PCS",
                            footerTemplate: "#=sum#",
                            template: function () {
                                return "<ng-form name='frmMnBasicBookedPcs'><input type='number' class='form-control' name='BASIC_BOOKED_PCS' ng-model='dataItem.BASIC_BOOKED_PCS' min='0' ng-style='(frmMnBasicBookedPcs.BASIC_BOOKED_PCS.$error.min)? vm.errstyle:\"\"' disabled  /></ng-form>";
                            },
                            filterable: false
                        },
                        {
                            title: "Free Alloc",
                            field: "BASIC_FREE_PCS",
                            footerTemplate: "#=sum#",
                            template: function () {
                                return "<ng-form name='frmMnBasicFreePcs'><input type='number' class='form-control' name='BASIC_FREE_PCS' ng-model='dataItem.BASIC_FREE_PCS' min='0' ng-style='(frmMnBasicFreePcs.BASIC_FREE_PCS.$error.min)? vm.errstyle:\"\"' disabled  /></ng-form>";
                            },
                            filterable: false
                        }                        
                    ]                                        
                },
                {
                    title: "Fancy (Pcs)",
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: center; font-weight: bold"
                    },
                    width: "30%",
                    columns: [                        
                        {
                            title: "Allocation",
                            field: "FANCY_ALLOC_PCS",
                            footerTemplate: "#=sum#",
                            template: function () {
                                return "<ng-form name='frmMnFancyPcs'><input type='number' class='form-control' name='FANCY_ALLOC_PCS' ng-model='dataItem.FANCY_ALLOC_PCS' min='0' ng-style='(frmMnFancyPcs.FANCY_ALLOC_PCS.$error.min)? vm.errstyle:\"\"' ng-disabled='dataItem.IS_FANCY_EXISTS>0' /></ng-form>";
                            },                            
                            filterable: false
                        },
                        {
                            title: "Booked",
                            field: "FANCY_BOOKED_PCS",
                            footerTemplate: "#=sum#",
                            template: function () {
                                return "<ng-form name='frmMnFancyBookedPcs'><input type='number' class='form-control' name='FANCY_BOOKED_PCS' ng-model='dataItem.FANCY_BOOKED_PCS' min='0' ng-style='(frmMnFancyBookedPcs.FANCY_BOOKED_PCS.$error.min)? vm.errstyle:\"\"' disabled  /></ng-form>";
                            },
                            filterable: false
                        },
                        {
                            title: "Free Alloc",
                            field: "FANCY_FREE_PCS",
                            footerTemplate: "#=sum#",
                            template: function () {
                                return "<ng-form name='frmMnFancyFreePcs'><input type='number' class='form-control' name='FANCY_FREE_PCS' ng-model='dataItem.FANCY_FREE_PCS' min='0' ng-style='(frmMnFancyFreePcs.FANCY_FREE_PCS.$error.min)? vm.errstyle:\"\"' disabled  /></ng-form>";
                            },
                            filterable: false
                        }
                    ]                    
                },
                {
                    title: "Polo (Pcs)",
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: center; font-weight: bold"
                    },
                    width: "25%",
                    columns: [                        
                        {
                            title: "Allocation",
                            field: "POLO_ALLOC_PCS",
                            footerTemplate: "#=sum#",
                            template: function () {
                                return "<ng-form name='frmMnPoloPcs'><input type='number' class='form-control' name='POLO_ALLOC_PCS' ng-model='dataItem.POLO_ALLOC_PCS' min='0' ng-style='(frmMnPoloPcs.POLO_ALLOC_PCS.$error.min)? vm.errstyle:\"\"' ng-disabled='dataItem.IS_POLO_EXISTS>0' /></ng-form>";
                            },
                            filterable: false
                        },
                        {
                            title: "Booked",
                            field: "POLO_BOOKED_PCS",
                            footerTemplate: "#=sum#",
                            template: function () {
                                return "<ng-form name='frmMnPoloBookedPcs'><input type='number' class='form-control' name='POLO_BOOKED_PCS' ng-model='dataItem.POLO_BOOKED_PCS' min='0' ng-style='(frmMnPoloBookedPcs.POLO_BOOKED_PCS.$error.min)? vm.errstyle:\"\"' disabled /></ng-form>";
                            },
                            filterable: false
                        },
                        {
                            title: "Free Alloc",
                            field: "POLO_FREE_PCS",
                            footerTemplate: "#=sum#",
                            template: function () {
                                return "<ng-form name='frmMnPoloFreePcs'><input type='number' class='form-control' name='POLO_FREE_PCS' ng-model='dataItem.POLO_FREE_PCS' min='0' ng-style='(frmMnPoloFreePcs.POLO_FREE_PCS.$error.min)? vm.errstyle:\"\"' disabled  /></ng-form>";
                            },
                            filterable: false
                        }
                    ]                    
                }                
            ]
        };

        vm.mainMnByrWiseAllocDataSource = new kendo.data.DataSource({
            aggregate: [
                { field: "BYR_ACC_GRP_NAME_EN", aggregate: "count" },
                { field: "BASIC_ALLOC_PCS", aggregate: "sum" },
                { field: "BASIC_BOOKED_PCS", aggregate: "sum" },
                { field: "BASIC_FREE_PCS", aggregate: "sum" },
                { field: "FANCY_ALLOC_PCS", aggregate: "sum" },
                { field: "FANCY_BOOKED_PCS", aggregate: "sum" },
                { field: "FANCY_FREE_PCS", aggregate: "sum" },
                { field: "POLO_ALLOC_PCS", aggregate: "sum" },
                { field: "POLO_BOOKED_PCS", aggregate: "sum" },
                { field: "POLO_FREE_PCS", aggregate: "sum" }
            ],
            transport: {
                read: function (e) {
                    PlanningDataService.getDataByFullUrl('/api/pln/ProdCapcti/GetByrWiseMnCapcty?pGMT_PROD_PLN_CLNDR_ID=' + (vm.form.GMT_PROD_PLN_CLNDR_ID || 0)).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });
     
        vm.detailExpand = function (dtlDataItem) {
            console.log(dtlDataItem);

            if (dtlDataItem) {
                return PlanningDataService.getDataByFullUrl("/api/pln/ProdCapcti/GetByrWiseWkCapcty?pGMT_PROD_PLN_CLNDR_ID=" + vm.form.GMT_PROD_PLN_CLNDR_ID + '&pMC_BYR_ACC_GRP_ID=' + dtlDataItem.MC_BYR_ACC_GRP_ID).then(function (res) {
                   
                    console.log(res);
                    dtlDataItem['WEEK_LIST'] = res;
                    //getWkByrWiseSummery(dtlDataItem.MC_BYR_ACC_GRP_ID);

                }, function (err) {
                    console.log(err);
                });
            }            
           
        };

        vm.onChangeWkAllocPcs = function (dataItem) {
            console.log(dataItem);

            dataItem.FREE_PCS = dataItem.ALLOC_PCS - dataItem.BOOKED_PCS;
        }

        vm.wkByrWiseSummery = { BASIC_ALLOC_PCS: 0, BASIC_BOOKED_PCS: 0, BASIC_FREE_PCS: 0 };
        function getWkByrWiseSummery(pMC_BYR_ACC_GRP_ID) {
            var mnByrWiseAllocData = _.filter(vm.mainMnByrWiseAllocDataSource.data(), function (ob) { return ob.MC_BYR_ACC_GRP_ID == pMC_BYR_ACC_GRP_ID });

            var wkByrWiseAllocData = [];
            angular.forEach(mnByrWiseAllocData, function (val, key) {

                angular.forEach(val['WEEK_LIST'], function (val1, key1) {

                    angular.forEach(val1['WK_BYR_WISE_ALLOC_LIST'], function (val2, key2) {
                        wkByrWiseAllocData.push({
                            GMT_CAPACITY_BYR_ALLOC_ID: val2['GMT_CAPACITY_BYR_ALLOC_ID'], GMT_CAPACITY_WK_TOT_ID: val2['GMT_CAPACITY_WK_TOT_ID'],
                            GMT_PRODUCT_TYP_ID: val2['GMT_PRODUCT_TYP_ID'], GMT_PROD_PLN_CLNDR_ID: val2['GMT_PROD_PLN_CLNDR_ID'],
                            MC_BYR_ACC_GRP_ID: val2['MC_BYR_ACC_GRP_ID'], ALLOC_PCS: val2['ALLOC_PCS'], BOOKED_PCS: val2['BOOKED_PCS'], FREE_PCS: val2['FREE_PCS']
                        });
                    });
                });

            });

            vm.wkByrWiseSummery = {
                BASIC_ALLOC_PCS: _.sumBy(_.filter(wkByrWiseAllocData, function (ob) { return ob.GMT_PRODUCT_TYP_ID==184 }), function (o) { return (o.ALLOC_PCS || 0); }),
                BASIC_BOOKED_PCS: _.sumBy(_.filter(wkByrWiseAllocData, function (ob) { return ob.GMT_PRODUCT_TYP_ID == 184 }), function (o) { return (o.BOOKED_PCS || 0); }),
                BASIC_FREE_PCS: _.sumBy(_.filter(wkByrWiseAllocData, function (ob) { return ob.GMT_PRODUCT_TYP_ID == 184 }), function (o) { return (o.FREE_PCS || 0); }),

                FANCY_ALLOC_PCS: _.sumBy(_.filter(wkByrWiseAllocData, function (ob) { return ob.GMT_PRODUCT_TYP_ID==185 }), function (o) { return (o.ALLOC_PCS || 0); }),
                FANCY_BOOKED_PCS: _.sumBy(_.filter(wkByrWiseAllocData, function (ob) { return ob.GMT_PRODUCT_TYP_ID == 185 }), function (o) { return (o.BOOKED_PCS || 0); }),
                FANCY_FREE_PCS: _.sumBy(_.filter(wkByrWiseAllocData, function (ob) { return ob.GMT_PRODUCT_TYP_ID == 185 }), function (o) { return (o.FREE_PCS || 0); }),

                POLO_ALLOC_PCS: _.sumBy(_.filter(wkByrWiseAllocData, function (ob) { return ob.GMT_PRODUCT_TYP_ID==186 }), function (o) { return (o.ALLOC_PCS || 0); }),
                POLO_BOOKED_PCS: _.sumBy(_.filter(wkByrWiseAllocData, function (ob) { return ob.GMT_PRODUCT_TYP_ID == 186 }), function (o) { return (o.BOOKED_PCS || 0); }),
                POLO_FREE_PCS: _.sumBy(_.filter(wkByrWiseAllocData, function (ob) { return ob.GMT_PRODUCT_TYP_ID == 186 }), function (o) { return (o.FREE_PCS || 0); })
            };
        }

        vm.mnCapctyFree4ByrAllocOption = {
            height: 150,
            scrollable: true,
            sortable: true,
            //pageable: true,
            //groupable: true,
            filterable: false,
            columns: [
                { field: "GMT_PRODUCT_TYP_NAME", title: "Type", width: "26%", footerTemplate: "Total:" },
                {
                    field: "MN_FREE_CAP_PCS", title: "Capcty", width: "37%", footerTemplate: "#=sum#", attributes: { style: "text-align: right;" },
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: right; font-weight: bold"
                    },
                    footerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: right; font-weight: bold"
                    }
                },
                {
                    field: "MN_FREE_CAP_PCS4BYR_ALLOC", title: "Free", width: "37%", footerTemplate: "#=sum#", attributes: { style: "text-align: right;" },
                    headerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: right; font-weight: bold"
                    },
                    footerAttributes: {
                        "class": "table-header-cell",
                        style: "text-align: right; font-weight: bold"
                    }
                }
            ]
        }

        vm.mnCapctyFree4ByrAllocDataSource = new kendo.data.DataSource({
            aggregate: [                
                { field: "MN_FREE_CAP_PCS", aggregate: "sum" },
                { field: "MN_FREE_CAP_PCS4BYR_ALLOC", aggregate: "sum" }
            ],
            schema: {                
                model: {
                    id: "GMT_PRODUCT_TYP_NAME",
                    fields: {                        
                        MN_FREE_CAP_PCS: { type: "number", filterable: false },
                        MN_FREE_CAP_PCS4BYR_ALLOC: { type: "number", filterable: false }
                    }
                }
            },
            transport: {
                read: function (e) {
                    PlanningDataService.getDataByFullUrl('/api/pln/ProdCapcti/GetMnCapctyFree4ByrAlloc?pGMT_PROD_PLN_CLNDR_ID=' + (vm.form.GMT_PROD_PLN_CLNDR_ID || 0)).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

        vm.save = function () {
            
            var submitData = angular.copy(vm.form);
            var mnByrWiseAllocData = vm.mainMnByrWiseAllocDataSource.data();
            console.log(mnByrWiseAllocData);
            
            var wkByrWiseAllocData = [];
            angular.forEach(mnByrWiseAllocData, function (val, key) {

                angular.forEach(val['WEEK_LIST'], function (val1, key1) {

                    angular.forEach(val1['WK_BYR_WISE_ALLOC_LIST'], function (val2, key2) {
                        wkByrWiseAllocData.push({
                            GMT_CAPACITY_BYR_ALLOC_ID: val2['GMT_CAPACITY_BYR_ALLOC_ID'], GMT_CAPACITY_WK_TOT_ID: val2['GMT_CAPACITY_WK_TOT_ID'],
                            GMT_PRODUCT_TYP_ID: val2['GMT_PRODUCT_TYP_ID'], GMT_PROD_PLN_CLNDR_ID: val2['GMT_PROD_PLN_CLNDR_ID'],
                            MC_BYR_ACC_GRP_ID: val2['MC_BYR_ACC_GRP_ID'], ALLOC_PCS: val2['ALLOC_PCS']
                        });
                    });
                });
                
            });
            console.log(wkByrWiseAllocData);
                      
            submitData['MN_BYR_WISE_ALLOC_XML'] = PlanningDataService.xmlStringShort(mnByrWiseAllocData.map(function (val) {
                return {
                    MC_BYR_ACC_GRP_ID: val.MC_BYR_ACC_GRP_ID, IS_BASIC_EXISTS: val.IS_BASIC_EXISTS, BASIC_ALLOC_PCS: val.BASIC_ALLOC_PCS,
                    IS_FANCY_EXISTS: val.IS_FANCY_EXISTS, FANCY_ALLOC_PCS: val.FANCY_ALLOC_PCS, IS_POLO_EXISTS: val.IS_POLO_EXISTS, POLO_ALLOC_PCS: val.POLO_ALLOC_PCS
                };
            }));

            submitData['WK_BYR_WISE_ALLOC_XML'] = PlanningDataService.xmlStringShort(wkByrWiseAllocData.map(function (val) {
                return val;
            }));


            //submitData['TRNSFR_PROD_PCS'] = _.sumBy(submitData.items, function (o) { return (o.TRNFER_QTY_PCS || 0); });

            console.log(submitData);
            //return;

            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    //console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/ProdCapcti/ByrWiseWkCapctySave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);
                            console.log(res);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                vm.mainMnByrWiseAllocDataSource.read();
                                vm.mnCapctyFree4ByrAllocDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.cancel = function () {
            var data = angular.copy(vm.form);

            vm.form = {
                GMT_CAPACITY_MN_ID: 0, LK_CALC_MTHD_ID: ($stateParams.pLK_CALC_MTHD_ID||609), WORK_DAYS_IN_MN: 26, PCT_ABSENCE: 5, PCT_BACKLOG: 0, HR_COMPANY_ID: data.HR_COMPANY_ID,
                PROD_UNIT_ID: data.PROD_UNIT_ID, CORE_DEPT_ID: data.CORE_DEPT_ID, GMT_PRODUCT_TYP_ID: data.GMT_PRODUCT_TYP_ID,
                RF_FISCAL_YEAR_ID: data.RF_FISCAL_YEAR_ID, GMT_PROD_PLN_CLNDR_ID: data.GMT_PROD_PLN_CLNDR_ID
            };

            $state.go('ProdCapctyMN', { pGMT_CAPACITY_MN_ID: 0 }, { notify: false });
        }

        vm.backToList = function () {
            $state.go('ProdCapctyMnList', { pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pPROD_UNIT_ID: vm.form.PROD_UNIT_ID, pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID, pGMT_PRODUCT_TYP_ID: vm.form.GMT_PRODUCT_TYP_ID });
        }


    }
})();
//=============== End for ByrWiseCapctyAllocController =================