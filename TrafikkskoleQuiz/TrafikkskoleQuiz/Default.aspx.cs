using System;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace TrafikkskoleQuiz
{

	public partial class Default : System.Web.UI.Page
	{
        MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
        protected void loginButton_Click(object sender, EventArgs e)
        {
            string username = loginUsername.Text;
            string password = loginPassword.Text;
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM users WHERE username = @username AND password = @password;";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Session["UserAuthentication"] = username;
                Response.Write("<script>alert('Login was succsesful')</script>");
                Response.Redirect("LoggedIn.aspx");
            }
            catch (MySqlException ex)
            {
                Response.Write("<script>alert('Wrong username or password')</script>");
            }


        }
    }
}
