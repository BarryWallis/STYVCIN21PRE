using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Dialogs
{
    /// <summary>
    /// Interaction logic for AbortRetryIgnoreDialog.xaml
    /// </summary>
    public partial class AbortRetryIgnoreDialog : Window, INotifyPropertyChanged
    {
        private AbortRetryIgnoreDialogButton buttonClicked;

        private string _caption;
        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                NotifyPropertyChanged();
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }

        public AbortRetryIgnoreDialog()
        {
            InitializeComponent();

            DataContext = this;
        }

        public AbortRetryIgnoreDialogButton Show(string message, string caption)
        {
            Message = message;
            Caption = caption;
            if (ShowDialog() ?? false)
                return buttonClicked;
            else
                return AbortRetryIgnoreDialogButton.Cancel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            buttonClicked = AbortRetryIgnoreDialogButton.Abort;
            DialogResult = true;
        }

        private void RetryButton_Click(object sender, RoutedEventArgs e)
        {
            buttonClicked = AbortRetryIgnoreDialogButton.Retry;
            DialogResult = true;
        }

        private void IgnoreButton_Click(object sender, RoutedEventArgs e)
        {
            buttonClicked = AbortRetryIgnoreDialogButton.Ignore;
            DialogResult = true;
        }
    }
}
