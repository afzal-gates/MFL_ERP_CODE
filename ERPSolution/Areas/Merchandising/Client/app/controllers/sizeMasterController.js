(function () {
    'use strict';
    angular.module('multitex.mrc').controller('SizeMasterController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', 'formData', 'AllSizeDatas', SizeMasterController]);
    function SizeMasterController($q, config, MrcDataService, $stateParams, $state, $scope, logger, formData, AllSizeDatas) {

        var vm = this;
        vm.errors = null;
        vm.Title = $state.current.Title || '';

        vm.form = formData.MC_SIZE_ID ? formData : { IS_ACTIVE : 'Y'};

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }



        vm.submitSizeData = function (data, token) {

            if (angular.isDefined(data.MC_SIZE_ID) && data.MC_SIZE_ID > 0) {

                return MrcDataService.updateSizeData(data, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        $state.go($state.current, $stateParams.current, { reload: true });
                        config.appToastMsg(res.data.PMSG);
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            } else {

                return MrcDataService.saveSizeData(data, token).then(function (res) {
                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        res['data'] = angular.fromJson(res.jsonStr);
                        config.appToastMsg(res.data.PMSG);
                        if (res.data.V_MC_SIZE_ID != 0) {
                            $state.go("SizeMaster", { MC_SIZE_ID: res.data.V_MC_SIZE_ID });
                        }
                    }
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
            }

        }

        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(AllSizeDatas);
                    }
                },
                pageSize: 10
            },
            filterable: true,
            selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "MC_SIZE_ID", title: "Size ID", type: "number", width: "50px" },
                { field: "SIZE_CODE", title: "Code", type: "string", width: "80px" },
                { field: "SIZE_NAME_EN", title: "Size Name(En)", type: "string", width: "80px" },
                { field: "IS_ACTIVE", title: "Is Active?", type: "string", width: "50px" },

                {
                    title: "Action",
                    template: function () {
                        return "</a><a ui-sref='SizeMaster({MC_SIZE_ID:dataItem.MC_SIZE_ID})' class='btn btn-xs blue'><i class='fa fa-edit'> Edit</i></a>";
                    },
                    width: "50px"
                }
            ]
        };

    }

})();