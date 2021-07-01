using Console_Calculator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_Calculator
{
    public class Parser : IToken
    {
        #region Implementation of IToken

        public List<Token> Parse(string input)
        {
            var tokens = new List<Token>();
            var array = input.ToCharArray();
            bool negativeDigit = false;
            for (int i = 0; i < array.Length; i++)
            {
                var c = array[i].ToString();
                if (string.IsNullOrWhiteSpace(c)) continue;

                if (IsNumeric(c))
                {
                    var position = i;
                    while (position < array.Length && IsNumeric(array[position].ToString())) ++position;
                    var value = input[i..position];
                    if (value.Contains(".")) value = input[i..position].Replace('.', ',');
                    if (negativeDigit)
                    {
                        value = $"-{value}";
                        negativeDigit = false;
                    }

                    if (!decimal.TryParse(value, out _))
                    {
                        Console.WriteLine($"Число в неправильном формате! {value} - Игнорировано.");
                        break;
                    }
                    Token token = new(value, TokenType.Digit);
                    tokens.Add(token);
                    i = position - 1;
                    continue;
                }

                switch (c)
                {
                    case "(":
                        tokens.Add(new Token(c, TokenType.OpenBrace));
                        break;

                    case ")":
                        tokens.Add(new Token(c, TokenType.CloseBrace));
                        break;

                    case "+":
                        if (tokens.LastOrDefault()?.Type == TokenType.Operator) break; //Если за одним оператором идут еще, то все, кроме первого игнорируются (1-+-+1 парсится в 1-1)
                        tokens.Add(new Token(c, TokenType.Operator, OperatorPriority.Add));
                        break;

                    case "-":
                        if (tokens.LastOrDefault()?.Type == TokenType.Operator) break;
                        if (tokens.LastOrDefault() is null && !array[i + 1].Equals('(') || tokens.LastOrDefault()?.Type == TokenType.OpenBrace)
                        {
                            negativeDigit = true;
                            break;
                        }
                        tokens.Add(new Token(c, TokenType.Operator, OperatorPriority.Substract));
                        break;

                    case "*":
                        if (tokens.LastOrDefault()?.Type == TokenType.Operator) break;
                        tokens.Add(new Token(c, TokenType.Operator, OperatorPriority.MultiPly));
                        break;

                    case "/":
                        if (tokens.LastOrDefault()?.Type == TokenType.Operator) break;
                        tokens.Add(new Token(c, TokenType.Operator, OperatorPriority.Divide));
                        break;

                    default:
                        Console.WriteLine($"Неизвестный оператор {c}");
                        break;
                }
            }
            return tokens;
        }

        private static bool IsNumeric(string c) => double.TryParse(c, out _) || c == "." || c == ",";

        #endregion Implementation of IToken
    }
}