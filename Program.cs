using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace processingtask
{
    class Program
    {
        private static FileStream outFile;

        static void Main(string[] args)
        {
            var dataInPath = GetEnvironmentVariable("DATA");
            var dataOutPath = GetEnvironmentVariable("OUTPUT");

            foreach (var line in ReadLines(dataInPath))
            {
                ProcessLine(dataOutPath, line);
            }
        }

        private static void ProcessLine(string dataOutPath, string data)
        {
            var output = $"Processed: {data}\n";
            Thread.SpinWait(5000);
            WriteToOutput(dataOutPath, output);
        }

        private static void WriteToOutput(string dataOutPath, string data)
        {
            if (outFile == null)
            {
                outFile = File.OpenWrite(dataOutPath);
            }
            outFile.Write(new System.Text.UTF8Encoding().GetBytes(data));
        }

        private static IEnumerable<string> ReadLines(string dataInPath)
        {
            using (var file = File.OpenText(dataInPath))
            {
                do
                {
                    yield return file.ReadLine();
                } while (!file.EndOfStream);
            }
        }

        private static string GetEnvironmentVariable(string variableName)
        {
            var value = Environment.GetEnvironmentVariable(variableName);
            if (value == null)
            {
                throw new ArgumentException($"Environmentvariable {variableName} not set.");
            }
            return value;
        }
    }
}
