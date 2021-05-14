using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'miniMaxSum' function below.
     *
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    public static void miniMaxSum(List<int> arr)
    {
        int size = arr.Count;
        List<int> result = new List<int>(size);
        for (int i = 0; i < size; i++)
        {
            int total = 0;
            for (int j = 0; j < size; j++)
            {
                if (i != j)
                {
                    total += arr[j];
                }
            }
            result.Add(total);
        }

        int minValue = 0;
        int maxValue = 0;
        foreach (int value in result)
        {
            if (value > maxValue)
            {
                maxValue = value;
            }
            else
            {
                minValue = value;
            }
        }

        Console.WriteLine(minValue + " " + maxValue);
    }

}

class Solution
{
    public static void Main(string[] args)
    {

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        Result.miniMaxSum(arr);
    }
}
