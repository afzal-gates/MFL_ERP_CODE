(function () {
    'use strict';
    angular.module('multitex.hr').controller('LeaveTransEditController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state','$scope', LeaveTransEditController]);
    function LeaveTransEditController($q, config, HrService, $filter, $http, $stateParams, $state, $scope) {

        var vm = this;
        vm.form = {};
        activate()
        vm.showSplash = true;

        function activate() {
            var promise = [getCompanyData(), getFiscalYearData(), getLeaveType(),getLookupListData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        function getCompanyData() {
            return HrService.getCompanyData().then(function (res) {
                //vm.form['HR_COMPANY_ID'] = res[0]['HR_COMPANY_ID'];
                vm.companyData = res;
            });
        }

        //$scope.$watch('vm.form.HR_COMPANY_ID', function (newVal, oldVal) {
        //    if (newVal != oldVal) {
        //        vm.companyId = newVal;
        //    }
        //});


        vm.onSelectPeriod = function (periodData) {
            vm.form['RF_FISCAL_YEAR_ID'] = periodData.RF_FISCAL_YEAR_ID;
            vm.form['RF_CAL_MONTH_ID'] = periodData.RF_CAL_MONTH_ID;
        }

        $scope.init = function (HR_COMPANY_ID) {
            return $http({
                method: 'get',
                url: '/Hr/Hrleavetrans/CompanyAllPeriodListData',
                params: { pHR_COMPANY_ID: HR_COMPANY_ID, pHR_PERIOD_TYPE_ID:3 }
            }).then(function (res) {
                console.log(res.data);
                vm.allPeriodListData = res.data;

            }).catch(function (message) {
                console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
        }


        function getLookupListData() {
            return HrService.getLookupListData(17).then(function (res) {
                vm.leaveStatusList = res;
            });
        }



        function getLeaveType() {
            return HrService.getLeaveTypeData().then(function (res) {
                console.log(res);
                vm.LeaveTypeData = res;
                return vm.LeaveTypeData;
            });
        }



        function getFiscalYearData() {
            return HrService.getDataByFullUrl('/api/common/GetPayFiscalYear?pIS_CLOSED=N').then(function (res) {
                vm.FiscalYearData = res;
                return; //vm.form['RF_FISCAL_YEAR_ID'] = res[0]['RF_FISCAL_YEAR_ID'];
            });
        }


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
            console.log(item);
            vm.form['HR_EMPLOYEE_ID'] = item.HR_EMPLOYEE_ID;
        }

        vm.loadLeaveTransData = function (data) {
            if (!data.EMPLOYEE_CODE && data.EMPLOYEE_CODE == '') {
                data['HR_EMPLOYEE_ID'] = null;
            }
            return HrService.loadLeaveTransData(data).then(function (res) {
                console.log(res);
                var dataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize: 10,
                    schema: {
                        model: {
                            fields: {
                                FROM_DATE: { type: "date" },
                                TO_DATE: { type: "date" },
                                APPLY_DATE: { type: "date" },
                                APPROVE_DATE: { type: "date" },
                                EDD_DT: { type: "date" }
                            }
                        }
                    }
                });
                $state.go('LeaveTransEdit.data', { dataSource: dataSource });
            });
        }
     
        //vm.openPeriodListData = {
        //    optionLabel: "-- Select --",
        //    filter: "startswith",
        //    autoBind: true,
        //    dataSource: {
        //        transport: {
        //            read: function (e) {
        //                $http({
        //                    method: 'post',
        //                    url: "/Hr/Hrleavetrans/CompanyAllPeriodListData",  //+ "&pType=" + showType
        //                    params: {
        //                        pHR_COMPANY_ID: vm.form.HR_COMPANY_ID||0,
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
        //    dataTextField: "MONTH_YEAR_NAME",
        //    dataValueField: "RF_FISCAL_YEAR_ID",
        //    //index:0,
        //    //template: ' #if(data.FY_NAME_EN!=undefined){#<span> #= data.MONTH_NAME_EN + "-" + data.FY_NAME_EN #</span>#}#' // '<span>#= data.MONTH_NAME_EN + "-" + data.FY_NAME_EN #</span>'
        //    select: function (e) {
        //        var dataItem = this.dataItem(e.item.index());
        //        var dt = moment(dataItem.END_DATE)._d;
        //        vm.form.EFFECTIVE_DT = $filter('date')(dt, vm.dtFormat);
        //        vm.form.RF_CAL_MONTH_ID = dataItem.RF_CAL_MONTH_ID;
        //    }
        //};

    }

})();