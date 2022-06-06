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
using Word = Microsoft.Office.Interop.Word;

namespace Polimer.MyPages
{
    /// <summary>
    /// Логика взаимодействия для AddRedacCpicaniePage.xaml
    /// </summary>
    public partial class AddRedacCpicaniePage : Page
    {
        private Model.Cpisanie _currentCpicanie = new Model.Cpisanie();
        private Model.Ctelag _currentCtelag= new Model.Ctelag();
        private Model.Material _currentMaterual = new Model.Material();
        private Model.Polychat _currentPolycha = new Model.Polychat();
        private readonly string TemplateFileName = System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Cpicanie.dotm";
        private decimal old;
        public AddRedacCpicaniePage(Model.Cpisanie selectedCpicanie)
        {
            InitializeComponent();
            // принятие значений если они есть
            if (selectedCpicanie != null)
            {
                _currentCpicanie = selectedCpicanie;
                _currentCtelag = ConnectBD.polimerEntities.Ctelag.Where(p=>p.IdCtelag == _currentCpicanie.IdCtelag).FirstOrDefault();
                _currentMaterual = ConnectBD.polimerEntities.Material.Where(p => p.IdMaterial == _currentCpicanie.IdMaterial).FirstOrDefault();
                _currentPolycha = ConnectBD.polimerEntities.Polychat.Where(p => p.IdPolychat == _currentCpicanie.IdPolychat).FirstOrDefault();
                old = _currentCpicanie.Количество;
                DPData.DisplayDate = _currentCpicanie.Дата;
            }

            var listPolych = ConnectBD.polimerEntities.Polychat.ToList();
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

            listPolych.Insert(0, new Model.Polychat
            {
                Наименование = "Отсутствует"
            });
            listMaterial.Insert(0, new Model.Material
            {
                Наименование = "Отсутствует"
            });
          
            CBPolych.ItemsSource = listPolych;
            CBPolych.SelectedIndex = 0;

            CBMaterial.ItemsSource = listMaterial;
            CBMaterial.SelectedIndex = 0;

            CBStellag.ItemsSource = nomerCtellag;
            CBStellag.SelectedIndex = 0;

           
            CBPolka.ItemsSource = nomerPolki;
            CBPolka.SelectedIndex = 0;

            DataContext = _currentCpicanie;

            if (selectedCpicanie != null)
            {
                CBMaterial.SelectedItem = _currentMaterual;
                CBMaterial.SelectedIndex = CBMaterial.Items.IndexOf(CBMaterial.SelectedItem);
                CBPolych.SelectedItem = _currentPolycha;
                CBPolych.SelectedIndex = CBPolych.Items.IndexOf(CBPolych.SelectedItem);
                CBStellag.SelectedIndex = Convert.ToInt32(_currentCtelag.НомерСтелажа) - 1;
                CBPolka.SelectedIndex = Convert.ToInt32(_currentCtelag.НомерПолки) - 1;
            }
        }
        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //Проверка и сохранение данных

            try
            {
                if (Proverka() == false)
                {
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка заполнения данных");
                return;
            }
            Model.Polychat selectedPolych = (Model.Polychat)CBPolych.SelectedItem;
            Model.Material selectedMaterial = (Model.Material)CBMaterial.SelectedItem;

            var selectCtelag = ConnectBD.polimerEntities.Ctelag.Where(p => p.НомерПолки == CBPolka.Text &&
             p.НомерСтелажа == CBStellag.Text && p.IdMaterial == selectedMaterial.IdMaterial).FirstOrDefault();

            _currentCpicanie.Дата = DPData.DisplayDate;
            _currentCpicanie.IdPolychat = selectedPolych.IdPolychat;
            _currentCpicanie.IdCtelag = selectCtelag.IdCtelag;
            _currentCpicanie.IdMaterial = selectedMaterial.IdMaterial;

            if (_currentCpicanie.IdCpisanie == 0)
            {
                selectCtelag.Осталось -= Convert.ToDecimal(TBKolich.Text);
            }
            else if (_currentCpicanie.IdCpisanie != 0)
            {
                selectCtelag.Осталось -= Convert.ToDecimal(TBKolich.Text)- old;
            }


            if (selectCtelag.IdCtelag == 0)
                ConnectBD.polimerEntities.Ctelag.Add(_currentCtelag);
            try
            {
                ConnectBD.polimerEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            if (_currentCpicanie.IdCpisanie == 0)
                ConnectBD.polimerEntities.Cpisanie.Add(_currentCpicanie);
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
        private bool Proverka()
        {
            Model.Material selectedMaterial = (Model.Material)CBMaterial.SelectedItem;
            var Ctelag = ConnectBD.polimerEntities.Ctelag.Where(p => p.НомерПолки == CBPolka.Text &&
            p.НомерСтелажа == CBStellag.Text && p.IdMaterial == selectedMaterial.IdMaterial).FirstOrDefault();

            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TBNomerDocument.Text))
                errors.AppendLine("Укажите номер документа");
            else if (Convert.ToInt32(TBNomerDocument.Text) <= 0)
            {
                errors.AppendLine("Укажите номер документа");
            }
            if (string.IsNullOrWhiteSpace(TBKolich.Text))
                errors.AppendLine("Укажите Количество");
            else if (Convert.ToDecimal(TBKolich.Text) <= 0)
                errors.AppendLine("Укажите количество");
            else if (Convert.ToDecimal(TBKolich.Text) > 1000000000)
                errors.AppendLine("В поле количество значение больше 1000000000");
            if (CBPolych.SelectedIndex == 0)
                errors.AppendLine("Выберите получателя");
            if (CBMaterial.SelectedIndex == 0)
                errors.AppendLine("Выберите материал");
            if (Ctelag == null)
            {
                errors.AppendLine("Материал не соответствует материалу на полке");
            }
            else if (Ctelag.Осталось < Convert.ToDecimal(TBKolich.Text) && _currentCpicanie.IdCpisanie == 0)
            {
                errors.AppendLine("Материала на стеллаже не достаточно");
            }
            else if (_currentCpicanie.IdCpisanie != 0 && (_currentCpicanie.Количество -= Convert.ToDecimal(TBKolich.Text) - old) < 0)
            {
                errors.AppendLine("Материала на стеллаже не достаточно");
            }
            

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return false;
            }
            return true;
        }

        private void ReplaceWordSub(string subToreplace, string text, Word.Document wordDocument)
        {
            // встака данных в Word
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: subToreplace, ReplaceWith: text);
        }

        private void BWord_Click(object sender, RoutedEventArgs e)
        {
            // экспорт в Word
            if (Proverka() == false)
            {
                return;
            }
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                var wordDocument = wordApp.Documents.Open(TemplateFileName);
                ReplaceWordSub("{IdCpisanie}", _currentCpicanie.IdCpisanie.ToString(), wordDocument);
                ReplaceWordSub("{Data}", _currentCpicanie.Дата.ToShortDateString(), wordDocument);
                ReplaceWordSub("{Cklad}", "Центральный склад", wordDocument);
                ReplaceWordSub("{Polychat}", _currentCpicanie.Polychat.Наименование, wordDocument);
                ReplaceWordSub("{NomerDoc}", _currentCpicanie.НомерДокумента.ToString(), wordDocument);
                ReplaceWordSub("{Material}", _currentCpicanie.Material.Наименование.ToString(), wordDocument);
                ReplaceWordSub("{IdEdIzmer}", _currentCpicanie.Material.EdIzmer.IdEdIzamer.ToString(), wordDocument);
                ReplaceWordSub("{NaimEdIzm}", _currentCpicanie.Material.EdIzmer.Наименование.ToString(), wordDocument);
                ReplaceWordSub("{Koluch}", Math.Round(_currentCpicanie.Количество).ToString(), wordDocument);
                wordApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CBMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CBMaterial.SelectedIndex > 0)
            {
                // присваивание полок и стеллажей если найден выбранный материал
                Model.Material selectedMater = (Model.Material)CBMaterial.SelectedItem;
                var ctellag = ConnectBD.polimerEntities.Ctelag.ToList();
                if(ctellag.Where(p => p.IdMaterial == selectedMater.IdMaterial).Count()>0)
                {
                    var seleCtellag = ctellag.Where(p => p.IdMaterial == selectedMater.IdMaterial).FirstOrDefault();

                    CBStellag.SelectedIndex = Convert.ToInt32(seleCtellag.НомерСтелажа) - 1;

                    CBPolka.SelectedIndex = Convert.ToInt32(seleCtellag.НомерПолки) - 1;
                }
               
            }
            
        }
    }
}
