using System;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace MAProject
{
    static class Program
    {
        private static string _function;
        private static Expr _parsedFunction;
        static void Main(string[] args)
        {
            Start:
            ParseFunction();
            Choices:
            Console.WriteLine("====================PASIRINKIMAI====================");
            Console.WriteLine("1. Apskaičiuoti liekamąjį narį");
            Console.WriteLine("2. Pasirinkti kitą funkciją");
            Console.WriteLine("3. Gauti pasirinktos funkcijos n-tos eilės išvestinę");
            Console.WriteLine("4. Baigti programą");
            Console.WriteLine("====================================================");
            Console.WriteLine();

            Console.Write("Jūsų pasirinkimas: ");
            var choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                Console.Write("Įveskite išvestinės eilę: ");
                var order = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.Write("X intervalas (nuo): ");
                var min = double.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.Write("X intervalas (iki): ");
                var max = double.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine("Funkcijos liekamasis narys: " + LagrangeRemainderCalculator.CalculateLagrangeRemainder(_parsedFunction, "x", order, min, max));
                Console.WriteLine();
                goto Choices;
            }
            else if (choice == "2")
            {
                goto Start;
            }
            else if (choice == "3")
            {
                Console.Write("Įveskite norimos išvestinės eilę: ");
                var order = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine($"Funkcijos {order} eilės išvestinė: " + LagrangeRemainderCalculator.GetDerivativeOfDegree(_parsedFunction, Expr.Variable("x"), order).ToString());
                Console.WriteLine();
                goto Choices;
            }
            else if (choice != "1" && choice != "2" && choice != "3" && choice != "4")
            {
                Console.WriteLine("Pasirinkimas neatpažintas...");
                Console.WriteLine();
                goto Choices;
            }

            Console.WriteLine("Programa baigta...");
        }

        private static void InputFunction()
        {
            Console.Write("Įveskite funkciją (naudokite x, kaip kintamajį): ");
            _function = Console.ReadLine();
            Console.WriteLine();
        }

        private static void ParseFunction()
        {
            while (true)
            {
                InputFunction();
                try
                {
                    _parsedFunction = Expr.Parse(_function);
                    break;
                }
                catch
                {
                    Console.WriteLine("Neteisingai įvesta funkcija, bandykite įvesti iš naujo.");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Sėkmingai pasirinkta funkcija: " + _parsedFunction.ToString());
            Console.WriteLine();
        }
    }
}
