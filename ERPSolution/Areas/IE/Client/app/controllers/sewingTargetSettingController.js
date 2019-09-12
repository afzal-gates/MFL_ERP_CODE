(function () {
    'use strict';
    angular.module('multitex').controller('SewingTargetSettingController', ['$q', '$stateParams', '$state', '$scope', '$modal', '$localStorage', 'IeDataService', 'FloorData', 'config', 'exception', 'cur_date_server', '$filter', 'Dialog', SewingTargetSettingController]);
    function SewingTargetSettingController($q, $stateParams, $state, $scope, $modal, $localStorage, IeDataService, FloorData, config, exception, cur_date_server, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.selectedFlr = -1;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.selectedDt = new Date().toISOString();
        if ($state.current.name === 'SewingTargetSetting') {
            $scope.isTargetSetting = true;
        } else if ($state.current.name === 'GmtManualProductionEntry') {
            $scope.isTargetSetting = false;
        };

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
            vm.selectedFlr = parseInt($stateParams.pFLOOR_LIST);
            getLineListData(parseInt($stateParams.pFLOOR_LIST), '', vm.selectedDt);
        } else {
            if (!$localStorage.HPE_FLOOR_LIST) {
                vm.selectedFlr = FloorData[0].HR_PROD_FLR_ID;
                getLineListData(FloorData[0].HR_PROD_FLR_ID, null,vm.selectedDt);
            } else {
                vm.selectedFlr = $localStorage.HPE_FLOOR_LIST[0];
                getLineListData($localStorage.HPE_FLOOR_LIST[0], '',vm.selectedDt);
             
            }
        }


        //vm.LineLoadScreen = function (data) {
        //    var url = '/Home/LineLoadPlan/#/LineLoadPlan?pFLOOR_LIST=' + data.HR_PROD_FLR_ID + '&pIS_WIN_SCREEN=true';
        //    var opt = 'location=yes,height=570,width=' + ($window.outerWidth - 10) + ',scrollbars=yes,status=yes';
        //    $window.open(url, "_blank", opt);
        //};


        vm.onChangeReason = function (itm) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'FaultReasonChangeModal.html',
                controller: function ($scope, $modalInstance, $q, LineInfo, IeDataService) {
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
                        return IeDataService.getDataByFullUrl('/api/common/getPerformanceFaultReasonData').then(function (res) {
                            $scope.dataSource = new kendo.data.DataSource({
                                data: res
                            });
                        });
                    };

                    $scope.onChangeReason = function (e) {
                        var item = e.sender.dataItem(e.sender.item);
                        if (item.RF_PFLT_RSN_TYPE_ID && item.RF_PFLT_RSN_TYPE_ID > 0) {
                            $scope.form['RSN_TYPE_NAME_EN'] = item.RSN_TYPE_NAME_EN;
                        } else if (item.RF_PFLT_RSN_TYPE_ID && item.RF_PFLT_RSN_TYPE_ID == -100) {
                            $scope.form['RSN_TYPE_NAME_EN'] = '';
                        }
                    }

                    $scope.save = function (data) {
                        if (data.RF_PFLT_RSN_TYPE_ID && parseInt(data.RF_PFLT_RSN_TYPE_ID) == -100) {
                            return IeDataService.saveDataByFullUrl(data, '/api/common/savePerformanceFaultReason').then(function (res) {
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
                if (data) {
                    itm['RSN_TYPE_NAME_EN'] = data.RSN_TYPE_NAME_EN;
                    itm['RF_PFLT_RSN_TYPE_ID'] = data.RF_PFLT_RSN_TYPE_ID;
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

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

        vm.onCalculateHourTarget = function (itm) {
            var SAM = (itm.USED_HLP + itm.USED_OP) * 60 * (itm.TARGET_EFF / 100);
            itm.HRLY_TARGET = Math.ceil(SAM / itm.SMV);
            itm.DAY_TARGET = Math.ceil(SAM / itm.SMV) * itm.TARGET_WH;
        };

        

        vm.OnChangeFloor = function (itm) {
            getLineListData(itm.HR_PROD_FLR_ID, '', vm.selectedDt);
            $state.go($state.current.name, {}, { notify: false, inherit: false });
            vm.selectedFlr = itm.HR_PROD_FLR_ID;
        };

        vm.refresh = function () {
            getLineListData(vm.selectedFlr, '',vm.selectedDt);
        }

        $scope.findTabIndex = function (key, index, col) {
            return parseInt((key + 1).toString() + (index + 1).toString()) + col;
        }


        function getLineListData(pHR_PROD_FLR_ID, pHR_PROD_LINE_LST, DT) {
            vm.CUR_FLR = FloorData.find(function (ob) { return ob.HR_PROD_FLR_ID == pHR_PROD_FLR_ID; });
             return IeDataService.getDataByUrl('/GmtIeTarget/GetDataForTargetSetting?pRF_CALENDAR_DATE=' + DT + '&pHR_PROD_FLR_ID=' + pHR_PROD_FLR_ID).then(function (res) {
                vm.data = res;
            });
        }

        $scope.onChangeVal = function (a) {
            angular.forEach(a.lines, function (val, key) {
                val.H1_HR = val.items.reduce(function (a, b) { return a + (b.H1_HR || 0); }, 0);
                val.H2_HR = val.items.reduce(function (a, b) { return a + (b.H2_HR || 0); }, 0);
                val.H3_HR = val.items.reduce(function (a, b) { return a + (b.H3_HR || 0); }, 0);
                val.H4_HR = val.items.reduce(function (a, b) { return a + (b.H4_HR || 0); }, 0);
                val.H5_HR = val.items.reduce(function (a, b) { return a + (b.H5_HR || 0); }, 0);
                val.H6_HR = val.items.reduce(function (a, b) { return a + (b.H6_HR || 0); }, 0);
                val.H7_HR = val.items.reduce(function (a, b) { return a + (b.H7_HR || 0); }, 0);
                val.H8_HR = val.items.reduce(function (a, b) { return a + (b.H8_HR || 0); }, 0);
                val.ACHV_QTY_OTWH = val.items.reduce(function (a, b) { return a + (b.ACHV_QTY_OTWH || 0); }, 0);
            });
            setTimeout(function () {
                a.H1_HR = a.lines.reduce(function (a, b) { return a + (b.H1_HR || 0); }, 0);
                a.H2_HR = a.lines.reduce(function (a, b) { return a + (b.H2_HR || 0); }, 0);
                a.H3_HR = a.lines.reduce(function (a, b) { return a + (b.H3_HR || 0); }, 0);
                a.H4_HR = a.lines.reduce(function (a, b) { return a + (b.H4_HR || 0); }, 0);
                a.H5_HR = a.lines.reduce(function (a, b) { return a + (b.H5_HR || 0); }, 0);
                a.H6_HR = a.lines.reduce(function (a, b) { return a + (b.H6_HR || 0); }, 0);
                a.H7_HR = a.lines.reduce(function (a, b) { return a + (b.H7_HR || 0); }, 0);
                a.H8_HR = a.lines.reduce(function (a, b) { return a + (b.H8_HR || 0); }, 0);
                a.ACHV_QTY_OTWH = a.lines.reduce(function (a, b) { return a + (b.ACHV_QTY_OTWH || 0); }, 0);
            }, 300);
        }
        
        vm.openConfigModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'HourlyProductionBoardConfigModal.html',
                controller: function ($scope, $modalInstance, FloorLineData, $q, $localStorage, FloorList) {
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
                        //angular.forEach($scope.lineList, function (val, key) {
                        //    if (val.HR_PROD_FLR_ID == HR_PROD_FLR_ID) {
                        //        val['IS_SELECT_LINE'] = IS_SELECT_FLR;
                        //    }
                        //});
                    };

                    $scope.option_hpe = $localStorage.option_hpe || 'A';

                    $scope.floor_hpe = ($localStorage.HPE_FLOOR_LIST && $localStorage.HPE_FLOOR_LIST.length) > 0 ? $localStorage.HPE_FLOOR_LIST[0] : null;

                    //$scope.lineList = FloorLineData.map(function (o) {
                    //    o['IS_SELECT_LINE'] = _.some($localStorage.HPE_LINE_LIST, function (b) {
                    //        return b == o.HR_PROD_LINE_ID;
                    //    });
                    //    return o;
                    //});

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
                        return IeDataService.getDataByFullUrl('/api/common/GetSewingLineData');
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

        $scope.findColorCode = function (d, h) {
            if ( d.prods ) {
                let idx = d.prods.findIndex(function (x) { return x.HOUR_NO === h });
                if (idx < 0) {
                    return 'white';
                } else {
                    if ((d.prods[idx].PROD_QTY / d.HRLY_TARGET) < 0.85) {
                        return 'orange';
                    } else {
                        return 'white';
                    }
                }
            } else {
                return 'white';
            }
        };

        $scope.findTitle= function(d, h) {
            if ( d.prods ) {
                let idx =  d.prods.findIndex( function (x) { return x.HOUR_NO === h; });
                if (idx < 0) {
                    return '';
                } else {
                    if ((d.prods[idx].PROD_QTY / d.HRLY_TARGET) < 0.85) {
                        return d.prods[idx]['REASON_DESC_EN'];
                    } else {
                        return '';
                    }
                }
            } else {
                return '';
            }
        };

        vm.openOrderAllocationCnfModal = function (pHR_PROD_LINE_ID, pGMT_PROD_PLN_CLNDR_ID, pLINE_CODE) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/ie/ie/_OrderAllocationCnfModal',
                controller: 'OrderAllocationCnfModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {

                    plan_datas: function () {
                        return IeDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getProdPlanDataByLine?pOption=3018&pHR_PROD_LINE_ID=' + pHR_PROD_LINE_ID);
                    },

                    V_GMT_PROD_PLN_CLNDR_ID: function () {
                        return pGMT_PROD_PLN_CLNDR_ID;
                    },
                    V_LINE_CODE: function () {
                        return pLINE_CODE;
                    }
                }
            });
            modalInstance.result.then(function () {
                return getLineListData(vm.selectedFlr, '', vm.selectedDt);
            });
        }

        vm.openOutPlanProdEntryModal = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/ie/ie/_OutPlanProdEntryModal',
                controller: 'OutOfPlanProdEntryModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    plan_datas: function () {
                        return IeDataService.
                         getDataByFullUrl('/api/pln/GmtLineLoad/getProdPlanDataByLine?pOption=3020&pHR_PROD_LINE_ID=' + data.HR_PROD_LINE_ID + '&pSTART_DT=' + moment().add(-15, 'days').format('MM/DD/YYYY') + '&pEND_DT=' + moment().format('MM/DD/YYYY'));
                    },
                    LineData: function () {
                        return data;
                    }
                }
            });
            modalInstance.result.then(function () {
                return getLineListData(vm.selectedFlr, '', vm.selectedDt);
            });
        }




        vm.openIeNptModal = function (pGMT_PROD_PLN_CLNDR_ID, pHR_PROD_LINE_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/ie/ie/_GmtIeNptModal',
                controller: 'GmtIeNptModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    npts_data: function () {
                        return IeDataService.getDataByUrl('/GmtIeTarget/GetNptData?pGMT_PROD_PLN_CLNDR_ID=' + pGMT_PROD_PLN_CLNDR_ID + '&pHR_PROD_LINE_ID=' + pHR_PROD_LINE_ID);
                    },
                    ResDept: function () {
                        return IeDataService.getDataByFullUrl('/api/common/getRespDeptList');
                    },
                    npt_reasons_datas: function () {
                        return IeDataService.getDataByUrl('/GmtIeTarget/GetItNptReasonData?pOption=3000');
                    }
                }
            });
            modalInstance.result.then(function () {
               //return getLineListData(vm.selectedFlr, '', vm.selectedDt);
            });
        }


        $scope.is_locked = function (H_HR, prod) {
            if (prod.length > 0) {
                var idx = prod.findIndex(function (b) { return b.HOUR_NO == H_HR; });

                if (idx > -1) {
                    return prod[idx].IS_LOCKED;
                } else {
                    true;
                }
            } else {
                return false;
            }
        }

        vm.submitData = function (dataOri, isValid, is_locked) {
           
            if (!isValid) {
                return;
            }
            var data2bSave = [];
            var hr_prod = [];
            angular.forEach(dataOri.lines, function (val) {
                angular.forEach(val.items, function (v) {
                    if ($state.current.name === 'GmtManualProductionEntry') {
                        Object.keys(v).forEach(function (b) {

                            if (b.slice(-3) === '_HR'  &&  v[b] !== null  && v.GMT_IE_TARGET_ID > 0) {
                                if (b.length == 5 && parseInt(b.substr(1, 2)) < 9 ) {
                                    hr_prod.push({ GMT_IE_TARGET_ID: v.GMT_IE_TARGET_ID, HOUR_NO: b.substr(1, 1), PROD_QTY: v[b], IS_LOCKED : is_locked });
                                } else if (b.length == 6 && parseInt(b.substr(1, 2)) < 9) {
                                    hr_prod.push({ GMT_IE_TARGET_ID: v.GMT_IE_TARGET_ID, HOUR_NO: b.substr(1, 2), PROD_QTY: v[b], IS_LOCKED: is_locked });
                                }
                            }
                        });

                        //if (v.ACHV_QTY_OTWH > 0 && Math.ceil(v.OTWH) > 0) {

                        if (v.ACHV_QTY_OTWH > 0 && Math.ceil(v.OTWH) > 0) {
                            var rem = v.ACHV_QTY_OTWH % Math.ceil(v.OTWH);
                            var div = (v.ACHV_QTY_OTWH - rem) / Math.ceil(v.OTWH);
                            for (var i = 0; i < Math.ceil(v.OTWH) ; i++) {
                                if (i == (Math.ceil(v.OTWH) - 1)) {
                                    hr_prod.push({ GMT_IE_TARGET_ID: v.GMT_IE_TARGET_ID, HOUR_NO: i + 9, PROD_QTY: (div + rem), IS_LOCKED: is_locked });
                                } else {
                                    hr_prod.push({ GMT_IE_TARGET_ID: v.GMT_IE_TARGET_ID, HOUR_NO: i + 9, PROD_QTY: div, IS_LOCKED: is_locked });
                                }
                            }
                        } else if (v.ACHV_QTY_OTWH > 0 && Math.ceil(v.OTWH) === 0) {
                            hr_prod.push({ GMT_IE_TARGET_ID: v.GMT_IE_TARGET_ID, HOUR_NO: 9, PROD_QTY: v.ACHV_QTY_OTWH, IS_LOCKED: is_locked });
                        }
                    }

                    if ($state.current.name === 'SewingTargetSetting') {
                        data2bSave.push(
                            {
                                GMT_IE_TARGET_ID: v.GMT_IE_TARGET_ID,
                                GMT_PLN_LINE_LOAD_ID: v.GMT_PLN_LINE_LOAD_ID,
                                GMT_PROD_PLN_CLNDR_ID: v.GMT_PROD_PLN_CLNDR_ID,
                                GMT_PLN_CLNDR_MN_ID: val.GMT_PLN_CLNDR_MN_ID,
                                HR_PROD_LINE_ID: v.HR_PROD_LINE_ID,
                                DAY_TARGET: v.DAY_TARGET,
                                HRLY_TARGET: v.HRLY_TARGET,
                                TARGET_EFF: v.TARGET_EFF,
                                TARGET_WH: v.TARGET_WH,
                                USED_HLP: v.USED_HLP,
                                USED_OP: v.USED_OP,
                                DAY_NO: v.DAY_NO,
                                R_BEGIN_HR: v.R_BEGIN_HR,
                                R_END_HR: v.R_END_HR,
                                IS_TRGT_LOCK: is_locked,
                                RF_PFLT_RSN_TYPE_ID: ((val.RF_PFLT_RSN_TYPE_ID || 0) < 1) ? '' : val.RF_PFLT_RSN_TYPE_ID
                            }
                        );
                    }

                    if ($state.current.name === 'GmtManualProductionEntry') {
                        data2bSave.push(
                            {
                                GMT_IE_TARGET_ID: v.GMT_IE_TARGET_ID,
                                GMT_PROD_PLN_CLNDR_ID: v.GMT_PROD_PLN_CLNDR_ID,
                                HR_PROD_LINE_ID: v.HR_PROD_LINE_ID,
                                HRLY_TARGET: v.HRLY_TARGET,
                                OTWH: v.OTWH,
                                RF_PFLT_RSN_TYPE_ID: ((val.RF_PFLT_RSN_TYPE_ID || 0) < 1) ? '' : val.RF_PFLT_RSN_TYPE_ID
                            }
                        );
                    }
                })
            });

            return IeDataService.saveDataByUrl({
                XML: config.xmlStringShort(data2bSave),
                XML_PROD: config.xmlStringShort(hr_prod),
                HR_PROD_FLR_ID: dataOri.HR_PROD_FLR_ID,
                GMT_PROD_PLN_CLNDR_ID: dataOri.lines[0].GMT_PROD_PLN_CLNDR_ID,
                pOption: ($state.current.name === 'SewingTargetSetting') ? 1001 : 1002
            }, '/GmtIeTarget/Save').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                         getLineListData(vm.selectedFlr, '', vm.selectedDt);
                    }
                    config.appToastMsg(res.data.PMSG);
                }

            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        vm.submitDataLock = function (dataOri, isValid, is_locked) {
            var msg =  ($state.current.name === 'SewingTargetSetting')? 'Locking Target Related Data...': 'Locking Production Data...'
            Dialog.confirm( msg+'<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     return vm.submitData(dataOri, isValid, is_locked);
                 });
        };

    }

})();