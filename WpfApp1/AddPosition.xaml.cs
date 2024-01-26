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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddPosition.xaml
    /// </summary>
    ///     
    public partial class AddPosition : Window
    {
        private bool ForEdit;
        MainWindow MWin;
        private bool check = true;
        private int id_;

        //конструтор добавления позиции
        public AddPosition(MainWindow win)
        {
            MWin = win;
            InitializeComponent();
            check = true;
        }
        //перегрузка для добавления позиции
        public AddPosition(MainWindow win, int id, string name, string type, int price)
        {
            MWin = win;
            InitializeComponent();
            MainTextBlock.Text = "Редактирование позиции";
            nameTB.Text = name;
            priceTB.Text = price.ToString();
            if (type == "Работа")
            {
                Radio1.IsChecked = true;
                Radio2.IsChecked = false;
            }
            else
            {
                Radio1.IsChecked = false;
                Radio2.IsChecked = true;
            }
            check = false;
            XDelBtn.Visibility = Visibility.Visible;
            this.id_ = id;
            XAddBtn.Content = "Редактировать";
        }



        //удалене позиции
        private void DelBtn(object sender, EventArgs e)
        {
            MWin.DelPos(id_);
            Close();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
        //выводит ошибку
        private void Err(string err)
        {
            errorWin errorWin = new errorWin(err);
            errorWin.Owner = this;
            errorWin.ShowDialog();
        }

        //выход
        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //добавяет позицию
        private void AddBtn(object sender, RoutedEventArgs e)
        {
            if (nameTB.Text.Length > 70)
            {
                Err("Имя слишком длинное!");
            }
            else {
                if (priceTB.Text.Length > 10)
                {
                    Err("Цена слишком большая!");
                }
                else { 

                    int a;
                    if (Int32.TryParse(priceTB.Text, out a))
                    {
                        string type;
                        if ((bool)(Radio1.IsChecked))
                        {
                            type = "Работа";
                        }
                        else {
                            type = "Материал";
                        }
                        if (check)
                        {
                            MWin.AddPosition(nameTB.Text, type, a);
                        }
                        else
                        {
                            
                            MWin.EditPos(id_, nameTB.Text, a, type);
                        }
                        Close();
                        
                    }
                    else {
                        
                        Err("Вы неправильно указали цену!");
                    }
                }
            }
            
            
            
        }

        private void Radio1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Radio2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
