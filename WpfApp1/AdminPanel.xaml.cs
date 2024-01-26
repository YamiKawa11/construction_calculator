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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {

        private MainWindow MainWin;
        public AdminPanel(MainWindow win)
        {
            this.MainWin = win;
            InitializeComponent();
        }
        //вызывает окно редактирования пользователя
        private void GridEditClick(object sender, RoutedEventArgs e)
        {
            AddAdminWin AddAdminWin = new AddAdminWin(MainWin, (MainDataGrid.SelectedItem as Admin), this);
            AddAdminWin.Owner = this;
            AddAdminWin.ShowDialog();
        }
        //выход
        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //приложение загруженно
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainDataGrid.ItemsSource = MainWin.GetAldminsist();
        }
        //удение пользователя
        public void RemoveSelectedUser()
        {
            MainDataGrid.ItemsSource = MainWin.GetAldminsist();
        }
        //добавление пользователя
        private void AddUserBtn(object sender, RoutedEventArgs e)
        {
            AddAdminWin AddAdminWin = new AddAdminWin(MainWin, this);
            AddAdminWin.Owner = this;
            AddAdminWin.ShowDialog();
        }
        //перезагрузка таблицы
        public void Refresh()
        {
            MainDataGrid.ItemsSource = MainWin.GetAldminsist();
            MainDataGrid.Items.Refresh();
        }
    }
}
