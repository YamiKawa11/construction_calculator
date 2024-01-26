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
    /// Логика взаимодействия для smeta.xaml
    /// </summary>
    public partial class smeta : Window
    {
        DataGrid a;

        //конструктор
        public smeta(DataGrid a)
        {
            this.a = a;
            InitializeComponent();
        }

        //окно загруженно
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in a.Items)
            {
                MainDataF.Items.Add(item);

            }
            User u = new User();
            u.Name = "Итого:";
            foreach (User item in MainDataF.Items)
            {
                u.Price += item.Price;
                u.Count += item.Count;
            }


            MainDataF.Items.Add(u);
            
            
        }

        //вывод диалога выбора принтера
        private void Print(object sender, RoutedEventArgs e)
        {
            PrintDialog print = new PrintDialog();
            if(print.ShowDialog() == true)
            {
                ToPrintGrid.LayoutTransform = new ScaleTransform(1.5, 1.5);
                int pageMargin = 5;

                
                Size pageSize = new Size(print.PrintableAreaWidth - pageMargin * 2,
                    print.PrintableAreaHeight - 20);

                ToPrintGrid.Measure(pageSize);
                ToPrintGrid.Arrange(new Rect(pageMargin, pageMargin, pageSize.Width, pageSize.Height));
                print.PrintVisual(ToPrintGrid, "");
            }
            Close();
            
            
        }
    }
}
