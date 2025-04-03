using OOP_Lab2.UserStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Lab2
{
    public class User 
    {
        public string Name { get; set; }

        public Document? TemporaryDocument { get; set; }
        public string? CurrentStrategy { get; set; }
        IUserStrategy? Strategy { get; set; }
        
        public string Logs { get; set; }

        private UserMenu Menu;
        public User(string strat,string name)
        {
            this.Name = name;
            this.Logs = "";
            this.CurrentStrategy = strat;
            SetStrategy();
            Menu = new UserMenu(this);
        }
        public void SetStrategy()
        {
            if(CurrentStrategy == "admin")
            {
                Strategy = new AdminStrategy();
            }
            if (CurrentStrategy == "reader")
            {
                Strategy = new ReaderStrategy();
            }
            if (CurrentStrategy == "writer")
            {
                Strategy = new EditorStrategy();
            }
            
        }
        public void Enter()
        {
            Menu.Start();
        }
        public void ExecuteStrategy()
        {
            Strategy?.Execute(this,TemporaryDocument);
        }
    }
}