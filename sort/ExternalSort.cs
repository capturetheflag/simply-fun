using System;
using System.IO;
using System.Linq;

namespace LargeFileSort
{
    public class ExternalSort
    {
        public const int ChunkSize = 4;

        public void Sort(FileStream fileStream)
        {
            var fileIndex = 0;

            using (var streamReader = new StreamReader(fileStream))
            {
                while (!streamReader.EndOfStream)
                {
                    var chunk = new int?[ChunkSize]; 
                    for (var i = 0; i < ChunkSize && !streamReader.EndOfStream; i++)
                    {
                        chunk[i] = int.Parse(streamReader.ReadLine());
                    }

                    Array.Sort(chunk);
                    File.WriteAllLines($"chunks/{fileIndex++}.txt", chunk.Select(n => n.ToString()).Where(s => !string.IsNullOrEmpty(s)));
                }
            }

            var buffer = new int?[ChunkSize];
            var streamReaders = new StreamReader[ChunkSize];
            for (var i = 0; i < ChunkSize; i++)
            {
                streamReaders[i] = new StreamReader(File.Open($"chunks/{i}.txt", FileMode.Open));
            }

            for (var i = 0; i < ChunkSize; i++)
            {
                var str = streamReaders[i].ReadLine();
                if (!string.IsNullOrEmpty(str))
                {
                    buffer[i] =  int.Parse(str);
                }
            }

            File.Delete("output/sorted.txt");
            var chunksLeft = ChunkSize;
            while (chunksLeft > 0)
            {
                int fileNameIndex = -1;
                int min = int.MaxValue;
                for (var i = 0; i < ChunkSize; i++)
                {
                    if (buffer[i] < min)
                    {
                        min = (int)buffer[i];
                        fileNameIndex = i;
                    }
                }

                File.AppendAllText("output/sorted.txt", $"{min.ToString()} ");
                if (!streamReaders[fileNameIndex].EndOfStream)
                {
                    var str = streamReaders[fileNameIndex].ReadLine();
                    if (!string.IsNullOrEmpty(str))
                    {
                        buffer[fileNameIndex] =  int.Parse(str);
                    }
                    else
                    {
                        chunksLeft--;
                    }
                }
                else
                {
                    buffer[fileNameIndex] =  null; 
                    chunksLeft--;
                }
            }
        }
    }
}

// Time Complexity
// K - chunk size, N - numbers in a file
// N / K - total number of chunks

// K * log(K) - sort one chunk of size K
// O(N * log(K)) - sort all chunks
// O(N * K) - find the minimum number N times in an array buffer of size K
// O((N * K) + (N * log(K)))
// O(N * K)