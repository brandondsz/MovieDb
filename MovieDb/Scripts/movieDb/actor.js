function onActorPostSuccess(data) {
    if (data !== null && typeof data === "object" && data.success !== undefined && data.success === true) {
        window.location = $("#DisplayActorUrl").val() + "?actorId=" + data.actorId;
    }
}

function onAdditionalData() {
    return {
        text: $("#actorSearch").val()
    };
}

function onActorSelect(e) {
    var dataItem = this.dataItem(e.item.index());
    window.location = $("#DisplayActorUrl").val() + "?actorId=" + dataItem.RowId;
}

function onActorGridChange(e) {
    grid = e.sender;
    var currentDataItem = grid.dataItem(this.select());
    window.location = $("#DisplayActorUrl").val() + "?actorId=" + currentDataItem.RowId;
}