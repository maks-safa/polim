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
        public AddRedacPostavHaYchetPage(Model.PostavHaYchet selectedPostHaYchet)
        {
            InitializeComponent();
            // принятие значений если они есть
            if (selectedPostHaYchet != null)
            {
                _currentPostHaYchet = selectedPostHaYchet;
            }

            var listPostav = ConnectBD.polimerEntities.Poctavzik.ToList();
            var listMaterial = ConnectBD.polimerEntities.Material.ToList();

            listPostav.Insert(0, new Model.Poctavzik
            {
                Наименование = "Отсутствует"
            });
            listMaterial.Insert(0, new Model.Material
            {
                Наименование = "Отсутствует"
            });


            CBPostav.ItemsSource = listPostav;
            CBPostav.SelectedIndex = 0;

            CBMaterial.ItemsSource = listMaterial;
            CBMaterial.SelectedIndex = 0;

            DataContext = _currentPostHaYchet;
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка и сохранение данных
            StringBuilder errors = new StringBuilder();
            //if (string.IsNullOrWhiteSpace(_currentCtelag.Наименование))
            //    errors.AppendLine("Укажите наименование");
            //else if (_currentCtelag.Наименование.Length > 100)
            //    errors.AppendLine("В поле наименование введено больше 100 символов");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

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
