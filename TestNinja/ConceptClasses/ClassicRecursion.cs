using System;

namespace TestNinja.ConceptClasses
{
    class ClassicRecursion
    {
        public static int GetFibonacciNumber(int index)
        {
            int result;
            if (index == 0 || index == 1)
            {
                result = index;
            }
            else
            {
                result = GetFibonacciNumber(index - 1) + GetFibonacciNumber(index - 2);
            }

            return result;
        }

        public static int CalculateFactorial(int number)
        {
            int result;

            if (number < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (number == 0 || number == 1)
            {
                result = 1;
            }
            else
            {
                result = number * CalculateFactorial(number - 1);
            }

            return result;
        }
    }
}
