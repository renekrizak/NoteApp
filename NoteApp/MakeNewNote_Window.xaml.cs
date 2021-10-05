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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NoteApp;
using Dapper;
using System.Data;
using System.Data.SQLite;


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
    }
}
