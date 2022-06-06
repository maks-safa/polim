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
    /// Логика взаимодействия для MaterialPage.xaml
    /// </summary>
    public partial class MaterialPage : Page
    {
        public MaterialPage()
        {
            InitializeComponent();
            var CR = ConnectBD.polimerEntities.Material.ToList();
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
           Manager.MainFrame.Navigate(new AddRedacMaterialPage((sender as Button).DataContext as Model.Material));
        }

        private void BYdal_Click(object sender, RoutedEventArgs e)
        {
            // удаление из БД данных
            var Del = (sender as Button).DataContext as Model.Material;
            var PostavHaYch = ConnectBD.polimerEntities.PostavHaYchet.ToList();
            var Ctell = ConnectBD.polimerEntities.Ctelag.ToList();
            var Cpicanie = ConnectBD.polimerEntities.Cpisanie.ToList();

            if (MessageBox.Show($"Вы точно хотите удалить. Все связанные поставки на учет, списания и стеллажи с этим материалом будут удалены", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectBD.polimerEntities.Material.Remove(Del);
                    if (PostavHaYch.Find(p => p.IdMaterial == Del.IdMaterial) != null)
                    {
                        var DelPostavHaYch = PostavHaYch.Where(p => p.IdMaterial == Del.IdMaterial);
                        ConnectBD.polimerEntities.PostavHaYchet.RemoveRange(DelPostavHaYch);
                        var DelCtell = Ctell.Where(p => p.IdMaterial == Del.IdMaterial);
                        ConnectBD.polimerEntities.Ctelag.RemoveRange(DelCtell);
                        foreach(var i in DelCtell)
                        {
                            var DelPostavCtell = Ctell.Where(p => p.IdCtelag == i.IdCtelag);
                            ConnectBD.polimerEntities.Ctelag.RemoveRange(DelPostavCtell);
                        }
                       
                        
                        var DelCpicanie = Cpicanie.Where(p => p.IdMaterial == Del.IdMaterial);
                        ConnectBD.polimerEntities.Cpisanie.RemoveRange(DelCpicanie);
                    }


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
            var CR = ConnectBD.polimerEntities.Material.ToList();

            if (CBTip.SelectedIndex > 0)
            {
                CR = CR.Where(p => p.Tip.Equals(CBTip.SelectedItem as Model.Tip)).ToList();
            }
            if (CBEdIzmer.SelectedIndex > 0)
            {
                CR = CR.Where(p => p.EdIzmer.Equals(CBEdIzmer.SelectedItem as Model.EdIzmer)).ToList();
            }
            CR = CR.Where(p => p.Наименование.ToLower().Contains(TBSearch.Text.ToLower())).ToList();

            LV.ItemsSource = CR;
        }
        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

   
        private void BDobav_Click(object sender, RoutedEventArgs e)
        {
           Manager.MainFrame.Navigate(new AddRedacMaterialPage(null));
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
