﻿using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface IUserRepository
    {
        public IList<User> GetAllUser();
        public User FindByName(string userName);
        public User FindByNameAndPassword(string userName, string password);
        public User FindById(int userId);
    }
}
