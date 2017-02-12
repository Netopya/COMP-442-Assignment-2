using COMP442_Assignment2.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment2.Lexical
{
    /*
        A single node in the DFA that is a final state
    */
    class SimpleFinalState : IState
    {
        private bool _backTrack;
        private Token _token;
        private bool _tokenShowContent;

        public SimpleFinalState(bool backTrack, Token token, bool tokenShowContent)
        {
            this._backTrack = backTrack;
            this._token = token;
            _tokenShowContent = tokenShowContent;
        }

        public SimpleFinalState(Token token)
        {
            _backTrack = false;
            this._token = token;
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
            if(_token == TokenList.Identifier)
            {
                return new IndentifierToken();
            }
            else if(_token == TokenList.Error)
            {
                return new ErrorToken();
            }
            else
            {
                return new SimpleToken(_token, _tokenShowContent);
            }
        }
    }
}
