(function () {
    'use strict';
    angular.module('multitex.displayboard').controller('HourlyProductionBoardController', ['$q', '$stateParams', '$state', '$scope', '$modal', '$localStorage', 'Hub', '$rootScope', '$window', 'DisplayboardDataService', '$filter', HourlyProductionBoardController]);
    function HourlyProductionBoardController($q, $stateParams, $state, $scope, $modal, $localStorage, Hub, $rootScope, $window, DisplayboardDataService, $filter) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;

        $scope.flrSelected = null;
        vm.dtSelected = null;
        var interval;
        var AutoRefreshInterval;

        var hubUrl = "/signalr";
        var connection = $.hubConnection(hubUrl, { useDefaultPath: true });
        var hub = connection.createHubProxy("dashboard");
        var hubStart = connection.start();

        var hub = new Hub('dashboard', {
            listeners: {
                'newConnection': function (id) {
                    $rootScope.$apply();
                },
                'removeConnection': function (id) {
                    $rootScope.$apply();
                },
                'executedFromServer': function () {
                    executedFromServer();
                    $rootScope.$apply();
                }
            },
            methods: [],
            errorHandler: function (error) {
                console.error(error);
            }
        });

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getLineListData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        var default_index = 0;
        var autoReload = function () {
            var item = $scope.FloorList[default_index].HR_PROD_FLR_ID;
            getBoardData(item.toString(), null);
            if (($scope.FloorList.length - 1) == default_index) {
                default_index = 0;
            } else {
                default_index += 1;
            }
        };




        function getLineListData() {
            return DisplayboardDataService.getDataByFullUrl('/api/common/GetSewingLineData').then(function (res) {
                $scope.FloorLineData = res;
                $scope.FloorList = _.map(_.groupBy(res, function (o) {
                    return o.HR_PROD_FLR_ID;
                }), function (g) {
                    g[0]['IS_ACTIVE'] = false;
                    return g[0];
                });
                if ($localStorage.option == "A") {
                    $scope.IS_AUTO_CHANGE = true;
                    getBoardData('1', null)
                    interval = setInterval(autoReload, ($localStorage.Interval || 20) * 1000);

                } else {
                    getBoardData($localStorage.Floor ? $localStorage.Floor.join(',') : '1', null);
                    clearInterval(interval);
                }
                
            });
        }
        var autoRefresh = function () {
            executedFromServer();
        };

        if (true) {
            AutoRefreshInterval = setInterval(autoRefresh, 10 * 60 * 1000);
        }


        vm.onOutoChangeChecked = function (IS_AUTO_CHANGE) {           
            if (IS_AUTO_CHANGE) {
                interval = setInterval(autoReload, ($localStorage.Interval || 20) * 1000); 
                clearInterval(AutoRefreshInterval);
            } else {
                clearInterval(interval);
                AutoRefreshInterval = setInterval(autoRefresh, 20 * 60 * 1000);
            }
            getBoardData('1', null);
        }



        vm.ChangeDataByDate = function (pPROD_DT) {
            getBoardData($scope.flrSelected, pPROD_DT);
            vm.dtSelected = pPROD_DT;
        };
        $window.ChangeDataByDate = vm.ChangeDataByDate;

       function executedFromServer() {
            if ($localStorage.option != 'A' && !vm.dtSelected) {
                getBoardData($scope.flrSelected, null);               
            }
        };


  


        function getBoardData(pHR_PROD_FLR_LST, pPROD_DT) {

            var HR_PROD_FLR_LST;
            if (!pHR_PROD_FLR_LST && $localStorage.Floor) {

                HR_PROD_FLR_LST = $localStorage.Floor.join(',');


                //var FloorSplit = HR_PROD_FLR_LST.split(',');

                angular.forEach($scope.FloorList, function (val, key) {
                    val['IS_ACTIVE'] = _.some($localStorage.Floor, function (o) { return parseInt(o) == parseInt(val.HR_PROD_FLR_ID) });
                });


                //var idx = _.findIndex($scope.FloorList, function (o) {
                //    return parseInt(o.HR_PROD_FLR_ID) == parseInt($localStorage.Floor[0]);
                //});

                //$scope.FloorList[idx]['IS_ACTIVE'] = true;


            } else {
                HR_PROD_FLR_LST = pHR_PROD_FLR_LST;
                var FloorSplit = HR_PROD_FLR_LST.split(',');

                angular.forEach($scope.FloorList,function (val, key) {
                    val['IS_ACTIVE'] = _.some(FloorSplit, function (o) { return parseInt(o) == parseInt(val.HR_PROD_FLR_ID) });
                });

                

            };
            $scope.flrSelected = HR_PROD_FLR_LST;

            if (!pPROD_DT) {
                $scope.dtSelected = null;
                $('#start').text($filter('date')(new Date(),'dd-MMM-yyyy'));
            } else {
                $scope.dtSelected = pPROD_DT;
            }
           
           DisplayboardDataService.getDataByFullUrl('/api/common/getSewingProductionDashBoard?pHR_PROD_FLR_LST=' + HR_PROD_FLR_LST + '&pPROD_DT=' + pPROD_DT).then(function (res) {
               vm.boardData = res.map(function (o) {
                   o['MergeLine'] = o.MergeLine.join(',').split(',');
                   return o;
               });
            });
        }

        vm.loadData = function () {
            var activeFloorList = _.map(_.filter($scope.FloorList, function (o) {
                return o.IS_ACTIVE == true;
            }), 'HR_PROD_FLR_ID');

            clearInterval(interval);
            $scope.IS_AUTO_CHANGE = false;
            if (activeFloorList.length > 0) {
                getBoardData(activeFloorList.join(','), $scope.dtSelected);
            } else {
                getBoardData('-1', $scope.dtSelected);
            }
      
        }


        vm.isDisabled = function (lst,ln) {
            return _.some(lst, function (o) {
                return parseInt(o) == ln;
            });
        }


        vm.openConfigModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'HourlyProductionBoardConfigModal.html',
                controller: function ($scope, DisplayboardDataService, $modalInstance, FloorLineData, $q, $localStorage, FloorList) {
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

                    $scope.option = $localStorage.option || 'A';
                    $scope.Interval = $localStorage.Interval || 60;

                    $scope.floor = ($localStorage.Floor && $localStorage.Floor.length) > 0 ? $localStorage.Floor[0] : null;

                    $scope.lineList = FloorLineData.map(function (o) {
                        o['IS_SELECT_LINE'] = _.some($localStorage.LINE_LIST, function (b) {
                            return b == o.HR_PROD_LINE_ID;
                        });
                        return o;
                    });


                    $scope.FloorChange = function (HR_PROD_FLR_ID, IS_SELECT_FLR) {
                        angular.forEach($scope.lineList, function (val, key) {
                            if (val.HR_PROD_FLR_ID == HR_PROD_FLR_ID) {
                                val['IS_SELECT_LINE'] = IS_SELECT_FLR;
                            }
                        });
                    };
                    $scope.save = function (data) {

                        var Floor = [];
                        $scope.alerts = [];

                        if ($scope.option == 'A') {

                             $localStorage.option = $scope.option;
                             $localStorage.Interval = $scope.Interval;
                             $localStorage.Floor = _.map($scope.FloorList, 'HR_PROD_FLR_ID');
                             $localStorage.LINE_LIST = [];

                            $scope.alerts.push({
                                type: 'success', msg: 'Save Successful.'
                            });

                        } else if ($scope.option == 'F') {
                            if (!$scope.floor) {
                                $scope.alerts.push({
                                    type: 'danger', msg: 'Please Select Floor.'
                                });
                                return;
                            }
                            Floor.push($scope.floor);
                           


                            $scope.alerts.push({
                                type: 'success', msg: 'Save Successful.'
                            });

                            $localStorage.Interval = null;
                            $localStorage.option = $scope.option;
                            $localStorage.Floor = Floor;
                            $localStorage.LINE_LIST = [];

                        } else if ($scope.option == 'M') {

                            angular.forEach(data, function (val, key) {
                                if (val.IS_SELECT_LINE) {
                                    var idx = Floor.indexOf(val.HR_PROD_FLR_ID);
                                    if (idx < 0) {
                                        Floor.push(val.HR_PROD_FLR_ID);
                                    }
                                }
  
                            })

                            if (Floor.length == 0) {
                                $scope.alerts.push({
                                    type: 'danger', msg: 'Please select atleast one Floor'
                                });
                                return;
                            }

                            $localStorage.LINE_LIST = data;
                            $localStorage.option = $scope.option;
                            $localStorage.Floor = Floor;
                            $localStorage.Interval = null;

                            $scope.alerts.push({
                                type: 'success', msg: 'Save Successful.'
                            });
                        }
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss();
                    }

                    $scope.close = function () {
                        $modalInstance.close({SUCCESS:true});
                    }

                },
                size: 'md',
                windowClass: 'large-Modal',
                resolve: {
                    FloorLineData: function () {
                        return $scope.FloorLineData;
                    },
                    FloorList : function(){
                        return $scope.FloorList;
                    }
                    
                }
            });

            modalInstance.result.then(function (data) {

                if (data.SUCCESS) {
                    if ($localStorage.option === 'A') {
                        var default_index = 0;
                        var autoReload = function () {
                            var item = $localStorage.Floor[default_index];
                            getBoardData(item.toString(), null);

                            if (($localStorage.Floor.length - 1) == default_index) {
                                    default_index = 0;
                                } else {
                                    default_index += 1;
                                }
                        };
                        interval = setInterval(autoReload, ($localStorage.Interval || 60) * 1000);
                        $scope.IS_AUTO_CHANGE = true;
                    } else {
                        getBoardData($localStorage.Floor.join(','), null);
                        clearInterval(interval);
                        $scope.IS_AUTO_CHANGE = false;
                    }
                }





            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });


        };
  
    }

})();