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
    /// Логика взаимодействия для PolychatPage.xaml
    /// </summary>
    public partial class PolychatPage : Page
    {
        public PolychatPage()
        {
            InitializeComponent(); var CR = ConnectBD.polimerEntities.Polychat.ToList();

            LV.ItemsSource = CR;
        }
        private void BRed_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRedacPolychatPage((sender as Button).DataContext as Model.Polychat));
        }

        private void BYdal_Click(object sender, RoutedEventArgs e)
        {
            //// удаление из БД данных
            //var Del = (sender as Button).DataContext as Model.Polychat;
            //var Ho = ConnectBD.polimerEntities.Material.ToList();

            //if (MessageBox.Show($"Вы точно хотите удалить. Все связанные материалы с этой единицей измерения будут удалены", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //{
            //    try
            //    {
            //        ConnectBD.polimerEntities.Polychat.Remove(Del);
            //        if (Ho.Find(p => p.Polychat == Del.IdEdIzamer) != null)
            //        {
            //            var asd = Ho.Where(p => p.Polychat == Del.Polychat);
            //            ConnectBD.polimerEntities.Polychat.RemoveRange(asd);
            //        }
            //        ConnectBD.polimerEntities.SaveChanges();
            //        MessageBox.Show("Данные удалены!");

            //        var CR = ConnectBD.polimerEntities.Polychat.ToList();
            //        LV.ItemsSource = CR;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString());
            //    }
            //};
        }

        private void Update()
        {
            var CR = ConnectBD.polimerEntities.Polychat.ToList();
            CR = CR.Where(p => p.Наименование.ToLower().Contains(TBSearch.Text.ToLower())).ToList();

            LV.ItemsSource = CR;
        }
        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void BDobav_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRedacPolychatPage(null));
        }

        private void BNazad_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CpravochPage());
        }
    }
}
