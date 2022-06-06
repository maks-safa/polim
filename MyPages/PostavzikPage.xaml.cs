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
    /// Логика взаимодействия для PostavzikPage.xaml
    /// </summary>
    public partial class PostavzikPage : Page
    {
        public PostavzikPage()
        {
            InitializeComponent();
            var CR = ConnectBD.polimerEntities.Poctavzik.ToList();

            LV.ItemsSource = CR;
        }
        private void BRed_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRedacPoctavzikPage((sender as Button).DataContext as Model.Poctavzik));
        }

        private void BYdal_Click(object sender, RoutedEventArgs e)
        {
            // удаление из БД данных
            var Del = (sender as Button).DataContext as Model.Poctavzik;
            var Postav = ConnectBD.polimerEntities.PostavHaYchet.ToList();

            if (MessageBox.Show($"Вы точно хотите удалить. Все связанные поставки на учет с этим поставщиком будут удалены", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectBD.polimerEntities.Poctavzik.Remove(Del);
                    if (Postav.Find(p => p.IdPoctav == Del.IdPoctav) != null)
                    {
                        var DelPostav = Postav.Where(p => p.IdPoctav == Del.IdPoctav);
                        ConnectBD.polimerEntities.PostavHaYchet.RemoveRange(DelPostav);
                    }
                    ConnectBD.polimerEntities.SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    var CR = ConnectBD.polimerEntities.Poctavzik.ToList();
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
            var CR = ConnectBD.polimerEntities.Poctavzik.ToList();
            CR = CR.Where(p => p.Наименование.ToLower().Contains(TBSearch.Text.ToLower())).ToList();

            LV.ItemsSource = CR;
        }
        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
        private void BDobav_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRedacPoctavzikPage(null));
        }

        private void BNazad_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CpravochPage());
        }
    }
}
