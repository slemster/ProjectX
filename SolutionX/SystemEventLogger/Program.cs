using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemEventLogger
{
    class Program
    {
        /// <summary>
        /// Sample console application to write events to the system log
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            WriteToSystemEventLog();
        }

        private static void WriteToSystemEventLog()
        {
            Console.WriteLine("Writing Test Message to System Event Log");
            
            try
            {
                var sSource = "ProjectX-SystemEventLogger";
                var sLog = "Application";
                var sEvent = "Sample Event";

                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);

                EventLog.WriteEntry(sSource, sEvent);

                EventLog.WriteEntry(sSource, sEvent, EventLogEntryType.Warning, 234);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Exception: {ex.InnerException?.ToString() ?? ""}");
                Console.WriteLine(
                    "If you receive this error, running this sample code with Administrator Rights may resolve the issue by allowing the 'Source' to be created.");
            }
            finally
            {
                Console.WriteLine("--Done--");
                Console.ReadLine();
            }
        }


    }
}
