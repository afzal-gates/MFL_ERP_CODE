(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrEmployeeRejoinController', ['logger', 'config', 'HrService', '$q', '$scope', '$http', '$location', '$state', '$stateParams', HrEmployeeRejoinController]);

    function HrEmployeeRejoinController(logger, config, HrService, $q, $scope, $http, $location, $state, $stateParams) {
        
        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.showSplash = true;
        vm.today = new Date();
        vm.insert = true;
        vm.form = {};

        vm.isBangla = false;
        vm.form.REJOIN_DT = vm.today;
        
        vm.rejoinDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.rejoinDateOpened = true;
        };

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.isNext = false;
        vm.next = function () {
            vm.isNext = true;
            vm.rejoinGrid();
        };

        vm.reset = function () {
            vm.insert = true;
            vm.formCopy = vm.form;
            vm.form = {};

            vm.form.HR_COMPANY_ID = vm.formCopy.HR_COMPANY_ID;
            vm.form.ACC_PAY_PERIOD_ID = vm.formCopy.ACC_PAY_PERIOD_ID;
            vm.form.REJOIN_DT = vm.formCopy.REJOIN_DT;
        };

        vm.editRecord = function (dataItem) {
            vm.insert = false;
            
            //$routeParams = dataItem;
            //$location.path('/OfficeEntry');

            //console.log($routeParams);
            vm.form = dataItem;
            //console.log(dataItem);

            return $state.go('EmployeeEntry', { pEmployeeObj: dataItem });
        };

        $scope.emoloyeeAutoMigrated = function (val) {
            return HrService.EmployeeAutoDataList(val, '31,32,148', vm.form.HR_COMPANY_ID).then(function (res) {
                return res;
            });        
        };

        $scope.onSelectItem = function (item) {
            //console.log(item);            
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN;
            vm.form.DEPARTMENT_NAME_EN = item.DEPARTMENT_NAME_EN;
            vm.form.DESIGNATION_NAME_EN = item.DESIGNATION_NAME_EN;
            vm.form.COMP_NAME_EN = item.COMP_NAME_EN;
            //vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;
            vm.form.EMP_FULL_NAME_EN = item.EMP_FULL_NAME_EN + ', ' + item.DESIGNATION_NAME_EN + ', ' + item.DEPARTMENT_NAME_EN;
            vm.form.TA_PROXI_ID_OLD = item.TA_PROXI_ID;
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;
                vm.form.EMP_FULL_NAME_EN = null;
                vm.form.DEPARTMENT_NAME_EN = null;
                vm.form.DESIGNATION_NAME_EN = null;
                vm.form.COMP_NAME_EN = null;
                //vm.form.HR_COMPANY_ID = null;
                vm.form.TA_PROXI_ID_OLD = null;
            }

        });

        $scope.$watchGroup(['vm.form.HR_COMPANY_ID', 'vm.form.ACC_PAY_PERIOD_ID'], function (newVal, oldVal) {
            //alert(newVal); 
            if (newVal[0] != null && newVal[1] != null) {
                $("#rejoinGrid").empty();
                vm.isNext = false;
            }
        });


        function getCompanyList() {            
            return vm.companyList = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                //index:1,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getCompanyData().then(function (res) {
                                e.success(res);

                                $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                                    optionLabel: "-- Select --",
                                    filter: "startswith",
                                    autoBind: true,
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                $http({
                                                    method: 'post',
                                                    url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                                    params: {
                                                        pHR_COMPANY_ID: res[0].HR_COMPANY_ID,
                                                        pHR_PERIOD_TYPE_ID:3
                                                    }
                                                }).
                                                success(function (data, status, headers, config) {
                                                    e.success(data);                                                    
                                                    vm.form.ACC_PAY_PERIOD_ID = res[0].ACC_PAY_PERIOD_ID;                                                    
                                                }).
                                                error(function (data, status, headers, config) {
                                                    alert('something went wrong')
                                                    console.log(status);
                                                });
                                            }
                                        }
                                    },
                                    dataTextField: "MONTH_YEAR_NAME",
                                    dataValueField: "ACC_PAY_PERIOD_ID"
                                });


                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    //alert('a');
                    var dataItem = this.dataItem(e.item);
                    var vHR_COMPANY_ID = dataItem.HR_COMPANY_ID; //this.value();

                    $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
                        optionLabel: "-- Select --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    $http({
                                        method: 'post',
                                        url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
                                        params: {
                                            pHR_COMPANY_ID: vHR_COMPANY_ID,
                                            pHR_PERIOD_TYPE_ID:3
                                        }
                                    }).
                                    success(function (data, status, headers, config) {
                                        e.success(data);                                        
                                    }).
                                    error(function (data, status, headers, config) {
                                        alert('something went wrong')
                                        console.log(status);
                                    });
                                }
                            }
                        },
                        dataTextField: "MONTH_YEAR_NAME",
                        dataValueField: "ACC_PAY_PERIOD_ID"
                    });
                }
            };
        };


        vm.rejoinGrid = function () {

            var dataSource = new kendo.data.DataSource({
                batch: true,
                transport: {
                    read: function (e) {
                        HrService.getRejoinListData(vm.form.HR_COMPANY_ID, vm.form.ACC_PAY_PERIOD_ID).then(function (res) {
                            e.success(res);
                        });
                    },

                    destroy: function (e) {
                        //console.log(e);
                        HrService.deleteRejoinData(e.data.models, vm.antiForgeryToken).then(function (res) {
                            //config.appToastMsg(res.vMsg);
                            e.success();

                        });
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }
                },
                pageSize: 10,
                schema: {
                    model: {
                        id: "HR_EMP_REJOIN_ID",
                        fields: {
                            EMPLOYEE_CODE: { type: "string", editable: false },
                            EMP_FULL_NAME_EN: { type: "string", editable: false },
                            DESIGNATION_NAME_EN: { type: "string", editable: false },
                            REJOIN_DT: { type: "date", editable: false },                            
                            REMARKS_EN: { type: "string", editable: true }                            
                        }
                    }
                }
            });


            
            $("#rejoinGrid").kendoGrid({
                dataSource: dataSource,
                navigatable: true,
                pageable: {
                    refresh: true,
                    pageSizes: true
                },
                //height: 550,
                //toolbar: ["save", "cancel"],
                columns: [
                    { field: "EMPLOYEE_CODE", title: "Code", type: "string", width: "80px" },
                    { field: "EMP_FULL_NAME_EN", title: "Name", type: "string", width: "150px" },
                    { field: "DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "150px" },
                    { field: "REJOIN_DT", title: "Re-join Date", type: "date", format: "{0:dd-MMM-yyyy}", width: "100px" },
                    { field: "REMARKS_EN", title: "Remarks", type: "string", width: "150px", filterable: false },                    
                    { command: "destroy", text: "Remove", title: "&nbsp;", width: "80px" }
                    //{ command: [{ text: "Remove", click:vm.test1() }], title: 'Actions' }
                    //{
                    //    title: "Action",
                    //    template: function () {
                    //        return "<a class='btn btn-xs blue' ng-click='vm.test1()' onclick='vm.removeRecord(dataItem)'  title='Remove'><i class='fa fa-remove'></i> Remove</a>";
                    //        //return "<a ng-click='vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
                    //    },
                    //    width: "70px"
                    //}
                ],
                editable: {
                    confirmation: false, //"Do you want to remove this record?",
                    mode: "inline"
                }
            });
            
        };
        
        vm.employeeRejoin = function (isValid, form) {
            $http({
                headers: { "RequestVerificationToken": vm.antiForgeryToken },
                url: '/Hr/HrEmployee/EmployeeRejoinSave',
                method: 'post',
                data: form
            }).success(function (data, status, headers, config1) {
                vm.errors = undefined;
                if (data.success === false) {
                    vm.errors = data.errors;
                }
                else {
                    if (data.vMsg.substr(0, 9) == 'MULTI-001') {
                        vm.rejoinGrid();
                        vm.reset();
                    };
                    config.appToastMsg(data.vMsg);

                }
            }).error(function (data, status, headers, config) {
                alert('something went wrong')
                console.log(status);
            });
        };


        //vm.gridOptions = {
        //    autoBind: true,
        //    dataSource: $stateParams.dataSource,
        //    filterable: {
        //        mode: "row"
        //    },
        //    selectable: "row",
        //    sortable: true,
        //    pageSize: 10,
        //    pageable: {
        //        refresh: true,
        //        pageSizes: true,
        //        buttonCount: 5
        //    },
        //    columns: [
        //        { field: "DEPARTMENT_NAME_EN", title: "Section", type: "string", width: "200px" },
        //        { field: "DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "200px" },
        //        { field: "FLOOR_NAME_EN", title: "Floor", type: "string", width: "100px" },
        //        { field: "LINE_NO", title: "Line#", type: "string", width: "100px" },
        //        {
        //            title: "Action",
        //            template: function () {
        //                return "<a class='btn btn-xs blue' ng-click='vm.editRecord(dataItem)'  title='Edit'><i class='fa fa-edit'></i>Edit</a>";
        //                //return "<a ng-click='vm.editRecord(dataItem)' title='Edit'><i class='fa fa-edit'></i></a>";
        //            },
        //            width: "50px"
        //        }

        //    ]
        //};
                                
                           

        function activate(){
            var promise = [getCompanyList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        //function getData(){
        //     return AdminService.getData().then(function (data) {
        //         //vm.schedulerOptions.dataSource.data = data;
        //         //return vm.schedulerOptions.dataSource.data;
           
        //    });
        //}

 
        

            
        
    }

    

})();