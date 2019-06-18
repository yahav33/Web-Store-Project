document.addEventListener("DOMContentLoaded", function () {
    const button = document.querySelectorAll("#delete");
    for (var i = 0; i < button.length; i++) {
        button[i].addEventListener("click", myDelete)
    }
    



    //catch the buttons
    async function myDelete(e) {
        e.preventDefault();
        const id = e.target.getAttribute("data-id");
        const url = document.querySelector(".del").getAttribute("href") + "/" + id;
        await fetch(url, {
            method: "DELETE",
        });
        


    }
});