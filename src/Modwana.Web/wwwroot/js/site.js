
$(function () {

    setupConfirm();

})

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