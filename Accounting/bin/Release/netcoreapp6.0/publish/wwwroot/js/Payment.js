function GetPaymentMethod() {
    var paymentMethod = $('#paymentMethod').val();

    if (paymentMethod == 1 || paymentMethod == 2) {
        $('#banks').attr("hidden", false);
        $('#creditCard').attr("hidden", true);
    }
    if (paymentMethod == 3 || paymentMethod == 4) {
        $('#creditCard').attr("hidden", false);
        $('#banks').attr("hidden", true);
    }
}

function Create() {
    var payment = {
        CustomerId: $('#CustomerId').val(),
        BankId: $('#bank').val(),
        SafesId: $('#safe').val(),
        Amount: $('#amount').val(),
        Description: $('#description').val(),
        Date: $('#ProcessDate').val(),
        Type: $('#Type').val(),
    }

    var InvoiceId = $('#InvoiceId').val();
    var InstallmentId = $('#InstallmentId').val();
    var CheckId = $('#CheckId').val();

    if (InvoiceId != null) {
        payment.InvoiceId = InvoiceId;
    }
    if (InstallmentId != null) {
        payment.InstallmentId = InstallmentId;
    }
    if (CheckId != null) {
        payment.CheckId = CheckId;
    }

    $.ajax({
        url: "/Payment/Create",
        data: {
            "payment": payment
        },
        type: "POST",

        success: function (response) {
            if (response.status == true) {

                toastr.success(response.message);

                setTimeout(function () {

                }, 700);
            }
            else {
                toastr.error(response.message);
            }
        }
    })


}