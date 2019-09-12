(function () {
    'use strict';

    angular.module('multitex.hr').controller('HrOtEntitledController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'HrService', HrOtEntitledController]);

    function HrOtEntitledController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, HrService) {

        var vm = this;
        activate();
        vm.title = config.appTitle;
        vm.dtFormat = config.appDateFormat;
        vm.timeFormat = config.appTimeFormat;
        vm.showSplash = true;
        
        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
            function (e) {
                e.preventDefault();
            });

        vm.insert = true;
        vm.today = new Date();
        
        //var currMonth=moment()
        var lastDayOfMonth = moment().daysInMonth();

        var firstDate = moment().date(1);
        var lastDate = moment().date(lastDayOfMonth);


        vm.form = {};
        var dt = firstDate._d;
        //vm.form.FROM_DT = $filter('date')(dt, vm.dtFormat);
        dt = lastDate._d;
        //vm.form.TO_DT = $filter('date')(dt, vm.dtFormat);                             
           
        //vm.form.OT_APRV_DATE = $filter('date')(vm.today, vm.dtFormat);
        //vm.form.OT_DATE = $filter('date')(vm.today, vm.dtFormat);
        //vm.form.OT_DATE = "";

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.approveDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.approveDateOpened = true;
        };

        vm.otDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.otDateOpened = true;
        };

        vm.isNext = false;
        
        vm.reset = function () {            
            vm.insert = true;
        };

        $scope.items = [{}];
        $scope.selectedItems = [{}];
        vm.next = function () {
            HrService.getNonEntitledDesigListData().then(function (res) {
                //vm.nonEntitledDesigListData = res;
                $scope.items = res;
                HrService.getEntitledDesigListData(vm.form.HR_OT_TEAM_ID).then(function (res1) {
                    //vm.form.HR_DESIGNATION_ID_LIST = res;
                    $scope.selectedItems = res1;
                    $scope.demoOptions = {
                        title: '',
                        filterPlaceHolder: 'Start typing to filter the lists below.',
                        labelAll: 'Designation not yet assigned',
                        labelSelected: 'Designation assigned',
                        helpMessage: 'Click items to transfer them between fields.',
                        /* angular will use this to filter your lists */
                        orderProperty: 'DESIG_ORDER',
                        /* this contains the initial list of all items (i.e. the left side) */
                        items: res,
                        /* this list should be initialized as empty or with any pre-selected items */
                        selectedItems: res1
                    };

                });
            });
            vm.isNext = true;            
        };                       

        $scope.$watch('vm.form.HR_OT_TEAM_ID', function (newVal, oldVal) {
            if (newVal != null && (newVal != oldVal)) {                
                vm.isNext = false;
            }
        });

        function getOtTeamListData() {            
            return vm.otTeamListData = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            HrService.getOtTeamListData().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "OT_TEAM_NAME_EN",
                dataValueField: "HR_OT_TEAM_ID"
            };
        };
           
        
        vm.submit = function (isValid, form, insert) {
            
            //if (!isValid) return;

            //alert('ok');     
            //console.log($scope.selectedItems);
            
            if (insert) {
                
                $http({
                    headers: { "RequestVerificationToken": vm.antiForgeryToken },
                    url: '/Hr/HrOtEntitled/BatchSave',
                    method: 'post',
                    data: {ob:form, ob1: $scope.selectedItems}
                }).success(function (data, status, headers, config1) {
                    vm.errors = [];
                    if (data.success === false) {
                        vm.errors = data.errors;
                    }
                    else {
                        if (data.vMsg.substr(0, 9) == 'MULTI-001') {
                            vm.reset();                            
                        };
                        config.appToastMsg(data.vMsg);

                    }
                }).error(function (data, status, headers, config) {
                    alert('something went wrong')
                    console.log(status);
                });
            }
            //else {
            //    $http({
            //        headers: { "RequestVerificationToken": vm.antiForgeryToken },
            //        url: '/Hr/HrOtApprove/Update',
            //        method: 'post',
            //        data: form
            //    }).success(function (data, status, headers, config1) {
            //        vm.errors = [];
            //        if (data.success === false) {
            //            vm.errors = data.errors;
            //        }
            //        else {
            //            if (data.vMsg.substr(0, 9) == 'MULTI-001') {
            //                vm.reset();
            //            };
            //            config.appToastMsg(data.vMsg);

            //        }
            //    }).error(function (data, status, headers, config) {
            //        alert('something went wrong')
            //        console.log(status);
            //    });
            //}

        };

                
        function activate(){
            var promise = [getOtTeamListData()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

       
            
        
    }

    

})();

