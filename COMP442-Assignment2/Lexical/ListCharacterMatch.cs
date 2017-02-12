using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    /*
        Allows a character to be matches to one from a
        list of characters
    */
    class ListCharacterMatch : ICharacterMatch
    {
        private readonly List<char> _matches;

        public ListCharacterMatch(List<char> matches)
        {
            _matches = matches;
        }

        public bool doesCharacterMatch(char character)
        {
            return _matches.Any(x => x == character);
        }
    }
}
