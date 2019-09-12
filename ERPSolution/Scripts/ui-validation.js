

function fnValidate() {

    var error = "";
    var notValid = 0;

    //required field validator
    $("input.required,textarea.required").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, this.value.length > 0);
    });

    $("input.password").each(function (i) {
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer field validator
    $("input.integer").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperator").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperatorNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $("input.integerNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $("input.double").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue <= 0)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue <= 0)
            this.value = 0;
    });

    //decimal field validator
    $("input.doubleNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue < 1)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue < 1)
            this.value = 0;
    });

    //Date field validator dd-MM-yyyy
    $("input.date").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, false));
    });

    //Date field validator dd-MM-yyyy
    $("input.dateNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value == 'DD-MM-YYYY') {
            this.value = '';
        }
        notValid += MarkError(this, IsDate(this.value, true));
    });

    //Date field validator dd-MM-yyyy and Not future date
    $("input.dateNRF").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDateF(this.value, true));
    });

    //Time field validator hh:mm tt
    $("input.time").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsTime(this.value));
    });

    //Time field validator hh:mm tt (allow blank)
    $("input.timeNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) return true;
        notValid += MarkError(this, IsTime(this.value));
    });

    //// drop down List Validation
    //$("select.required").each(function (i) {
    //    notValid += MarkErrorForDDL(this, this.selectedIndex > 0);
    //});

    // drop down List Validation
    $("select.required").each(function (i) {
        var val = (this.value || 0);
        var d = this.id + '_listbox';
        var control = document.querySelector('span[aria-owns="' + d + '"]');

        $(control).find('.k-state-default .k-input').each(function () {
            var current = $(this);
            notValid += MarkErrorForDDL(current, val > 0);
        });
                
    });


    // multi select drop down List Validation
    $("select.multiSelect").each(function (i) {
        var val = this.value;
        var d = this.id + '-list';
        var control = document.querySelector('div[id="' + d + '"]');

        var parentTag = $(this).parent().get(0).tagName;

        if (val.length > 0) {
            $(parentTag).parent(".multiSelect").css("border-color", "#898383");

            return 0;
        }
        else {
            $(parentTag).parent(".multiSelect").css("border-color", "red");
            return 1;
        }
    });


    // drop down List Validation
    $("select.requiredDDL").each(function (i) {
        notValid += MarkErrorForDDL(this, this.selectedIndex > 0);
    });

    
    //Set focus on the first required field.
    $("*").each(function () {
        if (this.tagName == "INPUT") {
            if (this.type == "text") {
                if ($(this).css('border-color') == 'red') {
                    // To solved the invisible field focussing error.
                    try {
                        this.focus();
                    }
                    catch (e) {
                    }
                    return false;
                }
            }
        }

        if (this.tagName == "SELECT") {
            if ($(this).css('color') == 'red') {
                this.focus();
                return false;
            }
        }
    });

    if (notValid > 0) {

        $("#lblMsg").html("<b>Please provide the following information:</b><br>" + error).css("color", "red");
        //$(document).scrollTop(0);
        return false;
    }
    else {
        $("#lblMsg").html("").css("color", "Green");
        return true;
    }

}



function fnValidateSub() {

    var error = "";
    var notValid = 0;

    //required field validator
    $("input.requiredSub,textarea.requiredSub").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, this.value.length > 0);
    });

    $("input.password").each(function (i) {
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer field validator
    $("input.integerSub").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, 0);
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperator").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperatorNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $("input.integerNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $("input.doubleSub").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, 0);
        var NewValue = parseFloat(this.value);
        if (NewValue <= 0)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue <= 0)
            this.value = 0;
    });

    //decimal field validator
    $("input.doubleNRSub").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/0,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue < 1)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue < 1)
            this.value = 0;
    });

    //Date field validator dd-MM-yyyy
    $("input.date").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, false));
    });

    //Date field validator dd-MM-yyyy
    $("input.dateNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value == 'DD-MM-YYYY') {
            this.value = '';
        }
        notValid += MarkError(this, IsDate(this.value, true));
    });

    //Date field validator dd-MM-yyyy and Not future date
    $("input.dateNRF").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDateF(this.value, true));
    });

    //Time field validator hh:mm tt
    $("input.time").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsTime(this.value));
    });

    //Time field validator hh:mm tt (allow blank)
    $("input.timeNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) return true;
        notValid += MarkError(this, IsTime(this.value));
    });

    // drop down List Validation
    $("select.requiredSub").each(function (i) {
        var val = (this.value || 0);
        var d = this.id + '_listbox';
        var control = document.querySelector('span[aria-owns="' + d + '"]');
        console.log(control);

        $(control).find('.k-state-default .k-input').each(function () {
            var current = $(this);
            notValid += MarkErrorForDDL(current, val > 0);
        });
        
    });


    // multi select drop down List Validation
    $("select.multiSelectSub").each(function (i) {
        var val = this.value;
        var d = this.id + '-list';
        var control = document.querySelector('div[id="' + d + '"]');

        var parentTag = $(this).parent().get(0).tagName;

        if (val.length > 0) {
            $(parentTag).parent(".multiSelectSub").css("border-color", "#898383");

            return 0;
        }
        else {
            $(parentTag).parent(".multiSelectSub").css("border-color", "red");
            return 1;
        }        
    });

    // drop down List Validation
    $("select.requiredSubDDL").each(function (i) {
        notValid += MarkErrorForDDL(this, this.selectedIndex > 0);
    });
    //// drop down List Validation
    //$("span.requiredSub").each(function (i) {
    //    notValid += MarkErrorForDDL(this, this.selectedIndex > 0);
    //});

    


    //Set focus on the first required field.
    $("*").each(function () {
        if (this.tagName == "INPUT") {
            if (this.type == "text") {
                if ($(this).css('border-color') == 'red') {
                    // To solved the invisible field focussing error.
                    try {
                        this.focus();
                    }
                    catch (e) {
                    }
                    return false;
                }
            }
        }

        if (this.tagName == "SELECT") {
            if ($(this).css('color') == 'red') {
                this.focus();
                return false;
            }
        }
    });

    if (notValid > 0) {
        //appToastMsg("Please provide the following information:");
        $("#lblMsgSub").html("<b>Please provide the following information:</b><br>" + error).css("color", "red");
        //$(document).scrollTop(0);
        return false;
    }
    else {
        $("#lblMsgSub").html("").css("color", "Green");
        return true;
    }

}


function fnValidateSub2() {

    var error = "";
    var notValid = 0;

    //required field validator
    $("input.requiredSub2,textarea.requiredSub3").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, this.value.length > 0);
    });

    $("input.passwordSub2").each(function (i) {
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer field validator
    $("input.integerSub2").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperator").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperatorNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $("input.integerNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $("input.doubleSub2").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue <= 0)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue <= 0)
            this.value = 0;
    });

    //decimal field validator
    $("input.doubleNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue < 1)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue < 1)
            this.value = 0;
    });

    //Date field validator dd-MM-yyyy
    $("input.date").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, false));
    });

    //Date field validator dd-MM-yyyy
    $("input.dateNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value == 'DD-MM-YYYY') {
            this.value = '';
        }
        notValid += MarkError(this, IsDate(this.value, true));
    });

    //Date field validator dd-MM-yyyy and Not future date
    $("input.dateNRF").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDateF(this.value, true));
    });

    //Time field validator hh:mm tt
    $("input.time").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsTime(this.value));
    });

    //Time field validator hh:mm tt (allow blank)
    $("input.timeNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) return true;
        notValid += MarkError(this, IsTime(this.value));
    });

    // drop down List Validation
    $("select.requiredSub2").each(function (i) {
        var val = (this.value || 0);
        var d = this.id + '_listbox';
        var control = document.querySelector('span[aria-owns="' + d + '"]');

        $(control).find('.k-state-default .k-input').each(function () {
            var current = $(this);
            notValid += MarkErrorForDDL(current, val > 0);
        });
    });


    // multi select drop down List Validation
    $("select.multiSelectSub2").each(function (i) {
        var val = this.value;
        var d = this.id + '-list';
        var control = document.querySelector('div[id="' + d + '"]');

        var parentTag = $(this).parent().get(0).tagName;

        if (val.length > 0) {
            $(parentTag).parent(".multiSelectSub2").css("border-color", "#898383");

            return 0;
        }
        else {
            $(parentTag).parent(".multiSelectSub2").css("border-color", "red");
            return 1;
        }
    });


    //Set focus on the first required field.
    $("*").each(function () {
        if (this.tagName == "INPUT") {
            if (this.type == "text") {
                if ($(this).css('border-color') == 'red') {
                    // To solved the invisible field focussing error.
                    try {
                        this.focus();
                    }
                    catch (e) {
                    }
                    return false;
                }
            }
        }

        if (this.tagName == "SELECT") {
            if ($(this).css('color') == 'red') {
                this.focus();
                return false;
            }
        }
    });

    if (notValid > 0) {
        //appToastMsg("Please provide the following information:");
        $("#lblMsg").html("<b>Please provide the following information:</b><br>" + error).css("color", "red");
        //$(document).scrollTop(0);
        return false;
    }
    else {
        $("#lblMsg").html("").css("color", "Green");
        return true;
    }

}


function fnValidateSub3() {

    var error = "";
    var notValid = 0;

    //required field validator
    $("input.requiredSub3,textarea.requiredSub3").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, this.value.length > 0);
    });

    $("input.passwordSub3").each(function (i) {
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer field validator
    $("input.integerSub3").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperator").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperatorNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $("input.integerNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $("input.doubleSub3").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue < 1)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue < 1)
            this.value = 0;
    });

    //decimal field validator
    $("input.doubleNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue < 1)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue < 1)
            this.value = 0;
    });

    //Date field validator dd-MM-yyyy
    $("input.date").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, false));
    });

    //Date field validator dd-MM-yyyy
    $("input.dateNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value == 'DD-MM-YYYY') {
            this.value = '';
        }
        notValid += MarkError(this, IsDate(this.value, true));
    });

    //Date field validator dd-MM-yyyy and Not future date
    $("input.dateNRF").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDateF(this.value, true));
    });

    //Time field validator hh:mm tt
    $("input.time").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsTime(this.value));
    });

    //Time field validator hh:mm tt (allow blank)
    $("input.timeNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) return true;
        notValid += MarkError(this, IsTime(this.value));
    });

    // drop down List Validation
    $("select.requiredSub3").each(function (i) {
        var val = (this.value || 0);
        var d = this.id + '_listbox';
        var control = document.querySelector('span[aria-owns="' + d + '"]');

        $(control).find('.k-state-default .k-input').each(function () {
            var current = $(this);
            notValid += MarkErrorForDDL(current, val > 0);
        });
    });


    // multi select drop down List Validation
    $("select.multiSelectSub3").each(function (i) {
        var val = this.value;
        var d = this.id + '-list';
        var control = document.querySelector('div[id="' + d + '"]');

        var parentTag = $(this).parent().get(0).tagName;

        if (val.length > 0) {
            $(parentTag).parent(".multiSelectSub3").css("border-color", "#898383");

            return 0;
        }
        else {
            $(parentTag).parent(".multiSelectSub3").css("border-color", "red");
            return 1;
        }
    });


    //Set focus on the first required field.
    $("*").each(function () {
        if (this.tagName == "INPUT") {
            if (this.type == "text") {
                if ($(this).css('border-color') == 'red') {
                    // To solved the invisible field focussing error.
                    try {
                        this.focus();
                    }
                    catch (e) {
                    }
                    return false;
                }
            }
        }

        if (this.tagName == "SELECT") {
            if ($(this).css('color') == 'red') {
                this.focus();
                return false;
            }
        }
    });

    if (notValid > 0) {
        //appToastMsg("Please provide the following information:");
        $("#lblMsgSub3").html("<b>Please provide the following information:</b><br>" + error).css("color", "red");
        //$(document).scrollTop(0);
        return false;
    }
    else {
        $("#lblMsgSub3").html("").css("color", "Green");
        return true;
    }

}


function fnValidateSub4() {

    var error = "";
    var notValid = 0;

    //required field validator
    $("input.requiredSub4,textarea.requiredSub4").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, this.value.length > 0);
    });

    $("input.passwordSub4").each(function (i) {
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer field validator
    $("input.integerSub4").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperator").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperatorNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $("input.integerNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $("input.doubleSub4").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue < 1)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue < 1)
            this.value = 0;
    });

    //decimal field validator
    $("input.doubleNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue < 1)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue < 1)
            this.value = 0;
    });

    //Date field validator dd-MM-yyyy
    $("input.date").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, false));
    });

    //Date field validator dd-MM-yyyy
    $("input.dateNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value == 'DD-MM-YYYY') {
            this.value = '';
        }
        notValid += MarkError(this, IsDate(this.value, true));
    });

    //Date field validator dd-MM-yyyy and Not future date
    $("input.dateNRF").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDateF(this.value, true));
    });

    //Time field validator hh:mm tt
    $("input.time").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsTime(this.value));
    });

    //Time field validator hh:mm tt (allow blank)
    $("input.timeNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) return true;
        notValid += MarkError(this, IsTime(this.value));
    });

    // drop down List Validation
    $("select.requiredSub4").each(function (i) {
        var val = (this.value || 0);
        var d = this.id + '_listbox';
        var control = document.querySelector('span[aria-owns="' + d + '"]');

        $(control).find('.k-state-default .k-input').each(function () {
            var current = $(this);
            notValid += MarkErrorForDDL(current, val > 0);
        });
    });


    // multi select drop down List Validation
    $("select.multiSelectSub4").each(function (i) {
        var val = this.value;
        var d = this.id + '-list';
        var control = document.querySelector('div[id="' + d + '"]');

        var parentTag = $(this).parent().get(0).tagName;

        if (val.length > 0) {
            $(parentTag).parent(".multiSelectSub4").css("border-color", "#898383");

            return 0;
        }
        else {
            $(parentTag).parent(".multiSelectSub4").css("border-color", "red");
            return 1;
        }
    });


    //Set focus on the first required field.
    $("*").each(function () {
        if (this.tagName == "INPUT") {
            if (this.type == "text") {
                if ($(this).css('border-color') == 'red') {
                    // To solved the invisible field focussing error.
                    try {
                        this.focus();
                    }
                    catch (e) {
                    }
                    return false;
                }
            }
        }

        if (this.tagName == "SELECT") {
            if ($(this).css('color') == 'red') {
                this.focus();
                return false;
            }
        }
    });

    if (notValid > 0) {
        //appToastMsg("Please provide the following information:");
        $("#lblMsgSub4").html("<b>Please provide the following information:</b><br>" + error).css("color", "red");
        //$(document).scrollTop(0);
        return false;
    }
    else {
        $("#lblMsgSub4").html("").css("color", "Green");
        return true;
    }

}


function fnValidateRpt() {

    var error = "";
    var notValid = 0;

    //required field validator
    $("input.requiredRpt,textarea.requiredRpt").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, this.value.length > 0);
    });

    $("input.passwordRpt").each(function (i) {
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer field validator
    $("input.integerRpt").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperatorRpt").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperatorNRRpt").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $("input.integerNRRpt").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $("input.doubleRpt").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue < 1)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue < 1)
            this.value = 0;
    });

    //decimal field validator
    $("input.doubleNRRpt").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        var NewValue = parseFloat(this.value);
        if (NewValue < 1)
            this.value = null;
        notValid += MarkError(this, IsDouble(this.value));
        if (NewValue < 1)
            this.value = 0;
    });

    //Date field validator dd-MM-yyyy
    $("input.dateRpt").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, false));
    });

    //Date field validator dd-MM-yyyy
    $("input.dateNRRpt").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value == 'DD-MM-YYYY') {
            this.value = '';
        }
        notValid += MarkError(this, IsDate(this.value, true));
    });

    //Date field validator dd-MM-yyyy and Not future date
    $("input.dateNRFRpt").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDateF(this.value, true));
    });

    //Time field validator hh:mm tt
    $("input.timeRpt").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsTime(this.value));
    });

    //Time field validator hh:mm tt (allow blank)
    $("input.timeNRRpt").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) return true;
        notValid += MarkError(this, IsTime(this.value));
    });

    // drop down List Validation
    $("select.requiredRpt").each(function (i) {
        var val = (this.value || 0);
        var d = this.id + '_listbox';
        var control = document.querySelector('span[aria-owns="' + d + '"]');

        $(control).find('.k-state-default .k-input').each(function () {
            var current = $(this);
            notValid += MarkErrorForDDL(current, val > 0);
        });
    });


    // multi select drop down List Validation
    $("select.multiSelectRpt").each(function (i) {
        var val = this.value;
        var d = this.id + '-list';
        var control = document.querySelector('div[id="' + d + '"]');

        var parentTag = $(this).parent().get(0).tagName;

        if (val.length > 0) {
            $(parentTag).parent(".multiSelectRpt").css("border-color", "#898383");

            return 0;
        }
        else {
            $(parentTag).parent(".multiSelectRpt").css("border-color", "red");
            return 1;
        }
    });


    //Set focus on the first required field.
    $("*").each(function () {
        if (this.tagName == "INPUT") {
            if (this.type == "text") {
                if ($(this).css('border-color') == 'red') {
                    // To solved the invisible field focussing error.
                    try {
                        this.focus();
                    }
                    catch (e) {
                    }
                    return false;
                }
            }
        }

        if (this.tagName == "SELECT") {
            if ($(this).css('color') == 'red') {
                this.focus();
                return false;
            }
        }
    });

    if (notValid > 0) {

        $("#ContentPlaceHolder1_lblMsg").html("<b>Please provide the following information:</b><br>" + error).css("color", "red");
        //$(document).scrollTop(0);
        return false;
    }
    else {
        $("#ContentPlaceHolder1_lblMsg").html("").css("color", "Green");
        return true;
    }

}





function fnValidatePopup() {

    var error = "";
    var notValid = 0;

    //required field validator
    $("input.prequired,textarea.prequired").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer field validator
    $("input.pinteger").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.pintegerWithCommaSeperator").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.pintegerWithCommaSeperatorNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $("input.pintegerNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $("input.pdouble").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsDouble(this.value));
    });

    //decimal field validator
    $("input.pdoubleNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsDouble(this.value));
    });

    //Date field validator dd-MM-yyyy
    $("input.pdate").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, false));
    });

    //Date field validator dd-MM-yyyy
    $("input.pdateNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value == 'DD-MM-YYYY') {
            this.value = '';
        }
        notValid += MarkError(this, IsDate(this.value, true));
    });

    //Date field validator dd-MM-yyyy and Not future date
    $("input.pdateNRF").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDateF(this.value, true));
    });

    //Time field validator hh:mm tt
    $("input.ptime").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsTime(this.value));
    });

    //Time field validator hh:mm tt (allow blank)
    $("input.ptimeNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) return true;
        notValid += MarkError(this, IsTime(this.value));
    });

    // drop down List Validation
    $("select.prequired").each(function (i) {
        var val = (this.value || 0);
        var d = this.id + '_listbox';
        var control = document.querySelector('span[aria-owns="' + d + '"]');

        $(control).find('.k-state-default .k-input').each(function () {
            var current = $(this);
            notValid += MarkErrorForDDL(current, val > 0);
        });
    });



    //Set focus on the first required field.
    $("*").each(function () {
        if (this.tagName == "INPUT") {
            if (this.type == "text") {
                if ($(this).css('border-color') == 'red') {
                    // To solved the invisible field focussing error.
                    try {
                        this.focus();
                    }
                    catch (e) {
                    }
                    return false;
                }
            }
        }

        if (this.tagName == "SELECT") {
            if ($(this).css('color') == 'red') {
                this.focus();
                return false;
            }
        }
    });

    if (notValid > 0) {
        $("span[id$=lblMsg]").html("<b>Please provide the following information:</b><br>" + error).css("color", "red");
        //$(document).scrollTop(0);
        return false;
    }
    else {
        $("span[id$=lblMsg]").html("").css("color", "green");
        return true;
    }

}


function fnValidateRow(bItem) {
    var error = "";

    var Row = bItem.parentNode.parentNode;

    var notValid = 0;


    //required field validator
    $(Row).find("input[class='required'],textarea[class='required']").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer required field validator
    $(Row).find("input.integer").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $(Row).find("input.integerNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $(Row).find("input.double").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDouble(this.value));
    });

    //decimal field validator
    $(Row).find("input.doubleNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsDouble(this.value));
    });

    //Date field validator dd-MM-yyyy
    $(Row).find("input.date").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, false));
    });

    //Date field validator dd-MM-yyyy
    $(Row).find("input.dateNR").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, true));
    });

    // drop down List Validation
    $(Row).find("select.required").each(function (i) {
        notValid += MarkErrorForDDL(this, this.selectedIndex > 0);
    });


    if (notValid > 0) {
        $("span[id$=lblMsg]").html("<b>Please provide the following information:</b><br>" + error).css("color", "red");
        return false;
    }
    else {
        $("span[id$=lblMsg]").html("").css("color", "black");
        return true;
    }
}

//check valid integer 1,15,18
function IsInteger(val) {
    var re = new RegExp("^[0-9]+$");
    return val.match(re);
}

//check valid date 25/12/1989
function IsDate(val, allowBalnk) {
    if (allowBalnk && val.length == 0) return true;
    var dateaprts = val.split('-');
    var dt = new Date(dateaprts[2], dateaprts[1] - 1, dateaprts[0]);
    return (dt.getDate() == dateaprts[0] && dt.getMonth() == dateaprts[1] - 1 && dt.getFullYear() == dateaprts[2])
}

//check valid date 25/12/1989 and Not future date
function IsDateF(val, allowBalnk) {
    var curDate = new Date();
    if (allowBalnk && val.length == 0) return true;
    var dateaprts = val.split('-');
    var dt = new Date(dateaprts[2], dateaprts[1] - 1, dateaprts[0]);
    return (dt.getDate() == dateaprts[0] && dt.getMonth() == dateaprts[1] - 1 && dt.getFullYear() == dateaprts[2] && dt <= curDate)
}

//check valid time 12:15 PM
function IsTime(val) {
    var err = 0
    if (val.length != 8) err = 1
    hh = val.substring(0, 2)// Hour f
    c = val.substring(2, 3)// ':'
    mm = val.substring(3, 5)// Min b
    e = val.substring(5, 6)// ' '
    tt = val.substring(6, 8)// AM/PM

    if (hh < 0 || hh > 12) err = 1
    if (mm < 0 || mm > 59) err = 1
    if (tt != 'AM' && tt != 'PM') err = 1
    if (err == 1) { return false; }
    else { return true };
}

//check valid decimal number  125,125.50 
function IsDouble(val) {
    val = val.replace(/,/g, '');
    return !isNaN(val) && (val.length > 0);
}



function MarkError(control, isValid) {
    //$(control.offsetParent).find("BR,p").remove();

    if (isValid) {
        $(control).css("border-color", "#898383");
        //$(control).css("border", "solid 1px #C5D3E4");
        return 0;
    }
    else {
        //    $(control.offsetParent).append("<br><p style='color:red'>Required</p>");
        //error +=$(this).attr('id').split('_txt')[1]+'<br>';
        $(control).css("border-color", "red");
        // alert(control.id);
        return 1;
    }

}


function MarkErrorForDDL(control, isValid) {
    if (isValid) {        
        $(control).css("color", "black");
        $(control).css("border-color", "#dbdbdb");
        return 0;
    }
    else {
        $(control).css("color", "red");
        $(control).css("border-color", "red");
        return 1;
    }

}


//function MarkErrorForDDL(control, isValid) {
//    if (isValid) {
//        $(control).css("color", "black");
//        return 0;
//    }
//    else {
//        $(control).css("color", "red");
//        return 1;
//    }

//}