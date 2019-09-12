(function () {
    'use strict';
    angular.module('multitex.knitting').factory('KnittingDataService', ['$http', 'exception', '$filter', 'x2js', 'access_token', KnittingDataService]);

    function KnittingDataService($http, exception, $filter, x2js, access_token) {
        var self = this;
        var urlBase = '/api/knit';
        //Common Functionality


        var stringOperators = {
            '~eq~': ' LIKE ',
            '~neq~': ' NOT LIKE ',
            '~doesnotcontain~': ' NOT LIKE ',
            '~contains~': ' LIKE ',
            '~startswith~': ' LIKE ',
            '~endswith~': ' LIKE ',
            '~and~': ' AND ',
            '~or~': ' OR '
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
                    val = val.replace(/'/g, '').replace(/datetime/g, '');
                    v_str += '&p' + val.split('~')[0] + '=' + val.split('~')[2];
                    if (key != obj.length - 1) {
                        v_str += '&'
                    }
                });
            }
            return v_str;
        };

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

        //Save Data with Url
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

        // Delete Data By Url
        self.delDataByUrl = function (url, data) {
            return $http({
                method: 'delete',
                url: urlBase + '/' + url,
                headers: { 'Authorization': 'Bearer ' + access_token },
                data: data
            })
            .then(function (result) {
                return result.data;
            }).catch(function (message) {
                exception.catcher('XHR loading Failded')(message);
            });
        }



        // Delete Data By Full Url
        self.delDataByFullUrl = function (url, data) {
            return $http({
                method: 'delete',
                url: url,
                headers: { 'Authorization': 'Bearer ' + access_token }
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
                            xml += " " + Field + "=\"" + (_.isNil(Value) ? '' : Value) + "\"";
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


        //self.parseXmlString = function (string) {
        //    var data = [];
        //    if (string) {
        //        var jsonObj = x2js.xml_str2json(string);
        //        if (jsonObj) {
        //            if (Object.keys(jsonObj).length) {
        //                if (_.isPlainObject(jsonObj.trans.row)) {
        //                    data.push(jsonObj.trans.row);
        //                } else {
        //                    data = jsonObj.trans.row;
        //                }
        //            }
        //        }
        //    }
        //    return data;
        //}

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

        self.post4pdfRes = function (url, data) {
            return $http.post(url, data, { responseType: 'arraybuffer' })
             .then(function (result) {
                 return result.data;
             }).catch(function (message) {
                 exception.catcher('XHR loading Failded')(message);
             });
        };
        return self;
    }


})();