﻿using System;
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
    /// Логика взаимодействия для CtelagPage.xaml
    /// </summary>
    public partial class CtelagPage : Page
    {
        public CtelagPage()
        {
            InitializeComponent();
            var CR = ConnectBD.polimerEntities.Ctelag.ToList();
            var Tip = ConnectBD.polimerEntities.Tip.ToList();
            var EdIzmer = ConnectBD.polimerEntities.EdIzmer.ToList();

            Tip.Insert(0, new Model.Tip
            {
                Наименование = "Отсутствует"
            });
            EdIzmer.Insert(0, new Model.EdIzmer
            {
                Наименование = "Отсутствует"
            });
            CBTip.ItemsSource = Tip;
            CBTip.SelectedIndex = 0;
            CBEdIzmer.ItemsSource = EdIzmer;
            CBEdIzmer.SelectedIndex = 0;
            LV.ItemsSource = CR;
        }
        private void BRed_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRedacCtelagPage((sender as Button).DataContext as Model.Ctelag));
        }

        private void BYdal_Click(object sender, RoutedEventArgs e)
        {
            // удаление из БД данных
            var Del = (sender as Button).DataContext as Model.Ctelag;
            var PostavHaYch = ConnectBD.polimerEntities.PostavHaYchet.ToList();

            if (MessageBox.Show($"Вы точно хотите удалить. Все поставки на учет связанные с этим стеллажом будут удалены", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectBD.polimerEntities.Ctelag.Remove(Del);
                    if (PostavHaYch.Find(p => p.IdCtelag == Del.IdCtelag) != null)
                    {
                        var DelPostavHaYch = PostavHaYch.Where(p => p.IdCtelag == Del.IdCtelag);
                        ConnectBD.polimerEntities.PostavHaYchet.RemoveRange(DelPostavHaYch);
                    }

                    ConnectBD.polimerEntities.SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    var CR = ConnectBD.polimerEntities.Ctelag.ToList();
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
            var CR = ConnectBD.polimerEntities.Ctelag.ToList();
            if (CBTip.SelectedIndex > 0)
            {
                CR = CR.Where(p => p.Material.Tip.Equals(CBTip.SelectedItem as Model.Tip)).ToList();
            }
            if (CBEdIzmer.SelectedIndex > 0)
            {
                CR = CR.Where(p => p.Material.EdIzmer.Equals(CBEdIzmer.SelectedItem as Model.EdIzmer)).ToList();
            }
            CR = CR.FindAll(p => p.Material.Наименование.ToLower().Contains(TBSearch.Text.ToLower())).ToList();

            LV.ItemsSource = CR;
        }
        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void BDobav_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRedacCtelagPage(null));
        }

        private void BNazad_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CpravochPage());
        }

        private void CBTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
