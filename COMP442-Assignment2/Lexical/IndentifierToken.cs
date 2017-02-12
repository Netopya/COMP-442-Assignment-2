using COMP442_Assignment2.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment2.Lexical
{
    /*
        A special class to specifically determine if
        an identifier is actually a reserved words, and to
        change its properties appropriately
    */
    class IndentifierToken : SimpleToken
    {
        Dictionary<string, Token> ReservedWords = new Dictionary<string, Token> {
            { "and",        TokenList.And },
            { "not",        TokenList.Not },
            { "or",         TokenList.Or },
            { "if",         TokenList.If },
            { "then",       TokenList.Then },
            { "else",       TokenList.Else },
            { "for",        TokenList.For },
            { "class",      TokenList.Class },
            { "int",        TokenList.IntRes },
            { "float",      TokenList.FloatRes },
            { "get",        TokenList.Get },
            { "put",        TokenList.Put },
            { "return",     TokenList.Return },
            { "program",    TokenList.Program }
        };

        public IndentifierToken() : base(TokenList.Identifier, true)
        {

        }

        public override void setInfo(string content, int line)
        {
            string name = content.Trim();
            if(ReservedWords.Keys.Contains(name))
            {
                _showContent = false;
                _token = ReservedWords[name];
            }

            base.setInfo(content, line);
        }
    }
}
