function QuestionIndex() {
    this.filterbutton = 'all';

    this.attachFilterInput = function () {
        var me = this;
        $('#filterbox').keyup(function () {
            me.questionfilter = $(this).val().toLowerCase();
            me.showFilteredProjects();
        });

        $('#index-filter-button-all').click(function () {
            me.filterbutton = 'all';
            me.showFilteredProjects();
        });

        $('#index-filter-button-solved').click(function () {
            me.filterbutton = 'solved';
            me.showFilteredProjects();
        });

        $('#index-filter-button-open').click(function () {
            me.filterbutton = 'open';
            me.showFilteredProjects();
        });
    }

    this.showFilteredProjects = function () {
        var me = this;
        $('.question-container').each(function () {
            $(this).hide();
            if ($(this).attr('data-question').indexOf(me.questionfilter) > -1
                || $(this).attr('data-questiontext').indexOf(me.questionfilter) > -1
                || $('#filterbox').val().length == 0) {

                if (me.filterbutton == 'all') {
                    $(this).show();
                }
                else if(me.filterbutton == 'solved' && $(this).attr('data-solved') == 'True')
                {
                    $(this).show();
                }
                else if (me.filterbutton == 'open' && $(this).attr('data-solved') == 'False') {
                    $(this).show();
                }
                          
            }
        });
    }
}

$(document).ready(function () {
    var obj = new QuestionIndex();
    obj.attachFilterInput();
});