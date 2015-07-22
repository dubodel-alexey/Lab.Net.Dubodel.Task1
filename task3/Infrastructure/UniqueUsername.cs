using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using task3.Infrastructure.Authentication;
using task3.Models;

namespace task3.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueUsername : ValidationAttribute
    {

        private DbContext _db = new DataContext();
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var username = (string)value;
                var user = _db.Set<User>().
                    FirstOrDefault(u => u.Username == username);
                if (user == null)
                    return true;
            }
            return false;
        }
    }
}