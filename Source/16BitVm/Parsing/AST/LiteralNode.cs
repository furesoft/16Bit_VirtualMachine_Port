namespace BitVm.Lib.Parsing.AST
{
    public class LiteralNode : ISyntaxNode
    {
        public LiteralNode(object value)
        {
            Value = value;
        }

        public object Value { get; set; }

        public void Accept(SyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}