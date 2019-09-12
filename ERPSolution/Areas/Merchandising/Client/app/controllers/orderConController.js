(function () {
    'use strict';
    angular.module('multitex.mrc').controller('OrderConController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'formData', '$filter', '$modal', '$timeout', OrderConController]);
    function OrderConController($q, config, MrcDataService, $stateParams, $state, $scope, formData, $filter, $modal, $timeout) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'StyleH';
        var key = 'MC_STYLE_H_ID';
        vm.BuyerListToBeFilter = [];

        var fData = angular.copy(formData);

        vm.Title = $state.current.Title || '';

        console.log(formData);


        vm.form = fData[key] ? fData : { IS_ACTIVE: 'Y', ORIGIN_ID: 1, MANUF_ID: 1, LK_STYL_DEV_ID: 366, items: [], QTY_MOU_ID: 1, HAS_SET: 'N', HAS_COMBO: 'N', HAS_MODEL: 'N', HAS_MULTI_COL_PACK: 'N', HR_COUNTRY_ID_LST:[] };

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerListData(),
                 getDevelopedByList(), getBuyerAccListByUserId(), getOrderTypeList(), getMOUList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.sizeShipmentDateOpened = [];
        vm.sizeShipmentDateOpen = function ($event, index) {
            if (vm.form.IS_ORD_FINALIZED != 'Y') {
                $event.preventDefault();
                $event.stopPropagation();

                vm.sizeShipmentDateOpened[index] = true;
            }
        };


        if (fData[key]) {
            if (fData['MC_SIZE_LST']) {
                fData['MC_SIZE_LST'] = fData.MC_SIZE_LST.split(',');
            }
        }



        $scope.StyleData = vm.form;

        function getOrderTypeList() {
            return MrcDataService.LookupListData(40).then(function (res) {
                vm.OrderTypeList = res;
            }, function (err) {
                console.log(err);
            });
        }

        vm.SizeList = {
            placeholder: "--Size List--",
            valuePrimitive: true,
            autoBind: false,
            dataSource: {
                transport: {
                    read: function (e) {
                        MrcDataService.selectAllData('SizeMaster').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.error(err);
                        });
                    }
                }
            },
            dataTextField: "SIZE_CODE",
            dataValueField: "MC_SIZE_ID"
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

        vm.PoListOfOrderDocRecvDeadlineDs = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            schema: {
                data: "data",
                total: "total",
                model: {
                    fields: {
                        ORD_CONF_DT: { type: "date" },
                        SHIP_DT: { type: "date" },
                        ORD_DOC_RCV_DT: { type: "date" },
                        TGT_ORD_DOC_RCV_DT: { type: "date" },
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/mrc/Style/getTargetOrdDocRecv';
                    url += '?pageNumber=' + (params.page || 1) + '&pageSize=' + (params.pageSize || 10);
                    url += config.kFilterStr2QueryParam(params.filter);

                    return MrcDataService.getDataByFullUrl(url).then(function (res) {
                        e.success({
                            total: res.total,
                            data: res.data
                        });
                    }, function (err) {
                        console.error(err);
                    })
                }
            },
            pageSize: 10
        });
        vm.PoListOfOrderDocRecvDeadlineOpt = {
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
            height: 250,
            scrollable: {
                virtual: true
            },

            selectable: false,
            sortable: true,
            columns: [

                {
                    title: "BuyerAcc",
                    field: 'BYR_ACC_NAME_EN',
                    width: "80px",
                    filterable: true
                },
                                {
                                    title: "Style/Order",
                                    field: 'STYLE_NO',
                                    width: "80px",
                                    filterable: true,
                                    template: function () {
                                        return "<span>{{dataItem.STYLE_NO}}</span><span style=\"display:block;\">{{dataItem.ORDER_NO}}</span>";
                                    },

                                },

                {
                    field: "TGT_ORD_DOC_RCV_DT", title: "DeadLine", type: "date", width: "60px", format: "{0:dd-MMM-yyyy}"
                },
                                {
                                    field: "SHIP_DT", title: "Ship", type: "date", width: "60px", format: "{0:dd-MMM-yyyy}"
                                },


            ]
        };



        $scope.$watch('vm.form.LK_ORD_TYPE_ID', function (newVal, oldVal) {
            if (newVal && newVal != oldVal) {
                if (newVal != 174 && !fData[key]) {
                    vm.form['MC_STYLE_H_ID'] = -1,
                    vm.form['MC_BUYER_ID'] = '',
                    vm.form['STYLE_NO'] = '',
                    vm.form['STYL_EXT_NO'] = '',
                    vm.form['REF_STYLE_NO'] = '',
                    vm.form['COMP_STYLE_NO'] = '',
                    vm.form['STYLE_DESC'] = '',
                    vm.form['ORIGIN_ID'] = 1,
                    vm.form['MANUF_ID'] = 1,
                    vm.form['HAS_SET'] = 'N',
                    vm.form['HAS_COMBO'] = 'N',
                    vm.form['LK_STYL_DEV_ID'] = '',
                    vm.form['IS_ACTIVE'] = 'Y',
                    vm.form['NO_OF_SET'] = 0,
                    vm.form['QTY_MOU_ID'] = 1,
                    vm.form['PCS_PER_PACK'] = 0,
                    vm.form['MATERIAL_DESC'] = '',
                    vm.form['HAS_MODEL'] = 'N',
                    vm.form['MC_BYR_ACC_ID'] = '',
                    vm.form['HAS_MULTI_COL_PACK'] = 'N',
                    vm.form['MC_STYLE_H_EXT_ID'] = '',
                    vm.form['ITEM_LIST'] = '',
                    vm.form['EXT_XML'] = '',
                    vm.form['ALL_ITEM_LIST'] = ''
                    vm.form['MC_BUYER_ID'] = '';
                    vm.form['items'] = [];
                    vm.form['MC_SIZE_LST'] = [];

                    $scope.StyleInOrderCon.$setPristine();
                }
            }
        });



        $scope.setLK_ORD_TYPE_ID = function (LK_ORD_TYPE_ID) {
            vm.form['LK_ORD_TYPE_ID'] = LK_ORD_TYPE_ID;
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


        function getBuyerAccListByUserId() {
            return vm.buyerAcList = {
                optionLabel: "--- Buyer A/C ---",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {

                            MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId/').then(function (res) {
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
                        var dataSource = new kendo.data.DataSource({
                            data: _.filter(vm.BuyerListToBeFilter, { 'MC_BYR_ACC_ID': MC_BYR_ACC_ID })
                        });

                        $("#MC_BUYER_ID").data("kendoDropDownList").setDataSource(dataSource);

                        if (vm.form.LK_ORD_TYPE_ID == 174) {
                            MrcDataService.getDataByUrl('/StyleH/BuyerAccWiseStyleListData/' + MC_BYR_ACC_ID).then(function (res) {
                                $("#MC_STYLE_H_ID").data("kendoDropDownList").setDataSource(new kendo.data.DataSource({
                                    data: res
                                }));
                            });

                        } else {
                            $("#MC_BUYER_ID").data("kendoDropDownList").dataSource.read();
                        }
                    } 
                },

                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }


        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO #</h5><p> Style: #: data.STYLE_NO #</p></span>';


      
        vm.buyerAcListFilter = {
            optionLabel: "--- Buyer A/C ---",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {

                        MrcDataService.GetAllOthers('/api/mrc/BuyerAcc/getBuyerAccListByUserId/').then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            },
            change: function (e) {
                var MC_BYR_ACC_ID = this.dataItem(e.item).MC_BYR_ACC_ID;
                if (MC_BYR_ACC_ID) {
                    vm.OrderListFilter = {
                        transport: {
                            read: function (e) {
                                var url = "/api/common/getOrderStyleDropDownData?pMC_BYR_ACC_ID=" + MC_BYR_ACC_ID;
                                var webapi = new kendo.data.transports.webapi({});
                                var params = webapi.parameterMap(e.data);
                                if (params.filter) {
                                    url += '&pORDER_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                                } else {
                                    url += '&pORDER_NO';
                                }

                                return MrcDataService.GetAllOthers(url).then(function (res) {
                                   
                                    e.success(res);
                                });
                            }
                        },
                        serverFiltering: true
                    };
                }
            },

            dataTextField: "BYR_ACC_NAME_EN",
            dataValueField: "MC_BYR_ACC_ID"
        };

        vm.onOrderChange = function (e) {
         
            $scope.MC_STYLE_H_ID_F = e.sender.dataItem(e.sender.item).MC_STYLE_H_ID||-1;
        }

        vm.OrderListFilter = new kendo.data.DataSource({
            data: []
        });





        function getBuyerListData() {
            return vm.buyerList = {
                optionLabel: "-- Buyer --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {

                            MrcDataService.BuyerByUserList().then(function (res) {
                                vm.BuyerListToBeFilter = angular.copy(res);

                                e.success(formData[key] ? _.filter(vm.BuyerListToBeFilter, function (o) {
                                    return parseInt(o.MC_BYR_ACC_ID) == parseInt(formData.MC_BYR_ACC_ID);
                                }) : []);

                            });
                        }
                    }
                },
                change: function (e) {

                    console.log(this.dataItem(e.item));

                    var MC_BUYER_ID = this.dataItem(e.item).MC_BUYER_ID;
                    if (MC_BUYER_ID) {

                        if (vm.form.LK_ORD_TYPE_ID == 174) {
                            var qmPrevStyle = '?pSTYLE_NO&pMC_BUYER_ID=' + MC_BUYER_ID + '&pMC_STYLE_H_ID';
                            return MrcDataService.getDataByUrl('/StyleH/getStyleDropDown' + qmPrevStyle).then(function (res) {
                                vm.styleData = new kendo.data.DataSource({
                                    data: res
                                });
                            });
                        }

                        vm.form['HR_COUNTRY_ID_LST'] = [this.dataItem(e.item).HR_COUNTRY_ID];

                    } else {
                        vm.form['HR_COUNTRY_ID_LST'] = [];
                    }
                },
                dataTextField: "BUYER_NAME_EN",
                dataValueField: "MC_BUYER_ID"
            };
        }

        vm.styleData = {
            optionLabel: "-- Style--",
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

                        if (fData.MC_BUYER_ID) {
                            qmPrevStyle += '&pMC_BUYER_ID=' + MC_BUYER_ID + '&pMC_STYLE_H_ID=' + ($stateParams.MC_STYLE_H_ID||'');
                        } else {
                            qmPrevStyle += '&pMC_BUYER_ID&pMC_STYLE_H_ID=' + ($stateParams.MC_STYLE_H_ID || '');
                        }
                        return MrcDataService.getDataByUrl('/StyleH/getStyleDropDown' + qmPrevStyle).then(function (res) {
                            e.success(res);
                        });
                    }
                },
                serverFiltering: true,
            },
            change: function (e){
                if (this.dataItem(e.item).MC_STYLE_H_ID) {
                    MrcDataService.selectData('StyleH', this.dataItem(e.item).MC_STYLE_H_ID).then(function (res) {
                        vm.form['LK_ORD_TYPE_ID'] = 174;
                        vm.form['MC_STYLE_H_ID'] = res.MC_STYLE_H_ID,
                        vm.form['MC_BUYER_ID'] = res.MC_BUYER_ID,
                        vm.form['STYLE_NO'] = res.STYLE_NO,
                        vm.form['STYL_EXT_NO'] = res.STYL_EXT_NO,
                        vm.form['REF_STYLE_NO'] = res.REF_STYLE_NO,
                        vm.form['COMP_STYLE_NO'] = res.COMP_STYLE_NO,
                        vm.form['STYLE_DESC'] = res.STYLE_DESC,
                        vm.form['ORIGIN_ID'] = res.ORIGIN_ID,
                        vm.form['MANUF_ID'] = res.MANUF_ID,
                        vm.form['HAS_SET'] = res.HAS_SET,
                        vm.form['HAS_COMBO'] = res.HAS_COMBO,
                        vm.form['LK_STYL_DEV_ID'] = res.LK_STYL_DEV_ID,
                        vm.form['IS_ACTIVE'] = res.IS_ACTIVE,
                        vm.form['NO_OF_SET'] = res.NO_OF_SET,
                        vm.form['QTY_MOU_ID'] = res.QTY_MOU_ID,
                        vm.form['PCS_PER_PACK'] = res.PCS_PER_PACK,
                        vm.form['MATERIAL_DESC'] = res.MATERIAL_DESC,
                        vm.form['HAS_MODEL'] = res.HAS_MODEL,
                        vm.form['MC_BYR_ACC_ID'] = res.MC_BYR_ACC_ID,
                        vm.form['HAS_MULTI_COL_PACK'] = res.HAS_MULTI_COL_PACK,
                        vm.form['MC_STYLE_H_EXT_ID'] = res.MC_STYLE_H_EXT_ID,
                        vm.form['ITEM_LIST'] = res.ITEM_LIST,
                        vm.form['EXT_XML'] = res.EXT_XML,
                        vm.form['ALL_ITEM_LIST'] = res.ALL_ITEM_LIST
                        vm.form['MC_BUYER_ID'] = res.MC_BUYER_ID;
                        vm.form['items'] = res.items;
                        vm.form['MC_SIZE_LST'] = res.MC_SIZE_LST ? res.MC_SIZE_LST.split(','):[];
                    });

                } else {
                    vm.form['MC_STYLE_H_ID'] = '',
                    vm.form['MC_BUYER_ID'] = '',
                    vm.form['STYLE_NO'] = '',
                    vm.form['STYL_EXT_NO'] = '',
                    vm.form['REF_STYLE_NO'] = '',
                    vm.form['COMP_STYLE_NO'] = '',
                    vm.form['STYLE_DESC'] = '',
                    vm.form['ORIGIN_ID'] = 1,
                    vm.form['MANUF_ID'] = 1,
                    vm.form['HAS_SET'] = 'N',
                    vm.form['HAS_COMBO'] = 'N',
                    vm.form['LK_STYL_DEV_ID'] = '',
                    vm.form['IS_ACTIVE'] = 'Y',
                    vm.form['NO_OF_SET'] = 0,
                    vm.form['QTY_MOU_ID'] = 1,
                    vm.form['PCS_PER_PACK'] = 0,
                    vm.form['MATERIAL_DESC'] = '',
                    vm.form['HAS_MODEL'] = 'N',
                    vm.form['MC_BYR_ACC_ID'] = vm.form.MC_BYR_ACC_ID,
                    vm.form['HAS_MULTI_COL_PACK'] = 'N',
                    vm.form['MC_STYLE_H_EXT_ID'] = '',
                    vm.form['ITEM_LIST'] = '',
                    vm.form['EXT_XML'] = '',
                    vm.form['ALL_ITEM_LIST'] = ''
                    vm.form['MC_BUYER_ID'] = vm.form.MC_BUYER_ID;
                    vm.form['items'] = [];
                    vm.form['MC_SIZE_LST'] = [];

                    $scope.StyleInOrderCon.$setPristine();
                }
            },

            dataTextField: "STYLE_NO",
            dataValueField: "MC_STYLE_H_ID"
        };

         //vm.styleData = {
         //       optionLabel: "-- Select Style --",
         //       filter: "startswith",
         //       autoBind: true,
         //       dataSource: {
         //           transport: {
         //               read: function (e) {
         //                   var url = null;
         //                   if (fData.MC_BYR_ACC_ID && !fData.MC_BUYER_ID) {
         //                       url = '/StyleH/BuyerAccWiseStyleListData/' + fData.MC_BYR_ACC_ID;
         //                   } else if (fData.MC_BYR_ACC_ID && fData.MC_BUYER_ID) {
         //                       url = '/StyleH/BuyerWiseStyleListData/' + fData.MC_BUYER_ID;
         //                   }

         //                   if (url) {
         //                       return MrcDataService.getDataByUrl(url).then(function (res) {
         //                           e.success(res);
         //                       }, function (err) {
         //                           console.log(err);
         //                       });
         //                   } else {
         //                       e.success([]);
         //                   }
         //               }
         //           }
         //       },
         //       dataBound: function (e) {
         //           //vm.form['LK_ORD_TYPE_ID'] = 174;
         //       },
         //       select: function (e) {
         //           var dataItem = this.dataItem(e.item);
         //           if (dataItem.MC_STYLE_H_ID && dataItem.MC_STYLE_H_ID > 0) {
         //               vm.form['LK_ORD_TYPE_ID'] = 174;
         //               vm.form['MC_STYLE_H_ID'] = dataItem.MC_STYLE_H_ID,
         //               vm.form['MC_BUYER_ID'] = dataItem.MC_BUYER_ID,
         //               vm.form['STYLE_NO'] = dataItem.STYLE_NO,
         //               vm.form['STYL_EXT_NO'] = dataItem.STYL_EXT_NO,
         //               vm.form['REF_STYLE_NO'] = dataItem.REF_STYLE_NO,
         //               vm.form['COMP_STYLE_NO'] = dataItem.COMP_STYLE_NO,
         //               vm.form['STYLE_DESC'] = dataItem.STYLE_DESC,
         //               vm.form['ORIGIN_ID'] = dataItem.ORIGIN_ID,
         //               vm.form['MANUF_ID'] = dataItem.MANUF_ID,
         //               vm.form['HAS_SET'] = dataItem.HAS_SET,
         //               vm.form['HAS_COMBO'] = dataItem.HAS_COMBO,
         //               vm.form['LK_STYL_DEV_ID'] = dataItem.LK_STYL_DEV_ID,
         //               vm.form['IS_ACTIVE'] = dataItem.IS_ACTIVE,
         //               vm.form['NO_OF_SET'] = dataItem.NO_OF_SET,
         //               vm.form['QTY_MOU_ID'] = dataItem.QTY_MOU_ID,
         //               vm.form['PCS_PER_PACK'] = dataItem.PCS_PER_PACK,
         //               vm.form['MATERIAL_DESC'] = dataItem.MATERIAL_DESC,
         //               vm.form['HAS_MODEL'] = dataItem.HAS_MODEL,
         //               vm.form['MC_BYR_ACC_ID'] = dataItem.MC_BYR_ACC_ID,
         //               vm.form['HAS_MULTI_COL_PACK'] = dataItem.HAS_MULTI_COL_PACK,
         //               vm.form['MC_STYLE_H_EXT_ID'] = dataItem.MC_STYLE_H_EXT_ID,
         //               vm.form['ITEM_LIST'] = dataItem.ITEM_LIST,
         //               vm.form['EXT_XML'] = dataItem.EXT_XML,
         //               vm.form['ALL_ITEM_LIST'] = dataItem.ALL_ITEM_LIST
         //               vm.form['MC_BUYER_ID'] = dataItem.MC_BUYER_ID;
         //               vm.form['items'] = dataItem.items;
         //               vm.form['MC_SIZE_LST'] = dataItem.MC_SIZE_LST? dataItem.MC_SIZE_LST.split(','): [];
         //           } else {
         //               vm.form['MC_STYLE_H_ID'] = '',
         //               vm.form['MC_BUYER_ID'] = '',
         //               vm.form['STYLE_NO'] = '',
         //               vm.form['STYL_EXT_NO'] = '',
         //               vm.form['REF_STYLE_NO'] = '',
         //               vm.form['COMP_STYLE_NO'] = '',
         //               vm.form['STYLE_DESC'] = '',
         //               vm.form['ORIGIN_ID'] = 1,
         //               vm.form['MANUF_ID'] = 1,
         //               vm.form['HAS_SET'] = 'N',
         //               vm.form['HAS_COMBO'] = 'N',
         //               vm.form['LK_STYL_DEV_ID'] = '',
         //               vm.form['IS_ACTIVE'] = 'Y',
         //               vm.form['NO_OF_SET'] = 0,
         //               vm.form['QTY_MOU_ID'] = 1,
         //               vm.form['PCS_PER_PACK'] = 0,
         //               vm.form['MATERIAL_DESC'] = '',
         //               vm.form['HAS_MODEL'] = 'N',
         //               vm.form['MC_BYR_ACC_ID'] = dataItem.MC_BYR_ACC_ID,
         //               vm.form['HAS_MULTI_COL_PACK'] = 'N',
         //               vm.form['MC_STYLE_H_EXT_ID'] = '',
         //               vm.form['ITEM_LIST'] = '',
         //               vm.form['EXT_XML'] = '',
         //               vm.form['ALL_ITEM_LIST'] = ''
         //               vm.form['MC_BUYER_ID'] = dataItem.MC_BUYER_ID;
         //               vm.form['items'] = [];
         //               vm.form['MC_SIZE_LST'] = [];

         //               $scope.StyleInOrderCon.$setPristine();
         //           }
         //       },
         //       dataTextField: "STYLE_NO",
         //       dataValueField: "MC_STYLE_H_ID"
         //   };




        function getInquiryListData() {
            return vm.inquiryList = {
                optionLabel: "-- Inquiry # --",
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.selectAllData('InquiryH').then(function (res) {
                                e.success(res);
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
                filter: "startswith",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.selectAllData('StyleH').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_ID"
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
                            MrcDataService.LookupListData(53).then(function (res) {
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

        vm.submitData = function (dataOri, token) {
            var data = angular.copy(dataOri);
            data['MC_SIZE_LST'] = (data.MC_SIZE_LST && data.MC_SIZE_LST.length > 0) ? data.MC_SIZE_LST.join(',') : '';

            if (angular.isDefined(data[key]) && data[key] > 0) {

                return MrcDataService.updateData(data, ctrl, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, $stateParams.current, { reload: true });
                        }

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
                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001' && res.data.V_MC_STYLE_H_ID != 0) {
                            config.appToastMsg(res.data.PMSG);
                            $state.go($state.current, { MC_STYLE_H_ID: res.data.V_MC_STYLE_H_ID });
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }
        }

        vm.goToStyleGallery = function (data) {
            var url = '/Merchandising/Mrc/StyleList?a=82/#/master/' + data.MC_STYLE_H_ID + '/Fab/0';
            var a = document.createElement('a');
            a.href = url;
            a.target = '_self';
            document.body.appendChild(a);
            a.click();
        };

        vm.goToFabricProjectionBooking = function (data) {
            var url = '/Merchandising/Mrc/ProjectionOrderList?a=341/#/ProjectionOrder?pMC_PROV_FAB_BK_H_ID=-1&pIS_VIEW=N&pMC_BYR_ACC_ID=' + data.MC_BYR_ACC_ID + '&pMC_BUYER_ID=' + data.MC_BUYER_ID + '&pMC_STYLE_H_ID=' + data.MC_STYLE_H_ID + '&pSTYLE_NO=' + data.STYLE_NO;
            var a = document.createElement('a');
            a.href = url;
            a.target = '_self';
            document.body.appendChild(a);
            a.click();
        }



    }

})();