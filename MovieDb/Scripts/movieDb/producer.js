function onProducerPostSuccess(data) {
    if (data !== null && typeof data === "object" && data.success !== undefined && data.success === true) {
        window.location = $("#DisplayProducerUrl").val() + "?producerId=" + data.producerId;
    }
}


function onAdditionalData() {
    return {
        text: $("#producerSearch").val()
    };
}

function onProducerSelect(e) {
    var dataItem = this.dataItem(e.item.index());debugger
    window.location = $("#DisplayProducerUrl").val() + "?producerId=" + dataItem.RowId;
}

function onProducerGridChange(e) {
    grid = e.sender;
    var currentDataItem = grid.dataItem(this.select());
    window.location = $("#DisplayProducerUrl").val() + "?producerId=" + currentDataItem.RowId;
}