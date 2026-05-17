<%@ Page Title="" Language="C#" MasterPageFile="~/Public.master" AutoEventWireup="true" CodeBehind="Huren.aspx.cs" Inherits="ExamenWebontwikkelingSkiWebshop.Huren" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PublicContent" runat="server">
    <div class="HurenAlles">
        <div class="container">
            <asp:Label ID="lblTitel" runat="server" Text="" CssClass="labelTitel"></asp:Label>

            <div class="alert alert-warning" id="divFout" runat="server">
                <asp:Label ID="lblFoutboodschap" runat="server" Text=""></asp:Label>
            </div>

            <div class="alert alert-success " id="divJuist" runat="server">
                <asp:Label ID="lblJuistboodschap" runat="server" Text=""></asp:Label>
            </div>

            <div class="row">
                <div class="col-md-9">

                    <div class="mt-1">
                        <asp:Label ID="Label1" runat="server" Text="Begindatum huren: " CssClass=""></asp:Label>
                    </div>
                    <asp:TextBox ID="txtBeginDatum" runat="server" TextMode="Date" CssClass="form-control mt-2 mb-3" OnTextChanged="txtBeginDatum_TextChanged" AutoPostBack="true"></asp:TextBox>


                    <div>
                        <asp:Label ID="Label2" runat="server" Text="Einddatum huren: " CssClass=""></asp:Label>
                    </div>
                    <asp:TextBox ID="txtEindDatum" runat="server" TextMode="Date" CssClass="form-control mt-2 mb-3" OnTextChanged="txtEindDatum_TextChanged" AutoPostBack="true"></asp:TextBox>


                    <div>
                        <asp:Label ID="Label3" runat="server" Text="Type materiaal: " CssClass=""></asp:Label>
                    </div>
                    <asp:DropDownList ID="ddlTypeMateriaal" runat="server" CssClass="form-select mt-2 mb-3" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeMateriaal_SelectedIndexChanged"></asp:DropDownList>


                    <div>
                        <asp:Label ID="Label4" runat="server" Text="Merk: " CssClass=""></asp:Label>
                    </div>
                    <asp:DropDownList ID="ddlMerk" runat="server" CssClass="form-select mt-2 mb-3" OnSelectedIndexChanged="ddlMerk_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>


                    <div>
                        <asp:Label ID="Label5" runat="server" Text="Materiaal: " CssClass=""></asp:Label>
                    </div>
                    <asp:DropDownList ID="ddlMateriaal" runat="server" CssClass="form-select mt-2 mb-3" AutoPostBack="true" OnSelectedIndexChanged="ddlMateriaal_SelectedIndexChanged"></asp:DropDownList>


                    <div>
                        <asp:Label ID="Label6" runat="server" Text="Maten: " CssClass=""></asp:Label>
                    </div>
                    <asp:DropDownList ID="ddlMaten" runat="server" CssClass="form-select mt-2 mb-3" AutoPostBack="true" OnSelectedIndexChanged="ddlMaten_SelectedIndexChanged"></asp:DropDownList>


                    <div>
                        <asp:Label ID="Label7" runat="server" Text="Nog beschikbaar: " CssClass=""></asp:Label>
                    </div>
                    <asp:TextBox ID="txtMaxAantal" runat="server" CssClass="form-control mt-2 mb-3" Enabled="False"></asp:TextBox>

                    <div>
                        <asp:Label ID="Label8" runat="server" Text="Aantal huren: " CssClass=""></asp:Label>
                    </div>
                    <asp:TextBox ID="txtHuren" runat="server" CssClass="form-control mt-2 mb-3"></asp:TextBox>

                </div>


                <div class="col-md-3">
                    <img src="images/products/atomicredsterg9revoshock.jpg" id="langlaufenfotoHuren" width="400" height="400" class="mt-3" />
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="buttonsHuren">
                            <asp:Button ID="btnToevoegenAanWinkelMand" runat="server" Text="Toevoegen aan winkelmand" CssClass="buttonHuren mb-3" OnClick="btnToevoegenAanWinkelMand_Click" />
                            <asp:Button runat="server" id="btnToonWinkelMand" text="Toon winkelmand" class="buttonHuren mb-3" OnClick="btnToonWinkelMand_Click" />
                            <asp:Button ID="Button3" runat="server" Text="Huur bevestigen" CssClass="buttonHuren mb-3" />
                        </div>
                    </div>
                </div>

                <div class="modal fade" id="modalForm" data-bs-backdrop="static">
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4>Winkelmand</h4>
                                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                            </div>
                            <div class="modal-body mt-4 mb-4 p-0">
                                <asp:Table ID="TableModal" runat="server" CssClass="table-striped table">
<%--                                    gevuld met c#--%>
                                </asp:Table>
                            </div>
                            <div class="modal-footer">
                                <button ID="btnModalSluiten" class="buttonHuren mb-3 btnModalSluiten">Sluiten</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
