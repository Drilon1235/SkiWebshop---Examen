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
                            <asp:Button runat="server" ID="btnToonWinkelMand" Text="Toon winkelmand" class="buttonHuren mb-3" OnClick="btnToonWinkelMand_Click" />
                            <asp:Button ID="Button3" runat="server" Text="Huur bevestigen" CssClass="buttonHuren mb-3" OnClick="Button3_Click" />
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
                                <button id="btnModalSluiten" class="buttonHuren m-2 btnModalSluiten">Sluiten</button>
                            </div>
                        </div>
                    </div>
                </div>



                <%--                modal huur bevestigen--%>
                <div class="modal fade" id="modalFormBevestigen" data-bs-backdrop="static">
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4>Bevestig huur</h4>
                                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                            </div>
                            <div class="modal-body mt-4 mb-3 p-0">
                                <asp:Table ID="TableBevestigenModal" runat="server" CssClass="table-striped table">
                                    <%--                                    gevuld met c#--%>
                                </asp:Table>
                                <div class=" m-3">
                                    <div class="mb-2 mt-4">
                                        <asp:Label ID="Label9" runat="server" Text="Voornaam: " CssClass=""></asp:Label>
                                    </div>
                                    <asp:TextBox ID="txtVoornaam" runat="server" CssClass="form-control mb-4" Required="required"></asp:TextBox>
                                    <div class="mb-2">
                                        <asp:Label ID="Label10" runat="server" Text="Achternaam: "></asp:Label>
                                    </div>
                                    <asp:TextBox ID="txtAchternaam" runat="server" CssClass="form-control mb-4" Required="required"></asp:TextBox>
                                    <div class="mb-2">
                                        <asp:Label ID="Label11" runat="server" Text="E-mail: "></asp:Label>
                                    </div>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Required="required"></asp:TextBox>
                                </div>

                            </div>
                            <div class="modal-footer">
                                <button id="btnModalSluitenBevestigen" class="buttonHuren m-2 btnModalSluiten">Bestelling plaatsen</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
