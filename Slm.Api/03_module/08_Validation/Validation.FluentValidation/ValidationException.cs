using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Validation.FluentValidation;

public class ValidationException : Exception
{
    public ValidationException() : base("One or more validation failures have occurred.")
    {
    }

    public ValidationException(string failures)
        : base(failures)
    {
    }
}