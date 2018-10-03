(() => {
    let idUsuario = "";
    $(document).on("click",".delUser",(e) => {
        e.preventDefault();
        idUsuario = e.currentTarget.getAttribute("data-id");
        $("#dlSpan").html(`<b>${idUsuario}</b>`);
        $("#delModal").modal();
    });
    $("#delBtn").click((e) => {
        $("#delModal").modal("hide");
        location.href = `/Usuarios/Delete/${idUsuario}`;
    });
})();