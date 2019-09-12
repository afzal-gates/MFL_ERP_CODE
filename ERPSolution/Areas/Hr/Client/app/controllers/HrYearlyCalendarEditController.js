(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrYearlyCalendarEditController', ['logger', 'config', '$q', '$scope', '$http', '$state', '$stateParams', 'HrService', '$rootScope', HrYearlyCalendarEditController]);

    function HrYearlyCalendarEditController(logger, config, $q, $scope, $http, $state, $stateParams, HrService, $rootScope) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.showSplash = true;
        vm.showCalendar = true;
                
        vm.form = {};

        function activate() {
            var promise = [getCompanyData(), getFiscalYearData()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }
                       
        function getCompanyData() {
            return HrService.getCompanyData().then(function (res) {
                vm.companyData = res;
                if ($stateParams.pHR_COMPANY_ID == null) {
                    vm.form['HR_COMPANY_ID'] = res[0]['HR_COMPANY_ID'];
                }
            });
        }

        function getFiscalYearData() {
            return HrService.getFiscalYearData().then(function (res) {
                vm.FiscalYearData = res;

                if ($stateParams.pRF_FISCAL_YEAR_ID == null) {
                    vm.form['RF_FISCAL_YEAR_ID'] = res[0]['RF_FISCAL_YEAR_ID'];
                }
            });
        }

        //console.log($stateParams.pHR_COMPANY_ID);
        vm.form.HR_COMPANY_ID = $stateParams.pHR_COMPANY_ID;
        vm.form.RF_FISCAL_YEAR_ID = $stateParams.pRF_FISCAL_YEAR_ID;

        //////////////////////////////////////////////////////                    
        
        vm.YearlyCalendarView = function () {
            $state.go('YearlyCalendarView', { pHR_COMPANY_ID: vm.form.HR_COMPANY_ID, pRF_FISCAL_YEAR_ID: vm.form.RF_FISCAL_YEAR_ID });
        };
        /////////////////////////////////////////////////////                         
        

        vm.today = new Date();

        vm.fromDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            
            vm.fromDateOpened = true;
        };

        vm.toDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            
            vm.toDateOpened = true;
        };
        vm.toDateMin = function (item) {
            console.log(item);
        };

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        //$scope.formats = ['dd-MMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        //$scope.format = $scope.formats[0];
        

        //vm.directive('pwCheck', function () {
        //    return {
        //        require: 'ngModel',
        //        link: function (scope, elem, attrs, ctrl) {
        //            scope.$watch(attrs.pwCheck, function (confirmPassword) {
        //                var isValid = ctrl.$viewValue === confirmPassword;
        //                ctrl.$setValidity('pwmatch', isValid);
        //            });
        //        }
        //    }
        //});


        vm.UpdateDateRange = function (isValid, form) {
            //console.log(form);
            //alert('t');
            //if (!isValid) return;
            //alert('ok');

            $http({
                headers: { "RequestVerificationToken": vm.antiForgeryToken },
                url: '/Hr/HrYearlyCalander/UpdateDateRange',
                method: 'post',
                data: form
            }).then(function (data, status, headers, config1) {
                
                vm.errors = undefined;
                
                if (data.data.success === false) {
                    vm.errors = data.data.errors;
                }
                else {
                    config.appToastMsg(data.data.vMsg);
                    vm.insert = true;
                }

            }).catch(function (message) {
                //console.log(message);
                exception.catcher('XHR loading Failded')(message);
            });
            
            return;
        };


        vm.holidayTypeList = {
            optionLabel: "Select",
            //autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/HrYearlyCalander/DayTypeListData",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "DAY_TYPE_NAME_EN",
            dataValueField: "HR_DAY_TYPE_ID",
            select: function onSelect(e) {
                //alert('Test');
                var dataItem = this.dataItem(e.item);
                
                //if (vm.form.IS_GOVT_HOLIDAY == 'Y' || dataItem.HR_DAY_TYPE_ID == 3) {
                //    vm.enable = true;
                //} else {
                //    vm.enable = false;
                //};

            }
        };


        vm.govtHolidayClick = function() {
            if (vm.form.IS_GOVT_HOLIDAY == 'Y' || vm.form.HR_DAY_TYPE_ID==3) {
                vm.enable = true;
            } else {
                vm.enable = false;
            };
        }

        vm.commonHolidayList = {
            optionLabel: "Select",
            //autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/HrYearlyCalander/HolidayListData",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "HOLIDAY_NAME_EN",
            dataValueField: "HR_HOLIDAY_ID",
        };


        //$scope.monthPickerConfig = {
        //    start: "year",
        //    depth: "year",
        //    format: "MMMM yyyy"
        //};
        $scope.datePickerConfig = {
            //start: "year",
            //depth: "year",
            format: vm.dtFormat
        };

        //vm.directive('pwCheck', function () {
        //    return {
        //        require: 'ngModel',
        //        link: function (scope, elem, attrs, ctrl) {
        //            scope.$watch(attrs.pwCheck, function (confirmPassword) {
        //                var isValid = ctrl.$viewValue === confirmPassword;
        //                ctrl.$setValidity('pwmatch', isValid);
        //            });
        //        }
        //    }
        //});


        $scope.validate = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                $scope.validationMessage = "Hooray! Your tickets has been booked!";
                $scope.validationClass = "valid";
            } else {
                $scope.validationMessage = "Oops! There is invalid data in the form.";
                $scope.validationClass = "invalid";
            }
        }

        //////////// End For View Form /////////////////////////////////////////////////////////////////////////////////






        //vm.tabs = [
        //{ title: 'Entry', content: 'Dynamic content 1' },
        //{ title: 'List', content: 'Dynamic content 2' }
        ////{ title: 'Dynamic Title 3', content: 'Dynamic content 3' }
        //];


        

        //function getData(){
        //     return AdminService.getData().then(function (data) {
        //         //vm.schedulerOptions.dataSource.data = data;
        //         //return vm.schedulerOptions.dataSource.data;
           
        //    });
        //}

 
        

            
        
    }

    

})();