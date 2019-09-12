(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleOrderConDtlDevController', ['$http', '$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'formData', '$filter', '$modal', '$timeout', '$sessionStorage', 'Dialog', 'cur_date_server', StyleOrderConDtlDevController]);
    function StyleOrderConDtlDevController($http, $q, config, MrcDataService, $stateParams, $state, $scope, formData, $filter, $modal, $timeout, $sessionStorage, Dialog, cur_date_server) {


        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.errors = null;
        vm.orderDtlQty = 0;
        vm.tnaTaskList = [];
        vm.OrderTypeList = [];
        var extStatus = [];

        var key = 'MC_ORDER_H_ID';

        vm.today = new Date();
        vm.params = $stateParams;

        vm.StyleData = $scope.$parent.$parent.StyleData;

        vm.StyleForm = $scope.$parent.$parent.Style;

        
        $scope.minDate = moment(cur_date_server).add(-3,'days')._d;
        console.log(moment(cur_date_server).add('days', -3)._d);

        if (formData[key]) {

            formData['WORK_STYLE_NO'] = formData['WORK_STYLE_NO'] || $stateParams.pWORK_STYLE_NO;
            vm.form = formData;
        } else {

            if ($state.current.LK_STYL_DEV_ID == 265) {
                vm.form = {
                    LK_ORD_STATUS_ID: 208, QTY_MOU_ID: 1, RF_CURRENCY_ID: 2, IS_MULTI_SHIP_DT: 'N', ORD_CONF_DT: vm.today,
                    WORK_STYLE_NO: $stateParams.pWORK_STYLE_NO, ORDER_NO:'Development', LK_STYL_DEV_TYP_ID: 541, TOT_ORD_QTY: 0
                };
            } else {
                vm.form = {
                    LK_ORD_STATUS_ID: 363, QTY_MOU_ID: 1, RF_CURRENCY_ID: 2, IS_MULTI_SHIP_DT: 'N', ORD_CONF_DT: vm.today,
                    WORK_STYLE_NO: $stateParams.pWORK_STYLE_NO
                };
            }

        }


        
        vm.oriForm = angular.copy(vm.form);

        activate();
        function activate() {
            var promise = [OrderStatusList(), getStyleDevTypeList(), getRF_CurrencyList(), getTnaTempList(), getMOUList(), getByrWiseSmplTyleList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
                $scope.$parent.setCurrentState('StyleH.OrderCon.Detail');
            });
        }


        $scope.templateOrderStatus = '<span class="#: LOOKUP_DATA_ID==365 ? \"k-state-disabled\": \"\"#">#=data.LK_DATA_NAME_EN#</span>';
        $scope.onSelectIOrderStatus = function (e) {
            $(".k-state-disabled").parent().click(false);
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

        function getStyleDevTypeList() {
            return vm.styleDevTypeOptions = {
                optionLabel: "-- Select Type --",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(106).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                dataBound: function (e) {
                    var item = this.dataItem(e.item);

                    if (!formData.LK_STYL_DEV_TYP_ID) {
                        vm.form.LK_STYL_DEV_TYP_ID = 541;
                        $stateParams.pLK_STYL_DEV_TYP_ID = 541;
                    }
                    else {
                        vm.form.LK_STYL_DEV_TYP_ID = formData.LK_STYL_DEV_TYP_ID;
                        $stateParams.pLK_STYL_DEV_TYP_ID = formData.LK_STYL_DEV_TYP_ID;
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);

                    //vm.form.LK_STYL_DEV_TYP_ID = item.LK_STYL_DEV_TYP_ID;
                    $stateParams.pLK_STYL_DEV_TYP_ID = item.LK_STYL_DEV_TYP_ID;
                    vm.form.TOT_ORD_QTY = 0;
                }
            }
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

                    if (item.RF_CURRENCY_ID && !formData.RF_CURRENCY_ID) {
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

        function getByrWiseSmplTyleList() {

            return vm.byrWiseSmplTypeList = {
                optionLabel: "-- Select Sample Type --",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.GetAllOthers('/api/mrc/buyer/SampleListByBuyerID/' + (vm.StyleData.MC_BUYER_ID || 0)).then(function (res) {
                                var dataList = _.filter(res, function (ob) { return ob.LK_ORD_TYPE_ID == 311 });
                                e.success(dataList);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "SMPL_TYPE_NAME",
                dataValueField: "RF_SMPL_TYPE_ID",
                dataBound: function (e) {
                    var ds = this.dataSource.data();
                    var item = this.dataItem(e.item);
                    
                    if (ds.length == 1) {
                        vm.form.RF_SMPL_TYPE_ID = ds[0].RF_SMPL_TYPE_ID;
                    }
                },
                select: function (e) {                    
                    var item = this.dataItem(e.item);
                    console.log(item.RF_SMPL_TYPE_ID);
                }
            };            
        }

        

        //$scope.$watchGroup(['vm.form.ORD_CONF_DT', 'vm.form.SHIP_DT'], function (newVal, oldVal) {
        //    if (newVal[1] && newVal[0]) {
        //        var a = moment(newVal[1]);
        //        var b = moment(newVal[0]);


        //        vm.form.LEAD_TIME = a.diff(b, 'days') + 1;
        //    }

        $scope.$watchGroup(['vm.form.ORD_CONF_DT', 'vm.form.SHIP_DT'], function (newVal, oldVal) {
            if (newVal[1] && newVal[0]) {

                var a, b;
                if (newVal[0] instanceof Date) {
                    a = newVal[0].toISOString();
                } else {
                    a = newVal[0];
                }
                if (newVal[1] instanceof Date) {
                    b = newVal[1].toISOString();
                } else {
                    b = newVal[1];
                }
 
                MrcDataService.getDataByFullUrl('/api/common/NoOfWorkingDay?pHR_COMPANY_ID=' + $scope.$parent.StyleData.MANUF_ID + '&pFROM_DT=' + a + '&pTO_DT=' + b).then(function (res) {
                    vm.form.LEAD_TIME = parseInt(angular.fromJson(res).V_NO_OF_DAYS);
                    MrcDataService.getDataByUrl('/StyleH/TnAList/OrderType/' + (formData.LK_ORD_TYPE_ID || 'null') + '/Buyer/' + (vm.StyleData.MC_BUYER_ID || 'null') + '/LeadTime/' + parseInt(angular.fromJson(res).V_NO_OF_DAYS)).then(function (res) {
                        vm.TnaTempList = new kendo.data.DataSource({
                            data: res
                        });
                    });
                });
               // vm.form.LEAD_TIME = a.diff(b, 'days')+1;
            }


        });



        // ========= Start New Code ==========
        vm.ordDtlGridOption = {
            height: 250,
            sortable: true,
            pageable: false,
            editable: false,
            //selectable: "cell",
            navigatable: true,
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        startswith: "Starts With",
                        eq: "Is Equal To"
                    }
                }
            },
            columns: [
                { field: "ITEM_SNAME", title: "Item", type: "string", width: "20%", footerTemplate: "Total Record: #=count#" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%" },
                { field: "COMBO_NO", title: "Combo", type: "string", width: "10%", hidden: true },
                { field: "SIZE_CODE", title: "Size", type: "string", width: "5%", filterable: false },                
                {
                    title: "S.Qty",
                    field: "SIZE_QTY",
                    footerTemplate: "#=sum#",
                    filterable: false,
                    template: function () {
                        return "<input type='number' class='form-control' ng-model='dataItem.SIZE_QTY' min='0' ng-change='vm.getTotalSizeWiseQty()' />";
                    },
                    width: "5%"
                }
                //{
                    //title: "Action",
                    //template: function () {                       
                    //    return "<a class='btn btn-xs blue' ui-sref='SmplFabBookEntry.Dtl({pMC_SMP_REQ_H_ID:dataItem.MC_SMP_REQ_H_ID})' ><i class='fa fa-edit'> Edit</i></a>    <a ng-click='vm.printBookingRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-print'> Print</i></a>";
                    //},
                    //width: "55px"
                //}
            ]                        
        };


        vm.ordDtlGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                   
                    console.log('*******============********');
                    console.log($stateParams);
                    MrcDataService.GetAllOthers('/api/mrc/Order/DevStyleWiseOrdSizeList?pMC_STYLE_H_ID=' + $stateParams.MC_STYLE_H_ID + '&pMC_STYLE_H_EXT_ID=' + $stateParams.pMC_STYLE_H_EXT_ID).then(function (res) {
                        
                        vm.orderDtlQty = _.sumBy(res, function (o) { return o.SIZE_QTY||0; });
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });

                }
            },
            aggregate: [
                { field: "ITEM_SNAME", aggregate: "count" },
                { field: "SIZE_QTY", aggregate: "sum" }
            ],
            schema: {
                model: {
                    id: "MC_ORDER_SIZE_ID",
                    fields: {
                        ITEM_SNAME: { type: "string", editable: false },
                        COLOR_NAME_EN: { type: "string", editable: false },
                        SIZE_CODE: { type: "string", editable: false },
                        SIZE_QTY: { type: "number", editable: false }
                    }
                }
            }
        });

        vm.getTotalSizeWiseQty = function () {
            var data = vm.ordDtlGridDataSource.data();
            vm.orderDtlQty = _.sumBy(data, function (o) { return o.SIZE_QTY || 0; });
        }


        vm.getSmplStyleCheck = function (submitData, token) {
            var deferred = $q.defer();
            $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: '/api/mrc/SampleReq/GetSmplStyleCheck',
                method: 'post',
                data: submitData
            }).then(function (res) {
                $scope.$parent.errors = undefined;
                if (res.data.success === false) {
                    $scope.$parent.errors = [];
                    $scope.$parent.errors = res.data.errors;
                }
                else {

                    var dt = JSON.parse(res.data.jsonStr);

                    if (dt.PMSG.substr(0, 9) == 'MULTI-003') {
                        deferred.resolve({ success: true, msg: dt.PMSG });
                    }
                    else if (dt.PMSG.substr(0, 9) == '') {
                        deferred.resolve({ success: true, msg: 'Do you want to save?' });
                    }
                    else {
                        deferred.reject({ success: false, msg: 'Failed' });
                    }
                    
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
                deferred.reject({ success: false, msg: 'Http request Failed' });

            });
            return deferred.promise;
        };



        vm.devSampleCreate = function (token) {
            vm.assignToUserList = '169,51,79,113,193,204,205,244'.split(',');
            var data = angular.copy(vm.form);

            data['HR_COMPANY_ID'] = $scope.$parent.StyleData['MANUF_ID'];
            data['MC_BYR_ACC_LST'] = $scope.$parent.StyleData['MC_BYR_ACC_ID'];
            data['MC_STYLE_H_EXT_ID'] = data['MC_STYLE_H_EXT_ID_ORD'];
            data['MC_ORDER_H_ID'] = $stateParams.pMC_ORDER_H_ID;
            data['LK_STYL_DEV_TYP_ID'] = $stateParams.pLK_STYL_DEV_TYP_ID || 541; // 541: Sample Making
            
            if ($stateParams.pLK_STYL_DEV_TYP_ID==541){
                data['SMP_REQ_TO'] = '169,51,79,113,193,204,205,244';
            }
            else {
                data['SMP_REQ_TO'] = '79,113,193,204,205,244';
            }
            
            data['SMP_REQ_ATTN'] = ''; //'169,51,79,193,204,205';
            data['SMP_REQ_DT'] = data['ORD_CONF_DT'];

            console.log(data);
            console.log($scope.$parent);

            var vDTL_XML = [{ MC_SMP_REQ_STYL_ID: 0, MC_ORDER_H_ID: data['MC_ORDER_H_ID'] }];
            data.DTL_XML = MrcDataService.xmlStringShort(vDTL_XML.map(function (ob) {
                return ob;
            }));

            vm.getSmplStyleCheck(data, token).then(function (res) {

                console.log(res);
                var vConfirmMsg = 'Do you want to auto generate sample program?';
                if (res.msg.substr(0, 9) == 'MULTI-003') {
                    //vConfirmMsg = res.msg.substr(9) + '</br></br> <b>Do you want to create another program for this style#?</b>';
                    alert(res.msg.substr(9));
                    return;
                }
                else {

                    Dialog.confirm(vConfirmMsg, 'Confirmation', ['Yes', 'No']).then(function () {
                        return MrcDataService.saveDataByUrl(data, '/SampleReq/SaveDevSmpAutoCreate').then(function (res) {
                            if (res.success === false) {
                                vm.errors = res.errors;
                            }
                            else {
                                res['data'] = angular.fromJson(res.jsonStr);
                                console.log(res.data);
                                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                    vm.form['MC_SMP_REQ_H_ID'] = parseInt(res.data.PMC_SMP_REQ_H_ID_RTN);
                                }


                                config.appToastMsg(res.data.PMSG);
                            }
                        }).catch(function (message) {
                            exception.catcher('XHR loading Failded')(message);
                        });
                    });
                }
            });

        }
        // ========= End New Code ============


        vm.submitData = function (RawData, token) {

            var dtlData = vm.ordDtlGridDataSource.data();            
            var data = angular.copy(RawData);

            var szQtyTotal = _.sumBy(dtlData, function (o) {
                return o.SIZE_QTY || 0;
            });

            if (szQtyTotal != data.TOT_ORD_QTY) {
                alert('Sorry! Total order qty. and size wise qty. should be equal');
            }
            else {
                data['ORD_CONF_DT'] = $filter('date')(data.ORD_CONF_DT, 'dd-MMM-yyyy');
                data['SHIP_DT'] = $filter('date')(data.SHIP_DT, 'dd-MMM-yyyy');
                data['MC_BUYER_ID'] = vm.StyleData.MC_BUYER_ID;
                data['IS_MULTI_SHIP_DT'] = data['IS_MULTI_SHIP_DT'] || "N";
                data['MC_BYR_ACC_ID'] = vm.StyleData.MC_BYR_ACC_ID;
                data['IS_ACTIVE'] = 'Y';
                data['MC_STYLE_H_ID'] = vm.StyleData.MC_STYLE_H_ID;
                data['MC_STYLE_H_EXT_ID'] = $stateParams.pMC_STYLE_H_EXT_ID;
                data['LK_ORD_TYPE_ID'] = String2OrdTypeID($stateParams.pIS_N_R);


                data.MC_ORDER_SIZE_DTL = MrcDataService.xmlStringShort(dtlData.map(function (ob) {
                    //ob['PROD_BATCH_NO'] = $scope.form.PROD_BATCH_NO;
                    ob['SHIP_DT'] = $filter('date')(data.SHIP_DT, vm.dtFormat);
                    return ob;
                }));


                console.log(data);
                //return;

                return MrcDataService.saveDataByUrl(data, '/Order/SaveDevOrder').then(function (res) {
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


                            vm.form['MC_ORDER_H_ID'] = parseInt(res.data.PMC_ORDER_H_ID_RTN);
                            vm.form['MC_SMP_REQ_H_ID'] = parseInt(res.data.PMC_SMP_REQ_H_ID_RTN);

                            
                            if (!_.isNil(res.data.PJOB_TRAC_NO_RTN)) {
                                vm.form['JOB_TRAC_NO'] = res.data.PJOB_TRAC_NO_RTN;
                            }

                            vm.ordDtlGridDataSource.read();

                        }

                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.cancel = function () {
            vm.form = angular.copy(vm.oriForm);
            $scope.StyleOrderForm.$setPristine();
        }

    }

})();