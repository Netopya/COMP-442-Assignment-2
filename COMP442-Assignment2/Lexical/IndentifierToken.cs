using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    /*
        A special class to specifically determine if
        an identifier is actually a reserved words, and to
        change its properties appropriately
    */
    class IndentifierToken : SimpleToken
    {
        HashSet<string> ReservedWords = new HashSet<string> { "and", "not", "or", "if", "then", "else", "for", "class", "int", "float", "get", "put", "return", "program" };

        public IndentifierToken() : base("Identifier", true)
        {

        }

        public override void setInfo(string content, int line)
        {
            string name = content.Trim();
            if(ReservedWords.Contains(name))
            {
                _showContent = false;
                _name = content;
            }

            base.setInfo(content, line);
        }
    }
}
