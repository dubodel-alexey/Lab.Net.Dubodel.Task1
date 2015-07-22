using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfRestfulService
{
    public interface IUserRepository
    {
        List<User> Get();
        User Get(int id);
        User Add(User item);
        bool Delete(int id);
        bool Update(User item);
    }
}
