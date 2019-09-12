(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleOrderConDtlController', ['$q', '$http', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'formData', '$filter', '$modal', '$timeout', '$sessionStorage', 'cur_date_server', StyleOrderConDtlController]);
    function StyleOrderConDtlController($q, $http, config, MrcDataService, $stateParams, $state, $scope, formData, $filter, $modal, $timeout, $sessionStorage, cur_date_server) {


        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.errors = null;
        vm.tnaTaskList = [];
        vm.OrderTypeList = [];
        var extStatus = [];

        var key = 'MC_ORDER_H_ID';

        vm.today = new Date();
        vm.params = $stateParams;

        vm.StyleData = $scope.$parent.$parent.StyleData;

        vm.StyleForm = $scope.$parent.$parent.Style;
        if (formData[key]) {

            formData['WORK_STYLE_NO'] = formData['WORK_STYLE_NO'] || $stateParams.pWORK_STYLE_NO;
    
            vm.form = formData;
        } else {

            if ($state.current.LK_STYL_DEV_ID == 265) {
                vm.form = {
                    LK_ORD_STATUS_ID: 208, QTY_MOU_ID: 1, RF_CURRENCY_ID: 2, IS_MULTI_SHIP_DT: 'N', ORD_CONF_DT: vm.today,
                    WORK_STYLE_NO: $stateParams.pWORK_STYLE_NO, ORDER_NO: 'Development', HR_COMPANY_ID: 1, PROD_UNIT_IDh: 2
                };
            } else {
                vm.form = {
                    LK_ORD_STATUS_ID: 363, QTY_MOU_ID: 1, RF_CURRENCY_ID: 2, IS_MULTI_SHIP_DT: 'N', ORD_CONF_DT: vm.today,
                    WORK_STYLE_NO: $stateParams.pWORK_STYLE_NO,

                    itmsOrdShipDT: [
                        { MC_ORDER_SHIP_ID: 0, MC_ORDER_H_ID: 0, SHIP_DT: $filter('date')(vm.today, vm.dtFormat), SHIP_DESC: 'Shipment-1' }, /*{ MC_ORDER_SHIP_ID: 0, MC_ORDER_H_ID: 0, SHIP_DT: $filter('date')(vm.today, vm.dtFormat), SHIP_DESC: '2nd', itemsStyle: [] }*/
                    ]
                    , HR_COMPANY_ID: 1, PROD_UNIT_ID: 2, IS_PROV: 'N'
                };
            }

        }


        if ($stateParams.pMC_ORDER_H_ID < 1) {
            vm.form['HR_COUNTRY_ID_LST'] = [vm.StyleData.HR_COUNTRY_ID];
        }
        
        vm.oriForm = angular.copy(vm.form);

        $scope.minDate = moment(cur_date_server).add(-3, 'days')._d;
        console.log(moment(cur_date_server).add('days', -3)._d);

        activate();
        function activate() {
            var promise = [OrderStatusList(), getRF_CurrencyList(), getTnaTempList(), getMOUList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                $scope.$parent.setCurrentState('StyleH.OrderCon.Detail');

                
                
            });
        }


        $scope.$watchGroup(['vm.form.ORDER_NO', 'vm.form.MC_ORDER_H_ID'], function (newVal, oldVal) {
            if (newVal[0] && newVal[1]) {
                if ( newVal[0] && (newVal[1]||0) < 1 ) {
                    return MrcDataService.getDataByUrl('/Order/OrderListByOrderNo/' + newVal[0] + '?pLK_ORD_STATUS_ID=363&pIS_PROV=N')
                           .then(function (res) {
                               vm.ordListByOrder = res;
                           }
                   );
                } else {
                          vm.ordListByOrder = [];
                }
            }

        });


        $scope.templateOrderStatus = '<span class="#: LOOKUP_DATA_ID==365 ? \"k-state-disabled\": \"\"#">#=data.LK_DATA_NAME_EN#</span>';
        $scope.onSelectIOrderStatus = function (e) {
            $(".k-state-disabled").parent().click(false);
        };

        $scope.openOrdShipExtentionModal = function (pMC_ORDER_SHIP_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Home/_UpdateOrdeShipDataModal',
                controller: 'OrdShipDateExtModalController',
                size: 'lg',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    V_MC_ORDER_SHIP_ID: function () {
                        return pMC_ORDER_SHIP_ID;
                    }
                }
            });

            modalInstance.result.then(function (data) {

            });
        };


        vm.tnaTaskNavigate = function (MC_TNA_TASK_ID) {
            var CurObj = _.find(vm.tnaTaskList, function (o) {
                return o.MC_TNA_TASK_ID == MC_TNA_TASK_ID;
            });
            var url = CurObj.PAGE_URL + '?bAcId=' + vm.StyleData.MC_BYR_ACC_ID;

            var a = document.createElement('a');
            a.href = url;
            a.target = '_blank';
            document.body.appendChild(a);
            a.click();
        };

        function getMOUList() {
            return vm.MOUList = {
                optionLabel: "--Qty Unit--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getMOUList().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "MOU_CODE",
                dataValueField: "RF_MOU_ID"
            };
        }

        $scope.$watch('vm.form.MC_TNA_TMPLT_H_ID', function (newVal, oldVal) {

            if (newVal == oldVal) {
                vm.form['TNA_GENERATION_DISABLED'] = false;
            } else {
                vm.form['TNA_GENERATION_DISABLED'] = true;
            }


        }, true);

        function String2OrdTypeID(IS_N_R) {
            var _IS_N_R;
            switch (IS_N_R) {
                case "N":
                    _IS_N_R = 175;
                    break;
                case "R":
                    _IS_N_R = 174;
                    break;
                case "D":
                    _IS_N_R = 311;
                    break;
            }
            return _IS_N_R;
        }



        vm.generateExtionNumber = function (IS_AUTO, LK_ORD_TYPE_ID) {
            if (IS_AUTO && LK_ORD_TYPE_ID) {
                var ordTyp;                
                switch (LK_ORD_TYPE_ID) {
                    case 175:
                        ordTyp = 'N';
                        break;
                    case 174:
                        ordTyp = 'R';
                        break;
                    case 311:
                        ordTyp = 'D';
                        break;
                }


                return MrcDataService.getDataByUrl('/StyleH/getStyleExtAuto/' + vm.StyleData.MC_STYLE_H_ID).then(function (ext) {
                    if (ext) {
                        vm.form['WORK_STYLE_NO'] = vm.StyleData.STYLE_NO + '-' +ordTyp+ ext;
                    }

                }, function (err) {
                    vm.form['WORK_STYLE_NO'] = vm.StyleData.STYLE_NO;
                });
            } else {
                vm.form['WORK_STYLE_NO'] = vm.StyleData.STYLE_NO;
            }
        };

        $scope.$watchGroup(['vm.form.TOT_ORD_QTY', 'vm.form.UNIT_PRICE'], function (newVal, oldVal) {
            if (newVal[0] && newVal[1]) {
                if (_.isNumber(newVal[0]) && _.isNumber(newVal[1])) {
                    vm.form['TOT_ORD_VALUE'] = parseInt(newVal[0] * newVal[1]);
                }
            }

        });
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        $scope.TGT_ORD_DOC_RCV_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.TGT_ORD_DOC_RCV_DTopened = true;
        }


        $scope.ConfirmDate_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.ConfirmDate_LNopened = true;
        }

        vm.shipmentDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.shipmentDateOpened = true;
        };

        vm.shipDateOnBlur = function () {           
            if (vm.form.itmsOrdShipDT.length == 1) {
                vm.form.itmsOrdShipDT[0].SHIP_DT = angular.copy(vm.form.SHIP_DT);
            }
        }

        vm.sizeShipmentDateOpened = [];
        vm.sizeShipmentDateOpen = function ($event, index) {
            if (vm.form.IS_ORD_FINALIZED != 'Y') {
                $event.preventDefault();
                $event.stopPropagation();

                vm.sizeShipmentDateOpened[index] = true;
            }
        };

        vm.template = '<span><h4 style="padding:0px;margin-top: 0px;margin-bottom: 0px;">#: data.TNA_TMPLT_CODE #</h4><p style="padding:0px;margin-top: 0px;margin-bottom: 0px;"> <small>#: data.REMARKS #</small></p></span>';
        function getTnaTempList() {
            return MrcDataService.getDataByUrl('/StyleH/TnAList/OrderType/' + (formData.LK_ORD_TYPE_ID || 'null') + '/Buyer/' + (vm.StyleData.MC_BUYER_ID || 'null') + '/LeadTime/' + formData.LEAD_TIME).then(function (res) {
                vm.TnaTempList = new kendo.data.DataSource({
                    data: res
                });
            });
        }




        vm.onTnaTemplateDataBound = function (e) {
            setTimeout(function () {
                var ds = e.sender.dataSource.data();
                if (ds.length == 1) {
                    e.sender.value(ds[0].MC_TNA_TMPLT_H_ID);
                    vm.form['MC_TNA_TMPLT_H_ID'] = ds[0].MC_TNA_TMPLT_H_ID;
                } else {
                    var dsBuyer = _.filter(ds, function (o) {
                        return parseInt(o.MC_BUYER_ID) == parseInt(vm.StyleData.MC_BUYER_ID || -1);
                    });
                    if (dsBuyer.length == 1) {
                        e.sender.value(dsBuyer[0].MC_TNA_TMPLT_H_ID);
                        vm.form['MC_TNA_TMPLT_H_ID'] = dsBuyer[0].MC_TNA_TMPLT_H_ID;
                    } else if (dsBuyer.length > 1) {
                        if (Object.keys(formData).length == 0 || !formData['MC_TNA_TMPLT_H_ID']) {
                            e.sender.value(dsBuyer[0].MC_TNA_TMPLT_H_ID);
                            vm.form['MC_TNA_TMPLT_H_ID'] = dsBuyer[0].MC_TNA_TMPLT_H_ID;
                        }
                    }
                }
            }, 100);
        }



        function OrderStatusList() {
            return vm.orderStatusList = {
                optionLabel: "-- Select Status --",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(56).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getRF_CurrencyList() {

            return vm.StyleRF_CurrencyList = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByUrlNoToken('/api/common/CurrencyList').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var datas = this.dataSource.data();
                    var item = this.dataItem(e.item);
                    if (datas.length > 1) {
                        vm.CurrCode = datas[1].CURR_CODE;
                    };

                    if (item.RF_CURRENCY_ID && (formData.MC_ORDER_H_ID<1)) {
                        return MrcDataService.getDataByUrlNoToken('https://www.exchangerate-api.com/USD/BDT/1?k=8abba360d9d7e7a974e2fb83').then(function (val) {
                            vm.form.CURR_CONV_LOC = parseFloat(val);
                        }, function (err) {
                            vm.form.CURR_CONV_LOC = 0;
                            console.error(err);
                        });
                    }

                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    var datas = this.dataSource.data();

                    if (item.CURR_CODE === 'USD') {
                        MrcDataService.getDataByUrlNoToken('https://www.exchangerate-api.com/USD/BDT/1?k=8abba360d9d7e7a974e2fb83').then(function (val) {
                            vm.form.CURR_CONV_LOC = parseFloat(val);
                        }, function (err) {
                            vm.form.CURR_CONV_LOC = 0;
                            console.error(err);
                        });
                    } else if (item.CURR_CODE === 'Euro') {

                        MrcDataService.getDataByUrlNoToken('https://www.exchangerate-api.com/EUR/BDT/1?k=8abba360d9d7e7a974e2fb83').then(function (val) {
                            vm.form.CURR_CONV_LOC = parseFloat(val);
                        }, function (err) {
                            vm.form.CURR_CONV_LOC = 0;
                            console.error(err);
                        });

                    } else {
                        vm.form.CURR_CONV_LOC = 1;
                    }


                    if (item.RF_CURRENCY_ID) {
                        vm.CurrCode = item.CURR_CODE;



                    } else {

                        if (datas.length > 1) {
                            vm.CurrCode = datas[1].CURR_CODE;
                        };
                    }

                },
                dataTextField: "CURR_CODE",
                dataValueField: "RF_CURRENCY_ID",
            };
        };

        vm.calculateOq = function (data) {
            if (data.IS_MULTI_SHIP_DT == "N") {
                data['TOT_ORD_QTY'] = _.sumBy(data.cap_itms, function (o) { return (o.ORDER_QTY || 0); });
            } else if (data.IS_MULTI_SHIP_DT == "Y") {
                data['TOT_ORD_QTY'] = _.sumBy(

                      data.itmsOrdShipDT.map(function (o) {
                          return {
                              ORDER_QTY: _.sumBy(o.cap_itms, function (o) { return (o.ORDER_QTY || 0); })
                          }
                      })
                    ,

                    function (o) { return (o.ORDER_QTY || 0); });
            }

        };

        $scope.$watchGroup(['vm.form.ORD_CONF_DT', 'vm.form.SHIP_DT'], function (newVal, oldVal) {
            if (newVal[1] && newVal[0]) {
                var a = moment(newVal[1]);
                var b = moment(newVal[0]);


                vm.form.LEAD_TIME = a.diff(b, 'days')+1;
            }



                if (newVal[0] && newVal[1]) {
                    return MrcDataService.getDataByUrl('/StyleH/TnAList/OrderType/' + (formData.LK_ORD_TYPE_ID || 'null') + '/Buyer/' + (vm.StyleData.MC_BUYER_ID || 'null') + '/LeadTime/' + vm.form.LEAD_TIME).then(function (res) {
                        vm.TnaTempList = new kendo.data.DataSource({
                            data: res
                        });
                    });
                }


        });

        vm.addRow = function (data, copyTo, index) {
            var copiedData = angular.copy(data);
            copiedData['MC_ORDER_SHIP_ID'] = -1;
            angular.forEach(copiedData.cap_itms, function (val) {
                val['MC_GMT_CAP_ITEM_ID'] = -1;
            });

            copyTo.push(copiedData);
        };
        vm.removeRow = function (index, removeFrom, master) {
            removeFrom.splice(index, 1);
        };


        vm.submitData = function (RawData, token) {
            vm.errors = undefined;

            var data = angular.copy(RawData);
            data['ORD_CONF_DT'] = $filter('date')(data.ORD_CONF_DT, 'dd-MMM-yyyy');
            data['SHIP_DT'] = $filter('date')(data.SHIP_DT, 'dd-MMM-yyyy');
            data['MC_BUYER_ID'] = vm.StyleData.MC_BUYER_ID;
            data['IS_MULTI_SHIP_DT'] = data['IS_MULTI_SHIP_DT'] || "N";
            data['MC_BYR_ACC_ID'] = vm.StyleData.MC_BYR_ACC_ID;
            data['IS_ACTIVE'] = 'Y';
            data['MC_STYLE_H_ID'] = vm.StyleData.MC_STYLE_H_ID;
            data['MC_STYLE_H_EXT_ID'] = $stateParams.pMC_STYLE_H_EXT_ID;
            data['LK_ORD_TYPE_ID'] = String2OrdTypeID($stateParams.pIS_N_R);
            data['HR_COUNTRY_ID_LST'] = data['HR_COUNTRY_ID_LST'].join(',');
            if (data.IS_MULTI_SHIP_DT == 'N') {
                data['itmsOrdShipDT'][0]['cap_itms'] = data['cap_itms'];
            }

            angular.forEach(data['itmsOrdShipDT'], function (val, key) {
                if (data.IS_MULTI_SHIP_DT == 'N') {
                    val['SHIP_DT'] = $filter('date')(data.SHIP_DT, 'dd-MMM-yyyy');
                } else {
                    val['SHIP_DT'] = $filter('date')(val['SHIP_DT'], 'dd-MMM-yyyy');
                }

                val['CapItmXML'] = config.xmlStringShortNoTag(val.cap_itms.map(function (o) {
                    return {
                        MC_GMT_CAP_ITEM_ID: o.MC_GMT_CAP_ITEM_ID,
                        LK_GARM_TYPE_ID: o.LK_GARM_TYPE_ID,
                        ORDER_QTY: o.ORDER_QTY,
                        SMV: o.SMV
                    }
                }));
            });

           
            var vIsValid = 'N';
            
            MrcDataService.getDataByFullUrl('/api/mrc/Order/GetGmtCapacityBookedData?pMC_BYR_ACC_ID=' + data['MC_BYR_ACC_ID'] + '&pSHIP_DT=' + data['SHIP_DT'] + '&pMC_ORDER_H_ID=' + (data['MC_ORDER_H_ID'] || 0)).then(function (res) {

                console.log(res);
                console.log(data['cap_itms']);
                
                var vPcsPerPack = 1;
                var vIsScAllowed = 'N';
                var vCapAvailablePcs = 0;
                var vExtraCapAvailablePcs = 0;

                var vTotCapQty = _.sumBy(res, function (ob) {
                    return ob.PLAN_PROD_PCS
                });                                
                //var vExtraCapQty = _.sumBy(res, function (ob) {
                //    return ob.SC_GMT_CAP_WITH_PERMISN_PCS
                //});
                var vTotBkQty = _.sumBy(res, function (ob) {
                    return ob.PCS_BOOKED
                });

                //console.log('Capacity: ' + vCapQty);
                //console.log('Extra Capacity: ' + vExtraCapQty);
                //console.log('Booking: ' + vBkQty);

                var vIsMnFreeCapPcs4ByrAlloc = 'N';
                var vIsForceBreak = 'N';
                
                angular.forEach(res, function (val, key) {
                    var vCapItm = _.filter(data['cap_itms'], function (ob) {
                        return val['GMT_PRODUCT_TYP_ID'] == ob.LK_GARM_TYPE_ID
                    });

                    vIsScAllowed = val['IS_SC_ALLOWED'];
                                                           
                    if (vIsForceBreak == 'N' && vCapItm[0]['ORDER_QTY'] > 0) {
                        
                        if ((val['PCS_BOOKED'] + val['CARRY_ORVERED_QTY'] + (vCapItm[0]['ORDER_QTY']*(vm.StyleData['PCS_PER_PACK']||vPcsPerPack))) <= val['PLAN_PROD_PCS']) {
                            vIsValid = 'Y';                           
                        }
                        else if(val['MN_FREE_CAP_PCS4BYR_ALLOC']>=vCapItm[0]['ORDER_QTY']){                            
                            vIsValid = 'N';
                            vIsMnFreeCapPcs4ByrAlloc = 'Y';
                            vIsForceBreak = 'Y';
                        }
                        else {
                            vIsValid = 'N';
                            vIsForceBreak = 'Y';
                            vCapAvailablePcs = val['PLAN_PROD_PCS'] - (val['PCS_BOOKED'] + val['CARRY_ORVERED_QTY']);
                            if (vIsScAllowed == 'Y') {
                                vExtraCapAvailablePcs = val['MN_FREE_CAP_PCS4BYR_ALLOC'] + val['SC_GMT_CAP_WITH_PERMISN_PCS'];
                            }
                        }
                    }                    
                });

                if (res.length < 1) {
                    vm.errors = [{ error: 'Sorry! Buyer wise allocation not available. You can take allocation by planning department.' }];
                    return;
                }
                if (vIsMnFreeCapPcs4ByrAlloc == 'Y') {
                    vm.errors = [{ error: 'Sorry! Capacity over-booked but over all free space available. You can take re-allocation by planning department.' }];
                    return;
                }

                if ((vTotBkQty + (data['TOT_ORD_QTY'] * vm.StyleData['PCS_PER_PACK'] || vPcsPerPack)) > vTotCapQty && vIsValid == 'Y') {
                    vIsValid = 'N';
                    vIsForceBreak = 'Y';
                }
                
                if (new Date(data['SHIP_DT']) <= new Date("30-Sep-2019")) {
                    vIsValid = 'Y';
                }
                //vIsValid = 'Y'; // Bypass capacity booking check                
                if (vIsValid == 'Y' || data['LK_ORD_STATUS_ID'] == 786) { //786 --> Waiting for OTP to Confirm
                  
                    return MrcDataService.saveDataByUrl(data, '/StyleH/SaveOrder').then(function (res) {
                        if (res.success === false) {
                            vm.errors = res.errors;
                        }
                        else {
                            res['data'] = angular.fromJson(res.jsonStr);
                            console.log(res.data);
                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                if (data.WORK_STYLE_NO.trim() != $stateParams.pWORK_STYLE_NO.trim()) {
                                    $scope.$parent.refreshStyleExtionList();
                                    $state.go('StyleH.OrderCon.Detail', { pWORK_STYLE_NO: data.WORK_STYLE_NO.trim() }, { notify: false });
                                }

                                if (formData[key]) {
                                    $scope.$parent.refreshStyleExtionList();
                                }

                                setTimeout(function () {
                                    var grid = $('#idStyleExtentionList').data("kendoGrid");
                                    $.each(grid.tbody.find('tr'), function () {
                                        if ($stateParams.pMC_STYLE_H_EXT_ID == 0) return;
                                        var model = grid.dataItem(this);
                                        if (model.MC_STYLE_H_EXT_ID == $stateParams.pMC_STYLE_H_EXT_ID) {
                                            grid.expandRow("[data-uid='" + model.uid + "']");
                                        }
                                    });
                                }, 400);


                                vm.form['MC_ORDER_H_ID'] = parseInt(res.data.OPMC_ORDER_H_ID);
                                vm.form['LK_ORD_STATUS_ID'] = parseInt(res.data.PRTN_LK_ORD_STATUS_ID);

                                if (!_.isNil(res.data.OPJOB_TRAC_NO)) {
                                    vm.form['JOB_TRAC_NO'] = res.data.OPJOB_TRAC_NO;
                                }


                            }

                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                }
                
                else {
                    
                    console.log(vCapAvailablePcs);
                    console.log(vExtraCapAvailablePcs);
                    
                    if (vIsScAllowed == 'Y' && vExtraCapAvailablePcs < 0) {
                        vm.errors = [{ error: 'Sorry! Extra booking capacity exceed.' }];
                        return;
                    }
                    else if ((data['TOT_ORD_QTY'] * vm.StyleData['PCS_PER_PACK'] || vPcsPerPack) > (vCapAvailablePcs + vExtraCapAvailablePcs)) {
                        data['IsExtraCapExceed'] = 'Y';
                    }
                    else {
                        data['IsExtraCapExceed'] = 'N';
                    }
                                       
                    var modalInstance = $modal.open({
                        animation: true,
                        templateUrl: 'StyleOrdConOtpModal.html',
                        controller: function ($modalInstance, $q, $scope, $http, $state, $filter, config, Dialog, MrcDataService, ParentObj, CapBkData) {

                            console.log($scope);
                            console.log(ParentObj);

                            $scope.IsExtraCapExceed = ParentObj['IsExtraCapExceed'];
                            $scope.capBkData4OTP = CapBkData;

                            $scope.save = function () {

                                ParentObj['IS_OTP_SEND'] = 'Y';

                                return MrcDataService.saveDataByUrl(ParentObj, '/StyleH/SaveOrder').then(function (res) {
                                    if (res.success === false) {
                                        vm.errors = res.errors;
                                    }
                                    else {
                                        res['data'] = angular.fromJson(res.jsonStr);
                                        console.log(res.data);
                                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                            $scope.cancel();

                                            if (ParentObj.WORK_STYLE_NO.trim() != $stateParams.pWORK_STYLE_NO.trim()) {
                                                $scope.$parent.refreshStyleExtionList();
                                                $state.go('StyleH.OrderCon.Detail', { pWORK_STYLE_NO: ParentObj.WORK_STYLE_NO.trim() }, { notify: false });
                                            }

                                            if (ParentObj['MC_ORDER_H_ID']) {
                                                $scope.$parent.refreshStyleExtionList();
                                            }

                                            setTimeout(function () {
                                                var grid = $('#idStyleExtentionList').data("kendoGrid");
                                                $.each(grid.tbody.find('tr'), function () {
                                                    if ($stateParams.pMC_STYLE_H_EXT_ID == 0) return;
                                                    var model = grid.dataItem(this);
                                                    if (model.MC_STYLE_H_EXT_ID == $stateParams.pMC_STYLE_H_EXT_ID) {
                                                        grid.expandRow("[data-uid='" + model.uid + "']");
                                                    }
                                                });
                                            }, 400);


                                            vm.form['MC_ORDER_H_ID'] = parseInt(res.data.OPMC_ORDER_H_ID);
                                            //vm.form['LK_ORD_STATUS_ID'] = 786; //Waiting for OTP to Confirm
                                            vm.form['LK_ORD_STATUS_ID'] = parseInt(res.data.PRTN_LK_ORD_STATUS_ID);

                                            if (!_.isNil(res.data.OPJOB_TRAC_NO)) {
                                                vm.form['JOB_TRAC_NO'] = res.data.OPJOB_TRAC_NO;
                                            }

                                            $http({
                                                url: '/Merchandising/mrc/FireMailSendOTP4OrderConfirm',
                                                method: 'get',
                                                params: { pMC_ORDER_H_ID: parseInt(res.data.OPMC_ORDER_H_ID) }
                                            }).then(function (res) {
                                                console.log(res);
                                            });


                                        }

                                        config.appToastMsg(res.data.PMSG);
                                    }
                                }).catch(function (message) {
                                    exception.catcher('XHR loading Failded')(message);
                                });
                            }

                            $scope.cancel = function (data) {
                                $modalInstance.dismiss();
                            }
                        },
                        size: 'lg',
                        windowClass: 'app-modal-window',
                        scope: $scope,
                        resolve: {
                            ParentObj: function () {
                                return data;
                            },
                            CapBkData: function (MrcDataService, $stateParams) {
                                return MrcDataService.getDataByFullUrl('/api/mrc/Order/GetGmtCapBkData4OTPModal?pMC_BYR_ACC_ID=' + data['MC_BYR_ACC_ID'] + '&pSHIP_DT=' + data['SHIP_DT'] + '&pMC_ORDER_H_ID=' + (data['MC_ORDER_H_ID'] || 0));
                            }
                        }
                    });

                    modalInstance.result.then(function (data) {

                    }, function () {
                        console.log('Modal dismissed at: ' + new Date());

                    });
                    

                }
            });
        }

        vm.isShowResendOTP = 'Y';
        vm.resendOTP = function () {
            vm.isShowResendOTP = 'N';

            $http({
                url: '/Merchandising/mrc/FireMailSendOTP4OrderConfirm',
                method: 'get',
                params: { pMC_ORDER_H_ID: parseInt(vm.form.MC_ORDER_H_ID) }
            }).then(function (res) {
                console.log(res);
                vm.isShowResendOTP = 'Y';

                if (res.data.success === true) {
                    config.appToastMsg("MULTI-001OTP send successfully");
                }
                else {
                    config.appToastMsg("MULTI-002Something wrong, pls try again");
                }
            });
        }
        
        vm.cancel = function () {
            vm.form = angular.copy(vm.oriForm);
            $scope.StyleOrderForm.$setPristine();
        }

        vm.companyOpt = {
            optionLabel: "-- All --",
            filter: "contains",
            autoBind: true,
            dataTextField: "COMP_NAME_EN",
            dataValueField: "HR_COMPANY_ID",
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getCompanyData').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            change: function (e) {
                var dataItem = this.dataItem(e.item);
                return vm.officeDs = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getOfficeList?pHR_COMPANY_ID=' + (dataItem.HR_COMPANY_ID || formData.HR_COMPANY_ID || '')).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                });
            }
        };

        vm.countryList = {
            placeholder: "--Select Country--",
            filter: "startswith",
            valuePrimitive: true,
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.GetAllOthers('/api/common/GetCountryList').then(function (res) {
                            console.log(res);
                            e.success(res);
                        });
                    }
                },
            },
            dataTextField: "COUNTRY_NAME_EN",
            dataValueField: "HR_COUNTRY_ID"
        };

        vm.officeOpt = {
            optionLabel: "-- All --",
            filter: "contains",
            autoBind: true,
            dataTextField: "OFFICE_NAME_EN",
            dataValueField: "HR_OFFICE_ID"
        };

        vm.officeDs = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    MrcDataService.getDataByFullUrl('/api/pln/GmtLineLoad/getOfficeList?pHR_COMPANY_ID=' + (formData.HR_COMPANY_ID||'')).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });


        vm.openModalCapacityBkStatus = function (pSHIP_DT) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'CapacityBookingStatus.html',
                controller: function ($scope, $modalInstance, MrcDataService) {

                    MrcDataService.getDataByFullUrl('/api/pln/PlanCommon/getGmtPlanCapcityDshBrdDataWkShip?pSHIP_DT=' + new Date(pSHIP_DT).toISOString()).then(function (res) {
                        $scope.chartDatas = res;
                    });


                    $scope.cancel = function (data) {
                        $modalInstance.dismiss();
                    }
                },
                size: 'lg',
                windowClass: 'large-Modal'
            });

            modalInstance.result.then(function (data) {

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        };

        vm.printByrWiseCapctyAlloc = function (pSHIP_DT) {

            var url = '/api/Pln/PlnReport/PreviewReportRDLC';
            
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", url);
            form.setAttribute("target", '_blank');
             
            vm.form.REPORT_CODE = 'RPT-5008';
            vm.form.FROM_MONTH = $filter('date')(pSHIP_DT, vm.dtFormat);
            
            var params = angular.copy(vm.form);

            console.log(params);

            for (var i in params) {
                if (params.hasOwnProperty(i)) {

                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = i;
                    //input.value = params[i];
                    input.value = (params[i] instanceof Date) ? params[i].toISOString() : params[i];
                    form.appendChild(input);
                }
            }

            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);

        };

    }

})();












