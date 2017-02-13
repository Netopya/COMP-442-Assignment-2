using COMP442_Assignment2.Tokens;
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
        List<Token> _predicts = new List<Token>();

        public Rule(Production production)
        {
            _production = production;
            _symbols = new List<IProduceable> { Tokens.TokenList.Epsilon};
        }

        public Rule(Production production, List<IProduceable> symbols)
        {
            _production = production;
            _symbols = symbols;
        }

        public string printProduction()
        {
            return string.Format("{0} -> {1}", _production.getProductName(), string.Join(" ", _symbols.Select(x => x.getProductName())));
        }

        public void addPredict(Token product)
        {
            if (_predicts.Contains(product))
                Console.WriteLine("LLC Rule Violation");

            _predicts.Add(product);
        }

        public Production getProduction()
        {
            return _production;
        }

        public List<IProduceable> getSymbols()
        {
            return _symbols;
        }

        public List<Token> getPredicts()
        {
            return _predicts;
        }
    }
}
