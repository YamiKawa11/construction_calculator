using OfficeOpenXml.FormulaParsing;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Admin
    {



        private string name, pass;
        private int isadmin;
        
        public int id {  get; set; }

        public string AreAdmin
        {
            get {
                if (isadmin == 0) return "Пользователь";
                else return "Администратор";
            }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }
        public int IsAdmin
        {
            get { return isadmin; }
            set { isadmin = value; }
        }
        

        public Admin(string name, string pass, int isadmin)
        {
            Name = name;
            Pass = pass;
            this.isadmin = isadmin;
        }   

        public Admin() { }

        public void AllInfo()
        {
            Console.WriteLine("====================");
            Console.WriteLine("name: "+ name);
            Console.WriteLine("pass: " + pass);
            Console.WriteLine("isadmin: " + isadmin);
            Console.WriteLine("id: " + id);
            Console.WriteLine("====================");



        }

        
    }
}
