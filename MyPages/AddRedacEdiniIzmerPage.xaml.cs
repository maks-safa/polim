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
    /// Логика взаимодействия для AddRedacEdiniIzmerPage.xaml
    /// </summary>
    public partial class AddRedacEdiniIzmerPage : Page
    {
        private Model.EdIzmer _currentEdIzmer = new Model.EdIzmer();
        public AddRedacEdiniIzmerPage(Model.EdIzmer selectedEdIzmer)
        {
            InitializeComponent();
            // принятие значений если они есть
            if (selectedEdIzmer != null)
            {
                _currentEdIzmer = selectedEdIzmer;
            }

            DataContext = _currentEdIzmer;
        }
        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка и сохранение данных
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentEdIzmer.Наименование))
                errors.AppendLine("Укажите наименование");
            else if (_currentEdIzmer.Наименование.Length > 50)
                errors.AppendLine("В поле наименование введено больше 50 символов");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentEdIzmer.IdEdIzamer == 0)
                ConnectBD.polimerEntities.EdIzmer.Add(_currentEdIzmer);

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
