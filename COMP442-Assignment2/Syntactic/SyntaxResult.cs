using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment2.Syntactic
{
    public class SyntaxResult
    {
        public List<List<IProduceable>> Derivation;
        public List<string> Errors;

        public SyntaxResult()
        {
            Derivation = new List<List<IProduceable>>();
            Errors = new List<string>();
        }
    }
}
