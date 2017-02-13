using COMP442_Assignment2.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment2.Syntactic
{
    class Production : IProduceable
    {
        string _name;

        public Production(string name)
        {
            _name = name;
        }

        public string getProductName()
        {
            return string.Format("{0}", _name);
        }
    }
}
