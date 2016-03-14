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
        
        /// <summary>
        /// If it returns null, Result is null.
        /// </summary>
        public Type Type
        {
            get
            {
                if (result is TimeSpan)
                    return typeof(TimeSpan);
                else if (result is int)
                    return typeof(int);
                else if (result is DateTime)
                    return typeof(DateTime);
                else
                    return null;
            }
        }

        public object Result
        {
            get
            {
                return result;
            }
        }

        /// <summary>
        /// PerformanceTester using an Action. Method will be Stopwatch.
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
        /// Run test performance method.
        /// </summary>
        /// <returns>TimeSpan for Stopwatch. int for TickCount. DateTime for Time</returns>
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

        /// <summary>
        /// Check result type is int or not.
        /// </summary>
        /// <returns>True if result type is int. otherwise false.</returns>
        public bool IsResultInt()
        {
            return this.Type == typeof(int);
        }

        /// <summary>
        /// Check result type is TimeSpan or not.
        /// </summary>
        /// <returns>True if result type is TimeSpan. otherwise false.</returns>
        public bool IsResultTimeSpan()
        {
            return this.Type == typeof(TimeSpan);
        }

        /// <summary>
        /// Check result type is DateTime or not.
        /// </summary>
        /// <returns>True if result type is DateTime. otherwise false.</returns>
        public bool IsResultDateTime()
        {
            return this.Type == typeof(DateTime);
        }
    }

    // WIP (Work in progress)
    /*
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

        public static implicit operator TimeSpan(PerformanceTesterResult ptr)
        {
            SecurityCheck(ptr.ts.GetType(), ptr.type);
            return (TimeSpan)ptr;
        }

        public static implicit operator int(PerformanceTesterResult ptr)
        {
            //PerformanceTesterResult temp = ptr;
            //SecurityCheck(temp.tc.GetType(), temp.type);
            return (int)ptr;
        }

        public static implicit operator DateTime(PerformanceTesterResult ptr)
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
    */
}
