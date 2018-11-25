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
    let search = document.getElementById("search");
    let searchFunc = function (e) {
        console.log(e);
        if (e.which === 13) {
            location.href = `?search=${search.value}`;
        }

    }
    window.addEventListener("keypress", searchFunc);
})();