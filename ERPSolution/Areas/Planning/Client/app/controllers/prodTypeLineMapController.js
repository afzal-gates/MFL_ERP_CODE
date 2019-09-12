(function () {
    'use strict';
    angular.module('multitex.planning').controller('ProdTypeLineMapController', ['$q', 'config', 'Dialog', 'PlanningDataService', '$stateParams', '$state', '$scope', '$modal', ProdTypeLineMapController]);
    function ProdTypeLineMapController($q, config, Dialog, PlanningDataService, $stateParams, $state, $scope, $modal) {
        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.datas = [];
        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;                
            });
        }



        vm.onChangeProdType = function (dataItem) {
            $state.go('ProdTypeLineMap', { pGMT_PRODUCT_TYP_ID: dataItem.GMT_PRODUCT_TYP_ID });
            getDataFloorLineData(dataItem.GMT_PRODUCT_TYP_ID);
        };


        function getDataFloorLineData(pGMT_PRODUCT_TYP_ID) {
            return PlanningDataService.getDataByFullUrl('/api/hr/getFloorByProdCategory?pGMT_PRODUCT_TYP_ID=' + pGMT_PRODUCT_TYP_ID).then(function (res) {
                vm.datas = res;
            });
        }

        vm.onChangeSelectAll = function (data, IS_CHECKED) {
            angular.forEach(data.items, function (val, key) {
                val['IS_ACTIVE'] = IS_CHECKED;
            });
        };

        vm.onDataBoundProdType = function (e) {
            var listView = e.sender;
            var firstItem = listView.element.children().first();
            if ($stateParams.pGMT_PRODUCT_TYP_ID) {
                var uid = _.find(listView.dataSource.data(), function (o) { return o.GMT_PRODUCT_TYP_ID == parseInt($stateParams.pGMT_PRODUCT_TYP_ID) }).uid;
                var listViewItem = listView.element.children("[data-uid='" + uid + "']");
                listView.select(listViewItem);
            } else {
                listView.select(firstItem);
            }    

        };


        vm.prodTypeDs = new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    PlanningDataService.getDataByFullUrl('/api/pln/ProdCapcti/GetProdTypeList').then(function (res) {
                        e.success(res);
                    });
                }
            }
        });

        vm.onCancel = function () {
            getDataFloorLineData($stateParams.pGMT_PRODUCT_TYP_ID);
        };


        vm.submitData = function (datas) {
            var data2Save = [];

            angular.forEach(datas, function (val, key) {
                angular.forEach(val.items, function (v, k) {
                    if (v.IS_ACTIVE == 'Y') {
                        data2Save.push({
                            HR_PROD_LINE_ID: v.HR_PROD_LINE_ID
                        });
                    }
                });
            });

            if (data2Save.length == 0) {
                return;
            }

            PlanningDataService.saveDataByUrl({
                GMT_PRODUCT_TYP_ID : $stateParams.pGMT_PRODUCT_TYP_ID,
                XML: config.xmlStringShort(data2Save),
                Option: 1000

            }, '/ProdCapcti/gmtProdTypeLineMapSave').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {

                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });

        };

        vm.openGmtItemModal = function (pHR_PROD_FLR_ID, pHR_PROD_LINE_ID, pBLDNG_CODE, pFLOOR_DESC_EN, pLINE_CODE) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/Planning/Pln/_LineGmtItemMapModal',
                controller: 'GmtItemTypeLineMapController',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    params: function () {
                        return {
                            HR_PROD_FLR_ID: pHR_PROD_FLR_ID,HR_PROD_LINE_ID: pHR_PROD_LINE_ID, BLDNG_CODE: pBLDNG_CODE,FLOOR_DESC_EN: pFLOOR_DESC_EN,LINE_CODE: pLINE_CODE
                        }
                    }
                }
            });

            modalInstance.result.then(function (data) {
                getDataFloorLineData($stateParams.pGMT_PRODUCT_TYP_ID);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }



    }

})();


(function () {
    'use strict';
    angular.module('multitex.planning').controller('GmtItemTypeLineMapController', ['$q', 'config', 'PlanningDataService', '$scope', '$modalInstance', 'params', GmtItemTypeLineMapController]);
    function GmtItemTypeLineMapController($q, config, PlanningDataService, $scope, $modalInstance, params) {
        $scope.params = params;
        getLineCategoryData(params.HR_PROD_LINE_ID);

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.onCancel = function () {
            getLineCategoryData(params.HR_PROD_LINE_ID);
        };

        $scope.onChangeSelectAll = function (data, IS_CHECKED) {
            angular.forEach(data.items, function (val, key) {
                val['IS_ACTIVE'] = IS_CHECKED;
            });
        };


        function getLineCategoryData(pHR_PROD_LINE_ID) {
            return PlanningDataService.getDataByFullUrl('/api/mrc/StyleItem/InvItemByLine?pPARENT_ID=8&pHR_PROD_LINE_ID=' + pHR_PROD_LINE_ID).then(function (res) {
                $scope.datas = res;
            });
           
        };

        $scope.submitData = function (datas) {
            var data2Save = [];
            angular.forEach(datas, function (val, key) {
                angular.forEach(val.items, function (v, k) {
                    if (v.IS_ACTIVE == 'Y') {
                        data2Save.push({
                            INV_ITEM_CAT_ID: v.INV_ITEM_CAT_ID
                        });
                    }
                });
            });

            if (data2Save.length == 0) {
                return;
            }

            PlanningDataService.saveDataByUrl({
                HR_PROD_FLR_ID: params.HR_PROD_FLR_ID,
                HR_PROD_LINE_ID: params.HR_PROD_LINE_ID,
                XML: config.xmlStringShort(data2Save),
                Option: 1001

            }, '/ProdCapcti/gmtProdTypeLineMapSave').then(function (res) {
                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        $modalInstance.close(params);
                    }
                    config.appToastMsg(res.data.PMSG);
                }
            });

        };



    }

})();

