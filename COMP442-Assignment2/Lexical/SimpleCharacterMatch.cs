using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    /*
        A generic character matcher that only recognizes
        a single character
    */
    class SimpleCharacterMatch : ICharacterMatch
    {
        private char _character;

        public SimpleCharacterMatch(char character)
        {
            _character = character;
        }

        public bool doesCharacterMatch(char character)
        {
            return _character == character;
        }
    }
}
