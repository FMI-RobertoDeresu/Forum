<%@ Page Title="Forum" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Posts.aspx.cs" Inherits="Forum.Web.Pages.Posts" %>

<%@ Import Namespace="Forum.Web.Helpers" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 id="message" runat="server" visible="false"></h3>
    <asp:HiddenField ID="topicId" runat="server" />

    <div runat="server" visible='<%# SecurityContext.IsAuthenticated %>' class="row">
        <div class="form-group" style="margin-bottom: 5px">
            <asp:TextBox ID="newPostText" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" Wrap="true" placeholder="Continut postare" />
        </div>
        <asp:LinkButton ID="insertPostButton" runat="server" OnClick="insertPostButton_Click" CssClass="btn btn-default btn-success">
            <i class="fa fa-plus" aria-hidden="true"></i> Adauga
        </asp:LinkButton>
    </div>

    <h4>Postari sortate dupa
        <asp:DropDownList ID="sortByDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="sortByDropDownList_SelectedIndexChanged"></asp:DropDownList>
    </h4>

    <asp:ListView ID="postsListView" DataSource="<%# GetPosts(CustomConvert.ToInt32(topicId.Value)) %>"
        OnItemEditing="postsView_ItemEditing" OnItemUpdating="postsView_ItemUpdating"
        OnItemCanceling="postsView_ItemCanceling" OnItemDeleting="postsView_ItemDeleting" runat="server">
        <LayoutTemplate>
            <div class="posts-container">
                <div id="itemPlaceholder" runat="server" />
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="row post-wrapper">
                <asp:HiddenField ID="Id" runat="server" Value='<%# Eval("Id") %>' />
                <div class="post-info col-xs-5 col-md-3 col-lg-2">
                    <div class="thumbnail">
                        <img runat="server" src='<%# JpegToBase64(Eval("CreatedByProfileImage").ToString()) %>' alt="" />
                        <div class="caption">
                            <a href='<%# GetRouteUrl("Profile", new { userId = Eval("CreatedById") }) %>'><%# Eval("CreatedByName") %></a>
                            <p><%# Eval("CreatedAt") %></p>
                            <div>
                                <asp:LinkButton ID="EditButton" runat="server" Visible='<%# Eval("CanEdit") %>' CommandName="Edit" CssClass="btn btn-xs btn-primary">
                                    <i class="fa fa-pencil" aria-hidden="true"></i> Modifica
                                </asp:LinkButton>
                                <asp:LinkButton ID="DeleteButton" runat="server" Visible='<%# Eval("CanDelete") %>' CommandName="Delete" CssClass="btn btn-xs btn-danger">
                                    <i class="fa fa-trash-o" aria-hidden="true"></i> Sterge
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-10 col-md-9 col-xs-7 post-text-wrapper font-17">
                    <div class="post-text">
                        <p><%# Eval("Text").ToString().Replace("\n", "<br/>") %></p>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <EditItemTemplate>
            <div class="row post-wrapper">
                <asp:HiddenField ID="Id" runat="server" Value='<%# Eval("Id") %>' />
                <div class="post-text col-xs-12">
                    <div class="form-group" style="margin-bottom: 5px">
                        <asp:TextBox ID="Text" runat="server" Text='<%# Eval("Text") %>' TextMode="MultiLine" CssClass="form-control" />
                    </div>
                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="btn btn-xs btn-success">
                        <i class="fa fa-check" aria-hidden="true"></i> Salveaza
                    </asp:LinkButton>
                    <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" CssClass="btn btn-xs btn-danger">
                        <i class="fa fa-times" aria-hidden="true"></i> Renunta
                    </asp:LinkButton>
                </div>
            </div>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>
