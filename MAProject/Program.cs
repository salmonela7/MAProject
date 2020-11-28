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
            Console.WriteLine("4. Gauti pasirinktos funkcijos n-tos Teiloro(Makloreno) formulę");
            Console.WriteLine("5. Baigti programą");
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

                Console.Write("X reiksme: ");
                var value = double.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine($"{order} eilės Teiloro(Makloreno) formulė: " + LagrangeRemainderCalculator.GetTaylorExpression(order + 1, Expr.Variable("x"), 0, _parsedFunction).ToString() + $" + r{order}(x)");
                Console.WriteLine();

                Console.WriteLine("Funkcijos liekamasis narys (Lagranzo forma): " + LagrangeRemainderCalculator.CalculateLagrangeRemainder(_parsedFunction, "x", order, 0, value));
                Console.WriteLine("Tikslus funkcijos liekamasis narys: " + LagrangeRemainderCalculator.CalculateExactRemainder(_parsedFunction, value, order));
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
            else if (choice == "4")
            {
                Console.Write("Įveskite norimą Teiloro(Makloreno) formulės eilę: ");
                var order = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine($"{order} eilės Teiloro(Makloreno) formulė: " + LagrangeRemainderCalculator.GetTaylorExpression(order + 1, Expr.Variable("x"), 0, _parsedFunction).ToString() + $" + r{order}(x)");
                Console.WriteLine();
                goto Choices;
            }
            else if (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5")
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
