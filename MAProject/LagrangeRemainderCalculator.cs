using System;
using System.Collections.Generic;
using MathNet.Numerics;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace MAProject
{
    public static class LagrangeRemainderCalculator
    {
        public static double CalculateLagrangeRemainder(Expr function, string variableName, int n, FloatingPoint x, FloatingPoint x0)
        {
            Console.WriteLine("Max: " + Math.Max(x.RealValue, x0.RealValue));
            var variable = new Dictionary<string, FloatingPoint>
            {
                { variableName, Math.Max(x.RealValue, x0.RealValue) }
            };

            var factorial = (FloatingPoint)SpecialFunctions.Factorial(n+1);

            var derivativeResult = function.CalculateFunctionAtDerivative(variable, Expr.Variable(variableName), n+1);

            var result = derivativeResult.RealValue / factorial.RealValue * Math.Pow(Math.Abs(x.RealValue - x0.RealValue), n+1);

            return result;
        }

        public static double CalculateExactRemainder(Expr func, FloatingPoint point, int order)
        {
            var pointVar = new Dictionary<string, FloatingPoint>
            {
                { "x", point}
            };

            var taylorExpr1 = GetTaylorExpression(order + 1, Expr.Variable("x"), 0, func);

            var trueValue = func.Evaluate(pointVar);
            var taylorValue = taylorExpr1.Evaluate(pointVar);

            return trueValue.RealValue - taylorValue.RealValue;
        }

        public static Expr GetDerivativeOfDegree(
            this Expr symbolicFunction,
            Expr variable, 
            int derivativeDegree)
        {
            for(int i = 0; i < derivativeDegree; i++)
            {
                symbolicFunction = symbolicFunction.Differentiate(variable);
            }

            return symbolicFunction;
        }

        public static Expr GetTaylorExpression(int k, Expr symbol, Expr al, Expr xl)
        {
            int factorial = 1;
            Expr accumulator = Expr.Zero;
            Expr derivative = xl;
            for (int i = 0; i < k; i++)
            {
                var subs = derivative.Substitute(symbol, al);
                derivative = derivative.Differentiate(symbol);
                accumulator = accumulator + subs / factorial * ((symbol - al).Pow(i));
                factorial *= (i + 1);
            }

            return accumulator.Expand();
        }

        private static FloatingPoint CalculateFunctionAtDerivative(
            this Expr symbolicExpression, 
            Dictionary<string, FloatingPoint> variables,
            Expr variableForDerivative, 
            int derivativeDegree)
        {
            var functionAtDerivative = symbolicExpression.GetDerivativeOfDegree(variableForDerivative, derivativeDegree);

            return functionAtDerivative.Evaluate(variables);
        }

        private static FloatingPoint GetRandomNumberInRange(FloatingPoint min, FloatingPoint max) => 
            new Random().NextDouble() * (max.RealValue - min.RealValue) + min.RealValue;
    }
}
