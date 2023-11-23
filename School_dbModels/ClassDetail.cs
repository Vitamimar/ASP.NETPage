using System;
using System.Collections.Generic;

namespace WebApplication2.School_dbModels;

public partial class ClassDetail
{
    public int Id { get; set; }

    public int CurricularUnit { get; set; }

    public string ClassName { get; set; } = null!;

    public string ClassYear { get; set; } = null!;

    public int Teacher { get; set; }

    public virtual CurricularUnit CurricularUnitNavigation { get; set; } = null!;

    public virtual Person TeacherNavigation { get; set; } = null!;
}
