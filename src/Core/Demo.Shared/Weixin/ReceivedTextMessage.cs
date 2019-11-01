using System;

namespace Demo.Models
{
    public class ReceivedTextMessage : Entity
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTimeOffset ReceivedTime { get; set; }
        public string Content { get; set; }
    }
}

