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
    /// Логика взаимодействия для AddRedacCtelagPage.xaml
    /// </summary>
    public partial class AddRedacCtelagPage : Page
    {
        private Model.Ctelag _currentCtelag = new Model.Ctelag();
        public AddRedacCtelagPage(Model.Ctelag selectedCtelag)
        {
            InitializeComponent();
            // принятие значений если они есть
            if (selectedCtelag != null)
            {
                _currentCtelag = selectedCtelag;
            }

            var listCklad = ConnectBD.polimerEntities.Cklad.ToList();
            var listMaterial = ConnectBD.polimerEntities.Material.ToList();
    

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
            listCklad.Insert(0, new Model.Cklad
            {
                Наименование = "Отсутствует"
            });
            listMaterial.Insert(0, new Model.Material
            {
                Наименование = "Отсутствует"
            });

            // Передача ComboBox данных
            CBCkald.ItemsSource = listCklad;
            CBCkald.SelectedIndex = 0;

            CBMaterial.ItemsSource = listMaterial;
            CBMaterial.SelectedIndex = 0;

            CBNomerCtellag.ItemsSource = nomerCtellag;
            CBNomerCtellag.SelectedIndex = 0;

            // принятие значений если они есть
            if (selectedCtelag != null)
            {
                CBNomerCtellag.SelectedIndex = Convert.ToInt32( selectedCtelag.НомерСтелажа)-1;
            }
            
            CBNomerPolki.ItemsSource = nomerPolki;
            CBNomerPolki.SelectedIndex = 0;

            // принятие значений если они есть
            if (selectedCtelag != null)
            {
                CBNomerPolki.SelectedIndex = Convert.ToInt32(selectedCtelag.НомерПолки)-1;
            }

            DataContext = _currentCtelag;
        }
        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка и сохранение данных
            StringBuilder errors = new StringBuilder();
            var s = _currentCtelag;
            if (CBCkald.SelectedIndex == 0)
            {
                errors.AppendLine("Не выбран склад");
            }
            if (CBMaterial.SelectedIndex == 0)
            {
                errors.AppendLine("Не выбран материал");
            }
            
            if(ConnectBD.polimerEntities.Ctelag.Where(p => p.НомерПолки == CBNomerPolki.Text &&
            p.НомерСтелажа == CBNomerCtellag.Text && p.IdMaterial == _currentCtelag.IdMaterial && p.Осталось == _currentCtelag.Осталось).Count() == 1)
            {
                
            }
            else if(ConnectBD.polimerEntities.Ctelag.Where(p => p.НомерПолки == CBNomerPolki.Text &&
            p.НомерСтелажа == CBNomerCtellag.Text && p.Осталось != -1).Count() > 0)
            {
                errors.AppendLine("Место занято");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentCtelag.IdCtelag == 0)
                ConnectBD.polimerEntities.Ctelag.Add(_currentCtelag);

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
