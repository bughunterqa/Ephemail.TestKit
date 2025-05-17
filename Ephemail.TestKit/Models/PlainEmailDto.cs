using System;
using System.Collections.Generic;

namespace Ephemail.TestKit.Models
{
    public class PlainEmailDto
    {
        public Guid Id { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Subject { get; set; }
        public DateTime ReceivedAt { get; set; }
        public string BodyText { get; set; } = string.Empty;
        public List<LinkDto> Links { get; set; } = new(); 
    }
}
