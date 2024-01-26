using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace WpfApp1
{




    
    public class reporter
    {


        FileInfo file;
        System.Windows.Controls.ItemCollection listofobjects;
        ExcelWin win;
        public reporter(string fileway, System.Windows.Controls.ItemCollection listofobjects, ExcelWin win, string filename = "name")
        {
            fileway = fileway + "\\" + filename + ".xlsx";
            this.file = new FileInfo(fileway);
            this.listofobjects = listofobjects;
            this.win = win;
            if (file.Exists)
            {
                try
                {
                    file.Delete();
                    file = new FileInfo(fileway);
                    main();
                }
                catch (Exception) { 
                
                    win.throwmessage("Не удалось получить доступ,", "попробуйте закрыть файл, если он открыт.");
                }
                
            }
            else
            {
                file = new FileInfo(fileway);
                main();
            }
        }
        private void main()
        {
            
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Лист");


                ws.Cells[1, 3].Value = "Приложение № _";
                ws.Cells[1, 3].Style.Font.Bold = true;

                //2
                ws.Cells[2, 3].Value = "к Договору № ______ от «__»__________";


                //5 - 13
                ws.Cells[5, 1].Value = "Подрядчик:";

                ws.Cells[5, 3].Value = "Исполнитель";
                ws.Cells[5, 3].Style.Font.Bold = true;

                ws.Cells[6, 3].Value = "ИП Иванов И.И.";
                ws.Cells[6, 3].Style.Font.Bold = true;




                ws.Cells[7, 3].Value = "ИНН: 000000000000";
                ws.Cells[8, 3].Value = "ОГРНИП: 000000000000000";
                ws.Cells[9, 3].Value = "Тел. +7 888 888 88 88";
                ws.Cells[10, 3].Value = "г. Москва, ул. Ленина";
                ws.Cells[11, 3].Value = "Р/с: 00000000000000000000 в РНКБ Банк (ПАО)";
                ws.Cells[12, 3].Value = "БИК: 000000000";
                ws.Cells[13, 3].Value = "Корр. счет: 00000000000000000000";

                using (var range = ws.Cells[5, 1, 5, 6]) range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                using (var range = ws.Cells[5, 6, 13, 6]) range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                using (var range = ws.Cells[13, 1, 13, 6]) range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                using (var range = ws.Cells[5, 1, 13, 1]) range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                using (var range = ws.Cells[5, 2, 13, 2]) range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                ws.Cells[16, 1].Value = "Адрес выполениня работ__________________________________________________________";
                ws.Cells[17, 1].Value = "_________________________________________________________________________________";

                ws.Cells[23, 1].Value = "№";
                ws.Cells[23, 2].Value = "Позиция";
                ws.Cells[23, 3].Value = "Тип";
                ws.Cells[23, 4].Value = "Кол-во";
                ws.Cells[23, 5].Value = "Стоимость";
                ws.Cells[23, 6].Value = "Общая стоимость";
                ws.Column(1).Width = 13;
                ws.Column(2).Width = 13;
                ws.Column(3).Width = 13;
                ws.Column(4).Width = 13;
                ws.Column(5).Width = 13;
                ws.Column(6).Width = 17;
                int counter = 0;
                double EndPrice = 0;
                foreach (User o in listofobjects)
                {
                    counter += 1;
                    ws.Cells[23 + counter, 1].Value = counter;
                    ws.Cells[23 + counter, 2].Value = o.Name;
                    ws.Cells[23 + counter, 3].Value = o.Type;
                    ws.Cells[23 + counter, 4].Value = o.Count;
                    ws.Cells[23 + counter, 5].Value = o.Price;
                    ws.Cells[23 + counter, 6].Value = (double)o.Price * (double)o.Count;
                    EndPrice += (double)o.Price * (double)o.Count;
                }
                ws.Cells[24 + counter, 2].Value = "Итого:";
                ws.Cells[24 + counter, 2].Style.Font.Bold = true;
                ws.Cells[24 + counter, 6].Value = EndPrice;
                ws.Cells[24 + counter, 6].Style.Font.Bold = true;

                using (var range = ws.Cells[23, 1, 24 + counter, 6])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                }
                ws.Cells[27 + counter, 1].Value = "_____________________ФИО исполнителя";
                ws.Cells[27 + counter, 4].Value = "_____________________ФИО заказчика";
                package.Save();
                win.throwmessage("Таблица успешно создана.");
            }
        }
    }
}
