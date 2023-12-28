using FluentValidation;
using Sys.Application.Company.Dto;
using Slm.Validation.FluentValidation;

namespace Sys.Application.Company.Validators;


public class InCompanyDtoValidator : AbstractValidator<InCompanyDto>
{
    public InCompanyDtoValidator()
    {
        //RuleFor(x => x.Name).Required().WithMessage("名称不能为空");


    }

}
