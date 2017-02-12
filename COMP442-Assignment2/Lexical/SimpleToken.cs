using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    /*
        A generic token class to hold a
        token's name,  line number, and lexeme
    */
    class SimpleToken : IToken
    {
        protected string _name;
        private string _content = string.Empty;
        protected bool _showContent = false;
        private int _line = -1;

        public SimpleToken(string name, bool showContent)
        {
            _name = name;
            _showContent = showContent;
        }

        // Create a human readable string for this token, with
        // the lexeme if appropriate
        public string getName()
        {
            if(_showContent)
            {
                return string.Format("<{0} ({1}) Line: {2}>", _name, _content, _line);
            }
            else
            {
                return string.Format("<{0} Line: {1}>", _name, _line);
            }
        }

        public virtual void setInfo(string content, int line)
        {
            _content = content;
            _line = line;
        }

        public virtual bool isError()
        {
            return false;
        }
    }
}
