﻿using System;
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
        WorkWithLocal w_l = new WorkWithLocal();
        public void Execute(User user,Document doc, Settings set,List<Document> d)
        {
            Manager = new EventManager();
            CurrDoc = doc;
            CurrentUser = user;
            menu = new DocumentMenu("admin",this,CurrDoc,set);
            settings = set;
            docs = d;
            menu.AdminMenu();
           
            //ChangeRole(CurrentUser);
            //CheckChangeRoleLogs();
        }
        
        public string Read()
        {
            return w_c.GetFileText("17gYVcgPxxoM4UsNsyq-i2uk8K9RGI4Co",CurrDoc.FileId);
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
            w_c.Delete(CurrDoc.SysFileId, "17gYVcgPxxoM4UsNsyq-i2uk8K9RGI4Co");

        }
        public void ModifyUp(ref string text,int a,int b,string c)
        {
            string t="";
            
            string[] tmp = text.Split("\n");
            if(a-1>tmp.Length-1)
            {
                text += "\r\n";   
            }
            else
            {
                string t1 = "";
                string t2 = "";
                if (b > tmp[a].Length)
                {
                    int k = tmp[a].Length;
                    for (int i = 0; i < b - 1 - k; ++i)
                    {
                        tmp[a] += " ";
                    }
                    //tmp[a] += c;
                    t1 = tmp[a];
                }
                else
                {

                    for (int i = 0; i < b - 1; ++i) t1 += tmp[a][i];
                    if (c.Length>0 && b - 1 >= 0) t1 += tmp[a][b - 1];
                    for (int i = b; i < tmp[a].Length; ++i) t2 += tmp[a][i];
                    text = "";
                    //int l = 0;
                    string ins = t1 + c + t2;
                }
                int l = 0;
                text = "";
                foreach (string s in tmp)
                {
                 
                    //if (b-1 > tmp[a].Length && tmp[a] == s) text += (s);
                    if(a==l) text += (t1 + c + t2);
                    else if (b - 1 > tmp[a].Length && tmp[a] == s) text += (s);
                    else text += (s);
                    if (l != tmp.Length - 1) text += "\n";
                    ++l;
                }
            }
        }
        public void RemoveString(ref string text, int a, int b)
        {
            string[] tmp = text.Split("\n");
            text = "";
            int l = 0;
            foreach (string s in tmp)
            {
                string kk="";
               if(s.Length!=0)kk = s.Remove(s.Length-1,1);
                if (l == a - 1 && l + 1 < tmp.Length)
                {
                    text += (kk + tmp[l + 1]);
                }
                else if (l == a) continue;
                else text += kk;
                if (l != tmp.Length - 2) text += "\r\n";
                ++l;
            }
            int y = 0;
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
        public void SaveLocal(string text)
        {
            w_l.Create(CurrDoc.name,text);
        }
        public void SaveCloud(string text)
        {
            w_l.Create(CurrDoc.name, text);
            CurrDoc.SetText(text);
           string outp= w_c.UploadFile(CurrDoc.name,CurrDoc.type, "1Iiy2UToZeMaFiviRQRwIf8aZJoxncAws","");
            if (outp == "-1") w_c.UpdateFile(CurrDoc.name, CurrDoc.type, "1Iiy2UToZeMaFiviRQRwIf8aZJoxncAws", "", CurrDoc.FileId);
            else CurrDoc.FileId = outp;
            w_l.Delete(CurrDoc.name);
        }
    }
}