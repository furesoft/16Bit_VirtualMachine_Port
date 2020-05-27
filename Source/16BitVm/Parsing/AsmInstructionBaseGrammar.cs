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

        public static Parser<ISyntaxNode> Address()
        {
            return Parse.Return(new LiteralNode("address_seperator"));
        }

        private static Parser<ISyntaxNode> RegPtrinternal() {
            return Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.SquareBracketExpression());
        }

        private static Parser<ISyntaxNode[]> DoubleArg(Parser<ISyntaxNode> p1, Parser<ISyntaxNode> p2)
        {
            return from arg1 in p1
                   from sep in InstructionSeperator()
                   from arg2 in p2
                   from osss in Parse.WhiteSpace.Optional()
                   select new ISyntaxNode[] { arg1, arg2 };
        }

        public static Parser<ISyntaxNode> LitReg(string mnemonic)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(AsmPrimitiveGrammar.HexLiteral().Or(AsmPrimitiveGrammar.SquareBracketExpression()), AsmPrimitiveGrammar.Register())
                   select new InstructionNode(mnem, mnemonic + "_lit_reg", args);
        }

        public static Parser<ISyntaxNode> RegReg(string mnemonic)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(AsmPrimitiveGrammar.Register(), AsmPrimitiveGrammar.Register())
                   select new InstructionNode(mnem, mnemonic + "_reg_reg", args);
        }

        public static Parser<ISyntaxNode> RegMem(string mnemonic)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(AsmPrimitiveGrammar.Register(), Address().Or(RegPtrinternal()))
                   select new InstructionNode(mnem, mnemonic + "_reg_mem", args);
        }

        public static Parser<ISyntaxNode> MemReg(string mnemonic)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(Address().Or(RegPtrinternal()), AsmPrimitiveGrammar.Register())
                   select new InstructionNode(mnem, mnemonic + "_mem_reg", args);
        }

        public static Parser<ISyntaxNode> LitMem(string mnemonic)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(
                       AsmPrimitiveGrammar.HexLiteral().Or(
                           RegPtrinternal()),
                           Address().Or(RegPtrinternal()))
                   select new InstructionNode(mnem, mnemonic + "_lit_mem", args);
        }

        public static Parser<ISyntaxNode> RegPtrReg(string mnemonic)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.Register()), AsmPrimitiveGrammar.Register())
                   select new InstructionNode(mnem, mnemonic + "_reg_ptr_reg", args);
        }

        public static Parser<ISyntaxNode> LitOffReg(string mnemonic)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from lit in AsmPrimitiveGrammar.HexLiteral().Or(RegPtrinternal())
                   from sep in InstructionSeperator()
                   from r1 in Parse.Char('&').Then((arg) => AsmPrimitiveGrammar.Register())
                   from sep2 in InstructionSeperator()
                   from r2 in AsmPrimitiveGrammar.Register()
                   from osss in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, mnemonic + "_lit_off_reg", lit, r1, r2);
        }

        public static Parser<ISyntaxNode> NoArg(string mnemonic)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, mnemonic);
        }

        public static Parser<ISyntaxNode> SingleReg(string mnemonic)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from r1 in AsmPrimitiveGrammar.Register()
                   from os in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, mnem + "_reg", r1);
        }

        public static Parser<ISyntaxNode> SingleLit(string mnemonic)
        {
            return from mnem in AsmPrimitiveGrammar.UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from lit in AsmPrimitiveGrammar.HexLiteral().Or(RegPtrinternal())
                   from os in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, mnem + "_lit", lit);
        }
    }
}