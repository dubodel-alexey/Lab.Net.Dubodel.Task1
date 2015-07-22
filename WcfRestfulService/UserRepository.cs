using System;
using System.Collections.Generic;

namespace WcfRestfulService
{
    public class UserRepository : IUserRepository
    {
        private List<User> books = new List<User>();
        private int counter = 1;

        public UserRepository()
        {
            Add(new User { id = 1, Name = "user1", Address = "address1" });
            Add(new User { id = 2, Name = "user2", Address = "address2" });
            Add(new User { id = 3, Name = "user3", Address = "address3" });
        }

        public List<User> Get()
        {
            return books;
        }

        public User Get(int id)
        {
            return books.Find(x => x.id == id);
        }

        public User Add(User item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            item.id = counter++;
            books.Add(item);
            return item;
        }

        public bool Delete(int id)
        {
            int idx = books.FindIndex(b => b.id == id);
            if (idx == -1)
                return false;

            books.RemoveAll(b => b.id == id);
            return true;
        }

        public bool Update(User item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            int idx = books.FindIndex(b => b.id == item.id);
            if (idx == -1)
                return false;

            books.RemoveAt(idx);
            books.Add(item);
            return true;
        }
    }
}