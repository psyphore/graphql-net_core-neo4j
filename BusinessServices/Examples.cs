using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace BusinessServices
{
    public class Examples
    {
        public static string FindNumber(List<int> arr, int k)
        {
            var result = "NO";
            for (var index = 0; index < arr.Count; index += 1)
            {
                if (arr[index] == k)
                {
                    result = "YES";
                    break;
                }
            }
            return result;
        }

        public static List<int> OddNumbers(int l, int r)
        {
            var result = new List<int>();
            if (l >= 1 && r <= Math.Abs(Math.Pow(10, 5)))
            {
                for (var index = l; index < r + 1; index += 1)
                    if (Math.Abs(index % 2) != 0)
                        result.Add(index);
            }
            return result;
        }

        public static decimal Calc(string value1, string value2, string calcOperator, out string errorMessage)
        {
            errorMessage = "";
            decimal returnValue = 0M;
            decimal v1 = 0M;
            decimal v2 = 0M;
            try
            {
                if (!string.IsNullOrEmpty(value1) && !decimal.TryParse(value1, out v1))
                    errorMessage = "Values must be numeric.";

                if (!string.IsNullOrEmpty(value2) && !decimal.TryParse(value2, out v2))
                    errorMessage = "Values must be numeric.";

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    returnValue = -9999;
                }
                else
                {
                    switch (calcOperator)
                    {
                        default:
                            break;
                    }

                    switch (calcOperator)
                    {
                        case "Add":
                            returnValue = v1 + v2;
                            break;

                        case "Subtract":
                            returnValue = v1 - v2;
                            break;

                        case "Multiply":
                            returnValue = v1 * v2;
                            break;

                        case "Divide":
                            returnValue = v1 / v2;
                            break;

                        default:
                            {
                                returnValue = -9999;
                                errorMessage = "Incorrect operator";
                            }
                            break;
                    }
                }
            }
            catch (System.Exception e)
            {
                errorMessage = e.Message;
                returnValue = -9999;
            }

            return returnValue;
        }

        public static string FormatAsCustomString(DateTime date, int number)
        {
            var dateStr = date.ToString("yyMMdd");
            var numberStr = number.ToString("00000");
            return string.Format("{0}-{1}", numberStr, dateStr);
        }

        public static int Calculate(int a, int b)
        {
            Thread.Sleep(2);
            return a * b;
        }

        public static bool IsFibo(int valueToCheck, int previousValue, int currentValue)
        {
            if (valueToCheck < currentValue)
                return false;

            if (currentValue > valueToCheck)
                return false;

            if (valueToCheck == currentValue)
                return true;

            return IsFibo(valueToCheck, currentValue, previousValue + currentValue);
        }

        public static Dictionary<string, int> AggregateNames(string[] lines)
        {
            //Write your code here
            var result = new Dictionary<string, int>();

            var groups = lines.GroupBy(l => l);
            foreach (var group in groups)
            {
                if (!result.ContainsKey(group.Key))
                    result.Add(group.Key, group.Count());
            }

            return result;
        }

        public static string ReverseWords(string input)
        {
            return string.Concat(Regex
                .Split(input, @"(\W+)")
                .Select(letter => letter.Length > 0 && char.IsLetter(letter[0])
                    ? string.Concat(letter.Reverse()) : letter));
        }
    }

    public class DataProcessor
    {
        private Stack<int> sharedStore;
        private Stack<int> answers;
        private object locker = new object();

        public DataProcessor()
        {
            this.sharedStore = new Stack<int>();
            this.answers = new Stack<int>();
        }

        public void InititialiseData(IEnumerable<int> input)
        {
            foreach (int i in input)
                sharedStore.Push(i);
        }

        public IEnumerable<int> GetResults()
        {
            while (answers.Count > 0)
                yield return answers.Pop();
        }

        //Call Calculate in the parent class to perform the calculation.
        //Do not calculate in this method
        public void CalculateData()
        {
            lock (locker)
            {
                int number = sharedStore.Pop();
                answers.Push(Examples.Calculate(number, sharedStore.Count));
            }
        }
    }
}