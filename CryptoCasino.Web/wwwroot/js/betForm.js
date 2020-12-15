$(function () {

    $('#betForm').on('submit', function (event) {
        event.preventDefault();
        const $withdrawForm = $('#betForm');
        let value = +($withdrawForm.find('input[name="BetAmount"]').val());
        let balance = +($('#balanceSpan').html());
        if (value > balance || value < 1) {
            const spanValidation = $withdrawForm.find('*[data-valmsg-for="BetAmount"]');
            spanValidation.addClass('field-validation-error');
            spanValidation.removeClass('field-validation-valid');
            spanValidation.html("Insufficient funds");

        }
        else {

            const data = $(this).serialize();
            const url = $(this).attr("action");
            const posting = $.post(url, data);
            let newBalance = balance - value;
            $('#balanceSpan').html(Math.round(newBalance * 100) / 100);

            posting.done(function (data) {
                $('#gameBoard').html(data);
                let winAmount = +($('#winningAmount').html());
                newBalance = +($('#balanceSpan').html()) + winAmount;
                $('#balanceSpan').html(Math.round(newBalance*100)/100);
                console.log(data);
            });
    }

    });
});