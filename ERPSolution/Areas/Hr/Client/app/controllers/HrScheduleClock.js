(function () {
    'use strict',

    angular.module('multitex.hr').controller('HrScheduleClock', ['config', '$stateParams','$http','$scope','$state',HrScheduleClock]);

    function HrScheduleClock(config, $stateParams, $http, $scope, $state) {
        var vm = this;

        //console.log($stateParams);
        //$http.get('/Hr/HrSchedule/ScheduleClockData', { pHrScheduleD01Id: $stateParams.HrScheduleD_Id }).then(function (res) {
        //    vm.clockData = res.data;
        //});

        vm.resetGroup = function () {
            $stateParams.HrScheduleD_Id = 0;
            //console.log(vm.dayData);
            var data = [];
            angular.forEach(vm.dayData, function (val, key) {
                
                val['HR_SCHEDULE_D011_ID'] = 0;
                val['IS_ACTIVE'] = "N";

                data.push(val);
            });
            vm.dayData = data;

            var data = [];
            angular.forEach(vm.clockData, function (val, key) {
                
                val['HR_SCHEDULE_D012_ID'] = 0;

                val['CLK_START_FROM'] = null;
                val['CLK_START_ACT'] = null;
                val['CLK_START_TO'] = null;
                val['CLK_START_GRACE'] = null;
                val['CLK_END_FROM'] = null;
                val['CLK_END_ACT'] = null;
                val['CLK_END_TO'] = null;
                val['CLK_END_GRACE'] = null;

                data.push(val);
            });
            vm.clockData = data;

            vm.weekData.HR_SCHEDULE_D01_ID = 0;
            vm.weekData.WK_GRP_NAME_EN = '';
            //console.log($stateParams);
        };



        $http({
            method: 'post',
            url: '/Hr/HrSchedule/ScheduleClockData',
            params: { scheduleD01Id: $stateParams.HrScheduleD_Id }
        }).then(function (res) {

            vm.clockData = [];
            var data = [];

            angular.forEach(res.data, function (val, key) {

                if (val.IS_ACTIVE == 'Y') {
                    if (val.CLK_START_FROM) {
                        var dtStartForm = val.CLK_START_FROM.replace(/\/Date\((-?\d+)\)\//, '$1');
                        dtStartForm = parseInt(dtStartForm);
                        val['CLK_START_FROM'] = new Date(dtStartForm);
                    }

                    if (val.CLK_START_ACT) {
                        var dtStartAct = val.CLK_START_ACT.replace(/\/Date\((-?\d+)\)\//, '$1');
                        dtStartAct = parseInt(dtStartAct);
                        val['CLK_START_ACT'] = new Date(dtStartAct);
                    }

                    if (val.CLK_START_TO) {
                        var dtStartTo = val.CLK_START_TO.replace(/\/Date\((-?\d+)\)\//, '$1');
                        dtStartTo = parseInt(dtStartTo);
                        val['CLK_START_TO'] = new Date(dtStartTo);
                    }

                    if (val.CLK_START_GRACE) {
                        var dtStartGrace = val.CLK_START_GRACE.replace(/\/Date\((-?\d+)\)\//, '$1');
                        dtStartGrace = parseInt(dtStartGrace);
                        val['CLK_START_GRACE'] = new Date(dtStartGrace);
                    }


                    if (val.CLK_END_FROM) {
                        var dtEndForm = val.CLK_END_FROM.replace(/\/Date\((-?\d+)\)\//, '$1');
                        dtEndForm = parseInt(dtEndForm);
                        val['CLK_END_FROM'] = new Date(dtEndForm);
                    }

                    if (val.CLK_END_ACT) {
                        var dtEndAct = val.CLK_END_ACT.replace(/\/Date\((-?\d+)\)\//, '$1');
                        dtEndAct = parseInt(dtEndAct);
                        val['CLK_END_ACT'] = new Date(dtEndAct);
                    }

                    if (val.CLK_END_TO) {
                        var dtEndTo = val.CLK_END_TO.replace(/\/Date\((-?\d+)\)\//, '$1');
                        dtEndTo = parseInt(dtEndTo);
                        val['CLK_END_TO'] = new Date(dtEndTo);
                    }

                    if (val.CLK_END_GRACE) {
                        var dtEndGrace = val.CLK_END_GRACE.replace(/\/Date\((-?\d+)\)\//, '$1');
                        dtEndGrace = parseInt(dtEndGrace);
                        val['CLK_END_GRACE'] = new Date(dtEndGrace);
                    }

                    data.push(val);
   
                } else {
                    data.push(val);
                }
                
            });
            //console.log(data);
            vm.clockData = data;         
        });

        $http({
            method:'post',
            url: '/Hr/HrSchedule/ScheduleDayData',
            params: { scheduleId: $stateParams.HrScheduleId, scheduleD01Id: $stateParams.HrScheduleD_Id }
        }).then(function (res) {
            vm.dayData = res.data;
        });

        $http({
            method: 'post',
            url: '/Hr/HrSchedule/WeekGroupData',
            params: { pHrScheduleD01Id: $stateParams.HrScheduleD_Id }
        }).then(function (res) {
            vm.weekData = res.data;
        });

       
        vm.cancel = function () {
            vm.insert = true;
            vm.form = { IS_ACTIVE: 'Y' };

            vm.errors = {};
            return $state.go('ScheduleEntry', { pNewSchedule:true });
        };

        vm.submit = function (isValid) {
            //console.log(vm.form);
            //console.log(vm.dayData);
            //console.log(vm.weekData);

            //alert(isValid);

            //if (!isValid) return;

            $http({
                headers: { "RequestVerificationToken": vm.antiForgeryToken },
                url: '/Hr/HrSchedule/DataBatchSave',
                method: 'post',
                data: { obWeek: vm.weekData, obDay: vm.dayData, obClock: vm.clockData }                
            }).success(function (data, status, headers, config1) {                    
                vm.errors = [];
                if (data.success === false) {                        
                    vm.errors = data.errors;
                }
                else {                    
                    vm.clockData = [];
                    var data1 = [];

                    angular.forEach(data.objReturn.scheduleClockData, function (val, key) {

                        if (val.IS_ACTIVE == 'Y') {
                            if (val.CLK_START_FROM) {
                                var dtStartForm = val.CLK_START_FROM.replace(/\/Date\((-?\d+)\)\//, '$1');
                                dtStartForm = parseInt(dtStartForm);
                                val['CLK_START_FROM'] = new Date(dtStartForm);
                            }

                            if (val.CLK_START_ACT) {
                                var dtStartAct = val.CLK_START_ACT.replace(/\/Date\((-?\d+)\)\//, '$1');
                                dtStartAct = parseInt(dtStartAct);
                                val['CLK_START_ACT'] = new Date(dtStartAct);
                            }

                            if (val.CLK_START_TO) {
                                var dtStartTo = val.CLK_START_TO.replace(/\/Date\((-?\d+)\)\//, '$1');
                                dtStartTo = parseInt(dtStartTo);
                                val['CLK_START_TO'] = new Date(dtStartTo);
                            }

                            if (val.CLK_START_GRACE) {
                                var dtStartGrace = val.CLK_START_GRACE.replace(/\/Date\((-?\d+)\)\//, '$1');
                                dtStartGrace = parseInt(dtStartGrace);
                                val['CLK_START_GRACE'] = new Date(dtStartGrace);
                            }


                            if (val.CLK_END_FROM) {
                                var dtEndForm = val.CLK_END_FROM.replace(/\/Date\((-?\d+)\)\//, '$1');
                                dtEndForm = parseInt(dtEndForm);
                                val['CLK_END_FROM'] = new Date(dtEndForm);
                            }

                            if (val.CLK_END_ACT) {
                                var dtEndAct = val.CLK_END_ACT.replace(/\/Date\((-?\d+)\)\//, '$1');
                                dtEndAct = parseInt(dtEndAct);
                                val['CLK_END_ACT'] = new Date(dtEndAct);
                            }

                            if (val.CLK_END_TO) {
                                var dtEndTo = val.CLK_END_TO.replace(/\/Date\((-?\d+)\)\//, '$1');
                                dtEndTo = parseInt(dtEndTo);
                                val['CLK_END_TO'] = new Date(dtEndTo);
                            }

                            if (val.CLK_END_GRACE) {
                                var dtEndGrace = val.CLK_END_GRACE.replace(/\/Date\((-?\d+)\)\//, '$1');
                                dtEndGrace = parseInt(dtEndGrace);
                                val['CLK_END_GRACE'] = new Date(dtEndGrace);
                            }

                            data1.push(val);

                        } else {
                            data1.push(val);
                        }

                    });                    
                    vm.clockData = data1;
                    
                    config.appToastMsg(data.objReturn.msg);                
                    //console.log(vm.clockData);
                }                    
            }).
            error(function (data, status, headers, config) {
                alert('something went wrong')
                console.log(status);
            });

        }


    }

})();