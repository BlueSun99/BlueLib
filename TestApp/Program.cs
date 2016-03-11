using BlueLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool IsEnabled = true;

            /* Test PerformanceTester */
            Action a1 = delegate ()
            {
                for (int i = 0; i < 10000; i++)
                {
                    if (IsEnabled)
                        Console.WriteLine("Hello!");
                }
            };

            Action a2 = delegate ()
            {
                for (int i = 0; i < 10000; i++)
                {
                    Console.WriteLine("Hello!");
                }
            };

            PerformanceTester pt1 = new PerformanceTester(a1, PerformanceTesterMethod.Time);
            PerformanceTester pt2 = new PerformanceTester(a2, PerformanceTesterMethod.Time);

            // NOTE THAT IT WILL NOT BE ONLY TimeSpan.
            // IF YOU CHOOSE TickCount, IT WILL BE int.
            // IF YOU CHOOSE DateTime, IT WILL BE DateTime.

            a1();
            Console.Clear();
            a2();
            Console.Clear();

            var ts1 = (TimeSpan)pt1.TestPerformance();
            Console.Clear();
            var ts2 = (TimeSpan)pt2.TestPerformance();
            Console.Clear();
            Console.WriteLine(ts1.Milliseconds);
            Console.WriteLine(ts2.Milliseconds);

            Console.ReadKey(true);
        }
    }
}
