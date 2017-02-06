App = {
    version: '1.0.00',
    contentType: 'application/json; charset=utf-8',
    dataType: 'json',

    init: function () {
        App.datePickerLocalize();
        App.initiControls();
    },

    initiControls: function () {
        if (typeof NProgress != 'undefined') {
            $(document).ajaxStart(function () {
                NProgress.set(0.4);
                NProgress.start();
            }).ajaxStop(function () {
                NProgress.done();
            });
        }
        $('#btnDownload')
            .hide()
            .click(App.download);
        $('#tbFrom,#tbTo').datepicker();
        $('#btnOK').click(App.createReport);
    },

    download: function () {
        var id = $(this).attr('fileId');
    },

    datePickerLocalize: function () {
        $.datepicker.regional['ru'] = {
            closeText: 'Закрыть',
            prevText: '&#x3c;Пред',
            nextText: 'След&#x3e;',
            currentText: 'Сегодня',
            monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
            'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
            monthNamesShort: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
            'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
            dayNames: ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
            dayNamesShort: ['вск', 'пнд', 'втр', 'срд', 'чтв', 'птн', 'сбт'],
            dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
            weekHeader: 'Нед',
            dateFormat: 'dd.mm.yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };
        $.datepicker.setDefaults($.datepicker.regional['ru']);
    },

    createReport: function () {
        $(this).attr('disabled', 'disabled');
        $('#btnDownload').hide();
        var postData =
        {
            from: $('#tbFrom').val(),
            to: $('#tbTo').val()
        };
        $.ajax({
            url: '/Home/CreateReport',
            method: 'POST',
            data: JSON.stringify(postData),
            contentType: App.contentType,
            dataType: App.dataType,
            async: true,
            success: function (obj) {
                if (obj.error) {
                    alert(obj.error);
                }
                else {
                    $('#btnDownload')
                        .attr('fileId', obj)
                        .show();
                }
            },
            error: function (jqXHR, exception) {
            },
            complete: function () {
                $('#btnOK').removeAttr('disabled');
            }
        });
    }
}

$(function () {
    App.init();
}); ''