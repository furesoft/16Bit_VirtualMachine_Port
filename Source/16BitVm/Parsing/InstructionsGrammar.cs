using System;
using BitVm.Lib.Parsing.AST;
using Sprache;

namespace BitVm.Lib.Parsing
{
    public static class InstructionsGrammar
    {
        public static Parser<ISyntaxNode> Mov()
        {
            const string mnemonic = "mov";

            return AsmInstructionBaseGrammar.RegReg(mnemonic).
                Or(AsmInstructionBaseGrammar.LitReg(mnemonic)).
                Or(AsmInstructionBaseGrammar.MemReg(mnemonic)).
                Or(AsmInstructionBaseGrammar.RegMem(mnemonic)).
                Or(AsmInstructionBaseGrammar.LitMem(mnemonic)).
                Or(AsmInstructionBaseGrammar.RegPtrReg(mnemonic).
                Or(AsmInstructionBaseGrammar.LitOffReg(mnemonic)));
        }

        private static Parser<ISyntaxNode> Op(string mnemonic)
        {
            return AsmInstructionBaseGrammar.RegReg(mnemonic).
                Or(AsmInstructionBaseGrammar.LitReg(mnemonic));
        }

        public static Parser<ISyntaxNode> Add()
        {
            const string mnemonic = "add";

            return Op(mnemonic);
        }

        public static Parser<ISyntaxNode> sub()
        {
            const string mnemonic = "sub";

            return Op(mnemonic);
        }

        public static Parser<ISyntaxNode> Mul()
        {
            const string mnemonic = "mul";

            return Op(mnemonic);
        }

        public static Parser<ISyntaxNode> Lsf()
        {
            const string mnemonic = "lsf";

            return AsmInstructionBaseGrammar.RegReg(mnemonic).
                Or(AsmInstructionBaseGrammar.LitReg(mnemonic));
        }

        public static Parser<ISyntaxNode> Rsf()
        {
            const string mnemonic = "rsf";

            return AsmInstructionBaseGrammar.RegReg(mnemonic).
                Or(AsmInstructionBaseGrammar.LitReg(mnemonic));
        }

        public static Parser<ISyntaxNode> and()
        {
            const string mnemonic = "and";

            return AsmInstructionBaseGrammar.RegReg(mnemonic).
                Or(AsmInstructionBaseGrammar.LitReg(mnemonic));
        }

        public static Parser<ISyntaxNode> Or()
        {
            const string mnemonic = "or";

            return AsmInstructionBaseGrammar.RegReg(mnemonic).
                Or(AsmInstructionBaseGrammar.LitReg(mnemonic));
        }

        public static Parser<ISyntaxNode> XOr()
        {
            const string mnemonic = "xor";

            return AsmInstructionBaseGrammar.RegReg(mnemonic).
                Or(AsmInstructionBaseGrammar.LitReg(mnemonic));
        }

        public static Parser<ISyntaxNode> Inc()
        {
            return AsmInstructionBaseGrammar.SingleReg("inc");
        }

        public static Parser<ISyntaxNode> Dec()
        {
            return AsmInstructionBaseGrammar.SingleReg("dec");
        }

        public static Parser<ISyntaxNode> Not()
        {
            return AsmInstructionBaseGrammar.SingleReg("not");
        }

        public static Parser<ISyntaxNode> Jeq()
        {
            const string mnemonic = "jeq";

            return AsmInstructionBaseGrammar.RegMem(mnemonic).
                Or(AsmInstructionBaseGrammar.LitMem(mnemonic));
        }

        public static Parser<ISyntaxNode> Jne()
        {
            const string mnemonic = "jne";

            return AsmInstructionBaseGrammar.RegMem(mnemonic).
                Or(AsmInstructionBaseGrammar.LitMem(mnemonic));
        }

        public static Parser<ISyntaxNode> Jlt()
        {
            const string mnemonic = "jlt";

            return AsmInstructionBaseGrammar.RegMem(mnemonic).
                Or(AsmInstructionBaseGrammar.LitMem(mnemonic));
        }

        public static Parser<ISyntaxNode> Jgt()
        {
            const string mnemonic = "jgt";

            return AsmInstructionBaseGrammar.RegMem(mnemonic).
                Or(AsmInstructionBaseGrammar.LitMem(mnemonic));
        }

        public static Parser<ISyntaxNode> Lle()
        {
            const string mnemonic = "jle";

            return AsmInstructionBaseGrammar.RegMem(mnemonic).
                Or(AsmInstructionBaseGrammar.LitMem(mnemonic));
        }

        public static Parser<ISyntaxNode> Jge()
        {
            const string mnemonic = "jge";

            return AsmInstructionBaseGrammar.RegMem(mnemonic).
                Or(AsmInstructionBaseGrammar.LitMem(mnemonic));
        }

        public static Parser<ISyntaxNode> Push()
        {
            const string mnemonic = "psh";

            return AsmInstructionBaseGrammar.SingleLit(mnemonic).
                Or(AsmInstructionBaseGrammar.SingleReg(mnemonic));
        }

        public static Parser<ISyntaxNode> Pop()
        {
            return AsmInstructionBaseGrammar.SingleReg("pop");
        }

        public static Parser<ISyntaxNode> Cal()
        {
            const string mnemonic = "cal";

            return AsmInstructionBaseGrammar.SingleLit(mnemonic).
                Or(AsmInstructionBaseGrammar.SingleReg(mnemonic));
        }

        public static Parser<ISyntaxNode> Ret()
        {
            return AsmInstructionBaseGrammar.NoArg("ret");
        }

        public static Parser<ISyntaxNode> Hlt()
        {
            return AsmInstructionBaseGrammar.NoArg("hlt");
        }

        public static Parser<ISyntaxNode> Instruction()
        {
            /*
             * module.exports = A.choice([
  mov,
  add,
  sub,
  inc,
  dec,
  mul,
  lsf,
  rsf,
  and,
  or,
  xor,
  not,
  jne,
  jeq,
  jlt,
  jgt,
  jle,
  jge,
  psh,
  pop,
  cal,
  ret,
  hlt,
]);
*/
        }
    }
}