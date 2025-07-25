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

namespace EU4AssetManagerIDEWPF
{
    /// <summary>
    /// Interaction logic for CountryMainMenuPage.xaml
    /// </summary>
    public partial class CountryMainMenuPage : Page
    {
        public CountryMainMenuPage()
        {
            InitializeComponent();
        }

        private void ViewCountryButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt";
            bool? result = dialog.ShowDialog();
            if (result == true) // AKA I picked a file.
            {
                string filename = dialog.FileName;
            }
        }
    }
}
