using System;

namespace Ephemail.TestKit.Models
{
    public class CompactEmailDto
    {
        public Guid Id { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Subject { get; set; }
        public DateTime ReceivedAt { get; set; }
        public string Preview { get; set; } = string.Empty;
    }
}
