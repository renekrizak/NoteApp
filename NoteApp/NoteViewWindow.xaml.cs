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
using System.Text.RegularExpressions;

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

        
        MainWindow win = new MainWindow();
        public static string stringID = "";

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }


        //Funkcia ktora mi premeni string z formatu Label+cislo na cisto string formatu cislo
        private static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        private void LoadNoteContent()
        {
            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
            SQLiteCommand cmd = conn.CreateCommand();
            conn.Open();
            SQLiteDataReader read;
            //Vezme mi nazov labelu a nasledne ho prekonvertuje na int
            stringID = win.getID();
            string result = GetNumbers(stringID);
            int id = 0;

            try
            {
                id = Int32.Parse(result);
            }
            catch(FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            //SQLite DB zacina od cisla 1, cize by som dostal no current row error ak by id bolo 0            
           /* if(id==0)
            {
                cmd.CommandText = "SELECT Title, Text FROM Note WHERE ID=" + id+1;
            }
            else if(id != 0)
            {
                cmd.CommandText = "SELECT Title, Text FROM Note WHERE ID=" + id+1;
            }*/

            if(id== 0)
            {
                cmd.CommandText = "SELECT Title, Text FROM Note WHERE ID=" + 1;
            }
            else
            {
                cmd.CommandText = "SELECT Title, Text FROM Note WHERE ID=" + id;
            }
                
            read = cmd.ExecuteReader();
            read.Read();
            testLabel.Content = id;
            //string title = result;
            string title = read["Title"] != null ? Convert.ToString(read["Title"]) : string.Empty;
            string content = read["Text"] != null ? Convert.ToString(read["Text"]) : string.Empty; 
            TitleLabelTextBlock.Text = title;
            ContentLabelTextBlock.Text = content;
            
        }

        private void deleteCurrentNote(object sender, RoutedEventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
            SQLiteCommand cmd = conn.CreateCommand();
            conn.Open();
            stringID = win.getID();
            string result = GetNumbers(stringID);
            int id = 0;
            try
            {
                id = Int32.Parse(result);
            }
            catch (FormatException f)
            {
                Console.WriteLine(f.Message);
            }
            cmd.CommandText = "DELETE FROM Note WHERE ID=" + id;
            cmd.ExecuteNonQuery();
            this.Close();
        }


    }
}
