(function () {
    'use strict';
    angular.module('multitex').controller('HourlyFinProductionEntryController', ['$q', '$stateParams', '$state', '$scope', '$modal', '$localStorage', 'DashBoardService', 'FloorData', 'config', 'exception', 'cur_date_server', '$filter', HourlyFinProductionEntryController]);
    function HourlyFinProductionEntryController($q, $stateParams, $state, $scope, $modal, $localStorage, DashBoardService, FloorData, config, exception, cur_date_server, $filter) {

        var vm = this;
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

        vm.selectedDt = cur_date_server;

        $scope.minDate = new Date(cur_date_server);
        $scope.dt = new Date(cur_date_server);
        $scope.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.opened = true;
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.format = config.appDateFormat;
        vm.onDateSelect = function (dt) {
            vm.selectedDt = $filter('date')(dt, 'dd-MMM-yyyy');
            getLineListData(vm.selectedFlr, $filter('date')(dt, 'dd-MMM-yyyy'));
        };

        $scope.FloorList = FloorData;

        if ($stateParams.pFLOOR_LIST) {
            getLineListData($stateParams.pFLOOR_LIST, vm.selectedDt);
            vm.selectedFlr = parseInt($stateParams.pFLOOR_LIST);
        } else {
            if (!$localStorage.HPEF_FLOOR_LIST) {
                vm.selectedFlr = FloorData[0].HR_PROD_FLR_ID;

                getLineListData(FloorData[0].HR_PROD_FLR_ID, null);
            } else {
                getLineListData($localStorage.HPEF_FLOOR_LIST[0], vm.selectedDt);
                vm.selectedFlr = $localStorage.HPEF_FLOOR_LIST[0];
            }
        }

        vm.OnChangeFloor = function (itm) {
            getLineListData(itm.HR_PROD_FLR_ID, vm.selectedDt);
            vm.selectedFlr = itm.HR_PROD_FLR_ID;
            angular.forEach($scope.FloorList, function (val, key) {
                if (itm.HR_PROD_FLR_ID == val.HR_PROD_FLR_ID) {
                    val['IS_ACTIVE'] = true;
                } else {
                    val['IS_ACTIVE'] = false;
                }
               
            });

            $state.go('HourlyFinProductionEntry', {}, { notify: false, inherit: false });

        };

        vm.refresh = function () {
            getLineListData(vm.selectedFlr, vm.selectedDt);
        }

        vm.onAllFloorSelect = function (flr,IS_ALL_FLOOR) {
            if (IS_ALL_FLOOR) {
                var flr_list = _.map(flr, 'HR_PROD_FLR_ID');
                vm.selectedFlr = flr_list.join(',');
                getLineListData(flr_list.join(','), vm.selectedDt);

                angular.forEach(flr, function (val, key) {
                    val['IS_ACTIVE'] = true;
                });

            } else {
                vm.selectedFlr = flr[0].HR_PROD_FLR_ID;

                getLineListData(flr[0].HR_PROD_FLR_ID, vm.selectedDt);
                angular.forEach(flr, function (val, key) {
                    if (val.HR_PROD_FLR_ID != flr[0].HR_PROD_FLR_ID) {
                        val['IS_ACTIVE'] = false;
                    }

                });
            }
        };


        function getLineListData(pHR_PROD_FLR_ID_LST, pPROD_DT) {
            return DashBoardService.getDataByUrl('/api/common/getFinishingProdEntryData?pHR_PROD_FLR_ID_LST=' + pHR_PROD_FLR_ID_LST + '&pPROD_DT=' + pPROD_DT).then(function (res) {
                vm.FloorLineData = res;
            });
        }

        $scope.$watch('vm.FloorLineData', function (newVal, oldVal) {
            //if (!oldVal)
            //    return;

            vm.HRLY_TGT_QTY_TTL = _.sumBy(newVal, function (o) { return (o.HRLY_TGT_QTY || 0); });
            vm.H1_PRD_QTY_TTL = _.sumBy(newVal, function (o) { return (o.H1_PRD_QTY || 0); });
            vm.H2_PRD_QTY_TTL = _.sumBy(newVal, function (o) { return (o.H2_PRD_QTY || 0); });
            vm.H3_PRD_QTY_TTL = _.sumBy(newVal, function (o) { return (o.H3_PRD_QTY || 0); });
            vm.H4_PRD_QTY_TTL = _.sumBy(newVal, function (o) { return (o.H4_PRD_QTY || 0); });
            vm.H5_PRD_QTY_TTL = _.sumBy(newVal, function (o) { return (o.H5_PRD_QTY || 0); });
            vm.H6_PRD_QTY_TTL = _.sumBy(newVal, function (o) { return (o.H6_PRD_QTY || 0); });
            vm.H7_PRD_QTY_TTL = _.sumBy(newVal, function (o) { return (o.H7_PRD_QTY || 0); });
            vm.H8_PRD_QTY_TTL = _.sumBy(newVal, function (o) { return (o.H8_PRD_QTY || 0); });

            vm.OT_TGT_QTY_TTL = _.sumBy(newVal, function (o) { return (o.OT_TGT_QTY || 0); });
            vm.OT_TGT_HR_TTL = _.sumBy(newVal, function (o) { return (o.OT_TGT_HR || 0); });
            vm.OT_ACT_PRD_QTY_TTL = _.sumBy(newVal, function (o) { return (o.OT_ACT_PRD_QTY || 0); });



        },true);
        
        vm.openConfigModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'HourlyProductionBoardConfigModal.html',
                controller: function ($scope,$modalInstance, FloorLineData, $q, $localStorage, FloorList) {
                    activate()
                    $scope.showSplash = true;
                    function activate() {
                        var promise = [];
                        return $q.all(promise).then(function () {
                            $scope.showSplash = false;
                        });
                    }
                    $scope.alerts = [];
                    $scope.FloorList = FloorList;
                    $scope.closeAlert = function (index) {
                        $scope.alerts.splice(index, 1);
                    };


                    $scope.FloorChange = function (HR_PROD_FLR_ID, IS_SELECT_FLR) {
                        angular.forEach($scope.lineList, function (val, key) {
                            if (val.HR_PROD_FLR_ID == HR_PROD_FLR_ID) {
                                val['IS_SELECT_LINE'] = IS_SELECT_FLR;
                            }
                        });
                    };

                    $scope.option_hpef = $localStorage.option_hpef || 'M';

                    $scope.lineList = FloorLineData.map(function (o) {
                        o['IS_SELECT_LINE'] = _.some($localStorage.HPEF_LINE_LIST, function (b) {
                            return b == o.HR_PROD_LINE_ID;
                        });
                        return o;
                    });

                    $scope.save = function (data) {

                        var Floor = [];
                        $scope.alerts = [];

                        if ($scope.option_hpef == 'F') {
                            if (!$scope.option_hpef) {
                                $scope.alerts.push({
                                    type: 'danger', msg: 'Please Select Floor.'
                                });
                                return;
                            }

                            Floor.push($scope.floor_hpef);

                            $scope.alerts.push({
                                type: 'success', msg: 'Save Successful.'
                            });

                            $localStorage.option_hpef = $scope.option_hpef;
                            $localStorage.HPEF_FLOOR_LIST = Floor;
                            $localStorage.HPEF_LINE_LIST = [];

                            setTimeout(function () {
                                $modalInstance.close([$localStorage.HPEF_FLOOR_LIST.join(','), '']);
                            }, 500);
                           


                        } else if ($scope.option_hpef == 'M') {
                            var data = _.filter(data, function (o) {
                                return o.IS_SELECT_LINE == true;
                            });

                            if (data.length == 0) {
                                $scope.alerts.push({
                                    type: 'danger', msg: 'Please select atleast one Line'
                                });
                                return;
                            }


                            angular.forEach(data, function (val, key) {
                                var idx = Floor.indexOf(val.HR_PROD_FLR_ID);
                                if (idx < 0) {
                                    Floor.push(val.HR_PROD_FLR_ID);
                                }
                            })

                            $localStorage.HPEF_LINE_LIST = _.map(data, 'HR_PROD_LINE_ID');
                            $localStorage.option_hpef = $scope.option_hpef;
                            $localStorage.HPEF_FLOOR_LIST = Floor;

                            $scope.alerts.push({
                                type: 'success', msg: 'Save Successful.'
                            });
                            setTimeout(function () {
                                $modalInstance.close([$localStorage.HPEF_FLOOR_LIST.join(','), $localStorage.HPEF_LINE_LIST.join(',')]);
                            },500);
                           
                        }
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }

                    $scope.close = function () {
                        $modalInstance.close();
                    }

                },
                size: 'md',
                windowClass: 'large-Modal',
                resolve: {
                    FloorLineData: function () {
                        return DashBoardService.getDataByUrl('/api/common/GetSewingLineData');
                    },
                    FloorList : function(){
                        return $scope.FloorList;
                    }
                    
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
                if (data.length > 0) {
                    getLineListData(data[0], data[1],vm.selectedDt);
                    vm.selectedFlr = data[0];
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });


        };
        vm.save = function (dataOri,isValid) {
            if (!isValid) {
                return;
            }
          var  data2bSave = dataOri.map(function (item) {
              return    {
                  GMT_FIN_PROD_ID: item.GMT_FIN_PROD_ID,
                  HR_PROD_FLR_ID: item.HR_PROD_FLR_ID,
                  RF_GMT_FP_TYPE_ID: item.RF_GMT_FP_TYPE_ID,
                  PROD_DT: item.PROD_DT.split('T')[0],
                  H1_PRD_QTY: item.H1_PRD_QTY,
                  H2_PRD_QTY: item.H2_PRD_QTY,
                  H3_PRD_QTY: item.H3_PRD_QTY,
                  H4_PRD_QTY: item.H4_PRD_QTY,
                  H5_PRD_QTY: item.H5_PRD_QTY,
                  H6_PRD_QTY: item.H6_PRD_QTY,
                  H7_PRD_QTY: item.H7_PRD_QTY,
                  H8_PRD_QTY: item.H8_PRD_QTY,
                  OT_TGT_HR: item.OT_TGT_HR,
                  OT_TGT_QTY: item.OT_TGT_QTY||0,
                  OT_ACT_PRD_QTY: item.OT_ACT_PRD_QTY,
                  HRLY_TGT_QTY: (item.HRLY_TGT_QTY||0),
                  REMARKS: item.REMARKS,
                  QTY_MOU_ID: 3,
                  CT_TGT_QTY: item.CT_TGT_QTY,
                  CT_PRD_QTY: item.CT_PRD_QTY
              }
          });

           
          return DashBoardService.saveDataByUrl({ XML: config.xmlStringShort(data2bSave)}, '/api/common/SaveFinishingData').then(function (res) {
              var o = angular.fromJson(res);
              if (o.PMSG.substr(0, 9) == 'MULTI-001') {
                  getLineListData(vm.selectedFlr, vm.selectedDt);
              }
              config.appToastMsg(o.PMSG);

          }).catch(function (message) {
              exception.catcher('XHR loading Failded')(message);
          });

        }

    }

})();