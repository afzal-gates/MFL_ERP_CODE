(function () {
    'use strict';
    angular.module('multitex.core').controller('UploadOtherDocsController', ['$q', '$stateParams', '$state', '$scope', '$http', '$modalInstance', '$localStorage', 'commonDataService', 'orderData', 'pageAccess', 'config', 'exception', 'CoreFileUploadService', 'Dialog', UploadOtherDocsController]);
    function UploadOtherDocsController($q, $stateParams, $state, $scope, $http, $modalInstance, $localStorage, commonDataService, orderData, pageAccess, config, exception, CoreFileUploadService, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        vm.selectedFlr = null;

        vm.isStyleExFirstLoad = 'Y';

        var key = 'MC_STYLE_H_EXT_ID';

        vm.form = {};//orderData[key] ? angular.copy(orderData) : { TAG_PRE_FIX: '', items: [] };
        vm.search = orderData[key] ? { MC_BYR_ACC_ID: orderData.MC_BYR_ACC_ID, MC_STYLE_H_EXT_ID: orderData.MC_STYLE_H_EXT_ID, DOC_REF_NO: '' } : { MC_BYR_ACC_ID: null, MC_STYLE_H_EXT_ID: null, DOC_REF_NO: '' };
        vm.file = {};

        if (pageAccess) {
            vm.pageAccess = pageAccess;
        }
        else {
            vm.pageAccess = { IS_ONLY_VIEW: 'Y' };
        }

        vm.styleExtTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO #</h5><p> (#: data.ORDER_NO||""#)</p></span>';
        vm.styleExtValueTemplate = '<span><h5 style="padding:0px;margin:0px;">#: data.STYLE_NO # (#: data.ORDER_NO||"" #)</h5></span>';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getBuyerAccListData(), getByrAccWiseStyleExtList(), getDocType()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
       

        vm.cancel = function (data) {
            $modalInstance.dismiss(data);
        };


        vm.docGridOption = {
            height: 220,
            sortable: true,
            pageable: false,
            scrollable: {
                virtual: true,
                //scrollable:true
            },
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
                { field: "DOC_REF_NO", title: "Search Tag", width: "30%" },
                { field: "DOC_TYPE_NAME_EN", title: "Doc Type", width: "12%" },
                { field: "STYLE_NO", title: "Style", width: "10%" },
                { field: "ORDER_NO", title: "Order", width: "10%" },
                { field: "DOC_PATH_URL", title: "File Name", width: "30%" },
                {
                    title: "Action",
                    template: function () {
                        return "<a class='btn btn-xs blue' href='/UPLOAD_DOCS/OTHER_DOCS/{{dataItem.DOC_PATH_URL}}' target='_blank' title='View'><i class='fa fa-file'></i></a> <button type='button' class='btn btn-xs red' ng-click='vm.removeRow(dataItem)' title='Remove'  ng-disabled='vm.pageAccess.IS_ONLY_VIEW==\"Y\"' ><i class='fa fa-remove'></i></button>";
                    },
                    width: "8%"
                }
            ]
        };

        
        vm.searchUploadedDoc = function () {
            vm.docGridDataSource.read();
        }

        vm.docGridDataSource = new kendo.data.DataSource({
            serverPaging: true,
            serverFiltering: true,
            pageSize: 10,
            schema: {
                data: "data",
                total: "total",
                model: {
                    id: "RF_DOC_ARCV_ID",
                    fields: {
                        DOC_REF_NO: { type: "string", editable: false }
                    }
                }
            },
            transport: {
                read: function (e) {
                    var webapi = new kendo.data.transports.webapi({});
                    var params = webapi.parameterMap(e.data);
                    var url = '/api/common/GetUploadDocList?pMC_BYR_ACC_ID=' + vm.search.MC_BYR_ACC_ID + '&pMC_STYLE_H_EXT_ID=' + vm.search.MC_STYLE_H_EXT_ID + '&pDOC_REF_NO=' + vm.search.DOC_REF_NO;

                    url += '&pageNumber=' + params.page + '&pageSize=' + params.pageSize;
                    url += commonDataService.kFilterStr2QueryParam(params.filter);

                    return commonDataService.getDataByFullUrl(url).then(function (res) {
                        console.log(res);
                        e.success(res);
                    });
                }
            }
        });

        vm.addToGrid = function (data) {
            console.log(data);

            data['FILE_NAME'] = data.ATT_FILE.name;

            vm.docGridDataSource.insert(0, data);

            vm.cancelForm();
        }

        vm.cancelForm = function () {
            var data = angular.copy(vm.form);

            vm.form = {
                RF_DOC_ARCV_ID: -1, MC_BUYER_ID: vm.form.MC_BUYER_ID, MC_STYLE_H_EXT_ID: vm.form.MC_STYLE_H_EXT_ID, MC_BYR_ACC_ID: vm.form.MC_BYR_ACC_ID,
                LK_DOC_TYPE_ID: vm.form.LK_DOC_TYPE_ID, DOC_DESC: null, DOC_REF_NO: null
            };            
        }

        vm.removeRow = function (dataItem) {

            Dialog.confirm('Do you want to remove the uploaded file?', 'Confirmation', ['Yes', 'No']).then(function () {

                return commonDataService.saveDataByFullUrl(dataItem, '/api/common/DeleteUploadedOtherDocs').then(function (res) {

                    console.log(res);

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res.data);

                        if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                            vm.docGridDataSource.remove(dataItem);
                        };

                        config.appToastMsg(res.data.PMSG);

                    }
                });

                
            });
        };

        function getBuyerAccListData() {
            vm.searchBuyerAccList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.search.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;
                    vm.search.MC_STYLE_H_EXT_ID = null;

                    vm.searchStyleExtDataSource.read();
                }                
            };

            return vm.buyerAccList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getDataByFullUrl('/api/mrc/BuyerAcc/getBuyerAccListByUserId').then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "BYR_ACC_NAME_EN",
                dataValueField: "MC_BYR_ACC_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    vm.form.MC_BYR_ACC_ID = item.MC_BYR_ACC_ID;

                    vm.styleExtDataSource.read();
                }                
            };
        };

        function getByrAccWiseStyleExtList() {
            vm.styleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.form.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;

                    vm.form.STYLE_NO = item.STYLE_NO;
                    vm.form.ORDER_NO = item.ORDER_NO;
                    
                    vm.genSearchTag();

                }
            };

            vm.searchStyleExtOption = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataTextField: "STYLE_NO",
                dataValueField: "MC_STYLE_H_EXT_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.search.MC_STYLE_H_ID = item.MC_STYLE_H_ID;
                    vm.search.MC_ORDER_H_ID_LST = item.MC_ORDER_H_ID;
                    vm.search.MC_STYLE_H_EXT_ID = item.MC_STYLE_H_EXT_ID;

                    if (item.MC_STYLE_H_EXT_ID > 0) {
                        vm.search.STYLE_NO = item.STYLE_NO;
                        vm.search.ORDER_NO = item.ORDER_NO;
                    }
                    else {
                        vm.search.STYLE_NO = '';
                        vm.search.ORDER_NO = '';
                    }
                },
                dataBound: function (e) {
                    if (orderData[key]) {
                        var MC_STYLE_H_EXT_ID = orderData['MC_STYLE_H_EXT_ID']; //this.dataItem(e.item).MC_BYR_ACC_ID;
                        vm.search['MC_STYLE_H_EXT_ID'] = orderData['MC_STYLE_H_EXT_ID'];
                    }
                }
            };


            vm.searchStyleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_ID=' + (vm.search.MC_BYR_ACC_ID || orderData.MC_BYR_ACC_ID || null); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += commonDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        if (orderData[key] && (paramsData[2] == undefined) && vm.isStyleExFirstLoad == 'Y') {
                            vm.isStyleExFirstLoad = 'N';
                            url += '&pMC_STYLE_H_EXT_ID=' + orderData.MC_STYLE_H_EXT_ID;
                        }
                        else {
                            url += '&pMC_STYLE_H_EXT_ID';
                        };

                        return commonDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });

            return vm.styleExtDataSource = new kendo.data.DataSource({
                serverFiltering: true,
                transport: {
                    read: function (e) {
                        var webapi = new kendo.data.transports.webapi({});
                        var params = webapi.parameterMap(e.data);
                        var url = '/api/mrc/Order/GetOrderList?pMC_BYR_ACC_ID=' + (vm.form.MC_BYR_ACC_ID||0); //pMC_BYR_ACC_ID=' + ((vm.form.MC_BYR_ACC_ID > 0) ? vm.form.MC_BYR_ACC_ID : 0);
                        //url += '&pageNumber=' + 1 + '&pageSize=' + 10;
                        url += commonDataService.kFilterStr2QueryParam(params.filter);

                        var paramsData = params.filter.replace(/'/g, '').split('~');
                        console.log(paramsData[2]);

                        return commonDataService.getDataByFullUrl(url).then(function (res) {
                            e.success(res);
                        }, function (err) {
                            console.log(err);
                        });
                    }
                }
            });
        };

        function getDocType() {
            return vm.docTypeList = {
                optionLabel: "-- Select --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getLookupList(129).then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID",
                select: function (e) {
                    var item = this.dataItem(e.item);
                    console.log(item);

                    vm.form.LK_DOC_TYPE_ID = item.LOOKUP_DATA_ID;
                    vm.form.TAG_PRE_FIX = item.LK_DATA_NAME_EN;

                    vm.genSearchTag();
                }                
            };
        };


       

        vm.genSearchTag = function () {
            vm.form.DOC_REF_NO = (vm.form.TAG_PRE_FIX||'') + '_' + (vm.form.STYLE_NO||'') + '(' + (vm.form.ORDER_NO||'') + ')';
        }

        function getExtention(fileName) {
            var i = fileName.lastIndexOf('.');
            if (i === -1) return false;
            return fileName.slice(i)
        }


        vm.submitData = function (data) {
                        
            data['ATT_FILE'] = vm.file.ATT_FILE;                       
            data['DOC_PATH_URL'] = data.DOC_REF_NO + getExtention(data.ATT_FILE.name);
            data['DISPLAY_ORDER'] = 1;
            
            //console.log(getModelAsFormData(data));
            //console.log(data);
            
            return CoreFileUploadService.save(data, '/GlobalUI/UploadOtherDocs', vm.antiForgeryToken).then(function (res) {
                $scope.errors = undefined;

                console.log(res);

                if (res.success === false) {
                    $scope.errors = [];
                    $scope.errors = res.data.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.docGridDataSource.read();
                    };

                    config.appToastMsg(res.data.PMSG);
                }
            }, function (err) {
                console.log(err);
            });
                
        };

    }

})();