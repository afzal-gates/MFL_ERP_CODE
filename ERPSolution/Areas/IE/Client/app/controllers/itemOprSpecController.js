(function () {
    'use strict';
    angular.module('multitex.ie').controller('ItemOprSpecController', ['$q', 'config', 'IeDataService', '$stateParams', '$state', '$scope', 'StyleItemListData', ItemOprSpecController]);
    function ItemOprSpecController($q, config, IeDataService, $stateParams, $state, $scope, StyleItemListData) {

        var vm = this;
        $scope.showSplash = false;
        vm.Title = $state.current.Title || '';
        var vMcStyleDItemId;

        vm.style = { STYLE_NO: '', STYLE_DESC: '' };

        console.log(StyleItemListData);
        if (StyleItemListData) {
            
            vm.style = { STYLE_NO: StyleItemListData[0].STYLE_NO, STYLE_DESC: StyleItemListData[0].STYLE_DESC };

            vMcStyleDItemId = ($stateParams.pMC_STYLE_D_ITEM_ID||StyleItemListData[0].MC_STYLE_D_ITEM_ID);
            vm.tabList = StyleItemListData.map(function (o) {
                if (o.MC_STYLE_D_ITEM_ID == parseInt(vMcStyleDItemId)) {
                    o['IS_TAB_ACT'] = true;
                } else {
                    o['IS_TAB_ACT'] = false;
                }
                return o;
            });
        }


        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        

        vm.onSelect = function (tab) {
            $scope.styleDItemData = tab;

            vMcStyleDItemId = tab.MC_STYLE_D_ITEM_ID;
            $state.go('GmtItemOprSpec.itmDtl', { pMC_BYR_ACC_ID: $stateParams.pMC_BYR_ACC_ID, pMC_STYLE_H_ID: $stateParams.pMC_STYLE_H_ID, pMC_STYLE_D_ITEM_ID: tab.MC_STYLE_D_ITEM_ID, pINV_ITEM_CAT_ID: tab.INV_ITEM_CAT_ID }, { reload: 'GmtItemOprSpec.itmDtl' });
        }
        


    }

})();





//========================= Start Item Operation Spec Detail ====================
(function () {
    'use strict';
    angular.module('multitex.ie').controller('ItemOprSpecDController', ['$q', 'config', 'IeDataService', '$stateParams', '$state', '$scope', '$timeout', '$modal', 'Dialog', ItemOprSpecDController]);
    function ItemOprSpecDController($q, config, IeDataService, $stateParams, $state, $scope, $timeout, $modal, Dialog) {

        var vm = this;
        $scope.showSplash = false;
        vm.Title = $state.current.Title || '';
        
        vm.form = $scope.$parent.styleDItemData['MC_STYLE_D_ITEM_ID'] ? $scope.$parent.styleDItemData : {
            MC_STYLE_D_ITEM_ID: $stateParams.pMC_STYLE_D_ITEM_ID, RF_FAB_CLASS_ID: null
        };
        

        activate();

        vm.showSplash = true;
        function activate() {
            var promise = [getFabClass(), getCatItmWiseItmOprMapList(), getNatureOfWorkList()];
            return $q.all(promise).then(function () {

                //$timeout(function () {
                //    vm.form.RF_FAB_CLASS_ID = $scope.$parent.styleDItemData['RF_FAB_CLASS_ID'];
                //}, 5000);

                vm.showSplash = false;
            });
        }

        
        function getNatureOfWorkList() {
            return vm.NatureOfWorkList = {
                optionLabel: "-- Nature of Work--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            IeDataService.getDataByFullUrl('/api/common/LookupListData/37').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getFabClass() {

            vm.fabClassOption = {
                optionLabel: "--- Select ---",
                filter: "contains",
                autoBind: true,
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);                   
                },
                //dataBound: function (e) {
                    
                //    if ($scope.$parent.styleDItemData['RF_FAB_CLASS_ID']) {
                //        alert('x');
                //        vm.form.RF_FAB_CLASS_ID = $scope.$parent.styleDItemData['RF_FAB_CLASS_ID'];
                //    }
                //},
                dataTextField: "FAB_CLASS_NAME_EN",
                dataValueField: "RF_FAB_CLASS_ID"
            };
                       
            return vm.fabClassDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        return IeDataService.getDataByFullUrl('/api/common/GmtFabClassList').then(function (res) {
                            e.success(res);
                        });
                    }
                }
            });            
        }

        function getCatItmWiseItmOprMapList() {
            vm.catWiseItmPart = [];
          
            return vm.catWiseItmOprMapGridDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        //alert('x');
                        return IeDataService.getDataByFullUrl('/api/ie/ItmOprSpec/GetCatItmWiseItmOprMapList?pINV_ITEM_CAT_ID=' + ($stateParams.pINV_ITEM_CAT_ID || 0) + '&pMC_STYLE_D_ITEM_ID=' + ($stateParams.pMC_STYLE_D_ITEM_ID || 0)).then(function (res) {
                            e.success(res);

                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
            
        }

        vm.onChangeItmPartOpr= function(oprList, itmOprSpecId){
            console.log(oprList);
            console.log(itmOprSpecId);
            
            angular.forEach(oprList, function (val, key) {
                if (val['GMT_PART_OPR_SPEC_ID'] == itmOprSpecId) {
                    val['IS_ACTIVE'] = 'Y';
                }
                else {
                    val['IS_ACTIVE'] = 'N';
                }
            });
        };

        vm.save = function () {

            var submitData = angular.copy(vm.form);
            var catWiseItmPartData = [];

            angular.forEach(vm.catWiseItmOprMapGridDataSource.data(), function (val, key) {
                angular.forEach(val['itemsPartOprList'], function (val1, key1) {
                    catWiseItmPartData.push(val1);
                });
            });

            submitData['GMT_MAP_ITM_OPR_XML'] = IeDataService.xmlStringShort(catWiseItmPartData.map(function (ob) {
                //console.log(ob);
                return ob;
            }));

            console.log(submitData);
            //console.log(vm.catWiseItmOprMapGridDataSource.data());
            //return;

            Dialog.confirm('Do you want save?', 'Confirmation', ['Yes', 'No'])
                .then(function () {

                    console.log(submitData);
                    $scope.showSplash = true;
                    return IeDataService.saveDataByFullUrl(submitData, '/api/ie/ItmOprSpec/BatchSave').then(function (res) {
                        console.log(res);

                        vm.errors = undefined;
                        if (res.success === false) {
                            vm.errors = [];
                            vm.errors = res.errors;
                        }
                        else {

                            res['data'] = angular.fromJson(res.jsonStr);

                            if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                                $scope.showSplash = false;
                                vm.catWiseItmOprMapGridDataSource.read();
                            }
                            config.appToastMsg(res.data.PMSG);
                        }
                    }).catch(function (message) {
                        exception.catcher('XHR loading Failded')(message);
                    });
                });
        };

        vm.cancel = function () {
            $state.go('GmtItemOprSpec.itmDtl', { pMC_BYR_ACC_ID: $stateParams.pMC_BYR_ACC_ID, pMC_STYLE_H_ID: $stateParams.pMC_STYLE_H_ID, pMC_STYLE_D_ITEM_ID: $stateParams.MC_STYLE_D_ITEM_ID, pINV_ITEM_CAT_ID: $stateParams.INV_ITEM_CAT_ID }, { reload: 'GmtItemOprSpec.itmDtl' });
        }

        vm.backToList = function () {
            $state.go('GmtStylItmList.grid', { pMC_BYR_ACC_ID: ($stateParams.pMC_BYR_ACC_ID||-1) });
        }

        vm.processSpecModal = function (dataItem) {
            console.log(dataItem);

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ProcessSpecModal.html',
                controller: 'PartProcessSpecController',
                controllerAs: 'vm',
                size: 'lg',
                scope: $scope,
                windowClass: 'large-Modal',
                resolve: {
                    GmtPartID: function () {
                        return dataItem.RF_GARM_PART_ID;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                //console.log('received');
                console.log(data);
                
                vm.catWiseItmOprMapGridDataSource.read();               

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };
        

    }

})();