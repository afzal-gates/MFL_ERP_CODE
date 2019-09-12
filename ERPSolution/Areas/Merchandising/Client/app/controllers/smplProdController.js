////////// Start Header Controller
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('SmplProdController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', 'formData', '$state', '$stateParams', '$modal', 'MrcDataService', 'Dialog', SmplProdController]);
    function SmplProdController(logger, config, $q, $scope, $http, exception, $filter, formData, $state, $stateParams, $modal, MrcDataService, Dialog) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        console.log($stateParams);

        vm.printButtonList = [{ BTN_ID: 1, BTN_NAME: 'Production Report' }, { BTN_ID: 2, BTN_NAME: 'Sample Card' }];
        vm.smplCardFormatList = [{ SMPL_FMT_ID: 1, SMPL_FMT_NAME: 'Lindex' }, { SMPL_FMT_ID: 2, SMPL_FMT_NAME: 'Other' }];

        var spDate = $stateParams.pPROD_DT == '1900-Jan-01' ? moment(vm.today).format("DD-MMM-YYYY") : moment($stateParams.pPROD_DT).format("DD-MMM-YYYY");

        vm.IS_PROG_PARAM = 'Y';
        vm.IS_NEXT = 'Y';
        var key = 'MC_SMP_REQ_H_ID';
        
        vm.form = { PROD_DT: spDate, SMPL_FMT_ID: 2 };
        

        vm.batchList = [];
        if (vm.batchList.length < 1) {
            MrcDataService.GetAllOthers('/api/mrc/SampleReq/getSmplProdBatchListByDate?&pPROD_DT=' + spDate).then(function (res) {
                vm.batchList = res;

                if (res.length > 0) {
                    var maxBatch = _.maxBy(res, function (ob) {
                        return ob.PROD_BATCH_NO;
                    });
                    console.log(maxBatch);

                    vm.form.PROD_BATCH_NO = maxBatch.PROD_BATCH_NO;//_.max(res);
                    vm.form.PROD_DT = spDate;
                    vm.form.DTL_XML = [];
                }
                else {
                    if ($stateParams.pPROD_DT == "1900-Jan-01") {
                        vm.form.PROD_DT = vm.today;
                        vm.form.PROD_BATCH_NO = 1;
                        vm.form.DTL_XML = [];
                    }
                    else {
                        //var spDate = moment($stateParams.pPROD_DT).format("DD-MMM-YYYY"); //$filter('date')(moment($stateParams.pPROD_DT, "YYYY-MMM-DD"), vm.dtFormat);            
                        var spBatch = parseInt($stateParams.pPROD_BATCH_NO);
                        console.log(spBatch);
                        vm.form.PROD_DT = spDate;
                        vm.form.PROD_BATCH_NO = spBatch;
                        vm.form.DTL_XML = [];
                    }
                }

            }, function (err) {
                console.log(err);
            });
        }
        
        
        $scope.HAS_DATA_PROD_GRID = 'N';
        $scope.IS_PROD_SAVE = 'N';

        $scope.form = vm.form;
        $scope.form.PROD_DT = $filter('date')(vm.form.PROD_DT, vm.dtFormat);
        $scope.sampBatchProd = [];
        
        vm.errstyle = { 'border': '1px solid #f13d3d' };

        activate();
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                //vm.form.MC_BYR_ACC_ID = $stateParams.pMC_BYR_ACC_ID;
                //vm.showBooking();

                vm.showSplash = false;                
            });
        }

        vm.sfbProdDateOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.sfbProdDateOpened = true;
        };


        vm.print = function (itm) {
            var rptCode;
            if (itm.BTN_ID == 1) {
                rptCode = 'RPT-2006';
            }
            else if (itm.BTN_ID == 2) {
                rptCode = 'RPT-2007';
            }

            var data = {
                SMPL_FMT_ID: vm.form.SMPL_FMT_ID,
                FROM_DATE: vm.form.PROD_DT
            };
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: rptCode }, data);

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

        vm.printRow1 = function (itm) {
            console.log(itm);

            var rptCode = 'RPT-2007';
            
            var data = {
                SMPL_FMT_ID: vm.form.SMPL_FMT_ID,
                MC_SMP_PROD_ID: itm.MC_SMP_PROD_ID,
                NO_OF_DELV_QTY_PRINT: itm.NO_OF_DELV_QTY_PRINT,
                FROM_DATE: vm.form.PROD_DT
            };
            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = angular.extend({ REPORT_CODE: rptCode }, data);

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

        vm.printRow = function (itm, smplFmtId) {

            itm['SMPL_FMT_ID'] = smplFmtId;

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'DelvQtyModal.html',
                controller: function (formData, $modalInstance, $scope) {
                    var vm = this;
                    vm.form = formData;
                    vm.form['NO_OF_DELV_QTY_PRINT'] = formData['DELV_QTY'];

                    $scope.cancel = function () {
                        var rptCode = 'RPT-2007';

                        var data = {
                            SMPL_FMT_ID: vm.form.SMPL_FMT_ID,
                            MC_SMP_PROD_ID: vm.form.MC_SMP_PROD_ID,
                            NO_OF_DELV_QTY_PRINT: vm.form.NO_OF_DELV_QTY_PRINT,
                            SMPL_REV_NO: vm.form.SMPL_REV_NO,
                            SMPL_OPT_NO: vm.form.SMPL_OPT_NO,
                            SMPL_ACT_FAB_GSM: vm.form.SMPL_ACT_FAB_GSM,
                            FROM_DATE: vm.form.PROD_DT
                        };
                        var form = document.createElement("form");
                        form.setAttribute("method", "post");
                        form.setAttribute("action", '/api/mrc/MrcReport/PreviewReport');
                        form.setAttribute("target", '_blank');

                        var params = angular.extend({ REPORT_CODE: rptCode }, data);

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

                        $modalInstance.close(data);
                    };

                },
                controllerAs: 'vm',
                //size: '500px',
                windowClass: 'large-Modal',
                resolve: {
                    formData: function () {                        
                        return itm;
                    }
                }
            });

            modalInstance.result.then(function (result) {
                console.log(result);
                
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }


        var bookingDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    return MrcDataService.GetAllOthers('/api/mrc/SampleReq/getSmpTypList4Prod').then(function (res) {
                        e.success(res);                                               
                    });                    
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            schema: {
                model: {
                    fields: {
                        SMP_REQ_DT: { type: "date" }
                    }
                }
            },
            group: [{ field: 'BYR_ACC_NAME_EN' }],
            sort: [{ field: "SMP_REQ_DT", dir: "desc" }],
            //pageSize: 10
        });

        function collapseAllGroups(grid) {
            grid.table.find(".k-grouping-row").each(function () {
                grid.collapseGroup(this);
            });
        }

        vm.gridOptionsBooking = {
            autoBind: true,
            height: '480px',
            scrollable: true,
            navigatable: true,
            dataSource: bookingDataSource,
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
            dataBound: function (e) {
                collapseAllGroups(this);
            },
            selectable: "row",
            sortable: true,
            //pageSize: 10,
            //pageable: true,
            refresh: true,       
            columns: [
                //{
                //    title: "Action",
                //    template: function () {
                //        return '<button class="btn btn-xs green" title="Select" ng-click="vm.selectSmpType(dataItem)"><i class="fa fa-hand-o-left"></i></button>';
                //    },
                //    width: "30px"
                //},
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "150px", hidden: true },                
                { field: "STYLE_NO", title: "Work Style", type: "string", width: "150px" },                                                
                { field: "SMPL_TYPE_NAME", title: "Smp. Type", type: "string", width: "200px" },
                { field: "SMP_REQ_DT", title: "Date", type: "date", width: "100px", format: "{0:" + vm.dtFormat + "}" },
                { field: "SMP_REQ_NO", title: "Program#", type: "date", width: "100px" }//,
                //{
                //    title: "S.Qty",
                //    template: function () {
                //        return "<input type='number' class='form-control' />";
                //    },
                //    width: "40px"
                //},
            ]            
        };
        
        vm.selectSmpType = function (dataItem) {
            //vm.showSplash = 'Y';

            vm.form.MC_SMP_REQ_H_ID = dataItem.MC_SMP_REQ_H_ID;
            vm.form.MC_BYR_ACC_ID = dataItem.MC_BYR_ACC_ID;
            vm.form.SMP_REQ_NO = dataItem.SMP_REQ_NO;
            vm.form.MC_STYLE_H_EXT_ID = dataItem.MC_STYLE_H_EXT_ID;
            vm.form.RF_SMPL_TYPE_ID = dataItem.RF_SMPL_TYPE_ID;

            //vm.IS_NEXT = 'N';
            vm.next();
        };

        //vm.smpTypeDataSource = new kendo.data.DataSource({
        //    batch: true,
        //    transport: {
        //        read: function (e) {
        //            MrcDataService.GetAllOthers('/api/mrc/buyer/SampleListByBuyerID/' + vm.MC_BUYER_ID).then(function (res) {
        //                e.success(res);
        //            }, function (err) {
        //                console.log(err);
        //            });

        //        },
        //        parameterMap: function (options, operation) {
        //            if (operation !== "read" && options.models) {
        //                return { models: kendo.stringify(options.models) };
        //            }
        //        }
        //    }
        //});

        $scope.smpProdGridOption = {
            height: 220,
            sortable: true,
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
                { field: "MC_SMP_PROD_ID", title: "Prod ID", type: "string", hidden: true },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "10%", editable: false, footerTemplate: "Total Record: #=count#" },
                { field: "STYLE_NO", title: "Work Style", type: "string", width: "10%", editable: false },
                { field: "SMPL_TYPE_NAME", title: "Smp. Type", type: "string", width: "10%", editable: false },
                { field: "ITEM_SNAME", title: "Item", type: "string", width: "20%", editable: false },
                { field: "COLOR_NAME_EN", title: "Color", type: "string", width: "10%", editable: false },
                { field: "SIZE_CODE", title: "Size", type: "string", width: "5%", filterable: false, editable: false },
                { field: "RQD_QTY", title: "R.Qty", type: "string", width: "5%", filterable: false, editable: false },
                { field: "MOU_CODE", title: "Unit", type: "string", width: "4%", filterable: false, editable: false },
                {
                    filterable: false,
                    footerTemplate: "#=sum#",
                    field: "SEW_QTY",
                    title: "S.Qty",                    
                    template: function () {
                        return "<input type='number' class='form-control' ng-model='dataItem.SEW_QTY' ng-disabled='dataItem.IS_SMP_FINALIZED==\"Y\"' min='0' ng-change='vm.onChangeSewQtyProd(dataItem)' />";
                    },
                    width: "5%"
                },
                {
                    filterable: false,
                    footerTemplate: "#=sum#",
                    field: "DELV_QTY",
                    title: "D.Qty",
                    template: function () {
                        return "<ng-form name='frm'> <input type='number' class='form-control' name='DELV_QTY' ng-model='dataItem.DELV_QTY' ng-disabled='dataItem.IS_SMP_FINALIZED==\"Y\"' min='0' max='{{dataItem.maxDelvQtyProd}}' ng-style='frm.DELV_QTY.$error.max? vm.errstyle:\"\"||frm.DELV_QTY.$error.min? vm.errstyle:\"\"' ng-change='vm.onChangeSewQtyProd(dataItem)' /> </ng-form>";
                    },
                    width: "5%"
                },
                {
                    filterable: false,
                    footerTemplate: "#=sum#",
                    field: "REJECT_QTY",
                    title: "Rj.Qty",
                    template: function () {
                        return "<input type='number' class='form-control' ng-model='dataItem.REJECT_QTY' ng-disabled='dataItem.IS_SMP_FINALIZED==\"Y\"' min='0' ng-change='vm.onChangeSewQtyProd(dataItem)' />";
                    },
                    width: "5%"
                },
                {
                    title: "Remarks",
                    field: "REMARKS",
                    filterable: false,
                    template: function () {
                        return "<input type='text' class='form-control' ng-model='dataItem.REMARKS' ng-disabled='dataItem.IS_SMP_FINALIZED==\"Y\"' />";
                    },
                    width: "6%"
                },
                {
                    title: "",
                    template: function () {
                        return "<button class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' ng-disabled='dataItem.IS_SMP_FINALIZED==\"Y\"' title='Remove' ><i class='fa fa-remove'></i></button> <button class='btn btn-xs blue' ng-click='vm.printRow(dataItem, vm.form.SMPL_FMT_ID)' title='Print Sample Card' ><i class='fa fa-print'></i></button>";
                    },
                    width: "5%"
                }
            ]
            //dataBound: function () {
            //    this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},            
        };

        vm.onChangeSewQtyProd = function (dataItem) {
            var balQty = parseInt(dataItem.SEW_QTY) - parseInt(dataItem.REJECT_QTY);
            console.log(dataItem);

            if (balQty > dataItem.RQD_QTY) {
                dataItem.maxDelvQtyProd = dataItem.RQD_QTY;
            }
            else {
                dataItem.maxDelvQtyProd = balQty;
            }
        };

        vm.removeRow = function (dataItem) {            
            $scope.smpProdGridDataSource.remove(dataItem);
            var dataList = $scope.smpProdGridDataSource.data();

            if (dataList.length > 0) {
                $scope.HAS_DATA_PROD_GRID = 'Y';
            }
            else {
                $scope.HAS_DATA_PROD_GRID = 'N';
            }
        };

        $scope.smpProdGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    MrcDataService.GetAllOthers('/api/mrc/SampleReq/getSmplProdQty?&pPROD_DT=' + $filter('date')(vm.form.PROD_DT, vm.dtFormat) + '&pPROD_BATCH_NO=' + vm.form.PROD_BATCH_NO).then(function (res) {
                        
                        if (res.length > 0) {
                            $scope.HAS_DATA_PROD_GRID = 'Y';
                            $scope.IS_PROD_SAVE = 'Y';

                            if (res[0].IS_SMP_FINALIZED == 'Y') {
                                $scope.IS_PROD_SUBMIT = 'Y';
                            }
                            else {
                                $scope.IS_PROD_SUBMIT = 'N';
                            }
                            e.success(res);
                        }
                        else {
                            $scope.HAS_DATA_PROD_GRID = 'N';
                            $scope.IS_PROD_SAVE = 'N';
                            $scope.IS_PROD_SUBMIT = 'N';
                            e.success([]);
                        }
                        
                    }, function (err) {
                        console.log(err);
                    });
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            aggregate: [
                { field: "BYR_ACC_NAME_EN", aggregate: "count"},
                { field: "SEW_QTY", aggregate: "sum" },
                { field: "REJECT_QTY", aggregate: "sum" },
                { field: "DELV_QTY", aggregate: "sum" }
            ],
            schema: {
                model: {
                    id: "MC_SMP_PROD_ID",
                    fields: {
                        ITEM_SNAME: { type: "string", editable: false },
                        COLOR_NAME_EN: { type: "string", editable: false },
                        SIZE_CODE: { type: "string", editable: false },
                        RQD_QTY: { type: "number", editable: false },
                        MOU_CODE: { type: "string", editable: false },
                        SEW_QTY: { type: "number", editable: true },
                        REJECT_QTY: { type: "number", editable: true },
                        DELV_QTY: { type: "number", editable: true }
                        //HR_DAY_TYPE: { defaultValue: { HR_DAY_TYPE_ID: "", DAY_TYPE_NAME_EN: "N/A" }, editable: true },
                    }
                }
            }            
        });
        

        vm.next = function () {
            $state.go('SmplProdEntry.Dtl', {
                pMC_SMP_REQ_H_ID: vm.form.MC_SMP_REQ_H_ID==null ? $stateParams.pMC_SMP_REQ_H_ID : vm.form.MC_SMP_REQ_H_ID,
                pMC_BYR_ACC_ID: vm.form.MC_BYR_ACC_ID == null ? $stateParams.pMC_BYR_ACC_ID : vm.form.MC_BYR_ACC_ID,
                pMC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID == null ? $stateParams.pMC_STYLE_H_EXT_ID : vm.form.MC_STYLE_H_EXT_ID,
                pRF_SMPL_TYPE_ID: vm.form.RF_SMPL_TYPE_ID == null ? $stateParams.pRF_SMPL_TYPE_ID : vm.form.RF_SMPL_TYPE_ID,
                pPROD_DT: vm.form.PROD_DT == null ? $stateParams.pPROD_DT : moment(vm.form.PROD_DT).format("YYYY-MMM-DD"),
                pPROD_BATCH_NO: vm.form.PROD_BATCH_NO == null ? $stateParams.pPROD_BATCH_NO : vm.form.PROD_BATCH_NO
            }, { reload: 'SmplProdEntry.Dtl' });
            vm.IS_NEXT = 'Y';
            //vm.showSplash = 'N';
        };
        
        $scope.$watch('vm.form', function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    $scope.form = vm.form;
                    $scope.form.PROD_DT = $filter('date')(vm.form.PROD_DT, vm.dtFormat);
                }
            }
        }, true);

        $scope.$watchGroup(['vm.form.PROD_DT', 'vm.form.PROD_BATCH_NO'], function (newVal, oldVal) {

            if (!_.isEqual(newVal, oldVal)) {
                if (newVal != '') {
                    vm.IS_NEXT = 'N';
                    $scope.IS_PROD_SUBMIT = 'N';

                    vm.next();
                    $scope.smpProdGridDataSource.read();
                }
            }
        }, true);
        
        vm.onChangeProdDate = function () {
            return MrcDataService.GetAllOthers('/api/mrc/SampleReq/getSmplProdBatchListByDate?&pPROD_DT=' + $filter('date')(vm.form.PROD_DT, vm.dtFormat)).then(function (res) {
                vm.batchList = res;
            }, function (err) {
                console.log(err);
            });
        }

        vm.onSelectBatch = function (batchNo) {
            $stateParams.pPROD_BATCH_NO = batchNo;
            vm.form.PROD_BATCH_NO = batchNo;
        }

        vm.NEW_BACH = { IS_BATCH_OPEN: $scope.IS_PROD_SUBMIT, NEW_BATCH_NO: 0 };
        vm.addNewBatch = function () {
            return MrcDataService.GetAllOthers('/api/mrc/SampleReq/getNewProdBatch?&pPROD_DT=' + $filter('date')(vm.form.PROD_DT, vm.dtFormat)).then(function (res) {

                console.log(res);
                vm.NEW_BACH = { IS_BATCH_OPEN: res.IS_BATCH_OPEN, NEW_BATCH_NO: res.NEW_BATCH_NO };

                if (res.IS_BATCH_OPEN == 'Y') {
                    Dialog.confirm('Batch# ' + parseInt(res.NEW_BATCH_NO)-1 + ' already open.', 'Warning', ['Ok','']).then(function () {
                         //alert('A batch# open');
                    });
                    //alert('A batch# open');
                }
                else {
                    $stateParams.pPROD_BATCH_NO = res.NEW_BATCH_NO;
                    vm.form.PROD_BATCH_NO = res.NEW_BATCH_NO;
                    $scope.smpProdGridDataSource.read();
                }

            }, function (err) {
                console.log(err);
            });
        }

        vm.submitData = function (token) {
            var submitProdData = angular.copy($scope.form);
            //submitData.DTL_XML = [];
            var prodData = $scope.smpProdGridDataSource.data();
            console.log(prodData);
            //return;
            //console.log($scope.form);

            submitProdData.DTL_XML = MrcDataService.xmlStringShort(prodData.map(function (ob) {
                ob['PROD_BATCH_NO'] = $scope.form.PROD_BATCH_NO;
                ob['PROD_DT'] = $filter('date')($scope.form.PROD_DT, vm.dtFormat);

                return ob;
            }));

            console.log(submitProdData);

            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: '/api/mrc/SampleReq/BatchSaveSmplProd',
                method: 'post',
                data: submitProdData
            })
            .then(function (res) {
                $scope.errors = undefined;
                if (res.data.success === false) {
                    $scope.errors = [];
                    $scope.errors = res.data.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.data.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.isSaved = true;
                        $scope.smpProdGridDataSource.read();                        
                    };

                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        vm.backToList = function () {
            return $state.go('SmplFabBookList', { pMC_BYR_ACC_ID: 0 });
        };

        vm.sendToInhouse = function (token) {
            var submitProdData = angular.copy($scope.form);
            //submitData.DTL_XML = [];
            var prodData = $scope.smpProdGridDataSource.data();
            //console.log(prodData);
            //console.log($scope.$parent.form);

            submitProdData.DTL_XML = MrcDataService.xmlStringShort(prodData.map(function (ob) {
                ob['PROD_BATCH_NO'] = $scope.form.PROD_BATCH_NO;
                ob['PROD_DT'] = $filter('date')($scope.form.PROD_DT, vm.dtFormat);

                return ob;
            }));

            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: '/api/mrc/SampleReq/BatchSaveSendToInhouse',
                method: 'post',
                data: submitProdData
            })
            .then(function (res) {
                $scope.errors = undefined;
                if (res.data.success === false) {
                    $scope.errors = [];
                    $scope.errors = res.data.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.data.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.isSaved = true;
                        $scope.smpProdGridDataSource.read();
                    };

                    config.appToastMsg(res.data.PMSG);
                }
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };


    }

})();
////////// End Header Controller





////////// Start Production Entry Detail Controller
(function () {
    'use strict';
    angular.module('multitex.mrc').controller('SmplProdDtlController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$state', '$stateParams', 'MrcDataService', SmplProdDtlController]);
    function SmplProdDtlController(logger, config, $q, $scope, $http, exception, $filter, $state, $stateParams, MrcDataService) {

        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
         
        vm.errStyle1 = { 'background-color': '#f13d3d' };
        vm.errstyle = { 'border': '1px solid #f13d3d' };
        //var noErrorStyle = { 'border': '1px solid #f13d3d' };

        console.log($stateParams);

        activate();
        function activate() {
            var promise = [
                //getByrAcList(), getOrderList(), getSampleTypeList(), getStyleItemList(), getStyleColorList(), getStyleSizeList()
            ];
            return $q.all(promise).then(function () {
                
                vm.showSplash = false;
            });
        }


        function getByrAcList() {
            vm.buyerAcOption = {
                optionLabel: "-- Select Buyer A/C --",
                filter: "contains",
                autoBind: true,
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    //console.log(item);

                    vm.orderDataSource = new kendo.data.DataSource({
                        data: _.filter(vm.orderDataList, { 'MC_BYR_ACC_ID': item.MC_BYR_ACC_ID })
                    });
                }
            };
        };

        vm.buyerAcDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    MrcDataService.GetAllOthers('/api/mrc/SampleReq/getByrAcListByReqId/' + $stateParams.pMC_SMP_REQ_H_ID).then(function (res) {
                        e.success(res);
                    }, function (err) {
                        console.log(err);
                    });
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            }
        });




        function getOrderList() {
            MrcDataService.GetAllOthers('/api/mrc/SampleReq/getOrdListByReqId/' + $stateParams.pMC_SMP_REQ_H_ID).then(function (res) {
                vm.orderDataList = res;
            }, function (err) {
                console.log(err);
            });

            vm.orderOption = {
                optionLabel: "-- Select Order --",
                filter: "startswith",
                autoBind: true,
                dataBound: function (e) {
                    var item = this.dataItem(e.item);
                    //vm.MC_BUYER_ID = item.MC_BUYER_ID;                    
                },
                dataTextField: "WORK_STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    //console.log(item);
                    vm.formDtl.MC_ORDER_H_ID = item.MC_ORDER_H_ID;

                    vm.smpTypeDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                MrcDataService.GetAllOthers('/api/mrc/buyer/SampleListByBuyerID/' + item.MC_BUYER_ID).then(function (res) {
                                    e.success(res);
                                }, function (err) {
                                    console.log(err);
                                });

                            },
                            parameterMap: function (options, operation) {
                                if (operation !== "read" && options.models) {
                                    return { models: kendo.stringify(options.models) };
                                }
                            }
                        }
                    });

                    vm.styleItemDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                return MrcDataService.GetAllOthers('/api/mrc/StyleDItem/ChildItemListByStyle/' + item.MC_STYLE_H_ID).then(function (res) {
                                    console.log(res);

                                    e.success(res);
                                }, function (err) {
                                    console.log(err);
                                });

                            },                            
                            parameterMap: function (options, operation) {
                                if (operation !== "read" && options.models) {
                                    return { models: kendo.stringify(options.models) };
                                }
                            }
                        }
                    });
                    
                    vm.styleColorDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                MrcDataService.GetAllOthers('/api/mrc/ColourMaster/ColourMappDataByBuyerId/' + parseInt(item.MC_STYLE_H_ID)).then(function (res) {
                                    vm.buyerColorList = _.map(_.groupBy(res, function (doc) {
                                        return doc.MC_COLOR_ID;
                                    }), function (grouped) {
                                        return grouped[0];
                                    });

                                    e.success(vm.buyerColorList);
                                }, function (err) {
                                    console.log(err);
                                });

                            },
                            parameterMap: function (options, operation) {
                                if (operation !== "read" && options.models) {
                                    return { models: kendo.stringify(options.models) };
                                }
                            }
                        }
                    });
                    
                    vm.styleSizeDataSource = new kendo.data.DataSource({
                        transport: {
                            read: function (e) {
                                MrcDataService.GetAllOthers('/api/mrc/Order/StyleWiseSizeList/' + item.MC_STYLE_H_ID).then(function (res) {
                                    //var styleItems=_.filter(res,{})
                                    e.success(res);
                                }, function (err) {
                                    console.log(err);
                                });

                            },
                            parameterMap: function (options, operation) {
                                if (operation !== "read" && options.models) {
                                    return { models: kendo.stringify(options.models) };
                                }
                            }
                        }
                    });
                    
                }
            };
            
        };

        function getSampleTypeList() {
            return vm.sampleTypeOption = {
                optionLabel: "-- Select Type --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "SMPL_TYPE_NAME",
                dataValueField: "RF_SMPL_TYPE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.formDtl.SMPL_TYPE_NAME = item.SMPL_TYPE_NAME;
                }
            };
        };
        
        function getStyleItemList() {
            return vm.styleItemOption = {
                optionLabel: "-- Select Item --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "ITEM_SNAME",
                dataValueField: "MC_STYLE_D_ITEM_ID",
                select: function (e) {                    
                    var item = this.dataItem(e.item);
                    console.log(item);
                    vm.formDtl.ITEM_SNAME = item.ITEM_SNAME;
                }
            };
        };

        function getStyleColorList() {
            return vm.styleColorOption = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "COLOR_NAME_EN",
                dataValueField: "MC_COLOR_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.formDtl.COLOR_NAME_EN = item.COLOR_NAME_EN;
                }
            };
        };

        function getStyleSizeList() {
            return vm.styleSizeOption = {
                optionLabel: "-- Select --",
                filter: "startswith",
                autoBind: true,
                dataTextField: "SIZE_CODE",
                dataValueField: "MC_SIZE_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.formDtl.SIZE_CODE = item.SIZE_CODE;
                }
            };
        };

        //function getMouList() {
        //    return vm.mouOption = {
        //        optionLabel: "-- Select --",
        //        filter: "startswith",
        //        autoBind: true,
        //        dataTextField: "SIZE_CODE",
        //        dataValueField: "MC_SIZE_ID"
        //    };
        //};
        

        //////////////////////// New //////////    
        
        vm.entryGridOption = {
            height: 180,
            sortable: true,
            pageable: false,
            editable: false,
            //selectable: "cell",
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
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", type: "string", width: "10%" },
                { field: "STYLE_NO", title: "Work Style", type: "string", width: "10%" },
                { field: "SMPL_TYPE_NAME", title: "Smp. Type", type: "string", width: "10%" },
                { field: "ITEM_SNAME", title: "Item", type: "string", width: "20%" },
                {
                    title: "Color",
                    field: "COLOR_NAME_EN",
                    filterable: false,
                    template: function () {
                        return "<span ng-show='dataItem.IS_ANY_COL!=\"Y\" && dataItem.IS_AVAILABLE_COL!=\"Y\"' >{{dataItem.COLOR_NAME_EN}}</span>" +
                                "<ng-form name='frmColor' ><span ng-show='dataItem.IS_ANY_COL==\"Y\" || dataItem.IS_AVAILABLE_COL==\"Y\"' ng-style='frmColor.PRD_COLOR_ID.$error.required? vm.errstyle:\"\"' > " +
                                "<select kendo-drop-down-list name='PRD_COLOR_ID' class='form-control' ng-model='dataItem.PRD_COLOR_ID' "+                                
                                "k-data-text-field='\"COLOR_NAME_EN\"' k-data-value-field='\"MC_COLOR_ID\"' k-option-label='\"-- Select --\"' required='true'" +
                                "k-data-source='dataItem.itemWiseColorList' k-on-select='vm.onChangeColorEntry(kendoEvent,dataItem)' ></select> </span>" +
                                "</ng-form>";
                    },
                    width: "10%"
                }, 
                //{ field: "itemWiseColorList", title: "Color", width: "70px", editor: "#=itemWiseColorList#" }, //template: "#=BUYER_TYPE.BUYER_NAME_EN#" },
                { field: "SIZE_CODE", title: "Size", type: "string", width: "5%", filterable: false },
                { field: "RQD_QTY", title: "R.Qty", type: "string", width: "5%", filterable: false },
                { field: "MOU_CODE", title: "Unit", type: "string", width: "5%", filterable: false },
                //{ field: "SEW_QTY", title: "S.Qty", type: "string", width: "40px", filterable: false, editable: true },
                //{ field: "DELV_QTY", title: "D.Qty", type: "string", width: "40px", filterable: false }//,                
                {
                    title: "S.Qty",
                    field: "SEW_QTY",
                    filterable: false,
                    template: function () {
                        return "<input type='number' class='form-control' ng-model='dataItem.SEW_QTY' min='0' ng-change='vm.onChangeSewQtyEntry(dataItem)' />";
                    },
                    width: "5%"
                },
                {
                    title: "D.Qty",
                    field: "DELV_QTY",
                    filterable: false,
                    template: function () {
                        return " <ng-form name='frm'> <input type='number' class='form-control' name='DELV_QTY' ng-model='dataItem.DELV_QTY' min='0' max='{{dataItem.maxDelvQtyEntry}}' ng-style='frm.DELV_QTY.$error.max? vm.errstyle:\"\"||frm.DELV_QTY.$error.min? vm.errstyle:\"\"' ng-change='vm.onChangeSewQtyEntry(dataItem)' /> </ng-form>";
                    },
                    width: "5%"
                },
                {
                    title: "Rj.Qty",
                    field: "REJECT_QTY",
                    filterable: false,
                    template: function () {
                        return "<input type='number' class='form-control' ng-model='dataItem.REJECT_QTY' min='0' ng-change='vm.onChangeSewQtyEntry(dataItem)' />";
                    },
                    width: "5%"
                },
                {
                    title: "Remarks",
                    field: "REMARKS",
                    filterable: false,
                    template: function () {
                        return "<input type='text' class='form-control' ng-model='dataItem.REMARKS' />";
                    },
                    width: "10%"
                }
                //{
                    //title: "Action",
                    //template: function () {                       
                    //    return "<a class='btn btn-xs blue' ui-sref='SmplFabBookEntry.Dtl({pMC_SMP_REQ_H_ID:dataItem.MC_SMP_REQ_H_ID})' ><i class='fa fa-edit'> Edit</i></a>    <a ng-click='vm.printBookingRecord(dataItem)' class='btn btn-xs blue'><i class='fa fa-print'> Print</i></a>";
                    //},
                    //width: "55px"
                //}
            ]
            //dataBound: function () {
            //    this.expandRow(this.tbody.find("tr.k-master-row").first());
            //},            
        };

        vm.onChangeColorEntry = function (e, dataItem) {
            var item = e.sender.dataItem(e.item);
            //console.log(item);

            dataItem.PRD_COLOR_ID = item.MC_COLOR_ID;
            dataItem.COLOR_NAME_EN = item.COLOR_NAME_EN;
        };

        vm.onChangeSewQtyEntry = function (dataItem) {
            var balQty = dataItem.SEW_QTY - dataItem.REJECT_QTY;
            //alert(balQty);

            if (balQty > dataItem.RQD_QTY) {
                dataItem.maxDelvQtyEntry = dataItem.RQD_QTY;
            }
            else {
                dataItem.maxDelvQtyEntry = balQty;
            }
        };

        vm.entryGridDataSource = new kendo.data.DataSource({
            batch: true,
            transport: {
                read: function (e) {
                    MrcDataService.GetAllOthers('/api/mrc/SampleReq/getStyleSmpTypWiseProdQty?pMC_SMP_REQ_H_ID=' + $stateParams.pMC_SMP_REQ_H_ID + '&pMC_STYLE_H_EXT_ID=' + $stateParams.pMC_STYLE_H_EXT_ID +
                                                '&pRF_SMPL_TYPE_ID=' + $stateParams.pRF_SMPL_TYPE_ID + '&pPROD_DT=' + $stateParams.pPROD_DT + '&pPROD_BATCH_NO=' + $stateParams.pPROD_BATCH_NO).then(function (res) {
                                                    e.success(res);
                                                }, function (err) {
                                                    console.log(err);
                                                });

                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            schema: {
                model: {
                    id: "MC_SMP_PROD_ID",
                    fields: {
                        ITEM_SNAME: { type: "string", editable: false },
                        COLOR_NAME_EN: { type: "string", editable: false },
                        SIZE_CODE: { type: "string", editable: false },
                        RQD_QTY: { type: "number", editable: false },
                        MOU_CODE: { type: "string", editable: false },
                        SEW_QTY: { type: "number", editable: true },
                        REJECT_QTY: { type: "number", editable: true },
                        DELV_QTY: { type: "number", editable: true }
                        //HR_DAY_TYPE: { defaultValue: { HR_DAY_TYPE_ID: "", DAY_TYPE_NAME_EN: "N/A" }, editable: true },
                    }
                }
            }
        });
        //////==============================================
        
        
        //$scope.$parent.smpProdGridDataSource = new kendo.data.DataSource({
        //    data: []
        //});

        

        vm.addToBatch = function () {
            var dataList = vm.entryGridDataSource.data();
            var prodDataList = $scope.$parent.smpProdGridDataSource.data();
            console.log(prodDataList);
            console.log(dataList);

            angular.forEach(dataList, function (val, key) {
                //var isInsert = _.some(prodDataList, { 'MC_SMP_REQ_D1_ID': val['MC_SMP_REQ_D1_ID'], 'active': false });
                if (val['SEW_QTY'] > 0 && val['DELV_QTY'] > 0) {
                    var isInsert = _.some(prodDataList, function(ob) { 
                        return ob.MC_SMP_REQ_D1_ID == val['MC_SMP_REQ_D1_ID'];
                    });
                    if (isInsert==false) {
                        $scope.$parent.sampBatchProd.push(val);
                        $scope.$parent.smpProdGridDataSource.insert(0, val);
                        $scope.$parent.HAS_DATA_PROD_GRID = 'Y';
                    }
                }
            });
                        
            vm.entryGridDataSource.read();
        };
        /////////////////////// End New //////////////////

    }

})();

////////// End Detail Controller