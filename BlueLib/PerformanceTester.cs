using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueLib
{
    public enum PerformanceTesterMethod
    {
        Stopwatch = 0,
        TickCount = 1,
        Time = 2
    }

    public class PerformanceTester
    {
        private Action a;
        private PerformanceTesterMethod ptm = PerformanceTesterMethod.Stopwatch;
        private object result;

        public object Result
        {
            get
            {
                return result;
            }
        }

        /// <summary>
        /// PerformanceTester using an Action. Method would be Stopwatch.
        /// </summary>
        /// <param name="a">Action to test performance.</param>
        public PerformanceTester(Action a)
        {
            this.a = a;
        }

        /// <summary>
        /// PerformanceTester using an Action.
        /// </summary>
        /// <param name="a">Action to test performance.</param>
        /// <param name="ptm">Method to test performance.</param>
        public PerformanceTester(Action a, PerformanceTesterMethod ptm)
        {
            this.a = a;
            this.ptm = ptm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object TestPerformance()
        {
            switch (ptm)
            {
                case PerformanceTesterMethod.Stopwatch:
                    Stopwatch sw = Stopwatch.StartNew();
                    a();
                    sw.Stop();
                    result = sw.Elapsed;
                    break;
                case PerformanceTesterMethod.TickCount:
                    int tc = Environment.TickCount;
                    a();
                    result = Environment.TickCount - tc;
                    break;
                case PerformanceTesterMethod.Time:
                    DateTime dt = DateTime.Now;
                    a();
                    result = DateTime.Now - dt;
                    break;
            }
            return result;
        }
    }

    public struct PerformanceTesterResult
    {
        private TimeSpan? ts;
        private int? tc;
        private DateTime? dt;
        private Type type;

        public PerformanceTesterResult(object obj)
        {
            ts = null;
            tc = null;
            dt = null;
            type = null;

            if (obj is TimeSpan)
            {
                this.ts = (TimeSpan)obj;
                this.type = ts.GetType();
            }
            else if (obj is int)
            {
                this.tc = (int)obj;
                this.type = tc.GetType();
            }
            else if (obj is DateTime)
            {
                this.dt = (DateTime)obj;
                this.type = dt.GetType();
            }
        }

        public static explicit operator TimeSpan(PerformanceTesterResult ptr)
        {
            SecurityCheck(ptr.ts.GetType(), ptr.type);
            return (TimeSpan)ptr;
        }

        public static explicit operator int(PerformanceTesterResult ptr)
        {
            //PerformanceTesterResult temp = ptr;
            //SecurityCheck(temp.tc.GetType(), temp.type);
            return (int)ptr;
        }

        public static explicit operator DateTime(PerformanceTesterResult ptr)
        {
            SecurityCheck(ptr.dt.GetType(), ptr.type);
            return (DateTime)ptr;
        }

        private static void SecurityCheck(Type type1, Type type2)
        {
            if(type1 != type2)
                throw new InvalidCastException();
        }
    }
}
