using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.School_dbModels;

public partial class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime Birth { get; set; }

    public int? Roles { get; set; }
    public virtual ICollection<ClassDetail>? ClassDetails { get; set; } = new List<ClassDetail>();

    public virtual Role? RolesNavigation { get; set; } = null!;
}