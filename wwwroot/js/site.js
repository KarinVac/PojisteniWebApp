@using Microsoft.AspNetCore.Identity
@using PojisteniWebApp
@inject SignInManager < IdentityUser > SignInManager

    < !DOCTYPE html >
        <html lang="cs">
            <head>
                <meta charset="utf-8" />
                <meta name="viewport" content="width=device-width, initial-scale=1.0" />
                <meta name="author" content="Aplikace pojištění" />
                <title>KVinsurance</title>
                <link rel="shortcut icon" href="~/images/favicon-32x32.png" />
                <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" asp-append-version="true" />
                <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
                @await RenderSectionAsync("CSS", required: false)
            </head>

            <body>
                <header>
                    <nav class="navbar navbar-expand-lg navbar-dark bg-primary fixed-top">
                        <div class="container">
                            <a class="navbar-brand" asp-controller="Home" asp-action="Index">
                                <img src="~/images/Gemini_Generated_Image_lbiiltlbiiltlbii.png" alt="Logo KV Insurance" style="height:40px;" />
                            </a>
                            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#mainNav"
                                aria-controls="mainNav" aria-expanded="false" aria-label="Toggle navigation">
                                <span class="navbar-toggler-icon"></span>
                            </button>

                            <div class="collapse navbar-collapse" id="mainNav">
                                <ul class="navbar-nav ms-auto">
                                    <li class="nav-item">
                                        <a class="nav-link" asp-controller="Home" asp-action="Index">Domů</a>
                                    </li>

                                    @if (User.IsInRole(UserRoles.Admin))
                                    {
                            <li class="nav-item">
                                <a class="nav-link" asp-controller="Clients" asp-action="Index">Pojištěnci</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" asp-controller="Insurances" asp-action="Index">Pojištění</a>
                            </li>
                                    }

                                    <li class="nav-item">
                                        <a class="nav-link" asp-controller="Home" asp-action="Contact">Kontakt</a>
                                    </li>

                                    @if (SignInManager.IsSignedIn(User))
                                    {
                                        @if (User.IsInRole(UserRoles.User))
                                    {
                                        <li class="nav-item">
                                            <a class="nav-link" asp-controller="Profile" asp-action="Index">Moje údaje</a>
                                        </li>
                                    }
                                    <li class="nav-item">
                                        <form asp-controller="Account" asp-action="Logout" method="post" id="logoutForm" style="display:none;"></form>
                                        <a href="#" onclick="event.preventDefault(); document.getElementById('logoutForm').submit();" class="btn btn-warning ms-lg-3">Odhlásit se</a>
                                    </li>
                        }
                                    else
                                    {
                                        <li class="nav-item">
                                            <a class="btn btn-warning ms-lg-3" asp-controller="Account" asp-action="Login">Přihlásit se</a>
                                        </li>
                                    }
                                </ul>
                            </div>
                        </div>
                    </nav>
                </header>

                <main class="container mt-5 pt-4">
                    <header>
                        <h1 class="my-4">@ViewData["Title"]</h1>
                    </header>
                    <section>
                        @RenderBody()
                    </section>
                </main>

                <footer class="bg-dark text-white text-center py-4 mt-5">
                    Vytvořila &copy;Karin Vaculíková @DateTime.Now.Year
                </footer>

                <script src="~/lib/jquery/dist/jquery.min.js"></script>
                <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
                <script src="~/js/site.js" asp-append-version="true"></script>
                @await RenderSectionAsync("Scripts", required: false)
            </body>
        </html>
