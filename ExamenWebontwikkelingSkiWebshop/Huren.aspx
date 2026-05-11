<%@ Page Title="" Language="C#" MasterPageFile="~/Public.master" AutoEventWireup="true" CodeBehind="Huren.aspx.cs" Inherits="ExamenWebontwikkelingSkiWebshop.Huren" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PublicContent" runat="server">
    <div class="container">
        <asp:Label ID="lblTitel" runat="server" Text=""></asp:Label>

        <div>
            <asp:Label ID="Label1" runat="server" Text="Begindatum huren: "></asp:Label>
        </div>
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Einddatum huren: "></asp:Label>
        </div>
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>

        <div>
            <asp:Label ID="Label3" runat="server" Text="Einddatum huren: "></asp:Label>
        </div>
        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-select"></asp:DropDownList>

    </div>
</asp:Content>
