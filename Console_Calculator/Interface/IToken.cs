using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Console_Calculator.Interface
{
    interface IToken
    {
        List<Token> Parse(string input);
    }
}
