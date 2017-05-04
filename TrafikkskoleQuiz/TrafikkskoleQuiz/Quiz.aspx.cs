using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Data;
using System.IO;

namespace TrafikkskoleQuiz
{
    public partial class WebForm2 :  WebForm1
    {
        ManualResetEvent mre = new ManualResetEvent(false);
        int i = 0;
        public string questionID;
        string prevCorrectID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAuthentication"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if(Session["numberOfQuestionsDone"] != null)
            {
                object y = Session["numberOfQuestionsDone"];
                string numberOfQuestionsDone = y.ToString();
                i = int.Parse(numberOfQuestionsDone);
            }
            if (Session["prevCorrectAnswer"] != null)
            {
                object y = Session["prevCorrectAnswer"];
                prevCorrectID = y.ToString();
            }
            loadQuestions();
            loadLastScore();
        }

        public void loadQuestions()
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
                    questionID = readerQ.GetString("questionID");
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
            Session["prevCorrectAnswer"] = questionID;
            MySqlDataReader readerA;
            MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
            
            try
            {
                string answerSQL = "SELECT * FROM answer WHERE questionID = @questionID ORDER BY RAND();";
                MySqlCommand cmdA = new MySqlCommand(answerSQL, conn);
                cmdA.Parameters.AddWithValue("@questionID", questionID);
                conn.Open();
                readerA = cmdA.ExecuteReader();
                
                while (readerA.Read())
                {
                    int trueOrFalse = readerA.GetInt16("correct");
                    string answerString = readerA.GetString("answer");
                    ;

                    //answer.Controls.Add();

                    Label answerLabel = new Label();
                    answer.Controls.Add(answerLabel);
                    answerLabel.Text = writeRadiobutton(trueOrFalse, answerString);

                    answer.Controls.Add(new Literal() { ID = "row", Text = "<br/>" });
                    
                }
                
            }
            catch
            {

            }

        }

        private string writeRadiobutton(int trueOrFalse, string answerString)
        {
            string type = "radio";
            string name = "id";
            string value = trueOrFalse.ToString();
            StringWriter stringWriter = new StringWriter();
            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Type, type);
                writer.AddAttribute(HtmlTextWriterAttribute.Name, name);
                writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.Write(answerString);
                writer.RenderEndTag();
                string html = stringWriter.ToString();
            }
            return stringWriter.ToString();
        }

        private void quizDone()
        {
            if (i == 20)
            {
                object username = Session["UserAuthentication"].ToString();
                MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
                MySqlDataReader reader;
                try
                {
                    string sql = "SELECT lastScore FROM users WHERE username = '" + username + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    conn.Open();

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string newScore = reader.GetString("lastScore");
                        int score = int.Parse(newScore);
                        Response.Write("<script>alert('Du fikk: " + newScore + "/20 riktige')</script>");
                    }
                }
                catch
                {
                    Response.Write("<script>alert('Du fikk: 0/20 riktige')</script>");
                    
                }
            }

        }

        protected void nextButton_Click(object sender, EventArgs e)
        {
            MySqlDataReader readerA;
            MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
            string correctAnswerSql = "SELECT * FROM answer WHERE questionID = @questionID AND correct = 1;";
            MySqlCommand cmdA = new MySqlCommand(correctAnswerSql, conn);
            cmdA.Parameters.AddWithValue("@questionID", prevCorrectID);
            conn.Open();
            readerA = cmdA.ExecuteReader();
            while (readerA.Read())
            {
                Label infoLabel = new Label();
                correctAnswer.Controls.Add(infoLabel);
                infoLabel.ForeColor = System.Drawing.Color.Green;
                infoLabel.Text = "Riktig svar på forrige spørsmål var: ";
                Label correctAnswerLabel = new Label();
                correctAnswerLabel.ForeColor = System.Drawing.Color.Green;
                correctAnswer.Controls.Add(correctAnswerLabel);
                correctAnswerLabel.Text = readerA.GetString("answer");
                correctAnswer.Controls.Add(new Literal() { ID = "row", Text = "<br/>" });
            }
            i++;
            Session["numberOfQuestionsDone"] = i;
            testLabel.Text = i.ToString();
            quizDone();
        }

    }
}
