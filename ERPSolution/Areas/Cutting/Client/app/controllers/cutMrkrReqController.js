////////// Start CutMrkrReqHController Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutMrkrReqHController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', '$modal', 'CuttingDataService', 'Dialog', 'mrkrReqHdrData', CutMrkrReqHController]);
    function CutMrkrReqHController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, $modal, CuttingDataService, Dialog, mrkrReqHdrData) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        console.log(mrkrReqHdrData);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");
        
        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'GMT_MRKR_REQ_ID';
        vm.today = new Date();

        vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p>(#: data.ORDER_NO #)</p></span>';
        vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';

        vm.form = mrkrReqHdrData[key] ? mrkrReqHdrData : {
            HR_COMPANY_ID: 1, HR_PROD_FLR_ID: 4, GMT_CUT_TABLE_ID: null, MRKR_REQ_DT: vm.today, GMT_MRKR_REQ_ID: 0, 
            MC_BYR_ACC_GRP_ID: $stateParams.pMC_BYR_ACC_GRP_ID || 0, MC_STYLE_H_EXT_ID: $stateParams.pMC_STYLE_H_EXT_ID || 0,
            MC_ORDER_H_ID: $stateParams.pMC_ORDER_H_ID || 0, GMT_COLOR_ID: $stateParams.pGMT_COLOR_ID || null, MC_STYLE_H_ID: 0
        };
        $scope.form = mrkrReqHdrData[key] ? mrkrReqHdrData : {
            HR_COMPANY_ID: 1, HR_PROD_FLR_ID: 4, GMT_CUT_TABLE_ID: null, MRKR_REQ_DT: vm.today, GMT_MRKR_REQ_ID: 0, 
            MC_BYR_ACC_GRP_ID: $stateParams.pMC_BYR_ACC_GRP_ID || 0, MC_STYLE_H_EXT_ID: $stateParams.pMC_STYLE_H_EXT_ID || 0, 
            MC_ORDER_H_ID: $stateParams.pMC_ORDER_H_ID || 0, GMT_COLOR_ID: $stateParams.pGMT_COLOR_ID || null, MC_STYLE_H_ID: 0
        };


        //vm.style = { MC_BYR_ACC_GRP_ID: 0, MC_STYLE_H_EXT_ID: 0, MC_STYLE_H_ID: 0, GMT_COLOR_ID: 0, MC_ORDER_H_ID: 0 };
                 

        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getCompanyList(), getLocationList(), getCuttingTableList(), getBuyerAcGrpList(), getByrAccWiseStyleExtList(),
                /*getOrderItem(),*/ getOrderColor(), getMarkerList()
            ];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }

        vm.reqDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.reqDateOpened = true;
        };
        
        
        function getCompanyList() {
            vm.companyOptions = {
                optionLabel: "-- Select Company --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.HR_COMPANY_ID = item.HR_COMPANY_ID;                   
                }
            };

            return vm.companyDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/common/CompanyList';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getLocationList() {
            vm.mcLocationOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FLOOR_DESC_EN",
                dataValueField: "HR_PROD_FLR_ID"
            };

            return vm.mcLocationDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CuttingDataService.getDataByFullUrl('/api/hr/GetProdFloorList?pLK_PFLR_TYP_ID=531').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                group: { field: "BLDNG_DESC_EN" },
                sort: [{ field: 'HR_PROD_BLDNG_ID', dir: 'asc' }]
            });
        }

        function getCuttingTableList() {
            vm.cuttingTableOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "TABLE_NO",
                dataValueField: "GMT_CUT_TABLE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.GMT_CUT_TABLE_ID = item.GMT_CUT_TABLE_ID;
                }
            };

            return vm.cuttingTableDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/Cutting/MrkrReq/GetCuttingTableList';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);

                            if (res.length == 1) {
                                vm.form.GMT_CUT_TABLE_ID = res[0].GMT_CUT_TABLE_ID;
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getBuyerAcGrpList() {

            vm.buyerAcGrpOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    
                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.BYR_ACC_GRP_NAME_EN = item.BYR_ACC_GRP_NAME_EN;

                    vm.OrdStyleExtDataSource.read();
                }                
            };

            vm.buyerAcGrpDataSource = new kendo.data.DataSource({                
                transport: {
                    read: function (e) {                        
                        var url = '/api/mrc/BuyerAcc/GetBuyerAccGrpList';
                       
                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);

                            if (res.length == 1) {
                                vm.form.MC_BYR_ACC_GRP_ID = res[0].MC_BYR_ACC_GRP_ID;
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
           
        }

        function getByrAccWiseStyleExtList() {
            vm.OrdStyleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    //vm.form.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;

                    vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;
                    vm.form.STYLE_NO = item.STYLE_NO;
                    vm.form.ORDER_NO = item.ORDER_NO;

                    //vm.getOrderColor(item.MC_FAB_PROD_ORD_H_ID);
                    vm.OrdColorDataSource.read();
                    //vm.OrdItemDataSource.read();
                },
                dataBound: function (e) {
                    console.log('Style Data Bound');

                    var ds = _.filter(e.sender.dataSource.data(), function (ob) {
                        if ($stateParams.pMC_STYLE_H_EXT_ID == ob.MC_STYLE_H_EXT_ID) {
                            return ob;
                        }
                    });

                    console.log(ds);

                    if (ds.length > 0) {
                        //vm.form.MC_FAB_PROD_ORD_H_ID = ds[0].MC_FAB_PROD_ORD_H_ID;
                        vm.form.MC_STYLE_H_ID = ds[0].MC_STYLE_H_ID;
                        vm.form.MC_ORDER_H_ID = ds[0].MC_ORDER_H_ID;
                        vm.form.STYLE_NO = ds[0].STYLE_NO;
                        vm.form.ORDER_NO = ds[0].ORDER_NO;
                    }
                }
            };

            

            vm.OrdStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetStyleExOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0) + '&pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID;
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += CuttingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData);

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);

                            if (res.length == 1) {
                                vm.form.MC_STYLE_H_EXT_ID = res[0].MC_STYLE_H_EXT_ID;
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
            
        };

        //api/mrc/StyleDItem/ChildItemListByStyle/1
        function getOrderItem() {
            vm.OrdItemOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "MC_STYLE_D_ITEM_ID"//,
                //select: function (e) {
                //    var item = this.dataItem(e.item);
                //    $scope.viewOrdItem.FROM_COLOR_NAME_EN = item.COLOR_NAME_EN;
                //    console.log(item);
                //}
            };

            return vm.OrdItemDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/StyleDItem/ChildItemListByStyle/' + vm.form.MC_STYLE_H_ID;

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {

                            angular.forEach(res, function (val, key) {
                                if (val['MODEL_NO'] != '') {
                                    val['ITEM_NAME_EN'] = val['ITEM_NAME_EN'] + ' ' + val['MODEL_NO'];
                                }
                                else if (val['COMBO_NO'] != '') {
                                    val['ITEM_NAME_EN'] = val['ITEM_NAME_EN'] + ' ' + val['COMBO_NO'];
                                }
                            });

                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getOrderColor() {
            vm.OrdColorOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);
                    
                    vm.form.GMT_COLOR_ID = item.MC_COLOR_ID;
                    vm.form.COLOR_NAME_EN = item.COLOR_NAME_EN;
                    
                    vm.mrkrListDataSource.read();
                }
            };

            return vm.OrdColorDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/Order/OrderOrByerAccWiseColorList/' + (vm.form.MC_ORDER_H_ID || -1) + '/null';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {

                            var colorList = _.map(_.groupBy(res, function (doc) {
                                return doc.MC_COLOR_ID;
                            }), function (grouped) {
                                return grouped[0];
                            });

                            e.success(colorList);

                            if (colorList.length == 1) {
                                vm.form.GMT_COLOR_ID = colorList[0].MC_COLOR_ID;
                                vm.form.COLOR_NAME_EN = colorList[0].COLOR_NAME_EN;

                                vm.mrkrListDataSource.read();
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        function getMarkerList() {
            vm.mrkrListOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "MRKR_SH_DESC",
                dataValueField: "GMT_MRKR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);                    
                }
            };

            return vm.mrkrListDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/Cutting/MrkrReq/GetMarkerList?pMC_STYLE_H_ID=' + (vm.form.MC_STYLE_H_ID||0);

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);

                            if (res.length == 1) {
                                vm.form.GMT_MRKR_ID = res[0].GMT_MRKR_ID;
                            }

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        }

        vm.markerModal = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'MarkerModal.html',
                controller: 'MarkerModalController',
                controllerAs: 'vm',
                size: 'lg',
                //scope: $scope,
                windowClass: 'app-modal-window',
                resolve: {
                    stylOrdData: function () {
                        var data=vm.form;
                        data['LK_WAY_TYPE_ID']=668;
                        data['LK_MRKR_TYPE_ID']=669;
                        
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                
                console.log(data);

                if (data.GMT_MRKR_ID && data.GMT_MRKR_ID > 0) {
                    vm.mrkrListDataSource.read().then(function () {
                        vm.form['GMT_MRKR_ID'] = data.GMT_MRKR_ID;
                    });
                }        
                
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };

        vm.mrkrListGridOption = {
            height: 170,
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
                //{ field: "CHALAN_DT", title: "Date", format: "{0:dd-MM-yyyy}", width: "100px", editable: false, filterable: false },
                { field: "MRKR_REF_NO", title: "Ref#", width: "30%", editable: false, filterable: true },
                { field: "BK_FIN_DIA", title: "Bking Dia", width: "20%", editable: false },
                { field: "CUT_FIN_DIA", title: "Cutable Dia", width: "20%", editable: false },
                { field: "MRKR_LEN", title: "Length", width: "15%", editable: false },
                { field: "MRKR_WDT", title: "Width", width: "15%", editable: false }
                //{
                //    title: "",
                //    template: function () {
                //        return "<button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.IS_FINALIZE==\"Y\"' ><i class='fa fa-remove'></i></button>";
                //    },
                //    width: "50px"
                //}
            ]
        };


        vm.mrkrListGridDataSource = new kendo.data.DataSource({                       
            transport: {
                read: function (e) {                    
                    var url = '/api/Cutting/MrkrReq/GetMarkerList?pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID;
                    
                    console.log(url);

                    CuttingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },           
            schema: {               
                model: {
                    id: "GMT_MRKR_ID",
                    fields: {                        
                        MRKR_REF_NO: { type: "string", filterable: false }
                    }
                }
            }
        });
        
      
        $scope.$watch('vm.form', function (newVal, oldVal) {
            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                }
            }
        }, true);

        $scope.$watchGroup(['vm.form.MC_BYR_ACC_GRP_ID', 'vm.form.MC_STYLE_H_EXT_ID', 'vm.form.GMT_COLOR_ID'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.selectedItem = undefined;
                    vm.errors = null;

                    vm.IS_NEXT = 'N';
                    
                   
                }
            }
        }, true);
             
        
        vm.save = function () {
            var submitData = angular.copy(vm.form);
            
            console.log(submitData);
            //return;


            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + $scope.token },
                         url: '/api/Cutting/MrkrReq/MrkrReqSave',
                         method: 'post',
                         data: submitData
                     }).then(function (res) {
                         //$scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             //$scope.$parent.errors = [];
                             //$scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             if (res.data.PGMT_MRKR_REQ_ID_RTN > 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                 vm.form['GMT_MRKR_REQ_ID'] = res.data.PGMT_MRKR_REQ_ID_RTN;
                                 vm.form['MRKR_REQ_NO'] = res.data.PMRKR_REQ_NO_RTN;
                                 vm.form['RF_ACTN_STATUS_ID'] = res.data.PRF_ACTN_STATUS_ID_RTN;

                                 $state.go('MrkrReqH', { pGMT_MRKR_REQ_ID: res.data.PGMT_MRKR_REQ_ID_RTN }, { notify: false });
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });           

        };

        vm.submit = function () {
            var submitData = angular.copy(vm.form);

            console.log(submitData);
            //return;


            Dialog.confirm('Do you want to finalize?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + $scope.token },
                         url: '/api/Cutting/MrkrReq/MrkrReqFinalize',
                         method: 'post',
                         data: submitData
                     }).then(function (res) {
                         //$scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             //$scope.$parent.errors = [];
                             //$scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                                 vm.form['RF_ACTN_STATUS_ID'] = res.data.PRF_ACTN_STATUS_ID_RTN;
                                                                  
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });

        };


        vm.backToList = function () {
            $state.go('MrkrReqList', { pMC_BYR_ACC_GRP_ID: vm.form.MC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID });
        }

    }

})();
////////// End CutMrkrReqHController Controller








////////// Start CutMrkrReqListController Controller
(function () {
    'use strict';
    angular.module('multitex.cutting').controller('CutMrkrReqListController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', '$modal', 'CuttingDataService', 'Dialog', CutMrkrReqListController]);
    function CutMrkrReqListController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, $modal, CuttingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        console.log($stateParams);
        //var rcvDate = moment($stateParams.pRCV_DT).format("DD-MMM-YYYY");

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'GMT_MRKR_REQ_ID';
        vm.today = new Date();

        vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p>(#: data.ORDER_NO #)</p></span>';
        vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';

        vm.form = { MC_BYR_ACC_GRP_ID: $stateParams.pMC_BYR_ACC_GRP_ID || 0, MC_STYLE_H_EXT_ID: $stateParams.pMC_STYLE_H_EXT_ID || 0 };
       
        //vm.style = { MC_BYR_ACC_GRP_ID: 0, MC_STYLE_H_EXT_ID: 0, MC_STYLE_H_ID: 0, GMT_COLOR_ID: 0, MC_ORDER_H_ID: 0 };


        $("input[type=text]").focus(function () { $(this).select(); }).mouseup(
           function (e) {
               e.preventDefault();
           });

        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [getBuyerAcGrpList(), getByrAccWiseStyleExtList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;
            });
        }


        function getBuyerAcGrpList() {

            vm.buyerAcGrpOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BYR_ACC_GRP_NAME_EN",
                dataValueField: "MC_BYR_ACC_GRP_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_BYR_ACC_GRP_ID = item.MC_BYR_ACC_GRP_ID;
                    vm.form.MC_STYLE_H_EXT_ID = null;

                    vm.OrdStyleExtDataSource.read();
                }
            };

            vm.buyerAcGrpDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        var url = '/api/mrc/BuyerAcc/GetBuyerAccGrpList';

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        }

        function getByrAccWiseStyleExtList() {
            vm.OrdStyleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    //vm.form.MC_FAB_PROD_ORD_H_ID = item.MC_FAB_PROD_ORD_H_ID;
                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;
                    vm.form.MC_ORDER_H_ID = item.MC_ORDER_H_ID;                    
                }
            };

            vm.OrdStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetStyleExOrderList?pMC_BYR_ACC_GRP_ID=' + ((vm.form.MC_BYR_ACC_GRP_ID > 0) ? vm.form.MC_BYR_ACC_GRP_ID : 0) + '&pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID;
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += CuttingDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData);

                        return CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };


        vm.FROM_DT_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.FROM_DT_Opened = true;
        }

        vm.TO_DT_Open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.TO_DT_Opened = true;
        }


        
        vm.mrkrReqListGridOption = {
            height: 400,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
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
                { field: "MRKR_REQ_DT", title: "Date", format: "{0:dd-MM-yyyy}", width: "10%", editable: false, filterable: false },
                { field: "MRKR_REQ_NO", title: "Ref#", width: "12%", editable: false, filterable: true },
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer A/C", width: "12%", editable: false, filterable: false },
                { field: "STYLE_NO", title: "Style#", width: "13%", editable: false, filterable: false },
                { field: "ORDER_NO", title: "Order#", width: "13%", editable: false, filterable: false },
                { field: "MRKR_SH_DESC", title: "Marker Description", width: "20%", editable: false, filterable: false },
                { field: "MRKR_RQD_QTY", title: "Qty", width: "5%", editable: false, filterable: false },
                {
                    field: "ACTN_STATUS_NAME",
                    title: "Status",
                    template: function () {
                        return "<label class='label label-sm label-warning' ng-show='dataItem.RF_ACTN_STATUS_ID==101' >{{dataItem.ACTN_STATUS_NAME}}</label> <label class='label label-sm label-success' ng-show='dataItem.RF_ACTN_STATUS_ID==102' >{{dataItem.ACTN_STATUS_NAME}}</label>";
                    },
                    width: "10%"
                },
                {
                    title: "",
                    template: function () {
                        return "<a class='btn btn-xs blue' href='/Cutting/Cutting/MrkrReq?a=363/#/mrkrReqH/{{dataItem.GMT_MRKR_REQ_ID}}?pMC_BYR_ACC_GRP_ID={{dataItem.MC_BYR_ACC_GRP_ID}}&pMC_STYLE_H_EXT_ID={{dataItem.MC_STYLE_H_EXT_ID}}&pMC_ORDER_H_ID={{dataItem.MC_ORDER_H_ID}}&pGMT_COLOR_ID={{dataItem.GMT_COLOR_ID}}' ><i class='fa fa-edit'></i></a>";
                    },
                    width: "5%"
                }
            ]
        };


        vm.mrkrReqListGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "GMT_MRKR_REQ_ID",
                    fields: {
                        MRKR_REQ_DT: { type: "date", editable: false },
                        MRKR_REQ_NO: { type: "string", filterable: false }
                    }
                }
            },
            pageSize: 10,
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/Cutting/MrkrReq/GetMrkrReqList?pageNumber=' + params.page + '&pageSize=' + params.pageSize + '&pMC_BYR_ACC_GRP_ID=' + vm.form.MC_BYR_ACC_GRP_ID || 0 + '&pMC_STYLE_H_EXT_ID=' + vm.form.MC_STYLE_H_EXT_ID;
                    url += CuttingDataService.kFilterStr2QueryParam(params.filter);

                    console.log(url);

                    CuttingDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

        vm.editRow = function (dataItem) {
            $state.go('MrkrReqH', { pGMT_MRKR_REQ_ID: dataItem.GMT_MRKR_REQ_ID })
        }

        vm.getReqList = function () {
            vm.mrkrReqListGridDataSource.read();
        }
        


    }

})();
////////// End CutMrkrReqListController Controller









// Start New Marker Modal Controller
(function () {
    'use strict';

    angular.module('multitex.cutting').controller('MarkerModalController', ['$modalInstance', '$q', '$scope', '$http', '$state', '$filter', 'config', 'stylOrdData', 'CuttingDataService', 'Dialog', MarkerModalController]);
    function MarkerModalController($modalInstance, $q, $scope, $http, $state, $filter, config, stylOrdData, CuttingDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;

        vm.mainSupportList = [{ IS_MAIN_SUPPORT: 'M', MAIN_SUPPORT_NAME: 'Main' }, { IS_MAIN_SUPPORT: 'S', MAIN_SUPPORT_NAME: 'Support' }];

        activate();

        vm.today = new Date();
        vm.dtFormat = config.appDateFormat;

        stylOrdData['GMT_MRKR_ID'] = 0;
        console.log(stylOrdData);

        vm.form = stylOrdData['MC_ORDER_H_ID'] ? stylOrdData : {};


        

        function activate() {
            var promise = [getFabricByStyleId(), getMrkrWayTypeList(), getMrkrTypeList(), getOrdSizeRatio(), getBookingDiaByProdOrdId(), getGmtPartList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }




        function getGmtPartList() {

            vm.gmtPartDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {

                        var url = '/api/common/GmtPartList';

                        console.log(url);

                        CuttingDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res)
                        });
                    }
                }
            });
        };

        vm.onCloseGmtPart = function () {

            vm.gmtPartIds = "";
            vm.form.RF_GARM_PART_LST = "";

            console.log(vm.form.RF_GARM_PART_ID_LIST);

            if (vm.form.RF_GARM_PART_ID_LIST != null) {

                if (vm.form.RF_GARM_PART_ID_LIST != []) {
                    vm.gmtPartIds = vm.form.RF_GARM_PART_ID_LIST.join(",");

                    vm.form.RF_GARM_PART_LST = vm.gmtPartIds;
                }
            }

        };

        function getFabricByStyleId() {

            vm.styleFabricOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "FABRIC_DESC",
                dataValueField: "MC_STYLE_D_FAB_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.bookingDiaDataSource.read();
                }
            };

            return vm.styleFabricDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CuttingDataService.getDataByFullUrl('/api/mrc/StyleDItemFab/SelectFabByStyleID/' + ((stylOrdData['MC_STYLE_H_ID'] > 0) ? stylOrdData['MC_STYLE_H_ID'] : 0)).then(function (res) {
                            e.success(res);

                            if (res.length == 1) {
                                vm.form.MC_STYLE_D_FAB_ID = res[0].MC_STYLE_D_FAB_ID;
                            }
                        });
                    }
                }
            });

        };

        function getBookingDiaByProdOrdId() {

            vm.bookingDiaOption = {
                optionLabel: "--- Select Dia ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.BK_FIN_DIA = item.FIN_DIA;
                    vm.form.DIA_MOU_ID = item.DIA_MOU_ID;
                },
                dataTextField: "FIN_DIA_N_DIA_TYPE",
                dataValueField: "FIN_DIA_N_DIA_TYPE"
            };

            return vm.bookingDiaDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        CuttingDataService.getDataByFullUrl('/api/knit/FabProdKnitOrder/GetFinDiaByProdOrdId?&pRF_FAB_PROD_CAT_ID=2&pMC_STYLE_H_EXT_ID=' + (stylOrdData.MC_STYLE_H_EXT_ID || 0)).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        }

        function getMrkrWayTypeList() {

            return vm.mrkrWayTypeList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CuttingDataService.LookupListData(135).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getMrkrTypeList() {

            return vm.mrkrTypeList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            CuttingDataService.LookupListData(136).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getOrdSizeRatio() {
            CuttingDataService.getDataByFullUrl("/api/Cutting/MrkrReq/GetOrdSizeCutRatio?pMC_ORDER_H_ID=" + stylOrdData['MC_ORDER_H_ID'] + '&pGMT_COLOR_ID=' + stylOrdData['GMT_COLOR_ID']).then(function (res) {

                vm.form['itemsOrdItm'] = res;

            });           
        };

        vm.save = function () {
            vm.errors = [];
            var submitData = angular.copy(vm.form);
            var ratioData = [];

            var vRatioQty = 0;
           
            angular.forEach(submitData['itemsOrdItm'], function (val, key) {
                angular.forEach(val['itemsOrdSizeRatio'], function (val1, key1) {
                    ratioData.push({ GMT_MRKR_D_ID: val1.GMT_MRKR_D_ID || 0, GMT_MRKR_ID: submitData['GMT_MRKR_ID'], MC_STYLE_D_ITEM_ID: val.MC_STYLE_D_ITEM_ID, MC_SIZE_ID: val1.MC_SIZE_ID, RATIO_QTY: val1.RATIO_QTY, SIZE_QTY: val1.SIZE_QTY, GMT_MRKR_RATIO_GRP_ID: val1.GMT_MRKR_RATIO_GRP_ID });

                    vRatioQty = vRatioQty + val1.RATIO_QTY;
                });
            });


            if (vRatioQty < 1) {
                vm.errors.push({ key: ['Sorry! Minimum a size ration should be need of a marker.'] });
            }

            if (vm.errors != undefined && vm.errors.length > 0) {               
                return;
            }
            else {
                vm.errors = undefined;
            }

          


            submitData.GMT_MRKR_D_XML = CuttingDataService.xmlStringShort(ratioData.map(function (o) {
                return o;
            }));

            
            console.log(submitData);
            //return;


            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + $scope.token },
                         url: '/api/Cutting/MrkrReq/MrkrBatchSave',
                         method: 'post',
                         data: submitData
                     }).then(function (res) {
                         //$scope.$parent.errors = undefined;
                         if (res.data.success === false) {
                             //$scope.$parent.errors = [];
                             //$scope.$parent.errors = res.data.errors;
                         }
                         else {

                             res['data'] = angular.fromJson(res.data.jsonStr);

                             if (res.data.PGMT_MRKR_ID_RTN > 0 && res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                 
                                 submitData['GMT_MRKR_ID'] = res.data.PGMT_MRKR_ID_RTN;
                                 submitData['MRKR_REF_NO_RTN'] = res.data.PMRKR_REF_NO_RTN;
                                 //submitData['REVISION_DT'] = $filter('date')($scope.REVISION_DT, "dd/MMM/yyyy hh:mm a");
                                 $modalInstance.close(submitData);
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });

            //var obj = {};
            //obj = $scope.asortDataList._data.map(function (ob) {
            //    return {
            //        DETAIL_INDEX: ob.DETAIL_INDEX, MC_SMP_REQ_D_ASORT_ID: ob.MC_SMP_REQ_D_ASORT_ID > 0 ? ob.MC_SMP_REQ_D_ASORT_ID : 0,
            //        MC_SMP_REQ_D_ID: ob.MC_SMP_REQ_D_ID, MC_ORDER_H_ID: ob.MC_ORDER_H_ID, STYLE_NO: ob.STYLE_NO, ASSORT_QTY: ob.RQD_QTY,
            //        QTY_MOU_ID: ob.MC_SMP_REQ_D_ASORT_ID > 0 ? ob.QTY_MOU_ID : $scope.QTY_MOU_ID
            //    };
            //});


        };


        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();
// End New Marker Modal Controller