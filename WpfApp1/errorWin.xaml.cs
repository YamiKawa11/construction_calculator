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
    /// Логика взаимодействия для errorWin.xaml
    /// </summary>
    public partial class errorWin : Window
    {
        private TextBox tb;
        private MainWindow win;
        private bool Is = true;
        public errorWin(string q, TextBox txtb, MainWindow w)
        {
            this.tb = txtb;
            win = w;
            InitializeComponent();
            ErrorMassage.Text = q;

        }
        public errorWin(string q, string w = "")
        {

            Is = false;
            InitializeComponent();
            ErrorMassage.Text = q;
            ErrorMassage2.Text = w;

        }

        //выход
        private void Okbtn(object sender, RoutedEventArgs e)
        {
            
            Close();
            if (Is)
            {
                (tb).Text = "Введите количество";
            }
            
        }
        //выход
        private void Window_Closed(object sender, EventArgs e)
        {
            if (Is)
            {
                (tb).Text = "Введите количество";
                win.ReCalculatePrice();
            }
            
        }
    }
}
