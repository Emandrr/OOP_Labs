using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab2.StyleDecorator
{
    class JsonDecorator : BaseDecorator
    {
        public JsonDecorator (Document doc) : base(doc)
        {
        }
        public override string Compile()
        {
            return doc.GetText();
        }
    }
}
