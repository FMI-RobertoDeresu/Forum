<%@ Page Title="Forum" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Forum.Web.Pages.Search" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 id="message" runat="server" visible="false"></h3>

    <asp:ListView ID="searchResultListView" runat="server">
        <LayoutTemplate>
            <ul class="list-group">
                <div id="itemPlaceholder" runat="server"></div>
            </ul>
        </LayoutTemplate>
        <ItemTemplate>
            <li class="list-group-item" style="margin-bottom:10px">
                <asp:HyperLink ID="resultLink" runat="server" NavigateUrl='<%# Eval("NavigateUrl") %>' Text='<%# Eval("DsiplayName") %>' Visible='<%# Eval("IsHyperlinkResult") %>'></asp:HyperLink>
                <div Id="resultContent" runat="server" Visible='<%# Eval("IsContentResult") %>'>
                    <div>
                        <%# Eval("ContentResult").ToString().Replace("\n", "<br/>") %>
                    </div>
                </div>
            </li>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
