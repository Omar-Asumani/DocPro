<%@ Page Title="Build doc" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Build.aspx.cs" Inherits="DocPro.Pages.Build" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3><%: Title %></h3>
    <asp:Label ID="TemplatesLabel" runat="server" Width="187"></asp:Label>
    <asp:DropDownList ID="TemplatesDropDownList" runat="server" OnSelectedIndexChanged="TemplatesDropDownList_SelectedIndexChanged"
        SelectMethod="PopulateTemplatesDropDownList" DataValueField="DocumentID" DataTextField="Name" AutoPostBack="True" Width="280">
    </asp:DropDownList>
    <asp:panel ID="TemplateInputsPanel" runat="server" style="margin-top:30px" Width="481">
    </asp:panel>
    <asp:Button ID="GenerateBtn" runat="server" Text="Generate" OnClick="GenerateBtn_Click" CssClass="btn top-margin" />
</asp:Content>
