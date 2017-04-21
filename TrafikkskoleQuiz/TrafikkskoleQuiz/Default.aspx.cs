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
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO users (username, password, highscore, lastScore) VALUES(@username, @password, NULL, NULL);";
                cmd.Parameters.AddWithValue("@username", loginUsername.Text);
                //cmd.Parameters.AddWithValue("@password", );
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Response.Write("<script>alert('Registraion was succsesful')</script>");
            }
            catch (MySqlException ex)
            {
                Response.Write("<script>alert('" + ex + "')</script>");
            }


        }
    }
}
