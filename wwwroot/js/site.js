// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#testImport').on('click', () => {
    $.ajax(
        {
            url: 'api/dml/importhighlevel',
            method : 'post'
        }).done((data) => {
            let result = JSON.stringify(data);
            $('#testImportResult').text("OK");
        }).fail((jqXHR, textStatus, errorThrown) => {
            $('#testImportResult').text("Error");
        })
})

$('#testReplace').on('click', () => {
    let _id = $('#testReplaceText').val();

    $.ajax(
        {
            url: 'api/dml/replace',
            method: 'post',
            data: $.param({
                Id: _id ?? ''
            })
        }).done((data) => {
            let result = JSON.stringify(data);
            $('#testReplaceResult').text("OK");
        }).fail((jqXHR, textStatus, errorThrown) => {
            $('#testReplaceResult').text("Error or not found");
        })
})

$('#testUpdate').on('click', () => {
    let _id = $('#testUpdateDoc').val() ?? '';
    let _fieldName = $('#testUpdateField').val() ?? '';
    let _fieldValue = $('#testUpdateText').val() ?? '';


    $.ajax(
        {
            url: 'api/dml/updatefield',
            method: 'get',
            data: $.param({
                Id: _id,
                fieldName: _fieldName,
                newValue: _fieldValue               
            })
        }).done((data) => {
            let result = JSON.stringify(data);
            $('#testUpdateResult').text("OK");
        }).fail((jqXHR, textStatus, errorThrown) => {
            let x = JSON.parse(JSON.stringify(jqXHR));
            let message = x['responseJSON']['message'];

            if (message) $('#testUpdateResult').text(message);
            if (!message) $('#testUpdateResult').text("Error or not found");
        })
})

$('#testAddFieldValue').on('click', () => {
    let _id = $('#testAddFieldDoc').val() ?? '';
    let _fieldName = $('#testAddFieldName').val() ?? '';
    let _fieldValue = $('#testAddFieldText').val() ?? '';


    $.ajax(
        {
            url: 'api/dml/addfield',
            method: 'get',
            data: $.param({
                Id: _id,
                fieldName: _fieldName,
                newValue: _fieldValue
            })
        }).done((data) => {
            let result = JSON.stringify(data);
            $('#testAddFieldResult').text("OK");
        }).fail((jqXHR, textStatus, errorThrown) => {
            let x = JSON.parse(JSON.stringify(jqXHR));
            let message = x['responseJSON']['message'];

            if (message) $('#testAddFieldResult').text(message);
            if (!message) $('#testAddFieldResult').text("Error or not found");
        })
})

$('#testRemoveField').on('click', () => {
    let _id = $('#testRemoveFieldDoc').val() ?? '';
    let _fieldName = $('#testRemoveFieldName').val() ?? '';


    $.ajax(
        {
            url: 'api/dml/removefield',
            method: 'get',
            data: $.param({
                Id: _id,
                fieldName: _fieldName
            })
        }).done((data) => {
            let result = JSON.stringify(data);
            $('#testRemoveFieldResult').text("OK");
        }).fail((jqXHR, textStatus, errorThrown) => {
            let x = JSON.parse(JSON.stringify(jqXHR));
            let message = x['responseJSON']['message'];
            if (message) $('#testRemoveFieldResult').text(message);
            if (!message) $('#testRemoveFieldResult').text("Error or not found");
        })
})

$('#testDelete').on('click', () => {
    let _id = $('#testDeleteDoc').val() ?? '';


    $.ajax(
        {
            url: 'api/dml/removedoc',
            method: 'post',
            data: $.param({
                Id: _id
            })
        }).done((data) => {
            let result = JSON.stringify(data);
            $('#testRemoveDocResult').text("OK");
        }).fail((jqXHR, textStatus, errorThrown) => {
            let x = JSON.parse(JSON.stringify(jqXHR));
            let message = x['responseJSON']['message'];
            if (message) $('#testRemoveDocResult').text(message);
            if (!message) $('#testRemoveDocResult').text("Error or not found");
        })
})