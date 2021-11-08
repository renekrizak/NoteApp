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
            CreateLabelAndLoadTitle();
        }

        /* Definicia db tabulky
        CREATE TABLE "Note" (
	        "ID"	INTEGER NOT NULL UNIQUE,
	        "Title"	TEXT NOT NULL UNIQUE,
	        "Text"	TEXT,
	        PRIMARY KEY("ID" AUTOINCREMENT)
        ); */
        //Connection string pre DB
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public int GetMaxTableIndex()
        /*
         Funkcia na zistenei najvacsieho IDcka v databaze, nasledne toto maxID vyuzijem na vytvorenie labelov
         */
        {
            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
            SQLiteCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = "SELECT rowid from Note order by ROWID DESC limit 1";
            int maxID = Convert.ToInt32(cmd.ExecuteScalar());
            return maxID;
        }

        /*
         Funkcia mi vytvori notes so vsetkymi potrebnymi parametrami a potom ich zobrazi na stackpanel
         */

        public void CreateLabelAndLoadTitle()
        {
            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
            SQLiteCommand cmd = conn.CreateCommand();
            conn.Open();

            Label noteLabel = new Label();
            Style style = FindResource("NoteTitleWithBorder") as Style;
            noteLabel.Style = style;
            int numberOfLabels = GetMaxTableIndex();
            Label[] labels = new Label[numberOfLabels];

            for (int i = 0, j = 1; i < numberOfLabels; i++, j++)
            {
                string labelID = "Label" + i;
                labels[i] = new Label();
                labels[i].Style = noteLabel.Style;
                labels[i].Name = labelID;
                string title = ReadLabelTitle(j);
                labels[i].Content = title;  
                string id = labels[i].Name;
                labels[i].AddHandler(Label.MouseLeftButtonUpEvent, new RoutedEventHandler(LabelClick));
            }

            for (int i = 0; i < numberOfLabels; i++)
            {
                NoteStackPanel.Children.Add(labels[i]);
            }
        }

        //   public delegate string PassLabelName(string name);

        //   public event PassLabelName StringReturnEvent;

        
        public static string labelID = "";
        public int clickCount = 0;

        /*
          Event handler na otvorenie noveho okna + odoslanie mena labelu na to,
          aby som mohol najst ktory label otvorit z databazy
         */
        private void LabelClick(object sender, EventArgs e)
        {
            NoteViewWindow showLabelContent = new NoteViewWindow();
            Label noteLabel = sender as Label;
            labelID = ((Label)sender).Name;
            if(noteLabel.Name != null)
            {
                if(clickCount != 0)
                {
                    showLabelContent.Name = ((Label)sender).Name;
                    showLabelContent.Show();
                    TestLabel.Content = labelID;
                }
                else
                {
                    clickCount++;
                }
            }
        }
        public string getID()
        {
            return labelID;
        }

        public string ReadLabelTitle(int j) //Funkcia ktora returne title danej poznamky podla ID (index j)
        {

            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
            SQLiteCommand cmd = conn.CreateCommand();
            conn.Open();
            SQLiteDataReader read;
            cmd.CommandText = "SELECT ID, Title FROM Note WHERE ID=" + j;
            read = cmd.ExecuteReader();
            read.Read();
            string title = read["Title"] != null ? Convert.ToString(read["Title"]) : string.Empty;
            return title;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)    //Zakladna drag move funkcionalita aby som vedel s aplikaciou hybat
        {
            if (e.LeftButton == MouseButtonState.Pressed)
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
