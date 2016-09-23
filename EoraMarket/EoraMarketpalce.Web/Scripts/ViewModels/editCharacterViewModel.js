function applyChanges(result) {
    window.location.reload();
}

function failChanges(result) {
    $.notify({
        // options
        icon: 'glyphicon glyphicon-warning-sign',
        message: result.statusText
    }, {
        // settings
        type: 'danger',
        placement: {
            from: "top",
            align: "center"
        }
    });
}