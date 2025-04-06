﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Lab2.UserStrategy
{
    public interface IUserStrategy
    {
        public void Execute(User user,Document doc,Settings set,List<Document> docs);
    }
}