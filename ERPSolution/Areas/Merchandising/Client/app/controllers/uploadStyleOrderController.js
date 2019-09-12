(function () {
    'use strict';
    angular.module('multitex.mrc').controller('UploadStyleOrder', ['$q', '$stateParams', '$state', '$scope', '$http', '$localStorage', 'CoreFileUploadService', 'config', 'exception', UploadStyleOrder]);
    function UploadStyleOrder($q, $stateParams, $state, $scope, $http, $localStorage, CoreFileUploadService, config, exception) {

        var vm = this;
        vm.Title = $state.current.Title || '';
        vm.errors = null;
        
        vm.file = { UPLOAD_FORMAT_ID: 1 };
        vm.uploadFormatList = [{ UPLOAD_FORMAT_ID: 1, UPLOAD_FORMAT_NAME: 'Style and Order Detail' }, { UPLOAD_FORMAT_ID: 2, UPLOAD_FORMAT_NAME: 'Style Fabrication' }];
        
        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }
       
        vm.format01GridOption = {
            height: 450,            
            sortable: true,
            pageable: false,
            //scrollable: {
            //    virtual: true,
            //    //scrollable:true
            //},
            filterable: false,
            columns: [
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer A/C Group", width: "150px" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "150px" },
                { field: "BUYER_NAME_EN", title: "Buyer", width: "150px" },
                { field: "STYLE_NO", title: "Style#", width: "150px" },
                { field: "STYLE_DESC", title: "Style Desc", width: "150px" },
                { field: "HAS_SET", title: "Set?", width: "100px" },
                { field: "PCS_PER_PACK", title: "Pcs Per Pack", width: "100px" },
                { field: "ORDER_NO", title: "Order#", width: "150px" },
                { field: "ORD_CONF_DT", title: "Confirm DT", width: "150px" },
                { field: "SHIP_DT", title: "Ship DT", width: "150px" },
                { field: "SHIP_COUNTRY", title: "Ship Country", width: "150px" },
                { field: "REVISION_NO", title: "Rev#", width: "100px" },
                { field: "REVISION_DT", title: "Rev DT", width: "150px" },
                { field: "REV_REASON", title: "Rev Reason", width: "150px" },
                { field: "ITEM_CAT_NAME_EN", title: "Category", width: "150px" },
                { field: "ITEM_NAME_EN", title: "Item", width: "150px" },
                { field: "PARENT_ITEM_NAME_EN", title: "Parent Item", width: "150px" },
                { field: "GMT_TYPE", title: "GMT Type", width: "150px" },
                { field: "COLOR_NAME_EN", title: "Color", width: "150px" },
                { field: "SIZE_CODE", title: "Size", width: "150px" },
                { field: "SIZE_QTY", title: "Qty", width: "150px" }
            ]
        };


        vm.format02GridOption = {
            height: 450,
            sortable: true,
            pageable: false,
            //scrollable: {
            //    virtual: true,
            //    //scrollable:true
            //},
            filterable: false,
            columns: [
                { field: "BYR_ACC_GRP_NAME_EN", title: "Buyer A/C Group", width: "150px" },
                { field: "BYR_ACC_NAME_EN", title: "Buyer A/C", width: "150px" },
                { field: "BUYER_NAME_EN", title: "Buyer", width: "150px" },
                { field: "STYLE_NO", title: "Style#", width: "150px" },
                { field: "FABRIC_DESC", title: "Fabric", width: "350px" }
            ]
        };
        

        vm.format01GridDataSource = new kendo.data.DataSource({
            data: []
        });

        vm.format02GridDataSource = new kendo.data.DataSource({
            data: []
        });

        function getExtention(fileName) {
            var i = fileName.lastIndexOf('.');
            if (i === -1) return false;
            return fileName.slice(i)
        }

        
        vm.nextData = function () {
            if ((vm.fromRecordNo + vm.maxRecShow) > vm.totalRecord)
            {
                return;
            }

            vm.showSplash = true;
            vm.fromRecordNo = vm.fromRecordNo + vm.maxRecShow;


            vm.format01GridDataSource = new kendo.data.DataSource({
                data: []
            });

            vm.format02GridDataSource = new kendo.data.DataSource({
                data: []
            });

            var data = { UPLOAD_FORMAT_ID: vm.file.UPLOAD_FORMAT_ID, SHOW_FROM_REC_NO: vm.fromRecordNo, MAX_REC_SHOW: vm.maxRecShow, ATT_FILE: vm.file.ATT_FILE };

            return CoreFileUploadService.save(data, '/Merchandising/Mrc/ShowStyleOrderFileData', vm.antiForgeryToken).then(function (res) {
                $scope.errors = undefined;

                console.log(res);

                if (res.success === false) {
                    $scope.errors = [];
                    $scope.errors = res.data.errors;
                }
                else {

                    if (data.UPLOAD_FORMAT_ID == 1) {
                        angular.forEach(res.data, function (val, key) {
                            vm.format01GridDataSource.add(val);
                        });
                    }
                    else if (data.UPLOAD_FORMAT_ID == 2) {
                        angular.forEach(res.data, function (val, key) {
                            vm.format02GridDataSource.add(val);
                        });
                    }
                    //config.appToastMsg(res.data.PMSG);

                    vm.showSplash = false;
                }

            }, function (err) {
                console.log(err);
            });
        };

        vm.previousData = function () {
            if (vm.fromRecordNo < 1) {
                return;
            }

            vm.showSplash = true;
            vm.fromRecordNo = vm.fromRecordNo - vm.maxRecShow;


            vm.format01GridDataSource = new kendo.data.DataSource({
                data: []
            });

            vm.format02GridDataSource = new kendo.data.DataSource({
                data: []
            });

            var data = { UPLOAD_FORMAT_ID: vm.file.UPLOAD_FORMAT_ID, SHOW_FROM_REC_NO: vm.fromRecordNo, MAX_REC_SHOW: vm.maxRecShow, ATT_FILE: vm.file.ATT_FILE };

            return CoreFileUploadService.save(data, '/Merchandising/Mrc/ShowStyleOrderFileData', vm.antiForgeryToken).then(function (res) {
                $scope.errors = undefined;

                console.log(res);

                if (res.success === false) {
                    $scope.errors = [];
                    $scope.errors = res.data.errors;
                }
                else {

                    if (data.UPLOAD_FORMAT_ID == 1) {
                        angular.forEach(res.data, function (val, key) {
                            vm.format01GridDataSource.add(val);
                        });
                    }
                    else if (data.UPLOAD_FORMAT_ID == 2) {
                        angular.forEach(res.data, function (val, key) {
                            vm.format02GridDataSource.add(val);
                        });
                    }
                    //config.appToastMsg(res.data.PMSG);

                    vm.showSplash = false;
                }

            }, function (err) {
                console.log(err);
            });
        };

        vm.fromRecordNo = 0;
        vm.maxRecShow = 20;
        vm.showStyleOrderFileData = function () {
            vm.showSplash = true;
            vm.fromRecordNo = 0;


            vm.format01GridDataSource = new kendo.data.DataSource({
                data: []
            });

            vm.format02GridDataSource = new kendo.data.DataSource({
                data: []
            });

            var data = { UPLOAD_FORMAT_ID: vm.file.UPLOAD_FORMAT_ID, SHOW_FROM_REC_NO: vm.fromRecordNo, MAX_REC_SHOW: vm.maxRecShow, ATT_FILE: vm.file.ATT_FILE };

            return CoreFileUploadService.save(data, '/Merchandising/Mrc/ShowStyleOrderFileData', vm.antiForgeryToken).then(function (res) {
                $scope.errors = undefined;

                console.log(res);
                vm.totalRecord = res.totalRecord;

                if (res.success === false) {
                    $scope.errors = [];
                    $scope.errors = res.data.errors;
                }
                else {

                    if (data.UPLOAD_FORMAT_ID == 1) {
                        angular.forEach(res.data, function (val, key) {
                            vm.format01GridDataSource.add(val);
                        });
                    }
                    else if (data.UPLOAD_FORMAT_ID == 2) {
                        angular.forEach(res.data, function (val, key) {
                            vm.format02GridDataSource.add(val);                            
                        });
                    }
                    //config.appToastMsg(res.data.PMSG);

                    vm.showSplash = false;
                }

            }, function (err) {
                console.log(err);
            });
        };

        vm.submitData = function (data) {
            vm.showSplash = true;
            var data = { UPLOAD_FORMAT_ID: vm.file.UPLOAD_FORMAT_ID, ATT_FILE: vm.file.ATT_FILE };
            
            return CoreFileUploadService.save(data, '/Merchandising/Mrc/UploadStyleOrderFileData', vm.antiForgeryToken).then(function (res) {
                $scope.errors = undefined;

                console.log(res);

                if (res.success === false) {
                    $scope.errors = [];
                    $scope.errors = res.data.errors;
                }
                else {

                    res['data'] = angular.fromJson(res.jsonStr);

                    //if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                    //    vm.docGridDataSource.read();
                    //};

                    config.appToastMsg(res.data.PMSG);
                }

                vm.showSplash = false;
            }, function (err) {
                console.log(err);
            });
                
        };

    }

})();