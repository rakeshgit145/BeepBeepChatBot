using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeepBotUnitTest.Models
{

    public class Comment
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        public string CompairMessage { get; set; }
    }

    public class MessagesList
    {
        public List<Comment> Comment { get; set; }
    }
  

}
