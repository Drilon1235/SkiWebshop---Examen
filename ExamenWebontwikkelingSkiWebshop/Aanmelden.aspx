<%@ Page Title="" Language="C#" MasterPageFile="~/Public.master" AutoEventWireup="true" CodeBehind="Aanmelden.aspx.cs" Inherits="ExamenWebontwikkelingSkiWebshop.Aanmelden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PublicContent" runat="server">
    <div class="container">
        <h1 class="mb-2 mt-2">Aanmelden</h1>

        <asp:Label ID="Label1" runat="server" Text="Gebruikersnaam: "></asp:Label>
        <asp:TextBox ID="txtGebruikersnaam" runat="server" CssClass="form-control"></asp:TextBox>

        
        <asp:Label ID="Label2" runat="server" Text="Wachtwoord: "></asp:Label>
        <asp:TextBox ID="txtWachtwoord" runat="server" CssClass="form-control"></asp:TextBox>

        <asp:Button ID="btnAanmelden" runat="server" Text="Aanmelden" CssClass="mt-2 mb-2 buttonversturen" />

    </div>
    
</asp:Content>
