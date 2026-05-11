<%@ Page Title="" Language="C#" MasterPageFile="~/Private.master" AutoEventWireup="true" CodeBehind="BeheerMerken.aspx.cs" Inherits="ExamenWebontwikkelingSkiWebshop.Beheermerken" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrivateContent" runat="server">
    <div class="container">
        <h1 class="mb-2 mt-2 titelBold">Beheer Merken</h1>


        <asp:Label ID="Label1" runat="server" Text="Selecteer een merk: "></asp:Label>
        <asp:DropDownList ID="ddlMerken" runat="server" CssClass="form-control"></asp:DropDownList>


        <asp:Label ID="Label2" runat="server" Text="Naam: "></asp:Label>
        <asp:DropDownList ID="ddlNaam" runat="server" CssClass="form-control" ></asp:DropDownList>

        <div class="buttonsHuren">       <%--de buttons in huren hebben dezelfde opmaak--%>
            <asp:Button ID="btnNieuw" runat="server" Text="Nieuw" CssClass="mt-2 mb-2 buttonHuren" />
            <asp:Button ID="btnBewaren" runat="server" Text="Bewaren" CssClass="mt-2 mb-2 buttonHuren" />
            <asp:Button ID="btnVerwijderen" runat="server" Text="Verwijderen" CssClass="mt-2 mb-2 buttonHuren" />
        </div>



    </div>
</asp:Content>
