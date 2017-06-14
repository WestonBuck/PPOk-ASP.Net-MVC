$(document).ready(function () {
    var histTable = $('#history-table').DataTable({
        "info": false,
        "pagingType": "simple",
        "pageLength": 10,
        "bLengthChange": false,
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
