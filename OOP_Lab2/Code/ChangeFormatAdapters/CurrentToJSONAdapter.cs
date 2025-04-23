﻿using OOP_Lab2.FileSafe;
using OOP_Lab2.UserStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab2.ChangeFormatAdapters
{
    public class CurrentToJSONAdapter
    {
        public void Parse(IUserStrategy admin,WorkWithCloud w_c,Document document)
        {
            document.type = 1;
            string[] tmp = document.name.Split(".");
            int i = 0;
            string txt="";
            
            foreach(string s in tmp)
            {
                if (i == tmp.Length - 1) continue;
                else txt += s;
                ++i;
            }
            txt += ".json";
            //admin.Delete();
            if(admin is AdminStrategy adm)adm.Upload(txt);
            if (admin is EditorStrategy ed) ed.Upload(txt);
            document.name = txt;
            
           // w_c.UploadFile(document.name,document.type, "17gYVcgPxxoM4UsNsyq-i2uk8K9RGI4Co","");
            //admin.SaveCloud(document.GetText());
        }
        

    }
}
