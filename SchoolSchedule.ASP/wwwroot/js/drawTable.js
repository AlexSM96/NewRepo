function drawTable(parameters) {
    let url = parameters.url;
    let className = parameters.data;

    let scheduleTable = $('#scheduleTable').DataTable({
        info: false,
        serverSide: true,
        searching: false,
        paging: true,
        sorting: false,
        ajax: {
            method: 'POST',
            url: url,
            data: { className }
        },
        columns: [
            { data: 'dayWeek' },
            { data: 'className' },
            { data: 'lessonNumber' },
            { data: 'lessonTimeStart' },
            { data: 'lessonTimeEnd' },
            { data: 'lessonName' },
            { data: 'teacherName' },
            { data: 'classRoom' }
        ],
        render: function (nRow, data) {
            for (var i = 0; i < scheduleTable.columns().header().length - 1; i++) {
                $('td', nRow).eq(i).css('cursor', 'pointer');
                $('td', nRow).eq(i).on('click', null);
            }
        }
    });
}