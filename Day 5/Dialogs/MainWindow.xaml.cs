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
using Microsoft.Win32;

namespace Dialogs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MessageDialog messageDialog = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) => Close();

        private void YesNoCancelButton_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Press the Yes, No or Cancel button", "Yes, No, Cancel Dialog", MessageBoxButton.YesNoCancel))
            {
                case MessageBoxResult.None:
                    Debug.Fail("Unexpected MessageBoxResult: None");
                    break;
                case MessageBoxResult.OK:
                    Debug.Fail("Unexpected MessageBoxResult: OK");
                    break;
                case MessageBoxResult.Cancel:
                    DialogResultsTextBox.Text = "Sorry, cancelled.";
                    break;
                case MessageBoxResult.Yes:
                    DialogResultsTextBox.Text = "Yes! Yes! Yes!";
                    break;
                case MessageBoxResult.No:
                    DialogResultsTextBox.Text = "No, no, no, no, no.";
                    break;
                default:
                    Debug.Fail("Unexpected MessageBoxResult");
                    break;
            }
        }

        private void AbortRetryIgnoreButton_Click(object sender, RoutedEventArgs e)
        {
            AbortRetryIgnoreDialog dialog = new AbortRetryIgnoreDialog();
            switch (dialog.Show("Press the Abort, Retry, Ignore or Cancel buttons", "Abort, Retry, Ignore Dialog"))
            {
                case AbortRetryIgnoreDialogButton.Abort:
                    DialogResultsTextBox.Text = "Abort";
                    break;
                case AbortRetryIgnoreDialogButton.Retry:
                    DialogResultsTextBox.Text = "Retry";
                    break;
                case AbortRetryIgnoreDialogButton.Ignore:
                    DialogResultsTextBox.Text = "Ignore";
                    break;
                case AbortRetryIgnoreDialogButton.Cancel:
                    DialogResultsTextBox.Text = "Cancel";
                    break;
                default:
                    Debug.Fail("Unexpected dialog result");
                    break;
            }
        }

        private void FileOpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                DialogResultsTextBox.Text = openFileDialog.FileName;
        }

        private void CustomDialogButton_Click(object sender, RoutedEventArgs e)
        {
            messageDialog = new MessageDialog();
            if (messageDialog.ShowDialog() == true)
            {
                DialogResultsTextBox.Text = messageDialog.MessageTextBox.Text;
                WhichOptionButton.IsEnabled = true;
                YesOrNoButton.IsEnabled = true;
            }
        }

        private void WhichOptionButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.Assert(messageDialog != null);

            switch (messageDialog.OptionButtonChecked)
            {
                case 1:
                    DialogResultsTextBox.Text = "The first option was selected.";
                    break;
                case 2:
                    DialogResultsTextBox.Text = "The second option was selected.";
                    break;
                case 3:
                    DialogResultsTextBox.Text = "The third option was selected.";
                    break;
                case 4:
                    DialogResultsTextBox.Text = "The fourth option was selected.";
                    break;
                default:
                    break;
            }
        }

        private void YesOrNoButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.Assert(messageDialog != null);

            DialogResultsTextBox.Text = messageDialog.MessageBoxResult.ToString();
        }
    }
}
