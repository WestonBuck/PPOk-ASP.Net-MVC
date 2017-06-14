$(document).ready(function () {
    var customerTable = $('#pendingRefills').dataTable({
        responsive: true,
        "info": false,
        "bLengthChange": false,
        "pagingType": "simple",
        "pageLength": 10,
        "sDom": '<pf<t>i>',
        "order": [[0, "asc"]],
        "columnDefs": [
          {
              "targets": [2],
              "searchable": false,
              "orderable": false,
              "visible": true
          }
        ]
    });
});