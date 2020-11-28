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
            var variable = new Dictionary<string, FloatingPoint>
            {
                { variableName, GetRandomNumberInRange(x0, x) }
            };

            var factorial = (FloatingPoint)SpecialFunctions.Factorial(n);

            var derivativeResult = function.CalculateFunctionAtDerivative(variable, Expr.Variable(variableName), ++n);

            var result = derivativeResult.RealValue / factorial.RealValue * Math.Pow(x.RealValue - variable[variableName].RealValue, n) * (x.RealValue - x0.RealValue);

            return result;
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
