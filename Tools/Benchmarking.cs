using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TamanaEngine
{
    public class Benchmarking
    {
        private static Stopwatch stopwatch;

        public static void Start()
        {
            if(stopwatch == null)
            {
                stopwatch = new Stopwatch();
            }

            stopwatch.Reset();
            stopwatch.Start();
        }

        public static void Stop()
        {
            stopwatch.Stop();
            Console.WriteLine("Millisecond : " + (float)stopwatch.ElapsedMilliseconds);
        }
    }
}
