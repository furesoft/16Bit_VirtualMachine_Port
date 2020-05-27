using BitVm.Lib.Parsing.AST;
using Sprache;

namespace BitVm.Lib.Parsing
{
    public static class AsmInstructionBaseGrammar
    {
        private static Parser<char> InstructionSeperator()
        {
            return from os in Parse.WhiteSpace.Optional()
                   from c in Parse.Char(',')
                   from oss in Parse.WhiteSpace.Optional()
                   select c;
        }

        private static Parser<ISyntaxNode[]> DoubleArg(Parser<ISyntaxNode> p1, Parser<ISyntaxNode> p2)
        {
            return from arg1 in p1
                   from sep in InstructionSeperator()
                   from arg2 in p2
                   from osss in Parse.WhiteSpace.Optional()
                   select new ISyntaxNode[] { arg1, arg2 };
        }

        public static Parser<ISyntaxNode> LitReg(string mnemonic, string type)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(AsmPrimitiveGrammar.HexLiteral().Or(AsmPrimitiveGrammar.SquareBracketExpression()), AsmPrimitiveGrammar.Register())
                   select new InstructionNode(mnem, type, args);
        }

        public static Parser<ISyntaxNode> RegReg(string mnemonic, string type)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(AsmPrimitiveGrammar.Register(), AsmPrimitiveGrammar.Register())
                   select new InstructionNode(mnem, type, args);
        }

        public static Parser<ISyntaxNode> RegMem(string mnemonic, string type)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(AsmPrimitiveGrammar.Register(), Address.Or(Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.SquareBracketExpression()))
                   select new InstructionNode(mnem, type, args);
        }

        public static Parser<ISyntaxNode> MemReg(string mnemonic, string type)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(Address.Or(Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.SquareBracketExpression()), AsmPrimitiveGrammar.Register())
                   select new InstructionNode(mnem, type, args);
        }

        public static Parser<ISyntaxNode> LitMem(string mnemonic, string type)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(AsmPrimitiveGrammar.HexLiteral().Or(Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.SquareBracketExpression()), Address.Or(Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.SquareBracketExpression()))
                   select new InstructionNode(mnem, type, args);
        }

        public static Parser<ISyntaxNode> RegPtrReg(string mnemonic, string type)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.Register()), AsmPrimitiveGrammar.Register())
                   select new InstructionNode(mnem, type, args);
        }

        public static Parser<ISyntaxNode> LitOffReg(string mnemonic, string type)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from lit in AsmPrimitiveGrammar.HexLiteral().Or(Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.SquareBracketExpression()))
                   from sep in InstructionSeperator()
                   from r1 in Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.Register())
                   from sep2 in InstructionSeperator()
                   from r2 in AsmPrimitiveGrammar.Register()
                   from osss in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, type, lit, r1, r2);
        }

        public static Parser<ISyntaxNode> NoArg(string mnemonic, string type)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, type);
        }

        public static Parser<ISyntaxNode> SingleReg(string mnemonic, string type)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from r1 in AsmPrimitiveGrammar.Register()
                   from os in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, type, r1);
        }

        public static Parser<ISyntaxNode> SingleLit(string mnemonic, string type)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from lit in AsmPrimitiveGrammar.HexLiteral().Or(Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.SquareBracketExpression()))
                   from os in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, type, lit);
        }
    }
}