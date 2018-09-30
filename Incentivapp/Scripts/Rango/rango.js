(() => {
    let idRango = "";
    $("#delRng").click((e) => {
        e.preventDefault();
        idRango = e.currentTarget.getAttribute("data-id");
        $("#dlSpan").html(`<b>${idRango}</b>`);
        $("#delModal").modal();
    });
    $("#delBtn").click((e) => {
        $("#delModal").modal("hide");
        location.href = `/Rango/Delete/${idRango}`;
    });
})();