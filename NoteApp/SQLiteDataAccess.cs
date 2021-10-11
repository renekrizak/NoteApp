/*using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;   
using System.Threading.Tasks;
using Dapper;

namespace NoteApp
{
    public class SQLiteDataAccess
    {

        public static string LoadTitles()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var noteTitle = cnn.Query("select * from Title", new DynamicParameters());
                return noteTitle.ToString();
            }
        }

        public static string LoadNoteContent()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var noteContent = cnn.Query("select * from NoteText", new DynamicParameters());
                return noteContent.ToString();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
} */
