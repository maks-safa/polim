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
    /// Логика взаимодействия для AddRedacPolychatPage.xaml
    /// </summary>
    public partial class AddRedacPolychatPage : Page
    {
        private Model.Polychat _currentPolychat = new Model.Polychat();
        public AddRedacPolychatPage(Model.Polychat selectedPolychat)
        {
            InitializeComponent();
            // принятие значений если они есть
            if (selectedPolychat != null)
            {
                _currentPolychat = selectedPolychat;
            }

            DataContext = _currentPolychat;
        }
        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка и сохранение данных
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentPolychat.Наименование))
                errors.AppendLine("Укажите наименование");
            else if (_currentPolychat.Наименование.Length > 200)
                errors.AppendLine("В поле наименование введено больше 200 символов");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentPolychat.IdPolychat == 0)
                ConnectBD.polimerEntities.Polychat.Add(_currentPolychat);

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
