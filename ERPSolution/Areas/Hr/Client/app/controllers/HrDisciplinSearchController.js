(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrDisciplinSearchController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'HrService', 'entityService', HrDisciplinSearchController]);

    function HrDisciplinSearchController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, HrService, entityService) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        vm.insert = true;
        vm.today = new Date();
        vm.form = {};

        $scope.emoloyeeAuto = function (val) {
            return $http.get('/Hr/HrEmployee/EmployeeAutoData', {
                params: {
                    pEMPLOYEE_CODE: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        $scope.onSelectItem = function (item) {
            //console.log(item);
            vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;           
        }

        $scope.$watch('vm.form.EMPLOYEE_CODE', function (newVal, oldVal) {

            if (!newVal || newVal == '') {
                vm.form.HR_EMPLOYEE_ID = null;                
            }

        });
            
        $scope.disActionAuto = function (val) {
            return $http.get('/Hr/HrDisciplinAction/AutoSearchDisciplinActionListData', {
                params: {
                    pDISP_ACT_REF_NO: val
                }
            }).then(function (response) {
                return response.data;
            });
        };

        //$scope.onDisActionSelectItem = function (item) {
        //    //console.log(item);
        //    vm.form.HR_EMPLOYEE_ID = item.HR_EMPLOYEE_ID;
        //}


        vm.addNew = function () {
            return $state.go('DisciplinAction');
        };

        vm.show = function () {

            $http({
                url: '/Hr/HrDisciplinAction/DisciplinActionListData',
                method: 'get',
                params: {
                    pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID,
                    pRF_CAL_MONTH_ID: vm.form.RF_CAL_MONTH_ID, pHR_EMPLOYEE_ID: vm.form.HR_EMPLOYEE_ID, pDISP_ACT_REF_NO: vm.form.DISP_ACT_REF_NO
                }
            })
            .then(function (res) {
                                
                var disciplinDataSource = new kendo.data.DataSource({
                    data: res.data,
                    pageSize: 10
                });

                //console.log(disciplinDataSource);
                $state.go('DisciplinList.data', { dataSource: disciplinDataSource });

                return;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

            
        };


        //function getCompanyList() {
        //    return vm.companyList = {
        //        optionLabel: "-- Select Company --",
        //        filter: "startswith",
        //        autoBind: true,
        //        //index:1,
        //        dataSource: {
        //            transport: {
        //                read: function (e) {
        //                    HrService.getCompanyData().then(function (res) {
        //                        e.success(res);

        //                        //$('#ACC_PAY_PERIOD_ID').kendoDropDownList({
        //                        //    //optionLabel: "-- Select Period --",
        //                        //    filter: "startswith",
        //                        //    autoBind: true,
        //                        //    dataSource: {
        //                        //        transport: {
        //                        //            read: function (e) {
        //                        //                $http({
        //                        //                    method: 'post',
        //                        //                    url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
        //                        //                    params: {
        //                        //                        pHR_COMPANY_ID: res[0].HR_COMPANY_ID,
        //                        //                        pHR_PERIOD_TYPE_ID: 3
        //                        //                    }
        //                        //                }).
        //                        //                success(function (data, status, headers, config) {
        //                        //                    e.success(data);
        //                        //                    vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;
        //                        //                }).
        //                        //                error(function (data, status, headers, config) {
        //                        //                    alert('something went wrong')
        //                        //                    console.log(status);
        //                        //                });
        //                        //            }
        //                        //        }
        //                        //    },
        //                        //    dataTextField: "REMARKS",
        //                        //    dataValueField: "ACC_PAY_PERIOD_ID"
        //                        //});

        //                    });
        //                }
        //            }
        //        },
        //        dataTextField: "COMP_NAME_EN",
        //        dataValueField: "HR_COMPANY_ID",
        //        select: function (e) {
        //            //alert('a');
        //            var dataItem = this.dataItem(e.item.index());
        //            var vHR_COMPANY_ID = dataItem.HR_COMPANY_ID; //this.value();

        //            $('#ACC_PAY_PERIOD_ID').kendoDropDownList({
        //                optionLabel: "-- Select Period --",
        //                filter: "startswith",
        //                autoBind: true,
        //                dataSource: {
        //                    transport: {
        //                        read: function (e) {
        //                            $http({
        //                                method: 'post',
        //                                url: "/Hr/HrDisciplinAction/CompanyOpenPeriodListData",  //+ "&pType=" + showType
        //                                params: {
        //                                    pHR_COMPANY_ID: vHR_COMPANY_ID,
        //                                    pHR_PERIOD_TYPE_ID: 3
        //                                }
        //                            }).
        //                            success(function (data, status, headers, config) {
        //                                e.success(data);
        //                                //if (data.length > 0) {
        //                                //    //vm.form.ACC_PAY_PERIOD_ID = data[0].ACC_PAY_PERIOD_ID;

        //                                //    dt = moment(data[0].START_DATE)._d;
        //                                //    vm.form.FROM_DATE = $filter('date')(dt, vm.dtFormat);

        //                                //    dt = moment(data[0].END_DATE)._d;
        //                                //    vm.form.TO_DATE = $filter('date')(dt, vm.dtFormat);
        //                                //    alert(vm.form.FROM_DATE);
        //                                //    vm.form.NO_OF_DAYS = (vm.form.TO_DATE - vm.form.FROM_DATE);
        //                                //};
        //                            }).
        //                            error(function (data, status, headers, config) {
        //                                alert('something went wrong')
        //                                console.log(status);
        //                            });
        //                        }
        //                    }
        //                },
        //                dataTextField: "MONTH_YEAR_NAME",
        //                dataValueField: "ACC_PAY_PERIOD_ID",
        //                select: function (e) {
        //                    var dataItem = this.dataItem(e.item.index());

        //                    dt = moment(dataItem.START_DATE)._d;
        //                    vm.form.FROM_DATE = $filter('date')(dt, vm.dtFormat);

        //                    var dt1 = moment(dataItem.END_DATE)._d;
        //                    vm.form.TO_DATE = $filter('date')(dt, vm.dtFormat);

        //                    vm.form.NO_OF_DAYS = moment(dt1).diff(dt, 'days') + 1;
        //                    //console.log(dataItem);

        //                }
        //            });
        //        }

        //    };

        //};

        vm.companyListData = {
            optionLabel: "-- Select --",
            filter: "startswith",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'post',
                            url: "/Hr/HrEmployee/CompanyListData"
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                }
            },
            dataTextField: "COMP_NAME_EN",
            dataValueField: "HR_COMPANY_ID",
            index:0,
            select: function (e) {
                var dataItem = this.dataItem(e.item);
                var vCompID = dataItem.HR_COMPANY_ID; //this.value();                
                //alert(vCompID);
                if (vCompID == '') {
                    vCompID = 0;
                }

                if (vCompID != null) {

                    $http({
                        url: '/Hr/HrDisciplinAction/CompanyAllPeriodListData',
                        method: 'get',
                        params: { pHR_COMPANY_ID: vCompID, pHR_PERIOD_TYPE_ID:3 }
                    })
                     .then(function (result) {
                         vm.form.RF_FISCAL_YEAR_ID = null;
                         vm.form.RF_CAL_MONTH_ID = null;
                         //console.log(result);
                         $("#ALL_PERIOD_ID").kendoDropDownList({
                             optionLabel: "-- Select --",
                             dataTextField: "MONTH_YEAR_NAME",
                             dataValueField: "RF_FISCAL_YEAR_ID",
                             dataSource: result,
                             filter: "startswith",
                             select: function (e) {
                                 var dataItem = this.dataItem(e.item);
                                 vm.form.RF_FISCAL_YEAR_ID = dataItem.RF_FISCAL_YEAR_ID;
                                 vm.form.RF_CAL_MONTH_ID = dataItem.RF_CAL_MONTH_ID;
                             }
                         });

                         return;
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });                    

                }

            }
        };

        

        function activate(){
            var promise = [];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();