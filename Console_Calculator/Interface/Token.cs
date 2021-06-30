namespace Console_Calculator.Interface
{
    public class Token
    {
        public readonly string Value;
        public readonly TokenType Type;
        public readonly OperatorPriority Priority;

        public Token(string input, TokenType type, OperatorPriority priority = OperatorPriority.NotOperator)
        {
            Value = input;
            Type = type;
            Priority = priority;
        }

    }

    public enum TokenType
    {
        Operator,
        Digit,
        OpenBrace,
        CloseBrace
    }

    public enum OperatorPriority
    {
        NotOperator = 0,
        Add = 1,
        Substract = 1,
        MultiPly = 2,
        Divide = 2
    }

}
