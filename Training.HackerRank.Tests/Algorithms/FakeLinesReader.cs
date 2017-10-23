using System;
using System.IO;
using Training.Common;

namespace Training.HackerRank.Tests.Algorithms
{
    public class FakeLinesReader : IReadLines, IDisposable
    {
        private readonly StreamReader _streamReader;

        public FakeLinesReader(string input)
        {
            var stream = GenerateStreamFromString(input);
            _streamReader = new StreamReader(stream);
        }

        public string ReadLine()
        {
            return _streamReader.ReadLine();
        }
        
        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }
    }
}