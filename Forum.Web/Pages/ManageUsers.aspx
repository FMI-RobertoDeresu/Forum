<%@ Page Title="Forum" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="Forum.Web.Pages.ManageUsers" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 id="message" runat="server" visible="false"></h3>
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-4">
            <div class="input-group">
                <asp:TextBox ID="searchQuery" runat="server" CssClass="form-control" onEnterTriggerClick="#searchButton" placeholder="Cauta utilizator.."></asp:TextBox>
                <span class="input-group-btn">
                    <asp:LinkButton ID="searchButton" runat="server" ClientIDMode="Static" OnClick="searchButton_Click"
                        OnClientClick='javascript:__doPostBack("ctl00$MainContent$searchButton","")' CssClass="btn btn-default">
                        <i class="fa fa-search" aria-hidden="true"></i>
                    </asp:LinkButton>
                </span>
            </div>
        </div>
    </div>
    <br />
    <asp:GridView ID="usersGridView" runat="server" DataKeyNames="Id" DataSource='<%# GetUsers() %>' AutoGenerateColumns="false"
        CssClass="table" UseAccessibleHeader="true" OnRowCreated="usersGridView_RowCreated" OnRowEditing="usersGridView_RowEditing"
        OnRowUpdating="usersGridView_RowUpdating" OnRowCancelingEdit="usersGridView_RowCanceling">
        <Columns>
            <asp:BoundField DataField="Id" ReadOnly="true" Visible="false" />
            <asp:TemplateField ShowHeader="true" HeaderText="Utilizator" ItemStyle-CssClass="col-md-3">
                <ItemTemplate>
                    <img src='<%# JpegToBase64(Eval("ProfileImageUrl").ToString()) %>' class="profile" />
                    <span class="font-20"><%# Eval("UserName") %></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="true" HeaderText="Nume" ItemStyle-CssClass="col-md-4">
                <ItemTemplate>
                    <span class="font-20"><%#Eval("FullName") %></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="true" HeaderText="Rol" ItemStyle-CssClass="col-md-3">
                <ItemTemplate>
                    <span class="font-20"><%#Eval("Role") %></span>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="roleDropDown" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="col-md-2">
                <ItemTemplate>
                    <asp:LinkButton ID="editButton" runat="server" CssClass="btn btn-xs btn-default" CommandName="Edit">
                        <i class="fa fa-pencil" aria-hidden="true"></i> Edit
                    </asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="updateButton" runat="server" CssClass="btn btn-xs btn-success" CommandName="Update">
                        <i class="fa fa-check" aria-hidden="true"></i> Update
                    </asp:LinkButton>
                    <asp:LinkButton ID="cancelButton" runat="server" CssClass="btn btn-xs btn-danger" CommandName="Cancel">
                        <i class="fa fa-times" aria-hidden="true"></i> Cancel
                    </asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
