function OnActorPostSuccess(data) {
    if (data !== null && typeof data === "object" && data.success !== undefined && data.success === true) {
        window.location = $("#DisplayActorUrl").val() + "?actorId=" + data.actorId;
    }
}
