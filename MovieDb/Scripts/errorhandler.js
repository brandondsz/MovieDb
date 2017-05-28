function logError(ex, stack) {
    if (ex == null) return;
    if (window.logErrorUrl == null) {
        alert("logErrorUrl must be defined.");
        return;
    }

    var url = ex.fileName != null ? ex.fileName : document.location;
    if (stack == null && ex.stack != null) stack = ex.stack;

    // format output
    var out = ex.message != null ? ex.name + ": " + ex.message : ex;
    out += ": at document path '" + url + "'.";
    if (stack != null) out += "\n  at " + stack.join("\n  at ");

    // send error message
    $.ajax({
        type: "POST",
        url: window.logErrorUrl,
        data: { Message: out },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}

Function.prototype.trace = function () {
    var trace = [];
    var current = this;
    while (current) {
        trace.push(current.signature());
        current = current.caller;
    }
    return trace;
};
Function.prototype.signature = function () {
    var signature = {
        name: this.getName(),
        params: [],
        toString: function () {
            var params = this.params.length > 0 ? "'" + this.params.join("', '") + "'" : "";
            return this.name + "(" + params + ")";
        }
    };
    if (this.arguments) {
        for (var x = 0; x < this.arguments.length; x++)
            signature.params.push(this.arguments[x]);
    }
    return signature;
};
Function.prototype.getName = function () {
    if (this.name)
        return this.name;
    var definition = this.toString().split("\n")[0];
    var exp = /^function ([^\s(]+).+/;
    if (exp.test(definition))
        return definition.split("\n")[0].replace(exp, "$1") || "anonymous";
    return "anonymous";
};
window.onerror = function (msg) {
    if (arguments != null && arguments.callee != null && arguments.callee.trace) {
        logError(msg, arguments.callee.trace());
    }
};

$(document).ajaxError(function (event, jqxhr, settings, exception) {
    if (jqxhr.status === 0 || jqxhr.readyState === 0) {
        return;
    }

    if (jqxhr.status === 401) {
        location.reload();
    } else {
        alertModal(jqxhr.statusText, jqxhr.responseText);
    }
});

function handleAjaxError(ajaxContext) {

    alertModal(ajaxContext.statusText, ajaxContext.responseText);
}

function alertModal(title, body) {
    var iFrame = $('<iframe class="iframe-full-view"></iframe>');
    $('#errorModalContent').html(iFrame);

    var iFrameDoc = iFrame[0].contentDocument || iFrame[0].contentWindow.document;
    iFrameDoc.write(body);
    iFrameDoc.close();

    // Display error message to the user in a modal
    $('#errorModalTitle').html(title);
    $('#errorModal').modal('show');
}