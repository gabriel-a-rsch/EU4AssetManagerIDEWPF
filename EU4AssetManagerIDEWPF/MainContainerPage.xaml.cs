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
    /// Interaction logic for MainContainerPage.xaml
    /// </summary>
    public partial class MainContainerPage : Page
    {
        public MainContainerPage()
        {
            InitializeComponent();
        }

        private void AboutApplication_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Owner = Window.GetWindow(this);
            aboutWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            aboutWindow.ShowDialog();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Are you sure you want to exit? Any unsaved changes will be lost.";
            string sCaption = "Exit";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    System.Windows.Application.Current.Shutdown();
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;

                case MessageBoxResult.Cancel:
                    /* ... */
                    break;
            }
        }
        private void UndoAction_Click(object sender, RoutedEventArgs e)
        {
            // Implement undo functionality here
        }
        private void RedoAction_Click(object sender, RoutedEventArgs e)
        {
            // Implement redo functionality here
        }
    }
}
