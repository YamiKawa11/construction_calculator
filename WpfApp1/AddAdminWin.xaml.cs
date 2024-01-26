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
    public partial class AddAdminWin : Window
    {

        MainWindow MWin;
        Admin Selected;
        AdminPanel PanelWin;
        private bool IsForAdd;

        //конструктор
        public AddAdminWin(MainWindow win, Admin a, AdminPanel panel)
        {

            this.PanelWin = panel;
            this.MWin = win;
            this.Selected = a;
            InitializeComponent();
            IsForAdd = false;

        }
        //перегрузка конструктора
        public AddAdminWin(MainWindow win, AdminPanel panel)
        {
            IsForAdd = true;
            this.PanelWin = panel;
            this.MWin = win;
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
        //показывает ошибку
        private void Err(string err)
        {
            errorWin errorWin = new errorWin(err);
            errorWin.Owner = this;
            errorWin.ShowDialog();
        }

        //выход из приложения
        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //добавляет позицию
        private void AddBtn(object sender, RoutedEventArgs e)
        {
            
                if (nameTB.Text.Length > 30)
                    Err("Имя слишком длинное!");
                else
                {
                    if (passTB.Text.Length > 30)
                        Err("Пароль слишком большой!");
                    else
                    {
                        if (!IsForAdd)
                        {
                            if (Selected.IsAdmin == 1)
                                MWin.RegUser(new Admin(nameTB.Text, passTB.Text, 1));
                            else
                                MWin.RegUser(new Admin(nameTB.Text, passTB.Text, 0));
                            MWin.DelAdminPosition(Selected);
                        PanelWin.Refresh();
                        Close();
                        }
                        else
                        {
                        Admin z = new Admin(nameTB.Text, passTB.Text, 0);
                            MWin.RegUser(z);
                            PanelWin.Refresh();
                        Close();
                            
                    }
                    }
                }
            
        }
        //удаляет позицию
        private void DelBtn(object sender, RoutedEventArgs e)
        {
            if (!IsForAdd)
            {
                MWin.DelAdminPosition(Selected);
                PanelWin.RemoveSelectedUser();
                Close();
            }
            else
            {
                Close();
            }
        }
        
        private void AddUserBtn(object sender, RoutedEventArgs e)
        { 
        }


        //загрузка окна
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsForAdd)
            {
                
                nameTB.Text = Selected.Name;
                passTB.Text = Selected.Pass;
                MainTextBlock.Text = "Изменение параметров";
            }
            else
            {
                XAddBtn.Content = "Добавить";
                CancelBtn.Visibility = Visibility.Collapsed;
                XDelBtn.Content = "Отмена";
            }
        }
    }
}
