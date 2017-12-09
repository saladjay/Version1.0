using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;

namespace Version1._0
{
    public class DataBaseOperator
    {
        private SQLiteConnection _MySQLiteConnection;

        public DataBaseOperator()
        {
            ConnectDatabase();
        }

        //创建一个空的数据库
        private void ConnectDatabase()
        {
            try
            {
                Debug.WriteLine("OldDatabase");
                _MySQLiteConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
                _MySQLiteConnection.Open();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NewDatabase");
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
                _MySQLiteConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
                _MySQLiteConnection.Open();
            }
        }

        private DataTable MainConferenceTable()
        {
            string sqlSelect = "select * from MainConference order by ConferenceID";
            SQLiteCommand sqlCommand = new SQLiteCommand(sqlSelect, _MySQLiteConnection);
            try
            {
                SQLiteDataReader reader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                Debug.WriteLine("MainConference table exist");
                return dataTable;
            }
            catch
            {
                string sqlCreate = "create table MainConference (name varchar(20), ConferenceID int)";
                sqlCommand = new SQLiteCommand(sqlCreate, _MySQLiteConnection);
                sqlCommand.ExecuteNonQuery();
                Debug.WriteLine("MainConference table not exist");

                sqlCommand = new SQLiteCommand(sqlSelect, _MySQLiteConnection);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
        }

        private DataTable AllMemberTable()
        {
            string sqlSelect = "select * from AllMembers order by Name";
            SQLiteCommand sqlCommand = new SQLiteCommand(sqlSelect, _MySQLiteConnection);
            try
            {
                SQLiteDataReader reader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                Debug.WriteLine("AllMembers table exist");
                return dataTable;
            }
            catch
            {
                string sqlCreate = "CREATE table AllMembers (name varchar(20), MemberID int)";
                sqlCommand = new SQLiteCommand(sqlCreate, _MySQLiteConnection);
                sqlCommand.ExecuteNonQuery();
                Debug.WriteLine("AllMembers table not exist");

                sqlCommand = new SQLiteCommand(sqlSelect, _MySQLiteConnection);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
        }

        private DataTable GetMembersOfConference(int ConferenceID)
        {
            string sqlSelect = string.Format("select * from Conference{0} order by Name", ConferenceID);
            SQLiteCommand sqlCommand = new SQLiteCommand(sqlSelect, _MySQLiteConnection);
            try
            {
                SQLiteDataReader reader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                Debug.WriteLine("Return All Member of Conference" + ConferenceID);
                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
