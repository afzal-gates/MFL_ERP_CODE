(function () {
    'use strict';
    angular.module('multitex').controller('LineLoadPlanController', ['$q', '$stateParams', '$state', '$scope', '$modal', '$localStorage', 'DashBoardService', 'FloorData', 'config', 'exception', 'BuyerAcc', lineLoadPlanController]);
    function lineLoadPlanController($q, $stateParams, $state, $scope, $modal, $localStorage, DashBoardService, FloorData, config, exception, BuyerAcc) {

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
        $scope.FloorList = FloorData;

        vm.isWindowScreen = $stateParams.pIS_WIN_SCREEN;


        $scope.dtFormat = config.appDateFormat;
        $scope.SewingStartDateOpened = [];
        $scope.SewingStartDateOpen = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.SewingStartDateOpened[index] = true;
        };



        vm.openlineMergeModal = function (CurLine,List) {

            var mgLnList = CurLine.PROD_LINE_LST.length > 0 ? CurLine.PROD_LINE_LST.split('') : [];
            var lnList = _.filter(List, function (o) {
                return o.HR_PROD_LINE_ID != CurLine.HR_PROD_LINE_ID && o.GMT_LN_LOAD_PLAN_ID>0;
            });

            var mgLnList2 = _.map(lnList, 'PROD_LINE_LST').join(',').split(',');




            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'LineMergeModal.html',
                controller: function ($scope, $modalInstance, $q, LineInfo, CurLine, DashBoardService) {
                    activate()

                    function activate() {
                        var promise = [];
                        return $q.all(promise).then(function () {

                        });
                    }

                    $scope.LINE_CODE = CurLine.LINE_CODE.split('+')[0];
                    $scope.LineList = LineInfo;

                    $scope.save = function (data) {

                        var dt = {
                            XML: config.xmlStringShort([{
                                GMT_LN_LOAD_PLAN_ID: CurLine.GMT_LN_LOAD_PLAN_ID,
                                PROD_LINE_LST: _.map(_.filter(data, function (o) {
                                    return o.IS_CHECKED == true;
                                }), 'HR_PROD_LINE_ID').join(','),

                            }]),
                            pOption: 1002
                        };
                        return DashBoardService.saveDataByUrl(dt,'/api/common/SaveLineLoadingPlanData').then(function (res) {
                            var o = angular.fromJson(res);
                            if (o.PMSG.substr(0, 9) == 'MULTI-001') {
                                $modalInstance.close({
                                    SUCCESS: true
                                });

                            }
                        }).catch(function (message) {
                            exception.catcher('XHR loading Failded')(message);
                        });
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss(null);
                    }
                },
                size: 'md',
                windowClass: 'large-Modal',
                resolve: {
                    LineInfo: function () {
                        return lnList.map(function (o) {
                            return {
                                HR_PROD_LINE_ID: o.HR_PROD_LINE_ID,
                                LINE_CODE: o.LINE_CODE,
                                IS_CHECKED: _.some(mgLnList, function (p) {
                                    return parseInt(p) == o.HR_PROD_LINE_ID;
                                }),
                                IS_HIDE: _.some(mgLnList2, function (p) {
                                    return parseInt(p) == o.HR_PROD_LINE_ID;
                                })|| o.PROD_LINE_LST.length>0
                            }
                        });
                    },
                    CurLine: function () {
                        return CurLine;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data) {
                    getLineListData(vm.selectedFlr, '');
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }



        if ($stateParams.pFLOOR_LIST) {
            getLineListData($stateParams.pFLOOR_LIST, ($stateParams.pLINE_LIST || ''));
            vm.selectedFlr = parseInt($stateParams.pFLOOR_LIST);
            
        } else {
            if (!$localStorage.LLP_FLOOR_LIST) {
                vm.selectedFlr = FloorData[0].HR_PROD_FLR_ID;

                getLineListData(FloorData[0].HR_PROD_FLR_ID, null);
            } else {
                getLineListData($localStorage.LLP_FLOOR_LIST[0], $localStorage.LLP_LINE_LIST ? $localStorage.LLP_LINE_LIST.join(',') : '');
                vm.selectedFlr = $localStorage.LLP_FLOOR_LIST[0];
            }
        }


        function getLineListData(pHR_PROD_FLR_LST,pHR_PROD_LINE_LST) {
            return DashBoardService.getDataByUrl('/api/common/getLineLoadingPlanData?pHR_PROD_FLR_LST=' + pHR_PROD_FLR_LST + '&pHR_PROD_LINE_LST=' + (pHR_PROD_LINE_LST||'')).then(function (res) {
                var mergeLine = res.MergeLine.join(',').split(',');
                vm.FloorLineData = res.data.map(function (o) {
                    o['BuyerAccDs'] =  new kendo.data.DataSource({
                        data: BuyerAcc
                    });

                    o['IS_HIDE'] = _.some(mergeLine, function (p) {
                       return parseInt(p) == o.HR_PROD_LINE_ID;
                    });
                    return o;
                });
            });
        }

        vm.OnChangeFloor = function (itm) {
            getLineListData(itm.HR_PROD_FLR_ID, '');
            $state.go('LineLoadPlan', {}, { notify: false, inherit: false });
            vm.selectedFlr = itm.HR_PROD_FLR_ID;
        };

        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO #</h5><p> Style: #: data.STYLE_NO #</p></span>';

        vm.onBuyerAccChange = function (item,e) {
            var obj = e.sender.dataItem(e.sender.item);
            if (obj.MC_BYR_ACC_ID) {

                item['oderListDs'] = {
                    transport: {
                        read: function (e) {
                            var url = "/api/common/getOrderStyleDropDownData?pMC_BYR_ACC_ID=" + obj.MC_BYR_ACC_ID;
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            if (params.filter) {
                                url += '&pORDER_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                            } else {
                                url += '&pORDER_NO';
                            }

                            return DashBoardService.getDataByUrl(url).then(function (res) {
                                e.success(res);
                            });
                        }
                    },
                    serverFiltering: true
                };

                item['BYR_ACC_NAME_EN'] = obj.BYR_ACC_NAME_EN;
            } else {
                item['oderListDs'] = new kendo.data.DataSource({
                    data: []
                });

                item['itemListDs'] = new kendo.data.DataSource({
                    data: []
                });
                item['BYR_ACC_NAME_EN'] = null;
            }
        };

        vm.onOrderChange = function (item, e) {
            var objj = e.sender.dataItem(e.sender.item);
            if (objj.MC_ORDER_H_ID) {

                DashBoardService.getDataByUrl("/api/common/getOrderStyleItemDropDownData?pMC_ORDER_H_ID=" + objj.MC_ORDER_H_ID + "&pMC_ORDER_SHIP_ID=" + objj.MC_ORDER_SHIP_ID).then(function (res) {
                    item['itemListDs'] = new kendo.data.DataSource({
                        data: res
                    });
                });
                item['ORDER_NO'] = objj.ORDER_NO;
            } else {
                item['itemListDs'] = new kendo.data.DataSource({
                    data: []
                });

                item['ORDER_NO'] = objj.ORDER_NO;
            }
        };

        vm.onItemDataBound = function (item, e) {
            var ds = e.sender.dataSource.data();
            if (ds.length == 1) {
                e.sender.value(ds[0].MC_ORDER_STYL_ID);
                item['MC_ORDER_STYL_ID'] = ds[0].MC_ORDER_STYL_ID;
                item['ITEM_NAME_EN'] = ds[0].ITEM_NAME_EN;
            }
        };

        vm.onItemChange = function (item, e) {
            var objjj = e.sender.dataItem(e.sender.item);
            if (objjj.MC_ORDER_STYL_ID) {
                item['ITEM_NAME_EN'] = objjj.ITEM_NAME_EN;
            } else {
                item['ITEM_NAME_EN'] = null;
            }
        };

        vm.edit = function (item) {
            item.IS_EDITABLE = !item.IS_EDITABLE;
            if (item.IS_EDITABLE) {
                item['oderListDs'] = {
                    transport: {
                        read: function (e) {
                            var url = "/api/common/getOrderStyleDropDownData?pMC_BYR_ACC_ID=" + item.MC_BYR_ACC_ID;
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            if (params.filter) {
                                url += '&pORDER_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                            } else {
                                url += '&pORDER_NO';
                            }

                            url += '&pMC_ORDER_H_ID=' + item.MC_ORDER_STYL_ID;

                            DashBoardService.getDataByUrl(url).then(function (res) {
                                e.success(res);
                            });
                        }
                    },
                    serverFiltering: true
                }


                DashBoardService.getDataByUrl("/api/common/getOrderStyleItemDropDownData?pMC_ORDER_H_ID=" + item.MC_ORDER_H_ID + '&pMC_ORDER_SHIP_ID=' + item.MC_ORDER_SHIP_ID).then(function (res) {
                    item['itemListDs'] = new kendo.data.DataSource({
                        data: res
                    });
                });
            }


          
        };
        
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

                    $scope.option_llp = $localStorage.option_llp || 'A';

                    $scope.floor_llp = ($localStorage.LLP_FLOOR_LIST && $localStorage.LLP_FLOOR_LIST.length) > 0 ? $localStorage.LLP_FLOOR_LIST[0] : null;

                    $scope.lineList = FloorLineData.map(function (o) {
                        o['IS_SELECT_LINE'] = _.some($localStorage.LLP_LINE_LIST, function (b) {
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

                        if ($scope.option_llp == 'F') {
                            if (!$scope.floor_llp) {
                                $scope.alerts.push({
                                    type: 'danger', msg: 'Please Select Floor.'
                                });
                                return;
                            }
                            Floor.push($scope.floor_llp);
                            $scope.alerts.push({
                                type: 'success', msg: 'Save Successful.'
                            });

                            $localStorage.option_llp = $scope.option_llp;
                            $localStorage.LLP_FLOOR_LIST = Floor;
                            $localStorage.LLP_LINE_LIST = [];

                            setTimeout(function () {
                                $modalInstance.close([$localStorage.LLP_FLOOR_LIST[0], '']);
                            }, 500);
                           


                        } else if ($scope.option_llp == 'M') {
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

                            $localStorage.LLP_LINE_LIST = _.map(data,'HR_PROD_LINE_ID');
                            $localStorage.option_llp = $scope.option_llp;
                            $localStorage.LLP_FLOOR_LIST = Floor;

                            $scope.alerts.push({
                                type: 'success', msg: 'Save Successful.'
                            });
                            setTimeout(function () {
                                $modalInstance.close([$localStorage.LLP_FLOOR_LIST[0], $localStorage.LLP_LINE_LIST.join(',')]);
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
                        return DashBoardService.getDataByUrl('/api/common/getLineLoadingPlanData').then(function (res) {
                            return res.data;
                        });
                    },
                    FloorList : function(){
                        return $scope.FloorList;
                    }
                    
                }
            });

            modalInstance.result.then(function (data) {
                if (data.length > 0) {
                    getLineListData(data[0], data[1]);
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
              return {
                  GMT_LN_LOAD_PLAN_ID : item.GMT_LN_LOAD_PLAN_ID,
                  MC_ORDER_STYL_ID : item.MC_ORDER_STYL_ID,
                  HR_PROD_LINE_ID : item.HR_PROD_LINE_ID,
                  SEW_START_DT: moment(item.SEW_START_DT).format("DD-MMM-YYYY"),
                  HRLY_TGT_QTY : item.HRLY_TGT_QTY,
                  QTY_MOU_ID : 1,
                  REQ_OPERATOR : item.REQ_OPERATOR,
                  REQ_HELPER : item.REQ_HELPER,
                  SMV : item.SMV,
                  REMARKS : item.REMARKS
              };
          });

          return DashBoardService.saveDataByUrl({ XML: config.xmlStringShort(data2bSave), pOption: 1000 }, '/api/common/SaveLineLoadingPlanData').then(function (res) {
              var o = angular.fromJson(res);

              if (o.PMSG.substr(0, 9) == 'MULTI-001') {
                  getLineListData(vm.selectedFlr, '');
              }
              config.appToastMsg(o.PMSG);


          }).catch(function (message) {
              exception.catcher('XHR loading Failded')(message);
          });

        }

    }

})();