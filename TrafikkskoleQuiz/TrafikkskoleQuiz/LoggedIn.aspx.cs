using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace TrafikkskoleQuiz
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAuthentication"] == null )
            {
                Response.Redirect("Default.aspx");
            }
            loadUsername();
            loadHighscore();
        }

        private void loadUsername()
        {
            object username = Session["UserAuthentication"];
            usernameLabel.Text = username.ToString();
        }

        private void loadHighscore()
        {
            object username = Session["UserAuthentication"].ToString();
            using (MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;"))
            using (MySqlCommand cmd = new MySqlCommand("SELECT highscore FROM users WHERE username = " + username + ";"))
            {
                
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();
                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            highscoreLabel.Text = reader["highscore"].ToString();
                        }
                    }
                }
                catch (Exception e) 
                {
                    highscoreLabel.Text = "0";
                    Response.Write("<script>alert" + e + "</script>");
                }
            }
        }

        protected void logOutButton_Click(object sender, EventArgs e)
        {
            Session["UserAuthentication"] = null;
            Response.Redirect("Default.aspx");
        }
    }
}