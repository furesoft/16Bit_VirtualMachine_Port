using System.Collections.Generic;
using BitVm.Lib.Parsing.AST;
using Sprache;

namespace BitVm.Lib.Parsing
{
    public class InstructionsGrammar : AsmInstructionBaseGrammar
    {
        public virtual Parser<ISyntaxNode> Mov()
        {
            const string mnemonic = "mov";

            return Choice(
                RegReg(mnemonic),
                LitReg(mnemonic),
                MemReg(mnemonic),
                RegMem(mnemonic),
                LitMem(mnemonic),
                RegPtrReg(mnemonic),
                LitOffReg(mnemonic)
            );
        }

        protected virtual Parser<ISyntaxNode> Op(string mnemonic)
        {
            return Choice(RegReg(mnemonic), LitReg(mnemonic), RegLit(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Add()
        {
            const string mnemonic = "add";

            return Op(mnemonic);
        }

        public virtual Parser<ISyntaxNode> Sub()
        {
            const string mnemonic = "sub";

            return Op(mnemonic);
        }

        public virtual Parser<ISyntaxNode> Mul()
        {
            const string mnemonic = "mul";

            return Op(mnemonic);
        }

        public virtual Parser<ISyntaxNode> Lsf()
        {
            const string mnemonic = "lsf";

            return RegReg(mnemonic).
                Or(LitReg(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Rsf()
        {
            const string mnemonic = "rsf";

            return RegReg(mnemonic).
                Or(LitReg(mnemonic));
        }

        public virtual Parser<ISyntaxNode> And()
        {
            const string mnemonic = "and";

            return RegReg(mnemonic).
                Or(LitReg(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Or()
        {
            const string mnemonic = "or";

            return RegReg(mnemonic).
                Or(LitReg(mnemonic));
        }

        public virtual Parser<ISyntaxNode> XOr()
        {
            const string mnemonic = "xor";

            return RegReg(mnemonic).
                Or(LitReg(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Inc()
        {
            return SingleReg("inc");
        }

        public virtual Parser<ISyntaxNode> Dec()
        {
            return SingleReg("dec");
        }

        public virtual Parser<ISyntaxNode> Not()
        {
            return SingleReg("not");
        }

        public virtual Parser<ISyntaxNode> Jeq()
        {
            const string mnemonic = "jeq";

            return RegMem(mnemonic).
                Or(LitMem(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Jne()
        {
            const string mnemonic = "jne";

            return RegMem(mnemonic).
                Or(LitMem(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Jlt()
        {
            const string mnemonic = "jlt";

            return RegMem(mnemonic).
                Or(LitMem(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Jgt()
        {
            const string mnemonic = "jgt";

            return RegMem(mnemonic).
                Or(LitMem(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Jle()
        {
            const string mnemonic = "jle";

            return RegMem(mnemonic).
                Or(LitMem(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Jge()
        {
            const string mnemonic = "jge";

            return RegMem(mnemonic).
                Or(LitMem(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Psh()
        {
            const string mnemonic = "psh";

            return SingleLit(mnemonic).
                Or(SingleReg(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Pop()
        {
            return SingleReg("pop");
        }

        public virtual Parser<ISyntaxNode> Cal()
        {
            const string mnemonic = "cal";

            return SingleLit(mnemonic).
                Or(SingleReg(mnemonic));
        }

        public virtual Parser<ISyntaxNode> Ret()
        {
            return NoArg("ret");
        }

        public virtual Parser<ISyntaxNode> Hlt()
        {
            return NoArg("hlt");
        }

        public virtual Parser<ISyntaxNode> Instruction()
        {
            return Choice(
                  Mov(),
                  Add(),
                  Sub(),
                  Inc(),
                  Dec(),
                  Mul(),
                  Lsf(),
                  Rsf(),
                  And(),
                  Or(),
                  XOr(),
                  Not(),
                  Jne(),
                  Jeq(),
                  Jlt(),
                  Jgt(),
                  Jle(),
                  Jge(),
                  Psh(),
                  Pop(),
                  Cal(),
                  Ret(),
                  Hlt()
             );
        }

        public virtual Parser<IEnumerable<ISyntaxNode>> Instructions() => Instruction().Many();

        public static IEnumerable<ISyntaxNode> Parse(string source)
        {
            return new InstructionsGrammar().Instructions().Parse(source);
        }
    }
}