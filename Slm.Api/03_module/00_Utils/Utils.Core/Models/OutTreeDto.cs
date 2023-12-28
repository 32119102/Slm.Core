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
