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

            LV.ItemsSource = CR;
        }
        private void BRed_Click(object sender, RoutedEventArgs e)
        {
           Manager.MainFrame.Navigate(new AddRedacMaterialPage((sender as Button).DataContext as Model.Material));
        }

        private void BYdal_Click(object sender, RoutedEventArgs e)
        {
            //// удаление из БД данных
            //var Del = (sender as Button).DataContext as Model.Material;
            //var Ho = ConnectBD.polimerEntities.CpicokZakaz.ToList();
            //var Ho2 = ConnectBD.polimerEntities.Zakaz.ToList();

            //if (MessageBox.Show($"Вы точно хотите удалить. Все связанные заказы с этим материалом будут удалены", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //{
            //    try
            //    {
            //        ConnectBD.polimerEntities.Material.Remove(Del);
            //        if (Ho.Find(p => p.IdMaterial == Del.IdMaterial) != null)
            //        {
            //            var delCpiZak = Ho.Where(p => p.IdMaterial == Del.IdMaterial);
            //            ConnectBD.polimerEntities.CpicokZakaz.RemoveRange(delCpiZak);

            //            var cpiDelZakaz = Ho.Find(p => p.IdMaterial == Del.IdMaterial);
            //            var delZakaz = Ho2.Where(p => p.IdZakaz == cpiDelZakaz.IdZakaz);
            //            ConnectBD.polimerEntities.Zakaz.RemoveRange(delZakaz);
            //        }


            //        ConnectBD.polimerEntities.SaveChanges();
            //        MessageBox.Show("Данные удалены!");

            //        var CR = ConnectBD.polimerEntities.Material.ToList();
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
            var CR = ConnectBD.polimerEntities.Material.ToList();
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
    }
}
