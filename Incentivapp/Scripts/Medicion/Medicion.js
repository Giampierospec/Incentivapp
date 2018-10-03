(() => {
    let idMed = "";
    $(document).on("click",".delMed",(e) => {
        e.preventDefault();
        idMed = e.currentTarget.getAttribute("data-id");
        $("#dlSpan").html(`<b>${idMed}</b>`);
        $("#delModal").modal();
    });
    $("#delBtn").click((e) => {
        $("#delModal").modal("hide");
        location.href = `/Medicion/Delete/${idMed}`;
    });
})();