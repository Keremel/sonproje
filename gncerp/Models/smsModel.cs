using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models
{
    public class smsModel
    {
        public class Header
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class Message
        {
            public string phoneNumber { get; set; }
            public string content { get; set; }
        }

        public class Body
        {
            public List<Message> messages { get; set; }
            public string title { get; set; }
        }

        public class Root
        {
            public Header header { get; set; }
            public Body body { get; set; }
        }




    }
}
