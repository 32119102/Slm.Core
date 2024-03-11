using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Authorize.Dto;

public class OutCaptchaDto
{
    /// <summary>
    /// id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// base64
    /// </summary>
    public string Img { get; set; }
}
