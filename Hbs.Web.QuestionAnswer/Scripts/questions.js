$(function () {
    var pictures = document.querySelectorAll('.dt-image-to-dialog');
    if (pictures) {
        $.each(pictures, function (index, picture) {
            $(picture).click(function () {
                var dialogElement = document.querySelector('#pictureDialog');
                dialogElement.querySelector('#pictureElement').src = this.src;
                $('#pictureDialog').modal('show');
            });
        });
    }
});

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

$(function () {
    var fileInputs = $('input[type="file"]');
    $.each(fileInputs, function (index, fileInput) {
        $(fileInput).on('change', function (event) {
            var files = event.currentTarget.files;
            var error = false;

            if (files.length > 0) {
                $('#selectedFiles').html('');

                for (var x = 0; x < files.length; x++) {
                    var filesize = ((files[x].size / 1024) / 1024).toFixed(4); // MB
                    var fileRow = "";

                    if (filesize <= 4) { //default MVC settings -> 4MB
                        fileRow = $.parseHTML('<div class="col-lg-1"><i class="glyphicon glyphicon-ok" title="CheckMark" aria-hidden="true"></i></div><div class="col-lg-11 dt-dotdotdot" title="' + files[x].name + '">' + files[x].name + '</div>');
                    }
                    else {
                        fileRow = $.parseHTML('<div class="col-lg-1"><i class="glyphicon glyphicon-remove" title="Warning" aria-hidden="true" style="color: red;"></i></div><div class="col-lg-3 dt-dotdotdot" title="' + files[x].name + '">' + files[x].name + '</div><div class=col-lg-8">' + 'Die Datei ist zu groß' + '</div>');
                        error = true;
                    }
                    $('#selectedFiles').append(fileRow);
                }
                if (error) {
                    $(fileInput).val('');
                }
            }
        });
    });
});

function deleteAttachment(id) {
    $('#attrowid' + id).hide();
    $('#attrdeleteid' + id).val(true);
}