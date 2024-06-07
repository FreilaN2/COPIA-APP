﻿using SpinningTrainer.Models;
using System.Collections.ObjectModel;

namespace SpinningTrainer.Repositories
{
    public interface IUserRepository
    {
        (bool, string, int) AuthenticateUser(string username, string password);
        void Add(UserModel userModel);
        void Update(UserModel userModel);
        (bool, string) UpdatePassword(string username, string password);
        bool Delete(int id);
        UserModel GetById(int Id);
        UserModel GetByUserName(string username);
        string ValidateUsernameforPasswordChange(string username);
        string ValidateUserEmalforUsernameRecovery(string email);
        bool VerifyMembershipValidity(int id);
        ObservableCollection<UserModel> GetAll();

    }
}
