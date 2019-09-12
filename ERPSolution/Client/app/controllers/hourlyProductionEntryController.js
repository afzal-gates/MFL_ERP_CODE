(function () {
    'use strict';
    angular.module('multitex').controller('HourlyProductionEntryController', ['$q', '$stateParams', '$state', '$scope', '$modal', '$localStorage', 'DashBoardService', 'FloorData', 'config', 'exception', 'cur_date_server', '$filter', HourlyProductionEntryController]);
    function HourlyProductionEntryController($q, $stateParams, $state, $scope, $modal, $localStorage, DashBoardService, FloorData, config, exception, cur_date_server, $filter) {

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


        //vm.LineLoadScreen = function (data) {
        //    var url = '/Home/LineLoadPlan/#/LineLoadPlan?pFLOOR_LIST=' + data.HR_PROD_FLR_ID + '&pIS_WIN_SCREEN=true';
        //    var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 10) + ',scrollbars=yes,status=yes';
        //    $window.open(url, "_blank", opt);
        //};


        vm.printEfficiencyReport = function (Date,IS_EXCEL) {
            var url = '/api/pln/PlnReport/PreviewReportRDLC';

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');
            var params = {
                REPORT_CODE: 'RPT-9000',
                PROD_DT: Date,
                IS_EXCEL_FORMAT: IS_EXCEL || 'N' 
            };


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


        vm.onChangeReason = function (itm) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'FaultReasonChangeModal.html',
                controller: function ($scope, $modalInstance, $q, LineInfo, DashBoardService) {
                    activate()
                    $scope.form = {
                        RSN_TYPE_NAME_EN: LineInfo.RSN_TYPE_NAME_EN,
                        RF_PFLT_RSN_TYPE_ID: LineInfo.RF_PFLT_RSN_TYPE_ID
                    };

                    $scope.showSplash = true;
                    function activate() {
                        var promise = [getPerformaceFaultData()];
                        return $q.all(promise).then(function () {
                            $scope.showSplash = false;
                        });
                    }


                    function getPerformaceFaultData() {
                      return  DashBoardService.getDataByUrl('/api/common/getPerformanceFaultReasonData').then(function (res) {
                          $scope.dataSource = new kendo.data.DataSource({
                              data: res
                          });
                        });
                    };

                    $scope.onChangeReason = function (e) {
                        var item = e.sender.dataItem(e.sender.item);
                        if (item.RF_PFLT_RSN_TYPE_ID && item.RF_PFLT_RSN_TYPE_ID>0) {
                            $scope.form['RSN_TYPE_NAME_EN'] = item.RSN_TYPE_NAME_EN;
                        } else if (item.RF_PFLT_RSN_TYPE_ID && item.RF_PFLT_RSN_TYPE_ID == -100) {
                            $scope.form['RSN_TYPE_NAME_EN'] = '';
                        }
                    }

                    $scope.save = function (data) {
                        if (data.RF_PFLT_RSN_TYPE_ID && parseInt(data.RF_PFLT_RSN_TYPE_ID) == -100) {
                            return DashBoardService.saveDataByUrl(data, '/api/common/savePerformanceFaultReason').then(function (res) {
                                var o = angular.fromJson(res);
                                if (o.PMSG.substr(0, 9) == 'MULTI-001') {
                                    $modalInstance.close({
                                        RF_PFLT_RSN_TYPE_ID: parseInt(o.OP_RF_PFLT_RSN_TYPE_ID),
                                        RSN_TYPE_NAME_EN: data.RSN_TYPE_NAME_EN
                                    });
                                      
                                }
                            }).catch(function (message) {
                                exception.catcher('XHR loading Failded')(message);
                            });
                        } else {
                            $modalInstance.close({
                                RF_PFLT_RSN_TYPE_ID: data.RF_PFLT_RSN_TYPE_ID,
                                RSN_TYPE_NAME_EN: data.RSN_TYPE_NAME_EN
                            });
                        }


                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss(null);
                    }
                },
                size: 'md',
                windowClass: 'large-Modal',
                resolve: {
                    LineInfo: function () {
                        return itm;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);

                if (data) {
                    itm['RSN_TYPE_NAME_EN'] = data.RSN_TYPE_NAME_EN;
                    itm['RF_PFLT_RSN_TYPE_ID'] = data.RF_PFLT_RSN_TYPE_ID;
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

        vm.onChangeGenMPTarget = function (data, tf) {
            angular.forEach(data, function (val, key) {
                val['IS_GH_TGT_LOCK'] = tf;
            });
        };

        vm.onChangeGenProduction = function (data, tf, hr) {
            angular.forEach(data, function (val, key) {
                val['CHK_GH_PRD_'+hr] = tf;
            });
        };


        vm.onChangeOTGenMPTarget = function (data, tf) {
            angular.forEach(data, function (val, key) {
                val['IS_OTH_TGT_LOCK'] = tf;
            });
        };

        vm.onChangeACTProduction = function (data, tf) {
            angular.forEach(data, function (val, key) {
                val['IS_OT_ACT_LOCK'] = tf;
            });
        };




        vm.ClosePlan = function (items, idx) {
            items[idx]['IS_PLAN_ACTIVE'] = 'N';
            vm.save(items,true);
        }

        vm.OnChangeFloor = function (itm) {
            getLineListData(itm.HR_PROD_FLR_ID, '', vm.selectedDt);
            $state.go('HourlyProductionEntry', {}, { notify: false, inherit: false });
            vm.selectedFlr = itm.HR_PROD_FLR_ID;
        };

        vm.refresh = function () {
            getLineListData(vm.selectedFlr, '',vm.selectedDt);
        }

        function getLineListData(pHR_PROD_FLR_LST,pHR_PROD_LINE_LST,DT) {
            return DashBoardService.getDataByUrl('/api/common/getLineLoadingPlanDataEntry?pHR_PROD_FLR_LST=' + pHR_PROD_FLR_LST + '&pHR_PROD_LINE_LST=' + (pHR_PROD_LINE_LST || '') + '&pPROD_DT=' + DT).then(function (res) {
                var mergeLine = res.MergeLine.join(',').split(',');
                vm.FloorLineData = res.data.map(function (o) {
                    o['IS_HIDE'] = _.some(mergeLine, function (p) {
                        return parseInt(p) == o.HR_PROD_LINE_ID;
                    });
                    return o;
                });


            });
        }

        $scope.$watch('vm.FloorLineData', function (newVal, oldVal) {

            vm.USE_OPERATOR_TTL = _.sumBy(newVal, function (o) { return (o.USE_OPERATOR || 0); });
            vm.USE_HELPER_TTL = _.sumBy(newVal, function (o) { return (o.USE_HELPER || 0); });

            vm.HRLY_TGT_QTY_TTL = _.sumBy(newVal, function (o) { return (o.HRLY_TGT_QTY || 0); });

            vm.DAILY_TGT_QTY_TTL = _.sumBy(newVal, function (o) { return (o.DAILY_TGT_QTY || 0); });

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

            vm.CHK_GH_PRD_1 = _.every(newVal, function (o) { return o.CHK_GH_PRD_1_D_S == 'Y'; });
            vm.CHK_GH_PRD_2 = _.every(newVal, function (o) { return o.CHK_GH_PRD_2_D_S == 'Y'; });
            vm.CHK_GH_PRD_3 = _.every(newVal, function (o) { return o.CHK_GH_PRD_3_D_S == 'Y'; });
            vm.CHK_GH_PRD_4 = _.every(newVal, function (o) { return o.CHK_GH_PRD_4_D_S == 'Y'; });
            vm.CHK_GH_PRD_5 = _.every(newVal, function (o) { return o.CHK_GH_PRD_5_D_S == 'Y'; });
            vm.CHK_GH_PRD_6 = _.every(newVal, function (o) { return o.CHK_GH_PRD_6_D_S == 'Y'; });
            vm.CHK_GH_PRD_7 = _.every(newVal, function (o) { return o.CHK_GH_PRD_7_D_S == 'Y'; });
            vm.CHK_GH_PRD_8 = _.every(newVal, function (o) { return o.CHK_GH_PRD_8_D_S == 'Y'; });

            vm.IS_GH_TGT_LOCK = 'Y';
            vm.IS_OTH_TGT_LOCK = _.every(newVal, function (o) { return o.IS_OTH_TGT_LOCK_S == 'Y'; });
            vm.IS_OT_ACT_LOCK = _.every(newVal, function (o) { return o.IS_OT_ACT_LOCK_S == 'Y'; });


            vm.CHK_GH_PRD_1_S = _.every(newVal, function (o) { return o.CHK_GH_PRD_1 == 'Y'; })? "Y":"N" ;
            vm.CHK_GH_PRD_2_S = _.every(newVal, function (o) { return o.CHK_GH_PRD_2 == 'Y'; })? "Y":"N" ;
            vm.CHK_GH_PRD_3_S = _.every(newVal, function (o) { return o.CHK_GH_PRD_3 == 'Y'; })? "Y":"N" ;
            vm.CHK_GH_PRD_4_S = _.every(newVal, function (o) { return o.CHK_GH_PRD_4 == 'Y'; })? "Y":"N" ;
            vm.CHK_GH_PRD_5_S = _.every(newVal, function (o) { return o.CHK_GH_PRD_5 == 'Y'; })? "Y":"N" ;
            vm.CHK_GH_PRD_6_S = _.every(newVal, function (o) { return o.CHK_GH_PRD_6 == 'Y'; })? "Y":"N" ;
            vm.CHK_GH_PRD_7_S = _.every(newVal, function (o) { return o.CHK_GH_PRD_7 == 'Y'; })? "Y":"N" ;
            vm.CHK_GH_PRD_8_S = _.every(newVal, function (o) { return o.CHK_GH_PRD_8 == 'Y'; })? "Y":"N" ;

            vm.IS_GH_TGT_LOCK_S = 'Y' ;
            vm.IS_OTH_TGT_LOCK_S = _.every(newVal, function (o) { return o.IS_OTH_TGT_LOCK == 'Y'; })? "Y":"N" ;
            vm.IS_OT_ACT_LOCK_S = _.every(newVal, function (o) { return o.IS_OT_ACT_LOCK == 'Y'; }) ? "Y" : "N";

            angular.forEach(newVal, function (val, key) {
                if (val.SMV && val.SMV > 0) {
                    val.HRLY_TGT_QTY = Math.ceil(((((val.USE_OPERATOR || 0) + (val.USE_HELPER || 0)) * 60) / val.SMV) * ((val.HRLY_TGT_EFF || 0) / 100));

                    val.DAILY_TGT_QTY = Math.ceil(((((val.USE_OPERATOR || 0) + (val.USE_HELPER || 0)) * 60 * val.GEN_TGT_HR) / val.SMV) * ((val.HRLY_TGT_EFF || 0) / 100));

                } else {
                    val.HRLY_TGT_QTY = 0;
                    val.DAILY_TGT_QTY = 0;
                }

            });



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
                        return DashBoardService.getDataByUrl('/api/common/GetSewingLineData');
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

        function makeCHK_GH_PRD(data) {
            var ret = [];
            for (var i = 1; i <= 8; i++) {
               
                console.log(data['CHK_GH_PRD_' + i]);
                if (data['CHK_GH_PRD_' + i] == 'Y')
                    ret.push(i);
            }
         vm.GEN_HOUR_LOCK = ret.join("");
        };


        vm.save = function (dataOri,isValid) {
            if (!isValid) {
                return;
            }

            makeCHK_GH_PRD(dataOri[0]);

          var  data2bSave = dataOri.map(function (item) {
              return    {
                  GMT_HRLY_PROD_ID: item.GMT_HRLY_PROD_ID,
                  GMT_LN_LOAD_PLAN_ID: item.GMT_LN_LOAD_PLAN_ID,
                  HR_PROD_LINE_ID: item.HR_PROD_LINE_ID,
                  H1_PRD_QTY: item.H1_PRD_QTY,
                  H2_PRD_QTY: item.H2_PRD_QTY,
                  H3_PRD_QTY: item.H3_PRD_QTY,
                  H4_PRD_QTY: item.H4_PRD_QTY,
                  H5_PRD_QTY: item.H5_PRD_QTY,
                  H6_PRD_QTY: item.H6_PRD_QTY,
                  H7_PRD_QTY: item.H7_PRD_QTY,
                  H8_PRD_QTY: item.H8_PRD_QTY,
                  OT_TGT_HR: item.OT_TGT_HR,
                  LK_FLOOR_ID: item.LK_FLOOR_ID,
                  LINE_NO : item.LINE_NO,
                  OT_TGT_QTY: item.OT_TGT_QTY,
                  OT_ACT_PRD_QTY: item.OT_ACT_PRD_QTY,
                  HRLY_TGT_QTY: item.HRLY_TGT_QTY,
                  NO_OF_MC: item.NO_OF_MC,
                  REMARKS: item.REMARKS,
                  IS_PLAN_ACTIVE: item.IS_PLAN_ACTIVE ? item.IS_PLAN_ACTIVE : 'Y',

                  RF_PFLT_RSN_TYPE_ID: item.RF_PFLT_RSN_TYPE_ID,

                  CUR_OPERATOR: item.CUR_OPERATOR,
                  CUR_HELPER: item.CUR_HELPER,

                  USE_OPERATOR: item.USE_OPERATOR,
                  USE_HELPER: item.USE_HELPER,
                  PROD_DT: item.PROD_DT.split('T')[0],
                  CHK_GH_PRD: vm.GEN_HOUR_LOCK,
                  IS_GH_TGT_LOCK: item.IS_GH_TGT_LOCK,
                  IS_OTH_TGT_LOCK: item.IS_OTH_TGT_LOCK,
                  IS_OT_ACT_LOCK: item.IS_OT_ACT_LOCK,
                  HRLY_TGT_EFF: item.HRLY_TGT_EFF,
                  CT_TGT_QTY: item.CT_TGT_QTY,
                  CT_PRD_QTY: item.CT_PRD_QTY,
                  GEN_TGT_HR: item.GEN_TGT_HR
                  
              }
          });

           
          return DashBoardService.saveDataByUrl({XML : config.xmlStringShort(data2bSave),pOption : 1001 }, '/api/common/SaveLineLoadingPlanData').then(function (res) {
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