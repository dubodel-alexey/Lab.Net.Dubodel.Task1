using System;
using System.ComponentModel.DataAnnotations;
using task3.Models;

namespace task3.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PasswordsMatchAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var signUpModel = (SignUpViewModel)value;
                if (signUpModel.Password == signUpModel.ConfirmPassword)
                    return true;
            }
            return false;
        }
    }
}