using System;
using System.Collections.Generic;

namespace LiveHappy.Domain.Models
{
    public class Anecdote
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public ICollection<AnecdoteTag> AnecdoteTags { get; set; } = new List<AnecdoteTag>();
    }
}
