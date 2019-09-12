(function () {
    'use strict';

    angular.module('multitex.security').controller('ReportTemplateController', ['$state', 'logger', 'config', '$q', '$scope', '$http', '$stateParams', '$filter', 'SecurityService', ReportTemplateController]);
    function ReportTemplateController($state, logger, config, $q, $scope, $http, $stateParams, $filter, SecurityService) {

        var vm = this;

        $scope.format = config.appDateFormat;
        vm.currentDate = $filter('date')(new Date(), config.appDateFormat);

        vm.form = {};
        vm.errors = {};

        activate();
        vm.Title = $state.current.Title || '';
        vm.showSplash = true;

        function activate() {
            var promise = [getBuyerList(), getreportTypeList(), loadTemplateList()];
            return $q.all(promise).then(function () {
                //logger.success(config.appTitle + ' : Admin module successfully loaded.', null);
                vm.showSplash = false;
            });
        }

        vm.editItem = function (dataItem) {
            var item = angular.copy(dataItem);
            vm.form = item;
            getBuyerList();
        }

        function getBuyerList() {

            SecurityService.getDataByFullUrl('/api/security/RptTmp/GetTemplateBuyerListByID?pRF_RPT_TMPLT_ID=' + (vm.form.RF_RPT_TMPLT_ID || 0)).then(function (res) {
                vm.buyerList = res;
            });

        }

        function getreportTypeList() {
            return vm.reportTypeList = {
                optionLabel: "-- Report Type --",
                filter: "contains",
                autoBind: true,
                dataSource: {
                    transport: {
                        read: function (e) {
                            SecurityService.LookupListData(119).then(function (res) {
                                e.success(res);
                            });
                        }
                    }
                },
                dataTextField: "LK_DATA_NAME_EN",
                dataValueField: "LOOKUP_DATA_ID"
            };
        };


        function loadTemplateList() {

            return vm.gridOptionsDS = new kendo.data.DataSource({
                serverPaging: false,
                serverSorting: false,
                serverFiltering: false,
                transport: {
                    read: function (e) {
                        vm.showSplash = true;
                        SecurityService.getDataByFullUrl('/api/security/RptTmp/SelectAll').then(function (res) {
                            e.success(res);
                            vm.showSplash = false;
                        });
                    }
                },
            })
        }

        vm.gridOptions = {
            //autoBind: true,            
            sortable: false,
            pageable: false,
            columns: [
                { field: "RF_RPT_TMPLT_ID", hidden: true },
                { field: "LK_RPT_DOC_TYPE_ID", hidden: true },

                { field: "RPT_TMPLT_CODE", title: "Code", width: "10%" },
                { field: "RPT_TMPLT_NAME", title: "Template Name", width: "30%" },
                { field: "RPT_PATH_URL", title: "Report Path", width: "30%" },
                { field: "BUYER_NAME_LST", title: "Buyer Name List", width: "30%" },

                {
                    title: "Action",
                    template: function () {
                        return "</a><a ng-click='vm.editItem(dataItem);' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a> <a ng-click='vm.deleteItem(dataItem);' class='btn btn-xs red'><i class='fa fa-remove'> Delete</i></a></a>";
                    },
                    width: "10%"
                }
            ],
        };

        vm.submitData = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var list = _.filter(vm.buyerList, function (x) { return x.IS_SELECT == "Y" });
                data["XML_TMP_D"] = SecurityService.xmlStringShort(list.map(function (o) {
                    return {
                        RF_RPT_TMPLT_BYR_ID: o.RF_RPT_TMPLT_BYR_ID == null ? 0 : o.RF_RPT_TMPLT_BYR_ID,
                        MC_BUYER_ID: o.MC_BUYER_ID == null ? 0 : o.MC_BUYER_ID,
                    }
                }));

                return SecurityService.saveDataByUrl(data, '//RptTmp/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        vm.gridOptionsDS.read();
                    }
                });
            }
        }
    }


})();