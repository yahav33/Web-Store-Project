document.addEventListener("DOMContentLoaded", function () {
    const button = document.querySelectorAll(".nav-link")
    for (var i = 0; i < button.length; i++) {
        button[i].addEventListener("click", openCity(event))
    }
    



    function openCity(evt, cityName) {

        var pageurl = window.location.href;
        console.log(pageurl);


        var tablinks = document.getElementsByClassName("nav-link");
        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
            tablinks[i].style.fontWeight = 'bold';
        }

        switch (pageurl) {

            case "https://localhost:44347/home":
                var ele = document.getElementById("home");
                ele.className += " active";
                break;
            case "https://localhost:44347/home/about":
                var ele = document.getElementById("about");
                ele.className += " active";
                break;
            case "https://localhost:44347/Login" :
                var ele = document.getElementById("login");
                ele.className += " active";
                break;
            case "https://localhost:44347/Register":
                var ele = document.getElementById("register");
                ele.className += " active";
                break;
            case "https://localhost:44347/ShoppingCart":
                var ele = document.getElementById("cart");
                ele.className += " active";
                break;
            case "https://localhost:44347/Product":
                var ele = document.getElementById("addposter");
                ele.className += " active";
                break;
            case "https://localhost:44347/Admin":
                var ele = document.getElementById("admin");
                ele.className += " active";
                break;
            case "https://localhost:44347/Product/MyProducts":
                var ele = document.getElementById("myProducts");
                ele.className += " active";
                break;
            default:
                var ele = document.getElementById("home");
                ele.className += " active";
                break;
        }



       
    }

    
});