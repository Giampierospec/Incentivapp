(() => {
    let idPremio = "";
    $(document).on("click",".delPr",(e) => {
        e.preventDefault();
        idPremio = e.currentTarget.getAttribute("data-id");
        $("#dlSpan").html(`<b>${idPremio}</b>`);
        $("#delModal").modal();
    });
    $("#delBtn").click((e) => {
        $("#delModal").modal("hide");
        location.href = `/Premios/Delete/${idPremio}`;
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