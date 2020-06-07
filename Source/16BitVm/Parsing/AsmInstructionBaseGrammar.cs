using BitVm.Lib.Parsing.AST;
using BitVm.Lib.Parsing.AST.Literals;
using Sprache;

namespace BitVm.Lib.Parsing
{
    public class AsmInstructionBaseGrammar : AsmPrimitiveGrammar
    {
        protected Parser<char> InstructionSeperator => 
             from os in Parse.WhiteSpace.Optional()
                   from c in Parse.Char(',')
                   from oss in Parse.WhiteSpace.Optional()
                   select c;
        

        public virtual Parser<ISyntaxNode> Address => from c in Parse.Char('&')
                    from hex in Parse.Many(HexDigit)
                    select new AddressLiteralNode(string.Join("", hex));


        protected Parser<ISyntaxNode> RegPtrinternal =>
            Parse.Char('&').Then((arg) => SquareBracketExpression);


        protected Parser<ISyntaxNode[]> DoubleArg(Parser<ISyntaxNode> p1, Parser<ISyntaxNode> p2)
        {
            return from arg1 in p1
                   from sep in InstructionSeperator
                   from arg2 in p2
                   from osss in Parse.WhiteSpace.Optional()
                   select new ISyntaxNode[] { arg1, arg2 };
        }

        public virtual Parser<ISyntaxNode> LitReg(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(HexLiteral.Or(SquareBracketExpression), Register)
                   select new InstructionNode(mnem, mnemonic + "_lit_reg", args);
        }

        public virtual Parser<ISyntaxNode> RegLit(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(Register, HexLiteral.Or(SquareBracketExpression))
                   select new InstructionNode(mnem, mnemonic + "_reg_lit", args);
        }

        public virtual Parser<ISyntaxNode> RegReg(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(Register, Register)
                   select new InstructionNode(mnem, mnemonic + "_reg_reg", args);
        }

        public virtual Parser<ISyntaxNode> RegMem(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(Register, Address.Or(RegPtrinternal))
                   select new InstructionNode(mnem, mnemonic + "_reg_mem", args);
        }

        public virtual Parser<ISyntaxNode> MemReg(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(Address.Or(RegPtrinternal), Register)
                   select new InstructionNode(mnem, mnemonic + "_mem_reg", args);
        }

        public virtual Parser<ISyntaxNode> LitMem(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(
                       HexLiteral.Or(
                           RegPtrinternal),
                           Address.Or(RegPtrinternal))
                   select new InstructionNode(mnem, mnemonic + "_lit_mem", args);
        }

        public virtual Parser<ISyntaxNode> RegPtrReg(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from args in DoubleArg(Parse.Char('&').Then((arg) => Register), Register)
                   select new InstructionNode(mnem, mnemonic + "_reg_ptr_reg", args);
        }

        public virtual Parser<ISyntaxNode> LitOffReg(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from lit in HexLiteral.Or(RegPtrinternal)
                   from sep in InstructionSeperator
                   from r1 in Parse.Char('&').Then((arg) => Register)
                   from sep2 in InstructionSeperator
                   from r2 in Register
                   from osss in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, mnemonic + "_lit_off_reg", lit, r1, r2);
        }

        public virtual Parser<ISyntaxNode> NoArg(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, mnemonic);
        }

        public virtual Parser<ISyntaxNode> SingleReg(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from r1 in Register
                   from os in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, mnem + "_reg", r1);
        }

        public virtual Parser<ISyntaxNode> SingleLit(string mnemonic)
        {
            return from mnem in UpperOrLowerStr(mnemonic)
                   from ws in Parse.WhiteSpace
                   from lit in HexLiteral.Or(RegPtrinternal)
                   from os in Parse.WhiteSpace.Optional()
                   select new InstructionNode(mnem, mnem + "_lit", lit);
        }
    }
}