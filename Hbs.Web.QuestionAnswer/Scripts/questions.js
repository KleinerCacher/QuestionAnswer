$('form').submit(function (e) {
    alert('fdgsdfgsfdgdsfg');
});

$('form').submit(function (e) {
    if ($('input:file', this).length > 0) {
        var filesUploading = $('input:file', this)[0].files.length;
        var filesExists = $('.singlefile:visible').length;

        if (filesUploading + filesExists > 5) {
            e.preventDefault();
            $('#maxfileserr').show();
        }
        else {
            $('#maxfileserr').hide();
            $('input:disabled').attr('readonly', 'true')
            $('input:disabled').removeAttr('disabled');
        }
    }
});