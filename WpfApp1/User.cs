using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public class User
    {
        private string name, type;
        private int price, count;
        private double out_;
        public int id {  get; set; }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public double Out
        {
            get {out_ = (double)price * (double)count;  return out_; }
            set { out_ = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public int Price
        {
            get { return price; }
            set { price = value; }
        }
        

        public User() { }

        public void AllInfo()
        {
            Console.WriteLine("====================");
            Console.WriteLine("Count: "+ count);
            Console.WriteLine("name: " + name);
            Console.WriteLine("type: " + type);
            Console.WriteLine("price: " + price);
            Console.WriteLine("id: " + id);
            Console.WriteLine("OUT: " + out_);
            Console.WriteLine("====================");



        }

        public User(string name, string type, int price)
        {
;           
            this.name = name;
            this.type = type;
            this.price = price;
            Count = 0;
            out_ = (double)price * (double)count;
        }
    }
}
