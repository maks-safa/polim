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
    /// Логика взаимодействия для CpisaniePage.xaml
    /// </summary>
    public partial class CpisaniePage : Page
    {
        public CpisaniePage()
        {
            InitializeComponent();
            var CR = ConnectBD.polimerEntities.Cpisanie.ToList();
            var Tip = ConnectBD.polimerEntities.Tip.ToList();
            var EdIzmer = ConnectBD.polimerEntities.EdIzmer.ToList();
            var Polych = ConnectBD.polimerEntities.Polychat.ToList();

            Tip.Insert(0, new Model.Tip
            {
                Наименование = "Отсутствует"
            });
            EdIzmer.Insert(0, new Model.EdIzmer
            {
                Наименование = "Отсутствует"
            });
            Polych.Insert(0, new Model.Polychat
            {
                Наименование = "Отсутствует"
            });
            CBTip.ItemsSource = Tip;
            CBTip.SelectedIndex = 0;
            CBEdIzmer.ItemsSource = EdIzmer;
            CBEdIzmer.SelectedIndex = 0;
            CBPolych.ItemsSource = Polych;
            CBPolych.SelectedIndex = 0;
            LV.ItemsSource = CR;
        }
        private void BRed_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRedacCpicaniePage((sender as Button).DataContext as Model.Cpisanie));
        }

        private void BYdal_Click(object sender, RoutedEventArgs e)
        {
            // удаление из БД данных
            var Del = (sender as Button).DataContext as Model.Cpisanie;
            
            if (MessageBox.Show($"Вы точно хотите удалить. Вы точно хотите удалить. Это списание будет удалена, но количество материала не изменится", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectBD.polimerEntities.Cpisanie.Remove(Del);
                    ConnectBD.polimerEntities.SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    var CR = ConnectBD.polimerEntities.Cpisanie.ToList();
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
            var CR = ConnectBD.polimerEntities.Cpisanie.ToList();
            if (CBTip.SelectedIndex > 0)
            {
                CR = CR.Where(p => p.Material.Tip.Equals(CBTip.SelectedItem as Model.Tip)).ToList();
            }
            if (CBEdIzmer.SelectedIndex > 0)
            {
                CR = CR.Where(p => p.Material.EdIzmer.Equals(CBEdIzmer.SelectedItem as Model.EdIzmer)).ToList();
            }
            if (CBPolych.SelectedIndex > 0)
            {
                CR = CR.Where(p => p.Polychat.Equals(CBPolych.SelectedItem as Model.Polychat)).ToList();
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
            Manager.MainFrame.Navigate(new AddRedacCpicaniePage(null));
        }

        private void BNazad_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new YchetPage());
        }

        private void CBTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
