<%@ Page Title="Import doc" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="DocPro.Pages.Import" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3><%: Title %>:</h3>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="SubmitTemplateBtn_Click" CssClass="btn top-margin" />
            <asp:Label ID="ErrorLabel" runat="server" CssClass="validation-msg"></asp:Label>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
