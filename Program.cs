using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class Program
    {
        public static Guid _myGuid;
        public static String _myName;
        public static String _chatRoom;
        static void Main(string[] args)
        {
            bool run = true;
            _myGuid = Guid.NewGuid();
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            ISubscriber sub = redis.GetSubscriber();

            Console.Write("Enter your name --> ");
            _myName = Console.ReadLine();

            Console.Write("Enter chat room (messages) --> ");
            _chatRoom = Console.ReadLine().ToLower();
            if (String.IsNullOrWhiteSpace(_chatRoom)) _chatRoom = "messages";
            sub.Subscribe(_chatRoom, Subscription);
            Console.WriteLine("You can now chat");

            while (run)
            {
                var x = Console.ReadLine();
                if (x.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                {
                    run = false;
                    break;
                }
                Message m = new Message(_myGuid, _myName, x);
                sub.Publish(_chatRoom, m.ToJson());

            }
        }

        private static void Subscription(RedisChannel channel, RedisValue message)
        {
            Message messageObject = (Message)message.ToString().ToInstance(typeof(Message));
            if (messageObject.SenderGuid != _myGuid)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(String.Format("{0}: {1}", messageObject.SenderName, messageObject.Content));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
