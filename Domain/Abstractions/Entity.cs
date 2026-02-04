using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions;

public abstract  class Entity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
