(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitYdProgramController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$state', '$scope', 'cur_date_server', 'YarnList', 'FormData', '$filter', 'Dialog', '$modal', KnitYdProgramController]);
    function KnitYdProgramController($q, config, KnittingDataService, $stateParams, $state, $scope, cur_date_server, YarnList, FormData, $filter, Dialog, $modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        $scope.form = {};
        $scope.alerts = [];
        activate()
       
        vm.showSplash = false;
        $scope.reqListShowed = true;

        function activate() {
            var promise = [getApprovalStatus(), getbuyerAcList(), getYarnPartList(), getYarDyeSrc()];
            return $q.all(promise).then(function () {
                
            });
        }

        function getYarDyeSrc() {
            return KnittingDataService.LookupListData(112).then(function (res) {
                vm.YdSrcDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function getYarnPartList() {
            return KnittingDataService.LookupListData(79).then(function (res) {
                vm.PartListDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }

        function getDataFabType(pMC_FAB_PROD_ORD_H_LST) {
            return KnittingDataService.getDataByUrl('/KnitYdProgram/getFabricTypeByFabOrderH?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_LST).then(function (res) {
                vm.FabTypeDs = new kendo.data.DataSource({
                    data: res
                });
            });
        };

        function getbuyerAcList() {
            return vm.buyerAcList = {
                optionLabel: "--- Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return KnittingDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var ds = this.dataSource.data();
                    if (ds.length == 1) {
                        this.value(ds[0].MC_BYR_ACC_ID);
                        $scope.form['MC_BYR_ACC_ID'] = ds[0].MC_BYR_ACC_ID;
                    }
                },
                change: function (e) {
                    var item = this.dataItem(e.item);
                    if (item.MC_BYR_ACC_ID) {
                        var multiselect = $("#ORDER_LIST").data("kendoMultiSelect");
                            multiselect.dataSource.read();
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }


        vm.makeRequisition = function (pKNT_YD_PRG_H_ID) {
            Dialog.confirm('You are going to make Yarn Requisition.<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 return KnittingDataService.saveDataByUrl({}, '/KnitYdProgram/CreateRequisition?pKNT_YD_PRG_H_ID=' + pKNT_YD_PRG_H_ID).then(function (res) {
                     if (res.success === false) {
                         vm.errors = res.errors;
                     }

                     else {
                         res['data'] = angular.fromJson(res.jsonStr);
                         if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                             $state.go('KnitYdProgram', { pKNT_YD_PRG_H_ID: pKNT_YD_PRG_H_ID }, { reload: true, inherit: false });
                         }
                         config.appToastMsg(res.data.PMSG);
                     }
                 }).catch(function (message) {
                     exception.catcher('XHR loading Failded')(message);
                 });
             });
        };

        vm.makeRevise = function (KNT_YD_PRG_H_ID) {
            Dialog.confirm('You are going to revise program.<br> Click <b>Yes</b> to proceed', 'Attention', ['Yes', 'No'])
             .then(function () {
                 $state.go('KnitYdProgram', { pPARENT_ID: KNT_YD_PRG_H_ID}, { reload: true, inherit: false });
             });
        };

        function getApprovalStatus() {
            return KnittingDataService.LookupListData(126).then(function (res) {
                vm.ApprovalStatusDs = new kendo.data.DataSource({
                    data: res
                });
            });
        }
        vm.APRVL_DT_OPENED = [];
        vm.APRVL_DT_OPEN = function ($event, index) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.APRVL_DT_OPENED[index] = true;
        };


        vm.params = $stateParams;

        if (Object.keys(FormData).length > 0 && FormData.KNT_YD_PRG_H_ID && FormData.KNT_YD_PRG_H_ID > 0) {

            $scope.form = angular.copy(FormData);
            $scope.form['feeder_plans'] = FormData.feeder_plans.map(function (o) {
                o['details'] = o.details.map(function (b) { b['MC_FAB_PROD_ORD_H_ID'] = o.MC_FAB_PROD_ORD_H_ID; return b; });
                return o;
            });

            angular.forEach($scope.form.col_reqs, function (val, key) {
                val['yarnItemListDs'] = YarnList;
            });

            $scope.form['MC_FAB_PROD_ORD_H_LST'] = FormData.MC_FAB_PROD_ORD_H_LST.split(',');
            getDataFabType(FormData.MC_FAB_PROD_ORD_H_LST.split(','));
        } else {

            $scope.form = {
                PRG_ISS_DT: new Date(cur_date_server),
                feeder_plans: [],
                col_reqs: [],
                PRG_REF_NO: FormData.PRG_REF_NO
            }
        }




        $scope.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST #</h5><p> #: data.STYLE_NO||"" #||#:data.FAB_PROD_CAT_SNAME#</p></span>';
        $scope.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO_LST # (#: data.STYLE_NO||"" #||#:data.FAB_PROD_CAT_SNAME#)</h5></span>';

        vm.template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO #</b></h5><p> Stock: #: data.STK_BALANCE # #: data.MOU_CODE #</p></span>';
        vm.Yarntemplate = '<span><span style="padding:0px;margin:0px;">#: data.ITEM_NAME_EN # #if (data.PCT_RATIO>0)  { #<span class="label label-sm label-danger" style="padding:0px;margin:0px;">Suggested</span># } # </span>';
        vm.value_template = '<span><h5 style="padding:0px;margin:0px;"><b>#: data.YRN_LOT_NO # </b> (#: data.STK_BALANCE # #: data.MOU_CODE #)</h5></span>';



        $scope.FabOederByOhDs = {
            transport: {
                read: function (e) {
                    vm.showSplash = true;
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    var url = "/api/mrc/StyleHExt/getFabProdOrderDataOh";
                    url += "?pRF_FAB_PROD_CAT_LST=" + ($scope.form.RF_FAB_PROD_CAT_ID || 2);
                    url += "&pMC_BYR_ACC_ID=" + ($scope.form.MC_BYR_ACC_ID || null);

                    if (params.filter) {
                        url += '&pORDER_NO_LST=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '&pORDER_NO_LST';
                    }

                    if ($stateParams.pKNT_YD_PRG_H_ID) {
                        url += '&pMC_FAB_PROD_ORD_H_LST=' + (Object.keys(FormData).length > 0 ? FormData.MC_FAB_PROD_ORD_H_LST : '');
                    }

                    url += '&pMC_STYLE_H_ID=' +  (FormData.MC_STYLE_H_ID||'');
                   
                    url += '&pIS_YD_ONLY=Y';
                    return KnittingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                        vm.showSplash = false;
                    });
                }
            },
            serverFiltering: true
        };


        vm.kntProgDs = {
            optionLabel: "--Program--",
            filter: "contains",
            autoBind: true,
            template: '<span><h4 style="padding:0px;margin:0px;"><b>#: data.PRG_REF_NO #</b></h4><h6 style="margin:0px;">#: data.FAB_PROD_CAT_SNAME #|#: data.BYR_ACC_NAME_EN #|#: data.ORDER_NO #|#: data.SUP_TRD_NAME_EN #</h6></span>',
            dataSource: {
                transport: {
                    read: function (e) {
                        var url = "/KnitYdProgram/getYdProgramListDs";
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        if (params.filter) {
                            url += '?pPRG_REF_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                        } else {
                            url += '?pPRG_REF_NO';
                        }

                        KnittingDataService.getDataByUrl(url).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true,
            },
            change: function (e) {
                var item = this.dataItem(e.item);
                if (item && item.KNT_YD_PRG_H_ID) {
                    $state.go('KnitYdProgram', { pPARENT_ID: item.KNT_YD_PRG_H_ID, pREV: 'N' }, { reload: true, inherit: false });
                } else {
                    $state.go('KnitYdProgram', {}, { reload: true, inherit: false });
                };
            },
            dataTextField: "PRG_REF_NO",
            dataValueField: "KNT_YD_PRG_H_ID"
        };





        vm.itemTemplate = '<p>#: data.EMP_FULL_NAME_EN # <span style="display:block;font-style: italic;"> <small>#: data.USER_EMAIL #</small></span></p>',
        vm.tagTemplate = '<p><span style="display:block;font-style: italic;"> <small>#: data.USER_EMAIL #</small></span></p>',

        vm.onOrderChange = function (e) {

            if (e.sender.value().length > 0) {


                getDetailsData(e.sender.value().join(','), $stateParams.pKNT_YD_PRG_H_ID,$stateParams.pPARENT_ID);
                getDataFabType(e.sender.value().join(','));

            } else {

                $scope.form['feeder_plans'] = [];
                $scope.form['col_reqs'] = [];
               
            }
        }

        //vm.onOrderDataBound = function (e) {
        //    if (e.sender.value().length > 0) {
        //        getDetailsData(e.sender.value().join(','),null, $stateParams.pPARENT_ID);
        //        getDataFabType(e.sender.value().join(','));
        //    }
        //};

        function getDetailsData(pMC_FAB_PROD_ORD_H_LST, pKNT_YD_PRG_H_ID, pPARENT_ID) {
            KnittingDataService.getDataByUrl('/KnitYdProgram/getBaseColorsByYdCol?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_LST + '&pKNT_YD_PRG_H_ID=' + (pKNT_YD_PRG_H_ID || null) + '&pPARENT_ID=' + (pPARENT_ID||null)).then(function (res) {
                $scope.form['feeder_plans'] = res.map(function (o) {
                    o['details'] = o.details.map(function (b) { b['MC_FAB_PROD_ORD_H_ID'] = o.MC_FAB_PROD_ORD_H_ID; return b; });
                    return o;
                });
            });
            KnittingDataService.getDataByUrl('/KnitYdProgram/getColorListbyFabOrder?pMC_FAB_PROD_ORD_H_LST=' + pMC_FAB_PROD_ORD_H_LST + '&pKNT_YD_PRG_H_ID=' + (pKNT_YD_PRG_H_ID || null) + '&pPARENT_ID=' + (pPARENT_ID||null)).then(function (res) {
                $scope.form['col_reqs'] = res.map(function (o) {
                    o['yarnItemListDs'] = YarnList;
                    return o;
                });
            });
        }


        vm.addToList = function (data, list) {
            var data2add = angular.copy(data);
            data2add['KNT_YDP_D_YRN_ID'] = -1;
            list.push(data2add);
        };

        vm.removeItem = function (list, idx) {
            if (idx > -1) {
                list.splice(idx, 1);
            }
        }

        vm.onFeedNoChange = function(pNET_GFAB_QTY,list){
            var CalSum = _.sumBy(list, function (o) { return (o.RQD_FDR_QTY || 0) });
            var sum = CalSum == 0 ? pNET_GFAB_QTY : CalSum;

            angular.forEach(list, function (val, key) {
                val['CAL_YRN_QTY'] = Math.ceil(parseFloat((pNET_GFAB_QTY / sum) * (val.RQD_FDR_QTY || 0)));
                val['RQD_YD_QTY'] = Math.ceil(parseFloat((pNET_GFAB_QTY / sum) * (val.RQD_FDR_QTY || 0)));
            });
        };

        vm.onProdCategoryChange = function (e) {
            var cat = e.sender.dataItem(e.sender.item);
            if (cat.RF_FAB_PROD_CAT_ID) {
                $scope.form['MC_FAB_PROD_ORD_H_LST'] = [];
                $scope.form['feeder_plans'] = [];
                $scope.form['col_reqs'] = [];
                var multiselect = $("#ORDER_LIST").data("kendoMultiSelect");
                multiselect.dataSource.read();
               
            } 
        };


        vm.FabProductionCatDs = {
            transport: {
                read: function (e) {
                    return KnittingDataService.getDataByFullUrl('/api/common/GetFabProdCat').then(function (res) {
                        e.success(_.findByValues(res, 'RF_FAB_PROD_CAT_ID', '1,2,3,5,6,8'))
                    });
                }

            }
        };


        //function getYarnItemList() {
        //    var url = '/KnitPlan/getYarnItemByFabId?pMC_STYLE_D_FAB_ID';
        //    url += '&pRF_FAB_PROD_CAT_ID=10';
        //    url += '&pKNT_SC_PRG_RCV_ID';
        //    return KnittingDataService.getDataByUrl(url).then(function (res) {
        //        console.log(res);
        //    });
        //}

        vm.onChangeFabType = function (item, e) {
            var FabTypeObj = e.sender.dataItem(e.sender.item);
            if (FabTypeObj.RF_FAB_TYPE_ID) {
                item['IS_FBP_VISIBLE'] = FabTypeObj.IS_FBP_VISIBLE;
            } else {
                item['IS_FBP_VISIBLE'] = 'N';
                item['LK_YFAB_PART_ID'] = '';
            }
        };


        vm.onChangeYarnItem = function (item, e, base, index) {
            var obj = e.sender.dataItem(e.sender.item);
            if (obj.YARN_ITEM_ID) {
                KnittingDataService.getDataByFullUrl('/api/common/GetCategoryWiseBrandList/2?pOption=3003&pKNT_YRN_LOT_ID_LST=' + obj.KNT_YRN_LOT_ID_LST).then(function (res) {
                    item['brandListDs'] = new kendo.data.DataSource({
                        data: res
                    });

                    if (index == 0) {
                        angular.forEach($scope.form.col_reqs, function (val, key) {
                            angular.forEach(val.details, function (v, k) {

                                if (v.KNT_YDP_D_COL_ID <= 0 && v.YARN_ITEM_ID <= 0) {
                                    v['YARN_ITEM_ID'] = item.YARN_ITEM_ID;
                                    v['brandListDs'] = new kendo.data.DataSource({
                                        data: res
                                    });
                                }

                            });
     
                        });
                    }


                });



            } else {
                item['brandListDs'] = new kendo.data.DataSource({
                    data: []
                });

                item['yarnLotListDs'] = new kendo.data.DataSource({
                    data: []
                });
            }
        };
        vm.onBoundYarnItem = function (item, base, index) {
            if (item.KNT_YRN_LOT_ID_LST) {
                return KnittingDataService.getDataByFullUrl('/api/common/GetCategoryWiseBrandList/2?pOption=3003&pKNT_YRN_LOT_ID_LST=' + item.KNT_YRN_LOT_ID_LST).then(function (res) {
                    item['brandListDs'] = new kendo.data.DataSource({
                        data: res
                    });
                });
            }
        }
        vm.onChangeBrnad = function (item, e, base, index) {
            var objBrand = e.sender.dataItem(e.sender.item);
            if (objBrand.RF_BRAND_ID) {
                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLot?pKNT_YRN_LOT_ID_LST=' + objBrand.KNT_YRN_LOT_ID_LST + '&pRF_BRAND_ID=' + objBrand.RF_BRAND_ID + '&pYARN_ITEM_ID=' + item.YARN_ITEM_ID).then(function (res) {
                    item['yarnLotListDs'] = new kendo.data.DataSource({
                        data: res
                    });


                    if (index == 0) {
                        angular.forEach($scope.form.col_reqs, function (val, key) {
                            if (val.KNT_YDP_D_COL_ID <= 0 && !val.RF_BRAND_ID) {
                                val['RF_BRAND_ID'] = objBrand.RF_BRAND_ID;;
                                val['yarnLotListDs'] = new kendo.data.DataSource({
                                    data: res
                                });
                            }
                        });
                    }

                });

            } else {
                item.yarnLotListDs = new kendo.data.DataSource({
                    data: []
                });
            }
        }
        vm.onBoundBrnad = function (item, base, index) {
            if (item.YARN_ITEM_ID && item.KNT_YRN_LOT_ID_LST && item.RF_BRAND_ID) {
                return KnittingDataService.getDataByUrl('/KnitPlan/getYarnLot?pKNT_YRN_LOT_ID_LST=' + item.KNT_YRN_LOT_ID_LST + '&pRF_BRAND_ID=' + item.RF_BRAND_ID + '&pYARN_ITEM_ID=' + item.YARN_ITEM_ID).then(function (res) {
                    item['yarnLotListDs'] = new kendo.data.DataSource({
                        data: res
                    });

                    if (base) {
                        angular.forEach($scope.datas, function (val, key) {
                            if (val['items'][index]['KNT_PLAN_D_ID'] < 0) {
                                val['items'][index]['RF_BRAND_ID'] = item.RF_BRAND_ID;
                                val['items'][index]['yarnLotListDs'] = new kendo.data.DataSource({
                                    data: res
                                });
                            }
                        });
                    }

                });
            }
        }

        vm.onChangeYarnLot = function (item, e, base, index) {
            var objLot = e.sender.dataItem(e.sender.item);
            if (objLot.KNT_YRN_LOT_ID) {
                item['STK_BALANCE'] = (objLot.STK_BALANCE || 0);
                item['KNT_YRN_LOT_STK_ID'] = objLot.KNT_YRN_LOT_STK_ID;
                if (base) {
                    angular.forEach($scope.datas, function (val, key) {
                        if (val['items'][index]['KNT_PLAN_D_ID'] < 0) {
                            val['items'][index]['KNT_YRN_LOT_ID'] = objLot.KNT_YRN_LOT_ID;
                        }
                    });
                }

            } else {
                item['STK_BALANCE'] = 0;
                item['KNT_YRN_LOT_STK_ID'] = '';

                if (base) {
                    angular.forEach($scope.datas, function (val, key) {
                        if (val['items'][index]['KNT_PLAN_D_ID'] < 0) {
                            val['items'][index]['KNT_YRN_LOT_ID'] = '';
                        }
                    });
                }



            }
        }
        vm.onBoundYarnLot = function (item, base, index) {
            if (item.KNT_YRN_LOT_ID && item.KNT_YRN_LOT_ID > 0) {
                item['STK_BALANCE'] = _.find(item.yarnLotListDs.data(), function (o) {
                    return o.KNT_YRN_LOT_ID == item.KNT_YRN_LOT_ID;
                }) ? _.find(item.yarnLotListDs.data(), function (o) {
                    return o.KNT_YRN_LOT_ID == item.KNT_YRN_LOT_ID;
                }).STK_BALANCE : 0;


                item['KNT_YRN_LOT_STK_ID'] = _.find(item.yarnLotListDs.data(), function (o) {
                    return o.KNT_YRN_LOT_ID == item.KNT_YRN_LOT_ID;
                }) ? _.find(item.yarnLotListDs.data(), function (o) {
                    return o.KNT_YRN_LOT_ID == item.KNT_YRN_LOT_ID;
                }).KNT_YRN_LOT_STK_ID : '';

                if (base) {
                    angular.forEach($scope.datas, function (val, key) {
                        if (val['items'][index]['KNT_PLAN_D_ID'] < 0) {
                            val['items'][index]['KNT_YRN_LOT_ID'] = item.KNT_YRN_LOT_ID;
                        }
                    });
                }
            }
        }


        vm.dtFormat = config.appDateFormat;
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.PRG_ISS_DT_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.PRG_ISS_DT_OPENED = true;
        };

        vm.DELV_TGT_DT_OPEN = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.DELV_TGT_DT_OPENED = true;
        };


        vm.supplierDs = {
            transport: {
                read: function (e) {
                    return KnittingDataService.getDataByFullUrl('/api/purchase/SupplierProfile/GetTypeWiseSuplier?pLK_SUP_TYPE_ID=556').then(function (res) {
                        e.success(res);
                    });
                }

            }
        };



        //function getUserList() {
        //    return KnittingDataService.getDataByFullUrl('/api/common/SelectAllUserData').then(function (res) {
        //        vm.userListDs = new kendo.data.DataSource({
        //            data: res
        //        });
        //    });
        //};



        function gropColorWiseData(datas) {
            console.log(datas);
            angular.forEach(_.map(_.groupBy(datas, function (o) {
                return o.MC_FAB_PROD_ORD_H_ID + o.YRN_COLOR_ID;
            }), function (g) {
                return {
                    MC_FAB_PROD_ORD_H_ID: parseInt(g[0].MC_FAB_PROD_ORD_H_ID),
                    YRN_COLOR_ID: parseInt(g[0].YRN_COLOR_ID),
                    CAL_YRN_QTY: _.sumBy(g, function (oo) { return oo.CAL_YRN_QTY || 0 })
                }
            }
             ), function (val, key) {

  

                 var idx = _.findIndex($scope.form.col_reqs, function (o) { return o.YRN_COLOR_ID == val.YRN_COLOR_ID && o.MC_FAB_PROD_ORD_H_ID == val.MC_FAB_PROD_ORD_H_ID ; })

                 if (idx > -1) {
                     $scope.form.col_reqs[idx]['RQD_YD_QTY'] = val.CAL_YRN_QTY;

                     if ($scope.form.col_reqs[idx]['details'].length == 1) {
                         $scope.form.col_reqs[idx]['details'][0]['RQD_YD_QTY'] = val.CAL_YRN_QTY;
                         $scope.form.col_reqs[idx]['details'][0]['RATIO_QTY'] = 100;
                     }
                 } 

             });
        }

        $scope.$watch('form.feeder_plans', function (newVal, oldVal) {
            var flatData = [];
            if (newVal.length == 0) {
                return;
            }

            angular.forEach(newVal, function (val, key) {

                flatData = flatData.concat(val.details);

                if ((newVal.length - 1) == key) {
                    gropColorWiseData(flatData);
                }
            });

        },true);

        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };


        vm.submitData = function (data, isValid) {
            if (!isValid)
                return;

            if (!data.SCM_SUPPLIER_ID) {
                $scope.YdProgramFrom.SCM_SUPPLIER_ID.$setValidity('required', false);
                return;
            }


            var feeder_plans = [];
            var col_reqs = [];

            angular.forEach(data.feeder_plans, function (val, key) {
                angular.forEach(val.details, function (vl, ky) {
                        feeder_plans.push({
                            YD_COMBO_ID: val.YD_COMBO_ID,
                            MC_FAB_PROD_ORD_H_ID: val.MC_FAB_PROD_ORD_H_ID,
                            KNT_YDP_D_FDR_ID: vl.KNT_YDP_D_FDR_ID,
                            RQD_FDR_QTY: vl.RQD_FDR_QTY,
                            YRN_COLOR_ID: vl.YRN_COLOR_ID,
                            CAL_YRN_QTY: vl.CAL_YRN_QTY
                        });
                });
            });

            angular.forEach(data.col_reqs, function (val, key) {

                //if (val.RQD_YD_QTY > 0) {
                    col_reqs.push({
                        YRN_COLOR_ID: val.YRN_COLOR_ID,
                        IS_YDIP_RQD: val.IS_YDIP_RQD || 'Y',
                        LK_YD_SRC_TYP_ID: (val.LK_YD_SRC_TYP_ID||''),
                        COL_REF_NO: val.COL_REF_NO,
                        YD_COLOR_NAME: val.COLOR_NAME_EN,
                        //IS_QTY_EQ: _.sumBy(val.details, function (oo) { return oo.RQD_YD_QTY || 0 }) == val.RQD_YD_QTY,
                        YARN_DUPLI_CNT: _.some(_.map(_.groupBy(val.details, function (o) {
                            return parseInt(o.RF_FAB_TYPE_ID || 0) + parseInt(o.YARN_ITEM_ID || 0) + parseInt(o.RF_BRAND_ID || 0) + parseInt(o.KNT_YRN_LOT_ID || 0) + parseInt(o.LK_YFAB_PART_ID || 0)
                        }), function (g) {
                            return g.length
                        }
                        ), function (o) { return o > 1 }),

                        YARN_XML: val.RQD_YD_QTY > 0 ? config.xmlStringShortNoTag(
                                    val.details.map(function (o) {
                                        return {
                                            KNT_YDP_D_YRN_ID: o.KNT_YDP_D_YRN_ID,
                                            RF_FAB_TYPE_ID: o.RF_FAB_TYPE_ID,
                                            YARN_ITEM_ID: o.YARN_ITEM_ID,
                                            KNT_YRN_LOT_ID: o.KNT_YRN_LOT_ID,
                                            RF_BRAND_ID: o.RF_BRAND_ID,
                                            LK_YFAB_PART_ID: o.LK_YFAB_PART_ID,
                                            RATIO_QTY: o.RATIO_QTY,
                                            RQD_YD_QTY: o.RQD_YD_QTY,
                                            RQD_CONE_QTY: o.RQD_CONE_QTY,
                                            QTY_PER_CONE: 0,
                                            REMARKS: o.REMARKS
                                        }
                                    })
                            ) : '',
                        RQD_YD_QTY: val.RQD_YD_QTY,
                        KNT_YDP_D_COL_ID: val.KNT_YDP_D_COL_ID,

                        MC_FAB_PROD_ORD_H_ID: val.MC_FAB_PROD_ORD_H_ID,
                        LK_YD_APV_STS_ID: (val.LK_YD_APV_STS_ID || -1),
                        COMMENTS: (val.COMMENTS || ''),
                        APRVL_DT: val.APRVL_DT ? $filter('date')(val.APRVL_DT, 'dd-MM-yyyy') : null,
                    });
                //}

            });

            if (_.some(col_reqs, function (o) { return o.YARN_DUPLI_CNT == true })) {
                $scope.alerts.push({
                    type: 'danger',
                    msg: 'Duplicate yarn/allocated qty not matched. Pls check and try again.'
                });
                setTimeout(function () {
                    $scope.alerts = [];
                }, 5000);

            } else {
                var d2save = {
                    KNT_YD_PRG_H_ID: data.KNT_YD_PRG_H_ID || -1,
                    MC_FAB_PROD_ORD_H_LST: data.MC_FAB_PROD_ORD_H_LST.join(','),
                    PRG_REF_NO: data.PRG_REF_NO,
                    PRG_ISS_DT: $filter('date')(data.PRG_ISS_DT, 'dd-MMM-yyyy'),
                    DELV_TGT_DT: $filter('date')(data.DELV_TGT_DT, 'dd-MMM-yyyy'),
                    IS_SUB_CONTR: data.IS_SUB_CONTR || 'N',
                    SCM_SUPPLIER_ID: data.SCM_SUPPLIER_ID,
                    PARENT_ID: data.PARENT_ID,
                    REMARKS: data.REMARKS || '',
                    REVISION_NO: data.REVISION_NO || 0,
                    IS_FINALIZED: data.IS_FINALIZED_M || 'N',
                    RF_FAB_PROD_CAT_ID: data.RF_FAB_PROD_CAT_ID,
                    FEEDER_PLANS_XML: config.xmlStringShort(feeder_plans),
                    COL_REQS_XML: config.xmlStringShort(col_reqs.map(function (b) {
                        return {
                            YRN_COLOR_ID: b.YRN_COLOR_ID,
                            IS_YDIP_RQD: b.IS_YDIP_RQD,
                            LK_YD_SRC_TYP_ID: b.LK_YD_SRC_TYP_ID,
                            COL_REF_NO: b.COL_REF_NO,
                            YARN_XML: b.YARN_XML,
                            RQD_YD_QTY: b.RQD_YD_QTY,
                            KNT_YDP_D_COL_ID: b.KNT_YDP_D_COL_ID,
                            MC_FAB_PROD_ORD_H_ID: b.MC_FAB_PROD_ORD_H_ID,
                            LK_YD_APV_STS_ID: b.LK_YD_APV_STS_ID,
                            COMMENTS: b.COMMENTS,
                            YD_COLOR_NAME: b.YD_COLOR_NAME,
                            APRVL_DT: b.APRVL_DT
                           };
                     })),
                    MC_BYR_ACC_ID: data.MC_BYR_ACC_ID
                };

                console.log(d2save);
                

                return KnittingDataService.saveDataByUrl(d2save, '/KnitYdProgram/Save').then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $state.go('KnitYdProgram', { pKNT_YD_PRG_H_ID: parseInt(res.data.OP_KNT_YD_PRG_H_ID) }, { reload: true, inherit: false });
                        }
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.printProgramRpt = function (pKNT_YD_PRG_H_ID) {
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-3528' }, {
                KNT_YD_PRG_H_ID: pKNT_YD_PRG_H_ID
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

        vm.openYrnReqModal = function (dataItemReq) {
            console.log(dataItemReq);

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Knitting/Knit/_KnitYdProgramYarnReqModal',
                controller: 'KnitYdProgramYarnReqModalController',
                size: 'lg',
                windowClass: 'app-modal-window',
                resolve: {
                    YdPrgData: function () {
                        return { KNT_YD_PRG_H_ID: $scope.form.KNT_YD_PRG_H_ID, PRG_REF_NO: $scope.form.PRG_REF_NO };
                    },
                    YrnStrReq: function () {
                        return dataItemReq;
                    }
                }
            });

            modalInstance.result.then(function (data) {

                console.log('test');
                vm.reqGridDataSource.read();                

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
            
        }


        vm.reqGridDataSource = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    return KnittingDataService.getDataByFullUrl('/api/knit/KnitYdProgram/GetYrnReqList4YD?&pKNT_YD_PRG_H_ID=' + ($scope.form.KNT_YD_PRG_H_ID || 0)).then(function (res) {

                        console.log(res);

                        e.success(res);
                    });
                }
            }
        });

        vm.reqGridOptions = {
            height: 150,
            scrollable: true,
            pageable: false,
            editable: false,
            selectable: "cell",
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
                { field: "STR_REQ_NO", title: "Requisition No", type: "string", width: "10%" },
                { field: "STR_REQ_DT", title: "Requisition Date", type: "date", template: "#= kendo.toString(kendo.parseDate(STR_REQ_DT,'yyyy-MM-dd'),'dd-MMM-yyyy') #", width: "10%" },
                { field: "REQ_TYPE_NAME", title: "Requisition Type", type: "string", width: "20%" },
                { field: "USER_NAME_EN", title: "Create By", type: "string", width: "10%" },
                { field: "TTL_REQ_QTY", title: "TTL Req. Qty", type: "string", width: "8%" },
                { field: "EVENT_NAME", title: "Status", type: "string", width: "10%" },
                {
                    title: "Action",
                    template: function () {
                        return "<button type='button' class='btn btn-xs blue' ng-click='vm.openYrnReqModal(dataItem)' >View</button>&nbsp;" + 
                            "<button type='button' class='btn btn-xs red' ng-click='vm.removeRequsition(dataItem)' ng-disabled='dataItem.RF_ACTN_STATUS_ID!=37' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "10%"
                }
            ]
        };

        vm.removeRequsition = function (dataItem) {
            var vMsg = 'Do you want to remove?';


            Dialog.confirm(vMsg, 'Confirmation', ['Yes', 'No']).then(function () {
                return KnittingDataService.saveDataByFullUrl(dataItem, "/api/Knit/KnitYdProgram/RemoveRequisition4YD").then(function (res) {

                    $scope.errors = undefined;
                    if (res.success === false) {
                        $scope.errors = [];
                        $scope.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                           
                            vm.reqGridDataSource.remove(dataItem);
                        };

                        config.appToastMsg(res.data.PMSG);
                    }

                });
            });
        }

    }
})();