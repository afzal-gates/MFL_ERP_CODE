//============== Start for HrIncrMemoController
(function () {
    'use strict';
    angular.module('multitex.hr').controller('HrIncrMemoController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', '$filter', HrIncrMemoController]);

    function HrIncrMemoController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams, $filter) {
                        
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

                
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.today = new Date();
        vm.periodType = 3;
        vm.form = { HR_INCR_MEMO_ID: 0, IS_BY_ADMIN: 'Y', IS_FINALIZED: 'N', EFFECTIVE_DT: vm.today, MEMO_DT: vm.today };

        
        
        activate();

        function activate() {
            var promise = [getCompanyList(), getIncrimentType()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        vm.memoDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.memoDateOpened = true;
        }
        
        vm.effectiveDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.effectiveDateOpened = true;
        }

        function getCompanyList() {
           
            vm.compOptions = {
                optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",                
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    return vm.compPayPeriodDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                return HrService.getDataByFullUrl("/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + dataItem.HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodType + "&pIS_CLOSED=N").then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    });
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.GetAllOthers('/api/common/CompanyList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                }
            }                                  
        };

                
        function getIncrimentType() {            
            HrService.GetDataByUrl('/api/common/GetIncrimentType').then(function (res) {
                vm.RF_INCR_TYPE_LIST = res;
                //vm.form.RF_INCR_TYPE_ID = 1;
            });
        };

        vm.incrTypeOnChange = function(e) {
            var item = e.sender.dataItem(e.sender.select());
            vm.form.HR_MANAGEMENT_TYPE_ID = item.HR_MANAGEMENT_TYPE_ID;
            //console.log(item);
        };

        //function getMemoList() {
        //    vm.incrMemoGridDataSource.read();
        //}

        vm.incrMemoGridOption = {
            //height: 170,
            sortable: true,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
            pageable: false,
            editable: false,
            selectable: "cell",
            navigatable: true,
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
                { field: "AUTH_NO", title: "Authorization#", type: "string", width: "90px" },
                { field: "MONTH_YEAR_NAME", title: "Incr.Period", type: "string", width: "80px" },
                { field: "INCR_TYPE_NAME_EN", title: "Incriment Type", type: "string", width: "150px" },
                { field: "COMP_NAME_EN", title: "Company", type: "string", width: "150px" },
                { field: "MEMO_NO", title: "Memo#", type: "string", width: "100px" },
                { field: "MEMO_DT", title: "Memo Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "70px", filterable: false, hidden: true },
                { field: "IS_FINALIZED", title: "Finalize?", type: "string", width: "50px", filterable: false },
                { field: "REMARKS", title: "Remarks", type: "string", width: "150px", filterable: false },
                {
                    title: "Action",
                    template: function () {                        
                        return "<button class='btn btn-xs blue' ng-click='vm.editData(dataItem)' ><i class='fa fa-edit'> Edit</i></button>";
                    },
                    width: "40px"
                }
            ]
        };

        vm.incrMemoGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 5,            
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/hr/HrIncriment/GetIncrMemoList';
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += HrService.kFilterStr2QueryParam(params.filter);

                    return HrService.GetDataByUrl(url).then(function (res) {
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });                    
                }
            },
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "HR_INCR_MEMO_ID",
                    fields: {
                        MEMO_NO: { type: "string", editable: false },
                        MEMO_DT: { type: "date", editable: false }                        
                    }
                }
            }
        });

               

        vm.reset = function () {
            //vm.form = { HR_INCR_MEMO_ID: 0, IS_BY_ADMIN: 'Y', IS_FINALIZED: 'N', EFFECTIVE_DT: vm.today, MEMO_DT: vm.today };
            vm.form.HR_INCR_MEMO_ID = 0;
            vm.form.AUTH_NO = null;
            vm.form.MEMO_NO = null;
            vm.form.MEMO_DT = null;
            vm.form.REMARKS = null;
        }

        vm.editData = function (data) {
            return vm.compPayPeriodDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        return HrService.getDataByFullUrl("/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + data.HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodType + "&pIS_SHOW4_RPT=Y").then(function (res) {
                            e.success(res);
                            vm.form = angular.copy(data);
                        });
                    }
                }
            });            
        }

        vm.submit = function () {
            var submitData = angular.copy(vm.form);
            submitData.MEMO_DT = $filter('date')(submitData.MEMO_DT, vm.dtFormat);
            submitData.EFFECTIVE_DT = $filter('date')(submitData.EFFECTIVE_DT, vm.dtFormat);

            return HrService.saveDataByUrl(submitData, '/api/Hr/HrIncriment/Save').then(function (res) {
                if (res.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    console.log(res);
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        //vm.reset();
                        vm.incrMemoGridDataSource.read();

                        vm.form.AUTH_NO = res.data.PAUTH_NO_RTN;
                        vm.form.HR_INCR_MEMO_ID = res.data.PHR_INCR_MEMO_ID_RTN;

                        vm.form.HR_COMPANY_ID = submitData.HR_COMPANY_ID;
                        vm.form.RF_INCR_TYPE_ID = submitData.RF_INCR_TYPE_ID;
                    };
                    
                    config.appToastMsg(res.data.PMSG);
                    
                }
            });                     
        };


        vm.memoFinalize = function () {
            return HrService.saveDataByUrl(vm.form, '/api/Hr/HrIncriment/MemoFinalize').then(function (res) {
                if (res.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    console.log(res);
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        //vm.reset();
                        vm.incrMemoGridDataSource.read();                        
                    };

                    config.appToastMsg(res.data.PMSG);

                }
            });
        }
            
        
    }
    
})();
//============== End for HrIncrMemoController














//============== Start for HrIncrProposalHController
(function () {
    'use strict';
    angular.module('multitex.hr').controller('HrIncrProposalHController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', 'incrHdrData', '$filter', HrIncrProposalHController]);

    function HrIncrProposalHController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams, incrHdrData, $filter) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };
        var key = 'HR_YR_INCR_H_ID';
        console.log(incrHdrData);

        //vm.IS_NEXT = 'Y';// $stateParams.pIS_FROM_BATCH_LIST == 'Y'?'Y':'N';
        vm.form = incrHdrData[key] ? incrHdrData : { IS_NEXT: $stateParams.pIS_FROM_BATCH_LIST == 'Y' ? 'Y' : 'N', IS_P_F: 'N', BATCH_APVRL_STATUS: '', USER_LEVEL: '', total: 0, HR_INCR_MEMO_ID: null, LK_FLOOR_ID: null, PROPOSE_BY: null, HR_DEPARTMENT_ID: null, CORE_DEPT_ID: null };
        $scope.form = incrHdrData[key] ? incrHdrData : { IS_NEXT: $stateParams.pIS_FROM_BATCH_LIST == 'Y' ? 'Y' : 'N', IS_P_F: 'N', BATCH_APVRL_STATUS: '', USER_LEVEL: '', total: 0, HR_INCR_MEMO_ID: null, LK_FLOOR_ID: null, PROPOSE_BY: null, HR_DEPARTMENT_ID: null, CORE_DEPT_ID: null };

        
        activate();

        function activate() {
            var promise = [getSubdepartmentListData(), floorList(), autoEmpLoad()]; //, autoEmpLoad()
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        $scope.$watch('vm.form', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;                   
                }
            }
        }, true);


        $scope.$watchGroup(['vm.form.HR_INCR_MEMO_ID', 'vm.form.CORE_DEPT_ID', 'vm.form.HR_DEPARTMENT_ID', 'vm.form.LK_FLOOR_ID'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    //vm.form.IS_NEXT = 'N';
                    //$state.go('IncrimentProposalH', {
                    //    pHR_YR_INCR_H_ID: 0, pHR_INCR_MEMO_ID: vm.form.HR_INCR_MEMO_ID, pEMPLOYEE_TYPE_ID: vm.form.EMPLOYEE_TYPE_ID,
                    //    pHR_DEPARTMENT_ID: vm.form.HR_DEPARTMENT_ID, pLK_FLOOR_ID: vm.form.LK_FLOOR_ID
                    //});
                }
            }
        }, true);

        vm.incrMemoGridOption = {
            height: 100,
            sortable: true,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
            pageable: false,
            editable: false,
            selectable: "row",
            navigatable: true,
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
                { field: "HR_INCR_MEMO_ID", title: "ID", type: "string", hidden: true },
                { field: "AUTH_NO", title: "Authorization#", type: "string", width: "12%" },
                { field: "MONTH_YEAR_NAME", title: "Incr.Period", type: "string", width: "10%" },
                { field: "COMP_NAME_EN", title: "Incr.Company", type: "string", width: "15%" },                
                { field: "INCR_TYPE_NAME_EN", title: "Increment Type", type: "string", width: "20%" },
                { field: "MEMO_NO", title: "Memo#", type: "string", width: "15%" },
                { field: "MEMO_DT", title: "Memo Date", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true }
            ],
            change: function (e) {
                var grid = $("#incrMemoGrid").data("kendoGrid");
                //grid.select("tr:eq(1)");
                var row = grid.select();
                var item = grid.dataItem(row);

                vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
                vm.form.HR_INCR_MEMO_ID = item.HR_INCR_MEMO_ID;
                vm.form.MEMO_DT = item.MEMO_DT;
                vm.form.MEMO_NO = item.MEMO_NO;
                vm.form.EMPLOYEE_TYPE_ID = item.HR_MANAGEMENT_TYPE_ID;
                vm.form.HR_MANAGEMENT_TYPE_ID = item.HR_MANAGEMENT_TYPE_ID;
                vm.form.FROM_DATE = item.START_DATE;
                vm.form.IS_FINALIZED = item.IS_FINALIZED;
                vm.form.IS_NEXT = 'N';

                $stateParams.pHR_INCR_MEMO_ID = item.HR_INCR_MEMO_ID;
                $stateParams.pEMPLOYEE_TYPE_ID = item.HR_MANAGEMENT_TYPE_ID;
                $stateParams.pIS_FROM_BATCH_LIST = 'N';

                $scope.$apply();
                console.log(vm.form);
            },
            dataBound: function () {
                var grid = this;
                $.each(grid.tbody.find('tr'), function (idx) {

                    var model = grid.dataItem(this);
                    if (idx == 0 && $stateParams.pHR_INCR_MEMO_ID == 0) {
                        vm.form.IS_NEXT = 'N';
                        vm.form.HR_COMPANY_ID = model.HR_COMPANY_ID;
                        vm.form.HR_INCR_MEMO_ID = model.HR_INCR_MEMO_ID;
                        vm.form.MEMO_DT = model.MEMO_DT;
                        vm.form.MEMO_NO = model.MEMO_NO;
                        vm.form.EMPLOYEE_TYPE_ID = model.HR_MANAGEMENT_TYPE_ID;
                        vm.form.HR_MANAGEMENT_TYPE_ID = model.HR_MANAGEMENT_TYPE_ID;
                        vm.form.FROM_DATE = model.START_DATE;
                        vm.form.IS_FINALIZED = model.IS_FINALIZED;

                        $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                    }
                    else if (model.HR_INCR_MEMO_ID == $stateParams.pHR_INCR_MEMO_ID && $stateParams.pHR_INCR_MEMO_ID > 0) {
                        vm.form.IS_NEXT = 'Y';
                        vm.form.HR_INCR_MEMO_ID = model.HR_INCR_MEMO_ID;
                        vm.form.MEMO_DT = model.MEMO_DT;
                        vm.form.MEMO_NO = model.MEMO_NO;
                        vm.form.EMPLOYEE_TYPE_ID = model.HR_MANAGEMENT_TYPE_ID;
                        vm.form.HR_MANAGEMENT_TYPE_ID = model.HR_MANAGEMENT_TYPE_ID;
                        vm.form.FROM_DATE = model.START_DATE;
                        vm.form.IS_FINALIZED = model.IS_FINALIZED;

                        $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                    }

                    //if ($stateParams.MC_ORDER_H_ID == 0) return;
                    //var model = grid.dataItem(this);
                    //if (model.MC_ORDER_H_ID == $stateParams.MC_ORDER_H_ID) {
                    //    $scope.Order = model;
                    //    $state.go('TnAFollowup.TnaTask', { MC_ORDER_H_ID: model.MC_ORDER_H_ID }, { reload: 'TnAFollowup.TnaTask' });
                    //    $('[data-uid=' + model.uid + ']').addClass('k-state-selected');
                    //}
                });
            }
        };

        vm.incrMemoGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 100,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "HR_INCR_MEMO_ID",
                    fields: {
                        MEMO_DT: { type: "date", editable: true }
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/hr/HrIncriment/GetIncrMemoList?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pRF_INCR_TYPE_ID_LST=3,4&pIS_FINALIZED=N';
                    url += HrService.kFilterStr2QueryParam(params.filter);

                    return HrService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });
                }                
            }
        });

        /*
        function GetMemoList() {           
            vm.memoListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "AUTH_NO",
                dataValueField: "HR_INCR_MEMO_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
                    vm.form.INCR_TYPE_NAME_EN = item.INCR_TYPE_NAME_EN;
                    vm.form.MEMO_NO = item.MEMO_NO;
                    vm.form.EMPLOYEE_TYPE_ID = item.HR_MANAGEMENT_TYPE_ID;
                }
            };

            return vm.memoListDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/hr/HrIncriment/GetIncrMemoList?pageNumber=1&pageSize=10';                                              
                        url += HrService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return HrService.getDataByFullUrl(url).then(function (res) {
                            console.log(res);
                            e.success(res.data);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };
        */

        function parentDepartmentList() {
            vm.parentDepartmentListData = {
                optionLabel: "-- Select Department --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/ParentDeptListData"  //+ "&pType=" + showType
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }//,
                    //group: { field: "PARENT_NAME" }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.HR_DEPARTMENT_ID; //this.value();                    

                    return vm.subdepartmentDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {

                                return HrService.getSubDepartmentData(dataItem.HR_DEPARTMENT_ID).then(function (res) {
                                    console.log(res);
                                    e.success(res);
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                        }
                    });                    
                }
            };
        };

        vm.onSectionDataBound = function (e) {           
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].HR_DEPARTMENT_ID);
                vm.form.HR_DEPARTMENT_ID = ds[0].HR_DEPARTMENT_ID;
                vm.form.CORE_DEPT_ID = ds[0].CORE_DEPT_ID;
            }
            else if (ds.length > 0 && $stateParams.pHR_DEPARTMENT_ID > 0) {
                
                e.sender.value($stateParams.pHR_DEPARTMENT_ID);
                vm.form.HR_DEPARTMENT_ID = $stateParams.pHR_DEPARTMENT_ID;
                var selectedDept = _.filter(ds, function (ob) {
                    if ($stateParams.pHR_DEPARTMENT_ID == ob.HR_DEPARTMENT_ID) {
                        //alert('Ok');
                        vm.form.CORE_DEPT_ID = ob.CORE_DEPT_ID;
                        return ob;
                    }
                });
            }
        };

        function getSubdepartmentListData() {
            vm.subdepartmentOption = {
                optionLabel: "-- Select Section --",
                filter: "contains",                
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    vm.form.CORE_DEPT_ID = dataItem.CORE_DEPT_ID;
                    //$scope.form.CORE_DEPT_ID = dataItem.CORE_DEPT_ID;
                    $stateParams.pHR_DEPARTMENT_ID = dataItem.CORE_DEPT_ID;
                    $stateParams.pIS_FROM_BATCH_LIST = 'N';

                    vm.form.IS_NEXT = 'N';
                }
            };

            return vm.subdepartmentDataSource = new kendo.data.DataSource({                
                transport: {
                    read: function (e) {
                        return HrService.getDataByFullUrl('/api/hr/HrIncriment/GetSection4IncrProp').then(function (res) {                            
                            e.success(res);
                        });

                    }
                }                
            });
        };

        vm.onFloorDataBound = function (e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1 && vm.form.HR_YR_INCR_H_ID==0) {
                e.sender.value(ds[0].LK_FLOOR_ID);
                vm.form.LK_FLOOR_ID = ds[0].LK_FLOOR_ID;
            }
            else if (ds.length > 0 && $stateParams.pLK_FLOOR_ID > 0) {
                e.sender.value($stateParams.pLK_FLOOR_ID);
                vm.form.LK_FLOOR_ID = $stateParams.pLK_FLOOR_ID;
            }
        };

        function floorList() {
            vm.prodFloorOption = {
                optionLabel: "-- Select Floor --",
                autoBind: true,                
                dataTextField: "FLOOR_CODE",
                dataValueField: "LK_FLOOR_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    
                    $stateParams.pLK_FLOOR_ID = dataItem.LK_FLOOR_ID;
                    $stateParams.pIS_FROM_BATCH_LIST = 'N';

                    vm.form.IS_NEXT = 'N';
                }
            };

            return vm.prodFloorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        return HrService.getDataByFullUrl('/api/hr/HrIncriment/GetFloor4IncrProp').then(function (res) {
                            e.success(res);
                        });

                    }
                }
            });


            //return vm.floorListData = {
            //    optionLabel: "-- Select Floor --",                
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                $http({
            //                    method: 'post',
            //                    url: "/Hr/HrEmployee/LookupListData",  //+ "&pType=" + showType
            //                    params: {
            //                        pLookupTableId: 18
            //                    }
            //                }).
            //                success(function (data, status, headers, config) {
            //                    e.success(data)
            //                }).
            //                error(function (data, status, headers, config) {
            //                    alert('something went wrong')
            //                    console.log(status);
            //                });
            //            }
            //        }
            //    },
            //    select: function (e) {
            //        var dataItem = this.dataItem(e.item);                    
            //        vm.form.FLOOR_NAME = dataItem.LK_DATA_NAME_EN;
            //    },
            //    dataTextField: "LK_DATA_NAME_EN",
            //    dataValueField: "LOOKUP_DATA_ID"
            //};
        };

        //function getDesigList() {
        //    return HrService.getDesigData(vm.form.CORE_DEPT_ID).then(function (res) {
        //        $scope.desigData = res;
        //    });
        //}

        function autoEmpLoad() {
            
            if ($stateParams.pIS_FROM_BATCH_LIST == 'Y') {
                vm.form.IS_NEXT = 'Y';
                if (vm.form.IS_P_F != undefined) {
                    //alert('Ok');
                    $state.go('IncrimentProposalH.Dtl', {
                        pHR_YR_INCR_H_ID: $stateParams.pHR_YR_INCR_H_ID, pHR_INCR_MEMO_ID: $stateParams.pHR_INCR_MEMO_ID, pEMPLOYEE_TYPE_ID: $stateParams.pEMPLOYEE_TYPE_ID,
                        pHR_DEPARTMENT_ID: $stateParams.pHR_DEPARTMENT_ID, pLK_FLOOR_ID: $stateParams.pLK_FLOOR_ID, pPROPOSE_BY: incrHdrData.PROPOSE_BY||0,
                        pIS_FROM_BATCH_LIST: $stateParams.pIS_FROM_BATCH_LIST
                    });

                    //return;
                }
            }

                //return HrService.getDataByFullUrl('/api/hr/HrIncriment/GetIncrHdr?pHR_YR_INCR_H_ID=' + $stateParams.pHR_YR_INCR_H_ID + '&pHR_INCR_MEMO_ID=' + $stateParams.pHR_INCR_MEMO_ID
                //                            + '&pHR_DEPARTMENT_ID=' + $stateParams.pHR_DEPARTMENT_ID + '&pLK_FLOOR_ID=' + $stateParams.pLK_FLOOR_ID + '&pPROPOSE_BY=' + $stateParams.pPROPOSE_BY + '&pIS_FROM_BATCH_LIST=' + $stateParams.pIS_FROM_BATCH_LIST).then(function (res) {

                //    console.log(res);
                    
                //    vm.form.HR_YR_INCR_H_ID = res.HR_YR_INCR_H_ID;
                //    vm.form.CORE_DEPT_ID = res.CORE_DEPT_ID;
                //    vm.form.IS_P_F = (res.IS_P_F == null ? 'N' : res.IS_P_F);
                //    vm.form.LK_INCR_STATUS_ID = res.LK_INCR_STATUS_ID;
                //    vm.form.BATCH_APVRL_STATUS = res.BATCH_APVRL_STATUS;
                //    vm.form.USER_LEVEL = res.USER_LEVEL;
                //    //$scope.form.USER_LEVEL = res.USER_LEVEL;
                                       
                //    vm.form.PROPOSE_BY = res.PROPOSE_BY;
                //    vm.form.USER_APROVER_LVL_NO = res.USER_APROVER_LVL_NO;
                //    vm.form.APROVER_LVL_NO = res.APROVER_LVL_NO;
                //    //vm.form.IS_PROPOSER = (res.IS_PROPOSER == null ? 'N' : res.IS_PROPOSER);
                //    //vm.form.IS_APPROVER = (res.IS_APPROVER == null ? 'N' : res.IS_APPROVER);
                //    vm.form.total = 0;

                //    //$scope.form.HR_YR_INCR_H_ID = res.HR_YR_INCR_H_ID;
                //    //$scope.form.IS_P_F = (res.IS_P_F == null ? 'N' : res.IS_P_F);
                //    //$scope.form.IS_PROPOSER = (res.IS_PROPOSER == null ? 'N' : res.IS_PROPOSER);
                //    //$scope.form.IS_APPROVER = (res.IS_APPROVER == null ? 'N' : res.IS_APPROVER);
                //    //$scope.form.total = 0;

                    
                //    //getDesigList();

                //}, function (err) {
                //    console.log(err);
                //});
            
        }

        vm.empLoad = function () {
            vm.form.IS_NEXT = 'Y';

            console.log($stateParams);

            return HrService.getDataByFullUrl('/api/hr/HrIncriment/GetIncrHdr?pHR_YR_INCR_H_ID=' + vm.form.HR_YR_INCR_H_ID + '&pHR_INCR_MEMO_ID=' + vm.form.HR_INCR_MEMO_ID
                                        + '&pHR_DEPARTMENT_ID=' + vm.form.HR_DEPARTMENT_ID + '&pLK_FLOOR_ID=' + vm.form.LK_FLOOR_ID + '&pPROPOSE_BY=' + $stateParams.pPROPOSE_BY).then(function (res) {
                console.log(res);
                vm.form.HR_YR_INCR_H_ID = res.HR_YR_INCR_H_ID;
                vm.form.IS_P_F = (res.IS_P_F == null ? 'N' : res.IS_P_F);
                vm.form.LK_INCR_STATUS_ID = res.LK_INCR_STATUS_ID;
                vm.form.BATCH_APVRL_STATUS = res.BATCH_APVRL_STATUS;
                vm.form.USER_LEVEL = res.USER_LEVEL;
                //$scope.form.USER_LEVEL = res.USER_LEVEL;
               
                vm.form.PROPOSE_BY = res.PROPOSE_BY;
                vm.form.USER_APROVER_LVL_NO = res.USER_APROVER_LVL_NO;
                vm.form.APROVER_LVL_NO = res.APROVER_LVL_NO;
                
                //vm.form.IS_PROPOSER = (res.IS_PROPOSER == null ? 'N' : res.IS_PROPOSER);
                //vm.form.IS_APPROVER = (res.IS_APPROVER == null ? 'N' : res.IS_APPROVER);
                vm.form.total = 0;
                
                if (vm.form.IS_P_F != undefined) {
                    $state.go('IncrimentProposalH.Dtl', {
                        pHR_YR_INCR_H_ID: vm.form.HR_YR_INCR_H_ID, pHR_INCR_MEMO_ID: vm.form.HR_INCR_MEMO_ID, pEMPLOYEE_TYPE_ID: vm.form.EMPLOYEE_TYPE_ID,
                        pHR_DEPARTMENT_ID: vm.form.HR_DEPARTMENT_ID, pLK_FLOOR_ID: vm.form.LK_FLOOR_ID, pPROPOSE_BY: res.PROPOSE_BY,
                        pIS_FROM_BATCH_LIST: $stateParams.pIS_FROM_BATCH_LIST,
                    }, { reload: 'IncrimentProposalH.Dtl' });

                    return;
                }
                //getDesigList();

            }, function (err) {
                console.log(err);
            });
            
        }
        
        //alert($stateParams.pIS_FROM_BATCH_LIST);

    }

})();
//============== End for HrIncrProposalHController
















//============== Start for HrIncrProposalDController
(function () {
    'use strict';
    angular.module('multitex.hr').controller('HrIncrProposalDController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', 'Dialog', '$state', '$stateParams', '$modal', '$filter', HrIncrProposalDController]);

    function HrIncrProposalDController(logger, config, HrService, $q, $scope, $http, $location, Dialog, $state, $stateParams, $modal, $filter) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.errstyle = { 'border': '1px solid #f13d3d' };
        //$scope.$parent.form = { total: 0 };
        var desigData = [];
        var gradeData = [];

        vm.printButtonList = [{ BUTTON_ID: 1, BUTTON_CAPTION: "Proposal List" }, { BUTTON_ID: 2, BUTTON_CAPTION: "Incr/Promotion Letter" }];


       
        activate();

        function activate() {
            var promise = [getIncrGradeList(), getDesigList()];
            return $q.all(promise).then(function () {
                //var grid = $("#incrEmpListGrid").data("kendoGrid");
                //grid.refresh();
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        vm.effectiveDateOpened = [];
        vm.effectiveDateOpen = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.effectiveDateOpened[index] = true;
        };

        function getDesigList() {            
            return HrService.getDesigData($scope.$parent.form.CORE_DEPT_ID).then(function (res) {
                desigData = res;
            });
        }
              
        function getIncrGradeList() {
            return HrService.GetDataByUrl('/api/hr/HrIncriment/GetIncrGradeList?pHR_EMPLOYEE_TYPE_ID=' + $stateParams.pEMPLOYEE_TYPE_ID).then(function (res) {
                gradeData = res;
            });
        }
        
        vm.approveAll = function (v, index) {
            var data = vm.incrEmpListGridDataSource.data(); //angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            console.log(data);

            angular.forEach(data, function (val, key) {
                if (val['APPROVE_DISABLED'] == false) {
                    val['IS_APPROVED'] = v;
                }
            });
        };

        function DesigDropDownEditor(container, options) {
            if (options.model.PROMOTED_DISABLED) {
                $("<span>" + options.model.get(options.field).DESIGNATION_NAME_EN + "</span>").appendTo(container);
                return;
            };

            $('<input data-text-field="DESIGNATION_NAME_EN" data-value-field="HR_DESIGNATION_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    filter: "contains",
                    optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {                                
                                var desigFilterList = _.filter(desigData, function (ob) {
                                    return (ob.HR_DEPARTMENT_ID == options.model.CORE_DEPT_ID && ob.DESIG_ORDER <= options.model.DESIG_ORDER);
                                });
                                //console.log(options.model.CORE_DEPT_ID);
                                //console.log(desigData);
                                e.success(desigFilterList);
                            }
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        console.log(item);
                        options.model.HR_DESIGNATION_ID = item.HR_DESIGNATION_ID;
                        //vm.incrEmpListGridDataSource.sync();
                    }
                });
        }

        function GradeDropDownEditor(container, options) {
            console.log(options.model);

            if (options.model.GRADE_DISABLED) {
                $("<span>" + options.model.get(options.field).INCR_GRADE_CODE + "</span>").appendTo(container);
                return;
            };

            $('<input data-text-field="INCR_GRADE_CODE" data-value-field="HR_INCR_GRADE_ID" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({                    
                    //optionLabel: "--N/A--",
                    autoBind: false,
                    dataSource: {
                        transport: {
                            read: function (e) {

                                var dataList = _.filter(gradeData, function (o) {
                                    
                                    if ($stateParams.pEMPLOYEE_TYPE_ID == 2) {
                                        if (options.model.DSG_GRP_ORDER < 9) {
                                            return o.DESIG_ORDER_LOW >= 1 && o.DESIG_ORDER_HIGH < 9;
                                        }
                                        else if (options.model.DSG_GRP_ORDER >= 9) {
                                            return o.DESIG_ORDER_LOW >= 9;
                                        }
                                    }
                                    else {
                                        return o;
                                    }
                                });

                                console.log(dataList);

                                e.success(dataList);
                            }                            
                        }
                    },
                    change: function (e) {
                        var item = this.dataItem(e.item);
                        //console.log(item.GRADE_AMT);

                        options.model.HR_INCR_GRADE_ID = item.HR_INCR_GRADE_ID;
                        options.model.GRADE_PCT = item.GRADE_AMT == undefined ? 0 : item.GRADE_AMT;
                        options.model.GRADE_HR_PCT = item.HR_PCT == undefined ? 0 : item.HR_PCT;
                        options.model.GRADE_ADDL_AMT = item.ADDL_AMT == undefined ? 0 : item.ADDL_AMT;
                        options.model.IS_B_G = item.IS_B_G == undefined ? 'B' : item.IS_B_G;
                        options.model.IS_P_F = item.IS_P_F == undefined ? 'P' : item.IS_P_F;

                        console.log(options.model);

                        if (options.model.IS_B_G == 'B') {
                            //options.model.ADDL_AMT = item.ADDL_AMT;
                            if (options.model.IS_P_F == 'P') {
                                var incrBasic = ((options.model.PRE_BASIC_SALARY * options.model.GRADE_PCT) + options.model.GRADE_ADDL_AMT);
                                var incrHR = incrBasic * options.model.GRADE_HR_PCT;
                                var incrAmt = incrBasic + incrHR + options.model.ADDL_AMT;
                                console.log(incrBasic);
                                console.log(incrHR);
                                console.log(incrAmt);

                                options.model.INCR_AMT = Math.round(incrAmt);
                                var incrPct = (options.model.INCR_AMT * 100) / options.model.PRE_BASIC_SALARY;
                                options.model.INCR_PCT = parseFloat(incrPct.toFixed(2));
                                options.model.NEW_GROSS = options.model.PRE_GROSS_SALARY + options.model.INCR_AMT;
                            }
                            else {
                                options.model.INCR_AMT = options.model.ADDL_AMT;
                                var incrPct = (options.model.INCR_AMT * 100) / options.model.PRE_BASIC_SALARY;
                                options.model.INCR_PCT = parseFloat(incrPct.toFixed(2));
                                options.model.NEW_GROSS = options.model.PRE_GROSS_SALARY + options.model.INCR_AMT;
                            }
                        }
                        else if (options.model.IS_B_G == 'G') {
                            //options.model.ADDL_AMT = item.ADDL_AMT;
                            if (options.model.IS_P_F == 'P') {
                                var incrGross = ((options.model.PRE_GROSS_SALARY * options.model.GRADE_PCT) + options.model.GRADE_ADDL_AMT);
                                var incrHR = incrGross * options.model.GRADE_HR_PCT;
                                var incrAmt = incrGross + incrHR + options.model.ADDL_AMT;
                                console.log(incrGross);
                                console.log(incrHR);
                                console.log(incrAmt);

                                options.model.INCR_AMT = Math.round(incrAmt);
                                var incrPct = (options.model.INCR_AMT * 100) / options.model.PRE_GROSS_SALARY;
                                options.model.INCR_PCT = parseFloat(incrPct.toFixed(2));
                                options.model.NEW_GROSS = options.model.PRE_GROSS_SALARY + options.model.INCR_AMT;
                            }
                            else {
                                options.model.INCR_AMT = options.model.ADDL_AMT;
                                var incrPct = (options.model.INCR_AMT * 100) / options.model.PRE_GROSS_SALARY;
                                options.model.INCR_PCT = parseFloat(incrPct.toFixed(2));
                                options.model.NEW_GROSS = options.model.PRE_GROSS_SALARY + options.model.INCR_AMT;
                            }
                        }
                        //vm.incrEmpListGridDataSource.sync();
                    }
                });
        }

        vm.onChangePromoted = function (dataItem) {
            var grid = $("#incrEmpListGrid").data("kendoGrid");
            grid.refresh();

            console.log(grid);
            console.log(dataItem);

            if (dataItem.IS_PROMOTED == 'Y') {
                dataItem.PROMOTED_DISABLED = false;

                if ($stateParams.pEMPLOYEE_TYPE_ID == 2 || $stateParams.pEMPLOYEE_TYPE_ID == 8) { // Worker
                    dataItem.GRADE_DISABLED = true;
                    dataItem.HR_INCR_GRADE_ID = null;
                    
                    dataItem.GRADE_PCT = 0; 
                    dataItem.GRADE_AMT = 0;
                    dataItem.GRADE_HR_PCT = 0;
                    dataItem.HR_PCT = 0;
                    dataItem.GRADE_ADDL_AMT = 0;
                    
                    //options.model.GRADE_ADDL_AMT = item.ADDL_AMT == undefined ? 0 : item.ADDL_AMT;
                    //options.model.IS_B_G = item.IS_B_G == undefined ? 'B' : item.IS_B_G;
                    //options.model.IS_P_F = item.IS_P_F == undefined ? 'P' : item.IS_P_F;

                    dataItem.set('HR_GRADE', { HR_INCR_GRADE_ID: "", INCR_GRADE_CODE: "--N/A--" });
                    
                    dataItem.INCR_AMT = dataItem.ADDL_AMT;
                    var incrPct = (dataItem.INCR_AMT * 100) / dataItem.PRE_BASIC_SALARY;
                    dataItem.INCR_PCT = parseFloat(incrPct.toFixed(2));
                    dataItem.NEW_GROSS = dataItem.PRE_GROSS_SALARY + dataItem.INCR_AMT;
                }
            }
            else {
                dataItem.PROMOTED_DISABLED = true;
                dataItem.HR_DESIGNATION_ID = null;
                dataItem.DESIGNATION_NAME_EN = dataItem.PRE_DESIGNATION_NAME_EN;
                dataItem.set('HR_DESIGNATION', { HR_DESIGNATION_ID: dataItem.PRE_HR_DESIGNATION_ID, DESIGNATION_NAME_EN: dataItem.PRE_DESIGNATION_NAME_EN });

                if ($stateParams.pEMPLOYEE_TYPE_ID == 8) { // Worker
                    dataItem.GRADE_DISABLED = false;
                }                
            }
        }

        function EffectiveDateEditor(container, options) {
            console.log(options.model);

            if (!options.model.GRADE_DISABLED || (($scope.$parent.form.IS_PROPOSER == 'Y' && $scope.$parent.form.IS_APPROVER == 'N' && $scope.$parent.form.IS_P_F == 'P')
                 || ($scope.$parent.form.IS_PROPOSER == 'N' && $scope.$parent.form.IS_APPROVER == 'Y' && $scope.$parent.form.IS_P_F != 'P') || $scope.$parent.form.IS_P_F == 'F')) {
                $("<span>" + kendo.toString(options.model.EFFECTIVE_DT, 'dd-MMM-yyyy') + "</span>").appendTo(container);
                return;
            };

            $('<input type="text" data-bind="value:' + options.field + '"/>')
            .appendTo(container)
            .kendoDatePicker({
                format: "dd-MMM-yyyy",
                value: options.model.EFFECTIVE_DT //kendo.toString(new Date(options.model.EFFECTIVE_DT), 'dd/MMM/yyyy')
            });
            
        }

        vm.onBlurSpecAmt = function (dataItem) {
            console.log(dataItem);
            if (dataItem.IS_B_G == 'B') {
                var incrBasic = ((dataItem.PRE_BASIC_SALARY * dataItem.GRADE_PCT) + dataItem.GRADE_ADDL_AMT);
                var incrHR = incrBasic * dataItem.GRADE_HR_PCT;
                var incrAmt = incrBasic + incrHR + dataItem.ADDL_AMT;
                console.log(incrBasic);
                console.log(incrHR);
                console.log(incrAmt);

                dataItem.INCR_AMT = Math.round(incrAmt);
                var incrPct = (dataItem.INCR_AMT * 100) / dataItem.PRE_BASIC_SALARY;
                dataItem.INCR_PCT = parseFloat(incrPct.toFixed(2));
                dataItem.NEW_GROSS = dataItem.PRE_GROSS_SALARY + dataItem.INCR_AMT;
            }
            else if (dataItem.IS_B_G == 'G') {
                var incrGross = ((dataItem.PRE_GROSS_SALARY * dataItem.GRADE_PCT) + dataItem.GRADE_ADDL_AMT);
                var incrHR = incrGross * dataItem.GRADE_HR_PCT;
                var incrAmt = incrGross + incrHR + dataItem.ADDL_AMT;
                console.log(incrGross);
                console.log(incrHR);
                console.log(incrAmt);

                dataItem.INCR_AMT = Math.round(incrAmt);
                var incrPct = (dataItem.INCR_AMT * 100) / dataItem.PRE_GROSS_SALARY;
                dataItem.INCR_PCT = parseFloat(incrPct.toFixed(2));
                dataItem.NEW_GROSS = dataItem.PRE_GROSS_SALARY + dataItem.INCR_AMT;
            }
            else {
                //var addlAmtDeff = dataItem.INCR_AMT;
                dataItem.INCR_AMT = dataItem.ADDL_AMT;
                var incrPct = (dataItem.INCR_AMT * 100) / dataItem.PRE_BASIC_SALARY;
                dataItem.INCR_PCT = parseFloat(incrPct.toFixed(2));
                dataItem.NEW_GROSS = dataItem.PRE_GROSS_SALARY + dataItem.INCR_AMT;
            }


            //dataItem.INCR_AMT = dataItem.INCR_AMT + dataItem.ADDL_AMT;
              
            //var incrPct = (dataItem.INCR_AMT * 100) / dataItem.PRE_GROSS_SALARY;
            //dataItem.INCR_PCT = parseFloat(incrPct.toFixed(2));
            //dataItem.NEW_GROSS = dataItem.PRE_GROSS_SALARY + dataItem.INCR_AMT;
            //vm.incrEmpListGridDataSource.sync();
        }

        vm.onBlurIncrAmt = function (dataItem) {            
            dataItem.INCR_PCT = parseFloat(((dataItem.INCR_AMT * 100) / dataItem.PRE_GROSS_SALARY).toFixed(2));
            dataItem.NEW_GROSS = dataItem.PRE_GROSS_SALARY + dataItem.INCR_AMT;
        }

        vm.onBlurIncrPct = function (dataItem) {            
            dataItem.INCR_AMT = (dataItem.PRE_GROSS_SALARY * dataItem.INCR_PCT) / 100;
            dataItem.NEW_GROSS = dataItem.PRE_GROSS_SALARY + dataItem.INCR_AMT;
        }


        ///////============= Start Employee History Controller ============
        vm.incrHistory = function (dataItem) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'EmpIncrHistoryModal.html',
                controller: function ($modalInstance, $scope, pHR_EMPLOYEE_ID, formData, HrService) {
                    $scope.form = {};
                    console.log(formData);
                    $scope.form = formData;

                    $scope.empIncrHistoryGridOption = {
                        height: 400,
                        sortable: true,
                        scrollable: true,
                        pageable: false,
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
                            { field: "EFFECTIVE_DT", title: "Eff.Date", format: "{0:dd-MMM-yyyy}", width: "7%" },
                            {
                                headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Approved?'\"></i>",
                                template: function () {
                                    return "<input type='checkbox' title='Approved?' ng-model='dataItem.IS_APPROVED' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='true' >";
                                },
                                width: "2%"
                            },
                            { field: "PRE_GROSS_SALARY", title: "Pre.Gross", type: "string", width: "7%", filterable: false },
                            { field: "PRE_BASIC_SALARY", title: "Pre.Basic", type: "string", width: "7%", filterable: false },
                            {
                                headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Promoted?'\"></i>",
                                template: function () {
                                    return "<input type='checkbox' title='Promoted?' ng-model='dataItem.IS_PROMOTED' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='true'  >";
                                },
                                width: "2%"
                            },
                            { field: "OLD_DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "16%", filterable: false },
                            { field: "NEW_DESIGNATION_NAME_EN", title: "New Designation", type: "string", width: "16%", filterable: false },
                            { field: "INCR_GRADE_CODE", title: "Grade", type: "string", width: "5%", filterable: false },
                            { field: "ADDL_AMT", title: "S.Amt", type: "string", width: "5%", filterable: false },
                            { field: "INCR_AMT", title: "Incr.Amt", type: "string", width: "7%", filterable: false },

                            { field: "EMPLOYEE_CODE", title: "Code", type: "string", hidden: true },
                            { field: "EMP_FULL_NAME_EN", title: "Name", type: "string", hidden: true },
                            { field: "JOINING_DT", title: "Join Date", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true },                            
                            { field: "LAST_INCR_DT", title: "L.Incr.Date", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true },
                            { field: "LAST_INCR_AMT", title: "L.Incr.", type: "number", hidden: true }                           
                        ]
                    };
                    

                    $scope.empIncrHistoryGridDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                HrService.getDataByFullUrl('/api/hr/HrIncriment/GetEmpIncrHistory?pHR_EMPLOYEE_ID=' + pHR_EMPLOYEE_ID).then(function (res) {
                                    e.success(res);
                                });
                            }
                        },
                        schema: {
                            model: {
                                fields: {
                                    EMPLOYEE_CODE: { type: "string", editable: false },
                                    EMP_FULL_NAME_EN: { type: "string", editable: false },
                                    JOINING_DT: { type: "date", editable: false },
                                    LAST_INCR_DT: { type: "date", editable: false },
                                    EFFECTIVE_DT: { type: "date", editable: true }
                                }
                            }
                        }
                    });



                    $scope.cancel = function () {                        
                        $modalInstance.close();
                    };
                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData:function () {
                        return {
                            HR_EMPLOYEE_ID: dataItem.HR_EMPLOYEE_ID, EMPLOYEE_CODE: dataItem.EMPLOYEE_CODE, EMP_FULL_NAME_EN: dataItem.EMP_FULL_NAME_EN,
                            DESIGNATION_NAME_EN: dataItem.DESIGNATION_NAME_EN
                        };
                    },
                    pHR_EMPLOYEE_ID: function () {
                        return dataItem.HR_EMPLOYEE_ID;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };
        ///////============= End Employee History Controller ============



        vm.incrEmpListGridOption = {
            height: 400,
            sortable: true,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
            pageable: false,
            editable: true,
            selectable: "cell",
            navigatable: true,
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

                //vm.incrEmpListGridOption.columns[15].hidden = true;
                $('#incrEmpListGrid td').on('dblclick', function (e) {

                    if ($(".k-grid-edit-row").length <= 1) {
                        $("#incrEmpListGrid").data("kendoGrid").editCell($(this));
                        $(this).focusout(function () {
                            if (!$("#incrEmpListGrid").data("kendoGrid").select().hasClass("k-state-selected"))
                                if (!$(".k-grid-edit-row input").hasClass("k-invalid"))
                                    $("#incrEmpListGrid").data("kendoGrid").closeCell();
                        });
                    }
                });
            },
            change: function (e) {
                console.log(e);

                //vm.incrEmpListGridDataSource.fetch();
                //vm.incrEmpListGridDataSource.refresh();

                var grid = $("#incrEmpListGrid").data("kendoGrid");
                console.log(grid);

                //grid.footer.find(".k-footer-template").replaceWith(grid.footerTemplate(this.aggregates()));

            },
            columns: [
                { field: "EMPLOYEE_CODE", title: "Code", type: "string", width: "7%" },
                { field: "EMP_FULL_NAME_EN", title: "Name", type: "string", width: "10%", footerTemplate: "Total Record: #=count#" },
                { field: "JOINING_DT", title: "Join Date", type: "date", format: "{0:dd-MM-yyyy}", width: "6%", filterable: false },
                { field: "PRE_GROSS_SALARY", title: "Curr.Gross", type: "string", width: "7%", filterable: false },
                { field: "PRE_BASIC_SALARY", title: "Curr.Basic", type: "string", width: "7%", filterable: false },
                { field: "LAST_INCR_DT", title: "L.Incr.Date", type: "date", format: "{0:dd-MM-yyyy}", width: "6%", filterable: false },
                { field: "LAST_INCR_AMT", title: "L.Incr.", type: "number", width: "6%", filterable: false },
                {
                    headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Promoted?'\"></i>",
                    template: function () {
                        return "&nbsp;&nbsp;<input type='checkbox' title='Promoted?' ng-model='dataItem.IS_PROMOTED' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-click='vm.onChangePromoted(dataItem)' >";
                    },
                    width: "2%"
                },
                { field: "PRE_HR_DESIGNATION_ID", title: "PrevDesig", type: "number", width: "6%", hidden: true },
                { field: "HR_DESIGNATION_ID", title: "CurrDesig", type: "number", width: "6%", hidden: true },
                { field: "HR_DESIGNATION", title: "Designation", width: "12%", filterable: false, editor: DesigDropDownEditor, template: "#=HR_DESIGNATION.DESIGNATION_NAME_EN#" },
                { fiels: "GRADE_DISABLED", title: "", hidden: true },
                //{ fiels: "GRADE_PCT", title: "G.PCT", type: "number", width: "5%" },
                //{ fiels: "GRADE_ADDL_AMT", title: "G.AddlAmt", type: "number", hidden: true, width: "5%" },
                //{ fiels: "IS_B_G", title: "B/G", type: "string", hidden: true, width: "5%" },
                { field: "HR_GRADE", title: "Grade", width: "5%", filterable: false, hidden: $stateParams.pEMPLOYEE_TYPE_ID == 2 ? false : false, editor: GradeDropDownEditor, template: "#=HR_GRADE.INCR_GRADE_CODE#" },
                {
                    title: "S.Amt",
                    field: "ADDL_AMT",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmAddlAmt'><input type='number' class='form-control' name='ADDL_AMT' ng-model='dataItem.ADDL_AMT' ng-disabled='!dataItem.INCR_AMT_DISABLED' ng-blur='vm.onBlurSpecAmt(dataItem)' min='0' ng-style='frmAddlAmt.ADDL_AMT.$error.min? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "5%",
                    filterable: false,
                    hidden: $stateParams.pEMPLOYEE_TYPE_ID == 2 ? false : false
                },
                {
                    title: "Incr.Amt",
                    field: "INCR_AMT",
                    footerTemplate: "#=sum#",
                    template: function () {
                        return "<ng-form name='frmIncrAmt'><input type='number' class='form-control' name='INCR_AMT' ng-model='dataItem.INCR_AMT' ng-disabled='dataItem.INCR_AMT_DISABLED' ng-blur='vm.onBlurIncrAmt(dataItem)' min='1' required ng-style='(frmIncrAmt.INCR_AMT.$error.min||frmIncrAmt.INCR_AMT.$error.required)? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "5%",
                    filterable: false
                },
                {
                    title: "(%)",
                    field: "INCR_PCT",
                    footerTemplate: "#= kendo.toString(average, '0.00') #", //"#=average#",
                    template: function () {
                        return "<ng-form name='frmIncrPct'><input type='number' class='form-control' name='INCR_PCT' ng-model='dataItem.INCR_PCT' ng-disabled='dataItem.INCR_AMT_DISABLED' ng-blur='vm.onBlurIncrPct(dataItem)' min='0' ng-style='frmIncrPct.INCR_PCT.$error.min? vm.errstyle:\"\"' /></ng-form>";
                    },
                    width: "4%",
                    filterable: false
                },
                { field: "NEW_GROSS", title: "N.Gross", width: "5%", filterable: false },
                {
                    field: "EFFECTIVE_DT", title: "Eff.Date", format: "{0:dd-MMM-yyyy}", width: "8%", editor: EffectiveDateEditor,
                    template: '<span ng-style="{background: dataItem.IS_EFF_DT_CHANGED == \'Y\'? \'red\' : \'white\', color: dataItem.IS_EFF_DT_CHANGED == \'Y\'? \'white\' : \'black\'}">#: kendo.toString(EFFECTIVE_DT,\'dd-MMM-yyyy\') #</span>',
                    filterable: false
                },
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.isApprove' ng-click='vm.approveAll(vm.isApprove,0)'  ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='{{$parent.form.USER_LEVEL!=\"APPROVER\"}}'> <i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'All Approved?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Approved?' ng-model='dataItem.IS_APPROVED' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='{{$parent.form.USER_LEVEL!=\"APPROVER\"}}' >";
                    },
                    width: "3%",
                    hidden: $scope.$parent.form.USER_LEVEL != "APPROVER" ? true : false //$scope.$parent.form.USER_LEVEL
                },
                {
                    title: "",
                    template: function () {
                        return "&nbsp;&nbsp;<button class='btn btn-xs blue' title='Increment History' ng-click='vm.incrHistory(dataItem)' ><i class='fa fa-bars'> </i></button>";
                    },
                    width: "3%"
                }
            ]
        };

        //if ($scope.$parent.form.USER_LEVEL == "APPROVER") {
        //    vm.isApprove = 'Y';            
        //}


        vm.incrEmpListGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            //serverSorting: true,
            //serverFiltering: true,
            pageSize: 2000,
            transport: {
                //autosync: true,
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/hr/HrIncriment/GetEmp4IncrProposal';
                    //url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pHR_INCR_MEMO_ID=' + $scope.$parent.form.HR_INCR_MEMO_ID + '&pEMPLOYEE_TYPE_ID=' + $scope.$parent.form.EMPLOYEE_TYPE_ID
                    //    + '&pHR_DEPARTMENT_ID=' + $scope.$parent.form.HR_DEPARTMENT_ID + '&pLK_FLOOR_ID=' + $scope.$parent.form.LK_FLOOR_ID;
                    url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pHR_INCR_MEMO_ID=' + $stateParams.pHR_INCR_MEMO_ID + '&pEMPLOYEE_TYPE_ID=' + $stateParams.pEMPLOYEE_TYPE_ID
                        + '&pHR_YR_INCR_H_ID=' + $stateParams.pHR_YR_INCR_H_ID + '&pHR_DEPARTMENT_ID=' + $stateParams.pHR_DEPARTMENT_ID + '&pLK_FLOOR_ID=' + $stateParams.pLK_FLOOR_ID;

                    url += HrService.kFilterStr2QueryParam(params.filter);

                    console.log(url);

                    return HrService.GetDataByUrl(url).then(function (res) {
                        //if (res.length > 0) {
                            var empIncrList = _.filter(res, function (ob) {
                                if (ob.pHR_YR_INCR_H_ID != null) {
                                    $scope.$parent.form.HR_YR_INCR_H_ID = ob.HR_YR_INCR_H_ID;
                                    $stateParams.pHR_YR_INCR_H_ID = ob.HR_YR_INCR_H_ID;
                                }

                                //ob['IS_PROPOSER'] = $scope.$parent.form.IS_PROPOSER;
                                //ob['IS_APPROVER'] = $scope.$parent.form.IS_APPROVER;

                                return ob;
                            });                            
                        //}
                        
                            e.success(res);
                            console.log(res);

                            //$scope.$parent.form.total = res.total;
                    }, function (err) {
                        console.log(err);
                    });
                },                
            },
            aggregate: [
                { field: "EMP_FULL_NAME_EN", aggregate: "count" },
                { field: "ADDL_AMT", aggregate: "sum" },
                { field: "INCR_AMT", aggregate: "sum" },
                { field: "INCR_PCT", aggregate: "average" }
            ],
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "HR_YR_INCR_D_ID",
                    fields: {
                        EMPLOYEE_CODE: { type: "string", editable: false },
                        EMP_FULL_NAME_EN: { type: "string", editable: false },
                        //DESIGNATION_NAME_EN: { type: "string", editable: true },
                        JOINING_DT: { type: "date", editable: false },
                        PRE_GROSS_SALARY: { type: "number", editable: false },
                        PRE_BASIC_SALARY: { type: "number", editable: false },
                        LAST_INCR_DT: { type: "date", editable: false },
                        LAST_INCR_AMT: { type: "number", editable: false },                        
                        EFFECTIVE_DT: { type: "date", editable: true },                        
                        IS_B_G: { type: "string", editable: false },
                        GRADE_PCT: { type: "number", editable: false },
                        GRADE_ADDL_AMT: { type: "number", editable: false },
                        ADDL_AMT: { type: "number", editable: false },
                        INCR_AMT: { type: "number", editable: false },
                        INCR_PCT: { type: "number", editable: false },
                        NEW_GROSS: { type: "number", editable: false },
                        HR_DESIGNATION: { defaultValue: { HR_DESIGNATION_ID: "", DESIGNATION_NAME_EN: "--N/A--" }, editable: true },
                        HR_GRADE: { defaultValue: { HR_INCR_GRADE_ID: "", INCR_GRADE_CODE: "--N/A--", GRADE_AMT: 0 }, editable: true }
                    }
                }
            }
        });
               

        vm.isUpdated = 'N';
        vm.submit = function () {
            var submitData = angular.copy($scope.$parent.form);
            
            var incrGridData = vm.incrEmpListGridDataSource.data();

            submitData.INCR_DTL_XML = HrService.xmlStringShort(incrGridData.map(function (ob) {
                return {
                    HR_YR_INCR_D_ID: ob.HR_YR_INCR_D_ID, HR_YR_INCR_H_ID: ob.HR_YR_INCR_H_ID, HR_EMPLOYEE_ID: ob.HR_EMPLOYEE_ID,
                    IS_PROMOTED: ob.IS_PROMOTED, HR_DESIGNATION_ID: ob.HR_DESIGNATION_ID, HR_INCR_GRADE_ID: ob.HR_INCR_GRADE_ID,
                    PRE_GROSS_SALARY: ob.PRE_GROSS_SALARY, PRE_BASIC_SALARY: ob.PRE_BASIC_SALARY, PRE_HR_DESIGNATION_ID: ob.PRE_HR_DESIGNATION_ID,
                    ADDL_AMT: ob.ADDL_AMT, INCR_AMT: ob.INCR_AMT, INCR_PCT: ob.INCR_PCT, EFFECTIVE_DT: ob.EFFECTIVE_DT, IS_APPROVED: ob.IS_APPROVED
                };
            }));
            
            console.log(submitData);
            //return;

            return HrService.saveDataByUrl(submitData, '/api/Hr/HrIncriment/IncrBatchSave').then(function (res) {

                console.log(res);

                if (res.success === false) {
                    vm.errors = res.data.errors;
                }
                else {                    
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $scope.$parent.form.HR_YR_INCR_H_ID = res.data.PHR_YR_INCR_H_ID_RTN;
                        $scope.$parent.form.IS_P_F = res.data.PIS_P_F_RTN;
                        $scope.$parent.form.LK_INCR_STATUS_ID = res.data.PLK_INCR_STATUS_ID_RTN;
                        $scope.$parent.form.BATCH_APVRL_STATUS = res.data.PBATCH_APVRL_STATUS_RTN;
                        $scope.$parent.form.APROVER_LVL_NO = res.data.PAPROVER_LVL_NO_RTN;
                        $scope.$parent.form.PPROPOSE_BY = res.data.PPROPOSE_BY_RTN;
                        $stateParams.pPROPOSE_BY = res.data.PPROPOSE_BY_RTN;

                        vm.isUpdated = 'Y';
                        vm.incrEmpListGridDataSource.read();
                    };

                    config.appToastMsg(res.data.PMSG);

                }
            });
        };

        vm.IS_P_F = "N";
        vm.propose = function () {
            Dialog.confirm('Do you want to propose?</br></br>After propose you cannot modify any data of this batch.', 'Confirmation', ['Yes', 'No']).then(function () {
                var submitData = angular.copy($scope.$parent.form);

                return HrService.saveDataByUrl(submitData, '/api/Hr/HrIncriment/IncrProposeSave').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $scope.$parent.form.HR_YR_INCR_H_ID = res.data.PHR_YR_INCR_H_ID_RTN;
                            $scope.$parent.form.IS_P_F = res.data.PIS_P_F_RTN;
                            $scope.$parent.form.LK_INCR_STATUS_ID = res.data.PLK_INCR_STATUS_ID_RTN;
                            $scope.$parent.form.BATCH_APVRL_STATUS = res.data.PBATCH_APVRL_STATUS_RTN;
                            $scope.$parent.form.APROVER_LVL_NO = res.data.PAPROVER_LVL_NO_RTN;

                            vm.IS_P_F = res.data.PIS_P_F_RTN;
                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });

            });
        };

        vm.verify = function () {
            Dialog.confirm('Do you want to verify?</br></br>After verify you cannot modify any data of this batch.', 'Confirmation', ['Yes', 'No']).then(function () {
                var submitData = angular.copy($scope.$parent.form);

                return HrService.saveDataByUrl(submitData, '/api/Hr/HrIncriment/IncrVeryficationSave').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $scope.$parent.form.HR_YR_INCR_H_ID = res.data.PHR_YR_INCR_H_ID_RTN;
                            $scope.$parent.form.IS_P_F = res.data.PIS_P_F_RTN;
                            $scope.$parent.form.LK_INCR_STATUS_ID = res.data.PLK_INCR_STATUS_ID_RTN;
                            $scope.$parent.form.BATCH_APVRL_STATUS = res.data.PBATCH_APVRL_STATUS_RTN;
                            $scope.$parent.form.APROVER_LVL_NO = res.data.PAPROVER_LVL_NO_RTN;

                            vm.IS_P_F = res.data.PIS_P_F_RTN;
                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });

            });
        };

        vm.batchFinalize = function () {
            Dialog.confirm('Do you want to finalize?</br></br>After finalize you cannot modify any data of this batch.', 'Confirmation', ['Yes', 'No']).then(function () {
                var submitData = angular.copy($scope.$parent.form);

                return HrService.saveDataByUrl(submitData, '/api/Hr/HrIncriment/IncrFinalizeSave').then(function (res) {
                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $scope.$parent.form.HR_YR_INCR_H_ID = res.data.PHR_YR_INCR_H_ID_RTN;
                            $scope.$parent.form.IS_P_F = res.data.PIS_P_F_RTN;
                            $scope.$parent.form.LK_INCR_STATUS_ID = res.data.PLK_INCR_STATUS_ID_RTN;
                            $scope.$parent.form.BATCH_APVRL_STATUS = res.data.PBATCH_APVRL_STATUS_RTN;
                            $scope.$parent.form.APROVER_LVL_NO = res.data.PAPROVER_LVL_NO_RTN;

                            vm.IS_P_F = res.data.PIS_P_F_RTN;
                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });

            });
        };

        vm.printIncrBatch = function (buttonID) {
            //console.log(dataItem);
            
            if (buttonID == 1) { // Incr. Proposal List
                $scope.form.REPORT_CODE = 'RPT-1056';
            }
            else if (buttonID == 2) { // Incr/Promotion Letter
                $scope.form.REPORT_CODE = 'RPT-1057';
            }
            
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/Hr/HrReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.copy($scope.form);
            console.log(params);

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

        vm.backToBachList = function () {
            $state.go('IncrProposalBatchList', { pHR_INCR_MEMO_ID: $stateParams.pHR_INCR_MEMO_ID });
        }

    }

})();
//============== End for HrIncrProposalDController















//============== Start for HrIncrProposalBatchListController
(function () {
    'use strict';
    angular.module('multitex.hr').controller('HrIncrProposalBatchListController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', '$filter', HrIncrProposalBatchListController]);

    function HrIncrProposalBatchListController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams, $filter) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

       
        vm.form = { HR_INCR_MEMO_ID: 0 };

        activate();

        function activate() {
            var promise = [getSubdepartmentListData(), floorList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        
        vm.incrMemoGridOption = {
            height: 100,
            sortable: true,            
            scrollable: {
                virtual: true,
                //scrollable:true
            },
            pageable: false,
            editable: false,
            selectable: "row",
            navigatable: true,
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
                { field: "HR_INCR_MEMO_ID", title: "ID", type: "string", hidden: true },
                { field: "AUTH_NO", title: "Authorization#", type: "string", width: "12%" },
                { field: "MONTH_YEAR_NAME", title: "Incr.Period", type: "string", width: "10%" },
                { field: "COMP_NAME_EN", title: "Incr.Company", type: "string", width: "15%" },                
                { field: "INCR_TYPE_NAME_EN", title: "Increment Type", type: "string", width: "20%" },
                { field: "MEMO_NO", title: "Memo#", type: "string", width: "15%" },
                { field: "MEMO_DT", title: "Memo Date", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true }
            ],
            change: function (e) {
                var grid = $("#incrMemoGrid").data("kendoGrid");
                //grid.select("tr:eq(1)");
                var row = grid.select();
                var item = grid.dataItem(row);

                console.log(item);
                vm.form.HR_INCR_MEMO_ID = item.HR_INCR_MEMO_ID;
                vm.form.HR_MANAGEMENT_TYPE_ID = item.HR_MANAGEMENT_TYPE_ID;
                vm.form.FROM_DATE = item.START_DATE;
                vm.form.IS_FINALIZED = item.IS_FINALIZED;
                
                $scope.$apply();
                //console.log(vm.form);
                vm.incrProposalBatchGridDataSource.read();
            }
        };

        vm.incrMemoGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 10,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "HR_INCR_MEMO_ID",
                    fields: {
                        MEMO_DT: { type: "date", editable: true }
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/hr/HrIncriment/GetIncrMemoList?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pRF_INCR_TYPE_ID_LST=3,4&pIS_FINALIZED=N';
                    url += HrService.kFilterStr2QueryParam(params.filter);

                    return HrService.getDataByFullUrl(url).then(function (res) {
                        //console.log(res);
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });

        
        vm.printIncrPropBatch = function (dataItem) {
            console.log(dataItem);

            vm.form.REPORT_CODE = 'RPT-1056';
            vm.form.HR_DEPARTMENT_ID = dataItem.HR_DEPARTMENT_ID;
            vm.form.LK_FLOOR_ID = dataItem.LK_FLOOR_ID;            

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/Hr/HrReport/PreviewReport');
            form.setAttribute("target", '_blank');
            
            var params = angular.copy(vm.form);
            console.log(params);

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



        vm.incrProposalBatchGridOption = {
            height: 400,
            sortable: true,
            scrollable: true,
            //scrollable: {
            //    virtual: true,                
            //},
            pageable: false,
            editable: false,
            selectable: "row",
            navigatable: true,
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
                { field: "HR_YR_INCR_H_ID", title: "ID", type: "string", hidden: true },
                { field: "SECTION_NAME", title: "Section", type: "string", width: "250px", footerTemplate: "Total Record: #=count#" },
                { field: "FLOOR_DESC_EN", title: "Floor", type: "string", width: "80px" },
                //{ field: "BATCH_STATUS", title: "Status", type: "string", width: "250px" },
                {
                    title: "Status",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.LK_INCR_STATUS_ID<508'>{{dataItem.BATCH_STATUS}}</label> <label class='label label-sm label-danger' ng-show='dataItem.LK_INCR_STATUS_ID==508'>{{dataItem.BATCH_STATUS}}</label> <label class='label label-sm label-success' ng-show='dataItem.LK_INCR_STATUS_ID==510'>{{dataItem.BATCH_STATUS}}</label>";
                    },
                    width: "250px"                    
                },
                { field: "TOTAL_EMPLOYEE", title: "Tot.Emp", type: "number", width: "90px", footerTemplate: "#=sum#" },
                { field: "TOTAL_INCR_AMT", title: "Tot.Incr.", type: "number", width: "100px", footerTemplate: "#=sum#" },
                {
                    title: "",
                    template: function () {
                        return "<a class='btn btn-xs blue' ui-sref='IncrimentProposalH({pHR_YR_INCR_H_ID:dataItem.HR_YR_INCR_H_ID, pHR_INCR_MEMO_ID: dataItem.HR_INCR_MEMO_ID, pEMPLOYEE_TYPE_ID: dataItem.EMPLOYEE_TYPE_ID, pHR_DEPARTMENT_ID: dataItem.HR_DEPARTMENT_ID, pLK_FLOOR_ID: dataItem.LK_FLOOR_ID, pIS_FROM_BATCH_LIST: \"Y\"})' ><i class='fa fa-edit'> Edit</i></a>&nbsp;<a ng-click='vm.printIncrPropBatch(dataItem)' class='btn btn-xs blue'><i class='fa fa-print'> Print</i></a>";
                    },
                    width: "90px"
                }
            ]
        };

        vm.incrProposalBatchGridDataSource = new kendo.data.DataSource({
            //serverPaging: true,
            //serverSorting: true,
            //serverFiltering: true,
            //pageSize: 10,
            aggregate: [
                { field: "SECTION_NAME", aggregate: "count" },
                { field: "TOTAL_EMPLOYEE", aggregate: "sum" },
                { field: "TOTAL_INCR_AMT", aggregate: "sum" }
            ],
            schema: {
                //data: "data",
                //total: "total",
                model: {
                    id: "HR_YR_INCR_H_ID",
                    fields: {
                        TOTAL_EMPLOYEE: { type: "number", editable: false },
                        TOTAL_INCR_AMT: { type: "number", editable: false }
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/hr/HrIncriment/GetIncrBatchList?pHR_INCR_MEMO_ID=' + vm.form.HR_INCR_MEMO_ID;
                    //url += HrService.kFilterStr2QueryParam(params.filter);

                    return HrService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });
                }
            }
        });


        function parentDepartmentList() {
            vm.parentDepartmentListData = {
                optionLabel: "-- Select Department --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                dataSource: {
                    transport: {
                        read: function (e) {
                            $http({
                                method: 'post',
                                url: "/Hr/HrEmployee/ParentDeptListData"  //+ "&pType=" + showType
                            }).
                            success(function (data, status, headers, config) {
                                e.success(data)
                            }).
                            error(function (data, status, headers, config) {
                                alert('something went wrong')
                                console.log(status);
                            });
                        }
                    }//,
                    //group: { field: "PARENT_NAME" }
                },
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.HR_DEPARTMENT_ID; //this.value();                    

                    return vm.subdepartmentDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {

                                return HrService.getSubDepartmentData(dataItem.HR_DEPARTMENT_ID).then(function (res) {
                                    console.log(res);
                                    e.success(res);
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                        }
                    });
                }
            };
        };

        function getSubdepartmentListData() {
            vm.subdepartmentOption = {
                optionLabel: "-- Select Section --",
                filter: "contains",
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    $scope.form.CORE_DEPT_ID = dataItem.CORE_DEPT_ID;
                }
            };

            return vm.subdepartmentDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        return HrService.getDataByFullUrl('/api/hr/HrIncriment/GetSection4IncrProp').then(function (res) {
                            e.success(res);
                        });

                    }
                }
            });
        };

        function floorList() {
            vm.prodFloorOption = {
                optionLabel: "-- Select Floor --",
                autoBind: true,
                dataTextField: "FLOOR_CODE",
                dataValueField: "LK_FLOOR_ID"
            };

            return vm.prodFloorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        return HrService.getDataByFullUrl('/api/hr/HrIncriment/GetFloor4IncrProp').then(function (res) {
                            e.success(res);
                        });

                    }
                }
            });


            //return vm.floorListData = {
            //    optionLabel: "-- Select Floor --",                
            //    autoBind: true,
            //    dataSource: {
            //        transport: {
            //            read: function (e) {
            //                $http({
            //                    method: 'post',
            //                    url: "/Hr/HrEmployee/LookupListData",  //+ "&pType=" + showType
            //                    params: {
            //                        pLookupTableId: 18
            //                    }
            //                }).
            //                success(function (data, status, headers, config) {
            //                    e.success(data)
            //                }).
            //                error(function (data, status, headers, config) {
            //                    alert('something went wrong')
            //                    console.log(status);
            //                });
            //            }
            //        }
            //    },
            //    select: function (e) {
            //        var dataItem = this.dataItem(e.item);                    
            //        vm.form.FLOOR_NAME = dataItem.LK_DATA_NAME_EN;
            //    },
            //    dataTextField: "LK_DATA_NAME_EN",
            //    dataValueField: "LOOKUP_DATA_ID"
            //};
        };

        vm.empLoad = function () {
            vm.IS_NEXT = 'Y';

            HrService.getDataByFullUrl('/api/hr/HrIncriment/GetIncrHdr?pHR_YR_INCR_H_ID=' + vm.form.HR_YR_INCR_H_ID + '&pHR_INCR_MEMO_ID=' + vm.form.HR_INCR_MEMO_ID
                                        + '&pHR_DEPARTMENT_ID=' + vm.form.HR_DEPARTMENT_ID + '&pLK_FLOOR_ID=' + vm.form.LK_FLOOR_ID).then(function (res) {
                                            console.log(res);
                                            vm.form.HR_YR_INCR_H_ID = res.HR_YR_INCR_H_ID;
                                            vm.form.IS_P_F = (res.IS_P_F == null ? 'N' : res.IS_P_F);
                                            vm.form.IS_PROPOSER = (res.IS_PROPOSER == null ? 'N' : res.IS_PROPOSER);
                                            vm.form.IS_APPROVER = (res.IS_APPROVER == null ? 'N' : res.IS_APPROVER);
                                            vm.form.total = 0;

                                            if (vm.form.IS_P_F != undefined) {
                                                $state.go('IncrimentProposalH.Dtl', {
                                                    pHR_YR_INCR_H_ID: vm.form.HR_YR_INCR_H_ID, pHR_INCR_MEMO_ID: vm.form.HR_INCR_MEMO_ID, pEMPLOYEE_TYPE_ID: vm.form.EMPLOYEE_TYPE_ID,
                                                    pHR_DEPARTMENT_ID: vm.form.HR_DEPARTMENT_ID, pLK_FLOOR_ID: vm.form.LK_FLOOR_ID
                                                }, { reload: 'IncrimentProposalH.Dtl' });

                                                return;
                                            }

                                        }, function (err) {
                                            console.log(err);
                                        });

        }

    }

})();
//============== End for HrIncrProposalHController


















//============== Start for HrIncrProcessController
(function () {
    'use strict';
    angular.module('multitex.hr').controller('HrIncrProcessController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', '$filter', 'Dialog', HrIncrProcessController]);

    function HrIncrProcessController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams, $filter, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;


        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.today = new Date();
        vm.periodType = 3;
        vm.form = {};



        activate();

        function activate() {
            var promise = [getCompanyList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        vm.memoDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.memoDateOpened = true;
        }

        vm.effectiveDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.effectiveDateOpened = true;
        }

        function getCompanyList() {

            vm.compOptions = {
                optionLabel: "-- Select Company --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    console.log(dataItem);
                    return vm.compPayPeriodDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                return HrService.getDataByFullUrl("/api/common/GetAccPayPeriod/?pHR_COMPANY_ID=" + dataItem.HR_COMPANY_ID + "&pHR_PERIOD_TYPE_ID=" + vm.periodType + "&pIS_CLOSED=N").then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    });
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.GetAllOthers('/api/common/CompanyList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                }
            }
        };


        vm.onChangePeriod = function (e) {
            var item = e.sender.dataItem(e.item);// e.sender.dataSource.data();
            vm.form.START_DATE = item.START_DATE;
            vm.form.END_DATE = item.END_DATE;
        };

        
        vm.incrProcess = function () {
            Dialog.confirm('Do you want to effect employee increment of selected company and period?', 'Confirmation', ['Yes', 'No']).then(function () {

                return HrService.saveDataByUrl(vm.form, '/api/Hr/HrIncriment/IncrEffectProcess').then(function (res) {
                    if (res.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        console.log(res);
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });

            });
        }


    }

})();
//============== End for HrIncrProcessController















//============== Start for IncrHistory4EmpController
(function () {
    'use strict';
    angular.module('multitex.hr').controller('IncrHistory4EmpController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', '$filter', 'formData', IncrHistory4EmpController]);

    function IncrHistory4EmpController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams, $filter, formData) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        var key = 'HR_EMPLOYEE_ID';

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.today = new Date();
        vm.periodType = 3;


        vm.form = formData[key] ? formData : {};



        activate();

        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        

        vm.emoloyeeAuto = function (val) {
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        vm.onSelectItem = function (item) {
            console.log(item);

            vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;
            vm.form.EMPLOYEE_CODE = item.EMPLOYEE_CODE;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;
                vm.empIncrHistoryGridDataSource.read();
            }

        });

        vm.empIncrHistoryGridOption = {
            height: 400,
            sortable: true,
            scrollable: true,
            pageable: false,
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
                { field: "EFFECTIVE_DT", title: "Eff.Date", format: "{0:dd-MMM-yyyy}", width: "7%" },
                {
                    headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Approved?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Approved?' ng-model='dataItem.IS_APPROVED' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='true' >";
                    },
                    width: "2%"
                },
                { field: "PRE_GROSS_SALARY", title: "Pre.Gross", type: "string", width: "7%", filterable: false },
                { field: "PRE_BASIC_SALARY", title: "Pre.Basic", type: "string", width: "7%", filterable: false },
                {
                    headerTemplate: "<i class=\"fa fa-info-circle\"  kendo-tooltip k-content=\"'Promoted?'\"></i>",
                    template: function () {
                        return "<input type='checkbox' title='Promoted?' ng-model='dataItem.IS_PROMOTED' ng-true-value='\"Y\"' ng-false-value='\"N\"' ng-disabled='true'  >";
                    },
                    width: "2%"
                },
                { field: "OLD_DESIGNATION_NAME_EN", title: "Old Designation", type: "string", width: "16%", filterable: false },
                { field: "NEW_DESIGNATION_NAME_EN", title: "New Designation", type: "string", width: "16%", filterable: false },
                { field: "INCR_GRADE_CODE", title: "Grade", type: "string", width: "5%", filterable: false },
                { field: "ADDL_AMT", title: "S.Amt", type: "string", width: "5%", filterable: false },
                { field: "INCR_AMT", title: "Incr.Amt", type: "string", width: "7%", filterable: false },

                { field: "EMPLOYEE_CODE", title: "Code", type: "string", hidden: true },
                { field: "EMP_FULL_NAME_EN", title: "Name", type: "string", hidden: true },
                { field: "JOINING_DT", title: "Join Date", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true },
                { field: "LAST_INCR_DT", title: "L.Incr.Date", type: "date", format: "{0:dd-MMM-yyyy}", hidden: true },
                { field: "LAST_INCR_AMT", title: "L.Incr.", type: "number", hidden: true }
            ]
        };


        vm.empIncrHistoryGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    HrService.getDataByFullUrl('/api/hr/HrIncriment/GetEmpIncrHistory?pHR_EMPLOYEE_ID=' + (vm.form.HR_EMPLOYEE_ID || $stateParams.pHR_EMPLOYEE_ID)).then(function (res) {
                        e.success(res);
                    });
                }
            },
            schema: {
                model: {
                    fields: {
                        EMPLOYEE_CODE: { type: "string", editable: false },
                        EMP_FULL_NAME_EN: { type: "string", editable: false },
                        JOINING_DT: { type: "date", editable: false },
                        LAST_INCR_DT: { type: "date", editable: false },
                        EFFECTIVE_DT: { type: "date", editable: true }
                    }
                }
            }
        });


        vm.showHistoryList = function () {
            vm.empIncrHistoryGridDataSource.read();
            $state.go('IncrHistory4Emp', { pHR_EMPLOYEE_ID: vm.form.HR_EMPLOYEE_ID }, { notify: false });
        }
      

    }

})();
//============== End for IncrHistory4EmpController