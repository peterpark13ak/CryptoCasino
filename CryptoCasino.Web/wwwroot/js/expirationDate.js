$(function () {
    const $commentForm = $('#addCard');

    $commentForm.on('submit', function (event) {
        event.preventDefault();

        const date = $('input[name="ExpirationDate"').val();
        const dateSplitted = date.split('/');
        const expirationDate = new Date(dateSplitted[1].trim() + '-' + dateSplitted[0].trim() + '-01');
        console.log(expirationDate);
        if (expirationDate < new Date() || expirationDate == 'Invalid Date') {
            console.log('Not passing!');
            const spanValidation = $('*[data-valmsg-for="ExpirationDate"]');
            spanValidation.addClass('field-validation-error');
            spanValidation.removeClass('field-validation-valid');
            spanValidation.html("Invalid date");

        }
        else {
            console.log('Passing');
            $(this).off("submit");
            $(this).submit();
        }
    });

});