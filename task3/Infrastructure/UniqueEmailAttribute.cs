using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using task3.Infrastructure.Authentication;
using task3.Models;

namespace task3.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueEmailAttribute : ValidationAttribute
    {

        private DbContext _db = new DataContext();
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var Email = (string)value;
                var user = _db.Set<User>().
                    FirstOrDefault(u => u.Username == Email);
                if (user == null)
                    return true;
            }
            return false;
        }
    }
}