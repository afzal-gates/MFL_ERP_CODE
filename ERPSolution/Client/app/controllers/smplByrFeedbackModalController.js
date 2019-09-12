(function () {
    'use strict';
    angular.module('multitex.mrc').controller('SmplByrFeedbackModalController', ['logger', 'config', '$q', '$scope', '$http', 'exception', '$filter', '$timeout', '$state', '$stateParams', '$modalInstance', 'smplByrFeedbackList', SmplByrFeedbackModalController]);

    function SmplByrFeedbackModalController(logger, config, $q, $scope, $http, exception, $filter, $timeout, $state, $stateParams, $modalInstance, smplByrFeedbackList) {
        var vm = this;
        vm.showSplash = true;
        vm.errors = null;
        vm.Title = $state.current.Title || '';
        vm.dtFormat = config.appDateFormat;
        
        vm.today = new Date();
        vm.form = {};
        console.log(smplByrFeedbackList);
        
        activate();
        function activate() {
            var promise = [getByrFeedBackData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        };

        
        $scope.smplFeedbackGridOption = {            
            //pageable: true, 
            height: 450,
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
                { field: "ITEM_SNAME", title: "Item", type: "string" },
                { field: "COLOR_NAME_EN", title: "Color", type: "string" },
                { field: "SIZE_CODE", title: "Size", type: "string" },
                { field: "REVISION_NO", title: "Revision#", type: "string" },
                { field: "EVENT_NAME", title: "Status", type: "string" },
                { field: "COMMENTS", title: "Comments", type: "string" }
            ]          
        };


        function getByrFeedBackData() {           
            $scope.smplFeedbackGridDataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        e.success(smplByrFeedbackList);
                    }
                }//,
                //pageSize: 10
            });

        };
        
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
       

    }

})();