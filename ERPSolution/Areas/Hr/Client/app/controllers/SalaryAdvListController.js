(function () {
    'use strict';
    angular.module('multitex.hr').controller('SalaryAdvListController', ['config', 'HrService', '$scope', 'logger', '$modal', '$q', '$http', SalaryAdvListController]);
    function SalaryAdvListController(config, HrService, $scope, logger, $modal, $q, $http) {

        var vm = this;
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.gridOptions = {
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        $http({
                            method: 'get',
                            url: "/Hr/HrSalAdvance/CurrentSalaryAdvanceList"  //+ "&pType=" + showType
                        }).
                        success(function (data, status, headers, config) {
                            e.success(data)
                        }).
                        error(function (data, status, headers, config) {
                            alert('something went wrong')
                            console.log(status);
                        });
                    }
                },
                pageSize: 10
            },
            filterable: true,
            //selectable: "row",
            sortable: true,
            pageSize: 10,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [
                { field: "EMPLOYEE_CODE", title: "Card #", type: "string", width: "80px" },
                { field: "EMP_FULL_NAME_EN", title: "Name", type: "string", width: "150px", filterable: false },
                { field: "DEPARTMENT_NAME_EN", title: "Department", type: "string", width: "100px" },
                { field: "DESIGNATION_NAME_EN", title: "Designation", type: "string", width: "100px" },
                { field: "LK_ADV_TYPE", title: "Type", type: "string", width: "100px" },

                { field: "ADV_APRV_AMT", title: "ApprvAmt", type: "number", width: "80px", filterable: false },

                { field: "NO_OF_INSTL", title: "#OfInstl", type: "number", width: "70px", filterable: false },
                { field: "TOT_INSTL_PAID", title: "InstlPaid", type: "number", width: "70px", filterable: false },
                { field: "TOT_AMT_PAID", title: "TtlAmtPaid", type: "number", width: "70px", filterable: false },
                
                {
                    title: "Action",
                    template: function () {
                        return "<a ui-sref='ManualSalaryAdvReq({i:dataItem.HR_SAL_ADVANCE_ID})' class='btn btn-xs purple'><i class='fa fa-edit'> Edit</i></a> <a ng-click='vm.openModalForMakingSchedule(dataItem)' class='btn btn-xs blue'><i class='fa fa-cogs'> Slab</i></a>";
                    },
                    width: "100px"
                }
            ]
        };


        vm.openModalForMakingSchedule = function (data) {
            console.log(data);

            if (data.items.length == 0 && data.NO_OF_SLAB == 1) {
                data.items.push({ HR_SAL_LN_PAY_SLB_ID: -1, SLAB_NO: 1, NO_OF_INSTL: data.NO_OF_INSTL, INSTL_AMT: data.INSTL_AMT });
            } else if (data.items.length == 0 && data.NO_OF_SLAB > 1) {
                var i = angular.copy(data.NO_OF_SLAB);
                var j = 1;
                while (0 < i) {
                    data.items.push({ HR_SAL_LN_PAY_SLB_ID: -1 * j, SLAB_NO: j, NO_OF_INSTL: null, INSTL_AMT: null });
                    i = i - 1;
                    j++;
                }
            }
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'AdvSalaryModal.html',
                controller: 'LoanSlabModalController',
                windowClass: 'large-Modal',
                resolve: {
                    item: function () {
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



        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

})();