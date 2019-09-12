(function () {
    'use strict';
    angular.module('multitex.mrc').factory('MrcDataService', ['$http', 'exception', '$filter', 'x2js', 'access_token', MrcDataService]);

    function MrcDataService($http, exception, $filter, x2js, access_token) {
        var self = this;
        var urlBase = '/api/mrc';
        //Common Functionality


        var stringOperators = {
            '~eq~' : ' LIKE ',
            '~neq~': ' NOT LIKE ',
            '~doesnotcontain~': ' NOT LIKE ',
            '~contains~': ' LIKE ',
            '~startswith~': ' LIKE ',
            '~endswith~': ' LIKE ',
            '~and~': ' AND ',
            '~or~' : ' OR '
        };

        self.kFilterStr2Sql = function (str) {
            var kFilterStr2SqlStr = null;
            angular.forEach(stringOperators, function (val, key) {
                kFilterStr2SqlStr = (kFilterStr2SqlStr || str).replace(new RegExp(key, 'g'), val);
            });
            return kFilterStr2SqlStr;
        };

        self.kFilterStr2QueryParam = function (str) {
            var v_str = '';
            if (str.length == 0) {
                return '';
            }
            var obj = str.split('~and~');
            if (obj.length > 0) {
                obj.forEach(function (val, key) {

                    if (val.search('datetime') < 0) {
                        val = val.replace(/'/g, '').replace(/datetime/g, '');
                        v_str += '&p' + val.split('~')[0] + '=' + val.split('~')[2];
                        if (key != obj.length - 1) {
                            v_str += '&'
                        }
                    } else {
                        val = val.replace(/'/g, '').replace(/datetime/g, '');
                        v_str += '&p' + val.split('~')[0] + '=' + moment(val.split('~')[2], "YYYY-MM-DD").format("DD-MMM-YYYY");
                        if (key != obj.length - 1) {
                            v_str += '&'
                        }
                    }
                });
            }
            return v_str;
        };


        self.submitLabdipData = function (data, token, url) {
            return $http({
                method: 'post',
                url: url,
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.updateLabdipData = function (data, token, url) {
            return $http({
                method: 'put',
                url: url,
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.post4pdfRes = function (url,data) {
           return $http.post(url, data, { responseType: 'arraybuffer' })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

        }

        self.SaveBatchData = function (data, token, url) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                //url: '/api/mrc/PrintStrikeOff/SaveBatchData',
                url: url,
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.UpdateBatchData = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/PrintStrikeOff/UpdateBatchData',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };


        self.BatchSaveLabdip = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/Labdip/BatchSaveLabdip',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };
        self.BatchUpdateLabdip = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/Labdip/BatchUpdateData',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.BuyerByUserList = function () {
            return $http({
                method: 'get',
                url: urlBase + '/buyer/BuyerByUserList'
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getUserDatalist = function () {
            return $http({
                method: 'get',
                url: '/api/common/SelectAllUserData'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };





        //Save

        self.saveData = function (data, ctrl, token) {
            return $http({
                method: 'post',
                url: urlBase + '/' + ctrl + '/Save',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        //Save Data with Url
        self.saveDataByUrl = function (data, url) {
            return $http({
                method: 'post',
                url: urlBase + url,
                headers: { 'Authorization': 'Bearer ' + access_token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }
        //Save Data with Full Url
        self.saveDataByFullUrl = function (data, url) {
            return $http({
                method: 'post',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        //Save Data With File
        self.saveDataWithFile = function (data, ctrl, token) {
            return $http({
                method: 'post',
                url: urlBase + '/' + ctrl + '/Save',
                headers: { 'Authorization': 'Bearer ' + token, 'enctype': 'multipart/form-data' },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }
        //Update Data
        self.updateData = function (data, ctrl, token) {
            return $http({
                method: 'put',
                url: urlBase + '/' + ctrl + '/Update',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        //Update With Data with File
        self.updateDataWithFile = function (data, ctrl, token) {
            return $http({
                method: 'put',
                url: urlBase + '/' + ctrl + '/Update',
                headers: { 'Authorization': 'Bearer ' + token, 'enctype': 'multipart/form-data' },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        // Delete Data By Url
        self.delDataByUrl = function (url, data, token) {
            return $http({
                method: 'delete',
                url: urlBase + '/' + url,
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        // Delete Data By Full Url
        self.delDataByFullUrl = function (url, data, token) {
            return $http({
                method: 'delete',
                url:  url,
                headers: { 'Authorization': 'Bearer ' + token }
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        

        //Select 
        self.selectData = function (ctrl, ID) {
            return $http({
                method: 'get',
                url: urlBase + '/' + ctrl + '/Select/' + ID
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        //Select All
        self.selectAllData = function (ctrl) {
            return $http({
                method: 'get',
                url: urlBase + '/' + ctrl + '/SelectAll'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.xmlStringShort = function (data) {
            var xml = '';
            if (angular.isArray(data)) {
                xml += '<trans>';
                angular.forEach(data, function (val, key) {
                    xml += ' <row ';
                    angular.forEach(val, function (Value, Field) {
                        if (angular.isDate(Value)) {
                            Value = $filter('date')(Value, 'dd-MMM-yyyy');
                        }

                        if (Field !== '$$hashKey') {
                            xml += " " + Field + "=\"" + (_.isNil(Value) ? '' : Value )+ "\"";
                        }
                    });
                    xml += ' />';
                });
                xml += '</trans>';
            }
            return xml;
        }

        self.xmlStringShortNoTag = function (data) {
            var xml = '';
            if (data.length > 0 && angular.isArray(data)) {
                xml += '#trans$';
                angular.forEach(data, function (val, key) {
                    xml += '#row ';
                    angular.forEach(val, function (Value, Field) {
                        if (Field !== '$$hashKey') {
                            xml += " " + Field + "=*" + (_.isNil(Value) ? '' : Value) + "*";
                        }
                    });
                    xml += '/$';
                });
                xml += '#/trans$';
            }
            return xml;
        }

        self.xmlStringShortNoTagChild = function (data) {
            var xml = '';
            if (data.length > 0 && angular.isArray(data)) {
                xml += '%trans@';
                angular.forEach(data, function (val, key) {
                    xml += '%row';
                    angular.forEach(val, function (Value, Field) {
                        if (angular.isDate(Value)) {
                            Value = $filter('date')(Value, 'dd-MMM-yyyy');
                        }

                        if (Field !== '$$hashKey') {
                            xml += " " + Field + "=~" + (_.isNil(Value) ? '' : Value) + "~";
                        }
                    });
                    xml += '/@';
                });
                xml += '%/trans@';
            }
            return xml;
        }


        self.xmlStringLong = function (data) {
            var xml = '';
            if (angular.isArray(data) && data.length > 0) {
                xml += '<trans>';
                angular.forEach(data, function (val, key) {
                    xml += '<row> ';
                    angular.forEach(val, function (Value, Field) {
                        if (angular.isDate(Value)) {
                            Value = $filter('date')(Value, 'dd-MMM-yyyy');
                        }
                        if (Field != '$$hashKey') {
                            xml += "<" + Field + ">" + (_.isNil(Value) ? '' : Value) + "</" + Field + ">"
                        }
                    });
                    xml += '</row>';
                });
                xml += '</trans>';
            }
            return xml;
        }

        self.xmlStringLongChild = function (data) {
            var xml = '';
            if (angular.isArray(data) && data.length > 0) {
                xml += '%trans@';
                angular.forEach(data, function (val, key) {
                    xml += '%row@ ';
                    angular.forEach(val, function (Value, Field) {
                        if (angular.isDate(Value)) {
                            Value = $filter('date')(Value, 'dd-MMM-yyyy');
                        }
                        if (Field != '$$hashKey') {
                            xml += "%" + Field + "@" + (_.isNil(Value) ? '' : Value) + "%/" + Field + "@"
                        }
                    });
                    xml += '%/row@';
                });
                xml += '%/trans@';
            }
            return xml;
        }


        self.parseXmlString = function (string) {
            var data = [];
            if (!_.isNil(string)) {
                var jsonObj = x2js.xml_str2json(string);
                if (jsonObj) {
                    if (Object.keys(jsonObj).length) {
                        if (_.isPlainObject(jsonObj.trans.row)) {
                            data.push(jsonObj.trans.row);
                        } else {
                            data = jsonObj.trans.row;
                        }
                    }
                }
            }
            return data;
        }


        self.getDataByUrl = function (url) {
            return $http({
                method: 'get',
                url: urlBase + url,
                headers: { 'Authorization': 'Bearer ' + access_token },
            }).then(function (result) {
                return result.data;
            }).catch(function (Message) {
                console.error(Message);
                exception.catcher('XHR loading Failded')(Message);
            });
        }

        self.getDataByFullUrl = function (url) {
            return $http({
                method: 'get',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token }
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }
        //Fetch External Url that does not support auth bearer.
        self.getDataByUrlNoToken = function (url) {
            return $http({
                method: 'get',
                url: url,
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }
        //Misc Select
        self.GetAllOthers = function (url) {
            return $http({
                method: 'get',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token }
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getStyleDtlParentItemList = function (pMC_STYLE_H_ID, pIS_GET_CHILD) {
            return $http({
                method: 'get',
                url: urlBase + '/StyleDItem/StyleDtlParentItemList/' + pMC_STYLE_H_ID + '/' + pIS_GET_CHILD
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getColourBuyerListData = function () {
            return $http({
                method: 'get',
                url: urlBase + '/ColourMaster/ColourBuyerListData/'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.orderDataList = function (pMC_BUYER_ID, pMC_ORDER_H_ID, pMC_STYLE_H_ID, pMC_BUYER_OFF_ID, pMC_BYR_ACC_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/Order/OrderDataList/' + pMC_BUYER_ID + '/' + pMC_ORDER_H_ID + '/' + pMC_STYLE_H_ID + '/' + pMC_BUYER_OFF_ID + '/' + pMC_BYR_ACC_ID
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }




        self.getItemByStyleID = function (MC_STYLE_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/StyleItem/ItemByStyleID/' + MC_STYLE_ID
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getOfficeDatasByBuyerId = function (MC_BUYER_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/buyer/' + MC_BUYER_ID + '/Offices'
            })
            .then(function (result) {
                var data = [];

                if (result.data.length > 0) {
                    angular.forEach(result.data, function (val, key) {
                        val['name'] = val.BOFF_NAME_EN;
                        val['id'] = val.MC_BUYER_OFF_ID;
                        val['EXIST'] = 'Y';
                        data.push(val);
                    })
                }
                return data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }



        //SelectAll

        self.getAllOfficeDatas = function () {
            return $http({
                method: 'get',
                url: urlBase + '/BuyerOffice/SelectAll'
            })
                .then(function (result) {
                    var data = [];
                    angular.forEach(result.data, function (val, key) {
                        val['name'] = val.BOFF_NAME_EN;
                        val['id'] = val.MC_BUYER_OFF_ID;
                        data.push(val);
                    })
                    return data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getCompanyData = function () {
            return $http.get('/hr/hrleavetrans/CompanyData')
                   .then(function (result) {
                       return result.data;
                   }).catch(function (message) {
                       exception.catcher('XHR loading Failded')(message);
                   });
        }



        self.getAllBuyerDatas = function () {
            return $http({
                method: 'get',
                url: urlBase + '/buyer/SelectAll'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getAllSizeDatas = function () {
            return $http({
                method: 'get',
                url: urlBase + '/SizeMaster/SelectAll'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getAllStyleDatas = function (pMC_BUYER_OFF_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/StyleH/BuyerWiseStyleList/' + pMC_BUYER_OFF_ID
            }).then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        //Select






        self.selectBuyerData = function (MC_BUYER_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/buyer/Select/' + MC_BUYER_ID
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }


        self.CostHeadList = function (MC_STYLE_ITEM_ID, pOption) {
            return $http({
                method: 'get',
                url: urlBase + '/StyleItem/CostHeadList/' + MC_STYLE_ITEM_ID + '/' + pOption
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.tnaTaskByGroup = function (MC_TNA_TASK_GRP_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/TnaTask/List/' + MC_TNA_TASK_GRP_ID
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.ParentTnaTaskByGroup = function (MC_TNA_TASK_GRP_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/TnaTemplateDtl/TaskListByGroup/' + MC_TNA_TASK_GRP_ID
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.getItemList = function (pINV_ITEM_CAT_ID) {
            alert("A");
            return $http({
                method: 'get',
                url: '/api/inv/Item/ItemList',
                params: { pINV_ITEM_CAT_ID: pINV_ITEM_CAT_ID }
            })
            .then(function (result) {
                //var data = [];
                //data = result.data;
                //console.log(data);
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };

        self.selectOfficeData = function (MC_BUYER_OFF_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/BuyerOffice/Select/' + MC_BUYER_OFF_ID
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.selectSizeData = function (MC_SIZE_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/SizeMaster/Select/' + MC_SIZE_ID
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }





        self.LookupListData = function (ID) {
            return $http({
                method: 'get',
                url: '/api/common/LookupListData/' + ID
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.invItemListByParent = function (ID) {
            return $http({
                method: 'get',
                url: urlBase + '/StyleItem/InvItemByParent/' + ID
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getUserData = function () {
            return $http({
                method: 'get',
                url: '/api/common/UserData'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }




        self.getMOUList = function () {
            return $http({
                method: 'get',
                url: '/api/common/MOUList/Y'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }


        self.getCurrencyList = function () {
            return $http({
                method: 'get',
                url: '/api/common/CurrencyList'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }



        self.GetCountryList = function (data, token) {
            return $http({
                method: 'get',
                url: urlBase + '/buyer/GetCountryList'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.GetTimeZoneList = function () {
            return $http({
                method: 'get',
                url: urlBase + '/BuyerOffice/GetTimeZoneList'
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        //Misc Save
        self.batchSaveOrder = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/Order/BatchSaveOrder',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.batchSaveBulkFabBooking = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/BulkFabBk/BatchSaveBulkFabBooking',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.batchSaveBulkFabPL = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/BulkFabBk/batchSaveBulkFabPL',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.batchSaveBulkFabRptDisplayOrder = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/BulkFabBk/BatchSaveBulkFabRptDisplayOrder',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.batchSaveBulkFabCollarCuff = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/BulkFabBk/BatchSaveBulkFabCollarCuff',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };

        self.processBulkFabBooking = function (data, token) {
            return $http({
                headers: { 'Authorization': 'Bearer ' + token },
                url: urlBase + '/BulkFabBk/ProcessBulkFabBooking',
                method: 'post',
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        };


        self.officeMapData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/buyer/SaveMapData',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }




        self.saveCostHeadMappingData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/StyleItem/saveCostHeadMappingData',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.saveBuyerData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/buyer/Save',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }


        self.submitBuyerAcc = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/BuyerAcc/submitBuyerAcc',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.saveOfficeData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/BuyerOffice/Save',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.saveSizeData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/SizeMaster/Save',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }



        self.saveStyleItemFabData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/StyleItem/saveStyleItemFabData',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }




        // Misc Update
        self.updateBuyerData = function (data, token) {
            return $http({
                method: 'put',
                url: urlBase + '/buyer/Update',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.BuyerColourMapData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/ColourMaster/BuyerColourMapData',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.updateOfficeData = function (data, token) {
            return $http({
                method: 'put',
                url: urlBase + '/BuyerOffice/Update',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }

        self.updateSizeData = function (data, token) {
            return $http({
                method: 'put',
                url: urlBase + '/SizeMaster/Update',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
                .then(function (result) {
                    return result.data;
                }).catch(function (message) {
                    exception.catcher('XHR loading Failded')(message);
                });
        }



        // New Method

        self.getBuyerDatasByColourId = function (MC_COLOR_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/buyer/BuyerDatasByColourId/' + MC_COLOR_ID
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.submitTnaTempGrid = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/TnaTemplateDtl/Save',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        };


        self.saveUserBuyerAccMapData = function (data, token) {
            return $http({
                method: 'post',
                url: urlBase + '/UserBuyerAcc/Save',
                headers: { 'Authorization': 'Bearer ' + token },
                data: data
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.getTaskList = function (MC_TNA_TMPLT_H_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/TnaTaskGroup/getTaskList/' + MC_TNA_TMPLT_H_ID
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.parentTasksList = function (MC_TNA_TMPLT_H_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/TnaTaskGroup/parentTasksList/' + MC_TNA_TMPLT_H_ID
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });

        }


        self.treelistData = function (MC_TNA_TMPLT_H_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/TnaTemplateDtl/treelistData/' + MC_TNA_TMPLT_H_ID
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }



        self.getBuyerAccDataByByerId = function (MC_BUYER_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/BuyerAcc/getBuyerAccDataByByerId/' + MC_BUYER_ID
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }


        self.getBuyerAccListByUserId = function (SC_USER_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/BuyerAcc/getBuyerAccListByUserId/' + SC_USER_ID
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        self.getBuyerWiseAccByUserList = function (pMC_BUYER_ID) {
            return $http({
                method: 'get',
                url: urlBase + '/BuyerAcc/getBuyerWiseAccByUserList/' + pMC_BUYER_ID
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        return self;
    }


})();

(function () {
    'use strict';
    angular.module('multitex.mrc').factory('MrcRptDataService', ['$http', 'exception', '$filter', 'access_token', MrcDataService]);

    function MrcDataService($http, exception, $filter, access_token) {
        var self = this;
        var urlBase = '/api/mrc';

        self.getDataByFullUrl = function (url) {
            return $http({
                method: 'get',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token }
            }).then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }

        return self;
    }


})();