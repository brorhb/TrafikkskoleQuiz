﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Data;

namespace TrafikkskoleQuiz
{
    public partial class WebForm2 :  WebForm1
    {
        ManualResetEvent mre = new ManualResetEvent(false);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAuthentication"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            loadQuestions();
            loadLastScore();
        }

        private void loadQuestions()
        {
            object username = Session["UserAuthentication"].ToString();
            MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
            MySqlDataReader readerQ = null;
            
            HtmlGenericControl liq = new HtmlGenericControl("br");
            try
            {
                string questionSql = "SELECT * FROM questions ORDER BY RAND() LIMIT 1;";
                MySqlCommand cmdQ = new MySqlCommand(questionSql, conn);
                conn.Open();

                readerQ = cmdQ.ExecuteReader();

                while (readerQ.Read())
                {
                    questions.Controls.Add(liq);
                    Label questionLabel = new Label();
                    questions.Controls.Add(questionLabel);
                    string questionID = readerQ.GetString("questionID");
                    questionLabel.Text = readerQ.GetString("question");
                    loadAnswer(questionID);
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
        }

        protected void loadAnswer(string questionID)
        {
            MySqlDataReader readerA;
            MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
            try
            {
                string answerSQL = "SELECT * FROM answer WHERE questionID = @questionID ORDER BY RAND() LIMIT 4;";
                MySqlCommand cmdA = new MySqlCommand(answerSQL, conn);
                cmdA.Parameters.AddWithValue("@questionID", questionID);
                conn.Open();
                readerA = cmdA.ExecuteReader();
                while (readerA.Read())
                {
                    Label answerLabel = new Label();
                    answer.Controls.Add(answerLabel);
                    answerLabel.Text = readerA.GetString("answer");
                    answer.Controls.Add(new Literal() { ID = "row", Text = "<br/>" });
                }
            }
            catch
            {

            }
            
        }

        protected void nextButton_Click(object sender, EventArgs e)
        {
        }
    }
}
