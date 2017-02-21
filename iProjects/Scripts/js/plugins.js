var w;
var h;
var dw;
var dh;

function executeFunctionByName(functionName, context /*, args */) {
    var args = [].slice.call(arguments).splice(2);
    var namespaces = functionName.split(".");
    var func = namespaces.pop();
    for (var i = 0; i < namespaces.length; i++) {
        context = context[namespaces[i]];
    }
    return context[func].apply(this, args);
}

var changeptype = function () {
    w = $(window).width();
    h = $(window).height();
    dw = $(document).width();
    dh = $(document).height();

    if (jQuery.browser.mobile === true) {
        $("body").addClass("mobile").removeClass("fixed-left");
    }

    if (!$("#wrapper").hasClass("forced")) {
        if (w > 990) {
            $("body").removeClass("smallscreen").addClass("widescreen");
            $("#wrapper").removeClass("enlarged");
        } else {
            $("body").removeClass("widescreen").addClass("smallscreen");
            $("#wrapper").addClass("enlarged");
            $(".left ul").removeAttr("style");
        }
        if ($("#wrapper").hasClass("enlarged") && $("body").hasClass("fixed-left")) {
            $("body").removeClass("fixed-left").addClass("fixed-left-void");
        } else if (!$("#wrapper").hasClass("enlarged") && $("body").hasClass("fixed-left-void")) {
            $("body").removeClass("fixed-left-void").addClass("fixed-left");
        }

    }
    toggle_slimscroll(".slimscrollleft");
};
$(document).ready(function () {
    FastClick.attach(document.body);
    resizefunc.push("initscrolls");
    resizefunc.push("changeptype");
    //$('.sparkline').sparkline('html', { enableTagOptions: true });

    $('.animate-number').each(function () {
        $(this).animateNumbers($(this).attr("data-value"), true, parseInt($(this).attr("data-duration")));
    });
    //TOOLTIP
    $('body').tooltip({
        selector: "[data-toggle=tooltip]",
        container: "body"
    });

    //RESPONSIVE SIDEBAR


    $(".open-right").click(function (e) {
        $("#wrapper").toggleClass("open-right-sidebar");
        e.stopPropagation();
        $("body").trigger("resize");
    });


    $(".open-left").click(function (e) {
        e.stopPropagation();
        $("#wrapper").toggleClass("enlarged");
        $("#wrapper").addClass("forced");

        if ($("#wrapper").hasClass("enlarged") && $("body").hasClass("fixed-left")) {
            $("body").removeClass("fixed-left").addClass("fixed-left-void");
        } else if (!$("#wrapper").hasClass("enlarged") && $("body").hasClass("fixed-left-void")) {
            $("body").removeClass("fixed-left-void").addClass("fixed-left");
        }
        if ($("#wrapper").hasClass("enlarged")) {
            $(".left ul").removeAttr("style");
        } else {
            $(".subdrop").siblings("ul:first").show();
        }
        toggle_slimscroll(".slimscrollleft");
        $("body").trigger("resize");
    });

    // LEFT SIDE MAIN NAVIGATION
    $("#sidebar-menu a").on('click', function (e) {
        if (!$("#wrapper").hasClass("enlarged")) {

            if ($(this).parent().hasClass("has_sub")) {
                e.preventDefault();
            }

            if (!$(this).hasClass("subdrop")) {
                // hide any open menus and remove all other classes
                $("ul", $(this).parents("ul:first")).slideUp(350);
                $("a", $(this).parents("ul:first")).removeClass("subdrop");
                $("#sidebar-menu .pull-right i").removeClass("fa-angle-up").addClass("fa-angle-down");

                // open our new menu and add the open class
                $(this).next("ul").slideDown(350);
                $(this).addClass("subdrop");
                $(".pull-right i", $(this).parents(".has_sub:last")).removeClass("fa-angle-down").addClass("fa-angle-up");
                $(".pull-right i", $(this).siblings("ul")).removeClass("fa-angle-up").addClass("fa-angle-down");
            } else if ($(this).hasClass("subdrop")) {
                $(this).removeClass("subdrop");
                $(this).next("ul").slideUp(350);
                $(".pull-right i", $(this).parent()).removeClass("fa-angle-up").addClass("fa-angle-down");
                //$(".pull-right i",$(this).parents("ul:eq(1)")).removeClass("fa-chevron-down").addClass("fa-chevron-left");
            }
        }
    });

    // NAVIGATION HIGHLIGHT & OPEN PARENT
    $("#sidebar-menu ul li.has_sub a.active").parents("li:last").children("a:first").addClass("active").trigger("click");

    //WIDGET ACTIONS
    $(".widget-header .widget-close").on("click", function (event) {
        event.preventDefault();
        $item = $(this).parents(".widget:first");
        bootbox.confirm("Are you sure to remove this widget?", function (result) {
            if (result === true) {
                $item.addClass("animated bounceOutUp");
                window.setTimeout(function () {
                    if ($item.data("is-app")) {

                        $item.removeClass("animated bounceOutUp");
                        if ($item.hasClass("ui-draggable")) {
                            $item.find(".widget-popout").click();
                        }
                        $item.hide();
                        $("a[data-app='" + $item.attr("id") + "']").addClass("clickable");
                    } else {
                        $item.remove();
                    }
                }, 300);
            }
        });
    });

    $(document).on("click", ".widget-header .widget-toggle", function (event) {
        event.preventDefault();
        $(this).toggleClass("closed").parents(".widget:first").find(".widget-content").slideToggle();
    });

    $(document).on("click", ".widget-header .widget-popout", function (event) {
        event.preventDefault();
        var widget = $(this).parents(".widget:first");
        if (widget.hasClass("modal-widget")) {
            $("i", this).removeClass("icon-window").addClass("icon-publish");
            widget.removeAttr("style").removeClass("modal-widget");
            widget.find(".widget-maximize,.widget-toggle").removeClass("nevershow");
            widget.draggable("destroy").resizable("destroy");
        } else {
            widget.removeClass("maximized");
            widget.find(".widget-maximize,.widget-toggle").addClass("nevershow");
            $("i", this).removeClass("icon-publish").addClass("icon-window");
            var w = widget.width();
            var h = widget.height();
            widget.addClass("modal-widget").removeAttr("style").width(w).height(h);
            $(widget).draggable({ handle: ".widget-header", containment: ".content-page" }).css({ "left": widget.position().left - 2, "top": widget.position().top - 2 }).resizable({ minHeight: 150, minWidth: 200 });
        }
        window.setTimeout(function () {
            $("body").trigger("resize");
        }, 300);
    });

    $("a[data-app]").each(function (e) {
        var app = $(this).data("app");
        var status = $(this).data("status");
        $("#" + app).data("is-app", true);
        if (status == "inactive") {
            $("#" + app).hide();
            $(this).addClass("clickable");
        }
    });

    $(document).on("click", "a[data-app].clickable", function (event) {
        event.preventDefault();
        $(this).removeClass("clickable");
        var app = $(this).data("app");
        $("#" + app).show();
        $("#" + app + " .widget-popout").click();
        topd = $("#" + app).offset().top - $(window).scrollTop();
        $("#" + app).css({ "left": "10px", "top": -(topd - 60) + "px" }).addClass("fadeInDown animated");
        window.setTimeout(function () {
            $("#" + app).removeClass("fadeInDown animated");
        }, 300);
    });

    $(document).on("click", ".widget", function () {
        if ($(this).hasClass("modal-widget")) {
            $(".modal-widget").css("z-index", 5);
            $(this).css("z-index", 6);
        }
    });

    $(document).on("click", '.widget .reload', function (event) {
        event.preventDefault();
        var el = $(this).parents(".widget:first");
        blockUI(el);
        window.setTimeout(function () {
            unblockUI(el);
        }, 1000);
    });

    $(document).on("click", ".widget-header .widget-maximize", function (event) {
        event.preventDefault();
        $(this).parents(".widget:first").removeAttr("style").toggleClass("maximized");
        $("i", this).toggleClass("icon-resize-full-1").toggleClass("icon-resize-small-1");
        $(this).parents(".widget:first").find(".widget-toggle").toggleClass("nevershow");
        $("body").trigger("resize");
        return false;
    });

    $(".portlets").sortable({
        connectWith: ".portlets",
        handle: ".widget-header",
        cancel: ".modal-widget",
        opacity: 0.5,
        dropOnEmpty: true,
        forcePlaceholderSize: true,
        receive: function (event, ui) { $("body").trigger("resize") }
    });

    
    // Init Code Highlighter
    prettyPrint();

    //RUN RESIZE ITEMS
    $(window).resize(debounce(resizeitems, 100));
    $("body").trigger("resize");

    //SELECT
    $('.selectpicker').selectpicker();


    //FILE INPUT
    $('input[type=file]').bootstrapFileInput();

    $(function () {
        $('.summernote').summernote({
            height: 500
        });
    });
    //Data Tables
    $(function() {
        $("#datatables-1").dataTable();

        var table = $('#datatables-2').DataTable();

        $("#datatables-2 tfoot th").each(function(i) {
            var select = $('<select class="form-control input-sm"><option value=""></option></select>')
                .appendTo($(this).empty())
                .on('change', function() {
                    table.column(i)
                        .search('^' + $(this).val() + '$', true, false)
                        .draw();
                });

            table.column(i).data().unique().sort().each(function(d, j) {
                select.append('<option value="' + d + '">' + d + '</option>')
            });
        });

        $('#datatables-3').dataTable({
            "footerCallback": function(row, data, start, end, display) {
                var api = this.api(), data;

                // Remove the formatting to get integer data for summation
                var intVal = function(i) {
                    return typeof i === 'string' ?
                        i.replace(/[\$,]/g, '') * 1 :
                        typeof i === 'number' ?
                        i : 0;
                };

                // Total over all pages
                data = api.column(4).data();
                total = data.length ?
                    data.reduce(function(a, b) {
                        return intVal(a) + intVal(b);
                    }) :
                    0;

                // Total over this page
                data = api.column(4, { page: 'current' }).data();
                pageTotal = data.length ?
                    data.reduce(function(a, b) {
                        return intVal(a) + intVal(b);
                    }) :
                    0;

                // Update footer
                $(api.column(4).footer()).html(
                    '$' + pageTotal + ' ( $' + total + ' total)'
                );
            }
        });
        $('#datatables-4').DataTable({
            dom: 'T<"clear">lfrtip',
            tableTools: {
                "sSwfPath": "./Scripts/js/plugins/jquery-datatables/extensions/TableTools/swf/copy_csv_xls_pdf.swf"
            }
        });
    });

    //DATE PICKER
    $('.datepicker-input').datepicker();

    $(function () {

        $('#ckeditor').ckeditor({ skin: 'bootstrapck' });

        $.fn.editable.defaults.mode = 'inline';
        //defaults
        $.fn.editable.defaults.url = '/post';

        //enable / disable
        $('#enable').click(function () {
            $('#user .editable').editable('toggleDisabled');
        });



        //editables 
        $('#username').editable({
            url: '/post',
            type: 'text',
            pk: 1,
            name: 'username',
            title: 'Enter username'
        });

        $('#firstname').editable({
            validate: function (value) {
                if ($.trim(value) == '') return 'This field is required';
            }
        });

        $('#sex').editable({
            prepend: "not selected",
            source: [
                { value: 1, text: 'Male' },
                { value: 2, text: 'Female' }
            ],
            display: function (value, sourceData) {
                var colors = { "": "gray", 1: "green", 2: "blue" },
                    elem = $.grep(sourceData, function (o) { return o.value == value; });

                if (elem.length) {
                    $(this).text(elem[0].text).css("color", colors[value]);
                } else {
                    $(this).empty();
                }
            }
        });

        $('#status').editable();

        $('#group').editable({
            showbuttons: false
        });

        $('#vacation').editable({
            datepicker: {
                todayBtn: 'linked'
            }
        });

        $('#dob').editable();

        $('#event').editable({
            placement: 'right',
            combodate: {
                firstItem: 'name'
            }
        });

        $('#meeting_start').editable({
            format: 'yyyy-mm-dd hh:ii',
            viewformat: 'dd/mm/yyyy hh:ii',
            validate: function (v) {
                if (v && v.getDate() == 10) return 'Day cant be 10!';
            },
            datetimepicker: {
                todayBtn: 'linked',
                weekStart: 1
            }
        });

        $('#comments').editable({
            showbuttons: 'bottom'
        });

        $('#note').editable();
        $('#pencil').click(function (e) {
            e.stopPropagation();
            e.preventDefault();
            $('#note').editable('toggle');
        });

        $('#state').editable({
            source: ["Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Dakota", "North Carolina", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"]
        });

        $('#state2').editable({
            value: 'California',
            typeahead: {
                name: 'state',
                local: ["Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Dakota", "North Carolina", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"]
            }
        });

        $('#fruits').editable({
            pk: 1,
            limit: 3,
            source: [
             { value: 1, text: 'banana' },
             { value: 2, text: 'peach' },
             { value: 3, text: 'apple' },
             { value: 4, text: 'watermelon' },
             { value: 5, text: 'orange' }
            ]
        });

        $('#tags').editable({
            inputclass: 'input-large',
            select2: {
                tags: ['html', 'javascript', 'css', 'ajax'],
                tokenSeparators: [",", " "]
            }
        });

        var countries = [];
        $.each({ "BD": "Bangladesh", "BE": "Belgium", "BF": "Burkina Faso", "BG": "Bulgaria", "BA": "Bosnia and Herzegovina", "BB": "Barbados", "WF": "Wallis and Futuna", "BL": "Saint Bartelemey", "BM": "Bermuda", "BN": "Brunei Darussalam", "BO": "Bolivia", "BH": "Bahrain", "BI": "Burundi", "BJ": "Benin", "BT": "Bhutan", "JM": "Jamaica", "BV": "Bouvet Island", "BW": "Botswana", "WS": "Samoa", "BR": "Brazil", "BS": "Bahamas", "JE": "Jersey", "BY": "Belarus", "O1": "Other Country", "LV": "Latvia", "RW": "Rwanda", "RS": "Serbia", "TL": "Timor-Leste", "RE": "Reunion", "LU": "Luxembourg", "TJ": "Tajikistan", "RO": "Romania", "PG": "Papua New Guinea", "GW": "Guinea-Bissau", "GU": "Guam", "GT": "Guatemala", "GS": "South Georgia and the South Sandwich Islands", "GR": "Greece", "GQ": "Equatorial Guinea", "GP": "Guadeloupe", "JP": "Japan", "GY": "Guyana", "GG": "Guernsey", "GF": "French Guiana", "GE": "Georgia", "GD": "Grenada", "GB": "United Kingdom", "GA": "Gabon", "SV": "El Salvador", "GN": "Guinea", "GM": "Gambia", "GL": "Greenland", "GI": "Gibraltar", "GH": "Ghana", "OM": "Oman", "TN": "Tunisia", "JO": "Jordan", "HR": "Croatia", "HT": "Haiti", "HU": "Hungary", "HK": "Hong Kong", "HN": "Honduras", "HM": "Heard Island and McDonald Islands", "VE": "Venezuela", "PR": "Puerto Rico", "PS": "Palestinian Territory", "PW": "Palau", "PT": "Portugal", "SJ": "Svalbard and Jan Mayen", "PY": "Paraguay", "IQ": "Iraq", "PA": "Panama", "PF": "French Polynesia", "BZ": "Belize", "PE": "Peru", "PK": "Pakistan", "PH": "Philippines", "PN": "Pitcairn", "TM": "Turkmenistan", "PL": "Poland", "PM": "Saint Pierre and Miquelon", "ZM": "Zambia", "EH": "Western Sahara", "RU": "Russian Federation", "EE": "Estonia", "EG": "Egypt", "TK": "Tokelau", "ZA": "South Africa", "EC": "Ecuador", "IT": "Italy", "VN": "Vietnam", "SB": "Solomon Islands", "EU": "Europe", "ET": "Ethiopia", "SO": "Somalia", "ZW": "Zimbabwe", "SA": "Saudi Arabia", "ES": "Spain", "ER": "Eritrea", "ME": "Montenegro", "MD": "Moldova, Republic of", "MG": "Madagascar", "MF": "Saint Martin", "MA": "Morocco", "MC": "Monaco", "UZ": "Uzbekistan", "MM": "Myanmar", "ML": "Mali", "MO": "Macao", "MN": "Mongolia", "MH": "Marshall Islands", "MK": "Macedonia", "MU": "Mauritius", "MT": "Malta", "MW": "Malawi", "MV": "Maldives", "MQ": "Martinique", "MP": "Northern Mariana Islands", "MS": "Montserrat", "MR": "Mauritania", "IM": "Isle of Man", "UG": "Uganda", "TZ": "Tanzania, United Republic of", "MY": "Malaysia", "MX": "Mexico", "IL": "Israel", "FR": "France", "IO": "British Indian Ocean Territory", "FX": "France, Metropolitan", "SH": "Saint Helena", "FI": "Finland", "FJ": "Fiji", "FK": "Falkland Islands (Malvinas)", "FM": "Micronesia, Federated States of", "FO": "Faroe Islands", "NI": "Nicaragua", "NL": "Netherlands", "NO": "Norway", "NA": "Namibia", "VU": "Vanuatu", "NC": "New Caledonia", "NE": "Niger", "NF": "Norfolk Island", "NG": "Nigeria", "NZ": "New Zealand", "NP": "Nepal", "NR": "Nauru", "NU": "Niue", "CK": "Cook Islands", "CI": "Cote d'Ivoire", "CH": "Switzerland", "CO": "Colombia", "CN": "China", "CM": "Cameroon", "CL": "Chile", "CC": "Cocos (Keeling) Islands", "CA": "Canada", "CG": "Congo", "CF": "Central African Republic", "CD": "Congo, The Democratic Republic of the", "CZ": "Czech Republic", "CY": "Cyprus", "CX": "Christmas Island", "CR": "Costa Rica", "CV": "Cape Verde", "CU": "Cuba", "SZ": "Swaziland", "SY": "Syrian Arab Republic", "KG": "Kyrgyzstan", "KE": "Kenya", "SR": "Suriname", "KI": "Kiribati", "KH": "Cambodia", "KN": "Saint Kitts and Nevis", "KM": "Comoros", "ST": "Sao Tome and Principe", "SK": "Slovakia", "KR": "Korea, Republic of", "SI": "Slovenia", "KP": "Korea, Democratic People's Republic of", "KW": "Kuwait", "SN": "Senegal", "SM": "San Marino", "SL": "Sierra Leone", "SC": "Seychelles", "KZ": "Kazakhstan", "KY": "Cayman Islands", "SG": "Singapore", "SE": "Sweden", "SD": "Sudan", "DO": "Dominican Republic", "DM": "Dominica", "DJ": "Djibouti", "DK": "Denmark", "VG": "Virgin Islands, British", "DE": "Germany", "YE": "Yemen", "DZ": "Algeria", "US": "United States", "UY": "Uruguay", "YT": "Mayotte", "UM": "United States Minor Outlying Islands", "LB": "Lebanon", "LC": "Saint Lucia", "LA": "Lao People's Democratic Republic", "TV": "Tuvalu", "TW": "Taiwan", "TT": "Trinidad and Tobago", "TR": "Turkey", "LK": "Sri Lanka", "LI": "Liechtenstein", "A1": "Anonymous Proxy", "TO": "Tonga", "LT": "Lithuania", "A2": "Satellite Provider", "LR": "Liberia", "LS": "Lesotho", "TH": "Thailand", "TF": "French Southern Territories", "TG": "Togo", "TD": "Chad", "TC": "Turks and Caicos Islands", "LY": "Libyan Arab Jamahiriya", "VA": "Holy See (Vatican City State)", "VC": "Saint Vincent and the Grenadines", "AE": "United Arab Emirates", "AD": "Andorra", "AG": "Antigua and Barbuda", "AF": "Afghanistan", "AI": "Anguilla", "VI": "Virgin Islands, U.S.", "IS": "Iceland", "IR": "Iran, Islamic Republic of", "AM": "Armenia", "AL": "Albania", "AO": "Angola", "AN": "Netherlands Antilles", "AQ": "Antarctica", "AP": "Asia/Pacific Region", "AS": "American Samoa", "AR": "Argentina", "AU": "Australia", "AT": "Austria", "AW": "Aruba", "IN": "India", "AX": "Aland Islands", "AZ": "Azerbaijan", "IE": "Ireland", "ID": "Indonesia", "UA": "Ukraine", "QA": "Qatar", "MZ": "Mozambique" }, function (k, v) {
            countries.push({ id: k, text: v });
        });
        $('#country').editable({
            source: countries,
            select2: {
                width: 200,
                placeholder: 'Select country',
                allowClear: true
            }
        });



        $('#address').editable({
            url: '/post',
            value: {
                city: "Moscow",
                street: "Lenina",
                building: "12"
            },
            validate: function (value) {
                if (value.city == '') return 'city is required!';
            },
            display: function (value) {
                if (!value) {
                    $(this).empty();
                    return;
                }
                var html = '<b>' + $('<div>').text(value.city).html() + '</b>, ' + $('<div>').text(value.street).html() + ' st., bld. ' + $('<div>').text(value.building).html();
                $(this).html(html);
            }
        });

        $('#user .editable').on('hidden', function (e, reason) {
            if (reason === 'save' || reason === 'nochange') {
                var $next = $(this).closest('tr').next().find('.editable');
                if ($('#autoopen').is(':checked')) {
                    setTimeout(function () {
                        $next.editable('show');
                    }, 300);
                } else {
                    $next.focus();
                }
            }
        });

    });
    
        var charts = $('.percentage');
        charts.easyPieChart({
            animate: 1000,
            lineWidth: 5,
            barColor: "#eb5055",
            lineCap: "butt",
            size: "150",
            scaleColor: "transparent",
            onStep: function (from, to, percent) {
                $(this.el).find('.cpercent').text(Math.round(percent));
            }
        });
        //$('.updatePieCharts').on('click', function (e) {
        //    e.preventDefault();
        //    charts.each(function () {
        //        $(this).data('easyPieChart').update(Math.floor(100 * Math.random()));
        //    });
        //});
    
    //Wiz
        $(function () {
            $('#myWizard').easyWizard({
                buttonsClass: 'btn btn-default',
                submitButtonClass: 'btn btn-primary'
            });
        });

     //ICHECK
    //$('input:not(.ios-switch)').iCheck({
    //    checkboxClass: 'icheckbox_square-aero',
    //    radioClass: 'iradio_square-aero',
    //    increaseArea: '20%' // optional
    //});

    // IOS7 SWITCH
    $(".ios-switch").each(function () {
        mySwitch = new Switch(this);
    });

    //GALLERY
    $('.gallery-wrap').each(function () { // the containers for all your galleries
        $(this).magnificPopup({
            delegate: 'a.zooming', // the selector for gallery item
            type: 'image',
            removalDelay: 300,
            mainClass: 'mfp-fade',
            gallery: {
                enabled: true
            }
        });
    });

    /* bootstrap tooltip */
    $(".tip").tooltip({ placement: 'top' });
    $(".tipb").tooltip({ placement: 'bottom' });
    $(".tipl").tooltip({ placement: 'left' });
    $(".tipr").tooltip({ placement: 'right' });
    /* eof bootstrap tooltip */

    /* scroll */
    if ($("#layout_scroll").length > 0)
        $("#layout_scroll").height($(window).height() - 80);

    if ($(".scroll").length > 0) $(".scroll").mCustomScrollbar({ advanced: { autoScrollOnFocus: false } });

    $(".modal").on('shown.bs.modal', function () {
        $(this).find('.scroll').mCustomScrollbar('update');
    });

    if ($(".scroll-mail").length > 0) {
        $(".scroll-mail").height($(window).height() - 185).mCustomScrollbar({ advanced: { autoScrollOnFocus: false } });
    }
    /* eof scroll */

    //Bootstrap file input    
    if ($("input.fileinput").length > 0)
        $("input.fileinput").bootstrapFileInput();
    //END Bootstrap file input    

    /* Accordion */
    if ($(".accordion").length > 0) {
        $(".accordion").accordion({ heightStyle: "content" });
        $(".accordion .ui-accordion-header:last").css('border-bottom', '0px');
    }
    /* EOF Accordion */

    /* uniform */
    if ($("input[type=checkbox]").length > 0 || $("input[type=radio]").length > 0)
        $("input[type=checkbox], input[type=radio]").not('.skip').uniform();
    /* eof uniform */

    /* select2 */
    if ($(".select2").length > 0) $(".select2").select2();
    /* eof select2 */

    /* tagsinput */
    if ($(".tags").length > 0) $(".tags").tagsInput({ width: '100%', height: 'auto' });
    if ($(".mail_tags").length > 0) $(".mail_tags").tagsInput({ width: '100%', height: 'auto', 'defaultText': 'add email' });
    /* eof tagsinput */

    /* jQuery UI Datepicker */
    if ($(".datepicker").length > 0) $(".datepicker").datepicker({ nextText: "", prevText: "" });
    if ($(".mdatepicker").length > 0) $(".mdatepicker").datepicker({ numberOfMonths: 3, nextText: "", prevText: "" });
    /* EOF jQuery UI Datepicker */

    /* Timepicker */
    if ($(".timepicker").length > 0) $(".timepicker").timepicker();
    /* EOF Timepicker */

    /* Datetimepicker */
    if ($(".datetimepicker").length > 0) $(".datetimepicker").datetimepicker({ nextText: "", prevText: "" });
    /* EOF Datetimepicker */

    /* Datatables */
    if ($("table.sortable_simple").length > 0)
        $("table.sortable_simple").dataTable({ "iDisplayLength": 5, "bLengthChange": false, "bFilter": false, "bInfo": false, "bPaginate": true });

    if ($("table.sortable_default").length > 0)
        $("table.sortable_default").dataTable({ "iDisplayLength": 5, "sPaginationType": "full_numbers", "bLengthChange": false, "bFilter": false, "bInfo": false, "bPaginate": true, "aoColumns": [{ "bSortable": false }, null, null, null, null] });

    if ($("table.sortable").length > 0)
        $("table.sortable").dataTable({ "iDisplayLength": 5, "aLengthMenu": [5, 10, 25, 50, 100], "sPaginationType": "full_numbers", "aoColumns": [{ "bSortable": false }, null, null, null, null] });
    /* eof Datatables */

    /* knob plugin */
    if ($(".knob").length > 0) $(".knob input").knob();
    /* eof knob plugin */

    /* sparkline */
    if ($(".sparkline").length > 0)
        $('.sparkline span').sparkline('html', { enableTagOptions: true });
    /* eof sparkline */

    /* CLEditor */
    if ($(".cle").length > 0)
        cE = $(".cle").cleditor({ width: "100%", height: 230 });

    if ($(".cleditor").length > 0)
        cEdit = $(".cleditor").cleditor({ width: "100%", height: 450 });

    if ($(".scle").length > 0)
        cEC = $(".scle").cleditor({ width: "100%", height: 230, controls: "bold italic underline strikethrough link unlink" })[0].focus();

    $('#modal_mail').on('shown.bs.modal', function () {
        cEC.refresh();
    });
    /* eof CLEditor */

    /* draggable modal */
    if ($(".modal-draggable").length > 0) {
        $(".modal-draggable").draggable();
    }
    /* eof draggable modal */

    /* Tinymce */
    if ($("textarea.tmce").length > 0) {
        tinymce.init({
            selector: "textarea.tmce",
            height: 400
        });
    }
    if ($("textarea.stmce").length > 0) {
        tinymce.init({
            selector: "textarea.stmce",
            toolbar: "bold italic | alignleft aligncenter alignright alignjustify | undo redo",
            menu: [],
            height: 200
        });
    }
    /* eof tinymce */

    /* ValidationEngine */
    if ($("#validate").length > 0 || $("#validate_custom").length > 0)
        $("#validate, #validate_custom").validationEngine('attach', { promptPosition: "topLeft" });
    /* EOF ValidationEngine */

    /* Stepy*/
    if ($("#wizard").length > 0) $('#wizard').stepy();

    if ($("#wizard_validate").length > 0) {
        $("#wizard_validate").validationEngine('attach', { promptPosition: "topLeft" });
        $('#wizard_validate').stepy({
            back: function (index) {
                //if(!$("#wizard_validate").validationEngine('validate')) return false; //uncomment if u need to validate on back click                
            },
            next: function (index) {
                if (!$("#wizard_validate").validationEngine('validate')) return false;
            },
            finish: function (index) {
                if (!$("#wizard_validate").validationEngine('validate')) return false;
            }
        });
    }
    /* EOF Stepy */

    /* Masked Input */
    if ($("input[class^='mask_']").length > 0) {
        $("input.mask_tin").mask('99-9999999');
        $("input.mask_ssn").mask('999-99-9999');
        $("input.mask_date").mask('9999-99-99');
        $("input.mask_product").mask('a*-999-a999');
        $("input.mask_phone").mask('99 (999) 999-99-99');
        $("input.mask_phone_ext").mask('99 (999) 999-9999? x99999');
        $("input.mask_credit").mask('9999-9999-9999-9999');
        $("input.mask_percent").mask('99%');
    }
    /* EOF Masked Input */

    /* Syntax Highlight */
    if ($("pre[class^=brush]").length > 0) {
        SyntaxHighlighter.defaults['toolbar'] = false;
        SyntaxHighlighter.all();
    }
    /* EOF Syntax Highlight */

    /* Fancybox */
    if ($(".fancybox").length > 0)
        $(".fancybox").fancybox({ padding: 10 });
    /* EOF Fancybox */

    /* carousel */
    if ($('.carousel').length > 0) $('.carousel').carousel();
    /* eof carousel */

    /* fullcalendar (demo) */

    if ($("#calendar").length > 0) {

        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();


        $('#external-events .external-event').each(function () {
            var eventObject = { title: $.trim($(this).text()) };

            $(this).data('eventObject', eventObject);
            $(this).draggable({
                zIndex: 999,
                revert: true,
                revertDuration: 0
            });
        });

        var calendar = $('#calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            editable: true,
            events: [{ title: 'All Day Event', start: new Date(y, m, 1) },
                     { title: 'Long Event', start: new Date(y, m, d - 5), end: new Date(y, m, d - 2) },
                     { id: 999, title: 'Repeating Event', start: new Date(y, m, d - 3, 16, 0), allDay: false },
                     { id: 999, title: 'Repeating Event', start: new Date(y, m, d + 4, 16, 0), allDay: false },
                     { title: 'Meeting', start: new Date(y, m, d, 10, 30), allDay: false },
                     { title: 'Lunch', start: new Date(y, m, d, 12, 0), end: new Date(y, m, d, 14, 0), allDay: false },
                     { title: 'Birthday Party', start: new Date(y, m, d + 1, 19, 0), end: new Date(y, m, d + 1, 22, 30), allDay: false },
                     { title: 'Click for Google', start: new Date(y, m, 28), end: new Date(y, m, 29), url: 'http://google.com/' }],
            droppable: true,
            selectable: true,
            selectHelper: true,
            select: function (start, end, allDay) {
                var title = prompt('Event Title:');
                if (title) {
                    calendar.fullCalendar('renderEvent',
                    {
                        title: title,
                        start: start,
                        end: end,
                        allDay: allDay
                    },
                    true
                    );
                }
                calendar.fullCalendar('unselect');
            },
            drop: function (date, allDay) {

                var originalEventObject = $(this).data('eventObject');

                var copiedEventObject = $.extend({}, originalEventObject);

                copiedEventObject.start = date;
                copiedEventObject.allDay = allDay;

                $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);


                if ($('#drop-remove').is(':checked')) {
                    $(this).remove();
                }

            }
        });


    }

    /* eof fullcalendar (demo) */

    /* popover (demo) */
    $("[data-toggle=popover]").popover();
    /* eof popover (demo) */

    /* slider example(demo) */
    $(".slider_example").slider({ range: "min", min: 0, max: 100, value: 50 });
    $(".slider_example2").slider({ range: true, min: 0, max: 500, values: [150, 350] });
    $(".slider_example3").slider({ orientation: "vertical", range: "min", min: 0, max: 100, value: 50 });
    $(".slider_example4").slider({ orientation: "vertical", min: 0, max: 500, range: true, values: [150, 350] });


    if ($("#price_rage").length > 0) {
        $("#price_rage").slider({
            range: true, min: 0, max: 3000, values: [1000, 2000],
            slide: function (event, ui) {
                $("#price_amount").html("$" + ui.values[0] + " - $" + ui.values[1]);
            }
        });
        $("#price_amount").html("$" + $("#price_rage").slider("values", 0) + " - $" + $("#price_rage").slider("values", 1));
    }
    /* eof slider example(demo) */

    /* slider example(demo) */
    $("#spinner").spinner();
    $("#spinner2").spinner({ step: 0.1 });
    $("#spinner3").spinner({ min: 0, max: 2500, step: 25.15, numberFormat: "C" });
    /* eof slider example(demo) */

    /* button state(demo)*/
    $('#fat-btn').click(function () {
        var btn = $(this);
        btn.button('loading');
        setTimeout(function () {
            btn.button('reset');
        }, 3000);
    });
    /* eof button state(demo)*/

    /* sortable (demo)*/
    if ($("#sortable_example_1").length > 0) {
        $("#sortable_example_1").sortable({ items: ".list-group-item" });
        $("#sortable_example_1").disableSelection();
    }
    /* eof sortable (demo)*/

    /* selectable (demo)*/
    if ($("#selectable_example_1").length > 0) {
        $("#selectable_example_1").selectable();
    }
    /* eof selectable (demo)*/
});

$(window).resize(function () {

    if ($("#layout_scroll").length > 0)
        $("#layout_scroll").height($(window).height() - 80).mCustomScrollbar('update');

    if ($(".scroll-mail").length > 0)
        $(".scroll-mail").height($(window).height() - 205).mCustomScrollbar('update');

});


    var debounce = function (func, wait, immediate) {
        var timeout, result;
        return function () {
            var context = this, args = arguments;
            var later = function () {
                timeout = null;
                if (!immediate) result = func.apply(context, args);
            };
            var callNow = immediate && !timeout;
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
            if (callNow) result = func.apply(context, args);
            return result;
        };
    };

function resizeitems() {
        if ($.isArray(resizefunc)) {
            for (i = 0; i < resizefunc.length; i++) {
                window[resizefunc[i]]();
            }
        }
    }

    function initscrolls() {
        if (jQuery.browser.mobile !== true) {
            //SLIM SCROLL
            $('.slimscroller').slimscroll({
                height: 'auto',
                size: "5px"
            });

            $('.slimscrollleft').slimScroll({
                height: 'auto',
                position: 'left',
                size: "5px",
                color: '#7A868F'
            });
        }
    }
    function toggle_slimscroll(item) {
        if ($("#wrapper").hasClass("enlarged")) {
            $(item).css("overflow", "inherit").parent().css("overflow", "inherit");
            $(item).siblings(".slimScrollBar").css("visibility", "hidden");
        } else {
            $(item).css("overflow", "hidden").parent().css("overflow", "hidden");
            $(item).siblings(".slimScrollBar").css("visibility", "visible");
        }
    }

    function nifty_modal_alert(effect, header, text) {

        var randLetter = String.fromCharCode(65 + Math.floor(Math.random() * 26));
        var uniqid = randLetter + Date.now();

        $modal = '<div class="md-modal md-effect-' + effect + '" id="' + uniqid + '">';
        $modal += '<div class="md-content">';
        $modal += '<h3>' + header + '</h3>';
        $modal += '<div class="md-modal-body">' + text;
        $modal += '</div>';
        $modal += '</div>';
        $modal += '</div>';

        $("body").prepend($modal);

        window.setTimeout(function () {
            $("#" + uniqid).addClass("md-show");
            $(".md-overlay,.md-close").click(function () {
                $("#" + uniqid).removeClass("md-show");
                window.setTimeout(function () { $("#" + uniqid).remove(); }, 500);
            });
        }, 100);

        return false;
    }

    function blockUI(item) {
        $(item).block({
            message: '<div class="loading"></div>',
            css: {
                border: 'none',
                width: '14px',
                backgroundColor: 'none'
            },
            overlayCSS: {
                backgroundColor: '#fff',
                opacity: 0.4,
                cursor: 'wait'
            }
        });
    }

    function unblockUI(item) {
        $(item).unblock();
    }

    function toggle_fullscreen() {
        var fullscreenEnabled = document.fullscreenEnabled || document.mozFullScreenEnabled || document.webkitFullscreenEnabled;
        if (fullscreenEnabled) {
            if (!document.fullscreenElement && !document.mozFullScreenElement && !document.webkitFullscreenElement && !document.msFullscreenElement) {
                launchIntoFullscreen(document.documentElement);
            } else {
                exitFullscreen();
            }
        }
    }


    // Thanks to http://davidwalsh.name/fullscreen

    function launchIntoFullscreen(element) {
        if (element.requestFullscreen) {
            element.requestFullscreen();
        } else if (element.mozRequestFullScreen) {
            element.mozRequestFullScreen();
        } else if (element.webkitRequestFullscreen) {
            element.webkitRequestFullscreen();
        } else if (element.msRequestFullscreen) {
            element.msRequestFullscreen();
        }
    }

    function exitFullscreen() {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        }
    }
