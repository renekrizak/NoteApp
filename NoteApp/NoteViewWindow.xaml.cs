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
    /// <summary>
    /// Interaction logic for NoteViewWindow.xaml
    /// </summary>
    public partial class NoteViewWindow : Window
    {
        public NoteViewWindow()
        {
            InitializeComponent();
            LoadNoteContent();
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }



        public void LoadNoteContent()
        {
            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
            SQLiteCommand cmd = conn.CreateCommand();
            conn.Open();
            SQLiteDataReader read;
            cmd.CommandText = "SELECT * FROM Note ";
            read = cmd.ExecuteReader();
            read.Read();
            string title = read["Title"] != null ? Convert.ToString(read["Title"]) : string.Empty;
            string content = read["Text"] != null ? Convert.ToString(read["Text"]) : string.Empty;
            TitleLabelTextBlock.Text = title;
            ContentLabelTextBlock.Text = content;
        }

    }
}
