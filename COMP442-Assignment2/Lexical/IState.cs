using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    /*
        An interface representing a node in the DFA, it can be a final state or
        have transitions to another state
    */
    interface IState
    {
        IState getNextState(char character);
        void addTransition(ICharacterMatch match, IState state);
        bool isFinalState();
        bool backTrack();
        IToken token();
    }
}
