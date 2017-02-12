using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    /*
        An interface that defines a class that can except a 
        character and determine if it matches something
    */
    interface ICharacterMatch
    {
        bool doesCharacterMatch(char character);
    }
}
