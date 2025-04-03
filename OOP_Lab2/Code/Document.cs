
using OOP_Lab2.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOP_Lab2.FileSafe;

namespace OOP_Lab2
{
    public class Document
    {
        public string name {get;set;}
        public List<User> ListOfAllUsers { get; set; }
        public User Admin { get; set; }
        
        public int type { get; set; }
        private CloudStorage Cstorage;
        private LocalStorage Lstorage;
        WorkWithCloud w_c;
        WorkWithLocal w_l;
        CloudStrategy CloudStr;
        public Document(User admin,string name,int type)
        {
            this.Admin = admin;
            ListOfAllUsers = new List<User>();
            ListOfAllUsers.Add(admin);
            this.name = name;
            w_c = new WorkWithCloud();
            w_l = new WorkWithLocal();
            this.type = type;
        }
        public void ExecuteCloud(User usr)
        {
            CloudStr = new CloudStrategy();
            Cstorage = new CloudStorage();
            CloudStr.Execute(Cstorage,usr,this);
        }
        
    }
}