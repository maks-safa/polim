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
    /// Логика взаимодействия для AddRedacPostavHaYchetPage.xaml
    /// </summary>
    public partial class AddRedacPostavHaYchetPage : Page
    {
        private Model.PostavHaYchet _currentPostHaYchet = new Model.PostavHaYchet();
        private Model.Ctelag _currentCtelag = new Model.Ctelag();
        private Model.Cklad _currentCklad = new Model.Cklad();
        public AddRedacPostavHaYchetPage(Model.PostavHaYchet selectedPostHaYchet)
        {
            InitializeComponent();
            
            // принятие значений если они есть
            if (selectedPostHaYchet != null)
            {
                _currentPostHaYchet = selectedPostHaYchet;
                _currentCtelag = ConnectBD.polimerEntities.Ctelag.Find(selectedPostHaYchet.IdCtelag);
                _currentCklad = ConnectBD.polimerEntities.Cklad.Find(_currentCtelag.IdCklad);
            }

            var listPostav = ConnectBD.polimerEntities.Poctavzik.ToList();
            var listMaterial = ConnectBD.polimerEntities.Material.ToList();
            var listCklad = ConnectBD.polimerEntities.Cklad.ToList();

            List<int> nomerCtellag = new List<int>();
            List<int> nomerPolki = new List<int>();

            for (int i = 1; i < 9; i++)
            {
                nomerCtellag.Add(i);
            }
            for (int i = 1; i < 13; i++)
            {
                nomerPolki.Add(i);
            }

            listPostav.Insert(0, new Model.Poctavzik
            {
                Наименование = "Отсутствует"
            });
            listMaterial.Insert(0, new Model.Material
            {
                Наименование = "Отсутствует"
            });
            listCklad.Insert(0, new Model.Cklad
            {
                Наименование = "Отсутствует"
            });


           

            CBPostav.ItemsSource = listPostav;
            CBPostav.SelectedIndex = 0;

            CBMaterial.ItemsSource = listMaterial;
            CBMaterial.SelectedIndex = 0;

            DataContext = _currentPostHaYchet;

            CBCklad.ItemsSource = listCklad;
            //CBCklad.SelectedIndex = 0;

            if (selectedPostHaYchet != null)
            {
                CBCklad.SelectedItem = _currentCklad;
               // CBCklad.SelectedIndex = CBCklad.SelectedItem.;
            }

            CBStellag.ItemsSource = nomerCtellag;
            CBStellag.SelectedIndex = 0;

            if (selectedPostHaYchet != null)
            {
                CBStellag.SelectedIndex = Convert.ToInt32(_currentCtelag.НомерСтелажа)-1;
            }


            CBPolka.ItemsSource = nomerPolki;
            CBPolka.SelectedIndex = 0;

            if (selectedPostHaYchet != null)
            {
                CBPolka.SelectedIndex = Convert.ToInt32( _currentCtelag.НомерПолки)-1;
            }
            


        }
        private void TBKoliches_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
           
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка и сохранение данных
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TBNomerDocument.Text))
                errors.AppendLine("Укажите номер документа");
            if (CBPostav.SelectedIndex == 0)
                errors.AppendLine("Выберите поставщика");
            
            if (CBMaterial.SelectedIndex == 0)
                errors.AppendLine("Выберите материал");

            if (string.IsNullOrWhiteSpace(TBCena.Text))
                errors.AppendLine("Укажите цену");
            else if (Convert.ToInt32( TBCena.Text) > 1000000000)
                errors.AppendLine("В поле цена значение больше 1000000000");
            if (string.IsNullOrWhiteSpace(TBKolich.Text))
                errors.AppendLine("Укажите Количество");
            else if (Convert.ToInt32(TBKolich.Text) > 1000000000)
                errors.AppendLine("В поле цена значение больше 1000000000");
            if (CBCklad.SelectedIndex == 0)
                errors.AppendLine("Выберите склад");
            if (CBStellag.SelectedIndex == 0)
                errors.AppendLine("Выберите стеллаж");
            if (CBPolka.SelectedIndex == 0)
                errors.AppendLine("Выберите полку");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            //var ckald =  CBCklad.SelectedValuePath;
            //_currentCtelag.IdCklad = ckald;
            
            if (_currentCtelag.IdCtelag == 0)
                ConnectBD.polimerEntities.Ctelag.Add(_currentCtelag);

            if (_currentPostHaYchet.IdPostHaYchet == 0)
                ConnectBD.polimerEntities.PostavHaYchet.Add(_currentPostHaYchet);

            try
            {
                ConnectBD.polimerEntities.SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
