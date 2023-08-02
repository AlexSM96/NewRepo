function createPair(parameters) {
    let url = parameters.url;
    let lessonName = parameters.lessonName;
    let teacherId = parameters.teacherId;

    $.ajax({
        type: 'POST',
        url: url,
        data: {
             teacherId,
             lessonName
        },
        success: function (response) {
            alert(response.description);
        },
        error: function (response) {
            alert(response.responseText);
        }
    })
}

function deletePair(parameters) { 
    let url = parameters.url;
    let teacherId = parameters.teacherId;
    let lessonName = parameters.lessonName;

    $.ajax({
        type: 'POST',
        url: url,
        data: {
            teacherId,
            lessonName
        },
        success: function (response) {
            alert(response.description);
        },
        error: function (response) {
            alert(response.responseText);
        }
    })
}