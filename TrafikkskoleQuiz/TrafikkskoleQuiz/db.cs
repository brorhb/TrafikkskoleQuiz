using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace TrafikkskoleQuiz
{
    public class db
    {
        private MySqlConnection conn;
        private MySqlDataReader reader;
        private DataSet mySqlDataset = new DataSet();
        private DataTable mySqlDataTable = new DataTable();

        string databaseString = "trafikkskole; Data Source = localhost; User Id = root; Password=;";

        //get connection string based on the url
        public db()
        {
            conn = new MySqlConnection(databaseString);
        }

        //open the db connection
        public MySqlConnection Openconn()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }

        //close the db connection
        public MySqlConnection Closeconn()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return conn;
        }

        // returns a dataset
        public DataSet returnDataSet(MySqlCommand DSCmd)
        {
            MySqlDataAdapter DSAdapter;
            try
            {
                mySqlDataset.Clear();
                DSCmd.Connection = Openconn();
                DSAdapter = new MySqlDataAdapter(DSCmd);
                DSAdapter.Fill(mySqlDataset);
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            finally
            {
                DSCmd.Connection = Closeconn();
            }
            return mySqlDataset;
        }

        public DataTable returnDataTable(MySqlCommand DTCmd)
        {
            MySqlDataAdapter DTadapter;
            try
            {
                mySqlDataTable.Clear();
                DTCmd.Connection = Openconn();
                DTadapter = new MySqlDataAdapter(DTCmd);
                DTadapter.Fill(mySqlDataTable);
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            finally
            {
                DTCmd.Connection = Closeconn();
            }
            return mySqlDataTable;
        }
    }
}