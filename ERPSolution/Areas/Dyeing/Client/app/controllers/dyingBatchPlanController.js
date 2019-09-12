/// <reference path="dyingBatchPlanController.js" />
(function () {
    'use strict';
    angular.module('multitex.dyeing').controller('DyingBatchPlanController', ['$q', 'config', 'DyeingDataService', '$stateParams', '$state', '$scope', '$timeout', '$modal', 'ScheduleData', 'Dialog', '$window', 'McList', DyingBatchPlanController]);
    function DyingBatchPlanController($q, config, DyeingDataService, $stateParams, $state, $scope, $timeout, $modal, ScheduleData, Dialog, $window, McList) {
        
        var vm = this;
        var dp;
        var floor;
        var switchShift = true;
        vm.EditPlanData = {

        };
        var LcScheduleData = ScheduleData;
        $scope.scale = 60;

        $scope.DYE_MC_TYPE_ID = null;
        $scope.IS_SMP_BLK = 'B';
        

        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getLastScPlanDt()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.companyDs = new kendo.data.DataSource({
            data: McList
        });

        vm.onCompanyDataBound = function (e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].HR_COMPANY_ID);
                $scope.HR_COMPANY_ID = ds[0].HR_COMPANY_ID;
            } else if(ds.length>1) {
                e.sender.value(ds[0].HR_COMPANY_ID);
                $scope.HR_COMPANY_ID = ds[0].HR_COMPANY_ID;
            } else {
                $scope.HR_COMPANY_ID = -1;
            }
        };



        vm.onChange1 = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.HR_COMPANY_ID) {
                loadResources(item.id, $scope.IS_SMP_BLK, LcScheduleData.DYE_BATCH_SCDL_ID, item.HR_COMPANY_ID);
            } else {
                 loadResources(null, $scope.IS_SMP_BLK, LcScheduleData.DYE_BATCH_SCDL_ID,null);
            }
        }

        vm.onChange2 = function (e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.id) {
                loadResources($scope.DYE_MC_TYPE_ID, item.id, LcScheduleData.DYE_BATCH_SCDL_ID,$scope.HR_COMPANY_ID);
            } else {
                loadResources($scope.DYE_MC_TYPE_ID, null, LcScheduleData.DYE_BATCH_SCDL_ID, $scope.HR_COMPANY_ID);
            }
        }

        vm.move = function (p) {
            if (p < 0) {
                $scope.schedulerConfig.scrollTo = LcScheduleData.START_DT;
                $scope.schedulerConfig.scrollToAnimated = "fast";
            } else {
                $scope.schedulerConfig.scrollTo = $scope.scheduler.visibleEnd();
                $scope.schedulerConfig.scrollToAnimated = "fast";
            }
        }

        function getLastScPlanDt() {
            return DyeingDataService.getDataByUrl('/DyeBatchPlan/getLastScPlanDt?pIS_SMP_BLK=B').then(function (res) {
                vm.PRE_BC_END_DT = angular.fromJson(res).OP_PRE_BC_END_DT;
            });
        }

        vm.Ds2 = new kendo.data.DataSource({
            data: [
              {
                  id: 10,
                  text:'10Min'
              },
              {
                    id: 15,
                    text: '15Min'
              },

            {
                id: 30,
                text: '30Min'
            },

            {
                id: 60,
                text: '1Hr'
            }
            ]
        });

        vm.Ds3 = new kendo.data.DataSource({
            data: [
              {
                  id: 'S',
                  text: 'Sample'
              },
              {
                  id: 'B',
                  text: 'Bulk'
              }
            ]
        });


        function getMachineType() {
            return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDataForDropDown?pOption=3000').then(function (res) {
                vm.Ds1 = new kendo.data.DataSource({
                    data: res
                });
            });
        };






        vm.DYE_BATCH_SCDL_ID = $stateParams.pDYE_BATCH_SCDL_ID || null;


        vm.openBatchPlanModal = function (item) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Dyeing/Dye/_DyeProgramScdlModal',
                controller: function ($scope, config, $modalInstance, $filter, DyeingDataService, formData) {
                    $scope.alerts = [];
                    $scope.closeAlert = function (index) {
                        $scope.alerts.splice(index, 1);
                    };


                    $scope.onStartChange = function (e) {
                        console.log(e.sender);
                    };

                    $scope.form = formData;


                    $scope.datePickerOptionsEnd = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
                        format: "dd-MMM-yyyy hh:mm tt",
                        change: function (e) {
                            $scope.form['DATE_DIFF'] = moment(this.value()).diff(moment($scope.form.START_DT), 'days');
                        }
                    };

                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
                        format: "dd-MMM-yyyy hh:mm tt"
                    };

                    $scope.save = function (data) {

                        if (data.IS_FINALIZED == 'Y') {

                            Dialog.confirm('Data will be lock in case of Finalized ?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                             .then(function () {
                                 data['pOption'] = 1000;
                                 data['START_DT'] = $filter('date')(data.START_DT, 'medium');
                                 data['END_DT'] = $filter('date')(data.END_DT, 'medium');
                                 return DyeingDataService.saveDataByUrl(data, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                                     if (res.success === false) {
                                     }
                                     else {
                                         res['data'] = angular.fromJson(res.jsonStr);

                                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                             $modalInstance.close({
                                                 DYE_BATCH_SCDL_ID: parseInt(res.data.OP_DYE_BATCH_SCDL_ID || 0)
                                             });
                                         }
                                         config.appToastMsg(res.data.PMSG);
                                     }
                                 });

                             });

                        } else {

                            data['pOption'] = 1000;
                            data['START_DT'] = $filter('date')(data.START_DT, 'medium');
                            data['END_DT'] = $filter('date')(data.END_DT, 'medium');
                            return DyeingDataService.saveDataByUrl(data, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                                if (res.success === false) {
                                }
                                else {
                                    res['data'] = angular.fromJson(res.jsonStr);

                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                        $modalInstance.close({
                                            DYE_BATCH_SCDL_ID: parseInt(res.data.OP_DYE_BATCH_SCDL_ID || 0)
                                        });
                                    }
                                    config.appToastMsg(res.data.PMSG);
                                }
                            });
                        }
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function () {
                        return {
                            DYE_BATCH_SCDL_ID: item.DYE_BATCH_SCDL_ID,
                            END_DT: item.END_DT,
                            IS_FINALIZED: item.IS_FINALIZED,
                            PRE_BC_END_DT: item.PRE_BC_END_DT,
                            SCDL_NOTE: item.SCDL_NOTE,
                            SCDL_REF_NO: item.SCDL_REF_NO,
                            START_DT: item.START_DT ? item.START_DT : item.PRE_BC_END_DT,
                            IS_SMP_BLK : item.IS_SMP_BLK?item.IS_SMP_BLK:'B'
                        }
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                $state.go('DyeBatchPlan', { pDYE_BATCH_SCDL_ID: dta.DYE_BATCH_SCDL_ID }, { reload: true });
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };




        function openNewProgramEntryModal(item) {
            console.log(item);
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Dyeing/Dye/_DyeBatchProgramModal',
                controller: 'DyeBatchProgramModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    formData: function () {
                        return item;
                    },
                    McDataDs: function () {
                        return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID&pIS_SMP_BLK=' + $scope.IS_SMP_BLK + '&pDYE_BATCH_SCDL_ID=' + (LcScheduleData.DYE_BATCH_SCDL_ID || $stateParams.pDYE_BATCH_SCDL_ID))
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS,true);
            }, function () {
                loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS,true);
            });
        };


        function openNewProgramEditModal(item) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'EditProgramEntry.html',
                controller: function ($scope, config, $modalInstance, DyeingDataService, $filter, formBatch) {

                    console.log("Afzal");
                    console.log(formBatch.DYE_MACHINE_ID);
                    console.log("Afzal");

                    var M_ID = angular.copy(formBatch.DYE_MACHINE_ID);


                    $scope.form = {
                        LK_DYE_MTHD_ID: formBatch.LK_DYE_MTHD_ID,
                        DYE_BATCH_PLAN_ID: formBatch.DYE_BATCH_PLAN_ID,
                        RQD_PRD_QTY: formBatch.RQD_PRD_QTY,
                        LOAD_TIME: formBatch.LOAD_TIME,
                        UN_LOAD_TIME: formBatch.UN_LOAD_TIME,
                        BYR_ACC_NAME_EN: formBatch.BYR_ACC_NAME_EN,
                        STYLE_NO: formBatch.WORK_STYLE_NO,
                        ORDER_NO_LIST: formBatch.ORDER_NO_LIST,
                        COLOR_NAME_EN: formBatch.COLOR_NAME_EN,
                        FAB_TYPE_NAME: formBatch.FAB_TYPE_NAME
                    };

                    $scope.form["DYE_MACHINE_ID"] = M_ID;
                    console.log($scope.form);

                    DyeingDataService.getDataByFullUrl('/api/common/GetDyeingMethodList').then(function (res) {
                        $scope.DyeingMthdDs = new kendo.data.DataSource({
                            data: res
                        });
                    });
                    
                    DyeingDataService.getDataByFullUrl('/api/Dye/DCIssueRequisition/GetMachineInfoByTypeID?pDYE_MC_TYPE_ID=0').then(function (res) {
                        $scope.machineListDs = new kendo.data.DataSource({
                            data: res
                        });
                    });
                                        
                    $scope.onChangeDuration = function (DURATION, LOAD_TIME) {
                        if (LOAD_TIME && DURATION) {
                            $scope.form['UN_LOAD_TIME'] = moment(LOAD_TIME).add(DURATION, 'h')._d;
                        }

                    };

                    $scope.onChangeDate = function (LOAD_TIME, UN_LOAD_TIME) {
                        $scope.form['DURATION'] = moment(new Date(UN_LOAD_TIME)).diff(moment(new Date(LOAD_TIME)), 'hours');
                    };

                    $scope.$watch('form.UN_LOAD_TIME', function (newVal, oldVal) {
                        if (newVal !== oldVal && $scope.form.LOAD_TIME) {
                            $scope.form['DURATION'] = moment(newVal).diff(moment($scope.form.LOAD_TIME), 'hours');
                        }

                    });



                    console.log($scope.form);
                    $scope.alerts = [];
                    $scope.closeAlert = function (index) {
                        $scope.alerts.splice(index, 1);
                    };


                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
                    };
                    $scope.save = function (data, isValid) {
                        if (!isValid) {
                            return;
                        }

                        var data2Save = [{
                                DYE_BATCH_PLAN_ID: data.DYE_BATCH_PLAN_ID,
                                LK_DYE_MTHD_ID: data.LK_DYE_MTHD_ID,
                                DYE_MACHINE_ID: data.DYE_MACHINE_ID,
                                RQD_PRD_QTY: data.RQD_PRD_QTY,
                                LOAD_TIME: $filter('date')(data.LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss'),
                                UN_LOAD_TIME: $filter('date')(data.UN_LOAD_TIME, 'yyyy-MM-ddTHH:mm:ss')
                            }
                        ];


                        return DyeingDataService.saveDataByUrl({
                            DYE_BATCH_SCDL_ID: item.DYE_BATCH_SCDL_ID,
                            XML: config.xmlStringShort(data2Save),
                            pOption: 1006
                        }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                            if (res.success === false) {
                            }
                            else {
                                res['data'] = angular.fromJson(res.jsonStr);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    $modalInstance.close({});
                                }
                                config.appToastMsg(res.data.PMSG);
                            }
                        });
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                size: 'lg',
                resolve: {
                    formBatch: function () {
                        var b_item = angular.copy(item);
                        return b_item;
                    }
                }
            });

            modalInstance.result.then(function (dta) {
                loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS, true);
            }, function () {
                loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS, true);
            });
        };

        function loadResources(pDYE_MC_TYPE_ID, pIS_SMP_BLK, pDYE_BATCH_SCDL_ID, pHR_COMPANY_ID) {
            DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID=' + (pDYE_MC_TYPE_ID || null) + '&pIS_SMP_BLK=' + (pIS_SMP_BLK || null) + '&pDYE_BATCH_SCDL_ID=' + (pDYE_BATCH_SCDL_ID || LcScheduleData.DYE_BATCH_SCDL_ID) + '&pHR_COMPANY_ID=' + (pHR_COMPANY_ID||null)).then(function (res) {
                $scope.schedulerConfig.resources = res.concat([{
                    D_PRD_CAPACITY: '',
                    TTL_LOAD: '',
                    LOAD_TO_DO: '',
                    name: ''
                }]);
                $scope.schedulerConfig.visible = true;
            });
        }

        $scope.onSearchEvent = function (query) {
            if (query) {
                dp.events.filter(query);
            } else {
                dp.events.filter(null);
            }
        }


        $scope.clearSelection = function () {
            if (dp) {
                dp.multiselect.clear();
            }

        }


        var general = new DayPilot.Menu({
            items:
                [
                        {
                            text: "Edit", onclick: function () {

                                var data = {
                                    LK_DYE_MTHD_ID: this.source.data.LK_DYE_MTHD_ID,
                                    DYE_BATCH_PLAN_ID: this.source.data.id,
                                    RQD_PRD_QTY: this.source.data.RQD_PRD_QTY,
                                    LOAD_TIME: this.source.data.start,
                                    UN_LOAD_TIME: this.source.data.end,
                                    BYR_ACC_NAME_EN: this.source.data.BYR_ACC_NAME_EN,
                                    STYLE_NO: this.source.data.WORK_STYLE_NO,
                                    ORDER_NO_LIST: this.source.data.ORDER_NO_LIST,
                                    COLOR_NAME_EN: this.source.data.COLOR_NAME_EN,
                                    FAB_TYPE_NAME: this.source.data.FAB_TYPE_NAME,
                                    DYE_MACHINE_ID: this.source.data.resource,
                                }

                                openNewProgramEditModal(data);
                            },

                        },



                    {
                        text: "Apply 10MinGap", onclick: function () {
                            var data = [];
                            if (dp.multiselect.events().length > 0) {
                                dp.multiselect.events().forEach(function (itm) {
                                    data.push({ DYE_BATCH_PLAN_ID: itm.data.id });
                                });
                            } else {
                                data.push({ DYE_BATCH_PLAN_ID: this.source.data.id });
                            };

                            Dialog.confirm('Applying 10 Min Gap at every Dye Program? <br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                            .then(function () {
                                return DyeingDataService.saveDataByUrl({
                                    DYE_BATCH_SCDL_ID: (LcScheduleData.DYE_BATCH_SCDL_ID || 0),
                                    XML: config.xmlStringShort(data),
                                    pOption: 1005
                                }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                                    res['data'] = angular.fromJson(res.jsonStr);
                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                        loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS, true);
                                    }
                                    config.appToastMsg(res.data.PMSG);
                                });
                            });
                        },

                    },
                    {
                        text: "Make Duplicate", onclick: function () {




                            var data = [];
                            data.push({
                                DYE_MACHINE_ID: this.source.data.resource,
                                DYE_BATCH_PLAN_ID: this.source.data.id,
                                LOAD_TIME: this.source.data.start,
                                UN_LOAD_TIME: this.source.data.end

                            });


                            Dialog.confirm('Creating Duplicate of Selected? <br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                            .then(function () {
                                return DyeingDataService.saveDataByUrl({
                                    DYE_BATCH_SCDL_ID: (LcScheduleData.DYE_BATCH_SCDL_ID || 0),
                                    XML: config.xmlStringShort(data),
                                    pOption: 1004
                                }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                                    if (res.success === false) {
                                    }
                                    else {
                                        res['data'] = angular.fromJson(res.jsonStr);
                                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                            loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS, true);
                                        }
                                        config.appToastMsg(res.data.PMSG);
                                    }
                                });
                            });
                        },

                    },
                    {
                        text: "Remove Selected", onclick: function () {
                            var data = [];
                            if (dp.multiselect.events().length > 0) {
                                dp.multiselect.events().forEach(function (itm) {
                                    data.push({ DYE_BATCH_PLAN_ID: itm.data.id });
                                });
                            } else {
                                data.push({ DYE_BATCH_PLAN_ID: this.source.data.id });
                            };

                            Dialog.confirm('Deleting Selected Program...?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                            .then(function () {
                                return DyeingDataService.saveDataByUrl({
                                    DYE_BATCH_SCDL_ID: (LcScheduleData.DYE_BATCH_SCDL_ID || 0),
                                    XML: config.xmlStringShort(data),
                                    pOption: 1003
                                }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                                    res['data'] = angular.fromJson(res.jsonStr);
                                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                        //loadResources(null, null, LcScheduleData.DYE_BATCH_SCDL_ID);
                                        loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS, true);
                                    }
                                    config.appToastMsg(res.data.PMSG);
                                });
                            });
                        },

                    },
                ]
        });

        var default_menu = new DayPilot.Menu({
            items:
                [{
                    text: "Edit", onclick: function () {

                        var data = {
                            LK_DYE_MTHD_ID: this.source.data.LK_DYE_MTHD_ID,
                            DYE_BATCH_PLAN_ID: this.source.data.id,
                            RQD_PRD_QTY: this.source.data.RQD_PRD_QTY,
                            LOAD_TIME: this.source.data.start,
                            UN_LOAD_TIME: this.source.data.end,
                            BYR_ACC_NAME_EN: this.source.data.BYR_ACC_NAME_EN,
                            STYLE_NO: this.source.data.WORK_STYLE_NO,
                            ORDER_NO_LIST: this.source.data.ORDER_NO_LIST,
                            COLOR_NAME_EN: this.source.data.COLOR_NAME_EN,
                            FAB_TYPE_NAME: this.source.data.FAB_TYPE_NAME,
                            DYE_MACHINE_ID: this.source.data.resource,
                        }
                        openNewProgramEditModal(data);
                    },

                },

                    {
                    text: "Apply 10MinGap", onclick: function () {
                        var data = [];
                        if (dp.multiselect.events().length > 0) {
                            dp.multiselect.events().forEach(function (itm) {
                                data.push({ DYE_BATCH_PLAN_ID: itm.data.id });
                            });
                        } else {
                            data.push({ DYE_BATCH_PLAN_ID: this.source.data.id });
                        };

                        Dialog.confirm('Applying 10 Min Gap at every Dye Program? <br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                        .then(function () {
                            return DyeingDataService.saveDataByUrl({
                                DYE_BATCH_SCDL_ID: (LcScheduleData.DYE_BATCH_SCDL_ID || 0),
                                XML: config.xmlStringShort(data),
                                pOption: 1005
                            }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                                res['data'] = angular.fromJson(res.jsonStr);
                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS, true);
                                }
                                config.appToastMsg(res.data.PMSG);
                            });
                        });
                    },

                }]
        });

        vm.printKnitCard = function (pDYE_BATCH_SCDL_ID) {
            var url = '/Dyeing/Dye/DyeingHtmlRpt/#/DyeingBatchProgramRpt?pDYE_BATCH_SCDL_ID=' + LcScheduleData.DYE_BATCH_SCDL_ID
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);
        }



        $scope.schedulerConfig = {
            visible: false, // will be displayed after loading the resources
            autoScroll: "Drag",
            cellDuration: $scope.scale,//1440,// one day   
            timeHeaders: [{ groupBy: "Month", format: "MMMM yyyy" }, { groupBy: "Day", format: "ddd,d" }, { groupBy: "Hour", format: "h tt" }],
            cellWidth: 18,
            rowHeaderWidth: 100,

            allowEventOverlap : true,

            startDate: new DayPilot.Date(ScheduleData.START_DT).addDays(-1),
            days: 10,
            multiSelectRectangle: "Free",
            multiMoveVerticalMode: "Disabled",
            //timeline: [],
            // rowHeaderScrolling: false,
            scale: 'CellDuration',
            heightSpec: "Full",//Max
            //crosshairType: "Full",
            //height: 600,
            rowDoubleClickHandling: "Enabled",
            eventHoverHandling: "Bubble",
            bubble: new DayPilot.Bubble(),

            useEventBoxes: "ShortEventsOnly",//'Always','ShortEventsOnly','Never'
            allowMultiSelect: true,
            rowMarginBottom : 8,
            eventClickHandling: "Select",
            eventMovingStartEndEnabled: true,
            eventMovingStartEndFormat: "MMM d,h:mm tt",
            eventResizingStartEndEnabled: true,
            eventResizingStartEndFormat: "MMM d,h:mm tt",

            eventTimeRangeSelected:false,

            //jointEventsResize: true,
            //jointEventsMove : true,

            //Moving
            moveBy: 'Full',
            allowMultiMove: true,
            onGridMouseDown: function (args) {
                if (switchShift) {
                    if (args.shift) {
                        args.action = "TimeRangeSelect";
                    }
                    else {
                        args.action = "RectangleSelect";
                    }
                }
            },
            separators: [
                { color: "Red", location: new DayPilot.Date(), layer: "BelowEvents" }
            ],

            contextMenuResource: new DayPilot.Menu({
                items: [
                      {
                          text: 'Add New Program', onclick: function () {
                              var e = this.source;

                              return DyeingDataService.getDataByUrl('/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID=null&pIS_SMP_BLK=B&pDYE_BATCH_SCDL_ID=' + (LcScheduleData.DYE_BATCH_SCDL_ID || $stateParams.pDYE_BATCH_SCDL_ID) + '&pDYE_MACHINE_ID=' + e.data.id).then(function (res) {
                                  var data = {
                                      DYE_MACHINE_ID: e.data.id,
                                      UN_LOAD_TIME: '',
                                      DYE_BATCH_SCDL_ID: (LcScheduleData.DYE_BATCH_SCDL_ID || $stateParams.pDYE_BATCH_SCDL_ID),
                                      START_DT: LcScheduleData.START_DT,
                                      END_DT: LcScheduleData.END_DT,
                                      DYE_BATCH_PLAN_ID: -1,
                                      DURATION: 0,
                                      RF_FAB_PROD_CAT_ID: 2,
                                      IS_SMP_BLK: 'B'
                                  }

                                  if(res.length==1){
                                      data['LOAD_TIME'] = res[0].LAST_EVT_END;
                                      data['MAX_LOAD'] = res[0].MAX_LOAD;
                                      data['PCT_EFFCNCY'] = res[0].PCT_EFFCNCY;
                                      openNewProgramEntryModal(data);
                                  }
                              });


                          }
                      }
                ]
            }),


            onRowFilter: function (args) {
                if (args.row.name.toUpperCase().indexOf(args.filter.toUpperCase()) === -1) {
                    args.visible = false;
                }
            },
            onEventFilter: function (args) {
                if (args.e.text().toUpperCase().indexOf(args.filter.toUpperCase()) === -1) {
                    args.visible = false;
                }
            },

            eventHeight: 35,

            rowHeaderColumns: [
                { title: "M/C", width: 100 }
            ],

            onBeforeResHeaderRender: function (args) {
                console.log(args.resource);
                args.resource.columns[0].html = args.resource.MODEL_NO;
                switch (args.resource.IS_SMP_BLK) {
                    case "S":
                        args.resource.cssClass = "status_dirty";
                        break;
                    case "B":
                        args.resource.cssClass = "status_cleanup";
                        break;
                }
                //args.resource.areas = [
                //        { right: 5, top: 5, width: 17, height: 17, v: "Hover", "html": "<i class=\"fa fa-chevron-down\"></i>", cssClass: "resource_action_menu", action: "ContextMenu", menu: dp.contextMenuResource },
                //        { left: 3, right: 0, top: 14, height: 3, v: "Visible", "html": "C:" + args.resource.D_PRD_CAPACITY + " |L:" + args.resource.TTL_LOAD + " |B: <b>" + args.resource.LOAD_TO_DO + "</b>", action: "None" }
                //];

            },
            onEventMoving: function (args) {
                //if (args.overlapping) {
                //    args.left.enabled = true;
                //    args.left.html = "Conflict with an existing Dyeing Program!";
                //}

                var offset = args.start.getMinutes() % 10;

                if (offset) {
                    args.start = args.start.addMinutes(-offset);
                    args.end = args.end.addMinutes(-offset);
                }

                args.left.enabled = true;
                args.left.html = args.start.toString("h:mm tt");
                //new DayPilot.Date(LcScheduleData.END_DT)
                //if (args.end > dp.visibleEnd()) {
                //    args.allowed = false;
                //    args.left.enabled = false;
                //    args.right.html = "Can't move after Schedule End Date.";
                //}

                //if (args.e.resource() === "A" && args.resource === "B") {  // don't allow moving from A to B
                //    args.left.enabled = false;
                //    args.right.html = "You can't move an event from Room 1 to Room B";

                //    args.allowed = false;
                //}


                //if (args.e.resource() === "A" && args.resource === "B") {  // don't allow moving from A to B
                //    args.left.enabled = false;
                //    args.right.html = "You can't move an event from Room 1 to Room B";

                //    args.allowed = false;
                //}
                //else if (args.resource === "B") {  // must start on a working day, maximum length one day
                //    while (args.start.getDayOfWeek() == 0 || args.start.getDayOfWeek() == 6) {
                //        args.start = args.start.addDays(1);
                //    }
                //    args.end = args.start.addDays(1);  // fixed duration
                //    args.left.enabled = false;
                //    args.right.html = "Events in Room 2 must start on a workday and are limited to 1 day.";
                //}

                //if (args.resource === "C") {
                //    var except = args.e.data;
                //    var events = dp.rows.find(args.resource).events.all();

                //    var start = args.start;
                //    var end = args.end;
                //    var overlaps = events.some(function(item) {
                //        return item.data !== except && DayPilot.Util.overlaps(item.start(), item.end(), start, end);
                //    });

                //    while (overlaps) {
                //        start = start.addDays(1);
                //        end = end.addDays(1);

                //        overlaps = events.some(function(item) {
                //            return item.data !== except && DayPilot.Util.overlaps(item.start(), item.end(), start, end);
                //        });
                //    }

                //    args.start = start;
                //    args.end = end;

                //    args.left.enabled = false;
                //    args.right.html = "Start automatically moved to " + args.start.toString("d MMMM, yyyy");
            },


            //onEventMove: function (args) {
            //    if (args.ctrl) {
            //        var data = [{
            //            DYE_BATCH_PLAN_ID: args.e.id(),
            //            LOAD_TIME: new DayPilot.Date(args.newStart).toString("yyyy-MM-ddTHH:mm:00"),
            //            UN_LOAD_TIME: new DayPilot.Date(args.newEnd).toString("yyyy-MM-ddTHH:mm:00"),
            //            DYE_MACHINE_ID: args.newResource
            //        }];

            //        DyeingDataService.saveDataByUrl({
            //            DYE_BATCH_SCDL_ID: (LcScheduleData.DYE_BATCH_SCDL_ID || 0),
            //            XML: config.xmlStringShort(data),
            //            pOption: 1004
            //        }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
            //            if (res.success === false) {
            //            }
            //            else {
            //                res['data'] = angular.fromJson(res.jsonStr);
            //                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
            //                   // loadResources($scope.DYE_MC_TYPE_ID, $scope.IS_SMP_BLK, LcScheduleData.DYE_BATCH_SCDL_ID);
            //                    loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS,true);
            //                } else {
            //                    config.appToastMsg(res.data.PMSG);
            //                }

            //            }
            //        });
            //        args.preventDefault();
            //    }
            //},
            onEventMoved: function (args) {
                var data = [];
                args.multimove.map(function (item) {
                    data.push({
                        DYE_MACHINE_ID: item.event.resource(),
                        DYE_BATCH_PLAN_ID: item.event.id(),
                        LOAD_TIME: new DayPilot.Date(item.start.value).toString("yyyy-MM-ddTHH:mm:00"),
                        UN_LOAD_TIME: new DayPilot.Date(item.end.value).toString("yyyy-MM-ddTHH:mm:00")
                    });
                });
                return DyeingDataService.saveDataByUrl({
                    DYE_BATCH_SCDL_ID: (LcScheduleData.DYE_BATCH_SCDL_ID || 0),
                    XML: config.xmlStringShort(data),
                    pOption: 1002,
                    IS_SMP_BLK:'B'
                }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                    if (res.success === false) {
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //loadResources($scope.DYE_MC_TYPE_ID, $scope.IS_SMP_BLK, LcScheduleData.DYE_BATCH_SCDL_ID);
                            loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS,true);
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                });
            },

            onRowDoubleClick: function (args) {

                dp.multiselect.clear();
            },
            onEventResizing: function (args) {
                //args.left.enabled = false;
                //args.right.enabled = true;
                //console.log(args.duration.totalHours());
            },

            onEventClick: function (args) {
                if (args.shift || args.ctrl) {
                    if (_.some(dp.multiselect.events(), function (o) {
                        return o.data.id == args.e.data.id;
                    })) {
                        dp.multiselect.remove(args.e, false);
                    } else {
                        dp.multiselect.add(args.e);
                    }
                    args.preventDefault();
                }
            },
            onEventSelect: function (args) {
                dp.multiselect.add(args.e);
            },
            onEventResized: function (args) {
                var data = [
                    {
                        DYE_MACHINE_ID: args.e.resource(),
                        DYE_BATCH_PLAN_ID: args.e.id(),
                        LOAD_TIME: args.newStart.value,
                        UN_LOAD_TIME: args.newEnd.value
                    }];

                return DyeingDataService.saveDataByUrl({
                    DYE_BATCH_SCDL_ID: (LcScheduleData.DYE_BATCH_SCDL_ID || 0),
                    XML: config.xmlStringShort(data),
                    pOption: 1002,
                    IS_SMP_BLK: 'B'
                }, '/DyeBatchPlan/SaveSchedulePlanData').then(function (res) {
                    if (res.success === false) {
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            //loadResources($scope.DYE_MC_TYPE_ID, $scope.IS_SMP_BLK, LcScheduleData.DYE_BATCH_SCDL_ID);
                            loadEvents(LcScheduleData.START_DT, LcScheduleData.END_DT, LcScheduleData.DURATION_IN_DYS,true);
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                });


            },

            //onTimeRangeSelected: function (args) {
            //    if ((Math.abs(new Date(args.start.value) - new Date(args.end.value)) / 36e5) < 3)
            //        return;
            //    var data = {
            //        DYE_MACHINE_ID: args.resource,
            //        LOAD_TIME: args.start.value,
            //        UN_LOAD_TIME: args.end.value,
            //        DYE_BATCH_SCDL_ID: LcScheduleData.DYE_BATCH_SCDL_ID,
            //        START_DT: LcScheduleData.START_DT,
            //        END_DT: LcScheduleData.END_DT,
            //        DYE_BATCH_PLAN_ID: -1,
            //        DURATION: Math.abs(new Date(args.start.value) - new Date(args.end.value)) / 36e5,
            //        LOAD_TO_DO: _.find($scope.schedulerConfig.resources, function (o) { return o.id == parseInt(args.resource); }).LOAD_TO_DO || 0
            //    }
            //    openNewProgramEntryModal(data);
            //},
            onBeforeEventRender: function (args) {
                var start = new DayPilot.Date(args.data.start);
                var end = new DayPilot.Date(args.data.end);

                var now = new DayPilot.Date();
                var today = new DayPilot.Date().getDatePart();
                var status = args.e.status;
                var bubbleHtml = '';

                args.data.html = args.data.text + " (" + start.toString("M/d") + " - " + end.toString("M/d") + ")";

                //+ "<br /><span style='color:gray'>" + status + "</span>"
                bubbleHtml += "<div>Progam#: <b>" + args.e.id + "</b></div>";
                //bubbleHtml += "<div>M/C#: <b>" + args.e.resource + "</b></div>";
                bubbleHtml += "<b>" + new DayPilot.Date(args.e.start).toString("d/M/yy hh:mm tt") + " to " + new DayPilot.Date(args.e.end).toString("d/M/yy hh:mm tt") + "</b></div>";
                bubbleHtml += "<div><b>[" + args.e.DURATION_STR + "]</b></div>";
                bubbleHtml += "<div>Buyer A/C : <b>" + args.e.BYR_ACC_NAME_EN + "</b></div>";
                bubbleHtml += "<div>Style : <b>" + args.e.WORK_STYLE_NO + "</b></div>";
                bubbleHtml += "<div>Order # : <b>" + args.e.ORDER_NO_LIST + "</b></div>";
                bubbleHtml += "<div>Fab Type : <b>" + args.e.FAB_TYPE_NAME + "</b></div>";
                bubbleHtml += "<div>Color : <b>" + args.e.COLOR_NAME_EN + "</b></div>";

                bubbleHtml += "<div>Quantity : <b>" + args.e.RQD_PRD_QTY + "</b> Kg</div>";

                //bubbleHtml += "<div>Completion: <b>" + args.e.pct_completion + "%</b></div>";
                //bubbleHtml += "<div>Status: <b style='color:" + args.e.barColor + ";'>" + args.e.status + "</b></div>";

                args.e.bubbleHtml = bubbleHtml;

                switch(args.e.barColor){
                    case "#43c150": //Batch Created
                        args.e.contextMenu = default_menu;
                        break;
                    case "#6e7ecf": //Hold
                        args.e.contextMenu = general;
                        break;
                    default:
                       
                      
                }
            },
            onBeforeTimeHeaderRender: function (args) {
                args.header.html = args.header.html.replace(" AM", "a").replace(" PM", "p");

                if (args.header.level === 2) {
                    if (args.header.end <= new DayPilot.Date(LcScheduleData.END_DT) && args.header.end > new DayPilot.Date(LcScheduleData.START_DT)) {
                        args.header.backColor = '#a1abe0';
                    }
                }


            },

        };

        $timeout(function () {
            dp = $scope.scheduler;  // debug
            loadEvents((LcScheduleData.START_DT || DayPilot.Date.today()), (LcScheduleData.END_DT || DayPilot.Date.today()), (LcScheduleData.DURATION_IN_DYS || 3),false);
        });

        vm.print = function () {
            dp.exportAs("jpg", { area: 'range', dateFrom: LcScheduleData.START_DT, dateTo: LcScheduleData.END_DT }).print();
        }

        function loadEvents(fromDT, toDT, DURATION_IN_DYS,vs) {
            return DyeingDataService.getDataByUrl('/DyeBatchPlan/loadEventData?pSTART_DT=' + new DayPilot.Date((fromDT || LcScheduleData.START_DT)).addDays(-3).toString("yyyy-MM-ddTHH:00:00") + '&pEND_DT=' + new DayPilot.Date(toDT).addDays(3).toString("yyyy-MM-ddTHH:00:00")).then(function (res) {

                //$scope.schedulerConfig.timeline = getTimeline((fromDT || LcScheduleData.START_DT), (toDT || LcScheduleData.END_DT), (DURATION_IN_DYS || LcScheduleData.DURATION_IN_DYS));
               // $scope.schedulerConfig.scrollTo = LcScheduleData.START_DT || DayPilot.Date.today();// $scope.scheduler.visibleStart();//visibleEnd()
                $scope.schedulerConfig.scrollTo = vs ? $scope.scheduler.getViewPort().start : LcScheduleData.START_DT;
                $scope.schedulerConfig.scrollToAnimated = "fast";
                //$scope.schedulerConfig.scrollToPosition = "left";
                $scope.events = res;
            });
        }

        //function getTimeline(st, ed, days) {
        //    var startdate = new DayPilot.Date(st).addDays(-3);
        //    var enddate = new DayPilot.Date(ed).addDays(3);
        //    var timeline = [];

        //    var startHr, endHour;

        //    for (var i = 0; i < (days + 3 + 3) ; i += 1) {
        //        var startDt = startdate.getDatePart().addDays(i);

        //        if (i == 0) {
        //            startHr = startdate.getHours();
        //            endHour = 24;
        //        } else if (i == (days + 2 + 3)) {
        //            startHr = 0
        //            endHour = enddate.getHours() + 1;// + (enddate.getMinutes() > 0 ? 1: 0);
        //        } else {
        //            startHr = 0
        //            endHour = 24;
        //        }

        //        for (var x = startHr  ; x < endHour ; x += 1) {
        //            timeline.push({ start: startDt.addHours(x), end: startDt.addHours(x + 1) });
        //        }

        //        //for (var x = (startHr * 60) ; x < (endHour * 60) ; x += 10) {
        //        //    timeline.push({ start: startDt.addMinutes(x), end: startDt.addMinutes(x + 10) });
        //        //}
        //    }

        //    return timeline;
        //}

        vm.SchedulePlanData = {
            optionLabel: "--Select Plan--",
            filter: "contains",
            autoBind: true,
            template: '<span><h4 style="padding:0px;margin:0px;"><b>#: data.SCDL_REF_NO #</b></h4><p> #= kendo.toString(kendo.parseDate(data.START_DT),"dd/MM/yyyy HH:mm") # to #= kendo.toString(kendo.parseDate(data.END_DT),"dd/MM/yyyy HH:mm") #</p></span>',
            valueTemplate: '<span><h4 style="padding:0px;margin:0px;"><b>#: data.SCDL_REF_NO #</b><small>(#= kendo.toString(kendo.parseDate(data.START_DT),"dd/MM/yyyy HH:mm") # to #= kendo.toString(kendo.parseDate(data.END_DT),"dd/MM/yyyy HH:mm") #)</small></h4></span>',
            dataSource: {
                transport: {
                    read: function (e) {


                        var url = "/DyeBatchPlan/getSchedulePlanDatas";
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '?pSCDL_REF_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '?pSCDL_REF_NO';
                        }
                        url += '&pDYE_BATCH_SCDL_ID=' + ($stateParams.pDYE_BATCH_SCDL_ID||null);
                        url += '&pIS_SMP_BLK=B';

                        DyeingDataService.getDataByUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true,
            },
            select: function (e) {

                var item = this.dataItem(e.item);
                if (item.IS_FINALIZED == 'N' && item.DYE_BATCH_SCDL_ID) {
                    vm.EditPlanData = item;
                }
                else {
                    vm.EditPlanData['DYE_BATCH_SCDL_ID'] = null;
                }

                if (item && item.DYE_BATCH_SCDL_ID) {
                    $state.go('DyeBatchPlan', { pDYE_BATCH_SCDL_ID: item.DYE_BATCH_SCDL_ID }, { notify: false });

                    $scope.schedulerConfig.startDate = new DayPilot.Date(item.START_DT).addDays(-1);
                    $scope.schedulerConfig.days = 5;
                    LcScheduleData = item;
                    //loadResources($scope.DYE_MC_TYPE_ID, $scope.IS_SMP_BLK, item.DYE_BATCH_SCDL_ID);
                    loadResources(null, 'B', item.DYE_BATCH_SCDL_ID, $scope.HR_COMPANY_ID);
                    loadEvents(item.START_DT, item.END_DT, item.DURATION_IN_DYS);

                };
            },
            dataBound: function (e) {
                var ds = e.sender.dataSource.data();

                if ($stateParams.pDYE_BATCH_SCDL_ID) {
                    var item = _.find(ds, function (o) {
                        return o.DYE_BATCH_SCDL_ID == $stateParams.pDYE_BATCH_SCDL_ID;
                    });
                } else {
                    var item = _.find(ds, function (o) {
                        return o.IS_ACTIVE == 'Y';
                    });
                }


                if (item && item.DYE_BATCH_SCDL_ID) {
                    LcScheduleData = item;
                    loadResources(null, 'B', item.DYE_BATCH_SCDL_ID, (McList.length>0 ? McList[0].HR_COMPANY_ID:''));
                    loadEvents(item.START_DT, item.END_DT, item.DURATION_IN_DYS);
                    $state.go('DyeBatchPlan', { pDYE_BATCH_SCDL_ID: item.DYE_BATCH_SCDL_ID }, { notify: false });
                    e.sender.value(item.DYE_BATCH_SCDL_ID);

                };
            },
            dataTextField: "SCDL_REF_NO",
            dataValueField: "DYE_BATCH_SCDL_ID"
        };


        vm.OpenReportDyeingBatchProg = function () {
            console.log('Ex');
            return DyeingDataService.post4pdfRes('/api/Dye/DyeReport/PreviewReport',
                  {
                      REPORT_CODE: 'RPT-4002',
                      FROM_DATE: LcScheduleData.START_DT,
                      TO_DATE: LcScheduleData.END_DT,
                      DYE_BATCH_SCDL_ID: LcScheduleData.DYE_BATCH_SCDL_ID,
                      HR_COMPANY_ID: $scope.HR_COMPANY_ID
                  }
                  ).then(function (res) {
                      var file = new Blob([res], { type: 'application/pdf' });
                      var fileURL = URL.createObjectURL(file);
                      $window.open(fileURL, "_new", "location=yes,width=1000,height=600,scrollbars=yes,status=yes");
                  })
        };
    }

})();