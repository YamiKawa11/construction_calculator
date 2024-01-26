using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace WpfApp1
{

    public partial class MainWindow : Window
    {


        ApplicationContext db;
        List<User> datas;
        public bool IsAdmin = false;
        private RegLogPanel reglogpanel;
        public bool ForceQuit = false;

        //конструктор
        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
        }

        //окно загружннно
        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            datas = db.Users.ToList();
            
            foreach (var item in datas)
            {
                item.Count = 0;
                item.Out = 0;
                MainDataF.Items.Add(item as User);
            }

        
            UserCheck();
        }

        //проверка, вошёл ли пользователь
        private void UserCheck()
        {
            bool IsFirst = true;

            foreach (Admin item in db.Admins.ToList())
            {
                if (item.IsAdmin == 1) IsFirst = false;
                break;
            }

            if (db.Admins.ToList().Count() == 0)
                
                IsFirst = true;
            if (IsFirst)
            {
                reglogpanel = new RegLogPanel(this, true);
                reglogpanel.Owner = this;
                reglogpanel.ShowDialog();
                reglogpanel.Focus();
            }
            else
            {
                reglogpanel = new RegLogPanel(this);
                reglogpanel.Owner = this;
                Console.WriteLine(ForceQuit);
                reglogpanel.ShowDialog();

            }
        }
        //регистрация администратора
        public void RegAdmin(string Name, string Pass)
        {
            Admin u = new Admin(Name, Pass, 1);
            db.Admins.Add(u);
            db.SaveChanges();
            Console.WriteLine(db.Admins.Count());
        }
        //регистрация пользователя
        public void RegUser(Admin a)
        {
            db.Admins.Add(a);
            db.SaveChanges();
        }

        //вспомогательная функция для удаления всех пользователей
        private void DelAllUsers(object sender, RoutedEventArgs e)
        {
            foreach (var item in db.Admins.ToList())
            {
                db.Admins.Remove(item as Admin);
            }
            db.SaveChanges();
        }

        //проверка, сеществует ли пользователь
        public int AreUserExist(string Name)
        {
            foreach (var item in db.Admins.ToList())
            {
                if(item.Name == Name)
                    return item.id;
            }
            return -1;
        }
        //проверка, подходит ли пароль
        public bool ArePassCorrect(int id, string pass)
        {
            if(db.Admins.Find(id).Pass == pass)
            {
                return true;
            }
            return false;
        }
        //проверка, админ ли пользователь(if)
        public void IsAdminF(int id)
        {
            if (db.Admins.Find(id).IsAdmin == 1)
            {
                IsAdmin = true;
            }
            else IsAdmin = false;
        }
        //Кнопка админ панели
        private void AdminPanelClick(object sender, RoutedEventArgs e)
        {
            if (IsAdmin)
            {
                AdminPanel AdminPanel = new AdminPanel(this);
                AdminPanel.Owner = this;
                AdminPanel.ShowDialog();
            }
            else
            {
                errorWin errorWin = new errorWin("Вы не администратор!");
                errorWin.Owner = this;
                errorWin.ShowDialog();
            }
        }
        //возвращает лист пользователей
        public List<Admin> GetAldminsist()
        {
            
            return db.Admins.ToList();
        }
        //Измеяет пользователя
        public void EditAdminPosition(int ObjId, string EnteredName, string EnteredPass)
        {
            db.Admins.Find(ObjId).Name = EnteredName;
            db.Admins.Find(ObjId).Pass = EnteredPass;
            db.SaveChanges();
        }
        //удаляет пользователя
        public void DelAdminPosition(Admin Obj)
        {
            db.Admins.Remove(db.Admins.Find(Obj.id));
            db.SaveChanges();

        }
        //удаляет позицию
        public void DelPos(int id)
        {
            User usr = (db.Users.Find(id));
            db.Users.Remove(usr);
            MainDataF.Items.Remove(usr);
            db.SaveChanges();
            MainDataF.Items.Refresh();
        }
        //возвращает объект конецчного datagrid
        public DataGrid GetSecDataG()
        {
            return SecDataG;
        }
        //редактирование позиции
        public void EditPos(int id, string name, int price, string type)
        {
            db.Users.Remove(db.Users.Find(id));
            User Nusr = new User(name, type, price);
            db.Users.Add(Nusr);
            MainDataF.Items[MainDataF.Items.IndexOf(MainDataF.SelectedItem)] = Nusr;
            MainDataF.Items.Refresh();
            db.SaveChanges();
        }
        //добавление позиции
        public void AddPosition(string name, string type, int price)
        {
            User user = new User(name, type, price);
            db.Users.Add(user);
            db.SaveChanges();
            MainDataF.Items.Add(user);
        }
        //кнопка экспорта в excel
        private void Excel(object sender, RoutedEventArgs e)
        {
            if (SecDataG.Items.Count == 0)
            {
                errorWin errorWin = new errorWin("Вы не добавили ни одной", "позиции!");
                errorWin.Owner = this;
                errorWin.ShowDialog();
            }
            else
            {
                ExcelWin XL = new ExcelWin(SecDataG.Items);
                XL.Owner = this;
                XL.ShowDialog();
            }
        }
        //кнопка печати
        private void Smeta(object sender, RoutedEventArgs e){
            if(SecDataG.Items.Count == 0)
            {
                errorWin errorWin = new errorWin("Вы не добавили ни одной", "позиции!");
                errorWin.Owner = this;
                errorWin.ShowDialog();
            }
            else
            {
                smeta smeta = new smeta(SecDataG);
                smeta.Owner = this;
                smeta.ShowDialog();
            }
            
        }
        //редактирование позиции(кнопка)
        private void EditDataBtn(object sender, RoutedEventArgs e)
        {
            User uo = (MainDataF.SelectedItem as User);
            AddPosition AddPosition = new AddPosition(this, uo.id, uo.Name, uo.Type, uo.Price);
            AddPosition.Owner = this;
            AddPosition.ShowDialog();
        }
        //кнопка добавление позиции
        private void AddPosBtn(object sender, RoutedEventArgs e)
        {
            AddPosition AddPosition = new AddPosition(this);
            AddPosition.Owner = this;
            AddPosition.ShowDialog();
        }

        //кнопка удаления из второй таблицы
        private void SecDataBtnClick(object sender, RoutedEventArgs e)
        {
            MainDataF.Items.Add(SecDataG.SelectedItem as User);
            SecDataG.Items.Remove(SecDataG.SelectedItem as User);
            double last = 0;
            double lastFM = 0;
            double lastFWork = 0;
            foreach (var item in SecDataG.Items)
            {
                last += (double)(item as User).Count * (double)(item as User).Price;
                if((item as User).Type == "Работа")
                    lastFWork+= (double)(item as User).Count * (double)(item as User).Price;
                else
                    lastFM+= (double)(item as User).Count * (double)(item as User).Price;
            }
            LastPriceTextBox.Text = "Итого: " + last.ToString() + " Рублей";
            LastForMaterialTextBox.Text = "За материалы: "+lastFM+" Рублей";
            LastForWorkTextBox.Text = "За работу: "+lastFWork+" Рублей";
        }
        //кнопка добавления из первой во вторую таблицу
        private void MainDataBtn(object sender, RoutedEventArgs e)
        {
            User a = MainDataF.SelectedItem as User;
            SecDataG.Items.Add(a);
            MainDataF.Items.Remove(a);

        }
        //пересчёт окончательной цены
        public void ReCalculatePrice()
        {
            double last = 0;
            double lastFM = 0;
            double lastFWork = 0;

            foreach (var item in SecDataG.Items)
            {
                User obj = (item as User);
                last += (double)obj.Count * (double)obj.Price;
                if ((item as User).Type == "Работа")
                {
                    lastFWork += (double)(item as User).Count * (double)(item as User).Price;
                }
                else
                {
                    lastFM += (double)(item as User).Count * (double)(item as User).Price;
                }
            }
            LastPriceTextBox.Text = "Итого: " + last.ToString() + " Рублей";
            LastForMaterialTextBox.Text = "За материалы: " + lastFM + " Рублей";
            LastForWorkTextBox.Text = "За работу: " + lastFWork + " Рублей";
        }
        //вспомогательная функция для textbox во второй таблице
        private void PriceInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if(((sender as TextBox).Text.Length > 10))
                ShowErr("Введённая строка слишком длинная!", (sender as TextBox), this);
            else
            {
                if ((sender as TextBox).Text.Length == 0)
                    ShowErr("Вы ничего не ввели!", (sender as TextBox), this);
                else
                {
                    int lastPrice;
                    if (int.TryParse(((sender as TextBox).Text), out lastPrice))
                    {
                        (((sender as TextBox).DataContext) as User).Count = Int32.Parse((sender as TextBox).Text);
                        ReCalculatePrice();
                    }
                    else ShowErr("Вы ввели не число!", (sender as TextBox), this);
                } 
            }
        }
        //вспомогательная функция для textbox во второй таблице
        private void PriceInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "Введите количество")
                (sender as TextBox).Text = "";
        }
        //функция, показывающая ошибку
        private void ShowErr(string msg, TextBox tb, MainWindow w)
        {
            errorWin errorWin = new errorWin(msg, tb, w);
            errorWin.Owner = this;
            errorWin.ShowDialog();
        }
        //закрытие окна
        public void CloseWin()
        {
            Close();
        }
    }
}
