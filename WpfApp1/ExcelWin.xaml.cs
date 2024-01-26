using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ExcelWin.xaml
    /// </summary>
    public partial class ExcelWin : Window
    {

        FileInfo file;
        ItemCollection Data;
        public ExcelWin(ItemCollection Data)
        {
            this.Data = Data;
            InitializeComponent();
        }

        //получание директории
        private void Directory(object sender, RoutedEventArgs e)
        {
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog dial = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dial.ShowDialog();
            if (dial.SelectedPath != "")
            {
                file = new FileInfo(dial.SelectedPath);
            }
            if (file == null)
            {
                errorWin errorWin = new errorWin("Вы не указали путь");
            }
            else Dtext.Text = file.FullName;




            }

        //вывод сообщения
        public void throwmessage(string message, string message2 = "")
            {
                errorWin errorWin = new errorWin(message, message2);
                errorWin.Owner = this;
                errorWin.ShowDialog();
            }
        //сохранение таблицы excel
        private void ExcelSave(object sender, RoutedEventArgs e)
        {
            if(nameTB.Text.Length == 0)
            {

                throwmessage("Вы ввели пустое название!");
                
            }
            else
            {
                if (nameTB.Text.Length <= 40) 
                {
                    if(Dtext.Text == "Директория не выбранна")
                    {
                        throwmessage("Вы не выбрали директорию!");
                    }
                    else { reporter rep = new reporter(file.FullName, Data, this, nameTB.Text); }
                }
                else
                {
                    throwmessage("Название файла слишком", "большое!");
                }
            }
        }
    }
}
