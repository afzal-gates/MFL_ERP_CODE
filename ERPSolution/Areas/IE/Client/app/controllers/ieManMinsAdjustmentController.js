(function () {
    'use strict';
    angular.module('multitex.ie').controller('IeManMinsAdjustmentController', ['$q', '$stateParams', '$state', '$scope', '$modal', '$localStorage', 'IeDataService', 'FloorData', 'config', 'exception', 'cur_date_server', '$filter', IeManMinsAdjustmentController]);
    function IeManMinsAdjustmentController($q, $stateParams, $state, $scope, $modal, $localStorage, IeDataService, FloorData, config, exception, cur_date_server, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.selectedFlr = null;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

 
        vm.selectedDt = new Date().toISOString();

        $scope.minDate = new Date();
        $scope.dt = new Date();
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
            getLineListData(vm.selectedFlr,'',$filter('date')(dt, 'dd-MMM-yyyy'));
        };


        $scope.FloorList = FloorData;

        if ($stateParams.pFLOOR_LIST) {
            getLineListData($stateParams.pFLOOR_LIST, ($stateParams.pLINE_LIST || ''), vm.selectedDt);
            vm.selectedFlr = parseInt($stateParams.pFLOOR_LIST);
        } else {
            if (!$localStorage.HPE_FLOOR_LIST) {
                vm.selectedFlr = FloorData[0].HR_PROD_FLR_ID;

                getLineListData(FloorData[0].HR_PROD_FLR_ID, null,vm.selectedDt);
            } else {
                getLineListData($localStorage.HPE_FLOOR_LIST[0], $localStorage.HPE_LINE_LIST ? $localStorage.HPE_LINE_LIST.join(',') : '',vm.selectedDt);
                vm.selectedFlr = $localStorage.HPE_FLOOR_LIST[0];
            }
        }

        vm.OnChangeFloor = function (itm) {
            getLineListData(itm.HR_PROD_FLR_ID, '', vm.selectedDt);
            $state.go('ManMinAdjustment', {}, { notify: false, inherit: false });
            vm.selectedFlr = itm.HR_PROD_FLR_ID;
        };

        vm.refresh = function () {
            getLineListData(vm.selectedFlr, '',vm.selectedDt);
        }

        function getLineListData(pHR_PROD_FLR_LST,pHR_PROD_LINE_LST,DT) {
            return IeDataService.getDataByUrl('/GmtIeTarget/GetDataIeManMinAdjustment?pOption=3000&pRF_CALENDAR_DATE=' + DT+ '&pHR_PROD_LINE_LST=' + (pHR_PROD_LINE_LST || '')+'&pHR_PROD_FLR_LST=' + pHR_PROD_FLR_LST ).then(function (res) {
                vm.FloorLineData = res;
            });
        }

        $scope.$watch('vm.FloorLineData', function (newVal, oldVal) {
            vm.H9_MP_ADD_TTL = _.sumBy(newVal, function (o) { return (o.H9_MP_ADD || 0); });
            vm.H10_MP_ADD_TTL = _.sumBy(newVal, function (o) { return (o.H10_MP_ADD || 0); });
            vm.H11_MP_ADD_TTL = _.sumBy(newVal, function (o) { return (o.H11_MP_ADD || 0); });
            vm.H12_MP_ADD_TTL = _.sumBy(newVal, function (o) { return (o.H12_MP_ADD || 0); });
            vm.H13_MP_ADD_TTL = _.sumBy(newVal, function (o) { return (o.H13_MP_ADD || 0); });
            vm.H14_MP_ADD_TTL = _.sumBy(newVal, function (o) { return (o.H14_MP_ADD || 0); });
            vm.H15_MP_ADD_TTL = _.sumBy(newVal, function (o) { return (o.H15_MP_ADD || 0); });
            vm.H15_P_MP_ADD_TTL = _.sumBy(newVal, function (o) { return (o.H15_P_MP_ADD || 0); });
            vm.USED_MP_TTL = _.sumBy(newVal, function (o) { return (o.USED_MP || 0); });
        }, true);


        
        vm.openConfigModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'HourlyProductionBoardConfigModal.html',
                controller: function ($scope, $modalInstance, FloorLineData, $q, $localStorage, FloorList) {
                    console.log(FloorLineData);
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

                    $scope.option_hpe = $localStorage.option_hpe || 'A';

                    $scope.floor_hpe = ($localStorage.HPE_FLOOR_LIST && $localStorage.HPE_FLOOR_LIST.length) > 0 ? $localStorage.HPE_FLOOR_LIST[0] : null;

                    $scope.lineList = FloorLineData.map(function (o) {
                        o['IS_SELECT_LINE'] = _.some($localStorage.HPE_LINE_LIST, function (b) {
                            return b == o.HR_PROD_LINE_ID;
                        });
                        return o;
                    });

                    $scope.save = function (data) {

                        var Floor = [];
                        $scope.alerts = [];

                        if ($scope.option_hpe == 'F') {
                            if (!$scope.floor_hpe) {
                                $scope.alerts.push({
                                    type: 'danger', msg: 'Please Select Floor.'
                                });
                                return;
                            }
                            Floor.push($scope.floor_hpe);
                            $scope.alerts.push({
                                type: 'success', msg: 'Save Successful.'
                            });

                            $localStorage.option_hpe = $scope.option_hpe;
                            $localStorage.HPE_FLOOR_LIST = Floor;
                            $localStorage.HPE_LINE_LIST = [];

                            setTimeout(function () {
                                $modalInstance.close([$localStorage.HPE_FLOOR_LIST[0], '']);
                            }, 500);
                           


                        } else if ($scope.option_hpe == 'M') {
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

                            $localStorage.HPE_LINE_LIST = _.map(data, 'HR_PROD_LINE_ID');
                            $localStorage.option_hpe = $scope.option_hpe;
                            $localStorage.HPE_FLOOR_LIST = Floor;

                            $scope.alerts.push({
                                type: 'success', msg: 'Save Successful.'
                            });
                            setTimeout(function () {
                                $modalInstance.close([$localStorage.HPE_FLOOR_LIST[0], $localStorage.HPE_LINE_LIST.join(',')]);
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
                        return IeDataService.getDataByUrl('/api/common/GetSewingLineData');
                    },
                    FloorList : function(){
                        return $scope.FloorList;
                    }
                    
                }
            });

            modalInstance.result.then(function (data) {
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
                  GMT_IE_MIN_ADJ_ID: item.GMT_IE_MIN_ADJ_ID,
                  H9_MP_ADD: item.H9_MP_ADD,
                  H10_MP_ADD: item.H10_MP_ADD,
                  H11_MP_ADD: item.H11_MP_ADD,
                  H12_MP_ADD: item.H12_MP_ADD,
                  H13_MP_ADD: item.H13_MP_ADD,
                  H14_MP_ADD: item.H14_MP_ADD,
                  H15_MP_ADD: item.H15_MP_ADD,
                  H15_P_MP_ADD: item.H15_P_MP_ADD              
              }
          });

           
          return IeDataService.saveDataByUrl({ XML: config.xmlStringShort(data2bSave), PROD_DT: vm.selectedDt }, '/GmtIeTarget/SaveManMinAdjustmentData').then(function (res) {
              var o = angular.fromJson(res);

              if (o.PMSG.substr(0, 9) == 'MULTI-001') {
                  getLineListData(vm.selectedFlr, '',vm.selectedDt);
              }
              config.appToastMsg(o.PMSG);
          }).catch(function (message) {
              exception.catcher('XHR loading Failded')(message);
          });

        }

    }

})();