using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace TrafikkskoleQuiz
{
    public partial class Registrer : System.Web.UI.Page
    {

        MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void registrerButton_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO users (username, password, highscore, lastScore) VALUES(@username, @password, NULL, NULL);";
                cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                cmd.Parameters.AddWithValue("@password", PasswordTextBox.Text);
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