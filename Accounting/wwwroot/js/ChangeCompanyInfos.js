function Duzenle() {
    var input = $('#Name').attr('readonly');
    console.log(input);
    if (input === 'readonly') {
        RemoveReadOnly();
        $('#editButton').html('Kaydet');
        $('#editButton').attr("onclick", "Kaydet()");
    } else {
        MakeReadOnly();
    }
}

function RemoveReadOnly() {
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].removeAttribute("readonly");
    }
}

function MakeReadOnly() {
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].setAttribute("readonly", true);
    }
}

function Kaydet() {
    var user = {
        Name: $("#Name").val(),
        Surname: $("#Surname").val(),
        CompanyName: $("#CompanyName").val(),
        Authorized: $("#Authorized").val(),
        City: $("#City").val(),
        District: $("#District").val(),
        PostCode: $("#PostCode").val(),
        Address: $("#Address").val(),
        Email: $("#Email").val(),
        Phone: $("#Phone").val(),
        MersisNo: $("#MersisNo").val(),
        TaxNo: $("#TaxNo").val(),
        TaxOffice: $("#TaxOffice").val(),
        TcNo: $("#TcNo").val(),
        WebSite: $("#WebSite").val(),
    };

    $.ajax({
        url: "/CompanyInfo/Update",
        data: {
            "user": user
        },
        type: "POST",

        success: function (response) {
            if (response.status == true) {

                toastr.success(response.message);

                setTimeout(function () {
                    MakeReadOnly();
                    $('#editButton').html('Düzenle');
                    $('#editButton').attr("onclick", "Duzenle()");
                }, 700);
            }
            else {
                toastr.error(response.message);
            }
        },
        error: function (response) {
            toastr.error("Hata!!!");
            console.log(response);
            MakeReadOnly();
            $('#editButton').html('Düzenle');
            $('#editButton').attr("onclick", "Duzenle()");
        }
    })
}