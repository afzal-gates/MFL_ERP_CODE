(function () {
    'use strict';
    angular.module('multitex.planning').controller('GmtLineLoadingPlanController', ['$q', 'config', 'PlanningDataService', '$stateParams', '$state', '$scope', '$timeout', '$modal', '$window', '$filter', 'Dialog', GmtLineLoadingPlanController]);
    function GmtLineLoadingPlanController($q, config, PlanningDataService, $stateParams, $state, $scope, $timeout, $modal, $window, $filter, Dialog) {

        var vm = this;
        $scope.showSplash = false;
        var dp;
        var floor;
        var switchShift = true;
        vm.params = $stateParams;
        vm.form = {
            IS_REGULAR_VIEW: 'Y',
            IS_GMT_ITEM_VIEW : 'N'
        };

        $scope.TimeRange = 3;
        $scope.scale = "1D";

        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        $scope.refreshData =  function() {
            dp.update();
        };

        vm.oderListDs = {
            transport: {
                read: function (e) {

                    var url = "/api/common/getOrderStyleDropDownDataForPln"
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    if (params.filter) {
                        url += '?pORDER_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        return PlanningDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        });
                    } else {
                        e.success([]);
                    }
                }
            },
            serverFiltering: true
        }


        //vm.companyOpt = {
        //    optionLabel: "-- All --",
        //    filter: "contains",
        //    autoBind: true,
        //    dataTextField: "COMP_NAME_EN",
        //    dataValueField: "HR_COMPANY_ID",
        //    dataSource: {
        //        transport: {
        //            read: function (e) {
        //                PlanningDataService.getDataByUrl('/GmtLineLoad/getCompanyData').then(function (res) {
        //                    e.success(res);
        //                }, function (err) {
        //                    console.log(err);
        //                });
        //            }
        //        }
        //    },
        //    change: function (e) {
        //        var dataItem = this.dataItem(e.item);
        //        return vm.officeDs = new kendo.data.DataSource({
        //            transport: {
        //                read: function (e) {
        //                    vm.form['HR_OFFICE_ID'] = '';
        //                    vm.form['HR_PROD_FLR_ID'] = '';
        //                    vm.form['HR_PROD_LINE_ID'] = '';
                          
        //                    loadResources(vm.form.HR_COMPANY_ID);

        //                    PlanningDataService.getDataByUrl('/GmtLineLoad/getOfficeList?pHR_COMPANY_ID=' + (dataItem.HR_COMPANY_ID||'')).then(function (res) {
        //                        e.success(res);
        //                    });
        //                }
        //            }
        //        });
        //    }
        //};

            //vm.officeOpt = {
            //    optionLabel: "-- All --",
            //    filter: "contains",
            //    autoBind: true,
            //    dataTextField: "OFFICE_NAME_EN",
            //    dataValueField: "HR_OFFICE_ID",
            //    change: function (e) {
            //        var ditm = this.dataItem(e.item);
            //        return vm.floorDs = new kendo.data.DataSource({
            //            transport: {
            //                read: function (e) {
                       
            //                    vm.form['HR_PROD_FLR_ID'] = '';
            //                    vm.form['HR_PROD_LINE_ID'] = '';
            //                    loadResources(vm.form.HR_COMPANY_ID, vm.form.HR_OFFICE_ID);
            //                    PlanningDataService.getDataByUrl('/GmtLineLoad/getFloorData?pHR_OFFICE_ID=' + (ditm.HR_OFFICE_ID||'')).then(function (res) {
            //                        e.success(res);
            //                    });
            //                }
            //            }
            //        });
            //    }
            //};

            $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO #</h5><p> Style: #: data.STYLE_NO #</p></span>';
            $scope.valueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';


            vm.refreshScheduler = function () {

                $state.go('GmtLineLoadPlan', { },  { reload: true });

                //var strt = $scope.scheduler.getViewPort().start.toString();
                //var end = $scope.scheduler.getViewPort().end.toString();
                //var resources = $scope.scheduler.getViewPort().resources.filter(function (x) { return x < 10000 }).join(',');
                //var url = '/GmtLineLoad/getResourceData';
                //url += '?pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID || '');
                //url += '&pHR_OFFICE_ID=' + (vm.form.HR_OFFICE_ID || '');
                //url += '&pHR_PROD_FLR_ID=' + (vm.form.HR_PROD_FLR_ID || '');
                //url += '&pHR_PROD_LINE_ID=' + (vm.form.HR_PROD_LINE_ID || '');
                //url += '&pMC_ORDER_SHIP_ID=' + (vm.form.MC_ORDER_SHIP_ID || '');

                //url += '&pSTART_DT=' + ($scope.scheduler.visibleStart() || '');
                //url += '&pEND_DT=' + ($scope.scheduler.visibleEnd() || '');
                //url += '&pRESOURCES=' + (resources || '');

                //return PlanningDataService.getDataByUrl(url).then(function (res) {
                //    $scope.schedulerConfig.resources = res;
                //    $scope.schedulerConfig.visible = true;
                //    $scope.scheduler.async = true;
                //    return PlanningDataService.getDataByUrl('/GmtLineLoad/getEventData?pSTART_DT=' + strt + '&pEND_DT=' + end + '&pRESOURCES=' + resources + '&pMC_ORDER_SHIP_ID=' + (vm.form.MC_ORDER_SHIP_ID || '')).then(function (res) {
                //        $scope.schedulerConfig.events = res;
                //        $scope.scheduler.loaded();
                //    });
                //});
            };
           vm.officeDs = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        PlanningDataService.getDataByUrl('/GmtLineLoad/getOfficeList?pHR_COMPANY_ID').then(function (res) {
                            e.success(res);
                        });
                    }
                }
           });

           vm.floorOpt = {
               optionLabel: "-- All --",
               filter: "contains",
               autoBind: true,
               dataTextField: "FLOOR_CODE",
               dataValueField: "HR_PROD_FLR_ID",
               change: function (e) {
                   var dtm = this.dataItem(e.item);
                   return vm.lineDs = new kendo.data.DataSource({
                       transport: {
                           read: function (e) {
                               vm.form['HR_PROD_LINE_ID'] = '';
                               loadResources(vm.form.HR_COMPANY_ID, vm.form.HR_OFFICE_ID, vm.form.HR_PROD_FLR_ID);
                               PlanningDataService.getDataByUrl('/GmtLineLoad/getLineData?pHR_PROD_FLR_ID=' + (dtm.HR_PROD_FLR_ID||'')).then(function (res) {
                                   e.success(res);
                               });
                           }
                       }
                   });
               }
           };

           vm.floorDs = new kendo.data.DataSource({
               transport: {
                   read: function (e) {
                       PlanningDataService.getDataByUrl('/GmtLineLoad/getFloorData?pHR_OFFICE_ID').then(function (res) {
                           e.success(res);
                       });
                   }
               }
           });

           vm.lineOpt = {
               optionLabel: "-- All --",
               filter: "contains",
               autoBind: true,
               change: function (e) {
                   loadResources(vm.form.HR_COMPANY_ID, vm.form.HR_OFFICE_ID, vm.form.HR_PROD_FLR_ID, vm.form.HR_PROD_LINE_ID);
               }
           };

           vm.lineDs = new kendo.data.DataSource({
               transport: {
                   read: function (e) {
                       PlanningDataService.getDataByUrl('/GmtLineLoad/getLineData?pHR_PROD_FLR_ID').then(function (res) {
                           e.success(res);
                       });
                   }
               }
           });


        //function createYarnRequisition(KNT_JOB_CRD_H_LST) {
        //    var modalInstance = $modal.open({
        //        animation: true,
        //        templateUrl: '/Knitting/Knit/_YarnReqInHouseProd',
        //        controller: 'YarnReqInHouseProdController',
        //        size: 'lg',
        //        windowClass: 'app-modal-window',
        //        resolve: {
        //            JobCardList: function () {
        //                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnReqInHouseProd?pKNT_JOB_CRD_H_LST=' + KNT_JOB_CRD_H_LST);
        //            }
        //        }
        //    });

        //    modalInstance.result.then(function (data) {
        //        console.log(data);
        //    }, function () {
        //        console.log('Modal dismissed at: ' + new Date());
        //    });
        //};

        //vm.printRequisitionList = function () {
        //    var form = document.createElement("form");
        //    form.setAttribute("method", "post");
        //    form.setAttribute("action", '/api/knit/KntReport/PreviewReport');
        //    form.setAttribute("target", '_blank');

        //    var params = angular.extend({ REPORT_CODE: 'RPT-3507' }, {
        //        FROM_DATE: $filter('date')(new Date(),'dd-MMM-yyyy'),
        //        TO_DATE: $filter('date')(new Date(), 'dd-MMM-yyyy')
        //    });

        //    for (var i in params) {
        //        if (params.hasOwnProperty(i)) {

        //            var input = document.createElement('input');
        //            input.type = 'hidden';
        //            input.name = i;
        //            input.value = params[i];
        //            form.appendChild(input);
        //        }
        //    }

        //    document.body.appendChild(form);
        //    form.submit();
        //    document.body.removeChild(form);
        //};

        

           function loadResources(pHR_COMPANY_ID, pHR_OFFICE_ID, pHR_PROD_FLR_ID, pHR_PROD_LINE_ID, pRESOURCES, pMC_ORDER_SHIP_ID) {


               var url = '/GmtLineLoad/getResourceData';
               url += '?pHR_COMPANY_ID=' + (pHR_COMPANY_ID || '');
               url += '&pHR_OFFICE_ID=' + (pHR_OFFICE_ID || '');
               url += '&pHR_PROD_FLR_ID=' + (pHR_PROD_FLR_ID || '');
               url += '&pHR_PROD_LINE_ID=' + (pHR_PROD_LINE_ID || '');
               url += '&pSTART_DT=' + ($scope.scheduler.visibleStart() || '');
               url += '&pEND_DT=' + ($scope.scheduler.visibleEnd() || '');
               url += '&pRESOURCES=' + (pRESOURCES || '')
               url += '&pMC_ORDER_SHIP_ID=' + (pMC_ORDER_SHIP_ID || '')

            return PlanningDataService.getDataByUrl(url).then(function (res) {
                $scope.schedulerConfig.resources = res;
                $scope.schedulerConfig.visible = true;
            });
        }


        function getMcDiaList() {
            return PlanningDataService.getDataByUrl('/KnitCommon/getMachineDiaList').then(function (res) {
                vm.machineDiaDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        //function getKnitMachineList(ACT_MC_DIA_ID, HR_PROD_FLR_ID,HR_COMPANY_ID) {
        //    return PlanningDataService.getDataByUrl('/KnitPlan/getKnitMachine?pACT_MC_DIA_ID=' + (ACT_MC_DIA_ID || null) + '&pHR_PROD_FLR_ID=' + (HR_PROD_FLR_ID || null) + '&pHR_COMPANY_ID=' + (HR_COMPANY_ID || null)).then(function (res) {
        //        $scope.MachineDs = new kendo.data.DataSource({
        //            data: res
        //        });
        //    });
        //}

        $scope.scaleDs = new kendo.data.DataSource({
            data: [
                { txt: 'Hours', val: '1H' },
                { txt: 'Days', val: '1D' },
                { txt: 'Weeks', val: '1W' }
            ]
        });


        $scope.timeRangeDs = new kendo.data.DataSource({
            data: [
                { txt: '1M', val: 1 },
                { txt: '2M', val: 2 },
                { txt: '3M', val: 3 },
                { txt: '4M', val: 4 },
                { txt: '5M', val: 5 },
                { txt: '6M', val: 6 }
            ]
        });

        $scope.$watchGroup(["scale", "TimeRange"], function (newVal, oldVal) {

            if (!oldVal[0] || !oldVal[1] || (oldVal[0] === newVal[0] && oldVal[1] === newVal[1])) {
                return;
            } else {
                if ($scope.scheduler) {
                    $scope.schedulerConfig.timeline = getTimeline($scope.scheduler.getViewPort().start);
                    $scope.schedulerConfig.timeHeaders = getTimeHeaders();
                    loadResources(vm.form.HR_COMPANY_ID, vm.form.HR_OFFICE_ID, vm.form.HR_PROD_FLR_ID, vm.form.HR_PROD_LINE_ID);
                    $scope.schedulerConfig.scrollToAnimated = "fast";
                    $scope.schedulerConfig.scrollTo = $scope.scheduler.getViewPort().start;
                }
            }

        });


        vm.ChangeDataByDate = function (pPROD_DT) {
            var day = pPROD_DT ? new DayPilot.Date(pPROD_DT) : new DayPilot.Date();
            if (!pPROD_DT) {
                $('#start').text(new DayPilot.Date().toString("dd-MMM-yyyy"));
            }
            if ($scope.scheduler.visibleStart().getDatePart() <= day && day < $scope.scheduler.visibleEnd()) {
                $scope.schedulerConfig.scrollToAnimated = "fast";
                $scope.schedulerConfig.scrollTo = day;
            } else {
                $scope.schedulerConfig.timeline = getTimeline(day);
                $scope.schedulerConfig.timeHeaders = getTimeHeaders();
                $scope.schedulerConfig.scrollToAnimated = "fast";
                $scope.schedulerConfig.scrollTo = day;
            }
        };
        $window.ChangeDataByDate = vm.ChangeDataByDate;



        $scope.onChangeCellWidth = function (cellwidth) {
            dp.cellWidth = cellwidth;
            dp.update();
        },

        $scope.onSearchEvent = function (query) {
            if (query) {
                dp.events.filter(query);
            } else {
                dp.events.filter(null);
            }
        }


        $scope.clearSelection = function () {
            if (!dp)
                return;
            dp.multiselect.clear();
        }


        function LineLoadEventOffset(pGMT_PLN_LINE_LOAD_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Planning/Pln/_GmtLineLoadingEventOffset',
                controller: 'GmtLineLoadingEventOffsetModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    data: function () {
                        return PlanningDataService.getDataByUrl('/GmtLineLoad/getEventDataById?pGMT_PLN_LINE_LOAD_ID=' + pGMT_PLN_LINE_LOAD_ID);
                    }
                }
            });
            modalInstance.result.then(function (dta) {
                dp.update();
            });
        }

        function PlanChangeModal(pGMT_PLN_LINE_LOAD_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Planning/Pln/_GmtPlanChangeModal',
                controller: 'GmtPlanChangeModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    data: function () {
                        return PlanningDataService.getDataByUrl('/GmtLineLoad/getEventDataById?pGMT_PLN_LINE_LOAD_ID=' + pGMT_PLN_LINE_LOAD_ID);
                    },
                    ResDept: function () {
                        return PlanningDataService.getDataByFullUrl('/api/common/getRespDeptList');
                    }
                }
            });
            modalInstance.result.then(function (dta) {
                dp.update();
            });
        }


        function OpenProdMonitoringModal(pMC_ORDER_ITEM_PLN_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Planning/Pln/_GmtProdMonitoringModal',
                controller: 'GmtProdMonitoringModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    data: function () {
                        return PlanningDataService.getDataByUrl('/GmtLineLoad/FindProdMonitoringData?pMC_ORDER_ITEM_PLN_ID=' + pMC_ORDER_ITEM_PLN_ID);
                    }
                }
            });
            modalInstance.result.then(function (dta) {
            });
        }

        function openManualLineLoadingModal(data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Planning/Pln/_ManualLineLoadingModal',
                controller: 'ManualLineLoadingModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    data: function () {
                        data['MC_ORDER_ITEM_PLN_LST'] = data.MC_ORDER_ITEM_PLN_ID.toString();
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                dp.update();
                //$state.go($state.current, $stateParams, { reload: vm.state, inherit: true });
            }, function () {
                dp.update();
            });
        }




        function TuningPlanFn(pGMT_PLN_LINE_LOAD_ID) {
            var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/Planning/Pln/_GmtLineLoadingTune',
                    controller: 'GmtLineLoadingTuneModalController',
                    size: 'lg',
                    windowClass: 'app-modal-window',
                    resolve: {
                        data: function () {
                            return PlanningDataService.getDataByUrl('/GmtLineLoad/getEventDataForTuning?pGMT_PLN_LINE_LOAD_ID=' + pGMT_PLN_LINE_LOAD_ID);
                        },
                        ResDept: function () {
                            return PlanningDataService.getDataByFullUrl('/api/common/getRespDeptList');
                        }
                    }
                });
            modalInstance.result.then(function (dta) {

                if (dta > 0) {
                    TuningPlanFn(dta);
                }

            }, function (dta) {
                dp.update();
                    //if (dta > 0) {
                    //    var resources = $scope.scheduler.getViewPort().resources.filter(function (x) { return x < 10000 }).join(',');
                    //    return PlanningDataService.getDataByUrl('/GmtLineLoad/getEventData?pSTART_DT=' + $scope.scheduler.visibleStart() + '&pEND_DT=' + $scope.scheduler.visibleEnd() + '&pRESOURCES=' + resources + '&pMC_ORDER_SHIP_ID=' + (vm.form.MC_ORDER_SHIP_ID || '')).then(function (res) {
                    //        $scope.schedulerConfig.events = res;
                            
                    //        $scope.schedulerConfig.scrollTo = $scope.scheduler.visibleStart();
                    //        $scope.schedulerConfig.scrollToAnimated = "fast";
                    //        $scope.schedulerConfig.scrollToPosition = "left";
                    //    });
                    //} else {
                    //     dp.update();
                    //}
                });
        }



        function Move2Next(source, pOption, msg_txt) {
          
            Dialog.confirm( msg_txt+'?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 $scope.showSplash = true;
                 return PlanningDataService.saveDataByUrl({
                     GMT_PLN_LINE_LOAD_ID: source.data.GMT_PLN_LINE_LOAD_ID,
                     HR_PROD_LINE_ID: source.data.resource,
                     SEW_START_DT: source.data.start,
                     SEW_END_DT: source.data.end,
                     pOption: pOption
                 }, '/GmtLineLoad/updateEvent').then(function (res) {
                     if (res.success === false) {
                         vm.errors = res.errors;
                     }
                     else {
                         res['data'] = angular.fromJson(res.jsonStr);
                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             dp.update();
                             $scope.showSplash = false;
                         } else {
                             config.appToastMsg(res.data.PMSG);
                         }
                     }
                 })
             });
        }

        function AutoFitStartDate(source, msg_txt) {
            Dialog.confirm(msg_txt + '?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 $scope.showSplash = true;
                 return PlanningDataService.getDataByUrl('/GmtLineLoad/getForcastLineLoadDataById2?pGMT_PLN_LINE_LOAD_ID=' + source.data.GMT_PLN_LINE_LOAD_ID + '&pIS_DYNAMIC_END=Y').then(function (dt) {
                     return PlanningDataService.saveDataByUrl({
                         GMT_PLN_LINE_LOAD_ID: source.data.GMT_PLN_LINE_LOAD_ID,
                         HR_PROD_LINE_ID: source.data.resource,
                         SEW_START_DT: dt.SEW_START_DT,
                         SEW_END_DT: dt.SEW_END_DT,
                         pOption: 1000
                     }, '/GmtLineLoad/updateEvent').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 dp.update();
                                 $scope.showSplash = false;
                             } else {
                                 config.appToastMsg(res.data.PMSG);
                             }
                         }
                     })

                 });

             });
        }

        function AutoFitEndDate(source, msg_txt) {
            Dialog.confirm(msg_txt + '?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 $scope.showSplash = true;
                 return PlanningDataService.getDataByUrl('/GmtLineLoad/getForcastLineLoadDataById?pGMT_PLN_LINE_LOAD_ID=' + source.data.GMT_PLN_LINE_LOAD_ID + '&pIS_DYNAMIC_START=Y').then(function (dt) {
                     return PlanningDataService.saveDataByUrl({
                         GMT_PLN_LINE_LOAD_ID: source.data.GMT_PLN_LINE_LOAD_ID,
                         HR_PROD_LINE_ID: source.data.resource,
                         SEW_START_DT: dt.SEW_START_DT,
                         SEW_END_DT: dt.SEW_END_DT,
                         pOption: 1000
                     }, '/GmtLineLoad/updateEvent').then(function (res) {
                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 dp.update();
                                 $scope.showSplash = false;
                             } else {
                                 config.appToastMsg(res.data.PMSG);
                             }
                         }
                     })

                 });

             });
        }




        function openTNATasks(source) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Planning/Pln/_TnaDataListModal',
                controller: 'GmtLineLoadTnaDatasModalController',
                size: 'lg',
                windowClass: 'app-modal-window',

                resolve: {
                    data: function () {
                        return PlanningDataService.getDataByUrl('/GmtLineLoad/getTnaDatas?pGMT_PLN_LINE_LOAD_ID=' + source.data.GMT_PLN_LINE_LOAD_ID);
                    },
                    summery: function () {
                        return PlanningDataService.getDataByUrl('/GmtLineLoad/GetCpSummeryData?pGMT_PLN_LINE_LOAD_ID=' + source.data.GMT_PLN_LINE_LOAD_ID);
                    },
                    allocation : function () {
                        return source.data;
                    }
                }
            });
            modalInstance.result.then(function (dta) {
                dp.update();
            }, function () {
                dp.update();
            });
        }


        function editKnitCard(source) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'KnitCardEdit.html',
                controller: function ($scope, KnittingDataService, $modalInstance, $q, kcData, Dialog) {


                    $scope.datePickerOptions = {
                        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
                    };

                    function setSchedule(KNT_MACHINE_ID, DURATION, KNT_JOB_CRD_H_ID) {
                        return KnittingDataService.getDataByUrl('/KnitPlan/getScheduleByMachine?pKNT_MACHINE_ID=' + KNT_MACHINE_ID + '&pDURATION=' + DURATION + '&pKNT_JOB_CRD_H_ID='+ KNT_JOB_CRD_H_ID).then(function (res) {

                            $scope.data['START_DT'] = res.START_DT;
                            $scope.data['END_DT'] = res.END_DT;

                        });
                    }

                    $scope.$watchGroup(['data.ASIGN_QTY', 'data.TG_D_PROD_QTY'], function (newVal, oldVal) {
                        if ($scope.data.KNT_MACHINE_ID && $scope.data.KNT_JOB_CRD_H_ID) {
                            if (!isNaN(Math.ceil(newVal[0] * (24 / (newVal[1] || 1))))) {
                                $scope.data['DURATION'] = Math.ceil(newVal[0] * (24 / (newVal[1] || 1)));

                                setSchedule($scope.data.KNT_MACHINE_ID, Math.ceil(newVal[0] * (24 / (newVal[1] || 1))),$scope.data.KNT_JOB_CRD_H_ID);
                            }
                        }
                    });

                    $scope.$watch('data.START_DT', function (newVal, oldVal) {
                        if (newVal && oldVal) {
                            
                            if ($scope.data.ASIGN_QTY > 0 && $scope.data.TG_D_PROD_QTY > 0 && $scope.data.DURATION > 0) {
                                $scope.data['END_DT'] = new DayPilot.Date(newVal).addHours($scope.data.DURATION).toStringSortable();
                            }
                        }
                    });

                    $scope.onMachineChange = function (e) {
                        if (e.sender.dataItem(e.sender.item).KNT_MACHINE_ID && ($scope.data.TG_D_PROD_QTY || 0) > 0 && ($scope.data.ASIGN_QTY || 0) > 0) {
                            $scope.data['DURATION'] = Math.ceil($scope.data.ASIGN_QTY * (24 / ($scope.data.TG_D_PROD_QTY || 1)));
                            setSchedule(e.sender.dataItem(e.sender.item).KNT_MACHINE_ID, $scope.data.DURATION);
                        }
                    };

                    KnittingDataService.getDataByUrl('/KnitPlan/getKnitMachine?pACT_MC_DIA_ID=' + kcData.ACT_MC_DIA_ID).then(function (res) {
                            $scope.KnitMachineListDs = new kendo.data.DataSource({
                                data: res
                            });
                    });
                    

                    $scope.data = kcData;
                    $scope.save = function (data, isValid) {

                        if (!isValid) {
                            return;
                        }

                        var data = {
                            KNT_JOB_CRD_H_ID: data.KNT_JOB_CRD_H_ID,
                            ASIGN_QTY: data.ASIGN_QTY,
                            TG_D_PROD_QTY: data.TG_D_PROD_QTY,
                            START_DT: (data.START_DT instanceof Date) ? $filter('date')(data.START_DT, 'yyyy-MM-dd HH:mm:ss Z') : data.START_DT,
                            END_DT:  (data.END_DT instanceof Date) ? $filter('date')(data.END_DT, 'yyyy-MM-dd HH:mm:ss Z') : data.END_DT,
                            KNT_MACHINE_ID: data.KNT_MACHINE_ID
                        }

                        return KnittingDataService.saveDataByUrl(data, '/KnitPlan/UpdateJobCardData').then(function (res) {
                            if (res.success === false) {
                                vm.errors = res.errors;
                            }
                            else {
                                res['data'] = angular.fromJson(res.jsonStr);

                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    modalInstance.close({ success: true });
                                }
                                config.appToastMsg(res.data.PMSG);
                            }
                        }).catch(function (message) {
                            exception.catcher('XHR loading Failded')(message);
                        });

                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                resolve: {
                    kcData: function () {
                        return KnittingDataService.getDataByUrl('/KnitPlan/getLoadPlanEditKc?pKNT_JOB_CRD_H_ID=' + source.data.id);
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {
                if (data.success) {
                    loadEvents();
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        function navigateKnitCardList(data) {    
            var url = '/Knitting/Knit/KnitPlan/#/JobCard?pKNT_PLAN_H_ID=' + data.KNT_PLAN_H_ID;
            var a = document.createElement('a');
            a.href = url;
            a.target = '_blank';
            document.body.appendChild(a);
            a.click();
        }

        var draft_menu = new DayPilot.Menu({
            items:
                [
                    {
                        text: "Tuning", onclick: function () {
                            TuningPlanFn(this.source.data.GMT_PLN_LINE_LOAD_ID);
                        }
                    },
                    {
                        text: "Allocation Re-arrange", onclick: function () {
                            openManualLineLoadingModal(this.source.data);
                        }
                    },


                    {
                        text: "Auto Fit (End Date)", onclick: function () {
                            AutoFitEndDate(this.source, 'Auto Fiting End Date Allocation...');
                        }
                    },
                    {
                        text: "Pull to Previous Loading", onclick: function () {
                            Move2Next(this.source, 1003, 'Pull to Previous Loading...');
                        }
                    },
                    {
                        text: "Auto Fit (Start Date)", onclick: function () {
                            AutoFitStartDate(this.source, 'Auto Fiting Start Date Allocation...');
                        }
                    },

                    {
                        text: "Pull to Next Loading", onclick: function () {
                            Move2Next(this.source, 1007, 'Pull to Next Loading...');
                        }
                    },

                    {
                        text: "Plan Time Offset", onclick: function () {
                            LineLoadEventOffset(this.source.data.GMT_PLN_LINE_LOAD_ID);
                        }
                    },
                    {
                        text: "Move to End", onclick: function () {
                            Move2Next(this.source, 1002, 'Moving to end.');
                        }
                    },
                    {
                        text: "Mark as Finalized", onclick: function () {
                            Move2Next(this.source, 1005, 'Marking as Finalized');
                        }
                    },
                    {
                        text: "Time & Action Calendar", onclick: function () {
                            openTNATasks(this.source);
                        }
                    },
                    {
                        text: "Production Monitoring", onclick: function () {
                            OpenProdMonitoringModal(this.source.data.MC_ORDER_ITEM_PLN_ID);
                        }
                    },
                ]
        });
        var finalized_menu = new DayPilot.Menu({
            items:
                [
                    {
                        text: "Tuning", onclick: function () {
                            TuningPlanFn(this.source.data.GMT_PLN_LINE_LOAD_ID);
                        }
                    },
               
                    {
                        text: "Mark as Plan Change", onclick: function () {
                            PlanChangeModal(this.source.data.GMT_PLN_LINE_LOAD_ID);
                        }
                    },
                    {
                        text: "Time & Action Calendar", onclick: function () {
                            openTNATasks(this.source);
                        }
                    },
                    {
                        text: "Production Monitoring", onclick: function () {
                            OpenProdMonitoringModal(this.source.data.MC_ORDER_ITEM_PLN_ID);
                        }
                    },
                ]
        });

        var running_menu = new DayPilot.Menu({
            items:
                [
                    {
                        text: "Tuning", onclick: function () {
                            TuningPlanFn(this.source.data.GMT_PLN_LINE_LOAD_ID);
                        }
                    },
                    {
                        text: "Allocation Re-arrange", onclick: function () {
                            openManualLineLoadingModal(this.source.data);
                        }
                    },

                    {
                        text: "Auto Fit (End Date)", onclick: function () {
                            AutoFitEndDate(this.source, 'Auto Fiting End Date Allocation...');
                        }
                    },

                    
                    {
                        text: "Time & Action Calendar", onclick: function () {
                            openTNATasks(this.source);
                        }
                    },
                    {
                        text: "Production Monitoring", onclick: function () {
                            OpenProdMonitoringModal(this.source.data.MC_ORDER_ITEM_PLN_ID);
                        }
                    }
                ]
        });

        var closed_menu = new DayPilot.Menu({
            items:
                [
                    
                ]
        });
        var hold_menu = new DayPilot.Menu({
            items:
                [
                    {
                        text: "Tuning", onclick: function () {
                            TuningPlanFn(this.source.data.GMT_PLN_LINE_LOAD_ID);
                        }
                    },
                    {
                        text: "Allocation Re-arrange", onclick: function () {
                            openManualLineLoadingModal(this.source.data);
                        }
                    },
                     {
                         text: "Auto Fit (End Date)", onclick: function () {
                             AutoFitEndDate(this.source, 'Auto Fiting End Date Allocation...');
                         }
                     },
                    {
                        text: "Pull to Previous Loading", onclick: function () {
                            Move2Next(this.source, 1003, 'Pull to Previous Loading...');
                        }
                    },
                    {
                        text: "Auto Fit (Start Date)", onclick: function () {
                            AutoFitStartDate(this.source, 'Auto Fiting Start Date Allocation...');
                        }
                    },

                    {
                        text: "Pull to Next Loading", onclick: function () {
                            Move2Next(this.source, 1007, 'Pull to Next Loading...');
                        }
                    },


                    {
                        text: "Move to End", onclick: function () {
                            Move2Next(this.source, 1002, 'Moving to end.');
                        }
                    },

                    {
                        text: "Mark as Finalized", onclick: function () {
                            Move2Next(this.source, 1005, 'Marking as Finalized');
                        }
                    },
                    {
                        text: "Time & Action Calendar", onclick: function () {
                            openTNATasks(this.source);
                        }
                    },
                    {
                        text: "Production Monitoring", onclick: function () {
                            OpenProdMonitoringModal(this.source.data.MC_ORDER_ITEM_PLN_ID);
                        }
                    },
                ]
        });

        vm.onOrderSearch = function (MC_ORDER_SHIP_ID) {
            var url = '/GmtLineLoad/getResourceData';
            url += '?pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID || '');
            url += '&pHR_OFFICE_ID=' + (vm.form.HR_OFFICE_ID || '');
            url += '&pHR_PROD_FLR_ID=' + (vm.form.HR_PROD_FLR_ID || '');
            url += '&pHR_PROD_LINE_ID=' + (vm.form.HR_PROD_LINE_ID || '');
            url += '&pMC_ORDER_SHIP_ID=' + (vm.form.MC_ORDER_SHIP_ID || '');

            url += '&pSTART_DT=' + ($scope.scheduler.visibleStart() || '');
            url += '&pEND_DT=' + ($scope.scheduler.visibleEnd() || '');
            url += '&pRESOURCES'
            $scope.showSplash = true;
            return PlanningDataService.getDataByUrl(url).then(function (res) {
                $scope.schedulerConfig.resources = res;
                $scope.schedulerConfig.visible = true;
                return PlanningDataService.getDataByUrl('/GmtLineLoad/getEventData?pSTART_DT=' + $scope.scheduler.visibleStart() + '&pEND_DT=' + $scope.scheduler.visibleEnd() + '&pRESOURCES&pMC_ORDER_SHIP_ID=' + (vm.form.MC_ORDER_SHIP_ID || '')).then(function (res) {
                    $scope.schedulerConfig.events = res;
                    $scope.showSplash = false;
                });
            });
        }


        vm.onChangeOrderShipData = function (e) {
            var itm = e.sender.dataItem(e.sender.item);
            if (!itm.MC_ORDER_SHIP_ID) {
                loadResources(vm.form.HR_COMPANY_ID, vm.form.HR_OFFICE_ID, vm.form.HR_PROD_FLR_ID, vm.form.HR_PROD_LINE_ID);
            }
        }

        $scope.schedulerConfig = {
            visible: false, // will be displayed after loading the resources
            scale: "Manual",
            cellWidth: 40,
            cellWidthSpec: 'Fixed',
            cellWidthMin : 15,
            cellDuration: 60,
            rowHeaderWidth: 200,
            rowHeaderScrolling: true,
            heightSpec: "Max",
            height: 550,
            weekStarts: 6,
            timeline: [],
            //timeHeaders: getTimeHeaders(),
            //businessBeginsHour : 8,
            //businessEndsHour : 20,
            //showNonBusiness : false,
            rowDoubleClickHandling: "Enabled",
            bubble: new DayPilot.Bubble(),

            eventMoveVerticalEnabled: false,
            rowMarginBottom: 6,
            allowEventOverlap : true,
            allowMultiSelect: true,
            multiSelectRectangle: "Free",
            eventClickHandling: "Select", //Select/Bubble
            
            eventHoverHandling: "Disabled",
            eventDoubleClickHandling: "Bubble",


            eventMovingStartEndEnabled: true,
            eventMovingStartEndFormat: "MMM d,hh tt",
            eventResizingStartEndEnabled: true,
            eventResizingStartEndFormat: "MMM d,hh tt",

            useEventBoxes: "Never",//'Always','ShortEventsOnly','Never';
            //floatingEvents : true,

            //Moving
            moveBy: 'Full',
            allowMultiMove: false,
            //eventMoveHandling: 'Update',
            //multiMoveVerticalMode: "Disabled",


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
            separators : [
                { color: "Red", location: new DayPilot.Date(), layer: "BelowEvents" }
            ],
            treePreventParentUsage: true,
            //scrollDelayEvents: 3000,
            //heightSpec : "Max",
            treeEnabled: true,
            dynamicLoading: true,
            scrollDelayDynamic : 500, 
            onScroll: function (args) {
                var strt = args.viewport.start.toString();
                var end = args.viewport.end.toString();
                var resources = args.viewport.resources.filter(function (x) { return x < 10000 }).join(',');

                args.async = true;

                var url = '/GmtLineLoad/getResourceData';
                url += '?pHR_COMPANY_ID=' + (vm.form.HR_COMPANY_ID || '');
                url += '&pHR_OFFICE_ID=' + (vm.form.HR_OFFICE_ID || '');
                url += '&pHR_PROD_FLR_ID=' + (vm.form.HR_PROD_FLR_ID || '');
                url += '&pHR_PROD_LINE_ID=' + (vm.form.HR_PROD_LINE_ID || '');
                url += '&pMC_ORDER_SHIP_ID=' + (vm.form.MC_ORDER_SHIP_ID || '');

                url += '&pSTART_DT=' + ($scope.scheduler.visibleStart() || '');
                url += '&pEND_DT=' + ($scope.scheduler.visibleEnd() || '');
                url += '&pRESOURCES=' + (resources || '');

                return PlanningDataService.getDataByUrl('/GmtLineLoad/getEventData?pSTART_DT=' + strt + '&pEND_DT=' + end + '&pRESOURCES=' + resources + '&pMC_ORDER_SHIP_ID=' + (vm.form.MC_ORDER_SHIP_ID || '') + '&pIS_REGULAR_VIEW=' + vm.form.IS_REGULAR_VIEW + '&pIS_GMT_ITEM_VIEW=' + vm.form.IS_GMT_ITEM_VIEW).then(function (res) {
                    args.events = res;
                    args.loaded();
                });


                //return PlanningDataService.getDataByUrl(url).then(function (res) {
                //    $scope.schedulerConfig.resources = res;
                //    $scope.schedulerConfig.visible = true;
                //    return PlanningDataService.getDataByUrl('/GmtLineLoad/getEventData?pSTART_DT=' + strt + '&pEND_DT=' + end + '&pRESOURCES=' + resources + '&pMC_ORDER_SHIP_ID=' + (vm.form.MC_ORDER_SHIP_ID || '')).then(function (res) {
                //        args.events = res;
                //        args.loaded();
                //    });


                //});
               

            },
            contextMenuResource : new DayPilot.Menu({items: [
                  {
                      text: '+ Manual Line Load', onclick: function () {
                          var e = this.source;
                          console.log(e);
                          //var svData = {};
                          //var data = [];
                          //console.log(e);
     
                          //svData['RF_LOCATION_ID'] = e.data.RF_LOCATION_ID;
                          ////svData['KNT_MACHINE_ID'] = e.data.id;


                          //if (e.data.id < 0) {
                          //    e.data.children.forEach(function (itm) {
                          //        dp.rows.find(itm.id).events.all().forEach(function (item) {
                          //            data.push(
                          //                  {
                          //                      KNT_MACHINE_ID: item.data.resource,
                          //                      KNT_JOB_CRD_H_ID: item.data.id,
                          //                      START_DT: item.data.start,
                          //                      END_DT: item.data.end

                          //                  }
                          //              );
                          //        })
                          //    });
                            


                          //} else {
                          //    e.events.all().forEach(function (itm) {
                          //        data.push(
                          //                {
                          //                    KNT_MACHINE_ID: itm.data.resource,
                          //                    KNT_JOB_CRD_H_ID: itm.data.id,
                          //                    START_DT: itm.data.start,
                          //                    END_DT: itm.data.end

                          //                }
                          //            )

                          //    });
                          //}
                         
                          //svData['XML'] = KnittingDataService.xmlStringShort(data);
                          //return KnittingDataService.saveDataByUrl(svData, '/McLoadPlan/removeEmptyCell').then(function (res) {
                          //    if (res.success === false) {
                          //        vm.errors = res.errors;
                          //    }
                          //    else {
                          //        res['data'] = angular.fromJson(res.jsonStr);

                          //        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                          //            loadEvents();
                          //        } else {
                          //            config.appToastMsg(res.data.PMSG);
                          //        }
                          //    }
                          //})
                      }
                  }
            ]}),
            onRowFilter : function(args) {
                if (args.row.name.toUpperCase().indexOf(args.filter.toUpperCase()) === -1) {
                    args.visible = false;
                }
            },
            onEventFilter: function (args) {
                if (args.e.text().toUpperCase().indexOf(args.filter.toUpperCase()) === -1) {
                    args.visible = false;
                }
            },

            eventHeight: 50,
            rowHeaderColumns: [
                {title: "Line", width: 70},
                { title: "MC", width: 20 },
                { title: "-", width: 5 },
                { title: "Best Fit", width: 100 },
              //  { title: "Criticality", width: 100 }
                

            ],

            onBeforeResHeaderRender: function(args) {
                args.resource.columns[0].html = args.resource.TTL_ACTV_POINT;
                args.resource.columns[1].backColor = args.resource.LINE_LBL_COLOR;
                args.resource.columns[2].html = args.resource.BEST_FIT_ITEM;
               // args.resource.columns[3].html = args.resource.LINE_CRITICALITY;
              
            },
            //onEventMoving: function (args) {

            //    var row = dp.rows.find(args.resource);
            //    var holidays = row.data.holidays;
            //    if (holidays.length == 0) {
            //        return;
            //    }
            //    var itemStart = holidays.some(function (range) {
            //        return (new DayPilot.Date(range.start) <= args.start) && (new DayPilot.Date(range.end) >= args.start);
            //    });

            //    var itemEnd = holidays.some(function (range) {
            //        return (new DayPilot.Date(range.start) <= args.end) && (new DayPilot.Date(range.end) >= args.end);
            //    });

            //    if (itemStart) {
            //        args.left.enabled = false;
            //        args.right.html = "<p style='color:red;'>Sewing can'nt be started on Weekend/Holyday </p>";
            //        args.allowed = false;

            //    }
            //    if (itemEnd) {
            //        args.right.enabled = false;
            //        args.left.html = "<p style='color:red;'>Sewing can'nt be ended on Weekend/Holyday </p>";
            //        args.allowed = false;
            //    }
            //},
            onEventMoved: function (args) {
                $scope.showSplash = true;
                return PlanningDataService.saveDataByUrl({
                    GMT_PLN_LINE_LOAD_ID: args.e.data.GMT_PLN_LINE_LOAD_ID,
                    HR_PROD_LINE_ID: args.newResource,
                    SEW_START_DT: args.newStart.value,
                    SEW_END_DT: args.newEnd.value
                }, '/GmtLineLoad/updateEvent').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $scope.showSplash = false;
                            dp.update();
                        } else {
                            config.appToastMsg(res.data.PMSG);
                        }
                    }
                })
            },

            onRowDoubleClick :function(args){
                dp.multiselect.clear();
            },
            onAfterRender : function(args) {
               console.log("onAfterRender: " + args.isScroll);
            },
            //onEventResizing: function (args) {

                //var row = dp.rows.find(args.e.data.HR_PROD_LINE_ID);
                //var holidays = row.data.holidays;
                //if (holidays.length == 0) {
                //    return;
                //}
                //var itemStart = holidays.some(function (range) {
                //    return (new DayPilot.Date(range.start) <= args.start) && (new DayPilot.Date(range.end) >= args.start);
                //});

                //var itemEnd= holidays.some(function (range) {
                //    return (new DayPilot.Date(range.start) <= args.end) && (new DayPilot.Date(range.end) >= args.end);
                //});

                //if (itemStart) {
                //    args.left.enabled = false;
                //    args.right.html = "<p style='color:red;'>Sewing can'nt be started on Weekend/Holyday </p>";
                //    args.allowed = false;

                //}
                //if (itemEnd) {
                //    args.right.enabled = false;
                //    args.left.html = "<p style='color:red;'>Sewing can'nt be started on Weekend/Holyday </p>";
                //    args.allowed = false;
                //}

                    //args.left.enabled = false;
                    //args.right.enabled = true;
                    //console.log(args.duration.totalHours());
            //},

            onBeforeCellRender: function (args) {
                if (!dp) {
                    return;
                }
                if (args.cell.resource > 10000) {
                    var row = dp.rows.find(args.cell.resource);
                    var mc_utis = row.data.mc_utis;

                    if (!mc_utis || mc_utis.length == 0) {
                        return;
                    }

                    var mc_uti = mc_utis.find(function (range) {
                        var start = new DayPilot.Date(range.start);
                        var end = new DayPilot.Date(range.end);
                        return DayPilot.Util.overlaps(start, end, args.cell.start, args.cell.end);
                    });
                    if (mc_uti) {
                        var str = (mc_uti.TTL_ACTV_POINT - mc_uti.PLAN_OP) > 0 ? "+" + (mc_uti.TTL_ACTV_POINT - mc_uti.PLAN_OP) : (mc_uti.TTL_ACTV_POINT - mc_uti.PLAN_OP);
                        args.cell.html = "<div title='" + str + "'  style='position:absolute;right:1px;bottom:1px;font-size:8pt;color:#581845;'>" + mc_uti.PLAN_OP + "</div>";
                        args.cell.backColor =
                            ((mc_uti.PLAN_OP / mc_uti.TTL_ACTV_POINT) <= 0.85) || ((mc_uti.PLAN_OP / mc_uti.TTL_ACTV_POINT) > 1.15) ? "#F9EECC" : "#f3f3f3";
                    } else {
                        args.cell.backColor = "#f3f3f3";
                    }
                } else {
                    var row = dp.rows.find(args.cell.resource);
                    var holidays = row.data.holidays;
                    var merged_days = row.data.merged_days;
                    if (holidays.length == 0 && merged_days.length ==0) {
                        return;
                    }
                    var item = holidays.find(function (range) {
                        var start = new DayPilot.Date(range.start);
                        var end = new DayPilot.Date(range.end);
                        return DayPilot.Util.overlaps(start, end, args.cell.start, args.cell.end);
                    });

                    var mrg = merged_days.find(function (range) {
                        var start_mrg = new DayPilot.Date(range.start);
                        var end_mrg = new DayPilot.Date(range.end);
                        return DayPilot.Util.overlaps(start_mrg, end_mrg, args.cell.start, args.cell.end);
                    });

                    if (item) {
                        args.cell.backColor = item.backColor;
                        args.cell.html = "<div style='position:absolute;right:1px;bottom:1px;font-size:7pt;color:#666;'>" + item.DAY_TYPE + "</div>";
                        args.cell.properties['aminul'] = 'N/A';
                        args.cell.properties['backColor'] = item.backColor;       
                    }

                    if (mrg) {
                        args.cell.backColor = mrg.backColor;
                        args.cell.html = "<div style='position:absolute;right:1px;bottom:1px;font-size:7pt;color:#666;'>" + mrg.DAY_TYPE + "</div>";
                        args.cell.properties['aminul'] = 'N/A';
                        args.cell.properties['backColor'] = mrg.backColor;
                    }


                       
                    //} else {
                    //    args.cell.backColor = '#ffffff';
                    //    args.cell.properties['aminul'] = true;
                    //    args.cell.properties['backColor'] = 'N/A';
                    //}
                }




            },

            onEventClick: function (args) {

                if (args.ctrl || args.shift) {
                    if(_.some(dp.multiselect.events(), function (o) {
                        return o.data.id == args.e.data.id;
                    })) {
                        dp.multiselect.remove(args.e,false);
                    } else {
                        dp.multiselect.add(args.e);
                    }
                    args.preventDefault();
                }
                console.log(dp.multiselect);
            },
            onEventSelect: function (args) {
                dp.multiselect.add(args.e);
            },
            onEventResized: function (args) {
                $scope.showSplash = true;
                return PlanningDataService.saveDataByUrl({
                    GMT_PLN_LINE_LOAD_ID: args.e.data.GMT_PLN_LINE_LOAD_ID,
                    HR_PROD_LINE_ID: args.e.data.HR_PROD_LINE_ID,
                    SEW_START_DT: args.newStart.value,
                    SEW_END_DT: args.newEnd.value
                }, '/GmtLineLoad/updateEvent').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            dp.update();
                            $scope.showSplash = false;
                        } else {
                            config.appToastMsg(res.data.PMSG);
                        }
                    }
                })


            },
            onBeforeEventRender: function(args) {
                var start = new DayPilot.Date(args.data.start);
                var end = new DayPilot.Date(args.data.end);

                var now = new DayPilot.Date();
                var today = new DayPilot.Date().getDatePart();
                var status = args.e.status;
                var bubbleHtml = '';
                var row = dp.rows.find(args.e.resource);
                var pct_completion = parseFloat(((args.e.TOT_PROD / args.e.ALLOCATED_QTY) * 100).toFixed(2));
                var pct_completion_op = parseFloat(((args.e.INITIAL_PROD_QTY / args.e.ALLOCATED_QTY) * 100).toFixed(2));

                if (row) {
                    var diff = (row.data.TTL_ACTV_POINT || 0) + args.e.MRG_ACTV_POINT - args.e.PLAN_OP;
                } else {
                    var diff = 0;
                }

                args.data.html = "<div style='color:" + args.e.fontColor + "; font-size: 8pt;font-weight:900'><b>" + args.e.ORDER_NO + ":" + args.e.BYR_ACC_NAME_EN + ":" + new DayPilot.Date(args.e.SHIP_DT).toString("d/MMM/yy") + "</b></div>"
                //args.data.text + " (" + start.toString("M/d") + " - " + end.toString("M/d") + ")";

                //+ "<br /><span style='color:gray'>" + status + "</span>"
               
                bubbleHtml += "<img src='data:image/png;base64, " + args.e.STYL_KEY_IMG + "' alt='No Photo' style='border:1px solid black;width:200px; height:100px'/>";
                bubbleHtml += "<div style='color:" + args.e.fontColor + "; font-size: 10pt;font-weight:900'><b>" + args.e.ORDER_NO + "(" + args.e.STYLE_NO + "):" + args.e.BYR_ACC_NAME_EN + ":" + new DayPilot.Date(args.e.SHIP_DT).toString("d/MMM/yy") + "</b></div>";
                bubbleHtml += "<div>Loaded : <b>" + new DayPilot.Date(args.e.start).toString("d/MMM/yy hh:mm tt") + " to " + new DayPilot.Date(args.e.end).toString("d/MMM/yy hh:mm tt") + "</b></div>";
                //bubbleHtml += "<div>Inital Qty : <b>0</b></div>";
                bubbleHtml += "<div>Allocated: <b>" + args.e.ALLOCATED_QTY + "</b> Production(Overall): <b>" + args.e.TOT_PROD + " (" + pct_completion + "%)</b> </div>";
                bubbleHtml += "<div>Production(OutOfPlan): <b>" + args.e.INITIAL_PROD_QTY + " (" + pct_completion_op + "%)</b></div>";

                bubbleHtml += "<div>" + args.e.ITEM_NAME_EN + "=> OP: <b>" + args.e.PLAN_OP + "</b> ASO: <b>" + args.e.PLAN_HP + "</b> WH:<b>" + args.e.PLAN_WH + "</b>SMV: <b> " + args.e.SMV + "</b></div>";
                bubbleHtml += "<div>----------------------------------------------</div>";
                bubbleHtml += "<div style='color:" + args.e.RGB_COL_CODE + ";'>Status => <b>" + args.e.PLN_STRIPE_PHASE_NAME + "</b></div>";

                if (diff >= 0 && diff <= 2) {
                    bubbleHtml += "<div><b style='color:#377723;'>Machine=> M[+" + diff + "]</b></div>";
                } else if (diff < 0) {
                    bubbleHtml += "<div><b style='color:#FF6726;'>Machine=> M[" + diff + "]</b></div>";
                } else if (diff > 2) {
                    bubbleHtml += "<div><b style='color:#FF6726;'>Machine=> M[+" + diff + "]</b></div>";
                }

                for (var i = 0; i < args.e.sts.length; i++) {
                 
                    bubbleHtml += "<div><b style='color:" + args.e.sts[i].STS_COLOR_CODE + ";'>" + args.e.sts[i].PLN_STS_CODE + "=> " + args.e.sts[i].PLN_STS_NAME + " </b> (" + args.e.sts[i].PLN_STS_AREA_NAME + ")</div>";
                }
                args.e.bubbleHtml = bubbleHtml;
                var paidColor = "#0B0049";
                var stsHtml = '';

                if (diff >= 0 && diff <= 2) {
                    stsHtml += "<b style='color:#377723;'>M[+" + diff + "]</b>";
                } else if (diff < 0) {
                    stsHtml += "<b style='color:#FF6726;'>M[" + diff + "]</b>";
                } else if (diff > 2) {
                    stsHtml += "<b style='color:#FF6726;'>M[+" + diff + "]</b>";
                }

                for (var i = 0; i < args.e.sts.length; i++) {
                    stsHtml += "<b style='color:" + args.e.sts[i].STS_COLOR_CODE + ";'>" + args.e.sts[i].PLN_STS_P_CODE + " </b>";
                }


                switch(args.e.GMT_PLN_STRIPE_PHASE_ID){
                    case 1: //Draft
                        args.e.contextMenu = draft_menu;
                        args.data.areas = [
                                        { bottom: 22, left: 4, html: "<div style='color:" + args.e.fontColor + "; font-size: 8pt;'><b>" + args.e.STYLE_NO + "|</b>" + args.e.ITEM_NAME_EN + " </div>", v: "Visible", action: "Move" },
                                        { bottom: 11, left: 4, html: "<div style='color:" + args.e.barColor + "; font-size: 6pt;'>" + stsHtml + "</div>", v: "Visible", action: "Move" },
                                        { bottom: 3, right: 0, html: "<div style='color:" + paidColor + "; font-size: 8pt;'>(" + args.e.ALLOCATED_QTY + "/" + args.e.ORDER_QTY + ")|" + args.e.TOT_PROD + "|" + pct_completion + "%</div>", v: "Visible", action: "ResizeEnd" },
                                        { left: 15, bottom: 3, right: 20, height: 3, html: "<div style='background-color: #00198C; height: 100%; width:" + pct_completion + "%'></div>" }
                         ];

                        break;
                    case 2: //Finalized
                        args.e.contextMenu = finalized_menu;
                        args.data.areas = [
                                        { bottom: 22, left: 4, html: "<div style='color:" + args.e.fontColor + "; font-size: 8pt;'><b>" + args.e.STYLE_NO + "|</b>" + args.e.ITEM_NAME_EN + " </div>", v: "Visible", action: "None" },
                                        { bottom: 11, left: 4, html: "<div style='color:" + args.e.barColor + "; font-size: 6pt;'>" + stsHtml + "</div>", v: "Visible", action: "None" },
                                        { bottom: 3, right: 0, html: "<div style='color:" + paidColor + "; font-size: 8pt;'>(" + args.e.ALLOCATED_QTY + "/" + args.e.ORDER_QTY + ")|" + args.e.TOT_PROD + "|" + pct_completion + "%</div>", v: "Visible", action: "None" },
                                        { left: 15, bottom: 3, right: 20, height: 3, html: "<div style='background-color: #00198C; height: 100%; width:" + pct_completion + "%'></div>" }
                        ];

                        break;
                    case 3:// Running
                        args.e.contextMenu = running_menu;
                        args.data.areas = [
                                   { bottom: 22, left: 4, html: "<div style='color:" + args.e.fontColor + "; font-size: 8pt;'><b>" + args.e.STYLE_NO + "|</b>" + args.e.ITEM_NAME_EN + " </div>", v: "Visible", action: "None" },
                                   { bottom: 11, left: 4, html: "<div style='color:" + args.e.barColor + "; font-size: 6pt;'>" + stsHtml + "</div>", v: "Visible", action: "None" },
                                   { bottom: 3, right: 0, html: "<div style='color:" + paidColor + "; font-size: 8pt;'>(" + args.e.ALLOCATED_QTY + "/" + args.e.ORDER_QTY + ")|" + args.e.TOT_PROD + "|" + pct_completion + "%</div>", v: "Visible", action: "ResizeEnd" },
                                   { left: 15, bottom: 3, right: 20, height: 3, html: "<div style='background-color: #00198C; height: 100%; width:" + pct_completion + "%'></div>" }
                        ];
                        break;
                    case 4:// Hold
                        args.e.contextMenu = hold_menu;
                        args.data.areas = [
                                        { bottom: 22, left: 4, html: "<div style='color:" + args.e.fontColor + "; font-size: 8pt;'><b>" + args.e.STYLE_NO + "|</b>" + args.e.ITEM_NAME_EN + " </div>", v: "Visible", action: "Move" },
                                        { bottom: 11, left: 4, html: "<div style='color:" + args.e.barColor + "; font-size: 6pt;'>" + stsHtml + "</div>", v: "Visible", action: "Move" },
                                        { bottom: 3, right: 0, html: "<div style='color:" + paidColor + "; font-size: 8pt;'>(" + args.e.ALLOCATED_QTY + "/" + args.e.ORDER_QTY + ")|" + args.e.TOT_PROD + "|" + pct_completion + "%</div>", v: "Visible", action: "ResizeEnd" },
                                        { left: 15, bottom: 3, right: 20, height: 3, html: "<div style='background-color: #00198C; height: 100%; width:" + pct_completion + "%'></div>" }
                        ];

                        break;
                    case 4:// Closed
                        args.e.contextMenu = closed_menu;
                        args.data.areas = [
                                       { bottom: 22, left: 4, html: "<div style='color:" + args.e.fontColor + "; font-size: 8pt;'><b>" + args.e.STYLE_NO + "|</b>" + args.e.ITEM_NAME_EN + " </div>", v: "Visible", action: "None" },
                                       { bottom: 11, left: 4, html: "<div style='color:" + args.e.barColor + "; font-size: 6pt;'>" + stsHtml + "</div>", v: "Visible", action: "None" },
                                       { bottom: 3, right: 0, html: "<div style='color:" + paidColor + "; font-size: 8pt;'>(" + args.e.ALLOCATED_QTY + "/" + args.e.ORDER_QTY + ")|" + args.e.TOT_PROD + "|" + pct_completion + "%</div>", v: "Visible", action: "None" },
                                       { left: 15, bottom: 3, right: 20, height: 3, html: "<div style='background-color: #00198C; height: 100%; width:" + pct_completion + "%'></div>" }
                        ];

                        break;
                }


            },
            onBeforeTimeHeaderRender: function (args) {
                if ($scope.scale === '1H') {
                    if (args.header.level == 3) {
                        args.header.html = args.header.html.replace(" AM", "a").replace(" PM", "p");
                    };
                }
                if ($scope.scale === '1D') {
                    if (args.header.level == 1) {
                        args.header.html = 'Week-' + args.header.start.weekNumber();
                    };
                }
                if ($scope.scale === '1W') {
                    if (args.header.level == 1) {
                        args.header.html = 'W-' + args.header.start.weekNumber();
                    };
                }

            },

        };



        $timeout(function () {
            dp = $scope.scheduler;  // debug

            $scope.schedulerConfig.timeline = getTimeline($scope.scheduler.getViewPort().start);
            $scope.schedulerConfig.timeHeaders = getTimeHeaders();
            loadResources(vm.form.HR_COMPANY_ID, vm.form.HR_OFFICE_ID, vm.form.HR_PROD_FLR_ID, vm.form.HR_PROD_LINE_ID);
            $scope.schedulerConfig.scrollToAnimated = "fast";
            $scope.schedulerConfig.scrollTo = $scope.scheduler.getViewPort().start;



           
        });

        //function loadEvents() {
        //    var pSTART_DT = $scope.scheduler.getViewPort().start.value;
        //    var pEND_DT = $scope.scheduler.getViewPort().end.value;
        //    var pRESOURCES= $scope.scheduler.getViewPort().resources.filter(function (x) { return x < 10000 }).join(',');

        //    return PlanningDataService.getDataByUrl('/GmtLineLoad/getEventData?pSTART_DT=' + pSTART_DT + '&pEND_DT=' + pEND_DT + '&pRESOURCES=' + pRESOURCES).then(function (res) {
        //        $scope.schedulerConfig.events = res;
        //    });
        //};

        function getTimeline(date) {
            var date = date || DayPilot.Date.today().addDays(-2);
            var start = new DayPilot.Date(date).getDatePart();

            var timeline = [];
            var begin, end, increment;
            switch ($scope.scale) {
                case "1H":
                    begin = 8;
                    end = 32;
                    increment = 1;
                    for (var j = 0; j < 1; j++) {

                        var strt = start.addMonths(j);
                        var days = strt.daysInMonth();

                        for (var i = 0; i < days; i++) {

                            var day = strt.addDays(i);
                            for (var x = begin; x < end; x += increment) {
                                timeline.push({ start: day.addHours(x), end: day.addHours(x + increment) });
                            }
                        }
                    }

                    break;
                case "1D":
                    for (var j = 0; j < $scope.TimeRange; j++) {
                        var strt = start.addMonths(j);
                        var days = strt.daysInMonth();
                        for (var i = 0; i < days; i++) {
                            var day = strt.addDays(i);
                            timeline.push({ start: day.addHours(8), end: day.addHours(21) });
                        }
                    }
                    break;
                case "1W":
                    var day = new DayPilot.Date(date).firstDayOfMonth().firstDayOfWeek().addDays(-1);
           
                    for (var j = 0; j < (($scope.TimeRange+2) * 30) ; j += 7) {
                        timeline.push({ start: day.addDays(j), end: day.addDays(j + 7) });
                    }
                    break;
                default:
                    throw "Invalid scale value";
            }
            return timeline;
        }
        function getTimeHeaders() {
            switch ($scope.scale) {
                case "1H":
                return [{ groupBy: "Month", format: "MMMM yyyy" },{ groupBy: 'Week' },{ groupBy: "Day", format: "d" }, { groupBy: "Cell", format: "h tt" }];
                    break;
                case "1D":
                    return [{ groupBy: "Month", format: "MMMM yyyy" }, { groupBy: 'Week' },{ groupBy: "Day", format: "d" }];
                    break;
                case "1W":
                    return [{ groupBy: "Month", format: "MMMM yyyy" }, { groupBy: 'Week' }];
                    break;
            }
        }
    }

})();