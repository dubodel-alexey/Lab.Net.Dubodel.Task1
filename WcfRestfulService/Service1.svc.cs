using System.Collections.Generic;

namespace WcfRestfulService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static IUserRepository repository = new UserRepository();
        public List<User> GetData()
        {
            var users = new List<User>
            {
                new User {id = 1, Name = "user1", Address = "address1"},
                new User {id = 2, Name = "user2", Address = "address2"}
            };

            return users;
        }

        public List<User> GetUsersList()
        {
            return repository.Get();
        }

        public User GetUserById(string id)
        {
            return repository.Get(int.Parse(id));
        }

        public string AddUser(User user)
        {
            User newUser = repository.Add(user);
            return "id=" + newUser.id;
        }

        public string UpdateUser(User user)
        {
            bool updated = repository.Update(user);
            if (updated)
                return "User with id = " + user.id + " updated successfully";
            return "Unable to update user with id = " + user.id;
        }

        public string DeleteUser(string id)
        {
            bool deleted = repository.Delete(int.Parse(id));
            if (deleted)
                return  "User with id = " + id + " deleted successfully.";
            return "Unable to delete user with id = " + id;
        }
    }
}
