using System;
using System.Collections.Generic;
using Console_Calculator.Interface;

namespace Console_Calculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("=================== Консольный калькулятор ==========================");
            Console.WriteLine("Введите выражение и нажмите \"Enter\" (для выхода наберите \"Exit\"): ");

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "Exit" || input == "exit") break;
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Пустая строка");
                    continue;
                }
                var tokens = new Parser().Parse(input);
                if (!IsBraceCountCorrect(tokens))
                {
                    Console.WriteLine("Количество скобок неверное!");
                    continue;
                }

                var cacl = new Calculator(tokens);
                var result = cacl.Calculating();
                Console.WriteLine($"Результат: {result}");
            }
        }

        private static bool IsBraceCountCorrect(List<Token> token)
        {
            int openBrace = 0;
            int closeBrace = 0;
            foreach (var t in token)
            {
                if (t.Type == TokenType.OpenBrace) openBrace++;
                if (t.Type == TokenType.CloseBrace) closeBrace++;
            }
            return openBrace == closeBrace;
        }
    }
}