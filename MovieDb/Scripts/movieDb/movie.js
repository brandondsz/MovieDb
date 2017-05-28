//#region MovieListing
function onAdditionalData() {
    return {
        text: $("#movieSearch").val()
    };
}

function onMovieSelect(e) {    
    var dataItem = this.dataItem(e.item.index());
    window.location = $("#DisplayMovieUrl").val() + "?movieId=" + dataItem.Value;
}

//#endregion 

//#region CreateEditMovie
$(document).ready(function () {
   

});

function onMovieSave(e) {
    window.location = $("#DisplayMovieUrl").val() + "?movieId=" + e.movieId;    
}

function addNewActor(id) {
    $("#" + id).data("kendoMultiSelect").close();
    $.ajax({
        type: "GET",
        url: $("#CreateEditActorPath").val(),
        dataType: "html",
        cache: false,
        success: function (result) {
            ShowCustomModal("Create Actor", result);
        }
    })
}

function addNewProducer(id) {
    $("#" + id).data("kendoMultiSelect").close();
    $.ajax({
        type: "GET",
        url: $("#CreateEditProducerPath").val(),
        dataType: "html",
        //data: data,
        cache: false,
        success: function (result) {
            ShowCustomModal("Create Producer", result);
        }
    });
}
function OnModalPostSuccess(data) {        
    if (data !== null && typeof data === "object" && data.success !== undefined && data.success === true) {
        HideCustomModal();
    }
}

function OnActorPostSuccess(data) {
    if (data !== null && typeof data === "object" && data.success !== undefined && data.success === true) {
        HideCustomModal();
    }
}

function OnProducerPostSuccess(data) {
    if (data !== null && typeof data === "object" && data.success !== undefined && data.success === true) {
        HideCustomModal();
    }
}

function onPosterUploadSelect(e) {
    if (e.files.length > 1) {
        alert("Please select max 1 files.");
        e.preventDefault();
    }
}

function onPosterUploadSuccess(e) {
    //dont allow more than 1
    $("#posterImage").getKendoUpload().disable();
}


function onPosterUploadBegin(e) {
    //use this to store filename with timestamp
    $("#PosterPath").val((new Date().getTime()) + "_" + e.files[0].name)
    e.data = {
        fileName: $("#PosterPath").val()
    };

}
function onPosterUploadRemove() {
    $("#posterImage").getKendoUpload().enable();
}
//#endregion
