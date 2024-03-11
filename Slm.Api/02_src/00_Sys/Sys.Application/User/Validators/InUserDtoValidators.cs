using FluentValidation;
using Slm.Validation.FluentValidation;
using Sys.Application.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.User.Validators;


public class InUserDtoValidators : AbstractValidator<InUserDto>
{
    public InUserDtoValidators()
    {
        RuleFor(a => a.Phone).Phone().WithMessage("请输入正确的手机号");
        //RuleFor(x => x.Name).Required().WithMessage("名称不能为空");


    }

}
