(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TnaFollowupMatrixGridController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'CODE_TEXT','$modal', TnaFollowupMatrixGridController]);
    function TnaFollowupMatrixGridController($q, config, MrcDataService, $stateParams, $state, $scope, CODE_TEXT,$modal) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getbuyerAcList(), getTNA_CODE_FOR_HIDE(),getOrderShipmentMonth()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        $scope.CODE_TEXT = CODE_TEXT;
        $scope.template = '<span><h5 style="padding:0px;margin:0px;">#: data.ORDER_NO #</h5><p> Style: #: data.STYLE_NO #</p></span>';

        function getbuyerAcList() {
            return vm.buyerAcList = {
                optionLabel: "--- Buyer A/C ---",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            return MrcDataService.getDataByUrl('/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataBound: function (e) {
                    var ds = this.dataSource.data();
                    if (ds.length == 1) {
                        vm.onSearch({ MC_BYR_ACC_ID: ds[0].MC_BYR_ACC_ID });
                        onBuyerAccChange(ds[0].MC_BYR_ACC_ID);
                    }
                },
                change: function (e) {
                    var item = this.dataItem(e.item);
                    onBuyerAccChange((item.MC_BYR_ACC_ID||null));
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID"
            };
        }


        vm.oderListDs = {
            transport: {
                read: function (e) {
                    var url = "/api/common/getOrderStyleDropDownData?pMC_BYR_ACC_ID";
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);

                    if (params.filter) {
                        url += '&pORDER_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                    } else {
                        url += '&pORDER_NO';
                    }

                    MrcDataService.getDataByFullUrl(url).then(function (res) {
                        e.success(res);
                    });
                }
            },
            serverFiltering: true
        };


        function onBuyerAccChange(MC_BYR_ACC_ID, FIRSTDATE,LASTDATE) {
                vm.oderListDs = {
                    transport: {
                        read: function (e) {
                            var url = "/api/common/getOrderStyleDropDownData?pMC_BYR_ACC_ID=" + (MC_BYR_ACC_ID||null);
                            var webapi = new kendo.data.transports.webapi({});
                            var params = webapi.parameterMap(e.data);

                            if (params.filter) {
                                url += '&pORDER_NO=' + params.filter.replace(/'/g, '').split('~')[2];
                            } else {
                                url += '&pORDER_NO';
                            }

                                url+= '&pOption=3002'
                            if (FIRSTDATE && LASTDATE) {
                                url += '&pFIRSTDATE=' + FIRSTDATE;
                                url += '&pLASTDATE=' +  LASTDATE;
                            }

                             MrcDataService.getDataByFullUrl(url).then(function (res) {
                                e.success(res);
                            });
                        }
                    },
                    serverFiltering: true
                };

           
        };

        function getOrderShipmentMonth(pMC_BYR_ACC_ID) {
            return MrcDataService.getDataByFullUrl('/api/mrc/Order/OrderShipmentMonth?pMC_BYR_ACC_ID=' + (pMC_BYR_ACC_ID || null)).then(function (res) {
                vm.shipMonthList = new kendo.data.DataSource({
                    data: res
                })
            });
        }


        vm.onChangeShipMonth = function (data, e) {
            var item = e.sender.dataItem(e.sender.item);
            if (item.hasOwnProperty('MONTHOF') && item.MONTHOF) {
                data['FIRSTDATE'] = item.FIRSTDATE;
                data['LASTDATE'] = item.LASTDATE;
            } else {
                vm.form['FIRSTDATE'] = null;
                vm.form['LASTDATE'] = null;
            }
        };


        function resetColumns(COLS) {
            var res_col = [{ field: "GRP_ORDER", hidden: true, title: "Style #", width: "100px", groupHeaderTemplate: " <b>  #= value # </b>" },
                { title: "Step", field: 'TEXT', width: "300px", filterable: true, locked: true }];
            angular.forEach(COLS, function (key) {
                if (key.substring(0, 3) == 'TNA') {
                    res_col.push(
                            {
                                headerTemplate: "{{CODE_TEXT['" + key + "']}}",
                                field: key,
                                width: "100px",
                                filterable: false,
                                template: "# if( SE_TEXT=='PA' && TNA_001 && kendo.parseInt(" + key + ")<0) {#<span style=\"display: block;background-color:salmon;\">#= " + key + " #<span># }# # if( SE_TEXT=='PA' && " + key + " && kendo.parseInt(" + key + ")>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= " + key + " #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= " + key + " #<span># }#"
                            }
                  );
                }


            });

            $("#tna_matrix_grid").data("kendoGrid").setOptions({
                columns: res_col
            });
        };


        vm.saveAs=function(type){
            var grid = $("#tna_matrix_grid").data("kendoGrid");
            if(type=='pdf')
                 grid.saveAsPDF();
            else if(type=='excel')
                grid.saveAsExcel();

        }

        function getTNA_CODE_FOR_HIDE() {
            return MrcDataService.getDataByFullUrl('/api/common/getTnAMatrixHideCode').then(function (res) {
                vm.tnaCodeHideDs = new kendo.data.DataSource({
                    data:res
                });
            });
        };

        vm.onChangeGroup = function (data,e) {
            var item = e.sender.dataItem(e.sender.item);

            if (item.ID == 2) {
                data['LK_ORD_TYPE_ID'] = 311;
            } else {
                data['LK_ORD_TYPE_ID'] = null;
            }

            if (item.VALUES) {
                resetColumns(item.VALUES)
            } else {
                resetColumns(Object.keys(CODE_TEXT));
            }

        }


        vm.onSearch = function (form) {
            vm.dataSource = new kendo.data.DataSource({
                serverPaging: true,
                serverFiltering: true,
                serverSorting: false,
                schema: {
                    data: 'data',
                    total: 'total',
                    model: {
                        fields: {
                            ORD_CONF_DT: { type: 'date' },
                            SHIP_DT: { type: 'date' }
                        }
                    }
                },
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);

                        var url = '/api/common/getTnAMatrixGridData';
                        url += '?pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                        url += '&pMC_BYR_ACC_ID=' + (form && form.MC_BYR_ACC_ID ? form.MC_BYR_ACC_ID : null);
                        url += '&pLK_ORD_TYPE_ID=' + (form && form.LK_ORD_TYPE_ID ? form.LK_ORD_TYPE_ID : null);

                        url += '&pMC_ORDER_H_ID_LST=' + (form && form.MC_ORDER_H_ID_LST ? form.MC_ORDER_H_ID_LST : '');

                        if (form.FIRSTDATE && form.LASTDATE) {
                            url += '&pFIRSTDATE=' + form.FIRSTDATE;
                            url += '&pLASTDATE=' + form.LASTDATE;
                        }

                        return MrcDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        })


                    }
                },
                pageSize: 5,
                sort: { field: "IDX", dir: "asc", compare: function (a, b, dir) { return a.IDX - b.IDX; } },

                group: [{ field: 'GRP_ORDER' }]
            });
        };




        function openTnaUpdateModal(data, code) {
            return MrcDataService.getDataByFullUrl('/api/common/OrderTnATaskByCode?pMC_ORDER_H_ID=' + data.MC_ORDER_H_ID + '&pTA_TASK_CODE=' + code).then(function (res) {
                if (res.MC_ORD_TNA_ID == 0) {
                    return;
                } 

                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: '/Merchandising/Mrc/_UpdateTnAModal',
                    controller: 'TnaUpdateModalController',
                    size: 'small',
                    windowClass: 'large-Modal',
                    resolve: {
                        Order: function () {
                            return data;
                        },
                        Task: function () {
                            return res
                        }
                    }
                });

                modalInstance.result.then(function (dt) {
                    if (dt && dt != 1) {
                        data.set(code, dt);
                    }
                });
            });
        };





        vm.options = {
            toolbar: kendo.template($("#TnAFollowupMatrixToolbarTemplate").html()),
            autoBind: true,
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
            selectable: "cell",
            sortable: false,
            pageable: true,
            height: 650,
            pdf: {
                fileName: "TnA Followup.pdf",
            },
            pdfExport: function(e) {
                e.promise
                .progress(function(e) {
                    console.log(kendo.format("{0:P} complete", e.progress));
                })
                .done(function() {
                    
                });
            },
            change: function (e) {

                var cell = this.select();
                var cellIndex = cell[0].cellIndex;
                var column = this.columns[cellIndex+1];
                var dataItem = this.dataItem(cell.closest("tr"));
                var val = dataItem[column.field];

                var AllowedCode = _.find(vm.tnaCodeHideDs.data(), function (o) {
                    return o.ID == 5;

                });

                if (AllowedCode && AllowedCode.VALUES.length > 0) {
                    var idx = AllowedCode.VALUES.indexOf(column.field);
                    if (dataItem.IS_P_A == 'A' && !val && idx>-1) {
                        return openTnaUpdateModal(dataItem, column.field);
                    }
                }




            },

            columns: [
                { field: "GRP_ORDER", hidden: true, title: "Style #", width: "100px", groupHeaderTemplate: " <b>  #= value # </b>" },
                {
                    title: "Style/Order #",
                    field: 'TEXT',
                    width: "300px",
                    filterable: true,
                    locked:true,
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_001']}}",
                    field: 'TNA_001',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_001 && kendo.parseInt(TNA_001)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_001 #<span># }# # if( SE_TEXT=='PA' && TNA_001 && kendo.parseInt(TNA_001)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_001 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_001 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_002']}}",
                    field: 'TNA_002',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_002 && kendo.parseInt(TNA_002)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_002 #<span># }# # if( SE_TEXT=='PA' && TNA_002 && kendo.parseInt(TNA_002)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_002 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_002 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_003']}}",
                    field: 'TNA_003',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_003 && kendo.parseInt(TNA_003)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_003 #<span># }# # if( SE_TEXT=='PA' && TNA_003 && kendo.parseInt(TNA_003)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_003 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_003 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_004']}}",
                    field: 'TNA_004',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_004 && kendo.parseInt(TNA_004)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_004 #<span># }# # if( SE_TEXT=='PA' && TNA_004 && kendo.parseInt(TNA_004)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_004 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_004 #<span># }#"

                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_005']}}",
                    field: 'TNA_005',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_005 && kendo.parseInt(TNA_005)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_005 #<span># }# # if( SE_TEXT=='PA' && TNA_005 && kendo.parseInt(TNA_005)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_005 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_005 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_006']}}",
                    field: 'TNA_006',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_006 && kendo.parseInt(TNA_006)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_006 #<span># }# # if( SE_TEXT=='PA' && TNA_006 && kendo.parseInt(TNA_006)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_006 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_006 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_007']}}",
                    field: 'TNA_007',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_007 && kendo.parseInt(TNA_007)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_007 #<span># }# # if( SE_TEXT=='PA' && TNA_007 && kendo.parseInt(TNA_007)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_007 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_007 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_008']}}",
                    field: 'TNA_008',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_008 && kendo.parseInt(TNA_008)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_008 #<span># }# # if( SE_TEXT=='PA' && TNA_008 && kendo.parseInt(TNA_008)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_008 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_008 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_009']}}",
                    field: 'TNA_009',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_009 && kendo.parseInt(TNA_009)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_009 #<span># }# # if( SE_TEXT=='PA' && TNA_009 && kendo.parseInt(TNA_009)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_009 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_009 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_010']}}",
                    field: 'TNA_010',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_010 && kendo.parseInt(TNA_010)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_010 #<span># }# # if( SE_TEXT=='PA' && TNA_010 && kendo.parseInt(TNA_010)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_010 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_010 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_011']}}",
                    field: 'TNA_011',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_011 && kendo.parseInt(TNA_011)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_011 #<span># }# # if( SE_TEXT=='PA' && TNA_011 && kendo.parseInt(TNA_011)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_011 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_011 #<span># }#"
                },

                {
                    headerTemplate: "{{CODE_TEXT['TNA_012']}}",
                    field: 'TNA_012',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_012 && kendo.parseInt(TNA_012)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_012 #<span># }# # if( SE_TEXT=='PA' && TNA_012 && kendo.parseInt(TNA_012)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_012 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_012 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_013']}}",
                    field: 'TNA_013',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_013 && kendo.parseInt(TNA_013)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_013 #<span># }# # if( SE_TEXT=='PA' && TNA_013 && kendo.parseInt(TNA_013)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_013 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_013 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_014']}}",
                    field: 'TNA_014',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_014 && kendo.parseInt(TNA_014)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_014 #<span># }# # if( SE_TEXT=='PA' && TNA_014 && kendo.parseInt(TNA_014)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_014 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_014 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_015']}}",
                    field: 'TNA_015',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_015 && kendo.parseInt(TNA_015)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_015 #<span># }# # if( SE_TEXT=='PA' && TNA_015 && kendo.parseInt(TNA_015)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_015 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_015 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_016']}}",
                    field: 'TNA_016',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_016 && kendo.parseInt(TNA_016)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_016 #<span># }# # if( SE_TEXT=='PA' && TNA_016 && kendo.parseInt(TNA_016)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_016 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_016 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_017']}}",
                    field: 'TNA_017',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_017 && kendo.parseInt(TNA_017)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_017 #<span># }# # if( SE_TEXT=='PA' && TNA_017 && kendo.parseInt(TNA_017)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_017 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_017 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_018']}}",
                    field: 'TNA_018',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_018 && kendo.parseInt(TNA_018)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_018 #<span># }# # if( SE_TEXT=='PA' && TNA_018 && kendo.parseInt(TNA_018)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_018 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_018 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_019']}}",
                    field: 'TNA_019',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_019 && kendo.parseInt(TNA_019)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_019 #<span># }# # if( SE_TEXT=='PA' && TNA_019 && kendo.parseInt(TNA_019)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_019 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_019 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_020']}}",
                    field: 'TNA_020',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_020 && kendo.parseInt(TNA_020)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_020 #<span># }# # if( SE_TEXT=='PA' && TNA_020 && kendo.parseInt(TNA_020)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_020 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_020 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_021']}}",
                    field: 'TNA_021',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_021 && kendo.parseInt(TNA_021)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_021 #<span># }# # if( SE_TEXT=='PA' && TNA_021 && kendo.parseInt(TNA_021)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_021 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_021 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_022']}}",
                    field: 'TNA_022',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_022 && kendo.parseInt(TNA_022)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_022 #<span># }# # if( SE_TEXT=='PA' && TNA_022 && kendo.parseInt(TNA_022)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_022 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_022 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_023']}}",
                    field: 'TNA_023',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_023 && kendo.parseInt(TNA_023)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_023 #<span># }# # if( SE_TEXT=='PA' && TNA_023 && kendo.parseInt(TNA_023)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_023 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_023 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_024']}}",
                    field: 'TNA_024',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_024 && kendo.parseInt(TNA_024)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_024 #<span># }# # if( SE_TEXT=='PA' && TNA_024 && kendo.parseInt(TNA_024)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_024 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_024 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_025']}}",
                    field: 'TNA_025',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_025 && kendo.parseInt(TNA_025)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_025 #<span># }# # if( SE_TEXT=='PA' && TNA_025 && kendo.parseInt(TNA_025)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_025 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_025 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_026']}}",
                    field: 'TNA_026',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_026 && kendo.parseInt(TNA_026)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_026 #<span># }# # if( SE_TEXT=='PA' && TNA_026 && kendo.parseInt(TNA_026)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_026 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_026 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_027']}}",
                    field: 'TNA_027',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_027 && kendo.parseInt(TNA_027)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_027 #<span># }# # if( SE_TEXT=='PA' && TNA_027 && kendo.parseInt(TNA_027)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_027 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_027 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_028']}}",
                    field: 'TNA_028',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_028 && kendo.parseInt(TNA_028)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_028 #<span># }# # if( SE_TEXT=='PA' && TNA_028 && kendo.parseInt(TNA_028)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_028 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_028 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_029']}}",
                    field: 'TNA_029',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_029 && kendo.parseInt(TNA_029)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_029 #<span># }# # if( SE_TEXT=='PA' && TNA_029 && kendo.parseInt(TNA_029)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_029 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_029 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_030']}}",
                    field: 'TNA_030',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_030 && kendo.parseInt(TNA_030)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_030 #<span># }# # if( SE_TEXT=='PA' && TNA_030 && kendo.parseInt(TNA_030)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_030 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_030 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_031']}}",
                    field: 'TNA_031',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_031 && kendo.parseInt(TNA_031)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_031 #<span># }# # if( SE_TEXT=='PA' && TNA_031 && kendo.parseInt(TNA_031)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_031 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_031 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_032']}}",
                    field: 'TNA_032',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_032 && kendo.parseInt(TNA_032)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_032 #<span># }# # if( SE_TEXT=='PA' && TNA_032 && kendo.parseInt(TNA_032)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_032 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_032 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_033']}}",
                    field: 'TNA_033',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_033 && kendo.parseInt(TNA_033)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_033 #<span># }# # if( SE_TEXT=='PA' && TNA_033 && kendo.parseInt(TNA_033)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_033 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_033 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_034']}}",
                    field: 'TNA_034',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_034 && kendo.parseInt(TNA_034)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_034 #<span># }# # if( SE_TEXT=='PA' && TNA_034 && kendo.parseInt(TNA_034)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_034 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_034 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_035']}}",
                    field: 'TNA_035',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_035 && kendo.parseInt(TNA_035)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_035 #<span># }# # if( SE_TEXT=='PA' && TNA_035 && kendo.parseInt(TNA_035)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_035 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_035 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_036']}}",
                    field: 'TNA_036',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_036 && kendo.parseInt(TNA_036)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_036 #<span># }# # if( SE_TEXT=='PA' && TNA_036 && kendo.parseInt(TNA_036)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_036 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_036 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_037']}}",
                    field: 'TNA_037',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_037 && kendo.parseInt(TNA_037)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_037 #<span># }# # if( SE_TEXT=='PA' && TNA_037 && kendo.parseInt(TNA_037)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_037 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_037 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_038']}}",
                    field: 'TNA_038',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_038 && kendo.parseInt(TNA_038)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_038 #<span># }# # if( SE_TEXT=='PA' && TNA_038 && kendo.parseInt(TNA_038)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_038 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_038 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_039']}}",
                    field: 'TNA_039',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_039 && kendo.parseInt(TNA_039)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_039 #<span># }# # if( SE_TEXT=='PA' && TNA_039 && kendo.parseInt(TNA_039)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_039 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_039 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_040']}}",
                    field: 'TNA_040',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_040 && kendo.parseInt(TNA_040)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_040 #<span># }# # if( SE_TEXT=='PA' && TNA_040 && kendo.parseInt(TNA_040)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_040 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_040 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_041']}}",
                    field: 'TNA_041',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_041 && kendo.parseInt(TNA_041)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_041 #<span># }# # if( SE_TEXT=='PA' && TNA_041 && kendo.parseInt(TNA_041)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_041 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_041 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_042']}}",
                    field: 'TNA_042',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_042 && kendo.parseInt(TNA_042)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_042 #<span># }# # if( SE_TEXT=='PA' && TNA_042 && kendo.parseInt(TNA_042)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_042 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_042 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_043']}}",
                    field: 'TNA_043',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_043 && kendo.parseInt(TNA_043)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_043 #<span># }# # if( SE_TEXT=='PA' && TNA_043 && kendo.parseInt(TNA_043)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_043 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_043 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_044']}}",
                    field: 'TNA_044',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_044 && kendo.parseInt(TNA_044)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_044 #<span># }# # if( SE_TEXT=='PA' && TNA_044 && kendo.parseInt(TNA_044)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_044 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_044 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_045']}}",
                    field: 'TNA_045',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_045 && kendo.parseInt(TNA_045)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_045 #<span># }# # if( SE_TEXT=='PA' && TNA_045 && kendo.parseInt(TNA_045)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_045 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_045 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_046']}}",
                    field: 'TNA_046',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_046 && kendo.parseInt(TNA_046)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_046 #<span># }# # if( SE_TEXT=='PA' && TNA_046 && kendo.parseInt(TNA_046)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_046 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_046 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_047']}}",
                    field: 'TNA_047',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_047 && kendo.parseInt(TNA_047)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_047 #<span># }# # if( SE_TEXT=='PA' && TNA_047 && kendo.parseInt(TNA_047)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_047 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_047 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_048']}}",
                    field: 'TNA_048',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_048 && kendo.parseInt(TNA_048)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_048 #<span># }# # if( SE_TEXT=='PA' && TNA_048 && kendo.parseInt(TNA_048)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_048 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_048 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_049']}}",
                    field: 'TNA_049',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_049 && kendo.parseInt(TNA_049)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_049 #<span># }# # if( SE_TEXT=='PA' && TNA_049 && kendo.parseInt(TNA_049)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_049 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_049 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_050']}}",
                    field: 'TNA_050',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_050 && kendo.parseInt(TNA_050)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_050 #<span># }# # if( SE_TEXT=='PA' && TNA_050 && kendo.parseInt(TNA_050)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_050 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_050 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_051']}}",
                    field: 'TNA_051',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_051 && kendo.parseInt(TNA_051)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_051 #<span># }# # if( SE_TEXT=='PA' && TNA_051 && kendo.parseInt(TNA_051)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_051 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_051 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_052']}}",
                    field: 'TNA_052',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_052 && kendo.parseInt(TNA_052)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_052 #<span># }# # if( SE_TEXT=='PA' && TNA_052 && kendo.parseInt(TNA_052)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_052 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_052 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_053']}}",
                    field: 'TNA_053',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_053 && kendo.parseInt(TNA_053)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_053 #<span># }# # if( SE_TEXT=='PA' && TNA_053 && kendo.parseInt(TNA_053)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_053 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_053 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_054']}}",
                    field: 'TNA_054',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_054 && kendo.parseInt(TNA_054)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_054 #<span># }# # if( SE_TEXT=='PA' && TNA_054 && kendo.parseInt(TNA_054)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_054 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_054 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_055']}}",
                    field: 'TNA_055',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_055 && kendo.parseInt(TNA_055)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_055 #<span># }# # if( SE_TEXT=='PA' && TNA_055 && kendo.parseInt(TNA_055)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_055 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_055 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_056']}}",
                    field: 'TNA_056',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_056 && kendo.parseInt(TNA_056)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_056 #<span># }# # if( SE_TEXT=='PA' && TNA_056 && kendo.parseInt(TNA_056)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_056 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_056 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_057']}}",
                    field: 'TNA_057',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_057 && kendo.parseInt(TNA_057)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_057 #<span># }# # if( SE_TEXT=='PA' && TNA_057 && kendo.parseInt(TNA_057)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_057 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_057 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_058']}}",
                    field: 'TNA_058',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_058 && kendo.parseInt(TNA_058)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_058 #<span># }# # if( SE_TEXT=='PA' && TNA_058 && kendo.parseInt(TNA_058)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_058 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_058 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_059']}}",
                    field: 'TNA_059',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_059 && kendo.parseInt(TNA_059)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_059 #<span># }# # if( SE_TEXT=='PA' && TNA_059 && kendo.parseInt(TNA_059)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_059 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_059 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_060']}}",
                    field: 'TNA_060',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_060 && kendo.parseInt(TNA_060)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_060 #<span># }# # if( SE_TEXT=='PA' && TNA_060 && kendo.parseInt(TNA_060)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_060 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_060 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_061']}}",
                    field: 'TNA_061',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_061 && kendo.parseInt(TNA_061)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_061 #<span># }# # if( SE_TEXT=='PA' && TNA_061 && kendo.parseInt(TNA_061)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_061 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_061 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_062']}}",
                    field: 'TNA_062',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_062 && kendo.parseInt(TNA_062)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_062 #<span># }# # if( SE_TEXT=='PA' && TNA_062 && kendo.parseInt(TNA_062)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_062 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_062 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_063']}}",
                    field: 'TNA_063',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_063 && kendo.parseInt(TNA_063)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_063 #<span># }# # if( SE_TEXT=='PA' && TNA_063 && kendo.parseInt(TNA_063)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_063 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_063 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_064']}}",
                    field: 'TNA_064',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_064 && kendo.parseInt(TNA_064)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_064 #<span># }# # if( SE_TEXT=='PA' && TNA_064 && kendo.parseInt(TNA_064)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_064 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_064 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_065']}}",
                    field: 'TNA_065',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_065 && kendo.parseInt(TNA_065)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_065 #<span># }# # if( SE_TEXT=='PA' && TNA_065 && kendo.parseInt(TNA_065)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_065 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_065 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_066']}}",
                    field: 'TNA_066',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_066 && kendo.parseInt(TNA_066)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_066 #<span># }# # if( SE_TEXT=='PA' && TNA_066 && kendo.parseInt(TNA_066)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_066 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_066 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_067']}}",
                    field: 'TNA_067',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_067 && kendo.parseInt(TNA_067)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_067 #<span># }# # if( SE_TEXT=='PA' && TNA_067 && kendo.parseInt(TNA_067)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_067 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_067 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_068']}}",
                    field: 'TNA_068',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_068 && kendo.parseInt(TNA_068)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_068 #<span># }# # if( SE_TEXT=='PA' && TNA_068 && kendo.parseInt(TNA_068)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_068 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_068 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_069']}}",
                    field: 'TNA_069',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_069 && kendo.parseInt(TNA_069)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_069 #<span># }# # if( SE_TEXT=='PA' && TNA_069 && kendo.parseInt(TNA_069)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_069 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_069 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_070']}}",
                    field: 'TNA_070',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_070 && kendo.parseInt(TNA_070)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_070 #<span># }# # if( SE_TEXT=='PA' && TNA_070 && kendo.parseInt(TNA_070)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_070 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_070 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_071']}}",
                    field: 'TNA_071',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_071 && kendo.parseInt(TNA_071)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_071 #<span># }# # if( SE_TEXT=='PA' && TNA_071 && kendo.parseInt(TNA_071)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_071 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_071 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_072']}}",
                    field: 'TNA_072',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_072 && kendo.parseInt(TNA_072)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_072 #<span># }# # if( SE_TEXT=='PA' && TNA_072 && kendo.parseInt(TNA_072)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_072 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_072 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_073']}}",
                    field: 'TNA_073',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_073 && kendo.parseInt(TNA_073)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_073 #<span># }# # if( SE_TEXT=='PA' && TNA_073 && kendo.parseInt(TNA_073)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_073 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_073 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_074']}}",
                    field: 'TNA_074',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_074 && kendo.parseInt(TNA_074)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_074 #<span># }# # if( SE_TEXT=='PA' && TNA_074 && kendo.parseInt(TNA_074)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_074 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_074 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_075']}}",
                    field: 'TNA_075',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_075 && kendo.parseInt(TNA_075)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_075 #<span># }# # if( SE_TEXT=='PA' && TNA_075 && kendo.parseInt(TNA_075)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_075 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_075 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_076']}}",
                    field: 'TNA_076',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_076 && kendo.parseInt(TNA_076)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_076 #<span># }# # if( SE_TEXT=='PA' && TNA_076 && kendo.parseInt(TNA_076)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_076 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_076 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_078']}}",
                    field: 'TNA_078',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_078 && kendo.parseInt(TNA_078)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_078 #<span># }# # if( SE_TEXT=='PA' && TNA_078 && kendo.parseInt(TNA_078)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_078 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_078 #<span># }#"
                },
                {
                    headerTemplate: "{{CODE_TEXT['TNA_079']}}",
                    field: 'TNA_079',
                    width: "100px",
                    filterable: false,
                    template: "# if( SE_TEXT=='PA' && TNA_079 && kendo.parseInt(TNA_079)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_079 #<span># }# # if( SE_TEXT=='PA' && TNA_079 && kendo.parseInt(TNA_079)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_079 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_079 #<span># }#"
                },

                 {
                     headerTemplate: "{{CODE_TEXT['TNA_080']}}",
                     field: 'TNA_080',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_080 && kendo.parseInt(TNA_080)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_080 #<span># }# # if( SE_TEXT=='PA' && TNA_080 && kendo.parseInt(TNA_080)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_080 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_080 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_081']}}",
                     field: 'TNA_081',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_081 && kendo.parseInt(TNA_081)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_081 #<span># }# # if( SE_TEXT=='PA' && TNA_081 && kendo.parseInt(TNA_081)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_081 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_081 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_082']}}",
                     field: 'TNA_082',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_082 && kendo.parseInt(TNA_082)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_082 #<span># }# # if( SE_TEXT=='PA' && TNA_082 && kendo.parseInt(TNA_082)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_082 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_082 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_083']}}",
                     field: 'TNA_083',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_083 && kendo.parseInt(TNA_083)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_083 #<span># }# # if( SE_TEXT=='PA' && TNA_083 && kendo.parseInt(TNA_083)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_083 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_083 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_084']}}",
                     field: 'TNA_084',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_084 && kendo.parseInt(TNA_084)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_084 #<span># }# # if( SE_TEXT=='PA' && TNA_084 && kendo.parseInt(TNA_084)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_084 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_084 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_085']}}",
                     field: 'TNA_085',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_085 && kendo.parseInt(TNA_085)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_085 #<span># }# # if( SE_TEXT=='PA' && TNA_085 && kendo.parseInt(TNA_085)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_085 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_085 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_086']}}",
                     field: 'TNA_086',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_086 && kendo.parseInt(TNA_086)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_086 #<span># }# # if( SE_TEXT=='PA' && TNA_086 && kendo.parseInt(TNA_086)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_086 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_086 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_087']}}",
                     field: 'TNA_087',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_087 && kendo.parseInt(TNA_087)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_087 #<span># }# # if( SE_TEXT=='PA' && TNA_087 && kendo.parseInt(TNA_087)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_087 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_087 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_088']}}",
                     field: 'TNA_088',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_088 && kendo.parseInt(TNA_088)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_088 #<span># }# # if( SE_TEXT=='PA' && TNA_088 && kendo.parseInt(TNA_088)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_088 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_088 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_089']}}",
                     field: 'TNA_089',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_089 && kendo.parseInt(TNA_089)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_089 #<span># }# # if( SE_TEXT=='PA' && TNA_089 && kendo.parseInt(TNA_089)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_089 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_089 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_090']}}",
                     field: 'TNA_090',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_090 && kendo.parseInt(TNA_090)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_090 #<span># }# # if( SE_TEXT=='PA' && TNA_090 && kendo.parseInt(TNA_090)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_090 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_090 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_091']}}",
                     field: 'TNA_091',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_091 && kendo.parseInt(TNA_091)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_091 #<span># }# # if( SE_TEXT=='PA' && TNA_091 && kendo.parseInt(TNA_091)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_091 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_091 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_092']}}",
                     field: 'TNA_092',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_092 && kendo.parseInt(TNA_092)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_092 #<span># }# # if( SE_TEXT=='PA' && TNA_092 && kendo.parseInt(TNA_092)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_092 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_092 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_093']}}",
                     field: 'TNA_093',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_093 && kendo.parseInt(TNA_093)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_093 #<span># }# # if( SE_TEXT=='PA' && TNA_093 && kendo.parseInt(TNA_093)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_093 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_093 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_094']}}",
                     field: 'TNA_094',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_094 && kendo.parseInt(TNA_094)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_094 #<span># }# # if( SE_TEXT=='PA' && TNA_094 && kendo.parseInt(TNA_094)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_094 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_094 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_095']}}",
                     field: 'TNA_095',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_095 && kendo.parseInt(TNA_095)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_095 #<span># }# # if( SE_TEXT=='PA' && TNA_095 && kendo.parseInt(TNA_095)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_095 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_095 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_096']}}",
                     field: 'TNA_096',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_096 && kendo.parseInt(TNA_096)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_096 #<span># }# # if( SE_TEXT=='PA' && TNA_096 && kendo.parseInt(TNA_096)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_096 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_096 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_097']}}",
                     field: 'TNA_097',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_097 && kendo.parseInt(TNA_097)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_097 #<span># }# # if( SE_TEXT=='PA' && TNA_097 && kendo.parseInt(TNA_097)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_097 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_097 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_098']}}",
                     field: 'TNA_098',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_098 && kendo.parseInt(TNA_098)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_098 #<span># }# # if( SE_TEXT=='PA' && TNA_098 && kendo.parseInt(TNA_098)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_098 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_098 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_099']}}",
                     field: 'TNA_099',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_099 && kendo.parseInt(TNA_099)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_099 #<span># }# # if( SE_TEXT=='PA' && TNA_099 && kendo.parseInt(TNA_099)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_099 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_099 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_100']}}",
                     field: 'TNA_100',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_100 && kendo.parseInt(TNA_100)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_100 #<span># }# # if( SE_TEXT=='PA' && TNA_100 && kendo.parseInt(TNA_100)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_100 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_100 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_101']}}",
                     field: 'TNA_101',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_101 && kendo.parseInt(TNA_101)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_101 #<span># }# # if( SE_TEXT=='PA' && TNA_101 && kendo.parseInt(TNA_101)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_101 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_101 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_102']}}",
                     field: 'TNA_102',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_102 && kendo.parseInt(TNA_102)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_102 #<span># }# # if( SE_TEXT=='PA' && TNA_102 && kendo.parseInt(TNA_102)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_102 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_102 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_103']}}",
                     field: 'TNA_103',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_103 && kendo.parseInt(TNA_103)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_103 #<span># }# # if( SE_TEXT=='PA' && TNA_103 && kendo.parseInt(TNA_103)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_103 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_103 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_004']}}",
                     field: 'TNA_004',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_004 && kendo.parseInt(TNA_004)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_004 #<span># }# # if( SE_TEXT=='PA' && TNA_004 && kendo.parseInt(TNA_004)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_004 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_004 #<span># }#"
                 },
                 {
                     headerTemplate: "{{CODE_TEXT['TNA_105']}}",
                     field: 'TNA_105',
                     width: "100px",
                     filterable: false,
                     template: "# if( SE_TEXT=='PA' && TNA_105 && kendo.parseInt(TNA_105)<0) {#<span style=\"display: block;background-color:salmon;\">#= TNA_105 #<span># }# # if( SE_TEXT=='PA' && TNA_105 && kendo.parseInt(TNA_105)>=0) {#<span style=\"display: block;background-color:LightGreen;\">#= TNA_105 #<span># }# # if ( SE_TEXT!='PA'){#<span style=\"display: block;\">#= TNA_105 #<span># }#"
                 }
            ]
        };

    }



})();