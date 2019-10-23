using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHappy.Domain.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AnecdoteTag> AnecdoteTags { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as Tag;

            if (item == null)
            {
                return false;
            }

            if (Name == item.Name)
                return true;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
