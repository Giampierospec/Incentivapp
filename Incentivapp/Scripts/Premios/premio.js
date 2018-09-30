(() => {
    let idPremio = "";
    $("#delPr").click((e) => {
        e.preventDefault();
        idPremio = e.currentTarget.getAttribute("data-id");
        $("#dlSpan").html(`<b>${idPremio}</b>`);
        $("#delModal").modal();
    });
    $("#delBtn").click((e) => {
        $("#delModal").modal("hide");
        location.href = `/Premios/Delete/${idPremio}`;
    });
})();