﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="TrafikkskoleQuiz.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>


    <title>Quiz</title>
</head>
<body>

    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <a class="navbar-brand" href="loggedIn.aspx">Verdens beste trafikkskole</a>
            </div>
        </div>
        <!--/.nav-collapse -->
    </nav>

    <form id="form1" runat="server">
        <div class="jumbotron col-md-12">
            <div class="col-md-12">
                <div>
                    <h3>
                        <div id="questions" runat="server">
                        </div>
                    </h3>
                    <h4>
                        <div name="answer" action="Quiz.aspx.cs" id="answer" runat="server">
                        </div>
                        <br />
                        <div id="correctAnswer" runat="server">
                        </div>
                    </h4>
                </div>
                
                <asp:Button ID="nextButton" runat="server" Text="Neste" OnClick="nextButton_Click" />
                <asp:Label ID="testLabel" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
