<%@ Page Title="Forum" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Forum.Web.Pages.Profile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 id="message" runat="server" visible="false"></h3>

    <div class="row">
        <div class="col-xs-1">
            <img id="profileImage" runat="server" src='<%# JpegToBase64(User.ProfileImage128Url) %>' alt="" />
        </div>
        <div class="col-xs-offset-1 col-xs-10">
            <h3><%# User.FullName %></h3>
            <h4>Prezent pe forum din: <%# User.CreatedAt.ToString("dd.MM.yyyy") %></h4>
        </div>
    </div>
    <div runat="server" visible="<%# SecurityContext.User?.Id == User.Id %>" class="row">
        <div class="col-xs-12 col-md-6 col-lg-4">
            <div class="form-group">
                <asp:FileUpload ID="profileImageUpload" runat="server" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="firstName" runat="server" CssClass="form-control" placeholder="Prenume" Text='<%# User.FirstName %>'></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="lastName" runat="server" CssClass="form-control" placeholder="Nume" Text='<%# User.LastName %>'></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:LinkButton ID="updateProfile" runat="server" OnClick="updateProfile_Click" CssClass="btn btn-success">
                    <i class="fa fa-wrench" aria-hidden="true"></i> Actualizeaza
                </asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
