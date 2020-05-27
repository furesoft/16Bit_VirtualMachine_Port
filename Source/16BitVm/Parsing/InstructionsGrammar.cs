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
    }
}