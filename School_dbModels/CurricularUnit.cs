using System;
using System.Collections.Generic;

namespace WebApplication2.School_dbModels;

public partial class CurricularUnit
{
    public int Id { get; set; }

    public string UnitName { get; set; } = null!;

    public string Objectives { get; set; } = null!;

    public virtual ICollection<ClassDetail> ClassDetails { get; set; } = new List<ClassDetail>();
}
