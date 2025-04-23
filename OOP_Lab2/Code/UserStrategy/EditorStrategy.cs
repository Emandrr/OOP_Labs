﻿using OOP_Lab2.ChangeFormatAdapters;
using OOP_Lab2.FileSafe;
using OOP_Lab2.Menu_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Lab2.UserStrategy
{
    public class EditorStrategy :IUserStrategy
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
        CurrentToJSONAdapter JAdapt;
        CurrentToMarkdownAdapter MdAdapt;
        CurrentToRTFAdapter RtfAdapt;
        CurrentToTxtAdapter TxtAdapt;
        CurrentToXMLAdapter XmlAdapt;
        
        public virtual void Execute(User user, Document doc,Settings set, List<Document> d)
        {
            Manager = new EventManager();
            CurrDoc = doc;
            CurrentUser = user;
            menu = new DocumentMenu("editor", this, CurrDoc, set);
            settings = set;
            docs = d;
            JAdapt = new CurrentToJSONAdapter();
            MdAdapt = new CurrentToMarkdownAdapter();
            RtfAdapt = new CurrentToRTFAdapter();
            TxtAdapt = new CurrentToTxtAdapter();
            XmlAdapt = new CurrentToXMLAdapter();
            menu.EditorMenu();
        }
        public void NotifyAdmin(Document doc, List<User> Users, string role, string UserName)
        {
            //doc.Set(UserName, role);
            if (doc.UserName != this.CurrentUser.Name)
            {


                Manager.Subscribe(doc.GetAdmin());
                Manager.Notify("User "+CurrentUser.Name+" changed this document on "+DateTime.Now.ToString()+"\n");
                return;
            }

        }
        public string Read()
        {
            return System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + CurrDoc.name);
        }
        public void SaveLocal(string text)
        {
            w_l.Create(CurrDoc.name, text);
        }
        public void SaveCloud(string text)
        {
            //w_l.Create(CurrDoc.name, text);
            CurrDoc.SetText(text);
            string outp = w_c.UploadFile(CurrDoc.name, CurrDoc.type, "1Iiy2UToZeMaFiviRQRwIf8aZJoxncAws", "");
            if (outp == "-1") w_c.UpdateFile(CurrDoc.name, CurrDoc.type, "1Iiy2UToZeMaFiviRQRwIf8aZJoxncAws", "", CurrDoc.FileId);
            else CurrDoc.FileId = outp;
            //w_l.Delete(CurrDoc.name);
        }
        public void Upload(string text)
        {
            w_l.Delete(CurrDoc.name);
            w_l.Create(text, CurrDoc.GetText());
            if (CurrDoc.FileId != "0")
            {
                w_c.UpdateFile(text, CurrDoc.type, "1Iiy2UToZeMaFiviRQRwIf8aZJoxncAws", "", CurrDoc.FileId);
            }

        }
        public void Delete()
        {
            docs.Remove(CurrDoc);
            manager.WriteCollection(docs, "mem1.json");
            if (System.IO.File.Exists("C:/Users/pavel/source/repos/OOP_Lab2/OOP_Lab2/bin/Debug/net8.0" + CurrDoc.name))
            {
                System.IO.File.Delete("C:/Users/pavel/source/repos/OOP_Lab2/OOP_Lab2/bin/Debug/net8.0" + CurrDoc.name);
            }
            w_c.Delete(CurrDoc.FileId, "1Iiy2UToZeMaFiviRQRwIf8aZJoxncAws");
            //w_c.Delete(CurrDoc.SysFileId, "17gYVcgPxxoM4UsNsyq-i2uk8K9RGI4Co");

        }
        public void ModifyUp(ref string text, int a, int b, string c)
        {
            string t = "";

            string[] tmp = text.Split("\n");
            if (a - 1 > tmp.Length - 1)
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
                    if (c.Length > 0 && b - 1 >= 0) t1 += tmp[a][b - 1];
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
                    if (a == l) text += (t1 + c + t2);
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
                string kk = "";
                if (s.Length != 0) kk = s.Remove(s.Length - 1, 1);
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
        
        public void ChangeFormat(string fmt)
        {
            if (fmt == "txt") TxtAdapt.Parse(this, w_c, CurrDoc);
            if (fmt == "json") JAdapt.Parse(this, w_c, CurrDoc);
            if (fmt == "md") MdAdapt.Parse(this, w_c, CurrDoc);
            if (fmt == "xml") XmlAdapt.Parse(this, w_c, CurrDoc);
            if (fmt == "rtf") RtfAdapt.Parse(this, w_c, CurrDoc);
        }
    }
}