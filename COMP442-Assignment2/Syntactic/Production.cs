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
        List<Token> _firstSet;
        List<Token> _followSet;

        public Production(string name, List<Token> firstSet, List<Token> followSet)
        {
            _name = name;
            _firstSet = firstSet;
            _followSet = followSet;
        }

        public string getProductName()
        {
            return string.Format("{0}", _name);
        }
    }
}
