using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment2.Syntactic
{
    class Rule
    {
        Production _production;
        List<IProduceable> _symbols;

        public Rule(Production production)
        {
            _production = production;
            _symbols = new List<IProduceable>();
        }

        public Rule(Production production, List<IProduceable> symbols)
        {
            _production = production;
            _symbols = symbols;
        }
    }
}
