(() => {
    let rangos = [];
    //getValues();
    let btnMed = document.getElementById("btnMed");
    let rango = document.getElementById("rango");
     getRandomPremio();


    $("#rango").change((e) => {
        getRandomPremio();
    });
    function getRandomPremio() {
        btnMed.setAttribute("disabled", "true");
        rango.setAttribute("disabled", "true");
        $.get(`/api/Fill/GetRandomPremio/?rangeId=${$("#rango").val()}`)
            .done((data) => {
                getValues(data);
                
            }).fail((err) => {
                console.log(err);
            });
    }
    function loopACoupleTimes(data) {
        for (var i = 0; i < 10; i++) {
            $("#idPremio").val(data.idPremio);
            $("#resIdPremio").html(data.valor);
        }
    }
    function getValues(data) {
        let rango = $("#rango option:selected").text().split('-');
        console.log(rango);
        let inicio = rango[0].trim(), fin = rango[1].trim();
        let iter = giveValues(inicio, fin);
        giveValues(inicio, fin,data);

    }
    function giveValues(inicio, fin,data) {
        let isANumber = new RegExp("^\\d+$");
        if (isANumber.test(inicio) && isANumber.test(fin)) {
            animateValue("resIdPremio", parseInt(inicio), parseInt(fin), 200, data, true, returnData);
        }
        else {
            animateValue("resIdPremio", inicio.charCodeAt(0), fin.charCodeAt(0), 200, data, false, returnData);

        }
    }
    function returnData(dt) {
        $("#idPremio").val(dt.idPremio);
        $("#resIdPremio").html(dt.valor);
        rango.removeAttribute("disabled");
        btnMed.removeAttribute("disabled");
    }
    function animateValue(id, start, end, duration, data, num,callback) {
        var range = end - start;
        var current = start;
        var increment = end > start ? 1 : -1;
        var stepTime = Math.abs(Math.floor(duration / range));
        var obj = document.getElementById(id);
        var timer = setInterval(function () {
            current += increment;
            obj.innerHTML = (num) ? current : String.fromCharCode(current);
            if (current == end) {
                clearInterval(timer);
                callback(data);
            }
        }, stepTime);
    }
})();