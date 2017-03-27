using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class Message
    {
        public Message()
        {

        }
        public Message(Guid myGuid, string myName, string content)
        {
            this.SenderGuid = myGuid;
            this.SenderName = myName;
            this.Content = content;
        }

        public Guid SenderGuid { get; set; }
        public String SenderName { get; set; }
        public String Content { get; set; }
    }
}
