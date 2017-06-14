$(document).ready(function () {
    var customerTable = $('#customer-table').dataTable({
        responsive: true,
        "info": false,
        "bLengthChange": false,
        "pagingType": "simple",
        "pageLength": 10,
        "sDom": '<pf<t>i>',
        "order": [[2, "asc"]],
        "columnDefs": [
            {
                "targets": [0],
                "searchable": false,
                "orderable": false,
                "visible": false
            },
            {
                "targets": [1],
                "searchable": true,
                "orderable": true,
                "visible": true
            },
            {
                "targets": [2],
                "searchable": true,
                "orderable": true,
                "visible": true
            }
        ]
    });
});


// consts
var MESSAGE_HISTORY_URL = "/Pharmacy/PersonHistory/";
var EDIT_CUSTOMER_URL = "/Pharmacy/EditCustomer/";
var EDIT_PHARMACY_URL = "/Admin/EditPharmacy/";
var ADD_PHARMACY_URL = "/Admin/AddPharmacy/";
var ADD_PHARMACIST_URL = "/Admin/AddPharmacist/";
var ADD_PERSON_URL = "/Pharmacy/AddPerson/";

var loadModule = function (url, sendData, type) {
    // default param
    type = type || "GET";

    // call PartialView into Modal
    $.ajax({
        url: url,
        type: type,
        data: { id: sendData },
        dataType: 'html',
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
};


var showModal = function (data) {
    $('#history-container').html(data);
    $('#history-container').modal('show');
}