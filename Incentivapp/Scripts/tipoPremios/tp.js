﻿(() => {
    let idTipoPremio = "";
    $(document).on("click",".delTp",(e) => {
        e.preventDefault();
        idTipoPremio = e.currentTarget.getAttribute("data-id");
        console.log(idTipoPremio);
        $("#dlSpan").html(`<b>${idTipoPremio}</b>`);
        $("#delModal").modal();
    });
    $("#delBtn").click((e) => {
        $("#delModal").modal("hide");
        location.href = `/TipoPremios/Delete/${idTipoPremio}`;
    });
    let search = document.getElementById("search");
    let searchFunc = function (e) {
        console.log(e);
        if (e.which === 13) {
            location.href = `?search=${search.value}`;
        }

    };
    window.addEventListener("keypress", searchFunc);
})();