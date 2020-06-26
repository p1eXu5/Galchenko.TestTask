using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using p1eXu5.Wpf.MvvmBaseLibrary;

namespace Galchenko.TestTask.DesktopClient.DialogWindows
{
    /// <summary>
    /// Interaction logic for AppointmentDialogWindow.xaml
    /// </summary>
    public partial class AppointmentDialogWindow : Window, IDialog
    {
        public AppointmentDialogWindow()
        {
            InitializeComponent();
        }
    }
}
