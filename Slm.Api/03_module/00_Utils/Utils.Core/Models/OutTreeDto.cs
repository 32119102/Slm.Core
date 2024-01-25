using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Utils.Core.Models;

public class OutTreeDto
{
    public dynamic Key { get; set; }


    public string Title { get; set; }

    public List<OutTreeDto> Children { get; set; }

}


public class OutCascaderDto
{
    public dynamic Value { get; set; }


    public string Label { get; set; }

    public bool Disabled { get; set; }

    public List<OutCascaderDto> Children { get; set; }

}
