(function () {
    'use strict';
    angular.module('multitex.knitting').controller('KnitScPartyRateController', ['$q', 'config', 'KnittingDataService', '$stateParams', '$http', '$state', '$scope', 'logger', 'commonDataService', KnitScPartyRateController]);
    function KnitScPartyRateController($q, config, KnittingDataService, $stateParams, $http, $state, $scope, logger, commonDataService) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = { KNT_MACHINE_ID: 0, IS_ACTIVE: 'Y', items: [] };
        vm.formCopy = {};

        activate();
        vm.showSplash = true;
        function activate() {
            var promise = [getRateGroupList(), GetStatusList(), GetSupplierList(), getCurrencyList()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        };

        $scope.QUOT_REF_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.QUOT_REF_DT_LNopened = true;
        }

        $scope.REVISED_DT_LNopen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.REVISED_DT_LNopened = true;
        }


        vm.reset = function () {
            //console.log(vm.formCopy);   
            vm.form.KNT_MACHINE_ID = 0;
            vm.form.KNT_MACHINE_NO = '';

            vm.onChangeMcGgList();
        };

        vm.rateDtlList = [];

        vm.getSupplierRateInfo = function () {
            if (vm.form.SCM_SUPPLIER_ID > 0) {
                KnittingDataService.getDataByFullUrl('/api/knit/KnitScPartyRate/GetScPartyRate?pSCM_SUPPLIER_ID=' + vm.form.SCM_SUPPLIER_ID + '&pMC_FAB_PROC_GRP_ID=' + vm.form.MC_FAB_PROC_GRP_ID).then(function (res) {
                    if (res.length > 0) {
                        if (res[0].SCM_SUPPLIER_ID > 0) {
                            vm.quatationList = new kendo.data.DataSource({
                                data: res
                            });

                            vm.quatatitionRateDtlList = new kendo.data.DataSource({
                                data: []
                            });
                        }
                        else {
                            vm.rateDtlList = new kendo.data.DataSource({
                                data: []
                            });

                            vm.quatatitionRateDtlList = new kendo.data.DataSource({
                                data: []
                            });
                            vm.form = { 'SCM_SUPPLIER_ID': vm.form.SCM_SUPPLIER_ID, 'MC_FAB_PROC_GRP_ID': vm.form.MC_FAB_PROC_GRP_ID, 'FAB_PROC_GRP_NAME': vm.form.FAB_PROC_GRP_NAME, 'RF_CURRENCY_ID': '', 'LK_APRVL_STATUS_ID': '', items: [] }

                            vm.form['items'] = []
                        }

                        KnittingDataService.getDataByFullUrl('/api/knit/KnitScPartyRate/GetScPartyRateDtl?pSCM_SUPPLIER_ID=' + (vm.form.SCM_SUPPLIER_ID || null)).then(function (resD) {
                        vm.rateDtlList = new kendo.data.DataSource({
                            data: resD
                        });
                    });

                    }
                    else {

                        vm.rateDtlList = new kendo.data.DataSource({
                            data: []
                        });

                        vm.quatatitionRateDtlList = new kendo.data.DataSource({
                            data: []
                        });
                        vm.form = { 'SCM_SUPPLIER_ID': vm.form.SCM_SUPPLIER_ID, 'MC_FAB_PROC_GRP_ID': vm.form.MC_FAB_PROC_GRP_ID, 'FAB_PROC_GRP_NAME': vm.form.FAB_PROC_GRP_NAME, 'RF_CURRENCY_ID': '', 'LK_APRVL_STATUS_ID': '', items: [] }

                        vm.form['items'] = []
                    }
                });

            }
        }

        function getRateGroupList() {
            return vm.rateGroupList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/Dye/DFProcessType/GetFabProcGrpList').then(function (res) {
                            e.success(res);
                        });
                    }
                },
                pageSize: 15
            });
        };


        function GetStatusList() {

            return vm.statusList = {
                optionLabel: "-- Select Status --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.LookupListData(116).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };

        function getCurrencyList() {
            return vm.currencyList = {
                optionLabel: "-- Select Currency --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            commonDataService.getCurrencyList().then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "CURR_NAME_EN",
                dataValueField: "RF_CURRENCY_ID"
            };
        };


        function GetSupplierList() {
            return vm.supplierList = {
                optionLabel: "-- Select Supplier --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            KnittingDataService.getDataByFullUrl('/api/purchase/SupplierProfile/SelectAll').then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "SUP_TRD_NAME_EN",
                dataValueField: "SCM_SUPPLIER_ID"
            };
        };

        vm.onChangeQuatation = function (dataItem) {

            var item = angular.copy(dataItem);
            vm.form = item;
            vm.form['items'] = [{ 'QUOT_DOC_PATH': item.QUOT_DOC_PATH }]

            var list =angular.copy(_.filter(vm.rateDtlList.data(), function (x) { return x.QUOT_REF_NO == item.QUOT_REF_NO }));

            vm.quatatitionRateDtlList = new kendo.data.DataSource({
                data: list
            });

        }

        vm.onChangeRateGroup = function (dataItem) {

            var item = angular.copy(dataItem);
            vm.form = { 'MC_FAB_PROC_GRP_ID': item.MC_FAB_PROC_GRP_ID, 'FAB_PROC_GRP_NAME': item.FAB_PROC_GRP_NAME, 'RF_CURRENCY_ID': '', 'LK_APRVL_STATUS_ID': '', 'SCM_SUPPLIER_ID': '' };
            //vm.form['MC_FAB_PROC_GRP_ID'] = item.MC_FAB_PROC_GRP_ID;
            //vm.form['FAB_PROC_GRP_NAME'] = item.FAB_PROC_GRP_NAME;

            vm.form['items'] = [];

            vm.rateDtlList = new kendo.data.DataSource({
                data: []
            });

            vm.quatationList = new kendo.data.DataSource({
                data: []
            });

            vm.formItem = {};
            vm.rateList = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        KnittingDataService.getDataByFullUrl('/api/mrc/BudgetSheet/getRate?pMC_FAB_PROC_GRP_ID=' + item.MC_FAB_PROC_GRP_ID).then(function (res) {

                            e.success(res);
                        });
                    }
                }
            });
        };


        vm.addToGridPath = function (data) {
            data['DOC_PATH_REF'] = new Date().getTime() + getExtention(data.ATT_FILE.name);
            vm.form.items.push(data);
            if (vm.slFileAreaShow && vm.form.items.length > 0) {
                vm.disableSubmit = false;
            } else {
                vm.disableSubmit = true;
            }
            vm.file = {};
        }

        vm.removeAddedDoc = function (index) {
            if (vm.form.items[index].HR_SL_DOCS_ID > 0) {
                vm.form.items[index].REMOVE = 'Y';
            } else {
                vm.form.items.splice(index, 1);
            }


            if (vm.slFileAreaShow && vm.form.items.length > 0) {
                vm.disableSubmit = false;
            } else {
                vm.disableSubmit = true;
            }

        }

        function getExtention(fileName) {
            var i = fileName.lastIndexOf('.');
            if (i === -1) return false;
            return fileName.slice(i)
        }



        vm.addToGrid = function () {
            //if (fnValidateSub3() == true) {

            var item = _.filter(vm.rateList.data(), function (o) { return o.MC_FAB_PROC_RATE_ID == vm.formItem.MC_FAB_PROC_RATE_ID });

            vm.formItem.FAB_PROC_NAME = item[0].FAB_PROC_NAME;

            if (vm.formItem.uid) {
                // Update
                var row = vm.quatatitionRateDtlList.getByUid(vm.formItem.uid);

                row["SCM_SC_QUOT_RATE_D_ID"] = vm.formItem.SCM_SC_QUOT_RATE_D_ID;
                row["FAB_PROC_NAME"] = vm.formItem.FAB_PROC_NAME;
                row["MC_FAB_PROC_RATE_ID"] = vm.formItem.MC_FAB_PROC_RATE_ID;
                row["PROD_RATE"] = vm.formItem.PROD_RATE;

            } else {
                if (vm.quatatitionRateDtlList != null) {

                    var idx = vm.quatatitionRateDtlList.data().length + 1;
                    vm.quatatitionRateDtlList.insert(idx, vm.formItem);
                }
                else {
                    var idx = vm.quatatitionRateDtlList.data().length + 1;
                    vm.quatatitionRateDtlList.insert(idx, vm.formItem);
                }

            }
            vm.formItem = { MC_FAB_PROC_RATE_ID: '' };

        };

        vm.gridOptionsRate = {
            pageable: false,
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
            height: '200px',
            scrollable: true,
            columns: [

                { field: "SCM_SC_QUOT_RATE_D_ID", hidden: true },
                { field: "SCM_SC_QUOT_RATE_H_ID", hidden: true },
                { field: "MC_FAB_PROC_RATE_ID", hidden: true },

                { field: "FAB_PROC_NAME", title: "Rate Desc.", type: "string", width: "50%" },
                { field: "PROD_RATE", title: "Rate", width: "10%" },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>  <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "20%"
                }
            ],
        };

        vm.gridOptions = {
            pageable: false,
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
            height: '200px',
            scrollable: true,
            columns: [

                { field: "SCM_SC_QUOT_RATE_D_ID", hidden: true },
                { field: "SCM_SC_QUOT_RATE_H_ID", hidden: true },
                { field: "MC_FAB_PROC_RATE_ID", hidden: true },

                { field: "FAB_PROC_NAME", title: "Rate Desc.", type: "string", width: "50%" },
                { field: "PROD_RATE", title: "Rate", width: "10%" },
                {
                    title: "View Quatation Att.",
                    template: function () {
                        return "</a><a href='{{dataItem.QUOT_DOC_PATH}}' target='_blank' class='btn btn-xs blue'><i class='fa fa-eye'> {{dataItem.QUOT_REF_NO}}</i></a></a>";
                    },
                    width: "20%"
                },
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>  <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "20%"
                }
            ],
        };

        vm.editItemData = function (data) {
            vm.formItem = angular.copy(data);
        }

        vm.removeData = function (data) {

            if (!data.SCM_SC_QUOT_RATE_D_ID || data.SCM_SC_QUOT_RATE_D_ID <= 0) {
                vm.rateDtlList.remove(data);
                return;
            };

            Dialog.confirm('Removing "' + data.FAB_PROC_NAME + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     vm.rateDtlList.remove(data);
                 });
        }


        vm.printPartyRate = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3549';

            vm.form.MC_FAB_PROC_GRP_ID = dataItem.MC_FAB_PROC_GRP_ID;
            vm.form.SCM_SUPPLIER_ID = dataItem.SCM_SUPPLIER_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReport');
            form.setAttribute("target", '_blank');

            var params = vm.form;

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


        vm.printScPartyRate = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3558';

            vm.form.SCM_SUPPLIER_ID = dataItem.SCM_SUPPLIER_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

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


        vm.printScPartyRateQuatationWise = function (dataItem) {
            console.log(dataItem);
            vm.form.REPORT_CODE = 'RPT-3574';

            vm.form.SCM_SUPPLIER_ID = dataItem.SCM_SUPPLIER_ID;
            vm.form.SCM_SC_QUOT_RATE_H_ID = dataItem.SCM_SC_QUOT_RATE_H_ID;

            var form = document.createElement("form");
            form.setAttribute("method", "post");
            form.setAttribute("action", '/api/Knit/KntReport/PreviewReportRDLC');
            form.setAttribute("target", '_blank');

            var params = vm.form;

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

        function getModelAsFormData(idata) {
            var dataAsFormData = new FormData();
            angular.forEach(idata, function (value, key) {
                dataAsFormData.append(key, value);
            });
            return dataAsFormData;
        };

        vm.submitAll = function (data) {

            if (fnValidate() == true) {

                //var data = angular.copy(item);               

                angular.forEach(data.items, function (val, key) {
                    console.log(val);
                    $http({
                        method: 'post',
                        url: '/Knitting/Knit/uploadImage',
                        data: getModelAsFormData(val),
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined, "RequestVerificationToken": key }
                    }).success(function (data, status, headers, config1) {
                        console.log(status);
                    }).
                      error(function (data, status, headers, config) {
                          console.log(status);
                      });
                });

                data["QUOT_DOC_PATH"] = data.items[0].DOC_PATH_REF;

                data["XML_D"] = KnittingDataService.xmlStringShort(vm.quatatitionRateDtlList.data().map(function (o) {
                    return {
                        SCM_SC_QUOT_RATE_D_ID: o.SCM_SC_QUOT_RATE_D_ID == null ? 0 : o.SCM_SC_QUOT_RATE_D_ID,
                        SCM_SC_QUOT_RATE_H_ID: o.SCM_SC_QUOT_RATE_H_ID == null ? 0 : o.SCM_SC_QUOT_RATE_H_ID,
                        MC_FAB_PROC_RATE_ID: o.MC_FAB_PROC_RATE_ID,
                        PROD_RATE: o.PROD_RATE,
                    }
                }));

                return KnittingDataService.saveDataByUrl(data, '/KnitScPartyRate/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        console.log(res);
                        config.appToastMsg(res.data.PMSG);
                        if (parseInt(res.data.OPSCM_SC_QUOT_RATE_H_ID) > 0) {
                            vm.form.SCM_SC_QUOT_RATE_H_ID = res.data.OPSCM_SC_QUOT_RATE_H_ID;
                            vm.getSupplierRateInfo();
                        }
                    }
                });
            }
        };

    }

})();




