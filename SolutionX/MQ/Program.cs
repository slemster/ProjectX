using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MQ
{
    class Program
    {
        /// <summary>
        /// Sample application to read/write messages to the local MSMQ
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) 
        {

            Console.WriteLine("Sending Test Message to Queue");

            try
            {
                MQInit();
                PushMessage();

                Console.WriteLine("Press enter to pop message off queue");
                Console.ReadLine();

                PopMessage();

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
        

        private static void MQInit()           
        {
            if (!MessageQueue.Exists(@".\Private$\ProjectX-Upload"))
            {
                MessageQueue.Create(@".\Private$\ProjectX-Upload");
            }

            if (!MessageQueue.Exists(@".\Private$\ProjectX-Download"))
            {
                MessageQueue.Create(@".\Private$\ProjectX-Download");
            }
        }

        private static void PushMessage()      
        {
            using (var mq = new MessageQueue(".\\Private$\\ProjectX-Upload"))
            {
                var rm    = new MQMessage { FileName = "\\1000.bin" };
                var mqmsg = new Message { Body = rm };

                mq.Send(mqmsg);
            }
        }

        private static void PopMessage()       
        {
            using (var msgQ = new MessageQueue(".\\Private$\\ProjectX-Upload"))
            {
                var rm         = new MQMessage();
                var o          = new Object();
                var arrTypes   = new System.Type[2];
                arrTypes[0]    = rm.GetType();
                arrTypes[1]    = o.GetType();
                msgQ.Formatter = new XmlMessageFormatter(arrTypes);

                var x = msgQ.Receive(); // This is not how we want to see if there are any messages because it will sleep until a message is added to the queue
                
                rm = ((MQMessage)x.Body);

                Console.WriteLine($"Message Received: {rm.FileName}");
            }
        }


    }

    public class MQMessage
    {
        public string FileName { get; set; }
    }
}
