using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    /*
        A single node in the DFA that is a final state
    */
    class SimpleFinalState : IState
    {
        private bool _backTrack;
        private string _tokenName;
        private bool _tokenShowContent;

        public SimpleFinalState(bool backTrack, string tokenName, bool tokenShowContent)
        {
            this._backTrack = backTrack;
            this._tokenName = tokenName;
            _tokenShowContent = tokenShowContent;
        }

        public SimpleFinalState(string tokenName)
        {
            _backTrack = false;
            this._tokenName = tokenName;
            _tokenShowContent = false;
        }

        // Cannot add transitions to a final state
        public void addTransition(ICharacterMatch match, IState state)
        {
            throw new NotImplementedException();
        }

        public bool backTrack()
        {
            return _backTrack;
        }

        // Null means to return to the root
        public IState getNextState(char character)
        {
            return null;
        }

        public bool isFinalState()
        {
            return true;
        }

        // Generate a new token for the name
        // given to this final state
        public IToken token()
        {
            if(_tokenName == "Identifier")
            {
                return new IndentifierToken();
            }
            else if(_tokenName == "Error")
            {
                return new ErrorToken();
            }
            else
            {
                return new SimpleToken(_tokenName, _tokenShowContent);
            }
        }
    }
}
