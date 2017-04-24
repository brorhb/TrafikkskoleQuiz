using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

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
            loadQuestions();
        }

        private void loadQuestions()
        {
            object username = Session["UserAuthentication"].ToString();
            MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
            MySqlDataReader reader = null;
            try
            {
                string questionSql = "SELECT * FROM questions ORDER BY RAND() LIMIT 10;";
                MySqlCommand cmd = new MySqlCommand(questionSql, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Label dynamicLabel = new Label();
                    questions.Controls.Add(dynamicLabel);
                    dynamicLabel.Text = reader.GetString("question");
                }
            }
            catch
            {

            }
            finally
            {

            }
        }
    }
}