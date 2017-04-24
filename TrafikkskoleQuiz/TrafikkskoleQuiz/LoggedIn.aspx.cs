﻿using System;
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
            loadLastScore();
        }

        private void loadLastScore()
        {
            object username = Session["UserAuthentication"].ToString();
            MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
            MySqlDataReader reader = null;

            try
            {
                string sql = "SELECT lastScore FROM users WHERE username = '" + username + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lastScoreLabel.Text = reader.GetString("lastScore");
                }
            }
            catch
            {
                lastScoreLabel.Text = "0";
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn != null) conn.Close();
            }
        }

        private void loadUsername()
        {
            object username = Session["UserAuthentication"];
            usernameLabel.Text = username.ToString();
        }

        private void loadHighscore()
        {
            object username = Session["UserAuthentication"].ToString();
            MySqlConnection conn = new MySqlConnection("Database=trafikkskole; Data Source=localhost;User Id=root; Password=;");
            MySqlDataReader reader = null;

            try
            {
                string sql = "SELECT highscore FROM users WHERE username = '" + username + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    highscoreLabel.Text = reader.GetString("highscore");
                }
            }
            catch
            {
                highscoreLabel.Text = "0";
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn != null) conn.Close();
            }
        }

        protected void startQuizButton_Click(object sender, EventArgs e)
        {

        }

        protected void logOutButton_Click(object sender, EventArgs e)
        {
            Session["UserAuthentication"] = null;
            Response.Redirect("Default.aspx");
        }


    }
}