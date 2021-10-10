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

class Solution
{
    static string CurrentMaximum = "";

    static List<int> CurrentMaximumArray = null;

    static void Swap(List<int> arr, int firstIndex, int secondIndex)
    {
        int temp = arr[firstIndex];
        arr[firstIndex] = arr[secondIndex];
        arr[secondIndex] = temp;
    }

    private static void Recursion(int index, int k, List<int> set)
    {
        if (index >= set.Count)
        {
            string currentString = string.Join(" ", set);

            if (string.IsNullOrEmpty(CurrentMaximum) || currentString.CompareTo(CurrentMaximum) > 0)
            {
                CurrentMaximum = currentString;
                CurrentMaximumArray = new List<int>(set);
            }
        }
        else
        {
            Recursion(index + 1, k, set);
            for (int i = set.Count - 1; i >= set.Count - k ; i--)
            {
                Swap(set, index, i);
                Recursion(index + 1, k, set);
                Swap(set, index, i);
            }
        }
    }

    public static void Main(string[] args)
    {            //int[] arrayOfIndexes = Enumerable.Range(0, set.Length + 1).ToArray();

        //string currentMaximum = "";
        //List<int> currentMaxList = null;

        //var index = 1;
        //while (index < set.Length)
        //{
        //    arrayOfIndexes[index]--;
        //    var j = set.Length - 1;
        //    if (index % 2 == 1)
        //    {
        //        j = arrayOfIndexes[index];
        //    }

        //    int temp = set[j];
        //    set[j] = set[index];
        //    set[index] = temp;

        //    index = 1;

        //    while (arrayOfIndexes[index] == 0)
        //    {
        //        arrayOfIndexes[index] = index;
        //        index++;
        //    }

        //    string currentSet = string.Join("", set);
        //    Console.WriteLine(currentSet);

        //    if (string.Compare(currentMaximum, currentSet) < 0)
        //    {
        //        currentMaximum = currentSet;
        //        currentMaxList = new List<int>(set);
        //    }
        //}


        //Console.WriteLine("###");
        //Console.WriteLine(currentMaximum);
        Recursion(0, 1, new List<int>(new int[]{4, 2, 3, 5, 1 }));
        Console.WriteLine(CurrentMaximum);
    }
}
