

function ShowAlertModal(title, message) {
    $("#alert-modal #alert-modal-body").text(message);
    $("#alert-modal #alert-modal-title").text(title);
    $("#alert-modal").modal("show");
}

function ShowCustomModal(title, html) {
    $("#custom-modal #custom-modal-body").html(html);
    $("#custom-modal #custom-modal-title").text(title);
    $("#custom-modal").modal("show");
}

function HideCustomModal() {
    $("#custom-modal").modal("hide");
}

function validateForm(formId) {
    var form = $(formId);

    var validator = form.kendoValidator().data("kendoValidator");

    if (validator.validate()) {
        return true;
    }
    return false;
}

function validateFormInput(formId, inputId) {
    var form = $(formId);
    var validator = form.kendoValidator().data("kendoValidator");
    if (validator.validateInput($(inputId))) {
        return true;
    }
    return false;
}
