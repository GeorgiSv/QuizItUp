var AnswersInputModelIndex = 2;

function RemoveAnswer() {

    let tag = "#A" + AnswersInputModelIndex;
    console.log(tag);

    AnswersInputModelIndex--;
    if (AnswersInputModelIndex < 2) {
        alert("Question should contain at least two answers");
        AnswersInputModelIndex++;
        return;
    }
    $("#A" + AnswersInputModelIndex).remove();
}

function AddMoreAnswers() {

    if (AnswersInputModelIndex >= 5) {
        alert("Max 5 questions permited!");
        return;
    }

    $("#AnswersContainer").
        append("<div id='A" + AnswersInputModelIndex + "' class='input-group mb-3'><div class= 'input-group-prepend'><div class='input-group-text'><input type='checkbox' id='checkBoxAnswer' value='true' name='AnswersInputModel[" + AnswersInputModelIndex + "].IsCorrectAnswer' aria-label='Checkbox' value='true'></div></div ><input type='text' class='form-control' aria-label='Text' placeholder='Answer ..' name='AnswersInputModel[" + AnswersInputModelIndex + "].AnswerText'> </div>");
    AnswersInputModelIndex++;
}

function validateQuestion() {
    let inputs = document.getElementsByTagName("input");

    let counterofCorrectAnswers = 0;
    for (var input of inputs) {
        if (input.className == "form-control" || input.className == "form-control valid") {

            if (input.value.length < 1 || input.value == " ") {
                alert("Please fill all empty answers!")
                return false;
            }
        }

        if (input.id == "checkBoxAnswer" || input.id == "Answer_AnswerText") {
            if (input.checked == true) {
                counterofCorrectAnswers++;
            }
        }
    }

    if (counterofCorrectAnswers > 1) {
        alert("Please select only one correct asnwer!")
        return false;
    }
    if (counterofCorrectAnswers == 0) {
        alert("Please select one correct asnwer!")
        return false;
    }
    

    return true;
}