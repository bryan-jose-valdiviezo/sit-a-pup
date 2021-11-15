﻿document.write('\
\
<head>\
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">\
    <link rel="stylesheet" href="/css/SignUp/SignUp.css" />\
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700,200" rel="stylesheet">\
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.1/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">\
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>\
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>\
</head>\
\
<nav class="navbar navbar-expand-lg bg-primary fixed-top navbar-transparent " color-on-scroll="400">\
    <div class="container">\
        <div class="dropdown button-dropdown">\
            <a href="#pablo" class="dropdown-toggle" id="navbarDropdown" data-toggle="dropdown">\
                <span class="button-bar"></span>\
                <span class="button-bar"></span>\
                <span class="button-bar"></span>\
            </a>\
            <div class="dropdown-menu" aria-labelledby="navbarDropdown">\
                <a class="dropdown-header">Dropdown header</a>\
                <a class="dropdown-item" href="#">Action</a>\
                <a class="dropdown-item" href="#">Another action</a>\
                <a class="dropdown-item" href="#">Something else here</a>\
                <div class="dropdown-divider"></div>\
                <a class="dropdown-item" href="#">Separated link</a>\
                <div class="dropdown-divider"></div>\
                <a class="dropdown-item" href="#">One more separated link</a>\
            </div>\
        </div>\
        <div class="collapse navbar-collapse justify-content-lg-start" id="navigation" data-nav-image="../assets/img/blurred-image-1.jpg">\
            <ul class="navbar-nav">\
                <li class="nav-item">\
                    <a class="nav-link" asp-controller="Home" asp-action="Index" asp-route-id="">Home</a>\
                </li>\
                <li class="nav-item">\
                    <a class="nav-link" asp-area="" asp-controller="Home" asp-action="FindSitter">Trouver un gardien</a>\
                </li>\
                <li class="nav-item">\
                    <a class="nav-link" asp-area="" asp-controller="Home" asp-action="BecomeSitter">Devenir gardien</a>\
                </li>\
                <li class="nav-item">\
                    <a class="nav-link" asp-area="" asp-controller="Users" asp-action="Index">Utilisateurs</a>\
                </li>\
                <li class="nav-item">\
                    <a class="nav-link" asp-area="" asp-controller="Chat" asp-action="Index">Chat</a>\
                </li>\
            </ul>\
        </div>\
        <div class="collapse navbar-collapse justify-content-end" id="navigation" data-nav-image="../assets/img/blurred-image-1.jpg">\
            <ul class="navbar-nav">\
                <li class="nav-item">\
                    <a class="nav-link" asp-controller="Users" asp-action="Details" asp-route-id="">Account</a>\
                </li>\
                <li class="nav-item">\
                    <a class="nav-link" href="https://github.com/creativetimofficial/now-ui-kit/issues">Have an issue?</a>\
                </li>\
                <li class="nav-item">\
                    <a class="nav-link" rel="tooltip" title="" data-placement="bottom" href="https://twitter.com/CreativeTim" target="_blank" data-original-title="Follow us on Twitter">\
                        <i class="fab fa-twitter"></i>\
                        <p class="d-lg-none d-xl-none">Twitter</p>\
                    </a>\
                </li>\
                <li class="nav-item">\
                    <a class="nav-link" rel="tooltip" title="" data-placement="bottom" href="https://www.facebook.com/CreativeTim" target="_blank" data-original-title="Like us on Facebook">\
                        <i class="fab fa-facebook-square"></i>\
                        <p class="d-lg-none d-xl-none">Facebook</p>\
                    </a>\
                </li>\
                <li class="nav-item">\
                    <a class="nav-link" rel="tooltip" title="" data-placement="bottom" href="https://www.instagram.com/CreativeTimOfficial" target="_blank" data-original-title="Follow us on Instagram">\
                        <i class="fab fa-instagram"></i>\
                        <p class="d-lg-none d-xl-none">Instagram</p>\
                    </a>\
                </li>\
            </ul>\
        </div>\
    </div>\
</nav>\
\
');