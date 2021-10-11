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
using Dapper;
using NoteApp;
using System.Configuration;
using System.Data;

namespace NoteApp
{
    /// <summary>
    /// Interaction logic for MakeNewNote_Window.xaml
    /// </summary>
    public partial class MakeNewNote_Window : Window
    {
        public MakeNewNote_Window()
        {
            InitializeComponent();
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }


        private void SaveNoteButtonEvent(object sender, RoutedEventArgs e)
        {       
            SQLiteConnection conn;
            conn = new SQLiteConnection(LoadConnectionString());  //funkcny zapis do databazy
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "INSERT INTO Note(Title, Text) VALUES (@title, @text)";
            cmd.Parameters.Add(new SQLiteParameter("@title", TitleTextInput.Text));
            cmd.Parameters.Add(new SQLiteParameter("@text", NoteTextInput.Text));
            cmd.ExecuteNonQuery();
            ReadData(conn);

        }

        /* Definicia db tabulky
        CREATE TABLE "Note" (
	        "ID"	INTEGER NOT NULL UNIQUE,
	        "Title"	TEXT NOT NULL UNIQUE,
	        "Text"	TEXT,
	        PRIMARY KEY("ID" AUTOINCREMENT)
        ); */

        public void ReadData(SQLiteConnection conn)             //read z databazy
        {
            SQLiteDataReader read;
            SQLiteCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Note";
            

            read = cmd.ExecuteReader();
            while(read.Read())
            {
                int idTest = read.GetInt32(0);                  //Precita to data na danom indexe, cize napr ID je index[0
                string Title = read.GetString(1);
                string noteText = read.GetString(2);
                testLbl.Content = $"{idTest} {Title} \n {noteText}";
            }
            conn.Close();
        }
    }
}
    