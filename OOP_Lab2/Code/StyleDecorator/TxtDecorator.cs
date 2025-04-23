﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab2.StyleDecorator
{
    public class TxtDecorator : BaseDecorator
    {
        public TxtDecorator(Document doc) : base(doc)
        {
        }
        public override string Compile()
        {
            return doc.GetText();
        }
    }
}
