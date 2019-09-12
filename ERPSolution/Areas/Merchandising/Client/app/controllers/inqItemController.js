(function () {
    'use strict';
    angular.module('multitex.mrc').controller('InqItemController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', '$modal', InqItemController]);
    function InqItemController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData, $modal) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'StyleItem';
        var key = 'MC_STYLE_ITEM_ID';
        vm.Title = $state.current.Title || '';
        vm.hideForm = true;

        vm.parentItemCategory = 8;
        vm.setItemCategory = 31;

        vm.form = formData[key] ? formData : { LK_ITEM_STATUS_ID: 258 };
        vm.Style = $scope.$parent.StyleData;
        vm.Inquiry = $scope.$parent.$parent.InquiryData;

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getGmtItemGroupList(), getGmtDeptList(), getSlvTypeList(), getNeckTypeList(), getRmgItemStatusList(), getAllItemList(), getPartList(), getFabGroupList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        function getGmtItemGroupList() {
            return vm.GmtItemGroupList = {
                optionLabel: "--Item Group--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.invItemListByParent(vm.parentItemCategory).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select : function(e){
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.INV_ITEM_CAT_ID;            
                    if (vParentID) {
                        if (vParentID == vm.setItemCategory) {
                            $("#INV_ITEM_CAT_ID").kendoDropDownList({
                                autoBind: true,
                                optionLabel: "-- Select Section --",
                                dataTextField: "ITEM_CAT_NAME_EN",
                                dataValueField: "INV_ITEM_CAT_ID",
                                dataSource: {
                                    transport: {
                                        read: function (e) {
                                            e.success([{ ITEM_CAT_NAME_EN: 'Set Item', INV_ITEM_CAT_ID: 31 }]);
                                        }
                                    }
                                },

                                index:1,
                                filter: "startswith"
                                
                            });

                        } else {
                            MrcDataService.invItemListByParent(vParentID).then(function (res) {
                                $("#INV_ITEM_CAT_ID").kendoDropDownList({
                                    autoBind: true,
                                    optionLabel: "-- Select Section --",
                                    dataTextField: "ITEM_CAT_NAME_EN",
                                    dataValueField: "INV_ITEM_CAT_ID",
                                    dataSource: res,
                                    filter: "startswith"
                                });

                            });
                        }


                    }
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        }

        function getAllItemList() {
            return MrcDataService.getItemByStyleID($stateParams.MC_STYLE_ID).then(function (res) {
                vm.allItemList = res;
            }, function (err) {
                console.log(err);
            });
        }


        function getGmtDeptList() {
            return vm.GmtDeptList = {
                optionLabel: "----",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(48).then(function (res) {
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


        function getSlvTypeList() {
            return vm.SlvTypeList = {
                optionLabel: "----",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(49).then(function (res) {
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

        function getPartList() {
            return vm.PartList = {
                optionLabel: "--Part--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(44).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                index:1,
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LK_DATA_NAME_EN"
            };
        }

        function getFabGroupList() {
            return MrcDataService.LookupListData(45).then(function (res) {
                 vm.getFabGroupList = res;
                }, function (err) {
                    console.log(err);
                });
        }



        function getNeckTypeList() {
            return vm.NeckTypeList = {
                optionLabel: "----",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(50).then(function (res) {
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

        function getRmgItemStatusList() {
            return vm.RmgItemStatusList = {
                optionLabel: "----",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(51).then(function (res) {
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



        vm.addItemTo = function (data) {
            vm.hideForm = false;
            vm.form['PARENT_ID'] = data.MC_STYLE_ITEM_ID;
            vm.form['COMBO_NO'] = 1;
        }

        vm.editItem = function (data) {
            vm.hideForm = false;
            vm.form = data;
        }

        

        vm.cancel = function () {
            vm.hideForm = true;
        }


        vm.addNew = function () {
            vm.form['LK_ITEM_STATUS_ID'] = 258 ;
            vm.hideForm = false;
        }

        vm.copyItem = function (data, copyTo, topBottom) {
            var copiedData = angular.copy(data);
               copiedData['copied'] = true;
               copiedData['MC_STYL_FAB_COST_ID'] = -1;
            var copyToData = angular.copy(copyTo);
            if (topBottom) {
                copyTo.items.push(copiedData);
            } else {
                copyTo.items.push(copyToData);
                copyTo.items.push(copiedData);
            }
        }

        vm.addCombo = function (data, copyTo) {
            var copiedData = angular.copy(data);
            copiedData['copied'] = true;
            copiedData['MC_STYLE_ITEM_ID'] = -1;
            copiedData['COMBO_NO'] = 2;

            if (copiedData.items.length>0) {
                angular.forEach(copiedData.items, function (val, key) {
                    val['copied'] = true;
                    val['MC_STYL_FAB_COST_ID'] = -1;
                });
            }
            copyTo.push(copiedData);
        }


        vm.addOption = function (data, copyTo) {
            var copiedData = angular.copy(data);
            copiedData['MC_STYLE_ITEM_ID'] = -1;
            copiedData['OPTION_NO'] = "2";
            copiedData['SEGMENT_FLAG'] = 'I';

            angular.forEach(copiedData.items, function (val, key) {
                val['copied'] = true;
                val['MC_STYLE_ITEM_ID'] = -1;
                if (val.items.length > 0) {
                    angular.forEach(val.items, function (val2, key2) {
                        val2['copied'] = true;
                        val2['MC_STYL_FAB_COST_ID'] = -1;
                    });
                }
            });
            copyTo.push(copiedData);
        }

        vm.addComboInTopBottomSet = function (item, copyTo) {
            var copiedData = angular.copy(item);
            copiedData['MC_STYLE_ITEM_ID'] = -1;
            copiedData['COMBO_NO'] = 2;
            copiedData['SEGMENT_FLAG'] = 'C';

            angular.forEach(copiedData.items, function (val, key) {
                val['copied'] = true;
                val['MC_STYLE_ITEM_ID'] = -1;
                if (val.items.length > 0) {
                    angular.forEach(val.items, function (val2, key2) {
                        val2['copied'] = true;
                        val2['MC_STYL_FAB_COST_ID'] = -1;
                    });
                }
            });
            copyTo.push(copiedData); 
        }



        vm.remove = function (index, removeFrom, master) {
            if (master) {
                if (removeFrom[index].MC_STYLE_ITEM_ID > 0) {
                    removeFrom[index]['REMOVE'] = -2;
                        angular.forEach(removeFrom[index].items, function (val, key) {
                            val['REMOVE'] = -2;
                        })
                } else {
                    removeFrom.splice(index, 1);

                }

            } else {

                if (removeFrom.items[index].MC_STYL_FAB_COST_ID > 0) {
                    removeFrom.items[index]['REMOVE'] = -2;
                } else {
                    removeFrom.items.splice(index,1);
                }
                
            }

        }


        vm.saveDataForNonComboSetItem = function (data, token, user_id) {
            angular.forEach(data, function (val,key) {
                val['CREATED_BY'] = user_id;
                val['LAST_UPDATED_BY'] = user_id;
                val['SEGMENT_FLAG'] = 'I';
                angular.forEach(val.items, function (val1, key1) {
                    val1['SEGMENT_FLAG'] = 'I';
                });
            });
            return MrcDataService.saveStyleItemFabData(data, token).then(function (res) {
                logger.success("Successfuly Updated");
                $state.go($state.current, $stateParams.current, { reload: true });
            }, function (err) {
                console.log(err);
            });
        }


        vm.saveDataForComboSetItem = function (data, token, user_id) {
            angular.forEach(data, function (val, key) {
                val['CREATED_BY'] = user_id;
                val['LAST_UPDATED_BY'] = user_id;
                val['SEGMENT_FLAG'] = 'C';
                angular.forEach(val.items, function (val1, key1) {
                    val1['SEGMENT_FLAG'] = 'C';
                });
            });
            return MrcDataService.saveStyleItemFabData(data, token).then(function (res) {
                logger.success("Successfuly Updated");
                $state.go($state.current, $stateParams.current, { reload: true });
            }, function (err) {
                console.log(err);
            });
        }

        vm.saveDataForComboOtherItem = function (data, token, user_id) {

            angular.forEach(data, function (val, key) {
                val['CREATED_BY'] = user_id;
                val['LAST_UPDATED_BY'] = user_id;
                val['SEGMENT_FLAG'] = 'I';
                angular.forEach(val.items, function (val1, key1) {
                    val1['SEGMENT_FLAG'] = 'C';
                });
            });
            return MrcDataService.saveStyleItemFabData(data, token).then(function (res) {
                logger.success("Successfuly Updated");
                $state.go($state.current, $stateParams.current, { reload: true });
            }, function (err) {
                console.log(err);
            });
        }

        MrcDataService.getDataByUrl('/StyleItem/ItemByStyleID2Level/' + $stateParams.MC_STYLE_ID).then(function (res) {
            vm.itemList2L = res;
        }, function (err) {
            console.log(err);
        });



        
        MrcDataService.getDataByUrl('/StyleItem/ItemByStyleIdForSetCombo/' + $stateParams.MC_STYLE_ID).then(function (res) {
            console.log(res);
            vm.ForSetCombo = res;
        }, function (err) {
            console.log(err);
        });



        vm.saveOtherNonComboData = function (data, token, user_id) {
            angular.forEach(data, function (val, key) {
                val['CREATED_BY'] = user_id;
                val['LAST_UPDATED_BY'] = user_id;
                val['SEGMENT_FLAG'] = 'I';
            });

           return MrcDataService.saveDataByUrl(data, '/StyleItem/saveStyleItemFabData2L', token).then(function (res) {
               logger.success("Successfuly Updated");
               $state.go($state.current, $stateParams.current, { reload: true });
            }, function (err) {
                console.log(err);
            });
        }
        



        vm.submitData = function (data, token, USER_ID) {

            data['MC_STYLE_ID'] = $stateParams.MC_STYLE_ID;
            //data['ITEM_GRP_ID'] = $scope.$parent.StyleData.ITEM_GRP_ID;
            data['CREATED_BY'] = USER_ID;
            data['LAST_UPDATED_BY'] = USER_ID;

            if (angular.isDefined(data[key]) && data[key] > 0) {

                return MrcDataService.updateData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        $state.go($state.current, $stateParams.current, { reload: true });
                        vm.hideForm = true;
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {

                return MrcDataService.saveData(data, ctrl, token).then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);

                        $state.go($state.current, $stateParams.current, {reload:true});

                        //if (res.data.V_MC_COLOR_ID != 0) {
                        //    $state.go($state.current, { MC_STYLE_ITEM_ID: res.data.V_MC_STYLE_ITEM_ID });
                        //}
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }


        vm.openStyleItemCostHead = function (data,type) {

            //1=Set with Combo
            //2=Set with No combo,
            //3=Other with Combo
            //4=Other with No Combo


            if (type == 1) {
                data['MC_STYLE_ITEM_ID'] = data.PARENT_ID;
                data['pOption'] = 3005;
            } else if (type == 2){
                data['pOption'] = 3007;
            } else if (type == 3) {
                data['pOption'] = 3008;
            } else if (type == 4) {
                data['pOption'] = 3009;
            }




            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ItemCostHead.html',
                controller: 'ItemCostHeadController',
                controllerAs:'vm',
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    Inquiry: function () {
                        return vm.Inquiry;
                    },
                    item: function () {
                        return data; 
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }



        function findWithAttr(array, attr, value) {
            for (var i = 0; i < array.length; i += 1) {
                if (array[i][attr] === value) {
                    return i;
                }
            }
        }

        vm.lsitView=function(){
            $state.go('Inquiry.Style.ItemList', $stateParams.current);
        }
    }

})();