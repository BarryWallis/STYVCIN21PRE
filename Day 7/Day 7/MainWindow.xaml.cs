using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

namespace Day7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fontFamilyName;

        public MainWindow()
        {
            InitializeComponent();

            FillFontsListBox();
            SetFontWeight();
            SetDisplayTextBlock();
        }

        public void FillFontsListBox()
        {
            List<System.Windows.Media.FontFamily> systemFontFamilies = Fonts.SystemFontFamilies.ToList();
            systemFontFamilies.Sort(delegate (System.Windows.Media.FontFamily x, System.Windows.Media.FontFamily y)
            {
                return x.ToString().CompareTo(y.ToString());
            });

            foreach (System.Windows.Media.FontFamily fontFamily in systemFontFamilies)
            {
                FontsListBox.Items.Add(fontFamily.Source);
            }

            FontsListBox.SelectedIndex = 0;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SampleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DisplayTextBlock != null && UseSampleTextCheckBox.IsChecked == true)
                DisplayTextBlock.Text = SampleTextBox.Text;
        }

        private void FontsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fontFamilyName = FontsListBox.SelectedItem as string;
            Debug.Assert(fontFamilyName != null);
            System.Windows.Media.FontFamily fontFamily = new System.Windows.Media.FontFamily(fontFamilyName);
            DisplayTextBlock.FontFamily = fontFamily;
            SetFontWeight();
            SetDisplayTextBlock();
        }

        private void SetFontWeight(object sender = null, RoutedEventArgs e = null) 
            => DisplayTextBlock.FontWeight = BoldCheckBox.IsChecked == true ? FontWeights.Bold : FontWeights.Normal;

        private void SetDisplayTextBlock(object sender = null, RoutedEventArgs e = null) 
            => DisplayTextBlock.Text = UseSampleTextCheckBox.IsChecked == true ? SampleTextBox.Text : fontFamilyName;
    }
}
