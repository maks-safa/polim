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
    /// Логика взаимодействия для PostavHaYchetPage.xaml
    /// </summary>
    public partial class PostavHaYchetPage : Page
    {
        public PostavHaYchetPage()
        {
            // инициализация
            InitializeComponent();
            var CR = ConnectBD.polimerEntities.PostavHaYchet.ToList();
            var Tip = ConnectBD.polimerEntities.Tip.ToList();
            var EdIzmer = ConnectBD.polimerEntities.EdIzmer.ToList();
            var Poctav = ConnectBD.polimerEntities.Poctavzik.ToList();

            Tip.Insert(0, new Model.Tip
            {
                Наименование = "Отсутствует"
            });
            EdIzmer.Insert(0, new Model.EdIzmer
            {
                Наименование = "Отсутствует"
            });
            Poctav.Insert(0, new Model.Poctavzik
            {
                Наименование = "Отсутствует"
            });
            CBTip.ItemsSource = Tip;
            CBTip.SelectedIndex = 0;
            CBEdIzmer.ItemsSource = EdIzmer;
            CBEdIzmer.SelectedIndex = 0;
            CBPostav.ItemsSource = Poctav;
            CBPostav.SelectedIndex = 0;
            LV.ItemsSource = CR;
        }
        private void BRed_Click(object sender, RoutedEventArgs e)
        {
            // переход на страницу добавления/редактирования
            Manager.MainFrame.Navigate(new AddRedacPostavHaYchetPage((sender as Button).DataContext as Model.PostavHaYchet));
        }

        private void BYdal_Click(object sender, RoutedEventArgs e)
        {
            // удаление из БД данных
            var Del = (sender as Button).DataContext as Model.PostavHaYchet;

            if (MessageBox.Show($"Вы точно хотите удалить. Эта поставка будет удалена, но количество материала не изменится", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectBD.polimerEntities.PostavHaYchet.Remove(Del);
                    ConnectBD.polimerEntities.SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            };
        }

        private void Update()
        {
            // обновление и поиск данных соответствующих параметрам
            var CR = ConnectBD.polimerEntities.PostavHaYchet.ToList();
            if (CBTip.SelectedIndex > 0)
            {
                CR = CR.Where(p => p.Material.Tip.Equals(CBTip.SelectedItem as Model.Tip)).ToList();
            }
            if (CBEdIzmer.SelectedIndex > 0)
            {
                CR = CR.Where(p => p.Material.EdIzmer.Equals(CBEdIzmer.SelectedItem as Model.EdIzmer)).ToList();
            }
            if (CBPostav.SelectedIndex > 0)
            {
                CR = CR.Where(p => p.IdPoctav == (CBPostav.SelectedItem as Model.Poctavzik).IdPoctav).ToList();
            }
            CR = CR.FindAll(p => p.НомерДокумента.ToString().ToLower().Contains(TBSearch.Text.ToLower())).ToList();
            LV.ItemsSource = CR;
        }
        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }   


        private void BDobav_Click(object sender, RoutedEventArgs e)
        {
            // переход на страницу добавления/редактирования
           Manager.MainFrame.Navigate(new AddRedacPostavHaYchetPage(null));
        }

        private void BNazad_Click(object sender, RoutedEventArgs e)
        {
            // назад
            Manager.MainFrame.Navigate(new YchetPage());
        }

        private void CBTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
