using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpuMonitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CPU Monitor Started");

            System.Timers.Timer _timer = new
                System.Timers.Timer();

            _timer.Interval = 3000;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Elapsed  += OnTimedEvent;


            Console.ReadKey();

        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            //Get the values and insert to DB
            var cpuValue = GetCpuValue();
            var dt = DateTime.Now;

            var record = "CPU %: " + cpuValue + ", time:" + dt; //, or ;

            Console.WriteLine(record);

            //append to text file
            File.AppendAllText("log.txt", record + Environment.NewLine);




        }




        private static int GetCpuValue()
        {
            var CpuCounter = new PerformanceCounter(
                "Processor", "% Processor Time", "_Total");
            CpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);

            var returnValue = (int)CpuCounter.NextValue();

            return returnValue;
        }



    }
}
