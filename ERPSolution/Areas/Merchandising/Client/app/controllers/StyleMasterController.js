(function () {
    'use strict';
    angular.module('multitex.mrc').controller('StyleMasterController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'formData', '$filter', '$modal', StyleMasterController]);
    function StyleMasterController($q, config, MrcDataService, $stateParams, $state, $scope, formData, $filter, $modal) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'StyleH';
        var key = 'MC_STYLE_H_ID';
        $scope.format = config.appDateFormat;
        vm.toDay = $filter('date')(new Date(), config.appDateFormat);
        vm.Title = $state.current.Title || '';
        var sizeList = '';
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getbuyerAcList(), getBuyerList(), getDevelopedByList(), getCountryList(), getCompanyList(),getInquiryListData(), getSeasonList(),
                getStyleRefListData(), getMOUList(),getSizeList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.form = { REVISION_DT: vm.toDay, IS_ACTIVE: 'Y', ORIGIN_ID: 1, MANUF_ID: 1, LK_STYL_DEV_ID: $state.current.LK_STYL_DEV_ID, items: [], QTY_MOU_ID: 1, HAS_SET: 'N', HAS_COMBO: 'N', HAS_MODEL: 'N', HAS_MULTI_COL_PACK: 'N' };
        
        if (formData[key]) {
            if (!formData['HAS_SET']) {
                formData['HAS_SET'] = 'N';
            }
            if (!formData['HAS_COMBO']) {
                formData['HAS_COMBO'] = 'N';
            }
            if (formData['MC_SIZE_LST']) {
                formData['MC_SIZE_LST'] = formData.MC_SIZE_LST.split(',');
            }
            vm.form = formData;
            $scope.StyleData = formData;
        }


        vm.itemState = ($state.current.name === 'StyleH.Item' || $state.current.name === 'StyleHDev.ItemDev') ? true : false;
        vm.fabState = ($state.current.name === 'StyleH.Fab' || $state.current.name === 'StyleHDev.FabDev') ? true : false;
        vm.orderConState = ($state.current.name === 'StyleH.OrderCon' || $state.current.name === 'StyleH.OrderCon.Detail' || $state.current.name === 'StyleHDev.OrderConDev' || $state.current.name === 'StyleHDev.OrderConDev.DetailDev') ? true : false;
        vm.colRefState = ($state.current.name === 'StyleH.ColRef' || $state.current.name === 'StyleHDev.ColRefDev') ? true : false;
        vm.StyleBomState = ($state.current.name === 'StyleH.StyleBom') ? true : false;
        //vm.devFabBookingState = ($state.current.name === 'StyleHDev.DevFabBooking') ? true : false;


        $scope.addSetItem = function (data) {
            vm.form['items'] = data;
        };


        $scope.$watch('vm.form.HAS_SET', function (newVal, oldVal) {
            if (newVal && newVal != oldVal) {
                if (newVal == 'Y') {
                    vm.form['QTY_MOU_ID'] = 2;
                } else {
                    vm.form['QTY_MOU_ID'] = 1;
                }
            }
        });


        vm.SetItemList = function (data) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'SetItemEntryModal.html',
                controller: 'SetItemEntryModalController',
                size: 'lg',
                windowClass: 'large-Modal',
                scope: $scope,
                resolve: {
                    Style: function () {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                console.log(data);
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        };







        $scope.REVISION_DTopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REVISION_DTopened = true;
        };

        function getSizeList() {
           return vm.SizeList = {
                placeholder: "--Size List--",
                valuePrimitive: true,
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {

                            //var webapi = new kendo.data.transports.webapi({});
                            //var params = webapi.parameterMap(e.data);
                            //console.log(params);
                            MrcDataService.selectAllData('SizeMaster').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.error(err);
                            });
                        }
                    },
                    //serverFiltering: true,
                },
                change: function (e) {
                    var value = this.value();
                    if (value.length == 0) {
                        sizeList = '';
                    } else {
                        sizeList = value.join(',');
                    }
               },

                dataBound: function (e) {
                    var value = this.value();
                    if (value.length == 0) {
                        sizeList = '';
                    } else {
                        sizeList = value.join(',');
                    }
                },
                dataTextField: "SIZE_CODE",
                dataValueField: "MC_SIZE_ID"
           };
        }


           


        function getbuyerAcList() {
           return vm.buyerAcList = {
                optionLabel: "--- Buyer A/C ---",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                          return MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                select: function (e) {
                    var MC_BYR_ACC_ID = this.dataItem(e.item).MC_BYR_ACC_ID;
                    if (MC_BYR_ACC_ID) {

                        var ds = new kendo.data.DataSource({
                            transport: {
                                read: function (e) {
                                    return MrcDataService.getDataByUrl('/buyer/getBuyerDropdownList?MC_BYR_ACC_ID=' + MC_BYR_ACC_ID).then(function (res) {
                                        e.success(res);
                                    }, function (err) {
                                        console.log(err);
                                    });
                                }
                            }
                        });
                        $("#MC_BUYER_ID").data("kendoDropDownList").setDataSource(ds);

                    } else {
                        $("#MC_BUYER_ID").data("kendoDropDownList").dataSource.read();
                    }
                },

                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }


        function getBuyerList(){
                 return vm.buyerList = {
                        optionLabel: "-- Buyer --",
                        filter: "startswith",
                        autoBind: true,
                        dataSource: {
                            transport: {
                                read: function (e) {
                                    return MrcDataService.getDataByUrl('/buyer/getBuyerDropdownList?MC_BYR_ACC_ID=' + (formData.MC_BYR_ACC_ID||'')).then(function (res) {
                                        e.success(res);
                                    }, function (err) {
                                        console.log(err);
                                    });
                                }
                            }
                        },
                        select: function (e) {
                            var MC_BUYER_ID = this.dataItem(e.item).MC_BUYER_ID;
                            if (MC_BUYER_ID) {
                                var dataSource1 = new kendo.data.DataSource({
                                    transport: {
                                        read: function (e) {
                                            return MrcDataService.getDataByUrl('/StyleH/getStyleDropDown?pSTYLE_NO&pMC_BUYER_ID=' + MC_BUYER_ID + '&pMC_STYLE_H_ID').then(function (res) {
                                                e.success(res);
                                            }, function (err) {
                                                console.log(err);
                                            });
                                        }
                                    }
                                });

                                var dataSource2 = new kendo.data.DataSource({
                                    data: _.filter(vm.inquiryListData, { 'MC_BUYER_ID': MC_BUYER_ID })
                                });

                                $("#PARENT_ID").data("kendoDropDownList").setDataSource(dataSource1);
                                $("#MC_INQR_H_ID").data("kendoDropDownList").setDataSource(dataSource2);
                            } else {
                                $("#PARENT_ID").data("kendoDropDownList").dataSource.read();
                                $("#MC_INQR_H_ID").data("kendoDropDownList").dataSource.read();
                            }
                        },
                        dataTextField: "BUYER_NAME_EN",
                        dataValueField: "MC_BUYER_ID"
                    };
        }
        function getInquiryListData() {
            return vm.inquiryList = {
                optionLabel: "-- Inquiry # --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.selectAllData('InquiryH').then(function (res) {
                                vm.inquiryListData = res;
                                e.success(_.filter(res, function (o) {
                                    return formData[key] ? o.MC_BUYER_ID == formData.MC_BUYER_ID : true;
                                }));
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "INQR_NO",
                dataValueField: "MC_INQR_H_ID"
            };
        }


        function getStyleRefListData() {
            return vm.StyleRefList = {
                optionLabel: "--  Prev. Style--",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);


                            if (params.filter) {
                                var qmPrevStyle = '?pSTYLE_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                            } else {
                                var qmPrevStyle = '?pSTYLE_NO';
                            }

                            if (formData[key]) {
                                qmPrevStyle += '&pMC_BUYER_ID=' + formData.MC_BUYER_ID;
                                qmPrevStyle += '&pMC_STYLE_H_ID=' + formData.PARENT_ID;
                            } else {
                                if (MC_BUYER_ID) {
                                    qmPrevStyle += '&pMC_BUYER_ID=' + MC_BUYER_ID + '&pMC_STYLE_H_ID';
                                } else {
                                    qmPrevStyle += '&pMC_BUYER_ID&pMC_STYLE_H_ID';
                                }
                            };
                           
                            return MrcDataService.getDataByUrl('/StyleH/getStyleDropDown' + qmPrevStyle).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    },
                    serverFiltering: true,
                },
                
                
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_ID"
            };
        }



        function getSeasonList() {
            return vm.seasonList = {
                optionLabel: "--Season--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.LookupListData(52).then(function (res) {
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


        function getDevelopedByList() {
           return vm.DevelopedByList = {
                optionLabel: "--Status--",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.LookupListData(53).then(function (res) {
                                e.success(res);
                                //e.success(res.filter(function (o) {
                                //    return $state.current.LK_STYL_DEV_ID == 265 ? o.LOOKUP_DATA_ID == $state.current.LK_STYL_DEV_ID : true;
                                //}));
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

        function getCountryList() {
            return vm.CountryList = {
                optionLabel: "-- Select Origin --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.GetCountryList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COUNTRY_NAME_EN",
                dataValueField: "HR_COUNTRY_ID"
            };
        }

        function getCompanyList() {
            return vm.CompanyList = {
                optionLabel: "-- Supplier --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.getCompanyData().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "COMP_NAME_EN",
                dataValueField: "HR_COMPANY_ID"
            };
        }

        vm.submitData = function (dataOri, token) {

            var data = angular.copy(dataOri);

            data['MC_SIZE_LST'] = sizeList;
            if (angular.isDefined(data[key]) && data[key] > 0) {

                if (data.items.length > 0) {
                    if (data.HAS_SET == 'Y' && data.HAS_MODEL == 'N') {
                        data['items'][0]['ITEM_NAME_EN'] = data.STYLE_DESC;
                    }
                } else if (data.items.length == 0) {
                    if (data.HAS_SET == 'Y' && data.HAS_COMBO == 'Y') {
                        data['items'] = [{ MC_STYLE_D_ITEM_ID: -1, COMBO_NO: data.items[0].COMBO_NO, MODEL_NO: 'Set', ITEM_NAME_EN: data.STYLE_DESC, SEGMENT_FLAG: 'I', HAS_MODEL: data.HAS_MODEL }];
                    } else if (data.HAS_SET == 'Y' && data.HAS_COMBO == 'N' && data.HAS_MODEL == 'N') {
                        data['items'] = [{ MC_STYLE_D_ITEM_ID: -1, COMBO_NO: '', MODEL_NO: 'Set', ITEM_NAME_EN: data.STYLE_DESC, SEGMENT_FLAG: 'I', HAS_MODEL: data.HAS_MODEL }];
                    }
                }

                console.log(data);

                return MrcDataService.updateData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            $state.go($state.current, $stateParams.current, { reload: 'StyleH' });
                        }

                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {

                if (data.HAS_SET == 'Y' && data.HAS_COMBO == 'Y') {
                    data['items'] = [{ MC_STYLE_D_ITEM_ID: -1, COMBO_NO: data.items[0].COMBO_NO, MODEL_NO: 'Set', ITEM_NAME_EN: data.STYLE_DESC, SEGMENT_FLAG: 'I', HAS_MODEL: data.HAS_MODEL }];
                } else if (data.HAS_SET == 'Y' && data.HAS_COMBO == 'N' && data.HAS_MODEL == 'N') {
                    data['items'] = [{ MC_STYLE_D_ITEM_ID: -1, COMBO_NO: '', MODEL_NO: 'Set', ITEM_NAME_EN: data.STYLE_DESC, SEGMENT_FLAG: 'I', HAS_MODEL: data.HAS_MODEL }];
                }
                return MrcDataService.saveData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);


                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001' && res.data.V_MC_STYLE_H_ID != 0) {
                            $state.go($state.current, { MC_STYLE_H_ID: res.data.V_MC_STYLE_H_ID });
                        }

                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.goToItemReg = function () {
            var params = angular.extend({}, $stateParams.current, { MC_STYLE_ITEM_ID: 0 })
            $state.go('Inquiry.Style.Item', params);
        }
    }

})();