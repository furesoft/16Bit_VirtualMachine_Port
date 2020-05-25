namespace BitVm.Lib.Parsing.AST
{
    public class RegisterLiteralNode : LiteralNode
    {
        public RegisterLiteralNode(Registers value) : base(value)
        {
        }
    }
}