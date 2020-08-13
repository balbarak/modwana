
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

    unblock(blockDiv);
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



function updateListFromDiv(div) {

    var url = getRouteUrl(div);

    updateContainer(url, div);
}

function updateContainer(url, divToUpdate) {

    var result = $.Deferred();

    $.ajax(url,
        {
            method: "GET",
            beforeSend: function () {
                block(divToUpdate);
            },
            complete: function (xhr, status) {
                unblock(divToUpdate);
            },
            success: function (data, status, xhr) {

                if (typeof data === 'object') {
                    $(divToUpdate).html(data.partialViewHtml);
                } else {
                    $(divToUpdate).html(data);
                }

                result.resolve(data);
            },
            error: function (xhr, status, error) {
                result.reject;
            }
        });

    return result.promise();
}


function getRouteUrl(div) {

    return $("input[data-route]", div).val();

}

function refreshUnobtrusiveValidation(formSelector) {
    var form = $(formSelector);
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);

}

function scrollToEle(ele, speed) {

    if (!speed)
        speed = 800;

    $('html, body').animate({
        scrollTop: $(ele).offset().top - 70
    }, speed, function () {

        //window.location.hash = ele;
    });

}



//========= Start Inline Form =========

function enableEdit(form) {

    var formId = "#" + $(form).attr("id");

    enableInputs(formId);

    $(form).find(editButtonSelector).hide();
    $(form).find(saveButtonSelector).show();
    $(form).find(cancelButtonSelector).show();

    //updateICheck();
}

function disableEdit(form) {

    if (form) {

        $(form).find(editButtonSelector).show();
        $(form).find(saveButtonSelector).hide();
        $(form).find(cancelButtonSelector).hide();

        var alert = $(form).find('[data-alert]');

        if (alert)
            $(alert).html('');

        var formId = "#" + $(form).attr("id");

        disableInputs(formId);

        //updateICheck();
    }

}

function disableInputs(form) {

    $('input,select,textarea', form).each(
        function (i) {

            if (jQuery().bootstrapSwitch && $(this).hasClass('make-switch')) {

                if (!$(this).bootstrapSwitch('disabled')) {
                    $(this).bootstrapSwitch('toggleDisabled');
                }
            }
            else {

                $(this).prop('disabled', true);
                $(this).attr('disabled', 'disabled');
            }
        });

}

function enableInputs(form) {

    $('input,select,textarea', form).each(
        function (i) {
            if (jQuery().bootstrapSwitch && $(this).hasClass('make-switch')) {

                if ($(this).bootstrapSwitch('disabled')) {
                    $(this).bootstrapSwitch('toggleDisabled');
                }
            }
            else {
                $(this).prop('disabled', false);
                $(this).removeAttr('disabled');
                $(this).prop('readonly', false);

            }
        });

}

function setFormStatus(formId) {

    var isInline = $(formId).data("inline-edit");

    if (!isInline)
        return;

    if ($(formId).data("form-status") === "Edit") {
        enableEdit(formId);
    }
    else {
        disableEdit(formId);
    }
}

function changeFormActionUrlAndStatus(button, status, isPost) {

    var url = $(button).attr('data-action-url');

    var form = $(button).closest('form');

    form.data("form-status", status);


    if (isPost)
        form.attr('method', 'post');
    else
        form.attr('method', 'get');

    form.attr('action', url);

}

function initInlineForms() {

    $("form").each(function () {

        var isInlineForm = $(this).attr("data-inline-edit");

        if (isInlineForm) {

            var id = $(this).attr("id");

            setFormStatus("#" + id);
        }

    });

}

//========= End Inline Form =========

function showModal(modal) {

    $(modal).modal('show');
}