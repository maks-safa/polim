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
    /// Логика взаимодействия для YchetPage.xaml
    /// </summary>
    public partial class YchetPage : Page
    {
        public YchetPage()
        {
            InitializeComponent();
        }

        private void BCpican_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CpisaniePage());
        }

        private void BPostHaYch_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PostavHaYchetPage());
        }
    }
}
