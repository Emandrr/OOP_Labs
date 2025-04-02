using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Lab2.UserStrategy
{
    public class AdminStrategy : IUserStrategy
    {
        private EventManager Manager;
        private User CurrentUser;
        Document CurrDoc;
        public void Execute(User user)
        {
            Manager = new EventManager();
            string name = Console.ReadLine();
            CurrDoc = new Document(user,name);
            CurrentUser = user;
            ChangeRole(CurrentUser);
            CheckChangeRoleLogs();
        }
        
        public void CheckChangeRoleLogs()
        {
            Console.WriteLine(CurrentUser.Logs);
        }
        

        public void ChangeRole(User user)
        {
            CurrentUser.CurrentStrategy = "read";
            Manager.Subscribe(user);
            Manager.Notify(user.Name + " mode changed to "+ CurrentUser.CurrentStrategy + " on document "+ CurrDoc.name+"\n");
        }
    }
}