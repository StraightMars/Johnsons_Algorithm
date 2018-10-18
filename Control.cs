using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex4JohnsonsAlgorythm
{
    class Control
    {
        static int FindMax(int[] arr)
        {
            int max = arr[0];
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }
            return max;
        }
        static void FindMinAndMinIndex(List<int> firstList, List<int> secondList, out int index, out int min, int max)
        {
            min = max;
            index = 0;
            for (int i = 0; i < firstList.Count; i++)
            {
                if (firstList[i] < min || secondList[i] < min)
                {
                    if (firstList[i] < min)
                    {
                        min = firstList[i];
                        index = i;
                    }
                    if (secondList[i] < min)
                    {
                        min = secondList[i];
                        index = i;
                    }
                }
            }
        }
        static void ShowMatrix(int[,] matr)
        {
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    Console.Write("{0,5}", matr[i, j]);
                }
                Console.WriteLine();
            }
        }
        public static string[] GetTimetable(int[] firstColumn, int[] secondColumn)
        {
            string firstString = "";
            string secondString = "";
            char[] name = new char[firstColumn.Length];
            for (int i = 0; i < name.Length; i++)
            {
                name[i] = (char)(i + 65);
            }
            List<char> names = new List<char>();
            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>();
            for (int i = 0; i < firstColumn.Length; i++)
            {
                firstList.Add(firstColumn[i]);
                secondList.Add(secondColumn[i]);
                names.Add(name[i]);
            }
            int[,] finalMatrix = new int[firstList.Count, 2];
            int beginning = 0;
            int end = firstList.Count - 1;
            int firstMax = FindMax(firstColumn);
            int secondMax = FindMax(secondColumn);
            int max = Math.Max(firstMax, secondMax);
            while (firstList.Count != 0)
            {
                FindMinAndMinIndex(firstList, secondList, out int index, out int min, max);
                if (min == firstList[index])
                {
                    name[beginning] = names[index];
                    finalMatrix[beginning, 0] = firstList[index];
                    finalMatrix[beginning, 1] = secondList[index];
                    beginning++;
                }
                else
                {
                    if (min == secondList[index])
                    {
                        name[end] = names[index];
                        finalMatrix[end, 0] = firstList[index];
                        finalMatrix[end, 1] = secondList[index];
                        end--;
                    }
                }
                firstList.RemoveAt(index);
                secondList.RemoveAt(index);
                names.RemoveAt(index);
            }


            int newOccupancy = 0;
            int oldOccupancy = 0;
            int[] workEnds = new int[finalMatrix.GetLength(0)];
            for (int i = 0; i < finalMatrix.GetLength(0); i++)
            {
                newOccupancy += finalMatrix[i, 0];
                int x = finalMatrix[i, 0];
                workEnds[i] = newOccupancy;
                while (x != 0)
                {
                    firstString += name[i];
                    x--;
                }
                oldOccupancy = newOccupancy;
            }
            newOccupancy = 0;
            oldOccupancy = 0;
            for (int i = 0; i < finalMatrix.GetLength(0); i++)
            {
                if (newOccupancy < workEnds[i])
                {
                    int buf = newOccupancy;
                    while (buf < workEnds[i])
                    {
                        secondString += " ";
                        buf++;
                    }
                    newOccupancy = workEnds[i];
                    oldOccupancy = workEnds[i];
                }
                newOccupancy += finalMatrix[i, 1];
                int x = finalMatrix[i, 1];
                while (x != 0)
                {
                    secondString += name[i];
                    x--;
                }
                oldOccupancy = newOccupancy;
            }
            string[] arr = new string[2];
            arr[0] = firstString;
            arr[1] = secondString;
            return arr;
        }
    }
}
