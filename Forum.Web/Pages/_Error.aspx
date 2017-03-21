<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_Error.aspx.cs" Inherits="Forum.Web.Pages._Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>A aparut o eroare in timpul procesarii!!</h3>
            <h2><%= errorMessage %></h2>
        </div>
    </form>
</body>
</html>
