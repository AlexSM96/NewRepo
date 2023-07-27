function openModal(parameters) {
    const url = parameters.url;
    const data = parameters.data;
    const modal = $('#modal');

    if (data === undefined || url === undefined) { 
        alert("Что то пошло не так...")
        return;
    }

    $.ajax({
        type: 'GET',
        url: url,
        data: {id: data},
        success: function (response) {
            modal.find('.modal-body').html(response);
            modal.modal('show');
        },
        failure: function () {
            modal.modal('hide');
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}