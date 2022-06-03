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
    /// Логика взаимодействия для AddRedacMaterialPage.xaml
    /// </summary>
    public partial class AddRedacMaterialPage : Page
    {
        private Model.Material _currentCurrentMaterial = new Model.Material();
        public AddRedacMaterialPage(Model.Material selectedMaterial)
        {
            InitializeComponent();
            if (selectedMaterial != null)
            {
                _currentCurrentMaterial = selectedMaterial;
            }

            var CB = ConnectBD.polimerEntities.EdIzmer.ToList();
            var CBa = ConnectBD.polimerEntities.Tip.ToList();

            CB.Insert(0, new Model.EdIzmer
            {
                Наименование = "Отсутствует"
            });
            CBa.Insert(0, new Model.Tip
            {
                Наименование = "Отсутствует"
            });

            CBEdIZmer.ItemsSource = CB;
            CBEdIZmer.SelectedIndex = 0;

            CBTip.ItemsSource = CBa;
            CBTip.SelectedIndex = 0;

            DataContext = _currentCurrentMaterial;
        }
        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка и сохранение данных
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentCurrentMaterial.Наименование))
                errors.AppendLine("Укажите наименование");
            else if (_currentCurrentMaterial.Наименование.Length > 4000)
                errors.AppendLine("В поле наименование введено больше 4000 символов");
            if (CBEdIZmer.SelectedIndex == 0)
            {
                errors.AppendLine("Не выбрана единица измерения");
            }
            if (CBTip.SelectedIndex == 0)
            {
                errors.AppendLine("Не выбран тип материала");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentCurrentMaterial.IdMaterial == 0)
                ConnectBD.polimerEntities.Material.Add(_currentCurrentMaterial);

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
