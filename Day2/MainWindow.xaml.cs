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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;

namespace Day2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DefaultMessageButton_Click(new object(), new RoutedEventArgs());
            ShowMessageActionCheckBox.IsChecked = true;
            ShowProgramActionCheckBox.IsChecked = true;
            EnableMessageActionCheckBox.IsChecked = true;
            EnableProgramActionCheckBox.IsChecked = true;
        }

        private void ShowMessageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(MessageTextBox.Text, Title);
        }

        private void DefaultMessageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Text = "Place a message here";
        }

        private void ClearMessageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Text = "";
        }

        private void RunProgramButton_Click(object sender, RoutedEventArgs e)
        {
            string selection = (ProgramToRunComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            switch (selection)
            {
                case "Notepad":
                    selection = "notepad.exe";
                    break;
                case "Paint":
                    selection = "mspaint.exe";
                    break;
                default:
                    selection = ChooseAProgram();
                    break;
            }

            if (selection != null)
                Process.Start(selection);
        }

        private static string ChooseAProgram()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DereferenceLinks = true,
                DefaultExt = "*.exe",
                Filter = "Executable programs|*.exe;*.com;*.bat|All Files|*.*"
            };
            return dialog.ShowDialog() ?? false ? dialog.FileName : null;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EnableMessageActionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageTextBox.IsEnabled = true;
            ShowMessageButton.IsEnabled = true;
            DefaultMessageButton.IsEnabled = true;
            ClearMessageButton.IsEnabled = true;
            EnterAMessageTextBlock.IsEnabled = true;
        }

        private void EnableMessageActionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageTextBox.IsEnabled = false;
            ShowMessageButton.IsEnabled = false;
            DefaultMessageButton.IsEnabled = false;
            ClearMessageButton.IsEnabled = false;
            EnterAMessageTextBlock.IsEnabled = false;
        }

        private void ShowMessageActionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Visibility = Visibility.Visible;
            ShowMessageButton.Visibility = Visibility.Visible;
            DefaultMessageButton.Visibility = Visibility.Visible;
            ClearMessageButton.Visibility = Visibility.Visible;
            EnterAMessageTextBlock.Visibility = Visibility.Visible;
        }

        private void ShowMessageActionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Visibility = Visibility.Hidden;
            ShowMessageButton.Visibility = Visibility.Hidden;
            DefaultMessageButton.Visibility = Visibility.Hidden;
            ClearMessageButton.Visibility = Visibility.Hidden;
            EnterAMessageTextBlock.Visibility = Visibility.Hidden;
        }

        private void EnableProgramActionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RunAProgramTextBlock.IsEnabled = true;
            ProgramToRunComboBox.IsEnabled = true;
            RunProgramButton.IsEnabled = true;
        }

        private void EnableProgramActionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            RunAProgramTextBlock.IsEnabled = false;
            ProgramToRunComboBox.IsEnabled = false;
            RunProgramButton.IsEnabled = false;
        }

        private void ShowProgramActionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RunAProgramTextBlock.Visibility = Visibility.Visible;
            ProgramToRunComboBox.Visibility = Visibility.Visible;
            RunProgramButton.Visibility = Visibility.Visible;
        }

        private void ShowProgramActionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            RunAProgramTextBlock.Visibility = Visibility.Hidden;
            ProgramToRunComboBox.Visibility = Visibility.Hidden;
            RunProgramButton.Visibility = Visibility.Visible;
        }
    }
}
