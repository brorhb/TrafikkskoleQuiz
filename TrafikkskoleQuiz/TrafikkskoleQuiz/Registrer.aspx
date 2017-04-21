<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrer.aspx.cs" Inherits="TrafikkskoleQuiz.Registrer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrering</title>
    <style type="text/css">
        
        .auto-style2 {
            width: 72px;
        }

        .auto-style3 {
            width: 72px;
            text-align: right;
        }

        .auto-style4 {
            width: 191px;
        }
        .auto-style5 {
            width: 72px;
            text-align: left;
            height: 23px;
        }
        .auto-style6 {
            width: 191px;
            height: 23px;
        }
        .auto-style7 {
            height: 23px;
            width: 139px;
        }
        .auto-style8 {
            width: 72px;
            text-align: left;
        }
        .auto-style9 {
            left: 0px;
            top: 0px;
            margin-top:80px;
        }
        .auto-style10 {
            width: 139px;
        }

    </style>


    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

</head>
<body>

    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="Default.aspx">Verdens Beste Trafikkskole</a>
            </div>
            <div id="navbar" class="navbar-collapse collapse">
                <form class="navbar-form navbar-right">
                    <div class="form-group">
                        <input type="text" placeholder="Email" class="form-control">
                    </div>
                    <div class="form-group">
                        <input type="password" placeholder="Password" class="form-control">
                    </div>
                    <button type="submit" class="btn btn-success">Sign in</button>
                </form>
            </div>
            <!--/.navbar-collapse -->
        </div>
    </nav>
    <div class="container-fluid">
        <div class="jumbotron">
            <form id="form1" runat="server">
                <table class="auto-style9">
                    <tr>
                        <td class="auto-style5">Username </td>
                        <td class="auto-style6">
                            <asp:TextBox ID="UsernameTextBox" runat="server" Width="180px"></asp:TextBox>
                        </td>
                        <td class="auto-style7">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Not a valid username" ForeColor="Red" ControlToValidate="UsernameTextBox"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style8">Password </td>
                        <td class="auto-style4">
                            <asp:TextBox ID="PasswordTextBox" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
                        </td>
                        <td class="auto-style10">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Not a valid password" ForeColor="Red" ControlToValidate="PasswordTextBox"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style10">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style10">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td class="auto-style4">
                            <asp:Button ID="registrerButton" runat="server" OnClick="registrerButton_Click" Text="Submit" />
                            <input id="Reset1" type="reset" value="reset" /></td>
                        <td class="auto-style10">&nbsp;</td>
                </table>
                <div>
                </div>
            </form>
        </div>
    </div>

</body>
</html>
