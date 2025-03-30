using System.Collections.Concurrent;
using MutualExclusion;

int numProcesses = 5;
BlockingCollection<int> tokenQueue = new BlockingCollection<int>(new ConcurrentQueue<int>());
tokenQueue.Add(0); // Start with process 0 holding the token

Task[] processes = new Task[numProcesses];
for (int i = 0; i < numProcesses; i++)
{
    int id = i;
    processes[i] = Task.Run(() => new TokenRing(id, numProcesses, tokenQueue).Run());
}

Task.WaitAll(processes);