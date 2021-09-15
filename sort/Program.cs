using System;
using System.IO;

namespace LargeFileSort
{
    class Program
    {
        private const string FilePath = "input/nums.txt";

        static void Main(string[] args)
        {
            using (var inputFileStream = File.Open(FilePath, FileMode.Open))
            {   
                new ExternalSort().Sort(inputFileStream);
            }
        }
    }
}