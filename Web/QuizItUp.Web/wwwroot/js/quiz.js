$(document).ready(function () {
    var startBtn = document.getElementById('start');

    var questionsCount = 0;
    var counter = 1;

    if (startBtn) {
        var forms = document.getElementsByTagName('form');
        var form;
        if (forms.length > 1) {
            form = forms[1]
        } else {
            form = forms[0]
        }
        questionsCount = parseInt(form.id);
        var nextBtns = Array.from(document.getElementsByTagName('a')).filter(x => x.id.includes('next'));
        var prevBtns = Array.from(document.getElementsByTagName('a')).filter(x => x.id.includes('prev'));
        $(nextBtns).click(loadNextQuestion);
        $(prevBtns).click(loadPreviousQuestion);
        startQuizEventHandler()
    }

    function startQuizEventHandler() {
        $(startBtn).click(function () {
            $(window).bind('beforeunload', function () {
                return 'Are you sure you want to leave?';
            });

            $('#submitResult').click(function () {
                $(window).unbind('beforeunload');
            });
            
            $('#pagging').show();
            $('#submit').show();
            $('#details').hide();
            showQuestion(counter);

            var pages = [...document.getElementsByClassName('page-item number')];
            pages.forEach(x => $(x).click(loadQuestion));
            $('#first').click(loadPreviousQuestion);
            $('#last').click(loadNextQuestion);
        })
    }

    function loadQuestion(e) {
        e.preventDefault();
        hideQuestion(counter)
        counter = parseInt(e.currentTarget.classList[e.currentTarget.classList.length - 1]);
        showQuestion(counter);
    }

    function loadNextQuestion(e) {
        e.preventDefault();
        hideQuestion(counter)
        if (counter == questionsCount) {
            showQuestion(counter);
        } else {
            showQuestion(counter + 1);
        }

        if (counter < questionsCount) {
            counter++;
        }
    }

    function loadPreviousQuestion(e) {
        e.preventDefault();
        hideQuestion(counter);
        if (counter == 1) {
            showQuestion(counter);
        } else {
            showQuestion(counter - 1)
        }

        if (counter > 1) {
            counter--;
        }
    }

    function showQuestion(counter) {
        $(`#${counter}`).show();
        if (counter == 1) {
            $('#first').addClass('disabled');
        } else if (counter == questionsCount) {
            $('#last').addClass('disabled');
        }
        else {
            $('#first').removeClass('disabled');
            $('#last').removeClass('disabled');
        }
        $('.number').removeClass('active');
        $(`.${counter}`).addClass('active');
    }

    function hideQuestion(counter) {
        $(`#${counter}`).hide();
    }
})