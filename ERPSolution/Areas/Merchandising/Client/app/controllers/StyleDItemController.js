(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleDItemController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', '$modal', '$http', '$timeout', 'Dialog', StyleDItemController]);
    function StyleDItemController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData, $modal, $http, $timeout, Dialog) {

        var vm = this;
        vm.errors = null;
        vm.ITEM_LIST = null;
        vm.i = 0;
        vm.j = 0;
        var ctrl = 'StyleDItem';
        var key = 'MC_STYLE_D_ITEM_ID';
        vm.Title = $state.current.Title || '';
        vm.allItemType = [];

        vm.disableNeckSlv = false;


        vm.StyleData = $scope.$parent.StyleData;

        vm.item = $scope.$parent.StyleData;
        vm.ItemList = vm.item.ITEM_LIST.split(',');
        vm.HAS_SET = ($scope.$parent.StyleData.HAS_SET) || 'N';
        vm.HAS_COMBO = ($scope.$parent.StyleData.HAS_COMBO) || 'N';




        vm.params = $stateParams;

        vm.division = null;
        vm.NeckType = null;
        vm.SlvType = null;
        vm.ItemType = null;


        vm.parentItemCategory = 8;
        vm.setItemCategory = 31;
        vm.knitBottom = 30;
        vm.WovenBottom = 33;



        vm.form = formData[key] ? formData : { LK_ITEM_STATUS_ID: 258, IS_SOLID : 'Y' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getAllItemCategoryList(), getGmtItemGroupList(), getGmtDeptList(), getSlvTypeList(), getNeckTypeList(), getNatureOfWorkList(), getRmgItemStatusList(), getParentItemsList(), getPrintTypelist(),
                          getEmbrTypelist(), getAopTypelist(), getGmtWashTypelist(), getYDTypelist(), getItemList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        if ($stateParams.PARENT_ID) {
            vm.form['PARENT_ID'] = $stateParams.PARENT_ID;
        }

        if (formData[key]) {
            if (formData.LK_PRN_TYPE_ID) {
                vm.form['LK_PRN_TYPE_ID'] = MrcDataService.parseXmlString(formData.LK_PRN_TYPE_ID);
            }
        }

        if (formData[key]) {
            if (formData.LK_EMB_TYPE_ID) {
                vm.form['LK_EMB_TYPE_ID'] = MrcDataService.parseXmlString(formData.LK_EMB_TYPE_ID);
            }
        }

        if (formData[key]) {
            if (formData.LK_GMT_WASH_TYPE_ID) {
                vm.form['LK_GMT_WASH_TYPE_ID'] = MrcDataService.parseXmlString(formData.LK_GMT_WASH_TYPE_ID);
            }
        }

        if (formData[key]) {
            if (formData.LK_AOP_TYPE_ID) {
                vm.form['LK_AOP_TYPE_ID'] = MrcDataService.parseXmlString(formData.LK_AOP_TYPE_ID);
            }
        }

        vm.removeKeyImage = function () {
            vm.form['STYL_KEY_IMG'] = null;
            angular.element(document.getElementById('STYL_KEY_IMG_FILE')).val(null);
        }


        vm.removeAltImage = function () {
            vm.form['STYL_ALT_IMG'] = null;
            angular.element(document.getElementById('STYL_ALT_IMG_FILE')).val(null);

        }

        function getPrintTypelist() {
            return MrcDataService.LookupListData(66).then(function (res) {
                var data = [];
                angular.forEach(res, function (val, key) {
                    data.push({ LOOKUP_DATA_ID: val.LOOKUP_DATA_ID, LK_DATA_NAME_EN: val.LK_DATA_NAME_EN })
                });
                vm.PrintTypelist = data;
            }, function (err) {
                console.log(err);
            });
        };



        function getEmbrTypelist() {
            return MrcDataService.LookupListData(69).then(function (res) {
                var data = [];
                angular.forEach(res, function (val, key) {
                    data.push({ LOOKUP_DATA_ID: val.LOOKUP_DATA_ID, LK_DATA_NAME_EN: val.LK_DATA_NAME_EN })
                });
                vm.EmbrTypelist = data;
            }, function (err) {
                console.log(err);
            });
        };



        function getAopTypelist() {
            return MrcDataService.LookupListData(70).then(function (res) {
                var data = [];
                angular.forEach(res, function (val, key) {
                    data.push({ LOOKUP_DATA_ID: val.LOOKUP_DATA_ID, LK_DATA_NAME_EN: val.LK_DATA_NAME_EN })
                });
                vm.AopTypelist = data;
            }, function (err) {
                console.log(err);
            });
        };



        function getGmtWashTypelist() {
            return MrcDataService.LookupListData(57).then(function (res) {
                var data = [];
                angular.forEach(res, function (val, key) {
                    data.push({ LOOKUP_DATA_ID: val.LOOKUP_DATA_ID, LK_DATA_NAME_EN: val.LK_DATA_NAME_EN })
                });
                vm.GmtWashTypelist = data;
            }, function (err) {
                console.log(err);
            });
        };




        function getYDTypelist() {
            return vm.YDTypelist = {
                optionLabel: "-- Y/D Type--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(71).then(function (res) {
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
        };





        vm.next = function () {
            var item = $('#PARENT_ID_ITEM').data("kendoDropDownList").dataItem($('#PARENT_ID_ITEM').data("kendoDropDownList").select());
            var ItemList = item.ITEM_LIST.split(',')
            if (vm.i < ItemList.length) {
                var params = angular.copy($stateParams);
                params['MC_STYLE_D_ITEM_ID'] = parseInt(ItemList[vm.i]);

                if ($state.current.LK_STYL_DEV_ID==265) {
                    $state.go('StyleHDev.ItemDev', params);
                } else {
                    $state.go('StyleH.Item', params);
                }

                vm.i = vm.i + 1;
            } else {
                vm.i = 0;
            }
        };


        vm.nextNonSet = function () {
            if (vm.i < vm.ItemList.length) {
                var params = angular.copy($stateParams);
                params['MC_STYLE_D_ITEM_ID'] = parseInt(vm.ItemList[vm.i]);

                if ($state.current.LK_STYL_DEV_ID == 265) {
                    $state.go('StyleHDev.ItemDev', params);
                } else {
                    $state.go('StyleH.Item', params);
                }

                vm.i = vm.i + 1;
            } else {
                vm.i = 0;
            }
        };


        vm.previous = function () {
            var item = $('#PARENT_ID_ITEM').data("kendoDropDownList").dataItem($('#PARENT_ID_ITEM').data("kendoDropDownList").select());
            var ItemList = item.ITEM_LIST.split(',');
            vm.j = ItemList.length - 1;

            if (vm.j > -1) {
                var params = angular.copy($stateParams);
                params['MC_STYLE_D_ITEM_ID'] = parseInt(ItemList[vm.j]);

                if ($state.current.LK_STYL_DEV_ID == 265) {
                    $state.go('StyleHDev.ItemDev', params);
                } else {
                    $state.go('StyleH.Item', params);
                }

                vm.j = vm.j - 1;
            } else {
                vm.j = 0;
            }
        };





        vm.previousNonSet = function () {

            vm.j = vm.ItemList.length - 1;

            if (vm.j > -1) {
                var params = angular.copy($stateParams);
                params['MC_STYLE_D_ITEM_ID'] = parseInt(vm.ItemList[vm.j]);

                if ($state.current.LK_STYL_DEV_ID == 265) {
                    $state.go('StyleHDev.ItemDev', params);
                } else {
                    $state.go('StyleH.Item', params);
                }

                vm.j = vm.j - 1;
            } else {
                vm.j = 0;
            }
        };

        function getGmtItemGroupList() {
            return vm.GmtItemGroupList = {
                optionLabel: "--Group--",
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
                select: function (e) {
                    var dataItem = this.dataItem(e.item);
                    var vParentID = dataItem.INV_ITEM_CAT_ID;

                    if (vParentID == vm.WovenBottom || vParentID == vm.knitBottom) {
                        vm.disableNeckSlv = true;
                        vm.form['LK_NECK_TYPE_ID'] = null;
                        vm.form['LK_SLV_TYPE_ID'] = null;


                    } else {
                        vm.disableNeckSlv = false;


                    }


                    if (vParentID) {
                        MrcDataService.invItemListByParent(vParentID).then(function (res) {
                            $("#INV_ITEM_CAT_ID").kendoDropDownList({
                                autoBind: true,
                                optionLabel: "-- Item Type --",
                                dataTextField: "ITEM_CAT_NAME_EN",
                                dataValueField: "INV_ITEM_CAT_ID",
                                dataSource: res,
                                select: function (e) {
                                    if (this.dataItem(e.item).INV_ITEM_CAT_ID) {
                                        vm.ItemType = this.dataItem(e.item).ITEM_CAT_NAME_EN;

                                    } else {
                                        delete vm['ItemType'];
                                    }
                                },
                                filter: "startswith"
                            });

                        });
                    }
                },
                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        }

        function getAllItemCategoryList() {
            return vm.AllItemCategoryList = {
                optionLabel: "-- Item Type--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByUrl('/StyleItem/InvItemByParent/0').then(function (res) {
                                vm.allItemType = angular.copy(res);
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var dataItem = this.dataItem(e.item);
                    vm.form['ITEM_GRP_ID'] = dataItem.PARENT_ID;
                },

                dataTextField: "ITEM_CAT_NAME_EN",
                dataValueField: "INV_ITEM_CAT_ID"
            };
        }



        $scope.$watchGroup(['vm.division', 'vm.NeckType', 'vm.SlvType', 'vm.ItemType', 'vm.form.MC_STYLE_D_ITEM_ID', 'vm.form.MODEL_NO'], function (newVal, oldVal) {

            if (angular.isUndefined(newVal[4])) {
                var a = newVal[0] || '';
                var b = newVal[1] || '';
                var c = newVal[2] || '';
                var d = newVal[3] || '';
                var e = newVal[5] || '';

                if (e == '') {
                    vm.form['ITEM_SNAME'] = a + ' ' + d;
                } else {
                    vm.form['ITEM_SNAME'] = e + ' - ' + a + ' ' + d;
                }


                vm.form['ITEM_NAME_EN'] = a + ' ' + b + ' ' + c + ' ' + d;
            }
        });



        function getNatureOfWorkList() {
            return vm.NatureOfWorkList = {
                optionLabel: "-- Nature of Work--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(37).then(function (res) {
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

        function getParentItemsList() {
            return vm.ParentItemsList = {
                optionLabel: "-- Set Item--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByUrl('/StyleDItem/ParentItemList/' + $stateParams.MC_STYLE_H_ID).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var item = this.dataItem(e.item);
                    if (item.MC_STYLE_D_ITEM_ID) {
                        vm.ItemList = item.ITEM_LIST.split(',');
                        vm.form['LK_ITEM_STATUS_ID'] = 258;
                        $state.go($state.current.name, { MC_STYLE_H_ID: $stateParams.MC_STYLE_H_ID, MC_STYLE_D_ITEM_ID: 0, PARENT_ID: item.MC_STYLE_D_ITEM_ID });

                    } else {
                        vm.ITEM_LIST = null;
                    }
                },
                dataTextField: "ITEM_NAME_EN",
                dataValueField: "MC_STYLE_D_ITEM_ID"
            };
        }
        function getGmtDeptList() {
            return vm.GmtDeptList = {
                optionLabel: "--Division/Size Class--",
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
                select: function (e) {
                    if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                        vm.division = this.dataItem(e.item).LK_DATA_NAME_EN;
                    } else {
                        delete vm['division'];
                    }

                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getSlvTypeList() {
            return vm.SlvTypeList = {
                optionLabel: "--Sleeve Type--",
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
                select: function (e) {

                    if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                        vm.SlvType = this.dataItem(e.item).LK_DATA_NAME_EN;
                    } else {
                        delete vm['SlvType'];
                    }
                },

                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
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
                optionLabel: "--Neck Type--",
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

                select: function (e) {

                    if (this.dataItem(e.item).LOOKUP_DATA_ID) {
                        vm.NeckType = this.dataItem(e.item).LK_DATA_NAME_EN;
                    } else {
                        delete vm['NeckType'];
                    }
                },

                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        }

        function getRmgItemStatusList() {
            return vm.RmgItemStatusList = {
                optionLabel: "--Status --",
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

        $scope.$watchGroup(['vm.form.STYL_ALT_IMG_FILE', 'vm.form.STYL_KEY_IMG_FILE'], function (newVal, OldVal) {
            if (newVal[0]) {
                vm.form["STYL_ALT_IMG"] = newVal[0].base64;
            }

            if (newVal[1]) {
                vm.form["STYL_KEY_IMG"] = newVal[1].base64;
            }
        }, true);


        function getItemList() {
            return vm.existingItemList = {
                optionLabel: "-- No Item --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getDataByUrl('/StyleDItem/StyleDtlItemList/' + $stateParams.MC_STYLE_H_ID + '/' + vm.HAS_SET).then(function (res) {
                                console.log(res);
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    if (this.dataItem(e.item).MC_STYLE_D_ITEM_ID) {
                        vm.form1 = this.dataItem(e.item);
                        vm.form['INV_ITEM_CAT_ID'] = this.dataItem(e.item).INV_ITEM_CAT_ID;

                        vm.form['ITEM_GRP_ID'] = (_.filter(vm.allItemType, function (o) {
                            return o.INV_ITEM_CAT_ID == vm.form['INV_ITEM_CAT_ID'];
                        })[0].PARENT_ID) || ''




                        vm.form['LK_GARM_DEPT_ID'] = this.dataItem(e.item).LK_GARM_DEPT_ID;
                        vm.form['LK_NECK_TYPE_ID'] = this.dataItem(e.item).LK_NECK_TYPE_ID;
                        vm.form['LK_SLV_TYPE_ID'] = this.dataItem(e.item).LK_SLV_TYPE_ID;
                        vm.form['ITEM_SNAME'] = this.dataItem(e.item).ITEM_SNAME;
                        vm.form['ITEM_NAME_EN'] = this.dataItem(e.item).ITEM_NAME_EN;
                        vm.form['IS_SOLID'] = this.dataItem(e.item).IS_SOLID;
                        vm.form['IS_YD'] = this.dataItem(e.item).IS_YD;
                        vm.form['IS_AOP'] = this.dataItem(e.item).IS_AOP;
                        vm.form['IS_EMB'] = this.dataItem(e.item).IS_EMB;
                        vm.form['IS_PRINT'] = this.dataItem(e.item).IS_PRINT;
                        vm.form['IS_GMT_WASH'] = this.dataItem(e.item).IS_GMT_WASH;

                        vm.form['TECH_SPEC'] = this.dataItem(e.item).TECH_SPEC;
                        vm.form['SP_INSTRUCTION'] = this.dataItem(e.item).SP_INSTRUCTION;

                        vm.form['LK_GARM_TYPE_ID'] = this.dataItem(e.item).LK_GARM_TYPE_ID;
                        vm.form['NO_MACHINE'] = this.dataItem(e.item).NO_MACHINE;
                        vm.form['EST_PROD_CAPACTY_HR'] = this.dataItem(e.item).EST_PROD_CAPACTY_HR;

                        vm.form['MC_STYLE_D_ITEM_ID'] = 0;
                        vm.form['PARENT_ID'] = vm.params.PARENT_ID || null;
                        vm.form['COMBO_NO'] = null;


                        $timeout(function () {
                            vm.form['LK_YD_TYPE_ID'] = vm.form1.LK_YD_TYPE_ID;
                            vm.form['LK_PRN_TYPE_ID'] = MrcDataService.parseXmlString(vm.form1.LK_PRN_TYPE_ID);
                            vm.form['LK_EMB_TYPE_ID'] = MrcDataService.parseXmlString(vm.form1.LK_EMB_TYPE_ID);
                            vm.form['LK_GMT_WASH_TYPE_ID'] = MrcDataService.parseXmlString(vm.form1.LK_GMT_WASH_TYPE_ID);
                            vm.form['LK_AOP_TYPE_ID'] = MrcDataService.parseXmlString(vm.form1.LK_AOP_TYPE_ID);
                        })



                    } else {
                        vm.form = { LK_ITEM_STATUS_ID: 258, PARENT_ID: vm.params.PARENT_ID || null };
                    }

                },
                dataTextField: "ITEM_SNAME",
                dataValueField: "MC_STYLE_D_ITEM_ID"
            };
        };





        vm.submitData = function (formData, token) {
            var data = angular.copy(formData);
            data['SEGMENT_FLAG'] = vm.HAS_COMBO == "Y" ? 'C' : 'I';

            data["MC_STYLE_H_ID"] = $stateParams.MC_STYLE_H_ID;

            if (data.PARENT_ID == data.MC_STYLE_D_ITEM_ID) {
                data['PARENT_ID'] = null;
            } else {
                data['PARENT_ID'] = vm.params.PARENT_ID;
            }

            if (data.IS_YD == 'N') {
                data['LK_YD_TYPE_ID'] = null;
            }


            if (data.IS_PRINT == 'N') {
                data['LK_PRN_TYPE_ID'] = '';
            } else {
                data["LK_PRN_TYPE_ID"] = MrcDataService.xmlStringLong(data.LK_PRN_TYPE_ID);
            }


            if (data.IS_AOP == 'N') {
                data['LK_AOP_TYPE_ID'] = '';
            } else {
                data["LK_AOP_TYPE_ID"] = MrcDataService.xmlStringLong(data.LK_AOP_TYPE_ID);
            }

            if (data.IS_EMB == 'N') {
                data['LK_EMB_TYPE_ID'] = '';
            } else {
                data["LK_EMB_TYPE_ID"] = MrcDataService.xmlStringLong(data.LK_EMB_TYPE_ID);
            }

            if (data.IS_GMT_WASH == 'N') {
                data['LK_GMT_WASH_TYPE_ID'] = '';
            } else {
                data["LK_GMT_WASH_TYPE_ID"] = MrcDataService.xmlStringLong(data.LK_GMT_WASH_TYPE_ID);
            }

            if (angular.isDefined(data[key]) && data[key] > 0) {

                $http({
                    method: 'post',
                    url: '/Merchandising/Mrc/Update',
                    data: data
                }).success(function (data, status, headers, config1) {

                    data['data'] = angular.fromJson(data.jsonStr);
                    config.appToastMsg(data.data.PMSG);

                    if (data.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        if ($state.current.LK_STYL_DEV_ID == 265) {
                            $state.go('StyleHDev.ItemDev', $stateParams.current, { reload: 'StyleHDev' });
                        } else {
                            $state.go('StyleH.Item', $stateParams.current, { reload: 'StyleH' });
                        }

                      
                    }

                }).error(function (data, status, headers, config) {
                    console.log(status);
                });

            } else {

                $http({
                    method: 'post',
                    url: '/Merchandising/Mrc/Save',
                    data: data
                }).success(function (data, status, headers, config1) {
                    data['data'] = angular.fromJson(data.jsonStr);
                    config.appToastMsg(data.data.PMSG);

                    if (data.data.V_MC_STYLE_D_ITEM_ID != 0 && data.data.PMSG.substr(0, 9) == 'MULTI-001') {

                        if ($state.current.LK_STYL_DEV_ID == 265) {
                            $state.go('StyleHDev.ItemDev', { MC_STYLE_D_ITEM_ID: data.data.V_MC_STYLE_D_ITEM_ID }, { reload: 'StyleHDev' });
                        } else {
                            $state.go('StyleH.Item', { MC_STYLE_D_ITEM_ID: data.data.V_MC_STYLE_D_ITEM_ID }, { reload: 'StyleH' });
                        }
                        
                    }
                }).error(function (data, status, headers, config) {
                    console.log(status);
                });

            }

        }
        function findWithAttr(array, attr, value) {
            for (var i = 0; i < array.length; i += 1) {
                if (array[i][attr] === value) {
                    return i;
                }
            }
        }

        vm.makeActiveToggle = function (formData, token) {

            var data = angular.copy(formData);
            data['SEGMENT_FLAG'] = vm.HAS_COMBO == "Y" ? 'C' : 'I';

            data["MC_STYLE_H_ID"] = $stateParams.MC_STYLE_H_ID;

            if (data.PARENT_ID == data.MC_STYLE_D_ITEM_ID) {
                data['PARENT_ID'] = null;
            } else {
                data['PARENT_ID'] = vm.params.PARENT_ID;
            }

            if (data.IS_YD == 'N') {
                data['LK_YD_TYPE_ID'] = null;
            }


            if (data.IS_PRINT == 'N') {
                data['LK_PRN_TYPE_ID'] = '';
            } else {
                data["LK_PRN_TYPE_ID"] = MrcDataService.xmlStringLong(data.LK_PRN_TYPE_ID);
            }


            if (data.IS_AOP == 'N') {
                data['LK_AOP_TYPE_ID'] = '';
            } else {
                data["LK_AOP_TYPE_ID"] = MrcDataService.xmlStringLong(data.LK_AOP_TYPE_ID);
            }

            if (data.IS_EMB == 'N') {
                data['LK_EMB_TYPE_ID'] = '';
            } else {
                data["LK_EMB_TYPE_ID"] = MrcDataService.xmlStringLong(data.LK_EMB_TYPE_ID);
            }

            if (data.IS_GMT_WASH == 'N') {
                data['LK_GMT_WASH_TYPE_ID'] = '';
            } else {
                data["LK_GMT_WASH_TYPE_ID"] = MrcDataService.xmlStringLong(data.LK_GMT_WASH_TYPE_ID);
            }
            var str = (data.LK_ITEM_STATUS_ID == 258 ? 'Disabling ' : 'Enabling ') + data.ITEM_NAME_EN + '.';
            data['LK_ITEM_STATUS_ID'] = data.LK_ITEM_STATUS_ID == 258 ? 256 : 258;

            Dialog.confirm(str, 'Are you sure?', ['Yes', 'No'])
             .then(function () {
                 $http({
                     method: 'post',
                     url: '/Merchandising/Mrc/Update',
                     data: data
                 }).success(function (data, status, headers, config1) {

                     data['data'] = angular.fromJson(data.jsonStr);
                     config.appToastMsg(data.data.PMSG);

                     if (data.data.PMSG.substr(0, 9) == 'MULTI-001') {
                         if ($state.current.LK_STYL_DEV_ID == 265) {
                             $state.go('StyleHDev.ItemDev', $stateParams.current, { reload: 'StyleHDev' });
                         } else {
                             $state.go('StyleH.Item', $stateParams.current, { reload: 'StyleH' });
                         }


                     }

                 }).error(function (data, status, headers, config) {
                     console.log(status);
                 });
             });
        };

        vm.showItemList = function (MC_STYLE_H_ID) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ItemList.html',
                controller: function ($scope, $modalInstance, ItemList) {

                    console.log(ItemList);
                    $scope.ItemList = ItemList;
                    $scope.cancel = function (data) {
                        $modalInstance.close(data);
                    }

                },
                size: 'lg',
                windowClass: 'large-Modal',
                resolve: {
                    ItemList: function (MrcDataService) {
                        return MrcDataService.getDataByUrl('/StyleDItem/StyleDtlItemList/' + MC_STYLE_H_ID + '/N');
                    }
                }
            });

            modalInstance.result.then(function (data) {
                if (data.MC_STYLE_D_ITEM_ID && data.MC_STYLE_D_ITEM_ID > 0) {
                    if (data.PARENT_ID) {
                        if ($state.current.LK_STYL_DEV_ID == 265) {
                            $state.go('StyleHDev.ItemDev', { MC_STYLE_D_ITEM_ID: data.MC_STYLE_D_ITEM_ID, PARENT_ID: data.PARENT_ID });
                        } else {
                            $state.go('StyleH.Item', { MC_STYLE_D_ITEM_ID: data.MC_STYLE_D_ITEM_ID, PARENT_ID: data.PARENT_ID });
                        }

                       
                    } else {
                        if ($state.current.LK_STYL_DEV_ID == 265) {
                            $state.go('StyleHDev.ItemDev', { MC_STYLE_D_ITEM_ID: data.MC_STYLE_D_ITEM_ID });
                        } else {
                            $state.go('StyleH.Item', { MC_STYLE_D_ITEM_ID: data.MC_STYLE_D_ITEM_ID });
                        }

                      
                    }
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

        };

    }

})();