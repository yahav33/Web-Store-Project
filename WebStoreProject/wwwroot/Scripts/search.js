document.addEventListener("DOMContentLoaded", function () {
    const button = document.querySelector("#searchImg")
    button.addEventListener("click", searchval)

    function searchval() {
        let searchin = document.querySelector("#searchinput");
        const url = "Product/Search/?searchvalue=" + searchin;
        await fetch(url, {
            method: "POST",
        });

    }
});