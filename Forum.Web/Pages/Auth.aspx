<%@ Page Title="Forum" Language="C#" AutoEventWireup="true" CodeBehind="Auth.aspx.cs" Inherits="Forum.Web.Pages.Auth" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %></title>
    <%:Styles.Render("~/css") %>
    <%:Scripts.Render("~/js") %>
</head>
<body>
    <form id="formAuth" runat="server">
        <div id="auth-wrapper" class="row">
            <div>
                <h4 id="message" runat="server" visible="false"></h4>
                <asp:HiddenField ID="pageActionType" runat="server" />
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox ID="username" runat="server" CssClass="form-control" placeholder="Utilizator"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                        <asp:TextBox ID="password" runat="server" onEnterTriggerClick="#signInButton1" CssClass="form-control"
                            type="password" placeholder="Parola"></asp:TextBox>
                    </div>
                </div>
                <div id="passwordReWrapper" runat="server" visible="false" class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                        <asp:TextBox ID="passwordRe" runat="server" CssClass="form-control" type="password"
                            onEnterTriggerClick="#registerButton2" placeholder="Reintroduceti parola"></asp:TextBox>
                    </div>
                </div>
                <div id="rememberMeWrapper" runat="server" visible="false" class="form-group white">
                    <asp:CheckBox ID="rememberMe" runat="server" Checked="true" Text="Tine-ma minte!"></asp:CheckBox>
                </div>
                <div class="form-group row">
                    <div class="col-xs-12 text-center">
                        <div id="signInButtons" runat="server" visible="false">
                            <asp:LinkButton ID="signInButton1" ClientIDMode="Static" runat="server" CssClass="btn btn-primary"
                                CommandName="signIn" OnClick="signInButton_Click" OnClientClick="javascript:__doPostBack('signInButton1','')">
                                <i class="fa fa-hand-o-right" aria-hidden="true"></i> Conectare
                            </asp:LinkButton>
                            <asp:LinkButton ID="registerButton1" runat="server" CssClass="btn btn-default" CommandName="register"
                                OnClick="registerButton_Click">
                                Nu am cont <i class="fa fa-arrow-right" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                        <div id="registerButtons" runat="server" visible="false">
                            <asp:LinkButton ID="registerButton2" ClientIDMode="Static" runat="server" CssClass="btn btn-primary"
                                CommandName="register" OnClick="registerButton_Click" OnClientClick="javascript:__doPostBack('registerButton2','')">
                                <i class="fa fa-thumbs-up" aria-hidden="true"></i> Inregistrare
                            </asp:LinkButton>
                            <asp:LinkButton ID="signInButton2" runat="server" CssClass="btn btn-default" CommandName="signIn" OnClick="signInButton_Click">
                                Am deja cont <i class="fa fa-arrow-right" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="form-group text-center">
                    <asp:HyperLink ID="backToSiteButton" runat="server" NavigateUrl='<%# GetRouteUrl("Default", null) %>' ForeColor="White">
                        Continua ca vizitator
                    </asp:HyperLink>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
