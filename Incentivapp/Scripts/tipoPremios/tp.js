(() => {
    let idTipoPremio = "";
    $("#delTp").click((e) => {
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
})();