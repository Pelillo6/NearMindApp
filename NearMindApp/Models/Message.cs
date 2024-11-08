using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMindApp.Models
{
    public class Message
    {
        public required string User { get; set; }
        public required string Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
