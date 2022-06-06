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
    /// Логика взаимодействия для AddRedacPoctavzikPage.xaml
    /// </summary>
    public partial class AddRedacPoctavzikPage : Page
    {
        private Model.Poctavzik _currentPoctavzik = new Model.Poctavzik();
        public AddRedacPoctavzikPage(Model.Poctavzik selectedPoctavzik)
        {
            InitializeComponent();
            // принятие значений если они есть
            if (selectedPoctavzik != null)
            {
                _currentPoctavzik = selectedPoctavzik;
            }

            DataContext = _currentPoctavzik;
        }
        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка и сохранение данных
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentPoctavzik.Наименование))
                errors.AppendLine("Укажите наименование");
            else if (_currentPoctavzik.Наименование.Length > 200)
                errors.AppendLine("В поле наименование введено больше 200 символов");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentPoctavzik.IdPoctav == 0)
                ConnectBD.polimerEntities.Poctavzik.Add(_currentPoctavzik);

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

        private void BNazad_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
