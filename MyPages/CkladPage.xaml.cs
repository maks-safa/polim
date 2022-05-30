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
    /// Логика взаимодействия для CkladPage.xaml
    /// </summary>
    public partial class CkladPage : Page
    {
        public CkladPage()
        {
            InitializeComponent();
            var CR = ConnectBD.polimerEntities.Cklad.ToList();

            LV.ItemsSource = CR;
        }
        private void BRed_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRedacCkladPage((sender as Button).DataContext as Model.Cklad));
        }

        private void BYdal_Click(object sender, RoutedEventArgs e)
        {
            // удаление из БД данных
            var Del = (sender as Button).DataContext as Model.Cklad;
            var Ho = ConnectBD.polimerEntities.Cklad.ToList();

            if (MessageBox.Show($"Вы точно хотите удалить. Все связанные материалы с этим складом будут удалены", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectBD.polimerEntities.Cklad.Remove(Del);
                    var asd = Ho.Where(p => p.IdCklad == Del.IdCklad);



                    ConnectBD.polimerEntities.SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    var CR = ConnectBD.polimerEntities.Cklad.ToList();
                    LV.ItemsSource = CR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            };
        }

        private void Update()
        {
            var CR = ConnectBD.polimerEntities.Cklad.ToList();
            CR = CR.FindAll(p => p.Наименование.ToLower().Contains(TBSearch.Text.ToLower())).ToList();

            LV.ItemsSource = CR;
        }
        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void BDobav_Click(object sender, RoutedEventArgs e)
        {
           Manager.MainFrame.Navigate(new AddRedacCkladPage(null));
        }
    }
}
