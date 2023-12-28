using System;
using System.Collections.Generic;
using System.Text;

namespace Slm.Data.Abstractions.Attributes;

public class TableFieldAttribute : Attribute
{
    public TableTypeEnum TableType { get; set; }

    public HandleTypeEnum HandleType { get; set; }
}



public enum TableTypeEnum
{
    UserId,
    UserName,
    TenantId,
    CreatOrgId,
    Dt,
    IsDelete
}


public enum HandleTypeEnum
{
    Add,
    Edit,
    Del
}