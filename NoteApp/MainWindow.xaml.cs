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


/*
    13.10.2021 - Potrebujem spravit to, ze ked kliknem na poznamku tak sa mi pravdepodobne objavi nove okno v ktorom
    bude aj content danej poznamky, dalej potrebujem spravit button ktory je schopny poznamku vymazat a upravit
    a na koniec spravit search podla titlu poznamky, popripade aj contentu danej poznamky a ukaze ju na vrchu stack panelu.

 */

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
        /* Definicia db tabulky
        CREATE TABLE "Note" (
	        "ID"	INTEGER NOT NULL UNIQUE,
	        "Title"	TEXT NOT NULL UNIQUE,
	        "Text"	TEXT,
	        PRIMARY KEY("ID" AUTOINCREMENT)
        ); */

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        private int GetMaxTableIndex() 
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

        public string FillLabelTitle(int j) //Funkcia ktora returne title danej poznamky podla ID (index j)
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
        private void CreateAndLoadNotes()
        { /* 13.10.2021
                Momentalne mi funkcia vytvori spravny pocet notes a nasledne im cez funkciu FillLabelTitle()
                nacita spravny title z databazy. Potrebujem neskor spravit to, aby notes vytvaralo asynchronne za behu
                aplikacie a nie iba pocas spustenia aplikacie.
           */
            SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());           
            SQLiteCommand cmd = conn.CreateCommand();           
            conn.Open();
           
            SQLiteDataReader read;

            Label noteLabel = new Label();          //Cast kodu ktora vytvori label + pole pre dane labels
            Style style = this.FindResource("NoteTitleWithBorder") as Style;
            noteLabel.Style = style;
            int numberOfLabels = GetMaxTableIndex();
            Label[] labels = new Label[numberOfLabels];

            cmd.CommandText = "SELECT * FROM Note";

            read = cmd.ExecuteReader();
            while (read.Read()) {                   //Vytvara labels na zaklade poctu ID's v databaze 
                for (int i = 0, j = 1; i < numberOfLabels; i++, j++)
                {
                    labels[i] = new Label();
                    labels[i].Style = noteLabel.Style;
                    //labels[i].Name = i.ToString(); //Nazov pouzijem na to, aby som vedel v NoteViewWindow loadnut spravny Note pomocou ID(i)
                    labels[i].AddHandler(Label.MouseLeftButtonUpEvent, new RoutedEventHandler(LoadNoteContent));
                    string title = FillLabelTitle(j);
                    labels[i].Content = title;
                       
                }
            }

            for (int i = 0; i < numberOfLabels; i++) //Vlozi a zobrazi labels v stackpanely NoteStackPanel
            {
                NoteStackPanel.Children.Add(labels[i]);
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)    //Zakladna drag move funkcionalita aby som vedel s aplikaciou hybat
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

        private void LoadNoteContent(object sender, RoutedEventArgs e)
        {
            NoteViewWindow showLabelContent = new NoteViewWindow();
            showLabelContent.Show();
        }


    }
}
