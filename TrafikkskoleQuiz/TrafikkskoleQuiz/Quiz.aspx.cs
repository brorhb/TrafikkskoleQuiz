using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrafikkskoleQuiz
{
    public partial class WebForm2 :  WebForm1
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAuthentication"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            loadLastScore();
        }
    }
}