document.addEventListener("DOMContentLoaded", function () {
    const button = document.querySelector("#btn");
    button.addEventListener("click", myFunc);


    async function myFunc(e) {
        e.preventDefault();
        var ischecked =  "no";
        var checkBox = document.getElementById("admincheck")
        if (checkBox.checked == true)
        {
            ischecked = "yes";
        }
        var userid = document.querySelector("#userId").innerHTML;

        const update = {
            Userid: userid,
            isChecked: ischecked,
           
        }

        const check = JSON.stringify(update);

        const url = "/Admin/AuthorizationAdmin/"
        await fetch(url, {
            method: 'PUT',
            body: check,
            headers: {
                'Content-Type': 'application/json',
            }

        });
        alert("User was updated!");
    }
});