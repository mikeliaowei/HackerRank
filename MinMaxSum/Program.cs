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
        List<long> result = new List<long>(size);
        for (int i = 0; i < size; i++)
        {
            long total = 0;
            for (int j = 0; j < size; j++)
            {
                if (i != j)
                {
                    total += arr[j];
                }
            }
            result.Add(total);
        }

        long minValue = 0;
        long maxValue = 0;

        if(size > 0)
		{
            minValue = result[0];
		}

        foreach (long value in result)
        {
            if (value > maxValue)
            {
                maxValue = value;
            }
           
            if (value < minValue)
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
