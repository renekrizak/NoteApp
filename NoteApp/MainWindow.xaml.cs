﻿using System;
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
        }
        
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        static SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
        static SQLiteCommand cmd = conn.CreateCommand();
        static SQLiteDataReader read = cmd.ExecuteReader();

        private int GetMaxTableIndex(SQLiteConnection conn, SQLiteCommand cmd, SQLiteDataReader read) 
            /*
             Finds highest index of ID in the table and returns it so that 
            i can create and fill the right ammount of labels
             */
        {
            conn.Open();
            cmd.CommandText = "SELECT * FROM Note ORDER BY ID DESC LIMIT 1";
            cmd.ExecuteScalar();
            return read.GetInt32(GetMaxTableIndex(conn, cmd, read));

        }

        private void CreateAndLoadNotes()
        {


            Label noteLabel = new Label();
            Style style = this.FindResource("NoteTitleWithBorder") as Style;
            noteLabel.Style = style;
            int numberOfLabels = GetMaxTableIndex(conn, cmd, read);
            Label[] labels = new Label[numberOfLabels];

            cmd.CommandText = "SELECT * FROM Note WHERE ID = @id";
            

            for(int i = 0; i < numberOfLabels; i++)
            {
                labels[i] = new Label();
                labels[i].Style = noteLabel.Style;
                labels[i].Content = read.GetValue(1);
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
