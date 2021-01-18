using System;

namespace WebApplication1.Models
{
    public class ClaimDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public Guid UserId { get; set; }
    }
}
