﻿using System.Diagnostics;

namespace ZXing.Net.Maui
{
    public class PerformanceCounter
    {
        static readonly Dictionary<string, Stopwatch> counters = new();

        public static string Start()
        {
            var guid = Guid.NewGuid().ToString();

            var sw = new Stopwatch();

            counters.Add(guid, sw);

            sw.Start();

            return guid;
        }

        public static TimeSpan Stop(string guid)
        {
            if (!counters.ContainsKey(guid))
                return TimeSpan.Zero;

            var sw = counters[guid];

            sw.Stop();

            counters.Remove(guid);

            return sw.Elapsed;
        }

        public static void Stop(string guid, string msg)
        {
            var elapsed = Stop(guid);

            if (!msg.Contains("{0}"))
                msg += " {0}";

            if (Debugger.IsAttached)
                Debug.WriteLine(msg, elapsed.TotalMilliseconds);
        }
    }
}