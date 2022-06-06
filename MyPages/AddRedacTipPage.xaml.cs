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
    /// Логика взаимодействия для AddRedacTipPage.xaml
    /// </summary>
    public partial class AddRedacTipPage : Page
    {
        private Model.Tip _currentTip = new Model.Tip();
        public AddRedacTipPage(Model.Tip selectedTip)
        {
            InitializeComponent();
            // принятие значений если они есть
            if (selectedTip != null)
            {
                _currentTip = selectedTip;
            }

            DataContext = _currentTip;
        }
        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка и сохранение данных
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentTip.Наименование))
                errors.AppendLine("Укажите наименование");
            else if (_currentTip.Наименование.Length > 100)
                errors.AppendLine("В поле наименование введено больше 100 символов");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentTip.IdTip == 0)
                ConnectBD.polimerEntities.Tip.Add(_currentTip);

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
