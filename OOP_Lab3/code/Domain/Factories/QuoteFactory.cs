using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_LAB3.Application.DTO;
namespace OOP_LAB3.Domain.Factories
{
    public static class QuoteFactory
    {
        public static QuoteDTO Create(string author,string text)
        {
            return new QuoteDTO(author,text);
        }
    }
}
