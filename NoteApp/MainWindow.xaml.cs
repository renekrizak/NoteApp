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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            CreateAndLoadNotes();
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        private int GetMaxTableIndex() 
            /*
             Finds highest index of ID in the table and returns it so that 
            i can create and fill the right ammount of labels
             */
        {
            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
            SQLiteCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = "SELECT * FROM Note ORDER BY ID DESC LIMIT 1";
            int maxID = Convert.ToInt32(cmd.ExecuteScalar());
            return maxID;


        }

        private void CreateAndLoadNotes()
        { /* 12.10.2021
           Tato kokotina ma spravit x pocet notes podla toho kolko je rows v databaze a nasledne
            zobrazit zatial iba title danej poznamky, potrebujem poriesit aby to realne fungovalo,
            program sice necrashuje ale ked to spustim sa deje holy kokot a urcite sa nezobrazia vytvorene
            labels ktore by sa mali
           */
            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
            SQLiteCommand cmd = conn.CreateCommand();
            conn.Open();
            SQLiteDataReader read;

            Label noteLabel = new Label();
            Style style = this.FindResource("NoteTitleWithBorder") as Style;
            noteLabel.Style = style;
            int numberOfLabels = GetMaxTableIndex();
            Label[] labels = new Label[300];

            cmd.CommandText = "SELECT Title FROM Note WHERE ID = @id";
            cmd.Parameters.Add(new SQLiteParameter("@id", GetMaxTableIndex()));


            read = cmd.ExecuteReader();
            while (read.Read()) { 
                for (int i = 0; i < 300; i++)
                {
                    labels[i] = new Label();
                    labels[i].Style = noteLabel.Style;
                    string title = read["Title"] != null ? Convert.ToString(read["Title"]) : String.Empty;
                    labels[i].Content = title;
                }
            }

            for (int i = 0; i < 300; i++)
            {
                NoteStackPanel.Children.Add(labels[i]);
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void createNewNote_Click(object sender, RoutedEventArgs e) //Shows pop-up window to create & save new note.
        {
            MakeNewNote_Window newNoteWindow = new MakeNewNote_Window();

            newNoteWindow.Show();

        }


    }
}
