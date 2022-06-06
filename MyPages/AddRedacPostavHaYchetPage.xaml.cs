using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для AddRedacPostavHaYchetPage.xaml
    /// </summary>
    public partial class AddRedacPostavHaYchetPage : Page
    {
        private Model.PostavHaYchet _currentPostHaYchet = new Model.PostavHaYchet();
        private Model.Ctelag _currentCtelag = new Model.Ctelag();
        private Model.Cklad _currentCklad = new Model.Cklad();
        private Model.Material _currentMaterual = new Model.Material();
        private readonly string TemplateFileName = System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Cpicanie.dotm";
        private decimal old;
        public AddRedacPostavHaYchetPage(Model.PostavHaYchet selectedPostHaYchet)
        {
            InitializeComponent();
            
            // принятие значений если они есть
            if (selectedPostHaYchet != null)
            {
                _currentPostHaYchet = selectedPostHaYchet;
                _currentCtelag = ConnectBD.polimerEntities.Ctelag.Find(selectedPostHaYchet.IdCtelag);
                _currentCklad = ConnectBD.polimerEntities.Cklad.Find(_currentCtelag.IdCklad);
                _currentMaterual = ConnectBD.polimerEntities.Material.Find(_currentCtelag.IdMaterial);
                old = _currentPostHaYchet.Количество;
                DPData.DisplayDate = _currentPostHaYchet.Дата;
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
            CBCklad.SelectedIndex = 0;

            CBStellag.ItemsSource = nomerCtellag;
            CBStellag.SelectedIndex = 0;

            CBPolka.ItemsSource = nomerPolki;
            CBPolka.SelectedIndex = 0;

            if (selectedPostHaYchet != null)
            {
                CBCklad.SelectedItem = _currentCklad;
                CBCklad.SelectedIndex = CBCklad.Items.IndexOf(CBCklad.SelectedItem);
                CBMaterial.SelectedItem = _currentMaterual;
                CBMaterial.SelectedIndex = CBMaterial.Items.IndexOf(CBMaterial.SelectedItem);
                CBStellag.SelectedIndex = Convert.ToInt32(_currentCtelag.НомерСтелажа)-1;
                CBPolka.SelectedIndex = Convert.ToInt32(_currentCtelag.НомерПолки)-1;
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
            try
            {

            if (string.IsNullOrWhiteSpace(TBNomerDocument.Text))
                errors.AppendLine("Укажите номер документа");
            else if (Convert.ToInt32(TBNomerDocument.Text) <=0)
                errors.AppendLine("Укажите номер документа");
            if (CBPostav.SelectedIndex == 0)
                errors.AppendLine("Выберите поставщика");

            if (CBMaterial.SelectedIndex == 0)
                errors.AppendLine("Выберите материал");

            if (string.IsNullOrWhiteSpace(TBCena.Text))
                errors.AppendLine("Укажите цену");
            else if ( Decimal.Parse(TBCena.Text) > 1000000000)
                errors.AppendLine("В поле цена значение больше 1000000000");
            if (string.IsNullOrWhiteSpace(TBKolich.Text))
                errors.AppendLine("Укажите Количество");
            else if (Decimal.Parse(TBKolich.Text) > 1000000000)
                errors.AppendLine("В поле цена значение больше 1000000000");
            if (CBCklad.SelectedIndex == 0)
                errors.AppendLine("Выберите склад");
            }
            catch
            {
                MessageBox.Show("Ошибка заполнения данных");
                return;
            }


            Model.Material selectedMaterial = (Model.Material)CBMaterial.SelectedItem;
            Model.Cklad selectedCklad = (Model.Cklad)CBCklad.SelectedItem;

            _currentPostHaYchet.IdMaterial = selectedMaterial.IdMaterial;
            _currentPostHaYchet.Дата = DPData.DisplayDate;

            _currentCtelag.IdCklad = selectedCklad.IdCklad;
            _currentCtelag.IdMaterial = selectedMaterial.IdMaterial;
            _currentCtelag.НомерПолки = CBPolka.Text;
            _currentCtelag.НомерСтелажа = CBStellag.Text;


            if (ConnectBD.polimerEntities.Ctelag.Where(p => p.НомерПолки == CBPolka.Text &&
             p.НомерСтелажа == CBStellag.Text && p.IdMaterial == selectedMaterial.IdMaterial).Count() == 1)
            {
                _currentCtelag = ConnectBD.polimerEntities.Ctelag.Where(p => p.НомерПолки == CBPolka.Text &&
             p.НомерСтелажа == CBStellag.Text && p.IdMaterial == selectedMaterial.IdMaterial).FirstOrDefault();
                if(_currentPostHaYchet.IdPostHaYchet == 0)
                {
                    _currentCtelag.Осталось += Convert.ToDecimal(TBKolich.Text);
                }
                else
                {
                    _currentCtelag.Осталось +=  Convert.ToDecimal(TBKolich.Text) - old;
                }
                

            }
            else if (ConnectBD.polimerEntities.Ctelag.Where(p => p.НомерПолки == CBPolka.Text &&
             p.НомерСтелажа == CBStellag.Text && p.Осталось != -1).Count() > 0)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            
            _currentPostHaYchet.IdCtelag = _currentCtelag.IdCtelag;
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

        //public decimal? CustomParse(string incomeValue, bool format)
        //{
        //    //decimal val;
        //    //if (!decimal.TryParse(incomeValue.Replace(",", "").Replace(".", ""), NumberStyles.Number, CultureInfo.InvariantCulture, out val))
        //    //    return null;
        //    //if(format == false)
        //    //{
        //    //    return val / 100;
        //    //}
        //    //else
        //    //{
        //    //    return  val / 1000;
        //    //}
            
        //}
        private bool Proverka()
        {
            StringBuilder errors = new StringBuilder();
            
            if (string.IsNullOrWhiteSpace(TBNomerDocument.Text))
                errors.AppendLine("Укажите номер документа");
            if (CBPostav.SelectedIndex == 0)
                errors.AppendLine("Выберите поставщика");

            if (CBMaterial.SelectedIndex == 0)
                errors.AppendLine("Выберите материал");

            if (string.IsNullOrWhiteSpace(TBCena.Text))
                errors.AppendLine("Укажите цену");
            else if (Convert.ToInt32(TBCena.Text) > 1000000000)
                errors.AppendLine("В поле цена значение больше 1000000000");
            if (string.IsNullOrWhiteSpace(TBKolich.Text))
                errors.AppendLine("Укажите Количество");
            else if (Convert.ToInt32(TBKolich.Text) > 1000000000)
                errors.AppendLine("В поле цена значение больше 1000000000");

            if (CBCklad.SelectedIndex == 0)
                errors.AppendLine("Выберите склад");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return false;
            }
            return true;
            
        }

        private void BNazad_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
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
            }
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                var wordDocument = wordApp.Documents.Open(TemplateFileName);
                ReplaceWordSub("{IdCpisanie}", _currentPostHaYchet.IdPostHaYchet.ToString(), wordDocument);
                ReplaceWordSub("{Data}", _currentPostHaYchet.Дата.ToShortDateString(), wordDocument);
                ReplaceWordSub("{Postav}", CBPostav.Text, wordDocument);
                ReplaceWordSub("{Cklad}", "Центральный склад", wordDocument);
                ReplaceWordSub("{NomerDoc}", _currentPostHaYchet.НомерДокумента.ToString(), wordDocument);
                ReplaceWordSub("{Material}", _currentPostHaYchet.Material.Наименование.ToString(), wordDocument);
                ReplaceWordSub("{IdEdIzmer}", _currentPostHaYchet.Material.EdIzmer.IdEdIzamer.ToString(), wordDocument);
                ReplaceWordSub("{NaimEdIzm}", _currentPostHaYchet.Material.EdIzmer.Наименование.ToString(), wordDocument);
                ReplaceWordSub("{Koluch}", Math.Round(_currentPostHaYchet.Количество).ToString(), wordDocument);
                wordApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CBMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBMaterial.SelectedIndex > 0)
            {
                // присваивание полок и стеллажей если найден выбранный материал
                Model.Material selectedMater = (Model.Material)CBMaterial.SelectedItem;
                var ctellag = ConnectBD.polimerEntities.Ctelag.ToList();
                if (ctellag.Where(p => p.IdMaterial == selectedMater.IdMaterial).Count() > 0)
                {
                    var seleCtellag = ctellag.Where(p => p.IdMaterial == selectedMater.IdMaterial).FirstOrDefault();

                    CBStellag.SelectedIndex = Convert.ToInt32(seleCtellag.НомерСтелажа) - 1;

                    CBPolka.SelectedIndex = Convert.ToInt32(seleCtellag.НомерПолки) - 1;
                }

            }

        }
    }
}
