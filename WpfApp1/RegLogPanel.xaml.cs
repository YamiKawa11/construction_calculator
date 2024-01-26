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
    /// Логика взаимодействия для RegLogPanel.xaml
    /// </summary>
    public partial class RegLogPanel : Window
    {
        private bool alreadyIn = false;
        private bool IsFirst;
        private MainWindow MainWin;

        //конструктор
        public RegLogPanel(MainWindow w ,bool IsFirst = false)
        {
            InitializeComponent();
            this.IsFirst = IsFirst;
            this.MainWin = w;
            if (!IsFirst) { MainWin.IsAdmin = false; }
            else { MainWin.IsAdmin = true; }
            
            
        }
        //вывод ошибки
        private void Err(string msg)
        {
            errorWin errorWin = new errorWin(msg);
            errorWin.Owner = this;
            errorWin.ShowDialog();
        }





        //регистрация/вход кнопка
        private void XAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForErrs())
            {
                if (IsFirst)
                {
                    MainWin.RegAdmin(LoginTB.Text, PassTB.Password);
                    //Console.WriteLine("REG ADMIN");
                    alreadyIn = true;
                    Close();
                }
                else
                {
                    if (MainWin.AreUserExist(LoginTB.Text) >= 0)
                    {
                        if (MainWin.ArePassCorrect(MainWin.AreUserExist(LoginTB.Text), PassTB.Password))
                        {
                            MainWin.IsAdminF(MainWin.AreUserExist(LoginTB.Text));
                            alreadyIn = true;
                            Close();
                        }
                        else
                        {
                            Err("Неверный пароль");
                        }
                    }
                    else
                    {
                        Err("Пользователя с таким именем не существует");
                    }
                }
            }
        }



        //проверка правельности ввода
        private bool CheckForErrs()
        {
            if (LoginTB.Text.Length > 70)
                Err("Имя слишком длинное!");
            else
            {
                if (LoginTB.Text == "" || LoginTB.Text == " ")
                    Err("Вы ввели пустое имя!");
                else
                {
                    if (PassTB.Password.Length > 70)
                        Err("Пароль слишком большой!");
                    else
                    {
                        if (PassTB.Password == "" || PassTB.Password == " ")
                        {
                            Err("Вы ввели пустое имя!");
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        //окно загруженно
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsFirst)
            {
                errorWin errorWin = new errorWin("Вам нужно зарегистрировать", "администратора") { };
                errorWin.Owner = this;
                errorWin.ShowDialog();
            }
        }




        //выход
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWin.CloseWin();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!alreadyIn)
            {
                e.Cancel = true;
                MainWin.CloseWin();
            }
            else
                e.Cancel = false;
        }
    }
}
