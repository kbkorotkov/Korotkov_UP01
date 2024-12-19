using Korotkov_UP01.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Korotkov_UP01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new AuthorizePage());
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ReportsPage());
        }

        private void QR_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new QRCodePage());
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Requests());
        }
        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = page.Title;

            if (page is Pages.AuthorizePage)
                MenuContainer.Visibility = Visibility.Hidden;
            else MenuContainer.Visibility = Visibility.Visible;
        }
    }
}
