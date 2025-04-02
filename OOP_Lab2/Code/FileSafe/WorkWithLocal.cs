using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP_Lab2.FileSafe
{
    class WorkWithLocal
    {
        public void Create(string name,string content,int type)
        {   if (type == 0) name += ".txt";
            if (type == 1) name += ".json";
            if (type == 2) name += ".xml";
            if (type == 3) name += ".md";
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory+name, content);
        }
        public void Delete(string name)
        {
           
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + name);
        }
        public void Update(string nameOne,int choise,int type)
        {
            string name = "";
            if (choise == 1)
            {
                name = Console.ReadLine();
                string context = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+nameOne);
                Delete(nameOne);
                Create(name,context,type);
            }


        }

    }
}
