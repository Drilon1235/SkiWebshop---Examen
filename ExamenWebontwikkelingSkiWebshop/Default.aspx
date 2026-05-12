<%@ Page Title="" Language="C#" MasterPageFile="~/Public.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExamenWebontwikkelingSkiWebshop.Default" %>


<asp:Content ID="Content2" ContentPlaceHolderID="PublicContent" runat="server">
    <div id="carouselExample"
        class="carousel slide"
        data-bs-ride="carousel"
        data-bs-interval="4000"
        data-bs-pause="false">

        <!-- INDICATORS -->
        <div class="carousel-indicators">
            <button type="button" data-bs-target="#carouselExample" data-bs-slide-to="0"
                class="active" aria-current="true">
            </button>

            <button type="button" data-bs-target="#carouselExample" data-bs-slide-to="1"></button>
        </div>

        <div class="carousel-inner">

            <!-- Slide 1 -->
            <div class="carousel-item active">
                <img src="images/xc.jpg" class="d-block w-100" alt="Langlaufen">

                <div class="carousel-caption custom-caption">
                    <p class="langlaufentekst">Langlaufen</p>
                    <a class="klikhieromTekst" href="Huren.aspx?type=langlauf">Klik hier om je langlaufmateriaal te huren</a>
                </div>
            </div>

            <!-- Slide 2 -->
            <div class="carousel-item">
                <img src="images/ski.jpg" class="d-block w-100" alt="Skiën">

                <div class="carousel-caption custom-caption">
                    <p class="langlaufentekst">LangLaufen</p>
                    <a class="klikhieromTekst" href="Huren.aspx?type=ski">Klik hier om je alpine skimateriaal te huren</a>
                </div>
            </div>

        </div>

        <!-- ⬅️ vorige -->
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
            <span class="carousel-control-prev-icon"></span>
        </button>

        <!-- ➡️ volgende -->
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
            <span class="carousel-control-next-icon"></span>
        </button>

    </div>
</asp:Content>
