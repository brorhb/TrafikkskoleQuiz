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
using System.Drawing;

namespace TrafikkskoleQuiz
{
    public partial class WebForm2 :  WebForm1
    {
        ManualResetEvent mre = new ManualResetEvent(false);
        int i = 0;
        public string questionID;
        string prevCorrectID;
        int points = 1;
        int selectedValueInt;

        Label answerLabel;

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
            if (Session["points"] != null)
            {
                object y = Session["points"];
                string pointsUntilNow = y.ToString();
                points = int.Parse(pointsUntilNow);
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
            List<RadioButton> radioButtons = new List<RadioButton>();
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

                    answerLabel = new Label();
                    answer.Controls.Add(answerLabel);
                    answerLabel.Text = writeRadiobutton(trueOrFalse, answerString);

                    /*RadioButton answerButton = new RadioButton();
                    answerButton.ID = "answer";
                    answerButton.GroupName = "answers";
                    answerButton.Text = answerString;
                    answerButton.Checked = false;
                    radioButtons.Add(answerButton);*/

                    answer.Controls.Add(new Literal() {  Text = "<br/>" });
                    
                }
                
            }
            catch
            {

            }

        }

        private bool isCorrect()
        {
            bool isUserCorrect = false;
            string selectedValue = Request.Form["answers"].ToString();
            selectedValueInt = int.Parse(selectedValue);
            if (selectedValueInt == 1)
            {
                Session["points"] = points+1;
                MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
                string updateSql = "UPDATE users SET lastScore = @plus1 WHERE username = @username;";
                MySqlCommand cmdA = new MySqlCommand(updateSql, conn);
                cmdA.Parameters.AddWithValue("@plus1", points);
                cmdA.Parameters.AddWithValue("@username", usernameString());
                conn.Open();
                int numRowsUpdated = cmdA.ExecuteNonQuery();
                cmdA.Dispose();

                isUserCorrect = true;
            }
            else
            {
                isUserCorrect = false;
            }
            return isUserCorrect;
        }

        private void updateHighscore()
        {
            if (points > int.Parse(loadHighscoreString()))
            {
                MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
                string updateSql = "UPDATE users SET highscore = @points WHERE username = @username;";
                MySqlCommand cmdA = new MySqlCommand(updateSql, conn);
                cmdA.Parameters.AddWithValue("@points", points);
                cmdA.Parameters.AddWithValue("@username", usernameString());
                conn.Open();
                int numRowsUpdated = cmdA.ExecuteNonQuery();
                cmdA.Dispose();
            }
        }

        private string writeRadiobutton(int trueOrFalse, string answerString)
        {
            string type = "radio";
            string name = "answers";
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
            if (i == 5)
            {
                updateHighscore();
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
                        Response.Write("<script>alert('Du fikk: " + newScore + "/20 riktige'); window.location.href='LoggedIn.aspx';</script>");
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
            isCorrect();
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
                correctAnswer.Controls.Add(new Literal() {  Text = "<br/>" });
            }
            i++;
            Session["numberOfQuestionsDone"] = i;
            testLabel.Text = i.ToString() + " / 20";
            quizDone();
        }

    }
}
