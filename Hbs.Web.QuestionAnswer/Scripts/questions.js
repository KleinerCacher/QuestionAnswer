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
                        fileRow = $.parseHTML('<div class="ms-Grid-row"><div class="ms-Grid-col ms-u-sm1"><i class="ms-Icon ms-Icon--CheckMark" title="CheckMark" aria-hidden="true"></i></div><div class="ms-Grid-col ms-u-sm11 dt-dotdotdot" title="' + files[x].name + '">' + files[x].name + '</div></div>');
                    }
                    else {
                        fileRow = $.parseHTML('<div class="ms-Grid-row"><div class="ms-Grid-col ms-u-sm1"><i class="ms-Icon ms-Icon--Warning" title="Warning" aria-hidden="true" style="color: red;"></i></div><div class="ms-Grid-col ms-u-sm6 dt-dotdotdot" title="' + files[x].name + '">' + files[x].name + '</div><div class="ms-Grid-col ms-u-sm5">' + DtLanguageResources['GeneralLabelFileTooBig'] + '</div></div>');

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