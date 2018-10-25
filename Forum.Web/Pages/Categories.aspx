<%@ Page Title="Forum" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="Forum.Web.Pages.Categories" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 id="message" runat="server" visible="false"></h3>
    <asp:HiddenField ID="categoryIdHiddenField" runat="server" />
    
    <asp:ListView ID="categoriesListView" runat="server" DataSource="<%# GetCategories() %>" DataKeyNames="Id"
        OnItemDataBound="categoriesListView_ItemDataBound" InsertItemPosition="FirstItem"
        OnItemInserting="categoriesListView_ItemInserting" OnItemEditing="categoriesListView_ItemEditing"
        OnItemCanceling="categoriesListView_ItemCanceling" OnItemUpdating="categoriesListView_ItemUpdating"
        OnItemDeleting="categoriesListView_ItemDeleting" OnItemCommand="categoriesListView_ItemCommand">
        <InsertItemTemplate>
            <div class="row" runat="server" visible='<%# SecurityContext.IsAdmin %>'>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <div class="input-group">
                        <asp:TextBox ID="name" runat="server" onEnterTriggerClick="#createCategoryButton" CssClass="form-control" placeholder="Denumire categorie" />
                        <div class="input-group-btn">
                            <asp:LinkButton ID="createCategoryButton" ClientIDMode="Static" runat="server" CommandName="Insert"
                                OnClientClick="javascript:__doPostBack('ctl00$MainContent$categoriesListView$ctrl0$createCategoryButton','')" CssClass="btn btn-default">
                                <i class="fa fa-plus" aria-hidden="true">&nbsp;Adauga</i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </InsertItemTemplate>
        <ItemTemplate>
            <div class="category-wrapper panel panel-default">
                <div class="category-info panel-heading">
                    <asp:HiddenField ID="id" Value='<%# Eval("Id") %>' Visible="false" runat="server" />
                    <a href="<%# string.Format("#category-{0}-subjects", Eval("Id")) %>" class="collapsed link-collapse underline-fix" data-toggle="collapse">
                        <h3 style="display: inline"><%# Eval("Name") %></h3>
                    </a>
                    <span style="display: inline">de <a href='<%# GetRouteUrl("Profile", new { userId = Eval("CreatedById") }) %>'><%# Eval("CreatedByName") %></a>
                        la <%# Eval("CreatedAt") %>
                    </span>
                    <div class="pull-right">
                        <asp:LinkButton ID="showInsertSubjectButton" runat="server" OnClick="showInsertSubjectButton_Click"
                            CssClass="btn btn-sm btn-success btn-addSubject" ToolTip="adauga subiect" Visible='<%# SecurityContext.IsAdmin %>'>
                            <i class="fa fa-plus" aria-hidden="true"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="categoryEditButton" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-warning" ToolTip="modifica denumire categorie" Visible='<%# SecurityContext.IsAdmin %>'>
                            <i class="fa fa-pencil" aria-hidden="true"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="categoryDeleteButton" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-danger" ToolTip="sterge categorie" Visible='<%# SecurityContext.IsAdmin %>'>
                            <i class="fa fa-trash" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </div>
                </div>
                <div id="<%# string.Format("category-{0}-subjects", Eval("Id")) %>" class="panel-collapse collapse">
                    <div class="categorySubjects-container panel-body">
                        <asp:GridView ID="subjectsGridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="Id"
                            AllowCustomPaging="true" PageSize='<%# PageSize %>' OnPageIndexChanging="subjectsGridView_PageIndexChanging"
                            OnRowEditing="subjectsGridView_RowEditing" OnRowDeleting="subjectsGridView_RowDeleting"
                            OnRowUpdating="subjectsGridView_RowUpdating" OnRowCancelingEdit="subjectsGridView_RowCanceling"
                            ShowFooter="false" OnRowCommand="subjectsGridView_RowCommand" OnRowCreated="subjectsGridView_RowCreated"
                            CssClass="table table-custom">
                            <HeaderStyle CssClass="table-header" />
                            <EmptyDataTemplate>
                                <div runat="server" visible='<%# SecurityContext.IsAdmin %>' class="input-group" style="width: 100%;">
                                    <asp:TextBox ID="subjectName" runat="server" CssClass="form-control input-sm" placeholder="Denumire subiect" />
                                    <span class="input-group-btn">
                                        <asp:LinkButton ID="insertRow" runat="server" CommandName="InsertRow" CssClass="btn btn-sm btn-success">
                                            <i class="fa fa-plus" aria-hidden="true">&nbsp;Adauga</i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="cancelInsertRow" runat="server" CommandName="CancelInsertRow" CssClass="btn btn-sm btn-danger">
                                            <i class="fa fa-times" aria-hidden="true">&nbsp;Renunta</i>
                                        </asp:LinkButton>
                                    </span>
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="Id" ReadOnly="true" Visible="false" />
                                <asp:TemplateField SortExpression="Name" HeaderText="Subiect" HeaderStyle-CssClass="col-xs-6 col-sm-5">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="subject" NavigateUrl='<%# GetRouteUrl("SubjectTopics", new { subjectId = Eval("Id") }) %>' runat="server" CssClass="underline-fix">
                                            <h4 style="display:inline"><%#Eval("Name") %></h4>
                                        </asp:HyperLink>
                                        <span>de <a href='<%# GetRouteUrl("Profile", new { userId = Eval("CreatedById") }) %>'><%# Eval("CreatedByName") %></a>
                                            la <%# Eval("CreatedAt") %>
                                        </span>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="subjectName" runat="server" Text='<%#Eval("Name") %>' CssClass="form-control input-sm" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <div runat="server" class="input-group" style="width: 100%">
                                            <asp:TextBox ID="subjectName" runat="server" CssClass="form-control input-sm" placeholder="Denumire subiect" />
                                            <span class="input-group-btn">
                                                <asp:LinkButton ID="insertRow" runat="server" CommandName="InsertRow" CssClass="btn btn-sm btn-success">
                                                    <i class="fa fa-plus" aria-hidden="true">&nbsp;Adauga</i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="cancelInsertRow" runat="server" CommandName="CancelInsertRow" CssClass="btn btn-sm btn-danger">
                                                    <i class="fa fa-times" aria-hidden="true">&nbsp;Renunta</i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NumberOfTopics" HeaderText="Teme" HeaderStyle-CssClass="col-xs-2">
                                    <ItemTemplate>
                                        <asp:Label ID="subjectNoOfTopicsLabel" runat="server" Text='<%#Eval("NumberOfTopics") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NumberOfPosts" HeaderText="Postari" HeaderStyle-CssClass="col-xs-2">
                                    <ItemTemplate>
                                        <asp:Label ID="subjectNoOfPostsLabel" runat="server" Text='<%#Eval("NumberOfPosts") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="LastPostCreatedAtSortFormat" HeaderText="Ultima postare" HeaderStyle-CssClass="col-sm-2 hidden-xs" ItemStyle-CssClass="hidden-xs">
                                    <ItemTemplate>
                                        <span runat="server" visible='<%# Eval("LastPostCreatedAt") != null %>'>de <a href='<%# GetRouteUrl("Profile", new { userId = Eval("LastPostCreatedById") }) %>'><%# Eval("LastPostCreatedByName") %></a>
                                            la <%# Eval("LastPostCreatedAt") %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="col-xs-2" ItemStyle-CssClass="pull-right">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="editButton" runat="server" CommandName="Edit" CssClass="btn btn-xs btn-warning" ToolTip="modifica denumire subiect" Visible='<%# SecurityContext.IsAdmin %>'>
                                                <i class="fa fa-pencil" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="deleteButton" runat="server" CommandName="Delete" CssClass="btn btn-xs btn-danger" ToolTip="sterge subiect" Visible='<%# SecurityContext.IsAdmin %>'>
                                                <i class="fa fa-trash" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="updateButton" runat="server" CommandName="Update" CssClass="btn btn-xs btn-success" ToolTip="Update">
                                            <i class="fa fa-check" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="cancelButton" runat="server" CommandName="Cancel" CssClass="btn btn-xs btn-danger" ToolTip="Cancel">
                                            <i class="fa fa-times" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" />
                            <PagerStyle CssClass="grid-pagination" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <EditItemTemplate>
            <div class="category-wrapper panel panel-default">
                <div class="category-info panel-heading">
                    <asp:HiddenField ID="id" Value='<%# Eval("Id") %>' Visible="false" runat="server" />
                    <div class="input-group">
                        <asp:TextBox ID="name" runat="server" Text='<%# Eval("Name") %>' CssClass="form-control input-sm" />
                        <span class="input-group-btn">
                            <asp:LinkButton ID="updateButton" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success" ToolTip="Update">
                                <i class="fa fa-check" aria-hidden="true"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="cancelButton" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-danger" ToolTip="Cancel">
                              <i class="fa fa-times" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </span>
                    </div>
                </div>
            </div>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>
