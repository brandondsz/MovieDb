function OnProducerPostSuccess(data) {
    if (data !== null && typeof data === "object" && data.success !== undefined && data.success === true) {
        window.location = $("#DisplayProducerUrl").val() + "?producerId=" + data.producerId;
    }
}
