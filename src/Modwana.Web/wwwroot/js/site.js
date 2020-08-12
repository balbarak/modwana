
$(function () {

    setupConfirm();

})

//========= Start Ajax Setting =========

function block(element) {


    var html = '<div class="loading-box">' +
                '<div class="loading-message">' +
                    '<p>' +
                        '<i class="fa fa-circle-o-notch fa-spin"></i>' +
                        'الرجاء الانتظار ...' +
                    '</p>'
                 '</div>' +
               '</div>';
    var options = {};

    var el = $(element);

    if (el.height() <= ($(window).height())) {
        options.cenrerY = true;
    }

    el.block({
        message: html,
        baseZ: 1000,
        centerY: options.cenrerY !== undefined ? options.cenrerY : false,
        css: {
            top: '10%',
            border: '0',
            padding: '0',
            backgroundColor: 'none'
        },
        overlayCSS: {
            backgroundColor: '#555',
            opacity: 0.5,
            cursor: 'wait'
        }
    });
}

function unblock(element) {

    $(element).unblock();
}

function onAjaxBegin(blockDiv) {

    block(blockDiv);
}

function onAjaxFailed(xhr, status, error, alertDiv, formId) {

    var data = xhr.responseJSON;

    if (!data) {
        data = xhr;
    }

    var isInline = $(formId).data("inline-edit");

    if (isInline) {
        $(formId).data("form-status", "Edit");

    }

    if (alertDiv)
        scrollToEle(alertDiv, 500);

}

function onAjaxSuccess(xhr, status, modalToHide) {
    if (modalToHide) {
        $(modalToHide).modal('hide');
    }
}

function onAjaxComplete(xhr, status, blockDiv, alertDiv, divToReplace, formId) {
    var data = xhr.responseJSON;

    if (!data) {
        data = xhr;
    }

    if (data) {

        if (data.isRedirect)
            window.location.href = data.redirectUrl;

        if (data.success || status === "success") {

            if (data.partialViewHtml)
                $(divToReplace).html(data.partialViewHtml);
            else
                $(divToReplace).html(data.responseText);

        }

        showAlert(data.alert, alertDiv);

        setFormStatus(formId);

    }

    if (formId)
        refreshUnobtrusiveValidation(formId);
}

//========= End Ajax Setting =========

function setupConfirm() {

    $('#confirm-modal').on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget);

        var action = button.data('action');

        $("#confirm-form").attr("action", action);
    });

    $('#confirm-ajax-modal').on('show.bs.modal', function (event) {

        var formSelector = "#confirm-ajax-form";

        $("#confirm-ajax-alert").html("");

        var button = $(event.relatedTarget);

        var action = button.data('action');
        var complete = button.data('complete');
        var success = button.data('success');

        if (complete)
            $(formSelector).attr("data-ajax-complete", complete);

        if (success)
            $(formSelector).attr("data-ajax-success", success);


        $(formSelector).attr("action", action);
    });
}


function refreshUnobtrusiveValidation(formSelector) {
    var form = $(formSelector);
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);

}