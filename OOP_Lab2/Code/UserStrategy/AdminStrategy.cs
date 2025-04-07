using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Google.Apis.Drive.v3.Data;
using OOP_Lab2.ChangeFormatAdapters;
using OOP_Lab2.FileSafe;
using OOP_Lab2.Menu_s;

namespace OOP_Lab2.UserStrategy
{
    public class AdminStrategy : IUserStrategy
    {
        private EventManager Manager;
        private User CurrentUser;
        Document CurrDoc;
        DocumentMenu menu;
        Settings settings;
        List<Document> docs;
        ManageMemFile<Document> manager = new ManageMemFile<Document>();
        WorkWithCloud w_c = new WorkWithCloud();
        public void Execute(User user,Document doc, Settings set,List<Document> d)
        {
            Manager = new EventManager();
            CurrDoc = doc;
            CurrentUser = user;
            menu = new DocumentMenu("admin",this,CurrDoc,set);
            menu.AdminMenu();
            settings = set;
            docs = d;
            //ChangeRole(CurrentUser);
            //CheckChangeRoleLogs();
        }
        
        public string Read()
        {
            return System.IO.File.ReadAllText("mem.json");
        }
        
        public void Delete()
        {
            docs.Remove(CurrDoc);
            manager.WriteCollection(docs,"mem1.json");
            if(System.IO.File.Exists("C:/Users/pavel/source/repos/OOP_Lab2/OOP_Lab2/bin/Debug/net8.0"+CurrDoc.name))
            {
                System.IO.File.Delete("C:/Users/pavel/source/repos/OOP_Lab2/OOP_Lab2/bin/Debug/net8.0" + CurrDoc.name);
            }
            w_c.Delete(CurrDoc.FileId, "1Iiy2UToZeMaFiviRQRwIf8aZJoxncAws");
            w_c.Delete(CurrDoc.FileId, "17gYVcgPxxoM4UsNsyq-i2uk8K9RGI4Co");

        }
        public void ModifyUp(ref string text,int a,int b,char c)
        {
            string[] tmp = text.Split("\n");
            if(a-1>tmp.Length)
            {
                text += "\n";   
            }
            else
            {
                if (b > tmp[a-1].Length)
                {
                    for(int i=0;i<b-1- tmp[a - 1].Length;++i)
                    {
                        tmp[a - 1] += " ";
                    }
                    tmp[a - 1] += c;
                }

                string t1 = "";
                string t2 = "";
                for (int i = 0; i < b-1; ++i) t1 += tmp[a - 1][i];
                for (int i = b; i < tmp[a-1].Length; ++i) t2 += tmp[a - 1][i];
                text = "";
                int l = 0;
                foreach(string s in tmp)
                {
                 
                    if (b > tmp[a - 1].Length && tmp[a - 1] == s) text += (s + "\n");
                    else if(a-1==l) text += (t1 + c + t2 + "\n");
                    else text += (s + "\n");
                    ++l;
                }
            }
        }
        public void ChangeRole(Document doc,List<User> Users,string role,string UserName)
        {
            doc.Set(UserName,role);
            foreach (User us in Users)
            {
                if (UserName == us.Name)
                {
                    us.CurrentStrategy = role;
                    Manager.Subscribe(us);
                    Manager.Notify(us.Name + " mode changed to " + role + " on document " + CurrDoc.name + "\n");
                    return;
                }
            }
          
            
        }
    }
}