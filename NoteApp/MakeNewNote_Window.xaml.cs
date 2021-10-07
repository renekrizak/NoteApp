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


        private void SaveNoteButtonEvent(object sender, RoutedEventArgs e)
        {
            string id = "Default";
            string title = TitleTextInput.Text.ToString();
            string noteText = NoteTextInput.Text.ToString();
            var con = new SQLiteConnection(ConfigurationManager.ConnectionStrings[id].ConnectionString);
            con.Open();

            var cmd = new SQLiteCommand(con);

            cmd.CommandText = "INSERT INTO [ImportantNote] (Title, NoteText) VALUES ( @title , @noteText)";
            cmd.Parameters.Add(new SQLiteParameter("@Title", "kokot"));
            cmd.Parameters.Add(new SQLiteParameter("@NoteText", "funguj6"));
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}
    