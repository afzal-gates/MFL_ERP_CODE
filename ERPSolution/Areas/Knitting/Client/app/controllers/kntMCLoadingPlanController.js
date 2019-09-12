/// <reference path="kntMCLoadingPlanController.js" />
(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KntMCLoadingPlanController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', '$timeout', '$modal', '$window', '$filter', KntMCLoadingPlanController]);
    function KntMCLoadingPlanController($q, config, KnittingDataService, $stateParams, $state, $scope, $timeout, $modal, $window, $filter) {

        var vm = this;
        var dp;
        var floor;
        var switchShift = true;
        vm.params = $stateParams;
        vm.form = {};

        $scope.TimeRange = 2;
        $scope.scale = "shifts";
        //$scope.HR_COMPANY_ID = 1;

        vm.form['HR_COMPANY_ID'] = $stateParams.pHR_COMPANY_ID;
        vm.form['HR_PROD_BLDNG_ID'] = $stateParams.pHR_PROD_BLDNG_ID;

        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getCompanyData(), getBuildingData($stateParams.pHR_COMPANY_ID || null), getMcDiaList(), getKnitMachineList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function createYarnRequisition(KNT_JOB_CRD_H_LST, KNT_PLAN_H_LST) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Knitting/Knit/_YarnReqInHouseProd',
                controller: 'YarnReqInHouseProdController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    JobCardList: function () {
                        return KnittingDataService.getDataByUrl('/KnitPlan/getYarnReqInHouseProd?pKNT_JOB_CRD_H_LST=' + KNT_JOB_CRD_H_LST + '&pKNT_PLAN_H_LST=' + KNT_PLAN_H_LST);
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.printRequisitionList = function () {
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-3507' }, {
                FROM_DATE: $filter('date')(new Date(),'dd-MMM-yyyy'),
                TO_DATE: $filter('date')(new Date(), 'dd-MMM-yyyy')
            });

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    input.value = params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        };

        

        function loadResources(pHR_PROD_FLR_ID, pKNT_MACHINE_ID, pHR_PROD_BLDNG_ID, pHR_COMPANY_ID, pKNT_MC_DIA_ID) {

            var url = '/McLoadPlan/loadResourceData?pHR_PROD_FLR_ID=' + pHR_PROD_FLR_ID + '&pKNT_MACHINE_ID=' + pKNT_MACHINE_ID;
               url += '&pHR_PROD_BLDNG_ID=' + pHR_PROD_BLDNG_ID;
               url += '&pHR_COMPANY_ID=' + pHR_COMPANY_ID;
               url += '&pKNT_MC_DIA_ID=' + (pKNT_MC_DIA_ID||null);
            return KnittingDataService.getDataByUrl(url).then(function (res) {
                $scope.schedulerConfig.resources = res;
                if (!pHR_PROD_FLR_ID) {
                    $scope.FloorDataDs = new kendo.data.DataSource({
                        data: res
                    });
                }

                $scope.schedulerConfig.visible = true;
            });
        }


        function getMcDiaList() {
            return KnittingDataService.getDataByUrl('/KnitCommon/getMachineDiaList').then(function (res) {
                vm.machineDiaDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        function getKnitMachineList(ACT_MC_DIA_ID, HR_PROD_FLR_ID,HR_COMPANY_ID) {
            return KnittingDataService.getDataByUrl('/KnitPlan/getKnitMachine?pACT_MC_DIA_ID=' + (ACT_MC_DIA_ID||null) + '&pHR_PROD_FLR_ID=' + (HR_PROD_FLR_ID || null) + '&pHR_COMPANY_ID=' + (HR_COMPANY_ID||null)).then(function (res) {
                $scope.MachineDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        $scope.scaleDs = new kendo.data.DataSource({
            data: [
                { txt: 'Hour', val: 'hours' },
                { txt: 'Shift', val: 'shifts' },
                { txt: 'Day', val: 'day' },
            ]
        });


        $scope.timeRangeDs = new kendo.data.DataSource({
            data: [
                { txt: '1M', val: 1 },
                { txt: '2M', val: 2 },
                { txt: '3M', val: 3 },
            ]
        });

        function getCompanyData() {
            return KnittingDataService.getDataByUrl('/KnitPlan/getCompanyList').then(function (res) {
                $scope.companyDs = new kendo.data.DataSource({
                     data: res
                });
            });
        };



        $scope.$watchGroup(["scale", "TimeRange"], function (newVal, oldVal) {

            if (!oldVal[0] && !oldVal[1]) {
                return;
            }

            $scope.schedulerConfig.timeline = getTimeline($scope.scheduler.getViewPort().start);
            $scope.schedulerConfig.timeHeaders = getTimeHeaders();
            $scope.schedulerConfig.scrollToAnimated = "fast";
            $scope.schedulerConfig.scrollTo = $scope.scheduler.getViewPort().start;  // keep the scrollbar position/by date
        });

        vm.ChangeDataByDate = function (pPROD_DT) {
            var day = pPROD_DT ? new DayPilot.Date(pPROD_DT) : new DayPilot.Date();
            if (!pPROD_DT) {
                $('#start').text(new DayPilot.Date().toString("dd-MMM-yyyy"));
            }
            if ($scope.scheduler.visibleStart().getDatePart() <= day &&  day < $scope.scheduler.visibleEnd()) {
                $scope.scheduler.scrollTo(day, "fast");  // just scroll
            }
            else {
                loadEvents(day);  // reload and scroll
            }
        };
        $window.ChangeDataByDate = vm.ChangeDataByDate;



        $scope.onChangeCellWidth = function (cellwidth) {
            var start = dp.getViewPort().start;
            dp.cellWidth = cellwidth;
            dp.update();
            dp.scrollTo(start);
        },

        $scope.onSearchEvent = function (query) {
            if (query) {
                dp.events.filter(query);
            } else {
                dp.events.filter(null);
            }
        }


        $scope.clearSelection = function () {
            dp.multiselect.clear();
        }


        function getBuildingData(pHR_COMPANY_ID) {

            if (!pHR_COMPANY_ID)
                return;

           return KnittingDataService.getDataByUrl('/McLoadPlan/loadCompanyBuildingData?pHR_COMPANY_ID=' + pHR_COMPANY_ID + '&pLK_PFLR_TYP_ID=527').then(function (res) {
                $scope.buildingds = new kendo.data.DataSource({
                    data: res
                });
            });
        };


        $scope.onChangeCompany = function (e) {
            var item = e.sender.dataItem(e.sender.item);

            if (item.HR_COMPANY_ID) {

                vm.params = Object.assign(vm.params, { pHR_COMPANY_ID: item.HR_COMPANY_ID });
                $state.go('KntMCLoadingPlan', vm.params, { notify: false });
                //getBuildingData(item.HR_COMPANY_ID);
                getKnitMachineList(null, null, item.HR_COMPANY_ID);

            } else {
                delete vm.params['pHR_COMPANY_ID'];
                delete vm.params['pKNT_MACHINE_ID'];
                delete vm.params['pHR_PROD_FLR_ID'];
                delete vm.params['pHR_PROD_BLDNG_ID'];
                $state.go('KntMCLoadingPlan', vm.params, { notify: false, inherit: false });
                getKnitMachineList(null, null,null);
            }

            loadResources(null, null, null, (item.HR_COMPANY_ID || null));
          
        };

        $scope.onChangeBulding = function (e, pHR_COMPANY_ID) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.HR_PROD_BLDNG_ID) {

                vm.params = Object.assign(vm.params, { pHR_PROD_BLDNG_ID: item.HR_PROD_BLDNG_ID });
                $state.go('KntMCLoadingPlan', vm.params, { notify: false });

                loadResources(null, null,item.HR_PROD_BLDNG_ID,(pHR_COMPANY_ID||null));
            } else {
                delete vm.params['pKNT_MACHINE_ID'];
                delete vm.params['pHR_PROD_FLR_ID'];
                delete vm.params['pHR_PROD_BLDNG_ID'];
                $state.go('KntMCLoadingPlan', vm.params, { notify: false, inherit: false });

                loadResources(null, null,null,(pHR_COMPANY_ID||null));
            }
        };


        

        $scope.onChangeFloor = function (e, pHR_PROD_BLDNG_ID, pHR_COMPANY_ID) {
            var item = e.sender.dataItem(e.sender.item);
            loadResources((item.HR_PROD_FLR_ID || null), null, pHR_PROD_BLDNG_ID, pHR_COMPANY_ID);

            if (item.HR_PROD_FLR_ID) {
                vm.params = Object.assign(vm.params, { pHR_PROD_FLR_ID: item.HR_PROD_FLR_ID });
                $state.go('KntMCLoadingPlan', vm.params, { notify: false });
            } else {
                delete vm.params['pKNT_MACHINE_ID'];
                delete vm.params['pHR_PROD_FLR_ID'];
                $state.go('KntMCLoadingPlan', vm.params, { notify: false, inherit: false });
            }
        };

        $scope.onChangeMachine = function (e,data) {
            var itm = e.sender.dataItem(e.sender.item);

            if (itm.KNT_MACHINE_ID) {
                vm.params = Object.assign(vm.params, { pKNT_MACHINE_ID: itm.KNT_MACHINE_ID });
                $state.go('KntMCLoadingPlan', vm.params, { notify: false });
                loadResources(null, (itm.KNT_MACHINE_ID || null), null, (data.HR_COMPANY_ID || null), (data.KNT_MC_DIA_ID || null));
            } else {
                delete vm.params['pKNT_MACHINE_ID'];
                $state.go('KntMCLoadingPlan', vm.params, { notify: false, inherit: false });
                loadResources(null, null, null, (data.HR_COMPANY_ID || null), (data.KNT_MC_DIA_ID || null));
            }
            
        }

        vm.onChangeMcDia = function (e,data) {
            var it = e.sender.dataItem(e.sender.item);
            if (it.KNT_MC_DIA_ID) {
                loadResources(null,null, null, (data.HR_COMPANY_ID || null), it.KNT_MC_DIA_ID);
                getKnitMachineList(it.KNT_MC_DIA_ID,null, (data.HR_COMPANY_ID||null));
            } else {
                loadResources(null,null,null, (data.HR_COMPANY_ID || null), null);
               
                getKnitMachineList(null, null, (data.HR_COMPANY_ID||null));
            }
            
        };


        function splitKnitCard(source) {
            console.log(source);
            var data = [];

            if (dp.multiselect.events().length > 0) {
                dp.multiselect.events().forEach(function (itm) {
                    data.push(
                            {
                                KNT_JOB_CRD_H_ID: itm.data.id,
                                PROD_QTY: itm.data.PROD_QTY,
                                TG_D_PROD_QTY: itm.data.TG_D_PROD_QTY,
                                ASIGN_QTY: itm.data.ASIGN_QTY,
                                UN_ASIGN_QTY: itm.data.UN_ASIGN_QTY,
                                NEW_UN_ASIGN_QTY: itm.data.ASIGN_QTY - itm.data.PROD_QTY - itm.data.UN_ASIGN_QTY,
                                REASON_DESC: '',
                                text: itm.data.text,
                                START_DT: itm.data.start
                            }
                        )

                });
            } else {
                data.push({
                    KNT_JOB_CRD_H_ID: source.data.id,
                    PROD_QTY: source.data.PROD_QTY,
                    TG_D_PROD_QTY: source.data.TG_D_PROD_QTY,
                    ASIGN_QTY: source.data.ASIGN_QTY,
                    UN_ASIGN_QTY: source.data.UN_ASIGN_QTY,
                    NEW_UN_ASIGN_QTY: source.data.ASIGN_QTY - source.data.PROD_QTY - source.data.UN_ASIGN_QTY,
                    REASON_DESC: '',
                    text: source.data.text,
                    START_DT: source.data.start

                });
            };
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'SplitKnitCardModal.html',
                controller: function ($scope, KnittingDataService, $modalInstance, $q, knitCardList, Dialog) {


                    $scope.knitCardList = knitCardList;

                    $scope.remove = function (kntcard, idx) {
                        return Dialog.confirm('Removing knit card ref# ' + kntcard + '?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                            .then(function () {
                                $scope.knitCardList.splice(idx, 1);
                            });

                    };
                    $scope.save = function (data, isValid) {

                        if (!isValid) {
                            return;
                        }

                        var d2bSave = data.map(function (o) {
                            return {
                                //TG_D_PROD_QTY: o.TG_D_PROD_QTY,
                                KNT_JOB_CRD_H_ID: o.KNT_JOB_CRD_H_ID,
                                FIRST_DURATION: Math.ceil((o.FIRST_SPLIT + o.PROD_QTY) * (24 / o.TG_D_PROD_QTY)),
                                SECOND_DURATION: Math.ceil(o.SECOND_SPLIT * (24 / o.TG_D_PROD_QTY)),
                                UN_ASIGN_QTY: o.SECOND_SPLIT,
                                //REASON_DESC: o.REASON_DESC,
                                START_DT: o.START_DT
                            }
                        });

                        return KnittingDataService.saveDataByUrl({}, '/McLoadPlan/saveEventMovedData?pXML=' + KnittingDataService.xmlStringShort(d2bSave) + '&pOption=1003').then(function (res) {
                            if (res.success === false) {
                                vm.errors = res.errors;
                            }
                            else {
                                res['data'] = angular.fromJson(res.jsonStr);
                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    modalInstance.close({ success: true });
                                } else {
                                    config.appToastMsg(res.data.PMSG);
                                }
                            }
                        });

                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                resolve: {
                    knitCardList: function () {
                        return data;
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

        function openUnAssignQtyModal(source) {
            var data = [];

            if (dp.multiselect.events().length > 0) {
                dp.multiselect.events().forEach(function (itm) {
                    data.push(
                            {
                                KNT_JOB_CRD_H_ID: itm.data.id,
                                PROD_QTY: itm.data.PROD_QTY,
                                TG_D_PROD_QTY: itm.data.TG_D_PROD_QTY,
                                ASIGN_QTY: itm.data.ASIGN_QTY,
                                UN_ASIGN_QTY: itm.data.UN_ASIGN_QTY,
                                NEW_UN_ASIGN_QTY: itm.data.ASIGN_QTY - itm.data.PROD_QTY - itm.data.UN_ASIGN_QTY,
                                REASON_DESC: '',
                                text: itm.data.text,
                                START_DT: itm.data.start
                            }
                        )

                });
            } else {
                data.push({
                    KNT_JOB_CRD_H_ID: source.data.id,
                    PROD_QTY: source.data.PROD_QTY,
                    TG_D_PROD_QTY: source.data.TG_D_PROD_QTY,
                    ASIGN_QTY: source.data.ASIGN_QTY,
                    UN_ASIGN_QTY: source.data.UN_ASIGN_QTY,
                    NEW_UN_ASIGN_QTY: source.data.ASIGN_QTY - source.data.PROD_QTY - source.data.UN_ASIGN_QTY,
                    REASON_DESC: '',
                    text: source.data.text,
                    START_DT: source.data.start

                });
            };
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'UnAssignQtyBatchModal.html',
                controller: function ($scope, KnittingDataService, $modalInstance, $q, knitCardList, Dialog) {


                    $scope.knitCardList = knitCardList;

                    $scope.remove = function (kntcard, idx) {
                        return Dialog.confirm('Removing knit card ref# ' + kntcard + '?<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                         .then(function () {
                             $scope.knitCardList.splice(idx, 1);
                         });

                    };
                    $scope.save = function (data, isValid) {

                        if (!isValid) {
                            return;
                        }

                        var d2bSave = data.map(function(o){                        
                            return {
                                TG_D_PROD_QTY: o.TG_D_PROD_QTY,
                                //ASIGN_QTY: o.ASIGN_QTY,
                                //PROD_QTY : o.PROD_QTY,
                                KNT_JOB_CRD_H_ID: o.KNT_JOB_CRD_H_ID,
                                DURATION: Math.ceil((o.ASIGN_QTY - o.UN_ASIGN_QTY - o.NEW_NEW_UN_ASIGN_QTY) * (24 / o.TG_D_PROD_QTY)),
                                UN_ASIGN_QTY: o.UN_ASIGN_QTY,
                                NEW_UN_ASIGN_QTY: o.NEW_NEW_UN_ASIGN_QTY,
                                REASON_DESC: o.REASON_DESC,
                                START_DT: o.START_DT
                            }
                        });
                        return KnittingDataService.saveDataByUrl({}, '/McLoadPlan/saveEventMovedData?pXML=' + KnittingDataService.xmlStringShort(d2bSave) + '&pOption=1002').then(function (res) {
                            if (res.success === false) {
                                vm.errors = res.errors;
                            }
                            else {
                                res['data'] = angular.fromJson(res.jsonStr);
                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                    modalInstance.close({ success: true });
                                } else {
                                    config.appToastMsg(res.data.PMSG);
                                }
                            }
                        });

                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }
                },
                resolve: {
                    knitCardList: function () {
                        return data;
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

        function MoveToEnd(source) {
            var data = [];
            if (dp.multiselect.events().length > 0) {
                dp.multiselect.events().forEach(function (itm) {
                    data.push(
                            {
                                KNT_MACHINE_ID: itm.data.resource,
                                KNT_JOB_CRD_H_ID: itm.data.id,
                                START_DT: itm.data.start,
                                END_DT: itm.data.end

                            }
                        )

                });
            } else {
                data.push({
                    KNT_MACHINE_ID: source.data.resource,
                    KNT_JOB_CRD_H_ID: source.data.id,
                    START_DT: source.data.start,
                    END_DT: source.data.end

                });
            };
            return KnittingDataService.saveDataByUrl({}, '/McLoadPlan/saveEventMovedData?pXML=' + KnittingDataService.xmlStringShort(data) + '&pOption=1001').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        loadEvents();
                    } else {
                        config.appToastMsg(res.data.PMSG);
                    }
                }
            });
        };

        function HoldnUnhold(source, KNT_JC_STS_TYPE_ID) {
            var data = [];
            if (dp.multiselect.events().length > 0) {
                dp.multiselect.events().forEach(function (itm) {
                    data.push(
                            {
                                KNT_JOB_CRD_H_ID: itm.data.id,
                                KNT_JC_STS_TYPE_ID: KNT_JC_STS_TYPE_ID

                            }
                        )

                });
            } else {
                data.push({
                    KNT_JOB_CRD_H_ID: source.data.id,
                    KNT_JC_STS_TYPE_ID: KNT_JC_STS_TYPE_ID
                });
            };
            return KnittingDataService.saveDataByUrl({}, '/McLoadPlan/saveEventMovedData?pXML=' + KnittingDataService.xmlStringShort(data) + '&pOption=1004').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);
                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        loadEvents();
                    } else {
                        config.appToastMsg(res.data.PMSG);
                    }
                }
            });
        };



        var general = new DayPilot.Menu({
            items:
               [
                {
                    text: "Print Knit Card", onclick: function () {
                        printKnitCard(this.source.data.id);
                    }
                },
                {
                    text: "Edit Knit Card", onclick: function () {
                        editKnitCard(this.source);
                    }
                },
               {
                   text: "Move to End", onclick: function () {
                       MoveToEnd(this.source);
                   }
               },

               {
                   text: "Hold", onclick: function () {
                       HoldnUnhold(this.source,5);
                   }
               },

               //{
               //    text: "Unassign Qty", onclick: function () {
               //        openUnAssignQtyModal(this.source);
               //    }
               //},
                {
                    text: "Split", onclick: function () {
                        splitKnitCard(this.source);
                    }
                },
                 {
                     text: "Go to List", onclick: function () {
                         navigateKnitCardList(this.source.data);
                     }
                 },
               {
                   text: "Create Yarn Requisition", onclick: function () {

                       var data = [];
                       var kp = [];
                       if (dp.multiselect.events().length > 0) {
                           dp.multiselect.events().forEach(function (itm) {
                               data.push(itm.data.id);
                               kp.push(itm.data.KNT_PLAN_H_ID);
                           });
                       } else {
                           data.push(this.source.data.id);
                           kp.push(this.source.data.KNT_PLAN_H_ID);
                       };

                       return createYarnRequisition(data.join(','), kp.join(','));
                   }
               },
               ]
        });

        var holded = new DayPilot.Menu({
            items:
                   [
                       {
                           text: "UnHold", onclick: function () {
                               HoldnUnhold(this.source, 2);
                           }
                       },
                        {
                            text: "Move to End", onclick: function () {
                                MoveToEnd(this.source);
                            }
                        },
                   ]
        });

        var running = new DayPilot.Menu({
            items:
                [
                    {
                        text: "Print Knit Card", onclick: function () {
                            printKnitCard(this.source.data.id);
                        }
                    },
                    {
                        text: "Edit Knit Card", onclick: function () {
                            editKnitCard(this.source);
                        }
                    },
                    //{
                    //    text: "Unassign Qty", onclick: function () {
                    //        openUnAssignQtyModal(this.source);
                    //    }
                    //},
                    {
                        text: "Hold", onclick: function () {
                            HoldnUnhold(this.source, 5);
                        }
                    },
                
                    //{
                    //    text: "Split", onclick: function () {
                    //        splitKnitCard(this.source);
                    //    }
                    //},
                    {
                        text: "MarkAsCompleted", onclick: function () {
                            HoldnUnhold(this.source, 8);
                        }
                    },
                    {
                        text: "Go to List", onclick: function () {
                            navigateKnitCardList(this.source.data);
                        }
                    },
                    {
                        text: "Create Yarn Requisition", onclick: function () {
                            var data = [];
                            var kp = [];
                            if (dp.multiselect.events().length > 0) {
                                dp.multiselect.events().forEach(function (itm) {
                                    data.push(itm.data.id);
                                    kp.push(itm.data.KNT_PLAN_H_ID);
                                   
                                });
                            } else {
                                data.push(this.source.data.id);
                                kp.push(this.source.data.KNT_PLAN_H_ID);
                            };
                            return createYarnRequisition(data.join(','), kp.join(','));
                        }
                    },
                ]
        });

        var default_menu = new DayPilot.Menu({
            items:
                [
                    {
                        text: "Create Yarn Requisition", onclick: function () {
                            var data = [];
                            var kp = [];
                            if (dp.multiselect.events().length > 0) {
                                dp.multiselect.events().forEach(function (itm) {
                                    data.push(itm.data.id);
                                    kp.push(itm.data.KNT_PLAN_H_ID);
                                });
                            } else {
                                data.push(this.source.data.id);
                                kp.push(this.source.data.KNT_PLAN_H_ID);
                            };
                            return createYarnRequisition(data.join(','), kp.join(','));
                        }
                    }
                ]
        });


        function printKnitCard(pKNT_JOB_CRD_H_ID) {
            var url = '/Knitting/Knit/KnitCardRpt/#/KnitCardRpt?pKNT_JOB_CRD_H_ID=' + pKNT_JOB_CRD_H_ID
            var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 300) + ',scrollbars=yes,status=yes';
            $window.open(url, "_blank", opt);
        }


    

        $scope.schedulerConfig = {
            visible: false, // will be displayed after loading the resources
            scale: "Manual",

            cellWidth: 30,
            cellDuration: 60,
            rowHeaderWidth: 200,
            rowHeaderScrolling: true,

            heightSpec : "Max",
            height : 800,


            timeline: getTimeline(),
            timeHeaders: getTimeHeaders(),

            rowDoubleClickHandling: "Enabled",
            eventHoverHandling: "Bubble",
            bubble: new DayPilot.Bubble(),

            eventMoveVerticalEnabled: false,
            rowMarginBottom: 8,

            allowEventOverlap : true,

            allowMultiSelect  : true,
            multiSelectRectangle: "Free",
            eventClickHandling: "Select",


            eventMovingStartEndEnabled: true,
            eventMovingStartEndFormat: "MMM d,hh tt",
            eventResizingStartEndEnabled: true,
            eventResizingStartEndFormat: "MMM d,hh tt",

            useEventBoxes: "ShortEventsOnly",//'Always','ShortEventsOnly','Never';

            //Moving
            moveBy: 'Full',
            allowMultiMove: true,
            multiMoveVerticalMode: "Disabled",


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
            treePreventParentUsage : true,
            //heightSpec : "Max",
            treeEnabled : true,
            contextMenuResource : new DayPilot.Menu({items: [
                  {
                      text: 'Re-arrange', onclick: function () {
                          var e = this.source;
                          var svData = {};
                          var data = [];
                          console.log(e);
     
                          svData['RF_LOCATION_ID'] = e.data.RF_LOCATION_ID;
                          //svData['KNT_MACHINE_ID'] = e.data.id;


                          if (e.data.id < 0) {
                              e.data.children.forEach(function (itm) {
                                  dp.rows.find(itm.id).events.all().forEach(function (item) {
                                      data.push(
                                            {
                                                KNT_MACHINE_ID: item.data.resource,
                                                KNT_JOB_CRD_H_ID: item.data.id,
                                                START_DT: item.data.start,
                                                END_DT: item.data.end

                                            }
                                        );
                                  })
                              });
                            


                          } else {
                              e.events.all().forEach(function (itm) {
                                  data.push(
                                          {
                                              KNT_MACHINE_ID: itm.data.resource,
                                              KNT_JOB_CRD_H_ID: itm.data.id,
                                              START_DT: itm.data.start,
                                              END_DT: itm.data.end

                                          }
                                      )

                              });
                          }
                         
                          svData['XML'] = KnittingDataService.xmlStringShort(data);
                          return KnittingDataService.saveDataByUrl(svData, '/McLoadPlan/removeEmptyCell').then(function (res) {
                              if (res.success === false) {
                                  vm.errors = res.errors;
                              }
                              else {
                                  res['data'] = angular.fromJson(res.jsonStr);

                                  if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                      loadEvents();
                                  } else {
                                      config.appToastMsg(res.data.PMSG);
                                  }
                              }
                          })
                      }
                  }
            ]}),




            // $scope.schedulerConfig.events.filter(query||null); 
            // $scope.schedulerConfig.rows.filter(query||null);


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

            eventHeight: 34,
            rowHeaderColumns: [
                {title: "Machine", width: 80},
                {title: "Dia x Gauge", width: 80},
                {title: "No.ofFeeder", width: 80}
            ],

            onBeforeResHeaderRender: function(args) {
                var beds = function(count) {
                    return count + " bed" + (count > 1 ? "s" : "");
                };

                //args.resource.columns[0].html = beds(args.resource.capacity);
                //args.resource.columns[1].html = args.resource.status;

                args.resource.columns[0].html = args.resource.dia_gauge;
                args.resource.columns[1].html = args.resource.no_feeder;

                //switch (args.resource.status) {
                //    case "Basic":
                //        args.resource.cssClass = "status_dirty";
                //        break;
                //    case "Polo":
                //        args.resource.cssClass = "status_cleanup";
                //        break;
                //}
            },
            onEventMoving: function (args) {
                //if (args.overlapping) {
                //    args.left.enabled = true;
                //    args.left.html = "Conflict with an existing Knit Card!";
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
            onEventMoved: function (args) {
                var data = [];
                args.multimove.map(function (item) {
                    data.push({
                        KNT_MACHINE_ID : item.event.resource(),
                        KNT_JOB_CRD_H_ID: item.event.id(),
                        START_DT: item.start.value,
                        END_DT: item.end.value
                    });
                });

                return KnittingDataService.saveDataByUrl({}, '/McLoadPlan/saveEventMovedData?pXML=' + KnittingDataService.xmlStringShort(data)).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            loadEvents();
                        } else {
                            config.appToastMsg(res.data.PMSG);
                        }
                    }
                })
            },

            onRowDoubleClick :function(args){
                dp.multiselect.clear();
            },
            onEventResizing : function(args) {
                    //args.left.enabled = false;
                    //args.right.enabled = true;
                    //console.log(args.duration.totalHours());
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
            },
            onEventSelect: function (args) {
                dp.multiselect.add(args.e);
            },
            onEventResized: function (args) {
                var data = [
                    {
                        KNT_MACHINE_ID: args.e.resource(),
                        KNT_JOB_CRD_H_ID: args.e.id(),
                        START_DT: args.e.start().value,
                        END_DT: args.e.end().value
                    }
                ];
                return KnittingDataService.saveDataByUrl({}, '/McLoadPlan/saveEventMovedData?pXML=' + KnittingDataService.xmlStringShort(data)).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            loadEvents();
                        } else {
                            config.appToastMsg(res.data.PMSG);
                        }
                    }
                });
            },
            onBeforeEventRender: function(args) {
                var start = new DayPilot.Date(args.data.start);
                var end = new DayPilot.Date(args.data.end);

                var now = new DayPilot.Date();
                var today = new DayPilot.Date().getDatePart();
                var status = args.e.status;
                var bubbleHtml = '';

                args.data.html = args.data.text + " (" + start.toString("M/d") + " - " + end.toString("M/d") + ")";

                //+ "<br /><span style='color:gray'>" + status + "</span>"
                bubbleHtml += "<div>Knit Card# :<b>" + args.e.JOB_CRD_NO + "</b> (" + new DayPilot.Date(args.e.start).toString("d/M/yy htt") + " to " + new DayPilot.Date(args.e.end).toString("d/M/yy htt") + ")</div>";
                bubbleHtml += "<div>Buyer A/C : <b>" + args.e.BYR_ACC_NAME_EN + "</b></div>";
                bubbleHtml += "<div>Style : <b>" + args.e.WORK_STYLE_NO + "</b></div>";
                bubbleHtml += "<div>Order # : <b>" + args.e.ORDER_NO_LIST + "</b></div>";
                bubbleHtml += "<div>Fab Type : <b>" + args.e.FAB_TYPE_NAME + "</b></div>";
                bubbleHtml += "<div>Color : <b>" + args.e.COLOR_NAME_EN + "</b></div>";

                bubbleHtml += "<div>----------------------------------------------</div>";

                bubbleHtml += "<div>Assign Qty : <b>" + (args.e.ASIGN_QTY - args.e.UN_ASIGN_QTY) + " Kg</b></div>";
                bubbleHtml += "<div>Target/Day : <b>" + args.e.TG_D_PROD_QTY + " Kg</b></div>";
                bubbleHtml += "<div>Production: <b> " + args.e.PROD_QTY + " Kg [" + args.e.pct_completion + "%]</b></div>";

                bubbleHtml += "<div>----------------------------------------------</div>";

                bubbleHtml += "<div>Status: <b style='color:" + args.e.barColor + ";'>" + args.e.status + "</b></div>";

                args.e.bubbleHtml = bubbleHtml;

                switch(args.e.KNT_JC_STS_TYPE_ID){
                    case 2: //Ready
                        args.e.contextMenu = general;
                        break;
                    case 5: //Hold
                        args.e.contextMenu = holded;
                        break;
                    case 3:// Running
                        args.e.contextMenu = running;
                        break;
                    default :
                        args.e.contextMenu = default_menu;
                }
                        
                
              
                var pct_completion = args.e.pct_completion;
                var paidColor = "#aaaaaa";
                args.data.areas = [
                    { bottom: 4, right: 4, html: "<div style='color:" + paidColor + "; font-size: 8pt;'>" + (args.e.ASIGN_QTY - args.e.UN_ASIGN_QTY) + "|" + args.e.PROD_QTY + "|" + pct_completion + "%</div>", v: "Visible", action: (args.e.KNT_JC_STS_TYPE_ID == 3 ? "ResizeEnd" : "None") },
                    { left: 4, bottom: 6, right: 4, height: 2, html: "<div style='background-color:" + paidColor + "; height: 100%; width:" + pct_completion + "%'></div>" }
                ];

            },
            onBeforeTimeHeaderRender: function (args) {
                args.header.html = args.header.html.replace(" AM", "a").replace(" PM", "p");

                if ($scope.scale === 'shifts') {
                    args.header.html = args.header.html.replace("6a", "A").replace("2p", "B").replace("10p", "C");
                }
            },

        };

        $timeout(function () {
            dp = $scope.scheduler;  // debug
            loadResources(($stateParams.pHR_PROD_FLR_ID||null),($stateParams.KNT_MACHINE_ID||null),($stateParams.HR_PROD_BLDNG_ID||null),($stateParams.pHR_COMPANY_ID||null));
            loadEvents(DayPilot.Date.today());
        });

        function loadEvents(day) {
            var from = $scope.scheduler.visibleStart();
            var to = $scope.scheduler.visibleEnd();
            if (day) {
                from = new DayPilot.Date(day).firstDayOfMonth();
                to = from.addMonths($scope.TimeRange);
            }

            var params = {
                start: from.toString(),
                end: to.toString()
            };

            $scope.CurrentDTRange = angular.copy(params);

            $scope.CurrentDTRange['start'] = new Date($scope.CurrentDTRange.start);
            $scope.CurrentDTRange['end'] = new Date($scope.CurrentDTRange.start);
            $scope.CurrentDTRange['today'] = new Date();

            return KnittingDataService.getDataByUrl('/McLoadPlan/loadEventData?pSTART_DT=' + params.start + '&pEND_DT=' + params.end).then(function (res) {
                $scope.schedulerConfig.timeline = getTimeline(day);
                $scope.schedulerConfig.scrollTo = day;
                $scope.schedulerConfig.scrollToAnimated = "fast";
                $scope.schedulerConfig.scrollToPosition = "left";
                $scope.events = res.map(function (o) {
                    o['moveVDisabled'] = true;
                    return o;
                });
            });

        }


        function getTimeline(date) {
            var date = date || DayPilot.Date.today();
            var start = new DayPilot.Date(date).firstDayOfMonth();

            var timeline = [];

            var increaseA;  // in hours
            var increaseB;  // in hours
            var increaseC;  // in hours
            var AShiftStarts;
            var AShiftEnds;
            var BShiftStarts;
            var BShiftEnds;
            var CShiftStarts;
            var CShiftEnds;;


            switch ($scope.scale) {
                case "hours":

                    AShiftStarts = 6;
                    AShiftEnds = 14;
                    BShiftStarts = 14;
                    BShiftEnds = 22;
                    CShiftStarts = 22;
                    CShiftEnds = 30;

                    increaseA = 1;
                    increaseB = 1;
                    increaseC = 1;
                    break;
                case "shifts":

                    AShiftStarts = 6;
                    AShiftEnds = 14;
                    BShiftStarts = 14;
                    BShiftEnds = 22;
                    CShiftStarts = 22;
                    CShiftEnds = 30;

                    increaseA = AShiftEnds - AShiftStarts;
                    increaseB = BShiftEnds - BShiftStarts;
                    increaseC = CShiftEnds - CShiftStarts;

                    break;

                case "day":
                    AShiftStarts = 0;
                    AShiftEnds = 23;
                    BShiftStarts = 1;
                    BShiftEnds = 0;
                    CShiftStarts = 1;
                    CShiftEnds = 0;

                    increaseA = 24;
                    increaseB = 0;
                    increaseC = 0;
                    break;

                default:
                    throw "Invalid scale value";
            }


            for (var j = 0; j < $scope.TimeRange; j++) {
                
                var strt = start.addMonths(j);
                var days = strt.daysInMonth();

                for (var i = 0; i < days; i++) {
                    var day = strt.addDays(i);

                    for (var x = AShiftStarts; x < AShiftEnds; x += increaseA) {
                        timeline.push({ start: day.addHours(x), end: day.addHours(x + increaseA) });
                    }

                    for (var x = BShiftStarts; x < BShiftEnds; x += increaseB) {
                        timeline.push({ start: day.addHours(x), end: day.addHours(x + increaseC) });
                    }

                    for (var x = CShiftStarts; x < CShiftEnds; x += increaseC) {
                        timeline.push({ start: day.addHours(x), end: day.addHours(x + increaseC) });
                    }
                }
            }


            return timeline;
        }
        function getTimeHeaders() {
            switch ($scope.scale) {
                case "hours":
                    return [{ groupBy: "Month", format: "MMMM yyyy" }, { groupBy: "Day", format: "ddd,d" }, { groupBy: "Hour", format: "h tt" }];
                    break;
                case "shifts":
                    return [{ groupBy: "Month", format: "MMMM yyyy" }, { groupBy: "Day", format: "ddd,d" }, { groupBy: "Cell", format: "h tt" }];
                    break;
                case "day":
                    return [{ groupBy: "Month", format: "MMMM yyyy" }, { groupBy: "Cell", format: "ddd,d" }];
                    break;
            }
        }
    }

})();