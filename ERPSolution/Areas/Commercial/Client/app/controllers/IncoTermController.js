
(function () {
    'use strict';
    angular.module('multitex.cmr').controller('IncoTermController', ['$q', 'config', 'CmrDataService', '$stateParams', '$state', '$rootScope', '$scope', 'cur_user_id', '$filter', 'Dialog', IncoTermController]);
    function IncoTermController($q, config, CmrDataService, $stateParams, $state, $rootScope, $scope, cur_user_id, $filter, Dialog) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        vm.today = new Date();

        vm.form = { REPORT_CODE: '', IS_ACTIVE: 'Y' };
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [loadData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }

        vm.clearAll = function (data) {

            vm.form = { IS_ACTIVE: 'Y' };
        }

        vm.editItemData = function (data) {
            vm.form = angular.copy(data);
        }

        vm.removeData = function (data) {

            Dialog.confirm('Finalizing "' + data.INCO_TERM_NAME_EN + '.', 'Attention', ['Yes', 'No'])
                 .then(function () {
                     var ndata = angular.copy(data);
                     return CmrDataService.saveDataByUrl(ndata, '/ReferenceTyp/DeleteIT').then(function (res) {

                         if (res.success === false) {
                             vm.errors = res.errors;
                         }
                         else {
                             res['data'] = angular.fromJson(res.jsonStr);
                             config.appToastMsg(res.data.PMSG);
                             loadData();
                         }
                     });
                 });
        }


        function loadData() {
            CmrDataService.getDataByFullUrl('/api/cmr/ReferenceTyp/GetIncoTerm').then(function (res) {

                vm.NotifyList = new kendo.data.DataSource({
                    data: res
                });
            });
        }

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

                { field: "RF_INCO_TERM_ID", hidden: true },

                { field: "INCO_TERM_CODE", title: "Code", width: "10%" },
                { field: "INCO_TERM_NAME_EN", title: "Name(EN)", width: "10%" },
                { field: "INCO_TERM_NAME_BN", title: "Name(BN)", width: "10%" },
                { field: "INCO_TERM_SNAME", title: "Short Name", width: "10%" },
                { field: "DISPLAY_ORDER", title: "Display Order", width: "10%" },
                { field: "IS_ACTIVE", title: "Status", width: "10%" },
                 
                {
                    title: "",
                    template: '<a  title="Delete" ng-click="vm.removeData(dataItem)" class="btn btn-xs red"><i class="fa fa-times-circle"></i></a>  <a  title="Edit" ng-click="vm.editItemData(dataItem)" class="btn btn-xs green"><i class="fa fa-edit"></i></a>',
                    width: "10%"
                }
            ],
        };



        vm.submitAll = function (dataOri) {

            if (fnValidate() == true) {

                var data = angular.copy(dataOri);

                var id = vm.form.RF_INCO_TERM_ID;

                return CmrDataService.saveDataByUrl(data, '/ReferenceTyp/SaveIT').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {

                        res['data'] = angular.fromJson(res.jsonStr);

                        if (id > 0) {
                            config.appToastMsg(res.data.PMSG);
                        }
                        else {
                            config.appToastMsg(res.data.PMSG);
                        }
                        loadData();

                    }
                });
            }
        };

    }

})();