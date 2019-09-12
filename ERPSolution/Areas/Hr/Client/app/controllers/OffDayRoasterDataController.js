(function () {
    'use strict';
    angular.module('multitex.hr').controller('OffDayRoasterDataController', ['$q', 'config', 'HrService', '$filter', '$http', '$stateParams', '$state', '$scope', OffDayRoasterDataController]);
    function OffDayRoasterDataController($q, config, HrService, $filter, $http, $stateParams, $state, $scope) {

        var vm = this;
        vm.form = {};
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
        


        $scope.EFFECTIVE_FROMopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.EFFECTIVE_FROMopened = true;
        };

        $scope.EXPIRED_ONopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.EXPIRED_ONopened = true;
        };

        vm.data = $stateParams.formData;

        vm.dtFormat = config.appDateFormat;
        vm.toDay = $filter('date')(new Date(), config.appDateFormat);
        vm.HR_COMPANY_ID = $stateParams.HR_COMPANY_ID;
        vm.showExpiredDate = false;

        //if ($stateParams.effectiveDate) {
        //    vm.showExpiredDate = true;
        //  vm.form['EFFECTIVE_FROM'] = $filter('date')(moment($stateParams.effectiveDate)._d, config.appDateFormat);
        //}
        

    

        vm.buttonHide = angular.equals({}, vm.data);

        vm.updateData = function () {
            var datas = [];

            vm.form['EXPIRED_ON'] = $filter('date')(moment().subtract(1, 'days')._d, config.appDateFormat);

            angular.forEach(vm.data, function (val, key) {
                if (angular.isDefined(val.HR_COMPANY_ID)) {
                    datas.push({ HR_COMPANY_ID: val.HR_COMPANY_ID, HR_EMPLOYEE_ID: key, RF_CALENDAR_DAY_ID: val.RF_CALENDAR_DAY_ID, HR_EMP_OFF_DAY_ID: val.HR_EMP_OFF_DAY_ID, EFFECTIVE_FROM: vm.form.EFFECTIVE_FROM, EXPIRED_ON: vm.form.EXPIRED_ON });
                }                 
            });

            HrService.submitDataOffdayRoaster(datas, 1000).then(function (res) {
                config.appToastMsg(res.vMsg);
            });
        }

        vm.gridOptions = {
            autoBind: true,
            dataSource: $stateParams.dataSource,
            height: '400px',
            scrollable: true,
            navigatable: true,
            sortable: {
                mode: "single",
                allowUnsort: false
            },
            pageable:true,
            //pageable: {
            //    refresh: true,
            //    pageSizes: true,
            //    buttonCount: 5
            //},
            columns: [
                { field: "EMPLOYEE_CODE", title: "Employee Code", type: "number", width: "50px" },
                { field: "EMP_FULL_NAME_EN", title: "Employee Name", type: "string", width: "60px" },
                { field: "DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "60px" },

                {
                    title: "Sat",
                    template: function () {
                        return "<input type='radio' ng-model='vm.data[dataItem.HR_EMPLOYEE_ID].RF_CALENDAR_DAY_ID' ng-value='1'><input type='hidden' ng-init='vm.data[dataItem.HR_EMPLOYEE_ID].HR_COMPANY_ID=vm.HR_COMPANY_ID' ng-model='vm.data[dataItem.HR_EMPLOYEE_ID].HR_COMPANY_ID'>";
                    },
                    width: "15px"
                },
                {
                    title: "Sun",
                    template: function () {
                        return "<input type='radio' ng-model='vm.data[dataItem.HR_EMPLOYEE_ID].RF_CALENDAR_DAY_ID' ng-value='2'><input type='hidden' ng-init='vm.data[dataItem.HR_EMPLOYEE_ID].HR_EMP_OFF_DAY_ID=dataItem.HR_EMP_OFF_DAY_ID' ng-model='vm.data[dataItem.HR_EMPLOYEE_ID].HR_EMP_OFF_DAY_ID'>";
                    },
                    width: "15px"
                },
                {
                    title: "Mon",
                    template: function () {
                        return "<input type='radio' ng-model='vm.data[dataItem.HR_EMPLOYEE_ID].RF_CALENDAR_DAY_ID' ng-value='3'>";
                    },
                    width: "15px"
                },

                {
                    title: "Twe",
                    template: function () {
                        return "<input type='radio' ng-model='vm.data[dataItem.HR_EMPLOYEE_ID].RF_CALENDAR_DAY_ID' ng-value='4'>";
                    },
                    width: "15px"
                },
                {
                    title: "Wed",
                    template: function () {
                        return "<input type='radio' ng-model='vm.data[dataItem.HR_EMPLOYEE_ID].RF_CALENDAR_DAY_ID' ng-value='5'>";
                    },
                    width: "15px"
                },
                {
                    title: "Thu",
                    template: function () {
                        return "<input type='radio' ng-model='vm.data[dataItem.HR_EMPLOYEE_ID].RF_CALENDAR_DAY_ID' ng-value='6'>";
                    },
                    width: "15px"
                },
                {
                    title: "Fri",
                    template: function () {
                        return "<input type='radio' ng-model='vm.data[dataItem.HR_EMPLOYEE_ID].RF_CALENDAR_DAY_ID' ng-value='7'>";
                    },
                    width: "15px"
                },

                {
                    field: "EFFECTIVE_FROM",
                    filterable: {
                        cell: {
                            enabled: false
                        }
                    },
                    title: "Effective Date", type: "date", format: "{0:" + vm.dtFormat + "}", width: "40px"
                }
            ]
        };

    }

})();