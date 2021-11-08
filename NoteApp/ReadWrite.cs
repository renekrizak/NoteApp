using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using NoteApp;
using System.Configuration;

namespace NoteApp
{
    class ReadWrite
    {
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

   /*     private void CreateLabel()
        {
            //Initializes connection to sql connection
            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
            SQLiteCommand cmd = conn.CreateCommand();
            conn.Open();

            Label noteLabel = new Label();
            Style style = FindResource("NoteTitleWithBorder");
        } */

        /*
        private string ReadTitle(string id)
        {
            
            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
            SQLiteCommand cmd = conn.CreateCommand();
            conn.Open();

            SQLiteDataReader read;
            cmd.CommandText = "Select ID, Title FROM NOTE WHERE ID=" + id;
            read = cmd.ExecuteReader();
            for(int i = 0; i < )
        }
        */
    }
}
