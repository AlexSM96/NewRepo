function openRequestModal(url) {
    const modal = $('#modal');
    $.ajax({
        type: 'GET',
        url: url,
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