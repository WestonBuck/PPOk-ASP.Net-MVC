$(document).ready(function () {
    // put response into modal
    $('#import-submit').click(function (e) {
        e.preventDefault();

        let form = $('#import-form');
        let url = form.attr('action');
        let formData = new FormData(form[0]);

        // submit form
        $.ajax({
            type: 'POST',
            url: url,
            contentType: false,
            cache: false,
            processData: false,
            data: formData,
            beforeSend: function () {
                $('#spinner').css('display', 'block');
            },
            success: function (data) {
                showModal(data);
            },
            complete: function () {
                $('#spinner').css('display', 'none');
            }
        });
    });
});

var showModal = function (data) {
    $('#import-container').html(data);
    $('#import-container').modal('show');
}

// send response to server
var submitUpload = function (data) {
    console.log(data);
    $.ajax({
        type: 'POST',
        url: '/Import/Upload/',
        dataType: 'json',
        data: { updateList: data }
    });
}


var submitRecall = function (data) {
    console.log(data);
    $.ajax({
        type: 'POST',
        url: '/Import/Upload/',
        dataType: 'json',
        data: { updateList: data, isRecall: true}
    });
}