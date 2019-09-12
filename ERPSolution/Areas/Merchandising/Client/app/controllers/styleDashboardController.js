(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleDashboardController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$timeout', '$state', '$stateParams', '$modal', 'MrcDataService', StyleDashboardController]);
    function StyleDashboardController(logger, config, $q, $scope, $http, exception, $filter, $timeout, $state, $stateParams, $modal, MrcDataService) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.today = new Date();
        vm.form = { MC_BYR_ACC_ID:0, BLK_FAB_REQ_NO: '' };

        
        activate();
        function activate() {
            var promise = [getBuyerAcList()];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                
                vm.showSplash = false;
            });
        };

        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 6
        };

        vm.fromDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.fromDateOpened = true;
        };

        vm.form.FROM_DT = vm.today;
        vm.toDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.toDateOpened = true;
        };
        vm.form.TO_DT = vm.today;

        function getBuyerAcList() {
           
            vm.buyerAccWiseStyleList = new kendo.data.ObservableArray([]);
            
            return vm.buyerAcList = {
                optionLabel: "--- Select Buyer A/C ---",
                filter: "startswith",
                autoBind: true,
                dataBound: function (e) {
                    var MC_BYR_ACC_ID = this.dataItem(e.item).MC_BYR_ACC_ID;
                    vm.form.MC_BYR_ACC_ID = MC_BYR_ACC_ID;

                    if (MC_BYR_ACC_ID) {
                        return MrcDataService.GetAllOthers('/api/mrc/StyleHExt/BuyerWiseStyleHExtList/' + MC_BYR_ACC_ID + '/' + null).then(function (res) {
                            vm.buyerAccWiseStyleList = res;
                            //alert($stateParams.pMC_STYLE_H_ID);
                            if ($stateParams.pMC_STYLE_H_ID) {
                                vm.form.MC_STYLE_H_ID = $stateParams.pMC_STYLE_H_ID;
                                vm.form.MC_STYLE_H_EXT_ID = $stateParams.MC_STYLE_H_EXT_ID;
                            }

                            vm.getBookingList();

                        }, function (err) {
                            console.log(err);
                        });
                    }
                    else {
                        //alert('else');
                        vm.buyerAccWiseStyleList = [];
                    }
                    
                },
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }

        vm.getBuyerAccWiseStyleList = function (e) {
            var MC_BYR_ACC_ID = e.sender.dataItem(e.item).MC_BYR_ACC_ID;
            $stateParams.BAID = MC_BYR_ACC_ID;

            if (parseInt(MC_BYR_ACC_ID) > 0 && MC_BYR_ACC_ID != '') {
                vm.buyerAccWiseStyleDataSource = new kendo.data.DataSource({
                    serverFiltering: true,
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            var url = '/StyleHExt/BuyerWiseStyleHExtList/' + MC_BYR_ACC_ID + '/' + null + '?';
                            url += MrcDataService.kFilterStr2QueryParam(params.filter);
                            url += '&pMC_STYLE_H_EXT_ID';

                            return MrcDataService.getDataByUrl(url).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            })
                        }
                    }
                });
            }
            else {
                vm.buyerAccWiseStyleDataSource = new kendo.data.DataSource({
                    data: []
                });
            }
        };


        vm.onSelectStyleExt = function (e) {
            var item = e.sender.dataItem(e.item);
            
            vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
            vm.form.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;
        };

        vm.getBookingList = function () {
            vm.mainStyleGridDataSource.read();

            //vm.fabReqDataSource = new kendo.data.DataSource({
            //    serverPaging: true,
            //    serverFiltering: true,                
            //    schema: {
            //        data: "data",
            //        total: "total"
            //    },
            //    transport: {
            //        read: function (e) {
            //            var webapi = new kendo.data.transports.webapi({});
            //            var params = webapi.parameterMap(e.data);
            //            var url = '/BulkFabBk/BulkFabBookingList/' + vm.form.MC_BYR_ACC_ID + '/' + null + '/' + vm.form.MC_STYLE_H_EXT_ID + '?';
            //            url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
            //            url += MrcDataService.kFilterStr2QueryParam(params.filter);
            //            console.log(params.filter);

            //            return MrcDataService.getDataByUrl(url).then(function (res) {
            //                angular.forEach(res.data, function (val, key) {                                
            //                    val['BLK_FAB_REQ_DT_STR'] = $filter('date')(val['BLK_FAB_REQ_DT'], vm.dtFormat);
            //                });
            //                e.success(res);
            //                console.log(res);
            //            }, function (err) {
            //                console.log(err);
            //            });
            //        }
            //    },
            //    pageSize: 10,
                
            //    group: [{ field: 'STYLE_NO' }],
            //    sort: [{ field: 'STYLE_NO', dir: 'asc' }, { field: 'BLK_FAB_REQ_DT', dir: 'desc' }]
            //});
        };



        vm.printBookingRecord = function (dataItem, pREVISION_NO) {
            //console.log(dataItem);

            vm.form.REPORT_CODE = 'RPT-2001';
            vm.form.MC_BLK_FAB_REQ_H_ID = dataItem.MC_BLK_FAB_REQ_H_ID;
            vm.form.IS_MULTI_SHIP_DT = dataItem.IS_MULTI_SHIP_DT;
            vm.form.MC_BLK_REVISION_NO = pREVISION_NO;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = vm.form;

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
        
        vm.navigateAction = function (dataItem, navigateId, pREVISION_NO) {
            if (navigateId == 1) {
                $state.go('BulkFabBkEntry.Dtl', { pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID });
            }
            else if (navigateId == 2) {
                vm.printBookingRecord(dataItem, pREVISION_NO);
            }
            else if (navigateId == 6) {
                $state.go('BudgetSheet', { pMC_BLK_FAB_REQ_H_ID: dataItem.MC_BLK_FAB_REQ_H_ID, pMC_STYLE_H_ID: dataItem.MC_STYLE_H_ID, pMC_STYL_BGT_H_ID: dataItem.MC_STYL_BGT_H_ID });
            }
            else if (navigateId == 7) {
                vm.printBudgetSheetReport(dataItem);
            }
        };
        
        vm.mainStyleGridOption = {
            height: 400,
            sortable: true,
            scrollable: true,
            //scrollable: {
            //    virtual: true,                
            //},
            pageable: true,
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
                //{ title: 'Image', template: '<img ng-click="openShowImageModal(dataItem.MC_STYLE_D_ITEM_ID)" data-ng-src="data:image/png;base64,#:data.STYL_KEY_IMG#" title="Click for Image Enlarge" alt="No Photo" style="border:1px solid black; width:45px" />', width: "60px" },                
                { field: "STYLE_NO", title: "Style#", type: "string", width: "100px", filterable: true },
                { field: "BUYER_NAME_EN", title: "Buyer", type: "string", width: "100px", filterable: true },                
                { field: "SEASON", title: "Season", type: "number", width: "100px", filterable: true },
                { field: "GARM_TYPE_NAME", title: "Nature Of Work", type: "string", width: "150px", filterable: true }
            ]
        };

        vm.mainStyleGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 10,            
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/StyleH/BuyerAcWiseStyleList?pMC_BYR_ACC_ID=' + vm.form.MC_BYR_ACC_ID;
                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += config.kFilterStr2QueryParam(params.filter);
                    console.log(params.filter);

                    return MrcDataService.getDataByUrl(url).then(function (res) {
                        e.success(res);
                        console.log(res);
                    }, function (err) {
                        console.log(err);
                    });
                }
            },
            schema: {
                //model: {
                //    fields: {
                //        SMP_REQ_DT: { type: "date" }
                //    }
                //},
                data: "data",
                total: "total"
            }
        });

        vm.detailStyleGridOption = function (hdrDataItem) {
            console.log(hdrDataItem);
            return {
                height: 250,
                dataSource: {
                    serverPaging: true,
                    serverSorting: true,
                    serverFiltering: true,
                    pageSize: 200,
                    pageable: true,
                    //scrollable: {
                    //    virtual: true,
                    //},
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);
                            var url = '/BulkFabBk/BulkFabBookingList/' + vm.form.MC_BYR_ACC_ID + '/' + hdrDataItem.MC_STYLE_H_ID + '/' + vm.form.MC_STYLE_H_EXT_ID + '?';
                            url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                            url += MrcDataService.kFilterStr2QueryParam(params.filter);
                            console.log(params.filter);

                            return MrcDataService.getDataByUrl(url).then(function (res) {
                                angular.forEach(res.data, function (val, key) {                                
                                    val['BLK_FAB_REQ_DT_STR'] = $filter('date')(val['BLK_FAB_REQ_DT'], vm.dtFormat);
                                });
                                e.success(res);
                                console.log(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    },                    
                    schema: {
                        model: {
                            fields: {
                                BLK_FAB_REQ_DT: { type: "date" }
                            }
                        },
                        data: "data",
                        total: "total"
                    },                    
                    filter: { field: "MC_SMP_REQ_H_ID", operator: "eq", value: hdrDataItem.MC_SMP_REQ_H_ID }
                },                               
                columns: [
                    { field: "STYLE_NO", title: "Style#", type: "string", hidden: true },
                    { field: "BLK_FAB_REQ_DT", title: "Booking Date", type: "date", format: "{0:dd-MMM-yyyy}" },
                    { field: "WORK_STYLE_NO_LST", title: "Work Style#", type: "string" },
                    { field: "ORDER_NO_LST", title: "Order#", type: "string" },
                    { field: "BLK_FAB_REQ_NO", title: "Booking Ref#", type: "string" },
                    { field: "REMARKS", title: "Remarks", type: "string", hidden: true },
                    {
                        title: "",
                        template: function () {
                            return '<ul kendo-menu k-orientation="menuOrientation" k-rebind="menuOrientation" k-on-select="onSelect(kendoEvent)">' +
                                    '<li><span style="color:black">Navigate</span>' +
                                        '<ul style="width:150px;">' +
                                            '<li><i class="fa fa-print"> Booking Print</i>' +
                                                '<ul style="width:150px;"><li class="k-item k-state-default k-first" ng-repeat="itm in dataItem.REVISION_LIST"><a class="k-link" style="color:black" ng-click="vm.navigateAction(dataItem,2,itm.REVISION_NO)">{{itm.REV_REASON}}</a></li></ul>' +
                                            '</li>' +                                            
                                        '</ul>' +
                                    '</li></ul>';
                        },
                        width: "120px"
                    }
                ]
            };
        };
        
        
        $scope.openShowImageModal = function (data) {
            
            var vurl = "/Merchandising/Mrc/GetFile/" + data;
            $scope.STYL_KEY_IMG = vurl;
            localStorage.setItem("VData", vurl);

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ImageShowup',
                controller: 'ShowImageModalController',
                size: '300px',
                windowClass: 'large-Modal',
                resolve: {
                    Buyer: function () {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                var vurl = "/Merchandising/Mrc/GetFile/" + data;
                $('#imgShowImage').attr('src', vurl);
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        }

        vm.printBudgetSheetReport = function (dataOri) {
            var data = {
                MC_STYL_BGT_H_ID: dataOri.MC_STYL_BGT_H_ID,
                REVISION_NO: dataOri.BGT_REVISION_NO
            };
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: 'RPT-2003' }, data);

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


    }
})();
