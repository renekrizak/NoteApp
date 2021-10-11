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
            conn = new SQLiteConnection(LoadConnectionString());
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "INSERT INTO Note(Title, Text) VALUES (@title, @text)";
            cmd.Parameters.Add(new SQLiteParameter("@title", TitleTextInput.Text));
            cmd.Parameters.Add(new SQLiteParameter("@text", NoteTextInput.Text));
            cmd.ExecuteNonQuery();


        }
    }
}
    