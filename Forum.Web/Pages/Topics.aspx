<%@ Page Title="Forum" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Topics.aspx.cs" Inherits="Forum.Web.Pages.Topics" %>

<%@ Import Namespace="Forum.Web.Helpers" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 id="message" runat="server"></h3>
    <asp:HiddenField ID="subjectId" runat="server" />

    <asp:ListView ID="topicsListView" runat="server" DataSource="<%# GetTopics() %>"
        InsertItemPosition="FirstItem" OnItemInserting="topicsListView_ItemInserting" OnItemEditing="topicsListView_ItemEditing"
        OnItemUpdating="topicsListView_ItemUpdating" OnItemCanceling="topicsListView_ItemCanceling"
        OnItemDeleting="topicsListView_ItemDeleting" OnItemDataBound="topicsListView_ItemDataBound">
        <LayoutTemplate>
            <ul class="list-group">
                <div id="itemPlaceholder" runat="server"></div>
            </ul>
        </LayoutTemplate>
        <InsertItemTemplate>
            <li runat="server" visible='<%# SecurityContext.IsAuthenticated %>' class="list-group-item">
                <div class="input-group">
                    <asp:TextBox ID="name" runat="server" CssClass="form-control input-sm" autocomplete="off" onEnterTriggerClick="#insertButton" placeholder="Denumire tema" />
                    <span class="input-group-btn">
                        <asp:LinkButton ID="insertButton" runat="server" ClientIDMode="Static" CommandName="Insert" CssClass="btn btn-sm btn-success"
                            OnClientClick="javascript:__doPostBack('ctl00$MainContent$topicsListView$ctrl0$insertButton','')">
                             <i class="fa fa-plus" aria-hidden="true"></i> Adauga
                        </asp:LinkButton>
                    </span>
                </div>
            </li>
        </InsertItemTemplate>
        <ItemTemplate>
            <li class="list-group-item">
                <asp:HiddenField ID="id" Value='<%# Eval("Id") %>' Visible="false" runat="server" />
                <asp:HyperLink ID="topicPostsButton" runat="server" NavigateUrl='<%# Eval("PostsUrl") %>' CssClass="underline-fix">
                    <span class="font-20"><%# Eval("Name") %></span>
                </asp:HyperLink>
                <div class="pull-right">
                    <asp:HyperLink ID="goToPostsButton" runat="server" NavigateUrl='<%# Eval("PostsUrl") %>' CssClass="btn btn-xs btn-default">
                        <i class="fa fa-comments" aria-hidden="true"></i> 
                        <span class="badge"><%# Eval("NumberOfPosts") %></span> Postari
                    </asp:HyperLink>
                    <asp:LinkButton ID="categoryEditButton" runat="server" Visible='<%# Eval("CanEdit") %>' CommandName="Edit" CssClass="btn btn-xs btn-warning">
                            <i class="fa fa-pencil" aria-hidden="true"></i> Modifica
                    </asp:LinkButton>
                    <asp:LinkButton ID="categoryDeleteButton" runat="server" Visible='<%# Eval("CanDelete") %>' CommandName="Delete" CssClass="btn btn-xs btn-danger">
                            <i class="fa fa-trash" aria-hidden="true"></i> Sterge
                    </asp:LinkButton>
                </div>
            </li>
        </ItemTemplate>
        <EditItemTemplate>
            <li class="list-group-item">
                <asp:HiddenField ID="id" Value='<%# Eval("Id") %>' Visible="false" runat="server" />
                <div class="input-group">
                    <asp:TextBox ID="name" runat="server" Visible="false" Text='<%# Eval("Name") %>' CssClass="form-control input-sm" />
                    <asp:DropDownList ID="subject" runat="server" Visible="false" CssClass="form-control input-sm"
                         ToolTip="muta acest topic in alt subiect"></asp:DropDownList>
                    <span class="input-group-btn">
                        <asp:LinkButton ID="updateButton" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success">
                                <i class="fa fa-check" aria-hidden="true"></i> Salveaza
                        </asp:LinkButton>
                        <asp:LinkButton ID="cancelButton" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-danger">
                              <i class="fa fa-times" aria-hidden="true"></i> Renunta
                        </asp:LinkButton>
                    </span>
                </div>
            </li>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>
