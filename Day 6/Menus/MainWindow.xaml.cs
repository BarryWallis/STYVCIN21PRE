using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand HelloCommand = new RoutedCommand();
        public static RoutedCommand ExitCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ExecutedHelloCommand(object sender, ExecutedRoutedEventArgs e) => MessageBox.Show("Hello there!", "Hello");

        private void CanExecuteHelloCommand(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        private void ExecutedExitCommand(object sender, ExecutedRoutedEventArgs e) => Close();

        private void CanExecuteExitCommand(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
    }
}
