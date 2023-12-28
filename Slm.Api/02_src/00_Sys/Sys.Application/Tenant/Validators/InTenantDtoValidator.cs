using FluentValidation;
using Sys.Application.Tenant.Dto;

namespace Sys.Application.Tenant.Validators;

/// <summary>
/// 租户新增验证
/// </summary>
public class InTenantDtoValidator : AbstractValidator<InTenantDto>
{


    public InTenantDtoValidator()
    {
        //RuleFor(x => x.Name).Required().WithMessage("名称不能为空");




    }



}

