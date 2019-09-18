using System;
using System.Collections.Generic;

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
                for (var index = l; index < r+1; index += 1)
                {
                    if (Math.Abs(index % 2) != 0)
                    {
                        result.Add(index);
                    }
                }
            }
            return result;
        }
    }
}
