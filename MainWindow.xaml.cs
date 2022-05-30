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

namespace Polimer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyClass.ConnectBD.polimerEntities = new Model.DiplomEntities();
            NavFrame.Navigate(new MyPages.CpravochPage());
            MyClass.Manager.MainFrame = NavFrame;//навигация
            RBCprav.IsChecked = true;
        }

        private void RowDefinition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) // перемещение окна
            {
                DragMove();
            }
        }

        private void ImClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void BRazve_Click(object sender, RoutedEventArgs e)
        {

            if (WindowState == WindowState.Maximized)// полный экран или вернутся с исходному размеру экрана
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }

        }

        private void BCvern_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void RBCprav_Click(object sender, RoutedEventArgs e)
        {
            MyClass.Manager.MainFrame.Navigate(new MyPages.CpravochPage());
        }

        private void RBProgramme_Click(object sender, RoutedEventArgs e)
        {

            var window = new MyPages.AboutBox();
         
            window.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            window.Show();
        }

        private void RBYchet_Click(object sender, RoutedEventArgs e)
        {
            MyClass.Manager.MainFrame.Navigate(new MyPages.PostavHaYchetPage());

        }
    }
}
