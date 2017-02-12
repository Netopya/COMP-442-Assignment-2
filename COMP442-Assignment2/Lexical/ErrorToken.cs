using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    // A token representing an error
    class ErrorToken : SimpleToken
    {
        public ErrorToken() : base("Error", true)
        {

        }

        public override bool isError()
        {
            return true;
        }
    }
}
