using System;

namespace FarmConnect.Core.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserMessage { get; set; }
        public string AIResponse { get; set; }
        public string Context { get; set; } // crops, soil, pests, etc.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; }
    }
}
