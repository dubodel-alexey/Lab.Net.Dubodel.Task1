using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using task3.Infrastructure.Authentication;
using task3.Models;

namespace task3.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LoginValidationAttribute : ValidationAttribute
    {

        private DbContext _db = new DataContext();
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var loginModel = (LoginViewModel)value;
                var user = _db.Set<User>().
                    FirstOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password) ??
                        _db.Set<User>().
                        FirstOrDefault(u => u.Email == loginModel.Username && u.Password == loginModel.Password);
                if (user != null)
                    return true;
            }
            return false;
        }
    }
}