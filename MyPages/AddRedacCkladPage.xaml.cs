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
    /// Логика взаимодействия для AddRedacCkladPage.xaml
    /// </summary>
    public partial class AddRedacCkladPage : Page
    {
        private Model.Cklad _currentCklad = new Model.Cklad();
        public AddRedacCkladPage(Model.Cklad selectedCklad)
        {
            InitializeComponent();
            // принятие значений если они есть
            if (selectedCklad != null)
            {
                _currentCklad = selectedCklad;
            }

            DataContext = _currentCklad;
        }
        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка и сохранение данных
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentCklad.Наименование))
                errors.AppendLine("Укажите наименование");
            else if (_currentCklad.Наименование.Length > 100)
                errors.AppendLine("В поле наименование введено больше 100 символов");
            

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentCklad.IdCklad == 0)
                ConnectBD.polimerEntities.Cklad.Add(_currentCklad);

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
