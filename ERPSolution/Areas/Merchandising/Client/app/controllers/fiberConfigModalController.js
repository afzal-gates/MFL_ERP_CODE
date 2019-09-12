(function () {
    'use strict';
    angular.module('multitex.mrc').controller('FiberConfigModalController', ['$scope', '$modalInstance', 'MrcDataService', 'FiberTypeList', 'Template', 'TMPLT_H_ID', 'config', FiberConfigModalController]);
    function FiberConfigModalController($scope, $modalInstance, MrcDataService, FiberTypeList, Template, TMPLT_H_ID, config) {
        $scope.tempChange = false;
        $scope.IS_ELA_MXD = 'N';
        $scope.CombiShwn = false;
        $scope.showSplash = false;
        $scope.duplicatedCombi = false;
        var combine = function (a, min) {
            var maxLen = 3;
            var fn = function (n, src, got, all) {
                if (n == 0) {
                    if (got.length > 0 && got.length <= maxLen) {
                        all[all.length] = got;
                    }
                    return;
                }
                for (var j = 0; j < src.length; j++) {
                    fn(n - 1, src.slice(j + 1), got.concat([src[j]]), all);
                }
                return;
            }
            var all = [];
            for (var i = min; i < a.length; i++) {
                fn(i, a, [], all);
            }
            if (a.length <= maxLen) {
                all.push(a);
            }

            return all;
        }

        var setCntConfDatas = function (ID) {
            if (!ID) {
                $scope.datas = [{
                    MC_FIB_COMB_CFG_ID: -1,
                    IS_BLEND_A_F: '',
                    IS_100PCT: 'Y',
                    IS_ELA_MXD: 'N',
                    IS_100PCT_ELA: 'N',
                    LK_FIB_TYPE_LST: null,
                    FIB_COMB_LST: []
                }];
            } else {
                return MrcDataService.getDataByUrl('/YarnSpec/FiberCombConfigData?pOption=3002&pMC_FIB_COMB_TMPLT_ID=' + ID).then(function (res) {

                    $scope.MC_FIB_COMB_TMPLT_ID = ID;

                    $scope.FIB_COMB_TMPLT_NAME = $scope.FIB_COMB_TMPLT_NAME || _.filter(Template, function (o) {
                        return parseInt(o.MC_FIB_COMB_TMPLT_ID) == ID;
                    })[0].FIB_COMB_TMPLT_NAME;

                    $scope.IS_ELA_MXD = _.filter(Template, function (o) {
                        return parseInt(o.MC_FIB_COMB_TMPLT_ID) == ID;
                    })[0].IS_ELA_MXD;

                    $scope.datas = res.map(function (o) {
                        o.LK_FIB_TYPE_LST = o.LK_FIB_TYPE_LST.split(',');
                        return o;
                    })

                }, function (err) {
                    console.error(err);
                })
            }
        };


        $scope.indexArryFiber = _.remove(_.map(FiberTypeList, 'LOOKUP_DATA_ID'), function (n) {
            if (n == 375) {
                return false;
            }
            return true;
        });

        $scope.addNew = function () {
            $scope.datas.push({
                MC_FIB_COMB_CFG_ID: -1,
                IS_BLEND_A_F: '',
                IS_100PCT: 'Y',
                IS_ELA_MXD: 'N',
                IS_100PCT_ELA :'N',
                LK_FIB_TYPE_LST: null,
                FIB_COMB_LST: []
            });
        };
        setCntConfDatas(TMPLT_H_ID);
        //New 
        $scope.cancel = function () {
            $modalInstance.close({
                MC_FIB_COMB_TMPLT_ID: $scope.MC_FIB_COMB_TMPLT_ID,
                FIB_COMB_TMPLT_NAME: $scope.FIB_COMB_TMPLT_NAME,
                IS_ELA_MXD: $scope.IS_ELA_MXD
            });
        };

        $scope.removeIt = function (index) {
            $scope.datas.splice(index, 1);
        }

        $scope.FiberTypeList = {
            placeholder: "--Fiber--",
            valuePrimitive: true,
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(_.filter(FiberTypeList, function (o) {
                            return o.LOOKUP_DATA_ID != 375;
                        }));
                    }
                }
            },
            dataTextField: "LK_DATA_NAME_EN",
            dataValueField: "LOOKUP_DATA_ID"
        };

        $scope.BlendType = {
            optionLabel: "N/A",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success([{
                            Text: 'Any',
                            Value: 'A'
                        },
                        {
                            Text: 'Fixed',
                            Value: 'F'
                        }
                        ]);
                    }
                }
            },
            dataTextField: "Text",
            dataValueField: "Value"
        };

        //Start New
        $scope.TemplateList = {
            optionLabel: "--New Template--",
            filter: "contains",
            autoBind: true,
            dataSource: {
                transport: {
                    read: function (e) {
                        e.success(Template)
                    }
                }
            },
            select: function (e) {
                var item = this.dataItem(e.item);
                if (item.MC_FIB_COMB_TMPLT_ID) {
                    $scope.FIB_COMB_TMPLT_NAME = item.FIB_COMB_TMPLT_NAME;
                    $scope.IS_ELA_MXD = item.IS_ELA_MXD;
                    setCntConfDatas(item.MC_FIB_COMB_TMPLT_ID);
                } else {
                    setCntConfDatas();
                    $scope.FIB_COMB_TMPLT_NAME = '';
                    $scope.IS_ELA_MXD = 'N';
                }
            },
            dataTextField: "FIB_COMB_TMPLT_NAME",
            dataValueField: "MC_FIB_COMB_TMPLT_ID"
        };
        //End New

        $scope.onChangeElaMixed = function (item) {
            $scope.currFiberList = angular.copy(item.LK_FIB_TYPE_LST);
            if (item.IS_ALL === 'Y') {
                $("#LK_FIB_TYPE_LST-" + item.index).data("kendoMultiSelect").value($scope.indexArryFiber);
                item.LK_FIB_TYPE_LST = $scope.indexArryFiber;
                item.IS_BLEND_A_F = 'A';
            } else {
                $("#LK_FIB_TYPE_LST-" + item.index).data("kendoMultiSelect").value([]);
                item.LK_FIB_TYPE_LST = null;
                item.IS_BLEND_A_F = '';
            }

        };

        var getMC_FIB_COMB_LST_ID = function (data, COMBINATION) {
            if (data.length == 0 ) return -1;
            var d = _.find(data, function (o) {
                return o.FIB_COMBINATION_ID === COMBINATION;
            });

            return d ? d.MC_FIB_COMB_LST_ID : -1;
        };

        $scope.save = function (formData, token, valid, MC_FIB_COMB_TMPLT_ID, FIB_COMB_TMPLT_NAME) {
            if (!valid) {
                return;
            }
            var formDataOri = angular.copy(formData);
            var allFibCombi = [];
            var allFibCombiDubFree = [];
            $scope.IS_ELA_MXD = _.some(formDataOri, function (o) { return (o.IS_ELA_MXD == 'Y') || (o.IS_100PCT_ELA == 'Y') }) ? 'Y' : 'N';

            if (formDataOri.length > 0) {

                $scope.showSplash = true;
                angular.forEach(formDataOri, function (val, key) {
                    var combi = '';
                    var subCombinationList = [];
                    var FIB_COMB_LST = [];
                    if (val.LK_FIB_TYPE_LST.length > 0) {
                        
                        //Only 100% Fiber
                        if (val.IS_100PCT && val.IS_100PCT == 'Y') {
                            val.LK_FIB_TYPE_LST.forEach(function (v) {
                                FIB_COMB_LST.push({
                                    C: v,
                                    xml: '#row C=*' + v + '*  I=*' + getMC_FIB_COMB_LST_ID(val.FIB_COMB_LST, v) + '* /$'
                                });
                            });
                        }

                        // Any Blend
                        if (val.IS_BLEND_A_F != 'F' && val.IS_BLEND_A_F && val.IS_BLEND_A_F == 'A') {

                            combine(val.LK_FIB_TYPE_LST, 2).forEach(function (v) {
                                combi = v.join(',');
                                FIB_COMB_LST.push({
                                    C: combi,
                                    xml: '#row C=*' + combi + '*  I=*' + getMC_FIB_COMB_LST_ID(val.FIB_COMB_LST, combi) + '* /$'
                                });
                                combi = '';
                            });

                            combi = '';
                        }

                        // Elastence Blend

                        if (val.IS_BLEND_A_F != 'F' && val.IS_ELA_MXD == 'Y') {

                            combine(val.LK_FIB_TYPE_LST, 2).forEach(function (v) {
                                combi = v.join(',');
                                subCombinationList.push({
                                    C: combi,
                                    I: getMC_FIB_COMB_LST_ID(val.FIB_COMB_LST, combi)
                                });
                                combi = '';
                            });
                        }


                        // 100% Fiber + Lycra Combination

                        if ( val.IS_BLEND_A_F != 'F' && val.IS_100PCT_ELA == 'Y') {
                            val.LK_FIB_TYPE_LST.forEach(function (v) {
                                combi = v;
                                subCombinationList.push({
                                    C: combi,
                                    I: getMC_FIB_COMB_LST_ID(val.FIB_COMB_LST, combi)
                                });
                                combi = '';
                            });
                        }

                        // Fixed Combination
                        if (val.IS_BLEND_A_F && val.IS_BLEND_A_F == 'F' && val.IS_ELA_MXD == 'N') {
                            combi = val.LK_FIB_TYPE_LST.join(',');
                            FIB_COMB_LST.push({
                                C: combi,
                                xml: '#row C=*' + combi + '*  I=*' + getMC_FIB_COMB_LST_ID(val.FIB_COMB_LST, combi) + '* /$'
                            });
 
                            combi = '';
                        }

                        if (val.IS_BLEND_A_F && val.IS_BLEND_A_F == 'F' && val.IS_ELA_MXD == 'Y') {
                            combi = val.LK_FIB_TYPE_LST.join(',') + ',375';

                            FIB_COMB_LST.push({
                                C: combi,
                                xml: '#row C=*' + combi + '*  I=*' + getMC_FIB_COMB_LST_ID(val.FIB_COMB_LST, combi) + '* /$'
                            }); 
                            combi = '';
                        }
 

                        /// Lycra Mixing Fn
                        if ((val.IS_ELA_MXD == 'Y' || val.IS_100PCT_ELA == 'Y') && val.IS_BLEND_A_F != 'F' ) {
                            subCombinationList.forEach(function (v) {
                                combi = v.C + ',375';
                                FIB_COMB_LST.push({
                                    C: combi,
                                    xml: '#row C=*' + combi + '*  I=*' + getMC_FIB_COMB_LST_ID(val.FIB_COMB_LST, combi) + '* /$'
                                });
                            });
                            combi = '';
                        }


                        var i, j, chunk = 100,k=0;

                        for (i = 0, j = FIB_COMB_LST.length; i < j; i += chunk) {
                            val['FIB_COMB_LST_XML_' + k] = '';
                            angular.forEach(FIB_COMB_LST.slice(i, i + chunk),function(val10,key10){
                                val['FIB_COMB_LST_XML_' + k] += val10.xml;
                            })

                            k += 1;
                        }

                      
                        allFibCombi = allFibCombi.concat(FIB_COMB_LST)
                    }
                });

                allFibCombiDubFree = _.uniqBy(allFibCombi, 'C');
                 
                if (allFibCombi.length != allFibCombiDubFree.length) {
                    $scope.duplicatedCombi = true;
                    $scope.showSplash = false;
                    return;
                } else {
                    $scope.duplicatedCombi = false;
                }
            }

            var data2bSave = {
                MC_FIB_COMB_TMPLT_ID: MC_FIB_COMB_TMPLT_ID || -1,
                FIB_COMB_TMPLT_NAME: FIB_COMB_TMPLT_NAME,
                IS_ELA_MXD : $scope.IS_ELA_MXD,
                XML: MrcDataService.xmlStringShort(
                            formDataOri.map(function (o) {
                                return {
                                    MC_FIB_COMB_CFG_ID: o.MC_FIB_COMB_CFG_ID,
                                    IS_100PCT: o.IS_100PCT,
                                    IS_BLEND_A_F: o.IS_BLEND_A_F,
                                    IS_ELA_MXD: o.IS_ELA_MXD,
                                    IS_100PCT_ELA : o.IS_100PCT_ELA,
                                    LK_FIB_TYPE_LST: o.LK_FIB_TYPE_LST.join(','),
                                    FIB_COMB_LST_XML_0: o.hasOwnProperty('FIB_COMB_LST_XML_0') ? o.FIB_COMB_LST_XML_0 : '',
                                    FIB_COMB_LST_XML_1: o.hasOwnProperty('FIB_COMB_LST_XML_1')? o.FIB_COMB_LST_XML_1 :'',
                                    FIB_COMB_LST_XML_2: o.hasOwnProperty('FIB_COMB_LST_XML_2')? o.FIB_COMB_LST_XML_2 :'',
                                    FIB_COMB_LST_XML_3: o.hasOwnProperty('FIB_COMB_LST_XML_3')? o.FIB_COMB_LST_XML_3 :'',
                                    FIB_COMB_LST_XML_4: o.hasOwnProperty('FIB_COMB_LST_XML_4') ? o.FIB_COMB_LST_XML_4 : '',
                                    FIB_COMB_LST_XML_5: o.hasOwnProperty('FIB_COMB_LST_XML_5')? o.FIB_COMB_LST_XML_5 :'',
                                    FIB_COMB_LST_XML_6: o.hasOwnProperty('FIB_COMB_LST_XML_6')? o.FIB_COMB_LST_XML_6 :'',
                                    FIB_COMB_LST_XML_7: o.hasOwnProperty('FIB_COMB_LST_XML_7')? o.FIB_COMB_LST_XML_7 :'',
                                    FIB_COMB_LST_XML_8: o.hasOwnProperty('FIB_COMB_LST_XML_8')? o.FIB_COMB_LST_XML_8 :'',
                                    FIB_COMB_LST_XML_9: o.hasOwnProperty('FIB_COMB_LST_XML_9')? o.FIB_COMB_LST_XML_9 :'',
                                    FIB_COMB_LST_XML_10: o.hasOwnProperty('FIB_COMB_LST_XML_10')? o.FIB_COMB_LST_XML_10 :'',
                                    FIB_COMB_LST_XML_11: o.hasOwnProperty('FIB_COMB_LST_XML_11')? o.FIB_COMB_LST_XML_11 :''
                                }
                            })
                    )
            };
            return MrcDataService.saveDataByUrl(data2bSave, '/FiberCombination/Save', token).then(function (res) {
                res['data'] = angular.fromJson(res.jsonStr);
                if (res.data.PMSG.substr(0, 9) == 'MULTI-001') {
                    $scope.showSplash = false;
                    setCntConfDatas(parseInt(res.data.V_MC_FIB_COMB_TMPLT_ID));
                    $scope.MC_FIB_COMB_TMPLT_ID = parseInt(res.data.V_MC_FIB_COMB_TMPLT_ID);
                }
                config.appToastMsg(res.data.PMSG);
            }, function (err) {
                console.error(err);
            })

        }

    }

})();