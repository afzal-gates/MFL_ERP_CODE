
(function () {
    'use strict';

    angular.module('multitex.garments').filter('OrdinalFilter', [OrdinalFilter]);
    function OrdinalFilter() {

        return function (number) {
            if (isNaN(number) || number < 1) {
                return number;
            } else {
                var lastDigit = number % 10;
                if (lastDigit === 1) {
                    return number + 'st'
                } else if (lastDigit === 2) {
                    return number + 'nd'
                } else if (lastDigit === 3) {
                    return number + 'rd'
                } else if (lastDigit > 3) {
                    return number + 'th'
                }
            }
        }
    }

    angular.module('multitex.garments').controller('GmtFinIronController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', '$localStorage', 'FloorData', 'GarmentsDataService', 'Dialog', '$modal', 'cur_date_server', GmtFinIronController]);
    function GmtFinIronController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, $localStorage, FloorData, GarmentsDataService, Dialog, $modal, cur_date_server) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;

        vm.MC_ORDER_H_LIST = [];

        vm.templateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p>(#: data.ORDER_NO #)</p></span>';
        vm.valueTemplateOh = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO #)</h5></span>';

        vm.today = new Date();
        
        if ($localStorage.GMT_FIN_IRN_CALENDAR_DT) {
            $scope.dt = $filter('date')($localStorage.GMT_FIN_IRN_CALENDAR_DT, vm.dtFormat);
            vm.selectedDt = $filter('date')($localStorage.GMT_FIN_IRN_CALENDAR_DT, vm.dtFormat);
        }
        else {
            $scope.dt = new Date(cur_date_server);
            vm.selectedDt = $filter('date')(cur_date_server, vm.dtFormat);
        }

        $scope.minDate = new Date(cur_date_server);        
        $scope.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.opened = true;
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        
        
        $scope.FloorList = FloorData;

        if (!$localStorage.GMT_FIN_IRN_PROD_FLR_LST) {
            var flr_list = _.map(FloorData, 'HR_PROD_FLR_ID');
            vm.selectedFlr = flr_list.join(',');
            vm.isFlrShowWithHr = true;
        }
        else {
            vm.selectedFlr = $localStorage.GMT_FIN_IRN_PROD_FLR_LST;
            vm.isFlrShowWithHr = false;

            angular.forEach($scope.FloorList, function (val, key) {
                if (vm.selectedFlr == val.HR_PROD_FLR_ID) {
                    val['IS_ACTIVE'] = true;


                } else {
                    val['IS_ACTIVE'] = false;
                }

            });
        }

        if (!$localStorage.GMT_FIN_IRN_HR_NO) {
            vm.selectedHrNo = 1;
        }
        else {
            vm.selectedHrNo = $localStorage.GMT_FIN_IRN_HR_NO;
        }


        activate();
        function activate() {
            var promise = [getGmtFinIronData(vm.selectedDt, vm.selectedFlr), getByrAccWiseStyleExtList()];
            return $q.all(promise).then(function () {
                
                vm.showSplash = false;                
            });
        }

        //vm.form = {
        //    GMT_PROD_PLN_CLNDR_ID: 0, CALENDAR_DT: vm.selectedDt, HR_PROD_FLR_ID: 0, FLOOR_DESC_EN: null, itemsGmtFinIronHdr: [
        //        { GMT_FIN_IRON_H_ID: 0, HR_PROD_FLR_ID: null, GMT_PROD_PLN_CLNDR_ID: null, HR_NO: null, MC_ORDER_H_LST: null, itemsGmtFinIronDtl: [] }
        //    ]
        //};


        vm.onDateSelect = function (dt) {
            vm.selectedDt = $filter('date')(dt, vm.dtFormat);
            //$localStorage.GMT_FIN_IRN_CALENDAR_DT = vm.selectedDt;

            getGmtFinIronData(vm.selectedDt, vm.selectedFlr);
        };

        vm.OnChangeFloor = function (itm) {
            getGmtFinIronData(vm.selectedDt, itm.HR_PROD_FLR_ID);

            vm.selectedFlr = itm.HR_PROD_FLR_ID;
            //$localStorage.GMT_FIN_IRN_PROD_FLR_LST = itm.HR_PROD_FLR_ID;

            angular.forEach($scope.FloorList, function (val, key) {
                if (itm.HR_PROD_FLR_ID == val.HR_PROD_FLR_ID) {
                    val['IS_ACTIVE'] = true;
                } else {
                    val['IS_ACTIVE'] = false;
                }
               
            });

            //$state.go('HourlyFinProductionEntry', {}, { notify: false, inherit: false });
        };

        vm.onFocus = function (hrItm) {

            var obj=_.maxBy(vm.GmtFinIronData, function (o) { return o.HR_NO; });

            if ((parseInt(obj.HR_NO) + 1) < 24) {

                hrItm.ADD_NEW_HR_NO = parseInt(obj.HR_NO) + 1;
            }
        }

        function getGmtFinIronData(pCALENDAR_DT, pHR_PROD_FLR_LST) {
            return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtFin/GetGmtFinIronData?pCALENDAR_DT=' + pCALENDAR_DT + '&pHR_PROD_FLR_LST=' + pHR_PROD_FLR_LST).then(function (res) {
                vm.GmtFinIronData = res;
            });
        }

        function getByrAccWiseStyleExtList() {
            
            vm.OrdStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetStyleExOrderList';
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += GarmentsDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData);

                        return GarmentsDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

        };
        
        vm.ordItmColorGenerate = function (hrItm) {

            var list = [];
            console.log(vm.MC_ORDER_H_LIST);

            angular.forEach(vm.MC_ORDER_H_LIST, function (val, key) {

                var vIndex = _.findIndex(hrItm.itemsOrder, function (o) {

                    console.log(o.MC_ORDER_H_ID);
                    console.log(val);

                    return o.MC_ORDER_H_ID == val;
                });

                //alert(vIndex);

                if (vIndex < 0) {
                    list.push(val);
                }
            });

            var vOrdLst = list.join(',');
            console.log(vOrdLst);

            return GarmentsDataService.getDataByFullUrl('/api/gmt/GmtFin/GetGmtFinOrderList?pMC_ORDER_H_LST=' + vOrdLst).then(function (res) {
                //hrItm.itemsOrder = res;

                angular.forEach(res, function (val, key) {
                    hrItm.itemsOrder.push(val);
                });
            });
        }

        vm.addNewFlrHr = function (hrItm) {
            var data = angular.copy(hrItm);

            if (parseInt(hrItm.ADD_NEW_HR_NO) < 24) {
                data['HR_NO'] = hrItm.ADD_NEW_HR_NO;
                data['HR_NAME'] = hrItm.ADD_NEW_HR_NO;

                hrItm.ADD_NEW_HR_NO = null;
                data.ADD_NEW_HR_NO = null;


                data['GMT_FIN_IRON_H_ID'] = 0;

                angular.forEach(data['itemsOrder'], function (val, key) {

                    angular.forEach(val['itemsOrdItmColor'], function (val1, key1) {

                        angular.forEach(val1['itemsOrdSize'], function (val2, key2) {

                            val2['GMT_FIN_IRON_D_ID'] = 0;
                            val2['IRON_QTY'] = 0;

                        });
                    });
                });

                console.log(data);
                vm.GmtFinIronData.push(data);
            }
        }

        //vm.getCardinal2Ordinal = function () {
        //    return function (number) {
        //        if (isNaN(number) || number < 1) {
        //            return number;
        //        } else {
        //            var lastDigit = number % 10;
        //            if (lastDigit === 1) {
        //                return number + 'st'
        //            } else if (lastDigit === 2) {
        //                return number + 'nd'
        //            } else if (lastDigit === 3) {
        //                return number + 'rd'
        //            } else if (lastDigit > 3) {
        //                return number + 'th'
        //            }
        //        }
        //    }
        //}
        
        vm.removeRow = function (index, removeFrom) {
            removeFrom.splice(index, 1);
        };

        vm.hideRow = function (index, hideFrom) {
            hideFrom[index].IS_HIDE = 'Y';
        };

        //vm.onSelectHrNo = function (items, $index) {
        //    angular.forEach(items, function (val, key) {
        //        if ($index != key) {
        //            val['IS_OPEN'] = false;
        //        }
        //    })
        //    items[$index]['IS_OPEN'] = !items[$index]['IS_OPEN'];
        //};


        vm.save = function () {

            $localStorage.GMT_FIN_IRN_PROD_FLR_LST = vm.selectedFlr;
            $localStorage.GMT_FIN_IRN_CALENDAR_DT = vm.selectedDt;
            $localStorage.GMT_FIN_IRN_HR_NO = vm.selectedHrNo;

            var gmtFinIronHdr = [];
            var gmtFinIronDtl = [];

            var vHdrIdx = 1;
           
            var submitData = {};

            angular.forEach(vm.GmtFinIronData, function (val, key) {
                
                var vOrdLst = "";
                
                angular.forEach(val['itemsOrder'], function (val1, key1) {

                    vOrdLst = vOrdLst + "," + val1['MC_ORDER_H_ID'];

                    angular.forEach(val1['itemsOrdItmColor'], function (val2, key2) {

                        angular.forEach(val2['itemsOrdSize'], function (val3, key3) {
                            gmtFinIronDtl.push({
                                HDR_ID: vHdrIdx, GMT_FIN_IRON_D_ID: val3.GMT_FIN_IRON_D_ID, GMT_FIN_IRON_H_ID: val3.GMT_FIN_IRON_H_ID, MC_ORDER_SIZE_ID: val3.MC_ORDER_SIZE_ID,
                                MC_SIZE_ID: val3.MC_SIZE_ID, MC_COLOR_ID: val3.MC_COLOR_ID, HR_COUNTRY_ID: val3.HR_COUNTRY_ID, MC_STYLE_D_ITEM_ID: val3.MC_STYLE_D_ITEM_ID,
                                MC_ORDER_SHIP_ID: val3.MC_ORDER_SHIP_ID, IRON_QTY: val3.IRON_QTY
                            });                            
                        });

                    });
                });

                gmtFinIronHdr.push({
                    HDR_ID: vHdrIdx, GMT_FIN_IRON_H_ID: val.GMT_FIN_IRON_H_ID, HR_PROD_FLR_ID: val.HR_PROD_FLR_ID, GMT_PROD_PLN_CLNDR_ID: val.GMT_PROD_PLN_CLNDR_ID,
                    HR_NO: val.HR_NO, MC_ORDER_H_LST: vOrdLst
                });

                vHdrIdx = vHdrIdx + 1;
            });



            submitData['GMT_FIN_IRON_H_XML'] = GarmentsDataService.xmlStringShort(gmtFinIronHdr.map(function (ob) {
                return ob;
            }));

            submitData['GMT_FIN_IRON_D_XML'] = GarmentsDataService.xmlStringShort(gmtFinIronDtl.map(function (ob) {
                return ob;
            }));

            console.log(submitData);
            //return;


            Dialog.confirm('Do you want to save?', 'Confirmation', ['Yes', 'No'])
                 .then(function () {

                     $http({
                         headers: { 'Authorization': 'Bearer ' + $scope.token },
                         url: '/api/gmt/GmtFin/BatchSaveGmtFinIron',
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
                                 $state.go('GmtFinIron', { pCALENDAR_DT: vm.selectedDt, pHR_PROD_FLR_LST: vm.selectedFlr }, { reload: true });
                             }
                             config.appToastMsg(res.data.PMSG);
                         }
                     }).catch(function (message) {
                         exception.catcher('XHR loading Failded')(message);
                     });


                 });

        };


      };
})();







