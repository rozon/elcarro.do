var SumitForm = function (id) {
    $("#" + id).submit(function (e) {
        e.preventDefault(); //prevent the default action

        //grab the form and wrap it with jQuery
        var $form = $(this);

        if (!$form.valid())
            return;

        //send your ajax request
        $.ajax({
            type: $form.prop('method'),
            url: $form.prop('action'),
            data: $form.serialize(),
            dataType: "json",
            traditional: true,
            success: function (response) {
                if (response.status === "error") {
                    Materialize.toast(response.message, 4000, 'rounded toast-error');
                } else {
                    Materialize.toast(response.message, 4000, 'rounded toast-success');
                }
            },
            error: function (jqXHR, status, exception) {
                console.log(jqXHR);
                console.log(jqXHR.responseText);
                Materialize.toast(jqXHR.statusText + " contact the administrators", 4000, 'rounded toast-error');
            }
        });
    });
}
