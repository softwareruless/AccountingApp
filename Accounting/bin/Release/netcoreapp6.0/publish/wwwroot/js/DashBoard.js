function getReports() {

    $.ajax({
        url: "/Home/DailyReports",

        success: function (result) {
            if (result != null) {

                gunlukSatisChart(result.dailySales);
                gunlukAlisChart(result.dailyPurchases);
                gunlukTahsilatChart(result.dailyCollection);
                gunlukOdemeChart(result.dailyPayment);
            }
        },

    })

    $.ajax({
        url: "/Home/SevenDaysReports",

        success: function (result) {
            console.log(result);
            if (result != null) {
                yediGunlukSatis(result.dailySales.dailySales);
                yediGunlukVade(result.dailyExpiryProcess);
            }
        },

    })

    $.ajax({
        url: "/Home/SevenDailyBankProcess",

        success: function (result) {
            if (result != null) {
                yediGunlukBanka(result);
            }
        },

    })

    $.ajax({
        url: "/Home/SevenDailySafeProcess",

        success: function (result) {
            if (result != null) {
                yediGunlukKasa(result);
            }
        },

    })

    $.ajax({
        url: "/Home/Balances",

        success: function (result) {
            if (result != null) {
                getBanksBalances(result.bankBalances);
                getSafesBalances(result.safeBalances);
            }
        },

    })
}


function gunlukSatisChart(dailySales) {
    var donutChartCanvas = $('#gunlukSatisChart').get(0).getContext('2d');
    if (dailySales.cash === 0 && dailySales.creditCard === 0 && dailySales.openAccount === 0) {
        $('#gunlukSatisP').attr("hidden", false);
    }
    var donutData = {
        labels: [
            'Nakit',
            'Kredi Kartı',
            'Açık Hesap',
        ],
        datasets: [
            {
                data: [dailySales.cash, dailySales.creditCard, dailySales.openAccount],
                backgroundColor: ['#5DADE2', '#FF5733', '#DAF7A6'],
            }
        ]
    }
    var donutOptions = {
        maintainAspectRatio: false,
        responsive: true,
    }

    new Chart(donutChartCanvas, {
        type: 'doughnut',
        data: donutData,
        options: donutOptions
    })
}

function gunlukAlisChart(dailyPurchases) {
    var donutChartCanvas = $('#gunlukAlisChart').get(0).getContext('2d');
    if (dailyPurchases.cash === 0 && dailyPurchases.creditCard === 0 && dailyPurchases.openAccount === 0) {
        $('#gunlukAlisP').attr("hidden", false);
    }
    var donutData = {
        labels: [
            'Nakit',
            'Kredi Kartı',
            'Açık Hesap',
        ],
        datasets: [
            {
                data: [dailyPurchases.cash, dailyPurchases.creditCard, dailyPurchases.openAccount],
                backgroundColor: ['#5DADE2', '#FF5733', '#DAF7A6'],
            }
        ]
    }
    var donutOptions = {
        maintainAspectRatio: false,
        responsive: true,
    }

    new Chart(donutChartCanvas, {
        type: 'doughnut',
        data: donutData,
        options: donutOptions
    })
}


function gunlukTahsilatChart(dailyCollection) {
    var donutChartCanvas = $('#gunlukTahsilatChart').get(0).getContext('2d');
    if (dailyCollection.cash === 0 && dailyCollection.creditCard === 0 && dailyCollection.check === 0) {
        $('#gunlukTahsilatP').attr("hidden", false);
    }
    var donutData = {
        labels: [
            'Nakit',
            'Kredi Kartı',
            'Çek',
        ],
        datasets: [
            {
                data: [dailyCollection.cash, dailyCollection.creditCard, dailyCollection.check],
                backgroundColor: ['#5DADE2', '#FF5733', '#DAF7A6'],
            }
        ]
    }
    var donutOptions = {
        maintainAspectRatio: false,
        responsive: true,
    }

    new Chart(donutChartCanvas, {
        type: 'doughnut',
        data: donutData,
        options: donutOptions
    })
}

function gunlukOdemeChart(dailyPayment) {
    var donutChartCanvas = $('#gunlukOdemeChart').get(0).getContext('2d');
    if (dailyPayment.cash === 0 && dailyPayment.creditCard === 0 && dailyPayment.check === 0) {
        $('#gunlukOdemeP').attr("hidden", false);
    }
    var donutData = {
        labels: [
            'Nakit',
            'Kredi Kartı',
            'Çek',
        ],
        datasets: [
            {
                data: [dailyPayment.cash, dailyPayment.creditCard, dailyPayment.check],
                backgroundColor: ['#5DADE2', '#FF5733', '#DAF7A6'],
            }
        ]
    }
    var donutOptions = {
        maintainAspectRatio: false,
        responsive: true,
    }

    new Chart(donutChartCanvas, {
        type: 'doughnut',
        data: donutData,
        options: donutOptions
    })
}


function yediGunlukSatis(yediGunlukSatis) {
    var areaChartCanvas = $('#yediGunlukSatis').get(0).getContext('2d')

    var Daylist = $.map(yediGunlukSatis, function (v) {
        return v.date;
    });
    var Datalist = $.map(yediGunlukSatis, function (v) {
        return v.amount;
    });

    var areaChartData = {

        labels: Daylist,
        datasets: [
            {
                label: 'Günlük Satışlar',
                backgroundColor: 'rgba(60,141,188,0.9)',
                borderColor: 'rgba(60,141,188,0.8)',
                pointRadius: false,
                pointColor: '#3b8bba',
                pointStrokeColor: 'rgba(60,141,188,1)',
                pointHighlightFill: '#3b8bba',
                pointHighlightStroke: 'rgba(60,141,188,1)',
                data: Datalist
            },
        ]
    }

    var areaChartOptions = {
        maintainAspectRatio: false,
        responsive: true,
        legend: {
            display: false
        },
        scales: {
            xAxes: [{
                gridLines: {
                    display: false,
                }
            }],
            yAxes: [{
                gridLines: {
                    display: false,
                }
            }]
        }
    }

    // This will get the first returned node in the jQuery collection.
    new Chart(areaChartCanvas, {
        type: 'line',
        data: areaChartData,
        options: areaChartOptions
    })
}

function yediGunlukVade(yediGunlukVade) {
    var areaChartCanvas = $('#yediGunlukVade').get(0).getContext('2d')

    var In = yediGunlukVade.dailyCollections;
    var Out = yediGunlukVade.dailyPayments;
    var Daylist = $.map(yediGunlukVade.dailyCollections, function (v) {
        return v.date;
    });
    var DatalistIn = $.map(In, function (v) {
        return v.amount;
    });
    var DatalistOut = $.map(Out, function (v) {
        return v.amount;
    });

    var areaChartData = {

        labels: Daylist,
        datasets: [
            {
                label: 'Giriş',
                backgroundColor: 'rgba(60,141,188,0.9)',
                borderColor: 'rgba(60,141,188,0.8)',
                pointRadius: false,
                pointColor: '#3b8bba',
                pointStrokeColor: 'rgba(60,141,188,1)',
                pointHighlightFill: '#3b8bba',
                pointHighlightStroke: 'rgba(60,141,188,1)',
                data: DatalistIn
            },
            {
                label: 'Çıkış',
                backgroundColor: 'rgba(210, 214, 222, 1)',
                borderColor: 'rgba(210, 214, 222, 1)',
                pointRadius: false,
                pointColor: 'rgba(210, 214, 222, 1)',
                pointStrokeColor: '#c1c7d1',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(220,220,220,1)',
                data: DatalistOut
            },
        ]
    }

    var areaChartOptions = {
        maintainAspectRatio: false,
        responsive: true,
        legend: {
            display: false
        },
        scales: {
            xAxes: [{
                gridLines: {
                    display: false,
                }
            }],
            yAxes: [{
                gridLines: {
                    display: false,
                }
            }]
        }
    }

    // This will get the first returned node in the jQuery collection.
    new Chart(areaChartCanvas, {
        type: 'line',
        data: areaChartData,
        options: areaChartOptions
    })
}


function yediGunlukBanka(yediGunlukBanka) {
    var areaChartCanvas = $('#yediGunlukBanka').get(0).getContext('2d')

    var In = yediGunlukBanka.in;
    var Out = yediGunlukBanka.out;
    var Daylist = $.map(yediGunlukBanka.in, function (v) {
        return v.date;
    });
    var DatalistIn = $.map(In, function (v) {
        return v.amount;
    });
    var DatalistOut = $.map(Out, function (v) {
        return v.amount;
    });

    var areaChartData = {

        labels: Daylist,
        datasets: [
            {
                label: 'Giriş',
                backgroundColor: 'rgba(60,141,188,0.9)',
                borderColor: 'rgba(60,141,188,0.8)',
                pointRadius: false,
                pointColor: '#3b8bba',
                pointStrokeColor: 'rgba(60,141,188,1)',
                pointHighlightFill: '#3b8bba',
                pointHighlightStroke: 'rgba(60,141,188,1)',
                data: DatalistIn
            },
            {
                label: 'Çıkış',
                backgroundColor: 'rgba(210, 214, 222, 1)',
                borderColor: 'rgba(210, 214, 222, 1)',
                pointRadius: false,
                pointColor: 'rgba(210, 214, 222, 1)',
                pointStrokeColor: '#c1c7d1',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(220,220,220,1)',
                data: DatalistOut
            },
        ]
    }

    var areaChartOptions = {
        maintainAspectRatio: false,
        responsive: true,
        legend: {
            display: false
        },
        scales: {
            xAxes: [{
                gridLines: {
                    display: false,
                }
            }],
            yAxes: [{
                gridLines: {
                    display: false,
                }
            }]
        }
    }

    // This will get the first returned node in the jQuery collection.
    new Chart(areaChartCanvas, {
        type: 'line',
        data: areaChartData,
        options: areaChartOptions
    })
}

function yediGunlukKasa(yediGunlukKasa) {
    var areaChartCanvas = $('#yediGunlukKasa').get(0).getContext('2d')

    var In = yediGunlukKasa.in;
    var Out = yediGunlukKasa.out;
    var Daylist = $.map(yediGunlukKasa.in, function (v) {
        return v.date;
    });
    var DatalistIn = $.map(In, function (v) {
        return v.amount;
    });
    var DatalistOut = $.map(Out, function (v) {
        return v.amount;
    });

    var areaChartData = {

        labels: Daylist,
        datasets: [
            {
                label: 'Giriş',
                backgroundColor: 'rgba(60,141,188,0.9)',
                borderColor: 'rgba(60,141,188,0.8)',
                pointRadius: false,
                pointColor: '#3b8bba',
                pointStrokeColor: 'rgba(60,141,188,1)',
                pointHighlightFill: '#3b8bba',
                pointHighlightStroke: 'rgba(60,141,188,1)',
                data: DatalistIn
            },
            {
                label: 'Çıkış',
                backgroundColor: 'rgba(210, 214, 222, 1)',
                borderColor: 'rgba(210, 214, 222, 1)',
                pointRadius: false,
                pointColor: 'rgba(210, 214, 222, 1)',
                pointStrokeColor: '#c1c7d1',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(220,220,220,1)',
                data: DatalistOut
            },
        ]
    }

    var areaChartOptions = {
        maintainAspectRatio: false,
        responsive: true,
        legend: {
            display: false
        },
        scales: {
            xAxes: [{
                gridLines: {
                    display: false,
                }
            }],
            yAxes: [{
                gridLines: {
                    display: false,
                }
            }]
        }
    }

    // This will get the first returned node in the jQuery collection.
    new Chart(areaChartCanvas, {
        type: 'line',
        data: areaChartData,
        options: areaChartOptions
    })
}


function getBanksBalances(bankBalances) {
    bankBalances.forEach(SetBankBalances);

    var table = document.getElementById("banks");
    table.deleteRow(0);
}

function SetBankBalances(item) {
    var table = document.getElementById("banks");
    var row = table.rows[0];
    var clone = row.cloneNode(true);
    clone.id = Math.floor(Math.random() * 100000000);
    table.appendChild(clone);

    $('#' + clone.id + ' .bank').html(item.name);
    $('#' + clone.id + ' .account').html(item.accountNo);
    $('#' + clone.id + ' .amount').html(item.balance + ' ₺');
}

function getSafesBalances(safeBalances) {
    safeBalances.forEach(SetSafeBalances);

    var table = document.getElementById("safes");
    table.deleteRow(0);
}

function SetSafeBalances(item) {
    var table = document.getElementById("safes");
    var row = table.rows[0];
    var clone = row.cloneNode(true);
    clone.id = Math.floor(Math.random() * 100000000);
    table.appendChild(clone);

    $('#' + clone.id + ' .bank').html(item.name);
    $('#' + clone.id + ' .amount').html(item.balance + ' ₺');
}
