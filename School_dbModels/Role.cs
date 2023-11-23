using System;
using System.Collections.Generic;

namespace WebApplication2.School_dbModels;

public partial class Role
{
    public int Id { get; set; }

    public string Labels { get; set; } = null!;

    public string RoleDescription { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
