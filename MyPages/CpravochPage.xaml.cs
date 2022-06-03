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
using Polimer.MyClass;

namespace Polimer.MyPages
{
    /// <summary>
    /// Логика взаимодействия для CpravochPage.xaml
    /// </summary>
    public partial class CpravochPage : Page
    {
        public CpravochPage()
        {
            InitializeComponent();
        }

        private void BMaterial_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MaterialPage());
        }

        private void BTip_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TipPage());
        }

        private void BEdIzmer_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EdinIzmerPage());
        }

        private void BPostav_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PostavzikPage());
        }

        private void BCklad_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CkladPage());
        }

        private void BCtelag_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CtelagPage());
        }

        private void BPolych_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PolychatPage());
        }

    }
}
