(() => {
    let idRol = "";
    $(document).on("click",".delRol",(e) => {
        e.preventDefault();
        idRol = e.currentTarget.getAttribute("data-id");
        $("#dlSpan").html(`<b>${idRol}</b>`);
        $("#delModal").modal();
    });
    $("#delBtn").click((e) => {
        $("#delModal").modal("hide");
        location.href = `/Roles/Delete/${idRol}`;
    });
})();