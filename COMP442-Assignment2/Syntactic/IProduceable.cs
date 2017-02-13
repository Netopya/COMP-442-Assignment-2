using COMP442_Assignment2.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment2.Syntactic
{
    public interface IProduceable
    {
        string getProductName();
        List<Token> getFirstSet();
        List<Token> getFollowSet();
    }
}
