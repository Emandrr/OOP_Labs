using OOP_Lab2.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab2.FileSafe
{
    class CloudStrategy
    {
        WorkWithCloud w_c;
        //WorkWithLocal w_l;
        CloudStorage Cstorage;
        public void Execute(CloudStorage cs)
        {
            w_c = new WorkWithCloud();
            Cstorage = cs;
            //w_c.UploadFile("temp.txt",0,null,null);
        }
        public void CloudSave(string dest, int type)
        {
            
            string id = w_c.UploadFile(dest, type, "2", "2");
            Cstorage.fileId = id;
            Cstorage.name = dest;
        }
        public void CloudUpdate(string dest, int type)
        {

            string a = w_c.UpdateFile(dest, type, "2", "2", Cstorage.fileId, 1);
            Cstorage.name = a;
        }
        public void CloudGetInfo(string folder)
        {
            var a = w_c.GetFiles(folder);
            foreach (var b in a)
            {
                Console.WriteLine(b.Name + " " + b.Size + " " + b.Id);
            }
        }

        public void CloudDelete()
        {
            w_c.Delete(Cstorage.fileId);
            Cstorage.fileId = "0";
        }
    }
}
