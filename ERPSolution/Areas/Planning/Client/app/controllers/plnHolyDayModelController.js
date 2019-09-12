(function () {
    'use strict';
    angular.module('multitex.planning').controller('PlnHolyDayModelController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$timeout', '$http', '$modal', PlnHolyDayModelController]);
    function PlnHolyDayModelController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $timeout, $http, $modal) {
        var vm = this;
        var dp;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.curMon = new Date();
        activate();
        vm.showSplash = true;
        vm.form = {};

        function activate() {
            var promise = [getCompanyList(), getOfficeList(), getSectionList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;                
            });
        }


        function getCompanyList() {

            vm.compOptions = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                dataSource: {
                    transport: {
                        read: function (e) {
                            PlanningDataService.getDataByFullUrl('/api/common/CompanyList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                change: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.form['COMP_NAME_EN'] = dataItem.COMP_NAME_EN;
                    return vm.officeDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                PlanningDataService.getDataByFullUrl('/api/common/GetOfficeList?pHR_COMPANY_ID=' + dataItem.HR_COMPANY_ID+'&pOption=3004').then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    });
                }
            }
        };

        function getOfficeList() {
            vm.officeOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "OFFICE_NAME_EN",
                dataValueField: "HR_OFFICE_ID",
                change: function (e) {
                    var ditm = this.dataItem(e.item);

                    vm.form['OFFICE_NAME_EN'] = ditm.OFFICE_NAME_EN;
                    return vm.sectionDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                PlanningDataService.getDataByFullUrl('/Hr/OffDayRoaster/getDeptByCompanyOffice?pHR_COMPANY_ID=' + vm.form.HR_COMPANY_ID + '&pHR_OFFICE_ID=' + ditm.HR_OFFICE_ID).then(function (res) {
                                    e.success(res);
                                });
                            }
                        }
                    });
                }
            }

            return vm.officeDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByFullUrl('/api/common/GetOfficeList?pHR_COMPANY_ID&pOption=3004').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });

        };

        function getSectionList() {
            return vm.sectionOption = {
                optionLabel: "-- Select Dept --",
                filter: "contains",
                autoBind: true,
                dataTextField: "DEPARTMENT_NAME_EN",
                dataValueField: "HR_DEPARTMENT_ID",
                change: function (e) {
                    var di = this.dataItem(e.item);
                    vm.form['DEPARTMENT_NAME_EN'] = di.DEPARTMENT_NAME_EN;
                }
            };
        };

       function openDModal (data) {
           var modalInstance = $modal.open({
               animation: true,
               templateUrl: '/Planning/Pln/_PlnHolyDayModal',
               controller: 'PlnHolyDayModalController',
               size: 'md',
               windowClass: 'large-Modal',
               resolve: {
                   params: function () {
                       return data;
                   }
               }
           });

           modalInstance.result.then(function (data) {
               getEvent(data, dp.visibleStart().value, dp.visibleEnd().value, true);
           }, function () {
               console.log('Modal dismissed at: ' + new Date());
           });
       }


       $scope.config = {
           weekStarts: 6,
           startDate: new DayPilot.Date(),
           onBeforeCellRender : function(args) {
               if (args.cell.start.getTime() === new DayPilot.Date().getDatePart().getTime()) {
                   args.cell.backColor = "#ac0";
               }
           },
           eventMoveHandling: 'disabled',
           eventResizeHandling :'disabled',
           //onEventMoved: function (args) {

           //    var data = {
           //        HR_COMPANY_ID: args.e.data.HR_COMPANY_ID,
           //        HR_OFFICE_ID: args.e.data.HR_OFFICE_ID,
           //        CORE_DEPT_ID: args.e.data.CORE_DEPT_ID,

           //        OFFICE_NAME_EN: vm.form.OFFICE_NAME_EN,
           //        COMP_NAME_EN: vm.form.COMP_NAME_EN,
           //        DEPARTMENT_NAME_EN: vm.form.DEPARTMENT_NAME_EN,
           //        HR_DAY_TYPE_ID: args.e.data.HR_DAY_TYPE_ID,
           //        START_DT: args.newStart.value,
           //        END_DT: args.newEnd.value
           //    };

           //    openDModal(data);
           //},
           //onEventResized : function (args) {
           //    var data = {
           //        HR_COMPANY_ID: args.e.data.HR_COMPANY_ID,
           //        HR_OFFICE_ID: args.e.data.HR_OFFICE_ID,
           //        CORE_DEPT_ID: args.e.data.CORE_DEPT_ID,

           //        OFFICE_NAME_EN: vm.form.OFFICE_NAME_EN,
           //        COMP_NAME_EN: vm.form.COMP_NAME_EN,
           //        DEPARTMENT_NAME_EN: vm.form.DEPARTMENT_NAME_EN,
           //        HR_DAY_TYPE_ID: args.e.data.HR_DAY_TYPE_ID,
           //        START_DT: args.newStart.value,
           //        END_DT: args.newEnd.value
           //    };
           //    openDModal(data);

           //},
           onTimeRangeSelected : function (args) {
               if (!vm.form.HR_COMPANY_ID || !vm.form.HR_OFFICE_ID || !vm.form.CORE_DEPT_ID) {
                   return;
               }

               var data = {
                   HR_COMPANY_ID : vm.form.HR_COMPANY_ID, 
                   HR_OFFICE_ID : vm.form.HR_OFFICE_ID , 
                   CORE_DEPT_ID: vm.form.CORE_DEPT_ID,

                   OFFICE_NAME_EN : vm.form.OFFICE_NAME_EN, 
                   COMP_NAME_EN : vm.form.COMP_NAME_EN, 
                   DEPARTMENT_NAME_EN: vm.form.DEPARTMENT_NAME_EN,
                   HR_DAY_TYPE_ID : '',
                   START_DT: args.start.value,
                   END_DT: DayPilot.Date(args.end.value).addDays(-1).toStringSortable()
               };
               openDModal(data);
           },
           contextMenu : new DayPilot.Menu({
               cssClassPrefix: "menu_default",
               items: [
               {
                   text: "Edit", onclick: function () {


                       var data = {
                           HR_COMPANY_ID: this.source.data.HR_COMPANY_ID,
                           HR_OFFICE_ID: this.source.data.HR_OFFICE_ID,
                           CORE_DEPT_ID: this.source.data.CORE_DEPT_ID,

                           OFFICE_NAME_EN: vm.form.OFFICE_NAME_EN,
                           COMP_NAME_EN: vm.form.COMP_NAME_EN,
                           DEPARTMENT_NAME_EN: vm.form.DEPARTMENT_NAME_EN,
                           HR_DAY_TYPE_ID: this.source.data.HR_DAY_TYPE_ID,
                           START_DT: this.source.data.start,
                           END_DT: this.source.data.end
                       };
                       openDModal(data);
                       
                   }
               }
               ]})    
       };

       function curMonth() {
           vm.curMon = new Date(dp.startDate);
       };

       function getEventMD(form, pSTART_DATE, pEND_DATE) {
           var url1 = '/PlanCommon/getHolydayDataMD?pHR_COMPANY_ID=' + form.HR_COMPANY_ID + '&pHR_OFFICE_ID=' + form.HR_OFFICE_ID + '&pSTART_DT=' + pSTART_DATE + '&pEND_DT=' + pEND_DATE;
           return PlanningDataService.getDataByUrl(url1).then(function (res) {
               vm.datas = res;
           });
       };


       function getEvent(form, pSTART_DATE, pEND_DATE, is_md) {
           if (is_md) {
               getEventMD(form, pSTART_DATE, pEND_DATE);
           }
           var url= '/PlanCommon/getHolydayData?pHR_COMPANY_ID='+form.HR_COMPANY_ID+'&pHR_OFFICE_ID='+form.HR_OFFICE_ID+'&pCORE_DEPT_ID='+form.CORE_DEPT_ID+'&pSTART_DT='+pSTART_DATE+'&pEND_DT='+pEND_DATE;
           return PlanningDataService.getDataByUrl(url).then(function (res) {
               $scope.events = res;
           });
       };

       vm.onClickLeft = function (dd) {
           vm.form['CORE_DEPT_ID'] = dd.CORE_DEPT_ID;
           vm.form['DEPARTMENT_NAME_EN'] = dd.DEPT_NAME;
           setTimeout(function () {
               getEvent(vm.form, dp.visibleStart().value, dp.visibleEnd().value,false);
           },300);

       };

       vm.navigate = function (val, form) {

           if (!form.HR_COMPANY_ID || !form.HR_OFFICE_ID || !form.CORE_DEPT_ID) {
               return;
           }

           dp.startDate = new DayPilot.Date(dp.startDate).addMonths(val);
           dp.update();
           curMonth();
           getEvent(form,dp.visibleStart().value,dp.visibleEnd().value,true);

       };


       vm.loadData = function (data) {
          return getEvent(data, dp.visibleStart().value,dp.visibleEnd().value,true);
       };


       $timeout(function () {
           dp = $scope.dp;
       });

    }

})();

(function () {
    'use strict';
    angular.module('multitex.planning').controller('PlnHolyDayModalController', ['$q', 'config', 'PlanningDataService', '$scope', '$modalInstance', 'params', PlnHolyDayModalController]);
    function PlnHolyDayModalController($q, config, PlanningDataService, $scope, $modalInstance, params) {
        $scope.params = params;

        $scope.showSplash = false;

        $scope.dtFormat = config.appDateFormat;
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.today = new Date();

        $scope.fromDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.fromDateOpened = true;
        };

        $scope.toDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.toDateOpened = true;
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.holidayTypeList = {
            optionLabel: "Select",
            dataSource: {
                transport: {
                    read: {
                        url: "/Hr/HrYearlyCalander/DayTypeListData",
                        dataType: "json"
                    }
                }
            },
            dataTextField: "DAY_TYPE_NAME_EN",
            dataValueField: "HR_DAY_TYPE_ID"
        };


        $scope.submitData = function (data) {
            $scope.showSplash = true;
            PlanningDataService.saveDataByUrl({
                HR_COMPANY_ID: data.HR_COMPANY_ID,
                HR_OFFICE_ID: data.HR_OFFICE_ID,
                CORE_DEPT_ID: data.CORE_DEPT_ID,
                HR_DAY_TYPE_ID: data.HR_DAY_TYPE_ID,
                start: data.START_DT,
                end: data.END_DT,
                IS_SAME_FOF_OTH: (data.IS_SAME_FOF_OTH||'N')

            }, '/PlanCommon/SaveHolyDayModelData').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $scope.showSplash = false;
                        $modalInstance.close(data);
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });

        };



    }

})();
