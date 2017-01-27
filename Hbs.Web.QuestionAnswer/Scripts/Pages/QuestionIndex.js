function QuestionIndex() {
    this.myprojects = true;
    this.projectstatus = '';
    this.projectfilter = '';

    this.attachFilterInput = function () {
        var me = this;
        $('#filterbox').keyup(function () {
            me.questionfilter = $(this).val().toLowerCase();
            me.showFilteredProjects();
        });
    }

    this.showFilteredProjects = function () {
        var me = this;
        $('.question-container').each(function () {
            $(this).hide();
            if ($(this).attr('data-question').indexOf(me.questionfilter) > -1 || $(this).attr('data-questiontext').indexOf(me.questionfilter) > -1) {
                        $(this).show();
                   
            }
        });
    }
}

$(document).ready(function () {
    var obj = new QuestionIndex();
    obj.attachFilterInput();
});