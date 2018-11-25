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
    let search = document.getElementById("search");
    let searchFunc = function (e) {
        console.log(e);
        if (e.which === 13) {
            location.href = `?search=${search.value}`;
        }

    };
    window.addEventListener("keypress", searchFunc);
})();