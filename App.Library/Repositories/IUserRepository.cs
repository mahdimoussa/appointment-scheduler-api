﻿using App.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Library.Repositories
{
    public interface IUserRepository
    {
        Task<Tuple<bool, List<User>>> GetAllUsers();
        Task<Tuple<bool, ResponseMessage>> SaveUser(User User);
        Task<Tuple<bool, bool>> DeleteUser(int Id);
         

    }
}
