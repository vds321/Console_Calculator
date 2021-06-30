using Console_Calculator.Interface;
using System;
using System.Collections.Generic;

namespace Console_Calculator
{
    public class Calculator
    {
        private List<Token> _tokens { get; set; }

        public Calculator(List<Token> tokens)
        {
            _tokens = tokens;
        }

        /// <summary>
        /// Для целей тестирования
        /// </summary>
        /// <param name="expression"></param>
        public Calculator(string expression)
        {
            var parser = new Parser();
            _tokens = parser.Parse(expression);
        }

        public string Calculating()
        {
            var numStack = new Stack<Token>();
            var opStack = new Stack<Token>();

            foreach (var token in _tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Digit:
                        numStack.Push(token);
                        break;

                    case TokenType.Operator:
                        if (opStack.Count != 0 && opStack.Peek().Type == TokenType.Operator && (int)opStack.Peek().Priority >= (int)token.Priority)
                        {
                            var first = numStack.Pop().Value;
                            var second = numStack.Count == 0 ? "0" : numStack.Pop().Value;
                            numStack.Push(Calculate(first, second, opStack.Pop().Value));
                        }
                        opStack.Push(token);
                        break;

                    case TokenType.OpenBrace:
                        opStack.Push(token);
                        break;

                    case TokenType.CloseBrace:
                        while (opStack.Count != 0 && opStack.Peek().Type == TokenType.Operator)
                        {
                            var first = numStack.Pop().Value;
                            var second = numStack.Count == 0 ? "0" : numStack.Pop().Value;
                            numStack.Push(Calculate(first, second, opStack.Pop().Value));
                        }
                        if (opStack.Count != 0 && opStack.Peek().Type == TokenType.OpenBrace)
                        {
                            opStack.Pop();
                        }
                        break;
                }
            }

            while (opStack.Count != 0)
            {
                if (numStack.Count == 0) return "Нет чисел!";
                var first = numStack.Pop().Value;
                var second = numStack.Count == 0 ? "0" : numStack.Pop().Value;
                var op = opStack.Pop().Value;
                numStack.Push(Calculate(first, second, op));
            }
            return numStack.Count != 1 ? "Нет операторов!" : Convert.ToDecimal(numStack.Pop().Value).ToString();
        }

        private Token Calculate(string first, string second, string operation)
        {
            var a = Convert.ToDecimal(first);
            var b = Convert.ToDecimal(second);
            decimal result = 0;
            switch (operation)
            {
                case "+":
                    result = b + a;
                    break;

                case "-":
                    result = b - a;
                    break;

                case "*":
                    result = b * a;
                    break;

                case "/":
                    try
                    {
                        result = b / a;
                        break;
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine($"Деление на 0! {b}/{a}");
                        break;
                    }
            }

            return new Token(result.ToString(), TokenType.Digit);
        }
    }
}