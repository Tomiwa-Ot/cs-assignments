using System.Collections.Concurrent;

namespace MutualExclusion
{
    class TokenRing
    {
        private int processId;
        private int totalProcesses;
        private BlockingCollection<int> tokenQueue;
        private static object lockObject = new object();

        public TokenRing(int id, int total, BlockingCollection<int> queue)
        {
            processId = id;
            totalProcesses = total;
            tokenQueue = queue;
        }

        public void Run()
        {
            while (true)
            {
                int token;
                if (tokenQueue.TryTake(out token, Timeout.Infinite))
                {
                    if (token == processId)
                    {
                        lock (lockObject)
                        {
                            Console.WriteLine($"Process {processId} is entering critical section.");
                            Thread.Sleep(1000); // Simulate work in critical section
                            Console.WriteLine($"Process {processId} is leaving critical section.");
                        }
                        int nextProcess = (processId + 1) % totalProcesses;
                        tokenQueue.Add(nextProcess);
                    }
                    else
                    {
                        tokenQueue.Add(token); // Pass the token forward
                    }
                }
            }
        }
    }
}