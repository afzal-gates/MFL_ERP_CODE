(function () {
    'use strict';
    angular.module('multitex.mrc').controller('TnaTaskPermissionController', ['$q', 'config', 'MrcDataService', '$stateParams', '$state', '$scope', 'logger', TnaTaskPermissionController]);
    function TnaTaskPermissionController($q, config, MrcDataService, $stateParams, $state, $scope, logger) {

        var vm = this;
        vm.errors = null;
        var ctrl = 'UserBuyerAcc';
        var key = 'MC_USR_BYRACC_ID';

        $scope.CUR_USER = '';
       
        vm.Title = $state.current.Title || '';

        activate()
        vm.showSplash = true;
        function activate() {
            var promise = [getMappedUserList(),getUserData()];
            return $q.all(promise).then(function () {
                vm.showSplash = false;
            });
        }


        vm.getTnaTaskByUserId = function (item) {
            vm.form = { cre: 'N', rea: 'N', upd: 'N', del: 'N', SC_USER_ID: item.SC_USER_ID };
            $("#ListViewUser").find('.k-state-selected').removeClass('k-state-selected');

            $scope.CUR_USER = item.USER_NAME_EN;

            return MrcDataService.getDataByUrl('/TnaTemplate/FindTaskListByUser/' + item.SC_USER_ID).then(function (res) {
                var data = [];
                angular.forEach(res, function (val, key) {
                    var crudArr = val.CRUD_FLAG.split("");
                   

                    if (crudArr.length > 0) {
                        console.log(_.indexOf(crudArr,'D'));

                        val.CRUD_FLAG = [_.indexOf(crudArr, 'C') > -1 ? 'C' : 'N', _.indexOf(crudArr, 'R') > -1 ? 'R' : 'N', _.indexOf(crudArr, 'U') > -1 ? 'U' : 'N', _.indexOf(crudArr, 'D') > -1 ? 'D' : 'N'];

                    } else if (crudArr.length == 0) {
                        val.CRUD_FLAG = ["N", "N", "N", "N"];
                    }

                    data.push(val);

                    
                });

  

                var dataSource = new kendo.data.DataSource({
                    transport: {
                        read: function (e) {
                            e.success(data);
                        }
                    }
                });

                $('#kendoGrid').data("kendoGrid").setDataSource(dataSource);

            }, function (err) {
                console.log(err);
            })

        
        }



        function getMappedUserList() {
            return MrcDataService.getDataByUrl('/TnaTemplate/FindTnaMappedUserList').then(function (res) {
                vm.dataSource = new kendo.data.DataSource({
                    data: res,
                    pageSize:6
                });
            }, function (err) {
                console.log(err);
            });
        }





        vm.saveGridData = function (token) {
            var data = angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
            var data1st = data.map(function (obj) {
                return {
                    CRUD_FLAG: _.remove(obj.CRUD_FLAG, function (n) {
                        return n != 'N'
                    }).join(""),
                    TA_TASK_SL: obj.TA_TASK_SL,
                    TA_TASK_NAME_EN: obj.TA_TASK_NAME_EN,
                    MC_USR_TNA_TASK_ID: obj.MC_USR_TNA_TASK_ID,
                    MC_TNA_TASK_ID: obj.MC_TNA_TASK_ID,
                    IS_ACTIVE: obj.IS_ACTIVE
                }
            });

            var data2nd = data1st.map(function (obj) {
                return {
                    CRUD_FLAG: obj.CRUD_FLAG,
                    MC_USR_TNA_TASK_ID: obj.MC_USR_TNA_TASK_ID,
                    MC_TNA_TASK_ID: obj.MC_TNA_TASK_ID,
                    IS_ACTIVE: obj.MC_USR_TNA_TASK_ID > 0 && obj.CRUD_FLAG == '' ? 'N' : 'Y'
                }
            });


            var dataToBeSave = _.reject(data2nd, function (obj) {
                return obj.CRUD_FLAG == '' && obj.IS_ACTIVE == 'N'
            });

            if (dataToBeSave.length == 0) {
                return;
            } else {
                var xml = MrcDataService.xmlStringShort(dataToBeSave);
               

                return MrcDataService.saveDataByUrl({ XML: xml, SC_USER_ID: vm.form.SC_USER_ID }, '/TnaTemplate/SaveTnaMappedTask', token).then(function (res) {
                    res['data'] = angular.fromJson(res.jsonStr);
                    config.appToastMsg(res.data.PMSG);

                    if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                        vm.getTnaTaskByUserId({ SC_USER_ID: vm.form.SC_USER_ID, USER_NAME_EN: $scope.CUR_USER });
                        getMappedUserList();

                    }

                }, function (err) {
                    console.log(err);
                })
            }

        }


        function getUserData() {
            return   vm.userList = {
                optionLabel: "-- Select User --",
                filter: "startswith",
                autoBind: true,
                valueTemplate: '<span class="selected-value" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span><span>#:data.USER_NAME_EN#</span>',
                template:       '<span class="k-state-default" style="background-image: url(\'/UPLOAD_DOCS/EMP_PHOTOS/#:data.EMPLOYEE_CODE#.jpg\')"></span>' +
                                '<span class="k-state-default"><h3>#: data.USER_NAME_EN #</h3><p>#: data.USER_EMAIL #</p></span>',
                dataSource: {
                    transport: {
                        read: function (e) {
                            MrcDataService.getUserData().then(function (res) {
                                e.success(res);
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }
                },
                change:function(e){
                    var item = this.dataItem(e.item);
                    vm.getTnaTaskByUserId(item);
                },
                height: 400,
                dataTextField: "LOGIN_ID",
                dataValueField: "SC_USER_ID"
            };

        }


        vm.selectAll = function (v, index) {
            var data = angular.copy($('#kendoGrid').data("kendoGrid").dataSource.data());
           

            angular.forEach(data,function(val,key){
                val['CRUD_FLAG'][index] = v;
            
            });

            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        e.success(data);
                    }
                }
            });
            $('#kendoGrid').data("kendoGrid").setDataSource(dataSource);

                

        }

        vm.gridOptions = {
            autoBind: true,
            height: '400px',
            scrollable: true,
            navigatable: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success([]);
                    }
                },
                schema: {
                    model: {
                        fields: {
                            TA_TASK_SL: { type: "number" },
                            TA_TASK_NAME_EN: { type: "string" }
                        }
                    }
                },

            },
            selectable: "cell",
            sortable: true,
            pageSize: 10,
            pageable: false,
            filterable: {
                mode:'row'
            },
            columns: [
                { field: "TA_TASK_SL", title: "SL", type: "string", width: "20px",filterable:false},
                {
                    field: "TA_TASK_NAME_EN", title: "Task Name", width: "120px",
                    filterable: {
                        cell: {
                            operator: "contains"
                        }
                    }
                },

                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.cre' ng-click='vm.selectAll(vm.cre,0)'  ng-true-value='\"C\"' ng-false-value='\"N\"'> Create",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.CRUD_FLAG[0]' ng-true-value='\"C\"' ng-false-value='\"N\"'>";
                    },
                    width: "20px"
                },
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.rea' ng-click='vm.selectAll(vm.rea,1)'  ng-true-value='\"R\"' ng-false-value='\"N\"'> Read",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.CRUD_FLAG[1]' ng-true-value='\"R\"' ng-false-value='\"N\"'>";
                    },
                    width: "20px"
                },
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.upd' ng-click='vm.selectAll(vm.upd,2)'  ng-true-value='\"U\"' ng-false-value='\"N\"'> Update",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.CRUD_FLAG[2]' ng-true-value='\"U\"' ng-false-value='\"N\"'>";
                    },
                    width: "20px"
                },
                {
                    headerTemplate: "<input type='checkbox' ng-model='vm.del' ng-click='vm.selectAll(vm.del,3)'  ng-true-value='\"D\"' ng-false-value='\"N\"'> Delete",
                    template: function () {
                        return "<input type='checkbox' ng-model='dataItem.CRUD_FLAG[3]' ng-true-value='\"D\"' ng-false-value='\"N\"'>";
                    },
                    width: "20px"
                }


            ]
        };

    }

})();