using System;
using System.Threading;
using System.Threading.Tasks;

namespace taskTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            var t = Task.Run(async delegate
            {
                await Task.Delay(3000, source.Token);
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("tik");
                }
                //throw new Exception("stoped");
                //return 42;
                return "end";
            });
            Console.WriteLine("I here and wait");
            Thread.Sleep(2899);
            Console.WriteLine("I woke up");
            source.Cancel();
            try
            {
                t.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                    Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
            }
            Console.Write("Task t Status: {0}", t.Status);
            if (t.Status == TaskStatus.RanToCompletion)
                Console.Write(", Result: {0}", t.Result);
            source.Dispose();
        }
    }
}
