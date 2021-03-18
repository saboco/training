using System.Collections.Generic;

namespace Training.Leetcode
{
    public class Logger
    {
        private Dictionary<string, int> _logs = new Dictionary<string, int>();

        public bool ShouldPrintMessage(int timestamp, string message)
        {
            if (!_logs.ContainsKey(message))
            {
                _logs.Add(message, timestamp);
                return true;
            }
            if (timestamp - _logs[message] >= 10)
            {
                _logs[message] = timestamp;
                return true;
            }
            return false;
        }
    }
}
