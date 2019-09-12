//=============== Start for ProdCapctyMnListController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('ProdCapctyMnListController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', ProdCapctyMnListController]);
    function ProdCapctyMnListController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modal, $timeout) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        var key = 'GMT_CAPACITY_MN_ID';
        
        vm.form = {};
        
        //$scope.form = formData[key] ? formData : {
        //    GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: ''
        //};
        
              
       
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getProdTypeList(), getClndrYearList(), getProdCapctyList()];
            return $q.all(promise).then(function () {
               
                vm.showSplash = false;                
            });
        }

                    

        //$scope.$watch('vm.form', function (newVal, oldVal) {
        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            $scope.form = vm.form;
        //        }
        //    }
        //}, true);


        //$scope.$watchGroup(['vm.form.RF_FAB_PROD_CAT_ID', 'vm.form.MC_BYR_ACC_ID', 'vm.form.MC_STYLE_H_EXT_ID'], function (newVal, oldVal) {

        //    if (!_.isEqual(newVal, oldVal)) {
        //        if (newVal != '') {
        //            vm.IS_NEXT = 'N';
        //        }
        //    }
        //}, true);
        
        vm.companyOpt = {
            optionLabel: "-- All --",
            filter: "contains",
            autoBind: true,
            dataTextField: "COMP_NAME_EN",
            dataValueField: "HR_COMPANY_ID",
            dataBound: function (e) {
                if ($stateParams.pHR_COMPANY_ID) {
                    vm.form.HR_COMPANY_ID = $stateParams.pHR_COMPANY_ID;
                }
            },
            dataSource: {
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getCompanyData').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            change: function (e) {
                var dataItem = this.dataItem(e.item);
                return vm.officeDs = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            PlanningDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getOfficeList?pHR_COMPANY_ID=' + (dataItem.HR_COMPANY_ID || '')).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }
        };

        vm.officeOpt = {
            optionLabel: "-- All --",
            filter: "contains",
            autoBind: true,
            dataTextField: "OFFICE_NAME_EN",
            dataValueField: "HR_OFFICE_ID",
            dataBound: function (e) {
                if ($stateParams.pPROD_UNIT_ID) {
                    vm.form.PROD_UNIT_ID = $stateParams.pPROD_UNIT_ID;
                }
            }
        };

        vm.officeDs = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    PlanningDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getOfficeList?pHR_COMPANY_ID').then(function (res) {
                        e.success(res);
                    });
                }
            }
        })
                
        function getClndrYearList() {
            vm.clndrYearOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FY_NAME_EN",
                dataValueField: "RF_FISCAL_YEAR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);                    
                },
                dataBound: function (e) {
                    if ($stateParams.pRF_FISCAL_YEAR_ID) {
                        vm.form.RF_FISCAL_YEAR_ID = $stateParams.pRF_FISCAL_YEAR_ID;
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
                }
            });
        };

        function getProdTypeList() {
            vm.prodTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);

                    vm.form.GMT_PRODUCT_TYP_ID = item.GMT_PRODUCT_TYP_ID;
                    $stateParams.pGMT_PRODUCT_TYP_ID = item.GMT_PRODUCT_TYP_ID;
                },
                dataBound: function (e) {
                    if ($stateParams.pGMT_PRODUCT_TYP_ID) {
                        vm.form.GMT_PRODUCT_TYP_ID = $stateParams.pGMT_PRODUCT_TYP_ID;
                    }
                }
            };

            return vm.prodTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.LookupListData(37).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };
        
        
        vm.prodCapctyMnOption = {
            height: 420,
            sortable: false,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            columns: [                
                { field: "MONTH_NAME_EN", title: "Month", width: "40%" },
                {
                    title: "Cutting",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.editRow(dataItem, 56)' ng-show='dataItem.CUTTING_ID==0' >Not Ok</button>" +
                            "<button type='button' class='btn btn-xs green' ng-click='vm.editRow(dataItem, 56)' ng-show='dataItem.CUTTING_ID>0' >Ok</button>";
                    },
                    width: "15%"
                },
                {
                    title: "Printing",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.editRow(dataItem, 59)' ng-show='dataItem.PRINTING_ID==0' >Not Ok</button>" +
                            "<button type='button' class='btn btn-xs green' ng-click='vm.editRow(dataItem, 59)' ng-show='dataItem.PRINTING_ID>0' >Ok</button>";
                    },
                    width: "15%"
                },
                {
                    title: "Embroidery",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.editRow(dataItem, 128)' ng-show='dataItem.EMBROIDERY_ID==0' >Not Ok</button>" +
                            "<button type='button' class='btn btn-xs green' ng-click='vm.editRow(dataItem, 128)' ng-show='dataItem.EMBROIDERY_ID>0' >Ok</button>";
                    },
                    width: "15%"
                },
                {
                    title: "Sewing",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.editRow(dataItem, 60)' ng-show='dataItem.SEWING_ID==0' >Not Ok</button>" +
                            "<button type='button' class='btn btn-xs green' ng-click='vm.editRow(dataItem, 60)' ng-show='dataItem.SEWING_ID>0' >Ok</button>";
                    },
                    width: "15%"
                },
                {
                    title: "GMT Finishing",
                    template: function () {
                        return "<button type='button' class='btn btn-xs red' ng-click='vm.editRow(dataItem, 61)' ng-show='dataItem.GMT_FINISHING_ID==0' >Not Ok</button>" +
                            "<button type='button' class='btn btn-xs green' ng-click='vm.editRow(dataItem, 61)' ng-show='dataItem.GMT_FINISHING_ID>0' >Ok</button>";
                    },
                    width: "15%"
                }
            ]
        };
        

        function getProdCapctyList() {
            vm.prodCapctyMnDataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        //e.success([]);
                        return PlanningDataService.getDataByFullUrl('/api/pln/ProdCapcti/GetCapctyProdMonthList?pPROD_UNIT_ID=' + (vm.form.PROD_UNIT_ID || $stateParams.pPROD_UNIT_ID || 0) + '&pGMT_PRODUCT_TYP_ID=' + (vm.form.GMT_PRODUCT_TYP_ID || $stateParams.pGMT_PRODUCT_TYP_ID) +
                            '&pRF_FISCAL_YEAR_ID=' + (vm.form.RF_FISCAL_YEAR_ID || $stateParams.pRF_FISCAL_YEAR_ID || 0)).then(function (res) {

                                if (res.length > 0) {
                                    e.success(res);
                                }
                                else {
                                    e.success([]);
                                }

                            }, function (err) {
                                console.log(err);
                            });
                    }
                }
            });
        }

        vm.editRow = function (dataItem, pCoreDeptID) {
            if (pCoreDeptID == 56) {
                $state.go('ProdCapctyMN', { pGMT_CAPACITY_MN_ID: (dataItem.CUTTING_ID || 0), pCORE_DEPT_ID: pCoreDeptID, pGMT_PRODUCT_TYP_ID: '', pHR_COMPANY_ID: $stateParams.pHR_COMPANY_ID, pPROD_UNIT_ID: $stateParams.pPROD_UNIT_ID, pRF_FISCAL_YEAR_ID: $stateParams.pRF_FISCAL_YEAR_ID,
                    pGMT_PROD_PLN_CLNDR_ID: dataItem.GMT_PROD_PLN_CLNDR_ID, pLK_CALC_MTHD_ID : 610 });
            }
            else if (pCoreDeptID == 59) {
                $state.go('ProdCapctyMN', {
                    pGMT_CAPACITY_MN_ID: (dataItem.PRINTING_ID || 0), pCORE_DEPT_ID: pCoreDeptID, pGMT_PRODUCT_TYP_ID: '', pHR_COMPANY_ID: $stateParams.pHR_COMPANY_ID, pPROD_UNIT_ID: $stateParams.pPROD_UNIT_ID, pRF_FISCAL_YEAR_ID: $stateParams.pRF_FISCAL_YEAR_ID,
                    pGMT_PROD_PLN_CLNDR_ID: dataItem.GMT_PROD_PLN_CLNDR_ID, pLK_CALC_MTHD_ID : 610
                });
            }
            else if (pCoreDeptID == 128) {
                    $state.go('ProdCapctyMN', { pGMT_CAPACITY_MN_ID: (dataItem.EMBROIDERY_ID || 0), pCORE_DEPT_ID: pCoreDeptID, pGMT_PRODUCT_TYP_ID: '', pHR_COMPANY_ID: $stateParams.pHR_COMPANY_ID, pPROD_UNIT_ID: $stateParams.pPROD_UNIT_ID, pRF_FISCAL_YEAR_ID: $stateParams.pRF_FISCAL_YEAR_ID,
                    pGMT_PROD_PLN_CLNDR_ID: dataItem.GMT_PROD_PLN_CLNDR_ID, pLK_CALC_MTHD_ID : 610 });
            }
            else if (pCoreDeptID == 60) {
          
                $state.go('ProdCapctyMN', {
                    pGMT_CAPACITY_MN_ID: (dataItem.SEWING_ID || 0), pCORE_DEPT_ID: pCoreDeptID,
                    pGMT_PRODUCT_TYP_ID: $stateParams.pGMT_PRODUCT_TYP_ID, pHR_COMPANY_ID: $stateParams.pHR_COMPANY_ID,
                    pPROD_UNIT_ID: $stateParams.pPROD_UNIT_ID, pRF_FISCAL_YEAR_ID: $stateParams.pRF_FISCAL_YEAR_ID,
                    pGMT_PROD_PLN_CLNDR_ID: dataItem.GMT_PROD_PLN_CLNDR_ID, pLK_CALC_MTHD_ID: 609
                });
            }
            else if (pCoreDeptID == 61) {
                $state.go('ProdCapctyMN', {
                    pGMT_CAPACITY_MN_ID: (dataItem.GMT_FINISHING_ID || 0), pCORE_DEPT_ID: pCoreDeptID, pGMT_PRODUCT_TYP_ID: '', pHR_COMPANY_ID: $stateParams.pHR_COMPANY_ID, pPROD_UNIT_ID: $stateParams.pPROD_UNIT_ID, pRF_FISCAL_YEAR_ID: $stateParams.pRF_FISCAL_YEAR_ID,
                    pGMT_PROD_PLN_CLNDR_ID: dataItem.GMT_PROD_PLN_CLNDR_ID, pLK_CALC_MTHD_ID: 610
                });
            }
        }

        vm.show = function () {
            vm.prodCapctyMnDataSource.read();

            $state.go('ProdCapctyMnList', {
                pHR_COMPANY_ID: (vm.form.HR_COMPANY_ID || $stateParams.pHR_COMPANY_ID), pPROD_UNIT_ID: (vm.form.PROD_UNIT_ID || $stateParams.pPROD_UNIT_ID), pRF_FISCAL_YEAR_ID: (vm.form.RF_FISCAL_YEAR_ID || $stateParams.pRF_FISCAL_YEAR_ID),
                pGMT_PRODUCT_TYP_ID: (vm.form.GMT_PRODUCT_TYP_ID || $stateParams.pGMT_PRODUCT_TYP_ID)
            }, { notify: false });
        }
       

        vm.addNew = function () {
            $state.go('ProdCapctyMN', { pGMT_CAPACITY_MN_ID: 0 });
        }

    }
})();
//=============== End for ProdCapctyMnListController =================







//=============== Start for ProdCapctyMNController =================
(function () {
    'use strict';
    angular.module('multitex.planning').controller('ProdCapctyMNController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$filter', '$modal', '$timeout', '$http', 'formData', ProdCapctyMNController]);
    function ProdCapctyMNController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $filter, $modal, $timeout, $http, formData) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.dtFormat = config.appDateFormat;
        //vm.dateTimeFormat = 'dd-MMM-yyyy hh:mm:ss a'; //config.appDateTimeFormat;
        vm.today = new Date();
        vm.isAddToGrid = 'Y';
        vm.IS_NEXT = 'N';

        var key = 'GMT_CAPACITY_MN_ID';

        vm.form = formData[key] ? formData : {
            GMT_CAPACITY_MN_ID: 0, LK_CALC_MTHD_ID: $stateParams.pLK_CALC_MTHD_ID, WORK_DAYS_IN_MN: 26, PCT_ABSENCE: 5, PCT_BACKLOG: 0, CORE_DEPT_ID: $stateParams.pCORE_DEPT_ID,
            HR_COMPANY_ID: ($stateParams.pHR_COMPANY_ID || ''),
            PROD_UNIT_ID: ($stateParams.pPROD_UNIT_ID || ''), GMT_PRODUCT_TYP_ID: ($stateParams.pGMT_PRODUCT_TYP_ID || ''),
            RF_FISCAL_YEAR_ID: ($stateParams.pRF_FISCAL_YEAR_ID || ''), GMT_PROD_PLN_CLNDR_ID: ($stateParams.pGMT_PROD_PLN_CLNDR_ID || ''),
            items : []
        };

        //$scope.form = formData[key] ? formData : {
        //    GMT_SW_MCN_SPEC_ID: 0, SW_MCN_SPEC: ''
        //};

        console.log(formData);


        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getSectionList(), getProdTypeList(), getClndrYearList(), getClndrMonthList(), getCapctiMonList4Copy(), getCalMethodList(), getCapTransferData()];
            return $q.all(promise).then(function () {

                vm.showSplash = false;
            });
        }

        function getCapTransferData() {
            if (!formData[key]) {
                PlanningDataService.getDataByUrl('/ProdCapcti/getCapTrnsferData?pGMT_CAPACITY_MN_ID=' + ($stateParams.pGMT_CAPACITY_MN_ID || -1) + '&pGMT_PRODUCT_TYP_ID=' + ($stateParams.pGMT_PRODUCT_TYP_ID))
                .then(function (res) {
                    vm.form.items = res;
                });
            }

        }

        vm.companyOpt = {
            optionLabel: "-- All --",
            filter: "contains",
            autoBind: true,
            dataTextField: "COMP_NAME_EN",
            dataValueField: "HR_COMPANY_ID",
            dataSource: {
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getCompanyData').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            change: function (e) {
                var dataItem = this.dataItem(e.item);

                vm.clndrMonth4CopyDataSource.read();
                return vm.officeDs = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            PlanningDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getOfficeList?pHR_COMPANY_ID=' + (dataItem.HR_COMPANY_ID || '')).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }
        };

        vm.officeOpt = {
            optionLabel: "-- All --",
            filter: "contains",
            autoBind: true,
            dataTextField: "OFFICE_NAME_EN",
            dataValueField: "HR_OFFICE_ID",
            change: function (e) {
                vm.clndrMonth4CopyDataSource.read();
            }
        };

        vm.officeDs = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    PlanningDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getOfficeList?pHR_COMPANY_ID').then(function (res) {
                        e.success(res);
                    });
                }
            }
        });


        //vm.onSelectOffice = function (e) {
        //    var itm = e.sender.dataItem(e.sender.item);
        //    console.log(itm);
        //}



        function getSectionList() {
            return vm.sectionOption = {
                optionLabel: "-- Select Dept --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/ParentDeptListData"  //+ "&pType=" + showType
                            }).
                            success(function (data, status, headers, config) {
                                console.log(data);

                                var list = _.filter(data, function (ob) {
                                    return (ob.HR_DEPARTMENT_ID == 56) || (ob.HR_DEPARTMENT_ID == 59) || (ob.HR_DEPARTMENT_ID == 128) || (ob.HR_DEPARTMENT_ID == 60) ||
                                        (ob.HR_DEPARTMENT_ID == 61);
                                });
                                e.success(list);
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }
                },
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                }
            };
        };

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
                    vm.clndrMonthDataSource.read();
                    vm.clndrMonth4CopyDataSource.read();
                },
                dataBound: function (e) {
                    if (formData[key]) {
                        vm.clndrMonthDataSource.read();
                        vm.clndrMonth4CopyDataSource.read();

                        vm.form.GMT_PROD_PLN_CLNDR_ID = formData['GMT_PROD_PLN_CLNDR_ID'];
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
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MONTH_NAME_EN",
                dataValueField: "GMT_PROD_PLN_CLNDR_ID",
                change: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.GMT_PROD_PLN_CLNDR_ID = item.GMT_PROD_PLN_CLNDR_ID;
                    
                    $timeout(function () {
                        vm.clndrMonth4CopyDataSource.read();
                    }, 1000);
                    
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

        function getCapctiMonList4Copy() {

            return vm.clndrMonth4CopyDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        //if (!vm.form.GMT_PRODUCT_TYP_ID) {
                        //    return;
                        //}

                        return PlanningDataService.getDataByFullUrl('/api/pln/ProdCapcti/GetCapctiMonList4Copy?pRF_FISCAL_YEAR_ID=' + (vm.form.RF_FISCAL_YEAR_ID || formData['RF_FISCAL_YEAR_ID'] || 0) + '&pGMT_PROD_PLN_CLNDR_ID=' + (vm.form.GMT_PROD_PLN_CLNDR_ID || formData['GMT_PROD_PLN_CLNDR_ID'] || 0) + '&pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID || formData['HR_COMPANY_ID'] || 0) + '&pPROD_UNIT_ID=' + (vm.form.PROD_UNIT_ID || formData['PROD_UNIT_ID'] || 0) + '&pGMT_PRODUCT_TYP_ID=' + (vm.form.GMT_PRODUCT_TYP_ID || formData['GMT_PRODUCT_TYP_ID'] || 0) + '&pCORE_DEPT_ID=' + (vm.form.CORE_DEPT_ID || formData['CORE_DEPT_ID'] || 0) + '&pLK_CALC_MTHD_ID=' + (vm.form.LK_CALC_MTHD_ID || formData['LK_CALC_MTHD_ID'] || 0)).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getProdTypeList() {

            vm.prodTypeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                change: function (e) {
                    vm.clndrMonth4CopyDataSource.read();
                    getCapTransferData();
                }
            };

            return vm.prodTypeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.LookupListData(37).then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });
        };

        function getCalMethodList() {
            return PlanningDataService.LookupListData(123).then(function (res) {
               
                vm.calMethodList = res;
            });
        };

        vm.onChangePlnProdQty = function () {
            var prodQty = 0;
            prodQty = (vm.form.NO_LINE_OR_WS || 1) * (vm.form.WORK_DAYS_IN_MN || 1) * (vm.form.PLAN_MP || 1) *
            (vm.form.AVG_WORK_HR || 1) * 60 * ((100 - (vm.form.PCT_ABSENCE || 0)) / 100) * (vm.form.FACT_EFICNCY / 100);
            vm.form.PLAN_PROD_QTY = Math.round(prodQty);
        }

        vm.onChangePlnProdPcs = function () {
            var prodPcs = 0;
            prodPcs = (vm.form.NO_LINE_OR_WS || 1) * (vm.form.WORK_DAYS_IN_MN || 1) * (vm.form.PLAN_MP || 1) *
            (vm.form.AVG_WORK_HR || 1) * 60 * ((100 - (vm.form.PCT_ABSENCE || 0)) / 100) * (vm.form.FACT_EFICNCY / 100);

            vm.form.PLAN_PROD_PCS = Math.round((prodPcs / (vm.form.AVG_SMV || 1)));
        }

        vm.onChangeAvgCM = function (data) {
            data['TTL_TRG_CM'] = parseInt((data.PLAN_PROD_PCS / 12) * data.AVG_CM);
        };

        vm.onChangeAvgFob = function (data) {
            data['TTL_TRG_FOB'] = parseInt(data.PLAN_PROD_PCS * data.AVG_FOB);
        };

        vm.onChangeCalMethod = function () {
            if (vm.form.LK_CALC_MTHD_ID == 609) {
                vm.form.WORK_DAYS_IN_MN = 26;
                vm.form.PCT_ABSENCE = 5;
            }
            else {
                vm.form.NO_LINE_OR_WS = null;
                vm.form.WORK_DAYS_IN_MN = null;
                vm.form.PLAN_MP = null;
                vm.form.PCT_ABSENCE = null;
                vm.form.FACT_EFICNCY = null;
                vm.form.AVG_WORK_HR = null;
                vm.form.AVG_SMV = null;
                vm.form.PLAN_PROD_QTY = null;
                vm.form.PLAN_PROD_PCS = null;
            }
        }


        vm.clndrMonth4CopyOption = {
            height: 400,
            sortable: false,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "row",
            columns: [
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.isSelect' ng-click='vm.selectAll(vm.isSelect,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' > <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Select?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Select?' ng-model='dataItem.IS_SELECT' ng-true-value='\"Y\"' ng-false-value='\"N\"' >";
                    },
                    width: "3%"
                },
                { field: "MONTH_NAME_EN", title: "Month", width: "25%" }
            ]
        };

        vm.selectAll = function (v, index) {
            var data = vm.clndrMonth4CopyDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            console.log(data);

            angular.forEach(data, function (val, key) {
                val['IS_SELECT'] = v;
            });
        };


        


        vm.save = function () {

            if (vm.form.LK_CALC_MTHD_ID == 610) { //By Qty
                vm.form.PLAN_PROD_QTY = angular.copy(vm.form.PLAN_PROD_PCS);
            }

            var submitData = angular.copy(vm.form);
            var plnClndrIdList = vm.clndrMonth4CopyDataSource.data();

            var list = _.map(_.filter(plnClndrIdList, function (ob) {
                return ob.IS_SELECT == 'Y';
            }), function (o) {
                return o.GMT_PROD_PLN_CLNDR_ID;
            });

            submitData['GMT_PROD_PLN_CLNDR_ID_LST'] = list.join(',');
            //submitData['GMT_PROD_PLN_CLNDR_ID_LST'] plnClndrIdList[].join(',');

            console.log(submitData);
            submitData['XML'] = config.xmlStringShort(submitData.items);


            submitData['XML_WK_CAP'] = config.xmlStringShort((submitData.wk_caps || []).map(function (val) {
                return {
                    GMT_CAPACITY_WK_ID: val.GMT_CAPACITY_WK_ID,
                    ADDL_PLAN_QTY: (val.ADDL_PLAN_QTY || 0)
                }
            }));


            submitData['TRNSFR_PROD_PCS'] = _.sumBy(submitData.items, function (o) { return (o.TRNFER_QTY_PCS || 0); });

            //return;

            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    //console.log(submitData);

                    return PlanningDataService.saveDataByFullUrl(submitData, '/api/pln/ProdCapcti/Save').then(function (res) {
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
                              //  vm.clndrMonth4CopyDataSource.read();

                               // vm.form.GMT_CAPACITY_MN_ID = res.data.PGMT_CAPACITY_MN_ID_RTN;
                                $state.go('ProdCapctyMN', {
                                    pGMT_CAPACITY_MN_ID: res.data.PGMT_CAPACITY_MN_ID_RTN,
                                    pPROD_UNIT_ID: vm.form.PROD_UNIT_ID,
                                    pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID,
                                    pGMT_PRODUCT_TYP_ID: vm.form.GMT_PRODUCT_TYP_ID,
                                    pGMT_PROD_PLN_CLNDR_ID: vm.form.GMT_PROD_PLN_CLNDR_ID
                                }, {reload: true});
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
//=============== End for ProdCapctyMNController =================