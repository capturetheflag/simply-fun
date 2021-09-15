using System;
using System.IO;

namespace Sort
{
    class Program
    {
        private const string FilePath = "input/nums.txt";

        static void Main(string[] args)
        {
            var inputFileStream = File.Open(FilePath, FileMode.Open);
          
            var mergeSort = new MergeSort();
            mergeSort.Sort(inputFileStream);
            inputFileStream.Close();
        }
    }
}