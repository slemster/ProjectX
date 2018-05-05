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
        /// Declaring any variable in your code has overhead. 
        /// When I have been doing some profiling sessions in the past to optimize code, 
        /// I have noticed that the constructors on the LogManager object can use a lot of CPU.
        /// 
        /// 
        /// Declare it as static and use this little trick so you don’t have to hard code the class type
        /// 
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        


        /// <summary>
        /// Sample console application to write events to the system log
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //WriteToSystemEventLog();

            WriteToSystemEventLogUsingLog4Net();
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

        private static void WriteToSystemEventLogUsingLog4Net()
        {
            // https://stackify.com/log4net-guide-dotnet-logging/

            Console.WriteLine("Writing Test Message to System Event Log");

            try
            {
                log.Debug("Throwing 'Debug' Message");
                log.Info("Throwing 'Info' Message");
                log.Warn("Throwing 'Warning' Message");
                log.Error("Throwing 'Error' Message");
                log.Fatal("Throwing 'Fatal' Message");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Exception: {ex.InnerException?.ToString() ?? ""}");
            }
            finally
            {
                Console.WriteLine("--Done--");
                Console.ReadLine();
            }
            
        }

    }
}
