﻿function GetPaymentMethod() {
    var paymentMethod = $('#paymentMethod').val();

    if (paymentMethod == 1) {
        $('#installment').attr("hidden", true);
        $('#creditCard').attr("hidden", true);
    }
    if (paymentMethod == 2) {
        $('#installment').attr("hidden", true);
        $('#creditCard').attr("hidden", true);
    }
    if (paymentMethod == 3 || paymentMethod == 4) {
        $('#creditCard').attr("hidden", false);
        $('#installment').attr("hidden", true);
    }
    if (paymentMethod == 5) {
        $('#installment').attr("hidden", false);
        $('#creditCard').attr("hidden", true);
    }
}

function CloneRow() {
    var table = document.getElementById("items");
    var row = table.rows[0];
    var clone = row.cloneNode(true);
    clone.id = Math.floor(Math.random() * 100000000);
    table.appendChild(clone);
    $('#' + clone.id + ' .amount').val(0);
    $('#' + clone.id + ' .total').val(0);
    $('#' + clone.id + ' .price').val(0);
    $('#' + clone.id + ' .discount').val(0);
    $('#' + clone.id + ' .tax').val(0);

    TableRow();
}

function deleteRow() {
    var index = $(this).closest('tr').index();
    var table = document.getElementById("items");
    table.deleteRow(index);

    TableRow();
}

function CalculateRow() {
    var index = $(this).closest('tr').index();
    var table = document.getElementById("items");
    var row = table.rows[index];
    id = row.id;

    var Price = $('#' + id + ' .price').val();
    var Amount = $('#' + id + ' .amount').val();
    var Tax = $('#' + id + ' .tax').val();
    var TaxType = $('#' + id + ' .taxtype').val();
    var Discount = $('#' + id + ' .discount').val();
    var DiscountType = $('#' + id + ' .discounttype').val();

    var RowPrice = Price * Amount;
    var Row = 0;
    var RowTotal = 0;

    if (DiscountType == 0) {
        Row = RowPrice - Discount;
    } else {
        Row = RowPrice - (RowPrice * Discount / 100);
    }

    if (TaxType == 0) {
        RowTotal = Row + ((Row / 100) * Tax);
    } else {
        RowTotal = Row;
    }

    $('#' + id + ' .total').val(RowTotal.toFixed(2));
}

function GetProduct() {
    var index = $(this).closest('tr').index();
    var table = document.getElementById("items");
    var row = table.rows[index];
    var id = row.id;

    var productId = $('#' + id + ' .product').val();

    $.ajax({
        url: "/Invoice/GetProduct",
        data: {
            "id": productId
        },
        success: function (response) {
            if (response != null) {
                $('#' + id + ' .price').val(response.price);
            }
            if (response == null) {
                $('#' + id + ' .price').val(0);
            }
        }
    })

}

function TableRow() {
    var table = document.getElementById("items");

    if (table.rows.length == 1) {
        $('.rowDelete').prop("disabled", true);
    } else {
        $('.rowDelete').prop("disabled", false);
    }
}

function CreateInvoice() {
    var invoice = {
        CustomerId: $("#CustomerId").val(),
        Description1: $("#Description").val(),
        City: $("#City").val(),
        District: $("#District").val(),
        Address: $("#Address").val(),
        TaxNo: $("#TaxNo").val(),
        TaxOffice: $("#TaxOffice").val(),
        CompanyName: $("#CompanyName").val(),
        Authorized: $("#Authorized").val(),
        City: $('#City').val(),
        District: $('#District').val(),
        Address: $('#Address').val(),
        TaxNo: $('#TaxNo').val(),
        TaxOffice: $('#TaxOffice').val(),
        CompanyName: $('#CompanyName').val(),
        Authorized: $('#Authorized').val(),
        Date: $('#Date').val(),
        WithHoldingRate: $('#tevkifatrate').val(),
        Type: $('#InvoiceType').val(),
        Invoiced: false,
    };

    if (invoice.Type == 4) {
        invoice.SalesDiscountType = $('#salesDiscountType').val();
        invoice.SalesDiscount = $('#salesDiscount').val();
    }

    console.log(invoice);

    if (invoice.CustomerId == 0 || invoice.CustomerId == null) {
        toastr.error('Lütfen Müşteri Seçiniz...');
        return false;
    }
    if (invoice.Description1 === " " || invoice.Description1 == null || invoice.Description1 === "") {
        toastr.error('Lütfen Açıklama Giriniz...');
        return false;
    }

    var items = GetAllItems();

    if (items == null) {
        return false;
    }

    $.ajax({
        url: "/Invoice/Create",
        data: {
            "invoice": invoice,
            "items": items
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

function UpdateInvoice(id) {
    var paymentMethod = $('#paymentMethod').val();
    var bank = 0;
    var installmentType = 0;
    var installmentAmount = 0;

    if (paymentMethod == 1) {

    }
    if (paymentMethod == 2) {
        //Burada Kaldım
    }
    if (paymentMethod == 3 || paymentMethod == 4) {
        bank = $('#bank').val();
    }
    if (paymentMethod == 5) {
        installmentType = $('#installmentType').val();
        installmentAmount = $('#installmentAmount').val();
    }

    var invoice = {
        Id: id,
        CustomerId: $("#CustomerId").val(),
        Description1: $("#Description").val(),
        City: $("#City").val(),
        District: $("#District").val(),
        Address: $("#Address").val(),
        TaxNo: $("#TaxNo").val(),
        TaxOffice: $("#TaxOffice").val(),
        CompanyName: $("#CompanyName").val(),
        Authorized: $("#Authorized").val(),
        City: $('#City').val(),
        District: $('#District').val(),
        Address: $('#Address').val(),
        TaxNo: $('#TaxNo').val(),
        TaxOffice: $('#TaxOffice').val(),
        CompanyName: $('#CompanyName').val(),
        Authorized: $('#Authorized').val(),
        Date: $('#Date').val(),
        WithHoldingRate: $('#tevkifatrate').val(),
        Type: $('#InvoiceType').val(),
        PaymentMethod: paymentMethod,
        Bank: bank,
        Invoiced: false,
    };

    //alert(invoice.CustomerId);

    if (invoice.CustomerId == 0 || invoice.CustomerId == null) {
        toastr.error('Lütfen Müşteri Seçiniz...');
        return false;
    }
    if (invoice.Description1 === "" || invoice.Description1 == null || invoice.Description1 === "") {
        toastr.error('Lütfen Açıklama Giriniz...');
        return false;
    }

    var items = GetAllItems();

    if (items == null) {
        return false;
    }

    $.ajax({
        url: "/Invoice/UpdateInvoiceType",
        data: {
            "invoice": invoice,
            "items": items,
            "InstallmentType": installmentType,
            "InstallmentAmount": installmentAmount,
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

function GetAllItems() {
    var list = [];

    $(".allitemlistlines").each(function () {
        var id = $(this).attr("id");

        var satir = {
            ProductId: $('#' + id + ' .product').val(),
            Price: $('#' + id + ' .price').val().replace(".", ","),
            Amount: $('#' + id + ' .amount').val(),
            Tax: $('#' + id + ' .tax').val(),
            TaxType: $('#' + id + ' .taxtype').val(),
            Discount: $('#' + id + ' .discount').val(),
            DiscountType: $('#' + id + ' .discounttype').val(),
        };

        list.push(satir);

        if (satir.Price == 0 || satir.Price == null) {
            return [];
        }
        if (satir.Amount == 0 || satir.Amount == null) {
            return [];
        }

    });

    return list;
}

function GetCustomer() {

    var CustomerId = $('#CustomerId').val();

    $.ajax({
        url: "/Invoice/GetCustomer",
        data: {
            "id": CustomerId
        },

        success: function (response) {
            if (response != null) {
                $('#City').val(response.city);
                $('#District').val(response.district);
                $('#Address').val(response.address);
                $('#TaxNo').val(response.taxNo);
                $('#TaxOffice').val(response.taxOffice);
                $('#CompanyName').val(response.companyName);
                $('#Authorized').val(response.authorized);


                if ($('#InvoiceType').val() == 4) {
                    CheckCustomerEInvoice(response.vKN);
                }
            }
        }
    })


}

function CalculateAll() {
    var SubTotal = 0;
    var TotalVat = 0;
    var TotalDiscount = 0;
    var Total = 0;
    var Tevkifat = 0;

    var TevkifatRate = $('#tevkifatrate').val();

    var SalesDiscountType = $('#salesDiscountType').val();
    var SalesDiscount = $('#salesDiscount').val();
    var InvoiceType = $('#InvoiceType').val();

    $(".allitemlistlines").each(function () {
        var id = $(this).attr("id");

        var Price = $('#' + id + ' .price').val();
        var Amount = $('#' + id + ' .amount').val();
        var Tax = $('#' + id + ' .tax').val();
        var TaxType = $('#' + id + ' .taxtype').val();
        var Discount = $('#' + id + ' .discount').val();
        var DiscountType = $('#' + id + ' .discounttype').val();

        var RowPrice = Price * Amount;
        var RowTax = 0;
        var RowTotal = 0;
        var RowDiscount = 0;

        if (DiscountType == 0) {
            RowDiscount = parseInt(Discount);
            RowTotal = RowPrice - RowDiscount;
        } else {
            RowDiscount = RowPrice * Discount / 100;
            RowTotal = RowPrice - RowDiscount;
        }

        if (TaxType == 0) {
            RowTax = (RowTotal / 100) * Tax;
            RowTotal += RowTax;
        } else {
            RowTax = (RowTotal / 100) * Tax;
            RowPrice -= RowTax;
        }

        SubTotal += RowPrice;
        TotalVat += RowTax;
        Total += RowTotal;
        TotalDiscount += RowDiscount;

    });

    if (InvoiceType == 4) {

        switch (SalesDiscountType) {
            case "0":
                var PercentSubTotal = SubTotal * SalesDiscount / 100;
                SubTotal -= PercentSubTotal;
                TotalDiscount += PercentSubTotal;
                break;
            case "1":
                var PercentTotal = Total * SalesDiscount / 100;
                Total -= Total * PercentTotal;
                TotalDiscount += PercentTotal;
                break;
            case "2":
                var TLSubTotal = SalesDiscount;
                TLSubTotal = parseInt(TLSubTotal);
                SubTotal -= TLSubTotal;
                TotalDiscount += TLSubTotal;
                break;
            case "3":
                var TLTotal = SalesDiscount;
                TLTotal = parseInt(TLTotal);
                Total -= TLTotal;
                TotalDiscount += TLTotal;
                break;
        }
    }

    if (TevkifatRate != 0) {
        Tevkifat = TotalVat * (TevkifatRate / 10);
    }
    Total -= Tevkifat;

    if (SubTotal != 0) {
        $('#subTotal').html(SubTotal.toFixed(2));
    } else {
        $('#subTotal').html(0);
    }

    if (TotalVat != 0) {
        $('#tax').html(TotalVat.toFixed(2));
    } else {
        $('#tax').html(0);
    }

    if (TotalDiscount != 0) {
        $('#discount').html(TotalDiscount.toFixed(2));
    } else {
        $('#discount').html(0);
    }

    if (Total != 0) {
        $('#total').html(Total.toFixed(2));
    } else {
        $('#total').html(0);
    }

    if (Tevkifat != 0) {
        $('#tevkifat').html(Tevkifat.toFixed(2));
    } else {
        $('#tevkifat').html(0);
    }
}

function SetTevkifatRate(tevkifat) {
    $('#tevkifatrate option[value=' + tevkifat + ']').attr('selected', true);
}

function SetCustomer(id) {
    $('#CustomerId option[value=' + id + ']').attr('selected', true);
}

function GetTransactions(invoiceId) {
    $.ajax({
        url: "/Invoice/GetTransactions",
        data: {
            "InvoiceId": invoiceId
        },

        success: function (response) {
            if (response != null) {
                response.forEach(SetTransaction);

                var table = document.getElementById("items");
                table.deleteRow(0);
                TableRow();
                CalculateAll();
            }
        }

    })
}

function SetTransaction(item) {
    var table = document.getElementById("items");
    var row = table.rows[0];
    var clone = row.cloneNode(true);
    clone.id = Math.floor(Math.random() * 100000000);
    table.appendChild(clone);

    $('#' + clone.id + ' .product option[value=' + item.product.id + ']').attr('selected', true);

    $('#' + clone.id + ' .tax option[value=' + item.tax + ']').attr('selected', true);
    $('#' + clone.id + ' .taxtype option[value=' + item.taxType + ']').attr('selected', true);
    $('#' + clone.id + ' .discounttype option[value=' + item.discountType + ']').attr('selected', true);

    $('#' + clone.id + ' .amount').val(item.amount);
    $('#' + clone.id + ' .price').val(item.product.price);
    $('#' + clone.id + ' .discount').val(item.discount);
    $('#' + clone.id + ' .tax').val(item.tax);
}

function Refund(RefundInvoiceId) {
    var invoice = {
        CustomerId: $("#CustomerId").val(),
        Description1: $("#Description").val(),
        City: $("#City").val(),
        District: $("#District").val(),
        Address: $("#Address").val(),
        TaxNo: $("#TaxNo").val(),
        TaxOffice: $("#TaxOffice").val(),
        CompanyName: $("#CompanyName").val(),
        Authorized: $("#Authorized").val(),
        City: $('#City').val(),
        District: $('#District').val(),
        Address: $('#Address').val(),
        TaxNo: $('#TaxNo').val(),
        TaxOffice: $('#TaxOffice').val(),
        CompanyName: $('#CompanyName').val(),
        Authorized: $('#Authorized').val(),
        Date: $('#Date').val(),
        WithHoldingRate: $('#tevkifatrate').val(),
        Type: $('#InvoiceType').val(),
        Invoiced: false,
        IsRefund: true,
        RefundInvoiceId: RefundInvoiceId,
    };

    if (invoice.CustomerId == 0 || invoice.CustomerId == null) {
        toastr.error('Lütfen Müşteri Seçiniz...');
        return false;
    }
    if (invoice.Description1 === " " || invoice.Description1 == null || invoice.Description1 === "") {
        toastr.error('Lütfen Açıklama Giriniz...');
        return false;
    }

    var items = GetAllItems();

    if (items == null) {
        return false;
    }

    $.ajax({
        url: "/Invoice/Create",
        data: {
            "invoice": invoice,
            "items": items
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

function CheckCustomerEInvoice(CustomerId) {

    $.ajax({
        url: "/Invoice/CheckCustomer",
        data: {
            "CustomerId": CustomerId
        },

        success: function (response) {
            if (response) {
                $('#einvoiceArea').attr('hidden', false);
                console.log("response true", response);
            }
            else {
                console.log("response false", response);
            }
        },

        error: function (response) {
            toastr.error("E-fatura Müşteri Kontrolünde Bir Hata Oluştu!");
            console.log("responseERROR", response);

        }

    })
}
