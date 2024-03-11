using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slm.Validation.FluentValidation.Validators;

/// <summary>
/// 自定义手机号验证
/// </summary>
public class PhoneValidator<T> : PropertyValidator<T, string>
{
    private const string Pattern = @"^1[345789]\d{9}$";
    private static Regex _regex;


    public PhoneValidator()
    {
        _regex = new Regex(Pattern);
    }

    public override string Name => "PhoneValidator";
    public override bool IsValid(ValidationContext<T> context, string propertyValue)
    {
        if (propertyValue == null)
            return false;
        return _regex.IsMatch(propertyValue.ToString());

    }


    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "'{PropertyName}' 手机号无效";
    }
}
