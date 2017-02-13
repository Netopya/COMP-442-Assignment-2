using COMP442_Assignment2.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment2.Syntactic
{
    class SyntacticAnalyzer
    {
        Dictionary<Production, Dictionary<Token, Rule>> table = new Dictionary<Production, Dictionary<Token, Rule>>();

        public SyntacticAnalyzer()
        {
            Production prog = new Production("prog");
            Production classDecl = new Production("classDecl");
            Production varFuncList = new Production("varFuncList");
            Production varFunc = new Production("varFunc");
            Production progBody = new Production("progBody");
            Production funcList = new Production("funcList");
            Production funcDef = new Production("funcDef");
            Production funcBody = new Production("funcBody");
            Production funcBodyList = new Production("funcBodyList");
            Production idtypeFuncBodyList = new Production("idtypeFuncBodyList");
            Production ntypeFuncBodyList = new Production("ntypeFuncBodyList");
            Production varDecl = new Production("varDecl");
            Production arraySizeList = new Production("arraySizeList");
            Production statement = new Production("statement");
            Production assignStat = new Production("assignStat");
            Production statBlock = new Production("statBlock");
            Production statementList = new Production("statementList");
            Production expr = new Production("expr");
            Production relOption = new Production("relOption");
            Production relExpr = new Production("relExpr");
            Production arithExpr = new Production("arithExpr");
            Production arithExprPrime = new Production("arithExprPrime");
            Production sign = new Production("sign");
            Production term = new Production("term");
            Production termPrime = new Production("termPrime");
            Production factor = new Production("factor");
            Production variable = new Production("variable");
            Production furtherIdNest = new Production("furtherIdNest");
            Production factorVarOrFunc = new Production("factorVarOrFunc");
            Production furtherFactor = new Production("furtherFactor");
            Production furtherIndice = new Production("furtherIndice");
            Production indiceList = new Production("indiceList");
            Production indice = new Production("indice");
            Production arraySize = new Production("arraySize");
            Production type = new Production("type");
            Production fParams = new Production("fParams");
            Production aParams = new Production("aParams");
            Production fParamsTail = new Production("fParamsTail");
            Production aParamsTail = new Production("aParamsTail");
            Production assignOp = new Production("assignOp");
            Production relOp = new Production("relOp");
            Production addOp = new Production("addOp");
            Production multOp = new Production("multOp");
            Production num = new Production("num");

            Rule r1 = new Rule(prog, new List<IProduceable> { TokenList.Class, TokenList.Program }); // prog -> classDecl
            Rule r2 = new Rule(classDecl, new List<IProduceable> {
                TokenList.Class, TokenList.Identifier, TokenList.OpenCurlyBracket, varFuncList, TokenList.CloseCurlyBracket
            }); // classDecl -> class id { varFuncList } ; classDecl
            Rule r3 = new Rule(classDecl); // classDecl -> EPSILON
            Rule r4 = new Rule(varFuncList, new List<IProduceable> { });
        }
    }
}
