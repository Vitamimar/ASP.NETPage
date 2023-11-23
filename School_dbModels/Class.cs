using System;
using System.Collections.Generic;

namespace WebApplication2.School_dbModels;

public partial class Class
{
    public int ClassDetails { get; set; }

    public int Students { get; set; }

    public virtual ClassDetail ClassDetailsNavigation { get; set; } = null!;

    public virtual Person StudentsNavigation { get; set; } = null!;
}
